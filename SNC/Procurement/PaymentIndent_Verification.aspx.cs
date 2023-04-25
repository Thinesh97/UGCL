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
using System.IO;
using System.Net;


public partial class PaymentIndent_Verification : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    VendorBL objVendorBL = null;
    SubContractorBL objSubContractorBL = null;
    OtherBL objOtherBL = null;
    ProjectBL objProjectBL = null;
    PaymentIndentBL objPaymentIndentBL = null;
    IndentBL objIndent = null;
    DataSet ds = null;
    decimal GST_Amount = 0;
    decimal Amt_ServiceMaterial = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    BindProjectList();
                    BindApprover();
                    BindVerifier();
                    BindVendor();
                    BindSubContracotr();
                    BindOther();
                    BindLedger();
                    rblBenType_SelectedIndexChanged(null, null);
                    BindState();
                    BindPaymentCategory();
                    if (Request.QueryString["PayInd_No"] != null)
                    {
                        ViewState["PayInd_No"] = Request.QueryString["PayInd_No"].ToString();
                        GetPaymentIndentDetails();
                        BindFileGrid();
                        BindItemGrid();
                        GetCompletedPayInd();
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
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindPaymentCategory()
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "GetPaymentCategory";
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_ALL_PAYMENTCATEGORY, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPaymentCategory.DataSource = null;
                ddlPaymentCategory.DataBind();
                ddlPaymentCategory.DataSource = ds.Tables[0];
                ddlPaymentCategory.DataValueField = "PaymentCategory_ID";
                ddlPaymentCategory.DataTextField = "PaymentCategory_Name";
                ddlPaymentCategory.DataBind();
                ddlPaymentCategory.Items.Insert(0, "-Select-");

                //Grid_PaymentCategory.DataSource = ds;
                //Grid_PaymentCategory.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }
    private void GetPaymentIndentDetails()
    {
        try
        {
            objPaymentIndentBL = new PaymentIndentBL();
            DataSet dsDetails = new DataSet();
            objPaymentIndentBL.Task = "GetPaymentIndentDetails";
            objPaymentIndentBL.PayInd_No = Request.QueryString["PayInd_No"].ToString();
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_PAYMENTINDENTDETAILS, ref dsDetails);
            if (dsDetails.Tables[0].Rows.Count > 0)
            {

                if (Session["UID"].ToString() == dsDetails.Tables[0].Rows[0]["Verifier"].ToString())
                {
                    ddlPaymentMode.Enabled = true;
                    ddlLedger.Enabled = true;
                    txtWorkDoneFor.Enabled=true;
                    ddlPaymentCategory.Enabled = true;
                }
                txtPayIndNo.Text = dsDetails.Tables[0].Rows[0]["PayInd_No"].ToString();
                txtPayIndDate.Text = Convert.ToDateTime(dsDetails.Tables[0].Rows[0]["PayInd_Date"]).ToString("dd-MM-yyyy");
                ddlFYear.SelectedValue = dsDetails.Tables[0].Rows[0]["FYear"].ToString();
                ddlProject.SelectedValue = dsDetails.Tables[0].Rows[0]["Project_Code"].ToString();
                txtTDSDeductedAmt.Text= dsDetails.Tables[0].Rows[0]["TDS_Amt"].ToString();
                txtTotalPayable.Text = dsDetails.Tables[0].Rows[0]["Total_Payable"].ToString();
                ddlTDSPercent.SelectedItem.Text= dsDetails.Tables[0].Rows[0]["TDS_Perc"].ToString();
                ddlProject_SelectedIndexChanged(null, null);
                ddlApprover.SelectedValue= dsDetails.Tables[0].Rows[0]["Approver"].ToString();
                ddlVerifier.SelectedValue = dsDetails.Tables[0].Rows[0]["Verifier"].ToString();
                rblBenType.SelectedValue = dsDetails.Tables[0].Rows[0]["Beneficiary_Type"].ToString();
                //txtSubContractorName.Text = dsDetails.Tables[0].Rows[0]["Payment_To_SubContractorName"].ToString();
                txtGSTPercent.Text = dsDetails.Tables[0].Rows[0]["GST_Percent"].ToString();
                txtGSTAmount.Text = dsDetails.Tables[0].Rows[0]["GST_Amount"].ToString();
                ddlPaymentCategory.SelectedItem.Text = dsDetails.Tables[0].Rows[0]["Payment_Category"].ToString();
                if (dsDetails.Tables[0].Rows[0]["Payment_To_SubContractorName"].ToString() !="")
                {
                    BindSubContracotr_Sub();
                    ddlPayment_to_SubContractorName.SelectedItem.Text = dsDetails.Tables[0].Rows[0]["Payment_To_SubContractorName"].ToString();
                    
                }
                if (dsDetails.Tables[0].Rows[0]["TDS_Perc"] != "")
                {
                     GST_Amount =Convert.ToDecimal(dsDetails.Tables[0].Rows[0]["GST_Amount"].ToString());
                    
                }
                txtOtherDeductions.Text = dsDetails.Tables[0].Rows[0]["Other_Deductions"].ToString();
                rblBenType_SelectedIndexChanged(null, null);
                if (rblBenType.SelectedValue == "Vendor")
                {
                    ddlVendor.SelectedValue = dsDetails.Tables[0].Rows[0]["Vendor_SubCon_ID"].ToString();
                    ddlVendor_SelectedIndexChanged(null, null);
                }
                else if (rblBenType.SelectedValue == "SubContractor")
                {
                    ddlSubContractor.SelectedValue = dsDetails.Tables[0].Rows[0]["Vendor_SubCon_ID"].ToString();
                    ddlSubContractor_SelectedIndexChanged(null, null);
                }
                else
                {
                    ddlOther.SelectedValue = dsDetails.Tables[0].Rows[0]["Vendor_SubCon_ID"].ToString();
                    ddlOther_SelectedIndexChanged(null, null);
                }

                ddlPOWO.SelectedValue = dsDetails.Tables[0].Rows[0]["POWO_ID"].ToString();
                ddlPOWO_SelectedIndexChanged(null, null);

                ddlLedger.SelectedItem.Text = dsDetails.Tables[0].Rows[0]["Ledger"].ToString();
                txtWorkDoneFor.Text = dsDetails.Tables[0].Rows[0]["WorkDoneFor"].ToString();

                txtBank.Text = dsDetails.Tables[0].Rows[0]["Bank_Name"].ToString();
                txtBranch.Text = dsDetails.Tables[0].Rows[0]["Bank_Branch"].ToString();
                txtAccNo.Text = dsDetails.Tables[0].Rows[0]["Bank_Account"].ToString();
                txtIFSC.Text = dsDetails.Tables[0].Rows[0]["Bank_IFSC"].ToString();
                txtInvoiceDate.Text = dsDetails.Tables[0].Rows[0]["Invoice_Date"].ToString();
                txtNoOfDueDate.Text = dsDetails.Tables[0].Rows[0]["No_Of_Due_Date"].ToString();
                ddlPaymentMode.SelectedValue = dsDetails.Tables[0].Rows[0]["Payment_Mode"].ToString();
                ddlPaymentType.SelectedValue = dsDetails.Tables[0].Rows[0]["Payment_Type"].ToString();
                txtAmt_ServiceMaterial.Text = dsDetails.Tables[0].Rows[0]["Amt_ServiceMaterial"].ToString();
                txtAmt_EarlierPayment.Text = dsDetails.Tables[0].Rows[0]["Amt_EarlierPayment"].ToString();
                if (dsDetails.Tables[0].Rows[0]["Amt_ServiceMaterial"] !="")
                {
                    Amt_ServiceMaterial =Convert.ToDecimal(dsDetails.Tables[0].Rows[0]["Amt_ServiceMaterial"].ToString());
                    txtBasicTotal.Text = (Amt_ServiceMaterial - GST_Amount).ToString();
                }
                txtAmt_PartPayment.Text = dsDetails.Tables[0].Rows[0]["Amt_PartPayment"].ToString();
                btnApproval.Visible = Session["UID"].ToString() == dsDetails.Tables[0].Rows[0]["Verifier"].ToString() ? true : false;
                btnModification.Visible = Session["UID"].ToString() == dsDetails.Tables[0].Rows[0]["Verifier"].ToString() ? true : false;
                btnReject.Visible = Session["UID"].ToString() == dsDetails.Tables[0].Rows[0]["Verifier"].ToString() ? true : false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindApprover()
    {
        try
        {
            IndentBL objIndent = new IndentBL();
            ds = new DataSet();
            objIndent.load(con, IndentBL.eLoadSp.SELECT_USERNAMES_ALL, ref ds);
            ddlApprover.DataSource = ds;
            ddlApprover.DataTextField = "Name";
            ddlApprover.DataValueField = "UID";
            ddlApprover.DataBind();
            ddlApprover.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindVerifier()
    {
        try
        {
            IndentBL objIndent = new IndentBL();
            ds = new DataSet();
            objIndent.load(con, IndentBL.eLoadSp.SELECT_USERNAMES_ALL, ref ds);
            ddlVerifier.DataSource = ds;
            ddlVerifier.DataTextField = "Name";
            ddlVerifier.DataValueField = "UID";
            ddlVerifier.DataBind();
            ddlVerifier.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindState()
    {
        try
        {
            ds = new DataSet();
            objVendorBL = new VendorBL();
            objVendorBL.load(con, VendorBL.eLoadSp.SELECT_ALL_State, ref ds);
            ddlState.DataSource = ds;
            ddlState.DataValueField = "State_Code";
            ddlState.DataTextField = "State";
            ddlState.DataBind();
            ddlState.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindVendor()
    {
        try
        {
            ds = new DataSet();
            objVendorBL = new VendorBL();
            objVendorBL.load(con, VendorBL.eLoadSp.SELECT_ALL_VENDOR_NAMES, ref ds);
            ddlVendor.DataSource = ds;
            ddlVendor.DataTextField = "Vendor_name";
            ddlVendor.DataValueField = "Vendor_ID";
            ddlVendor.DataBind();
            ddlVendor.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindSubContracotr()
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

    protected void BindOther()
    {
        try
        {
            ds = new DataSet();
            objOtherBL = new OtherBL();
            objOtherBL.load(con, OtherBL.eLoadSp.SELECT_ALL_OTHER_NAMES, ref ds);
            ddlOther.DataSource = ds;
            ddlOther.DataTextField = "Other_name";
            ddlOther.DataValueField = "Other_ID";
            ddlOther.DataBind();
            ddlOther.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindProjectList()
    {
        try
        {
            objProjectBL = new ProjectBL();
            ds = new DataSet();
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_ALL, ref ds);
            ddlProject.DataTextField = "Project_Name";
            ddlProject.DataValueField = "Project_Code";
            ddlProject.DataSource = ds;
            ddlProject.DataBind();
            ddlProject.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }


    protected void CalculateDate(object sender, EventArgs e)
    {
        if (txtPayIndDate.Text.ToString() !="" && txtInvoiceDate.Text.ToString() != "")
        {
            DateTime firstDate = Convert.ToDateTime(txtInvoiceDate.Text.ToString()); 
            DateTime secondDate = Convert.ToDateTime(txtPayIndDate.Text.ToString()); 
            System.TimeSpan diff = firstDate.Subtract(secondDate);
            //System.TimeSpan diff1 = (secondDate - firstDate).TotalDays.ToString();

            String diff2 = (secondDate- firstDate).TotalDays.ToString().Trim();
            txtNoOfDueDate.Text = diff2;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnApproval_Click(object sender, EventArgs e)
    {
        try
        {
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.PayInd_No = txtPayIndNo.Text.ToString();
            objPaymentIndentBL.Approver_ID = Convert.ToInt32(ddlApprover.SelectedValue.ToString());
            objPaymentIndentBL.Remarks = txtRemarks.Text.Trim();
            objPaymentIndentBL.Task = "SendForApproval";
            if (chkPriorityStaus.SelectedItem == null)
            {
                chkPriorityStaus.SelectedValue = null;
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select any of the Priority status !.');", true);
            }
            else if (chkPriorityStaus.Items.Cast<ListItem>().Count(i => i.Selected) > 1)
            {
                // code to handle more than one item selected
                chkPriorityStaus.SelectedValue = null;
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select any one of the Priority status !.');", true);
            }



            else if (chkPriorityStaus.Items.Count > 1 && !chkPriorityStaus.Items.Cast<ListItem>().Any(i => i.Selected))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select any of the Priority status !.');", true);
                chkPriorityStaus.SelectedValue = null;
            }
            else
            {
                 if (txtTDSDeductedAmt.Text != "")
                {
                    objPaymentIndentBL.TDS_Amt = Convert.ToDecimal(txtTDSDeductedAmt.Text.ToString());
                }
                if (ddlTDSPercent.Text != "-Select-")
                {
                    objPaymentIndentBL.TDS_Perc = Convert.ToDecimal(ddlTDSPercent.SelectedValue);
                }
                if (txtAmt_PartPayment.Text != "")
                {
                    objPaymentIndentBL.Amt_PartPayment = Convert.ToDecimal(txtAmt_PartPayment.Text.ToString());
                }

                if (ddlPaymentMode.SelectedItem.Text != "-Select-")
                {
                    objPaymentIndentBL.Payment_Mode = ddlPaymentMode.SelectedItem.Text;
                }
                if (ddlLedger.SelectedItem.Text != "-Select-")
                {
                    objPaymentIndentBL.Ledger_Name = ddlLedger.SelectedItem.Text;
                }
                if (txtWorkDoneFor.Text != "")
                {
                    objPaymentIndentBL.WorkDoneFor = txtWorkDoneFor.Text.Trim();
                }
                if (ddlPaymentCategory.SelectedItem.Text != "-Select-")
                {
                    objPaymentIndentBL.Payment_Category = ddlPaymentCategory.SelectedItem.Text;
                }
                if (objPaymentIndentBL.update(con, PaymentIndentBL.eLoadSp.UPDATE))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Indent sent for approval.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to send Payment Indent for approval !.');", true);
                }
                
            }

           
           
            
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnModification_Click(object sender, EventArgs e)
    {
        try
        {
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.PayInd_No = txtPayIndNo.Text.ToString();
            objPaymentIndentBL.Remarks = txtRemarks.Text.Trim();
            objPaymentIndentBL.Task = "SendForModification";
            if (objPaymentIndentBL.update(con, PaymentIndentBL.eLoadSp.UPDATE))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Indent sent for Modification.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to send Payment Indent for Modification !.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.PayInd_No = txtPayIndNo.Text.ToString();
            objPaymentIndentBL.Remarks = txtRemarks.Text.Trim();
            objPaymentIndentBL.Task = "SendForReject";
            if (objPaymentIndentBL.update(con, PaymentIndentBL.eLoadSp.UPDATE))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Indent Rejected successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to Reject Payment Indent !.');", true);
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
            Response.Redirect("../Procurement/PaymentIndentList_Verification.aspx", false);
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("PaymentIndent_Print.aspx?PayInd_No=" + ViewState["PayInd_No"].ToString(), false);
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    

    protected void rblBenType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblBenType.SelectedValue == "Vendor")
        {
            div_Vendor.Visible = true;
            div_SubCon.Visible = false;
            div_Other.Visible = false;
            ddlSubContractor.SelectedIndex = -1;
            ddlOther.SelectedIndex = -1;
            div_POWO1.Visible = true;
            div_POWO2.Visible = true;
        }
        else if (rblBenType.SelectedValue == "SubContractor")
        {
            div_Vendor.Visible = false;
            div_SubCon.Visible = true;
            div_Other.Visible = false;
            ddlVendor.SelectedIndex = -1;
            ddlOther.SelectedIndex = -1;
            div_POWO1.Visible = true;
            div_POWO2.Visible = true;
        }
        else
        {
            div_Vendor.Visible = false;
            div_SubCon.Visible = false;
            div_Other.Visible = true;
            ddlVendor.SelectedIndex = -1;
            ddlSubContractor.SelectedIndex = -1;
            div_POWO1.Visible = false;
            div_POWO2.Visible = false;
        }
    }

    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVendor.SelectedItem.Text != "-Select")
        {
            BindPOWO();
            GetKYCDetails();
            GetBankDetails();
        }
        else
        {
            lblGSTRegd.Text = "";
            lblPANCopy.Text = "";
            lblBankDetails.Text = "";
            lnkDownloadFile3.Text = "";
            lnkDownloadFile4.Text = "";
            lnkDownloadFile5.Text = "";

            txtBank.Text = "";
            txtBranch.Text = "";
            txtAccNo.Text = "";
            txtInvoiceDate.Text = "";
            txtNoOfDueDate.Text = "";
            txtIFSC.Text = "";
        }
        
    }

    protected void ddlSubContractor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubContractor.SelectedItem.Text != "-Select-")
        {
            BindPOWO();
            GetKYCDetails();
            GetBankDetails();
        }
        else
        {
            lblGSTRegd.Text = "";
            lblPANCopy.Text = "";
            lblBankDetails.Text = "";
            lnkDownloadFile3.Text = "";
            lnkDownloadFile4.Text = "";
            lnkDownloadFile5.Text = "";

            txtBank.Text = "";
            txtBranch.Text = "";
            txtAccNo.Text = "";
            txtIFSC.Text = "";
        }
    }
    protected void BindSubContracotr_Sub()
    {
        try
        {
            objPaymentIndentBL = new PaymentIndentBL();
            DataSet dsDetails = new DataSet();
            objPaymentIndentBL.SubCon_ID = Convert.ToString(ddlSubContractor.SelectedValue);
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SUBCONTRACTOR_DETAILS_SELECT_ALL, ref dsDetails);
            ddlPayment_to_SubContractorName.DataSource = dsDetails;
            ddlPayment_to_SubContractorName.DataTextField = "SubContractor_Name";
            ddlPayment_to_SubContractorName.DataValueField = "ID";
            ddlPayment_to_SubContractorName.DataBind();
            ddlPayment_to_SubContractorName.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void ddlOther_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOther.SelectedItem.Text != "-Select-")
        {
            ddlPOWO.Items.Clear();
            ddlPOWO.DataSource = null;
            ddlPOWO.DataBind();
            GetKYCDetails();
            GetBankDetails();
        }
        else
        {
            lblGSTRegd.Text = "";
            lblPANCopy.Text = "";
            lblBankDetails.Text = "";
            lnkDownloadFile3.Text = "";
            lnkDownloadFile4.Text = "";
            lnkDownloadFile5.Text = "";

            txtBank.Text = "";
            txtBranch.Text = "";
            txtAccNo.Text = "";
            txtIFSC.Text = "";
        }
    }

    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProject.SelectedItem.Text != "-Select-")
            {
                ds = new DataSet();
                objProjectBL = new ProjectBL();
                ds = new DataSet();
                objProjectBL.Project_Code = ddlProject.SelectedValue;
                objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_DETAILS_BY_ID, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlState.SelectedValue = ds.Tables[0].Rows[0]["State_Code"].ToString();
                    txtWorkDesc.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                    txtProjectCode.Text = ds.Tables[0].Rows[0]["Project_Code"].ToString();
                    txtAwardedBy.Text = ds.Tables[0].Rows[0]["Award_By"].ToString();
                }
            }
            else
            {
                ddlState.SelectedValue = "-Select-";
                txtWorkDesc.Text = string.Empty;
                txtProjectCode.Text = string.Empty;
                txtAwardedBy.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
        
        
    }

    protected void BindPOWO()
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Project_Code = Session["Project_Code"].ToString();
            if (rblBenType.SelectedValue == "Vendor")
            {
                objPaymentIndentBL.Vendor_ID = ddlVendor.SelectedValue;
                objPaymentIndentBL.Task = "LoadPOByVendor";
            }
            else if (rblBenType.SelectedValue == "SubContractor")
            {
                objPaymentIndentBL.SubCon_ID = ddlSubContractor.SelectedValue;
                objPaymentIndentBL.Task = "LoadWOBySubCon";
            }
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_POWO, ref ds);
            ddlPOWO.DataSource = ds;
            ddlPOWO.DataValueField = "POWO_ID";
            ddlPOWO.DataTextField = "POWO_No";
            ddlPOWO.DataBind();
            ddlPOWO.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void GetKYCDetails()
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Project_Code = Session["Project_Code"].ToString();
            if (rblBenType.SelectedValue == "Vendor")
            {
                objPaymentIndentBL.Vendor_ID = ddlVendor.SelectedValue;
                objPaymentIndentBL.Task = "GetKYCDetailsByVendor";
            }
            else if (rblBenType.SelectedValue == "SubContractor")
            {
                objPaymentIndentBL.SubCon_ID = ddlSubContractor.SelectedValue;
                objPaymentIndentBL.Task = "GetKYCDetailsBySubCon";
            }
            else
            {
                objPaymentIndentBL.Other_ID = ddlOther.SelectedValue;
                objPaymentIndentBL.Task = "GetKYCDetailsByOther";
            }
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_POWO, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblGSTRegd.Text = ds.Tables[0].Rows[0]["File_GSTRegistration"].ToString() != "" ? "YES" : "NO";
                lblPANCopy.Text = ds.Tables[0].Rows[0]["File_PANCopy"].ToString() != "" ? "YES" : "NO";
                lblBankDetails.Text = ds.Tables[0].Rows[0]["File_BankDetails"].ToString() != "" ? "YES" : "NO";

                lnkDownloadFile3.Text = ds.Tables[0].Rows[0]["File_GSTRegistration"].ToString();
                lnkDownloadFile4.Text = ds.Tables[0].Rows[0]["File_PANCopy"].ToString();
                lnkDownloadFile5.Text = ds.Tables[0].Rows[0]["File_BankDetails"].ToString();
            }
            else
            {
                lnkDownloadFile3.Text = "";
                lnkDownloadFile4.Text = "";
                lnkDownloadFile5.Text = "";
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void GetBankDetails()
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Project_Code = Session["Project_Code"].ToString();
            if (rblBenType.SelectedValue == "Vendor")
            {
                objPaymentIndentBL.Vendor_ID = ddlVendor.SelectedValue;
                objPaymentIndentBL.Task = "GetBankDetailsByVendor";
            }
            else if (rblBenType.SelectedValue == "SubContractor")
            {
                objPaymentIndentBL.SubCon_ID = ddlSubContractor.SelectedValue;
                objPaymentIndentBL.Task = "GetBankDetailsBySubCon";
            }
            else
            {
                objPaymentIndentBL.Other_ID = ddlOther.SelectedValue;
                objPaymentIndentBL.Task = "GetBankDetailsByOther";
            }

            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_POWO, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtBank.Text = ds.Tables[0].Rows[0]["Bank"].ToString();
                txtBranch.Text = ds.Tables[0].Rows[0]["Branch"].ToString();
                txtAccNo.Text = ds.Tables[0].Rows[0]["Acc_No"].ToString();
                txtIFSC.Text = ds.Tables[0].Rows[0]["IFSC"].ToString();
                txtLegalName.Text = ds.Tables[0].Rows[0]["Legal_Name"].ToString();
                if (ds.Tables[0].Rows[0]["Legal_Name"].ToString() != "")
                {
                    //txtSubContractorLegalName.Text = ds.Tables[0].Rows[0]["Legal_Name"].ToString();
                }
                else
                {
                    //txtSubContractorLegalName.Enabled = false;
                }

                //txtSubContractorName.Text = ddlSubContractor.SelectedItem.Text;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ddlPOWO_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            if (ddlPOWO.SelectedItem.Text != "-Select-")
            {
                objPaymentIndentBL.Project_Code = Session["Project_Code"].ToString();
                objPaymentIndentBL.POWO_ID = Convert.ToInt32(ddlPOWO.SelectedValue);
                
                objPaymentIndentBL.Task = "LoadPOWOByID";
                if (rblBenType.SelectedValue == "Vendor")
                {
                    objPaymentIndentBL.Vendor_ID = ddlVendor.SelectedValue;
                    objPaymentIndentBL.Task = "LoadPODetails";
                    //lnkPrintPOWO. = "PO Print Page";
                    btnPrintPOWO.Visible = true;
                    btnPrintPOWO.Target = "_blank";
                    btnPrintPOWO.HRef = "PO_Print.aspx?PONo=" + ddlPOWO.SelectedItem.Text;
                }
                else
                {
                    objPaymentIndentBL.SubCon_ID = ddlSubContractor.SelectedValue;
                    objPaymentIndentBL.Task = "LoadWODetails";
                    //lnkPrintPOWO.Text = "WO Print Page";
                    btnPrintPOWO.Visible = true;
                    btnPrintPOWO.Target = "_blank";
                    if (ddlPOWO.SelectedItem.Text.Contains("/HO/"))
                    {
                        objPaymentIndentBL.WO_Type = "HO";
                        btnPrintPOWO.HRef = "WOHire_Print.aspx?WONo=" + ddlPOWO.SelectedItem.Text;
                    }
                    else
                    {
                        objPaymentIndentBL.WO_Type = "WO";
                        btnPrintPOWO.HRef = "WO_Print.aspx?WONo=" + ddlPOWO.SelectedItem.Text;
                    }
                }
                objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_POWO, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtNatureOfWork.Text = ds.Tables[0].Rows[0]["Item_Type"].ToString();
                    txtNatureOfMaterial.Text = ds.Tables[0].Rows[0]["Item_Desc"].ToString();
                }
                else
                {
                    txtNatureOfWork.Text = "";
                    txtNatureOfMaterial.Text = "";
                }
            }
            else
            {
                btnPrintPOWO.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void lnkDownloadFile_Click(object sender, EventArgs e)
    {
        try
        {
            //string filePath = "../UploadedFiles/" + (sender as LinkButton).Text;
            //string filepathnew = Server.MapPath("~\\UploadedFiles\\" + (sender as LinkButton).Text);
            //FileInfo file = new FileInfo(filePath);
            //Response.Clear();
            //Response.ContentType = "application/pdf";
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            //Response.Flush();
            //Response.TransmitFile(filepathnew);
            ////Response.WriteFile(filePath);
            //Response.End();

            string filePath = Server.MapPath("~\\UploadedFiles\\" + (sender as LinkButton).Text.Replace("/", "-"));
            Response.Clear();

            string path = filePath;
            WebClient client = new WebClient();
            Byte[] buffer = client.DownloadData(path);
            if (buffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }

    protected void UPLOADFILES()
    {
        objPaymentIndentBL = new PaymentIndentBL();
        ds = new DataSet();
        objPaymentIndentBL.PayInd_No = ViewState["PayInd_No"].ToString();

        int billCount = 1;
        int siteImgCount = 1;
        objPaymentIndentBL.Task = "File_Count";
        objPaymentIndentBL.File_Type_Like = ViewState["PayInd_No"].ToString() + ddlUploadBill.SelectedItem.Text;
        if (objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.FILE_COUNT, ref ds))
        {
            billCount = ds.Tables[1].Rows[0]["BillCount"].ToString() == string.Empty ? 1 : Convert.ToInt32(ds.Tables[1].Rows[0]["BillCount"]) + 1;
            siteImgCount = ds.Tables[0].Rows[0]["SiteImgCount"].ToString() == string.Empty ? 1 : Convert.ToInt32(ds.Tables[0].Rows[0]["SiteImgCount"]) + 1;
        }

        if (fuBill.HasFile)
        {
            foreach (var file in fuBill.PostedFiles)
            {
                if (ddlUploadBill.SelectedItem.Text != "-Select-")
                {
                    objPaymentIndentBL.File_Type = ddlUploadBill.SelectedItem.Text;
                    objPaymentIndentBL.File_Bill = ViewState["PayInd_No"].ToString() + ddlUploadBill.SelectedItem.Text + billCount + System.IO.Path.GetExtension(fuBill.FileName);
                }
                else
                {
                    objPaymentIndentBL.File_Type = "Bill Copy";
                    objPaymentIndentBL.File_Bill = ViewState["PayInd_No"].ToString() + "_Bill" + billCount + System.IO.Path.GetExtension(fuBill.FileName);
                }
                fuBill.SaveAs(Server.MapPath("~\\UploadedFiles\\" + txtPayIndNo.Text + ddlUploadBill.SelectedItem.Text + billCount + System.IO.Path.GetExtension(fuBill.FileName)));
                objPaymentIndentBL.Task = "Insert_PaymentIndent_File_Bill";
                if (objPaymentIndentBL.insertFile(con, PaymentIndentBL.eLoadSp.FILE_INSERT))
                {
                    billCount++;
                }
            }
        }


        if (fuSiteImg.HasFile)
        {
            foreach (var file in fuSiteImg.PostedFiles)
            {
                objPaymentIndentBL.File_SiteImg = "_SiteImg" + siteImgCount + System.IO.Path.GetExtension(fuSiteImg.FileName);
                fuSiteImg.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "PaymentIndent_" + txtPayIndNo.Text + "_SiteImg" + siteImgCount + System.IO.Path.GetExtension(fuSiteImg.FileName)));
                objPaymentIndentBL.Task = "Insert_PaymentIndent_File_SiteImg";
                if (objPaymentIndentBL.insertFile(con, PaymentIndentBL.eLoadSp.FILE_INSERT))
                {
                    siteImgCount++;
                }
            }
        }

        BindFileGrid();
    }

    protected void BindFileGrid()
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "GetPaymentIndentFile";
            objPaymentIndentBL.PayInd_No = ViewState["PayInd_No"].ToString();
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_PAYMENTINDENTDETAILS, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Grid_File.DataSource = ds;
                Grid_File.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }

    protected void Grid_File_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "DeleteFile";
            objPaymentIndentBL.PayInd_No = ViewState["PayInd_No"].ToString();
            objPaymentIndentBL.File_Name = e.Record["File_Name"].ToString();

            if (objPaymentIndentBL.delete(con, PaymentIndentBL.eLoadSp.DELETE_FILE_BY_FILENAME))
            {
                BindFileGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('File has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete File !.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }


    protected void btnAddItem1_Click(object sender, EventArgs e)
    {
        ClearItemPopup();
        lblHeader.Text = "Add Material Issued Item";
        btnSaveItem.Text = "Save";
        ModalItem.Show();
    }

    protected void btnAddItem2_Click(object sender, EventArgs e)
    {
        ClearItemPopup();
        lblHeader.Text = "Add Withhold Item";
        btnSaveItem.Text = "Save";
        ModalItem.Show();
    }

    protected void btnSaveItem_Click(object sender, EventArgs e)
    {
        try
        {
            objPaymentIndentBL = new PaymentIndentBL();
            if (txtPayIndNo.Text != "")
            {
                objPaymentIndentBL.PayInd_No = txtPayIndNo.Text;
            }
            if (lblHeader.Text == "Add Material Issued Item")
            {
                objPaymentIndentBL.Item_Category = "Material Issued";
            }
            else
            {
                objPaymentIndentBL.Item_Category = "Withhold Item";
            }

            objPaymentIndentBL.Item_Name = txtItemName.Text;
            objPaymentIndentBL.Item_Amount = Convert.ToDecimal(txtItemAmt.Text.Trim());

            if (btnSaveItem.Text == "Save")
            {
                objPaymentIndentBL.Task = "Insert_PaymentIndent_Item";
                if (objPaymentIndentBL.insertItem(con, PaymentIndentBL.eLoadSp.ITEM_INSERT_UPDATE))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Item has been added successfully');", true);
                    BindItemGrid();
                    ClearItemPopup();
                }
                else
                {
                    ModalItem.Show();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Item already exists !!!');", true);
                }
            }
            //else
            //{
            //    objWO.Task = "UpdateWOSubItem";
            //    if (objWO.insertItem(con, WorkOrderBL.eLoadSp.UPDATE_WO_ITEM))
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Sub Item has been updated successfully');", true);
            //        BindWOItems();
            //        ClearItemPopup();
            //        ClearSubItemPopup();
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update Sub Item !!!');", true);
            //    }
            //}
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancelItem_Click(object sender, EventArgs e)
    {
        ClearItemPopup();
    }

    protected void ClearItemPopup()
    {
        btnSaveItem.Text = "Save";
        txtItemName.Text = "";
        txtItemAmt.Text = "";
    }

    protected void BindItemGrid()
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "GetPaymentIndentItem";
            if (ViewState["PayInd_No"] != null)
            {
                objPaymentIndentBL.PayInd_No = ViewState["PayInd_No"].ToString();
            }
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_PAYMENTINDENTDETAILS, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Grid_Item.DataSource = ds;
                Grid_Item.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }

    protected void Grid_Item_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "DeleteItem";
            if (ViewState["PayInd_No"] != null)
            {
                objPaymentIndentBL.PayInd_No = ViewState["PayInd_No"].ToString();
            }
            objPaymentIndentBL.Item_ID = Convert.ToInt32(e.Record["PayInd_Item_Id"]);

            if (objPaymentIndentBL.delete(con, PaymentIndentBL.eLoadSp.DELETE_ITEM_BY_ID))
            {
                BindItemGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Item has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete Item !.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindLedger()
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "GetLedger";
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_ALL_LEDGER, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlLedger.DataSource = null;
                ddlLedger.DataBind();
                ddlLedger.DataSource = ds.Tables[0];
                ddlLedger.DataValueField = "Ledger_ID";
                ddlLedger.DataTextField = "Ledger_Name";
                ddlLedger.DataBind();
                ddlLedger.Items.Insert(0, "-Select-");

                Grid_Ledger.DataSource = ds;
                Grid_Ledger.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }

    protected void btnSaveLedger_Click(object sender, EventArgs e)
    {
        try
        {
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Ledger_Name = txtLedgerName.Text.Trim();
            objPaymentIndentBL.Task = "Insert_Ledger";
            if (objPaymentIndentBL.insertLedger(con, PaymentIndentBL.eLoadSp.INSERT_LEDGER))
            {
                BindLedger();
                txtLedgerName.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Ledger has been added successfully.');", true);

                ModelLedgerPopup.Show();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Ledger already exists.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancelLedger_Click(object sender, EventArgs e)
    {
        txtLedgerName.Text = string.Empty;
        ModelLedgerPopup.Hide();
        //Response.Redirect("..//Master/Material.aspx", false);
    }

    protected void Grid_Ledger_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Ledger_ID = Convert.ToInt32(e.Record["Ledger_ID"].ToString());
            objPaymentIndentBL.Task = "Delete_Ledger";
            if (objPaymentIndentBL.delete(con, PaymentIndentBL.eLoadSp.DELETE_LEDGER))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Ledger has been deleted sucessfully.');", true);
                BindLedger();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to Delete !');", true);
            }
            ModelLedgerPopup.Show();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void GetCompletedPayInd()
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Project_Code = Session["Project_Code"].ToString();
            if (rblBenType.SelectedValue == "Vendor")
            {
                objPaymentIndentBL.Vendor_ID = ddlVendor.SelectedValue;
                objPaymentIndentBL.Task = "GetCompletedPayIndByVendor";
            }
            else if (rblBenType.SelectedValue == "SubContractor")
            {
                objPaymentIndentBL.SubCon_ID = ddlSubContractor.SelectedValue;
                objPaymentIndentBL.Task = "GetCompletedPayIndBySubCon";
            }
            else
            {
                objPaymentIndentBL.Other_ID = ddlOther.SelectedValue;
                objPaymentIndentBL.Task = "GetCompletedPayIndByOther";
            }

            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_POWO, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Grid_PaymentIndent_Completed.DataSource = ds;
                Grid_PaymentIndent_Completed.DataBind();
            }
            else
            {
                Grid_PaymentIndent_Completed.DataSource = null;
                Grid_PaymentIndent_Completed.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

}
