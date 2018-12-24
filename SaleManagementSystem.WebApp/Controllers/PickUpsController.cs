using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SaleManagementSystem.Common;
using SaleManagementSystem.Model.DAL;

namespace SaleManagementSystem.WebApp.Controllers
{
    public class PickUpsController : Controller
    {
        private SaleManagementSystemEntities db = new SaleManagementSystemEntities();

        // GET: PickUps
        public ActionResult Index(Guid? EmployeeId, Guid? SupplierId, string mindate, string maxdate, string minPrice, string maxPrice, string sortOrder)
        {
            var pickUps = db.PickUps.Include(p => p.Employee).Include(p => p.Supplier).Where(p => !p.IsDeleted && !p.Supplier.IsDeleted && !p.Employee.IsDeleted);
            ViewBag.EmployeeId = new SelectList(db.Employees.Where(p => !p.IsDeleted), "Id", "Name");
            ViewBag.SupplierId = new SelectList(db.Suppliers.Where(p => !p.IsDeleted), "Id", "Name");

            ViewBag.SupplierSortParm = String.IsNullOrEmpty(sortOrder) ? "Supplier_desc" : "";
            ViewBag.EmployeeSortParm = sortOrder == "Employee" ? "Employee_desc" : "Employee";
            ViewBag.DateSortParm = sortOrder == "PickUp_Date" ? "PickUp_Date_desc" : "PickUp_Date";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.QuantitySortParm = sortOrder == "Quantity" ? "Quantity_desc" : "Quantity";

            ViewBag.Employee = EmployeeId;
            ViewBag.Supplier = SupplierId;
            ViewBag.MinDate = mindate;
            ViewBag.MaxDate = maxdate;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;

            switch (sortOrder)
            {
                case "Supplier_desc":
                    pickUps = pickUps.OrderByDescending(s => s.Supplier.Name);
                    break;
                case "Employee":
                    pickUps = pickUps.OrderBy(s => s.Employee.Name);
                    break;
                case "Employee_desc":
                    pickUps = pickUps.OrderByDescending(s => s.Employee.Name);
                    break;
                case "PickUp_Date":
                    pickUps = pickUps.OrderBy(s => s.PickUpDate);
                    break;
                case "PickUp_Date_desc":
                    pickUps = pickUps.OrderByDescending(s => s.PickUpDate);
                    break;
                case "Price":
                    pickUps = pickUps.OrderBy(s => s.Price);
                    break;
                case "Price_desc":
                    pickUps = pickUps.OrderByDescending(s => s.Price);
                    break;
                case "Quantity":
                    pickUps = pickUps.OrderBy(s => s.Quantity);
                    break;
                case "Quantity_desc":
                    pickUps = pickUps.OrderByDescending(s => s.Quantity);
                    break;
                default:
                    pickUps = pickUps.OrderBy(s => s.Supplier.Name);
                    break;
            }

            if (EmployeeId != null)
            {
                pickUps = from p in pickUps where p.EmployeeId == EmployeeId select p;
            }
            if (SupplierId != null)
            {
                pickUps = from p in pickUps where p.SupplierId == SupplierId select p;
            }
            if (!String.IsNullOrEmpty(mindate))
            {
                DateTime DT = Convert.ToDateTime(mindate);
                pickUps = from p in pickUps where p.PickUpDate >= DT select p;
            }
            if (!String.IsNullOrEmpty(maxdate))
            {
                DateTime DE = Convert.ToDateTime(maxdate);
                pickUps = from p in pickUps where p.PickUpDate <= DE select p;
            }
            if (!String.IsNullOrEmpty(minPrice))
            {
                Decimal minP = Convert.ToDecimal(minPrice);
                pickUps = from p in pickUps where p.Price >= minP select p;
            }
            if (!String.IsNullOrEmpty(maxPrice))
            {
                Decimal maxP = Convert.ToDecimal(maxPrice);
                pickUps = from p in pickUps where p.Price <= maxP select p;
            }

            return View(pickUps.ToList());
        }

        // GET: PickUps/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees.Where(p => !p.IsDeleted), "Id", "Name");
            ViewBag.SupplierId = new SelectList(db.Suppliers.Where(p => !p.IsDeleted), "Id", "Name");
            return View();
        }

        // POST: PickUps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SupplierId,EmployeeId,PickUpDate,Price,Quantity,Address,Unit,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] PickUp pickUp)
        {
            AuditTable.InsertAuditFields(pickUp);
            if (ModelState.IsValid)
            {
                pickUp.Id = Guid.NewGuid();
                db.PickUps.Add(pickUp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees.Where(p => !p.IsDeleted), "Id", "Name", pickUp.EmployeeId);
            ViewBag.SupplierId = new SelectList(db.Suppliers.Where(p => !p.IsDeleted), "Id", "Name", pickUp.SupplierId);
            return View(pickUp);
        }

        // GET: PickUps/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickUp pickUp = db.PickUps.Find(id);
            if (pickUp == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees.Where(p => !p.IsDeleted), "Id", "Name", pickUp.EmployeeId);
            ViewBag.SupplierId = new SelectList(db.Suppliers.Where(p => !p.IsDeleted), "Id", "Name", pickUp.SupplierId);
            return View(pickUp);
        }

        // POST: PickUps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SupplierId,EmployeeId,PickUpDate,Price,Quantity,Address,Unit,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] PickUp pickUp)
        {
            AuditTable.UpdateAuditFields(pickUp);
            if (ModelState.IsValid)
            {
                db.Entry(pickUp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees.Where(p => !p.IsDeleted), "Id", "Name", pickUp.EmployeeId);
            ViewBag.SupplierId = new SelectList(db.Suppliers.Where(p => !p.IsDeleted), "Id", "Name", pickUp.SupplierId);
            return View(pickUp);
        }

        // GET: PickUps/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickUp pickUp = db.PickUps.Find(id);
            if (pickUp == null)
            {
                return HttpNotFound();
            }
            return View(pickUp);
        }

        // POST: PickUps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            PickUp pickUp = db.PickUps.Find(id);
            //db.PickUps.Remove(pickUp);
            pickUp.IsDeleted = true;
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
