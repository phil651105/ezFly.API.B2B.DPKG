using ezFly.API.B2B.DPKG.Models.DataModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezFly.API.B2B.DPKG.Models.DataModel.Search
{
	[Serializable]
	public class HotelComboModel
	{
		public string HTL_NO { get; set; }      //飯店編號
		public string HTL_NAME { get; set; }    //飯店名稱
	}
}
