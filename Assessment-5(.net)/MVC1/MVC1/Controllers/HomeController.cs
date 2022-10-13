using MVC1.Models;
using MVC1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC1.Controllers
{
    public class HomeController : Controller
    {
       
       SnadDataEntities Context=new SnadDataEntities();
        public ActionResult Index()
        {
            List<Employee> employees = Context.Employees.Include("Department").ToList();
            List<Employee> list = (from a in Context.Employees where a.DeptId == 1 select a).ToList();
            list = Context.Employees.Where(x => x.DeptId == 1).ToList();
            return View(employees);
            var empList = (from a in Context.Employees
                           join b in Context.Departments on a.DeptId equals b.DeptId
                           select new DeptEmp
                           {
                               DeptId = a.DeptId,
                               DeptName = b.DeptName,
                               DeptLocation = b.DeptLocation,
                               EmpId = a.EmpId,
                               EmpName = a.EmpName,
                               EmpSalary = a.EmpSalary
                           }).ToList();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            emp.EmpId = 0;
            Context.Employees.Add(emp);
            Context.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult Details(int id)
        {
            var emp = Context.Employees.Where(x => x.EmpId == id).SingleOrDefault();
            return View(emp);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var emp = Context.Employees.Where(x => x.EmpId == id).SingleOrDefault();
            return View(emp);
        }
        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            Context.Entry(emp).State = System.Data.Entity.EntityState.Modified;
            Context.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult Delete(int id)
        {
            var emp = Context.Employees.Where(x => x.EmpId == id).SingleOrDefault();
            Context.Employees.Remove(emp);
            Context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}