using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezFly.API.B2B.DPKG.Models.DataModel.Product
{
    public class TketDetailModel
    {
		public string TKET_NO { get; set; }       //票券編號
		public string TKET_NAME { get; set; }     //票券名稱
		public string SUP_NO { get; set; }        //供應商編號

		public string PRICE_TYPE { get; set; }    //價格別
		public string PRICE_TYPE1 { get; set; }   //價格別名稱
		public string PROD_DESC1 { get; set; }    //適用範圍
		public string PROD_DESC2 { get; set; }    //使用說明
		public string PROD_DESC3 { get; set; }    //備註說明	
		public string PROD_LIMIT { get; set; }    //注意事項
		public string VALID_S_DATE { get; set; }  //效期起日
		public string VALID_E_DATE { get; set; }  //效期迄日

		public string CAN_PAY_FLAG { get; set; }  //是KK還是HL
		public string CAN_DATE_FLAG { get; set; } //是否須指定使用日期
		public string QTY_KK { get; set; }        //可售數量

		public List<TketPriceModel> TKET_PRICE { get; set; }   //票券價格
	}
}
