<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true"  CodeBehind="RateCardList.aspx.cs" Inherits="SNC.SubContractorBills.RateCradList" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function exportgrid() {
            Grid_RC_List.exportToExcel();
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

                Rate Card List 

            </h3>

        </div>
        <div class="panel-body">
            <center>
                <ogrid:Grid runat="server" ID="Grid_RC_List" CallbackMode="false" AutoGenerateColumns="false" 
                    OnRowDataBound="Grid_RC_RowDataBound" 
                    FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="10" >
                    <ScrollingSettings ScrollWidth="100%"  ScrollHeight="400"/>
                      <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"
                         ColumnsToExport="WONo,WODate,Subcon_name,Type_Name,DurationOfWork,Status,Name,Prepared_By,Other_Terms,Remarks" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

                    <Columns>
                        <ogrid:Column DataField="RateCard_ID" HeaderText="Rate Card No" Width="190px" >
                             <TemplateSettings  TemplateId="WONoTemplate"/>
                        </ogrid:Column>
                        <ogrid:Column DataField="Project_Name" HeaderText="Project Name" Wrap="false"></ogrid:Column>
                        <ogrid:Column DataField="Subcon_name" HeaderText="Sub Contractor Name" Width="180px" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Work_OrderNO" HeaderText="Work Order No" Wrap="true"   Width="200px"></ogrid:Column>
                        <ogrid:Column DataField="WO_Description" HeaderText="Work Description" Align="center" Width="150px"></ogrid:Column>                 
                        <ogrid:Column DataField="Date" HeaderText="Rate Card Date" Width="100px" ></ogrid:Column>
                    </Columns>
                    <Templates>
                        <ogrid:GridTemplate ID="WONoTemplate" runat="server">
                            <Template>
                               <asp:HyperLink ID="lnkDPR_No" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["RateCard_ID"] %>'>
                        </asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
                </ogrid:Grid>
                <br />
                <center>
                    <a href="RateCard.aspx" runat="server" id="lnkbtnAdd" class="btn btn-default">Add New Rate Card</a> 
                    <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />  
                </center>
                      </center>
        </div>
    </div>
</asp:Content>
