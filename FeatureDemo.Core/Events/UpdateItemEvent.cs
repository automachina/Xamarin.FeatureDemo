using System;
using FeatureDemo.Core.Models;
using Prism.Events;

namespace FeatureDemo.Core.Events
{
    public class UpdateItemEvent : PubSubEvent<Item>
    {
        public UpdateItemEvent()
        {
        }
    }
}
