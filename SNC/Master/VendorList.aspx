<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Master/VendorList.aspx.cs" Inherits="VendorList" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function exportgrid() {
            VendorGrid.exportToExcel();
        }
        function beforedelete() {
            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                return false;
            }
            return true;
        }
    </script>


    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>
                List Of Vendors 
            </h3>
        </div>
        
        <div class="panel-body">
            <center>
            <ogrid:Grid runat="server" ID="VendorGrid" AutoGenerateColumns="false" OnRowDataBound="VendorGrid_RowDataBound" CallbackMode="false" OnDeleteCommand="VendorGrid_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                <ScrollingSettings ScrollWidth="100%" />
                <ExportingSettings ExportAllPages="true" ExportTemplates="true" 
                    ColumnsToExport="Vendor_ID,Vendor_name,Regs_No,Con_Person,Con_No,Email_ID,PAN_No,Bank,PFRegistration_No,LabourLicence_No,Branch,Acc_No,IFSC,Add_Line,Landmark,City,State,Country,Pin,ST_No,Account_Type,Bank2,Branch2,Account_Type2,Acc_No2,IFSC2,Nature_Work,Remark,RefPerson,File_GSTRegistration,File_PANCopy,File_BankDetails,Is_Asset" />
                <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  
                    CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
                <ClientSideEvents OnBeforeClientDelete="beforedelete" />
			
                <Columns>  
                    <ogrid:Column DataField="Vendor_ID" HeaderText="Vendor ID" runat="server" Width="100px" >
                        <TemplateSettings  TemplateId="VendorIDTemplate"/>
                    </ogrid:Column> 
                    <ogrid:Column DataField="Vendor_name" HeaderText="Vendor Name" Wrap="true" Width="250px"></ogrid:Column>
                    <ogrid:Column DataField="Regs_No" HeaderText="Registration No" Wrap="true"></ogrid:Column>
                    <ogrid:Column DataField="Con_Person" HeaderText="Contact Person" Wrap="true"></ogrid:Column>
                    <ogrid:Column DataField="Con_No" HeaderText="Contact No" Wrap="true" Width="110px" ></ogrid:Column>
                    <ogrid:Column DataField="Email_ID" HeaderText="Email ID" Wrap="true"></ogrid:Column>
               
                    <ogrid:Column DataField="PAN_No" HeaderText="PAN No"  Wrap="true" Width="130px"></ogrid:Column>
                    <ogrid:Column DataField="Bank" HeaderText="Bank Name" Wrap="true" Width="220px"></ogrid:Column>
                    <ogrid:Column DataField="PFRegistration_No" HeaderText="PF Registration No" ></ogrid:Column>
                    <ogrid:Column DataField="LabourLicence_No" HeaderText="Labour Licence No" ></ogrid:Column>
                    <ogrid:Column DataField="Branch" HeaderText="Branch" ></ogrid:Column>                
                    <ogrid:Column DataField="Acc_No" HeaderText="Account No" ></ogrid:Column>
                    <ogrid:Column DataField="IFSC" HeaderText="IFSC Code" ></ogrid:Column>
                    <ogrid:Column DataField="Add_Line" HeaderText="Address Line" Wrap="true" Width="250px"></ogrid:Column>
                    <ogrid:Column DataField="Landmark" HeaderText="Landmark" Wrap="true"></ogrid:Column>
                    <ogrid:Column DataField="City" HeaderText="City" Wrap="true" Width="110px"></ogrid:Column>
                    <ogrid:Column DataField="State" HeaderText="State" Wrap="true" Width="110px"></ogrid:Column>
                    <ogrid:Column DataField="Country" HeaderText="Country" Wrap="true" Width="110px"></ogrid:Column>
                
                    <ogrid:Column DataField="Pin" HeaderText="PinCode" Wrap="true" Width="110px"></ogrid:Column>
              
                    <ogrid:Column DataField="ST_No" HeaderText="Service Tax No" Wrap="true"></ogrid:Column>
                    <ogrid:Column DataField="Account_Type" HeaderText="Account Type" Wrap="true" Width="120px"></ogrid:Column>
                    <ogrid:Column DataField="Bank2" HeaderText="Bank 2" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="Branch2" HeaderText="Branch 2" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="Account_Type2" HeaderText="Account_Type 2" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="Acc_No2" HeaderText="Acc_No 2" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="IFSC2" HeaderText="IFSC 2" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="Nature_Work" HeaderText="Nature Work" Wrap="true"></ogrid:Column>
                    <ogrid:Column DataField="Remark" HeaderText="Remarks" Wrap="true"></ogrid:Column>
                    <ogrid:Column DataField="RefPerson" HeaderText="Refer Person" Wrap="true"></ogrid:Column>
                    <ogrid:Column DataField="File_GSTRegistration" HeaderText="GST Uploaded" runat="server" Width="125px">
                        <TemplateSettings TemplateId="GSTFileTemplate"/>
                    </ogrid:Column>
                    <ogrid:Column DataField="File_PANCopy" HeaderText="PAN Uploaded" runat="server" Width="125px">
                        <TemplateSettings TemplateId="PANFileTemplate"/>
                    </ogrid:Column>
                    <ogrid:Column DataField="File_BankDetails" HeaderText="BankDetails Uploaded" runat="server" Width="160px">
                        <TemplateSettings TemplateId="BankFileTemplate"/>
                    </ogrid:Column>
                    <ogrid:Column DataField="Is_Asset" HeaderText="Is Asset" runat="server" Width="100" >
                        <TemplateSettings TemplateId="IsAssetTemplate"/>
                    </ogrid:Column>    
                    <ogrid:Column DataField="" HeaderText="Delete" AllowDelete="true"  Width="90px"></ogrid:Column>
                </Columns>
            
                <Templates>
                    <ogrid:GridTemplate ID="VendorIDTemplate" runat="server">
                        <Template>               
                            <asp:HyperLink ID="lnlVendorID" runat="server" CssClass="gridCB" Text='<%#Container.DataItem["Vendor_ID"] %>'></asp:HyperLink>
                        </Template>
                    </ogrid:GridTemplate>
                    
                    <ogrid:GridTemplate ID="GSTFileTemplate" runat="server">
                        <Template>               
                            <asp:Label runat="server" Text='<%#Container.DataItem["File_GSTRegistration"].ToString() != "" ? "Yes" : "No" %>'></asp:Label>
                        </Template>
                    </ogrid:GridTemplate>
                    
                    <ogrid:GridTemplate ID="PANFileTemplate" runat="server">
                        <Template>               
                            <asp:Label runat="server" Text='<%#Container.DataItem["File_PANCopy"].ToString() != "" ? "Yes" : "No"  %>'></asp:Label>
                        </Template>
                    </ogrid:GridTemplate>
                    
                    <ogrid:GridTemplate ID="BankFileTemplate" runat="server">
                        <Template>               
                            <asp:Label runat="server" Text='<%#Container.DataItem["File_BankDetails"].ToString() != "" ? "Yes" : "No"  %>'></asp:Label>
                        </Template>
                    </ogrid:GridTemplate>
                    
                    <ogrid:GridTemplate ID="IsAssetTemplate" runat="server">
                        <Template>               
                            <asp:Label ID="lblIsAsset" runat="server" Text='<%#Container.DataItem["Is_Asset"].ToString() == "True" ? "Yes" : "No"  %>'></asp:Label>
                        </Template>
                    </ogrid:GridTemplate>

                </Templates>
            </ogrid:Grid>
            <br />
                
            <asp:LinkButton ID="lnkbtnAdd" Text="Add New Vendor" PostBackUrl="~/Master/VendorDetails.aspx" CssClass="btn btn-default"  runat="server"></asp:LinkButton>
            <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click" ></asp:Button>
                    
            <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value="Export To Excel" />    
            </center>
        </div>
    </div>
</asp:Content>

