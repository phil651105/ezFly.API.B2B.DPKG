using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezFly.API.B2B.DPKG.Models.DataModel.Product
{
    public class DcarModel
    {
		public string PROD_NO { get; set; }     //產品編號
		public string DCAR_XID { get; set; }    //

		public List<DcarDetailModel> DCARS { get; set; }   //租車細節
	}
}
