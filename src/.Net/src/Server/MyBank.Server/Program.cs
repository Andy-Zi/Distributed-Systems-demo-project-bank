﻿using MyBank.Nameservice;
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

            CreateDummyData(container);
            

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

        static void CreateDummyData(UnityContainer container)
        {
            var userRepository = container.Resolve<UserRepository>();
            var lukas = new User()
            {
                Username = "lukas",
                Password = "1234",
                Privilege = Privileges.Admin
            };

            var andy = new User()
            {
                Username = "andy",
                Password = "1234",
                Privilege = Privileges.User
            };
            userRepository.Entities.TryAdd(lukas.GetMappingKey(), lukas);
            userRepository.Entities.TryAdd(andy.GetMappingKey(), andy);

            var accountRepository = container.Resolve<AccountRepository>();

            var ac1 = new Account()
            {
                Owner = "lukas",
                Value = 1_000_000,
                Description = "Sparkasse",
                AccountNumber = Guid.NewGuid().ToString("N")
            };

            var ac2 = new Account()
            {
                Owner = "lukas",
                Value = -100,
                Description = "Reifeisen",
                AccountNumber = Guid.NewGuid().ToString("N")
            };

            accountRepository.Entities.TryAdd(ac1.GetMappingKey(), ac1);
            accountRepository.Entities.TryAdd(ac2.GetMappingKey(), ac2);

            var transactionRepository = container.Resolve<TransactionRepository>();
            var t1 = new Transaction()
            {
                Date = DateTime.Now,
                Amount = 100,
                Comment = "Das ist eine Transaktion",
                RecieverAccount = ac2.AccountNumber,
                SenderAccount = ac1.AccountNumber,
            };
            transactionRepository.Entities.TryAdd(t1.GetMappingKey(), t1);

        }
    }
}
