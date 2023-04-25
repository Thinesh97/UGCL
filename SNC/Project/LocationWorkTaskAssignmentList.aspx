<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true"  CodeBehind="LocationWorkTaskAssignmentList.aspx.cs" Inherits="SNC.Project.TaskList" %>

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
        <div class="panel-heading" style="width:600px">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

               locations / Works / Sub Works   

            </h3>

        </div>
        <div class="panel-body">
            <center>
                <ogrid:Grid runat="server" ID="Grid_LWT" CallbackMode="false" AutoGenerateColumns="false"   OnRowDataBound="Grid_LWTRowDataBound" 
                    FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="10" >
                    <ScrollingSettings ScrollWidth="100%"  ScrollHeight="400"/>
                      <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"
                         ColumnsToExport="WONo,WODate,Subcon_name,Type_Name,DurationOfWork,Status,Name,Prepared_By,Other_Terms,Remarks" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

                    <Columns>
                        
                        <ogrid:Column DataField="ID" HeaderText="ID" Width="100px" >
                             <TemplateSettings  TemplateId="Work_LocationTemplate"/>
                        </ogrid:Column>
                        <ogrid:Column DataField="Work_Location" HeaderText="Location" Width="300px" ></ogrid:Column>
                          <ogrid:Column DataField="Work_Name" HeaderText="Work" Width="300px" ></ogrid:Column>
                          <ogrid:Column DataField="SubWork_Name" HeaderText="Sub Works" Width="200px" ></ogrid:Column>
                        
                    </Columns>
                    <Templates>
                        <ogrid:GridTemplate ID="Work_LocationTemplate" runat="server">
                            <Template>
                                <asp:HyperLink ID="lnkLWT" runat="server" CssClass="gridCB"  CommandArgument='<%#Container.DataItem["ID"] %>' CommandName='<%#Container.DataItem["ID"] %>' Text='<%#Container.DataItem["ID"] %>'></asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>

                    </Templates>
                </ogrid:Grid>
                <br />
                <center>
                   
                     <a href="#Modal_Sitelocation" data-toggle="modal" role="button">
                            <asp:LinkButton ID="lnkbtnAdd" Text="Add New Entry" PostBackUrl="~/Project/LocationWorkTaskAssignment.aspx" CssClass="btn btn-default"  runat="server"></asp:LinkButton>
                    </a>
                    <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />  
                </center>
                      </center>
        </div>
    </div>

    
</asp:Content>
