using ezFly.API.B2B.DPKG.AppCode.DAL;
using ezFly.API.B2B.DPKG.Models.DataModel;
using ezFly.API.B2B.DPKG.Models.DataModel.Product;
using System;
using System.Collections.Generic;
using System.Data;

namespace ezFly.API.B2B.DPKG.Models.Repository
{
    public class HotelRepository
    {
        // 取得商品綁定飯店清單-精簡版
        public static List<HotelModel> LoadHotels(List<HotelModel> hotels, string prod_no, string htl_no, string s_date, string e_date)
        {
            try
            {
                DataSet ds = ProdDAL.GetBundleHotels(prod_no, htl_no, s_date);

                if (ds != null && ds.Tables[0].Rows.Count >0)
                {
                    var model = new HotelModel();
                    model.HOTEL_NO = ds.Tables[0].Rows[0].ToStringEx("HTL_NO");
                    model.HOTEL_NAME = ds.Tables[0].Rows[0].ToStringEx("HTL_NAME");
                    model.SUP_NO = ds.Tables[0].Rows[0].ToStringEx("SUP_NO");
                    model.PROFIT = ds.Tables[0].Rows[0].ToStringEx("PROFIT");
                    model.DAY = ds.Tables[0].Rows[0].ToStringEx("DAY");
                    model.MESSAGE = "查詢每晚飯店成功";
                    model.STATUS_CODE = "00";

                    List<HotelProjectModel> htlprojs = new List<HotelProjectModel>();
                    DataSet dsHtlProjs = HotelDAL.GetProject(prod_no, htl_no, s_date);

                    if (dsHtlProjs != null && dsHtlProjs.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drProj in dsHtlProjs.Tables[0].Rows)
                        {
                            var pjm = new HotelProjectModel();
                            pjm.PROJ_XID = drProj.ToStringEx("PROJ_XID");
                            pjm.INC_BREAKFAST = drProj.ToStringEx("BREAKFAST");
                            pjm.INC_DINNER = drProj.ToStringEx("DINNER");
                            pjm.INC_TEATIME = drProj.ToStringEx("AFTERNOON");
                            pjm.IS_TAIWANESE_ONLY = drProj.ToStringEx("TAIWAN_ONLY");
                            pjm.SHUTTLE_FLAG = drProj.ToStringEx("SHUTTLE_FLAG");
                            pjm.MESSAGE = "查詢飯店專案成功";
                            pjm.STATUS_CODE = "00";

                            //抓 PROJ_XID底下的PROJ_ROOM_XID
                            string PROJ_ROOM_XIDS = "";
                            DataSet proj_rooms = HotelDAL.GetProjRoomXid(htl_no, pjm.PROJ_XID, s_date);// string.Join(",", proj_room_xid);
                            foreach (DataRow room in proj_rooms.Tables[0].Rows)
                            {
                                PROJ_ROOM_XIDS += room.ToStringEx("PROJ_ROOM_XID") + ",";
                            }
                            char[] Char = { ',' };//刪掉最後一個逗點
                            List<HotelProjectRoomModel> rooms = new List<HotelProjectRoomModel>();
                            DataSet dsRoom = null;
                            //房控數 >=1
                            if (PROJ_ROOM_XIDS != "")
                            {
                                dsRoom = HotelDAL.GetAvailableProjectRoom(htl_no, s_date, pjm.PROJ_XID, PROJ_ROOM_XIDS.TrimEnd(Char));

                                if (dsRoom != null && dsRoom.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataRow drRoom in dsRoom.Tables[0].Rows)
                                    {
                                        List<HotelRoomPriceCostModel> price = new List<HotelRoomPriceCostModel>();
                                        DataSet dsPrice = HotelDAL.GetRoomPrice(htl_no, pjm.PROJ_XID, s_date, e_date);

                                        var pjr = new HotelProjectRoomModel();
                                        pjr.ROOM_XID = drRoom.ToStringEx("ROOM_XID");
                                        DataSet dsQH = HotelDAL.GetQtykkHlflag(htl_no, pjr.ROOM_XID, s_date);

                                        pjr.PROJ_ROOM_XID = drRoom.ToStringEx("PROJ_ROOM_XID");//房型名稱
                                        pjr.ROOM_NAME = drRoom.ToStringEx("ROOM_NAME");
                                        pjr.ROOM_IMG = drRoom.ToStringEx("ROOM_IMG");
                                        pjr.IS_ALLOW_HL = dsQH.Tables[0].Rows[0].ToStringEx("HL_FLAG");
                                        pjr.QTY_KK = dsQH.Tables[0].Rows[0].ToStringEx("QTY_KK");//雙人房有無房間  剩餘房數
                                        pjr.STATUS_CODE = "00";
                                        pjr.MESSAGE = "查詢房型成功";

                                        if (dsPrice != null && dsPrice.Tables[0].Rows.Count > 0)
                                        {
                                            var prm = new HotelRoomPriceCostModel();
                                            prm.CHECK_IN = dsPrice.Tables[0].Rows[0].ToStringEx("PRICE_DATE");

                                            prm.PRICE_1 = dsPrice.Tables[0].Rows[0].ToStringEx("PRICE_1");
                                            prm.PRICE_2 = dsPrice.Tables[0].Rows[0].ToStringEx("PRICE_2");
                                            prm.PRICE_3 = dsPrice.Tables[0].Rows[0].ToStringEx("PRICE_3");
                                            prm.PRICE_4 = dsPrice.Tables[0].Rows[0].ToStringEx("PRICE_4");
                                            prm.PRICE_5 = dsPrice.Tables[0].Rows[0].ToStringEx("PRICE_5");
                                            prm.PRICE_6 = dsPrice.Tables[0].Rows[0].ToStringEx("PRICE_6");
                                            prm.PRICE_7 = dsPrice.Tables[0].Rows[0].ToStringEx("PRICE_7");
                                            prm.PRICE_8 = dsPrice.Tables[0].Rows[0].ToStringEx("PRICE_8");

                                            prm.STATUS_CODE = "00";
                                            prm.MESSAGE = "查詢價格成功";

                                            price.Add(prm);
                                        }
                                        else
                                        {
                                            var prm = new HotelRoomPriceCostModel();
                                            prm.STATUS_CODE = "01";
                                            prm.MESSAGE = "查詢價格失敗";

                                            price.Add(prm);
                                        }

                                        pjr.HOTEL_PRICECOSTS = price;
                                        rooms.Add(pjr);
                                    }
                                }
                                else
                                {
                                    var pjr = new HotelProjectRoomModel();
                                    pjr.STATUS_CODE = "01";
                                    pjr.MESSAGE = "查詢房型失敗";

                                    rooms.Add(pjr);
                                }
                            }
                            else//候補
                            {
                                var pjr = new HotelProjectRoomModel();
                                pjr.STATUS_CODE = "01";
                                pjr.MESSAGE = "查詢房型失敗";

                                rooms.Add(pjr);
                            }

                            pjm.HOTEL_ROOMS = rooms;
                            htlprojs.Add(pjm);
                        }
                    }
                    else
                    {
                        var pjm = new HotelProjectModel();
                        pjm.MESSAGE = "查詢飯店專案失敗";
                        pjm.STATUS_CODE = "01";
                        htlprojs.Add(pjm);
                    }

                    model.HOTEL_PROJS = htlprojs;
                    hotels.Add(model);
                }
                else
                {
                    var model = new HotelModel();

                    model.MESSAGE = "查詢每晚飯店失敗";
                    model.STATUS_CODE = "01";

                    hotels.Add(model);
                }
                
            }
            catch (Exception ex)
            {
                Website.Instance.logger.FatalFormat("{0},{1}", ex.Message, ex.StackTrace);
            }

            return hotels;
        }
    }
}