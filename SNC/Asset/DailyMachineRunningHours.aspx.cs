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
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DailyMachineRunningHours : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    DailyRunningHourBL objDailyRunningHourBL = null;
    AssetRegistrationBL objAsset = null;
    DataSet ds = null;
    string fileNamewithName;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["UID"] != null)
            {
                BindUOMList();
                BindAssetType();
                imgBtnUploadedFile.Visible = false;
                if (rbtUnit.SelectedItem.Text == "Hours")
                {
                    trKM.Visible = false;
                    trHrs.Visible = true;
                }

                if (Request.QueryString["Daily_Run_Id"] != null)
                {
                    GetDRHList(Request.QueryString["Daily_Run_Id"].ToString());
                }
                if (Request.QueryString["ID"] != null)
                {
                    GetDRHList(Request.QueryString["ID"].ToString());
                }
            }
            else
            {
                Response.Redirect("../CommonPages/Login.aspx", false);
            }
        }
    }

    protected void BindUOMList()
    {
        try
        {
            ds = new DataSet();
            objDailyRunningHourBL = new DailyRunningHourBL();
            objDailyRunningHourBL.load(con, DailyRunningHourBL.eLoadSp.SELECT_UOM_ALL, ref ds);
            ddlUOM.DataSource = ds;
            ddlUOM.DataValueField = "UOM_ID";
            ddlUOM.DataTextField = "UOMPrefix";
            ddlUOM.DataBind();
            ddlUOM.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }

    private void BindAssetType()
    {
        try
        {
            ds = new DataSet();
            objAsset = new AssetRegistrationBL();
            objAsset.load(con, AssetRegistrationBL.eLoadSp.SELECT_ASSET_TYPE_ALL, ref ds);

            ddlType.Items.Clear();
            ddlType.DataTextField = "Asset_Type";
            ddlType.DataValueField = "Asset_Type_ID";
            ddlType.DataSource = ds;
            ddlType.DataBind();
            ddlType.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void DownloadFiles(string filename, string folderpath)
    {
        try
        {
            bool existfile = false;

            if (filename != string.Empty)
            {
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
                if (existfile)
                {
                    Response.ContentType = ContentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                    Response.WriteFile(fileNamewithName);
                    // Response.End();
                    Response.Flush();


                    //System.Diagnostics.Process process = new System.Diagnostics.Process();
                    //process.StartInfo.UseShellExecute = true;
                    //process.StartInfo.FileName = fileNamewithName;
                    //process.Start();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render(' File not exists !');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }
    protected void imgBtnDigitalSign_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ViewState["FileUpload"] !="")
            {
                //fileNamewithName = ViewState["FileUpload"].ToString();
                //Response.ContentType = ContentType;
                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileNamewithName);
                //Response.WriteFile(fileNamewithName);
                //// Response.End();
                //Response.Flush();

                string filePath = fileNamewithName;
                Response.ContentType = "image/jpg";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
                Response.TransmitFile(Server.MapPath(filePath));
                Response.End(); 
            }
           

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileReadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }
    protected void BindAssetNameList()
    {
        try
        {
            DataSet drhds = new DataSet();
            objDailyRunningHourBL = new DailyRunningHourBL();
            if (ddlAssetCategory.SelectedIndex != 0)
            {
                objDailyRunningHourBL.Asset_Category = Convert.ToInt32(ddlAssetCategory.SelectedValue);
                objDailyRunningHourBL.UID = Convert.ToInt32(Session["UID"]);
                objDailyRunningHourBL.load(con, DailyRunningHourBL.eLoadSp.SELECT_Asset_Name_ALL, ref drhds);
                ddlAssetName.DataSource = drhds;
                ddlAssetName.DataValueField = "Code";
                ddlAssetName.DataTextField = "AssetName";
                ddlAssetName.DataBind();
                ddlAssetName.Items.Insert(0, "-Select-");
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }

    protected void GetDRHList(string Daily_Run_Id)
    {
        try
        {
            objDailyRunningHourBL = new DailyRunningHourBL();
            ds = new DataSet();
            if (Request.QueryString["Daily_Run_Id"] != null)
            {
                objDailyRunningHourBL.Daily_Run_Id = Convert.ToInt32(Request.QueryString["Daily_Run_Id"].ToString());
            }
            if (Request.QueryString["ID"] != null)
            {
                objDailyRunningHourBL.Daily_Run_Id = Convert.ToInt32(Request.QueryString["ID"].ToString());
            }
            objDailyRunningHourBL.Daily_Run_Id = Convert.ToInt16(Daily_Run_Id);
            objDailyRunningHourBL.load(con, DailyRunningHourBL.eLoadSp.SELECT_DAILY_RUNNING_HOUR_BY_Daily_Run_Id, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtDate.Text = ds.Tables[0].Rows[0]["Date"].ToString();
                txtDate.Enabled = false;
                rbtUnit.SelectedValue = ds.Tables[0].Rows[0]["Unit"].ToString();
                if (rbtUnit.SelectedValue == "Litre/Hour")
                {
                    trKM.Visible = false;
                    trHrs.Visible = true;
                    txtStartKm.Text = string.Empty;
                    txtEndKm.Text = string.Empty;
                }
                else
                {
                    trHrs.Visible = false;
                    trKM.Visible = true;
                    txtStartHour.Text = string.Empty;
                    txtEndHour.Text = string.Empty;
                }
                ddlType.Enabled = false;
                ddlAssetCategory.Enabled = false;
                ddlAssetRegistration.Enabled = false;
                txtStartKm.Text = ds.Tables[0].Rows[0]["Start_Km"].ToString();
                txtEndKm.Text = ds.Tables[0].Rows[0]["End_Km"].ToString();
                txtStartHour.Text = ds.Tables[0].Rows[0]["Start_Hour"].ToString();
                txtEndHour.Text = ds.Tables[0].Rows[0]["End_Hour"].ToString();
                txtOutput.Text = ds.Tables[0].Rows[0]["Output"].ToString();
                txtIssuedDieselQty.Text = ds.Tables[0].Rows[0]["Issued_Diesel_Qty"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                ddlAssetName.SelectedValue = ds.Tables[0].Rows[0]["Asset_Code"].ToString();
                ddlAssetName.Enabled = false;

                ddlUOM.SelectedValue = ds.Tables[0].Rows[0]["UOM_ID"].ToString();
                ddlUOM.Enabled = true;
                btnSave.Text = "Update";
                rbtUnit.Enabled = false;
                txtIssuedDieselQty.Enabled = false;
                if (ds.Tables[0].Rows[0]["Asset_Type_ID"].ToString() != string.Empty)
                {

                    ddlType.SelectedValue = ds.Tables[0].Rows[0]["Asset_Type_ID"].ToString();
                    ddlType_SelectedIndexChanged(null, null);
                }

                if (ds.Tables[0].Rows[0]["Asset_cat_ID"].ToString() != string.Empty)
                {

                    ddlAssetCategory.SelectedValue = ds.Tables[0].Rows[0]["Asset_cat_ID"].ToString();
                    ddlAssetCategory_SelectedIndexChanged(null, null);
                }

                if (ds.Tables[0].Rows[0]["Asset_Code"].ToString() != string.Empty)
                {
                    ddlAssetName.SelectedValue = ds.Tables[0].Rows[0]["Asset_Code"].ToString();
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["FileUpload"].ToString()))
                {
                    imgBtnUploadedFile.ImageUrl = ds.Tables[0].Rows[0]["FileUpload"].ToString();
                    imgBtnUploadedFile.Visible = true;
                    ViewState["FileUpload"] = ds.Tables[0].Rows[0]["FileUpload"].ToString();
                }

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
            if (rbtUnit.SelectedValue == "Hours")
            {
                if (Convert.ToDecimal(txtStartHour.Text) > Convert.ToDecimal(txtEndHour.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('End Hour should be greater than Start Hour');", true);
                    return;
                }
            }

            if (rbtUnit.SelectedValue == "Kms")
            {
                if (Convert.ToDecimal(txtStartKm.Text) > Convert.ToDecimal(txtEndKm.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('End Km should be greater than Start Km');", true);
                    return;
                }
            }
            objDailyRunningHourBL = new DailyRunningHourBL();
            objDailyRunningHourBL.Date = Convert.ToDateTime(txtDate.Text);
            objDailyRunningHourBL.Asset_Code = ddlAssetName.SelectedValue;
            objDailyRunningHourBL.Unit = rbtUnit.SelectedValue;

            if (txtStartKm.Text != string.Empty)
            {
                objDailyRunningHourBL.Start_Km = Convert.ToDecimal(txtStartKm.Text);
            }
            else
            {
                objDailyRunningHourBL.Start_Km = 0;
            }

            if (txtEndKm.Text != string.Empty)
            {
                objDailyRunningHourBL.End_Km = Convert.ToDecimal(txtEndKm.Text);
            }
            else
            {
                objDailyRunningHourBL.End_Km = 0;
            }

            if (txtStartHour.Text != string.Empty)
            {
                objDailyRunningHourBL.Start_Hour = Convert.ToDecimal(txtStartHour.Text);
            }
            else
            {
                objDailyRunningHourBL.Start_Hour = 0;
            }

            if (txtEndHour.Text != string.Empty)
            {
                objDailyRunningHourBL.End_Hour = Convert.ToDecimal(txtEndHour.Text);
            }
            else
            {
                objDailyRunningHourBL.End_Hour = 0;
            }

            objDailyRunningHourBL.UOM_ID = Convert.ToInt16(ddlUOM.SelectedValue);
            objDailyRunningHourBL.Output = txtOutput.Text;
            objDailyRunningHourBL.Issued_Diesel_Qty = Convert.ToDecimal(txtIssuedDieselQty.Text);
            objDailyRunningHourBL.Remarks = txtRemarks.Text;
            objDailyRunningHourBL.UserID = Session["User_ID"].ToString();

            if (((Button)sender).Text == "Save")
            {
                if (objDailyRunningHourBL.insert(con, DailyRunningHourBL.eLoadSp.INSERT))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Daily Machine Hours  has been added successfully');", true);
                    ClearRunningHoursFields();
                    //GetAvailableDieselQtyFromStock();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Daily Machine Hours already exist, Pls try another Daily Machine Hours ');", true);
                }
            }
            else
            {
                if (Request.QueryString["Daily_Run_Id"] != null)
                {
                    GetDRHList(Request.QueryString["Daily_Run_Id"].ToString());
                }
                if (Request.QueryString["ID"] != null)
                {
                    objDailyRunningHourBL.Daily_Run_Id = Convert.ToInt32(Request.QueryString["ID"].ToString());
                }
                //objDailyRunningHourBL.Daily_Run_Id = Convert.ToInt32(Request.QueryString["Daily_Run_Id"].ToString());

                if (objDailyRunningHourBL.update(con, DailyRunningHourBL.eLoadSp.UPDATE))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Daily Machine Hours  details has been updated');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update the Daily Machine Hours  details');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }

    private void ClearRunningHoursFields()
    {
        txtDate.Text = string.Empty;
        //ddlAssetName.SelectedValue = ddlAssetName.Items.FindByText("-Select-").Value;
        ddlAssetCategory.Items.Clear();
        ddlAssetCategory.Items.Insert(0, "-Select");
        ddlAssetName.Items.Clear();
        ddlAssetName.Items.Insert(0, "-Select");
        ddlType.SelectedValue = ddlType.Items.FindByText("-Select-").Value;

        //ddlAssetCategory.SelectedValue = ddlAssetCategory.Items.FindByText("-Select-").Value;

        rbtUnit.SelectedValue = "Hours";
        txtStartKm.Text = "0.00";
        txtEndKm.Text = "0.00";
        txtStartHour.Text = string.Empty;
        txtEndHour.Text = string.Empty;
        ddlUOM.SelectedValue = ddlUOM.Items.FindByText("-Select-").Value;
        txtOutput.Text = "0.00";
        txtIssuedDieselQty.Text = string.Empty;
        txtRemarks.Text = string.Empty;
    }

    protected void ddlunit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtUnit.SelectedItem.Text == "Hours")
            {
                trKM.Visible = false;
                trHrs.Visible = true;
                txtStartKm.Text = "0.00";
                txtEndKm.Text = "0.00";
            }
            else
            {
                txtStartHour.Text = "0.00";
                txtEndHour.Text = "0.00";
                trHrs.Visible = false;
                trKM.Visible = true;
            }
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

    protected void ddlAssetCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAssetNameList();
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlType.SelectedIndex != 0)
            {
                objAsset = new AssetRegistrationBL();
                DataSet ds = new DataSet();
                objAsset.Asset_Type = ddlType.SelectedValue;
                objAsset.load(con, AssetRegistrationBL.eLoadSp.SELECT_Asset_BY_ID, ref ds);
                ddlAssetCategory.DataSource = ds;
                ddlAssetCategory.DataValueField = "AssetCatID";
                ddlAssetCategory.DataTextField = "CateName";
                ddlAssetCategory.DataBind();
                ddlAssetCategory.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlAssetCategory.Items.Clear();
                ddlAssetCategory.Items.Insert(0, "-Select-");
                ddlAssetCategory.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

}




