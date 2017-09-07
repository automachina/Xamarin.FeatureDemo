using System;
namespace FeatureDemo.Model.Client
{
    public class LifeCycleArgs : IEventArguments
    {
        public string Message { get; set; }
        public string Content { get; set; }
    }
}
