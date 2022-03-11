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
using System.Web.Security;

namespace ACE.Presentation
{
	/// <summary>
	/// Summary description for PersonalPage.
	/// </summary>
	public partial class PersonalPage_menue : System.Web.UI.Page
	{

		protected System.Web.UI.WebControls.Literal ShowTip;
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.Literal userType;
		protected System.Web.UI.WebControls.Literal FooterScript;

		private bool isLeftMenuSettingExists = false;

	
		private void designLeftMenuDiv()
		{
			IDataReader rdr2 = SHAB.Data.LCSD_SYSTEMDTLDB.getUIMenuExist(SHMA.Enterprise.Presentation.SessionObject.GetString("s_USE_TYPE"));
			while(rdr2.Read())
			{
				
				this.isLeftMenuSettingExists = true;
				break;
			}
			rdr2.Close();
			rdr2.Dispose();

			IDataReader rdr = SHAB.Data.LCSD_SYSTEMDTLDB.getUIMenu(SHMA.Enterprise.Presentation.SessionObject.GetString("s_USE_TYPE"));
			System.Text.StringBuilder menuSB = new System.Text.StringBuilder();
			menuSB.Append("<table border='0' cellspacing='1' cellpadding='0'>");
			string[] menu = null;
			string firstMenu = string.Empty;
			while(rdr.Read())
			{
				menu = rdr["csd_value"].ToString().Split(',');
				menuSB.Append("<tr><td>").Append("<a href='#' ");
				menuSB.Append("onClick=\"");
				if(menu[0].IndexOf("execute")>=0)
				{
					menuSB.Append(menu[0]+"\"");
				}
				else
				{
					menuSB.Append("setPage('").Append(menu[0]).Append("')\"");
				}
				if(firstMenu.Equals(string.Empty)) 
				{
					firstMenu = menu[0];
				}				
				menuSB.Append(" class='image'>");				
				menuSB.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Append(menu[1]).Append("</a></td></tr>");				
			}
			rdr.Close();
			rdr.Dispose();
			userManagementDiv.InnerHtml = menuSB.Append("</table>").ToString();
			if(SHMA.Enterprise.Presentation.SessionObject.GetString("s_USE_TYPE")=="R")
			firstMenu="";
			FooterScript.Text += "var firstMenu = '"+firstMenu+"';";
			//FooterScript.Text=menuSB.Append("</table>").ToString();
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			ltrlTitle.Text = ace.Ace_General.getApplicationTitle();
			//Show TipOfTheDay based on cookie "HIDEONLOGIN"
			if(!IsPostBack)
			{
				designLeftMenuDiv();

				if(!this.isLeftMenuSettingExists)
				{
					Server.Transfer("../Presentation/PersonalPage.aspx");
				}
				
				//Work only for Test Server if Its value is defined in Web.config
				lblTestVersion.Text="";
				if(ace.Ace_General.IsTestVersion())
				{
					lblTestVersion.Text = ace.Ace_General.getTestVersionInfo();
				}
				else
				{
					lblTestVersion.Width = 0;
					lblTestVersion.Height = 0;
					lblTestVersion.Visible = false;
				}

				CSSLiteral.Text = ace.Ace_General.LoadGlobalStyle(); //ace.Ace_General.loadMainStyle();

				//ShowTip_Option();

				string user = Session["s_USE_USERID"].ToString();
				_loggedUser.Text = user;
				_loggedUser2.Text = user;
				
				string branch = SHAB.Data.CCS_CHANLSUBDETLDB.GetLogOnBranch();
				_loggedBranch.Text = branch;
				_loggedBranch2.Text = branch;

				if (ace.Ace_General.IsBancaasurance())
					_loggedBranchLabel.Text = "Branch";
				else
					_loggedBranchLabel.Text = "Agent";

				_loggedBranchLabel2.Text  =_loggedBranchLabel.Text ;
				userType.Text = "'" + ace.Ace_General.getUserType(user) + "'";
				string newValidation = "var newValidation='N';";
				if(ace.ValidationUtility.isNewValidation())
				{
					newValidation = "var newValidation='Y';";
				}

				//--****** LCUI_CLIENTUI (table) - Get columns list to be use to apply style *******--//
				string columnsStyle = ace.clsIlasUtility.getColumsStyle();

				//Get Tabs list to be hide
				string hiddenTabs = "var hiddenTabs='" + ace.clsIlasUtility.getHiddenTabs() + "';";

				//Get Buttons needs to be hide
				//TODO : Need to remove this logic as it is now avaliable in LCUI_CLIENTUI Setup
				string hiddenButtons = "var hiddenButtons='" + ace.clsIlasUtility.getHiddenButtons() + "';";

				//Get Buttons needs to be hide
				string RoundYearsDifference = "var ageRoundCriteria ='" + ace.clsIlasUtility.getAgeRoundingCriteria() + "';";

				string clientSpecificSetting = newValidation + columnsStyle +  hiddenTabs + hiddenButtons + RoundYearsDifference;

				string ManualAdjustment = "var ManualAdjustment = 'N';"; 
				if(ace.clsIlasUtility.isManualAdjustmentAllowed())
					ManualAdjustment = "var ManualAdjustment = 'Y';"; 

				//FooterScript.Text = SHMA.Enterprise.Shared.EnvHelper.Parse(clientSpecificSetting + "var userType=SV(\"s_USE_TYPE\"); if(userType=='A'){document.getElementById(\"adminTab\").style.visibility='visible';} else{document.getElementById(\"adminTab\").style.visibility='hidden';}if(userType=='A'||userType=='S'){document.getElementById(\"acceptanceTab\").style.visibility='visible';document.getElementById(\"acceptanceTab1\").style.visibility='visible';} else{document.getElementById(\"acceptanceTab\").style.visibility='hidden';}" + ManualAdjustment);
				FooterScript.Text += SHMA.Enterprise.Shared.EnvHelper.Parse(clientSpecificSetting + "var userType=SV(\"s_USE_TYPE\");" + ManualAdjustment);

				
				//TODO - Show/Hide tabs based on Application Type
				//FooterScript.Text += "";


			}

		}

        private void ShowTip_Option()
		{
			ShowTip.Text = "showTip();";
			HttpCookie cookie = Request.Cookies["HIDEONLOGIN"];

			if (cookie != null)
				if(cookie.Value=="1")
					ShowTip.Text = "";			
		}

		/*private void SetVersion()
		{
			object version = SHMA.Enterprise.Presentation.SessionObject.Get("s_Version");
			if (version != null)
			{
				if(version.ToString().Trim().Length > 0)
					lblVersion.Text = "Version:<br>" +  version.ToString();
			}
		}*/


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
			
			if (Context.Session != null)
			{
				if (Session.IsNewSession)
				{
					if(Session["s_USE_USERID"]== null)
					{
						Response.Redirect("LoginPage.aspx");
					}
				}
			}

		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void imgLogout_Click(object sender, ImageClickEventArgs e)
		{
			//Session.Abandon();


			//if (IsPostBack)
			{
				
				Security.LogingUtility.Logout();
				FormsAuthentication.SignOut();
				Session.Abandon();
				

				Response.Write("<script type='text/javascript'>");
				Response.Write("parent.document.location='../Presentation/LoginPage.aspx'");
				Response.Write("</script>");
			}
		}

	}
}
