using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DapperExample.Actors;

namespace DapperExample.Models
{
    public class IngredientViewModel
    {
        private IActorRef target;
        private Inbox inbox;
        private int cupcakeID;

        public IngredientViewModel(int cupcakeId)
        {
            target = MvcApplication.ActorSystem.ActorOf(Props.Create<IngredientActor>());
            inbox = Inbox.Create(MvcApplication.ActorSystem);
            cupcakeID = cupcakeId;
        }

        public List<Ingredient> GetIngredients()
        {
            ActorMessage am = new ActorMessage(ActorMessageType.GetIngredientByCupcakeID, cupcakeID);
            inbox.Send(target, am);
            var ingredients = (List<Ingredient>)inbox.Receive(MvcApplication.Timeout);
            return ingredients;
        }
    }
}