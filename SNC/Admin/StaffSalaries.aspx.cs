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



public partial class StaffSalaries : System.Web.UI.Page

{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        DataSet ds = null;
        
        StaffSalariesBL objSS = null;
        ProjectBL objProjectBL = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    if (Session["UID"] != null)
                    {
                        //BindProjectList();
                        if (Request.QueryString["Staff_id"] != null)
                        {
                        txtEmployeeID.Enabled = false;
                            GetEmployeeContractDetails(Convert.ToInt32(Request.QueryString["Staff_id"].ToString()));
                            if (rblStatus.SelectedValue == "Active")
                            {
                                btnPrint.Visible = true;
                                btnPrint.Target = "_blank";
                                btnPrint.HRef = "EmployeeContractPrint.aspx?EmployeeID=" + Request.QueryString["Staff_id"].ToString();
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
                objSS = new StaffSalariesBL();
                objSS.Full_Name = txtFull_Name.Text.Trim();
                objSS.EmployeeID = txtEmployeeID.Text.Trim();
                //objEC.FatherName = txtFatherName.Text.Trim();
                //objEC.Employee_Permanent_Address = txtEmployee_Permanent_Address.Text.Trim();
                //objEC.Email_ID = txtEmail_ID.Text.Trim();
                //objEC.Qualification = txtQualification.Text.Trim();
                //objEC.Gender = ddlGender.Text.Trim();
                //objEC.Employee_Mobile_No = txtEmployee_Mobile_No.Text.Trim();
                //objEC.Date_of_Birth = Convert.ToDateTime(txtDate_of_Birth.Text.Trim());
                //objEC.Employment_Type = ddlEmployment_Type.Text.Trim();
                //objEC.Project = ddlProjects.SelectedValue.Trim();
                objSS.Designation = txtDesignation.Text.Trim();
                objSS.Actual_Date_of_Joining = Convert.ToDateTime(txtActual_Date_of_Joining.Text.Trim());
                objSS.Location = txtLocation.Text.Trim();
                //objEC.Reporting_Manager = txtReporting_Manager.Text.Trim();
                objSS.Status = rblStatus.Text.Trim();
                objSS.Basic_Pay = txtBasic_Pay.Text != string.Empty ? Convert.ToDecimal(txtBasic_Pay.Text) : Convert.ToDecimal(0.00);
                objSS.Conveyance_Allowance = txtConveyance_Allowance.Text != string.Empty ? Convert.ToDecimal(txtConveyance_Allowance.Text) : Convert.ToDecimal(0.00);
                objSS.HRA = txtHouse_Rental_Allowance.Text != string.Empty ? Convert.ToDecimal(txtHouse_Rental_Allowance.Text) : Convert.ToDecimal(0.00);
                objSS.Special_Allowance = txtSpecial_Allowance.Text != string.Empty ? Convert.ToDecimal(txtSpecial_Allowance.Text) : Convert.ToDecimal(0.00);
                objSS.Sub_Total_A = txtSub_Total_A.Text != string.Empty ? Convert.ToDecimal(txtSub_Total_A.Text) : Convert.ToDecimal(0.00);
                objSS.PF_ER = txtPF_ER.Text != string.Empty ? Convert.ToDecimal(txtPF_ER.Text) : Convert.ToDecimal(0.00);
                objSS.Total_Cost_to_Company = txtTotal_Cost_to_Company.Text != string.Empty ? Convert.ToDecimal(txtTotal_Cost_to_Company.Text) : Convert.ToDecimal(0.00);
                objSS.PF = txtPF.Text != string.Empty ? Convert.ToDecimal(txtPF.Text) : Convert.ToDecimal(0.00);
                objSS.PT = txtPT.Text != string.Empty ? Convert.ToDecimal(txtPT.Text) : Convert.ToDecimal(0.00);
                objSS.Deductions = txtTotal_Deductions_B.Text != string.Empty ? Convert.ToDecimal(txtTotal_Deductions_B.Text) : Convert.ToDecimal(0.00);
                objSS.NET_PAYMENT = txtNET_PAYMENT.Text != string.Empty ? Convert.ToDecimal(txtNET_PAYMENT.Text) : Convert.ToDecimal(0.00);
                //objEC.Resume = Convert.ToString(fupResume.FileName.Trim());
                //objEC.ID_Proof = Convert.ToString(fupIDProof.FileName.Trim());
                //objEC.Bank_Details = Convert.ToString(fupBankDetails.FileName.Trim());
                //objEC.Qualification_Certificates = Convert.ToString(fupQualificationCertificates.FileName.Trim());
                //objEC.Pay_Slips = Convert.ToString(fupPaySlips.FileName.Trim());
                //objEC.Other_Documents = Convert.ToString(fupOtherDocuments.FileName.Trim());
                objSS.Task = "InsertSS";

                if (btnSubmit.Text == "Submit")
                {
               
                    if (objSS.insert(con, StaffSalariesBL.eLoadSp.INSERT))
                    {
                        btnSubmit.Text = "Update";
                        objSS.EmployeeID = objSS.EmployeeID.ToString();
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
                    objSS.Task = "UpdateSS";
                    objSS.Staff_SalaryID = Convert.ToInt32(ViewState["Staff_SalaryID"].ToString());

                    if (objSS.update(con, StaffSalariesBL.eLoadSp.UPDATE))
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

        private void GetEmployeeContractDetails(int Staff_SalaryID)
        {
            try
            {
                objSS = new StaffSalariesBL();
                ds = new DataSet();
                objSS.Staff_SalaryID = Staff_SalaryID;
                objSS.Task = "GetStaffSalaryDetails";
                objSS.load(con, StaffSalariesBL.eLoadSp.SELECT_SSDETAILS_BY_Staff_SalaryID, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtFull_Name.Text = ds.Tables[0].Rows[0]["Full_Name"].ToString();
                    txtEmployeeID.Text = ds.Tables[0].Rows[0]["EmployeeID"].ToString();
                    
                    txtDesignation.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
                    txtActual_Date_of_Joining.Text = ds.Tables[0].Rows[0]["Actual_Date_of_Joining"].ToString();
                    txtLocation.Text = ds.Tables[0].Rows[0]["Location"].ToString();
                    
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
                    txtTotal_Deductions_B.Text = ds.Tables[0].Rows[0]["Deductions"].ToString();
                    txtNET_PAYMENT.Text = ds.Tables[0].Rows[0]["NET_PAYMENT"].ToString();
                    ViewState["Staff_SalaryID"] = ds.Tables[0].Rows[0]["Staff_SalaryID"].ToString();
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
                Response.Redirect("../Admin/staffSalaries.aspx", false);
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        //protected void BindProjectList()
        //{
        //    try
        //    {
        //        objProjectBL = new ProjectBL();
        //        ds = new DataSet();
        //        objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_ALL, ref ds);
        //        ddlProjects.DataTextField = "Project_Name";
        //        ddlProjects.DataValueField = "Project_Code";
        //        ddlProjects.DataSource = ds;
        //        ddlProjects.DataBind();
        //        ddlProjects.Items.Insert(0, "-Select-");
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        //    }
        //}
}
