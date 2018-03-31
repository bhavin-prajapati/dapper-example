using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DapperExample.Actors;
using DapperExample.Models;

namespace DapperExample.Controllers
{
    public class CupcakeController : Controller
    {
        private IActorRef target;
        private Inbox inbox;

        public CupcakeController()
        {
            target = MvcApplication.ActorSystem.ActorOf(Props.Create<CupcakeActor>());
            inbox = Inbox.Create(MvcApplication.ActorSystem);
        }

        // GET: Cupcake
        public ActionResult Index()
        {
            return View();
        }

        // GET: Cupcake/Details/5
        public ActionResult Details(int id)
        {
            ViewData["id"] = id;
            return View();
        }

        // GET: Cupcake/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cupcake/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cupcake/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cupcake/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Cupcake cupcake)
        {
            try
            {
                cupcake.CupcakeID = id;
                ActorMessage am = new ActorMessage(ActorMessageType.UpdateCupcake, cupcake);
                inbox.Send(target, am);
                bool edited = (bool)inbox.Receive(MvcApplication.Timeout);
                if (edited)
                {
                    TempData["shortMessage"] = "Cupcake was successfully saved.";
                }
                else
                {
                    TempData["shortMessage"] = "Error occured while saving cupcake.";
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cupcake/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cupcake/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ActorMessage am = new ActorMessage(ActorMessageType.DeleteCupcake, id);
                inbox.Send(target, am);
                bool delete = (bool)inbox.Receive(MvcApplication.Timeout);
                if (delete)
                {
                    TempData["shortMessage"] = "Cupcake was successfully deleted.";
                }
                else
                {
                    TempData["shortMessage"] = "Error occured while deleting cupcake.";
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
