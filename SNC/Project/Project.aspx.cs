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
using System.Net;
using System.IO;

public partial class Project : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    ProjectBL objProjectBL = null;
    LocationBL objLocation = null;
    VendorBL objVendorBL = null;
    DataSet ds = null;
    string DatetimenowEX = DateTime.Now.ToString("yyyyMMddHHmmssfff");
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //GetProjectDetails(Request.QueryString["ID"].ToString());
                //Div_ReplaytoLetter.Visible = false;
                if (Session["UID"] != null)
                {
                    //gridextlicense.Visible = true;
                    btn_addContact.Style.Add("display", "none");
                    BindProjectType();
                    BindState();
                    BindLicenseType();
                    //BindLocation();
                    BindCompany();
                    BindDepartment();
                    BindDesignation();
                    BindLetterList();
                    BindLetterListReplay_NewDDL_LetterID_ALL();
                    BindLetterListModal();
                    BindLetterDepNameUp();
                    BindFreshLetterGrid();
                    BindReplayToLetterGrid();
                    BindReplayToLetterGrid_letRecFrom_Dept();
                    BindFreshLetterGrid_letRecFrom_Dept();
                    BindLetterList_letRecFrom_Dept();
                    BindGridDrawings();
                    BindGridVariations();

                    BindGridQuantity();
                    BindGridContract_Agreement();
                    BindGridCredentials_Approvals();
                    BindLetterDepName();
                    if (Request.QueryString["ID"] != null)
                    {

                        BindBGType();
                        BindInsuranceType();

                        BindGridBG_DOC();
                        BindGridInsuranceDoc();
                        GetProjectDetails(Request.QueryString["ID"].ToString());
                        BindContactList();
                        BindSitelocation();
                        //div_FileUpload.Visible = true;
                        BindUploadedFile();
                        BindGridLisence();
                        BindLicenseType();
                        BindGridExtLisence();
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

    protected void GetProjectDetails(string ProjectCode)
    {
        try
        {
            objProjectBL = new ProjectBL();
            ds = new DataSet();
            objProjectBL.Project_Code = ProjectCode;
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_DETAILS_BY_ID, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["Project_Code"] = ds.Tables[0].Rows[0]["Project_Code"].ToString();
                txtProjectID.Text = ds.Tables[0].Rows[0]["Project_Code"].ToString();
                txtProjectName.Text = ds.Tables[0].Rows[0]["Project_Name"].ToString();

                ddlProjectType.SelectedValue = ds.Tables[0].Rows[0]["Type"].ToString();
                ddlProjectType.Enabled = false;
                ddlState.SelectedValue = ds.Tables[0].Rows[0]["State_Code"].ToString();
                ddlState.Enabled = false;
                ddlState_SelectedIndexChanged(null, null);
                ddlLocation.SelectedValue = ds.Tables[0].Rows[0]["Location"].ToString();
                txtAliasProject.Text = ds.Tables[0].Rows[0]["Alias_Name"].ToString();
                if (ds.Tables[0].Rows[0]["St_Date"].ToString() == string.Empty)
                {
                    txtStartDate.Text = string.Empty;
                }
                else
                {
                    txtStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["St_Date"]).ToString("dd-MM-yyyy");
                }
                txtStartDate.Enabled = false;
                if (ds.Tables[0].Rows[0]["End_Dt"].ToString() == string.Empty)
                {
                    txtCompletionDate.Text = string.Empty;
                }
                else
                {
                    txtCompletionDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["End_Dt"]).ToString("dd-MM-yyyy");
                }
                if (ds.Tables[0].Rows[0]["E_Agreement_Date"].ToString() == string.Empty)
                {
                    txtEmployerAgreementDate.Text = string.Empty;
                }
                else
                {
                    txtEmployerAgreementDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["E_Agreement_Date"]).ToString("dd-MM-yyyy");
                }
                if (ds.Tables[0].Rows[0]["Agreement_Date"].ToString() == string.Empty)
                {
                    txtPrincipalAgreementDate.Text = string.Empty;
                }
                else
                {
                    txtPrincipalAgreementDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Agreement_Date"]).ToString("dd-MM-yyyy");
                }

                txtProjectCost.Text = ds.Tables[0].Rows[0]["Proj_Cost"].ToString();
                if (ds.Tables[0].Rows[0]["Proj_Cost"].ToString() != "")
                {
                    txtamtinwords.Text = NumberToWords(Convert.ToInt64(Convert.ToDecimal(txtProjectCost.Text)));
                }
                ddlAwardedBy.SelectedValue = ds.Tables[0].Rows[0]["Award_By"].ToString();
                ddlAwardedBy.Enabled = false;
                //txtAwardedBy.Text = ds.Tables[0].Rows[0]["Award_By"].ToString();
                rd_Status.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtPrincipalContractor.Text = ds.Tables[0].Rows[0]["Principal_Contractor"].ToString();
                btn_addContact.Style.Add("display", "normal");
                btnSubmit.Text = "Update";
                txtProjectID.Enabled = false;
            }
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
            ddlState.DataSource = ds;
            ddlState.DataValueField = "State_Code";
            ddlState.DataTextField = "State";
            ddlState.DataBind();
            ddlState.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindLocation()
    {
        try
        {
            ds = new DataSet();
            objLocation = new LocationBL();
            objLocation.load(con, LocationBL.eLoadSp.SELECT_ALL, ref ds);
            ddlLocation.DataSource = ds;
            ddlLocation.DataValueField = "Location_ID";
            ddlLocation.DataTextField = "Location_Name";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindLocationByState()
    {
        try
        {
            string stateName;
            DataTable DatafilterDt;
            bool exists;
            DataSet dsLocation = new DataSet();
            objLocation = new LocationBL();
            objLocation.load(con, LocationBL.eLoadSp.SELECT_ALL, ref dsLocation);
            stateName = ddlState.SelectedItem.Text.ToString();
            ddlLocation.DataSource = dsLocation;
            if (dsLocation.Tables[0].Rows.Count > 0)
            {
                DatafilterDt = dsLocation.Tables[0];
                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("State").Equals(stateName)).Count() > 0;
                if (exists)
                {
                    DataTable dtLocation = DatafilterDt.AsEnumerable()
                                 .Where(r => r.Field<string>("State") == stateName)
                                 .CopyToDataTable();

                    ddlLocation.DataSource = dtLocation;
                    ddlLocation.DataValueField = "Location_ID";
                    ddlLocation.DataTextField = "Location_Name";
                    ddlLocation.DataBind();
                    ddlLocation.Items.Insert(0, "-Select-");
                }
                else
                {
                    ddlLocation.Items.Clear();
                    ddlLocation.DataSource = null;
                    ddlLocation.DataBind();
                    ddlLocation.Items.Insert(0, "-Select-");
                }
                exists = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindLetterDepName()
    {
        try
        {
            ds = new DataSet();
            ProjectBL objProjectBL = new ProjectBL();
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_Letter_Recived_Dept_Name_ALL, ref ds);
            GridLetter_Recived_Dept.DataSource = ds;
            GridLetter_Recived_Dept.DataBind();
            ddlLetterRecdFrom.DataSource = ds;
            ddlLetterRecdFrom.DataValueField = "ID";
            ddlLetterRecdFrom.DataTextField = "Letter_Recived_Dept";
            ddlLetterRecdFrom.DataBind();
            ddlLetterRecdFrom.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindLetterDepNameUp()
    {
        try
        {
            ds = new DataSet();
            ProjectBL objProjectBL = new ProjectBL();
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_Letter_Recived_Dept_Name_ALL, ref ds);
            GridLetter_Recived_Dept.DataSource = ds;
            GridLetter_Recived_Dept.DataBind();
            ddlfreshletterRecFrom.DataSource = ds;
            ddlfreshletterRecFrom.DataValueField = "ID";
            ddlfreshletterRecFrom.DataTextField = "Letter_Recived_Dept";
            ddlfreshletterRecFrom.DataBind();
            ddlfreshletterRecFrom.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindCompany()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_COMPANY_ALL, ref ds); ;

            Grid_Company.DataSource = ds;
            Grid_Company.DataBind();

            ddlAwardedBy.DataSource = ds;
            ddlAwardedBy.DataValueField = "Company_Code";
            ddlAwardedBy.DataTextField = "Company_Name";
            ddlAwardedBy.DataBind();
            ddlAwardedBy.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindDepartment()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_DEPARTMENT_ALL, ref ds);
            ddlDept.DataSource = ds;
            ddlDept.DataValueField = "Department";
            ddlDept.DataTextField = "Department";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindDesignation()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_DESIGNATION_ALL, ref ds);
            ddlDesignation.DataSource = ds;
            ddlDesignation.DataValueField = "Designation";
            ddlDesignation.DataTextField = "Designation";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindProjectType()
    {
        try
        {
            objProjectBL = new ProjectBL();
            DataSet ds123 = new DataSet();
            objProjectBL.ProjectTypeName = txtProjectTypeName.Text;
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECTTYPE_ALL, ref ds123);
            Grid_ProjectType.DataSource = ds123;
            Grid_ProjectType.DataBind();

            ddlProjectType.Items.Clear();
            ddlProjectType.DataSource = ds123;
            ddlProjectType.DataValueField = "Proj_Type_ID";
            ddlProjectType.DataTextField = "Proj_Type";
            ddlProjectType.DataBind();
            ddlProjectType.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindLocationByState();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.Project_Code = txtProjectID.Text;
            objProjectBL.Project_Name = txtProjectName.Text;
            objProjectBL.Type = Convert.ToInt32(ddlProjectType.SelectedValue);
            objProjectBL.State = ddlState.SelectedValue;
            objProjectBL.Location = Convert.ToInt32(ddlLocation.SelectedValue);
            objProjectBL.AliasProject = txtAliasProject.Text.Trim();
            objProjectBL.StartDate = Convert.ToDateTime(txtStartDate.Text.ToString());
            objProjectBL.Principal_Contractor = txtPrincipalContractor.Text.Trim();

            if (txtCompletionDate.Text.Trim() != string.Empty)
            {
                objProjectBL.End_Dt = Convert.ToDateTime(txtCompletionDate.Text);
            }
            else
            {
                objProjectBL.End_Dt = null;
            }

            if (txtProjectCost.Text.Trim() != string.Empty)
            {
                objProjectBL.Proj_Cost = Convert.ToDecimal(txtProjectCost.Text);
            }
            else
            {
                objProjectBL.Proj_Cost = null;
            }
            if (txtEmployerAgreementDate.Text.Trim() != string.Empty)
            {
                objProjectBL.E_Agreement_Date = Convert.ToDateTime(txtEmployerAgreementDate.Text);
            }
            else
            {
                objProjectBL.E_Agreement_Date = null;
            }
            if (txtPrincipalAgreementDate.Text.Trim() != string.Empty)
            {
                objProjectBL.Agreement_Date = Convert.ToDateTime(txtPrincipalAgreementDate.Text);
            }
            else
            {
                objProjectBL.Agreement_Date = null;
            }
            objProjectBL.Award_By = ddlAwardedBy.SelectedValue;
            objProjectBL.Status = rd_Status.SelectedItem.Value;
            objProjectBL.Description = txtDescription.Text;

            if (btnSubmit.Text == "Update")
            {
                //if (txtFile1.Text != string.Empty && fuFile1.HasFile)
                //{
                //    objProjectBL.File1 = txtFile1.Text;
                //    objProjectBL.FileName1 = txtProjectID.Text + "_" + txtFile1.Text + System.IO.Path.GetExtension(fuFile1.FileName);
                //}
                //if (txtFile2.Text != string.Empty && fuFile2.HasFile)
                //{
                //    objProjectBL.File2 = txtFile2.Text;
                //    objProjectBL.FileName2 = txtProjectID.Text + "_" + txtFile2.Text + System.IO.Path.GetExtension(fuFile2.FileName);
                //}
                //if (txtFile3.Text != string.Empty && fuFile3.HasFile)
                //{
                //    objProjectBL.File3 = txtFile3.Text;
                //    objProjectBL.FileName3 = txtProjectID.Text + "_" + txtFile3.Text + System.IO.Path.GetExtension(fuFile3.FileName);
                //}
                //if (txtFile4.Text != string.Empty && fuFile4.HasFile)
                //{
                //    objProjectBL.File4 = txtFile4.Text;
                //    objProjectBL.FileName4 = txtProjectID.Text + "_" + txtFile4.Text + System.IO.Path.GetExtension(fuFile4.FileName);
                //}
                //if (txtFile5.Text != string.Empty && fuFile5.HasFile)
                //{
                //    objProjectBL.File5 = txtFile5.Text;
                //    objProjectBL.FileName5 = txtProjectID.Text + "_" + txtFile5.Text + System.IO.Path.GetExtension(fuFile5.FileName);
                //}

                if (objProjectBL.update(con, ProjectBL.eLoadSp.UPDATE))
                {
                    //if (txtFile1.Text != string.Empty && fuFile1.HasFile)
                    //{
                    //    fuFile1.SaveAs(Server.MapPath("~\\UploadedFiles\\" + txtProjectID.Text.Replace("/", "-") + "_" + txtFile1.Text + System.IO.Path.GetExtension(fuFile1.FileName)));
                    //}
                    //if (txtFile2.Text != string.Empty && fuFile2.HasFile)
                    //{
                    //    fuFile2.SaveAs(Server.MapPath("~\\UploadedFiles\\" + txtProjectID.Text.Replace("/", "-") + "_" + txtFile2.Text + System.IO.Path.GetExtension(fuFile2.FileName)));
                    //}
                    //if (txtFile3.Text != string.Empty && fuFile3.HasFile)
                    //{
                    //    fuFile3.SaveAs(Server.MapPath("~\\UploadedFiles\\" + txtProjectID.Text.Replace("/", "-") + "_" + txtFile3.Text + System.IO.Path.GetExtension(fuFile3.FileName)));
                    //}
                    //if (txtFile4.Text != string.Empty && fuFile4.HasFile)
                    //{
                    //    fuFile4.SaveAs(Server.MapPath("~\\UploadedFiles\\" + txtProjectID.Text.Replace("/", "-") + "_" + txtFile4.Text + System.IO.Path.GetExtension(fuFile4.FileName)));
                    //}
                    //if (txtFile5.Text != string.Empty && fuFile5.HasFile)
                    //{
                    //    fuFile5.SaveAs(Server.MapPath("~\\UploadedFiles\\" + txtProjectID.Text.Replace("/", "-") + "_" + txtFile5.Text + System.IO.Path.GetExtension(fuFile5.FileName)));
                    //}
                    //btn_addContact.Style.Add("display", "normal");
                    //txtFile1.Text = "";
                    //txtFile2.Text = "";
                    //txtFile3.Text = "";
                    //txtFile4.Text = "";
                    //txtFile5.Text = "";
                    //BindUploadedFile();
                    //ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Project details has been Updated');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to update the project details');", true);
                }
            }
            else
            {
                if (objProjectBL.load(con, ProjectBL.eLoadSp.CHECK_PROJECTNAME_DUPLICATE))
                {
                    if (objProjectBL.insert(con, ProjectBL.eLoadSp.INSERT_PROJECT))
                    {
                        btn_addContact.Style.Add("display", "normal");
                        txtProjectID.Text = objProjectBL.Proj_Code.ToString();
                        btnSubmit.Text = "Update";
                        ddlProjectType.Enabled = false;
                        ddlState.Enabled = false;
                        txtStartDate.Enabled = false;
                        ddlAwardedBy.Enabled = false;
                        txtPrincipalAgreementDate.Enabled = false;
                        txtEmployerAgreementDate.Enabled = false;
                        //div_FileUpload.Visible = true;
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Project details has been Created');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to create project with duplicate project name');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnAddContact_Click(object sender, EventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.Proj_Code = txtProjectID.Text.Trim();
            objProjectBL.Cont_Type = ddlContactType.SelectedValue.Trim();
            objProjectBL.Name = txtContactName.Text.Trim();
            objProjectBL.Department = ddlDept.SelectedValue.Trim();
            objProjectBL.Designation = ddlDesignation.SelectedValue.Trim();
            objProjectBL.Cont_No = txtContactNo.Text.Trim();
            objProjectBL.Email_ID = txtContactEmailID.Text.Trim();
            objProjectBL.Dispatch_Add = txtDispatchAdd.Text.Trim();

            if (objProjectBL.Contactinsert(con, ProjectBL.eLoadSp.INSERT_PROJECT_CONTACT))
            {
                BindContactList();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Contact details has been Created');", true);
                ddlContactType.SelectedIndex = -1;
                txtContactName.Text = "";
                ddlDept.SelectedIndex = -1;
                ddlDesignation.SelectedIndex = -1;
                txtContactNo.Text = "";
                txtContactEmailID.Text = "";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to add the Contact details');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void BindContactList()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.Proj_Code = txtProjectID.Text.Trim();
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_CONTACT, ref ds);
            GridContact.DataSource = ds;
            GridContact.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }



    protected void ClearProject()
    {
        txtProjectName.Text = string.Empty;
        ddlProjectType.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        ddlLocation.SelectedIndex = 0;
        txtAliasProject.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtCompletionDate.Text = string.Empty;
        txtProjectCost.Text = string.Empty;
        ddlAwardedBy.SelectedIndex = 0;
        txtDescription.Text = string.Empty;
        rd_Status.SelectedIndex = 0;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSubmit.Text == "Update")
            {
                Response.Redirect("ProjectList.aspx", false);
            }
            else
            {
                ClearProject();
                Response.Redirect("../CommonPages/Home.aspx", false);
            }
        }

        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void GridContact_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.Proj_Con_ID = Convert.ToInt32(e.Record["Proj_Con_ID"]);

            if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_PROJECT_CONTACT))
            {
                BindContactList();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Contact info has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete the Contact details');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancelContact_Click(object sender, EventArgs e)
    {
        txtProjectID.Text = "";
        ddlContactType.SelectedIndex = -1;
        txtContactName.Text = "";
        ddlDept.SelectedIndex = -1;
        ddlDesignation.SelectedIndex = -1;
        txtContactNo.Text = "";
        txtContactEmailID.Text = "";
    }

    //For ModelPopup Project Type
    protected void lnkBtnProjectID_Click(object sender, EventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            ViewState["TYPEID"] = Convert.ToInt16(((LinkButton)sender).CommandName.ToString());
            objProjectBL.Proj_Type_ID = Convert.ToInt16(ViewState["TYPEID"]);
            if (objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_BY_PROJECT_ID))
            {
                txtProjectTypeName.Text = objProjectBL.ProjectTypeName.ToString();
                txtProjectTypeCode.Text = objProjectBL.ProjectTypeCode.ToString();
                btnSaveProjectType.Text = "Update";
                ModelProjectTypePopup.Show();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_ProjectType_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.Proj_Type_ID = Convert.ToInt32(e.Record["Proj_Type_ID"]);

            if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_PROJECTTYPE))
            {
                BindProjectType();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Project type has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Project type cannot be deleted as it is already in use.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    protected void Grid_Document_name_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.ID_DocumentName = Convert.ToInt32(e.Record["ID"]);

            if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_Department_Name))
            {
                BindLetterDepName();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Departent Name has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Departent Name cannot be deleted as it is already in use.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    protected void btnSaveProjectType_Click(object sender, EventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.ProjectTypeName = txtProjectTypeName.Text;
            objProjectBL.ProjectTypeCode = txtProjectTypeCode.Text;
            if (btnSaveProjectType.Text == "Save")
            {
                if (objProjectBL.ProjectTypeinsert(con, ProjectBL.eLoadSp.INSERT))
                {
                    BindProjectType();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Project Type has been Created');", true);
                    txtProjectTypeName.Text = string.Empty;
                    txtProjectTypeCode.Text = string.Empty;
                    ModelProjectTypePopup.Show();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Project Type or Code is already exists, Pls try another');", true);
                }
            }
            else
            {
                objProjectBL.Proj_Type_ID = Convert.ToInt16(ViewState["TYPEID"]);
                if (objProjectBL.ProjectTypeupdate(con, ProjectBL.eLoadSp.UPDATE_PROJECT_TYPE_BY_ID))
                {
                    BindProjectType();
                    txtProjectTypeName.Text = string.Empty;
                    txtProjectTypeCode.Text = string.Empty;
                    btnSaveProjectType.Text = "Save";
                    ModelProjectTypePopup.Show();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Project Type Details has been updated successfully.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancelProjectType_Click(object sender, EventArgs e)
    {
        txtProjectTypeName.Text = string.Empty;
        txtProjectTypeCode.Text = string.Empty;
    }

    //For ModelPopup Company Awarded By
    protected void lnkBtnCompany_Click(object sender, EventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            ViewState["Company_Code"] = ((LinkButton)sender).CommandName.ToString();
            objProjectBL.CompanyCode = ViewState["Company_Code"].ToString();
            if (objProjectBL.loadCompany(con, ProjectBL.eLoadSp.SELECT_COMPANY_BY_ID))
            {
                txtCompanyName.Text = objProjectBL.CompanyName.ToString();
                txtCompanyCode.Text = objProjectBL.CompanyCode.ToString();
                btnSaveCompany.Text = "Update";
                ModalCompanyPopup.Show();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_Company_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.CompanyCode = e.Record["Company_Code"].ToString();

            if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_COMPANY))
            {
                BindCompany();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Company has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Project type cannot be deleted as it is already in use.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void btnSaveLetterRecivedDepartment(object sender, EventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();

            objProjectBL.Letter_Recived_Dept = txtLetDepartmentNameUp.Text;


            if (btnSaveLetterRecDepartment.Text == "Save")
            {
                if (objProjectBL.LetterRecivedDepartmenInsert(con, ProjectBL.eLoadSp.INSERT_Letter_Recived_Dept))
                {
                    BindLetterDepName();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Department Name has been Created');", true);
                    txtLetDepartmentNameUp.Text = string.Empty;
                    // ModelLetterDept_Name.Show();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Department Name already exists, Pls try another ID');", true);
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSaveCompany_Click(object sender, EventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.CompanyName = txtCompanyName.Text;
            objProjectBL.CompanyCode = txtCompanyCode.Text;
            if (btnSaveCompany.Text == "Save")
            {
                if (objProjectBL.CompanyInsert(con, ProjectBL.eLoadSp.INSERT_COMPANY))
                {
                    BindCompany();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Company has been Created');", true);
                    txtCompanyName.Text = string.Empty;
                    txtCompanyCode.Text = string.Empty;
                    ModalCompanyPopup.Show();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Company already exists, Pls try another ID');", true);
                }
            }
            else
            {
                if (objProjectBL.CompanyUpdate(con, ProjectBL.eLoadSp.UPDATE_COMPANY_BY_ID))
                {
                    BindCompany();
                    txtCompanyName.Text = string.Empty;
                    txtCompanyCode.Text = string.Empty;
                    btnSaveCompany.Text = "Save";
                    ModalCompanyPopup.Show();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Company Details has been updated successfully.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancelCompany_Click(object sender, EventArgs e)
    {
        txtProjectTypeName.Text = string.Empty;
        txtProjectTypeCode.Text = string.Empty;
    }
    protected void btnCancelLetterRecivedDepartmenInsert_Click(object sender, EventArgs e)
    {
        txtLetDepartmentName.Text = string.Empty;
    }
    private void BindUploadedFile()
    {
        try
        {
            //ds= new DataSet();
            //objProjectBL = new ProjectBL();
            //objProjectBL.Proj_Code = txtProjectID.Text.Trim();
            //objProjectBL.Task = "Select_AllProjectFile";
            //objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_FILE_ALL, ref ds);
            //GridFile.DataSource = ds;
            //GridFile.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void GridFile_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.Task = "Delete_ProjectFile";
            objProjectBL.ProjectFile_ID = Convert.ToInt32(e.Record["ProjectFile_ID"]);

            if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_PROJECT_FILE))
            {
                BindUploadedFile();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Uploaded File has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete the Uploaded File');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void lnkDownloadFile_Click(object sender, EventArgs e)
    {
        try
        {
            string filePath = Server.MapPath("~\\UploadedFiles\\" + (sender as LinkButton).Text.Replace("/", "-"));
            Response.Clear();

            string path = filePath;
            //string path = "C:\\Users\\chetan\\Desktop\\SimproBackp\\sim\\Simpro\\UGCL\\UGCL14032023\\UGCL\\SNC-INVEN\\SNC\\UploadedFiles\\SPML-CP-08 LTR 86 Progress of works-Reg.pdf20230311120134132_.pdf";
            WebClient client = new WebClient();
            Byte[] buffer = client.DownloadData(path);
            if (buffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }
    private void BindFreshLetterGrid()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            if (Request.QueryString["ID"] != "")
            {
                objProjectBL.Project_Code = Request.QueryString["ID"];
            }
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_LetterID_ALL, ref ds);
            GridFreshLetter.DataSource = ds;
            GridFreshLetter.DataBind();


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void BindReplayToLetterGrid()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.ProjectCode = Request.QueryString["ID"];
            objProjectBL.Proj_Code = txtProjectID.Text.Trim();
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_REPLAYTOLETTER_GRID, ref ds);
            GridReplayToLetter.DataSource = ds;
            GridReplayToLetter.DataBind();


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void BindReplayToLetterGrid_letRecFrom_Dept()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.ProjectCode = Request.QueryString["ID"];
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_REPLAYTOLETTER_GRID_letRecFrom_Dept, ref ds);
            GridLetterRecFrDep_ReplayLeter.DataSource = ds;
            GridLetterRecFrDep_ReplayLeter.DataBind();


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    private void BindFreshLetterGrid_letRecFrom_Dept()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.ProjectCode = Request.QueryString["ID"];
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_LetterID_ALL_letRecFrom_Dept, ref ds);
            GridLetter_letRecFrom_Dept.DataSource = ds;
            GridLetter_letRecFrom_Dept.DataBind();


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }






    protected void btnCancel_FreshLetter_Click(object sender, EventArgs e)
    {
        ClearFreshLetterFilds();
    }
    protected void ClearFreshLetterFilds()
    {
        try
        {
            rblLetterSelect.SelectedValue = string.Empty;
            txtLetter_ID.Text = string.Empty;
            txtLetterRefNumber.Text = "";
            txtLetterReceivedDate.Text = "";
            txtDateofReceipt.Text = "";
            txtReplyByDate.Text = "";
            txtModeofRecepit.Text = "";
            txtFreshLetterSubject.Text = "";
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ClearReplayToLetterFilds()
    {
        try
        {
            rblLetterSelect.SelectedValue = string.Empty;
            txtModeofDispatch_RTL.Text = "";
            txtLetterRefNumber_RTL.Text = "";
            Date_RTL.Text = "";
            //txtLetterSentTo_RTL.Text = "";
            ddlReplayToLetter_ID_RTL.Text = "";
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void BindLetterList()
    {
        try
        {
            objProjectBL = new ProjectBL();
            DataSet data = new DataSet();
            if (Session["Project_Code"].ToString() != "")
            {
                objProjectBL.Project_Code = Convert.ToString(Session["Project_Code"]);
            }
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_LetterID_ALL, ref data);
            Grid_ProjectType.DataSource = data;
            ddlReplayToLetter_ID_RTL.Items.Clear();
            ddlReplayToLetter_ID_RTL.DataSource = data;
            ddlReplayToLetter_ID_RTL.DataValueField = "ID";
            ddlReplayToLetter_ID_RTL.DataTextField = "Letter_ID";
            ddlReplayToLetter_ID_RTL.DataBind();
            ddlReplayToLetter_ID_RTL.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindLetterListReplay_NewDDL_LetterID_ALL()
    {
        try
        {
            objProjectBL = new ProjectBL();
            DataSet data = new DataSet();
            if (Session["Project_Code"].ToString() != "")
            {
                objProjectBL.Project_Code = Convert.ToString(Session["Project_Code"]);
            }
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_Replay_NewDDL_LetterID_ALL, ref data);
            ddlFreshNewToletterId.Items.Clear();
            ddlFreshNewToletterId.DataSource = data;
            ddlFreshNewToletterId.DataValueField = "ID";
            ddlFreshNewToletterId.DataTextField = "Letter_ID";
            ddlFreshNewToletterId.DataBind();
            ddlFreshNewToletterId.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void BindLetterList_letRecFrom_Dept()
    {
        try
        {
            objProjectBL = new ProjectBL();
            DataSet data = new DataSet();
            if (Session["Project_Code"].ToString() != "")
            {
                objProjectBL.Project_Code = Convert.ToString(Session["Project_Code"]);
            }
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_LetterID_ALL_letRecFrom_Dept, ref data);
            Grid_ProjectType.DataSource = data;

            ddlCorToDepID.Items.Clear();
            ddlCorToDepID.DataSource = data;
            ddlCorToDepID.DataValueField = "ID";
            ddlCorToDepID.DataTextField = "Letter_ID";
            ddlCorToDepID.DataBind();
            ddlCorToDepID.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancelSaveReplytoLetter_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_FreshLetter_Click(object sender, EventArgs e)
    {

        if (rblLetterSelect.SelectedValue == "FreshLetter")
        {
            try
            {

                GetProjectDetails(Request.QueryString["ID"].ToString());
                if (ViewState["Project_Code"].ToString() != "")
                {
                    objProjectBL.Project_Code = Convert.ToString(ViewState["Project_Code"]);

                }

                DataSet catds = new DataSet();
                if (txtLetter_ID.Text != "" && rblLetterSelect.Text != "")
                {

                }
                else
                {
                    //if (objProjectBL.loadLetterID(con, ProjectBL.eLoadSp.GENERATE_LETTER_ID, ref catds))
                    //{
                    //    txtLetter_ID.Text = catds.Tables[0].Rows[0]["LETTER_ID"].ToString();
                    //}
                }
                //objProjectBL.Letter_ID = txtLetter_ID.Text;
                objProjectBL.LetterRefNumber = txtLetterRefNumber.Text;
                if (ddlLetterRecdFrom.SelectedValue != "-Select-")
                {
                    objProjectBL.LetterRecdFrom = ddlLetterRecdFrom.SelectedValue;
                }

                if (txtLetterReceivedDate.Text != "")
                {
                    objProjectBL.LetterReceivedDate = Convert.ToDateTime(txtLetterReceivedDate.Text);
                }
                if (txtLetterReceivedDate.Text != "")
                {
                    objProjectBL.DateofReceipt = Convert.ToDateTime(txtDateofReceipt.Text);
                }

                if (txtLetterReceivedDate.Text != "")
                {
                    objProjectBL.ReplyByDate = Convert.ToDateTime(txtReplyByDate.Text);
                }
                objProjectBL.ModeofRecepit = txtModeofRecepit.Text;
                objProjectBL.Copyof_Fresh_Letter_Path = txtProjectTypeName.Text;

                var ProjectCode = Request.QueryString["ID"].ToString();

                objProjectBL.FreshLetterSubject = txtFreshLetterSubject.Text;

                if (fupCopyof_Fresh_Letter.HasFile)
                {

                    objProjectBL.Copyof_Fresh_Letter_Path = txtLetter_ID.Text + "_Fr_Letter" + System.IO.Path.GetExtension(fupCopyof_Fresh_Letter.FileName);
                }
                if (fupCopyof_Fresh_Letter.HasFile)
                {
                    fupCopyof_Fresh_Letter.SaveAs(Server.MapPath("~\\UploadedFiles\\" + txtLetter_ID.Text.Replace("/", "-") + "_Fr_Letter" + System.IO.Path.GetExtension(fupCopyof_Fresh_Letter.FileName)));
                }

                if (rblLetterSelect.SelectedValue == "FreshLetter")
                {
                    if (objProjectBL.FreshLetter_insert(con, ProjectBL.eLoadSp.INSERT_FreshLetter))
                    {
                        //page_Received.Attributes.Remove("panel-collapse panel-body collapse out");
                        ////page_Received.Attributes.Add("class", "panel-collapse panel-body collapse in");
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Fresh Letter Details has been Saved Successfully');", true);
                        BindFreshLetterGrid();
                        ClearFreshLetterFilds();
                        BindLetterList();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Project Type or Code is already exists, Pls try another');", true);
                    }
                }
                else
                {
                    objProjectBL.Letter_ID = Convert.ToString(ViewState["Letter_ID"]);
                    if (objProjectBL.FreshLetterupdate(con, ProjectBL.eLoadSp.UPDATE_FreshLetter_BY_ID))
                    {
                        //page_Received.Attributes.Remove("panel-collapse panel-body collapse out");
                        ////page_Received.Attributes.Add("class", "panel-collapse panel-body collapse in");
                        ClearFreshLetterFilds();
                        btnSaveProjectType.Text = "Save";
                        ModelProjectTypePopup.Show();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Project Type Details has been updated successfully.');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        else
        {
            try
            {
                objProjectBL = new ProjectBL();

                if (ddlReplayToLetter_ID_RTL.SelectedItem.Text != "-Select-")
                {
                    objProjectBL.ReplayToLetter_ID_RTL = ddlReplayToLetter_ID_RTL.SelectedItem.Text;
                }
                else if (ddlFreshNewToletterId.SelectedItem.Text != "-Select-")
                {
                    objProjectBL.ReplayToLetter_ID_RTL = ddlFreshNewToletterId.SelectedItem.Text;
                }
                if (ddlCorToDepID.SelectedValue != "-Select-")
                {
                    objProjectBL.ReplayToLetter_ID_RTL = ddlCorToDepID.SelectedItem.Text;
                }
                objProjectBL.Date_RTL = Convert.ToDateTime(Date_RTL.Text);
                objProjectBL.LetterRefNumber_RTL = txtLetterRefNumber_RTL.Text;
                objProjectBL.ModeofDispatch_RTL = txtModeofDispatch_RTL.Text;
                objProjectBL.LetterSentTo_RTL = txtLetterSentTo.Text;
                objProjectBL.Subject = txtSubjuectCorrespondence.Text;
                //Start:Written by Prashanth
                objProjectBL.CC1 = Txtcc1.Text;
                objProjectBL.CC2 = Txtcc2.Text;
                objProjectBL.CC3 = Txtcc3.Text;
                objProjectBL.CC4 = Txtcc4.Text;
                objProjectBL.CC5 = Txtcc5.Text;
                //End: Written By Prashanth
                if (fupLetterCopy_RTL.HasFile)
                {

                    objProjectBL.LetterCopy_RTL = fupLetterCopy_RTL.FileName + DatetimenowEX + "_" + System.IO.Path.GetExtension(fupLetterCopy_RTL.FileName);
                }
                if (fupLetterCopy_RTL.HasFile)
                {
                    fupLetterCopy_RTL.SaveAs(Server.MapPath("~\\UploadedFiles\\" + fupLetterCopy_RTL.FileName + DatetimenowEX + "_" + System.IO.Path.GetExtension(fupLetterCopy_RTL.FileName)));
                }

                if (fupAcknowledgementCopy_RTL.HasFile)
                {

                    objProjectBL.AcknowledgementCopy_RTL = "Acknowledgement_" + fupAcknowledgementCopy_RTL.FileName + DatetimenowEX + "_" + System.IO.Path.GetExtension(fupAcknowledgementCopy_RTL.FileName);
                }
                if (fupAcknowledgementCopy_RTL.HasFile)
                {
                    fupAcknowledgementCopy_RTL.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "Acknowledgement_" + fupAcknowledgementCopy_RTL.FileName + DatetimenowEX + "_" + System.IO.Path.GetExtension(fupAcknowledgementCopy_RTL.FileName)));
                }
                if (Request.QueryString["ID"] != "")
                {
                    objProjectBL.ProjectCode = Request.QueryString["ID"];
                }
                if (rblLetterSelect.SelectedValue == "ReplayToletter")
                {
                    if (objProjectBL.ReplayToLetter_insert(con, ProjectBL.eLoadSp.INSERT_ReplayToLetter))
                    {
                        //// page_Received.Attributes.Remove("panel-collapse panel-body collapse out");
                        //page_Received.Attributes.Add("class", "panel-collapse panel-body collapse in");
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Replay To Letter Letter Details has been Saved Successfully');", true);
                        ClearReplayToLetterFilds();
                        BindReplayToLetterGrid();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Replay To Letter is already exists, Pls try another');", true);
                    }
                }
                else
                {
                    objProjectBL.ReplayToLetter_ID_RTL = Convert.ToString(ViewState["ReplayToLetter_ID_RTL"]);
                    if (objProjectBL.ReplayToLetterupdate(con, ProjectBL.eLoadSp.UPDATE_ReplayToLetter_BY_ID))
                    {
                        ClearReplayToLetterFilds();
                        btnSaveProjectType.Text = "Save";
                        ModelProjectTypePopup.Show();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Replay To Letter Details has been updated successfully.');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
    }
    protected void GridFreshLetter_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.ID = Convert.ToInt32(e.Record["ID"]);

            if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_FreshLetter))
            {
                BindFreshLetterGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Fresh Letter info has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete the Fresh Letter details');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void GridReplayLetter_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.ID = Convert.ToInt32(e.Record["ID"]);

            if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_ReplayLetter))
            {
                BindReplayToLetterGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Replay To Letter info has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete the Replay to Letter details');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void GridContractAgreement_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.ID = Convert.ToInt32(e.Record["ID"]);

            if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_Contract_Agreement))
            {
                BindGridContract_Agreement();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Contract Agreement info has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete the Contract Agreement details');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void GridReplayLetter_letRecFrom_Dept_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.ID = Convert.ToInt32(e.Record["ID"]);

            if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_ReplayLetter_letRecFrom_Dept))
            {
                BindReplayToLetterGrid_letRecFrom_Dept();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Replay To Letter info has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete the Replay to Letter details');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void GridFreshLetterletRecFrom_Dept_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.ID = Convert.ToInt32(e.Record["ID"]);

            if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_FreshLetter_letRecFrom_Dept))
            {
                BindLetterList_letRecFrom_Dept();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Fresh Letter info has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete the Fresh Letter details');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    public void ClearFreshLetter()
    {
        txtLetterID_letRecFrom_Dept.Text = string.Empty;
        txtLetterRefNo_letRecFrom_Dept.Text = string.Empty;
        txtLetterRecFrom_letRecFrom_Dept.Text = string.Empty;
        txtLetterRecDate_letRecFrom_Dept.Text = string.Empty;
        Date_of_Receipt_letRecFrom_Dept.Text = string.Empty;
        txtReplyByDate_letRecFrom_Dept.Text = string.Empty;
        txtModeofRecepit_ModeofRecepit.Text = string.Empty;
        txtSubjext_letRecFrom_Dept.Text = string.Empty;

    }
    protected void btn_Variations_Click(object sender, EventArgs e)
    {

        try
        {
            objProjectBL = new ProjectBL();

            if (Request.QueryString["ID"].ToString() != "")
            {
                objProjectBL.ProjectCode = Request.QueryString["ID"].ToString();

            }

            objProjectBL.Filename_Variations = txtFilename_Variations.Text;
            objProjectBL.ApprovalDocumentVariations = DatetimenowEX + txtApprovalDocumentVariations.Text;
            if (fup_Variations.HasFile)
            {

                objProjectBL.Variations_FilePath = DatetimenowEX + txtFilename_Variations.Text + "_" + fup_Variations.FileName.ToString() + System.IO.Path.GetExtension(fup_Variations.FileName);
            }
            if (fup_Variations.HasFile)
            {
                fup_Variations.SaveAs(Server.MapPath("~\\UploadedFiles\\" + DatetimenowEX + txtFilename_Variations.Text + "_" + fup_Variations.FileName.ToString() + System.IO.Path.GetExtension(fup_Variations.FileName)));
            }
            if (FupApprovalDocumentVariations.HasFile)
            {

                objProjectBL.ApprovalDocumentVariations_FilePath = DatetimenowEX + txtApprovalDocumentVariations.Text + "_" + FupApprovalDocumentVariations.FileName.ToString() + System.IO.Path.GetExtension(FupApprovalDocumentVariations.FileName);
            }
            if (FupApprovalDocumentVariations.HasFile)
            {
                fup_Variations.SaveAs(Server.MapPath("~\\UploadedFiles\\" + DatetimenowEX + txtApprovalDocumentVariations.Text + "_" + FupApprovalDocumentVariations.FileName.ToString() + System.IO.Path.GetExtension(FupApprovalDocumentVariations.FileName)));
            }
            if (btn_Variations.Text == "Save")
            {
                if (objProjectBL.Variations_Price_Adjustments(con, ProjectBL.eLoadSp.INSERT_Variations_Price_Adjustments))
                {
                    //page_Other.Attributes.Remove("panel-collapse panel-body collapse out");
                    //page_Other.Attributes.Add("class", "panel-collapse panel-body collapse in");
                    txtFilename_Variations.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Variations Price Adjustments Details has been Saved Successfully');", true);
                    BindGridVariations();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Filed to Save Variations Price Adjustments');", true);
                }
            }
            else
            {
                //objProjectBL.Letter_ID = Convert.ToString(ViewState["Letter_ID"]);
                //if (objProjectBL.FreshLetterupdate_letRecFrom_Dept(con, ProjectBL.eLoadSp.UPDATE_FreshLetter_BY_ID_letRecFrom_Dept))
                //{

                //    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Variations Price Adjustments Details has been updated successfully.');", true);
                //}
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    protected void btnCancelVariations_Click(object sender, EventArgs e)
    {


    }
    protected void btn_VendorCredentials_Click(object sender, EventArgs e)
    {

        try
        {
            ProjectBL objProjectBL = new ProjectBL();
            objProjectBL.ApprovalDocumentsCredentialsApproval = txtApprovalDocumentsCredentialsApproval.Text;
            objProjectBL.Filename_Vendor_Credentials = DatetimenowEX + txtFilename_VendorCredentials.Text;
            if (Request.QueryString["ID"].ToString() != "")
            {
                objProjectBL.ProjectCode = Request.QueryString["ID"].ToString();

            }
            if (fupFilename_VendorCredentials.HasFile)
            {

                objProjectBL.Vendor_Credentials_FilePath = DatetimenowEX + txtFilename_VendorCredentials.Text + System.IO.Path.GetExtension(fupFilename_VendorCredentials.FileName);
            }
            if (fupFilename_VendorCredentials.HasFile)
            {
                fupFilename_VendorCredentials.SaveAs(Server.MapPath("~\\UploadedFiles\\" + DatetimenowEX + txtFilename_VendorCredentials.Text + System.IO.Path.GetExtension(fupFilename_VendorCredentials.FileName)));
            }
            if (fupApprovalDocumentsCredentialsApproval.HasFile)
            {

                objProjectBL.ApprovalDocumentsCredentialsApproval_FilePath = DatetimenowEX + txtApprovalDocumentsCredentialsApproval.Text + System.IO.Path.GetExtension(fupApprovalDocumentsCredentialsApproval.FileName);
            }
            if (fupApprovalDocumentsCredentialsApproval.HasFile)
            {
                fupApprovalDocumentsCredentialsApproval.SaveAs(Server.MapPath("~\\UploadedFiles\\" + DatetimenowEX + txtApprovalDocumentsCredentialsApproval.Text + System.IO.Path.GetExtension(fupApprovalDocumentsCredentialsApproval.FileName)));
            }
            if (btn_VendorCredentials.Text == "Save")
            {
                if (objProjectBL.Vendor_Credentials(con, ProjectBL.eLoadSp.INSERT_Vendor_Credentials))
                {
                    //page_Other.Attributes.Remove("panel-collapse panel-body collapse out");
                    //page_Other.Attributes.Add("class", "panel-collapse panel-body collapse in");
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Vendor Credentials Details has been Saved Successfully');", true);
                    BindGridCredentials_Approvals();
                    txtFilename_VendorCredentials.Text = "";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Filed to Save Vendor Credentials');", true);
                }
            }
            else
            {
                //objProjectBL.Letter_ID = Convert.ToString(ViewState["Letter_ID"]);
                //if (objProjectBL.FreshLetterupdate_letRecFrom_Dept(con, ProjectBL.eLoadSp.UPDATE_FreshLetter_BY_ID_letRecFrom_Dept))
                //{

                //    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Variations Price Adjustments Details has been updated successfully.');", true);
                //}
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    protected void btnCancelVendorCredentials_Click(object sender, EventArgs e)
    {

    }

    protected void btnSave_ContractAgreement_Click(object sender, EventArgs e)
    {

        try
        {
            ProjectBL objProjectBL = new ProjectBL();
            objProjectBL.Filename_ContractAgreement = txt_Filename_ContractAgreement.Text;
            if (fupContractAgreement.HasFile)
            {

                objProjectBL.ContractAgreement_FilePath = DatetimenowEX + txt_Filename_ContractAgreement.Text + System.IO.Path.GetExtension(fupContractAgreement.FileName);
            }
            if (fupContractAgreement.HasFile)
            {
                fupContractAgreement.SaveAs(Server.MapPath("~\\UploadedFiles\\" + DatetimenowEX + txt_Filename_ContractAgreement.Text + System.IO.Path.GetExtension(fupContractAgreement.FileName)));
            }
            if (Request.QueryString["ID"] != "")
            {
                objProjectBL.ProjectCode = Request.QueryString["ID"];
            }

            if (btn_Variations.Text == "Save")
            {
                if (objProjectBL.Contract_Agreement(con, ProjectBL.eLoadSp.INSERT_Contract_Agreement))
                {
                    //page_Other.Attributes.Remove("panel-collapse panel-body collapse out");
                    //page_Other.Attributes.Add("class", "panel-collapse panel-body collapse in");
                    txt_Filename_ContractAgreement.Text = "";
                    BindGridContract_Agreement();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Contract Agreement Details has been Saved Successfully');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Filed to Save Contract Agreement Credentials');", true);
                }
            }
            else
            {
                //objProjectBL.Letter_ID = Convert.ToString(ViewState["Letter_ID"]);
                //if (objProjectBL.FreshLetterupdate_letRecFrom_Dept(con, ProjectBL.eLoadSp.UPDATE_FreshLetter_BY_ID_letRecFrom_Dept))
                //{

                //    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Variations Price Adjustments Details has been updated successfully.');", true);
                //}
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    protected void btnCancelContractAgreement_Click(object sender, EventArgs e)
    {

    }

    protected void btnSave_BillofQuantity_Click(object sender, EventArgs e)
    {

        try
        {
            ProjectBL objProjectBL = new ProjectBL();
            objProjectBL.Filename_BillofQuantity = txtBillofQuantity.Text;
            if (fupBillofQuantity.HasFile)
            {

                objProjectBL.BillofQuantity_FilePath = DatetimenowEX + txtBillofQuantity.Text + System.IO.Path.GetExtension(fupBillofQuantity.FileName);
            }
            if (fupBillofQuantity.HasFile)
            {
                fupBillofQuantity.SaveAs(Server.MapPath("~\\UploadedFiles\\" + DatetimenowEX + txtBillofQuantity.Text + System.IO.Path.GetExtension(fupBillofQuantity.FileName)));
            }
            if (Request.QueryString["ID"] != "")
            {
                objProjectBL.ProjectCode = Request.QueryString["ID"];
            }

            if (btnSaveBillofQuantity.Text == "Save")
            {
                if (objProjectBL.Bill_of_Quantity(con, ProjectBL.eLoadSp.INSERT_Bill_of_Quantity))
                {
                    //page_Other.Attributes.Remove("panel-collapse panel-body collapse out");
                    //page_Other.Attributes.Add("class", "panel-collapse panel-body collapse in");
                    txtBillofQuantity.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Bill of Quantity Details has been Saved Successfully');", true);
                    BindGridQuantity();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Filed to Save Bill of Quantity Credentials');", true);
                }
            }
            else
            {
                //objProjectBL.Letter_ID = Convert.ToString(ViewState["Letter_ID"]);
                //if (objProjectBL.FreshLetterupdate_letRecFrom_Dept(con, ProjectBL.eLoadSp.UPDATE_FreshLetter_BY_ID_letRecFrom_Dept))
                //{

                //    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Variations Price Adjustments Details has been updated successfully.');", true);
                //}
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    protected void btnCancel_BillofQuantity_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_Drawings_Click(object sender, EventArgs e)
    {

        try
        {
            ProjectBL objProjectBL = new ProjectBL();
            objProjectBL.Filename_Drawings = txtDrawings.Text;
            objProjectBL.ApprovalDocumentsDrawings = DatetimenowEX + txtApprovalDocumentsDrawings.Text;
            if (fupDrawings.HasFile)
            {

                objProjectBL.Drawings_FilePath = DatetimenowEX + txtDrawings.Text + System.IO.Path.GetExtension(fupDrawings.FileName);
            }
            if (fupDrawings.HasFile)
            {
                fupDrawings.SaveAs(Server.MapPath("~\\UploadedFiles\\" + DatetimenowEX + txtDrawings.Text + System.IO.Path.GetExtension(fupDrawings.FileName)));
            }
            if (fupApprovalDocumentsDrawings.HasFile)
            {

                objProjectBL.ApprovalDocumentsDrawings_FilePath = DatetimenowEX + txtApprovalDocumentsDrawings.Text + System.IO.Path.GetExtension(fupApprovalDocumentsDrawings.FileName);
            }
            if (fupApprovalDocumentsDrawings.HasFile)
            {
                fupDrawings.SaveAs(Server.MapPath("~\\UploadedFiles\\" + DatetimenowEX + txtApprovalDocumentsDrawings.Text + System.IO.Path.GetExtension(fupApprovalDocumentsDrawings.FileName)));
            }
            if (Request.QueryString["ID"] != "")
            {
                objProjectBL.ProjectCode = Request.QueryString["ID"];
            }

            if (btn_Save_Drawings.Text == "Save")
            {
                if (objProjectBL.Drawings(con, ProjectBL.eLoadSp.INSERT_Drawings))
                {
                    //page_Other.Attributes.Remove("panel-collapse panel-body collapse out");
                    //page_Other.Attributes.Add("class", "panel-collapse panel-body collapse in");
                    txtDrawings.Text = "";
                    BindGridDrawings();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Drawings Details has been Saved Successfully');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Filed to Save Drawings Credentials');", true);
                }
            }
            else
            {
                //objProjectBL.Letter_ID = Convert.ToString(ViewState["Letter_ID"]);
                //if (objProjectBL.FreshLetterupdate_letRecFrom_Dept(con, ProjectBL.eLoadSp.UPDATE_FreshLetter_BY_ID_letRecFrom_Dept))
                //{

                //    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Variations Price Adjustments Details has been updated successfully.');", true);
                //}
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    protected void btnCancel_Drawings_Click(object sender, EventArgs e)
    {

    }
    private void BindGridDrawings()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.ProjectCode = Request.QueryString["ID"];
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_GridDrawings, ref ds);
            GridDrawings.DataSource = ds;
            GridDrawings.DataBind();


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    private void BindGridVariations()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.ProjectCode = Request.QueryString["ID"];
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_GridVariations, ref ds);
            GridVariations.DataSource = ds;
            GridVariations.DataBind();


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    private void BindGridQuantity()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            if (Request.QueryString["ID"] != "")
            {
                objProjectBL.ProjectCode = Request.QueryString["ID"];
            }

            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_Bill_of_Quantity, ref ds);
            GridQuantity.DataSource = ds;
            GridQuantity.DataBind();


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    private void BindGridCredentials_Approvals()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.ProjectCode = Request.QueryString["ID"];
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_GridVendorCredentialsApprovals, ref ds);
            GridVendorCredentialsApprovals.DataSource = ds;
            GridVendorCredentialsApprovals.DataBind();


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    private void BindGridContract_Agreement()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.ProjectCode = Request.QueryString["ID"];
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_Contract_Ag, ref ds);
            GridContractAgreement.DataSource = ds;
            GridContractAgreement.DataBind();


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void GridVariations_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.ID = Convert.ToInt32(e.Record["ID"]);

            if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_Variations))
            {
                BindReplayToLetterGrid_letRecFrom_Dept();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Variations info has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete the Variations details');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void GridQuantity_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.ID = Convert.ToInt32(e.Record["ID"]);

            if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_Quantity))
            {
                BindGridQuantity();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Quantity info has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete the Quantity details');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void GridDrawings_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.ID = Convert.ToInt32(e.Record["ID"]);

            if (objProjectBL.delete(con, ProjectBL.eLoadSp.Delete_Drawings))
            {
                BindGridDrawings();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Drawings info has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete the Drawings details');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void GridDVendor_Credentials_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.ID = Convert.ToInt32(e.Record["ID"]);

            if (objProjectBL.delete(con, ProjectBL.eLoadSp.Delete_Vendor_Credentials))
            {
                BindGridCredentials_Approvals();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Vendor Credentials info has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete the Vendor Credentials details');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void btnSitelocation_Click(object sender, EventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.Proj_Code = txtProjectID.Text.Trim();
            objProjectBL.SiteContactPerson = txtSiteContactPerson.Text.Trim();
            objProjectBL.SiteMobileNumber = txtSiteMobileNumber.Text.Trim();
            objProjectBL.SiteAddress = txtSiteAddress.Text.Trim();
            if (objProjectBL.SiteLocationinsert(con, ProjectBL.eLoadSp.INSERT_SITE_LOCATION))
            {
                BindSitelocation();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Site Location details has been Created');", true);
                txtSiteContactPerson.Text = "";
                txtSiteMobileNumber.Text = "";
                txtSiteAddress.Text = "";

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to add the Contact details');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void btnCancelSitelocation_Click(object sender, EventArgs e)
    {
        txtSiteContactPerson.Text = "";
        txtSiteMobileNumber.Text = "";
        txtSiteAddress.Text = "";

    }

    private void BindSitelocation()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.Task = "Select_All_Sitelocation_By_Project_Code";
            objProjectBL.Proj_Code = txtProjectID.Text.Trim();
            objProjectBL.load(con, ProjectBL.eLoadSp.SITE_LOCATION_OPERATIONS, ref ds);
            Grid_Sitelocation.DataSource = ds;
            Grid_Sitelocation.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void GridSitelocation_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.Site_ID = Convert.ToInt32(e.Record["Site_ID"]);
            objProjectBL.Task = "Delete_Sitelocation_By_ID";
            if (objProjectBL.delete(con, ProjectBL.eLoadSp.SITE_LOCATION_OPERATIONS))
            {
                BindSitelocation();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Site location info has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete the Site location details');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void btnSaveInsurance_Click(object sender, EventArgs e)
    {

        try
        {
            if (fupInsuranceCopy.HasFile)
            {
                objProjectBL = new ProjectBL();

                if (Request.QueryString["ID"].ToString() != "")
                {
                    objProjectBL.Project_Code = Request.QueryString["ID"].ToString();

                }
                if (ddlInsuranceType.SelectedItem.Text != "-Select-")
                {
                    objProjectBL.Insurance_Type = ddlInsuranceType.SelectedItem.Text;
                }

                objProjectBL.Insurer_Name = txtInsurerName.Text;
                objProjectBL.Policy_Number = txtPolicyNumber.Text;
                objProjectBL.Policy_Amount = Convert.ToDecimal(txtPolicyAmount.Text);
                objProjectBL.Policy_Date = Convert.ToDateTime(txtPolicyDate.Text);
                objProjectBL.Insurance_Original_Expiry_Date = Convert.ToDateTime(txtInsurerOriginalExpiryDate.Text);

                if (fupInsuranceCopy.HasFile)
                {
                    objProjectBL.Insurance_Copy_FilePath = DatetimenowEX + "_" + fupInsuranceCopy.FileName.ToString() + System.IO.Path.GetExtension(fupInsuranceCopy.FileName);
                }
                if (fupInsuranceCopy.HasFile)
                {
                    fupInsuranceCopy.SaveAs(Server.MapPath("~\\UploadedFiles\\" + DatetimenowEX + "_" + fupInsuranceCopy.FileName.ToString() + System.IO.Path.GetExtension(fupInsuranceCopy.FileName)));
                }
                if (btnSaveBG.Text == "Save")
                {
                    if (objProjectBL.Insert_Insurance(con, ProjectBL.eLoadSp.INSERT_INSURANCE_DOCUMENTS))
                    {
                        BindGridInsuranceDoc();
                        //page_Other.Attributes.Remove("panel-collapse panel-body collapse out");
                        //page_Other.Attributes.Add("class", "panel-collapse panel-body collapse in");
                        ddlInsuranceType.SelectedIndex = 0;
                        txtInsurerName.Text = "";
                        txtPolicyNumber.Text = "";
                        txtPolicyAmount.Text = "";
                        txtPolicyDate.Text = "";
                        txtInsurerOriginalExpiryDate.Text = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Insurance Documents Details has been Saved Successfully');", true);
                        BindGridVariations();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Filed to Save Insurance Documents');", true);
                    }
                }
                else
                {
                    //objProjectBL.Letter_ID = Convert.ToString(ViewState["Letter_ID"]);
                    //if (objProjectBL.FreshLetterupdate_letRecFrom_Dept(con, ProjectBL.eLoadSp.UPDATE_FreshLetter_BY_ID_letRecFrom_Dept))
                    //{

                    //    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Variations Price Adjustments Details has been updated successfully.');", true);
                    //}
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please Select Insurance Copy');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    protected void btnCancelInsurance_Click(object sender, EventArgs e)
    {
        ddlInsuranceType.SelectedIndex = 0;
        txtInsurerName.Text = "";
        txtPolicyNumber.Text = "";
        txtPolicyAmount.Text = "";
        txtPolicyDate.Text = "";
        txtInsurerOriginalExpiryDate.Text = "";

    }

    protected void btnSaveBG_Click(object sender, EventArgs e)
    {

        try
        {
            objProjectBL = new ProjectBL();
            if (fupBGCopy.HasFile)
            {
                if (Request.QueryString["ID"].ToString() != "")
                {
                    objProjectBL.Project_Code = Request.QueryString["ID"].ToString();

                }
                if (ddlBGType.SelectedItem.Text != "")
                {
                    objProjectBL.BG_Type = ddlBGType.SelectedItem.Text;
                }
                objProjectBL.BG_Beificiary = txtBGBeificiary.Text;
                objProjectBL.BG_Number = txtBGNumber.Text;
                objProjectBL.BG_Amount = Convert.ToDecimal(txtBGAmount.Text);
                objProjectBL.BG_Date = Convert.ToDateTime(txtBGDate.Text);
                objProjectBL.Claim_Date = Convert.ToDateTime(txtBGClaimDate.Text);
                objProjectBL.Original_Expiry_Date = Convert.ToDateTime(txtBGOriginalExpiryDate.Text);
                //objProjectBL.Claim_Date = Convert.ToDateTime(txtBGClaimDate.Text);
                //if (fupBGCopy.HasFile)
                //{
                //    objProjectBL.BG_Copy_FilePath = DatetimenowEX + "_" + fupBGCopy.FileName.ToString() + System.IO.Path.GetExtension(fupBGCopy.FileName);
                //}
                //if (fupBGCopy.HasFile)
                //{
                //    fupBGCopy.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "_" + fupBGCopy.FileName.ToString() + System.IO.Path.GetExtension(fupBGCopy.FileName)));
                //}
                if (fupBGCopy.HasFile)
                {

                    objProjectBL.BG_Copy_FilePath = fupBGCopy.FileName + DatetimenowEX + "_" + System.IO.Path.GetExtension(fupBGCopy.FileName);
                }
                if (fupBGCopy.HasFile)
                {
                    fupBGCopy.SaveAs(Server.MapPath("~\\UploadedFiles\\" + fupBGCopy.FileName + DatetimenowEX + "_" + System.IO.Path.GetExtension(fupBGCopy.FileName)));
                }
                if (btnSaveBG.Text == "Save")
                {
                    if (objProjectBL.Insert_BG(con, ProjectBL.eLoadSp.INSERT_BG_DOCUMENTS))
                    {
                        BindGridBG_DOC();
                        //page_Other.Attributes.Remove("panel-collapse panel-body collapse out");
                        //page_Other.Attributes.Add("class", "panel-collapse panel-body collapse in");
                        ddlBGType.SelectedIndex = 0;
                        txtBGBeificiary.Text = "";
                        txtBGNumber.Text = "";
                        txtBGAmount.Text = "";
                        txtBGDate.Text = "";
                        txtBGOriginalExpiryDate.Text = "";
                        txtBGClaimDate.Text = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('BG Documents Details has been Saved Successfully');", true);
                        BindGridVariations();
                    }
                    //    else
                    //    {
                    //        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Filed to Save BG Documents');", true);
                    //    }
                    //}
                    else
                    {
                        //objProjectBL.Letter_ID = Convert.ToString(ViewState["Letter_ID"]);
                        //if (objProjectBL.FreshLetterupdate_letRecFrom_Dept(con, ProjectBL.eLoadSp.UPDATE_FreshLetter_BY_ID_letRecFrom_Dept))
                        //{

                        //    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Variations Price Adjustments Details has been updated successfully.');", true);
                        //}
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please Select BG Copy File');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    protected void btnCancelBG_Click(object sender, EventArgs e)
    {
        ddlBGType.SelectedIndex = 0;
        txtBGBeificiary.Text = "";
        txtBGNumber.Text = "";
        txtBGAmount.Text = "";
        txtBGDate.Text = "";
        txtBGOriginalExpiryDate.Text = "";
        txtBGClaimDate.Text = "";

    }
    private void BindGridInsuranceDoc()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            if (Request.QueryString["ID"] != "")
            {
                objProjectBL.ProjectCode = Request.QueryString["ID"];
            }

            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_INSURANCE_DOC, ref ds);
            GridInsuranceDoc.DataSource = ds;
            GridInsuranceDoc.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    private void BindGridBG_DOC()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            if (Request.QueryString["ID"] != "")
            {
                objProjectBL.ProjectCode = Request.QueryString["ID"];
            }

            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_BG_DOC_ALL, ref ds);
            GridBGDoc.DataSource = ds;
            GridBGDoc.DataBind();


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void GridBGDoc_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.ID = Convert.ToInt32(e.Record["ID"]);

            if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_BG_DOC))
            {
                BindGridBG_DOC();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('BG info has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete the BG details');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void GridInsuranceDoc_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.ID = Convert.ToInt32(e.Record["ID"]);

            if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_INSURANCE_DOC))
            {
                BindGridBG_DOC();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Insurance info has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete the Insurance details');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void lnkBtnLicenseType_Click(object sender, EventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            ViewState["License_Code"] = ((LinkButton)sender).CommandName.ToString();
            objProjectBL.License_Code = ViewState["License_Code"].ToString();
            if (objProjectBL.loadLicenseType(con, ProjectBL.eLoadSp.SELECT_LicenseType_BY_ID))
            {
                txtLicenseName.Text = objProjectBL.LicenseName.ToString();
                txtLicenseTypeCode.Text = objProjectBL.LicenseCode.ToString();
                BtnLCTSave.Text = "Update";
                //  ModalPopupLicenseType.Show();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    protected void btnLicense_Click(object sender, EventArgs e)
    {
        try
        {
            ProjectBL objProjectBL = new ProjectBL();
            objProjectBL.Issuer_Name = txt_issuername.Text;
            if (txtexpdate.Text.Trim() != string.Empty)
            {
                objProjectBL.Original_Expiry_Date = Convert.ToDateTime(txtexpdate.Text);
            }
            else
            {
                objProjectBL.Original_Expiry_Date = null;
            }

            if (txtlicensedate.Text.Trim() != string.Empty)
            {
                objProjectBL.License_Date = Convert.ToDateTime(txtlicensedate.Text);
            }
            else
            {
                objProjectBL.License_Date = null;
            }

            objProjectBL.License_Number = txt_LicenseNumber.Text;


            if (txtamt.Text.Trim() != string.Empty)
            {
                objProjectBL.Amount = Convert.ToInt32(txtamt.Text);
            }
            else
            {
                objProjectBL.Amount = null;
            }
            if (Request.QueryString["ID"] != "")
            {
                objProjectBL.Project_Code = Request.QueryString["ID"];
            }
            //if (FileuploadLicencecopy.HasFile)
            //{
            //    //objProjectBL.License_Copy_FilePath = DatetimenowEX + "_" + FileuploadLicencecopy.FileName.ToString() + System.IO.Path.GetExtension(FileuploadLicencecopy.FileName);
            //    objProjectBL.License_Copy_FilePath = "_GSTRegistration" + System.IO.Path.GetExtension(FileuploadLicencecopy.FileName);
            //}
            //if (FileuploadLicencecopy.HasFile)
            //{
            //    //FileuploadLicencecopy.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "_" + FileuploadLicencecopy.FileName.ToString() + System.IO.Path.GetExtension(FileuploadLicencecopy.FileName)));
            //    FileuploadLicencecopy.SaveAs(Path.Combine(Server.MapPath("~\\UploadedFiles\\" + "_" + FileuploadLicencecopy.FileName.ToString() + System.IO.Path.GetExtension(FileuploadLicencecopy.FileName))));
            //}


            if (FileuploadLicencecopy.HasFile)
            {

                objProjectBL.License_Copy_FilePath = FileuploadLicencecopy.FileName + DatetimenowEX + "_" + System.IO.Path.GetExtension(FileuploadLicencecopy.FileName);
            }
            if (FileuploadLicencecopy.HasFile)
            {
                FileuploadLicencecopy.SaveAs(Server.MapPath("~\\UploadedFiles\\" + FileuploadLicencecopy.FileName + DatetimenowEX + "_" + System.IO.Path.GetExtension(FileuploadLicencecopy.FileName)));
            }

            if (ddlLicensetype.SelectedItem.Text != "")
            {
                objProjectBL.License_Type = ddlLicensetype.SelectedItem.Text;
            }





            if (btnLicense.Text == "Save")
            {
                if (objProjectBL.License(con, ProjectBL.eLoadSp.INSERT_License))
                {
                    //page_Other.Attributes.Remove("panel-collapse panel-body collapse out");
                    //page_Other.Attributes.Add("class", "panel-collapse panel-body collapse in");
                    txtlicensedate.Text = "";
                    txtamt.Text = "";
                    txt_LicenseNumber.Text = "";
                    txt_issuername.Text = "";
                    txtexpdate.Text = "";
                    ddlLicensetype.SelectedIndex = -1;


                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('License Details has been Saved Successfully');", true);
                    BindGridLisence();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Filed to Save License Credentials');", true);
                }
            }
            else
            {
                //objProjectBL.Letter_ID = Convert.ToString(ViewState["Letter_ID"]);
                //if (objProjectBL.FreshLetterupdate_letRecFrom_Dept(con, ProjectBL.eLoadSp.UPDATE_FreshLetter_BY_ID_letRecFrom_Dept))
                //{

                //    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Variations Price Adjustments Details has been updated successfully.');", true);
                //}
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    private void BindGridLisence()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            if (Request.QueryString["ID"] != "")
            {
                //var ProjectCode = Request.QueryString["ID"].ToString();
                //objProjectBL.ProjectCode = Convert.ToString(ViewState["Project_Code"]);
                objProjectBL.ProjectCode = Request.QueryString["ID"];
            }

            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_Lisence, ref ds);
            gridlicense.DataSource = ds;
            gridlicense.DataBind();





        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    private void BindGridExtLisence()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            if (Request.QueryString["ID"] != "")
            {
                objProjectBL.ProjectCode = Request.QueryString["ID"];
            }

            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_Ext_Lisence, ref ds);
            gridextlicense.DataSource = ds;
            gridextlicense.DataBind();


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void gridlicense_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.ID = Convert.ToInt32(e.Record["ID"]);

            if (objProjectBL.delete(con, ProjectBL.eLoadSp.delete_License))
            {
                BindGridLisence();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Contract Agreement info has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete the Contract Agreement details');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void lnkBGAddExtension_Click(object sender, EventArgs e)
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.ID = Convert.ToInt32(((LinkButton)sender).CommandName);
            hdnBGExtensionID.Value = Convert.ToString(objProjectBL.ID);
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_BG_EXTENSION_BY_ID, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridBG_Extension.DataSource = ds;
                GridBG_Extension.DataBind();
            }

            ModalWOSubItem.Show();

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSaveBGExtension_Click(object sender, EventArgs e)
    {
        try
        {
            ProjectBL objProjectBL = new ProjectBL();
            objProjectBL.BGExtension = txtBGExtension.Text;
            if (txtBGValidupto.Text.Trim() != string.Empty)
            {
                objProjectBL.Valid_Upto = Convert.ToDateTime(txtBGValidupto.Text);
            }
            else
            {
                objProjectBL.Valid_Upto = null;
            }
            objProjectBL.BG_ID = Convert.ToInt32(hdnBGExtensionID.Value);
            if (fupBGExtensionCopy.HasFile)
            {
                objProjectBL.Extension_Copy = DatetimenowEX + "_" + fupBGExtensionCopy.FileName.ToString() + System.IO.Path.GetExtension(fupBGExtensionCopy.FileName);
            }
            if (fupBGExtensionCopy.HasFile)
            {
                fupBGExtensionCopy.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "_" + fupBGExtensionCopy.FileName.ToString() + System.IO.Path.GetExtension(fupBGExtensionCopy.FileName)));
            }

            if (btnSaveBGExtension.Text == "Save")
            {
                if (objProjectBL.BG_Extension(con, ProjectBL.eLoadSp.INSERT_BG_EXTENSION))
                {

                    hdnBGExtensionID.Value = "";
                    txtBGValidupto.Text = "";
                    txtBGExtension.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('BG Extension has been Saved Successfully');", true);
                    BindGridLisence();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Filed to Save BG Extension Credentials');", true);
                }
            }
            else
            {
                //objProjectBL.Letter_ID = Convert.ToString(ViewState["Letter_ID"]);
                //if (objProjectBL.FreshLetterupdate_letRecFrom_Dept(con, ProjectBL.eLoadSp.UPDATE_FreshLetter_BY_ID_letRecFrom_Dept))
                //{

                //    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Variations Price Adjustments Details has been updated successfully.');", true);
                //}
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void btnCancelBGExtension_Click(object sender, EventArgs e)
    {

    }
    protected void GridBGExtension_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.ID = Convert.ToInt32(e.Record["ID"]);

            if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_BG_DOC))
            {
                BindGridBG_DOC();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('BG info has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete the BG details');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void lnkInsuranceRenewal_Click(object sender, EventArgs e)
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.ID = Convert.ToInt32(((LinkButton)sender).CommandName);
            hdnInsuranceR.Value = Convert.ToString(objProjectBL.ID);
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_INSURANCERENEWAL_BY_ID, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Grid_Insurance_Renewal.DataSource = ds;
                Grid_Insurance_Renewal.DataBind();
            }

            ModalINSRItem.Show();

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void btnSaveInsuranceRenewal_Click(object sender, EventArgs e)
    {
        try
        {
            ProjectBL objProjectBL = new ProjectBL();
            objProjectBL.Insurance_Name = txtInsuranceName.Text;
            if (txtInsuranceValidUpto.Text.Trim() != string.Empty)
            {
                objProjectBL.Insurance_Renewal_Valid_Upto = Convert.ToDateTime(txtInsuranceValidUpto.Text);
            }
            else
            {
                objProjectBL.Insurance_Renewal_Valid_Upto = null;
            }
            objProjectBL.Insurance_ID = Convert.ToInt32(hdnInsuranceR.Value);
            if (FupInsuranceRenewalCopy.HasFile)
            {
                objProjectBL.Insurance_Renewal_Copy = DatetimenowEX + "_" + FupInsuranceRenewalCopy.FileName.ToString() + System.IO.Path.GetExtension(FupInsuranceRenewalCopy.FileName);
            }
            if (FupInsuranceRenewalCopy.HasFile)
            {
                FupInsuranceRenewalCopy.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "_" + FupInsuranceRenewalCopy.FileName.ToString() + System.IO.Path.GetExtension(FupInsuranceRenewalCopy.FileName)));
            }

            if (btnSaveInsuranceRenewal.Text == "Save")
            {
                if (objProjectBL.Insurance_Renewal(con, ProjectBL.eLoadSp.INSERT_INSURANCE_RENEWAL))
                {

                    hdnInsuranceR.Value = "";
                    txtInsuranceName.Text = "";
                    txtInsuranceValidUpto.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Insurance Renewal has been Saved Successfully');", true);
                    BindGridLisence();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Filed to Save Insurance Renewal Credentials');", true);
                }
            }
            else
            {
                //objProjectBL.Letter_ID = Convert.ToString(ViewState["Letter_ID"]);
                //if (objProjectBL.FreshLetterupdate_letRecFrom_Dept(con, ProjectBL.eLoadSp.UPDATE_FreshLetter_BY_ID_letRecFrom_Dept))
                //{

                //    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Variations Price Adjustments Details has been updated successfully.');", true);
                //}
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void btnCancelInsuranceRenewal_Click(object sender, EventArgs e)
    {

    }
    protected void lnklicenseExtensionExtension_Click(object sender, EventArgs e)
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.ID = Convert.ToInt32(((LinkButton)sender).CommandName);
            hdnLicenses.Value = Convert.ToString(objProjectBL.ID);
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_EXTENSION_LICENSE_BY_ID, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Grid_ExtensionLicense.DataSource = ds;
                Grid_ExtensionLicense.DataBind();
            }

            ModalLicensesItem.Show();

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void btnLicenseSave_Click(object sender, EventArgs e)
    {
        try
        {
            ProjectBL objProjectBL = new ProjectBL();
            objProjectBL.License_Extension = txtExtensionLicenses.Text;
            if (txtValidUptoLicensesExtension.Text.Trim() != string.Empty)
            {
                objProjectBL.License_Valid_Upto = Convert.ToDateTime(txtValidUptoLicensesExtension.Text);
            }
            else
            {
                objProjectBL.License_Valid_Upto = null;
            }
            objProjectBL.License_ID = Convert.ToInt32(hdnLicenses.Value);
            if (fupLicenseExtensionCopy.HasFile)
            {
                objProjectBL.License_Extension_Copy = DatetimenowEX + "_" + fupLicenseExtensionCopy.FileName.ToString() + System.IO.Path.GetExtension(fupLicenseExtensionCopy.FileName);
            }
            if (fupLicenseExtensionCopy.HasFile)
            {
                fupLicenseExtensionCopy.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "_" + fupLicenseExtensionCopy.FileName.ToString() + System.IO.Path.GetExtension(fupLicenseExtensionCopy.FileName)));
            }

            if (btnLicenseSave.Text == "Save")
            {
                if (objProjectBL.Extension_License(con, ProjectBL.eLoadSp.INSERT_EXTENSION_LICENSE))
                {

                    hdnLicenses.Value = "";
                    txtExtensionLicenses.Text = "";
                    txtValidUptoLicensesExtension.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('License Extension has been Saved Successfully');", true);
                    BindGridLisence();
                    BindGridExtLisence();
                    gridextlicense.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Filed to Save License Extension Credentials');", true);
                }
            }
            else
            {
                //objProjectBL.Letter_ID = Convert.ToString(ViewState["Letter_ID"]);
                //if (objProjectBL.FreshLetterupdate_letRecFrom_Dept(con, ProjectBL.eLoadSp.UPDATE_FreshLetter_BY_ID_letRecFrom_Dept))
                //{

                //    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Variations Price Adjustments Details has been updated successfully.');", true);
                //}
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void btnLicenseCancel_Click(object sender, EventArgs e)
    {
        hdnLicenses.Value = "";
        txtExtensionLicenses.Text = "";
        txtValidUptoLicensesExtension.Text = "";
    }
    protected void BtnLCTSave_Click(object sender, EventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.LicenseName = txtLicenseName.Text;
            objProjectBL.LicenseCode = txtLicenseTypeCode.Text;
            if (BtnLCTSave.Text == "Save")
            {
                if (objProjectBL.LicenseTypeInsert(con, ProjectBL.eLoadSp.INSERT_LicenseType))
                {
                    BindLicenseType();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('LicenseType has been Created');", true);
                    txtLicenseName.Text = string.Empty;
                    txtLicenseTypeCode.Text = string.Empty;
                    //ModalCompanyPopup.Show();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('LicenseType already exists, Pls try another ID');", true);
                }
            }
            else
            {
                if (objProjectBL.LicenseTypeUpdate(con, ProjectBL.eLoadSp.UPDATE_licensetype_BY_ID))
                {
                    BindCompany();
                    txtLicenseName.Text = string.Empty;
                    txtLicenseTypeCode.Text = string.Empty;
                    BtnLCTSave.Text = "Save";
                    //ModalCompanyPopup.Show();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('LicenseType Details has been updated successfully.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BtnLCTCancel_Click(object sender, EventArgs e)
    {
        txtLicenseName.Text = string.Empty;
        txtLicenseTypeCode.Text = string.Empty;
    }

    protected void BindLicenseType()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_LicenseType_ALL, ref ds);

            GridLisenceType.DataSource = ds;
            GridLisenceType.DataBind();

            ddlLicensetype.DataSource = ds;
            ddlLicensetype.DataValueField = "License_Code";
            ddlLicensetype.DataTextField = "License_Name";
            ddlLicensetype.DataBind();
            ddlLicensetype.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void GridLisenceType_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.LicenseCode = e.Record["License_Code"].ToString();

            if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_LicenseType))
            {
                BindLicenseType();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('License Type has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This License type cannot be deleted as it is already in use.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void lnkBtnEditBGType_Click(object sender, EventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            ViewState["BG_ID"] = ((LinkButton)sender).CommandName.ToString();
            objProjectBL.ID = Convert.ToInt32(ViewState["BG_ID"].ToString());
            if (objProjectBL.loadBGType(con, ProjectBL.eLoadSp.SELECT_BG_TYPE_BY_ID))
            {
                txtAddBGtype.Text = objProjectBL.AddBGtype.ToString();
                btnSaveBGType.Text = "Update";

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    protected void btnSaveBGType_Click(object sender, EventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.AddBGtype = txtAddBGtype.Text;
            objProjectBL.Project_Code = Convert.ToString(Request.QueryString["ID"]);
            if (btnSaveBGType.Text == "Save")
            {
                if (objProjectBL.BGTypeInsert(con, ProjectBL.eLoadSp.INSERT_BG_TYPE))
                {
                    BindBGType();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('BG Type has been Created');", true);
                    txtAddBGtype.Text = string.Empty;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('BG Type already exists, Pls try another ID');", true);
                }
            }
            else
            {
                if (objProjectBL.BGTypeUpdate(con, ProjectBL.eLoadSp.UPDATE_BG_TYPE_BY_ID))
                {
                    BindBGType();
                    txtAddBGtype.Text = string.Empty;
                    BtnLCTSave.Text = "Save";
                    ModalCompanyPopup.Show();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('BG Type Details has been updated successfully.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void btnCancelBGType_Click(object sender, EventArgs e)
    {
        txtAddBGtype.Text = string.Empty;

    }
    protected void GridBGType_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.ID = Convert.ToInt32(e.Record["ID"].ToString());
            if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_BG_TYPE_BY_ID))
            {
                BindBGType();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('BG Type has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This BG type cannot be deleted as it is already in use.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void BindBGType()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_BG_TYPE_BY_ALL, ref ds); ;
            Grid_BGType.DataSource = ds;
            Grid_BGType.DataBind();

            ddlBGType.DataSource = ds;
            ddlBGType.DataValueField = "ID";
            ddlBGType.DataTextField = "BG_Type";
            ddlBGType.DataBind();
            ddlBGType.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void lnkBtnEditInsuranceType_Click(object sender, EventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            ViewState["Insurance_ID"] = ((LinkButton)sender).CommandName.ToString();
            objProjectBL.ID = Convert.ToInt32(ViewState["Insurance_ID"].ToString());
            if (objProjectBL.loadInsuranceType(con, ProjectBL.eLoadSp.SELECT_BG_TYPE_BY_ID))
            {
                txtAddInsuranceType.Text = objProjectBL.AddInsuranceType.ToString();
                btnSaveInsuranceType.Text = "Update";

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    protected void btnSaveInsuranceType_Click(object sender, EventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.AddInsuranceType = txtAddInsuranceType.Text;
            objProjectBL.Project_Code = Convert.ToString(Request.QueryString["ID"]);
            if (btnSaveInsuranceType.Text == "Save")
            {
                if (objProjectBL.InsuranceTypeInsert(con, ProjectBL.eLoadSp.INSERT_INSURANCE_TYPE))
                {
                    BindInsuranceType();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Insurance Type has been Created');", true);
                    txtAddBGtype.Text = string.Empty;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Insurance Type already exists, Pls try another ID');", true);
                }
            }
            else
            {
                if (objProjectBL.InsuranceTypeUpdate(con, ProjectBL.eLoadSp.UPDATE_INSURANCE_TYPE_BY_ID))
                {
                    BindInsuranceType();
                    txtAddInsuranceType.Text = string.Empty;
                    btnSaveInsuranceType.Text = "Save";
                    //ModalCompanyPopup.Show();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Insurance Type Details has been updated successfully.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void btnCancelInsuranceType_Click(object sender, EventArgs e)
    {
        txtAddInsuranceType.Text = string.Empty;

    }
    protected void GridInsuranceType_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.ID = Convert.ToInt32(e.Record["ID"].ToString());
            if (objProjectBL.delete(con, ProjectBL.eLoadSp.DELETE_INSURANCE_TYPE))
            {
                BindBGType();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Insurance Type has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Insurance type cannot be deleted as it is already in use.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void BindInsuranceType()
    {
        try
        {
            ds = new DataSet();
            objProjectBL = new ProjectBL();
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_TB_INSURANCE_TYPE_ALL, ref ds); ;
            Grid_InsuranceType.DataSource = ds;
            Grid_InsuranceType.DataBind();

            ddlInsuranceType.DataSource = ds;
            ddlInsuranceType.DataValueField = "ID";
            ddlInsuranceType.DataTextField = "Insurance_Type";
            ddlInsuranceType.DataBind();
            ddlInsuranceType.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    //Written by prashanth G: Start
    protected void lnkLetterId_Click(object sender, EventArgs e)
    {

        objProjectBL = new ProjectBL();
        txtLetterID.Text = ((LinkButton)sender).CommandName.ToString();
        rbLetRecFrom_DeptUpdate.SelectedValue = "FreshLetter";
        objProjectBL.Letter_ID = txtLetterID.Text;
        objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_FRESHLETTER_DEPT_DETAILS_BY_ID, ref ds);
        if (ds.Tables[0].Rows.Count > 0)
        {

            txtLetterRefNo.Text = ds.Tables[0].Rows[0]["LetterRefNumber"].ToString();
            txtLetterRecFrom.Text = ds.Tables[0].Rows[0]["LetterRecdFrom"].ToString();
            if (ds.Tables[0].Rows[0]["LetterReceivedDate"].ToString() == string.Empty)
            {
                txtLetterRecDate.Text = string.Empty;
            }
            else
            {
                txtLetterRecDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["LetterReceivedDate"]).ToString("dd-MM-yyyy");
            }

            if (ds.Tables[0].Rows[0]["DateofReceipt"].ToString() == string.Empty)
            {
                txtDateofRecipt.Text = string.Empty;
            }
            else
            {
                txtDateofRecipt.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DateofReceipt"]).ToString("dd-MM-yyyy");
            }

            if (ds.Tables[0].Rows[0]["ReplyByDate"].ToString() == string.Empty)
            {
                txtReplyBydateModal.Text = string.Empty;
            }
            else
            {
                txtReplyBydateModal.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ReplyByDate"]).ToString("dd-MM-yyyy");
            }


            //ddlState_SelectedIndexChanged(null, null);
            //ddlLocation.SelectedValue = ds.Tables[0].Rows[0]["Location"].ToString();
            txtModeOfRecipt.Text = ds.Tables[0].Rows[0]["ModeofRecepit"].ToString();
            txtSubject.Text = ds.Tables[0].Rows[0]["FreshLetterSubject"].ToString();
            ModalLetterItem.Show();
        }


    }



    protected void BtnCancelFreshLetter_Click(object sender, EventArgs e)
    {
        txtLetterID.Text = "";
        txtLetterRecFrom.Text = "";
        txtLetterRecFrom.Text = "";
        txtLetterID.Text = "";
        txtLetterRefNo.Text = "";
        txtLetterRecFrom.Text = "";
        txtLetterRecDate.Text = "";
        txtDateofRecipt.Text = "";
        txtReplyBydateModal.Text = "";
        txtModeOfRecipt.Text = "";
        txtSubject.Text = "";

    }

    protected void BtnUpdateLetter_Click(object sender, EventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();
            objProjectBL.Letter_ID = txtLetterID.Text.ToString();


            DataSet catds = new DataSet();
            if (txtLetterRecFrom.Text != "" && rbLetRecFrom_DeptUpdate.Text != "")
            {

            }
            else
            {
                if (objProjectBL.loadLetterID(con, ProjectBL.eLoadSp.GENERATE_LETTER_ID_letRecFrom_Dept, ref catds))
                {
                    txtLetterRecFrom.Text = catds.Tables[0].Rows[0]["LETTER_ID"].ToString();
                }
            }
            objProjectBL.Letter_ID_RecFrDept = txtLetterID.Text;
            objProjectBL.LetterRefNumber_RecFrDept = txtLetterRefNo.Text;
            objProjectBL.LetterRecdFrom_RecFrDept = txtLetterRecFrom.Text;
            if (txtLetterRecDate.Text != "")
            {
                objProjectBL.LetterReceivedDate_RecFrDept = Convert.ToDateTime(txtLetterRecDate.Text);
            }
            if (txtDateofRecipt.Text != "")
            {
                objProjectBL.DateofReceipt_RecFrDept = Convert.ToDateTime(txtDateofRecipt.Text);
            }

            if (txtReplyBydateModal.Text != "")
            {
                objProjectBL.ReplyByDate_RecFrDept = Convert.ToDateTime(txtReplyBydateModal.Text);
            }
            objProjectBL.ModeofRecepit_RecFrDept = txtModeOfRecipt.Text;
            //objProjectBL.Copyof_Fresh_Letter_Path = txtProjectTypeName.Text;

            var ProjectCode = Request.QueryString["ID"].ToString();

            objProjectBL.FreshLetterSubject_RecFrDept = txtSubject.Text;

            if (FupCopyOfletter.HasFile)
            {

                objProjectBL.Copyof_Fresh_Letter_Path_RecFrDept = txtLetterRefNo.Text + "_" + DatetimenowEX + "_Fr_Letter_RecFrDept" + System.IO.Path.GetExtension(FupCopyOfletter.FileName);
            }
            if (FupCopyOfletter.HasFile)
            {
                FupCopyOfletter.SaveAs(Server.MapPath("~\\UploadedFiles\\" + txtLetterRefNo.Text + "_" + DatetimenowEX + "_Fr_Letter_RecFrDept" + System.IO.Path.GetExtension(FupCopyOfletter.FileName)));
            }
            if (rbLetRecFrom_DeptUpdate.SelectedValue == "FreshLetter")
            {
                objProjectBL.Task = "UpdateFLT";
                if (objProjectBL.FreshLetter_update_letRecFrom_Dept(con, ProjectBL.eLoadSp.UPDATE_FreshLetter_letRecFrom_Dept))
                {
                    //page_Correspondence.Attributes.Remove("panel-collapse panel-body collapse out");
                    //page_Correspondence.Attributes.Add("class", "panel-collapse panel-body collapse in");
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Fresh Letter Details has been Updated Successfully');", true);
                    BindFreshLetterGrid_letRecFrom_Dept();
                    ClearFreshLetter();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Project Type or Code is already exists, Pls try another');", true);
                }
            }
            else
            {
                objProjectBL.Letter_ID = Convert.ToString(ViewState["Letter_ID"]);
                if (objProjectBL.FreshLetterupdate_letRecFrom_Dept(con, ProjectBL.eLoadSp.UPDATE_FreshLetter_BY_ID_letRecFrom_Dept))
                {
                    //ClearFreshLetterFilds();
                    //btnSaveProjectType.Text = "Save";
                    //ModelProjectTypePopup.Show();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Project Type Details has been updated successfully.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    //Written by prashanth G: End
    protected void btnSave__letRecFrom_Dept_RepLt_Click(object sender, EventArgs e)
    {

        if (rbLetRecFrom_Dept.SelectedValue == "FreshLetter")
        {
            try
            {
                objProjectBL = new ProjectBL();
                GetProjectDetails(Request.QueryString["ID"].ToString());
                if (ViewState["Project_Code"].ToString() != "")
                {
                    objProjectBL.ProjectCode = Convert.ToString(ViewState["Project_Code"]);

                }

                DataSet catds = new DataSet();
                if (txtLetterID_letRecFrom_Dept.Text != "" && rbLetRecFrom_Dept.Text != "")
                {

                }
                else
                {
                    if (objProjectBL.loadLetterID(con, ProjectBL.eLoadSp.GENERATE_LETTER_ID_letRecFrom_Dept, ref catds))
                    {
                        txtLetterID_letRecFrom_Dept.Text = catds.Tables[0].Rows[0]["LETTER_ID"].ToString();
                    }
                }
                objProjectBL.Letter_ID_RecFrDept = txtLetterID_letRecFrom_Dept.Text;
                objProjectBL.LetterRefNumber_RecFrDept = txtLetterRefNo_letRecFrom_Dept.Text;
                objProjectBL.LetterRecdFrom_RecFrDept = txtLetterRecFrom_letRecFrom_Dept.Text;
                if (txtLetterRecDate_letRecFrom_Dept.Text != "")
                {
                    objProjectBL.LetterReceivedDate_RecFrDept = Convert.ToDateTime(txtLetterRecDate_letRecFrom_Dept.Text);
                }
                if (Date_of_Receipt_letRecFrom_Dept.Text != "")
                {
                    objProjectBL.DateofReceipt_RecFrDept = Convert.ToDateTime(Date_of_Receipt_letRecFrom_Dept.Text);
                }

                if (txtReplyByDate_letRecFrom_Dept.Text != "")
                {
                    objProjectBL.ReplyByDate_RecFrDept = Convert.ToDateTime(txtReplyByDate_letRecFrom_Dept.Text);
                }
                objProjectBL.ModeofRecepit_RecFrDept = txtModeofRecepit_ModeofRecepit.Text;
                //objProjectBL.Copyof_Fresh_Letter_Path = txtProjectTypeName.Text;

                var ProjectCode = Request.QueryString["ID"].ToString();

                objProjectBL.FreshLetterSubject_RecFrDept = txtSubjext_letRecFrom_Dept.Text;

                if (fupCopyofLetter_letRecFrom_Dept.HasFile)
                {

                    objProjectBL.Copyof_Fresh_Letter_Path_RecFrDept = txtLetterRefNo_letRecFrom_Dept.Text + "_" + DatetimenowEX + "_Fr_Letter_RecFrDept" + System.IO.Path.GetExtension(fupCopyofLetter_letRecFrom_Dept.FileName);
                }
                if (fupCopyofLetter_letRecFrom_Dept.HasFile)
                {
                    fupCopyofLetter_letRecFrom_Dept.SaveAs(Server.MapPath("~\\UploadedFiles\\" + txtLetterRefNo_letRecFrom_Dept.Text + "_" + DatetimenowEX + "_Fr_Letter_RecFrDept" + System.IO.Path.GetExtension(fupCopyofLetter_letRecFrom_Dept.FileName)));
                }
                if (rbLetRecFrom_Dept.SelectedValue == "FreshLetter")
                {
                    objProjectBL.Task = "InsertFLT";
                    if (objProjectBL.FreshLetter_insert_letRecFrom_Dept(con, ProjectBL.eLoadSp.INSERT_FreshLetter_letRecFrom_Dept))
                    {
                        //page_Correspondence.Attributes.Remove("panel-collapse panel-body collapse out");
                        //page_Correspondence.Attributes.Add("class", "panel-collapse panel-body collapse in");
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Fresh Letter Details has been Saved Successfully');", true);
                        BindFreshLetterGrid_letRecFrom_Dept();
                        ClearFreshLetter();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Project Type or Code is already exists, Pls try another');", true);
                    }
                }
                else
                {
                    objProjectBL.Letter_ID = Convert.ToString(ViewState["Letter_ID"]);
                    if (objProjectBL.FreshLetterupdate_letRecFrom_Dept(con, ProjectBL.eLoadSp.UPDATE_FreshLetter_BY_ID_letRecFrom_Dept))
                    {
                        //ClearFreshLetterFilds();
                        //btnSaveProjectType.Text = "Save";
                        //ModelProjectTypePopup.Show();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Project Type Details has been updated successfully.');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
    }


    protected void lnkLetterIdFreshLetter_Click(object sender, EventArgs e)
    {
        objProjectBL = new ProjectBL();
        txtFreshLetterID.Text = ((LinkButton)sender).CommandName.ToString();
        rbLetRecFrom_DeptUpdate.SelectedValue = "FreshLetter";
        objProjectBL.Letter_ID = txtFreshLetterID.Text;
        objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_FRESHLETTER_DETAILS_BY_ID, ref ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string kk = ds.Tables[0].Rows[0]["LetterRecdFrom"].ToString();

            txtFreshLetterRefNo.Text = ds.Tables[0].Rows[0]["LetterRefNumber"].ToString();
            if (ds.Tables[0].Rows[0]["LetterRecdFrom"].ToString() != "")
            {
                ddlfreshletterRecFrom.SelectedValue = ds.Tables[0].Rows[0]["LetterRecdFrom"].ToString();
            }

            if (ds.Tables[0].Rows[0]["LetterReceivedDate"].ToString() == string.Empty)
            {
                txtFreshletterRecDate.Text = string.Empty;
            }
            else
            {
                txtFreshletterRecDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["LetterReceivedDate"]).ToString("dd-MM-yyyy");
            }

            if (ds.Tables[0].Rows[0]["DateofReceipt"].ToString() == string.Empty)
            {
                txtFreashLetterdateofrecipt.Text = string.Empty;
            }
            else
            {
                txtFreashLetterdateofrecipt.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DateofReceipt"]).ToString("dd-MM-yyyy");
            }

            if (ds.Tables[0].Rows[0]["ReplyByDate"].ToString() == string.Empty)
            {
                TxtfreshLetterreplydate.Text = string.Empty;
            }
            else
            {
                TxtfreshLetterreplydate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ReplyByDate"]).ToString("dd-MM-yyyy");
            }


            //ddlState_SelectedIndexChanged(null, null);
            //ddlLocation.SelectedValue = ds.Tables[0].Rows[0]["Location"].ToString();
            txtfreshLetterModeOfRecipt.Text = ds.Tables[0].Rows[0]["ModeofRecepit"].ToString();
            txtGridFreshLetterSubject.Text = ds.Tables[0].Rows[0]["FreshLetterSubject"].ToString();
            BindLetterDepNameUp();
            ModalFreshLetter.Show();
        }
    }


    protected void btnFreshletterUpdate_Click(object sender, EventArgs e)
    {
        try
        {

            //GetProjectDetails(Request.QueryString["ID"].ToString());
            //if (ViewState["Project_Code"].ToString() != "")
            //{
            //    objProjectBL.Project_Code = Convert.ToString(ViewState["Project_Code"]);

            //}
            objProjectBL = new ProjectBL();
            DataSet catds = new DataSet();
            rblFreshReply.SelectedValue = "FreshLetter";
            if (txtFreshLetterID.Text != "" && rblFreshReply.Text != "")
            {
                //objProjectBL.Letter_ID = txtFreshLetterID.Text;
                //objProjectBL.LetterRefNumber = txtFreshLetterRefNo.Text;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Please Select Fresh Letter Button');", true);
                //if (objProjectBL.loadLetterID(con, ProjectBL.eLoadSp.GENERATE_LETTER_ID, ref catds))
                //{
                //    txtLetter_ID.Text = catds.Tables[0].Rows[0]["LETTER_ID"].ToString();
                //}
            }
            objProjectBL.Letter_ID = txtFreshLetterID.Text;
            objProjectBL.LetterRefNumber = txtFreshLetterRefNo.Text;
            if (ddlfreshletterRecFrom.SelectedValue != "-Select-")
            {
                objProjectBL.LetterRecdFrom = ddlfreshletterRecFrom.SelectedValue;
            }

            if (txtFreshletterRecDate.Text != "")
            {
                objProjectBL.LetterReceivedDate = Convert.ToDateTime(txtFreshletterRecDate.Text);
            }
            if (txtFreshletterRecDate.Text != "")
            {
                objProjectBL.DateofReceipt = Convert.ToDateTime(txtFreashLetterdateofrecipt.Text);
            }

            if (txtFreshletterRecDate.Text != "")
            {
                objProjectBL.ReplyByDate = Convert.ToDateTime(TxtfreshLetterreplydate.Text);
            }
            objProjectBL.ModeofRecepit = txtfreshLetterModeOfRecipt.Text;
            objProjectBL.Copyof_Fresh_Letter_Path = txtProjectTypeName.Text;

            var ProjectCode = Request.QueryString["ID"].ToString();

            objProjectBL.FreshLetterSubject = txtGridFreshLetterSubject.Text;

            if (FUPFreshLetter.HasFile)
            {

                objProjectBL.Copyof_Fresh_Letter_Path = txtFreshLetterID.Text + "_Fr_Letter" + System.IO.Path.GetExtension(FUPFreshLetter.FileName);
            }
            if (FUPFreshLetter.HasFile)
            {
                FUPFreshLetter.SaveAs(Server.MapPath("~\\UploadedFiles\\" + txtFreshLetterID.Text.Replace("/", "-") + "_Fr_Letter" + System.IO.Path.GetExtension(FUPFreshLetter.FileName)));
            }

            //if (rblLetterSelect.SelectedValue == "FreshLetter")
            //{
            //    if (objProjectBL.FreshLetter_insert(con, ProjectBL.eLoadSp.INSERT_FreshLetter))
            //    {
            //        //page_Received.Attributes.Remove("panel-collapse panel-body collapse out");
            //        ////page_Received.Attributes.Add("class", "panel-collapse panel-body collapse in");
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Fresh Letter Details has been Saved Successfully');", true);
            //        BindFreshLetterGrid();
            //        ClearFreshLetterFilds();
            //        BindLetterList();
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Project Type or Code is already exists, Pls try another');", true);
            //    }
            //}
            //else
            //{
            objProjectBL.Letter_ID = txtFreshLetterID.Text;
            if (objProjectBL.FreshLetterupdateModal(con, ProjectBL.eLoadSp.UPDATE_FreshLetter_BY_ID))
            {
                //page_Received.Attributes.Remove("panel-collapse panel-body collapse out");
                ////page_Received.Attributes.Add("class", "panel-collapse panel-body collapse in");
                //ClearFreshLetterFilds();
                //btnSaveProjectType.Text = "Save";
                //ModelProjectTypePopup.Show();
                BindFreshLetterGrid();
                BindLetterList();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Fresh Letter Details has been Updated Successfully');", true);
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnfreshlettercancel_Click(object sender, EventArgs e)
    {
        ddlfreshletterRecFrom.SelectedIndex = -1;

        txtGridFreshLetterSubject.Text = "";
        txtProjectTypeName.Text = "";
        txtfreshLetterModeOfRecipt.Text = "";
        TxtfreshLetterreplydate.Text = "";
        TxtfreshLetterreplydate.Text = "";
        txtFreashLetterdateofrecipt.Text = "";
        txtFreshletterRecDate.Text = "";
        txtFreshLetterRefNo.Text = "";
        txtFreshLetterID.Text = "";
    }

    protected void lnkreplyToLetter_Click(object sender, EventArgs e)
    {
        objProjectBL = new ProjectBL();



        objProjectBL.ReplayToLetter_ID_RTL = ((LinkButton)sender).CommandName.ToString();
        rblreplyToLetter.SelectedValue = "ReplayToletter";


        objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_REPLYTOLETTER_DETAILS_BY_ID, ref ds);
        if (ds.Tables[0].Rows.Count > 0)
        {

            //ddlReplyToletterId.SelectedValue = ds.Tables[0].Rows[0]["ReplayToLetter_ID_RTL"].ToString();


            txtReplyToletterLettersentto.Text = ds.Tables[0].Rows[0]["LetterSentTo_RTL"].ToString();
            txtReplyToletterReferenceNo.Text = ds.Tables[0].Rows[0]["LetterRefNumber_RTL"].ToString();
            if (ds.Tables[0].Rows[0]["ReplyToLetterDate"].ToString() == string.Empty)
            {
                txtReplyToletterDate.Text = string.Empty;
            }
            else
            {
                txtReplyToletterDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ReplyToLetterDate"]).ToString("dd-MM-yyyy");
            }

            ddlReplyToletterId.SelectedValue = ds.Tables[0].Rows[0]["ReplayToLetter_ID_RTL"].ToString();
            txtReplyToletterMOD.Text = ds.Tables[0].Rows[0]["ModeofDispatch_RTL"].ToString();
            txtReplyToletterSubject.Text = ds.Tables[0].Rows[0]["Subject"].ToString();
            txtReplyToletterCC1.Text = ds.Tables[0].Rows[0]["CC1"].ToString();
            txtReplyToletterCC2.Text = ds.Tables[0].Rows[0]["CC2"].ToString();
            txtReplyToletterCC3.Text = ds.Tables[0].Rows[0]["CC3"].ToString();
            txtReplyToletterCC4.Text = ds.Tables[0].Rows[0]["CC4"].ToString();
            txtReplyToletterCC5.Text = ds.Tables[0].Rows[0]["CC5"].ToString();
            ModalReplyToLetter.Show();
        }

    }

    protected void btnReplyToletterUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objProjectBL = new ProjectBL();

            if (ddlReplyToletterId.SelectedValue != "-Select-")
            {
                objProjectBL.ReplayToLetter_ID_RTL = ddlReplyToletterId.SelectedValue;
            }
            //if (ddlReplyToletterCorToDept.SelectedValue != "-Select-")
            //{
            //    objProjectBL.ReplayToLetter_ID_RTL = ddlReplyToletterCorToDept.SelectedItem.Text;
            //}
            objProjectBL.Date_RTL = Convert.ToDateTime(txtReplyToletterDate.Text);
            objProjectBL.LetterRefNumber_RTL = txtReplyToletterReferenceNo.Text;
            objProjectBL.ModeofDispatch_RTL = txtReplyToletterMOD.Text;
            objProjectBL.LetterSentTo_RTL = txtReplyToletterLettersentto.Text;
            objProjectBL.Subject = txtReplyToletterSubject.Text;
            //Start:Written by Prashanth
            objProjectBL.CC1 = txtReplyToletterCC1.Text;
            objProjectBL.CC2 = txtReplyToletterCC2.Text;
            objProjectBL.CC3 = txtReplyToletterCC3.Text;
            objProjectBL.CC4 = txtReplyToletterCC4.Text;
            objProjectBL.CC5 = txtReplyToletterCC5.Text;
            //End: Written By Prashanth
            if (FUPReplyToletterCopy.HasFile)
            {

                objProjectBL.LetterCopy_RTL = FUPReplyToletterCopy.FileName + DatetimenowEX + "_" + System.IO.Path.GetExtension(FUPReplyToletterCopy.FileName);
            }
            if (FUPReplyToletterCopy.HasFile)
            {
                FUPReplyToletterCopy.SaveAs(Server.MapPath("~\\UploadedFiles\\" + FUPReplyToletterCopy.FileName + DatetimenowEX + "_" + System.IO.Path.GetExtension(FUPReplyToletterCopy.FileName)));
            }

            if (FUPReplyToletterAcknowledgementCopy.HasFile)
            {

                objProjectBL.AcknowledgementCopy_RTL = "Acknowledgement_" + FUPReplyToletterAcknowledgementCopy.FileName + DatetimenowEX + "_" + System.IO.Path.GetExtension(FUPReplyToletterAcknowledgementCopy.FileName);
            }
            if (FUPReplyToletterAcknowledgementCopy.HasFile)
            {
                FUPReplyToletterAcknowledgementCopy.SaveAs(Server.MapPath("~\\UploadedFiles\\" + "Acknowledgement_" + FUPReplyToletterAcknowledgementCopy.FileName + DatetimenowEX + "_" + System.IO.Path.GetExtension(FUPReplyToletterAcknowledgementCopy.FileName)));
            }
            //if (Request.QueryString["ID"] != "")
            //{
            //    objProjectBL.ProjectCode = Request.QueryString["ID"];
            //}



            //objProjectBL.ReplayToLetter_ID_RTL = Convert.ToString(ViewState["ReplayToLetter_ID_RTL"]);
            if (objProjectBL.ReplayToLetterupdateModal(con, ProjectBL.eLoadSp.UPDATE_ReplayToLetter_BY_Letter_ID_RTL))
            {
                BindReplayToLetterGrid();
                //ClearReplayToLetterFilds();
                //btnSaveProjectType.Text = "Save";
                //ModelProjectTypePopup.Show();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Replay To Letter Details has been updated successfully.');", true);
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnReplyToletterCancel_Click(object sender, EventArgs e)
    {
        txtReplyToletterDate.Text = "";
        ddlReplyToletterId.SelectedIndex = -1;
        txtReplyToletterReferenceNo.Text = "";
        txtReplyToletterMOD.Text = "";
        txtReplyToletterLettersentto.Text = "";
        txtReplyToletterSubject.Text = "";
        txtReplyToletterCC1.Text = "";
        txtReplyToletterCC2.Text = "";
        txtReplyToletterCC3.Text = "";
        txtReplyToletterCC4.Text = "";
        txtReplyToletterCC5.Text = "";
        txtReplyToletterCC1.Text = "";

    }
    protected void BindLetterListModal()
    {
        try
        {
            objProjectBL = new ProjectBL();
            DataSet data = new DataSet();
            if (Session["Project_Code"].ToString() != "")
            {
                objProjectBL.Project_Code = Convert.ToString(Session["Project_Code"]);
            }
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_LetterID_ALL, ref data);
            Grid_ProjectType.DataSource = data;
            ddlReplyToletterId.Items.Clear();
            ddlReplyToletterId.DataSource = data;
            ddlReplyToletterId.DataValueField = "Letter_ID";
            ddlReplyToletterId.DataTextField = "Letter_ID";
            ddlReplyToletterId.DataBind();
            ddlReplyToletterId.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    public static string NumberToWords(Int64 number)
    {
        if (number == 0)
            return "zero";

        if (number < 0)
            return "minus " + NumberToWords(Math.Abs(number));

        string words = "";
        //if ((number / 100000000) > 0)
        //{
        //    words += NumberToWords(number / 100000000) + " ten Crore ";
        //    number %= 100000000;
        //}

        if ((number / 10000000) > 0)
        {
            words += NumberToWords(number / 10000000) + "  Crore ";
            number %= 10000000;
        }

        //if ((number / 1000000) > 0)
        //{
        //    words += NumberToWords(number / 1000000) + " Ten Lakh ";
        //    number %= 1000000;
        //}
        if ((number / 100000) > 0)
        {
            words += NumberToWords(number / 100000) + " Lakh ";
            number %= 100000;
        }
        if ((number / 1000) > 0)
        {
            words += NumberToWords(number / 1000) + " thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords(number / 100) + " hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "")
                words += "and ";

            var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            var tensMap = new[] { "zero", "Ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
        }

        return words;
    }

    protected void txtProjectCost_TextChanged(object sender, EventArgs e)
    {

        txtamtinwords.Text = NumberToWords(Convert.ToInt32(Convert.ToDecimal(txtProjectCost.Text)));
    }


    //Written by prashanth G: end
}






