using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaymentsGenerator.Abstracts
{
    public abstract class Command
    {
        public abstract string Execute();
        public abstract void UnExecute();
        public abstract IList<account> GetAccounts();
    }
}
