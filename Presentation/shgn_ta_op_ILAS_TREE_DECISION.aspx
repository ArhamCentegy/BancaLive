<%@ Page CodeBehind="shgn_ta_op_ILAS_TREE_DECISION.aspx.cs" Language="c#" AutoEventWireup="True" Inherits="SHAB.Presentation.shgn_ta_op_ILAS_TREE_DECISION" %>
<html>
<%Response.Write(ace.Ace_General.LoadPageStyle());%>

<body bottomMargin=0px topMargin=0px rightMargin=0px leftMargin=0px>
<table cellspacing=0 cellpadding=0 id="tabTable">
<tr>
<td>
  <DIV id=TabL0 onclick="setTab('../Presentation/shgn_gp_gp_LPVF_VALIDFIELDS_DECISION.aspx?pt=LPVF_VALIDFIELDS_DECISION&<%=ClientParams%>','D',this)" language=javascript style="VISIBILITY: hidden; CURSOR: hand; OVERFLOW: auto; FONT-FAMILY: tahoma,sans-serif">
    <table cellspacing=0 cellpadding=0>
    <tr>
      <td class="tab_Back"></td>
    </tr>
    </table>
    <DIV id=Tab language=javascript style="OVERFLOW: auto; TEXT-ALIGN: center; COLOR: white; FONT-FAMILY: Arial; FONT-SIZE: 12px; FONT-WEIGHT: bold; POSITION: absolute; TOP: 3px; WIDTH: 70px">
      <iSD>
Product
      </SDi>
    </DIV>
  </DIV>
  <DIV id=TabD0 onclick="setTab('../Presentation/shgn_gp_gp_LPVF_VALIDFIELDS_DECISION.aspx?pt=LPVF_VALIDFIELDS_DECISION&<%=ClientParams%>','L',this)" language=javascript style="VISIBILITY: visible; CURSOR: hand; OVERFLOW: auto; FONT-FAMILY: tahoma,sans-serif; POSITION: absolute; TOP: 0px">
    <table cellspacing=0 cellpadding=0>
    <tr>
      <td class="tab_Front"></td>
    </tr>
    </table>
    <DIV id=Tab language=javascript style="OVERFLOW: auto; TEXT-ALIGN: center; COLOR: white; FONT-FAMILY: Arial; FONT-SIZE: 12px; FONT-WEIGHT: bold; POSITION: absolute; TOP: 3px; WIDTH: 68px">
      <SDSi>
Product
      </iSDS>
    </DIV>
  </DIV>
</td>
<td>
  <DIV id=TabL1 onclick="setTab('../Presentation/shgn_gp_gp_LPVL_VALIDATION_DECISION.aspx?pt=LPVL_VALIDATION_DECISION&<%=ClientParams%>','D',this)" language=javascript style="VISIBILITY: hidden; CURSOR: hand; OVERFLOW: auto; FONT-FAMILY: tahoma,sans-serif">
    <table cellspacing=0 cellpadding=0>
    <tr>
      <td class="tab_Back"></td>
    </tr>
    </table>
    <DIV id=Tab language=javascript style="OVERFLOW: auto; TEXT-ALIGN: center; COLOR: white; FONT-FAMILY: Arial; FONT-SIZE: 12px; FONT-WEIGHT: bold; POSITION: absolute; TOP: 3px; WIDTH: 70px">
      <iSD>
Decision
      </SDi>
    </DIV>
  </DIV>
  <DIV id=TabD1 onclick="setTab('../Presentation/shgn_gp_gp_LPVL_VALIDATION_DECISION.aspx?pt=LPVL_VALIDATION_DECISION&<%=ClientParams%>','L',this)" language=javascript style="VISIBILITY: visible; CURSOR: hand; OVERFLOW: auto; FONT-FAMILY: tahoma,sans-serif; POSITION: absolute; TOP: 0px">
    <table cellspacing=0 cellpadding=0>
    <tr>
      <td class="tab_Front"></td>
    </tr>
    </table>
    <DIV id=Tab language=javascript style="OVERFLOW: auto; TEXT-ALIGN: center; COLOR: white; FONT-FAMILY: Arial; FONT-SIZE: 12px; FONT-WEIGHT: bold; POSITION: absolute; TOP: 3px; WIDTH: 68px">
      <SDSi>
