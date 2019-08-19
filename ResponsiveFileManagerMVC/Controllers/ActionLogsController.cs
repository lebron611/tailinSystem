using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ResponsiveFileManagerMVC.Models;

namespace ResponsiveFileManagerMVC.Controllers
{
    public class ActionLogsController : Controller
    {
        private DBModel db = new DBModel();

        // GET: ActionLogs
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.ActionLog.ToList());
        }

        // GET: ActionLogs/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActionLog actionLog = db.ActionLog.Find(id);
            if (actionLog == null)
            {
                return HttpNotFound();
            }
            return View(actionLog);
        }

        // GET: ActionLogs/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActionLogs/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "LogId,Operator,Refer,Destination,Method,MobleDevices,IPAddress,RequestTime")] ActionLog actionLog)
        {
            if (ModelState.IsValid)
            {
                db.ActionLog.Add(actionLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(actionLog);
        }

        // GET: ActionLogs/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActionLog actionLog = db.ActionLog.Find(id);
            if (actionLog == null)
            {
                return HttpNotFound();
            }
            return View(actionLog);
        }

        // POST: ActionLogs/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "LogId,Operator,Refer,Destination,Method,MobleDevices,IPAddress,RequestTime")] ActionLog actionLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actionLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(actionLog);
        }

        // GET: ActionLogs/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActionLog actionLog = db.ActionLog.Find(id);
            if (actionLog == null)
            {
                return HttpNotFound();
            }
            return View(actionLog);
        }

        // POST: ActionLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(long id)
        {
            ActionLog actionLog = db.ActionLog.Find(id);
            db.ActionLog.Remove(actionLog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
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
