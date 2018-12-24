using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SaleManagementSystem.Common;
using SaleManagementSystem.Model.DAL;

namespace SaleManagementSystem.WebApp.Controllers
{
    public class DeliveriesController : Controller
    {
        private SaleManagementSystemEntities db = new SaleManagementSystemEntities();

        // GET: Deliveries
        public ActionResult Index(Guid? saleId, string fromdate, string todate, string max, string min, string sortOrder)
        {
            var deliveries = db.Deliveries.Include(d => d.Sale).Where(s => !s.IsDeleted);
            ViewBag.SaleId = new SelectList(db.Sales.Where(s => !s.IsDeleted), "Id", "SaleCode");

            ViewBag.SaleCodeSortParm = String.IsNullOrEmpty(sortOrder) ? "SaleCode_desc" : "";
            ViewBag.CustomerSortParm = sortOrder == "Customer" ? "Customer_desc" : "Customer";
            ViewBag.EmployeeSortParm = sortOrder == "Employee" ? "Employee_desc" : "Employee";
            ViewBag.QuantitySortParm = sortOrder == "Quantity" ? "Quantity_desc" : "Quantity";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";

            ViewBag.Sale = saleId;
            ViewBag.FromDate = fromdate;
            ViewBag.ToDate = todate;
            ViewBag.Min = min;
            ViewBag.Max = max;

            switch (sortOrder)
            {
                case "SaleCode_desc":
                    deliveries = deliveries.OrderByDescending(s => s.Sale.SaleCode);
                    break;
                case "Customer":
                    deliveries = deliveries.OrderBy(s => s.Sale.Customer.Name);
                    break;
                case "Customer_desc":
                    deliveries = deliveries.OrderByDescending(s => s.Sale.Customer.Name);
                    break;
                case "Employee":
                    deliveries = deliveries.OrderBy(s => s.Sale.Employee.Name);
                    break;
                case "Employee_desc":
                    deliveries = deliveries.OrderByDescending(s => s.Sale.Employee.Name);
                    break;
                case "Quantity":
                    deliveries = deliveries.OrderBy(s => s.Quantity);
                    break;
                case "Quantity_desc":
                    deliveries = deliveries.OrderByDescending(s => s.Quantity);
                    break;
                case "Date":
                    deliveries = deliveries.OrderBy(s => s.DeliveriedDate);
                    break;
                case "Date_desc":
                    deliveries = deliveries.OrderByDescending(s => s.DeliveriedDate);
                    break;
                default:
                    deliveries = deliveries.OrderBy(s => s.Sale.SaleCode);
                    break;
            }

            if (!String.IsNullOrEmpty(max))
            {
                int Max = Convert.ToInt32(max);
                deliveries = deliveries.Where(s => s.Quantity <= Max);
            }
            if (!String.IsNullOrEmpty(min))
            {
                int Min = Convert.ToInt32(min);
                deliveries = deliveries.Where(s => s.Quantity >= Min);
            }
            if (!String.IsNullOrEmpty(fromdate))
            {
                DateTime FD = Convert.ToDateTime(fromdate);
                deliveries = deliveries.Where(s => s.DeliveriedDate >= FD);
            }
            if (!String.IsNullOrEmpty(todate))
            {
                DateTime TD = Convert.ToDateTime(todate);
                deliveries = deliveries.Where(s => s.DeliveriedDate <= TD);
            }
            if (saleId != null)
            {
                deliveries = deliveries.Where(s => s.SaleId == saleId);
            }
            return View(deliveries.ToList());
        }
        // GET: Deliveries/Create
        public ActionResult Create()
        {
            ViewBag.SaleId = new SelectList(db.Sales.Where(s => !s.IsDeleted), "Id", "SaleCode");
            return View();
        }

        // POST: Deliveries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SaleId,Quantity,DeliveriedDate,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] Delivery delivery)
        {
            AuditTable.InsertAuditFields(delivery);
            if (ModelState.IsValid)
            {
                delivery.Id = Guid.NewGuid();
                db.Deliveries.Add(delivery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SaleId = new SelectList(db.Sales.Where(s => !s.IsDeleted), "Id", "SaleCode", delivery.SaleId);
            return View(delivery);
        }

        // GET: Deliveries/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery delivery = db.Deliveries.Find(id);
            if (delivery == null)
            {
                return HttpNotFound();
            }
            ViewBag.SaleId = new SelectList(db.Sales.Where(s => !s.IsDeleted), "Id", "SaleCode", delivery.SaleId);
            return View(delivery);
        }

        // POST: Deliveries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SaleId,Quantity,DeliveriedDate,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] Delivery delivery)
        {
            AuditTable.UpdateAuditFields(delivery);
            if (ModelState.IsValid)
            {
                db.Entry(delivery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SaleId = new SelectList(db.Sales.Where(s => !s.IsDeleted), "Id", "SaleCode", delivery.SaleId);
            return View(delivery);
        }

        // GET: Deliveries/Delete/5
        public ActionResult Delete(Guid? id)
        {
            var entityExist = db.Sales.Where(s => s.CustomerId == id && !s.IsDeleted).FirstOrDefault();
            if (entityExist != null)
            {
                return PartialView("_Delete");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery delivery = db.Deliveries.Find(id);
            if (delivery == null)
            {
                return HttpNotFound();
            }
            return View(delivery);
        }

        // POST: Deliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Delivery delivery = db.Deliveries.Find(id);
            delivery.IsDeleted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult PopulateItem(string id)
        {
            var sale_Customer = db.Sales
                .Select(x => new { Id = x.Id.ToString(), x.Customer.Name })
                .FirstOrDefault(x => x.Id.ToString() == id);
            var sale_Employee = db.Sales
                .Select(x => new { Id = x.Id.ToString(), x.Employee.Name })
                .FirstOrDefault(x => x.Id.ToString() == id);

            return Json(new { data = sale_Customer, data1 = sale_Employee });
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
