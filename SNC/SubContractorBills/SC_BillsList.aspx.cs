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

namespace SNC.SubContractorBills
{
    public partial class SC_BillsList : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        WorkOrderBL objViewWOBL = null;
        SubContractorBillBL objSubContBL = null;
        DataSet ds = null;
        DataSet Accessds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UID"] != null)
                {
                   
                    if (!IsPostBack)
                    {
                        BindSub_ContractorBillList();
                       
                    }
                }
                else
                {
                    Response.Redirect("../CommonPages/Login.aspx", false);
                }
            }
            catch (Exception ex)
            {
               SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void BindSub_ContractorBillList()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();
                objSubContBL.BillingFrom_Date = DateTime.Now;
                objSubContBL.Billing_To_Date = DateTime.Now;
                objSubContBL.Project_Code = Session["Project_Code"].ToString();
                objSubContBL.Task = "Get_SubContractorBillList";
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SUB_CONTRACTOR_BILL_LABOUR_LIST, ref ds);
                Grid_Sub_Contractor_Bill.DataSource = ds;
                Grid_Sub_Contractor_Bill.DataBind();
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void btnExWOrtToPDF_Click(object sender, EventArgs e)
        {
            try
            {
                //Grid_WO.PageSize = -1;
                //Grid_WO.DataBind();
                //ExWOrtGridToPDF(Grid_WO);
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        private void ExWOrtGridToPDF(Obout.Grid.Grid GirdData)
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

        protected void Grid_SC_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
                {
                    HyperLink Editlik = e.Row.Cells[0].FindControl("lnkRA_Bill_No") as HyperLink;
                    Editlik.NavigateUrl = "~/SubContractorBills/SC_Bill.aspx?RA_Bill_No=" + Editlik.Text;
                    if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                    {
                        if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                        {
                            if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["WO_Update"].ToString()))
                            {
                                Editlik.NavigateUrl = "";
                            }
                        }
                    }

                    //if(Session["Is_CFO_User"]!=null)
                    //{
                    LinkButton lnkWO_Delete = e.Row.Cells[16].FindControl("lnkWO_Delete") as LinkButton;

                    if (Accessds.Tables[0].Rows[0]["Appr_WO_Delete"].ToString() == "" || Accessds.Tables[0].Rows[0]["Appr_WO_Delete"].ToString() == "False" || Accessds.Tables[0].Rows[0]["Appr_WO_Delete"].ToString() == "0")
                    {
                        if (e.Row.Cells[14].Text == "Approved")
                        {
                            lnkWO_Delete.Visible = false;
                        }
                        else
                        {
                            lnkWO_Delete.Visible = true;
                        }
                    }
                    else
                    {
                        lnkWO_Delete.Visible = true;
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void Grid_WO_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            try
            {
                objViewWOBL = new WorkOrderBL();
                objViewWOBL.WONo = e.Record["WONo"].ToString();
                if (objViewWOBL.delete(con, WorkOrderBL.eLoadSp.WO_Delete))
                {
                    //BindWorkOrderList();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('WO has been deleted successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('WO is refered other process!');", true);
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void btn_GenWOItemsList_Click(object sender, EventArgs e)
        {
            BindWOItemsList();
            ItemList_Gv.Visible = true;
        }

        protected void BindWOItemsList()
        {
            try
            {
                //ds = new DataSet();
                //objViewWOBL = new WorkOrderBL();
                //if (Session["Project_Code"] != null)
                //{
                //    objViewWOBL.ProjectCode = Session["Project_Code"].ToString();
                //    objViewWOBL.load(con, WorkOrderBL.eLoadSp.SELECT_WO_ITEMS_ALL_BY_PROJECT, ref ds);
                //    Gv_WOItemsList.DataSource = ds;
                //    Gv_WOItemsList.DataBind();
                //}
            }
            catch (Exception ex)
            {
               SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void lnkSC_Delete_Click(object sender, CommandEventArgs e)
        {
            try
            {
                if (HF_Confirm.Value != "false")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    objSubContBL = new SubContractorBillBL();
                    objSubContBL.Task = "DeleteSubContracotrBill";
                    if (objSubContBL.delete(con, SubContractorBillBL.eLoadSp.SC_DELETE))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Sub Contractor Bill has been deleted successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Sub Contractor Bill is refered other process!');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
    }
}