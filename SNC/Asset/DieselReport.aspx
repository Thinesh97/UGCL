<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="DieselReport.aspx.cs" Inherits="DieselReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <script type="text/javascript">
          function exportgrid() {
              GridDieselReport.exportToExcel();
          }


    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
   <%-- <asp:UpdatePanel ID="updatepanelDieselReport" runat="server">
        <ContentTemplate>--%>

       
    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

               Diesel Report 

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-2">Start Date</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtStartDate" CssClass="form-control" onkeypress="javascript: return false;" onPaste="javascript: return false;" runat="server" TabIndex="1"></asp:TextBox>
                  
                     <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtStartDate"  Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                </div>
                <div class="col-md-2">End Date
                     
                   
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtEndDate" runat="server"  onkeypress="javascript: return false;" onPaste="javascript: return false;" CssClass="form-control" TabIndex="2"></asp:TextBox>
                    
                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtEndDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                     <div>
                     <asp:CompareValidator ID="cmp1" runat="server"  ControlToCompare="txtStartDate" ControlToValidate="txtEndDate" Type="Date" Operator="GreaterThanEqual" Display="Dynamic"  ErrorMessage="End date should n't be less than start date" ForeColor="Red"></asp:CompareValidator>
                </div>
                    
                </div>
               
                

            </div>
             <div class="row">
                <div class="col-md-2">Project Name
                
                </div>
                <div class="col-md-4">
                     
                    <asp:DropDownList ID="ddlProjectName" CssClass="form-control" runat="server" TabIndex="3">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                   
                     
                </div>
             
            </div>
         
            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Search"  CssClass="btn btn-default" OnClick="btnSubmit_Click" TabIndex="4"></asp:Button>
                   
                   
                </div>

            </div>
            <br />
            <div class="row">

                   <center>
        <ogrid:Grid runat="server" ID="GridDieselReport" AllowGrouping="true" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" CallbackMode="false" AllowPaging="true" PageSize="10" >
            <ScrollingSettings ScrollWidth="100%" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <ExportingSettings ExportAllPages="true" ExportTemplates="true" />
            <Columns>             
                  <ogrid:Column DataField="Category_Name" HeaderText="Asset Category" ></ogrid:Column>                           
                <ogrid:Column DataField="Name" HeaderText="Asset Name" ></ogrid:Column>
                <ogrid:Column DataField="Code" HeaderText="Asset Code" ></ogrid:Column>
                 <ogrid:Column DataField="Reg_No" HeaderText="Reg No" ></ogrid:Column>
                <ogrid:Column DataField="Project_Name" HeaderText="Project Name" ></ogrid:Column>
                 <ogrid:Column DataField="Location_Name" HeaderText="Working Area" ></ogrid:Column>
             <%--   <ogrid:Column DataField="Date" HeaderText="Date" ></ogrid:Column> 
                <ogrid:Column DataField="Month" HeaderText="Month" ></ogrid:Column>  --%>            
                <ogrid:Column DataField="TOTAL" HeaderText="Issued Diesel" ></ogrid:Column>  
            <%--     <ogrid:Column DataField="Total" HeaderText="Total" ></ogrid:Column>     --%>           
            </Columns>
        </ogrid:Grid>
                      <br />
                      <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click" TabIndex="5"></asp:Button>
                    <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" tabindex="6" />    
                      </center>
            </div>
            <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        </div>

    </div>
 <%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
