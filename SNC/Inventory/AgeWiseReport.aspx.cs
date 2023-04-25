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


public partial class AgeWiseReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    StockBL objstockBL = null;
    ProjectBL objProjectBL = null;  
    DataSet ds = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UID"] == null)
            {
                Response.Redirect("~/CommonPages/Login.aspx", false);
                return;
            }
            if (!IsPostBack)
            {
                BindProjectNames();              
               // BindYearFromStock();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            objstockBL = new StockBL();
            ds = new DataSet();
            if (Session["Project_Code"] != null)
            {
                objstockBL.ProjectCode = ddlProjectName.SelectedValue.Trim();
            }
            else
            {
                objstockBL.ProjectCode = string.Empty;
            }
            
            
            objstockBL.Year = 0;
            if(txtStartDate.Text != string.Empty)
            {
                objstockBL.Date = Convert.ToDateTime(txtStartDate.Text);
            }
            else
            {
                objstockBL.Date = null;
            }
            
            objstockBL.load(con, StockBL.eLoadSp.SELECT_AGEWISE_REPORT, ref ds);
            if(ds.Tables.Count > 0)
            {
                Grid_AgeWise.DataSource = ds.Tables[0];
                Grid_AgeWise.DataBind();
            }
            else
            {
                Grid_AgeWise.DataSource = null;
                Grid_AgeWise.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

   
    //protected void BindYearFromStock()
    //{
    //    try
    //    {
    //        objstockBL = new StockBL();
    //        ds = new DataSet();
    //        objstockBL.load(con, StockBL.eLoadSp.SELECT_YEAR_FROM_STOCK, ref ds);
    //        ddlYear.DataSource = ds;
    //        ddlYear.DataTextField = "YEAR";
    //        ddlYear.DataValueField = "YEAR";
    //        ddlYear.DataBind();
    //        ddlYear.Items.Insert(0, "-Select-");
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

    //    }
    //}

    protected void BindProjectNames()
    {
        try
        {
            objProjectBL = new ProjectBL();
            ds = new DataSet();
            if (Session["Project_Code"] != null)
            {
                objProjectBL.Project_Code = Session["Project_Code"].ToString();
            }
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_BY_Project_Code, ref ds);
            //objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_ALL, ref ds);
            ddlProjectName.DataSource = ds;
            ddlProjectName.DataTextField = "Project_Name";
            ddlProjectName.DataValueField = "Project_Code";
            ddlProjectName.DataBind();
            ddlProjectName.Enabled = false;
            //ddlProjectName.Items.Insert(0, "-Select-");
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
            Grid_AgeWise.PageSize = -1;
            Grid_AgeWise.DataBind();
            ExportGridToPDF(Grid_AgeWise);

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
            Paragraph paragraph = new Paragraph("                                                                      Stock AgeWise Report");
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
        Response.AddHeader("content-disposition", "attachment;filename=StockAgeWiseReport.pdf");
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(fileStream.ToArray());
        // Response.End();
        HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

    }

    protected void lnkbtnItemCode_Click(object sender, EventArgs e)
    {
        try
        {
            objstockBL = new StockBL();
            ds = new DataSet();

            if (Session["Project_Code"] != null)
            {
                objstockBL.ProjectCode = ddlProjectName.SelectedValue.Trim();
            }
            else
            {
                objstockBL.ProjectCode = string.Empty;
            }

            objstockBL.Year =  0;
            objstockBL.Stock_Date = Convert.ToDateTime(txtStartDate.Text);

            objstockBL.Sector = ((LinkButton)sender).CommandArgument.ToString();
            objstockBL.Periods = ((LinkButton)sender).CommandName.ToString();

            objstockBL.load(con, StockBL.eLoadSp.SELECT_ITEM_DETAILS_BY_CODE, ref ds);
            if (ds.Tables.Count > 0)
            {
                Grid_ItemDetails.DataSource = ds.Tables[0];
                Grid_ItemDetails.DataBind(); 
            }
            else
            {
                Grid_ItemDetails.DataSource = ds.Tables[0];
                Grid_ItemDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    decimal days30 = 0, days60 = 0, days90 = 0, days120 = 0, days150 = 0, days180 = 0, daysabove = 0,Overall = 0;

    protected void Grid_AgeWise_RowCreated(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (IsPostBack && e.Row.RowType == Obout.Grid.GridRowType.Header)
            {
                days30 = 0;
                days60 = 0; 
                days90 = 0; 
                days120 = 0; 
                days150 = 0;
                days180 = 0;
                daysabove = 0;
                Overall = 0;

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_AgeWise_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[0].Text = "Total";
            }
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                days30 += decimal.Parse(e.Row.Cells[1].Text);
                days60 += decimal.Parse(e.Row.Cells[2].Text);
                days90 += decimal.Parse(e.Row.Cells[3].Text);
                days120 += decimal.Parse(e.Row.Cells[4].Text);
                days150 += decimal.Parse(e.Row.Cells[5].Text);
                days180 += decimal.Parse(e.Row.Cells[6].Text);
                daysabove += decimal.Parse(e.Row.Cells[7].Text);
                Overall += decimal.Parse(e.Row.Cells[8].Text);

               
            }
            else if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[1].Text = days30.ToString();
                e.Row.Cells[2].Text = days60.ToString();
                e.Row.Cells[3].Text = days90.ToString();
                e.Row.Cells[4].Text = days120.ToString();
                e.Row.Cells[5].Text = days150.ToString();
                e.Row.Cells[6].Text = days180.ToString();
                e.Row.Cells[7].Text = daysabove.ToString();
                e.Row.Cells[8].Text = Overall.ToString();

                days30 = 0;
                days60 = 0;
                days90 = 0;
                days120 = 0;
                days150 = 0;
                days180 = 0;
                daysabove = 0;
                Overall = 0;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_ItemDetails_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                qty += decimal.Parse(e.Row.Cells[4].Text);
                Rate += decimal.Parse(e.Row.Cells[5].Text);
                Amount += decimal.Parse(e.Row.Cells[6].Text);


               

            }
            if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[1].Text = "Total";
                e.Row.Cells[4].Text = qty.ToString();
                e.Row.Cells[5].Text = Rate.ToString();
                e.Row.Cells[6].Text = Amount.ToString();
                qty = 0; Rate = 0; Amount = 0;
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    decimal qty = 0, Rate = 0, Amount = 0;

    protected void Grid_ItemDetails_RowCreated(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (IsPostBack && e.Row.RowType == Obout.Grid.GridRowType.Header)
            {
                qty = 0; Rate = 0; Amount = 0;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
}
