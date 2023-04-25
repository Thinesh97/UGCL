using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tally;
using TallyBridge;
using System.IO;
using BusinessLayer;
using System;
using SNC.ErrorLogger;
using System.Collections;

namespace SNC.SubContractorBills
{
    public partial class DailyProgressReport : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        SubContractorBillBL objSubContBL = null;
        WorkLocationBL objWorkLocationBL = null;
        ProjectBL objProjectBL = null;
        DataSet ds = null;
        DataSet Accessds = new DataSet();
        public decimal Cumulative_Progress = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSubContractorList();
                BindProject();
                BindWoNumber();
                BindUOM();
                BindNMRNumber();
                BindWorkLocation_All();
                BindWorkName();
                BindUOMList();
                Bind_Material_All();
                BindUOMListMaterialUOM();
                BindUOMListUsedMat();
                btnAddItem.Visible = true;
                Div_GridDPR.Visible = false;
                Worker_Div.Visible = false;
                btnAddItem.Visible = false;
                if (Request.QueryString["DPR_No"] != null)
                {
                    btnAddItem.Visible = true;
                    BindGrid_DPR_Entry(Request.QueryString["DPR_No"]);
                    GetDPRDetails(Request.QueryString["DPR_No"].ToString());
                    BindDiscriptionOfWork();
                    BindAssetRegistrationList();
                    Get_All_Consumed_Material_List();
                    ds.Clear();
                    Get_All_UtilizedMachinery_List();
                    ds.Clear();
                    Get_All_Utilized_Labours_List();
                }
            }
        }
        private void GetDPRDetails(string DPR_No)
        {
            try
            {
                objSubContBL = new SubContractorBillBL();
                ds = new DataSet();
                objSubContBL.DPR_No = DPR_No;
                objSubContBL.Task = "GetDPRDetails";
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SUB_CONTRACTOR_BILL_OPERATIONS, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtDPRNo.Text = ds.Tables[0].Rows[0]["DPR_No"].ToString();
                    ddlFYear.SelectedValue = ds.Tables[0].Rows[0]["Financial_Year"].ToString();
                    ddlProject.SelectedValue = ds.Tables[0].Rows[0]["Project_Code"].ToString();
                    ddlSubContractor.SelectedValue = ds.Tables[0].Rows[0]["SubContractorID"].ToString();
                    ddlWO.SelectedItem.Text = ds.Tables[0].Rows[0]["Work_OrderNO"].ToString();
                    txtWorkDescription.Text = ds.Tables[0].Rows[0]["Work_Description"].ToString();
                    if ( ds.Tables[0].Rows[0]["Location_ID"] !=null)
                    {
                        ddlLocation.SelectedValue =ds.Tables[0].Rows[0]["Location_ID"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["Work_Name_ID"] != null)
                    {
                        ddlWorkName.SelectedValue = ds.Tables[0].Rows[0]["Work_Name_ID"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["SubWork_ID"] != null)
                    {
                        ddlSubWorkName.SelectedValue = ds.Tables[0].Rows[0]["SubWork_ID"].ToString();
                    }
                    ddlLocation_SelectedIndexChanged(null,null);
                    ddlWorkName_SelectedIndexChanged(null, null);
                    ddlSubWorkName_SelectedIndexChanged(null, null);
                    btnSubmit.Text = "Update";
                    Div_GridDPR.Visible = true;
                }
            }
            catch (Exception ex)
            {
               SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void BindSubContractorList()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_CONTRACTOR_ALL, ref ds);
                ddlSubContractor.DataSource = ds;
                ddlSubContractor.DataTextField = "Subcon_name";
                ddlSubContractor.DataValueField = "Subcon_ID";
                ddlSubContractor.DataBind();
                ddlSubContractor.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        
          
        protected void ddlDiscriptionOf_Work_Changed(object sender, EventArgs e)
        {
            try
            {
                ds = new DataSet();
                txtPresent_Progress.Text = "0";
                objSubContBL = new SubContractorBillBL();  objSubContBL.ID =Convert.ToInt32(ddlDiscriptionOf_Work.SelectedValue);
                objSubContBL.Task = "Get_RC_UOM";
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_DISCRIPTIONOFWORK, ref ds);
                ddlUOM.SelectedValue = ds.Tables[0].Rows[0]["UOM"].ToString();
                ModalDPRItem.Show();
                if (ddlDiscriptionOf_Work.SelectedValue!="-Select-")
                {
                    objSubContBL.Discription_of_Work = ddlDiscriptionOf_Work.SelectedItem.Text;
                    objSubContBL.Task = "Get_Cumulative_ForDPR";
                    objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_DISCRIPTIONOFWORK, ref ds);
                    if (ds.Tables[0].Rows[0]["Present_Progress"].ToString() != "" && ds.Tables[0].Rows[0]["Present_Progress"].ToString() != null)
                    {
                        //foreach (DataRow row in ds.Tables[0].Rows)
                        //{
                        //    decimal rowvalue = Convert.ToDecimal(row["Present_Progress"]);
                        //    Cumulative_Progress += Convert.ToDecimal(rowvalue);
                        //}

                        hdntxtCumulativeProgress.Value = ds.Tables[0].Rows[0]["Present_Progress"].ToString();
                    }
                    else
                    {
                        hdntxtCumulativeProgress.Value = "0";
                    }
                    
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void ddlSubContractor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSubContractor.SelectedIndex != 0)
                {
                    BindWoNumber();
                }
                else
                {
                    //ddlWONo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void BindProject()
        {
            try
            {
                 objProjectBL = new ProjectBL();
                ds = new DataSet();
                objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_ALL, ref ds);
                ddlProject.DataSource = ds;
                ddlProject.DataTextField = "Project_Name";
                ddlProject.DataValueField = "Project_Code";
                ddlProject.DataBind();
                ddlProject.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void BindDiscriptionOfWork()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();
                objSubContBL.Project_Code = Session["Project_Code"].ToString();
                objSubContBL.WONo = ddlWO.SelectedItem.Text;
                objSubContBL.Task = "Get_Discription_Of_Work_RC";
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_DISCRIPTIONOFWORK, ref ds);
                ddlDiscriptionOf_Work.DataSource = ds;
                ddlDiscriptionOf_Work.DataTextField = "Discription_Of_Work";
                ddlDiscriptionOf_Work.DataValueField = "ID";
                ddlDiscriptionOf_Work.DataBind();
                ddlDiscriptionOf_Work.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void BindUOM()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_UOM_ALL, ref ds);
                ddlUOM.DataSource = ds;
                ddlUOM.DataTextField = "UOM";
                ddlUOM.DataValueField = "UOM_ID";
                ddlUOM.DataBind();
                ddlUOM.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void BindWoNumber()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();
                
                objSubContBL.Task = "Get_WO_Number";
                objSubContBL.SubContractorID =Convert.ToString(ddlSubContractor.SelectedValue.ToString());
                objSubContBL.Project_Code = ddlProject.SelectedValue.ToString();
                objSubContBL.WONo = ddlProject.SelectedValue.ToString();
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_WO_NUMBER, ref ds);
                ddlWO.DataSource = ds;
                ddlWO.DataTextField = "WONo";
                ddlWO.DataValueField = "WO_ID";
                ddlWO.DataBind();
                ddlWO.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void BindGrid_DPR_Entry(string DPR_no)
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();

                objSubContBL.Task = "Get_Grid_DPR_Entry";
                objSubContBL.DPR_No = DPR_no.ToString();
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SUB_CONTRACTOR_BILL_OPERATIONS, ref ds);
                Grid_DPR_Entry.DataSource = ds;
                Grid_DPR_Entry.DataBind();
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                objSubContBL = new SubContractorBillBL();
                objSubContBL.DPR_No = txtDPRNo.Text;
                objSubContBL.Project_Code = ddlProject.SelectedValue;
                objSubContBL.SubContractorID = ddlSubContractor.SelectedValue;
                if (ddlWO.SelectedItem.Text != "-Select-")
                {
                    objSubContBL.WONo = ddlWO.SelectedItem.Text;
                }
                
               
                if (ddlLocation.SelectedItem.Text!="-Select-")
                {
                    objSubContBL.Location_ID = Convert.ToInt32(ddlLocation.SelectedValue);
                }
                if (ddlWorkName.SelectedItem.Text != "-Select-")
                {
                    objSubContBL.Work_Name_ID = Convert.ToInt32(ddlWorkName.SelectedValue);
                }
                if (ddlSubWorkName.SelectedItem.Text != "-Select-")
                {
                    objSubContBL.SubWork_ID = Convert.ToInt32(ddlSubWorkName.SelectedValue);
                }
                objSubContBL.Work_Description = txtWorkDescription.Text;
                objSubContBL.Financial_Year = ddlFYear.SelectedValue;
                objSubContBL.Task = "InsertDPR";

                if (btnSubmit.Text == "Submit")
                {
                   
                        if (objSubContBL.insert(con, SubContractorBillBL.eLoadSp.INSERT))
                        {
                            Div_GridDPR.Visible = true;
                             btnSubmit.Text = "Update";
                             btnAddItem.Visible = true;
                             BindGrid_DPR_Entry(objSubContBL.DPR_No.ToString());
                             GetDPRDetails(objSubContBL.DPR_No.ToString());
                             BindDiscriptionOfWork();
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('DPR details has been inserted sucessfully.');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to insert DPR details !.');", true);
                        }
                    
                }
                else
                {
                    objSubContBL.Task = "UpdateDPR";
                    objSubContBL.DPR_No = txtDPRNo.Text.ToString();
                    if (objSubContBL.DPR_UPDATE(con, SubContractorBillBL.eLoadSp.UPDATE_DPR))
                    {

                        Div_GridDPR.Visible = true;
                        btnSubmit.Text = "Update";
                        btnAddItem.Visible = true;
                        BindGrid_DPR_Entry(objSubContBL.DPR_No.ToString());
                        GetDPRDetails(objSubContBL.DPR_No.ToString());
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('DPR details has been updated sucessfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update DPR details !.');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        public void ClearEntry()
        {
                
                 txtDPRDate.Text = string.Empty;
                txtLocation_Chainage.Text = string.Empty;
                txtWork_Done_Activity.Text = string.Empty;
                ddlDiscriptionOf_Work.SelectedIndex = 0;
                txtPresent_Progress.Text = string.Empty;
                txtCumulativeProgress.Text = string.Empty;
                ddlUOM.SelectedIndex = 0;
                txtRemarks.Text = string.Empty;
            ddlNMR.SelectedIndex = 0;
            txtNoMen.Text = string.Empty;
            txtNoWomen.Text = string.Empty;
            txtNoHelper.Text = string.Empty;
            No_Men_NMR.Text = string.Empty;
            No_Women_NMR.Text = string.Empty;
            No_Helper_NMR.Text = string.Empty;

        }
        protected void btnSaveDPR_Entry_Click(object sender, EventArgs e)
        {
            try
            {
                objSubContBL = new SubContractorBillBL();
                objSubContBL.DPR_No = txtDPRNo.Text;
                objSubContBL.Location_Chainage = txtLocation_Chainage.Text;
                objSubContBL.Work_Done_Activity = txtWork_Done_Activity.Text;
                objSubContBL.Work_Description = ddlDiscriptionOf_Work.SelectedValue;
                objSubContBL.Present_Progress =Convert.ToDecimal(txtPresent_Progress.Text);
                objSubContBL.Cumulative_Progress = Convert.ToDecimal(txtCumulativeProgress.Text);
                objSubContBL.UOM = ddlUOM.SelectedItem.Text;
                objSubContBL.Date =Convert.ToDateTime(txtDPRDate.Text);
                objSubContBL.Remarks = txtRemarks.Text;
                objSubContBL.DPRFile_Path = fuDPRDoc.FileName;
                if (ddlNMR.SelectedIndex != 0)
                {
                    objSubContBL.NMR_ID = Convert.ToInt32(ddlNMR.SelectedIndex);
                    objSubContBL.No_Men = Convert.ToDecimal(txtNoMen.Text);
                    objSubContBL.No_Women = Convert.ToDecimal(txtNoWomen.Text);
                    objSubContBL.No_Heplers = Convert.ToDecimal(txtNoHelper.Text);
                }
                    if (hdnDPR_ID.Value!="")
                {
                    objSubContBL.ID =Convert.ToInt32(hdnDPR_ID.Value);
                }
                if (btnSaveDPR_Entry.Text == "Save")
                {
                    objSubContBL.Task = "Insert_DPR_Entry";
                    if (objSubContBL.insert_DPR_Entry(con, SubContractorBillBL.eLoadSp.INSERT_DPR_ENTRY))
                    {
                        ClearEntry();
                        BindGrid_DPR_Entry(txtDPRNo.Text.ToString());
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('DPR Entry has been added successfully');", true);
                    }
                    else
                    {
                        ClearEntry();
                        ModalDPRItem.Show();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Item already exists for this DPR !!!');", true);
                    }

                }
                else
                {
                    objSubContBL.Task = "Update_DPR_Entry";
                    if (objSubContBL.insert_DPR_Entry(con, SubContractorBillBL.eLoadSp.INSERT_DPR_ENTRY))
                    {
                        ClearEntry();
                        BindGrid_DPR_Entry(txtDPRNo.Text.ToString());
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Item has been updated successfully');", true);
                       
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update item !!!');", true);
                    }
                    
                }
            }
            catch (Exception ex)
            {
              SNC.ErrorLogger. ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            ModalDPRItem.Show();
        }
        protected void Get_WO_Details()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();

                objSubContBL.Task = "Get_WO_Details";
                objSubContBL.WONo = Convert.ToString(ddlWO.SelectedItem.Text.ToString());
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_WO_NUMBER, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtWorkDescription.Text = ds.Tables[0].Rows[0]["Item_Desc"].ToString();
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void ddlWO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSubContractor.SelectedIndex != 0)
                {
                    Get_WO_Details();
                    BindDiscriptionOfWork();
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
        protected void btnCancelDPR_Entry_Click(object sender, EventArgs e)
        {
            try
            {
                txtDPRDate.Text = string.Empty;
                txtLocation_Chainage.Text = string.Empty;
                txtWork_Done_Activity.Text = string.Empty;
                ddlDiscriptionOf_Work.SelectedIndex = 0;
                txtPresent_Progress.Text = string.Empty;
                txtCumulativeProgress.Text = string.Empty;
                ddlUOM.SelectedIndex = 0;
                txtRemarks.Text = string.Empty;
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void Grid_DPR_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void Grid_Discription_Of_Work_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            try
            {
                objSubContBL = new SubContractorBillBL();
                objSubContBL.Work_DescriptionID = Convert.ToInt32(e.Record["ID"].ToString());
                objSubContBL.Task = "Delete_Work_Description";
                if (objSubContBL.delete(con, SubContractorBillBL.eLoadSp.DELETE_WORK_DESCRIPTION))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Discription Of Work has been deleted sucessfully.');", true);
                    BindDiscriptionOfWork();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to Delete !');", true);
                }
                ModelLedgerPopup.Show();
            }
            catch (Exception ex)
            {
               SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void btnSaveDiscription_Of_Work_Click(object sender, EventArgs e)
        {
            try
            {
                objSubContBL = new SubContractorBillBL();
                objSubContBL.Work_Description = txtDiscription_Of_Work.Text.Trim();
                objSubContBL.Project_Code = ddlProject.SelectedValue;
                if (objSubContBL.insert_Discription_Of_Work(con, SubContractorBillBL.eLoadSp.INSERT_WORK_DESCRIPTION))
                {
                    BindDiscriptionOfWork();
                    txtDiscription_Of_Work.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Discription Of Work has been added successfully.');", true);

                    ModelLedgerPopup.Show();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Discription Of Work already exists.');", true);
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void btnCancelDiscription_Of_Work_Click(object sender, EventArgs e)
        {
            txtDiscription_Of_Work.Text = string.Empty;
            ModelLedgerPopup.Hide();
        }
        
             protected void Grid_DirectPOItem_DeleteCommand(object sender, EventArgs e)
        {
        }
            protected void lnkDPRItem_Click(object sender, EventArgs e)
        {
            try
            {
                objSubContBL = new SubContractorBillBL();
                ds = new DataSet();
                objSubContBL.ID = Convert.ToInt32(((LinkButton)sender).Text);
                hdnDPR_ID.Value =Convert.ToString(((LinkButton)sender).Text);
                //Session["ID"] = Convert.ToString(((LinkButton)sender).CommandName);
                objSubContBL.Task = "Get_By_DPR_No";
                if (objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_DPRREPORT_BY_DPR_No, ref ds))
                {
                    
                    txtDPRDate.Text = ds.Tables[0].Rows[0]["Date"].ToString();
                    txtLocation_Chainage.Text = ds.Tables[0].Rows[0]["Location_Chainage"].ToString();
                    txtWork_Done_Activity.Text = ds.Tables[0].Rows[0]["Work_Done_Activity"].ToString();
                    ddlDiscriptionOf_Work.SelectedValue= ds.Tables[0].Rows[0]["Work_Description"].ToString();
                    txtPresent_Progress.Text = ds.Tables[0].Rows[0]["Present_Progress"].ToString();
                    txtCumulativeProgress.Text = ds.Tables[0].Rows[0]["Cumulative_Progress"].ToString();
                    ddlUOM.SelectedItem.Text = ds.Tables[0].Rows[0]["UOM"].ToString();
                    txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                    ddlNMR.SelectedItem.Text = ds.Tables[0].Rows[0]["NMR_ID"].ToString();
                    txtNoMen.Text = ds.Tables[0].Rows[0]["No_Men"].ToString();
                    txtNoWomen.Text = ds.Tables[0].Rows[0]["No_Women"].ToString();
                    txtNoHelper.Text = ds.Tables[0].Rows[0]["No_Heplers"].ToString();
                    ModalDPRItem.Show();
                    btnSaveDPR_Entry.Text = "Update";
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void ddlNMR_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlNMR.SelectedIndex != 0)
                {
                    Worker_Div.Visible = true;
                    ds = new DataSet();
                    objSubContBL = new SubContractorBillBL();
                    objSubContBL.Task = "Get_NMR_BYID";
                    objSubContBL.ID = Convert.ToInt32(ddlNMR.SelectedValue);
                    objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_NMR_BYID, ref ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        No_Men_NMR.Text = ds.Tables[0].Rows[0]["NoOf_Men"].ToString();
                        No_Women_NMR.Text = ds.Tables[0].Rows[0]["NoOf_Women"].ToString();
                        No_Helper_NMR.Text = ds.Tables[0].Rows[0]["NoOf_Helpers"].ToString();
                    }
                    ModalDPRItem.Show();
                }
                else
                {
                    Worker_Div.Visible = false;
                    ModalDPRItem.Show();
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void BindNMRNumber()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();
                objSubContBL.Task = "Get_NMR_LIST";
                objSubContBL.Project_Code = Session["Project_Code"].ToString();
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_NMR, ref ds);
                ddlNMR.DataSource = ds;
                ddlNMR.DataTextField = "NMR_No";
                ddlNMR.DataValueField = "ID";
                ddlNMR.DataBind();
                ddlNMR.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
        protected void btnAddSOW_Click(object sender, EventArgs e)
        {

            ModalSOWItem.Show();
        }
        protected void btnAssignSC_Click(object sender, EventArgs e)
        {

            ModalPopupAssignSC.Show();
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
                    //txtSubWorkName.Text = string.Empty;

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
        protected void Get_All_Consumed_Material_List()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();

                if (Session["Project_Code"] != null)
                {
                    objWorkLocationBL.SubWork_ID = Convert.ToInt32(ddlSubWorkName.SelectedValue);
                    objWorkLocationBL.Task = "Select_Consumed_Material_By_SubWork_ID";
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
        protected void Get_All_UtilizedMachinery_List()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();

                if (Session["Project_Code"] != null)
                {
                    objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                    objWorkLocationBL.Task = "Select_All_Utilized_Machinary_DPR";
                    objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_MATERIAL_ALL, ref ds);
                    GridUtilizedMachinery.DataSource = ds;
                    GridUtilizedMachinery.DataBind();
                }
                else
                {
                    GridUtilizedMachinery.DataSource = null;
                    GridUtilizedMachinery.DataBind();
                }

            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }
        protected void Get_All_Utilized_Labours_List()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();

                if (Session["Project_Code"] != null)
                {
                    objWorkLocationBL.SubWork_ID = Convert.ToInt32(ddlSubWorkName.SelectedValue);
                    objWorkLocationBL.Task = "Select_UtilizedLabour";
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
        protected void ddlSubWorkName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
               
                if (ddlSubWorkName.SelectedItem.Text != "-Select-")
                {
                    BindAssetRegistrationList();
                    Get_All_Consumed_Material_List();
                    Bind_Material_All();
                    Bind_Utilized_Labour_All();
                    Get_All_UtilizedMachinery_List();
                    Get_All_Utilized_Labours_List();
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
        protected void BindWorkName()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();               
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
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
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

        protected void BindAssetRegistrationList()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
                if (ddlSubWorkName.SelectedItem.Text!="Select")
                {
                    objWorkLocationBL.SubWork_ID =Convert.ToInt32(ddlSubWorkName.SelectedValue);
                }
                objWorkLocationBL.Task = "Select_All_Utilized_Machinary";
                objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                ddlMachinary_Name.DataSource = ds;
                ddlMachinary_Name.DataTextField = "Name";
                ddlMachinary_Name.DataValueField = "Code";
                ddlMachinary_Name.DataBind();

                ddlMachinary_Name.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {

            }
        }
       
      
        protected void btnSaveUtilizedMachinary_Click(object sender, EventArgs e)
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                objWorkLocationBL.Task = "Insert_SubWorkUtilized_Machinary";
                objWorkLocationBL.Machinary_Name = Convert.ToString(ddlMachinary_Name.SelectedItem.Text);
                //objWorkLocationBL.SiteImage = Convert.ToInt32(ddlSubWorkName.SelectedValue);
                objWorkLocationBL.Remarks = Convert.ToString(txtRemarksUsedMat.Text);
                objWorkLocationBL.Issued_Diesel = Convert.ToDecimal(txtissuedDiesel.Text);
                objWorkLocationBL.OutPut = Convert.ToDecimal(txtOutput.Text);
                objWorkLocationBL.UOM = Convert.ToString(ddlUOMUsedMat.SelectedValue);
                objWorkLocationBL.EndKM = Convert.ToDecimal(txtEndKM.Text);
                objWorkLocationBL.StartKM = Convert.ToDecimal(txtStartKm.Text);
                objWorkLocationBL.Unit = Convert.ToString(ddlUnit.SelectedValue);
                objWorkLocationBL.Reg_Number = Convert.ToString(txtRegNumber.Text);
                objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                objWorkLocationBL.UtilizedMachinaryDate = Convert.ToDateTime(txtUtilizedMachinaryDate.Text);
                if (fup_UtilizedMachinary_File.HasFile)
                {
                    objWorkLocationBL.UtilizedMachinary_File = "UtilizedMachinary_Files"+ DateTime.Now + "_" + fup_UtilizedMachinary_File.FileName.ToString() + System.IO.Path.GetExtension(fup_UtilizedMachinary_File.FileName);
                }
                if (fup_UtilizedMachinary_File.HasFile)
                {
                    fup_UtilizedMachinary_File.SaveAs(Server.MapPath("~\\UploadedFiles\\" +"UtilizedMachinary_Files"+ DateTime.Now + "_" + fup_UtilizedMachinary_File.FileName.ToString() + System.IO.Path.GetExtension(fup_UtilizedMachinary_File.FileName)));
                }
                if (objWorkLocationBL.Utilized_Machinaryinsert(con, WorkLocationBL.eLoadSp.INSERT_WORK_LOCATION))
                {
                    Get_All_UtilizedMachinery_List();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Required Machinery details has been Created');", true);
                    //txtSubWorkName.Text = string.Empty;

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
        protected void btnSaveRequired_Material_Click(object sender, EventArgs e)
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                objWorkLocationBL.Task = "Insert_ConsumedMaterial";
                objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                objWorkLocationBL.SubWork_ID = Convert.ToInt32(ddlSubWorkName.SelectedValue);
                objWorkLocationBL.Consumed_Material_UOM = ddlRequiredMaterialUOM.SelectedValue;
                objWorkLocationBL.Consumed_Material_Quantity = Convert.ToDecimal(txtRequired_MaterialQuantity.Text.Trim());
                objWorkLocationBL.Consumed_Material_Name = Convert.ToString(ddlConsumedMaterial.SelectedItem.Text);
                objWorkLocationBL.Consumed_Material_ID = Convert.ToInt32(ddlConsumedMaterial.SelectedValue);
                objWorkLocationBL.ConsumedMaterialDate = Convert.ToDateTime(txtConsumedMaterialDate.Text);
                if (objWorkLocationBL.Consumed_Material_Insert(con, WorkLocationBL.eLoadSp.INSERT_WORK_LOCATION))
                {
                    Get_All_Consumed_Material_List();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Consumed Material details has been Created');", true);


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to add the Consumed Material');", true);
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void ddlRequiredMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
                if (Session["Project_Code"] != null)
                {
                    objWorkLocationBL.ID = Convert.ToInt32(ddlConsumedMaterial.SelectedValue);
                    objWorkLocationBL.Task = "SelectRequired_Material_Details_ByID";
                    objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                    txtRequired_MaterialQuantity.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
                    ddlRequiredMaterialUOM.SelectedItem.Text = ds.Tables[0].Rows[0]["UOM"].ToString();
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
        protected void ddlWorkName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
                
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
        protected void BindRequiredMaterialDetailsC()
        {
            objWorkLocationBL = new WorkLocationBL();
            ds = new DataSet();
            if (Session["Project_Code"] != null)
            {
                objWorkLocationBL.SubWork_ID = Convert.ToInt32(ddlSubWorkName.SelectedValue);
                objWorkLocationBL.Task = "SelectRequired_Material_Details";
                objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                ddlConsumedMaterial.DataSource = ds;
                ddlConsumedMaterial.DataTextField = "Material_Name";
                ddlConsumedMaterial.DataValueField = "ID";
                ddlConsumedMaterial.DataBind();
                ddlConsumedMaterial.Items.Insert(0, "-Select-");

            }
            else
            {
                ddlConsumedMaterial.DataSource = null;
                ddlConsumedMaterial.DataBind();
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
                ddlUOM.DataBind();
                ddlUOM.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {

            }
        }
        protected void BindUOMListMaterialUOM()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
                objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_UOM_ALL, ref ds);
                ddlRequiredMaterialUOM.DataSource = ds;
                ddlRequiredMaterialUOM.DataValueField = "UOM_ID";
                ddlRequiredMaterialUOM.DataTextField = "UOMPrefix";
                ddlRequiredMaterialUOM.DataBind();
                ddlRequiredMaterialUOM.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {

            }
        }
        protected void BindUOMListUsedMat()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
                objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_UOM_ALL, ref ds);
                ddlUOMUsedMat.DataSource = ds;
                ddlUOMUsedMat.DataValueField = "UOM_ID";
                ddlUOMUsedMat.DataTextField = "UOMPrefix";
                ddlUOMUsedMat.DataBind();
                ddlUOMUsedMat.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {

            }
        }
        protected void ddlMachinary_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            objWorkLocationBL = new WorkLocationBL();
            ds = new DataSet();
            if (ddlMachinary_Name.SelectedItem.Text != "Select")
            {
                objWorkLocationBL.Code = Convert.ToString(ddlMachinary_Name.SelectedValue);
            }
            objWorkLocationBL.Task = "Select_All_Utilized_Machinary_BYID";
            objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
            if (ds.Tables[0].Rows[0]["Unit"].ToString()!="")
            {
                ddlUnit.SelectedItem.Text = ds.Tables[0].Rows[0]["Unit"].ToString();
            }
            else
            {
                ddlUnit.SelectedItem.Text = "-Select-";
            }
            txtRegNumber.Text = ds.Tables[0].Rows[0]["Reg_No"].ToString();
             BindUOMListUsedMat();
            ModalSOWItem.Show();
           
        }

        protected void Bind_Material_All()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
                if (Session["Project_Code"] != null)
                {
                    if (ddlSubWorkName.SelectedItem.Text!="-Select-")
                    {
                        objWorkLocationBL.SubWork_ID = Convert.ToInt32(ddlSubWorkName.SelectedValue);
                    }
                    objWorkLocationBL.Task = "Get_Material_All_SubWork_Assinged";
                    objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                    ddlConsumedMaterial.DataSource = ds;
                    ddlConsumedMaterial.DataTextField = "Material_Name";
                    ddlConsumedMaterial.DataValueField = "ID";
                    ddlConsumedMaterial.DataBind();

                    ddlConsumedMaterial.Items.Insert(0, "-Select-");
                }
                else
                {
                    ddlConsumedMaterial.DataSource = null;
                    ddlConsumedMaterial.DataBind();
                }

            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }

        protected void ddlConsumedMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
                if (Session["Project_Code"] != null)
                {
                    if (ddlConsumedMaterial.SelectedItem.Text != "-Select-")
                    {
                        objWorkLocationBL.ID = Convert.ToInt32(ddlConsumedMaterial.SelectedValue);
                    }
                    objWorkLocationBL.Task = "Get_Material_SubWork_Assinged_BY_ID";
                    objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                    ddlConsumedMaterial.SelectedItem.Text = ds.Tables[0].Rows[0]["Material_Name"].ToString();
                    txtRequired_MaterialQuantity.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
                    ddlRequiredMaterialUOM.SelectedItem.Text = ds.Tables[0].Rows[0]["UOM"].ToString();
                }
                   

            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
            ModalPopupConsumedMaterial.Show();
        }

        protected void btnSaveUtilized_Labours_Click(object sender, EventArgs e)
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                objWorkLocationBL.Task = "Insert_UtilizedLabour";
                objWorkLocationBL.Project_Code = Session["Project_Code"].ToString();
                objWorkLocationBL.SubWork_ID = Convert.ToInt32(ddlSubWorkName.SelectedValue);
                objWorkLocationBL.UtilizedLabour_UOM = ddlLaboursUOM.SelectedItem.Text;
                objWorkLocationBL.UtilizedLabour_Quantity = Convert.ToDecimal(txtLaboursQuantity.Text.Trim());
                objWorkLocationBL.UtilizedLabour_LabourType = Convert.ToString(ddlLabour.SelectedItem.Text);
                objWorkLocationBL.UtilizedLabourDate =Convert.ToDateTime(txtUtilizedLabourDate.Text);
                if (objWorkLocationBL.Utilized_Labours_Insert(con, WorkLocationBL.eLoadSp.INSERT_WORK_LOCATION))
                {
                    Get_All_Utilized_Labours_List();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Utilized Labour details has been Created');", true);


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to add the Utilized Labour');", true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCancelUtilizedLabours_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {

            }
        }

        protected void Bind_Utilized_Labour_All()
        {
            try
            {
                objWorkLocationBL = new WorkLocationBL();
                ds = new DataSet();
                if (Session["Project_Code"] != null)
                {
                    if (ddlSubWorkName.SelectedItem.Text != "-Select-")
                    {
                        objWorkLocationBL.SubWork_ID = Convert.ToInt32(ddlSubWorkName.SelectedValue);
                    }
                    objWorkLocationBL.Task = "Get_Utilized_Labour_All";
                    objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
                    ddlLabour.DataSource = ds;
                    ddlLabour.DataTextField = "Type_Of_Labour";
                    ddlLabour.DataValueField = "ID";
                    ddlLabour.DataBind();

                    ddlLabour.Items.Insert(0, "-Select-");
                }
                else
                {
                    ddlLabour.DataSource = null;
                    ddlLabour.DataBind();
                }

            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }
        }
        protected void ddlLabour_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            objWorkLocationBL = new WorkLocationBL();
            ds = new DataSet();
            if (ddlLabour.SelectedItem.Text != "Select")
            {
                objWorkLocationBL.ID = Convert.ToInt32(ddlLabour.SelectedValue);
            }
            objWorkLocationBL.Task = "Get_Utilized_Labour_By_ID";
            objWorkLocationBL.load(con, WorkLocationBL.eLoadSp.SELECT_WORKLOCATION_ALL, ref ds);
            if (ds.Tables[0].Rows[0]["UOM"].ToString() != "")
            {
                ddlLaboursUOM.SelectedItem.Text = ds.Tables[0].Rows[0]["UOM"].ToString();
            }
            else
            {
                ddlUnit.SelectedItem.Text = "-Select-";
            }
            txtLaboursQuantity.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
            ModalPopupUtilizedLabours.Show();
        }
    }
}