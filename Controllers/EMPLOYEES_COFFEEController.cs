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
    public class EMPLOYEES_COFFEEController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: EMPLOYEES_COFFEE
        public async Task<ActionResult> Index()
        {
            var eMPLOYEES_COFFEE = db.EMPLOYEES_COFFEE.Include(e => e.DEPARTMENT).Include(e => e.EMP_HISTORY);
            return View(await eMPLOYEES_COFFEE.ToListAsync());
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(EMPLOYEES_COFFEE objUser)
        {
            if (ModelState.IsValid)
            {
                using (Entities1 db = new Entities1())
                {
                    var obj = db.EMPLOYEES_COFFEE.Where(a => a.EMP_USERNAME.Equals(objUser.EMP_USERNAME) && a.EMP_PASSWORD.Equals(objUser.EMP_PASSWORD)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.EMP_ID.ToString();
                        Session["UserName"] = obj.EMP_USERNAME.ToString();
                        
                    }
                }
            }
            return View(objUser);
        }

      

        // GET: EMPLOYEES_COFFEE/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLOYEES_COFFEE eMPLOYEES_COFFEE = await db.EMPLOYEES_COFFEE.FindAsync(id);
            if (eMPLOYEES_COFFEE == null)
            {
                return HttpNotFound();
            }
            return View(eMPLOYEES_COFFEE);
        }

        // GET: EMPLOYEES_COFFEE/Create
        public ActionResult Create()
        {
            ViewBag.DEPT_ID = new SelectList(db.DEPARTMENTS, "DEP_ID", "DEP_NAME");
            ViewBag.EMP_ID = new SelectList(db.EMP_HISTORY, "EMP_ID", "EMP_START_TIME");
            return View();
        }

        // POST: EMPLOYEES_COFFEE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EMP_ID,EMP_FIRSTNAME,EMP_LASTNAME,ADDRESS,SALARY,HIRE_DATE,END_DATE,EMP_USERNAME,EMP_PASSWORD,DEPT_ID")] EMPLOYEES_COFFEE eMPLOYEES_COFFEE)
        {
            if (ModelState.IsValid)
            {
                db.EMPLOYEES_COFFEE.Add(eMPLOYEES_COFFEE);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DEPT_ID = new SelectList(db.DEPARTMENTS, "DEP_ID", "DEP_NAME", eMPLOYEES_COFFEE.DEPT_ID);
            ViewBag.EMP_ID = new SelectList(db.EMP_HISTORY, "EMP_ID", "EMP_START_TIME", eMPLOYEES_COFFEE.EMP_ID);
            return View(eMPLOYEES_COFFEE);
        }

        // GET: EMPLOYEES_COFFEE/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLOYEES_COFFEE eMPLOYEES_COFFEE = await db.EMPLOYEES_COFFEE.FindAsync(id);
            if (eMPLOYEES_COFFEE == null)
            {
                return HttpNotFound();
            }
            ViewBag.DEPT_ID = new SelectList(db.DEPARTMENTS, "DEP_ID", "DEP_NAME", eMPLOYEES_COFFEE.DEPT_ID);
            ViewBag.EMP_ID = new SelectList(db.EMP_HISTORY, "EMP_ID", "EMP_START_TIME", eMPLOYEES_COFFEE.EMP_ID);
            return View(eMPLOYEES_COFFEE);
        }

        // POST: EMPLOYEES_COFFEE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EMP_ID,EMP_FIRSTNAME,EMP_LASTNAME,ADDRESS,SALARY,HIRE_DATE,END_DATE,EMP_USERNAME,EMP_PASSWORD,DEPT_ID")] EMPLOYEES_COFFEE eMPLOYEES_COFFEE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eMPLOYEES_COFFEE).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DEPT_ID = new SelectList(db.DEPARTMENTS, "DEP_ID", "DEP_NAME", eMPLOYEES_COFFEE.DEPT_ID);
            ViewBag.EMP_ID = new SelectList(db.EMP_HISTORY, "EMP_ID", "EMP_START_TIME", eMPLOYEES_COFFEE.EMP_ID);
            return View(eMPLOYEES_COFFEE);
        }

        // GET: EMPLOYEES_COFFEE/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLOYEES_COFFEE eMPLOYEES_COFFEE = await db.EMPLOYEES_COFFEE.FindAsync(id);
            if (eMPLOYEES_COFFEE == null)
            {
                return HttpNotFound();
            }
            return View(eMPLOYEES_COFFEE);
        }

        // POST: EMPLOYEES_COFFEE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            EMPLOYEES_COFFEE eMPLOYEES_COFFEE = await db.EMPLOYEES_COFFEE.FindAsync(id);
            db.EMPLOYEES_COFFEE.Remove(eMPLOYEES_COFFEE);
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
