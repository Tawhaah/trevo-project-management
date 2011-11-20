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
    public class NoteController : Controller
    {
        private EntityModelContainer _dataModel = new EntityModelContainer(); 

        //
        // GET: /Note/

        public ActionResult Index()
        {
            ViewData["Projects"] = _dataModel.Projects.OrderBy(p => p.Name).ToList();
            return View(_dataModel.Notes.ToList());
        }

        //
        // GET: /Note/Details/5

        public ActionResult Details(int id)
        {
            return View(GetNoteByID(id));
        }

        //
        // GET: /Note/Create

        public ActionResult Create()
        {
            ViewData["Projects"] = new SelectList(_dataModel.Projects.ToList(), "ProjectID", "Name");
            return View();
        } 

        //
        // POST: /Note/Create

        [HttpPost]
        public ActionResult Create(String Data, string LocX, string LocY, int ProjectID)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                Note note = new Note();
                note.Data = Data;
                note.LocX = LocX;
                note.LocY = LocY;
                note.ProjectID = ProjectID;
                note.DeveloperID = _dataModel.Developers.Single(d => d.UserName == User.Identity.Name).DeveloperID;

                _dataModel.AddToNotes(note);
                _dataModel.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        
        //
        // GET: /Note/Edit/5
 
        public ActionResult Edit(int id)
        {
            ViewData["Projects"] = new SelectList(_dataModel.Projects.ToList(), "ProjectID", "Name");
            try
            {
                return View(GetNoteByID(id));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Note/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                UpdateModel(GetNoteByID(id));
                _dataModel.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Note/Delete/5
 
        public ActionResult Delete(int id)
        {
            try
            {
                Note note = GetNoteByID(id);

                if (note == null)
                    return RedirectToAction("Index");
                else
                    return View(note);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Note/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Note note = GetNoteByID(id);

                if (note == null)
                    return RedirectToAction("Index");

                _dataModel.DeleteObject(note);
                _dataModel.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public void UpdateLocation(int id, string LocX, string LocY)
        {
            try
            {
                Note note = GetNoteByID(id);
                note.LocX = LocX;
                note.LocY = LocY;
                _dataModel.SaveChanges();
            }
            catch
            {
            }
        }

        public Note GetNoteByID(int id)
        {
            try
            {
                return _dataModel.Notes.Single(n => n.NoteID == id);
            }
            catch
            {
                return null;
            }
        }
    }
}
