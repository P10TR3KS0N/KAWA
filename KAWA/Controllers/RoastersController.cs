using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoffeeAppMvc5.Models;

namespace KAWA.Controllers
{
    public class RoastersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Roasters
        public ActionResult Index()
        {
            var roasters = db.Roasters.Include(r => r.City);
            return View(roasters.ToList());
        }

        // GET: Roasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roaster roaster = db.Roasters.Find(id);
            if (roaster == null)
            {
                return HttpNotFound();
            }
            return View(roaster);
        }

        // GET: Roasters/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name");
            return View();
        }

        // POST: Roasters/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CityId")] Roaster roaster)
        {
            if (ModelState.IsValid)
            {
                db.Roasters.Add(roaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", roaster.CityId);
            return View(roaster);
        }

        // GET: Roasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roaster roaster = db.Roasters.Find(id);
            if (roaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", roaster.CityId);
            return View(roaster);
        }

        // POST: Roasters/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CityId")] Roaster roaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", roaster.CityId);
            return View(roaster);
        }

        // GET: Roasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roaster roaster = db.Roasters.Find(id);
            if (roaster == null)
            {
                return HttpNotFound();
            }
            return View(roaster);
        }

        // POST: Roasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Roaster roaster = db.Roasters.Find(id);
            db.Roasters.Remove(roaster);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
