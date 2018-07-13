using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezFly.API.B2B.DPKG.Models.DataModel.Product
{
	public class ProdDetailModel
	{
		public string PROD_NO { get; set; }               //商品編號
		public string MESSAGE { get; set; }               //回傳訊息內容
		public string STATUS_CODE { get; set; }           //回傳狀態碼(00:成功)

		public List<TrafficModel> TRAFFICS { get; set; }  //交通
		public List<HotelModel> HOTELS{ get; set; }       //飯店
		public List<TourModel> TOURS { get; set; }        //行程
		public List<DcarModel> DCARS { get; set; }        //租車
		public List<TketModel> TKETS{ get; set; }         //票券
	}
}
