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
using System.IO;

namespace PrjctMngmt.Controllers
{
    public class IssueController : Controller
    {
        private EntityModelContainer _dataModel = new EntityModelContainer();
        private static string basePath = AppDomain.CurrentDomain.BaseDirectory + "Uploads\\IssueAttachments\\";

        //
        // GET: /Issue/

        public ActionResult Index()
        {
            ViewData["Projects"] = _dataModel.Projects.OrderBy(p => p.Name).ToList();
            //.OrderBy (i => (i.Priority == "Critical") ? 1 : (i.Priority == "High") ? 2 : (i.Priority == "Normal") ? 3 : (i.Priority == "Low") ? 4 : (i.Priority == "Trivial") ? 5)
            return View(_dataModel.Issues.OrderBy(i => i.ProjectID).ToList());
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
        public ActionResult Create(string Subject, string Priority, string Severity, string Status,
            string Description, string IssueCategoryName, int? MilestoneID, int ProjectID, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                Issue issue = new Issue();
                issue.Subject = Subject;
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

                //Save file to server if user selected a file
                if (file != null && file.ContentLength > 0)
                {
                    IssueAttachment issueAttchmnt = new IssueAttachment();
                    issueAttchmnt.Filename = Path.GetFileName(file.FileName);
                    issueAttchmnt.MimeType = file.ContentType;
                    var path = Path.Combine(basePath, issueAttchmnt.Filename);
                    file.SaveAs(path);
                    issueAttchmnt.DeveloperID = _dataModel.Developers.Single(d => d.UserName == User.Identity.Name).DeveloperID;
                    issueAttchmnt.EntryDate = DateTime.Now;
                    issueAttchmnt.IssueID = issue.IssueID;
                    _dataModel.AddToIssueAttachments(issueAttchmnt);
                    _dataModel.SaveChanges();
                }
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
        public ActionResult Edit(int id, HttpPostedFileBase file, FormCollection collection)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                UpdateModel(GetIssueByID(id));
                _dataModel.SaveChanges();

                //Save file to server if user selected a file
                if (file != null && file.ContentLength > 0)
                {
                    //remove old file first
                    IssueAttachment issueAttcmt = GetIssueAttachmentByIssueID(id);
                    var path = Path.Combine(basePath, issueAttcmt.Filename);
                    if (issueAttcmt != null && System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    //save new file
                    issueAttcmt.Filename = Path.GetFileName(file.FileName);
                    issueAttcmt.MimeType = file.ContentType;
                    path = Path.Combine(basePath, issueAttcmt.Filename);
                    file.SaveAs(path);

                    issueAttcmt.EntryDate = DateTime.Now;
                    issueAttcmt.DeveloperID = _dataModel.Developers.Single(d => d.UserName == User.Identity.Name).DeveloperID;

                    _dataModel.SaveChanges();
                }
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

            String[] statusStrings = { "New", "Started", "Fixed", "Done", "Duplicate", "WontFix", "Invalid" };
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

            String[] priorityStrings = { "Trivial", "Low", "Normal", "High", "Critical" };
            List<SelectListItem> priorityItems = new List<SelectListItem>();
            //int i = 1;
            foreach (String p in priorityStrings)
            {
                priorityItems.Add(new SelectListItem
                {
                    Text = p,
                    Value = p/*i.ToString()*/
                });
                //i--;
            }
            ViewData["Priority"] = priorityItems;
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

        public IssueAttachment GetIssueAttachmentByIssueID(int id)
        {
            try
            {
                return _dataModel.IssueAttachments.Single(i => i.IssueID == id);
            }
            catch
            {
                return null;
            }
        }
    }
}
