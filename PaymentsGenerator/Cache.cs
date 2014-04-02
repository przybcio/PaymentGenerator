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
            if (cachedList == null)
                using (var context = new Model1Container(enityBuilder.ToString()))
                    cachedList = context.account.Where(acnt => acnt.customer_id != 1 && acnt.status == "A").ToList();

            return cachedList;
        }

        public string SetEntityConnection(string dataSourceName, string initCatalogName)
        {
            enityBuilder.ProviderConnectionString = "Data Source=" + dataSourceName + "\\" + initCatalogName + ";Initial Catalog=ozyrys;Integrated Security=SSPI;multipleactiveresultsets=True;App=EntityFramework";
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
        //TODO: change to dictionary where key is DB instace name
        private IList<account> cachedList;
    }
}