Decision
      </iSDS>
    </DIV>
  </DIV>
</td>
<td>
  <DIV id=TabL2 onclick="setTab('../Presentation/shgn_gp_gp_LPVD_VALIDATIONDETAIL_DECISION.aspx?pt=LPVD_VALIDATIONDETAIL_DECISION&<%=ClientParams%>','D',this)" language=javascript style="VISIBILITY: hidden; CURSOR: hand; OVERFLOW: auto; FONT-FAMILY: tahoma,sans-serif">
    <table cellspacing=0 cellpadding=0>
    <tr>
      <td class="tab_Back"></td>
    </tr>
    </table>
    <DIV id=Tab language=javascript style="OVERFLOW: auto; TEXT-ALIGN: center; COLOR: white; FONT-FAMILY: Arial; FONT-SIZE: 12px; FONT-WEIGHT: bold; POSITION: absolute; TOP: 3px; WIDTH: 70px">
      <iSD>
Condition
      </SDi>
    </DIV>
  </DIV>
  <DIV id=TabD2 onclick="setTab('../Presentation/shgn_gp_gp_LPVD_VALIDATIONDETAIL_DECISION.aspx?pt=LPVD_VALIDATIONDETAIL_DECISION&<%=ClientParams%>','L',this)" language=javascript style="VISIBILITY: visible; CURSOR: hand; OVERFLOW: auto; FONT-FAMILY: tahoma,sans-serif; POSITION: absolute; TOP: 0px">
    <table cellspacing=0 cellpadding=0>
    <tr>
      <td class="tab_Front"></td>
    </tr>
    </table>
    <DIV id=Tab language=javascript style="OVERFLOW: auto; TEXT-ALIGN: center; COLOR: white; FONT-FAMILY: Arial; FONT-SIZE: 12px; FONT-WEIGHT: bold; POSITION: absolute; TOP: 3px; WIDTH: 68px">
      <SDSi>
Condition
      </iSDS>
    </DIV>
  </DIV>
</td>

</tr>
</table>
<Script language="JavaScript" src="..\shmalib\jscript\SHGN_GeneralFuncsTB.js"></script>  
<Script Language=JavaScript>
	var totalTab=3;
	var currTab=document.getElementById("TabL0");
	var prevTab=document.getElementById("TabL0");

	var barType="TAB";

	function setTab(pos,tabType,objRef) 
	{
		if (currTab.id!="TabL0" && currTab!=null && currTab.id.substring(4)==objRef.id.substring(4))
			return;
		if (prevTab!=currTab) {
			prevTab=currTab;
			currTab=objRef;
		}
		else
			currTab=objRef;
		for (i=0;i<totalTab;i++) {
			document.getElementById("TabD"+i).style.visibility="hidden";
			document.getElementById("TabL"+i).style.visibility="visible";
		}	
		var tabId="TabD";
		if (tabType=="L")
			tabId="TabL";
		tabId+=objRef.id.substring(4);
		objRef.style.visibility="hidden";
		document.getElementById(tabId).style.visibility="visible";
		send(pos);
	}
	var int_FrameId=

<%=(System.Object) Request["pid"] == null?"1":Request["pid"]%>;
	function send(pos)
	{
	        var a= parent.frames[int_FrameId];
		/*while (a==null) {
			int_FrameId--;
			a= parent.frames[int_FrameId];
		}*/
		a.location=pos;
	}
	setTab("../Presentation/shgn_gp_gp_LPVF_VALIDFIELDS_DECISION.aspx?pt=LPVF_VALIDFIELDS_DECISION&<%=ClientParams%>",'D',document.getElementById("TabL0"));
</Script>
</body>
</html>
