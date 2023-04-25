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

public partial class PaymentIndent_PO_Print : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    DataSet ds = null;
    PaymentIndentBL objPaymentIndentBL = null;
    decimal TotalAmt = 0.00m;
    decimal TDSPerc = 0.00m;
    decimal TotalTaxAmt = 0.00m;
    int TotalCharLength = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        GeneralClass objGen = new GeneralClass();
        if (!IsPostBack)
        {
            if (Session["UID"] != null)
            {
                if (Request.QueryString["State"] != null && Request.QueryString["PONo"] != null)
                {
                    GetCompanyDetails();
                    GetPurchaseOrderDetails(Request.QueryString["PONo"].ToString());
                    BindPOItems();
                    BindTaxGrid();
                    //BindPOItemWiseTaxGrid();

                    lblAmtAfterTax1.Text = (TotalAmt + TotalTaxAmt).ToString();
                    lblAmtAfterTax2.Text = (TotalAmt + TotalTaxAmt).ToString();
                    lblAmtAfterTax3.Text = (TotalAmt + TotalTaxAmt).ToString();
                    lblAmtAfterTax4.Text = (TotalAmt + TotalTaxAmt).ToString();
                    lblAmtAfterTax5.Text = (TotalAmt + TotalTaxAmt).ToString();

                    decimal TDSAmt = 0.00m;
                    TDSAmt = Math.Round((TotalAmt * Convert.ToDecimal(TDSPerc) / 100), 2);
                    lblTDSAmt1.Text = TDSAmt.ToString();
                    lblTDSAmt2.Text = TDSAmt.ToString();
                    lblTDSAmt3.Text = TDSAmt.ToString();
                    lblTDSAmt4.Text = TDSAmt.ToString();
                    lblTDSAmt5.Text = TDSAmt.ToString();

                    lblGrandTotal1.Text = (TotalAmt + TotalTaxAmt - TDSAmt).ToString();
                    lblGrandTotal2.Text = (TotalAmt + TotalTaxAmt - TDSAmt).ToString();
                    lblGrandTotal3.Text = (TotalAmt + TotalTaxAmt - TDSAmt).ToString();
                    lblGrandTotal4.Text = (TotalAmt + TotalTaxAmt - TDSAmt).ToString();
                    lblGrandTotal5.Text = (TotalAmt + TotalTaxAmt - TDSAmt).ToString();

                    decimal grandTotal = Math.Round(Convert.ToDecimal(lblGrandTotal1.Text), MidpointRounding.AwayFromZero) ;
                    lblAmountInWords1.Text = objGen.changeNumericToWordsINR(Convert.ToDouble(grandTotal));
                    lblAmountInWords2.Text = objGen.changeNumericToWordsINR(Convert.ToDouble(grandTotal)); 
                    lblAmountInWords3.Text = objGen.changeNumericToWordsINR(Convert.ToDouble(grandTotal));
                    lblAmountInWords4.Text = objGen.changeNumericToWordsINR(Convert.ToDouble(grandTotal));
                    lblAmountInWords5.Text = objGen.changeNumericToWordsINR(Convert.ToDouble(grandTotal));

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
                lblGSTIN_Company1.Text = ds.Tables[0].Rows[0]["CST_No"].ToString();
                lblTAN_Company.Text = ds.Tables[0].Rows[0]["TAN_No"].ToString();
                lblState_Company.Text = ds.Tables[0].Rows[0]["State_Name"].ToString();
                lblState_Company1.Text = ds.Tables[0].Rows[0]["State_Name"].ToString();
                lblCode_Company.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                lblCode_Company1.Text = ds.Tables[0].Rows[0]["Code"].ToString();
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
    
    private void GetPurchaseOrderDetails(string PONo)
    {
        objPaymentIndentBL = new PaymentIndentBL();
        ds = new DataSet();       
        objPaymentIndentBL.Task = "GetPODetailsPrint";
        objPaymentIndentBL.StateCode = Request.QueryString["State"].ToString();
        objPaymentIndentBL.POWO_Number = PONo;
        objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.POWO_PRINT, ref ds);
        if (ds.Tables[0].Rows.Count > 0)
        { 
            lblvendorname.Text = ds.Tables[0].Rows[0]["Vendor_name"].ToString();
            lblAddline.Text = ds.Tables[0].Rows[0]["Add_Line"].ToString();
            lblConNo.Text = ds.Tables[0].Rows[0]["Con_No"].ToString();
            lblVendorGSTNo.Text = ds.Tables[0].Rows[0]["Regs_No"].ToString();
            lblcity.Text = ds.Tables[0].Rows[0]["City"].ToString();
            lblState.Text = ds.Tables[0].Rows[0]["State"].ToString();
            lblStateCode.Text = ds.Tables[0].Rows[0]["State_Code"].ToString();
            //lblcountry.Text = ds.Tables[0].Rows[0]["Country"].ToString();
            lblPinNo.Text = ds.Tables[0].Rows[0]["Pin"].ToString();
            lblVendorPanNo.Text = ds.Tables[0].Rows[0]["Pan_No"].ToString();
            lblDestination.Text = ds.Tables[0].Rows[0]["Address_Line1"].ToString();
            lblDispatchMode.Text = ds.Tables[0].Rows[0]["DispatchMode"].ToString();
            lblPONo1.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
            lblPONo2.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
            lblPONo3.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
            lblPONo4.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
            lblPONo5.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
            lblQuotNo.Text = ds.Tables[0].Rows[0]["VendorRef"].ToString();
            lblPlaceOfDispatch.Text = ds.Tables[0].Rows[0]["Place_of_Dispatch"].ToString();
            lblDestination.Text = ds.Tables[0].Rows[0]["Destination"].ToString();
            lbldelscheduled.Text = ds.Tables[0].Rows[0]["DeliverySchedule"].ToString();
            lbldispatchAdvice.Text = ds.Tables[0].Rows[0]["DespatchAdvise"].ToString();
            lblOtherTerms.Text = ds.Tables[0].Rows[0]["Other_Terms_Print"].ToString();
            lbldate.Text = ds.Tables[0].Rows[0]["PODate"].ToString();
            lblJobNo.Text = ds.Tables[0].Rows[0]["Project_Code"].ToString();
            lblJobDesc.Text = ds.Tables[0].Rows[0]["Description"].ToString();

            lblTaxableAmt1.Text = ds.Tables[0].Rows[0]["TotalAmtWithTax"].ToString();
            lblTaxableAmt2.Text = ds.Tables[0].Rows[0]["TotalAmtWithTax"].ToString();
            lblTaxableAmt3.Text = ds.Tables[0].Rows[0]["TotalAmtWithTax"].ToString();
            lblTaxableAmt4.Text = ds.Tables[0].Rows[0]["TotalAmtWithTax"].ToString();
            lblTaxableAmt5.Text = ds.Tables[0].Rows[0]["TotalAmtWithTax"].ToString();
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

            TotalAmt = Convert.ToDecimal(lblTaxableAmt1.Text);
            if (ds.Tables[0].Rows[0]["TDSPerc"].ToString() != string.Empty)
            {
                TDSPerc = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDSPerc"].ToString());
            }

            div_Watermark_Cancel.Visible = ds.Tables[0].Rows[0]["Status"].ToString() == "Cancelled" ? true : false;

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
            objPaymentIndentBL = new PaymentIndentBL();
            ds = new DataSet();
            objPaymentIndentBL.Task = "GetPOItemDetailsPrint";
            objPaymentIndentBL.StateCode = Request.QueryString["State"].ToString();
            objPaymentIndentBL.POWO_ID = Convert.ToInt32(ViewState["POID"]); ;
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.POWO_PRINT, ref ds);

            int i = 0;
            DataTable Dt1 = new DataTable();
            DataTable Dt2 = new DataTable();
            DataTable Dt3 = new DataTable();
            DataTable Dt4 = new DataTable();
            DataTable Dt5 = new DataTable();
            Dt1 = ds.Tables[0].Clone();
            Dt2 = ds.Tables[0].Clone();
            Dt3 = ds.Tables[0].Clone();
            Dt4 = ds.Tables[0].Clone();
            Dt5 = ds.Tables[0].Clone();
            Dt1.Clear();
            Dt2.Clear();
            Dt3.Clear();
            Dt4.Clear();
            Dt5.Clear();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (Convert.ToInt32(row.ItemArray[12].ToString()) < 250) // For no. of Items it will be 250,For big Desc it will be 1000.
                {
                    DataRow newRow1 = Dt1.NewRow();
                    newRow1.ItemArray = row.ItemArray;
                    Dt1.Rows.Add(newRow1);                
                }
                else if (Convert.ToInt32(row.ItemArray[12].ToString()) < 4000)
                {
                    DataRow newRow2 = Dt2.NewRow();
                    newRow2.ItemArray = row.ItemArray;
                    Dt2.Rows.Add(newRow2);
                }
                else if (Convert.ToInt32(row.ItemArray[12].ToString()) < 6750)
                {
                    DataRow newRow3 = Dt3.NewRow();
                    newRow3.ItemArray = row.ItemArray;
                    Dt3.Rows.Add(newRow3);
                }
                else if (Convert.ToInt32(row.ItemArray[12].ToString()) < 9000)
                {
                    DataRow newRow4 = Dt4.NewRow();
                    newRow4.ItemArray = row.ItemArray;
                    Dt4.Rows.Add(newRow4);
                }
                else 
                {
                    DataRow newRow5 = Dt5.NewRow();
                    newRow5.ItemArray = row.ItemArray;
                    Dt5.Rows.Add(newRow5);
                }
                i++;
            }

            GridPrint1.DataSource = Dt1;
            GridPrint1.DataBind();

            GridPrint2.DataSource = Dt2;
            GridPrint2.DataBind();

            GridPrint3.DataSource = Dt3;
            GridPrint3.DataBind();

            GridPrint4.DataSource = Dt4;
            GridPrint4.DataBind();

            GridPrint5.DataSource = Dt5;
            GridPrint5.DataBind();

            if (Convert.ToInt32(ds.Tables[0].Compute("SUM(CharLength)", string.Empty)) < 250)
            {
                lblPageNo1a.Text = "1 of 6";
                lblPageNo2.Text = "2 of 6";
                lblPageNo3a.Text = "3 of 6";
                lblPageNo3b.Text = "4 of 6";
                lblPageNo3c.Text = "5 of 6";
                lblPageNo3d.Text = "6 of 6";
                div_Continue1a.Visible = false;
                divToPrint1b.Visible = false;
                divToPrint1c.Visible = false;
                divToPrint1d.Visible = false;
                divToPrint1e.Visible = false;
            }
            else if (Convert.ToInt32(ds.Tables[0].Compute("SUM(CharLength)", string.Empty)) < 4000)
            {
                lblPageNo1a.Text = "1 of 7";
                lblPageNo1b.Text = "2 of 7";
                lblPageNo2.Text = "3 of 7";
                lblPageNo3a.Text = "4 of 7";
                lblPageNo3b.Text = "5 of 7";
                lblPageNo3c.Text = "6 of 7";
                lblPageNo3d.Text = "7 of 7";
                div_Continue1b.Visible = false;
                divToPrint1c.Visible = false;
                divToPrint1d.Visible = false;
                divToPrint1e.Visible = false;
                mytbl3a.Visible = false;
            }
            else if (Convert.ToInt32(ds.Tables[0].Compute("SUM(CharLength)", string.Empty)) < 6750)
            {
                lblPageNo1a.Text = "1 of 8";
                lblPageNo1b.Text = "2 of 8";
                lblPageNo1c.Text = "3 of 8";
                lblPageNo2.Text = "3 of 8";
                lblPageNo3a.Text = "4 of 8";
                lblPageNo3b.Text = "5 of 8";
                lblPageNo3c.Text = "6 of 8";
                lblPageNo3d.Text = "7 of 8";
                div_Continue1c.Visible = false;
                divToPrint1d.Visible = false;
                divToPrint1e.Visible = false;
                mytbl3a.Visible = false;
                mytbl3b.Visible = false;
            }
            else if (Convert.ToInt32(ds.Tables[0].Compute("SUM(CharLength)", string.Empty)) < 9000)
            {
                lblPageNo1a.Text = "1 of 9";
                lblPageNo1b.Text = "2 of 9";
                lblPageNo1c.Text = "3 of 9";
                lblPageNo1d.Text = "4 of 9";
                lblPageNo2.Text = "5 of 9";
                lblPageNo3a.Text = "6 of 9";
                lblPageNo3b.Text = "7 of 9";
                lblPageNo3c.Text = "8 of 9";
                lblPageNo3d.Text = "9 of 9";
                div_Continue1d.Visible = false;
                divToPrint1e.Visible = false;
                mytbl3a.Visible = false;
                mytbl3b.Visible = false;
                mytbl3c.Visible = false;
            }
            else
            {
                lblPageNo1a.Text = "1 of 10";
                lblPageNo1b.Text = "2 of 10";
                lblPageNo1c.Text = "3 of 10";
                lblPageNo1d.Text = "4 of 10";
                lblPageNo1e.Text = "5 of 10";
                lblPageNo2.Text = "6 of 10";
                lblPageNo3a.Text = "7 of 10";
                lblPageNo3b.Text = "8 of 10";
                lblPageNo3c.Text = "9 of 10";
                lblPageNo3d.Text = "10 of 10";
                mytbl3a.Visible = false;
                mytbl3b.Visible = false;
                mytbl3c.Visible = false;
                mytbl3d.Visible = false;
            }
            TotalCharLength = Convert.ToInt32(ds.Tables[0].Compute("SUM(CharLength)", string.Empty));
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
            objPaymentIndentBL = new PaymentIndentBL();
            ds = new DataSet();
            objPaymentIndentBL.Task = "GetPOTaxPrint";
            objPaymentIndentBL.StateCode = Request.QueryString["State"].ToString();
            objPaymentIndentBL.POWO_ID = Convert.ToInt32(ViewState["POID"]); ;
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.POWO_PRINT, ref ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (TotalCharLength < 250)
                {
                    PO_GridTax1.DataSource = ds;
                    PO_GridTax1.DataBind();
                }
                else if (TotalCharLength < 4000)
                {
                    PO_GridTax2.DataSource = ds;
                    PO_GridTax2.DataBind();
                }
                else if (TotalCharLength < 6750)
                {
                    PO_GridTax3.DataSource = ds;
                    PO_GridTax3.DataBind();
                }
                else if (TotalCharLength < 9000)
                {
                    PO_GridTax4.DataSource = ds;
                    PO_GridTax4.DataBind();
                }
                else
                {
                    PO_GridTax5.DataSource = ds;
                    PO_GridTax5.DataBind();
                }
                //TotalTaxAmt = 0.00m;
            }
            else
            {
                PO_GridTax1.DataSource = null;
                PO_GridTax1.DataBind();
                PO_GridTax2.DataSource = null;
                PO_GridTax2.DataBind();
                PO_GridTax3.DataSource = null;
                PO_GridTax3.DataBind();
                PO_GridTax4.DataSource = null;
                PO_GridTax4.DataBind();
                PO_GridTax5.DataSource = null;
                PO_GridTax5.DataBind();
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
            objPaymentIndentBL = new PaymentIndentBL();
            ds = new DataSet();
            objPaymentIndentBL.Task = "GetPOItemwiseTaxPrint";
            objPaymentIndentBL.StateCode = Request.QueryString["State"].ToString();
            objPaymentIndentBL.POWO_ID = Convert.ToInt32(ViewState["POID"]); ;
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.POWO_PRINT, ref ds);

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

    protected void PO_GridTax1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPOTaxDesc = (Label)e.Row.FindControl("lblPOTaxDesc");
                Label lblPOTaxPerc = (Label)e.Row.FindControl("lblPOTaxPerc");
                Label lblPOTaxAmt = (Label)e.Row.FindControl("lblPOTaxAmt");

                if (lblPOTaxDesc.Text.Contains("GST"))
                {
                    lblPOTaxAmt.Text = Math.Round((TotalAmt * Convert.ToDecimal(lblPOTaxPerc.Text) / 100), 2).ToString();
                }

                lblPOTaxPerc.Text = lblPOTaxPerc.Text + '%';
                TotalTaxAmt += Convert.ToDecimal(lblPOTaxAmt.Text);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
}
