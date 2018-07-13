using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezFly.API.B2B.DPKG.Models.DataModel.Product
{
    public class DcarItemModel
    {
		public string BRAND_INFO { get; set; }    //廠牌
		public string CAR_TYPE { get; set; }      //車款
		public string CAPACITY { get; set; }      //乘客人數
		public string USE_HOUR { get; set; }      //可使用小時
		public string CAR_CC { get; set; }        //排氣量
		public string PIC_URL1 { get; set; }      //車輛照片
		public string PRICE { get; set; }         //價格
		public string COST { get; set; }          //成本
		public string CURRENCY { get; set; }      //幣別
		public string RATE { get; set; }          //匯率
	}
}
