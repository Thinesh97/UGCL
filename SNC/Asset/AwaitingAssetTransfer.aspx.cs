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


    public partial class AwaitingAssetTransfer : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        AssetTransferBL objassetTransferBL = null;
        DataSet ds = null;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(!IsPostBack)
                {
                    if (Session["UID"] != null)
                    {
                        BindAssetDetails();
                    }
                    else
                    {
                        Response.Redirect("../CommonPages/Login.aspx", false);
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty); 
            }


        }

        protected void BindAssetDetails()
        {
            try
            {
                ds = new DataSet();
                objassetTransferBL = new AssetTransferBL();
                objassetTransferBL.UserID = Convert.ToInt32(Session["UID"]);
                objassetTransferBL.Project_Code = Session["Project_Code"].ToString();
                objassetTransferBL.load(con, AssetTransferBL.eLoadSp.Binding_Awaiting_Asset_Status, ref ds);
                if(ds.Tables[0].Rows.Count > 0)
                {
                    GridAssets.DataSource = ds;
                    GridAssets.DataBind();
                }
                else
                {
                    GridAssets.DataSource = null;
                    GridAssets.DataBind();
                }
            }
            catch(Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void btnAccepts_Click(object sender, EventArgs e)
        {
            try
            {
                ds = new DataSet();
                objassetTransferBL = new AssetTransferBL();

                if (GridAssets.SelectedRecords != null)
                {
                    foreach (Hashtable ht in GridAssets.SelectedRecords)
                    {
                        objassetTransferBL.AssetTran_ID = Convert.ToInt32(ht["AssetTran_ID"]);

                        if (objassetTransferBL.AwaitingStatusupdate(con, AssetTransferBL.eLoadSp.Update_Awaiting_Asset_Status))
                        {
                            BindAssetDetails();
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please Select Any Asset.');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
            finally
            {
                GridAssets.SelectedRecords = null;
            }
        }

        protected void btnRejects_Click(object sender, EventArgs e)
        {
            try
            {
                ds = new DataSet();
                objassetTransferBL = new AssetTransferBL();

                if (GridAssets.SelectedRecords != null)
                {
                    foreach (Hashtable ht in GridAssets.SelectedRecords)
                    {
                        objassetTransferBL.AssetTran_ID = Convert.ToInt32(ht["AssetTran_ID"]);
                        if (objassetTransferBL.Statusupdate(con, AssetTransferBL.eLoadSp.Update_Awaiting_Asset_Status_Reject))
                        {
                            BindAssetDetails();
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please Select Any Asset.');", true);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
            finally
            {
                GridAssets.SelectedRecords = null;
            }
        }

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {


                string lnkbtnPrin = ((LinkButton)sender).CommandName.ToString();
                string pageurl = "../Asset/Print_Internal_Transfer_Assets.aspx?ID2=" + lnkbtnPrin;
                Response.Write("<script> window.open( '" + pageurl + "','_blank' ); </script>");
            }
            catch(Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
    }
