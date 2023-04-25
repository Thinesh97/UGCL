using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DataAccess;


namespace SNC.ErrorLogger
{
    public class ErrorLogger
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        
        /// <summary>
        /// Error Categories matching App.Config entries. Edit this list after editing App.config
        /// </summary>
        public enum enumErrorTypes
        {
            AppLogicError = 1,
            DBConnectionError = 2,
            DBSelectError = 3,
            DBInsertError = 4,
            DBUpdateError = 5,
            DBDeleteError = 6,
            FileReadError = 7,
            FileWriteError = 8,
            FileUploadError = 9,
            FileDownloadError = 10,
            ServerUnavailable = 11,
            TallyBridgeError = 12
        };

        /// <summary>
        /// Returns the string representation of enumErrorTypes. The error string values match this dlls
        /// App.config categorySources
        /// </summary>
        /// <param name="errorType">Element of custom created enumErrorTypes</param>
        /// <returns>string representation of error</returns>
        private static string getErrorString(enumErrorTypes errorType)
        {
            string strError = string.Empty;

            switch (errorType)
            {
                case enumErrorTypes.AppLogicError:
                    strError = "AppLogicError";
                    break;
                case enumErrorTypes.DBSelectError:
                    strError = "DBSelectError";
                    break;
                case enumErrorTypes.DBInsertError:
                    strError = "DBInsertError";
                    break;
                case enumErrorTypes.DBUpdateError:
                    strError = "DBUpdateError";
                    break;
                case enumErrorTypes.DBDeleteError:
                    strError = "DBDeleteError";
                    break;
                case enumErrorTypes.FileReadError:
                    strError = "FileReadError";
                    break;
                case enumErrorTypes.FileWriteError:
                    strError = "FileWriteError";
                    break;
                case enumErrorTypes.FileUploadError:
                    strError = "FileUploadError";
                    break;
                case enumErrorTypes.FileDownloadError:
                    strError = "FileDownloadError";
                    break;
                case enumErrorTypes.ServerUnavailable:
                    strError = "ServerUnavailable";
                    break;
                case enumErrorTypes.TallyBridgeError:
                    strError = "TallyBridgeError";
                    break;
                    
                default:
                    strError = "General";
                    break;
            }
            return strError;
        }

        /// <summary>
        /// Logs error entries into the table (Log). 
        /// To use this method, client apps need to refrence System.Collections.Generics
        /// and System.Diagnostics namespaces. Client apps also need to refrence
        /// [enterpriseLibrary.ConfigurationSource selectedSource="File Configuration Source"]
        /// tag.
        /// </summary>
        /// <param name="errorType">Element of custom created enumErrorTypes</param>
        /// <param name="message">Exception Message</param>
        /// <param name="eventID">EventId</param>
        /// <param name="title">Error Title</param>
        /// <param name="priority">Error Priority (1 being highest)</param>
        /// <param name="enumErrorEventType">enum System.Diagnostics.TraceEventType</param>
        /// <param name="valuePairs">IDictionary<string, object> object</param>
        public static void logError(enumErrorTypes errorType,
            Exception ex,           
            string PageName,
            string methodName,
            string EmpID)
        {
            try
            {
                LogError(errorType, ex, PageName, methodName,  EmpID);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Overloaded Method with additional parameter as exception
        /// </summary>
        /// <param name="errorType"></param>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        /// <param name="title"></param>
        /// <param name="priority"></param>
        /// <param name="enumErrorEventType"></param>
        /// <param name="valuePairs"></param>
        /// <param name="exception"></param>
        public static void logError(enumErrorTypes errorType,
            Exception ex,
            string PageName,
            string methodName,
            Exception exception,
            string EmpID)
        {
            try
            {
                if (exception is System.Threading.ThreadAbortException && exception.StackTrace.Contains("Response.End"))
                {
                    // Do Nothing
                }
                else
                {
                    LogError(errorType, ex, PageName, methodName,EmpID);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private static void LogError(enumErrorTypes errorType,
            Exception ex,            
            string PageName,
            string methodName,
            string EmpID
            )
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
            SqlParameter p_TimeStamp = new SqlParameter("@TimeStamp", DateTime.Now);
            SqlParameter p_ErrorType = new SqlParameter("@ErrorType", errorType.ToString());
            SqlParameter p_EmpID = new SqlParameter("@EmpID", EmpID);
            SqlParameter p_PageName = new SqlParameter("@PageName", PageName);
            SqlParameter p_methodName = new SqlParameter("@methodName", methodName);
            SqlParameter p_Message = new SqlParameter("@Message", ex.Message.ToString());
            con.Open();
            SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "Proc_ErrorLog_Insert", p_TimeStamp, p_ErrorType, p_EmpID, p_PageName, p_methodName, p_Message);
            con.Close();
        }
    }
}
                                    
          

            
            

          
       
