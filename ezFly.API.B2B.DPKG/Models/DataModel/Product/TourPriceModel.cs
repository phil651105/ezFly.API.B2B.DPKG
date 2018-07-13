using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezFly.API.B2B.DPKG.Models.DataModel.Product
{
    public class TourPriceModel
    {
		public string PRICE_COND { get; set; }        //票種
		public string PRICE_COND_NAME { get; set; }   //票種名稱
		public string AGE_S_LIMIT { get; set; }       //最小適用年齡
		public string AGE_E_LIMIT { get; set; }       //最大適用年齡

		public string PRICE { get; set; }             //售價
		public string COST { get; set; }              //成本
		public string CURRENCY { get; set; }          //幣別
		public string RATE { get; set; }              //匯率
	}
}
