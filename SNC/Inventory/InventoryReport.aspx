<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/SNC.Master" CodeBehind="InventoryReport.aspx.cs" Inherits="InventoryReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script type="text/javascript">
         function exportgrid() {
             GV_Details.exportToExcel();
         }
         </script>

      <script type="text/javascript">
          function exportgrid1() {
              GV_Sector.exportToExcel();
          }
         </script>

   



    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>



    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Inventory  Report 

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-2">Project Name
                    &nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="-Select-" ControlToValidate="ddlProjectName" CssClass="Validation_Text" ValidationGroup="ValSearch" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlProjectName" CssClass="form-control" runat="server">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>

                </div>
                <div class="col-md-2">
                    Month&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="-Select-" ControlToValidate="ddlMonth" CssClass="Validation_Text" ValidationGroup="ValSearch" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlMonth" CssClass="form-control" runat="server">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>



            </div>
            <div class="row">
                <div class="col-md-2">
                    Year &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="-Select-" runat="server" ControlToValidate="ddlYear" CssClass="Validation_Text" ValidationGroup="ValSearch" ErrorMessage="*"></asp:RequiredFieldValidator>
                
                </div>
                <div class="col-md-4">

                    <asp:DropDownList ID="ddlYear" CssClass="form-control" runat="server" >
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>


                </div>

            </div>

            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="ValSearch" CssClass="btn btn-default" OnClick="btnSearch_Click"></asp:Button>
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default"  CausesValidation="false" OnClick="btnCancel_Click"/>


                </div>

            </div>
            <br />
         <center>  <b> <asp:Label ID="ttlamount" Text="Total" Visible="false" runat="server"></asp:Label></b></center>
            <div>
                <!-- first grid     !-->
                <center>
                     <ogrid:Grid runat="server" ID="GV_Sector" AutoGenerateColumns="false" ShowColumnsFooter="true"  OnRowCreated="GV_Sector_RowCreated" OnRowDataBound="GV_Sector_RowDataBound"  FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
         
                         <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;" CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;" />
                        <ScrollingSettings ScrollWidth="52%" />
                         <ExportingSettings  ExportAllPages="true" ExportTemplates="true" ExportHiddenColumns="false" ExportedFilesTargetWindow="New"  ExportColumnsFooter="true"/>
            
                         <Columns>
                <ogrid:Column DataField="Sector" HeaderText="Sector" >
                    <TemplateSettings  TemplateId="SectorTemplate"/>
                </ogrid:Column>
                <ogrid:Column DataField="ID" Visible="false" HeaderText="ID"></ogrid:Column>
                <ogrid:Column DataField="Amount" HeaderText="Amount in Rs" ></ogrid:Column>
                
            </Columns>
                          <Templates>
                          <ogrid:GridTemplate ID="SectorTemplate" runat="server">
            <Template>
                <asp:LinkButton runat="server" ID="Sector"  Text='<%#Container.DataItem["Sector"] %>'  CommandArgument='<%#Container.DataItem["ID"] %>' OnClick="Sector_Click"></asp:LinkButton>

            </Template>
            </ogrid:GridTemplate>
                              </Templates>
        </ogrid:Grid>
                </center>
            </div>
            <br />
               <div class="row">
                 <center>
 <asp:Button ID="Button1" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click1"></asp:Button>
                     <input onclick="exportgrid1()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />    

                 </center>
                 </div>
            <br />
            <br />

            <div>
                <center>
                    <ogrid:Grid runat="server" ID="GV_Details" AutoGenerateColumns="false" ShowColumnsFooter="true" OnRowCreated="GV_Details_RowCreated" OnRowDataBound="GV_Details_RowDataBound"  FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                        
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;" CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;" />
                        <ScrollingSettings ScrollWidth="100%" />
                         <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
                        <Columns>
                            <ogrid:Column   HeaderText="Sl No." Width="80" runat="server">
                    <TemplateSettings TemplateId="tplNumbering"/>
                </ogrid:Column>

                                 
                                   <ogrid:Column DataField="Item_Name" HeaderText="Item Name" ></ogrid:Column>
                                  <ogrid:Column DataField="Item_Code" HeaderText="Item Code" ></ogrid:Column>
                                  
                                  <ogrid:Column DataField="UOM" HeaderText="UOM" ></ogrid:Column>
                                  <ogrid:Column DataField="OpeningStock" HeaderText="Opening Stock Qty" ></ogrid:Column>
                                  <ogrid:Column DataField="MRNPOQty" HeaderText="MRN PO Qty" ></ogrid:Column>
                                    <ogrid:Column DataField="MRNFromProject" HeaderText="MRN From  Project " ></ogrid:Column>
                                    <ogrid:Column DataField="MRNOtherPOQty" HeaderText="MRN From  Project Qty" ></ogrid:Column>
                                    <ogrid:Column DataField="TotalAvailableQty" HeaderText="Total Available for Issue" ></ogrid:Column>
                                    <ogrid:Column DataField="IssueQty" HeaderText="Issued Qty" ></ogrid:Column>
                                    <ogrid:Column DataField="ClosingStockInQty" HeaderText="Closing Stock in Qty" ></ogrid:Column>
                                    <ogrid:Column DataField="Rate" HeaderText="Rate" ></ogrid:Column>
                                    <ogrid:Column DataField="Amount" HeaderText="Amount" ></ogrid:Column>
                            <%-- <ogrid:Column DataField="Remarks" HeaderText="Remarks" ></ogrid:Column>--%>

                        </Columns>
                         <Templates>
                <ogrid:GridTemplate runat="server" ID="tplNumbering">
                    <Template>
                        <asp:Label runat="server" Text='<%# Container.RecordIndex + 1 %>' ID="lblno"></asp:Label>
                       <%-- <b><%# Container.RecordIndex + 1 %>.</b>--%>
                    </Template>
                </ogrid:GridTemplate>
            </Templates>    
                        
                       
                        </ogrid:Grid>
                </center>
            </div>
             <div class="row">
                 <center>
 <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click"></asp:Button>
                     <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />    

                 </center>
                 </div>
            <br />


        </div>

    </div>

</asp:Content>
