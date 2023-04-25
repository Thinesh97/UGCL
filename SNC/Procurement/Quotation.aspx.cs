using BusinessLayer;
using SNC.ErrorLogger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Quotation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    VendorBL objVendorBL = null;
    QuotationBL objQuotationBL = null;
    Category objCategory = null;
    IndentBL objIndent = null;
    DataSet ds = null;
    bool allow = true;

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    calQuotationDate.EndDate = DateTime.Now;
                    BindCategory();
                    BindVendor();
                    BindIndentNo();
                    if (Request.QueryString["ID"] != null)
                    {
                        GetQuotationDetails(Request.QueryString["ID"].ToString());
                        BindTaxItems();
                        BindTaxWiseItems();

                    }
                    if (Request.QueryString["RefID"] != null)
                    {
                        GetQuotationDetails(Request.QueryString["RefID"].ToString());
                        BindTaxItems();
                        BindTaxWiseItems();
                        btnAddItem.Visible = false;
                        btnSubmit.Visible = false;
                        btnAddTax.Visible = false;

                    }
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

    private void BindIndentNo()
    {
        try
        {
            ds = new DataSet();
            objIndent = new IndentBL();
            objIndent.Project_Code = Session["Project_Code"].ToString();
            objIndent.load(con, IndentBL.eLoadSp.SELECT_INDENT_FOR_QUOTATIONCOMPARE, ref ds);
            ddlIndentNo.DataSource = ds;
            ddlIndentNo.DataTextField = "Indent_No";
            ddlIndentNo.DataValueField = "Indent_No";
            ddlIndentNo.DataBind();
            ddlIndentNo.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindTaxWiseItems()
    {

        try
        {
            objQuotationBL = new QuotationBL();
            DataSet ds = new DataSet();
            objQuotationBL.QuotationNo = txtQuotation.Text;
            if (objQuotationBL.load(con, QuotationBL.eLoadSp.ItemWiseTaxSelect, ref ds))
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

    protected void BindTaxItems()
    {

        try
        {
            objQuotationBL = new QuotationBL();
            DataSet ds = new DataSet();
            objQuotationBL.QuotationNo = txtQuotation.Text;
            if (objQuotationBL.load(con, QuotationBL.eLoadSp.TaxItemsSelectionByID, ref ds))
            {
                if (ds.Tables[0].Rows.Count >= 0)
                {
                    Grid_Tax.DataSource = ds.Tables[0];
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


    protected void BindVendor()
    {
        try
        {
            objVendorBL = new VendorBL();
            DataSet ds = new DataSet();
            objVendorBL.load(con, VendorBL.eLoadSp.SELECT_VENDOR_ALL, ref ds);
            ddlVendor.DataSource = ds;
            ddlVendor.DataValueField = "Vendor_ID";
            ddlVendor.DataTextField = "Vendor_name";
            ddlVendor.DataBind();
            ddlVendor.Items.Insert(0, "-Select-");

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }


    protected void GetQuotationDetails(string QuotationNo)
    {
        try
        {
            objQuotationBL = new QuotationBL();
            DataSet ds = new DataSet();

            objQuotationBL.QuotationNo = QuotationNo;
            objQuotationBL.load(con, QuotationBL.eLoadSp.SELECT_QUOTATIONDETAILS_BY_ID, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtQuotation.Text = ds.Tables[0].Rows[0]["QuotationNo"].ToString();
                txtQuotationDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["QuotationDate"]).ToString("dd-MM-yyyy");
                ddlVendor.SelectedValue = ds.Tables[0].Rows[0]["VendorID"].ToString();
                txtVendorCode.Text = ds.Tables[0].Rows[0]["VendorID"].ToString();
                txtSaleEmployee.Text = ds.Tables[0].Rows[0]["SalesEmp"].ToString();
                txtPayment.Text = ds.Tables[0].Rows[0]["PaymentTerms"].ToString();
                txtDelivery.Text = ds.Tables[0].Rows[0]["Delivery"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
           
                txtQuotation.Enabled = false;
                if (ds.Tables[0].Rows[0]["Specific_quotation"].ToString() != string.Empty)
                {
                    chkspecificquotation.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Specific_quotation"].ToString());
                }
                else
                {
                    chkspecificquotation.Checked = false;
                }
                btnSubmit.Text = "Update";
                btnCancel.Text = "Cancel";
                HideGrid.Visible = true;
                btnAddItem.Visible = true;
                BindItem();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Clear()
    {
        ddlCategory.SelectedIndex = -1;
        ddlItem.Items.Clear();
        ddlItem.Items.Insert(0, "-Select-");
        ddlItem.SelectedIndex = -1;
        txtQty.Text = "0.00";
        txtAmount.Text = "0.00";
        txtPrice.Text = "0.00";
        txtUOM.Text = string.Empty;       
        
    }

    protected void Clear2()
    {
        txtQuotation.Text = "";
        txtQuotationDate.Text = "";
        ddlVendor.SelectedIndex = -1;
        txtVendorCode.Text = string.Empty;
        txtSaleEmployee.Text = "";
        txtPayment.Text = "";
        txtUOM.Text = "";
        txtDelivery.Text = "";

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objQuotationBL = new QuotationBL();
            objQuotationBL.QuotationNo = txtQuotation.Text;
            objQuotationBL.QuotationDate = Convert.ToDateTime(txtQuotationDate.Text);
            objQuotationBL.VendorID = ddlVendor.SelectedValue;
            objQuotationBL.SalesEmp = txtSaleEmployee.Text.Trim();
            objQuotationBL.PaymentTerms = txtPayment.Text.Trim();
            objQuotationBL.Delivery = txtDelivery.Text.Trim();
            objQuotationBL.Remarks = txtRemarks.Text.Trim();
            objQuotationBL.IndentNo = ddlIndentNo.SelectedIndex != 0 ? ddlIndentNo.SelectedValue : string.Empty;
            objQuotationBL.Project_Code = Session["Project_Code"].ToString();
            objQuotationBL.Specific_quotation = chkspecificquotation.Checked;

            if (btnSubmit.Text == "Submit")
            {
                if (objQuotationBL.INSERT(con, QuotationBL.eLoadSp.INSERT))
                {
                    chkReferIndent.Checked = false;
                    ddlIndentNo.SelectedIndex = 0;
                    chkReferIndent_CheckedChanged(null, null);

                    btnAddItem.Visible = true;
                    btnSubmit.Text = "Update";
                    btnCancel.Text = "Cancel";
                    txtQuotation.Enabled = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Quotation details has been saved successfully');", true);
                    BindItem();
                    //BindIndentItems();
                    QuotationItemGrid.Columns[8].Visible = true;
                    QuotationItemGrid.Columns[9].Visible = true;
                    QuotationItemGrid.Columns[10].Visible = true;
                    btnAddItem.Visible = true;                  

                }
                else
                {
                    
                    string msg = objQuotationBL.OutputResult.ToString();

                    if(msg != null)
                    {
                        if (msg == "QuotationNoExists")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Quotation No already exists.!!!!');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Quotation already exists for this Vendor on same date!');", true);
                        }     
                    }                                  
                }
            }
            else
            {
                if (objQuotationBL.update(con, QuotationBL.eLoadSp.UPDATE))
                {
                    chkReferIndent.Checked = false;
                    ddlIndentNo.SelectedIndex = 0;
                    chkReferIndent_CheckedChanged(null, null);

                    BindItem();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Quotation details has been updated successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update the quotation details');", true);
                }

            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void BindCategory()
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
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlVendor.SelectedIndex != 0)
            {
                objVendorBL = new VendorBL();
                ds = new DataSet();
                objVendorBL.Vendor_ID = ddlVendor.SelectedValue;
                objVendorBL.load(con, VendorBL.eLoadSp.SELECT_VENDORDETAILS_BY_ID, ref ds);
                if (ds.Tables.Count > 0)
                {
                    txtVendorCode.Text = ddlVendor.SelectedValue;
                    txtSaleEmployee.Text = ds.Tables[0].Rows[0]["Con_Person"].ToString();
                }
            }
            else
            {
                txtVendorCode.Text = string.Empty;
                txtSaleEmployee.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }


    protected void btnSaveItem_Click(object sender, EventArgs e)
    {
        try
        {
            objQuotationBL = new QuotationBL();
            
           
            objQuotationBL.QuotationNo = txtQuotation.Text;
            objQuotationBL.VendorID = ddlVendor.SelectedValue;
            objQuotationBL.QuotationDate = Convert.ToDateTime(txtQuotationDate.Text);
            objQuotationBL.CateID = Convert.ToInt32(ddlCategory.SelectedValue);
            objQuotationBL.ItemCode = ddlItem.SelectedValue;
            if (txtQty.Text == string.Empty || txtQty.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please Enter Qty');", true);
                return;
            }
            else
            {
                objQuotationBL.Qty = Convert.ToDecimal(txtQty.Text);
            }

            if (txtPrice.Text == string.Empty || txtPrice.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please Enter Price');", true);
                return;
            }
            else
            {
                objQuotationBL.Price = Convert.ToDecimal(txtPrice.Text);
            }

          
            objQuotationBL.Amt = Convert.ToDecimal(txtAmount.Text);
            if (btnSaveItem.Text == "Save")
            {
                if (objQuotationBL.InsertItem(con, QuotationBL.eLoadSp.ItemINERT))
                {
                    HideGrid.Visible = true;
                    BindItem();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Quotation Item has been added successfully');", true);
                    Clear();

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Item already exists for this Quotation.!!!');", true);
                }
            }

            else
            {
                objQuotationBL.QuotationItemID = Convert.ToInt32(ViewState["QuatationItemID"]);
                if (objQuotationBL.ItemUpdate(con, QuotationBL.eLoadSp.ItemUpdateNEW))
                {
                    BindItem();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Quotation Item details has been updated successfully');", true);
                    btnSaveItem.Text = "Save";
                    lblAddItems.Text = "Add Item";
                    Clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update the Item details');", true);
                }


            }
            BindTaxItems();
            ModelItemPopup.Hide();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBUpdateError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }


    protected void BindItem()
    {
        try
        {
            objQuotationBL = new QuotationBL();
            DataSet ds = new DataSet();
            objQuotationBL.QuotationNo = txtQuotation.Text;
            objQuotationBL.load(con, QuotationBL.eLoadSp.SELECT_QUOTATIONITEM_BY_QUTATIONID, ref ds);
            QuotationItemGrid.DataSource = ds;
            QuotationItemGrid.Columns[8].Visible = true;
            QuotationItemGrid.Columns[9].Visible = true;
            QuotationItemGrid.Columns[10].Visible = true;
            QuotationItemGrid.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                btnAddTax.Visible = true;

            }
            else
            {
                btnAddTax.Visible = false;

            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (((Button)sender).Text == "Clear")
        {
            Response.Redirect("../Procurement/Quotation.aspx");

        }
        if (((Button)sender).Text == "Cancel")
        {
            Response.Redirect("../Procurement/QuotationList.aspx");
        }
        else
        {
            Clear();
        }
    }

    decimal Qty = 0;
    decimal Price = 0;   
    decimal Amt = 0;
    decimal AmtWithTax = 0;
    decimal NetTotal = 0;

    protected void QuotationItemGrid_RowCreated(object sender, Obout.Grid.GridRowEventArgs e)
    {

        try
        {
            if (IsPostBack && e.Row.RowType == Obout.Grid.GridRowType.Header)
            {
                Qty = 0;
                Price = 0;
                Amt = 0;
                AmtWithTax = 0;
                NetTotal = 0;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void QuotationItemGrid_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                Qty += decimal.Parse(e.Row.Cells[4].Text);
                Price += decimal.Parse(e.Row.Cells[5].Text);
                Amt += decimal.Parse(e.Row.Cells[6].Text);
                AmtWithTax += decimal.Parse(e.Row.Cells[7].Text);
                if (e.Row.Cells[11].Text != string.Empty)
                {
                    NetTotal = decimal.Parse(e.Row.Cells[11].Text);
                    txtNetTotal.Text = NetTotal.ToString();

                }
               
            }
            if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[1].Text = "Total";
                e.Row.Cells[4].Text = Qty.ToString();
                Qty = 0;
                e.Row.Cells[5].Text = Price.ToString();
                Price = 0;
                e.Row.Cells[6].Text = Amt.ToString();
                Amt = 0;
                e.Row.Cells[7].Text = AmtWithTax.ToString();
                txtTotalAmt.Text = AmtWithTax.ToString();
                AmtWithTax = 0;
               
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["QuotationNo"] != null ? Session["QuotationNo"].ToString() : string.Empty);
        }

    }

    protected void lnkQuotationItem_Click(object sender, EventArgs e)
    {
        try
        {

            objQuotationBL = new QuotationBL();
            DataSet dsGetQutItem = new DataSet();
            ViewState["QuatationItemID"] = ((LinkButton)sender).CommandName.ToString();
            objQuotationBL.QuotationItemID = Convert.ToInt32(((LinkButton)sender).CommandName.ToString());
            objQuotationBL.load(con, QuotationBL.eLoadSp.SELECT_QuotationItem_By_QuotationItemID, ref dsGetQutItem);
            if (dsGetQutItem.Tables[0].Rows.Count > 0)
            {

                ddlCategory.SelectedValue = dsGetQutItem.Tables[0].Rows[0]["CateID"].ToString();
                ddlCategory_SelectedIndexChanged(sender, e);

                ddlItem.SelectedValue = dsGetQutItem.Tables[0].Rows[0]["ItemCode"].ToString();
                ddlItem_SelectedIndexChanged(sender, e);

                txtQty.Text = dsGetQutItem.Tables[0].Rows[0]["Qty"].ToString();
                txtPrice.Text = dsGetQutItem.Tables[0].Rows[0]["Price"].ToString();               
                txtAmount.Text = dsGetQutItem.Tables[0].Rows[0]["Amt"].ToString();
                btnSaveItem.Text = "Update";
                ModelItemPopup.Show();
                lblAddItems.Text = "Update Item";

                if(Convert.ToInt16(dsGetQutItem.Tables[0].Rows[0]["CountOfItemTax"]) > 0)
                {
                    txtQty.Enabled = false;
                    txtPrice.Enabled = false;
                }
                else
                {
                    txtQty.Enabled = true;
                    txtPrice.Enabled = true;
                }
            
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["QuotationItemID"] != null ? Session["QuotationItemID"].ToString() : string.Empty);
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

    protected void rbtntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtntype.SelectedValue == "Transport")
            {
                percentamount.Visible = false;
                TransportCost.Visible = true;
                decsription.Visible = false;
                packandforwardcost.Visible = false;
            }
            else if (rbtntype.SelectedValue == "PackingForwardingCost")
            {
                percentamount.Visible = false;
                TransportCost.Visible = false;
                decsription.Visible = false;
                packandforwardcost.Visible = true;

            }
            else
            {
                percentamount.Visible = true;
                decsription.Visible = true;
                TransportCost.Visible = false;
                packandforwardcost.Visible = false;

            }
            ModelAddTotalTax.Show();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void RdbTransportCost_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (RdbTransportCost.SelectedValue == "Amount")
            {
                PHTransport.Visible = true;
            }
            else
            {
                PHTransport.Visible = false;
            }
            ModelAddTotalTax.Show();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void RdbPackingCost_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (RdbPackingCost.SelectedValue == "Amount")
            {
                phPacking.Visible = true;
                ph_Packing_Perc.Visible = true;
            }
            else
            {
                txtpackingamount.Text = "0.00";
                txtPacking_Perc.Text = "0.00";
                phPacking.Visible = false;
                ph_Packing_Perc.Visible=false;
            }
            ModelAddTotalTax.Show();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }


    protected void btnSaveTax_Click(object sender, EventArgs e)
    {
        try
        {
            objQuotationBL = new QuotationBL();
            objQuotationBL.Quot_ID = txtQuotation.Text.Trim();
            objQuotationBL.Type = rbtntype.SelectedValue.Trim();
            if (rbtntype.SelectedValue == "Transport")
            {
                objQuotationBL.Description = RdbTransportCost.SelectedValue.ToString();
                objQuotationBL.Type_Perc = Convert.ToDecimal(0.00);
                objQuotationBL.Type_Amount = txttransportamount.Text != string.Empty ? Convert.ToDecimal(txttransportamount.Text.Trim()) : Convert.ToDecimal(0.00);
                // chnaged by vis 10-06-17
                //if (Convert.ToDecimal(txttransportamount.Text.Trim()) > 0)
                //    ViewState["TransportationExtraCostExits"] = true;

            }
            else if (rbtntype.SelectedValue == "PackingForwardingCost")
            {

                objQuotationBL.Description = RdbPackingCost.SelectedValue.ToString();
                objQuotationBL.Type_Perc = Convert.ToDecimal(txtPacking_Perc.Text.Trim());
                objQuotationBL.Type_Amount = txtpackingamount.Text != string.Empty ? Convert.ToDecimal(txtpackingamount.Text.Trim()) : Convert.ToDecimal(0.00);
            }
            else
            {

                allow = GetTaxDuplicate(txtdescription.Text.ToString(),txtQuotation.Text);
                if (allow)
                {
                    objQuotationBL.Description = txtdescription.Text.ToString();
                    objQuotationBL.Type_Perc = Convert.ToDecimal(txtTaxDiscountPercent.Text.Trim());
                    objQuotationBL.Type_Amount = txtTaxDiscountAmount.Text != string.Empty ? Convert.ToDecimal(txtTaxDiscountAmount.Text.Trim()) : Convert.ToDecimal(0.00);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Tax details is already exists for this Quotation.');", true);
                    return;
                }
                }
            if (btnSaveTax.Text == "Save")
            {
                objQuotationBL.ItemCode = string.Empty;

                if (objQuotationBL.TaxItemsInsertion(con, QuotationBL.eLoadSp.TaxItemsInsertion))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render(' Tax details has been added sucessfully.');", true);
                    txtTaxDiscountPercent.Text = "0";
                    txtTaxDiscountAmount.Text = "0.00";
                    txtdescription.Text = string.Empty;
                    RdbTransportCost.SelectedIndex = -1;
                    txttransportamount.Text = string.Empty;
                    RdbPackingCost.SelectedIndex = -1;
                    txtpackingamount.Text = string.Empty;
                    BindTaxItems();
                    BindTaxWiseItems();                   

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Transportation or Packing Cost already exists for this Quotation.');", true);
                }
            }
          
        }
        catch(Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }

    }

    private bool GetTaxDuplicate(string taxname,string qouteID)
    {
            QuotationBL
            objQuotationBLO = new QuotationBL();
            DataSet ds = new DataSet();
            objQuotationBLO.Description = taxname;
            objQuotationBLO.Quot_ID = qouteID;

            if (objQuotationBLO.load(con, QuotationBL.eLoadSp.SELECT_DUPLICATE_TAX, ref ds))
            {
                if(ds.Tables[0].Rows.Count>0)
                {
                    return false;
                }
            }

            return true;

    }


    protected void Grid_Tax_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objQuotationBL = new QuotationBL();
            objQuotationBL.Quot_Tax_ID = Convert.ToInt32(e.Record["Quot_Tax_ID"]);
            if (objQuotationBL.delete(con, QuotationBL.eLoadSp.TaxItemsDeletionByID))
            {
                BindTaxItems();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render(' Tax Item has been deleted sucessfully.');", true);

            }
            ModelItemPopup.Hide();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    protected void btnAddTax_Click(object sender, EventArgs e)
    {
        if (((Button)sender).Text == "AddTax")
        {
            txtTaxDiscountPercent.Text = "";
            txtTaxDiscountAmount.Text = "";
            txtdescription.Text = "";
            RdbTransportCost.SelectedIndex = -1;
            txttransportamount.Text = "";
            RdbPackingCost.SelectedIndex = -1;
            txtpackingamount.Text = "";
        }

    }


    protected void btnCancelTax_Click(object sender, EventArgs e)
    {
        if (((Button)sender).Text == "Cancel")
        {

            txtTaxDiscountPercent.Text = "0";
            txtTaxDiscountAmount.Text = "0.00";
            txtdescription.Text = "";
            RdbTransportCost.SelectedIndex = -1;
            txttransportamount.Text = "";
            RdbPackingCost.SelectedIndex = -1;
            txtpackingamount.Text = "";
        }

    }
    
    decimal NetTotalAmt = 0;
    protected void Grid_Tax_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                NetTotalAmt = Convert.ToDecimal(e.Row.Cells[7].Text);
                txtNetTotal.Text = NetTotalAmt.ToString();
            }
            else if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[0].Text = "Grand Total:";
                e.Row.Cells[3].Text = Convert.ToDecimal(NetTotalAmt).ToString();
                if(Convert.ToDecimal(NetTotalAmt) == 0)
                {
                    e.Row.Cells[3].Text = txtTotalAmt.Text.ToString();
                }

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void lnkAddTax_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["ItemCode"] = ((LinkButton)sender).CommandName.ToString();
            txtAmtWithTax.Text = ((LinkButton)sender).CommandArgument.ToString();       
           
            ModelItemTax.Show();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void btnItemTaxSave_Click(object sender, EventArgs e)
    {
        try
        {
            objQuotationBL = new QuotationBL();
            objQuotationBL.Quot_ID = txtQuotation.Text.Trim();
            objQuotationBL.Type = rbtntype1.SelectedValue.Trim();

            objQuotationBL.Description = txtdescription1.Text.ToString();
            objQuotationBL.Type_Perc = Convert.ToDecimal(txtTaxDiscountPercent1.Text.Trim());
            objQuotationBL.Type_Amount = txtTaxDiscountAmount1.Text != string.Empty ? Convert.ToDecimal(txtTaxDiscountAmount1.Text.Trim()) : Convert.ToDecimal(0.00);

            if (btnSaveTax.Text == "Save")
            {
                if (ViewState["ItemCode"] != null)
                {
                    objQuotationBL.ItemCode = ViewState["ItemCode"].ToString();
                }                

                if (objQuotationBL.TaxItemsInsertion(con, QuotationBL.eLoadSp.TaxItemsInsertion))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render(' Tax details has been added sucessfully.');", true);
                    txtTaxDiscountPercent1.Text = "0";
                    txtTaxDiscountAmount1.Text = "0.00";
                    txtdescription1.Text = string.Empty;
                    rbtntype1.ClearSelection();
                    BindItem();
                    BindTaxItems();
                    BindTaxWiseItems();
                    ViewState["ItemCode"] = null;

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Transportation or Packing Cost already exists for this PO.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    protected void GridItemWiseTax_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objQuotationBL = new QuotationBL();
            objQuotationBL.Quot_Item_Tax_ID = Convert.ToInt32(e.Record["Quot_Item_Tax_ID"]);
            if (objQuotationBL.delete(con, QuotationBL.eLoadSp.DELETE_ITEMWISE_TAX))
            {
                BindTaxWiseItems();
                BindItem();
                BindTaxItems();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render(' Tax Item has been deleted sucessfully.');", true);

            }
            ModelItemPopup.Hide();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    protected void QuotationItemGrid_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objQuotationBL = new QuotationBL();
            objQuotationBL.QuotationItemID = Convert.ToInt32(e.Record["QuotationItemID"]);
            if (objQuotationBL.delete(con, QuotationBL.eLoadSp.DELETE_QUOTATION_ITEM_BY_ID))
            {
                
                BindItem();
                BindTaxWiseItems();
                BindTaxItems();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Item has been deleted sucessfully.');", true);

            }
            ModelItemPopup.Hide();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    protected void chkReferIndent_CheckedChanged(object sender, EventArgs e)
    {
        if(chkReferIndent.Checked == true)
        {
            trIndentNo.Visible = true;
            tdIndentNo.Visible = true;
            BindIndentNo();
        }
        else
        {
            trIndentNo.Visible = false;
            tdIndentNo.Visible = false;
        }
    }

    private void BindIndentItems()
    {
        try
        {
            objQuotationBL = new QuotationBL();
            ds = new DataSet();
            objQuotationBL.QuotationNo = txtQuotation.Text != string.Empty ? txtQuotation.Text : null;
            objQuotationBL.IndentNo = ddlIndentNo.SelectedValue;
            objQuotationBL.load(con, QuotationBL.eLoadSp.SELECT_INDENT_ITEMS_FOR_QUOTATION, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                QuotationItemGrid.DataSource = ds;
                QuotationItemGrid.DataBind(); 
            }
            else
            {
                QuotationItemGrid.DataSource = ds;
                QuotationItemGrid.DataBind();
            }
            HideGrid.Visible = true;
            QuotationItemGrid.Columns[8].Visible = false;
            QuotationItemGrid.Columns[9].Visible = false;
            QuotationItemGrid.Columns[10].Visible = false;
            //Grid_Tax.Visible = false;
            //GridItemWiseTax.Visible = false;
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void ddlIndentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlIndentNo.SelectedIndex !=0)
        {
            BindIndentItems();
        }
        else
        {
            BindItem();
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
}



    
         




