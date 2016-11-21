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
using System.Web.Security;

namespace Database.Controllers
{
    public class USERsController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: USERs

        // Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(USER objUser)
        {


            using (Entities1 db = new Entities1())
            {
                var obj = db.USERS.Where(a => a.USERNAME.Equals(objUser.USERNAME) && a.PASSWORD.Equals(objUser.PASSWORD)).FirstOrDefault();
                if (obj != null)
                {
                    Session["UserID"] = obj.USER_ID.ToString();
                    Session["UserName"] = obj.USERNAME.ToString();
                    return RedirectToAction("Dash");
                }
                ModelState.AddModelError("", "Username or Password is wrong!");
            }
            return View(objUser);
        }
         
                
            
        
        public ActionResult Dash()
        {
            return View();
        }
        public async Task<ActionResult> Index()
        {
            return View(await db.USERS.ToListAsync());
        }

        // GET: USERs/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = await db.USERS.FindAsync(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // GET: USERs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: USERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "USER_ID,FIRST_NAME,LAST_NAME,USER_SEX,USER_TEL,USER_EMAIL,USER_BD,USER_POINT,USERNAME,PASSWORD")] USER uSER)
        {
            if (ModelState.IsValid)
            {
                db.USERS.Add(uSER);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(uSER);
        }

        // GET: USERs/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = await db.USERS.FindAsync(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // POST: USERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "USER_ID,FIRST_NAME,LAST_NAME,USER_SEX,USER_TEL,USER_EMAIL,USER_BD,USER_POINT,USERNAME,PASSWORD")] USER uSER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSER).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(uSER);
        }

        // GET: USERs/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = await db.USERS.FindAsync(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // POST: USERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            USER uSER = await db.USERS.FindAsync(id);
            db.USERS.Remove(uSER);
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
