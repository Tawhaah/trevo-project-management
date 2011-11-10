/*
Copyright (c) 2011 Petri Tuononen

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be included
in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrjctMngmt.Models;

namespace PrjctMngmt.Controllers
{
    public class IssueController : Controller
    {
        private EntityModelContainer _dataModel = new EntityModelContainer(); 

        //
        // GET: /Issue/

        public ActionResult Index()
        {
            return View(_dataModel.Issues.ToList());
        }

        //
        // GET: /Issue/Details/5

        public ActionResult Details(int id)
        {
            return View(GetIssueByID(id));
        }

        //
        // GET: /Issue/Create

        public ActionResult Create()
        {
            PopulateDropDownLists();

            return View();
        } 
        
        [HttpPost]
        public ActionResult Create(string Summary, string Priority, string Severity, string Status,
            string Description, string IssueCategoryName, int? MilestoneID, int ProjectID)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                Issue issue = new Issue();
                issue.Summary = Summary;
                issue.Priority = Priority;
                issue.Severity = Severity;
                issue.Status = Status;
                issue.Description = Description;
                issue.EntryDate = DateTime.Now;
                issue.IssueCategoryName = IssueCategoryName;
                issue.MilestoneID = MilestoneID;
                issue.ProjectID = ProjectID;
                _dataModel.AddToIssues(issue);
                _dataModel.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        

        //
        // GET: /Issue/Edit/5
 
        public ActionResult Edit(int id)
        {
            PopulateDropDownLists();
            try
            {
                return View(GetIssueByID(id));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Issue/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                UpdateModel(GetIssueByID(id));
                _dataModel.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Issue/Delete/5
 
        public ActionResult Delete(int id)
        {
            try
            {
                Issue issue = GetIssueByID(id);

                if (issue == null)
                    return RedirectToAction("Index");
                else
                    return View(issue);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Issue/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Issue issue = GetIssueByID(id);

                if (issue == null)
                    return RedirectToAction("Index");

                _dataModel.DeleteObject(issue);
                _dataModel.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public void PopulateDropDownLists()
        {
            SelectList issues = new SelectList(_dataModel.IssueCategories.ToList(), "IssueCategoryName", "IssueCategoryName");
            ViewData["IssueCategories"] = issues;

            SelectList milestones = new SelectList(_dataModel.Milestones.ToList(), "MilestoneID", "Name");
            ViewData["Milestones"] = milestones;

            var devQuery = _dataModel.Developers.Select(d => new { d.DeveloperID, DeveloperName = d.FirstName + " " + d.LastName });
            SelectList devs = new SelectList(devQuery.AsEnumerable(), "DeveloperID", "DeveloperName");
            ViewData["Developers"] = devs;

            SelectList projects = new SelectList(_dataModel.Projects.ToList(), "ProjectID", "Name");
            ViewData["Projects"] = projects;

            String[] statusStrings = { "Fixed", "Duplicate", "Wont_fix", "Works_for_me", "Invalid" };
            List<SelectListItem> statusItems = new List<SelectListItem>();
            foreach (String s in statusStrings)
            {
                statusItems.Add(new SelectListItem
                {
                    Text = s,
                    Value = s
                });
            }
            ViewData["Status"] = statusItems;
        }

        public Issue GetIssueByID(int id)
        {
            try
            {
                return _dataModel.Issues.Single(i => i.IssueID == id);
            }
            catch
            {
                return null;
            }
        }
    }
}
