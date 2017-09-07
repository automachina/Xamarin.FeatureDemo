using System;
using System.Reflection;

namespace FeatureDemo.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid InstitutionId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Institution Institution { get; set; }

    }
}
