using Irp_Net462.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Irp_Net462.Controllers
{
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
			return View();
		}

		// POST: Payments/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "ID,CounterCurrent,CounterPrev,Amount,CurrentTarif,OrderDate,ReceiptDate,Order,Receipt,PaymentCategoryID")] Payment payment)
		{
			if (ModelState.IsValid)
			{
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
		public ActionResult Edit([Bind(Include = "ID,CounterCurrent,CounterPrev,Amount,CurrentTarif,OrderDate,ReceiptDate,Order,Receipt,PaymentCategoryID")] Payment payment)
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
