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
    public partial class PO_Print_Backside : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        DataSet ds = null;
        PurchaseOrderBL objPO = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    if (Request.QueryString["PONo"] != null)
                    {
                        GetPODetailsPrint(Request.QueryString["PONo"].ToString());
                    }
                }
                else
                {
                    Response.Redirect("../CommonPages/Login.aspx", false);
                }
            }
        }

        private void GetPODetailsPrint(string PONo)
        {
            objPO = new PurchaseOrderBL();
            ds = new DataSet();
            objPO.PONo = PONo;
            objPO.load(con, PurchaseOrderBL.eLoadSp.SELECT_PODETAILS_BY_PONO, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {               
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
            }
        }
    }
}