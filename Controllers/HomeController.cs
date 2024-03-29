﻿using mvc_crud.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_crud.Models;

namespace mvc_crud.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public HomeController()
        {
            _employeeRepository = new EmployeeRepository(new EmployeeEntities());
        }
       public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public ActionResult Index()
        {
            var employee = _employeeRepository.GetEmployees();
            return View(employee);
        }
        public ActionResult Details(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            return View(employee);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.NewEmployee(employee);
                _employeeRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            return View(employee);
        }
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if(ModelState.IsValid)
            {
                _employeeRepository.UpdateEmployee(employee);
                _employeeRepository.Save();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(employee);
            }
          
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            return View(employee);
        }
        [HttpPost][ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            _employeeRepository.DeleteEmployee(id);
            _employeeRepository.Save();
            return RedirectToAction("Index", "Home");
        }
        //        [HttpGet]
        //        public ActionResult Delete(int id)
        //        {
        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadGateway);
        //            }
        //            var employee = _employeeRepository.GetEmployeeById(id)
        //;
        //            if (employee == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            return View(employee);
        //        }

        //        [HttpPost, ActionName("Delete")]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult ConfirmDelete(int id)
        //        {
        //            _employeeRepository.DeleteEmployee(id);
        //            _employeeRepository.Save();
        //            return RedirectToAction("Index", "Home");
        //        }
    }
}