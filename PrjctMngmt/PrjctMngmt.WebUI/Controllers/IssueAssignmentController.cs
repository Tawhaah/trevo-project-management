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
    public class IssueAssignmentController : Controller
    {
        private EntityModelContainer _dataModel = new EntityModelContainer(); 

        //
        // GET: /IssueAssignment/

        public ActionResult Index()
        {
            return View(_dataModel.IssueAssignments.ToList());
        }

        //
        // GET: /IssueAssignment/ShowIssueAssignments/5

        public ActionResult ShowIssueAssignments(int id)
        {
            //get issue assignments that belong to issue id
            return View(GetIssueAssignmentsByIssue(id));
        }

        //
        // GET: /IssueAssignment/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /IssueAssignment/Create

        public ActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        } 

        //
        // POST: /IssueAssignment/Create

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "IssueAssignmentID")]IssueAssignment newIssueAssignment)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _dataModel.AddToIssueAssignments(newIssueAssignment);
                _dataModel.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        
        //
        // GET: /IssueAssignment/Edit/5
 
        public ActionResult Edit(int id)
        {
            PopulateDropDownLists();
            try
            {
                return View(GetIssueAssignmentByID(id));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /IssueAssignment/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                UpdateModel(GetIssueAssignmentByID(id));
                _dataModel.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /IssueAssignment/Delete/5
 
        public ActionResult Delete(int id)
        {
            try
            {
                IssueAssignment issueAsgnmt = GetIssueAssignmentByID(id);

                if (issueAsgnmt == null)
                    return RedirectToAction("Index");
                else
                    return View(issueAsgnmt);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /IssueAssignment/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                IssueAssignment issueAsgnmt = GetIssueAssignmentByID(id);

                if (issueAsgnmt == null)
                    return RedirectToAction("Index");

                _dataModel.DeleteObject(issueAsgnmt);
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
            ViewData["Issues"] = new SelectList(_dataModel.Issues.ToList(), "IssueID", "Summary");
            var devQuery = _dataModel.Developers.Select(d => new { d.DeveloperID, DeveloperName = d.FirstName + " " + d.LastName });
            ViewData["Developers"] = new SelectList(devQuery.AsEnumerable(), "DeveloperID", "DeveloperName");
        }

        public IssueAssignment GetIssueAssignmentByID(int id)
        {
            try
            {
                return _dataModel.IssueAssignments.Single(i => i.IssueAssignmentID == id);
            }
            catch
            {
                return null;
            }
        }

        public List<IssueAssignment> GetIssueAssignmentsByIssue(int id)
        {
            try
            {
                IEnumerable<IssueAssignment> results = _dataModel.IssueAssignments.Where(i => i.IssueID == id);
                return results.Cast<IssueAssignment>().ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
