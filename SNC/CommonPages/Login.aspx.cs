using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Collections;
using System.Configuration;
using Bussinesslogic;
using BusinessLayer;
using DataAccess;
using SNC.ErrorLogger;
using Obout.Grid;
using System.Security.Cryptography;

public partial class Login : System.Web.UI.Page
{
    UserBL ObjUserBL = null;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);

    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack || IsAsync))
        {
            string returnUrl = Request.QueryString["ReturnUrl"];
            if (returnUrl != null)
                if (returnUrl == "/")
                {
                    Response.Redirect("~/CommonPages/Login.aspx", false);
                }
            if (Request.QueryString["status"] != null)
            {
                lblStatus.Text = Request.QueryString["status"].ToString();

            }
        }

    }


    protected void LoginSNC_Authenticate(object sender, AuthenticateEventArgs e)
    {
        bool oldpwdmatch = false;


        try
        {
            ObjUserBL = new UserBL();
            ds = new DataSet();
            ObjUserBL.UserID = LoginSNC.UserName.Trim();
            ObjUserBL.Task = "GetUsername";
            ObjUserBL.load(con, UserBL.eLoadSp.GET_USER_NAME, ref ds);

            if (ds.Tables[0].Rows.Count > 0)
            {

                oldpwdmatch = clsLoginEncDec_BLL.VerifyHash(LoginSNC.Password, "SHA512", ds.Tables[0].Rows[0]["Password"].ToString());

                if (oldpwdmatch)
                {

                    Session["UID"] = ds.Tables[0].Rows[0]["UID"].ToString();
                    Session["User_ID"] = ds.Tables[0].Rows[0]["UserID"].ToString();
                    Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                    Session["Email_ID"] = ds.Tables[0].Rows[0]["Email_ID"].ToString();
                    Session["Empid"] = ds.Tables[0].Rows[0]["Employee_ID"].ToString();
                    Session["Role"] = ds.Tables[0].Rows[0]["Role"].ToString();
                    Session["IsHoUser"] = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsHoUser"]);
                    //if (ds.Tables[0].Rows[0]["Is_CFO_User"].ToString() != "")
                    //{
                    //    Session["Is_CFO_User"] = Convert.ToBoolean(ds.Tables[0].Rows[0]["Is_CFO_User"]);
                    //}
                    //else
                    //{
                    //    Session["Is_CFO_User"] = false;
                    //}

                    if (Session["Role"].ToString() == "Application Admin" || Convert.ToBoolean(Session["IsHoUser"]))
                    {
                        LoginSNC.DestinationPageUrl = "~/CommonPages/ProjectSelection.aspx";

                    }
                    else
                    {
                        //Session["Project_Code"] = ds.Tables[0].Rows[0]["Project_Code"].ToString();                 
                        //LoginSNC.DestinationPageUrl = "~/CommonPages/Home.aspx";
                        LoginSNC.DestinationPageUrl = "~/CommonPages/ProjectSelection.aspx";
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Wrong password Entered');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Do not have application access or status is inactive');", true);
            }
            e.Authenticated = oldpwdmatch;
        }

        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
        // Response.Redirect("~/CommonPages/Home.aspx", false);
    }

    protected void btnPro_Click(object sender, EventArgs e)
    {
        LoginSNC.DestinationPageUrl = "~/CommonPages/Home.aspx";
    }

}
