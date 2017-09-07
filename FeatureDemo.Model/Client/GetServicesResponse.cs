using System.Collections.Generic;

namespace FeatureDemo.Model.Client
{
    public class GetServicesResponse
    {
        public List<Service> Services { get; set; }
        public string Version { get; set; }
    }
}
