<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="AssetRegistrationList.aspx.cs" Inherits="AssetRegistrationList" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function exportgrid() {
            Grid_Asset.exportToExcel();
        }
    </script>

    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>
                
                Asset Registration List

            </h3>

        </div>
        <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="Grid_Asset" AutoGenerateColumns="false"  OnRowDataBound="Grid_Asset_RowDataBound" CallbackMode="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" OnDeleteCommand="Grid_Registration_DeleteCommand" PageSize="10" AllowGrouping="true" >
            <ScrollingSettings ScrollWidth="100%" />
            <ExportingSettings ExportAllPages="true"  ExportTemplates="true"  ExportedFilesTargetWindow="New" AppendTimeStamp="true" 
                ColumnsToExport="Code,Asset_Type,Category_Name,Name,Owner,Reg_No,Project_Name,Location_Name,Vendor_name,Condition,StandardInput,StandardInputPerHour,Bill_No,Bill_date,UOM,StandardOutputPerHour,RunningHrsKms,Unit,Average,HireCharges,HSDRecoverable,Recieve_Date,Make,Inv_Amount,Subcon_name,Remark" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

            <Columns>
                <ogrid:Column DataField="Code" HeaderText="Asset Code" runat="server" Width="120" >
                    <TemplateSettings  TemplateId="AssetCodeTemplate"/>
                </ogrid:Column>       
                <ogrid:Column DataField="Asset_Type" HeaderText="Asset Type" Width="110" Wrap="true" ></ogrid:Column> 
                
                <ogrid:Column DataField="Category_Name" HeaderText="Asset Category" Width="130"  Wrap="true" ></ogrid:Column>
                <ogrid:Column DataField="Name" HeaderText="Asset Name" Width="180"  Wrap="true" ></ogrid:Column>
                
                <ogrid:Column DataField="Owner" HeaderText="Owner"  Width="80"  Wrap="true"></ogrid:Column>
                <ogrid:Column DataField="Reg_No" HeaderText="Reg No"  Width="120px"  Wrap="true"></ogrid:Column>
               
                <ogrid:Column DataField="Project_Name" HeaderText="Project" Width="150px"  Wrap="true"></ogrid:Column>
                <ogrid:Column DataField="Location_Name" HeaderText="Location" Width="200px"  Wrap="true" ></ogrid:Column>
                <ogrid:Column DataField="Vendor_name" HeaderText="Vendor" Width="180"  Wrap="true" ></ogrid:Column>
                <ogrid:Column DataField="Condition" HeaderText="Condition" Width="100"   Wrap="true"></ogrid:Column>                
                <ogrid:Column DataField="StandardInput" HeaderText="Standrad Unit" Width="150px"></ogrid:Column>
                <ogrid:Column DataField="StandardInputPerHour" HeaderText="Standard Input per hour" Width="180px"></ogrid:Column>
                <ogrid:Column DataField="Bill_No" HeaderText="Bill No" Width="180px"></ogrid:Column>
                <ogrid:Column DataField="Bill_date" HeaderText="Bill date" Width="150px"></ogrid:Column>
                <ogrid:Column DataField="UOM" HeaderText="UOM" Width="80px"></ogrid:Column>
                <ogrid:Column DataField="StandardOutputPerHour" HeaderText="Standard OutPut per hour" Width="190px"></ogrid:Column> 
                <ogrid:Column DataField="RunningHrsKms" HeaderText="RunningHrsKms" Width="135px"></ogrid:Column>
                <ogrid:Column DataField="Unit" HeaderText="Unit" Width="100px"   Wrap="true"></ogrid:Column>
                <ogrid:Column DataField="Average" HeaderText="Average Per Unit" Width="150px"></ogrid:Column>
                <ogrid:Column DataField="HireCharges" HeaderText="Hire Charges Per Month" Width="180px"></ogrid:Column>
                <ogrid:Column DataField="HSDRecoverable" HeaderText="HSDRecoverable" Width="150px"></ogrid:Column>
                <ogrid:Column DataField="Recieve_Date" HeaderText="Asset Received Date" Width="180px"></ogrid:Column>
                <ogrid:Column DataField="Make"  HeaderText="Make" Width="180px"></ogrid:Column>
                <ogrid:Column DataField="Inv_Amount" HeaderText="Inv_Amount" Width="180px"></ogrid:Column>
                
                <ogrid:Column DataField="Subcon_name" HeaderText="Contractor" Width="150"  Wrap="true" ></ogrid:Column>
                <ogrid:Column DataField="Remark" HeaderText="Remarks"></ogrid:Column>
                <ogrid:Column  AllowDelete="true" HeaderText="Delete" Width="80"   Wrap="true"></ogrid:Column>
            </Columns>
            <Templates>
                <ogrid:GridTemplate ID="AssetCodeTemplate" runat="server">
                    <Template>              
                       <asp:HyperLink ID="lnkAssetCode" runat="server"     CssClass="gridCB"  Text='<%#Container.DataItem["Code"] %>'></asp:HyperLink>          
                    </Template>
                </ogrid:GridTemplate>
            </Templates>
        </ogrid:Grid>
                <br />
                <a href="AssetRegistration.aspx" runat="server" id="lnkbtnAdd" class="btn btn-default">Add New</a>
     
                <asp:Button ID="btnExportToPDF" runat="server" OnClick="btnExportToPDF_Click" Text="Export To PDF" CssClass="btn btn-default"></asp:Button>
                <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />  
            </center>
        </div>
    </div>
</asp:Content>
