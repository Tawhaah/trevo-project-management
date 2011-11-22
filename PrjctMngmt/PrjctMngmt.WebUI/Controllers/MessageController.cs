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
    public class MessageController : Controller
    {
        private EntityModelContainer _dataModel = new EntityModelContainer(); 

        //
        // GET: /Message/

        public ActionResult Index()
        {
            ViewData["Topics"] = _dataModel.Topics.OrderBy(t => t.Name).ToList();
            var messages = _dataModel.Messages.OrderByDescending(m => m.EntryDate).ThenByDescending(m => m.EditDate);
            return View(messages);
        }

        //
        // GET: /Message/Create

        public ActionResult Create()
        {
            ViewData["Topics"] = new SelectList(_dataModel.Topics.OrderBy(t => t.Name), "TopicID", "Name");

            return View();
        }

        //
        // POST: /Message/Create

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(string Data, int TopicID)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                Message msg = new Message();
                msg.Data = Data;
                msg.EntryDate = DateTime.Now;
                msg.DeveloperID = _dataModel.Developers.Single(d => d.UserName == User.Identity.Name).DeveloperID;
                msg.TopicID = TopicID;
                _dataModel.AddToMessages(msg);
                _dataModel.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Message/Details/5

        public ActionResult Details(int id)
        {
            return View(GetMessageByID(id));
        }

        //
        // GET: /Message/Edit/5
 
        public ActionResult Edit(int id)
        {
            ViewData["Topics"] = new SelectList(_dataModel.Topics.OrderBy(t => t.Name), "TopicID", "Name");
            try
            {
                return View(GetMessageByID(id));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Message/Edit/5

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id, string Data, int TopicID)
        {
            if (!ModelState.IsValid)
                return View();

            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            try
            {
                Message msg = GetMessageByID(id);
                msg.Data = Data;
                msg.EditDate = DateTime.Now;
                msg.TopicID = TopicID;
                _dataModel.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Message/Delete/5
 
        public ActionResult Delete(int id)
        {
            try
            {
                Message msg = GetMessageByID(id);
                return View(msg);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Message/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Message msg = GetMessageByID(id);
                _dataModel.DeleteObject(msg);
                _dataModel.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public Message GetMessageByID(int id)
        {
            try
            {
                return _dataModel.Messages.Single(m => m.MessageID == id);
            }
            catch
            {
                return null;
            }
        }
    }
}
