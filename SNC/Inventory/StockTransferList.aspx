<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="StockTransferList.aspx.cs" Inherits="StockTransferList" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function exportgrid() {
            Gv_SectorWiseItems.exportToExcel();
        }
    </script>
    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                List of Stock Transfer

            </h3>

        </div>
        <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="Gv_StockTransferList" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
            <Columns>
                  <ogrid:Column DataField="Sector_Name" HeaderText="Sector Name">
                      <TemplateSettings  TemplateId="BudgetSectorTemplate"/>
                  </ogrid:Column>  
                 <ogrid:Column DataField="TOPROJECT" HeaderText="To Project" Wrap="true" ></ogrid:Column>
                <ogrid:Column DataField="TransferQty" HeaderText="Transferred Quantity" Align="right"></ogrid:Column>                 
               
                  <ogrid:Column DataField="To_ProjectCode" HeaderText="To Project Code" Visible="false"></ogrid:Column> 
            </Columns>
                <Templates>
                <ogrid:GridTemplate ID="BudgetSectorTemplate" runat="server">
                    <Template>
                        <asp:LinkButton ID="Lnkbtn_SectorName" Text='<%# Container.DataItem["Sector_Name"] %>' CommandArgument='<%# Container.DataItem["To_ProjectCode"] %>'  OnClick="Lnkbtn_SectorName_Click" runat="server"></asp:LinkButton>
                    </Template>
                </ogrid:GridTemplate>
            </Templates>
      
        </ogrid:Grid>

                 <br />
                 <br />
                 <br />
                 <br />

                 <ogrid:Grid runat="server" ID="Gv_SectorWiseItems" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings ScrollWidth="100%" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
            <Columns>
                <ogrid:Column DataField="Sector_Name" HeaderText="Sector Name"></ogrid:Column> 
             <ogrid:Column DataField="Category_Name" HeaderText="Category Name"></ogrid:Column>                
                <ogrid:Column DataField="Item_Name" HeaderText="Item Name" ></ogrid:Column>
                <ogrid:Column DataField="Item_Code" HeaderText="Item Code" ></ogrid:Column>
                 <ogrid:Column DataField="UOMPrefix" HeaderText="UOM" Width="80px" ></ogrid:Column>                     
                <ogrid:Column DataField="TransferQty" HeaderText="Transferred Quantity" Align="right" ></ogrid:Column>
                   <ogrid:Column DataField="TransfferdDate" HeaderText="Transfferd Date"></ogrid:Column>
                  <ogrid:Column DataField="Status" HeaderText="Status" ></ogrid:Column>
                
            </Columns>
              
      
        </ogrid:Grid>
                <br />
                <br />
                     
                    <asp:LinkButton ID="lnkbtnAdd" Text="New Stock Transfer" PostBackUrl="~/Inventory/StockTransfer.aspx" CssClass="btn btn-default"  runat="server"></asp:LinkButton>
                    
                    <asp:Button ID="Btn_ExportToPDF" runat="server" Text="Export To PDF"  OnClick="Btn_ExportToPDF_Click" CssClass="btn btn-default"></asp:Button>
                  <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />      
                      </center>
        </div>
    </div>
</asp:Content>
