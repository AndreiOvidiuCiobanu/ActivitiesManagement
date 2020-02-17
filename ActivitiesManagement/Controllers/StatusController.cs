using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActivitiesManagement.Controllers
{
    [Authorize(Roles = "Organiser,Administrator,Member,User")]
    public class StatusController : Controller
    {
        // GET: Status
        public ActionResult Index()
        {
            return View();
        }

        // GET: Status/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Status/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Status/Edit/5
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

        // GET: Status/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Status/Delete/5
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
