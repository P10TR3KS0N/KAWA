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
    public class CoffeeBeansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CoffeeBeans
        public ActionResult Index()
        {
            var coffeeBeans = db.CoffeeBeans.Include(c => c.OriginCountry);
            return View(coffeeBeans.ToList());
        }

        // GET: CoffeeBeans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoffeeBean coffeeBean = db.CoffeeBeans.Find(id);
            if (coffeeBean == null)
            {
                return HttpNotFound();
            }
            return View(coffeeBean);
        }

        // GET: CoffeeBeans/Create
        public ActionResult Create()
        {
            ViewBag.OriginCountryId = new SelectList(db.Countries, "Id", "Name");
            return View();
        }

        // POST: CoffeeBeans/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,OriginCountryId")] CoffeeBean coffeeBean)
        {
            if (ModelState.IsValid)
            {
                db.CoffeeBeans.Add(coffeeBean);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OriginCountryId = new SelectList(db.Countries, "Id", "Name", coffeeBean.OriginCountryId);
            return View(coffeeBean);
        }

        // GET: CoffeeBeans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoffeeBean coffeeBean = db.CoffeeBeans.Find(id);
            if (coffeeBean == null)
            {
                return HttpNotFound();
            }
            ViewBag.OriginCountryId = new SelectList(db.Countries, "Id", "Name", coffeeBean.OriginCountryId);
            return View(coffeeBean);
        }

        // POST: CoffeeBeans/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,OriginCountryId")] CoffeeBean coffeeBean)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coffeeBean).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OriginCountryId = new SelectList(db.Countries, "Id", "Name", coffeeBean.OriginCountryId);
            return View(coffeeBean);
        }

        // GET: CoffeeBeans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoffeeBean coffeeBean = db.CoffeeBeans.Find(id);
            if (coffeeBean == null)
            {
                return HttpNotFound();
            }
            return View(coffeeBean);
        }

        // POST: CoffeeBeans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CoffeeBean coffeeBean = db.CoffeeBeans.Find(id);
            db.CoffeeBeans.Remove(coffeeBean);
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
