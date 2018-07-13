using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezFly.API.B2B.DPKG.Models.DataModel.Product
{
    public class TketModel
    {
		public string PROD_NO { get; set; }     //產品編號
		public string TKET_XID { get; set; }    //

		public List<TketDetailModel> TKETS { get; set; }   //票券細節
	}
}
