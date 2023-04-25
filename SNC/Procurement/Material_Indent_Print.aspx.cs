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
using DataAccess;
using SNC.ErrorLogger;


public partial class Material_Indent_Print : System.Web.UI.Page
{
    IndentBL objIndent = null;
    DataSet ds = null;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["UID"] != null)
            {
                if (Request.QueryString["IndentNo"] != null)
                {
                    GetIndentDetails(Request.QueryString["IndentNo"].ToString());
                }
            }
            else
            {
                Response.Redirect("../CommonPages/Login.aspx", false);
            }
        }

    }
    private void BindIndentItem()
    {

        objIndent = new IndentBL();
        ds = new DataSet();
        objIndent.Indent_No = Request.QueryString["IndentNo"].ToString();
        objIndent.load(con, IndentBL.eLoadSp.SELECT_INDENT_ITEMS_BY_INDENT_NO, ref ds);
        GridMaterialIndentPrint.DataSource = ds;
        GridMaterialIndentPrint.DataBind();

    }
    protected void GetIndentDetails(string indent)
    {
        try
        {

            objIndent =new IndentBL ();
            objIndent.Indent_No = indent;
            objIndent.load(con, IndentBL.eLoadSp.SELECT_INDENTDETAILS_BY_ID, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblindent_no.Text = ds.Tables[0].Rows[0]["Indent_No"].ToString();
                lbldate.Text = ds.Tables[0].Rows[0]["Ind_date"].ToString();
                lblnameOfSite.Text = ds.Tables[0].Rows[0]["Location_Name"].ToString();
                lblfrom.Text = ds.Tables[0].Rows[0]["Process_From"].ToString();
                //lblremark.Text = ds.Tables[0].Rows[0]["Remark"].ToString();
                lblLocationname.Text = ds.Tables[0].Rows[0]["Location_Name"].ToString();
                lblto.Text = ds.Tables[0].Rows[0]["Process_From"].ToString();
                lbllPrepBy.Text = ds.Tables[0].Rows[0]["PreparedBy"].ToString();
                lblstockcheckby.Text = ds.Tables[0].Rows[0]["StockCheckBy"].ToString();
            // lblstockcheckby.Text =Convert.ToString( objIndent.Stock_check_By.Value);

                lblstchkby.Text = ds.Tables[0].Rows[0]["StockCheckBy"].ToString();
                lblQtySpcfics.Text = ds.Tables[0].Rows[0]["Qty_Spec"].ToString();
                lblnote.Text = ds.Tables[0].Rows[0]["NOTE"].ToString();
                if (ds.Tables[0].Rows[0]["Process_From"].ToString().ToLower() == "ho")
                {

                    lblApprovedBy.Text = ds.Tables[0].Rows[0]["HOApprover"].ToString();
                }
                else
                {
                    lblApprovedBy.Text = string.Empty;
                }
                    
                BindIndentItem();
            }
        }

        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

}
    
    
