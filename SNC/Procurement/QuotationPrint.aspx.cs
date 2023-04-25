using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLayer;
using System.Text;
using SNC.ErrorLogger;

    public partial class QuotationPrint : System.Web.UI.Page
    {
        IndentBL objIndentBL = null;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if(!Page.IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    if (Request.QueryString["IndentNo"] != null)
                    {
                        trItemName.Visible = false;
                        trIndentNo.Visible = true;
                        trItemName1.Visible = false;
                        trIndentNo1.Visible = true;
                        lblIndentNo.Text = (Request.QueryString["IndentNo"].ToString());
                        lblIndentNo1.Text = Request.QueryString["IndentNo"].ToString();
                        objIndentBL = new IndentBL();
                        DataSet ds = new DataSet();

                        objIndentBL.Indent_No = Request.QueryString["IndentNo"].ToString();
                       
                        objIndentBL.load(con, IndentBL.eLoadSp.SELECT_QUOTAION_COMPARSIONS_BY_ITEMORINDENT, ref ds);

                        Grid_QuotationComparsion.DataSource = ds;
                        Grid_QuotationComparsion.DataBind();
                        MergeRows(Grid_QuotationComparsion);
                      //  lblIndentdate.Text = ds.Tables[0].Rows[0]["Ind_date"].ToString();
                        QuotationPrintNewTable(ds);
                        ds.Clear();
                        objIndentBL.Indent_No = Request.QueryString["IndentNo"].ToString();
                        objIndentBL.load(con, IndentBL.eLoadSp.SELECT_INDENTDATE_ONINDENTNUMBER, ref ds);
                       lblIndentdate.Text = ds.Tables[0].Rows[0]["Ind_date"].ToString();
                       lblquatcomparedate.Text = System.DateTime.Now.ToString("dd/MM/yyyy"); 
                      // lblIndentdate.Text = string.Format(ds.Tables[0].Rows[0]["Ind_date"].ToString(), "dd-mm-yyyy");
                    }
                    if (Request.QueryString["Category"] != null && Server.UrlDecode(Request.QueryString["Item"]) != null)
                    {
                        trItemName.Visible = true;
                        trIndentNo.Visible = false;
                        trIndentNo1.Visible = false;
                        trItemName1.Visible = true;
                        objIndentBL = new IndentBL();
                        DataSet ds2 = new DataSet();
                        lblItem.Text = Request.QueryString["Item"].ToString();
                        lblItem1.Text = Request.QueryString["Item"].ToString();
                       
                        objIndentBL.Mat_Cat_Id = Convert.ToInt32(Request.QueryString["Category"].ToString());
                        objIndentBL.Item_Code = Request.QueryString["Item"].ToString();
                        objIndentBL.Indent_No = string.Empty;
                      //  objIndentBL.load(con, IndentBL.eLoadSp.SELECT_QUOTAION_COMPARSIONS_BY_ITEMORINDENT, ref ds2);

                        Grid_QuotationComparsion.DataSource = ds2;
                        Grid_QuotationComparsion.DataBind();
                        MergeRows(Grid_QuotationComparsion);
                        lblIndentdate.Text = ds2.Tables[0].Rows[0]["Ind_date"].ToString();
                        QuotationPrintNewTable(ds2);
                        lblquatcomparedate.Text = System.DateTime.Now.ToString("dd/MM/yyyy"); 
                    }
                }
                else
                {
                    Response.Redirect("../CommonPages/Login.aspx", false);
                }
            }
        }

        private void QuotationPrintNewTable(DataSet data)
        {
            try
            {
                if (data.Tables[0].Rows.Count > 0)
                {
                    StringBuilder html = new StringBuilder();

                    html.Append("<table border='1' width='100%'  class='GridCenter'>");
                    html.Append("<tr>");

                    //html.Append("<td style='text-align: center;font-weight:bold'>");
                    //html.Append("CategoryName");
                    //html.Append("</td>");

                    html.Append("<td style='text-align: center;font-weight:bold;width:200px'>");
                    html.Append("Item Name");
                    html.Append("</td>");



                    html.Append("<td style='text-align: center;font-weight:bold;width:100px'>");
                    html.Append("UOM");
                    html.Append("</td>");

                    html.Append("<td style='text-align: center;font-weight:bold;width:100px'>");
                    html.Append("Quantity");
                    html.Append("</td>");




                    var distinctValues = data.Tables[0].AsEnumerable()
                            .Select(row => new
                            {
                                attribute_name = row.Field<string>("Vendor_name"),
                                attri_VendorID = row.Field<string>("Vendor_ID"),
                                attri_TotalAmt = row.Field<decimal>("TotalAmt"),
                                attri_GrandTotalAmt = row.Field<decimal>("TotalAmt"),
                                attri_Transport = row.Field<string>("Transport"),
                                arr_Packing = row.Field<string>("Packing"),
                                attri_Paymentterms = row.Field<string>("PaymentTerms"),
                                arr_delivery = row.Field<string>("Delivery"),
                                arr_remarks = row.Field<string>("Remarks")


                            })
                            .Distinct();





                    foreach (var a in distinctValues.ToList())
                    {
                        html.Append("<td style='width:200px;'>");
                        html.Append("<center>");

                        html.Append("<table  border='1' width='100%' style='font-weight:bold'>");

                        html.Append("<tr>");

                        html.Append("<td colspan='2'>");
                        html.Append("<center>");
                        html.Append(a.attribute_name.ToString());


                        html.Append("</center>");
                        html.Append("</td>");
                        html.Append("</tr>");


                        html.Append("<tr>");

                        html.Append("<td style='width:50%;padding:5px'>");
                        html.Append("<center>");
                        html.Append("Rate");
                        html.Append("</center>");
                        html.Append("</td>");
                        html.Append("<td style='width:50%;padding:5px'>");
                        html.Append("<center>");
                        html.Append("Amt");
                        html.Append("</center>");
                        html.Append("</td>");

                        html.Append("</tr>");

                        html.Append("</table>");

                        html.Append("</center>");
                        html.Append("</td>");

                    }

                    html.Append("</tr>");


                    HashSet<string> SeenItems = new HashSet<string>();

                    foreach (DataRow dr in data.Tables[0].Rows)
                    {
                        if (!SeenItems.Contains(dr["Item_Name"].ToString()))
                        {
                            SeenItems.Add(dr["Item_Name"].ToString());

                            html.Append("<tr>");

                            //html.Append("<td>");
                            //html.Append(dr["Category_Name"].ToString());
                            //html.Append("</td>");

                            html.Append("<td style='width:200px'>");
                            html.Append(dr["Item_Name"].ToString());
                            html.Append("</td>");


                            html.Append("<td style='text-align: center;width:100px'>");
                            html.Append(dr["UOMName"].ToString());
                            html.Append("</td>");

                            html.Append("<td style='text-align: right;width:100px'>");
                            html.Append(dr["Qty_required"].ToString());
                            html.Append("</td>");


                            bool exists;
                            foreach (var a in distinctValues.ToList())
                            {

                                DataTable DatafilterDt = new DataTable();
                                DatafilterDt = data.Tables[0];

                                exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Item_Code").Equals(dr["Item_Code"])
                                                                                    && c.Field<string>("Vendor_ID").Equals(a.attri_VendorID)).Count() > 0;
                                if (exists)
                                {
                                    DataTable filterdData = DatafilterDt.AsEnumerable()
                                                 .Where(c => c.Field<string>("Item_Code").Equals(dr["Item_Code"])
                                                               && c.Field<string>("Vendor_ID").Equals(a.attri_VendorID))
                                                 .CopyToDataTable();


                                    html.Append("<td style='width:200px;'>");

                                    html.Append("<table  border='1' width='100%'>");
                                    html.Append("<tr>");
                                    html.Append("<td style='width:50%; text-align: right;padding:5px' >");
                                    html.Append(filterdData.Rows[0]["OverallRate"].ToString());
                                    html.Append("</td>");

                                    html.Append("<td style='width:50%; text-align: right;padding:5px' >");
                                    html.Append(filterdData.Rows[0]["WithOverAllAmt"].ToString());
                                    html.Append("</td>");

                                    html.Append("</tr>");
                                    html.Append("</table>");

                                    html.Append("</td>");


                                }
                                else
                                {
                                    html.Append("<td>");
                                    html.Append("</td>");
                                }
                                exists = false;
                            }

                            html.Append("</tr>");
                        }

                    }

                    html.Append("<tr>");
                    html.Append("<td colspan='3' style='text-align: right;font-weight:bold' >");
                    html.Append("Grand Total");
                    html.Append("</td>");
                    foreach (var a in distinctValues.ToList())
                    {
                        html.Append("<td style='text-align: right;font-weight:bold'>");
                        html.Append(a.attri_GrandTotalAmt.ToString());
                        html.Append("</td>");
                    }
                    html.Append("</tr>");

                    html.Append("<tr>");
                    html.Append("<td colspan='3' style='text-align: right;font-weight:bold' >");
                    html.Append("Transportation Cost");
                    html.Append("</td>");
                    foreach (var a in distinctValues.ToList())
                    {
                        html.Append("<td style='text-align: center;'>");
                        html.Append(a.attri_Transport.ToString());
                        html.Append("</td>");
                    }
                    html.Append("</tr>");

                    html.Append("<tr>");
                    html.Append("<td colspan='3' style='text-align: right;font-weight:bold' >");
                    html.Append("Packing & Forwarding Cost");
                    html.Append("</td>");
                    foreach (var a in distinctValues.ToList())
                    {
                        html.Append("<td style='text-align: center;'>");
                        html.Append(a.arr_Packing.ToString());
                        html.Append("</td>");
                    }
                    html.Append("</tr>");


                    html.Append("<tr>");
                    html.Append("<td colspan='3' style='text-align: right;font-weight:bold' >");
                    html.Append("Delivery");
                    html.Append("</td>");
                    foreach (var a in distinctValues.ToList())
                    {
                        html.Append("<td style='text-align: center;'>");
                        html.Append(a.arr_delivery.ToString());
                        html.Append("</td>");
                    }
                    html.Append("</tr>");


                    html.Append("<tr>");
                    html.Append("<td colspan='3' style='text-align: right;font-weight:bold' >");
                    html.Append("Payment Terms");
                    html.Append("</td>");
                    foreach (var a in distinctValues.ToList())
                    {
                        html.Append("<td style='text-align: center;'>");
                        html.Append(a.attri_Paymentterms.ToString());
                        html.Append("</td>");
                    }
                    html.Append("</tr>");

                    html.Append("<tr>");
                    html.Append("<td colspan='3' style='text-align: right;font-weight:bold' >");
                    html.Append("Remarks");
                    html.Append("</td>");
                    foreach (var a in distinctValues.ToList())
                    {
                        html.Append("<td style='text-align: center;'>");
                        html.Append(a.arr_remarks.ToString());
                        html.Append("</td>");
                    }
                    html.Append("</tr>");

                    html.Append("</table>");

                    PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void Grid_QuotationComparsion_PreRender(object sender, EventArgs e)
        {
            MergeRows(Grid_QuotationComparsion);
        }

        //Row merger for same VENDOR NAME
        public static void MergeRows(GridView gridView)
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                if (row.Cells[0].Text == previousRow.Cells[0].Text)
                {
                    row.Cells[0].RowSpan = previousRow.Cells[0].RowSpan < 2 ? 2 :
                                           previousRow.Cells[0].RowSpan + 1;
                    previousRow.Cells[0].Visible = false;
                }
                if (row.Cells[2].Text == previousRow.Cells[2].Text)
                {
                    row.Cells[2].RowSpan = previousRow.Cells[2].RowSpan < 2 ? 2 :
                                           previousRow.Cells[2].RowSpan + 1;
                    previousRow.Cells[2].Visible = false;

                   
                }

             
                if (row.Cells[15].Text == previousRow.Cells[15].Text)
                {
                    row.Cells[15].RowSpan = previousRow.Cells[15].RowSpan < 2 ? 2 :
                                                   previousRow.Cells[15].RowSpan + 1;
                    previousRow.Cells[15].Visible = false;

                }

            }
        }
    }
