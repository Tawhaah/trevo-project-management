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
    public class ClientController : Controller
    {
        private EntityModelContainer _dataModel = new EntityModelContainer(); 

        //
        // GET: /Client/

        public ActionResult Index()
        {
            return View(_dataModel.Clients.OrderBy(c => c.Name).ToList());
        }

        //
        // GET: /Client/Details/5

        public ActionResult Details(int id)
        {
            return View(GetClientByID(id));
        }

        //
        // GET: /Client/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Client/Create

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "ClientID")]Client newClient)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _dataModel.AddToClients(newClient);
                _dataModel.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Create", "Project");
            }
        }

        //
        // GET: /Client/CreateDialog

        [OutputCache(Duration = 0)]
        public ActionResult CreateDialog()
        {
            return PartialView(new Client());
        }

        //
        // POST: /Client/CreateDialog

        [HttpPost]
        public ActionResult CreateDialog(Client newClient)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _dataModel.AddToClients(newClient);
                _dataModel.SaveChanges();

                return RedirectToAction("Create", "Project");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Client/Edit/5
 
        public ActionResult Edit(int id)
        {
            try
            {
                return View(GetClientByID(id));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Client/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                UpdateModel(GetClientByID(id));
                _dataModel.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Client/Delete/5
 
        public ActionResult Delete(int id)
        {
            try
            {
                Client client = GetClientByID(id);

                if (client == null)
                    return RedirectToAction("Index");
                else
                    return View(client);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Client/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Client client = GetClientByID(id);

                if (client == null)
                    return RedirectToAction("Index");

                _dataModel.DeleteObject(client);
                _dataModel.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public Client GetClientByID(int id)
        {
            try
            {
                return _dataModel.Clients.Single(c => c.ClientID == id);
            }
            catch
            {
                return null;
            }
        }
    }
}
