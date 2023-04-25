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


public partial class StockTransferList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
   
    StockTransferBL objStockTranBL = null;
    DataSet ds = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           if(!IsPostBack)
           {
               if (Session["UID"] != null)
               {
                   BindStockTransferList();
               }
               else
               {
                   Response.Redirect("../CommonPages/Login.aspx", false);
               }
           }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    /// <summary>
    /// Binding All the stock transfer lists from the stock transfer table
    /// </summary>
    private void BindStockTransferList()
    {
        try
        {
            objStockTranBL = new StockTransferBL();
            ds = new DataSet();
            objStockTranBL.FromProjectCode = Session["Project_Code"].ToString(); 
            objStockTranBL.load(con, StockTransferBL.eLoadSp.SELECT_ALL_STOCK_TRANSFER, ref ds);
            if (ds.Tables.Count > 0)
            {
                Gv_StockTransferList.DataSource = ds;
                Gv_StockTransferList.DataBind();
            }
            else
            {
                Gv_StockTransferList.DataSource = null;
                Gv_StockTransferList.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    /// <summary>
    /// Binding All the items based on the particular Budget Sector 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Lnkbtn_SectorName_Click(object sender, EventArgs e)
    {
        try
        {
            objStockTranBL = new StockTransferBL();
            ds = new DataSet();
            DataTable dt = new DataTable();
            objStockTranBL.Budget_Sector = ((LinkButton)sender).Text.ToString();
            objStockTranBL.ToProjectCode = ((LinkButton)sender).CommandArgument.ToString();
            objStockTranBL.FromProjectCode = Session["Project_Code"].ToString(); 
            objStockTranBL.load(con, StockTransferBL.eLoadSp.SELECT_SECTORWISEITEMS_FROM_STOCK_TRANSFER, ref ds);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {


                Gv_SectorWiseItems.DataSource = dt;
                Gv_SectorWiseItems.DataBind();
            }
            else
            {
                Gv_SectorWiseItems.DataSource = null;
                Gv_SectorWiseItems.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Btn_ExportToPDF_Click(object sender, EventArgs e)
    {
        try
        {
            Gv_SectorWiseItems.PageSize = -1;
            Gv_SectorWiseItems.DataBind();
            ExportGridToPDF(Gv_SectorWiseItems);

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }
    /// <summary>
    /// Exporting the Gridview Data to PDF 
    /// </summary>
    /// <param name="GirdData"></param>
    private void ExportGridToPDF(Obout.Grid.Grid GirdData)
    {

        MemoryStream fileStream = new MemoryStream();
        Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
        try
        {
            PdfWriter wri = PdfWriter.GetInstance(doc, fileStream);
            doc.Open();
            Font font8 = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Color.WHITE);
            Paragraph paragraph = new Paragraph("Stock Transfer Items Details");
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
        Response.AddHeader("content-disposition", "attachment;filename=StockTransferItemsList.pdf");
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(fileStream.ToArray());
        // Response.End();
        HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

    }
}
       
    