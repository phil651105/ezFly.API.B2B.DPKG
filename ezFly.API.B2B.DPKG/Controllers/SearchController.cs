using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ezFly.API.B2B.DPKG.AppCode.DAL;
using ezFly.API.B2B.DPKG.Models.DataModel.Product;
using ezFly.API.B2B.DPKG.Models.DataModel.Search;
using ezFly.API.B2B.DPKG.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace ezFly.API.B2B.DPKG.Controllers
{
    [Produces("application/json")]
	[Route("api/[controller]")]
	public class SearchController : Controller
    {
		[HttpGet("{cityto}/{sdate}")]///api/Search/MZG/20180601
		public List<ProductModel> GetProdByDate(SearchProdRQModel list_rq)
		{
			List<ProductModel> prod_list = new List<ProductModel>();
			prod_list = ProdRepository.GetProd(list_rq);

			return prod_list;
		}

		[HttpPost]
		public List<ProductModel> GetProd([FromBody]SearchProdRQModel list_rq)
		{
			List<ProductModel> prod_list = new List<ProductModel>();
			prod_list = ProdRepository.GetProd(list_rq);

			return prod_list;
		}

		//GET api/values
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}
	}
}