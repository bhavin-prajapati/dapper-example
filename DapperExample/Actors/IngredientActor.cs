using Akka.Actor;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using DapperExample.Models;
using DapperExample.Services;

namespace DapperExample.Actors
{
    public class IngredientActor: ReceiveActor
    {
        private IIngredientService ingredientService;

        public IngredientActor()
        {
            this.ingredientService = ServiceBroker.ServiceContainer.Resolve(typeof(IIngredientService), ServiceBroker.IngredientServiceName) as IngredientService;

            Receive<ActorMessage>(am =>
            {
                switch ((ActorMessageType)am.Message)
                {
                    case ActorMessageType.GetIngredientByCupcakeID:
                        Sender.Tell(ingredientService.GetIngredientsByCupcakeID((int)am.Data));
                        break;
                    case ActorMessageType.InsertIngredient:
                        Sender.Tell(ingredientService.InsertIngredient((Ingredient)am.Data));
                        break;
                    case ActorMessageType.DeleteIngredient:
                        Sender.Tell(ingredientService.DeleteIngredient((int)am.Data));
                        break;
                    case ActorMessageType.UpdateIngredient:
                        Sender.Tell(ingredientService.UpdateIngredient((Ingredient)am.Data));
                        break;
                }
            });
        }
    }
}