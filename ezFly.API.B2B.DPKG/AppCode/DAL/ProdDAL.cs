using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using ezFly.API.B2B.DPKG.Models.DataModel.Product;
using ezFly.API.B2B.DPKG.Models.DataModel.Search;

namespace ezFly.API.B2B.DPKG.AppCode.DAL
{
	public class ProdDAL
	{

		// 取得自由行商品內容
		public static DataSet GetProduct(SearchProdRQModel rq)
		{
			OracleConnection ora_conn = new OracleConnection(Website.Instance.ERP_DB);

			DataSet ds = null;

			try
			{
			
				string sqlStmt = @"SELECT P.PROD_NO,P.PROD_NAME,
P.CITY_TO,P.SALE_S_DATE,P.SALE_E_DATE,D.PROD_DESC1,D.PROD_DESC2,D.PROD_DESC3,
P.REF_PRICE1 AS REF_PRICE
FROM PRODUCT P
LEFT JOIN PROD_DPKG D ON P.PROD_NO=D.PROD_NO
WHERE P.PROD_TYPE1 ='DPKG'
AND P.CITY_TO = :CITY_TO
AND P.PROD_STATUS1 = 'QS'
AND (:S_DATE BETWEEN P.SALE_S_DATE AND P.SALE_E_DATE)
AND ( P.PROD_TYPE1='DPKG' AND NVL(P.REF_PRICE1,0) >0 )
ORDER BY P.PROD_NO
";

				OracleParameter[] sqlParams = new OracleParameter[] {
					new OracleParameter("CITY_TO", rq.CITYTO.ToUpper()),
					new OracleParameter("S_DATE", rq.SDATE)
				};

				ds = OracleHelper.ExecuteDataset(Website.Instance.ERP_DB, CommandType.Text, sqlStmt, sqlParams);

			}
			catch (Exception ex)
			{

				Website.Instance.logger.FatalFormat("{0},{1}", ex.Message, ex.StackTrace);
			}

			return ds;
		}


        //取得商品綁定每晚飯店 
        public static DataSet GetBundleHotels(string prod_no, string htl_no,string s_date)
        {            

            DataSet ds = null;
            try
            {
         
                string sqlStmt = @"SELECT DISTINCT  HTL.PROD_NO AS H_PRODNO, HTL.HTL_NO ,INFO.HTL_NAME,HTL.STATUS,    
    HTL.LIVE_NIGHT AS DAY, PROJ.SUP_XID, PROJ.XID AS PROJ_XID,HTL.SHUTTLE_FLAG, HTL.SORT,HTL.PROFIT,
    ROOM.ROOM_XID, ROOM.XID AS PROJROOMXID
FROM PROD_DPKG_HOTEL HTL
LEFT JOIN BC_HOTEL_PROJ PROJ ON HTL.HTL_NO = PROJ.HTL_NO AND HTL.SUP_XID = PROJ.SUP_XID AND HTL.PROJ_XID = PROJ.XID
LEFT JOIN BC_HOTEL_PROJ_ROOM ROOM ON PROJ.SUP_XID = ROOM.SUP_XID AND PROJ.XID = ROOM.PROJ_XID AND ROOM.PROJ_ROOM_STATUS = '01'
LEFT JOIN BC_HOTEL_INFO INFO ON HTL.HTL_NO=INFO.HTL_NO
WHERE HTL.PROD_NO = :PROD_NO
AND HTL.HTL_NO = : HTL_NO
AND HTL.STATUS = 'QS'    
AND EXISTS (SELECT * FROM PROD_DPKG_SALE_RULE_MST WHERE PROD_NO=:PROD_NO AND (:S_DATE BETWEEN PROD_S_DATE AND PROD_E_DATE))    
ORDER BY HTL.PROD_NO, HTL.LIVE_NIGHT, HTL.SORT
";

                OracleParameter[] sqlParams = new OracleParameter[] {
                    new OracleParameter("PROD_NO", prod_no),
                    new OracleParameter("HTL_NO", htl_no),
                    new OracleParameter("S_DATE", s_date)
                };

                ds= OracleHelper.ExecuteDataset(Website.Instance.ERP_DB, CommandType.Text, sqlStmt, sqlParams);
            }
            catch (Exception ex)
            {
                Website.Instance.logger.FatalFormat("{0},{1}", ex.Message, ex.StackTrace);
                throw ex;
            }
            return ds;
        }

    }
}