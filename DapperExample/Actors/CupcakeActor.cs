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
    public class CupcakeActor: ReceiveActor
    {
        private ICupcakeService cupcakeService;

        public CupcakeActor()
        {
            this.cupcakeService = ServiceBroker.ServiceContainer.Resolve(typeof(ICupcakeService), ServiceBroker.CupcakeServiceName) as CupcakeService;

            Receive<ActorMessage>(am =>
            {
                switch ((ActorMessageType)am.Message)
                {
                    case ActorMessageType.GetCupcakes:
                        Sender.Tell(cupcakeService.GetCupcakes());
                        break;
                    case ActorMessageType.GetCupcakeByID:
                        Sender.Tell(cupcakeService.GetCupcakeByID((int)am.Data));
                        break;
                    case ActorMessageType.InsertCupcake:
                        Sender.Tell(cupcakeService.InsertCupcake((Cupcake)am.Data));
                        break;
                    case ActorMessageType.DeleteCupcake:
                        Sender.Tell(cupcakeService.DeleteCupcake((int)am.Data));
                        break;
                    case ActorMessageType.UpdateCupcake:
                        Sender.Tell(cupcakeService.UpdateCupcake((Cupcake)am.Data));
                        break;
                }
            });
        }
    }
}