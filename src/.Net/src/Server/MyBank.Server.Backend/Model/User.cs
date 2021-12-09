using MyBank.Interfaces;
using MyBank.Nameservice;
using Newtonsoft.Json;
using System;

namespace MyBank.Server.Backend.Model
{
    public class User : IUser, IEntity
    {
        public Privileges Privilege { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ID { get; set; }

        [JsonIgnore]
        public string Token => $"{ID}{((int)Privilege)}";

        public User()
        {
            OnInitialize();
        }

        private void OnInitialize()
        {
            Privilege = Privileges.User;
            ID = Guid.NewGuid().ToString("N");
        }

        public string GetMappingKey()
        {
            return Username;
        }
    }
}
