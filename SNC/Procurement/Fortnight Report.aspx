<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="Fortnight Report.aspx.cs" Inherits="FortnightReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function exportgrid() {
            Grid_FortNight.exportToExcel();
        }

        function exportBudgetGrid() {
            Grid_BudgetSectorDetails.exportToExcel();
        }

        function exportDateBasedgrid() {
            Gv_DateBased.exportToExcel();
        }

        function exportDateBasedSectorGrid() {
            Gv_DateBasedBudgetSectorDeatils.exportToExcel();
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Fortnight Report  

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-2">Search By</div>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="Rbl_SearchBy" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="Rbl_SearchBy_SelectedIndexChanged" runat="server">
                        <asp:ListItem Text="Fort Night" Value="FortNightBased" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Date Range" Value="DateBased"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                 <div class="col-md-2">Project Name</div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlProjectName" CssClass="form-control" runat="server" TabIndex="1">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row" id="trFortNight" runat="server">
               
               
                    <div class="col-md-2">Year</div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlYear" CssClass="form-control" runat="server" TabIndex="2">
                            <asp:ListItem>-Select-</asp:ListItem>
                        </asp:DropDownList>
                    </div>
               
                 
                    <div class="col-md-2">Month</div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlMonth" CssClass="form-control" runat="server" TabIndex="3">
                            <asp:ListItem>-Select-</asp:ListItem>
                        </asp:DropDownList>

                    </div>
               
              
            </div>
            <div class="row" id="trDateRange" runat="server" visible="false">
             
                    <div class="col-md-2">Start Date&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtStartDate" ValidationGroup="ValFortNight" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtStartDate" CssClass="form-control" onkeypress="javascript: return false;" onPaste="javascript: return false;" runat="server"></asp:TextBox>

                        <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtStartDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                    </div>
                
               
                    <div class="col-md-2">End Date &nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEndDate" ValidationGroup="ValFortNight" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtEndDate" CssClass="form-control" onkeypress="javascript: return false;" onPaste="javascript: return false;" runat="server"></asp:TextBox>

                        <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtEndDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                    </div>
               
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" ValidationGroup="ValFortNight" CssClass="btn btn-default" TabIndex="4"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" CssClass="btn btn-default" TabIndex="5"></asp:Button>
                </div>

            </div>

            <br />
            <br />
            <asp:Panel ID="Pnl_FortNightBased" runat="server">
                <center>
                       <asp:TextBox ID="txtTotalAmt" runat="server" Text="0" Style="display: none" />
        <ogrid:Grid runat="server" ID="Grid_FortNight" OnRowCreated="Grid_FortNight_RowCreated" OnRowDataBound="Grid_FortNight_RowDataBound" ShowColumnsFooter="true" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" CallbackMode="true" AllowPaging="true" PageSize="10" >
            <ScrollingSettings ScrollWidth="100%" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
               <ExportingSettings ExportAllPages="true" ExportTemplates="true"  ExportedFilesTargetWindow="New"  />

            <Columns>             
                <ogrid:Column DataField="Serial_No" HeaderText="SL.No" Width="100" ></ogrid:Column>
                <ogrid:Column DataField="Budget_Sector" HeaderText="Sector" >
                    <TemplateSettings TemplateId="BudgetSectorTemplate" />
                </ogrid:Column>            
                <ogrid:Column DataField="LocalAmt" HeaderText="1st FortNight Local Amount" Width="200px"  Align="Right"></ogrid:Column>
                <ogrid:Column DataField="HOAmt" HeaderText="1st FortNight HO Amount" Width="200px"  Align="Right"></ogrid:Column>
                <ogrid:Column DataField="TotalAmt" HeaderText="1st FortNight Total Amount" Width="200px"  Align="Right"></ogrid:Column>
              <ogrid:Column DataField="LocalAmt1" HeaderText="2nd FortNight Local Amount" Width="200px"  Align="Right"></ogrid:Column>
                <ogrid:Column DataField="HOAmt1" HeaderText="2nd FortNight HO Amount" Width="200px"  Align="Right"></ogrid:Column>
                <ogrid:Column DataField="TotalAmt1" HeaderText="2nd  FortNight Total Amount" Width="200px"  Align="Right"></ogrid:Column>    
                  <ogrid:Column DataField="GrandTotal" HeaderText="Grand Total" Width="150px"  Align="Right"></ogrid:Column>                 
            </Columns>
               <Templates>
                <ogrid:GridTemplate ID="BudgetSectorTemplate" runat="server">
                    <Template>
                        <asp:LinkButton ID="lnkbtnBudgetSector" Text='<%# Container.DataItem["Budget_Sector"] %>' OnClick="lnkbtnBudgetSector_Click" runat="server"></asp:LinkButton>
                    </Template>
                </ogrid:GridTemplate>
            </Templates>
        </ogrid:Grid>

                   <br />
                       <br />
                     <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click" TabIndex="5"></asp:Button>
                   <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" tabindex="6" /> 
                     <asp:Button ID="btn_GetALLBreakupFort" runat="server" Text="Get All sectors Break up" CssClass="btn btn-default" OnClick="GetALLBreakupSector_Click"   TabIndex="5"></asp:Button>    
                       
                       
                           <br /> <br />

                            <ogrid:Grid runat="server" ID="Grid_BudgetSectorDetails" ShowColumnsFooter="true"  OnRowCreated="Grid_BudgetSectorDetails_RowCreated" OnRowDataBound="Grid_BudgetSectorDetails_RowDataBound" ShowFooter="true" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings ScrollWidth="100%" />
             <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
               <ExportingSettings ExportAllPages="true" ExportTemplates="true"  ExportedFilesTargetWindow="New"  />
            <Columns>
                <ogrid:Column DataField="Budget_Sector" HeaderText="Sector" ></ogrid:Column>
                  <ogrid:Column DataField="PODate" HeaderText="PO Date"  Width="100px"></ogrid:Column>
                  <ogrid:Column DataField="Po_No" HeaderText="Po_No"  Width="180px"></ogrid:Column>
                <ogrid:Column DataField="Date" HeaderText="MRN Date"  Width="100px"></ogrid:Column>
                <ogrid:Column DataField="MRN_No" HeaderText="MRN No" ></ogrid:Column>
                <ogrid:Column DataField="Vendor_name" HeaderText="Vendor Name" ></ogrid:Column>             
                <ogrid:Column DataField="Item_Code" HeaderText="Item code" ></ogrid:Column>
                <ogrid:Column DataField="Item_Name" HeaderText="Item Name" ></ogrid:Column>
                <ogrid:Column DataField="UOMPrefix" HeaderText="Unit" ></ogrid:Column>
                <ogrid:Column DataField="Accepted_Qty" HeaderText="Quantity" ></ogrid:Column>
                <ogrid:Column DataField="RATE" HeaderText="Rate" Align="Right" Width="100px" ></ogrid:Column>
                <ogrid:Column DataField="Amount" HeaderText="Amount" Align="Right" Width="100px" ></ogrid:Column>
                 <ogrid:Column DataField="Tax" HeaderText="Tax/Transport Cost/Discount" Align="Right"  Width="180px" ></ogrid:Column>
                <ogrid:Column DataField="Invoice_No" HeaderText="Bill No" ></ogrid:Column>
                <ogrid:Column DataField="Bill_Date" HeaderText="Bill Date"  Width="100px"></ogrid:Column>
                <ogrid:Column DataField="LocalAmt" HeaderText="1st FortNight Local Amount " Width="200px"  Align="Right" ></ogrid:Column>
                <ogrid:Column DataField="HOAmt" HeaderText="1st FortNight HO Amount " Width="200px"  Align="Right" ></ogrid:Column>
                 <ogrid:Column DataField="TotalAmt" HeaderText="1st FortNight Total Amount" Width="200px"  Align="Right"></ogrid:Column>
                  <ogrid:Column DataField="LocalAmt1" HeaderText="2nd FortNight Local Amount " Width="200px"  Align="Right"></ogrid:Column>
                <ogrid:Column DataField="HOAmt1" HeaderText="2nd FortNight HO Amount " Width="200px"  Align="Right"></ogrid:Column>
                 <ogrid:Column DataField="TotalAmt1" HeaderText="2nd FortNight Total Amount" Width="200px"  Align="Right"></ogrid:Column>
            </Columns>
        </ogrid:Grid>
              
                      
                           <br />
                       <br />
                     <asp:Button ID="btnExportToPDFBreakUp" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDFBreakUp_Click"  TabIndex="7"></asp:Button>
                   <input onclick="exportBudgetGrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" tabindex="8" />     
                                
                      </center>
            </asp:Panel>
            <br />
            <asp:Panel ID="Pnl_DateBased" runat="server" Visible="false">
                  <center>
                <ogrid:Grid runat="server" ID="Gv_DateBased" OnRowCreated="Gv_DateBased_RowCreated" OnRowDataBound="Gv_DateBased_RowDataBound" ShowColumnsFooter="true" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" CallbackMode="true" AllowPaging="true" PageSize="10">
                    <ScrollingSettings ScrollWidth="90%" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;" CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;" />
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />

                    <Columns>
                        <ogrid:Column DataField="Serial_No" HeaderText="SL.No" Width="100"></ogrid:Column>
                        <ogrid:Column DataField="Budget_Sector" HeaderText="Sector">
                            <TemplateSettings TemplateId="DateBasedBudgetSectorTemplate" />
                        </ogrid:Column>                       
                        <ogrid:Column DataField="LocalAmt" HeaderText="Local Amount" Width="200" Align="Right"></ogrid:Column>
                        <ogrid:Column DataField="HOAmt" HeaderText="HO Amount" Width="200"  Align="Right"></ogrid:Column>
                        <ogrid:Column DataField="TotalAmt" HeaderText="Total Amount" Width="200" Align="Right"></ogrid:Column>

                    </Columns>
                    <Templates>
                        <ogrid:GridTemplate ID="DateBasedBudgetSectorTemplate" runat="server">
                            <Template>
                                <asp:LinkButton ID="lnkbtnDateBasedBudgetSector" Text='<%# Container.DataItem["Budget_Sector"] %>' OnClick="lnkbtnDateBasedBudgetSector_Click" runat="server"></asp:LinkButton>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
                </ogrid:Grid>
                   <br />
                       <br />
                       <asp:Button ID="Btn_ExportToPDF_DateBased" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="Btn_ExportToPDF_DateBased_Click" ></asp:Button>
                   <input onclick="exportDateBasedgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" tabindex="6" />     
                       
                       
                           <br /> <br />
                    <ogrid:Grid runat="server" ID="Gv_DateBasedBudgetSectorDeatils" ShowColumnsFooter="true"  OnRowCreated="Gv_DateBasedBudgetSectorDeatils_RowCreated" OnRowDataBound="Gv_DateBasedBudgetSectorDeatils_RowDataBound" ShowFooter="true" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings ScrollWidth="100%" />
             <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
               <ExportingSettings ExportAllPages="true" ExportTemplates="true"  ExportedFilesTargetWindow="New"  />
            <Columns>
                <ogrid:Column DataField="Budget_Sector" HeaderText="Sector" ></ogrid:Column>
                <ogrid:Column DataField="Date" HeaderText="Date"  Width="100px"></ogrid:Column>
                <ogrid:Column DataField="MRN_No" HeaderText="MRN No." ></ogrid:Column>
                <ogrid:Column DataField="Vendor_name" HeaderText="Vendor Name" ></ogrid:Column>             
                <ogrid:Column DataField="Item_Code" HeaderText="Item code" ></ogrid:Column>
                <ogrid:Column DataField="Item_Name" HeaderText="Item Name" ></ogrid:Column>
                <ogrid:Column DataField="UOMPrefix" HeaderText="Unit" ></ogrid:Column>
                <ogrid:Column DataField="Accepted_Qty" HeaderText="Quantity" ></ogrid:Column>
                <ogrid:Column DataField="RATE" HeaderText="Rate" Align="Right" ></ogrid:Column>
                <ogrid:Column DataField="Amount" HeaderText="Amount" Align="Right" ></ogrid:Column>
                <ogrid:Column DataField="Invoice_No" HeaderText="Bill No" ></ogrid:Column>
                <ogrid:Column DataField="Bill_Date" HeaderText="Bill Date"  Width="100px"></ogrid:Column>
                <ogrid:Column DataField="LocalAmt" HeaderText="Local Amount " Width="200px"  Align="Right" ></ogrid:Column>
                <ogrid:Column DataField="HOAmt" HeaderText="HO Amount " Width="200px"  Align="Right" ></ogrid:Column>
                 <ogrid:Column DataField="TotalAmt" HeaderText="Total Amount" Width="200px"  Align="Right"></ogrid:Column>
                 
            </Columns>
        </ogrid:Grid>
                       <br />
                       <br />
                   <asp:Button ID="Btn_ExportToPDF_DateBasedBreakUp" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="Btn_ExportToPDF_DateBasedBreakUp_Click" ></asp:Button>
                   <input onclick="exportDateBasedSectorGrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel"  />  
                        </center>
            </asp:Panel>

            <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        </div>

    </div>
    <style>
        .panel-heading {
            background: #0087dc !important;
        }
    </style>
</asp:Content>
