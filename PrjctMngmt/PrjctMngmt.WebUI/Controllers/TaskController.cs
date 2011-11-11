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
    public class TaskController : Controller
    {
        private EntityModelContainer _dataModel = new EntityModelContainer(); 

        //
        // GET: /Task/

        public ActionResult Index()
        {
            ViewData["Projects"] = _dataModel.Projects.ToList();
            return View(_dataModel.Tasks.ToList());
        }

        //
        // GET: /Task/Details/5

        public ActionResult Details(int id)
        {
            return View(GetTaskByID(id));
        }

        //
        // GET: /Task/Create

        public ActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        } 

        //
        // POST: /Task/Create

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "TaskID")]Task newTask)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _dataModel.AddToTasks(newTask);
                _dataModel.SaveChanges();
                
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        
        //
        // GET: /Task/Edit/5
 
        public ActionResult Edit(int id)
        {
            PopulateDropDownLists();
            try
            {
                return View(GetTaskByID(id));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Task task)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                UpdateModel(GetTaskByID(id));
                _dataModel.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Task/Delete/5
 
        public ActionResult Delete(int id)
        {
            try
            {
                Task task = GetTaskByID(id);

                if (task == null)
                    return RedirectToAction("Index");
                else
                    return View(task);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Task/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Task task = GetTaskByID(id);

                if (task == null)
                    return RedirectToAction("Index");

                _dataModel.DeleteObject(task);
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
            ViewData["TaskCategories"] = new SelectList(_dataModel.TaskCategories.ToList(), "Name", "Name");

            String[] statusStrings = { "Started", "Done", "Under review" };
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

        public Task GetTaskByID(int id)
        {
            try
            {
                return _dataModel.Tasks.Single(t => t.TaskID == id);
            }
            catch
            {
                return null;
            }
        }

        public ActionResult ToggleTaskStatus(int id)
        {
            try
            {
                Task task = GetTaskByID(id);
                if (task.Finished == 0)
                {
                    task.Finished = 1;
                }
                else
                {
                    task.Finished = 0;
                }
                UpdateModel(task);
                _dataModel.SaveChanges();
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
