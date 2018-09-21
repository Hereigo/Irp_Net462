using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Irp_Net462.Models
{
	public class DatabaseInitializer : CreateDatabaseIfNotExists<PaymentsContext>
	{
		protected override void Seed(PaymentsContext context)
		{
			if (!context.PaymentCategories.Any())
			{
				List<PaymentCategory> categories = new List<PaymentCategory>
				{
					new PaymentCategory{ Name="О.С.Б.Б."},
					new PaymentCategory{ Name="ОблЕнерго"},
					new PaymentCategory{ Name="Водоканал"}
				};
				context.PaymentCategories.AddRange(categories);
				context.SaveChanges();
			}
		}
	}
}