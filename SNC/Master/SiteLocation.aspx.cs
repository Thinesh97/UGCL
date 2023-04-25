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


public partial class SiteLocation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    CompanyBL objCompany = null;
    LocationBL objLocation = null;
    VendorBL objVendorBL = null;
    DataSet ds = null;
    DataSet Accessds = new DataSet();      
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UID"] != null)
            {
                ActionPermission();
                if (!IsPostBack)
                {

                    GetCompanyInfo();
                    BindCountry();
                    BindStateDropDown();
                    BindState();
                    BindBank();
                    BindLocationList();
                    BindBank_Details();
                    if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                    {

                        if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin" && !Convert.ToBoolean(Accessds.Tables[0].Rows[0]["CompySite_View"].ToString()))
                        {
                            Response.Redirect("~/CommonPages/Home.aspx", false);
                        }
                    }
                    else
                    {
                        Response.Redirect("~/CommonPages/Login.aspx", true);
                    }

                }
            }
            else
            {
                Response.Redirect("../CommonPages/Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ActionPermission()
    {
        try
        {
            if (Session["ActionAccess"] != null)
            {
                Accessds = (DataSet)Session["ActionAccess"];
            }
            if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
            {
                if (Accessds.Tables[0].Rows.Count > 0)
                {
                    if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                    {
                        if (Convert.ToBoolean(Accessds.Tables[0].Rows[0]["CompySite_Create"].ToString()))
                        {
                            btnAddLocation.Visible = true;
                        }
                        else
                        {
                            btnAddLocation.Visible = false;
                        }
                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["CompSite_Delete"].ToString()))
                        {


                            Grid_Location.Columns[14].Visible = false;

                        }
                        else
                        {
                            Grid_Location.Columns[14].Visible = true;
                        }

                    }
                }
            }
            else
            {
                Response.Redirect("~/CommonPages/Login.aspx", true);
            }


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }




    private void GetCompanyInfo()
    {
        try
        {
            objCompany = new CompanyBL();
            if (objCompany.load(con, CompanyBL.eLoadSp.SELECT_ALL))
            {
                txtCompanyName.Text = objCompany.Company_Name.ToString();
                txtCIN.Text = objCompany.CIN.ToString();
                txtPANNo.Text = objCompany.PAN.ToString();
                txtRegDate.Text = Convert.ToDateTime(objCompany.Reg_Dt).ToString("dd-MM-yyyy");
                txtCSTRegNo.Text = objCompany.CST_No.ToString();
                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindBank()
    {
        try
        {
            ds = new DataSet();
            objVendorBL = new VendorBL();
            objVendorBL.Task = "SELECT_BANK_LIST";
            objVendorBL.load(con, VendorBL.eLoadSp.SELECT_BANK_LIST, ref ds);
            ddlBank.DataSource = ds;
            ddlBank.DataTextField = "Bank_Name";
            ddlBank.DataValueField = "ID";
            ddlBank.DataBind();
            ddlBank.Items.Insert(0, "-Select-");

            GridBank.DataSource = ds;
            GridBank.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindBank_Details()
    {
        try
        {
            ds = new DataSet();
            objVendorBL = new VendorBL();
            objVendorBL.load(con, VendorBL.eLoadSp.SELECT_ALL_BANK_Detail, ref ds);
            GridFrontBank.DataSource = ds;
            GridFrontBank.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindCountry()
    {
        try
        {
            ds = new DataSet();
            objVendorBL = new VendorBL();    
            objVendorBL.load(con, VendorBL.eLoadSp.SELECT_ALL_COUNTRY, ref ds);
            ddlCountry.DataSource = ds;
            ddlCountry.DataTextField = "Country";
            ddlCountry.DataValueField = "Country";
            ddlCountry.DataBind();           
            ddlCountry.Items.Insert(0, "-Select-");
            ddlCountry.SelectedValue = ddlCountry.Items.FindByValue("India").Value;

            ddlCountryName.DataSource = ds;
            ddlCountryName.DataTextField = "Country";
            ddlCountryName.DataValueField = "Country";
            ddlCountryName.DataBind();
            ddlCountryName.Items.Insert(0, "-Select-");

            Grid_Country.DataSource = ds;
            Grid_Country.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void BindState()
    {
        try
        {
            ds = new DataSet();
            objVendorBL = new VendorBL();
            objVendorBL.load(con, VendorBL.eLoadSp.SELECT_ALL_State, ref ds);
            
            Grid_State.DataSource = ds;
            Grid_State.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void BindLocationList()
    {
        try
        {
            ds = new DataSet();
            objLocation = new LocationBL();
            objLocation.load(con, LocationBL.eLoadSp.SELECT_ALL,ref ds);
            Grid_Location.DataSource = ds;
            Grid_Location.DataBind();
          
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objCompany = new CompanyBL();
            objCompany.Company_Name = txtCompanyName.Text.Trim();
            objCompany.CIN = txtCIN.Text.Trim();
            objCompany.PAN = txtPANNo.Text.Trim();

            if (txtRegDate.Text.Trim() != string.Empty)
            {
                objCompany.Reg_Dt = Convert.ToDateTime(txtRegDate.Text.Trim());
            }
            else
            {
                objCompany.Reg_Dt = null;
            }
            objCompany.CST_No = txtCSTRegNo.Text.Trim();

            if (btnSave.Text == "Save")
            {

                if (objCompany.insert(con, CompanyBL.eLoadSp.INSERT))
                {
                    btnSave.Text = "Update";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Company info has been Added sucessfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Company by this Name already exists.');", true);
                    ClearControl();
                }
            }
            else
            {
                if (objCompany.update(con, CompanyBL.eLoadSp.UPDATE))
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Company information has been updated sucessfully.');", true);
                 
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSaveLocation_Click(object sender, EventArgs e)
    {
        try
        {
            objLocation = new LocationBL();
            objLocation.Loc_Type = ddlLocationType.SelectedValue.Trim();
            objLocation.Location_Name = txtLocationName.Text.Trim();
            objLocation.Short_Name = txtShortName.Text.Trim();
            objLocation.TIN = txtTINNo.Text.Trim();
            objLocation.GST = txtGSTNo.Text.Trim();
            objLocation.Address_Line1 = txtAddress.Text.Trim();
            objLocation.Landmark = txtLandmark.Text.Trim();
            objLocation.City = txtCity.Text.Trim();
            objLocation.State = ddlState.SelectedValue.Trim();
            objLocation.Country = ddlCountry.SelectedValue.Trim();
            objLocation.Contact_Name = txtContactName.Text.Trim();
            objLocation.Contact_No = txtContactNo.Text.Trim();

            if (txtPinNo.Text != string.Empty)
            {
                objLocation.PIN = Convert.ToInt32(txtPinNo.Text.Trim());
            }
            else
            {
                objLocation.PIN = null;
            }
     
            if(btnSaveLocation.Text == "Save")
            {
                if (objLocation.insert(con, LocationBL.eLoadSp.INSERT))
                {
                    BindLocationList();
                    ResetLocationFields();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Location has been Added sucessfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Location Name already exists, Please try another Name');", true);
                }
            }
            else 
            {
                objLocation.Location_ID = Convert.ToInt16(ViewState["LocationID"]); 
                if(objLocation.update(con,LocationBL.eLoadSp.UPDATE))
                {
                    BindLocationList();
                    ResetLocationFields();
                    lblHeading.Text = "Add Location";
                     ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Location has been updated sucessfully.');", true);                   
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Location Name already exists, Pls try another Name');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void ResetLocationFields()
    {
        ddlLocationType.SelectedValue = ddlLocationType.Items.FindByText("-Select-").Value;
        txtLocationName.Text = string.Empty;
        txtShortName.Text = string.Empty;
        txtGSTNo.Text = string.Empty;
        txtTINNo.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtLandmark.Text = string.Empty;
        txtCity.Text = string.Empty;
        
        ddlCountry.SelectedValue = ddlCountry.Items.FindByText("India").Value;
        BindStateDropDown();
        txtPinNo.Text = string.Empty;
        txtContactName.Text = string.Empty;
        txtContactNo.Text = string.Empty;
        btnSaveLocation.Text = "Save";
    }
  

    protected void Grid_Location_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objLocation = new LocationBL();
            objLocation.Location_ID = Convert.ToInt32(e.Record["Location_ID"]);

            if(objLocation.delete(con,LocationBL.eLoadSp.DELETE))
            {
                BindLocationList();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Location has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Location can not be deleted as it is already in use.');", true);
            }


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void lnkbtnLocationID_Click(object sender, EventArgs e)
    {
        try
        {           
            objLocation = new LocationBL();
            ViewState["LocationID"] = Convert.ToInt16(((LinkButton)sender).CommandName.ToString());
            objLocation.Location_ID = Convert.ToInt16(ViewState["LocationID"]);

            if (objLocation.load(con, LocationBL.eLoadSp.SELECTBYID))
            {
                ddlLocationType.SelectedValue = objLocation.Loc_Type.ToString();
                txtLocationName.Text = objLocation.Location_Name.ToString();
                txtShortName.Text = objLocation.Short_Name.ToString();
                txtGSTNo.Text = objLocation.GST.ToString();
                txtTINNo.Text = objLocation.TIN.ToString();
                txtAddress.Text = objLocation.Address_Line1.ToString();
                txtLandmark.Text = objLocation.Landmark.ToString();
                txtCity.Text = objLocation.City.ToString();
                ddlCountry.SelectedValue = objLocation.Country.ToString();
                ddlCountry_SelectedIndexChanged(null, null);
                
               
                ddlState.SelectedValue = objLocation.State.ToString();
                txtPinNo.Text = objLocation.PIN.ToString();
                if (!string.IsNullOrEmpty(objLocation.Contact_Name))
                {
                    txtContactName.Text = objLocation.Contact_Name.ToString();
                }
                if (!string.IsNullOrEmpty(objLocation.Contact_No))
                {
                    txtContactNo.Text = objLocation.Contact_No.ToString();
                }
                
                btnSaveLocation.Text = "Update";
                lblHeading.Text = "Update Location";
                mpeLocation.Show();
            }
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
            ClearControl();
            Response.Redirect("../CommonPages/Home.aspx", false);
           
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancelLocation_Click(object sender, EventArgs e)
    {
        BindLocationList();
        ResetLocationFields();
        lblHeading.Text = "Add Location";
    }
    public void ClearControl()
    {
        txtCompanyName.Text = string.Empty;
        txtCIN.Text = string.Empty;
        txtPANNo.Text = string.Empty;
        txtRegDate.Text = string.Empty;
        txtCSTRegNo.Text = string.Empty;
    }

    protected void btnExportToPDF_Click(object sender, EventArgs e)
    {

        try
        {
            Grid_Location.PageSize = -1;
            Grid_Location.DataBind();
            ExportGridToPDF(Grid_Location);

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }

    }
    private void ExportGridToPDF(Obout.Grid.Grid GirdData)
    {

        MemoryStream fileStream = new MemoryStream();
        Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
        try
        {
            PdfWriter wri = PdfWriter.GetInstance(doc, fileStream);
            doc.Open();
            Font font8 = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Color.WHITE);
            Paragraph paragraph = new Paragraph("                                                            Company and Location Details");
            PdfPTable PdfTable = new PdfPTable(GirdData.Columns.Count - 2);
            PdfPCell PdfPCell_Data = null;
            foreach (Obout.Grid.Column col in GirdData.Columns)
            {
                if (col.HeaderText != "Delete" & col.HeaderText != "Edit")
                {
                    PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(col.HeaderText, font8)));
                    PdfPCell_Data.BackgroundColor = iTextSharp.text.Color.GRAY;
                    PdfTable.AddCell(PdfPCell_Data);
                }
            }
            for (int i = 0; i < GirdData.Rows.Count; i++)
            {
                Hashtable dataItem = GirdData.Rows[i].ToHashtable();
                Font font1 = FontFactory.GetFont("ARIAL", 7);
                foreach (Obout.Grid.Column col in GirdData.Columns)
                {
                    if (col.HeaderText != "Delete" & col.HeaderText != "Edit")
                    {
                        PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(dataItem[col.DataField] != null ? dataItem[col.DataField].ToString() : "", font1)));
                        PdfTable.AddCell(PdfPCell_Data);
                    }
                }
            }

            PdfTable.SpacingBefore = 15f;
            doc.Add(paragraph);
            doc.Add(PdfTable);
        }
        finally
        {
            doc.Close();
        }

        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=CompanyandLocationDetails.pdf");
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(fileStream.ToArray());
        // Response.End();
        HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

    }

    protected void Grid_Location_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                {
                    if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                    {
                        LinkButton Editlik = e.Row.Cells[13].FindControl("LinkLocation") as LinkButton;

                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["CompySite_Update"].ToString()))
                        {                            
                            Editlik.Attributes.Add("onClick", "return false;");                        
                        } 
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_Country_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objLocation = new LocationBL();
            objLocation.Country = e.Record["Country"].ToString();
            if(objLocation.delete(con,LocationBL.eLoadSp.DELETE_COUNTRY))
            {

                BindCountry();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Country has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Country can not be deleted as it is already in use.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSaveCountry_Click(object sender, EventArgs e)
    {
        try
        {
           objLocation = new LocationBL();
           objLocation.Country = txtCountryName.Text.Trim();
            if(objLocation.insertCountry(con,LocationBL.eLoadSp.INSERT_COUNTRY))
            {

                BindCountry();
                txtCountryName.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Country Name has been added sucessfully.');", true);
                mpeLocation.Show();
                ModelCountryPopup.Show();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Country Name already exists !.');", true);
            }
           
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancelCountry_Click(object sender, EventArgs e)
    {
        mpeLocation.Show();
        ModelCountryPopup.Hide();
    }

    protected void Grid_State_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objLocation = new LocationBL();
            objLocation.State = e.Record["State"].ToString();
            if (objLocation.delete(con, LocationBL.eLoadSp.DELETE_STATE))
            {
                BindState();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('State has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete State name!.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSaveState_Click(object sender, EventArgs e)
    {
        try
        {
            objLocation = new LocationBL();
            objLocation.Country = ddlCountryName.SelectedValue.ToString();
            objLocation.State = txtStateName.Text.Trim();
            if(objLocation.insertState(con,LocationBL.eLoadSp.INSERT_STATE))
            {
                txtStateName.Text = string.Empty;
                ddlCountryName.SelectedValue = ddlCountryName.Items.FindByText("-Select-").Value;
                BindState();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('State Name has been added sucessfully.');", true);
                mpeLocation.Show();
                mpeState.Show();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This State Name already exists !.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindStateDropDown();
        mpeLocation.Show();
    }

    private void BindStateDropDown()
    {
        try
        {
            DataSet newds = new DataSet();
            DataTable DatafilterDt;
            bool exists;
            string countryName;
            objVendorBL = new VendorBL();
            if (ddlCountry.SelectedIndex != 0)
            {
                objVendorBL.load(con, VendorBL.eLoadSp.SELECT_ALL_State, ref newds);
                countryName = ddlCountry.SelectedValue.ToString();
                if (newds.Tables[0].Rows.Count > 0)
                {
                    DatafilterDt = newds.Tables[0];


                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Country").Equals(countryName)).Count() > 0;
                    if (exists)
                    {
                        DataTable Statedt = DatafilterDt.AsEnumerable()
                                     .Where(r => r.Field<string>("Country") == countryName)
                                     .CopyToDataTable();

                        ddlState.DataSource = Statedt;
                        ddlState.DataValueField = "State";
                        ddlState.DataTextField = "State";
                        ddlState.DataBind();

                    }
                    else
                    {
                        ddlState.Items.Clear();
                        ddlState.DataSource = null;
                        ddlState.DataBind();
                    }
                    exists = false;

                }
                ddlState.Items.Insert(0, "-Select-");
               
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnStateCancel_Click(object sender, EventArgs e)
    {
        mpeLocation.Show();
        mpeState.Hide();
    }
    protected void btn_AddCompanyBankDetails_Click(object sender, EventArgs e)
    {
        try
        {
            objLocation = new LocationBL();
           
            objLocation.Bank_Name = ddlBank.SelectedItem.Text;
            objLocation.Branch = Tb_bankbranch.Text;
            objLocation.Account_No = TB_accno.Text;
            objLocation.IFSC = TB_IFSC.Text;
            objLocation.MICR = TB_MICR.Text;
            objLocation.RTGS = TB_RTGS.Text;
            objLocation.SWIFT = TB_swift.Text;
            if (chkbankDefault.Checked)
            {
                if (chkbankDefault.Checked == true)
                {
                    objLocation.Make_Default_Bank = chkbankDefault.Checked = true;
                }
                objLocation.Make_Default_Bank = chkbankDefault.Checked;
            }
            if (objLocation.insertBank_Details(con, LocationBL.eLoadSp.INSERT_Bank_Details))
            {

                BindBank();
                txtBank.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Bank Details has been added sucessfully.');", true);
                BindBank_Details();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Bank Details already exists !.');", true);
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void BindBankDropDown()
    {
        try
        {
            ds = new DataSet();
            objVendorBL = new VendorBL();
            objVendorBL.Task = "SELECT_BANK_LIST";
            objVendorBL.load(con, VendorBL.eLoadSp.SELECT_BANK_LIST, ref ds);
            ddlBank.DataSource = ds;
            ddlBank.DataTextField = "Bank_Name";
            ddlBank.DataValueField = "ID";
            ddlBank.DataBind();
            ddlBank.Items.Insert(0, "-Select-");

           
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void btnSavebank_Click(object sender, EventArgs e)
    {
        try
        {
            objLocation = new LocationBL();
            objLocation.Bank = txtBank.Text.Trim();
            if (objLocation.insertBank(con, LocationBL.eLoadSp.INSERT_Bank))
            {
                ModalPopupBank.Hide();
                BindBank();
                BindBankDropDown();
                txtBank.Text = string.Empty;
                ModalPopupBank.Show();
                ModalPopupBankPane.Show();

                //ModalPopupBank.Show();
                //ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Bank Name has been added sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Bank Name already exists !.');", true);
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void Grid_Bank_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objLocation = new LocationBL();
            objLocation.Bank = e.Record["Bank_Name"].ToString();
            if (objLocation.delete(con, LocationBL.eLoadSp.DELETE_Bank))
            {

                BindBank();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Bank has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Bank can not be deleted as it is already in use.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_Bank_Details_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objLocation = new LocationBL();
            objLocation.ID = e.Record["ID"].ToString();
            if (objLocation.delete(con, LocationBL.eLoadSp.DELETE_Bank_Details))
            {

                BindBank_Details();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Bank has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Bank can not be deleted as it is already in use.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
}

