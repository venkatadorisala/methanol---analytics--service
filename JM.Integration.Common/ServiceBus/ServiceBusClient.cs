// Copyright (c) Johnson Matthey Organization 2021. All rights reserved.

namespace JM.Integration.Common.Services
{
    using JM.Integration.Common;
    using JM.Integration.Common.Constants;
    using Microsoft.Azure.ServiceBus;
    using Microsoft.Azure.ServiceBus.Core;
    using Newtonsoft.Json;
    using Polly;
    using Polly.Wrap;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class ServiceBusClient : IServiceBusClient
    {
        private readonly IMessageSender _primaryMessageSender;
        private const int numberOfHours = 5;
        private const int retryCount = 3;
        private const double timeSpanCount = 3;
        private const double powCount = 2;
        private const double transientCount = .1;

        private readonly IAsyncPolicy _transientRetryPolicy = Policy.HandleInner<TimeoutException>()
                                                                    .OrInner<ServerBusyException>()
                                                                    .WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromSeconds(transientCount * Math.Pow(powCount, retryAttempt)));

        private readonly IAsyncPolicy _circuitBreakerPolicy = Policy.HandleInner<TimeoutException>()
                                                                    .OrInner<ServerBusyException>()
                                                                    .CircuitBreakerAsync(retryCount, TimeSpan.FromSeconds(timeSpanCount));

        public ServiceBusClient(IMessageSender primaryMessageSender)
        {
            _primaryMessageSender = primaryMessageSender;
        }

        /// <summary>
        /// Send Message to topic
        /// </summary>
        /// <param name="message"></param>
        /// <param name="userProperties"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public async Task SendMessageAsync(object message, Dictionary<string, object> userProperties, string label)
        {
            Message messageToSend = CreateMessage(message, userProperties, label);
            AsyncPolicyWrap resilienceStrategy = Policy.WrapAsync(_transientRetryPolicy, _circuitBreakerPolicy);
            await resilienceStrategy.ExecuteAsync(async () => await _primaryMessageSender.SendAsync(messageToSend));
        }

        /// <summary>
        /// Schedule Sending a message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="userProperties"></param>
        /// <param name="scheduleEnqueueTimeUtc"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public async Task<long> ScheduleMessageAsync(object message, Dictionary<string, object> userProperties, DateTimeOffset scheduleEnqueueTimeUtc, string label)
        {
            if (scheduleEnqueueTimeUtc <= DateTimeOffset.UtcNow)
            {
                scheduleEnqueueTimeUtc = DateTimeOffset.UtcNow.AddHours(numberOfHours);
            }

            Message messageToSend = CreateMessage(message, userProperties, label);
            return await _primaryMessageSender.ScheduleMessageAsync(messageToSend, scheduleEnqueueTimeUtc);
        }

        /// <summary>
        /// Cancel Schedule Message
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public async Task CancelScheduledMessageAsync(long messageId)
        {
            await _primaryMessageSender.CancelScheduledMessageAsync(messageId);
        }

        private Message CreateMessage(object body, Dictionary<string, object> userProperties, string label)
        {
            Message messageToSend = new Message
            {
                ContentType = CommonConstant.ContentTypeJson,
                Body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented })),
                Label = label
            };

            foreach (KeyValuePair<string, object> userProperty in userProperties)
            {
                messageToSend.UserProperties.Add(userProperty.Key, userProperty.Value);
            }

            return messageToSend;
        }
    }
}