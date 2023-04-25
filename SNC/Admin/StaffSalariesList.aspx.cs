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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections;


public partial class StaffSalariesList : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    
    StaffSalariesBL objViewSS = null;
    DataSet ds = null;
    DataSet Accessds = new DataSet();


    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["UID"] != null)
            {
                ActionPermission();
                if (!IsPostBack)
                {
                    EmployeeContractBindList();
                }
            }
            else
            {
                Response.Redirect("../CommonPages/Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
                        if (Convert.ToBoolean(Accessds.Tables[0].Rows[0]["EmpContract_Create"].ToString()))
                        {
                            lnkbtnAdd.Visible = true;
                        }
                        else
                        {
                            lnkbtnAdd.Visible = false;
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
            //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void EmployeeContractBindList()
    {
        try
        {
            ds = new DataSet();
            
            objViewSS = new StaffSalariesBL();
            //objViewECBL.ProjectCode = Session["Project_Code"].ToString();
            objViewSS.Task = "StaffSalaryList";
            objViewSS.load(con, StaffSalariesBL.eLoadSp.PRO_Staff_Salaries_SELECT_ALL, ref ds);
            Grid_SS.DataSource = ds;
            Grid_SS.DataBind();
        }
        catch (Exception ex)
        {
            //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_SS_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                HyperLink Editlik = e.Row.Cells[1].FindControl("lnkSSNo") as HyperLink;

                
                int staffid=Convert.ToInt32(e.Row.Cells[0].Text);
                Editlik.NavigateUrl = "~/Admin/StaffSalaries.aspx?EmployeeID=" + Editlik.Text + "&Staff_id="+ staffid;
                if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                {
                    if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                    {
                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["EmpContract_Update"].ToString()))
                        {
                            Editlik.NavigateUrl = "";
                        }
                    }
                }

                LinkButton lnkWO_Delete = e.Row.Cells[16].FindControl("lnkSS_Delete") as LinkButton;
                if (Accessds.Tables[0].Rows[0]["EmpContract_Delete"].ToString() == "" || Accessds.Tables[0].Rows[0]["EmpContract_Delete"].ToString() == "False" || Accessds.Tables[0].Rows[0]["EmpContract_Delete"].ToString() == "0")
                {
                    if (e.Row.Cells[13].Text == "Approved")
                    {
                        lnkWO_Delete.Visible = false;
                    }
                    else
                    {
                        lnkWO_Delete.Visible = true;
                    }
                }
                else
                {
                    lnkWO_Delete.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_SS_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objViewSS = new StaffSalariesBL();
            objViewSS.Staff_SalaryID = Convert.ToInt32(e.Record["Staff_SalaryID"].ToString());
            if (objViewSS.delete(con, StaffSalariesBL.eLoadSp.SS_Delete))
            {
                EmployeeContractBindList();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Employee Contract has been deleted successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('EmployeeContract is refered other process!');", true);
            }
        }
        catch (Exception ex)
        {
            //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    protected void btn_GenECItemsList_Click(object sender, EventArgs e)
    {
        BindECItemsList();
        ItemList_Gv.Visible = true;
    }

    protected void BindECItemsList()
    {
        try
        {
            //ds = new DataSet();
            //objViewWOBL = new WorkOrderBL();
            //if (Session["Project_Code"] != null)
            //{
            //    objViewWOBL.ProjectCode = Session["Project_Code"].ToString();
            //    objViewWOBL.load(con, WorkOrderBL.eLoadSp.SELECT_WO_ITEMS_ALL_BY_PROJECT, ref ds);
            //    Gv_WOItemsList.DataSource = ds;
            //    Gv_WOItemsList.DataBind();
            //}
        }
        catch (Exception ex)
        {
            //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    //protected void lnkSS_Delete_Click(object sender, CommandEventArgs e)
    //{
    //    try
    //    {
    //        if (HF_Confirm.Value != "false")
    //        {
    //            //type=   RegisterStartupScript(typeof(Page), "exampleScript", "if(confirm('are you confirm?')) { document.getElementById('btn').click(); } ", true)
    //            int rowIndex = int.Parse(e.CommandArgument.ToString());
    //            Hashtable dataItem = Grid_SS.Rows[rowIndex].ToHashtable() as Hashtable;

    //            objViewSS = new StaffSalariesBL();

    //            objViewSS.EmployeeID = dataItem["EmployeeID"].ToString();
    //            objViewSS.Task = "DeleteEmployeeContract";
    //            if (objViewSS.delete(con, StaffSalariesBL.eLoadSp.SS_Delete))
    //            {
    //                EmployeeContractBindList();
    //                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Employee Contract has been deleted successfully');", true);
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Employee Contract is refered other process!');", true);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
    //    }
    //}

    protected void lnkSS_Delete_Command(object sender, CommandEventArgs e)
    {
        try
        {
            if (HF_Confirm.Value != "false")
            {
                //type=   RegisterStartupScript(typeof(Page), "exampleScript", "if(confirm('are you confirm?')) { document.getElementById('btn').click(); } ", true)
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                Hashtable dataItem = Grid_SS.Rows[rowIndex].ToHashtable() as Hashtable;

                objViewSS = new StaffSalariesBL();

                objViewSS.EmployeeID = dataItem["EmployeeID"].ToString();
                objViewSS.Task = "DeleteStaffSalary";
                if (objViewSS.delete(con, StaffSalariesBL.eLoadSp.SS_Delete))
                {
                    EmployeeContractBindList();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Staff salary Details has been deleted successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Staff salary Details is refered other process!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    
}
