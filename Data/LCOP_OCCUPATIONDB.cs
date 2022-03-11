using System;
using System.Data;
using System.Data.OleDb ;
using SHMA.Enterprise; 
using SHMA.Enterprise.Data; 
using SHMA.Enterprise.Shared;
using System.Text;
using System.Collections;
using System.Data;

namespace SHAB.Data
{
public class LCOP_OCCUPATIONDB:SHMA.CodeVision.Data.DataClassBase
{
//<constructor>
public LCOP_OCCUPATIONDB   (DataHolder dh):base(dh)
		
	
	
	{	}
//</constructor>
//<property><property-name>TableName</property-name><property-signature>
		public override String TableName
		{
			//</property-signature><property-body>
		get {return "LCOP_OCCUPATION";}
			//			//			//			//</property-body>
		}
		//</property>
//<property><property-name>RecordCount</property-name><property-signature>
		public static int RecordCount
		{
			//</property-signature><property-body>
		get
			{
				const string strQuery="SELECT COUNT(*) FROM LCOP_OCCUPATION";
				return (int) DB.CreateCommand(strQuery).ExecuteScalar();
			}
			//			//			//			//</property-body>
		}
		//</property>
	//<method><method-name>GetDDL_ILUS_ET_NM_PER_PERSONALDET_COP_OCCUPATICD_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ET_NM_PER_PERSONALDET_COP_OCCUPATICD_RO()
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(201);//to do we have to Optimize it too.
		sb_query.Append("SELECT COP_OCCUPATICD,");
		//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		//sb_query.Append("'-'");
		//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("COP_DESCR DESC_F  FROM LCOP_OCCUPATION  ORDER BY COP_DESCR");
		string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}

	//</method>

	//<method><method-name>GetILUS_ET_NM_Occupation_lister_filter_RO</method-name><method-signature>
	public static IDataReader GetILUS_ET_NM_Occupation_lister_filter_RO(string columnName,string columnValue)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(162);//to do we have to Optimize it too.
sb_query.Append("SELECT COP_OCCUPATICD FROM LCOP_OCCUPATION  WHERE  ({0} like '{1}') ORDER BY COP_DESCR ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
query = string.Format(query, columnName, columnValue);
query = string.Format(query, columnName, columnValue);
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
String strQuery = "SELECT count(*) FROM LCOP_OCCUPATION WHERE COP_OCCUPATICD=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@COP_OCCUPATICD",DbType.String, 3, pkNameValue["COP_OCCUPATICD"]));
int noOfRecords=(int)myCommand.ExecuteScalar();
return(noOfRecords>=1);
			//</method-body>
	}
	//</method>

	//<method><method-name>GetILUS_ET_NM_Occupation_lister_RO</method-name><method-signature>
	public static IDataReader GetILUS_ET_NM_Occupation_lister_RO(int offset,int numRows)
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(162);//to do we have to Optimize it too.
sb_query.Append("SELECT COP_OCCUPATICD FROM LCOP_OCCUPATION ORDER BY COP_DESCR ");
string query=sb_query.ToString();query = EnvHelper.Parse(query);
		
IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
					//</method-body>
	}
	//</method>

	//<method><method-name>GetDDL_ILUS_ET_NM_PER_PERSONALDETINS_COP_OCCUPATICD_RO</method-name><method-signature>
	public static IDataReader GetDDL_ILUS_ET_NM_PER_PERSONALDETINS_COP_OCCUPATICD_RO()
	{
	//</method-signature><method-body>
StringBuilder sb_query=new StringBuilder(201);//to do we have to Optimize it too.
sb_query.Append("SELECT COP_OCCUPATICD,");
//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
//sb_query.Append("'-'");
//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
sb_query.Append("COP_DESCR DESC_F  FROM LCOP_OCCUPATION  ORDER BY COP_DESCR");
string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
return myCommand.ExecuteReader();
	//</method-body>
	}
	//</method>

	//<method><method-name>getAll_RO</method-name><method-signature>
	public static IDataReader getAll_RO()
	{
	//</method-signature><method-body>
const String strQuery = "SELECT * FROM LCOP_OCCUPATION";
			IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);	
			return myCommand.ExecuteReader();
						//</method-body>
	}
	//</method>

	//<method><method-name>FindByPK</method-name><method-signature>
	public DataHolder FindByPK(string COP_OCCUPATICD)
	{
	//</method-signature><method-body>
String strQuery = "SELECT * FROM LCOP_OCCUPATION WHERE COP_OCCUPATICD=? ";
IDbCommand myCommand = DB.CreateCommand(strQuery, DB.Connection);
myCommand.Parameters.Add(DB.CreateParameter("@COP_OCCUPATICD",DbType.String, 3, COP_OCCUPATICD));

this.Holder.FillData(myCommand, "LCOP_OCCUPATION");return this.Holder;
			//</method-body>
	}
	//</method>

	public static IDataReader getOccupationById(string COP_OCCUPATICD)
	{
		//</method-signature><method-body>
		StringBuilder sb_query=new StringBuilder(201);//to do we have to Optimize it too.
		sb_query.Append("SELECT COP_OCCUPATICD,");
		//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		//sb_query.Append("'-'");
		//sb_query.Append(SHMA.Enterprise.Data.PortableSQL.getConcateOperator());
		sb_query.Append("COP_DESCR DESC_F  FROM LCOP_OCCUPATION WHERE COP_OCCUPATICD='"+COP_OCCUPATICD+"' ORDER BY COP_DESCR");
		string query=sb_query.ToString();
		query = EnvHelper.Parse(query);
		IDbCommand myCommand = DB.CreateCommand(query);
		return myCommand.ExecuteReader();
		//</method-body>
	}
}
}
