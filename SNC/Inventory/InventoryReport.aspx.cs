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



public partial class InventoryReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    StockBL objstockBL = null;
    ProjectBL objProjectBL = null;
    BudgetBL objBudgetBL = null;
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
            if (!Page.IsPostBack)
            {
                BindProjectNames();
                BindMonth();
                BindYear();
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void BindProjectNames()
    {
        try
        {
            objProjectBL = new ProjectBL();
            ds = new DataSet();
            if (Session["Project_Code"] != null)
            {
                objProjectBL.Project_Code = Session["Project_Code"].ToString();

                objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_BY_Project_Code, ref ds);
                ddlProjectName.DataSource = ds;
                ddlProjectName.DataTextField = "Project_Name";
                ddlProjectName.DataValueField = "Project_Code";
                ddlProjectName.DataBind();
                ddlProjectName.Enabled = false;
                ddlProjectName.Items.Insert(0, "-Select-");
                ddlProjectName.SelectedValue = Session["Project_Code"].ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }



    protected void BindMonth()
    {
        try
        {
            objBudgetBL = new BudgetBL();
            ds = new DataSet();
            objBudgetBL.load(con, BudgetBL.eLoadSp.SELECT_MONTH, ref ds);
            ddlMonth.DataSource = ds;
            ddlMonth.DataTextField = "Month";
            ddlMonth.DataValueField = "Month_ID";
            ddlMonth.DataBind();
            ddlMonth.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }
    protected void BindYear()
    {
        try
        {
            objstockBL = new StockBL();
            ds = new DataSet();
            objstockBL.load(con, StockBL.eLoadSp.SELECT_YEAR_FROM_STOCK, ref ds);
            ddlYear.DataSource = ds;
            ddlYear.DataTextField = "Year";
            ddlYear.DataValueField = "Year";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            objstockBL = new StockBL();
            ds = new DataSet();
            objstockBL.ProjectCode = Session["Project_Code"].ToString();
            objstockBL.Month = Convert.ToInt32(ddlMonth.SelectedValue);
            objstockBL.Year = Convert.ToInt32(ddlYear.SelectedValue);
            objstockBL.load(con, StockBL.eLoadSp.SELECT_SECTORS_BY_PROJECT_CODE, ref ds);
            ttlamount.Visible = true;

          
            if (ds.Tables[0].Rows.Count > 0)
            {

                decimal sum = ds.Tables[0].AsEnumerable().Sum(x => x.Field<decimal>("Amount"));

                ttlamount.Text = "Total Amount in Rs:" + sum.ToString();
                GV_Sector.DataSource = ds;
                GV_Sector.DataBind();
            }
            else
            {
                GV_Sector.DataSource = null;
                GV_Sector.DataBind();
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlMonth.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        GV_Sector.DataSource = null;
        GV_Sector.DataBind();
    }

    protected void Sector_Click(object sender, EventArgs e)
    {
        try
        {
            ds = new DataSet();
            objstockBL = new StockBL();
            objstockBL.ProjectCode = ddlProjectName.SelectedValue;
            objstockBL.Month = Convert.ToInt32(ddlMonth.SelectedValue);
            objstockBL.Year = Convert.ToInt32(ddlYear.SelectedValue);
            objstockBL.Budget_Sector_ID = Convert.ToInt32(((LinkButton)sender).CommandArgument.ToString());
            objstockBL.load(con, StockBL.eLoadSp.BUDGET_SECTOR_ID_CLICK, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GV_Details.DataSource = ds;
                GV_Details.DataBind();
            }
            else
            {
                GV_Details.DataSource = null;
                GV_Details.DataBind();


            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void btnExportToPDF_Click1(object sender, EventArgs e)
    {
        try
        {
            GV_Sector.PageSize = -1;
            GV_Sector.DataBind();
            ExportGridToPDF(GV_Sector);
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnExportToPDF_Click(object sender, EventArgs e)
    {
        try
        {
            GV_Details.PageSize = -1;
            GV_Details.DataBind();
            ExportGridToPDF(GV_Details);
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

            Paragraph paragraph = new Paragraph("                                                                       Inventory Report List");
            PdfPTable PdfTable = new PdfPTable(GirdData.Columns.Count-1);
           

            PdfPCell PdfPCell_Data = null;
            foreach (Obout.Grid.Column col in GirdData.Columns)
            {
                //if (col.HeaderText != "Delete" & col.HeaderText != "Transfer Req ID")
                //{
                if (col.HeaderText != "ID")
                {
                    PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(col.HeaderText, font8)));
                    PdfPCell_Data.BackgroundColor = iTextSharp.text.Color.GRAY;
                    PdfTable.AddCell(PdfPCell_Data);
                }
                //}
            }
            //for (int i = 0; i < GirdData.Rows.Count; i++)
            //{

            int i = 0;
            decimal sum =0.00M;

            foreach (Obout.Grid.GridRow row in GirdData.Rows)
            {
                Hashtable dataItem = GirdData.Rows[i].ToHashtable();

                Font font1 = FontFactory.GetFont("ARIAL", 7);
                foreach (Obout.Grid.Column col in GirdData.Columns)
                {
                    if (col.HeaderText != "ID")
                    {
                        PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(dataItem[col.DataField] != null ? dataItem[col.DataField].ToString() : "", font1)));
                        PdfTable.AddCell(PdfPCell_Data);
                    }

                    if (col.HeaderText == "Amount in Rs")
                    {
                        sum = sum + Convert.ToDecimal(dataItem[col.DataField].ToString());
                    }

                    //PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(sum)));
                    //PdfTable.AddCell(PdfPCell_Data);
                }


                i++;
            }


            //PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(sum)));
            //PdfTable.AddCell(PdfPCell_Data);
            Paragraph paragraphT = new Paragraph("                                                                       Total Amount in Rs :" + sum);

            PdfTable.SpacingBefore = 15f;
            doc.Add(paragraph);
            doc.Add(paragraphT);
            doc.Add(PdfTable);
        }
        finally
        {
            doc.Close();
        }
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename= InventoryReport.pdf");
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(fileStream.ToArray());
        // Response.End();
        HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

    }



    decimal openingStockQty = 0, MRNPOQty = 0, MRNFromProjectQty = 0, totalAvlForIssue = 0, issueQty = 0, closingStockinQty = 0, totalAmt = 0;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GV_Details_RowCreated(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (IsPostBack && e.Row.RowType == Obout.Grid.GridRowType.Header)
            {
                openingStockQty = 0;
                MRNPOQty = 0;
                MRNFromProjectQty = 0;
                totalAvlForIssue = 0;
                issueQty = 0;
                closingStockinQty = 0;
                totalAmt = 0;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    decimal tAmount = 0;
    protected void GV_Sector_RowCreated(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (IsPostBack && e.Row.RowType == Obout.Grid.GridRowType.Header)
            {
                tAmount = 0;

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }


    protected void GV_Sector_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {

                tAmount += decimal.Parse(e.Row.Cells[2].Text);
            }
            if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[2].Text = tAmount.ToString();
                tAmount = 0;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void GV_Details_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {

                openingStockQty += decimal.Parse(e.Row.Cells[4].Text);
                MRNPOQty += decimal.Parse(e.Row.Cells[5].Text);
                MRNFromProjectQty += decimal.Parse(e.Row.Cells[7].Text);
                totalAvlForIssue += decimal.Parse(e.Row.Cells[8].Text);
                issueQty += decimal.Parse(e.Row.Cells[9].Text);
                closingStockinQty += decimal.Parse(e.Row.Cells[10].Text);
                totalAmt += decimal.Parse(e.Row.Cells[12].Text);


            }
            if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[1].Text = "Total";
                e.Row.Cells[4].Text = openingStockQty.ToString();
                openingStockQty = 0;

                e.Row.Cells[5].Text = MRNPOQty.ToString();
                MRNPOQty = 0;

                e.Row.Cells[7].Text = MRNFromProjectQty.ToString();
                MRNFromProjectQty = 0;

                e.Row.Cells[8].Text = totalAvlForIssue.ToString();
                totalAvlForIssue = 0;

                e.Row.Cells[9].Text = issueQty.ToString();
                issueQty = 0;

                e.Row.Cells[10].Text = closingStockinQty.ToString();
                closingStockinQty = 0;

                e.Row.Cells[12].Text = totalAmt.ToString();
                totalAmt = 0;

            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
}
