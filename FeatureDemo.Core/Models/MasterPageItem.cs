using System;
namespace FeatureDemo.Core.Models
{
	public class MasterPageItem
	{
		public string Title { get; set; }

		public string IconSource { get; set; }

        public string TargetUri { get; set; }

        public MasterPageItem(){}

        public MasterPageItem(string title, string iconSource, string targetUri){
            Title = title;
            IconSource = iconSource;
            TargetUri = targetUri;
        }

    }
}
