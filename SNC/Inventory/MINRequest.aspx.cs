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
using DataAccess;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace SNC.Inventory
{
    public partial class MINRequest : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        ProjectBL objProjectBL = null;
        UOM objUOM = null;
        AssetRegistrationBL objAsset = null;
        AssetTransferBL objassetTransferBL = null;
        SubContractorBL objSubContractorBL = null;

        MINBL objMINBL = null;
        MaterialBL objMaterial = null;
        AssetRegistrationBL objAssetBL = null;
        DataSet ds = null;
        public decimal AvailiableQty = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["UID"] != null)
                    {
                   
                        BindProjectList();
                        BindUOMDetails();
                        BindAssetType();
                        BindContractList();
                        BindBudgetSector();
                        BindVerifier();
                        if (Request.QueryString["MIN_No"] != null)
                        {
                            GetMINLISTDetails(Request.QueryString["MIN_No"].ToString());
                            BindItemsList();
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
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void BindBudgetSector()
        {
            try
            {
                //bool exists;
                objMaterial = new MaterialBL();
                DataSet dsBudget = new DataSet();
                DataTable DatafilterDt = new DataTable();

                objMaterial.load(con, MaterialBL.eLoadSp.SELECT_BUDGET_SECTOR_ALL, ref dsBudget);

                //DatafilterDt = dsBudget.Tables[0];

                //exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Sector_Name").Equals("HSD")).Count() > 0;
                //if (exists)
                //{
                //    DataTable datadt = DatafilterDt.AsEnumerable()
                //                 .Where(r => r.Field<string>("Sector_Name") != "HSD")
                //                 .CopyToDataTable();

                //    ddlBudgetSector.DataSource = datadt;
                //}


                ddlBudgetSector.DataSource = dsBudget;
                ddlBudgetSector.DataTextField = "Sector_Name";
                ddlBudgetSector.DataValueField = "Budget_Sector_ID";
                ddlBudgetSector.DataBind();
                ddlBudgetSector.Items.Insert(0, "-Select");


            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }

        protected void BindVerifier()
        {
            try
            {
                IndentBL objIndent = new IndentBL();
                ds = new DataSet();
                objIndent.load(con, IndentBL.eLoadSp.SELECT_USERNAMES_ALL, ref ds);
                ddlVerifier.DataSource = ds;
                ddlVerifier.DataTextField = "Name";
                ddlVerifier.DataValueField = "UID";
                ddlVerifier.DataBind();
                ddlVerifier.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void BindProjectList()
        {
            try
            {
                objProjectBL = new ProjectBL();
                ds = new DataSet();

                if (Session["Project_Code"] != null)
                {
                    objProjectBL.Project_Code = Session["Project_Code"].ToString();

                    objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_BY_Project_Code, ref ds);
                    ddlProjectName.DataSource = ds;
                    ddlProjectName.DataTextField = "Project_Name";
                    ddlProjectName.DataValueField = "Project_Code";
                    ddlProjectName.DataBind();
                    ddlProjectName.Enabled = false;
                    ddlProjectName.Items.Insert(0, "-Select-");
                    ddlProjectName.SelectedValue = Session["Project_Code"].ToString();
                }


            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }
        protected void BindItemsList()
        {
            try
            {
                objMINBL = new MINBL();
                ds = new DataSet();
                objMINBL.MIN_No = txtMINNo.Text;
                objMINBL.load(con, MINBL.eLoadSp.SelectItemsAll, ref ds);
                ItemList_Grid.DataSource = ds;
                ItemList_Grid.DataBind();

            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }
        protected void BindContractList()
        {
            try
            {
                ds = new DataSet();
                objSubContractorBL = new SubContractorBL();
                objSubContractorBL.load(con, SubContractorBL.eLoadSp.SELECT_CONTRACTOR_ALL, ref ds);
                ddlSubContractor.DataSource = ds;
                ddlSubContractor.DataTextField = "Subcon_name";
                ddlSubContractor.DataValueField = "Subcon_ID";
                ddlSubContractor.DataBind();
                ddlSubContractor.Items.Insert(0, "-Select-");

            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        private void BindAssetType()
        {
            try
            {
                ds = new DataSet();
                objAssetBL = new AssetRegistrationBL();
                objAssetBL.load(con, AssetRegistrationBL.eLoadSp.SELECT_ASSET_TYPE_ALL, ref ds);

                ddlAssetType_r.DataSource = ds;
                ddlAssetType_r.DataTextField = "Asset_Type";
                ddlAssetType_r.DataValueField = "Asset_Type_ID";
                ddlAssetType_r.DataBind();
                ddlAssetType_r.Items.Insert(0, "-Select-");


            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void chkRenewable_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRenewable.Checked == true)
            {
                divrecurring.Visible = true;
                divrecurring1.Visible = true;
            }
            else
            {
                divrecurring.Visible = false;
                divrecurring1.Visible = false;
            }
            ModelMINPopup.Show();
        }
        protected void GetMINLISTDetails(string MIN_No)
        {
            try
            {
                objMINBL = new MINBL();
                ds = new DataSet();
                objMINBL.MIN_No = MIN_No;
                objMINBL.load(con, MINBL.eLoadSp.SELECT_BY_ID, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtMINNo.Text = ds.Tables[0].Rows[0]["MIN_No"].ToString();
                    txtDate.Text = ds.Tables[0].Rows[0]["DATE"].ToString();
                    ddlProjectName.SelectedValue = ds.Tables[0].Rows[0]["Project_Code"].ToString();
                    rdIssueTo.SelectedValue = ds.Tables[0].Rows[0]["Issue_To"].ToString();
                    txtIssueToFor.Text = ds.Tables[0].Rows[0]["IssueToFor"].ToString();
                    //ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["Department"].ToString();
                    //ddlEmployeeName.SelectedValue = ds.Tables[0].Rows[0]["Employee_ID"].ToString();
                    ddlSubContractor.SelectedValue = ds.Tables[0].Rows[0]["Subcon_ID"].ToString();
                    rdRecoverable.SelectedValue = ds.Tables[0].Rows[0]["Recoverable"].ToString();
                    txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                    txtMINNo.Enabled = false;
                    btnSubmit.Text = "Update";
                    btnAddItem.Visible = true;
                    rdIssueTo_SelectedIndexChanged(null, null);
                    rdIssueTo.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
                    ddlUnit.DataSource = ds;
                    ddlUnit.DataTextField = "UOMPrefix";
                    ddlUnit.DataValueField = "UOM_ID";
                    ddlUnit.DataBind();
                    ddlUnit.Items.Insert(0, "-Select-");
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }

        }

        protected void BindAssetRegistrationList()
        {
            try
            {
                ds = new DataSet();
                objAssetBL = new AssetRegistrationBL();
                objAssetBL.load(con, AssetRegistrationBL.eLoadSp.SELECT_ASSET_ALL, ref ds);
                ddlAssetName.DataSource = ds;
                ddlAssetName.DataTextField = "Name";
                ddlAssetName.DataValueField = "Code";
                ddlAssetName.DataBind();
                ddlAssetName.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                objMINBL = new MINBL();
                objMINBL.MIN_No = Convert.ToString(txtMINNo.Text);
                objMINBL.DATE = Convert.ToDateTime(txtDate.Text);
                objMINBL.Approver = Convert.ToString(ddlVerifier.SelectedValue);
                if (rdIssueTo.SelectedValue == "UGCL")
                {


                }
                else
                {
                    if (ddlSubContractor.SelectedIndex == 0 || rdRecoverable.SelectedValue == string.Empty)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select Sub Contractor and Recoverable.!');", true);
                        return;
                    }
                }

                // if (ddlProjectName.SelectedIndex != 0)
                //{
                if (Session["Project_Code"] != null)
                {
                    objMINBL.Project_Code = Session["Project_Code"].ToString();
                }

                // ddlProjectName.SelectedValue;
                //}
                else
                {
                    objMINBL.Project_Code = null;
                }

                if (rdIssueTo.SelectedValue == "UGCL")
                {

                    if (txtIssueToFor.Text != string.Empty)
                    {
                        objMINBL.IssueToFor = txtIssueToFor.Text;
                    }

                }
                else
                {
                    if (ddlSubContractor.SelectedIndex != 0)
                    {
                        objMINBL.Subcon_ID = ddlSubContractor.SelectedValue;
                    }
                    else
                    {
                        objMINBL.Subcon_ID = null;
                    }


                    objMINBL.Recoverable = Convert.ToBoolean(rdRecoverable.SelectedValue);
                }

                objMINBL.Issue_To = rdIssueTo.SelectedValue;
                objMINBL.Remarks = txtRemarks.Text;
                objMINBL.WONo = ddlWO.SelectedValue;
                if (((Button)sender).Text == "Submit")
                {
                    if (objMINBL.insert(con, MINBL.eLoadSp.INSERT))
                    {
                        btnAddItem.Visible = true;
                        txtMINNo.Text = objMINBL.MIN_No.ToString();
                        btnSubmit.Text = "Update";
                        rdIssueTo.Enabled = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Material Issue Note details has been Created');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Material Issue Note details failed to create');", true);
                    }
                }
                else
                {
                    objMINBL.MIN_No = txtMINNo.Text.Trim();
                    if (objMINBL.update(con, MINBL.eLoadSp.UPDATE))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Material Issue Note details has been updated');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Material Issue Note details Failed to update');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }
        private void BindCategoryDetails()
        {

            try
            {
                objMINBL = new MINBL();
                ds = new DataSet();
                if (objMINBL.load(con, MINBL.eLoadSp.SELECT_CATEGORY_FROM_STOCK, ref ds))
                {

                    if (ds.Tables[0].Rows.Count >= 0)
                    {

                        ddlCategory.DataSource = ds;
                        ddlCategory.DataTextField = "Category_Name";
                        ddlCategory.DataValueField = "Mat_cat_ID";
                        ddlCategory.DataBind();
                        ddlCategory.Items.Insert(0, "-Select-");
                    }
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            try
            {
                if (btnSubmit.Text == "Submit")
                {
                    Response.Redirect("../Inventory/MIN.aspx", false);
                }
                else
                {
                    Response.Redirect("../Inventory/MINList.aspx", false);
                }
            }

            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {

                objMINBL = new MINBL();
                ds = new DataSet();
                if (ddlCategory.SelectedIndex != 0)
                {
                    objMINBL.Mat_cat_ID = Convert.ToUInt16(ddlCategory.SelectedValue.Trim());
                    objMINBL.load(con, MINBL.eLoadSp.SELECT_ITEMCODE_BY_CATID_FROM_STOCK, ref ds);
                    ddlItem.DataSource = ds;
                    ddlItem.DataValueField = "Item_Code";
                    ddlItem.DataTextField = "Item_Name";
                    ddlItem.DataBind();
                    ddlItem.Items.Insert(0, "-Select-");
                    ddlItem.Enabled = true;
                    //ddlUnit.SelectedIndex = -1;
                    //ddlUnit.SelectedValue = ddlUnit.Items.FindByText("-Select-").Value;
                    ModelMINPopup.Show();
                }
                else
                {
                    ddlItem.SelectedValue = ddlItem.Items.FindByText("-Select-").Value;
                    ddlItem.Enabled = false;
                    // ddlUnit.SelectedIndex = -1;
                    //ddlUnit.SelectedValue = ddlUnit.Items.FindByText("-Select-").Value;
                    ModelMINPopup.Show();
                }
                txtAvailableQty.Text = string.Empty;
                ModelMINPopup.Show();
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }


        protected void ddlAssetName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAssetName.SelectedIndex != 0)
            {


                ModelMINPopup.Show();
            }
        }
        decimal OldQty = 0;
        protected void Btn_SaveItem(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(txtQty.Text.Trim()) > Convert.ToDecimal(txtAvailableQty.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Issue Quantity should not be greater than Available Quantity');", true);
                    return;
                }

                objMINBL = new MINBL();
                if (ddlBudgetSector.SelectedIndex > 0)
                {
                    objMINBL.Budget_Sector = ddlBudgetSector.SelectedValue;
                }

                objMINBL.Project_Code = ddlProjectName.SelectedValue;
                objMINBL.Mat_cat_ID = Convert.ToInt32(ddlCategory.SelectedValue);
                objMINBL.Item_Code = ddlItem.SelectedValue;
                objMINBL.Recurring = Convert.ToBoolean(chkRenewable.Checked);
                objMINBL.MIN_No = txtMINNo.Text;
                objMINBL.WONo = ddlWO.SelectedItem.Text;
                if (ddlAssetName.SelectedIndex != 0)
                {
                    objMINBL.Asset_Code = ddlAssetName.SelectedValue;
                }


                if (chkRenewable.Checked)
                {
                    if (ddlAssetName.SelectedIndex != 0)
                    {
                        objMINBL.Asset_Code = ddlAssetName.SelectedValue;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please Select Asset Name');", true);
                        ModelMINPopup.Show();
                        return;

                    }

                    objMINBL.Standard = Convert.ToDecimal(txtStandard.Text);
                    objMINBL.Service_Date = Convert.ToDateTime(txtServiceDate.Text);

                    if (ddlMaintenance.SelectedIndex > 0)
                    {
                        objMINBL.Maintenance = ddlMaintenance.SelectedItem.Text;
                    }



                    DataSet dschk = new DataSet();
                    DailyRunningHourBL objDailyRunningHourBL = new DailyRunningHourBL();
                    objDailyRunningHourBL.Asset_Code = ddlAssetName.SelectedValue;
                    objDailyRunningHourBL.Item_Code = ddlItem.SelectedValue;
                    objDailyRunningHourBL.load(con, DailyRunningHourBL.eLoadSp.CHECKING_ITEM_Remaining_Capacity, ref dschk);
                    if (dschk.Tables[0].Rows.Count > 0)
                    {
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "ConfirmationForRecurring()", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This recurring item not rearched 80 % of standard hour. Do you want to proceed?');", true);
                        // return;
                    }


                }
                objMINBL.Unit = Convert.ToInt32(ddlUnit.SelectedValue);
                objMINBL.RequestedQty = txtQty.Text != string.Empty ? Convert.ToDecimal(txtQty.Text) : Convert.ToDecimal(0);
                objMINBL.UserID = Session["User_ID"].ToString();
                objMINBL.Avaliable_Qty = Convert.ToDecimal(txtAvailableQty.Text.Trim());

                if (objMINBL.Avaliable_Qty >= 0)
                {
                    objMINBL.Final_Qty = (objMINBL.Avaliable_Qty - objMINBL.Issue_Qty);
                }
                objMINBL.DATE = Convert.ToDateTime(txtDate.Text);


                if (((Button)sender).Text == "Save")
                {
                    if (objMINBL.Iteminsert(con, MINBL.eLoadSp.Iteminsert))
                    {
                        //DataSet dsStock = new DataSet();
                        //objMINBL.Task = "Sum_Adjusted_Qty";
                        //objMINBL.load(con, MINBL.eLoadSp.DEDUCT_STOCK_BASED_ON_ISSUE_QTY, ref dsStock);
                        //if (true)
                        //{
                        //    AvailiableQty = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["Adjust_QTY"].ToString());
                        //}
                        //dsStock.Clear();
                        //DataSet Ds_Deduct_Stock = new DataSet();
                        //objMINBL.Task = "Get_Stock_Records";
                        //objMINBL.load(con, MINBL.eLoadSp.DEDUCT_STOCK_BASED_ON_ISSUE_QTY, ref Ds_Deduct_Stock);
                        //if (Ds_Deduct_Stock.Tables[0].Rows.Count >= 0)
                        //{
                        //    foreach (DataRow item in Ds_Deduct_Stock.Tables[0].Rows)
                        //    {
                        //        DataSet ds_Update = new DataSet();
                        //        objMINBL.Task = "Update_Stock_Records";
                        //        objMINBL.Issue_Qty = 0;
                        //        objMINBL.Stock_ID = Convert.ToInt32(item["Stock_ID"]);
                        //        objMINBL.load(con, MINBL.eLoadSp.UPDATE_STOCK_QTY, ref ds_Update);

                        //    }
                        //    objMINBL.Issue_Qty = AvailiableQty + Convert.ToDecimal(txtQty.Text.Trim());
                        //    if (objMINBL.Stock_ID != 0)
                        //    {
                        //        objMINBL.Task = "Update_Stock_Records";
                        //        DataSet ds_Update_Stock = new DataSet();
                        //        objMINBL.load(con, MINBL.eLoadSp.UPDATE_STOCK_QTY, ref ds_Update_Stock);
                        //    }
                        //}
                        BindItemsList();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Items details has been Created');", true);
                        ResetMRNItems();
                    }
                    else
                    {
                        BindItemsList();

                        ResetMRNItems();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Items details already exist');", true);
                    }
                }

            }
            catch (Exception ex)
            {

                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        private void ResetMRNItems()
        {
            txtServiceDate.Text = string.Empty;
            ddlAssetType_r.SelectedIndex = -1;
            ddlAssetCate_r.Items.Clear();
            ddlAssetCate_r.Items.Insert(0, "-Select-");
            ddlAssetName.Items.Clear();
            ddlAssetName.Items.Insert(0, "-Select-");
            txtQty.Text = string.Empty;
            txtStandard.Text = string.Empty;
            //ddlAssetName.SelectedValue = ddlAssetName.Items.FindByText("-Select-").Value;
            ddlBudgetSector.SelectedIndex = -1;

            ddlCategory.Items.Clear();
            ddlCategory.Items.Insert(0, "-Select-");
            ddlItem.Items.Clear();
            ddlItem.Items.Insert(0, "-Select-");
            //ddlItem.SelectedValue = ddlItem.Items.FindByText("-Select-").Value;
            ddlMaintenance.SelectedValue = ddlMaintenance.Items.FindByText("-Select-").Value;
            ddlUnit.Items.Clear();
            ddlUnit.Items.Insert(0, "-Select-");
            ddlUnit.SelectedIndex = 0;
            txtAvailableQty.Text = string.Empty;
            chkRenewable.Checked = false;
            divrecurring.Visible = false;

        }

        protected void rdIssueTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdIssueTo.SelectedValue == "UGCL")
                {
                    deptempname.Visible = true;
                    subcontractorrecoverable.Visible = false;
                }
                else
                {
                    subcontractorrecoverable.Visible = true;
                    deptempname.Visible = false;
                }
            }
            catch (Exception ex)
            {

                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objMINBL = new MINBL();
                DataSet dsItem = new DataSet();
                if (ddlItem.SelectedIndex != 0)
                {
                    objMINBL.Item_Code = ddlItem.SelectedValue.ToString();
                    objMINBL.Project_Code = ddlProjectName.SelectedValue;
                    objMINBL.DATE = Convert.ToDateTime(txtDate.Text);
                    if (objMINBL.load(con, MINBL.eLoadSp.SELECT_AVLQTY_FROM_STOCK, ref dsItem))
                    {
                        if (dsItem.Tables[0].Rows.Count > 0)
                        {
                            txtAvailableQty.Text = dsItem.Tables[0].Rows[0]["AvailableQty"].ToString();
                        }

                        if (dsItem.Tables[1].Rows.Count > 0)
                        {
                            ddlUnit.DataSource = dsItem.Tables[1];
                            ddlUnit.DataTextField = "UOMPrefix";
                            ddlUnit.DataValueField = "UOM_ID";
                            ddlUnit.DataBind();
                            ddlUnit.Items.Insert(0, "-Select-");
                            ddlUnit.SelectedValue = dsItem.Tables[1].Rows[0]["UOM_ID"].ToString();
                        }
                        else
                        {
                            ddlUnit.SelectedIndex = -1;
                            txtAvailableQty.Text = "0";
                        }
                    }
                }
                else
                {
                    ddlUnit.SelectedIndex = -1;
                    txtAvailableQty.Text = "0";
                }
                ModelMINPopup.Show();

            }
            catch (Exception ex)
            {

                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void ItemList_Grid_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            try
            {
                objMINBL = new MINBL();
                objMINBL.MIN_Item_ID = Convert.ToInt32(e.Record["MIN_Item_ID"]);
                objMINBL.UserID = Session["User_ID"].ToString();
                if (objMINBL.delete(con, MINBL.eLoadSp.DELETE_MINITEM_BY_ID))
                {
                    BindItemsList();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('MIN Items has been deleted successfully!');", true);
                }
            }
            catch (Exception ex)
            {

                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void btnCancelItem_Click(object sender, EventArgs e)
        {
            try
            {
                ResetMRNItems();
            }
            catch (Exception ex)
            {

                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void ddlBudgetSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ds = new DataSet();
                objMINBL = new MINBL();
                if (ddlBudgetSector.SelectedIndex != 0)
                {
                    objMINBL.Budget_Sector_ID = Convert.ToInt32(ddlBudgetSector.SelectedValue);
                    objMINBL.load(con, MINBL.eLoadSp.SELECT_Category_From_BudgetSector, ref ds);
                    ddlCategory.DataSource = ds;
                    ddlCategory.DataTextField = "Category_Name";
                    ddlCategory.DataValueField = "Mat_cat_ID";
                    ddlCategory.DataBind();
                    ddlCategory.Items.Insert(0, "-Select");
                    ModelMINPopup.Show();
                }
                else
                {
                    ddlCategory.Items.Clear();
                    ddlCategory.Items.Insert(0, "-Select");
                    ddlItem.Items.Clear();
                    ddlItem.Items.Insert(0, "-Select");
                    ddlUnit.Items.Clear();
                    ddlUnit.Items.Insert(0, "-Select");
                    txtQty.Text = string.Empty;
                    ModelMINPopup.Show();
                    txtAvailableQty.Text = string.Empty;
                }

                if (ddlBudgetSector.SelectedValue == "1" || ddlBudgetSector.SelectedValue == "2" || ddlBudgetSector.SelectedValue == "6" || ddlBudgetSector.SelectedValue == "7")
                {
                    TrAutomobiles.Visible = true;
                    TrAutomobiles1.Visible = true;
                }
                else
                {
                    TrAutomobiles.Visible = false;
                    TrAutomobiles1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void ddlAssetType_r_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAssetCategory2();
            ModelMINPopup.Show();
        }
        private void BindAssetCategory2()
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

                    ddlAssetName.Items.Clear();
                    ddlAssetName.Items.Insert(0, "-Select-");
                    ddlAssetName.SelectedIndex = 0;

                }
                else
                {
                    ddlAssetCate_r.Items.Clear();
                    ddlAssetCate_r.Items.Insert(0, "-Select-");
                    ddlAssetCate_r.SelectedIndex = 0;
                    ddlAssetName.Items.Clear();
                    ddlAssetName.Items.Insert(0, "-Select-");
                    ddlAssetName.SelectedIndex = 0;

                }

            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }


        protected void ddlAssetCate_r_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAssetNames();
            ModelMINPopup.Show();
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
                    objassetTransferBL.Project_Code = Session["Project_Code"].ToString();
                    objassetTransferBL.load(con, AssetTransferBL.eLoadSp.SELECT_ASSET_NAME_BY_ASSET_CATEGORY, ref ds);

                    ddlAssetName.DataSource = ds;
                    ddlAssetName.DataTextField = "AssetNameWithCode";
                    ddlAssetName.DataValueField = "Code";
                    ddlAssetName.DataBind();
                    ddlAssetName.Items.Insert(0, "-Select-");


                }
                else
                {
                    ddlAssetName.Items.Clear();
                    ddlAssetName.Items.Insert(0, "-Select-");
                    ddlAssetName.SelectedIndex = 0;

                }
            }
            catch (Exception ex)
            {
               SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void BindWoNumber()
        {
            try
            {
                ds = new DataSet();
                SubContractorBillBL objSubContBL = new SubContractorBillBL();

                objSubContBL.Task = "Get_WO_Number";
                objSubContBL.SubContractorID = Convert.ToString(ddlSubContractor.SelectedValue.ToString());
                objSubContBL.Project_Code = ddlProjectName.SelectedValue.ToString();
                objSubContBL.WONo = ddlSubContractor.SelectedValue.ToString();
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_WO_NUMBER, ref ds);
                ddlWO.DataSource = ds;
                ddlWO.DataTextField = "WONo";
                ddlWO.DataValueField = "WO_ID";
                ddlWO.DataBind();
                ddlWO.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void ddlSubContractor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSubContractor.SelectedIndex != 0)
                {
                    BindWoNumber();
                }
                else
                {
                    //ddlWONo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
    }
}