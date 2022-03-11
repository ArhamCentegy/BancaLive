using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using SHMA.Enterprise.Data;
using Types = SHMA.Enterprise.Data.Types;
using SHMA.Enterprise.Presentation;
using NameValueCollection = SHMA.Enterprise.NameValueCollection;
using ProcessException = SHMA.Enterprise.Exceptions.ProcessException;

using SHAB.Data;
using SHMA.Enterprise;

namespace Bancassurance.Presentation
{
	/// <summary>
	/// Summary description for ExecuteReport.
	/// </summary>
	
	
	public partial class ExecuteReport : System.Web.UI.Page
	{

		protected System.Web.UI.WebControls.Literal FooterScript;
		



		public const string RPT_ILLUSTRATION = "ILLUSTRATION";
		public const string RPT_POLICY       = "POLICY";
		public const string RPT_ADVICE       = "ADVICE";
		public const string RPT_PROFILE      = "PROFILE";
		public const string RPT_PROPOSALINQ  = "PROPOSALINQ";
		public const string RPT_SECURITYLOG  = "SECURITYLOG";
		public const string RPT_PROPSUMMARY  = "PROP_SUMMARY";		

		public const string RPT_PDILLUS       = "PDILLUS";

		string strProposal  = "";
		
		string strReportType= "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			pnlCommonInput.Visible = false;
			pnlSecurityLog.Visible = false;

			this.strProposal  = Request.QueryString["_Proposal"].ToString();
			this.strReportType= Request.QueryString["_ReportType"].ToString().ToUpper();

			Security.ACTIVITY reportActivity = Security.ACTIVITY.NONE;

			ReportHeading.Text = "Report";
			if(strReportType == RPT_ILLUSTRATION)
			{
				ReportHeading.Text = "Illustration";
				reportActivity = Security.ACTIVITY.ILLUSTRATION_PRINTED;
			}
			else if(strReportType == RPT_POLICY)
			{
				ReportHeading.Text = "Policy Printing";
				reportActivity = Security.ACTIVITY.POLICY_PRINTED;
			}
			else if(strReportType == RPT_ADVICE)
			{
				ReportHeading.Text = "Advice x";
				reportActivity = Security.ACTIVITY.ADVICE_PRINTED;
			}
			else if(strReportType == RPT_PROFILE || strReportType == RPT_PDILLUS)
			{
				Session["NP1_PROPOSAL"]=this.strProposal;
				ReportHeading.Text = "Profile Printing";
				reportActivity = Security.ACTIVITY.PERSONAL_PROFILE_PRINTED;
			}
			else if(strReportType == RPT_PROPOSALINQ)
			{
				Session["NP1_PROPOSAL"]=this.strProposal;
				ReportHeading.Text = "BSO Proposal Inquiry";
				reportActivity = Security.ACTIVITY.PROPOSAL_INQUIRY_PRINTED;
			}
			else if(strReportType == RPT_SECURITYLOG)
			{
				ReportHeading.Text = "Security Log";
			}
			else if(strReportType == RPT_PROPSUMMARY)
			{
				ReportHeading.Text = "Proposal Summary";
			}
			else
			{
				throw new Exception("Unknown Report Type");
			}


			try
			{	
				if(strReportType == RPT_PROPOSALINQ)
				{
					pnlCommonInput.Visible = true;
					if(!IsPostBack)
					{
						DateTime sysDate = Convert.ToDateTime(Session["s_CURR_SYSDATE"]);
						this.txtDATEFROM.Text = "01/" + sysDate.Month + "/" + sysDate.Year;
						this.txtDATETO.Text   = sysDate.Day + "/" + sysDate.Month + "/" + sysDate.Year;
					}
				}
				else if(strReportType == RPT_SECURITYLOG)
				{
					pnlCommonInput.Visible = true;
					pnlSecurityLog.Visible= true;
					SetChannelCombos();
					if(!IsPostBack)
					{
						DateTime sysDate = Convert.ToDateTime(Session["s_CURR_SYSDATE"]);
						DateTime dateFrom = sysDate.AddDays(-7);
						this.txtDATEFROM.Text = dateFrom.Day + "/" + dateFrom.Month + "/" + dateFrom.Year;
						this.txtDATETO.Text   = sysDate.Day  + "/" + sysDate.Month  + "/" + sysDate.Year;
					}
				}
				else
				{
					//************* Activity Log *************//
					Security.LogingUtility.GenerateActivityLog(reportActivity);

					//Get Report Information (Name and its Parameters)
					string[] arrReportInfo = getReportInfoFromSetup().Split(new char[]{'~'});
					string reportName = arrReportInfo[0];
					string moreReportParms = arrReportInfo[1];

					string ParamStr = "_q_cProposal," + strProposal + "," + strProposal + ";" + moreReportParms;
					string URL = "../CrystalReports/CrystalReport.aspx?_ParamStr=" + ParamStr + "&_RepName=" + "../CrystalReports/" + reportName;
								
					Response.Redirect(URL, false);

					
				}
			}
			catch(Exception ex)
			{
				//Response.Write(ex.Message);
				ReportError.Text = ex.Message;
			}
		}

		private string getReportInfoFromSetup()
		{
			try
			{
				ace.clsIlasReport objReport = new ace.clsIlasReport(this.strProposal,this.strReportType);
				return objReport.getReportInformation();
			}
			catch(Exception e)
			{
				throw new Exception("Error in getting Report." + e.Message);
			}
		}

