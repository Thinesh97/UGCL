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


    public partial class SalarySlip : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        DataSet ds = null;
       
        IndentBL objIndent = null;
        ProfessionalServiceOrderBL objSAL = null;
        DailyRunningHourBL objDailyRH = null;
        ProjectBL objProjectBL = null;
        protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                BindStaffNameList();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
        
        }

        protected void BindStaffNameList()
        {
            try
            {
                ds = new DataSet();
            objSAL = new ProfessionalServiceOrderBL();
            //objSAL.load(con, ProfessionalServiceOrderBL.eLoadSp.SELECT_Staff_NAme, ref ds);
            ddlname.DataSource = ds;
            ddlname.DataTextField = "Full_Name";
            ddlname.DataValueField = "Staff_SalaryID";
            ddlname.DataBind();
                ddlname.Items.Insert(0, "-Select-");
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

    protected void BtnGenerateSalarySlip_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admin/GenerateSalarySlip.aspx?StaffID=" + ddlname.SelectedValue + "&SalaryMonth="+ ddlmonth.SelectedItem, false);
    }
}


