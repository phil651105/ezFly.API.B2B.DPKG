using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;

namespace ezFly.API.B2B.DPKG.TEST
{
    public class Program
    {
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
