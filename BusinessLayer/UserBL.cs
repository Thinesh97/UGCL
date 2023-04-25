using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using DataAccess;
using System.Data;

namespace BusinessLayer
{
    public class UserBL
    {
        #region "Class: VendorANDSubCon Local Declarations"

        public enum eLoadSp
        {

            InsertDepartment = 0,
            InsertDesignation = 1,
            RetriveDesignation = 2,
            RetriveDepartment = 3,
            SELECT_PROJECT_ALL = 4,
            DEPARTMENT_CHECKDUPLICATE = 5,
            update = 6,
            DESIGNATION_CHECKDUPLICATE = 7,
            INSERT_ALLUSERS = 8,
            SELECT_USERS_DETAILS_BY_ID = 9,
            DELETE_DEPT = 10,
            DELETE_DESIGNATION = 11,
            UPDATE_USERByID = 12,
            FORGOTPASSWORD_GETNAME = 13,
            FORGOTPASSWORD_GET_PASSWORD = 14,
            CHANGEPASSWORD = 15,
            GET_USER_NAME = 16,
            CHECK_MAIL = 17,
            RETRIVE_MODULEACCESS_ALL = 18,
            UPDATE_NEW_PASSWORD

        };
        #endregion
        #region "Class: User Sets / Gets"

        public string DigitalSign_FileName
        {
            get;
            set;
        }
        public string Image_FilePath
        {
            get;
            set;
        }
        public string Department
        {
            get;
            set;
        }
        public string Designation
        {
            get;
            set;
        }
        public string Project_Code
        {
            get;
            set;
        }
        public string Project_Name
        {
            get;
            set;
        }
        public string Task
        {
            get;
            set;
        }
        public string Content
        {
            get;
            set;
        }
        public string ContentDesig
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string UserID
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public string Email_ID
        {
            get;
            set;
        }
        public string Employee_ID
        {
            get;
            set;
        }
        public string Status
        {
            get;
            set;
        }
        public string Role
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string NewPassword
        {
            get;
            set;
        }
        public string id
        {
            get;
            set;
        }

        public bool UserCreate
        {
            get;
            set;
        }

        public bool UserView
        {
            get;
            set;
        }

        public bool UserUpdate
        {
            get;
            set;
        }

        public bool UserDelete
        {
            get;
            set;
        }

        public bool EmpContractCreate { get; set; }

        public bool EmpContractView { get; set; }

        public bool EmpContractUpdate { get; set; }

        public bool EmpContractDelete { get; set; }

        public bool IsHoUser
        {
            get;
            set;
        }
        public bool Is_CFO_User
        {
            get;
            set;
        }
        public bool Appr_PO_Delete
        {
            get;
            set;
        }

        public bool Mail_update
        {
            get;
            set;
        }

        public bool CompySite_Create
        {
            get;
            set;
        }

        public bool CompySite_View
        {
            get;
            set;
        }

        public bool CompySite_Update
        {
            get;
            set;
        }


        public bool CompSite_Delete
        {
            get;
            set;
        }


        public bool Proj_Create
        {
            get;
            set;
        }


        public bool Proj_View
        {
            get;
            set;
        }

        public bool Proj_Update
        {
            get;
            set;
        }

        public bool Proj_Delete
        {
            get;
            set;
        }

        public bool UOM_Create
        {
            get;
            set;
        }

        public bool UOM_View
        {
            get;
            set;
        }

        public bool UOM_Update
        {
            get;
            set;
        }

        public bool UOM_Delete
        {
            get;
            set;
        }

        public bool Material_Create
        {
            get;
            set;
        }

        public bool Material_View
        {
            get;
            set;
        }

        public bool Material_Update
        {
            get;
            set;
        }

        public bool Material_Delete
        {
            get;
            set;
        }

        public bool Vendor_Create
        {
            get;
            set;
        }


        public bool Vendor_View
        {
            get;
            set;
        }

        public bool Vendor_Update
        {
            get;
            set;
        }

        public bool Vendor_Delete
        {
            get;
            set;
        }


        public bool Sub_Cont_Create
        {
            get;
            set;
        }

        public bool Sub_Cont_View
        {
            get;
            set;
        }


        public bool Sub_Cont_Update
        {
            get;
            set;
        }

        public bool Sub_Cont_Delete
        {
            get;
            set;
        }

        public bool OtherCreate { get; set; }

        public bool OtherView { get; set; }
        
        public bool OtherUpdate { get; set; }
        
        public bool OtherDelete { get; set; }

        public bool Bug_Create
        {
            get;
            set;
        }
        public bool Bug_View
        {
            get;
            set;
        }
        public bool Bug_Update
        {
            get;
            set;
        }
        public bool Bug_Delete
        {
            get;
            set;
        }


        public bool Pro_Bug_Create
        {
            get;
            set;
        }
        public bool Pro_Bug_View
        {
            get;
            set;
        }
        public bool Pro_Bug_Update
        {
            get;
            set;
        }
        public bool Pro_Bug_Delete
        {
            get;
            set;
        }




        public bool Bug_Mod_Create
        {
            get;
            set;
        }
        public bool Bug_Mod_View
        {
            get;
            set;
        }
        public bool Bug_Mod_Update
        {
            get;
            set;
        }
        public bool Bug_Mod_Delete
        {
            get;
            set;
        }

        public bool Bug_Reports
        {
            get;
            set;
        }

        public bool Stock_Create
        {
            get;
            set;
        }

        public bool Stock_View
        {
            get;
            set;
        }


        public bool Stock_Update
        {
            get;
            set;
        }

        public bool Stock_Delete
        {
            get;
            set;
        }
        public bool Stock_TransferCreate
        {
            get;
            set;
        }
        public bool Stock_TransferView
        {
            get;
            set;
        }
        public bool Stock_TransferUpdate
        {
            get;
            set;
        }
        public bool Stock_TransferDelete
        {
            get;
            set;
        }

        public bool Quotn_Create
        {
            get;
            set;
        }

        public bool Quotn_View
        {
            get;
            set;
        }

        public bool Quotn_Update
        {
            get;
            set;
        }

