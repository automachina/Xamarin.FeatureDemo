﻿using System;
namespace FeatureDemo.Model.Client
{
    public class MenuItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Uri Icon { get; set; }
        public ActionType Action { get; set; }
        public IActionParameters Parameters { get; set; }
        public Uri Style { get; set; }
        public string Version { get; set; }
    }
}
