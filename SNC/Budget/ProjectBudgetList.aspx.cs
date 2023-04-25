using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SNC.ErrorLogger;
using BusinessLayer;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ProjectBudgetList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    DataSet ds = null;
    DataSet Accessds = new DataSet(); 
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Session["UID"] != null)
        {
            ActionPermission();
            if (!IsPostBack)
            {
                BindProjectBudgets();

                if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                {
                    if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin" && Accessds.Tables[0].Rows[0]["Pro_BudgetView"].ToString() != string.Empty && !Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Pro_BudgetView"].ToString()))
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

                if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                {
                    if (Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Pro_BudgetCreate"].ToString()))
                    {
                        lnkbtnAdd.Visible = true;
                    }
                    else
                    {
                        lnkbtnAdd.Visible = false;
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

    protected void BindProjectBudgets()
    {

        try
        {
            ProjectBudgetBL objProjBudBL = new ProjectBudgetBL();

            ds = new DataSet();
            if (!string.IsNullOrEmpty(Session["Project_Code"].ToString()))
            {
                objProjBudBL.Project_ID = Session["Project_Code"].ToString();
                objProjBudBL.load(con, ProjectBudgetBL.eLoadSp.SELECT_ALL_PROJECT_BUDGETS, ref ds);
                if(ds.Tables.Count > 0)
                {
                    Gv_ProjectBudgetList.DataSource = ds;
                    Gv_ProjectBudgetList.DataBind();
                }
                else
                {
                    Gv_ProjectBudgetList.DataSource = null;
                    Gv_ProjectBudgetList.DataBind();
                }
                
            }
           
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

   
}
