<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true"  CodeBehind="NominalMasterRollList.aspx.cs" Inherits="SNC.SubContractorBills.NominalMasterRollList" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function ConfirmDelete() {
            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                return false;
            }
            return true;
        }
        function exportgrid() {
            Grid_NMR.exportToExcel();
        }
    </script>

    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

               Nominal Master Roll List

            </h3>

        </div>
     
                  <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="Grid_NMR"   CallbackMode="false" AutoGenerateColumns="false"   FolderStyle="../Gridstyles/grand_gray" OnRowDataBound="Grid_NMR_RowDataBound" OnDeleteCommand="Grid_NMR_Delete_Click" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true"   PageSize="10">
           
              <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
            <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />

            <Columns>
                   <ogrid:Column DataField="NMR_No" HeaderText="NMR No" runat="server" >
            <TemplateSettings  TemplateId="NMR_NoTemplate"/>
        </ogrid:Column>      
              
                <ogrid:Column DataField="BillDate_NMR" HeaderText="Date" Width="200px" ReadOnly="true" ></ogrid:Column>
                <ogrid:Column DataField="Subcon_name" HeaderText="Sub Contractor" Width="300px" ReadOnly="true" ></ogrid:Column>
                 <ogrid:Column DataField="WorkDescription_NMR" HeaderText="Work Description" Width="300px" ReadOnly="true" ></ogrid:Column>
                 <ogrid:Column DataField="WorkDone_At" HeaderText="Work Done At" Width="300px" ReadOnly="true" ></ogrid:Column>
                  <ogrid:Column DataField="ID" HeaderText="ID" Visible="false" Width="100" ></ogrid:Column>
                  <ogrid:Column  HeaderText="Delete" AllowDelete="true" Width="100" ></ogrid:Column>
            </Columns>
            
               <Templates>
                        <ogrid:GridTemplate ID="NMR_NoTemplate" runat="server">
                            <Template>
                               <asp:HyperLink ID="lnk_NMR_No" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["NMR_No"] %>'>
                        </asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
    </Templates>
        </ogrid:Grid>
                <br />
                 <a href="NominalMasterRoll.aspx" runat="server" id="lnkbtnAdd" class="btn btn-default">Add New NMR</a> 
                 <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />   
                  </center>      
                 <br />
               
                    
        </div>
                 </div>

        </div>
    </div>
</asp:Content>

