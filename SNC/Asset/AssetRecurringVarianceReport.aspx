<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Asset/AssetRecurringVarianceReport.aspx.cs" Inherits="AssetRecurringVarianceReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <Ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></Ajax:ToolkitScriptManager>
    <div class="panel panel-default">
        <script type="text/javascript">
            function exportgridMin() {
                VarRep_Grid.exportToExcel();
            }
        </script>
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Asset Recurring variance Report 

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->
            <div class="row">
                <div class="col-md-2">
                    Start Date &nbsp;
                   <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValVarRep"  ControlToValidate="txtStartDate"></asp:RequiredFieldValidator>--%>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtStartDate" CssClass="form-control" onkeypress="javascript: return false;" onPaste="javascript: return false;" runat="server" TabIndex="1"></asp:TextBox>

                    <Ajax:CalendarExtender ID="CalendarExtender2" TargetControlID="txtStartDate" Format="dd-MM-yyyy" runat="server"></Ajax:CalendarExtender>
                </div>
                <div class="col-md-2">
                    End Date &nbsp;
                      <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValVarRep"  ControlToValidate="txtEndDate"></asp:RequiredFieldValidator>--%>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtEndDate" runat="server" onkeypress="javascript: return false;" onPaste="javascript: return false;" CssClass="form-control" TabIndex="2"></asp:TextBox>

                    <Ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="txtEndDate" Format="dd-MM-yyyy" runat="server"></Ajax:CalendarExtender>
                    <div>
                        <asp:CompareValidator ID="cmp1" runat="server" ControlToCompare="txtStartDate" ValidationGroup="ValVarRep" ControlToValidate="txtEndDate" Type="Date" Operator="GreaterThanEqual" Display="Dynamic" ErrorMessage="End date should n't be less than start date" ForeColor="Red"></asp:CompareValidator>
                    </div>
                </div>
            </div>
            <div class="row">

                <div class="col-md-2">
                    Budget Sector &nbsp;
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValVarRep" InitialValue="-Select-" ControlToValidate="ddlBudgetSector"></asp:RequiredFieldValidator>



                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlBudgetSector" runat="server" OnSelectedIndexChanged="ddlBudgetSector_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" TabIndex="1">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Category Name &nbsp;
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValVarRep" InitialValue="-Select-" ControlToValidate="ddlCategoryName"></asp:RequiredFieldValidator>



                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlCategoryName" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control" TabIndex="2">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>



            </div>
            <div class="row">
                <div class="col-md-2">
                    Item&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValVarRep" InitialValue="-Select-" ControlToValidate="ddlItem_r"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlItem_r" runat="server" CssClass="form-control">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
                
                
            </div>
          
            <div class="row">
                <div class="col-md-2">
                    Asset Category&nbsp;
                </div>
                <div class="col-md-4">
                       <asp:DropDownList ID="ddlAssetCate_r" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAssetCate_r_SelectedIndexChanged" CssClass="form-control">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Asset &nbsp;
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlAsset" runat="server" CssClass="form-control">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
              <br />
            <center>
                <asp:Button ID="Btn_VarRep" runat="server" CssClass="btn btn-default" ValidationGroup="ValVarRep" Text="Search" OnClick="Btn_VarRep_Click"  />
            </center>

            <div id="VarRep_Gv" runat="server" visible="false">
                <br />


                <center>
                <ogrid:Grid runat="server" ID="VarRep_Grid" AllowGrouping="true" CallbackMode="true"   AutoGenerateColumns="false"  AllowRecordSelection="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">         
            <ScrollingSettings ScrollWidth="100%" />
             
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
			 <ExportingSettings ExportAllPages="true" ExportTemplates="true"  
                    />
            <Columns>
                
         
                 <ogrid:Column DataField="DATE" HeaderText="Date of Issue"  DataFormatString="{0:d}" ></ogrid:Column>
                  <ogrid:Column DataField="Code" HeaderText="Asset Code"  ></ogrid:Column>
                <ogrid:Column DataField="Reg_No" HeaderText="Reg No"  ></ogrid:Column>
                  <ogrid:Column DataField="Name" HeaderText="Asset Name"  ></ogrid:Column>
                <ogrid:Column DataField="Item_Name" HeaderText="Item Name "  ></ogrid:Column>
                 <ogrid:Column DataField="UOMPrefix" HeaderText="Unit" ></ogrid:Column>            
                <ogrid:Column DataField="Quantity" HeaderText="Qty"  ></ogrid:Column>
                <ogrid:Column DataField="Standard" HeaderText="Standard Hr/ KM" ></ogrid:Column>
                 <ogrid:Column DataField="Actual1" HeaderText="Actual Hr/ KM" ></ogrid:Column>   
                 <ogrid:Column DataField="variance1" HeaderText="Variance" ></ogrid:Column>
                  <ogrid:Column DataField="EndHoursORKM" HeaderText="End Hours/KM"  ></ogrid:Column>
                <ogrid:Column DataField="LastReplacedEndHoursORKM1" HeaderText="Last Replaced End Hours/KM" ></ogrid:Column>
                <ogrid:Column DataField="status" HeaderText="Status"  ></ogrid:Column>
                            
            </Columns>
             <Templates>

    </Templates>
                   
        </ogrid:Grid>
            </center>
                <br />

                <center>
                 <input onclick="exportgridMin()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />
                </center>

            </div>

        </div>
    </div>
</asp:Content>
