using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace EZFly.Web.Prod.DPKG.AppCode.DAL
{
    public class NationalHolidayDAL
    {
        public static bool ChkNationalHoliday(string Date)
        {
            try
            {
                string sqlStmt = @"SELECT * FROM HOLIDAY_DATE WHERE HOLIDAY_DATE=:HOLIDAY_DATE";

                List<OracleParameter> sqlParams = new List<OracleParameter>();
                sqlParams.Add(new OracleParameter("HOLIDAY_DATE", Date));

                DataSet ds = OracleHelper.ExecuteDataset(Website.Instance.ERP_DB, CommandType.Text, sqlStmt.ToString(), sqlParams.ToArray());
                if (ds.Tables[0].Rows.Count > 0) { return true; } else { return false; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}