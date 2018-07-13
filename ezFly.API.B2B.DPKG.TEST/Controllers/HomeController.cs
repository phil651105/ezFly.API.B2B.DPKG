using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ezFly.API.B2B.DPKG.TEST.Models;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace ezFly.API.B2B.DPKG.TEST.Controllers
{
	public class TestModel
	{
		public int id { get; set; }
		public string Name { get; set; }
	}

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
			try
			{
				var str = "";//JsonConvert.SerializeObject(M);
				var url = "http://localhost:1298/api/Search";
				var client = new HttpClient();

				//POST
				var content = new StringContent(str, Encoding.UTF8, "application/json");
				var response = client.PostAsync(url,content).Result;
				var strResult = response.Content.ReadAsStringAsync().Result;

			}

			catch (Exception ex)
			{
				throw ex;
			}

			ViewData["Message"] = "這是測試頁面";

			return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		public IActionResult Test()
		{
			
			return View();
		}
	}
}
