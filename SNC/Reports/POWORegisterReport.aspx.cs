using BusinessLayer;
using Obout.Grid;
using SNC.ErrorLogger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.IO;

public partial class POWORegisterReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    VendorBL objVendorBL = null;
    SubContractorBL objSubContractorBL = null;
    ProjectBL objProjectBL = null;
    PurchaseOrderBL objPO = null;
    WorkOrderBL objWO = null;
    DataSet ds = null;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    BindProjectList();
                    ddlOrderType_SelectedIndexChanged(null, null);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrderType.SelectedValue == "PO")
        {
            lblVendorOrSubCon.Text = "Vendor";
            ddlVendor.Visible = true;
            ddlSubContractor.Visible = false;
            BindVendorList();

            lstColumns.Items.Clear();
            lstColumns.Items.Add("Date");
            lstColumns.Items.Add("Project Site");
            lstColumns.Items.Add("Vendor Name");
            lstColumns.Items.Add("GST No");
            lstColumns.Items.Add("TDS Perc");
            lstColumns.Items.Add("TDS Value");
            lstColumns.Items.Add("CGST Amt");
            lstColumns.Items.Add("SGST Amt");
            lstColumns.Items.Add("IGST Amt");
            lstColumns.Items.Add("Total Amt");
            lstColumns.Items.Add("Vendor GST Copy");
            lstColumns.Items.Add("Vendor PAN Copy");
            lstColumns.Items.Add("Vendor Bank Details Copy");

            for (int i = 0; i < lstColumns.Items.Count; i++)
            {
                lstColumns.Items[i].Selected = true;
            }
        }
        else
        {
            lblVendorOrSubCon.Text = "Sub Contractor";
            ddlVendor.Visible = false;
            ddlSubContractor.Visible = true;
            BindSubContracotrList();

            lstColumns.Items.Clear();
            lstColumns.Items.Add("Date");
            lstColumns.Items.Add("Project Site");
            lstColumns.Items.Add("SubContractor Name");
            lstColumns.Items.Add("GST No");
            lstColumns.Items.Add("TDS Perc");
            lstColumns.Items.Add("TDS Value");
            lstColumns.Items.Add("CGST Amt");
            lstColumns.Items.Add("SGST Amt");
            lstColumns.Items.Add("IGST Amt");
            lstColumns.Items.Add("Total Amt");
            lstColumns.Items.Add("Completion Certificate");
            lstColumns.Items.Add("Sub Contractor GST Copy");
            lstColumns.Items.Add("Sub Contractor PAN Copy");
            lstColumns.Items.Add("Sub Contractor Bank Details");

            for (int i = 0; i < lstColumns.Items.Count; i++)
            {
                lstColumns.Items[i].Selected = true;
            }
        }
    }

    protected void BindVendorList()
    {
        try
        {
            ds = new DataSet();
            objVendorBL = new VendorBL();
            objVendorBL.load(con, VendorBL.eLoadSp.SELECT_ALL_VENDOR_NAMES, ref ds);
            ddlVendor.DataSource = ds;
            ddlVendor.DataTextField = "Vendor_name";
            ddlVendor.DataValueField = "Vendor_ID";
            ddlVendor.DataBind();
            ddlVendor.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindSubContracotrList()
    {
        try
        {
            ds = new DataSet();
            objSubContractorBL = new SubContractorBL();
            objSubContractorBL.load(con, SubContractorBL.eLoadSp.SELECT_CONTRACTOR_ALL, ref ds);
            ddlSubContractor.DataSource = ds;
            ddlSubContractor.DataTextField = "Subcon_name";
            ddlSubContractor.DataValueField = "Subcon_ID";
            ddlSubContractor.DataBind();
            ddlSubContractor.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindProjectList()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_ALL, ref ds);
            lstProject.DataValueField = "Project_Code";
            lstProject.DataTextField = "Project_Name"; 
            lstProject.DataSource = ds;
            lstProject.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //objPO = new PurchaseOrderBL();
            //objPO.ProjectCode = Session["Project_Code"].ToString();
            //objPO.QuotationNo = txtQuotNo.Text.Trim();
            //objPO.DespatchAdvise = txtDespatchAdvise.Text.Trim();
            //objPO.ContactName = txtContactName.Text.Trim();
            //objPO.ContactNo = txtContactNo.Text.Trim();
            //objPO.PaymentTerms = txtPayTerms.Text.Trim();
            //objPO.ApprovedBy = Convert.ToInt32(ddlApprovedBy.SelectedValue.Trim());
            //objPO.Status = rblStatus.SelectedValue.Trim();
            //objPO.TIN_No = txtTINNo.Text.Trim();
            //objPO.Remarks = txtRemarks.Text.Trim();
            //objPO.Others = txtOthers.Text.Trim();
            //objPO.Task = "InsertPO";
            
            //if (objPO.insert(con, PurchaseOrderBL.eLoadSp.INSERT))
            //{
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to insert PO details !.');", true);
            //}
            //else
            //{
            //    objPO.Task = "UpdatePO";
            //    objPO.PONo = txtPONo.Text.ToString();
                
            //    if (objPO.update(con, PurchaseOrderBL.eLoadSp.UPDATE))
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('PO details has been updated sucessfully.');", true);
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('PO amount is exceeding the Budget amount !.');", true);
            //    }
            //}
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            Session["OrderType"] = ddlOrderType.SelectedValue.ToString();
            if (ddlVendor.SelectedIndex > 0)
            {
                Session["VendorID"] = ddlVendor.SelectedValue.ToString();
            }
            if (ddlSubContractor.SelectedIndex > 0)
            {
                Session["SubContractorID"] = ddlSubContractor.SelectedValue.ToString();
            }
            if (txtFromDate.Text.Trim() != "")
            {
                Session["FromDate"] = txtFromDate.Text.Trim();
            }
            if (txtToDate.Text.Trim() != "")
            {
                Session["ToDate"] = txtToDate.Text.Trim();
            }
            if (txtFromAmount.Text.Trim() != "")
            {
                Session["FromAmount"] = txtFromAmount.Text.Trim();
            }
            if (txtToAmount.Text.Trim() != "")
            {
                Session["ToAmount"] = txtToAmount.Text.Trim();
            }

            string projects = string.Empty;
            foreach (ListItem listItem in lstProject.Items)
            {
                if (listItem.Selected)
                {
                    if (projects == string.Empty)
                    {
                        projects = listItem.Value;
                    }
                    else
                    {
                        projects += "," + listItem.Value;
                    }
                }
            }
            if (projects.ToString() != "")
            {
                Session["Projects"] = projects.ToString();
            }

            string display_Columns = string.Empty;
            foreach (ListItem listItem in lstColumns.Items)
            {
                if (listItem.Selected)
                {
                    if (display_Columns == string.Empty)
                    {
                        display_Columns = listItem.Value;
                    }
                    else
                    {
                        display_Columns += "," + listItem.Value;
                    }
                }
            }
            Session["Display_Columns"] = display_Columns.ToString();
            
            Session["Report_Flag"] = "POWO_Register";
            string popupvariable = "<script language='javascript'>" + "window.open('/Reports/UGCL_ReportViewer.aspx','','');</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "PopUpWindow", popupvariable, false);
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            ddlOrderType.SelectedValue = "PO";
            ddlOrderType_SelectedIndexChanged(null, null);
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            txtFromAmount.Text = string.Empty;
            txtToAmount.Text = string.Empty;
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

}
