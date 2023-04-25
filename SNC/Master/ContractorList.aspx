<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Master/ContractorList.aspx.cs" Inherits="ContractorList" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script type="text/javascript">
         function exportgridReg() {
             RegContractorGrid.exportToExcel();
         }
         function exportgridUnReg() {
             UnRegContractorGrid.exportToExcel();
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
                List Of Contractor 
            </h3>
        </div>
        
        <div class="panel-body">
            <center>
                <h3 class="panel-title"><b> Registered Contractor </b></h3>
            <ogrid:Grid runat="server" ID="RegContractorGrid" OnRowDataBound="ContractorGrid_RowDataBound" AutoGenerateColumns="false" CallbackMode="false" OnDeleteCommand="ContractorGrid_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                <ScrollingSettings ScrollWidth="100%" />
                <ExportingSettings ExportAllPages="true" ExportTemplates="true"  ColumnsToExport="Subcon_ID,Subcon_name,Regs_No,Con_Person,Con_No,Email_ID,PAN_No,ST_No,PFRegistration_No,LabourLicence_No,Bank,Branch,Account_Type,Acc_No,IFSC,Add_Line,Landmark,City,State,Country,Pin,Bank2,Branch2,Account_Type2,Acc_No2,IFSC2,Nature_Work,Remark,RefPerson, File_GSTRegistration, File_PANCopy, File_BankDetails" />
                <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
                <ClientSideEvents OnBeforeClientDelete="beforedelete" />
			
                <Columns>  
                    <ogrid:Column HeaderText="Contractor ID" DataField="Subcon_ID" runat="server" Width="150" >
                        <TemplateSettings  TemplateId="ContractorIDTemplate1"/>
                    </ogrid:Column>  
                    <ogrid:Column DataField="Subcon_name" HeaderText="Contractor Name" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="Regs_No" HeaderText="Registration No" ></ogrid:Column>
                    <ogrid:Column DataField="Con_Person" HeaderText="Contact Person" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="Con_No" HeaderText="Contact No" ></ogrid:Column>
                    <ogrid:Column DataField="Email_ID" HeaderText="Email ID" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="PAN_No" HeaderText="PAN No" ></ogrid:Column>
                    <ogrid:Column DataField="ST_No" HeaderText="Service Tax" ></ogrid:Column>
               
                    <ogrid:Column DataField="PFRegistration_No" HeaderText="PF Registration No" ></ogrid:Column>
                    <ogrid:Column DataField="LabourLicence_No" HeaderText="Labour Licence No" ></ogrid:Column>
                    <ogrid:Column DataField="Bank" HeaderText="Bank Name" ></ogrid:Column>
                    <ogrid:Column DataField="Branch" HeaderText="Branch"></ogrid:Column>
                    <ogrid:Column DataField="Account_Type" HeaderText="Acc Type" ></ogrid:Column>
                    <ogrid:Column DataField="Acc_No" HeaderText="Account No" ></ogrid:Column>
                    <ogrid:Column DataField="IFSC" HeaderText="IFSC Code" ></ogrid:Column> 
                    <ogrid:Column DataField="Add_Line" HeaderText="Address" ></ogrid:Column>
                    <ogrid:Column DataField="Landmark" HeaderText="Landmark" ></ogrid:Column>
                    <ogrid:Column DataField="City" HeaderText="City"></ogrid:Column>
                    <ogrid:Column DataField="State" HeaderText="State" Wrap="true"></ogrid:Column>
                    <ogrid:Column DataField="Country" HeaderText="Country" ></ogrid:Column>
                    <ogrid:Column DataField="Pin" HeaderText="PinCode" ></ogrid:Column>

                    <ogrid:Column DataField="Bank2" HeaderText="Bank 2" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="Branch2" HeaderText="Branch 2" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="Account_Type2" HeaderText="Account_Type 2" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="Acc_No2" HeaderText="Acc_No 2" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="IFSC2" HeaderText="IFSC 2" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="Remark" HeaderText="Remarks" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="RefPerson" HeaderText="Refer Person"></ogrid:Column>
                    <ogrid:Column DataField="Nature_Work" HeaderText="Nature Of Work" ></ogrid:Column>
                    <ogrid:Column DataField="File_GSTRegistration" HeaderText="GST Uploaded" runat="server" Width="125px">
                        <TemplateSettings TemplateId="GSTFileTemplate1"/>
                    </ogrid:Column>
                    <ogrid:Column DataField="File_PANCopy" HeaderText="PAN Uploaded" runat="server" Width="125px">
                        <TemplateSettings TemplateId="PANFileTemplate1"/>
                    </ogrid:Column>
                    <ogrid:Column DataField="File_BankDetails" HeaderText="BankDetails Uploaded" runat="server" Width="160px">
                        <TemplateSettings TemplateId="BankFileTemplate1"/>
                    </ogrid:Column>
                    <ogrid:Column DataField="File_IDProof" HeaderText="ID Proof Uploaded" runat="server" Width="150px" Visible="false">
                        <TemplateSettings TemplateId="IDFileTemplate1"/>
                    </ogrid:Column>
                    <ogrid:Column DataField="" HeaderText="Delete" AllowDelete="true" Width="80px" ></ogrid:Column> 
                </Columns>
                <Templates>
                    <ogrid:GridTemplate ID="ContractorIDTemplate1" runat="server">
                        <Template>
                                <%--<a id="lnkSubcontractor" runat="server" href='SubContractor.aspx?ID=<%#Container.DataItem["Subcon_ID"] %>'><%#Container.DataItem["Subcon_ID"] %> </a>   --%>
                              <asp:HyperLink ID="lnkSubcontractor" runat="server" CssClass="gridCB" Text='<%#Container.DataItem["Subcon_ID"] %>'>
                            </asp:HyperLink>
                        </Template>
                    </ogrid:GridTemplate>

                    <ogrid:GridTemplate ID="GSTFileTemplate1" runat="server">
                        <Template>               
                            <asp:Label runat="server" Text='<%#Container.DataItem["File_GSTRegistration"].ToString() != "" ? "Yes" : "No" %>'></asp:Label>
                        </Template>
                    </ogrid:GridTemplate>
                    
                    <ogrid:GridTemplate ID="PANFileTemplate1" runat="server">
                        <Template>               
                            <asp:Label runat="server" Text='<%#Container.DataItem["File_PANCopy"].ToString() != "" ? "Yes" : "No"  %>'></asp:Label>
                        </Template>
                    </ogrid:GridTemplate>
                    
                    <ogrid:GridTemplate ID="BankFileTemplate1" runat="server">
                        <Template>               
                            <asp:Label runat="server" Text='<%#Container.DataItem["File_BankDetails"].ToString() != "" ? "Yes" : "No"  %>'></asp:Label>
                        </Template>
                    </ogrid:GridTemplate>

                    <ogrid:GridTemplate ID="IDFileTemplate1" runat="server">
                        <Template>               
                            <asp:Label runat="server" Text='<%#Container.DataItem["File_IDProof"].ToString() != "" ? "Yes" : "No"  %>'></asp:Label>
                        </Template>
                    </ogrid:GridTemplate>

                </Templates>
            </ogrid:Grid>
                <br />
                <div class="row">
                    <asp:Button ID="btnExportToPDFReg" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDFReg_Click" ></asp:Button>
                    <input onclick="exportgridReg()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />
                </div>
                <br />
                <h3 class="panel-title"><b> Un Registered Contractor </b></h3>
            <ogrid:Grid runat="server" ID="UnRegContractorGrid" OnRowDataBound="ContractorGrid_RowDataBound" AutoGenerateColumns="false" CallbackMode="false" OnDeleteCommand="ContractorGrid_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                <ScrollingSettings ScrollWidth="100%" />
                <ExportingSettings ExportAllPages="true" ExportTemplates="true"  ColumnsToExport="Subcon_ID,Subcon_name,Regs_No,Con_Person,Con_No,Email_ID,PAN_No,ST_No,PFRegistration_No,LabourLicence_No,Bank,Branch,Account_Type,Acc_No,IFSC,Add_Line,Landmark,City,State,Country,Pin,Bank2,Branch2,Account_Type2,Acc_No2,IFSC2,Nature_Work,Remark,RefPerson, File_PANCopy, File_BankDetails, File_IDProof" />
                <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
                <ClientSideEvents OnBeforeClientDelete="beforedelete" />
			
                <Columns>  
                    <ogrid:Column HeaderText="Contractor ID" DataField="Subcon_ID" runat="server" Width="150" >
                        <TemplateSettings  TemplateId="ContractorIDTemplate2"/>
                    </ogrid:Column>  
                    <ogrid:Column DataField="Subcon_name" HeaderText="Contractor Name" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="Regs_No" HeaderText="Registration No" ></ogrid:Column>
                    <ogrid:Column DataField="Con_Person" HeaderText="Contact Person" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="Con_No" HeaderText="Contact No" ></ogrid:Column>
                    <ogrid:Column DataField="Email_ID" HeaderText="Email ID" ></ogrid:Column>
                    <ogrid:Column DataField="PAN_No" HeaderText="PAN No" ></ogrid:Column>
                    <ogrid:Column DataField="ST_No" HeaderText="Service Tax" ></ogrid:Column>
               
                    <ogrid:Column DataField="PFRegistration_No" HeaderText="PF Registration No" ></ogrid:Column>
                    <ogrid:Column DataField="LabourLicence_No" HeaderText="Labour Licence No" ></ogrid:Column>
                    <ogrid:Column DataField="Bank" HeaderText="Bank Name" ></ogrid:Column>
                    <ogrid:Column DataField="Branch" HeaderText="Branch"></ogrid:Column>
                    <ogrid:Column DataField="Account_Type" HeaderText="Acc Type" ></ogrid:Column>
                    <ogrid:Column DataField="Acc_No" HeaderText="Account No" ></ogrid:Column>
                    <ogrid:Column DataField="IFSC" HeaderText="IFSC Code" ></ogrid:Column> 
                    <ogrid:Column DataField="Add_Line" HeaderText="Address" ></ogrid:Column>
                    <ogrid:Column DataField="Landmark" HeaderText="Landmark" ></ogrid:Column>
                    <ogrid:Column DataField="City" HeaderText="City"></ogrid:Column>
                    <ogrid:Column DataField="State" HeaderText="State" Wrap="true"></ogrid:Column>
                    <ogrid:Column DataField="Country" HeaderText="Country" ></ogrid:Column>
                    <ogrid:Column DataField="Pin" HeaderText="PinCode" ></ogrid:Column>

                    <ogrid:Column DataField="Bank2" HeaderText="Bank 2" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="Branch2" HeaderText="Branch 2" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="Account_Type2" HeaderText="Account_Type 2" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="Acc_No2" HeaderText="Acc_No 2" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="IFSC2" HeaderText="IFSC 2" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="Remark" HeaderText="Remarks" Wrap="true" ></ogrid:Column>
                    <ogrid:Column DataField="RefPerson" HeaderText="Refer Person"></ogrid:Column>
                    <ogrid:Column DataField="Nature_Work" HeaderText="Nature Of Work" ></ogrid:Column>
                    <ogrid:Column DataField="File_GSTRegistration" HeaderText="GST Uploaded" runat="server" Width="125px" Visible="false">
                        <TemplateSettings TemplateId="GSTFileTemplate2"/>
                    </ogrid:Column>
                    <ogrid:Column DataField="File_PANCopy" HeaderText="PAN Uploaded" runat="server" Width="125px">
                        <TemplateSettings TemplateId="PANFileTemplate2"/>
                    </ogrid:Column>
                    <ogrid:Column DataField="File_BankDetails" HeaderText="BankDetails Uploaded" runat="server" Width="160px">
                        <TemplateSettings TemplateId="BankFileTemplate2"/>
                    </ogrid:Column>
                    <ogrid:Column DataField="File_IDProof" HeaderText="ID Proof Uploaded" runat="server" Width="150px">
                        <TemplateSettings TemplateId="IDFileTemplate2"/>
                    </ogrid:Column>
                    <ogrid:Column DataField="" HeaderText="Delete" AllowDelete="true" Width="80px" ></ogrid:Column> 
                </Columns>
                <Templates>
                    <ogrid:GridTemplate ID="ContractorIDTemplate2" runat="server">
                        <Template>
                                <%--<a id="lnkSubcontractor" runat="server" href='SubContractor.aspx?ID=<%#Container.DataItem["Subcon_ID"] %>'><%#Container.DataItem["Subcon_ID"] %> </a>   --%>
                              <asp:HyperLink ID="lnkSubcontractor" runat="server" CssClass="gridCB" Text='<%#Container.DataItem["Subcon_ID"] %>'>
                            </asp:HyperLink>
                        </Template>
                    </ogrid:GridTemplate>

                    <ogrid:GridTemplate ID="GSTFileTemplate2" runat="server">
                        <Template>               
                            <asp:Label runat="server" Text='<%#Container.DataItem["File_GSTRegistration"].ToString() != "" ? "Yes" : "No" %>'></asp:Label>
                        </Template>
                    </ogrid:GridTemplate>
                    
                    <ogrid:GridTemplate ID="PANFileTemplate2" runat="server">
                        <Template>               
                            <asp:Label runat="server" Text='<%#Container.DataItem["File_PANCopy"].ToString() != "" ? "Yes" : "No"  %>'></asp:Label>
                        </Template>
                    </ogrid:GridTemplate>
                    
                    <ogrid:GridTemplate ID="BankFileTemplate2" runat="server">
                        <Template>               
                            <asp:Label runat="server" Text='<%#Container.DataItem["File_BankDetails"].ToString() != "" ? "Yes" : "No"  %>'></asp:Label>
                        </Template>
                    </ogrid:GridTemplate>

                    <ogrid:GridTemplate ID="IDFileTemplate2" runat="server">
                        <Template>               
                            <asp:Label runat="server" Text='<%#Container.DataItem["File_IDProof"].ToString() != "" ? "Yes" : "No"  %>'></asp:Label>
                        </Template>
                    </ogrid:GridTemplate>

                </Templates>
            </ogrid:Grid>
                <br />
                <div class="row">
                    <asp:Button ID="btnExportToPDFUnReg" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDFUnReg_Click" ></asp:Button>
                    <input onclick="exportgridUnReg()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />
                </div>
                    <br />
                <asp:LinkButton ID="lnkbtnAdd" Text="Add New Contractor" PostBackUrl="~/Master/SubContractor.aspx" CssClass="btn btn-default"  runat="server"></asp:LinkButton>          
            </center>
        </div>
    </div>
</asp:Content>

