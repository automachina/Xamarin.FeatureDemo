using System;
using System.Linq;
using FeatureDemo.Api.Repository;

namespace FeatureDemo.Api.Context
{
    public static class DbInitialize
    {
        public static void Initialize(FeatureContext context)
        {
            context.Database.EnsureCreated();

            if(context.Items.Any())
            {
                context.Items.RemoveRange(context.Items.ToArray());
                context.SaveChanges();
            }

            MockData.Items.ForEach(i => context.Items.Add(i));

            context.SaveChanges();

			if (context.Institutions.Any())
			{
				context.Institutions.RemoveRange(context.Institutions.ToArray());
				context.SaveChanges();
			}

			MockData.Institutions.ForEach(i => context.Institutions.Add(i));

			context.SaveChanges();

			if(context.Atms.Any())
            {
                context.Atms.RemoveRange(context.Atms.ToArray());
                context.SaveChanges();
            }

            MockData.Atms.ForEach(a => context.Add(a));

            context.SaveChanges();

            if(context.Users.Any())
            {
                context.Users.RemoveRange(context.Users.ToArray());
                context.SaveChanges();
            }

            MockData.Users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();
        }
    }
}
