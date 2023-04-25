<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="ProjectBudgetList.aspx.cs" Inherits="ProjectBudgetList" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function exportgrid() {
            Gv_ProjectBudgetList.exportToExcel();
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

                Project Budget List

            </h3>

        </div>
        <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="Gv_ProjectBudgetList" AutoGenerateColumns="false"  AllowGrouping="true" CallbackMode="false"  AllowRecordSelection="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">         
            <ScrollingSettings ScrollWidth="100%"  />
            
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
			
           <ExportingSettings ExportAllPages="true"  ExportTemplates="true" ExportedFilesTargetWindow="New" ColumnsToExport="Project_Name,Date,Name,Status"/>
            <Columns>
               <ogrid:Column DataField="Proj_Bud_ID" HeaderText="Edit" Wrap="true">
            <TemplateSettings  TemplateId="ProjectBudgetIDTemplate"/>
        </ogrid:Column> 
                   <ogrid:Column DataField="Project_Name" HeaderText="Project Name" Width="200"  Wrap="true" ></ogrid:Column>        
                 <ogrid:Column DataField="Date" HeaderText="Created Date"  Wrap="true"></ogrid:Column>   
                 <ogrid:Column DataField="Name" HeaderText="Created By" Wrap="true"   ></ogrid:Column>
                 <ogrid:Column DataField="Status" HeaderText="Status" Wrap="true" ></ogrid:Column>
                 
                        
            </Columns>
             <Templates>
        <ogrid:GridTemplate ID="ProjectBudgetIDTemplate" runat="server">
            <Template>
                 <a href='ProjectBudgetCreate.aspx?ID=<%#Container.DataItem["Proj_Bud_ID"] %>'>Edit</a>
               
               
            </Template>
        </ogrid:GridTemplate>

    </Templates>

        </ogrid:Grid>
                      <br />
                       <asp:LinkButton ID="lnkbtnAdd" Text="Add New Project Budget" PostBackUrl="~/Budget/ProjectBudgetCreate.aspx" CssClass="btn btn-default"  runat="server"></asp:LinkButton>
                    
                    
                <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />         
                      </center>
        </div>
    </div>
</asp:Content>
