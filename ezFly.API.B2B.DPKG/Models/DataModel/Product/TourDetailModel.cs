using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezFly.API.B2B.DPKG.Models.DataModel.Product
{
    public class TourDetailModel
    {
		public string TOUR_NO { get; set; }			    //行程編號
		public string TOUR_NAME { get; set; }           //行程名稱
		public string SUP_NO { get; set; }              //供應商編號

		public string STATUS { get; set; }				//狀態
		public string TOUR_PLACE { get; set; }		    //集合地點
		public string REG_MAN { get; set; }				//連絡人
		public string REG_TEL { get; set; }				//連絡電話
		public string REG_EMAIL { get; set; }           //連絡信箱
		public string USE_TIME { get; set; }            //指定時段(可選)
		public string HTL_SHUTTLE { get; set; }		    //是否有接送服務
		public string TOUR_DESC1 { get; set; }			//內容包含
		public string TOUR_DESC2 { get; set; }			//內容不包含
		public string CAN_PAY_FLAG { get; set; }        //是KK還是HL
		public string CAN_DATE_FLAG { get; set; }       //是否須指定使用日期
		public string ENFORCE_DAY { get; set; }         //指定的使用日期

		public List<TourPriceModel> TOUR_PRICE { get; set; }   //行程價格
	}
}
