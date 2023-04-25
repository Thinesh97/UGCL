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
namespace SNC.SubContractorBills
{
    public partial class SC_BillPrint : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        DataSet ds = null;
        CompanyBL objCompany = null;
        WorkOrderBL objWO = null;
        GeneralClass objGen = new GeneralClass();
        decimal TotalAmt = 0.0m;
        decimal TotalBilledAmt = 0.0m;
        decimal TotalPaiddAmt = 0.0m;
        decimal PendingAmount = 0.0m;
        decimal TotalTaxAmt = 0.0m;
        decimal TDSPerc = 0.0m;
        int WO_ItemCount = 0;
        int SOW_ItemCount = 0;
        decimal RateCardTotal = 0.0m;
        decimal MINTotal = 0.0m;
        decimal TotalOF_G = 0.0m;
        decimal TotalOF_C = 0.0m;
        decimal TotalOF_H = 0.0m;
        decimal TotalOF_I = 0.0m;
        decimal TotalOF_J = 0.0m;
        decimal CurrentInvoiceAmount = 0.0m;
        decimal TotalLabourCost = 0.0m;
        decimal NetPayableAmount = 0.0m;
        SubContractorBillBL objSubContBL = new SubContractorBillBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    if (Request.QueryString["Bill_No"] != null)
                    {
                        divToPrint1.Visible = false;
                        div_NMR.Visible = false;
                        GetCompanyDetails();
                        GetSC_Bill_Details(Request.QueryString["Bill_No"]);
                        GetSC_Bill_RateCard_Items();
                        GetSC_Completed_Payment_Details();
                        GetSC_DPR_Items();
                        GetTaxDetails();
                        Get_Grid_SC_MIN_Details();
                        BindGrid_Labour_List();

                    }
                }
                else
                {
                    Response.Redirect("../CommonPages/Login.aspx", false);
                }
            }
        }
        private void GetSC_Bill_Details(string RA_Bill_NoRQ)
        {
            try
            {
                objSubContBL = new SubContractorBillBL();
                ds = new DataSet();
                objSubContBL.RA_Bill_No = RA_Bill_NoRQ;
                objSubContBL.Task = "GetSC_Details_By_BillID";
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.GET_SC_BILL_DETAIL, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblSubContactorName.Text = ds.Tables[0].Rows[0]["Subcon_name"].ToString();
                    lblSubContactorName_NMR.Text = ds.Tables[0].Rows[0]["Subcon_name"].ToString();
                    lblSubContactorID.Text = ds.Tables[0].Rows[0]["Subcon_ID"].ToString();
                    lblSubContactorID_NMR.Text = ds.Tables[0].Rows[0]["Subcon_ID"].ToString();
                    lblSubContactorAddess.Text = ds.Tables[0].Rows[0]["Add_Line"].ToString();
                    lblSubContactorAddess_NMR.Text = ds.Tables[0].Rows[0]["Add_Line"].ToString();
                    lblSubContactorCity.Text = ds.Tables[0].Rows[0]["City"].ToString();
                    lblSubContactorCity_NMR.Text = ds.Tables[0].Rows[0]["City"].ToString();
                    lblSubContactorState_NMR.Text = ds.Tables[0].Rows[0]["State"].ToString();
                    lblSubContactorState.Text = ds.Tables[0].Rows[0]["State"].ToString();
                    lblSubContactorCountry.Text = ds.Tables[0].Rows[0]["Country"].ToString();
                    lblSubContactorCountry_NMR.Text = ds.Tables[0].Rows[0]["Country"].ToString();
                    lblSubContactorMobileNumber.Text = ds.Tables[0].Rows[0]["Con_No"].ToString();
                    lblSubContactorMobileNumber_NMR.Text = ds.Tables[0].Rows[0]["Con_No"].ToString();
                    lblSubContractorGST.Text = ds.Tables[0].Rows[0]["Regs_No"].ToString();
                    lblSubContractorGST_NMR.Text = ds.Tables[0].Rows[0]["Regs_No"].ToString();
                    lblSubContractorPAN.Text = ds.Tables[0].Rows[0]["PAN_No"].ToString();
                    lblSubContractorPANr_NMR.Text = ds.Tables[0].Rows[0]["PAN_No"].ToString();
                    lblSCBillDate.Text = ds.Tables[0].Rows[0]["SC_Bill_Date"].ToString();
                    lblSCBillDate_NMR.Text = ds.Tables[0].Rows[0]["SC_Bill_Date"].ToString();
                    lblBillingPeriodTo.Text = ds.Tables[0].Rows[0]["SC_Billing_To_Date"].ToString();
                    lblBillingPeriodTo_NMR.Text = ds.Tables[0].Rows[0]["SC_Billing_To_Date"].ToString();
                    lblBillingPeriodFrom.Text = ds.Tables[0].Rows[0]["SC_BillingFrom_Date"].ToString();
                    lblBillingPeriodFrom_NMR.Text = ds.Tables[0].Rows[0]["SC_BillingFrom_Date"].ToString();
                    lblProject.Text = ds.Tables[0].Rows[0]["Project_Name"].ToString();
                    lblProject_NMR.Text = ds.Tables[0].Rows[0]["Project_Name"].ToString();
                    lblSUCBankName.InnerText = ds.Tables[0].Rows[0]["Bank"].ToString();
                    lblSUCBankName_NMR.InnerText = ds.Tables[0].Rows[0]["Bank"].ToString();
                    lblSUCBankACNumber.InnerText = ds.Tables[0].Rows[0]["Acc_No"].ToString();
                    lblSUCBankACNumber_NMR.InnerText = ds.Tables[0].Rows[0]["Acc_No"].ToString();
                    lblSCIFSCCode.InnerText = ds.Tables[0].Rows[0]["IFSC"].ToString();
                    lblSCIFSCCode_NMR.InnerText = ds.Tables[0].Rows[0]["IFSC"].ToString();
                    lblScnmae2.InnerText = ds.Tables[0].Rows[0]["Subcon_name"].ToString();
                    lblScnmae2NMR.InnerText = ds.Tables[0].Rows[0]["Subcon_name"].ToString();
                    lblInvoiceNumber.InnerHtml= Request.QueryString["Bill_No"].ToString();
                    lblInvoiceNumber_NMR.InnerHtml = Request.QueryString["Bill_No"].ToString();
                    lblDate.InnerText = ds.Tables[0].Rows[0]["SC_Bill_Date"].ToString();
                    lblDate_NMR.InnerText = ds.Tables[0].Rows[0]["SC_Bill_Date"].ToString(); 
                    lblSCWork.InnerText = ds.Tables[0].Rows[0]["SC_Work_Description"].ToString();
                    lblSCWork_NMR.InnerText = ds.Tables[0].Rows[0]["SC_Work_Description"].ToString();
                    lblWOnumber_NMR.InnerText = ds.Tables[0].Rows[0]["SC_Work_Description"].ToString();
                    lblWOnumber.InnerText = ds.Tables[0].Rows[0]["WorkOrderNo"].ToString();  
                    lblScnmae2MIN.InnerText = ds.Tables[0].Rows[0]["Subcon_name"].ToString();
                    lblInvoiceNumberMIN.InnerHtml= Request.QueryString["Bill_No"].ToString();
                    lblSCWorkMIN.InnerText = ds.Tables[0].Rows[0]["SC_Work_Description"].ToString();
                    lblDateMIN.InnerText = ds.Tables[0].Rows[0]["SC_Bill_Date"].ToString();
                    lblWOnumberMIN.InnerText = ds.Tables[0].Rows[0]["WorkOrderNo"].ToString();
                    lblScnmae2PY.InnerText = ds.Tables[0].Rows[0]["Subcon_name"].ToString();
                    lblInvoiceNumberPY.InnerHtml = Request.QueryString["Bill_No"].ToString();
                    lblSCWorkPY.InnerText = ds.Tables[0].Rows[0]["SC_Work_Description"].ToString();
                    lblDatePY.InnerText = ds.Tables[0].Rows[0]["SC_Bill_Date"].ToString();
                    lblMainSCWork.InnerText = ds.Tables[0].Rows[0]["SC_Work_Description"].ToString();
                    lblMainSCWork_NMR.InnerText = ds.Tables[0].Rows[0]["SC_Work_Description"].ToString();
                    lblWOnumberPY.InnerText = ds.Tables[0].Rows[0]["WorkOrderNo"].ToString();

                    if (ds.Tables[0].Rows[0]["DPR"].ToString() == "True")
                    {
                        divToPrint2.Visible = true;
                    }
                    else
                    {
                        divToPrint2.Visible = false;
                    }
                    if (ds.Tables[0].Rows[0]["NMR"].ToString() == "True")
                    {
                        div_NMR.Visible = true;
                    }
                    else
                    {
                        div_NMR.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        private void GetCompanyDetails()
        {
            try
            {
                objCompany = new CompanyBL();
                ds = new DataSet();
                objCompany.load(con, CompanyBL.eLoadSp.SELECT_ALL, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblAddressLine1_Company_SC.Text = ds.Tables[0].Rows[0]["AddressLine1"].ToString();
                    lblAddressLine2_Company_SC.Text = ds.Tables[0].Rows[0]["AddressLine2"].ToString();
                    lblGSTIN_Company_SC.Text = ds.Tables[0].Rows[0]["CST_No"].ToString();
                    lblTAN_Company_SC.Text = ds.Tables[0].Rows[0]["TAN_No"].ToString();
                    lblState_Company_SC.Text = ds.Tables[0].Rows[0]["State_Name"].ToString();
                    lblCode_Company_SC.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                    lblEmail_Company_SC.Text = ds.Tables[0].Rows[0]["Email_Id"].ToString();

                    //
                    lblAddressLine1_Company_SC_NMR.Text = ds.Tables[0].Rows[0]["AddressLine1"].ToString();
                    lblAddressLine2_Company_SC_NMR.Text = ds.Tables[0].Rows[0]["AddressLine2"].ToString();
                    lblGSTIN_Company_SC_NMR.Text = ds.Tables[0].Rows[0]["CST_No"].ToString();
                    lblTAN_Company_SC_NMR.Text = ds.Tables[0].Rows[0]["TAN_No"].ToString();
                    lblState_Company_SC_NMR.Text = ds.Tables[0].Rows[0]["State_Name"].ToString();
                    lblCode_Company_SC_NMR.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                    lblEmail_Company_SC_NMR.Text = ds.Tables[0].Rows[0]["Email_Id"].ToString();


                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        private void GetTaxDetails()
        {
            try
            {
                objSubContBL = new SubContractorBillBL();
                ds = new DataSet();
               objSubContBL.RA_Bill_No= Request.QueryString["Bill_No"].ToString();
                objSubContBL.Task = "GetTax_SC";
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.GETTAX_SC, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblSC_SGST.InnerText = ds.Tables[0].Rows[0]["SGST"].ToString();
                    lblSC_SGST_NMR.InnerText = ds.Tables[0].Rows[0]["SGST"].ToString();
                    lblSC_IGST.InnerText = ds.Tables[0].Rows[0]["IGST"].ToString();
                    lblSC_IGST_NMR.InnerText = ds.Tables[0].Rows[0]["IGST"].ToString();
                    lblSC_TDS.InnerText = ds.Tables[0].Rows[0]["TDS"].ToString();
                    lblSC_TDS_NMR.InnerText = ds.Tables[0].Rows[0]["TDS"].ToString();
                    lblSC_TDS_Note.InnerText = ds.Tables[0].Rows[0]["TDS"].ToString();
                    lblSC_TDS_Note_NMR.InnerText = ds.Tables[0].Rows[0]["TDS"].ToString();
                    lblSC_CGST.InnerText = ds.Tables[0].Rows[0]["CGST"].ToString();
                    lblSC_CGST_NMR.InnerText = ds.Tables[0].Rows[0]["CGST"].ToString();
                    lblRetentionPerc.InnerText = ds.Tables[0].Rows[0]["RetentionPerc"].ToString();
                    lblRetentionPerc_NMR.InnerText = ds.Tables[0].Rows[0]["RetentionPerc"].ToString();
                    lblRetentionAmont.InnerText = ds.Tables[0].Rows[0]["RetentionAmount"].ToString();
                    //lblRetentionAmont_NMR.InnerText = ds.Tables[0].Rows[0]["RetentionAmount"].ToString();
                    if ( ds.Tables[0].Rows[0]["RetentionType"].ToString()=="Percentage")
                    {
                        if (Session["RateCardTotal"].ToString() != "")
                        {
                            decimal CalcIRetentionPer= Convert.ToDecimal(ds.Tables[0].Rows[0]["RetentionPerc"].ToString());
                            decimal BillAmount = Convert.ToDecimal(Session["RateCardTotal"].ToString());
                            lblRetentionAmont.InnerText = (BillAmount * CalcIRetentionPer / 100).ToString();
                             TotalOF_I =Convert.ToDecimal((BillAmount * CalcIRetentionPer / 100).ToString());
                           
                        }
                    }
	
                    //lblSC_SGST.InnerText = ds.Tables[0].Rows[0]["TDS"].ToString();
                    if (Session["RateCardTotal"] !=null)
                    {
                        decimal CalcCGST =0;
                        decimal BillAmount=0;
                        decimal CalcTDS = 0;
                        decimal CalcIGST = 0;
                        decimal CalcSGST = 0;
                        

                        if (!string.IsNullOrEmpty(Session["RateCardTotal"].ToString()))
                        {
                             BillAmount =Convert.ToDecimal(Session["RateCardTotal"].ToString());
                        }
                        if (   ds.Tables[0].Rows[0]["SGST"] !=null && ds.Tables[0].Rows[0]["SGST"] !="")
                        {
                              CalcIGST = Convert.ToDecimal(ds.Tables[0].Rows[0]["SGST"].ToString());
                            
                        }
                        if (ds.Tables[0].Rows[0]["IGST"] != null && ds.Tables[0].Rows[0]["IGST"] != "") ;
                        {
                             CalcIGST = Convert.ToDecimal(ds.Tables[0].Rows[0]["IGST"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TDS"].ToString()))
                        {
                             CalcTDS = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDS"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CGST"].ToString()))
                        {
                             CalcCGST = Convert.ToDecimal(ds.Tables[0].Rows[0]["CGST"].ToString());
                        }
                        
                        lblSC_SGST_Amount.InnerText = (BillAmount * CalcSGST / 100).ToString();
                        lblSC_IGST_Amount.InnerText = (BillAmount * CalcIGST / 100).ToString();
                        lblTDS_amount.InnerText = (BillAmount * CalcTDS / 100).ToString();
                        lblSC_CGST_Amount.InnerText = (BillAmount * CalcCGST / 100).ToString();
                        decimal SGST =Convert.ToDecimal((BillAmount * CalcSGST / 100).ToString());
                        decimal IGST = Convert.ToDecimal((BillAmount * CalcIGST / 100).ToString());
                        decimal CGST=Convert.ToDecimal((BillAmount * CalcCGST / 100).ToString());
                         TotalOF_J = Convert.ToDecimal((BillAmount * CalcTDS / 100).ToString());
                        lblGSTTotal.InnerText = (SGST + IGST + CGST).ToString();
                        TotalOF_G = Convert.ToDecimal((SGST + IGST + CGST).ToString());
                    }

                    if (Session["TotalLabourCost"] != null)
                    {
                        decimal CalcCGST = 0;
                        decimal BillAmount = 0;
                        decimal CalcTDS = 0;
                        decimal CalcIGST = 0;
                        decimal CalcSGST = 0;
                        if (!string.IsNullOrEmpty(Session["TotalLabourCost"].ToString()))
                        {
                            BillAmount = Convert.ToDecimal(Session["TotalLabourCost"].ToString());
                        }
                        if (ds.Tables[0].Rows[0]["SGST"] != null && ds.Tables[0].Rows[0]["SGST"] != "")
                        {
                            CalcIGST = Convert.ToDecimal(ds.Tables[0].Rows[0]["SGST"].ToString());

                        }
                        if (ds.Tables[0].Rows[0]["IGST"] != null && ds.Tables[0].Rows[0]["IGST"] != "") ;
                        {
                            CalcIGST = Convert.ToDecimal(ds.Tables[0].Rows[0]["IGST"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TDS"].ToString()))
                        {
                            CalcTDS = Convert.ToDecimal(ds.Tables[0].Rows[0]["TDS"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CGST"].ToString()))
                        {
                            CalcCGST = Convert.ToDecimal(ds.Tables[0].Rows[0]["CGST"].ToString());
                        }

                        lblSC_SGST_Amount_NMR.InnerText = (BillAmount * CalcSGST / 100).ToString();
                        lblSC_IGST_Amount_NMR.InnerText = (BillAmount * CalcIGST / 100).ToString();
                        lblTDS_amount.InnerText = (BillAmount * CalcTDS / 100).ToString();
                        lblSC_CGST_Amount_NMR.InnerText = (BillAmount * CalcCGST / 100).ToString();
                        decimal SGST = Convert.ToDecimal((BillAmount * CalcSGST / 100).ToString());
                        decimal IGST = Convert.ToDecimal((BillAmount * CalcIGST / 100).ToString());
                        decimal CGST = Convert.ToDecimal((BillAmount * CalcCGST / 100).ToString());
                        TotalOF_J = Convert.ToDecimal((BillAmount * CalcTDS / 100).ToString());
                        lblGSTTotal_NMR.InnerText = (SGST + IGST + CGST).ToString();
                     decimal TotalOF_GST = Convert.ToDecimal((SGST + IGST + CGST).ToString());
                        lblGSTTotal_NMR.InnerText=TotalOF_GST.ToString();
                        Decimal TotalLabourCost = Convert.ToDecimal(Session["TotalLabourCost"]);
                        decimal TotalOfL =Convert.ToDecimal(TotalLabourCost - TotalOF_GST);
                        lblTotalAmont_H_NMR.InnerText =Convert.ToString(TotalOfL);
                        lblNetAmount_NMR.InnerText = Convert.ToString(TotalOfL);
                    }
                   
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        public int TruncateDecimal(decimal value, int precision)
        {
            int step = (int)Math.Pow(10, precision);
            int tmp = (int)Math.Truncate(step * value);
            return tmp / step;
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
        private void GetSC_Bill_RateCard_Items()
        {

            if (Session["DPR_Value"] == "Selected")
            {
                try
                {
                    
                    objSubContBL = new SubContractorBillBL();
                    ds = new DataSet();
                    objSubContBL.RateCard_ID = Request.QueryString["Bill_No"].ToString();
                    objSubContBL.Task = "Get_SC_Bill_RateCard";
                    objSubContBL.load(con, SubContractorBillBL.eLoadSp.GET_SC_BILL_DETAIL_RateCard_Items, ref ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        divToPrint1.Visible = true;
                        WorkDoneGrid.DataSource = ds.Tables[0];
                        WorkDoneGrid.DataBind();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            decimal RowTotalAmont = Convert.ToDecimal(row["TotalRate"]);
                            RateCardTotal += Convert.ToDecimal(RowTotalAmont);
                        }
                        lblRateCardTotal.InnerText = RateCardTotal.ToString();
                        Session["RateCardTotal"] = RateCardTotal.ToString();
                    }
                    else
                    {
                        WorkDoneGrid.DataSource = ds.Tables[0];
                        WorkDoneGrid.DataBind();
                        divToPrint1.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
                }
               
            }
            if (Session["NMR_Value"] == "Selected")
            {
                try
                {

                    objSubContBL = new SubContractorBillBL();
                    ds = new DataSet();
                    //

                    objSubContBL.BillingFrom_Date = Convert.ToDateTime(Session["txtBillingPeriodFrom"]);
                    objSubContBL.Billing_To_Date = Convert.ToDateTime(Session["txtBillingPeriodTo"]);
                    objSubContBL.SubContractorID = Session["ddlSubContractor"].ToString();
                    objSubContBL.WO_NMR = Session["ddlWO"].ToString();
                    objSubContBL.Task = "Get_SC_Bill_NMR";
                    objSubContBL.load(con, SubContractorBillBL.eLoadSp.GET_SC_BILL_DETAIL_RateCard_Items_NMR, ref ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        div_NMR.Visible = true;
                        GridNMR.DataSource = ds.Tables[0];
                        GridNMR.DataBind();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            decimal TotalLabourCostAmont = Convert.ToDecimal(row["TotalLabourCost"]);
                            TotalLabourCost += Convert.ToDecimal(TotalLabourCostAmont);
                        }
                        lblRateCardTotal_NMR.InnerText = TotalLabourCost.ToString();
                        Session["TotalLabourCost"] = TotalLabourCost.ToString();
                    }
                    else
                    {
                        WorkDoneGrid.DataSource = ds.Tables[0];
                        WorkDoneGrid.DataBind();
                        divToPrint1.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
                }
               
            }
           
        }
        private void GetSC_DPR_Items()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();
                objSubContBL.Task = "Get_DPR_Details_SC_BillPrint";
                objSubContBL.BillingFrom_Date = Convert.ToDateTime(Session["txtBillingPeriodFrom"]);
                objSubContBL.Billing_To_Date = Convert.ToDateTime(Session["txtBillingPeriodTo"]);
                objSubContBL.WONo = Session["ddlWO"].ToString();
                objSubContBL.SubContractorID = Session["ddlSubContractor"].ToString();
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_DPR_DETAILS_SC_PRINT, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridDPR_Items.DataSource = ds.Tables[0];
                    GridDPR_Items.DataBind();
                    divToPrint2.Visible = true;
                }
                else
                {
                    GridDPR_Items.DataSource = ds.Tables[0];
                    GridDPR_Items.DataBind();
                    divToPrint2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }

            //try
            //{
            //    objSubContBL = new SubContractorBillBL();
            //    ds = new DataSet();
            //    objSubContBL.RateCard_ID = Request.QueryString["Bill_No"].ToString();
            //    objSubContBL.Task = "Get_SC_Bill_DPR";
            //    objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_DPR_DETAILS, ref ds);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        GridDPR_Items.DataSource = ds.Tables[0];
            //        GridDPR_Items.DataBind();
            //    }
               
            //}
            //catch (Exception ex)
            //{
            //    SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            //}
        }
        private void GetSC_Completed_Payment_Details()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();
                objSubContBL.Task = "Get_Payment_Indent";
                objSubContBL.BillingFrom_Date = Convert.ToDateTime(Session["txtBillingPeriodFrom"]);
                objSubContBL.Billing_To_Date = Convert.ToDateTime(Session["txtBillingPeriodTo"]);
                objSubContBL.SubContractorID = Session["ddlSubContractor"].ToString();
                objSubContBL.WONo = Session["ddlWO"].ToString();
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_PAYMENT_INDENT, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Grid_CompletedPayment.DataSource = ds.Tables[0];
                    Grid_CompletedPayment.DataBind();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                         decimal PaymentSingleTotalBilledAmt =  !string.IsNullOrEmpty(row["Amt_ServiceMaterial"].ToString()) ? Convert.ToInt32(row["Amt_ServiceMaterial"]) : 0;
                         decimal PaymentSingleTotalPaiddAmt = !string.IsNullOrEmpty(row["Amt_Transferable"].ToString()) ? Convert.ToInt32(row["Amt_Transferable"]) : 0;
                        TotalBilledAmt = TotalBilledAmt + Convert.ToDecimal(PaymentSingleTotalBilledAmt);
                        TotalPaiddAmt = TotalPaiddAmt + Convert.ToDecimal(PaymentSingleTotalPaiddAmt);
                    }
                    lblTotalPaidAmont.InnerText = TotalPaiddAmt.ToString();
                    lblTotalBilledAmont.InnerText = TotalBilledAmt.ToString();
                    PendingAmount = (TotalBilledAmt - TotalPaiddAmt);
                    //lblPendingAmount.InnerText = PendingAmount.ToString();
                    
                   
                }
                else
                {
                    Grid_CompletedPayment.DataSource = ds.Tables[0];
                    Grid_CompletedPayment.DataBind();
                    lblTotalPaidAmont.InnerText = "0";
                    lblTotalBilledAmont.InnerText = "0";
                    lblPendingAmount.InnerText = "0";
                    divToPrint4.Visible = false;
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }

           
        }
        private void Get_Grid_SC_MIN_Details()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();
                objSubContBL.Task = "Get_MIN";
                objSubContBL.BillingFrom_Date = Convert.ToDateTime(Session["txtBillingPeriodFrom"]);
                objSubContBL.Billing_To_Date = Convert.ToDateTime(Session["txtBillingPeriodTo"]);
                objSubContBL.SubContractorID = Session["ddlSubContractor"].ToString();
                objSubContBL.WONo = Session["ddlWO"].ToString();
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_PAYMENT_INDENT, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    Grid_SC_MIN.DataSource = ds;
                    Grid_SC_MIN.DataBind();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        decimal MinLineTotal = !string.IsNullOrEmpty(row["TotalValueR"].ToString()) ? Convert.ToInt32(row["TotalValueR"]) : 0;
                        MINTotal = MINTotal + Convert.ToDecimal(MinLineTotal);
                       
                    }
                    lblTotalTaxableAmt.InnerText = (RateCardTotal - MINTotal).ToString();
                    TotalOF_C = Convert.ToDecimal((RateCardTotal - MINTotal).ToString());
                    //lblGSTTotal.InnerText = Convert.ToString((TotalOF_C - TotalOF_G));
                    lblDeductions.InnerText = MINTotal.ToString();
                    lblTotalAmont_H.InnerText = Convert.ToString((TotalOF_C - TotalOF_G));
                    TotalOF_H = Convert.ToDecimal((TotalOF_C - TotalOF_G));
                    lblNetAmount.InnerText =Convert.ToString((TotalOF_H - (TotalOF_I + TotalOF_J)));
                    lblCurrentInvoiceAmount.InnerText = Convert.ToString((TotalOF_H - (TotalOF_I + TotalOF_J)));
                    CurrentInvoiceAmount = Convert.ToDecimal((TotalOF_H - (TotalOF_I + TotalOF_J)));
                    NetPayableAmount = Convert.ToDecimal(CurrentInvoiceAmount + TotalBilledAmt);
                    lblPendingAmount.InnerText = Convert.ToString(Convert.ToDecimal(NetPayableAmount - TotalPaiddAmt));
                    //lblTotalAmount.InnerText = TotalOF_H.ToString();
                }
                else
                {
                    Grid_SC_MIN.DataSource = ds.Tables[0];
                    Grid_SC_MIN.DataBind();
                    divToPrint3.Visible = false;
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }

           
        }

        protected void BindGrid_Labour_List()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();
                objSubContBL.BillingFrom_Date = Convert.ToDateTime(Session["txtBillingPeriodFrom"]);
                objSubContBL.Billing_To_Date = Convert.ToDateTime(Session["txtBillingPeriodTo"]);
                objSubContBL.Task = "SelectAll_Labour_By_NMR_ID_Print";
                objSubContBL.WONo = Convert.ToString(lblWOnumber.InnerText);
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SUB_CONTRACTOR_BILL_LABOUR_LIST, ref ds);
                Grid_LabourList.DataSource = ds;
                Grid_LabourList.DataBind();
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
    }
}