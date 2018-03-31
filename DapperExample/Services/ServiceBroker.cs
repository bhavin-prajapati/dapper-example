using System.Globalization;
using Microsoft.Practices.Unity;

namespace DapperExample.Services
{
    public static class ServiceBroker
    {
        internal const string CupcakeServiceName = "CupcakeService";
        internal const string IngredientServiceName = "IngredientService";
        internal static UnityContainer iocContainer;

        public static UnityContainer ServiceContainer
        {
            get
            {
                return iocContainer;
            }
        }

        static ServiceBroker()
        {
            iocContainer = new UnityContainer();
        }

        internal static void RegisterServices()
        {
            iocContainer.RegisterType<ICupcakeService, CupcakeService>(ServiceBroker.CupcakeServiceName, new ContainerControlledLifetimeManager());
            iocContainer.RegisterType<IIngredientService, IngredientService>(ServiceBroker.IngredientServiceName, new ContainerControlledLifetimeManager());
        }
    }
}
