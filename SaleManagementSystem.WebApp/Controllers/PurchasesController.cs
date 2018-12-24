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
    public class PurchasesController : Controller
    {
        private SaleManagementSystemEntities db = new SaleManagementSystemEntities();

        // GET: Purchases
        public ActionResult Index(Guid? SupplierId, string minPrice, string maxPrice, string sortOrder)
        {
            var purchases = db.Purchases.Include(p => p.Supplier).Where(p => !p.Supplier.IsDeleted && !p.IsDeleted);
            ViewBag.SupplierId = new SelectList(db.Suppliers.Where(p => !p.IsDeleted), "Id", "Name");

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";

            ViewBag.Supplier = SupplierId;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;

            switch (sortOrder)
            {
                case "name_desc":
                    purchases = purchases.OrderByDescending(s => s.Supplier.Name);
                    break;
                case "Price":
                    purchases = purchases.OrderBy(s => s.Price);
                    break;
                case "Price_desc":
                    purchases = purchases.OrderByDescending(s => s.Price);
                    break;
                case "Date":
                    purchases = purchases.OrderBy(s => s.PurchaseDate);
                    break;
                case "Date_desc":
                    purchases = purchases.OrderByDescending(s => s.PurchaseDate);
                    break;
                default:
                    purchases = purchases.OrderBy(s => s.Supplier.Name);
                    break;
            }

            if (SupplierId != null)
            {
                purchases = from p in purchases where p.SupplierId == SupplierId select p;
            }
            if (!String.IsNullOrEmpty(minPrice))
            {
                Decimal minP = Convert.ToDecimal(minPrice);
                purchases = from p in purchases where p.Price >= minP select p;
            }
            if (!String.IsNullOrEmpty(maxPrice))
            {
                Decimal maxP = Convert.ToDecimal(maxPrice);
                purchases = from p in purchases where p.Price <= maxP select p;
            }

            return View(purchases.ToList());
        }

        // GET: Purchases/Create
        public ActionResult Create()
        {
            ViewBag.SupplierId = new SelectList(db.Suppliers.Where(p => !p.IsDeleted), "Id", "Name");
            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SupplierId,Description,Price,PurchaseDate,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] Purchase purchase)
        {
            AuditTable.InsertAuditFields(purchase);
            if (ModelState.IsValid)
            {
                purchase.Id = Guid.NewGuid();
                db.Purchases.Add(purchase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SupplierId = new SelectList(db.Suppliers.Where(p => !p.IsDeleted), "Id", "Name", purchase.SupplierId);
            return View(purchase);
        }

        // GET: Purchases/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierId = new SelectList(db.Suppliers.Where(p => !p.IsDeleted), "Id", "Name", purchase.SupplierId);
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SupplierId,Description,Price,PurchaseDate,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] Purchase purchase)
        {
            AuditTable.UpdateAuditFields(purchase);
            if (ModelState.IsValid)
            {
                db.Entry(purchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierId = new SelectList(db.Suppliers.Where(p => !p.IsDeleted), "Id", "Name", purchase.SupplierId);
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Purchase purchase = db.Purchases.Find(id);
            //db.Purchases.Remove(purchase);
            purchase.IsDeleted = true;
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
