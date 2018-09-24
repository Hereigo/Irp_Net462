using Irp_Net462.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Irp_Net462.Controllers
{
	[Authorize]
	public class PaymentsController : Controller
	{
		private PaymentsContext db = new PaymentsContext();

		// GET: Payments
		public ActionResult Index()
		{
			IQueryable<Payment> payments = db.Payments.Include(p => p.PaymentCategory);
			return View(payments.ToList());
		}

		// GET: Payments/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Payment payment = db.Payments.Find(id);
			if (payment == null)
			{
				return HttpNotFound();
			}
			return View(payment);
		}

		// GET: Payments/Create
		public ActionResult Create()
		{
			ViewBag.PaymentCategoryID = new SelectList(db.PaymentCategories, "ID", "Name");

			Payment newPayment = new Payment
			{
				Amount = 0.01F,
				CounterCurrent = 0,
				CounterPrev = 0,
				CurrentTarif = 0.01F,
				Order = "none",
				OrderDate = DateTime.Now,
				PaymentPeriod = DateTime.Now,
				Receipt = "none",
				ReceiptDate = DateTime.Now,
			};

			return View(newPayment);
		}

		// POST: Payments/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(HttpPostedFileBase orderFile, HttpPostedFileBase receiptFile, [Bind(Include = "ID,CounterCurrent,CounterPrev,Amount,CurrentTarif,OrderDate,ReceiptDate,PaymentPeriod,Order,Receipt,PaymentCategoryID")] Payment payment)
		{
			if (ModelState.IsValid)
			{
				string category = db.PaymentCategories.Single(c => c.ID == payment.PaymentCategoryID).Name.ToString();

				string catAndDate = $"{category}_{DateTime.Now.ToString("yyMMdd_HHmm")}";

				if (orderFile.ContentLength > 0)
				{
					string orderFileName = $"{catAndDate}_O.{Path.GetExtension(orderFile.FileName)}";

					string path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), orderFileName);

					orderFile.SaveAs(path);

					payment.Order = orderFileName;
				}
				if (receiptFile.ContentLength > 0)
				{
					string receiptFileName = $"{catAndDate}_R.{Path.GetExtension(receiptFile.FileName)}";

					string path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), receiptFileName);

					receiptFile.SaveAs(path);

					payment.Receipt = receiptFileName;
				}

				db.Payments.Add(payment);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.PaymentCategoryID = new SelectList(db.PaymentCategories, "ID", "Name", payment.PaymentCategoryID);
			return View(payment);
		}

		// GET: Payments/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Payment payment = db.Payments.Find(id);
			if (payment == null)
			{
				return HttpNotFound();
			}
			ViewBag.PaymentCategoryID = new SelectList(db.PaymentCategories, "ID", "Name", payment.PaymentCategoryID);
			return View(payment);
		}

		// POST: Payments/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "ID,CounterCurrent,CounterPrev,Amount,CurrentTarif,OrderDate,ReceiptDate,PaymentPeriod,Order,Receipt,PaymentCategoryID")] Payment payment)
		{
			if (ModelState.IsValid)
			{
				db.Entry(payment).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.PaymentCategoryID = new SelectList(db.PaymentCategories, "ID", "Name", payment.PaymentCategoryID);
			return View(payment);
		}

		// GET: Payments/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Payment payment = db.Payments.Find(id);
			if (payment == null)
			{
				return HttpNotFound();
			}
			return View(payment);
		}

		// POST: Payments/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Payment payment = db.Payments.Find(id);
			db.Payments.Remove(payment);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
