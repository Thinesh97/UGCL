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

namespace SNC.Procurement
{
    public partial class Bank_Advice_Print : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        PaymentIndentBL objPaymentIndentBL = null;
        DataSet ds = null;
        DataSet Accessds = new DataSet();
        private decimal Total_Amount = 0;
        GeneralClass objGen = new GeneralClass();
        decimal TotalAmt = 0.0m;
        decimal TotalTaxAmt = 0.0m;
        decimal TDSPerc = 0.0m;
        //private decimal lab_bankAdv_amtrans = 0;

        string fileNamewithName;

        protected void Page_Load(object sender, EventArgs e)
        {
            // GetCompanyDetails();
            try
            {
                if (!IsPostBack)
                {
                    if (Session["UID"] != null)
                    {


                        if (Request.QueryString["PayInd_No"] != null)
                        {

                            // string amount = lbl_Payind_Total.Text;
                            string g = Request.QueryString["PayInd_No"].ToString();
                            GetUserDetail(Request.QueryString["PayInd_No"].ToString());


                            decimal amount = Convert.ToDecimal(lbl_Payind_Total.Text);

                            decimal grandTotal = Math.Round(amount, MidpointRounding.AwayFromZero);
                            lbl_Payind_Totalwords.Text = objGen.changeNumericToWordsINR(Convert.ToDouble(grandTotal));
                            //lblAmountInWords_Page2.Text = objGen.changeNumericToWordsINR(Convert.ToDouble(grandTotal));




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
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void GetUserDetail(string PayInd_No)
        {
            try
            {
                objPaymentIndentBL = new PaymentIndentBL();
                //objPaymentIndentBL.PayInd_No = ViewState["PayInd_No"].ToString();
                ds = new DataSet();
                objPaymentIndentBL.PayInd_No = PayInd_No;

                objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_PAYIND_DETAILS_BY_PAYINDNO, ref ds);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    lbl_CompanyName.Text = ds.Tables[0].Rows[0]["Vendor_name"].ToString();
                    lbl_Payind_No.Text = ds.Tables[0].Rows[0]["PayInd_No"].ToString();
                    lbl_Payind_Date.Text = ds.Tables[0].Rows[0]["PayInd_Date"].ToString();
                    lbl_Payind_MobNo.Text = ds.Tables[0].Rows[0]["Con_No"].ToString();
                    lbl_Payind_Email.Text = ds.Tables[0].Rows[0]["Email_ID"].ToString();
                    lbl_Payind_Bankname.Text = ds.Tables[0].Rows[0]["Bank"].ToString();
                    lbl_Payind_AccNo.Text = ds.Tables[0].Rows[0]["Acc_No"].ToString();
                    lbl_Payind_Branch.Text = ds.Tables[0].Rows[0]["Branch"].ToString();
                    lbl_Payind_ISFC.Text = ds.Tables[0].Rows[0]["IFSC"].ToString();
                    lbl_Payind_PayMethod.Text = ds.Tables[0].Rows[0]["Payment_Mode"].ToString();
                    lbl_Payind_CompAddr1.Text = ds.Tables[0].Rows[0]["Add_Line"].ToString();
                    lbl_Payind_CompAddr2.Text = ds.Tables[0].Rows[0]["City"].ToString();
                    lbl_Payind_CompAddr3.Text = ds.Tables[0].Rows[0]["Pin"].ToString();
                    //lbl_Payind_CompAddr4.Text = ds.Tables[0].Rows[0]["Con_No"].ToString();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Grdbankadvice.DataSource = ds;
                        double totalSalary = 0;
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            totalSalary += Convert.ToDouble(dr["Amt_Approved"]);
                        }

                        //--- Here 3 is the number of column where you want to show the total.  
                        lbl_Payind_Total.Text = totalSalary.ToString();
                        Grdbankadvice.DataBind();
                    }

                    decimal amount = Convert.ToDecimal(lbl_Payind_Total.Text);//Here you will replace your value.
                    //string amount = new objGen.changeNumericToWordsINR.NumberToEnglishWordConverter().changeCurrencyToWords(amount);



                }

            }


            catch (Exception ex)
            {
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }

        }
    }
}