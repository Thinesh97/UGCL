<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="ServiceDetailsReport.aspx.cs" Inherits="ServiceDetailsReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script type="text/javascript">
         function exportgrid() {
             GridServiceDetlReport.exportToExcel();
         }


    </script>


    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

               Service Details Report 

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-2">Start Date</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtStartDate" CssClass="form-control" runat="server" TabIndex="1"></asp:TextBox>
                     <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtStartDate" Format="dd/MM/yyyy" runat="server"></asp:CalendarExtender>
                </div>
                <div class="col-md-2">End Date</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" TabIndex="2" ></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtEndDate" Format="dd/MM/yyyy" runat="server"></asp:CalendarExtender>
                </div>

            </div>
             <div class="row">
                <div class="col-md-2">Project Name</div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlProjectName" CssClass="form-control" runat="server" TabIndex="3" >
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>

                </div>
             
            </div>
         
            <br />
            <div class="row">
                <div class="col-md-12 text-center" tabindex="4">
                    <asp:Button ID="btnSubmit" runat="server" Text="Search" CssClass="btn btn-default" ></asp:Button>
                   
                   
                </div>

            </div>
            <br />
            <div class="row">

                   <center>
        <ogrid:Grid runat="server" ID="GridServiceDetlReport" AutoGenerateColumns="false" AllowFiltering="true" AllowAddingRecords="false" CallbackMode="false" AllowPaging="true" PageSize="10" >
            <ScrollingSettings ScrollWidth="100%" />
            <Columns>    
                <ogrid:Column DataField="" HeaderText="Date" ></ogrid:Column>          
                <ogrid:Column DataField="" HeaderText="Asset Category" ></ogrid:Column>                           
                <ogrid:Column DataField="" HeaderText="Asset Name" ></ogrid:Column>
                <ogrid:Column DataField="" HeaderText="Asset Code" ></ogrid:Column>
               
                <ogrid:Column DataField="" HeaderText="Project Name" ></ogrid:Column>
                 <ogrid:Column DataField="" HeaderText="UOM" ></ogrid:Column>
                   <ogrid:Column DataField="" HeaderText="Quantity" ></ogrid:Column>
                <ogrid:Column DataField="" HeaderText="Month" ></ogrid:Column>              
                <ogrid:Column DataField="" HeaderText="Local Purchase" ></ogrid:Column> 
                <ogrid:Column DataField="" HeaderText="HO Purchase" ></ogrid:Column>   
                 <ogrid:Column DataField="" HeaderText="Total Purchase" ></ogrid:Column>                    
            </Columns>
        </ogrid:Grid>
                      <br />
                      <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click" TabIndex="5" ></asp:Button>
                      <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" tabindex="6"  />      
                      </center>
            </div>
            <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        </div>

    </div>
    
</asp:Content>
