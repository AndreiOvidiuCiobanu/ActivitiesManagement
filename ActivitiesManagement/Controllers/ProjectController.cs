using ActivitiesManagement.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ActivitiesManagement.Controllers
{
    [Authorize(Roles = "Organiser,Administrator,Member,User")]
    public class ProjectController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Project
        public ActionResult Index()
        {
            IEnumerable<Project> allProjects = Enumerable.Empty<Project>();
            if (User.IsInRole("Administrator"))
            {

                allProjects = db.Projects;
            }
            else
            if (User.IsInRole("Member") || User.IsInRole("Organiser"))
            {
                var userId = User.Identity.GetUserId();
                var memberTeams = db.PersonsInTeams.Where(m => m.ApplicationUserId == userId).AsEnumerable();
                //if (User.IsInRole("User"))
                allProjects = (from persInTeams in memberTeams
                               join teams in db.Teams on persInTeams.TeamId equals teams.Id
                               join projects in db.Projects on teams.Id equals projects.TeamId
                               select projects).AsEnumerable();

                //IEnumerable<Project> projects = db.Projects.Where(i => i.Id > 0);
                //ViewBag.Projects = projects;
            }else
            if (User.IsInRole("User"))
            {
                ViewBag.Projects = null;
                return View();
            }
            ViewBag.Projects = allProjects;
            return View();
        }

        // GET: Project/Details/5
        public ActionResult Details(int id)
        {
            var project = db.Projects.Where(i => i.Id == id);
            ViewBag.Tasks = db.Todos.Where(i => i.ProjectId == id);
            return View(project);
        }

        // GET: Project/Create
        [Authorize(Roles = "Organiser,Administrator,Member,User")]
        public ActionResult Create()
        {
            Project project = new Project();
            //var t = db.Teams.Where(i => i.Id > 0).ToList();
            project.TeamsForProjects = GetAllTeams();
            return View(project);
        }

        [NonAction]
        public ICollection<SelectListItem> GetAllTeams()
        {
            var selectList = new List<SelectListItem>();
            var teams = db.Teams.Where(i => i.Id > 0).ToArray();

            foreach (var team in teams)
            {
                // Adaugam in lista elementele necesare pentru dropdown
                selectList.Add(new SelectListItem
                {
                    Value = team.Id.ToString(),
                    Text = team.Name.ToString()
                });
            }

            return selectList;
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(Project project)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            try
            {
                project.TeamsForProjects = GetAllTeams();
                if (ModelState.IsValid)
                {
                    if (User.IsInRole("User"))
                    {
                        //ApplicationUser user = db.Users.Where(u => u.Id == User.Identity.GetUserId()).FirstOrDefault();
                        UserManager.RemoveFromRole(User.Identity.GetUserId(), "User");
                        UserManager.AddToRole(User.Identity.GetUserId(), "Organiser");
                    }
                    db.Projects.Add(project);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(project);
                }
            }
            catch (Exception e)
            {

                return View();
            }
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int id)
        {

            return View();
        }

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Project/Delete/5
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
