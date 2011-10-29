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
    public class TeamController : Controller
    {
        private EntityModelContainer _dataModel = new EntityModelContainer(); 

        //
        // GET: /Team/

        public ActionResult Index()
        {
            List<Developer> devs = _dataModel.Developers.ToList();
            List<string> devNames = new List<string>();
            foreach (Developer dev in devs)
            {
                devNames.Add(dev.FirstName+" "+dev.LastName);
            }
            ViewData["Developers"] = devNames;
            return View(_dataModel.Teams.ToList());
        }

        //
        // GET: /Team/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Team/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Team/Create

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "TeamID")]Team newTeam)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _dataModel.AddToTeams(newTeam);
                _dataModel.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        
        //
        // GET: /Team/Edit/5
 
        public ActionResult Edit(string TeamName)
        {
            try
            {
                return View(GetTeamByName(TeamName));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Team/Edit/5

        [HttpPost]
        public ActionResult Edit(string TeamName, FormCollection collection)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                UpdateModel(GetTeamByName(TeamName));
                _dataModel.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Team/Delete/5
 
        public ActionResult Delete(string TeamName)
        {
            try
            {
                Team team = GetTeamByName(TeamName);

                if (team == null)
                    return RedirectToAction("Index");
                else
                    return View(team);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Team/Delete/5

        [HttpPost]
        public ActionResult Delete(string TeamName, FormCollection collection)
        {
            try
            {
                Team team = GetTeamByName(TeamName);

                if (team == null)
                    return RedirectToAction("Index");

                _dataModel.DeleteObject(team);
                _dataModel.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public Team GetTeamByName(string TeamName)
        {
            try
            {
                return _dataModel.Teams.Single(t => t.TeamName == TeamName);
            }
            catch
            {
                return null;
            }
        }

        public JsonResult DevelopersByTeam(string TeamName)
        {
            /*
            List<Developer> teamList = new List<Developer>();
            List<Developer> developers = _dataModel.Developers.ToList();
            foreach (Developer dev in developers)
            {
                if (dev.TeamName == TeamName)
                {
                    teamList.Add(dev);
                }
            }
            */
            
            var developers = _dataModel.Developers
                .Where(d => d.TeamName.Contains(TeamName))
                .Select(d => new { label = d.FirstName + " " + d.LastName });

            return Json(developers, JsonRequestBehavior.AllowGet);
        }
    }
}
