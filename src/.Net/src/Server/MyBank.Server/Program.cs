using MyBank.Server.Backend;
using MyBank.Server.Backend.Repository;
using System.IO;
using System.Reflection;
using Unity;
using Unity.Lifetime;

namespace MyBank.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var container = new UnityContainer();

            container.RegisterInstance(new ApplicationEnvironment(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));
            container.RegisterType<UserRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<AccountRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<AuthenticationService>(new ContainerControlledLifetimeManager());
            container.RegisterType<BankService>(new ContainerControlledLifetimeManager());

            var service = container.Resolve<BankService>();
            service.Initialize();


            container.Dispose();
        }
    }
}
