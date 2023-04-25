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

namespace SNC.Procurement
{
    public partial class RequestForQuotationList : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        PurchaseOrderBL objViewPOBL = null;
        DataSet ds = null;
        DataSet Accessds = new DataSet();
        RequestForquotationBL objViewRFQ = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UID"] != null)
                {
                    ActionPermission();
                    if (!IsPostBack)
                    {
                        Grid_PO.Visible = false;
                        BindPurchaseOrderList();
                        BindRFQPendingList();
                        if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                        {

                            if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin" && !Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PO_View"].ToString()))
                            {
                                Response.Redirect("~/CommonPages/Home.aspx", false);
                            }
                        }
                        else
                        {
                            Response.Redirect("~/CommonPages/Login.aspx", true);
                        }
                    }
                }
                else
                {
                    Response.Redirect("../CommonPages/Login.aspx", false);
                }
            }
            catch (Exception ex)
            {
               // ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void ActionPermission()
        {
            try
            {
                if (Session["ActionAccess"] != null)
                {
                    Accessds = (DataSet)Session["ActionAccess"];
                }
                if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                {
                    if (Accessds.Tables[0].Rows.Count > 0)
                    {
                        if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                        {
                            if (Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PO_Create"].ToString()))
                            {
                                lnkbtnAdd.Visible = true;
                            }
                            else
                            {
                                lnkbtnAdd.Visible = false;
                            }

                        }
                    }
                }
                else
                {
                    Response.Redirect("~/CommonPages/Login.aspx", true);
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }


        protected void BindPurchaseOrderList()
        {
            try
            {
                if (Session["Role"].ToString() == "Application Admin")
                {
                    ds = new DataSet();
                    objViewPOBL = new PurchaseOrderBL();
                    objViewPOBL.ProjectCode = Session["Project_Code"].ToString();
                    objViewPOBL.Task = "DirectPO";
                    objViewPOBL.load(con, PurchaseOrderBL.eLoadSp.PRO_PURCHASE_ORDER_SELECT_ALL, ref ds);
                    Grid_PO.DataSource = ds;
                    Grid_PO.DataBind();
                }
                else if (Session["Role"].ToString() == "Other")
                {
                    ds = new DataSet();
                    objViewPOBL = new PurchaseOrderBL();
                    objViewPOBL.ProjectCode = Session["Project_Code"].ToString();
                    objViewPOBL.Task = "DirectPO_Other";
                    objViewPOBL.load(con, PurchaseOrderBL.eLoadSp.PRO_PURCHASE_ORDER_SELECT_ALL, ref ds);
                    Grid_PO.DataSource = ds;
                    Grid_PO.DataBind();
                }
            }
            catch (Exception ex)
            {
               // ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void BindRFQPendingList()
        {
            try
            {
                if (Session["Role"].ToString() == "Application Admin")
                {
                    ds = new DataSet();
                    objViewRFQ = new RequestForquotationBL();
                    objViewRFQ.ProjectCode = Session["Project_Code"].ToString();
                    objViewRFQ.Task = "DirectRFQ_Pending";
                    objViewRFQ.load(con, RequestForquotationBL.eLoadSp.REQUEST_FOR_QUOTATION_SELECT_ALL, ref ds);
                    Grid_RFQ_Pending.DataSource = ds;
                    Grid_RFQ_Pending.DataBind();
                }
                else if (Session["Role"].ToString() == "Other")
                {
                    ds = new DataSet();
                    objViewRFQ = new RequestForquotationBL();
                    objViewRFQ.ProjectCode = Session["Project_Code"].ToString();
                    objViewRFQ.Task = "DirectPO_Pending_Other";
                    objViewRFQ.load(con, RequestForquotationBL.eLoadSp.REQUEST_FOR_QUOTATION_SELECT_ALL, ref ds);
                    Grid_RFQ_Pending.DataSource = ds;
                    Grid_RFQ_Pending.DataBind();
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void btnExportToPDF_Click(object sender, EventArgs e)
        {
            try
            {
                Grid_PO.PageSize = -1;
                Grid_PO.DataBind();
                ExportGridToPDF(Grid_PO);
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        private void ExportGridToPDF(Obout.Grid.Grid GirdData)
        {
            MemoryStream fileStream = new MemoryStream();
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            try
            {
                PdfWriter wri = PdfWriter.GetInstance(doc, fileStream);
                doc.Open();
                Font font8 = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Color.WHITE);
                Paragraph paragraph = new Paragraph("Purchase Order Details");
                PdfPTable PdfTable = new PdfPTable(GirdData.Columns.Count - 1);
                PdfPCell PdfPCell_Data = null;
                foreach (Obout.Grid.Column col in GirdData.Columns)
                {
                    if (col.HeaderText != "Delete")
                    {
                        PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(col.HeaderText, font8)));
                        PdfPCell_Data.BackgroundColor = iTextSharp.text.Color.GRAY;
                        PdfTable.AddCell(PdfPCell_Data);
                    }
                }
                for (int i = 0; i < GirdData.Rows.Count; i++)
                {
                    Hashtable dataItem = GirdData.Rows[i].ToHashtable();
                    Font font1 = FontFactory.GetFont("ARIAL", 7);
                    foreach (Obout.Grid.Column col in GirdData.Columns)
                    {
                        if (col.HeaderText != "Delete")
                        {
                            PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(dataItem[col.DataField] != null ? dataItem[col.DataField].ToString() : "", font1)));
                            PdfTable.AddCell(PdfPCell_Data);
                        }
                    }
                }

                PdfTable.SpacingBefore = 15f;
                doc.Add(paragraph);
                doc.Add(PdfTable);
            }
            finally
            {
                doc.Close();
            }

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=PurchaseOrderDetails.pdf");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(fileStream.ToArray());
            // Response.End();
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
        }

        protected void Grid_PO_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
                {
                    HyperLink Editlik = e.Row.Cells[0].FindControl("lnkRFQNo") as HyperLink;
                    Editlik.NavigateUrl = "~/Procurement/RequestForQuotation.aspx?RFQNo=" + Editlik.Text;
                    if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                    {
                        if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                        {
                            if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PO_Update"].ToString()))
                            {
                                Editlik.NavigateUrl = "";
                            }
                        }
                    }

                    LinkButton lnkPO_Delete = e.Row.Cells[14].FindControl("lnkRFQ_Delete") as LinkButton;

                    if (Accessds.Tables[0].Rows[0]["Appr_PO_Delete"].ToString() == "" || Accessds.Tables[0].Rows[0]["Appr_PO_Delete"].ToString() == "False" || Accessds.Tables[0].Rows[0]["Appr_PO_Delete"].ToString() == "0")
                    {
                        if (e.Row.Cells[14].Text == "Approved")
                        {
                            lnkPO_Delete.Visible = false;
                        }
                        else
                        {
                            lnkPO_Delete.Visible = true;
                        }
                    }
                    else
                    {
                        lnkPO_Delete.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void Grid_RFQ_RowDataBound_Pending(object sender, Obout.Grid.GridRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
                {
                    HyperLink Editlik = e.Row.Cells[0].FindControl("lnkRFQNo") as HyperLink;
                    Editlik.NavigateUrl = "~/Procurement/RequestForQuotation.aspx?RFQNo=" + Editlik.Text;
                    if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                    {
                        if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                        {
                            if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PO_Update"].ToString()))
                            {
                                Editlik.NavigateUrl = "";
                            }
                        }
                    }

                    LinkButton lnkRFQ_Delete = e.Row.Cells[12].FindControl("lnkRFQ_Delete") as LinkButton;

                    if (Accessds.Tables[0].Rows[0]["Appr_PO_Delete"].ToString() == "" || Accessds.Tables[0].Rows[0]["Appr_PO_Delete"].ToString() == "False" || Accessds.Tables[0].Rows[0]["Appr_PO_Delete"].ToString() == "0")
                    {
                        if (e.Row.Cells[14].Text == "Approved")
                        {
                            lnkRFQ_Delete.Visible = false;
                        }
                        else
                        {
                            lnkRFQ_Delete.Visible = true;
                        }
                    }
                    else
                    {
                        lnkRFQ_Delete.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
               // ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void lnkRFQ_Delete_Click(object sender, CommandEventArgs e)
        {


            try
            {
                if (HF_Confirm.Value != "false")
                {
                    //type=   RegisterStartupScript(typeof(Page), "exampleScript", "if(confirm('are you confirm?')) { document.getElementById('btn').click(); } ", true)
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    Hashtable dataItem = Grid_RFQ_Pending.Rows[rowIndex].ToHashtable() as Hashtable;

                    objViewRFQ = new RequestForquotationBL();

                    objViewRFQ.RFQNo = dataItem["RFQNo"].ToString();
                    if (objViewRFQ.delete(con, RequestForquotationBL.eLoadSp.RFQ_Delete))
                    {
                        BindPurchaseOrderList();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Direct RFQ has been deleted successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Direct RFQ is refered other process!');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }


            //        protected void lnkRFQ_Delete_Click(object sender, Obout.Grid.GridRecordEventArgs e)
            //try
            //{
            //    objViewRFQ = new RequestForquotationBL();
            //    objViewRFQ.RFQNo = e.Record["RFQNo"].ToString();
            //    if (objViewRFQ.delete(con, RequestForquotationBL.eLoadSp.RFQ_Delete))
            //    {
            //       // BindPurchaseOrderList();
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Direct RFQ has been deleted successfully');", true);
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Direct RFQ is refered other process!');", true);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            //}
        }


        protected void btn_GenPOItemsList_Click(object sender, EventArgs e)
        {
            BindPOItemsList();
            ItemList_Gv.Visible = true;
        }

        protected void BindPOItemsList()
        {
            try
            {
                ds = new DataSet();
                objViewPOBL = new PurchaseOrderBL();
                if (Session["Project_Code"] != null)
                {
                    objViewPOBL.ProjectCode = Session["Project_Code"].ToString();
                    objViewPOBL.load(con, PurchaseOrderBL.eLoadSp.SELECT_PO_ITEMS_ALL_BY_PROJECT, ref ds);
                    Gv_POItemsList.DataSource = ds;
                    Gv_POItemsList.DataBind();
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void lnkPO_Delete_Click(object sender, CommandEventArgs e)
        {
            try
            {
                if (HF_Confirm.Value != "false")
                {
                    //type=   RegisterStartupScript(typeof(Page), "exampleScript", "if(confirm('are you confirm?')) { document.getElementById('btn').click(); } ", true)
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    Hashtable dataItem = Grid_PO.Rows[rowIndex].ToHashtable() as Hashtable;

                    objViewPOBL = new PurchaseOrderBL();

                    objViewPOBL.PONo = dataItem["PONo"].ToString();
                    if (objViewPOBL.delete(con, PurchaseOrderBL.eLoadSp.PO_Delete))
                    {
                        BindPurchaseOrderList();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Direct PO has been deleted successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Direct PO is refered other process!');", true);
                    }
                }
            }
            catch (Exception ex)
            {
               // ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
    }
}