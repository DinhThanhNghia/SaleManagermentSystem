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
    public class MenuItemsController : Controller
    {
        private SaleManagementSystemEntities db = new SaleManagementSystemEntities();

        // GET: MenuItems
        public ActionResult Index(Guid? itemId, string fromdate, string todate, string max, string min, string sortOrder)
        {
            var menuItems = db.MenuItems.Include(m => m.Item).Where(s => !s.IsDeleted);
            ViewBag.ItemId = new SelectList(db.Items.Where(s => !s.IsDeleted), "Id", "Name");

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AmountSortParm = sortOrder == "Amount" ? "Amount_desc" : "Amount";
            ViewBag.CreateDateSortParm = sortOrder == "CreateDate" ? "CreateDate_desc" : "CreateDate";
            ViewBag.LastUseDateSortParm = sortOrder == "LastUseDate" ? "LastUseDate_desc" : "LastUseDate";
            ViewBag.RatingSortParm = sortOrder == "Rating" ? "Rating_desc" : "Rating";

            ViewBag.ItemName = itemId;
            ViewBag.CreateDate = fromdate;
            ViewBag.LastUseDate = todate;
            ViewBag.MinAmount = min;
            ViewBag.MaxAmount = max;

            switch (sortOrder)
            {
                case "name_desc":
                    menuItems = menuItems.OrderByDescending(s => s.Item.Name);
                    break;
                case "Amount":
                    menuItems = menuItems.OrderBy(s => s.Item.Amount);
                    break;
                case "Amount_desc":
                    menuItems = menuItems.OrderByDescending(s => s.Item.Amount);
                    break;
                case "CreateDate":
                    menuItems = menuItems.OrderBy(s => s.CreateDate);
                    break;
                case "CreateDate_desc":
                    menuItems = menuItems.OrderByDescending(s => s.CreateDate);
                    break;
                case "LastUseDate":
                    menuItems = menuItems.OrderBy(s => s.LastUsedDate);
                    break;
                case "LastUseDate_desc":
                    menuItems = menuItems.OrderByDescending(s => s.LastUsedDate);
                    break;
                case "Rating":
                    menuItems = menuItems.OrderBy(s => s.Rating);
                    break;
                case "Rating_desc":
                    menuItems = menuItems.OrderByDescending(s => s.Rating);
                    break;
                default:
                    menuItems = menuItems.OrderBy(s => s.Item.Name);
                    break;
            }

            if (!String.IsNullOrEmpty(max))
            {
                decimal Max = Convert.ToDecimal(max);
                menuItems = menuItems.Where(s => s.Item.Amount <= Max);
            }
            if (!String.IsNullOrEmpty(min))
            {
                decimal Min = Convert.ToDecimal(min);
                menuItems = menuItems.Where(s => s.Item.Amount >= Min);
            }
            if (!String.IsNullOrEmpty(fromdate))
            {
                DateTime FD = Convert.ToDateTime(fromdate);
                menuItems = menuItems.Where(s => s.CreateDate >= FD);
            }
            if (!String.IsNullOrEmpty(todate))
            {
                DateTime TD = Convert.ToDateTime(todate);
                menuItems = menuItems.Where(s => s.LastUsedDate <= TD);
            }
            if (itemId != null)
            {
                menuItems = menuItems.Where(s => s.ItemId == itemId);
            }
            return View(menuItems.ToList());
        }
        // GET: MenuItems/Create
        public ActionResult Create()
        {
            ViewBag.ItemId = new SelectList(db.Items.Where(s => !s.IsDeleted), "Id", "Name");
            return View();
        }

        // POST: MenuItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ItemId,CreateDate,LastUsedDate, Rating, IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] MenuItem menuItem)
        {
            AuditTable.InsertAuditFields(menuItem);
            if (ModelState.IsValid)
            {
                menuItem.Id = Guid.NewGuid();
                db.MenuItems.Add(menuItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemId = new SelectList(db.Items.Where(s => !s.IsDeleted), "Id", "Name", menuItem.ItemId);
            return View(menuItem);
        }

        // GET: MenuItems/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItem menuItem = db.MenuItems.Find(id);
            if (menuItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemId = new SelectList(db.Items.Where(s => !s.IsDeleted), "Id", "Name", menuItem.ItemId);
            return View(menuItem);
        }

        // POST: MenuItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ItemId,CreateDate,LastUsedDate, Rating, IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] MenuItem menuItem)
        {
            AuditTable.UpdateAuditFields(menuItem);
            if (ModelState.IsValid)
            {
                db.Entry(menuItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemId = new SelectList(db.Items.Where(s => !s.IsDeleted), "Id", "Name", menuItem.ItemId);
            return View(menuItem);
        }

        // GET: MenuItems/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItem menuItem = db.MenuItems.Find(id);
            if (menuItem == null)
            {
                return HttpNotFound();
            }
            return View(menuItem);
        }

        // POST: MenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MenuItem menuItem = db.MenuItems.Find(id);
            menuItem.IsDeleted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET Id for display in popup
        [HttpPost]
        public ActionResult PopulateItem(string id)
        {
            var item = db.Items
                .Select(x => new { Id = x.Id.ToString(), x.Amount, x.Unit })
                .FirstOrDefault(x => x.Id.ToString() == id);

            return Json(new { data = item });
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
