using System;
using Prism.Mvvm;
using Xamarin.Forms.Maps;

namespace FeatureDemo.Core.Models
{
    public class Atm : BindableBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public Pin Location { get; set; }
    }
}
