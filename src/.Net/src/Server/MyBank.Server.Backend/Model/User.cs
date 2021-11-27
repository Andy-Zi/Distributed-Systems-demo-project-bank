using MyBank.Interfaces;
using MyBank.Nameservice;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBank.Server.Backend.Model
{
    internal class User : IUser
    {
        public Privileges Privilege { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AccountNumber { get; set; }
        public string Token => $"{AccountNumber}{Privilege}";

        public User()
        {
            OnInitialize();
        }

        private void OnInitialize()
        {
            Privilege = Privileges.User;
            AccountNumber = Guid.NewGuid().ToString("N");
        }
    }
}
