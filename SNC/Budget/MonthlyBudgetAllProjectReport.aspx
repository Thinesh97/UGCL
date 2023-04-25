<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="MonthlyBudgetAllProjectReport.aspx.cs" Inherits="MonthlyBudgetAllProjectReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function exportgrid() {
            Grid_Search.exportToExcel();
        }
        function exportgridProject() {
            gridcompanywise.exportToExcel();
        }
        
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
   


            <div class="panel panel-default">
                <div class="panel-heading">

                    <h3 class="panel-title">
                        <i class="glyphicon glyphicon-envelope"></i>

                        Monthly Budget  Report 

                    </h3>

                </div>
                <div class="panel-body">
                    <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

                    <div class="row">
                        <div class="col-md-2">Project Name</div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlProjectName" CssClass="form-control" runat="server" TabIndex="1">
                                <asp:ListItem>-Select-</asp:ListItem>
                                 <asp:ListItem>-All-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">Year</div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlProjectYear" CssClass="form-control" runat="server" TabIndex="2">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-2">Month</div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlProjectMonth" CssClass="form-control" runat="server" TabIndex="3">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>

                        </div>

                    </div>

                    <br />
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Search" CssClass="btn btn-default" OnClick="btnSubmit_Click" TabIndex="4"></asp:Button>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btnCancel_Click" TabIndex="5"></asp:Button>


                        </div>

                    </div>
                    <br />
                    <asp:TextBox ID="txtTotal" runat="server" Style="display: none;"></asp:TextBox>
                    <div class="row">
                     
                        <center>
                              <asp:TextBox ID="txtTotalBudAmt" Text="0" runat="server" Style="display: none"></asp:TextBox>
        <ogrid:Grid runat="server" ID="Grid_Search" OnRowCreated="Grid_Search_RowCreated" OnRowDataBound="Grid_Search_RowDataBound" AllowGrouping="true"    ShowColumnsFooter="true"  AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" CallbackMode="false" AllowPaging="true" PageSize="10" >
   
           <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
               <ExportingSettings ExportAllPages="true" ExportTemplates="true"  ExportedFilesTargetWindow="New"  ExportColumnsFooter="true" />
            
            <Columns>
                  <ogrid:Column DataField="SINO" HeaderText="SI No"  Width="80px"  >
                      <TemplateSettings TemplateId="GetSlNo" />
                  </ogrid:Column> 
                   <ogrid:Column DataField="Section" HeaderText="Particulars" >
                    <TemplateSettings TemplateId="BudgetSectorTemplate" />
                </ogrid:Column> 
                <ogrid:Column DataField="BudAmt" HeaderText="Budget Amount" Align="Right" ></ogrid:Column>
                 <ogrid:Column DataField="AppAmout" HeaderText="Approved Budget Amount" Align="Right"  Wrap="true" Width="200px"  ></ogrid:Column>          
                  <ogrid:Column DataField="POAmount" HeaderText="PO Amount" Align="Right"  Wrap="true" Width="200px"  ></ogrid:Column> 
                    
                          
            </Columns>    
              <Templates>
                <ogrid:GridTemplate ID="BudgetSectorTemplate" runat="server">
                    <Template>
                        <asp:LinkButton ID="lnkbtnBudgetSector" Text='<%# Container.DataItem["Section"] %>' OnClick="lnkbtnBudgetSector_Click" runat="server"></asp:LinkButton>
                    </Template>
                </ogrid:GridTemplate>
            </Templates>     
            <Templates>
                <ogrid:GridTemplate runat="server" ID="GetSlNo">
                    <Template>
                      <b>  <%#(Container.RecordIndex + 1) %>.</b>
                    </Template>
                </ogrid:GridTemplate>
            </Templates>    
        </ogrid:Grid>
                      <br />
                      <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click" TabIndex="6"></asp:Button>
                       &nbsp;
                     <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" tabindex="7" />        
                      </center>
                    </div>
                    <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
                </div>
                <center>
                     <ogrid:Grid runat="server" ID="gridcompanywise"  AllowGrouping="true"
                             ShowColumnsFooter="true"  AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray"
                          AllowFiltering="true" AllowAddingRecords="false" CallbackMode="false" AllowPaging="true" PageSize="30" >
   
           <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
               <ExportingSettings ExportAllPages="true" ExportTemplates="true"  ExportedFilesTargetWindow="New"  ExportColumnsFooter="true" />
            
            <Columns>
                  <ogrid:Column DataField="SINO" HeaderText="SI No"  Width="80px"  >
                      <TemplateSettings TemplateId="GridTemplate1" />
                  </ogrid:Column> 
             
                <ogrid:Column DataField="ProjectCode" HeaderText="Project Code"   ></ogrid:Column>
                   <ogrid:Column DataField="Section" HeaderText="Sector"   ></ogrid:Column>
                <ogrid:Column DataField="BudAmt" HeaderText="Budget Amount" Align="Right" ></ogrid:Column>
                 <ogrid:Column DataField="AppAmout" HeaderText="Approved Budget Amount" Align="Right"  Wrap="true" Width="200px"  ></ogrid:Column>              
                      <ogrid:Column DataField="POAmount" HeaderText="PO Amount" Align="Right"  Wrap="true" Width="200px"  ></ogrid:Column>              
                      
            </Columns> 
                    
            <Templates>
                <ogrid:GridTemplate runat="server" ID="GridTemplate1">
                    <Template>
                      <b>  <%#(Container.RecordIndex + 1) %>.</b>
                    </Template>
                </ogrid:GridTemplate>
            </Templates>    
        </ogrid:Grid>
                </center>
                   <br />
                <center>
                      <asp:Button ID="btnProjectWiseExport" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnProjectWiseExport_Click" TabIndex="6"></asp:Button>
                       &nbsp;
                     <input onclick="exportgridProject()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" tabindex="7" />        
                      </center>
                    </div>
                <br />
            </div>
       
</asp:Content>


