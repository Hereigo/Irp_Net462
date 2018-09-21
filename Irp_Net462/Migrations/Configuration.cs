namespace Irp_Net462.Migrations
{
	using Irp_Net462.Models;
	using System.Collections.Generic;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<Irp_Net462.Models.PaymentsContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
		}

		protected override void Seed(Irp_Net462.Models.PaymentsContext context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data.
			if (!context.PaymentCategories.Any())
			{
				List<PaymentCategory> categories = new List<PaymentCategory>
				{
					new PaymentCategory{ Name="О.С.Б.Б."},
					new PaymentCategory{ Name="КиївОблЕнерго"},
					new PaymentCategory{ Name="ІрпiньВодоканал"}
				};

				context.PaymentCategories.AddRange(categories);
				context.SaveChanges();
			}
		}
	}
}