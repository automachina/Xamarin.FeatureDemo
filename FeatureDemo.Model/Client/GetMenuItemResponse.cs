using System.Collections.Generic;

namespace FeatureDemo.Model.Client
{
    public class GetMenuItemResponse
    {
        public List<MenuItem> Items { get; set; }
        public string Version { get; set; }
    }
}
