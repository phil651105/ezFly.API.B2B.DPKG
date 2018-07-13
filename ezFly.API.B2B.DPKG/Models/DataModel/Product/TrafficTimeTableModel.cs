using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezFly.API.B2B.DPKG.Models.DataModel.Product
{
    public class TrafficTimeTableModel
    {
		public string TIMETB_XID { get; set; }
	
		public string S_DATE { get; set; }         //日期
		public string FORWARD_FLAG { get; set; }   //去回註記(1去程/2回程)
		public string CARRIER_CODE { get; set; }   //航空公司代碼
		public string CARRIER_NAME { get; set; }   //航空公司名稱
		public string FLY_NO { get; set; }         //班次
		public string DEP_FROM { get; set; }       //交通起點
		public string DEP_FROM_NAME { get; set; }  //交通起點名稱
		public string DEP_FROM_TIME { get; set; }  //出發時間
		public string ARR_TO { get; set; }         //交通迄點
		public string ARR_TO_NAME { get; set; }    //交通迄點名稱
		public string ARR_TO_TIME { get; set; }    //抵達時間
		public string FLY_HL_FLAG { get; set; }    //是否候補(限飛機)
		public string ARNK_FLAG { get; set; }      //回程航程斷開--依回程航段指示查位
		public string MIN_STAY_DAYS { get; set; }  //最短停留天數
		public string MAX_STAY_DAYS { get; set; }  //最長停留天數
		public string VALID_S_DATE { get; set; }   //效期起日
		public string VALID_E_DATE { get; set; }   //效期迄日
		public string IS_HOLIDAY { get; set; }     //是否為假日
		public string BOOKING_CLASS { get; set; }  //訂位艙等
		public string SEAT_COUNT { get; set; }     //可用空位
		public string FARE_BASIS { get; set; }     //票面價代碼(B7/AE)
		public string CAN_HL { get; set; }         //可否後補
	}
}







