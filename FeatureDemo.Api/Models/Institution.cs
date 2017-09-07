using System;
using System.Collections.Generic;

namespace FeatureDemo.Api.Models
{
    public class Institution
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<User> Users { get; set; }
        public virtual IEnumerable<Atm> Atms { get; set; }
    }
}
