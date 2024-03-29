﻿/*
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
using System.Web.Configuration;

namespace PrjctMngmt.Controllers
{
    [Authorize]
    public class TaskAssignmentController : Controller
    {
        private EntityModelContainer _dataModel = new EntityModelContainer(); 

        //
        // GET: /TaskAssignment/

        public ActionResult Index()
        {
            return View(_dataModel.TaskAssignments.ToList());
        }

        //
        // GET: /TaskAssignment/ShowTaskAssignments/5

        public ActionResult ShowTaskAssignments(int id)
        {
            ViewBag.TaskName = _dataModel.Tasks.Single(t => t.TaskID == id).Name;
            //get task assignments that belong to task id
            return View(GetTaskAssignmentsByTask(id));
        }

        //
        // GET: /TaskAssignment/Details/5

        public ActionResult Details(int id)
        {
            return View(GetTaskAssignmentByID(id));
        }

        //
        // GET: /TaskAssignment/Create

        public ActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        } 

        //
        // POST: /TaskAssignment/Create

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "TaskAssignmentID")]TaskAssignment newTaskAssignment)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _dataModel.AddToTaskAssignments(newTaskAssignment);
                _dataModel.SaveChanges();

                return RedirectToAction("Index", "Task");
            }
            catch
            {
                return RedirectToAction("Index", "Task");
            }
        }
        
        //
        // GET: /TaskAssignment/Edit/5
 
        public ActionResult Edit(int id)
        {
            PopulateDropDownLists();
            try
            {
                return View(GetTaskAssignmentByID(id));
            }
            catch
            {
                return RedirectToAction("Index", "Task");
            }
        }

        //
        // POST: /TaskAssignment/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                UpdateModel(GetTaskAssignmentByID(id));
                _dataModel.SaveChanges();
                return RedirectToAction("Index", "Task");
            }
            catch
            {
                return RedirectToAction("Index", "Task");
            }
        }

        //
        // GET: /TaskAssignment/Delete/5
 
        public ActionResult Delete(int id)
        {
            try
            {
                TaskAssignment taskAsgmnt = GetTaskAssignmentByID(id);

                if (taskAsgmnt == null)
                    return RedirectToAction("Index", "Task");
                else
                    return View(taskAsgmnt);
            }
            catch
            {
                return RedirectToAction("Index", "Task");
            }
        }

        //
        // POST: /TaskAssignment/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                TaskAssignment taskAsgnmt = GetTaskAssignmentByID(id);

                if (taskAsgnmt == null)
                    return RedirectToAction("Index", "Task");

                _dataModel.DeleteObject(taskAsgnmt);
                _dataModel.SaveChanges();

                return RedirectToAction("Index", "Task");
            }
            catch
            {
                return RedirectToAction("Index", "Task");
            }
        }

        public void PopulateDropDownLists()
        {
            ViewData["Tasks"] = new SelectList(_dataModel.Tasks.OrderBy(t => t.Name), "TaskID", "Name");
            var devQuery = _dataModel.Developers.Select(d => new { d.DeveloperID, DeveloperName = d.FirstName + " " + d.LastName }).OrderBy(d => d.DeveloperName);
            ViewData["Developers"] = new SelectList(devQuery.AsEnumerable(), "DeveloperID", "DeveloperName");
        }

        public TaskAssignment GetTaskAssignmentByID(int id)
        {
            try
            {
                return _dataModel.TaskAssignments.Single(t => t.TaskAssignmentID == id);
            }
            catch
            {
                return null;
            }
        }

        public List<TaskAssignment> GetTaskAssignmentsByTask(int id)
        {
            try
            {
                IEnumerable<TaskAssignment> results = _dataModel.TaskAssignments.Where(t => t.TaskID == id);
                return results.Cast<TaskAssignment>().ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
