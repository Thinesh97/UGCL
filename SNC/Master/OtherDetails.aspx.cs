using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using SNC.ErrorLogger;
using BusinessLayer;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;

public partial class OtherDetails : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    VendorBL objVendorBL = null;
    LocationBL objLocation = null;
    SubContractorBL objSubContractorBL = null;
    OtherBL objOtherBL = null;
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
                        GetOtherDetails(Request.QueryString["ID"].ToString());
                    }

                    rd_usepan_SelectedIndexChanged(null, null);
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

    protected void GetOtherDetails(string OtherID)
    {
        try
        {
            objOtherBL = new OtherBL();
            ds = new DataSet();
            objOtherBL.Other_ID = OtherID;
            objOtherBL.load(con, OtherBL.eLoadSp.SELECT_OTHERDETAILS_BY_ID, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtOtherID.Text = ds.Tables[0].Rows[0]["Other_ID"].ToString();
                txtOtherName.Text = ds.Tables[0].Rows[0]["Other_name"].ToString();
                txtLegalName.Text = ds.Tables[0].Rows[0]["Legal_Name"].ToString();
                txtRegistrationNo.Text = ds.Tables[0].Rows[0]["Regs_No"].ToString();
                rd_usepan.SelectedValue = Convert.ToBoolean(ds.Tables[0].Rows[0]["Use_PAN"]) ? "1" : "0";
                txtPANNo.Text = ds.Tables[0].Rows[0]["PAN_No"].ToString();
                txtServiceTax.Text = ds.Tables[0].Rows[0]["ST_No"].ToString();
                txtContactPerson.Text = ds.Tables[0].Rows[0]["Con_Person"].ToString();
                txtContactNo.Text = ds.Tables[0].Rows[0]["Con_No"].ToString();
                txtEmailID.Text = ds.Tables[0].Rows[0]["Email_ID"].ToString();
                txtaddr.Text = ds.Tables[0].Rows[0]["Add_Line"].ToString();
                txtLandnark.Text = ds.Tables[0].Rows[0]["Landmark"].ToString();
                txtCity.Text = ds.Tables[0].Rows[0]["City"].ToString();
                ddlState.SelectedValue = ds.Tables[0].Rows[0]["State"].ToString();
                ddlCountry.SelectedItem.Text = ds.Tables[0].Rows[0]["Country"].ToString();
                txtPInNo.Text = ds.Tables[0].Rows[0]["Pin"].ToString();
                chkRef.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["ChkRef"].ToString());
                chkRef_CheckedChanged(null, null);
                txtRefPerson.Text = ds.Tables[0].Rows[0]["RefPerson"].ToString();

                txtBranchName.Text = ds.Tables[0].Rows[0]["Branch"].ToString();
                ddlBankName.SelectedValue = ds.Tables[0].Rows[0]["Bank"].ToString();
                rd_accountype.SelectedValue = ds.Tables[0].Rows[0]["Account_Type"].ToString();
                txtAccountNo.Text = ds.Tables[0].Rows[0]["Acc_No"].ToString();
                txtIFSCCode.Text = ds.Tables[0].Rows[0]["IFSC"].ToString();
                txtPFRegistartion.Text = ds.Tables[0].Rows[0]["PFRegistration_No"].ToString();
                txtLabourLicenceNo.Text = ds.Tables[0].Rows[0]["LabourLicence_No"].ToString();
                txtNatureOfWork.Text = ds.Tables[0].Rows[0]["Nature_Work"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remark"].ToString();

                txtBranchName2.Text = ds.Tables[0].Rows[0]["Branch2"].ToString();
                ddlBankName2.SelectedValue = ds.Tables[0].Rows[0]["Bank2"].ToString();
                rblAccountType2.SelectedValue = ds.Tables[0].Rows[0]["Account_Type2"].ToString();
                txtAccNo2.Text = ds.Tables[0].Rows[0]["Acc_No2"].ToString();
                txtIFSCCode2.Text = ds.Tables[0].Rows[0]["IFSC2"].ToString();
                if (ds.Tables[0].Rows[0]["Is_Asset"].ToString() != string.Empty)
                {
                    chIsAsset.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Is_Asset"].ToString());
                }
                else
                {
                    chIsAsset.Checked = false;
                }

                div_AfterUpload1.Visible = ds.Tables[0].Rows[0]["File_GSTRegistration"].ToString() != string.Empty ? true : false;
                lnkDownloadFile1.Text = ds.Tables[0].Rows[0]["File_GSTRegistration"].ToString();
                div_AfterUpload2.Visible = ds.Tables[0].Rows[0]["File_PANCopy"].ToString() != string.Empty ? true : false;
                lnkDownloadFile2.Text = ds.Tables[0].Rows[0]["File_PANCopy"].ToString();
                div_AfterUpload3.Visible = ds.Tables[0].Rows[0]["File_BankDetails"].ToString() != string.Empty ? true : false;
                lnkDownloadFile3.Text = ds.Tables[0].Rows[0]["File_BankDetails"].ToString();

                txtOtherID.Enabled = false;
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
            objOtherBL = new OtherBL();
            objOtherBL.Other_ID = txtOtherID.Text;
            objOtherBL.Other_Name = txtOtherName.Text;
            objOtherBL.Legal_Name = txtLegalName.Text.Trim();
            objOtherBL.Regs_No = txtRegistrationNo.Text;
            objOtherBL.Use_PAN = Convert.ToInt32(rd_usepan.SelectedValue) == 1 ? true : false;
            if (txtPANNo.Text != string.Empty)
            {
                objOtherBL.PAN_NO = txtPANNo.Text;
            }
            else
            {
                objOtherBL.PAN_NO = "NA";
            }

            objOtherBL.Con_Person = txtContactPerson.Text;
            objOtherBL.Con_No = txtContactNo.Text;
            objOtherBL.Email_ID = txtEmailID.Text;
            objOtherBL.Add_Line = txtaddr.Text;
            objOtherBL.Landmark = txtLandnark.Text;
            objOtherBL.City = txtCity.Text;
            objOtherBL.State = ddlState.SelectedValue;
            objOtherBL.Country = ddlCountry.SelectedValue;
            if (chkRef.Checked == true)
            {
                objOtherBL.ChkRef = true;
                objOtherBL.RefPersonName = txtRefPerson.Text;
            }
            else
            {
                objOtherBL.ChkRef = false;
                objOtherBL.RefPersonName = string.Empty;
            }
            if (txtPInNo.Text.Trim() != string.Empty)
            {
                objOtherBL.Pin = Convert.ToInt32(txtPInNo.Text.Trim());
            }
            else
            {
                objOtherBL.Pin = null;
            }
            if (txtPFRegistartion.Text != string.Empty)
            {
                objOtherBL.PFRegistartionNo = txtPFRegistartion.Text.Trim();
            }
            else
            {
                objOtherBL.PFRegistartionNo = null;
            }
            if (txtLabourLicenceNo.Text != string.Empty)
            {
                objOtherBL.LabourLicenseNo = txtLabourLicenceNo.Text.Trim();
            }
            else
            {
                objOtherBL.LabourLicenseNo = null;
            }
            objOtherBL.Branch = txtBranchName.Text;
            objOtherBL.Bank = ddlBankName.SelectedValue;
            objOtherBL.Account_Type = rd_accountype.SelectedValue;
            objOtherBL.Acc_No = txtAccountNo.Text;
            objOtherBL.IFSC = txtIFSCCode.Text;

            objOtherBL.Branch2 = txtBranchName2.Text.Trim();

            objOtherBL.Bank2 = ddlBankName2.SelectedIndex != 0 ? ddlBankName2.SelectedValue : null;
            objOtherBL.Account_Type2 = rblAccountType2.SelectedValue;
            objOtherBL.Acc_No2 = txtAccNo2.Text.Trim();
            objOtherBL.IFSC2 = txtIFSCCode2.Text.Trim();

            if (txtServiceTax.Text != string.Empty)
            {
                objOtherBL.ST_No = Convert.ToInt64(txtServiceTax.Text);
            }
            else
            {
                objOtherBL.ST_No = null;
            }

            objOtherBL.Nature_Work = txtNatureOfWork.Text;
            objOtherBL.Remark = txtRemarks.Text;
            objOtherBL.IsAsst = chIsAsset.Checked;

            if (fuGSTRegd.HasFile)
            {
                objOtherBL.File_GSTRegistration = "_GSTRegistration" + System.IO.Path.GetExtension(fuGSTRegd.FileName);
            }
            if (fuPANCopy.HasFile)
            {
                objOtherBL.File_PANCopy = "_PANCopy" + System.IO.Path.GetExtension(fuPANCopy.FileName);
            }
            if (fuBankDetails.HasFile)
            {
                objOtherBL.File_BankDetails = "_BankDetails" + System.IO.Path.GetExtension(fuBankDetails.FileName);
            }

            if (((Button)sender).Text == "Submit")
            {
                if (objOtherBL.insert(con, OtherBL.eLoadSp.INSERT))
                {
                    txtOtherID.Text = objOtherBL.Other_ID.ToString();
                    if (fuGSTRegd.HasFile)
                    {
                        fuGSTRegd.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "Other_" + txtOtherID.Text + "_GSTRegistration" + System.IO.Path.GetExtension(fuGSTRegd.FileName)));
                    }
                    if (fuPANCopy.HasFile)
                    {
                        fuPANCopy.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "Other_" + txtOtherID.Text + "_PANCopy" + System.IO.Path.GetExtension(fuPANCopy.FileName)));
                    }
                    if (fuBankDetails.HasFile)
                    {
                        fuBankDetails.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "Other_" + txtOtherID.Text + "_BankDetails" + System.IO.Path.GetExtension(fuBankDetails.FileName)));
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Other details has been Created');", true);
                    ClearControl();
                }
                else
                {
                    string msg = objOtherBL.OutputMsg.ToString();
                    if (msg == "OtherNameExists")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Other Name already exists, Pls try with another Name');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('PAN No already exists.!!!');", true);
                    }
                }
            }
            else
            {
                if (objOtherBL.update(con, OtherBL.eLoadSp.UPDATE))
                {
                    if (fuGSTRegd.HasFile)
                    {
                        fuGSTRegd.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "Other_" + txtOtherID.Text + "_GSTRegistration" + System.IO.Path.GetExtension(fuGSTRegd.FileName)));
                    }
                    if (fuPANCopy.HasFile)
                    {
                        fuPANCopy.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "Other_" + txtOtherID.Text + "_PANCopy" + System.IO.Path.GetExtension(fuPANCopy.FileName)));
                    }
                    if (fuBankDetails.HasFile)
                    {
                        fuBankDetails.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "Other_" + txtOtherID.Text + "_BankDetails" + System.IO.Path.GetExtension(fuBankDetails.FileName)));
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Other details has been updated');", true);
                    GetOtherDetails(txtOtherID.Text);
                }
                else
                {
                    string Resultmsg = objOtherBL.OutputMsg.ToString();
                    if (Resultmsg == "OtherNameExists")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Other ID Or Name  already exists, Pls try with  another ID Or Name');", true);
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
        try
        {
            if (btnSubmit.Text == "Submit")
            {
                Response.Redirect("../Master/OtherDetails.aspx", false);
            }
            else
            {
                Response.Redirect("../Master/OtherList.aspx", false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
            txtPANNo.ReadOnly = false;
            txtPANNo.Enabled = true;
            RequiredFieldValidator4.Enabled = true;
        }
    }
    
    public void ClearControl()
    {
        txtNatureOfWork.Text = string.Empty;
        txtPFRegistartion.Text = string.Empty;
        txtOtherID.Text = string.Empty;
        txtOtherName.Text = string.Empty;
        txtRegistrationNo.Text = string.Empty;
        rd_usepan.ClearSelection();
        txtPANNo.Text = string.Empty;
        txtContactPerson.Text = string.Empty;
        txtContactNo.Text = string.Empty;
        txtEmailID.Text = string.Empty;
        txtaddr.Text = string.Empty;
        txtRefPerson.Text = string.Empty;
        chkRef.Checked = false;
        txtLandnark.Text = string.Empty;
        txtCity.Text = string.Empty;
        ddlState.SelectedIndex = 0;
        ddlCountry.SelectedIndex = 0;
        txtPInNo.Text = string.Empty;
        ddlBankName.SelectedIndex = 0;
        txtServiceTax.Text = string.Empty;
        txtBranchName.Text = string.Empty;
        rd_accountype.SelectedIndex = -1;
        txtAccountNo.Text = string.Empty;
        txtIFSCCode.Text = string.Empty;

        ddlBankName2.SelectedIndex = 0;
        txtBranchName2.Text = string.Empty;
        rblAccountType2.ClearSelection();
        txtAccNo2.Text = string.Empty;
        txtIFSCCode2.Text = string.Empty;

        txtNatureOfWork.Text = string.Empty;
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
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('New Bank Name Added Successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Bank Name is already exists, Pls try another Name');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('State has been deleted sucessfully.');", true);
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
            refperson.Visible = true;
        }
        else
        {
            refperson.Visible = false;
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
}

