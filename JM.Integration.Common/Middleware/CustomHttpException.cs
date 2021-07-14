// Copyright (c) Johnson Matthey Organization 2021. All rights reserved.

namespace JM.Integration.Common.Middleware
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http;
    using System.Runtime.Serialization;

    /// <summary>
    /// CustomHttpException.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class CustomHttpException : Exception
    {
        public CustomHttpException()
        {
        }

        public CustomHttpException(string message)
            : base(message)
        {
        }

        protected CustomHttpException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        public CustomHttpException(string message, Exception innerException)
        : base(message, innerException)
        {
        }

        public CustomHttpException(HttpResponseMessage response)
        {
            this.Response = response;
        }

        public HttpResponseMessage Response { get; set; }
    }
}