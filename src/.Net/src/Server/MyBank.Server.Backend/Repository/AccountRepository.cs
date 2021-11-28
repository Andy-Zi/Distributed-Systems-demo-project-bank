﻿using MyBank.Server.Backend.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBank.Server.Backend.Repository
{
    public class AccountRepository : BaseRepository<Account>
    {
        public AccountRepository() : base("Accounts.json")
        {

        }

        public IList<Account> GetAccounts(string userName)
        {
            IList<Account> result = new List<Account>();
            lock (LockObject)
            {
                foreach(var account in Entities.Values)
                {
                    if(account.Owner == userName)
                        result.Add(account);

                }
            }
            return result;
     
        }
    }
}
