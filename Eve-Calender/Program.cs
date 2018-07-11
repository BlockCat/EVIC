using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Eve_Calender
{
    public class Program
    {
        //public static string CLIENT_ID = "1529cac857a848989f881c396c4b6bd7";
        //public static string CLIENT_SECRET = "yOEypT0kyycbdam61qHOBgLobtcHtIOPj1yfeqZ9";
        //public static string BASE_URL = "http://evic.catdock.org";

        public static string CLIENT_ID = Environment.GetEnvironmentVariable("CLIENT_ID");
        public static string CLIENT_SECRET = Environment.GetEnvironmentVariable("CLIENT_SECRET");
        public static string USER_AGENT = Environment.GetEnvironmentVariable("USER_AGENT");
        public static string BASE_URL = Environment.GetEnvironmentVariable("BASE_URL");

        public static string MONGO_DB = Environment.GetEnvironmentVariable("MONGO_DB");
        public static string MONGO_USERNAME = Environment.GetEnvironmentVariable("MONGO_USERNAME");
        public static string MONGO_PASSWORD = Environment.GetEnvironmentVariable("MONGO_PASSWORD");
        public static string MONGO_PORT = Environment.GetEnvironmentVariable("MONGO_PORT");
        public static string MONGO_HOST = Environment.GetEnvironmentVariable("MONGO_HOST");

        public static string REDIRECT_URL = $"{BASE_URL}/api/Authorization/code";
        
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
