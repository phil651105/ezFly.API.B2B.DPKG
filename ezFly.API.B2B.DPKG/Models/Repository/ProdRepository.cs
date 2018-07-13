using ezFly.API.B2B.DPKG.AppCode.DAL;
using ezFly.API.B2B.DPKG.Models.DataModel.Product;
using ezFly.API.B2B.DPKG.Models.DataModel.Search;
using System;
using System.Collections.Generic;
using System.Data;

namespace ezFly.API.B2B.DPKG.Models.Repository
{
    public class ProdRepository
    {
        // 依區間取得自由行商品
        public static List<ProductModel> GetProd(SearchProdRQModel list_rq)
        {
            List<ProductModel> prod_list = new List<ProductModel>();

            try
            {
                DataSet ds = ProdDAL.GetProduct(list_rq);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var model = new ProductModel();

                    model.PROD_NO = dr.ToStringEx("PROD_NO");
                    model.PROD_NAME = dr.ToStringEx("PROD_NAME");
                    model.SALE_S_DATE = dr.ToStringEx("SALE_S_DATE");
                    model.SALE_E_DATE = dr.ToStringEx("SALE_E_DATE");
                    model.PROD_DESC1 = dr.ToStringEx("PROD_DESC1");
                    model.PROD_DESC2 = dr.ToStringEx("PROD_DESC2");
                    model.PROD_DESC3 = dr.ToStringEx("PROD_DESC3");
                    model.REF_PRICE = dr.ToStringEx("REF_PRICE");

                    #region - 滿足HTL_COMBO -

                    var prod_no = model.PROD_NO;
                    List<HotelComboModel> htlcombos_lst = new List<HotelComboModel>();
                    DataSet dsHtl = HotelDAL.QuryHtlCombo(prod_no);
                    if (dsHtl != null && dsHtl.Tables[0].Rows.Count > 0)
                    {
                        int h = dsHtl.Tables[0].Rows.Count;
                        string[] htl_name = new string[h];  // 藍映海岸景觀民宿_馬祖麗堤民宿
                        string[] htl_no = new string[h];   //DHTL000002263_DHTL000000457

                        string ht1 = dsHtl.Tables[0].Rows[0]["HTL_NAME1"].ToString();
                        string ht2 = dsHtl.Tables[0].Rows[0]["HTL_NAME2"].ToString();
                        string ht3 = dsHtl.Tables[0].Rows[0]["HTL_NAME3"].ToString();
                        string ht4 = dsHtl.Tables[0].Rows[0]["HTL_NAME4"].ToString();

                        foreach (string x in htl_name)
                        {
                            h--;

                            if (ht1 != "" && ht2 != "" && ht3 != "" && ht4 != "")//如果每晚都不空
                            {
                                htl_name[h] = string.Format("{0}_{1}_{2}_{3}", dsHtl.Tables[0].Rows[h]["HTL_NAME1"].ToString(), dsHtl.Tables[0].Rows[h]["HTL_NAME2"].ToString(), dsHtl.Tables[0].Rows[h]["HTL_NAME3"].ToString(), dsHtl.Tables[0].Rows[h]["HTL_NAME4"].ToString());

                                htl_no[h] = dsHtl.Tables[0].Rows[h]["HTL_NO1"].ToString() + "_" + dsHtl.Tables[0].Rows[h]["HTL_NO2"].ToString() + "_" + dsHtl.Tables[0].Rows[h]["HTL_NO3"].ToString() + "_" + dsHtl.Tables[0].Rows[h]["HTL_NO4"].ToString();
                            }
                            else
                            {
                                if (ht4 == "")
                                {
                                    htl_name[h] = string.Format("{0}_{1}_{2}", dsHtl.Tables[0].Rows[h]["HTL_NAME1"].ToString(), dsHtl.Tables[0].Rows[h]["HTL_NAME2"].ToString(), dsHtl.Tables[0].Rows[h]["HTL_NAME3"].ToString());
                                    htl_no[h] = dsHtl.Tables[0].Rows[h]["HTL_NO1"].ToString() + "_" + dsHtl.Tables[0].Rows[h]["HTL_NO2"].ToString() + "_" + dsHtl.Tables[0].Rows[h]["HTL_NO3"].ToString();
                                }
                                if (ht3 == "" && ht4 == "")
                                {
                                    htl_name[h] = string.Format("{0}_{1}", dsHtl.Tables[0].Rows[h]["HTL_NAME1"].ToString(), dsHtl.Tables[0].Rows[h]["HTL_NAME2"].ToString());

                                    htl_no[h] = dsHtl.Tables[0].Rows[h]["HTL_NO1"].ToString() + "_" + dsHtl.Tables[0].Rows[h]["HTL_NO2"].ToString();
                                }
                                if (ht2 == "" && ht3 == "" && ht4 == "")
                                {
                                    htl_name[h] = string.Format("{0}", dsHtl.Tables[0].Rows[h]["HTL_NAME1"].ToString());
                                    htl_no[h] = dsHtl.Tables[0].Rows[h]["HTL_NO1"].ToString();
                                }
                                if (ht1 == "" && ht2 == "" && ht3 == "" && ht4 == "")
                                {
                                    htl_name[h] = "無飯店";
                                }
                            }
                        }

                        for (int nIndex = 0; nIndex < htl_name.Length; nIndex++)
                        {
                            // if (htlcombos_lst.Where(c => c.HOTEL_NAME.Equals(htl_name[nIndex])).Count() > 0) continue;
                            htlcombos_lst.Add(new HotelComboModel()
                            {
                                HTL_NAME = htl_name[nIndex],
                                HTL_NO = htl_no[nIndex],
                            });
                        }
                    }
                    else
                    {
                        htlcombos_lst.Add(new HotelComboModel()
                        {
                            HTL_NAME = "無飯店",
                            HTL_NO = ""
                        });
                    }
                    model.HTL_COMBO = htlcombos_lst;

                    #endregion - 滿足HTL_COMBO -

                    prod_list.Add(model);
                }
            }
            catch (Exception ex)
            {
                Website.Instance.logger.FatalFormat("{0},{1}", ex.Message, ex.StackTrace);
            }

