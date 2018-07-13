using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezFly.API.B2B.DPKG.Models.DataModel.Product
{
    public class DcarLocationModel
    {
		public string LOCATION_NAME { get; set; }    //車行名稱
		public string LOCATION_ADD { get; set; }     //車行地址
		public string LOCATION_TEL { get; set; }     //車行電話
		public string LOCATION_HOURS { get; set; }   //營業時間
	}
}
