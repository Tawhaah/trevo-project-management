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

namespace PrjctMngmt.Controllers
{
    [Authorize]
    public class MilestoneController : Controller
    {
        private EntityModelContainer _dataModel = new EntityModelContainer(); 

        //
        // GET: /Milestone/

        public ActionResult Index()
        {
            return View(_dataModel.Milestones.OrderBy(m => m.Name).ToList());
        }

        //
        // GET: /Mileston/ShowProjectMilestones/5

        public ActionResult ShowProjectMilestones(int id)
        {
            ViewBag.ProjectName = _dataModel.Projects.Single(p => p.ProjectID == id).Name;
            //get milestones that belong to project
            return View(GetMilestonesByProject(id));
        }

        //
        // GET: /Milestone/Details/5

        public ActionResult Details(int id)
        {
            return View(GetMilestoneByID(id));
        }

        //
        // GET: /Milestone/Create

        public ActionResult Create()
        {
            ViewData["Projects"] = new SelectList(_dataModel.Projects.OrderBy(p => p.Name), "ProjectID", "Name");
            return View(new Milestone());
        }

        //
        // POST: /Milestone/Create

        [HttpPost, ValidateInput(false)]
        public ActionResult Create([Bind(Exclude = "MilestoneID")]Milestone newMilestone)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _dataModel.AddToMilestones(newMilestone);
                _dataModel.SaveChanges();

                return RedirectToAction("Index", "Project");
            }
            catch
            {
                return RedirectToAction("Index", "Project");
            }
        }

        //
        // GET: /Milestone/CreateDialog

        [OutputCache(Duration = 0)]
        public ActionResult CreateDialog()
        {
            ViewData["Projects"] = new SelectList(_dataModel.Projects.OrderBy(p => p.Name), "ProjectID", "Name");
            return PartialView(new Milestone());
        }

        //
        // POST: /Milestone/CreateDialog

        [HttpPost, ValidateInput(false)]
        public ActionResult CreateDialog([Bind(Exclude = "MilestoneID")]Milestone newMilestone)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _dataModel.AddToMilestones(newMilestone);
                _dataModel.SaveChanges();

                return RedirectToAction("Create", "Issue");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Milestone/Edit/5
 
        public ActionResult Edit(int id)
        {
            ViewData["Projects"] = new SelectList(_dataModel.Projects.OrderBy(p => p.Name), "ProjectID", "Name");
            try
            {
                return View(GetMilestoneByID(id));
            }
            catch
            {
                return RedirectToAction("Index", "Project");
            }
        }

        //
        // POST: /Milestone/Edit/5

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                UpdateModel(GetMilestoneByID(id));
                _dataModel.SaveChanges();
                return RedirectToAction("Index", "Project");
            }
            catch
            {
                return RedirectToAction("Index", "Project");
            }
        }

        //
        // GET: /Milestone/Delete/5
 
        public ActionResult Delete(int id)
        {
            try
            {
                Milestone milestone = GetMilestoneByID(id);

                if (milestone == null)
                    return RedirectToAction("Index", "Project");
                else
                    return View(milestone);
            }
            catch
            {
                return RedirectToAction("Index", "Project");
            }
        }

        //
        // POST: /Milestone/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Milestone milestone = GetMilestoneByID(id);

                if (milestone == null)
                    return RedirectToAction("Index", "Project");

                _dataModel.DeleteObject(milestone);
                _dataModel.SaveChanges();

                return RedirectToAction("Index", "Project");
            }
            catch
            {
                return RedirectToAction("Index", "Project");
            }
        }

        public Milestone GetMilestoneByID(int id)
        {
            try
            {
                return _dataModel.Milestones.Single(m => m.MilestoneID == id);
            }
            catch
            {
                return null;
            }
        }

        public List<Milestone> GetMilestonesByProject(int id)
        {
            try
            {
                IEnumerable<Milestone> results = _dataModel.Milestones.Where(p => p.ProjectID == id).OrderBy(m => m.Name);
                return results.Cast<Milestone>().ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
