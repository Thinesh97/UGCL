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


public partial class QuotationList : System.Web.UI.Page
{
    QuotationBL objQuotationBL = null;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    DataSet ds = null;
    DataSet Accessds = new DataSet(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UID"] != null)
            {
                ActionPermission();
                if (!IsPostBack)
                {

                    BindQuotation();
                    if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                    {

                        if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin" && !Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Quotn_View"].ToString()))
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
        
        catch(Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

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
                        if (Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Quotn_Create"].ToString()))
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
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindQuotation()
    {
        try
        {
            objQuotationBL = new QuotationBL();
            ds = new DataSet();
            objQuotationBL.Project_Code = Session["Project_Code"].ToString();
            objQuotationBL.load(con, QuotationBL.eLoadSp.SELECT_QUOTATIONLIST_ALL, ref ds);
            QuotationGrid.DataSource = ds;
            QuotationGrid.DataBind();

        }
        catch(Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

   

    protected void btnExportToPDF_Click(object sender, EventArgs e)
    {
        try
        {
            QuotationGrid.PageSize = -1;
            QuotationGrid.DataBind();
            ExportGridToPDF(QuotationGrid);

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
            Paragraph paragraph = new Paragraph("                                                                   Quotation Details");
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
        Response.AddHeader("content-disposition", "attachment;filename=QuotationLists.pdf");
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(fileStream.ToArray());
        // Response.End();
        HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

    }

    protected void QuotationGrid_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                HyperLink Editlink = e.Row.Cells[0].FindControl("lnkQuotationNo") as HyperLink;

                Editlink.NavigateUrl = "~/Procurement/Quotation.aspx?ID=" + Editlink.Text;

                if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                {
                    if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                    {
                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Quotn_Update"].ToString()))
                        {
                            Editlink.NavigateUrl = "";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }
    protected void BindquotationItemsList()
    {
        try
        {
            ds = new DataSet();
            objQuotationBL = new QuotationBL();
            if (Session["Project_Code"] != null)
            {
                objQuotationBL.Project_Code = Session["Project_Code"].ToString();
                objQuotationBL.load(con, QuotationBL.eLoadSp.SELECT_ALL_QUOTATION_ITEMS_LIST_BY_Project, ref ds);
             
                Gv_quotationitemList.DataSource = ds;
                Gv_quotationitemList.DataBind();
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void QuotationGrid_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objQuotationBL = new QuotationBL();
            objQuotationBL.QuotationNo = e.Record["QuotationNo"].ToString();
            if (objQuotationBL.delete(con, QuotationBL.eLoadSp.Quotation_DELETE))
            {
                BindQuotation();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Quotation has been deleted successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete the quotation');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btn_GenquotationItemsList_Click(object sender, EventArgs e)
    {
        BindquotationItemsList(); ;
        quotationList_Gv.Visible = true;
    }
   

}
