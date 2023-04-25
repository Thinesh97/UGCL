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

public partial class EmployeeContractList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    EmployeeContractBL objViewECBL = null;
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
            objViewECBL = new EmployeeContractBL();
            //objViewECBL.ProjectCode = Session["Project_Code"].ToString();
            objViewECBL.Task = "EmployeeContractList";
            objViewECBL.load(con, EmployeeContractBL.eLoadSp.PRO_Employee_Contract_SELECT_ALL, ref ds);
            Grid_EC.DataSource = ds;
            Grid_EC.DataBind();
        }
        catch (Exception ex)
        {
            //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    
    protected void Grid_EC_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                HyperLink Editlik = e.Row.Cells[0].FindControl("lnkECNo") as HyperLink;
                Editlik.NavigateUrl = "~/Admin/EmployeeContract.aspx?EmployeeID=" + Editlik.Text;
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

                LinkButton lnkWO_Delete = e.Row.Cells[15].FindControl("lnkEC_Delete") as LinkButton;
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
    
    protected void Grid_EC_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objViewECBL = new EmployeeContractBL();
            objViewECBL.Employee_ContractID = Convert.ToInt32(e.Record["Employee_ContractID"].ToString());
            if (objViewECBL.delete(con, EmployeeContractBL.eLoadSp.EC_Delete))
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
    
    protected void lnkEC_Delete_Click(object sender, CommandEventArgs e)
    {
        try
        {
            if (HF_Confirm.Value != "false")
            {
                //type=   RegisterStartupScript(typeof(Page), "exampleScript", "if(confirm('are you confirm?')) { document.getElementById('btn').click(); } ", true)
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                Hashtable dataItem = Grid_EC.Rows[rowIndex].ToHashtable() as Hashtable;

                objViewECBL = new EmployeeContractBL();

                objViewECBL.EmployeeID = dataItem["EmployeeID"].ToString();
                objViewECBL.Task = "DeleteEmployeeContract";
                if (objViewECBL.delete(con, EmployeeContractBL.eLoadSp.EC_Delete))
                {
                    EmployeeContractBindList();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Employee Contract has been deleted successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Employee Contract is refered other process!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
}


