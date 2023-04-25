using BusinessLayer;
using Obout.Grid;
using SNC.ErrorLogger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.IO;
//using SelectPdf;

public partial class WorkOrderHire : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    DataSet ds = null;
    SubContractorBL objSubContractorBL = null;
    IndentBL objIndent = null;
    WorkOrderBL objWO = null;
    DailyRunningHourBL objDailyRH = null;
    ProjectBL objProjectBL = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    BindSubContracotrList();
                    BindWOType();
                    BindUsersNames();
                    BindUOM();
                    BindProject();

                    if (Request.QueryString["WONo"] != null)
                    {
                        GetWorkOrderDetails(Request.QueryString["WONo"].ToString());
                        btnPrint.Visible = true;
                        btnPrint.Target = "_blank";
                        btnPrint.HRef = "WOHire_Print.aspx?WONo=" + Request.QueryString["WONo"].ToString();
                        div_Draft.Visible = true;
                        btnPrintPDF.Visible = false;
                        btnAddItem.Visible = true;
                        btnSubmit.Text = "Update";
                    }
                }
                else
                {
                    Response.Redirect("../CommonPages/Login.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindSubContracotrList()
    {
        try
        {
            ds = new DataSet();
            objSubContractorBL = new SubContractorBL();
            objSubContractorBL.load(con, SubContractorBL.eLoadSp.SELECT_CONTRACTOR_ALL, ref ds);
            ddlSubContractor.DataSource = ds;
            ddlSubContractor.DataTextField = "Subcon_name";
            ddlSubContractor.DataValueField = "Subcon_ID";
            ddlSubContractor.DataBind();
            ddlSubContractor.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindWOType()
    {
        try
        {
            ds = new DataSet();
            objWO = new WorkOrderBL();
            objWO.load(con, WorkOrderBL.eLoadSp.SELECT_WO_TYPE_ALL, ref ds);
            ddlWOType.DataSource = ds;
            ddlWOType.DataTextField = "Type_Name";
            ddlWOType.DataValueField = "Type_ID";
            ddlWOType.DataBind();
            ddlWOType.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void btnApproveWHO_Click(object sender, EventArgs e)
    {
        try
        {
            ds = new DataSet();
            objWO = new WorkOrderBL();

            objWO.IsApproved = true;
            objWO.Task = "UpdateApprovelStatus";
            objWO.WONo = Request.QueryString["WONo"];
            objWO.Approver_Com = txtApproverComments.Text;
            if (objWO.load(con, WorkOrderBL.eLoadSp.UPDATE_APROVEL_STAUSWHO))
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('DPO details has been updated sucessfully.');", true);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void BindUsersNames()
    {
        try
        {
            objIndent = new IndentBL();
            ds = new DataSet();
            objIndent.load(con, IndentBL.eLoadSp.SELECT_USERNAMES_ALL, ref ds);
            bool exists;
            DataTable DatafilterDt = new DataTable();

            if (ds.Tables[0].Rows.Count > 0)
            {
                DatafilterDt = ds.Tables[0];

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<bool>("IsHoUser").Equals(true)).Count() > 0;
                if (exists)
                {
                    EnumerableRowCollection<DataRow> query = from order in DatafilterDt.AsEnumerable()
                                                             //where order.Field<bool>("IsHoUser") == true && order.Field<int>("UID") == 9 || order.Field<int>("UID") == 54 || order.Field<int>("UID") == 33 || order.Field<int>("UID") == 74 || order.Field<int>("UID") == 84
                                                             select order;

                    DataView view = query.AsDataView();

                    ddlApprovedBy.DataSource = view;
                    ddlApprovedBy.DataTextField = "Name";
                    ddlApprovedBy.DataValueField = "UID";
                    ddlApprovedBy.DataBind();
                    ddlApprovedBy.Items.Insert(0, "-Select-");
                    ddlApprovedBy.SelectedValue = Session["UID"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindUOM()
    {
        try
        {
            ds = new DataSet();
            objDailyRH = new DailyRunningHourBL();
            objDailyRH.load(con, DailyRunningHourBL.eLoadSp.SELECT_UOM_ALL, ref ds);
            ddlUOM.DataSource = ds;
            ddlUOM.DataTextField = "UOM";
            ddlUOM.DataValueField = "UOM_ID";
            ddlUOM.DataBind();
            ddlUOM.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindProject()
    {
        try
        {
            objProjectBL = new ProjectBL();
            ds = new DataSet();
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_ALL, ref ds);
            ddlProject.DataSource = ds;
            ddlProject.DataTextField = "Project_Name";
            ddlProject.DataValueField = "Project_Code";
            ddlProject.DataBind();
            ddlProject.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }


    private void GetWorkOrderDetails(string WONo)
    {
        try
        {
            objWO = new WorkOrderBL();
            ds = new DataSet();
            objWO.WONo = WONo;
            objWO.Task = "GetWorkOrderHireDetails";
            objWO.load(con, WorkOrderBL.eLoadSp.SELECT_WODETAILS_BY_WONO, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtWONo.Text = ds.Tables[0].Rows[0]["WONo"].ToString();
                txtWODate.Text = ds.Tables[0].Rows[0]["WODate"].ToString();
                ddlProject.SelectedValue = ds.Tables[0].Rows[0]["Project_Code"].ToString();
                ddlWOType.SelectedValue = ds.Tables[0].Rows[0]["WO_Type"].ToString();
                ddlSubContractor.SelectedValue = ds.Tables[0].Rows[0]["SubContractorID"].ToString();
                if (ds.Tables[0].Rows[0]["FYear"].ToString() != string.Empty)
                {
                    ddlFYear.SelectedValue = ds.Tables[0].Rows[0]["FYear"].ToString();
                }
                ddlFYear.Enabled = false;
                if (ds.Tables[0].Rows[0]["Month"].ToString() != string.Empty)
                {
                    ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["Month"].ToString();
                }
                ddlMonth.Enabled = false;
                txtDurationOfWork.Text = ds.Tables[0].Rows[0]["DurationOfWork"].ToString();
                txtWorkLocation.Text = ds.Tables[0].Rows[0]["WorkLocation"].ToString();
                txtPayTerms.Text = ds.Tables[0].Rows[0]["PaymentTerms"].ToString();
                ddlApprovedBy.SelectedValue = ds.Tables[0].Rows[0]["ApprovedBy"].ToString();
                txtGstNo.Text = ds.Tables[0].Rows[0]["GST_No"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                txtOthers.Text = ds.Tables[0].Rows[0]["Other_Terms"].ToString();
                txtContactName.Text = ds.Tables[0].Rows[0]["Contact_Name"].ToString();
                txtContactNo.Text = ds.Tables[0].Rows[0]["Contact_No"].ToString();
                txtScopeOfWork.Text = ds.Tables[0].Rows[0]["ScopeOfWork"].ToString();
                ddlTDSPerc.SelectedValue = ds.Tables[0].Rows[0]["TDSPerc"].ToString();
                ViewState["WOID"] = ds.Tables[0].Rows[0]["WO_ID"].ToString();
                if (ds.Tables[0].Rows[0]["Status"].ToString() != string.Empty)
                {
                    rblStatus.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ApprovalStatus"].ToString() != string.Empty)
                {
                    rdoStatus.SelectedValue = ds.Tables[0].Rows[0]["ApprovalStatus"].ToString();
                    txtDate.Text = ds.Tables[0].Rows[0]["Approval_Date"].ToString();
                    txtComments.Text = ds.Tables[0].Rows[0]["Approver_Com"].ToString();
                }

                if (ds.Tables[0].Rows[0]["ApprovalStatus"].ToString() == "1")
                {
                    btnSubmit.Visible = false;
                }
                if (ds.Tables[0].Rows[0]["IsAprroved"].ToString() == "False")
                {
                    chkDraft.Checked = true;
                    btnPrint.Visible = true;
                    btnApproveWHO.Visible = true;
                }
                else
                {
                }
                if (ds.Tables[0].Rows[0]["ApprovedBy"].ToString() == Session["UID"].ToString())
                {
                    ApproverComments.Visible = true;
                }
                else
                {
                    ApproverComments.Visible = false;
                    btnApproveWHO.Visible = false;
                }

                BindWOItems();
                //BindWOItemWiseTaxGrid();
                //BindTaxGrid();
                //GetTINNoFromLocation();
                
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ddlSubContractor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(ddlSubContractor.SelectedIndex > 0)
            {
                objSubContractorBL = new SubContractorBL();
                ds = new DataSet();
                objSubContractorBL.Subcon_ID = ddlSubContractor.SelectedValue.ToString();
                objSubContractorBL.load(con, SubContractorBL.eLoadSp.SELECT_CONTRACTORDETAILS_BY_ID, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtGstNo.Text = ds.Tables[0].Rows[0]["Regs_No"].ToString();
                    txtContactName.Text = ds.Tables[0].Rows[0]["Con_Person"].ToString();
                    txtContactNo.Text = ds.Tables[0].Rows[0]["Con_No"].ToString();
                }
            }
            else
            {
                txtGstNo.Text = string.Empty;
                txtContactName.Text = string.Empty;
                txtContactNo.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objWO = new WorkOrderBL();
            objWO.UserID = Convert.ToInt32(Session["UID"].ToString());
            objWO.WODate = Convert.ToDateTime(txtWODate.Text.Trim());
            objWO.FYear = ddlFYear.SelectedValue.Trim();
            objWO.Month = ddlMonth.SelectedValue.Trim();
            objWO.ProjectCode = ddlProject.SelectedValue.Trim();
            objWO.SubContractorID = ddlSubContractor.SelectedValue.Trim();
            objWO.WOTypeID = Convert.ToInt32(ddlWOType.SelectedValue);
            objWO.WorkLocation = txtWorkLocation.Text.Trim();
            objWO.DurationOfWork = txtDurationOfWork.Text.Trim();
            objWO.ContactName = txtContactName.Text.Trim();
            objWO.ContactNo = txtContactNo.Text.Trim();
            objWO.PaymentTerms = txtPayTerms.Text.Trim();
            objWO.ApprovedBy = Convert.ToInt32(ddlApprovedBy.SelectedValue.Trim());
            objWO.Status = rblStatus.SelectedValue.Trim();
            objWO.GST_No = txtGstNo.Text.Trim();
            objWO.Remarks = txtRemarks.Text.Trim();
            objWO.Others = txtOthers.Text.Trim();
            objWO.ScopeOfWork = txtScopeOfWork.Text.Trim();
            objWO.TDSPerc = Convert.ToDecimal(ddlTDSPerc.SelectedValue);
            objWO.Task = "InsertWOHire";

            if (btnSubmit.Text == "Submit")
            {
                if(rblStatus.SelectedValue != "Open")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select Open status for Hire Order submission!.');", true);
                    return;
                }
                else
                { 
                    if (objWO.insert(con, WorkOrderBL.eLoadSp.INSERT))
                    {
                        ViewState["WONo"] = objWO.WONo.ToString();
                        ViewState["WOID"] = Convert.ToInt32(objWO.WO_ID);
                        btnPrint.Visible = true;
                        btnPrint.HRef = "WOHire_Print.aspx?PONo=" + ViewState["WONo"].ToString();
                        btnPrint.Target = "_blank";
                        div_Draft.Visible = true;
                        btnPrintPDF.Visible = false;
                        btnAddItem.Visible = true;
                        btnSubmit.Text = "Update";
                        txtWONo.Enabled = false;
                        ddlSubContractor.Enabled = false;
                        txtWONo.Text = objWO.WONo.ToString();
                        
                        //Response.Redirect("WorkOrder.aspx?WONo=" + ViewState["WONo"].ToString() + "&s=1", false);
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Hire Order details has been inserted sucessfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to insert Hire Order details !.');", true);
                    }
                }
            }
            else
            {
                objWO.Task = "UpdateWOHire";
                objWO.WONo = txtWONo.Text.ToString();

                if (objWO.update(con, WorkOrderBL.eLoadSp.UPDATE))
                {
                    btnPrint.Visible = true;
                    btnPrint.HRef = "WOHire_Print.aspx?WONo=" + Request.QueryString["WONo"].ToString();
                    btnPrint.Target = "_blank";
                    div_Draft.Visible = true;
                    btnPrintPDF.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Hire Order details has been updated sucessfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update Hire Order details !.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../Procurement/WorkOrderHireList.aspx", false);
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void BindWOItems()
    {
        try
        {
            objWO = new WorkOrderBL();
            ds = new DataSet();
            objWO.WO_ID = Convert.ToInt32(ViewState["WOID"]);
            objWO.Task = "SelectWOHireItems";
            objWO.load(con, WorkOrderBL.eLoadSp.SELECT_WO_ITEMS_BY_WONO, ref ds);
            Grid_WOItem.DataSource = ds;
            Grid_WOItem.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        ClearItemPopup();
        btnSaveWOItem.Text = "Save";
        ModalWOItem.Show();
    }

    protected void btnSaveWOItem_Click(object sender, EventArgs e)
    {
        try
        {
            objWO = new WorkOrderBL();
            objWO.WONo = txtWONo.Text;
            objWO.Item_Type = txtWorkType.Text;
            objWO.Item_Desc = txtWorkDesc.Text;
            if (Session["WO_Item_Id"] != null)
            {
                objWO.WO_Item_Id = Convert.ToInt32(Session["WO_Item_Id"]);
            }
            if (txtWOPerc.Text != string.Empty)
            {
                objWO.Item_Perc = Convert.ToDecimal(txtWOPerc.Text.Trim());
            }
            if (txtRate.Text != string.Empty)
            {
                objWO.Item_Rate = Convert.ToDecimal(txtRate.Text.Trim());
            }
            if (ddlUOM.SelectedIndex > 0)
            {
                objWO.Item_UOM = Convert.ToInt32(ddlUOM.SelectedValue);
            }
            if (txtWOQty.Text != string.Empty)
            {
                objWO.Item_Qty = Convert.ToDecimal(txtWOQty.Text.Trim());
            }
            objWO.Item_Amt = Convert.ToDecimal(txtAmount.Text.Trim());
            objWO.Gst_Exist = chklTax.SelectedValue == "Inclusive" ? true : false;
            objWO.Igst_Perc = Convert.ToDecimal(txtIgstPerc.Text.Trim());
            objWO.Cgst_Perc = Convert.ToDecimal(txtCgstPerc.Text.Trim());
            objWO.Sgst_Perc = Convert.ToDecimal(txtSgstPerc.Text.Trim());

            if (btnSaveWOItem.Text == "Save")
            {
                objWO.Task = "InsertWOHireItem";
                if (objWO.insertItem(con, WorkOrderBL.eLoadSp.INSERT_WO_ITEM))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Item has been added successfully');", true);
                    ClearItemPopup();
                    BindWOItems();
                }
                else
                {
                    ModalWOItem.Show();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Item already exists for this Work Order !!!');", true);
                }
            
            }
            else
            {
                objWO.Task = "UpdateWOHireItem";
                if (objWO.insertItem(con, WorkOrderBL.eLoadSp.UPDATE_WO_ITEM))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Item has been updated successfully');", true);
                    ClearItemPopup(); 
                    BindWOItems();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update item !!!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancelWOItem_Click(object sender, EventArgs e)
    {
        ClearItemPopup();
    }

    protected void lnkWOItem_Click(object sender, EventArgs e)
    {
        try
        {
            objWO = new WorkOrderBL();
            ds = new DataSet();
            objWO.WO_Item_Id = Convert.ToInt32(((LinkButton)sender).CommandName);
            objWO.Task = "SelectWOHireItemDetails";
            if (objWO.load(con, WorkOrderBL.eLoadSp.SELECT_WO_ITEM_BY_ITEM_ID, ref ds))
            {
                txtWorkType.Text = ds.Tables[0].Rows[0]["Item_Type"].ToString();
                txtWorkType.Enabled = false;
                txtWorkDesc.Text = ds.Tables[0].Rows[0]["Item_Desc"].ToString();
                txtWOPerc.Text = ds.Tables[0].Rows[0]["Item_Perc"].ToString();
                txtWOQty.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
                txtRate.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
                txtAmount.Text = ds.Tables[0].Rows[0]["Total_Amt"].ToString();
                ddlUOM.SelectedValue = ds.Tables[0].Rows[0]["UOM"].ToString();
                if (ds.Tables[0].Rows[0]["Gst_Exist"].ToString() != "")
                {
                    chklTax.SelectedValue = Convert.ToBoolean(ds.Tables[0].Rows[0]["Gst_Exist"]) == true ? "Inclusive" : "Exclusive";
                }
                txtIgstPerc.Text = ds.Tables[0].Rows[0]["Igst_Perc"].ToString();
                txtIgstAmt.Text = ds.Tables[0].Rows[0]["Igst_Amt"].ToString();
                txtCgstPerc.Text = ds.Tables[0].Rows[0]["Cgst_Perc"].ToString();
                txtCgstAmt.Text = ds.Tables[0].Rows[0]["Cgst_Amt"].ToString();
                txtSgstPerc.Text = ds.Tables[0].Rows[0]["Sgst_Perc"].ToString();
                txtSgstAmt.Text = ds.Tables[0].Rows[0]["Sgst_Amt"].ToString();
                btnSaveWOItem.Text = "Update";
                ModalWOItem.Show();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_WOItem_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objWO = new WorkOrderBL();
            objWO.WO_Item_Id = Convert.ToInt32(e.Record["WO_Item_Id"]);
            objWO.Task = "DeleteWOHireItem";
            if (objWO.delete(con, WorkOrderBL.eLoadSp.DELETE_WO_ITEM_BY_ID))
            {
                BindWOItems();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Hire Order Item has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete Hire Order Item !');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    
    protected void ClearItemPopup()
    {
        btnSaveWOItem.Text = "Save";
        txtWorkType.Text = "";
        txtWorkDesc.Text = "";
        txtRate.Text = "0.00";
        txtAmount.Text = "0.00";
        txtWOQty.Text = "";
        ddlUOM.SelectedIndex = -1;
        txtIgstPerc.Text = "0.00";
        txtIgstAmt.Text = "0.00";
        txtCgstPerc.Text = "0.00";
        txtCgstAmt.Text = "0.00";
        txtSgstPerc.Text = "0.00";
        txtSgstAmt.Text = "0.00";
    }

    protected void btnPrintPDF_Click(object sender, EventArgs e)
    {
        try
        {
            string MsgBody = "";
            string Url = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/" + "Procurement/WOHire_Print.aspx?WONo=" + Request.QueryString["WONo"].ToString();
            MsgBody = getHTMLFromURL(Url);
            //SavePDF_HO(Url);
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private string getHTMLFromURL(string Url)
    {
        HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(Url);
        webrequest.Method = "GET";
        webrequest.ContentLength = 0;
        string result = "";
        WebResponse response = webrequest.GetResponse();

        using (StreamReader stream = new StreamReader(response.GetResponseStream()))
        {
            result = stream.ReadToEnd();
        }
        return result;
    }

    //string FileNametosave = null;
    //public void SavePDF_HO(string MsgBody)
    //{
    //    try
    //    {
    //        HtmlToPdf pdf = new HtmlToPdf();
    //        FileNametosave = "HO_" + Request.QueryString["WONo"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + ".pdf";

    //        string url = MsgBody + "&Pdf=1";
    //        if (url != "")
    //        {
    //            SelectPdf.PdfDocument doc = pdf.ConvertUrl(url);
    //            var dir = HttpContext.Current.Server.MapPath("~/Downloaded_PDF/");
    //            if (!Directory.Exists(dir))
    //                Directory.CreateDirectory(dir);
    //            var fileName = dir + FileNametosave.Replace("/", "-");
    //            doc.Save(fileName);
    //            doc.Close();
    //            //ScriptManager.RegisterStartupScript(this, this.GetType(), "showSaveMessage", "swal('Success', 'PDF File Saved Succcessfully', 'success');", true);
    //        }
    //        else
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "showSaveMessage", "swal('Alert', 'Hire Order Print cannot be created.', 'alert');", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["ID"] != null ? Session["ID"].ToString() : string.Empty);
    //    }
    //}

    protected void chkDraft_ChckedChanged(object sender, EventArgs e)
    {
        if (chkDraft.Checked == true)
        {
            btnPrint.HRef = "WOHire_Print.aspx?WONo=" + Request.QueryString["WONo"].ToString() + "&Draft=1";
        }
        else
        {
            btnPrint.HRef = "WOHire_Print.aspx?WONo=" + Request.QueryString["WONo"].ToString();
        }
    }

}