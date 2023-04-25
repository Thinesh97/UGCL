<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderReport.aspx.cs" Inherits="SNC.Procurement.PurchaseOrderReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

               Purchase Order Report 

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-2">Start Date</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtStartDate" CssClass="form-control" runat="server"></asp:TextBox>
                     <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtStartDate" Format="dd/MM/yyyy" runat="server"></asp:CalendarExtender>
                </div>
                <div class="col-md-2">End Date</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtEndDate" Format="dd/MM/yyyy" runat="server"></asp:CalendarExtender>
                </div>

            </div>
            <%-- <div class="row">
                <div class="col-md-2">Project Name</div>
                <div class="col-md-4">
                    <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>

                </div>
             
            </div>--%>
         
            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Search" CssClass="btn btn-default"></asp:Button>
                   
                   
                </div>

            </div>
            <br />
            <div class="row">

                   <center>
        <ogrid:Grid runat="server" ID="Grid1" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" CallbackMode="false" AllowPaging="true" PageSize="10" >
            <ScrollingSettings ScrollWidth="100%" />
            <Columns>             
                <ogrid:Column DataField="" HeaderText="Project ID" ></ogrid:Column>
                <ogrid:Column DataField="" HeaderText="Project Name" ></ogrid:Column>
                <ogrid:Column DataField="" HeaderText="Year" ></ogrid:Column>
                <ogrid:Column DataField="" HeaderText="Month" ></ogrid:Column>
                <ogrid:Column DataField="" HeaderText="Total Values Of Purchase" ></ogrid:Column>
                <ogrid:Column DataField="" HeaderText="Total Purchase From Local" ></ogrid:Column> 
                <ogrid:Column DataField="" HeaderText="Total Purchase From HO" ></ogrid:Column>                 
            </Columns>
        </ogrid:Grid>
                      <br />
                      <asp:Button ID="button1" runat="server" Text="Export To PDF" CssClass="btn btn-default"></asp:Button>
                    <asp:Button ID="button2" runat="server" Text="Export To Excel" CssClass="btn btn-default"></asp:Button>     
                      </center>
            </div>
            <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        </div>

    </div>
    
</asp:Content>
