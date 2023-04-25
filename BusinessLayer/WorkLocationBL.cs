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
   public class WorkLocationBL
    {
        #region "Class: WL  Declarations"

        public enum eLoadSp
        {
            SELECT_WORKLOCATION_ALL = 1,
            INSERT_WORK_LOCATION=2,
            SELECT_UOM_ALL=3,
            SELECT_ALL_MATERIALDETAILS = 4,
            SELECT_MATERIAL_ALL=5,
            SELECT_ASSET_ALL=6,
            SELECT_CONTRACTOR_ALL=7,

        };

        #endregion


        #region "Class: MRNBL Sets / Gets"
        public string Project_Code { get; set; }
        public string Work_Location { get; set; }
        public string Work_Name { get; set; }
        public string Task { get; set; }
        public string SubWork_Name { get; set; }
        public string Quantity { get; set; }
        public string UOM { get; set; }
        public int WorkName_ID { get; set; }
        public int ID { get; set; }
        public int Work_Location_ID { get; set; }
        //Utilized Machinary 
        public string Machinary_Name { get; set; }
        public string SiteImage { get; set; }
        public string Remarks { get; set; }
        public decimal Issued_Diesel { get; set; }
        public decimal OutPut { get; set; }
       
        public decimal EndKM { get; set; }
        public decimal StartKM { get; set; }
        public string Unit { get; set; }
        public string Reg_Number { get; set; }

       // 
        public string UtilizedLabour_LabourType  { get; set; }
        public decimal UtilizedLabour_Quantity { get; set; }
        public string UtilizedLabour_UOM { get; set; }
        public DateTime UtilizedLabourDate { get; set; }

        public string Consumed_Material_Name{ get; set; }
        public int Consumed_Material_ID { get; set; }
        public decimal Consumed_Material_Quantity { get; set; }
        public string Consumed_Material_UOM { get; set; }
        public DateTime ConsumedMaterialDate { get; set; }
        public DateTime UtilizedMachinaryDate { get; set; }

        public string UtilizedMachinary_File { get; set; }
        // Sub Work Req Material
        public string Type_Of_Labour { get; set; }
        public string Name_Of_Machinery { get; set; }
        public string Material_Name { get; set; }
        public int Material_ID{ get; set; }
        public decimal SubWork_Quantity { get; set; }
        public string SubWork_UOM { get; set; }
        public int SubWork_ID { get; set; }
        public string Asset_Type     { get; set; }
        public string Asset { get; set; }
        public string Asset_Category { get; set; }
        public string Asset_RegNo { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Location_ID { get; set; }
       
        public int SubWorkName_ID { get; set; }

        public int Sub_ContractorID { get; set; }
       
        #endregion
        #region "Class: Category Methods"


        private string getSpName(eLoadSp enumSpName)
        {
            switch (enumSpName)
            {
                case eLoadSp.SELECT_WORKLOCATION_ALL:
                    return "PRO_WORK_LOCATION_OPERATIONS";
                case eLoadSp.INSERT_WORK_LOCATION:
                case eLoadSp.SELECT_MATERIAL_ALL:
                    return "PRO_WORK_LOCATION_OPERATIONS";
                case eLoadSp.SELECT_UOM_ALL:
                    return "PROC_UOM_SELECT";
                case eLoadSp.SELECT_ALL_MATERIALDETAILS:
                    return "PROC_MATERIAL_SELECT";
                case eLoadSp.SELECT_ASSET_ALL:
                    return "PROC_ASSET_SELECT_ALL";
                case eLoadSp.SELECT_CONTRACTOR_ALL:
                    return "PRO_contractor_SELECT_ALL_FOR_ASSET";
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

                case eLoadSp.SELECT_WORKLOCATION_ALL:
                    colParams = new SqlParameter[]
                   {
                        new SqlParameter("@Project_Code", this.Project_Code),
                        new SqlParameter("@Task", this.Task),
                         new SqlParameter("@ID", this.ID),
                         new SqlParameter("@Work_Location_ID", this.Work_Location_ID),
                          new SqlParameter("@WorkName_ID", this.WorkName_ID),
                         new SqlParameter("@SubWork_ID", this.SubWork_ID),
                          new SqlParameter("@Asset_RegNo", this.Asset_RegNo),
                           new SqlParameter("@Code", this.Code),
                   };
                    break;
                case eLoadSp.SELECT_MATERIAL_ALL:
                    colParams = new SqlParameter[]
                   {
                        new SqlParameter("@Task", this.Task),
                         new SqlParameter("@SubWork_ID", this.SubWork_ID),
                          new SqlParameter("@Project_Code", this.Project_Code),
                        
                   };
                    break;
                case eLoadSp.SELECT_ASSET_ALL:
                    colParams = new SqlParameter[]
                    { 
                       
                    
                     new SqlParameter("@Project_Code", this.Project_Code)
                    };
                    break;
            }
            return colParams;
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
        public bool WorkLocationinsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return WorkLocationinsert(SqlConn, null, enumSpName);
        }
        private bool WorkLocationinsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Project_Code", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code ?? (object)DBNull.Value ),
                new SqlParameter("@Work_Location", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Work_Location", DataRowVersion.Current, this.Work_Location ?? (object)DBNull.Value ),
                 new SqlParameter("@Task", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task ?? (object)DBNull.Value ),
               
               
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
        public bool WorkNameinsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return WorkNameinsert(SqlConn, null, enumSpName);
        }
        private bool WorkNameinsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Work_Location_ID", SqlDbType.Int, 500, ParameterDirection.Input, false, 0, 0, "Work_Location_ID", DataRowVersion.Current, this.Work_Location_ID  ),
                new SqlParameter("@Work_Name", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Work_Name", DataRowVersion.Current, this.Work_Name ?? (object)DBNull.Value ),
                 new SqlParameter("@Task", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task ?? (object)DBNull.Value ),
               
               
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
        public bool SubWorkRequired_Materialinsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return SubWorkRequired_Materialinsert(SqlConn, null, enumSpName);
        }
       
        public bool Consumed_Material_Insert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Consumed_Material_Insert(SqlConn, null, enumSpName);
        }
        public bool Utilized_Labours_Insert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Utilized_Labours_Insert(SqlConn, null, enumSpName);
        }
        public bool SubWorkRequired_Machineryinsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return SubWorkRequired_Machineryinsert(SqlConn, null, enumSpName);
        }
        public bool Utilized_Machinaryinsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Utilized_Machinaryinsert(SqlConn, null, enumSpName);
        }
        public bool SubWorkRequired_Labourinsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return SubWorkRequired_Labourinsert(SqlConn, null, enumSpName);
        }
        public bool SubWorkRequired_Masterinsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return SubWorkRequired_Masterinsert(SqlConn, null, enumSpName);
        }
        public bool SubWorkRequired_SubContraContractor(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return SubWorkRequired_SubContraContractor(SqlConn, null, enumSpName);
        }
        private bool SubWorkRequired_SubContraContractor(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Sub_ContractorID", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Sub_ContractorID", DataRowVersion.Current, this.Sub_ContractorID  ),
                 new SqlParameter("@SubWork_ID", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "SubWork_ID", DataRowVersion.Current, this.SubWork_ID  ),
                new SqlParameter("@Location_ID", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Location_ID", DataRowVersion.Current, this.Location_ID  ),
                new SqlParameter("@WorkName_ID", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "WorkName_ID", DataRowVersion.Current, this.WorkName_ID ),
                new SqlParameter("@Project_Code", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code ),             
                 new SqlParameter("@Task", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task ?? (object)DBNull.Value ),
               
               
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
        private bool SubWorkRequired_Masterinsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Location_ID", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Location_ID", DataRowVersion.Current, this.Location_ID  ),
                new SqlParameter("@WorkName_ID", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "WorkName_ID", DataRowVersion.Current, this.WorkName_ID ),
                 new SqlParameter("@SubWorkName_ID", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "SubWorkName_ID", DataRowVersion.Current, this.SubWorkName_ID ),
                 new SqlParameter("@Project_Code", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code ),             
                 new SqlParameter("@Task", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task ?? (object)DBNull.Value ),
               
               
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
        private bool SubWorkRequired_Labourinsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Type_Of_Labour", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Type_Of_Labour", DataRowVersion.Current, this.Type_Of_Labour  ),
                new SqlParameter("@Quantity", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Quantity", DataRowVersion.Current, this.SubWork_Quantity ),
                 new SqlParameter("@UOM", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "UOM", DataRowVersion.Current, this.SubWork_UOM ),
                 new SqlParameter("@SubWork_ID", SqlDbType.Int, 1000, ParameterDirection.Input, false, 0, 0, "SubWork_ID", DataRowVersion.Current, this.SubWork_ID ),
                
                 new SqlParameter("@Task", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task ?? (object)DBNull.Value ),
               
               
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

        private bool Utilized_Machinaryinsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Reg_Number", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Reg_Number", DataRowVersion.Current, this.Reg_Number  ),
                new SqlParameter("@Unit", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Unit", DataRowVersion.Current, this.Unit ),
                 new SqlParameter("@StartKM", SqlDbType.Decimal, 1000, ParameterDirection.Input, false, 0, 0, "StartKM", DataRowVersion.Current, this.StartKM ),
                 new SqlParameter("@EndKM", SqlDbType.Decimal, 1000, ParameterDirection.Input, false, 0, 0, "EndKM", DataRowVersion.Current, this.EndKM ),
                  new SqlParameter("@UOM", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "UOM", DataRowVersion.Current, this.UOM ),
                   new SqlParameter("@OutPut", SqlDbType.Decimal, 1000, ParameterDirection.Input, false, 0, 0, "OutPut", DataRowVersion.Current, this.OutPut ),
                    new SqlParameter("@Issued_Diesel", SqlDbType.Decimal, 5000, ParameterDirection.Input, false, 0, 0, "Issued_Diesel", DataRowVersion.Current, this.Issued_Diesel ),
                         new SqlParameter("@Remarks", SqlDbType.VarChar, 5000, ParameterDirection.Input, false, 0, 0, "Remarks", DataRowVersion.Current, this.Remarks ),
                          new SqlParameter("@SiteImage", SqlDbType.VarChar, 5000, ParameterDirection.Input, false, 0, 0, "SiteImage", DataRowVersion.Current, this.SiteImage ),
                           new SqlParameter("@Code", SqlDbType.VarChar, 5000, ParameterDirection.Input, false, 0, 0, "Code", DataRowVersion.Current, this.Code ),
                           new SqlParameter("@Project_Code", SqlDbType.VarChar, 5000, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code ),
                            new SqlParameter("@UtilizedMachinaryDate", SqlDbType.Date, 5000, ParameterDirection.Input, false, 0, 0, "UtilizedMachinaryDate", DataRowVersion.Current, this.UtilizedMachinaryDate ),
                            new SqlParameter("@Machinary_Name", SqlDbType.VarChar, 5000, ParameterDirection.Input, false, 0, 0, "Machinary_Name", DataRowVersion.Current, this.Machinary_Name ),
                            new SqlParameter("@UtilizedMachinary_File", SqlDbType.VarChar, 5000, ParameterDirection.Input, false, 0, 0, "UtilizedMachinary_File", DataRowVersion.Current, this.UtilizedMachinary_File ),
                 new SqlParameter("@Task", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task ?? (object)DBNull.Value ),
               
               
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
        private bool SubWorkRequired_Machineryinsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Asset_Type", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Asset_Type", DataRowVersion.Current, this.Asset_Type  ),
                new SqlParameter("@Asset", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Asset", DataRowVersion.Current, this.Asset ),
                 new SqlParameter("@Asset_Category", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Asset_Category", DataRowVersion.Current, this.Asset_Category ),
                 new SqlParameter("@SubWork_ID", SqlDbType.Int, 1000, ParameterDirection.Input, false, 0, 0, "SubWork_ID", DataRowVersion.Current, this.SubWork_ID ),
                  new SqlParameter("@Quantity", SqlDbType.Int, 1000, ParameterDirection.Input, false, 0, 0, "Quantity", DataRowVersion.Current, this.SubWork_Quantity ),
                   new SqlParameter("@Asset_RegNo", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Asset_RegNo", DataRowVersion.Current, this.Asset_RegNo ),
                    new SqlParameter("@Name", SqlDbType.VarChar, 5000, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, this.Name ),
                         new SqlParameter("@Code", SqlDbType.VarChar, 5000, ParameterDirection.Input, false, 0, 0, "Code", DataRowVersion.Current, this.Code ),
                 new SqlParameter("@Task", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task ?? (object)DBNull.Value ),
               
               
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

        private bool Consumed_Material_Insert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Material_ID", SqlDbType.Int, 1000, ParameterDirection.Input, false, 0, 0, "Material_ID", DataRowVersion.Current, this.Consumed_Material_ID  ),
                 new SqlParameter("@Material_Name", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Material_Name", DataRowVersion.Current, this.Consumed_Material_Name  ),
                new SqlParameter("@Quantity", SqlDbType.Decimal, 1000, ParameterDirection.Input, false, 0, 0, "Quantity", DataRowVersion.Current, this.Consumed_Material_Quantity ),
                 new SqlParameter("@UOM", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "UOM", DataRowVersion.Current, this.Consumed_Material_UOM ),
                 new SqlParameter("@SubWork_ID", SqlDbType.Int, 1000, ParameterDirection.Input, false, 0, 0, "SubWork_ID", DataRowVersion.Current, this.SubWork_ID ),
                  new SqlParameter("@Project_Code", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code ),
                    new SqlParameter("@ConsumedMaterialDate", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "ConsumedMaterialDate", DataRowVersion.Current, this.ConsumedMaterialDate ),
                 new SqlParameter("@Task", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task ?? (object)DBNull.Value ),
               
               
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
        private bool Utilized_Labours_Insert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Labour_Type", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Labour_Type", DataRowVersion.Current, this.UtilizedLabour_LabourType  ),
                new SqlParameter("@Quantity", SqlDbType.Decimal, 1000, ParameterDirection.Input, false, 0, 0, "Quantity", DataRowVersion.Current, this.UtilizedLabour_Quantity ),
                 new SqlParameter("@UOM", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "UOM", DataRowVersion.Current, this.UtilizedLabour_UOM ),
                 new SqlParameter("@SubWork_ID", SqlDbType.Int, 1000, ParameterDirection.Input, false, 0, 0, "SubWork_ID", DataRowVersion.Current, this.SubWork_ID ),
                  new SqlParameter("@Project_Code", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code ),
                   new SqlParameter("@UtilizedLabourDate", SqlDbType.Date, 1000, ParameterDirection.Input, false, 0, 0, "UtilizedLabourDate", DataRowVersion.Current, this.UtilizedLabourDate ),
                 new SqlParameter("@Task", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task ?? (object)DBNull.Value ),
               
               
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
        private bool SubWorkRequired_Materialinsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Material_Name", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Material_Name", DataRowVersion.Current, this.Material_Name  ),
                new SqlParameter("@Quantity", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Quantity", DataRowVersion.Current, this.SubWork_Quantity ),
                 new SqlParameter("@UOM", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "UOM", DataRowVersion.Current, this.SubWork_UOM ),
                 new SqlParameter("@SubWork_ID", SqlDbType.Int, 1000, ParameterDirection.Input, false, 0, 0, "SubWork_ID", DataRowVersion.Current, this.SubWork_ID ),
                 new SqlParameter("@Task", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task ?? (object)DBNull.Value ),
               
               
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
        public bool SubWorkNameinsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return SubWorkNameinsert(SqlConn, null, enumSpName);
        }
        private bool SubWorkNameinsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@WorkName_ID", SqlDbType.Int, 500, ParameterDirection.Input, false, 0, 0, "WorkName_ID", DataRowVersion.Current, this.WorkName_ID  ),
                new SqlParameter("@SubWork_Name", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "SubWork_Name", DataRowVersion.Current, this.SubWork_Name ?? (object)DBNull.Value ),
                 new SqlParameter("@UOM", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "UOM", DataRowVersion.Current, this.UOM ?? (object)DBNull.Value ),
                  new SqlParameter("@Quantity", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Quantity", DataRowVersion.Current, this.Quantity ?? (object)DBNull.Value ),
                 new SqlParameter("@Task", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task ?? (object)DBNull.Value ),
               
               
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
