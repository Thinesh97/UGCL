<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="NominalMasterRoll.aspx.cs"   Inherits="SNC.SubContractorBills.NominalMasterRoll"%>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        $(document).ready(function () {
            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
        });
        function ConfirmDelete() {
            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                return false;
            }
            return true;
        }
        function exportgrid() {
            Grid_NMR.exportToExcel();
        }
        function CalculateTotalCostOfLabour() {

            let No_Men = document.getElementById("<%=txtRequiredLabour.ClientID%>").value;
            let MenRate = document.getElementById("<%=txtLabourRate.ClientID%>").value;
            var result = (No_Men * MenRate)
            document.getElementById("<%=txtTotalCostOfLabour .ClientID%>").value = (result);

        }
        
    </script>
     <script type="text/javascript">
         $(document).ready(function () {
             $('.chosen-select').chosen();
         });
     </script>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
   
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>
               Nominal Master Roll
            </h3>
        </div>
        <div class="panel-body">
            <!------------------------------------------------------------Body Content-------------------------------------------------------------------->
          <div class="row" runat="server">
              <div class="col-md-2">
                   NMR ID
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtNMRID" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                </div>  
              <div class="col-md-2">
                  Date&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNMRBillDate" CssClass="Validation_Text" ValidationGroup="ValNMR" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtNMRBillDate" CssClass="form-control" onkeypress="javascript:return false;" onpaste="javascript:return false;" runat="server" TabIndex="2" AutoComplete="Off"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd-MM-yyyy" TargetControlID="txtNMRBillDate"></ajaxToolkit:CalendarExtender>
                </div>
           </div>

            <div class="row">
                <div class="row">
                 <div class="col-md-2">
                    Project Name&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" InitialValue="-Select-" ControlToValidate="ddlProject_NMR" CssClass="Validation_Text" ValidationGroup="ValWO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlProject_NMR" class="chosen-select form-control" TabIndex="4"></asp:DropDownList>
                </div>
                 <div class="col-md-2">
                    Sub Contractor&nbsp;
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlSubContractor_NMR" AutoPostBack="true"  OnSelectedIndexChanged="ddlSubContractor_SelectedIndexChanged"  class="chosen-select form-control" TabIndex="4"></asp:DropDownList>
                </div>

              
            </div>
            <div class="row">
                 <div class="col-md-2">
                    Work Order&nbsp;
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlWO_NMR" OnSelectedIndexChanged="ddlWO_SelectedIndexChanged"  AutoPostBack="true" class="chosen-select form-control" TabIndex="4"></asp:DropDownList>
                </div>
                
            </div>
             <div class="row">
                 
                <div class="col-md-2">
                  Work Description
                </div>
                <div class="col-md-6">
                    <asp:TextBox ID="txtWorkDescription_NMR" CssClass="form-control" TextMode="MultiLine"  runat="server"></asp:TextBox>
                   
                </div>
            </div>
           
             <div class="row">
                <div class="col-md-2">
                  Work Done At&nbsp;
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"  ControlToValidate="txtWorkDoneAt" CssClass="Validation_Text" ValidationGroup="ValNMR" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-6">
                    <asp:TextBox ID="txtWorkDoneAt" CssClass="form-control" TextMode="MultiLine"  runat="server"></asp:TextBox>
                   
                </div>
            </div>
            <br />
            <br />
         <div class="col-md-12 text-center">
                 <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ValNMR" OnClick="btnSubmit_Click"   CssClass="btn btn-default"  TabIndex="9"></asp:Button>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" TabIndex="10"></asp:Button>
              <asp:Button ID="btnAddSOW" runat="server" Visible="true" Text="Add Labour" CssClass="btn btn-default" CausesValidation="false" OnClick="btnAddSOW_Click" TabIndex="15"></asp:Button>
            <br />
            <br />
           </div>
            </div>
                <br />
               

          <div class="panel panel-default">
        <div >
            <br />
            <h3 class="panel-title" style="text-align:center">    
               Date Wise Labour List  for - <label id="lblGridLable" runat="server"></label>

            </h3>

        </div>
     
                  <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="Grid_LabourList"   CallbackMode="false" AutoGenerateColumns="false"   FolderStyle="../Gridstyles/grand_gray" OnRowDataBound="Grid_LabourList_RowDataBound" OnDeleteCommand="Grid_LabourList_Delete_Click" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true"   PageSize="10">
           
              <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
            <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />

            <Columns>
                   <ogrid:Column DataField="ID" HeaderText="ID" Width="100px"  runat="server" >
            <TemplateSettings  TemplateId="NMR_NoTemplate"/>
        </ogrid:Column>      
              
              
                <ogrid:Column DataField="Labour_Date" HeaderText="Date" Width="100px" ReadOnly="true" ></ogrid:Column>
                  <ogrid:Column DataField="Labour_Type" HeaderText="Labour Type" Width="210px" ReadOnly="true" ></ogrid:Column>
                 <ogrid:Column DataField="NoOf_Labour" HeaderText="No Of Labour" ReadOnly="true" Width="110px" ></ogrid:Column>
                <ogrid:Column DataField="Labour_Rate" HeaderText="Rate" ReadOnly="true"  Width="110px" ></ogrid:Column>
                 <ogrid:Column DataField="LabourCost_Total" HeaderText="Cost"  Width="110px" Wrap="true" ></ogrid:Column>
                <ogrid:Column DataField="Project_Code" HeaderText="Project Code"   Width="120px" ReadOnly="true" ></ogrid:Column>
                <ogrid:Column DataField="WO_NMR" HeaderText="Work Oder Number"  Width="120px" ReadOnly="true" ></ogrid:Column>
                  <ogrid:Column  HeaderText="Delete" AllowDelete="true" Width="100" ></ogrid:Column>
            </Columns>
            
               <Templates>
                        <ogrid:GridTemplate ID="NMR_NoTemplate" runat="server">
                            <Template>
                               <asp:HyperLink ID="lnk_NMR_No" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["ID"] %>'>
                        </asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
    </Templates>
        </ogrid:Grid>
                <br />
                 
                  </center>      
                 <br />
               
                    
        </div>
                 </div>
            </div>
       </div>
        <ajaxToolkit:ModalPopupExtender ID="ModalSOWItem" runat="server" PopupControlID="PanelSOWItem" TargetControlID="btnAddSOW"
        CancelControlID="BtnClose2" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    
       <asp:Panel ID="PanelSOWItem" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="BtnClose2" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center><h5 id="myModalamt1SOW"><asp:Label ID="Label2" runat="server" Text="Add Required Labours"></asp:Label></h5></center>
                </div>
                <div class="modal-body">   
                      <div class="row">
                      <div class="col-md-4">
                   Labour Type&nbsp;
                 <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="Validation_Text" ErrorMessage="*" InitialValue="-Select-" ControlToValidate="ddlSubWorkName" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    <a href="#ImageSubWorkName" data-toggle="modal" role="button">
                        <asp:ImageButton ID="ImageSubWorkName" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                    </a>
                        
                    </div>
                                 <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="ddlLabour" class="form-control" TabIndex="4"></asp:DropDownList>
                </div>
                          </div>
                           <div class="row"> 
                     <div class="col-md-4">
                   Date &nbsp;
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  ControlToValidate="txtrqRequiredDate" CssClass="Validation_Text" ValidationGroup="ValLaboursAdd" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                 <div class="col-md-6">
                    <asp:TextBox ID="txtrqRequiredDate" CssClass="form-control" onkeypress="javascript:return false;" onpaste="javascript:return false;" runat="server" TabIndex="2" AutoComplete="Off"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" Format="dd-MM-yyyy" TargetControlID="txtrqRequiredDate"></ajaxToolkit:CalendarExtender>
                </div>
                         </div>
                    <div class="row"> 
                     <div class="col-md-4">
                    No of Labour's&nbsp;
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"  ControlToValidate="txtRequiredLabour" CssClass="Validation_Text" ValidationGroup="ValLaboursAdd" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-6">
                   <asp:TextBox ID="txtRequiredLabour"   onkeyup="CalculateTotalCostOfLabour()" CssClass="form-control input-pos-float" runat="server"></asp:TextBox>
                </div>
                         </div>
              <div class="row"> 
                   <div class="col-md-4">
                    Rate&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"  ControlToValidate="txtLabourRate" CssClass="Validation_Text" ValidationGroup="ValLaboursAdd" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-6">
                   <asp:TextBox ID="txtLabourRate"  onkeyup="CalculateTotalCostOfLabour()" CssClass="form-control input-pos-float" runat="server"></asp:TextBox>
                </div>
              </div>
               <div class="row">
                    <div class="col-md-4">
               Total  &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"  ControlToValidate="txtTotalCostOfLabour" CssClass="Validation_Text" ValidationGroup="ValLaboursAdd" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-6">
                    <asp:TextBox ID="txtTotalCostOfLabour" CssClass="form-control input-pos-float"  runat="server"></asp:TextBox>
                </div>
               </div>
                </div>

                   
                    <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnbtnSaveRequired" runat="server" Text="Save" OnClick="btnSaveRequired_Labour_Click"  CssClass="btn btn-default" ValidationGroup="ValLaboursAdd"  />
                      <asp:Button ID="btnbtnCancelRequired_Labour" runat="server" Text="Cancel" OnClick="btnCancelRequired_Labour_Click"  CssClass="btn btn-default"  CausesValidation="false"  />
                    </center>
                </div>

                 
            </div>
        </div>
            </asp:Panel>
     <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="PanelSubWorkName" TargetControlID="ImageSubWorkName"
            CancelControlID="btnCloseSubWorkname" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
     <asp:Panel ID="PanelSubWorkName" runat="server" align="center">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" id="btnCloseSubWorkname" data-dismiss="modal" aria-hidden="true">×</button>
                        <center><h5 id="myModalSubworkName">Add Labour Type</h5></center>
                    </div>
                    <div class="modal-body">
                       <div class="row">
                        <div class="col-md-4">
                           Labour Type&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtLabourType"  CssClass="Validation_Text" ValidationGroup="ValLabourType" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtLabourType"  CssClass="form-control" MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>
                     
                           <div class="modal-footer">
                        <center>
         <asp:Button ID="Button2" runat="server" Text="Save" OnClick="btnLabourType_Click"  CssClass="btn btn-default" ValidationGroup="ValLabourType"  />
                        <asp:Button ID="Button3" runat="server" Text="Cancel" OnClick="btnCancelLabourType_Click"  CssClass="btn btn-default"  CausesValidation="false"  />
                 </center>
                               <div>
                                     <center>
        <ogrid:Grid runat="server" ID="Grid_Labour_Type"   CallbackMode="false" AutoGenerateColumns="false"   FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true"   PageSize="10">
           
              <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
            <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />

            <Columns>
                   <ogrid:Column DataField="ID" HeaderText="NMR No" Visible="false" runat="server" >
            <TemplateSettings  TemplateId="NMR_Labour_Type"/>
        </ogrid:Column>      
              
                <ogrid:Column DataField="Labour_Type" HeaderText="Labour Type" Width="300px" ReadOnly="true" ></ogrid:Column>
                  <ogrid:Column  HeaderText="Delete" AllowDelete="true" Width="100" ></ogrid:Column>
            </Columns>
            
               <Templates>
                        <ogrid:GridTemplate ID="NMR_Labour_Type" runat="server">
                            <Template>
                               <asp:HyperLink ID="lnk_NMR_No" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["ID"] %>'>
                        </asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
    </Templates>
        </ogrid:Grid>
                <br />
                   
                  </center>      
                               </div>
                    </div>
                    </div>
   
                    </div>
                 
                </div>
         


        </asp:Panel>
        </asp:Content>