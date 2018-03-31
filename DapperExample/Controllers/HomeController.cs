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
    public class HomeController : Controller
    {
        private IActorRef target;
        private Inbox inbox;

        public HomeController()
        {
            target = MvcApplication.ActorSystem.ActorOf(Props.Create<CupcakeActor>());
            inbox = Inbox.Create(MvcApplication.ActorSystem);
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (TempData["shortMessage"] != null)
            {
                ViewBag.Message = TempData["shortMessage"].ToString();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(Cupcake model)
        {
            if (ModelState.IsValid)
            {
                ActorMessage am = new ActorMessage(ActorMessageType.InsertCupcake, model);
                inbox.Send(target, am);
                bool added = (bool)inbox.Receive(MvcApplication.Timeout);
                if (added)
                {
                    ViewBag.Message = "Cupcake was successfully added.";
                }
                else
                {
                    ViewBag.Message = "Error occured while adding cupcake.";
                }
            }
            return View(model);
        }
    }
}