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



    public partial class DailyMachineRunningHoursList : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        DailyRunningHourBL objDailyRunningHourBL = null;
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

                        BindDRHList();
                        if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                        {

                            if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin" && !Convert.ToBoolean(Accessds.Tables[0].Rows[0]["DailRun_View"].ToString()))
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
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
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
                            if (Convert.ToBoolean(Accessds.Tables[0].Rows[0]["DailRun_Create"].ToString()))
                            {
                                lnkbtnAdd.Visible = true;
                            }
                            else
                            {
                                lnkbtnAdd.Visible = false;
                            }
                            if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["DailRun_Delete"].ToString()))
                            {
                                DRHGrid.Columns[12].Visible = false;
                            }
                            else
                            {
                                DRHGrid.Columns[12].Visible = true;
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

        private void BindDRHList()
        {
            try
            {
                ds = new DataSet();
                objDailyRunningHourBL = new DailyRunningHourBL();
                objDailyRunningHourBL.Project_Code = Session["Project_Code"].ToString();
                objDailyRunningHourBL.load(con, DailyRunningHourBL.eLoadSp.SELECT_DAILY_RUNNING_HOUR_LIST,ref ds);
                DRHGrid.DataSource = ds;
                DRHGrid.DataBind();
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
            }
        }
     
        protected void lnkDailyRunId_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format("~/Asset/DailyMachineRunningHours.aspx?Daily_Run_Id=" + ((LinkButton)sender).Text.ToString(), false));
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
            }
        }

        protected void btnExportToPDF_Click(object sender, EventArgs e)
        {
            try
            {
                DRHGrid.PageSize = -1;
                DRHGrid.DataBind();
                ExportGridToPDF(DRHGrid);
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
                Paragraph paragraph = new Paragraph("                                                           Daily Machine Running Hours Details");
                PdfPTable PdfTable = new PdfPTable(GirdData.Columns.Count-3);
                PdfPCell PdfPCell_Data = null;
                foreach (Obout.Grid.Column col in GirdData.Columns)
                {
                    if (col.HeaderText != "Delete" & col.HeaderText != "Edit" & col.HeaderText != "Asset Code")
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
                        if (col.HeaderText != "Delete" & col.HeaderText != "Edit" & col.HeaderText != "Asset Code")
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
            Response.AddHeader("content-disposition", "attachment;filename=Daily Machine Running Hours List.pdf");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(fileStream.ToArray());
            // Response.End();
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

        }

        protected void DRHGrid_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            try
            {
                objDailyRunningHourBL = new DailyRunningHourBL();
                objDailyRunningHourBL.Daily_Run_Id = Convert.ToInt32(e.Record["Daily_Run_Id"]);
                objDailyRunningHourBL.UserID = Session["User_ID"].ToString();
                if (objDailyRunningHourBL.delete(con, DailyRunningHourBL.eLoadSp.Running_DELETE_TYPE_BY_ID))
                {
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Daily Running HoursList has been deleted sucessfully.');", true);
                    BindDRHList();

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Daily Running HoursList Item can not be deleted as it is already in use.');", true);
                }


            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void DRHGrid_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
                {
                    HyperLink Editlink = e.Row.Cells[0].FindControl("lnkDailyRunId") as HyperLink;

                    Editlink.NavigateUrl = "~/Asset/DailyMachineRunningHours.aspx?ID=" + Editlink.Text;

                    if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                    {
                        if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                        {
                            if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["DailRun_Update"].ToString()))
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
    }





