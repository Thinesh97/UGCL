using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;
using SNC.ErrorLogger;
using System.Collections;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

    public partial class AssetMappingToProjectList : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        AssetTransferBL objassetTransferBL = null;
        DataSet ds = null;
        DataSet Accessds = new DataSet(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UID"] != null)
            {
                ActionPermission();
                if (!IsPostBack)
                {
                    BINDFROMTOList();
                    if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                    {

                        if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin" && !Convert.ToBoolean(Accessds.Tables[0].Rows[0]["AssTra_View"].ToString()))
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
        protected void BINDFROMTOList()
        {
            try
            {

                ds = new DataSet();
                objassetTransferBL = new AssetTransferBL();
                objassetTransferBL.Project_Code = Session["Project_Code"].ToString();
                objassetTransferBL.load(con, AssetTransferBL.eLoadSp.BIND_FROM_TO_PROJECT_LIST, ref ds);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    GV_TOFromGrid.DataSource = ds;
                    GV_TOFromGrid.DataBind();
                }

            }
            catch(Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
                            if (Convert.ToBoolean(Accessds.Tables[0].Rows[0]["AssTra_Create"].ToString()))
                            {
                                lnkbtnAdd.Visible = true;
                            }
                            else
                            {
                                lnkbtnAdd.Visible = false;
                            }
                            if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["AssTra_Delete"].ToString()))
                            {
                                //GridAssetTransfer.Columns[8].Visible = false;
                            }
                            else
                            {
                               // GridAssetTransfer.Columns[8].Visible = true;
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

        protected void linkAssetTranId_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format("../Asset/AssetMappingToProject.aspx?ID=" + ((LinkButton)sender).Text, false));
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void btnExportToPDF_Click(object sender, EventArgs e)
        {
            try
            {
                GV_AssetDetailsGrid.PageSize = -1;
                GV_AssetDetailsGrid.DataBind();
                ExportGridToPDF(GV_AssetDetailsGrid);
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
                Paragraph paragraph = new Paragraph("                                                               Asset Transfer List");
                PdfPTable PdfTable = new PdfPTable(GirdData.Columns.Count-3);
                PdfPCell PdfPCell_Data = null;
                foreach (Obout.Grid.Column col in GirdData.Columns)
                {
                    if (col.HeaderText != "Print" & col.HeaderText != "Select All" & col.HeaderText != "AssetTran_ID")
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
                        if (col.HeaderText != "Print" & col.HeaderText != "Select All" & col.HeaderText != "AssetTran_ID")
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
            Response.AddHeader("content-disposition", "attachment;filename= AssetTransferList.pdf");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(fileStream.ToArray());
            // Response.End();
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

        }

        protected void GridAssetTransfer_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            try
            {
                objassetTransferBL = new AssetTransferBL();
                objassetTransferBL.AssetTran_ID = Convert.ToInt32(e.Record["AssetTran_ID"].ToString());
                if(objassetTransferBL.delete(con,AssetTransferBL.eLoadSp.DELETE_ASSET_TRANSFER_BY_ID))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Asset Transfer has been deleted sucessfully.');", true);
                   // BindAssetTransfer();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to Delete the Asset Transfer.');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            } 
        }

        protected void GridAssetTransfer_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
                {
                    HyperLink Editlink = e.Row.Cells[0].FindControl("lnkAssetTranID") as HyperLink;

                    Editlink.NavigateUrl = "~/Asset/AssetMappingToProject.aspx?ID=" + Editlink.Text;

                    if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                    {
                        if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                        {
                            if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["AssTra_Update"].ToString()))
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

        protected void lnkbtnToProject_Click(object sender, EventArgs e)
        {
            try
            {
                ds = new DataSet();
                DataTable dt = new DataTable();
                objassetTransferBL = new AssetTransferBL();
                ViewState["Project_Code"] = ((LinkButton)sender).CommandArgument.ToString();
                REUSE_GRID_METHOD();
              


            }
            catch(Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void REUSE_GRID_METHOD()
        {
            objassetTransferBL = new AssetTransferBL();
            objassetTransferBL.Project_Code = ViewState["Project_Code"].ToString();
            objassetTransferBL.load(con, AssetTransferBL.eLoadSp.BIND_ASSET_DETAILS_FROM_TO_Project, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {


                GV_AssetDetailsGrid.DataSource = ds;
                GV_AssetDetailsGrid.DataBind();
                
            }
            else
            {
                GV_AssetDetailsGrid.DataSource = null;
                GV_AssetDetailsGrid.DataBind();

            }
        }
        protected void btnCancelList_Click(object sender, EventArgs e)
        {
            try
            {
                ds = new DataSet();
                objassetTransferBL = new AssetTransferBL();
                if (GV_AssetDetailsGrid.SelectedRecords != null)
                {
                    foreach (Hashtable ht in GV_AssetDetailsGrid.SelectedRecords)
                    {
                        objassetTransferBL.AssetTran_ID = Convert.ToInt32(ht["AssetTran_ID"]);

                        if (objassetTransferBL.Statusupdate(con, AssetTransferBL.eLoadSp.STATUS_UPADTE))
                        {
                            REUSE_GRID_METHOD();
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Asset Status Updated sucessfully.');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select any one or more assets .');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
            finally
            {
                GV_AssetDetailsGrid.SelectedRecords = null;
            }
        }

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
              
                string lnkbtnPrin = ((LinkButton)sender).CommandName.ToString();
                string pageurl = "../Asset/Print_Internal_Transfer_Assets.aspx?ID=" + lnkbtnPrin;
                Response.Write("<script> window.open( '" + pageurl + "','_blank' ); </script>");


                //Response.Redirect(string.Format("../Asset/Print_Internal_Transfer_Assets.aspx?ID=" + ((LinkButton)sender).CommandName.ToString(), false));
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }       
    }
