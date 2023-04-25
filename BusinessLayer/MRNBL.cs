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
    public class MRNBL
    {




        #region "Class: MRNBL Local Declarations"

        public enum eLoadSp
        {
            SELECT_ALL = 0,
            INSERT = 1,
            UPDATE = 2,
            SELECT_BY_ID = 3,
            SELECT_MRNLIST = 4,
            PROC_IndentDate_BY_IndentNo = 5,
            SELECT_IndentDate_BY_IndentNo = 6,
            SELECT_PROC_PODate_BY_PONo = 7,
            SELECT_MRN_ITEM_BY_MATERIAL_ITEM_ID = 8,
            PROC_SELECT_PO_INDENTITEMS = 9,
            PROC_SELECT_PONO_ITEMS = 10,
            SELECT_MRNITEMS_BY_MRNNO = 11,
            SELECT_INDENTITEMS_FOR_MRN = 12,
            UPDATE_MRNITEM_BY_ID = 13,
            DELETE_MRNITEM_BY_ID = 14,
            INSERT_MRN_ITEM_TO_STOCK = 15,
            SELECT_INDENTS_FOR_MRN = 16,
            PROC_SELECT_INDENTITEMS = 17,
            UPDATE_TRANSPORTATION_COST = 18,
            SELECT_MRNLIST_ALL_BY_PROJECT = 19,
            Update_Tally_Status = 20,
            Tally_AutoMation_flag = 21,
            SELECT_Transport_Amt = 22,
            SELECT_Sector_Amt = 23,
            //INSERT_LedgerHead = 24,
            //UPDATE_PROJECT_TYPE_BY_ID = 25,
            //DELETE_PROJECTTYPE = 26,
            //SELECT_PROJECTTYPE_ALL = 27,
            //SELECT_PROJECT_BY_PROJECT_ID = 28,
            SELECT_LEDGER_HEAD = 24,
            INSERT_LEDGER_HEAD = 25,
            UPDATE_LEDGER_HEAD = 26,
            DELETE_LEDGER_HEAD = 27,
            UPDATE_MRNITEM_Local_BY_ID = 29,
            SELECT_ALL_PO_BY_VENDOR_ID = 30,
            SELECT_INDENT_BY_VENDOR_ID = 31,
            SELECT_ALL_WO_BY_SUBCON_ID = 32,
            SELECT_CONTRACTOR_ALL = 33,
            PROC_UPDATE_MRN_BY_ID = 34,
            SELECT_WO_ITEMS_BY_WONO = 35,
            PROC_SERVICE_MRN_SELECT_PONO_ITEMS = 36,
            SELECT_MRN_MATERIAL_ITEM_ID = 37,
            INSERT_LOCAL_MRN = 38,
            SELECT_SERVICEMRN_LIST = 39,
            INSERT_SERVICE_MRN = 40,
            SELECT_All_SERVICE_MRN_ITEMS = 41,
            SRN_SELECT_BY_ID = 42,
        };

        #endregion


        #region "Class: MRNBL Sets / Gets"

        public int UID { get; set; }

        public string PONo { get; set; }
        public int PO_ID { get; set; }
        public string WONo { get; set; }
        public int WO_ID { get; set; }
        public string UserID { get; set; }
        public int Material_Item_Id { get; set; }
        public string Task { get; set; }
        public string MRN_No { get; set; }
        public DateTime Date { get; set; }
        public DateTime Bill_Date { get; set; }
        public string Project_Code { get; set; }
        public string Vendor_Id { get; set; }
        public string SubContractor_Id { get; set; }
        public string Indent_No { get; set; }
        public string Service_MRN_PO_NO { get; set; }
        public string Invoice_No { get; set; }
        public string Payment_Terms { get; set; }
        public string Remarks { get; set; }
        public int Mat_cat_ID { get; set; }
        public string Item_Code { get; set; }

        public decimal Indent_Qty { get; set; }
        public decimal PO_Qty { get; set; }
        public decimal Received_Qty { get; set; }
        public decimal Accepted_Qty { get; set; }
        public decimal Rejected_Qty { get; set; }
        public decimal Pending_Qty { get; set; }

        public decimal Total_Price { get; set; }

        public decimal PricePerUnit { get; set; }
        public decimal Discount_Amount { get; set; }
        public decimal TaxPercent_MRN { get; set; }
        public decimal Total_TaxAmount { get; set; }
        public decimal percentComplete { get; set; }
        public decimal Net_MRN_Total { get; set; }

        public decimal TransportCost { get; set; }
        public decimal? Item_TransportCost { get; set; }
        public string Ledger_Head { get; set; }
        public string Ledger_Head_Old { get; set; }
        public int? entered { get; set; }
        public int Proj_Type_ID { get; set; }
        public decimal Price_Local_MRN { get; set; }

        public decimal TaxPrcent { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal DisPrcent { get; set; }
        public decimal Disamt { get; set; }
        public decimal invoiceAmt { get; set; }
        public decimal Amt { get; set; }
        public string MRN_Type { get; set; }
        public string File_Invoice { get; set; }
        public string File_WayBill { get; set; }
        public string File_Other { get; set; }
        public bool Price_fluctuations { get; set; }//by prashanth


        // Service MRN Items

        public string WO_Description { get; set; }
        public string Net_Payable_ServiceMRN { get; set; }
        public string SRN_No { get; set; }

        #endregion

        #region "Class: Category Methods"

        /// <summary>
        /// Function Name:  fillCollectionFromDr.
        /// Called By:      Nill.
        /// Description:    Fill the data from data reader. 
        /// Change history: Nill.
        /// </summary>

        private bool fillCollectionFromDr(SqlDataReader dr, ref Dictionary<int, MRNBL> dicCountry)
        {
            if (dicCountry == null)
            {
                dicCountry = new Dictionary<int, MRNBL>();
            }

            try
            {
                // Loop though the data reader
                while (dr.Read())
                {
                    //clsCountry.CountryName
                    //if (dr["Category_Name"] != DBNull.Value)
                    //{
                    //    this.Category_Name = dr["Category_Name"].ToString();
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
                    //if (dr["Category_Name"] != DBNull.Value)
                    //{
                    //    this.Category_Name = dr["Category_Name"].ToString();
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
                case eLoadSp.SELECT_ALL:
                    return "PRO_MRN_SELECT_ALL";
                case eLoadSp.INSERT:
                    return "PRO_MRN_INSERT";
                case eLoadSp.INSERT_LOCAL_MRN:
                    return "PRO_MRN_INSERT_LOCAL";
                case eLoadSp.INSERT_SERVICE_MRN:
                    return "PRO_INSERT_SERVICE_MRN";
                case eLoadSp.UPDATE:
                    return "PRO_MRN_UPDATE";
                case eLoadSp.SELECT_BY_ID:
                    return "PRO_MRN_SELECT_BY_MRN_NO";
                case eLoadSp.SRN_SELECT_BY_ID:
                    return "PRO_SRN_SELECT_BY_ID";
                case eLoadSp.SELECT_MRNLIST:
                    return "PRO_MRNLIST";
                case eLoadSp.SELECT_SERVICEMRN_LIST:
                    return "PRO_SELECT_SERVICEMRN_LIST";
                case eLoadSp.SELECT_IndentDate_BY_IndentNo:
                    return "PROC_IndentDate_BY_IndentNo";
                case eLoadSp.SELECT_PROC_PODate_BY_PONo:
                    return "PROC_PODate_BY_PONo";
                case eLoadSp.SELECT_MRN_ITEM_BY_MATERIAL_ITEM_ID:
                    return "PRO_MRN_ITEM_SELECT_BY_MATERIAL_ITEM_ID";
                case eLoadSp.SELECT_MRN_MATERIAL_ITEM_ID:
                    return "PRO_MRN_ITEM_SELECT_BY_PO_NO";
                case eLoadSp.PROC_SELECT_PO_INDENTITEMS:
                    return "PROC_SELECT_PO_INDENTITEMS";
                case eLoadSp.PROC_SELECT_PONO_ITEMS:
                    return "PROC_SELECT_PONO_ITEMS";
                case eLoadSp.SELECT_INDENTITEMS_FOR_MRN:
                    return "PROC_SELECT_INDENTITEMS_FOR_MRN";
                case eLoadSp.SELECT_MRNITEMS_BY_MRNNO:
                    return "PROC_MRN_ITEMS_SELECT_BY_MRNNO";
                case eLoadSp.PROC_UPDATE_MRN_BY_ID:
                    return "PROC_UPDATE_MRN_BY_ID";
                case eLoadSp.UPDATE_MRNITEM_Local_BY_ID:
                    return "PROC_UPDATE_MRN_Local_ITEM_BY_ID";
                case eLoadSp.DELETE_MRNITEM_BY_ID:
                    return "PROC_DELETE_MRN_ITEM_BY_ID";
                case eLoadSp.INSERT_MRN_ITEM_TO_STOCK:
                    return "PROC_INSERT_MRN_ITEM_TO_STOCK";
                case eLoadSp.SELECT_INDENTS_FOR_MRN:
                    return "PROC_SELECT_INDENT_FOR_MRN";
                case eLoadSp.PROC_SELECT_INDENTITEMS:
                    return "PROC_SELECT_INDENTITEMS";
                case eLoadSp.UPDATE_TRANSPORTATION_COST:
                    return "PROC_UPDATE_TRANSPORATION_COST_BY_MRN_NO";
                case eLoadSp.SELECT_MRNLIST_ALL_BY_PROJECT:
                    return "PROC_MRN_ITEMS_SELECT_ALL_By_PROJECT";
                case eLoadSp.Update_Tally_Status:
                    return "PROC_Update_Tally_Status";
                case eLoadSp.Tally_AutoMation_flag:
                    return "PROC_Tally_AutoMation_flag";
                case eLoadSp.SELECT_Transport_Amt:
                    return "Proc_SELECT_Transport_Amt";
                case eLoadSp.SELECT_Sector_Amt:
                    return "Proc_SELECT_Sector_Amt";
                //case eLoadSp.INSERT_LedgerHead:
                //    return "PRO_Ledger_INSERT";
                //case eLoadSp.UPDATE_PROJECT_TYPE_BY_ID:
                //    return "PROC_Ledger_UPDATE";
                //case eLoadSp.DELETE_PROJECTTYPE:
                //    return "PROC_DELETE_Ledger";
                //case eLoadSp.SELECT_PROJECTTYPE_ALL:
                //    return "PRO_SELECT_Ledger_ALL";
                //case eLoadSp.SELECT_PROJECT_BY_PROJECT_ID:
                //    return "PROC_Ledger_SELECTBYID";
                case eLoadSp.SELECT_LEDGER_HEAD:
                case eLoadSp.INSERT_LEDGER_HEAD:
                case eLoadSp.UPDATE_LEDGER_HEAD:
                case eLoadSp.DELETE_LEDGER_HEAD:
                    return "PROC_LEDGER_HEAD";
                case eLoadSp.SELECT_All_SERVICE_MRN_ITEMS:
                    return "PROC_SELECT_All_SERVICE_MRN_ITEMS";
                case eLoadSp.SELECT_ALL_PO_BY_VENDOR_ID:
                case eLoadSp.SELECT_ALL_WO_BY_SUBCON_ID:
                case eLoadSp.SELECT_INDENT_BY_VENDOR_ID:
                    return "PROC_SELECT_INDENT_FOR_MRN";
                case eLoadSp.SELECT_CONTRACTOR_ALL:
                    return "PRO_contractor_SELECT_ALL";

                case eLoadSp.SELECT_WO_ITEMS_BY_WONO:
                    return "PROC_WO_SELECT_ALL";
                case eLoadSp.PROC_SERVICE_MRN_SELECT_PONO_ITEMS:
                    return "PROC_WO_SELECT_ALL";



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
                case eLoadSp.SELECT_LEDGER_HEAD:
                case eLoadSp.UPDATE_LEDGER_HEAD:
                case eLoadSp.DELETE_LEDGER_HEAD:
                    colParams = new SqlParameter[]
				    {
				        new SqlParameter("@Project_Code", this.Project_Code),
                        new SqlParameter("@Ledger_Head", this.Ledger_Head),
                        new SqlParameter("@Ledger_Head_Old", this.Ledger_Head_Old),
                        new SqlParameter("@Task", this.Task)
				    };
                    break;
                case eLoadSp.SELECT_All_SERVICE_MRN_ITEMS:
                    colParams = new SqlParameter[]
				    {
				        new SqlParameter("@SRN_No", this.SRN_No),
				    };
                    break;

                case eLoadSp.SRN_SELECT_BY_ID:
                    colParams = new SqlParameter[]
				    {
				        new SqlParameter("@MRN_No", this.MRN_No),
				    };
                    break;
                case eLoadSp.SELECT_BY_ID:
                    colParams = new SqlParameter[]
				    {
				        new SqlParameter("@MRN_No", this.MRN_No),
				    };
                    break;
                case eLoadSp.SELECT_IndentDate_BY_IndentNo:
                case eLoadSp.PROC_SELECT_INDENTITEMS:
                    colParams = new[]
                    {
                        new SqlParameter("@Indent_No",this.Indent_No),
                    };
                    break;
                case eLoadSp.SELECT_PROC_PODate_BY_PONo:
                    colParams = new[]
                    {
                        new SqlParameter("@PONo",this.PONo),
                    };
                    break;
                case eLoadSp.SELECT_MRN_ITEM_BY_MATERIAL_ITEM_ID:
                    colParams = new[]
                    {
                        new SqlParameter("@Material_Item_Id",this.Material_Item_Id),
                        new SqlParameter("@PONo",this.PONo)
                    };
                    break;

                case eLoadSp.PROC_SELECT_PO_INDENTITEMS:
                    colParams = new[]
                    {
                         new SqlParameter("@Indent_No",this.Indent_No)
                    };
                    break;

                case eLoadSp.PROC_SELECT_PONO_ITEMS:
                    colParams = new[]
                    {
                         new SqlParameter("@PONo",this.PONo)
                    };
                    break;
                case eLoadSp.SELECT_MRN_MATERIAL_ITEM_ID:
                    colParams = new[]
                    {
                         new SqlParameter("@PONo",this.PONo)
                    };
                    break;
                case eLoadSp.SELECT_MRNITEMS_BY_MRNNO:
                    colParams = new[]
                    {
                         new SqlParameter("@MRNNo",this.MRN_No),
                         new SqlParameter("@PO_No",this.PONo)
                    };
                    break;
                case eLoadSp.SELECT_WO_ITEMS_BY_WONO:
                    colParams = new SqlParameter[]
                    {
                    new SqlParameter("@WOID", this.WO_ID),
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@WONo",this.WONo)
                    };
                    break;
                case eLoadSp.SELECT_INDENTITEMS_FOR_MRN:
                    colParams = new[]
                    {
                         new SqlParameter("@Indent_No",this.Indent_No)
                    };
                    break;
                case eLoadSp.DELETE_MRNITEM_BY_ID:
                    colParams = new[]
                    {
                        new SqlParameter("@Material_Item_Id",this.Material_Item_Id),
                        new SqlParameter("@Project_Code",this.Project_Code)
                    };
                    break;
                case eLoadSp.SELECT_MRNLIST:
                    colParams = new[]
                    {
                         new SqlParameter("@Project_Code",this.Project_Code)
                    };
                    break;
                case eLoadSp.SELECT_SERVICEMRN_LIST:
                    colParams = new[]
                    {
                         new SqlParameter("@Project_Code",this.Project_Code)
                    };
                    break;
                case eLoadSp.SELECT_INDENTS_FOR_MRN:
                    colParams = new[]
                    {
                        new SqlParameter("@Project_Code",this.Project_Code)
                    };
                    break;
                case eLoadSp.SELECT_MRNLIST_ALL_BY_PROJECT:
                    colParams = new[]
                    {
                        new SqlParameter("@Project_Code",this.Project_Code)
                    };
                    break;

                case eLoadSp.SELECT_Transport_Amt:
                    colParams = new[]
                    {
                         new SqlParameter("@MRN_No",this.MRN_No)
                    };
                    break;

                case eLoadSp.SELECT_Sector_Amt:
                    colParams = new[]
                    {
                        new SqlParameter("@Category_ID",this.Mat_cat_ID),
                        new SqlParameter("@MRN_Date",this.Date),
                        new SqlParameter("@Project_Code",this.Project_Code),
                        new SqlParameter("@Amt",this.Amt),
                        new SqlParameter("@TaxPrcent",this.TaxPrcent),
                        new SqlParameter("@TaxAmt",this.TaxAmt),
                        new SqlParameter("@DisPrcent",this.DisPrcent),
                        new SqlParameter("@DisAmt",this.Disamt),
                        new SqlParameter("@TransportAmt",this.Item_TransportCost),
                        new SqlParameter("@MRN_No", this.MRN_No),
                        new SqlParameter("@Material_Item_Id",this.Material_Item_Id),
                    };
                    break;

                case eLoadSp.SELECT_ALL_PO_BY_VENDOR_ID:
                case eLoadSp.SELECT_INDENT_BY_VENDOR_ID:
                case eLoadSp.SELECT_ALL_WO_BY_SUBCON_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@Project_Code",this.Project_Code),
                    new SqlParameter("@Vendor_ID",this.Vendor_Id),
                    new SqlParameter("@SubCon_ID",this.SubContractor_Id),
                };
                    break;
            }
            return colParams;
        }
        public bool insert_ServiceMRN_Items(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insert_ServiceMRN_Items(SqlConn, null, enumSpName);
        }
        public bool insert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insert(SqlConn, null, enumSpName);
        }

        public bool insert(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return insert(null, SqlTran, enumSpName);
        }
        private bool insert_ServiceMRN_Items(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;
                SqlParameter[] colParams = new SqlParameter[]
			{
                new SqlParameter("@SRN_No",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"SRN_No",DataRowVersion.Current,this.SRN_No),
                new SqlParameter("@Net_Payable",SqlDbType.Decimal,30,ParameterDirection.Input,false,0,0,"Net_Payable",DataRowVersion.Current,this.Net_Payable_ServiceMRN),
                new SqlParameter("@Wo_Description",SqlDbType.VarChar,8000,ParameterDirection.Input,false,0,0,"Wo_Description",DataRowVersion.Current,this.WO_Description),
                
                
            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));

                    this.MRN_No = (string)colParams.First().Value;
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
        private bool insert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;
                SqlParameter[] colParams = new SqlParameter[]
			{
                new SqlParameter("@MRN_No",SqlDbType.VarChar,100,ParameterDirection.Output,false,0,0,"MRN_No",DataRowVersion.Current,this.MRN_No),
                new SqlParameter("@Date",SqlDbType.Date,3,ParameterDirection.Input,false,0,0,"Date",DataRowVersion.Current,this.Date),
                new SqlParameter("@Bill_Date",SqlDbType.Date,3,ParameterDirection.Input,false,0,0,"Bill_Date",DataRowVersion.Current,this.Bill_Date),
                new SqlParameter("@Vendor_Id",SqlDbType.VarChar,8,ParameterDirection.Input,false,0,0,"Vendor_Id",DataRowVersion.Current,this.Vendor_Id),
                new SqlParameter("@SubCon_Id",SqlDbType.VarChar,8,ParameterDirection.Input,false,0,0,"Vendor_Id",DataRowVersion.Current,this.SubContractor_Id),
                new SqlParameter("@Indent_No",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Indent_No",DataRowVersion.Current,this.Indent_No ?? (object)DBNull.Value),
                new SqlParameter("@PO_No",SqlDbType.VarChar,30,ParameterDirection.Input,false,0,0,"PONo",DataRowVersion.Current,this.PONo ?? (object)DBNull.Value),
                new SqlParameter("@WO_No",SqlDbType.VarChar,30,ParameterDirection.Input,false,0,0,"WONo",DataRowVersion.Current,this.WONo ?? (object)DBNull.Value),
                new SqlParameter("@Invoice_No",SqlDbType.VarChar,30,ParameterDirection.Input,false,0,0,"Invoice_No",DataRowVersion.Current,this.Invoice_No ?? (object)DBNull.Value),
                
                new SqlParameter("@Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Remarks",DataRowVersion.Current,this.Remarks ?? (object)DBNull.Value),
                new SqlParameter("@UserID",SqlDbType.VarChar,30,ParameterDirection.Input,false,0,0,"UserID",DataRowVersion.Current,this.UserID),
                new SqlParameter("@TransportCost",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"TransportCost",DataRowVersion.Current,this.TransportCost),
 
                new SqlParameter("@Ledger_Head",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Ledger_Head",DataRowVersion.Current,this.Ledger_Head ?? (object)DBNull.Value),
                new SqlParameter("@MRN_Type",SqlDbType.VarChar,500,ParameterDirection.Input,false,0,0,"MRN_Type",DataRowVersion.Current,this.MRN_Type),
                new SqlParameter("@File_Invoice",SqlDbType.VarChar,500,ParameterDirection.Input,false,0,0,"File_Invoice",DataRowVersion.Current,this.File_Invoice ?? (object)DBNull.Value),
                new SqlParameter("@File_WayBill",SqlDbType.VarChar,500,ParameterDirection.Input,false,0,0,"File_WayBill",DataRowVersion.Current,this.File_WayBill ?? (object)DBNull.Value),
                new SqlParameter("@File_Other",SqlDbType.VarChar,500,ParameterDirection.Input,false,0,0,"File_Other",DataRowVersion.Current,this.File_Other ?? (object)DBNull.Value),
                new SqlParameter("@Task",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Task",DataRowVersion.Current,this.Task ?? (object)DBNull.Value),
            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));

                    this.MRN_No = (string)colParams.First().Value;
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


        public bool insertMRNItemToStock(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insertMRNItemToStock(SqlConn, null, enumSpName);
        }

        private bool insertMRNItemToStock(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;
                SqlParameter[] colParams = new SqlParameter[]
			{
                new SqlParameter("@MRNNo",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"MRN_No",DataRowVersion.Current,this.MRN_No),             
                new SqlParameter("@MatCatID",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Mat_cat_ID",DataRowVersion.Current,this.Mat_cat_ID),
               new SqlParameter("@ItemCode",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Item_Code",DataRowVersion.Current,this.Item_Code)
 		
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

        public bool load(SqlConnection SqlConn, eLoadSp enumSpName, ref Dictionary<int, MRNBL> diclCountry)
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

        public bool UpdateMRNTransportationCost(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return UpdateMRNTransportationCost(SqlConn, null, enumSpName);
        }

        private bool UpdateMRNTransportationCost(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
                
                new SqlParameter("@MRNNo",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"MRN_No",DataRowVersion.Current,this.MRN_No),
                new SqlParameter("@TransportCost",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"TransportCost",DataRowVersion.Current,this.TransportCost)
                  
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

        public bool UpdateMRNLocalItem(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return UpdateMRNLocalItem(SqlConn, null, enumSpName);
        }

        private bool UpdateMRNLocalItem(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
                new SqlParameter("@MRNItemID",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Material_Item_Id",DataRowVersion.Current,this.Material_Item_Id),
                new SqlParameter("@Received_Qty",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Received_Qty",DataRowVersion.Current,this.Received_Qty),
                new SqlParameter("@Accepted_Qty",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Accepted_Qty",DataRowVersion.Current,this.Accepted_Qty),
                new SqlParameter("@Rejected_Qty",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Rejected_Qty",DataRowVersion.Current,this.Rejected_Qty),
                new SqlParameter("@Pending_Qty",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Pending_Qty",DataRowVersion.Current,this.Pending_Qty),
			    new SqlParameter("@UserID",SqlDbType.VarChar,30,ParameterDirection.Input,false,0,0,"Pending_Qty",DataRowVersion.Current,this.UserID),
                new SqlParameter("@Item_TransportCost",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Item_TransportCost",DataRowVersion.Current,this.Item_TransportCost ?? (object)DBNull.Value),
                new SqlParameter("@Price_Local_MRN",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Price_Local_MRN",DataRowVersion.Current,this.Price_Local_MRN),

                new SqlParameter("@TaxPrcent",this.TaxPrcent),
                new SqlParameter("@TaxAmt",this.TaxAmt),
                new SqlParameter("@DisPrcent",this.DisPrcent),
                new SqlParameter("@DisAmt",this.Disamt),
                new SqlParameter("@Amount",this.Amt),
                new SqlParameter("@InvoiceAmt",this.invoiceAmt)
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

        public bool UpdateMRNItem(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return UpdateMRNItem(SqlConn, null, enumSpName);
        }

        private bool UpdateMRNItem(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;
                SqlParameter[] colParams = new SqlParameter[]
			{
                
                new SqlParameter("@MRNItemID",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Material_Item_Id",DataRowVersion.Current,this.Material_Item_Id),
                new SqlParameter("@Received_Qty",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Received_Qty",DataRowVersion.Current,this.Received_Qty),
                new SqlParameter("@Accepted_Qty",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Accepted_Qty",DataRowVersion.Current,this.Accepted_Qty),
                new SqlParameter("@Rejected_Qty",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Rejected_Qty",DataRowVersion.Current,this.Rejected_Qty),
                new SqlParameter("@Pending_Qty",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Pending_Qty",DataRowVersion.Current,this.Pending_Qty),
                new SqlParameter("@Item_TransportCost",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Item_TransportCost",DataRowVersion.Current,this.Item_TransportCost ?? (object)DBNull.Value),
                new SqlParameter("@Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Amount",DataRowVersion.Current,this.Total_Price),
                new SqlParameter("@TotalTax",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"TotalTax",DataRowVersion.Current,this.TaxPercent_MRN),
                new SqlParameter("@TaxAmt",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"TaxAmt",DataRowVersion.Current,this.Total_TaxAmount),
                new SqlParameter("@DiscountAmt",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"DiscountAmt",DataRowVersion.Current,this.Discount_Amount),
                new SqlParameter("@ProicePerUnit",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"ProicePerUnit",DataRowVersion.Current,this.PricePerUnit),
                new SqlParameter("@InvoiceAmt",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"InvoiceAmt",DataRowVersion.Current,this.Net_MRN_Total),
                new SqlParameter("@ItemCode",SqlDbType.VarChar,50,ParameterDirection.Input,false,0,0,"ItemCode",DataRowVersion.Current,this.Item_Code),
                 new SqlParameter("@MRN_No",SqlDbType.VarChar,50,ParameterDirection.Input,false,0,0,"MRN_No",DataRowVersion.Current,this.MRN_No),

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
                 
                new SqlParameter("@MRN_No",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"@MRN_No",DataRowVersion.Current,this.MRN_No),
                new SqlParameter("@Date",SqlDbType.Date,3,ParameterDirection.Input,false,0,0,"Date",DataRowVersion.Current,this.Date),
                new SqlParameter("@Bill_Date",SqlDbType.Date,3,ParameterDirection.Input,false,0,0,"Bill_Date",DataRowVersion.Current,this.Bill_Date),
                new SqlParameter("@Vendor_Id",SqlDbType.VarChar,8,ParameterDirection.Input,false,0,0,"Vendor_Id",DataRowVersion.Current,this.Vendor_Id),
                new SqlParameter("@SubCon_Id",SqlDbType.VarChar,8,ParameterDirection.Input,false,0,0,"Vendor_Id",DataRowVersion.Current,this.SubContractor_Id),
                new SqlParameter("@Indent_No",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Indent_No",DataRowVersion.Current,this.Indent_No ?? (object)DBNull.Value),
                new SqlParameter("@PO_No",SqlDbType.VarChar,30,ParameterDirection.Input,false,0,0,"PONo",DataRowVersion.Current,this.PONo ?? (object)DBNull.Value),
                new SqlParameter("@WO_No",SqlDbType.VarChar,30,ParameterDirection.Input,false,0,0,"WONo",DataRowVersion.Current,this.WONo ?? (object)DBNull.Value),
                new SqlParameter("@Invoice_No",SqlDbType.VarChar,30,ParameterDirection.Input,false,0,0,"Invoice_No",DataRowVersion.Current,this.Invoice_No ?? (object)DBNull.Value),
                
                new SqlParameter("@Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Remarks",DataRowVersion.Current,this.Remarks ?? (object)DBNull.Value),
                new SqlParameter("@UserID",SqlDbType.VarChar,30,ParameterDirection.Input,false,0,0,"UserID",DataRowVersion.Current,this.UserID),
                new SqlParameter("@TransportCost",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"TransportCost",DataRowVersion.Current,this.TransportCost),
 
                new SqlParameter("@Ledger_Head",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Ledger_Head",DataRowVersion.Current,this.Ledger_Head ?? (object)DBNull.Value),
                new SqlParameter("@MRN_Type",SqlDbType.VarChar,50,ParameterDirection.Input,false,0,0,"MRN_Type",DataRowVersion.Current,this.MRN_Type),
                new SqlParameter("@File_Invoice",SqlDbType.VarChar,50,ParameterDirection.Input,false,0,0,"File_Invoice",DataRowVersion.Current,this.File_Invoice ?? (object)DBNull.Value),
                new SqlParameter("@File_WayBill",SqlDbType.VarChar,50,ParameterDirection.Input,false,0,0,"File_WayBill",DataRowVersion.Current,this.File_WayBill ?? (object)DBNull.Value),
                new SqlParameter("@File_Other",SqlDbType.VarChar,50,ParameterDirection.Input,false,0,0,"File_Other",DataRowVersion.Current,this.File_Other ?? (object)DBNull.Value),
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

        public bool updateTallyStatus(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updateTallyStatus(SqlConn, null, enumSpName);
        }

        public bool updateTallyStatus(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return updateTallyStatus(null, SqlTran, enumSpName);
        }

        private bool updateTallyStatus(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
                 
                new SqlParameter("@MRN_No",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"MRN_No",DataRowVersion.Current,this.MRN_No),
              new SqlParameter("@Entered", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Entered", DataRowVersion.Current, this.entered),

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


        public bool AutoMationTallyStatus(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return AutoMationTallyStatus(SqlConn, null, enumSpName);
        }

        public bool AutoMationTallyStatus(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return AutoMationTallyStatus(null, SqlTran, enumSpName);
        }

        private bool AutoMationTallyStatus(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
			    {
                    new SqlParameter("@MRN_No",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"MRN_No",DataRowVersion.Current,this.MRN_No),
                    new SqlParameter("@Entered", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Entered", DataRowVersion.Current, this.entered),
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

        public MRNBL()
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

        public bool insertLedgerHead(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insertLedgerHead(SqlConn, null, enumSpName);
        }

        private bool insertLedgerHead(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;
                SqlParameter[] colParams = new SqlParameter[]
			{
				new SqlParameter("@Ledger_Head", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Ledger_Head", DataRowVersion.Current, this.Ledger_Head),
				new SqlParameter("@Project_Code", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code),
                new SqlParameter("@Task", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task),
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

        public bool updateLedgerHead(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updateLedgerHead(SqlConn, null, enumSpName);
        }

        private bool updateLedgerHead(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
				new SqlParameter("@Ledger_Head", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Ledger_Head", DataRowVersion.Current, this.Ledger_Head),
				new SqlParameter("@Ledger_Head_Old", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Ledger_Head", DataRowVersion.Current, this.Ledger_Head_Old),
                new SqlParameter("@Task", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Task", DataRowVersion.Current, this.Task),
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

        #endregion


    }


}
