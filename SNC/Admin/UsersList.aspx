<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Admin/UsersList.aspx.cs" Inherits="UsersList" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function exportgrid() {
            Grid_User.exportToExcel();
        }
    </script>

    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>
                List Of Users
            </h3>

        </div>
        <div class="panel-body">
            <center>
         <ogrid:Grid runat="server" ID="Grid_User" OnRowDataBound="Grid_User_RowDataBound"   AutoGenerateColumns="false" AllowGrouping="false" FolderStyle="../Gridstyles/grand_gray"   AllowMultiRecordSelection="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10" >
           
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <ExportingSettings  ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
            <ScrollingSettings ScrollWidth="100%" />
            <Columns>             
                <ogrid:column HeaderText="Employee ID" DataField="Employee_ID"  Width="140px" >
                    <TemplateSettings  TemplateId="UserIDTemplate"/>
                </ogrid:column>
                <ogrid:Column DataField="Name" HeaderText="Name" ></ogrid:Column>
                
                <ogrid:Column DataField="Email_ID" HeaderText="Email" ></ogrid:Column>
                <ogrid:Column DataField="Designation" HeaderText="Designation"></ogrid:Column>
                <ogrid:Column DataField="Department" HeaderText="Department"></ogrid:Column>
                <ogrid:Column DataField="Project_Code" HeaderText="Assigned Project" Wrap="true"></ogrid:Column>
                <ogrid:Column DataField="Role" HeaderText="Role" ></ogrid:Column>
                <ogrid:Column DataField="Status" HeaderText="Status" Width="100px" ></ogrid:Column>
                <ogrid:Column DataField="IsHoUser" HeaderText="Is Ho User" Width="97px"></ogrid:Column>
                <ogrid:column HeaderText="Is Ho User" DataField="IsHoUser"  >
                    <TemplateSettings  TemplateId="HOuserTemplate"/>
                </ogrid:column>
            </Columns>  
            <Templates>
                <ogrid:GridTemplate ID="HOuserTemplate" runat="server">
                    <Template>
                        <asp:Label runat="server" ID="lblHoUser" Text='<%#Container.DataItem["IsHoUser"].ToString() == "True" ? "Yes" : "No" %>'></asp:Label>
                    </Template>
                </ogrid:GridTemplate>

            </Templates>
            <Templates>
                <ogrid:GridTemplate ID="UserIDTemplate" runat="server">
                    <Template>
                        <asp:LinkButton ID="lnkEmployeeID" runat="server"   CssClass="gridCB" OnClick="lnkEmployeeID_Click"  Text= '<%#Container.DataItem["Employee_ID"] %>' ></asp:LinkButton>
                    </Template>
                </ogrid:GridTemplate>
            </Templates>
            </ogrid:Grid>
                <br />
                <asp:LinkButton ID="lnkbtnAdd" Text="Add New User" PostBackUrl="~/Admin/Users.aspx" CssClass="btn btn-default"  runat="server"></asp:LinkButton>
                <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" OnClick="btnExportToPDF_Click" CssClass="btn btn-default"></asp:Button>
                <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />        
                      </center>
        </div>
    </div>
</asp:Content>
