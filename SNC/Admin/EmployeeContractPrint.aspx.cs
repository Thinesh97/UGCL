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

public partial class EmployeeContractPrint : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    DataSet ds = null;
    EmployeeContractBL objEC = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["UID"] != null)
            {
                if (Request.QueryString["EmployeeID"] != null)
                {
                    GetEmployeeContractDetails(Request.QueryString["EmployeeID"].ToString());
                    //BindWOItems();
                }

                else
                {
                    Response.Redirect("../CommonPages/Login.aspx", false);
                }
            }
        }
    }

    private void GetEmployeeContractDetails(string EmployeeID)
    {
        Decimal CalYearly;
        Decimal CalTotalYealy;
        objEC = new EmployeeContractBL();
        ds = new DataSet();
        objEC.EmployeeID = EmployeeID;
        GeneralClass objGen = new GeneralClass();
        objEC.Task = "GetEmployeeContractDetails";
        objEC.load(con, EmployeeContractBL.eLoadSp.SELECT_ECDETAILS_BY_Employee_ContractID, ref ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            CalYearly = Convert.ToDecimal(ds.Tables[0].Rows[0]["Total_Cost_to_Company"].ToString());
            CalTotalYealy = CalYearly * 12;
            lblTotal_Cost_to_Company_y.Text = CalTotalYealy.ToString();
            lblTotal_Cost_to_Company_Yearly_InWords.Text = objGen.changeNumericToWordsINR(Convert.ToDouble(CalTotalYealy));
            lblFull_Name.Text = ds.Tables[0].Rows[0]["Full_Name"].ToString();
            lblFull_Name8.Text = ds.Tables[0].Rows[0]["Full_Name"].ToString();
            lblPermanent_Address.Text = ds.Tables[0].Rows[0]["Employee_Permanent_Address"].ToString();
            lblFatherName.Text = ds.Tables[0].Rows[0]["FatherName"].ToString();
            lblActualDOfJoining1.Text = ds.Tables[0].Rows[0]["Actual_Date_of_Joining"].ToString();
            lblActualDOfJoining.Text = ds.Tables[0].Rows[0]["Actual_Date_of_Joining"].ToString();
            lblActualDOfJoining2.Text = ds.Tables[0].Rows[0]["Actual_Date_of_Joining"].ToString();
            lblFull_Name1.Text = ds.Tables[0].Rows[0]["Full_Name"].ToString();
            lblPermanent_Address1.Text = ds.Tables[0].Rows[0]["Employee_Permanent_Address"].ToString();
            lblFull_Name2.Text = ds.Tables[0].Rows[0]["Full_Name"].ToString();
            lblDesignation.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
            lblFull_Name3.Text = ds.Tables[0].Rows[0]["Full_Name"].ToString();
            lblFull_Name6.Text = ds.Tables[0].Rows[0]["Full_Name"].ToString();
            lblPermanent_Address2.Text = ds.Tables[0].Rows[0]["Employee_Permanent_Address"].ToString();
            lblFull_Name4.Text = ds.Tables[0].Rows[0]["Employee_Permanent_Address"].ToString();
            lblActualDOfJoining3.Text = ds.Tables[0].Rows[0]["Actual_Date_of_Joining"].ToString();
            lblFull_Name4.Text = ds.Tables[0].Rows[0]["Full_Name"].ToString();
            lblPermanent_Address3.Text = ds.Tables[0].Rows[0]["Employee_Permanent_Address"].ToString();
            lblBasic_Pay.Text = ds.Tables[0].Rows[0]["Basic_Pay"].ToString();
            lblBasic_PayY.Text = ds.Tables[0].Rows[0]["Basic_Pay"].ToString();
            lblHouse_Rental_Allowance.Text = ds.Tables[0].Rows[0]["HRA"].ToString();
            lblHouse_Rental_AllowanceY.Text = ds.Tables[0].Rows[0]["HRA"].ToString();
            lblConveyance_Allowance.Text = ds.Tables[0].Rows[0]["Conveyance_Allowance"].ToString();
            lblConveyance_AllowanceY.Text = ds.Tables[0].Rows[0]["Conveyance_Allowance"].ToString();
            lblSpecial_Allowance.Text = ds.Tables[0].Rows[0]["Special_Allowance"].ToString();
            lblSpecial_AllowanceY.Text = ds.Tables[0].Rows[0]["Special_Allowance"].ToString();
            lblSub_Total_A.Text = ds.Tables[0].Rows[0]["Sub_Total_A"].ToString();
            lblSub_Total_AY.Text = ds.Tables[0].Rows[0]["Sub_Total_A"].ToString();
            lblPF_ER.Text = ds.Tables[0].Rows[0]["PF_ER"].ToString();
            lblPF_ERY.Text = ds.Tables[0].Rows[0]["PF_ER"].ToString();
            lblTotal_Cost_to_Company.Text = ds.Tables[0].Rows[0]["Total_Cost_to_Company"].ToString();
            lblTotal_Cost_to_CompanyY.Text = ds.Tables[0].Rows[0]["Total_Cost_to_Company"].ToString();
            lblDeductions.Text = ds.Tables[0].Rows[0]["Deductions"].ToString();
            lblDeductionsY.Text = ds.Tables[0].Rows[0]["Deductions"].ToString();
            lblPF.Text = ds.Tables[0].Rows[0]["PF"].ToString();
            lblPFY.Text = ds.Tables[0].Rows[0]["PF"].ToString();
            lblPT.Text = ds.Tables[0].Rows[0]["PT"].ToString();
            lblPTY.Text = ds.Tables[0].Rows[0]["PT"].ToString();
            lblTotal_Deductions.Text = ds.Tables[0].Rows[0]["Total_Deductions_B"].ToString();
            lblTotal_DeductionsY.Text = ds.Tables[0].Rows[0]["Total_Deductions_B"].ToString();
            lblNET_PAYMENT.Text = ds.Tables[0].Rows[0]["NET_PAYMENT"].ToString();
            lblNET_PAYMENTY.Text = ds.Tables[0].Rows[0]["NET_PAYMENT"].ToString();
        }
    }
}