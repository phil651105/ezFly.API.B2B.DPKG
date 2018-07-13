using System.ComponentModel;

namespace ezFly.API.B2B.DPKG.Models.DataModel
{
    public class HotelRoomPriceCostModel
    {
        public string CHECK_IN { get; set; }

        [DefaultValue("")]
        public string PRICE_1 { get; set; }     //1人入住售價
        [DefaultValue("")]
        public string PRICE_2 { get; set; }     //2人入住售價
        [DefaultValue("")]
        public string PRICE_3 { get; set; }     //3人入住售價
        [DefaultValue("")]
        public string PRICE_4 { get; set; }     //4人入住售價
        [DefaultValue("")]
        public string PRICE_5 { get; set; }     //5人入住售價
        [DefaultValue("")]
        public string PRICE_6 { get; set; }     //6人入住售價
        [DefaultValue("")]
        public string PRICE_7 { get; set; }     //7人入住售價
        [DefaultValue("")]
        public string PRICE_8 { get; set; }     //8人入住售價

        public string MESSAGE { get; set; }               //回傳訊息內容
        public string STATUS_CODE { get; set; }           //回傳狀態碼(00:成功)
    }
}