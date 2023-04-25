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


public partial class ProffessionalServiceOrder : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    DataSet ds = null;
    SubContractorBL objSubContractorBL = null;
    IndentBL objIndent = null;
    //WorkOrderBL objWO = null;
    //WorkOrderBL objPSO = null;
    ProfessionalServiceOrderBL objPSO = null;
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
                    BindPSOType();
                    BindUsersNames();
                    BindUOM();
                    BindProject();

                    if (Request.QueryString["PSONo"] != null)
                    {
                        GetPSODetails(Request.QueryString["PSONo"].ToString());
                        btnPrint.Visible = true;
                        btnPrint.Target = "_blank";
                        btnPrint.HRef = "PSOPrint.aspx?PSONo=" + Request.QueryString["PSONo"].ToString();
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

    protected void BindPSOType()
    {
        try
        {
            ds = new DataSet();
            objPSO = new ProfessionalServiceOrderBL();
            objPSO.load(con, ProfessionalServiceOrderBL.eLoadSp.SELECT_PSO_TYPE_ALL, ref ds);
            ddlPSOType.DataSource = ds;
            ddlPSOType.DataTextField = "Type_Name";
            ddlPSOType.DataValueField = "Type_ID";
            ddlPSOType.DataBind();
            ddlPSOType.Items.Insert(0, "-Select-");
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

            //ddlUOMSOW.DataSource = ds;
            //ddlUOMSOW.DataTextField = "UOM";
            //ddlUOMSOW.DataValueField = "UOM_ID";
            //ddlUOMSOW.DataBind();
            //ddlUOMSOW.Items.Insert(0, "-Select-");

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


    private void GetPSODetails(string PSONo)
    {
        try
        {
            objPSO = new ProfessionalServiceOrderBL();
            ds = new DataSet();
            objPSO.PSONo = PSONo;
            objPSO.Task = "GetPSODetails";
            objPSO.load(con, ProfessionalServiceOrderBL.eLoadSp.SELECT_PSODETAILS_BY_PSONO, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtPSONo.Text = ds.Tables[0].Rows[0]["PSONo"].ToString();
                txtPSODate.Text = ds.Tables[0].Rows[0]["PSODate"].ToString();
                ddlProject.SelectedValue = ds.Tables[0].Rows[0]["Project_Code"].ToString();
                ddlPSOType.SelectedValue = ds.Tables[0].Rows[0]["PSO_Type"].ToString();
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
                ViewState["PSO_ID"] = ds.Tables[0].Rows[0]["PSO_ID"].ToString();
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
                BindPSOItems();
                BindPSOSWItems();
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
            objPSO = new ProfessionalServiceOrderBL();
            objPSO.UserID = Convert.ToInt32(Session["UID"].ToString());
            objPSO.PSODate = Convert.ToDateTime(txtPSODate.Text.Trim());
            objPSO.ProjectCode = ddlProject.SelectedValue.Trim();
            objPSO.FYear = ddlFYear.SelectedValue.Trim();
            objPSO.Month = ddlMonth.SelectedValue.Trim();
            objPSO.SubContractorID = ddlSubContractor.SelectedValue.Trim();
            objPSO.PSOTypeID = Convert.ToInt32(ddlPSOType.SelectedValue);
            objPSO.WorkLocation = txtWorkLocation.Text.Trim();
            objPSO.DurationOfWork = txtDurationOfWork.Text.Trim();
            objPSO.ContactName = txtContactName.Text.Trim();
            objPSO.ContactNo = txtContactNo.Text.Trim();
            objPSO.PaymentTerms = txtPayTerms.Text.Trim();
            objPSO.ApprovedBy = Convert.ToInt32(ddlApprovedBy.SelectedValue.Trim());
            objPSO.Status = rblStatus.SelectedValue.Trim();
            objPSO.GST_No = txtGstNo.Text.Trim();
            objPSO.Remarks = txtRemarks.Text.Trim();
            objPSO.Others = txtOthers.Text.Trim();
            objPSO.TDSPerc = Convert.ToDecimal(ddlTDSPerc.SelectedValue);
            if (rblOrderType.SelectedValue != "")
            {
                objPSO.OrderType = rblOrderType.SelectedValue.Trim();
            }

            objPSO.Task = "InsertPSO";

            if (btnSubmit.Text == "Submit")
            {
                if (rblStatus.SelectedValue != "Open")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select Open status for Professinal Service order submission!.');", true);
                    return;
                }
                else
                {
                    if (objPSO.insert(con, ProfessionalServiceOrderBL.eLoadSp.INSERT))
                    {
                        ViewState["PSONo"] = objPSO.PSONo.ToString();
                        ViewState["PSO_ID"] = Convert.ToInt32(objPSO.PSO_ID);
                        btnPrint.Visible = true;
                        btnPrint.HRef = "PSOPrint.aspx?PSONo=" + ViewState["PSONo"].ToString();
                        btnPrint.Target = "_blank";
                        div_Draft.Visible = true;
                        div_BeforeUpload.Visible = true;
                        btnPrintPDF.Visible = false;
                        btnAddItem.Visible = true;
                        btnAddSOW.Visible = true;
                        btnSubmit.Text = "Update";
                        txtPSONo.Enabled = false;
                        ddlSubContractor.Enabled = false;
                        txtPSONo.Text = objPSO.PSONo.ToString();
                        GetPSODetails(ViewState["PSONo"].ToString());
                        //Response.Redirect("WorkOrder.aspx?WONo=" + ViewState["WONo"].ToString() + "&s=1", false);
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('PSO details has been inserted sucessfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to insert PSO details !.');", true);
                    }
                }
            }
            else
            {
                objPSO.Task = "UpdatePSO";
                objPSO.PSONo = txtPSONo.Text.ToString();
                if (fuWODoc.HasFile)
                {
                    objPSO.Uploaded_File = "PSO_" + txtPSONo.Text.ToString() + ".pdf";
                }

                if (objPSO.update(con, ProfessionalServiceOrderBL.eLoadSp.UPDATE))
                {
                    if (fuWODoc.HasFile)
                    {
                        fuWODoc.SaveAs(Server.MapPath("~\\UploadedFiles\\PSO_" + txtPSONo.Text.Replace("/", "-") + ".pdf"));
                        lnkDownloadFile.Text = "PSO_" + txtPSONo.Text + ".pdf";
                        div_AfterUpload.Visible = true;
                    }
                    btnPrint.Visible = true;
                    btnPrint.HRef = "PSOPrint.aspx?PSONo=" + txtPSONo.Text.ToString();
                    btnPrint.Target = "_blank";
                    div_Draft.Visible = true;
                    div_BeforeUpload.Visible = true;
                    btnPrintPDF.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('PSO details has been updated sucessfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update PSO details !.');", true);
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
            Response.Redirect("../Procurement/ProffessionalServiceOrderList.aspx", false);
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void BindPSOItems()
    {
        try
        {
            objPSO = new ProfessionalServiceOrderBL();
            ds = new DataSet();
            if (Request.QueryString["PSONo"] != "")
            {
                objPSO.PSO_ID = Convert.ToString(Request.QueryString["PSONo"]);
            }
            else
            {
                objPSO.PSO_ID = Convert.ToString(ViewState["PSONo"].ToString());
            }
            objPSO.Task = "SelectPSOItems";
            objPSO.load(con, ProfessionalServiceOrderBL.eLoadSp.SELECT_PSO_ITEMS_BY_PSONO, ref ds);
            Grid_PSOItem.DataSource = ds;
            Grid_PSOItem.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    
    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        ClearItemPopup();
        btnSavePSOItem.Text = "Save";
        ModalWOItem.Show();
    }
    
    protected void btnSavePSOItem_Click(object sender, EventArgs e)
    {
        try
        {
            objPSO = new ProfessionalServiceOrderBL();
            objPSO.PSONo = (txtPSONo.Text);
            objPSO.Item_Type = txtWorkType.Text;
            objPSO.Item_Desc = txtWorkDesc.Text;
            if (Session["PSO_Item_Id"] != null)
            {
                objPSO.PSO_Item_Id = Convert.ToInt32(Session["PSO_Item_Id"]);
            }
            if (txtPSOPerc.Text != string.Empty)
            {
                objPSO.Item_Perc = Convert.ToDecimal(txtPSOPerc.Text.Trim());
            }
            if (txtRate.Text != string.Empty)
            {
                objPSO.Item_Rate = Convert.ToDecimal(txtRate.Text.Trim());
            }
            if (ddlUOM.SelectedIndex > 0)
            {
                objPSO.Item_UOM = Convert.ToInt32(ddlUOM.SelectedValue);
            }
            if (txtPSOQty.Text != string.Empty)
            {
                objPSO.Item_Qty = Convert.ToDecimal(txtPSOQty.Text.Trim());
            }
            if (txtAmount.Text != string.Empty)
            {
                objPSO.Item_Amt = Convert.ToDecimal(txtAmount.Text.Trim());
            }
            objPSO.Gst_Exist = chklTax.SelectedValue == "Inclusive" ? true : false;
            objPSO.Igst_Perc = Convert.ToDecimal(txtIgstPerc.Text.Trim());
            objPSO.Cgst_Perc = Convert.ToDecimal(txtCgstPerc.Text.Trim());
            objPSO.Sgst_Perc = Convert.ToDecimal(txtSgstPerc.Text.Trim());

            if (btnSavePSOItem.Text == "Save")
            {
                objPSO.Task = "InsertPSOItem";
                if (objPSO.insertItem(con, ProfessionalServiceOrderBL.eLoadSp.INSERT_PSO_ITEM))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Item has been added successfully');", true);
                    ClearItemPopup();
                    BindPSOItems();
                }
                else
                {
                    ModalWOItem.Show();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Item already exists for this Professional Service Order !!!');", true);
                }

            }
            else
            {
                objPSO.Task = "UpdatePSOItem";
                if (objPSO.insertItem(con, ProfessionalServiceOrderBL.eLoadSp.UPDATE_PSO_ITEM))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Item has been updated successfully');", true);
                    ClearItemPopup();
                    BindPSOItems();
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

    protected void btnCancelPSOItem_Click(object sender, EventArgs e)
    {
        ClearItemPopup();
        btnAddSubItem.Visible = false;
    }

    protected void lnkPSOItem_Click(object sender, EventArgs e)
    {
        try
        {
            objPSO = new ProfessionalServiceOrderBL();
            ds = new DataSet();
            objPSO.PSO_Item_Id = Convert.ToInt32(((LinkButton)sender).CommandName);
            Session["PSO_Item_Id"] = Convert.ToInt32(((LinkButton)sender).CommandName);
            objPSO.Task = "SelectPSOItemDetails";
            if (objPSO.load(con, ProfessionalServiceOrderBL.eLoadSp.SELECT_PSO_ITEM_BY_ITEM_ID, ref ds))
            {
                txtWorkType.Text = ds.Tables[0].Rows[0]["Item_Type"].ToString();
                txtWorkType.Enabled = false;
                txtWorkDesc.Text = ds.Tables[0].Rows[0]["Item_Desc"].ToString();
                txtPSOPerc.Text = ds.Tables[0].Rows[0]["Item_Perc"].ToString();
                txtPSOQty.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
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
                txtSgstAmt.Text = ds.Tables[0].Rows[0]["Sgst_Amt"].ToString();
                btnSavePSOItem.Text = "Update";
                btnAddSubItem.Visible = true;
                ModalWOItem.Show();

                if (ds.Tables[0].Rows[0]["Sub_Item_Desc1"].ToString() != "")
                {
                    txtWorkDesc_Sub.Text = ds.Tables[0].Rows[0]["Sub_Item_Desc1"].ToString();
                    txtRate_Sub.Text = ds.Tables[0].Rows[0]["Sub_Item_Rate"].ToString();
                    txtWOQty_Sub.Text = ds.Tables[0].Rows[0]["Sub_Item_Qty"].ToString();
                    ddlUOM_Sub.SelectedValue = ds.Tables[0].Rows[0]["Sub_Item_UOM"].ToString();
                    btnSavePSOSubItem.Text = "Update";
                    ModalWOSubItem.Show();
                }
                else
                {
                    btnSavePSOSubItem.Text = "Save";
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

    protected void Grid_PSOItem_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objPSO = new ProfessionalServiceOrderBL();
            objPSO.PSO_Item_Id = Convert.ToInt32(e.Record["PSO_Item_Id"]);
            objPSO.Task = "DeletePSOItem";
            if (objPSO.delete(con, ProfessionalServiceOrderBL.eLoadSp.DELETE_PSO_ITEM_BY_ID))
            {
                BindPSOItems();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('PSO Item has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete PSO Item !');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    
    protected void ClearItemPopup()
    {
        btnSavePSOItem.Text = "Save";
        txtWorkType.Text = "";
        txtWorkType.Enabled = true;
        txtWorkDesc.Text = "";
        txtPSOPerc.Text = "";
        txtRate.Text = "";
        txtAmount.Text = "";
        txtPSOQty.Text = "";
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
        btnSavePSOSubItem.Text = "Save";
        txtWorkDesc_Sub.Text = "";
        txtRate_Sub.Text = "0.00";
        txtWOQty_Sub.Text = "";
        ddlUOM_Sub.SelectedIndex = -1;
        ClearItemPopup();
    }

    protected void btnAddSubItem_Click(object sender, EventArgs e)
    {
        ClearSubItemPopup();
        btnSavePSOSubItem.Text = "Save";
        ModalWOSubItem.Show();
    }

    protected void btnSavePSOSubItem_Click(object sender, EventArgs e)
    {
        try
        {
            objPSO = new ProfessionalServiceOrderBL();
            objPSO.PSONo = txtPSONo.Text;
            objPSO.Item_Type = txtWorkType.Text;
            objPSO.Item_Desc = txtWorkDesc.Text;
            objPSO.PSO_Item_Id = Convert.ToInt32(Session["PSO_Item_Id"]);
            objPSO.Sub_Item_Desc = txtWorkDesc_Sub.Text;
            if (txtRate_Sub.Text != string.Empty)
            {
                objPSO.Item_Rate = Convert.ToDecimal(txtRate_Sub.Text.Trim());
            }
            if (ddlUOM_Sub.SelectedIndex > 0)
            {
                objPSO.Item_UOM = Convert.ToInt32(ddlUOM_Sub.SelectedValue);
            }

            if (txtWOQty_Sub.Text != string.Empty)
            {
                objPSO.Item_Qty = Convert.ToDecimal(txtWOQty_Sub.Text.Trim());
            }

            if (btnSavePSOSubItem.Text == "Save")
            {
                objPSO.Task = "InsertPSOSubItem";
                if (objPSO.insertItem(con, ProfessionalServiceOrderBL.eLoadSp.INSERT_PSO_ITEM))
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
                objPSO.Task = "UpdatePSOSubItem";
                if (objPSO.insertItem(con, ProfessionalServiceOrderBL.eLoadSp.PSO_INSERT_UPDATE))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Sub Item has been updated successfully');", true);
                    BindPSOItems();
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
            string Url = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/" + "Procurement/PSOPrint_PDF.aspx?PSONo=" + Request.QueryString["PSONo"].ToString();
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
            btnPrint.HRef = "PSOPrint.aspx?PSONo=" + Request.QueryString["PSONo"].ToString() + "&Draft=1";
        }
        else
        {
            btnPrint.HRef = "PSOPrint.aspx?PSONo=" + Request.QueryString["PSONo"].ToString();
        }
    }
    protected void lnkSOWItem_Click(object sender, EventArgs e)
    {
        try
        {
            objPSO = new ProfessionalServiceOrderBL();
            ds = new DataSet();
            Session["PSO_Item_Id"] = Convert.ToInt32(((LinkButton)sender).CommandName);
            objPSO.Task = "SelectSOWItemDetails";
            if (objPSO.load(con, ProfessionalServiceOrderBL.eLoadSp.SELECT_SOW_ITEM_BY_ITEM_ID, ref ds))
            {
                txtSOW.Text = ds.Tables[0].Rows[0]["Scope_Of_Work"].ToString();
                TxtTentavieTime.Text = ds.Tables[0].Rows[0]["Tentavie_Time"].ToString();


                TxtAmountRate.Text = ds.Tables[0].Rows[0]["Amount"].ToString();
                ModalSOWItem.Show();

                if (ds.Tables[0].Rows[0]["Sub_Item_Desc1"].ToString() != "")
                {
                    txtWorkDesc_Sub.Text = ds.Tables[0].Rows[0]["Sub_Item_Desc1"].ToString();
                    txtRate_Sub.Text = ds.Tables[0].Rows[0]["Sub_Item_Rate"].ToString();
                    txtWOQty_Sub.Text = ds.Tables[0].Rows[0]["Sub_Item_Qty"].ToString();
                    ddlUOM_Sub.SelectedValue = ds.Tables[0].Rows[0]["Sub_Item_UOM"].ToString();
                    btnSavePSOSubItem.Text = "Update";
                    ModalWOSubItem.Show();
                }
                else
                {
                    btnSavePSOSubItem.Text = "Save";
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
    private void BindPSOSWItems()
    {
        try
        {
            objPSO = new ProfessionalServiceOrderBL();
            ds = new DataSet();
            objPSO.PSO_ID = Convert.ToString(ViewState["PSO_ID"]);
            objPSO.Task = "SelectSOWItemsPSO";
            objPSO.load(con, ProfessionalServiceOrderBL.eLoadSp.SELECT_SOW_ITEM_BY_ITEM_ID_PSO, ref ds);
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
            objPSO = new ProfessionalServiceOrderBL();


            objPSO.SOW_Item_Id = Convert.ToInt32(e.Record["SOW_Item_Id"]);
            objPSO.Task = "DeleteSOWItemPSO";
            if (objPSO.delete(con, ProfessionalServiceOrderBL.eLoadSp.DELETE_SOW_ITEM_BY_ID))
            {
                BindPSOSWItems();
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
        txtPSOQty.Text = "";
        //ddlUOMSOW.SelectedIndex = -1;
        TxtAmountRate.Text = "";
        TextStaff.Text = "";
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
            objPSO = new ProfessionalServiceOrderBL();
            objPSO.PSONo = txtPSONo.Text;
            if (Session["SOW_Item_Id"] != null)
            {
                objPSO.SOW_Item_Id = Convert.ToInt32(Session["SOW_Item_Id"]);
            }
            if (txtSOW.Text != string.Empty)
            {
                objPSO.Scope_Of_Work = txtSOW.Text.Trim();
            }
            //objPSO.Tentavie_Time = Convert.ToDateTime(TxtTentavieTime.Text.Trim());

            objPSO.Tentavie_Time = Convert.ToDateTime(TxtTentavieTime.Text.Trim());
            if (TextStaff.Text != string.Empty)
            {
                objPSO.No_of_Staff_to_be_Deputed = Convert.ToDecimal(TextStaff.Text.Trim());
            }
            if (TxtAmountRate.Text != string.Empty)
            {
                objPSO.Amount = Convert.ToDecimal(TxtAmountRate.Text.Trim());
            }


            if (btnSaveSOWItem.Text == "Save")
            {
                objPSO.Task = "InsertSOWItemPSO";
                if (objPSO.insertSOWItem(con, ProfessionalServiceOrderBL.eLoadSp.INSERT_SOW_ITEM_PSO))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Scope Of Work Item has been added successfully');", true);
                    ClearSOWItemPopup();
                    BindPSOSWItems();
                }
                else
                {
                    ModalSOWItem.Show();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Item already exists for this Professional Service Order !!!');", true);
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


