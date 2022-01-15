using System;
using System.Collections.Generic;
using System.Text;

namespace MyBank.Nameservice.Exceptions
{
    public class ServerNotReachableException : Exception
    {
        public ServerNotReachableException(Exception innerException) : base("Server can't be reached! Is your Connection Working?",innerException)
        {

        }
    }
}
