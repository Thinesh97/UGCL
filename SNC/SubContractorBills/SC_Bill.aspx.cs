using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tally;
using TallyBridge;
using System.IO;
using BusinessLayer;
using System;
using SNC.ErrorLogger;

namespace SNC.SubContractorBills
{
    public partial class SC_Bill : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        SubContractorBillBL objSubContBL = null;
        ProjectBL objProjectBL = null;
        DataSet ds = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSubContractorList();
                BindProject();
                BindWoNumber();
                dpradd.Visible = false;
                Div_PaymentDetail.Visible = false;
                if (Request.QueryString["RA_Bill_No"] != null)
                {
                    ViewState["SC_Bill_No"] = Request.QueryString["RA_Bill_No"].ToString();
                    GetSC_Bill_Details(Request.QueryString["RA_Bill_No"]);
                    btnAddDPR.Visible = false;
                    Get_MIN();
                    Get_DPR_Details();
                    Get_Payment_Indent();
                    dpradd.Visible = true;
                    Div_PaymentDetail.Visible = true;
                    lbtnPrint.Visible = true;
                    Div_btnIssuedMaterial.Visible = true;
                    btnAddTax.Visible = true;
                    btnAddRetention.Visible = true;
                    GetSC_Bill_Details(objSubContBL.RA_Bill_No);
                    Bind_Grid_Tax();
                    BindGrid_Labour_List();
                }
            }
        }
        protected void BindWoNumber()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();

                objSubContBL.Task = "Get_WO_Number";
                objSubContBL.SubContractorID = Convert.ToString(ddlSubContractor.SelectedValue.ToString());
                objSubContBL.Project_Code = ddlProject.SelectedValue.ToString();
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_WO_NUMBER, ref ds);
                ddlWO.DataSource = ds;
                ddlWO.DataTextField = "WONo";
                ddlWO.DataValueField = "WO_ID";
                ddlWO.DataBind();
                ddlWO.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        private void GetSC_Bill_Details(string RA_Bill_NoRQ)
        {
            try
            {
                objSubContBL = new SubContractorBillBL();
                ds = new DataSet();
                objSubContBL.RA_Bill_No = RA_Bill_NoRQ;
                objSubContBL.Task = "GetSC_Bill_Details";
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.GET_SC_BILL_DETAIL, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    txtRA_BillNo.Text = ds.Tables[0].Rows[0]["RA_Bill_No"].ToString();
                    txtSCBillDate.Text = ds.Tables[0].Rows[0]["SC_Bill_Date"].ToString();
                    ddlProject.SelectedValue = ds.Tables[0].Rows[0]["Project_Code"].ToString();
                    ddlFYear.SelectedValue = ds.Tables[0].Rows[0]["Financial_Year"].ToString();
                    ddlSubContractor.SelectedValue = ds.Tables[0].Rows[0]["SubContractorID"].ToString();
                    ddlWO.SelectedItem.Text = ds.Tables[0].Rows[0]["WorkOrderNo"].ToString();
                    ViewState["WorkOrderNo_SC"] = ds.Tables[0].Rows[0]["WorkOrderNo"].ToString();
                    txtBillingPeriodFrom.Text = ds.Tables[0].Rows[0]["SC_BillingFrom_Date"].ToString();
                    txtBillingPeriodTo.Text = ds.Tables[0].Rows[0]["SC_Billing_To_Date"].ToString();
                    txtWorkDescription.Text = ds.Tables[0].Rows[0]["SC_Work_Description"].ToString();
                    hdnSC_Bill_ID.Value = ds.Tables[0].Rows[0]["SC_Bill_ID"].ToString();
                    if (ds.Tables[0].Rows[0]["DPR"].ToString() == "True")
                    {
                        Session["DPR_Value"] = "Selected";
                        Session["NMR_Value"] = "Not Selected";
                        CheckBoxDPR.Checked = true;
                        Div_DPRList.Visible = true;
                    }
                    else
                    {
                        CheckBoxDPR.Checked = false;
                        Div_DPRList.Visible = false;
                    }
                    if (ds.Tables[0].Rows[0]["NMR"].ToString() == "True")
                    {
                        CheckBoxNMR.Checked = true;
                        Div_NMRList.Visible = true;
                        Session["NMR_Value"] = "Selected";
                        Session["DPR_Value"] = "Not Selected";
                    }
                    else
                    {
                        CheckBoxNMR.Checked = false;
                        Div_NMRList.Visible = false;
                    }
                    btnSubmit.Text = "Update";
                    
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void BindSubContractorList()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_CONTRACTOR_ALL, ref ds);
                ddlSubContractor.DataSource = ds;
                ddlSubContractor.DataTextField = "Subcon_name";
                ddlSubContractor.DataValueField = "Subcon_ID";
                ddlSubContractor.DataBind();
                ddlSubContractor.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void ddlWO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSubContractor.SelectedIndex != 0)
                {
                    Get_WO_Details();
                }
                else
                {
                    //ddlWONo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void ddlSubContractor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSubContractor.SelectedIndex != 0)
                {
                    BindWoNumber();
                }
                else
                {
                    //ddlWONo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void btnSCBill_Click(object sender, EventArgs e)
        {
            try
            {
                objSubContBL = new SubContractorBillBL();
                objSubContBL.RA_Bill_No = txtRA_BillNo.Text;
                objSubContBL.SC_Bill_Date =Convert.ToDateTime(txtSCBillDate.Text);
                objSubContBL.Project_Code = ddlProject.SelectedValue;
                if (ddlWO.SelectedItem.Text!="-Select-")
                {
                    objSubContBL.SC_WONo = ddlWO.SelectedItem.Text;
                }
                objSubContBL.SubContractorID = ddlSubContractor.SelectedValue;
                objSubContBL.SC_BillingFrom_Date =Convert.ToDateTime(txtBillingPeriodFrom.Text);
                objSubContBL.SC_Billing_To_Date = Convert.ToDateTime(txtBillingPeriodTo.Text);
                objSubContBL.SC_Work_Description = txtWorkDescription.Text;
                if (CheckBoxNMR.Checked)
                {
                    objSubContBL.NMR = true;
                }
                else
                {
                    objSubContBL.NMR = false;
                }
                if (CheckBoxDPR.Checked)
                {
                    objSubContBL.DPR = true;
                }
                else
                {
                    objSubContBL.DPR = false;
                }
                objSubContBL.CGST = 0;
                objSubContBL.SGST = 0;
                objSubContBL.IGST = 0;
                objSubContBL.Retention = 0;
                objSubContBL.RetentionAmount = 0;
                //objSubContBL.RetentionType = ;
                objSubContBL.Financial_Year =ddlFYear.Text;
                objSubContBL.Task = "Insert_Sub_Contractor_Bill";
                if (btnSubmit.Text == "Submit")
                {

                    if (objSubContBL.insert_Sub_Contractor_Bill(con, SubContractorBillBL.eLoadSp.INSERT_SUB_CONTRACTOR_BILL))
                    {
                        GetSC_Bill_Details(objSubContBL.RA_Bill_No.ToString());
                        btnAddDPR.Visible = false;
                        Get_MIN();
                        Get_DPR_Details();
                        Get_Payment_Indent();
                        dpradd.Visible = true;
                        Div_PaymentDetail.Visible = true;
                        Div_btnIssuedMaterial.Visible = true;
                        btnAddDPR.Visible = false;
                        btnAddPayment.Visible = false;
                        btnIssuedMaterial.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('DPR details has been Update sucessfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to Update DPR details !.');", true);
                    }
                }
                else
                {
                    objSubContBL.SC_Bill_ID = hdnSC_Bill_ID.Value;
                    objSubContBL.Task = "Update_Sub_Contractor_Bill";
                    if (objSubContBL.Update_Sub_Contractor_Bill(con, SubContractorBillBL.eLoadSp.INSERT_SUB_CONTRACTOR_BILL))
                    {
                        GetSC_Bill_Details(objSubContBL.RA_Bill_No.ToString());
                        btnAddDPR.Visible = false;
                        Get_MIN();
                        Get_DPR_Details();
                        Get_Payment_Indent();
                        dpradd.Visible = true;
                        Div_PaymentDetail.Visible = true;
                        Div_btnIssuedMaterial.Visible = true;
                        btnAddDPR.Visible = false;
                        btnAddPayment.Visible = false;
                        btnIssuedMaterial.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('DPR details has been inserted sucessfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to insert DPR details !.');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
     
        protected void btnCancelWOItem_Click(object sender, EventArgs e)
        {
            //ClearItemPopup();
            //btnAddSubItem.Visible = false;
        }
        protected void btnAddDPR_Click(object sender, EventArgs e)
        {
            dpradd.Visible = true;
            Get_DPR_Details();
        }
        protected void btnAddPaymen_Click(object sender, EventArgs e)
        {
            Div_PaymentDetail.Visible = true;
            Get_Payment_Indent();
        }

        protected void btnAddMIN_Click(object sender, EventArgs e)
        {
            Div_btnIssuedMaterial.Visible = true;
            Get_MIN();
        }
        protected void Grid_MIN_UpdateCommand(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            try
            {

                objSubContBL = new SubContractorBillBL();
                objSubContBL.MIN_Item_ID = e.Record["MIN_Item_ID"].ToString();
                if (e.Record["Rate"].ToString() != "")
                {
                    objSubContBL.MIN_Item_Rate = Convert.ToDecimal(e.Record["Rate"].ToString());
                }
               
                    objSubContBL.Task = "Update_MIN_Rate";
                    if (objSubContBL.Update_MIN_Rate(con, SubContractorBillBL.eLoadSp.INSERT_SUB_CONTRACTOR_BILL))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render(' Rate has been added sucessfully.');", true);
                        ClearTax();
                        Get_MIN();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Invalid Rate');", true);
                    }
                }
                      catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
            }
        }
        protected void btnCancelTax_Click(object sender, EventArgs e)
        {
            ClearTax();
            btnSaveTax.Text = "UpdateTax";
            ModelTaxPopup.Hide();
        }

      
        protected void btnSaveTax_Click(object sender, EventArgs e)
        {
            try
            {
                objSubContBL = new SubContractorBillBL();
                objSubContBL.SC_Bill_ID= hdnSC_Bill_ID.Value;

                if (txtIgstPercPO.Text !="")
                {

                    objSubContBL.IGST = Convert.ToDecimal(txtIgstPercPO.Text.Trim());
                    //objPO.Igst_Amt = Convert.ToDecimal(txtTotal.Text.Trim()) * Convert.ToDecimal(txtIgstPercPO.Text.Trim()) / 100;
                }
                 if (txtCgstPercPO.Text != "")
                {

                    objSubContBL.CGST = Convert.ToDecimal(txtCgstPercPO.Text.Trim());
                    //objPO.Cgst_Amt = Convert.ToDecimal(txtTotal.Text.Trim()) * Convert.ToDecimal(txtCgstPercPO.Text.Trim()) / 100;
                }
                 if (txtSgstPercPO.Text != "")
                {

                    objSubContBL.SGST = Convert.ToDecimal(txtSgstPercPO.Text.Trim());
                    //objPO.Sgst_Amt = Convert.ToDecimal(txtTotal.Text.Trim()) * Convert.ToDecimal(txtSgstPercPO.Text.Trim()) / 100;
                }
                 if (txtTDSperc.Text != "")
                {

                    objSubContBL.TDS = Convert.ToDecimal(txtTDSperc.Text.Trim());
                    //objPO.Sgst_Amt = Convert.ToDecimal(txtTotal.Text.Trim()) * Convert.ToDecimal(txtSgstPercPO.Text.Trim()) / 100;
                }
                if (btnSaveTax.Text == "UpdateTax")
                {
                    objSubContBL.Task = "Update_Sub_Contractor_Bill_Tax";
                    if (objSubContBL.insert_Sub_Contractor_Bill_Tax(con, SubContractorBillBL.eLoadSp.INSERT_SUB_CONTRACTOR_BILL))
                    
                    {
                        Bind_Grid_Tax();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render(' Tax details has been added sucessfully.');", true);
                        ClearTax();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Entry already exists for this PO.');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void rbtntype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtntype.SelectedValue == "IGST")
                {
                    div_Igst.Visible = true;
                    div_Cgst.Visible = false;
                    div_Sgst.Visible = false;
                    div_TDS.Visible = false;
                  
                }
                else if (rbtntype.SelectedValue == "CGST")
                {
                    div_Igst.Visible = false;
                    div_Cgst.Visible = true;
                    div_Sgst.Visible = false;
                    div_TDS.Visible = false;
                }
                else if (rbtntype.SelectedValue == "SGST")
                {
                    div_Igst.Visible = false;
                    div_Cgst.Visible = false;
                    div_Sgst.Visible = true;
                    div_TDS.Visible = false;
                }
                else if (rbtntype.SelectedValue == "TDS")
                {
                    div_Igst.Visible = false;
                    div_Cgst.Visible = false;
                    div_Sgst.Visible = false;
                    div_TDS.Visible = true;
                }
                ModelTaxPopup.Show();
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void btnAddTax_Click(object sender, EventArgs e)
        {

            ModelTaxPopup.Show();
            btnSaveTax.Text = "UpdateTax";
            ClearTax();
        }
        protected void btnCancelRetention_Click(object sender, EventArgs e)
        {
            ModelRetentionPopup.Hide();
            btnSaveRetention.Text = "Update_Retention";
            ClearRetention();
        }
        protected void btnSaveRetention_Click(object sender, EventArgs e)
        {
            try
            {
                objSubContBL = new SubContractorBillBL();
                objSubContBL.SC_Bill_ID= hdnSC_Bill_ID.Value;
                objSubContBL.RetentionType = rblRetention.SelectedValue;
                if (rblRetention.SelectedValue == "Percentage" )
                {
                    objSubContBL.RetentionPerc = Convert.ToDecimal(txtRetentionPrec.Text.Trim());
                   
                }
                else if (rblRetention.SelectedValue == "Amount") 
                {
                    objSubContBL.RetentionAmount = Convert.ToDecimal(txtRetentioninAmont.Text.Trim());
                }

                if (btnSaveRetention.Text == "Update_Retention")
                {
                    objSubContBL.Task = "Update_Retention";
                    if (objSubContBL.insert_Sub_Contractor_Bill_Retention(con, SubContractorBillBL.eLoadSp.INSERT_SUB_CONTRACTOR_BILL))
                    
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Retention details has been added sucessfully.');", true);
                        ClearRetention();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Entry already exists for this SC Bill.');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void btnAddRetention_Click(object sender, EventArgs e)
        {
            ModelRetentionPopup.Show();
            btnSaveRetention.Text = "Update_Retention";
            ClearRetention();
        }
        protected void ClearRetention()
        {
            txtRetentioninAmont.Text = null;
            txtRetentionPrec.Text = null;
        }
        protected void ClearTax()
        {
            txtIgstPercPO.Text = null;

            txtCgstPercPO.Text = null;

            txtSgstPercPO.Text = null;
            txtTDSperc.Text = null;
           
        }


        protected void Bind_Grid_Tax()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();
                objSubContBL.Task = "GetTax_SC";
                objSubContBL.RA_Bill_No =Convert.ToString(ViewState["SC_Bill_No"]);
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.GETTAX_SC, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Grid_Tax.DataSource = ds;
                    Grid_Tax.DataBind();

                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void Get_MIN()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();
                objSubContBL.Task = "Get_MIN_SC";
                objSubContBL.BillingFrom_Date = Convert.ToDateTime(txtBillingPeriodFrom.Text);
                objSubContBL.Billing_To_Date = Convert.ToDateTime(txtBillingPeriodTo.Text);
                objSubContBL.SubContractorID = Convert.ToString(ddlSubContractor.SelectedValue.ToString());
                objSubContBL.WONo = Convert.ToString(ddlWO.SelectedItem.Text.ToString());
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_PAYMENT_INDENT, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Grid_MIN.DataSource = ds;
                    Grid_MIN.DataBind();

                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void Get_Payment_Indent()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();
                objSubContBL.Task = "Get_Payment_Indent";
                objSubContBL.BillingFrom_Date = Convert.ToDateTime(txtBillingPeriodFrom.Text);
                objSubContBL.Billing_To_Date = Convert.ToDateTime(txtBillingPeriodTo.Text);
                objSubContBL.SubContractorID = Convert.ToString(ddlSubContractor.SelectedValue.ToString());
                Session["ddlSubContractor"] = Convert.ToString(ddlSubContractor.SelectedValue.ToString());
                objSubContBL.WONo = Convert.ToString(ddlWO.SelectedItem.Text.ToString());
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_PAYMENT_INDENT, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Grid_Payment_Indent.DataSource = ds;
                    Grid_Payment_Indent.DataBind();

                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void Grid_SC_MIN()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();
                objSubContBL.Task = "Get_Payment_Indent";
                objSubContBL.BillingFrom_Date = Convert.ToDateTime(txtBillingPeriodFrom.Text);
                objSubContBL.Billing_To_Date = Convert.ToDateTime(txtBillingPeriodTo.Text);
                objSubContBL.SubContractorID = Convert.ToString(ddlSubContractor.SelectedValue.ToString());
                objSubContBL.WONo = Convert.ToString(ddlWO.SelectedItem.Text.ToString());
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_PAYMENT_INDENT, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Grid_Payment_Indent.DataSource = ds;
                    Grid_Payment_Indent.DataBind();

                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void Get_DPR_Details()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();
                objSubContBL.Task = "Get_DPR_Details";
                objSubContBL.BillingFrom_Date  =Convert.ToDateTime(txtBillingPeriodFrom.Text);
                Session["txtBillingPeriodFrom"] = Convert.ToDateTime(txtBillingPeriodFrom.Text);
                objSubContBL.Billing_To_Date = Convert.ToDateTime(txtBillingPeriodTo.Text);
                Session["txtBillingPeriodTo"] = Convert.ToDateTime(txtBillingPeriodTo.Text);
                objSubContBL.WONo = Convert.ToString(ddlWO.SelectedValue);
                Session["ddlWO"] = Convert.ToString(ddlWO.SelectedValue);
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_DPR_DETAILS, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Grid_DPR.DataSource = ds;
                    Grid_DPR.DataBind();

                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void Get_WO_Details()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();

                objSubContBL.Task = "Get_WO_Details";
                objSubContBL.WONo = Convert.ToString(ddlWO.SelectedItem.Text.ToString());
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_WO_NUMBER, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtWorkDescription.Text=ds.Tables[0].Rows[0]["Item_Desc"].ToString();
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void Grid_DPR_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            //try
            //{

            //    objIndentBL = new IndentBL();
            //    objIndentBL.Indent_No = Convert.ToString(e.Record["Indent_No"].ToString());
            //    if (objIndentBL.delete(con, IndentBL.eLoadSp.DELETE_INDENT_BY_INDENTNO))
            //    {
            //        BindIndentList();
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Indent has been  deleted sucessfully');", true);
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete the indent because it use some where');", true);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            //}

        }
        protected void Grid_DPR_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
        {
            //try
            //{
            //    if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            //    {
            //        HyperLink Editlink = e.Row.Cells[0].FindControl("lnkIndentNo") as HyperLink;

            //        Editlink.NavigateUrl = "~/Procurement/Indent.aspx?IndentNo=" + Editlink.Text;

            //        if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
            //        {
            //            if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
            //            {
            //                if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Ind_Update"].ToString()))
            //                {
            //                    Editlink.NavigateUrl = "";
            //                }
            //                if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Ind_Delete"].ToString()))
            //                {
            //                    Grid_Indent.Columns[9].Visible = false;
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            //}
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("SC_BillPrint.aspx?Bill_No=" + ViewState["SC_Bill_No"].ToString(), false);
                
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void BindGrid_Labour_List()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();
                objSubContBL.BillingFrom_Date =Convert.ToDateTime(txtBillingPeriodFrom.Text);
                objSubContBL.Billing_To_Date = Convert.ToDateTime(txtBillingPeriodTo.Text);
                Session["txtBillingPeriodFrom"] = Convert.ToDateTime(txtBillingPeriodFrom.Text);
                Session["txtBillingPeriodTo"] = Convert.ToDateTime(txtBillingPeriodTo.Text);
                objSubContBL.Task = "SelectAll_Labour_By_NMR_ID_Print";
                objSubContBL.WONo = Convert.ToString(ddlWO.SelectedItem.Text);
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SUB_CONTRACTOR_BILL_LABOUR_LIST, ref ds);
                Grid_LabourList.DataSource = ds;
                Grid_LabourList.DataBind();
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
    }
}