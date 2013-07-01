using System;
using Microsoft.Practices.Unity;
using MvcUnityBootstrapperTest.Business;
using MvcUnityBootstrapperTest.UnityExtensions;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Linq;

namespace MvcUnityBootstrapperTest.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            var myAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("MvcUnityBootstrapperTest")).ToArray();

            container.AddNewExtension<Interception>();
            container.RegisterTypes(
                UnityHelpers.GetTypesWithCustomAttribute<UnityIoCPerRequestLifetimeAttribute>(myAssemblies),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                PerRequest,
                getInjectionMembers: t => new InjectionMember[]
                    {
                        new Interceptor<InterfaceInterceptor>(),
                        new InterceptionBehavior<MvcUnityBootstrapperTest.UnityExtensions.DiagnosisBehaviour>()
                    })
                     .RegisterTypes(
                         UnityHelpers.GetTypesWithCustomAttribute<UnityIoCTransientLifetimeAttribute>(myAssemblies),
                         WithMappings.FromMatchingInterface,
                         WithName.Default,
                         WithLifetime.Transient);

            container.Configure<Interception>().SetInterceptorFor<UnitOfWorkExample>(
                       new VirtualMethodInterceptor());
                      

            // Single InterfaceInterceptor
            // DiagnosisBehaviour d = new DiagnosisBehaviour();
            //  container.RegisterType<IBusinessClass2>(
            //    new InterceptionBehavior(d),
            //    new Interceptor(new InterfaceInterceptor()));


            // This method checks and for classes from Loaded Assemblies and creates a per request lifetime object for classes with the custom attribute 
            //container.RegisterTypes(AllClasses.FromLoadedAssemblies(),
            //                        UnityHelpers.FromAllInterfacesWith_PerRequestLifetimeAttribute,
            //                        WithName.Default,
            //                        PerRequest
            //                    )
            //         .RegisterType<IUnitOfWorkExample, UnitOfWorkExampleTest>(new TransientLifetimeManager());
 
        }

        public static Func<System.Type, Microsoft.Practices.Unity.LifetimeManager> PerRequest = (x) => new PerRequestLifetimeManager();
    }
}

