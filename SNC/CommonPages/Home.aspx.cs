using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using SNC.ErrorLogger;
using Bussinesslogic;

public partial class Home : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    Category objcategory = new Category();
    DataSet ds = null;
    AssetTransferBL objassetTransferBL = null;
    MINBL objMINBL = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Project_Code"] != "" && Request.QueryString["Project_Code"] != null)
                {
                    Session["Project_Code"] = Request.QueryString["Project_Code"];
                }
                if (Session["UID"] != null)
                {
                    Awaiting_Asset_Count();
                    RecurringItems();
                    CountDieselIssuedandPOProcessed();
                    RecurringItemsPerMonth();
                    CountVendorAssetSubcontractor();
                    //BindBudgetCount();
                    //BindIndent();
                    //BindPoAwaitingApproval();
                    GetCountofAwaitingStockTransfer();
                    BindPaymentIndentList_Verification();
                    BindPaymentIndentList_Approved();

                    BindPaymentIndentList();
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

    private void Awaiting_Asset_Count()
    {
        try
        {
            ds = new DataSet();
            objassetTransferBL = new AssetTransferBL();
            objassetTransferBL.UserID = Convert.ToInt32(Session["UID"]);
            objassetTransferBL.Project_Code = Session["Project_Code"].ToString();
            objassetTransferBL.load(con, AssetTransferBL.eLoadSp.Binding_Awaiting_Asset_Status, ref ds);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int rows = ds.Tables[0].Rows.Count;
                Label1.Text = rows.ToString();
            }
            else
            {
                Label1.Text = "0";
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void GetCountofAwaitingStockTransfer()
    {
        try
        {
            StockTransferBL objStockTranBL = new StockTransferBL();
            DataSet dsST = new DataSet();
            if (Session["UID"] != null)
            {
                objStockTranBL.Stock_Receiver = Convert.ToInt32(Session["UID"]);
                objStockTranBL.FromProjectCode = Session["Project_Code"].ToString();
                objStockTranBL.load(con, StockTransferBL.eLoadSp.SELECT_STOCK_TRANSFER_REQUEST_FOR_USER, ref dsST);
                if (dsST.Tables[0].Rows.Count > 0)
                {
                    int rows = dsST.Tables[0].Rows.Count;
                    Lbl_AwaitingStockTransfer.Text = rows.ToString();
                }
                else
                {
                    Lbl_AwaitingStockTransfer.Text = "0";
                }
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void RecurringItems()
    {
        try
        {
            DataSet dsnew = new DataSet();
            objMINBL = new MINBL();
            objMINBL.Project_Code = Session["Project_Code"].ToString();
            objMINBL.load(con, MINBL.eLoadSp.ASSET_RecurringItems, ref dsnew);
            if (dsnew.Tables[0].Rows.Count > 0)
            {
                int rows = dsnew.Tables[0].Rows.Count;
                lblAssetRecurringItem.Text = rows.ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    //protected void BindPoAwaitingApproval()
    //{
    //    try
    //    {
    //        DataSet ds2 = new DataSet();
    //        PurchaseOrderBL objPO = new PurchaseOrderBL();
    //        objPO.ApprovedBy = Convert.ToInt32(Session["UID"]);
    //        objPO.ProjectCode = Session["Project_Code"].ToString();
    //        objPO.load(con, PurchaseOrderBL.eLoadSp.POAWAITING_APPROVAL, ref ds2);

    //        lblPOwaiting.Text = ds2.Tables[0].Rows.Count.ToString(); 
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
    //    }
    //}

    protected void RecurringItemsPerMonth()
    {
        try
        {
            ds = new DataSet();
            objMINBL = new MINBL();
            objMINBL.Project_Code = Session["Project_Code"].ToString();
            objMINBL.load(con, MINBL.eLoadSp.SELECT_Count_RecurringItems, ref ds);
            lblRecurringItems.Text = ds.Tables[0].Rows[0]["Column1"].ToString();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void CountDieselIssuedandPOProcessed()
    {
        try
        {
            ds = new DataSet();
            objcategory = new Category();

            objcategory.Project_Code = Session["Project_Code"].ToString();
            objcategory.load(con, Category.eLoadSp.Count_Diesel_Issued_and_PO_Processed, ref ds);
            lbtnCountDieselIssued.Text = ds.Tables[0].Rows[0]["Diesel"].ToString();
            lbtnCountPOProcessed.Text = ds.Tables[0].Rows[0]["PO_NO"].ToString();
            lbtnCountApprovedBudget.Text = ds.Tables[0].Rows[0]["ApprovedBudget"].ToString();

            lblUtilisedBudget.Text = ds.Tables[0].Rows[0]["UtilisedBudget"].ToString();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    //protected void BindIndent()
    //{
    //    try
    //    {
    //        IndentBL objIndentBL = new IndentBL();
    //        DataSet ds1 = new DataSet();
    //        objIndentBL.Process_By = Convert.ToInt32(Session["UID"]);
    //        objIndentBL.Project_Code = Session["Project_Code"].ToString();
    //        objIndentBL.load(con, IndentBL.eLoadSp.SELECT_INDENTPROCESS_By_UserID, ref ds1);

    //        lblIndentProcess.Text = ds1.Tables[0].Rows.Count.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
    //    }
    //}

    protected void CountVendorAssetSubcontractor()
    {
        try
        {
            ds = new DataSet();
            objcategory = new Category();
            objcategory.Project_Code = Session["Project_Code"].ToString();
            objcategory.load(con, Category.eLoadSp.COUNT_TOTAL_VENDOR_ASSETS_Subcontractor, ref ds);
            lbtnCountTotalVendor.Text = ds.Tables[0].Rows[0]["TotalVendor"].ToString();
            lbtnCountTotalSubcontractor.Text = ds.Tables[0].Rows[0]["TotalSubcontractor"].ToString();
            lbtnCountTotalassets.Text = ds.Tables[0].Rows[0]["TotalAssets"].ToString();
            lbtnCountRunningProject.Text = ds.Tables[0].Rows[0]["RunningProject"].ToString();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    //protected void BindBudgetCount()
    //{
    //    try
    //    {
    //        DataTable Dt = new DataTable();
    //        DataTable FilteredDt = new DataTable();
    //        BudgetBL objBudgetBL = new BudgetBL();
    //        ds = new DataSet();
    //        objBudgetBL.Project_Code = Session["Project_Code"].ToString();
    //        objBudgetBL.load(con, BudgetBL.eLoadSp.BUDGET_AWAITING_APPROVAL, ref ds);

    //        Dt = ds.Tables[0];
    //        FilteredDt = Dt.Clone();

    //        foreach (DataRow dr in Dt.Rows)
    //        {
    //            if (dr["Report_Person"].ToString().Contains(','))
    //            {
    //                string[] Rep = dr["Report_Person"].ToString().Split(',');

    //                if (Rep.Contains(Session["UID"].ToString()))
    //                {
    //                    FilteredDt.Rows.Add(dr.ItemArray);
    //                }
    //            }
    //            else if (dr["Report_Person"].ToString() == Session["UID"].ToString())
    //            {
    //                FilteredDt.Rows.Add(dr.ItemArray);
    //            }
    //        }

    //        //lblAppBudget.Text = FilteredDt.Rows.Count.ToString();

    //        FilteredDt.Clear();
    //        foreach (DataRow dr in Dt.Rows)
    //        {
    //            if (dr["Primary_Person"].ToString() == Session["UID"].ToString())
    //            {
    //                FilteredDt.Rows.Add(dr.ItemArray);
    //            }
    //        }
    //        //lblAppBudget.Text = (Convert.ToInt32(lblAppBudget.Text) + FilteredDt.Rows.Count).ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
    //    }
    //}

    protected void lbtnTotalassets_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Asset/AssetRegistrationList.aspx", true);
    }

    protected void lbtnPOProcessed_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Procurement/PurchaseOrderList.aspx", true);
    }

    protected void lbtnDieselIssued_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Asset/DieselReport.aspx", true);
    }

    protected void lbtnTotalSubcontractor_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Master/ContractorList.aspx", true);
    }

    protected void lbtnTotalVendor_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Master/VendorList.aspx", true);
    }

    protected void lbtnCountPOProcessed_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Procurement/PurchaseOrderList.aspx", true);
    }

    protected void lbtnCountTotalassets_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Asset/AssetRegistrationList.aspx", true);
    }

    protected void lbtnCountDieselIssued_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Asset/DieselReport.aspx", true);
    }

    protected void lbtnCountTotalSubcontractor_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Master/ContractorList.aspx", true);
    }

    protected void lbtnCountTotalVendor_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Master/VendorList.aspx", true);
    }

    protected void lbtnRunningProject_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Project/ProjectList.aspx", true);
    }

    protected void lbtnCountRunningProject_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Project/ProjectList.aspx", true);
    }

    protected void lbtnCountApprovedBudget_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Budget/BudgetList.aspx", true);
    }

    protected void lbtnApprovedBudget_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Budget/BudgetList.aspx", true);
    }

    protected void BindPaymentIndentList()
    {
        try
        {
            ds = new DataSet();
            DataTable Data = new DataTable();
            PaymentIndentBL objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "GetAllPaymentIndent_Approval";
            objPaymentIndentBL.User_ID = Convert.ToInt32(Session["UID"]);
            objPaymentIndentBL.Project_Code = Session["Project_Code"].ToString();
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_PAYMENT_INDENT_ALL, ref ds);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblPaymentIndWaitingApproval.Text = ds.Tables[0].Rows.Count.ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindPaymentIndentList_Approved()
    {
        try
        {
            ds = new DataSet();
            PaymentIndentBL objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "GetAllPaymentIndent";
            objPaymentIndentBL.Project_Code = Session["Project_Code"].ToString();
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_PAYMENT_INDENT_ALL, ref ds);
            if (ds != null && ds.Tables.Count > 0)
            {
                lblApprovedPaymentInd.Text = ds.Tables[1].Rows.Count.ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindPaymentIndentList_Verification()
    {
        try
        {
            ds = new DataSet();
            PaymentIndentBL objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "GetAllPaymentIndent_Verification";
            objPaymentIndentBL.Project_Code = Session["Project_Code"].ToString();
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_PAYMENT_INDENT_ALL, ref ds);
            if (ds != null && ds.Tables.Count > 0)
            {
                lblPaymentIndWaiting.Text = ds.Tables[0].Rows.Count.ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
}
