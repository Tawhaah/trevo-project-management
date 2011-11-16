using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrjctMngmt.Models;

namespace PrjctMngmt.Controllers
{
    public class DashboardController : Controller
    {
        private EntityModelContainer db = new EntityModelContainer();
        private DashboardModel model = new DashboardModel();

        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            model.TotalTasks = GetAmountOfTotalTasks();
            model.FinishedTasks = GetAmountOfFinishedTasks();
            model.UnfinishedTasks = GetAmountOfUnfinishedTasks();

            model.TotalProjects = GetAmountOfTotalProjects();
            model.FinishedProjects = GetAmountOfFinishedProjects();
            model.UnfinishedProjects = GetAmountOfUnfinishedProjects();

            model.TotalDevelopers = GetAmountOfDevelopers();

            return View(model);
        }

        //
        // GET: /Dashboard/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Dashboard/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Dashboard/Create

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
        
        //
        // GET: /Dashboard/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Dashboard/Edit/5

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

        //
        // GET: /Dashboard/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Dashboard/Delete/5

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

        public int GetAmountOfUnfinishedProjects()
        {
            int amount = db.Projects.ToArray().Length -
                db.Projects.Where(p => p.Status.Contains("Finished")).ToArray().Length;
            return amount;
        }

        public int GetAmountOfFinishedProjects()
        {
            var projects = db.Projects.Where(t => t.Status.Contains("Finished"));
            int amount = projects.ToArray().Length;
            return amount;
        }

        public int GetAmountOfTotalProjects()
        {
            int amount = db.Projects.ToArray().Length;
            return amount;
        }

        public int GetAmountOfFinishedTasks()
        {
            //byte values cannot be compared with LINQ ... therefore
            List<Task> tasks = db.Tasks.ToList();
            int amount = 0;
            foreach(Task task in tasks) 
            {
                if (task.Finished == 1)
                {
                    amount++;
                }
            }
            return amount;
        }

        public int GetAmountOfUnfinishedTasks()
        {
            //byte values cannot be compared with LINQ ... therefore
            List<Task> tasks = db.Tasks.ToList();
            int amount = 0;
            foreach (Task task in tasks)
            {
                if (task.Finished == 0)
                {
                    amount++;
                }
            }
            return amount;
        }

        public int GetAmountOfTotalTasks()
        {
            int amount = db.Tasks.ToArray().Length;
            return amount;
        }

        public int GetAmountOfDevelopers()
        {
            return db.Developers.ToArray().Length;
        }
    }
}
