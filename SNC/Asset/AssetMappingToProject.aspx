<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Asset/AssetMappingToProject.aspx.cs" Inherits="AssetMappingToProject" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
      



    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>


    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>
              Asset Transfer
            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-2">
                    From Project&nbsp;
                    <asp:RequiredFieldValidator ID="RFVProject" runat="server" InitialValue="-Select-" ControlToValidate="ddlFromProject" CssClass="Validation_Text" ValidationGroup="ValTransfer" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlFromProject" runat="server" CssClass="form-control">
                        <asp:ListItem Text="-Select-"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    To Project
&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="-Select-" ControlToValidate="ddlToProject" CssClass="Validation_Text" ValidationGroup="ValTransfer" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlToProject" runat="server" CssClass="form-control"  >
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>


            </div>

            <div class="row">
               
                <div class="col-md-2">
                    Department &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="-Select-" ControlToValidate="ddlDepartment" CssClass="Validation_Text" ValidationGroup="ValTransfer" ErrorMessage="*"></asp:RequiredFieldValidator>

                 

                </div>
                

                    
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"  AutoPostBack="true" >
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
                      

                <div class="col-md-2">
                    Receiver 
 &nbsp;
                 
 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="-Select-" ControlToValidate="ddlReceiver" CssClass="Validation_Text" ValidationGroup="ValTransfer" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlReceiver" runat="server" CssClass="form-control"  >
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>


            </div>




            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="Btn_Search" runat="server" OnClick="Btn_Search_Click" Text="Search" ValidationGroup="ValTransfer" CssClass="btn btn-default" ></asp:Button>
                    <asp:Button ID="Btn_Cancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="Btn_Cancel_Click" ></asp:Button>
                </div>

            </div>
        
            <div>
            <ogrid:Grid runat="server" ID="GVSearchGrid" CallbackMode="false" OnUpdateCommand="GVSearchGrid_UpdateCommand" KeepSelectedRecords="true"    AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings ScrollWidth="97%" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
            <Columns>
                  <ogrid:Column HeaderText="Edit" AllowEdit="true" Width="120px"></ogrid:Column>
                 <ogrid:Column DataField="Asset_Code" HeaderText="AssetCode" ReadOnly="true"></ogrid:Column>                
                <ogrid:Column DataField="Name" HeaderText="AssetName" ReadOnly="true"></ogrid:Column>
                  <ogrid:Column DataField="Asset_Type" HeaderText="Type" ReadOnly="true"></ogrid:Column>                
                <ogrid:Column DataField="Category_Name" HeaderText="Category" ReadOnly="true"></ogrid:Column>
                  <ogrid:Column DataField="Condition" HeaderText="Condition" ReadOnly="true"></ogrid:Column>                
                <ogrid:Column DataField="TransferDate" HeaderText="TransferDate"  ></ogrid:Column>
                 <ogrid:Column DataField="ScheduleStartDate" HeaderText="ScheduleStartDate"></ogrid:Column>
                <ogrid:CheckBoxColumn DataField="DL" Width="50px" ></ogrid:CheckBoxColumn>
                <ogrid:CheckBoxColumn DataField="RC" ></ogrid:CheckBoxColumn>
                <ogrid:CheckBoxColumn DataField="Road_Tax_Reciept" Width="150px" ></ogrid:CheckBoxColumn>
                <ogrid:CheckBoxColumn DataField="INSURANCE" Width="120px" ></ogrid:CheckBoxColumn>
                <ogrid:CheckBoxColumn DataField="PERMIT"  Width="85px"></ogrid:CheckBoxColumn>
                <ogrid:CheckBoxColumn DataField="NOC" Width="90px" ></ogrid:CheckBoxColumn>
                <ogrid:CheckBoxColumn DataField="FC" Width="70px" ></ogrid:CheckBoxColumn>
                <ogrid:CheckBoxColumn DataField="Way_BILL" Width="120px"  ></ogrid:CheckBoxColumn>
                 <ogrid:Column DataField="Remarks" HeaderText="Remarks" ></ogrid:Column>
                 <ogrid:CheckBoxSelectColumn HeaderText=" Select All" ShowHeaderCheckBox="true" Width="100px"></ogrid:CheckBoxSelectColumn>




                </Columns>
                </ogrid:Grid>
                <div class="row">
                <div class="col-md-12 text-center">
                <asp:Button ID="btntransfer" runat="server" Text="Transfer" OnClick="btntransfer_Click" CssClass="btn btn-default" />&nbsp;
                
                    </div>

            </div>
           
        </div>
    </div>
   
    

</asp:Content>

