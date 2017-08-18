using System;
namespace FeatureDemo.Api.Models
{
	public class Atm
	{
        public Guid Id { get; set; }
        public Guid InstitutionId { get; set; }
        public bool IsVisible { get; set; }
		public string Title { get; set; }
		public string Subtitle { get; set; }
		public Position Position { get; set; }
		public bool ShowCallout { get; set; }

        public virtual Institution Institution { get; set; }
	}
}
