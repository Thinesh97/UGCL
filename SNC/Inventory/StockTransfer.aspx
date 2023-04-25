<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Inventory/StockTransfer.aspx.cs" Inherits="StockTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <%--  <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>

    <script type="text/javascript">
        $(document).ready(function () {

            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
        });

    </script>

    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Stock Transfer

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-2">
                    From Project&nbsp;
                    <asp:RequiredFieldValidator ID="RFVProject" runat="server" InitialValue="-Select-" ControlToValidate="ddlFromProject" CssClass="Validation_Text" ValidationGroup="ValStock" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlFromProject" runat="server" CssClass="form-control">
                        <asp:ListItem Text="-Select-"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    To Project
&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="-Select-" ControlToValidate="ddlToProject" CssClass="Validation_Text" ValidationGroup="ValStock" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlToProject" runat="server" CssClass="form-control"  TabIndex="2">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>


            </div>

            <div class="row">
                <div class="col-md-2">
                    Department

                 

                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" TabIndex="3">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-md-2">
                    Receiver 
 &nbsp;
                 
 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="-Select-" ControlToValidate="ddlReceiver" CssClass="Validation_Text" ValidationGroup="ValStock" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlReceiver" runat="server" CssClass="form-control"  TabIndex="4">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>


            </div>




            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="Btn_Search" runat="server" OnClick="Btn_Search_Click" Text="Search" ValidationGroup="ValStock" CssClass="btn btn-default" TabIndex="5"></asp:Button>
                    <asp:Button ID="Btn_Cancel" runat="server" Text="Cancel" CssClass="btn btn-default" TabIndex="6"></asp:Button>
                </div>

            </div>
        </div>
        <br />        
              <center>
        <ogrid:Grid runat="server" ID="Gv_BudgetSector" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="false" AllowPaging="true" PageSize="10">
            <ScrollingSettings ScrollWidth="40%" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
            <Columns>
                  <ogrid:Column DataField="Sector_Name" HeaderText="Sector" >
                    <TemplateSettings TemplateId="BudgetSectorTemplate" />
                </ogrid:Column>
                             
                <ogrid:Column DataField="AvailableQty" HeaderText="Available Qty" Align="right"></ogrid:Column>
                 
               
                
            </Columns>
              
        <Templates>
                <ogrid:GridTemplate ID="BudgetSectorTemplate" runat="server">
                    <Template>
                        <asp:LinkButton ID="lnkbtnSectorName" Text='<%# Container.DataItem["Sector_Name"] %>'  OnClick="lnkbtnSectorName_Click" runat="server"></asp:LinkButton>
                    </Template>
                </ogrid:GridTemplate>
            </Templates>
        </ogrid:Grid>
                  <br />
                  <br />
                   <ogrid:Grid runat="server" ID="Gv_SectorWiseItems" CallbackMode="false"  OnUpdateCommand="Gv_SectorWiseItems_UpdateCommand" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings ScrollWidth="97%" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
            <Columns>
                
              <ogrid:Column HeaderText="Edit" AllowEdit="true" Width="100px"></ogrid:Column>
                 <ogrid:Column DataField="Sector_Name" HeaderText="Sector_Name" ReadOnly="true"></ogrid:Column>                
                <ogrid:Column DataField="Category_Name" HeaderText="Category" ReadOnly="true"></ogrid:Column>
                  <ogrid:Column DataField="Item_Code" HeaderText="Item Code" ReadOnly="true"></ogrid:Column>                
                <ogrid:Column DataField="Item_Name" HeaderText="Item Name" ReadOnly="true"></ogrid:Column>
                  <ogrid:Column DataField="UOMPrefix" HeaderText="UOM" ReadOnly="true"></ogrid:Column>                
                <ogrid:Column DataField="Updated_QTY" HeaderText="Available Qty"  ReadOnly="true" Align="right"></ogrid:Column>

                   <ogrid:Column DataField="TransferQty" HeaderText="Transfer Qty" Align="right">
                       <%-- <TemplateSettings TemplateId="TransferQtyTemplate" />--%>
                   </ogrid:Column>
                 <ogrid:CheckBoxSelectColumn HeaderText=" Select All" ShowHeaderCheckBox="true" Width="100px" ></ogrid:CheckBoxSelectColumn>
                 
               
                
            </Columns>
             <%-- <Templates>
                <ogrid:GridTemplate ID="TransferQtyTemplate" runat="server">
                    <Template>
                       <asp:TextBox ID="txtTransferQty" runat="server" Text='<%#Container.DataItem["TransferQty"] %>'></asp:TextBox>
                    </Template>
                </ogrid:GridTemplate>
            </Templates>--%>
      
        </ogrid:Grid>
                  <br />

                  </center>
         <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="Btn_StockTransfer" runat="server" Text="Transfer" OnClick="Btn_StockTransfer_Click"  ValidationGroup="ValStockTransfer" CssClass="btn btn-default" TabIndex="9"></asp:Button>
                    <asp:Button ID="Btn_StockTranCancel" runat="server" Text="Cancel" CssClass="btn btn-default" TabIndex="10"></asp:Button>
                </div>

            </div>
        <br />
    </div>
</asp:Content>
