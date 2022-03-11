using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Reflection;
using SHMA.Enterprise;
using SHMA.Enterprise.Data;
using SHMA.Enterprise.Shared;
using SHMA.Enterprise.Presentation;
using SHMA.Enterprise.Exceptions;
using shsm;
using SHAB.Data;
using SHAB.Business; 
using SHAB.Shared.Exceptions;

namespace SHAB.Presentation
{
	//shgn_gs_se_stdgridscreen_
	public partial class shgn_ss_se_stdscreen_ILUS_ST_CHANNEL : SHMA.Enterprise.Presentation.TwoStepController{
	
		//controls

		protected System.Web.UI.HtmlControls.HtmlInputButton btnHideLister;
		protected System.Web.UI.WebControls.Literal _lastEvent;


		protected System.Web.UI.WebControls.Literal MessageScript;
		protected System.Web.UI.WebControls.Literal FooterScript;
		protected System.Web.UI.WebControls.Literal HeaderScript;
		

		private int pageNumber=1;
		int PAGE_SIZE= 20 ;
		private int recordCount=0;
		bool recordSelected = false;
		
		NameValueCollection columnNameValue=null;
	
		string[] AllProcess = {"shsm.SHSM_VerifyTransaction", "shsm.SHSM_RejectTransaction", "dummy_process"};
		string AllowedProcess = "";
		
		shgn.SHGNCommand entityClass;
				
				
	#region pk variables declaration		
			private string  CCH_CODE;
						
	#endregion
				
	#region Web Form Designer generated code
		override protected void OnInit(EventArgs e) 
		{
			InitializeComponent();
			base.OnInit(e);

			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
			Response.Cache.SetNoStore();
		}
		
