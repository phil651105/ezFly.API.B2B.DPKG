using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using ezfly.dpkg.crslib;
using ezFly.API.B2B.DPKG.AppCode.DAL;
using ezFly.API.B2B.DPKG.Models.DataModel.Product;
using EZFly.Web.Prod.DPKG.AppCode.DAL;
using EZFly.Web.Prod.DPKG.AppCode.Tools;

namespace ezFly.API.B2B.DPKG.Models.Repository
{
	public class TrafficRepository
	{
		#region 航段與成本

		// 取得交通航段
		public static List<TrafficModel> GetTrafficSectors(ProdDetailRQModel req)
		{
			List<TrafficModel> sectors = new List<TrafficModel>();

			try
			{
				// 區段 SECTOR
				DataSet sec_ds = TrafficDAL.GetTrafficSectors(req.PRODNO,req.SDATE,req.EDATE);
				var fw_flag = "";
				var accum_add_day = 0;
				var total_psg = req.ADULT + req.CHILD + req.CHILDNB + req.SENIOR;

				foreach (DataRow dr in sec_ds.Tables[0].Rows)
				{
					// 檢查去/回程國定假日

					var AddPriceWeeks = string.IsNullOrEmpty(dr.ToStringEx("ADD_PRICE_WEEKS")) ? "0000000" : dr.ToStringEx("ADD_PRICE_WEEKS");
					var s_is_holiday = AddPriceWeeks.Substring(Convert.ToInt32(Convert.ToDateTime(req.SDATE).DayOfWeek), 1); // 出發日是否為假日
					var e_is_holiday = AddPriceWeeks.Substring(Convert.ToInt32(Convert.ToDateTime(req.EDATE).DayOfWeek), 1); // 回程日是否為假日

					s_is_holiday = (s_is_holiday.Equals("1") || NationalHolidayDAL.ChkNationalHoliday(req.SDATE)) ? "1" : "0";
					e_is_holiday = (e_is_holiday.Equals("1") || NationalHolidayDAL.ChkNationalHoliday(req.EDATE)) ? "1" : "0";

					var IsTripInHoliday = (dr.ToStringEx("FORWARD_FLAG").Equals("1") ? s_is_holiday : e_is_holiday).Equals("1") ? true : false;
					var dept_date = dr.ToStringEx("FORWARD_FLAG").Equals("1") ? req.SDATE : req.EDATE;

					// 各段交通累積日數, 當去回程切換數值應該歸零重計
					if (fw_flag != dr.ToStringEx("FORWARD_FLAG"))
					{
						accum_add_day = 0;
						fw_flag = dr.ToStringEx("FORWARD_FLAG");
					}
					accum_add_day += dr.ToInt32("ADD_DAY");

					try
					{
						// 航段使用交通成本
						TrafficSectorCostModel SectorCost = GetTrafficCost(req.PRODNO, dr.ToStringEx("DEP_FROM"), dr.ToStringEx("ARR_TO"),
																Convert.ToDateTime(dept_date), dr.ToInt64("TRAFFIC_XID"), dr.ToInt64("TRAFFIC_COST_XID"), IsTripInHoliday);
						// 跳過無成本的交通段
						if (SectorCost == null)
						{
							throw new Exception(string.Format("PROD_NO={0}, FORWARD_FLAG={1}, DATE={2}, TRAFFIC_XID={3}, TRAFFIC_COST_XID={4} 交通成本為 NULL",
									req.PRODNO, dr.ToStringEx("FORWARD_FLAG"), dept_date, dr.ToInt64("TRAFFIC_XID"), dr.ToInt64("TRAFFIC_COST_XID")));
						}

						// 若承運商為B7, 帶入預設成本與行程代碼
						if (dr.ToStringEx("CARRIER_CODE").Equals("B7"))
						{
							SectorCost.ADULT_TOURCODE = IsTripInHoliday ? dr.ToStringEx("ADT_TOURCODE_H") : dr.ToStringEx("ADT_TOURCODE");
							SectorCost.CHILD_TOURCODE = IsTripInHoliday ? dr.ToStringEx("CHD_TOURCODE_H") : dr.ToStringEx("CHD_TOURCODE");
							SectorCost.SENIOR_TOURCODE = IsTripInHoliday ? dr.ToStringEx("SEN_TOURCODE_H") : dr.ToStringEx("SEN_TOURCODE");

							SectorCost.ADULT_FAREBASIS = IsTripInHoliday ? dr.ToStringEx("ADT_FAREBASIS_H") : dr.ToStringEx("ADT_FAREBASIS");
							SectorCost.CHILD_FAREBASIS = IsTripInHoliday ? dr.ToStringEx("CHD_FAREBASIS_H") : dr.ToStringEx("CHD_FAREBASIS");
							SectorCost.SENIOR_FAREBASIS = IsTripInHoliday ? dr.ToStringEx("SEN_FAREBASIS_H") : dr.ToStringEx("SEN_FAREBASIS");
						}

						#region 建構交通航段Model

						var sec_item = new TrafficSectorModel()
						{
							PROD_NO = dr.ToStringEx("PROD_NO"),
							TRAFFIC_XID = dr.ToStringEx("TRAFFIC_XID"),
							TRAFFIC_COST_XID = dr.ToStringEx("TRAFFIC_COST_XID"),
							TRAFFIC_COST_PRICE_XID = dr.ToStringEx("TRAFFIC_COST_PRICE_XID"),
							
							TRIP_WAY = dr.ToStringEx("TRIP_WAY"),
							FORWARD_FLAG=dr.ToStringEx("FORWARD_FLAG"),
							SECTOR = dr.ToInt32("SEC"),
							TRAFFIC_TYPE = dr.ToStringEx("TRAFFIC_TYPE"),
							CARRIER_CODE=dr.ToStringEx("CARRIER_CODE"),
							DEP_FROM=dr.ToStringEx("DEP_FROM"),
							ARR_TO= dr.ToStringEx("ARR_TO"),
							FLY_HL_FLAG= dr.ToStringEx("FLY_HL_FLAG"),
							SUP_NO = dr.ToStringEx("SUP_NO"),
							FARE_BASIS= dr.ToStringEx("FARE_BASIS"),
							ARNK_FLAG = dr.ToStringEx("ARNK_FLAG"),

							// 票規
							RULE_TYPE = dr.ToStringEx("RULE_TYPE"),

							// 交通成本
							TRAFFIC_COST = SectorCost,
							
						};

						var timetable = QueryCrsTimeTable(dr.ToDateTime("S_DATE"), dr.ToDateTime("E_DATE"), sec_item, total_psg);

						#endregion 建構交通航段Model

						var sector = new TrafficModel()
						{
							PROD_NO = dr.ToStringEx("PROD_NO"),
							TRAFFIC_XID = dr.ToStringEx("TRAFFIC_XID"),

							TRIP_WAY = dr.ToStringEx("TRIP_WAY"),
							SECTOR = dr.ToStringEx("SEC"),
							TRAFFIC_TYPE = dr.ToStringEx("TRAFFIC_TYPE"),
							TRAFFIC_NAME = dr.ToStringEx("CARRIER_CODE_NAME"),
							SUP_NO = dr.ToStringEx("SUP_NO"),
							RETURN_FEE = dr.ToStringEx("RETURN_FEE"),
							RETURN_FEE_PERCENT = Convert.ToDouble(string.IsNullOrEmpty(dr.ToStringEx("RETURN_FEE_PERCENT")) ? "0" : dr.ToStringEx("RETURN_FEE_PERCENT")),
							// 票規
							RULE_TYPE = dr.ToStringEx("RULE_TYPE"),

							// 交通位控
							//TRAFFIC_QTYS = Qty,
							// 交通時刻表
							//TRAFFIC_TIMETBS = timetable
						};

						sectors.Add(sector);
						
					}
					catch (Exception ex2)
					{
						Website.Instance.logger.FatalFormat("{0},{1}", ex2.Message, ex2.StackTrace);
					}
				}
			}
			catch (Exception ex)
			{
				//Website.Instance.logger.FatalFormat("{0},{1}", ex.Message, ex.StackTrace);
				throw ex;
			}

			return sectors;
		}

