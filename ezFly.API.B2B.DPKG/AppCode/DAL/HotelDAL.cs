using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace ezFly.API.B2B.DPKG.AppCode.DAL
{
    public class HotelDAL
    {
        // 抓每晚的飯店組合
        public static DataSet QuryHtlCombo(String prod_no)
        {
            if (prod_no == null) return null;

            try
            {
                String Str = @"SELECT A.*,B.*,C.*,D.* FROM (
SELECT A.PROD_NO,A.HTL_NO AS HTL_NO1 ,A.PROJ_XID AS PROJ_XID1 ,D.HTL_NAME AS HTL_NAME1,E.PROJ_NAME AS PORJ_NAME1 FROM  PROD_DPKG_HOTEL A
LEFT JOIN BC_HOTEL_INFO D ON A.HTL_NO=D.HTL_NO
LEFT JOIN BC_HOTEL_PROJ E ON A.HTL_NO=E.HTL_NO AND A.PROJ_XID=E.XID
WHERE A.PROD_NO=:PROD_NO AND A.STATUS='QS' AND A.LIVE_NIGHT=1
)A
LEFT JOIN (
SELECT A.PROD_NO,A.HTL_NO AS HTL_NO2 ,A.PROJ_XID AS PROJ_XID2,D.HTL_NAME AS HTL_NAME2,E.PROJ_NAME AS PROJ_NAME2 FROM  PROD_DPKG_HOTEL A
LEFT JOIN BC_HOTEL_INFO D ON A.HTL_NO=D.HTL_NO
LEFT JOIN BC_HOTEL_PROJ E ON A.HTL_NO=E.HTL_NO AND A.PROJ_XID=E.XID
WHERE A.PROD_NO=:PROD_NO AND A.STATUS='QS' AND A.LIVE_NIGHT=2
)B ON A.PROD_NO=B.PROD_NO
LEFT JOIN (
SELECT A.PROD_NO,A.HTL_NO AS HTL_NO3 ,A.PROJ_XID AS PROJ_XID3,D.HTL_NAME AS HTL_NAME3,E.PROJ_NAME AS PROJ_NAME3 FROM  PROD_DPKG_HOTEL A
LEFT JOIN BC_HOTEL_INFO D ON A.HTL_NO=D.HTL_NO
LEFT JOIN BC_HOTEL_PROJ E ON A.HTL_NO=E.HTL_NO AND A.PROJ_XID=E.XID
WHERE A.PROD_NO=:PROD_NO AND A.STATUS='QS' AND A.LIVE_NIGHT=3
)C ON B.PROD_NO=C.PROD_NO
LEFT JOIN (
SELECT A.PROD_NO,A.HTL_NO AS HTL_NO4 ,A.PROJ_XID AS PROJ_XID4,D.HTL_NAME AS HTL_NAME4,E.PROJ_NAME AS PROJ_NAME4 FROM  PROD_DPKG_HOTEL A
LEFT JOIN BC_HOTEL_INFO D ON A.HTL_NO=D.HTL_NO
LEFT JOIN BC_HOTEL_PROJ E ON A.HTL_NO=E.HTL_NO AND A.PROJ_XID=E.XID
WHERE A.PROD_NO=:PROD_NO AND A.STATUS='QS' AND A.LIVE_NIGHT=4
)D ON C.PROD_NO=D.PROD_NO

";

                OracleParameter[] Op = new OracleParameter[]{
                     new OracleParameter("PROD_NO",prod_no)
                   };

                return OracleHelper.ExecuteDataset(Website.Instance.ERP_DB, CommandType.Text, Str.ToString(), Op);
            }
            catch (Exception ex)
            {
                Website.Instance.logger.FatalFormat("{0},{1}", ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        // 取得指定飯店專案
        public static DataSet GetProject(string prod_no, string htl_no, string check_in)
        {
            if (prod_no == null) return null;

            try
            {
                string sqlStmt = @"SELECT DISTINCT A.HTL_NO, A.XID AS PROJ_XID, A.SUP_XID, A.PROJ_NAME, A.PROJ_TYPE,
 A.SUP_DISC_TYPE, A.PROCESSDAYS, TO_CHAR(A.PROJ_DESC) AS PROJ_DESC, A.SORT, A.FAX, A.EMAIL, A.BREAKFAST_FLAG AS BREAKFAST,
 A.AFTERNOON_TEA_FLAG AS AFTERNOON, A.DINNER_FLAG AS DINNER, A.LIMIT_NIGHT, A.LIMIT_QTY, A.TAIWAN_ONLY, Q.HL_FLAG, S.SUP_NO,
 N.PRICE_FLAG, N.PRICE_FLAG_TYPE, N.PRICE_FLAG_VALUE, N.UN_CANCEL_FLAG, N.UN_CHANGE_FLAG, N.LAST_CHANGE_DAY,
 O.SHUTTLE_FLAG
FROM BC_HOTEL_PROJ A
LEFT JOIN BC_HOTEL_PROJ_ROOM B ON A.SUP_XID = B.SUP_XID AND A.XID = B.PROJ_XID AND B.PROJ_ROOM_STATUS = '01'
LEFT JOIN BC_HOTEL_ROOM_INFO Q ON B.ROOM_XID = Q.ROOM_XID AND B.HTL_NO = Q.HTL_NO AND B.SUP_XID = Q.SUP_XID AND Q.HTL_DATE =:CHECKIN
LEFT JOIN BC_HOTEL_SUP S ON A.SUP_XID=S.XID
LEFT JOIN (
    SELECT N1.HTL_NO, N1.PROJ_XID, N1.SUP_XID, N1.PRICE_FLAG, N1.PRICE_FLAG_TYPE, N1.PRICE_FLAG_VALUE, N2.*
    FROM BC_HOTEL_PROJ_CANCEL N1
    LEFT JOIN BC_HOTEL_PROJ_CANCEL_MID N2 ON N2.PROJ_CNL_XID = N1.XID
    WHERE N1.HTL_NO = :HTL_NO
    --AND  N1.PROJ_XID = :PROJ_XID
       AND (N2.PROD_S_DATE <= :CHECKIN AND :CHECKIN <= N2.PROD_E_DATE )
       AND ROWNUM = 1
) N ON A.HTL_NO=N.HTL_NO AND A.XID=N.PROJ_XID AND A.SUP_XID=N.SUP_XID
LEFT JOIN PROD_DPKG_HOTEL O ON O.HTL_NO=A.HTL_NO AND O.PROJ_XID=A.XID AND O.PROD_NO=:PROD_NO
WHERE A.HTL_NO=:HTL_NO
--AND A.XID=:PROJ_XID
ORDER BY A.SORT
";

                List<OracleParameter> sqlParams = new List<OracleParameter>();

                sqlParams.Add(new OracleParameter("PROD_NO", prod_no));
                sqlParams.Add(new OracleParameter("HTL_NO", htl_no));
                sqlParams.Add(new OracleParameter("CHECKIN", check_in));

                return OracleHelper.ExecuteDataset(Website.Instance.ERP_DB, CommandType.Text, sqlStmt, sqlParams.ToArray());
            }
            catch (Exception ex)
            {
                Website.Instance.logger.FatalFormat("{0},{1}", ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        //  抓取專案可用房型清單
        public static DataSet GetAvailableProjectRoom(string htl_no, string check_in, string proj_xid,string str_proj_rooms)
        {
            DataSet ds = null;

            try
            {
                string sqlStmt = @"SELECT A.*, B.PROJ_DESC, D.ROOM_NOTE, D.ROOM_DESC, D.ROOM_QTY, D.ROOM_VIEW AS ROOM_VIEW_DESC, 
 D.ROOM_SIZE_INC_BALCONY, D.ROOM_SIZE, D.ROOM_SIZE_UNIT, D.BEDTYPE_XID AS BEDTYPE_ID, F.BEDTYPE_NAME, F.BEDTYPE_ENAME, 
 F.BEDTYPE_SIZE, D.LIMIT_GUEST, D.EXTRA_BED, D.LIMIT_BABY, D.LIMIT_CHILD, D.BABY_BED, F.XID AS BEDTYPE_XID,
 F.BEDTYPE_NAME, F.BEDTYPE_ENAME, F.BEDTYPE_SIZE, F.SELECT_FLAG, P.PIC_LINK AS ROOM_IMG
FROM (
    SELECT DISTINCT A.HTL_NO, A.XID AS PROJ_XID, B.XID AS PROJ_ROOM_XID, A.SUP_XID, A.PROJ_NAME, A.BREAKFAST_FLAG, 
      A.AFTERNOON_TEA_FLAG, A.DINNER_FLAG, A.TAIWAN_ONLY, A.SORT AS PROJ_SORT,
      R.XID AS ROOM_XID, R.ROOM_NAME, R.ROOM_ENAME, R.PROFILE_XID, H.ROOM_FACILITIES
    FROM BC_HOTEL_PROJ A
    JOIN BC_HOTEL_PROJ_ROOM B ON B.PROJ_XID = A.XID
    JOIN BC_HOTEL_INFO H ON H.HTL_NO = A.HTL_NO
    JOIN BC_HOTEL_ROOM R ON R.XID = B.ROOM_XID  
    WHERE 1=1
     AND A.HTL_NO=:HTL_NO
     AND A.XID=:PROJ_XID
     AND B.XID IN (" + str_proj_rooms + @")
     AND TO_CHAR(SYSDATE, 'YYYYMMDDHH24MI') < :CHECKIN || '1200'
     AND TO_CHAR(SYSDATE + H.PROCESSDAYS, 'YYYYMMDD') <= :CHECKIN
     AND TO_DATE(A.SALE_S_DATE || A.SALE_S_TIME, 'YYYYMMDDHH24MI') <= SYSDATE
     AND TO_DATE(A.SALE_E_DATE || A.SALE_E_TIME, 'YYYYMMDDHH24MI') >= SYSDATE
     AND A.PROJ_STATUS = '01' AND A.APPROVAL_FLAG = '1' AND B.PROJ_ROOM_STATUS = '01' AND R.STATUS = '01'
     --AND (A.LIMIT_NIGHT <= :STAY_DAYS AND MOD(:STAY_DAYS, A.LIMIT_NIGHT) = 0)
    -- AND A.LIMIT_QTY <= :ROOM_QTY  
     AND A.XID NOT IN (SELECT PROJ_XID FROM BC_HOTEL_PROJ_EMBARGO_DATE WHERE EMBARGO_STATUS = '01' AND :CHECKIN >= PROD_S_DATE AND :CHECKIN <= PROD_E_DATE )
     AND ( NVL(A.SUP_DISC_TYPE, '0000') NOT IN ('0001','0002') 
            OR (NVL(A.SUP_DISC_TYPE, '0000') = '0001' AND TO_CHAR(SYSDATE + A.PROCESSDAYS, 'YYYYMMDD') <= :CHECKIN) 
            OR (NVL(A.SUP_DISC_TYPE, '0000') = '0002' AND TO_CHAR(SYSDATE + A.PROCESSDAYS, 'YYYYMMDD') >= :CHECKIN))
) A 
LEFT JOIN BC_HOTEL_PROJ B ON B.HTL_NO=A.HTL_NO AND B.XID=A.PROJ_XID
LEFT JOIN BC_HOTEL_ROOM_PROFILE D ON D.XID = A.PROFILE_XID
LEFT JOIN BC_ROOM_BEDTYPE F ON F.XID = D.BEDTYPE_XID
LEFT JOIN BC_HOTEL_ROOM_PHOTO P ON P.XID = D.XID AND P.SEQNO='1'        
ORDER BY PROJ_XID, PROJ_ROOM_XID
";

                OracleParameter[] sqlParams = new OracleParameter[] {
                    new OracleParameter("HTL_NO", htl_no),
                    new OracleParameter("CHECKIN", check_in),
                    new OracleParameter("PROJ_XID", proj_xid)
                  //  new OracleParameter("STAY_DAYS", "1")
                };

                ds = OracleHelper.ExecuteDataset(Website.Instance.ERP_DB, CommandType.Text, sqlStmt, sqlParams);
            }
            catch (Exception ex)
            {
                Website.Instance.logger.FatalFormat("{0},{1}", ex.Message, ex.StackTrace);
                throw ex;
            }
            return ds;
        }

        public static DataSet GetRoomPrice(string htl_no, string proj_xid, string s_date, string e_date)
        {
            try
            {
                string sqlStmt = @"
SELECT HTL.PROD_NO AS H_PRODNO,HTL.HTL_NO,HTL.LIVE_NIGHT AS DAY,HTL.SUP_XID,HTL.PROJ_XID,HTL.PROFIT,ROOM.XID AS PROJROOMXID,
    QTY.HTL_DATE AS QTY_DATE,QTY.ROOM_XID,QTY.QTY_KK,QTY.HL_FLAG,
    PRICE.HTL_DATE AS PRICE_DATE,
    PRICE.COST_1,PRICE.COST_2,PRICE.COST_3,PRICE.COST_4,PRICE.COST_5,PRICE.COST_6,PRICE.COST_7,PRICE.COST_8,
    PRICE.PRICE_1,PRICE.PRICE_2,PRICE.PRICE_3,PRICE.PRICE_4,PRICE.PRICE_5,PRICE.PRICE_6,PRICE.PRICE_7,PRICE.PRICE_8,
    SAME_HTL_FLAG

FROM PROD_DPKG_HOTEL HTL
LEFT JOIN BC_HOTEL_PROJ PROJ ON HTL.HTL_NO = PROJ.HTL_NO AND HTL.SUP_XID = PROJ.SUP_XID AND HTL.PROJ_XID = PROJ.XID
LEFT JOIN BC_HOTEL_PROJ_ROOM ROOM ON PROJ.SUP_XID = ROOM.SUP_XID AND PROJ.XID = ROOM.PROJ_XID AND ROOM.PROJ_ROOM_STATUS = '01'
LEFT JOIN BC_HOTEL_ROOM_INFO QTY ON ROOM.ROOM_XID = QTY.ROOM_XID AND ROOM.HTL_NO = QTY.HTL_NO AND ROOM.SUP_XID = QTY.SUP_XID
LEFT JOIN BC_HOTEL_SALES_INFO PRICE ON PROJ.XID = PRICE.PROJ_XID AND PROJ.HTL_NO = PRICE.HTL_NO AND PROJ.SUP_XID = PRICE.SUP_XID AND ROOM.XID = PRICE.PROJ_ROOM_XID AND QTY.HTL_DATE = PRICE.HTL_DATE
WHERE   HTL.STATUS = 'QS' AND PROJ.PROJ_STATUS='01'
    AND QTY.HTL_DATE = TO_CHAR (TO_DATE ( :S_DATE, 'YYYYMMDD') + (HTL.LIVE_NIGHT - 1), 'YYYYMMDD') --第一天入住日
    AND QTY.HTL_DATE IS NOT NULL
    AND PRICE.HTL_DATE IS NOT NULL
    AND (QTY.QTY_KK >= 0 OR QTY.HL_FLAG = '1')
    AND EXISTS (SELECT * FROM PROD_DPKG_SALE_RULE_MST WHERE  (:S_DATE BETWEEN PROD_S_DATE AND PROD_E_DATE) AND STATUS='QS')
    AND HTL.PROJ_XID=:PROJ_XID
ORDER BY HTL.LIVE_NIGHT
";

                OracleParameter[] Op = new OracleParameter[] {
                    new OracleParameter("HTL_NO", htl_no),
                    new OracleParameter("PROJ_XID", proj_xid),
                    new OracleParameter("S_DATE", s_date),
                    new OracleParameter("E_DATE", e_date)
                };

                return OracleHelper.ExecuteDataset(Website.Instance.ERP_DB, CommandType.Text, sqlStmt.ToString(), Op);
            }
            catch (Exception ex)
            {
                Website.Instance.logger.FatalFormat("{0},{1}", ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        //抓 PROJ_XID底下的PROJ_ROOM_XID
        public static DataSet GetProjRoomXid(string htl_no, string proj_xid, string s_date)
        {
            try
            {
                string sqlStmt = @"
SELECT A.*
FROM (
    SELECT DISTINCT A.HTL_NO, A.XID AS PROJ_XID, B.XID AS PROJ_ROOM_XID,A.SUP_XID, A.PROJ_NAME,
      R.XID AS ROOM_XID, R.ROOM_NAME
    FROM BC_HOTEL_PROJ A
    JOIN BC_HOTEL_PROJ_ROOM B ON B.PROJ_XID = A.XID
    JOIN BC_HOTEL_INFO H ON H.HTL_NO = A.HTL_NO
    JOIN BC_HOTEL_ROOM R ON R.XID = B.ROOM_XID
    WHERE 1=1
     AND A.HTL_NO=:HTL_NO
     AND A.XID=:PROJ_XID
    AND TO_CHAR(SYSDATE, 'YYYYMMDDHH24MI') < :CHECKIN || '1200'
    AND TO_CHAR(SYSDATE + H.PROCESSDAYS, 'YYYYMMDD') <= :CHECKIN
     AND TO_DATE(A.SALE_S_DATE || A.SALE_S_TIME, 'YYYYMMDDHH24MI') <= SYSDATE
     AND TO_DATE(A.SALE_E_DATE || A.SALE_E_TIME, 'YYYYMMDDHH24MI') >= SYSDATE
     AND A.PROJ_STATUS = '01' AND A.APPROVAL_FLAG = '1' AND B.PROJ_ROOM_STATUS = '01' AND R.STATUS = '01'
    AND A.XID NOT IN (SELECT PROJ_XID FROM BC_HOTEL_PROJ_EMBARGO_DATE WHERE EMBARGO_STATUS = '01' AND :CHECKIN >= PROD_S_DATE AND :CHECKIN <= PROD_E_DATE )
     AND ( NVL(A.SUP_DISC_TYPE, '0000') NOT IN ('0001','0002')
            OR (NVL(A.SUP_DISC_TYPE, '0000') = '0001' AND TO_CHAR(SYSDATE + A.PROCESSDAYS, 'YYYYMMDD') <= :CHECKIN)
            OR (NVL(A.SUP_DISC_TYPE, '0000') = '0002' AND TO_CHAR(SYSDATE + A.PROCESSDAYS, 'YYYYMMDD') >= :CHECKIN))
) A
LEFT JOIN BC_HOTEL_PROJ B ON B.HTL_NO=A.HTL_NO AND B.XID=A.PROJ_XID
 ORDER BY PROJ_XID, PROJ_ROOM_XID
";

                OracleParameter[] Op = new OracleParameter[] {
                    new OracleParameter("HTL_NO", htl_no),
                    new OracleParameter("PROJ_XID", proj_xid),
                    new OracleParameter("CHECKIN", s_date)
                };

                return OracleHelper.ExecuteDataset(Website.Instance.ERP_DB, CommandType.Text, sqlStmt.ToString(), Op);
            }
            catch (Exception ex)
            {
                Website.Instance.logger.FatalFormat("{0},{1}", ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        //抓 QTY_KK  .  HL_FLAG
        public static DataSet GetQtykkHlflag(string htl_no, string room_xid, string s_date)
        {
            try
            {
                string sqlStmt = @"SELECT  XID, HTL_NO, ROOM_XID , HTL_DATE, QTY_KK, HL_FLAG
FROM  BC_HOTEL_ROOM_INFO 
WHERE  HTL_NO=:HTL_NO
 AND ROOM_XID=:ROOM_XID
 AND HTL_DATE = :HTL_DATE
";

                OracleParameter[] Op = new OracleParameter[] {
                    new OracleParameter("HTL_NO", htl_no),
                    new OracleParameter("ROOM_XID", room_xid),
                    new OracleParameter("HTL_DATE", s_date)
                };

                return OracleHelper.ExecuteDataset(Website.Instance.ERP_DB, CommandType.Text, sqlStmt.ToString(), Op);
            }
            catch (Exception ex)
            {
                Website.Instance.logger.FatalFormat("{0},{1}", ex.Message, ex.StackTrace);
                throw ex;
            }
        }
    }
}