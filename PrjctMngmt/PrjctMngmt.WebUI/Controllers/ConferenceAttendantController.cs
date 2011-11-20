using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrjctMngmt.Models;

namespace PrjctMngmt.Controllers
{ 
    public class ConferenceAttendantController : Controller
    {
        private EntityModelContainer db = new EntityModelContainer();

        //
        // GET: /ConferenceAttendants/

        public ViewResult Index()
        {
            var ConferenceAttendants = db.ConferenceAttendants.Include("Conference").Include("Developer");
            return View(ConferenceAttendants.ToList());
        }

        //
        // GET: /ConferenceAttendants/Create

        public ActionResult Create()
        {
            ViewBag.ConferenceID = new SelectList(db.Conferences, "ConferenceID", "Name");
            ViewBag.DeveloperID = new SelectList(db.Developers, "DeveloperID", "FirstName");
            return View();
        } 

        //
        // POST: /ConferenceAttendants/Create

        [HttpPost]
        public ActionResult Create(ConferenceAttendant ConferenceAttendant)
        {
            if (ModelState.IsValid)
            {
                db.ConferenceAttendants.AddObject(ConferenceAttendant);
                db.SaveChanges();
                return RedirectToAction("Index", "Conference");  
            }
            return View();
        }
        
        //
        // GET: /ConferenceAttendants/Edit/5
 
        public ActionResult Edit(int id)
        {
            ConferenceAttendant ConferenceAttendant = db.ConferenceAttendants.Single(c => c.ID == id);
            ViewBag.ConferenceID = new SelectList(db.Conferences, "ConferenceID", "Name", ConferenceAttendant.ConferenceID);
            ViewBag.DeveloperID = new SelectList(db.Developers, "DeveloperID", "FirstName", ConferenceAttendant.DeveloperID);
            return View(ConferenceAttendant);
        }

        //
        // POST: /ConferenceAttendants/Edit/5

        [HttpPost]
        public ActionResult Edit(ConferenceAttendant ConferenceAttendant)
        {
            if (ModelState.IsValid)
            {
                db.ConferenceAttendants.Attach(ConferenceAttendant);
                db.ObjectStateManager.ChangeObjectState(ConferenceAttendant, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index", "Conference");
            }
            return View();
        }

        //
        // GET: /ConferenceAttendants/Delete/5
 
        public ActionResult Delete(int id)
        {
            ConferenceAttendant ConferenceAttendant = db.ConferenceAttendants.Single(c => c.ID == id);
            return View(ConferenceAttendant);
        }

        //
        // POST: /ConferenceAttendants/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ConferenceAttendant ConferenceAttendant = db.ConferenceAttendants.Single(c => c.ID == id);
            db.ConferenceAttendants.DeleteObject(ConferenceAttendant);
            db.SaveChanges();
            return RedirectToAction("Index", "Conference");
        }

        public ActionResult SeeAttendants(int id)
        {
            return View(GetConferenceAttendantsByConference(id));
        }

        public List<ConferenceAttendant> GetConferenceAttendantsByConference(int id)
        {
            try
            {
                IEnumerable<ConferenceAttendant> results = db.ConferenceAttendants.Where(c => c.ConferenceID == id);
                return results.Cast<ConferenceAttendant>().ToList();
            }
            catch
            {
                return null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}