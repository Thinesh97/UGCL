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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace SNC.SubContractorBills
{
    public partial class NominalMasterRoll : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        SubContractorBillBL objSubContBL = null;
        ProjectBL objProjectBL = null;
        DataSet ds = null;
        DataSet Accessds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindProject();
                BindSubContractorList();
                BindGrid_Labour_Type();
                if (Request.QueryString["NMR_No"] != null)
                {
                    BindWoNumberAll();
                    GetNMRDetails(Request.QueryString["NMR_No"]);
                    BindGrid_Labour_List();
                    
                }
              
            }
        }
        protected void BindSubContractorList()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_CONTRACTOR_ALL, ref ds);
                ddlSubContractor_NMR.DataSource = ds;
                ddlSubContractor_NMR.DataTextField = "Subcon_name";
                ddlSubContractor_NMR.DataValueField = "Subcon_ID";
                ddlSubContractor_NMR.DataBind();
                ddlSubContractor_NMR.Items.Insert(0, "-Select-");
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
                if (ddlSubContractor_NMR.SelectedIndex != 0)
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
                ddlProject_NMR.DataSource = ds;
                ddlProject_NMR.DataTextField = "Project_Name";
                ddlProject_NMR.DataValueField = "Project_Code";
                ddlProject_NMR.DataBind();
                ddlProject_NMR.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        private void GetNMRDetails(string NMR_NoRet)
        {
            try
            {
                objSubContBL = new SubContractorBillBL();
                ds = new DataSet();
                objSubContBL.NMR_No = NMR_NoRet;
                objSubContBL.Task = "GetNMRDetails";
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SUB_CONTRACTOR_BILL_OPERATIONS, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtNMRID.Text = ds.Tables[0].Rows[0]["NMR_No"].ToString();
                    txtNMRBillDate.Text = ds.Tables[0].Rows[0]["BillDate_NMR"].ToString();
                    ddlProject_NMR.SelectedValue = ds.Tables[0].Rows[0]["Project_NMR"].ToString();
                    ddlWO_NMR.SelectedValue = ds.Tables[0].Rows[0]["WO_NMR"].ToString();
                    lblGridLable.InnerText = ddlWO_NMR.SelectedItem.Text;
                    ddlSubContractor_NMR.SelectedValue = ds.Tables[0].Rows[0]["SubContractor_NMR"].ToString();
                    txtWorkDescription_NMR.Text = ds.Tables[0].Rows[0]["WorkDescription_NMR"].ToString();
                    //txtNoOfMen.Text = ds.Tables[0].Rows[0]["NoOf_Men"].ToString();
                    //txtMenRate.Text = ds.Tables[0].Rows[0]["Men_Rate"].ToString();
                    //txtMenTotal.Text = ds.Tables[0].Rows[0]["Men_Total"].ToString();
                    //txtNoOfWomen.Text = ds.Tables[0].Rows[0]["NoOf_Women"].ToString();
                    //txtWomenRate.Text = ds.Tables[0].Rows[0]["Women_Rate"].ToString();
                    //txtWomenTotal.Text = ds.Tables[0].Rows[0]["Women_Total"].ToString();
                    //txtNoOfHelpers.Text = ds.Tables[0].Rows[0]["NoOf_Helpers"].ToString();
                    //txtHelpersRate.Text = ds.Tables[0].Rows[0]["Helpers_Rate"].ToString();
                    //txtHelpersTotal.Text = ds.Tables[0].Rows[0]["Helpers_Total"].ToString();
                    txtWorkDoneAt.Text = ds.Tables[0].Rows[0]["WorkDone_At"].ToString();
                    btnSubmit.Text = "Update";

                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void BindWoNumberAll()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();

                objSubContBL.Task = "Get_WO_Number_All";
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_WO_NUMBER, ref ds);
                ddlWO_NMR.DataSource = ds;
                ddlWO_NMR.DataTextField = "WONo";
                ddlWO_NMR.DataValueField = "WO_ID";
                ddlWO_NMR.DataBind();
                ddlWO_NMR.Items.Insert(0, "-Select-");
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
                objSubContBL.SubContractorID = Convert.ToString(ddlSubContractor_NMR.SelectedValue.ToString());
                objSubContBL.Project_Code = ddlProject_NMR.SelectedValue.ToString();
                objSubContBL.WONo = ddlProject_NMR.SelectedValue.ToString();
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_WO_NUMBER, ref ds);
                ddlWO_NMR.DataSource = ds;
                ddlWO_NMR.DataTextField = "WONo";
                ddlWO_NMR.DataValueField = "WO_ID";
                ddlWO_NMR.DataBind();
                ddlWO_NMR.Items.Insert(0, "-Select-");
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
                if (ddlSubContractor_NMR.SelectedIndex != 0)
                {
                    Get_WO_Details();
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
        protected void Get_WO_Details()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();

                objSubContBL.Task = "Get_WO_Details";
                objSubContBL.WONo = Convert.ToString(ddlWO_NMR.SelectedItem.Text.ToString());
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SELECT_WO_NUMBER, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtWorkDescription_NMR.Text = ds.Tables[0].Rows[0]["Item_Desc"].ToString();
                }
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        public void Clear()
        {
            txtNMRID.Text=string.Empty;
            txtNMRBillDate.Text=string.Empty;
            ddlProject_NMR.SelectedIndex = 0;
            ddlSubContractor_NMR.SelectedIndex = 0;
            ddlWO_NMR.SelectedIndex = 0;
            txtWorkDescription_NMR.Text=string.Empty;
            //txtNoOfMen.Text=string.Empty;
            //txtMenRate.Text=string.Empty;
            //txtMenTotal.Text=string.Empty;
            //txtNoOfWomen.Text=string.Empty;
            //txtWomenRate.Text=string.Empty;
            //txtWomenTotal.Text=string.Empty;
            //txtNoOfHelpers.Text=string.Empty;
            //txtHelpersRate.Text=string.Empty;
            //txtHelpersTotal.Text=string.Empty;
            txtWorkDoneAt.Text = string.Empty;
        }

        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                objSubContBL = new SubContractorBillBL();
                objSubContBL.NMR_No = txtNMRID.Text;
                objSubContBL.BillDate_NMR =Convert.ToDateTime(txtNMRBillDate.Text);
                objSubContBL.Project_NMR = ddlProject_NMR.SelectedValue;
                objSubContBL.SubContractor_NMR = ddlSubContractor_NMR.SelectedValue;
                objSubContBL.WO_NMR = ddlWO_NMR.SelectedItem.Text;
                objSubContBL.WorkDescription_NMR = txtWorkDescription_NMR.Text;
                objSubContBL.WorkDone_At = txtWorkDoneAt.Text;
                objSubContBL.Task = "InsertNMR";
                if (btnSubmit.Text == "Submit")
                {

                    if (objSubContBL.insertNMR(con, SubContractorBillBL.eLoadSp.INSERT_NMR))
                    {
                        //btnSubmit.Text = "Update";
                        GetNMRDetails(objSubContBL.NMR_No);
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('NMR details has been inserted sucessfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to insert NMR details !.');", true);
                    }

                }
                //else
                //{
                //    objWO.Task = "UpdateWO";
                //    objWO.WONo = txtWONo.Text.ToString();
                //    if (fuWODoc.HasFile)
                //    {
                //        objWO.Uploaded_File = "WO_" + txtWONo.Text.ToString() + ".pdf";
                //    }

                //    if (objWO.update(con, WorkOrderBL.eLoadSp.UPDATE))
                //    {
                //        if (fuWODoc.HasFile)
                //        {
                //            fuWODoc.SaveAs(Server.MapPath("~\\UploadedFiles\\WO_" + txtWONo.Text.Replace("/", "-") + ".pdf"));
                //            lnkDownloadFile.Text = "WO_" + txtWONo.Text + ".pdf";
                //            div_AfterUpload.Visible = true;
                //        }
                //        btnPrint.Visible = true;
                //        btnPrint.HRef = "WO_Print.aspx?WONo=" + txtWONo.Text.ToString();
                //        btnPrint.Target = "_blank";
                //        div_Draft.Visible = true;
                //        div_BeforeUpload.Visible = true;
                //        btnPrintPDF.Visible = false;
                //        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('WO details has been updated sucessfully.');", true);
                //    }
                //    else
                //    {
                //        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update WO details !.');", true);
                //    }
                //}
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

        protected void BindGrid_Labour_List()
        {
            try
            {
                ds = new DataSet();
                objSubContBL = new SubContractorBillBL();

                objSubContBL.Task = "SelectAll_Labour_By_NMR_ID";
                objSubContBL.NMR_No = Convert.ToString(txtNMRID.Text);
                objSubContBL.load(con, SubContractorBillBL.eLoadSp.SUB_CONTRACTOR_BILL_OPERATIONS, ref ds);
                Grid_LabourList.DataSource = ds;
                Grid_LabourList.DataBind();
            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }


        protected void Grid_LabourList_Delete_Click(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            try
            {
                objSubContBL = new SubContractorBillBL();
                objSubContBL.NMR_No = Convert.ToString(e.Record["NMR_No"]);
                objSubContBL.Task = "Delete_NMR";
                if (objSubContBL.delete(con, SubContractorBillBL.eLoadSp.SUB_CONTRACTOR_BILL_OPERATIONS))
                {
                    //BindGrid_NMR_Entry();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('NMR Item has been deleted sucessfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete NMR Item !');", true);
                }

            }
            catch (Exception ex)
            {
                SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void Grid_LabourList_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
        {
            //try
            //{
            //    if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            //    {
            //        HyperLink Editlik = e.Row.Cells[0].FindControl("lnk_NMR_No") as HyperLink;
            //        Editlik.NavigateUrl = "~/SubContractorBills/NominalMasterRoll.aspx?NMR_No=" + Editlik.Text;
            //        if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
            //        {
            //            if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
            //            {
            //                if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["WO_Update"].ToString()))
            //                {
            //                    Editlik.NavigateUrl = "";
            //                }
            //            }
            //        }
            //        //LinkButton lnkNMR_Delete = e.Row.Cells[8].FindControl("lnkNMR_Delete") as LinkButton;

            //    }
            //}
            //catch (Exception ex)
            //{
            //    SNC.ErrorLogger.ErrorLogger.logError(SNC.ErrorLogger.ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            //}
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

        public void ClearItemLbour()
        {
            txtrqRequiredDate.Text = string.Empty;
            txtRequiredLabour.Text = string.Empty;
            txtLabourRate.Text = string.Empty;
            txtTotalCostOfLabour.Text = string.Empty;
        }
        protected void btnSaveRequired_Labour_Click(object sender, EventArgs e)
        {
            try
            {
                objSubContBL = new SubContractorBillBL();
                objSubContBL.Project_Code = Convert.ToString(ddlProject_NMR.SelectedValue);
                objSubContBL.WO_NMR = Convert.ToString(ddlWO_NMR.SelectedItem.Text);
                objSubContBL.NMR_No = Convert.ToString(txtNMRID.Text);
                objSubContBL.Labour_Date = Convert.ToDateTime(txtrqRequiredDate.Text);
                objSubContBL.NoOf_Labour = Convert.ToDecimal(txtRequiredLabour.Text);
                objSubContBL.Labour_Rate = Convert.ToDecimal(txtLabourRate.Text);
                objSubContBL.Labour_Type_Labour = Convert.ToString(ddlLabour.SelectedValue);
                objSubContBL.LabourCost_Total = Convert.ToDecimal(txtTotalCostOfLabour.Text);
                objSubContBL.Task = "InsertTB_NMR_LABOUR";
                if (objSubContBL.InsertTB_NMR_LABOUR(con, SubContractorBillBL.eLoadSp.InsertTB_NMR_Labour))
                {
                    ClearItemLbour();
                    BindGrid_Labour_List();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Required Labour has been Created');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to add the Required Labour');", true);
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnCancelRequired_Labour_Click(object sender, EventArgs e)
        {
            
        }
        protected void btnCancelLabourType_Click(object sender, EventArgs e)
        {
            
        }
    }
}