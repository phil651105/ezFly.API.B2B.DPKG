using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enyim.Caching;
using ezFly.API.B2B.DPKG.Models.DataModel.Product;
using ezFly.API.B2B.DPKG.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ezFly.API.B2B.DPKG.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
		private IMemcachedClient _memcachedClient;
		public ProductController(IMemcachedClient _cache)
		{
			_memcachedClient = _cache;
		}

		//Product/DPKG000000068/DHTL000002224_DHTL000000588/20180610/20180613/TSA/MFK/1/0/0/0
		[HttpGet("{prodno}/{htlno}/{sdate}/{edate}/{cityfrom}/{cityto}/{adult}/{child}/{childnb}/{senior}")]
		public ProdDetailModel GetProdDetail(ProdDetailRQModel prod_rq)
		{
			ProdDetailModel prod = new ProdDetailModel();
			prod = ProdRepository.GetProdDetail(prod_rq);
            //prod = ProdRepository.GetProdDetail(prod_no, s_date, e_date);                 

            return prod;
		}

		[HttpPost]
		public ProdDetailModel GetProdDetail([FromBody]ProdDetailRQPostModel prod_rq)
		{
			ProdDetailModel prod = new ProdDetailModel();
			prod = ProdRepository.GetProdDetail(prod_rq);
			//prod = ProdRepository.GetProdDetail("DPKG000001364", Convert.ToDateTime("06/10/2018"), Convert.ToDateTime("06/13/2018"));

			return prod;
		}
	}
}