using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezFly.API.B2B.DPKG.Models.DataModel.Product
{
    public class DcarDetailModel
    {
		public string DCAR_NO { get; set; }             //租車編號
		public string DCAR_NAME { get; set; }           //租車名稱
		public string SUP_NO { get; set; }              //供應商編號

		public string CAR_NOTE1 { get; set; }           //租車規定
		public string CAR_NOTE2 { get; set; }           //取車須知說明
		public string CAR_NOTE3 { get; set; }           //用車須知說明
		public string CAR_NOTE4 { get; set; }           //還車須知說明
		public string CAR_NOTE5 { get; set; }           //取消規定&連絡方式
		public string REG_MAN { get; set; }             //連絡人
		public string REG_TEL { get; set; }             //連絡電話
		public string REG_EMAIL { get; set; }           //連絡信箱
		public string CAN_PAY_FLAG { get; set; }        //是KK還是HL
		public string CAN_DATE_FLAG { get; set; }       //是否須指定使用日期

		public List<DcarItemModel> DCAR_ITEMS { get; set; }       //租車詳細價格
		public List<DcarLocationModel> DCAR_LOCAL { get; set; }   //租車地點資訊
	}
}
