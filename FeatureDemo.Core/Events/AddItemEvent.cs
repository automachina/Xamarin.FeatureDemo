using System;
using FeatureDemo.Core.Models;
using Prism.Events;

namespace FeatureDemo.Core.Events
{
    public class AddItemEvent : PubSubEvent<Item>
    {
        public AddItemEvent()
        {
        }
    }
}
