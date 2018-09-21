using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Irp_Net462.Models
{
	public class Payment
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		[Required]
		public int CounterCurrent { get; set; }

		[Required]
		public int CounterPrev { get; set; }

		[Required]
		public decimal Amount { get; set; }

		[Required]
		public decimal CurrentTarif { get; set; }

		[Required]
		public DateTime OrderDate { get; set; }

		//[Required]
		//public DateTime PaymentPeriod { get; set; }

		[Required]
		public DateTime ReceiptDate { get; set; }

		[Required]
		public string Order { get; set; }

		[Required]
		public string Receipt { get; set; }

		[ForeignKey(nameof(PaymentCategory))]
		public int PaymentCategoryID { get; set; }

		public PaymentCategory PaymentCategory { get; set; }
	}
}