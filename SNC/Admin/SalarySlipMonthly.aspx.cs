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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections;

namespace SNC.Admin
{
    public partial class SalarySlipMonthly : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        DataSet ds = null;
        DataSet Accessds = new DataSet();
        StaffSalariesBL objViewSS = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try

            {
                if (Session["UID"] != null)
                {
                    if (!IsPostBack)
                    {
                        ddlyear.Items.Add("-Select-");

                        BindYear();
                        EmployeeContractBindList();
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void BindYear()
        {

            int StartYear = 2010;
            int EndYear = 2040;
            string year = "";
           
            for (int i = 2010; i < EndYear; i++)
            {
                year = i.ToString();
                ddlyear.Items.Add(year);
                
            }
          
        }

        protected void ddlSalarymonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvsalary.Visible = true;
        }
        protected void Grid_monthsalary_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
                {
                    HyperLink Editlik = e.Row.Cells[0].FindControl("lnkSSNo") as HyperLink;
                    Editlik.NavigateUrl = "~/Admin/StaffSalaries.aspx?EmployeeID=" + Editlik.Text;
                    if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                    {
                        if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                        {
                            if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["EmpContract_Update"].ToString()))
                            {
                                Editlik.NavigateUrl = "";
                            }
                        }
                    }

                    LinkButton lnkWO_Delete = e.Row.Cells[16].FindControl("lnkSS_Delete") as LinkButton;
                    if (Accessds.Tables[0].Rows[0]["EmpContract_Delete"].ToString() == "" || Accessds.Tables[0].Rows[0]["EmpContract_Delete"].ToString() == "False" || Accessds.Tables[0].Rows[0]["EmpContract_Delete"].ToString() == "0")
                    {
                        if (e.Row.Cells[13].Text == "Approved")
                        {
                            lnkWO_Delete.Visible = false;
                        }
                        else
                        {
                            lnkWO_Delete.Visible = true;
                        }
                    }
                    else
                    {
                        lnkWO_Delete.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void lnkSS_Delete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (HF_Confirm.Value != "false")
                {
                    //type=   RegisterStartupScript(typeof(Page), "exampleScript", "if(confirm('are you confirm?')) { document.getElementById('btn').click(); } ", true)
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    Hashtable dataItem = Gridmonthsalary.Rows[rowIndex].ToHashtable() as Hashtable;

                    objViewSS = new StaffSalariesBL();

                    objViewSS.EmployeeID = dataItem["EmployeeID"].ToString();
                    objViewSS.Task = "DeleteStaffSalary";
                    if (objViewSS.delete(con, StaffSalariesBL.eLoadSp.SS_Delete))
                    {
                        EmployeeContractBindList();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Staff salary Details has been deleted successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Staff salary Details is refered other process!');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void EmployeeContractBindList()
        {
            try
            {
                ds = new DataSet();

                objViewSS = new StaffSalariesBL();
                //objViewECBL.ProjectCode = Session["Project_Code"].ToString();
                objViewSS.Task = "StaffSalaryList";
                objViewSS.load(con, StaffSalariesBL.eLoadSp.PRO_Staff_Salaries_SELECT_ALL, ref ds);
                Gridmonthsalary.DataSource = ds;
                Gridmonthsalary.DataBind();
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            //string filePath = (sender as LinkButton).CommandArgument;
            //Response.ContentType = ContentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            //Response.WriteFile(filePath);
            //Response.End();
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            
        }

        protected void Btngenbulk_Click(object sender, EventArgs e)
        {
            
        }

        protected void Sumofnetpay(object sender, EventArgs e)
        {
            
        }

        protected void Gridmonthsalary_UpdateCommand(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            try
            {

                objViewSS = new StaffSalariesBL();
                objViewSS.EmployeeID = e.Record["Staff_SalaryID"].ToString();
               
                if (e.Record["Full_Name"].ToString() != "")
                {
                    objViewSS.Full_Name = e.Record["Full_Name"].ToString();
                }
                if (e.Record["EmployeeID"].ToString() != "")
                {
                    objViewSS.FatherName = e.Record["EmployeeID"].ToString();
                }
                if (e.Record["Designation"].ToString() != "")
                {
                    objViewSS.Designation = e.Record["Designation"].ToString();
                }
                if (e.Record["Assinged_to_Project"].ToString() != "")
                {
                    //objViewSS.Assinged_to_Project = Convert.ToDateTime(e.Record["Assinged_to_Project"].ToString());
                }
                if (e.Record["Basic_Pay"].ToString() != "")
                {
                    objViewSS.Basic_Pay = Convert.ToDecimal(e.Record["Basic_Pay"].ToString());
                }
                if (e.Record["HRA"].ToString() != "")
                {
                    objViewSS.HRA = Convert.ToDecimal(e.Record["HRA"].ToString());
                }

                if (e.Record["Conveyance_Allowance"].ToString() != "")
                {
                    objViewSS.Conveyance_Allowance = Convert.ToDecimal(e.Record["Conveyance_Allowance"].ToString());
                }

                if (e.Record["Special_Allowance"].ToString() != "")
                {
                    objViewSS.Special_Allowance = Convert.ToDecimal(e.Record["Special_Allowance"].ToString());
                }
                if (e.Record["Sub_Total_A"].ToString() != "")
                {
                    objViewSS.Sub_Total_A = Convert.ToDecimal(e.Record["Sub_Total_A"].ToString());
                }
                if (e.Record["PF_ER"].ToString() != "")
                {
                    objViewSS.PF_ER = Convert.ToDecimal(e.Record["PF_ER"].ToString());
                }
                if (e.Record["CTC"].ToString() != "")
                {
                    objViewSS.CTC = Convert.ToDecimal(e.Record["CTC"].ToString());
                }
                if (e.Record["PF"].ToString() != "")
                {
                    objViewSS.PF = Convert.ToDecimal(e.Record["PF"].ToString());
                }
                if (e.Record["PT"].ToString() != "")
                {
                    objViewSS.PF = Convert.ToDecimal(e.Record["PT"].ToString());
                }
                if (e.Record["Net_Salary"].ToString() != "")
                {
                    objViewSS.Net_Salary= Convert.ToDecimal(e.Record["NET_PAYMENT"].ToString());
                }
                if (e.Record["Deductions"].ToString() != "")
                {
                    objViewSS.Deductions = Convert.ToDecimal(e.Record["Deductions"].ToString());
                }
                if (e.Record["No_of_Days_Present"].ToString() != "")
                {
                    objViewSS.No_of_Days_Present = Convert.ToInt32(e.Record["No_of_Days_Present"].ToString());
                }
                if (e.Record["LOP_Amount"].ToString() != "")
                {
                    objViewSS.LOP_Amount= Convert.ToDecimal(e.Record["LOP_Amount"].ToString());
                }
                if (e.Record["LOP_Days"].ToString() != "")
                {
                    objViewSS.LOP_Days = Convert.ToInt32(e.Record["LOP_Days"].ToString());
                }
                if (e.Record["NET_PAYMENT"].ToString() != "")
                {
                    objViewSS.NET_PAYMENT = Convert.ToDecimal(e.Record["NET_PAYMENT"].ToString());
                }



                //objPaymentIndentBL.Invoice_Date =Convert.ToDateTime("");

                objViewSS.Task = "UpdateSS";
                if (objViewSS.update(con, StaffSalariesBL.eLoadSp.UPDATE))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Update successfull');", true);
                   // BindPaymentIndentList();
                }
            }
            catch (Exception ex)
            {
               // ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
            }
        }
    }
       
    
}