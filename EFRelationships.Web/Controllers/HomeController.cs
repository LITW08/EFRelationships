using EFRelationships.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EFRelationships.Data;
using Microsoft.Extensions.Configuration;

namespace EFRelationships.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;
        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var repo = new PeopleCarsRepository(_connectionString);
            return View(repo.GetPeople());
        }

        public IActionResult NewPerson()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPerson(Person person)
        {
            var repo = new PeopleCarsRepository(_connectionString);
            repo.AddPerson(person);
            return RedirectToAction("Index");
        }

        public IActionResult AddCar(int personId)
        {
            var repo = new PeopleCarsRepository(_connectionString);
            var person = repo.GetPerson(personId);
            return View(person);
        }

        [HttpPost]
        public IActionResult AddCar(Car car)
        {
            var repo = new PeopleCarsRepository(_connectionString);
            repo.AddCar(car);
            return RedirectToAction("Index");
        }

        public IActionResult Losers()
        {
            var repo = new PeopleCarsRepository(_connectionString);
            return View(repo.GetPeopleWithNoCars());
        }
    }
}
