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


public partial class FortnightReport : System.Web.UI.Page
{
    ProjectBL objProjectBL = null;
    BudgetBL objBudgetBL = null;
    PurchaseOrderBL objPOBL = null;
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
            }
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_BY_Project_Code, ref ds);
           // objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_ALL, ref ds);
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
    /// <summary>
    /// For Binding Months into DropDown ddlMonth
    /// </summary>
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
    /// <summary>
    /// For Binding Years into DropDown ddlYear
    /// </summary>
    protected void BindYear()
    {
        try
        {
            objBudgetBL = new BudgetBL();
            ds = new DataSet();
            objBudgetBL.load(con, BudgetBL.eLoadSp.SELECT_YEAR, ref ds);
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
    /// <summary>
    /// For Searching and Showing the Results in Grid_FortNight And Gv_DateBased based on the selection of Radio Button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Grid_FortNight.DataSource = null;
            Grid_FortNight.DataBind();
            Gv_DateBased.DataSource = null;
            Gv_DateBased.DataBind();
            objPOBL = new PurchaseOrderBL();
            ds = new DataSet();
            if (Session["Project_Code"] != null)
            {
                objPOBL.ProjectCode = ddlProjectName.SelectedValue.Trim();
            }
            else
            {
                objPOBL.ProjectCode = string.Empty;
            }


            if (Rbl_SearchBy.SelectedValue == "FortNightBased")
            {
                //objPOBL.ProjectCode = ddlProjectName.SelectedIndex != 0 ? ddlProjectName.SelectedValue.Trim() : string.Empty;
                objPOBL.Month = ddlMonth.SelectedIndex != 0 ? Convert.ToInt16(ddlMonth.SelectedValue.Trim()) : 0;
                objPOBL.Year = ddlYear.SelectedIndex != 0 ? Convert.ToInt32(ddlYear.SelectedValue.Trim()) : 0;
                objPOBL.load(con, PurchaseOrderBL.eLoadSp.SELECT_FORTNIGHT_REPORT, ref ds);
                if (ds.Tables.Count > 0)
                {
                    Grid_FortNight.DataSource = ds.Tables[0];
                    Grid_FortNight.DataBind();
                }
                else
                {
                    Grid_FortNight.DataSource = ds.Tables[0];
                    Grid_FortNight.DataBind();
                }
            }
            else
            {
                objPOBL.StartDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                objPOBL.EndDate = Convert.ToDateTime(txtEndDate.Text.Trim());
                objPOBL.load(con, PurchaseOrderBL.eLoadSp.SELECT_FORTNIGHT_REPORT_DATE_BASED, ref ds);
                if (ds.Tables.Count > 0)
                {
                    Gv_DateBased.DataSource = ds.Tables[0];
                    Gv_DateBased.DataBind();
                }
                else
                {
                    Gv_DateBased.DataSource = ds.Tables[0];
                    Gv_DateBased.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    /// <summary>
    /// Exporting Into Pdf
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportToPDF_Click(object sender, EventArgs e)
    {
        try
        {
            Grid_FortNight.PageSize = -1;
            Grid_FortNight.DataBind();
            ExportGridToPDF(Grid_FortNight);

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);

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
            Paragraph paragraph = new Paragraph("FortNight Report Details");
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
        Response.AddHeader("content-disposition", "attachment;filename=Fortnight Report.pdf");
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(fileStream.ToArray());
        // Response.End();
        HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

    }


    /// <summary>
    /// For Clearing  Values
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            
            ddlProjectName.SelectedIndex = -1;
            ddlYear.SelectedIndex = -1;
            ddlMonth.SelectedIndex = -1;
            Grid_FortNight.DataSource = null;
            Grid_FortNight.DataBind();

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);

        }
    }
    /// <summary>
    /// For Clicking In the Grid_FortNight and that values are going to Bind In Grid_BudgetSectorDetails grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void lnkbtnBudgetSector_Click(object sender, EventArgs e)
    {
        try
        {
            objPOBL = new PurchaseOrderBL();
            ds = new DataSet();
            if (Session["Project_Code"] != null)
            {
                objPOBL.ProjectCode = ddlProjectName.SelectedValue.Trim();
            }

            objPOBL.Month = ddlMonth.SelectedIndex != 0 ? Convert.ToInt16(ddlMonth.SelectedValue.Trim()) : 0;
            objPOBL.Year = ddlYear.SelectedIndex != 0 ? Convert.ToInt32(ddlYear.SelectedValue.Trim()) : 0;

            objPOBL.BudgetSector = ((LinkButton)sender).Text.ToString();
            objPOBL.load(con, PurchaseOrderBL.eLoadSp.SELECT_SECTORWISE_FORTNIGHT_REPORT, ref ds);



            if (ds.Tables.Count > 0)
            {
                Grid_BudgetSectorDetails.DataSource = ds.Tables[0];
                Grid_BudgetSectorDetails.DataBind();
            }
            else
            {
                Grid_BudgetSectorDetails.DataSource = ds.Tables[0];
                Grid_BudgetSectorDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }        
    }

    protected void GetALLBreakupSector_Click(object sender, EventArgs e)
    {
        try
        {
            objPOBL = new PurchaseOrderBL();
            ds = new DataSet();
            if (Session["Project_Code"] != null)
            {
                objPOBL.ProjectCode = ddlProjectName.SelectedValue.Trim();
            }

            objPOBL.Month = ddlMonth.SelectedIndex != 0 ? Convert.ToInt16(ddlMonth.SelectedValue.Trim()) : 0;
            objPOBL.Year = ddlYear.SelectedIndex != 0 ? Convert.ToInt32(ddlYear.SelectedValue.Trim()) : 0;

            
            objPOBL.load(con, PurchaseOrderBL.eLoadSp.SELECT_SECTORWISE_FORTNIGHT_REPORT_ALL, ref ds);



            if (ds.Tables.Count > 0)
            {
                Grid_BudgetSectorDetails.DataSource = ds.Tables[0];
                Grid_BudgetSectorDetails.DataBind();
            }
            else
            {
                Grid_BudgetSectorDetails.DataSource = ds.Tables[0];
                Grid_BudgetSectorDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnExportToPDFBreakUp_Click(object sender, EventArgs e)
    {
        try
        {
            Grid_BudgetSectorDetails.PageSize = -1;
            Grid_BudgetSectorDetails.DataBind();
            ExportGridToPDF(Grid_BudgetSectorDetails);

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);

        }
    }

    decimal Amt = 0, FirstTotalAmt = 0, FirstFirstAmt = 0, FirstSecondAmt = 0, FirstTotalAmt1 = 0, FirstFirstAmt1 = 0, FirstSecondAmt1 = 0;   
    /// <summary>
    /// For Caluculating Qty and Amount
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Grid_BudgetSectorDetails_RowCreated(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (IsPostBack && e.Row.RowType == Obout.Grid.GridRowType.Header)
            {
                Amt = 0;
                FirstTotalAmt = 0;              
                FirstFirstAmt = 0;
                FirstSecondAmt = 0;
                FirstTotalAmt1 = 0;
                FirstFirstAmt1 = 0;
                FirstSecondAmt1 = 0;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_BudgetSectorDetails_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
               
                Amt += decimal.Parse(e.Row.Cells[9].Text);
                FirstFirstAmt += decimal.Parse(e.Row.Cells[13].Text);
                FirstSecondAmt += decimal.Parse(e.Row.Cells[14].Text);
                FirstTotalAmt += decimal.Parse(e.Row.Cells[15].Text);
                FirstFirstAmt1 += decimal.Parse(e.Row.Cells[16].Text);
                FirstSecondAmt1 += decimal.Parse(e.Row.Cells[17].Text);
                FirstTotalAmt1 += decimal.Parse(e.Row.Cells[18].Text); 
              
            }
            if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[1].Text = "Total";              
                e.Row.Cells[9].Text = Amt.ToString();
                Amt = 0;

                e.Row.Cells[13].Text = FirstFirstAmt.ToString();
                FirstFirstAmt = 0;
                e.Row.Cells[14].Text = FirstSecondAmt.ToString();
                FirstSecondAmt = 0;
                e.Row.Cells[15].Text = FirstTotalAmt.ToString();
                FirstTotalAmt = 0;


                e.Row.Cells[16].Text = FirstFirstAmt1.ToString();
                FirstFirstAmt1 = 0;
                e.Row.Cells[17].Text = FirstSecondAmt1.ToString();
                FirstSecondAmt1 = 0;
                e.Row.Cells[18].Text = FirstTotalAmt1.ToString();
                FirstTotalAmt1 = 0;
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["QuotationNo"] != null ? Session["QuotationNo"].ToString() : string.Empty);
        }
    }
    decimal Local_Amount_1 = 0;
    decimal HO_Amount_1 = 0;
    decimal Total_Amount_1 = 0;
    decimal Local_Amount_2 = 0;
    decimal HO_Amount_2 = 0;
    decimal Total_Amount_2 = 0;
    decimal GrandTotal = 0;
    /// <summary>
    /// for caluculating Total Amount In the ColumnFooter and showing into the Grid_FortNight
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Grid_FortNight_RowCreated(object sender, Obout.Grid.GridRowEventArgs e)
    {

        try
        {
            if (IsPostBack && e.Row.RowType == Obout.Grid.GridRowType.Header)
            {
                Local_Amount_1 = 0;
                HO_Amount_1 = 0;
                Total_Amount_1 = 0;
                Local_Amount_2 = 0;
                HO_Amount_2 = 0;
                Total_Amount_2 = 0;
                GrandTotal = 0;

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_FortNight_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[1].Text = "Total";
            }
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                Local_Amount_1 += decimal.Parse(e.Row.Cells[2].Text);
                HO_Amount_1 += decimal.Parse(e.Row.Cells[3].Text);
                Total_Amount_1 += decimal.Parse(e.Row.Cells[4].Text);
                Local_Amount_2 += decimal.Parse(e.Row.Cells[5].Text);
                HO_Amount_2 += decimal.Parse(e.Row.Cells[6].Text);
                Total_Amount_2 += decimal.Parse(e.Row.Cells[7].Text);
                GrandTotal += decimal.Parse(e.Row.Cells[8].Text);
            }
            else if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[2].Text = Local_Amount_1.ToString();
                e.Row.Cells[3].Text = HO_Amount_1.ToString();
                e.Row.Cells[4].Text = Total_Amount_1.ToString();
                e.Row.Cells[5].Text = Local_Amount_2.ToString();
                e.Row.Cells[6].Text = HO_Amount_2.ToString();
                e.Row.Cells[7].Text = Total_Amount_2.ToString();
                e.Row.Cells[8].Text = GrandTotal.ToString();

                txtTotalAmt.Text = Local_Amount_1.ToString();
                txtTotalAmt.Text = HO_Amount_1.ToString();
                txtTotalAmt.Text = Total_Amount_1.ToString();
                txtTotalAmt.Text = Local_Amount_2.ToString();
                txtTotalAmt.Text = HO_Amount_2.ToString();
                txtTotalAmt.Text = Total_Amount_2.ToString();
                txtTotalAmt.Text = GrandTotal.ToString();


                Local_Amount_1 = 0;
                HO_Amount_1 = 0;
                Total_Amount_1 = 0;
                Local_Amount_2 = 0;
                HO_Amount_2 = 0;
                Total_Amount_2 = 0;
                GrandTotal = 0;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Rbl_SearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Rbl_SearchBy.SelectedValue == "FortNightBased")
            {
               
                trFortNight.Visible = true;
                trDateRange.Visible = false;
              
                Pnl_DateBased.Visible = false;
                Pnl_FortNightBased.Visible = true;

                Gv_DateBasedBudgetSectorDeatils.DataSource = null;
                Gv_DateBasedBudgetSectorDeatils.DataBind();
                Gv_DateBased.DataSource = null;
                Gv_DateBased.DataBind();
            }
            else
            {
                trFortNight.Visible = false;
                trDateRange.Visible = true;

                Pnl_DateBased.Visible = true;
                Pnl_FortNightBased.Visible = false;

                Grid_BudgetSectorDetails.DataSource = null;
                Grid_BudgetSectorDetails.DataBind();
                Grid_FortNight.DataSource = null;
                Grid_FortNight.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void lnkbtnDateBasedBudgetSector_Click(object sender, EventArgs e)
    {
        try
        {
            objPOBL = new PurchaseOrderBL();
            ds = new DataSet();
            if (Session["Project_Code"] != null)
            {
                objPOBL.ProjectCode = ddlProjectName.SelectedValue.Trim();
            }
            objPOBL.BudgetSector = ((LinkButton)sender).Text.ToString();
            objPOBL.StartDate = Convert.ToDateTime(txtStartDate.Text.Trim());
            objPOBL.EndDate = Convert.ToDateTime(txtEndDate.Text.Trim());
            objPOBL.load(con, PurchaseOrderBL.eLoadSp.SELECT_SECTORWISE_FORTNIGHT_REPORT_DATE_BASED, ref ds);
            if (ds.Tables.Count > 0)
            {
                Gv_DateBasedBudgetSectorDeatils.DataSource = ds.Tables[0];
                Gv_DateBasedBudgetSectorDeatils.DataBind();
            }
            else
            {
                Gv_DateBasedBudgetSectorDeatils.DataSource = ds.Tables[0];
                Gv_DateBasedBudgetSectorDeatils.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }        
    }

    decimal Local_Amount_DateBased = 0;
    decimal HO_Amount_DateBased = 0;
    decimal Total_Amount_DateBased = 0;
    protected void Gv_DateBased_RowCreated(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (IsPostBack && e.Row.RowType == Obout.Grid.GridRowType.Header)
            {
                Local_Amount_DateBased = 0;
                HO_Amount_DateBased = 0;
                Total_Amount_DateBased = 0;              
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    
    protected void Gv_DateBased_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[1].Text = "Total";
            }
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                Local_Amount_DateBased += decimal.Parse(e.Row.Cells[2].Text);
                HO_Amount_DateBased += decimal.Parse(e.Row.Cells[3].Text);
                Total_Amount_DateBased += decimal.Parse(e.Row.Cells[4].Text);
              
            }
            else if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[2].Text = Local_Amount_DateBased.ToString();
                e.Row.Cells[3].Text = HO_Amount_DateBased.ToString();
                e.Row.Cells[4].Text = Total_Amount_DateBased.ToString();
              

                txtTotalAmt.Text = Local_Amount_DateBased.ToString();
                txtTotalAmt.Text = HO_Amount_DateBased.ToString();
                txtTotalAmt.Text = Total_Amount_DateBased.ToString();
             


                Local_Amount_DateBased = 0;
                HO_Amount_DateBased = 0;
                Total_Amount_DateBased = 0;
              
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    decimal AmtDateBased = 0, amt = 0 , F= 0, s = 0 ;
    protected void Gv_DateBasedBudgetSectorDeatils_RowCreated(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (IsPostBack && e.Row.RowType == Obout.Grid.GridRowType.Header)
            {
                AmtDateBased = 0;
                amt = 0;
                F = 0;
                s = 0; 
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Gv_DateBasedBudgetSectorDeatils_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                AmtDateBased += decimal.Parse(e.Row.Cells[14].Text);
                amt += decimal.Parse(e.Row.Cells[9].Text);
                F += decimal.Parse(e.Row.Cells[12].Text);
                s += decimal.Parse(e.Row.Cells[13].Text);
            }
            if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[1].Text = "Total";

                e.Row.Cells[14].Text = AmtDateBased.ToString();
                AmtDateBased = 0;

                e.Row.Cells[9].Text = amt.ToString();
                amt = 0;
                e.Row.Cells[12].Text = F.ToString();
                F = 0;
                e.Row.Cells[13].Text = s.ToString();
                s = 0;
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["QuotationNo"].ToString() : string.Empty);
        }
    }

    protected void Btn_ExportToPDF_DateBasedBreakUp_Click(object sender, EventArgs e)
    {
        try
        {
            Gv_DateBasedBudgetSectorDeatils.PageSize = -1;
            Gv_DateBasedBudgetSectorDeatils.DataBind();
            ExportGridToPDF(Gv_DateBasedBudgetSectorDeatils);

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);

        }
    }

    protected void Btn_ExportToPDF_DateBased_Click(object sender, EventArgs e)
    {
        try
        {
            Gv_DateBased.PageSize = -1;
            Gv_DateBased.DataBind();
            ExportGridToPDF(Gv_DateBased);

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UserID"] != null ? Session["UserID"].ToString() : string.Empty);

        }
    }

}
