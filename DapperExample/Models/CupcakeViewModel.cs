using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DapperExample.Actors;

namespace DapperExample.Models
{
    public class CupcakeViewModel
    {
        private IActorRef target;
        private Inbox inbox;

        public CupcakeViewModel()
        {
            target = MvcApplication.ActorSystem.ActorOf(Props.Create<CupcakeActor>());
            inbox = Inbox.Create(MvcApplication.ActorSystem);
        }

        public List<Cupcake> GetCupcakes()
        {
            ActorMessage am = new ActorMessage(ActorMessageType.GetCupcakes, null);
            inbox.Send(target, am);
            var cupcakes = (List<Cupcake>)inbox.Receive(MvcApplication.Timeout);
            return cupcakes;
        }
    }
}