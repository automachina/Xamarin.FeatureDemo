using System.Collections.Generic;

namespace FeatureDemo.Model.Client
{
    public class PostCaptureResultsRequest
    {
        public string Id { get; set; }
        public List<string> Results { get; set; }
    }
}
