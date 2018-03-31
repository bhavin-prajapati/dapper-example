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
    public class IngredientController : Controller
    {
        private IActorRef target;
        private Inbox inbox;

        public IngredientController()
        {
            target = MvcApplication.ActorSystem.ActorOf(Props.Create<IngredientActor>());
            inbox = Inbox.Create(MvcApplication.ActorSystem);
        }

        // GET: Ingredient
        public ActionResult Index()
        {
            Ingredient model = new Ingredient();
            return View(model);
        }

        // GET: Ingredient/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ingredient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ingredient/Create
        [HttpPost]
        public ActionResult Create(Ingredient ingredient)
        {
            try
            {
                ActorMessage am = new ActorMessage(ActorMessageType.InsertIngredient, ingredient);
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

                return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
            }
            catch
            {
                return View();
            }
        }

        // GET: Ingredient/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ingredient/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Ingredient cupcake)
        {
            try
            {
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ingredient/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ingredient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                string ingredient = Request.QueryString["ingredientID"];
                int ingredientID = int.Parse(ingredient);
                ActorMessage am = new ActorMessage(ActorMessageType.DeleteIngredient, ingredientID);
                inbox.Send(target, am);
                bool delete = (bool)inbox.Receive(MvcApplication.Timeout);
                if (delete)
                {
                    TempData["shortMessage"] = "Ingredient was successfully deleted.";
                }
                else
                {
                    TempData["shortMessage"] = "Error occured while deleting ingredient.";
                }
                return RedirectToAction("Index", "Ingredient", new { id = id });
            }
            catch
            {
                return View();
            }
        }
    }
}
