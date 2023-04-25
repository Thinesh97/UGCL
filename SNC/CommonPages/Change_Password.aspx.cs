using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Collections;
using System.Configuration;
using Bussinesslogic;
using BusinessLayer;
using DataAccess;
using SNC.ErrorLogger;

public partial class Change_Password : System.Web.UI.Page
{
    UserBL ObjUserBL = null;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);

    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["Name"] != null && Session["User_ID"] != null)
            {
                Label_name.Text = Session["Name"].ToString();

                txtuid.Text = Session["User_ID"].ToString();


                if (!IsPostBack)
                {
                    txtold.Text = "";
                    txtnew.Text = "";
                    txt_confirm.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    public void getusername()
    {
        UserBL objuserBL = new UserBL();
        objuserBL.id = Session["Name"].ToString();

        try
        {
            ds = new DataSet();
            ObjUserBL = new UserBL();
            ObjUserBL.Name = Session["Name"].ToString();

            {


            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }

    }



    protected void btn_ChangePassword_Click(object sender, EventArgs e)
    {
        bool oldpwdmatch = false;
        ObjUserBL = new UserBL();

        Labelcheck.Visible = false;
        try
        {

            if (Session["User_ID"] != null)
            {
                ds = new DataSet();
                txtuid.Text = Session["User_ID"].ToString();
                ObjUserBL.UserID = txtuid.Text.Trim();
                ObjUserBL.Task = "GetUsername";
                ObjUserBL.load(con, UserBL.eLoadSp.GET_USER_NAME, ref ds);


                if (ds.Tables[0].Rows.Count > 0)
                {

                    oldpwdmatch = clsLoginEncDec_BLL.VerifyHash(txtold.Text, "SHA512", ds.Tables[0].Rows[0]["Password"].ToString());

                    if (oldpwdmatch)
                    {
                        ObjUserBL = new UserBL();


                        if (txtnew.Text == txt_confirm.Text)
                        {
                            ObjUserBL = new UserBL();

                            ObjUserBL.UserID = txtuid.Text;
                            ObjUserBL.Password = clsLoginEncDec_BLL.ComputeHash(txtnew.Text.Trim(), "SHA512", null);
                            ObjUserBL.Task = "ChangePass";
                            ObjUserBL.CHANGEPASSWORD_UPDATE(con, UserBL.eLoadSp.CHANGEPASSWORD);

                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Password Changes Successfully...!');", true);
                                Response.Redirect("../CommonPages/Login.aspx", false);
                            }

                        }
                        else if (txtnew.Text != txt_confirm.Text)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Both new password and confirm password must be same...!');", true);
                            // ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Mail Config Failed to create');", true);
                        }
                    }




                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please Provide Correct Old Password!');", true);

                    }
                }


                else
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Check Username and Old Password');", true);
                }
            }


            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Login Failed Server Problem');", true);
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

}

