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


public partial class GrandAbstract : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    GrandAbstractBL objGrandAbstractBL = null;
    BudgetBL objbudgetbl = null;
    DataSet ds = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UID"] != null)
                {
                    if (Request.QueryString["ApprovedID"] != null)
                    {
                        GetBudgetDetail(Convert.ToInt32(Request.QueryString["ApprovedID"].ToString()));
                    }
                }
                else
                {
                    Response.Redirect("../CommonPages/Login.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void GetBudgetDetail(int AbsBid)
    {
        try
        {
            objbudgetbl = new BudgetBL();
            ds = new DataSet();
            objbudgetbl.Abs_BID = AbsBid;

            objbudgetbl.load(con, BudgetBL.eLoadSp.SELECT_BY_ID, ref ds);

            if (ds.Tables[0].Rows.Count > 0)
            {

                txtBudgetAutomobiles.Text = ds.Tables[0].Rows[0]["Auto_amount_Budget"].ToString();
                txtBudegtedPM.Text = ds.Tables[0].Rows[0]["PlMach_Amount_Budget"].ToString();
                txtBudgetShutteringMaterial.Text = ds.Tables[0].Rows[0]["Shutter_Mat_Amount_Budget"].ToString();
                txtBudgetConsumableItems.Text = ds.Tables[0].Rows[0]["Consumable_Amount_Budget"].ToString();
                txtBudgetElectricalItems.Text = ds.Tables[0].Rows[0]["Elec_Amoun_Budgett"].ToString();
                txtBudgetHSD.Text = ds.Tables[0].Rows[0]["HSD_Pet_Amount_Budget"].ToString();
                txtBudgetPOL.Text = ds.Tables[0].Rows[0]["Oil_Lube_Amount_Budget"].ToString();
                txtBudgetHardwareItems.Text = ds.Tables[0].Rows[0]["Hardware_Amount_Budget"].ToString();
                txtBudgetWeldingElectrodes.Text = ds.Tables[0].Rows[0]["Weld_Elec_Amount_Budget"].ToString();
                txtBudgetOxygenAcetylene.Text = ds.Tables[0].Rows[0]["Oxygen_ace_Amount_Budget"].ToString();
                txtBudgetSafetyItems.Text = ds.Tables[0].Rows[0]["Safety_Item_Budget"].ToString();
                txtBudgetStaffWelfare.Text = ds.Tables[0].Rows[0]["Staff_wel_Amount_Budget"].ToString();
                txtBudgetMessExpenditure.Text = ds.Tables[0].Rows[0]["Mess_Expense_amount_Budget"].ToString();
                txtBudgetPrintingStationary.Text = ds.Tables[0].Rows[0]["Print_Sta_Amount_Budget"].ToString();
                txtBudgetRepairMaintenance.Text = ds.Tables[0].Rows[0]["Repair_Maint_Amount_Budget"].ToString();
                txtBudgetedBOQitems.Text = ds.Tables[0].Rows[0]["BOQ_Amount_Budget"].ToString();
                txtBudgetSnitaryMaterials.Text = ds.Tables[0].Rows[0]["Sanitary_Amount_Budget"].ToString();
                txtBudgetBlastingMaterial.Text = ds.Tables[0].Rows[0]["Blast_ma_Amount_Budget"].ToString();
                txtBudgetFurnituresFixtures.Text = ds.Tables[0].Rows[0]["FnF_Amount_Budget"].ToString();
                txtBudgetAssets.Text = ds.Tables[0].Rows[0]["Fix_Asset_Amount_Budget"].ToString();
                txtBudgetInfItems.Text = ds.Tables[0].Rows[0]["Infra_Amount_Budget"].ToString();
                txtBudgetSand.Text = ds.Tables[0].Rows[0]["Sand_Amount_Budget"].ToString();
                txtBudgetJelly.Text = ds.Tables[0].Rows[0]["Jelly_Metal_Amount_Budget"].ToString();
                txtBudgetRedSoil.Text = ds.Tables[0].Rows[0]["Red_Soil_Budget"].ToString();
                txtBudgetCement.Text = ds.Tables[0].Rows[0]["Cement_Budget"].ToString();
                txtBudgetChemical.Text = ds.Tables[0].Rows[0]["Chem_Amount_Budget"].ToString();
                txtBudgetSteels.Text = ds.Tables[0].Rows[0]["Steel_Amount_Budget"].ToString();
                txtBudgetBricks.Text = ds.Tables[0].Rows[0]["Brick_Amount_Budget"].ToString();
                txtBudgetOthers.Text = ds.Tables[0].Rows[0]["Other_Amount_Budget"].ToString();
                txtBudgetOtherCM.Text = ds.Tables[0].Rows[0]["Oth_Const_Amount_Budget"].ToString();         
                   

                if (ds.Tables[0].Rows[0]["Auto_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead1.Visible = true;
                    rowauto.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtAutomobile.Text = ds.Tables[0].Rows[0]["Auto_amount"].ToString();
                    

                }
               

                if (ds.Tables[0].Rows[0]["PlMach_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead1.Visible = true;
                    rowplant.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtPlantMachinery.Text = ds.Tables[0].Rows[0]["PlMach_Amount"].ToString();

                   
                   
                }


                if (ds.Tables[0].Rows[0]["Shutter_Mat_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead1.Visible = true;
                    rowshutt.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtShutteringMaterials.Text = ds.Tables[0].Rows[0]["Shutter_Mat_Amount"].ToString();
                    
                }


                if (ds.Tables[0].Rows[0]["Consumable_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead1.Visible = true;
                    rowconsum.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtConsumableItems.Text = ds.Tables[0].Rows[0]["Consumable_Amount"].ToString();
                   
                    
                }


                if (ds.Tables[0].Rows[0]["Elec_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead1.Visible = true;
                    rowelec.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtElectricaItem.Text = ds.Tables[0].Rows[0]["Elec_Amount"].ToString();
                    
                    
                }

                if (ds.Tables[0].Rows[0]["HSD_Pet_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead1.Visible = true;
                    rowhsd.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtHSDPetrol.Text = ds.Tables[0].Rows[0]["HSD_Pet_Amount"].ToString();
                    
                    
                }

                if (ds.Tables[0].Rows[0]["Oil_Lube_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead1.Visible = true;
                    rowoil.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtOilLubricants.Text = ds.Tables[0].Rows[0]["Oil_Lube_Amount"].ToString();
                    
                   
                }

                if (ds.Tables[0].Rows[0]["Hardware_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead1.Visible = true;
                    rowhard.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtHardwareItems.Text = ds.Tables[0].Rows[0]["Hardware_Amount"].ToString();
                    
                    
                }

                if (ds.Tables[0].Rows[0]["Weld_Elec_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead1.Visible = true;
                    rowwelding.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtWeldingElectrodes.Text = ds.Tables[0].Rows[0]["Weld_Elec_Amount"].ToString();
                    
                }

                if (ds.Tables[0].Rows[0]["Oxygen_ace_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead1.Visible = true;
                    rowoxygen.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtOxygenAcetyleneGas.Text = ds.Tables[0].Rows[0]["Oxygen_ace_Amount"].ToString();
                    
                   
                }

                if (ds.Tables[0].Rows[0]["Safety_Item_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead1.Visible = true;
                    rowsafety.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtSafetyItems.Text = ds.Tables[0].Rows[0]["Safety_Item"].ToString();
                  
                    
                }


                if (ds.Tables[0].Rows[0]["Staff_wel_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead1.Visible = true;
                    rowstaff.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtStaffWelfare.Text = ds.Tables[0].Rows[0]["Staff_wel_Amount"].ToString();
                  
                   
                }


                if (ds.Tables[0].Rows[0]["Mess_Expense_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead1.Visible = true;
                    rowmessexp.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtMessExpenditure.Text = ds.Tables[0].Rows[0]["Mess_Expense_amount"].ToString();
                   
                   
                }

                if (ds.Tables[0].Rows[0]["Print_Sta_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead1.Visible = true;
                    rowprin.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtPrintingStationery.Text = ds.Tables[0].Rows[0]["Print_Sta_Amount"].ToString();
                
                }

                if (ds.Tables[0].Rows[0]["Repair_Maint_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead1.Visible = true;
                    rowrepairs.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtRepairsMaintenance.Text = ds.Tables[0].Rows[0]["Repair_Maint_Amount"].ToString();
                    
                }


                if (ds.Tables[0].Rows[0]["BOQ_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead1.Visible = true;
                    rowBOQ.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtBOQItems.Text = ds.Tables[0].Rows[0]["BOQ_Amount"].ToString();
                    
                   
                }

                if ( ds.Tables[0].Rows[0]["Sanitary_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead1.Visible = true;
                    rowsanitary.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtSanitaryMaterials.Text = ds.Tables[0].Rows[0]["Sanitary_Amount"].ToString();
                  
                }

                if (ds.Tables[0].Rows[0]["Blast_ma_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead1.Visible = true;
                    rowblasting.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtBlastingMaterials.Text = ds.Tables[0].Rows[0]["Blast_ma_Amount"].ToString();
                   
                }

                if (ds.Tables[0].Rows[0]["FnF_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead1.Visible = true;
                    rowfurnitures.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtFurnituresFixtures.Text = ds.Tables[0].Rows[0]["FnF_Amount"].ToString();
                  
                   
                }

                if (ds.Tables[0].Rows[0]["Fix_Asset_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead1.Visible = true;
                    rowfixed.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtFixedAssets.Text = ds.Tables[0].Rows[0]["Fix_Asset_Amount"].ToString();
                   
                   
                }

                if (ds.Tables[0].Rows[0]["Infra_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead1.Visible = true;
                    rowinfra.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtInfrastructureItems.Text = ds.Tables[0].Rows[0]["Infra_Amount"].ToString();
                  
                   
                }


                if (ds.Tables[0].Rows[0]["Sand_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead2.Visible = true;
                    rowsand.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtsand.Text = ds.Tables[0].Rows[0]["Sand_Amount"].ToString();
                  
                   
                }

                if (ds.Tables[0].Rows[0]["Jelly_Metal_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead2.Visible = true;
                    rowjelly.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtJellyMetalAgr.Text = ds.Tables[0].Rows[0]["Jelly_Metal_Amount"].ToString();
                  
                   
                }

                if (ds.Tables[0].Rows[0]["Red_Soil_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead2.Visible = true;
                    rowred.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtredsoil.Text = ds.Tables[0].Rows[0]["Red_Soil"].ToString();
                  
                   
                }

                if (ds.Tables[0].Rows[0]["Cement_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead2.Visible = true;
                    rowcement.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtcement.Text = ds.Tables[0].Rows[0]["Cement"].ToString();
                 
                   
                }

                if (ds.Tables[0].Rows[0]["Chem_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead2.Visible = true;
                    rowchemical.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtchemicalamt.Text = ds.Tables[0].Rows[0]["Chem_Amount"].ToString();
                    
                   
                }

                if (ds.Tables[0].Rows[0]["Steel_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead2.Visible = true;
                    rowsteels.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtSteels.Text = ds.Tables[0].Rows[0]["Steel_Amount"].ToString();
                   
                   
                }
                if (ds.Tables[0].Rows[0]["Brick_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowhead2.Visible = true;
                    rowbricks.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtBricks.Text = ds.Tables[0].Rows[0]["Brick_Amount"].ToString();
                   
                   
                }

                if (ds.Tables[0].Rows[0]["Other_ApprovalStatus"].ToString().Trim() == "Modified")
                {
                    rowother1.Visible = true;
                    btnDecision.Visible = true;
                    btnCancel.Visible = true;
                    txtothers.Text = ds.Tables[0].Rows[0]["Other_Amount"].ToString();
                   
                   
                }

                 if (ds.Tables[0].Rows[0]["Oth_Const_ApprovalStatus"].ToString().Trim() == "Modified")
                 {
                     rowhead2.Visible = true;
                     rowother2.Visible = true;
                     btnDecision.Visible = true;
                     btnCancel.Visible = true;
                     txtOthersconst.Text = ds.Tables[0].Rows[0]["Oth_Const_Amount"].ToString();
                    
                    
                 }               
             
                EnableControl();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
       
    }
    public void EnableControl()
    {
        txtAutomobile.Enabled = false;
        txtPlantMachinery.Enabled = false;
        txtShutteringMaterials.Enabled = false;
        txtConsumableItems.Enabled = false;
        txtElectricaItem.Enabled = false;
        txtHSDPetrol.Enabled = false;
        txtOilLubricants.Enabled = false;
        txtHardwareItems.Enabled = false;
        txtWeldingElectrodes.Enabled = false;
        txtOxygenAcetyleneGas.Enabled = false;
        txtSafetyItems.Enabled = false;
        txtStaffWelfare.Enabled = false;
        txtMessExpenditure.Enabled = false;
        txtPrintingStationery.Enabled = false;
        txtRepairsMaintenance.Enabled = false;
        txtBOQItems.Enabled = false;
        txtSanitaryMaterials.Enabled = false;
        txtBlastingMaterials.Enabled = false;
        txtFurnituresFixtures.Enabled = false;
        txtFixedAssets.Enabled = false;
        txtInfrastructureItems.Enabled = false;
        txtsand.Enabled = false;
        txtJellyMetalAgr.Enabled = false;
        txtredsoil.Enabled = false;
        txtcement.Enabled = false;
        txtchemicalamt.Enabled = false;
        txtSteels.Enabled = false;
        txtBricks.Enabled = false;
        txtothers.Enabled = false;
        txtOthersconst.Enabled = false;
    }

    
    protected void btnview_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Redirect("../Budget/MonthlyBudget.aspx?Abs_BID=" + Request.QueryString["ApprovedID"].ToString() +"&MFYRQ=0",false);

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    protected void btnDecision_Click(object sender, EventArgs e)
    {
        try
        {
            objGrandAbstractBL = new GrandAbstractBL();
            objGrandAbstractBL.ABS_BID = Convert.ToInt32(Request.QueryString["ApprovedID"].ToString());
            objGrandAbstractBL.Budget_ID = Request.QueryString["BudgetID"].ToString();

            if (rowauto.Visible)
            {
                objGrandAbstractBL.Auto_amount = Convert.ToDecimal(txtAutomobile.Text);
                objGrandAbstractBL.Auto_Status = AutoddlStatus.SelectedIndex != 0 ? AutoddlStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Auto_Remarks = txtAutoRemarks.Text;
               
            }

            if (rowplant.Visible)
            {
                objGrandAbstractBL.PlMach_Amount = Convert.ToDecimal(txtPlantMachinery.Text);
                objGrandAbstractBL.PlMach_Status = PlantddStatus.SelectedIndex != 0 ? PlantddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.PlMach_Remarks = txtPlantRemarks.Text;
               
            }

            if (rowshutt.Visible)
            {
                objGrandAbstractBL.Shutter_Mat_Amount = Convert.ToDecimal(txtShutteringMaterials.Text);
                objGrandAbstractBL.Shutter_Mat_Status = StutterddStatus.SelectedIndex != 0 ? StutterddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Shutter_Mat_Remarks = txtstutterRemarks.Text;

                 }

            if (rowconsum.Visible)
            {
                objGrandAbstractBL.Consumable_Amount = Convert.ToDecimal(txtConsumableItems.Text);
                objGrandAbstractBL.Consumable_Status = ConsuddStatus.SelectedIndex != 0 ? ConsuddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Consumable_Remarks = txtConRemarks.Text;

              
            }

            if (rowelec.Visible)
            {
                objGrandAbstractBL.Elec_Amount = Convert.ToDecimal(txtElectricaItem.Text);
                objGrandAbstractBL.Elec_Status = ElecddStatus.SelectedIndex != 0 ? ElecddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Elec_Remarks = txtElecRemarks.Text;
               
            }

            if (rowhsd.Visible)
            {
                objGrandAbstractBL.HSD_Pet_Amount = Convert.ToDecimal(txtHSDPetrol.Text);
                objGrandAbstractBL.HSD_Pet_Status = HSDddStatus.SelectedIndex != 0 ? HSDddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.HSD_Pet_Remarks = txtHSDRemarks.Text;
              
            }

            if (rowoil.Visible)
            {
                objGrandAbstractBL.Oil_Lube_Amount = Convert.ToDecimal(txtOilLubricants.Text);
                objGrandAbstractBL.Oil_Lube_Status = OilddStatus.SelectedIndex != 0 ? OilddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Oil_Lube_Remarks = txtOilRemarks.Text;
             
            }

            if (rowhard.Visible)
            {
                objGrandAbstractBL.Hardware_Amount = Convert.ToDecimal(txtHardwareItems.Text);
                objGrandAbstractBL.Hardware_Status = HardddStatus.SelectedIndex != 0 ? HardddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Hardware_Remarks = txtHardRemarks.Text;
                }

            if (rowwelding.Visible)
            {
                objGrandAbstractBL.Weld_Elec_Amount = Convert.ToDecimal(txtWeldingElectrodes.Text);
                objGrandAbstractBL.Weld_Elec_Status = WeldddStatus.SelectedIndex != 0 ? WeldddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Weld_Elec_Remarks = txtWeldRemarks.Text;
                 }

            if (rowoxygen.Visible)
            {
                objGrandAbstractBL.Oxygen_ace_Amount = Convert.ToDecimal(txtOxygenAcetyleneGas.Text);
                objGrandAbstractBL.Oxygen_ace_Status = OxyddStatus.SelectedIndex != 0 ? OxyddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Oxygen_ace_Remarks = txtOxyRemarks.Text;
                
            }

            if (rowsafety.Visible)
            {
                objGrandAbstractBL.Safety_Item = Convert.ToDecimal(txtSafetyItems.Text);
                objGrandAbstractBL.Safety_Item_Status = SafyddStatus.SelectedIndex != 0 ? SafyddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Safety_Item_Remarks = txtSafRemarks.Text;
               
            }

            if (rowstaff.Visible)
            {
                objGrandAbstractBL.Staff_wel_Amount = Convert.ToDecimal(txtStaffWelfare.Text);
                objGrandAbstractBL.Staff_wel_Status = StaffddStatus.SelectedIndex != 0 ? StaffddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Staff_wel_Remarks = txtStaffRemarks.Text;
               
            }

            if (rowmessexp.Visible)
            {
                objGrandAbstractBL.Mess_Expense_amount = Convert.ToDecimal(txtMessExpenditure.Text);
                objGrandAbstractBL.Mess_Expense_Status = MessddStatus.SelectedIndex != 0 ? MessddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Mess_Expense_Remarks = txtMessReamrks.Text;
              
            }

       
            
            
            if (rowprin.Visible)
            {
                objGrandAbstractBL.Print_Sta_Amount = Convert.ToDecimal(txtPrintingStationery.Text);
                objGrandAbstractBL.Print_Sta_Status = PrintddStatus.SelectedIndex != 0 ? PrintddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Print_Sta_Remarks = txtPrintRemarks.Text;
              
            }

            if (rowrepairs.Visible)
            {
                objGrandAbstractBL.Repair_Maint_Amount = Convert.ToDecimal(txtRepairsMaintenance.Text);
                objGrandAbstractBL.Repair_Maint_Status = RepairddStatus.SelectedIndex != 0 ? RepairddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Repair_Maint_Remarks = txtRepairRemarks.Text;
               
            }

            if (rowBOQ.Visible)
            {
                objGrandAbstractBL.BOQ_Amount = Convert.ToDecimal(txtBOQItems.Text);
                objGrandAbstractBL.BOQ_Status = BOQddStatus.SelectedIndex != 0 ? BOQddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.BOQ_Remarks = txtBOQRemarks.Text;
              
            }

            if (rowsanitary.Visible)
            {
                objGrandAbstractBL.Sanitary_Amount = Convert.ToDecimal(txtSanitaryMaterials.Text);
                objGrandAbstractBL.Sanitary_Status = SaniddStatus.SelectedIndex != 0 ? SaniddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Sanitary_Remarks = txtsaniRemarks.Text;
              
            }


            if (rowblasting.Visible)
            {
                objGrandAbstractBL.Blast_ma_Amount = Convert.ToDecimal(txtBlastingMaterials.Text);
                objGrandAbstractBL.Blast_ma_Status = BlastddStatus.SelectedIndex != 0 ? BlastddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Blast_ma_Remarks = txtBlasRemarks.Text;
               
            }

            if (rowfurnitures.Visible)
            {
                objGrandAbstractBL.FnF_Amount = Convert.ToDecimal(txtFurnituresFixtures.Text);
                objGrandAbstractBL.FnF_Status = FurniddStatus.SelectedIndex != 0 ? FurniddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.FnF_Remarks = txtFurnRemarks.Text;
              
            }


            if (rowfixed.Visible)
            {
                objGrandAbstractBL.Fix_Asset_Amount = Convert.ToDecimal(txtFixedAssets.Text);
                objGrandAbstractBL.Fix_Asset_Status = FixedddStatus.SelectedIndex != 0 ? FixedddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Fix_Asset_Remarks = txtFixedRemarks.Text;
                
           
            }

            if (rowinfra.Visible)
            {
                objGrandAbstractBL.Infra_Amount = Convert.ToDecimal(txtInfrastructureItems.Text);
                objGrandAbstractBL.Infra_Status = InfrddStatus.SelectedIndex != 0 ? InfrddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Infra_Remarks = txtInfraRemarks.Text;
             
           
            }

            if (rowsand.Visible)
            {
                objGrandAbstractBL.Sand_Amount = Convert.ToDecimal(txtsand.Text);
                objGrandAbstractBL.Sand_Status = SandddStatus.SelectedIndex != 0 ? SandddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Sand_Remarks = txtSanRemarks.Text;
               
           
            }

            if (rowjelly.Visible)
            {
                objGrandAbstractBL.Jelly_Metal_Amount = Convert.ToDecimal(txtJellyMetalAgr.Text);
                objGrandAbstractBL.Jelly_Metal_Status = JellddStatus.SelectedIndex != 0 ? JellddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Jelly_MetalRemarks = txtJellyRemarks.Text;
              
           
            }

            if (rowred.Visible)
            {
                objGrandAbstractBL.Red_Soil = Convert.ToDecimal(txtredsoil.Text);
                objGrandAbstractBL.Red_Soil_Status = RedddStatus.SelectedIndex != 0 ? RedddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Red_Soil_Remarks = txtRedRemarks.Text;
               
           
            }

            if (rowcement.Visible)
            {
                objGrandAbstractBL.Cement = Convert.ToDecimal(txtcement.Text);
                objGrandAbstractBL.Cement_Status = CementddStatus.SelectedIndex != 0 ? CementddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Cement_Remarks = txtCenmeRemarks.Text;
               
           
            }


            if (rowchemical.Visible)
            {
                objGrandAbstractBL.Chem_Amount = Convert.ToDecimal(txtchemicalamt.Text);
                objGrandAbstractBL.Chem_Status = ChemiddStatus.SelectedIndex != 0 ? ChemiddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Chem_Remarks = txtChemiRemarks.Text;
               
           
            }

            if (rowbricks.Visible)
            {
                objGrandAbstractBL.Brick_Amount = Convert.ToDecimal(txtBricks.Text);
                objGrandAbstractBL.Brick_Status =  BricksddStatus.SelectedIndex != 0 ? BricksddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Brick_Remarks = txtBricksRmarks.Text;
             
           
            }

            if (rowsteels.Visible)
            {
                objGrandAbstractBL.Steel_Amount = Convert.ToDecimal(txtSteels.Text);
                objGrandAbstractBL.Steel_Status = SteelddStatus.SelectedIndex != 0 ? SteelddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Steel_Remarks = txtSteelRemarks.Text;
               
            }

            if (rowother1.Visible)
            {
                objGrandAbstractBL.Other_Amount = Convert.ToDecimal(txtothers.Text);
                objGrandAbstractBL.Other_Status = otherddStatus.SelectedIndex != 0 ? otherddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Other_Remarks = txtOtherRemarks.Text;
               
           
            }
            if (rowother2.Visible)  
            {
                objGrandAbstractBL.Oth_Const_Amount = Convert.ToDecimal(txtOthersconst.Text);
                objGrandAbstractBL.Oth_Const_Status = OtherConddStatus.SelectedIndex != 0 ? OtherConddStatus.SelectedItem.Text : string.Empty;
                objGrandAbstractBL.Oth_Const_Remarks = txttherConRemarks.Text;
             
           
            }


            objGrandAbstractBL.Auto_amount_Budget = Convert.ToDecimal(txtBudgetAutomobiles.Text);
            objGrandAbstractBL.PlMach_Amount_Budget = Convert.ToDecimal(txtBudegtedPM.Text);
            objGrandAbstractBL.Shutter_Mat_Amount_Budgeted = Convert.ToDecimal(txtBudgetShutteringMaterial.Text);
            objGrandAbstractBL.Consumable_Amount_Budgeted = Convert.ToDecimal(txtBudgetConsumableItems.Text);
            objGrandAbstractBL.Elec_Amount_Budgeted = Convert.ToDecimal(txtBudgetElectricalItems.Text);
            objGrandAbstractBL.HSD_Pet_Amount_Budgeted = Convert.ToDecimal(txtBudgetHSD.Text);
            objGrandAbstractBL.Oil_Lube_Amount_Budgeted = Convert.ToDecimal(txtBudgetPOL.Text);
            objGrandAbstractBL.Hardware_Amount_Budgeted = Convert.ToDecimal(txtBudgetHardwareItems.Text);
            objGrandAbstractBL.Weld_Elec_Amount_Budgeted = Convert.ToDecimal(txtBudgetWeldingElectrodes.Text);
            objGrandAbstractBL.Oxygen_ace_Amount_Budgeted = Convert.ToDecimal(txtBudgetOxygenAcetylene.Text);
            objGrandAbstractBL.Safety_Item_Amount_Budgeted = Convert.ToDecimal(txtBudgetSafetyItems.Text);
            objGrandAbstractBL.Staff_wel_Amount_Budgeted = Convert.ToDecimal(txtBudgetStaffWelfare.Text);
            objGrandAbstractBL.Mess_Expense_amount_Budgeted = Convert.ToDecimal(txtBudgetMessExpenditure.Text);
            objGrandAbstractBL.Print_Sta_Amount_Budgeted = Convert.ToDecimal(txtBudgetPrintingStationary.Text);
            objGrandAbstractBL.Repair_Maint_Amount_Budgeted = Convert.ToDecimal(txtBudgetRepairMaintenance.Text);
            objGrandAbstractBL.BOQ_Amount_Budgeted = Convert.ToDecimal(txtBudgetedBOQitems.Text);
            objGrandAbstractBL.Sanitary_Amount_Budgeted = Convert.ToDecimal(txtBudgetSnitaryMaterials.Text);
            objGrandAbstractBL.Blast_ma_Amount_Budgeted = Convert.ToDecimal(txtBudgetBlastingMaterial.Text);


            objGrandAbstractBL.Oth_Const_Amount_Budgeted = Convert.ToDecimal(txtBudgetOtherCM.Text);
            objGrandAbstractBL.Other_Amount_Budgeted = Convert.ToDecimal(txtBudgetOthers.Text);
            objGrandAbstractBL.FnF_Status_Budgeted = Convert.ToDecimal(txtBudgetFurnituresFixtures.Text);
            objGrandAbstractBL.Fix_Asset_Amount_Budgeted = Convert.ToDecimal(txtBudgetAssets.Text);
            objGrandAbstractBL.Sand_Amount_Budgeted = Convert.ToDecimal(txtBudgetSand.Text);
            objGrandAbstractBL.Infra_Amount_Budgeted = Convert.ToDecimal(txtBudgetInfItems.Text);
            objGrandAbstractBL.Jelly_Metal_Amount_Budgeted = Convert.ToDecimal(txtBudgetJelly.Text);
            objGrandAbstractBL.Red_Soil_amount_Budgeted = Convert.ToDecimal(txtBudgetRedSoil.Text);
            objGrandAbstractBL.Chem_Amount_Budgeted = Convert.ToDecimal(txtBudgetChemical.Text);
            objGrandAbstractBL.Cement_amount_Budgeted = Convert.ToDecimal(txtBudgetCement.Text);


            objGrandAbstractBL.Brick_Amount_Budgeted = Convert.ToDecimal(txtBudgetBricks.Text);
            objGrandAbstractBL.Steel_Amount_Budgeted = Convert.ToDecimal(txtBudgetSteels.Text);
           
           
           
          
           
           
          
           
           
            
           
          
           
            
           
          
            
            
            
           
          
         
          











            if (objGrandAbstractBL.insert(con, GrandAbstractBL.eLoadSp.INSERT))
            {
                btnDecision.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Grand abstract has been updated sucessfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to  update the grand abstract');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("BudgetApproval.aspx", false);
    }

    protected void txtBudgetHardwareItems_TextChanged(object sender, EventArgs e)
    {
        TextBox txtName = (TextBox) sender;
        BudgetBL objGrandAbstractBL = new BudgetBL();
        
        objGrandAbstractBL.ABS_BID = Convert.ToInt32(Request.QueryString["ApprovedID"].ToString());
        objGrandAbstractBL.Budget_ID = Request.QueryString["BudgetID"].ToString();
        objGrandAbstractBL.SECTOR_NAME = (txtName.ID);
        if (txtName.Text != string.Empty && txtName.ID != "txtSafRemarks")
        {
            objGrandAbstractBL.SECTORVALUE = Convert.ToDecimal (txtName.Text);
        }

        objGrandAbstractBL.load(con, BudgetBL.eLoadSp.GET_BUDGET_VALIDATION, ref ds);

        
         if (ds.Tables.Count > 0)
         {
            
             ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('You cannot go beyond " + (ds.Tables[0].Rows[0]["ValidationText"].ToString()) + "');", true);
             txtName.Text = "0";
         }
    }
}
