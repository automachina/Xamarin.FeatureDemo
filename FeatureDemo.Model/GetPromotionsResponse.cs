using System;
namespace FeatureDemo.Model
{
    public class GetPromotionsResponse
    {
        public List<Promotion> Promotions { get; set; }
        public string Version { get; set; }
    }
}
