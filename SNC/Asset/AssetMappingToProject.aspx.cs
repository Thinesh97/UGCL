using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using BusinessLayer;
using SNC.ErrorLogger;
using Bussinesslogic;
using System.Collections;



public partial class AssetMappingToProject : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    ProjectBL objprojectBL = new ProjectBL();
    AssetRegistrationBL objAssetRegistraton = new AssetRegistrationBL();
    DataSet ds = null;
    IndentBL objIndent = null;
    ProjectBL objprojectbl = null;
    AssetTransferBL objassetTransferBL = new AssetTransferBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    BindProjectFromAndTO();
                    BindDepartmentTO();
                    BindProjectFILTER();
                }
                else
                {
                    Response.Redirect("../CommonPages/Login.aspx", false);
                }
            }
        }
        catch(Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void BindDepartmentTO()
    {

        try
        {
            ds = new DataSet();
            objprojectbl = new ProjectBL();
            objprojectbl.load(con, ProjectBL.eLoadSp.SELECT_DEPARTMENT_ALL, ref ds);
            ddlDepartment.DataSource = ds;
            ddlDepartment.DataTextField = "Department";
            ddlDepartment.DataValueField = "Department";
            ddlDepartment.DataBind();

            ddlDepartment.Items.Insert(0, "-Select-");

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }


    protected void BindProjectFromAndTO()
    {

        try
        {
            ds = new DataSet();
            objprojectbl = new ProjectBL();
            if (Session["Project_Code"] != null)
            {
                objprojectbl.Project_Code = Session["Project_Code"].ToString();

                objprojectbl.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_BY_Project_Code, ref ds);
                ddlFromProject.DataSource = ds;
                ddlFromProject.DataTextField = "Project_Name";
                ddlFromProject.DataValueField = "Project_Code";
                ddlFromProject.DataBind();
                ddlFromProject.Items.Insert(0, "-Select-");
                ddlFromProject.SelectedValue = Session["Project_Code"].ToString();
                ddlFromProject.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }


    protected void BindProjectFILTER()
    {
        try
        {
            DataSet dsProject = new DataSet();
                objassetTransferBL = new AssetTransferBL();
                if (Session["Project_Code"] != null)
                {
                    objassetTransferBL.Assign_To_project = Session["Project_Code"].ToString();
                    objassetTransferBL.load(con, AssetTransferBL.eLoadSp.SELECT_ASSIGN_TO_PROJECT_NAME_FOR_ASSET, ref dsProject);
                   ddlToProject.DataSource = dsProject;
                   ddlToProject.DataTextField = "Project_Name";
                   ddlToProject.DataValueField = "Project_Code";
                   ddlToProject.DataBind();
                   ddlToProject.Items.Insert(0, "-Select-");
                }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    


    protected void GVSearchGrid_UpdateCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {

        try
        {
            objassetTransferBL = new AssetTransferBL();
            DataTable dt = new DataTable();
         


            DataTable table;
            DataTable dtStock = new DataTable();

            table = (DataTable)ViewState["ads"];
         

            DateTime tdate,sdate;


           
            if ((DateTime.TryParse(e.Record["TransferDate"].ToString(),out tdate)) && ( DateTime.TryParse(e.Record["ScheduleStartDate"].ToString(),out sdate)))
            {
                string code = e.Record["Asset_Code"].ToString();

                foreach (DataRow dr in table.Rows) 
                {
                    if (dr["Asset_Code"].ToString() == code)
                    {
                        dr["TransferDate"] = e.Record["TransferDate"].ToString();
                        dr["ScheduleStartDate"] = e.Record["ScheduleStartDate"].ToString();
                        dr["Remarks"] = e.Record["Remarks"].ToString();
                        dr["DL"] =Convert.ToBoolean(e.Record["DL"]);
                        dr["RC"] = Convert.ToBoolean(e.Record["RC"]);
                        dr["Road_Tax_Reciept"] = Convert.ToBoolean(e.Record["Road_Tax_Reciept"]);
                        dr["INSURANCE"] = Convert.ToBoolean(e.Record["INSURANCE"]);
                        dr["PERMIT"] = Convert.ToBoolean(e.Record["PERMIT"]);
                        dr["NOC"] = Convert.ToBoolean(e.Record["NOC"]);
                        dr["FC"] = Convert.ToBoolean(e.Record["FC"]);
                        dr["Way_BILL"] = Convert.ToBoolean(e.Record["Way_BILL"]);
   

                        break;
                    }
                }
               
                 GVSearchGrid.DataSource = table;
                GVSearchGrid.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Asset updated successfully!');", true);
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please enter TransferDate either ScheduleStartDate in Correct Format!');", true);
                return;
                
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }     

    }
    

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindUsers();
    }
    protected void BindUsers()
    {
        try
        {
            if (ddlDepartment.SelectedIndex != 0)
            {
                string department = ddlDepartment.SelectedValue;
                objIndent = new IndentBL();
                DataSet dsUsers = new DataSet();
                bool exists;
                DataTable DatafilterDt = new DataTable();
                objIndent.load(con, IndentBL.eLoadSp.SELECT_USERNAMES_ALL, ref dsUsers);
                ddlReceiver.DataSource = dsUsers;

                if (dsUsers.Tables[0].Rows.Count > 0)
                {
                    DatafilterDt = dsUsers.Tables[0];


                    exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Department").Equals(department)).Count() > 0;
                    if (exists)
                    {
                        DataTable Usersdt = DatafilterDt.AsEnumerable()
                                     .Where(r => r.Field<string>("Department") == department)
                                     .CopyToDataTable();

                        ddlReceiver.DataSource = Usersdt;
                        ddlReceiver.DataTextField = "Name";
                        ddlReceiver.DataValueField = "UID";
                        ddlReceiver.DataBind();
                        ddlReceiver.Items.Insert(0, "-Select-");
                    }
                    else
                    {
                        ddlReceiver.Items.Clear();
                        ddlReceiver.Items.Insert(0, "-Select-");
                    }
                    exists = false;
                }

            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        try
        {


            ds = new DataSet();
            DataTable dt = new DataTable();
            objassetTransferBL = new AssetTransferBL();
            objassetTransferBL.Project_Code = ddlFromProject.SelectedValue;
            objassetTransferBL.load(con, AssetTransferBL.eLoadSp.ASSET_TRANSFER_SEARCH_CLICK, ref ds);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["ads"] = dt = ds.Tables[0];

                GVSearchGrid.DataSource = ds;
                GVSearchGrid.DataBind();
            }
        }
        catch(Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btntransfer_Click(object sender, EventArgs e)
    {
        try
        {
            ds = new DataSet();
            objassetTransferBL = new AssetTransferBL();
            if (ddlFromProject.SelectedIndex != 0 && ddlToProject.SelectedIndex != 0)
            {
                if (GVSearchGrid.SelectedRecords != null)
                {
                    foreach (Hashtable ht in GVSearchGrid.SelectedRecords)
                    {

                        objassetTransferBL.Asset_Code = ht["Asset_Code"].ToString();
                        objassetTransferBL.Condition = ht["Condition"].ToString();
                        objassetTransferBL.DL = Convert.ToBoolean(ht["DL"]);
                        objassetTransferBL.RC = Convert.ToBoolean(ht["RC"]);
                        objassetTransferBL.Road_Tax_Reciept = Convert.ToBoolean(ht["Road_Tax_Reciept"]);
                        objassetTransferBL.INSURANCE = Convert.ToBoolean(ht["INSURANCE"]);
                        objassetTransferBL.PERMIT = Convert.ToBoolean(ht["PERMIT"]);
                        objassetTransferBL.NOC = Convert.ToBoolean(ht["NOC"]);
                        objassetTransferBL.FC = Convert.ToBoolean(ht["FC"]);
                        objassetTransferBL.Way_BILL = Convert.ToBoolean(ht["Way_BILL"]);
                        objassetTransferBL.Transfer_Date = Convert.ToDateTime(ht["TransferDate"].ToString());
                        objassetTransferBL.Scheduled_Date = Convert.ToDateTime(ht["ScheduleStartDate"].ToString());
                        objassetTransferBL.Remarks = ht["Remarks"].ToString();
                        objassetTransferBL.Assign_To_project = ddlToProject.SelectedValue;
                        objassetTransferBL.FromProject = ddlFromProject.SelectedValue;
                        objassetTransferBL.Asset_Reciever = Convert.ToInt32(ddlReceiver.SelectedValue);
                        if (objassetTransferBL.insert(con, AssetTransferBL.eLoadSp.INSERT_ASSET_TRANSFER))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Asset transferred sucessfully!!!');", true);
                            Btn_Search_Click(sender, e);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to transfer the Asset!!!');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please Select Any Asset to Transfer!!!');", true);

                }

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
        finally
        {
            GVSearchGrid.SelectedRecords = null;
        }
    }

    protected void Btn_Cancel_Click(object sender, EventArgs e)
    {
        ddlToProject.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        ddlReceiver.Items.Clear();
       
        ddlReceiver.Items.Insert(0, "-Select-");
        GVSearchGrid.DataSource = null;
        GVSearchGrid.DataBind();
    }
}