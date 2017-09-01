using System;
namespace FeatureDemo.Model
{
    public class ProfileCreateRequest
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DeviceId { get; set; }
        public DateTime Submitted { get; set; }
    }
}
