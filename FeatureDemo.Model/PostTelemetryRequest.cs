using System;
namespace FeatureDemo.Model
{
    public class PostTelemetryRequest
    {
        public string DeviceId { get; set; }
        public List<Telemetry> Telemetry { get; set; }
    }
}
