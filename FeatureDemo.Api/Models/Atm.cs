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

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Atm)) return false;
            var atm = obj as Atm;
            return Id == atm.Id && InstitutionId == atm.InstitutionId && IsVisible == atm.IsVisible && Title == atm.Title && Subtitle == atm.Subtitle && ShowCallout == atm.ShowCallout && Latitude.Equals(atm.Latitude) && Longitude.Equals(atm.Longitude);
                
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
	}
}
