<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="AgeWiseReport.aspx.cs" Inherits="AgeWiseReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function exportgrid() {
            Grid_AgeWise.exportToExcel();
        }
        function exportgrid1() {
            Grid_ItemDetails.exportToExcel();
        }
        
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Age Wise Report 

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-2">Project Name</div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlProjectName" CssClass="form-control" runat="server" TabIndex="1">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>

                 <div class="col-md-2">Date
                    &nbsp;

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtStartDate" ValidationGroup="ValAgewise" CssClass="Validation_Text"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtStartDate" CssClass="form-control" onkeypress="javascript: return false;" onPaste="javascript: return false;" runat="server" TabIndex="1"></asp:TextBox>

                    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtStartDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>

                </div>
               <%-- <div class="col-md-2">Year &nbsp;
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" InitialValue="-Select-" ControlToValidate="ddlYear" ValidationGroup="ValAgewise" CssClass="Validation_Text"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlYear" CssClass="form-control" runat="server" TabIndex="2">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>--%>

            </div>
            <div class="row">
               

            </div>

            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" ValidationGroup="ValAgewise" CssClass="btn btn-default" TabIndex="4"></asp:Button>
                </div>

            </div>
            <br />
            <div class="row">

                <center>



        <ogrid:Grid runat="server" ID="Grid_AgeWise" ShowColumnsFooter="true"  OnRowCreated="Grid_AgeWise_RowCreated" OnRowDataBound="Grid_AgeWise_RowDataBound" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" CallbackMode="true" AllowPaging="true" PageSize="10" >
         <ScrollingSettings ScrollWidth="100%" />
             <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
               <ExportingSettings ExportAllPages="true" ExportTemplates="true"   />
            <Columns>   
                       
             <ogrid:Column DataField="Budget_Sector" HeaderText="Sector" ></ogrid:Column>

                 <ogrid:Column DataField="StockBelow30days" HeaderText="(<30 Days)"  >
                    <TemplateSettings TemplateId="StockBelow30daysTemplate" />
                </ogrid:Column>
             
                 <ogrid:Column DataField="StockM30To60days" HeaderText="30 To 60 Days"  >
                    <TemplateSettings TemplateId="StockM30To60daysTemplate" />
                </ogrid:Column>

                <ogrid:Column DataField="StockM60To90days" HeaderText="60 To 90 Days"  >
                    <TemplateSettings TemplateId="StockM60To90daysTemplate" />
                </ogrid:Column>

                 <ogrid:Column DataField="StockM90To120days" HeaderText="90 To 120 Days"  >
                    <TemplateSettings TemplateId="StockM90To120daysTemplate" />
                </ogrid:Column>

                  <ogrid:Column DataField="StockM120To150days" HeaderText="120 To 150 Days"  >
                    <TemplateSettings TemplateId="StockM120To150daysTemplate" />
                </ogrid:Column>

                <ogrid:Column DataField="StockM150To180days" HeaderText="150 To 180 Days"  >
                    <TemplateSettings TemplateId="StockM150To180daysTemplate" />
                </ogrid:Column>

                <ogrid:Column DataField="StockM180days" HeaderText="Dead Stock ( > 180 Days)"  >
                    <TemplateSettings TemplateId="StockM180daysTemplate" />
                </ogrid:Column>

                 <ogrid:Column DataField="Total"  HeaderText="Total Amt"></ogrid:Column>
                           
            </Columns>
            <Templates>
                <ogrid:GridTemplate ID="StockBelow30daysTemplate" runat="server">
                    <Template>                   
                        <asp:LinkButton ID="lnkbtnItemCode" Text='<%# Container.DataItem["StockBelow30days"] %>' CommandArgument='<%# Container.DataItem["Budget_Sector"] %>'
                            CommandName ="<= 30" OnClick="lnkbtnItemCode_Click" runat="server"></asp:LinkButton>
                    </Template>
                </ogrid:GridTemplate>
            </Templates>
             <Templates>
                <ogrid:GridTemplate ID="StockM30To60daysTemplate" runat="server">
                    <Template>                   
                        <asp:LinkButton ID="lnkbtnItemCode" Text='<%# Container.DataItem["StockM30To60days"] %>' CommandArgument='<%# Container.DataItem["Budget_Sector"] %>'
                            CommandName ="30To60" OnClick="lnkbtnItemCode_Click" runat="server"></asp:LinkButton>
                    </Template>
                </ogrid:GridTemplate>
            </Templates>
             <Templates>
                <ogrid:GridTemplate ID="StockM60To90daysTemplate" runat="server">
                    <Template>                   
                        <asp:LinkButton ID="lnkbtnItemCode" Text='<%# Container.DataItem["StockM60To90days"] %>' CommandArgument='<%# Container.DataItem["Budget_Sector"] %>'
                            CommandName ="60To90" OnClick="lnkbtnItemCode_Click" runat="server"></asp:LinkButton>
                    </Template>
                </ogrid:GridTemplate>
            </Templates>
             <Templates>
                <ogrid:GridTemplate ID="StockM90To120daysTemplate" runat="server">
                    <Template>                   
                        <asp:LinkButton ID="lnkbtnItemCode" Text='<%# Container.DataItem["StockM90To120days"] %>' CommandArgument='<%# Container.DataItem["Budget_Sector"] %>'
                            CommandName ="90To120" OnClick="lnkbtnItemCode_Click" runat="server"></asp:LinkButton>
                    </Template>
                </ogrid:GridTemplate>
            </Templates>
             <Templates>
                <ogrid:GridTemplate ID="StockM120To150daysTemplate" runat="server">
                    <Template>                   
                        <asp:LinkButton ID="lnkbtnItemCode" Text='<%# Container.DataItem["StockM120To150days"] %>' CommandArgument='<%# Container.DataItem["Budget_Sector"] %>'
                            CommandName ="120To150" OnClick="lnkbtnItemCode_Click" runat="server"></asp:LinkButton>
                    </Template>
                </ogrid:GridTemplate>
            </Templates>
             <Templates>
                <ogrid:GridTemplate ID="StockM150To180daysTemplate" runat="server">
                    <Template>                   
                        <asp:LinkButton ID="lnkbtnItemCode" Text='<%# Container.DataItem["StockM150To180days"] %>' CommandArgument='<%# Container.DataItem["Budget_Sector"] %>'
                            CommandName ="150To180" OnClick="lnkbtnItemCode_Click" runat="server"></asp:LinkButton>
                    </Template>
                </ogrid:GridTemplate>
            </Templates>
             <Templates>
                <ogrid:GridTemplate ID="StockM180daysTemplate" runat="server">
                    <Template>                   
                        <asp:LinkButton ID="lnkbtnItemCode" Text='<%# Container.DataItem["StockM180days"] %>' CommandArgument='<%# Container.DataItem["Budget_Sector"] %>'
                            CommandName =">180" OnClick="lnkbtnItemCode_Click" runat="server"></asp:LinkButton>
                    </Template>
                </ogrid:GridTemplate>
            </Templates>
           
        </ogrid:Grid>
                      <br />
                         <asp:Button ID="btnExportToPDF" runat="server" OnClick="btnExportToPDF_Click" Text="Export To PDF" CssClass="btn btn-default" TabIndex="5"></asp:Button>
                     <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" tabindex="6" />  
                       <br /> <br />

                            <ogrid:Grid runat="server" ID="Grid_ItemDetails" ShowColumnsFooter="true" ShowFooter="true" OnRowCreated="Grid_ItemDetails_RowCreated" OnRowDataBound="Grid_ItemDetails_RowDataBound" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings ScrollWidth="100%" />
                                 <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
               <ExportingSettings ExportAllPages="true" ExportTemplates="true"  />
            <Columns>
               <%-- <ogrid:Column DataField="MRN_No" HeaderText="MRN No" ></ogrid:Column>
                <ogrid:Column DataField="Date" HeaderText="MRN Date" ></ogrid:Column>--%>
                <ogrid:Column DataField="Item_Code" HeaderText="Item code" ></ogrid:Column>
                <ogrid:Column DataField="Item_Name" HeaderText="Item Name" ></ogrid:Column>
                <ogrid:Column DataField="Category_Name" HeaderText="Category" ></ogrid:Column>
              <%--  <ogrid:Column DataField="Vendor_name" HeaderText="Vendor  Name" ></ogrid:Column>--%>
                <ogrid:Column DataField="UOM" HeaderText="UOM" ></ogrid:Column>
                <ogrid:Column DataField="Qty" HeaderText="Qty" ></ogrid:Column>
                <ogrid:Column DataField="Rate" HeaderText="Rate" ></ogrid:Column>
                <ogrid:Column DataField="Amount" HeaderText="Amount" ></ogrid:Column>
                
            </Columns>
        </ogrid:Grid>

                       <br />
                    
                       <input onclick="exportgrid1()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" tabindex="6" /> 
                      </center>
            </div>
            <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        </div>

    </div>

</asp:Content>

