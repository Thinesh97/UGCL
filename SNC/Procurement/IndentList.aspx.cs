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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class IndentList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    IndentBL objIndentBL = null;
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

                    BindIndentList();
                    if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                    {

                        if (Accessds.Tables[0].Rows.Count > 0)
                        {
                            if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin" && !Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Ind_View"].ToString()))
                            {
                                Response.Redirect("~/CommonPages/Home.aspx", false);
                            }
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
                        if (Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Ind_Create"].ToString()))
                        {
                            lnkbtnAdd.Visible = true;
                        }
                        else
                        {
                            lnkbtnAdd.Visible = false;
                        }
                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Ind_Delete"].ToString()))
                        {
                            Grid_Indent.Columns[9].Visible = false;
                        }
                        else
                        {
                            Grid_Indent.Columns[9].Visible = true;
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
    
    protected void BindIndentList()
    {
        try
        {
            ds = new DataSet();
            objIndentBL = new IndentBL();
            objIndentBL.Project_Code = Session["Project_Code"].ToString();
            objIndentBL.load(con, IndentBL.eLoadSp.SELECT_INDENT_ALL_NEW, ref ds);
            if (ds != null && ds.Tables.Count > 0)
            {
                Grid_Indent.DataSource = ds;
                Grid_Indent.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindIndentItemsList()
    {
        try
        {
            ds = new DataSet();
            objIndentBL = new IndentBL();
            if (Session["Project_Code"] != null)
            {
                objIndentBL.Project_Code = Session["Project_Code"].ToString();
                objIndentBL.load(con, IndentBL.eLoadSp.SELECT_ALL_INDENT_ITEMS_LIST_BY_Project, ref ds);
                Gv_IndItemsList.DataSource = ds;
                Gv_IndItemsList.DataBind();
            }

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
            Grid_Indent.PageSize = -1;
            Grid_Indent.DataBind();
            ExportGridToPDF(Grid_Indent);

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
            Paragraph paragraph = new Paragraph("                                                                   Indent Details");
            PdfPTable PdfTable = new PdfPTable(GirdData.Columns.Count-1);
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
        Response.AddHeader("content-disposition", "attachment;filename=IndentDetails.pdf");
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(fileStream.ToArray());
        // Response.End();
        HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

    }

    protected void Grid_Indent_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {

            objIndentBL = new IndentBL();
            objIndentBL.Indent_No = Convert.ToString(e.Record["Indent_No"].ToString());
            if (objIndentBL.delete(con, IndentBL.eLoadSp.DELETE_INDENT_BY_INDENTNO))
            {
                BindIndentList();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Indent has been  deleted sucessfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete the indent because it use some where');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }

    }

    protected void Grid_Indent_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                HyperLink Editlink = e.Row.Cells[0].FindControl("lnkIndentNo") as HyperLink;

                Editlink.NavigateUrl = "~/Procurement/Indent.aspx?IndentNo=" + Editlink.Text;

                if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                {
                    if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                    {
                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Ind_Update"].ToString()))
                        {
                            Editlink.NavigateUrl = "";
                        }
                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Ind_Delete"].ToString()))
                        {
                            Grid_Indent.Columns[9].Visible = false;
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

    protected void btn_GenIndItemsList_Click(object sender, EventArgs e)
    {
        BindIndentItemsList();
        ItemList_Gv.Visible = true;
    }
}
