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
    public partial class SubContractorAccountList : System.Web.UI.Page
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
                        BindSubContractorList();
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

        protected void BindSubContractorList()
        {
            try
            {

                ds = new DataSet();
                objAccountBL = new AccountsBL();
                objAccountBL.Project_Code = Session["Project_Code"].ToString();
                objAccountBL.Task = "SelectSubContractorAccountList";
                objAccountBL.load(con, AccountsBL.eLoadSp.SELECT_AccountList_Vendor_ALL, ref ds);
                Grid_SubContractorAccountList.DataSource = ds;
                Grid_SubContractorAccountList.DataBind();
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
    }
}