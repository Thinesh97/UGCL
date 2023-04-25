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

public partial class PaymentIndent_WO_Print : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    DataSet ds = null;
    GeneralClass objGen = new GeneralClass();
    PaymentIndentBL objPaymentIndentBL = null;
    decimal TotalAmt = 0.0m;
    decimal TotalTaxAmt = 0.0m;
    decimal TDSPerc = 0.0m;
    int WO_ItemCount = 0;
    int SOW_ItemCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UID"] != null)
            {
                if (Request.QueryString["State"] != null && Request.QueryString["WONo"] != null)
                {
                    GetCompanyDetails();
                    GetWorkOrderDetails(Request.QueryString["WONo"].ToString());
                    BindWOItems();
                    BindSOWItems();
                    AssignPageNumber();

                    lblAmtAfterTax.Text = (TotalAmt + TotalTaxAmt).ToString();
                    lblAmtAfterTax_Page2.Text = (TotalAmt + TotalTaxAmt).ToString();

                    //TotalTaxAmt = Convert.ToDecimal(lblIgstAmt.Text) + Convert.ToDecimal(lblCgstAmt.Text) + Convert.ToDecimal(lblSgstAmt.Text);
                    decimal TDSAmt = 0.00m;
                    TDSAmt = Math.Round((TotalAmt * Convert.ToDecimal(TDSPerc) / 100), 2);
                    lblTDSAmt.Text = TDSAmt.ToString();
                    lblTDSAmt_Page2.Text = TDSAmt.ToString();

                    lblGrandTotal.Text = (Math.Round(TotalAmt + TotalTaxAmt - TDSAmt, 2)).ToString();
                    lblGrandTotal_Page2.Text = (TotalAmt + TotalTaxAmt - TDSAmt).ToString();

                    decimal grandTotal = Math.Round(Convert.ToDecimal(lblGrandTotal.Text), MidpointRounding.AwayFromZero);
                    lblAmountInWords.Text = objGen.changeNumericToWordsINR(Convert.ToDouble(grandTotal));
                    lblAmountInWords_Page2.Text = objGen.changeNumericToWordsINR(Convert.ToDouble(grandTotal));

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
        objPaymentIndentBL.Task = "GetWODetailsPrint";
        objPaymentIndentBL.StateCode = Request.QueryString["State"].ToString();
        objPaymentIndentBL.POWO_Number = WONo;
        objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.POWO_PRINT, ref ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblWONo.Text = ds.Tables[0].Rows[0]["WONo"].ToString();
            lblWONo2.Text = ds.Tables[0].Rows[0]["WONo"].ToString();
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
                imgBtnDigitalSign10.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign11.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign12.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign13.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign14.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign15.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign16.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign17.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign18.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign19.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign20.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign21.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign22.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign23.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign24.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign25.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign26.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign27.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign28.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign29.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign30.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign31.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign32.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign33.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign34.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign35.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign36.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign37.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign38.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign39.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign40.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
                imgBtnDigitalSign41.ImageUrl = ds.Tables[0].Rows[0]["Image_FilePath"].ToString();
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
            lblTaxableAmt_Page2.Text = ds.Tables[0].Rows[0]["TotalQtyAmt"].ToString();
            lblTDSPerc.Text = ds.Tables[0].Rows[0]["TDSPercPrint"].ToString();
            lblTDSPerc_Page2.Text = ds.Tables[0].Rows[0]["TDSPercPrint"].ToString();
            TDSPerc = Convert.ToDecimal(lblTDSPerc.Text);


            //ANNEXURE-A
            lblContactName.Text = ds.Tables[0].Rows[0]["Contact_Name"].ToString();
            lblContactNo.Text = ds.Tables[0].Rows[0]["Contact_No"].ToString();
            lblPaymentTerms.Text = ds.Tables[0].Rows[0]["PaymentTerms"].ToString();
            lblWorkLocation.Text = ds.Tables[0].Rows[0]["WorkLocation"].ToString();
            //lblOtherTerms.Text = ds.Tables[0].Rows[0]["Other_Terms"].ToString();
            if (ds.Tables[1].Rows.Count > 0)
            {
                Grid_OtherTerms.DataSource = ds.Tables[1];
                Grid_OtherTerms.DataBind();
            }

            //General Term and conditions
            lblWONo1.Text = ds.Tables[0].Rows[0]["WONo"].ToString();
            lblWODate1.Text = ds.Tables[0].Rows[0]["WODate"].ToString();
            lblContractor.Text = "United Global Corporation Ltd.";
            lblSubcontractor.Text = ds.Tables[0].Rows[0]["Subcon_name"].ToString();
            lblProjectName.Text = ds.Tables[0].Rows[0]["Project_Name"].ToString();

            lblPrincipalContractor.Text = ds.Tables[0].Rows[0]["Principal_Contractor"].ToString();
            lblAward_By.Text = ds.Tables[0].Rows[0]["Award_By"].ToString();
            lblAgreementdate1.Text = ds.Tables[0].Rows[0]["Agreement_Date"].ToString();
            lblPrincipalContractor1.Text = ds.Tables[0].Rows[0]["Principal_Contractor"].ToString();
            lblPrincipalContractor2.Text = ds.Tables[0].Rows[0]["Principal_Contractor"].ToString();
            lblEmployerAgreementDate.Text = ds.Tables[0].Rows[0]["E_Agreement_Date"].ToString();
            lblSubcontractor1.Text = ds.Tables[0].Rows[0]["Subcon_name"].ToString();

            lblWONo3.Text = ds.Tables[0].Rows[0]["WONo"].ToString();
            lblWODate2.Text = ds.Tables[0].Rows[0]["WODate"].ToString();
            lblSubcontractorName1.Text = ds.Tables[0].Rows[0]["Subcon_name"].ToString();
            lblAddline1.Text = ds.Tables[0].Rows[0]["Add_Line"].ToString();
            lblCity1.Text = ds.Tables[0].Rows[0]["City"].ToString();
            lblPinNo1.Text = ds.Tables[0].Rows[0]["Pin"].ToString();

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
            objPaymentIndentBL.Task = "GetWOItemDetailsPrint";
            objPaymentIndentBL.StateCode = Request.QueryString["State"].ToString();
            objPaymentIndentBL.POWO_ID = Convert.ToInt32(ViewState["WOID"]); ;
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.POWO_PRINT, ref ds);
            WO_ItemCount = ds.Tables[0].Rows.Count;
            int i = 0;
            DataTable Dt1 = new DataTable();
            DataTable Dt2 = new DataTable();
            Dt1 = ds.Tables[0].Clone();
            Dt2 = ds.Tables[0].Clone();
            Dt1.Clear();
            Dt2.Clear();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (i < 5)
                {
                    DataRow newRow1 = Dt1.NewRow();
                    newRow1.ItemArray = row.ItemArray;
                    Dt1.Rows.Add(newRow1);
                    lblPageNo1.Text = "1 of 18";
                    lblPageNo3.Text = "2 of 18";
                    lblPageNo4.Text = "3 of 18";
                    lblPageNo5.Text = "4 of 18";
                    lblPageNo6.Text = "5 of 18";
                    lblPageNo7.Text = "6 of 18";
                    lblPageNo8.Text = "7 of 18";
                    lblPageNo9.Text = "8 of 18";
                    lblPageNo10.Text = "9 of 18";
                    lblPageNo11.Text = "10 of 18";
                    lblPageNo12.Text = "11 of 18";
                    lblPageNo13.Text = "12 of 18";
                    lblPageNo14.Text = "13 of 18";
                    lblPageNo15.Text = "14 of 18";
                    lblPageNo16.Text = "15 of 18";
                    lblPageNo17.Text = "16 of 18";
                    lblPageNo18.Text = "17 of 18";
                    lblPageNo19.Text = "18 of 18";
                }
                else
                {
                    DataRow newRow2 = Dt2.NewRow();
                    newRow2.ItemArray = row.ItemArray;
                    Dt2.Rows.Add(newRow2);
                    lblPageNo1.Text = "1 of 19";
                    lblPageNo2.Text = "2 of 19";
                    lblPageNo3.Text = "3 of 19";
                    lblPageNo4.Text = "4 of 19";
                    lblPageNo5.Text = "5 of 19";
                    lblPageNo6.Text = "6 of 19";
                    lblPageNo7.Text = "7 of 19";
                    lblPageNo8.Text = "8 of 19";
                    lblPageNo9.Text = "9 of 19";
                    lblPageNo10.Text = "10 of 19";
                    lblPageNo11.Text = "11 of 19";
                    lblPageNo12.Text = "12 of 19";
                    lblPageNo13.Text = "13 of 19";
                    lblPageNo14.Text = "14 of 19";
                    lblPageNo15.Text = "15 of 19";
                    lblPageNo16.Text = "16 of 19";
                    lblPageNo17.Text = "17 of 19";
                    lblPageNo18.Text = "18 of 19";
                    lblPageNo19.Text = "19 of 19";
                }
                i++;
            }

            GridPrint1.DataSource = Dt1;
            GridPrint1.DataBind();

            GridPrint2.DataSource = Dt2;
            GridPrint2.DataBind();

            Decimal TotalQty = Convert.ToDecimal(ds.Tables[0].Compute("SUM(Quantity)", string.Empty));
            if (TotalQty <= 0)
            {
                GridPrint1.Columns[2].Visible = false;
                GridPrint2.Columns[2].Visible = false;
            }

            if (ds.Tables[0].Rows.Count < 6)
            {
                div_Continue.Visible = false;
                divToPrint2.Visible = false;
            }
            else
            {
                mytbl3a.Visible = false;
            }


            //Tax Calc            
            lblIgstPerc.Text = ds.Tables[0].Rows[0]["Igst_Perc"].ToString();
            lblIgstPerc1.Text = ds.Tables[0].Rows[0]["Igst_Perc"].ToString();
            //lblIgstAmt.Text = Total_Igst_Amt.ToString();
            lblIgstAmt.Text = (Math.Round((TotalAmt * Convert.ToDecimal(lblIgstPerc.Text) / 100), 2)).ToString();
            lblCgstPerc.Text = ds.Tables[0].Rows[0]["Cgst_Perc"].ToString();
            lblCgstPerc1.Text = ds.Tables[0].Rows[0]["Cgst_Perc"].ToString();
            lblCgstAmt.Text = (Math.Round((TotalAmt * Convert.ToDecimal(lblCgstPerc.Text) / 100), 2)).ToString();
            lblSgstPerc.Text = ds.Tables[0].Rows[0]["Sgst_Perc"].ToString();
            lblSgstPerc1.Text = ds.Tables[0].Rows[0]["Sgst_Perc"].ToString();
            lblSgstAmt.Text = (Math.Round((TotalAmt * Convert.ToDecimal(lblSgstPerc.Text) / 100), 2)).ToString();

            tr_IGST.Visible = Convert.ToDecimal(lblIgstAmt.Text) == 0 ? false : true;
            tr_CGST.Visible = Convert.ToDecimal(lblCgstAmt.Text) == 0 ? false : true;
            tr_SGST.Visible = Convert.ToDecimal(lblSgstAmt.Text) == 0 ? false : true;

            lblIgstPerc_Page2.Text = ds.Tables[0].Rows[0]["Igst_Perc"].ToString();
            lblIgstPerc1_Page2.Text = ds.Tables[0].Rows[0]["Igst_Perc"].ToString();
            lblIgstAmt_Page2.Text = (TotalAmt * Convert.ToDecimal(lblIgstPerc_Page2.Text) / 100).ToString();
            lblCgstPerc_Page2.Text = ds.Tables[0].Rows[0]["Cgst_Perc"].ToString();
            lblCgstPerc1_Page2.Text = ds.Tables[0].Rows[0]["Cgst_Perc"].ToString();
            lblCgstAmt_Page2.Text = (TotalAmt * Convert.ToDecimal(lblCgstPerc_Page2.Text) / 100).ToString();
            lblSgstPerc_Page2.Text = ds.Tables[0].Rows[0]["Sgst_Perc"].ToString();
            lblSgstPerc1_Page2.Text = ds.Tables[0].Rows[0]["Sgst_Perc"].ToString();
            lblSgstAmt_Page2.Text = (TotalAmt * Convert.ToDecimal(lblSgstPerc_Page2.Text) / 100).ToString();

            tr_IGST_Page2.Visible = Convert.ToDecimal(lblIgstAmt.Text) == 0 ? false : true;
            tr_CGST_Page2.Visible = Convert.ToDecimal(lblCgstAmt.Text) == 0 ? false : true;
            tr_SGST_Page2.Visible = Convert.ToDecimal(lblSgstAmt.Text) == 0 ? false : true;

            //ANNEXURE-A
            lblDesc.Text = ds.Tables[0].Rows[0]["Item_Type"].ToString();
            if (ds.Tables[0].Rows[0]["Gst_Exist"].ToString() != "")
            {
                lblGst.Text = Convert.ToBoolean(ds.Tables[0].Rows[0]["Gst_Exist"]) == true ? "Inclusive" : "Exclusive";
            }

            //ANNEXURE-B
            lblRateUOM.Text = ds.Tables[0].Rows[0]["RateUOM"].ToString();
            lblItemDesc.Text = ds.Tables[0].Rows[0]["Item_Desc"].ToString();



            //ANNEXURE-C
            lblItemDesc1.Text = ds.Tables[0].Rows[0]["Item_Desc"].ToString();

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
            if (DataBinder.Eval(e.Row.DataItem, "Total_Amt") != DBNull.Value)
            {
                TotalAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total_Amt"));
            }
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

    private void BindSOWItems()
    {
        try
        {
            divToPrint7.Visible = false;
            divToPrint8.Visible = false;
            divToPrint9.Visible = false;
            divToPrint10.Visible = false;
            divToPrint11.Visible = false;
            divToPrint12.Visible = false;
            divToPrint13.Visible = false;
            divToPrint14.Visible = false;
            divToPrint15.Visible = false;
            divToPrint16.Visible = false;
            divToPrint17.Visible = false;
            divToPrint18.Visible = false;
            divToPrint19.Visible = false;
            divToPrint20.Visible = false;
            divToPrint21.Visible = false;
            divToPrint22.Visible = false;
            divToPrint23.Visible = false;
            divToPrint24.Visible = false;
            divToPrint25.Visible = false;
            divToPrint26.Visible = false;
            divToPrint27.Visible = false;

            objPaymentIndentBL = new PaymentIndentBL();
            ds = new DataSet();
            objPaymentIndentBL.Task = "GetSOWItemsPrint";
            objPaymentIndentBL.StateCode = Request.QueryString["State"].ToString();
            objPaymentIndentBL.POWO_ID = Convert.ToInt32(ViewState["WOID"]); ;
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.POWO_PRINT, ref ds);
            SOW_ItemCount = ds.Tables[0].Rows.Count;

            int i = 0;
            DataTable Dt1 = new DataTable();
            DataTable Dt2 = new DataTable();
            DataTable Dt3 = new DataTable();
            DataTable Dt4 = new DataTable();
            DataTable Dt5 = new DataTable();
            DataTable Dt6 = new DataTable();
            DataTable Dt7 = new DataTable();
            DataTable Dt8 = new DataTable();
            DataTable Dt9 = new DataTable();
            DataTable Dt10 = new DataTable();
            DataTable Dt11 = new DataTable();
            DataTable Dt12 = new DataTable();
            DataTable Dt13 = new DataTable();
            DataTable Dt14 = new DataTable();
            DataTable Dt15 = new DataTable();
            DataTable Dt16 = new DataTable();
            DataTable Dt17 = new DataTable();
            DataTable Dt18 = new DataTable();
            DataTable Dt19 = new DataTable();
            DataTable Dt20 = new DataTable();
            DataTable Dt21 = new DataTable();
            Dt1 = ds.Tables[0].Clone();
            Dt2 = ds.Tables[0].Clone();
            Dt3 = ds.Tables[0].Clone();
            Dt4 = ds.Tables[0].Clone();
            Dt5 = ds.Tables[0].Clone();
            Dt6 = ds.Tables[0].Clone();
            Dt7 = ds.Tables[0].Clone();
            Dt8 = ds.Tables[0].Clone();
            Dt9 = ds.Tables[0].Clone();
            Dt10 = ds.Tables[0].Clone();
            Dt11 = ds.Tables[0].Clone();
            Dt12 = ds.Tables[0].Clone();
            Dt13 = ds.Tables[0].Clone();
            Dt14 = ds.Tables[0].Clone();
            Dt15 = ds.Tables[0].Clone();
            Dt16 = ds.Tables[0].Clone();
            Dt17 = ds.Tables[0].Clone();
            Dt18 = ds.Tables[0].Clone();
            Dt19 = ds.Tables[0].Clone();
            Dt20 = ds.Tables[0].Clone();
            Dt21 = ds.Tables[0].Clone();
            Dt1.Clear();
            Dt2.Clear();
            Dt3.Clear();
            Dt4.Clear();
            Dt5.Clear();
            Dt6.Clear();
            Dt7.Clear();
            Dt8.Clear();
            Dt9.Clear();
            Dt10.Clear();
            Dt11.Clear();
            Dt12.Clear();
            Dt13.Clear();
            Dt14.Clear();
            Dt15.Clear();
            Dt16.Clear();
            Dt17.Clear();
            Dt18.Clear();
            Dt19.Clear();
            Dt20.Clear();
            Dt21.Clear();
            var result = Dt1.AsEnumerable().Take(5);
            //Dt5.Clear();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (i < 18)
                {
                    DataRow newRow1 = Dt1.NewRow();
                    newRow1.ItemArray = row.ItemArray;
                    Dt1.Rows.Add(newRow1);
                }
                if (i > 17)
                {
                    if (i < 36)
                    {
                        divToPrint7.Visible = true;
                        DataRow newRow2 = Dt2.NewRow();
                        newRow2.ItemArray = row.ItemArray;
                        Dt2.Rows.Add(newRow2);
                    }

                }
                if (i > 35)
                {
                    if (i < 54)
                    {
                        divToPrint8.Visible = true;
                        DataRow newRow3 = Dt3.NewRow();
                        newRow3.ItemArray = row.ItemArray;
                        Dt3.Rows.Add(newRow3);
                    }


                }
                if (i > 53)
                {
                    if (i < 72)
                    {
                        divToPrint9.Visible = true;
                        DataRow newRow4 = Dt4.NewRow();
                        newRow4.ItemArray = row.ItemArray;
                        Dt4.Rows.Add(newRow4);
                    }

                }
                if (i > 71)
                {
                    if (i < 90)
                    {
                        divToPrint10.Visible = true;
                        DataRow newRow5 = Dt5.NewRow();
                        newRow5.ItemArray = row.ItemArray;
                        Dt5.Rows.Add(newRow5);
                    }

                }
                if (i > 89)
                {
                    if (i < 108)
                    {
                        divToPrint11.Visible = true;
                        DataRow newRow6 = Dt6.NewRow();
                        newRow6.ItemArray = row.ItemArray;
                        Dt6.Rows.Add(newRow6);
                    }

                }

                if (i > 107)
                {
                    if (i < 126)
                    {
                        divToPrint12.Visible = true;
                        DataRow newRow7 = Dt7.NewRow();
                        newRow7.ItemArray = row.ItemArray;
                        Dt7.Rows.Add(newRow7);
                    }

                }
                if (i > 125)
                {
                    if (i < 144)
                    {
                        divToPrint13.Visible = true;
                        DataRow newRow8 = Dt8.NewRow();
                        newRow8.ItemArray = row.ItemArray;
                        Dt8.Rows.Add(newRow8);
                    }

                }
                if (i > 143)
                {
                    if (i < 162)
                    {
                        divToPrint14.Visible = true;
                        DataRow newRow9 = Dt9.NewRow();
                        newRow9.ItemArray = row.ItemArray;
                        Dt9.Rows.Add(newRow9);
                    }

                }

                if (i > 161)
                {
                    if (i < 180)
                    {
                        divToPrint15.Visible = true;
                        DataRow newRow10 = Dt10.NewRow();
                        newRow10.ItemArray = row.ItemArray;
                        Dt10.Rows.Add(newRow10);
                    }

                }
                if (i > 179)
                {
                    if (i < 198)
                    {
                        divToPrint16.Visible = true;
                        DataRow newRow11 = Dt11.NewRow();
                        newRow11.ItemArray = row.ItemArray;
                        Dt11.Rows.Add(newRow11);
                    }

                }
                if (i > 197)
                {
                    if (i < 216)
                    {
                        divToPrint17.Visible = true;
                        DataRow newRow12 = Dt12.NewRow();
                        newRow12.ItemArray = row.ItemArray;
                        Dt12.Rows.Add(newRow12);
                    }

                }
                if (i > 215)
                {
                    if (i < 234)
                    {
                        divToPrint18.Visible = true;
                        DataRow newRow13 = Dt13.NewRow();
                        newRow13.ItemArray = row.ItemArray;
                        Dt13.Rows.Add(newRow13);
                    }

                }
                if (i > 233)
                {
                    if (i < 252)
                    {
                        divToPrint19.Visible = true;
                        DataRow newRow14 = Dt14.NewRow();
                        newRow14.ItemArray = row.ItemArray;
                        Dt14.Rows.Add(newRow14);
                    }

                }
                if (i > 251)
                {
                    if (i < 270)
                    {
                        divToPrint20.Visible = true;
                        DataRow newRow15 = Dt15.NewRow();
                        newRow15.ItemArray = row.ItemArray;
                        Dt15.Rows.Add(newRow15);
                    }

                }
                if (i > 269)
                {
                    if (i < 288)
                    {
                        divToPrint21.Visible = true;
                        DataRow newRow15 = Dt16.NewRow();
                        newRow15.ItemArray = row.ItemArray;
                        Dt16.Rows.Add(newRow15);
                    }

                }
                if (i > 287)
                {
                    if (i < 306)
                    {
                        divToPrint22.Visible = true;
                        DataRow newRow16 = Dt16.NewRow();
                        newRow16.ItemArray = row.ItemArray;
                        Dt16.Rows.Add(newRow16);
                    }

                }
                if (i > 305)
                {
                    if (i < 324)
                    {
                        divToPrint23.Visible = true;
                        DataRow newRow17 = Dt17.NewRow();
                        newRow17.ItemArray = row.ItemArray;
                        Dt17.Rows.Add(newRow17);
                    }

                }
                if (i > 323)
                {
                    if (i < 342)
                    {
                        divToPrint24.Visible = true;
                        DataRow newRow18 = Dt18.NewRow();
                        newRow18.ItemArray = row.ItemArray;
                        Dt18.Rows.Add(newRow18);
                    }

                }
                if (i > 341)
                {
                    if (i < 360)
                    {
                        divToPrint25.Visible = true;
                        DataRow newRow19 = Dt19.NewRow();
                        newRow19.ItemArray = row.ItemArray;
                        Dt19.Rows.Add(newRow19);
                    }

                }
                if (i > 359)
                {
                    if (i < 378)
                    {
                        divToPrint26.Visible = true;
                        DataRow newRow20 = Dt20.NewRow();
                        newRow20.ItemArray = row.ItemArray;
                        Dt20.Rows.Add(newRow20);
                    }

                }
                if (i > 377)
                {
                    if (i < 396)
                    {
                        divToPrint27.Visible = true;
                        DataRow newRow21 = Dt21.NewRow();
                        newRow21.ItemArray = row.ItemArray;
                        Dt21.Rows.Add(newRow21);
                    }

                }
                GridPrint3.DataSource = Dt1;
                GridPrint3.DataBind();
                GridPrint4.DataSource = Dt2;
                GridPrint4.DataBind();
                GridPrint8.DataSource = Dt3;
                GridPrint8.DataBind();
                GridPrint9.DataSource = Dt4;
                GridPrint9.DataBind();
                GridPrint10.DataSource = Dt5;
                GridPrint10.DataBind();
                GridPrint11.DataSource = Dt6;
                GridPrint11.DataBind();
                GridPrint12.DataSource = Dt7;
                GridPrint12.DataBind();
                GridPrint13.DataSource = Dt8;
                GridPrint13.DataBind();
                GridPrint14.DataSource = Dt9;
                GridPrint14.DataBind();
                GridPrint15.DataSource = Dt10;
                GridPrint15.DataBind();
                GridPrint16.DataSource = Dt11;
                GridPrint16.DataBind();
                GridPrint17.DataSource = Dt12;
                GridPrint17.DataBind();
                GridPrint18.DataSource = Dt13;
                GridPrint18.DataBind();
                GridPrint19.DataSource = Dt14;
                GridPrint19.DataBind();
                GridPrint20.DataSource = Dt15;
                GridPrint20.DataBind();
                GridPrint21.DataSource = Dt16;
                GridPrint21.DataBind();
                GridPrint22.DataSource = Dt17;
                GridPrint22.DataBind();
                GridPrint23.DataSource = Dt18;
                GridPrint23.DataBind();
                GridPrint24.DataSource = Dt19;
                GridPrint24.DataBind();
                GridPrint25.DataSource = Dt20;
                GridPrint25.DataBind();
                GridPrint26.DataSource = Dt21;
                GridPrint26.DataBind();

                i++;
            }


            if (ds.Tables[0].Rows.Count <= 0)
            {
                divToPrint6.Visible = false;
                divToPrint7.Visible = false;
            }
            else if (ds.Tables[0].Rows.Count < 20)
            {
                divToPrint7.Visible = false;
            }
            else if (ds.Tables[0].Rows.Count >= 20)
            {
                divToPrint7.Visible = true;
            }

            GridPrint3.DataSource = Dt1;
            GridPrint3.DataBind();

            GridPrint4.DataSource = Dt2;
            GridPrint4.DataBind();
            if (ds.Tables[0].Rows.Count <= 0)
            {
                lblAnnexure.Text = "Annexure A,B & C Are Integral Part of This Work Order";
                lblAnnexure1.Text = "Annexure A,B & C Are Integral Part of This Work Order";
            }
            else
            {
                lblAnnexure.Text = "Annexure A,B,C & D Are Integral Part of This Work Order";
                lblAnnexure1.Text = "Annexure A,B,C & D Are Integral Part of This Work Order";
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void AssignPageNumber()
    {
        try
        {
            if (WO_ItemCount < 6 && SOW_ItemCount < 1)
            {
                lblPageNo1.Text = "1 of 18";
                lblPageNo3.Text = "2 of 18";
                lblPageNo4.Text = "3 of 18";
                lblPageNo5.Text = "4 of 18";
                lblPageNo6.Text = "5 of 18";
                lblPageNo7.Text = "6 of 18";
                lblPageNo8.Text = "7 of 18";
                lblPageNo9.Text = "8 of 18";
                lblPageNo10.Text = "9 of 18";
                lblPageNo11.Text = "10 of 18";
                lblPageNo12.Text = "11 of 18";
                lblPageNo13.Text = "12 of 18";
                lblPageNo14.Text = "13 of 18";
                lblPageNo15.Text = "14 of 18";
                lblPageNo16.Text = "15 of 18";
                lblPageNo17.Text = "16 of 18";
                lblPageNo18.Text = "17 of 18";
                lblPageNo19.Text = "18 of 18";
            }
            else if (WO_ItemCount < 6 && (SOW_ItemCount <= 20 && SOW_ItemCount >= 1))
            {
                lblPageNo1.Text = "1 of 19";
                lblPageNo3.Text = "2 of 19";
                lblPageNo4.Text = "3 of 19";
                lblPageNo5.Text = "4 of 19";
                lblPageNo6.Text = "5 of 19";
                lblPageNo7.Text = "6 of 19";
                lblPageNo8.Text = "7 of 19";
                lblPageNo9.Text = "8 of 19";
                lblPageNo10.Text = "9 of 19";
                lblPageNo11.Text = "10 of 19";
                lblPageNo12.Text = "11 of 19";
                lblPageNo13.Text = "12 of 19";
                lblPageNo14.Text = "13 of 19";
                lblPageNo15.Text = "14 of 19";
                lblPageNo16.Text = "15 of 19";
                lblPageNo17.Text = "16 of 19";
                lblPageNo18.Text = "17 of 19";
                lblPageNo19.Text = "18 of 19";
                lblPageNo20.Text = "19 of 19";
            }

            else if (WO_ItemCount < 6 && SOW_ItemCount >= 20)
            {
                lblPageNo1.Text = "1 of 21";
                lblPageNo3.Text = "2 of 21";
                lblPageNo4.Text = "3 of 21";
                lblPageNo5.Text = "4 of 21";
                lblPageNo6.Text = "5 of 21";
                lblPageNo7.Text = "6 of 21";
                lblPageNo8.Text = "7 of 21";
                lblPageNo9.Text = "8 of 21";
                lblPageNo10.Text = "9 of 21";
                lblPageNo11.Text = "10 of 21";
                lblPageNo12.Text = "11 of 21";
                lblPageNo13.Text = "12 of 21";
                lblPageNo14.Text = "13 of 21";
                lblPageNo15.Text = "14 of 21";
                lblPageNo16.Text = "15 of 21";
                lblPageNo17.Text = "16 of 21";
                lblPageNo18.Text = "18 of 21";
                lblPageNo19.Text = "19 of 21";
                lblPageNo20.Text = "20 of 21";
                lblPageNo21.Text = "21 of 21";
            }
            else if (WO_ItemCount >= 6 && SOW_ItemCount >= 20)
            {
                if (SOW_ItemCount >= 18)
                {
                    lblPageNo1.Text = "1 of 21";
                    lblPageNo2.Text = "2 of 21";
                    lblPageNo3.Text = "3 of 21";
                    lblPageNo4.Text = "4 of 21";
                    lblPageNo5.Text = "5 of 21";
                    lblPageNo6.Text = "6 of 21";
                    lblPageNo7.Text = "7 of 21";
                    lblPageNo8.Text = "8 of 21";
                    lblPageNo9.Text = "9 of 21";
                    lblPageNo10.Text = "10 of 21";
                    lblPageNo11.Text = "11 of 21";
                    lblPageNo12.Text = "12 of 21";
                    lblPageNo13.Text = "13 of 21";
                    lblPageNo14.Text = "14 of 21";
                    lblPageNo15.Text = "15 of 21";
                    lblPageNo16.Text = "16 of 21";
                    lblPageNo17.Text = "17 of 21";
                    lblPageNo18.Text = "18 of 21";
                    lblPageNo19.Text = "19 of 21";
                    lblPageNo20.Text = "20 of 21";
                    lblPageNo21.Text = "21 of 21";
                }
                if (SOW_ItemCount >= 36)
                {
                    lblPageNo1.Text = "1 of 22";
                    lblPageNo2.Text = "2 of 22";
                    lblPageNo3.Text = "3 of 22";
                    lblPageNo4.Text = "4 of 22";
                    lblPageNo5.Text = "5 of 22";
                    lblPageNo6.Text = "6 of 22";
                    lblPageNo7.Text = "7 of 22";
                    lblPageNo8.Text = "8 of 22";
                    lblPageNo9.Text = "9 of 22";
                    lblPageNo10.Text = "10 of 22";
                    lblPageNo11.Text = "11 of 22";
                    lblPageNo12.Text = "12 of 22";
                    lblPageNo13.Text = "13 of 22";
                    lblPageNo14.Text = "14 of 22";
                    lblPageNo15.Text = "15 of 22";
                    lblPageNo16.Text = "16 of 22";
                    lblPageNo17.Text = "17 of 22";
                    lblPageNo18.Text = "18 of 22";
                    lblPageNo19.Text = "19 of 22";
                    lblPageNo20.Text = "20 of 22";
                    lblPageNo21.Text = "21 of 22";
                    lblPageNo22.Text = "22 of 22";

                }
                if (SOW_ItemCount >= 54)
                {
                    lblPageNo1.Text = "1 of 23";
                    lblPageNo2.Text = "2 of 23";
                    lblPageNo3.Text = "3 of 23";
                    lblPageNo4.Text = "4 of 23";
                    lblPageNo5.Text = "5 of 23";
                    lblPageNo6.Text = "6 of 23";
                    lblPageNo7.Text = "7 of 23";
                    lblPageNo8.Text = "8 of 23";
                    lblPageNo9.Text = "9 of 23";
                    lblPageNo10.Text = "10 of 23";
                    lblPageNo11.Text = "11 of 23";
                    lblPageNo12.Text = "12 of 23";
                    lblPageNo13.Text = "13 of 23";
                    lblPageNo14.Text = "14 of 23";
                    lblPageNo15.Text = "15 of 23";
                    lblPageNo16.Text = "16 of 23";
                    lblPageNo17.Text = "17 of 23";
                    lblPageNo18.Text = "18 of 23";
                    lblPageNo19.Text = "19 of 23";
                    lblPageNo20.Text = "20 of 23";
                    lblPageNo21.Text = "21 of 23";
                    lblPageNo22.Text = "22 of 23";
                    lblPageNo23.Text = "23 of 23";


                }
                if (SOW_ItemCount >= 72)
                {
                    lblPageNo1.Text = "1 of 24";
                    lblPageNo2.Text = "2 of 24";
                    lblPageNo3.Text = "3 of 24";
                    lblPageNo4.Text = "4 of 24";
                    lblPageNo5.Text = "5 of 24";
                    lblPageNo6.Text = "6 of 24";
                    lblPageNo7.Text = "7 of 24";
                    lblPageNo8.Text = "8 of 24";
                    lblPageNo9.Text = "9 of 24";
                    lblPageNo10.Text = "10 of 24";
                    lblPageNo11.Text = "11 of 24";
                    lblPageNo12.Text = "12 of 24";
                    lblPageNo13.Text = "13 of 24";
                    lblPageNo14.Text = "14 of 24";
                    lblPageNo15.Text = "15 of 24";
                    lblPageNo16.Text = "16 of 24";
                    lblPageNo17.Text = "17 of 24";
                    lblPageNo18.Text = "18 of 24";
                    lblPageNo19.Text = "19 of 24";
                    lblPageNo20.Text = "20 of 24";
                    lblPageNo21.Text = "21 of 24";
                    lblPageNo22.Text = "22 of 24";
                    lblPageNo23.Text = "23 of 24";
                    lblPageNo24.Text = "24 of 24";
                }
                if (SOW_ItemCount >= 90)
                {
                    lblPageNo1.Text = "1 of 25";
                    lblPageNo2.Text = "2 of 25";
                    lblPageNo3.Text = "3 of 25";
                    lblPageNo4.Text = "4 of 25";
                    lblPageNo5.Text = "5 of 25";
                    lblPageNo6.Text = "6 of 25";
                    lblPageNo7.Text = "7 of 25";
                    lblPageNo8.Text = "8 of 25";
                    lblPageNo9.Text = "9 of 25";
                    lblPageNo10.Text = "10 of 25";
                    lblPageNo11.Text = "11 of 25";
                    lblPageNo12.Text = "12 of 25";
                    lblPageNo13.Text = "13 of 25";
                    lblPageNo14.Text = "14 of 25";
                    lblPageNo15.Text = "15 of 25";
                    lblPageNo16.Text = "16 of 25";
                    lblPageNo17.Text = "17 of 25";
                    lblPageNo18.Text = "18 of 25";
                    lblPageNo19.Text = "19 of 25";
                    lblPageNo20.Text = "20 of 25";
                    lblPageNo21.Text = "21 of 25";
                    lblPageNo22.Text = "22 of 25";
                    lblPageNo23.Text = "23 of 25";
                    lblPageNo24.Text = "24 of 25";
                    lblPageNo25.Text = "25 of 25";

                }
                if (SOW_ItemCount >= 108)
                {
                    lblPageNo1.Text = "1 of 26";
                    lblPageNo2.Text = "2 of 26";
                    lblPageNo3.Text = "3 of 26";
                    lblPageNo4.Text = "4 of 26";
                    lblPageNo5.Text = "5 of 26";
                    lblPageNo6.Text = "6 of 26";
                    lblPageNo7.Text = "7 of 26";
                    lblPageNo8.Text = "8 of 26";
                    lblPageNo9.Text = "9 of 26";
                    lblPageNo10.Text = "10 of 26";
                    lblPageNo11.Text = "11 of 26";
                    lblPageNo12.Text = "12 of 26";
                    lblPageNo13.Text = "13 of 26";
                    lblPageNo14.Text = "14 of 26";
                    lblPageNo15.Text = "15 of 26";
                    lblPageNo16.Text = "16 of 26";
                    lblPageNo17.Text = "17 of 26";
                    lblPageNo18.Text = "18 of 26";
                    lblPageNo19.Text = "19 of 26";
                    lblPageNo20.Text = "20 of 26";
                    lblPageNo21.Text = "21 of 26";
                    lblPageNo22.Text = "22 of 26";
                    lblPageNo23.Text = "23 of 26";
                    lblPageNo24.Text = "24 of 26";
                    lblPageNo25.Text = "25 of 26";
                    lblPageNo26.Text = "26 of 26";

                }
                if (SOW_ItemCount >= 126)
                {
                    lblPageNo1.Text = "1 of 27";
                    lblPageNo2.Text = "2 of 27";
                    lblPageNo3.Text = "3 of 27";
                    lblPageNo4.Text = "4 of 27";
                    lblPageNo5.Text = "5 of 27";
                    lblPageNo6.Text = "6 of 27";
                    lblPageNo7.Text = "7 of 27";
                    lblPageNo8.Text = "8 of 27";
                    lblPageNo9.Text = "9 of 27";
                    lblPageNo10.Text = "10 of 27";
                    lblPageNo11.Text = "11 of 27";
                    lblPageNo12.Text = "12 of 27";
                    lblPageNo13.Text = "13 of 27";
                    lblPageNo14.Text = "14 of 27";
                    lblPageNo15.Text = "15 of 27";
                    lblPageNo16.Text = "16 of 27";
                    lblPageNo17.Text = "17 of 27";
                    lblPageNo18.Text = "18 of 27";
                    lblPageNo19.Text = "19 of 27";
                    lblPageNo20.Text = "20 of 27";
                    lblPageNo21.Text = "21 of 27";
                    lblPageNo22.Text = "22 of 27";
                    lblPageNo23.Text = "23 of 27";
                    lblPageNo24.Text = "24 of 27";
                    lblPageNo25.Text = "25 of 27";
                    lblPageNo26.Text = "26 of 27";
                    lblPageNo27.Text = "27 of 27";
                }
                if (SOW_ItemCount >= 144)
                {
                    lblPageNo1.Text = "1 of 28";
                    lblPageNo2.Text = "2 of 28";
                    lblPageNo3.Text = "3 of 28";
                    lblPageNo4.Text = "4 of 28";
                    lblPageNo5.Text = "5 of 28";
                    lblPageNo6.Text = "6 of 28";
                    lblPageNo7.Text = "7 of 28";
                    lblPageNo8.Text = "8 of 28";
                    lblPageNo9.Text = "9 of 28";
                    lblPageNo10.Text = "10 of 28";
                    lblPageNo11.Text = "11 of 28";
                    lblPageNo12.Text = "12 of 28";
                    lblPageNo13.Text = "13 of 28";
                    lblPageNo14.Text = "14 of 28";
                    lblPageNo15.Text = "15 of 28";
                    lblPageNo16.Text = "16 of 28";
                    lblPageNo17.Text = "17 of 28";
                    lblPageNo18.Text = "18 of 28";
                    lblPageNo19.Text = "19 of 28";
                    lblPageNo20.Text = "20 of 28";
                    lblPageNo21.Text = "21 of 28";
                    lblPageNo22.Text = "22 of 28";
                    lblPageNo23.Text = "23 of 28";
                    lblPageNo24.Text = "24 of 28";
                    lblPageNo25.Text = "25 of 28";
                    lblPageNo26.Text = "26 of 28";
                    lblPageNo27.Text = "27 of 28";
                    lblPageNo28.Text = "28 of 28";
                }

                if (SOW_ItemCount >= 162)
                {
                    lblPageNo1.Text = "1 of 29";
                    lblPageNo2.Text = "2 of 29";
                    lblPageNo3.Text = "3 of 29";
                    lblPageNo4.Text = "4 of 29";
                    lblPageNo5.Text = "5 of 29";
                    lblPageNo6.Text = "6 of 29";
                    lblPageNo7.Text = "7 of 29";
                    lblPageNo8.Text = "8 of 29";
                    lblPageNo9.Text = "9 of 29";
                    lblPageNo10.Text = "10 of 29";
                    lblPageNo11.Text = "11 of 29";
                    lblPageNo12.Text = "12 of 29";
                    lblPageNo13.Text = "13 of 29";
                    lblPageNo14.Text = "14 of 29";
                    lblPageNo15.Text = "15 of 29";
                    lblPageNo16.Text = "16 of 29";
                    lblPageNo17.Text = "17 of 29";
                    lblPageNo18.Text = "18 of 29";
                    lblPageNo19.Text = "19 of 29";
                    lblPageNo20.Text = "20 of 29";
                    lblPageNo21.Text = "21 of 29";
                    lblPageNo22.Text = "22 of 29";
                    lblPageNo23.Text = "23 of 29";
                    lblPageNo24.Text = "24 of 29";
                    lblPageNo25.Text = "25 of 29";
                    lblPageNo26.Text = "26 of 29";
                    lblPageNo27.Text = "27 of 29";
                    lblPageNo28.Text = "28 of 29";
                    lblPageNo29.Text = "29 of 29";
                }
                if (SOW_ItemCount >= 180)
                {
                    lblPageNo1.Text = "1 of 30";
                    lblPageNo2.Text = "2 of 30";
                    lblPageNo3.Text = "3 of 30";
                    lblPageNo4.Text = "4 of 30";
                    lblPageNo5.Text = "5 of 30";
                    lblPageNo6.Text = "6 of 30";
                    lblPageNo7.Text = "7 of 30";
                    lblPageNo8.Text = "8 of 30";
                    lblPageNo9.Text = "9 of 30";
                    lblPageNo10.Text = "10 of 30";
                    lblPageNo11.Text = "11 of 30";
                    lblPageNo12.Text = "12 of 30";
                    lblPageNo13.Text = "13 of 30";
                    lblPageNo14.Text = "14 of 30";
                    lblPageNo15.Text = "15 of 30";
                    lblPageNo16.Text = "16 of 30";
                    lblPageNo17.Text = "17 of 30";
                    lblPageNo18.Text = "18 of 30";
                    lblPageNo19.Text = "19 of 30";
                    lblPageNo20.Text = "20 of 30";
                    lblPageNo21.Text = "21 of 30";
                    lblPageNo22.Text = "22 of 30";
                    lblPageNo23.Text = "23 of 30";
                    lblPageNo24.Text = "24 of 30";
                    lblPageNo25.Text = "25 of 30";
                    lblPageNo26.Text = "26 of 30";
                    lblPageNo27.Text = "27 of 30";
                    lblPageNo28.Text = "28 of 30";
                    lblPageNo29.Text = "29 of 30";
                    lblPageNo30.Text = "30 of 30";
                }
                if (SOW_ItemCount >= 198)
                {
                    lblPageNo1.Text = "1 of 31";
                    lblPageNo2.Text = "2 of 31";
                    lblPageNo3.Text = "3 of 31";
                    lblPageNo4.Text = "4 of 31";
                    lblPageNo5.Text = "5 of 31";
                    lblPageNo6.Text = "6 of 31";
                    lblPageNo7.Text = "7 of 31";
                    lblPageNo8.Text = "8 of 31";
                    lblPageNo9.Text = "9 of 31";
                    lblPageNo10.Text = "10 of 31";
                    lblPageNo11.Text = "11 of 31";
                    lblPageNo12.Text = "12 of 31";
                    lblPageNo13.Text = "13 of 31";
                    lblPageNo14.Text = "14 of 31";
                    lblPageNo15.Text = "15 of 31";
                    lblPageNo16.Text = "16 of 31";
                    lblPageNo17.Text = "17 of 31";
                    lblPageNo18.Text = "18 of 31";
                    lblPageNo19.Text = "19 of 31";
                    lblPageNo20.Text = "20 of 31";
                    lblPageNo21.Text = "21 of 31";
                    lblPageNo22.Text = "22 of 31";
                    lblPageNo23.Text = "23 of 31";
                    lblPageNo24.Text = "24 of 31";
                    lblPageNo25.Text = "25 of 31";
                    lblPageNo26.Text = "26 of 31";
                    lblPageNo27.Text = "27 of 31";
                    lblPageNo28.Text = "28 of 31";
                    lblPageNo29.Text = "29 of 31";
                    lblPageNo30.Text = "30 of 31";
                    lblPageNo31.Text = "31 of 31";
                }
                if (SOW_ItemCount >= 216)
                {
                    lblPageNo1.Text = "1 of 32";
                    lblPageNo2.Text = "2 of 32";
                    lblPageNo3.Text = "3 of 32";
                    lblPageNo4.Text = "4 of 32";
                    lblPageNo5.Text = "5 of 32";
                    lblPageNo6.Text = "6 of 32";
                    lblPageNo7.Text = "7 of 32";
                    lblPageNo8.Text = "8 of 32";
                    lblPageNo9.Text = "9 of 32";
                    lblPageNo10.Text = "10 of 32";
                    lblPageNo11.Text = "11 of 32";
                    lblPageNo12.Text = "12 of 32";
                    lblPageNo13.Text = "13 of 32";
                    lblPageNo14.Text = "14 of 32";
                    lblPageNo15.Text = "15 of 32";
                    lblPageNo16.Text = "16 of 32";
                    lblPageNo17.Text = "17 of 32";
                    lblPageNo18.Text = "18 of 32";
                    lblPageNo19.Text = "19 of 32";
                    lblPageNo20.Text = "20 of 32";
                    lblPageNo21.Text = "21 of 32";
                    lblPageNo22.Text = "22 of 32";
                    lblPageNo23.Text = "23 of 32";
                    lblPageNo24.Text = "24 of 32";
                    lblPageNo25.Text = "25 of 32";
                    lblPageNo26.Text = "26 of 32";
                    lblPageNo27.Text = "27 of 32";
                    lblPageNo28.Text = "28 of 32";
                    lblPageNo29.Text = "29 of 32";
                    lblPageNo30.Text = "30 of 32";
                    lblPageNo31.Text = "31 of 32";
                    lblPageNo32.Text = "32 of 32";
                }
                if (SOW_ItemCount >= 234)
                {
                    lblPageNo1.Text = "1 of 33";
                    lblPageNo2.Text = "2 of 33";
                    lblPageNo3.Text = "3 of 33";
                    lblPageNo4.Text = "4 of 33";
                    lblPageNo5.Text = "5 of 33";
                    lblPageNo6.Text = "6 of 33";
                    lblPageNo7.Text = "7 of 33";
                    lblPageNo8.Text = "8 of 33";
                    lblPageNo9.Text = "9 of 33";
                    lblPageNo10.Text = "10 of 33";
                    lblPageNo11.Text = "11 of 33";
                    lblPageNo12.Text = "12 of 33";
                    lblPageNo13.Text = "13 of 33";
                    lblPageNo14.Text = "14 of 33";
                    lblPageNo15.Text = "15 of 33";
                    lblPageNo16.Text = "16 of 33";
                    lblPageNo17.Text = "17 of 33";
                    lblPageNo18.Text = "18 of 33";
                    lblPageNo19.Text = "19 of 33";
                    lblPageNo20.Text = "20 of 33";
                    lblPageNo21.Text = "21 of 33";
                    lblPageNo22.Text = "22 of 33";
                    lblPageNo23.Text = "23 of 33";
                    lblPageNo24.Text = "24 of 33";
                    lblPageNo25.Text = "25 of 33";
                    lblPageNo26.Text = "26 of 33";
                    lblPageNo27.Text = "27 of 33";
                    lblPageNo28.Text = "28 of 33";
                    lblPageNo29.Text = "29 of 33";
                    lblPageNo30.Text = "30 of 33";
                    lblPageNo31.Text = "31 of 33";
                    lblPageNo32.Text = "32 of 33";
                    lblPageNo33.Text = "33 of 33";
                }
                if (SOW_ItemCount >= 252)
                {
                    lblPageNo1.Text = "1 of 34";
                    lblPageNo2.Text = "2 of 34";
                    lblPageNo3.Text = "3 of 34";
                    lblPageNo4.Text = "4 of 34";
                    lblPageNo5.Text = "5 of 34";
                    lblPageNo6.Text = "6 of 34";
                    lblPageNo7.Text = "7 of 34";
                    lblPageNo8.Text = "8 of 34";
                    lblPageNo9.Text = "9 of 34";
                    lblPageNo10.Text = "10 of 34";
                    lblPageNo11.Text = "11 of 34";
                    lblPageNo12.Text = "12 of 34";
                    lblPageNo13.Text = "13 of 34";
                    lblPageNo14.Text = "14 of 34";
                    lblPageNo15.Text = "15 of 34";
                    lblPageNo16.Text = "16 of 34";
                    lblPageNo17.Text = "17 of 34";
                    lblPageNo18.Text = "18 of 34";
                    lblPageNo19.Text = "19 of 34";
                    lblPageNo20.Text = "20 of 34";
                    lblPageNo21.Text = "21 of 34";
                    lblPageNo22.Text = "22 of 34";
                    lblPageNo23.Text = "23 of 34";
                    lblPageNo24.Text = "24 of 34";
                    lblPageNo25.Text = "25 of 34";
                    lblPageNo26.Text = "26 of 34";
                    lblPageNo27.Text = "27 of 34";
                    lblPageNo28.Text = "28 of 34";
                    lblPageNo29.Text = "29 of 34";
                    lblPageNo30.Text = "30 of 34";
                    lblPageNo31.Text = "31 of 34";
                    lblPageNo32.Text = "32 of 34";
                    lblPageNo33.Text = "33 of 34";
                    lblPageNo34.Text = "34 of 34";
                }
                if (SOW_ItemCount >= 270)
                {
                    lblPageNo1.Text = "1 of 35";
                    lblPageNo2.Text = "2 of 35";
                    lblPageNo3.Text = "3 of 35";
                    lblPageNo4.Text = "4 of 35";
                    lblPageNo5.Text = "5 of 35";
                    lblPageNo6.Text = "6 of 35";
                    lblPageNo7.Text = "7 of 35";
                    lblPageNo8.Text = "8 of 35";
                    lblPageNo9.Text = "9 of 35";
                    lblPageNo10.Text = "10 of 35";
                    lblPageNo11.Text = "11 of 35";
                    lblPageNo12.Text = "12 of 35";
                    lblPageNo13.Text = "13 of 35";
                    lblPageNo14.Text = "14 of 35";
                    lblPageNo15.Text = "15 of 35";
                    lblPageNo16.Text = "16 of 35";
                    lblPageNo17.Text = "17 of 35";
                    lblPageNo18.Text = "18 of 35";
                    lblPageNo19.Text = "19 of 35";
                    lblPageNo20.Text = "20 of 35";
                    lblPageNo21.Text = "21 of 35";
                    lblPageNo22.Text = "22 of 35";
                    lblPageNo23.Text = "23 of 35";
                    lblPageNo24.Text = "24 of 35";
                    lblPageNo25.Text = "25 of 35";
                    lblPageNo26.Text = "26 of 35";
                    lblPageNo27.Text = "27 of 35";
                    lblPageNo28.Text = "28 of 35";
                    lblPageNo29.Text = "29 of 35";
                    lblPageNo30.Text = "30 of 35";
                    lblPageNo31.Text = "31 of 35";
                    lblPageNo32.Text = "32 of 35";
                    lblPageNo33.Text = "33 of 35";
                    lblPageNo34.Text = "34 of 35";
                    lblPageNo35.Text = "35 of 35";
                }
                if (SOW_ItemCount >= 288)
                {
                    lblPageNo1.Text = "1 of 36";
                    lblPageNo2.Text = "2 of 36";
                    lblPageNo3.Text = "3 of 36";
                    lblPageNo4.Text = "4 of 36";
                    lblPageNo5.Text = "5 of 36";
                    lblPageNo6.Text = "6 of 36";
                    lblPageNo7.Text = "7 of 36";
                    lblPageNo8.Text = "8 of 36";
                    lblPageNo9.Text = "9 of 36";
                    lblPageNo10.Text = "10 of 36";
                    lblPageNo11.Text = "11 of 36";
                    lblPageNo12.Text = "12 of 36";
                    lblPageNo13.Text = "13 of 36";
                    lblPageNo14.Text = "14 of 36";
                    lblPageNo15.Text = "15 of 36";
                    lblPageNo16.Text = "16 of 36";
                    lblPageNo17.Text = "17 of 36";
                    lblPageNo18.Text = "18 of 36";
                    lblPageNo19.Text = "19 of 36";
                    lblPageNo20.Text = "20 of 36";
                    lblPageNo21.Text = "21 of 36";
                    lblPageNo22.Text = "22 of 36";
                    lblPageNo23.Text = "23 of 36";
                    lblPageNo24.Text = "24 of 36";
                    lblPageNo25.Text = "25 of 36";
                    lblPageNo26.Text = "26 of 36";
                    lblPageNo27.Text = "27 of 36";
                    lblPageNo28.Text = "28 of 36";
                    lblPageNo29.Text = "29 of 36";
                    lblPageNo30.Text = "30 of 36";
                    lblPageNo31.Text = "31 of 36";
                    lblPageNo32.Text = "32 of 36";
                    lblPageNo33.Text = "33 of 36";
                    lblPageNo34.Text = "34 of 36";
                    lblPageNo35.Text = "35 of 36";
                    lblPageNo36.Text = "36 of 36";
                }
                if (SOW_ItemCount >= 306)
                {
                    lblPageNo1.Text = "1 of 37";
                    lblPageNo2.Text = "2 of 37";
                    lblPageNo3.Text = "3 of 37";
                    lblPageNo4.Text = "4 of 37";
                    lblPageNo5.Text = "5 of 37";
                    lblPageNo6.Text = "6 of 37";
                    lblPageNo7.Text = "7 of 37";
                    lblPageNo8.Text = "8 of 37";
                    lblPageNo9.Text = "9 of 37";
                    lblPageNo10.Text = "10 of 37";
                    lblPageNo11.Text = "11 of 37";
                    lblPageNo12.Text = "12 of 37";
                    lblPageNo13.Text = "13 of 37";
                    lblPageNo14.Text = "14 of 37";
                    lblPageNo15.Text = "15 of 37";
                    lblPageNo16.Text = "16 of 37";
                    lblPageNo17.Text = "17 of 37";
                    lblPageNo18.Text = "18 of 37";
                    lblPageNo19.Text = "19 of 37";
                    lblPageNo20.Text = "20 of 37";
                    lblPageNo21.Text = "21 of 37";
                    lblPageNo22.Text = "22 of 37";
                    lblPageNo23.Text = "23 of 37";
                    lblPageNo24.Text = "24 of 37";
                    lblPageNo25.Text = "25 of 37";
                    lblPageNo26.Text = "26 of 37";
                    lblPageNo27.Text = "27 of 37";
                    lblPageNo28.Text = "28 of 37";
                    lblPageNo29.Text = "29 of 37";
                    lblPageNo30.Text = "30 of 37";
                    lblPageNo31.Text = "31 of 37";
                    lblPageNo32.Text = "32 of 37";
                    lblPageNo33.Text = "33 of 37";
                    lblPageNo34.Text = "34 of 37";
                    lblPageNo35.Text = "35 of 37";
                    lblPageNo36.Text = "36 of 37";
                    lblPageNo37.Text = "37 of 37";
                }
                if (SOW_ItemCount >= 324)
                {
                    lblPageNo1.Text = "1 of 38";
                    lblPageNo2.Text = "2 of 38";
                    lblPageNo3.Text = "3 of 38";
                    lblPageNo4.Text = "4 of 38";
                    lblPageNo5.Text = "5 of 38";
                    lblPageNo6.Text = "6 of 38";
                    lblPageNo7.Text = "7 of 38";
                    lblPageNo8.Text = "8 of 38";
                    lblPageNo9.Text = "9 of 38";
                    lblPageNo10.Text = "10 of 38";
                    lblPageNo11.Text = "11 of 38";
                    lblPageNo12.Text = "12 of 38";
                    lblPageNo13.Text = "13 of 38";
                    lblPageNo14.Text = "14 of 38";
                    lblPageNo15.Text = "15 of 38";
                    lblPageNo16.Text = "16 of 38";
                    lblPageNo17.Text = "17 of 38";
                    lblPageNo18.Text = "18 of 38";
                    lblPageNo19.Text = "19 of 38";
                    lblPageNo20.Text = "20 of 38";
                    lblPageNo21.Text = "21 of 38";
                    lblPageNo22.Text = "22 of 38";
                    lblPageNo23.Text = "23 of 38";
                    lblPageNo24.Text = "24 of 38";
                    lblPageNo25.Text = "25 of 38";
                    lblPageNo26.Text = "26 of 38";
                    lblPageNo27.Text = "27 of 38";
                    lblPageNo28.Text = "28 of 38";
                    lblPageNo29.Text = "29 of 38";
                    lblPageNo30.Text = "30 of 38";
                    lblPageNo31.Text = "31 of 38";
                    lblPageNo32.Text = "32 of 38";
                    lblPageNo33.Text = "33 of 38";
                    lblPageNo34.Text = "34 of 38";
                    lblPageNo35.Text = "35 of 38";
                    lblPageNo36.Text = "36 of 38";
                    lblPageNo37.Text = "37 of 38";
                    lblPageNo38.Text = "38 of 38";
                }
                if (SOW_ItemCount >= 360)
                {
                    lblPageNo1.Text = "1 of 39";
                    lblPageNo2.Text = "2 of 39";
                    lblPageNo3.Text = "3 of 39";
                    lblPageNo4.Text = "4 of 39";
                    lblPageNo5.Text = "5 of 39";
                    lblPageNo6.Text = "6 of 39";
                    lblPageNo7.Text = "7 of 39";
                    lblPageNo8.Text = "8 of 39";
                    lblPageNo9.Text = "9 of 39";
                    lblPageNo10.Text = "10 of 39";
                    lblPageNo11.Text = "11 of 39";
                    lblPageNo12.Text = "12 of 39";
                    lblPageNo13.Text = "13 of 39";
                    lblPageNo14.Text = "14 of 39";
                    lblPageNo15.Text = "15 of 39";
                    lblPageNo16.Text = "16 of 39";
                    lblPageNo17.Text = "17 of 39";
                    lblPageNo18.Text = "18 of 39";
                    lblPageNo19.Text = "19 of 39";
                    lblPageNo20.Text = "20 of 39";
                    lblPageNo21.Text = "21 of 39";
                    lblPageNo22.Text = "22 of 39";
                    lblPageNo23.Text = "23 of 39";
                    lblPageNo24.Text = "24 of 39";
                    lblPageNo25.Text = "25 of 39";
                    lblPageNo26.Text = "26 of 39";
                    lblPageNo27.Text = "27 of 39";
                    lblPageNo28.Text = "28 of 39";
                    lblPageNo29.Text = "29 of 39";
                    lblPageNo30.Text = "30 of 39";
                    lblPageNo31.Text = "31 of 39";
                    lblPageNo32.Text = "32 of 39";
                    lblPageNo33.Text = "33 of 39";
                    lblPageNo34.Text = "34 of 39";
                    lblPageNo35.Text = "35 of 39";
                    lblPageNo36.Text = "36 of 39";
                    lblPageNo37.Text = "37 of 39";
                    lblPageNo38.Text = "38 of 39";
                    lblPageNo39.Text = "39 of 39";
                }
                if (SOW_ItemCount >= 378)
                {
                    lblPageNo1.Text = "1 of 40";
                    lblPageNo2.Text = "2 of 40";
                    lblPageNo3.Text = "3 of 40";
                    lblPageNo4.Text = "4 of 40";
                    lblPageNo5.Text = "5 of 40";
                    lblPageNo6.Text = "6 of 40";
                    lblPageNo7.Text = "7 of 40";
                    lblPageNo8.Text = "8 of 40";
                    lblPageNo9.Text = "9 of 40";
                    lblPageNo10.Text = "10 of 40";
                    lblPageNo11.Text = "11 of 40";
                    lblPageNo12.Text = "12 of 40";
                    lblPageNo13.Text = "13 of 40";
                    lblPageNo14.Text = "14 of 40";
                    lblPageNo15.Text = "15 of 40";
                    lblPageNo16.Text = "16 of 40";
                    lblPageNo17.Text = "17 of 40";
                    lblPageNo18.Text = "18 of 40";
                    lblPageNo19.Text = "19 of 40";
                    lblPageNo20.Text = "20 of 40";
                    lblPageNo21.Text = "21 of 40";
                    lblPageNo22.Text = "22 of 40";
                    lblPageNo23.Text = "23 of 40";
                    lblPageNo24.Text = "24 of 40";
                    lblPageNo25.Text = "25 of 40";
                    lblPageNo26.Text = "26 of 40";
                    lblPageNo27.Text = "27 of 40";
                    lblPageNo28.Text = "28 of 40";
                    lblPageNo29.Text = "29 of 40";
                    lblPageNo30.Text = "30 of 40";
                    lblPageNo31.Text = "31 of 40";
                    lblPageNo32.Text = "32 of 40";
                    lblPageNo33.Text = "33 of 40";
                    lblPageNo34.Text = "34 of 40";
                    lblPageNo35.Text = "35 of 40";
                    lblPageNo36.Text = "36 of 40";
                    lblPageNo37.Text = "37 of 40";
                    lblPageNo38.Text = "38 of 40";
                    lblPageNo39.Text = "39 of 40";
                    lblPageNo40.Text = "40 of 40";
                }
                if (SOW_ItemCount >= 396)
                {
                    lblPageNo1.Text = "1 of 41";
                    lblPageNo2.Text = "2 of 41";
                    lblPageNo3.Text = "3 of 41";
                    lblPageNo4.Text = "4 of 41";
                    lblPageNo5.Text = "5 of 41";
                    lblPageNo6.Text = "6 of 41";
                    lblPageNo7.Text = "7 of 41";
                    lblPageNo8.Text = "8 of 41";
                    lblPageNo9.Text = "9 of 41";
                    lblPageNo10.Text = "10 of 41";
                    lblPageNo11.Text = "11 of 41";
                    lblPageNo12.Text = "12 of 41";
                    lblPageNo13.Text = "13 of 41";
                    lblPageNo14.Text = "14 of 41";
                    lblPageNo15.Text = "15 of 41";
                    lblPageNo16.Text = "16 of 41";
                    lblPageNo17.Text = "17 of 41";
                    lblPageNo18.Text = "18 of 41";
                    lblPageNo19.Text = "19 of 41";
                    lblPageNo20.Text = "20 of 41";
                    lblPageNo21.Text = "21 of 41";
                    lblPageNo22.Text = "22 of 41";
                    lblPageNo23.Text = "23 of 41";
                    lblPageNo24.Text = "24 of 41";
                    lblPageNo25.Text = "25 of 41";
                    lblPageNo26.Text = "26 of 41";
                    lblPageNo27.Text = "27 of 41";
                    lblPageNo28.Text = "28 of 41";
                    lblPageNo29.Text = "29 of 41";
                    lblPageNo30.Text = "30 of 41";
                    lblPageNo31.Text = "31 of 41";
                    lblPageNo32.Text = "32 of 41";
                    lblPageNo33.Text = "33 of 41";
                    lblPageNo34.Text = "34 of 41";
                    lblPageNo35.Text = "35 of 41";
                    lblPageNo36.Text = "36 of 41";
                    lblPageNo37.Text = "37 of 41";
                    lblPageNo38.Text = "38 of 41";
                    lblPageNo39.Text = "39 of 41";
                    lblPageNo40.Text = "40 of 41";
                    lblPageNo41.Text = "41 of 41";
                }
            }
            else if (WO_ItemCount >= 6 && (SOW_ItemCount < 20 && SOW_ItemCount != 0))
            {
                lblPageNo1.Text = "1 of 20";
                lblPageNo2.Text = "2 of 20";
                lblPageNo3.Text = "3 of 20";
                lblPageNo4.Text = "4 of 20";
                lblPageNo5.Text = "5 of 20";
                lblPageNo6.Text = "6 of 20";
                lblPageNo7.Text = "7 of 20";
                lblPageNo8.Text = "8 of 20";
                lblPageNo9.Text = "9 of 20";
                lblPageNo10.Text = "10 of 20";
                lblPageNo11.Text = "11 of 20";
                lblPageNo12.Text = "12 of 20";
                lblPageNo13.Text = "13 of 20";
                lblPageNo14.Text = "14 of 20";
                lblPageNo15.Text = "15 of 20";
                lblPageNo16.Text = "16 of 20";
                lblPageNo17.Text = "17 of 20";
                lblPageNo18.Text = "18 of 20";
                lblPageNo19.Text = "19 of 20";
                lblPageNo20.Text = "20 of 20";
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

}
