using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
namespace SHAB.Data{
public class LCNT_NATIONALITYDB:SHMA.CodeVision.Data.DataClassBase
{
	//<constructor>
	public LCNT_NATIONALITYDB (DataHolder dh):base(dh)
	{	}
	//</constructor>
	
	//<property><property-name>TableName</property-name><property-signature>
	public override String TableName
	{
		//</property-signature><property-body>
	get {return "LCNT_NATIONALITY";}
		//			//			//			//</property-body>
	}
	//</property>

	//<property><property-name>RecordCount</property-name><property-signature>
	public static int RecordCount
	{
		//</property-signature><property-body>
		get
		{
			const string strQuery="SELECT COUNT(*) FROM LCNT_NATIONALITY";
			return (int) DB.CreateCommand(strQuery).ExecuteScalar();
		}
		//			//			//			//</property-body>
	}
	//</property>

	//<method><method-name>GetDDL_ILUS_ET_NM_PER_PERSONALDET_CNT_NATCD_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ET_NM_PER_PERSONALDET_CNT_NATCD_RO()
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(211);//to do we have to Optimize it too.
		sb_query.Append("SELECT CNT_NATCD,CNT_DESCR DESC_F  FROM LCNT_NATIONALITY  ORDER BY CNT_DESCR");
		string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}
	//</method>

	//<method><method-name>Exists</method-name><method-signature>
	public static Boolean Exists(NameValueCollection pkNameValue)
	{
		//</method-signature><method-body>
		String strQuery = "SELECT count(*) FROM LCNT_NATIONALITY WHERE CNT_NATCD=? ";
		IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
		myCommand.Parameters.Add(DB.CreateParameter("@CNT_NATCD",DbType.String, 3, pkNameValue["CNT_NATCD"]));
		int noOfRecords=(int)myCommand.ExecuteScalar();
		return(noOfRecords>=1);
		//</method-body>
	}
	//</method>


	//<method><method-name>getAll_RO</method-name><method-signature>
	public static IDataReader getAll_RO()
	{
		//</method-signature><method-body>
		const String strQuery = "SELECT * FROM LCNT_NATIONALITY";
		IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
		return myCommand.ExecuteReader();
		//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string CNT_NATCD)
	{
		//</method-signature><method-body>
		String strQuery = "SELECT * FROM LCNT_NATIONALITY WHERE CNT_NATCD=? ";
		IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
		myCommand.Parameters.Add(DB.CreateParameter("@CNT_NATCD",DbType.String, 3, CNT_NATCD));

		this.Holder.FillData(myCommand, "LCNT_NATIONALITY");return this.Holder;
		//</method-body>
	}
	//</method>

}
}
