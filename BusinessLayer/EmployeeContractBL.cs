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
    public class EmployeeContractBL
    {
        #region "Class: WorkOrderBL Local Declarations"

        public enum eLoadSp
        {
            INSERT = 0,
            UPDATE = 1,
            SELECT_EC_ITEMS_BY_Employee_ContractID = 2,
            SELECT_EC_ITEMS_BY_Employee_ContractID_FOR_PRINT = 3,
            PRO_EmployeeContract_SELECT_ALL = 4,
            SELECT_ECDETAILS_BY_Employee_ContractID = 5,
            PRO_Employee_Contract_SELECT_ALL = 6,
            EC_Delete = 7,
        };

        #endregion
        #region "Class: EmployeeContractBL Sets / Gets"

        public int Employee_ContractID { get; set; }
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
        public Decimal Conveyance_Allowance { get; set; }
        public Decimal Special_Allowance { get; set; }
        public Decimal Sub_Total_A { get; set; }
        public Decimal PF_ER { get; set; }
        public Decimal Total_Cost_to_Company { get; set; }
        public Decimal Deductions { get; set; }
        public Decimal PF { get; set; }
        public Decimal PT { get; set; }
        public Decimal Total_Deductions_B { get; set; }
        public Decimal NET_PAYMENT { get; set; }
        //Upload Documents
        public string Resume { get; set; }
        public string ID_Proof { get; set; }
        public string Bank_Details { get; set; }
        public string Qualification_Certificates { get; set; }
        public string Pay_Slips { get; set; }
        public string Other_Documents { get; set; }

        public string Task { get; set; }


        #endregion
        #region "Class: Employee Contract  Methods"

        /// <summary>
        /// Function Name:  fillObjectFromDr.
        /// Called By:      Nill.
        /// Description:    Fill the object from data reader. 
        /// Change history: Nill.
        private bool fillObjectFromDr(SqlDataReader dr)
        {
            try
            {
                // Loop though the data reader
                while (dr.Read())
                {
                    //clsCountry.CountryName
                    if (dr["UOM"] != DBNull.Value)
                    {
                        //this.UOM = dr["UOM"].ToString();
                    }

                }
                return true;
            }
            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }
        /// <summary>
        /// Function Name:  getSpName.
        /// Called By:      Nill.
        /// Description:    Get the SpName from enumSpName 
        /// Change history: Nill.
        /// </summary>

        private string getSpName(eLoadSp enumSpName)
        {
            switch (enumSpName)
            {

                case eLoadSp.INSERT:
                    return "PROC_EC_INSERT_UPDATE";
                case eLoadSp.UPDATE:
                    return "PROC_EC_INSERT_UPDATE";
                case eLoadSp.SELECT_ECDETAILS_BY_Employee_ContractID:
                    return "PROC_SELECT_ECDETAILS_BY_Employee_ContractID";
                case eLoadSp.PRO_Employee_Contract_SELECT_ALL:
                    return "PRO_Employee_Contract_SELECT_ALL";
                case eLoadSp.EC_Delete:
                    return "PRO_DELETE_EmployeeContract_BY_Employee_ContractID";
                default:
                    return string.Empty;
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

        private bool insert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
               new SqlParameter("@Employee_ContractID", SqlDbType.Int,40, ParameterDirection.Input, false, 0, 0, "Employee_ContractID", DataRowVersion.Current, this.Employee_ContractID),
                new SqlParameter("@Full_Name", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Full_Name", DataRowVersion.Current, this.Full_Name),
                new SqlParameter("@EmployeeID", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "EmployeeID", DataRowVersion.Current, this.EmployeeID),
                new SqlParameter("@FatherName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "FatherName", DataRowVersion.Current, this.FatherName),
                new SqlParameter("@Employee_Permanent_Address", SqlDbType.VarChar,1000, ParameterDirection.Input, false, 0, 0, "Employee_Permanent_Address", DataRowVersion.Current, this.Employee_Permanent_Address),
                new SqlParameter("@Email_ID", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Email_ID", DataRowVersion.Current, this.Email_ID),
                new SqlParameter("@Qualification", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "Qualification", DataRowVersion.Current, this.Qualification),
                new SqlParameter("@Gender", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Gender", DataRowVersion.Current, this.Gender),
                new SqlParameter("@Employee_Mobile_No", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Employee_Mobile_No", DataRowVersion.Current, this.Employee_Mobile_No),
                new SqlParameter("@Date_of_Birth", SqlDbType.Date,100, ParameterDirection.Input, false, 0, 0, "Date_of_Birth", DataRowVersion.Current, this.Date_of_Birth),
                new SqlParameter("@Employment_Type", SqlDbType.VarChar,100, ParameterDirection.Input, false, 10, 0, "Employment_Type", DataRowVersion.Current, this.Employment_Type),
                new SqlParameter("@Project", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "Project", DataRowVersion.Current, this.Project),
                new SqlParameter("@Designation", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "Designation", DataRowVersion.Current, this.Designation),
                new SqlParameter("@Actual_Date_of_Joining", SqlDbType.Date,50, ParameterDirection.Input, false, 10, 0, "Actual_Date_of_Joining", DataRowVersion.Current, this.Actual_Date_of_Joining),
                new SqlParameter("@Location", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "Location", DataRowVersion.Current, this.Location),
                new SqlParameter("@Reporting_Manager", SqlDbType.VarChar,100, ParameterDirection.Input, false,0, 0, "Reporting_Manager", DataRowVersion.Current, this.Reporting_Manager),
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
                new SqlParameter("@Total_Deductions_B", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Total_Deductions_B", DataRowVersion.Current, this.Total_Deductions_B),
                new SqlParameter("@NET_PAYMENT", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "NET_PAYMENT", DataRowVersion.Current, this.NET_PAYMENT),
                 new SqlParameter("@Resume", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Full_Name", DataRowVersion.Current, this.Resume),
                new SqlParameter("@ID_Proof", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Full_Name", DataRowVersion.Current, this.ID_Proof),
                new SqlParameter("@Bank_Details", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Full_Name", DataRowVersion.Current, this.Bank_Details),
                new SqlParameter("@Qualification_Certificates", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Full_Name", DataRowVersion.Current, this.Qualification_Certificates),
                new SqlParameter("@Pay_Slips", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Full_Name", DataRowVersion.Current, this.Pay_Slips),
                new SqlParameter("@Other_Documents", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Full_Name", DataRowVersion.Current, this.Other_Documents),
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
        private bool update(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{   
                
                new SqlParameter("@Employee_ContractID", SqlDbType.Int,40, ParameterDirection.Input, false, 0, 0, "Employee_ContractID", DataRowVersion.Current, this.Employee_ContractID),
                new SqlParameter("@Full_Name", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Full_Name", DataRowVersion.Current, this.Full_Name),
                new SqlParameter("@EmployeeID", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "EmployeeID", DataRowVersion.Current, this.EmployeeID),
                new SqlParameter("@FatherName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "FatherName", DataRowVersion.Current, this.FatherName),
                new SqlParameter("@Employee_Permanent_Address", SqlDbType.VarChar,1000, ParameterDirection.Input, false, 0, 0, "Employee_Permanent_Address", DataRowVersion.Current, this.Employee_Permanent_Address),
                new SqlParameter("@Email_ID", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Email_ID", DataRowVersion.Current, this.Email_ID),
                new SqlParameter("@Qualification", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "Qualification", DataRowVersion.Current, this.Qualification),
                new SqlParameter("@Gender", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Gender", DataRowVersion.Current, this.Gender),
                new SqlParameter("@Employee_Mobile_No", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Employee_Mobile_No", DataRowVersion.Current, this.Employee_Mobile_No),
                new SqlParameter("@Date_of_Birth", SqlDbType.Date,100, ParameterDirection.Input, false, 0, 0, "Date_of_Birth", DataRowVersion.Current, this.Date_of_Birth),
                new SqlParameter("@Employment_Type", SqlDbType.VarChar,100, ParameterDirection.Input, false, 10, 0, "Employment_Type", DataRowVersion.Current, this.Employment_Type),
                new SqlParameter("@Project", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "Project", DataRowVersion.Current, this.Project),
                new SqlParameter("@Designation", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "Designation", DataRowVersion.Current, this.Designation),
                new SqlParameter("@Actual_Date_of_Joining", SqlDbType.Date,50, ParameterDirection.Input, false, 10, 0, "Actual_Date_of_Joining", DataRowVersion.Current, this.Actual_Date_of_Joining),
                new SqlParameter("@Location", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "Location", DataRowVersion.Current, this.Location),
                new SqlParameter("@Reporting_Manager", SqlDbType.VarChar,100, ParameterDirection.Input, false,0, 0, "Reporting_Manager", DataRowVersion.Current, this.Reporting_Manager),
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
                new SqlParameter("@Total_Deductions_B", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Total_Deductions_B", DataRowVersion.Current, this.Total_Deductions_B),
                new SqlParameter("@NET_PAYMENT", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "NET_PAYMENT", DataRowVersion.Current, this.NET_PAYMENT),
                 new SqlParameter("@Resume", SqlDbType.VarChar,4, ParameterDirection.Input, false, 0, 0, "Full_Name", DataRowVersion.Current, this.Resume),
                new SqlParameter("@ID_Proof", SqlDbType.VarChar,4, ParameterDirection.Input, false, 0, 0, "Full_Name", DataRowVersion.Current, this.ID_Proof),
                new SqlParameter("@Bank_Details", SqlDbType.VarChar,4, ParameterDirection.Input, false, 0, 0, "Full_Name", DataRowVersion.Current, this.Bank_Details),
                new SqlParameter("@Qualification_Certificates", SqlDbType.VarChar,4, ParameterDirection.Input, false, 0, 0, "Full_Name", DataRowVersion.Current, this.Qualification_Certificates),
                new SqlParameter("@Pay_Slips", SqlDbType.VarChar,4, ParameterDirection.Input, false, 0, 0, "Full_Name", DataRowVersion.Current, this.Pay_Slips),
                new SqlParameter("@Other_Documents", SqlDbType.VarChar,4, ParameterDirection.Input, false, 0, 0, "Full_Name", DataRowVersion.Current, this.Other_Documents),
                 new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false,0, 0, "Task", DataRowVersion.Current, this.Task),
			};

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                    this.Employee_ContractID = (int)colParams.First().Value;
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
        private SqlParameter[] getSpParamArray(eLoadSp enumSpName)
        {
            SqlParameter[] colParams = new SqlParameter[]
		{
		};

            switch (enumSpName)
            {

                case eLoadSp.INSERT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Employee_ContractID ", this.Employee_ContractID)
                     
                };
                    break;
                case eLoadSp.UPDATE:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Employee_ContractID ", this.Employee_ContractID)
                     
                };
                    break;

                case eLoadSp.SELECT_EC_ITEMS_BY_Employee_ContractID:
                case eLoadSp.SELECT_EC_ITEMS_BY_Employee_ContractID_FOR_PRINT:
                case eLoadSp.SELECT_ECDETAILS_BY_Employee_ContractID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID", this.EmployeeID),
                    new SqlParameter("@Task", this.Task)
                    
                };
                    break;
                case eLoadSp.EC_Delete:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID", this.EmployeeID),
                    new SqlParameter("@Task", this.Task)
                    
                };
                    break;
                case eLoadSp.PRO_Employee_Contract_SELECT_ALL:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Employee_ContractID", this.Employee_ContractID),
                    new SqlParameter("@Task", this.Task)
                    
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
        public bool load(SqlConnection SqlConn, eLoadSp enumSpName, ref Dictionary<int, ProjectBL> diclCountry)
        {
            SqlDataReader dr = null;

            try
            {
                SqlParameter[] colParams = new SqlParameter[]
			{				
			};

                colParams = getSpParamArray(enumSpName);

                if (colParams == null)
                {
                    dr = SqlHelper.ExecuteReader(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName));
                }
                else
                {
                    dr = SqlHelper.ExecuteReader(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                }
                //
                //Build a collection of objects based on the data reader.
                //
                if (fillCollectionFromDr(dr, ref diclCountry))
                {
                    return true;
                }
                else
                {
                    //clsErrors.Add("VEND_LD");
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
            finally
            {
                if (dr != null)
                {
                    if (dr.IsClosed != true)
                    {
                        dr.Close();
                    }
                }
            }
        }

        public bool load(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            SqlDataReader dr = null;

            try
            {
                SqlParameter[] colParams = new SqlParameter[]
            {				
            };

                colParams = getSpParamArray(enumSpName);

                if (colParams == null)
                {
                    dr = SqlHelper.ExecuteReader(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName));
                }
                else
                {
                    dr = SqlHelper.ExecuteReader(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                }

                if (fillObjectFromDr(dr))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.Message.First();
                return false;
            }
            finally
            {
                if (dr != null)
                {
                    if (dr.IsClosed != true)
                    {
                        dr.Close();
                    }
                }
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
        private bool fillCollectionFromDr(SqlDataReader dr, ref Dictionary<int, ProjectBL> dicCountry)
        {
            if (dicCountry == null)
            {
                dicCountry = new Dictionary<int, ProjectBL>();
            }

            try
            {
                // Loop though the data reader
                while (dr.Read())
                {
                    //clsCountry.CountryName
                    if (dr["Employee_ContractID"] != DBNull.Value)
                    {
                        this.Employee_ContractID = Convert.ToInt32(dr["Employee_ContractID"].ToString());
                    }


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
        #endregion
    }
}
