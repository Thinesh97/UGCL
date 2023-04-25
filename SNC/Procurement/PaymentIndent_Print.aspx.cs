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

public partial class PaymentIndent_Print : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    DataSet ds = null;
    PaymentIndentBL objPaymentIndentBL = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        GeneralClass objGen = new GeneralClass();
        if (!IsPostBack)
        {
            if (Session["UID"] != null)
            {
                if (Request.QueryString["PayInd_No"] != null)
                {
                    GetPaymentIndentDetails();
                    BindItems();
                }
            }
            else
            {
                Response.Redirect("../CommonPages/Login.aspx", false);
            }
        }
    }
    
    private void GetPaymentIndentDetails()
    {
        try
        {
            GeneralClass objGen = new GeneralClass();
            objPaymentIndentBL = new PaymentIndentBL();
            DataSet dsDetails = new DataSet();
            objPaymentIndentBL.Task = "GetPaymentIndentDetailsPrint";
            objPaymentIndentBL.PayInd_No = Request.QueryString["PayInd_No"].ToString();
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_PAYMENTINDENTDETAILS, ref dsDetails);
            if (dsDetails.Tables[0].Rows.Count > 0)
            {
                lblPayIndNo.Text = dsDetails.Tables[0].Rows[0]["PayInd_No"].ToString();
                lblPayIndDate.Text = Convert.ToDateTime(dsDetails.Tables[0].Rows[0]["PayInd_Date"]).ToString("dd-MM-yyyy");
                lblFYr.Text = dsDetails.Tables[0].Rows[0]["FYear"].ToString();
                lblState.Text = dsDetails.Tables[0].Rows[0]["State_Name"].ToString();
                ViewState["BeneficiaryType"] = dsDetails.Tables[0].Rows[0]["Beneficiary_Type"].ToString();
                lblVendorSubcon.Text = dsDetails.Tables[0].Rows[0]["Ben_Name"].ToString();
                ViewState["VendorSubconID"] = dsDetails.Tables[0].Rows[0]["Vendor_SubCon_ID"].ToString();
                lblProject.Text = dsDetails.Tables[0].Rows[0]["Project_Code"].ToString();
                lblAwardedBy.Text = dsDetails.Tables[0].Rows[0]["Award_By"].ToString();
                lblWorkDesc.Text = dsDetails.Tables[0].Rows[0]["WorkDesc"].ToString();
                //lblPricipalCon.Text = dsDetails.Tables[0].Rows[0]["FYear"].ToString();
                lblPOWO.Text = dsDetails.Tables[0].Rows[0]["POWO_No"].ToString();
                ViewState["POWOID"] = dsDetails.Tables[0].Rows[0]["POWO_ID"].ToString();
                txtAmt_ServiceMaterial.Text = dsDetails.Tables[0].Rows[0]["Amt_ServiceMaterial"].ToString();
                txtAmt_EarlierPayment.Text = dsDetails.Tables[0].Rows[0]["Amt_EarlierPayment"].ToString();
                txtAmt_PartPayment.Text = dsDetails.Tables[0].Rows[0]["Amt_PartPayment"].ToString();
                decimal TotalAmt = Math.Round(Convert.ToDecimal(dsDetails.Tables[0].Rows[0]["TotalAmt"].ToString()), MidpointRounding.AwayFromZero);
                lblTotalAmt.Text = TotalAmt.ToString();
                lblAmountInWords.Text = objGen.changeNumericToWordsINR(Convert.ToDouble(TotalAmt));
                
                GETPOWODetails();
                GetKYCDetails();

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void GETPOWODetails()
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Project_Code = Session["Project_Code"].ToString();
            objPaymentIndentBL.POWO_ID = Convert.ToInt32(ViewState["POWOID"].ToString());
            objPaymentIndentBL.Task = "LoadPOWOByID";
            if (ViewState["BeneficiaryType"].ToString() == "Vendor")
            {
                objPaymentIndentBL.Vendor_ID = ViewState["VendorSubconID"].ToString();
                objPaymentIndentBL.Task = "LoadPODetails";
            }
            else
            {
                objPaymentIndentBL.SubCon_ID = ViewState["VendorSubconID"].ToString();
                objPaymentIndentBL.Task = "LoadWODetails";
            }
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_POWO, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblNatureOfWork.Text = ds.Tables[0].Rows[0]["Item_Type"].ToString();
                lblNatureOfMaterial.Text = ds.Tables[0].Rows[0]["Item_Desc"].ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void GetKYCDetails()
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Project_Code = Session["Project_Code"].ToString();
            if (ViewState["BeneficiaryType"].ToString() == "Vendor")
            {
                objPaymentIndentBL.Vendor_ID = ViewState["VendorSubconID"].ToString();
                objPaymentIndentBL.Task = "GetKYCDetailsByVendor";
            }
            else
            {
                objPaymentIndentBL.SubCon_ID = ViewState["VendorSubconID"].ToString();
                objPaymentIndentBL.Task = "GetKYCDetailsBySubCon";
            }
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_POWO, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblGSTRegd.Text = ds.Tables[0].Rows[0]["File_GSTRegistration"].ToString() != "" ? "YES" : "NO";
                lblPANCopy.Text = ds.Tables[0].Rows[0]["File_PANCopy"].ToString() != "" ? "YES" : "NO";
                lblBankDetails.Text = ds.Tables[0].Rows[0]["File_BankDetails"].ToString() != "" ? "YES" : "NO";
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void BindItems()
    {
        try
        {
            objPaymentIndentBL = new PaymentIndentBL();
            ds = new DataSet();
            objPaymentIndentBL.Task = "GetPaymentIndentItemPrint";
            objPaymentIndentBL.PayInd_No = Request.QueryString["PayInd_No"].ToString();
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_PAYMENTINDENTDETAILS, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Grid_Item1.DataSource = ds.Tables[0];
                Grid_Item1.DataBind();
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                Grid_Item2.DataSource = ds.Tables[1];
                Grid_Item2.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

}
