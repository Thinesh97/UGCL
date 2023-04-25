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

namespace SNC.Project
{
    public partial class LocationWorkTaskAssignment : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        WorkLocationBL objWorkLocationBL = null;
        SubContractorBillBL objSubContBL = null;
        DataSet ds = null;
        DataSet Accessds = new DataSet();  
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Div_ReplaytoLetter.Visible = false;
                if (Session["UID"] != null)
                {
                    BindWorkLocation_All();
                    BindWorkLocation_All_ListGird();
                    btnLocation_VED.Visible = true;
                    Get_WorkName_All();
                    Get_SubWorkName_All();
                    BindUOMList();
                    BindMaterialDetails();
                    BindAssetRegistrationList();
                    BindSubContracotr();
                    Bind_Material_All();
                    BindGrid_Labour_Type();
                }
                if (Request.QueryString["LWT_ID"]!=null)
                {
                   
                    GetLWT_BYID(Convert.ToInt32(Request.QueryString["LWT_ID"]));
                }
            }
        }
        protected void GetLWT_BYID(int LWT_ID)
        {
            objWorkLocationBL = new WorkLocationBL();
            ds = new DataSet();
            Get_SubWorkName_All();
            btnSubWorkName_VED.Visible = true;
            if (Session["Project_Code"] != null)
            {
                objWorkLocationBL.ID = Convert.ToInt32(LWT_ID);
                objWorkLocationBL.Task = "Select_LWT_BY_ID";
                objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                ddlLocation.SelectedValue = ds.Tables[0].Rows[0]["Location_ID"].ToString();
                ddlWorkName.SelectedValue = ds.Tables[0].Rows[0]["WorkName_ID"].ToString();
                ddlSubWorkName.SelectedValue = ds.Tables[0].Rows[0]["SubWorkName_ID"].ToString();
                ds.Clear();
                ddlLocation_SelectedIndexChanged(null,null);
                ds.Clear();
                ddlWorkName_SelectedIndexChanged(null, null);
                BindSubWorkForSC();
                ds.Clear();
                ddlSubWorkName_SelectedIndexChanged(null, null);
                ds.Clear();
                Div_SubworkDetail.Visible = true;
                Get_All_SubWorkRequired_Material();
                ds.Clear();
                Get_All_SubWorkRequired_Machinery();
                ds.Clear();
                Get_All_SubWorkRequired_Labours();
                btnSaveAll.Visible = false;
            }
        }
        protected void BindSubWorkForSC()
        {
            objWorkLocationBL = new WorkLocationBL();
            ds = new DataSet();
            Get_SubWorkName_All();
            btnSubWorkName_VED.Visible = true;
            if (Session["Project_Code"] != null)
            {
                objWorkLocationBL.WorkName_ID = Convert.ToInt32(ddlWorkName.SelectedValue);
                objWorkLocationBL.Task = "Get_SubWorkName_All";
                objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                ddlSubworkAssingment.DataSource = ds;
                ddlSubworkAssingment.DataTextField = "SubWork_Name";
                ddlSubworkAssingment.DataValueField = "ID";
                ddlSubworkAssingment.DataBind();
                ddlSubworkAssingment.Items.Insert(0, "-Select-");
               
            }
            else
            {
                ddlSubWorkName.DataSource = null;
                ddlSubWorkName.DataBind();
            }
        }
        protected void BindSubWork()
        {
            objWorkLocationBL = new WorkLocationBL();
            ds = new DataSet();
            Get_SubWorkName_All();
            btnSubWorkName_VED.Visible = true;
            if (Session["Project_Code"] != null)
            {
                objWorkLocationBL.WorkName_ID = Convert.ToInt32(ddlWorkName.SelectedValue);
                objWorkLocationBL.Task = "Get_SubWorkName_All";
                objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                ddlSubWorkName.DataSource = ds;
                ddlSubWorkName.DataTextField = "SubWork_Name";
                ddlSubWorkName.DataValueField = "ID";
                ddlSubWorkName.DataBind();
                ddlSubworkAssingment.DataSource = ds;
                ddlSubworkAssingment.DataTextField = "SubWork_Name";
                ddlSubworkAssingment.DataValueField = "ID";
                ddlSubworkAssingment.DataBind();
                ddlSubworkAssingment.Items.Insert(0, "-Select-");
                ddlSubWorkName.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlSubWorkName.DataSource = null;
                ddlSubWorkName.DataBind();
            }
        }
        protected void BindWorkName()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
                Get_WorkName_All();
                btnWorkName_VED.Visible = true;
                if (Session["Project_Code"] != null)
                {
                    objWorkLocationBL.Work_Location_ID = Convert.ToInt32(ddlLocation.SelectedValue);
                    objWorkLocationBL.Task = "Get_WorkName_BY_ID";
                    objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                    objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                    ddlWorkName.DataSource = ds;
                    ddlWorkName.DataTextField = "Work_Name";
                    ddlWorkName.DataValueField = "ID";
                    ddlWorkName.DataBind();

                    ddlWorkName.Items.Insert(0, "-Select-");

                }
                else
                {
                    ddlWorkName.DataSource = null;
                    ddlWorkName.DataBind();
                }

            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }
        protected void Get_SubWorkName_All()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();

                if (Session["Project_Code"] != null)
                {
                    if (ddlWorkName.SelectedValue != "Select" && ddlWorkName.SelectedItem.Text != "")
                    {
                        objWorkLocationBL.WorkName_ID = Convert.ToInt32(ddlWorkName.SelectedValue);
                    }
                    objWorkLocationBL.Task = "Get_SubWorkName_All";
                    objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                    objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                    Grid_SubWork_Name_VED.DataSource = ds;
                    Grid_SubWork_Name_VED.DataBind();
                }
                else
                {
                    Grid_SubWork_Name_VED.DataSource = null;
                    Grid_SubWork_Name_VED.DataBind();
                }

            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }
        protected void Get_WorkName_All()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();

                if (Session["Project_Code"] != null)
                {
                    if (ddlLocation.SelectedValue != "Select" && ddlLocation.SelectedItem.Text != "")
                    {
                        objWorkLocationBL.Work_Location_ID = Convert.ToInt32(ddlLocation.SelectedValue);
                    }
                    objWorkLocationBL.Task = "Get_WorkName_All";
                    objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                    objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                    Grid_Work_Name.DataSource = ds;
                    Grid_Work_Name.DataBind();
                }
                else
                {
                    Grid_Work_Name.DataSource = null;
                    Grid_Work_Name.DataBind();
                }

            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

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
               
            }
            catch (Exception ex)
            {
              
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
                Paragraph paragraph = new Paragraph("                                                                       Project List Details");
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
            Response.AddHeader("content-disposition", "attachment;filename=ProjectLists.pdf");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(fileStream.ToArray());
            // Response.End();
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

        }
        protected void btnCancelWorkName_Click(object sender, EventArgs e)
        {
            txtWorkName.Text = string.Empty;
        }
        protected void ddlWorkName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
                Get_SubWorkName_All();
                btnSubWorkName_VED.Visible = true;
                if (Session["Project_Code"] != null)
                {
                    objWorkLocationBL.WorkName_ID =Convert.ToInt32(ddlWorkName.SelectedValue);
                    objWorkLocationBL.Task = "Get_SubWorkName_All";
                    objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                    objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                    ddlSubWorkName.DataSource = ds;
                    ddlSubWorkName.DataTextField = "SubWork_Name";
                    ddlSubWorkName.DataValueField = "ID";
                    ddlSubWorkName.DataBind();

                    ddlSubWorkName.Items.Insert(0, "-Select-");
                }
                else
                {
                    ddlSubWorkName.DataSource = null;
                    ddlSubWorkName.DataBind();
                }

            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
                Get_WorkName_All();
                btnWorkName_VED.Visible = true;
                if (Session["Project_Code"] != null)
                {
                    objWorkLocationBL.Work_Location_ID =Convert.ToInt32(ddlLocation.SelectedValue);
                    objWorkLocationBL.Task = "Get_WorkName_BY_ID";
                    objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                    objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                    ddlWorkName.DataSource = ds;
                    ddlWorkName.DataTextField = "Work_Name";
                    ddlWorkName.DataValueField = "ID";
                    ddlWorkName.DataBind();

                    ddlWorkName.Items.Insert(0, "-Select-");
                   
                }
                else
                {
                    ddlWorkName.DataSource = null;
                    ddlWorkName.DataBind();
                }

            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }
        protected void BindWorkLocation_All_ListGird()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();

                if (Session["Project_Code"] != null)
                {
                    objWorkLocationBL.Task = "Get_WorkLocation_All";
                    objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                    objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                    Grid_Location_List.DataSource = ds;
                    Grid_Location_List.DataBind();
                }
                else
                {
                    Grid_Location_List.DataSource = null;
                    Grid_Location_List.DataBind();
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }
        protected void BindUOMList()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
                objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_UOM_ALL, ref ds);
                ddlUOM.DataSource = ds;
                ddlUOM.DataValueField = "UOM_ID";
                ddlUOM.DataTextField = "UOMPrefix";
                ddlSubWorkUOM.DataSource = ds;
                ddlSubWorkUOM.DataValueField = "UOM_ID";
                ddlSubWorkUOM.DataTextField = "UOMPrefix";
                
                ddlLaboursUOM.DataSource = ds;
                ddlLaboursUOM.DataTextField = "UOMPrefix";
                ddlLaboursUOM.DataValueField = "UOM_ID";
                ddlLaboursUOM.SelectedValue = "64";
                ddlUOM.DataBind();
                ddlSubWorkUOM.DataBind();
                
                ddlLaboursUOM.DataBind();
                ddlUOM.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {
              
            }
        }
        private void BindMaterialDetails()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
                objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_ALL_MATERIALDETAILS, ref ds);
                {
                    ddlMachineryName.DataSource = ds;
                    ddlMachineryName.DataValueField = "Item_Code";
                    ddlMachineryName.DataTextField = "Item_Name";

                }
            }
            catch (Exception ex)
            {
               
            }
        }
        protected void btnSubWorkName_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlWorkName.SelectedItem.Text != "-Select-")
                {
                    objWorkLocationBL = new WorkLocationBL();
                objWorkLocationBL.Task = "Insert_SubWorkName";
                objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                objWorkLocationBL.WorkName_ID =Convert.ToInt32(ddlWorkName.SelectedValue);
                objWorkLocationBL.SubWork_Name = txtSubWorkName.Text.Trim();
                objWorkLocationBL.UOM = ddlSubWorkUOM.SelectedItem.Text;
                objWorkLocationBL.Quantity = txtSubWorkQuantity.Text.Trim();
                if (objWorkLocationBL.SubWorkNameinsert(con, WorkLocationBL.eLoadSp.INSERT_WORK_LOCATION))
                {
                    BindSubWork();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Sub Work Name details has been Created');", true);
                    txtSubWorkName.Text = string.Empty;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to add the Sub Work Name');", true);
                }
            
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please Select Work Name');", true);
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnCancelSubWorkName_Click(object sender, EventArgs e)
        {
            txtSubWorkName.Text = string.Empty;
        }

        protected void BindSubContracotr()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
                objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_CONTRACTOR_ALL, ref ds);
                ddlSubContractor.DataSource = ds;
                ddlSubContractor.DataTextField = "Subcon_name";
                ddlSubContractor.DataValueField = "Subcon_ID";
                ddlSubContractor.DataBind();
                ddlSubContractor.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void ddlSubWorkName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
                Div_SubworkDetail.Visible = true;
                if (ddlSubWorkName.SelectedItem.Text!="-Select-")
                {
                    Get_All_SubWorkRequired_Material();

                    Get_All_SubWorkRequired_Machinery();
                    Get_All_SubWorkRequired_Labours();
                }
                if (Session["Project_Code"] != null)
                {
                    if (ddlWorkName.SelectedValue != "" || ddlWorkName.SelectedItem.Text != "")
                    {
                        objWorkLocationBL.WorkName_ID = Convert.ToInt32(ddlWorkName.SelectedValue);
                    }
                    objWorkLocationBL.Task = "Get_SubWorkName_All";
                    objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                    objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                    lblworkname.InnerText = ddlWorkName.SelectedItem.Text;
                    lblSubworkname.InnerText = ds.Tables[0].Rows[0]["SubWork_Name"].ToString();
                    lblSubworQty.InnerText = ds.Tables[0].Rows[0]["Quantity"].ToString();
                    lblSubworuom.InnerText = ds.Tables[0].Rows[0]["UOM"].ToString();
                }
                else
                {
                    
                }

            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }
        protected void btnviewWorkName_Click(object sender, EventArgs e)
            {

            }
       //protected void ddlSubWorkName_SelectedIndexChanged(object sender, EventArgs e)
       // {
       //     try
       //     {
       //         objWorkLocationBL = new WorkLocationBL();
       //         ds = new DataSet();

       //         if (Session["Project_Code"] != null)
       //         {
       //             objWorkLocationBL.ID = Convert.ToInt32(Request.QueryString["WN_ID"]);
       //             objWorkLocationBL.Task = "Get_WorkName_BY_ID";
       //             objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
       //             objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
       //             //lblWorkName.InnerText = ds.Tables[0].Rows[0]["Work_Name"].ToString();
       //         }
       //     }
       //     catch (Exception ex)
       //     {
       //         SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

       //     }
       // }

        //protected void Grid_Work_LocationsRowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
        //        {
        //            HyperLink Editlik = e.Row.Cells[0].FindControl("lnk_ID") as HyperLink;
        //            Editlik.NavigateUrl = "~/Project/SubWorkReqMapping.aspx?SWN_ID=" + Editlik.Text;
        //            if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
        //            {
        //                if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
        //                {
        //                    if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["WO_Update"].ToString()))
        //                    {
        //                        Editlik.NavigateUrl = "";
        //                    }
        //                }
        //            }
        //            LinkButton lnkWO_Delete = e.Row.Cells[3].FindControl("lnkDPR_Delete") as LinkButton;


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        //    }
        //}
        protected void btnWorkName_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlLocation.SelectedItem.Text!="-Select-")
                {
                    objWorkLocationBL = new WorkLocationBL();
                objWorkLocationBL.Task = "Insert_WorkName";
                objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                objWorkLocationBL.Work_Location_ID =Convert.ToInt32(ddlLocation.SelectedValue);
                objWorkLocationBL.Work_Name = txtWorkName.Text.Trim();
                if (objWorkLocationBL.WorkNameinsert(con, WorkLocationBL.eLoadSp.INSERT_WORK_LOCATION))
                {
                    BindWorkName();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Work Name details has been Created');", true);
                    txtWorkName.Text = string.Empty;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to add the Work Name');", true);
                }
            }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please Select Location');", true);
                }
                }
	
                
            catch (Exception ex)
            {

            }
        }
        protected void BindWorkLocation_All()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();

                if (Session["Project_Code"] != null)
                {
                    objWorkLocationBL.Task = "Get_WorkLocation_All";
                    objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                    objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                    ddlLocation.DataSource = ds;
                    ddlLocation.DataTextField = "Work_Location";
                    ddlLocation.DataValueField = "ID";
                    ddlLocation.DataBind();

                    ddlLocation.Items.Insert(0, "-Select-");
                }
                else
                {
                    ddlLocation.DataSource = null;
                    ddlLocation.DataBind();
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }
        protected void ProjectList_Grid_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            try
            {
                //ds = new DataSet();
                //objProjectBL = new ProjectBL();

                //objProjectBL.Proj_Code = e.Record["Project_Code"].ToString();

                //if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_PROJECT_FROM_LIST))
                //{
                //    BindProjectList();
                //    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Project list type has been deleted successfully');", true);
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Project List type cannot be deleted as it is already in use.');", true);
                //}
            }
            catch (Exception ex)
            {
               
            }
        }
        protected void ProjectList_Grid_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
                {
                    HyperLink Editlik = e.Row.Cells[0].FindControl("lnkProjectID") as HyperLink;

                    Editlik.NavigateUrl = "~/Project/Project.aspx?ID=" + Editlik.Text;

                    if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                    {
                        if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                        {
                            if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Proj_Update"].ToString()))
                            {
                                Editlik.NavigateUrl = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void btnCancelWorklocation_Click(object sender, EventArgs e)
        {
            txtWorkLocation.Text = string.Empty;
            }
        protected void btnWorklocation_Click(object sender, EventArgs e)
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                objWorkLocationBL.Task = "Insert_WorkLocation";
                objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                objWorkLocationBL.Work_Location = txtWorkLocation.Text.Trim();
                if (objWorkLocationBL.WorkLocationinsert(con, WorkLocationBL.eLoadSp.INSERT_WORK_LOCATION))
                {
                    BindWorkLocation_All();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Work Location details has been Created');", true);
                    txtWorkLocation.Text = string.Empty;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to add the Work Location');", true);
                }
            }
            catch (Exception ex)
            {
              
            }
        }
      
        protected void btnCancelRequired_Material_Click(object sender, EventArgs e)
        {
           
            txtRequired_MaterialQuantity.Text = string.Empty;
            ddlRequired_MaterialName.Text = string.Empty;
        }

        protected void btnSaveRequired_Material_Click(object sender, EventArgs e)
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                objWorkLocationBL.Task = "Insert_SubWorkRequired_Material";
                objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                objWorkLocationBL.SubWork_ID =Convert.ToInt32(ddlSubWorkName.SelectedValue);
                objWorkLocationBL.Material_Name = ddlRequired_MaterialName.SelectedItem.Text;
                objWorkLocationBL.SubWork_UOM = ddlUOM.SelectedItem.Text;
                objWorkLocationBL.SubWork_Quantity = Convert.ToDecimal(txtRequired_MaterialQuantity.Text.Trim());
                if (objWorkLocationBL.SubWorkRequired_Materialinsert(con, WorkLocationBL.eLoadSp.INSERT_WORK_LOCATION))
                {
                    Get_All_SubWorkRequired_Material();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Required Material details has been Created');", true);
                    txtSubWorkName.Text = string.Empty;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to add the Required Material');", true);
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnAddSOW_Click(object sender, EventArgs e)
        {
            
            ModalSOWItem.Show();
        }
        protected void btnAssignSC_Click(object sender, EventArgs e)
        {

            ModalPopupAssignSC.Show();
        }
        protected void Get_All_SubWorkRequired_Material()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();

                if (Session["Project_Code"] != null)
                {
                    objWorkLocationBL.SubWork_ID =Convert.ToInt32(ddlSubWorkName.SelectedValue);
                    objWorkLocationBL.Task = "Select_All_SubWorkRequired_Material";
                    objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_MATERIAL_ALL, ref ds);
                    Grid_RequiredMaterial.DataSource = ds;
                    Grid_RequiredMaterial.DataBind();
                }
                else
                {
                    Grid_RequiredMaterial.DataSource = null;
                    Grid_RequiredMaterial.DataBind();
                }

            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }
        protected void Get_All_SubWorkRequired_Machinery()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();

                if (Session["Project_Code"] != null)
                {
                    objWorkLocationBL.SubWork_ID = Convert.ToInt32(ddlSubWorkName.SelectedValue);
                    objWorkLocationBL.Task = "Select_All_SubWorkRequired_Machinery";
                    objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_MATERIAL_ALL, ref ds);
                    GridRequiredMachinery.DataSource = ds;
                    GridRequiredMachinery.DataBind();
                }
                else
                {
                    GridRequiredMachinery.DataSource = null;
                    GridRequiredMachinery.DataBind();
                }

            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }
        protected void Get_All_SubWorkRequired_Labours()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();

                if (Session["Project_Code"] != null)
                {
                    objWorkLocationBL.SubWork_ID = Convert.ToInt32(ddlSubWorkName.SelectedValue);
                    objWorkLocationBL.Task = "Select_All_SubWorkRequired_Labours";
                    objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_MATERIAL_ALL, ref ds);
                    GridRequiredLabours.DataSource = ds;
                    GridRequiredLabours.DataBind();
                }
                else
                {
                    GridRequiredLabours.DataSource = null;
                    GridRequiredLabours.DataBind();
                }

            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }

        protected void BindAssetRegistrationList()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
                objWorkLocationBL.Task = "Select_All_Asset_Type";
                objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                ddlAsset_Type.DataSource = ds;
                ddlAsset_Type.DataTextField = "Asset_Type";
                ddlAsset_Type.DataValueField = "Asset_Type_ID";
                ddlAsset_Type.DataBind();

                ddlAsset_Type.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {
               
            }
        }
        protected void ddlAssetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
                ModalSOWItem.Show();
                if (Session["Project_Code"] != null)
                {
                    objWorkLocationBL.ID = Convert.ToInt32(ddlAsset_Type.SelectedValue);
                    objWorkLocationBL.Task = "Select_All_Asset_Category";
                    objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                    ddlAssetCategory.DataSource = ds;
                    ddlAssetCategory.DataTextField = "Category_Name";
                    ddlAssetCategory.DataValueField = "Asset_cat_ID";
                    ddlAssetCategory.DataBind();

                    ddlAssetCategory.Items.Insert(0, "-Select-");
                }
                else
                {
                    ddlSubWorkName.DataSource = null;
                    ddlSubWorkName.DataBind();
                }
                ModalSOWItem.Show();
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }
        protected void ddlAssetCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
                ModalSOWItem.Show();
                if (Session["Project_Code"] != null)
                {
                    objWorkLocationBL.ID = Convert.ToInt32(ddlAssetCategory.SelectedValue);
                    objWorkLocationBL.Task = "Select_All_Asset_Name";
                    objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                    ddlMachineryName.DataSource = ds;
                    ddlMachineryName.DataTextField = "Name";
                    ddlMachineryName.DataValueField = "Code";
                    ddlMachineryName.DataBind();

                    ddlMachineryName.Items.Insert(0, "-Select-");
                }
                else
                {
                    ddlMachineryName.DataSource = null;
                    ddlMachineryName.DataBind();
                }
                ModalSOWItem.Show();
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }
        protected void ddlMachineryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
                ModalSOWItem.Show();
                if (Session["Project_Code"] != null)
                {
                    objWorkLocationBL.ID = Convert.ToInt32(ddlMachineryName.SelectedValue);
                    objWorkLocationBL.Task = "Select_All_Asset_RegNo";
                    objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                    ddlMachineryRegNo.DataSource = ds;
                    ddlMachineryRegNo.DataTextField = "Reg_No";
                    ddlMachineryRegNo.DataValueField = "Code";
                    ddlMachineryRegNo.DataBind();

                    ddlMachineryRegNo.Items.Insert(0, "-Select-");
                }
                else
                {
                    ddlMachineryName.DataSource = null;
                    ddlMachineryName.DataBind();
                }
                ModalSOWItem.Show();
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }
       
        protected void btnSaveRequired_Machinery_Click(object sender, EventArgs e)
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                objWorkLocationBL.Task = "Insert_SubWorkRequired_Machinery";
                objWorkLocationBL.SubWork_ID = Convert.ToInt32(ddlSubWorkName.SelectedValue);
                objWorkLocationBL.Asset_Type = Convert.ToString(ddlAsset_Type.SelectedItem.Text);
                objWorkLocationBL.Asset_Category = Convert.ToString(ddlAssetCategory.SelectedItem.Text);
                objWorkLocationBL.Asset = Convert.ToString(ddlMachineryName.SelectedItem.Text);
                objWorkLocationBL.SubWork_Quantity = Convert.ToDecimal(txtMachineryQuantity.Text.Trim());
                objWorkLocationBL.Name = Convert.ToString(ddlMachineryName.SelectedItem.Text);
                objWorkLocationBL.Code = Convert.ToString(ddlMachineryName.SelectedValue);
                if (ddlMachineryRegNo.SelectedItem.Text!="Select")
                {
                    objWorkLocationBL.Asset_RegNo = Convert.ToString(ddlMachineryRegNo.SelectedValue);
                }
                else
                {
                    objWorkLocationBL.Asset_RegNo = "NA";
                }
                if (objWorkLocationBL.SubWorkRequired_Machineryinsert(con, WorkLocationBL.eLoadSp.INSERT_WORK_LOCATION))
                {
                    Get_All_SubWorkRequired_Machinery();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Required Machinery details has been Created');", true);
                    txtSubWorkName.Text = string.Empty;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to add the Required Machinery');", true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSaveRequired_Labours_Click(object sender, EventArgs e)
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                objWorkLocationBL.Task = "Insert_SubWorkRequired_Labours";
                objWorkLocationBL.SubWork_ID = Convert.ToInt32(ddlSubWorkName.SelectedValue);
                objWorkLocationBL.Type_Of_Labour = Convert.ToString(ddlLabour.SelectedItem.Text);
                objWorkLocationBL.Asset_Category = Convert.ToString(ddlAssetCategory.SelectedItem.Text);
                objWorkLocationBL.SubWork_UOM = Convert.ToString(ddlLaboursUOM.SelectedItem.Text);
                objWorkLocationBL.SubWork_Quantity = Convert.ToDecimal(txtLaboursQuantity.Text.Trim());
                if (objWorkLocationBL.SubWorkRequired_Labourinsert(con, WorkLocationBL.eLoadSp.INSERT_WORK_LOCATION))
                {
                    Get_All_SubWorkRequired_Labours();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Required Labours details has been Created');", true);
                    txtSubWorkName.Text = string.Empty;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to add the Required Labours');", true);
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnCancelRequired_Labours_Click(object sender, EventArgs e)
        {
            
           
        }
        protected void btnSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                objWorkLocationBL.Task = "Insert_LocationWorkTaskAssignment";
                objWorkLocationBL.Location_ID = Convert.ToInt32(ddlLocation.SelectedValue);
                objWorkLocationBL.WorkName_ID = Convert.ToInt32(ddlWorkName.SelectedValue);
                objWorkLocationBL.SubWorkName_ID = Convert.ToInt32(ddlSubWorkName.SelectedValue);
                objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                if (objWorkLocationBL.SubWorkRequired_Masterinsert(con, WorkLocationBL.eLoadSp.INSERT_WORK_LOCATION))
                {
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Required Master details has been Created');", true);
                    txtSubWorkName.Text = string.Empty;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to add the Required Master');", true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSaveContractor_Click(object sender, EventArgs e)
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                objWorkLocationBL.Task = "Insert_Subcontractor";
                objWorkLocationBL.Sub_ContractorID = Convert.ToInt32(ddlSubContractor.SelectedValue);
                objWorkLocationBL.SubWork_ID = Convert.ToInt32(ddlSubworkAssingment.SelectedValue);
                objWorkLocationBL.Location_ID = Convert.ToInt32(ddlLocation.SelectedValue);
                objWorkLocationBL.WorkName_ID = Convert.ToInt32(ddlWorkName.SelectedValue);
                objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                if (objWorkLocationBL.SubWorkRequired_SubContraContractor(con, WorkLocationBL.eLoadSp.INSERT_WORK_LOCATION))
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Required Work is Assigned to Sub Contractor details has been Created');", true);
                    txtSubWorkName.Text = string.Empty;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to Assign the Work');", true);
                }
            }
            catch (Exception ex)
            {

            }
        }
       
        protected void BindGrid_Labour_Type()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();

                objSubContBL.Task = "SelectAll_Labour_Type";
                objSubContBL.Project_Code = Session["Project_Code"].ToString();
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SUB_CONTRACTOR_BILL_OPERATIONS, ref ds);
                Grid_Labour_Type.DataSource = ds;
                Grid_Labour_Type.DataBind();
                ddlLabour.DataSource = ds;
                ddlLabour.DataTextField = "Labour_Type";
                ddlLabour.DataValueField = "ID";
                ddlLabour.DataBind();
                ddlLabour.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void Bind_Material_All()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
                if (Session["Project_Code"] != null)
                {
                    
                    objWorkLocationBL.Task = "Get_Material_All";
                    objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                    ddlRequired_MaterialName.DataSource = ds;
                    ddlRequired_MaterialName.DataTextField = "Item_Name";
                    ddlRequired_MaterialName.DataValueField = "Item_Code";
                    ddlRequired_MaterialName.DataBind();

                    ddlRequired_MaterialName.Items.Insert(0, "-Select-");
                }
                else
                {
                    ddlRequired_MaterialName.DataSource = null;
                    ddlRequired_MaterialName.DataBind();
                }
               
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }
        protected void btnLabourType_Click(object sender, EventArgs e)
        {
            try
            {
                objSubContBL = new SubContractorBillBL();
                objSubContBL.Project_Code = Session["Project_Code"].ToString();
                objSubContBL.Labour_Type = Convert.ToString(txtLabourType.Text);
                objSubContBL.Task = "InsertLabour_Type";

                if (objSubContBL.InsertLabour_Type(con, SubContractorBillBL.eLoadSp.INSERT_LABOURTYPE))
                {
                    BindGrid_Labour_Type();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Required Labour Type  has been Created');", true);


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to add the Required Labour Type');", true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCancelLabourType_Click(object sender, EventArgs e)
        {

        }
    }
}