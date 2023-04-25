using BusinessLayer;
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


public partial class AssetRecurringVarianceReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    MaterialBL objMaterial = null;
    DailyRunningHourBL objDailyRunningHour = null;
    AssetRegistrationBL objAsset = null;
    AssetTransferBL objassetTransferBL = null;
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindBudgetSectors();
                //BindAssetRegistrationList();
                BindBudgetsSomeSectors();
                BindAssetCategory();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void ddlBudgetSector_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindCategoryDetails();
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
            Category objCategory = new Category();
            DataSet ds = new DataSet();
            DataTable DatafilterDt;
            bool exists;
            int BudgetSectorID;
            if (objCategory.load(con, Category.eLoadSp.SELECT_ALL, ref ds))
            {
              
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

                            ddlCategoryName.DataSource = SectorIDdt;
                            ddlCategoryName.DataValueField = "Mat_cat_ID";
                            ddlCategoryName.DataTextField = "Category_Name";
                            ddlCategoryName.DataBind();

                        }
                        else
                        {
                            ddlCategoryName.Items.Clear();
                            ddlCategoryName.DataSource = null;
                            ddlCategoryName.DataBind();
                        }
                        exists = false;

                    }
                    ddlCategoryName.Items.Insert(0, "-Select-");
                }             

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void BindAssetRegistrationList()
    {
        try
        {
            DataSet ds = new DataSet();
            AssetRegistrationBL objAssetBL = new AssetRegistrationBL();
            objAssetBL.Project_Code = Session["Project_Code"].ToString();

            objAssetBL.load(con, AssetRegistrationBL.eLoadSp.SELECT_ASSET_ALL, ref ds);
            ddlAsset.DataSource = ds;
            ddlAsset.DataValueField = "Code";
            ddlAsset.DataTextField = "AssetNameWithCode";
            ddlAsset.DataBind();
            ddlAsset.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void BindAssetCategory()
    {
        try
        {
           
                objAsset = new AssetRegistrationBL();
                DataSet ds = new DataSet();

                objAsset.load(con, AssetRegistrationBL.eLoadSp.SELECT_ASSET_CATEGORY_ALL, ref ds);
                ddlAssetCate_r.DataSource = ds;
                ddlAssetCate_r.DataValueField = "Asset_cat_ID";
                ddlAssetCate_r.DataTextField = "Category_Name";
                ddlAssetCate_r.DataBind();
                ddlAssetCate_r.Items.Insert(0, "-Select-");


            
           

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindAssetNames()
    {
        try
        {
            if (ddlAssetCate_r.SelectedIndex > 0)
            {
                objassetTransferBL = new AssetTransferBL();
                DataSet ds = new DataSet();
                objassetTransferBL.Asset_cat_ID = Convert.ToInt32(ddlAssetCate_r.SelectedValue);
                if (Session["Project_Code"] != null)
                {
                    objassetTransferBL.Project_Code = Session["Project_Code"].ToString();
                }

                objassetTransferBL.load(con, AssetTransferBL.eLoadSp.SELECT_ASSET_NAME_BY_ASSET_CATEGORY, ref ds);
                ddlAsset.DataSource = ds;
                ddlAsset.DataTextField = "AssetNameWithCode";
                ddlAsset.DataValueField = "Code";
                ddlAsset.DataBind();
                ddlAsset.Items.Insert(0, "-Select-");



            }
            else
            {
                ddlAsset.Items.Clear();
                ddlAsset.Items.Insert(0, "-Select-");
                ddlAsset.SelectedIndex = 0;

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
            ddlItem_r.Items.Clear();
            if (ddlCategoryName.SelectedIndex != 0)
            {
                IndentBL objIndent = new IndentBL();
                DataSet ds = new DataSet();
                objIndent.Mat_Cat_Id = Convert.ToUInt16(ddlCategoryName.SelectedValue);
                objIndent.load(con, IndentBL.eLoadSp.SELECT_ITEMCODE_BY_CATEGORY_ID, ref ds);
                ddlItem_r.DataSource = ds;
                ddlItem_r.DataValueField = "Item_Code";
                ddlItem_r.DataTextField = "Item_Name";
                ddlItem_r.DataBind();
                ddlItem_r.Items.Insert(0, "-Select-");
            }
            else
            {               
                ddlItem_r.Items.Insert(0, "-Select-");
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    private void BindBudgetSectors()
    {
        objMaterial = new MaterialBL();
        DataSet  ds = new DataSet();
        objMaterial.load(con, MaterialBL.eLoadSp.SELECT_BUDGET_SECTOR_ALL, ref ds);
        if (ds.Tables.Count > 0)
        {

            ddlBudgetSector.DataSource = ds.Tables[0];
            ddlBudgetSector.DataTextField = "Sector_Name";
            ddlBudgetSector.DataValueField = "Budget_Sector_ID";
            ddlBudgetSector.DataBind();
            ddlBudgetSector.Items.Insert(0, "-Select-");

        }
    }

    private void BindBudgetsSomeSectors()
    {
        objMaterial = new MaterialBL();
        DataSet ds = new DataSet();
        objMaterial.load(con, MaterialBL.eLoadSp.SELECT_BUDGET_SECTOR, ref ds);
        if (ds.Tables.Count > 0)
        {

            ddlBudgetSector.DataSource = ds.Tables[0];
            ddlBudgetSector.DataTextField = "Sector_Name";
            ddlBudgetSector.DataValueField = "Budget_Sector_ID";
            ddlBudgetSector.DataBind();
            ddlBudgetSector.Items.Insert(0, "-Select-");

        }
    }


    protected void BindItemsList()
    {
        try
        {
            objDailyRunningHour = new DailyRunningHourBL();
            ds = new DataSet();

            if ((txtStartDate.Text != string.Empty && txtEndDate.Text == string.Empty) || (txtStartDate.Text == string.Empty && txtEndDate.Text != string.Empty)) 
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Enter both Start and End Date.');", true);
                return;
            }
            else if (txtStartDate.Text != string.Empty && txtEndDate.Text != string.Empty)
            {               
                objDailyRunningHour.StDate = Convert.ToDateTime(txtStartDate.Text);
                objDailyRunningHour.EndDate = Convert.ToDateTime(txtEndDate.Text);                
            }

            if(ddlAsset.SelectedIndex != 0)
            {
                objDailyRunningHour.Asset_Code = ddlAsset.SelectedValue;
            }
            else
            {
                objDailyRunningHour.Asset_Code = null;
            }
            objDailyRunningHour.Item_Code = ddlItem_r.SelectedValue;

            objDailyRunningHour.load(con, DailyRunningHourBL.eLoadSp.Asset_Recuring_Variance_Report, ref ds);
            VarRep_Grid.DataSource = ds;
            VarRep_Grid.DataBind();

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);

        }
    }

    protected void Btn_VarRep_Click(object sender, EventArgs e)
    {
        BindItemsList();
        VarRep_Gv.Visible = true;
    }

    protected void ddlAssetCate_r_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAssetNames();
    }
}
