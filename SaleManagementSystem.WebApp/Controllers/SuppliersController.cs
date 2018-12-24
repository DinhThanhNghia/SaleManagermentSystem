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
    public class SuppliersController : Controller
    {
        private SaleManagementSystemEntities db = new SaleManagementSystemEntities();

        // GET: Suppliers
        public ActionResult Index(string name, string contact, string date, string sortOrder)
        {
            var suppliers = db.Suppliers.Where(s => !s.IsDeleted);

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Provided_Date" ? "Provided_Date_desc" : "Provided_Date";
            ViewBag.RatingSortParm = sortOrder == "Rating" ? "Rating_desc" : "Rating";

            ViewBag.Name = name;
            ViewBag.Contact = contact;
            ViewBag.Date = date;

            switch (sortOrder)
            {
                case "name_desc":
                    suppliers = suppliers.OrderByDescending(s => s.Name);
                    break;
                case "Provided_Date":
                    suppliers = suppliers.OrderBy(s => s.ProvideDate);
                    break;
                case "Provided_Date_desc":
                    suppliers = suppliers.OrderByDescending(s => s.ProvideDate);
                    break;
                case "Rating":
                    suppliers = suppliers.OrderBy(s => s.Rating);
                    break;
                case "Rating_desc":
                    suppliers = suppliers.OrderByDescending(s => s.Rating);
                    break;
                default:
                    suppliers = suppliers.OrderBy(s => s.Name);
                    break;
            }

            var result = new List<Supplier>();

            if (!string.IsNullOrEmpty(name))
            {
                string searchString = StringHelpers.ConvertToUnSign(name).ToLower();
                result = suppliers.ToList().FindAll(
                    delegate (Supplier user)
                    {
                        if (StringHelpers.ConvertToUnSign(user.Name.ToLower()).Contains(searchString))
                            return true;
                        else
                            return false;
                    }
                );
            }
            else
            {
                result = suppliers.ToList();
            }
            if (!String.IsNullOrEmpty(contact))
            {
                result = result.Where(s => s.ContactNo.Contains(contact)).ToList();
            }
            if (!String.IsNullOrEmpty(date))
            {
                DateTime DT = Convert.ToDateTime(date);
                result = result.Where(s => s.ProvideDate == DT).ToList();
            }
            return View(result);
        }
        //POST: Check Duplicate return Json
        public JsonResult CheckName(string Name, Guid? Id)
        {
            return Json(!db.Suppliers.Any(x => x.Name == Name && x.Id != Id && !x.IsDeleted), JsonRequestBehavior.AllowGet);
        }
        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Rating,ContactNo,ProvideDate,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] Supplier supplier)
        {
            AuditTable.InsertAuditFields(supplier);
            if (ModelState.IsValid)
            {
                supplier.Id = Guid.NewGuid();
                db.Suppliers.Add(supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Rating,ContactNo,ProvideDate,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] Supplier supplier)
        {
            AuditTable.UpdateAuditFields(supplier);
            var entity = db.Suppliers.Where(p => p.Id != supplier.Id && p.Name == supplier.Name && !p.IsDeleted);
            if (entity != null && entity.Count() > 0)
            {
                TempData["msg"] = "<script>alert('This name is already exist in system, please enter another name');</script>";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(Guid? id)
        {
            var entityExist1 = db.Purchases.Where(s => s.SupplierId == id && !s.IsDeleted).FirstOrDefault();
            var entityExist2 = db.PickUps.Where(s => s.SupplierId == id && !s.IsDeleted).FirstOrDefault();
            if (entityExist1 != null || entityExist2 != null)
            {
                return PartialView("_Delete");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            //db.Suppliers.Remove(supplier);
            supplier.IsDeleted = true;
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
