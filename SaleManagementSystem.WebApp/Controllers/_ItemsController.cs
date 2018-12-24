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
    public class _ItemsController : Controller
    {
        private SaleManagementSystemEntities db = new SaleManagementSystemEntities();

        // GET: _Items
        public ActionResult Index(string searchName, string searchMinAmount, string searchMaxAmount, string searchMinDate, string searchMaxDate, string sortOrder)
        {
            var items = db.Items.Where(i => !i.IsDeleted);

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Expired_Date" ? "Expired_Date_desc" : "Expired_Date";
            ViewBag.AmountSortParm = sortOrder == "Amount" ? "Amount_desc" : "Amount";

            ViewBag.Name = searchName;
            ViewBag.MinAmount = searchMinAmount;
            ViewBag.MaxAmount = searchMaxAmount;
            ViewBag.MinDate = searchMinDate;
            ViewBag.MaxDate = searchMaxDate;

            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(s => s.Name);
                    break;
                case "Expired_Date":
                    items = items.OrderBy(s => s.DateExpired);
                    break;
                case "Expired_Date_desc":
                    items = items.OrderByDescending(s => s.DateExpired);
                    break;
                case "Amount":
                    items = items.OrderBy(s => s.Amount);
                    break;
                case "Amount_desc":
                    items = items.OrderByDescending(s => s.Amount);
                    break;
                default:
                    items = items.OrderBy(s => s.Name);
                    break;
            }

            var result = new List<Item>();

            if (!String.IsNullOrEmpty(searchName))
            {
                string searchString = StringHelpers.ConvertToUnSign(searchName).ToLower();
                result = items.ToList().FindAll(
                    delegate (Item i)
                    {
                        if ((StringHelpers.ConvertToUnSign(i.Name.ToLower()).Contains(searchString)))
                            return true;
                        else
                            return false;
                    }
                );
            }
            else
            {
                result = items.ToList();
            }
            if (!String.IsNullOrEmpty(searchMinAmount))
            {
                Decimal minAmount = Convert.ToDecimal(searchMinAmount);
                result = result.Where(s => s.Amount >= minAmount).ToList();
            }
            if (!String.IsNullOrEmpty(searchMaxAmount))
            {
                Decimal maxAmount = Convert.ToDecimal(searchMaxAmount);
                result = result.Where(s => s.Amount <= maxAmount).ToList();
            }
            if (!String.IsNullOrEmpty(searchMinDate))
            {
                DateTime fromDate = Convert.ToDateTime(searchMinDate);
                result = result.Where(s => s.DateExpired >= fromDate).ToList();
            }
            if (!String.IsNullOrEmpty(searchMaxDate))
            {
                DateTime toDate = Convert.ToDateTime(searchMaxDate);
                result = result.Where(s => s.DateExpired <= toDate).ToList();
            }
            return View(result);
        }
        //POST: Check Duplicate return Json
        public JsonResult CheckName(string Name, Guid? Id)
        {
            return Json(!db.Items.Any(x => x.Name == Name && x.Id != Id && !x.IsDeleted), JsonRequestBehavior.AllowGet);
        }
        // GET: _Items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: _Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Unit,Amount,DateExpired,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] Item item)
        {
            AuditTable.InsertAuditFields(item);
            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid();
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: _Items/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: _Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Unit,Amount,DateExpired,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] Item item)
        {
            AuditTable.UpdateAuditFields(item);
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: _Items/Delete/5
        public ActionResult Delete(Guid? id)
        {
            var entityExist1 = db.MenuItems.Where(s => s.ItemId == id && !s.IsDeleted).FirstOrDefault();
            var entityExist2 = db.Properties.Where(s => s.ItemId == id && !s.IsDeleted).FirstOrDefault();
            var entityExist3 = db.Sales.Where(s => s.ItemId == id && !s.IsDeleted).FirstOrDefault();
            if (entityExist1 != null || entityExist2 != null || entityExist3 != null)
            {
                return PartialView("_Delete");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: _Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Item item = db.Items.Find(id);
            item.IsDeleted = true;
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
