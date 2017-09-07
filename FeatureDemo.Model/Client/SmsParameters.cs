using System;
namespace FeatureDemo.Model.Client
{
    public class SmsParameters : IActionParameters
    {
        public string Number { get; set; }
        public string Body { get; set; }
    }
}
