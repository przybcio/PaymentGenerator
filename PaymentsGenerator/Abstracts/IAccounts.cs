using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaymentsGenerator.Abstracts
{
    public interface IAccounts
    {
        IList<account> GetAccounts();
    }
}
