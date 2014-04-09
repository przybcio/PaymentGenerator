using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.EntityClient;

namespace PaymentsGenerator.Model
{
    public class AccountCache
    {       
        public IList<account> GetAccounts()
        {
            if (!cachedList.Any())
            {
                using (var context = new Model1Container(sqlConn))
                    cachedList= context.account.Where(acnt => acnt.customer_id != 1 && acnt.status == "A").ToList();
            }
            return cachedList;
        }

        public string SetEntityConnection(string serverName, string initInstanceName)
        {
            EntityConnectionStringBuilder enityBuilder = new EntityConnectionStringBuilder(); 
            enityBuilder.Provider = "System.Data.SqlClient";
            enityBuilder.Metadata = @"res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl";
            enityBuilder.ProviderConnectionString = "Data Source=" + serverName + "\\" + initInstanceName + ";Initial Catalog=ozyrys;Integrated Security=SSPI;multipleactiveresultsets=True;App=EntityFramework";
            sqlConn = enityBuilder.ToString();
            return sqlConn;
        }
        public AccountCache()
        {
            cachedList = new List<account>();
        }
        private string sqlConn;
        private IList<account> cachedList;
    }
}
