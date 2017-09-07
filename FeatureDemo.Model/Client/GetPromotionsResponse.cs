using System.Collections.Generic;

namespace FeatureDemo.Model.Client
{
    public class GetPromotionsResponse
    {
        public List<Promotion> Promotions { get; set; }
        public string Version { get; set; }
    }
}
