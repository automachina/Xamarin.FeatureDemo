using System;
namespace FeatureDemo.Model.Client
{
    public class InteractionArgs : IEventArguments
    {
        public InteractionType Interaction { get; set; }
        public string Content { get; set; }
    }
}
