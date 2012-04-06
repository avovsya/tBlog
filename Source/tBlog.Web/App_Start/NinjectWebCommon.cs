using tBlog.Data.Abstract;
using tBlog.Data.Infrastructure;
using tBlog.Data.Repositories;

[assembly: WebActivator.PreApplicationStartMethod(typeof(tBlog.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(tBlog.Web.App_Start.NinjectWebCommon), "Stop")]

namespace tBlog.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

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
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDatabaseFactory>().To<DatabaseFactory>().InRequestScope();
            kernel.Bind<IEntryRepository>().To<EntryRepository>();
            kernel.Bind<ICommentRepository>().To<CommentRepository>();
            kernel.Bind<ISettingRepository>().To<SettingRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind(typeof(IRepository<>)).To(typeof(BaseRepository<>));
        }        
    }
}
