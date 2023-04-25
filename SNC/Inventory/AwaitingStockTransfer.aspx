<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Inventory/AwaitingStockTransfer.aspx.cs" Inherits="AwaitingStockRequest" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function exportgrid() {
            Gv_StockTranferItems.exportToExcel();
        }

        window.onload = function () {
            oboutGrid.prototype.selectRecordByClick = function () {
                return;
            }
            oboutGrid.prototype.showSelectionArea = function () {
                return;
            }
        }
    </script>
    <script type="text/javascript">
          window.setInterval(function () {
              window.location.reload();
          }, 600000);
    </script>
    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                List of Stock Transfer Requests

            </h3>

        </div>
        <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="Gv_StockTransferRequest" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings ScrollWidth="70%" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
            <Columns>
                
                   <ogrid:Column DataField="Sector_Name" HeaderText="Sector Name">
                       <TemplateSettings TemplateId="BudgetSectorTemplate" />
                   </ogrid:Column>  
                 <ogrid:Column DataField="FROMPROJECT" HeaderText="From Project" ></ogrid:Column>
                           
                <ogrid:Column DataField="TransferQty" HeaderText="Transferred Qty" Align="right"></ogrid:Column>
                   <ogrid:Column DataField="Name" HeaderText="Transferred By" ></ogrid:Column>
                <ogrid:Column DataField="From_ProjectCode" HeaderText="From Project Code" Visible="false" ></ogrid:Column>
                
            </Columns>
              <Templates>
                   <ogrid:GridTemplate ID="BudgetSectorTemplate" runat="server">
                    <Template>
                        <asp:LinkButton ID="Lnkbtn_SectorName" Text='<%# Container.DataItem["Sector_Name"] %>' CommandArgument='<%#Container.DataItem["From_ProjectCode"] %>'  OnClick="Lnkbtn_SectorName_Click" runat="server"></asp:LinkButton>
                    </Template>
                </ogrid:GridTemplate>
              </Templates>
      
        </ogrid:Grid>

                 <br />
                 <br />
                 <br />
                 <br />

                 <ogrid:Grid runat="server" ID="Gv_StockTranferItems" CallbackMode="true" KeepSelectedRecords="false"  AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings ScrollWidth="100%" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
            <Columns>
                  <ogrid:Column DataField="StockTransfer_ID" HeaderText="Stock Transfer ID" Visible="false"></ogrid:Column> 
                 <ogrid:Column DataField="Sector_Name" HeaderText="Sector Name"></ogrid:Column>  
             <ogrid:Column DataField="Category_Name" HeaderText="Category Name"></ogrid:Column>                
                <ogrid:Column DataField="Item_Name" HeaderText="Item Name" ></ogrid:Column>
                <ogrid:Column DataField="Item_Code" HeaderText="Item Code" ></ogrid:Column>
                 <ogrid:Column DataField="UOMPrefix" HeaderText="UOM" ></ogrid:Column>                     
                <ogrid:Column DataField="TransferQty" HeaderText="Transferred Qty" Align="right"></ogrid:Column>
                    <ogrid:Column DataField="Status" HeaderText="Status" ></ogrid:Column>
                <ogrid:Column DataField="From_ProjectCode" HeaderText="From Project Code" Visible="false" ></ogrid:Column>
                   <ogrid:Column DataField="To_ProjectCode" HeaderText="To Project Code" Visible="false" ></ogrid:Column>
                <ogrid:CheckBoxSelectColumn ShowHeaderCheckBox="true" HeaderText=" Select All" Width="100px"></ogrid:CheckBoxSelectColumn>
                
            </Columns>
              
      
        </ogrid:Grid>
                <br />
                <br />

                    <asp:Button ID="Btn_AcceptTransferItems" runat="server" Text="Accept" OnClick="Btn_AcceptTransferItems_Click" CssClass="btn btn-default"  ></asp:Button>
                       <asp:Button ID="Btn_RejectTransferItems" runat="server" Text="Reject" OnClick="Btn_RejectTransferItems_Click" CssClass="btn btn-default"  ></asp:Button>
                   <asp:Button ID="Btn_Cancel" runat="server" Text="Cancel" CssClass="btn btn-default"  OnClick="Btn_Cancel_Click" ></asp:Button>
                  
                      </center>
        </div>
    </div>
</asp:Content>
