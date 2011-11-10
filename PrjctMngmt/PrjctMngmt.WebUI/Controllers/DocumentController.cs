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
using System.IO;

namespace PrjctMngmt.Controllers
{
    public class DocumentController : Controller
    {
        private EntityModelContainer _dataModel = new EntityModelContainer();

        private static string basePath = AppDomain.CurrentDomain.BaseDirectory + "Uploads\\Documents\\";

        //
        // GET: /Document/

        public ActionResult Index()
        {
            return View(_dataModel.Documents.ToList());
        }

        //
        // GET: /Document/Details/5

        public ActionResult Details(int id)
        {
            return View(GetDocumentByID(id));
        }

        //
        // GET: /Document/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Document/Create

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "DocumentID")]Document newDoc, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                //Save file to server if user selected a file
                if (file != null && file.ContentLength > 0)
                {
                    newDoc.FileName = Path.GetFileName(file.FileName);
                    newDoc.MimeType = file.ContentType;
                    var path = Path.Combine(basePath, newDoc.FileName);
                    file.SaveAs(path);
                }

                newDoc.DeveloperID = 1; //TODO: Change to dynamic
                newDoc.Name = newDoc.Name;
                newDoc.EntryDate = DateTime.Now;
                _dataModel.AddToDocuments(newDoc);
                _dataModel.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        
        //
        // GET: /Document/Edit/5
 
        public ActionResult Edit(int id)
        {
            try
            {
                return View(GetDocumentByID(id));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Document/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                UpdateModel(GetDocumentByID(id));
                _dataModel.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Document/Delete/5
 
        public ActionResult Delete(int id)
        {
            try
            {
                Document doc = GetDocumentByID(id);

                if (doc == null)
                    return RedirectToAction("Index");
                else
                    return View(doc);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Document/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Document doc = GetDocumentByID(id);

                if (doc == null)
                    return RedirectToAction("Index");

                //delete entry from database
                _dataModel.DeleteObject(doc);
                _dataModel.SaveChanges();

                //delete file from the server
                FileInfo docFile = new FileInfo(basePath + doc.FileName);
                docFile.Delete();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public FilePathResult GetFile(int id)
        {
            Document doc = GetDocumentByID(id);
            if (doc != null)
            {
                try
                {
                    string filename = doc.FileName;
                    return File(basePath + filename, doc.MimeType);
                }
                catch
                {
                    //TODO: log error - file no longer exists on server
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public Document GetDocumentByID(int id)
        {
            try
            {
                return _dataModel.Documents.Single(d => d.DocumentID == id);
            }
            catch
            {
                return null;
            }
        }
    }
}
