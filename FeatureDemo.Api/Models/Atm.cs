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
        public bool ShowCallout { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		
        public virtual Institution Institution { get; set; }

	}
}
