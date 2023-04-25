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
using System.Collections;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;


public partial class BudgetApproval : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);

    DataSet ds = null;  

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    BindBudgetModRequest();
                }
                else
                {
                    Response.Redirect("../CommonPages/Login.aspx", false);
                }
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    protected void BindBudgetModRequest()
    {

        try
        {
            DataTable Dt = new DataTable();
            DataTable FilteredDt = new DataTable();
            BudgetBL objBudgetBL = new BudgetBL();
            ds = new DataSet();
            objBudgetBL.Project_Code = Session["Project_Code"].ToString();
            objBudgetBL.load(con, BudgetBL.eLoadSp.BUDGET_AWAITING_APPROVAL, ref ds);

            Dt = ds.Tables[0];
            FilteredDt = Dt.Clone();

            foreach (DataRow dr in Dt.Rows)
            {
                if (dr["Report_Person"].ToString().Contains(','))
                {
                    string[] Rep = dr["Report_Person"].ToString().Split(',');

                    if (Rep.Contains(Session["UID"].ToString()))
                    {
                        FilteredDt.Rows.Add(dr.ItemArray);
                    }
                }
                else if (dr["Report_Person"].ToString() == Session["UID"].ToString())
                {
                    FilteredDt.Rows.Add(dr.ItemArray);
                }
            }
            GridBudgetApproval.DataSource = FilteredDt;
            GridBudgetApproval.DataBind();


            FilteredDt.Clear();
            foreach (DataRow dr in Dt.Rows)
            {
                if (dr["Primary_Person"].ToString() == Session["UID"].ToString())
                {
                    FilteredDt.Rows.Add(dr.ItemArray);
                }
            }

            GridPrimaryApproval.DataSource = FilteredDt;
            GridPrimaryApproval.DataBind();


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void btnExportToPDF_Click(object sender, EventArgs e)
    {

        try
        {
            GridBudgetApproval.PageSize = -1;
            GridBudgetApproval.DataBind();
            ExportGridToPDF(GridBudgetApproval);

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

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
            Paragraph paragraph = new Paragraph("Budget Approval  Details");
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
        Response.AddHeader("content-disposition", "attachment;filename=Budget Approval.pdf");
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(fileStream.ToArray());
        // Response.End();
        HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

    }

    protected void btnExportPDFPrimarryapproval_Click(object sender, EventArgs e)
    {
        try
        {
            GridPrimaryApproval.PageSize = -1;
            GridPrimaryApproval.DataBind();
            ExportGridToPDF(GridPrimaryApproval);

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }
}

    