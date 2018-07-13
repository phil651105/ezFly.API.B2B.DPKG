using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezFly.API.B2B.DPKG.Models.DataModel.Product
{
    public class TrafficQtyModel
    {
		public string QTY_XID { get; set; }

		public string S_DATE { get; set; }          //日期
		public string FORWARD_FLAG { get; set; }    //去回註記(1去程/2回程)
		public string STATUS { get; set; }          //上下架狀態
		public string QTY_TYPE { get; set; }        //位控類型(虛擬位/後補/禁售)
		public string QTY_KK { get; set; }          //虛擬位控
		public string EMBARGO_FLY_NO { get; set; }  //單日禁售航班
	}
}
