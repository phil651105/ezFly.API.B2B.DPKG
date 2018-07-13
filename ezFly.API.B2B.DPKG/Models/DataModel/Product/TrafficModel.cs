using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezFly.API.B2B.DPKG.Models.DataModel.Product
{
    public class TrafficModel
    {
		public string PROD_NO { get; set; }            //產品編號
		public string TRAFFIC_XID { get; set; }        //costxid_qtyxid_timetbxid

		public string TRIP_WAY { get; set; }           //航程(來回RT/單程OW)
		public string FORWARD_FLAG { get; set; }       //去回註記(1去程/2回程)
		public string SECTOR { get; set; }             //交通區段編號
		public string TRAFFIC_TYPE { get; set; }       //交通類別
		public string TRAFFIC_NAME { get; set; }       //交通名稱
		public string SUP_NO { get; set; }             //供應商編號
		public string RETURN_FEE { get; set; }         //退票手續費
		public double RETURN_FEE_PERCENT { get; set; } //退票手續費百分比
		public string RULE_TYPE { get; set; }          //指定使用票規(AE)

		public TrafficSectorModel TRAFFIC_SECTOR { get; set; }      //交通航段
		public TrafficQtyModel TRAFFIC_QTYS { get; set; }           //交通位控
		public TrafficTimeTableModel TRAFFIC_TIMETBS { get; set; }  //交通時刻表
	}
}

