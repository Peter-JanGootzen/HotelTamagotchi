[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(HotelTamagotchi.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(HotelTamagotchi.Web.App_Start.NinjectWebCommon), "Stop")]

namespace HotelTamagotchi.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using HotelTamagotchi.Web.Repositories;
    using HotelTamagotchi.Web.Models;
    using Ninject.Web.Common.WebHost;
    using HotelTamagotchi.Web.Controllers;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IHotelTamagotchiContext>().To<HotelTamagotchiContext>();
            kernel.Bind<ITamagotchiRepository>().To<TamagotchiRepository>();
            kernel.Bind<IHotelRoomRepository>().To<HotelRoomRepository>();
        }
    }
}
