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


public partial class Forget_Password : System.Web.UI.Page
{
   
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    UserBL ObjUserBL = null;
    DataSet ds = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        // generaterandompassword();
    }
    protected static string generaterandompassword()
    {
        string allowedChars = "";
        allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
        allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
        allowedChars += "1,2,3,4,5,6,7,8,9,0,!,@,#,$,%,&,?";
        char[] sep = { ',' };
        string[] arr = allowedChars.Split(sep);
        string passwordString = "";
        string temp = "";
        Random rand = new Random();
        for (int i = 0; i < Convert.ToInt32(8); i++)
        {
            temp = arr[rand.Next(0, arr.Length)];
            passwordString += temp;
        }
        return passwordString;

    }

    protected void btnSendmail_Click(object sender, EventArgs e)
    {
        try
        {

            string randpwd = generaterandompassword();
            string encryrandpwd = clsLoginEncDec_BLL.ComputeHash(randpwd, "SHA512", null);

            ObjUserBL = new UserBL();
            ds = new DataSet();
            ObjUserBL.UserID = txtUserId.Text;
            ObjUserBL.Email_ID = txtEmailId.Text;


            ObjUserBL.Password = encryrandpwd;
            ObjUserBL.Task = "UPDATEFORGOTPASSWORD";

            if (ObjUserBL.updateNewPassword(con, UserBL.eLoadSp.UPDATE_NEW_PASSWORD))
            {
                ObjUserBL.UserID = txtUserId.Text;
                ObjUserBL.Email_ID = txtEmailId.Text;
                ObjUserBL.Task = "GetPassword";
                ObjUserBL.load(con, UserBL.eLoadSp.FORGOTPASSWORD_GET_PASSWORD, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    string name = ds.Tables[0].Rows[0]["Name"].ToString();
                    string password = ds.Tables[0].Rows[0]["Password"].ToString();

                    //  ObjUserBL.UserID = string.Empty;

                    ObjUserBL.Task = "CheckMail";
                    ObjUserBL.load(con, UserBL.eLoadSp.CHECK_MAIL, ref ds);


                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        ObjUserBL.Password = encryrandpwd;
                        ObjUserBL.Email_ID = txtEmailId.Text;
                        ObjUserBL.UserID = txtUserId.Text;
                        MailMessage mObj = new MailMessage();
                        mObj.From = new MailAddress(ds.Tables[0].Rows[0]["Email"].ToString());
                        mObj.To.Add(txtEmailId.Text);
                        mObj.Subject = "Your Password Details";
                        mObj.Body = "Hi " + name + ", <br/><br/>Please check your new temporary Password Details<br/><br/>Your Password: " + randpwd + "<br/> <br/> Please remember to change this temporary password on first log in.<br/>";
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
                        Response.Write("<script>alert('Password Details Sent Successfully.')</script>");
                        txtUserId.Text = string.Empty;
                        txtEmailId.Text = string.Empty;


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

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }


    }
    protected void btnBack_Click1(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../CommonPages/Login.aspx", false);
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
}




