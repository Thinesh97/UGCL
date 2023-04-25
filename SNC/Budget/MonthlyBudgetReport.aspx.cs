using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SNC.ErrorLogger;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections;


public partial class MonthlyBudgetReport : System.Web.UI.Page
{
    ProjectBL objProjectBL = null;
    BudgetBL objBudgetBL = null;
    DataSet ds = null;    
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
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
                BindMonth();
                BindYear();

            }
        }
        catch(Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    /// <summary>
    /// For Binding ProjectNames into DropDown ddlProjectName
    /// </summary>
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
                // objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_ALL, ref ds);
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
    /// <summary>
    /// For Binding Months into DropDown ddlProjectMonth
    /// </summary>
    protected void BindMonth()
    {
        try
        {
            objBudgetBL = new BudgetBL();
            ds = new DataSet();
            objBudgetBL.load(con, BudgetBL.eLoadSp.SELECT_MONTH, ref ds);
            ddlProjectMonth.DataSource = ds;
            ddlProjectMonth.DataTextField = "Month";
            ddlProjectMonth.DataValueField = "Month_ID";
            ddlProjectMonth.DataBind();
            ddlProjectMonth.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }
    /// <summary>
    ///  For Binding Years into DropDown ddlProjectYear
    /// </summary>
    protected void BindYear()
    {
        try
        {
            objBudgetBL = new BudgetBL();
            ds = new DataSet();
            objBudgetBL.load(con, BudgetBL.eLoadSp.SELECT_YEAR, ref ds);
            ddlProjectYear.DataSource = ds;
            ddlProjectYear.DataTextField = "Year";
            ddlProjectYear.DataValueField = "Year";
            ddlProjectYear.DataBind();
            ddlProjectYear.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }
    /// <summary>
    /// For Searching the Records and binding into the Grid_Search Grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlProjectName.SelectedIndex != 0 || ddlProjectMonth.SelectedIndex != 0 || ddlProjectYear.SelectedIndex != 0)
            {


                objBudgetBL = new BudgetBL();
                ds = new DataSet();
                objBudgetBL.Project_Code = ddlProjectName.SelectedValue.Trim();
                objBudgetBL.Month = ddlProjectMonth.SelectedIndex != 0 ? Convert.ToInt16(ddlProjectMonth.SelectedValue.Trim()) : 0;
                objBudgetBL.Year = ddlProjectYear.SelectedIndex != 0 ? Convert.ToInt32(ddlProjectYear.SelectedValue.Trim()) : 0;
                objBudgetBL.load(con, BudgetBL.eLoadSp.BUDGET_SEARCH, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Grid_Search.DataSource = ds;
                    Grid_Search.DataBind();
                }
                else
                {
                    Grid_Search.DataSource = null;
                    Grid_Search.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please Select Budget Details To Search!');", true);

                return;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    /// <summary>
    /// For Exporting Pdf 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportToPDF_Click(object sender, EventArgs e)
    {

        Grid_Search.PageSize = -1;
        Grid_Search.DataBind();
        ExportGridToPDF(Grid_Search);


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
            Paragraph paragraph = new Paragraph("Budget Details");
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
        Response.AddHeader("content-disposition", "attachment;filename=BudgetDetails.pdf");
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(fileStream.ToArray());
        // Response.End();
        HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlProjectMonth.SelectedIndex = -1;
       
        ddlProjectYear.SelectedIndex = -1;
        Grid_Search.DataSource = null;
        Grid_Search.DataBind();
    }



    decimal Total_Amount = 0;
    decimal Total_App_Amount = 0;
 /// <summary>
 /// For Caluculating TotalAmount and showing in the Grid
 /// </summary>
 /// <param name="sender"></param>
 /// <param name="e"></param>
    protected void Grid_Search_RowCreated(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (IsPostBack && e.Row.RowType == Obout.Grid.GridRowType.Header)
            {
                Total_Amount = 0;
                Total_App_Amount = 0;

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_Search_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[1].Text = "Total Amount";
            }
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                Total_Amount += decimal.Parse(e.Row.Cells[2].Text);
                Total_App_Amount += decimal.Parse(e.Row.Cells[3].Text);
            }
            else if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[2].Text = Total_Amount.ToString();
                e.Row.Cells[3].Text = Total_App_Amount.ToString();

                txtTotalBudAmt.Text = Total_Amount.ToString();
                txtTotalBudAmt.Text = Total_App_Amount.ToString();
                Total_Amount = 0;
                Total_App_Amount = 0;
            }
        }
        catch(Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }
   
}













