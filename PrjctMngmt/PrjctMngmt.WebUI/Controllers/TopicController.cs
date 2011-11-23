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
    public class TopicController : Controller
    {
        private EntityModelContainer _dataModel = new EntityModelContainer();

        //
        // GET: /Topic/

        public ActionResult Index()
        {
            return View(_dataModel.Topics.ToList());
        }

        //
        // GET: /Topic/Details/5

        public ActionResult Details(int id)
        {
            return View(GetTopicByID(id));
        }

        //
        // GET: /Topic/Create

        [OutputCache(Duration = 0)]
        public ActionResult Create()
        {
            return PartialView(new Topic());
        } 

        //
        // POST: /Topic/Create

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "TopicID")]Topic newTopic)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _dataModel.AddToTopics(newTopic);
                _dataModel.SaveChanges();

                return RedirectToAction("Index", "Message");
            }
            catch
            {
                return RedirectToAction("Index", "Message");
            }
        }
        
        //
        // GET: /Topic/Edit/5
 
        public ActionResult Edit(int id)
        {
            try
            {
                return View(GetTopicByID(id));
            }
            catch
            {
                return RedirectToAction("Index", "Message");
            }
        }

        //
        // POST: /Topic/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                UpdateModel(GetTopicByID(id));
                _dataModel.SaveChanges();
                return RedirectToAction("Index", "Message");
            }
            catch
            {
                return RedirectToAction("Index", "Message");
            }
        }

        //
        // GET: /Topic/Delete/5
 
        public ActionResult Delete(int id)
        {
            try
            {
                Topic topic = GetTopicByID(id);

                if (topic == null)
                    return RedirectToAction("Index", "Message");
                else
                    return View(topic);
            }
            catch
            {
                return RedirectToAction("Index", "Message");
            }
        }

        //
        // POST: /Topic/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Topic topic = GetTopicByID(id);

                if (topic == null)
                    return RedirectToAction("Index", "Message");

                _dataModel.DeleteObject(topic);
                _dataModel.SaveChanges();

                return RedirectToAction("Index", "Message");
            }
            catch
            {
                return RedirectToAction("Index", "Message");
            }
        }

        public Topic GetTopicByID(int id)
        {
            try
            {
                return _dataModel.Topics.Single(t => t.TopicID == id);
            }
            catch
            {
                return null;
            }
        }
    }
}
