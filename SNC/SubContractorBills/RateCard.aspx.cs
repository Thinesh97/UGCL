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
    public partial class RateCard : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        SubContractorBillBL objSubContBL = null;
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
                btnAddItem.Visible = true;
                Div_GridDPR.Visible = false;
                btnAddItem.Visible = false;
                if (Request.QueryString["RC_No"] != null)
                {
                    btnAddItem.Visible = true;
                    BindGrid_RC_Entry(Request.QueryString["RC_No"]);
                    GetRCDetails(Request.QueryString["RC_No"].ToString());
                }
            }
        }
        private void GetRCDetails(string RC_No)
        {
            try
            {
                objSubContBL = new SubContractorBillBL();
                ds = new DataSet();
                objSubContBL.RateCard_ID = RC_No;
                objSubContBL.Task = "GetRCDetails";
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SUB_CONTRACTOR_BILL_OPERATIONS, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtRCID.Text = ds.Tables[0].Rows[0]["RateCard_ID"].ToString();
                    txtRateCardDate.Text = ds.Tables[0].Rows[0]["RCDate"].ToString();
                    ddlProject.SelectedValue = ds.Tables[0].Rows[0]["Project_Code"].ToString();
                    ddlSubContractor.SelectedValue = ds.Tables[0].Rows[0]["SubContractorID"].ToString();
                    ddlWO.SelectedItem.Text = ds.Tables[0].Rows[0]["Work_OrderNO"].ToString();
                    txtWorkDescription.Text = ds.Tables[0].Rows[0]["WO_Description"].ToString();
                    ddlWoTypeRC.SelectedValue = ds.Tables[0].Rows[0]["WO_Type"].ToString();
                    ID_WOno.InnerText = ds.Tables[0].Rows[0]["Work_OrderNO"].ToString();
                    ddlFYear.SelectedValue = ds.Tables[0].Rows[0]["Financial_Year"].ToString();
                    if (ds.Tables[0].Rows[0]["FixedOrRecurring"].ToString()=="Fixed")
                    {
                        chkFixed.Checked = true;
                        chkRecurring.Checked = false;
                    }
                    else if (ds.Tables[0].Rows[0]["FixedOrRecurring"].ToString() == "Recurring")
                    {
                        chkFixed.Checked = false;
                        chkRecurring.Checked = true;
                        txtRCQuantity_Item.Text = "1";
                        txtRCQuantity_Item.Enabled = false;
                    }
                    
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
       
        protected void BindUOM()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_UOM_ALL, ref ds);
                ddlUMORC_Item.DataSource = ds;
                ddlUMORC_Item.DataTextField = "UOM";
                ddlUMORC_Item.DataValueField = "UOM_ID";
                ddlUMORC_Item.DataBind();
                ddlUMORC_Item.Items.Insert(0, "-Select-");
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

                objSubContBL.Task = "Get_WO_Number_RC";
                objSubContBL.SubContractorID = Convert.ToString(ddlSubContractor.SelectedValue.ToString());
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
        protected void BindGrid_RC_Entry(string RC_No)
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();

                objSubContBL.Task = "Get_Grid_RC_Entry";
                objSubContBL.RateCard_ID = RC_No;
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SUB_CONTRACTOR_BILL_OPERATIONS, ref ds);
                Grid_RC_Entry.DataSource = ds;
                Grid_RC_Entry.DataBind();
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
                objSubContBL.RateCard_ID = txtRCID.Text;
                objSubContBL.DateRC =Convert.ToDateTime(txtRateCardDate.Text);
                objSubContBL.SubContractorID = ddlSubContractor.SelectedValue;
                objSubContBL.Project_Code = ddlProject.SelectedValue;
                objSubContBL.Financial_Year = ddlFYear.SelectedValue;
                if (ddlWO.SelectedItem.Text != "-Select-")
                {
                    objSubContBL.WONo = ddlWO.SelectedItem.Text;
                }
                objSubContBL.Work_Description = txtWorkDescription.Text;
                objSubContBL.WO_Type_RC = ddlWoTypeRC.SelectedValue;
                
                        if (chkFixed.Checked == true )
                        {
                            objSubContBL.FixedOrRecurring = "Fixed";
                        }
                    else if(chkRecurring.Checked == true){
                        objSubContBL.FixedOrRecurring = "Recurring";
                    }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please Select Recurring or Fixed!.');", true);
                        }
		 
                
             
                objSubContBL.Task = "InsertRC";

                if (btnSubmit.Text == "Submit")
                {

                    if (objSubContBL.insertRC(con, SubContractorBillBL.eLoadSp.INSERT_RC))
                    {
                        Div_GridDPR.Visible = true;
                        btnSubmit.Text = "Update";
                        btnAddItem.Visible = true;
                        BindGrid_RC_Entry(objSubContBL.RateCard_ID.ToString());
                        GetRCDetails(objSubContBL.RateCard_ID.ToString());
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('DPR details has been inserted sucessfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to insert DPR details !.');", true);
                    }

                }
                else
                {
                    objSubContBL.Task = "UpdateRC";
                    if (objSubContBL.DPR_UPDATE(con, SubContractorBillBL.eLoadSp.UPDATE_DPR))
                    {

                        Div_GridDPR.Visible = true;
                        btnSubmit.Text = "Update";
                        btnAddItem.Visible = true;
                        BindGrid_RC_Entry(objSubContBL.RateCard_ID.ToString());
                        GetRCDetails(objSubContBL.RateCard_ID.ToString());
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

            txtDiscriptionOfWorkRC_Item.Text = string.Empty;
            txtDiscriptionOfWorkRC_Item.Text = string.Empty;
            ddlUMORC_Item.SelectedValue = null;
            txtRateRC_Item.Text = string.Empty;
            txtAmountRC_Item.Text = string.Empty;
        }
        protected void btnSaveRC_Entry_Click(object sender, EventArgs e)
        {
            try
            {
                objSubContBL = new SubContractorBillBL();
                objSubContBL.RateCard_ID = txtRCID.Text;
                objSubContBL.Discription_Of_WorkRCItem = txtDiscriptionOfWorkRC_Item.Text;
                objSubContBL.QuantityRCItem = Convert.ToDecimal(txtRCQuantity_Item.Text);
                objSubContBL.UOMRCItem=Convert.ToInt32(ddlUMORC_Item.SelectedValue);
                objSubContBL.RateRCItem=Convert.ToDecimal(txtRateRC_Item.Text);
                objSubContBL.AmountRCItem = Convert.ToDecimal(txtAmountRC_Item.Text);
                if (btnSaveRC_Entry.Text == "Save")
                {
                    objSubContBL.Task = "Insert_RC_Entry";
                    if (objSubContBL.insert_RC_Entry(con, SubContractorBillBL.eLoadSp.RATECARD_INSERT_UPDATE))
                    {
                        ClearEntry();
                        BindGrid_RC_Entry(txtRCID.Text.ToString());
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
                        //BindGrid_RC_Entry(txtDPRNo.Text.ToString());
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
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
                    ddlProject.SelectedValue = ds.Tables[0].Rows[0]["Project_Code"].ToString();
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
        protected void btnCancelRC_Entry_Click(object sender, EventArgs e)
        {
            try
            {
                //txtDPRDate.Text = string.Empty;
                //txtLocation_Chainage.Text = string.Empty;
                //txtWork_Done_Activity.Text = string.Empty;
                //ddlDiscriptionOf_Work.SelectedIndex = 0;
                //txtPresent_Progress.Text = string.Empty;
                //txtCumulativeProgress.Text = string.Empty;
                //ddlUOM.SelectedIndex = 0;
                //txtRemarks.Text = string.Empty;
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
        protected void Grid_RC_Item_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            try
            {
                objSubContBL = new SubContractorBillBL();
                objSubContBL.ID = Convert.ToInt32(e.Record["ID"].ToString());
                objSubContBL.Task = "Delete_RC_Item";
                if (objSubContBL.delete(con, SubContractorBillBL.eLoadSp.SUB_CONTRACTOR_BILL_OPERATIONS))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Rate Card Of Work Order has been deleted sucessfully.');", true);
                    BindGrid_RC_Entry(txtRCID.Text);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to Delete !');", true);
                }
                
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
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
                //hdnDPR_ID.Value = Convert.ToString(((LinkButton)sender).Text);
                //Session["ID"] = Convert.ToString(((LinkButton)sender).CommandName);
                objSubContBL.Task = "Get_By_RC_Item_No";
                if (objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_DPRREPORT_BY_DPR_No, ref ds))
                {

                    txtDiscriptionOfWorkRC_Item.Text = ds.Tables[0].Rows[0]["Discription_Of_Work"].ToString();
                    txtRCQuantity_Item.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
                    ddlUMORC_Item.SelectedValue = ds.Tables[0].Rows[0]["UOM"].ToString();
                    txtAmountRC_Item.Text = ds.Tables[0].Rows[0]["Amount"].ToString();
                    txtRateRC_Item.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
                    ModalDPRItem.Show();
                    btnSaveRC_Entry.Text = "Update";
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
                //if (ddlNMR.SelectedIndex != 0)
                //{
                //    Worker_Div.Visible = true;
                //    ds = new DataSet();
                //    objSubContBL = new SubContractorBillBL();
                //    objSubContBL.Task = "Get_NMR_BYID";
                //    objSubContBL.ID = Convert.ToInt32(ddlNMR.SelectedValue);
                //    objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_NMR_BYID, ref ds);
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        No_Men_NMR.Text = ds.Tables[0].Rows[0]["NoOf_Men"].ToString();
                //        No_Women_NMR.Text = ds.Tables[0].Rows[0]["NoOf_Women"].ToString();
                //        No_Helper_NMR.Text = ds.Tables[0].Rows[0]["NoOf_Helpers"].ToString();
                //    }
                //    ModalDPRItem.Show();
                //}
                //else
                //{
                //    Worker_Div.Visible = false;
                //    ModalDPRItem.Show();
                //}
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
                //ddlNMR.DataSource = ds;
                //ddlNMR.DataTextField = "NMR_No";
                //ddlNMR.DataValueField = "ID";
                //ddlNMR.DataBind();
                //ddlNMR.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

    }
}