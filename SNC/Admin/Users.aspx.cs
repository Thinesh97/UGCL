using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using BusinessLayer;
using SNC.ErrorLogger;
using Bussinesslogic;
using System.Net;
using System.Net.Mail;
using System.IO;


public partial class Users : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    ProjectBL objprojectbl = new ProjectBL();

    UserBL objuserBL = null;
    DataSet ds = null;
    /// <summary>
    /// This Method Will Bind Designation Data
    /// To DropDownList as well as GridView
    /// </summary>
    /// 

    string fileNamewithName;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    BindDepartment();
                    BindDesignation();
                    BindProject();
                    //if (Session["Project_Code"] != null)
                    //{

                    //    ddlAssignToProject.SelectedValue = Session["Project_Code"].ToString();
                    //}
                    //ddlAssignToProject.Enabled = false;
                    //ddlAssignToProject_SelectedIndexChanged1(null, null);

                    if (Request.QueryString["ID"] != null)
                    {
                        GetUserDetail(Request.QueryString["ID"].ToString());

                        ddlDepartment_SelectedIndexChanged(sender, e);

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




    protected void SentMail()
    {
        //UserBL objUserBL = new UserBL();
        try
        {

            string encrypwd = clsLoginEncDec_BLL.ComputeHash(txtPassword.Text, "SHA512", null);


            UserBL objuserBL = new UserBL();
            ds = new DataSet();

            objuserBL.UserID = txtUsername.Text;
            objuserBL.Email_ID = txtEmailid.Text;

            objuserBL.Task = "GetPassword";
            objuserBL.load(con, UserBL.eLoadSp.FORGOTPASSWORD_GET_PASSWORD, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {

                string name = ds.Tables[0].Rows[0]["Name"].ToString();
                string password = ds.Tables[0].Rows[0]["Password"].ToString();

                //  ObjUserBL.UserID = string.Empty;

                objuserBL.Task = "CheckMail";
                objuserBL.load(con, UserBL.eLoadSp.CHECK_MAIL, ref ds);


                if (ds.Tables[0].Rows.Count > 0)
                {

                    objuserBL.Password = encrypwd;


                    objuserBL.Email_ID = txtEmailid.Text;
                    objuserBL.UserID = txtUsername.Text;
                    MailMessage mObj = new MailMessage();
                    mObj.From = new MailAddress(ds.Tables[0].Rows[0]["Email"].ToString());
                    mObj.To.Add(txtEmailid.Text);
                    mObj.Subject = "Your Password Details";
                    mObj.Body = "Hi " + name + ", <br/><br/>Your User Id is: " + txtUsername.Text + " and Password is: " + txtPassword.Text + "<br/> <br/> Please remember to change this temporary password on first log in.<br/>";
                    mObj.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = ds.Tables[0].Rows[0]["SMTPHost"].ToString();
                    smtp.Port = int.Parse(ds.Tables[0].Rows[0]["Port_no"].ToString());
                    smtp.Credentials = new System.Net.NetworkCredential(ds.Tables[0].Rows[0]["Email"].ToString(), ds.Tables[0].Rows[0]["Password"].ToString());
                    if (ds.Tables[0].Rows[0]["SSL_able"].ToString() == "Yes")
                    {
                        smtp.EnableSsl = true;

                    }
                    else
                    {
                        smtp.EnableSsl = false;
                    }

                    smtp.Send(mObj);
                    //   Response.Write("<script>alert('Password Details Sent Successfully.')</script>");


                }
                else
                {
                    Response.Write("<script>alert('Mail Configuration details needed to be added inorder to send the Password.')</script>");



                }

            }
            else
            {
                Response.Write("<script>alert(' The User ID & Email ID you have entered is not matching. Please Re enter.')</script>");

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void BindDesignation()
    {

        try
        {
            ds = new DataSet();
            objprojectbl = new ProjectBL();
            objprojectbl.load(con, ProjectBL.eLoadSp.SELECT_DESIGNATION_ALL, ref ds);
            ddlDesignation.DataSource = ds;
            ddlDesignation.DataTextField = "Designation";
            ddlDesignation.DataValueField = "Designation";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, "-Select-");

            Grid_Designation.DataSource = ds;
            Grid_Designation.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    /// <summary>
    /// This Method Will Bind Department Data
    /// To Department DropDownList as well as GridView
    /// </summary>
    protected void BindDepartment()
    {

        try
        {
            ds = new DataSet();
            objprojectbl = new ProjectBL();
            objprojectbl.load(con, ProjectBL.eLoadSp.SELECT_DEPARTMENT_ALL, ref ds);
            ddlDepartment.DataSource = ds;
            ddlDepartment.DataTextField = "Department";
            ddlDepartment.DataValueField = "Department";
            ddlDepartment.DataBind();

            ddlDepartment.Items.Insert(0, "-Select-");


            Grid_Department.DataSource = ds;
            Grid_Department.DataBind();
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
            ds = new DataSet();
            objprojectbl = new ProjectBL();
            objprojectbl.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_ALL, ref ds);
           
            lstAssignToProject.DataValueField = "Project_Code";
            lstAssignToProject.DataTextField = "Project_Name";
            lstAssignToProject.DataSource = ds;
            lstAssignToProject.DataBind();


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSaveDepartment_Click(object sender, EventArgs e)
    {
        try
        {
            objuserBL = new UserBL();


            objuserBL.Department = txtDeptName.Text.Trim();
            if (btnSaveDepartment.Text == "Save")
            {
                objuserBL.Task = "insert";
                if (objuserBL.insert(con, UserBL.eLoadSp.InsertDepartment))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render(' User Department name is Added Successfully');", true);
                    mpeDept.Show();
                    BindDepartment();
                    txtDeptName.Text = string.Empty;

                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Department Name already exists');", true);
                }

            }
            else
            {

                objuserBL.Department = ViewState["Dept"].ToString();
                objuserBL.Task = "DEPARTMENT_CHECKDUPLICATE";
                if (objuserBL.load(con, UserBL.eLoadSp.DEPARTMENT_CHECKDUPLICATE) && objuserBL.Department == Convert.ToString(ViewState["Dept"]))
                {
                    objuserBL.Department = txtDeptName.Text.Trim();
                    objuserBL.Content = ViewState["Dept"].ToString();
                    objuserBL.Task = "update";
                    if (objuserBL.update(con, UserBL.eLoadSp.update))
                    {
                        BindDepartment();
                        btnSaveDepartment.Text = "Save";
                        txtDeptName.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Department has been updated sucessfully.');", true);

                    }
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Department name  is already exists.');", true);
                }
            }
        }

        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }


    }

    protected void btnSaveDesignation_Click(object sender, EventArgs e)
    {
        try
        {
            objuserBL = new UserBL();

            objuserBL.Designation = txtDesignation.Text.Trim();
            if (btnSaveDesignation.Text == "Save")
            {

                objuserBL.Task = "Insert";
                if (objuserBL.insertDesignation(con, UserBL.eLoadSp.InsertDesignation))
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render(' User Designation is Added Successfully');", true);
                    mpeDesig.Show();
                    BindDesignation();
                    txtDesignation.Text = string.Empty; ;
                }


                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('User designation already existed !');", true);
                }
            }

            else
            {
                objuserBL.Designation = ViewState["Desig"].ToString();
                objuserBL.Task = "DESIGNATION_CHECKDUPLICATE";
                if (objuserBL.load(con, UserBL.eLoadSp.DESIGNATION_CHECKDUPLICATE) && objuserBL.Designation == Convert.ToString(ViewState["Desig"]))
                {

                    objuserBL.Designation = txtDesignation.Text.Trim();
                    objuserBL.ContentDesig = ViewState["Desig"].ToString();
                    objuserBL.Task = "Desig_Update";
                    if (objuserBL.Desig_Update(con, UserBL.eLoadSp.InsertDesignation))
                    {
                        BindDesignation();
                        btnSaveDesignation.Text = "Save";
                        txtDesignation.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Designation has been updated sucessfully.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('You can't update it !');", true);
                }
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void lnkDepartment_Click(object sender, EventArgs e)
    {

        try
        {
            ViewState["Dept"] = ((LinkButton)sender).Text.ToString();
            txtDeptName.Text = ViewState["Dept"].ToString();
            btnSaveDepartment.Text = "Update";
            //ddlDepartment.Enabled = false;
            mpeDept.Show();
        }


        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }




    protected void lnkDesignation_Click(object sender, EventArgs e)
    {

        try
        {
            ViewState["Desig"] = ((LinkButton)sender).Text.ToString();
            txtDesignation.Text = ViewState["Desig"].ToString();
            btnSaveDesignation.Text = "Update";
            mpeDesig.Show();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        UserBL obj = new UserBL();
        {
            try
            {
                UserBL objuserBL = new UserBL();
                objuserBL.Name = Tb_name.Text;
                objuserBL.UserID = txtUsername.Text;


                string encrypwd = clsLoginEncDec_BLL.ComputeHash(txtPassword.Text, "SHA512", null);
                objuserBL.Password = encrypwd;
                objuserBL.Email_ID = txtEmailid.Text;
                objuserBL.Employee_ID = Tb_empID.Text;
                //objuserBL.Is_CFO_User = chkIsCFOUser.Checked;
                objuserBL.Appr_PO_Delete = chk_Appr_PO_Delete.Checked;

                if (ddlDepartment.SelectedIndex != 0)
                {
                    objuserBL.Department = ddlDepartment.SelectedValue;

                }
                else
                {
                    objuserBL.Department = null;
                }

                if (ddlDesignation.SelectedIndex != 0)
                {
                    objuserBL.Designation = ddlDesignation.SelectedValue;

                }
                else
                {
                    objuserBL.Designation = null;
                }

                string projects = string.Empty;
                foreach (ListItem listItem in lstAssignToProject.Items)
                {
                    if (listItem.Selected)
                    {
                        if (projects == string.Empty)
                        {
                            projects = listItem.Value;
                        }
                        else
                        {
                            projects += "," + listItem.Value;
                        }
                    }
                }
                if (projects !="")
                {
                    objuserBL.Project_Code = projects.ToString();
                }
                else
                {
                    objuserBL.Project_Code = null;
                }
                objuserBL.Status = rblStatus.SelectedValue;
                if (chkboxIsHoUser.Checked == true)
                {
                    objuserBL.IsHoUser = true;
                }
                else
                {
                    objuserBL.IsHoUser = false;
                }


                objuserBL.Role = ddlRole.SelectedIndex != 0 ? ddlRole.SelectedValue : string.Empty;

                if (chkboxUserCreate.Checked == true)
                {
                    objuserBL.UserCreate = true;
                }
                else
                {
                    objuserBL.UserCreate = false;
                }

                if (chkboxUserview.Checked == true)
                {
                    objuserBL.UserView = true;
                }
                else
                {
                    objuserBL.UserView = false;
                }

                if (chkboxUserupdate.Checked == true)
                {
                    objuserBL.UserUpdate = true;
                }
                else
                {
                    objuserBL.UserUpdate = false;
                }

                if (chkboxUserdelete.Checked == true)
                {
                    objuserBL.UserDelete = true;
                }
                else
                {
                    objuserBL.UserDelete = false;
                }

                if (chkboxEmpContractCreate.Checked == true)
                {
                    objuserBL.EmpContractCreate = true;
                }
                else
                {
                    objuserBL.EmpContractCreate = false;
                }

                if (chkboxEmpContractView.Checked == true)
                {
                    objuserBL.EmpContractView = true;
                }
                else
                {
                    objuserBL.EmpContractView = false;
                }

                if (chkboxEmpContractUpdate.Checked == true)
                {
                    objuserBL.EmpContractUpdate = true;
                }
                else
                {
                    objuserBL.EmpContractUpdate = false;
                }

                if (chkboxEmpContractDelete.Checked == true)
                {
                    objuserBL.EmpContractDelete = true;
                }
                else
                {
                    objuserBL.EmpContractDelete = false;
                }

                if (chkboxMailConfigUpdate.Checked == true)
                {
                    objuserBL.Mail_update = true;
                }

                else
                {
                    objuserBL.Mail_update = false;
                }

                if (chkboxStockTransferCreate.Checked == true)
                {
                    objuserBL.Stock_TransferCreate = true;
                }
                else
                {
                    objuserBL.Stock_TransferCreate = false;
                }

                if (chkboxStockTransferView.Checked == true)
                {
                    objuserBL.Stock_TransferView = true;
                }
                else
                {
                    objuserBL.Stock_TransferView = false;
                }


                if (chkboxStockTransferUpdate.Checked == true)
                {
                    objuserBL.Stock_TransferUpdate = true;
                }
                else
                {
                    objuserBL.Stock_TransferUpdate = false;
                }
                if (chkboxStockTransferDelete.Checked == true)
                {
                    objuserBL.Stock_TransferDelete = true;
                }
                else
                {
                    objuserBL.Stock_TransferDelete = false;
                }
                
                if (chckboxCreateComSite.Checked == true)
                {
                    objuserBL.CompySite_Create = true;
                }

                else
                {
                    objuserBL.CompySite_Create = false;
                }

                if (chckboxViewComSite.Checked == true)
                {
                    objuserBL.CompySite_View = true;
                }

                else
                {
                    objuserBL.CompySite_View = false;
                }

                if (chckboxUpdateComSite.Checked == true)
                {
                    objuserBL.CompySite_Update = true;
                }

                else
                {
                    objuserBL.CompySite_Update = false;
                }

                if (chckboxDeleteComSite.Checked == true)
                {
                    objuserBL.CompSite_Delete = true;
                }

                else
                {
                    objuserBL.CompSite_Delete = false;
                }

                if (ChkboxCreateProject.Checked == true)
                {
                    objuserBL.Proj_Create = true;
                }

                else
                {
                    objuserBL.Proj_Create = false;
                }

                if (ChkboxViewProject.Checked == true)
                {
                    objuserBL.Proj_View = true;
                }

                else
                {
                    objuserBL.Proj_View = false;
                }

                if (ChkboxUpdateProject.Checked == true)
                {
                    objuserBL.Proj_Update = true;
                }

                else
                {
                    objuserBL.Proj_Update = false;
                }

                if (ChkboxDeleteProject.Checked == true)
                {
                    objuserBL.Proj_Delete = true;
                }

                else
                {
                    objuserBL.Proj_Delete = false;
                }

                if (ChkBoxCreateUom.Checked == true)
                {
                    objuserBL.UOM_Create = true;
                }

                else
                {
                    objuserBL.UOM_Create = false;
                }

                if (ChkBoxViewUom.Checked == true)
                {
                    objuserBL.UOM_View = true;
                }

                else
                {
                    objuserBL.UOM_View = false;
                }

                if (ChkBoxUpdateUom.Checked == true)
                {
                    objuserBL.UOM_Update = true;
                }

                else
                {
                    objuserBL.UOM_Update = false;
                }

                if (ChkBoxDeleteUom.Checked == true)
                {
                    objuserBL.UOM_Delete = true;
                }

                else
                {
                    objuserBL.UOM_Delete = false;
                }

                if (chkboxMateriaCreate.Checked == true)
                {
                    objuserBL.Material_Create = true;
                }

                else
                {
                    objuserBL.Material_Create = false;
                }

                if (chkboxMateriaView.Checked == true)
                {
                    objuserBL.Material_View = true;
                }

                else
                {
                    objuserBL.Material_View = false;
                }

                if (chkboxMateriaUpdate.Checked == true)
                {
                    objuserBL.Material_Update = true;
                }

                else
                {
                    objuserBL.Material_Update = false;
                }

                if (chkboxMaterialDelete.Checked == true)
                {
                    objuserBL.Material_Delete = true;
                }

                else
                {
                    objuserBL.Material_Delete = false;
                }

                if (ChBoxVendorCreate.Checked == true)
                {
                    objuserBL.Vendor_Create = true;
                }

                else
                {
                    objuserBL.Vendor_Create = false;
                }

                if (ChBoxVendorView.Checked == true)
                {
                    objuserBL.Vendor_View = true;
                }

                else
                {
                    objuserBL.Vendor_View = false;
                }

                if (ChBoxVendorUpdate.Checked == true)
                {
                    objuserBL.Vendor_Update = true;
                }

                else
                {
                    objuserBL.Vendor_Update = false;
                }

                if (ChBoxVendorDelete.Checked == true)
                {
                    objuserBL.Vendor_Delete = true;
                }

                else
                {
                    objuserBL.Vendor_Delete = false;
                }

                if (chkboxSubContractorCreate.Checked == true)
                {
                    objuserBL.Sub_Cont_Create = true;
                }
                else
                {
                    objuserBL.Sub_Cont_Create = false;
                }

                if (chkboxSubContractorView.Checked == true)
                {
                    objuserBL.Sub_Cont_View = true;
                }
                else
                {
                    objuserBL.Sub_Cont_View = false;
                }

                if (chkboxSubContractorUpdate.Checked == true)
                {
                    objuserBL.Sub_Cont_Update = true;
                }
                else
                {
                    objuserBL.Sub_Cont_Update = false;
                }

                if (chkboxSubContractorDelete.Checked == true)
                {
                    objuserBL.Sub_Cont_Delete = true;
                }
                else
                {
                    objuserBL.Sub_Cont_Delete = false;
                }

                if (chkboxOtherCreate.Checked == true)
                {
                    objuserBL.OtherCreate = true;
                }
                else
                {
                    objuserBL.OtherCreate = false;
                }

                if (chkboxOtherView.Checked == true)
                {
                    objuserBL.OtherView = true;
                }
                else
                {
                    objuserBL.OtherView = false;
                }

                if (chkboxOtherUpdate.Checked == true)
                {
                    objuserBL.OtherUpdate = true;
                }
                else
                {
                    objuserBL.OtherUpdate = false;
                }

                if (chkboxOtherDelete.Checked == true)
                {
                    objuserBL.OtherDelete = true;
                }
                else
                {
                    objuserBL.OtherDelete = false;
                }

                if (chkBoxBudgetCreate.Checked == true)
                {
                    objuserBL.Bug_Create = true;
                }
                else
                {
                    objuserBL.Bug_Create = false;
                }

                if (chkBoxBudgetView.Checked == true)
                {
                    objuserBL.Bug_View = true;
                }

                else
                {
                    objuserBL.Bug_View = false;
                }
                if (chkBoxBudgetUpdate.Checked == true)
                {
                    objuserBL.Bug_Update = true;
                }

                else
                {
                    objuserBL.Bug_Update = false;
                }

                if (chkBoxBudgetDelete.Checked == true)
                {
                    objuserBL.Bug_Delete = true;
                }

                else
                {
                    objuserBL.Bug_Delete = false;
                }



                if (ChkProBudgetCreate.Checked == true)
                {
                    objuserBL.Pro_Bug_Create = true;
                }

                else
                {
                    objuserBL.Pro_Bug_Create = false;
                }

                if (ChkProBudgetView.Checked == true)
                {
                    objuserBL.Pro_Bug_View = true;
                }

                else
                {
                    objuserBL.Pro_Bug_View = false;
                }
                if (ChkProBudgetUpdate.Checked == true)
                {
                    objuserBL.Pro_Bug_Update = true;
                }

                else
                {
                    objuserBL.Pro_Bug_Update = false;
                }

                if (ChkProBudgetDelete.Checked == true)
                {
                    objuserBL.Pro_Bug_Delete = true;
                }

                else
                {
                    objuserBL.Pro_Bug_Delete = false;
                }



                if (chkboxBudgetModREquestCreate.Checked == true)
                {
                    objuserBL.Bug_Mod_Create = true;
                }

                else
                {
                    objuserBL.Bug_Mod_Create = false;
                }

                if (chkboxBudgetModREquestView.Checked == true)
                {
                    objuserBL.Bug_Mod_View = true;
                }

                else
                {
                    objuserBL.Bug_Mod_View = false;
                }

                if (chkboxBudgetModREquestUpdate.Checked == true)
                {
                    objuserBL.Bug_Mod_Update = true;
                }

                else
                {
                    objuserBL.Bug_Mod_Update = false;
                }

                if (chkboxBudgetModREquestDelete.Checked == true)
                {
                    objuserBL.Bug_Mod_Delete = true;
                }

                else
                {
                    objuserBL.Bug_Mod_Delete = false;
                }

                if (ChkBoxReportView.Checked == true)
                {
                    objuserBL.Bug_Reports = true;
                }

                else
                {
                    objuserBL.Bug_Reports = false;
                }


                if (chkboxQuotationCreate.Checked == true)
                {
                    objuserBL.Quotn_Create = true;
                }

                else
                {
                    objuserBL.Quotn_Create = false;
                }

                if (chkboxQuotationView.Checked == true)
                {
                    objuserBL.Quotn_View = true;
                }

                else
                {
                    objuserBL.Quotn_View = false;
                }

                if (chkboxQuotationUpdate.Checked == true)
                {
                    objuserBL.Quotn_Update = true;
                }

                else
                {
                    objuserBL.Quotn_Update = false;
                }
                if (chkboxQuotationDelete.Checked == true)
                {
                    objuserBL.Quotn_Delete = true;
                }

                else
                {
                    objuserBL.Quotn_Delete = false;
                }
                if (chkboxQuotationCompare.Checked == true)
                {
                    objuserBL.Quotn_Compare = true;
                }

                else
                {
                    objuserBL.Quotn_Compare = false;
                }
                if (chkboxIndentCreate.Checked == true)
                {
                    objuserBL.Ind_Create = true;
                }

                else
                {
                    objuserBL.Ind_Create = false;
                }

                if (chkboxIndentView.Checked == true)
                {
                    objuserBL.Ind_View = true;
                }

                else
                {
                    objuserBL.Ind_View = false;
                }


                if (CheckBoxIndentUpdate.Checked == true)
                {
                    objuserBL.Ind_Update = true;
                }

                else
                {
                    objuserBL.Ind_Update = false;
                }

                if (CheckBoxIndentDelete.Checked == true)
                {
                    objuserBL.Ind_Delete = true;
                }

                else
                {
                    objuserBL.Ind_Delete = false;
                }

                if (ChkBoxPOCreate.Checked == true)
                {
                    objuserBL.PO_Create = true;
                }
                else
                {
                    objuserBL.PO_Create = false;
                }

                if (ChkBoxView.Checked == true)
                {
                    objuserBL.PO_View = true;
                }
                else
                {
                    objuserBL.PO_View = false;
                }

                if (ChkBoxPOUpdate.Checked == true)
                {
                    objuserBL.PO_Update = true;
                }
                else
                {
                    objuserBL.PO_Update = false;
                }


                if (ChkBoxDelete.Checked == true)
                {
                    objuserBL.PO_Delete = true;
                }
                else
                {
                    objuserBL.PO_Delete = false;
                }

                if (ChkBoxPayIndCreate.Checked == true)
                {
                    objuserBL.PayInd_Create = true;
                }
                else
                {
                    objuserBL.PayInd_Create = false;
                }

                if (ChkBoxPayIndView.Checked == true)
                {
                    objuserBL.PayInd_View = true;
                }
                else
                {
                    objuserBL.PayInd_View = false;
                }

                if (ChkBoxPayIndUpdate.Checked == true)
                {
                    objuserBL.PayInd_Update = true;
                }
                else
                {
                    objuserBL.PayInd_Update = false;
                }

                if (ChkBoxPayIndDelete.Checked == true)
                {
                    objuserBL.PayInd_Delete = true;
                }
                else
                {
                    objuserBL.PayInd_Delete = false;
                }

                if (ChkBoxPayIndVerCreate.Checked == true)
                {
                    objuserBL.PayInd_Ver_Create = true;
                }
                else
                {
                    objuserBL.PayInd_Ver_Create = false;
                }

                if (ChkBoxPayIndVerView.Checked == true)
                {
                    objuserBL.PayInd_Ver_View = true;
                }
                else
                {
                    objuserBL.PayInd_Ver_View = false;
                }

                if (ChkBoxPayIndVerUpdate.Checked == true)
                {
                    objuserBL.PayInd_Ver_Update = true;
                }
                else
                {
                    objuserBL.PayInd_Ver_Update = false;
                }

                if (ChkBoxPayIndVerDelete.Checked == true)
                {
                    objuserBL.PayInd_Ver_Delete = true;
                }
                else
                {
                    objuserBL.PayInd_Ver_Delete = false;
                }

                if (ChkBoxPayIndAppCreate.Checked == true)
                {
                    objuserBL.PayInd_App_Create = true;
                }
                else
                {
                    objuserBL.PayInd_App_Create = false;
                }

                if (ChkBoxPayIndAppView.Checked == true)
                {
                    objuserBL.PayInd_App_View = true;
                }
                else
                {
                    objuserBL.PayInd_App_View = false;
                }

                if (ChkBoxPayIndAppUpdate.Checked == true)
                {
                    objuserBL.PayInd_App_Update = true;
                }
                else
                {
                    objuserBL.PayInd_App_Update = false;
                }

                if (ChkBoxPayIndAppDelete.Checked == true)
                {
                    objuserBL.PayInd_App_Delete = true;
                }
                else
                {
                    objuserBL.PayInd_App_Delete = false;
                }

                if (chkboxPOReportView.Checked == true)
                {
                    objuserBL.Proc_Reports = true;
                }

                else
                {
                    objuserBL.Proc_Reports = false;
                }
                if (chkboxStockCreate.Checked == true)
                {
                    objuserBL.Stock_Create = true;
                }

                else
                {
                    objuserBL.Stock_Create = false;
                }
                if (chkboxStockView.Checked == true)
                {
                    objuserBL.Stock_View = true;
                }

                else
                {
                    objuserBL.Stock_View = false;
                }
                if (chkboxStockUpdate.Checked == true)
                {
                    objuserBL.Stock_Update = true;
                }

                else
                {
                    objuserBL.Stock_Update = false;
                }
                if (chkboxStockDelete.Checked == true)
                {
                    objuserBL.Stock_Delete = true;
                }

                else
                {
                    objuserBL.Stock_Delete = false;
                }


                if (ChkBoxMRNCreate.Checked == true)
                {
                    objuserBL.MRN_Create = true;
                }

                else
                {
                    objuserBL.MRN_Create = false;
                }

                if (ChkBoxMRNView.Checked == true)
                {
                    objuserBL.MRN_View = true;
                }

                else
                {
                    objuserBL.MRN_View = false;
                }


                if (ChkBoxMRNUpdate.Checked == true)
                {
                    objuserBL.MRN_Update = true;
                }

                else
                {
                    objuserBL.MRN_Update = false;
                }

                if (ChkBoxMRNDelete.Checked == true)
                {
                    objuserBL.MRN_Delete = true;
                }

                else
                {
                    objuserBL.MRN_Delete = false;
                }

                if (ChkBoxMINCreate.Checked == true)
                {
                    objuserBL.MIN_Create = true;
                }

                else
                {
                    objuserBL.MIN_Create = false;
                }

                if (ChkBoxMINView.Checked == true)
                {
                    objuserBL.MIN_View = true;
                }

                else
                {
                    objuserBL.MIN_View = false;
                }

                if (ChkBoxMINUpdate.Checked == true)
                {
                    objuserBL.MIN_Update = true;
                }

                else
                {
                    objuserBL.MIN_Update = false;
                }

                if (ChkBoxMINDelete.Checked == true)
                {
                    objuserBL.MIN_Delete = true;
                }

                else
                {
                    objuserBL.MRN_Delete = false;
                }


                if (ChkBoxINVReportsView.Checked == true)
                {
                    objuserBL.Inv_Reports = true;
                }

                else
                {
                    objuserBL.Inv_Reports = false;
                }

                if (ChkBoxAssetRegCreate.Checked == true)
                {
                    objuserBL.AssReg_Create = true;
                }

                else
                {
                    objuserBL.AssReg_Create = false;
                }

                if (ChkBoxAssetRegView.Checked == true)
                {
                    objuserBL.AssReg_View = true;
                }

                else
                {
                    objuserBL.AssReg_View = false;
                }

                if (ChkBoxAssetRegUpdate.Checked == true)
                {
                    objuserBL.AssReg_Update = true;
                }

                else
                {
                    objuserBL.AssReg_Update = false;
                }



                if (ChkBoxAssetRegDelete.Checked == true)
                {
                    objuserBL.AssReg_Delete = true;
                }

                else
                {
                    objuserBL.AssReg_Delete = false;
                }

                if (ChkBoxAssetTransferCreate.Checked == true)
                {
                    objuserBL.AssTra_Create = true;
                }

                else
                {
                    objuserBL.AssTra_Create = false;
                }

                if (ChkBoxAssetTransferView.Checked == true)
                {
                    objuserBL.AssTra_View = true;
                }

                else
                {
                    objuserBL.AssTra_View = false;
                }

                if (ChkBoxAssetTransferUpdate.Checked == true)
                {
                    objuserBL.AssTra_Update = true;
                }

                else
                {
                    objuserBL.AssTra_Update = false;
                }

                if (ChkBoxAssetTransferDelete.Checked == true)
                {
                    objuserBL.AssTra_Delete = true;
                }

                else
                {
                    objuserBL.AssTra_Delete = false;
                }

                if (chkboxDailyRunHourKmCreate.Checked == true)
                {
                    objuserBL.DailRun_Create = true;
                }

                else
                {
                    objuserBL.DailRun_Create = false;
                }


                if (chkboxDailyRunHourKmView.Checked == true)
                {
                    objuserBL.DailRun_View = true;
                }

                else
                {
                    objuserBL.DailRun_View = false;
                }


                if (chkboxDailyRunHourKmUpdate.Checked == true)
                {
                    objuserBL.DailRun_Update = true;
                }

                else
                {
                    objuserBL.DailRun_Update = false;
                }
                if (chkboxDailyRunHourKmDelete.Checked == true)
                {
                    objuserBL.DailRun_Delete = true;
                }

                else
                {
                    objuserBL.DailRun_Delete = false;
                }


                if (ChkBoxAssetReportsView.Checked == true)
                {
                    objuserBL.Ass_Rep = true;
                }

                else
                {
                    objuserBL.Ass_Rep = false;
                }

                if (chk_Local_MRN_Create.Checked == true)
                {
                    objuserBL.Local_MRN_Create = true;
                }
                else
                {
                    objuserBL.Local_MRN_Create = false;
                }

                if (chk_Local_MRN_Update.Checked == true)
                {
                    objuserBL.Local_MRN_Update = true;
                }
                else
                {
                    objuserBL.Local_MRN_Update = false;
                }

                if (chk_LocalMRN_View.Checked == true)
                {
                    objuserBL.LocalMRN_View = true;
                }
                else
                {
                    objuserBL.LocalMRN_View = false;
                }

                if (chk_Local_MRN_Delete.Checked == true)
                {
                    objuserBL.Local_MRN_Delete = true;
                }
                else
                {
                    objuserBL.Local_MRN_Delete = false;
                }







                if (SignUploader.HasFile)
                {
                    if (ValidateImageSize())
                    {
                        string[] validFileTypes = { "png", "jpg", "jpeg" };
                        string ext = System.IO.Path.GetExtension(SignUploader.PostedFile.FileName);
                        bool isValidFile = false;
                        for (int i = 0; i < validFileTypes.Length; i++)
                        {
                            if (ext == "." + validFileTypes[i])
                            {
                                isValidFile = true;
                                break;
                            }
                        }
                        if (!isValidFile)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please upload the png or jpg or jpeg');", true);

                        }
                        bool existfile = false;
                        string folderpath = Server.MapPath(ConfigurationManager.AppSettings["DigitalSignImagepath"]).ToString();
                        string filename = SignUploader.FileName; 
                        string[] filePaths = Directory.GetFiles(folderpath);
                        foreach (string filechk in filePaths)
                        {
                            if (filechk == folderpath + filename)
                            {
                                fileNamewithName = filechk;
                                existfile = true;
                                break;
                            }

                        }
                        if (!existfile)
                        {
                            string uniqueDateTime = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss-fff_");
                            objuserBL.DigitalSign_FileName = SignUploader.FileName;
                            objuserBL.Image_FilePath = "~/EmployeeDigitalSignImg/" + SignUploader.FileName.Trim();

                            SignUploader.SaveAs(Server.MapPath("../EmployeeDigitalSignImg/" + SignUploader.FileName));

                            // this.imgBtnDigitalSign.ImageUrl = "~/EmployeeDigitalSignImg/" + SignUploader.FileName.Trim();
                            if (!string.IsNullOrEmpty(objuserBL.DigitalSign_FileName))
                            {
                                ViewState["fileName"] = objuserBL.DigitalSign_FileName;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render(' This Signature is refereed to other User.!!!');", true);
                            return;
                        }
                    }

                }


                if (btnSubmit.Text == "Submit")
                {
                    if (!string.IsNullOrEmpty(objuserBL.Image_FilePath))
                    {
                        ViewState["filePath"] = objuserBL.Image_FilePath;
                    }
                    
                    if (objuserBL.Insert_AllUserDetails(con, UserBL.eLoadSp.INSERT_ALLUSERS))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('User Details has been Added Successfully');", true);
                        SentMail();
                        Clear();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render(' UserName  or Employee-ID Already Exist!!!');", true);
                    }
                }

                else
                {
                    objuserBL.Employee_ID = Tb_empID.Text.Trim();

                    if (objuserBL.Update_UserDetailsById(con, UserBL.eLoadSp.UPDATE_USERByID))
                    {
                        if (SignUploader.HasFile && ViewState["filePath"] != null)
                        {
                            File.Delete(Server.MapPath(ViewState["filePath"].ToString()));
                        }
                        this.imgBtnDigitalSign.ImageUrl = "~/EmployeeDigitalSignImg/" + SignUploader.FileName.Trim();
                        imgBtnDigitalSign.Visible = true;
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('User details has been updated sucessfully.');", true);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
            //ResetUsersItems();
        }

    }

    protected bool ValidateImageSize()
    {
        int fileSize = SignUploader.PostedFile.ContentLength;
        //Limit size to approx 500KB for image
        if ((fileSize > 0 & fileSize < 512000))
        {
            return true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render(' Please upload jpg/jpeg/png image of upto 500KB size only!!!');", true);
            return false;
        }
    }


    /// <summary>
    /// imgpassword ()Method is used for Auto Password Generating
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgpassword_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            string allowedChars = "";
            allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "1,2,3,4,5,6,7,8,9,0,!,@,#,$,%,&,?";
            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            Random rand = new Random();
            for (int i = 0; i < 8; i++)
            {
                txtPassword.Text += arr[rand.Next(0, arr.Length)];
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"].ToString());
        }
        imgpassword.Visible = false;
    }


    private void ResetUsersItems()
    {

        Tb_name.Text = string.Empty;
        Tb_empID.Text = string.Empty;

        ddlRole.SelectedValue = ddlRole.Items.FindByText("-select-").Value;
        txtEmailid.Text = string.Empty;
        chkboxIsHoUser.Checked = false;
        lstAssignToProject.SelectedValue = lstAssignToProject.Items.FindByText("-select-").Value;
        txtProjectcode.Text = string.Empty;
        txtUsername.Text = string.Empty;
        txtPassword.Text = string.Empty;
        rblStatus.SelectedValue = rblStatus.Items.FindByText("-select-").Value;

    }


    //protected void ddlAssignToProject_SelectedIndexChanged1(object sender, EventArgs e)
    //{
    //    if (ddlAssignToProject.SelectedIndex > 0)
    //    {
    //        txtProjectcode.Text = ddlAssignToProject.SelectedValue;
    //    }
    //    else
    //    {
    //        txtProjectcode.Text = string.Empty;
    //    }

    //}
    protected void GetUserDetail(string EmpId)
    {
        try
        {
            objuserBL = new UserBL();
            ds = new DataSet();
            objuserBL.Employee_ID = EmpId;

            objuserBL.load(con, UserBL.eLoadSp.SELECT_USERS_DETAILS_BY_ID, ref ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Tb_name.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                Tb_empID.Text = ds.Tables[0].Rows[0]["Employee_ID"].ToString();
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["Department"].ToString();

                ddlDesignation.SelectedValue = ds.Tables[0].Rows[0]["Designation"].ToString();
                ddlRole.SelectedValue = ds.Tables[0].Rows[0]["Role"].ToString();
                txtEmailid.Text = ds.Tables[0].Rows[0]["Email_ID"].ToString();
                lstAssignToProject.SelectedValue = ds.Tables[0].Rows[0]["Project_Code"].ToString();
                txtProjectcode.Text = ds.Tables[0].Rows[0]["Project_Code"].ToString();
                chkboxIsHoUser.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsHoUser"].ToString());
                txtUsername.Text = ds.Tables[0].Rows[0]["UserID"].ToString();

                txtPassword.Text = ds.Tables[0].Rows[0]["Password"].ToString();

                rblStatus.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                Tb_empID.Enabled = false;

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DigitalSign_FileName"].ToString()))
                {

                    ViewState["fileName"] = ds.Tables[0].Rows[0]["DigitalSign_FileName"].ToString();
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Image_FilePath"].ToString()))
                {
                    imgBtnDigitalSign.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                    imgBtnDigitalSign.Visible = true;
                    ViewState["filePath"] = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                }

                btnSubmit.Text = "Update";
                btnCancel.Text = "Cancel";
                imgpassword.Visible = false;
                txtUsername.Enabled = false;

            }

        }


        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }





    protected void Grid_Department_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            UserBL objuserBL = new UserBL();

            objuserBL.Department = Convert.ToString(e.Record["Department"].ToString());
            objuserBL.Task = "DEPARTMENT_DELETE";
            if (objuserBL.delete(con, UserBL.eLoadSp.DELETE_DEPT))
            {
                BindDepartment();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Department has been deleted sucessfully.');", true);

            }


            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Designation exists in one or more Users');", true);
            }
            mpeDept.Show();

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void Clear()
    {
        imgpassword.Visible = true;
        txtUsername.Text = string.Empty;
        Tb_name.Text = string.Empty;

        txtPassword.Text = string.Empty;
        txtEmailid.Text = string.Empty;
        chkboxIsHoUser.Checked = false;
        Tb_empID.Text = string.Empty;
        ddlDepartment.SelectedIndex = -1;
        ddlDesignation.SelectedIndex = -1;

        rblStatus.SelectedIndex = -1;
        ddlRole.SelectedIndex = -1;
        chkboxUserCreate.Checked = false;
        chkboxUserview.Checked = false;
        chkboxUserupdate.Checked = false;
        chkboxUserdelete.Checked = false;
        chkboxEmpContractCreate.Checked = false;
        chkboxEmpContractView.Checked = false;
        chkboxEmpContractUpdate.Checked = false;
        chkboxEmpContractDelete.Checked = false;
        chkboxMailConfigUpdate.Checked = false;
        chckboxCreateComSite.Checked = false;
        chckboxViewComSite.Checked = false;
        chckboxUpdateComSite.Checked = false;
        chckboxDeleteComSite.Checked = false;
        ChkboxCreateProject.Checked = false;
        ChkboxViewProject.Checked = false;
        ChkboxUpdateProject.Checked = false;
        ChkboxDeleteProject.Checked = false;
        ChkBoxCreateUom.Checked = false;
        ChkBoxViewUom.Checked = false;
        ChkBoxUpdateUom.Checked = false;
        ChkBoxDeleteUom.Checked = false;
        chkboxMateriaCreate.Checked = false;
        chkboxMateriaView.Checked = false;
        chkboxMateriaUpdate.Checked = false;
        chkboxMaterialDelete.Checked = false;
        ChBoxVendorCreate.Checked = false;
        ChBoxVendorView.Checked = false;
        ChBoxVendorUpdate.Checked = false;
        ChBoxVendorDelete.Checked = false;
        chkboxSubContractorCreate.Checked = false;
        chkboxSubContractorView.Checked = false;
        chkboxSubContractorUpdate.Checked = false;
        chkboxSubContractorDelete.Checked = false;
        chkboxOtherCreate.Checked = false;
        chkboxOtherView.Checked = false;
        chkboxOtherUpdate.Checked = false;
        chkboxOtherDelete.Checked = false;
        chkBoxBudgetCreate.Checked = false;
        chkBoxBudgetView.Checked = false;
        chkBoxBudgetUpdate.Checked = false;
        chkBoxBudgetDelete.Checked = false;
        ChkProBudgetCreate.Checked = false;
        ChkProBudgetView.Checked = false;
        ChkProBudgetUpdate.Checked = false;
        ChkProBudgetDelete.Checked = false;
        ChkProBudgetAll.Checked = false;
        chkboxBudgetModREquestCreate.Checked = false;
        chkboxBudgetModREquestView.Checked = false;
        chkboxBudgetModREquestUpdate.Checked = false;
        chkboxBudgetModREquestDelete.Checked = false;
        ChkBoxReportView.Checked = false;
        chkboxQuotationCreate.Checked = false;
        chkboxQuotationView.Checked = false;
        chkboxQuotationUpdate.Checked = false;
        chkboxQuotationDelete.Checked = false;
        chkboxQuotationCompare.Checked = false;
        chkboxIndentCreate.Checked = false;
        chkboxIndentView.Checked = false;
        CheckBoxIndentUpdate.Checked = false;
        CheckBoxIndentDelete.Checked = false;
        ChkBoxPOCreate.Checked = false;
        ChkBoxView.Checked = false;
        ChkBoxPOUpdate.Checked = false;
        ChkBoxDelete.Checked = false;
        
        ChkBoxPayIndCreate.Checked = false;
        ChkBoxPayIndView.Checked = false;
        ChkBoxPayIndUpdate.Checked = false;
        ChkBoxPayIndDelete.Checked = false;
        ChkBoxPayIndVerCreate.Checked = false;
        ChkBoxPayIndVerView.Checked = false;
        ChkBoxPayIndVerUpdate.Checked = false;
        ChkBoxPayIndVerDelete.Checked = false;
        ChkBoxPayIndAppCreate.Checked = false;
        ChkBoxPayIndAppView.Checked = false;
        ChkBoxPayIndAppUpdate.Checked = false;
        ChkBoxPayIndAppDelete.Checked = false;

        chkboxPOReportView.Checked = false;
        chkboxStockCreate.Checked = false;
        chkboxStockView.Checked = false;
        chkboxStockUpdate.Checked = false;
        chkboxStockDelete.Checked = false;
        ChkBoxMRNCreate.Checked = false;
        chkboxQuotationtAll.Checked = false;
        ChkBoxMRNView.Checked = false;
        chkboxStockTransferAll.Checked = false;
        chkboxStockTransferCreate.Checked = false;
        chkboxStockTransferView.Checked = false;
        chkboxStockTransferUpdate.Checked = false;
        chkboxStockTransferDelete.Checked = false;
        ChkBoxMRNUpdate.Checked = false;
        ChkBoxMRNDelete.Checked = false;
        ChkBoxMINCreate.Checked = false;
        ChkBoxMINView.Checked = false;
        ChkBoxMINUpdate.Checked = false;
        ChkBoxMINDelete.Checked = false;
        ChkBoxINVReportsView.Checked = false;
        ChkBoxAssetRegCreate.Checked = false;
        ChkBoxAssetRegView.Checked = false;
        ChkBoxAssetRegUpdate.Checked = false;
        ChkBoxAssetRegDelete.Checked = false;
        ChkBoxAssetTransferCreate.Checked = false;
        ChkBoxAssetTransferView.Checked = false;
        ChkBoxAssetTransferUpdate.Checked = false;
        ChkBoxAssetTransferDelete.Checked = false;
        chkboxDailyRunHourKmCreate.Checked = false;
        chkboxDailyRunHourKmView.Checked = false;
        chkboxDailyRunHourKmUpdate.Checked = false;
        chkboxDailyRunHourKmDelete.Checked = false;
        ChkBoxAssetReportsView.Checked = false;
        
        chkboxUserAll.Checked = false;
        chkboxEmpContractAll.Checked = false;
        chkboxMailconfigAll.Checked = false;
        chckboxAllComSite.Checked = false;
        ChkboxAllProject.Checked = false;
        ChkBoxAllUom.Checked = false;
        chkboxMaterialAll.Checked = false;
        ChBoxVendorAll.Checked = false;
        chkboxSubContractorALL.Checked = false;
        chkboxOtherAll.Checked = false;
        chkBoxBudgetAll.Checked = false;
        chkboxBudgetModREquestALL.Checked = false;
        ChkBoxReportALL.Checked = false;
        chkboxQuotationtAll.Checked = false;
        chkboxIndentAll.Checked = false;
        ChkBoxPOAll.Checked = false;
        ChkBoxPayIndAll.Checked = false;
        ChkBoxPayIndVerAll.Checked = false;
        ChkBoxPayIndAppAll.Checked = false;
        chkboxPOReportAll.Checked = false;
        chkboxStockAll.Checked = false;
        chkboxMRNAll.Checked = false;
        ChkBoxMIN_All.Checked = false;
        ChkBoxINVReportsAll.Checked = false;
        ChkBoxAssetRegAll.Checked = false;
        ChkBoxAssetTransferAll.Checked = false;
        chkboxDailyRunHourKmAll.Checked = false;
        ChkBoxAssetReportsAll.Checked = false;

    }
    protected void Grid_Designation_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            UserBL objuserBL = new UserBL();

            objuserBL.Designation = Convert.ToString(e.Record["Designation"].ToString());
            objuserBL.Task = "DESIGNATION_DELETE";
            if (objuserBL.delete(con, UserBL.eLoadSp.DELETE_DESIGNATION))
            {
                BindDesignation();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Designation has been deleted sucessfully.');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Designation exists in one or more Users !');", true);
            }
            mpeDesig.Show();

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ds = new DataSet();
            objuserBL = new UserBL();
            objuserBL.Department = ddlDepartment.SelectedValue;
            objuserBL.load(con, UserBL.eLoadSp.RETRIVE_MODULEACCESS_ALL, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                chkboxUserCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["UserCreate"].ToString());
                chkboxUserview.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["UserView"].ToString());
                chkboxUserupdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["UserUpdate"].ToString());
                chkboxUserdelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["UserDelete"].ToString());
                chkboxEmpContractCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["EmpContract_Create"].ToString());
                chkboxEmpContractView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["EmpContract_View"].ToString());
                chkboxEmpContractUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["EmpContract_Update"].ToString());
                chkboxEmpContractDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["EmpContract_Delete"].ToString());
                chkboxMailConfigUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Mail_update"].ToString());
                chckboxCreateComSite.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["CompySite_Create"].ToString());
                chckboxViewComSite.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["CompySite_View"].ToString());
                chckboxUpdateComSite.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["CompySite_Update"].ToString());
                chckboxDeleteComSite.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["CompSite_Delete"].ToString());
                ChkboxCreateProject.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Proj_Create"].ToString());
                ChkboxViewProject.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Proj_View"].ToString());
                ChkboxUpdateProject.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Proj_Update"].ToString());
                ChkboxDeleteProject.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Proj_Delete"].ToString());

                ChkBoxCreateUom.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["UOM_Create"].ToString());
                ChkBoxViewUom.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["UOM_View"].ToString());
                ChkBoxUpdateUom.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["UOM_Update"].ToString());
                ChkBoxDeleteUom.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["UOM_Delete"].ToString());
                chkboxMateriaCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Material_Create"].ToString());
                chkboxMateriaView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Material_View"].ToString());
                chkboxMateriaUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Material_Update"].ToString());

                chkboxMaterialDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Material_Delete"].ToString());
                ChBoxVendorCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Vendor_Create"].ToString());
                ChBoxVendorView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Vendor_View"].ToString());
                ChBoxVendorUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Vendor_Update"].ToString());
                ChBoxVendorDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Vendor_Delete"].ToString());

                chkboxSubContractorCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Sub_Cont_Create"].ToString());
                chkboxSubContractorView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Sub_Cont_View"].ToString());
                chkboxSubContractorUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Sub_Cont_Update"].ToString());
                chkboxSubContractorDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Sub_Cont_Delete"].ToString());

                chkboxOtherCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Other_Create"].ToString());
                chkboxOtherView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Other_View"].ToString());
                chkboxOtherUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Other_Update"].ToString());
                chkboxOtherDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Other_Delete"].ToString());

                chkBoxBudgetCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Bug_Create"].ToString());
                chkBoxBudgetView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Bug_View"].ToString());
                chkBoxBudgetUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Bug_Update"].ToString());
                chkBoxBudgetDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Bug_Delete"].ToString());

                ChkProBudgetCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Pro_BudgetCreate"].ToString());
                ChkProBudgetView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Pro_BudgetView"].ToString());
                ChkProBudgetUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Pro_BudgetUpdate"].ToString());
                ChkProBudgetDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Pro_BudgetDelete"].ToString());

                chkboxBudgetModREquestCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Bug_Mod_Create"].ToString());
                chkboxBudgetModREquestView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Bug_Mod_View"].ToString());
                chkboxBudgetModREquestUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Bug_Mod_Update"].ToString());
                chkboxBudgetModREquestDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Bug_Mod_Delete"].ToString());
                ChkBoxReportView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Bug_Reports"].ToString());

                chkboxQuotationCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Quotn_Create"].ToString());
                chkboxQuotationView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Quotn_View"].ToString());
                chkboxQuotationUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Quotn_Update"].ToString());
                chkboxQuotationDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Quotn_Delete"].ToString());
                chkboxQuotationCompare.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Quotn_Compare"].ToString());
                chkboxIndentCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Ind_Create"].ToString());
                chkboxIndentView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Ind_View"].ToString());
                CheckBoxIndentUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Ind_Update"].ToString());
                CheckBoxIndentDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Ind_Delete"].ToString());
                ChkBoxPOCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["PO_Create"].ToString());
                ChkBoxView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["PO_View"].ToString());
                ChkBoxPOUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["PO_Update"].ToString());
                ChkBoxDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["PO_Delete"].ToString());

                ChkBoxPayIndCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_Create"].ToString());
                ChkBoxPayIndView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_View"].ToString());
                ChkBoxPayIndUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_Update"].ToString());
                ChkBoxPayIndDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_Delete"].ToString());
                ChkBoxPayIndVerCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_Ver_Create"].ToString());
                ChkBoxPayIndVerView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_Ver_View"].ToString());
                ChkBoxPayIndVerUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_Ver_Update"].ToString());
                ChkBoxPayIndVerDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_Ver_Delete"].ToString());
                ChkBoxPayIndAppCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_App_Create"].ToString());
                ChkBoxPayIndAppView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_App_View"].ToString());
                ChkBoxPayIndAppUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_App_Update"].ToString());
                ChkBoxPayIndAppDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_App_Delete"].ToString());

                chkboxStockCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Stock_Create"].ToString());
                chkboxStockView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Stock_View"].ToString());
                chkboxStockUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Stock_Update"].ToString());
                chkboxStockDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Stock_Delete"].ToString());
                chkboxStockTransferCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Stock_TransferCreate"].ToString());
                chkboxStockTransferView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Stock_TransferView"].ToString());
                chkboxStockTransferUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Stock_TransferUpdate"].ToString());
                chkboxStockTransferDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Stock_TransferDelete"].ToString());


                ChkBoxMRNUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["MRN_Update"].ToString());
                ChkBoxMRNDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["MRN_Delete"].ToString());
                chkboxPOReportView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Proc_Reports"].ToString());
                ChkBoxMRNCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["MRN_Create"].ToString());
                ChkBoxMRNView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["MRN_View"].ToString());
                ChkBoxMINCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["MIN_Create"].ToString());
                ChkBoxMINView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["MIN_View"].ToString());
                ChkBoxMINUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["MIN_Update"].ToString());
                ChkBoxMINDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["MIN_Delete"].ToString());
                ChkBoxINVReportsView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Inv_Reports"].ToString());
                ChkBoxAssetRegCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["AssReg_Create"].ToString());
                ChkBoxAssetRegView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["AssReg_View"].ToString());
                ChkBoxAssetRegUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["AssReg_Update"].ToString());
                ChkBoxAssetRegDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["AssReg_Delete"].ToString());
                ChkBoxAssetTransferCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["AssTra_Create"].ToString());
                ChkBoxAssetTransferView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["AssTra_View"].ToString());
                ChkBoxAssetTransferUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["AssTra_Update"].ToString());
                ChkBoxAssetTransferDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["AssTra_Delete"].ToString());
                chkboxDailyRunHourKmCreate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["DailRun_Create"].ToString());
                chkboxDailyRunHourKmView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["DailRun_View"].ToString());
                chkboxDailyRunHourKmUpdate.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["DailRun_Update"].ToString());
                chkboxDailyRunHourKmDelete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["DailRun_Delete"].ToString());
                ChkBoxAssetReportsView.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Ass_Rep"].ToString());

                chk_Local_MRN_Create.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Local_MRN_Create"].ToString());
                chk_Local_MRN_Update.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Local_MRN_Update"].ToString());
                chk_LocalMRN_View.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["LocalMRN_View"].ToString());
                chk_Local_MRN_Delete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Local_MRN_Delete"].ToString());
                chk_Appr_PO_Delete.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Appr_PO_Delete"].ToString());

            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }


    protected void btnCancelDesignation_Click(object sender, EventArgs e)
    {

        txtDesignation.Text = string.Empty;
        btnSaveDesignation.Text = "Save";
    }

    protected void btnCancelDepartment_Click(object sender, EventArgs e)
    {
        txtDeptName.Text = string.Empty;
        btnSaveDepartment.Text = "save";

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            Response.Redirect("../Admin/UsersList.aspx", false);
        }
        else
        {
            Clear();
        }
    }

    protected void imgBtnDigitalSign_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            DownloadFiles(ViewState["fileName"].ToString(), Server.MapPath(ConfigurationManager.AppSettings["DigitalSignImagepath"]));

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileReadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }

    //download the attachement 
    protected void DownloadFiles(string filename, string folderpath)
    {
        try
        {
            bool existfile = false;

            if (filename != string.Empty)
            {
                string[] filePaths = Directory.GetFiles(folderpath);
                foreach (string filechk in filePaths)
                {
                    if (filechk == folderpath + filename)
                    {
                        fileNamewithName = filechk;
                        existfile = true;
                        break;
                    }

                }
                if (existfile)
                {
                    Response.ContentType = ContentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                    Response.WriteFile(fileNamewithName);
                    // Response.End();
                    Response.Flush();


                    //System.Diagnostics.Process process = new System.Diagnostics.Process();
                    //process.StartInfo.UseShellExecute = true;
                    //process.StartInfo.FileName = fileNamewithName;
                    //process.Start();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render(' File not exists !');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }       
    }

    protected void chkIsCFOUser_CheckedChanged(object sender, EventArgs e)
    {
        //if(chkIsCFOUser.Checked)
        //{
        //    chk_Appr_PO_Delete.Enabled = true;
        //    chk_Appr_PO_Delete_All.Enabled = true;
        //}
        //else
        //{
        //    chk_Appr_PO_Delete.Checked = false;
        //    chk_Appr_PO_Delete_All.Checked = false;
        //    chk_Appr_PO_Delete.Enabled = false;
        //    chk_Appr_PO_Delete_All.Enabled = false;
        //}
    }
}