        public bool Quotn_Delete
        {
            get;
            set;
        }
        public bool Quotn_Compare
        {
            get;
            set;
        }

        public bool Ind_Create
        {
            get;
            set;
        }

        public bool Ind_View
        {
            get;
            set;
        }

        public bool Ind_Update
        {
            get;
            set;
        }

        public bool Ind_Delete
        {
            get;
            set;
        }

        public bool PO_Create
        {
            get;
            set;
        }

        public bool PO_View
        {
            get;
            set;
        }

        public bool PO_Update
        {
            get;
            set;
        }

        public bool PO_Delete
        {
            get;
            set;
        }

        public bool PayInd_Create { get; set; }
        public bool PayInd_View { get; set; }
        public bool PayInd_Update { get; set; }
        public bool PayInd_Delete { get; set; }
        
        public bool PayInd_Ver_Create { get; set; }
        public bool PayInd_Ver_View { get; set; }
        public bool PayInd_Ver_Update { get; set; }
        public bool PayInd_Ver_Delete { get; set; }
            
        public bool PayInd_App_Create { get; set; }
        public bool PayInd_App_View { get; set; }
        public bool PayInd_App_Update { get; set; }
        public bool PayInd_App_Delete { get; set; }

        public bool Proc_Reports
        {
            get;
            set;
        }

        public bool MRN_Create
        {
            get;
            set;
        }

        public bool MRN_View
        {
            get;
            set;
        }

        public bool MRN_Update
        {
            get;
            set;
        }

        public bool MRN_Delete
        {
            get;
            set;
        }

        public bool MIN_Create
        {
            get;
            set;
        }


        public bool MIN_View
        {
            get;
            set;
        }


        public bool MIN_Update
        {
            get;
            set;
        }


        public bool MIN_Delete
        {
            get;
            set;
        }

        public bool Inv_Reports
        {
            get;
            set;
        }


        public bool AssReg_Create
        {
            get;
            set;
        }


        public bool AssReg_View
        {
            get;
            set;
        }


        public bool AssReg_Update
        {
            get;
            set;
        }

        public bool AssReg_Delete
        {
            get;
            set;
        }

        public bool AssTra_Create
        {
            get;
            set;
        }

        public bool AssTra_View
        {
            get;
            set;
        }

        public bool AssTra_Update
        {
            get;
            set;
        }

        public bool AssTra_Delete
        {
            get;
            set;
        }

        public bool DailRun_Create
        {
            get;
            set;
        }

        public bool DailRun_View
        {
            get;
            set;
        }

        public bool DailRun_Update
        {
            get;
            set;
        }

        public bool DailRun_Delete
        {
            get;
            set;
        }

        public bool Ass_Rep
        {
            get;
            set;
        }

        public bool  Local_MRN_Create{ get; set; }
        public bool  Local_MRN_Update{ get; set; }
        public bool  LocalMRN_View{ get; set; }
        public bool Local_MRN_Delete { get; set; }



        //public string Department
        //{
        //    get;
        //    set;
        //}



