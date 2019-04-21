using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMLibrary
{
    public class WebConfig
    {
        private WebConfig()
        {
        }

        public static void SetDBConnecttion(string ConnectionString)
        {
            System.Web.HttpContext.Current.Session["DBLocalConnectionString"] = ConnectionString;
        }


    }
}
