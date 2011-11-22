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
using System.Web.Security;

namespace PrjctMngmt.Controllers
{
    public class DeveloperController : Controller
    {
        private EntityModelContainer _dataModel = new EntityModelContainer(); 

        //
        // GET: /Developer/

        public ActionResult Index()
        {
            List<Developer> developers = _dataModel.Developers.OrderBy(d => d.LastName)
                                         .ThenBy(d => d.LastName).ToList();
            return View(developers);
        }

        //
        // GET: /Developer/Details/5

        public ActionResult Details(int id)
        {
            return View(GetDeveloperByID(id));
        }

        //
        // GET: /Developer/Create

        public ActionResult Create()
        {
            SelectList teams = new SelectList(_dataModel.Teams, "TeamName", "TeamName");
            ViewData["Teams"] = teams;

            return View();
        } 

        //
        // POST: /Developer/Create

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "DeveloperID")]Developer newDev /*string FirstName, string LastName, string Email, string PhoneNumber, string Position, string TeamName*/)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                /*
                Developer dev = new Developer();
                dev.FirstName = FirstName;
                dev.LastName = LastName;
                dev.Email = Email;
                dev.PhoneNumber = PhoneNumber;
                dev.Position = Position;
                dev.TeamName = TeamName;
                 */

                newDev.UserName = Membership.GetUser().UserName;

                _dataModel.AddToDevelopers(newDev);
                _dataModel.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Developer/CreateDialog

        [OutputCache(Duration = 0)]
        public ActionResult CreateDialog()
        {
            SelectList teams = new SelectList(_dataModel.Teams, "TeamName", "TeamName");
            ViewData["Teams"] = teams;
            return PartialView(new Developer());
        }

        //
        // POST: /Developer/CreateDialog
        /*
        [HttpPost]
        public ActionResult CreateDialog(string FirstName, string LastName, string Email, string PhoneNumber, string Position, string TeamName)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                Developer dev = new Developer();
                dev.FirstName = FirstName;
                dev.LastName = LastName;
                dev.Email = Email;
                dev.PhoneNumber = PhoneNumber;
                dev.Position = Position;
                dev.TeamName = TeamName;
                dev.UserName = Membership.GetUser().UserName;
                _dataModel.AddToDevelopers(dev);
                _dataModel.SaveChanges();

                return View();
            }
            catch
            {
                return View();
            }
        }
        */

        //
        // GET: /Developer/Edit/5
 
        public ActionResult Edit(int id)
        {
            SelectList teams = new SelectList(_dataModel.Teams.OrderBy(t => t.TeamName), "TeamName", "TeamName");
            ViewData["Teams"] = teams;

            try
            {
                return View(GetDeveloperByID(id));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Developer/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                UpdateModel(GetDeveloperByID(id));
                _dataModel.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Developer/Delete/5
 
        public ActionResult Delete(int id)
        {
            try
            {
                Developer dev = GetDeveloperByID(id);

                if (dev == null)
                    return View();
                else
                    return View(dev);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Developer/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Developer dev = GetDeveloperByID(id);

                if (dev == null)
                    return RedirectToAction("Index");

                _dataModel.DeleteObject(dev);
                _dataModel.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult ChangeTeam(int devId, string teamName)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                Developer dev = GetDeveloperByID(devId);
                dev.TeamName = teamName;
                _dataModel.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public Developer GetDeveloperByID(int id)
        {
            try
            {
                return _dataModel.Developers.Single(d => d.DeveloperID == id);
            }
            catch
            {
                return null;
            }
        }
    }
}
