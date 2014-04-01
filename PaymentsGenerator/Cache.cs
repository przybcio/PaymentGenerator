using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaymentsGenerator
{
    public class Cache
    {
        public static Cache Instance()
        {
            if (instance == null)
                instance = new Cache();
            return instance;
        }
        public IList<account> GetAccounts()
        {
            if (cachedList == null)
                using (var context = new Model1Container())
                    cachedList = context.account.Where(acnt => acnt.customer_id != 1 && acnt.status == "A").ToList();

            return cachedList;
        }

        private Cache()
        {

        }

        private static Cache instance;
        private IList<account> cachedList;
    }
}
