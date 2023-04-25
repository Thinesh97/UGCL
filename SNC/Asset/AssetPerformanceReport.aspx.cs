using BusinessLayer;
using iTextSharp.text.pdf;
using SNC.ErrorLogger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AssetPerformanceReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    DataSet ds = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UID"] == null)
            {
                Response.Redirect("~/CommonPages/Login.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                BindProject();
                Display_Columns_List();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindProject()
    {
        ProjectBL objprojectbl = new ProjectBL();
        try
        {
            ds = new DataSet();
            objprojectbl = new ProjectBL();
            if (Session["Project_Code"] != null)
            {
                objprojectbl.Project_Code = Session["Project_Code"].ToString();

                objprojectbl.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_BY_Project_Code, ref ds);

                ddlProjectName.DataSource = ds;
                ddlProjectName.DataTextField = "Project_Name";
                ddlProjectName.DataValueField = "Project_Code";
                ddlProjectName.DataBind();
                ddlProjectName.Enabled = false;
                ddlProjectName.SelectedValue = Session["Project_Code"].ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Display_Columns_List()
    {
            lstColumns.Items.Clear();
            lstColumns.Items.Add("Asset Type");
            lstColumns.Items.Add("Asset Category");
            lstColumns.Items.Add("Asset Name");
            lstColumns.Items.Add("Make");
            lstColumns.Items.Add("Asset Code");
            lstColumns.Items.Add("Reg No");
            lstColumns.Items.Add("Working Area");
            lstColumns.Items.Add("Start Hour");
            lstColumns.Items.Add("End Hour");
            lstColumns.Items.Add("Hour Duration");
            lstColumns.Items.Add("Start Km");
            lstColumns.Items.Add("End Km");
            lstColumns.Items.Add("Distance");
            lstColumns.Items.Add("Issued Diesel");
            lstColumns.Items.Add("Fuel consumption in Ltrs");
            lstColumns.Items.Add("Standard Avg fuel consumption");
            lstColumns.Items.Add("Act Avg Fuel Consumption I/P");
            lstColumns.Items.Add("Variance (+Fav / -Unfav)");
            lstColumns.Items.Add("Actual Output");
            lstColumns.Items.Add("Standard Avg Output");
            lstColumns.Items.Add("Act Avg Output");
            lstColumns.Items.Add("Variance (+Fav / -Unfav)");
            lstColumns.Items.Add("Cost of fuel Consumption");
            lstColumns.Items.Add("Cost of Prevent Maintenance");
            lstColumns.Items.Add("Cost of Schedule Maintenance");
            lstColumns.Items.Add("Cost of Other");
            lstColumns.Items.Add("Total Maintenance Cost");
            lstColumns.Items.Add("Total cost");

        for (int i = 0; i < lstColumns.Items.Count; i++)
            {
                lstColumns.Items[i].Selected = true;
            }
        }


    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlProjectName.SelectedValue != "")
            {
                Session["Projects"] = ddlProjectName.SelectedValue.ToString();
            }
            if (txtStartDate.Text.Trim() != "")
            {
                Session["FromDate"] = txtStartDate.Text.Trim();
            }
            if (txtEndDate.Text.Trim() != "")
            {
                Session["ToDate"] = txtEndDate.Text.Trim();
            }
           
            string display_Columns = string.Empty;
            foreach (ListItem listItem in lstColumns.Items)
            {
                if (listItem.Selected)
                {
                    if (display_Columns == string.Empty)
                    {
                        display_Columns = listItem.Value;
                    }
                    else
                    {
                        display_Columns += "," + listItem.Value;
                    }
                }
            }
            Session["Display_Columns"] = display_Columns.ToString();

            Session["Report_Flag"] = "AssetReport";
            string popupvariable = "<script language='javascript'>" + "window.open('/Reports/UGCL_ReportViewer.aspx','','');</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "PopUpWindow", popupvariable, false);
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            ddlProjectName.SelectedIndex = 0;
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
}
