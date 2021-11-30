using MyBank.Nameservice;
using MyBank.Server.Backend;
using MyBank.Server.Backend.Model;
using MyBank.Server.Backend.Repository;
using MyBank.Server.WCF;
using System;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace MyBank.Server
{
    public static class ServiceRegistry
    {
        public static void Register(UnityContainer container)
        {
            container.RegisterInstance(new ApplicationEnvironment(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));
            container.RegisterType<UserRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<AccountRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<TransactionRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<AuthenticationService>(new ContainerControlledLifetimeManager());
            container.RegisterType<BankService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IWCFBankService, WCFBankService>();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            var container = new UnityContainer();

            ServiceRegistry.Register(container);


            var service = container.Resolve<BankService>();
            service.Initialize();

            var userRepository = container.Resolve<UserRepository>();
            var lukas = new User()
            {
                Username = "lukas",
                Password = "1234",
                Privilege = Privileges.Admin
            };
            userRepository.Entities.TryAdd(lukas.GetMappingKey(), lukas);

            var cancelationSource = new CancellationTokenSource();

            var wcfThread = new Thread(() =>
            {
                var token = cancelationSource.Token;
                var wcfService = container.Resolve<IWCFBankService>();
                ServiceHost host = new ServiceHost(wcfService, new Uri("http://localhost:8000/WCFBankService"));
                host.Description.Behaviors.Add(new ServiceMetadataBehavior() { HttpGetEnabled = true });
                host.Open();
                Console.WriteLine("Started WCF Server!");
                while (!token.IsCancellationRequested)
                    Task.Delay(500).Wait(token);

                host.Close();
            });
            wcfThread.Start();

            Console.ReadLine();
            cancelationSource.Cancel(false);

            container.Dispose();
        }
    }
}
