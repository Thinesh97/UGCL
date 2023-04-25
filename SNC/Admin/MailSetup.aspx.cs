using DataAccess;
using SNC.ErrorLogger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class MailSetup : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    MailConfig objMailConfig = null;
    DataSet ds = null;
    DataSet Accessds = new DataSet();      
    //SqlDataReader dr = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UID"] != null)
            {
                ActionPermission();
                this.txtPassword.Attributes.Add("value", this.txtPassword.Text);
                if (!IsPostBack)
                {

                    GetMailConfigDetails();
                    if (!(String.IsNullOrEmpty(txtPassword.Text.Trim())))
                    {
                        txtPassword.Attributes["value"] = txtPassword.Text;
                    }
                    if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                    {
                        if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin" && !Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Mail_update"].ToString()))
                        {
                            Response.Redirect("~/CommonPages/Home.aspx", false);
                        }
                    }
                    else
                    {
                        Response.Redirect("~/CommonPages/Login.aspx", true);
                    }
                }
            }
            else
            {
                Response.Redirect("../CommonPages/Login.aspx", false);
            }
        }
        catch(Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void ActionPermission()
    {
        try
        {
            if (Session["ActionAccess"] != null)
            {
                Accessds = (DataSet)Session["ActionAccess"];
            }
            if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
            {
                if (Accessds.Tables[0].Rows.Count > 0)
                {
                    if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                    {
                        if (Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Mail_update"].ToString()))
                        {
                            btnSubmit.Visible = true;
                        }
                        else
                        {
                            btnSubmit.Visible = false;
                        }
                    }
                }
            }

            else
            {
                Response.Redirect("~/CommonPages/Login.aspx", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }



    private void GetMailConfigDetails()
    {
        try
        {
            objMailConfig = new MailConfig();

            ds = new DataSet();
            //dr = new SqlDataReader();
            if (objMailConfig.load(con, MailConfig.eLoadSp.SELECT_ALL, ref ds))
            {


                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtSMTPhost.Text = ds.Tables[0].Rows[0]["SMTPHost"].ToString();
                    txtSMTPNo.Text = ds.Tables[0].Rows[0]["Port_no"].ToString();
                    txtEmailID.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                    txtPassword.Text = ds.Tables[0].Rows[0]["Password"].ToString();
                    rblSSL.SelectedValue = ds.Tables[0].Rows[0]["SSL_able"].ToString();
                }
            }
        }
        catch (Exception ex)
        {

            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objMailConfig = new MailConfig();
            objMailConfig.SMTPHost = txtSMTPhost.Text.Trim().ToString();
            objMailConfig.PortNo = Convert.ToInt16(txtSMTPNo.Text.Trim().ToString());
            objMailConfig.EmailID = txtEmailID.Text.Trim().ToString();
            objMailConfig.Password = txtPassword.Text.Trim().ToString();
            objMailConfig.SSLAble = rblSSL.SelectedValue.Trim().ToString();

            if (objMailConfig.update(con, MailConfig.eLoadSp.UPDATE))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Mail Config Updated Sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Mail Config Failed to Updated');", true);
            }

        }
        catch (Exception ex)
        {

            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }


}

