using log4net;
using System.Net;
using Microsoft.Extensions.Configuration;
using System.IO;
//using Npgsql;
//using NpgsqlTypes;
using Oracle.ManagedDataAccess.Client;
using System.Data;
//using log4net;

/// <summary>
/// Summary description for Website
/// </summary>
public sealed class Website
{
    public static readonly Website Instance = new Website();

    public IConfiguration Configuration
    {
		get;
		private set;
    }

    // Postgresql ERP DB 連線方式
    public string ERP_DB
    {
		get;
		private set;
	}

	public log4net.ILog logger
	{
		get;
		private set;
	}

	// 主機站台識別
    public string StationID
    {
		get;
		private set;
	}

    /// <summary>
    ///   Constructor
    /// </summary>
    /// 
    private Website()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void Init(IConfiguration config)
    {
        this.Configuration = config;
        this.ERP_DB = Configuration["ConnectionStrings:SqlConnectionString"];

        string szLog4NetCfgFile = string.Format("{0}\\log4net.config", Directory.GetCurrentDirectory());

		StationID = Dns.GetHostName();

        //NpgsqlConnection npg_conn = new NpgsqlConnection(_ERP_DB);
        //NpgsqlTransaction npg_tran = null;

        //Initialize log4net;
        //var logRepository = LogManager.GetRepository( System.Reflection.Assembly.GetEntryAssembly());
        //log4net.Config.XmlConfigurator.Configure(logRepository, new FileInfo(szLog4NetCfgFile));

        //_logger = LogManager.GetLogger(logRepository.Name, "logger");
        //logger.Info("Webiste.Initialized .....");


        string dd = Configuration["ConnectionStrings:SqlConnectionString"];
		string ddd = "select * from users";
		using (OracleConnection cn = new OracleConnection(dd))
		{
			cn.Open();
			DataSet ds = OracleHelper.ExecuteDataset(cn, CommandType.Text, ddd);
		}
	}
}