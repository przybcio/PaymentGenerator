using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaymentsGenerator.Abstracts;
using PaymentsGenerator.Model;

namespace PaymentsGenerator.ModelCommands
{
    public class InvokerAccountCache 
    {
        public IList<account> Accounts()
        {
            Command lastCmd;
            if (TryGetLastCommand(out lastCmd))
            {
                return lastCmd.GetAccounts();
            }
            else
                return null;
        }

        public string Do(string serverName, string initInstanceName)
        {
            var cmd = new AccountCacheCommand( new AccountCache(), serverName, initInstanceName);
            var result = cmd.Execute();
            cmds.Add(cmd);

            return result;
        }

        public void Undo()
        {
            Command lastCmd;
            if (TryGetLastCommand(out lastCmd))
            {
                lastCmd.UnExecute();
                cmds.Remove(lastCmd);
            }
        }

        public static InvokerAccountCache Instance()
        {
            if (instance == null)
                instance = new InvokerAccountCache();
            return instance;
        }

        private bool TryGetLastCommand(out Command cmd)
        {
            cmd = null;
            cmd = cmds.LastOrDefault();
            return cmd != null;
                
        }
        private InvokerAccountCache()
        {          
            cmds = new List<Command>();            
        }
        private static InvokerAccountCache instance;
        private IList<Command> cmds;
    }
}