		private void InitializeComponent(){
			this.lister.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.lister_ItemDataBound);
			this.lister.ItemCommand += new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.lister_ItemCommand);

		}
	#endregion		


	#region Major methods of First Step		
		protected override void ValidateParams() 
		{
			base.ValidateParams ();			
			string[] param;
			foreach (string key in Request.Params.AllKeys)
			{
				if (key!=null && key.StartsWith("r_"))
				{
					param = Request[key].Split(',');
					SessionObject.Set(key.Replace("r_",""), param[param.Length-1]); 					
				}
			}
		}
		sealed protected override DataHolder GetInputData(DataHolder dataHolder)
		{
			GetSessionValues();
			CheckKeyLevel();
			//recordCount = CCH_CHANNELDB.RecordCount;
			return   dataHolder;      
		}
	
		sealed protected override void BindInputData(DataHolder dataHolder)
		{
	
						
			_lastEvent.Text = "New";		

			DataTable table = new DataTable("CCH_CHANNEL");
			IDataReader CCH_CHANNELReader= CCH_CHANNELDB.GetILUS_ST_CHANNEL_lister_RO(pageNumber * PAGE_SIZE, PAGE_SIZE);
			recordSelected = IsRecordSelected();
			if (recordSelected)
			{
				recordCount = Utilities.Reader2Table(CCH_CHANNELReader, table, PAGE_SIZE, CCH_CHANNEL.PrimaryKeys, out pageNumber);
			}
			else
			{
				recordCount = Utilities.Reader2Table(CCH_CHANNELReader, table, PAGE_SIZE, pageNumber);
			}
			CCH_CHANNELReader.Close();
			BindLister(table);							
		

			HeaderScript.Text = EnvHelper.Parse("") ;
			FooterScript.Text = EnvHelper.Parse("") ;
				
			
		
			/************** Array Data Script **************/
			
			
			RegisterArrayDeclaration("AllowedProcess", AllowedProcess);
			pagerList.SelectedIndex = pageNumber-1;		
			
			SetLastEvent();
			SetListerVisibility();
		}
	#endregion
    
	#region Major methods of the final step
		protected override void ValidateRequest() {
			base.ValidateRequest();									
			foreach (string key in CCH_CHANNEL.PrimaryKeys){
				Control ctrl = myForm.FindControl("txt" + key);				
				if (ctrl!=null){
					if (ctrl is WebControl){
					//TextBox textBox = (TextBox)ctrl;
						WebControl control = (WebControl)ctrl;
						if ((control.Enabled == false) && (Request[control.UniqueID]!=null)){
							control.Enabled = true;
						}				
					}
				}
			}			
		}
		sealed protected override DataHolder GetData(DataHolder dataHolder) {	
			pageNumber = pagerList.SelectedIndex +1;
			//recordCount = CCH_CHANNELDB.RecordCount;
			return dataHolder;
		}      
	
		sealed protected override void ApplyDomainLogic(DataHolder dataHolder){
			SHSM_AuditTrail auditTrail = new SHSM_AuditTrail();
			columnNameValue=new NameValueCollection();
			SaveTransaction = false;		
			
			SHSM_SecurityPermission security;
			switch ((EnumControlArgs)ControlArgs[0])
			{
			case (EnumControlArgs.Save):
				_lastEvent.Text = "Save";
				DB.BeginTransaction();
				SaveTransaction = true;
				dataHolder = new CCH_CHANNELDB(dataHolder).FindByPK(txtCCH_CODE.Text);
				columnNameValue.Add("CCH_CODE",txtCCH_CODE.Text.Trim()==""?null:txtCCH_CODE.Text);
columnNameValue.Add("CCH_DESCR",txtCCH_DESCR.Text.Trim()==""?null:txtCCH_DESCR.Text);
								
				security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), CCH_CHANNEL.PrimaryKeys, columnNameValue, "CCH_CHANNEL");
				if (security.SaveAllowed){
					
					new CCH_CHANNEL(dataHolder).Add(columnNameValue,getAllFields(),"ILUS_ST_CHANNEL",null);

					dataHolder.Update(DB.Transaction);
					
					auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()),CCH_CHANNEL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_INSERT, "CCH_CHANNEL");
					_lastEvent.Text = "Save"; 					
					PrintMessage("Record has been saved");
				}
				else{
					PrintMessage("You are not autherized to Save.");
				}
				break;
			case (EnumControlArgs.Update):					
				DB.BeginTransaction();
				SaveTransaction = true;
				dataHolder = new CCH_CHANNELDB(dataHolder).FindByPK(txtCCH_CODE.Text);				
				columnNameValue.Add("CCH_CODE",txtCCH_CODE.Text.Trim()==""?null:txtCCH_CODE.Text);
columnNameValue.Add("CCH_DESCR",txtCCH_DESCR.Text.Trim()==""?null:txtCCH_DESCR.Text);

				security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), CCH_CHANNEL.PrimaryKeys, columnNameValue, "CCH_CHANNEL");
				if (security.UpdateAllowed){
					
					new CCH_CHANNEL(dataHolder).Update(Utilities.File2EntityID(this.ToString()),columnNameValue);

					dataHolder.Update(DB.Transaction);
					
					auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), CCH_CHANNEL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_UPDATE, "CCH_CHANNEL");
					recordSelected = true;
					PrintMessage("Record has been updated");
				}
				else{
					PrintMessage("You are not autherized to Update.");
				}
				break;
			case (EnumControlArgs.Delete):
				DB.BeginTransaction();
				SaveTransaction = true;
				dataHolder = new CCH_CHANNELDB(dataHolder).FindByPK(txtCCH_CODE.Text);				
				columnNameValue.Add("CCH_CODE",txtCCH_CODE.Text.Trim()==""?null:txtCCH_CODE.Text);
