using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel;
using Microsoft.Reporting.WebForms;
using System.Reflection;
using BusinessLayer;

namespace SNC.Reports
{
    public partial class UGCL_ReportViewer : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        DataSet Ds;
        DataTable Data;
        DataTable Data2;
        DataTable Data3;
        ReportBL objReportBL = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DisableUnwantedExportFormat(ReportViewer1, "PDF");
                DisableUnwantedExportFormat(ReportViewer1, "WORD");
                Bind_Report();
            }
        }

        public void DisableUnwantedExportFormat(ReportViewer ReportViewerID, string strFormatName)
        {
            FieldInfo info;
            foreach (RenderingExtension extension in ReportViewerID.LocalReport.ListRenderingExtensions())
            {
                if (extension.Name.ToLower().Contains(strFormatName.ToLower()))
                {
                    info = extension.GetType().GetField("m_isVisible", BindingFlags.Instance | BindingFlags.NonPublic);
                    info.SetValue(extension, false);
                }
            }
        }

        protected void Bind_Report()
        {
            objReportBL = new ReportBL();
            if (Session["Report_Flag"] != null && Session["Report_Flag"].ToString() != string.Empty)
            {
                if (Session["Report_Flag"].ToString() == "POWO_Register")
                {
                    objReportBL.Task = "POWO_Register";
                    if (Session["OrderType"] != null)
                    {
                        objReportBL.OrderType = Session["OrderType"].ToString();
                    }
                    if (Session["VendorID"] != null)
                    {
                        objReportBL.VendorID = Session["VendorID"].ToString();
                    }
                    if (Session["SubContractorID"] != null)
                    {
                        objReportBL.SubContractorID = Session["SubContractorID"].ToString();
                    } 
                    if (Session["FromDate"] != null)
                    {
                        objReportBL.From_Date = Convert.ToDateTime(Session["FromDate"].ToString());
                    }
                    if (Session["ToDate"] != null)
                    {
                        objReportBL.To_Date = Convert.ToDateTime(Session["ToDate"].ToString());
                    }
                    if (Session["FromAmount"] != null)
                    {
                        objReportBL.FromAmount = Session["FromAmount"].ToString();
                    }
                    if (Session["ToAmount"] != null)
                    {
                        objReportBL.ToAmount = Session["ToAmount"].ToString();
                    }
                    if (Session["Projects"] != null)
                    {
                        objReportBL.Projects = Session["Projects"].ToString();
                    }
                    objReportBL.Display_Columns = Session["Display_Columns"].ToString();
                    Ds = new DataSet(); Ds.Clear();
                    Data = new DataTable(); Data.Clear();
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Reports/Report_RDLCs/POWORegister.rdlc");
                    objReportBL.load(con, ReportBL.eLoadSp.POWO_Register_Report, ref Ds);
                    Data = Ds.Tables[0];
                    ReportDataSource datasource = new ReportDataSource("DataSet1", Data);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                }
                else
                {
                    if (Session["Report_Flag"].ToString() == "AssetReport")
                    {
                        objReportBL.Task = "AssetReport";

                        if (Session["FromDate"] != null)
                        {
                            objReportBL.From_Date = Convert.ToDateTime(Session["FromDate"].ToString());
                        }
                        if (Session["ToDate"] != null)
                        {
                            objReportBL.To_Date = Convert.ToDateTime(Session["ToDate"].ToString());
                        }
                        if (Session["Projects"] != null)
                        {
                            objReportBL.Projects = Session["Projects"].ToString();
                        }
                        objReportBL.Display_Columns = Session["Display_Columns"].ToString();
                        Ds = new DataSet(); Ds.Clear();
                        Data = new DataTable(); Data.Clear();
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Reports/Report_RDLCs/AssetReport.rdlc");
                        objReportBL.load(con, ReportBL.eLoadSp.Asset_Report, ref Ds);
                        Data = Ds.Tables[0];
                        ReportDataSource datasource = new ReportDataSource("DataSet1", Data);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(datasource);
                    }
                }
            }
        }
    }
}