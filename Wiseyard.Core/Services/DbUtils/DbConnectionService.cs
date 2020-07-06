using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Wiseyard.Core.Services.DbUtils
{
    class DbConnectionService
    {
        public static string GetConnectionString()
        {
            var conStr = ConfigurationManager.ConnectionStrings["WiseyardConnectionString"]?.ConnectionString;

            if (!string.IsNullOrWhiteSpace(conStr))
            {
                return conStr;
            }
            else
            {
                return @"Data Source=.;Initial Catalog=wiseyard;Integrated Security=True";
            }
        }
    }
}
