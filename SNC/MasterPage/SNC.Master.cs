using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SNC.MasterPage
{
    public partial class SNC : System.Web.UI.MasterPage
    {
        UserBL ObjUserBL = null;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);

        DataSet ds = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UID"] == null)
                {
                    Response.Redirect("~/CommonPages/Login.aspx", false);
                }
                else if (Session["Empid"] != null && Session["Empid"].ToString() != string.Empty)
                {
                    PermissionAccess();
                    txtUserName.Text = Session["Name"].ToString();
                }
                if (!IsPostBack)
                {
                    if (Session["Role"].ToString() == "Application Admin" || Convert.ToBoolean(Session["IsHoUser"]))
                    {
                        divProjectselection.Visible = true;
                        divProjectselection1.Visible = true;
                        BindProjectList();
                    }
                    else
                    {
                        divProjectselection.Visible = true;
                        divProjectselection1.Visible = true;
                        BindProjectList();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.First();
            }
        }

        protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProjects.SelectedIndex != 0)
            {
                Session["Project_Code"] = ddlProjects.SelectedValue;
                ddlProjects.SelectedValue = Session["Project_Code"].ToString();
                Response.Redirect("~/CommonPages/Home.aspx");
            }
            else
            {
                Response.Redirect("~/CommonPages/Login.aspx", false);
                Session.Abandon();
            }
        }

        protected void BindProjectList()
        {
            //try
            //{
            //    ProjectBL objProjectBL = new ProjectBL();
            //    DataSet ds = new DataSet();
            //    objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_ALL, ref ds);
            //    ddlProjects.DataTextField = "Project_Name";
            //    ddlProjects.DataValueField = "Project_Code";
            //    ddlProjects.DataSource = ds;
            //    ddlProjects.DataBind();
            //    ddlProjects.Items.Insert(0, "-Select-");

            //    if (Session["Project_Code"] != null)
            //    {
            //        ddlProjects.SelectedValue = Session["Project_Code"].ToString();
            //    }
            //}
            //catch (Exception)
            //{

            //}
            try
            {
                ProjectBL objProjectBL = new ProjectBL();
                ds = new DataSet();
                DataSet datads = null;
                objProjectBL.Project_Code = "P-0049/KA/19-20/WSS/BWSSB";
                objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_DETAILS_BY_ID, ref datads);
                DataTable Dt1 = new DataTable();
                Dt1 = datads.Tables[0].Clone();
                Dt1.Clear();



                if (Session["UID"] != null)
                {
                    objProjectBL.UserID = Convert.ToInt32(Session["UID"]);
                    objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_USER_DETAILS_BY_ID, ref ds);
                    if (ds.Tables[0].Rows[0]["Role"].ToString() == "Other")
                    {
                        string str = ds.Tables[0].Rows[0]["Project_Code"].ToString();
                        char[] spearator = { ',' };
                        String[] strlist = str.Split(spearator);
                        foreach (var items in strlist)
                        {
                            objProjectBL.Project_Code = items;

                            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_DETAILS_BY_ID, ref datads);
                            foreach (DataRow row in datads.Tables[0].Rows)
                            {
                                if (Convert.ToInt32(row.ItemArray[0].ToString()) < 250)
                                {
                                    Dt1.Rows.Add(row.ItemArray);
                                }
                            }

                        }
                        ddlProjects.DataTextField = "Project_Name";
                        ddlProjects.DataValueField = "Project_Code";
                        ddlProjects.DataSource = Dt1;
                        ddlProjects.DataBind();
                        ddlProjects.Items.Insert(0, "-Select-");
                        if (Session["Project_Code"] != null)
                        {
                            ddlProjects.SelectedValue = Session["Project_Code"].ToString();
                        }
                    }
                    else
                    {
                        objProjectBL = new ProjectBL();
                        ds = new DataSet();
                        //objProjectBL.UserID =Convert.ToInt32(Request.QueryString["ID"]);
                        objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_ALL, ref ds);
                        ddlProjects.DataTextField = "Project_Name";
                        ddlProjects.DataValueField = "Project_Code";
                        ddlProjects.DataSource = ds;
                        ddlProjects.DataBind();
                        ddlProjects.Items.Insert(0, "-Select-");
                        if (Session["Project_Code"] != null)
                        {
                            ddlProjects.SelectedValue = Session["Project_Code"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void PermissionAccess()
        {
            try
            {
                ObjUserBL = new UserBL();
                ds = new DataSet();
                ObjUserBL.Employee_ID = Convert.ToString(Session["Empid"]);

                ObjUserBL.load(con, UserBL.eLoadSp.SELECT_USERS_DETAILS_BY_ID, ref ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Session["ActionAccess"] = ds;

                    if (ds.Tables[0].Rows[0]["Role"].ToString() == "Application Admin")
                    {
                        li_Local_MRN.Visible = true;
                        likUsers.Visible = true;
                        likEmployeeContract.Visible = true;
                        likMailconf.Visible = true;
                        likSite.Visible = true;
                        likProject.Visible = true;
                        likUOM.Visible = true;
                        likMaterial.Visible = true;
                        likVendor.Visible = true;
                        likContractor.Visible = true;
                        likOther.Visible = true;
                        likBudget.Visible = true;
                        likBudgetModification.Visible = true;
                        likMonthlyBudReport.Visible = true;
                        liMonthlyReportAll.Visible = true;
                        likBudgetVarianceReport.Visible = true;
                        likQuotation.Visible = true;
                        likQuotationCompar.Visible = true;
                        likIndent.Visible = true;
                        likPO.Visible = true;
                        likDPO.Visible = true;
                        likWO.Visible = true;
                        likPSO.Visible = true;
                        likWOH.Visible = true;
                        liPaymentIndent.Visible = true;
                        liPaymentIndent_Verification.Visible = true;
                        liPaymentIndent_Approval.Visible = true;
                        likFReport.Visible = true;
                        likStock.Visible = true;
                        likMRN.Visible = true;
                        likMIN.Visible = true;
                        likAgeWiseReport.Visible = true;
                        likAssetRegis.Visible = true;
                        likAssetTran.Visible = true;
                        likDailyRunningHour.Visible = true;
                        //likDRHReport.Visible = true;
                        //likDDReport.Visible = true;
                        // likServiceDetailsReport.Visible = true;
                        likPerformanceReport.Visible = true;
                        likProjectBudget.Visible = true;
                        likstaffSalary.Visible = true;
                        liRFQ.Visible = true;
                        likLWT.Visible = true;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows[0]["Pro_BudgetCreate"].ToString() != "" && Convert.ToBoolean(ds.Tables[0].Rows[0]["Pro_BudgetCreate"].ToString()) || ds.Tables[0].Rows[0]["Pro_BudgetView"].ToString() != "" && Convert.ToBoolean(ds.Tables[0].Rows[0]["Pro_BudgetView"].ToString()) ||
                            ds.Tables[0].Rows[0]["Pro_BudgetUpdate"].ToString() != "" && Convert.ToBoolean(ds.Tables[0].Rows[0]["Pro_BudgetUpdate"].ToString()) || ds.Tables[0].Rows[0]["Pro_BudgetDelete"].ToString() != "" && Convert.ToBoolean(ds.Tables[0].Rows[0]["Pro_BudgetDelete"].ToString()))
                        {
                            likProjectBudget.Visible = true;
                        }
                        else
                        {
                            likProjectBudget.Visible = false;
                        }

                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["UserCreate"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["UserView"].ToString()) ||
                           Convert.ToBoolean(ds.Tables[0].Rows[0]["UserUpdate"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["UserDelete"].ToString()))
                        {
                            likUsers.Visible = true;
                        }
                        else
                        {
                            likUsers.Visible = false;
                        }

                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["EmpContract_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["EmpContract_View"].ToString()) ||
                           Convert.ToBoolean(ds.Tables[0].Rows[0]["EmpContract_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["EmpContract_Delete"].ToString()))
                        {
                            likEmployeeContract.Visible = true;
                        }
                        else
                        {
                            likEmployeeContract.Visible = false;
                        }

                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Mail_update"].ToString()))
                        {
                            likMailconf.Visible = true;
                        }
                        else
                        {
                            likMailconf.Visible = false;
                        }


                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["CompySite_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["CompySite_View"].ToString()) ||
                           Convert.ToBoolean(ds.Tables[0].Rows[0]["CompySite_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["CompSite_Delete"].ToString()))
                        {
                            likSite.Visible = true;
                        }
                        else
                        {
                            likSite.Visible = false;
                        }


                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Proj_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["Proj_View"].ToString()) ||
                          Convert.ToBoolean(ds.Tables[0].Rows[0]["Proj_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["Proj_Delete"].ToString()))
                        {
                            likProject.Visible = true;
                        }
                        else
                        {
                            likProject.Visible = false;
                        }


                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["UOM_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["UOM_View"].ToString()) ||
                          Convert.ToBoolean(ds.Tables[0].Rows[0]["UOM_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["UOM_Delete"].ToString()))
                        {
                            likUOM.Visible = true;
                        }
                        else
                        {
                            likUOM.Visible = false;
                        }


                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Material_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["Material_View"].ToString()) ||
                         Convert.ToBoolean(ds.Tables[0].Rows[0]["Material_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["Material_Delete"].ToString()))
                        {
                            likMaterial.Visible = true;
                        }
                        else
                        {
                            likMaterial.Visible = false;
                        }

                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Vendor_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["Vendor_View"].ToString()) ||
                        Convert.ToBoolean(ds.Tables[0].Rows[0]["Vendor_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["Vendor_Delete"].ToString()))
                        {
                            likVendor.Visible = true;
                        }
                        else
                        {
                            likVendor.Visible = false;
                        }


                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Sub_Cont_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["Sub_Cont_View"].ToString()) ||
                           Convert.ToBoolean(ds.Tables[0].Rows[0]["Sub_Cont_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["Sub_Cont_Delete"].ToString()))
                        {
                            likContractor.Visible = true;
                        }
                        else
                        {
                            likContractor.Visible = false;
                        }

                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Other_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["Other_View"].ToString()) ||
                           Convert.ToBoolean(ds.Tables[0].Rows[0]["Other_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["Other_Delete"].ToString()))
                        {
                            likOther.Visible = true;
                        }
                        else
                        {
                            likOther.Visible = false;
                        }


                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Bug_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["Bug_View"].ToString()) ||
                           Convert.ToBoolean(ds.Tables[0].Rows[0]["Bug_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["Bug_Delete"].ToString()))
                        {
                            likBudget.Visible = true;
                        }
                        else
                        {
                            likBudget.Visible = false;
                        }


                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Bug_Mod_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["Bug_Mod_View"].ToString()) ||
                          Convert.ToBoolean(ds.Tables[0].Rows[0]["Bug_Mod_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["Bug_Mod_Delete"].ToString()))
                        {
                            likBudgetModification.Visible = true;
                        }
                        else
                        {
                            likBudgetModification.Visible = false;
                        }


                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Bug_Reports"].ToString()))
                        {
                            likMonthlyBudReport.Visible = true;
                            likBudgetVarianceReport.Visible = true;
                        }
                        else
                        {
                            likMonthlyBudReport.Visible = false;
                            likBudgetVarianceReport.Visible = false;
                        }


                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Quotn_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["Quotn_View"].ToString()) ||
                            Convert.ToBoolean(ds.Tables[0].Rows[0]["Quotn_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["Quotn_Delete"].ToString()))
                        {
                            likQuotation.Visible = true;
                        }
                        else
                        {
                            likQuotation.Visible = false;
                        }

                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Quotn_Compare"].ToString()))
                        {
                            likQuotationCompar.Visible = true;

                        }
                        else
                        {
                            likQuotationCompar.Visible = false;

                        }

                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Ind_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["Ind_View"].ToString()) ||
                          Convert.ToBoolean(ds.Tables[0].Rows[0]["Ind_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["Ind_Delete"].ToString()))
                        {
                            likIndent.Visible = true;
                            liPaymentIndent.Visible = true;
                            liPaymentIndent_Verification.Visible = true;
                            liPaymentIndent_Approval.Visible = true;
                        }
                        else
                        {
                            likIndent.Visible = false;
                            liPaymentIndent.Visible = false;
                            liPaymentIndent_Verification.Visible = true;
                            liPaymentIndent_Approval.Visible = false;
                        }


                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["PO_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["PO_View"].ToString()) ||
                         Convert.ToBoolean(ds.Tables[0].Rows[0]["PO_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["PO_Delete"].ToString()))
                        {
                            likPO.Visible = true;
                            likDPO.Visible = true;
                            likWO.Visible = true;
                            likWOH.Visible = true;
                            likPSO.Visible = true;
                        }
                        else
                        {
                            likPO.Visible = false;
                            likDPO.Visible = false;
                            likWO.Visible = false;
                            likWOH.Visible = false;
                            likPSO.Visible = false;
                        }

                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_View"].ToString()) ||
                         Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_Delete"].ToString()))
                        {
                            liPaymentIndent.Visible = true;
                        }
                        else
                        {
                            liPaymentIndent.Visible = false;
                        }

                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_Ver_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_Ver_View"].ToString()) ||
                         Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_Ver_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_Ver_Delete"].ToString()))
                        {
                            liPaymentIndent_Verification.Visible = true;
                        }
                        else
                        {
                            liPaymentIndent_Verification.Visible = false;
                        }

                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_App_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_App_View"].ToString()) ||
                         Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_App_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["PayInd_App_Delete"].ToString()))
                        {
                            liPaymentIndent_Approval.Visible = true;
                        }
                        else
                        {
                            liPaymentIndent_Approval.Visible = false;
                        }

                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Proc_Reports"].ToString()))
                        {
                            likFReport.Visible = true;
                        }
                        else
                        {
                            likFReport.Visible = false;
                        }


                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Stock_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["Stock_View"].ToString()) ||
                        Convert.ToBoolean(ds.Tables[0].Rows[0]["Stock_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["Stock_Delete"].ToString()))
                        {
                            likStock.Visible = true;
                        }
                        else
                        {
                            likStock.Visible = false;
                        }


                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["MRN_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["MRN_Delete"].ToString()) ||
                        Convert.ToBoolean(ds.Tables[0].Rows[0]["MRN_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["MRN_View"].ToString()))
                        {
                            likMRN.Visible = true;
                        }
                        else
                        {
                            likMRN.Visible = false;
                        }


                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["MIN_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["MIN_View"].ToString()) ||
                       Convert.ToBoolean(ds.Tables[0].Rows[0]["MIN_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["MIN_Delete"].ToString()))
                        {
                            likMIN.Visible = true;
                        }
                        else
                        {
                            likMIN.Visible = false;
                        }


                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Inv_Reports"].ToString()))
                        {
                            likAgeWiseReport.Visible = true;
                        }
                        else
                        {
                            likAgeWiseReport.Visible = false;
                        }



                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["AssReg_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["AssReg_View"].ToString()) ||
                         Convert.ToBoolean(ds.Tables[0].Rows[0]["AssReg_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["AssReg_Delete"].ToString()))
                        {
                            likAssetRegis.Visible = true;
                        }
                        else
                        {
                            likAssetRegis.Visible = false;
                        }


                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["AssTra_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["AssTra_View"].ToString()) ||
                         Convert.ToBoolean(ds.Tables[0].Rows[0]["AssTra_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["AssTra_Delete"].ToString()))
                        {
                            likAssetTran.Visible = true;
                        }
                        else
                        {
                            likAssetTran.Visible = false;
                        }


                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["DailRun_Create"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["DailRun_View"].ToString()) ||
                            Convert.ToBoolean(ds.Tables[0].Rows[0]["DailRun_Update"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["DailRun_Delete"].ToString()))
                        {
                            likDailyRunningHour.Visible = true;
                        }
                        else
                        {
                            likDailyRunningHour.Visible = false;
                        }


                        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Ass_Rep"].ToString()))
                        {
                            //likDRHReport.Visible = true;
                            //likDDReport.Visible = true;
                            //likServiceDetailsReport.Visible = true;
                            likPerformanceReport.Visible = true;
                        }
                        else
                        {
                            //likDRHReport.Visible = false;
                            //likDDReport.Visible = false;
                            //likServiceDetailsReport.Visible = false;
                            likPerformanceReport.Visible = false;
                        }
                    }

                }
                else
                {
                    Session["ActionAccess"] = null;
                }
            }
            catch (Exception ex)
            {
                ex.Message.First();
            }
        }

        protected void linkLogout_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/CommonPages/Login.aspx", false);
                Session.RemoveAll();
            }
            catch (Exception ex)
            {
                ex.Message.First();
            }
        }
    }
}




