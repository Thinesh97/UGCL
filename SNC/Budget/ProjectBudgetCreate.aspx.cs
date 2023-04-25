using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SNC.ErrorLogger;
using BusinessLayer;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public partial class ProjectBudget : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    ProjectBL objProjectBL = null;
    MaterialBL objMaterial = null;
    IndentBL objIndent = null;
    Category objCategory = null;
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UID"] != null)
            {
                BindProjectNames();
                BindUsersNames();
                BindBudgetSectors();

                if (Request.QueryString["ID"] != null)
                {
                    GetProjectBudgetDetails();
                    
                }
                else
                {
                    txtCreatedDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    if (Session["Project_Code"] != null)
                    {
                        ddlProjectName.SelectedValue = Session["Project_Code"].ToString();
                    }
                    ddlCreatedBy.SelectedValue = Session["UID"].ToString();
                }
            }
            else
            {
                Response.Redirect("../CommonPages/Login.aspx", false);
            }
        }
    }


    protected void BindProjectNames()
    {
        try
        {
            objProjectBL = new ProjectBL();
            ds = new DataSet();
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_ALL, ref ds);
            ddlProjectName.DataSource = ds;
            ddlProjectName.DataTextField = "Project_Name";
            ddlProjectName.DataValueField = "Project_Code";
            ddlProjectName.DataBind();
            ddlProjectName.Items.Insert(0, "-Select-");

            
            ddlProjectName.Enabled = false;

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

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCreatedBy.DataSource = ds;
                ddlCreatedBy.DataTextField = "Name";
                ddlCreatedBy.DataValueField = "UID";
                ddlCreatedBy.DataBind();
                ddlCreatedBy.Items.Insert(0, "-Select-");
               
                ddlCreatedBy.Enabled = false;

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    private void BindBudgetSectors()
    {
        try
        {
            objMaterial = new MaterialBL();
            ds = new DataSet(); 
            objMaterial.load(con, MaterialBL.eLoadSp.SELECT_BUDGET_SECTOR_ALL, ref ds);
            if (ds.Tables.Count > 0)
            {

                ddlBudgetSector.DataSource = ds.Tables[0];
                ddlBudgetSector.DataTextField = "Sector_Name";
                ddlBudgetSector.DataValueField = "Budget_Sector_ID";
                ddlBudgetSector.DataBind();
                ddlBudgetSector.Items.Insert(0, "-Select-");
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }


    protected void GetProjectBudgetDetails()
    {
        try
        {
            ProjectBudgetBL objGetProjBudBL = new ProjectBudgetBL();
            ds = new DataSet();
            objGetProjBudBL.Proj_Bud_ID = Convert.ToInt32(Request.QueryString["ID"].ToString());

            objGetProjBudBL.load(con, ProjectBudgetBL.eLoadSp.GET_PROJECT_BUDGET_DETAILS_BY_ID, ref ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlProjectName.SelectedValue = ds.Tables[0].Rows[0]["Project_ID"].ToString();
                ddlCreatedBy.SelectedValue = ds.Tables[0].Rows[0]["Created_By"].ToString();
                rd_Status.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                txtCreatedDate.Text = ds.Tables[0].Rows[0]["Date"].ToString();

                btnSubmit.Text = "Update";
                btn_addSector.Visible = true;
                ViewState["ProjectBudgetID"] = ds.Tables[0].Rows[0]["Proj_Bud_ID"].ToString();

                BindBudgetSectorsGrid();

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }


    protected void BindBudgetSectorsGrid()
    {
        try
        {
            ProjectBudgetBL objGetProjBudBL = new ProjectBudgetBL();

            DataSet dsNEW = new DataSet();
            dsNEW.Clear();
            if (!string.IsNullOrEmpty(ViewState["ProjectBudgetID"].ToString()))
            {
                objGetProjBudBL.Proj_Bud_ID = Convert.ToInt32(ViewState["ProjectBudgetID"].ToString());
            }
           
            objGetProjBudBL.load(con, ProjectBudgetBL.eLoadSp.SELECT_PROJECT_BUDGET_SECTORS_BY_ID, ref dsNEW);

            if (dsNEW.Tables[0].Rows.Count > 0)
            {
                Gv_ProjBudgetSector.DataSource = dsNEW;
                Gv_ProjBudgetSector.DataBind();
            }
            else
            {
              
                Gv_ProjBudgetSector.ClearPreviousDataSource();
                Gv_ProjBudgetSector.DataSource = dsNEW;
                Gv_ProjBudgetSector.DataBind();
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

            ProjectBudgetBL objProjBudBL = new ProjectBudgetBL();
            if (ddlProjectName.SelectedIndex != 0)
            {
                objProjBudBL.Project_ID = ddlProjectName.SelectedValue;
            }
           
            if (ddlCreatedBy.SelectedIndex != 0)
            {
                objProjBudBL.Created_By = Convert.ToInt32(ddlCreatedBy.SelectedValue);
            }
            objProjBudBL.Date = Convert.ToDateTime(txtCreatedDate.Text.Trim());

            objProjBudBL.Status = Convert.ToInt32(rd_Status.SelectedValue);

           
            if (btnSubmit.Text == "Submit")
            {
                if (objProjBudBL.insert(con, ProjectBudgetBL.eLoadSp.INSERT_PROJECT_BUDGET))
                {
                    ViewState["ProjectBudgetID"] = objProjBudBL.Proj_Bud_ID.ToString();
                    btnSubmit.Text = "Update";
                    btn_addSector.Visible = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Project Budget has been created Successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Project Budget already exists for this Project..!!!');", true);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(ViewState["ProjectBudgetID"].ToString()))
                {
                    objProjBudBL.Proj_Bud_ID = int.Parse(ViewState["ProjectBudgetID"].ToString());
                }

                if (objProjBudBL.update(con, ProjectBudgetBL.eLoadSp.UPDATE_PROJECT_BUDGET))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Project Budget has been Updated successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to Update Project Budget Details..!!!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ddlBudgetSector_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBudgetSector.SelectedValue == "16" && ddlBudgetSector.SelectedItem.Text == "BOQ Items")
            {
                BindCategoryDetails();
                trCategory.Visible = true;
            }
            else
            {
                trCategory.Visible = false;
            }
            
            mpeSector.Show();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void BindCategoryDetails()
    {
        try
        {
            objCategory = new Category();
            DataSet dsCat = new DataSet();
            DataTable DatafilterDt;
            DataTable dt;
            bool exists;
            int BudgetSectorID;
            if (objCategory.load(con, Category.eLoadSp.SELECT_ALL, ref dsCat))
            {
                dt = dsCat.Tables[0];
                if (ddlBudgetSector.SelectedIndex != 0)
                {
                    BudgetSectorID = Convert.ToInt32(ddlBudgetSector.SelectedValue.ToString());
                    if (dsCat.Tables[0].Rows.Count > 0)
                    {
                        DatafilterDt = dsCat.Tables[0];


                        exists = DatafilterDt.AsEnumerable().Where(c => c.Field<int>("Budget_Sector_ID").Equals(BudgetSectorID)).Count() > 0;
                        if (exists)
                        {
                            DataTable SectorIDdt = DatafilterDt.AsEnumerable()
                                         .Where(r => r.Field<int>("Budget_Sector_ID") == BudgetSectorID)
                                         .CopyToDataTable();

                            ddlCategoryName.DataSource = SectorIDdt;
                            ddlCategoryName.DataValueField = "Mat_cat_ID";
                            ddlCategoryName.DataTextField = "Category_Name";
                            ddlCategoryName.DataBind();

                        }
                        else
                        {
                            ddlCategoryName.Items.Clear();
                            ddlCategoryName.DataSource = null;
                            ddlCategoryName.DataBind();
                        }
                        exists = false;
                    }
                    ddlCategoryName.Items.Insert(0, "-Select-");
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSaveSector_Click(object sender, EventArgs e)
    {
        try
        {
            ProjectBudgetBL objProjBudSecBL = new ProjectBudgetBL();
            if (ddlBudgetSector.SelectedIndex != 0)
            {
                objProjBudSecBL.Budget_Sector_ID = Convert.ToInt32(ddlBudgetSector.SelectedValue);
            }

            if (ddlCategoryName.SelectedIndex != 0)
            {
                objProjBudSecBL.Category = Convert.ToInt32(ddlCategoryName.SelectedValue);
            }
            else
            {
                objProjBudSecBL.Category = null;
            }

            objProjBudSecBL.Quantity = txtQty.Text.Trim() != string.Empty ? Convert.ToDecimal(txtQty.Text.Trim()) : 0;

            if (!string.IsNullOrEmpty(ViewState["ProjectBudgetID"].ToString()))
            {
                objProjBudSecBL.Proj_Bud_ID = Convert.ToInt32(ViewState["ProjectBudgetID"].ToString());
            }

            if (btnSaveSector.Text == "Save")
            {
                if (objProjBudSecBL.insertBudgetSector(con, ProjectBudgetBL.eLoadSp.INSERT_PROJECT_BUDGET_SECTOR))
                {
                   
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Budget sector has been added Successfully');", true);
                    BindBudgetSectorsGrid();
                    ResetSectorDeatils();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Budget Sector already exists for this Project..!!!');", true);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(ViewState["ProjBudSectorID"].ToString()))
                {
                    objProjBudSecBL.Proj_Budget_Sec_ID = int.Parse(ViewState["ProjBudSectorID"].ToString());
                }

                if (objProjBudSecBL.updateProjectBudgetSector(con, ProjectBudgetBL.eLoadSp.UPDATE_PROJECT_BUDGET_SECTOR))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Sector has been Updated successfully');", true);
                    BindBudgetSectorsGrid();
                    ResetSectorDeatils();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to Update Sector Details..!!!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
      
    }

    protected void Gv_ProjBudgetSector_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ProjectBudgetBL objProjBudBL = new ProjectBudgetBL();
            objProjBudBL.Proj_Budget_Sec_ID = Convert.ToInt32(e.Record["Proj_Budget_Sec_ID"].ToString());
            if (objProjBudBL.delete(con, ProjectBudgetBL.eLoadSp.DELETE_BUDGET_SECTORS_BY_ID))
            {
                BindBudgetSectorsGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Sector has been deleted successfully.');", true);

                ResetSectorDeatils();
               
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete..!!!');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    private void ResetSectorDeatils()
    {
        ddlBudgetSector.SelectedValue = ddlBudgetSector.Items.FindByText("-Select-").Value;
        ddlCategoryName.Items.Clear();
        ddlCategoryName.Items.Insert(0, "-Select-");
        txtQty.Text = "0";
        btnSaveSector.Text = "Save";
        lblHeading.Text = "Add Sector";
        trCategory.Visible = false;
    }

    protected void lnk_ProjBudSectorID_Click(object sender, EventArgs e)
    {
        try
        {
            ProjectBudgetBL objProjSecBudBL = new ProjectBudgetBL();
            ViewState["ProjBudSectorID"] = Convert.ToInt32(((LinkButton)sender).CommandName.ToString());
            objProjSecBudBL.Proj_Budget_Sec_ID = Convert.ToInt32(ViewState["ProjBudSectorID"].ToString());
            DataSet dsEdit = new DataSet();
            if (objProjSecBudBL.load(con, ProjectBudgetBL.eLoadSp.SELECT_BUDGET_SECTOR_DETAILS_BY_ID,ref dsEdit))
            {
                ddlBudgetSector.SelectedValue = dsEdit.Tables[0].Rows[0]["Budget_Sector_ID"].ToString();

                ddlBudgetSector_SelectedIndexChanged(null, null);

                if (!string.IsNullOrEmpty(dsEdit.Tables[0].Rows[0]["Category"].ToString()))
                {
                    ddlCategoryName.SelectedValue = dsEdit.Tables[0].Rows[0]["Category"].ToString();
                }

                txtQty.Text = dsEdit.Tables[0].Rows[0]["Quantity"].ToString();

                btnSaveSector.Text = "Update";
                lblHeading.Text = "Update Sector";
                mpeSector.Show();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancelSector_Click(object sender, EventArgs e)
    {
        mpeSector.Hide();
        ResetSectorDeatils();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Budget/ProjectBudgetList.aspx", false);
    }
}
