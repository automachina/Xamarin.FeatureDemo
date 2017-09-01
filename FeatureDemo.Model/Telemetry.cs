using System;
namespace FeatureDemo.Model
{
    public class Telemetry
    {
        public DateTime Date { get; set; }
        public EventType EventType { get; set; }
        public string Arguments { get; set; }
    }
}
