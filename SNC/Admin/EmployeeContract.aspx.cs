using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SNC.ErrorLogger;
using System.Net;
using System.IO;

public partial class EmployeeContract : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    DataSet ds = null;
    EmployeeContractBL objEC = null;
    ProjectBL objProjectBL = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    BindProjectList();
                    if (Request.QueryString["EmployeeID"] != null)
                    {

                        GetEmployeeContractDetails(Request.QueryString["EmployeeID"].ToString());
                        if (rblStatus.SelectedValue == "Active")
                        {
                            btnPrint.Visible = true;
                            btnPrint.Target = "_blank";
                            btnPrint.HRef = "EmployeeContractPrint.aspx?EmployeeID=" + Request.QueryString["EmployeeID"].ToString();
                            btnPrint.Visible = true;
                        }
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
            //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objEC = new EmployeeContractBL();
            objEC.Full_Name = txtFull_Name.Text.Trim();
            objEC.EmployeeID = txtEmployeeID.Text.Trim();
            objEC.FatherName = txtFatherName.Text.Trim();
            objEC.Employee_Permanent_Address = txtEmployee_Permanent_Address.Text.Trim();
            objEC.Email_ID = txtEmail_ID.Text.Trim();
            objEC.Qualification = txtQualification.Text.Trim();
            objEC.Gender = ddlGender.Text.Trim();
            objEC.Employee_Mobile_No = txtEmployee_Mobile_No.Text.Trim();
            objEC.Date_of_Birth = Convert.ToDateTime(txtDate_of_Birth.Text.Trim());
            objEC.Employment_Type = ddlEmployment_Type.Text.Trim();
            objEC.Project = ddlProjects.SelectedValue.Trim();
            objEC.Designation = txtDesignation.Text.Trim();
            objEC.Actual_Date_of_Joining = Convert.ToDateTime(txtActual_Date_of_Joining.Text.Trim());
            objEC.Location = txtLocation.Text.Trim();
            objEC.Reporting_Manager = txtReporting_Manager.Text.Trim();
            objEC.Status = rblStatus.Text.Trim();
            objEC.Basic_Pay = txtBasic_Pay.Text != string.Empty ? Convert.ToDecimal(txtBasic_Pay.Text) : Convert.ToDecimal(0.00);
            objEC.Conveyance_Allowance = txtConveyance_Allowance.Text != string.Empty ? Convert.ToDecimal(txtConveyance_Allowance.Text) : Convert.ToDecimal(0.00);
            objEC.HRA = txtHouse_Rental_Allowance.Text != string.Empty ? Convert.ToDecimal(txtHouse_Rental_Allowance.Text) : Convert.ToDecimal(0.00);
            objEC.Special_Allowance = txtSpecial_Allowance.Text != string.Empty ? Convert.ToDecimal(txtSpecial_Allowance.Text) : Convert.ToDecimal(0.00);
            objEC.Sub_Total_A = txtSub_Total_A.Text != string.Empty ? Convert.ToDecimal(txtSub_Total_A.Text) : Convert.ToDecimal(0.00);
            objEC.PF_ER = txtPF_ER.Text != string.Empty ? Convert.ToDecimal(txtPF_ER.Text) : Convert.ToDecimal(0.00);
            objEC.Total_Cost_to_Company = txtTotal_Cost_to_Company.Text != string.Empty ? Convert.ToDecimal(txtTotal_Cost_to_Company.Text) : Convert.ToDecimal(0.00);
            objEC.PF = txtPF.Text != string.Empty ? Convert.ToDecimal(txtPF.Text) : Convert.ToDecimal(0.00);
            objEC.PT = txtPT.Text != string.Empty ? Convert.ToDecimal(txtPT.Text) : Convert.ToDecimal(0.00);
            objEC.Total_Deductions_B = txtTotal_Deductions_B.Text != string.Empty ? Convert.ToDecimal(txtTotal_Deductions_B.Text) : Convert.ToDecimal(0.00);
            objEC.NET_PAYMENT = txtNET_PAYMENT.Text != string.Empty ? Convert.ToDecimal(txtNET_PAYMENT.Text) : Convert.ToDecimal(0.00);
            objEC.Resume = Convert.ToString(fupResume.FileName.Trim());
            objEC.ID_Proof = Convert.ToString(fupIDProof.FileName.Trim());
            objEC.Bank_Details = Convert.ToString(fupBankDetails.FileName.Trim());
            objEC.Qualification_Certificates = Convert.ToString(fupQualificationCertificates.FileName.Trim());
            objEC.Pay_Slips = Convert.ToString(fupPaySlips.FileName.Trim());
            objEC.Other_Documents = Convert.ToString(fupOtherDocuments.FileName.Trim());
            objEC.Task = "InsertEC";

            if (btnSubmit.Text == "Submit")
            {
                if (objEC.insert(con, EmployeeContractBL.eLoadSp.INSERT))
                {
                    btnSubmit.Text = "Update";
                    objEC.EmployeeID = objEC.EmployeeID.ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Employee Contract details has been added sucessfully.');", true);
                    ClearItem();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to add Employee Contract details !.');", true);
                    ClearItem();
                }
            }

            else
            {
                objEC.Task = "UpdateEC";
                objEC.Employee_ContractID = Convert.ToInt32(ViewState["Employee_ContractID"].ToString());

                if (objEC.update(con, EmployeeContractBL.eLoadSp.UPDATE))
                {
                    if (rblStatus.SelectedValue == "Active")
                    {
                        btnPrint.Visible = true;
                        btnPrint.HRef = "EmployeeContractPrint.aspx?EmployeeID=" + Request.QueryString["EmployeeID"].ToString();
                        btnPrint.Target = "_blank";
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Employee Contract details has been updated sucessfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update Employee Contract details !.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void GetEmployeeContractDetails(string EmployeeID)
    {
        try
        {
            objEC = new EmployeeContractBL();
            ds = new DataSet();
            objEC.EmployeeID = EmployeeID;
            objEC.Task = "GetEmployeeContractDetails";
            objEC.load(con, EmployeeContractBL.eLoadSp.SELECT_ECDETAILS_BY_Employee_ContractID, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtFull_Name.Text = ds.Tables[0].Rows[0]["Full_Name"].ToString();
                txtEmployeeID.Text = ds.Tables[0].Rows[0]["EmployeeID"].ToString();
                txtFatherName.Text = ds.Tables[0].Rows[0]["FatherName"].ToString();
                txtEmployee_Permanent_Address.Text = ds.Tables[0].Rows[0]["Employee_Permanent_Address"].ToString();
                txtEmail_ID.Text = ds.Tables[0].Rows[0]["Email_ID"].ToString();
                txtQualification.Text = ds.Tables[0].Rows[0]["Qualification"].ToString();
                ddlGender.SelectedValue = ds.Tables[0].Rows[0]["Gender"].ToString();
                txtEmployee_Mobile_No.Text = ds.Tables[0].Rows[0]["Employee_Mobile_No"].ToString();
                txtDate_of_Birth.Text = ds.Tables[0].Rows[0]["Date_of_Birth"].ToString();
                ddlEmployment_Type.SelectedValue = ds.Tables[0].Rows[0]["Employment_Type"].ToString();
                ddlProjects.SelectedValue = ds.Tables[0].Rows[0]["Project"].ToString();
                txtDesignation.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
                txtActual_Date_of_Joining.Text = ds.Tables[0].Rows[0]["Actual_Date_of_Joining"].ToString();
                txtLocation.Text = ds.Tables[0].Rows[0]["Location"].ToString();
                txtReporting_Manager.Text = ds.Tables[0].Rows[0]["Reporting_Manager"].ToString();
                rblStatus.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                txtBasic_Pay.Text = ds.Tables[0].Rows[0]["Basic_Pay"].ToString();
                txtHouse_Rental_Allowance.Text = ds.Tables[0].Rows[0]["HRA"].ToString();
                txtConveyance_Allowance.Text = ds.Tables[0].Rows[0]["Conveyance_Allowance"].ToString();
                txtSpecial_Allowance.Text = ds.Tables[0].Rows[0]["Special_Allowance"].ToString();
                txtSub_Total_A.Text = ds.Tables[0].Rows[0]["Sub_Total_A"].ToString();
                txtPF_ER.Text = ds.Tables[0].Rows[0]["PF_ER"].ToString();
                txtTotal_Cost_to_Company.Text = ds.Tables[0].Rows[0]["Total_Cost_to_Company"].ToString();
                //txtDeductions.Text = ds.Tables[0].Rows[0]["Deductions"].ToString();
                txtPF.Text = ds.Tables[0].Rows[0]["PF"].ToString();
                txtPT.Text = ds.Tables[0].Rows[0]["PT"].ToString();
                txtTotal_Deductions_B.Text = ds.Tables[0].Rows[0]["Total_Deductions_B"].ToString();
                txtNET_PAYMENT.Text = ds.Tables[0].Rows[0]["NET_PAYMENT"].ToString();
                ViewState["Employee_ContractID"] = ds.Tables[0].Rows[0]["Employee_ContractID"].ToString();
            }
        }
        catch (Exception ex)
        {
            //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ClearItem()
    {

        txtEmployeeID.Text = "";
        txtFull_Name.Text = "";
        txtFatherName.Text = "";
        txtEmployee_Permanent_Address.Text = "";
        txtEmail_ID.Text = "";
        txtQualification.Text = "";
        ddlGender.SelectedIndex = -1;
        txtEmployee_Mobile_No.Text = "";
        txtDate_of_Birth.Text = "";
        ddlEmployment_Type.SelectedIndex = -1;
        ddlProjects.SelectedIndex = -1;
        txtDesignation.Text = "";
        txtActual_Date_of_Joining.Text = "";
        txtLocation.Text = "";
        txtReporting_Manager.Text = "";
        rblStatus.SelectedIndex = -1;
        txtBasic_Pay.Text = "";
        txtConveyance_Allowance.Text = "";
        txtHouse_Rental_Allowance.Text = "";
        txtSpecial_Allowance.Text = "";
        txtSub_Total_A.Text = "";
        txtPF_ER.Text = "";
        txtTotal_Cost_to_Company.Text = "";
        //txtDeductions.Text = "";
        txtPF.Text = "";
        txtPT.Text = "";
        txtTotal_Deductions_B.Text = "";
        txtNET_PAYMENT.Text = "";

    }

    protected void btnCancelEmployeeContract_Click(object sender, EventArgs e)
    {
        ClearItem();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../Admin/EmployeeContract.aspx", false);
        }
        catch (Exception ex)
        {
            //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindProjectList()
    {
        try
        {
            objProjectBL = new ProjectBL();
            ds = new DataSet();
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_ALL, ref ds);
            ddlProjects.DataTextField = "Project_Name";
            ddlProjects.DataValueField = "Project_Code";
            ddlProjects.DataSource = ds;
            ddlProjects.DataBind();
            ddlProjects.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
}