<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true"  CodeBehind="LocationWorkTaskAssignment.aspx.cs" Inherits="SNC.Project.LocationWorkTaskAssignment" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
        });
        function exportgrid() {
            Grid_RC_List.exportToExcel();
        }
        $('body').on('shown.bs.modal', '.modal', function () {
            $(this).find('select').each(function () {
                var dropdownParent = $(document.body);
                if ($(this).parents('.modal.in:first').length !== 0)
                    dropdownParent = $(this).parents('.modal.in:first');
                $(this).select2({
                    dropdownParent: dropdownParent
                    // ...
                });
            });
        });

        var callbackSuccess = function (data) {
            if (data) {
                populateDropDownById(data, 'objectiveBanks');
                $(".chzn-select").chosen();
                $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
                //Other things
            }
        };
        $('#modal').on('shown.bs.modal', function () {
            $('.chzn-select', this).chosen();
        });
        function beforedelete() {
            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
               return false;
            }
            else {
              
                return true;
            }
        }
        function ConfirmDelete() {
            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                return false;
            }
            else {

                return true;
            }
        }
    </script>
      <script type="text/javascript">
          $(document).ready(function () {
              $('.chosen-select').chosen();
          });
    </script>
    <style>
       .chosen-container{
  width: 100% !important;
}
    </style>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Work Location's 

            </h3>

        </div>
        <div class="panel-body">
                <div class="row">
                <div class="col-md-3">
                    Add or Select Location&nbsp;
                  <asp:RequiredFieldValidator ID="RequiredFieldValidatorDEPT" runat="server" CssClass="Validation_Text" ErrorMessage="*" InitialValue="-Select-" ControlToValidate="ddlLocation" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    <a href="#myModal1" data-toggle="modal" role="button">
                        <asp:ImageButton ID="imgBtnDept" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                    </a>


                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlLocation" class="chosen-select form-control" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true" TabIndex="3">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
                 <div class="col-md-3">
                   <a href="#myModalLocationVED" data-toggle="modal" role="button">
                        <asp:Button ID="btnLocation_VED"  Visible="false" runat="server" Text="View / Edit / Delete"    CssClass="btn btn-default" TabIndex="12"></asp:Button>
                    </a>
                </div>
            </div>
             <div class="row">
                <div class="col-md-3">
                    Add or Select Work Name&nbsp;
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="Validation_Text" ErrorMessage="*" InitialValue="-Select-" ControlToValidate="ddlWorkName" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    <a href="#myModal1" data-toggle="modal" role="button">
                        <asp:ImageButton ID="ImageWorkName" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                    </a>


                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlWorkName" class="chosen-select form-control" OnSelectedIndexChanged="ddlWorkName_SelectedIndexChanged" AutoPostBack="true" TabIndex="3">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-3">
                   <a href="#myModal112" data-toggle="modal" role="button">
                        <asp:Button ID="btnWorkName_VED" Visible="false" runat="server" Text="View / Edit / Delete"    CssClass="btn btn-default" TabIndex="12"></asp:Button>
                    </a>
                </div>
            </div>
             <div class="row">
                <div class="col-md-3">
                    Add or Select Sub Work Name&nbsp;
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="Validation_Text" ErrorMessage="*" InitialValue="-Select-" ControlToValidate="ddlSubWorkName" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    <a href="#ImageSubWorkName" data-toggle="modal" role="button">
                        <asp:ImageButton ID="ImageSubWorkName" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                    </a>


                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlSubWorkName" class="chosen-select form-control" OnSelectedIndexChanged="ddlSubWorkName_SelectedIndexChanged" AutoPostBack="true" TabIndex="3">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
                 <div class="col-md-3">
                   <a href="#myModalSubwork_VED" data-toggle="modal" role="button">
                        <asp:Button ID="btnSubWorkName_VED" Visible="false"  runat="server" Text="View / Edit / Delete"    CssClass="btn btn-default" TabIndex="12"></asp:Button>
                    </a>
                </div>
            </div>

            <div runat="server" id="Div_SubworkDetail" visible="false">
                 <div class="panel-body">
                     <div>
                             <table style="border: 1px solid; padding-left:100px; width: 100%";>
                            <tr>
                                <th colspan="3"style="font-size:large;text-align: center"> Summary Of - <label style="color:brown" runat="server" id="Vendorname"></label></th>
                            </tr>
                            <tr style="border: 1px solid;">
                                 <td style="border: 1px solid; text-align:center"> Work Name</td>
                                <td style="border: 1px solid; text-align:center">Sub Work Name</td>
                                <td style="border: 1px solid;text-align:center">Quantity</td>
                                <td style="border: 1px solid;text-align:center">UOM</td>
                            </tr>
                            <tr style="border: 1px solid;">
                                  <td style="border: 1px solid;text-align:center; font:200px">
                                    <label style="font-size:large; color:green" runat="server" id="lblworkname"></label>
                                </td>
                                <td style="border: 1px solid;text-align:center; font:200px">
                                    <label style="font-size:large; color:green" runat="server" id="lblSubworkname"></label>
                                </td>
                                <td style="border: 1px solid;text-align:center">
                                    <label style="font-size:large; color:darkgoldenrod" runat="server" id="lblSubworQty"></label>
                                </td>
                                <td style="border: 1px solid;text-align:center">
                                    <label style="font-size:large; color:green"  runat="server" id="lblSubworuom"></label>
                                </td>
                            </tr>
                        </table>
                      
                     </div>
                     <br />
                     <br />
            <div>
                   <center>
                   
                     
                <a href="#Modal_RequiredMaterial" data-toggle="modal" role="button">
                        <asp:Button ID="btnRequiredMaterial" runat="server" Text="Add Required Material" CssClass="btn btn-default" TabIndex="12"></asp:Button>
                     </a>
                      <asp:Button ID="btnAddSOW" runat="server" Visible="true" Text="Add Required Machinery" CssClass="btn btn-default" CausesValidation="false" OnClick="btnAddSOW_Click" TabIndex="15"></asp:Button>
                           <a href="#Modal_RequiredLabours" data-toggle="modal" role="button">
                        <asp:Button ID="btnRequiredMachineryNoOfLabours" runat="server" Text="Add Required Labours" CssClass="btn btn-default" TabIndex="12"></asp:Button>
                    </a>
                     <asp:Button ID="btnSaveAll" runat="server" Text="Save The Record" OnClick="btnSaveAll_Click"   CssClass="btn btn-default" TabIndex="12"></asp:Button>
                       <asp:Button ID="btnAssignSC" runat="server" Visible="true" Text="Assign Work To Sub Contractor" CssClass="btn btn-default" CausesValidation="false" OnClick="btnAssignSC_Click" TabIndex="15"></asp:Button>
                </center>
            </div>
            <br />
            <br />

          <div>
              <h4 style="text-align:center">Required Material List</h4>
                <center>
                <ogrid:Grid runat="server" ID="Grid_RequiredMaterial" CallbackMode="false" AutoGenerateColumns="false"   
                    FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="10" >
                    <ScrollingSettings ScrollWidth="100%"  ScrollHeight="400"/>
                      <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"
                         ColumnsToExport="WONo,WODate,Subcon_name,Type_Name,DurationOfWork,Status,Name,Prepared_By,Other_Terms,Remarks" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

                    <Columns>
                        <ogrid:Column DataField="ID" HeaderText="ID" Width="190px" >     </ogrid:Column>
                        <ogrid:Column DataField="Material_Name" HeaderText="Name Of Machinery" Width="250px" ></ogrid:Column>
                         <ogrid:Column DataField="Quantity" HeaderText="Quantity" Width="200px" ></ogrid:Column>
                         <ogrid:Column DataField="UOM" HeaderText="UOM" Width="200px" ></ogrid:Column>
                    </Columns>
                    <Templates>
                      
                    </Templates>
                </ogrid:Grid>
                <br />
             
                      </center>
          </div>
                       <div>
              <h4 style="text-align:center">Required Machinery List</h4>
                <center>
                 <ogrid:Grid runat="server" ID="GridRequiredMachinery" CallbackMode="false" AutoGenerateColumns="false"  
                    FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="10" >
                    <ScrollingSettings ScrollWidth="100%"  ScrollHeight="400"/>
                      <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"
                         ColumnsToExport="WONo,WODate,Subcon_name,Type_Name,DurationOfWork,Status,Name,Prepared_By,Other_Terms,Remarks" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

                    <Columns>
                        <ogrid:Column DataField="ID" HeaderText="ID" Width="190px" >
                        </ogrid:Column>
                         <ogrid:Column DataField="Name" HeaderText="Machinery Name" Width="100px" ></ogrid:Column>
                        <ogrid:Column DataField="Asset_Type" HeaderText="Asset Type" Width="100px" ></ogrid:Column>
                         <ogrid:Column DataField="Asset_Category" HeaderText="Asset Category" Width="200px" ></ogrid:Column>
                         <ogrid:Column DataField="Asset" HeaderText="Asset" Width="250px" ></ogrid:Column> 
                         <ogrid:Column DataField="Asset_RegNo" HeaderText="Asset Reg No" Width="100px" ></ogrid:Column> 
                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" Width="150px" ></ogrid:Column> 
                    </Columns>
                    <Templates>
                        <ogrid:GridTemplate ID="GridTemplate1" runat="server">
                            <Template>
                               <asp:HyperLink ID="lnk_ID" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["ID"] %>'>
                        </asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
                </ogrid:Grid>
                <br />
             
                      </center>
          </div>
                      <div>
              <h4 style="text-align:center">Required Labours List</h4>
                <center>
                 <ogrid:Grid runat="server" ID="GridRequiredLabours" CallbackMode="false" AutoGenerateColumns="false"  
                    FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="10" >
                    <ScrollingSettings ScrollWidth="100%"  ScrollHeight="400"/>
                      <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"
                         ColumnsToExport="WONo,WODate,Subcon_name,Type_Name,DurationOfWork,Status,Name,Prepared_By,Other_Terms,Remarks" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

                    <Columns>
                        <ogrid:Column DataField="ID" HeaderText="ID" Width="190px" >
                        </ogrid:Column>
                        <ogrid:Column DataField="Type_Of_Labour" HeaderText="Type Of Labour" Width="300px" ></ogrid:Column>
                         <ogrid:Column DataField="Quantity" HeaderText="Quantity" Width="200px" ></ogrid:Column>
                         <ogrid:Column DataField="UOM" HeaderText="UOM" Width="300px" ></ogrid:Column>
                    </Columns>
                    <Templates>
                     
                    </Templates>
                </ogrid:Grid>
                <br />
             
                      </center>
          </div>
        </div>
    </div>
            </div>
        </div>
       <ajaxToolkit:ModalPopupExtender ID="mpeDept" runat="server" PopupControlID="PanelDept" TargetControlID="imgBtnDept"
            CancelControlID="btnClose" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
     <asp:Panel ID="PanelDept" runat="server" align="center">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" id="btnClose" data-dismiss="modal" aria-hidden="true">×</button>
                        <center><h5 id="myModalLabel1">Add Work location Details</h5></center>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                        <div class="col-md-3">
                            Work Location&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ControlToValidate="txtWorkLocation"  CssClass="Validation_Text" ValidationGroup="Valworklocation" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtWorkLocation"  CssClass="form-control" MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>
                    </div>
                    <div class="modal-footer">
                        <center>
           <asp:Button ID="Button1" runat="server" Text="Save" OnClick="btnWorklocation_Click"  CssClass="btn btn-default" ValidationGroup="Valworklocation"  />
                        <asp:Button ID="Button7" runat="server" Text="Cancel" OnClick="btnCancelWorklocation_Click"  CssClass="btn btn-default"  CausesValidation="false"  />
                 </center>

                    </div>
                </div>
            </div>


        </asp:Panel>
     <ajaxToolkit:ModalPopupExtender ID="ModalSOWItem" runat="server" PopupControlID="PanelSOWItem" TargetControlID="btnAddSOW"
        CancelControlID="BtnClose2" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    
       <asp:Panel ID="PanelSOWItem" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="BtnClose2" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center><h5 id="myModalamt1SOW"><asp:Label ID="Label2" runat="server" Text="Add Required Machinery"></asp:Label></h5></center>
                </div>
                <div class="modal-body">   
                      <div class="row">
                       <div class="col-md-4" style="text-align:right">
                            Asset Type
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValRequired_Machinery" ControlToValidate="ddlAsset_Type"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlAsset_Type" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlAssetType_SelectedIndexChanged" AutoPostBack="true"  TabIndex="8">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                     <div class="row">
                        
                        <div class="col-md-4" style="text-align:right">
                             Asset Category Name&nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValRequired_Machinery" ControlToValidate="ddlAssetCategory"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlAssetCategory" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlAssetCategory_SelectedIndexChanged" AutoPostBack="true"   TabIndex="8">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        
                        <div class="col-md-4" style="text-align:right">
                             Required Machinery Name&nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="*"  InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValRequired_Machinery" ControlToValidate="ddlMachineryName"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlMachineryName" CssClass="form-control" OnSelectedIndexChanged="ddlMachineryName_SelectedIndexChanged"  AutoPostBack="true"   runat="server" TabIndex="8">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                     <div class="row">
                        
                        <div class="col-md-4" style="text-align:right">
                             Machinery Reg No&nbsp;
                            
                        </div>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlMachineryRegNo" CssClass="form-control" runat="server" TabIndex="8">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                      <div class="row">
                        <div class="col-md-4" style="text-align:right">
                           Quantity&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtMachineryQuantity"  CssClass="Validation_Text" ValidationGroup="ValRequired_Machinery" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtMachineryQuantity"  CssClass="form-control" MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>
                </div>
                    <div class="modal-footer">
                    <center>
                        <asp:Button ID="Button5" runat="server" Text="Save" OnClick="btnSaveRequired_Machinery_Click"  CssClass="btn btn-default" ValidationGroup="ValRequired_Machinery"  />
                       <%-- <asp:Button ID="Button5" runat="server" Text="Cancel" OnClick="btnCancelRequired_Machinery_Click"  CssClass="btn btn-default"  CausesValidation="false"  />--%>
                    </center>
                </div>
            </div>
        </div>

    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupAssignSC" runat="server" PopupControlID="PanelAssignSCtem" TargetControlID="btnAssignSC"
        CancelControlID="AssignSC_Close" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    
       <asp:Panel ID="PanelAssignSCtem" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="AssignSC_Close" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center><h5 id="myModalamt1AssignSC"><asp:Label ID="Label1" runat="server" Text="Add Assign Work To Sub Contractor"></asp:Label></h5></center>
                </div>
                <div class="modal-body">   
                      <div class="row">
                      <div  class="col-md-4" style="text-align:right">Sub Contractor Name
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17"  runat="server" InitialValue="-Select-" ControlToValidate="ddlSubContractor" CssClass="Validation_Text" ValidationGroup="ValRequired_SC" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </div>
                    <div  class="col-md-6" style="text-align:right" >
                        <asp:DropDownList runat="server" ID="ddlSubContractor"  class="chosen-select form-control" TabIndex="4"></asp:DropDownList>
                    </div>
                    </div>
                     <div class="row">
                        
                        <div class="col-md-4" style="text-align:right">
                             Sub Work List&nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="*" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValRequired_SC" ControlToValidate="ddlSubworkAssingment"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlSubworkAssingment" CssClass="form-control" runat="server"   TabIndex="8">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                   
                </div>
                    <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnbtnSaveContractor" runat="server" Text="Save" OnClick="btnSaveContractor_Click"  CssClass="btn btn-default" ValidationGroup="ValRequired_SC"  />
                       <%-- <asp:Button ID="Button5" runat="server" Text="Cancel" OnClick="btnCancelRequired_Machinery_Click"  CssClass="btn btn-default"  CausesValidation="false"  />--%>
                    </center>
                </div>
            </div>
        </div>

    </asp:Panel>
         <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="PanelWorkName" TargetControlID="ImageWorkName"
            CancelControlID="btnCloseWorkname" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
     <asp:Panel ID="PanelWorkName" runat="server" align="center">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" id="btnCloseWorkname" data-dismiss="modal" aria-hidden="true">×</button>
                        <center><h5 id="myModalworkName">Add Work Details</h5></center>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                        <div class="col-md-3">
                            Work Location&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtWorkLocation"  CssClass="Validation_Text" ValidationGroup="Valworklocation" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtWorkName"  CssClass="form-control" MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>
                    </div>
                    <div class="modal-footer">
                        <center>
           <asp:Button ID="btnSaveWorkName" runat="server" Text="Save" OnClick="btnWorkName_Click"  CssClass="btn btn-default" ValidationGroup="ValworkName"  />
                        <asp:Button ID="btnCancelWorkName" runat="server" Text="Cancel" OnClick="btnCancelWorkName_Click"  CssClass="btn btn-default"  CausesValidation="false"  />
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
                        <center><h5 id="myModalSubworkName">Add Sub Work Details</h5></center>
                    </div>
                    <div class="modal-body">
                       <div class="row">
                        <div class="col-md-4">
                           Sub Work Name&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtSubWorkName"  CssClass="Validation_Text" ValidationGroup="ValSubWorkName" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtSubWorkName"  CssClass="form-control" MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>
                      <div class="row">
                        <div class="col-md-4">
                           Quantity&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtSubWorkQuantity"  CssClass="Validation_Text" ValidationGroup="ValSubWorkName" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtSubWorkQuantity"   CssClass="form-control input-pos-float" MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>
                        <div class="row">

                        
                     <div class="col-md-4">
                            UOM
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValSubWorkName" ControlToValidate="ddlSubWorkUOM"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlSubWorkUOM" CssClass="form-control" runat="server"  TabIndex="8">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                            </div>
                           <div class="modal-footer">
                        <center>
         <asp:Button ID="Button2" runat="server" Text="Save" OnClick="btnSubWorkName_Click"  CssClass="btn btn-default" ValidationGroup="ValSubWorkName"  />
                        <asp:Button ID="Button3" runat="server" Text="Cancel" OnClick="btnCancelSubWorkName_Click"  CssClass="btn btn-default"  CausesValidation="false"  />
                 </center>

                    </div>
                    </div>
   
                    </div>
                 
                </div>
         


        </asp:Panel>
    <div class="modal small fade" id="Modal_RequiredMaterial" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 650px">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>  <h5 id="Modal_RequiredMaterialLabel1">Add Required Material Details</h5></center>
                </div>
                <div class="modal-body">
                  
                    <div class="row">
                        <div class="col-md-4" style="text-align:right">
                           Required Material Name&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*"  InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValRequired_Machinery" ControlToValidate="ddlRequired_MaterialName"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6" style="text-align:right">
                            <asp:DropDownList ID="ddlRequired_MaterialName"  class="chosen-select form-control" runat="server"  TabIndex="8">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                       </div>
                    </div>
                      <div class="row">
                        <div class="col-md-4" style="text-align:right">
                           Quantity&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtRequired_MaterialQuantity"  CssClass="Validation_Text" ValidationGroup="ValRequired_Material" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtRequired_MaterialQuantity"   CssClass="form-control input-pos-float"  MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>
                      <div class="row">
                       <div class="col-md-4" style="text-align:right">
                            UOM
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValRequired_Material" ControlToValidate="ddlUOM"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlUOM" CssClass="form-control" runat="server" TabIndex="8">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnSaveRequired_Material" runat="server" Text="Save" OnClick="btnSaveRequired_Material_Click"  CssClass="btn btn-default" ValidationGroup="ValRequired_Material"  />
                        <asp:Button ID="btnCancelRequired_Material" runat="server" Text="Cancel" OnClick="btnCancelRequired_Material_Click"  CssClass="btn btn-default"  CausesValidation="false"  />
                    </center>
                </div>
            </div>
        </div>
    </div>
     <div class="modal small fade" id="myModal112" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 650px">
            <div class="modal-content">
                <div class="modal-header">
                  
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                     <center>  <h5 id="myModalLabel112">Work Name List</h5></center>
                    </div>
                <div class="modal-body"  id="mybtnalig">
                   <center>
                <ogrid:Grid runat="server" ID="Grid_Work_Name" CallbackMode="false" AutoGenerateColumns="false"   
                    FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="10" >
                    <ScrollingSettings ScrollWidth="100%"  ScrollHeight="400"/>
                      <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"
                         ColumnsToExport="WONo,WODate,Subcon_name,Type_Name,DurationOfWork,Status,Name,Prepared_By,Other_Terms,Remarks" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

                    <Columns>
                        <ogrid:Column DataField="ID" HeaderText="ID" Width="190px" >
                             <TemplateSettings  TemplateId="WONoTemplate"/>
                        </ogrid:Column>
                        <ogrid:Column DataField="ID" HeaderText="ID" Width="100px" ></ogrid:Column>
                        <ogrid:Column DataField="Work_Name" HeaderText="Works / Task" Width="100px" ></ogrid:Column>
                    </Columns>
                    <Templates>
                        <ogrid:GridTemplate ID="GridTemplate4" runat="server">
                            <Template>
                               <asp:HyperLink ID="lnk_ID" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["ID"] %>'>
                        </asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
                </ogrid:Grid>
                <br />
                <center>
                </div>
        </div>
    </div>
    </div>
     <div class="modal small fade" id="myModalLocationVED" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 650px">
            <div class="modal-content">
                <div class="modal-header">
                  
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                     <center>  <h5 id="myModalLocationVED_Lable">Location List</h5></center>
                    </div>
                <div class="modal-body"  id="mybtnaligLocation">
                   <center>
                <ogrid:Grid runat="server" ID="Grid_Location_List" CallbackMode="false" AutoGenerateColumns="false"   
                    FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="10" >
                    <ScrollingSettings ScrollWidth="100%"  ScrollHeight="400"/>
                      <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"
                         ColumnsToExport="WONo,WODate,Subcon_name,Type_Name,DurationOfWork,Status,Name,Prepared_By,Other_Terms,Remarks" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

                    <Columns>
                        <ogrid:Column DataField="ID" HeaderText="ID" Width="190px" >
                             <TemplateSettings  TemplateId="WONoTemplate"/>
                        </ogrid:Column>
                        <ogrid:Column DataField="Work_Location" HeaderText="Sub Works" Width="100px" ></ogrid:Column>
                         <ogrid:Column DataField="Project_Code" HeaderText="Quantity" Width="100px" ></ogrid:Column>
                        
                    </Columns>
                    <Templates>
                        <ogrid:GridTemplate ID="GridTemplate3" runat="server">
                            <Template>
                               <asp:HyperLink ID="lnk_ID" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["ID"] %>'>
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
     <div class="modal small fade" id="myModalSubwork_VED" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 650px">
            <div class="modal-content">
                <div class="modal-header">
                  
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                     <center>  <h5 id="myModalSubworkVED_Lable">Sub Work List</h5></center>
                    </div>
                <div class="modal-body"  id="mybtnaligModalSubwork">
                  <center>
                <ogrid:Grid runat="server" ID="Grid_SubWork_Name_VED" CallbackMode="false" AutoGenerateColumns="false"   
                    FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="10" >
                    <ScrollingSettings ScrollWidth="100%"  ScrollHeight="400"/>
                      <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"
                         ColumnsToExport="WONo,WODate,Subcon_name,Type_Name,DurationOfWork,Status,Name,Prepared_By,Other_Terms,Remarks" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

                    <Columns>
                        <ogrid:Column DataField="ID" HeaderText="ID" Width="190px" >
                             <TemplateSettings  TemplateId="WONoTemplate"/>
                        </ogrid:Column>
                        <ogrid:Column DataField="SubWork_Name" HeaderText="Sub Works" Width="100px" ></ogrid:Column>
                         <ogrid:Column DataField="Quantity" HeaderText="Quantity" Width="100px" ></ogrid:Column>
                         <ogrid:Column DataField="UOM" HeaderText="UOM" Width="100px" ></ogrid:Column>
                    </Columns>
                    <Templates>
                        <ogrid:GridTemplate ID="GridTemplate5" runat="server">
                            <Template>
                               <asp:HyperLink ID="lnk_ID" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["ID"] %>'>
                        </asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
                </ogrid:Grid>
                <br />
                <center>
                </div>
        </div>
    </div>
    </div>
    
    <div class="modal small fade" id="Modal_RequiredLabours" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 650px">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>  <h5 id="Modal_RequiredLaboursLabel1">Add Required Labours Details</h5></center>
                </div>
                <div class="modal-body">
                  
                     <div class="row">
                      <div class="col-md-4" style="text-align:right"> 
                   Labour Type&nbsp;
                 <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="Validation_Text" ErrorMessage="*" InitialValue="-Select-" ControlToValidate="ddlSubWorkName" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                    <a href="#ImageLabourType" data-toggle="modal" role="button">
                        <asp:ImageButton ID="ImageLabourType" runat="server"  ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                    </a>
                        
                    </div>
                                 <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="ddlLabour" class="form-control" TabIndex="4"></asp:DropDownList>
                </div>
                          </div>
                      <div class="row">
                        <div class="col-md-4" style="text-align:right">
                           Quantity&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="txtLaboursQuantity"  CssClass="Validation_Text" ValidationGroup="ValRequired_Labours" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6" style="text-align:right">
                            <asp:TextBox ID="txtLaboursQuantity"   CssClass="form-control input-pos-float"  MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>
                      <div class="row">
                         <div class="col-md-4" style="text-align:right">
                            UOM
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValRequired_Labours" ControlToValidate="ddlLaboursUOM"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6" style="text-align:right">
                            <asp:DropDownList ID="ddlLaboursUOM"  CssClass="form-control" runat="server"  TabIndex="8">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="Button6" runat="server" Text="Save" OnClick="btnSaveRequired_Labours_Click"  CssClass="btn btn-default" ValidationGroup="ValRequired_Labours"  />
                        <asp:Button ID="Button8" runat="server" Text="Cancel" OnClick="btnCancelRequired_Labours_Click"  CssClass="btn btn-default"  CausesValidation="false"  />
                    </center>
                </div>
            </div>
        </div>
    </div>


     <ajaxToolkit:ModalPopupExtender ID="ModalPopupImageLabourType" runat="server" PopupControlID="PanelLabourType" TargetControlID="ImageLabourType"
            CancelControlID="btnCloseLabourType" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
     <asp:Panel ID="PanelLabourType" runat="server" align="center">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" id="btnCloseLabourType" data-dismiss="modal" aria-hidden="true">×</button>
                        <center><h5 id="myModalLabourType">Add Labour Type</h5></center>
                    </div>
                    <div class="modal-body">
                       <div class="row">
                        <div class="col-md-4">
                           Labour Type&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtLabourType"  CssClass="Validation_Text" ValidationGroup="ValLabourType" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtLabourType"  CssClass="form-control" MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>
                     
                           <div class="modal-footer">
                        <center>
         <asp:Button ID="Button4" runat="server" Text="Save" OnClick="btnLabourType_Click"  CssClass="btn btn-default" ValidationGroup="ValLabourType"  />
                        <asp:Button ID="Button9" runat="server" Text="Cancel" OnClick="btnCancelLabourType_Click"  CssClass="btn btn-default"  CausesValidation="false"  />
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


