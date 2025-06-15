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
    public class CoffeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Coffees
        public ActionResult Index()
        {
            var coffees = db.Coffees.Include(c => c.Bean).Include(c => c.Roaster);
            return View(coffees.ToList());
        }

        // GET: Coffees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coffee coffee = db.Coffees.Find(id);
            if (coffee == null)
            {
                return HttpNotFound();
            }
            return View(coffee);
        }

        // GET: Coffees/Create
        public ActionResult Create()
        {
            ViewBag.BeanId = new SelectList(db.CoffeeBeans, "Id", "Name");
            ViewBag.RoasterId = new SelectList(db.Roasters, "Id", "Name");
            return View();
        }

        // POST: Coffees/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,RoastLevel,Price,BeanId,RoasterId")] Coffee coffee)
        {
            if (ModelState.IsValid)
            {
                db.Coffees.Add(coffee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BeanId = new SelectList(db.CoffeeBeans, "Id", "Name", coffee.BeanId);
            ViewBag.RoasterId = new SelectList(db.Roasters, "Id", "Name", coffee.RoasterId);
            return View(coffee);
        }

        // GET: Coffees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coffee coffee = db.Coffees.Find(id);
            if (coffee == null)
            {
                return HttpNotFound();
            }
            ViewBag.BeanId = new SelectList(db.CoffeeBeans, "Id", "Name", coffee.BeanId);
            ViewBag.RoasterId = new SelectList(db.Roasters, "Id", "Name", coffee.RoasterId);
            return View(coffee);
        }

        // POST: Coffees/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,RoastLevel,Price,BeanId,RoasterId")] Coffee coffee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coffee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BeanId = new SelectList(db.CoffeeBeans, "Id", "Name", coffee.BeanId);
            ViewBag.RoasterId = new SelectList(db.Roasters, "Id", "Name", coffee.RoasterId);
            return View(coffee);
        }

        // GET: Coffees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coffee coffee = db.Coffees.Find(id);
            if (coffee == null)
            {
                return HttpNotFound();
            }
            return View(coffee);
        }

        // POST: Coffees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Coffee coffee = db.Coffees.Find(id);
            db.Coffees.Remove(coffee);
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
