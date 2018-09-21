using System.Data.Entity;

namespace Irp_Net462.Models
{
	public class PaymentsContext : DbContext
	{
		// You can add custom code to this file. Changes will not be overwritten.
		// 
		// If you want Entity Framework to drop and regenerate your database
		// automatically whenever you change your model schema, please use data migrations.
		// For more information refer to the documentation:
		// http://msdn.microsoft.com/en-us/data/jj591621.aspx

		public PaymentsContext() : base("name=PaymentsContext")
		{
		}

		public DbSet<Irp_Net462.Models.PaymentCategory> PaymentCategories { get; set; }

		public DbSet<Irp_Net462.Models.Payment> Payments { get; set; }

	}
}
