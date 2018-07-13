using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezFly.API.B2B.DPKG.Models.DataModel.Product
{
    public class HotelModel
    {
		public string HOTEL_NO { get; set; }    //飯店編號
		public string HOTEL_NAME { get; set; }  //飯店名稱
		public string SUP_NO { get; set; }      //供應商代碼
		public string PROFIT { get; set; }      //調整價格(利潤)
		public string DAY { get; set; }         //實際入住日

        public string MESSAGE { get; set; }               //回傳訊息內容
        public string STATUS_CODE { get; set; }           //回傳狀態碼(00:成功)

        public List<HotelProjectModel> HOTEL_PROJS { get; set; }     //飯店專案
	}
}
