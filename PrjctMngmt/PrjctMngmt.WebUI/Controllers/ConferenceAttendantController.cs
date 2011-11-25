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
    [Authorize]
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
            ViewBag.ConferenceID = new SelectList(db.Conferences.OrderBy(d => d.Name), "ConferenceID", "Name");
            ViewBag.DeveloperID = new SelectList(db.Developers.Select(d => new { d.DeveloperID, DeveloperName = d.FirstName + " " + d.LastName }).OrderBy(d => d.DeveloperName), "DeveloperID", "DeveloperName");
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
            ViewBag.Conferences = new SelectList(db.Conferences.OrderBy(d => d.Name), "ConferenceID", "Name");
            ViewBag.Developers = new SelectList(db.Developers.Select(d => new { d.DeveloperID, DeveloperName = d.FirstName + " " + d.LastName }).OrderBy(d => d.DeveloperName), "DeveloperID", "DeveloperName");
            ViewBag.ConferenceName = ConferenceAttendant.Conference.Name;
            ViewBag.DeveloperName = ConferenceAttendant.Developer.FirstName + " " + ConferenceAttendant.Developer.LastName;
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
            ViewBag.ConferenceName = db.Conferences.Single(c => c.ConferenceID == id).Name;
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