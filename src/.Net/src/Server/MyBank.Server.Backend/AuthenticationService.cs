using MyBank.Nameservice;
using MyBank.Nameservice.Exceptions;
using MyBank.Server.Backend.Model;
using MyBank.Server.Backend.Repository;
using System;
using System.Collections.Concurrent;
using System.Linq;
using Unity;

namespace MyBank.Server.Backend
{
    public class AuthenticationService
    {
        [Dependency] public UserRepository UserRepository { get; set; }

        public ConcurrentDictionary<string, string> LoggedInUsers = new ConcurrentDictionary<string, string>();
        public AuthenticationService()
        {

        }

        public bool Authenticate(string token,Privileges privileges = Privileges.User)
        {
            if (!LoggedInUsers.ContainsKey(token))
                return false;

            var userPrivilege = token.Last() - '0'; // Char to int 
            return userPrivilege >= (int)privileges;
        }

        public string LogIn(string username,string password)
        {
            if (!UserRepository.Entities.ContainsKey(username))
                throw new LoginException("Wrong Username or Password!");

            var user = UserRepository.Entities[username];

            if(user.Password != password)
                throw new LoginException("Wrong Username or Password!");

            if (LoggedInUsers.ContainsKey(user.Token))
            {
                //User was already logged in we have to invalidate the current session and create a new one
                LoggedInUsers.TryRemove(user.Token, out _);
                user.ID = Guid.NewGuid().ToString("N");
            }

            LoggedInUsers.TryAdd(user.Token, user.Username);
            return user.Token;
        }

        public void LogOut(string token)
        {
            if(LoggedInUsers.ContainsKey(token))
                LoggedInUsers.TryRemove(token, out _);
        }
    }
}
