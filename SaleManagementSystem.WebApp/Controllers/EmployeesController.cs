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
    public class EmployeesController : Controller
    {
        private SaleManagementSystemEntities db = new SaleManagementSystemEntities();

        // GET: Employees
        public ActionResult Index(string searchname, string searchphone, string searchjoindate, string searchleftdate, string searchposition, string searchminsalary, string searchmaxsalary, string sortOrder)
        {
            var employees = db.Employees.Where(s => !s.IsDeleted);

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.JoinedDateSortParm = sortOrder == "JoinedDate" ? "JoinedDate_desc" : "JoinedDate";
            ViewBag.LeftDateSortParm = sortOrder == "LeftDate" ? "LeftDate_desc" : "LeftDate";
            ViewBag.SalarySortParm = sortOrder == "Salary" ? "Salary_desc" : "Salary";

            ViewBag.Name = searchname;
            ViewBag.Phone = searchphone;
            ViewBag.JoinDate = searchjoindate;
            ViewBag.LeftDate = searchleftdate;
            ViewBag.Position = searchposition;
            ViewBag.MinSalary = searchminsalary;
            ViewBag.MaxSalary = searchmaxsalary;

            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(s => s.Name);
                    break;
                case "JoinedDate":
                    employees = employees.OrderBy(s => s.JoinDate);
                    break;
                case "JoinedDate_desc":
                    employees = employees.OrderByDescending(s => s.JoinDate);
                    break;
                case "LeftDate":
                    employees = employees.OrderBy(s => s.LeftDate);
                    break;
                case "LeftDate_desc":
                    employees = employees.OrderByDescending(s => s.LeftDate);
                    break;
                case "Salary":
                    employees = employees.OrderBy(s => s.Salary);
                    break;
                case "Salary_desc":
                    employees = employees.OrderByDescending(s => s.Salary);
                    break;
                default:
                    employees = employees.OrderBy(s => s.Name);
                    break;
            }

            var result = new List<Employee>();

            if (!String.IsNullOrEmpty(searchname))
            {
                string searchString = StringHelpers.ConvertToUnSign(searchname).ToLower();
                result = employees.ToList().FindAll(
                    delegate (Employee e)
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
                result = employees.ToList();
            }
            if (!String.IsNullOrEmpty(searchphone))
            {
                result = result.Where(s => s.Phone.Contains(searchphone)).ToList();
            }
            if (!String.IsNullOrEmpty(searchjoindate))
            {
                DateTime DT = Convert.ToDateTime(searchjoindate);
                result = result.Where(s => s.JoinDate >= DT).ToList();
            }
            if (!String.IsNullOrEmpty(searchleftdate))
            {
                DateTime DE = Convert.ToDateTime(searchleftdate);
                result = result.Where(s => s.LeftDate <= DE).ToList();
            }
            if (!String.IsNullOrEmpty(searchposition))
            {
                result = result.Where(s => s.Position.Contains(searchposition)).ToList();
            }

            if (!String.IsNullOrEmpty(searchminsalary))
            {
                Decimal Minsalary = Convert.ToDecimal(searchminsalary);
                result = result.Where(s => s.Salary >= Minsalary).ToList();
            }
            if (!String.IsNullOrEmpty(searchmaxsalary))
            {
                Decimal Maxsalary = Convert.ToDecimal(searchmaxsalary);
                result = result.Where(s => s.Salary <= Maxsalary).ToList();
            }
            return View(result);
        }
        //POST: Check Duplicate return to Json
        public JsonResult CheckName(string Name, Guid? Id)
        {
            return Json(!db.Employees.Any(x => x.Name == Name && x.Id != Id && !x.IsDeleted), JsonRequestBehavior.AllowGet);
        }
        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Phone,JoinDate,LeftDate,Position,Salary,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] Employee employee, string name)
        {
            AuditTable.InsertAuditFields(employee);
            if (ModelState.IsValid)
            {
                employee.Id = Guid.NewGuid();
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Phone,JoinDate,LeftDate,Position,Salary,IsDeleted,InsAt,InsBy,UpdAt,UpdBy")] Employee employee)
        {
            AuditTable.UpdateAuditFields(employee); var entity = db.Employees.Where(p => p.Id != employee.Id && p.Name == employee.Name && !p.IsDeleted);
            if (entity != null && entity.Count() > 0)
            {
                TempData["msg"] = "<script>alert('This name is already exist in system, please enter another name');</script>";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(Guid? id)
        {
            var entityExist1 = db.PickUps.Where(s => s.EmployeeId == id && !s.IsDeleted).FirstOrDefault();
            var entityExist2 = db.Sales.Where(s => s.EmployeeId == id && !s.IsDeleted).FirstOrDefault();
            if (entityExist1 != null || entityExist2 != null)
            {
                return PartialView("_Delete");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Employee employee = db.Employees.Find(id);
            employee.IsDeleted = true;
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
