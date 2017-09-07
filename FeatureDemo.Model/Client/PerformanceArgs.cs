using System;
namespace FeatureDemo.Model.Client
{
    public class PerformanceArgs : IEventArguments
    {
        public MetricType Metric { get; set; }
        public string Value { get; set; }
    }
}
