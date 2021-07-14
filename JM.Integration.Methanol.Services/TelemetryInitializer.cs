using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace JM.Integration.Methanol.Services
{
    public class TelemetryInitializer : ITelemetryInitializer
    {
        private readonly IConfiguration _config;

        public TelemetryInitializer(IConfiguration config)
        {
            //_config = config;
        }

        public void Initialize(ITelemetry telemetry)
        {
            //telemetry.Context.GlobalProperties.TryAdd("familyName", _config["FamilyName"]);
            var activity = Activity.Current;

            //  telemetry.Context.Operation.ParentId =v.FirstOrDefault(x=>x.)

            if (activity != null)
            {
                telemetry.Context.Operation.ParentId = activity?.Tags.SingleOrDefault(x => x.Key == "ParentId").Value;

                foreach (var tag in activity.Tags)
                {
                    telemetry.Context.GlobalProperties.TryAdd(tag.Key, tag.Value);
                }
            }
        }
    }
}