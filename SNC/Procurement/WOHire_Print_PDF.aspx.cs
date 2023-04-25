﻿using BusinessLayer;
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

public partial class WOHire_Print_PDF : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    DataSet ds = null;
    WorkOrderBL objWO = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            //if (Session["UID"] != null)
            //{
                if (Request.QueryString["WONo"] != null)
                {
                    GetWorkOrderDetails(Request.QueryString["WONo"].ToString());
                    BindWOItems();
                    BindScopeOfWork();
                }
            //}
            //else
            //{
            //    Response.Redirect("../CommonPages/Login.aspx", false);
            //}
        }
    }

    
    private void GetWorkOrderDetails(string WONo)
    {
        objWO = new WorkOrderBL();
        ds = new DataSet();
        objWO.WONo = WONo;
        objWO.Task = "GetWorkOrderHireDetails";
        objWO.load(con, WorkOrderBL.eLoadSp.SELECT_WODETAILS_BY_WONO, ref ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblWONo.Text = ds.Tables[0].Rows[0]["WONo"].ToString();
            lblWODate.Text = ds.Tables[0].Rows[0]["WODate"].ToString();
            lblSubcontractorName.Text = ds.Tables[0].Rows[0]["Subcon_name"].ToString();
            lblAddline.Text = ds.Tables[0].Rows[0]["Add_Line"].ToString();
            lblConNo.Text = ds.Tables[0].Rows[0]["Con_No"].ToString();
            lblSubcontractorGSTNo.Text = ds.Tables[0].Rows[0]["Regs_No"].ToString();
            lblcity.Text = ds.Tables[0].Rows[0]["City"].ToString();
            lblState.Text = ds.Tables[0].Rows[0]["State"].ToString();
            lblStateCode.Text = ds.Tables[0].Rows[0]["State_Code"].ToString();
            lblPinNo.Text = ds.Tables[0].Rows[0]["Pin"].ToString();
            lblDestination.Text = ds.Tables[0].Rows[0]["WorkLocation"].ToString();
            lblJobNo.Text = ds.Tables[0].Rows[0]["Project_Name"].ToString();
            lblJobDesc.Text = ds.Tables[0].Rows[0]["Project_Desc"].ToString();
            //lblQuotNo.Text = ds.Tables[0].Rows[0]["Quot_No"].ToString();
            //lbldelscheduled.Text = ds.Tables[0].Rows[0]["DeliverySchedule"].ToString();
            //lbldispatchAdvice.Text = ds.Tables[0].Rows[0]["DespatchAdvise"].ToString();
            //lblOtherTerms.Text = ds.Tables[0].Rows[0]["Other_Terms"].ToString();
            //lbltotamt.Text = ds.Tables[0].Rows[0]["TotalQtyAmt"].ToString();
            //lblgrandtotal.Text = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[0]["NetTotalAmt"]), MidpointRounding.AwayFromZero).ToString();

            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ApprovalStatus"].ToString()) && ds.Tables[0].Rows[0]["ApprovalStatus"].ToString() == "Approved")
            {
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Image_FilePath"].ToString()))
                {
                    ImgAuthorisedSign.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                    ImgAuthorisedSign.Visible = true;
                }
            }
            else
            {
                ImgAuthorisedSign.Visible = false;
            }


            //ANNEXURE-A
            lblContactName.Text = ds.Tables[0].Rows[0]["Contact_Name"].ToString();
            lblContactNo.Text = ds.Tables[0].Rows[0]["Contact_No"].ToString();
            lblPaymentTerms.Text = ds.Tables[0].Rows[0]["PaymentTerms"].ToString();
            lblWorkLocation.Text = ds.Tables[0].Rows[0]["WorkLocation"].ToString();
            lblOtherTerms.Text = ds.Tables[0].Rows[0]["Other_Terms"].ToString();
 
            //General Term and conditions
            lblWONo1.Text = ds.Tables[0].Rows[0]["WONo"].ToString();
            lblWODate1.Text = ds.Tables[0].Rows[0]["WODate"].ToString();
            lblContractor.Text = "United Global Corporation Ltd.";
            lblSubcontractor.Text = ds.Tables[0].Rows[0]["Subcon_name"].ToString();
            lblProjectName.Text = ds.Tables[0].Rows[0]["Project_Name"].ToString();
            lblSubcontractor1.Text = ds.Tables[0].Rows[0]["Subcon_name"].ToString();
            
            ViewState["WOID"] = ds.Tables[0].Rows[0]["WO_ID"].ToString();
        }

    }

    public int TruncateDecimal(decimal value, int precision)
    {
        int step = (int)Math.Pow(10, precision);
        int tmp = (int)Math.Truncate(step * value);
        return tmp / step;
    }

    private void BindWOItems()
    {
        try
        {
            GeneralClass objGen = new GeneralClass();
            objWO = new WorkOrderBL();
            ds = new DataSet();
            // objWO.WO_ID=Convert.ToInt32(Request.QueryString["WONo"].ToString());
            objWO.WO_ID = Convert.ToInt32(ViewState["WOID"]);
            objWO.Task = "GetWOHireItemDetails";
            objWO.load(con, WorkOrderBL.eLoadSp.SELECT_WO_ITEMS_BY_WONO_FOR_PRINT, ref ds);

            GridPrint.DataSource = ds;
            GridPrint.DataBind();

            //Tax Calc
            lblToatlAmt.Text = Total_Amt.ToString();
            lblIgstPerc.Text = ds.Tables[0].Rows[0]["Igst_Perc"].ToString();
            lblIgstPerc1.Text = ds.Tables[0].Rows[0]["Igst_Perc"].ToString();
            lblIgstAmt.Text = Total_Igst_Amt.ToString();
            lblCgstPerc.Text = ds.Tables[0].Rows[0]["Cgst_Perc"].ToString();
            lblCgstPerc1.Text = ds.Tables[0].Rows[0]["Cgst_Perc"].ToString();
            lblCgstAmt.Text = Total_Cgst_Amt.ToString();
            lblSgstPerc.Text = ds.Tables[0].Rows[0]["Sgst_Perc"].ToString();
            lblSgstPerc1.Text = ds.Tables[0].Rows[0]["Sgst_Perc"].ToString();
            lblSgstAmt.Text = Total_Sgst_Amt.ToString();
            decimal grandTotal = Convert.ToDecimal(lblToatlAmt.Text) + Convert.ToDecimal(lblIgstAmt.Text) + Convert.ToDecimal(lblCgstAmt.Text) + Convert.ToDecimal(lblSgstAmt.Text);
            lblgrandtotal.Text = grandTotal.ToString();
            decimal gt = Math.Round(grandTotal, MidpointRounding.AwayFromZero);
            lblAmountInWords.Text = objGen.changeNumericToWordsINR(Convert.ToInt32(gt));

            tr_IGST.Visible = Convert.ToDecimal(lblIgstAmt.Text) == 0 ? false : true;
            tr_CGST.Visible = Convert.ToDecimal(lblIgstAmt.Text) == 0 ? true : false;
            tr_SGST.Visible = Convert.ToDecimal(lblIgstAmt.Text) == 0 ? true : false;

            //ANNEXURE-A
            lblDesc.Text = ds.Tables[0].Rows[0]["Item_Type"].ToString();
            if (ds.Tables[0].Rows[0]["Gst_Exist"].ToString() != "")
            {
                lblGst.Text = Convert.ToBoolean(ds.Tables[0].Rows[0]["Gst_Exist"]) == true ? "Inclusive" : "Exclusive";
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    decimal Total_Amt = 0.0m;
    decimal Total_Igst_Amt = 0.0m;
    decimal Total_Cgst_Amt = 0.0m;
    decimal Total_Sgst_Amt = 0.0m;

    protected void GridPrint_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.DataItem != null))
        {
            Total_Amt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total_Amt"));
            Total_Igst_Amt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Igst_Amt"));
            Total_Cgst_Amt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Cgst_Amt"));
            Total_Sgst_Amt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Sgst_Amt"));
        }
        
        if (e.Row.RowType == DataControlRowType.Footer)
        {
        }
    }

    private void BindScopeOfWork()
    {
        try
        {
            GeneralClass objGen = new GeneralClass();
            objWO = new WorkOrderBL();
            ds = new DataSet();
            objWO.WO_ID = Convert.ToInt32(ViewState["WOID"]);
            objWO.Task = "GetWOHireScopeOfWork";
            objWO.load(con, WorkOrderBL.eLoadSp.SELECT_WO_ITEMS_BY_WONO_FOR_PRINT, ref ds);

            GridScopeOfWork.DataSource = ds;
            GridScopeOfWork.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void BindTaxGrid()
    {
        try
        {
            objWO = new WorkOrderBL();
            ds = new DataSet();
            objWO.WO_ID = Convert.ToInt32(ViewState["WOID"].ToString());
            objWO.load(con, WorkOrderBL.eLoadSp.SELECT_WO_TAX_BY_WOID_FOR_PRINT, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                WO_GridTax.DataSource = ds;
                WO_GridTax.DataBind();
            }
            else
            {
                WO_GridTax.DataSource = null;
                WO_GridTax.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void BindWOItemWiseTaxGrid()
    {
        try
        {
            objWO = new WorkOrderBL();
            ds = new DataSet();
            objWO.WO_ID = Convert.ToInt32(ViewState["WOID"].ToString());
            //objWO.load(con, WorkOrderBL.eLoadSp.SELECT_WO_ITEMWISE_TAX_BY_WO_ID_FOR_PRINT, ref ds);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    WO_ItemTaxGrid.DataSource = ds.Tables[0];
            //    WO_ItemTaxGrid.DataBind();
            //}
            //else
            //{
            //    WO_ItemTaxGrid.DataSource = ds.Tables[0];
            //    WO_ItemTaxGrid.DataBind();
            //}
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
}
