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

public partial class PO_Print_PDF : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    DataSet ds = null;
    PurchaseOrderBL objPO = null;
    int result;
    protected void Page_Load(object sender, EventArgs e)
    {
        GeneralClass objGen = new GeneralClass();
        if (!IsPostBack)
        {
            //if (Session["UID"] != null)
            //{
                if (Request.QueryString["PONo"] != null)
                {
                    //div_Watermark.Visible = false;
                    GetPurchaseOrderDetails(Request.QueryString["PONo"].ToString());
                    BindTaxGrid();
                    BindPOItemWiseTaxGrid();

                    //lblGransTotalAmt.Text = Convert.ToString(TotalItemAmount + TotalItemSpecialAmount + TotalSampleAmount - TotalItemDiscountAmount);
                    decimal grandTotal = Math.Round(Convert.ToDecimal(lblgrandtotal.Text), MidpointRounding.AwayFromZero) ;
                    lblAmountInWords.Text = objGen.changeNumericToWordsINR(Convert.ToDouble(grandTotal));
                }
            //}
            //else
            //{
            //    Response.Redirect("../CommonPages/Login.aspx", false);
            //}
        }
    }

    
    private void GetPurchaseOrderDetails(string PONo)
    {
        objPO = new PurchaseOrderBL();
        ds = new DataSet();
        objPO.PONo = PONo;
        objPO.load(con, PurchaseOrderBL.eLoadSp.SELECT_PODETAILS_BY_PONO, ref ds);
        if (ds.Tables[0].Rows.Count > 0)
        { 
            lbindentNo.Text = ds.Tables[0].Rows[0]["IndentNo"].ToString();
            lbindentDate.Text = ds.Tables[0].Rows[0]["Indent_Date"].ToString();
            
            lblvendorname.Text = ds.Tables[0].Rows[0]["Vendor_name"].ToString();
            lblAddline.Text = ds.Tables[0].Rows[0]["Add_Line"].ToString();
            lblConNo.Text = ds.Tables[0].Rows[0]["Con_No"].ToString();
            lblVendorTINNo.Text = ds.Tables[0].Rows[0]["Regs_No"].ToString();
            lblcity.Text = ds.Tables[0].Rows[0]["City"].ToString();
            lblState.Text = ds.Tables[0].Rows[0]["State"].ToString();
            lblStateCode.Text = ds.Tables[0].Rows[0]["State_Code"].ToString();
            //lblcountry.Text = ds.Tables[0].Rows[0]["Country"].ToString();
            lblPinNo.Text = ds.Tables[0].Rows[0]["Pin"].ToString();
            lblGSTIN.Text = ds.Tables[0].Rows[0]["TIN_No"].ToString();
            lblDestination.Text = ds.Tables[0].Rows[0]["Address_Line1"].ToString();
            lblpurchaseorderid.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
            lblQuotNo.Text = ds.Tables[0].Rows[0]["Quot_No"].ToString();
            lblQuotationRefDate.Text = ds.Tables[0].Rows[0]["VendorRef"].ToString();
            lbldelscheduled.Text = ds.Tables[0].Rows[0]["DeliverySchedule"].ToString();
            lbldispatchAdvice.Text = ds.Tables[0].Rows[0]["DespatchAdvise"].ToString();
            lblOtherTerms.Text = ds.Tables[0].Rows[0]["Other_Terms"].ToString();
            lbldate.Text = ds.Tables[0].Rows[0]["PODate"].ToString();
            lblJobNo.Text = ds.Tables[0].Rows[0]["Project_Code"].ToString();
            lblJobDesc.Text = ds.Tables[0].Rows[0]["Description"].ToString();

            //result = TruncateDecimal(Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalQtyAmt"].ToString()), 2);
            //lbltotamt.Text = result.ToString();
            //result = TruncateDecimal(Convert.ToDecimal(ds.Tables[0].Rows[0]["NetTotalAmt"].ToString()), 2);
            //lblgrandtotal.Text = result.ToString();
            lbltotamt.Text = ds.Tables[0].Rows[0]["TotalAmtWithTax"].ToString();
            lblgrandtotal.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalAmtWithTaxAndPOTax"]).ToString();

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
            lblDeliveryAdd.Text = ds.Tables[0].Rows[0]["DespatchAdvise"].ToString();
            lblContactName.Text = ds.Tables[0].Rows[0]["Contact_Name"].ToString();
            lblContactNo.Text = ds.Tables[0].Rows[0]["Contact_No"].ToString();
            lblGst.Text = ds.Tables[0].Rows[0]["AllTypeCharges"].ToString().Contains("Tax") ? "Inclusive" : "Exclusive";
            lblTransportation.Text = ds.Tables[0].Rows[0]["AllTypeCharges"].ToString().Contains("Transport") ? "Inclusive" : "Exclusive";
            lblPackingForwarding.Text = ds.Tables[0].Rows[0]["AllTypeCharges"].ToString().Contains("PackingForwardingCost") ? "Applicable" : "Not Applicable";
            lblPaymentTerms.Text = ds.Tables[0].Rows[0]["PaymentTerms"].ToString();

            ViewState["POID"] = ds.Tables[0].Rows[0]["PO_ID"].ToString();
            BindPOItems();
        }

    }

    public int TruncateDecimal(decimal value, int precision)
    {
        int step = (int)Math.Pow(10, precision);
        int tmp = (int)Math.Truncate(step * value);
        return tmp / step;
    }

    private void BindPOItems()
    {
        try
        {
            objPO = new PurchaseOrderBL();
            ds = new DataSet();
            // objPO.PO_ID=Convert.ToInt32(Request.QueryString["PONo"].ToString());
            objPO.PO_ID = Convert.ToInt32(ViewState["POID"]);
            objPO.load(con, PurchaseOrderBL.eLoadSp.SELECT_PO_ITEMS_BY_PONO_FOR_PRINT, ref ds);

            GridPrint.DataSource = ds;
            GridPrint.DataBind();
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
            objPO = new PurchaseOrderBL();
            ds = new DataSet();
            objPO.PO_ID = Convert.ToInt32(ViewState["POID"].ToString());
            objPO.load(con, PurchaseOrderBL.eLoadSp.SELECT_PO_TAX_BY_POID_FOR_PRINT, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                PO_GridTax.DataSource = ds;
                PO_GridTax.DataBind();
            }
            else
            {
                PO_GridTax.DataSource = null;
                PO_GridTax.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void BindPOItemWiseTaxGrid()
    {
        try
        {
            objPO = new PurchaseOrderBL();
            ds = new DataSet();
            objPO.PO_ID = Convert.ToInt32(ViewState["POID"].ToString());
            objPO.load(con, PurchaseOrderBL.eLoadSp.SELECT_PO_ITEMWISE_TAX_BY_PO_ID_FOR_PRINT, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                PO_ItemTaxGrid.DataSource = ds.Tables[0];
                PO_ItemTaxGrid.DataBind();
            }
            else
            {
                PO_ItemTaxGrid.DataSource = ds.Tables[0];
                PO_ItemTaxGrid.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
}
