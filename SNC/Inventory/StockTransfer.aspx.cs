using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using BusinessLayer;
using SNC.ErrorLogger;
using Bussinesslogic;
using System.Net;
using System.Net.Mail;
using System.Collections;



public partial class StockTransfer : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    ProjectBL objprojectbl = null;
    IndentBL objIndent = null;
    StockTransferBL objStockTranBL = null;
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UID"] != null)
            {
                BindProject();
                BindDepartment();
                BindDepartment();

                ddlFromProject.SelectedValue = Session["Project_Code"].ToString();
                ddlFromProject.Enabled = false;
                BindToProject();
            }
            else
            {
                Response.Redirect("../CommonPages/Login.aspx", false);
            }
        }
    }
    /// <summary>
    /// Binding All Projects From Tb_Project
    /// </summary>
    protected void BindProject()
    {

        try
        {
            ds = new DataSet();
            DataTable filterDt = new DataTable();
            objprojectbl = new ProjectBL();

           // objprojectbl.Project_Code  = Session["Project_Code"].ToString();
            objprojectbl.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_ALL, ref ds);
            ddlFromProject.DataSource = ds;
            ddlFromProject.DataTextField = "Project_Name";
            ddlFromProject.DataValueField = "Project_Code";
            ddlFromProject.DataBind();
            ddlFromProject.Items.Insert(0, "-Select-");

          

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }


    protected void BindToProject()
    {
        try
        {
            DataSet dsProject = new DataSet();
            AssetTransferBL objassetTransferBL = new AssetTransferBL();
            if (Session["Project_Code"] != null)
            {
                objassetTransferBL.Assign_To_project = Session["Project_Code"].ToString();
                objassetTransferBL.load(con, AssetTransferBL.eLoadSp.SELECT_ASSIGN_TO_PROJECT_NAME_FOR_ASSET, ref dsProject);
                ddlToProject.DataSource = dsProject;
                ddlToProject.DataTextField = "Project_Name";
                ddlToProject.DataValueField = "Project_Code";
                ddlToProject.DataBind();
                ddlToProject.Items.Insert(0, "-Select-");
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }


    /// <summary>
    /// This Method Will Bind Department Data
    /// To Department DropDownList
    /// </summary>
    protected void BindDepartment()
    {

        try
        {
            ds = new DataSet();
            objprojectbl = new ProjectBL();
            objprojectbl.load(con, ProjectBL.eLoadSp.SELECT_DEPARTMENT_ALL, ref ds);
            ddlDepartment.DataSource = ds;
            ddlDepartment.DataTextField = "Department";
            ddlDepartment.DataValueField = "Department";
            ddlDepartment.DataBind();

            ddlDepartment.Items.Insert(0, "-Select-");

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    /// <summary>
    /// Binding the users based on the selection of the Department
    /// </summary>
    protected void BindUsers()
    {
        try
        {
            if(ddlDepartment.SelectedIndex != 0)
            {
                string department = ddlDepartment.SelectedValue;
                objIndent = new IndentBL();
                DataSet dsUsers = new DataSet();
                bool exists;
                DataTable DatafilterDt = new DataTable();
                objIndent.load(con, IndentBL.eLoadSp.SELECT_USERNAMES_ALL, ref dsUsers);
                ddlReceiver.DataSource = dsUsers;

                if (dsUsers.Tables[0].Rows.Count > 0)
                {
                    DatafilterDt = dsUsers.Tables[0];


                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Department").Equals(department)).Count() > 0;
                    if (exists)
                    {
                        DataTable Usersdt = DatafilterDt.AsEnumerable()
                                     .Where(r => r.Field<string>("Department") == department)
                                     .CopyToDataTable();

                        ddlReceiver.DataSource = Usersdt;
                        ddlReceiver.DataTextField = "Name";
                        ddlReceiver.DataValueField = "UID";
                        ddlReceiver.DataBind();
                        ddlReceiver.Items.Insert(0, "-Select-");
                    }
                    else
                    {
                        ddlReceiver.Items.Clear();
                        ddlReceiver.Items.Insert(0, "-Select-");
                    }
                    exists = false;
                }

            }          
           
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindUsers();
    }
    /// <summary>
    /// Binding all the available qty of each sectors based on the selection of the From Project dropdown
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        ds = new DataSet();
        objStockTranBL = new StockTransferBL();
        objStockTranBL.FromProjectCode = ddlFromProject.SelectedValue;
        objStockTranBL.load(con, StockTransferBL.eLoadSp.SELECT_AVL_QTY_SECTOR_WISE_FROM_STOCK_BY_PROJECT, ref ds);
        if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            Gv_BudgetSector.DataSource = ds;
            Gv_BudgetSector.DataBind();
        }
    }
    /// <summary>
    /// Binding All the items based on the particular Budget Sector 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkbtnSectorName_Click(object sender, EventArgs e)
    {
        try
        {
            objStockTranBL = new StockTransferBL();
            ds = new DataSet();
            DataTable dt = new DataTable();
            objStockTranBL.Budget_Sector = ((LinkButton)sender).Text.ToString();
            objStockTranBL.FromProjectCode = ddlFromProject.SelectedValue;
            objStockTranBL.load(con, StockTransferBL.eLoadSp.SELECT_SECTORWISE_ITEMS_FOR_STOCK_TRANSFER, ref ds);
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                ViewState["EditStockTransferdt"] = ds.Tables[0];
                Gv_SectorWiseItems.DataSource = ds.Tables[0];
                Gv_SectorWiseItems.DataBind();
            }
            else
            {
                Gv_SectorWiseItems.DataSource = ds.Tables[0];
                Gv_SectorWiseItems.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }        
    }
    /// <summary>
    /// Updating the Transfer Qty of each item and re- binding to the Gv_SectorWiseItems with updated tranfser qty
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Gv_SectorWiseItems_UpdateCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objStockTranBL = new StockTransferBL();
          

            DataTable table;
            DataTable dtStock = new DataTable();
         
            table = (DataTable)ViewState["EditStockTransferdt"];
          

            decimal number;
            if (Decimal.TryParse(e.Record["TransferQty"].ToString(), out number))
            {
                if (Convert.ToDecimal(e.Record["TransferQty"]) <= Convert.ToDecimal(e.Record["Updated_QTY"]))
                {
                    string itemcode = e.Record["Item_Code"].ToString();


                    //var dValue = from row in table.AsEnumerable()
                    //             where row.Field<string>("Item_Code") == itemcode

                    //             select row.Field<decimal>("TransferQty") == Convert.ToDecimal(e.Record["TransferQty"]);


                    foreach (DataRow dr in table.Rows) // search whole table
                    {
                        if (dr["Item_Code"].ToString() == itemcode)
                        {
                            dr["TransferQty"] = e.Record["TransferQty"].ToString();
                            break;
                        }
                    }


                    //DataRow dr = table.Select("Item_Co-de=" + itemcode).FirstOrDefault(); 
                    //if (dr != null)
                    //{
                    //    dr["TransferQty"] =  Convert.ToDecimal(e.Record["TransferQty"]); 
                    //}

                    Gv_SectorWiseItems.DataSource = table;
                    Gv_SectorWiseItems.DataBind();

                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Transfer Quantity updated successfully!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Transfer Quantity should not be greater than Available Quantity!');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please enter only decimal values in Transfer Quantity!');", true);
                return;
            }
           
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }     
    }
    /// <summary>
    /// Inserting into stock transfer table based on the Selected records from the Gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_StockTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            objStockTranBL = new StockTransferBL();
            if(ddlFromProject.SelectedIndex != 0 && ddlToProject.SelectedIndex != 0 && ddlReceiver.SelectedIndex != 0)
            {
                if(Gv_SectorWiseItems.SelectedRecords != null)
                {
                    foreach (Hashtable ht in Gv_SectorWiseItems.SelectedRecords)
                    {
                        objStockTranBL.FromProjectCode = ddlFromProject.SelectedValue;
                        objStockTranBL.ToProjectCode = ddlToProject.SelectedValue;
                        objStockTranBL.Stock_Receiver = Convert.ToInt32(ddlReceiver.SelectedValue);

                        objStockTranBL.Item_Code = ht["Item_Code"].ToString();
                        objStockTranBL.AvailableQty = Convert.ToDecimal(ht["Updated_QTY"].ToString());
                        objStockTranBL.TransferQty = Convert.ToDecimal(ht["TransferQty"].ToString());
                        if (objStockTranBL.insertToStockTransfer(con, StockTransferBL.eLoadSp.INSERT_TO_STOCK_TRANSFER))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Stock transferred sucessfully!!!');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to transfer the stock!!!');", true);
                        }
                    }
                    Gv_SectorWiseItems.SelectedRecords.Clear();
                    Btn_Search_Click(null, null);
                    Gv_SectorWiseItems.DataSource = null;
                    Gv_SectorWiseItems.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select at least one record to transfer!!!');", true);
                }
               
            }
            else
            {
                 ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select From project,To Project & Receiver!!!');", true);
                 return;
            }
            
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
        finally
        {
            Gv_SectorWiseItems.SelectedRecords = null;
        }
    }
}