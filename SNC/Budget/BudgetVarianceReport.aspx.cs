using iTextSharp.text;
using iTextSharp.text.pdf;
using SNC.ErrorLogger;
using System.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BusinessLayer;


public partial class BudgetVarianceReport : System.Web.UI.Page
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
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
        
    }
    /// <summary>
    /// For Searching the Records and Binding Into the  Grid_VarianceReport Grid
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
                objBudgetBL.load(con, BudgetBL.eLoadSp.BUDGET_Variance_SEARCH, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    Grid_VarianceReport.DataSource = ds;
                    Grid_VarianceReport.DataBind();
                }
                else
                {
                    Grid_VarianceReport.DataSource = null;
                    Grid_VarianceReport.DataBind();
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
    /// For Binding the ProjectNames Into the DropDown ddlProjectName
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
    /// For Binding Months Into the DropDown ddlProjectMonth
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
    ///  For Binding Year Into the DropDown ddlProjectYear
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
    /// Exporting to Pdf
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportToPDF_Click(object sender, EventArgs e)
    {
        Grid_VarianceReport.PageSize = -1;
        Grid_VarianceReport.DataBind();
        ExportGridToPDF(Grid_VarianceReport);

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
            Paragraph paragraph = new Paragraph("Budget Variance Details");
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
        Response.AddHeader("content-disposition", "attachment;filename=BudgetVarianceDetails.pdf");
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
        Grid_VarianceReport.DataSource = null;
        Grid_VarianceReport.DataBind();
    }


    decimal App_Total_Amount = 0;
    decimal Actual_Amt = 0;
    decimal Variance_Amt = 0;
    /// <summary>
    /// For Caluculating TotalAmt and Showing Into the Grid_VarianceReport Grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Grid_VarianceReport_RowCreated(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (IsPostBack && e.Row.RowType == Obout.Grid.GridRowType.Header)
            {
                App_Total_Amount = 0;
                Actual_Amt = 0;
                Variance_Amt = 0;

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_VarianceReport_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[1].Text = "Total Amount";
            }
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                App_Total_Amount += decimal.Parse(e.Row.Cells[2].Text);
                Actual_Amt += decimal.Parse(e.Row.Cells[3].Text);
                Variance_Amt += decimal.Parse(e.Row.Cells[4].Text);
            }
            else if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[2].Text = App_Total_Amount.ToString();
                e.Row.Cells[3].Text = Actual_Amt.ToString();
                e.Row.Cells[4].Text = Variance_Amt.ToString();

                txtTotalAmt.Text = App_Total_Amount.ToString();
                txtTotalAmt.Text = Actual_Amt.ToString();
                txtTotalAmt.Text = Variance_Amt.ToString();
                App_Total_Amount = 0;
                Actual_Amt = 0;
                Variance_Amt = 0;
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void Sector_Click(object sender, EventArgs e)
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
                objBudgetBL.Bud_type = ((LinkButton)sender).Text;
                objBudgetBL.load(con, BudgetBL.eLoadSp.BUDGET_Variance_BreakUp, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    GridBreakup.DataSource = ds;
                    GridBreakup.DataBind();
                    divbudgetbreak.Visible = true;
                }
                else
                {
                    GridBreakup.DataSource = null;
                    GridBreakup.DataBind();
                    divbudgetbreak.Visible = false;
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

    protected void GridBreakup_RowCreated(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (IsPostBack && e.Row.RowType == Obout.Grid.GridRowType.Header)
            {
                budgetQty = 0;
                budgetAmt = 0;
                purchaseqty = 0;
                purchaseamt = 0;
                breakupvariance = 0;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    decimal budgetQty = 0, budgetAmt = 0, purchaseqty, purchaseamt = 0,breakupvariance = 0;
    protected void GridBreakup_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {

                budgetQty += decimal.Parse(e.Row.Cells[4].Text);
                budgetAmt += decimal.Parse(e.Row.Cells[5].Text);
                purchaseqty += decimal.Parse(e.Row.Cells[7].Text);
                purchaseamt += decimal.Parse(e.Row.Cells[8].Text);
                breakupvariance += decimal.Parse(e.Row.Cells[9].Text);
            }
            if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[4].Text = budgetQty.ToString();
                e.Row.Cells[5].Text = budgetAmt.ToString();
                e.Row.Cells[7].Text = purchaseqty.ToString();
                e.Row.Cells[8].Text = purchaseamt.ToString();
                e.Row.Cells[9].Text = breakupvariance.ToString();

                budgetQty = 0;
                budgetAmt = 0;
                purchaseqty= 0; 
                purchaseamt = 0;
                breakupvariance = 0;


            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
}
