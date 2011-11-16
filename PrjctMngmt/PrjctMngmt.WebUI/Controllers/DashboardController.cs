using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrjctMngmt.Models;
using System.Web.Helpers;

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

        public void TaskPieChart()
        {
            new Chart(width: 300, height: 200)
                .AddTitle("Tasks")
                .AddSeries("Default", chartType: "Pie",
                xValue: new[] { "Finished", "Unfinished" },
                yValues: new[] { GetAmountOfFinishedTasks(), GetAmountOfUnfinishedTasks() })
                .Write("png");
        }


        public void ProjectPieChart()
        {
            new Chart(width: 300, height: 200)
                .AddTitle("Projects")
                .AddSeries("Default", chartType: "Pie",
                xValue: new[] { "Finished", "Unfinished" },
                yValues: new[] { GetAmountOfFinishedProjects(), GetAmountOfUnfinishedProjects() })
                .Write("png");
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
