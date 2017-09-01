using System;
using System.ComponentModel;

namespace FeatureDemo.Model
{
    public enum EventType
    {
        [Description("Logging an app debug message.")]
        Debug,
        [Description("Logging an app error.")]
        Error,
        [Description("Tracking app performance.")]
        Performance,
        [Description("Tracking app interactions.")]
        Interaction,
        [Description("Tracking app life-cycle.")]
        LifeCycle
    }
}
