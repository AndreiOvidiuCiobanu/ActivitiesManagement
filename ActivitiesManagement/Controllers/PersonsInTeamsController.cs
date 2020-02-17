using ActivitiesManagement.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActivitiesManagement.Controllers
{
    [Authorize(Roles = "Organiser,Administrator,Member,User")]
    public class PersonsInTeamsController : Controller
    {
        // GET: PersonsInTeams
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            IEnumerable<ApplicationUser> applicationUsers = db.Users.Where(i => i.Id != null);
            ViewBag.Users = applicationUsers;
            return View();
        }

        // GET: PersonsInTeams/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [NonAction]
        public ICollection<SelectListItem> GetAllUsers()
        {
            var selectList = new List<SelectListItem>();
            var users = db.Users.Where(i => i.Id != null).ToArray();

            foreach (var user in users)
            {
                // Adaugam in lista elementele necesare pentru dropdown
                selectList.Add(new SelectListItem
                {
                    Value = user.Id.ToString(),
                    Text = user.UserName.ToString()
                });
            }

            return selectList;
        }

        public ActionResult Show(int id)
        {

            return View();
        }

        // GET: PersonsInTeams/Create
        [Authorize(Roles = "Organiser,Administrator")]
        public ActionResult Create(int id)
        {
            PersonsInTeams personsInTeams = new PersonsInTeams();
            personsInTeams.TeamId = id;
            personsInTeams.UsersForTeams = GetAllUsers();
            return View(personsInTeams);
        }

        // POST: PersonsInTeams/Create
        [HttpPost]
        [Authorize(Roles = "Organiser,Administrator")]
        public ActionResult Create(PersonsInTeams personsInTeams)
        {
            personsInTeams.UsersForTeams = GetAllUsers();
            try
            {
                if (ModelState.IsValid)
                {
                    db.PersonsInTeams.Add(personsInTeams);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = personsInTeams.TeamId });
                }
                else
                {
                    return View(personsInTeams);
                }
            }
            catch (Exception e)
            {

                return View();
            }
        }

        // GET: PersonsInTeams/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PersonsInTeams/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonsInTeams/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersonsInTeams/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
