using System;
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
    }
}
