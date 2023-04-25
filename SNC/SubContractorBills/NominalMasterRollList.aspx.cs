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

namespace SNC.SubContractorBills
{
    public partial class NominalMasterRollList : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        WorkOrderBL objViewWOBL = null;
        SubContractorBillBL objSubContBL = null;
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
                        BindGrid_NMR_Entry();

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

       

        protected void Grid_NMR_Delete_Click(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            try
            {
                objSubContBL = new SubContractorBillBL();
                objSubContBL.NMR_No = Convert.ToString(e.Record["NMR_No"]);
                objSubContBL.Task = "Delete_NMR";
                if (objSubContBL.delete(con, SubContractorBillBL.eLoadSp.SUB_CONTRACTOR_BILL_OPERATIONS))
                {
                    BindGrid_NMR_Entry();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('NMR Item has been deleted sucessfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete NMR Item !');", true);
                }

            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void Grid_NMR_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
                {
                    HyperLink Editlik = e.Row.Cells[0].FindControl("lnk_NMR_No") as HyperLink;
                    Editlik.NavigateUrl = "~/SubContractorBills/NominalMasterRoll.aspx?NMR_No=" + Editlik.Text;
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
                    //LinkButton lnkNMR_Delete = e.Row.Cells[8].FindControl("lnkNMR_Delete") as LinkButton;

                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void BindGrid_NMR_Entry()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();
                objSubContBL.Project_Code = Session["Project_Code"].ToString(); 
                objSubContBL.Task = "GetNMR_List";
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SUB_CONTRACTOR_BILL_OPERATIONS, ref ds);
                Grid_NMR.DataSource = ds;
                Grid_NMR.DataBind();
                //foreach (DataRow row in ds.Tables[0].Rows)
                //{
                //    decimal rowvalue = Convert.ToDecimal(row["Present_Progress"]);
                //    Cumulative_Progress += Convert.ToDecimal(rowvalue);
                //}

                //hdntxtCumulativeProgress.Value = Cumulative_Progress.ToString();

            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
    }
}