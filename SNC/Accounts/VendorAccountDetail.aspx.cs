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
namespace SNC.Accounts
{
    public partial class VendorAccountDetail : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        AccountsBL objAccountBL = null;
        //AccountsBL objSCBillBL = null;
        DataSet ds = null;
        DataSet Accessds = new DataSet();
        decimal TotalBilledAmount = 0;
        decimal PaidAmount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Vendor_ID"] !="")
            {
                BindAccountDetailbyID();
                BindAccountDetailbyID_ForPaidAmount();
            }            
        }

        protected void BindAccountDetailbyID()
        {
            try
            {
                ds = new DataSet();
                objAccountBL = new AccountsBL();
                objAccountBL.Project_Code = Session["Project_Code"].ToString();
                objAccountBL.Task = "SelectVendorAccountDetail_ByID";
                objAccountBL.Vendor_SubCon_ID = Request.QueryString["Vendor_ID"];
                objAccountBL.load(con, AccountsBL.eLoadSp.SELECT_AccountList_Vendor_ALL, ref ds);
                Grid_VendorAccountDetail.DataSource = ds;
                Grid_VendorAccountDetail.DataBind();
                foreach (DataRow row in ds.Tables[0].Rows)
                { 
                    TotalBilledAmount = TotalBilledAmount + Convert.ToDecimal(row["InvoiceAmt"]);

                }
                Amount_Billed.InnerText = Convert.ToString(TotalBilledAmount);
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void Grid_VendorAccount_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
                {
                    //HyperLink Editlik = e.Row.Cells[0].FindControl("lnkVendor_ID") as HyperLink;
                    //Editlik.NavigateUrl = "~/Accounts/VendorAccountDetail.aspx?Vendor_ID=" + Editlik.Text;
                    TotalBilledAmount = Convert.ToDecimal(e.Row.Cells[4].ToString());
                }
                Amount_Billed.InnerText =Convert.ToString(TotalBilledAmount);
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void BindAccountDetailbyID_ForPaidAmount()
        {
            try
            {
                DataSet PaidData = new DataSet();
                objAccountBL = new AccountsBL();
                objAccountBL.Project_Code = Session["Project_Code"].ToString();
                objAccountBL.Task = "SelectVendorAccountDetail_ByID";
                objAccountBL.Vendor_SubCon_ID = Request.QueryString["Vendor_ID"];
                objAccountBL.load(con, AccountsBL.eLoadSp.SELECT_AccountList_Vendor_ALL, ref PaidData);
                Grid_VendorAccountDetailPaid.DataSource = PaidData.Tables[1];
                Grid_VendorAccountDetailPaid.DataBind();
                foreach (DataRow row in PaidData.Tables[1].Rows)
                {
                    PaidAmount = PaidAmount + Convert.ToDecimal(row["Amt_Transferable"]);
                    Grid_name_Vendor.InnerText = Convert.ToString(row["Vendor_name"]);
                    lbl_grid_Billedamount.InnerText= Convert.ToString(row["Vendor_name"]);
                    Vendorname.InnerText = Convert.ToString(row["Vendor_name"]);
                }
                Amount_PAid.InnerText = Convert.ToString(PaidAmount);
                Balance_Amount.InnerText =Convert.ToString(TotalBilledAmount - PaidAmount);
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
    }
}