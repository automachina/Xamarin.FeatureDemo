using System;
using FeatureDemo.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FeatureDemo.Api.Context
{
    public interface IDbContext
    {
        DbSet<Item> Items { get; set; }
        DbSet<Atm> Atms { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Institution> Institutions { get; set; }
    }
}
