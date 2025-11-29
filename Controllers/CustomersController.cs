using Microsoft.AspNetCore.Mvc;
using CustomerManagementApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace CustomerManagementApp.Controllers
{
    public class CustomersController : Controller
    {
        private static List<Customer> customers = new List<Customer>()
        {
            new Customer { Id = 1, Name = "John Doe", Email = "john@example.com" },
            new Customer { Id = 2, Name = "Jane Smith", Email = "jane@example.com" }
        };

        public IActionResult Index()
        {
            return View(customers);
        }

        public IActionResult Details(int id)
        {
            var cust = customers.FirstOrDefault(c => c.Id == id);
            return View(cust);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            customer.Id = customers.Max(c => c.Id) + 1;
            customers.Add(customer);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var cust = customers.FirstOrDefault(c => c.Id == id);
            return View(cust);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            var existing = customers.FirstOrDefault(c => c.Id == customer.Id);
            existing.Name = customer.Name;
            existing.Email = customer.Email;
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var cust = customers.FirstOrDefault(c => c.Id == id);
            return View(cust);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var cust = customers.FirstOrDefault(c => c.Id == id);
            customers.Remove(cust);
            return RedirectToAction("Index");
        }
    }
}
