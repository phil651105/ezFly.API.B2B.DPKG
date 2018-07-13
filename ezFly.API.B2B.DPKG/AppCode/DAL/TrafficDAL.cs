using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace ezFly.API.B2B.DPKG.AppCode.DAL
{
	public class TrafficDAL
	{
		public static DataSet GetTrafficSectors(string prod_no, string s_date, string e_date)
		{
			OracleConnection ora_conn = new OracleConnection(Website.Instance.ERP_DB);

			try
			{
				ora_conn.Open();

				string sqlStmt = @"
SELECT A.PROD_NO, A.SEQNO, A.TRAFFIC_XID, A.TRAFFIC_COST_XID, A.FORWARD_FLAG, A.SEC, A.TRAFFIC_TYPE, 
 A.CARRIER_CODE, E.CLASSIFY1 AS CARRIER_CODE_NAME, A.FLY_NAME,  A.CITY_FROM,  T1.C_NAME AS CITY_FROM_NAME, 
 A.CITY_TO,  T2.C_NAME AS CITY_TO_NAME, A.STAY_FLAG, A.STAY_NIGHT, A.ADD_DAY, 
 A.DEP_FROM, C1.NAME2 AS DEP_FROM_NAME, A.STATUS, A.ARR_TO,  C2.NAME2 AS ARR_TO_NAME, 
 C.TRIP_WAY, C.TRAFFIC_TYPE2, C.CLASS_BOOKING, C.CLASS_SERVICE, C.FLY_HL_FLAG, C.SUP_NO, 
 B.MIN_STAY_DAYS, B.MAX_STAY_DAYS, A.ARNK_FLAG,
 NULL AS ADD_PRICE_WEEKS, NULL AS TRAFFIC_COST_PRICE_XID, NULL AS VALID_S_DATE, NULL AS VALID_E_DATE, 
 NULL AS FARE, NULL AS RETURN_FEE, NULL AS RETURN_FEE_PERCENT, NULL AS RULE_TYPE,
 NULL AS ADT_TOURCODE, NULL AS ADT_TOURCODE_H,
 NULL AS CHD_TOURCODE, NULL AS CHD_TOURCODE_H, 
 NULL AS SEN_TOURCODE, NULL AS SEN_TOURCODE_H,
 NULL AS ADT_FAREBASIS, NULL AS ADT_FAREBASIS_H, 
 NULL AS CHD_FAREBASIS, NULL AS CHD_FAREBASIS_H, 
 NULL AS SEN_FAREBASIS, NULL AS SEN_FAREBASIS_H
FROM PROD_DPKG_FLY A
LEFT JOIN PROD_DPKG B ON A.PROD_NO=B.PROD_NO
LEFT JOIN PROD_DPKG_TRAFFIC C ON A.TRAFFIC_XID=C.XID
LEFT JOIN CITYS T1 ON A.CITY_FROM=T1.CITY_NO
LEFT JOIN CITYS T2 ON A.CITY_TO=T2.CITY_NO
LEFT JOIN MV_DPKG_CITY C1 ON A.DEP_FROM=C1.ID AND A.TRAFFIC_TYPE=C1.TRAFFIC_TYPE
LEFT JOIN MV_DPKG_CITY C2 ON A.ARR_TO=C2.ID AND A.TRAFFIC_TYPE=C2.TRAFFIC_TYPE
LEFT JOIN CODE E ON A.CARRIER_CODE=E.CODE_ID AND E.CODE_TYPE='DPKG_CARRIER_CODE'
WHERE A.PROD_NO=:PROD_NO AND A.STATUS='QS'
ORDER BY A.FORWARD_FLAG, A.SEQNO
";

				OracleParameter[] sqlParams = new OracleParameter[] {
					new OracleParameter("PROD_NO", prod_no),
				};

				DataSet ds = OracleHelper.ExecuteDataset(ora_conn, CommandType.Text, sqlStmt, sqlParams);

				sqlStmt = @"
SELECT * FROM (
   SELECT DISTINCT B.XID AS TRAFFIC_COST_PRICE_XID, B.COST_XID, 
    B.VALID_S_DATE, B.VALID_E_DATE, B.FARE, B.RETURN_FEE, B.RETURN_FEE_PERCENT, B.ADD_PRICE_WEEKS, B.RULE_TYPE, 
    B.ADULT_TOURCODE, B.ADULT_TOURCODE_H, B.CHD_TOURCODE, B.CHD_TOURCODE_H, B.OLD_TOURCODE, B.OLD_TOURCODE_H,
    B.ADULT_FARE_BASIS, B.ADULT_FARE_BASIS_H, B.CHD_FARE_BASIS, B.CHD_FARE_BASIS_H, B.OLD_FARE_BASIS, B.OLD_FARE_BASIS_H
  FROM PROD_DPKG_TRAFFIC_COST_DTL B 
  INNER JOIN PROD_DPKG_TRAFFIC_COST_DATE C ON B.COST_XID =C.COST_XID AND B.XID=C.COST_DTL_XID
  WHERE B.TRAFFIC_XID=:TRAFFIC_XID AND B.STATUS='QS'
    AND B.COST_XID=:TRAFFIC_COST_XID 
    AND :USE_DATE BETWEEN B.VALID_S_DATE AND B.VALID_E_DATE
    ORDER BY B.VALID_E_DATE ASC
) WHERE ROWNUM <= 1
";

				// 重新找出各段使用成本, 更新最適用記錄....
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					Int64 traffic_xid = dr.ToInt64("TRAFFIC_XID");
					Int64 traffic_cost_xid = dr.ToInt64("TRAFFIC_COST_XID");

					sqlParams = new OracleParameter[] {
						new OracleParameter("TRAFFIC_XID", traffic_xid),
						new OracleParameter("TRAFFIC_COST_XID", traffic_cost_xid),
						new OracleParameter("USE_DATE", dr.ToStringEx("FORWARD_FLAG").Equals("1") ? s_date : e_date)
					};

					try
					{
						DataSet cost_ds = OracleHelper.ExecuteDataset(ora_conn, CommandType.Text, sqlStmt, sqlParams);

						if (cost_ds != null && cost_ds.Tables.Count > 0 && cost_ds.Tables[0].Rows.Count > 0)
						{
							DataRow cost_dr = cost_ds.Tables[0].Rows[0];

							dr["TRAFFIC_COST_PRICE_XID"] = cost_dr["TRAFFIC_COST_PRICE_XID"];
							dr["VALID_S_DATE"] = cost_dr["VALID_S_DATE"];
							dr["VALID_E_DATE"] = cost_dr["VALID_E_DATE"];
							dr["FARE"] = cost_dr["FARE"];
							dr["RETURN_FEE"] = cost_dr["RETURN_FEE"];
							dr["RETURN_FEE_PERCENT"] = cost_dr["RETURN_FEE_PERCENT"];
							dr["ADD_PRICE_WEEKS"] = cost_dr["ADD_PRICE_WEEKS"];
							dr["RULE_TYPE"] = cost_dr["RULE_TYPE"];

							dr["ADT_TOURCODE"] = cost_dr["ADULT_TOURCODE"];
							dr["ADT_TOURCODE_H"] = cost_dr["ADULT_TOURCODE_H"];
							dr["CHD_TOURCODE"] = cost_dr["CHD_TOURCODE"];
							dr["CHD_TOURCODE_H"] = cost_dr["CHD_TOURCODE_H"];
							dr["SEN_TOURCODE"] = cost_dr["OLD_TOURCODE"];
							dr["SEN_TOURCODE_H"] = cost_dr["OLD_TOURCODE_H"];

							dr["ADT_FAREBASIS"] = cost_dr["ADULT_FARE_BASIS"];
							dr["ADT_FAREBASIS_H"] = cost_dr["ADULT_FARE_BASIS_H"];
							dr["CHD_FAREBASIS"] = cost_dr["CHD_FARE_BASIS"];
							dr["CHD_FAREBASIS_H"] = cost_dr["CHD_FARE_BASIS_H"];
							dr["SEN_FAREBASIS"] = cost_dr["OLD_FARE_BASIS"];
							dr["SEN_FAREBASIS_H"] = cost_dr["OLD_FARE_BASIS_H"];
						}
					}
					catch (Exception ex2)
					{
						Website.Instance.logger.FatalFormat("{0},{1}", ex2.Message, ex2.StackTrace);
					}
				}

				ora_conn.Close();
				return ds;
			}
			catch (Exception ex)
			{
				ora_conn.Close();
				throw ex;
			}
		}

		//根據航段找出所有符合指定日期及價格 & 加假規則
		public static DataSet QuryDailyFlyCost(object prod_no, object dept, object arrv, object s_date, object traffic_xid,
			object traffic_cost_xid)
		{
			try
			{
				String Str = @"SELECT * FROM (
    SELECT B.FLY_COUNT,C.TRIP_WAY,C1.NAME||'_'||C2.NAME AS FROMTO,A.*,
        TO_CHAR(TO_DATE(:S_DATE,'yyyyMMdd') + A.ADD_DAY,'yyyyMMdd') AS USE_DATE, 
        B.TOTAL_STAY_NIGHT
    FROM PROD_DPKG_FLY A
    LEFT JOIN PROD_DPKG B ON A.PROD_NO=B.PROD_NO
    LEFT JOIN PROD_DPKG_TRAFFIC C ON A.TRAFFIC_XID=C.XID
    LEFT JOIN MV_DPKG_CITY C1 ON A.TRAFFIC_TYPE=C1.TRAFFIC_TYPE AND C1.ID=A.CITY_FROM
    LEFT JOIN MV_DPKG_CITY C2 ON A.TRAFFIC_TYPE=C2.TRAFFIC_TYPE AND C2.ID=A.CITY_TO
    WHERE A.PROD_NO=:PROD_NO AND
        (A.TRAFFIC_XID=:TRAFFIC_XID OR (TRAFFIC_XID !=:TRAFFIC_XID AND SEC!=1 AND  SEC !=B.FLY_COUNT ) OR 
        (SEC=B.FLY_COUNT AND A.TRAFFIC_XID!=:TRAFFIC_XID AND  C.TRIP_WAY='OW' ))
    ORDER BY A.SEC
)A
INNER JOIN 
(
    SELECT A.XID,A.TRAFFIC_XID,B.VALID_S_DATE,B.VALID_E_DATE,B.ADD_PRICE_WEEKS,
        B.ADULT_COST,B.ADULT_COST_H,B.CHD_COST,B.CHD_COST_H,B.OLD_COST,B.OLD_COST_H,C.DATE_AT ,B.XID AS COST_DTL_XID
    FROM PROD_DPKG_TRAFFIC_COST A
    INNER JOIN PROD_DPKG_TRAFFIC_COST_DTL  B ON A.XID=B.COST_XID
    INNER JOIN PROD_DPKG_TRAFFIC_COST_DATE C ON B.COST_XID =C.COST_XID AND B.XID=C.COST_DTL_XID
    WHERE A.TRAFFIC_XID=:TRAFFIC_XID AND A.XID=:TRAFFIC_COST_XID AND A.STATUS='QS' AND B.STATUS='QS'
        AND TO_CHAR(SYSDATE,'yyyyMMdd')  <= B.VALID_E_DATE--*between B.VALID_S_DATE AND B.VALID_E_DATE*
    ORDER BY DATE_AT        
)B 
ON A.TRAFFIC_XID=B.TRAFFIC_XID AND A.TRAFFIC_COST_XID =B.XID
WHERE A.USE_DATE=B.DATE_AT
    AND A.DEP_FROM=:DEP_FROM AND A.ARR_TO=:ARR_TO
    AND NOT EXISTS ( SELECT 1 FROM PROD_DPKG_TRAFFIC_EMBARGO_DATE EMB WHERE B.COST_DTL_XID=EMB.COST_DTL_XID AND 
        EMB.COST_XID=A.TRAFFIC_COST_XID AND B.DATE_AT BETWEEN EMB.S_DATE AND EMB.E_DATE AND EMB.STATUS='QS' AND EMB.FORWARD_FLAG= A.FORWARD_FLAG )
                               
ORDER BY B.DATE_AT,FORWARD_FLAG
";

				OracleParameter[] Op = new OracleParameter[]{
					 new OracleParameter("PROD_NO",prod_no),
					 new OracleParameter("S_DATE", s_date),
					 new OracleParameter("TRAFFIC_XID", traffic_xid),
					 new OracleParameter("TRAFFIC_COST_XID", traffic_cost_xid),
					 new OracleParameter("DEP_FROM", dept),
					 new OracleParameter("ARR_TO", arrv)
				};

				return OracleHelper.ExecuteDataset(Website.Instance.ERP_DB, CommandType.Text, Str.ToString(), Op);
			}
			catch (Exception ex)
			{
				//Website.Instance.logger.FatalFormat("{0},{1}", ex.Message, ex.StackTrace);
				throw ex;
			}
		}
	}
}
