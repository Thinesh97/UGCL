<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="AssetMappingToProjectList.aspx.cs" Inherits="AssetMappingToProjectList" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function exportgrid() {
            GV_AssetDetailsGrid.exportToExcel();
        }
       


        function beforedelete() {
            if (confirm("Are you sure want to delete this record?") == false) {
                return false;
            }
            return true;
        }
    </script>
   
    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Asset Transfer List

            </h3>

        </div>
        <div class="panel-body">
            <center>
            <ogrid:Grid runat="server" ID="GV_TOFromGrid" AutoGenerateColumns="false" CallbackMode="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;" CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;" />
                <ScrollingSettings ScrollWidth="50%" />
                <Columns>
                   
                    <ogrid:Column DataField="Project_Code" HeaderText="To Project"  runat="server" Width="384px">
                        <TemplateSettings TemplateId="ToProjectTemplate" />
                    </ogrid:Column>
                    <ogrid:Column DataField="TotalCount" Width="100px" HeaderText="Qty"  runat="server" >
                    </ogrid:Column>
                </Columns>
                <Templates>
                    <ogrid:GridTemplate ID="ToProjectTemplate" runat="server">
                        <Template>
                            <asp:LinkButton ID="lnkbtnToProject" runat="server" CssClass="gridCB" CommandArgument='<%#Container.DataItem["Project_Code"] %>' Text='<%#Container.DataItem["Project_Name"] %>' OnClick="lnkbtnToProject_Click"></asp:LinkButton>

                        </Template>

                    </ogrid:GridTemplate>
                </Templates>
            </ogrid:Grid>
                </center>
        </div>
        <div class="panel-body">
       
                <ogrid:Grid runat="server" ID="GV_AssetDetailsGrid" KeepSelectedRecords="true"  AutoGenerateColumns="false" CallbackMode="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                    <ExportingSettings ExportAllPages="true" ColumnsToExport="Name,Asset_Code,Reg_No,Asset_Type,Category_Name,Condition,Status" ExportTemplates="true" ExportedFilesTargetWindow="New" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;" CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;" />
                    <ScrollingSettings ScrollWidth="100%" />
                    <Columns>
                        <ogrid:Column DataField="Print" HeaderText="Print">
                             <TemplateSettings TemplateId="PrintID" />
                        </ogrid:Column>
                    
                        <ogrid:Column DataField="AssetTran_ID" HeaderText="AssetTran_ID"  Visible="false">
                            
                        </ogrid:Column>
                        <ogrid:Column DataField="Name" HeaderText="Asset Name"></ogrid:Column>
                        <ogrid:Column DataField="Asset_Code" HeaderText="Asset Code"></ogrid:Column>
                        <ogrid:Column DataField="Reg_No" HeaderText="Reg No"></ogrid:Column>
                        <ogrid:Column DataField="Asset_Type" HeaderText="Asset_Type"></ogrid:Column>
                        <ogrid:Column DataField="Category_Name" HeaderText="Category"></ogrid:Column>
                        <ogrid:Column DataField="Condition" HeaderText="Condition"></ogrid:Column>
                        <ogrid:Column DataField="Status" HeaderText="Status" Width="120px"></ogrid:Column>
                        <ogrid:CheckBoxSelectColumn HeaderText="Select All" ShowHeaderCheckBox="true" Width="100px"></ogrid:CheckBoxSelectColumn>

                    </Columns>
                    <Templates>
                        <ogrid:GridTemplate ID="PrintID" runat="server">
                            <Template>
                                 
                                
 <asp:LinkButton ID="lnkbtnPrint" runat="server" CssClass="gridCB" CommandName='<%#Container.DataItem["AssetTran_ID"] %>'  Text='Print' OnClick="lnkbtnPrint_Click"></asp:LinkButton>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
                </ogrid:Grid>
            </div>
            <div class="panel-body">
              <center>
                       <br />
                        <a href="AssetMappingToProject.aspx" id="lnkbtnAdd" runat="server" class="btn btn-default">Add New Asset Transfer</a>
                      <asp:Button ID="btnCancelList" runat="server" OnClick="btnCancelList_Click" CssClass="btn btn-default" Text="Transfer Cancel" CausesValidation="false" />
             
                      <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click"></asp:Button>
               
                       <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />    
                      </center>
            </div>
        </div>
</asp:Content>
