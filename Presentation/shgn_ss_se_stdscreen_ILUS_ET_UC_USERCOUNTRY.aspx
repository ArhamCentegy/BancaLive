<%@ Register TagPrefix="UC" TagName="EntityHeading" Src="EntityHeading.ascx" %>
<%@ Register TagPrefix="SHMA" Namespace="SHMA.Enterprise.Presentation.WebControls" Assembly="Enterprise" %>
<%@ Page language="c#" Codebehind="shgn_ss_se_stdscreen_ILUS_ET_UC_USERCOUNTRY.aspx.cs" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ss_se_stdscreen_ILUS_ET_UC_USERCOUNTRY" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
     	<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" >
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles/Style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="JSFiles/PortableSQL.js"></script>
		<script language="javascript" src="JSFiles/JScriptFG.js"></script>
		<script language="javascript" src="JSFiles/msrsclient.js"></script>
		<script language="javascript" src="JSFiles/NumberFormat.js"></script>
		<SCRIPT language="JavaScript" src="../shmalib/jscript/WebUIValidation.js"></SCRIPT>
		<script language="javascript">
		<asp:Literal id="MessageScript" runat="server" EnableViewState="False"></asp:Literal>
		<asp:Literal id="HeaderScript" runat="server" EnableViewState="True"></asp:Literal>
			_lastEvent = '<asp:Literal id="_lastEvent" runat="server" EnableViewState="True"></asp:Literal>';

		<!-- <!--column-management-array--> -->
		
			
		/********** dependent combo's queries **********/
		

		</script>
	</HEAD>
	<body>
		<UC:EntityHeading ParamSource="FixValue" ParamValue="User Country Info" id="EntityHeading" runat="server"></UC:EntityHeading>
		<form id="myForm" name="myForm" method="post" runat="server">
			<div class="NormalEntryTableDiv" id="NormalEntryTableDiv" style="Z-INDEX: 101" runat="server">
				<fieldset><legend style="COLOR: #336696">Entry</legend>
					<TABLE id="entryTable" cellSpacing="5" cellPadding="1" border="0">
						<TR id='rowUSE_USERID' class="TRow_Normal">
							<TD>User Code</TD>
							<TD><shma:TextBox id="txtUSE_USERID" tabIndex="1" runat="server" Width='8.0pc' MaxLength="15" BaseType="Character"
									CssClass="RequiredField"></shma:TextBox>
								<asp:CompareValidator id="cfvUSE_USERID" runat="server" ControlToValidate="txtUSE_USERID" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>
						<TR id='rowCCN_CTRYCD' class="TRow_Alt">
							<TD>Country Code</TD>
							<TD><shma:TextBox id="txtCCN_CTRYCD" tabIndex="2" runat="server" Width='8.0pc' MaxLength="15" BaseType="Character"></shma:TextBox>
								<asp:CompareValidator id="cfvCCN_CTRYCD" runat="server" ControlToValidate="txtCCN_CTRYCD" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
								<asp:requiredfieldvalidator id="rfvCCN_CTRYCD" runat="server" ErrorMessage="Required" ControlToValidate="txtCCN_CTRYCD"
									Display="Dynamic"></asp:requiredfieldvalidator>
							</TD>
						</TR>
						<TR id='rowUCN_DEFAULT' class="TRow_Normal">
							<TD>Default</TD>
							<TD><SHMA:dropdownlist id="ddlUCN_DEFAULT" tabIndex="3" Width="8.0pc" runat="server" CssClass="RequiredField">
									<asp:ListItem Value="Y">Yes</asp:ListItem>
									<asp:ListItem Value="N">No</asp:ListItem>
								</SHMA:dropdownlist>
								<asp:CompareValidator id="cfvUCN_DEFAULT" runat="server" ControlToValidate="ddlUCN_DEFAULT" Operator="DataTypeCheck"
									Type="String" ErrorMessage="String Format is Incorrect " EnableClientScript="False" Display="Dynamic"></asp:CompareValidator>
							</TD>
						</TR>
						<TR>
							<td><P><asp:label id="lblServerError" runat="server" Visible="False" ForeColor="Red" EnableViewState="false"></asp:label></P>
							</td>
							<TD></TD>
						</TR>
					</TABLE>
				</fieldset>
			</div>
			<INPUT id="_CustomArgName" style="WIDTH: 0px" name="_CustomArgName" runat="server">
			<INPUT id="_CustomArgVal" style="WIDTH: 0px" name="_CustomArgVal" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;
			<INPUT id="_CustomEvent" style="WIDTH: 0px" type="button" value="Button" name="_CustomEvent"
				runat="server" onserverclick="_CustomEvent_ServerClick"> <INPUT id="_CustomEventVal" style="WIDTH: 0px" name="_CustomEventVal" runat="server">
			<INPUT type="hidden" name="frm_FetchData_qry" id="frm_FetchData_qry">
			<script language="javascript">
				
		</script>
		</form>
		<script language="javascript"><asp:literal id="FooterScript" runat="server" EnableViewState="True"></asp:literal>fcStandardFooterFunctionsCall();</script>
		<br>
		<br>
		<br>
		<br>
		<br>
		<br>
		<table width="100%" BORDER="0">
			<tr>
				<td align="right"><A class="button2" onclick="parent.frames[3].addClicked()" href="#">Add 
						New</A> <A class="button2" onclick="parent.frames[3].saveClicked()" href="#">Save</A>
					<A class="button2" onclick="parent.frames[3].deleteClicked()" href="#">Delete</A>
				</A>
				<td align="right"></td>
			</tr>
		</table>
	</body>
</HTML>
