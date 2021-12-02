using MyBank.Interfaces;
using MyBank.Nameservice;

namespace MyBank.Client
{
    public class ApplicationEnvironment
    {
        public string CurrentToken { get; set; }
        public ConnectionTypes CurrentConnectionType { get; set; }
        public ErrorHandlingServiceConnector ServiceConnector { get; set; }
    }
}
