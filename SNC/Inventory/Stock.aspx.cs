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



public partial class Stock : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    StockBL objstockBL = null;
    Category objCategory = null;
    IndentBL objIntentBL = null;
    VendorBL objVendorBL = null;
    MaterialBL objMaterial = null;
    ProjectBL objProjectBL = null;

    UOM objUOM = null;
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UID"] != null)
            {
                BindVendor();
                BindProjectsList();


                BindBudgetSectors();
                //BindCategoryDetails();
                BindUOMDetails();
                if (Request.QueryString["ID"] != null)
                {

                    int id = Int32.Parse(Request.QueryString["ID"]);
                    GetStockDetails(id);
                    ddlBudgetSector_SelectedIndexChanged(sender, e);
                    ddlCategory_SelectedIndexChanged(sender, e);
                    ddlItemName_SelectedIndexChanged(sender, e);
                }
            }
            else
            {
                Response.Redirect("../CommonPages/Login.aspx", false);
            }
        }
       
    }



    protected void BindProjectsList()
    {
        try
        {
            objProjectBL = new ProjectBL();
            ds = new DataSet();
            if (Session["Project_Code"] != null)
            {
                objProjectBL.Project_Code = Session["Project_Code"].ToString();

                objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_BY_Project_Code, ref ds);
                ddlProjectName.DataTextField = "Project_Name";
                ddlProjectName.DataValueField = "Project_Code";
                ddlProjectName.DataSource = ds;
                ddlProjectName.DataBind();
                ddlProjectName.Enabled = false;
                ddlProjectName.Items.Insert(0, "-Select-");
                ddlProjectName.SelectedValue = Session["Project_Code"].ToString();
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }
    private void BindCategoryDetails()
    {

        try
        {
            objCategory = new Category();
            ds = new DataSet();
            if (objCategory.load(con, Category.eLoadSp.SELECT_ALL, ref ds))
            {

                if (ds.Tables[0].Rows.Count >= 0)
                {

                    ddlCategory.DataSource = ds;
                    ddlCategory.DataTextField = "Category_Name";
                    ddlCategory.DataValueField = "Mat_cat_ID";
                    ddlCategory.DataBind();
                    ddlCategory.Items.Insert(0, "-Select-");
                   // ViewState["catname"] = ddlCategory.SelectedItem;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }




    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            objIntentBL = new IndentBL();
            ds = new DataSet();
            if(ddlCategory.SelectedIndex != 0)
            {
                objIntentBL.Mat_Cat_Id = Convert.ToUInt16(ddlCategory.SelectedValue.Trim());
                objIntentBL.load(con, IndentBL.eLoadSp.SELECT_ITEMCODE_BY_CATEGORY_ID, ref ds);
                ddlItemName.DataSource = ds;
                ddlItemName.DataValueField = "Item_Code";
                ddlItemName.DataTextField = "Item_Name";

                ddlItemName.DataBind();
                ddlItemName.Items.Insert(0, "-Select-");
                if (Request.QueryString["ID"] == null)
                {
                    txtItemCode.Text = string.Empty;
                    ddlUOM.SelectedValue = ddlUOM.Items.FindByText("-Select-").Value;
                }              
            }
            else
            {
                txtItemCode.Text = string.Empty;
                ddlUOM.SelectedValue = ddlUOM.Items.FindByText("-Select-").Value;
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindVendor()
    {

        try
        {
            ds = new DataSet();
            objVendorBL = new VendorBL();
            objVendorBL.load(con, VendorBL.eLoadSp.SELECT_VENDOR_ALL, ref ds);
            ddlVendorName.DataSource = ds;
            ddlVendorName.DataTextField = "Vendor_name";
            ddlVendorName.DataValueField = "Vendor_ID";
            ddlVendorName.DataBind();
            ddlVendorName.Items.Insert(0, "-Select-");


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ddlVendorName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlVendorName.SelectedIndex != 0)
            {
                txtVendorCode.Text = ddlVendorName.SelectedValue;
                txtVendorCode.Enabled = false;
            }
            else
            {
                txtVendorCode.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    private void BindUOMDetails()
    {
        try
        {
            objUOM = new UOM();
            ds = new DataSet();
            if (objUOM.load(con, UOM.eLoadSp.SELECT_ALL, ref ds))
            {

                if (ds.Tables[0].Rows.Count >= 0)
                {
                    ddlUOM.DataSource = ds;
                    ddlUOM.DataTextField = "UOM";
                    ddlUOM.DataValueField = "UOM_ID";
                    ddlUOM.DataBind();
                    ddlUOM.Items.Insert(0, "-Select-");
                }

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objMaterial = new MaterialBL();
            ds = new DataSet();
            objMaterial.Item_Code = ddlItemName.SelectedValue;
            objMaterial.load(con, MaterialBL.eLoadSp.SELECTBYITEMCODE, ref ds);
            // txtItemCode.Text = ddlItemName.SelectedValue;

            if (ddlItemName.SelectedIndex > 0)
            {
                txtItemCode.Text = ddlItemName.SelectedValue;
                txtItemCode.Enabled = false;
                ddlUOM.SelectedValue = ds.Tables[0].Rows[0]["UOM"].ToString();
            }
            else
            {
                txtItemCode.Text = string.Empty;
                ddlUOM.SelectedValue = ddlUOM.Items.FindByText("-Select-").Value;
            }
           

        }

        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        objstockBL = new StockBL();
        try
        {
            //if(ddlProjectName.SelectedIndex != 0)
            //{
                //objstockBL.ProjectCode = ddlProjectName.SelectedValue;
            if(Session["Project_Code"]!=null)
            {
                objstockBL.ProjectCode = ddlProjectName.SelectedValue.Trim();
            }
               
            //}
            if (ddlCategory.SelectedIndex != 0)
            {
                objstockBL.Mat_cat_ID = Convert.ToInt32(ddlCategory.SelectedValue);

            }
            else
            {
                // objstockBL.Mat_cat_ID = "";
            }

            if (txtDate.Text.Trim() != string.Empty)
            {
                objstockBL.Stock_Date = Convert.ToDateTime(txtDate.Text.Trim());
            }
            else
            {
               // objstockBL.Stock_Date="";
            }
            if (ddlItemName.SelectedIndex != 0)
            {
                objstockBL.Item_Code = ddlItemName.SelectedValue.ToString();
       
                txtItemCode.Text = objstockBL.Item_Code;

            }
            else
            {
                objstockBL.Item_Code = null;
            }

            if (ddlVendorName.SelectedIndex != 0)
            {
                objstockBL.Vendor_Id = ddlVendorName.SelectedValue.ToString();
                txtVendorCode.Text = objstockBL.Vendor_Id;

            }
            else
            {
                objstockBL.Vendor_Id = null;
            }

            objstockBL.Bill_No = txtBillNo.Text;
            objstockBL.UOM = Convert.ToInt32(ddlUOM.SelectedValue);
            
            objstockBL.Adjust_QTY = txtQty.Text != string.Empty ? Convert.ToDecimal(txtQty.Text) : Convert.ToDecimal(0.00);
            objstockBL.Avl_Qty = txtQty.Text != string.Empty ? Convert.ToDecimal(txtQty.Text) : Convert.ToDecimal(0.00);
            objstockBL.Remarks = txtRemarks.Text;
            objstockBL.Rate = txtRate.Text != string.Empty ? Convert.ToDecimal(txtRate.Text) : Convert.ToDecimal(0.00);


            if (btnSubmit.Text == "Submit")
            {
                objstockBL.Adjust_Type = "Additional";
                if (objstockBL.insert(con, StockBL.eLoadSp.INSERT_STOCK))
                {
                    ResetUsersItems();
                    ddlProjectName.Enabled = false;
                    ViewState["ViewSta_Stock_ID"] = objstockBL.Stock_ID.ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Stock Details has been Inserted');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render(' Stock Already Exist!!!');", true);
                }
                
            }
            else
            {
               objstockBL.Stock_ID = int.Parse(ViewState["ViewSta_Stock_ID"].ToString());
               if (objstockBL.update(con, StockBL.eLoadSp.UPDATE_STOCK_BYID))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render(' Stock details has been updated sucessfully.');", true);
                }
            }
          
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }


    }
    protected void GetStockDetails(int stockID)
    {
        try
        {
            objstockBL = new StockBL();
            ds = new DataSet();
            objstockBL.Stock_ID = stockID;
            objstockBL.load(con, StockBL.eLoadSp.SELECT_STOCKBY_ID, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlBudgetSector.SelectedValue = ds.Tables[0].Rows[0]["Budget_Sector_ID"].ToString();
                ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["Mat_cat_ID"].ToString();
                ddlItemName.SelectedValue = ds.Tables[0].Rows[0]["Item_Code"].ToString();
                txtDate.Text = ds.Tables[0].Rows[0]["Stock_Date"].ToString();
                txtItemCode.Text = ds.Tables[0].Rows[0]["Item_Code"].ToString();
                txtVendorCode.Text = ds.Tables[0].Rows[0]["Vendor_ID"].ToString();
                ddlVendorName.SelectedValue = ds.Tables[0].Rows[0]["Vendor_ID"].ToString();
                txtBillNo.Text = ds.Tables[0].Rows[0]["Bill_No"].ToString();
                txtRate.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
                ddlUOM.SelectedValue = ds.Tables[0].Rows[0]["UOM"].ToString();

                txtQty.Text = ds.Tables[0].Rows[0]["Updated_QTY"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                ddlProjectName.SelectedValue = ds.Tables[0].Rows[0]["Project_Code"].ToString();
                ddlProjectName.Enabled = false;
                btnSubmit.Text = "Update";
                ViewState["ViewSta_Stock_ID"] = ds.Tables[0].Rows[0]["Stock_ID"].ToString();
 
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    private void ResetUsersItems()
    {

        txtDate.Text = string.Empty;
        txtItemCode.Text = string.Empty;
        ddlItemName.SelectedIndex = 0;
        ddlVendorName.SelectedIndex = 0;

        ddlCategory.SelectedValue = ddlCategory.Items.FindByText("-Select-").Value;
        txtVendorCode.Text = string.Empty;
        ddlUOM.SelectedValue = ddlUOM.Items.FindByText("-Select-").Value;
        txtBillNo.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtQty.Text = string.Empty;
        txtRemarks.Text = string.Empty;
      

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["ID"] != null)
            {
                Response.Redirect("../Inventory/Stock List.aspx", false);
            }
            else
            {
                Response.Redirect("../Inventory/Stock.aspx", false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    private void BindBudgetSectors()
    {
        objMaterial = new MaterialBL();

        DataSet dsnew = new DataSet();
        objMaterial.load(con, MaterialBL.eLoadSp.SELECT_BUDGET_SECTOR_ALL, ref dsnew);
        if (dsnew.Tables.Count > 0)
        {
            ddlBudgetSector.DataSource = dsnew;
            ddlBudgetSector.DataTextField = "Sector_Name";
            ddlBudgetSector.DataValueField = "Budget_Sector_ID";
            ddlBudgetSector.DataBind();
            ddlBudgetSector.Items.Insert(0, "-Select-");
        }

    }

    protected void ddlBudgetSector_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindCategoryDetailsWithSector();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void BindCategoryDetailsWithSector()
    {
        DataTable dt = new DataTable();

        try
        {
            objCategory = new Category();
            ds = new DataSet();
            DataTable DatafilterDt;
            bool exists;
            int BudgetSectorID;
            ddlCategory.Items.Clear();
            if (objCategory.load(con, Category.eLoadSp.SELECT_ALL, ref ds))
            {
                dt = ds.Tables[0];
                if (ddlBudgetSector.SelectedIndex != 0)
                {
                    BudgetSectorID = Convert.ToInt16(ddlBudgetSector.SelectedValue.ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DatafilterDt = ds.Tables[0];


                        exists = DatafilterDt.AsEnumerable().Where(c => c.Field<int>("Budget_Sector_ID").Equals(BudgetSectorID)).Count() > 0;
                        if (exists)
                        {
                            DataTable SectorIDdt = DatafilterDt.AsEnumerable()
                                         .Where(r => r.Field<int>("Budget_Sector_ID") == BudgetSectorID)
                                         .CopyToDataTable();

                            ddlCategory.DataSource = SectorIDdt;
                            ddlCategory.DataValueField = "Mat_cat_ID";
                            ddlCategory.DataTextField = "Category_Name";
                            ddlCategory.DataBind();

                        }
                        else
                        {
                            ddlCategory.Items.Clear();
                            ddlCategory.DataSource = null;
                            ddlCategory.DataBind();
                        }
                        exists = false;

                    }
                   
                    ddlCategory.Items.Insert(0, "-Select-");
                    
                }
                else
                {
                    ddlCategory.Items.Insert(0, "-Select-");
                    ddlCategory.SelectedIndex = 0;

                }
               
                
               
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
}