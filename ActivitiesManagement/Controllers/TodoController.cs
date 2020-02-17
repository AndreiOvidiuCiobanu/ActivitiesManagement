using ActivitiesManagement.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActivitiesManagement.Controllers
{
    [Authorize(Roles = "Organiser,Administrator,Member,User")]
    public class TodoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Todo
        public ActionResult Index(int? teamId = null)
        {
            IEnumerable<Todo> todos = Enumerable.Empty<Todo>();
            if(teamId == null)
            {
                if (User.IsInRole("Administrator"))
                {

                    todos = db.Todos.Where(i => i.Id > 0);
                }
                else
                 if (User.IsInRole("Member"))
                {
                    var userId = User.Identity.GetUserId();
                    var memberTeams = db.PersonsInTeams.Where(m => m.ApplicationUserId == userId).AsEnumerable();
                    //if (User.IsInRole("User"))
                    todos = (from persInTeams in memberTeams
                             join teams in db.Teams on persInTeams.TeamId equals teams.Id
                             join projects in db.Projects on teams.Id equals projects.TeamId
                             join tasks in db.Todos on projects.Id equals tasks.ProjectId
                             select tasks).AsEnumerable();
                }
                ViewBag.Todos = todos;
            }
            else
            {
                if (User.IsInRole("Administrator"))
                {

                    todos = db.Todos.Where(i => i.Id > 0);
                }
                else
                if (User.IsInRole("Member"))
                {
                    var userId = User.Identity.GetUserId();
                    var memberTeams = db.PersonsInTeams.Where(m => m.ApplicationUserId == userId).AsEnumerable();
                    //if (User.IsInRole("User"))
                    todos = (from persInTeams in memberTeams
                             join teams in db.Teams on persInTeams.TeamId equals teams.Id where teams.Id == teamId
                             join projects in db.Projects on teams.Id equals projects.TeamId
                             join tasks in db.Todos on projects.Id equals tasks.ProjectId
                             select tasks).AsEnumerable();
                }
                ViewBag.Todos = todos;

            }
           
            return View();
        }

        public ActionResult Show(int id)
        {
            var todo = db.Todos.Where(i => i.ProjectId == id);
            ViewBag.ProjectId = id;
            ViewBag.TaskId = db.Todos.Where(i => i.ProjectId == id).Select(i => i.Id).FirstOrDefault();
            return View(todo);
        }

        // GET: Todo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [Authorize(Roles = "Organiser,Administrator")]

        [NonAction]
        public ICollection<SelectListItem> GetAllUsersByTeam(int teamId)
        {
            var selectList = new List<SelectListItem>();
            var users = db.PersonsInTeams;

            foreach (var user in users)
            {
                // Adaugam in lista elementele necesare pentru dropdown
                var userName = db.Users.Where(u => u.Id == user.ApplicationUserId).FirstOrDefault().UserName;
                selectList.Add(new SelectListItem
                {
                    Value = user.ApplicationUserId.ToString(),
                    Text = userName
                });
            }

            return selectList;
        }
        // GET: Todo/Create
        public ActionResult Create(int id)
        {
            Todo todo = new Todo();
            todo.StatusesForTasks = GetAllStatuses();
            ViewBag.projectId = id;
            todo.ProjectId = id;
            Project project = db.Projects.Where(p => p.Id == id).FirstOrDefault();
            todo.PersonsForTasks = GetAllUsersByTeam(project.TeamId);
            todo.ApplicationUserId = User.Identity.GetUserId();
            return View(todo);
        }
        [NonAction]
        public ICollection<SelectListItem> GetAllStatuses()
        {
            var selectList = new List<SelectListItem>();
            var statuses = db.Statuses.Where(i => i.Id > 0).ToArray();

            foreach (var status in statuses)
            {
                // Adaugam in lista elementele necesare pentru dropdown
                selectList.Add(new SelectListItem
                {
                    Value = status.Id.ToString(),
                    Text = status.Text.ToString()
                });
            }

            return selectList;
        }

        // POST: Todo/Create
        [HttpPost]
        [Authorize(Roles = "Organiser,Administrator")]
        public ActionResult Create(Todo todo, int projectId)
        {
            todo.StatusesForTasks = GetAllStatuses();
            Project project = db.Projects.Where(p => p.Id == projectId).FirstOrDefault();
            todo.PersonsForTasks = GetAllUsersByTeam(project.TeamId);
            try
            {
                if (ModelState.IsValid)
                {
                    db.Todos.Add(todo);
                    db.SaveChanges();
                    return RedirectToAction("Show", new { id = todo.ProjectId });
                }
                else
                {
                    return View(todo);
                }
            }
            catch (Exception e)
            {

                return View();
            }
        }

        // GET: Todo/Edit/5
        public ActionResult Edit(int id)
        {
            Todo todo = db.Todos.Where(t => t.Id == id).FirstOrDefault();
            todo.StatusesForTasks = GetAllStatuses();
            return View(todo);
        }

        // POST: Todo/Edit/5
        [HttpPut]
        public ActionResult Edit(int id, Todo collection)
        {

            collection.StatusesForTasks = GetAllStatuses();

            try
            {
                if (ModelState.IsValid)
                {
                    Todo todo = db.Todos.Find(id);
                    if (TryUpdateModel(todo))
                    {
                        todo.Id = id;
                        todo.ProjectId = collection.ProjectId;
                        todo.StatusId = collection.StatusId;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(collection);
                }
            }
            catch (Exception e)
            {
                return View(collection);
            }

        }
        // POST: Todo/Delete/5
       // [HttpDelete]
        [Authorize(Roles = "Organiser,Administrator")]
        public ActionResult Delete(int id)
        {
            Todo todo = db.Todos.Find(id);
            db.Todos.Remove(todo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
