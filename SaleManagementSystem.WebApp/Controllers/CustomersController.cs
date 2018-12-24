using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SaleManagementSystem.Common;
using SaleManagementSystem.Model.DAL;

namespace SaleManagementSystem.WebApp.Controllers
{
    public class CustomersController : Controller
    {
        private SaleManagementSystemEntities db = new SaleManagementSystemEntities();

        // GET: Customers
        public ActionResult Index(string name, string phone, string sortOrder)
        {
            var customers = db.Customers.Where(s => !s.IsDeleted);

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            ViewBag.Name = name;
            ViewBag.Phone = phone;

            switch (sortOrder)
            {
                case "name_desc":
                    customers = customers.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    customers = customers.OrderBy(s => s.JoinDate);
                    break;
                case "date_desc":
                    customers = customers.OrderByDescending(s => s.JoinDate);
                    break;
                default:
                    customers = customers.OrderBy(s => s.Name);
                    break;
            }

            var result = new List<Customer>();

            if (!string.IsNullOrEmpty(name))
            {
                string searchString = StringHelpers.ConvertToUnSign(name).ToLower();
                result = customers.ToList().FindAll(
                    delegate (Customer e)
                    {
                        if ((StringHelpers.ConvertToUnSign(e.Name.ToLower()).Contains(searchString))
                        )
                            return true;
                        else
                            return false;
                    }
                );
            }
            else
            {
                result = customers.ToList();
            }
            if (!String.IsNullOrEmpty(phone))
            {
                result = result.Where(s => s.Phone.Contains(phone)).ToList();
            }
            return View(result);
        }
        //POST: Check Duplicate return Json
        public JsonResult CheckName(string Name, Guid? Id)
        {
            return Json(!db.Customers.Any(x => x.Name == Name && x.Id != Id && !x.IsDeleted), JsonRequestBehavior.AllowGet);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,Phone,JoinDate,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] Customer customer)
        {
            AuditTable.InsertAuditFields(customer);
            if (ModelState.IsValid)
            {
                customer.Id = Guid.NewGuid();
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,Phone,JoinDate,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] Customer customer)
        {
            AuditTable.UpdateAuditFields(customer);
            var entity = db.Customers.Where(p => p.Id != customer.Id && p.Name == customer.Name && !p.IsDeleted);
            if (entity != null && entity.Count() > 0)
            {
                TempData["msg"] = "<script>alert('This name is already exist in system, please enter another name');</script>";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
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
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Customer customer = db.Customers.Find(id);
            customer.IsDeleted = true;
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
