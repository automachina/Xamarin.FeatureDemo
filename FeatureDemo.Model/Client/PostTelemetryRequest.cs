using System.Collections.Generic;

namespace FeatureDemo.Model.Client
{
    public class PostTelemetryRequest
    {
        public string DeviceId { get; set; }
        public List<Telemetry> Telemetry { get; set; }
    }
}
