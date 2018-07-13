using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezFly.API.B2B.DPKG.Models.DataModel.Product
{
    public class TketPriceModel
    {
		public string ADULT_PRICE { get; set; }    //成人價格
		public string CHILD_PRICE { get; set; }    //孩童價格
		public string SENIOR_PRICE { get; set; }   //老人價格

		public string ADULT_COST { get; set; }     //成人成本
		public string CHILD_COST { get; set; }     //孩童成本
		public string SENIOR_COST { get; set; }    //老人成本

		public string CURRENCY { get; set; }       //幣別
		public string RATE { get; set; }           //匯率
	}
}
