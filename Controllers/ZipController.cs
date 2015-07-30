using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZipCodeManager.Models;
using System.Data.Entity.Validation; 

namespace ZipCodeManager.Controllers
{
    public class ZipController : Controller
    {
        private DB_9D02D7_zipcodemanagerEntities db = new DB_9D02D7_zipcodemanagerEntities();

        //
        // GET: /Zip/
        //In the view we render a partial view and post the data to the create method on the Zip controller
        public ActionResult Index()
        {   
            /*
             *Create a simple list of states and populate for the view. We could have created a ViewModel and
             *put the list in there. For the sake of simplicity I used the ViewBag
             */
            ViewBag.stateList = new SelectList((from s in db.Zips.ToList() select s.State).Distinct().OrderBy(state => state),"State");
            return View(db.Zips.ToList());
        }
        
      
        //
        // GET: /Zip/Details/5

        public ActionResult Details(int id = 0)
        {
            Zip zip = db.Zips.Find(id);
            if (zip == null)
            {
                return HttpNotFound();
            }
            return View(zip);
        }

        //
        // GET: /Zip/Create
        /*
         * Since we use a partial view on the index method, we'll reuse it it.
         * If the user selects the 'create' hyperlink we'll just render that partial view again. This way we don't have two views with the same functionality
         */
        public ActionResult Create()
        {
            return View("_Create");
        }

        //
        // POST: /Zip/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Zip zip)
        {
            if (ModelState.IsValid)
            {
                db.Zips.Add(zip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(zip);
        }

        //
        // GET: /Zip/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Zip zip = db.Zips.Find(id);
            if (zip == null)
            {
                return HttpNotFound();
            }
            return View(zip);
        }

        //
        // POST: /Zip/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Zip zip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zip);
        }

        //
        // GET: /Zip/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Zip zip = db.Zips.Find(id);
            if (zip == null)
            {
                return HttpNotFound();
            }
            return View(zip);
        }

        //
        // POST: /Zip/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zip zip = db.Zips.Find(id);
            db.Zips.Remove(zip);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}