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
using SNC.ErrorLogger;
using System.Collections;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;



public partial class Internal_Transfer_Assets : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    AssetTransferBL objassetTransferBL = null;
    DataSet ds = null;
    DataSet Accessds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    if (Request.QueryString["ID"] != null)
                    {
                        objassetTransferBL = new AssetTransferBL();
                        objassetTransferBL.AssetTran_ID = Convert.ToInt32(Request.QueryString["ID"]);
                        if (objassetTransferBL.load(con, AssetTransferBL.eLoadSp.POPULATING_VALUES_TO_PRINT_PAGE, ref ds))
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Label lblSite = (Label)this.FindControl("lblSite");
                                lblSite.Text = ds.Tables[0].Rows[0]["Site"].ToString();
                                lblmanufacture.Text = ds.Tables[0].Rows[0]["Manufacture"].ToString();
                                chkDL.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["DL"]);
                                chkRC.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["RC"]);
                                chkROADTAXRECEIPT.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Road_Tax_Reciept"]);
                                chkINSURANCE.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["INSURANCE"]);
                                chkPERMIT.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["PERMIT"]);
                                chkNOC.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["NOC"]);
                                chkFC.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["FC"]);
                                lblcoderegno.Text = ds.Tables[0].Rows[0]["Reg"].ToString();
                                lblType.Text = ds.Tables[0].Rows[0]["Type"].ToString();
                                lbldispatch.Text = ds.Tables[0].Rows[0]["Dispatch"].ToString();
                                chkWayBILL.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Way_BILL"]);


                            }

                    }

                    if (Request.QueryString["ID2"] != null)
                    {
                        objassetTransferBL = new AssetTransferBL();
                        objassetTransferBL.AssetTran_ID = Convert.ToInt32(Request.QueryString["ID2"]);
                        if (objassetTransferBL.load(con, AssetTransferBL.eLoadSp.POPULATING_VALUES_TO_PRINT_PAGE, ref ds))
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Label lblSite = (Label)this.FindControl("lblSite");
                                lblSite.Text = ds.Tables[0].Rows[0]["Site"].ToString();
                                lblmanufacture.Text = ds.Tables[0].Rows[0]["Manufacture"].ToString();
                                chkDL.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["DL"]);
                                chkRC.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["RC"]);
                                chkROADTAXRECEIPT.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Road_Tax_Reciept"]);
                                chkINSURANCE.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["INSURANCE"]);
                                chkPERMIT.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["PERMIT"]);
                                chkNOC.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["NOC"]);
                                chkFC.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["FC"]);
                                chkWayBILL.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Way_BILL"]);
                                lblcoderegno.Text = ds.Tables[0].Rows[0]["Reg"].ToString();
                                lblType.Text = ds.Tables[0].Rows[0]["Type"].ToString();
                                lbldispatch.Text = ds.Tables[0].Rows[0]["Dispatch"].ToString();



                                lblcodeandreg2.Text = ds.Tables[0].Rows[0]["Reg"].ToString();
                                lbldispatch2.Text = ds.Tables[0].Rows[0]["Dispatch"].ToString();
                                lbltype2.Text = ds.Tables[0].Rows[0]["Type"].ToString();
                                lblManufacturserver.Text = ds.Tables[0].Rows[0]["Manufacture"].ToString();
                                lblsite2.Text = ds.Tables[0].Rows[0]["Site"].ToString();
                                CheckBox1.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["DL"]);
                                CheckBox2.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["RC"]);
                                CheckBox3.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Road_Tax_Reciept"]);
                                CheckBox4.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["INSURANCE"]);
                                CheckBox5.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["PERMIT"]);
                                CheckBox6.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["NOC"]);
                                CheckBox7.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["FC"]);
                                CheckBox8.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Way_BILL"]);

                            }
                    }
                }
            }
            else
            {
                Response.Redirect("../CommonPages/Login.aspx", false);
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
}
