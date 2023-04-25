using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SNC.ErrorLogger;
using System.Net;
using System.IO;

namespace SNC.Admin
{
    public partial class GenerateSalarySlip : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        DataSet ds = null;

        StaffSalariesBL objSS = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    if (Request.QueryString["StaffID"] != null)
                    {
                        GetEmployeeContractDetails(Convert.ToInt32(Request.QueryString["StaffID"].ToString()));
                    }
                        
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }

        }

        private void GetEmployeeContractDetails(int StaffId)
        {
            try
            {
                objSS = new StaffSalariesBL();
                ds = new DataSet();
                objSS.Staff_SalaryID = StaffId;
                objSS.Task = "GetStaffSalaryDetailsById";
                objSS.load(con, StaffSalariesBL.eLoadSp.SELECT_SSDETAILS_BY_Staff_SalaryID_Print, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    LblName.Text = ds.Tables[0].Rows[0]["Full_Name"].ToString();
                    LblEmployeeId.Text = ds.Tables[0].Rows[0]["EmployeeID"].ToString();
                    LblPayslip.Text = Request.QueryString["SalaryMonth"].ToString();
                    LblDestination.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
                    LblDateOfJoining.Text = ds.Tables[0].Rows[0]["Actual_Date_of_Joining"].ToString();
                 
                    LblBasicActual.Text = ds.Tables[0].Rows[0]["Basic_Pay"].ToString();
                    LblActualHRA.Text = ds.Tables[0].Rows[0]["HRA"].ToString();
                    LblCONVEYANCEActual.Text = ds.Tables[0].Rows[0]["Conveyance_Allowance"].ToString();
                    LblSPECIALActual.Text = ds.Tables[0].Rows[0]["Special_Allowance"].ToString();
                    LblGROSSActual.Text = ds.Tables[0].Rows[0]["Sub_Total_A"].ToString();
                    // LblPFAmount.Text = ds.Tables[0].Rows[0]["PF_ER"].ToString();
                    //txtTotal_Cost_to_Company.Text = ds.Tables[0].Rows[0]["Total_Cost_to_Company"].ToString();
                    //txtDeductions.Text = ds.Tables[0].Rows[0]["Deductions"].ToString();
                    //txtLocation.Text = ds.Tables[0].Rows[0]["Location"].ToString();

                    //rblStatus.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                    LblPFAmount.Text = ds.Tables[0].Rows[0]["PF"].ToString();
                    LblProfessionalTax.Text = ds.Tables[0].Rows[0]["PT"].ToString();
                    //txtTotal_Deductions_B.Text = ds.Tables[0].Rows[0]["Total_Deductions_B"].ToString();
                    //txtNET_PAYMENT.Text = ds.Tables[0].Rows[0]["NET_PAYMENT"].ToString();
                    //ViewState["Staff_SalaryID"] = ds.Tables[0].Rows[0]["Staff_SalaryID"].ToString();
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
    }
}