using System;
using System.ComponentModel;

namespace FeatureDemo.Model.Client
{
    public enum MetricType
    {
        [Description("The amount of time taken to load a resource.")]
        LoadTime,
        [Description("The amount of memory consumed by the app.")]
        MemoryUsage,
        [Description("The amount of CPU time comsumed by the app.")]
        CpuUsage,
        [Description("The amount of data consumed by the app.")]
        DataUsage
    }
}
