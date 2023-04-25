<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="ProjectBudgetCreate.aspx.cs" Inherits="ProjectBudget" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {

            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
        });

        function exportgrid() {
            Gv_ProjBudgetSector.exportToExcel();
        }
        function beforedelete() {
            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                return false;
            }
            return true;
        }

    </script>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Project Budget Details

            </h3>

        </div>

        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->
            <div class="row">
                <div class="col-md-2">
                    Date&nbsp;                  
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtCreatedDate" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtCreatedDate" Format="dd-MM-yyyy" runat="server"></ajaxToolkit:CalendarExtender>
                </div>
                <div class="col-md-2">
                    Project&nbsp;                   
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlProjectName" CssClass="form-control">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">
                    Created By&nbsp;               
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlCreatedBy" CssClass="form-control">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Status&nbsp;&nbsp;              
                </div>
                <div class="col-md-4">
                    <asp:RadioButtonList runat="server" ID="rd_Status" RepeatDirection="Horizontal" TabIndex="8">
                        <asp:ListItem Text="Open" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Hold" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Close" Value="3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <br />
            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-default"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default"></asp:Button>

                    <asp:Button ID="btn_addSector" runat="server" Text="Add Sector" Visible="false" CssClass="btn btn-default"></asp:Button>

                </div>

            </div>


            <br />
            <br />
            <center>
        <ogrid:Grid runat="server" ID="Gv_ProjBudgetSector" OnDeleteCommand="Gv_ProjBudgetSector_DeleteCommand"  CallbackMode="false" AutoGenerateColumns="false"  FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowPaging="true" PageSize="10"  AllowAddingRecords="false" >
          <ScrollingSettings ScrollWidth="93%"  />
    <ExportingSettings ExportAllPages="true"  ExportTemplates="true" ExportedFilesTargetWindow="New" ColumnsToExport="Sector_Name,Category_Name,Quantity"/>
            
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
    <ClientSideEvents OnBeforeClientDelete="beforedelete" />
            <Columns>           
                 
              
                <ogrid:Column DataField="Sector_Name" HeaderText="Sector Name" ItemStyle-Wrap="true" Width="250" ></ogrid:Column>
                 <ogrid:Column DataField="Category_Name" HeaderText="Category Name" ItemStyle-Wrap="true" Width="250" ></ogrid:Column>
                 
             <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Wrap="true" Width="150" ></ogrid:Column>
                  <ogrid:Column DataField="Proj_Budget_Sec_ID" HeaderText="Edit"  ItemStyle-Wrap="true" Width="120px" runat="server" >
            <TemplateSettings  TemplateId="SectorIDTemplate"/>
        </ogrid:Column>     
                  <ogrid:Column AllowDelete="true" HeaderText="Delete" ItemStyle-Wrap="true" Width="150px"></ogrid:Column> 
                 
            </Columns>
           <Templates>
        <ogrid:GridTemplate ID="SectorIDTemplate" runat="server" >
            <Template>
             <asp:LinkButton ID="lnk_ProjBudSectorID" CommandName='<%#Container.DataItem["Proj_Budget_Sec_ID"] %>' Text="Edit" OnClick="lnk_ProjBudSectorID_Click"   runat="server" CausesValidation="false" />             
                  
            </Template>
        </ogrid:GridTemplate>
             
    </Templates>
        </ogrid:Grid>
                   <br />  
                      
                      <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" tabindex="10" />        
                      </center>
        </div>
    </div>

    <!-- ModalPopupExtender -->

    <ajaxToolkit:ModalPopupExtender ID="mpeSector" runat="server" PopupControlID="PanelLocation" TargetControlID="btn_addSector"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelLocation" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnClose" class="close" data-dismiss="modal" aria-hidden="true">x</button>
                    <center>  <h5 id="myModalLabelcrate"><asp:Label ID="lblHeading" runat="server" Text="Add Sector"></asp:Label></h5></center>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-2">
                            Sector&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="ValSector" InitialValue="-Select-" ControlToValidate="ddlBudgetSector" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-8">
                            <asp:DropDownList ID="ddlBudgetSector" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBudgetSector_SelectedIndexChanged" runat="server">
                                <asp:ListItem Text="-Select-"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" runat="server" id="trCategory" visible="false">
                        <div class="col-md-2">
                            Category &nbsp;
         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCategoryName" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValSector" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-8">
                            <asp:DropDownList ID="ddlCategoryName" CssClass="form-control" runat="server">
                                <asp:ListItem Text="-Select-"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            Quantity &nbsp;
         <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtQty" CssClass="Validation_Text" ValidationGroup="ValSector" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-8">
                          <asp:TextBox runat="server" ID="txtQty"  autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="row text-center">
                        <asp:Button ID="btnSaveSector" runat="server" Text="Save" OnClick="btnSaveSector_Click" CssClass="btn btn-default" ValidationGroup="ValSector" />
                        <asp:Button ID="btnCancelSector" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btnCancelSector_Click" CausesValidation="false" />
                    </div>

                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