		//計算機票成本 (不管第一段是從那裡出發)
		private static TrafficSectorCostModel GetTrafficCost(string prod_no, string dept, string arrv, DateTime s_date,
			long traffic_xid, long traffic_cost_xid, bool is_holiday)
		{
			TrafficSectorCostModel costModel = null;

			try
			{
				String dept_date = s_date.ToString("yyyyMMdd");
				int ThisDayOfWeek = Convert.ToInt32(s_date.DayOfWeek);

				// 找出該段交通成本
				DataSet dsFlyCost = TrafficDAL.QuryDailyFlyCost(prod_no, dept, arrv, dept_date, traffic_xid, traffic_cost_xid);

				// 查無任何成本, 發出異常警示
				if (dsFlyCost.Tables[0].Rows.Count < 1) return costModel;

				// 過濾掉有重覆及效期迄日比較遠的資料
				List<DataRow> cost_lst =  dsFlyCost.Tables[0].AsEnumerable().OrderBy(f => f.Field<Decimal>("SEC")).ThenBy(f => f.Field<string>("VALID_E_DATE")).ToList<DataRow>();
				if (cost_lst != null)
				{
					if (cost_lst.Count > 0)
					{
						cost_lst = ReChkSecValid(cost_lst, dept_date); //過濾掉有重覆及效期迄日比較遠的資料
					}
				}

				if (cost_lst != null)
				{
					bool ChkFlyCost = true;
					float AdultFlyCost = 0, ChildFlyCost = 0, SeniorFlyCost = 0;

					// 取得交通成本
					ReCalculateFlyCost(ref ChkFlyCost, ref AdultFlyCost, ref ChildFlyCost, ref SeniorFlyCost, cost_lst, is_holiday);

					// 建立交通航段的成本
					costModel = new TrafficSectorCostModel()
					{
						COST_XID = traffic_cost_xid,
						ADULT_COST = AdultFlyCost.ToString(),
						CHILD_COST = ChildFlyCost.ToString(),
						SENIOR_COST = SeniorFlyCost.ToString()
					};
				}
			}
			catch (Exception ex)
			{
				//Website.Instance.logger.FatalFormat("{0},{1}", ex.Message, ex.StackTrace);
				throw ex;
			}

			return costModel;
		}

