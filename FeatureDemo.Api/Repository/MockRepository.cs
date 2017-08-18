using System;
using System.Collections.Generic;
using System.Linq;
using FeatureDemo.Api.Models;

namespace FeatureDemo.Api.Repository
{
    public class MockRepository : IRepository
    {

        List<Atm> Atms { get; set; }
        List<Item> Items { get; set; }
        List<User> Users { get; set; }
        List<Institution> Institutions { get; set; }

        public MockRepository()
        {
            Items = new List<Item>() 
            {
				new Item { Id = Guid.NewGuid(), Text = "First item", Description="This is a nice description"},
				new Item { Id = Guid.NewGuid(), Text = "Second item", Description="This is a nice description"},
				new Item { Id = Guid.NewGuid(), Text = "Third item", Description="This is a nice description"},
				new Item { Id = Guid.NewGuid(), Text = "Fourth item", Description="This is a nice description"},
				new Item { Id = Guid.NewGuid(), Text = "Fifth item", Description="This is a nice description"},
				new Item { Id = Guid.NewGuid(), Text = "Sixth item", Description="This is a nice description"}
            };

			Institutions = new List<Institution>()
			{
				new Institution { Id = Guid.NewGuid(), Name = "Northwest Community Credit Union"}
			};

            var nwcu = Institutions.FirstOrDefault();

            Atms = new List<Atm>() 
            {
                new Atm { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, IsVisible = true, Title = "Gateway St, Springfield",  Subtitle = "3660 Gateway St, Springfield, OR 97477", Position = new Position(44.0873705, -123.0449549), ShowCallout = true },
				new Atm { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, IsVisible = true, Title = "Main St, Springfield", Subtitle = "5000 Main St, Springfield, OR 97478", Position =new Position(44.0460806, -122.9441635), ShowCallout = true },
				new Atm { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, IsVisible = true, Title = "West 11th, Eugene", Subtitle = "3701 W 11th Ave, Eugene, OR 97402", Position = new Position(44.0484311, -123.1484119), ShowCallout = true },
				new Atm { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, IsVisible = true, Title = "East 8th, Eugene", Subtitle = "545 E 8th Ave, Eugene, OR 97401", Position = new Position(44.0512886, -123.0844738), ShowCallout = true },
				new Atm { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, IsVisible = true, Title = "Stephens St, Roseburg", Subtitle = "4221 NE Stephens St Suite 101, Roseburg, OR 97470", Position = new Position(43.2610622, -123.3510518), ShowCallout = true },
				new Atm { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, IsVisible = true, Title = "Molalla, Oregon City", Subtitle = "1689 Molalla Ave, Oregon City, OR 97045", Position = new Position(45.333237,-122.58797), ShowCallout = true }
            };

            Users = new List<User>() 
            {
                new User { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, FirstName = "Matt", LastName = "Smith", Email = "msith@gmail.com", PhoneNumber = "15419128541" },
                new User { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, FirstName = "Sally", LastName = "Johnson", Email = "sjohnson@msn.com", PhoneNumber = "15417446852" },
                new User { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, FirstName = "John", LastName = "Johnson", Email = "jjohnson@gmail.com", PhoneNumber = "15417446852" },
                new User { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, FirstName = "Sara", LastName = "Kline", Email = "skline@yahoo.com", PhoneNumber = "15419121234" },
                new User { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, FirstName = "Maria", LastName = "Ailbhe", Email = "mariaailbhe@gmail.com", PhoneNumber = "12062449754" },
                new User { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, FirstName = "Makara", LastName = "Ennis", Email = "mennis82@comcast.com", PhoneNumber = "15417628456" },
                new User { Id = Guid.NewGuid(), Institution = nwcu, InstitutionId = nwcu.Id, FirstName = "Suman", LastName = "Ash", Email = "sumanash@link.com", PhoneNumber = "15417227158" },
            };


        }

        public Atm AddAtm(Atm atm)
        {
            if (string.IsNullOrEmpty(atm.Id.ToString()) || Atms.Any(a => a.Id == atm.Id))
                atm.Id = Guid.NewGuid();

            Atms.Add(atm);
            return atm;
        }

        public Institution AddInstitution(Institution institution)
        {
            if (string.IsNullOrEmpty(institution.Id.ToString()) || Institutions.Any(a => a.Id == institution.Id))
                institution.Id = Guid.NewGuid();

            Institutions.Add(institution);
            return institution;
        }

        public Item AddItem(Item item)
        {
            if (string.IsNullOrEmpty(item.Id.ToString()) || Items.Any(i => i.Id == item.Id))
                item.Id = Guid.NewGuid();
            
            Items.Add(item);
            return item;
        }

        public User AddUser(User user)
        {
            if (string.IsNullOrEmpty(user.Id.ToString()) || Users.Any(u => u.Id == user.Id))
                user.Id = Guid.NewGuid();

            Users.Add(user);
            return user;
        }

        public bool DeleteAtm(Atm atm)
        {
            return Atms.Remove(atm);
        }

        public bool DeleteAtm(Guid id)
        {
            var atm = Atms.FirstOrDefault(a => a.Id == id);
            if(atm != null)
            {
                return Atms.Remove(atm);
            }
            return false;
        }

        public bool DeleteInstitution(Guid id)
        {
            var inst = Institutions.FirstOrDefault(i => i.Id == id);
            if(inst != null)
            {
                return Institutions.Remove(inst);
            }
            return false;
        }

        public bool DeleteItem(Item item)
        {
            return Items.Remove(item);
        }

        public bool DeleteItem(Guid id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if(item != null)
            {
                return Items.Remove(item);
            }
            return false;
        }

        public bool DeleteUser(User user)
        {
            return Users.Remove(user);
        }

        public bool DeleteUser(Guid id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if(user != null)
            {
                return Users.Remove(user);
            }
            return false;
        }

        public Atm GetAtm(Guid id)
        {
            return Atms.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Atm> GetAtms()
        {
            return Atms;
        }

        public IEnumerable<Atm> GetAtms(Guid institution)
        {
            return Atms.Where(a => a.InstitutionId == institution);
        }

        public Institution GetInstitution(Guid id)
        {
            return Institutions.FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<Institution> GetInstitutions()
        {
            return Institutions;
        }

        public Item GetItem(Guid id)
        {
            return Items.FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<Item> GetItems()
        {
            return Items;
        }

        public User GetUser(Guid id)
        {
            return Users.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<User> GetUsers()
        {
            return Users;
        }

        public IEnumerable<User> GetUsers(Guid institution)
        {
            return Users.Where(u => u.InstitutionId == institution);
        }

        public Atm UpdateAtm(Atm atm)
        {
            var idx = Atms.FindIndex(a => a.Id == atm.Id);
            if(idx >= 0)
            {
                return Atms[idx] = atm;
            }
            return null;
        }

        public Institution UpdateInstitution(Institution institution)
        {
            var idx = Institutions.FindIndex(i => i.Id == institution.Id);
            if(idx >= 0)
            {
                return Institutions[idx] = institution;
            }
            return null;
        }

        public Item UpdateItem(Item item)
        {
            var idx = Items.FindIndex(i => i.Id == item.Id);
            if(idx >= 0)
            {
                return Items[idx] = item;
            }
            return null;
        }

        public User UpdateUser(User user)
        {
            var idx = Users.FindIndex(u => u.Id == user.Id);
            if(idx >= 0)
            {
                return Users[idx] = user;
            }
            return null;
        }
    }
}
