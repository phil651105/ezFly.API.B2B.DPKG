using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezFly.API.B2B.DPKG.Models.DataModel.Product
{
    public class TrafficSectorCostModel
    {
		public long COST_XID { get; set; }

		public string ADULT_PRICE { get; set; }      //成人票價
		public string ADULT_COST { get; set; }       //成人成本
		public string CHILD_PRICE { get; set; }      //孩童票價
		public string CHILD_COST { get; set; }       //孩童成本
		public string SENIOR_PRICE { get; set; }     //老人票價
		public string SENIOR_COST { get; set; }      //老人成本

		public string ADULT_TOURCODE { get; set; }   //成人行程代碼(B7)
		public string CHILD_TOURCODE { get; set; }   //孩童行程代碼(B7)
		public string SENIOR_TOURCODE { get; set; }  //老人行程代碼(B7)

		public string ADULT_FAREBASIS { get; set; }  //成人票面價代碼
		public string CHILD_FAREBASIS { get; set; }  //孩童票面價代碼
		public string SENIOR_FAREBASIS { get; set; } //老人票面價代碼
	}
}