        #endregion

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


                }
                return true;
            }
            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }
        private bool fillObjectFromDr(SqlDataReader dr)
        {
            try
            {
                // Loop though the data reader
                while (dr.Read())
                {



                }
                return true;
            }
            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }
        private SqlParameter[] getSpParamArray(eLoadSp enumSpName)
        {
            SqlParameter[] colParams = new SqlParameter[]
		{
		};

            switch (enumSpName)
            {


                case eLoadSp.DEPARTMENT_CHECKDUPLICATE:
                    colParams = new SqlParameter[]
                {
                   new SqlParameter("@Department", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Department", DataRowVersion.Current, this.Department), 
                    new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task), 
						
                };
                    break;
                case eLoadSp.DESIGNATION_CHECKDUPLICATE:
                    colParams = new SqlParameter[]
                {
                   new SqlParameter("@Designation", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Designation", DataRowVersion.Current, this.Designation), 
					 new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task), 
                };
                    break;
                case eLoadSp.SELECT_USERS_DETAILS_BY_ID:
                    colParams = new SqlParameter[]
                {
                   new SqlParameter("@EmployeeID", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Employee_ID", DataRowVersion.Current, this.Employee_ID), 
					
                };
                    break;
                case eLoadSp.DELETE_DEPT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Department", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Department", DataRowVersion.Current, this.Department), 
                    new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task), 
					
                };
                    break;
                case eLoadSp.DELETE_DESIGNATION:
                    colParams = new SqlParameter[]
                {
                     new SqlParameter("@Designation", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Designation", DataRowVersion.Current, this.Designation), 
					 new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task), 
					
                };
                    break;
                case eLoadSp.FORGOTPASSWORD_GETNAME:
                    colParams = new SqlParameter[]
                {
                     new SqlParameter("@username", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "UserID", DataRowVersion.Current, this.UserID), 
                      new SqlParameter("@Emailid", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Email_ID", DataRowVersion.Current, this.Email_ID), 
					 new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task), 
					
                };
                    break;
                case eLoadSp.FORGOTPASSWORD_GET_PASSWORD:
                    colParams = new SqlParameter[]
                {
                     new SqlParameter("@username", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "UserID", DataRowVersion.Current, this.UserID), 
                      new SqlParameter("@Emailid", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Email_ID", DataRowVersion.Current, this.Email_ID), 
                      // new SqlParameter("@password", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Password", DataRowVersion.Current, this.Password), 
					 new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task), 
					
                };

                    break;
                case eLoadSp.GET_USER_NAME:
                    colParams = new SqlParameter[]
                {
                     new SqlParameter("@username", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "UserID", DataRowVersion.Current, this.UserID), 
                     
					 new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task), 
					
                };
                    break;
                case eLoadSp.CHECK_MAIL:
                    colParams = new SqlParameter[]
                {
                  
					 new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task), 
					
                };
                    break;
                case eLoadSp.RETRIVE_MODULEACCESS_ALL:
                    colParams = new SqlParameter[]
                {
                  
					 new SqlParameter("@Department", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Department", DataRowVersion.Current, this.Department), 
					
                };
                    break;


            }

            return colParams;
        }


        public bool insertDesignation(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insertDesignation(SqlConn, null, enumSpName);
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
        public bool insertDesignation(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return insertDesignation(null, SqlTran, enumSpName);
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
        private bool insertDesignation(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 1;

                SqlParameter[] colParams = new SqlParameter[]
			{
				new SqlParameter("@Designation", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Designation", DataRowVersion.Current, this.Designation),     
          			new SqlParameter("@Task", SqlDbType.VarChar, 50,  ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task)			
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
        private bool insert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
				new SqlParameter("@Department", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Department", DataRowVersion.Current, this.Department), 
              	new SqlParameter("@Task", SqlDbType.VarChar, 50,  ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task)			
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
        public bool deleteDesig(SqlConnection objConn, eLoadSp enmSPName)
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





        public bool updateNewPassword(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updateNewPassword(SqlConn, null, enumSpName);
        }





        private bool updateNewPassword(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
                
				 new SqlParameter("@username", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "UserID", DataRowVersion.Current, this.UserID), 
                     new SqlParameter("@Emailid", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Email_ID", DataRowVersion.Current, this.Email_ID),
                     new SqlParameter("@password", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Password", DataRowVersion.Current, this.Password),
                       new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task)
				
			};




                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
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
                
				new SqlParameter("@Department", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Department", DataRowVersion.Current, this.Department),
               	new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current ,this.Task),		
                new SqlParameter("@Content", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Content", DataRowVersion.Current, this.Content),
				
			};




                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
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

        /// <summary>
        /// Fuction Name: update.
        /// Called By:  Nill.
        /// Description: check the sql connection.
        /// Changes history: Nill.
        /// </summary>
        /// <param name="SqlConn"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
        public bool Update_UserDetailsById(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Update_UserDetailsById(SqlConn, null, enumSpName);
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
        public bool Update_UserDetailsById(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return Update_UserDetailsById(null, SqlTran, enumSpName);
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
        private bool Update_UserDetailsById(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 12;

                SqlParameter[] colParams = new SqlParameter[]
			{
                new SqlParameter("@Local_MRN_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Local_MRN_Create", DataRowVersion.Current, this.Local_MRN_Create),
	            new SqlParameter("@Local_MRN_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Local_MRN_Update", DataRowVersion.Current, this.Local_MRN_Update),
                new SqlParameter("@LocalMRN_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "LocalMRN_View", DataRowVersion.Current, this.LocalMRN_View),
                new SqlParameter("@Local_MRN_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Local_MRN_Delete", DataRowVersion.Current, this.Local_MRN_Delete),
			    new SqlParameter("@Appr_PO_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Appr_PO_Delete", DataRowVersion.Current, this.Appr_PO_Delete)
,
			    new SqlParameter("@Name", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, this.Name), 
                new SqlParameter("@UserID", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "UserID", DataRowVersion.Current, this.UserID), 
                new SqlParameter("@Password ", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "Password", DataRowVersion.Current, this.Password),
                new SqlParameter("@Email_ID", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Email_ID", DataRowVersion.Current, this.Email_ID),
                new SqlParameter("@Employee_ID ", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Employee_ID", DataRowVersion.Current, this.Employee_ID),
                new SqlParameter("@Project_Code ", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code ?? (object)DBNull.Value),
                new SqlParameter("@Status ", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Status", DataRowVersion.Current, this.Status ?? (object)DBNull.Value),
                new SqlParameter("@Role", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Role", DataRowVersion.Current, this.Role ?? (object)DBNull.Value),
				new SqlParameter("@Designation", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Designation", DataRowVersion.Current, this.Designation ?? (object)DBNull.Value),    
                new SqlParameter("@Department", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Department", DataRowVersion.Current, this.Department ?? (object)DBNull.Value), 
                new SqlParameter("@IsHoUser", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "IsHoUser", DataRowVersion.Current, this.IsHoUser),  
                new SqlParameter("@DigitalSign_FileName", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "DigitalSign_FileName", DataRowVersion.Current, this.DigitalSign_FileName), 

                new SqlParameter("@Image_FilePath", SqlDbType.NVarChar, 4000, ParameterDirection.Input, false, 0, 0, "Image_FilePath", DataRowVersion.Current, this.Image_FilePath), 

                // Page Level Access For Every User
                new SqlParameter("@UserCreate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "UserCreate", DataRowVersion.Current, this.UserCreate),             
                new SqlParameter("@UserView", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "UserView", DataRowVersion.Current, this.UserView),
                new SqlParameter("@UserUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "UserUpdate", DataRowVersion.Current, this.UserUpdate),
                new SqlParameter("@UserDelete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "UserDelete", DataRowVersion.Current, this.UserDelete),
                new SqlParameter("@EmpContractCreate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "EmpContractCreate", DataRowVersion.Current, this.EmpContractCreate),             
                new SqlParameter("@EmpContractView", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "EmpContractView", DataRowVersion.Current, this.EmpContractView),
                new SqlParameter("@EmpContractUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "EmpContractUpdate", DataRowVersion.Current, this.EmpContractUpdate),
                new SqlParameter("@EmpContractDelete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "EmpContractDelete", DataRowVersion.Current, this.EmpContractDelete),
                new SqlParameter("@Mail_update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Mail_update", DataRowVersion.Current, this.Mail_update),
                new SqlParameter("@CompySite_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "CompySite_Creat", DataRowVersion.Current, this.CompySite_Create),
                new SqlParameter("@CompySite_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "CompySite_View", DataRowVersion.Current, this.CompySite_View),
                    new SqlParameter("@CompySite_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "CompySite_Update", DataRowVersion.Current, this.CompySite_Update),
                      new SqlParameter("@CompSite_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "CompSite_Delete", DataRowVersion.Current, this.CompSite_Delete),
                        new SqlParameter("@Proj_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Proj_Create", DataRowVersion.Current, this.Proj_Create),
                        new SqlParameter("@Proj_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Proj_View", DataRowVersion.Current, this.Proj_View),
                         new SqlParameter("@Proj_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Proj_Update", DataRowVersion.Current, this.Proj_Update),
                           new SqlParameter("@Proj_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Proj_Delete", DataRowVersion.Current, this.Proj_Delete),
                             new SqlParameter("@UOM_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "UOM_Create", DataRowVersion.Current, this.UOM_Create),
                                 new SqlParameter("@UOM_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "UOM_View", DataRowVersion.Current, this.@UOM_View),
                              new SqlParameter("@UOM_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "UOM_Update", DataRowVersion.Current, this.UOM_Update),
                              new SqlParameter("@UOM_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "UOM_Delete", DataRowVersion.Current, this.UOM_Delete),
                               new SqlParameter("@Material_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Material_Create", DataRowVersion.Current, this.Material_Create),
                                new SqlParameter("@Material_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Material_View", DataRowVersion.Current, this.Material_View),
                                 new SqlParameter("@Material_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Material_Update", DataRowVersion.Current, this.Material_Update),
                                  new SqlParameter("@Material_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Material_Delete", DataRowVersion.Current, this.Material_Delete),
                                    new SqlParameter("@Vendor_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Vendor_Create", DataRowVersion.Current, this.Vendor_Create),
                                    new SqlParameter("@Vendor_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Vendor_View", DataRowVersion.Current, this.Vendor_View),
                                    new SqlParameter("@Vendor_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Vendor_Update", DataRowVersion.Current, this.Vendor_Update),
                                    new SqlParameter("@Vendor_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Vendor_Delete", DataRowVersion.Current, this.Vendor_Delete),
                                     new SqlParameter("@Sub_Cont_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Sub_Cont_Create", DataRowVersion.Current, this.Sub_Cont_Create),
                                      new SqlParameter("@Sub_Cont_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Sub_Cont_View", DataRowVersion.Current, this.Sub_Cont_View),
                                       new SqlParameter("@Sub_Cont_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Sub_Cont_Update", DataRowVersion.Current, this.Sub_Cont_Update),
                                        new SqlParameter("@Sub_Cont_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Sub_Cont_Delete", DataRowVersion.Current, this.Sub_Cont_Delete),
                new SqlParameter("@OtherCreate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "OtherCreate", DataRowVersion.Current, this.OtherCreate),             
                new SqlParameter("@OtherView", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "OtherView", DataRowVersion.Current, this.OtherView),
                new SqlParameter("@OtherUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "OtherUpdate", DataRowVersion.Current, this.OtherUpdate),
                new SqlParameter("@OtherDelete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "OtherDelete", DataRowVersion.Current, this.OtherDelete),  
                  
                                        new SqlParameter("@Bug_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Create", DataRowVersion.Current, this.Bug_Create),
                                           new SqlParameter("@Bug_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_View", DataRowVersion.Current, this.Bug_View),

                                            new SqlParameter("@Bug_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Update", DataRowVersion.Current, this.Bug_Update),
                                            new SqlParameter("@Bug_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Delete", DataRowVersion.Current, this.Bug_Delete),


                                             new SqlParameter("@Pro_BudgetCreate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Create", DataRowVersion.Current, this.Pro_Bug_Create),
                                           new SqlParameter("@Pro_BudgetView", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_View", DataRowVersion.Current, this.Pro_Bug_View),

                                            new SqlParameter("@Pro_BudgetUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Update", DataRowVersion.Current, this.Pro_Bug_Update),
                                            new SqlParameter("@Pro_BudgetDelete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Delete", DataRowVersion.Current, this.Pro_Bug_Delete),


                                            new SqlParameter("@Bug_Mod_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Mod_Create", DataRowVersion.Current, this.Bug_Mod_Create),
                                            new SqlParameter("@Bug_Mod_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Mod_View", DataRowVersion.Current, this.Bug_Mod_View),
                                            new SqlParameter("@Bug_Mod_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Mod_Update", DataRowVersion.Current, this.Bug_Mod_Update),
                                            new SqlParameter("@Bug_Mod_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Mod_Delete", DataRowVersion.Current, this.Bug_Mod_Delete),
                                            new SqlParameter("@Bug_Reports", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Reports", DataRowVersion.Current, this.Bug_Reports),
                                            new SqlParameter("@Stock_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Stock_Create", DataRowVersion.Current, this.Stock_Create),
                                            new SqlParameter("@Stock_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Stock_View", DataRowVersion.Current, this.Stock_View),
                                             new SqlParameter("@Stock_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Stock_Update", DataRowVersion.Current, this.Stock_Update),
                                              new SqlParameter("@Stock_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Stock_Delete", DataRowVersion.Current, this.Stock_Delete),
                                               new SqlParameter("@Stock_TransferCreate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Stock_TransferCreate", DataRowVersion.Current, this.Stock_TransferCreate),
                                              new SqlParameter("@Stock_TransferView", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Stock_TransferView", DataRowVersion.Current, this.Stock_TransferView),
                                             new SqlParameter("@Stock_TransferUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Stock_TransferUpdate", DataRowVersion.Current, this.Stock_TransferUpdate),
                                              new SqlParameter("@Stock_TransferDelete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Stock_TransferDelete", DataRowVersion.Current, this.Stock_TransferDelete),

                                               new SqlParameter("@Quotn_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Quotn_Create", DataRowVersion.Current, this.Quotn_Create),
                                                new SqlParameter("@Quotn_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Quotn_View", DataRowVersion.Current, this.Quotn_View),
                                                new SqlParameter("@Quotn_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Quotn_Update", DataRowVersion.Current, this.Quotn_Update),
                                                new SqlParameter("@Quotn_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Quotn_Delete", DataRowVersion.Current, this.Quotn_Delete),
                                                new SqlParameter("@Quotn_Compare", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Quotn_Compare", DataRowVersion.Current, this.Quotn_Compare),
                                                new SqlParameter("@Ind_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Ind_Create", DataRowVersion.Current, this.Ind_Create),
                                                 new SqlParameter("@Ind_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Ind_View", DataRowVersion.Current, this.Ind_View),
                                                  new SqlParameter("@Ind_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Ind_Update", DataRowVersion.Current, this.Ind_Update),
                                                   new SqlParameter("@Ind_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Ind_Delete", DataRowVersion.Current, this.Ind_Delete),
                                                   new SqlParameter("@PO_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PO_Create", DataRowVersion.Current, this.PO_Create),
                                                   new SqlParameter("@PO_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PO_View", DataRowVersion.Current, this.PO_View),
                                                    new SqlParameter("@PO_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PO_Update", DataRowVersion.Current, this.PO_Update),
                                                    new SqlParameter("@PO_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PO_Delete", DataRowVersion.Current, this.PO_Delete),
                new SqlParameter("@PayInd_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_Create", DataRowVersion.Current, this.PayInd_Create),             
                new SqlParameter("@PayInd_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_View", DataRowVersion.Current, this.PayInd_View),
                new SqlParameter("@PayInd_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_Update", DataRowVersion.Current, this.PayInd_Update),
                new SqlParameter("@PayInd_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_Delete", DataRowVersion.Current, this.PayInd_Delete),  
                new SqlParameter("@PayInd_Ver_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_Ver_Create", DataRowVersion.Current, this.PayInd_Ver_Create),             
                new SqlParameter("@PayInd_Ver_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_Ver_View", DataRowVersion.Current, this.PayInd_Ver_View),
                new SqlParameter("@PayInd_Ver_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_Ver_Update", DataRowVersion.Current, this.PayInd_Ver_Update),
                new SqlParameter("@PayInd_Ver_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_Ver_Delete", DataRowVersion.Current, this.PayInd_Ver_Delete),  
                new SqlParameter("@PayInd_App_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_App_Create", DataRowVersion.Current, this.PayInd_App_Create),             
                new SqlParameter("@PayInd_App_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_App_View", DataRowVersion.Current, this.PayInd_App_View),
                new SqlParameter("@PayInd_App_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_App_Update", DataRowVersion.Current, this.PayInd_App_Update),
                new SqlParameter("@PayInd_App_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_App_Delete", DataRowVersion.Current, this.PayInd_App_Delete),  
                        
                new SqlParameter("@Proc_Reports", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Proc_Reports", DataRowVersion.Current, this.Proc_Reports),
                           
                new SqlParameter("@MRN_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "MRN_Create", DataRowVersion.Current, this.MRN_Create),
                new SqlParameter("@MRN_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "MRN_View", DataRowVersion.Current, this.MRN_View),
                new SqlParameter("@MRN_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "MRN_Update", DataRowVersion.Current, this.MRN_Update),
                new SqlParameter("@MRN_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "MRN_Delete", DataRowVersion.Current, this.MRN_Delete),
                new SqlParameter("@MIN_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "MIN_Create", DataRowVersion.Current, this.MIN_Create),
                new SqlParameter("@MIN_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "MIN_View", DataRowVersion.Current, this.MIN_View),
                new SqlParameter("@MIN_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "MIN_Update", DataRowVersion.Current, this.MIN_Update),
                new SqlParameter("@MIN_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "MIN_Delete", DataRowVersion.Current, this.MIN_Delete),
                new SqlParameter("@Inv_Reports", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Inv_Reports", DataRowVersion.Current, this.Inv_Reports),
                new SqlParameter("@AssReg_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "AssReg_Create", DataRowVersion.Current, this.AssReg_Create),
                new SqlParameter("@AssReg_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "AssReg_View", DataRowVersion.Current, this.AssReg_View),
                new SqlParameter("@AssReg_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "AssReg_Update", DataRowVersion.Current, this.AssReg_Update),

                new SqlParameter("@AssReg_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "AssReg_Delete", DataRowVersion.Current, this.AssReg_Delete),

                new SqlParameter("@AssTra_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "AssTra_Create", DataRowVersion.Current, this.AssTra_Create),

                new SqlParameter("@AssTra_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "AssTra_View", DataRowVersion.Current, this.AssTra_View),

                new SqlParameter("@AssTra_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "AssTra_Update", DataRowVersion.Current, this.AssTra_Update),
                new SqlParameter("@AssTra_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "AssTra_Delete", DataRowVersion.Current, this.AssTra_Delete),
                new SqlParameter("@DailRun_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "DailRun_Create", DataRowVersion.Current, this.DailRun_Create),
                new SqlParameter("@DailRun_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "DailRun_View", DataRowVersion.Current, this.DailRun_View),
                new SqlParameter("@DailRun_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "DailRun_Update", DataRowVersion.Current, this.DailRun_Update),
                new SqlParameter("@DailRun_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "DailRun_Delete", DataRowVersion.Current, this.DailRun_Delete),
                new SqlParameter("@Ass_Rep", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Ass_Rep", DataRowVersion.Current, this.Ass_Rep),
				
			};

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
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
        /// <summary>
        /// Fuction Name: update.
        /// Called By:  Nill.
        /// Description: check the sql connection.
        /// Changes history: Nill.
        /// </summary>
        /// <param name="SqlConn"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
        public bool CHANGEPASSWORD_UPDATE(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return CHANGEPASSWORD_UPDATE(SqlConn, null, enumSpName);
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
        public bool CHANGEPASSWORD_UPDATE(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return CHANGEPASSWORD_UPDATE(null, SqlTran, enumSpName);
        }
        private bool CHANGEPASSWORD_UPDATE(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 15;

                SqlParameter[] colParams = new SqlParameter[]
			{
             
               
				  new SqlParameter("@username", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "UserID", DataRowVersion.Current, this.UserID), 
                     new SqlParameter("@password", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "Password", DataRowVersion.Current, this.Password), 
					 new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task), 
                     
				
			};

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
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
        /// <summary>
        /// Fuction Name: update.
        /// Called By:  Nill.
        /// Description: check the sql connection.
        /// Changes history: Nill.
        /// </summary>
        /// <param name="SqlConn"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
        public bool Desig_Update(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Desig_Update(SqlConn, null, enumSpName);
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
        public bool Desig_Update(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return Desig_Update(null, SqlTran, enumSpName);
        }
        private bool Desig_Update(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
               
               
				new SqlParameter("@Designation", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Designation", DataRowVersion.Current, this.Designation),
               	new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current ,this.Task),		
                new SqlParameter("@ContentDesig", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ContentDesig", DataRowVersion.Current, this.ContentDesig),
				
			};

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
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
        public bool Insert_AllUserDetails(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Insert_AllUserDetails(SqlConn, null, enumSpName);
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
        public bool Insert_AllUserDetails(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return Insert_AllUserDetails(null, SqlTran, enumSpName);
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
        private bool Insert_AllUserDetails(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{  
                new SqlParameter("@Name", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, this.Name), 
                new SqlParameter("@UserID", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "UserID", DataRowVersion.Current, this.UserID), 
                new SqlParameter("@Password ", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "Password", DataRowVersion.Current, this.Password),
                new SqlParameter("@Email_ID", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Email_ID", DataRowVersion.Current, this.Email_ID),
                new SqlParameter("@Employee_ID ", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Employee_ID", DataRowVersion.Current, this.Employee_ID),
                new SqlParameter("@Project_Code ", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code ?? (object)DBNull.Value),
                new SqlParameter("@Status ", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Status", DataRowVersion.Current, this.Status ?? (object)DBNull.Value),
                new SqlParameter("@Role", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Role", DataRowVersion.Current, this.Role ?? (object)DBNull.Value),
				new SqlParameter("@Designation", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Designation", DataRowVersion.Current, this.Designation ?? (object)DBNull.Value),    
                new SqlParameter("@Department", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Department", DataRowVersion.Current, this.Department ?? (object)DBNull.Value), 
                new SqlParameter("@IsHoUser", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "IsHoUser", DataRowVersion.Current, this.IsHoUser), 
            
                  new SqlParameter("@DigitalSign_FileName", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "DigitalSign_FileName", DataRowVersion.Current, this.DigitalSign_FileName), 

                    new SqlParameter("@Image_FilePath", SqlDbType.NVarChar, 4000, ParameterDirection.Input, false, 0, 0, "Image_FilePath", DataRowVersion.Current, this.Image_FilePath), 

                // Page Level Access
                new SqlParameter("@UserCreate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "UserCreate", DataRowVersion.Current, this.UserCreate),             
                new SqlParameter("@UserView", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "UserView", DataRowVersion.Current, this.UserView),
                new SqlParameter("@UserUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "UserUpdate", DataRowVersion.Current, this.UserUpdate),
                new SqlParameter("@UserDelete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "UserDelete", DataRowVersion.Current, this.UserDelete),
                new SqlParameter("@EmpContractCreate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "EmpContractCreate", DataRowVersion.Current, this.EmpContractCreate),             
                new SqlParameter("@EmpContractView", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "EmpContractView", DataRowVersion.Current, this.EmpContractView),
                new SqlParameter("@EmpContractUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "EmpContractUpdate", DataRowVersion.Current, this.EmpContractUpdate),
                new SqlParameter("@EmpContractDelete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "EmpContractDelete", DataRowVersion.Current, this.EmpContractDelete),
                new SqlParameter("@Mail_update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Mail_update", DataRowVersion.Current, this.Mail_update),
                new SqlParameter("@CompySite_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "CompySite_Creat", DataRowVersion.Current, this.CompySite_Create),
                new SqlParameter("@CompySite_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "CompySite_View", DataRowVersion.Current, this.CompySite_View),
                new SqlParameter("@CompySite_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "CompySite_Update", DataRowVersion.Current, this.CompySite_Update),
                new SqlParameter("@CompSite_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "CompSite_Delete", DataRowVersion.Current, this.CompSite_Delete),
                new SqlParameter("@Proj_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Proj_Create", DataRowVersion.Current, this.Proj_Delete),
                new SqlParameter("@Proj_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Proj_View", DataRowVersion.Current, this.Proj_View),
                new SqlParameter("@Proj_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Proj_Update", DataRowVersion.Current, this.Proj_Update),
                new SqlParameter("@Proj_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Proj_Delete", DataRowVersion.Current, this.Proj_Delete),
                new SqlParameter("@UOM_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "UOM_Create", DataRowVersion.Current, this.UOM_Create),
                new SqlParameter("@UOM_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "UOM_View", DataRowVersion.Current, this.@UOM_View),
                new SqlParameter("@UOM_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "UOM_Update", DataRowVersion.Current, this.UOM_Update),
                new SqlParameter("@UOM_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "UOM_Delete", DataRowVersion.Current, this.UOM_Delete),
                new SqlParameter("@Pro_BudgetCreate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Create", DataRowVersion.Current, this.Pro_Bug_Create),
                new SqlParameter("@Pro_BudgetView", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_View", DataRowVersion.Current, this.Pro_Bug_View),

                                            new SqlParameter("@Pro_BudgetUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Update", DataRowVersion.Current, this.Pro_Bug_Update),
                                            new SqlParameter("@Pro_BudgetDelete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Delete", DataRowVersion.Current, this.Pro_Bug_Delete),
                               new SqlParameter("@Material_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Material_Create", DataRowVersion.Current, this.Material_Create),
                                new SqlParameter("@Material_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Material_View", DataRowVersion.Current, this.Material_View),
                                 new SqlParameter("@Material_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Material_Update", DataRowVersion.Current, this.Material_Update),
                                  new SqlParameter("@Material_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Material_Delete", DataRowVersion.Current, this.Material_Delete),
                                    new SqlParameter("@Vendor_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Vendor_Create", DataRowVersion.Current, this.Vendor_Create),
                                    new SqlParameter("@Vendor_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Vendor_View", DataRowVersion.Current, this.Vendor_View),
                                    new SqlParameter("@Vendor_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Vendor_Update", DataRowVersion.Current, this.Vendor_Update),
                                    new SqlParameter("@Vendor_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Vendor_Delete", DataRowVersion.Current, this.Vendor_Delete),
                                     new SqlParameter("@Sub_Cont_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Sub_Cont_Create", DataRowVersion.Current, this.Sub_Cont_Create),
                                      new SqlParameter("@Sub_Cont_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Sub_Cont_View", DataRowVersion.Current, this.Sub_Cont_View),
                                       new SqlParameter("@Sub_Cont_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Sub_Cont_Update", DataRowVersion.Current, this.Sub_Cont_Update),
                                        new SqlParameter("@Sub_Cont_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Sub_Cont_Delete", DataRowVersion.Current, this.Sub_Cont_Delete),
                new SqlParameter("@OtherCreate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "OtherCreate", DataRowVersion.Current, this.OtherCreate),             
                new SqlParameter("@OtherView", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "OtherView", DataRowVersion.Current, this.OtherView),
                new SqlParameter("@OtherUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "OtherUpdate", DataRowVersion.Current, this.OtherUpdate),
                new SqlParameter("@OtherDelete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "OtherDelete", DataRowVersion.Current, this.OtherDelete),  
                                        new SqlParameter("@Bug_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Create", DataRowVersion.Current, this.Bug_Create),
                                           new SqlParameter("@Bug_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_View", DataRowVersion.Current, this.Bug_View),

                                            new SqlParameter("@Bug_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Update", DataRowVersion.Current, this.Bug_Update),
                                            new SqlParameter("@Bug_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Delete", DataRowVersion.Current, this.Bug_Delete),
                                            new SqlParameter("@Bug_Mod_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Mod_Create", DataRowVersion.Current, this.Bug_Mod_Create),
                                            new SqlParameter("@Bug_Mod_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Mod_View", DataRowVersion.Current, this.Bug_Mod_View),
                                            new SqlParameter("@Bug_Mod_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Mod_Update", DataRowVersion.Current, this.Bug_Mod_Update),
                                            new SqlParameter("@Bug_Mod_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Mod_Delete", DataRowVersion.Current, this.Bug_Mod_Delete),
                                            new SqlParameter("@Bug_Reports", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Bug_Reports", DataRowVersion.Current, this.Bug_Reports),
                                            new SqlParameter("@Stock_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Stock_Create", DataRowVersion.Current, this.Stock_Create),
                                            new SqlParameter("@Stock_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Stock_View", DataRowVersion.Current, this.Stock_View),
                                             new SqlParameter("@Stock_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Stock_Update", DataRowVersion.Current, this.Stock_Update),
                                              new SqlParameter("@Stock_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Stock_Delete", DataRowVersion.Current, this.Stock_Delete),
                                               new SqlParameter("@Quotn_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Quotn_Create", DataRowVersion.Current, this.Quotn_Create),


                                               new SqlParameter("@Stock_TransferCreate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Stock_TransferCreate", DataRowVersion.Current, this.Stock_TransferCreate),
                                              new SqlParameter("@Stock_TransferView", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Stock_TransferView", DataRowVersion.Current, this.Stock_TransferView),
                                             new SqlParameter("@Stock_TransferUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Stock_TransferUpdate", DataRowVersion.Current, this.Stock_TransferUpdate),
                                              new SqlParameter("@Stock_TransferDelete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Stock_TransferDelete", DataRowVersion.Current, this.Stock_TransferDelete),
                                            
                                               new SqlParameter("@Quotn_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Quotn_View", DataRowVersion.Current, this.Quotn_View),
                                                new SqlParameter("@Quotn_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Quotn_Update", DataRowVersion.Current, this.Quotn_Update),
                                                new SqlParameter("@Quotn_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Quotn_Delete", DataRowVersion.Current, this.Quotn_Delete),
                                               new SqlParameter("@Quotn_Compare", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Quotn_Compare", DataRowVersion.Current, this.Quotn_Compare),
                                                new SqlParameter("@Ind_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Ind_Create", DataRowVersion.Current, this.Ind_Create),
                                                 new SqlParameter("@Ind_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Ind_View", DataRowVersion.Current, this.Ind_View),
                                                  new SqlParameter("@Ind_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Ind_Update", DataRowVersion.Current, this.Ind_Update),
                                                   new SqlParameter("@Ind_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Ind_Delete", DataRowVersion.Current, this.Ind_Delete),
                                                   new SqlParameter("@PO_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PO_Create", DataRowVersion.Current, this.PO_Create),
                                                   new SqlParameter("@PO_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PO_View", DataRowVersion.Current, this.PO_View),
                                                    new SqlParameter("@PO_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PO_Update", DataRowVersion.Current, this.PO_Update),
                                                    new SqlParameter("@PO_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PO_Delete", DataRowVersion.Current, this.PO_Delete),
                new SqlParameter("@PayInd_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_Create", DataRowVersion.Current, this.PayInd_Create),             
                new SqlParameter("@PayInd_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_View", DataRowVersion.Current, this.PayInd_View),
                new SqlParameter("@PayInd_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_Update", DataRowVersion.Current, this.PayInd_Update),
                new SqlParameter("@PayInd_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_Delete", DataRowVersion.Current, this.PayInd_Delete),  
                new SqlParameter("@PayInd_Ver_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_Ver_Create", DataRowVersion.Current, this.PayInd_Ver_Create),             
                new SqlParameter("@PayInd_Ver_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_Ver_View", DataRowVersion.Current, this.PayInd_Ver_View),
                new SqlParameter("@PayInd_Ver_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_Ver_Update", DataRowVersion.Current, this.PayInd_Ver_Update),
                new SqlParameter("@PayInd_Ver_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_Ver_Delete", DataRowVersion.Current, this.PayInd_Ver_Delete),  
                new SqlParameter("@PayInd_App_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_App_Create", DataRowVersion.Current, this.PayInd_App_Create),             
                new SqlParameter("@PayInd_App_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_App_View", DataRowVersion.Current, this.PayInd_App_View),
                new SqlParameter("@PayInd_App_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_App_Update", DataRowVersion.Current, this.PayInd_App_Update),
                new SqlParameter("@PayInd_App_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "PayInd_App_Delete", DataRowVersion.Current, this.PayInd_App_Delete),  
                    
                new SqlParameter("@Proc_Reports", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Proc_Reports", DataRowVersion.Current, this.Proc_Reports),
                
                new SqlParameter("@MRN_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "MRN_Create", DataRowVersion.Current, this.MRN_Create),
                new SqlParameter("@MRN_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "MRN_View", DataRowVersion.Current, this.MRN_View),
                new SqlParameter("@MRN_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "MRN_Update", DataRowVersion.Current, this.MRN_Update),
                new SqlParameter("@MRN_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "MRN_Delete", DataRowVersion.Current, this.MRN_Delete),
                new SqlParameter("@MIN_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "MIN_Create", DataRowVersion.Current, this.MIN_Create),
                new SqlParameter("@MIN_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "MIN_View", DataRowVersion.Current, this.MIN_View),
                new SqlParameter("@MIN_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "MIN_Update", DataRowVersion.Current, this.MIN_Update),
                new SqlParameter("@MIN_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "MIN_Delete", DataRowVersion.Current, this.MIN_Delete),
                new SqlParameter("@Inv_Reports", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Inv_Reports", DataRowVersion.Current, this.Inv_Reports),
                new SqlParameter("@AssReg_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "AssReg_Create", DataRowVersion.Current, this.AssReg_Create),
                new SqlParameter("@AssReg_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "AssReg_View", DataRowVersion.Current, this.AssReg_View),
                new SqlParameter("@AssReg_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "AssReg_Update", DataRowVersion.Current, this.AssReg_Update),

                         new SqlParameter("@AssReg_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "AssReg_Delete", DataRowVersion.Current, this.AssReg_Delete),

                         new SqlParameter("@AssTra_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "AssTra_Create", DataRowVersion.Current, this.AssTra_Create),

                         new SqlParameter("@AssTra_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "AssTra_View", DataRowVersion.Current, this.AssTra_View),

                         new SqlParameter("@AssTra_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "AssTra_Update", DataRowVersion.Current, this.AssTra_Update),
                         new SqlParameter("@AssTra_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "AssTra_Delete", DataRowVersion.Current, this.AssTra_Delete),
                         new SqlParameter("@DailRun_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "DailRun_Create", DataRowVersion.Current, this.DailRun_Create),
                         new SqlParameter("@DailRun_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "DailRun_View", DataRowVersion.Current, this.DailRun_View),
                         new SqlParameter("@DailRun_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "DailRun_Update", DataRowVersion.Current, this.DailRun_Update),
                         new SqlParameter("@DailRun_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "DailRun_Delete", DataRowVersion.Current, this.DailRun_Delete),
                         new SqlParameter("@Ass_Rep", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Ass_Rep", DataRowVersion.Current, this.Ass_Rep),
          		
                        new SqlParameter("@Local_MRN_Create", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Local_MRN_Create", DataRowVersion.Current, this.Local_MRN_Create),
	                    new SqlParameter("@Local_MRN_Update", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Local_MRN_Update", DataRowVersion.Current, this.Local_MRN_Update),
                        new SqlParameter("@LocalMRN_View", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "LocalMRN_View", DataRowVersion.Current, this.LocalMRN_View),
                        new SqlParameter("@Local_MRN_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Local_MRN_Delete", DataRowVersion.Current, this.Local_MRN_Delete),
			            new SqlParameter("@Appr_PO_Delete", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Appr_PO_Delete", DataRowVersion.Current, this.Appr_PO_Delete)

            
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


        public UserBL()
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


        private string getSpName(eLoadSp enumSp)
        {
            switch (enumSp)
            {
                case eLoadSp.InsertDepartment:
                    return "Pro_InsertDepartment";
                case eLoadSp.InsertDesignation:
                    return "Pro_InsertDesignation";
                case eLoadSp.RetriveDesignation:
                    return "Pro_RetriveDesignation";
                case eLoadSp.RetriveDepartment:
                    return "Pro_RetriveDepartment";
                case eLoadSp.SELECT_PROJECT_ALL:
                    return "PRO_SELECT_PROJECT_ALL";
                case eLoadSp.update:
                    return "Pro_InsertDepartment";
                case eLoadSp.DEPARTMENT_CHECKDUPLICATE:
                    return "Pro_InsertDepartment";
                case eLoadSp.DESIGNATION_CHECKDUPLICATE:
                    return "Pro_InsertDesignation";
                case eLoadSp.INSERT_ALLUSERS:
                    return "Pro_INSERT_USERS";
                case eLoadSp.SELECT_USERS_DETAILS_BY_ID:
                    return "PRO_SELECT_USERS_DETAILS_BY_ID";
                case eLoadSp.DELETE_DEPT:
                    return "Pro_InsertDepartment";
                case eLoadSp.DELETE_DESIGNATION:
                    return "Pro_InsertDesignation";
                case eLoadSp.UPDATE_USERByID:
                    return "PRO_UPDATE_USERByID";
                case eLoadSp.FORGOTPASSWORD_GETNAME:
                    return "PRO_FORGOT_PASSWORD";
                case eLoadSp.FORGOTPASSWORD_GET_PASSWORD:
                    return "PRO_FORGOT_PASSWORD";
                case eLoadSp.CHANGEPASSWORD:
                    return "PRO_FORGOT_PASSWORD";
                case eLoadSp.GET_USER_NAME:
                    return "PRO_FORGOT_PASSWORD";
                case eLoadSp.CHECK_MAIL:
                    return "PRO_FORGOT_PASSWORD";
                case eLoadSp.RETRIVE_MODULEACCESS_ALL:
                    return "PRO_RETRIVE_MODULEACCESS_ALL";
                case eLoadSp.UPDATE_NEW_PASSWORD:
                    return "PRO_FORGOT_PASSWORD";
                default:
                    return string.Empty;
            }
        }
        #region "Class: Vendor Methods"
        /// <summary>
        /// Fuction Name:   Insert.
        /// Called By:      Nill.
        /// Description:    Check the Sql conncetion.
        /// Change histroy: Nill.
        /// </summary>
        /// <param name="con"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
        //public string  insert(SqlConnection SqlConn,enumSp)
        //{
        //    return insert(con, null, enumSp);
        //}


        #endregion

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


    }
}
