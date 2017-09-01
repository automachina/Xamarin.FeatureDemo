using System;
namespace FeatureDemo.Model
{
    public class UserLoginResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<UserOptions> Preferences { get; set; }
    }
}
