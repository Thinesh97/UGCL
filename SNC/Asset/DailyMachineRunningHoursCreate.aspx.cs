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

public partial class DailyMachineRunningHoursCreate : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    DailyRunningHourBL objDailyRunningHourBL = null;
    AssetRegistrationBL objAsset = null;
    DataSet ds = null;
    DataSet ds_DEDUCT = new DataSet();
    ProjectBL objProjectBL = null;
    decimal TotalIssuedQty = Convert.ToDecimal("0.00");
    string fileNamewithName;
    string uniqueDateTime = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss-fff_");
    decimal AvailiableQty=0;
    int Diesel=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    BindProjectList();
                    //GetAvailableDieselQtyFromStock();
                    gvDailyRunningHours.DataSource = null;
                    gvDailyRunningHours.DataBind();
                }
                else
                {
                    Response.Redirect("../CommonPages/Login.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindProjectList()
    {
        try
        {
            objProjectBL = new ProjectBL();
            ds = new DataSet();
            if (Session["Project_Code"] != null)
            {
                objProjectBL.Project_Code = Session["Project_Code"].ToString();

                objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_BY_Project_Code, ref ds);
                ddlProjectNames.DataTextField = "Project_Name";
                ddlProjectNames.DataValueField = "Project_Code";
                ddlProjectNames.DataSource = ds;
                ddlProjectNames.DataBind();
                ddlProjectNames.Enabled = false;
                ddlProjectNames.Items.Insert(0, "-Select-");
                ddlProjectNames.SelectedValue = Session["Project_Code"].ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void GetAvailableDieselQtyFromStock()
    {
        try
        {
            ds = new DataSet();
            objDailyRunningHourBL = new DailyRunningHourBL();
            objDailyRunningHourBL.Project_Code = ddlProjectNames.SelectedValue;
            objDailyRunningHourBL.Date = Convert.ToDateTime(txtDate.Text);
            objDailyRunningHourBL.load(con, DailyRunningHourBL.eLoadSp.SELECT_AVL_DIESEL_QTY_FROM_STOCK, ref ds);
            if (ds.Tables.Count > 0)
            {
                //var Avl_Qty = Convert.ToDecimal(ds.Tables[0].Rows[0]["Avl_Qty"].ToString());
                //var Adjust_QTY = Convert.ToDecimal(ds.Tables[0].Rows[0]["Adjust_QTY"].ToString());
                //var AvaliableQty =Convert.ToDecimal(Avl_Qty - Adjust_QTY);
                //var DaillyRunningUsed = Convert.ToDecimal(ds.Tables[0].Rows[1]["DaillyRunningUsed"].ToString());
                txtIssuedDieselQty.Text = Convert.ToString(ds.Tables[0].Rows[0]["AvlDieselQty"].ToString());
            }
            else
            {
                txtIssuedDieselQty.Text = "0.00";
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvDailyRunningHours.Rows.Count; i++)
            {
                TextBox txttxtIssuedDiesel = (TextBox)gvDailyRunningHours.Rows[i].FindControl("txtIssuedDiesel");
                if (txttxtIssuedDiesel != null)
                {
                    TotalIssuedQty = TotalIssuedQty + Convert.ToDecimal(txttxtIssuedDiesel.Text);
                }
            }
            if (TotalIssuedQty > Convert.ToDecimal(txtIssuedDieselQty.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Issued Diesel Qty is greater than Available Diesel Qty');", true);
                return;
            }

            for (int i = 0; i < gvDailyRunningHours.Rows.Count; i++)
            {
                Label lblUnit = (Label)gvDailyRunningHours.Rows[i].FindControl("lblUnit");
                Label lbllblCode = (Label)gvDailyRunningHours.Rows[i].FindControl("lblCode");
                TextBox txttxtStartKM = (TextBox)gvDailyRunningHours.Rows[i].FindControl("txtStartKM");

                TextBox txttxtEndKM = (TextBox)gvDailyRunningHours.Rows[i].FindControl("txtEndKM");
                DropDownList ddlddlUOM = (DropDownList)gvDailyRunningHours.Rows[i].FindControl("ddlUOM");
                TextBox txttxtOutput = (TextBox)gvDailyRunningHours.Rows[i].FindControl("txtOutput");
                TextBox txttxtIssuedDiesel = (TextBox)gvDailyRunningHours.Rows[i].FindControl("txtIssuedDiesel");
                TextBox txttxtRemarks = (TextBox)gvDailyRunningHours.Rows[i].FindControl("txtRemarks");
                FileUpload FUDocs = (FileUpload)gvDailyRunningHours.Rows[i].FindControl("FUDocs");

                // Convert.ToDecimal(txttxtStartKM.Text) != Convert.ToDecimal("0.00") && Convert.ToDecimal(txttxtEndKM.Text) != Convert.ToDecimal("0.00")
                if (txttxtIssuedDiesel.Text != "0.00")
                {
                    Diesel = Convert.ToInt32(txttxtIssuedDiesel.Text);
                }
                else
                {
                    Diesel = 0;
                }
                var StartKM = 0;
                var EndKM = 0;
                if (txttxtStartKM.Text != "0.00" && txttxtEndKM.Text != "0.00")
                {
                     StartKM = Convert.ToInt32(txttxtStartKM.Text);
                     EndKM = Convert.ToInt32(txttxtEndKM.Text);
                }
                
                if (StartKM > 0 && EndKM > 0)
                {
                    if ((Convert.ToDecimal(txttxtStartKM.Text) != Convert.ToDecimal("0.00") && Convert.ToDecimal(txttxtEndKM.Text) != Convert.ToDecimal("0.00")) )
                    {
                        objDailyRunningHourBL = new DailyRunningHourBL();
                        objDailyRunningHourBL.Project_Code = ddlProjectNames.SelectedValue;

                        if (lblUnit != null)
                        {
                            objDailyRunningHourBL.Unit = lblUnit.Text;
                        }

                        if (lbllblCode != null)
                        {
                            objDailyRunningHourBL.Asset_Code = lbllblCode.Text;
                        }

                        objDailyRunningHourBL.Date = Convert.ToDateTime(txtDate.Text);


                        if (txttxtStartKM != null && lblUnit != null && lblUnit.Text == "Litre/Hour")
                        {
                            objDailyRunningHourBL.Start_Hour = Convert.ToDecimal(txttxtStartKM.Text);

                        }
                        else if (txttxtStartKM != null && lblUnit != null && lblUnit.Text != "Litre/Hour")
                        {
                            objDailyRunningHourBL.Start_Km = Convert.ToDecimal(txttxtStartKM.Text);
                        }


                        if (txttxtEndKM != null && lblUnit != null && lblUnit.Text == "Litre/Hour")
                        {
                            objDailyRunningHourBL.End_Hour = Convert.ToDecimal(txttxtEndKM.Text);
                        }
                        else if (txttxtEndKM != null && lblUnit != null && lblUnit.Text != "Litre/Hour")
                        {
                            objDailyRunningHourBL.End_Km = Convert.ToDecimal(txttxtEndKM.Text);

                        }

                        if (ddlddlUOM != null && (ddlddlUOM.SelectedIndex != 0 && ddlddlUOM.SelectedIndex != -1))
                        {
                            objDailyRunningHourBL.UOM_ID = Convert.ToInt16(ddlddlUOM.SelectedValue);
                        }

                        if (txttxtOutput != null)
                        {
                            objDailyRunningHourBL.Output = txttxtOutput.Text;
                        }
                        if (txttxtIssuedDiesel != null)
                        {
                            objDailyRunningHourBL.Issued_Diesel_Qty = Convert.ToDecimal(txttxtIssuedDiesel.Text);
                        }

                        if (txttxtRemarks != null)
                        {
                            objDailyRunningHourBL.Remarks = txttxtRemarks.Text;
                        }

                        if (FUDocs.HasFile)
                        {

                            string[] validFileTypes = { "png", "jpg", "jpeg" };
                            string ext = System.IO.Path.GetExtension(FUDocs.PostedFile.FileName);
                            bool isValidFile = false;
                            for (int j = 0; j < validFileTypes.Length; j++)
                            {
                                if (ext == "." + validFileTypes[j])
                                {
                                    isValidFile = true;
                                    break;
                                }
                            }
                            if (!isValidFile)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please upload the png or jpg or jpeg');", true);

                            }
                            bool existfile = false;
                            string folderpath = Server.MapPath(ConfigurationManager.AppSettings["DailyMachineRunningpath"]).ToString();
                            string filename = FUDocs.FileName;
                            string[] filePaths = Directory.GetFiles(folderpath);
                            foreach (string filechk in filePaths)
                            {
                                if (filechk == folderpath + filename)
                                {
                                    fileNamewithName = filechk;
                                    existfile = true;
                                    break;
                                }

                            }
                            if (!existfile)
                            {

                                objDailyRunningHourBL.FileUpload = FUDocs.FileName;
                                objDailyRunningHourBL.FileUpload = "~/DailyMachineRunningHours_Files/" + uniqueDateTime + FUDocs.FileName.Trim();

                                FUDocs.SaveAs(Server.MapPath("../DailyMachineRunningHours_Files/" + uniqueDateTime + FUDocs.FileName));

                                // this.imgBtnDigitalSign.ImageUrl = "~/EmployeeDigitalSignImg/" + SignUploader.FileName.Trim();
                                if (!string.IsNullOrEmpty(objDailyRunningHourBL.FileUpload))
                                {
                                    ViewState["FileUpload"] = objDailyRunningHourBL.FileUpload;
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render(' This Document Name Already Exists.!!!');", true);
                                return;
                            }
                        }

                    }
                    if (((Button)sender).Text == "Save")
                    {

                        if (objDailyRunningHourBL.insert(con, DailyRunningHourBL.eLoadSp.INSERT))
                        {
                            MINBL objMINBL = new MINBL();
                            DataSet dsStock = new DataSet();
                            objMINBL.Project_Code = ddlProjectNames.SelectedValue;
                            objMINBL.Task = "Sum_Adjusted_Qty_For_DRH";
                            objMINBL.load(con, MINBL.eLoadSp.DEDUCT_STOCK_BASED_ON_ISSUE_QTY, ref dsStock);
                            if (true)
                            {
                                AvailiableQty = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["Adjust_QTY"].ToString());
                            }
                            objMINBL.Task = "Get_Stock_Records_For_DRH";
                            objMINBL.Project_Code = ddlProjectNames.SelectedValue;
                            objMINBL.load(con, MINBL.eLoadSp.DEDUCT_STOCK_BASED_ON_ISSUE_QTY, ref ds_DEDUCT);
                            if (ds_DEDUCT.Tables[0].Rows.Count >= 0)
                            {
                                foreach (DataRow item in ds_DEDUCT.Tables[0].Rows)
                                {
                                    
                                    DataSet ds_Update_Stock_Records = new DataSet();
                                    objMINBL.Task = "Update_Stock_Records";
                                    objMINBL.Issue_Qty = 0;
                                    objMINBL.Stock_ID = Convert.ToInt32(item["Stock_ID"]);
                                    objMINBL.load(con, MINBL.eLoadSp.UPDATE_STOCK_QTY, ref ds_Update_Stock_Records);
                                }
                                objMINBL.Issue_Qty = AvailiableQty + Convert.ToDecimal(Diesel);
                                if (objMINBL.Stock_ID != 0)
                                {
                                    objMINBL.Task = "Update_Stock_Records";
                                    DataSet ds_Update_Stock = new DataSet();
                                    objMINBL.load(con, MINBL.eLoadSp.UPDATE_STOCK_QTY, ref ds_Update_Stock);
                                }
                                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Daily Machine Hours  has been added successfully');", true);
                                GetAvailableDieselQtyFromStock();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Daily Machine Hours already exist, Pls try another Daily Machine Hours ');", true);
                            }
                        }
                    }
                }
            }
            BindALLAsset();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                Response.Redirect("../Asset/DailyMachineRunningHours.aspx", false);
            }
            else
            {
                Response.Redirect("../Asset/DailyMachineRunningHoursList.aspx", false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindALLAsset()
    {
        try
        {
            ds = new DataSet();
            objAsset = new AssetRegistrationBL();
            objAsset.Project_Code = ddlProjectNames.SelectedValue;
            objAsset.Date = Convert.ToDateTime(txtDate.Text.Trim());
            if (objAsset.load(con, AssetRegistrationBL.eLoadSp.Search_WITH_Project_Code_AND_DATE, ref ds))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDailyRunningHours.DataSource = ds;
                    gvDailyRunningHours.DataBind();
                }
                else
                {
                    gvDailyRunningHours.DataSource = null;
                    gvDailyRunningHours.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindALLAsset();
    }

    protected void gvDailyRunningHours_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlUOM = (DropDownList)e.Row.FindControl("ddlUOM");
                if (ddlUOM != null)
                {
                    UOM objUOM = new UOM();
                    ds = new DataSet();
                    objUOM.load(con, UOM.eLoadSp.SELECT_ALL, ref ds);

                    ddlUOM.DataSource = ds;
                    ddlUOM.DataTextField = "UOMPrefix";
                    ddlUOM.DataValueField = "UOM_ID";
                    ddlUOM.DataBind();
                    ddlUOM.Items.Insert(0, "-Select-");
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void gvDailyRunningHours_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDailyRunningHours.PageIndex = e.NewPageIndex;
        BindALLAsset();
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        if(txtDate.Text != string.Empty)
        {
            GetAvailableDieselQtyFromStock();
        }
        else
        {
            txtIssuedDieselQty.Text = "0.00";
        }
    }
}




