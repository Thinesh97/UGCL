using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{

    public class VendorBL
    {
        #region "Class: VendorANDSubCon Local Declarations"

        public enum eLoadSp
        {
            SELECT_ALL = 0,
            INSERT = 1,
            SELECT_ALL_COUNTRY = 2,
            SELECT_ALL_State = 3,
            SELECT_ALL_BANK = 4,
            SELECT_VENDOR_ALL = 5,
            SELECT_VENDORDETAILS_BY_ID = 6,
            UPDATE = 7,
            DELETE = 8,
            SELECT_ASSET_VENDOR_ALL = 9,
            SELECT_ALL_VENDOR_NAMES = 10,
            SELECT_ALL_BANK_Detail = 11,
            SELECT_BANK_LIST = 12,
            INSERT_SUB_VENDOR_DETAILS = 13,
            SUB_VENDOR_SELECT_ALL_DETAIL = 14,
            DELETE_SUB_VENDOR_DETAIL = 15,
            TB_BANK_SELECT_ALL_DETAIL_BY_ID = 16,
            SELECT_BANK_LIST_PAYMENT_IND=17,

        };
        #endregion
        /// <summary>
        /// Class Name:     fillCollectionFromDr.
        /// Called By:      Nill.
        /// Description:    Get or set the data from parameter value. 
        /// Change history: Nill.
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="dicAssociateMapping"></param>
        /// <returns></returns>
        #region "Class: Vendor Sets / Gets"
        public string Task
        {
            get;
            set;
        }
        public string ddlbank
        {
            get;
            set;
        }
        public string Vendor_ID
        {
            get;
            set;
        }
        
        public string Vendor_name
        {
            get;
            set;
        }
        public string Legal_Name
        {
            get;
            set;
        }
        public string RefPersonName
        {
            get;
            set;
        }
        public string Regs_No
        {
            get;
            set;
        }
        public string PFRegistartionNo
        {
            get;
            set;
        }
        public string LabourLicenseNo
        {
            get;
            set;
        }
        public bool Use_PAN
        {
            get; set;
        }



        public bool ChkRef
        {
            get;
            set;
        }

        public string PAN_NO
        {
            get;
            set;
        }
        public string Con_Person
        {
            get; set;
        }

        public string Con_No
        {
            get; set;
        }
        public string Email_ID
        {
            get; set;
        }

        public int BankID
        {
            get; set;
        }
        public int ID
        {
            get; set;
        }
        public string Add_Line
        {
            get; set;
        }

        public string Landmark
        {
            get;
            set;
        }
        public string City
        {
            get; set;
        }
        public string State
        {
            get;
            set;
        }
        public string Country
        {
            get;
            set;
        }

        public int? Pin
        {
            get;
            set;
        }
        public Int64? ST_No
        {
            get;
            set;
        }

        public string Bank
        {
            get;
            set;

        }
        public string Branch
        {
            get; set;
        }
        public string Account_Type
        {
            get; set;
        }
        public string Acc_No
        {
            get;
            set;
        }
        public string IFSC
        {
            get;
            set;
        }
        public string Nature_Work
        {
            get;
            set;
        }
        public string Remark
        {
            get;
            set;
        }
        public string Bank2
        {
            get;
            set;

        }
        public string Branch2
        {
            get;
            set;
        }
        public string Account_Type2
        {
            get;
            set;
        }
        public string Acc_No2
        {
            get;
            set;
        }
        public string IFSC2
        {
            get;
            set;
        }
        public bool IsAsst
        {
            get;
            set;
        }
        public string OutputMsg
        {
            get;
            set;
        }

        public string Vendor_Name { get; set; }
        public string Address { get; set; }
        public string Bank_Name { get; set; }
        public string Account_Number { get; set; }
        public string Vendor_Branch { get; set; }
        public string IFC_code { get; set; }



        public string File_GSTRegistration { get; set; }
        public string File_PANCopy { get; set; }
        public string File_BankDetails { get; set; }
        #endregion

        #region "Class: Vendor Methods"

        /// <summary>
        /// Function Name:  fillCollectionFromDr.
        /// Called By:      Nill.
        /// Description:    Fill the data from data reader. 
        /// Change history: Nill.
        /// </summary>

        private bool fillCollectionFromDr(SqlDataReader dr, ref Dictionary<int, MailConfig> dicCountry)
        {
            if (dicCountry == null)
            {
                dicCountry = new Dictionary<int, MailConfig>();
            }

            try
            {
                // Loop though the data reader
                while (dr.Read())
                {
                    //clsCountry.CountryName
                    //if (dr["CountryName"] != DBNull.Value)
                    //{
                    //    this.CountryName = dr["CountryName"].ToString();
                    //}


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
                    //if (dr["CountryName"] != DBNull.Value)
                    //{
                    //    this.CountryName = dr["CountryName"].ToString();
                    //}



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
                    return "PROC_Vendor_INSERT";
                case eLoadSp.INSERT_SUB_VENDOR_DETAILS:
                    return "PROC_SUB_VENDOR_DETAILS_INSERT";
                case eLoadSp.SELECT_ALL_COUNTRY:
                    return "PROC_Tb_Country_SELECT_ALL";
                case eLoadSp.SELECT_ALL_State:
                    return "PROC_Tb_State_SELECT_ALL";
                case eLoadSp.SELECT_ALL_BANK:
                    return "PROC_Tb_Bank_SELECT_ALL";
                case eLoadSp.SELECT_VENDOR_ALL:
                    return "PRO_Vendor_SELECT_ALL";
                case eLoadSp.SELECT_ALL_VENDOR_NAMES:
                    return "PROC_SELECT_VENDORS";
                case eLoadSp.SELECT_ASSET_VENDOR_ALL:
                    return "PRO_Asset_Vendor_SELECT_ALL";
                case eLoadSp.SELECT_VENDORDETAILS_BY_ID:
                    return "PRO_SELECT_VendorDetails_BY_ID";
                case eLoadSp.UPDATE:
                    return "PROC_Vendor_UPDATE";
                case eLoadSp.DELETE:
                    return "PRO_DELETE_VendorDetails_BY_ID";
                case eLoadSp.SELECT_ALL_BANK_Detail:
                    return "PROC_Tb_Bank_SELECT_ALL_Detail";
                case eLoadSp.SELECT_BANK_LIST_PAYMENT_IND:
                    return "PROC_SELECT_BANK_LIST_PAYMENT_IND";
                case eLoadSp.SELECT_BANK_LIST:
                    return "PROC_SELECT_BANK_LIST";
                case eLoadSp.SUB_VENDOR_SELECT_ALL_DETAIL:
                    return "PROC_SUB_VENDOR_SELECT_ALL_Detail";
                case eLoadSp.DELETE_SUB_VENDOR_DETAIL:
                    return "PROC_DELETE_SUB_VENDOR_DETAIL";
                case eLoadSp.TB_BANK_SELECT_ALL_DETAIL_BY_ID:
                    return "PROC_Tb_Bank_SELECT_ALL_Detail_BY_ID";

                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Function Name:  getSpParamArray.
        /// Called By:      Nill.
        /// Description:    Get the parameter in enumSpName wise
        /// Change history: Nill.
        /// </summary>
        private SqlParameter[] getSpParamArray(eLoadSp enumSpName)
        {
            SqlParameter[] colParams = new SqlParameter[]
        {
        };

            switch (enumSpName)
            {

                case eLoadSp.SELECT_VENDORDETAILS_BY_ID:
                case eLoadSp.DELETE:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@VendorID", SqlDbType.VarChar, 16, ParameterDirection.Input, false, 0, 0, "Vendor_ID", DataRowVersion.Current, this.Vendor_ID ?? (Object)DBNull.Value),
                };
                    break;



                case eLoadSp.DELETE_SUB_VENDOR_DETAIL:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ID", SqlDbType.VarChar, 16, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, this.ID ),
                };
                    break;
                case eLoadSp.TB_BANK_SELECT_ALL_DETAIL_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@BankID", SqlDbType.VarChar, 16, ParameterDirection.Input, false, 0, 0, "BankID", DataRowVersion.Current, this.BankID ),
                };
                    break;
                case eLoadSp.SUB_VENDOR_SELECT_ALL_DETAIL:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Vendor_ID", SqlDbType.VarChar, 16, ParameterDirection.Input, false, 0, 0, "Vendor_ID", DataRowVersion.Current, this.Vendor_ID ),
                };
                    break;

            }

            return colParams;
        }
        /// <summary>
        /// Fuction Name:   Insert.
        /// Called By:      Nill.
        /// Description:    Check the Sql conncetion.
        /// Change histroy: Nill.
        /// </summary>
        /// <param name="SqlConn"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
        public bool insert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insert(SqlConn, null, enumSpName);
        }

        public bool insertsubvendor(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insertsubvendor(SqlConn, null, enumSpName);
        }
        /// <summary>
        /// Fuction Name:   Insert.
        /// Called By:      Nill.
        /// Description:    Check the Sql transaction.
        /// Change histroy: Nill.
        /// </summary>
        /// <param name="SqlTran"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
        public bool insert(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return insert(null, SqlTran, enumSpName);
        }
        /// <summary>
        /// Fuction Name:   Insert.
        /// Called By:      Nill.
        /// Description:    Inserted values to table
        /// Change histroy: Nill.
        /// </summary>
        /// <param name="SqlTran,SqlTran"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
        /// 
        private bool insert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Vendor_ID", SqlDbType.VarChar, 16, ParameterDirection.Output, false, 0, 0, "Vendor_ID", DataRowVersion.Current, this.Vendor_ID ?? (Object)DBNull.Value),
                new SqlParameter("@OutputResult", SqlDbType.VarChar, 100, ParameterDirection.Output, false, 0, 0, "OutputMsg", DataRowVersion.Current, this.OutputMsg ?? (Object)DBNull.Value),
                new SqlParameter("@Vendor_name", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Vendor_name", DataRowVersion.Current, this.Vendor_name ?? (Object)DBNull.Value),
                new SqlParameter("@Regs_No", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Regs_No", DataRowVersion.Current, this.Regs_No),
                new SqlParameter("@Use_PAN", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Use_PAN", DataRowVersion.Current, this.Use_PAN),
                new SqlParameter("@PAN_No", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "PAN_No", DataRowVersion.Current, this.PAN_NO),
                new SqlParameter("@ST_No", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 0, 0, "ST_No", DataRowVersion.Current, this.ST_No ?? (Object)DBNull.Value),
                new SqlParameter("@Con_Person", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Con_Person", DataRowVersion.Current, this.Con_Person),
                new SqlParameter("@Con_No", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Con_No", DataRowVersion.Current, this.Con_No),
                new SqlParameter("@Email_ID", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Email_ID", DataRowVersion.Current, this.Email_ID),
                new SqlParameter("@Add_Line", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Add_Line", DataRowVersion.Current, this.Add_Line),



                new SqlParameter("@Landmark", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Landmark", DataRowVersion.Current, this.Landmark),
                new SqlParameter("@City", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "City", DataRowVersion.Current, this.City),
                new SqlParameter("@State", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "State", DataRowVersion.Current, this.State),
                new SqlParameter("@Country", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Country", DataRowVersion.Current, this.Country),
                new SqlParameter("@Pin", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Pin", DataRowVersion.Current, this.Pin ?? (Object)DBNull.Value),
                new SqlParameter("@Bank", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Bank", DataRowVersion.Current, this.Bank),
                new SqlParameter("@Branch", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Branch", DataRowVersion.Current, this.Branch),
                new SqlParameter("@Account_Type", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Account_Type", DataRowVersion.Current, this.Account_Type),
                new SqlParameter("@Acc_No", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Acc_No", DataRowVersion.Current, this.Acc_No),
                new SqlParameter("@IFSC", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "IFSC", DataRowVersion.Current, this.IFSC),
                new SqlParameter("@Chkref", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "ChkRef", DataRowVersion.Current, this.ChkRef),
                new SqlParameter("@RefPerson", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "RefPerson", DataRowVersion.Current, this.RefPersonName),
                new SqlParameter("@Bank2", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Bank2", DataRowVersion.Current, this.Bank2 ?? (Object)DBNull.Value),
                new SqlParameter("@Branch2", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Branch2", DataRowVersion.Current, this.Branch2 ?? (Object)DBNull.Value),
                new SqlParameter("@Account_Type2", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Account_Type2", DataRowVersion.Current, this.Account_Type2 ?? (Object)DBNull.Value),
                new SqlParameter("@Acc_No2", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Acc_No2", DataRowVersion.Current, this.Acc_No2 ?? (Object)DBNull.Value),
                new SqlParameter("@IFSC2", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "IFSC2", DataRowVersion.Current, this.IFSC2 ?? (Object)DBNull.Value),

                new SqlParameter("@Nature_Work", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Nature_Work", DataRowVersion.Current, this.Nature_Work),
                new SqlParameter("@Remark", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "Remark", DataRowVersion.Current, this.Remark),
                new SqlParameter("@IsAsst", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "IsAsst", DataRowVersion.Current, this.IsAsst),
                new SqlParameter("@PFRegistrationNo", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "PFRegistration_No", DataRowVersion.Current, this.PFRegistartionNo?? (Object)DBNull.Value),
                new SqlParameter("@LabourLicenseNo", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "LabourLicence_No", DataRowVersion.Current, this.LabourLicenseNo?? (Object)DBNull.Value),

                new SqlParameter("@File_GSTRegistration", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "File_GSTRegistration", DataRowVersion.Current, this.File_GSTRegistration ?? (Object)DBNull.Value),
                new SqlParameter("@File_PANCopy", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "File_PANCopy", DataRowVersion.Current, this.File_PANCopy ?? (Object)DBNull.Value),
                new SqlParameter("@File_BankDetails", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "File_BankDetails", DataRowVersion.Current, this.File_BankDetails ?? (Object)DBNull.Value),

                new SqlParameter("@Legal_Name", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Legal_Name", DataRowVersion.Current, this.Legal_Name ?? (Object)DBNull.Value)
            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                    if (intIdentityValue > 0)
                    {
                        this.Vendor_ID = (string)colParams.First().Value;
                    }
                    else
                    {
                        this.OutputMsg = (string)colParams[1].Value.ToString();
                    }

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

        private bool insertsubvendor(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                 new SqlParameter("@Vendor_Name", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Vendor_Name", DataRowVersion.Current, this.Vendor_Name ?? (Object)DBNull.Value),
                new SqlParameter("@Address", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Address", DataRowVersion.Current, this.Address ?? (Object)DBNull.Value),
                new SqlParameter("@Bank_Name", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Bank_Name", DataRowVersion.Current, this.Bank_Name ?? (Object)DBNull.Value),
                new SqlParameter("@Account_Number", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Account_Number", DataRowVersion.Current, this.Account_Number),
                new SqlParameter("@Branch", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Branch", DataRowVersion.Current, this.Branch),
                new SqlParameter("@IFC_code", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "IFC_code", DataRowVersion.Current, this.IFC_code),
                new SqlParameter("@Vendor_ID", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Vendor_ID", DataRowVersion.Current, this.Vendor_ID)
            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
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

        /// <summary>
        /// Fuction Name:   load.
        /// Called by:      Nill.
        /// Description:    Load the values from data reader.
        /// Called By:      Nill.
        /// Changes history:Nill.
        /// </summary>
        /// <param name="SqlConn,"></param>
        /// <param name="enumSpName"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        public bool load(SqlConnection SqlConn, eLoadSp enumSpName, ref SqlDataReader dr)
        {
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

                return true;
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
        /// <summary>
        /// Fuction Name:   load.
        /// Called by:      Nill.
        /// Description:    Load the values from enumSpName parameter.
        /// Called By:      Nill.
        /// Changes history:Nill.
        /// </summary>
        /// <param name="SqlConn"></param>
        /// <param name="enumSpName"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        public bool load(SqlConnection SqlConn, eLoadSp enumSpName, ref Dictionary<int, MailConfig> diclCountry)
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
        /// <summary>
        /// Fuction Name:   load.
        /// Called by:      Nill.
        /// Description:    Load the values without parameter.
        /// Called By:      Nill.
        /// Changes history:Nill.
        /// </summary>
        /// <param name="SqlConn"></param>
        /// <param name="enumSpName"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Fuction Name: delete.
        /// Called By:  Nill.
        /// Description: it is used to delete the data from table.
        /// Changes history: Nill.
        /// </summary>
        /// <param name="SqlConn"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Fuction Name: update.
        /// Called By:  Nill.
        /// Description: check the sql connection.
        /// Changes history: Nill.
        /// </summary>
        /// <param name="SqlConn"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
        public bool update(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return update(SqlConn, null, enumSpName);
        }
        /// <summary>
        /// Fuction Name: update.
        /// Called By:  Nill.
        /// Description: check the sql transaction.
        /// Changes history: Nill.
        /// </summary>
        /// <param name="SqlTran"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
        public bool update(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return update(null, SqlTran, enumSpName);
        }
        /// <summary>
        /// Fuction Name: update.
        /// Called By:  Nill.
        /// Description: Updated the values from table.
        /// Changes history: Nill.
        /// </summary>
        /// <param name="SqlTran,SqlConn"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
        private bool update(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@OutputResult", SqlDbType.VarChar, 100, ParameterDirection.Output, false, 0, 0, "OutputMsg", DataRowVersion.Current, this.OutputMsg ?? (Object)DBNull.Value),
                new SqlParameter("@Vendor_ID", SqlDbType.VarChar, 16, ParameterDirection.Input, false, 0, 0, "Vendor_ID", DataRowVersion.Current, this.Vendor_ID ?? (Object)DBNull.Value),
                new SqlParameter("@Vendor_name", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "@Vendor_name", DataRowVersion.Current, this.Vendor_name ?? (Object)DBNull.Value),
                new SqlParameter("@Regs_No", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Regs_No", DataRowVersion.Current, this.Regs_No),
                new SqlParameter("@Use_PAN", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Use_PAN", DataRowVersion.Current, this.Use_PAN),
                new SqlParameter("@PAN_No", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "PAN_No", DataRowVersion.Current, this.PAN_NO),
                new SqlParameter("@ST_No ", SqlDbType.BigInt,8, ParameterDirection.Input, false, 0, 0, "ST_No", DataRowVersion.Current, this.ST_No ?? (Object)DBNull.Value),
                new SqlParameter("@Con_Person", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Con_Person", DataRowVersion.Current, this.Con_Person),
                new SqlParameter("@Con_No", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Con_No", DataRowVersion.Current, this.Con_No),
                new SqlParameter("@Email_ID", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Email_ID", DataRowVersion.Current, this.Email_ID),
                new SqlParameter("@Add_Line", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Add_Line", DataRowVersion.Current, this.Add_Line),
                new SqlParameter("@Landmark", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Landmark", DataRowVersion.Current, this.Landmark),
                new SqlParameter("@City", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "City", DataRowVersion.Current, this.City),
                new SqlParameter("@State", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "State", DataRowVersion.Current, this.State),
                 new SqlParameter("@PFRegistrationNo", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "PFRegistration_No", DataRowVersion.Current, this.PFRegistartionNo?? (Object)DBNull.Value),
                 new SqlParameter("@LabourLicenseNo", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "LabourLicence_No", DataRowVersion.Current, this.LabourLicenseNo?? (Object)DBNull.Value),
                new SqlParameter("@Country", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Country", DataRowVersion.Current, this.Country),
                new SqlParameter("@Pin", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "Pin", DataRowVersion.Current, this.Pin ?? (Object)DBNull.Value),
                new SqlParameter("@Bank", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Bank", DataRowVersion.Current, this.Bank),
                new SqlParameter("@Branch", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Branch", DataRowVersion.Current, this.Branch),
                new SqlParameter("@Account_Type", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Account_Type", DataRowVersion.Current, this.Account_Type),
                new SqlParameter("@Acc_No", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Acc_No", DataRowVersion.Current, this.Acc_No),
                new SqlParameter("@IFSC", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "IFSC", DataRowVersion.Current, this.IFSC),
                new SqlParameter("@Chkref", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "ChkRef", DataRowVersion.Current, this.ChkRef),
                new SqlParameter("@RefPerson", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "RefPerson", DataRowVersion.Current, this.RefPersonName),

                new SqlParameter("@Bank2", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Bank2", DataRowVersion.Current, this.Bank2 ?? (Object)DBNull.Value),
                new SqlParameter("@Branch2", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Branch2", DataRowVersion.Current, this.Branch2 ?? (Object)DBNull.Value),
                new SqlParameter("@Account_Type2", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Account_Type2", DataRowVersion.Current, this.Account_Type2 ?? (Object)DBNull.Value),
                new SqlParameter("@Acc_No2", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Acc_No2", DataRowVersion.Current, this.Acc_No2 ?? (Object)DBNull.Value),
                new SqlParameter("@IFSC2", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "IFSC2", DataRowVersion.Current, this.IFSC2 ?? (Object)DBNull.Value),
                new SqlParameter("@IsAsst", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "IsAsst", DataRowVersion.Current, this.IsAsst),
                new SqlParameter("@Nature_Work", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Nature_Work", DataRowVersion.Current, this.Nature_Work),
                new SqlParameter("@Remark", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Remark", DataRowVersion.Current, this.Remark),

                new SqlParameter("@File_GSTRegistration", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "File_GSTRegistration", DataRowVersion.Current, this.File_GSTRegistration ?? (Object)DBNull.Value),
                new SqlParameter("@File_PANCopy", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "File_PANCopy", DataRowVersion.Current, this.File_PANCopy ?? (Object)DBNull.Value),
                new SqlParameter("@File_BankDetails", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "File_BankDetails", DataRowVersion.Current, this.File_BankDetails ?? (Object)DBNull.Value),

                new SqlParameter("@Legal_Name", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Legal_Name", DataRowVersion.Current, this.Legal_Name ?? (Object)DBNull.Value)
            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                    if (intReturnVal < 0)
                    {
                        this.OutputMsg = (string)colParams.First().Value;
                    }
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


        public VendorBL()
        {

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
