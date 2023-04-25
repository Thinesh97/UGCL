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

namespace SNC.Procurement
{
    public partial class BankIndent : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        VendorBL objVendorBL = null;
        SubContractorBL objSubContractorBL = null;
        OtherBL objOtherBL = null;
        ProjectBL objProjectBL = null;
        PaymentIndentBL objPaymentIndentBL = null;
        IndentBL objIndent = null;
        DataSet ds = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindState();
                BindBank();
                BindPaymentCatagory();
                BindPaymentCatagoryddl();
                BindPaymentSubCatddl();
                BindPaymentSubcatagory();
                BindPaymentTowards();

                if (Request.QueryString["BnkInd_No"] != null)
                {
                    GetBankIndentDetails();
                    btnSubmit.Text = "Update";
                }
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
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void BindBank()
        {
            try
            {
                ds = new DataSet();
                objVendorBL = new VendorBL();
                objVendorBL.load(con, VendorBL.eLoadSp.SELECT_BANK_LIST_PAYMENT_IND, ref ds);
                ddlbank.DataSource = ds;
                ddlbank.DataTextField = "Bank_Name";
                ddlbank.DataValueField = "ID";
                ddlbank.DataBind();
                ddlbank.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }


        protected void BindPaymentCatagory()
        {
            try
            {
                objProjectBL = new ProjectBL();
                DataSet ds = new DataSet();
                objProjectBL.txtpaymentcat = txtpaymentcat.Text;
                objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PAYMENT_CATAGORY_ALL, ref ds);
                Grid_PaymentTerms.DataSource = ds;
                Grid_PaymentTerms.DataBind();

                ddlpaymentcat.Items.Clear();
                ddlpaymentcat.DataSource = ds;
                ddlpaymentcat.DataValueField = "PAYMENT_ID";
                ddlpaymentcat.DataTextField = "PAYMENT_TERMS";
                ddlpaymentcat.DataBind();
                ddlpaymentcat.Items.Insert(0, "-Select-");

            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void BindPaymentCatagoryddl()
        {
            try
            {
                objProjectBL = new ProjectBL();
                DataSet ds = new DataSet();
                objProjectBL.txtpaymentcat = txtpaymentcat.Text;
                objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PAYMENT_CATAGORY_ALL, ref ds);
                ddlpaycat.Items.Clear();
                ddlpaycat.DataSource = ds;
                ddlpaycat.DataValueField = "PAYMENT_ID";
                ddlpaycat.DataTextField = "PAYMENT_TERMS";
                ddlpaycat.DataBind();
                ddlpaycat.Items.Insert(0, "-Select-");

            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void BindPaymentSubCatddl()
        {
            try
            {
                objProjectBL = new ProjectBL();
                DataSet ds = new DataSet();
                objProjectBL.txtpaymentsubcat = txtpaymentsubcat.Text;

                objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PAYMENT_SUB_CATAGORY_ALL, ref ds);
                ddlpaysubcat.Items.Clear();
                ddlpaysubcat.DataSource = ds;
                ddlpaysubcat.DataValueField = "PAYMENT_SUB_ID";
                ddlpaysubcat.DataTextField = "PAYMENT_CATAGORY_SUB";
                ddlpaysubcat.DataBind();
                ddlpaysubcat.Items.Insert(0, "-Select-");

            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void BindPaymentSubcatagory()
        {
            try
            {

                if (ddlpaymentcat.SelectedItem.Value != "" && ddlpaymentcat.SelectedItem.Value != "-Select-")
                {
                    objProjectBL = new ProjectBL();
                    DataSet ds = new DataSet();
                    objProjectBL.txtpaymentsubcat = txtpaymentsubcat.Text;

                    objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PAYMENT_SUB_CATAGORY_ALL, ref ds);
                    Grid_PaymentSubCat.DataSource = ds;
                    Grid_PaymentSubCat.DataBind();


                    ddlpaymentsubcat.Items.Clear();
                    ddlpaymentsubcat.Items.Add("-Select-");


                    objProjectBL = new ProjectBL();
                    objProjectBL.PAYMENT_ID = ddlpaymentcat.SelectedItem.Value;
                    objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PAYMENT_CATAGORY_DROPDOWN, ref ds);

                    ddlpaymentsubcat.Items.Clear();
                    ddlpaymentsubcat.DataSource = ds;
                    ddlpaymentsubcat.DataValueField = "PAYMENT_SUB_ID";
                    ddlpaymentsubcat.DataTextField = "PAYMENT_CATAGORY_SUB";

                    ddlpaymentsubcat.DataBind();
                    //ddlpaymentsubcat.Items.Clear();
                    //ddlpaymentsubcat.DataSource = ds;
                    //ddlpaymentsubcat.DataValueField = "PAYMENT_SUB_ID";
                    //ddlpaymentsubcat.DataTextField = "PAYMENT_CATAGORY_SUB";
                    //ddlpaymentsubcat.DataBind();
                    //ddlpaymentsubcat.Items.Insert(0, "-Select-");
                }
                else
                {
                    ddlpaymentsubcat.Items.Insert(0, "-Select-");
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }


        protected void ddlpaymentcat_SelectedIndexChanged1(object sender, EventArgs e)
        {
            ddlpaymentsubcat.Items.Clear();
            ddlpaymentsubcat.Items.Add("-Select-");


            objProjectBL = new ProjectBL();
            DataSet ds = new DataSet();
            objProjectBL.PAYMENT_ID = ddlpaymentcat.SelectedItem.Value;
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PAYMENT_CATAGORY_DROPDOWN, ref ds);

            ddlpaymentsubcat.Items.Clear();
            ddlpaymentsubcat.DataSource = ds;
            ddlpaymentsubcat.DataValueField = "PAYMENT_SUB_ID";
            ddlpaymentsubcat.DataTextField = "PAYMENT_CATAGORY_SUB";

            ddlpaymentsubcat.DataBind();
            //ddlpaymentsubcat.Items.Insert(0, "-Select-");
        }

        protected void ddlpaymentsubcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlpaymenttowards.Items.Clear();
            ddlpaymenttowards.Items.Add("-Select-");


            objProjectBL = new ProjectBL();
            DataSet ds = new DataSet();
            objProjectBL.PAYMENT_SUB_ID = ddlpaymentsubcat.SelectedItem.Value;
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PAYMENT_SUBCATAGORY_DROPDOWN, ref ds);

            ddlpaymenttowards.Items.Clear();
            ddlpaymenttowards.DataSource = ds;
            ddlpaymenttowards.DataValueField = "PAYMENT_TOWARDS_ID";
            ddlpaymenttowards.DataTextField = "PAYMENT_CATAGORY_TOWARDS";

            ddlpaymenttowards.DataBind();

        }

        protected void BindPaymentTowards()
        {
            try
            {
                if (ddlpaymentsubcat.SelectedItem.Value != "" && ddlpaymentsubcat.SelectedItem.Value != "-Select-")
                {
                    objProjectBL = new ProjectBL();
                    DataSet ds = new DataSet();
                    objProjectBL.txtpaymentTowards = txtpaymentTowards.Text;
                    objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PAYMENT_CATAGORY_TOWARDS_ALL, ref ds);
                    Grid_PyamentTowards.DataSource = ds;
                    Grid_PyamentTowards.DataBind();

                    ddlpaymenttowards.Items.Clear();
                    ddlpaymenttowards.Items.Add("-Select-");


                    objProjectBL = new ProjectBL();

                    objProjectBL.PAYMENT_SUB_ID = ddlpaymentsubcat.SelectedItem.Value;
                    objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PAYMENT_SUBCATAGORY_DROPDOWN, ref ds);

                    ddlpaymenttowards.Items.Clear();
                    ddlpaymenttowards.DataSource = ds;
                    ddlpaymenttowards.DataValueField = "PAYMENT_TOWARDS_ID";
                    ddlpaymenttowards.DataTextField = "PAYMENT_CATAGORY_TOWARDS";

                    ddlpaymenttowards.DataBind();

                    //ddlpaymenttowards.Items.Clear();
                    //ddlpaymenttowards.DataSource = ds;
                    //ddlpaymenttowards.DataValueField = "PAYMENT_TOWARDS_ID";
                    //ddlpaymenttowards.DataTextField = "PAYMENT_CATAGORY_TOWARDS";
                    //ddlpaymenttowards.DataBind();
                    //ddlpaymenttowards.Items.Insert(0, "-Select-");

                }
                else
                {
                    ddlpaymenttowards.Items.Insert(0, "-Select-");
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlState.SelectedItem.Text != "-Select-")
                {
                    BindProjectList();
                }
                else
                {
                    ddlProject.SelectedValue = "-Select-";
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.ErrorLogger.logError(ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void ddlbank_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlbank.SelectedItem.Text != "-Select-")
                {
                    ds = new DataSet();
                    objVendorBL = new VendorBL();
                    ds = new DataSet();
                    objVendorBL.BankID = Convert.ToInt32(ddlbank.SelectedValue);
                    objVendorBL.load(con, VendorBL.eLoadSp.TB_BANK_SELECT_ALL_DETAIL_BY_ID, ref ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtbank.Text = ds.Tables[0].Rows[0]["Bank_Name"].ToString();
                        txtbranch.Text = ds.Tables[0].Rows[0]["Branch"].ToString();
                        txtaccountno.Text = ds.Tables[0].Rows[0]["Account_No"].ToString();
                        txtisfc.Text = ds.Tables[0].Rows[0]["IFSC"].ToString();
                    }
                }
                else
                {
                    txtbank.Text = "-Select-";
                    txtbranch.Text = string.Empty;
                    txtaccountno.Text = string.Empty;
                    txtisfc.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        private void GetBankIndentDetails()
        {
            try
            {
                objPaymentIndentBL = new PaymentIndentBL();
                DataSet dsDetails = new DataSet();
                objPaymentIndentBL.BANK_INDENT_No = Request.QueryString["BnkInd_No"].ToString();
                objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_BANKINDENT_ALL_BY_BNKIND_ID, ref dsDetails);
                if (dsDetails.Tables[0].Rows.Count > 0)
                {
                    txtBankIndNo.Text = dsDetails.Tables[0].Rows[0]["BANK_INDENT_No"].ToString();
                    txtBankIndDate.Text = dsDetails.Tables[0].Rows[0]["INDENT_Date"].ToString();
                    ddlState.SelectedValue = dsDetails.Tables[0].Rows[0]["State_Code"].ToString();
                    ddlState_SelectedIndexChanged(null, null);
                    ddlProject.SelectedValue = dsDetails.Tables[0].Rows[0]["Project_Code"].ToString();
                    ddlbank.SelectedValue = dsDetails.Tables[0].Rows[0]["BANK_NAME"].ToString();
                    ddlProject.SelectedValue = dsDetails.Tables[0].Rows[0]["FYear"].ToString();
                    txtbank.Text = dsDetails.Tables[0].Rows[0]["BRANCH_NAME"].ToString();
                    txtbranch.Text = dsDetails.Tables[0].Rows[0]["BRANCH_NAME"].ToString();
                    txtbank.Text = ddlbank.SelectedItem.Text;
                    txtaccountno.Text = dsDetails.Tables[0].Rows[0]["ACCOUNT_NUMBER"].ToString();
                    txtisfc.Text = dsDetails.Tables[0].Rows[0]["ISFC"].ToString();
                    rblpaytype.SelectedValue = dsDetails.Tables[0].Rows[0]["PAYMENT_TYPE"].ToString();
                    ddlpaymentcat.SelectedValue = dsDetails.Tables[0].Rows[0]["PAYMENT_CATAGARY"].ToString();
                    ddlpaymentsubcat.SelectedValue = dsDetails.Tables[0].Rows[0]["PAYMENT_SUB_CATAGARY"].ToString();
                    ddlpaymenttowards.SelectedValue = dsDetails.Tables[0].Rows[0]["TOWARDS"].ToString();
                    txtrefdetails.Text = dsDetails.Tables[0].Rows[0]["REFDETAILS"].ToString();
                    txtNarration.Text = dsDetails.Tables[0].Rows[0]["NARRATION"].ToString();
                    txtrateofinterest.Text = dsDetails.Tables[0].Rows[0]["RATE_OF_INTEREST"].ToString();
                    txtamount.Text = dsDetails.Tables[0].Rows[0]["AMOUNT"].ToString();

                    txtCGSTperc.Text = dsDetails.Tables[0].Rows[0]["CGSTPerc"].ToString();
                    txtSGSTperc.Text = dsDetails.Tables[0].Rows[0]["SGSTPerc"].ToString();
                    txtIGSTperc.Text = dsDetails.Tables[0].Rows[0]["IGSTPerc"].ToString();
                    txtCGSTamt.Text = dsDetails.Tables[0].Rows[0]["CGSTAmt"].ToString();
                    txtSGSTamt.Text = dsDetails.Tables[0].Rows[0]["SGSTAmt"].ToString();
                    txtIGSTamt.Text = dsDetails.Tables[0].Rows[0]["IGSTAmt"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }


        protected void btnsavepaymentcat_Click(object sender, EventArgs e)
        {
            try
            {
                objProjectBL = new ProjectBL();
                objProjectBL.txtpaymentcat = txtpaymentcat.Text;




                if (btnsavepaymentcat.Text == "Save")
                {
                    if (objProjectBL.PaymentTerminsert(con, ProjectBL.eLoadSp.INSERT_PAYMENT_TERMS))
                    {
                        BindPaymentCatagory();
                        BindPaymentCatagoryddl();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Category has been Created');", true);
                        txtpaymentcat.Text = string.Empty;
                        //txtProjectTypeCode.Text = string.Empty;
                        ModelPaymentPopup.Show();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Payment Category is already exists, Pls try another');", true);
                    }
                }
                else
                {
                    objProjectBL = new ProjectBL();
                    objProjectBL.txtpaymentcat = txtpaymentcat.Text;
                    //ViewState["TYPEID"] = Convert.ToInt16(((LinkButton)sender).CommandName.ToString());
                    objProjectBL.PAYMENT_ID = Convert.ToInt16(ViewState["TYPEID"]).ToString();
                    if (objProjectBL.Paymentcat(con, ProjectBL.eLoadSp.UPDATE_PAYMENT_CAT_BY_ID))
                    {
                        BindPaymentCatagory();
                        txtpaymentcat.Text = string.Empty;
                        //txtProjectTypeCode.Text = string.Empty;
                        btnsavepaymentcat.Text = "Save";
                        ModelPaymentPopup.Show();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Category Details has been updated successfully.');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void btnCancelpayment_Click(object sender, EventArgs e)
        {
            txtpaymentcat.Text = string.Empty;

        }

        protected void Grid_PaymentTerms_DeleteCommand(object sender, GridRecordEventArgs e)
        {
            try
            {
                ds = new DataSet();
                objProjectBL = new ProjectBL();
                //objProjectBL.Task = "Delete_payment_Terms";
                objProjectBL.PAYMENT_ID = Convert.ToInt32(e.Record["PAYMENT_ID"]).ToString();

                if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_PAYMENT_TERMS))
                {
                    BindPaymentCatagory();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Terms has been deleted sucessfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Payment Terms cannot be deleted as it is already in use.');", true);
                }
            }
            catch (Exception ex)
            {
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }

        }

        protected void lnkbtnpayterms_Click(object sender, EventArgs e)
        {
            try
            {
                objProjectBL = new ProjectBL();
                ViewState["TYPEID"] = Convert.ToInt16(((LinkButton)sender).CommandName.ToString());
                objProjectBL.PAYMENT_ID = Convert.ToInt16(ViewState["TYPEID"]).ToString();
                //objProjectBL.Task = "Select_Payment_Terms_by_ID";
                if (objProjectBL.loadPC(con, ProjectBL.eLoadSp.SELECT_PAYMENT_TERMS_BY_PAYMENTTERMS_ID))
                {
                    txtpaymentcat.Text = objProjectBL.txtpaymentcat.ToString();

                    btnsavepaymentcat.Text = "Update";
                    ModelPaymentPopup.Show();
                }
            }
            catch (Exception ex)
            {
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void btnpaymentsubcat_Click(object sender, EventArgs e)
        {
            try
            {
                objProjectBL = new ProjectBL();
                objProjectBL.txtpaymentsubcat = txtpaymentsubcat.Text;
                objProjectBL.paymentddl = Convert.ToInt32(ddlpaycat.SelectedItem.Value);




                if (btnpaymentsubcat.Text == "Save")
                {
                    if (objProjectBL.PaymentTermSubinsert(con, ProjectBL.eLoadSp.INSERT_PAYMENT_CATAGORY_SUB))
                    {
                        BindPaymentSubcatagory();
                        BindPaymentSubCatddl();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Sub Category has been Created');", true);
                        txtpaymentsubcat.Text = string.Empty;
                        //txtProjectTypeCode.Text = string.Empty;
                        ModalPaymentSubCatPopup.Show();
                        //BindPaymentCatagoryddl();

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Payment Sub Category is already exists, Pls try another');", true);
                    }
                }
                else
                {
                    objProjectBL = new ProjectBL();
                    objProjectBL.txtpaymentsubcat = txtpaymentsubcat.Text;
                    // ViewState["TYPEID"] = Convert.ToInt16(((LinkButton)sender).CommandName.ToString());
                    objProjectBL.PAYMENT_SUB_ID = Convert.ToInt16(ViewState["TYPEID"]).ToString();
                    if (objProjectBL.Paymentcatsub(con, ProjectBL.eLoadSp.UPDATE_PAYMENT_CAT_SUB_BY_ID))
                    {
                        BindPaymentSubcatagory();
                        txtpaymentcat.Text = string.Empty;
                        //txtProjectTypeCode.Text = string.Empty;
                        btnsavepaymentcat.Text = "Save";
                        ModalPaymentSubCatPopup.Show();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Sub Category Details has been updated successfully.');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void btnpaymentsubcatcancel_Click(object sender, EventArgs e)
        {
            txtpaymentsubcat.Text = string.Empty;

        }

        protected void lnkbtnpaySubCatagory_Click(object sender, EventArgs e)
        {
            try
            {
                objProjectBL = new ProjectBL();
                ViewState["TYPEID"] = Convert.ToInt16(((LinkButton)sender).CommandName.ToString());
                objProjectBL.PAYMENT_SUB_ID = Convert.ToInt16(ViewState["TYPEID"]).ToString();
                //objProjectBL.Task = "Select_Payment_Terms_by_ID";
                if (objProjectBL.loadPSC(con, ProjectBL.eLoadSp.SELECT_PAYMENT_SUB_CATGORY_BY_PAYMENTTERMS_ID))
                {
                    txtpaymentsubcat.Text = objProjectBL.txtpaymentsubcat.ToString();

                    btnpaymentsubcat.Text = "Update";
                    ModalPaymentSubCatPopup.Show();
                }
            }
            catch (Exception ex)
            {
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void Grid_PaymentSubCat_DeleteCommand(object sender, GridRecordEventArgs e)
        {
            try
            {
                ds = new DataSet();
                objProjectBL = new ProjectBL();
                //objProjectBL.Task = "Delete_payment_Terms";
                objProjectBL.PAYMENT_SUB_ID = Convert.ToInt32(e.Record["PAYMENT_SUB_ID"]).ToString();

                if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_PAYMENT_SUB_CAT))
                {
                    BindPaymentSubcatagory();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Sub Catagory has been deleted sucessfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Payment Sub Catagory cannot be deleted as it is already in use.');", true);
                }
            }
            catch (Exception ex)
            {
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }

        }

        protected void btnsavetowards_Click(object sender, EventArgs e)
        {
            try
            {
                objProjectBL = new ProjectBL();
                objProjectBL.txtpaymentTowards = txtpaymentTowards.Text;
                objProjectBL.paymentsubddl = Convert.ToInt32(ddlpaysubcat.SelectedItem.Value);



                if (btnsavetowards.Text == "Save")
                {
                    if (objProjectBL.PaymentcatTowardsinsert(con, ProjectBL.eLoadSp.INSERT_PAYMENT_CATAGORY_TOWARDS))
                    {
                        BindPaymentTowards();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Category Towards has been Created');", true);
                        txtpaymentTowards.Text = string.Empty;
                        //txtProjectTypeCode.Text = string.Empty;
                        ModalPaymentTowardsPopup.Show();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Payment Category Towards is already exists, Pls try another');", true);
                    }
                }
                else
                {
                    objProjectBL = new ProjectBL();
                    objProjectBL.txtpaymentTowards = txtpaymentTowards.Text;
                    // ViewState["TYPEID"] = Convert.ToInt16(((LinkButton)sender).CommandName.ToString());
                    objProjectBL.PAYMENT_TOWARDS_ID = Convert.ToInt16(ViewState["TYPEID"]).ToString();
                    if (objProjectBL.Paymentcattowards(con, ProjectBL.eLoadSp.UPDATE_PAYMENT_CAT_TOWARDS_BY_ID))
                    {
                        BindPaymentTowards();

                        txtpaymentTowards.Text = string.Empty;
                        //txtProjectTypeCode.Text = string.Empty;
                        btnsavetowards.Text = "Save";
                        ModalPaymentTowardsPopup.Show();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Category Towards Details has been updated successfully.');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void btncanceltowards_Click(object sender, EventArgs e)
        {
            txtpaymentTowards.Text = string.Empty;
        }

        protected void Grid_PyamentTowards_DeleteCommand(object sender, GridRecordEventArgs e)
        {
            try
            {
                ds = new DataSet();
                objProjectBL = new ProjectBL();
                //objProjectBL.Task = "Delete_payment_Terms";
                objProjectBL.PAYMENT_TOWARDS_ID = Convert.ToInt32(e.Record["PAYMENT_TOWARDS_ID"]).ToString();

                if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_PAYMENT_TOWARDS))
                {
                    BindPaymentTowards();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Terms has been deleted sucessfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Payment Terms cannot be deleted as it is already in use.');", true);
                }
            }
            catch (Exception ex)
            {
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void lnkbtnpaytowards_Click(object sender, EventArgs e)
        {
            try
            {
                objProjectBL = new ProjectBL();

                ViewState["TYPEID"] = Convert.ToInt16(((LinkButton)sender).CommandName.ToString());
                objProjectBL.PAYMENT_TOWARDS_ID = Convert.ToInt16(ViewState["TYPEID"]).ToString();
                objProjectBL.txtpaymentTowards = txtpaymentTowards.Text;
                //objProjectBL.Task = "Select_Payment_Terms_by_ID";
                if (objProjectBL.loadPCT(con, ProjectBL.eLoadSp.SELECT_PAYMENT_CATGORY_TOWARDS_BY_PAYMENT_TOWARDS_ID))
                {
                    txtpaymentTowards.Text = objProjectBL.txtpaymentTowards.ToString();
                    btnsavetowards.Text = "Update";
                    ModalPaymentTowardsPopup.Show();
                }
            }
            catch (Exception ex)
            {
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                objProjectBL = new ProjectBL();
                objProjectBL.UserID = Convert.ToInt32(Session["UID"].ToString());
                objProjectBL.IndentDate = Convert.ToDateTime(txtBankIndDate.Text.Trim());
                objProjectBL.StateCode = ddlState.SelectedValue.Trim();
                objProjectBL.ProjectCode = ddlProject.SelectedValue.Trim();
                objProjectBL.FYear = ddlFYear.SelectedValue.Trim();
                objProjectBL.BankName = ddlbank.SelectedValue.Trim();
                objProjectBL.Bank = txtbank.Text.Trim();
                objProjectBL.Branch = txtbranch.Text.Trim();
                objProjectBL.ISFC = txtisfc.Text.Trim();
                objProjectBL.PaymentType = rblpaytype.SelectedValue.Trim();
                objProjectBL.PaymentCatagary = ddlpaymentcat.SelectedValue.Trim();
                objProjectBL.PaymentSubCatagary = ddlpaymentsubcat.SelectedValue.Trim();
                objProjectBL.Towards = ddlpaymenttowards.SelectedValue.Trim();
                objProjectBL.Refdetails = txtrefdetails.Text.Trim();
                objProjectBL.Narration = txtNarration.Text.Trim();
                objProjectBL.RateOfInterest = txtrateofinterest.Text.Trim();
                objProjectBL.ACCOUNT_NUMBER = txtaccountno.Text.Trim();
                objProjectBL.Amount = Convert.ToDecimal(txtamount.Text.Trim());

                objProjectBL.CGSTPerc = Convert.ToDecimal(txtCGSTperc.Text.Trim());
                objProjectBL.SGSTPerc = Convert.ToDecimal(txtSGSTperc.Text.Trim());
                objProjectBL.IGSTPerc = Convert.ToDecimal(txtIGSTperc.Text.Trim());
                objProjectBL.CGSTAmt = Convert.ToDecimal(txtamount.Text.Trim()) * Convert.ToDecimal(txtCGSTperc.Text.Trim()) / 100;
                objProjectBL.SGSTAmt = Convert.ToDecimal(txtamount.Text.Trim()) * Convert.ToDecimal(txtSGSTperc.Text.Trim()) / 100;
                objProjectBL.IGSTAmt = Convert.ToDecimal(txtamount.Text.Trim()) * Convert.ToDecimal(txtIGSTperc.Text.Trim()) / 100;



                if (fuGSTRegd.HasFile)
                {
                    objProjectBL.Uploaded_File = "_Document" + System.IO.Path.GetExtension(fuGSTRegd.FileName);
                }

                //if (rblOrderType.SelectedValue != "")
                //{
                //    objPSO.OrderType = rblOrderType.SelectedValue.Trim();
                //}

                objProjectBL.Task = "InsertBankIndent";

                if (btnSubmit.Text == "Submit")
                {

                    if (objProjectBL.insertBI(con, ProjectBL.eLoadSp.INSERTBANKINDENT))
                    {
                        ViewState["BANK_INDENT_No"] = objProjectBL.BANK_INDENT_No.ToString();
                        ViewState["BANK_INDENT_ID"] = Convert.ToInt32(objProjectBL.BANK_INDENT_ID);
                        btnSubmit.Text = "Update";
                        txtBankIndNo.Enabled = false;
                        txtBankIndNo.Text = objProjectBL.BANK_INDENT_No.ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Bank Indent details has been inserted sucessfully.');", true);
                        Response.Redirect("~/Procurement/PaymentIndentList.aspx", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to insert Bank Indent details !.');", true);
                    }
                }

                else
                {
                    objProjectBL.Task = "UpdateBankIndent";
                    objProjectBL.BANK_INDENT_No = txtBankIndNo.Text.ToString();
                    if (objProjectBL.updateBI(con, ProjectBL.eLoadSp.UPDATEBANKINDENT))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Banking Indent details has been updated sucessfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update Banking Indent details !.');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void ddlpaymentcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlpaymentsubcat.Items.Clear();
            ddlpaymentsubcat.Items.Add("-Select-");


            objProjectBL = new ProjectBL();
            DataSet ds = new DataSet();
            objProjectBL.PaymentCatagary = ddlpaymentcat.SelectedItem.Value;
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PAYMENT_CATAGORY_DROPDOWN, ref ds);
            //ddlProject.DataSource = ds;
            //ddlProject.DataTextField = "Project_Name";
            // ddlProject.DataValueField = "Project_Code";
            ddlpaymentsubcat.DataBind();
            ddlpaymentsubcat.Items.Insert(0, "-Select-");
            BindPaymentCatagoryddl();
            //SqlCommand cmd = new SqlCommand("select * from tbl_state where country_id=" + DropDownList1.SelectedItem.Value, con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            // DropDownList2.DataSource = dt;
            //  DropDownList2.DataBind();
        }


    }
}