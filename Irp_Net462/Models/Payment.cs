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
		[DataType(DataType.Currency, ErrorMessage = "Format is 0.01")]
		[DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
		public float Amount { get; set; }

		[Required]
		[DataType(DataType.Currency, ErrorMessage = "Format is 0.01")]
		[DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
		public float CurrentTarif { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime OrderDate { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime ReceiptDate { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime PaymentPeriod { get; set; }

		[Required]
		public string Order { get; set; }

		[Required]
		public string Receipt { get; set; }

		[ForeignKey(nameof(PaymentCategory))]
		public int PaymentCategoryID { get; set; }

		public PaymentCategory PaymentCategory { get; set; }
	}
}