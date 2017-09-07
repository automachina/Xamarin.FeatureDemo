using System;
namespace FeatureDemo.Model.Client
{
    public class EmailParameters : IActionParameters
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
