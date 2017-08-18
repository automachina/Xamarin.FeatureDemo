using System;
using System.Collections.Generic;
using FeatureDemo.Api.Context;
using FeatureDemo.Api.Models;

namespace FeatureDemo.Api.Repository
{
    public class MySqlRepository : IRepository
    {
        IDbContext db;
        public MySqlRepository(IDbContext db)
        {
            this.db = db;
        }

        public Atm AddAtm(Atm atm)
        {
            throw new NotImplementedException();
        }

        public Institution AddInstitution(Institution institution)
        {
            throw new NotImplementedException();
        }

        public Item AddItem(Item item)
        {
            throw new NotImplementedException();
        }

        public User AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAtm(Atm atm)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAtm(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteInstitution(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteItem(Item item)
        {
            throw new NotImplementedException();
        }

        public bool DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public Atm GetAtm(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Atm> GetAtms()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Atm> GetAtms(Guid institution)
        {
            throw new NotImplementedException();
        }

        public Institution GetInstitution(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Institution> GetInstitutions()
        {
            throw new NotImplementedException();
        }

        public Item GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetItems()
        {
            throw new NotImplementedException();
        }

        public User GetUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers(Guid institution)
        {
            throw new NotImplementedException();
        }

        public Atm UpdateAtm(Atm atm)
        {
            throw new NotImplementedException();
        }

        public Institution UpdateInstitution(Institution institution)
        {
            throw new NotImplementedException();
        }

        public Item UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }

        public User UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
