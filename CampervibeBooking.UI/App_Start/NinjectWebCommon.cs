[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CampervibeBooking.UI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(CampervibeBooking.UI.App_Start.NinjectWebCommon), "Stop")]

namespace CampervibeBooking.UI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using CampervibeBooking.Data.Common;
    using CampervibeBooking.Data.Core;
    using CampervibeBooking.UI.ViewModelMappers.Booking;
    using CampervibeBooking.UI.Security;
    using CampervibeBooking.UI.ActionFilters;
    using System.Web.Mvc;
    using Ninject.Web.Mvc.FilterBindingSyntax;
    using CampervibeBooking.UI.Logging;
    using CampervibeBooking.Domain.InfrastructureContracts;
    using CampervibeBooking.UI.ServiceProxies.Vehicle;

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
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ILog>().To<SqlLog>();
            kernel.Bind<IContextProvider>().To<GenericContextProvider>().InRequestScope();
            kernel.Bind<IMakeViewModelMapper>().To<MakeViewModelMapper>();
            kernel.Bind<IGetPendingForVehicleViewModelMapper>().To<GetPendingForVehicleViewModelMapper>();
            kernel.Bind<IUserProvider>().To<UserProvider>();
            kernel.Bind<IIndexViewModelMapper>().To<IndexViewModelMapper>();
            kernel.Bind<IVehicleServiceProxy>().To<VehicleServiceProxy>();
            kernel.BindFilter<EntityFrameworkWriteContextFilter>(FilterScope.Action, 1000).WhenActionMethodHas<EntityFrameworkWriteContextAttribute>();
            kernel.BindFilter<EntityFrameworkReadContextFilter>(FilterScope.Action, 1000).WhenActionMethodHas<EntityFrameworkReadContextAttribute>();
            kernel.BindFilter<LogFilter>(FilterScope.Action, 1050).WhenActionMethodHas<LogAttribute>();
            
            new DataRegistry().RegisterServices(kernel);
        }        
    }
}
