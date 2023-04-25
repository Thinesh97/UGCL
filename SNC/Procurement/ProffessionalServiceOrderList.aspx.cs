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


    public partial class Proffessional_Service_Order : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    WorkOrderBL objViewWOBL = null;
    DataSet ds = null;
    DataSet Accessds = new DataSet();
    ProfessionalServiceOrderBL objViewPSO = null;
    protected void Page_Load(object sender, EventArgs e)
        {
        try
        {
            if (Session["UID"] != null)
            {
                //ActionPermission();
                if (!IsPostBack)
                {
                    BindPSoList();
                    //if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                    //{
                    //    if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin" && !Convert.ToBoolean(Accessds.Tables[0].Rows[0]["WO_View"].ToString()))
                    //    {
                    //        Response.Redirect("~/CommonPages/Home.aspx", false);
                    //    }
                    //}
                    //else
                    //{
                    //    Response.Redirect("~/CommonPages/Login.aspx", true);
                    //}
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
                        if (Convert.ToBoolean(Accessds.Tables[0].Rows[0]["WO_Create"].ToString()))
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

    protected void BindPSoList()
    {
        try
        {
            ds = new DataSet();
            objViewPSO = new ProfessionalServiceOrderBL();
            objViewPSO.ProjectCode = Session["Project_Code"].ToString();
            objViewPSO.Task = "PSOList";
            objViewPSO.load(con, ProfessionalServiceOrderBL.eLoadSp.pro_Proffessional_Service_Order_Select_All, ref ds);
            Grid_PSO.DataSource = ds;
            Grid_PSO.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnExWOrtToPDF_Click(object sender, EventArgs e)
    {
        try
        {
            Grid_PSO.PageSize = -1;
            Grid_PSO.DataBind();
            ExPSOrtGridToPDF(Grid_PSO);
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void ExPSOrtGridToPDF(Obout.Grid.Grid GirdData)
    {

        MemoryStream fileStream = new MemoryStream();
        Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
        try
        {
            PdfWriter wri = PdfWriter.GetInstance(doc, fileStream);
            doc.Open();
            Font font8 = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Color.WHITE);
            Paragraph paragraph = new Paragraph("Work Order Details");
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
        Response.AddHeader("content-disWOsition", "attachment;filename=WorkOrderDetails.pdf");
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(fileStream.ToArray());
        // ResWOnse.End();
        HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

    }

    protected void Grid_PSO_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                HyperLink Editlik = e.Row.Cells[0].FindControl("lnkPSONo") as HyperLink;
                Editlik.NavigateUrl = "~/Procurement/ProffessionalServiceOrder.aspx?PSONo=" + Editlik.Text;
                if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                {
                    if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                    {
                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PSO_Update"].ToString()))
                        {
                            Editlik.NavigateUrl = "";
                        }
                    }
                }

                //if(Session["Is_CFO_User"]!=null)
                //{
                LinkButton lnkPSO_Delete = e.Row.Cells[16].FindControl("lnkPSO_Delete") as LinkButton;

                if (Accessds.Tables[0].Rows[0]["Appr_PSO_Delete"].ToString() == "" || Accessds.Tables[0].Rows[0]["Appr_PSO_Delete"].ToString() == "False" || Accessds.Tables[0].Rows[0]["Appr_PSO_Delete"].ToString() == "0")
                {
                    if (e.Row.Cells[14].Text == "Approved")
                    {
                        lnkPSO_Delete.Visible = false;
                    }
                    else
                    {
                        lnkPSO_Delete.Visible = true;
                    }
                }
                else
                {
                    lnkPSO_Delete.Visible = true;
                }
                //}
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_PSO_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objViewPSO = new ProfessionalServiceOrderBL();
            objViewPSO.PSONo = e.Record["PSONo"].ToString();
            if (objViewPSO.delete(con, ProfessionalServiceOrderBL.eLoadSp.PSO_Delete))
            {
                BindPSoList();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('PSO has been deleted successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('PSO is refered other process!');", true);
            }
        }
        catch (Exception ex) 
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btn_GenWOItemsList_Click(object sender, EventArgs e)
    {
        BindPSOItemsList();
        ItemList_Gv.Visible = true;
    }

    protected void BindPSOItemsList()
    {
        //try
        //{
        //    ds = new DataSet();
        //    objViewWOBL = new WorkOrderBL();
        //    if (Session["Project_Code"] != null)
        //    {
        //        objViewWOBL.ProjectCode = Session["Project_Code"].ToString();
        //        objViewWOBL.load(con, WorkOrderBL.eLoadSp.SELECT_WO_ITEMS_ALL_BY_PROJECT, ref ds);
        //        Gv_WOItemsList.DataSource = ds;
        //        Gv_WOItemsList.DataBind();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        //}
    }

    protected void lnkPSO_Delete_Click(object sender, CommandEventArgs e)
    {
        try
        {
            if (HF_Confirm.Value != "false")
            {
                //type=   RegisterStartupScript(typeof(Page), "exampleScript", "if(confirm('are you confirm?')) { document.getElementById('btn').click(); } ", true)
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                Hashtable dataItem = Grid_PSO.Rows[rowIndex].ToHashtable() as Hashtable;

                objViewPSO = new ProfessionalServiceOrderBL();

                objViewPSO.PSONo = dataItem["PSONo"].ToString();
                objViewPSO.Task = "DeleteWorkOrder";
                if (objViewPSO.delete(con, ProfessionalServiceOrderBL.eLoadSp.PSO_Delete))
                {
                    BindPSoList();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('PSO has been deleted successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('PSO is refered other process!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
}
