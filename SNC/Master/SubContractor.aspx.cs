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
using System.IO;

public partial class SubContractor : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    SubContractorBL objSubContractorBL = null;
    LocationBL objLocation = null;
    VendorBL objVendorBL = null;
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    BindCountry();
                    ddlCountry_SelectedIndexChanged(sender, e);
                    BindStateGrid();
                    BindBank();
                    
                    if (Request.QueryString["ID"] != null)
                    {
                        Bind_SubContractor_Details(Convert.ToInt32(Request.QueryString["ID"]));
                        GetContractorDetails(Request.QueryString["ID"].ToString());
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
            ddlCountry.SelectedValue = ddlCountry.Items.FindByText("India").Value;

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
    protected void BindStateGrid()
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
    protected void BindBank()
    {
        try
        {
            ds = new DataSet();
            objVendorBL = new VendorBL();
            objVendorBL.load(con, VendorBL.eLoadSp.SELECT_ALL_BANK, ref ds);
            ddlBankName.DataSource = ds;
            ddlBankName.DataTextField = "Bank";
            ddlBankName.DataValueField = "Bank";
            ddlBankName.DataBind();
            ddlBankName.Items.Insert(0, "-Select-");

            ddlBankName2.DataSource = ds;
            ddlBankName2.DataTextField = "Bank";
            ddlBankName2.DataValueField = "Bank";
            ddlBankName2.DataBind();
            ddlBankName2.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    
    protected void GetContractorDetails(string ContractorID)
    {
        try
        {
            ds = new DataSet();
            objSubContractorBL = new SubContractorBL();
            objSubContractorBL.Subcon_ID = ContractorID;
            objSubContractorBL.load(con, SubContractorBL.eLoadSp.SELECT_CONTRACTORDETAILS_BY_ID, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                objSubContractorBL.Subcon_ID = ds.Tables[0].Rows[0]["Subcon_ID"].ToString();
                txtContractorID.Text = ds.Tables[0].Rows[0]["Subcon_ID"].ToString();
                txtContractorName.Text = ds.Tables[0].Rows[0]["Subcon_name"].ToString();
                txtLegalName.Text = ds.Tables[0].Rows[0]["Legal_Name"].ToString();

                rblContractorType.SelectedValue = Convert.ToBoolean(ds.Tables[0].Rows[0]["Register"]) ? "1" : "0";
                rblContractorType_SelectedIndexChanged(null, null);
                txtRegistrationNo.Text = ds.Tables[0].Rows[0]["Regs_No"].ToString();
                
                rd_usepan.SelectedValue = Convert.ToBoolean(ds.Tables[0].Rows[0]["Use_PAN"]) ? "1" : "0";
                rd_usepan_SelectedIndexChanged(null, null);
                txtPANNo.Text = ds.Tables[0].Rows[0]["PAN_No"].ToString();
                
                txtServiceTax.Text = ds.Tables[0].Rows[0]["ST_No"].ToString();
                txtContactPerson.Text = ds.Tables[0].Rows[0]["Con_Person"].ToString();
                txtContactNo.Text = ds.Tables[0].Rows[0]["Con_No"].ToString();
                txtEmailID.Text = ds.Tables[0].Rows[0]["Email_ID"].ToString();
                txtaddr.Text = ds.Tables[0].Rows[0]["Add_Line"].ToString();
                txtLandmark.Text = ds.Tables[0].Rows[0]["Landmark"].ToString();
                txtPFRegistartion.Text = ds.Tables[0].Rows[0]["PFRegistration_No"].ToString();
                txtLabourLicenceNo.Text = ds.Tables[0].Rows[0]["LabourLicence_No"].ToString();
                txtNatureOfWork.Text = ds.Tables[0].Rows[0]["Nature_Work"].ToString();
                txtCity.Text = ds.Tables[0].Rows[0]["City"].ToString();
                ddlState.SelectedValue = ds.Tables[0].Rows[0]["State"].ToString();
                if (ds.Tables[0].Rows[0]["ChkRef"].ToString() !="")
                {
                    chkRef.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["ChkRef"].ToString());
                }
                chkRef_CheckedChanged(null, null);
                txtRefPerson.Text = ds.Tables[0].Rows[0]["RefPerson"].ToString();
                ddlCountry.SelectedItem.Text = ds.Tables[0].Rows[0]["Country"].ToString();
                txtPinNo.Text = ds.Tables[0].Rows[0]["Pin"].ToString();
                txtBranchName.Text = ds.Tables[0].Rows[0]["Branch"].ToString();
                ddlBankName.SelectedValue = ds.Tables[0].Rows[0]["Bank"].ToString();
                rd_accountype.SelectedValue = ds.Tables[0].Rows[0]["Account_Type"].ToString();
                txtAccountNo.Text = ds.Tables[0].Rows[0]["Acc_No"].ToString();
                txtIFSCCode.Text = ds.Tables[0].Rows[0]["IFSC"].ToString();

                txtBranchName2.Text = ds.Tables[0].Rows[0]["Branch2"].ToString();
                //ddlBankName2.SelectedValue = ds.Tables[0].Rows[0]["Bank2"].ToString();
                rblAccountType2.SelectedValue = ds.Tables[0].Rows[0]["Account_Type2"].ToString();
                txtAccNo2.Text = ds.Tables[0].Rows[0]["Acc_No2"].ToString();
                txtIFSCCode2.Text = ds.Tables[0].Rows[0]["IFSC2"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remark"].ToString();
                
                div_AfterUpload1.Visible = ds.Tables[0].Rows[0]["File_GSTRegistration"].ToString() != string.Empty ? true : false;
                lnkDownloadFile1.Text = ds.Tables[0].Rows[0]["File_GSTRegistration"].ToString();
                div_AfterUpload2.Visible = ds.Tables[0].Rows[0]["File_PANCopy"].ToString() != string.Empty ? true : false;
                lnkDownloadFile2.Text = ds.Tables[0].Rows[0]["File_PANCopy"].ToString();
                div_AfterUpload3.Visible = ds.Tables[0].Rows[0]["File_BankDetails"].ToString() != string.Empty ? true : false;
                lnkDownloadFile3.Text = ds.Tables[0].Rows[0]["File_BankDetails"].ToString();
                div_AfterUpload4.Visible = ds.Tables[0].Rows[0]["File_IDProof"].ToString() != string.Empty ? true : false;
                lnkDownloadFile4.Text = ds.Tables[0].Rows[0]["File_IDProof"].ToString();

                txtContractorID.Enabled = false;
                btnSubmit.Text = "Update";
            }
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
            objSubContractorBL = new SubContractorBL();
            objSubContractorBL.Subcon_ID = txtContractorID.Text;
            objSubContractorBL.Subcon_name = txtContractorName.Text;
            objSubContractorBL.Legal_Name = txtLegalName.Text.Trim();
            objSubContractorBL.RegsOrNot = Convert.ToInt32(rblContractorType.SelectedValue) == 1 ? true : false;
            objSubContractorBL.Regs_No = txtRegistrationNo.Text != string.Empty ? txtRegistrationNo.Text : "N/A";
            objSubContractorBL.Use_PAN = Convert.ToInt32(rd_usepan.SelectedValue) == 1 ? true : false;
            objSubContractorBL.PAN_NO = txtPANNo.Text != string.Empty ? txtPANNo.Text : "N/A";
            if (txtServiceTax.Text.Trim() != string.Empty)
            {
                objSubContractorBL.ST_No = Convert.ToInt64(txtServiceTax.Text);
            }
            else
            {
                objSubContractorBL.ST_No = null;
            }

            objSubContractorBL.Con_Person = txtContactPerson.Text;
            objSubContractorBL.Con_No = txtContactNo.Text;
            objSubContractorBL.Email_ID = txtEmailID.Text;
            objSubContractorBL.PFRegistartionNo = txtPFRegistartion.Text != string.Empty ? txtPFRegistartion.Text.Trim() : null;
            objSubContractorBL.LabourLicenseNo = txtLabourLicenceNo.Text != string.Empty ? txtLabourLicenceNo.Text.Trim() : null;
            
            objSubContractorBL.Add_Line = txtaddr.Text;
            objSubContractorBL.Landmark = txtLandmark.Text;
            objSubContractorBL.City = txtCity.Text;
            objSubContractorBL.State = ddlState.SelectedValue;
            objSubContractorBL.Nature_Work = txtNatureOfWork.Text != string.Empty ? txtNatureOfWork.Text.Trim() : null;
            objSubContractorBL.Country = ddlCountry.SelectedValue;
            if (txtPinNo.Text.Trim() != string.Empty)
            {
                objSubContractorBL.Pin =  Convert.ToInt32(txtPinNo.Text.Trim());
            }
            else
            {
                objSubContractorBL.Pin = null;
            }

            if (chkRef.Checked == true)
            {
                objSubContractorBL.ChkRef = true;
                objSubContractorBL.RefPersonName = txtRefPerson.Text;
            }
            else
            {
                objSubContractorBL.ChkRef = false;
                objSubContractorBL.RefPersonName = string.Empty;
            }
            objSubContractorBL.Branch = txtBranchName.Text;
            objSubContractorBL.Bank = ddlBankName.SelectedValue;
            objSubContractorBL.Account_Type = rd_accountype.SelectedValue;
            objSubContractorBL.Acc_No = txtAccountNo.Text;
            objSubContractorBL.IFSC = txtIFSCCode.Text;

            objSubContractorBL.Branch2 = txtBranchName2.Text.Trim();
            objSubContractorBL.Bank2 = ddlBankName2.SelectedIndex != 0 ? ddlBankName2.SelectedValue : null;
            objSubContractorBL.Account_Type2 = rblAccountType2.SelectedValue;
            objSubContractorBL.Acc_No2 = txtAccNo2.Text.Trim();
            objSubContractorBL.IFSC2 = txtIFSCCode2.Text.Trim();
            objSubContractorBL.Remark = txtRemarks.Text;

            //Upload Files
            if (fuGSTRegd.HasFile)
            {
                objSubContractorBL.File_GSTRegistration = "_GSTRegistration" + System.IO.Path.GetExtension(fuGSTRegd.FileName);
            }
            if (fuPANCopy.HasFile)
            {
                objSubContractorBL.File_PANCopy = "_PANCopy" + System.IO.Path.GetExtension(fuPANCopy.FileName);
            }
            if (fuBankDetails.HasFile)
            {
                objSubContractorBL.File_BankDetails = "_BankDetails" + System.IO.Path.GetExtension(fuBankDetails.FileName);
            }
            if (fuIDProof.HasFile)
            {
                objSubContractorBL.File_IDProof = "_IDProof" + System.IO.Path.GetExtension(fuIDProof.FileName);
            }

            if (((Button)sender).Text == "Submit")
            {

                if (objSubContractorBL.insert(con, SubContractorBL.eLoadSp.INSERT))
                {
                    txtContractorID.Text = objSubContractorBL.Subcon_ID.ToString();
                    if (fuGSTRegd.HasFile)
                    {
                        fuGSTRegd.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "Contractor_" + txtContractorID.Text + "_GSTRegistration" + System.IO.Path.GetExtension(fuGSTRegd.FileName)));
                    }
                    if (fuPANCopy.HasFile)
                    {
                        fuPANCopy.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "Contractor_" + txtContractorID.Text + "_PANCopy" + System.IO.Path.GetExtension(fuPANCopy.FileName)));
                    }
                    if (fuBankDetails.HasFile)
                    {
                        fuBankDetails.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "Contractor_" + txtContractorID.Text + "_BankDetails" + System.IO.Path.GetExtension(fuBankDetails.FileName)));
                    }
                    if (fuIDProof.HasFile)
                    {
                        fuIDProof.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "Contractor_" + txtContractorID.Text + "_IDProof" + System.IO.Path.GetExtension(fuIDProof.FileName)));
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Contractor details has been Added Successfuly');", true);
                    ControlClear();
                }

                else
                {
                    string msg = objSubContractorBL.OutputResult.ToString();
                    if (msg == "ContractorNameExists")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Contractor ID Or Name already exists, Pls try another ID or Name');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('PAN No already exists.!!!');", true);
                    }
                    
                }
            }
            else
            {
                if (objSubContractorBL.update(con, SubContractorBL.eLoadSp.UPDATE))
                {
                    if (fuGSTRegd.HasFile)
                    {
                        fuGSTRegd.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "Contractor_" + txtContractorID.Text + "_GSTRegistration" + System.IO.Path.GetExtension(fuGSTRegd.FileName)));
                    }
                    if (fuPANCopy.HasFile)
                    {
                        fuPANCopy.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "Contractor_" + txtContractorID.Text + "_PANCopy" + System.IO.Path.GetExtension(fuPANCopy.FileName)));
                    }
                    if (fuBankDetails.HasFile)
                    {
                        fuBankDetails.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "Contractor_" + txtContractorID.Text + "_BankDetails" + System.IO.Path.GetExtension(fuBankDetails.FileName)));
                    }
                    if (fuIDProof.HasFile)
                    {
                        fuIDProof.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "Contractor_" + txtContractorID.Text + "_IDProof" + System.IO.Path.GetExtension(fuIDProof.FileName)));
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Contractor details has been updated');", true);
                    GetContractorDetails(txtContractorID.Text);
                }
                else
                {
                    string Resultmsg = objSubContractorBL.OutputResult.ToString();
                    if (Resultmsg == "ContractorNameExists")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Contractor Name already exists, Pls try with Name');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('PAN No already exists.!!!');", true);
                    }
                  
                }
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Master/ContractorList.aspx", false);
    }

    protected void rblContractorType_SelectedIndexChanged(object Test1, EventArgs t)
    {
        if (rblContractorType.SelectedValue == "0")
        {
            txtRegistrationNo.Enabled = false;
            txtRegistrationNo.Text = string.Empty;
            RequiredFieldValidator2.Enabled = false;
            div_GSTUpload.Visible = false;
            div_IDProofUpload.Visible = true;
        }
        else
        {
            txtRegistrationNo.Enabled = true;
            RequiredFieldValidator2.Enabled = true;
            div_GSTUpload.Visible = true;
            div_IDProofUpload.Visible = false;
        }
    }

    protected void rd_usepan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rd_usepan.SelectedValue == "0")
        {
            txtPANNo.Enabled = false;
            txtPANNo.Text = string.Empty;
            RequiredFieldValidator4.Enabled = false;
        }
        else if (rd_usepan.SelectedValue == "1")
        {
            txtPANNo.Enabled = true;
            RequiredFieldValidator4.Enabled = true;
        }

    }
    
    protected void ControlClear()
    {
        txtNatureOfWork.Text = string.Empty;
        txtPFRegistartion.Text = string.Empty;
        txtLabourLicenceNo.Text = string.Empty;
        txtContractorID.Text = string.Empty;
        txtContractorName.Text = string.Empty;
        txtRegistrationNo.Text = string.Empty;
        rd_usepan.ClearSelection();
        chkRef.Checked = false;
        txtRefPerson.Text = string.Empty;
        txtPANNo.Text = string.Empty;
        txtServiceTax.Text = string.Empty;
        txtContactPerson.Text = string.Empty;
        txtContactNo.Text = string.Empty;
        txtEmailID.Text = string.Empty;
        txtaddr.Text = string.Empty;
        txtLandmark.Text = string.Empty;
        txtCity.Text = string.Empty;
        ddlState.SelectedIndex = 0;
        ddlCountry.SelectedIndex = 0;
        txtPinNo.Text = string.Empty;

        ddlBankName.SelectedIndex = 0;
        txtBranchName.Text = string.Empty;
        rd_accountype.SelectedIndex = -1;

        ddlBankName2.SelectedIndex = 0;
        txtBranchName2.Text = string.Empty;
        rblAccountType2.ClearSelection();
        txtAccNo2.Text = string.Empty;
        txtIFSCCode2.Text = string.Empty;

        txtAccountNo.Text = string.Empty;
        txtIFSCCode.Text = string.Empty;
        txtRemarks.Text = string.Empty;
    }

    protected void btnSaveBank_Click(object sender, EventArgs e)
    {
        try
        {
            objSubContractorBL = new SubContractorBL();

            objSubContractorBL.Bank = txtbankname.Text;

            if (((Button)sender).Text == "Save")
            {
                if (objSubContractorBL.insertBank(con, SubContractorBL.eLoadSp.INSERT_BANK))
                {
                    BindBank();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render(' New Bank Name Added Successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Bank Name already exists. Please try another name!');", true);
                }
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void Grid_Country_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objLocation = new LocationBL();
            objLocation.Country = e.Record["Country"].ToString();
            if (objLocation.delete(con, LocationBL.eLoadSp.DELETE_COUNTRY))
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
            if (objLocation.insertCountry(con, LocationBL.eLoadSp.INSERT_COUNTRY))
            {

                BindCountry();
                txtCountryName.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Country Name has been added sucessfully.');", true);
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
        ModelCountryPopup.Hide();
    }

    protected void Grid_State_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        objLocation = new LocationBL();
        objLocation.State = e.Record["State"].ToString();
        if (objLocation.delete(con, LocationBL.eLoadSp.DELETE_STATE))
        {
            BindStateGrid();
            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('State has been deleted successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete State name!.');", true);
        }
    }

    protected void btnSaveState_Click(object sender, EventArgs e)
    {
        try
        {
            objLocation = new LocationBL();
            objLocation.Country = ddlCountryName.SelectedValue.ToString();
            objLocation.State = txtStateName.Text.Trim();
            if (objLocation.insertState(con, LocationBL.eLoadSp.INSERT_STATE))
            {
                txtStateName.Text = string.Empty;
                ddlCountryName.SelectedValue = ddlCountryName.Items.FindByText("-Select-").Value;
                BindStateGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('State Name has been added sucessfully.');", true);
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
        mpeState.Hide();
    }

    protected void chkRef_CheckedChanged(object sender, EventArgs e)
    {
        if (chkRef.Checked == true)
        {
            txtRefPerson.Enabled = true;
        }
        else
        {
            txtRefPerson.Enabled = false;
            txtRefPerson.Text = string.Empty;
        }
    }

    protected void lnkDownloadFile_Click(object sender, EventArgs e)
    {
        try
        {
            string filePath = "../UploadedFiles/" + (sender as LinkButton).Text;
            string filepathnew = Server.MapPath("~\\UploadedFiles\\" + (sender as LinkButton).Text);
            FileInfo file = new FileInfo(filePath);
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.Flush();
            Response.TransmitFile(filepathnew);
            //Response.WriteFile(filePath);
            Response.End();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }
    protected void btnAddSub_Contractor_Click(object sender, EventArgs e)
    {
        ModelItemPopup.Show();

        lblAddItems.Text = "Add Sub Contractor Details";
        Clear();
    }
    protected void btnCancelSubContractorBankDetail_Click(object sender, EventArgs e)
    {
        Clear();
        btnSaveSubContractor_Bank_Details.Text = "Save";
        ModelItemPopup.Hide();
    }
    protected void Clear()
    {

        txtSubContractorName.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtVendorBankName.Text = string.Empty;
        txtAccountNumber.Text = string.Empty;
        txtBranch.Text = string.Empty;
        txtIFCCode.Text = string.Empty;

    }
    //protected void btnCancelSubContractor_Bank_Details_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("../Master/SubContractor.aspx", false);
    //}
    protected void btnSubContractor_Bank_Details_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["ID"] != null)
            {
                objSubContractorBL = new SubContractorBL();
                objSubContractorBL.SubContractor_Name = txtSubContractorName.Text;
                objSubContractorBL.Address = txtAddress.Text;
                objSubContractorBL.Bank_Name = txtVendorBankName.Text.Trim();
                objSubContractorBL.Account_Number = txtAccountNumber.Text;
                objSubContractorBL.Branch = txtBranch.Text;
                objSubContractorBL.IFC_code = txtIFCCode.Text;

                objSubContractorBL.Subcon_ID = Convert.ToString(Request.QueryString["ID"]);

                if (objSubContractorBL.insertsubcontractor(con, SubContractorBL.eLoadSp.INSERT_SUBCONTRACTOR_DETAILS))
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Sub Contractor details has been Created');", true);
                    Bind_SubContractor_Details(Convert.ToInt32(Request.QueryString["ID"]));
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Sub Contractor Name already exists, Pls try with another Name');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void Grid_SubContractorDetails_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objSubContractorBL = new SubContractorBL();
            objSubContractorBL.ID = Convert.ToInt32(e.Record["ID"]);
            if (objSubContractorBL.load(con, SubContractorBL.eLoadSp.DELETE_SUBCONTRACTOR_DETAILS))
            {
                Bind_SubContractor_Details(Convert.ToInt32(e.Record["Subcon_ID"]));
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Sub Contractor Detail has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete Sub Contractor Detail !.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void Bind_SubContractor_Details(int ID)
    {
        try
        {
            ds = new DataSet();
            objSubContractorBL = new SubContractorBL();
            objSubContractorBL.Subcon_ID =Convert.ToString(ID);
            objSubContractorBL.load(con, SubContractorBL.eLoadSp.SUBCONTRACTOR_SELECT_ALL_DETAIL, ref ds);
            Grid_SubContractorDetails.DataSource = ds;
            Grid_SubContractorDetails.DataBind();

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
}
