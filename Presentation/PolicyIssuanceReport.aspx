<%@ Register TagPrefix="shma" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="PolicyIssuanceReport.aspx.cs" AutoEventWireup="True" Inherits="Bancassurance.Presentation.PolicyIssuanceReport" %>
<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>Data DMPs</TITLE>
		<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8">
	</HEAD>
	<BODY>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script type="text/JavaScript" src="../shmalib/jscript/Login.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<!-- <LINK rel="stylesheet" type="text/css" href="Styles/Style.css"> -->
		<%Response.Write(ace.Ace_General.loadInnerStyle());%>
		<asp:literal id="HeaderScript" EnableViewState="True" runat="server"></asp:literal>
		<table>
			<tr class="form_heading">
				<td height="20" colSpan="6">&nbsp; Policy Issuance Report
				</td>
			</tr>
		</table>
		<form id="myForm1" method="post" name="myForm1" runat="server">
			<div style="Z-INDEX: 101" id="NormalEntryTableDiv" runat="server">
				<P><LEGEND style="COLOR: #336692"></LEGEND></P>
				<TABLE id="entryTable" border="0" cellSpacing="5" cellPadding="1" width="100%">
                    <TR id="row_BANK" class="TRow_Normal">
						<TD style="WIDTH: 194px">Users:</TD>
						<TD><SHMA:dropdownlist id="ddl_Users" runat="server" BlankValue="True" DataTextField="USE_NAME" DataValueField="USE_USERID"
								tabIndex="1" Width="248px" CssClass="RequiredField"></SHMA:dropdownlist></TD>
					</TR>
					<TR id="row_Date" class="TRow_Normal">
						<TD style="WIDTH: 194px">Date Type:</TD>
						<TD>
							<asp:DropDownList id="ddlDate" runat="server" tabIndex="1" Width="248px" CssClass="RequiredField"
								style="Z-INDEX: 0">
								<asp:ListItem Value="IssueDate" Selected="True">Issued Date</asp:ListItem>
							</asp:DropDownList>
						</TD>
					</TR>
					<TR id="rowUSE_USERID" class="TRow_Normal">
						<TD style="WIDTH: 194px">From:</TD>
						<TD><SHMA:DATEPOPUP style="Z-INDEX: 0" id="txtDATEFROM" tabIndex="2" runat="server" maxlength="10" ExternalResourcePath="jsfiles/DatePopUp.js"
								ImageUrl="Images/image1.jpg" Width="5.0pc"></SHMA:DATEPOPUP>&nbsp;<asp:comparevalidator style="Z-INDEX: 0" id="cfvDATEFROM" runat="server" ErrorMessage="Date Format is Incorrect "
								ControlToValidate="txtDATEFROM" Display="Dynamic" Type="Date" Operator="DataTypeCheck"></asp:comparevalidator></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 194px">To :</TD>
						<TD>
							<SHMA:DATEPOPUP style="Z-INDEX: 0" id="txtDATETO" tabIndex="5" runat="server" maxlength="10" ExternalResourcePath="jsfiles/DatePopUp.js"
								ImageUrl="Images/image1.jpg" Width="5.0pc"></SHMA:DATEPOPUP>&nbsp;
							<asp:comparevalidator style="Z-INDEX: 0" id="cfvDATETO" runat="server" ErrorMessage="Date Format is Incorrect "
								ControlToValidate="txtDATETO" Display="Dynamic" Type="Date" Operator="DataTypeCheck"></asp:comparevalidator>
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 194px; HEIGHT: 25px"></TD>
						<TD>
							<a href="#" class="button2" onclick="saveUpdate('btnGenerateExcel');">&nbsp;&nbsp;Generate 
								Report &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
							<asp:button id="btnGenerateExcel" runat="server" Width="0px" Text="Generate MIS Text File" Font-Bold="True" onclick="btnGenerateExcel_Click"></asp:button>
						</TD>
					</TR>
					<TR id="rowUCN_DEFAULT" class="TRow_Alt">
						<TD style="WIDTH: 194px; HEIGHT: 11px">
							<P><asp:label style="Z-INDEX: 0" id="lblServerError" EnableViewState="false" runat="server" Visible="False"
									ForeColor="Red"></asp:label></P>
						</TD>
					</TR>
					<tr>
						<td>&nbsp;</td>
						<td>&nbsp;
						</td>
						<TD></TD>
						<TD></TD>
					</tr>
					<TR>
						<td style="WIDTH: 194px">
							<P>&nbsp;</P>
						</td>
						<TD>
							<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								<asp:imagebutton style="DISPLAY: none" id="btn_Save" runat="server" ImageUrl="Images/savee.JPG" onClientClick="return CheckDates();"></asp:imagebutton></P>
						</TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
						<TD></TD>
					</TR>
				</TABLE>
			</div>
			<INPUT style="WIDTH: 0px" id="_CustomArgName" name="_CustomArgName" runat="server">
			<INPUT style="WIDTH: 0px" id="_CustomArgVal" name="_CustomArgVal" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;
			<INPUT style="WIDTH: 0px" id="_CustomEvent" value="Button" type="button" name="_CustomEvent"
				runat="server"> <INPUT style="WIDTH: 0px" id="_CustomEventVal" name="_CustomEventVal" runat="server">&nbsp;
			<table border="0" width="100%">
				<tr>
					<td align="right"><A href="#"></A>&nbsp; <A href="#"></A>&nbsp; <A href="#"></A>
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">
		<asp:Literal id="callJs" runat="server" EnableViewState="False"></asp:Literal>
     	
     	function Download_BancaFile()
	    {
	      window.location.replace( "UploadedFiles/downloadProposalBanca.xls" );
	    }
	    function Download_UblFile()
	    {
	      window.location.replace( "UploadedFiles/downloadProposalUbl.xls" );
	    }
	    function Download_ILASFile()
	    {
	      window.location.replace( "UploadedFiles/downloadProposalIlas.xls" );
	    }
	    function saveUpdate(ButtonId)
		{
			 document.all(ButtonId).click();
		}
		</script>
	</BODY>
</HTML>
