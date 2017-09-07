using System;
namespace FeatureDemo.Model.Client
{
    public class Telemetry
    {
        public DateTime Date { get; set; }
        public EventType EventType { get; set; }
        public IEventArguments Arguments { get; set; }
    }
}
