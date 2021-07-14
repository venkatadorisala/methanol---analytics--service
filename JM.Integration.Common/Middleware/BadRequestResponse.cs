// Copyright (c) Johnson Matthey Organization 2021. All rights reserved.

namespace JM.Integration.Common.Middleware
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    /// <summary>
    /// BadRequestResponse.
    /// </summary>
    public class BadRequestResponse : ApiResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestResponse"/> class.
        /// </summary>
        public BadRequestResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestResponse"/> class.
        /// </summary>
        /// <param name="context"></param>
        public BadRequestResponse(ActionContext context)
        {
            this.StatusCode = (int)HttpStatusCode.BadRequest;
            this.StatusDescription = nameof(HttpStatusCode.BadRequest);
            this.Errors = this.GenerateErrors(context);
        }

        /// <summary>
        /// GenerateErrors.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>string.</returns>
        private List<string> GenerateErrors(ActionContext context)
        {
            List<string> errorList = new List<string>();

            foreach (var keyModelStatePair in context.ModelState)
            {
                var errors = keyModelStatePair.Value.Errors;

                if (errors == null || errors.Count <= 0)
                {
                    continue;
                }

                errorList.AddRange(errors.Select(modelError => modelError.ErrorMessage));
            }

            return errorList;
        }
    }
}