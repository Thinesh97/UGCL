using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;
using SNC.ErrorLogger;


public partial class BudgetModifyRequest : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);

    BudgetModiRequestBL objbudmodreqBL = null;
    DataSet ds = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    BindProject();
                    if (Session["Project_Code"] != null)
                    {
                        ddlProjectName.SelectedValue = Session["Project_Code"].ToString();
                    }
                    ddlProjectName.Enabled = false;
                    ddlProjectName_SelectedIndexChanged(null, null);
                    txtRequestedBy.Text = Session["Name"].ToString();
                    txtRequestedBy.Enabled = false;

                    if (Request.QueryString["ID"] != null)
                    {
                        GetBudgetDetail(Convert.ToInt32(Request.QueryString["ID"]));
                        ddlProjectName_SelectedIndexChanged(null, null);
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
    protected void BindProject()
    {
        ProjectBL objprojectbl = new ProjectBL();
        try
        {
            ds = new DataSet();
            objprojectbl = new ProjectBL();
            //objprojectbl.UserID = Convert.ToInt32(Session["UID"]);
            objprojectbl.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_ALL, ref ds);
            ddlProjectName.DataSource = ds;
            ddlProjectName.DataTextField = "Project_Name";
            ddlProjectName.DataValueField = "Project_Code";
            ddlProjectName.DataBind();
            ddlProjectName.Items.Insert(0, "-Select-");


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void GetBudgetDetail(int budMrid)
    {
        try
        {
            objbudmodreqBL = new BudgetModiRequestBL();
            ds = new DataSet();
            objbudmodreqBL.Budget_MR_ID = budMrid;

            objbudmodreqBL.load(con, BudgetModiRequestBL.eLoadSp.BUDMOD_REQUEST_BYID, ref ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlProjectName.SelectedValue = ds.Tables[0].Rows[0]["Project_Code"].ToString();  
                ddlMonthlyBudget.SelectedValue = ds.Tables[0].Rows[0]["Monthly_Budget"].ToString();
                ddlMonthlyBudget.Enabled = false;
                txtRequestedBy.Text = ds.Tables[0].Rows[0]["Request_By"].ToString();
                txtApprovedBy.Text = ds.Tables[0].Rows[0]["Approved_byName"].ToString();
                txtReason.Text = ds.Tables[0].Rows[0]["Reason"].ToString();
                btnSubmit.Text = "Update";
                btnCancel.Text = "Cancel";
                ViewState["ViewSta_Budget_MR_ID"] = ds.Tables[0].Rows[0]["Budget_MR_ID"].ToString();
                ViewState["Primary_Person"] = ds.Tables[0].Rows[0]["Primary_Person"].ToString();

            }

        }


        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {

            //if (DateTime.Now.Date.DayOfWeek.ToString() != "Sunday" && Convert.ToInt32(DateTime.Now.Day) > 24)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('You can apply the budget modification till 24 ! ');", true);
            //    return;
            //}
            //else if (DateTime.Now.Date.DayOfWeek.ToString() == "Sunday" && Convert.ToInt32(DateTime.Now.Day) > 25)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('You can apply the budget modification till 24 !');", true);
            //    return;
            //}
            


            BudgetModiRequestBL objbudgetmodireqBL = new BudgetModiRequestBL();
            if (ddlMonthlyBudget.SelectedIndex != 0)
            {
                objbudgetmodireqBL.Project_Code = ddlProjectName.SelectedValue;

            }
            else
            {
                objbudgetmodireqBL.Project_Code = null;
            }
            //objbudgetmodireqBL.Budget_Sector = ddlBudgetSector.Text;
            if (ddlMonthlyBudget.SelectedIndex != 0)
            {
                objbudgetmodireqBL.Monthly_Budget = ddlMonthlyBudget.SelectedValue;

            }


            objbudgetmodireqBL.Requested_By = Convert.ToInt32(Session["UID"]);
            objbudgetmodireqBL.Reason = txtReason.Text;
            objbudgetmodireqBL.Approved_By = ViewState["Primary_Person"] != null ? Convert.ToInt32(ViewState["Primary_Person"]) : Convert.ToInt32(Session["UID"]);

            if (btnSubmit.Text == "Submit")
            {
                objbudgetmodireqBL.insert(con, BudgetModiRequestBL.eLoadSp.INSERT_BUDMOD_REQUEST);
                if (objbudgetmodireqBL.result == "s")
                {
                    ddlMonthlyBudget.Enabled = false;
                    btnSubmit.Text = "Update";
                    btnCancel.Text = "Cancel";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Budget Modification Details Inserted Successfully');", true);
                }
                else if (objbudgetmodireqBL.result == "a")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Budget modification already exists!');", true);
                }
                else if (objbudgetmodireqBL.result == "e")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('You can apply the budget modification till month end of budget!');", true);
                }
            }
            else
            {
                objbudgetmodireqBL.Budget_MR_ID = int.Parse(ViewState["ViewSta_Budget_MR_ID"].ToString());

                if (objbudgetmodireqBL.update(con, BudgetModiRequestBL.eLoadSp.UPDATE_BUDMOD_REQUEST))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Budget Modification Request Updated successfully');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }





    protected void ddlMonthlyBudget_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlMonthlyBudget.SelectedIndex > 0)
            {
                if (ddlProjectName.SelectedIndex != 0)
                {
                    BudgetModiRequestBL objbudmodreqBL = new BudgetModiRequestBL();
                    ds = new DataSet();
                    objbudmodreqBL = new BudgetModiRequestBL();
                    objbudmodreqBL.load(con, BudgetModiRequestBL.eLoadSp.SELECT_MONTHLYBUDGET, ref ds);


                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();
                        dt = ds.Tables[0];

                        DataTable Draftdt = dt.AsEnumerable().Where(r => r.Field<int>("Abs_BID") == Convert.ToInt32(ddlMonthlyBudget.SelectedValue)).CopyToDataTable();
                        txtApprovedBy.Text = ds.Tables[0].Rows[0]["PrimaryPersonName"].ToString();
                        ViewState["Primary_Person"] = ds.Tables[0].Rows[0]["Primary_Person"].ToString();
                    }

                }
            }
            else
            {
                txtApprovedBy.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }
    private void ResetBudgetModifiedRequestItems()
    {

        ddlProjectName.SelectedValue = ddlProjectName.Items.FindByText("-select-").Value;
        ddlMonthlyBudget.SelectedValue = ddlMonthlyBudget.Items.FindByText("-select-").Value;
        //ddlBudgetSector.SelectedValue = ddlBudgetSector.Items.FindByText("-select-").Value;
        txtRequestedBy.Text = string.Empty;
        txtApprovedBy.Text = string.Empty;
        txtReason.Text = string.Empty;

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (((Button)sender).Text == "Clear")
        {
           // ddlBudgetSector.SelectedIndex = -1;
            txtApprovedBy.Text = string.Empty;
            txtReason.Text = string.Empty;
            ddlMonthlyBudget.SelectedIndex = -1;
           

        }
        else
        {
            Response.Redirect("../Budget/BudgetModifyRequestList.aspx", false);
        }
    }

    protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProjectName.SelectedIndex != 0)
            {
                BudgetModiRequestBL objbudmodreqBL = new BudgetModiRequestBL();
                ds = new DataSet();
                objbudmodreqBL = new BudgetModiRequestBL();
                objbudmodreqBL.load(con, BudgetModiRequestBL.eLoadSp.SELECT_MONTHLYBUDGET, ref ds);


                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];

                    bool exists = dt.AsEnumerable().Where(r => r.Field<string>("Project_Code").Equals(ddlProjectName.SelectedValue)).Count() > 0;
                    if (exists)
                    {
                        DataTable Draftdt = dt.AsEnumerable().Where(r => r.Field<string>("Project_Code") == ddlProjectName.SelectedValue).CopyToDataTable();
                        ddlMonthlyBudget.DataSource = Draftdt;
                        ddlMonthlyBudget.DataTextField = "Budget_ID";
                        ddlMonthlyBudget.DataValueField = "Abs_BID";
                        ddlMonthlyBudget.DataBind();
                        ddlMonthlyBudget.Items.Insert(0, "-Select-");
                    }
                    else
                    {
                        ddlMonthlyBudget.Items.Clear();
                        ddlMonthlyBudget.Items.Insert(0, "-Select-");
                    }
                }
            }
            else
            {
                ddlMonthlyBudget.Items.Clear();
                ddlMonthlyBudget.Items.Insert(0, "-Select-");
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }
}
