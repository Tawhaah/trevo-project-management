using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrjctMngmt.Models;
using System.Web.Helpers;
using PrjctMngmt.Helpers;

namespace PrjctMngmt.Controllers
{
    [Authorize]
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

            model.TotalIssues = GetAmountOfTotalIssues();
            model.FinishedIssues = GetAmountOfFinishedIssues();
            model.UnfinishedIssues = GetAmountOfUnfinishedIssues();

            model.ConfsDuringWeek = GetConferencesThisWeek();
            model.ConfsNextWeek = GetConferencesNextWeek();
            model.ConfsDuringMonth = GetConferencesThisMonth();

            model.MilestonesDuringWeek = GetMilestonesThisWeek();
            model.MilestonesNextWeek = GetMilestonesNextWeek();
            model.MilestonesDuringMonth = GetMilestonesThisMonth();

            return View(model);
        }

        public void TaskPieChart()
        {
            int finished = GetAmountOfFinishedTasks();
            int unfinished = GetAmountOfUnfinishedTasks();
            new Chart(width: 300, height: 200, theme: CustomChartThemes.PieTheme)
                .AddTitle("Tasks")
                .AddSeries("Default", chartType: "Pie",
                xValue: new[] { "Finished\n" + finished, "Unfinished\n" + unfinished },
                yValues: new[] { finished, unfinished })
                .Write("png");
        }


        public void ProjectPieChart()
        {
            int finished = GetAmountOfFinishedProjects();
            int unfinished = GetAmountOfUnfinishedProjects();
            new Chart(width: 300, height: 200, theme: CustomChartThemes.PieTheme)
                .AddTitle("Projects")
                .AddSeries("Default", chartType: "Pie",
                xValue: new[] { "Finished\n" + finished, "Unfinished\n" + unfinished },
                yValues: new[] { finished, unfinished })
                .Write("png");
        }

        public void IssuePieChart()
        {
            int finished = GetAmountOfFinishedIssues();
            int unfinished = GetAmountOfUnfinishedIssues();
            new Chart(width: 300, height: 200, theme: CustomChartThemes.PieTheme)
                .AddTitle("Issues")
                .AddSeries("Default", chartType: "Pie",
                xValue: new[] { "Finished\n" + finished, "Unfinished\n" + unfinished },
                yValues: new[] { finished, unfinished })
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

        public int GetAmountOfUnfinishedIssues()
        {
            int amount = db.Issues.ToArray().Length - GetAmountOfFinishedIssues();
            return amount;
        }

        public int GetAmountOfFinishedIssues()
        {
            int amount = 
                db.Issues.Where(i => i.Status.Contains("Fixed")).ToArray().Length
                + db.Issues.Where(i => i.Status.Contains("Done")).ToArray().Length
                + db.Issues.Where(i => i.Status.Contains("Duplicate")).ToArray().Length
                + db.Issues.Where(i => i.Status.Contains("WontFix")).ToArray().Length
                + db.Issues.Where(i => i.Status.Contains("Invalid")).ToArray().Length;
            return amount;
        }

        public int GetAmountOfTotalIssues()
        {
            int amount = db.Issues.ToArray().Length;
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

        public List<Milestone> GetMilestonesThisWeek()
        {
            var datenow = DateTime.Now;
            var datenowPlusWeek = datenow.AddDays(7);
            var milestones = from m in db.Milestones
                            where m.DueDate >= datenow && m.DueDate <= datenowPlusWeek
                            orderby m.DueDate
                            select m;
            List<Milestone> mList = milestones.ToList<Milestone>();
            return mList;
        }

        public List<Milestone> GetMilestonesNextWeek()
        {
            var datenow = DateTime.Now;
            var datenowPlusWeek = datenow.AddDays(7);
            var datenowPlusTwoWeeks = datenow.AddDays(14);
            var milestones = from m in db.Milestones
                            where m.DueDate >= datenowPlusWeek && m.DueDate <= datenowPlusTwoWeeks
                            orderby m.DueDate
                            select m;
            List<Milestone> mList = milestones.ToList<Milestone>();
            return mList;
        }

        public List<Milestone> GetMilestonesThisMonth()
        {
            var datenow = DateTime.Now;
            var datenowPlusMonth = datenow.AddMonths(1);
            var milestones = from m in db.Milestones
                             where m.DueDate >= datenow && m.DueDate <= datenowPlusMonth
                             orderby m.DueDate
                             select m;
            List<Milestone> mList = milestones.ToList<Milestone>();
            return mList;
        }

        public List<Conference> GetConferencesThisWeek()
        {
            var datenow = DateTime.Now;
            var datenowPlusWeek = datenow.AddDays(7);
            var confs = from c in db.Conferences
                        where c.Date >= datenow && c.Date <= datenowPlusWeek
                        orderby c.Date
                        select c;
            List<Conference> confList = confs.ToList<Conference>();
            return confList;
        }

        public List<Conference> GetConferencesNextWeek()
        {
            var datenow = DateTime.Now;
            var datenowPlusWeek = datenow.AddDays(7);
            var datenowPlusTwoWeeks = datenow.AddDays(14);
            var confs = from c in db.Conferences
                        where c.Date >= datenowPlusWeek && c.Date <= datenowPlusTwoWeeks
                        orderby c.Date
                        select c;
            List<Conference> confList = confs.ToList<Conference>();
            return confList;
        }

        public List<Conference> GetConferencesThisMonth()
        {
            var datenow = DateTime.Now;
            var datenowPlusMonth = datenow.AddMonths(1);
            var confs = from c in db.Conferences
                        where c.Date >= datenow && c.Date <= datenowPlusMonth
                        orderby c.Date
                        select c;
            List<Conference> confList = confs.ToList<Conference>();
            return confList;
        }
    }
}
