<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="ProjectList.aspx.cs" Inherits="ProjectList" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
            function exportgrid() {
                ProjectList_Grid.exportToExcel();
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
                Project List
            </h3>
        </div>
        
        <div class="panel-body">
            <center>
                <ogrid:Grid runat="server" ID="ProjectList_Grid" AutoGenerateColumns="false" AllowGrouping="true" CallbackMode="false" OnRowDataBound="ProjectList_Grid_RowDataBound" OnDeleteCommand="ProjectList_Grid_DeleteCommand" AllowRecordSelection="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">         
                    <ScrollingSettings ScrollWidth="100%" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
                    <ExportingSettings ExportAllPages="true" ExportedFilesTargetWindow="New"   ExportTemplates="true"  ColumnsToExport="Project_Code,Project_Name,Proj_Type,Location,Proj_Cost,StartDate,EndDate,Award_By,Status,Description"  />
                    <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                    <Columns>
               
                        <ogrid:Column DataField="Project_Code" HeaderText="Project ID" runat="server" Width="200" >
                            <TemplateSettings  TemplateId="ProjectIDTemplate"/>
                        </ogrid:Column> 
                        <ogrid:Column DataField="Project_Name" HeaderText="Project  Name" Wrap="true"  Width="180px"></ogrid:Column>
                        <ogrid:Column DataField="Proj_Type" HeaderText="Project Type" Wrap="true" Width="130px" ></ogrid:Column>
                        <ogrid:Column DataField="State_Name" HeaderText="State" Wrap="true" Width="130px"></ogrid:Column>   
                        <ogrid:Column DataField="Location_Name" HeaderText="Location" Wrap="true" Width="150px"></ogrid:Column>
                        <ogrid:Column DataField="Proj_Cost" HeaderText="Project cost" Align="right" Width="120px"></ogrid:Column>
                        <ogrid:Column DataField="StartDate" HeaderText="Start Date"  Width="100px" DataFormatString="{0:dd-MM-yyyy}"></ogrid:Column>
                        <ogrid:Column DataField="EndDate" HeaderText="End Date" Width="100px" DataFormatString="{0:dd-MM-yyyy}" ></ogrid:Column>
                        <ogrid:Column DataField="Award_By" HeaderText="Awarded By" Width="130px" ></ogrid:Column>
                        <ogrid:Column DataField="Status" HeaderText="Status" Width="80px" ></ogrid:Column>  
                        <ogrid:Column DataField="Description" HeaderText="Description" Wrap="true" Width="220px"></ogrid:Column>
                        <ogrid:Column  HeaderText="Delete" AllowDelete="true"  Width="90px"></ogrid:Column>        
                    </Columns>
                    <Templates>
                        <ogrid:GridTemplate ID="ProjectIDTemplate" runat="server">
                            <Template>
                                <asp:HyperLink ID="lnkProjectID" runat="server" CssClass="gridCB" Text='<%#Container.DataItem["Project_Code"] %>'></asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>

                    </Templates>

                </ogrid:Grid>
                <br />
                <asp:LinkButton ID="lnkbtnAdd" Text="Add New Project" PostBackUrl="~/Project/Project.aspx" CssClass="btn btn-default"  runat="server"></asp:LinkButton>
                <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click" ></asp:Button>
                    
                <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />         
            </center>
        </div>
    </div>
</asp:Content>