		//統計每一段的FLY 成本
		private static void ReCalculateFlyCost(ref Boolean ChkFlyCost, ref float AdultFlyCost, ref float ChdFlyCost, ref float SenoirFlyCost,
			List<DataRow> cost_lst, bool IsHoliday)
		{
			if (cost_lst.Count > 0)
			{
				//先找出這一天是星期幾..
				//如果航段定義為來回,當去程是否加假有落在上面,有的話以假日加價的成本計,回程依去程定義為主
				//如果航段定義為單程,就單程單程看.
				//int ThisDayOfWeek = Convert.ToStringEx(e.Day.Date.DayOfWeek);

				List<string> TripWayIsRt = new List<string> { };  //定義了去程 trp_way 是 RT 的航段明細

				foreach (DataRow Dr in cost_lst)
				{
					string AddPriceWeeks = Dr.ToStringEx("ADD_PRICE_WEEKS");

					try
					{
						float AdultPrice = 0, ChdPrice = 0, SeniorPrice = 0;

						AdultPrice = IsHoliday ? Dr.ToSingle("ADULT_COST_H") : Dr.ToSingle("ADULT_COST");
						ChdPrice = IsHoliday ? Dr.ToSingle("CHD_COST_H") : Dr.ToSingle("CHD_COST");
						SeniorPrice = IsHoliday ? Dr.ToSingle("OLD_COST_H") : Dr.ToSingle("OLD_COST");

						AdultFlyCost = AdultPrice;
						ChdFlyCost = ChdPrice;
						SenoirFlyCost = SeniorPrice;

						ChkFlyCost = true;
					}
					catch (Exception ex)
					{
						Website.Instance.logger.FatalFormat("{0},{1}", ex.Message, ex.StackTrace);
						ChkFlyCost = false;
					}
				}

			}
			else
			{
				ChkFlyCost = false;
			}

		}

		//過濾掉有重覆及效期迄日比較遠的ROW
		private static List<System.Data.DataRow> ReChkSecValid(List<System.Data.DataRow> lst, String ThisDay)
		{
			List<System.Data.DataRow> lst1 = new List<System.Data.DataRow>();
			IFormatProvider culture = new System.Globalization.CultureInfo("zh-TW", true);
			DateTime d1 = DateTime.ParseExact(ThisDay, "yyyyMMdd", culture);


			String PreSec = "";
			String PreValidEdate = "";
			int i = 0;
			foreach (DataRow Dr in lst)
			{
				String Sec = Dr["SEC"].ToString();
				String ValidEdate = Dr["VALID_E_DATE"].ToString();
				//String ValidSdate = Dr["VALID_S_DATE"].ToString();
				DateTime d2 = DateTime.ParseExact(ValidEdate, "yyyyMMdd", culture);
				if (d1 <= d2)
				{
					if (!PreSec.Equals(Sec) || PreSec == "")
					{
						lst1.Add(Dr);
					}
					PreSec = Sec;
					PreValidEdate = ValidEdate;
					i++;
				}
			}
			return lst1;
		}

		#endregion

		///////////////////////

		#region 班次時刻表

		// 取得航班時刻表
		//public static List<TrafficTimeTableModel> GetApkgTimeTable(string prod_no, DateTime s_date, string carrier_code, int total_psg)//, bool IsAEOneWayForce = false)
		//{
		//	List<TrafficTimeTableModel> time_tables = new List<TrafficTimeTableModel>();

		//	try
		//	{
		//		// 列出有關航假航段內容
		//		var apkg_sectors = sectors.Where(s => s.TRAFFIC_TYPE == "0001" && GetQtyTypeRule(s.CARRIER_CODE) == "CRS").ToList();
		//		time_tables.AddRange(QueryCrsTimeTable(carrier_code, s_date, apkg_sectors, total_psg));

		//		// 非航假航段內容
		//		var other_sectors = sectors.Where(s => !apkg_sectors.Contains(s)).ToList();
		//		time_tables.AddRange(QueryTimeTable(prod_no, s_date, null, other_sectors));
		//	}
		//	catch (Exception ex)
		//	{
		//		Website.Instance.logger.FatalFormat("{0},{1}", ex.Message, ex.StackTrace);
		//	}

		//	return time_tables;
		//}

