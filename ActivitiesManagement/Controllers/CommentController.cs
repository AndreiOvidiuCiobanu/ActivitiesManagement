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
    public class CommentController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Comment
        public ActionResult Index()
        {
            IEnumerable<Comment> comment = db.Comments.Where(i => i.Id > 0);
            ViewBag.Points = comment;
            return View();
        }

        // GET: Comment/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.TaskId = id;
            ViewBag.Points = db.Comments.Where(i => i.TodoId == id);
            return View();
        }

        public ActionResult Show(int id)
        {
            ViewBag.point = db.Comments.Where(i => i.TodoId == id);
            ViewBag.todoId = id;
            ViewBag.userId = User.Identity.GetUserId();
            return View();
        }

        // GET: Comment/Create
        [Authorize(Roles = "Organiser,Administrator,Member")]
        public ActionResult Create(int id)
        {
            Comment comment = new Comment();
            comment.TodoId = id;
            comment.ApplicationUserId = User.Identity.GetUserId();
            return View(comment);
        }

        // POST: Comment/Create
        [Authorize(Roles = "Organiser,Administrator,Member")]
        [HttpPost]
        public ActionResult Create(Comment comment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Comments.Add(comment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(comment);
                }
            }
            catch (Exception e)
            {

                return View();
            }
        }

        // GET: Comment/Edit/5
        [Authorize(Roles = "Organiser,Administrator,Member")]
        public ActionResult Edit(int id)
        {
            Comment comment = db.Comments.Where(i => i.Id == id).FirstOrDefault();
            return View(comment);
        }

        // POST: Comment/Edit/5
        [HttpPut]
        [Authorize(Roles = "Organiser,Administrator,Member")]
        public ActionResult Edit(int id, Comment requestComment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Comment comment = db.Comments.Find(id);
                    if (TryUpdateModel(comment))
                    {
                        comment.Point = requestComment.Point;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Show", new { id = comment.TodoId });
                }
                else
                {
                    return View(requestComment);
                }
            }
            catch (Exception e)
            {
                return View(requestComment);
            }
        }

        // POST: Comment/Delete/5
        [HttpDelete]
        [Authorize(Roles = "Organiser,Administrator,Member")]
        public ActionResult Delete(int id)
        {
            Comment comment = db.Comments.Find(id);
            var todoId = comment.TodoId;
            db.Comments.Remove(comment);
            db.SaveChanges();
            TempData["message"] = "Comment-ul a fost sters!";
            return RedirectToAction("Show", new { id = todoId });
        }
    }
}
