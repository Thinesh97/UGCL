using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;
using SNC.ErrorLogger;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections;
using System.IO;

    public partial class POAwaitingApproval : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        PurchaseOrderBL objPO = null;
        DataSet ds = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    BindPoAwaitingApproval();
                }
                else
                {
                    Response.Redirect("../CommonPages/Login.aspx", false);
                }
            }

        }
        protected void BindPoAwaitingApproval()
        {

            try
            {
                ds = new DataSet();
                objPO = new PurchaseOrderBL();
                objPO.ApprovedBy = Convert.ToInt32(Session["UID"]);
                objPO.ProjectCode = Session["Project_Code"].ToString();
                objPO.load(con, PurchaseOrderBL.eLoadSp.POAWAITING_APPROVAL, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridPoAwaitingApproval.DataSource = ds;
                    GridPoAwaitingApproval.DataBind();
                }
                else
                {
                    GridPoAwaitingApproval.DataSource = null;
                    GridPoAwaitingApproval.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }

        }

      
        protected void btnExportToPDF_Click(object sender, System.EventArgs e)
        {
            try
            {
                GridPoAwaitingApproval.PageSize = -1;
                GridPoAwaitingApproval.DataBind();
                ExportGridToPDF(GridPoAwaitingApproval);

            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

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
                Paragraph paragraph = new Paragraph("                                                             PO Awaiting For Approval Lists");
                PdfPTable PdfTable = new PdfPTable(GirdData.Columns.Count);
                PdfPCell PdfPCell_Data = null;
                foreach (Obout.Grid.Column col in GirdData.Columns)
                {
                    PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(col.HeaderText, font8)));
                    PdfPCell_Data.BackgroundColor = iTextSharp.text.Color.GRAY;
                    PdfTable.AddCell(PdfPCell_Data);
                }
                for (int i = 0; i < GirdData.Rows.Count; i++)
                {
                    Hashtable dataItem = GirdData.Rows[i].ToHashtable();
                    Font font1 = FontFactory.GetFont("ARIAL", 7);
                    foreach (Obout.Grid.Column col in GirdData.Columns)
                    {
                        PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(dataItem[col.DataField] != null ? dataItem[col.DataField].ToString() : "", font1)));
                        PdfTable.AddCell(PdfPCell_Data);
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
            Response.AddHeader("content-disposition", "attachment;filename=POAwaitingForApprovalLists.pdf");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(fileStream.ToArray());
            // Response.End();
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

        }

      
    }
