<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Asset/AssetPerformanceReport.aspx.cs" Inherits="AssetPerformanceReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function exportgrid() {
            Grid_AssetPerformanceReport.exportToExcel();
        }
        function exportgrid2() {
            GVAssetDetails.exportToExcel();
        }
        function exportgrid3() {
            GvVehicleWisePerformanceCost.exportToExcel();
        }

    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Asset Performance Report 

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->
            <div class="row">
                <div class="col-md-2">Start Date</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtStartDate" CssClass="form-control" onkeypress="javascript: return false;" onPaste="javascript: return false;" runat="server" TabIndex="1" AutoComplete="Off"></asp:TextBox>

                    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtStartDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                </div>
                <div class="col-md-2">
                    End Date
                     
                   
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtEndDate" runat="server" onkeypress="javascript: return false;" onPaste="javascript: return false;" CssClass="form-control" TabIndex="2" AutoComplete="Off"></asp:TextBox>

                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtEndDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                    <div>
                        <asp:CompareValidator ID="cmp1" runat="server" ControlToCompare="txtStartDate" ControlToValidate="txtEndDate" Type="Date" Operator="GreaterThanEqual" Display="Dynamic" ErrorMessage="End date should n't be less than start date" ForeColor="Red"></asp:CompareValidator>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    Project Name                
                </div>
                <div class="col-md-4">

                    <asp:DropDownList ID="ddlProjectName" CssClass="form-control" runat="server" TabIndex="3">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>

                </div>

                 <div class="col-md-2">Display Columns</div>
                <div class="col-md-4">
                    <asp:ListBox runat="server" ID="lstColumns" SelectionMode="Multiple" CssClass="form-control" TabIndex="14" Rows="11"></asp:ListBox>
                </div>
                </div>            
               <br />
             <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSearch" runat="server" Text="Generate" OnClick="btnGenerate_Click" CssClass="btn btn-default" TabIndex="4"></asp:Button>    
                     <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnReset_Click"  CssClass="btn btn-default" TabIndex="5"></asp:Button>        
                </div>

            </div>
             
                <br />
            

                <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
    </div>

    </div>
</asp:Content>
