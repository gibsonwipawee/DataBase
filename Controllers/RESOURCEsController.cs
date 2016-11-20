using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Database.Model;

namespace Database.Controllers
{
    public class RESOURCEsController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: RESOURCEs
        public async Task<ActionResult> Index()
        {
            return View(await db.RESOURCES.ToListAsync());
        }

        // GET: RESOURCEs/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RESOURCE rESOURCE = await db.RESOURCES.FindAsync(id);
            if (rESOURCE == null)
            {
                return HttpNotFound();
            }
            return View(rESOURCE);
        }

        // GET: RESOURCEs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RESOURCEs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RESOURCE_ID,RESOURCE_NAME,ORDER_DATE,AMOUNT")] RESOURCE rESOURCE)
        {
            if (ModelState.IsValid)
            {
                db.RESOURCES.Add(rESOURCE);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(rESOURCE);
        }

        // GET: RESOURCEs/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RESOURCE rESOURCE = await db.RESOURCES.FindAsync(id);
            if (rESOURCE == null)
            {
                return HttpNotFound();
            }
            return View(rESOURCE);
        }

        // POST: RESOURCEs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RESOURCE_ID,RESOURCE_NAME,ORDER_DATE,AMOUNT")] RESOURCE rESOURCE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rESOURCE).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(rESOURCE);
        }

        // GET: RESOURCEs/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RESOURCE rESOURCE = await db.RESOURCES.FindAsync(id);
            if (rESOURCE == null)
            {
                return HttpNotFound();
            }
            return View(rESOURCE);
        }

        // POST: RESOURCEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            RESOURCE rESOURCE = await db.RESOURCES.FindAsync(id);
            db.RESOURCES.Remove(rESOURCE);
            await db.SaveChangesAsync();
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
