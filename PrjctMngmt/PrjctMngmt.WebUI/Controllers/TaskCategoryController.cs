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
    public class TaskCategoryController : Controller
    {
        private EntityModelContainer _dataModel = new EntityModelContainer(); 

        //
        // GET: /TaskCategory/

        public ActionResult Index()
        {
            return View(_dataModel.TaskCategories.OrderBy(t => t.Name).ToList());
        }

        //
        // GET: /TaskCategory/Details/string

        public ActionResult Details(string name)
        {
            return View(GetTaskCategoryByName(name));
        }

        //
        // GET: /TaskCategory/Create

        [OutputCache(Duration = 0)]
        public ActionResult Create()
        {
            return PartialView(new TaskCategory());
        }

        //
        // POST: /TaskCategory/Create

        [HttpPost]
        public ActionResult Create(TaskCategory newTaskCategory)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _dataModel.AddToTaskCategories(newTaskCategory);
                _dataModel.SaveChanges();

                return RedirectToAction("Create", "Task");
            }
            catch
            {
                return RedirectToAction("Create", "Task");
            }
        }
        
        //
        // GET: /TaskCategory/Edit/5

        public ActionResult Edit(string TaskCategoryName)
        {
            try
            {
                return View(GetTaskCategoryByName(TaskCategoryName));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /TaskCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(string TaskCategoryName, TaskCategory taskCategory)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                UpdateModel(GetTaskCategoryByName(TaskCategoryName));
                _dataModel.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /TaskCategory/Delete/5
 
        public ActionResult Delete(string TaskCategoryName)
        {
            try
            {
                TaskCategory taskCat = GetTaskCategoryByName(TaskCategoryName);

                if (taskCat == null)
                    return RedirectToAction("Index");
                else
                    return View(taskCat);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /TaskCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(string TaskCategoryName, FormCollection collection)
        {
            try
            {
                TaskCategory taskcat = GetTaskCategoryByName(TaskCategoryName);

                if (taskcat == null)
                    return RedirectToAction("Index");

                _dataModel.DeleteObject(taskcat);
                _dataModel.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public TaskCategory GetTaskCategoryByName(string TaskCategoryName)
        {
            try
            {
                return _dataModel.TaskCategories.Single(t => t.Name == TaskCategoryName);
            }
            catch
            {
                return null;
            }
        }
    }
}
