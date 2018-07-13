using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezFly.API.B2B.DPKG.Models.DataModel.Product
{
    public class HotelProjectRoomModel
    {
        public string PROJ_ROOM_XID { get; set; }
        public string ROOM_XID { get; set; }
        public string ROOM_NAME { get; set; }
        public string ROOM_IMG { get; set; }      //房間圖片
		public string IS_ALLOW_HL { get; set; }   //是否可以後補
		public string QTY_KK { get; set; }        // 剩餘間數

        public string MESSAGE { get; set; }               //回傳訊息內容
        public string STATUS_CODE { get; set; }           //回傳狀態碼(00:成功)

        public List<HotelRoomPriceCostModel> HOTEL_PRICECOSTS { get; set; }  //飯店價格
	}
}
