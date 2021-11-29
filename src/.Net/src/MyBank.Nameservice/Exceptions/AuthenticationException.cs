using System;
using System.Collections.Generic;
using System.Text;

namespace MyBank.Nameservice.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException(string message):base(message)
        {

        }
    }

    public class LoginException : Exception
    {
        public LoginException(string message) : base(message)
        {

        }
    }
}
