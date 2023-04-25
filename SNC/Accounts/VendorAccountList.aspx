<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true"  CodeBehind="VendorAccountList.aspx.cs" Inherits="SNC.Accounts.VendorAccountList" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function exportgrid() {
            Grid_VendorAccountList.exportToExcel();
        }

       
        function beforedelete() {
            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
               return false;
            }
            else {
              
                return true;
            }
        }
    </script>

    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

               Vendor Account List

            </h3>

        </div>
        <br />
        <br />
        <div>
            <div class="panel-body">
            <center>
                <ogrid:Grid runat="server" ID="Grid_VendorAccountList" CallbackMode="false" AutoGenerateColumns="false" OnRowDataBound="Grid_VendorAccount_RowDataBound" 
                   
                    FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="10" >
                    <ScrollingSettings ScrollWidth="100%"  ScrollHeight="400"/>
                      <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"
                         ColumnsToExport="WONo,WODate,Subcon_name,Type_Name,DurationOfWork,Status,Name,Prepared_By,Other_Terms,Remarks" />
                      <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

                   <Columns>
                        <ogrid:Column DataField="Vendor_ID" HeaderText="Vendor ID" Width="100px" >
                             <TemplateSettings  TemplateId="Vendor_IDTemplate"/>
                        </ogrid:Column>
                        <ogrid:Column DataField="Vendor_name" HeaderText="Vendor Vame" Width="400px" Wrap="false"></ogrid:Column>
                            <ogrid:Column DataField="TotalBilledAmount" HeaderText="Total Billing Amount" Width="200px" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="PaidAmt" HeaderText="Total Payment Done" Width="200px" Wrap="true"></ogrid:Column>
                    </Columns>
                    <Templates>
                        <ogrid:GridTemplate ID="Vendor_IDTemplate" runat="server">
                            <Template>
                               <asp:HyperLink ID="lnkVendor_ID" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["Vendor_ID"] %>'>
                        </asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
                </ogrid:Grid>
                <br />
                <center>
                    <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />  
                </center>
                      </center>
        </div>
        </div>
           </div>

</asp:Content>
