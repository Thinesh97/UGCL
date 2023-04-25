<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Budget/GrandAbstract.aspx.cs" Inherits="GrandAbstract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    


    <div class="panel panel-default">
       <div class="panel-heading" >
       
    <h3 class="panel-title" >
        <i class="glyphicon glyphicon-envelope" ></i>

        Grand Abstract

    </h3>

</div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row" id="rowhead1" runat="server" visible="true">
                <div style="width:18%" class="col-md-2"><b>Particulars</b></div>
                <div style="width:15%" class="col-md-2"><b> Budget Amount</b></div>  
                             
                <div style="width:17%" class="col-md-1"><b>Status</b></div>
                <div style="width:17%" class="col-md-2"><b> Approve Amount</b></div>  
                <div style="width:20%" class="col-md-4">
                   <b> Remarks</b>
                </div>
                <div style="width:20%" class="col-md-4">
                 <b>
                     <asp:Button ID="btnview" runat="server" Text="Budget View Details" OnClick="btnview_Click" class="btn btn-default" />
                 </b>
                    
                </div>
            </div>
            <br />
              <div class="row" id="rowauto" runat="server" visible="false" >
                <div class="col-md-2">Automobiles
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="AutoddlStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtAutomobile"  CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                
                <div class="col-md-2">
                    <asp:DropDownList runat="server" ID="AutoddlStatus" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                      
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                       
                    </asp:DropDownList> 
                </div>
                   <div class="col-md-2">                       
                       <asp:TextBox runat="server" ID="txtBudgetAutomobiles" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtAutoRemarks" Text="0" CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>
                </div>
                  
            </div>
            <div class="row" id="rowplant" runat="server" visible="false">
                <div class="col-md-2">Plant & Machinery
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="PlantddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtPlantMachinery" Text="0"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                   
                <div class="col-md-2">

                   <asp:DropDownList runat="server" ID="PlantddStatus" CssClass="form-control">
                         <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                    
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                       
                    </asp:DropDownList> 

                </div>
                <div class="col-md-2">  
                    <asp:TextBox runat="server" ID="txtBudegtedPM" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" /> 
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtPlantRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
                
            </div>
            <div class="row" id="rowshutt" runat="server" visible="false">
                <div class="col-md-2">Shuttering Materials
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="StutterddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtShutteringMaterials"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                 
                <div class="col-md-2">
                   <asp:DropDownList runat="server" ID="StutterddStatus" CssClass="form-control">
                       <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                     
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                       
                    </asp:DropDownList> 

                </div>
                <div class="col-md-2">  
                   <asp:TextBox runat="server" ID="txtBudgetShutteringMaterial" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtstutterRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
                 
            </div>
            <div class="row" id="rowconsum" runat="server" visible="false">
                <div class="col-md-2">Consumable Items
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ConsuddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtConsumableItems"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                 
                <div class="col-md-2">
                   <asp:DropDownList runat="server" ID="ConsuddStatus" CssClass="form-control">
                     <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                       
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                       
                    </asp:DropDownList> 

                </div>
                 <div class="col-md-2">  
                   <asp:TextBox runat="server" ID="txtBudgetConsumableItems" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtConRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
                 
            </div>
            <div class="row" id="rowelec" runat="server" visible="false">
                <div class="col-md-2">Electrical Item
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ElecddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtElectricaItem"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                   
                <div class="col-md-2">
                 <asp:DropDownList runat="server" ID="ElecddStatus" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                       
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                      
                    </asp:DropDownList> 

                </div>
                 <div class="col-md-2">  
                  <asp:TextBox runat="server" ID="txtBudgetElectricalItems" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtElecRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
                 
            </div>
            <div class="row" id="rowhsd" runat="server" visible="false">
                <div class="col-md-2">HSD
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="HSDddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtHSDPetrol"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                   
                <div class="col-md-2">
                   <asp:DropDownList runat="server" ID="HSDddStatus" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                       
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                       
                    </asp:DropDownList> 

                </div>
                <div class="col-md-2">  
                <asp:TextBox runat="server" ID="txtBudgetHSD" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtHSDRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
              
            </div>
            <div class="row" id="rowoil" runat="server" visible="false">
                <div class="col-md-2">Petrol,Oil & Lubricants
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="OilddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtOilLubricants"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                  
                <div class="col-md-2">
                   <asp:DropDownList runat="server" ID="OilddStatus" CssClass="form-control">
                         <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                    
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                     
                    </asp:DropDownList> 

                </div>
                <div class="col-md-2">  
               <asp:TextBox runat="server" ID="txtBudgetPOL" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtOilRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
               
            </div>
            <div class="row" id="rowhard" runat="server" visible="false">
                <div class="col-md-2">Hardware Items
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="HardddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtHardwareItems"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                 
                <div class="col-md-2">
                   <asp:DropDownList runat="server" ID="HardddStatus" CssClass="form-control">
                       <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                  
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                       
                    </asp:DropDownList> 

                </div>
                <div class="col-md-2">  
              <asp:TextBox runat="server" ID="txtBudgetHardwareItems"  AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtHardRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
                
            </div>
            <div class="row" id="rowwelding" runat="server" visible="false">
                <div class="col-md-2">Welding Electrodes
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="WeldddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtWeldingElectrodes"  CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                  
                <div class="col-md-2">
                    <asp:DropDownList runat="server" ID="WeldddStatus" CssClass="form-control">
                           <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                     
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                      
                    </asp:DropDownList> 

                </div>
                   <div class="col-md-2">  
            <asp:TextBox runat="server" ID="txtBudgetWeldingElectrodes" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtWeldRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
                
            </div>
            <div class="row" id="rowoxygen" runat="server" visible="false">
                <div class="col-md-2">Oxygen & Acetylene Gas
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="OxyddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtOxygenAcetyleneGas"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                  
                <div class="col-md-2">
                     <asp:DropDownList runat="server" ID="OxyddStatus" CssClass="form-control">
                           <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                      
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                       
                    </asp:DropDownList> 

                </div>
                 <div class="col-md-2">  
          <asp:TextBox runat="server" ID="txtBudgetOxygenAcetylene" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" Text="0" />
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtOxyRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
               
            </div>
            <div class="row" id="rowsafety" runat="server" visible="false">
                <div class="col-md-2">Safety Items
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="SafyddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtSafetyItems"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                   
                <div class="col-md-2">
                     <asp:DropDownList runat="server" ID="SafyddStatus" CssClass="form-control">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                       
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                       
                    </asp:DropDownList> 

                </div>
                 <div class="col-md-2">  
         <asp:TextBox runat="server" ID="txtBudgetSafetyItems" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtSafRemarks"  AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
                
            </div>
            <div class="row" id="rowstaff" runat="server" visible="false">
                <div class="col-md-2">Staff Welfare
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="StaffddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtStaffWelfare"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                  
                 <div class="col-md-2">
                     <asp:DropDownList runat="server" ID="StaffddStatus" CssClass="form-control">
                              <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                       
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                      
                    </asp:DropDownList> 

                </div>
                <div class="col-md-2">  
      <asp:TextBox runat="server" ID="txtBudgetStaffWelfare" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtStaffRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
               
            </div>
            <div class="row" id="rowmessexp" runat="server" visible="false">
                <div class="col-md-2">Mess Expenditure
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="MessddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtMessExpenditure"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                   
               <div class="col-md-2">
                     <asp:DropDownList runat="server" ID="MessddStatus" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                      
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                      
                    </asp:DropDownList> 

                </div>
                <div class="col-md-2">  
     <asp:TextBox runat="server" ID="txtBudgetMessExpenditure" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtMessReamrks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
             
            </div>
            <div class="row" id="rowprin" runat="server" visible="false">
                <div class="col-md-2">Printing & Stationery
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="PrintddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtPrintingStationery"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                  
                <div class="col-md-2">
                     <asp:DropDownList runat="server" ID="PrintddStatus" CssClass="form-control">
                         <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                      
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                       
                    </asp:DropDownList> 

                </div>
                 <div class="col-md-2">  
     <asp:TextBox runat="server" ID="txtBudgetPrintingStationary" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtPrintRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
                 <div class="col-md-1">
                       

                </div>
            </div>
            <div class="row" id="rowrepairs" runat="server" visible="false">
                <div class="col-md-2">Repairs & Maintenance
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="RepairddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtRepairsMaintenance"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                   
                <div class="col-md-2">
                     <asp:DropDownList runat="server" ID="RepairddStatus" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                       
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                        
                    </asp:DropDownList> 

                </div>
                   <div class="col-md-2">  
     <asp:TextBox runat="server" ID="txtBudgetRepairMaintenance" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtRepairRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
               
            </div>
            <div class="row" id="rowBOQ" runat="server" visible="false">
                <div class="col-md-2">BOQ Items
                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="BOQddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtBOQItems"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                   
               <div class="col-md-2">
                     <asp:DropDownList runat="server" ID="BOQddStatus" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                    
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                    
                    </asp:DropDownList> 

                </div>
                <div class="col-md-2">  
     <asp:TextBox runat="server" ID="txtBudgetedBOQitems" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtBOQRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
                 
            </div>
            <div class="row" id="rowsanitary" runat="server" visible="false">
                <div class="col-md-2">Sanitary Materials
                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="SaniddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtSanitaryMaterials"   CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                  
               <div class="col-md-2">
                     <asp:DropDownList runat="server" ID="SaniddStatus" CssClass="form-control">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                     
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                      
                    </asp:DropDownList> 

                </div>
                <div class="col-md-2">  
   <asp:TextBox runat="server" ID="txtBudgetSnitaryMaterials" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtsaniRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
               
            </div>
            <div class="row" id="rowblasting" runat="server" visible="false">
                <div class="col-md-2">Blasting Materials
                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="BlastddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtBlastingMaterials"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                   
                <div class="col-md-2">
                     <asp:DropDownList runat="server" ID="BlastddStatus" CssClass="form-control">
                          <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                     
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                       
                    </asp:DropDownList> 

                </div>
                <div class="col-md-2">  
  <asp:TextBox runat="server" ID="txtBudgetBlastingMaterial" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtBlasRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
                
            </div>
            <div class="row" id="rowfurnitures" runat="server" visible="false">
                <div class="col-md-2">Furnitures & Fixtures
                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="FurniddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtFurnituresFixtures"  CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                 
                <div class="col-md-2">
                     <asp:DropDownList runat="server" ID="FurniddStatus" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                   
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                      
                    </asp:DropDownList> 

                </div>
                 <div class="col-md-2">  
  <asp:TextBox runat="server" ID="txtBudgetFurnituresFixtures" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtFurnRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
                
            </div>
            <div class="row" id="rowfixed" runat="server" visible="false">
                <div class="col-md-2">Fixed Assets
                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="FixedddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtFixedAssets"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                  
                <div class="col-md-2">
                     <asp:DropDownList runat="server" ID="FixedddStatus" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                      
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                     
                    </asp:DropDownList> 

                </div>
                 <div class="col-md-2">  
 <asp:TextBox runat="server" ID="txtBudgetAssets" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
               <div class="col-md-3">
                    <asp:TextBox ID="txtFixedRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
                 
            </div>
               <div class="row" id="rowinfra" runat="server" visible="false">
                <div class="col-md-2">Infrastructure Items
                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="InfrddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtInfrastructureItems"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                   
                <div class="col-md-2">
                     <asp:DropDownList runat="server" ID="InfrddStatus" CssClass="form-control">
                          <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                      
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                      
                    </asp:DropDownList> 

                </div>
                     <div class="col-md-2">  
 <asp:TextBox runat="server" ID="txtBudgetInfItems" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
               <div class="col-md-3">
                    <asp:TextBox ID="txtInfraRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
               
            </div>




            <div class="row" id="rowother1" runat="server" visible="false">
                <div class="col-md-2">Others
                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="otherddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtothers"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                   
                <div class="col-md-2">
                     <asp:DropDownList runat="server" ID="otherddStatus" CssClass="form-control">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                   
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                       
                    </asp:DropDownList> 

                </div>
                <div class="col-md-2">  
 <asp:TextBox runat="server" ID="txtBudgetOthers" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtOtherRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
                 
            </div>



            <br />
            <div class="row" id="rowhead2" runat="server" visible="false">
                <div class="col-md-2"><b>Construction Materials</b></div>
                <div class="col-md-2"><b> Budget Amount</b></div>
                 <div class="col-md-2"><b> Recoverable Amount</b></div>
                <div class="col-md-2"><b>Status</b></div>
                <div class="col-md-4">
                   <b> Remarks</b>

                </div>
            </div>
            <br />
            <div class="row" id="rowsand" runat="server" visible="false">
                <div class="col-md-2">Sand
                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="SandddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtsand"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                   
             <div class="col-md-2">
                     <asp:DropDownList runat="server" ID="SandddStatus" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                      
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                        
                    </asp:DropDownList> 

                </div>
                 <div class="col-md-2">  
 <asp:TextBox runat="server" ID="txtBudgetSand" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtSanRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
               
            </div>
            <div class="row" id="rowjelly" runat="server" visible="false">
                <div class="col-md-2">Jelly/Metal/Aggregates
                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="JellddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtJellyMetalAgr"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                   
               <div class="col-md-2">
                     <asp:DropDownList runat="server" ID="JellddStatus" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                      
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                      
                    </asp:DropDownList> 

                </div>
                  <div class="col-md-2">  
 <asp:TextBox runat="server" ID="txtBudgetJelly" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtJellyRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
                
            </div>
            <div class="row" id="rowred" runat="server" visible="false">
                <div class="col-md-2">Red Soil
                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="RedddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtredsoil"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                  
                <div class="col-md-2">
                     <asp:DropDownList runat="server" ID="RedddStatus" CssClass="form-control">
                           <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                       
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                      
                    </asp:DropDownList> 

                </div>
                <div class="col-md-2">  
 <asp:TextBox runat="server" ID="txtBudgetRedSoil" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
               <div class="col-md-3">
                    <asp:TextBox ID="txtRedRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
                
            </div>
            <div class="row" id="rowcement" runat="server" visible="false">
                <div class="col-md-2">Cement
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="CementddStatus" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtcement"  CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                  
              <div class="col-md-2">
                     <asp:DropDownList runat="server" ID="CementddStatus" CssClass="form-control">
                       <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                      
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                   
                    </asp:DropDownList> 

                </div>
                 <div class="col-md-2">  
 <asp:TextBox runat="server" ID="txtBudgetCement" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtCenmeRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
                
            </div>
            <div class="row" id="rowchemical" runat="server" visible="false">
                <div class="col-md-2">Chemicals
                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="ChemiddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtchemicalamt"  CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                  
                <div class="col-md-2">
                     <asp:DropDownList runat="server" ID="ChemiddStatus" CssClass="form-control">
                          <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                       
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                       
                    </asp:DropDownList>
                </div>
                  <div class="col-md-2">  
 <asp:TextBox runat="server" ID="txtBudgetChemical" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
              <div class="col-md-3">
                    <asp:TextBox ID="txtChemiRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
               
            </div>
            <div class="row" id="rowsteels" runat="server" visible="false">
                <div class="col-md-2">Steels
                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="SteelddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtSteels"  CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                  
                <div class="col-md-2">
                     <asp:DropDownList runat="server" ID="SteelddStatus" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                 
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                  
                    </asp:DropDownList> 

                </div>
                 <div class="col-md-2">  
 <asp:TextBox runat="server" ID="txtBudgetSteels" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
               <div class="col-md-3">
                    <asp:TextBox ID="txtSteelRemarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
               
            </div>
            <div class="row" id="rowbricks" runat="server" visible="false">
                <div class="col-md-2">Bricks
                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="BricksddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtBricks"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                  
                <div class="col-md-2">
                     <asp:DropDownList runat="server" ID="BricksddStatus" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                       
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                      
                    </asp:DropDownList> 

                </div>
                  <div class="col-md-2">  
 <asp:TextBox runat="server" ID="txtBudgetBricks" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
               <div class="col-md-3">
                    <asp:TextBox ID="txtBricksRmarks"  CssClass="form-control" TextMode="MultiLine" style="resize:none" runat="server"></asp:TextBox>

                </div>
                
            </div>
            <div class="row" id="rowother2" runat="server" visible="false">
                <div class="col-md-2">OtherConst
                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="OtherConddStatus" Display="Dynamic" ValidationGroup="ValDesc" InitialValue="0" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtOthersconst"  CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                  
                <div class="col-md-2">
                     <asp:DropDownList runat="server" ID="OtherConddStatus" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>                   
                        <asp:ListItem Text="Modify" Value="Send back for modification"></asp:ListItem>                      
                    </asp:DropDownList> 

                </div>
                 <div class="col-md-2">  
 <asp:TextBox runat="server" ID="txtBudgetOtherCM" AutoPostBack="true" OnTextChanged="txtBudgetHardwareItems_TextChanged" CssClass="form-control" Text="0" />
                    </div>
               <div class="col-md-3">
                    <asp:TextBox ID="txttherConRemarks"  CssClass="form-control" TextMode="MultiLine"  style="resize:none" runat="server"></asp:TextBox>

                </div>
                
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnDecision" runat="server" OnClick="btnDecision_Click" Text="Decision" Visible="false" CssClass="btn btn-default" ValidationGroup="ValDesc" ></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="false" CssClass="btn btn-default" OnClick="btnCancel_Click"></asp:Button>                   
                </div>
                
            </div>


             <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        </div>

    </div>
</asp:Content>