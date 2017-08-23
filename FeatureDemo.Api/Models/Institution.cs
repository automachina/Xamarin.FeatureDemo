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

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Institution)) return false;
            var inst = obj as Institution;
            return Id == inst.Id && Code == inst.Code && Name == inst.Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
