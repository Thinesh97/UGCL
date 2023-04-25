using BusinessLayer;
using SNC.ErrorLogger;
using System;
using System.Collections;
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
    public partial class PurchaseOrderDetails : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        ProjectBL objProjectBL = null;
        AssetRegistrationBL objAsset = null;
        AssetTransferBL objassetTransferBL = null;
        IndentBL objIndent = null;
        DataSet ds = null;
        MaterialBL objMaterial = null;
        BudgetBL objBudgetBL = null;
        Category objCategory = null;
        UOM objUOM = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindBudgetPurchageCost();
        }

        protected void BindBudgetPurchageCost()
        {
            try
            {
                objBudgetBL = new BudgetBL();
                DataSet ds1 = new DataSet();
                DataTable DatafilterDt = new DataTable();
            
                objBudgetBL.Budget_ID = Request.QueryString["BudgetID"].ToString();
                objBudgetBL.Project_Code = Request.QueryString["Pcode"].ToString();
                objBudgetBL.SECTOR_NAME = Request.QueryString["Type"].ToString();
                objBudgetBL.load(con, BudgetBL.eLoadSp.PoAmountApproval, ref ds1);

                if (ds1.Tables[0].Rows.Count > 0)
                {



                    DatafilterDt = ds1.Tables[0];

                    gvPODetails.DataSource = DatafilterDt;
                    gvPODetails.DataBind();

                }
            }
            catch (Exception)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
            }
        }

        protected void btn_GenquotationItemsList_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Budget/MonthlyBudget.aspx?Abs_BID=" + Request.QueryString["Abs_BID"].ToString() + "&MFYRQ=" + Request.QueryString["MFYRQ"].ToString() + "&BudgetID=" + Request.QueryString["BudgetID"].ToString(), false);
 
        }

    }
}