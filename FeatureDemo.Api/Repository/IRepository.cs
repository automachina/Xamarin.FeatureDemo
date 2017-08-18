using System;
using System.Collections.Generic;
using FeatureDemo.Api.Models;

namespace FeatureDemo.Api.Repository
{
    public interface IRepository
    {
        //Atm Api
        IEnumerable<Atm> GetAtms();
        IEnumerable<Atm> GetAtms(Guid institution);
        Atm GetAtm(Guid id);
        Atm AddAtm(Atm atm);
        Atm UpdateAtm(Atm atm);
        bool DeleteAtm(Atm atm);
        bool DeleteAtm(Guid id);

        //User Api
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsers(Guid institution);
        User GetUser(Guid id);
        User AddUser(User user);
        User UpdateUser(User user);
        bool DeleteUser(User user);
        bool DeleteUser(Guid id);

        //Institution Api
        IEnumerable<Institution> GetInstitutions();
        Institution GetInstitution(Guid id);
        Institution AddInstitution(Institution institution);
        Institution UpdateInstitution(Institution institution);
        bool DeleteInstitution(Guid id);

        //Item Api
        IEnumerable<Item> GetItems();
        Item GetItem(Guid id);
        Item AddItem(Item item);
        Item UpdateItem(Item item);
        bool DeleteItem(Item item);
        bool DeleteItem(Guid id);
    }
}
