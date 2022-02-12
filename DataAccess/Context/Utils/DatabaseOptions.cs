using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context.Utils
{
    public static class DatabaseOptions
    {
        private static string connectionString = "";
        //Getting connection string from the dataaccess.json
        public static string GetConnectionString()
        {
            if (connectionString != "")
            {
                return connectionString;
            }
            string pathToExe = Process.GetCurrentProcess().MainModule.FileName;
            string pathToContentRoot = Path.GetDirectoryName(pathToExe);
            string json = Path.Combine(pathToContentRoot, "dataaccess.json");

            //Check if running from source using console instead of published version
            if (!File.Exists(json))
            {
                pathToContentRoot = AppContext.BaseDirectory;
            }

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(pathToContentRoot)
                .AddJsonFile("dataaccess.json");

            IConfiguration configuration = configurationBuilder.Build();

            connectionString = configuration.GetConnectionString("DefaultConnection");
            return connectionString;
        }
    }
}
