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
    public class ProjectController : Controller
    {
        private EntityModelContainer _dataModel = new EntityModelContainer(); 

        //
        // GET: /Project/

        public ActionResult Index()
        {
            return View(_dataModel.Projects.OrderBy(p => p.Name).ToList());
        }

        //
        // GET: /Project/Details/5

        public ActionResult Details(int id)
        {
            return View(GetProjectByID(id));
        }

        //
        // GET: /Project/Create

        public ActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        } 

        //
        // POST: /Project/Create

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "ProjectID")]Project newProj)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _dataModel.AddToProjects(newProj);
                _dataModel.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        
        //
        // GET: /Project/Edit/5
 
        public ActionResult Edit(int id)
        {
            PopulateDropDownLists();
            try
            {
                return View(GetProjectByID(id));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Project/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Project proj)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                UpdateModel(GetProjectByID(id));
                _dataModel.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Project/Delete/5
 
        public ActionResult Delete(int id)
        {
            try
            {
                Project proj = GetProjectByID(id);

                if (proj == null)
                    return RedirectToAction("Index");
                else
                    return View(proj);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Project/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Project proj = GetProjectByID(id);

                if (proj == null)
                    return RedirectToAction("Index");

                _dataModel.DeleteObject(proj);
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
            SelectList clients = new SelectList(_dataModel.Clients.ToList(), "ClientID", "Name");
            ViewData["Clients"] = clients;

            SelectList projCatgrs = new SelectList(_dataModel.ProjectCategories.ToList(), "ProjectCategoryID", "Name");
            ViewData["ProjectCategories"] = projCatgrs;

            String[] statusStrings = { "Started", "Finished", "Under review", "Approved", "Testing" };
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

        public Project GetProjectByID(int id)
        {
            try
            {
                return _dataModel.Projects.Single(p => p.ProjectID == id);
            }
            catch
            {
                return null;
            }
        }
    }
}