            return prod_list;
        }

        // 依產編取得自由行商品細節
        public static ProdDetailModel GetProdDetail(ProdDetailRQModel req)
        {
            ProdDetailModel prod = new ProdDetailModel();

            try
            {
				#region 取得交通清單

				//取得航段資訊
				List<TrafficModel> traffics = TrafficRepository.GetTrafficSectors(req);


				// 交通位控
				TRAFFIC_QTYS = Qty;
				// 交通時刻表
				TRAFFIC_TIMETBS = TimeTable;

				// 航段可用位控
				//TrafficQtyModel Qty = "";

				// 航段時刻表
				if (dr.ToStringEx("CARRIER_CODE") == "B7" || dr.ToStringEx("CARRIER_CODE") == "AE")
				{
					List<TrafficTimeTableModel> TimeTable = QueryCrsTimeTable(dr.ToStringEx("PROD_NO"), dr.ToStringEx("CARRIER_CODE"), dr.ToDateTime("S_DATE"), sectors, total_psg);
				}
				else
				{
					//List <TrafficTimeTableModel> TimeTable = GetApkgTimeTable(dr.ToStringEx("PROD_NO"), dr.ToDateTime("S_DATE"), dr.ToStringEx("CARRIER_CODE"), dr.ToStringEx("SECTOR"), total_psg);
					//string prod_no, DateTime s_date, string carrier_code,List<TrafficTimeTableModel> sectors, int total_psg
				}
				#endregion 取得交通清單

				#region 取得飯店清單
				string[] htlnoArray = rq.HTLNO.Split("_");//切割hotel combo , 帶入每晚飯店編號
                List<HotelModel> htls = new List<HotelModel>();
                foreach (var htlno in htlnoArray)
                {
                     htls = HotelRepository.LoadHotels(htls, rq.PRODNO, htlno, rq.SDATE, rq.EDATE);

                    if (htls == null)
                    {
                        prod.PROD_NO = rq.PRODNO;
                        prod.MESSAGE = "查無可用飯店";
                        prod.STATUS_CODE = "01";//失敗
                    }
                    else
                    {
                        prod.PROD_NO = rq.PRODNO;
                        prod.MESSAGE = "查飯店成功";
                        prod.STATUS_CODE = "00";//成功
                        prod.HOTELS = htls;
                    }
                }
				#endregion 取得飯店清單
			}
			catch (Exception ex)
            {
                Website.Instance.logger.FatalFormat("{0},{1}", ex.Message, ex.StackTrace);
            }

            return prod;
        }
    }
}