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
using DataAccess;
using SNC.ErrorLogger;
using System.Collections;
using Obout.Grid;


public partial class Indent : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    ProjectBL objProjectBL = null;
    LocationBL objLocation = null;
    IndentBL objIndent = null;
    DataSet ds = null;
    decimal Qty_Available = 0;
    decimal Qty_Required = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CalendarTentative.StartDate = DateTime.Now;

            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    BindProjectNames();
                    if (Session["Project_Code"] != null)
                    {
                        ddlProjectName.SelectedValue = Session["Project_Code"].ToString();
                    }
                    ddlProjectName.Enabled = false;
                    ddlProjectName_SelectedIndexChanged(null, null);
                    BindUsersNames();
                    BindLocation();
                   // BindBudgetSectors();
                    if (Request.QueryString["IndentNo"] != null)
                    {
                        GetIndentDetails();
                        rblProcessFrom_SelectedIndexChanged(sender, e);
                        rd_Status_SelectedIndexChanged(sender, e);
                        CheckIndentNoInPO();
                        BindCategoryDetails();
                        btnPrint.Target = "_blank";
                        btnPrint.HRef = "Material_Indent_Print.aspx?IndentNo=" + Request.QueryString["IndentNo"].ToString();

                        //  if (ddlBudget.SelectedIndex == 0)
                        if (rd_Status.SelectedValue == "Processed")
                        {
                            btnAddItem.Visible = false;
                            btnReferBudgetItems.Visible = false;
                            btnSubmit.Visible = false;
                            Grid_IndentItem.Columns[0].Visible = false;
                            Grid_IndentItem.Columns[1].Visible = false;
                            Grid_IndentItem.Columns[19].Visible = false;
                            Qty_Available = 0;
                            Qty_Required = 0;
                            btnMultipitemDelete.Visible = false;
                            Grid_IndentItem.DataBind();
                        }
                        ddlBudget_SelectedIndexChanged(null, null);
                    }
                    else
                    {
                        txtIndDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
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

    protected void BindProjectNames()
    {
        try
        {
            objProjectBL = new ProjectBL();
            ds = new DataSet();
            //objProjectBL.UserID = Convert.ToInt32(Session["UID"]);
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_ALL, ref ds);
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

    private void CheckIndentNoInPO()
    {
        objIndent = new IndentBL();
        ds = new DataSet();
        objIndent.Indent_No = Request.QueryString["IndentNo"].ToString();
        objIndent.load(con, IndentBL.eLoadSp.CHECK_INDENTNO_IN_PO, ref ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            btnSubmit.Visible = false;
            btnAddItem.Visible = false;
            btnReferBudgetItems.Visible = false;
            btnProceed.Visible = false;
            btnPrint.Visible = true;
            Grid_IndentItem.Columns[0].Visible = false;
            Grid_IndentItem.Columns[14].Visible = false;
            Qty_Available = 0;
            Qty_Required = 0;

            Grid_IndentItem.DataBind();
        }
    }

    protected void BindBudgets()
    {
        try
        {
            objIndent = new IndentBL();
            DataSet budDS = new DataSet();
            objIndent.Project_Code = ddlProjectName.SelectedValue.Trim();
            objIndent.load(con, IndentBL.eLoadSp.SELECT_BUDGETID_ALL, ref budDS);
            ddlBudget.DataSource = budDS;
            ddlBudget.DataTextField = "Budget_ID";
            ddlBudget.DataValueField = "Abs_BID";
            ddlBudget.DataBind();
            ddlBudget.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

   

    protected void BindBudgetSectors()
    {
        try
        {
            objIndent = new IndentBL();
            DataSet budSectorDS = new DataSet();
            objIndent.AbstractBudget_ID = Convert.ToInt32(ddlBudget.SelectedValue);
            //objIndent.Project_Code = objIndent.Project_Code = ddlProjectName.SelectedValue.Trim();
            objIndent.load(con, IndentBL.eLoadSp.SELECT_BUDGET_SECTORS_BY_ID, ref budSectorDS);
            ddlBudgetSector.DataSource = budSectorDS;
            ddlBudgetSector.DataTextField = "Bud_type";
            ddlBudgetSector.DataValueField = "Bud_type";
            ddlBudgetSector.DataBind();
            ddlBudgetSector.Items.Insert(0, "-Select-");


            ddlSectorName.DataSource = budSectorDS;
            ddlSectorName.DataTextField = "Bud_type";
            ddlSectorName.DataValueField = "Bud_type";
            ddlSectorName.DataBind();
            ddlSectorName.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    protected void BindUsersNames()
    {
        try
        {
            objIndent = new IndentBL();
            ds = new DataSet();
            bool exists;
            DataTable DatafilterDt = new DataTable();
            objIndent.load(con, IndentBL.eLoadSp.SELECT_USERNAMES_ALL, ref ds);



            if (ds.Tables[0].Rows.Count > 0)
            {
                DatafilterDt = ds.Tables[0];

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<bool>("IsHoUser").Equals(true)).Count() > 0;
                if (exists)
                {
                    DataTable Itemsdt = DatafilterDt.AsEnumerable()
                                 .Where(r => r.Field<bool>("IsHoUser") == true)
                                 .CopyToDataTable();

                    ddlProcessBy.DataSource = Itemsdt;
                    ddlProcessBy.DataTextField = "Name";
                    ddlProcessBy.DataValueField = "UID";
                    ddlProcessBy.DataBind();
                    ddlProcessBy.Items.Insert(0, "-Select-");


                    ddlHOApproved.DataSource = Itemsdt;
                    ddlHOApproved.DataTextField = "Name";
                    ddlHOApproved.DataValueField = "UID";
                    ddlHOApproved.DataBind();
                    ddlHOApproved.Items.Insert(0, "-Select-");

                }
                exists = false;

                ddlPreparedBy.DataSource = ds;
                ddlPreparedBy.DataTextField = "Name";
                ddlPreparedBy.DataValueField = "UID";
                ddlPreparedBy.DataBind();
                ddlPreparedBy.Items.Insert(0, "-Select-");
                ddlPreparedBy.SelectedValue = Session["UID"].ToString();
                ddlPreparedBy.Enabled = false;

                ddlStckChk.DataSource = ds;
                ddlStckChk.DataTextField = "Name";
                ddlStckChk.DataValueField = "UID";
                ddlStckChk.DataBind();
                ddlStckChk.Items.Insert(0, "-Select-");
            }

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
            objLocation = new LocationBL();
            objLocation.load(con, LocationBL.eLoadSp.SELECT_ALL, ref ds);
            ddlNameofSite.DataSource = ds;
            ddlNameofSite.DataValueField = "Location_ID";
            ddlNameofSite.DataTextField = "Location_Name";
            ddlNameofSite.DataBind();
            ddlNameofSite.Items.Insert(0, "-Select-");

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
            objIndent = new IndentBL();
            DataSet dsCat = new DataSet();
            if (ddlBudget.SelectedIndex != 0)
            {
                objIndent.AbstractBudget_ID = Convert.ToInt32(ddlBudget.SelectedValue.Trim());
                objIndent.BudgetSector = ddlBudgetSector.SelectedValue;
                if (objIndent.load(con, IndentBL.eLoadSp.SELECT_MATERIAL_FROM_BUDGET_ITEM, ref dsCat))
                {

                    if (dsCat.Tables[0].Rows.Count >= 0)
                    {

                        ddlCategory.DataSource = dsCat;
                        ddlCategory.DataTextField = "Category_Name";
                        ddlCategory.DataValueField = "Mat_cat_ID";
                        ddlCategory.DataBind();
                        ddlCategory.Items.Insert(0, "-Select-");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }



    private void GetProjectBudgetQty()
    {

        try
        {
            objIndent = new IndentBL();
            DataSet dsProjBud = new DataSet();

            objIndent.BudgetSector = ddlBudgetSector.SelectedValue;
            objIndent.Project_Code = ddlProjectName.SelectedValue;
            if (objIndent.load(con, IndentBL.eLoadSp.GET_PROJECT_BUDGET_QTY, ref dsProjBud))
            {

                if (dsProjBud.Tables[0].Rows.Count > 0)
                {

                    ViewState["ExistingIndentQty"] = dsProjBud.Tables[0].Rows[0]["ExistingIndentQty"].ToString();
                    txtTotalIndentQty.Text = ViewState["ExistingIndentQty"].ToString();
                }
                if(dsProjBud.Tables[1].Rows.Count > 0)
                {
                    ViewState["ProjBudQty"] = dsProjBud.Tables[1].Rows[0]["ProjectBudgetQty"].ToString();
                    txtProjBudQty.Text = ViewState["ProjBudQty"].ToString();
                }
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }


    protected void GetIndentDetails()
    {
        try
        {

            objIndent = new IndentBL();
            DataSet dsin = new DataSet();
            objIndent.Indent_No = Request.QueryString["IndentNo"].ToString();
            objIndent.load(con, IndentBL.eLoadSp.SELECT_INDENTDETAILS_BY_ID, ref dsin);
            if (dsin.Tables[0].Rows.Count > 0)
            {
                ddlProjectName.SelectedValue = dsin.Tables[0].Rows[0]["Project_Code"].ToString();
                BindBudgets();
                ddlBudget.SelectedValue = dsin.Tables[0].Rows[0]["Budget_ID"].ToString();
                txtIndentNo.Text = dsin.Tables[0].Rows[0]["Indent_No"].ToString();
                txtIndDate.Text = dsin.Tables[0].Rows[0]["Ind_date"].ToString();
                ddlPreparedBy.SelectedValue = dsin.Tables[0].Rows[0]["Prep_By"].ToString();
                ddlStckChk.SelectedValue = dsin.Tables[0].Rows[0]["Stock_check_By"].ToString();
                ddlHOApproved.SelectedValue = dsin.Tables[0].Rows[0]["HO_approver"].ToString();
                rblProcessFrom.SelectedValue = dsin.Tables[0].Rows[0]["Process_From"].ToString();
                ddlProcessBy.SelectedValue = dsin.Tables[0].Rows[0]["Process_By"].ToString();
                ddlNameofSite.SelectedValue = dsin.Tables[0].Rows[0]["Location"].ToString();
                rd_Status.SelectedValue = dsin.Tables[0].Rows[0]["Status"].ToString();

                txtRemarks.Text = dsin.Tables[0].Rows[0]["Remark"].ToString();
                txtnote.Text=dsin.Tables[0].Rows[0]["NOTE"].ToString();
                txtIndentNo.Enabled = false;
                btnSubmit.Text = "Update";
                btnAddItem.Visible = true;
                btnReferBudgetItems.Visible = true;
                btnProceed.Visible = true;
                btnPrint.Visible = true;
                BindIndentItems();
            }
        }

        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (rblProcessFrom.SelectedValue.Trim() == "HO" && ddlProcessBy.SelectedValue.Trim() == "-Select-")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select Process By.');", true);
                return;
            }
            objIndent = new IndentBL();
            objIndent.Project_Code = ddlProjectName.SelectedValue.Trim();

            objIndent.Budget_ID = ddlBudget.SelectedValue.Trim();
            //objIndent.Indent_No = txtIndentNo.Text.Trim();
            objIndent.Ind_date = Convert.ToDateTime(txtIndDate.Text.Trim());
            objIndent.Prep_By = Convert.ToInt32(ddlPreparedBy.SelectedValue.Trim());

            if (ddlStckChk.SelectedValue.Trim() != "-Select-")
            {
                objIndent.Stock_check_By = Convert.ToInt16(ddlStckChk.SelectedValue.Trim());
            }
            else
            {
                objIndent.Stock_check_By = null;
            }

            if (ddlHOApproved.SelectedValue.Trim() != "-Select-")
            {
                objIndent.HO_approver = Convert.ToInt16(ddlHOApproved.SelectedValue.Trim());
            }
            else
            {
                objIndent.HO_approver = null;
            }
            objIndent.Process_From = rblProcessFrom.SelectedValue.Trim();
            if (ddlProcessBy.SelectedValue.Trim() != "-Select-")
            {
                objIndent.Process_By = Convert.ToInt32(ddlProcessBy.SelectedValue.Trim());
            }
            else
            {
                objIndent.Process_By = null;
            }
            objIndent.Location = Convert.ToInt32(ddlNameofSite.SelectedValue.Trim());
            objIndent.Status = rd_Status.SelectedValue.Trim();
            objIndent.Remark = txtRemarks.Text.Trim();
            objIndent.NOTE = txtnote.Text.Trim();

            if (btnSubmit.Text == "Submit")
            {

                if (objIndent.insert(con, IndentBL.eLoadSp.INSERT))
                {
                    btnReferBudgetItems.Visible = true;
                    btnAddItem.Visible = true;
                    //btnProceed.Visible = true;
                    btnPrint.Visible = true;
                    btnSubmit.Text = "Update";
                    txtIndentNo.Enabled = false;
                    txtIndentNo.Text = objIndent.Indent_No.ToString();
                    ViewState["IndentNo"] = objIndent.Indent_No.ToString();
                    btnPrint.Target = "_blank";
                    btnPrint.HRef = "Material_Indent_Print.aspx?IndentNo=" + ViewState["IndentNo"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Indent details has been inserted successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Indent No already exists.');", true);
                }
            }
            else
            {
                objIndent.Indent_No = txtIndentNo.Text.Trim();
                if (objIndent.update(con, IndentBL.eLoadSp.UPDATE))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Indent details has been updated successfully.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindBudgets();


            AssetRegistrationBL objAsset = new AssetRegistrationBL();
            ds = new DataSet();
            objAsset.Project_Code = ddlProjectName.SelectedValue;
            objAsset.load(con, AssetRegistrationBL.eLoadSp.PROJECT_BASED_LOCATION, ref ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlNameofSite.SelectedValue = ds.Tables[0].Rows[0]["Location"].ToString();
            }
            else
            {
                ddlNameofSite.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void rblProcessFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblProcessFrom.SelectedValue.Trim() == "HO")
            {
                ddlProcessBy.Enabled = true;
            }
            else
            {
                ddlProcessBy.SelectedIndex = 0;
                ddlProcessBy.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            {
                Response.Redirect("IndentList.aspx", false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void rblBOQ_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblBOQ.SelectedValue.Trim() == "1")
            {
                txtBOQ.Enabled = true;
            }
            else
            {
                txtBOQ.Enabled = false;
                txtBOQ.Text = string.Empty;
            }
            mpeIndentItem.Show();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSaveItem_Click(object sender, EventArgs e)
    {
        try
        {

            if (ViewState["LimitedItems"] != null && Convert.ToInt32(ViewState["LimitedItems"]) > 20 )
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Indent line items is 20 it is exceed pls create a new indent for other items ');", true);
                return;
            }
            


            if ((Convert.ToDecimal(txtReqQty.Text) + Convert.ToDecimal(txtExistingQty.Text)) > Convert.ToDecimal(txtBudgetQty.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Required Qty Should Not Exceed The Budget Qty!');", true);
                return;
            }

            if (txtProjBudQty.Text != string.Empty && (Convert.ToDecimal(txtReqQty.Text) + Convert.ToDecimal(txtTotalIndentQty.Text)) > Convert.ToDecimal(txtProjBudQty.Text))
            {
                if (ddlBudgetSector.SelectedValue == "BOQ Items")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Required Qty Should Not Exceed the Project Budgeted Qty for this Category.!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Required Qty Should Not Exceed the Project Budgeted Qty for this Sector.!');", true);
                }
               
                return;
            }

            objIndent = new IndentBL();
            objIndent.Indent_No = txtIndentNo.Text.Trim();
            objIndent.Mat_Cat_Id = Convert.ToInt32(ddlCategory.SelectedValue.Trim());
            objIndent.Item_Code = ddlItem.SelectedValue.Trim();
            if (rblBOQ.SelectedValue != string.Empty)
            {
                objIndent.BOQ = Convert.ToInt32(rblBOQ.SelectedValue.Trim()) == 1 ? true : false;
            }
            objIndent.BOQ_No = txtBOQ.Text.Trim();
            objIndent.Total_Qty_Invoked = txtTotQty.Text.Trim();
            objIndent.Qty_Available = Convert.ToDecimal(txtAvailQty.Text.Trim());
            objIndent.Qty_required = Convert.ToDecimal(txtReqQty.Text.Trim());
            objIndent.AbstractBudget_ID = Convert.ToInt32(ddlBudget.SelectedValue.Trim());
            objIndent.Total_Qty_Recevied = txtQtyAlready.Text != string.Empty ? Convert.ToDecimal(txtQtyAlready.Text.Trim()) : Convert.ToDecimal(0.00);
            if (txtTenativeDate.Text.Trim() != string.Empty)
            {
                objIndent.Tentative_Date = Convert.ToDateTime(txtTenativeDate.Text.Trim());
            }
            else
            {
                objIndent.Tentative_Date = null;
            }
            ///////////////////////////////////////////////

            objIndent.Recurring = chkRenewable.Checked;
            if (ddlAssetName.SelectedIndex > 0 && chkRenewable.Checked==true)
            objIndent.AssetCode = ddlAssetName.SelectedValue;
            /////////////////////////////////////////////////////
            objIndent.Whether_Req_Qty = RdWhetherRQ.SelectedValue == "1" ? true : false;
            objIndent.Remarks = txtRemark.Text.Trim();

            if (btnSaveItem.Text.Trim() == "Save")
            {
                if (objIndent.IndentIteminsert(con, IndentBL.eLoadSp.INSERT_INDENT_ITEMS))
                {
                    BindIndentItems();
                    ResetIndentItems();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Indent Item details has been saved successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Item Code already exists.');", true);
                    mpeIndentItem.Show();
                }
            }
            else
            {
                objIndent.Indent_Item_Id = Convert.ToInt32(ViewState["IndentItemID"]);
                if (objIndent.updateIndentItem(con, IndentBL.eLoadSp.UPDATE_INDENT_ITEM_BY_ID))
                {
                    BindIndentItems();
                    ResetIndentItems();
                    btnSaveItem.Text = "Save";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Indent Item has been Updated sucessfully.');", true);
                }
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void ResetIndentItems()
    {
        ddlBudgetSector.SelectedValue = ddlBudgetSector.Items.FindByText("-Select-").Value;

        ddlCategory.Items.Clear();
        ddlCategory.Items.Insert(0, "-Select-");
        ddlCategory.SelectedValue = ddlCategory.Items.FindByText("-Select-").Value;
        ddlItem.Items.Clear();
        ddlItem.Items.Insert(0, "-Select-");
        ddlItem.SelectedValue = ddlItem.Items.FindByText("-Select-").Value;
        rblBOQ.ClearSelection();
        txtBOQ.Text = string.Empty;
        txtUOM.Text = string.Empty;
        txtTotQty.Text = string.Empty;
        txtAvailQty.Text = "0.00";
        txtReqQty.Text = "0.00";
        txtBudgetQty.Text = "0.00";
        txtQtyAlready.Text = "0.00";
        txtTenativeDate.Text = string.Empty;
        txtRemark.Text = string.Empty;
        RdWhetherRQ.ClearSelection();
        ddlBudgetSector.Enabled = true;
        ddlCategory.Enabled = true;
        ddlItem.Enabled = true;
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            objIndent = new IndentBL();
            DataSet catds = new DataSet();
            if (ddlCategory.SelectedIndex != 0)
            {
                objIndent.Mat_Cat_Id = Convert.ToInt32(ddlCategory.SelectedValue.Trim());
                objIndent.AbstractBudget_ID = Convert.ToInt32(ddlBudget.SelectedValue.Trim());
                objIndent.load(con, IndentBL.eLoadSp.SELECT_ITEMS_FROM_BUDGET_ITEM, ref catds);
                ddlItem.DataSource = catds;
                ddlItem.DataValueField = "Item_Code";
                ddlItem.DataTextField = "Item_Name";
                ddlItem.DataBind();
                ddlItem.Items.Insert(0, "-Select-");
                mpeIndentItem.Show();
            }
            if (ddlBudgetSector.SelectedValue == "BOQ Items")
            {
                GetCategoryBasedQtyForBOQItems();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void GetCategoryBasedQtyForBOQItems()
    {
        objIndent = new IndentBL();
        DataSet catdsnew = new DataSet();
        if (ddlCategory.SelectedIndex != 0)
        {
            objIndent.Mat_Cat_Id = Convert.ToInt32(ddlCategory.SelectedValue.Trim());
            objIndent.BudgetSector = ddlBudgetSector.SelectedValue.ToString();
            objIndent.Project_Code = ddlProjectName.SelectedValue.ToString();
            objIndent.load(con, IndentBL.eLoadSp.GET_CATEGORY_BASED_QTY_FOR_BOQ_ITEMS, ref catdsnew);

            if (catdsnew.Tables[0].Rows.Count > 0)
            {

                ViewState["CatBasedExistingIndentQty"] = catdsnew.Tables[0].Rows[0]["ExistingCatBasedIndentQty"].ToString();
                txtTotalIndentQty.Text = ViewState["CatBasedExistingIndentQty"].ToString();
            }
            if (catdsnew.Tables[1].Rows.Count > 0)
            {
                ViewState["ProjectBudgetQtyForBOQQty"] = catdsnew.Tables[1].Rows[0]["ProjectBudgetQtyForBOQCatBased"].ToString();
                txtProjBudQty.Text = ViewState["ProjectBudgetQtyForBOQQty"].ToString();
            }
        }
    }

    protected void GetQuantityAvail()
    {
        try
        {
            MINBL objMINBL = new MINBL();

            if (ddlItem.SelectedIndex != 0)
            {
                objMINBL.Item_Code = ddlItem.SelectedValue.ToString();
                //objMINBL.UID = Convert.ToInt32(Session["UID"]);
                objMINBL.BudgetID = Convert.ToInt32(ddlBudget.SelectedValue);
                objMINBL.Project_Code = ddlProjectName.SelectedValue;
                if (objMINBL.load(con, MINBL.eLoadSp.SELECT_UOM_BY_ITEM_CODE))
                {
                    if (objMINBL.Quantity.ToString() != string.Empty)
                    {

                        txtAvailQty.Text = objMINBL.Quantity.ToString();
                    }

                    else
                    {
                        txtAvailQty.Text = "0.00";
                    }
                }

            }
            else
            {

                txtUOM.Text = string.Empty;
                txtAvailQty.Text = string.Empty;
            }
            mpeIndentItem.Show();

        }

        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet dst = new DataSet();
            objIndent = new IndentBL();
            if (ddlItem.SelectedIndex != 0)
            {
                objIndent.Item_Code = ddlItem.SelectedValue.Trim();

                if (chkRenewable.Checked)
                {
                    if (ddlAssetName.SelectedIndex > 0)
                        objIndent.AssetCode = ddlAssetName.SelectedValue.Trim();
                    objIndent.Task = "CalculateTotalRunningHours";
                    objIndent.date = txtIndDate.Text.Trim();
                    if (objIndent.GetTotalRunningHours(con, IndentBL.eLoadSp.GetTotalRunningHours))
                        txtTotalrunninghrs.Text = objIndent.Total_Qty_Invoked;
                    if (objIndent.Total_Qty_Invoked == "")
                        txtTotalrunninghrs.Text = "0";
                    else txtTotalrunninghrs.Text = objIndent.Total_Qty_Invoked;

                    txtUOM.Text = objIndent.UOM.ToString();


                    Totalrunninghrs.Visible = true;
                }
                else
                {
                    Totalrunninghrs.Visible = false;
                }
                if (objIndent.load(con, IndentBL.eLoadSp.SELECT_UOM_BY_ITEMCODE, ref dst))
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        txtUOM.Text = dst.Tables[0].Rows[0]["UOMPrefix"].ToString();
                    }

                    GetQuantityAvail();
                    GetExistingQtyForThisBudget();
                   
                    GetBudgetQuantity();
                    GetMRNReceviedQuantity();
                    decimal ReqQty = 0;
                    decimal ExistingQty = 0;
                    ExistingQty = Convert.ToDecimal(ViewState["ExistingQty"]);
                    //if (txtBudgetQty.Text != string.Empty && Convert.ToDecimal(txtBudgetQty.Text) > Convert.ToDecimal(txtAvailQty.Text))
                    //{

                    //    ReqQty = Convert.ToDecimal(txtBudgetQty.Text) - (Convert.ToDecimal(ExistingQty));
                    //    txtReqQty.Text = ReqQty.ToString();
                    //}
                    //else
                    //{
                    //    ReqQty = (Convert.ToDecimal(txtAvailQty.Text) + Convert.ToDecimal(ExistingQty)) - Convert.ToDecimal(txtBudgetQty.Text.ToString());
                    //    txtReqQty.Text = ReqQty.ToString();
                    //}

                    if (txtBudgetQty.Text != string.Empty && Convert.ToDecimal(txtBudgetQty.Text) > 0)
                    {

                        ReqQty = Convert.ToDecimal(txtBudgetQty.Text) - (Convert.ToDecimal(ExistingQty));
                        txtReqQty.Text = ReqQty.ToString();
                    }
                    else
                    {
                        txtReqQty.Text = "0";
                    }
                }
                else
                {
                    txtUOM.Text = string.Empty;

                }
            }
            else
            {
                txtUOM.Text = string.Empty;
                txtReqQty.Text = "0";
                txtAvailQty.Text = "0";
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void GetExistingQtyForThisBudget()
    {
        IndentBL objIndentItem = new IndentBL();
        DataSet dsIndentItem = new DataSet();
        if (ddlItem.SelectedIndex != 0)
        {
            objIndentItem.Item_Code = ddlItem.SelectedValue.Trim();
            objIndentItem.Indent_No = txtIndentNo.Text.Trim();
            objIndentItem.Project_Code = ddlProjectName.SelectedValue;
            objIndentItem.Budget_ID = ddlBudget.SelectedItem.Text.Trim().ToString();
            objIndentItem.AbstractBudget_ID = ddlBudget.SelectedIndex != 0 ? Convert.ToInt32(ddlBudget.SelectedValue.Trim()) : 0;
            if (objIndentItem.load(con, IndentBL.eLoadSp.SELECT_ALREADY_EXISTING_QTY_FOR_THIS_BUDGET, ref dsIndentItem))
            {

                if (dsIndentItem.Tables.Count > 0)
                {
                    ViewState["ExistingQty"] = dsIndentItem.Tables[0].Rows[0]["ExistingQtyForBudget"].ToString();
                    txtExistingQty.Text = ViewState["ExistingQty"].ToString();
                }
                //else
                //{
                //    txtQtyAlready.Text = "0.00";
                //}
            }
        }
    }
    private void GetMRNReceviedQuantity()
    {
        IndentBL objMRNIndent = new IndentBL();
        DataSet dsMRN = new DataSet();
        if (ddlItem.SelectedIndex != 0)
        {
            objMRNIndent.Item_Code = ddlItem.SelectedValue.Trim();
            objMRNIndent.Indent_No = txtIndentNo.Text.Trim();
            objMRNIndent.Project_Code = ddlProjectName.SelectedValue;
            if (objMRNIndent.load(con, IndentBL.eLoadSp.SELECT_QTY_ALREADY_RECEIVED_IN_MRN, ref dsMRN))
            {

                if (dsMRN.Tables.Count > 0)
                {
                    txtQtyAlready.Text = dsMRN.Tables[0].Rows[0]["QtyAlreadyReceived"].ToString();
                }
                else
                {
                    txtQtyAlready.Text = "0.00";
                }
            }
        }
    }

    private void GetBudgetQuantity()
    {
        IndentBL objBudIndent = new IndentBL();
        DataSet dsBud = new DataSet();
        if (ddlItem.SelectedIndex != 0)
        {
            objBudIndent.Item_Code = ddlItem.SelectedValue.Trim();
            objBudIndent.AbstractBudget_ID = Convert.ToInt32(ddlBudget.SelectedValue);
            if (objBudIndent.load(con, IndentBL.eLoadSp.SELECT_BUDGET_QUANTITY, ref dsBud))
            {

                if (dsBud.Tables.Count > 0)
                {
                    txtBudgetQty.Text = dsBud.Tables[0].Rows[0]["Req_Qty"].ToString();
                }
                else
                {
                    txtBudgetQty.Text = "0.00";
                }
            }
        }
    }

    private void BindIndentItems()
    {
        try
        {
            objIndent = new IndentBL();
            DataSet dsbind = new DataSet();
            objIndent.Indent_No = txtIndentNo.Text.Trim();
            objIndent.load(con, IndentBL.eLoadSp.SELECT_INDENT_ITEMS_BY_INDENT_NO, ref dsbind);
            Grid_IndentItem.DataSource = dsbind;
            Grid_IndentItem.DataBind();

            if(dsbind.Tables[0].Rows.Count > 0 )
            {
                ViewState["LimitedItems"] = dsbind.Tables[0].Rows.Count;
            }
            else
            {
                ViewState["LimitedItems"] = 0;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void lnkbtnIndentItemID_Click(object sender, EventArgs e)
    {
        try
        {

            objIndent = new IndentBL();
            DataSet dsIndItemID = new DataSet();
            ViewState["IndentItemID"] = Convert.ToInt16(((LinkButton)sender).Text.ToString());
            objIndent.Indent_Item_Id = Convert.ToInt16(ViewState["IndentItemID"]);

            objIndent.load(con, IndentBL.eLoadSp.SELECT_INDENT_ITEM_BY_INDENT_ID, ref dsIndItemID);

            if (dsIndItemID.Tables[0].Rows.Count > 0)
            {
                ddlBudgetSector.SelectedValue = dsIndItemID.Tables[0].Rows[0]["Budget_Sector"].ToString();

                BindCategoryDetails();
                ddlCategory.SelectedIndex = -1;
                //ddlCategory.SelectedValue = dsIndItemID.Tables[0].Rows[0]["Mat_Cat_Id"].ToString();
                ddlCategory.Items.FindByValue(dsIndItemID.Tables[0].Rows[0]["Mat_Cat_Id"].ToString()).Selected = true ;

                if (dsIndItemID.Tables[0].Rows[0]["BOQ"].ToString() != string.Empty)
                {
                    rblBOQ.SelectedValue = Convert.ToBoolean(dsIndItemID.Tables[0].Rows[0]["BOQ"]) ? "1" : "0";

                }
                txtBOQ.Text = dsIndItemID.Tables[0].Rows[0]["BOQ_No"].ToString();
                txtUOM.Text = dsIndItemID.Tables[0].Rows[0]["UOM"].ToString();
                txtTotQty.Text = dsIndItemID.Tables[0].Rows[0]["Total_Qty_Involved"].ToString();
                txtAvailQty.Text = dsIndItemID.Tables[0].Rows[0]["Qty_Available"].ToString();
                ViewState["RequiredQty"] = dsIndItemID.Tables[0].Rows[0]["Qty_required"].ToString();
                txtReqQty.Text = dsIndItemID.Tables[0].Rows[0]["Qty_required"].ToString();
                txtTenativeDate.Text = dsIndItemID.Tables[0].Rows[0]["Tentative_Date"].ToString();
                RdWhetherRQ.SelectedValue = dsIndItemID.Tables[0].Rows[0]["Whether_Req_Qty"].ToString();
                txtRemark.Text = dsIndItemID.Tables[0].Rows[0]["Remarks"].ToString();
                btnSaveItem.Text = "Update";
                ddlCategory_SelectedIndexChanged(sender, e);
                ddlItem.SelectedValue = dsIndItemID.Tables[0].Rows[0]["Item_Code"].ToString();
                ddlItem_SelectedIndexChanged(sender, e);
                rblBOQ_SelectedIndexChanged(sender, e);
                ddlCategory.Enabled = false;
                ddlBudgetSector.Enabled = false;
                ddlItem.Enabled = false;
                bool b=Convert.ToBoolean (dsIndItemID.Tables[0].Rows[0]["Recurring"].ToString());
                chkRenewable.Checked = b;
                if (chkRenewable.Checked ==b)
                {
                    TrAutomobiles.Visible = true;
                    BindAssetNamesUpdate();
                    ddlAssetName.SelectedValue = dsIndItemID.Tables[0].Rows[0]["AssetCode"].ToString();
                    Totalrunninghrs.Visible = true;
                    txtTotalrunninghrs.Enabled = true;
                    txtTotalrunninghrs.ReadOnly = false;
                    txtTotalrunninghrs.Text = dsIndItemID.Tables[0].Rows[0]["TotalRunningHrs"].ToString();
                    txtTotalrunninghrs.ReadOnly = true;
                }
                mpeIndentItem.Show();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_IndentItem_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objIndent = new IndentBL();
            objIndent.Indent_Item_Id = Convert.ToInt32(e.Record["Indent_Item_Id"]);

            if (objIndent.delete(con, IndentBL.eLoadSp.DELETE_INDENT_ITEM_BY_ID))
            {
                BindIndentItems();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Indent Item has been deleted successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete Indent Item.');", true);
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }



    protected void Grid_IndentItem_RowCreated(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (IsPostBack && e.Row.RowType == Obout.Grid.GridRowType.Header)
            {

                Qty_Available = 0;
                Qty_Required = 0;

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_IndentItem_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[0].Text = "Total";
            }

            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                Qty_Available += decimal.Parse(e.Row.Cells[8].Text);
                Qty_Required += decimal.Parse(e.Row.Cells[9].Text);

            }
            else if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[8].Text = Qty_Available.ToString();
                Qty_Available = 0;
                e.Row.Cells[9].Text = Qty_Required.ToString();
                Qty_Required = 0;
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnProceed_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["IndentNo"] != null)
            {
                Response.Redirect(string.Format("../Procurement/PurchaseOrder.aspx?IndentNo=" + Request.QueryString["IndentNo"].ToString()), false);
            }

            else
            {
                Response.Redirect(string.Format("../Procurement/PurchaseOrder.aspx?IndentNo=" + txtIndentNo.Text), false);
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    protected void rd_Status_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rd_Status.SelectedValue == "Processed" && btnSubmit.Text == "Update")
            {
                DataSet ds1 = new DataSet();
                DataTable DatafilterDt = new DataTable();
                bool exists;

                if (txtIndentNo.Text != string.Empty)
                {
                    IndentBL objIndent = new IndentBL();

                    objIndent.Indent_No = txtIndentNo.Text.Trim();
                    objIndent.load(con, IndentBL.eLoadSp.SELECT_INDENT_ITEMS_BY_INDENT_NO, ref ds1);
                }

                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    DatafilterDt = ds1.Tables[0];

                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<decimal>("Qty_required").Equals(Convert.ToDecimal("0.00"))).Count() > 0;
                    if (exists)
                    {
                        btnProceed.Visible = false;
                        rd_Status.SelectedValue = "Open";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please make it all item required qty more then 0');", true);
                        return;
                    }
                    exists = false;
                }


                btnProceed.Visible = true;
            }
            else
            {
                btnProceed.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    protected void ddlBudget_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBudget.SelectedIndex != 0)
            {
                BindBudgetSectors();

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
            if (ddlBudgetSector.SelectedIndex != 0)
            {
                BindCategoryDetails();
                GetProjectBudgetQty();
                mpeIndentItem.Show();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    protected void btnCancelItem_Click(object sender, EventArgs e)
    {
        ResetIndentItems();
        btnSaveItem.Text = "Save";
    }

    protected void btnReferBudgetItems_Click(object sender, EventArgs e)
    {

        ModelReferBudgetItems.Show();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            objIndent = new IndentBL();
            DataSet budSectorNameDS = new DataSet();
            if (ddlSectorName.SelectedIndex != 0 && ddlBudget.SelectedIndex != 0)
            {
                objIndent.AbstractBudget_ID = Convert.ToInt32(ddlBudget.SelectedValue);
                objIndent.BudgetSector = ddlSectorName.SelectedValue;
                objIndent.load(con, IndentBL.eLoadSp.SELECT_BUDGET_ITEMS_BY_SECTORNAME, ref budSectorNameDS);

                GridReferBudgetItems.DataSource = budSectorNameDS;
                GridReferBudgetItems.DataBind();
                btnImportReferBudgetItem.Visible = true;

            }
            ModelReferBudgetItems.Show();

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnImportReferBudgetItem_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["LimitedItems"] != null && Convert.ToInt32(ViewState["LimitedItems"]) > 20)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Indent line items is 20 it is exceed pls create a new indent for other items ');", true);
                return;
            }

            if (GridReferBudgetItems.SelectedRecords != null && ViewState["LimitedItems"] != null && (Convert.ToInt32(ViewState["LimitedItems"]) + GridReferBudgetItems.SelectedRecords.Count) > 20)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Indent line items is 20 it is exceed pls create a new indent for other items ');", true);
                return;
            }



            objIndent = new IndentBL();
            if (GridReferBudgetItems.SelectedRecords != null)
            {

                foreach (Hashtable ht in GridReferBudgetItems.SelectedRecords)
                {


                    objIndent.BudgetItemID = Convert.ToInt32(ht["Bud_Item_Id"]);
                    objIndent.Indent_No = txtIndentNo.Text.Trim();
                    objIndent.Item_Code = ht["Item_Code"].ToString();
                    objIndent.Project_Code = ddlProjectName.SelectedValue;
                    objIndent.AbstractBudget_ID = ddlBudget.SelectedIndex != 0 ? Convert.ToInt32(ddlBudget.SelectedValue.Trim()) : 0;
                    if (objIndent.ReferBudgetIteminsert(con, IndentBL.eLoadSp.INSERT_TO_INDENT_ITEM_FROM_BUDGET_ITEM))
                    {
                        //ResetIndentItems();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Budget Item details has been imported successfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('One of the Item already exists.');", true);

                    }
                }
                BindIndentItems();
                GridReferBudgetItems.DataSource = null;
                GridReferBudgetItems.DataBind();
                ddlSectorName.SelectedValue = ddlSectorName.Items.FindByText("-Select-").Value;
                btnImportReferBudgetItem.Visible = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select atleast one record to import.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
        finally
        {
            GridReferBudgetItems.SelectedRecords = null;
        }
    }
    /// ///////////////////////////////////////////
    
    protected void chkRenewable_CheckedChanged(object sender, EventArgs e)
    {
        if (chkRenewable.Checked)
            TrAutomobiles.Visible = true;
        else TrAutomobiles.Visible = false;
        mpeIndentItem.Show();
        BindAssetType();
    }
    protected void ddlAssetType_r_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAssetCategory2();
        mpeIndentItem.Show();
    }

    protected void ddlAssetCate_r_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAssetNames();
        mpeIndentItem.Show();
    }
    protected void BindAssetNames()
    {
        try
        {
            if (ddlAssetCate_r.SelectedIndex > 0)
            {
                AssetTransferBL objassetTransferBL = new AssetTransferBL();
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
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void BindAssetNamesUpdate()
    {
        try
        {
           
                AssetTransferBL objassetTransferBL = new AssetTransferBL();
                DataSet ds = new DataSet();
                objassetTransferBL.task = "Update";
                objassetTransferBL.Project_Code = Session["Project_Code"].ToString();
                objassetTransferBL.load(con, AssetTransferBL.eLoadSp.SELECT_ASSET_NAME_BY_ASSET_CATEGORY, ref ds);

                ddlAssetName.DataSource = ds;
                ddlAssetName.DataTextField = "AssetNameWithCode";
                ddlAssetName.DataValueField = "Code";
                ddlAssetName.DataBind();
                ddlAssetName.Items.Insert(0, "-Select-");

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    private void BindAssetCategory2()
    {
        try
        {
            if (ddlAssetType_r.SelectedIndex != 0)
            {
                AssetRegistrationBL objAsset = new AssetRegistrationBL();
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
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    private void BindAssetType()
    {
        try
        {
            ds = new DataSet();
            AssetRegistrationBL objAssetBL = new AssetRegistrationBL();
            objAssetBL.load(con, AssetRegistrationBL.eLoadSp.SELECT_ASSET_TYPE_ALL, ref ds);

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

    protected void btnMultipitemDelete_Click(object sender, EventArgs e)
    {
        try
        {

            if (Grid_IndentItem.SelectedRecords != null)
            {                
                foreach (Hashtable oRecord in Grid_IndentItem.SelectedRecords)
                {                   


                    objIndent = new IndentBL();
                    objIndent.Indent_Item_Id = Convert.ToInt32(oRecord["Indent_Item_Id"]);
                    objIndent.delete(con, IndentBL.eLoadSp.DELETE_INDENT_ITEM_BY_ID);

                    //if ()
                    //{                        
                        
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete Indent Item.');", true);
                    //}
                }

                BindIndentItems();

                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Indent Item has been deleted successfully.');", true);

                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select anyone items for delete');", true);
            }

        }
        catch(Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }

    //protected void btnAddItem_Click(object sender, EventArgs e)
    //{
    //    BindBudgetSectors();
    //}

    
    ////////////////////////////////////////////////////////////////////////////
}
