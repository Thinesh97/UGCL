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
//namespace SNC.Reports
//{
public partial class MaterialReciveNoteReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    VendorBL objVendorBL = null;
    SubContractorBL objSubContractorBL = null;
    PurchaseOrderBL objPurchasrOrderBL = null;
    WorkOrderBL objworkOrderBL = null;
    ProjectBL objProjectBL = null;
    PurchaseOrderBL objPO = null;
    WorkOrderBL objWO = null;
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["UID"] != null)
                    {
                        BindProjectList();
                        ddlreportType_SelectedIndexChanged(null, null);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
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
    protected void ddlreportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlreportType.SelectedValue == "PM")
        {
            lblVendorOrSubCon.Text = "Vendor";
            ddlVendorWise.Visible = true;
            ddlSubContractor.Visible = false;
            BindVendorList();

            
        }
        else
        {
            lblVendorOrSubCon.Text = "Sub Contractor";
            ddlVendorWise.Visible = false;
            ddlSubContractor.Visible = true;
            BindSubContracotrList();

            //lstColumns.Items.Clear();
            //lstColumns.Items.Add("Date");
            //lstColumns.Items.Add("Project Site");
            //lstColumns.Items.Add("SubContractor Name");
            //lstColumns.Items.Add("GST No");
            //lstColumns.Items.Add("TDS Perc");
            //lstColumns.Items.Add("TDS Value");
            //lstColumns.Items.Add("CGST Amt");
            //lstColumns.Items.Add("SGST Amt");
            //lstColumns.Items.Add("IGST Amt");
            //lstColumns.Items.Add("Total Amt");
            //lstColumns.Items.Add("Completion Certificate");
            //lstColumns.Items.Add("Sub Contractor GST Copy");
            //lstColumns.Items.Add("Sub Contractor PAN Copy");
            //lstColumns.Items.Add("Sub Contractor Bank Details");

            //for (int i = 0; i < lstColumns.Items.Count; i++)
            //{
            //    lstColumns.Items[i].Selected = true;
            //}
        }
    }

    protected void BindVendorList()
    {
        try
        {
            ds = new DataSet();
            objVendorBL = new VendorBL();
            objVendorBL.load(con, VendorBL.eLoadSp.SELECT_ALL_VENDOR_NAMES, ref ds);
            ddlVendorWise.DataSource = ds;
            ddlVendorWise.DataTextField = "Vendor_name";
            ddlVendorWise.DataValueField = "Vendor_ID";
            ddlVendorWise.DataBind();
            ddlVendorWise.Items.Insert(0, "-Select-");
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

    protected void ddlVendorWise_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVendorPO();
    }
    protected void BindVendorPO()
    {
        try
        {
            ds = new DataSet();
            objPurchasrOrderBL = new PurchaseOrderBL();
            objPurchasrOrderBL.VendorID = ddlVendorWise.SelectedValue;            
            objPurchasrOrderBL.load(con, PurchaseOrderBL.eLoadSp.PONo_SELECT_BY_VendorId, ref ds);
            ddlpowo.DataSource = ds;
            ddlpowo.DataTextField = "PONo";
            ddlpowo.DataValueField = "VendorID";
            ddlpowo.DataBind();
            ddlpowo.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void ddlSubContractor_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubcontractorWOHO();
    }
    protected void BindSubcontractorWOHO()
    {
        try
        {
            ds = new DataSet();
            objworkOrderBL = new WorkOrderBL();
            objworkOrderBL.SubContractorID = ddlSubContractor.SelectedValue;
            objworkOrderBL.load(con, WorkOrderBL.eLoadSp.WOHNo_SELECT_BY_SubcotractorId, ref ds);
            ddlpowo.DataSource = ds;
            ddlpowo.DataTextField = "POWO_No";
            ddlpowo.DataValueField = "POWO_ID";
            ddlpowo.DataBind();
            ddlpowo.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlreportType.SelectedValue = "PM";
            ddlreportType_SelectedIndexChanged(null, null);
            ddlVendorWise_SelectedIndexChanged(null, null);
            ddlSubContractor_SelectedIndexChanged(null, null);
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            ddlpowo.SelectedIndex=-1;
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
            Session["OrderType"] = ddlreportType.SelectedValue.ToString();
            if (ddlVendorWise.SelectedIndex > 0)
            {
                Session["VendorID"] = ddlVendorWise.SelectedValue.ToString();
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
            if (ddlpowo.SelectedIndex > 0)
            {
                Session["PONumber"] = ddlpowo.SelectedValue.ToString();
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
            Session["Report_Flag"] = "POWOMRN_Register";
            string popupvariable = "<script language='javascript'>" + "window.open('/Reports/UGCL_MRNReportViewer.aspx','','');</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "PopUpWindow", popupvariable, false);
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
}

//}