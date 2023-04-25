using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNC.ErrorLogger;


namespace BusinessLayer
{
    public class StaffSalariesBL
    {
        public enum eLoadSp
        {

            SELECT_SS_ITEMS_BY_Employee_StaffID = 1,
            PRO_Staff_Salaries_SELECT_ALL = 2,
            SS_Delete = 3,
            INSERT = 4,
            UPDATE = 5,
            SELECT_SSDETAILS_BY_Staff_SalaryID = 6,
            SELECT_SSDETAILS_BY_Staff_SalaryID_Print = 7,
            INSERT_Salary_Status = 8
             


        };
        #region "Class: EmployeeContractBL Sets / Gets"


        public int Staff_SalaryID { get; set; }
        public int No_of_Days_Present { get; set; }
        public string EmployeeID { get; set; }
        public string Full_Name { get; set; }
        public string FatherName { get; set; }
        public string Employee_Permanent_Address { get; set; }
        public string Email_ID { get; set; }
        public string Qualification { get; set; }
        public string Gender { get; set; }
        public string Employee_Mobile_No { get; set; }
        public DateTime? Date_of_Birth { get; set; }
        public string Employment_Type { get; set; }
        public string Project { get; set; }
        public string Designation { get; set; }
        public DateTime? Actual_Date_of_Joining { get; set; }
        public string Location { get; set; }
        public string Reporting_Manager { get; set; }
        public string Status { get; set; }
        // Salry
        public Decimal Basic_Pay { get; set; }
        public Decimal HRA { get; set; }
        public Decimal LOP_Amount { get; set; }

        public Decimal CTC { get; set; }
        public Decimal Conveyance_Allowance { get; set; }
        public Decimal Special_Allowance { get; set; }
        public Decimal Sub_Total_A { get; set; }
        public Decimal PF_ER { get; set; }
        public Decimal Total_Cost_to_Company { get; set; }
        public Decimal Deductions { get; set; }
        public Decimal PF { get; set; }
        public Decimal PT { get; set; }
        //public Decimal Total_Deductions_B { get; set; }
        public Decimal NET_PAYMENT { get; set; }

        public decimal Net_Salary { get; set; }
        //Upload Documents
        public string Resume { get; set; }
        public string ID_Proof { get; set; }
        public string Bank_Details { get; set; }
        public string Qualification_Certificates { get; set; }
        public string Pay_Slips { get; set; }
        public string Other_Documents { get; set; }

        public string Task { get; set; }

        public int LOP_Days { get; set; }
        public int Salary_id { get; set; }
        public string Salary_Id_number { get; set; }

