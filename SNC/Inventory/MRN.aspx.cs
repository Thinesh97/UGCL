using BusinessLayer;
using SNC.ErrorLogger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tally;
using TallyBridge;
using System.IO;

public partial class MRN : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    IndentBL objIndentBL = null;
    VendorBL objVendorBL = null;
    PurchaseOrderBL objPurchaseOrderBL = null;
    MRNBL objMRNBL = null;
    DataSet ds = null;
    MINBL objMINBL = new MINBL();
    bool TallyEnterie = false;

    DataTable dtRemark = new DataTable();
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    if (ViewState["RemarkConcatenation"] == null)
                    {
                        dtRemark.Columns.Add("MaterialID");
                        dtRemark.Columns.Add("MaterialName");
                        dtRemark.Columns.Add("Qty");
                        dtRemark.Columns.Add("UOM");
                        ViewState["RemarkConcatenation"] = dtRemark;
                    }
                    BindVendorNameList();
                    BindSubContractorList();
                    BindLedgerHead();
                    //DdlBindItems();
                    BindCategoryDetails();
                    div_AllFields.Visible = false;
                    if (Request.QueryString["MRN_No"] != null)
                    {
                        GetMRNDetails(Request.QueryString["MRN_No"].ToString());
                        BindMRNItemGrid();
                    }
                    else if (Request.QueryString["SRN_No"] != null)
                    {
                        GetSRNDetails(Request.QueryString["SRN_No"].ToString());
                        BindMRNItemGrid();
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
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
        }
    }

    protected void BindVendorNameList()
    {
        try
        {
            DataSet dsBindVendor = new DataSet();
            objVendorBL = new VendorBL();
            objVendorBL.load(con, VendorBL.eLoadSp.SELECT_VENDOR_ALL, ref dsBindVendor);
            ddlVendorName.DataSource = dsBindVendor;
            ddlVendorName.DataValueField = "Vendor_ID";
            ddlVendorName.DataTextField = "Vendor_name";
            ddlVendorName.DataBind();
            ddlVendorName.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
        }
    }

    protected void BindSubContractorList()
    {
        try
        {
            ds = new DataSet();
            objMRNBL = new MRNBL();
            objMRNBL.load(con, MRNBL.eLoadSp.SELECT_CONTRACTOR_ALL, ref ds);
            ddlSubContractor.DataSource = ds;
            ddlSubContractor.DataTextField = "Subcon_name";
            ddlSubContractor.DataValueField = "Subcon_ID";
            ddlSubContractor.DataBind();
            ddlSubContractor.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void rblMRNType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            div_AllFields.Visible = true;
            if (rblMRNType.SelectedValue == "PurchaseMRN")
            {
                div_Vendor.Visible = true;
                div_SubCon.Visible = false;
                div_PO.Visible = true;
                div_WO.Visible = false;
                div_upBill.Visible = true;
                GridMRNItem.Visible = true;
                Grid_ServiceMRNItem_WO.Visible = false;
                div_VendorNameLocal.Visible = false;
                MRN_No_Lable.InnerText = "MRN No";
                MRN_Date_Lable.InnerText = "MRN Date";
                btn_AddServiceItem.Visible = false;
            }
            else if (rblMRNType.SelectedValue == "ServiceMRN")
            {
                div_Vendor.Visible = false;
                div_SubCon.Visible = true;
                div_PO.Visible = false;
                div_WO.Visible = true;
                div_upBill.Visible = false;
                GridMRNItem.Visible = false;
                Grid_ServiceMRNItem_WO.Visible = true;
                div_VendorNameLocal.Visible = false;
                MRN_No_Lable.InnerText = "SRN No";
                MRN_Date_Lable.InnerText = "SRN Date";
                btn_AddServiceItem.Visible = false;
                btn_AddLocalItem.Visible = false;
            }
            else
            {
                div_Vendor.Visible = false;
                div_VendorNameLocal.Visible = true;
                div_SubCon.Visible = false;
                div_PO.Visible = false;
                div_WO.Visible = false;
                div_upBill.Visible = true;
                GridMRNItem.Visible = true;
                Grid_ServiceMRNItem_WO.Visible = false;
                MRN_No_Lable.InnerText = "MRN No";
                MRN_Date_Lable.InnerText = "MRN Date";
                btn_AddServiceItem.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
        }
    }

    protected void ddlVendorName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlVendorName.SelectedIndex != 0)
            {
                BindAllPOByVendorID_ForMRN();
                //BindIndentNoByVendorID();
            }
            else
            {
                ddlPONo.SelectedIndex = 0;
                //ddlIndentNo.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
        }
    }

    protected void ddlSubContractor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubContractor.SelectedIndex != 0)
            {
                BindAllWOBySubConID();
            }
            else
            {
                ddlWONo.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
        }
    }

    protected void BindAllPOByVendorID()
    {
        try
        {
            DataSet dsBindPO = new DataSet();
            objMRNBL = new MRNBL();
            objMRNBL.Task = "BindPONoByVendorID";
            objMRNBL.Project_Code = Session["Project_Code"].ToString();
            objMRNBL.Vendor_Id = ddlVendorName.SelectedValue;
            objMRNBL.load(con, MRNBL.eLoadSp.SELECT_ALL_PO_BY_VENDOR_ID, ref dsBindPO);

            if (dsBindPO != null)
            {
                ddlPONo.DataSource = dsBindPO;
                ddlPONo.DataTextField = "PONo";
                ddlPONo.DataValueField = "PONo";
                ddlPONo.DataBind();
                ddlPONo.Items.Insert(0, "-Select-");
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
        }
    }

    protected void BindAllPOByVendorID_ForMRN()
    {
        try
        {
            DataSet dsBindPO = new DataSet();
            objMRNBL = new MRNBL();
            objMRNBL.Task = "BindPONoByVendorID_ForMRN";
            objMRNBL.Project_Code = Session["Project_Code"].ToString();
            objMRNBL.Vendor_Id = ddlVendorName.SelectedValue;
            objMRNBL.load(con, MRNBL.eLoadSp.SELECT_ALL_PO_BY_VENDOR_ID, ref dsBindPO);

            if (dsBindPO != null)
            {
                ddlPONo.DataSource = dsBindPO;
                ddlPONo.DataTextField = "PONo";
                ddlPONo.DataValueField = "PONo";
                ddlPONo.DataBind();
                ddlPONo.Items.Insert(0, "-Select-");
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
        }
    }

    protected void BindAllWOBySubConID()
    {
        try
        {
            DataSet dsWO = new DataSet();
            objMRNBL = new MRNBL();
            objMRNBL.Task = "BindWONoBySubConID";
            objMRNBL.SubContractor_Id = ddlSubContractor.SelectedValue;
            objMRNBL.Project_Code = Session["Project_Code"].ToString();
            objMRNBL.load(con, MRNBL.eLoadSp.SELECT_ALL_WO_BY_SUBCON_ID, ref dsWO);

            ddlWONo.Items.Clear();
            ddlWONo.DataSource = dsWO.Tables[0];
            ddlWONo.DataValueField = "WONo";
            ddlWONo.DataTextField = "WONo";
            ddlWONo.DataBind();
            ddlWONo.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ddlPONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPONo.SelectedIndex != 0)
        {
            BindTransportcost();
            BindPOdate();
            BindPOItems();
        }
        else
        {
            txtPODate.Text = string.Empty;
        }
    }

    protected void BindTransportcost()
    {
        try
        {
            DataSet dsBindtrasnport = new DataSet();
            objPurchaseOrderBL = new PurchaseOrderBL();
            objPurchaseOrderBL.load(con, PurchaseOrderBL.eLoadSp.SELECT_TRASPORT, ref dsBindtrasnport);
            if (dsBindtrasnport.Tables[0].Rows.Count > 0)
            {
                txtTransportationCost.Text = dsBindtrasnport.Tables[0].Rows[0]["Type_Amount"].ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
        }
    }

    private void BindPOdate()
    {
        try
        {
            objMRNBL = new MRNBL();
            DataSet dsPODate = new DataSet();
            dsPODate.Clear();
            objMRNBL.PONo = ddlPONo.SelectedValue;
            objMRNBL.load(con, MRNBL.eLoadSp.SELECT_PROC_PODate_BY_PONo, ref dsPODate);
            if (dsPODate.Tables[0].Rows.Count > 0)
            {
                txtPODate.Text = dsPODate.Tables[0].Rows[0]["PODate"].ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
        }
    }

    private void BindPOItems()
    {
        try
        {
            GridMRNItem.ClearPreviousDataSource();
            GridMRNItem.DataSource = null;
            GridMRNItem.DataBind();

            objMRNBL = new MRNBL();
            DataSet dsPOItems = new DataSet();
            objMRNBL.PONo = ddlPONo.SelectedValue.ToString();
            objMRNBL.load(con, MRNBL.eLoadSp.PROC_SELECT_PONO_ITEMS, ref dsPOItems);

            if (dsPOItems.Tables[0].Rows.Count > 0)
            {
                GridMRNItem.DataSource = dsPOItems.Tables[0];
                GridMRNItem.DataBind();
                GridMRNItem.Visible = true;
            }
            if (Convert.ToBoolean(dsPOItems.Tables[0].Rows[0]["TransportCost_Exists"].ToString()) == true)
            {
                trTransportCost.Visible = true;
            }
            else
            {
                trTransportCost.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
        }
    }

    protected void ddlWONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWONo.SelectedIndex != 0)
        {
            BindWOItems();
        }
        else
        {
            txtWODate.Text = string.Empty;
        }
    }

    private void BindWOItems()
    {
        try
        {
            GridMRNItem.ClearPreviousDataSource();
            GridMRNItem.DataSource = null;
            GridMRNItem.DataBind();

            objMRNBL = new MRNBL();
            DataSet dsWOItems = new DataSet();
            objMRNBL.WONo = ddlWONo.SelectedValue.ToString();
            objMRNBL.Task = "SelectWO_BYID";
            objMRNBL.load(con, MRNBL.eLoadSp.SELECT_WO_ITEMS_BY_WONO, ref dsWOItems);
            if (dsWOItems.Tables[0].Rows.Count != 0)
            {
                txtWODate.Text = dsWOItems.Tables[0].Rows[0]["WODate"].ToString();
            }
            else
            {
                txtWODate.Text = dsWOItems.Tables[1].Rows[0]["WODate"].ToString();
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
        }
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtDate.Text != string.Empty)
            {
                if (txtPODate.Text != string.Empty && Convert.ToDateTime(txtDate.Text) < Convert.ToDateTime(txtPODate.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('MRN date should be equal or above date of PO!');", true);
                    txtDate.Text = string.Empty;
                }
                //else if (txtIndentDate.Text != string.Empty && Convert.ToDateTime(txtDate.Text) < Convert.ToDateTime(txtIndentDate.Text))
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('MRN date should be equal or above date of Indent!');", true);
                //    txtDate.Text = string.Empty;
                //}
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }







    #region INDENT Not in Use

    //protected void BindIndentNoByVendorID()
    //{
    //    try
    //    {
    //        DataSet dsBindIndent = new DataSet();
    //        objMRNBL = new MRNBL();
    //        objMRNBL.Task = "BindIndentNoByVendorID";
    //        objMRNBL.Project_Code = Session["Project_Code"].ToString();
    //        objMRNBL.Vendor_Id = ddlVendorName.SelectedValue;
    //        objMRNBL.load(con, MRNBL.eLoadSp.SELECT_INDENT_BY_VENDOR_ID, ref dsBindIndent);

    //        if (dsBindIndent != null)
    //        {
    //            //ddlIndentNo.DataSource = dsBindIndent;
    //            //ddlIndentNo.DataValueField = "Indent_No";
    //            //ddlIndentNo.DataTextField = "Indent_No";
    //            //ddlIndentNo.DataBind();
    //            //ddlIndentNo.Items.Insert(0, "-Select-");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
    //    }
    //}

    //protected void BindAllIndentNo()
    //{
    //    try
    //    {
    //        DataSet dsIndentNo = new DataSet();
    //        objIndentBL = new IndentBL();
    //        objIndentBL.load(con, IndentBL.eLoadSp.SELECT_INDENT_ALL, ref dsIndentNo);
    //        ddlIndentNo.DataSource = dsIndentNo;
    //        ddlIndentNo.DataValueField = "Indent_No";
    //        ddlIndentNo.DataTextField = "Indent_No";
    //        ddlIndentNo.DataBind();
    //        ddlPONo.Items.Insert(0, "-Select-");
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
    //    }
    //}

    //protected void ddlIndentNo_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlIndentNo.SelectedIndex != 0)
    //    {
    //        GridMRNItem.ClearPreviousDataSource();
    //        GridMRNItem.DataSource = null;
    //        GridMRNItem.DataBind();
    //        ddlVendorName.Enabled = true;
    //        if (Request.QueryString["MRN_No"] != null)
    //        {
    //            BindIndentDate();
    //        }
    //        else
    //        {
    //            BindIndentDate();
    //            BindPO();
    //        }
    //        if (ddlPONo.Items.Count < 2)
    //        {
    //            trTransportCost.Visible = true;
    //        }
    //    }
    //    else
    //    {
    //        txtIndentDate.Text = string.Empty;
    //        trTransportCost.Visible = false;
    //    }
    //}

    //private void BindIndentDate()
    //{
    //    try
    //    {
    //        objMRNBL = new MRNBL();
    //        DataSet dsDate = new DataSet();
    //        objMRNBL.Indent_No = ddlIndentNo.SelectedValue;
    //        objMRNBL.load(con, MRNBL.eLoadSp.SELECT_IndentDate_BY_IndentNo, ref dsDate);
    //        if (dsDate.Tables[0].Rows.Count > 0)
    //        {
    //            txtIndentDate.Text = dsDate.Tables[0].Rows[0]["Ind_date"].ToString();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
    //    }
    //}

    //private void BindPO()
    //{
    //    try
    //    {
    //        GridMRNItem.ClearPreviousDataSource();
    //        GridMRNItem.DataSource = null;
    //        GridMRNItem.DataBind();
    //        objMRNBL = new MRNBL();
    //        DataSet dsPO = new DataSet();
    //        DataTable DatafilterDt;
    //        bool exists;
    //        //objMRNBL.Indent_No = ddlIndentNo.SelectedValue.ToString();

    //        objMRNBL.load(con, MRNBL.eLoadSp.PROC_SELECT_PO_INDENTITEMS, ref dsPO);
    //        if (dsPO.Tables[0].Rows.Count > 0)
    //        {
    //            trTransportCost.Visible = false;
    //            if (dsPO.Tables[0].Select("Status <> 'Close'").Length > 0)
    //            //if (dsPO.Tables[0].Rows[0]["Status"].ToString() != "Close")
    //            {
    //                if (dsPO.Tables[0].Select("Status <> 'Close' AND ApprovalStatus = 'Approved'").Length > 0)
    //                {
    //                    if (dsPO.Tables[0].Rows.Count > 0)
    //                    {
    //                        DatafilterDt = dsPO.Tables[0];

    //                        exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("ApprovalStatus") != null && c.Field<string>("Status") != null && c.Field<string>("ApprovalStatus").Equals("Approved")).Where(c => c.Field<string>("Status").Equals("Processed")).Count() > 0;
    //                        if (exists)
    //                        {
    //                            DataTable IndentNodt = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("ApprovalStatus") != null && c.Field<string>("Status") != null && c.Field<string>("ApprovalStatus").Equals("Approved")).Where(c => c.Field<string>("Status").Equals("Processed"))
    //                                         .CopyToDataTable();

    //                            ddlPONo.DataSource = IndentNodt;
    //                            ddlPONo.DataValueField = "PONo";
    //                            ddlPONo.DataTextField = "PONo";
    //                            ddlPONo.DataBind();

    //                        }
    //                        exists = false;
    //                    }
    //                    //ddlPONo.DataSource = dsPO;
    //                    //ddlPONo.DataValueField = "PONo";
    //                    //ddlPONo.DataTextField = "PONo";
    //                    //ddlPONo.DataBind();
    //                    ddlPONo.Items.Insert(0, "-Select-");
    //                    GridMRNItem.DataSource = null;
    //                    GridMRNItem.DataBind();

    //                    txtPODate.Text = string.Empty;
    //                }
    //                else
    //                {
    //                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('PO is not approved.Please approve the PO & Proceed!!!');", true);
    //                    GridMRNItem.ClearPreviousDataSource();
    //                    GridMRNItem.DataSource = null;
    //                    GridMRNItem.DataBind();
    //                }
    //            }
    //        }
    //        else
    //        {
    //            trTransportCost.Visible = true;
    //            //DataSet dsPO1 = new DataSet();
    //            //objMRNBL.load(con, MRNBL.eLoadSp.SELECT_IndentDate_BY_IndentNo, ref dsPO1);

    //            //if (dsPO1.Tables[0].Rows.Count > 0)
    //            //{
    //            txtPODate.Text = string.Empty;
    //            BindIndentItems();

    //            ddlPONo.Items.Clear();
    //            ddlPONo.Items.Insert(0, "-Select-");
    //            //}

    //            GridMRNItem.Columns[17].Visible = false;
    //            GridMRNItem.Columns[16].Visible = false;
    //            GridMRNItem.DataBind();
    //            //ddlPoNp.Items.Insert(0, "-Select-");

    //            //GridIntendItem.DataSource = ds;
    //            //GridIntendItem.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
    //    }
    //}

    //private void BindIndentItems()
    //{
    //    try
    //    {
    //        GridMRNItem.ClearPreviousDataSource();
    //        GridMRNItem.DataSource = null;
    //        GridMRNItem.DataBind();
    //        objMRNBL = new MRNBL();
    //        DataSet dsIndItems = new DataSet();
    //        //objMRNBL.Indent_No = ddlIndentNo.SelectedValue.ToString();
    //        objMRNBL.load(con, MRNBL.eLoadSp.SELECT_INDENTITEMS_FOR_MRN, ref dsIndItems);
    //        GridMRNItem.DataSource = dsIndItems;
    //        GridMRNItem.DataBind();
    //        GridMRNItem.Columns[16].Visible = false;
    //        GridMRNItem.Columns[17].Visible = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
    //    }
    //}

    #endregion
    protected void GetSRNDetails(string SRN_No)
    {
        try
        {
            objMRNBL = new MRNBL();
            DataSet dsdetails = new DataSet();
            objMRNBL.MRN_No = SRN_No;
            objMRNBL.load(con, MRNBL.eLoadSp.SRN_SELECT_BY_ID, ref dsdetails);
            if (dsdetails.Tables[0].Rows.Count > 0)
            {
                rblMRNType.SelectedValue = dsdetails.Tables[0].Rows[0]["MRN_Type"].ToString();
                rblMRNType_SelectedIndexChanged(null, null);
                rblMRNType.Enabled = false;
                txtMRNNo.Text = dsdetails.Tables[0].Rows[0]["MRN_No"].ToString();
                txtDate.Text = dsdetails.Tables[0].Rows[0]["Date1"].ToString();
                txtBillDate.Text = dsdetails.Tables[0].Rows[0]["Bill_Date1"].ToString();
                ddlVendorName.SelectedValue = dsdetails.Tables[0].Rows[0]["Vendor_Id"].ToString();
                ddlSubContractor.SelectedValue = dsdetails.Tables[0].Rows[0]["SubCon_Id"].ToString();
                ddlVendorName.Enabled = false;
                ddlSubContractor.Enabled = false;
                BindAllPOByVendorID();
                BindAllWOBySubConID();
                ddlPONo.SelectedItem.Text = dsdetails.Tables[0].Rows[0]["PO_No"].ToString();
                ddlWONo.SelectedItem.Text = dsdetails.Tables[0].Rows[0]["WO_No"].ToString();
                rblMRNType.SelectedValue = dsdetails.Tables[0].Rows[0]["MRN_Type"].ToString();
                ddlPONo.Enabled = false;
                ddlWONo.Enabled = false;
                BindTransportcost();
                BindPOdate();
                txtInvoiceNo.Text = dsdetails.Tables[0].Rows[0]["Invoice_No"].ToString();
                txtRemarks.Text = dsdetails.Tables[0].Rows[0]["Remarks"].ToString();
                if (dsdetails.Tables[0].Rows[0]["Ledger_Head"].ToString() != string.Empty)
                {
                    ddlLedgerhead.SelectedItem.Text = dsdetails.Tables[0].Rows[0]["Ledger_Head"].ToString();
                }
                else
                {
                    ddlLedgerhead.SelectedIndex = 0;
                }
                ViewState["Ledger_Head"] = dsdetails.Tables[0].Rows[0]["Ledger_Head"].ToString();

                div_AfterUpload_Invoice.Visible = dsdetails.Tables[0].Rows[0]["File_Invoice"].ToString() != string.Empty ? true : false;
                lnkDownloadFile.Text = dsdetails.Tables[0].Rows[0]["File_Invoice"].ToString();
                div_AfterUpload_WayBill.Visible = dsdetails.Tables[0].Rows[0]["File_WayBill"].ToString() != string.Empty ? true : false;
                lnkDownloadFile_WayBill.Text = dsdetails.Tables[0].Rows[0]["File_WayBill"].ToString();
                div_AfterUpload_Other.Visible = dsdetails.Tables[0].Rows[0]["File_Other"].ToString() != string.Empty ? true : false;
                lnkDownloadFile_Other.Text = dsdetails.Tables[0].Rows[0]["File_Other"].ToString();
                btnSubmit.Text = "Update";

                if (dsdetails.Tables[0].Rows[0]["Entered"].ToString() != "")
                {
                    if (Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["Entered"]) == false)
                    {
                        Btn_Entered.Visible = true;
                    }
                    else
                    {
                        Btn_Entered.Visible = false;
                    }
                }
                else
                {
                    Btn_Entered.Visible = true;
                }
                if (ddlPONo.SelectedIndex == 0)
                {
                    txtTransportationCost.Text = dsdetails.Tables[0].Rows[0]["TransportCost"].ToString();
                }
                if (Request.QueryString["SRN_No"] != null)
                {
                    div_AllFields.Visible = true;
                    if (rblMRNType.SelectedValue == "PurchaseMRN")
                    {
                        div_Vendor.Visible = true;
                        div_SubCon.Visible = false;
                        div_PO.Visible = true;
                        div_WO.Visible = false;
                        div_upBill.Visible = true;
                        GridMRNItem.Visible = true;
                        Grid_ServiceMRNItem_WO.Visible = false;
                        div_VendorNameLocal.Visible = false;
                        MRN_No_Lable.InnerText = "MRN No";
                        MRN_Date_Lable.InnerText = "MRN Date";
                        btn_AddServiceItem.Visible = false;
                    }
                    else if (rblMRNType.SelectedValue == "ServiceMRN")
                    {
                        div_Vendor.Visible = false;
                        div_SubCon.Visible = true;
                        div_PO.Visible = false;
                        div_WO.Visible = true;
                        div_upBill.Visible = false;
                        GridMRNItem.Visible = false;
                        Grid_ServiceMRNItem_WO.Visible = true;
                        div_VendorNameLocal.Visible = false;
                        MRN_No_Lable.InnerText = "SRN No";
                        MRN_Date_Lable.InnerText = "SRN Date";
                        btn_AddServiceItem.Visible = false;
                        btn_AddLocalItem.Visible = false;
                        BindGrid_ServiceMRNItem_WO();
                        btn_AddServiceItem.Visible = true;
                        Btn_Entered.Visible = false;
                    }
                    else
                    {
                        div_Vendor.Visible = false;
                        div_VendorNameLocal.Visible = true;
                        div_SubCon.Visible = false;
                        div_PO.Visible = false;
                        div_WO.Visible = false;
                        div_upBill.Visible = true;
                        GridMRNItem.Visible = true;
                        Grid_ServiceMRNItem_WO.Visible = false;
                        MRN_No_Lable.InnerText = "MRN No";
                        MRN_Date_Lable.InnerText = "MRN Date";
                        btn_AddServiceItem.Visible = false;
                    }

                }
            }
        }
        //else if(){

        //}


        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
        }
    }
    protected void GetMRNDetails(string MRN_No)
    {
        try
        {
            objMRNBL = new MRNBL();
            DataSet dsdetails = new DataSet();
            objMRNBL.MRN_No = MRN_No;
            objMRNBL.load(con, MRNBL.eLoadSp.SELECT_BY_ID, ref dsdetails);
            if (dsdetails.Tables[0].Rows.Count > 0)
            {
                rblMRNType.SelectedValue = dsdetails.Tables[0].Rows[0]["MRN_Type"].ToString();
                rblMRNType_SelectedIndexChanged(null, null);
                rblMRNType.Enabled = false;
                txtMRNNo.Text = dsdetails.Tables[0].Rows[0]["MRN_No"].ToString();
                txtDate.Text = dsdetails.Tables[0].Rows[0]["Date1"].ToString();
                txtBillDate.Text = dsdetails.Tables[0].Rows[0]["Bill_Date1"].ToString();
                ddlVendorName.SelectedValue = dsdetails.Tables[0].Rows[0]["Vendor_Id"].ToString();
                ddlSubContractor.SelectedValue = dsdetails.Tables[0].Rows[0]["SubCon_Id"].ToString();
                ddlVendorName.Enabled = false;
                ddlSubContractor.Enabled = false;
                BindAllPOByVendorID();
                BindAllWOBySubConID();
                ddlPONo.SelectedItem.Text = dsdetails.Tables[0].Rows[0]["PO_No"].ToString();
                ddlWONo.SelectedItem.Text = dsdetails.Tables[0].Rows[0]["WO_No"].ToString();
                rblMRNType.SelectedValue = dsdetails.Tables[0].Rows[0]["MRN_Type"].ToString();
                ddlPONo.Enabled = false;
                ddlWONo.Enabled = false;
                BindTransportcost();
                BindPOdate();
                txtInvoiceNo.Text = dsdetails.Tables[0].Rows[0]["Invoice_No"].ToString();
                txtRemarks.Text = dsdetails.Tables[0].Rows[0]["Remarks"].ToString();
                if (dsdetails.Tables[0].Rows[0]["Ledger_Head"].ToString() != string.Empty)
                {
                    ddlLedgerhead.SelectedItem.Text = dsdetails.Tables[0].Rows[0]["Ledger_Head"].ToString();
                }
                else
                {
                    ddlLedgerhead.SelectedIndex = 0;
                }
                ViewState["Ledger_Head"] = dsdetails.Tables[0].Rows[0]["Ledger_Head"].ToString();

                div_AfterUpload_Invoice.Visible = dsdetails.Tables[0].Rows[0]["File_Invoice"].ToString() != string.Empty ? true : false;
                lnkDownloadFile.Text = dsdetails.Tables[0].Rows[0]["File_Invoice"].ToString();
                div_AfterUpload_WayBill.Visible = dsdetails.Tables[0].Rows[0]["File_WayBill"].ToString() != string.Empty ? true : false;
                lnkDownloadFile_WayBill.Text = dsdetails.Tables[0].Rows[0]["File_WayBill"].ToString();
                div_AfterUpload_Other.Visible = dsdetails.Tables[0].Rows[0]["File_Other"].ToString() != string.Empty ? true : false;
                lnkDownloadFile_Other.Text = dsdetails.Tables[0].Rows[0]["File_Other"].ToString();
                btnSubmit.Text = "Update";

                if (dsdetails.Tables[0].Rows[0]["Entered"].ToString() != "")
                {
                    if (Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["Entered"]) == false)
                    {
                        Btn_Entered.Visible = true;
                    }
                    else
                    {
                        Btn_Entered.Visible = false;
                    }
                }
                else
                {
                    Btn_Entered.Visible = true;
                }

                if (dsdetails.Tables[0].Rows[0]["TransportCost_Exists"].ToString() != string.Empty)
                {
                    if (Convert.ToBoolean(dsdetails.Tables[0].Rows[0]["TransportCost_Exists"].ToString()) != false && dsdetails.Tables[0].Rows[0]["TransportCost"].ToString() != string.Empty)
                    {
                        txtTransportationCost.Text = dsdetails.Tables[0].Rows[0]["TransportCost"].ToString();
                        trTransportCost.Visible = true;
                    }
                    else
                    {
                        trTransportCost.Visible = false;
                    }

                    ViewState["TransportCostExists"] = dsdetails.Tables[0].Rows[0]["TransportCost_Exists"].ToString();
                }
                if (ddlPONo.SelectedIndex == 0)
                {
                    txtTransportationCost.Text = dsdetails.Tables[0].Rows[0]["TransportCost"].ToString();
                }
                if (Request.QueryString["MRN_No"] != null)
                {
                    div_AllFields.Visible = true;
                    if (rblMRNType.SelectedValue == "PurchaseMRN")
                    {
                        div_Vendor.Visible = true;
                        div_SubCon.Visible = false;
                        div_PO.Visible = true;
                        div_WO.Visible = false;
                        div_upBill.Visible = true;
                        GridMRNItem.Visible = true;
                        Grid_ServiceMRNItem_WO.Visible = false;
                        div_VendorNameLocal.Visible = false;
                        MRN_No_Lable.InnerText = "MRN No";
                        MRN_Date_Lable.InnerText = "MRN Date";
                        btn_AddServiceItem.Visible = false;
                    }
                    else if (rblMRNType.SelectedValue == "ServiceMRN")
                    {
                        div_Vendor.Visible = false;
                        div_SubCon.Visible = true;
                        div_PO.Visible = false;
                        div_WO.Visible = true;
                        div_upBill.Visible = false;
                        GridMRNItem.Visible = false;
                        Grid_ServiceMRNItem_WO.Visible = true;
                        div_VendorNameLocal.Visible = false;
                        MRN_No_Lable.InnerText = "SRN No";
                        MRN_Date_Lable.InnerText = "SRN Date";
                        btn_AddServiceItem.Visible = false;
                        btn_AddLocalItem.Visible = false;
                    }
                    else
                    {
                        div_Vendor.Visible = false;
                        div_VendorNameLocal.Visible = true;
                        div_SubCon.Visible = false;
                        div_PO.Visible = false;
                        div_WO.Visible = false;
                        div_upBill.Visible = true;
                        GridMRNItem.Visible = true;
                        Grid_ServiceMRNItem_WO.Visible = false;
                        MRN_No_Lable.InnerText = "MRN No";
                        MRN_Date_Lable.InnerText = "MRN Date";
                        btn_AddServiceItem.Visible = false;
                    }
                    //GetMRNDetails(Request.QueryString["MRN_No"].ToString());
                    BindMRNItemGrid();
                }
            }
        }
        //else if(){

        //}


        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
        }
    }
    protected void BindGrid_ServiceMRNItem_WO()
    {
        try
        {
            DataSet dsBindMRN_S = new DataSet();
            objVendorBL = new VendorBL();
            if (Request.QueryString["SRN_No"].ToString() != "")
            {
                objMRNBL.SRN_No = (Request.QueryString["SRN_No"].ToString());
            }
            else
            {
                objMRNBL.SRN_No = txtMRNNo.Text.ToString();
            }
            objMRNBL.load(con, MRNBL.eLoadSp.SELECT_All_SERVICE_MRN_ITEMS, ref dsBindMRN_S);
            Grid_ServiceMRNItem_WO.DataSource = dsBindMRN_S;
            Grid_ServiceMRNItem_WO.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
        }
    }
    protected void btnSaveServiceMRN_Items_Click(object sender, EventArgs e)
    {
        objMRNBL = new MRNBL();
        objMRNBL.SRN_No = Convert.ToString(txtMRNNo.Text.Trim());
        objMRNBL.Net_Payable_ServiceMRN = Convert.ToString(txtNet_Payable_ServiceMRN.Text.Trim());
        objMRNBL.WO_Description = Convert.ToString(txtWO_Description.Text.Trim());
        if (objMRNBL.insert_ServiceMRN_Items(con, MRNBL.eLoadSp.INSERT_SERVICE_MRN))
        {
            BindGrid_ServiceMRNItem_WO();
            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Service MRN has been added sucessfully');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Service MRN already exists, Pls try another!');", true);
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int isSuccess = 0;
            objMRNBL = new MRNBL();
            objMRNBL.MRN_Type = rblMRNType.SelectedValue.ToString();
            objMRNBL.Date = Convert.ToDateTime(txtDate.Text.Trim());
            objMRNBL.Bill_Date = Convert.ToDateTime(txtBillDate.Text.Trim());
            objMRNBL.Invoice_No = txtInvoiceNo.Text.Trim();
            objMRNBL.Remarks = txtRemarks.Text.Trim();
            objMRNBL.Ledger_Head = ddlLedgerhead.SelectedItem.Text.Trim();
            objMRNBL.TransportCost = txtTransportationCost.Text != string.Empty ? Convert.ToDecimal(txtTransportationCost.Text.Trim()) : 0;
            objMRNBL.UserID = Session["User_ID"].ToString();

            if (fuInvoiceCopy.HasFile)
            {
                objMRNBL.File_Invoice = "_InvoiceCopy.pdf";
            }
            if (fuWay_Bill_Copy.HasFile)
            {
                objMRNBL.File_WayBill = "_WayBillCopy.pdf";
            }
            if (fuOther_PurchaseMRN.HasFile)
            {
                objMRNBL.File_Other = "_Other.pdf";
            }
            if (rblMRNType.SelectedValue == "PurchaseMRN")
            {
                objMRNBL.Task = "Insert_PurchaseMRN";
                if (ddlPONo.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Please Select PO');", true);
                    return;
                }
                objMRNBL.Vendor_Id = ddlVendorName.SelectedValue;
                objMRNBL.PONo = ddlPONo.SelectedItem.Text;

                //objMRNBL.Indent_No = ddlIndentNo.SelectedValue;
                //objBlockRegBO.Block_Type = ddlBlockType.SelectedIndex != 0 ? ddlBlockType.SelectedItem.Text : string.Empty;
                //objMRNBL.Payment_Terms = txtPaymentTerms.Text.Trim();
            }
            else if (rblMRNType.SelectedValue == "ServiceMRN")
            {
                btn_AddServiceItem.Visible = true;
                objMRNBL.Task = "Insert_ServiceMRN";
                if (ddlWONo.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Please Select WO');", true);
                    return;
                }
                objMRNBL.SubContractor_Id = ddlSubContractor.SelectedValue;
                objMRNBL.WONo = ddlWONo.SelectedItem.Text;
            }
            else
            {
                if (((Button)sender).Text == "Submit" && rblMRNType.SelectedValue == "LocalMRN")
                {
                    objMRNBL.Task = "Insert_LocalMRN";
                    objMRNBL.SubContractor_Id = "0";
                    objMRNBL.WONo = "0";
                    //objMRNBL.PONo = "0";
                    //objMRNBL.Indent_No = "0";
                    //objMRNBL.Vendor_Id = "0000";
                    bool Insert = objMRNBL.insert(con, MRNBL.eLoadSp.INSERT_LOCAL_MRN);
                    if (Insert)
                    {
                        txtMRNNo.Text = objMRNBL.MRN_No.ToString();
                        btnSubmit.Text = "Update";
                        isSuccess = 1;
                        btn_AddLocalItem.Visible = true;
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Material  Received Note has been added sucessfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('MRN No already exists, Pls try another!');", true);
                    }
                }
            }


            if (((Button)sender).Text == "Submit" && rblMRNType.SelectedValue != "LocalMRN")
            {
                if (objMRNBL.insert(con, MRNBL.eLoadSp.INSERT))
                {
                    txtMRNNo.Text = objMRNBL.MRN_No.ToString();
                    btnSubmit.Text = "Update";
                    isSuccess = 1;
                    //btn_AddLocalItem.Visible = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Material  Received Note has been added sucessfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('MRN No already exists, Pls try another!');", true);
                }
            }
            else
            {
                objMRNBL.MRN_No = txtMRNNo.Text.Trim();
                if (objMRNBL.update(con, MRNBL.eLoadSp.UPDATE))
                {
                    isSuccess = 1;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('MRN details has been updated');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update the MRN details');", true);
                }
            }

            if (isSuccess == 1)
            {
                ViewState["Ledger_Head"] = ddlLedgerhead.SelectedItem.Text.Trim();
                BindMRNItemGrid();
                if (fuInvoiceCopy.HasFile)
                {
                    fuInvoiceCopy.SaveAs(Server.MapPath("~\\UploadedFiles\\MRN\\" + txtMRNNo.Text + "_InoviceCopy.pdf"));
                    lnkDownloadFile.Text = txtMRNNo.Text + "_InvoiceCopy.pdf";
                    div_AfterUpload_Invoice.Visible = true;
                }
                if (fuWay_Bill_Copy.HasFile)
                {
                    fuWay_Bill_Copy.SaveAs(Server.MapPath("~\\UploadedFiles\\MRN\\" + txtMRNNo.Text + "_WayBillCopy.pdf"));
                    lnkDownloadFile_WayBill.Text = txtMRNNo.Text + "_WayBillCopy.pdf";
                    div_AfterUpload_WayBill.Visible = true;
                }
                if (fuOther_PurchaseMRN.HasFile)
                {
                    fuOther_PurchaseMRN.SaveAs(Server.MapPath("~\\UploadedFiles\\MRN\\" + txtMRNNo.Text + "_Other.pdf"));
                    lnkDownloadFile_Other.Text = txtMRNNo.Text + "_Other.pdf";
                    div_AfterUpload_Other.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
        }
    }

    //private void CheckTransportationExits()
    //{
    //    try
    //    {
    //        objMRNBL = new MRNBL();
    //        if(ddlVendorName.SelectedIndex != 0)
    //        {
    //            objMRNBL.Vendor_Id = ddlVendorName.SelectedValue;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
    //    }
    //}

    private void BindMRNItemGrid()
    {
        try
        {
            objMRNBL = new MRNBL();
            DataSet dsMRN = new DataSet();
            objMRNBL.MRN_No = txtMRNNo.Text.Trim();
            objMRNBL.PONo = ddlPONo.SelectedValue.ToString();
            objMRNBL.load(con, MRNBL.eLoadSp.SELECT_MRNITEMS_BY_MRNNO, ref dsMRN);
            GridMRNItem.ClearPreviousDataSource();
            GridMRNItem.DataSource = null;
            GridMRNItem.DataBind();
            DataTable Dt1 = new DataTable();
            Dt1 = dsMRN.Tables[0].Clone();
            Dt1.Clear();
            //foreach (DataRow row in dsMRN.Tables[0].Rows)
            //{
            //    int POQTY = !string.IsNullOrEmpty(row["PO_Qty"].ToString()) ? Convert.ToInt32(row["PO_Qty"]) : 0;
            //    int MRNPOQTY = !string.IsNullOrEmpty(row["MRNAccepted_Qty"].ToString()) ? Convert.ToInt32(row["MRNAccepted_Qty"]) : 0;
            //    if (MRNPOQTY != POQTY)
            //    {
            //        DataRow newRow1 = Dt1.NewRow();
            //        newRow1.ItemArray = row.ItemArray;
            //        Dt1.Rows.Add(newRow1);
            //    }
            //}
            if (dsMRN.Tables[0].Rows.Count > 0)
            {
                GridMRNItem.DataSource = dsMRN;
                GridMRNItem.DataBind();
                GridMRNItem.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
        }
    }

    protected void MRN_ItemID_Click(object sender, EventArgs e)
    {
        try
        {
            objMRNBL = new MRNBL();
            ds = new DataSet();
            ds.Clear();

            objMRNBL.Material_Item_Id = Convert.ToInt32(((LinkButton)sender).CommandName.ToString());

            if (objMRNBL.Material_Item_Id == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('MRN is not created.');", true);
                return;
            }
            ViewState["MRNItemID"] = Convert.ToInt32(((LinkButton)sender).CommandName.ToString());
            ViewState["MaterialName"] = ((LinkButton)sender).CommandArgument.ToString().Split('#').First();
            ViewState["UOM"] = ((LinkButton)sender).CommandArgument.ToString().Split('#').Last();
            //   ----------------------
            if (ddlPONo.SelectedItem.Text == "-Select-")
            {
                objMRNBL.load(con, MRNBL.eLoadSp.SELECT_MRN_ITEM_BY_MATERIAL_ITEM_ID, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtCategory1.Text = ds.Tables[0].Rows[0]["Category_Name"].ToString();
                    txtItem1.Text = ds.Tables[0].Rows[0]["Item_Name"].ToString();
                    txtIndenQty1.Text = ds.Tables[0].Rows[0]["Indent_Qty"].ToString();
                    HF_Cat_ID.Value = ds.Tables[0].Rows[0]["Mat_cat_ID"].ToString();

                    txtreqquantity.Text = ds.Tables[0].Rows[0]["Received_Qty"].ToString();
                    txtamount.Text = ds.Tables[0].Rows[0]["Amount"].ToString();
                    txtTaxPrecentLocal.Text = ds.Tables[0].Rows[0]["TotalTax"].ToString();
                    txtTaxAmt.Text = ds.Tables[0].Rows[0]["TaxAmt"].ToString();
                    txtDisAmt.Text = ds.Tables[0].Rows[0]["DiscountAmt"].ToString();
                    txtDisPrecent.Text = ds.Tables[0].Rows[0]["DiscountPerc"].ToString();
                    txtPrice.Text = ds.Tables[0].Rows[0]["Price_Local_MRN"].ToString();
                    txtItemTransportationCostLocal.Text = ds.Tables[0].Rows[0]["Item_TransportCost"].ToString();


                    objMRNBL.MRN_No = txtMRNNo.Text;
                    objMRNBL.load(con, MRNBL.eLoadSp.SELECT_Transport_Amt, ref ds);
                    if ((Convert.ToDecimal(ds.Tables[0].Rows[0]["TransportCost"].ToString())).Equals(0))
                    {
                        D_Pop_Transportation_Cost.Visible = true;
                    }
                    else
                    {
                        D_Pop_Transportation_Cost.Visible = false;
                        txtItemTransportationCostLocal.Text = "0.00";
                    }
                }
                mpeMRNtemsLocal.Show();
                mpeMRNtems.Hide();
            }
            //     --------------------
            else
            {
                if (ddlPONo.SelectedItem.Text != "-Select-")
                {
                    objMRNBL.PONo = ddlPONo.SelectedValue.ToString();
                }
                objMRNBL.load(con, MRNBL.eLoadSp.SELECT_MRN_ITEM_BY_MATERIAL_ITEM_ID, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtCategory.Text = ds.Tables[0].Rows[0]["Category_Name"].ToString();
                    txtPricePerUnit.Text = ds.Tables[0].Rows[0]["ItemRate"].ToString();
                    txttotal.Text = ds.Tables[0].Rows[0]["TotalAmount"].ToString();
                    txtItem.Text = ds.Tables[0].Rows[0]["Item_Name"].ToString();
                    txtItemCode.Text = ds.Tables[0].Rows[0]["Item_Code"].ToString();//
                    txtMRN_No.Text = ds.Tables[0].Rows[0]["MRN_No"].ToString();//
                    txtTaxPercent.Text = ds.Tables[0].Rows[0]["TotalTax"].ToString();
                    txtItemTransportationCost.Text = ds.Tables[0].Rows[0]["Item_TransportCost"].ToString();
                    txtDiscountAmount.Text = ds.Tables[0].Rows[0]["DiscountAmt"].ToString();
                    txtPOQty.Text = ds.Tables[0].Rows[0]["PO_Qty"].ToString();

                    if (Convert.ToBoolean(ds.Tables[0].Rows[0]["fluct_price"].ToString()) == null)
                    {
                        cbPricefluctUP.Checked = false;
                    }
                    else
                    {
                        cbPricefluctUP.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["fluct_price"].ToString());//by prashanth
                    }

                    txtReceivedQty.Text = ds.Tables[0].Rows[0]["Received_Qty"].ToString();
                    txtAcceptedQty.Text = ds.Tables[0].Rows[0]["Accepted_Qty"].ToString();
                    txtRejectedQty.Text = ds.Tables[0].Rows[0]["Rejected_Qty"].ToString();
                    txtPendingQty.Text = ds.Tables[0].Rows[0]["Pending_Qty"].ToString();

                    if (cbPricefluctUP.Checked == true) //by prashanth
                    {
                        txtPricePerUnit.Enabled = true;

                    }
                    else
                    {
                        txtPricePerUnit.Enabled = false;
                    }

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Qty_Already_Received"].ToString()))
                    {
                        txtAlreadyReceivedQty.Text = ds.Tables[0].Rows[0]["Qty_Already_Received"].ToString();
                    }
                    else
                    {
                        txtAlreadyReceivedQty.Text = "0";
                    }

                    ViewState["ItemCode"] = ds.Tables[0].Rows[0]["Item_Code"].ToString();
                    ViewState["MatCatID"] = ds.Tables[0].Rows[0]["Mat_cat_ID"].ToString();
                    ViewState["MRNNo"] = ds.Tables[0].Rows[0]["MRN_No"].ToString();
                    btnUpdate.Text = "Update";

                    if (ds.Tables[0].Rows[0]["ItemTransCost"] != null && ds.Tables[0].Rows[0]["ItemTransCost"].ToString() != string.Empty && ds.Tables[0].Rows[0]["ItemTransCost"].ToString() != "0.0")
                    {
                        txtItemTransportationCost.Text = ds.Tables[0].Rows[0]["Item_TransportCost"].ToString();
                    }
                    mpeMRNtems.Show();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
        }
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objMRNBL = new MRNBL();

            if ((Convert.ToDecimal(txtReceivedQty.Text) > 0) && (Convert.ToDecimal(txtAcceptedQty.Text) == 0))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Accepted Qty Should not be Zero.!!!');", true);
                return;
            }
            objMRNBL.Material_Item_Id = Convert.ToInt32(ViewState["MRNItemID"]);
            objMRNBL.Received_Qty = txtReceivedQty.Text.Trim() != string.Empty ? Convert.ToDecimal(txtReceivedQty.Text.Trim()) : Convert.ToDecimal(0.00);
            objMRNBL.Accepted_Qty = txtAcceptedQty.Text.Trim() != string.Empty ? Convert.ToDecimal(txtAcceptedQty.Text.Trim()) : Convert.ToDecimal(0.00);
            objMRNBL.Rejected_Qty = txtRejectedQty.Text.Trim() != string.Empty ? Convert.ToDecimal(txtRejectedQty.Text.Trim()) : Convert.ToDecimal(0.00);
            objMRNBL.Pending_Qty = txtPendingQty.Text.Trim() != string.Empty ? Convert.ToDecimal(txtPendingQty.Text.Trim()) : Convert.ToDecimal(0.00);
            objMRNBL.Item_Code = txtItemCode.Text.Trim() != string.Empty ? Convert.ToString(txtItemCode.Text.Trim()) : Convert.ToString("");
            objMRNBL.MRN_No = txtMRN_No.Text.Trim() != string.Empty ? Convert.ToString(txtMRN_No.Text.Trim()) : Convert.ToString("");
            objMRNBL.TaxPercent_MRN = txtTaxPercent.Text.Trim() != string.Empty ? Convert.ToDecimal(txtTaxPercent.Text.Trim()) : Convert.ToDecimal(0.00);
            objMRNBL.Discount_Amount = txtDiscountAmount.Text.Trim() != string.Empty ? Convert.ToDecimal(txtDiscountAmount.Text.Trim()) : Convert.ToDecimal(0.00);
            //objPurchaseOrderBL.Price_fluctuations = cbPricefluctUP.Checked! = string.Empty ? Convert.ToString(txtItemCode.Text.Trim()) : Convert.ToString("");

            objMRNBL.PricePerUnit = txtPricePerUnit.Text.Trim() != string.Empty ? Convert.ToDecimal(txtPricePerUnit.Text.Trim()) : Convert.ToDecimal(0.00);
            objMRNBL.Total_Price = Convert.ToDecimal(objMRNBL.Accepted_Qty * objMRNBL.PricePerUnit);
            if (Convert.ToDecimal(txtItemTransportationCost.Text) > 0 && txtItemTransportationCost.Text.Trim() != string.Empty)
            {
                objMRNBL.Item_TransportCost = txtItemTransportationCost.Text.Trim() != string.Empty ? Convert.ToDecimal(txtItemTransportationCost.Text.Trim()) : Convert.ToDecimal(0.00);
                txtTransportationCost.Text = "0.00";
            }
            else
            {
                objMRNBL.Item_TransportCost = 0;
            }

            if (ViewState["RemarkConcatenation"] != null)
            {
                dt = (DataTable)ViewState["RemarkConcatenation"];
                var query = dt.AsEnumerable().Where(r => r.Field<string>("MaterialID") == ViewState["MRNItemID"].ToString());

                foreach (var row in query.ToList())
                    row.Delete();

                DataRow dr = dt.NewRow();
                dr["MaterialID"] = Convert.ToInt32(ViewState["MRNItemID"]);
                dr["MaterialName"] = ViewState["MaterialName"].ToString();
                dr["Qty"] = objMRNBL.Accepted_Qty.ToString();
                dr["UOM"] = ViewState["UOM"].ToString();
                dt.Rows.Add(dr);

                ViewState["RemarkConcatenation"] = dt;
            }

            txtRemarks.Text = string.Empty;
            txtRemarks.Text = "Being ";
            objMRNBL.percentComplete = Convert.ToDecimal(objMRNBL.Total_Price + objMRNBL.Item_TransportCost);

            objMRNBL.Total_TaxAmount = Convert.ToDecimal((objMRNBL.percentComplete * objMRNBL.TaxPercent_MRN) / 100);
            objMRNBL.Net_MRN_Total = Convert.ToDecimal(objMRNBL.Total_TaxAmount + objMRNBL.percentComplete) - Convert.ToDecimal(objMRNBL.Discount_Amount);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow a in dt.Rows)
                {
                    txtRemarks.Text = txtRemarks.Text + a["MaterialName"].ToString() + " " + a["Qty"].ToString() + " " + a["UOM"].ToString();
                }
            }
            else
            {
                txtRemarks.Text = string.Empty;
            }
            txtRemarks.Text = txtRemarks.Text + " purchased vide bill #" + txtInvoiceNo.Text;
            objMRNBL.UserID = Session["User_ID"].ToString();
            objMRNBL.Net_MRN_Total = (objMRNBL.Total_TaxAmount + objMRNBL.TransportCost + objMRNBL.Total_Price - objMRNBL.Discount_Amount);
            objMRNBL.MRN_Type = rblMRNType.SelectedValue.ToString();
            //objMRNBL.Price_fluctuations = cbPricefluctUP.Checked;

            if (objMRNBL.UpdateMRNItem(con, MRNBL.eLoadSp.PROC_UPDATE_MRN_BY_ID))
            {
                BindMRNItemGrid();
                ClearMRNItem();

                if (ViewState["TransportCostExists"] != null)
                {
                    if (Convert.ToBoolean(ViewState["TransportCostExists"].ToString()) != false)
                    {
                        btnUpdateTransportationCost.Visible = true;
                    }
                    else
                    {
                        btnUpdateTransportationCost.Visible = false;
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('MRN Item details has been updated');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to Update MRN Item details.!);", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBUpdateError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
        }
    }

    protected void btnUpdateLocal_Click(object sender, EventArgs e)
    {
        try
        {
            if ((Convert.ToDecimal(txtreqquantity.Text) > 0) && (Convert.ToDecimal(txtreqquantity.Text) == 0))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Required Qty Should not be Zero.!!!');", true);
                return;
            }
            if ((Convert.ToDecimal(txtreqquantity.Text) > Convert.ToDecimal(txtIndenQty1.Text)))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Required Qty should be less than Indent Qty.!!!');", true);
                return;
            }
            objMRNBL = new MRNBL();
            objMRNBL.Mat_cat_ID = Convert.ToInt32(HF_Cat_ID.Value);
            objMRNBL.Date = Convert.ToDateTime(txtDate.Text.Trim());
            if (Session["Project_Code"] != null)
            {
                objMRNBL.Project_Code = Session["Project_Code"].ToString();
            }
            else
            {
                objMRNBL.Project_Code = "";
            }

            if (Convert.ToDecimal(txtItemTransportationCostLocal.Text) > 0 && txtItemTransportationCostLocal.Text.Trim() != string.Empty)
            {
                objMRNBL.Item_TransportCost = txtItemTransportationCostLocal.Text.Trim() != string.Empty ? Convert.ToDecimal(txtItemTransportationCostLocal.Text.Trim()) : Convert.ToDecimal(0.00);
            }
            else
            {
                objMRNBL.Item_TransportCost = Convert.ToDecimal("0.00");
            }

            objMRNBL.TaxAmt = txtTaxAmt.Text != string.Empty ? Convert.ToDecimal(txtTaxAmt.Text) : Convert.ToDecimal("0.00");
            objMRNBL.TaxPrcent = txtTaxPrecentLocal.Text != string.Empty ? Convert.ToDecimal(txtTaxPrecentLocal.Text) : Convert.ToDecimal("0.00");
            objMRNBL.DisPrcent = txtDisPrecent.Text != string.Empty ? Convert.ToDecimal(txtDisPrecent.Text) : Convert.ToDecimal("0.00");
            objMRNBL.Disamt = txtDisAmt.Text != string.Empty ? Convert.ToDecimal(txtDisAmt.Text) : Convert.ToDecimal("0.00");
            objMRNBL.Amt = Convert.ToDecimal(txtamount.Text);
            objMRNBL.MRN_No = txtMRNNo.Text;
            objMRNBL.Material_Item_Id = Convert.ToInt32(ViewState["MRNItemID"]);
            objMRNBL.load(con, MRNBL.eLoadSp.SELECT_Sector_Amt, ref ds);
            if (ds.Tables[0].Rows[0]["Entry_Check"].ToString() == "1")
            {
                objMRNBL.invoiceAmt = Convert.ToDecimal(ds.Tables[0].Rows[0]["InvoiceAmt"]);
                objMRNBL.TaxAmt = Convert.ToDecimal(ds.Tables[0].Rows[0]["TaxAmt"].ToString());
                objMRNBL.Disamt = Convert.ToDecimal(ds.Tables[0].Rows[0]["DisAmt"].ToString());
                objMRNBL.Received_Qty = txtreqquantity.Text.Trim() != string.Empty ? Convert.ToDecimal(txtreqquantity.Text.Trim()) : Convert.ToDecimal(0.00);
                objMRNBL.Accepted_Qty = txtreqquantity.Text.Trim() != string.Empty ? Convert.ToDecimal(txtreqquantity.Text.Trim()) : Convert.ToDecimal(0.00);
                objMRNBL.Rejected_Qty = Convert.ToDecimal(0.00);
                objMRNBL.Pending_Qty = Convert.ToDecimal(0.00);

                if (ViewState["RemarkConcatenation"] != null)
                {
                    dt = (DataTable)ViewState["RemarkConcatenation"];
                    var query = dt.AsEnumerable().Where(r => r.Field<string>("MaterialID") == ViewState["MRNItemID"].ToString());
                    foreach (var row in query.ToList())
                        row.Delete();

                    DataRow dr = dt.NewRow();
                    dr["MaterialID"] = Convert.ToInt32(ViewState["MRNItemID"]);
                    dr["MaterialName"] = ViewState["MaterialName"].ToString();
                    dr["Qty"] = objMRNBL.Accepted_Qty.ToString();
                    dr["UOM"] = ViewState["UOM"].ToString();
                    dt.Rows.Add(dr);
                    ViewState["RemarkConcatenation"] = dt;
                }

                txtRemarks.Text = string.Empty;
                txtRemarks.Text = "Being ";

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow a in dt.Rows)
                    {
                        txtRemarks.Text = txtRemarks.Text + a["MaterialName"].ToString() + " " + a["Qty"].ToString() + " " + a["UOM"].ToString();
                    }
                }
                else
                {
                    txtRemarks.Text = string.Empty;
                }
                txtRemarks.Text = txtRemarks.Text + " purchased vide bill #" + txtInvoiceNo.Text;
                objMRNBL.Price_Local_MRN = txtPrice.Text == "" ? Convert.ToDecimal(0.00) : Convert.ToDecimal(txtPrice.Text);
                objMRNBL.UserID = Session["User_ID"].ToString();
                if (objMRNBL.UpdateMRNLocalItem(con, MRNBL.eLoadSp.UPDATE_MRNITEM_Local_BY_ID))
                {
                    BindMRNItemGrid();

                    if (ViewState["TransportCostExists"] != null)
                    {
                        if (Convert.ToBoolean(ViewState["TransportCostExists"].ToString()) != false)
                        {
                            btnUpdateTransportationCost.Visible = true;
                        }
                        else
                        {
                            btnUpdateTransportationCost.Visible = false;
                        }
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('MRN Item details has been updated');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to Update MRN Item details.!);", true);
                }
            }
            else if (ds.Tables[0].Rows[0]["Entry_Check"].ToString() == "0")
            {
                mpeMRNtemsLocal.Show();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Sector Amount Exceeding Monthly Budget (Purchased Rs - " + ds.Tables[0].Rows[0]["AlreadyPurchased"].ToString() + ").');", true);
            }
            //else if (ds.Tables[0].Rows[0]["Entry_Check"].ToString() == "2")
            //{
            //    mpeMRNtemsLocal.Show();
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Local purchase is not applicable for this item');", true);
            //}
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBUpdateError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
        }
    }

    protected void GridMRNItem_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objMRNBL = new MRNBL();
            objMRNBL.Material_Item_Id = Convert.ToInt32(e.Record["Material_Item_Id"]);
            if (Session["Project_Code"].ToString() != null)
            {
                objMRNBL.Project_Code = Session["Project_Code"].ToString();
            }

            if (objMRNBL.delete(con, MRNBL.eLoadSp.DELETE_MRNITEM_BY_ID))
            {
                BindMRNItemGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('MRN Item has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Item has been issued.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSubmit.Text == "Submit")
            {
                Response.Redirect("../Inventory/MRN.aspx", false);
            }
            else
            {
                Response.Redirect("../Inventory/MRNList.aspx", false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
                mpeMRNtemsLocalAddItem.Show();
            }
            else
            {
                ddlItem.SelectedValue = ddlItem.Items.FindByText("-Select-").Value;
                ddlItem.Enabled = false;
                // ddlUnit.SelectedIndex = -1;
                //ddlUnit.SelectedValue = ddlUnit.Items.FindByText("-Select-").Value;
                mpeMRNtemsLocalAddItem.Show();
            }
            //txtAvailableQty.Text = string.Empty;
            mpeMRNtemsLocalAddItem.Show();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
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
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
        }

    }

    protected void Btn_AddLocalMRN_Item_Click(object sender, EventArgs e)
    {
        try
        {
            mpeMRNtemsLocalAddItem.Show();
            mpeMRNtems.Hide();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Btn_AddServiceMRN_Item_Click(object sender, EventArgs e)
    {
        try
        {
            ModalPopupMRNItemService.Show();
            //mpeMRNtems.Hide();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void btnUpdateTransportationCost_Click(object sender, EventArgs e)
    {
        try
        {
            objMRNBL = new MRNBL();
            objMRNBL.MRN_No = txtMRNNo.Text.Trim();
            objMRNBL.TransportCost = txtTransportationCost.Text != string.Empty ? Convert.ToDecimal(txtTransportationCost.Text.Trim()) : 0;
            if (objMRNBL.UpdateMRNTransportationCost(con, MRNBL.eLoadSp.UPDATE_TRANSPORTATION_COST))
            {
                BindMRNItemGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Transportation Cost has been updated sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to Update Transportation Cost.!');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBUpdateError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }



    decimal Amount = 0;
    decimal InvoiceAmount = 0;
    decimal PoQty = 0;
    decimal RecievedQty = 0;
    decimal AcceptedQty = 0;
    decimal RejectedQty = 0;
    decimal PendingQty = 0;
    decimal Rate = 0;
    decimal Tax = 0;
    decimal TaxAmt = 0;
    decimal Discount = 0;
    decimal DiscountAmt = 0;
    decimal TransAmt = 0;
    protected void GridMRNItem_RowCreated(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (IsPostBack && e.Row.RowType == Obout.Grid.GridRowType.Header)
            {
                Amount = 0;
                InvoiceAmount = 0;
                PoQty = 0;
                RecievedQty = 0;
                AcceptedQty = 0;
                RejectedQty = 0;
                PendingQty = 0;
                Rate = 0;
                Tax = 0;
                TaxAmt = 0;
                Discount = 0;
                DiscountAmt = 0;
                TransAmt = 0;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void GridMRNItem_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[0].Text = "Total Amount";
            }
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                if (!string.IsNullOrEmpty(e.Row.Cells[8].Text))
                {
                    Amount += decimal.Parse(e.Row.Cells[8].Text);
                    InvoiceAmount += decimal.Parse(e.Row.Cells[13].Text);

                    PoQty += decimal.Parse(e.Row.Cells[2].Text);
                    RecievedQty += decimal.Parse(e.Row.Cells[3].Text);
                    AcceptedQty += decimal.Parse(e.Row.Cells[4].Text);
                    RejectedQty += decimal.Parse(e.Row.Cells[5].Text);
                    PendingQty += decimal.Parse(e.Row.Cells[6].Text);
                    Rate += decimal.Parse(e.Row.Cells[7].Text);
                    Tax += decimal.Parse(e.Row.Cells[9].Text);
                    TaxAmt += decimal.Parse(e.Row.Cells[10].Text);
                    DiscountAmt += decimal.Parse(e.Row.Cells[11].Text);
                    TransAmt += decimal.Parse(e.Row.Cells[12].Text);
                }
            }
            else if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[8].Text = Amount.ToString();
                e.Row.Cells[13].Text = txtTransportationCost.Text == string.Empty || txtTransportationCost.Text == "0" ? InvoiceAmount.ToString() : (Convert.ToDecimal(txtTransportationCost.Text) + InvoiceAmount).ToString();
                ViewState["InvoiceAmt"] = txtTransportationCost.Text == string.Empty || txtTransportationCost.Text == "0" ? InvoiceAmount.ToString() : (Convert.ToDecimal(txtTransportationCost.Text) + InvoiceAmount).ToString();

                e.Row.Cells[2].Text = PoQty.ToString();
                e.Row.Cells[3].Text = RecievedQty.ToString();
                e.Row.Cells[4].Text = AcceptedQty.ToString();
                e.Row.Cells[5].Text = RejectedQty.ToString();
                e.Row.Cells[6].Text = PendingQty.ToString();
                e.Row.Cells[7].Text = Rate.ToString();
                e.Row.Cells[9].Text = Tax.ToString();
                e.Row.Cells[10].Text = TaxAmt.ToString();
                e.Row.Cells[11].Text = DiscountAmt.ToString();
                e.Row.Cells[12].Text = TransAmt.ToString();

                Amount = 0;
                InvoiceAmount = 0;

                PoQty = 0;
                RecievedQty = 0;
                AcceptedQty = 0;
                RejectedQty = 0;
                PendingQty = 0;
                Rate = 0;
                Tax = 0;
                TaxAmt = 0;
                Discount = 0;
                DiscountAmt = 0;
                TransAmt = 0;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Btn_Tally_Entered_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Ledger_Head"] == null || ViewState["Ledger_Head"].ToString() == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please enter Ledger Header and click on Update button');", true);
                TallyEnterie = false;
                return;
            }

            if (txtInvoiceNo.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please enter Invoice No and click on Update button');", true);
                TallyEnterie = false;
                return;
            }

            objMRNBL = new MRNBL();
            objMRNBL.MRN_No = txtMRNNo.Text;
            objMRNBL.entered = 1;
            if (objMRNBL.AutoMationTallyStatus(con, MRNBL.eLoadSp.Tally_AutoMation_flag))
            {
                TallyEnterie = true;
            }

            if (TallyEnterie)
            {
                //System.Diagnostics.Process[] _process = null;
                //_process = System.Diagnostics.Process.GetProcessesByName("TallyBridgeApp");
                //if (_process.Count() == 0)
                //{
                //    var example_process = System.Diagnostics.Process.Start(@"C:\inetpub\wwwroot\WindowsFormsApplication1\TallyBridgeApp\bin\Debug\TallyBridgeApp.exe");
                //}
                Thread.Sleep(12000);
            }

            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("PROC_SELECT_InvoiceAmt_Tally", con);
            SqlDataAdapter objAda = new SqlDataAdapter(cmd);
            objAda.Fill(dt);
            con.Close();

            if (dt.Rows.Count == 0)
            {
                //if (TallyEnterie)
                //{
                //    objMRNBL = new MRNBL();
                //    objMRNBL.MRN_No = txtMRNNo.Text;
                //    objMRNBL.entered = 1;
                //    if (objMRNBL.updateTallyStatus(con, MRNBL.eLoadSp.Update_Tally_Status))
                //    {
                //        Btn_Entered.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('MRN details has been updated sucessfully in Tally');", true);
                //    }
                //    else
                //    {
                //  ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update the MRN details in Tally');", true);
                // }
                //}
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render(' Pls check Ledger head is exists or not if exists pls contact to administrator');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render(' Tally Bridge Tool " + ex.Message.ToString() + "');", true);
        }
    }

    #region Ledger Head

    //protected void BindLedgerHead()
    //{
    //    try
    //    {
    //        ddlLedgerhead.Items.Insert(0, "-Select-");
    //        ddlLedgerhead.Items.Insert(1, "Aggregates");
    //        ddlLedgerhead.Items.Insert(2, "Cement");
    //        ddlLedgerhead.Items.Insert(3, "Consumable");
    //        ddlLedgerhead.Items.Insert(4, "Materials");
    //        ddlLedgerhead.Items.Insert(5, "Sand");
    //        ddlLedgerhead.Items.Insert(6, "Steel");
    //        ddlLedgerhead.Items.Insert(7, "Electrical Maintainence");
    //        ddlLedgerhead.Items.Insert(8, "Fuel & Lubricants");
    //        ddlLedgerhead.Items.Insert(9, "Machinery Maintenance");
    //        ddlLedgerhead.Items.Insert(10, "Vehicle Maintenance");
    //        ddlLedgerhead.Items.Insert(11, "Computer Maintenance");
    //        ddlLedgerhead.Items.Insert(12, "Office Maintenance");
    //        ddlLedgerhead.Items.Insert(13, "Printing & Stationery");
    //        ddlLedgerhead.Items.Insert(14, "Repairs & Maintenance");
    //        ddlLedgerhead.Items.Insert(15, "Staff Welfare");

    //        if (Session["Role"].ToString() == "Application Admin")
    //        {
    //            ddlLedgerhead.Items.Insert(16, "Others");
    //        }

    //        ddlLedgerhead.SelectedIndex = 0;
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}

    protected void BindLedgerHead()
    {
        try
        {
            objMRNBL = new MRNBL();
            DataSet dsLH = new DataSet();
            objMRNBL.Project_Code = Session["Project_Code"].ToString();
            objMRNBL.Task = "GetAllLedgerHead";
            objMRNBL.load(con, MRNBL.eLoadSp.SELECT_LEDGER_HEAD, ref dsLH);
            Grid_LedgerHead.DataSource = dsLH;
            Grid_LedgerHead.DataBind();

            ddlLedgerhead.Items.Clear();
            ddlLedgerhead.DataSource = dsLH;
            ddlLedgerhead.DataValueField = "Ledger_Head_Name";
            ddlLedgerhead.DataTextField = "Ledger_Head_Name";
            ddlLedgerhead.DataBind();
            ddlLedgerhead.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSaveLedgerHead_Click(object sender, EventArgs e)
    {
        try
        {
            objMRNBL = new MRNBL();
            objMRNBL.Ledger_Head = txtLedgerHead.Text;
            objMRNBL.Project_Code = Session["Project_Code"].ToString();

            if (btnSaveLedgerHead.Text == "Save")
            {
                objMRNBL.Task = "InsertLedgerHead";
                if (objMRNBL.insertLedgerHead(con, MRNBL.eLoadSp.INSERT_LEDGER_HEAD))
                {
                    txtLedgerHead.Text = string.Empty;
                    BindLedgerHead();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Ledger Head has been Created');", true);
                    ModelLedgerHeadPopup.Show();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Ledger Head is already exists, Pls try another name');", true);
                    ModelLedgerHeadPopup.Show();
                }
            }
            else
            {
                objMRNBL.Ledger_Head_Old = ViewState["LedgerHeadOld"].ToString();
                objMRNBL.Task = "UpdateLedgerHead";
                if (objMRNBL.updateLedgerHead(con, MRNBL.eLoadSp.UPDATE_LEDGER_HEAD))
                {
                    BindLedgerHead();
                    txtLedgerHead.Text = string.Empty;
                    btnSaveLedgerHead.Text = "Save";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Ledger Head has been updated successfully.');", true);
                    ModelLedgerHeadPopup.Show();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Ledger Head can't be updated,');", true);
                    ModelLedgerHeadPopup.Show();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancelLedgerHead_Click(object sender, EventArgs e)
    {
        txtLedgerHead.Text = string.Empty;
    }

    protected void Grid_LedgerHead_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ds = new DataSet();
            objMRNBL = new MRNBL();
            objMRNBL.Ledger_Head = e.Record["Ledger_Head_Name"].ToString();
            objMRNBL.Project_Code = Session["Project_Code"].ToString();
            objMRNBL.Task = "DeleteLedgerHead";
            if (objMRNBL.delete(con, MRNBL.eLoadSp.DELETE_LEDGER_HEAD))
            {
                BindLedgerHead();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Ledger Head has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Ledger Head is already used in application');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void lnkLedgerHead_Click(object sender, EventArgs e)
    {
        try
        {
            objMRNBL = new MRNBL();
            objMRNBL.Ledger_Head = ((LinkButton)sender).CommandName.ToString();
            ViewState["LedgerHeadOld"] = ((LinkButton)sender).CommandName.ToString();

            txtLedgerHead.Text = objMRNBL.Ledger_Head;
            btnSaveLedgerHead.Text = "Update";
            ModelLedgerHeadPopup.Show();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    #endregion






    protected void lnkDownloadFile_Click(object sender, EventArgs e)
    {
        try
        {
            string filePath = Server.MapPath("~\\UploadedFiles\\MRN\\") + (sender as LinkButton).Text;
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.Flush();
            Response.TransmitFile(filePath);
            //Response.WriteFile(filePath);
            Response.End();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }


    protected void lnkDownloadFile_WayBill_Click(object sender, EventArgs e)
    {
        try
        {
            string filePath = Server.MapPath("~\\UploadedFiles\\MRN\\") + (sender as LinkButton).Text;
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.Flush();
            Response.TransmitFile(filePath);
            //Response.WriteFile(filePath);
            Response.End();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }

    protected void ClearMRNItem()
    {
        txtCategory.Text = "";
        txtItem.Text = "";
        txtPOQty.Text = "";
        txtPricePerUnit.Text = "";
        txttotal.Text = "";
        txtAlreadyReceivedQty.Text = "";
        txtReceivedQty.Text = "";
        txtAcceptedQty.Text = "";
        txtRejectedQty.Text = "";
        txtPendingQty.Text = "";
        txtTaxPercent.Text = "";
        txtItemTransportationCost.Text = "";
        txtDiscountAmount.Text = "";
    }

    protected void lnkDownloadFile_Other_Click(object sender, EventArgs e)
    {
        try
        {
            string filePath = Server.MapPath("~\\UploadedFiles\\MRN\\") + (sender as LinkButton).Text;
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.Flush();
            Response.TransmitFile(filePath);
            //Response.WriteFile(filePath);
            Response.End();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }
    //protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        objMINBL = new MINBL();
    //        DataSet dsItem = new DataSet();
    //        if (ddlItem.SelectedIndex != 0)
    //        {
    //            objMINBL.Item_Code = ddlItem.SelectedValue.ToString();
    //            objMINBL.Project_Code = ddlProjectName.SelectedValue;
    //            objMINBL.DATE = Convert.ToDateTime(txtDate.Text);
    //            if (objMINBL.load(con, MINBL.eLoadSp.SELECT_AVLQTY_FROM_STOCK, ref dsItem))
    //            {
    //                //if (dsItem.Tables[0].Rows.Count > 0)
    //                //{
    //                //    txtAvailableQty.Text = dsItem.Tables[0].Rows[0]["AvailableQty"].ToString();
    //                //}

    //                if (dsItem.Tables[1].Rows.Count > 0)
    //                {
    //                    ddlUnit.DataSource = dsItem.Tables[1];
    //                    ddlUnit.DataTextField = "UOMPrefix";
    //                    ddlUnit.DataValueField = "UOM_ID";
    //                    ddlUnit.DataBind();
    //                    ddlUnit.Items.Insert(0, "-Select-");
    //                    ddlUnit.SelectedValue = dsItem.Tables[1].Rows[0]["UOM_ID"].ToString();
    //                }
    //                else
    //                {
    //                    ddlUnit.SelectedIndex = -1;
    //                    txtAvailableQty.Text = "0";
    //                }
    //            }
    //        }
    //        else
    //        {
    //            ddlUnit.SelectedIndex = -1;
    //            txtAvailableQty.Text = "0";
    //        }
    //        ModelMINPopup.Show();

    //    }
    //    catch (Exception ex)
    //    {

    //        ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);
    //    }
    //}
}

