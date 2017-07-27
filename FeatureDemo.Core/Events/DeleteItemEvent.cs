using FeatureDemo.Core.Models;
using Prism.Events;

namespace FeatureDemo.Core.Events
{
    public class DeleteItemEvent : PubSubEvent<Item>
    {
        public DeleteItemEvent()
        {
        }
    }
}
