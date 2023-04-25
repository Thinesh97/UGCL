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

public partial class PaymentIndent_WOHire_Print : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    DataSet ds = null;
    GeneralClass objGen = new GeneralClass();
    PaymentIndentBL objPaymentIndentBL = null;
    decimal TotalAmt = 0.0m;
    decimal TotalTaxAmt = 0.0m;
    decimal TDSPerc = 0.0m;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UID"] != null)
            {
                if (Request.QueryString["WONo"] != null)
                {
                    GetCompanyDetails();
                    GetWorkOrderDetails(Request.QueryString["WONo"].ToString());
                    BindWOItems();

                    lblAmtAfterTax.Text = (TotalAmt + TotalTaxAmt).ToString();
                    decimal TDSAmt = 0.00m;
                    TDSAmt = Math.Round((TotalAmt * Convert.ToDecimal(TDSPerc) / 100), 2);
                    lblTDSAmt.Text = TDSAmt.ToString();

                    lblGrandTotal.Text = (Math.Round(TotalAmt + TotalTaxAmt - TDSAmt, 2)).ToString();
                    decimal grandTotal = Math.Round(Convert.ToDecimal(lblGrandTotal.Text), MidpointRounding.AwayFromZero);
                    lblAmountInWords.Text = objGen.changeNumericToWordsINR(Convert.ToDouble(grandTotal));

                    if (Request.QueryString["Draft"] != null)
                    {
                        div_Watermark_Draft.Visible = true;
                    }
                }
            }
            else
            {
                Response.Redirect("../CommonPages/Login.aspx", false);
            }
        }
    }

    private void GetCompanyDetails()
    {
        try
        {
            objPaymentIndentBL = new PaymentIndentBL();
            ds = new DataSet();
            objPaymentIndentBL.Task = "GetCompanyDetails";
            objPaymentIndentBL.StateCode = Request.QueryString["State"].ToString();
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.POWO_PRINT, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblAddressLine1_Company.Text = ds.Tables[0].Rows[0]["AddressLine1"].ToString();
                lblAddressLine2_Company.Text = ds.Tables[0].Rows[0]["AddressLine2"].ToString();
                lblGSTIN_Company.Text = ds.Tables[0].Rows[0]["CST_No"].ToString();
                lblTAN_Company.Text = ds.Tables[0].Rows[0]["TAN_No"].ToString();
                lblState_Company.Text = ds.Tables[0].Rows[0]["State_Name"].ToString();
                lblCode_Company.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                lblEmail_Company.Text = ds.Tables[0].Rows[0]["Email_Id"].ToString();

                //Bill Address
                lblAddressLine1_Bill.Text = ds.Tables[0].Rows[0]["AddressLine1"].ToString();
                lblAddressLine2_Bill.Text = ds.Tables[0].Rows[0]["AddressLine2"].ToString();
                lblGSTIN_Bill.Text = ds.Tables[0].Rows[0]["CST_No"].ToString();
                lblTAN_Bill.Text = ds.Tables[0].Rows[0]["TAN_No"].ToString();
                lblState_Bill.Text = ds.Tables[0].Rows[0]["State_Name"].ToString();
                lblCode_Bill.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                lblEmail_Bill.Text = ds.Tables[0].Rows[0]["Email_Id"].ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void GetWorkOrderDetails(string WONo)
    {
        objPaymentIndentBL = new PaymentIndentBL();
        ds = new DataSet();
        objPaymentIndentBL.Task = "GetHODetailsPrint";
        objPaymentIndentBL.StateCode = Request.QueryString["State"].ToString();
        objPaymentIndentBL.POWO_Number = WONo;
        objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.POWO_PRINT, ref ds);
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
            lblPanNo.Text = ds.Tables[0].Rows[0]["Pan_No"].ToString();
            lblDestination.Text = ds.Tables[0].Rows[0]["WorkLocation"].ToString();
            lblJobNo.Text = ds.Tables[0].Rows[0]["Project_Name"].ToString();
            lblJobDesc.Text = ds.Tables[0].Rows[0]["Project_Desc"].ToString();
            div_Watermark_Cancel.Visible = ds.Tables[0].Rows[0]["Status"].ToString() == "Cancelled" ? true : false;
            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Image_FilePath"].ToString()))
            {
                imgBtnDigitalSign.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign1.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign2.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign3.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign4.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign5.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign6.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign7.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign8.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign9.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign.Visible = true;
                ViewState["filePath"] = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
            }
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

            //Tax Calc
            lblTaxableAmt.Text = ds.Tables[0].Rows[0]["TotalQtyAmt"].ToString();
            lblTDSPerc.Text = ds.Tables[0].Rows[0]["TDSPercPrint"].ToString();
            TDSPerc = Convert.ToDecimal(lblTDSPerc.Text);

            //ANNEXURE-A
            lblContactName.Text = ds.Tables[0].Rows[0]["Contact_Name"].ToString();
            lblContactNo.Text = ds.Tables[0].Rows[0]["Contact_No"].ToString();
            lblPaymentTerms.Text = ds.Tables[0].Rows[0]["PaymentTerms"].ToString();
            lblWorkLocation.Text = ds.Tables[0].Rows[0]["WorkLocation"].ToString();
            lblOtherTerms.Text = ds.Tables[0].Rows[0]["Other_Terms"].ToString();
 
            //General Term and conditions
            lblWONo1.Text = ds.Tables[0].Rows[0]["WONo"].ToString();
            lblWONo2.Text = ds.Tables[0].Rows[0]["WONo"].ToString();
            lblWODate1.Text = ds.Tables[0].Rows[0]["WODate"].ToString();
            lblWODate2.Text = ds.Tables[0].Rows[0]["WODate"].ToString();
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
            objPaymentIndentBL = new PaymentIndentBL();
            ds = new DataSet();
            objPaymentIndentBL.Task = "GetHOItemDetailsPrint";
            objPaymentIndentBL.StateCode = Request.QueryString["State"].ToString();
            objPaymentIndentBL.POWO_ID = Convert.ToInt32(ViewState["WOID"]); ;
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.POWO_PRINT, ref ds);

            GridPrint.DataSource = ds;
            GridPrint.DataBind();

            //Tax Calc
            lblIgstPerc.Text = ds.Tables[0].Rows[0]["Igst_Perc"].ToString();
            lblIgstPerc1.Text = ds.Tables[0].Rows[0]["Igst_Perc"].ToString();
            lblIgstAmt.Text = (Math.Round((TotalAmt * Convert.ToDecimal(lblIgstPerc.Text) / 100),2)).ToString();
            lblCgstPerc.Text = ds.Tables[0].Rows[0]["Cgst_Perc"].ToString();
            lblCgstPerc1.Text = ds.Tables[0].Rows[0]["Cgst_Perc"].ToString();
            lblCgstAmt.Text = (Math.Round((TotalAmt * Convert.ToDecimal(lblCgstPerc.Text) / 100), 2)).ToString();
            lblSgstPerc.Text = ds.Tables[0].Rows[0]["Sgst_Perc"].ToString();
            lblSgstPerc1.Text = ds.Tables[0].Rows[0]["Sgst_Perc"].ToString();
            lblSgstAmt.Text = (Math.Round((TotalAmt * Convert.ToDecimal(lblSgstPerc.Text) / 100), 2)).ToString();

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

    decimal Total_Igst_Amt = 0.0m;
    decimal Total_Cgst_Amt = 0.0m;
    decimal Total_Sgst_Amt = 0.0m;
    protected void GridPrint_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.DataItem != null))
        {
            TotalAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total_Amt"));

            //If TDS is calculated after tax then use this
            Total_Igst_Amt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Igst_Amt"));
            Total_Cgst_Amt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Cgst_Amt"));
            Total_Sgst_Amt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Sgst_Amt"));
            TotalTaxAmt = Total_Igst_Amt + Total_Cgst_Amt + Total_Sgst_Amt;
        }
        
        if (e.Row.RowType == DataControlRowType.Footer)
        {
        }
    }

    private void BindTaxGrid()
    {
        try
        {
            //objPaymentIndentBL = new PaymentIndentBL();
            //ds = new DataSet();
            //objPaymentIndentBL.Task = "GetWOTaxPrint";
            //objPaymentIndentBL.StateCode = Request.QueryString["State"].ToString();
            //objPaymentIndentBL.POWO_ID = Convert.ToInt32(ViewState["WOID"]); ;
            //objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.POWO_PRINT, ref ds);

            //objWO.load(con, WorkOrderBL.eLoadSp.SELECT_WO_TAX_BY_WOID_FOR_PRINT, ref ds);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    WO_GridTax.DataSource = ds;
            //    WO_GridTax.DataBind();
            //}
            //else
            //{
            //    WO_GridTax.DataSource = null;
            //    WO_GridTax.DataBind();
            //}
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
}
