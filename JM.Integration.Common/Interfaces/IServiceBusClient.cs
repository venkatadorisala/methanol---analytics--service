// Copyright (c) Johnson Matthey Organization 2021. All rights reserved.

namespace JM.Integration.Common
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// IServiceBusClient.
    /// </summary>
    public interface IServiceBusClient
    {
        /// <summary>
        /// SendMessageAsync.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="userProperties"></param>
        /// <param name="label"></param>
        /// <returns>.</returns>
        Task SendMessageAsync(object message, Dictionary<string, object> userProperties, string label);

        /// <summary>
        /// ScheduleMessageAsync.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="userProperties"></param>
        /// <param name="scheduleEnqueueTimeUtc"></param>
        /// <param name="label"></param>
        /// <returns>long.</returns>
        Task<long> ScheduleMessageAsync(object message, Dictionary<string, object> userProperties, DateTimeOffset scheduleEnqueueTimeUtc, string label);

        /// <summary>
        /// CancelScheduledMessageAsync.
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns>.</returns>
        Task CancelScheduledMessageAsync(long messageId);
    }
}