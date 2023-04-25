<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true"  CodeBehind="VendorAccountDetail.aspx.cs" Inherits="SNC.Accounts.VendorAccountDetail" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function exportgrid() {
            Grid_DPR_List.exportToExcel();
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
                 <div class="row">
                    <div style="width: 100%">
                        <table style="border: 1px solid; padding-left:100px; width: 100%";>
                            <tr>
                                <th colspan="3"style="font-size:large;text-align: center">Accounts Summary Of - <label style="color:brown" runat="server" id="Vendorname"></label></th>
                            </tr>
                            <tr style="border: 1px solid;">
                                <td style="border: 1px solid; text-align:center">Total Billed Amount</td>
                                <td style="border: 1px solid;text-align:center">Total Paid Amount</td>
                                <td style="border: 1px solid;text-align:center">Total OutStanding</td>
                            </tr>
                            <tr style="border: 1px solid;">
                                <td style="border: 1px solid;text-align:center; font:200px">
                                    <label style="font-size:large; color:green" runat="server" id="Amount_Billed"></label>
                                </td>
                                <td style="border: 1px solid;text-align:center">
                                    <label style="font-size:large; color:darkgoldenrod" runat="server" id="Amount_PAid"></label>
                                </td>
                                <td style="border: 1px solid;text-align:center">
                                    <label style="font-size:large; color:green"  runat="server" id="Balance_Amount"></label>
                                </td>
                            </tr>
                        </table>
                    </div>
                     
                </div>
                <br />
                <hr />
                 <h4 style="text-align:center"> <label id="lbl_grid_Billedamount" runat="server"></label> Summary of Billed Amount History</h4>
            <center>
                <ogrid:Grid runat="server" ID="Grid_VendorAccountDetail" CallbackMode="false" AutoGenerateColumns="false"  OnRowDataBound="Grid_VendorAccount_RowDataBound" 
                   
                    FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="10" >
                    <ScrollingSettings ScrollWidth="100%"  ScrollHeight="400"/>
                      <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"
                         ColumnsToExport="WONo,WODate,Subcon_name,Type_Name,DurationOfWork,Status,Name,Prepared_By,Other_Terms,Remarks" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

                    <Columns>
                        <ogrid:Column DataField="Vendor_ID" HeaderText="Vendor ID" Width="190px" >
                             <TemplateSettings  TemplateId="WONoTemplate"/> 
                        </ogrid:Column>
                         <ogrid:Column DataField="Vendor_ID" HeaderText="Vendor ID" Wrap="false"></ogrid:Column>
                        <ogrid:Column DataField="MRN_No" HeaderText="MRN No" Wrap="false"></ogrid:Column>
                        <ogrid:Column DataField="Vendor_name" HeaderText="Vendor Vame" Width="180px" Wrap="true"></ogrid:Column>
                            <ogrid:Column DataField="InvoiceAmt" HeaderText="Total Billed Amount" Width="180px" Wrap="true"></ogrid:Column>
                    </Columns>
                    
                </ogrid:Grid>
                <br />
                <center>
                    <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />  
                </center>
                      </center>
        </div>
           <br />
            <br />
               <h4 style="text-align:center"> <label id="Grid_name_Vendor" runat="server"></label>Summary of paid Amount History</h4>
            <center>
                <ogrid:Grid runat="server" ID="Grid_VendorAccountDetailPaid" CallbackMode="false" AutoGenerateColumns="false"  OnRowDataBound="Grid_VendorAccount_RowDataBound" 
                   
                    FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="10" >
                    <ScrollingSettings ScrollWidth="100%"  ScrollHeight="400"/>
                      <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"
                         ColumnsToExport="WONo,WODate,Subcon_name,Type_Name,DurationOfWork,Status,Name,Prepared_By,Other_Terms,Remarks" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

                    <Columns>
                        <ogrid:Column DataField="Vendor_ID" HeaderText="Vendor ID" Width="190px" >
                             <TemplateSettings  TemplateId="WONoTemplate"/> 
                        </ogrid:Column>
                         <ogrid:Column DataField="PayInd_No" HeaderText="Payment Indent No " Wrap="false"></ogrid:Column>
                        <ogrid:Column DataField="PayInd_Date" HeaderText="Payment Indent Date" Wrap="false"></ogrid:Column>
                         <ogrid:Column DataField="Vendor_name" HeaderText="Vendor name" Wrap="false"></ogrid:Column>                        
                        <ogrid:Column DataField="Project_Code" HeaderText="Project Code" Width="180px" Wrap="true"></ogrid:Column>
                            <ogrid:Column DataField="Amt_Transferable" HeaderText="Total Paid Amount" Width="180px" Wrap="true"></ogrid:Column>
                    </Columns>
                    
                </ogrid:Grid>
                <br />
                <center>
                    <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />  
                </center>
                      </center>
        </div>
           </div>

</asp:Content>
