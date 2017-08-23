using System;
using Newtonsoft.Json;

namespace FeatureDemo.Api.Models
{
    public class Item
    {

        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null | !(obj is Item)) return false;
            var item = obj as Item;
            return Id == item.Id && Text == item.Text && Description == item.Description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
