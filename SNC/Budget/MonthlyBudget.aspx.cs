using BusinessLayer;
using SNC.ErrorLogger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class MonthlyBudget : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    ProjectBL objProjectBL = null;
    AssetRegistrationBL objAsset = null;
    AssetTransferBL objassetTransferBL = null;
    IndentBL objIndent = null;
    DataSet ds = null;
    MaterialBL objMaterial = null;
    BudgetBL objBudgetBL = null;
    Category objCategory = null;
    UOM objUOM = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["UID"] != null)
            {
                Session["count"] = 1;

                if (!IsPostBack)
                {
                    CalServiceDate.StartDate = DateTime.Now;

                    BindBudgetSectors();
                    BindProjectList();
                    if (Session["Project_Code"] != null)
                    {
                        ddlProject.SelectedValue = Session["Project_Code"].ToString();
                        ddlExistingProjectName.SelectedValue = Session["Project_Code"].ToString();
                    }
                    ddlProject.Enabled = false;
                    ddlExistingProjectName.Enabled = false;
                    ddlExistingProjectName_SelectedIndexChanged(null, null);
                    ddlProject_SelectedIndexChanged(null, null);

                    BindUserList();
                    BindMonth();

                    BindAssetType();
                    BindUOMDetails();


                    if (Request.QueryString["ID"] != null && Request.QueryString["BudgetID"] == null)
                    {
                        GetBudgetDetails(Request.QueryString["ID"].ToString());
                        LoadBudgetDetailsByAbs_BID(Request.QueryString["ID"].ToString(), true);
                    }
                    else
                    {

                        if (Convert.ToInt32(DateTime.Now.Month.ToString()) == 12)
                        {
                            txtYear.Text = Convert.ToString(Convert.ToInt32(DateTime.Now.Year.ToString()) + 1);
                        }
                        else
                        {
                            txtYear.Text = DateTime.Now.Year.ToString();
                        }

                        txtCreatedDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                        DateTime createDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                        txtBudgetClosedDate.Text = createDate.ToString("dd-MM-yyyy");
                    }

                    if (Request.QueryString["BudgetID"] != null && Request.QueryString["Abs_BID"] == null)
                    {
                        GetBudgetDetails(Request.QueryString["ID"].ToString());
                        btnSendForApproval.Visible = false;
                        BtnPrint.Visible = false;
                        ItemListContainer.Visible = true;
                        LoadBudgetDetailsByAbs_BID(Request.QueryString["ID"].ToString(), true);
                        VisableRemarks();
                    }

                    if (Request.QueryString["Abs_BID"] != null && Request.QueryString["MFYRQ"] != null && Request.QueryString["MFYRQ"].ToString() == "0")
                    {
                        GetBudgetDetails(Request.QueryString["Abs_BID"].ToString());

                        btnSendForApproval.Visible = false;
                        BtnPrint.Visible = true;
                        btnSaveBudget.Visible = false;
                        btnCancelItem.Visible = false;
                        ItemListContainer.Visible = true;
                        LoadBudgetDetailsByAbs_BID(Request.QueryString["Abs_BID"].ToString(), false);
                        VisableRemarks();
                    }
                    else if (Request.QueryString["Abs_BID"] != null && Request.QueryString["MFYRQ"] != null && Request.QueryString["MFYRQ"].ToString() == "1")
                    {
                        GetBudgetDetails(Request.QueryString["Abs_BID"].ToString());
                        btnSendForApproval.Visible = false;
                        BtnPrint.Visible = false;
                        ItemListContainer.Visible = true;
                        LoadBudgetDetailsByAbs_BID(Request.QueryString["Abs_BID"].ToString(), false);
                    }

                }
            }
            else
            {
                Response.Redirect("../CommonPages/Login.aspx", false);
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
        ds = new DataSet();

        objMaterial.load(con, MaterialBL.eLoadSp.SELECT_BUDGET_SECTOR_ALL, ref ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBudgetSector.DataSource = ds;
            ddlBudgetSector.DataTextField = "Sector_Name";
            ddlBudgetSector.DataValueField = "Budget_Sector_ID";
            ddlBudgetSector.DataBind();
            ddlBudgetSector.Items.Insert(0, "-Select-");
        }
        else
        {
            ddlBudgetSector.Items.Insert(0, "-Select-");
        }
    }
    protected void VisableRemarks()
    {
        try
        {
            DataSet ds = new DataSet();
            GrandAbstractBL ObjGrandAbstractBL = new GrandAbstractBL();
            if (Request.QueryString["BudgetID"]!= null)
            {
                ObjGrandAbstractBL.Budget_ID = Request.QueryString["BudgetID"].ToString();
            }
            else if (ViewState["BudgetID"] != null)
            {
                ObjGrandAbstractBL.Budget_ID = ViewState["BudgetID"].ToString();
            }
            ObjGrandAbstractBL.load(con, GrandAbstractBL.eLoadSp.SELECT_REMARKS, ref ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Auto_Remarks"].ToString() != string.Empty)
                {
                    rowAutoRemarks.Visible = true;
                    lblAutoRemark.Text = ds.Tables[0].Rows[0]["Auto_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PlMach_Remarks"].ToString() != string.Empty)
                {
                    rowPlant.Visible = true;
                    lblPlantRemarks.Text = ds.Tables[0].Rows[0]["PlMach_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Shutter_Mat_Remarks"].ToString() != string.Empty)
                {
                    trshutter.Visible = true;
                    shutterRemarks.Text = ds.Tables[0].Rows[0]["Shutter_Mat_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Consumable_Remarks"].ToString() != string.Empty)
                {
                    trConsumable.Visible = true;
                    reConsumable.Text = ds.Tables[0].Rows[0]["Consumable_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Elec_Remarks"].ToString() != string.Empty)
                {
                    trElectrical.Visible = true;
                    reElectrical.Text = ds.Tables[0].Rows[0]["Elec_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HSD_Pet_Remarks"].ToString() != string.Empty)
                {
                    trHSD.Visible = true;
                    reHSD.Text = ds.Tables[0].Rows[0]["HSD_Pet_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Oil_Lube_Remarks"].ToString() != string.Empty)
                {
                    trPetrol.Visible = true;
                    rePetrol.Text = ds.Tables[0].Rows[0]["Oil_Lube_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Weld_Elec_Remarks"].ToString() != string.Empty)
                {
                    trWelding.Visible = true;
                    reWelding.Text = ds.Tables[0].Rows[0]["Weld_Elec_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Hardware_Remarks"].ToString() != string.Empty)
                {
                    trHard.Visible = true;
                    reHard.Text = ds.Tables[0].Rows[0]["Hardware_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Oxygen_ace_Remarks"].ToString() != string.Empty)
                {
                    trOxygen.Visible = true;
                    reOxygen.Text = ds.Tables[0].Rows[0]["Oxygen_ace_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Safety_Item_Remarks"].ToString() != string.Empty)
                {
                    trSafety.Visible = true;
                    reSafety.Text = ds.Tables[0].Rows[0]["Safety_Item_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Staff_wel_Remarks"].ToString() != string.Empty)
                {
                    trStaff.Visible = true;
                    reStaff.Text = ds.Tables[0].Rows[0]["Staff_wel_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Mess_Expense_Remarks"].ToString() != string.Empty)
                {
                    trMess.Visible = true;
                    reMess.Text = ds.Tables[0].Rows[0]["Mess_Expense_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Print_Sta_Remarks"].ToString() != string.Empty)
                {
                    trPrinting.Visible = true;
                    rePrinting.Text = ds.Tables[0].Rows[0]["Print_Sta_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Repair_Maint_Remarks"].ToString() != string.Empty)
                {
                    trRepairs.Visible = true;
                    reRepairs.Text = ds.Tables[0].Rows[0]["Repair_Maint_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["BOQ_Remarks"].ToString() != string.Empty)
                {
                    trBOQ.Visible = true;
                    reBOQ.Text = ds.Tables[0].Rows[0]["BOQ_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Sanitary_Remarks"].ToString() != string.Empty)
                {
                    trSanitary.Visible = true;
                    reSanitary.Text = ds.Tables[0].Rows[0]["Sanitary_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Blast_ma_Remarks"].ToString() != string.Empty)
                {
                    trBlasting.Visible = true;
                    reBlasting.Text = ds.Tables[0].Rows[0]["Blast_ma_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FnF_Remarks"].ToString() != string.Empty)
                {
                    trFurnitures.Visible = true;
                    reFurnitures.Text = ds.Tables[0].Rows[0]["FnF_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fix_Asset_Remarks"].ToString() != string.Empty)
                {
                    trFixed.Visible = true;
                    reFixed.Text = ds.Tables[0].Rows[0]["Fix_Asset_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Infra_Remarks"].ToString() != string.Empty)
                {
                    trInfrastructure.Visible = true;
                    reInfrastructure.Text = ds.Tables[0].Rows[0]["Infra_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Sand_Remarks"].ToString() != string.Empty)
                {
                    trSand.Visible = true;
                    reSand.Text = ds.Tables[0].Rows[0]["Sand_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Jelly_MetalRemarks"].ToString() != string.Empty)
                {
                    trJelly.Visible = true;
                    reJelly.Text = ds.Tables[0].Rows[0]["Jelly_MetalRemarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Red_Soil_Remarks"].ToString() != string.Empty)
                {
                    trRed.Visible = true;
                    reRed.Text = ds.Tables[0].Rows[0]["Red_Soil_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Cement_Remarks"].ToString() != string.Empty)
                {
                    trCement.Visible = true;
                    reCement.Text = ds.Tables[0].Rows[0]["Cement_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Chem_Remarks"].ToString() != string.Empty)
                {
                    trChemicals.Visible = true;
                    reChemicals.Text = ds.Tables[0].Rows[0]["Chem_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Brick_Remarks"].ToString() != string.Empty)
                {
                    trBricks.Visible = true;
                    reBricks.Text = ds.Tables[0].Rows[0]["Brick_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Steel_Remarks"].ToString() != string.Empty)
                {
                    trSteels.Visible = true;
                    reSteels.Text = ds.Tables[0].Rows[0]["Steel_Remarks"].ToString();

                }
                if (ds.Tables[0].Rows[0]["Oth_Const_Remarks"].ToString() != string.Empty)
                {
                    trOtherc.Visible = true;
                    reOtherc.Text = ds.Tables[0].Rows[0]["Oth_Const_Remarks"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Other_Remarks"].ToString() != string.Empty)
                {
                    trOthers.Visible = true;
                    reOthers.Text = ds.Tables[0].Rows[0]["Other_Remarks"].ToString();

                }

            }


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }

    }


   
  
   

    protected void BindProjectList()
    {
        try
        {
            objProjectBL = new ProjectBL();
            ds = new DataSet();
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_ALL, ref ds);
            ddlProject.DataTextField = "Project_Name";
            ddlProject.DataValueField = "Project_Code";
            ddlProject.DataSource = ds;
            ddlProject.DataBind();
            ddlProject.Items.Insert(0, "-Select-");

            ddlExistingProjectName.DataSource = ds;
            ddlExistingProjectName.DataTextField = "Project_Name";
            ddlExistingProjectName.DataValueField = "Project_Code";

            ddlExistingProjectName.DataBind();
            ddlExistingProjectName.Items.Insert(0, "-Select-");

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }
    protected void BindMonth()
    {
        try
        {
            objBudgetBL = new BudgetBL();
            ds = new DataSet();
            objBudgetBL.load(con, BudgetBL.eLoadSp.SELECT_MONTH, ref ds);
            ddlMonth.DataSource = ds;
            ddlMonth.DataTextField = "Month";
            ddlMonth.DataValueField = "Month_ID";
            ddlMonth.DataBind();
            ddlMonth.Items.Insert(0, "-Select-");
            if (Convert.ToInt32(DateTime.Now.Month.ToString()) < 12)
            {
                ddlMonth.SelectedValue = (Convert.ToInt32(DateTime.Now.Month.ToString()) + 1).ToString();
            }
            else
            {
                ddlMonth.SelectedValue = "1";
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    protected void BindUserList()
    {
        try
        {

            objIndent = new IndentBL();
            ds = new DataSet();
            bool exists;
            DataTable DatafilterDt = new DataTable();
            objIndent.load(con, IndentBL.eLoadSp.SELECT_USERNAMES_ALL, ref ds);


            DatafilterDt = ds.Tables[0];

            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<bool>("IsHoUser").Equals(true) && c.Field<string>("Status") == "Active").Count() > 0;
            if (exists)
            {
                DataTable Itemsdt = DatafilterDt.AsEnumerable()
                             .Where(r => r.Field<bool>("IsHoUser") == true && r.Field<string>("Status") == "Active")
                             .CopyToDataTable();


                ddlPromaryPerson.DataSource = Itemsdt;
                ddlPromaryPerson.DataValueField = "UID";
                ddlPromaryPerson.DataTextField = "Name";
                ddlPromaryPerson.DataBind();
                ddlPromaryPerson.Items.Insert(0, "-Select-");

                ddlReportingPerson.DataSource = Itemsdt;
                ddlReportingPerson.DataValueField = "UID";
                ddlReportingPerson.DataTextField = "Name";
                ddlReportingPerson.DataBind();


            }


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
                ddlAssetName_r.DataSource = ds;
                ddlAssetName_r.DataTextField = "AssetNameWithCode";
                ddlAssetName_r.DataValueField = "Code";
                ddlAssetName_r.DataBind();
                ddlAssetName_r.Items.Insert(0, "-Select-");



            }
            else
            {
                ddlAssetName_r.Items.Clear();
                ddlAssetName_r.Items.Insert(0, "-Select-");
                ddlAssetName_r.SelectedIndex = 0;

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
            objUOM.load(con, UOM.eLoadSp.SELECT_ALL, ref ds);

            ddlUnit_r.DataSource = ds;
            ddlUnit_r.DataTextField = "UOMPrefix";
            ddlUnit_r.DataValueField = "UOM_ID";
            ddlUnit_r.DataBind();
            ddlUnit_r.Items.Insert(0, "-Select-");


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
            objCategory.load(con, Category.eLoadSp.SELECT_ALL, ref ds);

            ddlCateGory_r.DataSource = ds;
            ddlCateGory_r.DataTextField = "Category_Name";
            ddlCateGory_r.DataValueField = "Mat_cat_ID";
            ddlCateGory_r.DataBind();
            ddlCateGory_r.Items.Insert(0, "-Select-");

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    private void BindAssetType()
    {
        try
        {
            ds = new DataSet();
            objAsset = new AssetRegistrationBL();
            objAsset.load(con, AssetRegistrationBL.eLoadSp.SELECT_ASSET_TYPE_ALL, ref ds);

            ddlAssetType_r.DataSource = ds;
            ddlAssetType_r.DataTextField = "Asset_Type";
            ddlAssetType_r.DataValueField = "Asset_Type_ID";
            ddlAssetType_r.DataBind();
            ddlAssetType_r.Items.Insert(0, "-Select-");


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
            if (ddlAssetType_r.SelectedIndex != 0)
            {
                objAsset = new AssetRegistrationBL();
                DataSet ds = new DataSet();
                objAsset.Asset_Type = ddlAssetType_r.SelectedValue;
                objAsset.load(con, AssetRegistrationBL.eLoadSp.SELECT_Asset_BY_ID, ref ds);
                ddlAssetCate_r.DataSource = ds;
                ddlAssetCate_r.DataValueField = "AssetCatID";
                ddlAssetCate_r.DataTextField = "CateName";
                ddlAssetCate_r.DataBind();
                ddlAssetCate_r.Items.Insert(0, "-Select-");

                ddlAssetName_r.Items.Clear();
                ddlAssetName_r.Items.Insert(0, "-Select-");
                ddlAssetName_r.SelectedIndex = 0;

            }
            else
            {
                ddlAssetCate_r.Items.Clear();
                ddlAssetCate_r.Items.Insert(0, "-Select-");
                ddlAssetCate_r.SelectedIndex = 0;
                ddlAssetName_r.Items.Clear();
                ddlAssetName_r.Items.Insert(0, "-Select-");
                ddlAssetName_r.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void LoadBudgetDetailsByAbs_BID(string Abs_BID, bool status)
    {

        try
        {

            if (Request.QueryString["MFYRQ"] != null && Request.QueryString["MFYRQ"].ToString() == "1")
            {
                status = true;
                // Add material item
                img_AutoNew.Visible = status;
                Imag_PlantCSS.Visible = status;
                Imag_shuttCSS.Visible = status;
                ImageCSS_Consu.Visible = status;
                ImagCSS_Elect.Visible = status;
                ImageCSS_HSD.Visible = status;
                ImageCSS_Oil.Visible = status;
                ImageCSS_Hard.Visible = status;
                ImagCSS_Wedd.Visible = status;
                ImageCSS_Oxygen.Visible = status;
                ImageBfCSS_Safe.Visible = status;
                ImageCSS_Staff.Visible = status;
                ImageBCSS_Mess.Visible = status;
                ImageCSS_Print.Visible = status;
                ImageCSS_Repair.Visible = status;
                ImagefCSS_BOQ.Visible = status;
                ImageBfCSS_Sanit.Visible = status;
                ImagCSS_Blast.Visible = status;
                ImageCSS_Furn.Visible = status;
                ImageCSS_Fixed.Visible = status;
                ImaCSS_Infr.Visible = status;
                ImagCSS_Sand.Visible = status;
                ImageCSS_Jelly.Visible = status;
                ImageCSS_Red.Visible = status;
                ImagCSS_Cement.Visible = status;
                ImagfCSS_Chemi.Visible = status;
                ImaCSS_Bricks.Visible = status;
                ImagCSS_steel.Visible = status;
                ImagefCSS_OtherCons.Visible = status;
                ImageCSS_Others.Visible = status;

                // Add new Item 

                PopupItem_Auto.Visible = status;
                img_Plant.Visible = status;
                img_Shutter.Visible = status;
                Img_Consume.Visible = status;
                img_Elect.Visible = status;
                img_HSD.Visible = status;
                img_Oil.Visible = status;
                img_Hardware.Visible = status;
                img_Welding.Visible = status;
                img_Oxygen.Visible = status;
                img_Safety.Visible = status;
                img_Staff.Visible = status;
                img_Mess.Visible = status;
                img_Printing.Visible = status;
                img_Repairs.Visible = status;
                img_BOQ.Visible = status;
                img_Sanitary.Visible = status;
                img_Blasting.Visible = status;
                img_Furnitures.Visible = status;
                img_FixedAssets.Visible = status;
                img_Infrastructure.Visible = status;
                img_Sand.Visible = status;
                img_Jelly.Visible = status;
                img_RedSoil.Visible = status;
                img_Cement.Visible = status;
                img_Chemicals.Visible = status;
                img_Bricks.Visible = status;
                img_Steels.Visible = status;
                img_OtherConstruction.Visible = status;
                img_Others.Visible = status;

                // Refer exists Budget Items

                imgref_AutoNew.Visible = status;
                imgref_PlantCSS.Visible = status;
                imgref_shuttCSS.Visible = status;
                imgrefCSS_Consu.Visible = status;
                imgrefCSS_Elect.Visible = status;
                imgrefCSS_HSD.Visible = status;
                imgrefCSS_Oil.Visible = status;
                imgrefCSS_Hard.Visible = status;
                imgrefCSS_Wedd.Visible = status;
                imgrefCSS_Oxygen.Visible = status;
                imgrefCSS_Safe.Visible = status;
                imgrefCSS_Staff.Visible = status;
                imgrefCSS_Mess.Visible = status;
                imgrefCSS_Print.Visible = status;
                imgrefCSS_Repair.Visible = status;
                imgrefCSS_BOQ.Visible = status;
                imgrefCSS_Sanit.Visible = status;
                imgrefCSS_Blast.Visible = status;
                imgrefCSS_Furn.Visible = status;
                imgrefCSS_Fixed.Visible = status;
                imgrefCSS_Infr.Visible = status;
                imgrefCSS_Sand.Visible = status;
                imgrefCSS_Jelly.Visible = status;
                imgrefCSS_Red.Visible = status;
                imgrefCSS_Cement.Visible = status;
                imgrefCSS_Chemi.Visible = status;
                imgrefCSS_Bricks.Visible = status;
                imgrefCSS_steel.Visible = status;
                imgrefCSS_OtherCons.Visible = status;
                imgrefCSS_Others.Visible = status;

                // Item Edit and Delete 


                AutomobilesList.Columns[10].Visible = status;
                AutomobilesList.Columns[9].Visible = status;

                GridPlant.Columns[10].Visible = status;
                GridPlant.Columns[9].Visible = status;

                GridShutter.Columns[9].Visible = status;
                GridShutter.Columns[8].Visible = status;

                GridConsume.Columns[9].Visible = status;
                GridConsume.Columns[8].Visible = status;

                Grid_Elect.Columns[9].Visible = status;
                Grid_Elect.Columns[8].Visible = status;


                Grid_HSD.Columns[9].Visible = status;
                Grid_HSD.Columns[8].Visible = status;

                Grid_Oil.Columns[9].Visible = status;
                Grid_Oil.Columns[8].Visible = status;

                Grid_Hardware.Columns[9].Visible = status;
                Grid_Hardware.Columns[8].Visible = status;

                Grid_Welding.Columns[9].Visible = status;
                Grid_Welding.Columns[8].Visible = status;

                Grid_Oxygen.Columns[9].Visible = status;
                Grid_Oxygen.Columns[8].Visible = status;

                Grid_Safety.Columns[9].Visible = status;
                Grid_Safety.Columns[8].Visible = status;

                Grid_Staff.Columns[9].Visible = status;
                Grid_Staff.Columns[8].Visible = status;

                Grid_Mess.Columns[9].Visible = status;
                Grid_Mess.Columns[8].Visible = status;

                Grid_Printing.Columns[9].Visible = status;
                Grid_Printing.Columns[8].Visible = status;

                Grid_Repairs.Columns[9].Visible = status;
                Grid_Repairs.Columns[8].Visible = status;

                Grid_BOQ.Columns[9].Visible = status;
                Grid_BOQ.Columns[8].Visible = status;

                Grid_Sanitary.Columns[9].Visible = status;
                Grid_Sanitary.Columns[8].Visible = status;

                Grid_Blasting.Columns[9].Visible = status;
                Grid_Blasting.Columns[8].Visible = status;

                Grid_Furnitures.Columns[9].Visible = status;
                Grid_Furnitures.Columns[8].Visible = status;

                Grid_Fixed_Assets.Columns[9].Visible = status;
                Grid_Fixed_Assets.Columns[8].Visible = status;

                Grid_Infrastructure.Columns[9].Visible = status;
                Grid_Infrastructure.Columns[8].Visible = status;

                Grid_Sand.Columns[9].Visible = status;
                Grid_Sand.Columns[8].Visible = status;

                Grid_Jelly.Columns[9].Visible = status;
                Grid_Jelly.Columns[8].Visible = status;

                Grid_RedSoil.Columns[9].Visible = status;
                Grid_RedSoil.Columns[8].Visible = status;

                Grid_Cement.Columns[9].Visible = status;
                Grid_Cement.Columns[8].Visible = status;

                Grid_Chemicals.Columns[9].Visible = status;
                Grid_Chemicals.Columns[8].Visible = status;

                Grid_Bricks.Columns[9].Visible = status;
                Grid_Bricks.Columns[8].Visible = status;

                Grid_Steels.Columns[9].Visible = status;
                Grid_Steels.Columns[8].Visible = status;

                Grid_Other_Construction.Columns[9].Visible = status;
                Grid_Other_Construction.Columns[8].Visible = status;

                Grid_Others.Columns[9].Visible = status;
                Grid_Others.Columns[8].Visible = status;
            }
            else
            {
                if (status)
                {
                    objBudgetBL = new BudgetBL();
                    ds = new DataSet();
                    objBudgetBL.Abs_BID = Convert.ToInt32(Abs_BID);
                    objBudgetBL.load(con, BudgetBL.eLoadSp.SELECT_BY_ID, ref ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        // Add new item
                        if (ds.Tables[0].Rows[0]["Auto_ApprovalStatus"].ToString() == "Approved")
                        {
                            img_AutoNew.Visible = false;
                            PopupItem_Auto.Visible = false;
                            imgref_AutoNew.Visible = false;
                            AutomobilesList.Columns[10].Visible = false;
                            AutomobilesList.Columns[9].Visible = false;
                        }
                        else
                        {
                            img_AutoNew.Visible = true;
                            PopupItem_Auto.Visible = true;
                            imgref_AutoNew.Visible = true;
                            AutomobilesList.Columns[10].Visible = true;
                            AutomobilesList.Columns[9].Visible = true;
                        }
                        if (ds.Tables[0].Rows[0]["PlMach_ApprovalStatus"].ToString() == "Approved")
                        {
                            Imag_PlantCSS.Visible = false;
                            img_Plant.Visible = false;
                            imgref_PlantCSS.Visible = false;
                            GridPlant.Columns[10].Visible = false;
                            GridPlant.Columns[9].Visible = false;
                        }
                        else
                        {
                            Imag_PlantCSS.Visible = true;
                            img_Plant.Visible = true;
                            imgref_PlantCSS.Visible = true;
                            GridPlant.Columns[10].Visible = true;
                            GridPlant.Columns[9].Visible = true;
                        }
                        if (ds.Tables[0].Rows[0]["Shutter_Mat_ApprovalStatus"].ToString() == "Approved")
                        {
                            Imag_shuttCSS.Visible = false;
                            img_Shutter.Visible = false;
                            imgref_shuttCSS.Visible = false;
                            GridShutter.Columns[9].Visible = false;
                            GridShutter.Columns[8].Visible = false;
                        }
                        else
                        {
                            Imag_shuttCSS.Visible = true;
                            img_Shutter.Visible = true;
                            imgref_shuttCSS.Visible = true;
                            GridShutter.Columns[9].Visible = true;
                            GridShutter.Columns[8].Visible = true;
                        }
                        if (ds.Tables[0].Rows[0]["Consumable_ApprovalStatus"].ToString() == "Approved")
                        {
                            ImageCSS_Consu.Visible = false;
                            Img_Consume.Visible = false;
                            imgrefCSS_Consu.Visible = false;
                            GridConsume.Columns[9].Visible = false;
                            GridConsume.Columns[8].Visible = false;
                        }
                        else
                        {
                            ImageCSS_Consu.Visible = true;
                            Img_Consume.Visible = true;
                            imgrefCSS_Consu.Visible = true;
                            GridConsume.Columns[9].Visible = true;
                            GridConsume.Columns[8].Visible = true;
                        }
                        if (ds.Tables[0].Rows[0]["Elec_ApprovalStatus"].ToString() == "Approved")
                        {
                            ImagCSS_Elect.Visible = false;
                            img_Elect.Visible = false;
                            imgrefCSS_Elect.Visible = false;
                            Grid_Elect.Columns[9].Visible = false;
                            Grid_Elect.Columns[8].Visible = false;
                        }
                        else
                        {
                            ImagCSS_Elect.Visible = true;
                            img_Elect.Visible = true;
                            imgrefCSS_Elect.Visible = true;
                            Grid_Elect.Columns[9].Visible = true;
                            Grid_Elect.Columns[8].Visible = true;
                        }
                        if (ds.Tables[0].Rows[0]["HSD_Pet_ApprovalStatus"].ToString() == "Approved")
                        {
                            ImageCSS_HSD.Visible = false;
                            img_HSD.Visible = false;
                            imgrefCSS_HSD.Visible = false;
                            Grid_HSD.Columns[9].Visible = false;
                            Grid_HSD.Columns[8].Visible = false;
                        }
                        else
                        {
                            ImageCSS_HSD.Visible = true;
                            img_HSD.Visible = true;
                            imgrefCSS_HSD.Visible = true;
                            Grid_HSD.Columns[9].Visible = true;
                            Grid_HSD.Columns[8].Visible = true;
                        }
                        if (ds.Tables[0].Rows[0]["Oil_Lube_ApprovalStatus"].ToString() == "Approved")
                        {
                            ImageCSS_Oil.Visible = false;
                            img_Oil.Visible = false;
                            imgrefCSS_Oil.Visible = false;
                            Grid_Oil.Columns[9].Visible = false;
                            Grid_Oil.Columns[8].Visible = false;
                        }
                        else
                        {
                            ImageCSS_Oil.Visible = true;
                            img_Oil.Visible = true;
                            imgrefCSS_Oil.Visible = true;
                            Grid_Oil.Columns[9].Visible = true;
                            Grid_Oil.Columns[8].Visible = true;
                        }
                        if (ds.Tables[0].Rows[0]["Hardware_ApprovalStatus"].ToString() == "Approved")
                        {
                            ImageCSS_Hard.Visible = false;
                            img_Hardware.Visible = false;
                            imgrefCSS_Hard.Visible = false;
                            Grid_Hardware.Columns[9].Visible = false;
                            Grid_Hardware.Columns[8].Visible = false;
                        }
                        else
                        {
                            ImageCSS_Hard.Visible = true;
                            img_Hardware.Visible = true;
                            imgrefCSS_Hard.Visible = true;
                            Grid_Hardware.Columns[9].Visible = true;
                            Grid_Hardware.Columns[8].Visible = true;
                        }
                        if (ds.Tables[0].Rows[0]["Weld_Elec_ApprovalStatus"].ToString() == "Approved")
                        {
                            ImagCSS_Wedd.Visible = false;
                            img_Welding.Visible = false;
                            imgrefCSS_Wedd.Visible = false;
                            Grid_Welding.Columns[9].Visible = false;
                            Grid_Welding.Columns[8].Visible = false;

                        }
                        else
                        {
                            ImagCSS_Wedd.Visible = true;
                            img_Welding.Visible = true;
                            imgrefCSS_Wedd.Visible = true;
                            Grid_Welding.Columns[9].Visible = true;
                            Grid_Welding.Columns[8].Visible = true;

                        }
                        if (ds.Tables[0].Rows[0]["Oxygen_ace_ApprovalStatus"].ToString() == "Approved")
                        {
                            ImageCSS_Oxygen.Visible = false;
                            img_Oxygen.Visible = false;
                            imgrefCSS_Oxygen.Visible = false;
                            Grid_Oxygen.Columns[9].Visible = false;
                            Grid_Oxygen.Columns[8].Visible = false;
                        }
                        else
                        {
                            ImageCSS_Oxygen.Visible = true;
                            img_Oxygen.Visible = true;
                            imgrefCSS_Oxygen.Visible = true;
                            Grid_Oxygen.Columns[9].Visible = true;
                            Grid_Oxygen.Columns[8].Visible = true;
                        }

                        if (ds.Tables[0].Rows[0]["Safety_Item_ApprovalStatus"].ToString() == "Approved")
                        {
                            ImageBfCSS_Safe.Visible = false;
                            img_Safety.Visible = false;
                            imgrefCSS_Safe.Visible = false;
                            Grid_Safety.Columns[9].Visible = false;
                            Grid_Safety.Columns[8].Visible = false;
                        }
                        else
                        {
                            ImageBfCSS_Safe.Visible = true;
                            img_Safety.Visible = true;
                            imgrefCSS_Safe.Visible = true;
                            Grid_Safety.Columns[9].Visible = true;
                            Grid_Safety.Columns[8].Visible = true;
                        }
                        if (ds.Tables[0].Rows[0]["Staff_wel_ApprovalStatus"].ToString() == "Approved")
                        {
                            img_Staff.Visible = false;
                            ImageCSS_Staff.Visible = false;
                            imgrefCSS_Staff.Visible = false;
                            Grid_Staff.Columns[9].Visible = false;
                            Grid_Staff.Columns[8].Visible = false;
                        }
                        else
                        {
                            img_Staff.Visible = true;
                            ImageCSS_Staff.Visible = true;
                            imgrefCSS_Staff.Visible = true;
                            Grid_Staff.Columns[9].Visible = true;
                            Grid_Staff.Columns[8].Visible = true;

                        }
                        if (ds.Tables[0].Rows[0]["Mess_Expense_ApprovalStatus"].ToString() == "Approved")
                        {
                            img_Mess.Visible = false;
                            ImageBCSS_Mess.Visible = false;
                            imgrefCSS_Mess.Visible = false;
                            Grid_Mess.Columns[9].Visible = false;
                            Grid_Mess.Columns[8].Visible = false;
                        }
                        else
                        {
                            img_Mess.Visible = true;
                            ImageBCSS_Mess.Visible = true;
                            imgrefCSS_Mess.Visible = true;
                            Grid_Mess.Columns[9].Visible = true;
                            Grid_Mess.Columns[8].Visible = true;
                        }
                        if (ds.Tables[0].Rows[0]["Print_Sta_ApprovalStatus"].ToString() == "Approved")
                        {
                            img_Printing.Visible = false;
                            ImageCSS_Print.Visible = false;
                            imgrefCSS_Print.Visible = false;
                            Grid_Printing.Columns[9].Visible = false;
                            Grid_Printing.Columns[8].Visible = false;
                        }
                        else
                        {
                            img_Printing.Visible = true;
                            ImageCSS_Print.Visible = true;
                            imgrefCSS_Print.Visible = true;
                            Grid_Printing.Columns[9].Visible = true;
                            Grid_Printing.Columns[8].Visible = true;

                        }
                        if (ds.Tables[0].Rows[0]["Repair_Maint_ApprovalStatus"].ToString() == "Approved")
                        {
                            img_Repairs.Visible = false;
                            ImageCSS_Repair.Visible = false;
                            imgrefCSS_Repair.Visible = false;
                            Grid_Repairs.Columns[9].Visible = false;
                            Grid_Repairs.Columns[8].Visible = false;
                        }
                        else
                        {
                            img_Repairs.Visible = true;
                            ImageCSS_Repair.Visible = true;
                            imgrefCSS_Repair.Visible = true;
                            Grid_Repairs.Columns[9].Visible = true;
                            Grid_Repairs.Columns[8].Visible = true;
                        }
                        if (ds.Tables[0].Rows[0]["BOQ_ApprovalStatus"].ToString() == "Approved")
                        {
                            img_BOQ.Visible = false;
                            ImagefCSS_BOQ.Visible = false;
                            imgrefCSS_BOQ.Visible = false;
                            Grid_BOQ.Columns[9].Visible = false;
                            Grid_BOQ.Columns[8].Visible = false;
                        }
                        else
                        {
                            img_BOQ.Visible = true;
                            ImagefCSS_BOQ.Visible = true;
                            imgrefCSS_BOQ.Visible = true;
                            Grid_BOQ.Columns[9].Visible = true;
                            Grid_BOQ.Columns[8].Visible = true;

                        }
                        if (ds.Tables[0].Rows[0]["Sanitary_ApprovalStatus"].ToString() == "Approved")
                        {
                            img_Sanitary.Visible = false;
                            ImageBfCSS_Sanit.Visible = false;
                            imgrefCSS_Sanit.Visible = false;
                            Grid_Sanitary.Columns[9].Visible = false;
                            Grid_Sanitary.Columns[8].Visible = false;
                        }
                        else
                        {
                            img_Sanitary.Visible = true;
                            ImageBfCSS_Sanit.Visible = true;
                            imgrefCSS_Sanit.Visible = true;
                            Grid_Sanitary.Columns[9].Visible = true;
                            Grid_Sanitary.Columns[8].Visible = true;
                        }
                        if (ds.Tables[0].Rows[0]["Blast_ma_ApprovalStatus"].ToString() == "Approved")
                        {
                            img_Blasting.Visible = false;
                            ImagCSS_Blast.Visible = false;
                            imgrefCSS_Blast.Visible = false;
                            Grid_Blasting.Columns[9].Visible = false;
                            Grid_Blasting.Columns[8].Visible = false;
                        }
                        else
                        {
                            img_Blasting.Visible = true;
                            ImagCSS_Blast.Visible = true;
                            imgrefCSS_Blast.Visible = true;
                            Grid_Blasting.Columns[9].Visible = true;
                            Grid_Blasting.Columns[8].Visible = true;
                        }
                        if (ds.Tables[0].Rows[0]["FnF_ApprovalStatus"].ToString() == "Approved")
                        {
                            img_Furnitures.Visible = false;
                            ImageCSS_Furn.Visible = false;
                            imgrefCSS_Furn.Visible = false;
                            Grid_Furnitures.Columns[9].Visible = false;
                            Grid_Furnitures.Columns[8].Visible = false;

                        }
                        else
                        {
                            img_Furnitures.Visible = true;
                            ImageCSS_Furn.Visible = true;
                            imgrefCSS_Furn.Visible = true;
                            Grid_Furnitures.Columns[9].Visible = true;
                            Grid_Furnitures.Columns[8].Visible = true;
                        }
                        if (ds.Tables[0].Rows[0]["Fix_Asset_ApprovalStatus"].ToString() == "Approved")
                        {
                            img_FixedAssets.Visible = false;
                            ImageCSS_Fixed.Visible = false;
                            imgrefCSS_Fixed.Visible = false;
                            Grid_Fixed_Assets.Columns[9].Visible = false;
                            Grid_Fixed_Assets.Columns[8].Visible = false;
                        }
                        else
                        {
                            img_FixedAssets.Visible = true;
                            ImageCSS_Fixed.Visible = true;
                            imgrefCSS_Fixed.Visible = true;
                            Grid_Fixed_Assets.Columns[9].Visible = true;
                            Grid_Fixed_Assets.Columns[8].Visible = true;
                        }
                        if (ds.Tables[0].Rows[0]["Infra_ApprovalStatus"].ToString() == "Approved")
                        {
                            img_Infrastructure.Visible = false;
                            ImaCSS_Infr.Visible = false;
                            imgrefCSS_Infr.Visible = false;
                            Grid_Infrastructure.Columns[9].Visible = false;
                            Grid_Infrastructure.Columns[8].Visible = false;
                        }
                        else
                        {
                            img_Infrastructure.Visible = true;
                            ImaCSS_Infr.Visible = true;
                            imgrefCSS_Infr.Visible = true;
                            Grid_Infrastructure.Columns[9].Visible = true;
                            Grid_Infrastructure.Columns[8].Visible = true;

                        }

                        if (ds.Tables[0].Rows[0]["Sand_ApprovalStatus"].ToString() == "Approved")
                        {
                            img_Sand.Visible = false;
                            ImagCSS_Sand.Visible = false;
                            imgrefCSS_Sand.Visible = false;
                            Grid_Sand.Columns[9].Visible = false;
                            Grid_Sand.Columns[8].Visible = false;
                        }
                        else
                        {
                            img_Sand.Visible = true;
                            ImagCSS_Sand.Visible = true;
                            imgrefCSS_Sand.Visible = true;
                            Grid_Sand.Columns[9].Visible = true;
                            Grid_Sand.Columns[8].Visible = true;

                        }
                        if (ds.Tables[0].Rows[0]["Jelly_Metal_ApprovalStatus"].ToString() == "Approved")
                        {
                            img_Jelly.Visible = false;
                            ImageCSS_Jelly.Visible = false;
                            imgrefCSS_Jelly.Visible = false;
                            Grid_Jelly.Columns[9].Visible = false;
                            Grid_Jelly.Columns[8].Visible = false;
                        }
                        else
                        {
                            img_Jelly.Visible = true;
                            ImageCSS_Jelly.Visible = true;
                            imgrefCSS_Jelly.Visible = true;
                            Grid_Jelly.Columns[9].Visible = true;
                            Grid_Jelly.Columns[8].Visible = true;
                        }
                        if (ds.Tables[0].Rows[0]["Red_Soil_ApprovalStatus"].ToString() == "Approved")
                        {
                            img_RedSoil.Visible = false;
                            ImageCSS_Red.Visible = false;
                            imgrefCSS_Red.Visible = false;
                            Grid_RedSoil.Columns[9].Visible = false;
                            Grid_RedSoil.Columns[8].Visible = false;
                        }
                        else
                        {
                            img_RedSoil.Visible = true;
                            ImageCSS_Red.Visible = true;
                            imgrefCSS_Red.Visible = true;
                            Grid_RedSoil.Columns[9].Visible = true;
                            Grid_RedSoil.Columns[8].Visible = true;

                        }
                        if (ds.Tables[0].Rows[0]["Cement_ApprovalStatus"].ToString() == "Approved")
                        {
                            img_Cement.Visible = false;
                            ImagCSS_Cement.Visible = false;
                            imgrefCSS_Cement.Visible = false;
                            Grid_Cement.Columns[9].Visible = false;
                            Grid_Cement.Columns[8].Visible = false;

                        }
                        else
                        {
                            img_Cement.Visible = true;
                            ImagCSS_Cement.Visible = true;
                            imgrefCSS_Cement.Visible = true;
                            Grid_Cement.Columns[9].Visible = true;
                            Grid_Cement.Columns[8].Visible = true;
                        }
                        if (ds.Tables[0].Rows[0]["Chem_ApprovalStatus"].ToString() == "Approved")
                        {
                            img_Chemicals.Visible = false;
                            ImagfCSS_Chemi.Visible = false;
                            imgrefCSS_Chemi.Visible = false;
                            Grid_Chemicals.Columns[9].Visible = false;
                            Grid_Chemicals.Columns[8].Visible = false;
                        }
                        else
                        {
                            img_Chemicals.Visible = true;
                            ImagfCSS_Chemi.Visible = true;
                            imgrefCSS_Chemi.Visible = true;
                            Grid_Chemicals.Columns[9].Visible = true;
                            Grid_Chemicals.Columns[8].Visible = true;
                        }
                        if (ds.Tables[0].Rows[0]["Brick_ApprovalStatus"].ToString() == "Approved")
                        {
                            img_Bricks.Visible = false;
                            ImaCSS_Bricks.Visible = false;
                            imgrefCSS_Bricks.Visible = false;
                            Grid_Bricks.Columns[9].Visible = false;
                            Grid_Bricks.Columns[8].Visible = false;
                        }
                        else
                        {
                            img_Bricks.Visible = true;
                            ImaCSS_Bricks.Visible = true;
                            imgrefCSS_Bricks.Visible = true;
                            Grid_Bricks.Columns[9].Visible = true;
                            Grid_Bricks.Columns[8].Visible = true;
                        }
                        if (ds.Tables[0].Rows[0]["Steel_ApprovalStatus"].ToString() == "Approved")
                        {
                            img_Steels.Visible = false;
                            ImagCSS_steel.Visible = false;
                            imgrefCSS_steel.Visible = false;
                            Grid_Steels.Columns[9].Visible = false;
                            Grid_Steels.Columns[8].Visible = false;
                        }
                        else
                        {
                            img_Steels.Visible = true;
                            ImagCSS_steel.Visible = true;
                            imgrefCSS_steel.Visible = true;
                            Grid_Steels.Columns[9].Visible = true;
                            Grid_Steels.Columns[8].Visible = true;
                        }
                        if (ds.Tables[0].Rows[0]["Oth_Const_ApprovalStatus"].ToString() == "Approved")
                        {
                            img_OtherConstruction.Visible = false;
                            ImagefCSS_OtherCons.Visible = false;
                            imgrefCSS_OtherCons.Visible = false;
                            Grid_Other_Construction.Columns[9].Visible = false;
                            Grid_Other_Construction.Columns[8].Visible = false;

                        }
                        else
                        {
                            img_OtherConstruction.Visible = true;
                            ImagefCSS_OtherCons.Visible = true;
                            imgrefCSS_OtherCons.Visible = true;
                            Grid_Other_Construction.Columns[9].Visible = true;
                            Grid_Other_Construction.Columns[8].Visible = true;

                        }
                        if (ds.Tables[0].Rows[0]["Other_ApprovalStatus"].ToString() == "Approved")
                        {
                            img_Others.Visible = false;
                            ImageCSS_Others.Visible = false;
                            imgrefCSS_Others.Visible = false;
                            Grid_Others.Columns[9].Visible = false;
                            Grid_Others.Columns[8].Visible = false;
                        }
                        else
                        {
                            img_Others.Visible = true;
                            ImageCSS_Others.Visible = true;
                            imgrefCSS_Others.Visible = true;
                            Grid_Others.Columns[9].Visible = true;
                            Grid_Others.Columns[8].Visible = true;
                        }
                    }
                }
                else
                {
                    // Add material item
                    img_AutoNew.Visible = status;
                    Imag_PlantCSS.Visible = status;
                    Imag_shuttCSS.Visible = status;
                    ImageCSS_Consu.Visible = status;
                    ImagCSS_Elect.Visible = status;
                    ImageCSS_HSD.Visible = status;
                    ImageCSS_Oil.Visible = status;
                    ImageCSS_Hard.Visible = status;
                    ImagCSS_Wedd.Visible = status;
                    ImageCSS_Oxygen.Visible = status;
                    ImageBfCSS_Safe.Visible = status;
                    ImageCSS_Staff.Visible = status;
                    ImageBCSS_Mess.Visible = status;
                    ImageCSS_Print.Visible = status;
                    ImageCSS_Repair.Visible = status;
                    ImagefCSS_BOQ.Visible = status;
                    ImageBfCSS_Sanit.Visible = status;
                    ImagCSS_Blast.Visible = status;
                    ImageCSS_Furn.Visible = status;
                    ImageCSS_Fixed.Visible = status;
                    ImaCSS_Infr.Visible = status;
                    ImagCSS_Sand.Visible = status;
                    ImageCSS_Jelly.Visible = status;
                    ImageCSS_Red.Visible = status;
                    ImagCSS_Cement.Visible = status;
                    ImagfCSS_Chemi.Visible = status;
                    ImaCSS_Bricks.Visible = status;
                    ImagCSS_steel.Visible = status;
                    ImagefCSS_OtherCons.Visible = status;
                    ImageCSS_Others.Visible = status;

                    // Add new Item 

                    PopupItem_Auto.Visible = status;
                    img_Plant.Visible = status;
                    img_Shutter.Visible = status;
                    Img_Consume.Visible = status;
                    img_Elect.Visible = status;
                    img_HSD.Visible = status;
                    img_Oil.Visible = status;
                    img_Hardware.Visible = status;
                    img_Welding.Visible = status;
                    img_Oxygen.Visible = status;
                    img_Safety.Visible = status;
                    img_Staff.Visible = status;
                    img_Mess.Visible = status;
                    img_Printing.Visible = status;
                    img_Repairs.Visible = status;
                    img_BOQ.Visible = status;
                    img_Sanitary.Visible = status;
                    img_Blasting.Visible = status;
                    img_Furnitures.Visible = status;
                    img_FixedAssets.Visible = status;
                    img_Infrastructure.Visible = status;
                    img_Sand.Visible = status;
                    img_Jelly.Visible = status;
                    img_RedSoil.Visible = status;
                    img_Cement.Visible = status;
                    img_Chemicals.Visible = status;
                    img_Bricks.Visible = status;
                    img_Steels.Visible = status;
                    img_OtherConstruction.Visible = status;
                    img_Others.Visible = status;

                    // Refer exists Budget Items

                    imgref_AutoNew.Visible = status;
                    imgref_PlantCSS.Visible = status;
                    imgref_shuttCSS.Visible = status;
                    imgrefCSS_Consu.Visible = status;
                    imgrefCSS_Elect.Visible = status;
                    imgrefCSS_HSD.Visible = status;
                    imgrefCSS_Oil.Visible = status;
                    imgrefCSS_Hard.Visible = status;
                    imgrefCSS_Wedd.Visible = status;
                    imgrefCSS_Oxygen.Visible = status;
                    imgrefCSS_Safe.Visible = status;
                    imgrefCSS_Staff.Visible = status;
                    imgrefCSS_Mess.Visible = status;
                    imgrefCSS_Print.Visible = status;
                    imgrefCSS_Repair.Visible = status;
                    imgrefCSS_BOQ.Visible = status;
                    imgrefCSS_Sanit.Visible = status;
                    imgrefCSS_Blast.Visible = status;
                    imgrefCSS_Furn.Visible = status;
                    imgrefCSS_Fixed.Visible = status;
                    imgrefCSS_Infr.Visible = status;
                    imgrefCSS_Sand.Visible = status;
                    imgrefCSS_Jelly.Visible = status;
                    imgrefCSS_Red.Visible = status;
                    imgrefCSS_Cement.Visible = status;
                    imgrefCSS_Chemi.Visible = status;
                    imgrefCSS_Bricks.Visible = status;
                    imgrefCSS_steel.Visible = status;
                    imgrefCSS_OtherCons.Visible = status;
                    imgrefCSS_Others.Visible = status;

                    // Item Edit and Delete 


                    AutomobilesList.Columns[10].Visible = status;
                    AutomobilesList.Columns[9].Visible = status;

                    GridPlant.Columns[10].Visible = status;
                    GridPlant.Columns[9].Visible = status;

                    GridShutter.Columns[9].Visible = status;
                    GridShutter.Columns[8].Visible = status;

                    GridConsume.Columns[9].Visible = status;
                    GridConsume.Columns[8].Visible = status;

                    Grid_Elect.Columns[9].Visible = status;
                    Grid_Elect.Columns[8].Visible = status;


                    Grid_HSD.Columns[9].Visible = status;
                    Grid_HSD.Columns[8].Visible = status;

                    Grid_Oil.Columns[9].Visible = status;
                    Grid_Oil.Columns[8].Visible = status;

                    Grid_Hardware.Columns[9].Visible = status;
                    Grid_Hardware.Columns[8].Visible = status;

                    Grid_Welding.Columns[9].Visible = status;
                    Grid_Welding.Columns[8].Visible = status;

                    Grid_Oxygen.Columns[9].Visible = status;
                    Grid_Oxygen.Columns[8].Visible = status;

                    Grid_Safety.Columns[9].Visible = status;
                    Grid_Safety.Columns[8].Visible = status;

                    Grid_Staff.Columns[9].Visible = status;
                    Grid_Staff.Columns[8].Visible = status;

                    Grid_Mess.Columns[9].Visible = status;
                    Grid_Mess.Columns[8].Visible = status;

                    Grid_Printing.Columns[9].Visible = status;
                    Grid_Printing.Columns[8].Visible = status;

                    Grid_Repairs.Columns[9].Visible = status;
                    Grid_Repairs.Columns[8].Visible = status;

                    Grid_BOQ.Columns[9].Visible = status;
                    Grid_BOQ.Columns[8].Visible = status;

                    Grid_Sanitary.Columns[9].Visible = status;
                    Grid_Sanitary.Columns[8].Visible = status;

                    Grid_Blasting.Columns[9].Visible = status;
                    Grid_Blasting.Columns[8].Visible = status;

                    Grid_Furnitures.Columns[9].Visible = status;
                    Grid_Furnitures.Columns[8].Visible = status;

                    Grid_Fixed_Assets.Columns[9].Visible = status;
                    Grid_Fixed_Assets.Columns[8].Visible = status;

                    Grid_Infrastructure.Columns[9].Visible = status;
                    Grid_Infrastructure.Columns[8].Visible = status;

                    Grid_Sand.Columns[9].Visible = status;
                    Grid_Sand.Columns[8].Visible = status;

                    Grid_Jelly.Columns[9].Visible = status;
                    Grid_Jelly.Columns[8].Visible = status;

                    Grid_RedSoil.Columns[9].Visible = status;
                    Grid_RedSoil.Columns[8].Visible = status;

                    Grid_Cement.Columns[9].Visible = status;
                    Grid_Cement.Columns[8].Visible = status;

                    Grid_Chemicals.Columns[9].Visible = status;
                    Grid_Chemicals.Columns[8].Visible = status;

                    Grid_Bricks.Columns[9].Visible = status;
                    Grid_Bricks.Columns[8].Visible = status;

                    Grid_Steels.Columns[9].Visible = status;
                    Grid_Steels.Columns[8].Visible = status;

                    Grid_Other_Construction.Columns[9].Visible = status;
                    Grid_Other_Construction.Columns[8].Visible = status;

                    Grid_Others.Columns[9].Visible = status;
                    Grid_Others.Columns[8].Visible = status;

                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
        
    }

    protected void GetBudgetDetails(string Abs_BID)
    {
        try
        {
            objBudgetBL = new BudgetBL();
            ds = new DataSet();
            objBudgetBL.Abs_BID = Convert.ToInt32(Abs_BID);
            objBudgetBL.load(con, BudgetBL.eLoadSp.SELECT_BY_ID, ref ds);

            if (ds.Tables[0].Rows.Count > 0)
            {


                ddlProject.SelectedValue = txtProjectcode.Text = ds.Tables[0].Rows[0]["Project_Code"].ToString();
                ddlMonth.SelectedValue = ds.Tables[0].Rows[0]["Month"].ToString();
                txtYear.Text = ds.Tables[0].Rows[0]["Year"].ToString();



                ddlReportingPerson.SelectedValue = ds.Tables[0].Rows[0]["Report_Person"].ToString();

                txtCreatedDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Creation_Date"]).ToString("dd-MM-yyyy");
                txtBudgetClosedDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Closing_date"]).ToString("dd-MM-yyyy");
                rd_Status.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                txtDescriptionBud.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                ddlPromaryPerson.SelectedValue = ds.Tables[0].Rows[0]["Primary_Person"].ToString();
                txtTotalValues.Text = ds.Tables[0].Rows[0]["Total_Amount"].ToString();
                txtBudgetID.Text = ds.Tables[0].Rows[0]["Budget_ID"].ToString();

                ddlProject.Enabled = false;
                ddlMonth.Enabled = false;
                btnSendForApproval.Visible = true;
                BtnPrint.Visible = true;

                btnSaveBudget.Text = "Update";
                btnCancelItem.Text = "Cancel";

                ViewState["Abs_BID"] = ds.Tables[0].Rows[0]["Abs_BID"].ToString();
                ItemListContainer.Visible = true;

                ViewState["BudgetID"] = ds.Tables[0].Rows[0]["Budget_ID"].ToString();

                BindBudgetItems();
                BindBudgetPurchageCost();

            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    protected void PopupFieldsClear()
    {

        txtDescription_r.Text = string.Empty;
        ddlCateGory_r.SelectedIndex = 0;
        ddlItem_r.SelectedIndex = 0;
        txtPartNo_r.Text = string.Empty;
        chkRecurring_r.Checked = false;
        ddlAssetType_r.SelectedIndex = 0;
        ddlAssetCate_r.SelectedIndex = 0;
        ddlAssetName_r.SelectedIndex = 0;
        ddlMaintainance_r.SelectedIndex = 0;
        txtStdard_r.Text = string.Empty;
        txtServiceDate_r.Text = string.Empty;
        ddlUnit_r.SelectedIndex = 0;
        txtRequiredQty_r.Text = string.Empty;
        txtRate_r.Text = string.Empty;
        txtValuesOdPurchase_r.Text = string.Empty;
        txtLocal_r.Text = string.Empty;
        txtHo_r.Text = string.Empty;



    }


    protected void FieldsClear()
    {


        ddlPromaryPerson.SelectedIndex = 0;
        txtCreatedDate.Text = string.Empty;
        txtBudgetClosedDate.Text = string.Empty;
        txtBudgetID.Text = string.Empty;
        txtTotalValues.Text = string.Empty;
        txtDescriptionBud.Text = string.Empty;
    }



    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlItem_r.Items.Clear();
            if (ddlCateGory_r.SelectedIndex != 0)
            {
                objIndent = new IndentBL();
                ds = new DataSet();
                objIndent.Mat_Cat_Id = Convert.ToUInt16(ddlCateGory_r.SelectedValue);
                objIndent.load(con, IndentBL.eLoadSp.SELECT_ITEMCODE_BY_CATEGORY_ID, ref ds);
                ddlItem_r.DataSource = ds;
                ddlItem_r.DataValueField = "Item_Code";
                ddlItem_r.DataTextField = "Item_Name";
                ddlItem_r.DataBind();
                ddlItem_r.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlItem_r.Items.Clear();
                ddlItem_r.Items.Insert(0, "-Select-");
                ddlUnit_r.SelectedValue = ddlUnit_r.Items.FindByText("-Select-").Value;
            }
            ModelPopupRecurringItem.Show();

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }


    protected void btnRecurringItemSave_Click(object sender, EventArgs e)
    {
        try
        {

            objBudgetBL = new BudgetBL();
            objBudgetBL.Description = txtDescription_r.Text;
            objBudgetBL.CategoryID = Convert.ToInt32(ddlCateGory_r.SelectedValue);
            objBudgetBL.ItemCode = ddlItem_r.SelectedValue;
            objBudgetBL.Part_No = txtPartNo_r.Text;
            objBudgetBL.Recurr_mat = chkRecurring_r.Checked;
            if (ddlAssetName_r.SelectedIndex != -1 && ddlAssetName_r.SelectedIndex != 0)
            {
                objBudgetBL.Asset_ID = ddlAssetName_r.SelectedValue;
            }
            objBudgetBL.UOM = Convert.ToInt32(ddlUnit_r.SelectedValue);
            objBudgetBL.Req_Qty = Convert.ToDecimal(txtRequiredQty_r.Text);
            objBudgetBL.Rate = Convert.ToDecimal(txtRate_r.Text);
            objBudgetBL.Purc_Value = Convert.ToDecimal(txtValuesOdPurchase_r.Text);

            if (txtLocal_r.Text != string.Empty)
            {
                objBudgetBL.Local = Convert.ToDecimal(txtLocal_r.Text);
            }
            else
            {
                objBudgetBL.Local = Convert.ToDecimal("0.00");
            }

            objBudgetBL.HO = Convert.ToDecimal(txtHo_r.Text);


            if (ViewState["TypeOfItems"] != null)
            {

                if (ViewState["TypeOfItems"].ToString() == "Auto")
                {
                    objBudgetBL.Bud_type = "Automobiles";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Plant")
                {
                    objBudgetBL.Bud_type = "Plant & Machinery";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Oil")
                {
                    objBudgetBL.Bud_type = "Petrol,Oil & Lubricants";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Shutter")
                {
                    objBudgetBL.Bud_type = "Shuttering Materials";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Consume")
                {
                    objBudgetBL.Bud_type = "Consumable Items";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Elect")
                {
                    objBudgetBL.Bud_type = "Electrical Items";
                }
                else if (ViewState["TypeOfItems"].ToString() == "HSD")
                {
                    objBudgetBL.Bud_type = "HSD";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Hardware")
                {
                    objBudgetBL.Bud_type = "Hardware Items";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Welding")
                {
                    objBudgetBL.Bud_type = "Welding Electrodes";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Oxygen")
                {
                    objBudgetBL.Bud_type = "Oxygen & Acetylene Gas";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Safety")
                {
                    objBudgetBL.Bud_type = "Safety Items";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Staff")
                {
                    objBudgetBL.Bud_type = "Staff Welfare";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Mess")
                {
                    objBudgetBL.Bud_type = "Mess Expenditure";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Printing")
                {
                    objBudgetBL.Bud_type = "Printing & Stationery";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Repairs")
                {
                    objBudgetBL.Bud_type = "Repairs & Maintenance";
                }
                else if (ViewState["TypeOfItems"].ToString() == "BOQ")
                {
                    objBudgetBL.Bud_type = "BOQ Items";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Sanitary")
                {
                    objBudgetBL.Bud_type = "Sanitary Materials";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Blasting")
                {
                    objBudgetBL.Bud_type = "Blasting Materials";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Furnitures")
                {
                    objBudgetBL.Bud_type = "Furnitures & Fixtures";
                }
                else if (ViewState["TypeOfItems"].ToString() == "FixedAssets")
                {
                    objBudgetBL.Bud_type = "Fixed Assets";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Infrastructure")
                {
                    objBudgetBL.Bud_type = "Infrastructure Items";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Sand")
                {
                    objBudgetBL.Bud_type = "Sand";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Jelly")
                {
                    objBudgetBL.Bud_type = "Jelly/Metal/Aggregates";
                }
                else if (ViewState["TypeOfItems"].ToString() == "RedSoil")
                {
                    objBudgetBL.Bud_type = "Red Soil";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Cement")
                {
                    objBudgetBL.Bud_type = "Cement";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Chemicals")
                {
                    objBudgetBL.Bud_type = "Chemicals";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Bricks")
                {
                    objBudgetBL.Bud_type = "Bricks";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Steels")
                {
                    objBudgetBL.Bud_type = "Steels";
                }
                else if (ViewState["TypeOfItems"].ToString() == "OtherConstruction")
                {
                    objBudgetBL.Bud_type = "Other Construction Materials";
                }
                else if (ViewState["TypeOfItems"].ToString() == "Others")
                {
                    objBudgetBL.Bud_type = "Others";
                }

            }
            else
            {
                objBudgetBL.Bud_type = "Automobiles";
            }


            objBudgetBL.Abs_BID = Convert.ToInt32(ViewState["Abs_BID"]);


            if (btnSaveItem.Text == "Save")
            {
                objBudgetBL.Task = "InsertBudgetItem";

                if (objBudgetBL.ItemInsert(con, BudgetBL.eLoadSp.INSERT_ITEM))
                {
                    ModelPopupRecurringItem.Hide();
                    ViewState["TypeOfItems"] = null;
                    BindBudgetItems();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Item has been added successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Item has been already exists in this budget');", true);

                }
            }
            else
            {
                objBudgetBL.Task = "UpdateBudgetItem";
                objBudgetBL.Bud_Item_Id = Convert.ToInt32(ViewState["BudgetItemID"]);

                if (objBudgetBL.ItemInsert(con, BudgetBL.eLoadSp.INSERT_ITEM))
                {

                    ViewState["TypeOfItems"] = null;
                    BindBudgetItems();
                    btnSaveItem.Text = "Save";
                    ItemFieldsEnable(true);
                    ModelPopupRecurringItem.Hide();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Item has been updated successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update budget Item!');", true);

                }
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }


  




    //NEW 
    protected void BindBudgetItems()
    {
        try
        {
            objBudgetBL = new BudgetBL();
            DataSet ds1 = new DataSet();
            bool exists;
            DataTable DatafilterDt = new DataTable();

            objBudgetBL.Abs_bud_ID = Convert.ToInt32(ViewState["Abs_BID"]);
            objBudgetBL.load(con, BudgetBL.eLoadSp.SELECT_BUDGETITEM_BY_ABS_BUDID, ref ds1);

            if (ds1.Tables[0].Rows.Count > 0)
            {
                #region budget sector bind

                	
			

                lblAutoApproved.Text = ds1.Tables[0].Rows[0]["Auto_amount_Budget1"].ToString();
                lblPMpproved.Text = ds1.Tables[0].Rows[0]["PlMach_Amount_Budget1"].ToString();
                lblSMApproved.Text = ds1.Tables[0].Rows[0]["Shutter_Mat_Amount_Budget1"].ToString();
                lblCIapproved.Text = ds1.Tables[0].Rows[0]["Consumable_Amount_Budget1"].ToString();
                lblEIapproved.Text = ds1.Tables[0].Rows[0]["Elec_Amoun_Budgett1"].ToString();
                lblHSDapproved.Text= ds1.Tables[0].Rows[0]["HSD_Pet_Amount_Budget1"].ToString();
                lblPOLapproved.Text = ds1.Tables[0].Rows[0]["Oil_Lube_Amount_Budget1"].ToString();
                lblHIapproved.Text = ds1.Tables[0].Rows[0]["Hardware_Amount_Budget1"].ToString();
                lblWEapproved.Text = ds1.Tables[0].Rows[0]["Weld_Elec_Amount_Budget1"].ToString();
                lblOAGapproved.Text = ds1.Tables[0].Rows[0]["Oxygen_ace_Amount_Budget1"].ToString();
                lblSIapproved.Text = ds1.Tables[0].Rows[0]["Safety_Item_Budget1"].ToString();
                lblSWapproved.Text = ds1.Tables[0].Rows[0]["Staff_wel_Amount_Budget1"].ToString();
               lblMEapproved.Text= ds1.Tables[0].Rows[0]["Mess_Expense_amount_Budget1"].ToString();
                lblPSapproved.Text = ds1.Tables[0].Rows[0]["Print_Sta_Amount_Budget1"].ToString();
               lblRMapproved.Text= ds1.Tables[0].Rows[0]["Repair_Maint_Amount_Budget1"].ToString();
               lblBIapproved.Text = ds1.Tables[0].Rows[0]["BOQ_Amount_Budget1"].ToString();
               lblSMaterialapproved.Text = ds1.Tables[0].Rows[0]["Sanitary_Amount_Budget1"].ToString();
               lblBMapproved.Text= ds1.Tables[0].Rows[0]["Blast_ma_Amount_Budget1"].ToString();
               lblFFapproved.Text = ds1.Tables[0].Rows[0]["FnF_Amount_Budget1"].ToString();
               lblFAapproved.Text = ds1.Tables[0].Rows[0]["Fix_Asset_Amount_Budget1"].ToString();
               lblIIapproved.Text = ds1.Tables[0].Rows[0]["Infra_Amount_Budget1"].ToString();
               lblSapproved.Text = ds1.Tables[0].Rows[0]["Sand_Amount_Budget1"].ToString();
               lblJMaapproved.Text = ds1.Tables[0].Rows[0]["Jelly_Metal_Amount_Budget1"].ToString();
               lblRSapproved.Text = ds1.Tables[0].Rows[0]["Red_Soil_Budget1"].ToString();
               lblcementApproved.Text = ds1.Tables[0].Rows[0]["Cement_Budget1"].ToString();
               lblchemicalApproved.Text = ds1.Tables[0].Rows[0]["Chem_Amount_Budget1"].ToString();
               lblBricksApproved.Text = ds1.Tables[0].Rows[0]["Brick_Amount_Budget1"].ToString();
               lblSteelsApproved.Text = ds1.Tables[0].Rows[0]["Steel_Amount_Budget1"].ToString();
               lblOCMapproved.Text = ds1.Tables[0].Rows[0]["Oth_Const_Amount_Budget1"].ToString();
               lblOthApproved.Text = ds1.Tables[0].Rows[0]["Other_Amount_Budget1"].ToString();

           
			


                #endregion


                txtTotalValues.Text = ds1.Tables[0].Rows[0]["BudTotalamt"].ToString();

                btnSendForApproval.Visible = true;
                DatafilterDt = ds1.Tables[0];


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Automobiles")).Count() > 0;
                if (exists)
                {
                    DataTable Automobilesdt = DatafilterDt.AsEnumerable()
                                 .Where(r => r.Field<string>("Bud_type") == "Automobiles")
                                 .CopyToDataTable();

                    AutomobilesList.DataSource = Automobilesdt;
                    AutomobilesList.DataBind();
                }
                else
                {
                    AutomobilesList.DataSource = null;
                    AutomobilesList.DataBind();
                    lblAutoTotal.Text = "0";
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Plant & Machinery")).Count() > 0;
                if (exists)
                {
                    DataTable Plantdt = DatafilterDt.AsEnumerable()
                                 .Where(r => r.Field<string>("Bud_type") == "Plant & Machinery")
                                 .CopyToDataTable();

                    GridPlant.DataSource = Plantdt;
                    GridPlant.DataBind();
                }
                else
                {
                    GridPlant.DataSource = null;
                    GridPlant.DataBind();
                    lblTotalValuesPlant.Text = "0";
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Shuttering Materials")).Count() > 0;
                if (exists)
                {
                    DataTable Sutterdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Shuttering Materials")
                                .CopyToDataTable();

                    GridShutter.DataSource = Sutterdt;
                    GridShutter.DataBind();
                }
                else
                {
                    GridShutter.DataSource = null;
                    GridShutter.DataBind();
                    lblShutteringTotal.Text = "0";
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Consumable Items")).Count() > 0;
                if (exists)
                {
                    DataTable Consumdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Consumable Items")
                                .CopyToDataTable();

                    GridConsume.DataSource = Consumdt;
                    GridConsume.DataBind();
                }
                else
                {
                    GridConsume.DataSource = null;
                    GridConsume.DataBind();
                    lblConsumeTotal.Text = "0";
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Electrical Items")).Count() > 0;
                if (exists)
                {
                    DataTable Electdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Electrical Items")
                                .CopyToDataTable();

                    Grid_Elect.DataSource = Electdt;
                    Grid_Elect.DataBind();
                }
                else
                {
                    Grid_Elect.DataSource = null;
                    Grid_Elect.DataBind();
                    lblTotalElect.Text = "0";
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("HSD")).Count() > 0;
                if (exists)
                {

                    DataTable HSDdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "HSD")
                                .CopyToDataTable();

                    Grid_HSD.DataSource = HSDdt;
                    Grid_HSD.DataBind();

                }
                else
                {
                    Grid_HSD.DataSource = null;
                    Grid_HSD.DataBind();
                    lblTotalHSD.Text = "0";
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Petrol,Oil & Lubricants")).Count() > 0;
                if (exists)
                {

                    DataTable Oildt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Petrol,Oil & Lubricants")
                                .CopyToDataTable();

                    Grid_Oil.DataSource = Oildt;
                    Grid_Oil.DataBind();

                }
                else
                {
                    Grid_Oil.DataSource = null;
                    Grid_Oil.DataBind();
                    lblTotalOil.Text = "0";
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Hardware Items")).Count() > 0;
                if (exists)
                {

                    DataTable Harddt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Hardware Items")
                                .CopyToDataTable();

                    Grid_Hardware.DataSource = Harddt;
                    Grid_Hardware.DataBind();
                }
                else
                {
                    Grid_Hardware.DataSource = null;
                    Grid_Hardware.DataBind();
                    lblTotalHardware.Text = "0";
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Welding Electrodes")).Count() > 0;
                if (exists)
                {
                    ////watch
                    DataTable Weldingdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Welding Electrodes")
                                .CopyToDataTable();

                    Grid_Welding.DataSource = Weldingdt;
                    Grid_Welding.DataBind();
                }
                else
                {
                    Grid_Welding.DataSource = null;
                    Grid_Welding.DataBind();
                    lblTotalWelding.Text = "0";
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Oxygen & Acetylene Gas")).Count() > 0;
                if (exists)
                {
                    ////watch
                    DataTable Oxygendt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Oxygen & Acetylene Gas")
                                .CopyToDataTable();

                    Grid_Oxygen.DataSource = Oxygendt;
                    Grid_Oxygen.DataBind();
                }
                else
                {
                    Grid_Oxygen.DataSource = null;
                    Grid_Oxygen.DataBind();
                    lblTotalOxygen.Text = "0";
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Safety Items")).Count() > 0;
                if (exists)
                {

                    DataTable Safetydt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Safety Items")
                                .CopyToDataTable();

                    Grid_Safety.DataSource = Safetydt;
                    Grid_Safety.DataBind();

                }
                else
                {
                    Grid_Safety.DataSource = null;
                    Grid_Safety.DataBind();
                    lblTotalSafety.Text = "0";
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Staff Welfare")).Count() > 0;
                if (exists)
                {

                    DataTable Staffdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Staff Welfare")
                                .CopyToDataTable();

                    Grid_Staff.DataSource = Staffdt;
                    Grid_Staff.DataBind();
                }
                else
                {
                    Grid_Staff.DataSource = null;
                    Grid_Staff.DataBind();
                    lblTotalStaff.Text = "0";
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Mess Expenditure")).Count() > 0;
                if (exists)
                {

                    DataTable Messdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Mess Expenditure")
                                .CopyToDataTable();

                    Grid_Mess.DataSource = Messdt;
                    Grid_Mess.DataBind();
                }
                else
                {
                    Grid_Mess.DataSource = null;
                    Grid_Mess.DataBind();
                    lblTotalMess.Text = "0";
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Printing & Stationery")).Count() > 0;
                if (exists)
                {

                    DataTable Printingdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Printing & Stationery")
                                .CopyToDataTable();

                    Grid_Printing.DataSource = Printingdt;
                    Grid_Printing.DataBind();
                }
                else
                {
                    Grid_Printing.DataSource = null;
                    Grid_Printing.DataBind();
                    lblTotalPrinting.Text = "0";
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Repairs & Maintenance")).Count() > 0;
                if (exists)
                {

                    DataTable Repairsdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Repairs & Maintenance")
                                .CopyToDataTable();

                    Grid_Repairs.DataSource = Repairsdt;
                    Grid_Repairs.DataBind();
                }
                else
                {
                    Grid_Repairs.DataSource = null;
                    Grid_Repairs.DataBind();
                    lblTotalRepairs.Text = "0";
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("BOQ Items")).Count() > 0;
                if (exists)
                {

                    DataTable BOQdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "BOQ Items")
                                .CopyToDataTable();

                    Grid_BOQ.DataSource = BOQdt;
                    Grid_BOQ.DataBind();
                }
                else
                {
                    Grid_BOQ.DataSource = null;
                    Grid_BOQ.DataBind();
                    lblTotalBoQ.Text = "0";
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Sanitary Materials")).Count() > 0;
                if (exists)
                {

                    DataTable Sanitarydt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Sanitary Materials")
                                .CopyToDataTable();

                    Grid_Sanitary.DataSource = Sanitarydt;
                    Grid_Sanitary.DataBind();
                }
                else
                {
                    Grid_Sanitary.DataSource = null;
                    Grid_Sanitary.DataBind();
                    lblTotalSanitary.Text = "0";
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Blasting Materials")).Count() > 0;
                if (exists)
                {

                    DataTable Blastingdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Blasting Materials")
                                .CopyToDataTable();

                    Grid_Blasting.DataSource = Blastingdt;
                    Grid_Blasting.DataBind();
                }
                else
                {
                    Grid_Blasting.DataSource = null;
                    Grid_Blasting.DataBind();
                    lblTotalBlasting.Text = "0";
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Furnitures & Fixtures")).Count() > 0;
                if (exists)
                {

                    DataTable Furnituresdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Furnitures & Fixtures")
                                .CopyToDataTable();

                    Grid_Furnitures.DataSource = Furnituresdt;
                    Grid_Furnitures.DataBind();
                }
                else
                {
                    Grid_Furnitures.DataSource = null;
                    Grid_Furnitures.DataBind();
                    lblTotalFurnitures.Text = "0";
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Fixed Assets")).Count() > 0;
                if (exists)
                {

                    DataTable Fixed_Assetsdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Fixed Assets")
                                .CopyToDataTable();

                    Grid_Fixed_Assets.DataSource = Fixed_Assetsdt;
                    Grid_Fixed_Assets.DataBind();
                }
                else
                {
                    Grid_Fixed_Assets.DataSource = null;
                    Grid_Fixed_Assets.DataBind();
                    lblTotalFixedAssets.Text = "0";
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Infrastructure Items")).Count() > 0;
                if (exists)
                {

                    DataTable Infrastructuredt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Infrastructure Items")
                                .CopyToDataTable();

                    Grid_Infrastructure.DataSource = Infrastructuredt;
                    Grid_Infrastructure.DataBind();
                }
                else
                {
                    Grid_Infrastructure.DataSource = null;
                    Grid_Infrastructure.DataBind();
                    lblTotalInfrastructure.Text = "0";
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Sand")).Count() > 0;
                if (exists)
                {

                    DataTable Sanddt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Sand")
                                .CopyToDataTable();

                    Grid_Sand.DataSource = Sanddt;
                    Grid_Sand.DataBind();
                }
                else
                {
                    Grid_Sand.DataSource = null;
                    Grid_Sand.DataBind();
                    lblTotalSand.Text = "0";
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Jelly/Metal/Aggregates")).Count() > 0;
                if (exists)
                {

                    DataTable Jellydt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Jelly/Metal/Aggregates")
                                .CopyToDataTable();

                    Grid_Jelly.DataSource = Jellydt;
                    Grid_Jelly.DataBind();
                }
                else
                {
                    Grid_Jelly.DataSource = null;
                    Grid_Jelly.DataBind();
                    lblTotalJelly.Text = "0";
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Red Soil")).Count() > 0;
                if (exists)
                {

                    DataTable RedSoildt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Red Soil")
                                .CopyToDataTable();

                    Grid_RedSoil.DataSource = RedSoildt;
                    Grid_RedSoil.DataBind();
                }
                else
                {
                    Grid_RedSoil.DataSource = null;
                    Grid_RedSoil.DataBind();
                    lblRedSoil.Text = "0";
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Cement")).Count() > 0;
                if (exists)
                {

                    DataTable Cementdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Cement")
                                .CopyToDataTable();

                    Grid_Cement.DataSource = Cementdt;
                    Grid_Cement.DataBind();
                }
                else
                {
                    Grid_Cement.DataSource = null;
                    Grid_Cement.DataBind();
                    lblTotalCement.Text = "0";
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Chemicals")).Count() > 0;
                if (exists)
                {

                    DataTable Chemicalsdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Chemicals")
                                .CopyToDataTable();

                    Grid_Chemicals.DataSource = Chemicalsdt;
                    Grid_Chemicals.DataBind();
                }
                else
                {
                    Grid_Chemicals.DataSource = null;
                    Grid_Chemicals.DataBind();
                    lblTotalChemicals.Text = "0";
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Bricks")).Count() > 0;
                if (exists)
                {

                    DataTable Bricksdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Bricks")
                                .CopyToDataTable();

                    Grid_Bricks.DataSource = Bricksdt;
                    Grid_Bricks.DataBind();
                }
                else
                {
                    Grid_Bricks.DataSource = null;
                    Grid_Bricks.DataBind();
                    lblTotalBricks.Text = "0";
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Steels")).Count() > 0;
                if (exists)
                {

                    DataTable Steelsdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Steels")
                                .CopyToDataTable();

                    Grid_Steels.DataSource = Steelsdt;
                    Grid_Steels.DataBind();
                }
                else
                {
                    Grid_Steels.DataSource = null;
                    Grid_Steels.DataBind();
                    lblTotalSteels.Text = "0";
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Other Construction Materials")).Count() > 0;
                if (exists)
                {

                    DataTable OtherConstructiondt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Other Construction Materials")
                                .CopyToDataTable();

                    Grid_Other_Construction.DataSource = OtherConstructiondt;
                    Grid_Other_Construction.DataBind();
                }
                else
                {
                    Grid_Other_Construction.DataSource = null;
                    Grid_Other_Construction.DataBind();
                    lblTotal_OtherConstruction.Text = "0";
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Others")).Count() > 0;
                if (exists)
                {
                    DataTable Othersdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Bud_type") == "Others")
                                .CopyToDataTable();

                    Grid_Others.DataSource = Othersdt;
                    Grid_Others.DataBind();
                }
                else
                {
                    Grid_Others.DataSource = null;
                    Grid_Others.DataBind();
                    lblTotalOthers.Text = "0";
                }
                exists = false;

            }
            else
            {
                btnSendForApproval.Visible = false;


                AutomobilesList.DataSource = null;
                AutomobilesList.DataBind();
                lblAutoTotal.Text = "0";

                GridPlant.DataSource = null;
                GridPlant.DataBind();
                lblTotalValuesPlant.Text = "0";

                GridShutter.DataSource = null;
                GridShutter.DataBind();
                lblShutteringTotal.Text = "0";

                GridConsume.DataSource = null;
                GridConsume.DataBind();
                lblConsumeTotal.Text = "0";

                Grid_Elect.DataSource = null;
                Grid_Elect.DataBind();
                lblTotalElect.Text = "0";

                Grid_HSD.DataSource = null;
                Grid_HSD.DataBind();
                lblTotalHSD.Text = "0";

                Grid_Oil.DataSource = null;
                Grid_Oil.DataBind();
                lblTotalOil.Text = "0";

                Grid_Hardware.DataSource = null;
                Grid_Hardware.DataBind();
                lblTotalHardware.Text = "0";

                Grid_Welding.DataSource = null;
                Grid_Welding.DataBind();
                lblTotalWelding.Text = "0";

                Grid_Oxygen.DataSource = null;
                Grid_Oxygen.DataBind();
                lblTotalOxygen.Text = "0";


                Grid_Safety.DataSource = null;
                Grid_Safety.DataBind();
                lblTotalSafety.Text = "0";

                Grid_Staff.DataSource = null;
                Grid_Staff.DataBind();
                lblTotalStaff.Text = "0";

                Grid_Mess.DataSource = null;
                Grid_Mess.DataBind();
                lblTotalMess.Text = "0";

                Grid_Printing.DataSource = null;
                Grid_Printing.DataBind();
                lblTotalPrinting.Text = "0";

                Grid_Repairs.DataSource = null;
                Grid_Repairs.DataBind();
                lblTotalRepairs.Text = "0";

                Grid_BOQ.DataSource = null;
                Grid_BOQ.DataBind();
                lblTotalBoQ.Text = "0";

                Grid_Sanitary.DataSource = null;
                Grid_Sanitary.DataBind();
                lblTotalSanitary.Text = "0";

                Grid_Blasting.DataSource = null;
                Grid_Blasting.DataBind();
                lblTotalBlasting.Text = "0";

                Grid_Furnitures.DataSource = null;
                Grid_Furnitures.DataBind();
                lblTotalFurnitures.Text = "0";

                Grid_Fixed_Assets.DataSource = null;
                Grid_Fixed_Assets.DataBind();
                lblTotalFixedAssets.Text = "0";

                Grid_Infrastructure.DataSource = null;
                Grid_Infrastructure.DataBind();
                lblTotalInfrastructure.Text = "0";

                Grid_Sand.DataSource = null;
                Grid_Sand.DataBind();
                lblTotalSand.Text = "0";

                Grid_Jelly.DataSource = null;
                Grid_Jelly.DataBind();
                lblTotalJelly.Text = "0";

                Grid_RedSoil.DataSource = null;
                Grid_RedSoil.DataBind();
                lblRedSoil.Text = "0";


                Grid_Cement.DataSource = null;
                Grid_Cement.DataBind();
                lblTotalCement.Text = "0";

                Grid_Chemicals.DataSource = null;
                Grid_Chemicals.DataBind();
                lblTotalChemicals.Text = "0";


                Grid_Bricks.DataSource = null;
                Grid_Bricks.DataBind();
                lblTotalBricks.Text = "0";

                Grid_Steels.DataSource = null;
                Grid_Steels.DataBind();
                lblTotalSteels.Text = "0";

                Grid_Other_Construction.DataSource = null;
                Grid_Other_Construction.DataBind();
                lblTotal_OtherConstruction.Text = "0";

                Grid_Others.DataSource = null;
                Grid_Others.DataBind();
                lblTotalOthers.Text = "0";
            }

        }
        catch (Exception ex)
        {
            btnSendForApproval.Visible = false;
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }



    protected void BindBudgetPurchageCost()
    {
        try
        {
            objBudgetBL = new BudgetBL();
            DataSet ds1 = new DataSet();
            bool exists;
            DataTable DatafilterDt = new DataTable();

            objBudgetBL.Budget_ID = txtBudgetID.Text;
            objBudgetBL.Project_Code = txtProjectcode.Text;
            objBudgetBL.load(con, BudgetBL.eLoadSp.Budget_Item_Project_Cost, ref ds1);

            if (ds1.Tables[0].Rows.Count > 0)
            {
               

               
                DatafilterDt = ds1.Tables[0];


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Automobiles")).Count() > 0;
                if (exists)
                {
                    decimal Automobilestotal =0;
                    DataTable Automobilesdt = DatafilterDt.AsEnumerable()
                                 .Where(r => r.Field<string>("Sector_Name") == "Automobiles")
                                 .CopyToDataTable();
                    for (int i = 0; i < Automobilesdt.Rows.Count; i++)
                    {
                        Automobilestotal += Convert.ToDecimal(Automobilesdt.Rows[i]["Total"]);
                    }
                    lbPOAutoMobiles.Text = Convert.ToString(Automobilestotal);
                        lblRemAutoMobiles.Text = Convert.ToString(Convert.ToDecimal(lblAutoApproved.Text) - Automobilestotal);
                  // lblPOAutoMobiles.Text=
                }
                else
                {

                    lbPOAutoMobiles.Text = "0";
                    lblRemAutoMobiles.Text = lblAutoApproved.Text;
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Plant & Machinery")).Count() > 0;
                if (exists)
                {
                    decimal PlantTotal = 0;
                    DataTable Plantdt = DatafilterDt.AsEnumerable()
                                 .Where(r => r.Field<string>("Sector_Name") == "Plant & Machinery")
                                 .CopyToDataTable();
                    for (int i = 0; i < Plantdt.Rows.Count; i++)
                    {
                        PlantTotal += Convert.ToDecimal(Plantdt.Rows[i]["Total"]);
                    }
                    lbPOPlantandMach.Text = Convert.ToString(PlantTotal);
                    lblRemPlantandMach.Text = Convert.ToString(Convert.ToDecimal(lblPMpproved.Text) - PlantTotal);
                }
                else
                {

                    lbPOPlantandMach.Text = "0";
                    lblRemPlantandMach.Text=lblPMpproved.Text;
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Shuttering Materials")).Count() > 0;
                if (exists)
                {
                    Decimal SutterTotal = 0;
                    DataTable Sutterdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Shuttering Materials")
                                .CopyToDataTable();
                    for (int i = 0; i < Sutterdt.Rows.Count; i++)
                    {
                        SutterTotal += Convert.ToDecimal(Sutterdt.Rows[i]["Total"]);
                    }
                    lbPOShutteringMaterial.Text = Convert.ToString(SutterTotal);
                    lblRemShutteringMaterial.Text = Convert.ToString(Convert.ToDecimal(lblSMApproved.Text) - SutterTotal);

                }
                else
                {

                    lbPOShutteringMaterial.Text = "0";
                    lblRemShutteringMaterial.Text = lblSMApproved.Text;
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Consumable Items")).Count() > 0;
                if (exists)
                {
                    Decimal ConsumTotal = 0;
                    DataTable Consumdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Consumable Items")
                                .CopyToDataTable();
                    for (int i = 0; i < Consumdt.Rows.Count; i++)
                    {
                        ConsumTotal += Convert.ToDecimal(Consumdt.Rows[i]["Total"]);
                    }
                    lbPOConsumable.Text = Convert.ToString(ConsumTotal);
                    lblRemConsumable.Text = Convert.ToString(Convert.ToDecimal(lblCIapproved.Text) - ConsumTotal);

                }
                else
                {

                    lbPOConsumable.Text = "0";
                    lblRemConsumable.Text = lblCIapproved.Text;
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Electrical Items")).Count() > 0;
                if (exists)
                {
                    Decimal ElectTotal = 0;
                    DataTable Electdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Electrical Items")
                                .CopyToDataTable();
                    for (int i = 0; i < Electdt.Rows.Count; i++)
                    {
                        ElectTotal += Convert.ToDecimal(Electdt.Rows[i]["Total"]);
                    }
                    lbPOElectricItem.Text = Convert.ToString(ElectTotal);
                    lblRemElectricItem.Text = Convert.ToString(Convert.ToDecimal(lblEIapproved.Text) - ElectTotal);

                }
                else
                {

                    lbPOElectricItem.Text = "0";
                    lblRemElectricItem.Text = lblEIapproved.Text;
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("HSD")).Count() > 0;
                if (exists)
                {
                    Decimal HSD = 0;
                    DataTable HSDdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "HSD")
                                .CopyToDataTable();
                    for (int i = 0; i < HSDdt.Rows.Count; i++)
                    {
                        HSD += Convert.ToDecimal(HSDdt.Rows[i]["Total"]);
                    }
                    lbPOHSD.Text = Convert.ToString(HSD);
                    lblRemHSD.Text = Convert.ToString(Convert.ToDecimal(lblHSDapproved.Text) - HSD);

                }
                else
                {

                    lbPOHSD.Text = "0";
                    lblRemHSD.Text = lblHSDapproved.Text;
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Petrol,Oil & Lubricants")).Count() > 0;
                if (exists)
                {
                    Decimal PetrolOil = 0;
                    DataTable Oildt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Petrol,Oil & Lubricants")
                                .CopyToDataTable();
                    for (int i = 0; i < Oildt.Rows.Count; i++)
                    {
                        PetrolOil += Convert.ToDecimal(Oildt.Rows[i]["Total"]);
                    }
                    lbPOPetrolOil.Text = Convert.ToString(PetrolOil);
                    lblRemPetrolOil.Text = Convert.ToString(Convert.ToDecimal(lblPOLapproved.Text) - PetrolOil);


                }
                else
                {

                    lbPOPetrolOil.Text = "0";
                    lblRemPetrolOil.Text = lblPOLapproved.Text;
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Hardware Items")).Count() > 0;
                if (exists)
                {
                    Decimal Hard = 0;
                    DataTable Harddt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Hardware Items")
                                .CopyToDataTable();
                    for (int i = 0; i < Harddt.Rows.Count; i++)
                    {
                        Hard += Convert.ToDecimal(Harddt.Rows[i]["Total"]);
                    }
                    lbPOHardwareItems.Text = Convert.ToString(Hard);
                    lblRemHI.Text = Convert.ToString(Convert.ToDecimal(lblHIapproved.Text) - Hard);

                }
                else
                {

                    lbPOHardwareItems.Text = "0";
                    lblRemHI.Text = lblHIapproved.Text;
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Welding Electrodes")).Count() > 0;
                if (exists)
                {
                    Decimal Welding = 0;
                    ////watch
                    DataTable Weldingdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Welding Electrodes")
                                .CopyToDataTable();
                    for (int i = 0; i < Weldingdt.Rows.Count; i++)
                    {
                        Welding += Convert.ToDecimal(Weldingdt.Rows[i]["Total"]);
                    }
                    lbPOWeldingElecrodes.Text = Convert.ToString(Welding);
                    lblRemWE.Text = Convert.ToString(Convert.ToDecimal(lblWEapproved.Text) - Welding);

                }
                else
                {

                    lbPOWeldingElecrodes.Text = "0";
                    lblRemWE.Text = lblWEapproved.Text;
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Oxygen & Acetylene Gas")).Count() > 0;
                if (exists)
                {
                    Decimal Oxygen = 0;
                    ////watch
                    DataTable Oxygendt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Oxygen & Acetylene Gas")
                                .CopyToDataTable();
                    for (int i = 0; i < Oxygendt.Rows.Count; i++)
                    {
                        Oxygen += Convert.ToDecimal(Oxygendt.Rows[i]["Total"]);
                    }
                    lbPOOxygen.Text = Convert.ToString(Oxygen);
                    lblRemOAG.Text = Convert.ToString(Convert.ToDecimal(lblOAGapproved.Text) - Oxygen);

                }
                else
                {

                    lbPOOxygen.Text = "0";
                    lblRemOAG.Text = lblOAGapproved.Text;
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Safety Items")).Count() > 0;
                if (exists)
                {
                    Decimal Safety = 0;
                    DataTable Safetydt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Safety Items")
                                .CopyToDataTable();
                    for (int i = 0; i < Safetydt.Rows.Count; i++)
                    {
                        Safety += Convert.ToDecimal(Safetydt.Rows[i]["Total"]);
                    }
                    lbPOSafty.Text = Convert.ToString(Safety);
                    lblRemSI.Text = Convert.ToString(Convert.ToDecimal(lblSIapproved.Text) - Safety);


                }
                else
                {

                    lbPOSafty.Text = "0";
                    lblRemSI.Text = lblSIapproved.Text;
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Staff Welfare")).Count() > 0;
                if (exists)
                {
                    decimal StaffWelfare = 0;
                    DataTable Staffdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Staff Welfare")
                                .CopyToDataTable();
                    for (int i = 0; i < Staffdt.Rows.Count; i++)
                    {
                        StaffWelfare += Convert.ToDecimal(Staffdt.Rows[i]["Total"]);
                    }
                    lbStaffWelfare.Text = Convert.ToString(StaffWelfare);
                    lblRemSW.Text = Convert.ToString(Convert.ToDecimal(lblSWapproved.Text) - StaffWelfare);


                }
                else
                {

                    lbStaffWelfare.Text = "0";
                    lblRemSW.Text = lblSWapproved.Text;
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Mess Expenditure")).Count() > 0;
                if (exists)
                {
                    decimal Mess = 0;
                    DataTable Messdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Mess Expenditure")
                                .CopyToDataTable();

                    for (int i = 0; i < Messdt.Rows.Count; i++)
                    {
                        Mess += Convert.ToDecimal(Messdt.Rows[i]["Total"]);
                    }
                    lbPOMessExpend.Text = Convert.ToString(Mess);
                    lblRemME.Text = Convert.ToString(Convert.ToDecimal(lblMEapproved.Text) - Mess);

                }
                else
                {

                    lbPOMessExpend.Text = "0";
                    lblRemME.Text = lblMEapproved.Text;
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Printing & Stationery")).Count() > 0;
                if (exists)
                {
                    Decimal Printing = 0;

                    DataTable Printingdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Printing & Stationery")
                                .CopyToDataTable();

                    for (int i = 0; i < Printingdt.Rows.Count; i++)
                    {
                        Printing += Convert.ToDecimal(Printingdt.Rows[i]["Total"]);
                    }
                    lbPOPrinting.Text = Convert.ToString(Printing);
                    lblRemPS.Text = Convert.ToString(Convert.ToDecimal(lblPSapproved.Text) - Printing);


                }
                else
                {

                    lbPOPrinting.Text = "0";
                    lblRemPS.Text = lblPSapproved.Text;
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Repairs & Maintenance")).Count() > 0;
                if (exists)
                {
                    Decimal Printing = 0;
                    DataTable Repairsdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Repairs & Maintenance")
                                .CopyToDataTable();
                    for (int i = 0; i < Repairsdt.Rows.Count; i++)
                    {
                        Printing += Convert.ToDecimal(Repairsdt.Rows[i]["Total"]);
                    }
                    lbPORepair.Text = Convert.ToString(Printing);
                    lblRemRM.Text = Convert.ToString(Convert.ToDecimal(lblRMapproved.Text) - Printing);


                }
                else
                {

                    lbPORepair.Text = "0";
                    lblRemRM.Text = lblRMapproved.Text;
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("BOQ Items")).Count() > 0;
                if (exists)
                {
                    Decimal BOQ = 0;
                    DataTable BOQdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "BOQ Items")
                                .CopyToDataTable();
                    for (int i = 0; i < BOQdt.Rows.Count; i++)
                    {
                        BOQ += Convert.ToDecimal(BOQdt.Rows[i]["Total"]);
                    }
                    lbPOBOQ.Text = Convert.ToString(BOQ);
                    lblRemBI.Text = Convert.ToString(Convert.ToDecimal(lblBIapproved.Text) - BOQ);


                }
                else
                {

                    lbPOBOQ.Text = "0";
                    lblRemBI.Text = lblBIapproved.Text;
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Sanitary Materials")).Count() > 0;
                if (exists)
                {
                    Decimal Sanitary = 0;

                    DataTable Sanitarydt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Sanitary Materials")
                                .CopyToDataTable();
                    for (int i = 0; i < Sanitarydt.Rows.Count; i++)
                    {
                        Sanitary += Convert.ToDecimal(Sanitarydt.Rows[i]["Total"]);
                    }
                    lbPOSanitary.Text = Convert.ToString(Sanitary);
                    lblRemSMaterial.Text = Convert.ToString(Convert.ToDecimal(lblSMaterialapproved.Text) - Sanitary);


                }
                else
                {

                    lbPOSanitary.Text = "0";
                    lblRemSMaterial.Text = lblSMaterialapproved.Text;
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Blasting Materials")).Count() > 0;
                if (exists)
                {
                    Decimal Blasting = 0;
                    DataTable Blastingdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Blasting Materials")
                                .CopyToDataTable();
                    for (int i = 0; i < Blastingdt.Rows.Count; i++)
                    {
                        Blasting += Convert.ToDecimal(Blastingdt.Rows[i]["Total"]);
                    }
                    lbPOBlasting.Text = Convert.ToString(Blasting);
                    lblRemBM.Text = Convert.ToString(Convert.ToDecimal(lblBMapproved.Text) - Blasting);


                }
                else
                {

                    lbPOBlasting.Text = "0";
                    lblRemBM.Text = lblBMapproved.Text;
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Furnitures & Fixtures")).Count() > 0;
                if (exists)
                {
                    Decimal Furnitures = 0;
                    DataTable Furnituresdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Furnitures & Fixtures")
                                .CopyToDataTable();
                    for (int i = 0; i < Furnituresdt.Rows.Count; i++)
                    {
                        Furnitures += Convert.ToDecimal(Furnituresdt.Rows[i]["Total"]);
                    }
                    lbPOFurnitures.Text = Convert.ToString(Furnitures);
                    lblRemFF.Text = Convert.ToString(Convert.ToDecimal(lblFFapproved.Text) - Furnitures);

                }
                else
                {

                    lbPOFurnitures.Text = "0";
                    lblRemFF.Text = lblFFapproved.Text;
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Fixed Assets")).Count() > 0;
                if (exists)
                {
                    Decimal Fixed_Assets = 0;

                    DataTable Fixed_Assetsdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Fixed Assets")
                                .CopyToDataTable();
                    for (int i = 0; i < Fixed_Assetsdt.Rows.Count; i++)
                    {
                        Fixed_Assets += Convert.ToDecimal(Fixed_Assetsdt.Rows[i]["Total"]);
                    }
                    lbPOFixed.Text = Convert.ToString(Fixed_Assets);
                    lblRemFA.Text = Convert.ToString(Convert.ToDecimal(lblFAapproved.Text) - Fixed_Assets);


                }
                else
                {

                    lbPOFixed.Text = "0";
                    lblRemFA.Text = lblFAapproved.Text;
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Infrastructure Items")).Count() > 0;
                if (exists)
                {
                    Decimal Infrastructure = 0;
                    
                    DataTable Infrastructuredt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Infrastructure Items")
                                .CopyToDataTable();
                    for (int i = 0; i < Infrastructuredt.Rows.Count; i++)
                    {
                        Infrastructure += Convert.ToDecimal(Infrastructuredt.Rows[i]["Total"]);
                    }
                    lbPOInfrastructure.Text = Convert.ToString(Infrastructure);
                    lblRemII.Text = Convert.ToString(Convert.ToDecimal(lblIIapproved.Text) - Infrastructure);


                }
                else
                {

                    lbPOInfrastructure.Text = "0";
                    lblRemII.Text = lblIIapproved.Text;
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Sand")).Count() > 0;
                if (exists)
                {
                    Decimal Sand = 0;

                    DataTable Sanddt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Sand")
                                .CopyToDataTable();
                    for (int i = 0; i < Sanddt.Rows.Count; i++)
                    {
                        Sand += Convert.ToDecimal(Sanddt.Rows[i]["Total"]);
                    }
                    lbPOSand.Text = Convert.ToString(Sand);
                    lblRemS.Text = Convert.ToString(Convert.ToDecimal(lblSapproved.Text) - Sand);


                }
                else
                {

                    lbPOSand.Text = "0";
                    lblRemS.Text = lblSapproved.Text;
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Jelly/Metal/Aggregates")).Count() > 0;
                if (exists)
                {
                    Decimal Jelly = 0;
                    DataTable Jellydt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Jelly/Metal/Aggregates")
                                .CopyToDataTable();
                    for (int i = 0; i < Jellydt.Rows.Count; i++)
                    {
                        Jelly += Convert.ToDecimal(Jellydt.Rows[i]["Total"]);
                    }
                    lbPOJelly.Text = Convert.ToString(Jelly);
                    lblRemJMa.Text = Convert.ToString(Convert.ToDecimal(lblJMaapproved.Text) - Jelly);


                }
                else
                {

                    lbPOJelly.Text = "0";
                    lblRemJMa.Text = lblJMaapproved.Text;
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Red Soil")).Count() > 0;
                if (exists)
                {
                    Decimal RedSoil = 0;
                    DataTable RedSoildt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Red Soil")
                                .CopyToDataTable();
                    for (int i = 0; i < RedSoildt.Rows.Count; i++)
                    {
                        RedSoil += Convert.ToDecimal(RedSoildt.Rows[i]["Total"]);
                    }
                    lbPORedSoil.Text = Convert.ToString(RedSoil);
                    lblRemRS.Text = Convert.ToString(Convert.ToDecimal(lblRSapproved.Text) - RedSoil);


                }
                else
                {

                    lbPORedSoil.Text = "0";
                    lblRemRS.Text = lblRSapproved.Text;
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Cement")).Count() > 0;
                if (exists)
                
                {

                    Decimal Cement = 0;


                    DataTable Cementdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Cement")
                                .CopyToDataTable();
                    for (int i = 0; i < Cementdt.Rows.Count; i++)
                    {
                        Cement += Convert.ToDecimal(Cementdt.Rows[i]["Total"]);
                    }
                    lbPOCement.Text = Convert.ToString(Cement);
                    lblRemCement.Text = Convert.ToString(Convert.ToDecimal(lblcementApproved.Text) - Cement);


                }
                else
                {

                    lbPOCement.Text = "0";
                    lblRemCement.Text = lblcementApproved.Text;
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Chemicals")).Count() > 0;
                if (exists)
                {
                    Decimal Chemicals = 0;

                    DataTable Chemicalsdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Chemicals")
                                .CopyToDataTable();
                    for (int i = 0; i < Chemicalsdt.Rows.Count; i++)
                    {
                        Chemicals += Convert.ToDecimal(Chemicalsdt.Rows[i]["Total"]);
                    }
                    lbPOChemicals.Text = Convert.ToString(Chemicals);
                    lblRemchemical.Text = Convert.ToString(Convert.ToDecimal(lblchemicalApproved.Text) - Chemicals);

                }
                else
                {

                    lbPOChemicals.Text = "0";
                    lblRemchemical.Text = lblchemicalApproved.Text;
                }
                exists = false;

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Bricks")).Count() > 0;
                if (exists)
                {
                    Decimal Bricks = 0;
                    DataTable Bricksdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Bricks")
                                .CopyToDataTable();
                    for (int i = 0; i < Bricksdt.Rows.Count; i++)
                    {
                        Bricks += Convert.ToDecimal(Bricksdt.Rows[i]["Total"]);
                    }
                    lbPOBricks.Text = Convert.ToString(Bricks);
                    lblRemBricks.Text = Convert.ToString(Convert.ToDecimal(lblBricksApproved.Text) - Bricks);


                }
                else
                {

                    lbPOBricks.Text = "0";
                    lblRemBricks.Text = lblBricksApproved.Text;
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Steels")).Count() > 0;
                if (exists)
                {
                    Decimal Steels = 0;
                    DataTable Steelsdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Steels")
                                .CopyToDataTable();
                    for (int i = 0; i < Steelsdt.Rows.Count; i++)
                    {
                        Steels += Convert.ToDecimal(Steelsdt.Rows[i]["Total"]);
                    }
                    lbPOSteels.Text = Convert.ToString(Steels);
                    lblRemSteels.Text = Convert.ToString(Convert.ToDecimal(lblSteelsApproved.Text) - Steels);


                }
                else
                {

                    lbPOSteels.Text = "0";
                    lblRemSteels.Text = lblSteelsApproved.Text;
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Other Construction Materials")).Count() > 0;
                if (exists)
                {
                    Decimal OtherConstruction = 0;
                    DataTable OtherConstructiondt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Other Construction Materials")
                                .CopyToDataTable();
                    for (int i = 0; i < OtherConstructiondt.Rows.Count; i++)
                    {
                        OtherConstruction += Convert.ToDecimal(OtherConstructiondt.Rows[i]["Total"]);
                    }
                    lbPOOther_Construction.Text = Convert.ToString(OtherConstruction);
                    lblRemOCM.Text = Convert.ToString(Convert.ToDecimal(lblOCMapproved.Text) - OtherConstruction);


                }
                else
                {

                    lbPOOther_Construction.Text = "0";
                    lblRemOCM.Text = lblOCMapproved.Text;
                }
                exists = false;


                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Others")).Count() > 0;
                if (exists)
                {
                    Decimal Others = 0;
                    DataTable Othersdt = DatafilterDt.AsEnumerable()
                                .Where(r => r.Field<string>("Sector_Name") == "Others")
                                .CopyToDataTable();
                    for (int i = 0; i < Othersdt.Rows.Count; i++)
                    {
                        Others += Convert.ToDecimal(Othersdt.Rows[i]["Total"]);
                    }
                    lbPOOthers.Text = Convert.ToString(Others);
                    lblRemOth.Text = Convert.ToString(Convert.ToDecimal(lblOthApproved.Text) - Others);
                }
                else
                {

                    lbPOOthers.Text = "0";
                    lblRemOth.Text = lblOthApproved.Text;
                }
                exists = false;

            }
            else
            {
               

            }

        }
        catch (Exception ex)
        {
            btnSendForApproval.Visible = false;
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    //clue 3
    private void BindCategoryBasedOnBudgetSector()
    {
        try
        {
            objCategory = new Category();
            DataSet dsCategory = new DataSet();

            bool exists;
            DataTable DatafilterDt = new DataTable();
            string sectorName;

            objCategory.load(con, Category.eLoadSp.SELECT_ALL, ref dsCategory);
            ddlItem_r.Items.Clear();
            ddlItem_r.Items.Insert(0, "-Select");
            if (dsCategory.Tables[0].Rows.Count > 0)
            {
                DatafilterDt = dsCategory.Tables[0];
                if (ViewState["TypeOfItems"].ToString() == "Auto")
                {
                    sectorName = "Automobiles";
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals(sectorName)).Count() > 0;
                    if (exists)
                    {
                        DataTable Automobilesdt = DatafilterDt.AsEnumerable()
                                     .Where(r => r.Field<string>("Sector_Name") == "Automobiles")
                                     .CopyToDataTable();

                        ddlCateGory_r.DataSource = Automobilesdt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;

                }
                else if (ViewState["TypeOfItems"].ToString() == "Plant")
                {
                    sectorName = "Plant & Machinery";
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals(sectorName)).Count() > 0;
                    if (exists)
                    {
                        DataTable Plantdt = DatafilterDt.AsEnumerable()
                                     .Where(r => r.Field<string>("Sector_Name") == "Plant & Machinery")
                                     .CopyToDataTable();

                        ddlCateGory_r.DataSource = Plantdt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }


                else if (ViewState["TypeOfItems"].ToString() == "Shutter")
                {
                    sectorName = "Shuttering Materials";

                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Shuttering Materials")).Count() > 0;
                    if (exists)
                    {
                        DataTable Sutterdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Shuttering Materials")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Sutterdt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Consume")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Consumable Items")).Count() > 0;
                    if (exists)
                    {
                        DataTable Consumdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Consumable Items")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Consumdt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Elect")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Electrical Items")).Count() > 0;
                    if (exists)
                    {
                        DataTable Electdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Electrical Items")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Electdt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "HSD")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("HSD")).Count() > 0;
                    if (exists)
                    {

                        DataTable HSDdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "HSD")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = HSDdt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }
                else if (ViewState["TypeOfItems"].ToString() == "Oil")
                {
                    sectorName = "Petrol,Oil & Lubricants";
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals(sectorName)).Count() > 0;
                    if (exists)
                    {
                        DataTable Oildt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Petrol,Oil & Lubricants")
                                    .CopyToDataTable();


                        ddlCateGory_r.DataSource = Oildt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }


                else if (ViewState["TypeOfItems"].ToString() == "Hardware")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Hardware Items")).Count() > 0;
                    if (exists)
                    {

                        DataTable Harddt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Hardware Items")
                                    .CopyToDataTable();


                        ddlCateGory_r.DataSource = Harddt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Welding")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Welding Electrodes")).Count() > 0;
                    if (exists)
                    {

                        DataTable Weldingdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Welding Electrodes")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Weldingdt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Oxygen")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Oxygen & Acetylene Gas")).Count() > 0;
                    if (exists)
                    {

                        DataTable Oxygendt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Oxygen & Acetylene Gas")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Oxygendt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Safety")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Safety Items")).Count() > 0;
                    if (exists)
                    {

                        DataTable Safetydt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Safety Items")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Safetydt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Staff")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Staff Welfare")).Count() > 0;
                    if (exists)
                    {

                        DataTable Staffdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Staff Welfare")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Staffdt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Mess")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Mess Expenditure")).Count() > 0;
                    if (exists)
                    {

                        DataTable Messdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Mess Expenditure")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Messdt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Printing")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Printing & Stationery")).Count() > 0;
                    if (exists)
                    {

                        DataTable Printingdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Printing & Stationery")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Printingdt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }
                else if (ViewState["TypeOfItems"].ToString() == "Repairs")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Repairs & Maintenance")).Count() > 0;
                    if (exists)
                    {

                        DataTable Repairsdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Repairs & Maintenance")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Repairsdt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "BOQ")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("BOQ Items")).Count() > 0;
                    if (exists)
                    {

                        DataTable BOQdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "BOQ Items")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = BOQdt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Sanitary")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Sanitary Materials")).Count() > 0;
                    if (exists)
                    {

                        DataTable Sanitarydt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Sanitary Materials")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Sanitarydt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Blasting")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Blasting Materials")).Count() > 0;
                    if (exists)
                    {

                        DataTable Blastingdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Blasting Materials")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Blastingdt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();


                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Furnitures")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Furnitures & Fixtures")).Count() > 0;
                    if (exists)
                    {

                        DataTable Furnituresdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Furnitures & Fixtures")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Furnituresdt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "FixedAssets")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Fixed Assets")).Count() > 0;
                    if (exists)
                    {

                        DataTable Fixed_Assetsdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Fixed Assets")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Fixed_Assetsdt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Infrastructure")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Infrastructure Items")).Count() > 0;
                    if (exists)
                    {

                        DataTable Infrastructuredt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Infrastructure Items")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Infrastructuredt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Sand")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Sand")).Count() > 0;
                    if (exists)
                    {

                        DataTable Sanddt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Sand")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Sanddt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }
                else if (ViewState["TypeOfItems"].ToString() == "Jelly")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Jelly/Metal/Aggregates")).Count() > 0;
                    if (exists)
                    {

                        DataTable Jellydt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Jelly/Metal/Aggregates")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Jellydt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "RedSoil")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Red Soil")).Count() > 0;
                    if (exists)
                    {

                        DataTable RedSoildt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Red Soil")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = RedSoildt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Cement")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Cement")).Count() > 0;
                    if (exists)
                    {

                        DataTable Cementdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Cement")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Cementdt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Chemicals")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Chemicals")).Count() > 0;
                    if (exists)
                    {

                        DataTable Chemicalsdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Chemicals")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Chemicalsdt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Bricks")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Bricks")).Count() > 0;
                    if (exists)
                    {

                        DataTable Bricksdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Bricks")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Bricksdt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Steels")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Steels")).Count() > 0;
                    if (exists)
                    {

                        DataTable Steelsdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Steels")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Steelsdt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "OtherConstruction")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Other Construction Materials")).Count() > 0;
                    if (exists)
                    {

                        DataTable OtherConstructiondt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Other Construction Materials")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = OtherConstructiondt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Others")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Others")).Count() > 0;
                    if (exists)
                    {
                        DataTable Othersdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Others")
                                    .CopyToDataTable();

                        ddlCateGory_r.DataSource = Othersdt;
                        ddlCateGory_r.DataTextField = "Category_Name";
                        ddlCateGory_r.DataValueField = "Mat_cat_ID";
                        ddlCateGory_r.DataBind();

                    }
                    else
                    {
                        ddlCateGory_r.Items.Clear();
                    }
                    exists = false;

                }
            }
            else
            {
                ddlCateGory_r.Items.Insert(0, "-Select-");
            }
            ddlCateGory_r.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }



    protected void btnItempopCancel_Click(object sender, EventArgs e)
    {
        trAssetType.Visible = true;
        trAsset.Visible = true;
        trRecurring.Visible = true;
        trRecurringCntr.Visible = true;

        PopupFieldsClear();
        ModelPopupRecurringItem.Hide();
    }

    protected void PopupItem_Auto_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ViewState["TypeOfItems"] = (((ImageButton)sender).ID).Split('_').Last();
            ModelPopupRecurringItem.Show();


            ItemFieldsEnable(true);

            BindCategoryBasedOnBudgetSector();

            if (ViewState["TypeOfItems"].ToString() == "Shutter" || ViewState["TypeOfItems"].ToString() == "Consume" || ViewState["TypeOfItems"].ToString() == "Elect" || ViewState["TypeOfItems"].ToString() == "HSD" || ViewState["TypeOfItems"].ToString() == "Hardware" || ViewState["TypeOfItems"].ToString() == "Welding" || ViewState["TypeOfItems"].ToString() == "Oxygen" || ViewState["TypeOfItems"].ToString() == "Safety" || ViewState["TypeOfItems"].ToString() == "Staff" || ViewState["TypeOfItems"].ToString() == "Mess" || ViewState["TypeOfItems"].ToString() == "Printing" || ViewState["TypeOfItems"].ToString() == "BOQ" || ViewState["TypeOfItems"].ToString() == "Sanitary" || ViewState["TypeOfItems"].ToString() == "Blasting" || ViewState["TypeOfItems"].ToString() == "Furnitures" || ViewState["TypeOfItems"].ToString() == "FixedAssets" || ViewState["TypeOfItems"].ToString() == "Infrastructure" || ViewState["TypeOfItems"].ToString() == "Sand" || ViewState["TypeOfItems"].ToString() == "Jelly" || ViewState["TypeOfItems"].ToString() == "RedSoil" || ViewState["TypeOfItems"].ToString() == "Cement" || ViewState["TypeOfItems"].ToString() == "Chemicals" || ViewState["TypeOfItems"].ToString() == "Bricks" || ViewState["TypeOfItems"].ToString() == "Steels" || ViewState["TypeOfItems"].ToString() == "OtherConstruction" || ViewState["TypeOfItems"].ToString() == "Others")
            {
                trAssetType.Visible = false;
                trAsset.Visible = false;
                trRecurring.Visible = false;
                trRecurringCntr.Visible = false;
                divrecurring.Visible = false;
                chkRecurring_r.Checked = false;
            }
            else
            {
                trAssetType.Visible = true;
                trAsset.Visible = true;
                trRecurring.Visible = true;
                trRecurringCntr.Visible = true;
            }
            PopupFieldsClear();

            btnSaveItem.Text = "Save";
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }
    #region

    decimal TotalPurchseValues = Convert.ToDecimal("0.00");
    decimal TotalHOValues = Convert.ToDecimal("0.00");
    decimal TotalLocalValues = Convert.ToDecimal("0.00");
    protected void AutomobilesList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValues = TotalPurchseValues + Convert.ToDecimal(lblPurchaseValues.Text);
                }

                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValues = TotalHOValues + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValues = TotalLocalValues + Convert.ToDecimal(lblLocal_Value.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblAutoTotal.Text = lblTotalPOV.Text = TotalPurchseValues.ToString();
                }

                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValues.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValues.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }



    decimal TotalPurchseValuesPlant = Convert.ToDecimal("0.00");
    decimal TotalHOValuesPlant = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesPlant = Convert.ToDecimal("0.00");
    protected void GridPlant_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesPlant = TotalPurchseValuesPlant + Convert.ToDecimal(lblPurchaseValues.Text);
                }


                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesPlant = TotalHOValuesPlant + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesPlant = TotalLocalValuesPlant + Convert.ToDecimal(lblLocal_Value.Text);
                }


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalValuesPlant.Text = lblTotalPOV.Text = TotalPurchseValuesPlant.ToString();
                }


                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesPlant.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesPlant.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    decimal TotalPurchseValuesOil = Convert.ToDecimal("0.00");
    protected void GridOil_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesOil = TotalPurchseValuesOil + Convert.ToDecimal(lblPurchaseValues.Text);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    //lblTotalValuesOil.Text = lblTotalPOV.Text = TotalPurchseValuesOil.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlUnit_r.Enabled = false;
            DataSet ds = new DataSet();
            objIndent = new IndentBL();
            if (ddlItem_r.SelectedIndex != 0)
            {
                objIndent.Item_Code = ddlItem_r.SelectedValue.Trim();
                objIndent.load(con, IndentBL.eLoadSp.SELECT_UOM_BY_ITEMCODE, ref ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlUnit_r.SelectedValue = ds.Tables[0].Rows[0]["UOMID"].ToString();
                }
                else
                {
                    ddlUnit_r.SelectedIndex = 0;
                }
            }
            else
            {
                ddlUnit_r.SelectedIndex = 0;
            }
            ModelPopupRecurringItem.Show();

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void chkRenewable_CheckedChanged(object sender, EventArgs e)
    {
        if (chkRecurring_r.Checked == true)
        {
            divrecurring.Visible = true;
        }
        else
        {
            divrecurring.Visible = false;
        }
        ModelPopupRecurringItem.Show();
    }
    /// <summary>

    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Printimage_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Request.QueryString["ID"] != null)
            {
                Response.Redirect("../Budget/Itemised_Budget.aspx?ID=" + Request.QueryString["ID"].ToString() + "&Type=" + ((ImageButton)sender).ID.ToString() + "&Proj=" + ddlProject.SelectedItem.Text + " - Code is : " + ddlProject.SelectedValue.ToString(), false);
            }
            else if (Request.QueryString["Abs_BID"] != null)
            {
                Response.Redirect("../Budget/Itemised_Budget.aspx?ID=" + Request.QueryString["Abs_BID"].ToString() + "&Type=" + ((ImageButton)sender).ID.ToString() + "&Proj=" + ddlProject.SelectedItem.Text + " - Code is " + ddlProject.SelectedValue.ToString(), false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProject.SelectedIndex != 0)
        {
            txtProjectcode.Text = ddlProject.SelectedValue;
        }
        else
        {
            txtProjectcode.Text = string.Empty;
        }
    }



    protected void btnSaveBudget_Click(object sender, EventArgs e)
    {
        try
        {
            objBudgetBL = new BudgetBL();
            objBudgetBL.Project_Code = txtProjectcode.Text;
            objBudgetBL.Month = Convert.ToInt32(ddlMonth.SelectedValue);
            objBudgetBL.Year = Convert.ToInt32(txtYear.Text);
            objBudgetBL.Report_Person = ddlReportingPerson.SelectedValue;

            objBudgetBL.Primary_Person = Convert.ToInt32(ddlPromaryPerson.SelectedValue);
            objBudgetBL.Creation_Date = Convert.ToDateTime(txtCreatedDate.Text);

            if (txtBudgetClosedDate.Text != String.Empty)
            {
                objBudgetBL.Closing_date = Convert.ToDateTime(txtBudgetClosedDate.Text);
            }

            objBudgetBL.Status = rd_Status.SelectedValue;
            objBudgetBL.Description = txtDescriptionBud.Text;



            if (((Button)sender).Text == "Save")
            {
                objBudgetBL.Approval_Status = "Draft";
                objBudgetBL.Approver_Comment = string.Empty;
                objBudgetBL.Rev_Status = "0.1";
                objBudgetBL.SenBack_Budget_ID = "";

                if (objBudgetBL.insert(con, BudgetBL.eLoadSp.INSERT))
                {
                    ItemListContainer.Visible = true;
                    BtnPrint.Visible = true;
                    btnSaveBudget.Text = "Update";
                    ddlProject.Enabled = false;
                    txtBudgetID.Text = objBudgetBL.Budget_ID;
                    ViewState["Abs_BID"] = objBudgetBL.Abs_BID;
                    //Response.Redirect("MonthlyBudget.aspx?ID=" + objBudgetBL.Abs_BID, false);
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Monthly Budget created successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('The Monthly Budget that you are trying to add already exists for the selected project.');", true);
                }
            }
            else
            {
                if (Request.QueryString["BudgetID"] == null)
                {
                    objBudgetBL.Abs_BID = Convert.ToInt32(ViewState["Abs_BID"]);
                    objBudgetBL.Task = "BugdetUpdate";
                    if (objBudgetBL.update(con, BudgetBL.eLoadSp.UPDATE))
                    {
                        ItemListContainer.Visible = true;
                        btnSendForApproval.Visible = true;
                        BtnPrint.Visible = true;                       
                        LoadBudgetDetailsByAbs_BID(ViewState["Abs_BID"].ToString(), true);

                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Monthly budget has been updated');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update the monthly budget');", true);
                    }
                }
                else
                {
                    objBudgetBL.Approval_Status = "Draft";
                    objBudgetBL.Approver_Comment = string.Empty;
                    objBudgetBL.Rev_Status = "0.1";
                    objBudgetBL.SenBack_Budget_ID = Convert.ToString(Request.QueryString["BudgetID"]);

                    objBudgetBL.SenBack_Abs_BID = Convert.ToInt32(ViewState["Abs_BID"]);

                    if (objBudgetBL.insert(con, BudgetBL.eLoadSp.INSERT))
                    {
                        btnSendForApproval.Visible = true;
                        BtnPrint.Visible = true;
                        btnSaveBudget.Text = "Update";

                        txtBudgetID.Text = objBudgetBL.Budget_ID;
                        ViewState["Abs_BID"] = objBudgetBL.Abs_BID;
                        ItemListContainer.Visible = true;                       
                        LoadBudgetDetailsByAbs_BID(ViewState["Abs_BID"].ToString(), true);

                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Monthly budget has been updated');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update the monthly budget');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    decimal TotalConsuPurchseValues = Convert.ToDecimal("0.00");
    decimal TotalHOValuesConsu = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesConsu = Convert.ToDecimal("0.00");
    protected void GridConsume_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalConsuPurchseValues = TotalConsuPurchseValues + Convert.ToDecimal(lblPurchaseValues.Text);
                }


                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesConsu = TotalHOValuesConsu + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesConsu = TotalLocalValuesConsu + Convert.ToDecimal(lblLocal_Value.Text);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblConsumeTotal.Text = lblTotalPOV.Text = TotalConsuPurchseValues.ToString();
                }

                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesConsu.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesConsu.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    decimal TotalShuttePurchseValues = Convert.ToDecimal("0.00");
    decimal TotalHOValuesShut = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesShut = Convert.ToDecimal("0.00");
    protected void GridShutter_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalShuttePurchseValues = TotalShuttePurchseValues + Convert.ToDecimal(lblPurchaseValues.Text);
                }


                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesShut = TotalHOValuesShut + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesShut = TotalLocalValuesShut + Convert.ToDecimal(lblLocal_Value.Text);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblShutteringTotal.Text = lblTotalPOV.Text = TotalShuttePurchseValues.ToString();
                }



                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesShut.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesShut.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    decimal TotalElectPurchseValues = Convert.ToDecimal("0.00");
    decimal TotalHOValuesElect = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesElect = Convert.ToDecimal("0.00");
    protected void Grid_Elect_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalElectPurchseValues = TotalElectPurchseValues + Convert.ToDecimal(lblPurchaseValues.Text);
                }

                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesElect = TotalHOValuesElect + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesElect = TotalLocalValuesElect + Convert.ToDecimal(lblLocal_Value.Text);
                }


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalElect.Text = lblTotalPOV.Text = TotalElectPurchseValues.ToString();
                }


                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesElect.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesElect.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    decimal TotalPurchseValuesHSD = Convert.ToDecimal("0.00");
    decimal TotalHOValuesHSD = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesHSD = Convert.ToDecimal("0.00");

    protected void Grid_HSD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesHSD = TotalPurchseValuesHSD + Convert.ToDecimal(lblPurchaseValues.Text);
                }

                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesHSD = TotalHOValuesHSD + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesHSD = TotalLocalValuesHSD + Convert.ToDecimal(lblLocal_Value.Text);
                }


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalHSD.Text = lblTotalPOV.Text = TotalPurchseValuesHSD.ToString();
                }

                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesHSD.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesHSD.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    decimal TotalPurchseValuesOils = Convert.ToDecimal("0.00");
    decimal TotalHOValuesPetrol = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesPetrol = Convert.ToDecimal("0.00");
    protected void Grid_Oil_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesOils = TotalPurchseValuesOils + Convert.ToDecimal(lblPurchaseValues.Text);
                }

                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesPetrol = TotalHOValuesPetrol + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesPetrol = TotalLocalValuesPetrol + Convert.ToDecimal(lblLocal_Value.Text);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalOil.Text = lblTotalPOV.Text = TotalPurchseValuesOils.ToString();
                }

                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesPetrol.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesPetrol.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    decimal TotalPurchseValuesHard = Convert.ToDecimal("0.00");
    decimal TotalHOValuesHardware = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesHardware = Convert.ToDecimal("0.00");

    protected void Grid_Hardware_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesHard = TotalPurchseValuesHard + Convert.ToDecimal(lblPurchaseValues.Text);
                }

                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesHardware = TotalHOValuesHardware + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesHardware = TotalLocalValuesHardware + Convert.ToDecimal(lblLocal_Value.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalHardware.Text = lblTotalPOV.Text = TotalPurchseValuesHard.ToString();
                }


                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesHardware.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesHardware.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    decimal TotalPurchseValuesWelding = Convert.ToDecimal("0.00");
    decimal TotalHOValuesWelding = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesWelding = Convert.ToDecimal("0.00");
    protected void Grid_Welding_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesWelding = TotalPurchseValuesWelding + Convert.ToDecimal(lblPurchaseValues.Text);
                }


                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesWelding = TotalHOValuesWelding + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesWelding = TotalLocalValuesWelding + Convert.ToDecimal(lblLocal_Value.Text);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalWelding.Text = lblTotalPOV.Text = TotalPurchseValuesWelding.ToString();
                }

                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesWelding.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesWelding.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    decimal TotalPurchseValuesOxygen = Convert.ToDecimal("0.00");
    decimal TotalHOValuesOxygen = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesOxygen = Convert.ToDecimal("0.00");
    protected void Grid_Oxygen_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesOxygen = TotalPurchseValuesOxygen + Convert.ToDecimal(lblPurchaseValues.Text);
                }

                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesOxygen = TotalHOValuesOxygen + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesOxygen = TotalLocalValuesOxygen + Convert.ToDecimal(lblLocal_Value.Text);
                }


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalOxygen.Text = lblTotalPOV.Text = TotalPurchseValuesOxygen.ToString();
                }


                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesOxygen.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesOxygen.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    decimal TotalPurchseValuesSafety = Convert.ToDecimal("0.00");
    decimal TotalHOValuesSafety = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesSafety = Convert.ToDecimal("0.00");
    protected void Grid_Safety_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesSafety = TotalPurchseValuesSafety + Convert.ToDecimal(lblPurchaseValues.Text);
                }



                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesSafety = TotalHOValuesSafety + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesSafety = TotalLocalValuesSafety + Convert.ToDecimal(lblLocal_Value.Text);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalSafety.Text = lblTotalPOV.Text = TotalPurchseValuesSafety.ToString();
                }

                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesSafety.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesSafety.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    decimal TotalPurchseValuesStaff = Convert.ToDecimal("0.00");
    decimal TotalHOValuesStaff = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesStaff = Convert.ToDecimal("0.00");
    protected void Grid_Staff_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesStaff = TotalPurchseValuesStaff + Convert.ToDecimal(lblPurchaseValues.Text);
                }

                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesStaff = TotalHOValuesStaff + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesStaff = TotalLocalValues + Convert.ToDecimal(lblLocal_Value.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalStaff.Text = lblTotalPOV.Text = TotalPurchseValuesStaff.ToString();
                }

                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesStaff.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesStaff.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    decimal TotalPurchseValuesMess = Convert.ToDecimal("0.00");
    decimal TotalHOValuesMess = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesMess = Convert.ToDecimal("0.00");
    protected void Grid_Mess_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesMess = TotalPurchseValuesMess + Convert.ToDecimal(lblPurchaseValues.Text);
                }
                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesMess = TotalHOValuesMess + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesMess = TotalLocalValuesMess + Convert.ToDecimal(lblLocal_Value.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalMess.Text = lblTotalPOV.Text = TotalPurchseValuesMess.ToString();
                }


                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesMess.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesMess.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    decimal TotalPurchseValuesPrinting = Convert.ToDecimal("0.00");
    decimal TotalHOValuesPrint = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesPrint = Convert.ToDecimal("0.00");
    protected void Grid_Printing_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesPrinting = TotalPurchseValuesPrinting + Convert.ToDecimal(lblPurchaseValues.Text);
                }

                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesPrint = TotalHOValuesPrint + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesPrint = TotalLocalValuesPrint + Convert.ToDecimal(lblLocal_Value.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalPrinting.Text = lblTotalPOV.Text = TotalPurchseValuesPrinting.ToString();
                }

                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesPrint.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesPrint.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    decimal TotalPurchseValuesRepairs = Convert.ToDecimal("0.00");
    decimal TotalHOValuesRepairs = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesRepairs = Convert.ToDecimal("0.00");
    protected void Grid_Repairs_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesRepairs = TotalPurchseValuesRepairs + Convert.ToDecimal(lblPurchaseValues.Text);
                }



                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesRepairs = TotalHOValuesRepairs + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesRepairs = TotalLocalValuesRepairs + Convert.ToDecimal(lblLocal_Value.Text);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalRepairs.Text = lblTotalPOV.Text = TotalPurchseValuesRepairs.ToString();
                }

                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesRepairs.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesRepairs.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    decimal TotalPurchseValuesBOQ = Convert.ToDecimal("0.00");
    decimal TotalHOValuesBOQ = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesBOQ = Convert.ToDecimal("0.00");
    protected void Grid_BOQ_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesBOQ = TotalPurchseValuesBOQ + Convert.ToDecimal(lblPurchaseValues.Text);
                }

                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesBOQ = TotalHOValuesBOQ + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesBOQ = TotalLocalValuesBOQ + Convert.ToDecimal(lblLocal_Value.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalBoQ.Text = lblTotalPOV.Text = TotalPurchseValuesBOQ.ToString();
                }

                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesBOQ.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesBOQ.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    decimal TotalPurchseValuesSanitary = Convert.ToDecimal("0.00");
    decimal TotalHOValuesSanitary = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesSanitary = Convert.ToDecimal("0.00");
    protected void Grid_Sanitary_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesSanitary = TotalPurchseValuesSanitary + Convert.ToDecimal(lblPurchaseValues.Text);
                }

                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesSanitary = TotalHOValuesSanitary + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesSanitary = TotalLocalValuesSanitary + Convert.ToDecimal(lblLocal_Value.Text);
                }


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalSanitary.Text = lblTotalPOV.Text = TotalPurchseValuesSanitary.ToString();
                }


                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesSanitary.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesSanitary.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    decimal TotalPurchseValuesBlasting = Convert.ToDecimal("0.00");
    decimal TotalHOValuesBlasting = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesBlasting = Convert.ToDecimal("0.00");
    protected void Grid_Blasting_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesBlasting = TotalPurchseValuesBlasting + Convert.ToDecimal(lblPurchaseValues.Text);
                }

                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesBlasting = TotalHOValuesBlasting + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesBlasting = TotalLocalValuesBlasting + Convert.ToDecimal(lblLocal_Value.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalBlasting.Text = lblTotalPOV.Text = TotalPurchseValuesBlasting.ToString();
                }
                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesBlasting.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesBlasting.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    decimal TotalPurchseValuesFurnitures = Convert.ToDecimal("0.00");
    decimal TotalHOValuesFurnit = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesFurnit = Convert.ToDecimal("0.00");
    protected void Grid_Furnitures_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesFurnitures = TotalPurchseValuesFurnitures + Convert.ToDecimal(lblPurchaseValues.Text);
                }
                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesFurnit = TotalHOValuesFurnit + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesFurnit = TotalLocalValuesFurnit + Convert.ToDecimal(lblLocal_Value.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalFurnitures.Text = lblTotalPOV.Text = TotalPurchseValuesFurnitures.ToString();
                }
                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesFurnit.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesFurnit.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    decimal TotalPurchseValuesFixedAssets = Convert.ToDecimal("0.00");
    decimal TotalHOValuesFixed = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesFixed = Convert.ToDecimal("0.00");
    protected void Grid_Fixed_Assets_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesFixedAssets = TotalPurchseValuesFixedAssets + Convert.ToDecimal(lblPurchaseValues.Text);
                }
                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesFixed = TotalHOValues + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesFixed = TotalLocalValues + Convert.ToDecimal(lblLocal_Value.Text);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalFixedAssets.Text = lblTotalPOV.Text = TotalPurchseValuesFixedAssets.ToString();
                }
                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesFixed.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesFixed.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    decimal TotalPurchseValuesInfrastructure = Convert.ToDecimal("0.00");
    decimal TotalHOValuesInfra = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesInfra = Convert.ToDecimal("0.00");
    protected void Grid_Infrastructure_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesInfrastructure = TotalPurchseValuesInfrastructure + Convert.ToDecimal(lblPurchaseValues.Text);
                }


                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesInfra = TotalHOValuesInfra + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesInfra = TotalLocalValuesInfra + Convert.ToDecimal(lblLocal_Value.Text);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalInfrastructure.Text = lblTotalPOV.Text = TotalPurchseValuesInfrastructure.ToString();
                }

                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesInfra.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesInfra.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    decimal TotalPurchseValuesSand = Convert.ToDecimal("0.00");
    decimal TotalHOValuesSand = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesSand = Convert.ToDecimal("0.00");
    protected void Grid_Sand_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesSand = TotalPurchseValuesSand + Convert.ToDecimal(lblPurchaseValues.Text);
                }

                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesSand = TotalHOValuesSand + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesSand = TotalLocalValuesSand + Convert.ToDecimal(lblLocal_Value.Text);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalSand.Text = lblTotalPOV.Text = TotalPurchseValuesSand.ToString();
                }
                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesSand.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesSand.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    decimal TotalPurchseValuesJelly = Convert.ToDecimal("0.00");
    decimal TotalHOValuesJelly = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesJelly = Convert.ToDecimal("0.00");
    protected void Grid_Jelly_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesJelly = TotalPurchseValuesJelly + Convert.ToDecimal(lblPurchaseValues.Text);
                }


                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesJelly = TotalHOValuesJelly + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesJelly = TotalLocalValuesJelly + Convert.ToDecimal(lblLocal_Value.Text);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalJelly.Text = lblTotalPOV.Text = TotalPurchseValuesJelly.ToString();
                }

                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesJelly.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesJelly.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    decimal TotalPurchseValuesRedSoil = Convert.ToDecimal("0.00");
    decimal TotalHOValuesRed = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesRed = Convert.ToDecimal("0.00");
    protected void Grid_RedSoil_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesRedSoil = TotalPurchseValuesRedSoil + Convert.ToDecimal(lblPurchaseValues.Text);
                }

                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesRed = TotalHOValuesRed + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesRed = TotalLocalValuesRed + Convert.ToDecimal(lblLocal_Value.Text);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblRedSoil.Text = lblTotalPOV.Text = TotalPurchseValuesRedSoil.ToString();
                }


                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesRed.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesRed.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    decimal TotalPurchseValuesCement = Convert.ToDecimal("0.00");
    decimal TotalHOValuesCement = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesCement = Convert.ToDecimal("0.00");
    protected void Grid_Cement_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesCement = TotalPurchseValuesCement + Convert.ToDecimal(lblPurchaseValues.Text);
                }

                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesCement = TotalHOValuesCement + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesCement = TotalLocalValuesCement + Convert.ToDecimal(lblLocal_Value.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalCement.Text = lblTotalPOV.Text = TotalPurchseValuesCement.ToString();
                }

                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesCement.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesCement.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    decimal TotalPurchseValuesChemicals = Convert.ToDecimal("0.00");
    decimal TotalHOValuesChemi = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesChemi = Convert.ToDecimal("0.00");
    protected void Grid_Chemicals_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesChemicals = TotalPurchseValuesChemicals + Convert.ToDecimal(lblPurchaseValues.Text);
                }

                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesChemi = TotalHOValues + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesChemi = TotalLocalValues + Convert.ToDecimal(lblLocal_Value.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalChemicals.Text = lblTotalPOV.Text = TotalPurchseValuesChemicals.ToString();
                }
                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesChemi.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesChemi.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    decimal TotalPurchseValuesBricks = Convert.ToDecimal("0.00");
    decimal TotalHOValuesBricks = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesBricks = Convert.ToDecimal("0.00");
    protected void Grid_Bricks_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesBricks = TotalPurchseValuesBricks + Convert.ToDecimal(lblPurchaseValues.Text);
                }


                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesBricks = TotalHOValuesBricks + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesBricks = TotalLocalValuesBricks + Convert.ToDecimal(lblLocal_Value.Text);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalBricks.Text = lblTotalPOV.Text = TotalPurchseValuesBricks.ToString();
                }

                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesBricks.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesBricks.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    decimal TotalPurchseValuesSteels = Convert.ToDecimal("0.00");
    decimal TotalHOValuesSteels = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesSteels = Convert.ToDecimal("0.00");
    protected void Grid_Steels_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesSteels = TotalPurchseValuesSteels + Convert.ToDecimal(lblPurchaseValues.Text);
                }
                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesSteels = TotalHOValuesSteels + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesSteels = TotalLocalValuesSteels + Convert.ToDecimal(lblLocal_Value.Text);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalSteels.Text = lblTotalPOV.Text = TotalPurchseValuesSteels.ToString();
                }

                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesSteels.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesSteels.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    decimal TotalPurchseValuesOtherConstruction = Convert.ToDecimal("0.00");
    decimal TotalHOValuesOtherC = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesOtherC = Convert.ToDecimal("0.00");
    protected void Grid_Other_Construction_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesOtherConstruction = TotalPurchseValuesOtherConstruction + Convert.ToDecimal(lblPurchaseValues.Text);
                }
                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesOtherC = TotalHOValuesOtherC + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesOtherC = TotalLocalValuesOtherC + Convert.ToDecimal(lblLocal_Value.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotal_OtherConstruction.Text = lblTotalPOV.Text = TotalPurchseValuesOtherConstruction.ToString();
                }


                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesOtherC.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesOtherC.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    decimal TotalPurchseValuesOthers = Convert.ToDecimal("0.00");
    decimal TotalHOValuesOthers = Convert.ToDecimal("0.00");
    decimal TotalLocalValuesOthers = Convert.ToDecimal("0.00");
    protected void Grid_Others_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
                if (lblPurchaseValues != null)
                {
                    TotalPurchseValuesOthers = TotalPurchseValuesOthers + Convert.ToDecimal(lblPurchaseValues.Text);
                }
                Label lblHO = (Label)e.Row.FindControl("lblHO");
                if (lblHO != null)
                {
                    TotalHOValuesOthers = TotalHOValuesOthers + Convert.ToDecimal(lblHO.Text);
                }

                Label lblLocal_Value = (Label)e.Row.FindControl("lblLocal_Value");
                if (lblLocal_Value != null)
                {
                    TotalLocalValuesOthers = TotalLocalValuesOthers + Convert.ToDecimal(lblLocal_Value.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
                if (lblTotalPOV != null)
                {
                    lblTotalOthers.Text = lblTotalPOV.Text = TotalPurchseValuesOthers.ToString();
                }


                Label lblTotalHO = (Label)e.Row.FindControl("lblTotalHO");
                if (lblTotalHO != null)
                {
                    lblTotalHO.Text = TotalHOValuesOthers.ToString();
                }

                Label lblTotalLocal_Value = (Label)e.Row.FindControl("lblTotalLocal_Value");
                if (lblTotalLocal_Value != null)
                {
                    lblTotalLocal_Value.Text = TotalLocalValuesOthers.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void btnSendForApproval_Click(object sender, EventArgs e)
    {
        try
        {
            objBudgetBL = new BudgetBL();
            objBudgetBL.Task = "SendApproval";
            objBudgetBL.Abs_BID = Convert.ToInt32(ViewState["Abs_BID"]);
            if (objBudgetBL.update(con, BudgetBL.eLoadSp.UPDATE))
            {
                btnSendForApproval.Visible = false;
                btnSaveBudget.Visible = false; 
                LoadBudgetDetailsByAbs_BID("", false);              

                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Monthly budget has been sent for approval');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to sent the monthly budget');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBUpdateError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (btnCancelItem.Text == "Cancel")
        {
            Response.Redirect("BudgetList.aspx", false);
        }
        else
        {
            FieldsClear();
        }

    }

    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["Abs_BID"] != null)
            {
                Response.Redirect("Grand_Abstract_Print.aspx?ID=" + Request.QueryString["Abs_BID"].ToString(), false);
            }
            else if (Request.QueryString["ID"] != null)
            {
                Response.Redirect("Grand_Abstract_Print.aspx?ID=" + Request.QueryString["ID"].ToString(), false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BudgetList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            objBudgetBL = new BudgetBL();
            LinkButton lnkDelete;
            int budgetItemID;

            if (e.CommandName == "DeleteBudgetItem")
            {
                lnkDelete = (LinkButton)e.CommandSource;
                string autoId = lnkDelete.CommandArgument;

                budgetItemID = Convert.ToInt32(e.CommandArgument);
                objBudgetBL.Bud_Item_Id = budgetItemID;

                if (objBudgetBL.delete(con, BudgetBL.eLoadSp.DELETE_BUDGETITEM_BY_ID))
                {
                    BindBudgetItems();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Budget Item has been deleted successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete Budget Item.!');", true);
                }

            }
            if (e.CommandName.Split('_').First() == "EditBudgetItem")
            {
                LinkButton likAutoEdit;

                likAutoEdit = (LinkButton)e.CommandSource;
                ViewState["BudgetItemID"] = e.CommandArgument;
                btnSaveItem.Text = "Update";

                ViewState["TypeOfItems"] = e.CommandName.Split('_').Last();
                ModelPopupRecurringItem.Show();

                BindCategoryBasedOnBudgetSector();

                if (ViewState["TypeOfItems"].ToString() == "Shutter" || ViewState["TypeOfItems"].ToString() == "Consume" || ViewState["TypeOfItems"].ToString() == "Elect" || ViewState["TypeOfItems"].ToString() == "HSD" || ViewState["TypeOfItems"].ToString() == "Hardware" || ViewState["TypeOfItems"].ToString() == "Welding" || ViewState["TypeOfItems"].ToString() == "Oxygen" || ViewState["TypeOfItems"].ToString() == "Safety" || ViewState["TypeOfItems"].ToString() == "Staff" || ViewState["TypeOfItems"].ToString() == "Mess" || ViewState["TypeOfItems"].ToString() == "Printing" || ViewState["TypeOfItems"].ToString() == "BOQ" || ViewState["TypeOfItems"].ToString() == "Sanitary" || ViewState["TypeOfItems"].ToString() == "Blasting" || ViewState["TypeOfItems"].ToString() == "Furnitures" || ViewState["TypeOfItems"].ToString() == "FixedAssets" || ViewState["TypeOfItems"].ToString() == "Infrastructure" || ViewState["TypeOfItems"].ToString() == "Sand" || ViewState["TypeOfItems"].ToString() == "Jelly" || ViewState["TypeOfItems"].ToString() == "RedSoil" || ViewState["TypeOfItems"].ToString() == "Cement" || ViewState["TypeOfItems"].ToString() == "Chemicals" || ViewState["TypeOfItems"].ToString() == "Bricks" || ViewState["TypeOfItems"].ToString() == "Steels" || ViewState["TypeOfItems"].ToString() == "OtherConstruction" || ViewState["TypeOfItems"].ToString() == "Others")
                {
                    trAssetType.Visible = false;
                    trAsset.Visible = false;
                    trRecurring.Visible = false;
                    trRecurringCntr.Visible = false;
                    divrecurring.Visible = false;
                    chkRecurring_r.Checked = false;
                }
                else
                {
                    trAssetType.Visible = true;
                    trAsset.Visible = true;
                    trRecurring.Visible = true;
                    trRecurringCntr.Visible = true;
                }

                ItemFieldsEnable(false);

                DataSet ds = new DataSet();

                BudgetBL ObjBudgetBL = new BudgetBL();
                ObjBudgetBL.Bud_Item_Id = Convert.ToInt32(likAutoEdit.CommandArgument);
                ObjBudgetBL.load(con, BudgetBL.eLoadSp.SELECT_BudgetId_By_ID, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtRequiredQty_r.Text = ds.Tables[0].Rows[0]["Req_Qty"].ToString();
                    txtRate_r.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
                    txtValuesOdPurchase_r.Text = ds.Tables[0].Rows[0]["Purc_Value"].ToString();
                    txtLocal_r.Text = ds.Tables[0].Rows[0]["Local_Value"].ToString();
                    txtHo_r.Text = ds.Tables[0].Rows[0]["HO_Value"].ToString();

                    ddlCateGory_r.SelectedValue = ds.Tables[0].Rows[0]["Mat_cat_ID"].ToString();
                    ddlCategory_SelectedIndexChanged(null, null);
                    ddlItem_r.SelectedValue = ds.Tables[0].Rows[0]["Item_Code"].ToString();
                    txtPartNo_r.Text = ds.Tables[0].Rows[0]["Part_No"].ToString();

                    //ddlAssetType_r.SelectedValue = ds.Tables[0].Rows[0]["Part_No"].ToString();

                    //ddlAssetCate_r.SelectedValue = ds.Tables[0].Rows[0]["Part_No"].ToString();

                    ddlAssetName_r.Items.Insert(0, ds.Tables[0].Rows[0]["Asset_ID"].ToString());

                    ddlAssetName_r.SelectedValue = ds.Tables[0].Rows[0]["Asset_ID"].ToString();

                    ddlUnit_r.SelectedValue = ds.Tables[0].Rows[0]["UOM"].ToString();

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Recurr_mat"].ToString()) && Convert.ToBoolean(ds.Tables[0].Rows[0]["Recurr_mat"].ToString()) == true)
                    {
                        chkRecurring_r.Checked = true;


                        ddlMaintainance_r.SelectedValue = ds.Tables[0].Rows[0]["Part_No"].ToString();

                        txtStdard_r.Text = ds.Tables[0].Rows[0]["Part_No"].ToString();

                        txtServiceDate_r.Text = ds.Tables[0].Rows[0]["Part_No"].ToString();

                    }
                    else
                    {
                        chkRecurring_r.Checked = false;
                    }
                    chkRenewable_CheckedChanged(null, null);
                }

            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ItemFieldsEnable(bool Status)
    {
        ddlCateGory_r.Enabled = Status;
        ddlItem_r.Enabled = Status;
        chkRecurring_r.Checked = Status;
        chkRenewable_CheckedChanged(null, null);

        //ddlMaintainance_r.Enabled = Status;
        //txtStdard_r.Enabled = Status;
        //txtServiceDate_r.Enabled = Status;
        ddlUnit_r.Enabled = Status;

    }

    protected void ddlAssetType_r_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAssetCategory();
        ModelPopupRecurringItem.Show();
    }

    protected void ddlAssetCate_r_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAssetNames();
        ModelPopupRecurringItem.Show();
    }

    protected void imgref_AutoNew_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ViewState["TypeOfItems"] = (((ImageButton)sender).ID).Split('_').Last();

            GridViewHorror.DataSource = null;
            GridViewHorror.DataBind();
            ModelPopupCSSItem.Show();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ddlExistingProjectName_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (ddlExistingProjectName.SelectedIndex > 0)
            {
                DataSet dsPK = new DataSet();
                objBudgetBL = new BudgetBL();
                objBudgetBL.Project_Code = ddlExistingProjectName.SelectedValue;
                objBudgetBL.load(con, BudgetBL.eLoadSp.SELECT_BUDGET_ID_BY_PROJECT_NAME, ref dsPK);

                ddlExistingMonth.DataSource = dsPK;
                ddlExistingMonth.DataValueField = "Abs_BID";
                ddlExistingMonth.DataTextField = "Budget_ID";
                ddlExistingMonth.DataBind();
                ddlExistingMonth.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlExistingMonth.Items.Clear();
                ddlExistingMonth.Items.Insert(0, "-Select-");

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnExistingBudgetCancel_Click(object sender, EventArgs e)
    {
        ModelPopupCSSItem.Hide();
    }

    #endregion
    //11
    protected void btnExistingBudgetSearch_Click(object sender, EventArgs e)
    {

        try
        {
            bool exists;
            string sectorName;
            DataTable DatafilterDt = new DataTable();
            objBudgetBL = new BudgetBL();
            ds = new DataSet();
            objBudgetBL.Project_Code = ddlExistingProjectName.SelectedValue;
            objBudgetBL.Abs_BID = Convert.ToInt32(ddlExistingMonth.SelectedValue);
            objBudgetBL.load(con, BudgetBL.eLoadSp.SELECT_GRID_BY_SEARCH_Click, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DatafilterDt = ds.Tables[0];


                if (ViewState["TypeOfItems"].ToString() == "AutoNew")
                {
                    sectorName = "Automobiles";
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals(sectorName)).Count() > 0;
                    if (exists)
                    {
                        DataTable Automobilesdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Automobiles")
                                    .CopyToDataTable();

                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Automobilesdt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        btn_ItemImport.Visible = false;
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }
                else if (ViewState["TypeOfItems"].ToString() == "PlantCSS")
                {
                    sectorName = "Plant & Machinery";
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals(sectorName)).Count() > 0;
                    if (exists)
                    {
                        DataTable Plantdt = DatafilterDt.AsEnumerable()
                                     .Where(r => r.Field<string>("Bud_type") == "Plant & Machinery")
                                     .CopyToDataTable();

                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Plantdt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }
                else if (ViewState["TypeOfItems"].ToString() == "shuttCSS")
                {
                    sectorName = "Shuttering Materials";

                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Shuttering Materials")).Count() > 0;
                    if (exists)
                    {
                        DataTable Sutterdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Shuttering Materials")
                                    .CopyToDataTable();

                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Sutterdt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;
                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Consu")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Consumable Items")).Count() > 0;
                    if (exists)
                    {
                        DataTable Consumdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Consumable Items")
                                    .CopyToDataTable();

                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Consumdt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Elect")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Electrical Items")).Count() > 0;
                    if (exists)
                    {
                        DataTable Electdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Electrical Items")
                                    .CopyToDataTable();

                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Electdt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }
                else if (ViewState["TypeOfItems"].ToString() == "HSD")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("HSD")).Count() > 0;
                    if (exists)
                    {

                        DataTable HSDdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "HSD")
                                    .CopyToDataTable();

                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = HSDdt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;
                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Oil")
                {
                    sectorName = "Petrol,Oil & Lubricants";
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals(sectorName)).Count() > 0;
                    if (exists)
                    {
                        DataTable Oildt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Petrol,Oil & Lubricants")
                                    .CopyToDataTable();

                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Oildt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }


                else if (ViewState["TypeOfItems"].ToString() == "Hard")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Hardware Items")).Count() > 0;
                    if (exists)
                    {

                        DataTable Harddt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Hardware Items")
                                    .CopyToDataTable();

                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Harddt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Wedd")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Welding Electrodes")).Count() > 0;
                    if (exists)
                    {

                        DataTable Weldingdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Welding Electrodes")
                                    .CopyToDataTable();

                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Weldingdt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Oxygen")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Oxygen & Acetylene Gas")).Count() > 0;
                    if (exists)
                    {

                        DataTable Oxygendt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Oxygen & Acetylene Gas")
                                    .CopyToDataTable();
                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Oxygendt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Safe")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Safety Items")).Count() > 0;
                    if (exists)
                    {

                        DataTable Safetydt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Safety Items")
                                    .CopyToDataTable();

                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Safetydt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Staff")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Staff Welfare")).Count() > 0;
                    if (exists)
                    {

                        DataTable Staffdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Staff Welfare")
                                    .CopyToDataTable();
                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Staffdt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Mess")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Mess Expenditure")).Count() > 0;
                    if (exists)
                    {

                        DataTable Messdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Mess Expenditure")
                                    .CopyToDataTable();

                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Messdt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Print")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Printing & Stationery")).Count() > 0;
                    if (exists)
                    {

                        DataTable Printingdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Printing & Stationery")
                                    .CopyToDataTable();
                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Printingdt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }
                else if (ViewState["TypeOfItems"].ToString() == "Repair")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Repairs & Maintenance")).Count() > 0;
                    if (exists)
                    {

                        DataTable Repairsdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Repairs & Maintenance")
                                    .CopyToDataTable();

                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Repairsdt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "BOQ")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("BOQ Items")).Count() > 0;
                    if (exists)
                    {

                        DataTable BOQdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "BOQ Items")
                                    .CopyToDataTable();


                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = BOQdt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Sanit")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Sanitary Materials")).Count() > 0;
                    if (exists)
                    {

                        DataTable Sanitarydt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Sanitary Materials")
                                    .CopyToDataTable();
                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Sanitarydt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;
                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Blast")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Blasting Materials")).Count() > 0;
                    if (exists)
                    {

                        DataTable Blastingdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Blasting Materials")
                                    .CopyToDataTable();

                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Blastingdt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Furn")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Furnitures & Fixtures")).Count() > 0;
                    if (exists)
                    {

                        DataTable Furnituresdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Furnitures & Fixtures")
                                    .CopyToDataTable();
                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Furnituresdt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Fixed")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Fixed Assets")).Count() > 0;
                    if (exists)
                    {

                        DataTable Fixed_Assetsdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Fixed Assets")
                                    .CopyToDataTable();

                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Fixed_Assetsdt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;
                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Infr")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Infrastructure Items")).Count() > 0;
                    if (exists)
                    {

                        DataTable Infrastructuredt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Infrastructure Items")
                                    .CopyToDataTable();
                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Infrastructuredt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Sand")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Sand")).Count() > 0;
                    if (exists)
                    {

                        DataTable Sanddt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Sand")
                                    .CopyToDataTable();

                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Sanddt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }
                else if (ViewState["TypeOfItems"].ToString() == "Jelly")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Jelly/Metal/Aggregates")).Count() > 0;
                    if (exists)
                    {

                        DataTable Jellydt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Jelly/Metal/Aggregates")
                                    .CopyToDataTable();

                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Jellydt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Red")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Red Soil")).Count() > 0;
                    if (exists)
                    {

                        DataTable RedSoildt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Red Soil")
                                    .CopyToDataTable();
                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = RedSoildt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Cement")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Cement")).Count() > 0;
                    if (exists)
                    {

                        DataTable Cementdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Bud_type") == "Cement")
                                    .CopyToDataTable();

                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Cementdt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Chemi")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Chemicals")).Count() > 0;
                    if (exists)
                    {

                        DataTable Chemicalsdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Chemicals")
                                    .CopyToDataTable();

                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Chemicalsdt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Bricks")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Bricks")).Count() > 0;
                    if (exists)
                    {

                        DataTable Bricksdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Bricks")
                                    .CopyToDataTable();

                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Bricksdt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "steel")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Steels")).Count() > 0;
                    if (exists)
                    {

                        DataTable Steelsdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Steels")
                                    .CopyToDataTable();
                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Steelsdt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "OtherCons")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Other Construction Materials")).Count() > 0;
                    if (exists)
                    {

                        DataTable OtherConstructiondt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Other Construction Materials")
                                    .CopyToDataTable();
                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = OtherConstructiondt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;
                }

                else if (ViewState["TypeOfItems"].ToString() == "Others")
                {
                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("Others")).Count() > 0;
                    if (exists)
                    {
                        DataTable Othersdt = DatafilterDt.AsEnumerable()
                                    .Where(r => r.Field<string>("Sector_Name") == "Others")
                                    .CopyToDataTable();

                        GridViewHorror.Visible = true;
                        GridViewHorror.DataSource = Othersdt;
                        GridViewHorror.DataBind();
                        ModelPopupCSSItem.Show();
                        btn_ItemImport.Visible = true;

                    }
                    else
                    {
                        GridViewHorror.DataSource = null;
                        GridViewHorror.DataBind();
                        btn_ItemImport.Visible = false;
                    }
                    exists = false;

                }
            }
            else
            {
                GridViewHorror.DataSource = null;
                GridViewHorror.DataBind();
                btn_ItemImport.Visible = false;
            }


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }



    }

    protected void btn_ItemImport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["ID"] != null)
            {
                objBudgetBL = new BudgetBL();

                objBudgetBL.CurrentAbs_BID = Convert.ToInt32(Request.QueryString["ID"]);
                objBudgetBL.Project_Code = ddlExistingProjectName.SelectedValue;
                objBudgetBL.Abs_BID = Convert.ToInt32(ddlExistingMonth.SelectedValue);


                if (ViewState["TypeOfItems"] != null)
                {

                    if (ViewState["TypeOfItems"].ToString() == "AutoNew")
                    {
                        objBudgetBL.Bud_type = "Automobiles";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "PlantCSS")
                    {
                        objBudgetBL.Bud_type = "Plant & Machinery";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Oil")
                    {
                        objBudgetBL.Bud_type = "Petrol,Oil & Lubricants";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "shuttCSS")
                    {
                        objBudgetBL.Bud_type = "Shuttering Materials";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Consu")
                    {
                        objBudgetBL.Bud_type = "Consumable Items";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Elect")
                    {
                        objBudgetBL.Bud_type = "Electrical Items";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "HSD")
                    {
                        objBudgetBL.Bud_type = "HSD";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Hard")
                    {
                        objBudgetBL.Bud_type = "Hardware Items";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Wedd")
                    {
                        objBudgetBL.Bud_type = "Welding Electrodes";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Oxygen")
                    {
                        objBudgetBL.Bud_type = "Oxygen & Acetylene Gas";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Safe")
                    {
                        objBudgetBL.Bud_type = "Safety Items";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Staff")
                    {
                        objBudgetBL.Bud_type = "Staff Welfare";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Mess")
                    {
                        objBudgetBL.Bud_type = "Mess Expenditure";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Print")
                    {
                        objBudgetBL.Bud_type = "Printing & Stationery";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Repair")
                    {
                        objBudgetBL.Bud_type = "Repairs & Maintenance";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "BOQ")
                    {
                        objBudgetBL.Bud_type = "BOQ Items";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Sanit")
                    {
                        objBudgetBL.Bud_type = "Sanitary Materials";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Blast")
                    {
                        objBudgetBL.Bud_type = "Blasting Materials";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Furn")
                    {
                        objBudgetBL.Bud_type = "Furnitures & Fixtures";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Fixed")
                    {
                        objBudgetBL.Bud_type = "Fixed Assets";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Infr")
                    {
                        objBudgetBL.Bud_type = "Infrastructure Items";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Sand")
                    {
                        objBudgetBL.Bud_type = "Sand";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Jelly")
                    {
                        objBudgetBL.Bud_type = "Jelly/Metal/Aggregates";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Red")
                    {
                        objBudgetBL.Bud_type = "Red Soil";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Cement")
                    {
                        objBudgetBL.Bud_type = "Cement";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Chemi")
                    {
                        objBudgetBL.Bud_type = "Chemicals";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Bricks")
                    {
                        objBudgetBL.Bud_type = "Bricks";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "steel")
                    {
                        objBudgetBL.Bud_type = "Steels";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "OtherCons")
                    {
                        objBudgetBL.Bud_type = "Other Construction Materials";
                    }
                    else if (ViewState["TypeOfItems"].ToString() == "Others")
                    {
                        objBudgetBL.Bud_type = "Others";
                    }

                }
                else
                {
                    objBudgetBL.Bud_type = "Automobiles";
                }


                if (objBudgetBL.ItemImport(con, BudgetBL.eLoadSp.BudgetItemImport))
                {
                    ModelPopupCSSItem.Hide();
                    ViewState["TypeOfItems"] = null;
                    BindBudgetItems();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Item has been added successfully');", true);
                }
                else
                {
                    ModelPopupCSSItem.Hide();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to  import the data');", true);

                }

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void img_MaterialRefer_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string FiredEvent = (((ImageButton)sender).ID).Split('_').Last();

            if (FiredEvent == "AutoNew")
            {
                ddlBudgetSector.SelectedValue = "1";
                ddlBudgetSector.Enabled = false;
            }
            else if (FiredEvent == "PlantCSS")
            {
                ddlBudgetSector.SelectedValue = "2";
                ddlBudgetSector.Enabled = false;
            }
            else if (FiredEvent == "shuttCSS")
            {
                ddlBudgetSector.SelectedValue = "3";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "Consu")
            {
                ddlBudgetSector.SelectedValue = "4";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "Elect")
            {
                ddlBudgetSector.SelectedValue = "5";
                ddlBudgetSector.Enabled = false;
            }
            else if (FiredEvent == "HSD")
            {
                ddlBudgetSector.SelectedValue = "6";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "Oil")
            {
                ddlBudgetSector.SelectedValue = "7";
                ddlBudgetSector.Enabled = false;
            }
            else if (FiredEvent == "Hard")
            {
                ddlBudgetSector.SelectedValue = "8";
                ddlBudgetSector.Enabled = false;
            }
            else if (FiredEvent == "Wedd")
            {
                ddlBudgetSector.SelectedValue = "9";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "Oxygen")
            {
                ddlBudgetSector.SelectedValue = "10";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "Safe")
            {
                ddlBudgetSector.SelectedValue = "11";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "Staff")
            {
                ddlBudgetSector.SelectedValue = "12";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "Mess")
            {
                ddlBudgetSector.SelectedValue = "13";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "Print")
            {
                ddlBudgetSector.SelectedValue = "14";
                ddlBudgetSector.Enabled = false;
            }
            else if (FiredEvent == "Repair")
            {
                ddlBudgetSector.SelectedValue = "15";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "BOQ")
            {
                ddlBudgetSector.SelectedValue = "16";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "Sanit")
            {
                ddlBudgetSector.SelectedValue = "17";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "Blast")
            {
                ddlBudgetSector.SelectedValue = "18";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "Furn")
            {
                ddlBudgetSector.SelectedValue = "19";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "Fixed")
            {
                ddlBudgetSector.SelectedValue = "20";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "Infr")
            {
                ddlBudgetSector.SelectedValue = "21";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "Sand")
            {
                ddlBudgetSector.SelectedValue = "22";
                ddlBudgetSector.Enabled = false;
            }
            else if (FiredEvent == "Jelly")
            {
                ddlBudgetSector.SelectedValue = "23";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "Red")
            {
                ddlBudgetSector.SelectedValue = "24";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "Cement")
            {
                ddlBudgetSector.SelectedValue = "25";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "Chemi")
            {
                ddlBudgetSector.SelectedValue = "26";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "Bricks")
            {
                ddlBudgetSector.SelectedValue = "27";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "steel")
            {
                ddlBudgetSector.SelectedValue = "28";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "OtherCons")
            {
                ddlBudgetSector.SelectedValue = "29";
                ddlBudgetSector.Enabled = false;
            }

            else if (FiredEvent == "Others")
            {
                ddlBudgetSector.SelectedValue = "30";
                ddlBudgetSector.Enabled = false;

            }
            ddlBudgetSector_SelectedIndexChanged(null, null);
            ModelReferMaterial.Show();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }









    private void BindCategoryDetailsForPopUp()
    {

        try
        {
            objCategory = new Category();
            ds = new DataSet();
            DataTable DatafilterDt;
            DataTable dt;

            bool exists;
            int BudgetSectorID;
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



            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void ddlBudgetSector_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            RefMaterialGridItems.DataSource = null;
            RefMaterialGridItems.DataBind();
            btnImportPOPUP.Visible = false;
            BindCategoryDetailsForPopUp();
            ModelReferMaterial.Show();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSearchPop_Click(object sender, EventArgs e)
    {
        try
        {
            objMaterial = new MaterialBL();
            DataSet dsnew = new DataSet();
            objMaterial.Budget_Sector_ID = Convert.ToInt32(ddlBudgetSector.SelectedValue);
            if (ddlCategory.SelectedIndex != 0)
            {
                objMaterial.Cat_ID = Convert.ToInt32(ddlCategory.SelectedValue);
            }
            else
            {
                objMaterial.Cat_ID = 0;
            }
            objMaterial.load(con, MaterialBL.eLoadSp.SELECT_ITEMS_BY_SEARCH, ref dsnew);

            RefMaterialGridItems.DataSource = dsnew;
            RefMaterialGridItems.DataBind();

            if (dsnew.Tables[0].Rows.Count > 0)
            {
                btnImportPOPUP.Visible = true;
            }
            else
            {
                btnImportPOPUP.Visible = false;
            }

            ModelReferMaterial.Show();

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }



    protected void btnCancelPop_Click(object sender, EventArgs e)
    {
        ddlBudgetSector.SelectedIndex = -1;
        ddlCategory.Items.Clear();
        ddlCategory.Items.Insert(0, "-Select");
        RefMaterialGridItems.DataSource = null;
        RefMaterialGridItems.DataBind();
        btnImportPOPUP.Visible = false;
    }



    protected void btnImportPOPUP_Click(object sender, EventArgs e)
    {
        try
        {
            objBudgetBL = new BudgetBL();
            objMaterial = new MaterialBL();
            DataSet ds = new DataSet();

            if (RefMaterialGridItems.SelectedRecords != null)
            {
                foreach (Hashtable ht in RefMaterialGridItems.SelectedRecords)
                {
                    objBudgetBL.ItemCode = ht["Item_Code"].ToString();
                    objBudgetBL.Abs_bud_ID = Convert.ToInt32(ViewState["Abs_BID"]);
                    objBudgetBL.Bud_type = ddlBudgetSector.SelectedValue;
                    objBudgetBL.Task = "InsertBudgetItemPOPUP";

                    objBudgetBL.ItemInsertPOPUP(con, BudgetBL.eLoadSp.INSERT_FROM_POPUP);
                    BindBudgetItems();
                }
            }
            else
            {
                ModelReferMaterial.Show();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select any one Item');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void lbPOAutoMobiles_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        string type = btn.CommandArgument;

        //string redirect = "<script>window.open('../Procurement/PurchaseOrderDetails.aspx?BudgetID='+ txtBudgetID.Text + ' &Pcode = ' + txtProjectcode.Text + ' &Type =' + type);</script>";
        //Response.Write(redirect);
        Response.Redirect("../Procurement/PurchaseOrderDetails.aspx?BudgetID=" + txtBudgetID.Text + "&Pcode=" + txtProjectcode.Text + "&Type=" + type + "&Abs_BID=" + Request.QueryString["Abs_BID"].ToString() + "&MFYRQ="+Request.QueryString["MFYRQ"].ToString(), false);
    }
}

   