        public decimal Sum_of_Net_Salary { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        #endregion
        public string payment_indent_status { get; set; }
        private string getSpName(eLoadSp enumSpName)
        {
            switch (enumSpName)
            {
                case eLoadSp.SELECT_SS_ITEMS_BY_Employee_StaffID:
                    return "PROC_SELECT_ECDETAILS_BY_Employee_ContractID";
                case eLoadSp.PRO_Staff_Salaries_SELECT_ALL:
                    return "PRO_Staff_Service_SELECT_ALL";
                case eLoadSp.SS_Delete:
                    return "PRO_DELETE_StaffSalaries_BY_Staff_SalaryID";
                case eLoadSp.INSERT:
                    return "PROC_SS_INSERT_UPDATE";
                case eLoadSp.UPDATE:
                    return "PROC_SS_INSERT_UPDATE"; 
                case eLoadSp.SELECT_SSDETAILS_BY_Staff_SalaryID:
                    return "PROC_SELECT_SSDETAILS_BY_Staff_SalaryID";
                case eLoadSp.SELECT_SSDETAILS_BY_Staff_SalaryID_Print:
                    return "PROC_SELECT_SSDETAILS_BY_Staff_SalaryID_forPrint";
                case eLoadSp.INSERT_Salary_Status:
                    return "PRO_Salary_Status_INSERT";

                default:
                    return string.Empty;
            }
        }
        private SqlParameter[] getSpParamArray(eLoadSp enumSpName)
        {
            SqlParameter[] colParams = new SqlParameter[]
        {
        };

            switch (enumSpName)
            {
                case eLoadSp.SELECT_SS_ITEMS_BY_Employee_StaffID:
                    //case eLoadSp.SELECT_EC_ITEMS_BY_Employee_ContractID_FOR_PRINT:
                    //case eLoadSp.SELECT_ECDETAILS_BY_Employee_ContractID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID", this.EmployeeID),
                    new SqlParameter("@Task", this.Task)

                };
                    break;

                case eLoadSp.PRO_Staff_Salaries_SELECT_ALL:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Staff_SalaryID", this.Staff_SalaryID),
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@month", this.month),
                    new SqlParameter("@year", this.year)
                    
                };
                    break;
                case eLoadSp.SS_Delete:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID", this.EmployeeID),
                    new SqlParameter("@Task", this.Task)

                };
                    break;
                case eLoadSp.INSERT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Staff_SalaryID", this.Staff_SalaryID)

                };
                    break;
                case eLoadSp.UPDATE:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Staff_SalaryID ", this.Staff_SalaryID)

                };
                    break;

                case eLoadSp.SELECT_SSDETAILS_BY_Staff_SalaryID:
                //case eLoadSp.SELECT_EC_ITEMS_BY_Employee_ContractID_FOR_PRINT:
                //case eLoadSp.SELECT_ECDETAILS_BY_Employee_ContractID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Staff_SalaryID", this.Staff_SalaryID),
                    new SqlParameter("@Task", this.Task)

                };
                    break;

                case eLoadSp.SELECT_SSDETAILS_BY_Staff_SalaryID_Print:
                    //case eLoadSp.SELECT_EC_ITEMS_BY_Employee_ContractID_FOR_PRINT:
                    //case eLoadSp.SELECT_ECDETAILS_BY_Employee_ContractID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Staff_SalaryID", this.Staff_SalaryID),
                    new SqlParameter("@Task", this.Task)

                };
                    break;
                case eLoadSp.INSERT_Salary_Status:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Salary_id ", this.Salary_id)

                };
                    break;

            }
            return colParams;
        }
        public bool load(SqlConnection SqlConn, eLoadSp enumSpName, ref DataSet ds)
        {
            try
            {
                SqlParameter[] colParams = new SqlParameter[]
            {
            };

                colParams = getSpParamArray(enumSpName);

                if (colParams == null)
                {
                    ds = SqlHelper.ExecuteDataset(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName));
                }
                else
                {
                    ds = SqlHelper.ExecuteDataset(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }
        public bool delete(SqlConnection objConn, eLoadSp enmSPName)
        {
            try
            {
                int _intReturnVal;
                SqlParameter[] colParams = getSpParamArray(enmSPName);
                _intReturnVal = SqlHelper.ExecuteNonQuery(objConn, CommandType.StoredProcedure, getSpName(enmSPName), colParams);
                if (_intReturnVal < 1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }
        public bool insert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insert(SqlConn, null, enumSpName);
        }
        public bool insert(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return insert(null, SqlTran, enumSpName);
        }

        public bool INSERT_Salary_Status(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return INSERT_Salary_Status(SqlConn, null, enumSpName);
        }
        public bool INSERT_Salary_Status(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return INSERT_Salary_Status(null, SqlTran, enumSpName);
        }

        private bool insert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
               new SqlParameter("@Staff_SalaryID", SqlDbType.Int,40, ParameterDirection.Input, false, 0, 0, "Staff SalaryID", DataRowVersion.Current, this.Staff_SalaryID),
                new SqlParameter("@Full_Name", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Full_Name", DataRowVersion.Current, this.Full_Name),
                new SqlParameter("@EmployeeID", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "EmployeeID", DataRowVersion.Current, this.EmployeeID),
                //new SqlParameter("@FatherName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "FatherName", DataRowVersion.Current, this.FatherName),
                //new SqlParameter("@Employee_Permanent_Address", SqlDbType.VarChar,1000, ParameterDirection.Input, false, 0, 0, "Employee_Permanent_Address", DataRowVersion.Current, this.Employee_Permanent_Address),
                //new SqlParameter("@Email_ID", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Email_ID", DataRowVersion.Current, this.Email_ID),
                //new SqlParameter("@Qualification", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "Qualification", DataRowVersion.Current, this.Qualification),
                //new SqlParameter("@Gender", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Gender", DataRowVersion.Current, this.Gender),
                //new SqlParameter("@Employee_Mobile_No", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Employee_Mobile_No", DataRowVersion.Current, this.Employee_Mobile_No),
                //new SqlParameter("@Date_of_Birth", SqlDbType.Date,100, ParameterDirection.Input, false, 0, 0, "Date_of_Birth", DataRowVersion.Current, this.Date_of_Birth),
                //new SqlParameter("@Employment_Type", SqlDbType.VarChar,100, ParameterDirection.Input, false, 10, 0, "Employment_Type", DataRowVersion.Current, this.Employment_Type),
                //new SqlParameter("@Project", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "Project", DataRowVersion.Current, this.Project),
                new SqlParameter("@Designation", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "Designation", DataRowVersion.Current, this.Designation),
                new SqlParameter("@Actual_Date_of_Joining", SqlDbType.Date,50, ParameterDirection.Input, false, 10, 0, "Actual_Date_of_Joining", DataRowVersion.Current, this.Actual_Date_of_Joining),
                new SqlParameter("@Location", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "Location", DataRowVersion.Current, this.Location),
                //new SqlParameter("@Reporting_Manager", SqlDbType.VarChar,100, ParameterDirection.Input, false,0, 0, "Reporting_Manager", DataRowVersion.Current, this.Reporting_Manager),
                new SqlParameter("@Status", SqlDbType.VarChar,100, ParameterDirection.Input, false,0, 0, "Status", DataRowVersion.Current, this.Status),
                new SqlParameter("@Basic_Pay", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Basic_Pay", DataRowVersion.Current, this.Basic_Pay),
                new SqlParameter("@HRA", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Basic_Pay", DataRowVersion.Current, this.HRA),
                new SqlParameter("@Conveyance_Allowance", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Conveyance_Allowance", DataRowVersion.Current, this.Conveyance_Allowance),
                new SqlParameter("@Special_Allowance", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Special_Allowance", DataRowVersion.Current, this.Special_Allowance),
                new SqlParameter("@Sub_Total_A", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Sub_Total_A", DataRowVersion.Current, this.Sub_Total_A),
                new SqlParameter("@PF_ER", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Total_Cost_to_Company", DataRowVersion.Current, this.PF_ER),
                new SqlParameter("@Total_Cost_to_Company", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Total_Cost_to_Company", DataRowVersion.Current, this.Total_Cost_to_Company),
                new SqlParameter("@Deductions", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Deductions", DataRowVersion.Current, this.Deductions),
                new SqlParameter("@PF", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "PF", DataRowVersion.Current, this.PF),
                new SqlParameter("@PT", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "PF", DataRowVersion.Current, this.PT),
              new SqlParameter("@Net_Salary", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Net_Salary", DataRowVersion.Current, this.Net_Salary),
                new SqlParameter("@CTC", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "CTC", DataRowVersion.Current, this.CTC),
                new SqlParameter("@No_of_Days_Present", SqlDbType.Int, 9, ParameterDirection.Input, false, 0, 0, "No_of_Days_Present", DataRowVersion.Current, this.No_of_Days_Present),
                 new SqlParameter("@LOP_Amount", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "LOP_Amount", DataRowVersion.Current, this.LOP_Amount),
                   new SqlParameter("@LOP_Days", SqlDbType.Int, 9, ParameterDirection.Input, false, 0, 0, "LOP_Days", DataRowVersion.Current, this.LOP_Days),
                   new SqlParameter("@NET_PAYMENT", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "NET_PAYMENT", DataRowVersion.Current, this.NET_PAYMENT),
                 new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false,0, 0, "Task", DataRowVersion.Current, this.Task),


            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                    // string check;
                    //check = colParams[0].Value.ToString();

                    ////this.WONo = (string)colParams.First().Value;
                    //this.Employee_ContractID = (int)colParams[0].Value;
                }

                if (SqlTran != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlTran, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                }
                if (intIdentityValue < 1)
                {
                    return false;
                }
                return true;
            }

            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }

        }
        private bool INSERT_Salary_Status(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
               new SqlParameter("@Salary_id", SqlDbType.Int,40, ParameterDirection.Input, false, 0, 0, "Salary_id", DataRowVersion.Current, this.Salary_id),
                new SqlParameter("@Salary_Id_number", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Salary_Id_number", DataRowVersion.Current, this.Salary_Id_number),
                new SqlParameter("@month", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "month", DataRowVersion.Current, this.month),
              new SqlParameter("@year", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "year", DataRowVersion.Current, this.year),
                   new SqlParameter("@Sum_of_Net_Salary", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Sum_of_Net_Salary", DataRowVersion.Current, this.Sum_of_Net_Salary),
                 new SqlParameter("@payment_indent_status", SqlDbType.VarChar, 50, ParameterDirection.Input, false,0, 0, "payment_indent_status", DataRowVersion.Current, this.payment_indent_status),
                 new SqlParameter("@Staff_SalaryID", SqlDbType.Int, 50, ParameterDirection.Input, false,0, 0, "Staff_SalaryID", DataRowVersion.Current, this.Staff_SalaryID),
                 

            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                    // string check;
                    //check = colParams[0].Value.ToString();

                    ////this.WONo = (string)colParams.First().Value;
                    //this.Employee_ContractID = (int)colParams[0].Value;
                }

                if (SqlTran != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlTran, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                }
                if (intIdentityValue < 1)
                {
                    return false;
                }
                return true;
            }

            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }

        }
        
        private bool validate()
        {
            try
            {
                return true;
            }
            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }
        private bool update(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {

                new SqlParameter("@Staff_SalaryID", SqlDbType.Int,40, ParameterDirection.Input, false, 0, 0, "Staff SalaryID", DataRowVersion.Current, this.Staff_SalaryID),
                new SqlParameter("@Full_Name", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Full_Name", DataRowVersion.Current, this.Full_Name),
                new SqlParameter("@EmployeeID", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "EmployeeID", DataRowVersion.Current, this.EmployeeID),
               
                new SqlParameter("@Designation", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "Designation", DataRowVersion.Current, this.Designation),
                new SqlParameter("@Actual_Date_of_Joining", SqlDbType.Date,50, ParameterDirection.Input, false, 10, 0, "Actual_Date_of_Joining", DataRowVersion.Current, this.Actual_Date_of_Joining),
                new SqlParameter("@Location", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "Location", DataRowVersion.Current, this.Location),
                
                new SqlParameter("@Status", SqlDbType.VarChar,100, ParameterDirection.Input, false,0, 0, "Status", DataRowVersion.Current, this.Status),
                new SqlParameter("@Basic_Pay", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Basic_Pay", DataRowVersion.Current, this.Basic_Pay),
                new SqlParameter("@HRA", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Basic_Pay", DataRowVersion.Current, this.HRA),
                new SqlParameter("@Conveyance_Allowance", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Conveyance_Allowance", DataRowVersion.Current, this.Conveyance_Allowance),
                new SqlParameter("@Special_Allowance", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Special_Allowance", DataRowVersion.Current, this.Special_Allowance),
                new SqlParameter("@Sub_Total_A", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Sub_Total_A", DataRowVersion.Current, this.Sub_Total_A),
                new SqlParameter("@PF_ER", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Total_Cost_to_Company", DataRowVersion.Current, this.PF_ER),
                new SqlParameter("@Total_Cost_to_Company", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Total_Cost_to_Company", DataRowVersion.Current, this.Total_Cost_to_Company),
                new SqlParameter("@Deductions", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Deductions", DataRowVersion.Current, this.Deductions),
                new SqlParameter("@PF", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "PF", DataRowVersion.Current, this.PF),
                new SqlParameter("@PT", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "PF", DataRowVersion.Current, this.PT),
                //new SqlParameter("@Total_Deductions_B", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Total_Deductions_B", DataRowVersion.Current, this.Total_Deductions_B),
                new SqlParameter("@Net_Salary", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Net_Salary", DataRowVersion.Current, this.Net_Salary),
                new SqlParameter("@CTC", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "CTC", DataRowVersion.Current, this.CTC),
                new SqlParameter("@No_of_Days_Present", SqlDbType.Int, 9, ParameterDirection.Input, false, 0, 0, "No_of_Days_Present", DataRowVersion.Current, this.No_of_Days_Present),
                 new SqlParameter("@LOP_Amount", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "LOP_Amount", DataRowVersion.Current, this.LOP_Amount),
                   new SqlParameter("@LOP_Days", SqlDbType.Int, 9, ParameterDirection.Input, false, 0, 0, "LOP_Days", DataRowVersion.Current, this.LOP_Days),
                   new SqlParameter("@NET_PAYMENT", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "NET_PAYMENT", DataRowVersion.Current, this.NET_PAYMENT),
                 new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false,0, 0, "Task", DataRowVersion.Current, this.Task),

                



            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                    this.Staff_SalaryID = (int)colParams.First().Value;
                }

                if (SqlTran != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlTran, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                }

                if (intReturnVal < 1)
                {
                    return false;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }
        public bool update(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return update(SqlConn, null, enumSpName);
        }
        private bool updateStaff(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {

                new SqlParameter("@Staff_SalaryID", SqlDbType.Int,40, ParameterDirection.Input, false, 0, 0, "Staff SalaryID", DataRowVersion.Current, this.Staff_SalaryID),
               

               
                new SqlParameter("@Basic_Pay", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Basic_Pay", DataRowVersion.Current, this.Basic_Pay),
                new SqlParameter("@HRA", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Basic_Pay", DataRowVersion.Current, this.HRA),
                new SqlParameter("@Conveyance_Allowance", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Conveyance_Allowance", DataRowVersion.Current, this.Conveyance_Allowance),
                new SqlParameter("@Special_Allowance", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Special_Allowance", DataRowVersion.Current, this.Special_Allowance),
               // new SqlParameter("@Sub_Total_A", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Sub_Total_A", DataRowVersion.Current, this.Sub_Total_A),
                new SqlParameter("@PF_ER", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "PF_ER", DataRowVersion.Current, this.PF_ER),
                //new SqlParameter("@Total_Cost_to_Company", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Total_Cost_to_Company", DataRowVersion.Current, this.Total_Cost_to_Company),
                new SqlParameter("@Deductions", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Deductions", DataRowVersion.Current, this.Deductions),
                new SqlParameter("@PF", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "PF", DataRowVersion.Current, this.PF),
                new SqlParameter("@PT", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "PT", DataRowVersion.Current, this.PT),
                //new SqlParameter("@Total_Deductions_B", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Total_Deductions_B", DataRowVersion.Current, this.Total_Deductions_B),
                //new SqlParameter("@Net_Salary", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Net_Salary", DataRowVersion.Current, this.Net_Salary),
               // new SqlParameter("@CTC", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "CTC", DataRowVersion.Current, this.CTC),
                new SqlParameter("@No_of_Days_Present", SqlDbType.Int, 9, ParameterDirection.Input, false, 0, 0, "No_of_Days_Present", DataRowVersion.Current, this.No_of_Days_Present),
                 new SqlParameter("@LOP_Amount", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "LOP_Amount", DataRowVersion.Current, this.LOP_Amount),
                   new SqlParameter("@LOP_Days", SqlDbType.Int, 9, ParameterDirection.Input, false, 0, 0, "LOP_Days", DataRowVersion.Current, this.LOP_Days),
                  // new SqlParameter("@NET_PAYMENT", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "NET_PAYMENT", DataRowVersion.Current, this.NET_PAYMENT),
                 new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false,0, 0, "Task", DataRowVersion.Current, this.Task),





            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                    this.Staff_SalaryID = (int)colParams.First().Value;
                }

                if (SqlTran != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlTran, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                }

                if (intReturnVal < 1)
                {
                    return false;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }
        public bool updateStaff(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updateStaff(SqlConn, null, enumSpName);
        }
    }
}
