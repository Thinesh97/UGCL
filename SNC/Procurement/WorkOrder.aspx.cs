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

public partial class WorkOrder : System.Web.UI.Page
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
                        btnPrint.HRef = "WO_Print.aspx?WONo=" + Request.QueryString["WONo"].ToString();
                        div_Draft.Visible = true;
                        div_BeforeUpload.Visible = true;
                        btnPrintPDF.Visible = false;
                        btnAddItem.Visible = true;
                        btnAddSOW.Visible = true;
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
    
    protected void btnApproveWO_Click(object sender, EventArgs e)
    {
        try
        {
            ds = new DataSet();
            objWO = new WorkOrderBL();

            objWO.IsApproved = true;
            objWO.Task = "UpdateApprovelStatus";
            objWO.WONo = Request.QueryString["WONo"];
            objWO.Approver_Com = txtApproverComments.Text;
            if (objWO.load(con, WorkOrderBL.eLoadSp.UPDATE_APROVEL_STAUSWO))
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('DPO details has been updated sucessfully.');", true);
            }
        }
        catch (Exception)
        {

            throw;
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

            ddlUOM_Sub.DataSource = ds;
            ddlUOM_Sub.DataTextField = "UOM";
            ddlUOM_Sub.DataValueField = "UOM_ID";
            ddlUOM_Sub.DataBind();
            ddlUOM_Sub.Items.Insert(0, "-Select-");

            ddlUOMSOW.DataSource = ds;
            ddlUOMSOW.DataTextField = "UOM";
            ddlUOMSOW.DataValueField = "UOM_ID";
            ddlUOMSOW.DataBind();
            ddlUOMSOW.Items.Insert(0, "-Select-");
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
            objWO.Task = "GetWorkOrderDetails";
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
                rblOrderType.SelectedValue = ds.Tables[0].Rows[0]["OrderType"].ToString();
                txtPayTerms.Text = ds.Tables[0].Rows[0]["PaymentTerms"].ToString();
                ddlApprovedBy.SelectedValue = ds.Tables[0].Rows[0]["ApprovedBy"].ToString();
                txtGstNo.Text = ds.Tables[0].Rows[0]["GST_No"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                txtOthers.Text = ds.Tables[0].Rows[0]["Other_Terms"].ToString();
                txtContactName.Text = ds.Tables[0].Rows[0]["Contact_Name"].ToString();
                txtContactNo.Text = ds.Tables[0].Rows[0]["Contact_No"].ToString();
                ddlTDSPerc.SelectedValue = ds.Tables[0].Rows[0]["TDSPerc"].ToString();
                lnkDownloadFile.Text = ds.Tables[0].Rows[0]["Uploaded_File"].ToString();
                div_AfterUpload.Visible = ds.Tables[0].Rows[0]["Uploaded_File"].ToString() != string.Empty ? true : false;
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

                if (ds.Tables[0].Rows[0]["ApprovalStatus"].ToString() == "Approved")
                {
                    btnSubmit.Visible = false;
                }
                if (ds.Tables[0].Rows[0]["ApprovalStatus"].ToString() == "1")
                {
                    btnSubmit.Visible = false;
                }
                if (ds.Tables[0].Rows[0]["IsAprroved"].ToString() == "False")
                {
                    chkDraft.Checked = true;
                    btnPrint.Visible = true;
                    btnApproveWO.Visible = true;
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
                    btnApproveWO.Visible = false;
                }
                BindWOItems();
                BindSOWItems();
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
            if (ddlSubContractor.SelectedIndex > 0)
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
            objWO.ProjectCode = ddlProject.SelectedValue.Trim();
            objWO.FYear = ddlFYear.SelectedValue.Trim();
            objWO.Month = ddlMonth.SelectedValue.Trim();
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
            objWO.TDSPerc = Convert.ToDecimal(ddlTDSPerc.SelectedValue);
            if (rblOrderType.SelectedValue != "")
            {
                objWO.OrderType = rblOrderType.SelectedValue.Trim();
            }

            objWO.Task = "InsertWO";

            if (btnSubmit.Text == "Submit")
            {
                if (rblStatus.SelectedValue != "Open")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select Open status for Work Order submission!.');", true);
                    return;
                }
                else
                {
                    if (objWO.insert(con, WorkOrderBL.eLoadSp.INSERT))
                    {
                        ViewState["WONo"] = objWO.WONo.ToString();
                        ViewState["WOID"] = Convert.ToInt32(objWO.WO_ID);
                        btnPrint.Visible = true;
                        btnPrint.HRef = "WO_Print.aspx?WONo=" + ViewState["WONo"].ToString();
                        btnPrint.Target = "_blank";
                        div_Draft.Visible = true;
                        div_BeforeUpload.Visible = true;
                        btnPrintPDF.Visible = false;
                        btnAddItem.Visible = true;
                        btnAddSOW.Visible = true;
                        btnSubmit.Text = "Update";
                        txtWONo.Enabled = false;
                        ddlSubContractor.Enabled = false;
                        txtWONo.Text = objWO.WONo.ToString();

                        //Response.Redirect("WorkOrder.aspx?WONo=" + ViewState["WONo"].ToString() + "&s=1", false);
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('WO details has been inserted sucessfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to insert WO details !.');", true);
                    }
                }
            }
            else
            {
                objWO.Task = "UpdateWO";
                objWO.WONo = txtWONo.Text.ToString();
                if (fuWODoc.HasFile)
                {
                    objWO.Uploaded_File = "WO_" + txtWONo.Text.ToString() + ".pdf";
                }

                if (objWO.update(con, WorkOrderBL.eLoadSp.UPDATE))
                {
                    if (fuWODoc.HasFile)
                    {
                        fuWODoc.SaveAs(Server.MapPath("~\\UploadedFiles\\WO_" + txtWONo.Text.Replace("/", "-") + ".pdf"));
                        lnkDownloadFile.Text = "WO_" + txtWONo.Text + ".pdf";
                        div_AfterUpload.Visible = true;
                    }
                    btnPrint.Visible = true;
                    btnPrint.HRef = "WO_Print.aspx?WONo=" + txtWONo.Text.ToString();
                    btnPrint.Target = "_blank";
                    div_Draft.Visible = true;
                    div_BeforeUpload.Visible = true;
                    btnPrintPDF.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('WO details has been updated sucessfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update WO details !.');", true);
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
            Response.Redirect("../Procurement/WorkOrderList.aspx", false);
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
            objWO.Task = "SelectWOItems";
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
            if (txtAmount.Text != string.Empty)
            {
                objWO.Item_Amt = Convert.ToDecimal(txtAmount.Text.Trim());
            }
            objWO.Gst_Exist = chklTax.SelectedValue == "Inclusive" ? true : false;
            objWO.Igst_Perc = Convert.ToDecimal(txtIgstPerc.Text.Trim());
            objWO.Cgst_Perc = Convert.ToDecimal(txtCgstPerc.Text.Trim());
            objWO.Sgst_Perc = Convert.ToDecimal(txtSgstPerc.Text.Trim());

            if (btnSaveWOItem.Text == "Save")
            {
                objWO.Task = "InsertWOItem";
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
                objWO.Task = "UpdateWOItem";
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
                btnAddSubItem.Visible = false;
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
        btnAddSubItem.Visible = false;
    }

    protected void lnkWOItem_Click(object sender, EventArgs e)
    {
        try
        {
            objWO = new WorkOrderBL();
            ds = new DataSet();
            objWO.WO_Item_Id = Convert.ToInt32(((LinkButton)sender).CommandName);
            Session["WO_Item_Id"] = Convert.ToInt32(((LinkButton)sender).CommandName);
            objWO.Task = "SelectWOItemDetails";
            if (objWO.load(con, WorkOrderBL.eLoadSp.SELECT_WO_ITEM_BY_ITEM_ID, ref ds))
            {
                txtWorkType.Text = ds.Tables[0].Rows[0]["Item_Type"].ToString();
                txtWorkType.Enabled = false;
                txtWorkDesc.Text = ds.Tables[0].Rows[0]["Item_Desc"].ToString();
                txtWOPerc.Text = ds.Tables[0].Rows[0]["Item_Perc"].ToString();
                txtWOQty.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
                txtRate.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
                txtAmount.Text = ds.Tables[0].Rows[0]["Total_Amt"].ToString();
                if (ds.Tables[0].Rows[0]["UOM"].ToString() != "0" && ds.Tables[0].Rows[0]["UOM"].ToString() != string.Empty)
                {
                    ddlUOM.SelectedValue = ds.Tables[0].Rows[0]["UOM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Gst_Exist"].ToString() != "")
                {
                    chklTax.SelectedValue = Convert.ToBoolean(ds.Tables[0].Rows[0]["Gst_Exist"]) == true ? "Inclusive" : "Exclusive";
                }
                txtIgstPerc.Text = ds.Tables[0].Rows[0]["Igst_Perc"].ToString();
                txtIgstAmt.Text = ds.Tables[0].Rows[0]["Igst_Amt"].ToString();
                txtCgstPerc.Text = ds.Tables[0].Rows[0]["Cgst_Perc"].ToString();
                txtCgstAmt.Text = ds.Tables[0].Rows[0]["Cgst_Amt"].ToString();
                txtSgstPerc.Text = ds.Tables[0].Rows[0]["Sgst_Perc"].ToString();
                txtSgstPerc.Text = ds.Tables[0].Rows[0]["Sgst_Perc"].ToString();
                txtSgstAmt.Text = ds.Tables[0].Rows[0]["Sgst_Amt"].ToString();
                btnSaveWOItem.Text = "Update";
                btnAddSubItem.Visible = true;
                ModalWOItem.Show();

                if (ds.Tables[0].Rows[0]["Sub_Item_Desc1"].ToString() != "")
                {
                    txtWorkDesc_Sub.Text = ds.Tables[0].Rows[0]["Sub_Item_Desc1"].ToString();
                    txtRate_Sub.Text = ds.Tables[0].Rows[0]["Sub_Item_Rate"].ToString();
                    txtWOQty_Sub.Text = ds.Tables[0].Rows[0]["Sub_Item_Qty"].ToString();
                    ddlUOM_Sub.SelectedValue = ds.Tables[0].Rows[0]["Sub_Item_UOM"].ToString();
                    btnSaveWOSubItem.Text = "Update";
                    ModalWOSubItem.Show();
                }
                else
                {
                    btnSaveWOSubItem.Text = "Save";
                    txtWorkDesc_Sub.Text = "";
                    txtRate_Sub.Text = "0.00";
                    txtWOQty_Sub.Text = "";
                    ddlUOM_Sub.SelectedIndex = -1;
                }
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
            objWO.Task = "DeleteWOItem";
            if (objWO.delete(con, WorkOrderBL.eLoadSp.DELETE_WO_ITEM_BY_ID))
            {
                BindWOItems();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('WO Item has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete WO Item !');", true);
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
        txtWorkType.Enabled = true;
        txtWorkDesc.Text = "";
        txtWOPerc.Text = "";
        txtRate.Text = "";
        txtAmount.Text = "";
        txtWOQty.Text = "";
        ddlUOM.SelectedIndex = -1;
        txtIgstPerc.Text = "0.00";
        txtIgstAmt.Text = "0.00";
        txtCgstPerc.Text = "0.00";
        txtCgstAmt.Text = "0.00";
        txtSgstPerc.Text = "0.00";
        txtSgstAmt.Text = "0.00";
    }
    protected void ClearSubItemPopup()
    {
        btnSaveWOSubItem.Text = "Save";
        txtWorkDesc_Sub.Text = "";
        txtRate_Sub.Text = "0.00";
        txtWOQty_Sub.Text = "";
        ddlUOM_Sub.SelectedIndex = -1;
        ClearItemPopup();
    }

    protected void btnAddSubItem_Click(object sender, EventArgs e)
    {
        ClearSubItemPopup();
        btnSaveWOSubItem.Text = "Save";
        ModalWOSubItem.Show();
    }

    protected void btnSaveWOSubItem_Click(object sender, EventArgs e)
    {
        try
        {
            objWO = new WorkOrderBL();
            objWO.WONo = txtWONo.Text;
            objWO.Item_Type = txtWorkType.Text;
            objWO.Item_Desc = txtWorkDesc.Text;
            objWO.WO_Item_Id = Convert.ToInt32(Session["WO_Item_Id"]);
            objWO.Sub_Item_Desc = txtWorkDesc_Sub.Text;
            if (txtRate_Sub.Text != string.Empty)
            {
                objWO.Item_Rate = Convert.ToDecimal(txtRate_Sub.Text.Trim());
            }
            if (ddlUOM_Sub.SelectedIndex > 0)
            {
                objWO.Item_UOM = Convert.ToInt32(ddlUOM_Sub.SelectedValue);
            }

            if (txtWOQty_Sub.Text != string.Empty)
            {
                objWO.Item_Qty = Convert.ToDecimal(txtWOQty_Sub.Text.Trim());
            }

            if (btnSaveWOSubItem.Text == "Save")
            {
                objWO.Task = "InsertWOSubItem";
                if (objWO.insertItem(con, WorkOrderBL.eLoadSp.INSERT_WO_ITEM))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Sub Item has been added successfully');", true);
                    //BindWOItems();
                    ClearSOWItemPopup();
                }
                else
                {
                    ModalSOWItem.Show();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Sub Item already exists for this Work Order !!!');", true);
                }
            }
            else
            {
                objWO.Task = "UpdateWOSubItem";
                if (objWO.insertItem(con, WorkOrderBL.eLoadSp.UPDATE_WO_ITEM))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Sub Item has been updated successfully');", true);
                    BindWOItems();
                    //BindSOWItems();
                    ClearItemPopup();
                    ClearSubItemPopup();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update Sub Item !!!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancelWOSubItem_Click(object sender, EventArgs e)
    {
        ClearSubItemPopup();
        btnAddSubItem.Visible = false;
    }

    protected void btnPrintPDF_Click(object sender, EventArgs e)
    {
        try
        {
            string MsgBody = "";
            string Url = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/" + "Procurement/WO_Print_PDF.aspx?WONo=" + Request.QueryString["WONo"].ToString();
            MsgBody = getHTMLFromURL(Url);
            //SavePDF_WO(Url);
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
    //public void SavePDF_WO(string MsgBody)
    //{
    //    try
    //    {
    //        HtmlToPdf pdf = new HtmlToPdf();
    //        FileNametosave = "WO_" + Request.QueryString["WONo"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + ".pdf";

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
    //            // ScriptManager.RegisterStartupScript(this, this.GetType(), "showSaveMessage", "swal('Success', 'PDF File Saved Succcessfully', 'success');", true);
    //        }
    //        else
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "showSaveMessage", "swal('Alert', 'Work Order Print cannot be created.', 'alert');", true);
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
            btnPrint.HRef = "WO_Print.aspx?WONo=" + Request.QueryString["WONo"].ToString() + "&Draft=1";
        }
        else
        {
            btnPrint.HRef = "WO_Print.aspx?WONo=" + Request.QueryString["WONo"].ToString();
        }
    }
    protected void lnkSOWItem_Click(object sender, EventArgs e)
    {
        try
        {
            objWO = new WorkOrderBL();
            ds = new DataSet();
            Session["WO_Item_Id"] = Convert.ToInt32(((LinkButton)sender).CommandName);
            objWO.Task = "SelectSOWItemDetails";
            if (objWO.load(con, WorkOrderBL.eLoadSp.SELECT_SOW_ITEM_BY_ITEM_ID, ref ds))
            {
                txtSOW.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
                txtSOWQuantity.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
                if (ds.Tables[0].Rows[0]["UOM"].ToString() != "0" && ds.Tables[0].Rows[0]["UOM"].ToString() != string.Empty)
                {
                    ddlUOM.SelectedValue = ds.Tables[0].Rows[0]["UOM"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Gst_Exist"].ToString() != "")
                {
                    chklTax.SelectedValue = Convert.ToBoolean(ds.Tables[0].Rows[0]["Gst_Exist"]) == true ? "Inclusive" : "Exclusive";
                }
                txtSOWRate.Text = ds.Tables[0].Rows[0]["txtSOWRate"].ToString();
                ModalSOWItem.Show();

                if (ds.Tables[0].Rows[0]["Sub_Item_Desc1"].ToString() != "")
                {
                    txtWorkDesc_Sub.Text = ds.Tables[0].Rows[0]["Sub_Item_Desc1"].ToString();
                    txtRate_Sub.Text = ds.Tables[0].Rows[0]["Sub_Item_Rate"].ToString();
                    txtWOQty_Sub.Text = ds.Tables[0].Rows[0]["Sub_Item_Qty"].ToString();
                    ddlUOM_Sub.SelectedValue = ds.Tables[0].Rows[0]["Sub_Item_UOM"].ToString();
                    btnSaveWOSubItem.Text = "Update";
                    ModalWOSubItem.Show();
                }
                else
                {
                    btnSaveWOSubItem.Text = "Save";
                    txtWorkDesc_Sub.Text = "";
                    txtRate_Sub.Text = "0.00";
                    txtWOQty_Sub.Text = "";
                    ddlUOM_Sub.SelectedIndex = -1;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancelSOWItem_Click(object sender, EventArgs e)
    {
        ClearSOWItemPopup();

    }
    private void BindSOWItems()
    {
        try
        {
            objWO = new WorkOrderBL();
            ds = new DataSet();
            objWO.WO_ID = Convert.ToInt32(ViewState["WOID"]);
            objWO.Task = "SelectSOWItems";
            objWO.load(con, WorkOrderBL.eLoadSp.SELECT_SOW_ITEM_BY_ITEM_ID, ref ds);
            Grid_SOWItem.DataSource = ds;
            Grid_SOWItem.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void Grid_SOWItem_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objWO = new WorkOrderBL();
            objWO.SOW_Item_Id = Convert.ToInt32(e.Record["SOW_Item_Id"]);
            objWO.Task = "DeleteSOWItem";
            if (objWO.delete(con, WorkOrderBL.eLoadSp.DELETE_SOW_ITEM_BY_ID))
            {
                BindSOWItems();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Scope Of Work Item has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete Scope Of Work  Item !');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void ClearSOWItemPopup()
    {
        txtSOW.Text = "";
        txtSOWQuantity.Text = "";
        ddlUOMSOW.SelectedIndex = -1;
        txtSOWRate.Text = "";
    }
    protected void btnAddSOW_Click(object sender, EventArgs e)
    {
        ClearSOWItemPopup();
        ModalSOWItem.Show();
    }
    protected void btnSaveSOWItem_Click(object sender, EventArgs e)
    {
        try
        {
            objWO = new WorkOrderBL();
            objWO.WONo = txtWONo.Text;
            if (Session["SOW_Item_Id"] != null)
            {
                objWO.SOW_Item_Id = Convert.ToInt32(Session["SOW_Item_Id"]);
            }
            if (txtSOW.Text != string.Empty)
            {
                objWO.Scope_Of_Work = txtSOW.Text.Trim();
            }
            if (txtSOWQuantity.Text != string.Empty)
            {
                objWO.Quantity = Convert.ToDecimal(txtSOWQuantity.Text.Trim());
            }
            if (ddlUOMSOW.SelectedIndex > 0)
            {
                objWO.UMO = Convert.ToInt32(ddlUOMSOW.SelectedValue);
            }
            if (txtSOWRate.Text != string.Empty)
            {
                objWO.Rate = Convert.ToDecimal(txtSOWRate.Text.Trim());
            }


            if (btnSaveSOWItem.Text == "Save")
            {
                objWO.Task = "InsertSOWItem";
                if (objWO.insertSOWItem(con, WorkOrderBL.eLoadSp.INSERT_SOW_ITEM))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Scope Of Work Item has been added successfully');", true);
                    ClearSOWItemPopup();
                    BindSOWItems();
                }
                else
                {
                    ModalSOWItem.Show();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Item already exists for this Work Order !!!');", true);
                }

            }
            else
            {
                //objWO.Task = "UpdateSOWItem";
                //if (objWO.insertItem(con, WorkOrderBL.eLoadSp.UPDATE_SOW_ITEM))
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Scope Of Work Item has been updated successfully');", true);
                //    ClearSOWItemPopup();
                //    BindSOWItems();
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update Scope Of Work Item !!!');", true);
                //}
                //btnAddSubItem.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void lnkDownloadFile_Click(object sender, EventArgs e)
    {
        try
        {
            string filePath = Server.MapPath("~\\UploadedFiles\\" + (sender as LinkButton).Text.Replace("/", "-"));
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.Flush();
            Response.TransmitFile(filePath);
            //Response.WriteFile(filePath);
            Response.End();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }

}