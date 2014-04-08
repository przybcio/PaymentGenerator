using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.EntityClient;

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
            IList<account> resultList;
            if (!cachedList.TryGetValue(instanceName, out resultList))
            {
                using (var context = new Model1Container(enityBuilder.ToString()))
                    resultList = context.account.Where(acnt => acnt.customer_id != 1 && acnt.status == "A").ToList();
                cachedList.Add(new KeyValuePair<string, IList<account>>(instanceName, resultList));
            }
            return resultList;
        }

        public string SetEntityConnection(string serverName, string initInstanceName)
        {
            this.instanceName = initInstanceName.ToUpper();
            enityBuilder.ProviderConnectionString = "Data Source=" + serverName + "\\" + initInstanceName + ";Initial Catalog=ozyrys;Integrated Security=SSPI;multipleactiveresultsets=True;App=EntityFramework";
            return enityBuilder.ToString();
        }
        private Cache()
        {
            enityBuilder.Provider = "System.Data.SqlClient";
            enityBuilder.ProviderConnectionString = sqlConn;
            enityBuilder.Metadata = @"res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl";
        }


        private EntityConnectionStringBuilder enityBuilder = new EntityConnectionStringBuilder(); 
        private string sqlConn = @"Data Source=.\sqlexpress_dnb;initial catalog=ozyrys;integrated security=True;multipleactiveresultsets=True;App=EntityFramework"; 
        private static Cache instance;
        private string instanceName = "sqlexpress_dnb".ToUpper();
        private IDictionary<string, IList<account>> cachedList = new Dictionary<string, IList<account>>();
    }
}