		// 查詢 DPKG CRS, 取得航班時刻
		protected static List<TrafficTimeTableModel> QueryCrsTimeTable(DateTime s_date, DateTime e_date, TrafficSectorModel sector, int total_psg)//(string carrier_code, DateTime S_DATE, string cityfrom, string cityto, int total_psg)
		{
			List<TrafficTimeTableModel> time_tables = new List<TrafficTimeTableModel>();

			try
			{
				var soapClients = Website.Instance.Configuration.GetSection("SoapClient");
				var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport) { MaxReceivedMessageSize = Int32.MaxValue };
				var ep = new EndpointAddress(soapClients["WS_DPKG_CRS"]);
				WS_DPKG_CRS.CrsServiceSoapClient crs_service = new WS_DPKG_CRS.CrsServiceSoapClient(binding, ep);

				Request_GetAv reqGetAv = new Request_GetAv();
				reqGetAv.CrsSystem = sector.CARRIER_CODE.Equals("B7") ? CrsSystemOption.B7 : sector.CARRIER_CODE.Equals("AE") ? CrsSystemOption.AE : CrsSystemOption.GE;
				if (sector.CARRIER_CODE == "AE")
				{
					reqGetAv.Rule_Type = sector.RULE_TYPE;
				}

				List<Segment> dep_seg = new List<Segment>();


				dep_seg.Add(new Segment()
				{
					Airline = sector.CARRIER_CODE,
					TripFlag = sector.FORWARD_FLAG,

					DepartureDate = e_date.ToString("yyyyMMdd"),
					DepartureTime = "0000",
					DepartureAirport = sector.DEP_FROM,

					ArrivalDate = s_date.ToString("yyyyMMdd"),
					ArrivalTime = "2359",
					ArrivalAirport = sector.ARR_TO,
					BookingSeatCount = total_psg
				});
				


				List<Itinerary> _Itinerary = new List<Itinerary>();
				_Itinerary.Add(new Itinerary() { Segment = dep_seg.ToArray() });

				reqGetAv.Itinerary = _Itinerary.ToArray();

				// 詢問CRSGW有效位控
				string xmlResp = GetAvTask(crs_service, XMLTool.XMLSerialize(reqGetAv)).Result;


				Response_GetAv respGetAv = new Response_GetAv();
				respGetAv = (Response_GetAv)XMLTool.XMLDeSerialize(xmlResp, respGetAv.GetType().ToString());

				var sel_segs = respGetAv.AvItinerary.Where(s => s.Segments != null && s.Segments.Count() > 0).SelectMany(s => s.Segments);

				// 產出 Time Table
				foreach (var seg in sel_segs)
				{
					foreach (var item in seg.Segment)
					{
						var sec = sector.Where(s => s.DEP_FROM.Equals(item.DepartureAirport) && s.ARR_TO.Equals(item.ArrivalAirport)).FirstOrDefault();
						DateTime dept_date = DateTime.ParseExact(item.DepartureDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
						//DateTime arrv_date = DateTime.ParseExact(item.ArrivalDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

						//成本與搭乘限制
						DataSet dtl_ds = LimitDAL.GetTrafficCostDtl(sector.TRAFFIC_COST_PRICE_XID, sector.TRAFFIC_XID, sector.TRAFFIC_COST_XID, S_DATE.ToString("yyyyMMdd"));

						int nInvalidRules = 0; //未符合規則計數, 0: 通過

						//過濾交通限制
						if (dtl_ds.Tables[0].Rows.Count != 0)
						{
							// 檢查是否指定使用 FAIR_BASIS
							var sel_fair_basis = dtl_ds.Tables[0].AsEnumerable().Where(r => !string.IsNullOrEmpty(r.Field<string>("FAIR_BASIS"))).Select(b => b.Field<string>("FAIR_BASIS")).Distinct().ToList();
							if (sel_fair_basis.Count() > 0)
							{
								// 比對 FAIR_BASIS
								if (sel_fair_basis.Where(f => f.Equals(item.FareBasis, StringComparison.InvariantCultureIgnoreCase)).Count() < 1)
								{
									nInvalidRules++; // 累計未符合規則
								}
							}

							// 過濾無效航班規則
							bool IsValidFlight = FilterTraffic(dr.ToDateTime("S_DATE"), item.DepartureTime.Replace(":", ""), item.FlightNumber, sector, dtl_ds.Tables[0]);
							if (!IsValidFlight) nInvalidRules++; // 累計未符合規則
						}

						if (nInvalidRules == 0)
						{
							//var HL_FLAG = sector.FLY_HL_FLAG.Equals("0") ? false : true;
							var HL_FLAG = sector.FLY_HL_FLAG;

							// 通過過濾規則放入時刻表
							time_tables.Add(new TrafficTimeTableModel()
							{
								S_DATE = dept_date.ToString(),
								FORWARD_FLAG = item.TripFlag,
								DEP_FROM_NAME = sector.DEP_FROM_NAME,
								DEP_FROM = item.DepartureAirport,
								ARR_TO_NAME = sector.ARR_TO_NAME,
								ARR_TO = item.ArrivalAirport,
								FLY_NO = item.FlightNumber,
								DEP_FROM_TIME = item.DepartureTime,
								ARR_TO_TIME = item.ArrivalTime,
								BOOKING_CLASS = item.BookingClass,
								SEAT_COUNT = item.BookingSeatCount.Value,
								FARE_BASIS = item.FareBasis,
								CAN_HL = HL_FLAG
							});
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return time_tables;
		}

		// 高鐵班次時刻表
		public static List<TrafficTimeTableModel> GetThsrTimeTable(BookingModel booking, List<TrafficSectorCostModel> go_sectors, List<TrafficSectorCostModel> back_sectors)
		{
			List<TrafficTimeTableModel> time_tables = new List<TrafficTimeTableModel>();

			try
			{
				time_tables.AddRange(QueryTimeTable(booking.Product.PROD_NO, booking.S_DATE, null, go_sectors));
				time_tables.AddRange(QueryTimeTable(booking.Product.PROD_NO, booking.E_DATE, null, back_sectors));
			}
			catch (Exception ex)
			{
				Website.Instance.logger.FatalFormat("{0},{1}", ex.Message, ex.StackTrace);
			}

			return time_tables;
		}

		// 火車班次時刻表 
		public static List<TrafficTimeTableModel> GetTrainTimeTable(BookingModel booking, List<TrafficTimeTableModel> sectors)
		{
			List<TrafficTimeTableModel> time_tables = new List<TrafficTimeTableModel>();

			try
			{
				time_tables.AddRange(QueryTimeTable(booking.Product.PROD_NO, booking.S_DATE, booking.E_DATE, sectors));
			}
			catch (Exception ex)
			{
				Website.Instance.logger.FatalFormat("{0},{1}", ex.Message, ex.StackTrace);
			}

			return time_tables;
		}

		// 巴士班次時刻表
		public static List<TrafficTimeTableModel> GetBusTimeTable(BookingModel booking, List<TrafficTimeTableModel> sectors)
		{
			List<TrafficTimeTableModel> time_tables = new List<TrafficTimeTableModel>();

			try
			{
				time_tables.AddRange(QueryTimeTable(booking.Product.PROD_NO, booking.S_DATE, booking.E_DATE, sectors));
			}
			catch (Exception ex)
			{
				Website.Instance.logger.FatalFormat("{0},{1}", ex.Message, ex.StackTrace);
			}

			return time_tables;
		}

		// 查詢系統已設置航班時刻
		public static List<TrafficTimeTableModel> QueryTimeTable(string prod_no, DateTime s_date, DateTime? e_date, List<TrafficSectorCostModel> sectors)
		{
			List<TrafficTimeTableModel> time_tables = new List<TrafficTimeTableModel>();

			try
			{
				if (sectors.Count() < 1) return time_tables;

				foreach (var sec_item in sectors)
				{
					//成本與搭乘限制
					DataSet dtl_ds = LimitDAL.GetTrafficCostDtl(sec_item.TRAFFIC_COST_PRICE_XID, sec_item.TRAFFIC_XID, sec_item.TRAFFIC_COST_XID, s_date.ToString("yyyyMMdd"));

					// 確認最後使用出發日
					DateTime dept_date = s_date.AddDays(sec_item.ACCUM_ADD_DAY);

					//所有航段班次
					DataSet time_ds = (sec_item.TRAFFIC_TYPE == "0002") ?  // 除高鐵外, 其它依系統指定
										ThsrDAL.GetTimeTable(prod_no,
											dept_date.ToString("yyyyMMdd"),
											sec_item.FORWARD_FLAG,
											sec_item.DEP_FROM,
											sec_item.ARR_TO) :
										TrainDAL.GetTimeTable(prod_no, // 除航假只給 s_date 外, 其它一律會帶 s_date 與 e_date
											(sec_item.FORWARD_FLAG == "1" ? dept_date : (e_date.HasValue ? e_date.Value.AddDays(sec_item.ACCUM_ADD_DAY) : s_date)).ToString("yyyyMMdd"),
											sec_item.FORWARD_FLAG,
											sec_item.DEP_FROM,
											sec_item.ARR_TO);

					foreach (DataRow dr in time_ds.Tables[0].Rows)
					{
						bool chk = false;

						//過濾交通限制
						if (dtl_ds.Tables[0].Rows.Count != 0)
						{
							chk = (sec_item.TRAFFIC_TYPE == "0002") ?
								FilterTraffic(s_date, dr.ToStringEx("DEP_TIME"), dr.ToStringEx("TRAIN"), sec_item, dtl_ds.Tables[0]) :
								FilterTraffic(s_date, dr.ToStringEx("S_TIME"), dr.ToStringEx("FLY_NO"), sec_item, dtl_ds.Tables[0]);
						}
						else { chk = true; } //沒有限制

						//通過過濾規則放入時刻表
						if (chk)
						{
							DateTime work_date = sec_item.FORWARD_FLAG.Equals("1") ? s_date : ((e_date.HasValue) ? e_date.Value : s_date);

							if (sec_item.TRAFFIC_TYPE == "0002")
							{
								time_tables.Add(new TrafficTimeTableModel()
								{
									S_DATE = s_date.AddDays(sec_item.ADD_DAY),
									FLY_NO = dr.ToStringEx("TRAIN"),
									FORWARD_FLAG = sec_item.FORWARD_FLAG,
									SECTOR = sec_item.SECTOR,

									DEP_FROM = dr.ToStringEx("DEP_STATION_NO"),
									DEP_FROM_NAME = dr.ToStringEx("DEP_STATION_NAME"),
									ARR_TO = dr.ToStringEx("ARR_STATION_NO"),
									ARR_TO_NAME = dr.ToStringEx("ARR_STATION_NAME"),

									DEP_FROM_TIME = dr.ToStringEx("DEP_TIME").Insert(2, ":"),
									ARR_TO_TIME = dr.ToStringEx("ARR_TIME").Insert(2, ":"),

									SEAT_COUNT = dr.ToStringEx("QTY_KK"),
									CAN_HL = sec_item.FLY_HL_FLAG.Equals("1") ? true : false,

									CARRIER_CODE = sec_item.CARRIER_CODE,
									CARRIER_NAME = sec_item.CARRIER_CODE_NAME,

									TOKEN_DATA = string.Format("{0:yyyyMMdd},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", work_date.AddDays(sec_item.ACCUM_ADD_DAY),
													sec_item.FORWARD_FLAG, sec_item.TRAFFIC_XID, sec_item.TRAFFIC_COST_XID, sec_item.DEP_FROM,
													sec_item.ARR_TO, dr.ToStringEx("TRAIN"), dr.ToStringEx("DEP_TIME").Insert(2, ":"), dr.ToStringEx("ARR_TIME").Insert(2, ":"),
													dr.ToStringEx("QTY_KK"), "", "")
								});
							}
							else
							{
								time_tables.Add(new TrafficTimeTableModel()
								{
									S_DATE = s_date.AddDays(sec_item.ADD_DAY),
									FLY_NO = dr.ToStringEx("FLY_NO"),
									FORWARD_FLAG = sec_item.FORWARD_FLAG,
									SECTOR = sec_item.SECTOR,

									DEP_FROM = dr.ToStringEx("DEP_FROM"),
									DEP_FROM_NAME = dr.ToStringEx("DEP_FROM_NAME"),
									ARR_TO = dr.ToStringEx("ARR_TO"),
									ARR_TO_NAME = dr.ToStringEx("ARR_TO_NAME"),

									DEP_FROM_TIME = dr.ToStringEx("S_TIME").Insert(2, ":"),
									ARR_TO_TIME = dr.ToStringEx("E_TIME").Insert(2, ":"),

									SEAT_COUNT = dr.ToStringEx("QTY_KK"),
									CAN_HL = sec_item.FLY_HL_FLAG.Equals("1") ? true : false,

									CARRIER_CODE = sec_item.CARRIER_CODE,
									CARRIER_NAME = (sec_item.TRAFFIC_TYPE == "0003") ? sec_item.CARRIER_NAME : string.Format("{0},{1},{2}艙", sec_item.CARRIER_CODE, sec_item.CARRIER_NAME, sec_item.BOOKING_CLASS),

									TOKEN_DATA = string.Format("{0:yyyyMMdd},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", work_date.AddDays(sec_item.ACCUM_ADD_DAY),
													sec_item.FORWARD_FLAG, sec_item.TRAFFIC_XID, sec_item.TRAFFIC_COST_XID, sec_item.DEP_FROM,
													sec_item.ARR_TO, dr.ToStringEx("FLY_NO"), dr.ToStringEx("S_TIME").Insert(2, ":"), dr.ToStringEx("E_TIME").Insert(2, ":"),
													dr.ToStringEx("QTY_KK"), "", "")
								});
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return time_tables;
		}

		// 航假/小三通班次時刻表
		public static List<TrafficTimeTableModel> GetApkgTimeTable(BookingModel booking, List<TrafficSectorCostModel> sectors)
		{
			List<TrafficTimeTableModel> time_table = new List<TrafficTimeTableModel>();

			try
			{
				DateTime s_date = booking.S_DATE;
				foreach (var sec_item in sectors)
				{
					//成本與搭乘限制
					DataSet dtl_ds = LimitDAL.GetTrafficCostDtl(sec_item.TRAFFIC_COST_PRICE_XID, sec_item.TRAFFIC_XID, sec_item.TRAFFIC_COST_XID, s_date.ToString("yyyyMMdd"));

					//所有交通班次
					DataSet time_ds = TrainDAL.GetTimeTable(booking.Product.PROD_NO, s_date.ToString("yyyyMMdd"), sec_item.FORWARD_FLAG, sec_item.DEP_FROM, sec_item.ARR_TO);
					foreach (DataRow dr in time_ds.Tables[0].Rows)
					{
						bool chk = false;

						//過濾交通限制
						if (dtl_ds.Tables[0].Rows.Count != 0)
						{
							chk = FilterTraffic(s_date, dr.ToStringEx("S_TIME"), dr.ToStringEx("FLY_NO"), sec_item, dtl_ds.Tables[0]);
						}
						else { chk = true; } //沒有限制

						//通過過濾規則放入時刻表
						if (chk)
						{
							time_table.Add(new TrafficTimeTableModel()
							{
								S_DATE = s_date.AddDays(sec_item.ADD_DAY),
								FLY_NO = dr.ToStringEx("FLY_NO"),
								FORWARD_FLAG = sec_item.FORWARD_FLAG,
								SECTOR = sec_item.SECTOR,

								DEP_FROM = dr.ToStringEx("DEP_FROM"),
								DEP_FROM_NAME = dr.ToStringEx("DEP_FROM_NAME"),
								ARR_TO = dr.ToStringEx("ARR_TO"),
								ARR_TO_NAME = dr.ToStringEx("ARR_TO_NAME"),

								DEP_FROM_TIME = dr.ToStringEx("S_TIME").Insert(2, ":"),
								ARR_TO_TIME = dr.ToStringEx("E_TIME").Insert(2, ":"),

								SEAT_COUNT = dr.ToStringEx("QTY_KK")
							});
						}
					}
				}
			}
			catch (Exception ex)
			{
				Website.Instance.logger.FatalFormat("{0},{1}", ex.Message, ex.StackTrace);
			}

			return time_table;
		}

		#endregion

		// 呼叫CRS
		public static async Task<String> GetAvTask(WS_DPKG_CRS.CrsServiceSoapClient crs_service,
			string xmldata)
		{
			var _task = await crs_service.GetAvAsync(xmldata);
			var xml = _task.Body.GetAvResult;

			return xml;
		}

		//過濾交通限制
		public static bool FilterTraffic(DateTime S_Date, string S_Time, string Fly_No, TrafficModel sec_item, DataTable dt)
		{
			bool date_chk = false;
			bool chk = false;
			foreach (DataRow dtl_dr in dt.Rows)
			{
				string Forward_Flag = dtl_dr.ToStringEx("FORWARD_FLAG");

				//判斷禁搭日期
				if (Forward_Flag != "" && Forward_Flag == sec_item.FORWARD_FLAG)
				{
					DateTime emgargo_s_date = DateTime.ParseExact(dtl_dr.ToStringEx("S_DATE"), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);//禁搭日起
					DateTime emgargo_e_date = DateTime.ParseExact(dtl_dr.ToStringEx("E_DATE"), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);//禁搭日迄

					if (S_Date >= emgargo_s_date && S_Date <= emgargo_e_date) { date_chk = false; break; }
					else { date_chk = true; }
				}
				else { date_chk = true; }

				//判斷禁搭航班、時間
				if (date_chk == true)
				{
					IFormatProvider culture = new System.Globalization.CultureInfo("zh-TW", true);

					string go_limit_flight_no = dtl_dr.ToStringEx("GO_LIMIT_FLIGHT_NO");
					string go_embargo_flight_no = dtl_dr.ToStringEx("GO_EMBARGO_FLIGHT_NO");
					string back_limit_flight_no = dtl_dr.ToStringEx("BACK_LIMIT_FLIGHT_NO");
					string back_embargo_flight_no = dtl_dr.ToStringEx("BACK_EMBARGO_FLIGHT_NO");
					DateTime s_time = DateTime.ParseExact(S_Time, "HHmm", culture);//出發時間

					if (sec_item.FORWARD_FLAG == "1")//1-->去程
					{
						//去程限搭航班
						if (go_limit_flight_no != "")
						{
							foreach (string s in go_limit_flight_no.Split(','))
							{
								if (s == Fly_No) { chk = true; break; } else { chk = false; }
							}
						}
						//去程禁搭航班
						else if (go_embargo_flight_no != "")
						{
							foreach (string s in go_embargo_flight_no.Split(','))
							{
								if (s == Fly_No) { chk = false; break; } else { chk = true; }
							}
						}
						else { chk = true; }

						DateTime go_limit_s_time;
						DateTime go_limit_e_time;
						DateTime go_embargo_s_time;
						DateTime go_embargo_e_time;

						if (chk == true)
						{
							//去程限搭時段起 迄    
							if (dtl_dr.ToStringEx("GO_LIMIT_FLIGHT_S_TIME") != "" && dtl_dr.ToStringEx("GO_LIMIT_FLIGHT_E_TIME") != "")
							{
								go_limit_s_time = DateTime.ParseExact(dtl_dr.ToStringEx("GO_LIMIT_FLIGHT_S_TIME"), "HHmm", culture);
								go_limit_e_time = DateTime.ParseExact(dtl_dr.ToStringEx("GO_LIMIT_FLIGHT_E_TIME"), "HHmm", culture);

								if (s_time >= go_limit_s_time && s_time <= go_limit_e_time) { chk = true; } else { chk = false; }
							}
							else if (dtl_dr.ToStringEx("GO_LIMIT_FLIGHT_S_TIME") != "" && dtl_dr.ToStringEx("GO_LIMIT_FLIGHT_E_TIME") == "")
							{
								go_limit_s_time = DateTime.ParseExact(dtl_dr.ToStringEx("GO_LIMIT_FLIGHT_S_TIME"), "HHmm", culture);
								if (s_time >= go_limit_s_time) { chk = true; } else { chk = false; }
							}
							else if (dtl_dr.ToStringEx("GO_LIMIT_FLIGHT_S_TIME") == "" && dtl_dr.ToStringEx("GO_LIMIT_FLIGHT_E_TIME") != "")
							{
								go_limit_e_time = DateTime.ParseExact(dtl_dr.ToStringEx("GO_LIMIT_FLIGHT_E_TIME"), "HHmm", culture);
								if (s_time <= go_limit_e_time) { chk = true; } else { chk = false; }
							}
							//去程禁搭時段起 迄
							else if (dtl_dr.ToStringEx("GO_EMBARGO_FLIGHT_S_TIME") != "" && dtl_dr.ToStringEx("GO_EMBARGO_FLIGHT_E_TIME") != "")
							{
								go_embargo_s_time = DateTime.ParseExact(dtl_dr.ToStringEx("GO_EMBARGO_FLIGHT_S_TIME"), "HHmm", culture);
								go_embargo_e_time = DateTime.ParseExact(dtl_dr.ToStringEx("GO_EMBARGO_FLIGHT_E_TIME"), "HHmm", culture);

								if (!((s_time >= go_embargo_s_time) && (s_time <= go_embargo_e_time))) { chk = true; } else { chk = false; }
							}
							else if (dtl_dr.ToStringEx("GO_EMBARGO_FLIGHT_S_TIME") != "" && dtl_dr.ToStringEx("GO_EMBARGO_FLIGHT_E_TIME") == "")
							{
								go_embargo_s_time = DateTime.ParseExact(dtl_dr.ToStringEx("GO_EMBARGO_FLIGHT_S_TIME"), "HHmm", culture);

								if (!(s_time >= go_embargo_s_time)) { chk = true; } else { chk = false; }
							}
							else if (dtl_dr.ToStringEx("GO_EMBARGO_FLIGHT_S_TIME") == "" && dtl_dr.ToStringEx("GO_EMBARGO_FLIGHT_E_TIME") != "")
							{
								go_embargo_e_time = DateTime.ParseExact(dtl_dr.ToStringEx("GO_EMBARGO_FLIGHT_E_TIME"), "HHmm", culture);

								if (!(s_time <= go_embargo_e_time)) { chk = true; } else { chk = false; }
							}
						}
					}
					else //2-->回程
					{
						//回程限搭航班
						if (back_limit_flight_no != "")
						{
							foreach (string s in back_limit_flight_no.Split(','))
							{
								if (s == Fly_No) { chk = true; break; } else { chk = false; }
							}
						}
						//回程禁搭航班
						else if (back_embargo_flight_no != "")
						{
							foreach (string s in back_embargo_flight_no.Split(','))
							{
								if (s == Fly_No) { chk = false; break; } else { chk = true; }
							}
						}
						else { chk = true; }

						DateTime back_limit_s_time;
						DateTime back_limit_e_time;
						DateTime back_embargo_s_time;
						DateTime back_embargo_e_time;

						if (chk == true)
						{
							//回程限搭時段起 迄
							if (dtl_dr.ToStringEx("BACK_LIMIT_FLIGHT_S_TIME") != "" && dtl_dr.ToStringEx("BACK_LIMIT_FLIGHT_E_TIME") != "")
							{
								back_limit_s_time = DateTime.ParseExact(dtl_dr.ToStringEx("BACK_LIMIT_FLIGHT_S_TIME"), "HHmm", culture);
								back_limit_e_time = DateTime.ParseExact(dtl_dr.ToStringEx("BACK_LIMIT_FLIGHT_E_TIME"), "HHmm", culture);

								if (s_time >= back_limit_s_time && s_time <= back_limit_e_time) { chk = true; } else { chk = false; }
							}
							else if (dtl_dr.ToStringEx("BACK_LIMIT_FLIGHT_S_TIME") != "" && dtl_dr.ToStringEx("BACK_LIMIT_FLIGHT_E_TIME") == "")
							{
								back_limit_s_time = DateTime.ParseExact(dtl_dr.ToStringEx("BACK_LIMIT_FLIGHT_S_TIME"), "HHmm", culture);

								if (s_time >= back_limit_s_time) { chk = true; } else { chk = false; }
							}
							else if (dtl_dr.ToStringEx("BACK_LIMIT_FLIGHT_S_TIME") == "" && dtl_dr.ToStringEx("BACK_LIMIT_FLIGHT_E_TIME") != "")
							{
								back_limit_e_time = DateTime.ParseExact(dtl_dr.ToStringEx("BACK_LIMIT_FLIGHT_E_TIME"), "HHmm", culture);

								if (s_time <= back_limit_e_time) { chk = true; } else { chk = false; }
							}
							//回程禁搭時段起 迄
							else if (dtl_dr.ToStringEx("BACK_EMBARGO_FLIGHT_S_TIME") != "" && dtl_dr.ToStringEx("BACK_EMBARGO_FLIGHT_E_TIME") != "")
							{
								back_embargo_s_time = DateTime.ParseExact(dtl_dr.ToStringEx("BACK_EMBARGO_FLIGHT_S_TIME"), "HHmm", culture);
								back_embargo_e_time = DateTime.ParseExact(dtl_dr.ToStringEx("BACK_EMBARGO_FLIGHT_E_TIME"), "HHmm", culture);

								if (!((s_time >= back_embargo_s_time) && (s_time <= back_embargo_e_time))) { chk = true; } else { chk = false; }
							}
							else if (dtl_dr.ToStringEx("BACK_EMBARGO_FLIGHT_S_TIME") != "" && dtl_dr.ToStringEx("BACK_EMBARGO_FLIGHT_E_TIME") == "")
							{
								back_embargo_s_time = DateTime.ParseExact(dtl_dr.ToStringEx("BACK_EMBARGO_FLIGHT_S_TIME"), "HHmm", culture);

								if (!(s_time >= back_embargo_s_time)) { chk = true; } else { chk = false; }
							}
							else if (dtl_dr.ToStringEx("BACK_EMBARGO_FLIGHT_S_TIME") == "" && dtl_dr.ToStringEx("BACK_EMBARGO_FLIGHT_E_TIME") != "")
							{
								back_embargo_e_time = DateTime.ParseExact(dtl_dr.ToStringEx("BACK_EMBARGO_FLIGHT_E_TIME"), "HHmm", culture);

								if (!(s_time <= back_embargo_e_time)) { chk = true; } else { chk = false; }
							}
						}
					}
				}
			}
			return chk;
		}

	}
}
