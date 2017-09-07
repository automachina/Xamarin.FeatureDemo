using System.Collections.Generic;

namespace FeatureDemo.Model.Client
{
    public class UserLoginResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<UserOption> Preferences { get; set; }
        public string Token { get; set; }
    }
}
