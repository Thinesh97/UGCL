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


public partial class PaymentIndent : System.Web.UI.Page
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
    decimal TDSAmtPercentage = 0.00m;
    decimal TDSAmt = 0.00m;
    decimal TotalItemAmount = 0.00m;
    decimal EarlierPayment = 0.00m;
    decimal TaxDeduction = 0.00m;
    decimal Amt_Transferable = 0;
    decimal Amt_EarlierPayment = 0;
    decimal Total_Payable = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    BindState();
                    BindApprover();
                    BindVerifier();
                    BindLedger();
                    BindPaymentCategory();
                    rblBenType_SelectedIndexChanged(null, null);

                    if (Request.QueryString["PayInd_No"] != null)
                    {
                        ViewState["PayInd_No"] = Request.QueryString["PayInd_No"].ToString();
                        GetPaymentIndentDetails();
                        BindFileGrid();
                        BindItemGrid();
                    }
                }
                else
                {
                    Response.Redirect("../CommonPages/Login.aspx", false);
                }
                //if (Request.QueryString["PageID"].ToString() != null)
                //{
                //    ddlTDSPercent.Enabled = false;
                //    txtTDSDeductedAmt.Enabled = false;
                //}
                //else
                //{
                //    Response.Redirect("../CommonPages/Login.aspx", false);
                //}

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
                txtPayIndNo.Text = dsDetails.Tables[0].Rows[0]["PayInd_No"].ToString();
                txtPayIndDate.Text = Convert.ToDateTime(dsDetails.Tables[0].Rows[0]["PayInd_Date"]).ToString("dd-MM-yyyy");

                ddlState.SelectedValue = dsDetails.Tables[0].Rows[0]["State_Code"].ToString();
                ddlState_SelectedIndexChanged(null, null);
                ddlFYear.SelectedValue = dsDetails.Tables[0].Rows[0]["FYear"].ToString();
                ddlProject.SelectedValue = dsDetails.Tables[0].Rows[0]["Project_Code"].ToString();
                ddlProject_SelectedIndexChanged(null, null);
                ddlApprover.SelectedValue = dsDetails.Tables[0].Rows[0]["Approver"].ToString();
                rblBenType.SelectedValue = dsDetails.Tables[0].Rows[0]["Beneficiary_Type"].ToString();
                if (dsDetails.Tables[0].Rows[0]["Payment_To_SubContractorName"].ToString() != "")
                {
                    BindSubCon_Sub();
                    ddlPayment_to_SubContractorName.SelectedItem.Text = dsDetails.Tables[0].Rows[0]["Payment_To_SubContractorName"].ToString();

                }
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
                txtService_Item_Supplied_Month.Text = dsDetails.Tables[0].Rows[0]["Service_Item_Supplied_Month"].ToString();
                txtBank.Text = dsDetails.Tables[0].Rows[0]["Bank_Name"].ToString();
                txtBranch.Text = dsDetails.Tables[0].Rows[0]["Bank_Branch"].ToString();
                txtAccNo.Text = dsDetails.Tables[0].Rows[0]["Bank_Account"].ToString();
                txtIFSC.Text = dsDetails.Tables[0].Rows[0]["Bank_IFSC"].ToString();
                txtInvoiceDate.Text = dsDetails.Tables[0].Rows[0]["Invoice_Date"].ToString();
                txtNoOfDueDate.Text = dsDetails.Tables[0].Rows[0]["No_Of_Due_Date"].ToString();
                ddlPaymentMode.SelectedValue = dsDetails.Tables[0].Rows[0]["Payment_Mode"].ToString();
                ddlPaymentType.SelectedValue = dsDetails.Tables[0].Rows[0]["Payment_Type"].ToString();
                ddlPaymentCategory.SelectedItem.Text = dsDetails.Tables[0].Rows[0]["Payment_Category"].ToString();
                txtAmt_ServiceMaterial.Text = dsDetails.Tables[0].Rows[0]["Amt_ServiceMaterial"].ToString();
                if (dsDetails.Tables[0].Rows[0]["Status"].ToString() == "SendForApproval")
                {
                    Status.ForeColor = System.Drawing.Color.Violet;
                    Status.Text = "Pending For Approval";
                }

                else if (dsDetails.Tables[0].Rows[0]["Status"].ToString() == "Approved")
                {
                    Status.ForeColor = System.Drawing.Color.Green;
                    Status.Text = dsDetails.Tables[0].Rows[0]["Status"].ToString();
                }
                else if (dsDetails.Tables[0].Rows[0]["Status"].ToString() == "Hold")
                {
                    Status.ForeColor = System.Drawing.Color.Orange;
                    Status.Text = dsDetails.Tables[0].Rows[0]["Status"].ToString();
                }
                else if (dsDetails.Tables[0].Rows[0]["Status"].ToString() == "SendForModification")
                {
                    Status.ForeColor = System.Drawing.Color.Blue;
                    Status.Text = dsDetails.Tables[0].Rows[0]["Status"].ToString();
                }
                else if (dsDetails.Tables[0].Rows[0]["Status"].ToString() == "Rejected")
                {
                    Status.ForeColor = System.Drawing.Color.Red;
                    Status.Text = dsDetails.Tables[0].Rows[0]["Status"].ToString();
                }
                else if (dsDetails.Tables[0].Rows[0]["Status"].ToString() == "Returned")
                {
                    Status.ForeColor = System.Drawing.Color.Red;
                    Status.Text = dsDetails.Tables[0].Rows[0]["Status"].ToString();
                }

                if (!DBNull.Value.Equals(dsDetails.Tables[0].Rows[0]["GST_Amount"]))
                {
                    GST_Amount = Convert.ToDecimal(dsDetails.Tables[0].Rows[0]["GST_Amount"]);

                }
                if (dsDetails.Tables[0].Rows[0]["Amt_ServiceMaterial"] != "")
                {
                    Amt_ServiceMaterial = Convert.ToDecimal(dsDetails.Tables[0].Rows[0]["Amt_ServiceMaterial"].ToString());
                    txtBasicTotal.Text = (Amt_ServiceMaterial - GST_Amount).ToString();
                }
                txtAmt_EarlierPayment.Text = dsDetails.Tables[0].Rows[0]["Amt_EarlierPayment"].ToString();
                ddlVerifier.SelectedValue = dsDetails.Tables[0].Rows[0]["Verifier"].ToString();
                txtAmt_PartPayment.Text = dsDetails.Tables[0].Rows[0]["Amt_PartPayment"].ToString();
                txtTDSDeductedAmt.Text = dsDetails.Tables[0].Rows[0]["TDS_Amt"].ToString();
                ddlTDSPercent.SelectedValue = dsDetails.Tables[0].Rows[0]["TDS_Perc"].ToString();
                txtTotalPayable.Text = dsDetails.Tables[0].Rows[0]["Total_Payable"].ToString();
                ddlTDSPercent.SelectedItem.Text = dsDetails.Tables[0].Rows[0]["TDS_Perc"].ToString();
                txtGSTPercent.Text = dsDetails.Tables[0].Rows[0]["GST_Percent"].ToString();
                txtGSTAmount.Text = dsDetails.Tables[0].Rows[0]["GST_Amount"].ToString();
                txtRemarks.Text = dsDetails.Tables[0].Rows[0]["Verifier_Remarks"].ToString();
                txtOtherDeductions.Text = dsDetails.Tables[0].Rows[0]["Other_Deductions"].ToString();
                //if (!DBNull.Value.Equals(dsDetails.Tables[0].Rows[0]["Amt_ServiceMaterial"]))
                //{
                //    GetEarlierPayment(ddlPOWO.SelectedValue);
                //    if (!DBNull.Value.Equals(dsDetails.Tables[0].Rows[0]["Total_Payable"]))
                //    {
                //        var BilledValue = Convert.ToDecimal(dsDetails.Tables[0].Rows[0]["Total_Payable"].ToString());
                //        txtAmt_EarlierPayment.Text = EarlierPayment.ToString();
                //        txtAmt_PartPayment.Text = (BilledValue - EarlierPayment).ToString();
                //    }

                //}
                if (dsDetails.Tables[0].Rows[0]["Status"].ToString() == "SendForApproval")
                {

                    if (Session["UID"].ToString() == dsDetails.Tables[0].Rows[0]["Approver"].ToString())
                    {
                        btnAprove.Enabled = true;
                        BtnHold.Enabled = true;
                        btnModification.Enabled = true;
                        btnReject.Enabled = true;
                        AproveAmountDiv.Visible = true;
                        RemarksDiv.Visible = true;
                        txtAprovedAmount.Text = dsDetails.Tables[0].Rows[0]["Amt_PartPayment"].ToString();
                    }
                }
                if (dsDetails.Tables[0].Rows[0]["Status"].ToString() == "SendForApproval")
                {
                    btnSubmit.Text = "Update Indent";
                }
                else
                {
                    btnSubmit.Text = "Update";
                }

                lbtnPrint.Visible = true;
                btnSubmit.Visible = Session["UID"].ToString() == dsDetails.Tables[0].Rows[0]["Approver"].ToString() ? false : true;
                if (Request.QueryString["PageID"] != null)
                {
                    if (Request.QueryString["PageID"] == "11")
                    {
                        GetEarlierPayment(ddlPOWO.SelectedValue);
                        btnSubmit.Text = "Send for Verification";
                        txtPayIndNo.Text = string.Empty;
                        if (!DBNull.Value.Equals(dsDetails.Tables[0].Rows[0]["Total_Payable"]))
                        {
                            txtTotalPayable.Text = dsDetails.Tables[0].Rows[0]["Total_Payable"].ToString();
                        }
                        if (!DBNull.Value.Equals(dsDetails.Tables[0].Rows[0]["Amt_EarlierPayment"]))
                        {
                            Amt_EarlierPayment = Convert.ToDecimal(dsDetails.Tables[0].Rows[0]["Amt_EarlierPayment"].ToString());
                        }
                        if (!DBNull.Value.Equals(dsDetails.Tables[0].Rows[0]["Amt_Transferable"]))
                        {
                            Amt_Transferable = Convert.ToDecimal(dsDetails.Tables[0].Rows[0]["Amt_Transferable"].ToString());
                        }
                        ddlApprover.SelectedValue = Convert.ToString(!DBNull.Value.Equals(dsDetails.Tables[0].Rows[0]["Approver"]));

                        if (EarlierPayment > 0)
                        {
                            txtAmt_EarlierPayment.Text = EarlierPayment.ToString();
                            txtAmt_PartPayment.Text = (Total_Payable - EarlierPayment).ToString();
                        }
                        btnSubmit.Visible = true;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    //Binding all the dropdownlist
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
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "GetAllState";
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.GET_STATE, ref ds);
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

    protected void BindProjectList()
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "GetAllProjectForState";
            objPaymentIndentBL.StateCode = ddlState.SelectedValue;
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.GET_PROJECT_BY_STATE, ref ds);
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

    protected void BindVendor()
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL.Task = "GetAllVendorForState";
            objPaymentIndentBL.StateCode = ddlState.SelectedValue;
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.GET_VENDOR_BY_STATE, ref ds);
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

    protected void BindSubCon()
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL.Task = "GetAllSubConForState";
            objPaymentIndentBL.StateCode = ddlState.SelectedValue;
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.GET_SUBCON_BY_STATE, ref ds);
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
            objPaymentIndentBL.Task = "GetAllOtherForState";
            objPaymentIndentBL.StateCode = ddlState.SelectedValue;
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.GET_OTHER_BY_STATE, ref ds);
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

    protected void BindSubCon_Sub()
    {
        try
        {
            DataSet dsSubCon = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "GetAllSubConBySubCon";
            objPaymentIndentBL.StateCode = ddlState.SelectedValue;
            objPaymentIndentBL.SubCon_ID = Convert.ToString(ddlSubContractor.SelectedValue);
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.GET_BENEFICIARY_DETAIL, ref dsSubCon);
            ddlPayment_to_SubContractorName.DataSource = dsSubCon;
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

    protected void BindPOWO()
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.StateCode = ddlState.SelectedValue;
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
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.GET_BENEFICIARY_DETAIL, ref ds);
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

    //All change events
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
            Ra_Dvi.Visible = false;
            Supplied_From_ToDateDiv.Visible = true;
            txtAmt_EarlierPayment.Text = string.Empty;
            txtAmt_EarlierPayment.Text = string.Empty;
            txtAmt_ServiceMaterial.Text = string.Empty;
            RequiredFieldValidator6.Enabled = false;
            RequiredFieldValidator5.Enabled = false;
            RequiredFieldValidator4.Enabled = true;
            RequiredFieldValidator7.Enabled = true;
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
            Ra_Dvi.Visible = true;
            Supplied_From_ToDateDiv.Visible = true;
            txtAmt_EarlierPayment.Text = string.Empty;
            txtAmt_ServiceMaterial.Text = string.Empty;
            RequiredFieldValidator6.Enabled = false;
            RequiredFieldValidator5.Enabled = true;
            RequiredFieldValidator4.Enabled = false;
            RequiredFieldValidator7.Enabled = true;
        }
        else
        {
            Ra_Dvi.Visible = false;
            div_Vendor.Visible = false;
            div_SubCon.Visible = false;
            div_Other.Visible = true;
            ddlVendor.SelectedIndex = -1;
            ddlSubContractor.SelectedIndex = -1;
            div_POWO1.Visible = false;
            div_POWO2.Visible = false;
            Supplied_From_ToDateDiv.Visible = true;
            txtAmt_EarlierPayment.Text = string.Empty;
            txtAmt_ServiceMaterial.Text = string.Empty;
            RequiredFieldValidator5.Enabled = false;
            RequiredFieldValidator4.Enabled = false;
            RequiredFieldValidator6.Enabled = true;
            RequiredFieldValidator7.Enabled = false;
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlState.SelectedItem.Text != "-Select-")
            {
                BindProjectList();
                BindVendor();
                BindSubCon();
                BindOther();
                txtWorkDesc.Text = string.Empty;
                txtProjectCode.Text = string.Empty;
                txtAwardedBy.Text = string.Empty;
            }
            else
            {
                ddlProject.SelectedValue = "-Select-";
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

    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProject.SelectedItem.Text != "-Select-")
            {
                ds = new DataSet();
                objPaymentIndentBL = new PaymentIndentBL();
                objPaymentIndentBL.Task = "GetProjectDetails";
                objPaymentIndentBL.StateCode = ddlState.SelectedValue;
                objPaymentIndentBL.Project_Code = ddlProject.SelectedValue;
                objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_PROJECT_DETAILS_BY_ID, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtWorkDesc.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                    txtProjectCode.Text = ds.Tables[0].Rows[0]["Project_Code"].ToString();
                    txtAwardedBy.Text = ds.Tables[0].Rows[0]["Award_By"].ToString();
                }
            }
            else
            {
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

    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVendor.SelectedItem.Text != "-Select")
        {
            BindPOWO();
            GetBeneficiaryDetails();
            GetCompletedPayInd();
            BindSubCon_Sub();
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
            GetBeneficiaryDetails();
            GetCompletedPayInd();
            BindSubCon_Sub();
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

    protected void ddlOther_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOther.SelectedItem.Text != "-Select-")
        {
            ddlPOWO.Items.Clear();
            ddlPOWO.DataSource = null;
            ddlPOWO.DataBind();
            GetBeneficiaryDetails();
            BindSubCon_Sub();
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

    protected void ddlPOWO_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPOWO.SelectedItem.Text != "-Select-")
            {
                GetEarlierPayment(ddlPOWO.SelectedValue);

                if (rblBenType.SelectedValue == "Vendor")
                {
                    //lnkPrintPOWO.Text = "PO Print Page";
                    btnPrintPOWO.Visible = true;
                    btnPrintPOWO.Target = "_blank";
                    btnPrintPOWO.HRef = "PaymentIndent_PO_Print.aspx?State=" + ddlState.SelectedValue + "&PONo=" + ddlPOWO.SelectedItem.Text;

                    DataSet dsPOWO = new DataSet();
                    objPaymentIndentBL = new PaymentIndentBL();
                    objPaymentIndentBL.Task = "POItemDesc";
                    objPaymentIndentBL.StateCode = ddlState.SelectedValue;
                    objPaymentIndentBL.POWO_ID = Convert.ToInt32(ddlPOWO.SelectedValue);
                    objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.GET_POWO_DETAILS, ref dsPOWO);
                    if (dsPOWO.Tables[0].Rows.Count > 0)
                    {
                        txtNatureOfWork.Text = dsPOWO.Tables[0].Rows[0]["Item_Type"].ToString();
                        txtNatureOfMaterial.Text = dsPOWO.Tables[0].Rows[0]["Item_Desc"].ToString();
                    }
                    else
                    {
                        txtNatureOfWork.Text = "";
                        txtNatureOfMaterial.Text = "";
                    }
                    GetPurchaseOrderDetails();
                }
                else
                {
                    DataSet dsPOWO = new DataSet();
                    objPaymentIndentBL = new PaymentIndentBL();
                    objPaymentIndentBL.Task = "WOItemDesc";
                    objPaymentIndentBL.StateCode = ddlState.SelectedValue;
                    objPaymentIndentBL.POWO_ID = Convert.ToInt32(ddlPOWO.SelectedValue);

                    //lnkPrintPOWO.Text = "WO Print Page";
                    btnPrintPOWO.Visible = true;
                    btnPrintPOWO.Target = "_blank";
                    if (ddlPOWO.SelectedItem.Text.Contains("/HO/"))
                    {
                        btnPrintPOWO.HRef = "PaymentIndent_WOHire_Print.aspx?State=" + ddlState.SelectedValue + "&WONo=" + ddlPOWO.SelectedItem.Text;
                        objPaymentIndentBL.WO_Type = "HO";
                    }
                    else if (ddlPOWO.SelectedItem.Text.Contains("/WO/"))
                    {
                        btnPrintPOWO.HRef = "PaymentIndent_WO_Print.aspx?State=" + ddlState.SelectedValue + "&WONo=" + ddlPOWO.SelectedItem.Text;
                        objPaymentIndentBL.WO_Type = "WO";
                    }
                    else
                    {
                        //btnPrintPOWO.HRef = "WO_Print.aspx?WONo=" + ddlPOWO.SelectedItem.Text;
                        objPaymentIndentBL.WO_Type = "PSO";
                        btnPrintPOWO.HRef = "PaymentIndent_PSO_Print.aspx?State=" + ddlState.SelectedValue + "&PSONo=" + ddlPOWO.SelectedItem.Text;
                    }

                    objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.GET_POWO_DETAILS, ref dsPOWO);
                    if (dsPOWO.Tables[0].Rows.Count > 0)
                    {
                        txtNatureOfWork.Text = dsPOWO.Tables[0].Rows[0]["Item_Type"].ToString();
                        txtNatureOfMaterial.Text = dsPOWO.Tables[0].Rows[0]["Item_Desc"].ToString();
                    }
                    else
                    {
                        txtNatureOfWork.Text = "";
                        txtNatureOfMaterial.Text = "";
                    }
                    GetWorkOrderDetails();
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

    protected void ddlPayment_to_SubContractorName_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsBankdetails = new DataSet();
        objPaymentIndentBL = new PaymentIndentBL();
        objPaymentIndentBL.Task = "GetSubConDetails_SubCon";
        objPaymentIndentBL.StateCode = ddlState.SelectedValue;
        objPaymentIndentBL.SubCon_ID = ddlPayment_to_SubContractorName.SelectedValue;
        objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.GET_BENEFICIARY_DETAIL, ref dsBankdetails);
        if (dsBankdetails.Tables[0].Rows.Count > 0)
        {
            txtBank.Text = dsBankdetails.Tables[0].Rows[0]["Bank_Name"].ToString();
            txtBranch.Text = dsBankdetails.Tables[0].Rows[0]["Branch"].ToString();
            txtAccNo.Text = dsBankdetails.Tables[0].Rows[0]["Account_Number"].ToString();
            txtIFSC.Text = dsBankdetails.Tables[0].Rows[0]["IFC_code"].ToString();
        }
    }

    protected void CalculateDate(object sender, EventArgs e)
    {
        if (txtPayIndDate.Text.ToString() != "" && txtInvoiceDate.Text.ToString() != "")
        {
            DateTime firstDate = Convert.ToDateTime(txtInvoiceDate.Text.ToString());
            DateTime secondDate = Convert.ToDateTime(txtPayIndDate.Text.ToString());
            txtNoOfDueDate.Text = (secondDate - firstDate).TotalDays.ToString().Trim();
        }
        else
        {
            txtNoOfDueDate.Text = string.Empty;
        }
    }


    //All button click events
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["PageID"] != null)
            {
                if (Request.QueryString["PageID"] == "11")
                {
                    if (Request.QueryString["PayInd_No"] != null)
                    {
                        objPaymentIndentBL = new PaymentIndentBL();
                        objPaymentIndentBL.PayInd_No_Partial = Request.QueryString["PayInd_No"].ToString();
                    }
                    objPaymentIndentBL.Task = "Update_Partpayment_Status";
                    if (objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.UPDATE_PARTPAYMENT_STATUS))
                    {

                    }
                }
            }
            if (rblBenType.Text == "SubContractor" && CheckBox_RA_bill.Checked)
            {
                if (fuBill.FileName != "")
                {
                    objPaymentIndentBL = new PaymentIndentBL();
                    objPaymentIndentBL.StateCode = ddlState.SelectedValue;
                    objPaymentIndentBL.PayIndDate = Convert.ToDateTime(txtPayIndDate.Text.Trim());
                    objPaymentIndentBL.FYear = ddlFYear.SelectedValue;
                    if (ddlApprover.SelectedValue != null)
                    {
                        objPaymentIndentBL.Approver_ID = Convert.ToInt32(ddlApprover.SelectedValue.ToString());
                    }

                    if (ddlVerifier.SelectedItem.Text != "-Select-")
                    {
                        objPaymentIndentBL.Verifier_ID = Convert.ToInt32(ddlVerifier.SelectedValue.ToString());
                    }

                    objPaymentIndentBL.BeneficiaryType = rblBenType.SelectedValue;
                    objPaymentIndentBL.Vendor_ID = ddlVendor.SelectedValue.Trim();
                    objPaymentIndentBL.SubCon_ID = ddlSubContractor.SelectedValue.Trim();
                    objPaymentIndentBL.Other_ID = ddlOther.SelectedValue.Trim();
                    objPaymentIndentBL.User_ID = Convert.ToInt32(Session["UID"].ToString());
                    if (ddlPayment_to_SubContractorName.SelectedItem.Text != "-Select-")
                    {
                        objPaymentIndentBL.Payment_To_SubContractorName = ddlPayment_to_SubContractorName.SelectedItem.Text;
                    }

                    if (ddlPaymentCategory.SelectedItem.Text != "")
                    {
                        objPaymentIndentBL.Payment_Category = ddlPaymentCategory.SelectedItem.Text;
                    }
                    if (txtService_Items_Date_From.Text != "")
                    {
                        objPaymentIndentBL.Service_Items_Date_From = Convert.ToDateTime(txtService_Items_Date_From.Text);
                    }
                    else
                    {
                        objPaymentIndentBL.Service_Items_Date_From = DateTime.Today;
                    }
                    if (txtService_Items_Date_TO.Text != "")
                    {
                        objPaymentIndentBL.Service_Items_Date_TO = Convert.ToDateTime(txtService_Items_Date_TO.Text);
                    }
                    else
                    {
                        objPaymentIndentBL.Service_Items_Date_TO = DateTime.Today;
                    }
                    if (ddlProject.SelectedItem.Text != "-Select-")
                    {
                        objPaymentIndentBL.Project = ddlProject.SelectedItem.Text;
                        objPaymentIndentBL.Project_Code = ddlProject.SelectedValue;
                    }
                    //objPaymentIndentBL.State = ddlState.SelectedValue.Trim();
                    //objPaymentIndentBL.WorkDesc = txtWorkDesc.Text.Trim();
                    //objPaymentIndentBL.AwardedBy = txtAwardedBy.Text.Trim();
                    //objPaymentIndentBL.PrincipalContractor = txtPrincipalContractor.Text.Trim();
                    objPaymentIndentBL.Bank_Name = txtBank.Text.Trim();
                    objPaymentIndentBL.Bank_Branch = txtBranch.Text.Trim();
                    objPaymentIndentBL.Bank_Account = txtAccNo.Text.Trim();
                    if (txtGSTPercent.Text != "")
                    {
                        objPaymentIndentBL.GST_Percent = Convert.ToDecimal(txtGSTPercent.Text.Trim());
                        objPaymentIndentBL.GST_Amount = Convert.ToDecimal(txtGSTAmount.Text.Trim());
                    }
                    if (txtOtherDeductions.Text != "")
                    {
                        objPaymentIndentBL.Other_Deductions = Convert.ToDecimal(txtOtherDeductions.Text.Trim());
                    }
                    if (txtTotalPayable.Text != "")
                    {
                        objPaymentIndentBL.Total_Payable = Convert.ToDecimal(txtTotalPayable.Text.Trim());
                    }
                    if (txtTDSDeductedAmt.Text != "")
                    {
                        objPaymentIndentBL.TDS_Amt = Convert.ToDecimal(txtTDSDeductedAmt.Text.ToString());
                    }
                    if (ddlTDSPercent.SelectedValue != "-Select-" && ddlTDSPercent.SelectedValue != "")
                    {
                        objPaymentIndentBL.TDS_Perc = Convert.ToDecimal(ddlTDSPercent.SelectedValue);
                    }
                    if (txtAmt_PartPayment.Text != "")
                    {
                        objPaymentIndentBL.Amt_PartPayment = Convert.ToDecimal(txtAmt_PartPayment.Text.ToString());
                    }
                    if (txtTDSDeductedAmt.Text != "")
                    {
                        objPaymentIndentBL.TDS_Amt = Convert.ToDecimal(txtTDSDeductedAmt.Text.ToString());
                    }
                    if (txtInvoiceDate.Text != "")
                    {
                        objPaymentIndentBL.Invoice_Date = Convert.ToDateTime(txtInvoiceDate.Text.Trim());
                    }
                    if (txtNoOfDueDate.Text != "")
                    {
                        objPaymentIndentBL.No_Of_Due_Date = Convert.ToInt32(txtNoOfDueDate.Text.Trim());
                    }
                    objPaymentIndentBL.Bank_IFSC = txtIFSC.Text.Trim();
                    if (rblBenType.SelectedValue != "Other")
                    {
                        if (ddlPOWO.SelectedValue != "-Select-")
                        {
                            objPaymentIndentBL.POWO_ID = Convert.ToInt32(ddlPOWO.SelectedValue);
                        }
                        objPaymentIndentBL.WO_Type = ddlPOWO.SelectedItem.Text.Contains("/HO/") ? "HO" : "WO";
                    }

                    objPaymentIndentBL.Payment_Type = ddlPaymentType.SelectedItem.Text;
                    objPaymentIndentBL.Payment_Mode = ddlPaymentMode.SelectedItem.Text;
                    if (txtAmt_ServiceMaterial.Text != "")
                    {
                        objPaymentIndentBL.Amt_ServiceMaterial = Convert.ToDecimal(txtAmt_ServiceMaterial.Text.Trim());
                    }
                    if (txtAmt_EarlierPayment.Text != "")
                    {
                        objPaymentIndentBL.Amt_EarlierPayment = Convert.ToDecimal(txtAmt_EarlierPayment.Text.Trim());
                    }

                    if (ddlLedger.SelectedItem.Text != "- Select -")
                    {
                        objPaymentIndentBL.Ledger_Name = ddlLedger.SelectedItem.Text;
                    }
                    objPaymentIndentBL.Status = "Draft";
                    objPaymentIndentBL.WorkDoneFor = txtWorkDoneFor.Text.Trim();
                    objPaymentIndentBL.Service_Item_Supplied_Month = txtService_Item_Supplied_Month.Text.Trim();
                    objPaymentIndentBL.Payment_Approval_Status = false;

                    if (Request.QueryString["PageID"] == "11")
                    {
                        objPaymentIndentBL.BalanceGrid = true;
                        objPaymentIndentBL.Status = "SendForApproval";
                        objPaymentIndentBL.TDS_Amt = 0;
                        objPaymentIndentBL.TDS_Perc = 0;
                        objPaymentIndentBL.Amt_PartPayment = Convert.ToDecimal(txtAmt_PartPayment.Text.ToString());
                    }
                    else
                    {
                        objPaymentIndentBL.BalanceGrid = true;
                    }
                    if (btnSubmit.Text == "Send for Verification")
                    {
                        objPaymentIndentBL.Task = "Insert_PaymentIndent";
                        if (objPaymentIndentBL.insert(con, PaymentIndentBL.eLoadSp.INSERT))
                        {
                            btnSubmit.Text = "Update";
                            lbtnPrint.Visible = true;
                            txtPayIndNo.Text = objPaymentIndentBL.PayInd_No.ToString();
                            ViewState["PayInd_No"] = objPaymentIndentBL.PayInd_No.ToString();
                            ViewState["PayInd_ID"] = Convert.ToInt32(objPaymentIndentBL.PayInd_ID);

                            UPLOADFILES();
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Indent details has been inserted sucessfully.');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to insert Payment Indent details !.');", true);
                        }
                    }
                    else
                    {
                        objPaymentIndentBL.Task = "Update_PaymentIndent";
                        if (btnSubmit.Text == "Update Indent")
                        {
                            objPaymentIndentBL.Status = "SendForApproval";
                        }
                        objPaymentIndentBL.PayInd_No = txtPayIndNo.Text.ToString();

                        if (objPaymentIndentBL.update(con, PaymentIndentBL.eLoadSp.UPDATE))
                        {
                            UPLOADFILES();
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Indent details has been updated sucessfully.');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to Update Payment Indent details !.');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Upload Bill is Mandatory');", true);
                }
            }
            else
            {
                objPaymentIndentBL = new PaymentIndentBL();
                objPaymentIndentBL.StateCode = ddlState.SelectedValue;
                objPaymentIndentBL.PayIndDate = Convert.ToDateTime(txtPayIndDate.Text.Trim());
                objPaymentIndentBL.FYear = ddlFYear.SelectedValue;
                if (ddlApprover.SelectedValue != "-Select-")
                {
                    objPaymentIndentBL.Approver_ID = Convert.ToInt32(ddlApprover.SelectedValue.ToString());
                }
                if (ddlVerifier.SelectedValue != "-Select-")
                {
                    objPaymentIndentBL.Verifier_ID = Convert.ToInt32(ddlVerifier.SelectedValue.ToString());
                }
                if (ddlPaymentCategory.SelectedItem.Text != "")
                {
                    objPaymentIndentBL.Payment_Category = ddlPaymentCategory.SelectedItem.Text;
                }
                objPaymentIndentBL.BeneficiaryType = rblBenType.SelectedValue;
                objPaymentIndentBL.Vendor_ID = ddlVendor.SelectedValue.Trim();
                objPaymentIndentBL.SubCon_ID = ddlSubContractor.SelectedValue.Trim();
                objPaymentIndentBL.Other_ID = ddlOther.SelectedValue.Trim();
                objPaymentIndentBL.User_ID = Convert.ToInt32(Session["UID"].ToString());
                if (txtGSTPercent.Text != "")
                {
                    objPaymentIndentBL.GST_Percent = Convert.ToDecimal(txtGSTPercent.Text.Trim());
                    objPaymentIndentBL.GST_Amount = Convert.ToDecimal(txtGSTAmount.Text.Trim());
                }
                if (txtOtherDeductions.Text != "")
                {
                    objPaymentIndentBL.Other_Deductions = Convert.ToDecimal(txtOtherDeductions.Text.Trim());
                }
                if (ddlPayment_to_SubContractorName.SelectedItem.Text != "-Select-")
                {
                    objPaymentIndentBL.Payment_To_SubContractorName = ddlPayment_to_SubContractorName.SelectedItem.Text;
                }
                if (ddlTDSPercent.SelectedValue != "-Select-" && ddlTDSPercent.SelectedValue != "")
                {
                    objPaymentIndentBL.TDS_Perc = Convert.ToDecimal(ddlTDSPercent.SelectedValue);
                }
                if (txtTDSDeductedAmt.Text != "")
                {
                    objPaymentIndentBL.TDS_Amt = Convert.ToDecimal(txtTDSDeductedAmt.Text);
                }
                if (txtService_Items_Date_From.Text != "")
                {
                    objPaymentIndentBL.Service_Items_Date_From = Convert.ToDateTime(txtService_Items_Date_From.Text);
                }
                else
                {
                    objPaymentIndentBL.Service_Items_Date_From = DateTime.Today;
                }
                if (txtService_Items_Date_TO.Text != "")
                {
                    objPaymentIndentBL.Service_Items_Date_TO = Convert.ToDateTime(txtService_Items_Date_TO.Text);
                }
                else
                {
                    objPaymentIndentBL.Service_Items_Date_TO = DateTime.Today;
                }
                if (ddlProject.SelectedItem.Text != "-Select-")
                {
                    objPaymentIndentBL.Project = ddlProject.SelectedItem.Text;
                    objPaymentIndentBL.Project_Code = ddlProject.SelectedValue;
                }
                if (txtTotalPayable.Text != "")
                {
                    objPaymentIndentBL.Total_Payable = Convert.ToDecimal(txtTotalPayable.Text.Trim());
                }
                //objPaymentIndentBL.State = ddlState.SelectedValue.Trim();
                //objPaymentIndentBL.WorkDesc = txtWorkDesc.Text.Trim();
                //objPaymentIndentBL.AwardedBy = txtAwardedBy.Text.Trim();
                //objPaymentIndentBL.PrincipalContractor = txtPrincipalContractor.Text.Trim();
                objPaymentIndentBL.Bank_Name = txtBank.Text.Trim();
                objPaymentIndentBL.Bank_Branch = txtBranch.Text.Trim();
                objPaymentIndentBL.Bank_Account = txtAccNo.Text.Trim();
                if (txtInvoiceDate.Text != "")
                {
                    objPaymentIndentBL.Invoice_Date = Convert.ToDateTime(txtInvoiceDate.Text.Trim());
                }
                if (txtNoOfDueDate.Text != "")
                {
                    objPaymentIndentBL.No_Of_Due_Date = Convert.ToInt32(txtNoOfDueDate.Text.Trim());
                }
                objPaymentIndentBL.Bank_IFSC = txtIFSC.Text.Trim();
                if (rblBenType.SelectedValue != "Other")
                {
                    if (ddlPOWO.SelectedValue != "-Select-")
                    {
                        objPaymentIndentBL.POWO_ID = Convert.ToInt32(ddlPOWO.SelectedValue);
                    }
                    objPaymentIndentBL.WO_Type = ddlPOWO.SelectedItem.Text.Contains("/HO/") ? "HO" : "WO";
                }

                objPaymentIndentBL.Payment_Type = ddlPaymentType.SelectedItem.Text;
                objPaymentIndentBL.Payment_Mode = ddlPaymentMode.SelectedItem.Text;
                if (txtAmt_ServiceMaterial.Text != "")
                {
                    objPaymentIndentBL.Amt_ServiceMaterial = Convert.ToDecimal(txtAmt_ServiceMaterial.Text.Trim());
                }
                if (txtAmt_EarlierPayment.Text != "")
                {
                    objPaymentIndentBL.Amt_EarlierPayment = Convert.ToDecimal(txtAmt_EarlierPayment.Text.Trim());
                }
                if (txtTDSDeductedAmt.Text != "")
                {
                    objPaymentIndentBL.TDS_Amt = Convert.ToDecimal(txtTDSDeductedAmt.Text.Trim());
                }
                if (txtAmt_PartPayment.Text != "")
                {
                    objPaymentIndentBL.Amt_PartPayment = Convert.ToDecimal(txtAmt_PartPayment.Text.Trim());
                }
                if (ddlLedger.SelectedItem.Text != "- Select -")
                {
                    objPaymentIndentBL.Ledger_Name = ddlLedger.SelectedItem.Text;
                }

                objPaymentIndentBL.WorkDoneFor = txtWorkDoneFor.Text.Trim();
                objPaymentIndentBL.Service_Item_Supplied_Month = txtService_Item_Supplied_Month.Text.Trim();
                objPaymentIndentBL.Payment_Approval_Status = false;

                if (Request.QueryString["PageID"] == "11")
                {
                    objPaymentIndentBL.BalanceGrid = true;
                    objPaymentIndentBL.Status = "SendForApproval";
                    objPaymentIndentBL.TDS_Amt = 0;
                    objPaymentIndentBL.TDS_Perc = 0;
                    objPaymentIndentBL.Amt_PartPayment = Convert.ToDecimal(txtAmt_PartPayment.Text.ToString());
                }
                else
                {
                    objPaymentIndentBL.BalanceGrid = false;
                    objPaymentIndentBL.Status = "Draft";
                }
                if (btnSubmit.Text == "Send for Verification")
                {
                    objPaymentIndentBL.Task = "Insert_PaymentIndent";
                    if (objPaymentIndentBL.insert(con, PaymentIndentBL.eLoadSp.INSERT))
                    {
                        btnSubmit.Text = "Update";
                        lbtnPrint.Visible = true;
                        txtPayIndNo.Text = objPaymentIndentBL.PayInd_No.ToString();
                        ViewState["PayInd_No"] = objPaymentIndentBL.PayInd_No.ToString();
                        ViewState["PayInd_ID"] = Convert.ToInt32(objPaymentIndentBL.PayInd_ID);

                        UPLOADFILES();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Indent details has been inserted sucessfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to insert Payment Indent details !.');", true);
                    }
                }
                else
                {
                    objPaymentIndentBL.Task = "Update_PaymentIndent";
                    objPaymentIndentBL.PayInd_No = txtPayIndNo.Text.ToString();
                    if (btnSubmit.Text == "Update Indent")
                    {
                        objPaymentIndentBL.Status = "SendForApproval";
                    }
                    if (objPaymentIndentBL.update(con, PaymentIndentBL.eLoadSp.UPDATE))
                    {
                        UPLOADFILES();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Indent details has been updated sucessfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to Update Payment Indent details !.');", true);
                    }
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
            Response.Redirect("../Procurement/PaymentIndentList.aspx", false);
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







    protected void GetBeneficiaryDetails()
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.StateCode = ddlState.SelectedValue;
            objPaymentIndentBL.Project_Code = Session["Project_Code"].ToString();
            if (rblBenType.SelectedValue == "Vendor")
            {
                objPaymentIndentBL.Vendor_ID = ddlVendor.SelectedValue;
                objPaymentIndentBL.Task = "GetVendorDetails";
            }
            else if (rblBenType.SelectedValue == "SubContractor")
            {
                objPaymentIndentBL.SubCon_ID = ddlSubContractor.SelectedValue;
                objPaymentIndentBL.Task = "GetSubConDetails";
            }
            else
            {
                objPaymentIndentBL.Other_ID = ddlOther.SelectedValue;
                objPaymentIndentBL.Task = "GetOtherDetails";
            }
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.GET_BENEFICIARY_DETAIL, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblGSTRegd.Text = ds.Tables[0].Rows[0]["File_GSTRegistration"].ToString() != "" ? "YES" : "NO";
                lblPANCopy.Text = ds.Tables[0].Rows[0]["File_PANCopy"].ToString() != "" ? "YES" : "NO";
                lblBankDetails.Text = ds.Tables[0].Rows[0]["File_BankDetails"].ToString() != "" ? "YES" : "NO";

                lnkDownloadFile3.Text = ds.Tables[0].Rows[0]["File_GSTRegistration"].ToString();
                lnkDownloadFile4.Text = ds.Tables[0].Rows[0]["File_PANCopy"].ToString();
                lnkDownloadFile5.Text = ds.Tables[0].Rows[0]["File_BankDetails"].ToString();

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
            else
            {
                lnkDownloadFile3.Text = "";
                lnkDownloadFile4.Text = "";
                lnkDownloadFile5.Text = "";

                txtBank.Text = string.Empty;
                txtBranch.Text = string.Empty;
                txtAccNo.Text = string.Empty;
                txtIFSC.Text = string.Empty;
                txtLegalName.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void GetCompletedPayInd()
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.StateCode = ddlState.SelectedValue;
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

            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.GET_BENEFICIARY_DETAIL, ref ds);
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



    protected void GetEarlierPayment(string POWO_ID)
    {
        try
        {
            DataSet dsPayment = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.StateCode = ddlState.SelectedValue;
            objPaymentIndentBL.POWO_ID = Convert.ToInt32(POWO_ID);
            if (rblBenType.SelectedValue == "Vendor")
            {
                objPaymentIndentBL.Vendor_ID = ddlVendor.SelectedValue;
                objPaymentIndentBL.Task = "GetEarlierPayment_Vendor";
            }
            else if (rblBenType.SelectedValue == "SubContractor")
            {
                objPaymentIndentBL.SubCon_ID = ddlSubContractor.SelectedValue;
                objPaymentIndentBL.Task = "GetEarlierPayment_SubContractor";
            }
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.GET_POWO_DETAILS, ref dsPayment);
            if (!DBNull.Value.Equals(dsPayment.Tables[0].Rows[0]["Total_Payable"]))
            {
                Total_Payable = Convert.ToDecimal(dsPayment.Tables[0].Rows[0]["Total_Payable"]);

            }
            foreach (DataRow row in dsPayment.Tables[0].Rows)
            {
                int EarlierPaymentSingle = !string.IsNullOrEmpty(row["Amt_Transferable"].ToString()) ? Convert.ToInt32(row["Amt_Transferable"]) : 0;
                EarlierPayment = EarlierPayment + Convert.ToDecimal(EarlierPaymentSingle);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void GetPurchaseOrderDetails()
    {
        try
        {
            DataSet dsPO = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "PODetailsByPONo";
            objPaymentIndentBL.StateCode = ddlState.SelectedValue;
            objPaymentIndentBL.POWO_Number = ddlPOWO.SelectedItem.Text;
            //objPO.load(con, PurchaseOrderBL.eLoadSp.SELECT_PODETAILS_BY_PONO, ref dsPO);
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.GET_POWO_DETAILS, ref dsPO);
            if (dsPO.Tables[0].Rows.Count > 0)
            {
                TDSAmt = Convert.ToDecimal(dsPO.Tables[0].Rows[0]["TDSPerc"].ToString());
                BindPOItems();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void GetWorkOrderDetails()
    {
        try
        {
            DataSet dsWO = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "WODetailsByWONo";
            objPaymentIndentBL.StateCode = ddlState.SelectedValue;
            objPaymentIndentBL.POWO_Number = ddlPOWO.SelectedItem.Text;
            if (ddlPOWO.SelectedItem.Text.Contains("/HO/"))
            {
                objPaymentIndentBL.WO_Type = "HO";
            }
            else if (ddlPOWO.SelectedItem.Text.Contains("/WO/"))
            {
                objPaymentIndentBL.WO_Type = "WO";
            }
            else
            {
                objPaymentIndentBL.WO_Type = "PSO";
            }
            //objWO.Task = "GetWorkOrderDetails";
            //objWO.load(con, WorkOrderBL.eLoadSp.SELECT_WODETAILS_BY_WONO, ref ds);
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.GET_POWO_DETAILS, ref dsWO);
            if (dsWO.Tables[0].Rows.Count > 0)
            {
                TDSAmt = Convert.ToDecimal(dsWO.Tables[0].Rows[0]["TDSPercPrint"].ToString());
                BindWOItems();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void BindPOItems()
    {
        try
        {
            DataSet dsPOI = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "POItemDetailsByPOID1";
            objPaymentIndentBL.StateCode = ddlState.SelectedValue;
            objPaymentIndentBL.POWO_ID = Convert.ToInt32(ddlPOWO.SelectedValue);
            //objPO.load(con, PurchaseOrderBL.eLoadSp.SELECT_PO_ITEMS_BY_PONO_FOR_PRINT, ref dsPOI);
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.GET_POWO_DETAILS, ref dsPOI);
            foreach (DataRow row in dsPOI.Tables[0].Rows)
            {
                decimal ItemAmount = Convert.ToDecimal(row["Amount"]);
                TotalItemAmount += Convert.ToDecimal(ItemAmount);
            }

            dsPOI.Clear();
            dsPOI = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "POItemDetailsByPOID2";
            objPaymentIndentBL.StateCode = ddlState.SelectedValue;
            objPaymentIndentBL.POWO_ID = Convert.ToInt32(ddlPOWO.SelectedValue);
            //objPO.load(con, PurchaseOrderBL.eLoadSp.SELECT_PO_TAX_BY_POID_FOR_PRINT, ref dsPOI);
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.GET_POWO_DETAILS, ref dsPOI);
            foreach (DataRow row in dsPOI.Tables[0].Rows)
            {
                decimal TaxTotalAmount = Convert.ToDecimal(row["Type_Amount"]);
                TaxDeduction += Convert.ToDecimal(TaxTotalAmount);
            }

            if (TotalItemAmount > 0)
            {
                TDSAmtPercentage = Convert.ToDecimal(Convert.ToString(Math.Round((TotalItemAmount * Convert.ToDecimal(TDSAmt) / 100), 2)));
                txtAmt_ServiceMaterial.Text = Convert.ToString(TotalItemAmount);
                txtAmt_EarlierPayment.Text = EarlierPayment.ToString();
                txtAmt_PartPayment.Text = (Total_Payable - EarlierPayment).ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void BindWOItems()
    {
        try
        {
            DataSet dsWOI = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "WOItemDetailsByWOID";
            objPaymentIndentBL.StateCode = ddlState.SelectedValue;
            objPaymentIndentBL.POWO_ID = Convert.ToInt32(ddlPOWO.SelectedValue);
            if (ddlPOWO.SelectedItem.Text.Contains("/HO/"))
            {
                objPaymentIndentBL.WO_Type = "HO";
            }
            else if (ddlPOWO.SelectedItem.Text.Contains("/WO/"))
            {
                objPaymentIndentBL.WO_Type = "WO";
            }
            else
            {
                objPaymentIndentBL.WO_Type = "PSO";
            }
            //objWO.Task = "GetWOItemDetails";
            //objWO.load(con, WorkOrderBL.eLoadSp.SELECT_WO_ITEMS_BY_WONO_FOR_PRINT, ref ds);
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.GET_POWO_DETAILS, ref dsWOI);
            foreach (DataRow row in dsWOI.Tables[0].Rows)
            {
                int ItemAmount = !string.IsNullOrEmpty(row["Total_Amt"].ToString()) ? Convert.ToInt32(row["Total_Amt"]) : 0;
                TotalItemAmount = +Convert.ToDecimal(ItemAmount);
            }
            if (TotalItemAmount > 0)
            {
                TDSAmtPercentage = Convert.ToDecimal(Convert.ToString(Math.Round((TotalItemAmount * Convert.ToDecimal(TDSAmt) / 100), 2)));
                txtAmt_ServiceMaterial.Text = Convert.ToString(TotalItemAmount);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSavePaymentCategory_Click(object sender, EventArgs e)
    {
        try
        {
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.PaymentCategory_Name = txtPaymentCategory.Text.Trim();
            objPaymentIndentBL.Task = "Insert_PaymentCategory";
            if (objPaymentIndentBL.insertPaymentCategory(con, PaymentIndentBL.eLoadSp.INSERT_PAYMENTCATEGORY))
            {
                BindPaymentCategory();
                txtPaymentCategory.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Category Name has been added successfully.');", true);

                ModalPopupPaymentCategory.Show();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Payment Category_Name already exists.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancelPaymentCategory_Click(object sender, EventArgs e)
    {
        txtPaymentCategory.Text = string.Empty;
        ModalPopupPaymentCategory.Hide();
        //Response.Redirect("..//Master/Material.aspx", false);
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

                Grid_PaymentCategory.DataSource = ds;
                Grid_PaymentCategory.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }

    protected void Grid_PaymentCategory_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.PaymentCategory_ID = Convert.ToInt32(e.Record["PaymentCategory_ID"].ToString());
            objPaymentIndentBL.Task = "Delete_PaymentCategory";
            if (objPaymentIndentBL.delete(con, PaymentIndentBL.eLoadSp.DELETE_PAYMENTCATEGORY))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('PaymentCategory has been deleted sucessfully.');", true);
                BindPaymentCategory();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to Delete !');", true);
            }
            ModalPopupPaymentCategory.Show();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnHold_Click(object sender, EventArgs e)
    {
        try
        {

            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.PayInd_No = txtPayIndNo.Text;
            if (txtRemarks.Text.Trim() != string.Empty)
            {
                objPaymentIndentBL.Remarks = Convert.ToString(txtRemarks.Text);
            }
            if (txtAprovedAmount.Text.Trim() != string.Empty)
            {
                objPaymentIndentBL.Amt_Approved = Convert.ToDecimal(txtAprovedAmount.Text.Trim());
            }
            objPaymentIndentBL.Status = "Hold";
            objPaymentIndentBL.Task = "Update_PaymentIndent_Status";
            if (objPaymentIndentBL.updateStatus(con, PaymentIndentBL.eLoadSp.UPDATE_STATUS))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Indent Moved To Hold successfully');", true);
                GetPaymentIndentDetails();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to Approve Payment Indent !.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {

        try
        {
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.PayInd_No = txtPayIndNo.Text;
            if (txtRemarks.Text.Trim() != string.Empty)
            {
                objPaymentIndentBL.Remarks = Convert.ToString(txtRemarks.Text);
            }
            if (txtAprovedAmount.Text.Trim() != string.Empty)
            {
                objPaymentIndentBL.Amt_Approved = Convert.ToDecimal(txtAprovedAmount.Text.Trim());
                objPaymentIndentBL.Payment_Approved_Date = Convert.ToDateTime(DateTime.Now);
                objPaymentIndentBL.Status = "Approved";
                objPaymentIndentBL.Task = "Update_PaymentIndent_Status_Approve";
                if (objPaymentIndentBL.updateStatus(con, PaymentIndentBL.eLoadSp.UPDATE_STATUS))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Indent Approved successfully');", true);
                    GetPaymentIndentDetails();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to Approve Payment Indent !.');", true);
                }
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.PayInd_No = txtPayIndNo.Text.ToString();
            objPaymentIndentBL.Remarks = txtRemarks.Text.Trim();
            if (txtRemarks.Text.Trim() != string.Empty)
            {
                objPaymentIndentBL.Remarks = Convert.ToString(txtRemarks.Text);
            }
            objPaymentIndentBL.Task = "SendForReject";
            if (objPaymentIndentBL.update(con, PaymentIndentBL.eLoadSp.UPDATE))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Indent Rejected successfully');", true);
                GetPaymentIndentDetails();
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
    protected void btnModification_Click(object sender, EventArgs e)
    {
        try
        {
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.PayInd_No = txtPayIndNo.Text.ToString();
            objPaymentIndentBL.Remarks = txtRemarks.Text.Trim();
            if (txtRemarks.Text.Trim() != string.Empty)
            {
                objPaymentIndentBL.Remarks = Convert.ToString(txtRemarks.Text);
            }
            objPaymentIndentBL.Task = "SendForModification";
            if (objPaymentIndentBL.update(con, PaymentIndentBL.eLoadSp.UPDATE))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Indent sent for Modification.');", true);
                GetPaymentIndentDetails();
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

    protected void CheckIndentSuppliedPeriod_Changed(object sender, EventArgs e)
    {
        objPaymentIndentBL = new PaymentIndentBL();
        DataSet dsService = new DataSet();
        objPaymentIndentBL.Service_Items_Date_From = Convert.ToDateTime(txtService_Items_Date_From.Text);
        if (rblBenType.SelectedValue != "")
        {
            if (rblBenType.SelectedValue == "Vendor")
            {
                objPaymentIndentBL.Vendor_SubCon_ID = Convert.ToInt32(ddlVendor.SelectedValue.Trim());

            }
            else if (rblBenType.SelectedValue == "SubContractor")
            {
                objPaymentIndentBL.Vendor_SubCon_ID = Convert.ToInt32(ddlSubContractor.SelectedValue.Trim());

            }
            else if (rblBenType.SelectedValue == "Other")
            {
                objPaymentIndentBL.Vendor_SubCon_ID = Convert.ToInt32(ddlOther.SelectedValue.Trim());
            }
        }
        objPaymentIndentBL.POWO_ID = Convert.ToInt32(ddlPOWO.SelectedValue.Trim());

        objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.CHECK_INDENT_SUPPLIED_PERIOD, ref dsService);
        if (dsService.Tables[0].Rows.Count > 0)
        {
            txtService_Items_Date_From.Text = String.Empty;
            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Payment Indent Already Avaliable for the Date Period !.');", true);
        }
        else
        {
            objPaymentIndentBL.Service_Items_Date_From = Convert.ToDateTime(txtService_Items_Date_From.Text);
        }

    }
}
