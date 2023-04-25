using BusinessLayer;
using System;
using SNC.ErrorLogger;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Text;
using System.Collections;



public partial class QuotationComparsions : System.Web.UI.Page
{
    MaterialBL objMaterial = null;
    IndentBL objIndent = null;
    IndentBL objIndentBL = null;
    Category objCategory = null;
    DataSet ds = null;
    decimal minlevel = 0;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    btn_ProceedToPO.Visible = false;
                    //BindCategoryDetails();
                    BindIndentList();
                    BindBudgetSectors();
                }
                else
                {
                    Response.Redirect("../CommonPages/Login.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }

    private void BindBudgetSectors()
    {
        objMaterial = new MaterialBL();
        ds = new DataSet();
        DataTable DatafilterDt;
        DataTable dtSector = new DataTable();
        DataTable dtCategorySector = new DataTable();
        objMaterial.load(con, MaterialBL.eLoadSp.SELECT_BUDGET_SECTOR_ALL, ref ds);
        if (ds.Tables.Count > 0)
        {
            DatafilterDt = ds.Tables[0];

            ddlBudgetSector.DataSource = ds.Tables[0];
            ddlBudgetSector.DataTextField = "Sector_Name";
            ddlBudgetSector.DataValueField = "Budget_Sector_ID";
            ddlBudgetSector.DataBind();
            ddlBudgetSector.Items.Insert(0, "-Select-");

        }
    }

    protected void ddlBudgetSector_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlCategory.Items.Clear();
            ddlCategory.Items.Insert(0, "-Select-");
            BindCategoryDetails();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }


    private void BindCategoryDetails()
    {

        try
        {
            objCategory = new Category();
            ds = new DataSet();
            DataTable dt = null;
            DataTable DatafilterDt;
            bool exists;
            int BudgetSectorID;
            if (objCategory.load(con, Category.eLoadSp.SELECT_ALL, ref ds))
            {
                dt = ds.Tables[0];
                if (ddlBudgetSector.SelectedIndex != 0)
                {
                    BudgetSectorID = Convert.ToInt16(ddlBudgetSector.SelectedValue.ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DatafilterDt = ds.Tables[0];


                        exists = DatafilterDt.AsEnumerable().Where(c => c.Field<int>("Budget_Sector_ID").Equals(BudgetSectorID)).Count() > 0;
                        if (exists)
                        {
                            DataTable SectorIDdt = DatafilterDt.AsEnumerable()
                                         .Where(r => r.Field<int>("Budget_Sector_ID") == BudgetSectorID)
                                         .CopyToDataTable();

                            ddlCategory.DataSource = SectorIDdt;
                            ddlCategory.DataValueField = "Mat_cat_ID";
                            ddlCategory.DataTextField = "Category_Name";
                            ddlCategory.DataBind();

                        }
                        else
                        {
                            ddlCategory.Items.Clear();
                            ddlCategory.DataSource = null;
                            ddlCategory.DataBind();
                        }
                        exists = false;

                    }
                    ddlCategory.Items.Insert(0, "-Select-");
                }

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    private void BindIndentList()
    {
        try
        {
            //if (chkspecificquotation.Checked != false)
            //{
                ds = new DataSet();
                objIndentBL = new IndentBL();
                objIndentBL.Project_Code = Session["Project_Code"].ToString();

                objIndentBL.load(con, IndentBL.eLoadSp.SELECT_INDENT_NO_FOR_PO, ref ds);
                ddlIndentNo.DataSource = ds;
                ddlIndentNo.DataTextField = "Indent_No";
                ddlIndentNo.DataValueField = "Indent_No";
                ddlIndentNo.DataBind();
                ddlIndentNo.Items.Insert(0, "-Select-");

                //BindQuotationComparsionsGrid();
                //btnPrint.Visible = true;
                //btnPrint.Target = "_blank";
                //btnPrint.HRef = "QuotationPrint.aspx?IndentNo=" + ddlIndentNo.SelectedValue;

                //btn_ProceedToPO.Visible = true;
           // }
        }
        // else
        //    { 
        //    ds = new DataSet();
        //    objIndentBL = new IndentBL();
        //    objIndentBL.Project_Code = Session["Project_Code"].ToString();
        //    objIndentBL.load(con, IndentBL.eLoadSp.SELECT_INDENT_NO_FOR_PO, ref ds);
        //    ddlIndentNo.DataSource = ds;
        //    ddlIndentNo.DataTextField = "Indent_No";
        //    ddlIndentNo.DataValueField = "Indent_No";
        //    ddlIndentNo.DataBind();
        //    ddlIndentNo.Items.Insert(0, "-Select-");
        //}
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            objIndent = new IndentBL();
            ds = new DataSet();
            objIndent.Mat_Cat_Id = Convert.ToUInt16(ddlCategory.SelectedValue.Trim());
            objIndent.load(con, IndentBL.eLoadSp.SELECT_ITEMCODE_BY_CATEGORY_ID, ref ds);
            ddlItem.DataSource = ds;
            ddlItem.DataValueField = "Item_Code";
            ddlItem.DataTextField = "Item_Name";
            ddlItem.DataBind();
            ddlItem.Items.Insert(0, "-Select-");

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    
    {
        Div10.Visible = false;
        //lblDate2.Text = System.DateTime.Now.ToString("dd/MM/yyyy"); 
        lblDate2.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        try
        {
            objIndentBL = new IndentBL();


            if (rd_SearchBy.SelectedValue == "ItemBased")
            {
                if (ddlCategory.SelectedIndex == 0 || ddlItem.SelectedIndex == 0)
                {
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select Category and Item !');", true);
                    return;
                }
                else
                {
                  
                    btn_ProceedToPO.Visible = false;
                    btnPrint.Visible = true;
                    btnPrint.Target = "_blank";
                    btnPrint.HRef = "QuotationPrint.aspx?Category=" + Server.UrlEncode(ddlCategory.SelectedValue) + "&Item=" + Server.UrlEncode(ddlItem.SelectedValue);


                    //Response.Redirect("../Procurement/QuotationPrint.aspx?IndentNo=" + ddlIndentNo.SelectedValue, true);

                    BindQuotationComparsionsGrid1();

                }
            }
            else
            {
              

                if (ddlIndentNo.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select Indent No!');", true);
                    return;
                }
                else
                {
                 
                    BindQuotationComparsionsGrid();
                    btnPrint.Visible = true;
                    btnPrint.Target = "_blank";
                    btnPrint.HRef = "QuotationPrint.aspx?IndentNo=" + ddlIndentNo.SelectedValue;

                    btn_ProceedToPO.Visible = true;
                }
            }

            //else if ()

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    private void BindQuotationComparsionsGrid1()
    {
        Div10.Visible = true;
        try
        {


            objIndentBL = new IndentBL();
            ds = new DataSet();

            objIndentBL.Mat_Cat_Id = Convert.ToInt32(ddlCategory.SelectedIndex != 0 ? Convert.ToInt32(ddlCategory.SelectedValue) : 0);
            objIndentBL.Item_Code = ddlItem.SelectedIndex != 0 ? ddlItem.SelectedValue.Trim() : string.Empty;
            objIndentBL.Project_Code = Session["Project_Code"].ToString();

            objIndentBL.load(con, IndentBL.eLoadSp.SELECT_QUOTAION_COMPARSIONS_BY_ITEMS, ref ds);

            if (ddlIndentNo.SelectedIndex != 0)
            {
                Grid_QuotationComparsion.DataSource = ds.Tables[0];
                Grid_QuotationComparsion.Columns[16].Visible = true;
                Grid_QuotationComparsion.DataBind();
            }
            else
            {
                Grid_QuotationComparsion.DataSource = ds.Tables[0];
                Grid_QuotationComparsion.Columns[16].Visible = false;
                Grid_QuotationComparsion.DataBind();
            }

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                minlevel = Convert.ToDecimal(ds.Tables[0].Compute("min(TotalAmt)", string.Empty));
                foreach (GridViewRow row in Grid_QuotationComparsion.Rows)
                {
                    row.Cells[15].ForeColor = (row.Cells[15].Text == minlevel.ToString()) ? System.Drawing.Color.Blue : System.Drawing.Color.Black;
                }
            }



            if (ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder html = new StringBuilder();

                html.Append("<table border='1' width='100%' class='table tbl1' >");
                html.Append("<tr>");

                html.Append("<td>");
                html.Append("CategoryName");
                html.Append("</td>");

                html.Append("<td>");
                html.Append("Item Name");
                html.Append("</td>");



                html.Append("<td>");
                html.Append("UOM");
                html.Append("</td>");

                html.Append("<td>");
                html.Append("Quantity");
                html.Append("</td>");




                var distinctValues = ds.Tables[0].AsEnumerable()
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
                    html.Append("<td>");


                    html.Append("<table  border='1' width='100%' >");

                    html.Append("<tr>");

                    html.Append("<td colspan='2'>");
                    html.Append("<center>");
                    html.Append(a.attribute_name + " </br> " + "Vendor ID = " + "<a href ='PurchaseOrder.aspx?QuotationIndentNo=" + ddlIndentNo.SelectedValue + "&Vendor=" + a.attri_VendorID.ToString() + "'>" + a.attri_VendorID.ToString() + "</a>");


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


                    html.Append("</td>");

                }

                html.Append("</tr>");


                HashSet<string> SeenItems = new HashSet<string>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (!SeenItems.Contains(dr["Item_Name"].ToString()))
                    {
                        SeenItems.Add(dr["Item_Name"].ToString());

                        html.Append("<tr>");

                        html.Append("<td>");
                        html.Append(dr["Category_Name"].ToString());
                        html.Append("</td>");

                        html.Append("<td>");
                        html.Append(dr["Item_Name"].ToString());
                        html.Append("</td>");


                        html.Append("<td>");
                        html.Append(dr["UOMName"].ToString());
                        html.Append("</td>");

                        html.Append("<td>");
                        html.Append(dr["Qty_required"].ToString());
                        html.Append("</td>");


                        bool exists;
                        foreach (var a in distinctValues.ToList())
                        {

                            DataTable DatafilterDt = new DataTable();
                            DatafilterDt = ds.Tables[0];

                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Item_Code").Equals(dr["Item_Code"])
                                                                                && c.Field<string>("Vendor_ID").Equals(a.attri_VendorID)).Count() > 0;
                            if (exists)
                            {
                                DataTable filterdData = DatafilterDt.AsEnumerable()
                                             .Where(c => c.Field<string>("Item_Code").Equals(dr["Item_Code"])
                                                           && c.Field<string>("Vendor_ID").Equals(a.attri_VendorID))
                                             .CopyToDataTable();


                                html.Append("<td>");

                                html.Append("<table  border='1' width='100%' >");
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
                html.Append("<td colspan='4' style='text-align: right;font-weight:bold' >");
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
                html.Append("<td colspan='4' style='text-align: right;font-weight:bold' >");
                html.Append("Transportation Cost");
                html.Append("</td>");
                foreach (var a in distinctValues.ToList())
                {
                    html.Append("<td>");
                    html.Append(a.attri_Transport.ToString());
                    html.Append("</td>");
                }
                html.Append("</tr>");

                html.Append("<tr>");
                html.Append("<td colspan='4' style='text-align: right;font-weight:bold' >");
                html.Append("Packing & Forwarding Cost");
                html.Append("</td>");
                foreach (var a in distinctValues.ToList())
                {
                    html.Append("<td>");
                    html.Append(a.arr_Packing.ToString());
                    html.Append("</td>");
                }
                html.Append("</tr>");


                html.Append("<tr>");
                html.Append("<td colspan='4' style='text-align: right;font-weight:bold' >");
                html.Append("Delivery");
                html.Append("</td>");
                foreach (var a in distinctValues.ToList())
                {
                    html.Append("<td>");
                    html.Append(a.arr_delivery.ToString());
                    html.Append("</td>");
                }
                html.Append("</tr>");


                html.Append("<tr>");
                html.Append("<td colspan='4' style='text-align: right;font-weight:bold' >");
                html.Append("Payment Terms");
                html.Append("</td>");
                foreach (var a in distinctValues.ToList())
                {
                    html.Append("<td>");
                    html.Append(a.attri_Paymentterms.ToString());
                    html.Append("</td>");
                }
                html.Append("</tr>");

                html.Append("<tr>");
                html.Append("<td colspan='4' style='text-align: right;font-weight:bold' >");
                html.Append("Remarks");
                html.Append("</td>");
                foreach (var a in distinctValues.ToList())
                {
                    html.Append("<td>");
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

    private void BindQuotationComparsionsGrid()
    {
        Div10.Visible = true;
        try
        {


            objIndentBL = new IndentBL();
            ds = new DataSet();
           
            objIndentBL.Indent_No = ddlIndentNo.SelectedIndex != 0 ? ddlIndentNo.SelectedValue.ToString() : string.Empty;
            objIndentBL.Specific_quotation = chkspecificquotation.Checked;
            objIndentBL.Project_Code = Session["Project_Code"].ToString();
            objIndentBL.load(con, IndentBL.eLoadSp.SELECT_QUOTAION_COMPARSIONS_BY_ITEMORINDENT, ref ds);

            if (ddlIndentNo.SelectedIndex != 0)
            {
                Grid_QuotationComparsion.DataSource = ds.Tables[0];
                Grid_QuotationComparsion.Columns[16].Visible = true;
                Grid_QuotationComparsion.DataBind();
            }
            else
            {
                Grid_QuotationComparsion.DataSource = ds.Tables[0];
                Grid_QuotationComparsion.Columns[16].Visible = false;
                Grid_QuotationComparsion.DataBind();
            }

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                minlevel = Convert.ToDecimal(ds.Tables[0].Compute("min(TotalAmt)", string.Empty));
                foreach (GridViewRow row in Grid_QuotationComparsion.Rows)
                {
                    row.Cells[15].ForeColor = (row.Cells[15].Text == minlevel.ToString()) ? System.Drawing.Color.Blue : System.Drawing.Color.Black;
                }
            }



            if (ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder html = new StringBuilder();

                html.Append("<table border='1' width='100%' class='table tbl1' >");
                html.Append("<tr>");

                html.Append("<td>");
                html.Append("CategoryName");
                html.Append("</td>");

                html.Append("<td>");
                html.Append("Item Name");
                html.Append("</td>");



                html.Append("<td>");
                html.Append("UOM");
                html.Append("</td>");

                html.Append("<td>");
                html.Append("Quantity");
                html.Append("</td>");




                var distinctValues = ds.Tables[0].AsEnumerable()
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
                    html.Append("<td>");


                    html.Append("<table  border='1' width='100%' >");

                    html.Append("<tr>");

                    html.Append("<td colspan='2'>");
                    html.Append("<center>");
                    html.Append(a.attribute_name + " </br> " + "Vendor ID = " + "<a href ='PurchaseOrder.aspx?QuotationIndentNo=" + ddlIndentNo.SelectedValue + "&Vendor=" + a.attri_VendorID.ToString() + "'>" + a.attri_VendorID.ToString() + "</a>");


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


                    html.Append("</td>");

                }

                html.Append("</tr>");


                HashSet<string> SeenItems = new HashSet<string>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (!SeenItems.Contains(dr["Item_Name"].ToString()))
                    {
                        SeenItems.Add(dr["Item_Name"].ToString());

                        html.Append("<tr>");

                        html.Append("<td>");
                        html.Append(dr["Category_Name"].ToString());
                        html.Append("</td>");

                        html.Append("<td>");
                        html.Append(dr["Item_Name"].ToString());
                        html.Append("</td>");


                        html.Append("<td>");
                        html.Append(dr["UOMName"].ToString());
                        html.Append("</td>");

                        html.Append("<td>");
                        html.Append(dr["Qty_required"].ToString());
                        html.Append("</td>");


                        bool exists;
                        foreach (var a in distinctValues.ToList())
                        {

                            DataTable DatafilterDt = new DataTable();
                            DatafilterDt = ds.Tables[0];

                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Item_Code").Equals(dr["Item_Code"])
                                                                                && c.Field<string>("Vendor_ID").Equals(a.attri_VendorID)).Count() > 0;
                            if (exists)
                            {
                                DataTable filterdData = DatafilterDt.AsEnumerable()
                                             .Where(c => c.Field<string>("Item_Code").Equals(dr["Item_Code"])
                                                           && c.Field<string>("Vendor_ID").Equals(a.attri_VendorID))
                                             .CopyToDataTable();


                                html.Append("<td>");

                                html.Append("<table  border='1' width='100%' >");
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
                html.Append("<td colspan='4' style='text-align: right;font-weight:bold' >");
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
                html.Append("<td colspan='4' style='text-align: right;font-weight:bold' >");
                html.Append("Transportation Cost");
                html.Append("</td>");
                foreach (var a in distinctValues.ToList())
                {
                    html.Append("<td>");
                    html.Append(a.attri_Transport.ToString());
                    html.Append("</td>");
                }
                html.Append("</tr>");

                html.Append("<tr>");
                html.Append("<td colspan='4' style='text-align: right;font-weight:bold' >");
                html.Append("Packing & Forwarding Cost");
                html.Append("</td>");
                foreach (var a in distinctValues.ToList())
                {
                    html.Append("<td>");
                    html.Append(a.arr_Packing.ToString());
                    html.Append("</td>");
                }
                html.Append("</tr>");


                html.Append("<tr>");
                html.Append("<td colspan='4' style='text-align: right;font-weight:bold' >");
                html.Append("Delivery");
                html.Append("</td>");
                foreach (var a in distinctValues.ToList())
                {
                    html.Append("<td>");
                    html.Append(a.arr_delivery.ToString());
                    html.Append("</td>");
                }
                html.Append("</tr>");


                html.Append("<tr>");
                html.Append("<td colspan='4' style='text-align: right;font-weight:bold' >");
                html.Append("Payment Terms");
                html.Append("</td>");
                foreach (var a in distinctValues.ToList())
                {
                    html.Append("<td>");
                    html.Append(a.attri_Paymentterms.ToString());
                    html.Append("</td>");
                }
                html.Append("</tr>");

                html.Append("<tr>");
                html.Append("<td colspan='4' style='text-align: right;font-weight:bold' >");
                html.Append("Remarks");
                html.Append("</td>");
                foreach (var a in distinctValues.ToList())
                {
                    html.Append("<td>");
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


    protected void rd_SearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rd_SearchBy.SelectedItem.Text == "Item based")
            {
          
                CateBaseddiv.Visible = true;
                itemDiv.Visible = true;
                Indentbaseddiv.Visible = false;
                ddlIndentNo.SelectedValue = ddlIndentNo.Items.FindByText("-Select-").Value;
                Grid_QuotationComparsion.DataSource = null;
                Grid_QuotationComparsion.DataBind();
                btn_ProceedToPO.Visible = false;
                Div10.Visible = false;
            }
            else if (rd_SearchBy.SelectedItem.Text == "Indent based")
            {
                
                itemDiv.Visible = false;
                CateBaseddiv.Visible = false;
                Indentbaseddiv.Visible = true;
                ddlCategory.SelectedValue = ddlCategory.Items.FindByText("-Select-").Value;
                ddlItem.SelectedValue = ddlItem.Items.FindByText("-Select-").Value;
                Grid_QuotationComparsion.DataSource = null;
                Grid_QuotationComparsion.DataBind();
                Div10.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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

                row.Cells[16].RowSpan = previousRow.Cells[16].RowSpan < 2 ? 2 :
                                               previousRow.Cells[16].RowSpan + 1;
                previousRow.Cells[16].Visible = false;
            }

            //if (row.Cells[3].Text == previousRow.Cells[3].Text)
            //{
            //    row.Cells[3].RowSpan = previousRow.Cells[3].RowSpan < 2 ? 2 :
            //                                   previousRow.Cells[3].RowSpan + 1;
            //    previousRow.Cells[3].Visible = false;
            //}
            if (row.Cells[15].Text == previousRow.Cells[15].Text)
            {
                row.Cells[15].RowSpan = previousRow.Cells[15].RowSpan < 2 ? 2 :
                                               previousRow.Cells[15].RowSpan + 1;
                previousRow.Cells[15].Visible = false;

            }



        }

    }

    protected void Grid_QuotationComparsion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid_QuotationComparsion.PageIndex = e.NewPageIndex;
        BindQuotationComparsionsGrid();
    }



    protected void btn_ProceedToPO_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in Grid_QuotationComparsion.Rows)
            {
                bool chkCheck = ((RadioButton)row.FindControl("ChkSelect")).Checked;

                if (chkCheck)
                {
                    string VendorID = ((Label)row.FindControl("lblVendorID")).Text;

                    Response.Redirect("PurchaseOrder.aspx?QuotationIndentNo=" + ddlIndentNo.SelectedValue + "&Vendor=" + VendorID, false);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (ddlIndentNo.SelectedIndex != 0)
        {

            Response.Redirect("../Procurement/QuotationPrint.aspx?IndentNo=" + ddlIndentNo.SelectedValue, true);
        }
        else
        {
            
            Response.Redirect("../Procurement/QuotationPrint.aspx?Category=" + Server.UrlEncode(ddlCategory.SelectedValue) + "&Item=" + Server.UrlEncode(ddlItem.SelectedValue),true);
        }


    }

    protected void ChkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in Grid_QuotationComparsion.Rows)
            {
                RadioButton SelectedRadio = ((RadioButton)row.FindControl("ChkSelect"));

                if (SelectedRadio != null && SelectedRadio.UniqueID == ((RadioButton)sender).UniqueID.ToString())
                {
                    SelectedRadio.Checked = true;

                }
                else
                {
                    SelectedRadio.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }



    public class GridViewTemplate : ITemplate
    {
        //A variable to hold the type of ListItemType.
        ListItemType _templateType;

        //A variable to hold the column name.
        string _columnName;

        //Constructor where we define the template type and column name.
        public GridViewTemplate(ListItemType type, string colname)
        {
            //Stores the template type.
            _templateType = type;

            //Stores the column name.
            _columnName = colname;
        }

        void ITemplate.InstantiateIn(System.Web.UI.Control container)
        {
            switch (_templateType)
            {
                case ListItemType.Header:
                    //Creates a new label control and add it to the container.
                    Label lbl = new Label();            //Allocates the new label object.
                    lbl.Text = _columnName;             //Assigns the name of the column in the lable.
                    container.Controls.Add(lbl);        //Adds the newly created label control to the container.
                    break;

                case ListItemType.Item:
                    //Creates a new text box control and add it to the container.
                    TextBox tb1 = new TextBox();                            //Allocates the new text box object.
                    tb1.DataBinding += new EventHandler(tb1_DataBinding);   //Attaches the data binding event.
                    tb1.Columns = 4;                                        //Creates a column with size 4.
                    container.Controls.Add(tb1);                            //Adds the newly created textbox to the container.
                    break;

                case ListItemType.EditItem:
                    //As, I am not using any EditItem, I didnot added any code here.
                    break;

                case ListItemType.Footer:
                    CheckBox chkColumn = new CheckBox();
                    chkColumn.ID = "Chk" + _columnName;
                    container.Controls.Add(chkColumn);
                    break;
            }
        }

        /// <summary>
        /// This is the event, which will be raised when the binding happens.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tb1_DataBinding(object sender, EventArgs e)
        {
            TextBox txtdata = (TextBox)sender;
            GridViewRow container = (GridViewRow)txtdata.NamingContainer;
            object dataValue = DataBinder.Eval(container.DataItem, _columnName);
            if (dataValue != DBNull.Value)
            {
                txtdata.Text = dataValue.ToString();
            }
        }
    }

    
}
