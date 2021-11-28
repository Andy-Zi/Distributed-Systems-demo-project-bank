using MyBank.Server.Backend.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBank.Server.Backend.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository() : base("Users.json")
        {

        }
    }
}
