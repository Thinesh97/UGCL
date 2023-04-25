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
//using SelectPdf;

public partial class PurchaseOrder : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    VendorBL objVendorBL = null;
    IndentBL objIndent = null;
    decimal NetTotalAmt = 0;
    decimal Total_Amount = 0;
    DataSet ds = null;
    PurchaseOrderBL objPO = null;
    ProjectBL objProjectBL = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    BindVendorList();
                    //BindIndentNo();
                    BindUsersNames();
                    BindProject();

                    if (Request.QueryString["PONo"] != null)
                    {
                        GetPurchaseOrderDetails(Request.QueryString["PONo"].ToString());

                        ddlIndentNo.Enabled = false;
                        //btnMailToVendor.Visible = true;
                        //btnTermsnConditons.Visible = true;
                        //btnTermsnConditons.Target = "_blank";
                        //btnTermsnConditons.HRef = "PO_Print_Backside.aspx?PONo=" + Request.QueryString["PONo"].ToString();
                        btnPrint.Visible = true;
                        btnPrint.Target = "_blank";
                        btnPrint.HRef = "PO_Print.aspx?PONo=" + Request.QueryString["PONo"].ToString();
                        div_Draft.Visible = true;
                        div_BeforeUpload.Visible = true;
                        btnPrintPDF.Visible = false;
                        Grid_POItem.Columns[15].Visible = false;
                        Grid_POItem.Columns[16].Visible = false;

                        btnSubmit.Text = "Update";
                        CheckPONoInMRN();
                    }

                    if (Request.QueryString["PONo"] != null && Request.QueryString["s"] != null)
                    {
                        GetPurchaseOrderDetails(Request.QueryString["PONo"].ToString());
                        ddlIndentNo.Enabled = false;
                        // btnMailToVendor.Visible = true;
                        //btnTermsnConditons.Visible = true;
                        //btnTermsnConditons.Target = "_blank";
                        //btnTermsnConditons.HRef = "PO_Print_Backside.aspx?PONo=" + Request.QueryString["PONo"].ToString();
                        btnPrint.Visible = true;
                        btnPrint.Target = "_blank";
                        btnPrint.HRef = "PO_Print.aspx?PONo=" + Request.QueryString["PONo"].ToString();
                        div_Draft.Visible = true;
                        div_BeforeUpload.Visible = true;
                        btnPrintPDF.Visible = false;
                        Grid_POItem.Columns[15].Visible = false;
                        Grid_POItem.Columns[16].Visible = false;
                        btnSubmit.Text = "Update";
                        CheckPONoInMRN();

                        string message = "PO has been Created Successfully";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                    }

                    if (Request.QueryString["PoApprovalID"] != null)
                    {
                        GetPurchaseOrderDetails(Request.QueryString["PoApprovalID"].ToString());

                        ddlIndentNo.Enabled = false;
                        btnPrint.Visible = true;
                        btnPrint.Target = "_blank";
                        btnPrint.HRef = "PO_Print.aspx?PONo=" + Request.QueryString["PoApprovalID"].ToString();
                        div_Draft.Visible = true;
                        div_BeforeUpload.Visible = true;
                        btnPrintPDF.Visible = false;
                        //btnMailToVendor.Visible = false;
                        //btnTermsnConditons.Visible = true;
                        //btnTermsnConditons.Target = "_blank";
                        //btnTermsnConditons.HRef = "PO_Print_Backside.aspx?PONo=" + Request.QueryString["PoApprovalID"].ToString();

                        Grid_POItem.Columns[15].Visible = false;
                        Grid_POItem.Columns[16].Visible = false;
                        btnSubmit.Visible = false;
                        btnCancel.Visible = true;
                        divPOapproval.Visible = true;
                        txtDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    }

                    if (Request.QueryString["IndentNo"] != null)
                    {
                        lbl_data.Text = Request.QueryString["IndentNo"].ToString();
                        Grid_POItem.Columns[16].Visible = false;
                        Grid_POItem.Columns[15].Visible = false;
                        GetIndentDetails(Request.QueryString["IndentNo"].ToString());
                    }

                    if (Request.QueryString["QuotationIndentNo"] != null && Request.QueryString["Vendor"] != null)
                    {
                        Grid_POItem.Columns[16].Visible = false;
                        Grid_POItem.Columns[15].Visible = false;

                        GetIndentDetails(Request.QueryString["QuotationIndentNo"].ToString());
                        ddlVendor.SelectedValue = Request.QueryString["Vendor"].ToString();
                        ddlVendor_SelectedIndexChanged(null, null);
                        GetTINNoFromLocation();
                    }
                }
                else
                {
                    Response.Redirect("../CommonPages/Login.aspx", false);
                }
            }
            calDecision.StartDate = DateTime.Now;
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void CheckPONoInMRN()
    {
        objPO = new PurchaseOrderBL();
        ds = new DataSet();
        objPO.PONo = Request.QueryString["PONo"].ToString();
        objPO.load(con, PurchaseOrderBL.eLoadSp.CHECK_PONO_IN_MRN, ref ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            Grid_POItem.Columns[16].Visible = false;
            Grid_POItem.Columns[15].Visible = false;
            NetTotalAmt = 0;
            Total_Amount = 0;

            //Grid_POItem.DataBind();
            BindPOItems();
            BindPOItemWiseTaxGrid();
            BindTaxGrid();

            btnSubmit.Visible = false;
            //btnMailToVendor.Visible = false;
            btnCancel.Visible = true;
            btnPrint.Visible = true;
            div_Draft.Visible = true;
            btnPrintPDF.Visible = false;
            //btnTermsnConditons.Visible = true;
        }
    }

    private void BindIndentItem()
    {
        objIndent = new IndentBL();
        ds = new DataSet();
        objIndent.Indent_No = ddlIndentNo.Text;
        objIndent.load(con, IndentBL.eLoadSp.SELECT_INDENT_ITEMS_BY_INDENT_NO, ref ds);

        //Total_Amount = 0;
        Grid_POItem.DataSource = ds;
        Grid_POItem.DataBind();
    }

    protected void GetIndentDetails(string intdentNo)
    {
        try
        {

            objIndent = new IndentBL();
            ds = new DataSet();
            objIndent.Indent_No = intdentNo;
            objIndent.load(con, IndentBL.eLoadSp.SELECT_INDENTDETAILS_BY_ID, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlIndentNo.Text = ds.Tables[0].Rows[0]["Indent_No"].ToString();
                BindIndentItem();
                BindIndentItems();
            }
        }

        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void GetPurchaseOrderDetails(string PONo)
    {
        objPO = new PurchaseOrderBL();
        ds = new DataSet();
        objPO.PONo = PONo;
        objPO.load(con, PurchaseOrderBL.eLoadSp.SELECT_PODETAILS_BY_PONO, ref ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtPONo.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
            txtPODate.Text = ds.Tables[0].Rows[0]["PODate"].ToString();
            ddlProject.SelectedValue = ds.Tables[0].Rows[0]["Project_Code"].ToString();
            ddlProject_SelectedIndexChanged(null, null);
            ddlIndentNo.Items.Insert(0, ds.Tables[0].Rows[0]["IndentNo"].ToString());
            ddlIndentNo.SelectedValue = ds.Tables[0].Rows[0]["IndentNo"].ToString();
            txtQuotNo.Text = ds.Tables[0].Rows[0]["Quot_No"].ToString();
            txtDeliverySchedule.Text = ds.Tables[0].Rows[0]["DeliverySchedule"].ToString();
            ddlVendor.SelectedValue = ds.Tables[0].Rows[0]["VendorID"].ToString();
            txtVendorRef.Text = ds.Tables[0].Rows[0]["VendorRef"].ToString();
            txtDueDate.Text = ds.Tables[0].Rows[0]["Due_Date"].ToString();
            //txtUGCLRef.Text = ds.Tables[0].Rows[0]["UGCLRef"].ToString();
            Session["DispatchAdd"] = ds.Tables[0].Rows[0]["DespatchAdvise"].ToString();
            txtPayTerms.Text = ds.Tables[0].Rows[0]["PaymentTerms"].ToString();
            ddlApprovedBy.SelectedValue = ds.Tables[0].Rows[0]["ApprovedBy"].ToString();
            txtTINNo.Text = ds.Tables[0].Rows[0]["TIN_No"].ToString();
            txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
            txtOthers.Text = ds.Tables[0].Rows[0]["Other_Terms"].ToString();
            txtContactName.Text = ds.Tables[0].Rows[0]["Contact_Name"].ToString();
            txtContactNo.Text = ds.Tables[0].Rows[0]["Contact_No"].ToString();
            lnkDownloadFile.Text = ds.Tables[0].Rows[0]["Uploaded_File"].ToString();
            div_AfterUpload.Visible = ds.Tables[0].Rows[0]["Uploaded_File"].ToString() != string.Empty ? true : false;
            ViewState["POID"] = ds.Tables[0].Rows[0]["PO_ID"].ToString();
            if (ds.Tables[0].Rows[0]["Status"].ToString() != string.Empty)
            {
                rblStatus.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
            }
            if (ds.Tables[0].Rows[0]["ApprovalStatus"].ToString() != string.Empty)
            {
                rdoStatus.SelectedValue = ds.Tables[0].Rows[0]["ApprovalStatus"].ToString();
                txtDate.Text = ds.Tables[0].Rows[0]["Approval_Date"].ToString();
                txtComments.Text = ds.Tables[0].Rows[0]["Approver_Com"].ToString();
            }

            if (ds.Tables[0].Rows[0]["ApprovalStatus"].ToString() == "Approved")
            {
                btnSubmit.Visible = false;
            }

            BindPOItems();
            BindPOItemWiseTaxGrid();
            BindTaxGrid();
            GetTINNoFromLocation();
            txtDespatchAdvise.Text = Session["DispatchAdd"].ToString(); //As dispatch location is changing in previous function.
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

    protected void BindUsersNames()
    {
        try
        {
            objIndent = new IndentBL();
            ds = new DataSet();
            objIndent.load(con, IndentBL.eLoadSp.SELECT_USERNAMES_ALL, ref ds);
            bool exists;
            DataTable DatafilterDt = new DataTable();

            if (ds.Tables[0].Rows.Count > 0)
            {
                DatafilterDt = ds.Tables[0];

                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<bool>("IsHoUser").Equals(true)).Count() > 0;
                if (exists)
                {
                    EnumerableRowCollection<DataRow> query = from order in DatafilterDt.AsEnumerable()
                                                             //where order.Field<bool>("IsHoUser") == true && order.Field<int>("UID") == 9 || order.Field<int>("UID") == 54 || order.Field<int>("UID") == 33 || order.Field<int>("UID") == 74 || order.Field<int>("UID") == 84
                                                             select order;

                    DataView view = query.AsDataView();

                    ddlApprovedBy.DataSource = view;
                    ddlApprovedBy.DataTextField = "Name";
                    ddlApprovedBy.DataValueField = "UID";
                    ddlApprovedBy.DataBind();
                    ddlApprovedBy.Items.Insert(0, "-Select-");
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindIndentNo()
    {
        try
        {
            ds = new DataSet();
            DataTable DatafilterDt;
            bool exists;
            objIndent = new IndentBL();
            objIndent.Indent_No = lbl_data.Text;
            //objIndent.UserID = Convert.ToInt32(Session["UID"]);
            objIndent.Project_Code = ddlProject.SelectedValue.ToString();
            objIndent.load(con, IndentBL.eLoadSp.SELECT_INDENT_NO_FOR_PO, ref ds);
            if (Request.QueryString["IndentNo"] != null || Request.QueryString["QuotationIndentNo"] != null)
            {
                ddlIndentNo.Items.Clear();
                ddlIndentNo.DataSource = ds;
                ddlIndentNo.DataTextField = "Indent_No";
                ddlIndentNo.DataValueField = "Indent_No";
                ddlIndentNo.DataBind();
            }
            else
            {
                DatafilterDt = ds.Tables[0];
                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Status").Equals("Processed")).Count() > 0;
                if (exists)
                {
                    DataTable IndentNodt = DatafilterDt.AsEnumerable()
                                 .Where(r => r.Field<string>("Status") == "Processed")
                                 .CopyToDataTable();
                    ddlIndentNo.Items.Clear();
                    ddlIndentNo.DataSource = IndentNodt;
                    ddlIndentNo.DataValueField = "Indent_No";
                    ddlIndentNo.DataTextField = "Indent_No";
                    ddlIndentNo.DataBind();
                }
                exists = false;
            }
            //ddlIndentNo.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objPO = new PurchaseOrderBL();
            objPO.ProjectCode = ddlProject.SelectedValue.ToString();  //Session["Project_Code"].ToString();
            objPO.PODate = Convert.ToDateTime(txtPODate.Text.Trim());
            objPO.IndentNo = ddlIndentNo.SelectedValue.Trim();
            objPO.DeliverySchedule = txtDeliverySchedule.Text.Trim();
            objPO.VendorID = ddlVendor.SelectedValue.Trim();            
            objPO.VendorRef = txtVendorRef.Text.Trim();
            if (txtDueDate.Text != string.Empty)
            {
                objPO.DueDate = Convert.ToDateTime(txtDueDate.Text.Trim());
            }
            else
            {
                objPO.DueDate = null;
            }
            //if (txtUGCLRef.Text != string.Empty)
            //{
            //    objPO.UGCLRef = Convert.ToDateTime(txtUGCLRef.Text.Trim());
            //}
            //else
            //{
            //    objPO.UGCLRef = null;
            //}
            objPO.QuotationNo = txtQuotNo.Text.Trim();
            objPO.DespatchAdvise = txtDespatchAdvise.Text.Trim();
            objPO.ContactName = txtContactName.Text.Trim();
            objPO.ContactNo = txtContactNo.Text.Trim();
            objPO.PaymentTerms = txtPayTerms.Text.Trim();
            objPO.ApprovedBy = Convert.ToInt32(ddlApprovedBy.SelectedValue.Trim());
            objPO.Status = rblStatus.SelectedValue.Trim();
            objPO.TIN_No = txtTINNo.Text.Trim();
            objPO.Remarks = txtRemarks.Text.Trim();
            objPO.Others = txtOthers.Text.Trim();
            objPO.Task = "InsertPO";
            if (ViewState["TransportationExtraCostExits"] != null)
            {
                objPO.TransportCost_Exists = Convert.ToBoolean(ViewState["TransportationExtraCostExits"]);
            }
            else
            {
                objPO.TransportCost_Exists = false;
            }

            if (Session["UID"] != null)
            {
                int U = Convert.ToInt32(Session["UID"].ToString());
                objPO.UserID = U;
            }

            if (Request.QueryString["QuotationIndentNo"] != null && Request.QueryString["Vendor"] != null)
            {
                objPO.Flag = 0;
            }
            else
            {
                objPO.Flag = 1;
            }


            if (btnSubmit.Text == "Submit")
            {
                if(rblStatus.SelectedValue!="Open")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select Open status for PO submission!.');", true);
                    return;
                }
                else
                { 
                    if (objPO.insert(con, PurchaseOrderBL.eLoadSp.INSERT))
                    {
                        //btnMailToVendor.Visible = true;
                        btnPrint.Visible = true;
                        btnPrint.HRef = "PO_Print.aspx?PONo=" + objPO.PONo.ToString();
                        btnPrint.Target = "_blank";
                        div_Draft.Visible = true;
                        div_BeforeUpload.Visible = true;
                        btnPrintPDF.Visible = false;

                        btnSubmit.Text = "Update";
                        txtPONo.Enabled = false;
                        ddlIndentNo.Enabled = false;
                        txtPONo.Text = objPO.PONo.ToString();
                        ViewState["PONo"] = objPO.PONo.ToString();
                        ViewState["POID"] = Convert.ToInt32(objPO.PO_ID);
                        //btnTermsnConditons.Visible = true;
                        //btnTermsnConditons.Target = "_blank";
                        //btnTermsnConditons.HRef = "PO_Print_Backside.aspx?PONo=" + ViewState["PONo"].ToString();

                        BindPOItems();
                        //BindPOItemWiseTaxGrid();
                        BindTaxGrid();

                        NetTotalAmt = 0;
                        Total_Amount = 0;

                        Response.Redirect("PurchaseOrder.aspx?PONo=" + ViewState["PONo"].ToString() + "&s=1", false);

                        ddlVendor.Enabled = false;

                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('PO details has been inserted sucessfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to insert PO details !.');", true);
                    }
                }
            }
            else
            {
                objPO.Task = "UpdatePO";
                objPO.PONo = txtPONo.Text.ToString();
                if (fuPODoc.HasFile)
                {
                    objPO.Uploaded_File = "PO_" + txtPONo.Text.ToString() + ".pdf";
                }

                if (objPO.update(con, PurchaseOrderBL.eLoadSp.UPDATE))
                {
                    if (fuPODoc.HasFile)
                    {
                        fuPODoc.SaveAs(Server.MapPath("~\\UploadedFiles\\PO_" + txtPONo.Text.Replace("/", "-") + ".pdf"));
                        lnkDownloadFile.Text = "PO_" + txtPONo.Text + ".pdf";
                        div_AfterUpload.Visible = true;
                    }
                    btnPrint.Visible = true;
                    btnPrint.HRef = "PO_Print.aspx?PONo=" + Request.QueryString["PONo"].ToString();
                    btnPrint.Target = "_blank";
                    div_Draft.Visible = true;
                    div_BeforeUpload.Visible = true;
                    btnPrintPDF.Visible = false;
                    Grid_POItem.Columns[16].Visible = false;
                    Grid_POItem.Columns[15].Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('PO details has been updated sucessfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('PO amount is exceeding the Budget amount !.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void BindTaxItems(string QutoationNo)
    {
        try
        {
            objPO = new PurchaseOrderBL();
            objPO.IndentNo = ddlIndentNo.SelectedValue.ToString();
            objPO.VendorID = ddlVendor.SelectedValue;
            objPO.Task = "BindPOTaxGrid";
            objPO.ProjectCode = ddlProject.SelectedValue.ToString();
            DataSet ds = new DataSet();
            DataTable filterdt;

            if (objPO.load(con, PurchaseOrderBL.eLoadSp.PROC_SELECT_PO_CREATING_ITEMS, ref ds))
            {
                if (ds.Tables[0].Rows.Count >= 0)
                {
                    filterdt = ds.Tables[0];

                    foreach (DataRow row in filterdt.Rows)
                    {
                        string type = row["Type"].ToString();
                        string value = row["Description"].ToString();

                        if (type == "Transport" && value == "Extra")
                        {
                            ViewState["TransportationExtraCostExits"] = true;
                        }
                    }
                    //DataTable dt1 = new DataTable();
                    //dt1.Columns.Add("Type", typeof(string));
                    //dt1.Columns.Add("Type_Perc", typeof(decimal));
                    //dt1.Columns.Add("Description", typeof(string));
                    //dt1.Columns.Add("Type_Amount", typeof(decimal));

                    //DataView view = new DataView(ds.Tables[0]);
                    //DataTable distinctValues = view.ToTable(true, "Type", "Type_Perc", "Description");

                    //DataView dv = new DataView(ds.Tables[0]);

                    //Decimal Amount = 0.0M;
                    //for (int i = 0; i < distinctValues.Rows.Count; i++)
                    //{
                    //    dv.RowFilter = "Type = '" + distinctValues.Rows[i]["Type"].ToString() + "' and Type_Perc=" + distinctValues.Rows[i]["Type_Perc"].ToString() + " and Description='" + distinctValues.Rows[i]["Description"].ToString() + "'";
                    //    for (int j = 0; j < dv.ToTable().Rows.Count; j++)
                    //    {
                    //        Amount = Amount + Convert.ToDecimal(dv.ToTable().Rows[j]["Type_Amount"].ToString());
                    //    }
                    //    dt1.Rows.Add(distinctValues.Rows[i]["Type"].ToString(), distinctValues.Rows[i]["Type_Perc"].ToString(), distinctValues.Rows[i]["Description"].ToString(), Amount);
                    //    Amount = 0.0M;
                    //}
                    //Grid_Tax.DataSource = ds.Tables[0];
                    //Grid_Tax.DataBind();
                    //var query = from s in ds.Tables[0].AsEnumerable()
                    //            group s by new { Type = s.Field<string>("Type"), Type_Perc = s.Field<decimal>("Type_Perc"), Description = s.Field<string>("Description") }
                    //                into grp
                    //                orderby grp.Key.Type
                    //                select new
                    //                {
                    //                    Type = grp.Key.Type,
                    //                    Type_Perc = grp.Key.Type_Perc,
                    //                    Description = grp.Key.Description,
                    //                    Sum = grp.Sum(r => r.Field<decimal>("Type_Amount")),
                    //                };
                    //DataTable dt1 = new DataTable();
                    //dt1.Columns.Add("Type", typeof(string));
                    //dt1.Columns.Add("Type_Perc", typeof(decimal));
                    //dt1.Columns.Add("Description", typeof(string));
                    //dt1.Columns.Add("Type_Amount", typeof(decimal));
                    //foreach (var t in query)
                    //{
                    //    dt1.Rows.Add(t.Type, t.Type_Perc, t.Description, t.Sum);
                    //}
                    Grid_Tax.DataSource = filterdt;
                    Grid_Tax.DataBind();
                }
                else
                {
                    Grid_Tax.DataSource = null;
                    Grid_Tax.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ddlIndentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlIndentNo.SelectedIndex != 0)
        {
            BindIndentItems();
            GetTINNoFromLocation();
        }
    }

    protected void Grid_Tax_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                // NetTotalAmt = Convert.ToDecimal(e.Row.Cells[4].Text);
                if (e.Row.Cells[0].Text.ToString().ToLower() == "discount" && ViewState["Grand Total"] != null && Convert.ToDecimal(ViewState["Grand Total"]) != Convert.ToDecimal("0.00"))
                {
                    ViewState["Grand Total"] = Convert.ToDecimal(ViewState["Grand Total"]) - Convert.ToDecimal(e.Row.Cells[3].Text);
                }
                else
                {
                    ViewState["Grand Total"] = Convert.ToDecimal(ViewState["Grand Total"]) + Convert.ToDecimal(e.Row.Cells[3].Text);
                }
                GridDataControlFieldCell cell = e.Row.Cells[4] as GridDataControlFieldCell;

                LinkButton lnkeditbtn = cell.FindControl("lnkEditamt") as LinkButton;
                if (e.Row.Cells[0].Text.ToString().ToLower() == "transport")
                {
                    lnkeditbtn.Visible = true;
                }
                else
                {
                    lnkeditbtn.Visible = false;
                }

            }
            else if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[0].Text = "Grand Total:";
                e.Row.Cells[3].Text = ViewState["Grand Total"] != null ? ViewState["Grand Total"].ToString() : "0";
                //if (Convert.ToDecimal(NetTotalAmt) == 0)
                //{
                //    e.Row.Cells[3].Text = txtTotal.Text.ToString();
                //}
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void BindIndentItems()
    {
        try
        {
            objPO = new PurchaseOrderBL();
            ds = new DataSet();
            objPO.IndentNo = ddlIndentNo.SelectedValue.ToString();
            objPO.VendorID = ddlVendor.SelectedValue;
            if (Request.QueryString["QuotationIndentNo"] != null && Request.QueryString["Vendor"] != null)
            {
                objPO.Flag = 0;
            }
            else
            {
                objPO.Flag = 1;
            }
            objPO.ProjectCode = ddlProject.SelectedValue.ToString();
            objPO.Task = "BindPOItemGrid";

            objPO.load(con, PurchaseOrderBL.eLoadSp.PROC_SELECT_PO_CREATING_ITEMS, ref ds);
            Grid_POItem.Columns[16].Visible = false;
            Grid_POItem.Columns[15].Visible = false;

          
            if (ds.Tables[0].Rows.Count > 0)
            {
                BindTaxItems(ds.Tables[0].Rows[0]["QuotationNo"].ToString());
                BindTaxWiseItems(ds.Tables[0].Rows[0]["QuotationNo"].ToString());
            }

            Grid_POItem.DataSource = null;
            Grid_POItem.DataBind();
            Total_Amount = 0;
            Grid_POItem.DataSource = ds;
            Grid_POItem.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindTaxWiseItems(string QutoationNo)
    {
        try
        {
            objPO = new PurchaseOrderBL();
            objPO.IndentNo = ddlIndentNo.SelectedValue.ToString();
            objPO.VendorID = ddlVendor.SelectedValue;
            objPO.ProjectCode = ddlProject.SelectedValue.ToString();
            objPO.Task = "BindPOItemTaxGrid";
            DataSet ds = new DataSet();

            if (objPO.load(con, PurchaseOrderBL.eLoadSp.PROC_SELECT_PO_CREATING_ITEMS, ref ds))
            {
                GridItemWiseTax.DataSource = ds.Tables[0];
                GridItemWiseTax.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void GetTINNoFromLocation()
    {
        objIndent = new IndentBL();
        ds = new DataSet();
        objIndent.Indent_No = ddlIndentNo.SelectedValue.ToString();
        objIndent.load(con, IndentBL.eLoadSp.SELECT_TINNO_FROM_LOCATION_BY_INDENTNO, ref ds);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["GST"].ToString() != string.Empty)
            {
                txtTINNo.Text = ds.Tables[0].Rows[0]["GST"].ToString();

                txtDespatchAdvise.Text = ds.Tables[0].Rows[0]["DespatchAdvise"].ToString();
            }
            //if (ds.Tables[0].Rows[0]["TIN"].ToString() != string.Empty)
            //{
            //    txtTINNo.Text = ds.Tables[0].Rows[0]["TIN"].ToString();
            //    txtDespatchAdvise.Text = ds.Tables[0].Rows[0]["DespatchAdvise"].ToString();
            //}
            else
            {
                txtTINNo.Text = string.Empty;
            }
        }
    }

    private void BindPOItems()
    {
        try
        {
            objPO = new PurchaseOrderBL();
            ds = new DataSet();
            objPO.PO_ID = Convert.ToInt32(ViewState["POID"]);
            objPO.load(con, PurchaseOrderBL.eLoadSp.SELECT_PO_ITEMS_BY_PONO, ref ds);
            Total_Amount = 0;
            Grid_POItem.DataSource = ds;
            Grid_POItem.Columns[15].Visible = false;
            Grid_POItem.Columns[16].Visible = false;
            Grid_POItem.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_POItem_RowCreated(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if ( e.Row.RowType == Obout.Grid.GridRowType.Header)
            {
                Total_Amount = 0;
                ViewState["Grand Total"] = 0;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

   // decimal Totalbasicamt;
    protected void Grid_POItem_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[0].Text = "Total Amount";
                //ViewState["Grand Total"] = "0.00";
            }
           
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                if (!string.IsNullOrEmpty(e.Row.Cells[10].Text))
                {
                    ViewState["Grand Total"] =  Total_Amount += decimal.Parse(e.Row.Cells[11].Text);
                    //Totalbasicamt += decimal.Parse(e.Row.Cells[9].Text); 
                }
            }
            else if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[11].Text = Total_Amount.ToString();
                //ViewState["Grand Total"] = Total_Amount.ToString();
                txtTotal.Text = Total_Amount.ToString();
                Total_Amount = 0;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void UpdatePONetTotalAmount()
    {
        try
        {
            objPO = new PurchaseOrderBL();
            ds = new DataSet();
            objPO.PO_ID = Convert.ToInt32(ViewState["POID"].ToString());
            objPO.updatePONetToatl(con, PurchaseOrderBL.eLoadSp.UPDATE_PO_NETTOTAL);
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBUpdateError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void BindPOItemWiseTaxGrid()
    {
        try
        {
            objPO = new PurchaseOrderBL();
            ds = new DataSet();
            objPO.PO_ID = Convert.ToInt32(ViewState["POID"].ToString());
            objPO.load(con, PurchaseOrderBL.eLoadSp.SELECT_PO_ITEMWISE_TAX_BY_PO_ID, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridItemWiseTax.DataSource = ds.Tables[0];
                GridItemWiseTax.DataBind();
            }
            else
            {
                GridItemWiseTax.DataSource = ds.Tables[0];
                GridItemWiseTax.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void BindTaxGrid()
    {
        try
        {
            objPO = new PurchaseOrderBL();
            ds = new DataSet();
            DataTable filterdt;
            objPO.PO_ID = Convert.ToInt32(ViewState["POID"].ToString());
            objPO.load(con, PurchaseOrderBL.eLoadSp.SELECT_PO_TAX_BY_POID, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                filterdt = ds.Tables[0];

                foreach (DataRow row in filterdt.Rows)
                {
                    string type = row["Type"].ToString();
                    string value = row["Description"].ToString();
                    if (type == "Transport" && value == "Extra")
                    {
                        ViewState["TransportationExtraCostExits"] = true;
                    }
                    if (type == "Transport")
                    {
                        ViewState["TransportationExtraCostExits"] = true;
                    }
                }

                //DataTable dt1 = new DataTable();
                //dt1.Columns.Add("Type", typeof(string));
                //dt1.Columns.Add("Type_Perc", typeof(decimal));
                //dt1.Columns.Add("Description", typeof(string));
                //dt1.Columns.Add("Type_Amount", typeof(decimal));

                //DataView view = new DataView(ds.Tables[0]);
                //DataTable distinctValues = view.ToTable(true, "Type", "Type_Perc", "Description");

                //DataView dv = new DataView(ds.Tables[0]);

                //Decimal Amount = 0.0M;

                //for (int i = 0; i < distinctValues.Rows.Count; i++)
                //{
                //    dv.RowFilter = "Type = '" + distinctValues.Rows[i]["Type"].ToString() + "' and Type_Perc=" + distinctValues.Rows[i]["Type_Perc"].ToString() + " and Description='" + distinctValues.Rows[i]["Description"].ToString() + "'";
                //    for (int j = 0; j < dv.ToTable().Rows.Count; j++)
                //    {
                //        Amount = Amount + Convert.ToDecimal(dv.ToTable().Rows[j]["Type_Amount"].ToString());
                //    }
                //    dt1.Rows.Add(distinctValues.Rows[i]["Type"].ToString(), distinctValues.Rows[i]["Type_Perc"].ToString(), distinctValues.Rows[i]["Description"].ToString(), Amount);
                //    Amount = 0.0M;
                //}


                //var query = from s in ds.Tables[0].AsEnumerable()
                //            group s by new {  Type = s.Field<string>("Type"), Type_Perc = s.Field<decimal>("Type_Perc"), Description = s.Field<string>("Description") }

                //                into grp
                //                orderby grp.Key.Type ascending

                //                select new
                //                {
                //                    Type = grp.Key.Type,
                //                    Type_Perc = grp.Key.Type_Perc,
                //                    Description = grp.Key.Description,
                //                    Sum = grp.Sum(r => r.Field<decimal>("Type_Amount")),
                //                };

                //foreach (var t in query)
                //{
                //    dt1.Rows.Add(t.Type, t.Type_Perc, t.Description, t.Sum);
                //}

                Grid_Tax.DataSource = ds;
                Grid_Tax.DataBind();


                //         var groupbyfilter = from dr in ds.Tables["Order"].AsEnumerable()
                //                             group dr by dr["CustomerID"];
                //         ds.Tables[0] = ds.Tables[0].AsEnumerable()
                //.GroupBy(r => new { Col1 = r["Col1"], Col2 = r["Col2"] })
                //.Select(g => g.OrderBy(r => r["PK"]).First())
                //.CopyToDataTable();

            }
            else
            {
                Grid_Tax.DataSource = ds.Tables[0];
                Grid_Tax.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_POItem_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objPO = new PurchaseOrderBL();
            objPO.PO_Item_Id = Convert.ToInt32(e.Record["PO_Item_Id"]);

            if (objPO.delete(con, PurchaseOrderBL.eLoadSp.DELETE_PO_ITEM_BY_ID))
            {
                BindPOItems();
                BindPOItemWiseTaxGrid();
                //UpdatePONetTotalAmount();
                BindTaxGrid();

                Grid_POItem.Columns[16].Visible = true;
                Grid_POItem.Columns[15].Visible = true;

                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('PO Item has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete PO Item!.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_Tax_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objPO = new PurchaseOrderBL();
            objPO.PO_Tax_ID = Convert.ToInt32(e.Record["PO_Tax_ID"]);

            if (objPO.delete(con, PurchaseOrderBL.eLoadSp.DELETE_PO_TAX_BY_ID))
            {
                UpdatePONetTotalAmount();
                BindTaxGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('PO Tax Item has been deleted sucessfully.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../Procurement/PurchaseOrderList.aspx", false);
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnDecision_Click(object sender, EventArgs e)
    {
        try
        {

            objPO = new PurchaseOrderBL();

            ds = new DataSet();
            objPO.Approver_Com = txtComments.Text.Trim();
            objPO.ApprovalStatus = rdoStatus.SelectedValue.Trim();
            if (txtDate.Text != string.Empty)
            {
                objPO.Approval_Date = Convert.ToDateTime(txtDate.Text);
            }
            else
            {
                objPO.Approval_Date = null;
            }
            objPO.PONo = txtPONo.Text.Trim();
            if (objPO.UpdateApprovalPO(con, PurchaseOrderBL.eLoadSp.UPDATE_APPROVAL_POAWAITING))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Thanks for your Decision.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBUpdateError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    public void ResetControl()
    {
        txtComments.Text = string.Empty;
        rdoStatus.ClearSelection();
        txtDate.Text = string.Empty;
    }
    
    protected void btnApprovalCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../Procurement/POAwaitingApproval.aspx", false);
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVendor.SelectedIndex != 0)
        {
            ddlIndentNo_SelectedIndexChanged(null, null);
        }
    }

    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindIndentNo();
    }

    protected void Grid_Tax_RowCreated(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if ( e.Row.RowType == Obout.Grid.GridRowType.Header)
            {
                NetTotalAmt = 0;
                //ViewState["Grand Total"] = Totalbasicamt;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void lnkEditQty_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["POItemID"] = ((LinkButton)sender).CommandName.ToString();

            txtPOQty.Text = ((LinkButton)sender).CommandArgument.ToString();
            GetExistingQtyForThisIndent();
            ModelPOQty.Show();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void GetExistingQtyForThisIndent()
    {
        PurchaseOrderBL objQtyBL = new PurchaseOrderBL();
        DataSet dsPOQty = new DataSet();
        //if (ddlIndentNo.SelectedIndex != 0)
        //{

        objQtyBL.IndentNo = ddlIndentNo.SelectedValue;
        objQtyBL.PO_Item_Id = Convert.ToInt32(ViewState["POItemID"]);
        if (objQtyBL.load(con, PurchaseOrderBL.eLoadSp.SELECT_EXISTING_QTY_OF_INDENT, ref dsPOQty))
        {

            if (dsPOQty.Tables.Count > 0)
            {
                ViewState["ExistingPOQty"] = dsPOQty.Tables[0].Rows[0]["ExistingPOQtyOfIndent"].ToString();
                ViewState["IndentQty"] = dsPOQty.Tables[0].Rows[0]["IndentQty"].ToString();
                ViewState["PendingQty"] = dsPOQty.Tables[0].Rows[0]["PendingQty"].ToString();
                ViewState["POItemQty"] = dsPOQty.Tables[0].Rows[0]["POItemQty"].ToString();

            }
        }
        //}
    }

    protected void btnPoQtySave_Click(object sender, EventArgs e)
    {
        try
        {
            objPO = new PurchaseOrderBL();

            if (Convert.ToDecimal(txtPOQty.Text) > Convert.ToDecimal(ViewState["IndentQty"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('PO Qty Should not be greater than Indent Qty!.');", true);
                return;
            }
            else if (Convert.ToDecimal(txtPOQty.Text) > (Convert.ToDecimal(ViewState["PendingQty"])) + Convert.ToDecimal(ViewState["POItemQty"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('PO Qty Should not be greater than Remaining Qty!.');", true);
                return;
            }
            objPO.IndentNo = ddlIndentNo.SelectedItem.Text;
            objPO.VendorID = ddlVendor.SelectedValue;
            objPO.PO_ID = Convert.ToInt32(ViewState["POID"]);
            objPO.Qty_required = txtPOQty.Text != string.Empty ? Convert.ToDecimal(txtPOQty.Text.Trim()) : 0;
            objPO.PO_Item_Id = Convert.ToInt32(ViewState["POItemID"]);
            objPO.Flag = 0;

            if (objPO.updatePOItemQty(con, PurchaseOrderBL.eLoadSp.UPDATE_PO_ITEM_QTY_BY_ID))
            {
                BindPOItems();
                BindPOItemWiseTaxGrid();
                BindTaxGrid();

                Grid_POItem.Columns[16].Visible = true;
                Grid_POItem.Columns[15].Visible = true;

                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Quantity updated sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update the Quantity.');", true);
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBUpdateError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnUpdateOverallAmt_Click(object sender, EventArgs e)
    {
        objPO = new PurchaseOrderBL();
        if (txtOverallTaxAmount.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please enter a valid Amount');", true);
        }
        else
        {
            objPO.Amount = Convert.ToDecimal(txtOverallTaxAmount.Text);
            objPO.PO_Tax_ID = Convert.ToInt32(ViewState["PO_Tax_ID"]);
            if (objPO.updatePOOverallAmount(con, PurchaseOrderBL.eLoadSp.UPDATE_PO_ITEM_OVERALL_AMOUNT_BY_ID))
            {
                BindPOItems();
                BindPOItemWiseTaxGrid();
                //UpdatePONetTotalAmount();
                BindTaxGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Amount updated sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update the Amount.');", true);
            }   
        }
    }

    protected void lnkEditamt_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["PO_Tax_ID"] = ((LinkButton)sender).CommandName.ToString();
            txtOverallTaxAmount.Text = ((LinkButton)sender).CommandArgument.ToString();
            ModalOverallAmt.Show();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnPrintPDF_Click(object sender, EventArgs e)
    {
        try
        {
            string MsgBody = "";
            string Url = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/" + "Procurement/PO_Print_PDF.aspx?PONo=" + Request.QueryString["PONo"].ToString();
            MsgBody = getHTMLFromURL(Url);
            //SavePDF_PO(Url);
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private string getHTMLFromURL(string Url)
    {
        HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(Url);
        webrequest.Method = "GET";
        webrequest.ContentLength = 0;
        string result = "";
        WebResponse response = webrequest.GetResponse();

        using (StreamReader stream = new StreamReader(response.GetResponseStream()))
        {
            result = stream.ReadToEnd();
        }
        return result;
    }

    //string FileNametosave = null;
    //public void SavePDF_PO(string MsgBody)
    //{
    //    try
    //    {
    //        HtmlToPdf pdf = new HtmlToPdf();
    //        FileNametosave = "PO_" + Request.QueryString["PONo"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + ".pdf";

    //        string url = MsgBody + "&Pdf=1";
    //        if (url != "")
    //        {
    //            SelectPdf.PdfDocument doc = pdf.ConvertUrl(url);
    //            var dir = HttpContext.Current.Server.MapPath("~/Downloaded_PDF/");
    //            if (!Directory.Exists(dir))
    //                Directory.CreateDirectory(dir);
    //            var fileName = dir + FileNametosave.Replace("/", "-");
    //            doc.Save(fileName);
    //            doc.Close();
    //            // ScriptManager.RegisterStartupScript(this, this.GetType(), "showSaveMessage", "swal('Success', 'PDF File Saved Succcessfully', 'success');", true);
    //        }
    //        else
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "showSaveMessage", "swal('Alert', 'Purchase Order Print cannot be created.', 'alert');", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["ID"] != null ? Session["ID"].ToString() : string.Empty);
    //    }
    //}

    protected void chkDraft_ChckedChanged(object sender, EventArgs e)
    {
        if (chkDraft.Checked == true)
        {
            btnPrint.HRef = "PO_Print.aspx?PONo=" + Request.QueryString["PONo"].ToString()+"&Draft=1";
        }
        else
        {
            btnPrint.HRef = "PO_Print.aspx?PONo=" + Request.QueryString["PONo"].ToString();
        }
    }

    protected void lnkDownloadFile_Click(object sender, EventArgs e)
    {
        try
        {
            string filePath = Server.MapPath("~\\UploadedFiles\\" + (sender as LinkButton).Text.Replace("/", "-"));
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.Flush();
            Response.TransmitFile(filePath);
            //Response.WriteFile(filePath);
            Response.End();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }
}
