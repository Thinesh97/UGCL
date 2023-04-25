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
using System.Web.UI.HtmlControls;


public partial class ContractorList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    SubContractorBL objSubContractorBL = null;
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

                    BindContractList();
                    if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                    {
                        if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin" && !Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Sub_Cont_View"].ToString()))
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
        catch (Exception ex)
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
                        if (Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Sub_Cont_Create"].ToString()))
                        {
                            lnkbtnAdd.Visible = true;
                        }
                        else
                        {
                            lnkbtnAdd.Visible = false;
                        }
                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Sub_Cont_Delete"].ToString()))
                        {
                            RegContractorGrid.Columns[18].Visible = false;
                            UnRegContractorGrid.Columns[18].Visible = false;
                        }
                        else
                        {
                            RegContractorGrid.Columns[18].Visible = true;
                            UnRegContractorGrid.Columns[18].Visible = true;
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

    protected void BindContractList()
    {
        try
        {
            ds = new DataSet();
            objSubContractorBL = new SubContractorBL();
            objSubContractorBL.load(con, SubContractorBL.eLoadSp.SUBCONTRACTOR_REGISTERED_UNREGISTERED_SELECT_ALL, ref ds);
            RegContractorGrid.DataSource = ds.Tables[0];
            RegContractorGrid.DataBind();

            UnRegContractorGrid.DataSource = ds.Tables[1];
            UnRegContractorGrid.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    
    protected void lnkSubcon_ID_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(string.Format("../Master/SubContractor.aspx?ID=" + ((LinkButton)sender).Text.ToString(), false));

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    protected void ContractorGrid_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objSubContractorBL = new SubContractorBL();
            objSubContractorBL.Subcon_ID = e.Record["Subcon_ID"].ToString();
          
            if (objSubContractorBL.load(con, SubContractorBL.eLoadSp.DELETE))
            {
                BindContractList();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Contractor details has been deleted');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This contrator cannot be deleted as it is already in use.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ContractorGrid_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                HyperLink Editlik = e.Row.Cells[0].FindControl("lnkSubcontractor") as HyperLink;

                Editlik.NavigateUrl = "~/Master/SubContractor.aspx?ID=" + Editlik.Text;

                if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                {
                    if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                    {
                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Sub_Cont_Update"].ToString()))
                        {
                            Editlik.NavigateUrl = "";
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

    protected void btnExportToPDFReg_Click(object sender, EventArgs e)
    {
        try
        {
            RegContractorGrid.PageSize = -1;
            RegContractorGrid.DataBind();
            ExportGridToPDF(RegContractorGrid, "Reg");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void btnExportToPDFUnReg_Click(object sender, EventArgs e)
    {
        try
        {
            UnRegContractorGrid.PageSize = -1;
            UnRegContractorGrid.DataBind();
            ExportGridToPDF(UnRegContractorGrid, "UnReg");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    private void ExportGridToPDF(Obout.Grid.Grid GirdData, string RegOrNot)
    {

        MemoryStream fileStream = new MemoryStream();
        Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
        try
        {
            PdfWriter wri = PdfWriter.GetInstance(doc, fileStream);
            doc.Open();
            Font font8 = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Color.WHITE);
            Paragraph paragraph = new Paragraph("                                                                       Contractor Details");
            PdfPTable PdfTable = new PdfPTable(GirdData.Columns.Count - 1);
            PdfPCell PdfPCell_Data = null;

            foreach (Obout.Grid.Column col in GirdData.Columns)
            {
                if (col.HeaderText != "Delete")
                {
                    PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(col.HeaderText, font8)));
                    PdfPCell_Data.BackgroundColor = iTextSharp.text.Color.GRAY;
                    PdfTable.AddCell(PdfPCell_Data);
                }
            }
            for (int i = 0; i < GirdData.Rows.Count; i++)
            {
                Hashtable dataItem = GirdData.Rows[i].ToHashtable();
                Font font1 = FontFactory.GetFont("ARIAL", 7);
                foreach (Obout.Grid.Column col in GirdData.Columns)
                {
                    if (col.HeaderText != "Delete")
                    {
                        PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(dataItem[col.DataField] != null ? dataItem[col.DataField].ToString() : "", font1)));
                        PdfTable.AddCell(PdfPCell_Data);
                    }
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
        if (RegOrNot == "Reg")
        {
            Response.AddHeader("content-disposition", "attachment;filename=RegContractorLists.pdf");
        }
        else
        {
            Response.AddHeader("content-disposition", "attachment;filename=UnRegContractorLists.pdf");
        }
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(fileStream.ToArray());
        // Response.End();
        HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
    }
}