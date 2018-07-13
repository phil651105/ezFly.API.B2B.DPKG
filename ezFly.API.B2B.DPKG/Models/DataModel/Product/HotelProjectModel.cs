using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezFly.API.B2B.DPKG.Models.DataModel.Product
{
    public class HotelProjectModel
    {
        public string PROJ_XID { get; set; }
        public string INC_BREAKFAST { get; set; }      //是否含早餐
		public string INC_TEATIME { get; set; }        //是否含下午茶
		public string INC_DINNER { get; set; }         //是否含晚餐
		public string IS_TAIWANESE_ONLY { get; set; }  //是否台灣人限定
		public string SHUTTLE_FLAG { get; set; }       //是否有飯店接送

        public string MESSAGE { get; set; }               //回傳訊息內容
        public string STATUS_CODE { get; set; }           //回傳狀態碼(00:成功)

        public List<HotelProjectRoomModel> HOTEL_ROOMS { get; set; }         //飯店房型
	}
}
