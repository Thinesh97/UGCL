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
public partial class MachineRunningHoursKmsReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    DailyRunningHourBL objDailyRunningHourBL = null;
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
                BindProject();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    protected void BindProject()
    {
        ProjectBL objprojectbl = new ProjectBL();
        try
        {
            ds = new DataSet();
            objprojectbl = new ProjectBL();
            if (Session["Project_Code"] != null)
            {
                objprojectbl.Project_Code = Session["Project_Code"].ToString();
            }
            objprojectbl.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_BY_Project_Code, ref ds);
            //objprojectbl.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_ALL, ref ds);
            ddlProjectName.DataSource = ds;
            ddlProjectName.DataTextField = "Project_Name";
            ddlProjectName.DataValueField = "Project_Code";
            ddlProjectName.DataBind();
            ddlProjectName.Enabled = false;
            // ddlProjectName.Items.Insert(0, "-Select-");

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
            GridMachRunHourReport.PageSize = -1;
            GridMachRunHourReport.DataBind();
            ExportGridToPDF(GridMachRunHourReport);

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
            Paragraph paragraph = new Paragraph("                                                   Machine Running Hours/Kms Report ");
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
        Response.AddHeader("content-disposition", "attachment;filename=MachineRunningHours/KmsReport.pdf");
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(fileStream.ToArray());
        // Response.End();
        HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if ((txtStartDate.Text != string.Empty && txtEndDate.Text == string.Empty) || (txtStartDate.Text == string.Empty && txtEndDate.Text != string.Empty))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please Make Ensure you selected Start Date and End date.!!!');", true);
                GridMachRunHourReport.DataSource = null;
                GridMachRunHourReport.DataBind();
                return;
            }

            //I got Doubt Here
            if (txtStartDate.Text == string.Empty && txtEndDate.Text == string.Empty && (ddlProjectName.SelectedIndex == -1 || ddlProjectName.SelectedIndex == 0))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select any one option.!!!');", true);
                GridMachRunHourReport.DataSource = null;
                GridMachRunHourReport.DataBind();
                return;
            }
            objDailyRunningHourBL = new DailyRunningHourBL();
            ds = new DataSet();
            if (txtStartDate.Text != string.Empty)
            {
                objDailyRunningHourBL.StDate = Convert.ToDateTime(txtStartDate.Text);
            }
            else
            {
                objDailyRunningHourBL.StDate = null;
            }
            if (txtEndDate.Text != string.Empty)
            {
                objDailyRunningHourBL.EndDate = Convert.ToDateTime(txtEndDate.Text);
            }
            else
            {
                objDailyRunningHourBL.EndDate = null;
            }
            if (Session["Project_Code"] != null)
            {
                objDailyRunningHourBL.Project_Code = ddlProjectName.SelectedValue.Trim();
            }
            else
            {
                objDailyRunningHourBL.Project_Code = string.Empty;
            }
            //objDailyRunningHourBL.Project_Code = ddlProjectName.SelectedIndex != 0 ? ddlProjectName.SelectedValue : string.Empty;
            objDailyRunningHourBL.load(con, DailyRunningHourBL.eLoadSp.SELECT_DAILY_MONTHLY_RUNNING_HRS_KMS_REPORT, ref ds);

            if (ds.Tables.Count > 0)
            {
                GridMachRunHourReport.DataSource = ds.Tables[0];
                GridMachRunHourReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

}
