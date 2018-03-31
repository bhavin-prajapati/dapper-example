using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DapperExample
{
    public enum ActorMessageType
    {
        GetCupcakes,
        GetCupcakeByID,
        InsertCupcake,
        DeleteCupcake,
        UpdateCupcake,
        GetIngredientByCupcakeID,
        InsertIngredient,
        DeleteIngredient,
        UpdateIngredient
    }
    
    public class ActorMessage
    {
        private ActorMessageType message;
        private object data;

        public ActorMessageType Message { get { return message; } }

        public object Data { get { return data; } }

        public ActorMessage(ActorMessageType message, object data)
        {
            this.message = message;
            this.data = data;
        }
    }
}