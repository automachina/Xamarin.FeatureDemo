using System.Collections.Generic;

namespace FeatureDemo.Model.Client
{
    public class GetCapturesResponse
    {
        public List<Capture> Captures { get; set; }
        public string Version { get; set; }
    }
}
