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
    public class _PropertiesController : Controller
    {
        private SaleManagementSystemEntities db = new SaleManagementSystemEntities();

        // GET: _Properties
        public ActionResult Index(Guid? itemId, string fromdate, string todate, string sortOrder)
        {
            var properties = db.Properties.Include(p => p.Item).Where(s => !s.IsDeleted).Select(s => s);
            ViewBag.ItemId = new SelectList(db.Items.Where(s => !s.IsDeleted), "Id", "Name");

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Owned_Date" ? "Owned_Date_desc" : "Owned_Date";

            ViewBag.Item = itemId;
            ViewBag.FromDate = fromdate;
            ViewBag.ToDate = todate;

            switch (sortOrder)
            {
                case "name_desc":
                    properties = properties.OrderByDescending(s => s.Item.Name);
                    break;
                case "Owned_Date":
                    properties = properties.OrderBy(s => s.OwnedDate);
                    break;
                case "Owned_Date_desc":
                    properties = properties.OrderByDescending(s => s.OwnedDate);
                    break;
                default:
                    properties = properties.OrderBy(s => s.Item.Name);
                    break;
            }

            if (!String.IsNullOrEmpty(fromdate))
            {
                DateTime FD = Convert.ToDateTime(fromdate);
                properties = properties.Where(s => s.OwnedDate >= FD);
            }
            if (!String.IsNullOrEmpty(todate))
            {
                DateTime TD = Convert.ToDateTime(todate);
                properties = properties.Where(s => s.OwnedDate <= TD);
            }
            if (itemId != null)
            {
                properties = properties.Where(s => s.ItemId == itemId);
            }
            return View(properties.ToList());
        }
        // GET: _Properties/Create
        public ActionResult Create()
        {
            ViewBag.ItemId = new SelectList(db.Items.Where(s => !s.IsDeleted), "Id", "Name");
            return View();
        }

        // POST: _Properties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ItemId,Description,Status,OwnedDate,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] Property property)
        {
            AuditTable.InsertAuditFields(property);
            if (ModelState.IsValid)
            {
                property.Id = Guid.NewGuid();
                db.Properties.Add(property);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemId = new SelectList(db.Items.Where(s => !s.IsDeleted), "Id", "Name", property.ItemId);
            return View(property);
        }

        // GET: _Properties/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemId = new SelectList(db.Items.Where(s => !s.IsDeleted), "Id", "Name", property.ItemId);
            return View(property);
        }

        // POST: _Properties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ItemId,Description,Status,OwnedDate,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] Property property)
        {
            AuditTable.UpdateAuditFields(property);
            if (ModelState.IsValid)
            {
                db.Entry(property).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemId = new SelectList(db.Items.Where(s => !s.IsDeleted), "Id", "Name", property.ItemId);
            return View(property);
        }

        // GET: _Properties/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: _Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Property property = db.Properties.Find(id);
            property.IsDeleted = true;
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
