using System;
using System.Collections.Generic;
using System.Linq;
using FeatureDemo.Api.Models;

namespace FeatureDemo.Api.Repository
{
    public class MockData
    {

        static List<Item> _items; public static List<Item> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<Item>()
                    {
                        new Item { Id = Guid.NewGuid(), Text = "First item", Description="This is a nice description"},
                        new Item { Id = Guid.NewGuid(), Text = "Second item", Description="This is a nice description"},
                        new Item { Id = Guid.NewGuid(), Text = "Third item", Description="This is a nice description"},
                        new Item { Id = Guid.NewGuid(), Text = "Fourth item", Description="This is a nice description"},
                        new Item { Id = Guid.NewGuid(), Text = "Fifth item", Description="This is a nice description"},
                        new Item { Id = Guid.NewGuid(), Text = "Sixth item", Description="This is a nice description"}
                    };
                }
                return _items;
            }
        }

        static List<Institution> _institutions; public static List<Institution> Institutions 
        {
            get
            {
                if (_institutions == null)
                {
                    _institutions = new List<Institution>()
                    {
                        new Institution { Id = Guid.NewGuid(), Code = "NWCU", Name = "Northwest Community Credit Union" },
                        new Institution { Id = Guid.NewGuid(), Code = "OCCU", Name = "Oregon Community Credit Union" }
                    };
                }
                return _institutions;
            }
        }

        static List<User> _users; public static List<User> Users 
        {
            get
            {
                var nwcu = Institutions.FirstOrDefault(i => i.Code == "NWCU");
                var occu = Institutions.FirstOrDefault(i => i.Code == "OCCU");
                if (_users == null)
                {
                    _users = new List<User>()
                    {
                        new User { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, FirstName = "Matt", LastName = "Smith", Email = "msith@gmail.com", PhoneNumber = "15419128541" },
                        new User { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, FirstName = "Sally", LastName = "Johnson", Email = "sjohnson@msn.com", PhoneNumber = "15417446852" },
                        new User { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, FirstName = "John", LastName = "Johnson", Email = "jjohnson@gmail.com", PhoneNumber = "15417446852" },
                        new User { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, FirstName = "Sara", LastName = "Kline", Email = "skline@yahoo.com", PhoneNumber = "15419121234" },
                        new User { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, FirstName = "Maria", LastName = "Ailbhe", Email = "mariaailbhe@gmail.com", PhoneNumber = "12062449754" },
                        new User { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, FirstName = "Makara", LastName = "Ennis", Email = "mennis82@comcast.com", PhoneNumber = "15417628456" },
                        new User { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, FirstName = "Suman", LastName = "Ash", Email = "sumanash@link.com", PhoneNumber = "15417227158" },

                        new User { Id = Guid.NewGuid(), Institution = occu, InstitutionId = occu.Id, FirstName = "Astor", LastName = "Nestrin", Email = "astornestrin@store.com", PhoneNumber = "12067289517" },
                        new User { Id = Guid.NewGuid(), Institution = occu, InstitutionId = occu.Id, FirstName = "Linton", LastName = "Hyacintha", Email = "lhyacintha@lane.edu", PhoneNumber = "15419128877" },
                        new User { Id = Guid.NewGuid(), Institution = occu, InstitutionId = occu.Id, FirstName = "Lavender", LastName = "Cyprianus", Email = "lcyprianus@uo.com", PhoneNumber = "15413658874" },
                        new User { Id = Guid.NewGuid(), Institution = occu, InstitutionId = occu.Id, FirstName = "Pratima", LastName = "Emanuel", Email = "pemanuel@gmail.com", PhoneNumber = "15418871258" },
                        new User { Id = Guid.NewGuid(), Institution = occu, InstitutionId = occu.Id, FirstName = "Medata", LastName = "Bulus", Email = "astornestrin@store.com", PhoneNumber = "1503287458" },
                        new User { Id = Guid.NewGuid(), Institution = occu, InstitutionId = occu.Id, FirstName = "Seong-Su", LastName = "Rasim", Email = "seongsu88@yahoo.com", PhoneNumber = "15038871249" },
                    };
                }
                return _users;
            }
        }

        static List<Atm> _atms; public static List<Atm> Atms 
        { 
            get
            {
				var nwcu = Institutions.FirstOrDefault(i => i.Code == "NWCU");
				var occu = Institutions.FirstOrDefault(i => i.Code == "OCCU");
                if (_atms == null)
                {
                    _atms = new List<Atm>()
                    {
                        new Atm { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, IsVisible = true, Title = "Gateway St, Springfield",  Subtitle = "3660 Gateway St, Springfield, OR 97477", Longitude = 44.0873705, Latitude = -123.0449549, ShowCallout = true },
                        new Atm { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, IsVisible = true, Title = "Main St, Springfield", Subtitle = "5000 Main St, Springfield, OR 97478", Longitude = 44.0460806, Latitude = -122.9441635, ShowCallout = true },
                        new Atm { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, IsVisible = true, Title = "West 11th, Eugene", Subtitle = "3701 W 11th Ave, Eugene, OR 97402", Longitude = 44.0484311, Latitude = -123.1484119, ShowCallout = true },
                        new Atm { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, IsVisible = true, Title = "East 8th, Eugene", Subtitle = "545 E 8th Ave, Eugene, OR 97401", Longitude = 44.0512886, Latitude = -123.0844738, ShowCallout = true },
                        new Atm { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, IsVisible = true, Title = "Stephens St, Roseburg", Subtitle = "4221 NE Stephens St Suite 101, Roseburg, OR 97470", Longitude = 43.2610622, Latitude = -123.3510518, ShowCallout = true },
                        new Atm { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, IsVisible = true, Title = "Molalla, Oregon City", Subtitle = "1689 Molalla Ave, Oregon City, OR 97045", Longitude = 45.333237, Latitude = -122.58797, ShowCallout = true },

                        new Atm { Id = Guid.NewGuid(), Institution = occu, InstitutionId = occu.Id, IsVisible = true, Title = "Barger Dr, Eugene", Subtitle = "4239 Barger Dr, Eugene, OR 97402", Longitude = 44.084602, Latitude = -123.17072, ShowCallout = true },
                        new Atm { Id = Guid.NewGuid(), Institution = occu, InstitutionId = occu.Id, IsVisible = true, Title = "Division Ave, Eugene", Subtitle = "45 Division Ave, Eugene, OR 97404", Longitude = 44.098965, Latitude = -123.12804, ShowCallout = true },
                        new Atm { Id = Guid.NewGuid(), Institution = occu, InstitutionId = occu.Id, IsVisible = true, Title = "West 11th, Eugene", Subtitle = "3065 W 11th Ave, Eugene, OR 97402", Longitude = 44.047955, Latitude = -123.13847, ShowCallout = true },
                        new Atm { Id = Guid.NewGuid(), Institution = occu, InstitutionId = occu.Id, IsVisible = true, Title = "Harlow Rd, Springfield", Subtitle = "925 Harlow Rd, Springfield, OR 97477", Longitude = 44.07084, Latitude = -123.04133, ShowCallout = true },
                        new Atm { Id = Guid.NewGuid(), Institution = occu, InstitutionId = occu.Id, IsVisible = true, Title = "Mohawk Blvd, Springfield", Subtitle = "1981 Mohawk Blvd, Springfield, OR 97477", Longitude = 44.063744, Latitude = -122.996635, ShowCallout = true },
                        new Atm { Id = Guid.NewGuid(), Institution = occu, InstitutionId = occu.Id, IsVisible = true, Title = "Willamette St, Eugene", Subtitle = "2890 Willamette St, Eugene, OR 97405", Longitude = 44.02643, Latitude = -123.09073, ShowCallout = true },
                    };
                }
                return _atms;
            }
        }
    }
}