columnNameValue.Add("CCH_DESCR",txtCCH_DESCR.Text.Trim()==""?null:txtCCH_DESCR.Text);

				security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), CCH_CHANNEL.PrimaryKeys, columnNameValue, "CCH_CHANNEL");
				if (security.DeleteAllowed){
				
				new CCH_CHANNEL(dataHolder).Delete(columnNameValue);

				dataHolder.Update(DB.Transaction);
				
				auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), CCH_CHANNEL.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "CCH_CHANNEL");
				PrintMessage("Record has been deleted");				
				}
				else{
					PrintMessage("You are not autherized to Delete.");
				}

				break;
			case (EnumControlArgs.Process):						
				DB.BeginTransaction();
				SaveTransaction = true;
				dataHolder = new CCH_CHANNELDB(dataHolder).FindByPK(txtCCH_CODE.Text);				
				columnNameValue.Add("CCH_CODE",txtCCH_CODE.Text.Trim()==""?null:txtCCH_CODE.Text);
columnNameValue.Add("CCH_DESCR",txtCCH_DESCR.Text.Trim()==""?null:txtCCH_DESCR.Text);

				security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), CCH_CHANNEL.PrimaryKeys, columnNameValue, "CCH_CHANNEL");
				string result="";					
				if (_CustomArgName.Value == "ProcessName"){
					string processName = _CustomArgVal.Value;	
					if (security.ProcessAllowed(processName)){
						Type type = Type.GetType(processName);											
						if (type != null){
							shgn.ProcessCommand proccessCommand = (shgn.ProcessCommand)Activator.CreateInstance(type);
							NameValueCollection[] dataRows = new NameValueCollection[1];
							bool[] SelectedRowIndexes = new bool[1];
							//dataRows[0] = columnNameValue;
							dataRows[0] = getAllFields();
							SelectedRowIndexes[0] = true;
							proccessCommand.setAllFields(getAllFields());
							proccessCommand.setEntityID(Utilities.File2EntityID(this.ToString()));
							proccessCommand.setPrimaryKeys(CCH_CHANNEL.PrimaryKeys);
							proccessCommand.setTableName("CCH_CHANNEL");
							proccessCommand.setDataRows(dataRows);
							proccessCommand.setSelectedRows(SelectedRowIndexes);
							result = proccessCommand.processing();
							//auditTrail.fssaveAuditLog(Utilities.File2EntityID(this.ToString()), PR_GL_CA_ACCOUNT.PrimaryKeys, columnNameValue, SHSM_AuditTrail.DML_OPERATION_DELETE, "PR_GL_CA_ACCOUNT");
						}
					}
					else{
						result = "You are not Autherized to Execute Process.";
					}						
				}	
				recordSelected =true;
				if (result.Length>0)
					PrintMessage(result);
				break;
			}
		}
		
		sealed protected override void DataBind(DataHolder dataHolder){			
			
		  	
			
			CCH_CHANNELDB CCH_CHANNELDB_obj = new CCH_CHANNELDB(dataHolder);		
			IDataReader CCH_CHANNELReader;
			DataTable table = new DataTable("CCH_CHANNEL") ;

			if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Edit){
				DataRow row = CCH_CHANNELDB_obj.FindByPK(CCH_CODE)["CCH_CHANNEL"].Rows[0];
				ShowData(row);
			}		
			else{
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Filter){
					pageNumber = 1;
					ViewState["filterCol"] = _CustomArgName.Value;
					ViewState["filterVal"] = _CustomArgVal.Value;
				}
				
				if (ViewState["filterVal"]==null || ViewState["filterVal"].ToString().Trim()=="%")
					CCH_CHANNELReader = CCH_CHANNELDB.GetILUS_ST_CHANNEL_lister_RO(pageNumber * PAGE_SIZE, PAGE_SIZE);//get_Orders_Data_RO();				
				else
					CCH_CHANNELReader = CCH_CHANNELDB.GetILUS_ST_CHANNEL_lister_filter_RO(ViewState["filterCol"].ToString(), ViewState["filterVal"].ToString());//get_Orders_Data_RO(ViewState["filterCol"].ToString(), ViewState["filterVal"].ToString());
				recordCount = Utilities.Reader2Table(CCH_CHANNELReader, table, PAGE_SIZE, pageNumber);
				CCH_CHANNELReader.Close();
	
				BindLister(table);
                        if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Delete)
				RefreshDataFields();
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save)
				{
					ShowData(dataHolder["CCH_CHANNEL"].Rows[0]);
				}		
			}
			/* a temporary work arround for errors in save replace it later with proper error flow */
			if (_lastEvent.Text == EnumControlArgs.View.ToString()){
				SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), CCH_CHANNEL.PrimaryKeys, columnNameValue, "CCH_CHANNEL");
				if (!security.UpdateAllowed)
					_lastEvent.Text = EnumControlArgs.View.ToString() ;
				else{
					if (ControlArgs[0] != null)
						_lastEvent.Text = ControlArgs[0].ToString();
				}
			}
			else{
				if ((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Save){
					_lastEvent.Text = EnumControlArgs.Edit.ToString();	
				}			
				else{
					if((EnumControlArgs)ControlArgs[0] == EnumControlArgs.Delete)
					{
						_lastEvent.Text = "New";
					}
					else
						_lastEvent.Text = ((EnumControlArgs)ControlArgs[0]).ToString();
				}
			}
			//for header & footer script					
			RegisterArrayDeclaration("AllowedProcess", AllowedProcess);	

			HeaderScript.Text = EnvHelper.Parse("");
			FooterScript.Text = EnvHelper.Parse("");

			pagerList.SelectedIndex = pageNumber - 1;			
			
			
			SetLastEvent();
			SetListerVisibility();
		}
	#endregion	

	#region Events
		protected void pagerList_SelectedIndexChanged(object sender, System.EventArgs e) {
			pageNumber = pagerList.SelectedIndex+1;
			ControlArgs=new object[1];
			ControlArgs[0]=EnumControlArgs.Pager;
			DoControl();
			pagerList.SelectedIndex=pageNumber-1;
		}
		private void btnViewAll_Click(object sender, System.EventArgs e) {
			ControlArgs=new object[1];
			ControlArgs[0]=EnumControlArgs.Cancel  ;
			DoControl();
		}
		
		private void lister_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e) {
			foreach (RepeaterItem item in lister.Items){
				if (item == e.Item){
					((HtmlTableRow)item.FindControl("ListerRow")).Attributes["class"] = "ListerSelItem";
				}
				else{
					if (item.ItemType == ListItemType.Item)
						((HtmlTableRow)item.FindControl("ListerRow")).Attributes["class"] = "ListerItem";
					else
						((HtmlTableRow)item.FindControl("ListerRow")).Attributes["class"] = "ListerAlterItem";
				}
			}
			if (e.CommandName == "Edit") {								
				if (e.Item.ItemType==ListItemType.Item){									
					CCH_CODE=((LinkButton)e.Item.FindControl("linkCCH_CODE1")).Text;

				}
				else if (e.Item.ItemType==ListItemType.AlternatingItem){
					CCH_CODE=((LinkButton)e.Item.FindControl("linkCCH_CODE2")).Text;
	
				}
				ControlArgs=new object[1];
				ControlArgs[0]=EnumControlArgs.Edit; 
				DoControl();								
			}				
		}	
		protected void _CustomEvent_ServerClick(object sender, System.EventArgs e) {
			ControlArgs = new object[1];
			switch (_CustomEventVal.Value){
				case "Update" :
					ControlArgs[0]=EnumControlArgs.Update;
					DoControl();
					break;
				case "Save" :
					ControlArgs[0]=EnumControlArgs.Save;
					DoControl();
					break;
				case "Delete" :
					ControlArgs[0]=EnumControlArgs.Delete;
					DoControl();
					break;
				case "Filter" :
					ControlArgs[0] = EnumControlArgs.Filter;
					DoControl();
					break;
				case "Process" :
					ControlArgs[0] = EnumControlArgs.Process;
					DoControl();
					break;	
			}
			_CustomEventVal.Value="";										
		}		
		private void lister_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e) 
		{
			if (recordSelected)
				FindAndSelectCurrentRecord(e);
			HtmlTableRow tRow = (HtmlTableRow)e.Item.FindControl("ListerRow");
			LinkButton linkCCH_CODE = new LinkButton();
			if (e.Item.ItemType==ListItemType.Item){
				linkCCH_CODE = (LinkButton)e.Item.FindControl("linkCCH_CODE1");
			}
			else if (e.Item.ItemType==ListItemType.AlternatingItem){
				linkCCH_CODE=(LinkButton)e.Item.FindControl("linkCCH_CODE2");	
			}			
			tRow.Attributes.Add("onclick", linkCCH_CODE.ClientID + ".click();" );
		}
		protected void Page_Unload(object sender, System.EventArgs e)
		{
			//base.OnUnload(e);
			if (SetFieldsInSession()){
				SessionObject.Set("CCH_CODE",txtCCH_CODE.Text);
		SessionObject.Set("CCH_DESCR",txtCCH_DESCR.Text);
		
			}
		}	
	
	#endregion 
		protected override bool TransactionRequired {
	 	 get {
			return true;
		     }
		}

		private void GetSessionValues()
		{
			if (false){	
				DisableForm();
				throw new SHAB.Shared.Exceptions.SessionValNotFoundException("Select value first");
			}
			else
			{
				



				//ltlorg_code.Text = SessionObject.GetString("org_code");
			}
		}		
		
		private void CheckKeyLevel()
		{
			
		}

		void RefreshDataFields()
		{
			//SessionObject.Set(<entity-field>, row["<entity-field>"].ToString());
			txtCCH_CODE.Enabled = true;
txtCCH_CODE.Text="";
txtCCH_DESCR.Text="";

		}

		protected void ShowData(DataRow objRow)
		{
			RefreshDataFields();
			txtCCH_CODE.Text=objRow["CCH_CODE"].ToString();
txtCCH_CODE.Enabled=false;
txtCCH_DESCR.Text=objRow["CCH_DESCR"].ToString();


			if (columnNameValue == null || columnNameValue.Count == 0)
				columnNameValue = Utilities.RowToNameValue(objRow);
			SHSM_SecurityPermission security = new SHSM_SecurityPermission( Utilities.File2EntityID(this.ToString()), CCH_CHANNEL.PrimaryKeys, columnNameValue, "CCH_CHANNEL");
			foreach(string processName in AllProcess){
				if (security.ProcessAllowed(processName)){
					AllowedProcess += "'" + processName + "'" + "," ;
				}
			}
			if (AllowedProcess.Length>0)
				AllowedProcess = AllowedProcess.Substring(0, AllowedProcess.Length-1);
			if (!security.UpdateAllowed){
				_lastEvent.Text = EnumControlArgs.View.ToString();
			}
		}

		void SetLastEvent(){
		          if (Request["Operation"] == "View")
	                   _lastEvent.Text = "View" ;
 	        }

		void SetListerVisibility(){

                    Utilities.SetListerVisibility(this);
	        }



		private void BindLister(DataTable table)
		{
			lister.DataSource = table;
			lister.DataBind();
			pagerList.Items.Clear();
			for (int i=1;recordCount>0; recordCount-=PAGE_SIZE){				
				pagerList.Items.Add(i.ToString());		
				i++;
			}
			
			//pagerList.SelectedIndex = pageNumber-1;//commented bcz of pagging error
		}

		protected sealed override string ErrorHandle(string message)
		{
			message = base.ErrorHandle(message);
			PrintMessage(message);return message;
		}
		protected void PrintMessage(string message)
		{
			MessageScript.Text = string.Format("alert('{0}')", message.Replace("'","").Replace("\n","").Replace("\r",""));
		}

		bool SetFieldsInSession()
		{
			bool flag = false;
			if (_lastEvent.Text.Equals(EnumControlArgs.Edit.ToString()))
			{
				flag = true;
			}
			else 
			{				
				if (ControlArgs!=null)
				{
					if (ControlArgs[0]!=null)
					{
						EnumControlArgs arg = (EnumControlArgs)ControlArgs[0] ;
						if (arg.Equals(EnumControlArgs.Save) || arg.Equals(EnumControlArgs.Edit))
						{
							flag = true;
						}
					}					
				}
			}
			return flag;
		}

		/**
		 * New Method Added For New Support
	 	 */
		private NameValueCollection getAllFields() {
			NameValueCollection allFields = new NameValueCollection();
			foreach(object key in columnNameValue.Keys) {
				string strKey = key.ToString();
				allFields.add(strKey, columnNameValue.get(strKey));
			}

			//foreach (Control c in this.myForm.Controls) {	
			foreach (Control c in this.EntryTableDiv.Controls) {	
				string _fieldName="";
				if (c is WebControl) {
					switch (c.GetType().ToString()) {
						case "System.Web.UI.WebControls.TextBox":
							if (c.ID.IndexOf("txt")==0)
								_fieldName = c.ID.Replace("txt","");
							else
								_fieldName = c.ID;
							if (!columnNameValue.Contains(_fieldName)) {
								allFields.add(_fieldName, ((TextBox)c).Text);
							}
							break;
						case "SHMA.Enterprise.Presentation.WebControls.DropDownList":
							if (c.ID.IndexOf("ddl")==0)
								_fieldName = c.ID.Replace("ddl","");
							else
								_fieldName = c.ID;
							if (!columnNameValue.Contains(_fieldName)) {
								allFields.add(_fieldName, ((DropDownList)c).SelectedValue.ToString());
							}
							break;
					}
				}
			}	
			return allFields;
		}


		bool IsRecordSelected(){
			bool selected = true;
			foreach (string pk in CCH_CHANNEL.PrimaryKeys){
				string strPK = SessionObject.GetString(pk);
				if (strPK == null || strPK.Trim().Length == 0){
					selected  = false;
				}				
			}
			return selected ;
		}
		private void FindAndSelectCurrentRecord(System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			RepeaterItem item=e.Item;
			bool recordFound = true;
			DataRowView row = (DataRowView)item.DataItem;
			foreach(string pk in  CCH_CHANNEL.PrimaryKeys){
				if ((SessionObject.Get(pk)!=null) && (!SessionObject.GetString(pk).Equals(row.Row[pk].ToString())))
					recordFound = false;
			}
			if (recordFound){
				if (item.ItemType == ListItemType.Item){
					CCH_CODE=((LinkButton)e.Item.FindControl("linkCCH_CODE1")).Text;
	
				}
				else{
					CCH_CODE=((LinkButton)e.Item.FindControl("linkCCH_CODE2")).Text;
	
				}
				((HtmlTableRow)item.FindControl("ListerRow")).Attributes["class"] = "ListerSelItem";
				DataRow selectedRow = new CCH_CHANNELDB(dataHolder).FindByPK(CCH_CODE)["CCH_CHANNEL"].Rows[0];
				ShowData(selectedRow);							
				_lastEvent.Text = "Edit";
			}
		}
		void DisableForm(){
			if (btnHideLister!=null)
				btnHideLister.Disabled=true;
			EntryTableDiv.Style.Add("visibility" , "hidden");
			ListerDiv.Style.Add("visibility" , "hidden");
			HeaderScript.Text = "";
			FooterScript.Text = "";
			_lastEvent.Text = EnumControlArgs.None.ToString();//new induction	

		}
		System.Web.UI.ControlCollection EntryFormFields{
			get{	
				return EntryTableDiv.Controls;
			}
		}
	}
}

