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
    public class ProjectCategoryController : Controller
    {
        private EntityModelContainer _dataModel = new EntityModelContainer(); 

        //
        // GET: /ProjectCategory/

        public ActionResult Index()
        {
            return View(_dataModel.ProjectCategories.ToList());
        }

        //
        // GET: /ProjectCategory/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ProjectCategory/Create

        [OutputCache(Duration = 0)]
        public ActionResult Create()
        {
            return PartialView(new ProjectCategory());
        }

        //
        // POST: /ProjectCategory/Create

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "ProjectCategoryID")]ProjectCategory newProjCategory)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _dataModel.AddToProjectCategories(newProjCategory);
                _dataModel.SaveChanges();

                return RedirectToAction("Create", "Project");
            }
            catch
            {
                return RedirectToAction("Create", "Project");
            }
        }
        
        //
        // GET: /ProjectCategory/Edit/5
 
        public ActionResult Edit(int id)
        {
            try
            {
                return View(GetProjectCategoryByID(id));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /ProjectCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                UpdateModel(GetProjectCategoryByID(id));
                _dataModel.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /ProjectCategory/Delete/5
 
        public ActionResult Delete(int id)
        {
            try
            {
                ProjectCategory projCat = GetProjectCategoryByID(id);

                if (projCat == null)
                    return RedirectToAction("Index");
                else
                    return View(projCat);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /ProjectCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ProjectCategory projCat = GetProjectCategoryByID(id);

                if (projCat == null)
                    return RedirectToAction("Index");

                _dataModel.DeleteObject(projCat);
                _dataModel.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ProjectCategory GetProjectCategoryByID(int id)
        {
            try
            {
                return _dataModel.ProjectCategories.Single(p => p.ProjectCategoryID == id);
            }
            catch
            {
                return null;
            }
        }
    }
}
