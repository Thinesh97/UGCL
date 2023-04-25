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

namespace SNC.Procurement
{
    public partial class RequestForQuotation : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        VendorBL objVendorBL = null;
        IndentBL objIndent = null;
        Category objCategory = null;
        //RequestForquotationBL objRFQ = null;
        RequestForquotationBL objRFQ = null;
        ProjectBL objProjectBL = null;
        DataSet ds = null;

        decimal Total_Amount = 0;
        decimal Total_Amount_WithTax = 0;
        decimal Total_Igst = 0;
        decimal Total_Cgst = 0;
        decimal Total_Sgst = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["UID"] != null)
                    {
                        BindVendorList();
                        BindUsersNames();
                        BindCategory();
                        BindProject();
                        btnApprove.Visible = false;
                        if (Request.QueryString["RFQNo"] != null)
                        {
                            GetRFQDetails(Request.QueryString["RFQNo"].ToString());
                            btnPrint.Visible = true;
                            btnPrint.Target = "_blank";
                            btnPrint.HRef = "RFQ_Print.aspx?RFQNo=" + Request.QueryString["RFQNo"].ToString();
                            div_Draft.Visible = true;
                            div_BeforeUpload.Visible = true;
                            btnPrintPDF.Visible = false;
                            btnAddItem.Visible = true;
                            btnAddTax.Visible = true;
                            btnSubmit.Text = "Update";

                        }

                        if (Request.QueryString["RFQNo"] != null && Request.QueryString["s"] != null)
                        {
                            GetRFQDetails(Request.QueryString["RFQNo"].ToString());
                            btnPrint.Visible = true;
                            btnPrint.Target = "_blank";
                            btnPrint.HRef = "RFQ_Print.aspx?RFQNo=" + Request.QueryString["RFQNo"].ToString();
                            div_Draft.Visible = true;
                            btnPrintPDF.Visible = false;
                            btnAddItem.Visible = true;
                            btnAddTax.Visible = true;
                            btnSubmit.Text = "Update";

                            string message = "Direct RFQ has been Created Successfully";
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
                            GetRFQDetails(Request.QueryString["PoApprovalID"].ToString());
                            btnPrint.Visible = true;
                            btnPrint.Target = "_blank";
                            btnPrint.HRef = "RFQ_Print.aspx?RFQNo=" + Request.QueryString["PoApprovalID"].ToString();
                            div_Draft.Visible = true;
                            btnPrintPDF.Visible = false;
                            btnSubmit.Visible = false;
                            btnCancel.Visible = true;
                            divPOapproval.Visible = true;
                            txtDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                        }

                    }
                    else
                    {
                        Response.Redirect("../CommonPages/Login.aspx", false);
                    }
                }
                decimal s = Total_Amount;
                calDecision.StartDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
                //  ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void BindCategory()
        {
            try
            {
                objCategory = new Category();
                DataSet ds = new DataSet();
                if (objCategory.load(con, Category.eLoadSp.SELECT_ALL, ref ds))
                {
                    if (ds.Tables[0].Rows.Count >= 0)
                    {
                        ddlCategory.DataSource = ds;
                        ddlCategory.DataTextField = "Category_Name";
                        ddlCategory.DataValueField = "Mat_cat_ID";
                        ddlCategory.DataBind();
                        ddlCategory.Items.Insert(0, "-Select-");
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVendor.SelectedIndex != 0)
            {
                objVendorBL = new VendorBL();
                ds = new DataSet();
                objVendorBL.Vendor_ID = ddlVendor.SelectedValue;
                objVendorBL.load(con, VendorBL.eLoadSp.SELECT_VENDORDETAILS_BY_ID, ref ds);
                if (ds.Tables.Count > 0)
                {
                    //txtDespatchAdvise.Text = ds.Tables[0].Rows[0]["Add_Line"].ToString();
                    //txtContactName.Text = ds.Tables[0].Rows[0]["Con_Person"].ToString();
                    //txtContactNo.Text = ds.Tables[0].Rows[0]["Con_No"].ToString();
                    txtTINNo.Text = ds.Tables[0].Rows[0]["Regs_No"].ToString();
                }
            }
            else
            {
                txtTINNo.Text = string.Empty;
                txtContactName.Text = string.Empty;
                txtContactNo.Text = string.Empty;
            }
        }

        private void GetRFQDetails(string RFQNo)
        {
            try
            {
                objRFQ = new RequestForquotationBL();
                ds = new DataSet();
                objRFQ.RFQNo = RFQNo;
                objRFQ.load(con, RequestForquotationBL.eLoadSp.SELECT_RFQDETAILS_BY_RFQNO, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtRFQNo.Text = ds.Tables[0].Rows[0]["RFQNo"].ToString();
                    txtRFQDate.Text = ds.Tables[0].Rows[0]["RFQDate"].ToString();
                    ddlProject.SelectedValue = ds.Tables[0].Rows[0]["Project_Code"].ToString();
                    txtDeliverySchedule.Text = ds.Tables[0].Rows[0]["DeliverySchedule"].ToString();
                    ddlVendor.SelectedValue = ds.Tables[0].Rows[0]["VendorID"].ToString();
                    if (ds.Tables[0].Rows[0]["FYear"].ToString() != string.Empty)
                    {
                        ddlFYear.SelectedValue = ds.Tables[0].Rows[0]["FYear"].ToString();
                        ddlFYear.Enabled = false;
                    }
                    txtDestination.Text = ds.Tables[0].Rows[0]["Destination"].ToString();
                    txtPlaceofDispatch.Text = ds.Tables[0].Rows[0]["Place_of_Dispatch"].ToString();
                    txtVendorRef.Text = ds.Tables[0].Rows[0]["VendorRef"].ToString();
                    txtDueDate.Text = ds.Tables[0].Rows[0]["Due_Date"].ToString();
                    ddlDispatchTo.SelectedItem.Text = ds.Tables[0].Rows[0]["DespatchAdvise"].ToString();
                    txtPayTerms.Text = ds.Tables[0].Rows[0]["PaymentTerms"].ToString();
                    ddlApprovedBy.SelectedValue = ds.Tables[0].Rows[0]["ApprovedBy"].ToString();
                    txtTINNo.Text = ds.Tables[0].Rows[0]["TIN_No"].ToString();
                    txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                    txtOthers.Text = ds.Tables[0].Rows[0]["Other_Terms"].ToString();
                    txtContactName.Text = ds.Tables[0].Rows[0]["Contact_Name"].ToString();
                    txtContactNo.Text = ds.Tables[0].Rows[0]["Contact_No"].ToString();
                    txtDispatchMode.Text = ds.Tables[0].Rows[0]["DispatchMode"].ToString();
                    ddlTDSPerc.SelectedValue = ds.Tables[0].Rows[0]["TDSPerc"].ToString();
                    lnkDownloadFile.Text = ds.Tables[0].Rows[0]["Uploaded_File"].ToString();
                    div_AfterUpload.Visible = ds.Tables[0].Rows[0]["Uploaded_File"].ToString() != string.Empty ? true : false;
                    ViewState["RFQID"] = ds.Tables[0].Rows[0]["RFQ_ID"].ToString();
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

                    if (ds.Tables[0].Rows[0]["ApprovalStatus"].ToString() == "1")
                    {
                        btnSubmit.Visible = false;
                    }
                    if (ds.Tables[0].Rows[0]["IsAprroved"].ToString() == "False")
                    {
                        chkDraft.Checked = true;
                        btnPrint.Visible = true;
                        btnApprove.Visible = true;
                    }
                    else
                    {
                    }
                    if (ds.Tables[0].Rows[0]["ApprovedBy"].ToString() == Session["UID"].ToString())
                    {
                        ApproverComments.Visible = true;
                    }
                    else
                    {
                        ApproverComments.Visible = false;
                        btnApprove.Visible = false;
                    }
                    BindRFQItems();
                    BindRFQTax();
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                objRFQ = new RequestForquotationBL();
                objRFQ.RFQDate = Convert.ToDateTime(txtRFQDate.Text.Trim());
                objRFQ.ProjectCode = ddlProject.SelectedValue.Trim();
                objRFQ.DeliverySchedule = txtDeliverySchedule.Text.Trim();
                objRFQ.VendorID = ddlVendor.SelectedValue.Trim();
                objRFQ.VendorRef = txtVendorRef.Text.Trim();
                //objRFQ.RFQNo = "2";
                if (txtDueDate.Text != string.Empty)
                {
                    objRFQ.DueDate = Convert.ToDateTime(txtDueDate.Text.Trim());
                }
                else
                {
                    objRFQ.DueDate = null;
                }
                objRFQ.Destination = txtDestination.Text.Trim();
                objRFQ.Place_of_Dispatch = txtPlaceofDispatch.Text.Trim();
                objRFQ.DespatchAdvise = ddlDispatchTo.SelectedItem.Text;
                objRFQ.ContactName = txtContactName.Text.Trim();
                objRFQ.ContactNo = txtContactNo.Text.Trim();
                objRFQ.PaymentTerms = txtPayTerms.Text;
                objRFQ.ApprovedBy = Convert.ToInt32(ddlApprovedBy.SelectedValue.Trim());
                objRFQ.Status = rblStatus.SelectedValue.Trim();
                objRFQ.TIN_No = txtTINNo.Text.Trim();
                objRFQ.Remarks = txtRemarks.Text.Trim();
                objRFQ.Others = txtOthers.Text;
                objRFQ.FYear = ddlFYear.SelectedValue.Trim();
                objRFQ.ProjectCode = ddlProject.SelectedValue.ToString();
                objRFQ.DispatchMode = txtDispatchMode.Text.Trim();
                objRFQ.Approver_Com = txtApproverComments.Text.Trim();
                objRFQ.TDSPerc = Convert.ToDecimal(ddlTDSPerc.SelectedValue);

                objRFQ.Task = "InsertRFQ";

                if (Session["UID"] != null)
                {
                    objRFQ.UserID = Convert.ToInt32(Session["UID"].ToString());
                }

                if (Request.QueryString["QuotationIndentNo"] != null && Request.QueryString["Vendor"] != null)
                {
                    objRFQ.Flag = 0;
                }
                else
                {
                    objRFQ.Flag = 1;
                }


                if (btnSubmit.Text == "Submit")
                {
                    if (rblStatus.SelectedValue != "Open")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select Open status for RFQ submission!.');", true);
                        return;
                    }
                    else
                    {
                        if (objRFQ.insert(con, RequestForquotationBL.eLoadSp.INSERT))
                        {
                            btnPrint.Visible = true;
                            btnPrint.HRef = "RFQ_Print.aspx?RFQNo=" + objRFQ.RFQNo.ToString();
                            btnPrint.Target = "_blank";
                            div_Draft.Visible = true;
                            div_BeforeUpload.Visible = true;
                            btnPrintPDF.Visible = false;
                            btnAddItem.Visible = true;
                            btnAddTax.Visible = true;
                            //objRFQ.RFQNo = txtRFQNo.Text.ToString();
                            btnSubmit.Text = "Update";

                            ddlVendor.Enabled = false;
                            txtRFQNo.Text = objRFQ.RFQNo.ToString();
                            txtRFQNo.Enabled = false;
                            ViewState["RFQNo"] = objRFQ.RFQNo.ToString();
                            ViewState["RFQ_ID"] = Convert.ToInt32(objRFQ.RFQ_ID);
                            Response.Redirect("RequestForQuotation.aspx?RFQNo=" + ViewState["RFQNo"].ToString() + "&s=1", false);

                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Direct RFQ details has been inserted sucessfully.');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to insert Direct RFQ details !.');", true);
                        }
                    }
                }
                else
                {
                    objRFQ.Task = "UpdateDirectRFQ";
                    objRFQ.RFQNo = txtRFQNo.Text.ToString();
                    if (fuDPODoc.HasFile)
                    {
                        objRFQ.Uploaded_File = "RFQ_" + txtRFQNo.Text.ToString() + ".pdf";
                    }

                    if (objRFQ.update(con, RequestForquotationBL.eLoadSp.UPDATE))
                    {
                        if (fuDPODoc.HasFile)
                        {
                            fuDPODoc.SaveAs(Server.MapPath("~\\UploadedFiles\\RFQ_" + txtRFQNo.Text.Replace("/", "-") + ".pdf"));
                            lnkDownloadFile.Text = "RFQ_" + txtRFQNo.Text + ".pdf";
                            div_AfterUpload.Visible = true;
                        }
                        btnPrint.Visible = true;
                        btnPrint.HRef = "RFQ_Print.aspx?RFQNo=" + Request.QueryString["RFQNo"].ToString();
                        btnPrint.Target = "_blank";
                        div_Draft.Visible = true;
                        div_BeforeUpload.Visible = true;
                        btnPrintPDF.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('RFQ details has been updated sucessfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('RFQ amount is exceeding the Budget amount !.');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                //  ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("../Procurement/RequestForQuotationList.aspx", false);
            }
            catch (Exception ex)
            {
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            ModelItemPopup.Show();
            txtQty.Enabled = true;
            txtPrice.Enabled = true;
            btnSaveItem.Text = "Save";
            lblAddItems.Text = "Add Item";
            Clear();
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                objRFQ = new RequestForquotationBL();
                objRFQ.IsApproved = true;
                objRFQ.Task = "UpdateApprovelStatus";
                objRFQ.PONo = Request.QueryString["PONo"];
                objRFQ.Approver_Com = txtApproverComments.Text;
                if (objRFQ.load(con, RequestForquotationBL.eLoadSp.UPDATE_APROVEL_STAUSDPO))
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('DPO has been Approved sucessfully.');", true);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCategory.SelectedIndex != 0)
                {
                    objIndent = new IndentBL();
                    DataSet ds = new DataSet();
                    objIndent.Mat_Cat_Id = Convert.ToUInt16(ddlCategory.SelectedValue.Trim());
                    objIndent.load(con, IndentBL.eLoadSp.SELECT_ITEMCODE_BY_CATEGORY_ID, ref ds);
                    ddlItem.DataSource = ds;
                    ddlItem.DataValueField = "Item_Code";
                    ddlItem.DataTextField = "Item_Name";
                    ddlItem.DataBind();
                    ddlItem.Items.Insert(0, "-Select-");
                }
                else
                {
                    ddlItem.Items.Insert(0, "-Select-");
                }

                ModelItemPopup.Show();
            }
            catch (Exception ex)
            {
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objIndent = new IndentBL();
                objIndent.Item_Code = ddlItem.SelectedValue.Trim();
                if (objIndent.load(con, IndentBL.eLoadSp.SELECT_UOM_BY_ITEMCODE))
                {
                    txtUOM.Text = objIndent.UOM.ToString();
                }
                else
                {
                    txtUOM.Text = string.Empty;
                }

                ModelItemPopup.Show();
            }
            catch (Exception ex)
            {
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void btnCancelItem_Click(object sender, EventArgs e)
        {
            Clear();
            lblAddItems.Text = "Add Item";
            btnSaveItem.Text = "Save";
            ModelItemPopup.Hide();
            txtQty.Enabled = true;
            txtPrice.Enabled = true;
        }

        protected void btnSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                objRFQ = new RequestForquotationBL();
                objRFQ.RFQNo = txtRFQNo.Text;
                objRFQ.Mat_Cat_Id = Convert.ToInt32(ddlCategory.SelectedValue);
                objRFQ.Item_Code = ddlItem.SelectedValue;
                if (txtQty.Text == string.Empty || txtQty.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please Enter Qty');", true);
                    return;
                }
                else
                {
                    objRFQ.Qty = Convert.ToDecimal(txtQty.Text);
                }

                if (txtPrice.Text == string.Empty || txtPrice.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please Enter Price');", true);
                    return;
                }
                else
                {
                    objRFQ.Price = Convert.ToDecimal(txtPrice.Text);
                }
                if (txtAmount.Text != string.Empty && txtAmount.Text != "0")
                {
                    objRFQ.Amount = Convert.ToDecimal(txtAmount.Text);
                }
                objRFQ.Amountrfq = (Convert.ToDecimal(txtPrice.Text)) * (Convert.ToDecimal(txtQty.Text));
                objRFQ.Igstrfq_Perc = Convert.ToDecimal(txtIgstPerc.Text.Trim());
                objRFQ.Cgstrfq_Perc = Convert.ToDecimal(txtCgstPerc.Text.Trim());
                objRFQ.Sgstrfq_Perc = Convert.ToDecimal(txtSgstPerc.Text.Trim());
                objRFQ.RFQ_Item_Id = Convert.ToInt32(Session["DPR_Item_Id"]);
                if (btnSaveItem.Text == "Save")
                {
                    objRFQ.Task = "InsertRFQItem";
                    if (objRFQ.DirectRFQItemInsertUpdate(con, RequestForquotationBL.eLoadSp.INSERT_DIRECT_RFQ_ITEM))
                    {
                        BindRFQItems();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('RFQ Item has been added successfully');", true);
                        Clear();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Item already exists for this RFQ.!!!');", true);
                    }
                }
                else
                {
                    objRFQ.Task = "UpdateRFQItem";
                    if (objRFQ.DirectRFQItemInsertUpdate(con, RequestForquotationBL.eLoadSp.UPDATE_DIRECT_RFQ_ITEM))
                    {
                        BindRFQItems();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('RFQ Item details has been updated successfully');", true);
                        btnSaveItem.Text = "Save";
                        lblAddItems.Text = "Add Item";
                        Clear();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update the RFQ Item details');", true);
                    }
                }
                ModelItemPopup.Hide();
            }
            catch (Exception ex)
            {
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBUpdateError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void Clear()
        {
            ddlCategory.SelectedIndex = -1;
            ddlItem.Items.Clear();
            ddlItem.Items.Insert(0, "-Select-");
            ddlItem.SelectedIndex = -1;
            txtUOM.Text = string.Empty;
            txtQty.Text = "0.00";
            txtAmount.Text = "0.00";
            txtPrice.Text = "0.00";
            txtIgstPerc.Text = "0.00";
            txtIgstAmt.Text = "0.00";
            txtCgstPerc.Text = "0.00";
            txtCgstAmt.Text = "0.00";
            txtSgstPerc.Text = "0.00";
            txtSgstAmt.Text = "0.00";
        }

        private void BindRFQItems()
        {
            try
            {
                objRFQ = new RequestForquotationBL();
                ds = new DataSet();
                objRFQ.RFQ_ID = Convert.ToInt32(ViewState["RFQID"]);
                objRFQ.load(con, RequestForquotationBL.eLoadSp.SELECT_RFQ_ITEMS_BY_RFQNO, ref ds);
                Total_Amount = 0;
                Grid_DirectRFQItem.DataSource = ds;
                Grid_DirectRFQItem.DataBind();
            }
            catch (Exception ex)
            {
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void Grid_DirectRFQItem_RowCreated(object sender, Obout.Grid.GridRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == Obout.Grid.GridRowType.Header)
                {
                    Total_Amount = 0;
                    Total_Igst = 0;
                    Total_Cgst = 0;
                    Total_Sgst = 0;
                    Total_Amount_WithTax = 0;
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void Grid_DirectRFQItem_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
                {
                    if (!string.IsNullOrEmpty(e.Row.Cells[5].Text))
                    {
                        Total_Amount += decimal.Parse(e.Row.Cells[5].Text);
                    }
                    if (!string.IsNullOrEmpty(e.Row.Cells[6].Text))
                    {
                        Total_Igst += decimal.Parse(e.Row.Cells[6].Text);
                    }
                    if (!string.IsNullOrEmpty(e.Row.Cells[7].Text))
                    {
                        Total_Cgst += decimal.Parse(e.Row.Cells[7].Text);
                    }
                    if (!string.IsNullOrEmpty(e.Row.Cells[8].Text))
                    {
                        Total_Sgst += decimal.Parse(e.Row.Cells[8].Text);
                    }
                }
                else if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
                {
                    e.Row.Cells[0].Text = "Total Amount";
                    e.Row.Cells[5].Text = Total_Amount.ToString();
                    e.Row.Cells[6].Text = Total_Igst.ToString();
                    e.Row.Cells[7].Text = Total_Cgst.ToString();
                    e.Row.Cells[8].Text = Total_Sgst.ToString();
                    Total_Amount_WithTax = Total_Amount + Total_Igst + Total_Cgst + Total_Sgst;
                    txtTotal.Text = Total_Amount_WithTax.ToString();

                    Total_Amount = 0;
                    Total_Igst = 0;
                    Total_Cgst = 0;
                    Total_Sgst = 0;
                    Total_Amount_WithTax = 0;
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void Grid_DirectRFQItem_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            try
            {
                objRFQ = new RequestForquotationBL();
                objRFQ.RFQ_Item_Id = Convert.ToInt32(e.Record["RFQ_Item_Id"]);
                objRFQ.Task = "DeleteRFQItem";
                if (objRFQ.DirectRFQItemInsertUpdate(con, RequestForquotationBL.eLoadSp.DELETE_DIRECT_RFQ_ITEM))
                {
                    BindRFQItems();

                    //Grid_DirectPOItem.Columns[16].Visible = true;
                    //Grid_DirectPOItem.Columns[15].Visible = true;

                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('RFQ Item has been deleted sucessfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete RFQ Item!.');", true);
                }
            }
            catch (Exception ex)
            {
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void lnkWOItem_Click(object sender, EventArgs e)
        {
            try
            {
                objRFQ = new RequestForquotationBL();
                ds = new DataSet();
                objRFQ.RFQ_Item_Id = Convert.ToInt32(((LinkButton)sender).CommandName);
                Session["DPR_Item_Id"] = Convert.ToInt32(((LinkButton)sender).CommandName);
                if (objRFQ.load(con, RequestForquotationBL.eLoadSp.SELECT_RFQ_ITEM_BY_ITEM_ID, ref ds))
                {
                    ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["Mat_Cat_Id"].ToString();
                    ddlCategory_SelectedIndexChanged(null, null);
                    ddlItem.SelectedValue = ds.Tables[0].Rows[0]["Item_Code"].ToString();
                    txtUOM.Text = ds.Tables[0].Rows[0]["UOM"].ToString();
                    txtQty.Text = ds.Tables[0].Rows[0]["Qty_required"].ToString();
                    txtPrice.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
                    txtAmount.Text = ds.Tables[0].Rows[0]["Amount"].ToString();

                    txtIgstPerc.Text = ds.Tables[0].Rows[0]["Igstrfq_Perc"].ToString() == "" ? "0.00" : ds.Tables[0].Rows[0]["Igstrfq_Perc"].ToString();
                    txtIgstAmt.Text = ds.Tables[0].Rows[0]["Igstrfq_Amt"].ToString() == "" ? "0.00" : ds.Tables[0].Rows[0]["Igstrfq_Amt"].ToString();
                    txtCgstPerc.Text = ds.Tables[0].Rows[0]["Cgstrfq_Perc"].ToString() == "" ? "0.00" : ds.Tables[0].Rows[0]["Cgst_Pfqerc"].ToString();
                    txtCgstAmt.Text = ds.Tables[0].Rows[0]["Cgstrfq_Amt"].ToString() == "" ? "0.00" : ds.Tables[0].Rows[0]["Cgstrfq_Amt"].ToString();
                    txtSgstPerc.Text = ds.Tables[0].Rows[0]["Sgstrfq_Perc"].ToString() == "" ? "0.00" : ds.Tables[0].Rows[0]["Sgstrfq_Perc"].ToString();
                    txtSgstAmt.Text = ds.Tables[0].Rows[0]["Sgstrfq_Amt"].ToString() == "" ? "0.00" : ds.Tables[0].Rows[0]["Sgstrfq_Amt"].ToString();
                    btnSaveItem.Text = "Update";
                    ModelItemPopup.Show();
                }
            }
            catch (Exception ex)
            {
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }



        protected void btnAddTax_Click(object sender, EventArgs e)
        {
            decimal s = Total_Amount;
            ModelTaxPopup.Show();
            btnSaveTax.Text = "Save";
            ClearTax();
        }

        protected void rbtntype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtntype.SelectedValue == "IGST")
                {
                    div_Igst.Visible = true;
                    div_Cgst.Visible = false;
                    div_Sgst.Visible = false;
                    div_Packing.Visible = false;
                    div_Transport.Visible = false;
                }
                else if (rbtntype.SelectedValue == "CGST")
                {
                    div_Igst.Visible = false;
                    div_Cgst.Visible = true;
                    div_Sgst.Visible = false;
                    div_Packing.Visible = false;
                    div_Transport.Visible = false;
                }
                else if (rbtntype.SelectedValue == "SGST")
                {
                    div_Igst.Visible = false;
                    div_Cgst.Visible = false;
                    div_Sgst.Visible = true;
                    div_Packing.Visible = false;
                    div_Transport.Visible = false;
                }
                else if (rbtntype.SelectedValue == "Transport")
                {
                    div_Igst.Visible = false;
                    div_Cgst.Visible = false;
                    div_Sgst.Visible = false;
                    div_Packing.Visible = false;
                    div_Transport.Visible = true;
                }
                else if (rbtntype.SelectedValue == "PackingForwardingCost")
                {
                    div_Igst.Visible = false;
                    div_Cgst.Visible = false;
                    div_Sgst.Visible = false;
                    div_Packing.Visible = true;
                    div_Transport.Visible = false;
                }
                ModelTaxPopup.Show();
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void btnSaveTax_Click(object sender, EventArgs e)
        {
            try
            {
                objRFQ = new RequestForquotationBL();
                objRFQ.RFQNo = txtRFQNo.Text;
                objRFQ.TaxType = rbtntype.SelectedValue;
                if (rbtntype.SelectedValue == "IGST")
                {
                    objRFQ.Igstrfq_Perc = Convert.ToDecimal(txtIgstPercPO.Text.Trim());
                    objRFQ.Igstrfq_Amt = Convert.ToDecimal(txtTotal.Text.Trim()) * Convert.ToDecimal(txtIgstPercPO.Text.Trim()) / 100;
                }
                else if (rbtntype.SelectedValue == "CGST")
                {
                    objRFQ.Cgstrfq_Perc = Convert.ToDecimal(txtCgstPercPO.Text.Trim());
                    objRFQ.Cgstrfq_Amt = Convert.ToDecimal(txtTotal.Text.Trim()) * Convert.ToDecimal(txtCgstPercPO.Text.Trim()) / 100;
                }
                else if (rbtntype.SelectedValue == "SGST")
                {
                    objRFQ.Sgstrfq_Perc = Convert.ToDecimal(txtSgstPercPO.Text.Trim());
                    objRFQ.Sgstrfq_Amt = Convert.ToDecimal(txtTotal.Text.Trim()) * Convert.ToDecimal(txtSgstPercPO.Text.Trim()) / 100;
                }
                else if (rbtntype.SelectedValue == "PackingForwardingCost")
                {
                    objRFQ.PackingCost = Convert.ToDecimal(txtPackingCost.Text.Trim());
                }
                else if (rbtntype.SelectedValue == "Transport")
                {
                    objRFQ.TransportCost = Convert.ToDecimal(txtTransportCost.Text.Trim());
                }

                if (btnSaveTax.Text == "Save")
                {
                    objRFQ.Task = "InsertRFQTax";
                    if (objRFQ.DirectRFQTaxInsertUpdate(con, RequestForquotationBL.eLoadSp.INSERT_DIRECT_RFQ_TAX))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render(' Tax details has been added sucessfully.');", true);
                        ClearTax();
                        BindRFQTax();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Entry already exists for this PO.');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void btnCancelTax_Click(object sender, EventArgs e)
        {
            ClearTax();
            btnSaveTax.Text = "Save";
            ModelTaxPopup.Hide();
        }

        protected void ClearTax()
        {
            txtIgstPercPO.Text = "0.00";
            txtIgstAmtPO.Text = "0.00";
            txtCgstPercPO.Text = "0.00";
            txtCgstAmtPO.Text = "0.00";
            txtSgstPercPO.Text = "0.00";
            txtSgstAmtPO.Text = "0.00";
            txtPackingCost.Text = "0.00";
            txtTransportCost.Text = "0.00";
        }

        private void BindRFQTax()
        {
            try
            {
                objRFQ = new RequestForquotationBL();
                ds = new DataSet();
                objRFQ.RFQ_ID = Convert.ToInt32(ViewState["RFQID"]);
                objRFQ.load(con, RequestForquotationBL.eLoadSp.SELECT_RFQ_TAX_BY_RFQID, ref ds);
                Grid_DirectRFQTax.DataSource = ds;
                Grid_DirectRFQTax.DataBind();
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void Grid_DirectRFQTax_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            try
            {
                objRFQ = new RequestForquotationBL();
                objRFQ.RFQ_Tax_ID = Convert.ToInt32(e.Record["RFQ_Tax_Id"]);
                objRFQ.Task = "DeleteRFQTax";
                if (objRFQ.DirectRFQTaxInsertUpdate(con, RequestForquotationBL.eLoadSp.DELETE_DIRECT_RFQ_TAX))
                {
                    BindRFQTax();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('PO Tax has been deleted sucessfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete PO Tax !.');", true);
                }
            }
            catch (Exception ex)
            {
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
                //  ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "showSaveMessage", "swal('Alert', 'Direct Purchase Order Print cannot be created.', 'alert');", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["ID"] != null ? Session["ID"].ToString() : string.Empty);
        //    }
        //}


        //Extra code which are not used for UGCL
        protected void btnDecision_Click(object sender, EventArgs e)
        {
            try
            {
                objRFQ = new RequestForquotationBL();
                ds = new DataSet();
                objRFQ.Approver_Com = txtComments.Text.Trim();
                objRFQ.ApprovalStatus = rdoStatus.SelectedValue.Trim();
                if (txtDate.Text != string.Empty)
                {
                    objRFQ.Approval_Date = Convert.ToDateTime(txtDate.Text);
                }
                else
                {
                    objRFQ.Approval_Date = null;
                }
                objRFQ.PONo = txtRFQNo.Text.Trim();
                if (objRFQ.UpdateApprovalPO(con, RequestForquotationBL.eLoadSp.UPDATE_APPROVAL_POAWAITING))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Thanks for your Decision.');", true);
                }
            }
            catch (Exception ex)
            {
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBUpdateError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void chkDraft_ChckedChanged(object sender, EventArgs e)
        {
            if (chkDraft.Checked == true)
            {
                btnPrint.HRef = "PO_Print.aspx?PONo=" + Request.QueryString["PONo"].ToString() + "&Draft=1";
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
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
            }
        }
        protected void BindDispatchToList()
        {
            try
            {
                ds = new DataSet();
                objProjectBL = new ProjectBL();
                objProjectBL.Task = "Select_All_Sitelocation_By_Project_Code";
                if (ddlProject.SelectedValue != "-Select-")
                {
                    objProjectBL.Proj_Code = ddlProject.SelectedValue;
                }
                objProjectBL.load(con, ProjectBL.eLoadSp.SITE_LOCATION_OPERATIONS, ref ds);
                ddlDispatchTo.DataSource = ds;
                ddlDispatchTo.DataTextField = "SiteAddress";
                ddlDispatchTo.DataValueField = "Site_ID";
                ddlDispatchTo.DataBind();
                ddlDispatchTo.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void ddlDispatchTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            objProjectBL = new ProjectBL();
            if (ddlDispatchTo.SelectedValue != "-Select-")
            {
                objProjectBL.Site_ID = Convert.ToInt32(ddlDispatchTo.SelectedValue.ToString());
            }
            objProjectBL.Task = "Select_All_Sitelocation_By_Site_ID";
            objProjectBL.load(con, ProjectBL.eLoadSp.SITE_LOCATION_OPERATIONS, ref ds);
            if (ds.Tables.Count > 0)
            {

                txtContactName.Text = ds.Tables[0].Rows[0]["SiteContactPerson"].ToString();
                txtContactNo.Text = ds.Tables[0].Rows[0]["SiteMobileNumber"].ToString();
            }

            else
            {
                txtContactName.Text = string.Empty;
                txtContactNo.Text = string.Empty;
            }
        }
        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProject.SelectedValue != "-Select-")
            {
                BindDispatchToList();
            }

        }
    }
}