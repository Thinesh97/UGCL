using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using BusinessLayer;
using SNC.ErrorLogger;
using System.IO;
using System.Text;



using System.Net;
//using System.IO;
using System.Drawing;
using Color = System.Drawing.Color;

namespace SNC.Reports
{
    public partial class UGCL_MRNReportViewer : System.Web.UI.Page
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
                DataTable dt = new DataTable();
                BindReport();
            }

        }
        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    //base.VerifyRenderingInServerForm(control);
        //}

        public void BindReport()
        {
            try
            {

                Ds = new DataSet();
                objReportBL = new ReportBL();
                objReportBL.Task = "POWOMRN_Register";
                if (Session["FromDate"] != null)
                {
                    objReportBL.From_Date = DateTime.Parse(Session["FromDate"].ToString());
                }


                if (Session["ToDate"] != null)
                {
                    objReportBL.To_Date = DateTime.Parse(Session["ToDate"].ToString());
                    // objReportBL.To_Date = To_Date.ToString("yyyy-dd-mm"); 
                }



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


                if (Session["Projects"] != null)
                {
                    objReportBL.Projects = Session["Projects"].ToString();
                }

                if (Session["PONumber"] != null)
                {
                    objReportBL.Projects = Session["Projects"].ToString();
                }



                objReportBL.load(con, ReportBL.eLoadSp.POWO_Register_Report, ref Ds);
                GridItemsMRNReport.DataSource = Ds;
                GridItemsMRNReport.DataBind();

            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }

        }
        protected void lnkDownloadFile_Click(object sender, EventArgs e)
        {
            try
            {

                string filePath = Server.MapPath("~\\UploadedFiles\\" + (sender as LinkButton).Text.Replace("/", "-"));
                Response.Clear();

                string path = filePath;
                WebClient client = new WebClient();
                Byte[] buffer = client.DownloadData(path);
                if (buffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                }

            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
            }
        }

        protected void DownloadFile(object sender, EventArgs e) //Edited by Prashanth G
        {
            try
            {

                string filePath = Server.MapPath("~\\UploadedFiles\\" + (sender as LinkButton).Text.Replace(@"\\", "'\'"));
                Response.Clear();

                string path = filePath;
                //path.Replace(@"\\", "\");
                String x = Path.GetFullPath(path);

                x = x.Replace("//", "/");
                //x = "C:/Users/chetan/Desktop/SimproBackp/sim/Simpro/UGCL/UGCL19.01-2023/UGCL/SNC-INVEN/SNC/UploadedFiles/MRN-001_InvoiceCopy.pdf";

                //Path.GetFullPath(path).Replace("\\", "\\\\");
                //path = path.TrimEnd('\') + '\';
                //path = path.GetFullPath(path);
                WebClient client = new WebClient();
                Byte[] buffer = client.DownloadData(path);
                if (buffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                }

            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
            }
        }

        //protected void ExportExcell_Click(object sender, EventArgs e)
        //{

        //    ExportGridToExcel();
        //}
        //private void ExportGridToExcel()
        //{
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    Response.Charset = "";
        //    string FileName = "Vithal" + DateTime.Now + ".xls";
        //    StringWriter strwritter = new StringWriter();
        //    HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.ContentType = "application/vnd.ms-excel";
        //    Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        //    GridItemsMRNReport.GridLines = GridLines.Both;
        //    GridItemsMRNReport.HeaderStyle.Font.Bold = true;
        //    GridItemsMRNReport.RenderControl(htmltextwrtter);
        //    Response.Write(strwritter.ToString());
        //    Response.End();

        //}
        protected void ExportToExcel(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridItemsMRNReport.AllowPaging = false;
                this.BindReport();

                GridItemsMRNReport.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in GridItemsMRNReport.HeaderRow.Cells)
                {
                    cell.BackColor = GridItemsMRNReport.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridItemsMRNReport.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridItemsMRNReport.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridItemsMRNReport.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                        List<Control> controls = new List<Control>();

                        //Add controls to be removed to Generic List
                        foreach (Control control in cell.Controls)
                        {
                            controls.Add(control);
                        }

                        //Loop through the controls to be removed and replace with Literal
                        foreach (Control control in controls)
                        {
                            switch (control.GetType().Name)
                            {
                                case "HyperLink":
                                    cell.Controls.Add(new Literal { Text = (control as HyperLink).Text });
                                    break;
                                case "TextBox":
                                    cell.Controls.Add(new Literal { Text = (control as TextBox).Text });
                                    break;
                                case "LinkButton":
                                    cell.Controls.Add(new Literal { Text = (control as LinkButton).Text });
                                    break;
                                case "CheckBox":
                                    cell.Controls.Add(new Literal { Text = (control as CheckBox).Text });
                                    break;
                                case "RadioButton":
                                    cell.Controls.Add(new Literal { Text = (control as RadioButton).Text });
                                    break;
                            }
                            cell.Controls.Remove(control);
                        }
                    }
                }

                GridItemsMRNReport.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }


        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridItemsMRNReport.PageIndex = e.NewPageIndex;
            this.BindReport();
        }
        protected void btnExportToPDF_Click(object sender, EventArgs e)
        {


            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages
                    GridItemsMRNReport.AllowPaging = false;
                    this.BindReport();

                    GridItemsMRNReport.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=MRNGridReport.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
    }


}