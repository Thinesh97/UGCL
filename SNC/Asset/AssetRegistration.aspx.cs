using SNC.ErrorLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;

public partial class AssetRegistration : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    AssetRegistrationBL objAsset = null;
    ProjectBL objProjectBL = null;
    LocationBL objLocationBL = null;
    SubContractorBL objSubContractorBL = null;
    VendorBL objVendorBL = null;
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    rblowner_SelectedIndexChanged(sender, e);

                    BindAssetType();
                    BindUOMDetails();
                    BindAssetCategory();
                    BindVendor();
                    BindProjectNames();
                    if (Session["Project_Code"] != null)
                    {
                        ddlPresentProjectName.SelectedValue = Session["Project_Code"].ToString();
                    }
                    ddlPresentProjectName.Enabled = false;
                    ddlPresentProjectName_SelectedIndexChanged(null, null);
                    BindLocation();
                    BindContractList();
                    if (Request.QueryString["AssetCode"] != null)
                    {
                        GetAssetRegistrationDetails();
                    }
                    if (Request.QueryString["ID"] != null)
                    {
                        GetAssetRegistrationDetails();
                    }
                }
                else
                {
                    Response.Redirect("../CommonPages/Login.aspx", false);
                }               
            }
            else
            {
                if (Page.Request.Params.Get("__EVENTTARGET").Contains("Grid_AssetType"))
                {
                    mpeAssetType.Show();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void BindUOMDetails()
    {
        try
        {
            UOM objUOM = new UOM();
            DataSet ds = new DataSet();
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
    
    protected void GetAssetRegistrationDetails()
    {
        try
        {
            objAsset = new AssetRegistrationBL();
            ds = new DataSet();

            if (Request.QueryString["AssetCode"] != null)
            {
                objAsset.Code = Request.QueryString["AssetCode"].ToString();
            }
            if (Request.QueryString["ID"] != null)
            {
                objAsset.Code = Request.QueryString["ID"].ToString();
            }

            objAsset.load(con, AssetRegistrationBL.eLoadSp.SELECT_ASSET_DETAILS_BY_CODE, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlType.SelectedValue = ds.Tables[0].Rows[0]["Asset_Type"].ToString();
                ddlType.Enabled = false;
                rblowner.SelectedValue = ds.Tables[0].Rows[0]["Owner"].ToString();

                if (rblowner.SelectedValue == "UGCL")
                {
                    placeholderUGCL.Visible = true;
                    placeholderHired.Visible = false;
                }
                else
                {
                    placeholderUGCL.Visible = false;
                    placeholderHired.Visible = true;
                }
                ddlType_SelectedIndexChanged(null, null);
                ddlAssetCategory.SelectedValue = ds.Tables[0].Rows[0]["Asset_category"].ToString();
                ddlAssetCategory.Enabled = false;

                txtAssetCode.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                txtAssetName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                txtRegNo.Text = ds.Tables[0].Rows[0]["Reg_No"].ToString();

                ddlLocation.SelectedValue = ds.Tables[0].Rows[0]["Location"].ToString();
                txtEngineNo.Text = ds.Tables[0].Rows[0]["EngineNo"].ToString();
                txtInsValidDate.Text = ds.Tables[0].Rows[0]["InsValid_Date"].ToString();

                txtStandardInputHour.Text = ds.Tables[0].Rows[0]["StandardInputPerHour"].ToString();
                txtStandardOutPutHour.Text = ds.Tables[0].Rows[0]["StandardOutputPerHour"].ToString();
                rdbinput.SelectedValue = ds.Tables[0].Rows[0]["StandardInput"].ToString();
                ddlUnit_r.SelectedValue = ds.Tables[0].Rows[0]["OutputUOM"].ToString();

                txtBillNo.Text = ds.Tables[0].Rows[0]["Bill_No"].ToString();
                txtBillDate.Text = ds.Tables[0].Rows[0]["Bill_date"].ToString();
                ddlVendorName.SelectedValue = ds.Tables[0].Rows[0]["Vendor"].ToString();
                txtInvoiceAmt.Text = ds.Tables[0].Rows[0]["Inv_Amount"].ToString();
                txtMake.Text = ds.Tables[0].Rows[0]["Make"].ToString();
                ddlCondition.SelectedValue = ds.Tables[0].Rows[0]["Condition"].ToString();
                ddlContractor.SelectedValue = ds.Tables[0].Rows[0]["Contractor"].ToString();

                txtAssetRecievedDate.Text = ds.Tables[0].Rows[0]["RecieveDate"].ToString();
                rd_HSD.SelectedValue = ds.Tables[0].Rows[0]["HSDRecoverable"].ToString();
                txtRunningHrsKms.Text = ds.Tables[0].Rows[0]["RunningHrsKms"].ToString();
                rd_Unit.SelectedValue = ds.Tables[0].Rows[0]["Unit"].ToString();
                txtAvg.Text = ds.Tables[0].Rows[0]["Average"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remark"].ToString();
                txtHireCharges.Text = ds.Tables[0].Rows[0]["HireCharges"].ToString();
                txtAssetCode.Enabled = false;
                btnSaveAsset.Text = "Update";
                rblowner.Enabled = false;

                div_AfterUpload1.Visible = ds.Tables[0].Rows[0]["File_RC"].ToString() != string.Empty ? true : false;
                lnkDownloadFile1.Text = ds.Tables[0].Rows[0]["File_RC"].ToString();
                div_AfterUpload2.Visible = ds.Tables[0].Rows[0]["File_FC"].ToString() != string.Empty ? true : false;
                lnkDownloadFile2.Text = ds.Tables[0].Rows[0]["File_FC"].ToString();
                div_AfterUpload3.Visible = ds.Tables[0].Rows[0]["File_Permit"].ToString() != string.Empty ? true : false;
                lnkDownloadFile3.Text = ds.Tables[0].Rows[0]["File_Permit"].ToString();
                div_AfterUpload4.Visible = ds.Tables[0].Rows[0]["File_Insurance"].ToString() != string.Empty ? true : false;
                lnkDownloadFile4.Text = ds.Tables[0].Rows[0]["File_Insurance"].ToString();
                div_AfterUpload5.Visible = ds.Tables[0].Rows[0]["File_Misc1"].ToString() != string.Empty ? true : false;
                lnkDownloadFile5.Text = ds.Tables[0].Rows[0]["File_Misc1"].ToString();
                div_AfterUpload6.Visible = ds.Tables[0].Rows[0]["File_Misc2"].ToString() != string.Empty ? true : false;
                lnkDownloadFile6.Text = ds.Tables[0].Rows[0]["File_Misc2"].ToString();
            }
        }

        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindProjectNames()
    {
        try
        {
            objProjectBL = new ProjectBL();
            ds = new DataSet();
            // objProjectBL.UserID = Convert.ToInt32(Session["UID"]);
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_ALL, ref ds);
            ddlPresentProjectName.DataSource = ds;
            ddlPresentProjectName.DataTextField = "Project_Name";
            ddlPresentProjectName.DataValueField = "Project_Code";
            ddlPresentProjectName.DataBind();
            ddlPresentProjectName.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    protected void BindLocation()
    {
        try
        {
            ds = new DataSet();
            objLocationBL = new LocationBL();
            objLocationBL.load(con, LocationBL.eLoadSp.SELECT_ALL, ref ds);
            ddlLocation.DataSource = ds;
            ddlLocation.DataValueField = "Location_ID";
            ddlLocation.DataTextField = "Location_Name";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, "-Select-");

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
            objVendorBL = new VendorBL();
            ds = new DataSet();
            objVendorBL.load(con, VendorBL.eLoadSp.SELECT_ASSET_VENDOR_ALL, ref ds);
            ddlVendorName.DataSource = ds;
            ddlVendorName.DataValueField = "Vendor_ID";
            ddlVendorName.DataTextField = "Vendor_name";
            ddlVendorName.DataBind();
            ddlVendorName.Items.Insert(0, "-Select-");

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    protected void BindContractList()
    {
        try
        {
            ds = new DataSet();
            objSubContractorBL = new SubContractorBL();
            objSubContractorBL.load(con, SubContractorBL.eLoadSp.SELECT_CONTRACTOR_ALL_FOR_ASSET, ref ds);

            ddlContractor.DataSource = ds;
            ddlContractor.DataValueField = "Subcon_ID";
            ddlContractor.DataTextField = "Subcon_name";
            ddlContractor.DataBind();
            ddlContractor.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void rblowner_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblowner.SelectedValue == "UGCL")
            {
                placeholderUGCL.Visible = true;
                placeholderHired.Visible = false;
            }
            else
            {
                placeholderUGCL.Visible = false;
                placeholderHired.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSaveType_Click(object sender, EventArgs e)
    {
        try
        {
            objAsset = new AssetRegistrationBL();
            objAsset.Asset_Type = txttype.Text.Trim();
            if (objAsset.AssetTypeInsert(con, AssetRegistrationBL.eLoadSp.INSERT_ASSET_TYPE))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Asset Type has been inserted sucessfully.');", true);
                BindAssetType();
                txttype.Text = string.Empty;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Asset Type already exists.');", true);
            }
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

            ddlType.Items.Clear();
            ddlType.DataTextField = "Asset_Type";
            ddlType.DataValueField = "Asset_Type_ID";
            ddlType.DataSource = ds;
            ddlType.DataBind();
            ddlType.Items.Insert(0, "-Select-");
            ddlAssetType.Items.Clear();
            ddlAssetType.DataSource = ds;
            ddlAssetType.DataBind();
            ddlAssetType.DataTextField = "Asset_Type";
            ddlAssetType.DataValueField = "Asset_Type_ID";
            ddlAssetType.DataBind();
            ddlAssetType.Items.Insert(0, "-Select-");
            Grid_AssetType.DataSource = ds;
            Grid_AssetType.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    //For Clearing After creating Assets
    private void ClearInputValues()
    {
        ddlType.SelectedIndex = -1;
        ddlAssetCategory.SelectedIndex = -1;
        txtStandardInputHour.Text = "0.00";
        txtStandardOutPutHour.Text = "0.00";
        ddlAssetType.SelectedIndex = -1;
        txtAssetCode.Text = string.Empty;
        txtAssetName.Text = string.Empty;
        txtRegNo.Text = string.Empty;
        txtBillDate.Text = string.Empty;
        ddlUnit_r.SelectedIndex = -1;
        txtBillNo.Text = string.Empty;
        ddlVendorName.SelectedIndex = -1;
        txtInvoiceAmt.Text = string.Empty;
        txtRunningHrsKms.Text = string.Empty;
        txtAvg.Text = string.Empty;
        txtHireCharges.Text = string.Empty;
        txtMake.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        ddlCondition.SelectedIndex = -1;
        txtAssetRecievedDate.Text = string.Empty;
        rd_HSD.ClearSelection();
        rd_Unit.ClearSelection();
        txtEngineNo.Text = string.Empty;
        txtInsValidDate.Text = string.Empty;

    }
    protected void imgbtnAssetType_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            mpeAssetType.Show();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSaveAssetCategory_Click(object sender, EventArgs e)
    {
        try
        {
            objAsset = new AssetRegistrationBL();
            objAsset.Asset_Type_ID = Convert.ToInt16(ddlAssetType.SelectedValue.Trim());
            objAsset.Category_Name = txtCategoryName.Text.Trim();
            objAsset.Cat_Prefix = txtCategoryPrefix.Text.Trim();
            if (objAsset.AssetCategoryInsert(con, AssetRegistrationBL.eLoadSp.INSERT_ASSET_CATEGORY))
            {
                BindAssetCategory();
                ddlType_SelectedIndexChanged(null, null);
                txtCategoryName.Text = string.Empty;
                txtCategoryPrefix.Text = string.Empty;
                ddlAssetType.SelectedValue = ddlAssetType.Items.FindByText("-Select-").Value;
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Asset Category has been inserted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Category Name or Prefix already exists.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void BindAssetCategory()
    {
        try
        {
            ds = new DataSet();
            objAsset = new AssetRegistrationBL();
            objAsset.load(con, AssetRegistrationBL.eLoadSp.SELECT_ASSET_CATEGORY_ALL, ref ds);
            Grid_Category.DataSource = ds;
            Grid_Category.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void imgBtnCategory_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            mpeAssetCategory.Show();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSaveAsset_Click(object sender, EventArgs e)
    {
        try
        {
            objAsset = new AssetRegistrationBL();
            objAsset.Asset_Type_ID = Convert.ToInt16(ddlType.SelectedValue.Trim());
            objAsset.Asset_cat_ID = Convert.ToInt16(ddlAssetCategory.SelectedValue.Trim());
            objAsset.Owner = rblowner.SelectedValue.Trim();
            objAsset.Code = txtAssetCode.Text.Trim();
            objAsset.Name = txtAssetName.Text.Trim();
            objAsset.Reg_No = txtRegNo.Text.Trim();

            objAsset.Location = Convert.ToInt16(ddlLocation.SelectedValue.Trim());
            objAsset.EngineNo = txtEngineNo.Text.Trim();
            if (txtInsValidDate.Text != string.Empty)
            {
                objAsset.InsValidDate = Convert.ToDateTime(txtInsValidDate.Text.Trim());
            }else{
                objAsset.InsValidDate = null;
            }
            
            objAsset.Bill_No = txtBillNo.Text.Trim();
            if (txtBillDate.Text.Trim() != string.Empty)
            {
                objAsset.Bill_date = Convert.ToDateTime(txtBillDate.Text.Trim());
            }
            else
            {
                objAsset.Bill_date = null;
            }
            objAsset.Vendor = ddlVendorName.SelectedIndex > 0 ? ddlVendorName.SelectedValue.Trim() : null;
            objAsset.Inv_Amount = txtInvoiceAmt.Text.Trim() != string.Empty ? Convert.ToDecimal(txtInvoiceAmt.Text.Trim()) : Convert.ToDecimal(0);
            objAsset.Make = txtMake.Text.Trim();
            objAsset.Condition = ddlCondition.SelectedIndex > 0 ? ddlCondition.SelectedValue.Trim() : null;

            if (txtHireCharges.Text != string.Empty)
            {
                objAsset.HireCharges = Convert.ToDecimal(txtHireCharges.Text.Trim());
            }
            else
            {
                objAsset.HireCharges = null;
            }
            objAsset.StandardInput = rdbinput.SelectedValue;
            objAsset.Project_Code = ddlPresentProjectName.SelectedValue;
            if (ddlUnit_r.SelectedIndex != 0)
            {
                objAsset.OutputUOM = Convert.ToInt32(ddlUnit_r.SelectedValue);
            }

            objAsset.StandardInputPerHour = Convert.ToDecimal(txtStandardInputHour.Text);
            objAsset.StandardOutputPerHour = Convert.ToDecimal(txtStandardOutPutHour.Text);

            objAsset.Remark = txtRemarks.Text.Trim();
            objAsset.Contractor = ddlContractor.SelectedIndex > 0 ? ddlContractor.SelectedValue.Trim() : null;
            if (txtAssetRecievedDate.Text != string.Empty)
            {
                objAsset.Recieve_Date = Convert.ToDateTime(txtAssetRecievedDate.Text);
            }
            else
            {
                objAsset.Recieve_Date = null;
            }
            objAsset.HSD_Recoverable = rd_HSD.SelectedValue;
            //objAsset.Diesel = txtDiesel.Text.Trim() != string.Empty ? Convert.ToDecimal(txtDiesel.Text.Trim()) : Convert.ToDecimal(0);
            objAsset.RunningHrsKms = txtRunningHrsKms.Text.Trim() != string.Empty ? Convert.ToDecimal(txtRunningHrsKms.Text.Trim()) : Convert.ToDecimal(0);
            objAsset.Unit = rd_Unit.SelectedIndex != -1 ? rd_Unit.SelectedValue.Trim() : null;
            objAsset.Average = txtAvg.Text.Trim();

            if (fuRC.HasFile)
            {
                objAsset.File_RC = "_RC" + System.IO.Path.GetExtension(fuRC.FileName);
            }
            if (fuFC.HasFile)
            {
                objAsset.File_FC = "_FC" + System.IO.Path.GetExtension(fuFC.FileName);
            }
            if (fuPermit.HasFile)
            {
                objAsset.File_Permit = "_Permit" + System.IO.Path.GetExtension(fuPermit.FileName);
            }
            if (fuInsurance.HasFile)
            {
                objAsset.File_Insurance = "_Insurance" + System.IO.Path.GetExtension(fuInsurance.FileName);
            }
            if (fuMisc1.HasFile)
            {
                objAsset.File_Misc1 = "_Misc1" + System.IO.Path.GetExtension(fuMisc1.FileName);
            }
            if (fuMisc2.HasFile)
            {
                objAsset.File_Misc2 = "_Misc2" + System.IO.Path.GetExtension(fuMisc2.FileName);
            }

            if (((Button)sender).Text == "Save")
            {
                if (objAsset.insert(con, AssetRegistrationBL.eLoadSp.INSERT_ASSET))
                {
                    if (fuRC.HasFile)
                    {
                        fuRC.SaveAs(Server.MapPath("~\\UploadedFiles\\Asset\\" + txtAssetCode.Text + "_RC" + System.IO.Path.GetExtension(fuRC.FileName)));
                    }
                    if (fuFC.HasFile)
                    {
                        fuFC.SaveAs(Server.MapPath("~\\UploadedFiles\\Asset\\" + txtAssetCode.Text + "_FC" + System.IO.Path.GetExtension(fuFC.FileName)));
                    }
                    if (fuPermit.HasFile)
                    {
                        fuPermit.SaveAs(Server.MapPath("~\\UploadedFiles\\Asset\\" + txtAssetCode.Text + "_Permit" + System.IO.Path.GetExtension(fuPermit.FileName)));
                    }
                    if (fuInsurance.HasFile)
                    {
                        fuInsurance.SaveAs(Server.MapPath("~\\UploadedFiles\\Asset\\" + txtAssetCode.Text + "_Insurance" + System.IO.Path.GetExtension(fuInsurance.FileName)));
                    }
                    if (fuMisc1.HasFile)
                    {
                        fuMisc1.SaveAs(Server.MapPath("~\\UploadedFiles\\Asset\\" + txtAssetCode.Text + "_Misc1" + System.IO.Path.GetExtension(fuMisc1.FileName)));
                    }
                    if (fuMisc2.HasFile)
                    {
                        fuMisc2.SaveAs(Server.MapPath("~\\UploadedFiles\\Asset\\" + txtAssetCode.Text + "_Misc2" + System.IO.Path.GetExtension(fuMisc2.FileName)));
                    }
                    ClearInputValues();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Asset details has been created sucessfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Asset Code already exists!.');", true);
                }
            }

            else
            {
                if (Request.QueryString["AssetCode"] != null)
                {
                    objAsset.Code = Request.QueryString["AssetCode"].ToString();
                }
                if (Request.QueryString["ID"] != null)
                {
                    objAsset.Code = Request.QueryString["ID"].ToString();
                }

                if (objAsset.update(con, AssetRegistrationBL.eLoadSp.UPDATE_ASSET_TYPE))
                {
                    if (fuRC.HasFile)
                    {
                        fuRC.SaveAs(Server.MapPath("~\\UploadedFiles\\Asset\\" + txtAssetCode.Text + "_RC" + System.IO.Path.GetExtension(fuRC.FileName)));
                    }
                    if (fuFC.HasFile)
                    {
                        fuFC.SaveAs(Server.MapPath("~\\UploadedFiles\\Asset\\" + txtAssetCode.Text + "_FC" + System.IO.Path.GetExtension(fuFC.FileName)));
                    }
                    if (fuPermit.HasFile)
                    {
                        fuPermit.SaveAs(Server.MapPath("~\\UploadedFiles\\Asset\\" + txtAssetCode.Text + "_Permit" + System.IO.Path.GetExtension(fuPermit.FileName)));
                    }
                    if (fuInsurance.HasFile)
                    {
                        fuInsurance.SaveAs(Server.MapPath("~\\UploadedFiles\\Asset\\" + txtAssetCode.Text + "_Insurance" + System.IO.Path.GetExtension(fuInsurance.FileName)));
                    }
                    if (fuMisc1.HasFile)
                    {
                        fuMisc1.SaveAs(Server.MapPath("~\\UploadedFiles\\Asset\\" + txtAssetCode.Text + "_Misc1" + System.IO.Path.GetExtension(fuMisc1.FileName)));
                    }
                    if (fuMisc2.HasFile)
                    {
                        fuMisc2.SaveAs(Server.MapPath("~\\UploadedFiles\\Asset\\" + txtAssetCode.Text + "_Misc2" + System.IO.Path.GetExtension(fuMisc2.FileName)));
                    }
                    GetAssetRegistrationDetails();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Asset Registration  details has been updated');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update the Asset Registration details');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancelAsset_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["AssetCode"] != null || Request.QueryString["ID"] != null)
            {

                Response.Redirect("../Asset/AssetRegistrationList.aspx", false);
            }
            else
            {
                Response.Redirect("../Asset/AssetRegistration.aspx", false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_AssetType_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ds = new DataSet();
            objAsset = new AssetRegistrationBL();
            objAsset.Asset_Type_ID = Convert.ToInt32(e.Record["Asset_Type_ID"]);

            if (objAsset.delete(con, AssetRegistrationBL.eLoadSp.DELETE_ASSET_TYPE_BY_ID))
            {

                BindAssetType();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Asset type has been deleted sucessfully.');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Asset type cannot be deleted as it is already in use.');", true);
            }

            mpeAssetType.Hide();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_Category_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ds = new DataSet();
            objAsset = new AssetRegistrationBL();
            objAsset.Asset_cat_ID = Convert.ToInt32(e.Record["Asset_cat_ID"]);

            if (objAsset.delete(con, AssetRegistrationBL.eLoadSp.DELETE_ASSET_CATEGORY_DELETE_BY_ID))
            {

                BindAssetCategory();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Asset category has been deleted sucessfully.');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Asset category cannot be deleted as it is already in use.');", true);
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancelType_Click(object sender, EventArgs e)
    {
        txttype.Text = string.Empty;
    }


    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlType.SelectedIndex != 0)
            {
                objAsset = new AssetRegistrationBL();
                DataSet ds = new DataSet();
                objAsset.Asset_Type = ddlType.SelectedValue;
                objAsset.load(con, AssetRegistrationBL.eLoadSp.SELECT_Asset_BY_ID, ref ds);
                ddlAssetCategory.DataSource = ds;
                ddlAssetCategory.DataValueField = "AssetCatID";
                ddlAssetCategory.DataTextField = "CateName";
                ddlAssetCategory.DataBind();
                ddlAssetCategory.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlAssetCategory.Items.Clear();
                ddlAssetCategory.Items.Insert(0, "-Select-");
                ddlAssetCategory.SelectedIndex = 0;

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    /// <summary>
    /// ///////////////project loaction BINDING
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlPresentProjectName_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            objAsset = new AssetRegistrationBL();
            ds = new DataSet();
            objAsset.Project_Code = ddlPresentProjectName.SelectedValue;
            objAsset.load(con, AssetRegistrationBL.eLoadSp.PROJECT_BASED_LOCATION, ref ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlLocation.SelectedValue = ds.Tables[0].Rows[0]["Location"].ToString();
            }
            else
            {
                ddlLocation.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    protected void lnkDownloadFile_Click(object sender, EventArgs e)
    {
        try
        {
            string filePath = "../UploadedFiles/Asset/" + (sender as LinkButton).Text;
            string filepathnew = Server.MapPath("~\\UploadedFiles\\Asset\\" + (sender as LinkButton).Text);
            FileInfo file = new FileInfo(filePath);
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.Flush();
            Response.TransmitFile(filepathnew);
            //Response.WriteFile(filePath);
            Response.End();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }
}
