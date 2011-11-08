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
    public class ProjectAssignmentController : Controller
    {
        private EntityModelContainer _dataModel = new EntityModelContainer(); 

        //
        // GET: /ProjectAssignment/

        public ActionResult Index()
        {
            PopulateDropDownLists();
            return View(_dataModel.ProjectAssignments.ToList());
        }

        //
        // GET: /ProjectAssignment/ShowProjectAssignments/5

        public ActionResult ShowProjectAssignments(int id)
        {
            //get project assignments that belong to project id
            return View(GetProjectAssignmentsByProject(id));
        }

        //
        // GET: /ProjectAssignment/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ProjectAssignment/Create

        public ActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        } 

        //
        // POST: /ProjectAssignment/Create

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "ProjectAssignmentID")]ProjectAssignment newProjAsgnmt)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _dataModel.AddToProjectAssignments(newProjAsgnmt);
                _dataModel.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        
        //
        // GET: /ProjectAssignment/Edit/5
 
        public ActionResult Edit(int id)
        {
            PopulateDropDownLists();
            try
            {
                return View(GetProjectAssignmentByID(id));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /ProjectAssignment/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, ProjectAssignment projAsgnmt)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                UpdateModel(GetProjectAssignmentByID(id));
                _dataModel.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /ProjectAssignment/Delete/5
 
        public ActionResult Delete(int id)
        {
            try
            {
                ProjectAssignment projAsgnmt = GetProjectAssignmentByID(id);

                if (projAsgnmt == null)
                    return RedirectToAction("Index");
                else
                    return View(projAsgnmt);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /ProjectAssignment/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ProjectAssignment projAsgnmt = GetProjectAssignmentByID(id);

                if (projAsgnmt == null)
                    return RedirectToAction("Index");

                _dataModel.DeleteObject(projAsgnmt);
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
            ViewData["Projects"] = new SelectList(_dataModel.Projects.ToList(), "ProjectID", "Name");
            var devQuery = _dataModel.Developers.Select(d => new { d.DeveloperID, DeveloperName = d.FirstName + " " + d.LastName });
            ViewData["Developers"] = new SelectList(devQuery.AsEnumerable(), "DeveloperID", "DeveloperName");
        }

        public ProjectAssignment GetProjectAssignmentByID(int id)
        {
            try
            {
                return _dataModel.ProjectAssignments.Single(p => p.ProjectAssignmentID == id);
            }
            catch
            {
                return null;
            }
        }

        public List<ProjectAssignment> GetProjectAssignmentsByProject(int id)
        {
            try
            {
                IEnumerable<ProjectAssignment> results = _dataModel.ProjectAssignments.Where(p => p.ProjectID == id);
                return results.Cast<ProjectAssignment>().ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
