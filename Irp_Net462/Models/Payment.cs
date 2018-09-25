using System;
using System.ComponentModel;
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
		[DisplayName("Лічильник поточний")]
		public int CounterCurrent { get; set; }

		[Required]
		[DisplayName("Лічильник попередній")]
		public int CounterPrev { get; set; }

		[Required]
		[DisplayName("Сума")]
		[DataType(DataType.Currency, ErrorMessage = "Format is 0.01")]
		[DisplayFormat(DataFormatString = "{0:C}")] //, ApplyFormatInEditMode = true)]
		public float Amount { get; set; }

		[Required]
		[DisplayName("Тариф")]
		[DataType(DataType.Currency, ErrorMessage = "Format is 0.01")]
		[DisplayFormat(DataFormatString = "{0:C}")] //, ApplyFormatInEditMode = true)]
		public float CurrentTarif { get; set; }

		[Required]
		[DisplayName("Рахунок")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime OrderDate { get; set; }

		[Required]
		[DisplayName("Оплата")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime ReceiptDate { get; set; }

		[Required]
		[DisplayName("Період оплати")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime PaymentPeriod { get; set; }

		[Required]
		[DisplayName("Рахунок")]
		public string Order { get; set; }

		[Required]
		[DisplayName("Оплата")]
		public string Receipt { get; set; }

		[ForeignKey(nameof(PaymentCategory))]
		public int PaymentCategoryID { get; set; }

		[DisplayName("КАТЕГОРІЯ")]
		public PaymentCategory PaymentCategory { get; set; }
	}
}