		#region "Mandatory Checks"
		private bool validate()
		{
			if(strReportType == RPT_ILLUSTRATION)
			{
				return validateIllustration();
			}
			else if(strReportType == RPT_POLICY)
			{
				return validatePolicy();
			}
			else if(strReportType == RPT_ADVICE)
			{
				return validateAdvice();
			}
			else if(strReportType == RPT_PROFILE)
			{
				return validateProfile();
			}
			else if(strReportType == RPT_PDILLUS)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private bool validateIllustration()
		{
			checkPremium();
			return true;
		}

		private bool validatePolicy()
		{
			checkPremium();
			//**** Check either Policy Number is issued or not ****//
			rowset rs = DB.executeQuery("Select 'A' FROM LNP1_POLICYMASTR WHERE NP1_PROPOSAL='"+ this.strProposal +"' AND NP1_POLICYNO IS NOT NULL ");
			if(rs.next() == false)
			{
				throw new Exception("Please complete Acceptance for this Proposal");
			}
			return true;
		}

		private bool validateAdvice()
		{
			checkPremium();
			return true;
		}

		private bool validateProfile()
		{
			checkPremium();
			//**** Check either Policy Number is issued or not ****//
			rowset rs = DB.executeQuery("Select 'A' FROM LNAD_ADDRESS WHERE NP1_PROPOSAL='"+ this.strProposal +"' and NAD_ADDRESSTYP='C' ");
			if(rs.next() == false)
			{
				throw new Exception("Please enter Information till Correspondence Address");
			}
			return true;
		}

		private void checkPremium()
		{
			string query = "SELECT NPR_PREMIUM FROM LNPR_PRODUCT WHERE NP1_PROPOSAL='"+ this.strProposal +"'  AND NPR_BASICFLAG='Y' AND NVL(NPR_PREMIUM,0) > 0 "; 
			rowset rs = DB.executeQuery(query);
			if(rs.next())
			{
				if(rs.getObject("NPR_PREMIUM") == null)
				{
					throw new Exception("Please calculate Premium from Plan-Rider");
				}

				if(rs.getDouble("NPR_PREMIUM") == 0)
				{
					throw new Exception("Please calculate Premium from Plan-Rider");
				}

			}
			else
			{
				throw new Exception("Please calculate Premium from Plan-Rider");
			}		
		}
		#endregion
	
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);

			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
			Response.Cache.SetNoStore();
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void btnGenerate_Click(object sender, System.EventArgs e)
		{
			string userId = Convert.ToString(Session["s_USE_USERID"]);

			string reportName = "";
			string ParamStr = ""; 

			if(pnlSecurityLog.Visible == true)
			{
				if(ddlREPORT.SelectedValue == "LOGINLOG")
				{
					reportName = "login_Access_log";

				}
				else if(ddlREPORT.SelectedValue == "ACTIVITYLOG")
				{
					reportName = "login_Act_log";

				}
				ParamStr += "_q_cSLL_DATE_FROM," + txtDATEFROM.Text + "," + txtDATEFROM.Text + ";";
				ParamStr += "_q_cSLL_DATE_TO,"   + txtDATETO.Text   + "," + txtDATETO.Text   + ";";

				ParamStr += "_q_cCCH_CODE," + ddlCCH_CODE_1.SelectedValue + "," + ddlCCH_CODE_1.SelectedValue + ";";
				ParamStr += "_q_cCCD_CODE," + ddlCCD_CODE_1.SelectedValue + "," + ddlCCD_CODE_1.SelectedValue + ";";
			}
			else
			{
				//************* Activity Log *************//
				Security.LogingUtility.GenerateActivityLog(Security.ACTIVITY.PROPOSAL_INQUIRY_PRINTED);
				reportName = "ProposalInquiry";

				ParamStr += "_q_cP_UserId,"   + userId           + "," + userId           + ";"; 
				ParamStr += "_q_cP_DateFrom," + txtDATEFROM.Text + "," + txtDATEFROM.Text + ";";
				ParamStr += "_q_cP_DateTo,"   + txtDATETO.Text   + "," + txtDATETO.Text   + ";";

			}

			string URL = "../CrystalReports/CrystalReport.aspx?_ParamStr=" + ParamStr + "&_RepName=" + "../CrystalReports/" + reportName;
			Response.Redirect(URL, false);
		}

		private void SetChannelCombos()
		{
			//New columns for Channel and Channel Detail columns
			//ddlCCH_CODE_1.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"'); Channel_ChangeEvent(this);");
			//ddlCCD_CODE_1.Attributes.Add("onchange" ,"SetStatus('" + e.Item.ItemIndex.ToString()+"'); ChannelDetail_ChangeEvent(this);");
			ddlCCH_CODE_1.Attributes.Add("onchange" ,"Channel_ChangeEvent(this);");
			ddlCCD_CODE_1.Attributes.Add("onchange" ,"ChannelDetail_ChangeEvent(this);");
			
			IDataReader drCCH_CODE = CCH_CHANNELDB.GetDDL_CHANNELS();
			ddlCCH_CODE_1.DataSource = drCCH_CODE;
			ddlCCH_CODE_1.DataBind();
			drCCH_CODE.Close();

			if(ddlCCH_CODE_1.Items.Count > 0)
			{
				ddlCCH_CODE_1.SelectedIndex = 0;

			
				IDataReader drCCD_CODE = CCD_CHANNELDETAILDB.GetDDL_CHANNELDETAIL(ddlCCH_CODE_1.SelectedValue) ;
				ddlCCD_CODE_1.DataSource = drCCD_CODE;
				ddlCCD_CODE_1.DataBind();
				drCCD_CODE.Close();

				if(ddlCCD_CODE_1.Items.Count > 0)
					ddlCCD_CODE_1.SelectedIndex = 0;

			}
			FooterScript.Text = "Channel_ChangeEvent(document.getElementById('ddlCCH_CODE_1'));";

		}
	}
}
