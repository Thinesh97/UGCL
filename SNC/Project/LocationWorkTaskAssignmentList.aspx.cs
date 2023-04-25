using BusinessLayer;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SNC.ErrorLogger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SNC.Project
{
    public partial class TaskList : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        WorkLocationBL objWorkLocationBL = null;
        DataSet ds = null;
        DataSet Accessds = new DataSet();  
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Div_ReplaytoLetter.Visible = false;
                if (Session["UID"] != null)
                {
                    Get_WorkName_All();
                }
                
            }
        }
       
        protected void Get_WorkName_All()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();

                if (Session["Project_Code"] != null)
                {
                    
                    objWorkLocationBL.Task = "Select_All_LWT";
                    objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                    objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                    Grid_LWT.DataSource = ds;
                    Grid_LWT.DataBind();
                }
                else
                {
                    Grid_LWT.DataSource = null;
                    Grid_LWT.DataBind();
                }
                
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }
        protected void Grid_LWTRowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
                {
                    HyperLink Editlik = e.Row.Cells[0].FindControl("lnkLWT") as HyperLink;

                    Editlik.NavigateUrl = "~/Project/LocationWorkTaskAssignment.aspx?LWT_ID=" + Editlik.Text;

                    if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                    {
                        if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                        {
                            if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Proj_Update"].ToString()))
                            {
                                Editlik.NavigateUrl = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}