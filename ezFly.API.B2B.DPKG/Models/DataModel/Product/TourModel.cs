using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezFly.API.B2B.DPKG.Models.DataModel.Product
{
    public class TourModel
    {
		public string PROD_NO { get; set; }     //產品編號
		public string TOUR_XID { get; set; }    //

		public List<TourDetailModel> TOURS { get; set; }   //行程細節
	}
}
