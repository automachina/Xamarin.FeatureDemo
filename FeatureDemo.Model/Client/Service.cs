﻿using System;
namespace FeatureDemo.Model.Client
{
    public class Service
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Uri Icon { get; set; }
        public string HtmlSource { get; set; }
        public ActionType Action { get; set; }
        public string Parameters { get; set; }
        public string Version { get; set; }
    }
}
