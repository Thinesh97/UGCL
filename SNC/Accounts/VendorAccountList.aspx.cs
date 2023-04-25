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
    public partial class VendorAccountList : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        AccountsBL objAccountBL = null;
        //AccountsBL objSCBillBL = null;
        DataSet ds = null;
        DataSet Accessds = new DataSet();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UID"] != null)
                {
                    if (!IsPostBack)
                    {
                        BindAccountList();
                    }
                }
                else
                {
                    Response.Redirect("../CommonPages/Login.aspx", false);
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void BindAccountList()
        {
            try
            {

                ds = new DataSet();
                objAccountBL = new AccountsBL();
                objAccountBL.Project_Code = Session["Project_Code"].ToString();
                objAccountBL.Task = "SelectVendorAccountList";
                objAccountBL.load(con, AccountsBL.eLoadSp.SELECT_AccountList_Vendor_ALL, ref ds);
                Grid_VendorAccountList.DataSource = ds;
                Grid_VendorAccountList.DataBind();
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
                    HyperLink Editlik = e.Row.Cells[0].FindControl("lnkVendor_ID") as HyperLink;
                    Editlik.NavigateUrl = "~/Accounts/VendorAccountDetail.aspx?Vendor_ID=" + Editlik.Text;
                    
                    if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                    {
                        if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                        {
                            if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["WO_Update"].ToString()))
                            {
                                Editlik.NavigateUrl = "";
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
    }
}