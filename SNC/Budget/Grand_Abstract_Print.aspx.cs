using BusinessLayer;
using System;
using SNC.ErrorLogger;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;



    public partial class Grand_Abstract_Print : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        DataSet ds = null;
        BudgetBL objBudgetBL = new BudgetBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    BindGrid();
                }
                else
                {
                    Response.Redirect("../CommonPages/Login.aspx", false);
                }
            }
        }



        private void BindGrid()
        {
            try
            {

                ds = new DataSet();
                objBudgetBL = new BudgetBL();
                objBudgetBL.Abs_BID =Convert.ToInt32(Request.QueryString["ID"].ToString());
                objBudgetBL.load(con, BudgetBL.eLoadSp.PRINT_BUDGET_BY_ID, ref ds);

                if(ds.Tables[0].Rows.Count >0 )
                {                    
                    lblProjectName.Text = ds.Tables[0].Rows[0]["Project_Name"].ToString();
                    lblBudget_ID.Text = ds.Tables[0].Rows[0]["Budget_ID"].ToString();
                    lblGrandTotal.Text = ds.Tables[0].Rows[0]["GRANTAmt"].ToString();

                    GridPrint.DataSource = ds;
                    GridPrint.DataBind();
                }

                

            }
            catch(Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBUpdateError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }





        }
    }
