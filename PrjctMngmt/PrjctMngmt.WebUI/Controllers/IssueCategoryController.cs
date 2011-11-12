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
    public class IssueCategoryController : Controller
    {
        private EntityModelContainer _dataModel = new EntityModelContainer(); 

        //
        // GET: /IssueCategory/

        public ActionResult Index()
        {
            return View(_dataModel.IssueCategories.ToList());
        }

        //
        // GET: /IssueCategory/Details/string

        public ActionResult Details(string name)
        {
            return View(GetIssueCategoryByName(name));
        }

        //
        // GET: /IssueCategory/Create

        [OutputCache(Duration = 0)]
        public ActionResult Create()
        {
            return PartialView(new IssueCategory());
        } 

        //
        // POST: /IssueCategory/Create

        [HttpPost]
        public ActionResult Create(string IssueCategoryName)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                IssueCategory issueCat = new IssueCategory();
                issueCat.IssueCategoryName = IssueCategoryName;
                _dataModel.AddToIssueCategories(issueCat);
                _dataModel.SaveChanges();

                return RedirectToAction("Create", "Issue");
            }
            catch
            {
                return RedirectToAction("Create", "Issue");
            }
        }
        
        //
        // GET: /IssueCategory/Edit/5
 
        public ActionResult Edit(string IssueCategoryName)
        {
            try
            {
                return View(GetIssueCategoryByName(IssueCategoryName));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /IssueCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(string IssueCategoryName, FormCollection collection)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                UpdateModel(GetIssueCategoryByName(IssueCategoryName));
                _dataModel.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /IssueCategory/Delete/5

        public ActionResult Delete(string IssueCategoryName)
        {
            try
            {
                IssueCategory issueCat = GetIssueCategoryByName(IssueCategoryName);

                if (issueCat == null)
                    return RedirectToAction("Index");
                else
                    return View(issueCat);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /IssueCategory/Delete/5

        [HttpPost]
        public ActionResult Delete(string IssueCategoryName, FormCollection collection)
        {
            try
            {
                IssueCategory issueCat = GetIssueCategoryByName(IssueCategoryName);

                if (issueCat == null)
                    return RedirectToAction("Index");

                _dataModel.DeleteObject(issueCat);
                _dataModel.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public IssueCategory GetIssueCategoryByName(string IssueCategoryName)
        {
            try
            {
                return _dataModel.IssueCategories.Single(i => i.IssueCategoryName == IssueCategoryName);
            }
            catch
            {
                return null;
            }
        }
    }
}
