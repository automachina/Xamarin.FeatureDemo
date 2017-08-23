using System;
using System.Reflection;

namespace FeatureDemo.Api.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid InstitutionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Institution Institution { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is User)) return false;
            var user = obj as User;
            return Id == user.Id && InstitutionId == user.InstitutionId && FirstName == user.FirstName && LastName == user.LastName && Email == user.Email && PhoneNumber == user.PhoneNumber;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
