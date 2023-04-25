using BusinessLayer;
using iTextSharp.text;
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


public partial class AwaitingStockRequest : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);

    StockTransferBL objStockTranBL = null;
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    BindStockTransferRequestList();
                }
                else
                {
                    Response.Redirect("../CommonPages/Login.aspx", false);
                }
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    /// <summary>
    /// Binding All the stock transfer items based on the Logged in User & Grouped by Budget Sector
    /// </summary>
    private void BindStockTransferRequestList()
    {
        try
        {
            objStockTranBL = new StockTransferBL();
            ds = new DataSet();
            if (Session["UID"] != null)
            {
                objStockTranBL.Stock_Receiver = Convert.ToInt32(Session["UID"]);
                objStockTranBL.FromProjectCode = Session["Project_Code"].ToString();
                objStockTranBL.load(con, StockTransferBL.eLoadSp.SELECT_STOCK_TRANSFER_REQUEST_FOR_USER, ref ds);
                if (ds.Tables.Count > 0)
                {
                    Gv_StockTransferRequest.DataSource = ds;
                    Gv_StockTransferRequest.DataBind();
                }
                else
                {
                    Gv_StockTransferRequest.DataSource = null;
                    Gv_StockTransferRequest.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    /// <summary>
    /// Binding All the items based on the Budget Sector & Logged in User
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Lnkbtn_SectorName_Click(object sender, EventArgs e)
    {
        try
        {

            if (((LinkButton)sender).Text != null)
            {
                ViewState["BudgetSector"] = ((LinkButton)sender).Text.ToString();
                ViewState["FromProjectCode"] = ((LinkButton)sender).CommandArgument.ToString();
            }

            BindStockTransferItems();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void BindStockTransferItems()
    {
        try
        {
            objStockTranBL = new StockTransferBL();
            ds = new DataSet();
            DataTable dt = new DataTable();

            if (ViewState["BudgetSector"] != null)
            {
                objStockTranBL.Budget_Sector = ViewState["BudgetSector"].ToString();
            }
            if (ViewState["FromProjectCode"] != null)
            {
                objStockTranBL.FromProjectCode = ViewState["FromProjectCode"].ToString();
            }
            if (Session["UID"] != null)
            {
                objStockTranBL.Stock_Receiver = Convert.ToInt32(Session["UID"]);
                objStockTranBL.ToProjectCode = Session["Project_Code"].ToString();
            }
            objStockTranBL.load(con, StockTransferBL.eLoadSp.SELECT_SECTORWISEITEMS_FROM_STOCK_TRANSFER_FOR_USER, ref ds);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                Gv_StockTranferItems.DataSource = dt;
                Gv_StockTranferItems.DataBind();
            }
            else
            {
                Gv_StockTranferItems.DataSource = null;
                Gv_StockTranferItems.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    /// <summary>
    /// Inserting Selected Items from the Gridview into Stock Table and also updating the Project Code
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_AcceptTransferItems_Click(object sender, EventArgs e)
    {
        try
        {
            objStockTranBL = new StockTransferBL();
            if (Gv_StockTranferItems.SelectedRecords != null)
            {
                foreach (Hashtable ht in Gv_StockTranferItems.SelectedRecords)
                {

                    objStockTranBL.StockTransfer_ID = Convert.ToInt32(ht["StockTransfer_ID"].ToString());
                    objStockTranBL.Item_Code = ht["Item_Code"].ToString();
                    objStockTranBL.FromProjectCode = ht["From_ProjectCode"].ToString();

                    if (objStockTranBL.insertTransferredItemsToStock(con, StockTransferBL.eLoadSp.INSERT_TRANSFERRED_ITEMS_TO_STOCK))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Selected records Accepted sucessfully!!!');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to Accept the Selected records!!!');", true);
                    }
                }
                Gv_StockTranferItems.SelectedRecords.Clear();
                BindStockTransferRequestList();
                BindStockTransferItems();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select at least one record to Accept!!!');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBUpdateError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
        finally
        {
            Gv_StockTranferItems.SelectedRecords = null;
        }
    }
    /// <summary>
    /// Updating the status of the Stock Tranfer table to rejected based on the selected records in the Gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_RejectTransferItems_Click(object sender, EventArgs e)
    {
        try
        {
            objStockTranBL = new StockTransferBL();
            if (Gv_StockTranferItems.SelectedRecords != null)
            {
                foreach (Hashtable ht in Gv_StockTranferItems.SelectedRecords)
                {

                    objStockTranBL.StockTransfer_ID = Convert.ToInt32(ht["StockTransfer_ID"].ToString());
                    objStockTranBL.Item_Code = ht["Item_Code"].ToString();

                    if (objStockTranBL.updateStockTransfer(con, StockTransferBL.eLoadSp.UPDATE_STOCK_TRANSFER_STATUS_BY_ID))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Selected records rejected sucessfully!!!');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to reject the Selected records!!!');", true);
                    }
                }
                Gv_StockTranferItems.SelectedRecords.Clear();
                BindStockTransferRequestList();
                BindStockTransferItems();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select at least one record to reject!!!');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBUpdateError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
        finally
        {
            Gv_StockTranferItems.SelectedRecords = null;
        }
    }

    protected void Btn_Cancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../CommonPages/Home.aspx", false);
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
}

