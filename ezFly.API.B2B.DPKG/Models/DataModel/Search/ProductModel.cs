using ezFly.API.B2B.DPKG.Models.DataModel.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezFly.API.B2B.DPKG.Models.DataModel.Product
{
	public class ProductModel
	{
		public string PROD_NO { get; set; }     //商品編號
		public string PROD_NAME { get; set; }   //商品名稱

		public string SALE_S_DATE { get; set; } //銷售起日
		public string SALE_E_DATE { get; set; } //銷售迄日

		public string PROD_DESC1 { get; set; }  //內容包含
		public string PROD_DESC2 { get; set; }  //內容不包含
		public string PROD_DESC3 { get; set; }  //注意事項

		public string REF_PRICE { get; set; }   //參考價

		public List<HotelComboModel> HTL_COMBO{ get; set; }   //飯店組合編號
	}
}

