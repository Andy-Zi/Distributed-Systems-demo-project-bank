using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Owin;

namespace MyBank.Server.RestAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            BankServiceConfiguration.Configure(config);
            app.UseWebApi(config);
        }
    }
}