using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezFly.API.B2B.DPKG.Models.DataModel.Product
{
	public class ProdDetailRQModel
	{
		public string PRODNO { get; set; }
        public string HTLNO { get; set; }//HotelComboModel
        public string SDATE { get; set; }
		public string EDATE { get; set; }
		public string CITYFROM { get; set; }
		public string CITYTO { get; set; }
		public int ADULT { get; set; }
		public int CHILD { get; set; }
		public int CHILDNB { get; set; }
		public int SENIOR { get; set; }

	}

	public class ProdDetailRQPostModel : ProdDetailRQModel
	{
	}
}
