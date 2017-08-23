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

        Guid? _institutionId; public Guid? InstitutionId 
        {
            get => _institutionId;
            set => _institutionId = value;
        }

        public MockRepository()
        {
            Items = MockData.Items;

            Institutions = MockData.Institutions;

            Atms = MockData.Atms;

            Users = MockData.Users;
        }

        public Atm AddAtm(Atm atm)
        {
            if (InstitutionId.HasValue && InstitutionId.Value != atm.InstitutionId)
                return null;

            atm.Id = Guid.NewGuid();
            Atms.Add(atm);
            return Atms.FirstOrDefault(a => a.Id == atm.Id);
        }

        public Institution AddInstitution(Institution institution)
        {
            if (InstitutionId.HasValue) return null;

            institution.Id = Guid.NewGuid();
            Institutions.Add(institution);
            return institution;
        }

        public Item AddItem(Item item)
        {
            item.Id = Guid.NewGuid();
            Items.Add(item);
            return item;
        }

        public User AddUser(User user)
        {
            if (InstitutionId.HasValue && InstitutionId.Value != user.InstitutionId) return null;

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
            if (item == null) return false;
            return Items.Remove(item);
        }

        public bool DeleteUser(User user)
        {
            return Users.Remove(user);
        }

        public bool DeleteUser(Guid id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return false;
            return Users.Remove(user);
        }

        public Atm GetAtm(Guid id)
        {
            return Atms.FirstOrDefault(a => a.Id == id && ((InstitutionId.HasValue && InstitutionId.Value == a.InstitutionId) || (!InstitutionId.HasValue)));
        }

        public IEnumerable<Atm> GetAtms()
        {
            return InstitutionId.HasValue ? GetAtms(InstitutionId.Value) : Atms;
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
            return InstitutionId.HasValue ? GetUsers(InstitutionId.Value) : Users;
        }

        public IEnumerable<User> GetUsers(Guid institution)
        {
            return Users.Where(u => u.InstitutionId == institution);
        }

        public Atm UpdateAtm(Atm atm)
        {

            if (InstitutionId.HasValue && InstitutionId.Value != atm.InstitutionId) return null;

            var idx = InstitutionId.HasValue ? Atms.FindIndex(a => a.Id == atm.Id && InstitutionId.Value == a.InstitutionId) : Atms.FindIndex(a => a.Id == atm.Id);
            if (idx < 0) return null;
            return Atms[idx] = atm;
        }

        public Institution UpdateInstitution(Institution institution)
        {
            var idx = Institutions.FindIndex(i => i.Id == institution.Id);
            if (idx < 0) return null;
            return Institutions[idx] = institution;
        }

        public Item UpdateItem(Item item)
        {
            var idx = Items.FindIndex(i => i.Id == item.Id);
            if (idx < 0) return null;
            return Items[idx] = item;
        }

        public User UpdateUser(User user)
        {
            if (InstitutionId.HasValue && InstitutionId.Value != user.InstitutionId) return null;

            var idx = InstitutionId.HasValue ? Users.FindIndex(u => u.Id == user.Id && InstitutionId.Value == user.InstitutionId) : Users.FindIndex(u => u.Id == user.Id);
            if (idx < 0) return null;
            return Users[idx] = user;
        }
    }
}
