using MyBank.Interfaces;
using MyBank.Nameservice;
using MyBank.RESTConnector;
using MyBank.WCFConnector;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace MyBank.Client
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = new UnityContainer();
            container.RegisterInstance<IUnityContainer>(container);
            container.RegisterType<LoginForm>();
            container.RegisterType<MainForm>();
            container.RegisterInstance(new ApplicationEnvironment());
            container.RegisterType<IServiceConnector, WCFServiceConnector>(nameof(ConnectionTypes.WCF),new ContainerControlledLifetimeManager());
            container.RegisterType<IServiceConnector, RESTServiceConnector>(nameof(ConnectionTypes.REST), new ContainerControlledLifetimeManager());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<LoginForm>());
            Application.Exit();

            //Keep the container but start a new UI-Thread

            var applicationEnvironemnt = container.Resolve<ApplicationEnvironment>();
            if(!string.IsNullOrEmpty(applicationEnvironemnt.CurrentToken))
                Application.Run(container.Resolve<MainForm>());
        }
    }
}
