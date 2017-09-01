﻿using System;
namespace FeatureDemo.Model
{
    public class ProfileCreateResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<UserOption> Preferences { get; set; }
        public string Token { get; set; }
    }
}
