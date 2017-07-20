using System;
using FeatureDemo.Core.Models;
using Prism.Events;

namespace FeatureDemo.Core.Events
{
    public class SaveItemEvent : PubSubEvent<Item>
    {
        public SaveItemEvent()
        {
        }
    }
}
