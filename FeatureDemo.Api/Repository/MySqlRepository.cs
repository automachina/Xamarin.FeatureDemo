using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FeatureDemo.Api.Context;
using FeatureDemo.Api.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FeatureDemo.Api.Repository
{
    public class MySqlRepository : IRepository
    {
        readonly FeatureContext db;
        public MySqlRepository(FeatureContext db)
        {
            this.db = db;
        }

		Guid? _institutionId; public Guid? InstitutionId
		{
			get => _institutionId;
			set => _institutionId = value;
		}

        public Atm AddAtm(Atm atm)
        {
            if (atm == null || (InstitutionId.HasValue && InstitutionId.Value != atm.InstitutionId)) return null;

            db.Atms.Add(atm);
            db.SaveChanges();
            return db.Atms.FirstOrDefault(a => a.Id == atm.Id);
        }

        public Institution AddInstitution(Institution institution)
        {
            if (institution == null) return null;

            db.Institutions.Add(institution);
            db.SaveChanges();
            return db.Institutions.FirstOrDefault(i => i.Id == institution.Id);
        }

        public Item AddItem(Item item)
        {
            if (item == null) return null;

            db.Items.Add(item);
            db.SaveChanges();
            return db.Items.FirstOrDefault(i => i.Id == item.Id);
        }

        public User AddUser(User user)
        {
            if (user == null || (InstitutionId.HasValue && InstitutionId.Value != user.InstitutionId)) return null;

            db.Users.Add(user);
            db.SaveChanges();
            return db.Users.FirstOrDefault(u => u.Id == user.Id);
        }

        public bool DeleteAtm(Atm atm)
        {
            if (atm == null || (InstitutionId.HasValue && InstitutionId.Value != atm.InstitutionId)) return false;

            try
            {
                if(db.Atms.Any(a => a.Id == atm.Id && ((InstitutionId.HasValue && InstitutionId.Value == a.InstitutionId) || !InstitutionId.HasValue)))
                {
                    db.Atms.Remove(atm);
                    db.SaveChanges();
                }
                return true;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error in DeleteAtm(Atm): {e.Message}");
                return false;
            }
        }

        public bool DeleteAtm(Guid id)
        {
			try
			{
				if (db.Atms.Any(a => a.Id == id && ((InstitutionId.HasValue && InstitutionId.Value == a.InstitutionId) || !InstitutionId.HasValue)))
				{
                    db.Atms.Remove(db.Atms.FirstOrDefault(a => a.Id == id));
					db.SaveChanges();
				}
				return true;
			}
			catch (Exception e)
			{
                Debug.WriteLine($"Error in DeleteAtm(Guid): {e.Message}");
				return false;
			}
        }

        public bool DeleteInstitution(Guid id)
        {
			try
			{
                if (db.Institutions.Any(a => a.Id == id))
				{
					db.Institutions.Remove(db.Institutions.FirstOrDefault(a => a.Id == id));
					db.SaveChanges();
				}
				return true;
			}
			catch (Exception e)
			{
				Debug.WriteLine($"Error in DeleteInstitution(Guid): {e.Message}");
				return false;
			}
        }

        public bool DeleteItem(Item item)
        {
            if (item == null) return false;

			try
			{
				if (db.Items.Any(a => a.Id == item.Id))
				{
					db.Items.Remove(db.Items.FirstOrDefault(a => a.Id == item.Id));
					db.SaveChanges();
				}
				return true;
			}
			catch (Exception e)
			{
				Debug.WriteLine($"Error in DeleteItem(Item): {e.Message}");
				return false;
			}
        }

        public bool DeleteItem(Guid id)
        {
			try
			{
				if (db.Items.Any(a => a.Id == id))
				{
					db.Items.Remove(db.Items.FirstOrDefault(a => a.Id == id));
					db.SaveChanges();
				}
				return true;
			}
			catch (Exception e)
			{
				Debug.WriteLine($"Error in DeleteItem(Guid): {e.Message}");
				return false;
			}
        }

        public bool DeleteUser(User user)
        {
            if (user == null || (InstitutionId.HasValue && InstitutionId.Value != user.InstitutionId)) return false;

			try
			{
				if (db.Users.Any(a => a.Id == user.Id && ((InstitutionId.HasValue && InstitutionId.Value == a.InstitutionId) || !InstitutionId.HasValue)))
				{
					db.Users.Remove(db.Users.FirstOrDefault(a => a.Id == user.Id));
					db.SaveChanges();
				}
				return true;
			}
			catch (Exception e)
			{
				Debug.WriteLine($"Error in DeleteUser(User): {e.Message}");
				return false;
			}
        }

        public bool DeleteUser(Guid id)
        {
			try
			{
				if (db.Users.Any(a => a.Id == id && ((InstitutionId.HasValue && InstitutionId.Value == a.InstitutionId) || !InstitutionId.HasValue)))
				{
					db.Users.Remove(db.Users.FirstOrDefault(a => a.Id == id));
					db.SaveChanges();
				}
				return true;
			}
			catch (Exception e)
			{
				Debug.WriteLine($"Error in DeleteUser(Guid): {e.Message}");
				return false;
			}
        }

        public Atm GetAtm(Guid id)
        {
            return db.Atms.FirstOrDefault(a => a.Id == id && ((InstitutionId.HasValue && InstitutionId.Value == a.InstitutionId) || !InstitutionId.HasValue));
        }

        public IEnumerable<Atm> GetAtms()
        {
            return InstitutionId.HasValue ? GetAtms(InstitutionId.Value) : db.Atms;
        }

        public IEnumerable<Atm> GetAtms(Guid institution)
        {
            return db.Atms.Where(a => a.InstitutionId == institution);
        }

        public Institution GetInstitution(Guid id)
        {
            return db.Institutions.FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<Institution> GetInstitutions()
        {
            return db.Institutions;
        }

        public Item GetItem(Guid id)
        {
            return db.Items.FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<Item> GetItems()
        {
            return db.Items;
        }

        public User GetUser(Guid id)
        {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<User> GetUsers()
        {
            return InstitutionId.HasValue ? GetUsers(InstitutionId.Value) : db.Users;
        }

        public IEnumerable<User> GetUsers(Guid institution)
        {
            return db.Users.Where(u => u.InstitutionId == institution);
        }

        public Atm UpdateAtm(Atm atm)
        {
            if (atm == null || (InstitutionId.HasValue && InstitutionId.Value != atm.InstitutionId)) return null;

            if(db.Atms.Any(a => a.Id == atm.Id && ((InstitutionId.HasValue && InstitutionId.Value != a.InstitutionId) || !InstitutionId.HasValue)))
            {
                db.Atms.Update(atm);
                db.SaveChanges();
                return db.Atms.FirstOrDefault(a => a.Id == atm.Id);
            }
            return null;
        }

        public Institution UpdateInstitution(Institution institution)
        {
            if (institution == null) return null;

            if (db.Institutions.Any(a => a.Id == institution.Id))
			{
                db.Institutions.Update(institution);
				db.SaveChanges();
				return db.Institutions.FirstOrDefault(a => a.Id == institution.Id);
			}
			return null;
        }

        public Item UpdateItem(Item item)
        {
            if (item == null) return null;

            if (db.Items.Any(a => a.Id == item.Id))
			{
				db.Items.Update(item);
				db.SaveChanges();
				return db.Items.FirstOrDefault(a => a.Id == item.Id);
			}
			return null;
        }

        public User UpdateUser(User user)
        {
            if (user == null || (InstitutionId.HasValue && InstitutionId.Value != user.InstitutionId)) return null;

			if (db.Users.Any(a => a.Id == user.Id && ((InstitutionId.HasValue && InstitutionId.Value != a.InstitutionId) || !InstitutionId.HasValue)))
			{
				db.Users.Update(user);
				db.SaveChanges();
				return db.Users.FirstOrDefault(a => a.Id == user.Id);
			}
			return null;
        }
    }
}
