using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaymentsGenerator.Abstracts;
using PaymentsGenerator.Model;

namespace PaymentsGenerator.ModelCommands
{
    public class AccountCacheCommand : Command, IAccounts
    {
        public override string Execute()
        {
            return cache.SetEntityConnection(serverName, initInstanceName);
        }

        public override void UnExecute()
        {
            cache.SetEntityConnection(serverName, initInstanceName);
        }

        public override IList<account> GetAccounts()
        {
            return cache.GetAccounts();
        }

        public AccountCacheCommand(AccountCache cache, string serverName, string initInstanceName)
        {
            this.cache = cache;
            this.serverName =  serverName;
            this.initInstanceName = initInstanceName;
        }
        private AccountCache cache;
        private string initInstanceName;
        private string serverName;
    }
}
