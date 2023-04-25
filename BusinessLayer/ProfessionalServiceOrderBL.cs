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
    public class ProfessionalServiceOrderBL
    {
        public enum eLoadSp
        {
            pro_Proffessional_Service_Order_Select_All = 01,
            PSO_Delete = 02,
            SELECT_PSO_TYPE_ALL = 03,
            SELECT_PSODETAILS_BY_PSONO = 04,
            INSERT = 05,
            UPDATE = 06,
            SELECT_PSO_ITEMS_BY_PSONO = 07,
            INSERT_PSO_ITEM = 08,
            UPDATE_PSO_ITEM = 09,
            SELECT_PSO_ITEM_BY_ITEM_ID = 10,
            DELETE_PSO_ITEM_BY_ID = 11,
            PSO_INSERT_UPDATE = 12,
            SELECT_SOW_ITEM_BY_ITEM_ID = 13,
            DELETE_SOW_ITEM_BY_ID = 14,
            INSERT_SOW_ITEM = 15,
            SELECT_PSO_ITEMS_BY_PSONO_FOR_PRINT = 16,
            SELECT_PSO_TAX_BY_PSOID_FOR_PRINT = 17,
            INSERT_SOW_ITEM_PSO = 18,
            SELECT_SOW_ITEM_BY_ITEM_ID_PSO = 19,
            SELECT_WO_ITEMS_ALL_BY_PROJECT = 20

        };
        public int UserID { get; set; }
        public string PSO_ID { get; set; }
        public string PSONo { get; set; }
        public DateTime PSODate { get; set; }
        public int PSOTypeID { get; set; }
        public string FYear { get; set; }
        public string Month { get; set; }
        public string SubContractorID { get; set; }
        public string WorkLocation { get; set; }
        public string DurationOfWork { get; set; }
        public string PaymentTerms { get; set; }
        public int ApprovedBy { get; set; }
        public int LocationID { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }

        public string Others { get; set; }
        public string ContactName { get; set; }
        public string ContactNo { get; set; }
        public string ScopeOfWork { get; set; }
        public string DispatchMode { get; set; }
        public decimal TDSPerc { get; set; }
        public string OrderType { get; set; }
        public string Uploaded_File { get; set; }

        public int? PSO_Item_Id { get; set; }
        public int? Parent_PSO_Item_Id { get; set; }
        public string Item_Type { get; set; }
        public string Item_Desc { get; set; }
        public string Sub_Item_Desc { get; set; }
        public decimal? Item_Perc { get; set; }
        public decimal? Item_Rate { get; set; }
        public int? Item_UOM { get; set; }
        public decimal? Item_Qty { get; set; }
        public decimal? Item_Amt { get; set; }
        public bool Gst_Exist { get; set; }
        public decimal Igst_Perc { get; set; }
        public decimal Igst_Amt { get; set; }
        public decimal Cgst_Perc { get; set; }
        public decimal Cgst_Amt { get; set; }
        public decimal Sgst_Perc { get; set; }
        public decimal Sgst_Amt { get; set; }
        public int SOW_Item_Id { get; set; }
        public string Scope_Of_Work { get; set; }
        public decimal? Quantity { get; set; }
        public int UMO { get; set; }
        public decimal? Rate { get; set; }
        public string Task { get; set; }
        public string ProjectCode { get; set; }
        public string GST_No { get; set; }
        public decimal? No_of_Staff_to_be_Deputed { get; set; }



        public DateTime Tentavie_Time { get; set; }
        public decimal? Amount { get; set; }





        private string getSpName(eLoadSp enumSpName)
        {
            switch (enumSpName)
            {
                case eLoadSp.pro_Proffessional_Service_Order_Select_All:
                    return "pro_Proffessional_Service_Order_Select_All";
                case eLoadSp.PSO_Delete:
                    return "PRO_DELETE_PSO_BY_PSONO";
                case eLoadSp.SELECT_PSO_TYPE_ALL:
                    return "PROC_PSO_TYPE_ALL";
                case eLoadSp.SELECT_PSODETAILS_BY_PSONO:
                    return "PROC_SELECT_PSODETAILS_BY_PSONO";
                case eLoadSp.INSERT:
                    return "PROC_PSO_INSERT_UPDATE";
                case eLoadSp.UPDATE:
                    return "PROC_PSO_INSERT_UPDATE";
                case eLoadSp.SELECT_PSO_ITEMS_BY_PSONO:
                    return "PROC_PSO_SELECT_ALL";
                case eLoadSp.INSERT_PSO_ITEM:
                    return "PROC_PSO_ITEM_INSERT_UPDATE";
                case eLoadSp.UPDATE_PSO_ITEM:
                    return "PROC_PSO_ITEM_INSERT_UPDATE";
                case eLoadSp.SELECT_PSO_ITEM_BY_ITEM_ID:
                    return "PROC_PSO_ITEM_SELECT";
                case eLoadSp.DELETE_PSO_ITEM_BY_ID:
                    return "PROC_DELETE_PSO_ITEM_BY_ID";
                case eLoadSp.PSO_INSERT_UPDATE:
                    return "PROC_PSO_ITEM_INSERT_UPDATE";
                case eLoadSp.SELECT_SOW_ITEM_BY_ITEM_ID:
                    return "SELECT_SOW_ITEM_BY_ITEM_ID";
                case eLoadSp.DELETE_SOW_ITEM_BY_ID:
                    return "PROC_DELETE_SOW_ITEM_BY_ID";
                case eLoadSp.INSERT_SOW_ITEM:
                    return "PROC_SOW_ITEM_INSERT_UPDATE";
                case eLoadSp.SELECT_PSO_ITEMS_BY_PSONO_FOR_PRINT:
                    return "PROC_PSO_SELECT_For_Print";
                case eLoadSp.SELECT_PSO_TAX_BY_PSOID_FOR_PRINT:
                    return "SELECT_WO_TAX_BY_WOID_For_Print";
                case eLoadSp.INSERT_SOW_ITEM_PSO:
                    return "PROC_SOW_ITEM_INSERT_UPDATE_PSO";
                case eLoadSp.SELECT_SOW_ITEM_BY_ITEM_ID_PSO:
                    return "SELECT_SOW_ITEM_BY_ITEM_ID_PSO";
                case eLoadSp.SELECT_WO_ITEMS_ALL_BY_PROJECT:
                    return "PROC_WO_ITEMS_SELECT_BY_PROJECT";

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
                case eLoadSp.pro_Proffessional_Service_Order_Select_All:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@Project_Code", this.ProjectCode),
                        new SqlParameter("@Task", this.Task)
                    };
                    //break;

                    //case eLoadSp.PSO_Delete:
                    //    //case eLoadSp.CHECK_PSONO_IN_MRN:
                    //    colParams = new SqlParameter[]
                    //{
                    //    new SqlParameter("@Task", this.Task),
                    //    new SqlParameter("@PSONo", this.PSONo)

                    //};
                    break;
                case eLoadSp.SELECT_PSODETAILS_BY_PSONO:
                case eLoadSp.PSO_Delete:
                    //case eLoadSp.CHECK_WONO_IN_MRN:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@PSONo", this.PSONo)

                };
                    break;
                case eLoadSp.INSERT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@PSOID", this.PSO_ID)

                };
                    break;
                case eLoadSp.SELECT_PSO_ITEMS_BY_PSONO:
                    //case eLoadSp.SELECT_WO_ITEMS_BY_WONO_FOR_PRINT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@PSOID", this.PSO_ID),
                    new SqlParameter("@Task", this.Task)

                };
                    break;


                case eLoadSp.SELECT_PSO_ITEM_BY_ITEM_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@PSOItemID", this.PSO_Item_Id),
                    new SqlParameter("@Task", this.Task)

                };
                    break;
                case eLoadSp.DELETE_PSO_ITEM_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@PSOItemID", this.PSO_Item_Id),
                    new SqlParameter("@Task", this.Task)
                };

                    break;
                case eLoadSp.SELECT_SOW_ITEM_BY_ITEM_ID:
                    //case eLoadSp.SELECT_SOW_ITEM_BY_ITEM_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@PSOID", this.PSO_ID),
                    new SqlParameter("@Task", this.Task)

                };
                    break;
                case eLoadSp.DELETE_SOW_ITEM_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@SOW_Item_Id", this.SOW_Item_Id),
                    new SqlParameter("@Task", this.Task)
                };

                    break;
                //case eLoadSp.SELECT_PSO_ITEMS_BY_PSONO:
                case eLoadSp.SELECT_PSO_ITEMS_BY_PSONO_FOR_PRINT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@PSOID", this.PSO_ID),
                    new SqlParameter("@Task", this.Task)

                };
                    break;
                case eLoadSp.SELECT_SOW_ITEM_BY_ITEM_ID_PSO:
                    //case eLoadSp.SELECT_SOW_ITEM_BY_ITEM_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@PSOID", this.PSO_ID),
                    new SqlParameter("@Task", this.Task)

                };
                    break;

            }

            return colParams;
        }
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
                    if (dr["Indent_No"] != DBNull.Value)
                    {
                        this.PSONo = dr["Indent_No"].ToString();
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
        public bool PSOTaxInsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return PSOTaxInsert(SqlConn, null, enumSpName);
        }

        private bool PSOTaxInsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@PSO_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "PSO_ID", DataRowVersion.Current, this.PSO_ID),
               
                //new SqlParameter("@Type", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Type", DataRowVersion.Current, this.Type),
                //new SqlParameter("@Description", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Current, this.Description),
                //new SqlParameter("@Type_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Type_Perc", DataRowVersion.Current, this.Type_Perc),
                //new SqlParameter("@Type_Amount", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Type_Amount", DataRowVersion.Current, this.Type_Amount)
				
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
        public bool updatePSOTax(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updatePSOTax(SqlConn, null, enumSpName);
        }

        private bool updatePSOTax(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                
                //new SqlParameter("@WO_Tax_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "@WO_Tax_ID", DataRowVersion.Current, this.WO_Tax_ID),
              
                // new SqlParameter("@WO_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "WO_ID", DataRowVersion.Current, this.WO_ID),
               
                //new SqlParameter("@Type", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Type", DataRowVersion.Current, this.Type),
                //new SqlParameter("@Description", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Current, this.Description),
                //new SqlParameter("@Type_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Type_Perc", DataRowVersion.Current, this.Type_Perc),
                //new SqlParameter("@Type_Amount", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Type_Amount", DataRowVersion.Current, this.Type_Amount)
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
        public bool updatePSONetToatl(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updatePSONetToatl(SqlConn, null, enumSpName);
        }

        private bool updatePSONetToatl(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {

                new SqlParameter("@PSO_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "PSO_ID", DataRowVersion.Current, this.PSO_ID)


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


        public bool UpdateApprovalPSO(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return UpdateApprovalPSO(SqlConn, null, enumSpName);
        }

        public bool UpdateApprovalPSO(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return UpdateApprovalPSO(null, SqlTran, enumSpName);
        }

        private bool UpdateApprovalPSO(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 16;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@PSONo", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "PSONo", DataRowVersion.Current, this.PSONo),
                 //new SqlParameter("@Approval_Date", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "Approval_Date", DataRowVersion.Current, this.Approval_Date),
                 //new SqlParameter("@Approver_Com", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "Approver_Com", DataRowVersion.Current, this.Approver_Com),
                 //new SqlParameter("@ApprovalStatus", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "ApprovalStatus", DataRowVersion.Current, this.ApprovalStatus),
            
				
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
                new SqlParameter("@PSONo", SqlDbType.VarChar, 30, ParameterDirection.Output, false, 0, 0, "PSONo", DataRowVersion.Current, this.PSONo),
                new SqlParameter("@PSOID", SqlDbType.Int,4, ParameterDirection.Output, false, 0, 0, "PSO_ID", DataRowVersion.Current, this.PSO_ID),
                new SqlParameter("@PSODate", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "PSODate", DataRowVersion.Current, this.PSODate),
                new SqlParameter("@Project_Code", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.ProjectCode),
                new SqlParameter("@PSOTypeID", SqlDbType.Int,4, ParameterDirection.Input, false, 0, 0, "PSOTypeID", DataRowVersion.Current, this.PSOTypeID),
                new SqlParameter("@SubContractorID", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "SubContractorID", DataRowVersion.Current, this.SubContractorID),
                new SqlParameter("@WorkLocation", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "WorkLocation", DataRowVersion.Current, this.WorkLocation),
                new SqlParameter("@DurationOfWork", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "DurationOfWork", DataRowVersion.Current, this.DurationOfWork),
                new SqlParameter("@PaymentTerms", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "PaymentTerms", DataRowVersion.Current, this.PaymentTerms),
                new SqlParameter("@GST_No", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "GST_No", DataRowVersion.Current, this.GST_No),
                new SqlParameter("@ApprovedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "ApprovedBy", DataRowVersion.Current, this.ApprovedBy),
                new SqlParameter("@Status", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "Status", DataRowVersion.Current, this.Status),
                new SqlParameter("@Remarks", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "Remarks", DataRowVersion.Current, this.Remarks),
                new SqlParameter("@Others", SqlDbType.VarChar,2000, ParameterDirection.Input, false, 10, 0, "Others", DataRowVersion.Current, this.Others),
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
                new SqlParameter("@Prepared_By", SqlDbType.Int,4, ParameterDirection.Input, false,0, 0, "Prepared_By", DataRowVersion.Current, this.UserID),
                new SqlParameter("@ContactName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "ContactName", DataRowVersion.Current, this.ContactName),
                new SqlParameter("@ContactNo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "ContactNo", DataRowVersion.Current, this.ContactNo),
                new SqlParameter("@ScopeOfWork", SqlDbType.VarChar, 5000, ParameterDirection.Input, false, 10, 0, "ScopeOfWork", DataRowVersion.Current, this.ScopeOfWork),
                new SqlParameter("@TDSPerc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "TDSPerc", DataRowVersion.Current, this.TDSPerc),
                new SqlParameter("@FYear", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "FYear", DataRowVersion.Current, this.FYear),
                new SqlParameter("@OrderType", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "OrderType", DataRowVersion.Current, this.OrderType),
                new SqlParameter("@Month", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Month", DataRowVersion.Current, this.Month)
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

                    this.PSONo = (string)colParams.First().Value;
                    //this.PSO_ID = (int)colParams[1].Value;
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
        public bool insertItem(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insertItem(SqlConn, null, enumSpName);
        }

        public bool insertItem(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return insertItem(null, SqlTran, enumSpName);
        }

        private bool insertItem(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
                new SqlParameter("@PSONo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "PSONo", DataRowVersion.Current, this.PSONo),
                new SqlParameter("@Item_Type", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "Item_Type", DataRowVersion.Current, this.Item_Type),
                new SqlParameter("@Item_Desc", SqlDbType.VarChar, 6000, ParameterDirection.Input, false, 0, 0, "Item_Desc", DataRowVersion.Current, this.Item_Desc),
                new SqlParameter("@Item_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Item_Perc", DataRowVersion.Current, this.Item_Perc),
                new SqlParameter("@Rate", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Rate", DataRowVersion.Current, this.Item_Rate),
                new SqlParameter("@UOM", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "UOM", DataRowVersion.Current, this.Item_UOM),
                new SqlParameter("@Item_Qty", SqlDbType.Decimal, 10, ParameterDirection.Input, false, 10, 0, "Item_Qty", DataRowVersion.Current, this.Item_Qty),
                new SqlParameter("@Total_Amt", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Total_Amt", DataRowVersion.Current, this.Item_Amt),
                new SqlParameter("@Gst_Exist", SqlDbType.Bit, 9, ParameterDirection.Input, false, 10, 0, "Gst_Exist", DataRowVersion.Current, this.Gst_Exist),
                new SqlParameter("@Igst_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Igst_Perc", DataRowVersion.Current, this.Igst_Perc),
                new SqlParameter("@Cgst_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Cgst_Perc", DataRowVersion.Current, this.Cgst_Perc),
                new SqlParameter("@Sgst_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Sgst_Perc", DataRowVersion.Current, this.Sgst_Perc),
                new SqlParameter("@Sub_Item_Desc", SqlDbType.VarChar, 6000, ParameterDirection.Input, false, 0, 0, "Sub_Item_Desc", DataRowVersion.Current, this.Sub_Item_Desc),
                new SqlParameter("@PSO_Item_Id", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "PSO_Item_Id", DataRowVersion.Current, this.PSO_Item_Id),
                new SqlParameter("@Parent_PSO_Item_Id", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Parent_PSO_Item_Id", DataRowVersion.Current, this.Parent_PSO_Item_Id)


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

                    //this.WONo = (string)colParams.First().Value;
                    //this.WO_ID = (int)colParams[1].Value;
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
        public bool insertSOWItem(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insertSOWItem(SqlConn, null, enumSpName);
        }

        public bool insertSOWItem(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return insertSOWItem(null, SqlTran, enumSpName);
        }

        private bool insertSOWItem(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
                new SqlParameter("@PSO_ID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "PSO_ID", DataRowVersion.Current, this.PSO_ID),
                new SqlParameter("@Scope_Of_Work", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Scope_Of_Work", DataRowVersion.Current, this.Scope_Of_Work),

                 new SqlParameter("@No_of_Staff_to_be_Deputed", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "No_of_Staff_to_be_Deputed", DataRowVersion.Current, this.No_of_Staff_to_be_Deputed),
                  new SqlParameter("@Tentavie_Time", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "Tentavie_Time", DataRowVersion.Current, this.Tentavie_Time),

                  new SqlParameter("@Amount", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Amount", DataRowVersion.Current, this.Amount),
                new SqlParameter("@PSONo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "PSONo", DataRowVersion.Current, this.PSONo),



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

                    //this.WONo = (string)colParams.First().Value;
                    //this.WO_ID = (int)colParams[1].Value;
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
        public bool updatePSOItemQty(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updatePSOItemQty(SqlConn, null, enumSpName);
        }
        private bool updatePSOItemQty(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {               
               //new SqlParameter("@WO_Item_Id", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "WO_Item_Id", DataRowVersion.Current, this.WO_Item_Id),
               //new SqlParameter("@QtyRequired", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Qty_required", DataRowVersion.Current, this.Qty_required),
               //new SqlParameter("@WOID", this.WO_ID),
               //new SqlParameter("@VendorID",this.SubContractorID),
               //new SqlParameter("@Flag",this.Flag),
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
        private bool updatePSOOverallAmount(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = getSpParamArray(enumSpName);

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

        public bool updatePSOOverallAmount(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updatePSOOverallAmount(SqlConn, null, enumSpName);
        }
        public bool update(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return update(SqlConn, null, enumSpName);
        }

        public bool update(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return update(null, SqlTran, enumSpName);
        }

        private bool update(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@PSONo", SqlDbType.VarChar, 50, ParameterDirection.InputOutput, false, 0, 0, "PSONo", DataRowVersion.Current, this.PSONo),
                new SqlParameter("@PSODate", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "PSODate", DataRowVersion.Current, this.PSODate),
                new SqlParameter("@PSOTypeID", SqlDbType.Int,4, ParameterDirection.Input, false, 0, 0, "PSOTypeID", DataRowVersion.Current, this.PSOTypeID),
                new SqlParameter("@SubContractorID", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "SubContractorID", DataRowVersion.Current, this.SubContractorID),
                new SqlParameter("@WorkLocation", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "WorkLocation", DataRowVersion.Current, this.WorkLocation),
                new SqlParameter("@DurationOfWork", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "DurationOfWork", DataRowVersion.Current, this.DurationOfWork),
                new SqlParameter("@PaymentTerms", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "PaymentTerms", DataRowVersion.Current, this.PaymentTerms),
                new SqlParameter("@GST_No", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "GST_No", DataRowVersion.Current, this.GST_No),
                new SqlParameter("@ApprovedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "ApprovedBy", DataRowVersion.Current, this.ApprovedBy),
                new SqlParameter("@Status", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "Status", DataRowVersion.Current, this.Status),
                new SqlParameter("@Remarks", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "Remarks", DataRowVersion.Current, this.Remarks),
                new SqlParameter("@Others", SqlDbType.VarChar,2000, ParameterDirection.Input, false, 10, 0, "Others", DataRowVersion.Current, this.Others),
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
                new SqlParameter("@Prepared_By", SqlDbType.Int,4, ParameterDirection.Input, false,0, 0, "Prepared_By", DataRowVersion.Current, this.UserID),
                new SqlParameter("@ContactName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "ContactName", DataRowVersion.Current, this.ContactName),
                new SqlParameter("@ContactNo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "ContactNo", DataRowVersion.Current, this.ContactNo),
                new SqlParameter("@ScopeOfWork", SqlDbType.VarChar, 5000, ParameterDirection.Input, false, 10, 0, "ScopeOfWork", DataRowVersion.Current, this.ScopeOfWork),
                new SqlParameter("@TDSPerc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "TDSPerc", DataRowVersion.Current, this.TDSPerc),
                new SqlParameter("@Uploaded_File", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Uploaded_File", DataRowVersion.Current, this.Uploaded_File),
                new SqlParameter("@FYear", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "FYear", DataRowVersion.Current, this.FYear),
                new SqlParameter("@Month", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Month", DataRowVersion.Current, this.Month)
            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                    this.PSONo = (string)colParams.First().Value;
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
        public bool updateItem(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updateItem(SqlConn, null, enumSpName);
        }

        public bool updateItem(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return updateItem(null, SqlTran, enumSpName);
        }

        private bool updateItem(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
                new SqlParameter("@PSONo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "PSONo", DataRowVersion.Current, this.PSONo),
                new SqlParameter("@Item_Type", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "Item_Type", DataRowVersion.Current, this.Item_Type),
                new SqlParameter("@Item_Desc", SqlDbType.VarChar, 6000, ParameterDirection.Input, false, 0, 0, "Item_Desc", DataRowVersion.Current, this.Item_Desc),
                new SqlParameter("@Item_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Item_Perc", DataRowVersion.Current, this.Item_Perc),
                new SqlParameter("@Rate", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Rate", DataRowVersion.Current, this.Item_Rate),
                new SqlParameter("@UOM", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "UOM", DataRowVersion.Current, this.Item_UOM),
                new SqlParameter("@Item_Qty", SqlDbType.Decimal, 10, ParameterDirection.Input, false, 10, 0, "Item_Qty", DataRowVersion.Current, this.Item_Qty),
                new SqlParameter("@Total_Amt", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Total_Amt", DataRowVersion.Current, this.Item_Amt),
                new SqlParameter("@Gst_Exist", SqlDbType.Bit, 9, ParameterDirection.Input, false, 10, 0, "Gst_Exist", DataRowVersion.Current, this.Gst_Exist),
                new SqlParameter("@Igst_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Igst_Perc", DataRowVersion.Current, this.Igst_Perc),
                new SqlParameter("@Cgst_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Cgst_Perc", DataRowVersion.Current, this.Cgst_Perc),
                new SqlParameter("@Sgst_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Sgst_Perc", DataRowVersion.Current, this.Sgst_Perc)

            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                    //this.WONo = (string)colParams.First().Value;
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
        public ProfessionalServiceOrderBL()
        {
            //Set default values
        }



    }
}
