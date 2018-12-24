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
    public class SalesController : Controller
    {
        private SaleManagementSystemEntities db = new SaleManagementSystemEntities();

        // GET: Sales
        public ActionResult Index(Guid? customerId, Guid? employeeId, string searchItemName, string searchCode, string sortOrder)
        {
            var sales = db.Sales.Include(s => s.Customer).Include(s => s.Employee).Include(s => s.Item)
                .Where(s => !s.IsDeleted);

            ViewBag.CustomerId = new SelectList(db.Customers.Where(c => !c.IsDeleted), "Id", "Name");
            ViewBag.EmployeeId = new SelectList(db.Employees.Where(c => !c.IsDeleted), "Id", "Name");
            ViewBag.searchItem = new SelectList(db.Items.Where(c => !c.IsDeleted), "Id", "Name");

            ViewBag.SaleCodeSortParm = String.IsNullOrEmpty(sortOrder) ? "saleCode_desc" : "";
            ViewBag.CustomerSortParm = sortOrder == "Customer" ? "Customer_desc" : "Customer";
            ViewBag.EmployeeSortParm = sortOrder == "Employee" ? "Employee_desc" : "Employee";
            ViewBag.ItemSortParm = sortOrder == "Item" ? "Item_desc" : "Item";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.QuantitySortParm = sortOrder == "Quantity" ? "Quantity_desc" : "Quantity";

            ViewBag.Customer = customerId;
            ViewBag.Employee = employeeId;
            ViewBag.Code = searchCode;
            ViewBag.Item = searchItemName;

            switch (sortOrder)
            {
                case "saleCode_desc":
                    sales = sales.OrderByDescending(s => s.SaleCode);
                    break;
                case "Customer":
                    sales = sales.OrderBy(s => s.Customer.Name);
                    break;
                case "Customer_desc":
                    sales = sales.OrderByDescending(s => s.Customer.Name);
                    break;
                case "Employee":
                    sales = sales.OrderBy(s => s.Employee.Name);
                    break;
                case "Employee_desc":
                    sales = sales.OrderByDescending(s => s.Employee.Name);
                    break;
                case "Item":
                    sales = sales.OrderBy(s => s.Item.Name);
                    break;
                case "Item_desc":
                    sales = sales.OrderByDescending(s => s.Item.Name);
                    break;
                case "Price":
                    sales = sales.OrderBy(s => s.Price);
                    break;
                case "Price_desc":
                    sales = sales.OrderByDescending(s => s.Price);
                    break;
                case "Quantity":
                    sales = sales.OrderBy(s => s.Quantity);
                    break;
                case "Quantity_desc":
                    sales = sales.OrderByDescending(s => s.Quantity);
                    break;
                default:
                    sales = sales.OrderBy(s => s.SaleCode);
                    break;
            }

            var result = new List<Sale>();

            if (!String.IsNullOrEmpty(searchItemName))
            {
                string searchString = StringHelpers.ConvertToUnSign(searchItemName).ToLower();
                result = sales.ToList().FindAll(
                    delegate (Sale s)
                    {
                        if ((StringHelpers.ConvertToUnSign(s.Item.Name.ToLower()).Contains(searchString)))
                            return true;
                        else
                            return false;
                    }
                );
            }
            else
            {
                result = sales.ToList();
            }
            if (!String.IsNullOrEmpty(searchCode))
            {
                result = result.Where(s => s.SaleCode.Contains(searchCode)).ToList();
            }
            if (customerId != null)
            {
                result = result.Where(s => s.CustomerId == customerId).ToList();
            }
            if (employeeId != null)
            {
                result = result.Where(s => s.EmployeeId == employeeId).ToList();
            }
            return View(result);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers.Where(s => !s.IsDeleted), "Id", "Name");
            ViewBag.EmployeeId = new SelectList(db.Employees.Where(s => !s.IsDeleted), "Id", "Name");
            ViewBag.ItemId = new SelectList(db.Items.Where(s => !s.IsDeleted), "Id", "Name");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeId,CustomerId,SaleCode,Unit,Price,ItemId,Quantity,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] Sale sale)
        {
            AuditTable.InsertAuditFields(sale);
            //sale.SaleCode = "GroupOne-" + DateTime.Now.ToString("MMddyyyy-HHmm");
            if (ModelState.IsValid)
            {
                sale.Id = Guid.NewGuid();
                db.Sales.Add(sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers.Where(s => !s.IsDeleted), "Id", "Name", sale.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees.Where(s => !s.IsDeleted), "Id", "Name", sale.EmployeeId);
            ViewBag.ItemId = new SelectList(db.Items.Where(s => !s.IsDeleted), "Id", "Name", sale.ItemId);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers.Where(s => !s.IsDeleted), "Id", "Name", sale.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees.Where(s => !s.IsDeleted), "Id", "Name", sale.EmployeeId);
            ViewBag.ItemId = new SelectList(db.Items.Where(s => !s.IsDeleted), "Id", "Name", sale.ItemId);
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,CustomerId,SaleCode,Unit,Price,ItemId,Quantity,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] Sale sale)
        {
            AuditTable.UpdateAuditFields(sale);
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers.Where(s => !s.IsDeleted), "Id", "Name", sale.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees.Where(s => !s.IsDeleted), "Id", "Name", sale.EmployeeId);
            ViewBag.ItemId = new SelectList(db.Items.Where(s => !s.IsDeleted), "Id", "Name", sale.ItemId);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(Guid? id)
        {
            var entityExist = db.Deliveries.Where(s => s.SaleId == id && !s.IsDeleted).FirstOrDefault();
            if (entityExist != null)
            {
                return PartialView("_Delete");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Sale sale = db.Sales.Find(id);
            sale.IsDeleted = true;
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
