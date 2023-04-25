<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="MachineRunningHoursKmsReport.aspx.cs" Inherits="MachineRunningHoursKmsReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function exportgrid() {
            GridMachRunHourReport.exportToExcel();
        }
      

    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

               Machine Running Hours/Kms Report 

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-2">Start Date</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtStartDate" CssClass="form-control" onpaste="javascript : return false" onkeypress="javascript : return false" runat="server" TabIndex="1"></asp:TextBox>
                     <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtStartDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                </div>
                <div class="col-md-2">End Date</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtEndDate" runat="server" onpaste="javascript : return false" onkeypress="javascript : return false" CssClass="form-control" TabIndex="2"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtEndDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                </div>

            </div>
             <div class="row">
                <div class="col-md-2">Project Name</div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlProjectName" CssClass="form-control" runat="server" TabIndex="3">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>

                </div>
             
            </div>
         
            <br />
         
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Search" OnClick="btnSubmit_Click" CssClass="btn btn-default" TabIndex="4"></asp:Button>
                   
                   
                </div>
                 </div>
     
            <br />
            <div class="row">

                   <center>
                       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                           <ContentTemplate>
        <ogrid:Grid runat="server" ID="GridMachRunHourReport" AllowGrouping="true" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowFiltering="true" AllowAddingRecords="false" CallbackMode="false" AllowPaging="true" PageSize="10" >
            <ScrollingSettings ScrollWidth="95%" />
             <ExportingSettings ExportAllPages="true"  ExportTemplates="true" />
             <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <Columns>
                 <ogrid:Column DataField="Asset_Type" HeaderText="Asset Type" Width ="100px" Wrap="true"></ogrid:Column>             
                <ogrid:Column DataField="Category_Name" HeaderText="Asset Category" Width ="130px" Wrap="true"></ogrid:Column>     
                <ogrid:Column DataField="Name" HeaderText="Asset Name" Width ="150px" Wrap="true"></ogrid:Column>
                <ogrid:Column DataField="Code" HeaderText="Asset Code" Width ="100px" Wrap="true"></ogrid:Column>
                <ogrid:Column DataField="Reg_No" HeaderText="Reg No" Width ="100px" Wrap="true"></ogrid:Column>
               <%-- <ogrid:Column DataField="DRHDate" HeaderText="Date" Width ="100px" Wrap="true"></ogrid:Column>--%>
                <%-- <ogrid:Column DataField="Month" HeaderText="Month" Width ="100px" Wrap="true"></ogrid:Column>--%> 
                <ogrid:Column DataField="Project_Name" HeaderText="Project Name" Width ="250px" Wrap="true"></ogrid:Column>
                 <ogrid:Column DataField="Location_Name" HeaderText="Working Area" Width ="130px" Wrap="true"></ogrid:Column>
               <%-- <ogrid:Column DataField="UOM" HeaderText="UOM" Width ="100px" Wrap="true"></ogrid:Column>--%>
               <ogrid:Column DataField="Start_Hour" HeaderText="Start Hour" Width ="100px" Wrap="true" Align="right"></ogrid:Column>
                 <ogrid:Column DataField="End_Hour" HeaderText="End Hour" Width ="90px" Wrap="true" Align="right"></ogrid:Column>
                <ogrid:Column DataField="Hour_duration" HeaderText="Hour Duration" Width="120px"  Align="right" ></ogrid:Column>
                <ogrid:Column DataField="Start_Km" HeaderText="Start Km" Width ="90px" Wrap="true" Align="right"></ogrid:Column>
                <ogrid:Column DataField="End_Km" HeaderText="End Km" Width ="80px" Wrap="true" Align="right"></ogrid:Column>
               <ogrid:Column DataField="Distance_Duration" HeaderText="Distance Duration" Width="135px"  Align="right" ></ogrid:Column>
                <ogrid:Column DataField="Issued_Diesel_Qty" HeaderText="Issued Diesel" Width="120px"  Align="right"></ogrid:Column>  
                              
            </Columns>
        </ogrid:Grid>
                               </ContentTemplate>
                           <Triggers>
                               <asp:AsyncPostBackTrigger  ControlID="btnSubmit" EventName="Click" />
                           </Triggers>
                       </asp:UpdatePanel>
                      <br />
                      <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click" TabIndex="5"></asp:Button>
                 <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" tabindex="6" />    
                      </center>
            </div>
            <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        </div>

    </div>
    
</asp:Content>
