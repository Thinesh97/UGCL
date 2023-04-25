<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Inventory/MIN.aspx.cs" Inherits="MIN" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
        });

    </script>
    <script type="text/javascript">

        //function ConfirmationForRecurring() {
        //    if (confirm("This recurring item not rearched 80 % of standard hour. Do you want to proceed?") == false) {
        //        return false;
        //    }
        //    else {

        //        window.location.href = "MIN.aspx?MIN_No=MIN-SNC-003";
        //    }
        //}

    </script>
       <script type="text/javascript">
           $(document).ready(function () {
               $('.chosen-select').chosen();
           });

       </script>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>


    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Material Issue Note 

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-2">
                    MIN No&nbsp;
                  
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtMINNo" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>

                </div>
                <div class="col-md-2">
                    Date&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDate" CssClass="Validation_Text" ValidationGroup="ValMIN" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtDate" CssClass="form-control" onkeypress="javascript:return false;" onpaste="javascript:return false;" runat="server"></asp:TextBox>

                    <ajaxToolkit:CalendarExtender runat="server" ID="cal1" Format="dd-MM-yyyy" TargetControlID="txtDate"></ajaxToolkit:CalendarExtender>


                </div>

            </div>
            <div class="row">

                <div class="col-md-2">
                    Project Name&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="-Select-" ControlToValidate="ddlProjectName" CssClass="Validation_Text" ValidationGroup="ValMIN" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>

                </div>
                <div class="col-md-2">
                    Issue To&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rdIssueTo" CssClass="Validation_Text" ValidationGroup="ValMIN" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="rdIssueTo" AutoPostBack="true" OnSelectedIndexChanged="rdIssueTo_SelectedIndexChanged">
                        <asp:ListItem Text="UGCL&nbsp;" Value="UGCL"></asp:ListItem>
                        <asp:ListItem Text="Sub Contractor" Value="Sub Contractor"></asp:ListItem>
                    </asp:RadioButtonList>

                </div>

            </div>
            <div class="row" visible="false" runat="server" id="deptempname">
                <div class="col-md-2">
                    Issue To/For
                
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtIssueToFor" runat="server" CssClass="form-control" MaxLength="20" />

                </div>
            </div>
           <div class="row" visible="false" runat="server" id="subcontractorrecoverable">
                <div class="col-md-2">Sub Contractor Name</div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlSubContractor" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubContractor_SelectedIndexChanged"  class="chosen-select form-control">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
                 <div class="col-md-2">
                   Select WO&nbsp;
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlWO" runat="server"   CssClass="form-control" TabIndex="3"></asp:DropDownList>
                </div>
                
            </div>
            <div class="row">
                <div class="col-md-2">Approver Name</div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlVerifier" class="chosen-select form-control" TabIndex="4"></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Recoverable&nbsp;
                 
                </div>
                <div class="col-md-4">
                    <asp:RadioButtonList runat="server" ID="rdRecoverable" RepeatDirection="Horizontal">
                        <asp:ListItem Value="True" Text="Yes" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="False" Text="No"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
               
            </div>
            <div  class="row">
                 <div class="col-md-2">Remarks</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" MaxLength="250" Style="resize: none" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ValMIN" CssClass="btn btn-default" OnClick="btnSubmit_Click"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btnCancel_Click"></asp:Button>
                    <a href="#myModalAddItem" data-toggle="modal" role="button">
                        <asp:Button ID="btnAddItem" Text="Add Item" CssClass="btn btn-default" runat="server" Visible="false" CausesValidation="false" />
                    </a>
                </div>

            </div>
            <br />
            <center>
                
                <%--i am creating oboutgrid here-- % <ogrid:Column DataField="MIN_Item_ID" HeaderText="Item ID"  Visible="true"></ogrid:Column>>--%>
                  <ogrid:Grid runat="server" ID="ItemList_Grid" OnRowDataBound="ItemList_Grid_RowDataBound" CallbackMode="false" OnDeleteCommand="ItemList_Grid_DeleteCommand" AutoGenerateColumns="false"  AllowRecordSelection="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">         
            <ScrollingSettings ScrollWidth="100%" />
             <ExportingSettings ExportAllPages="true"   ColumnsToExport="MIN_Item_ID,Budget_Sector,Item_Code,Mat_cat_ID,Unit,Recurring,Asset_Code,Standard,Maintenance,Service_Date,Quantity,MIN_No" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
			
            <Columns>
                
               <ogrid:Column HeaderText="MIN Item ID" runat="server" Width="110" >
            <TemplateSettings  TemplateId="ListItemTemplate"/>
        </ogrid:Column> 
                 <ogrid:Column DataField="MIN_Item_ID" HeaderText="Item ID"  Visible="false"></ogrid:Column>
                <ogrid:Column DataField="Item_Name" HeaderText="Item" Width="110" ></ogrid:Column>
                 <ogrid:Column DataField="UOMName" HeaderText="Unit" Width="90"></ogrid:Column>  
                <ogrid:Column DataField="RequestedQty" HeaderText="Requested QTY" Align="right" Width="110"></ogrid:Column>
                <ogrid:Column HeaderText="Approve QTY" AllowEdit="true" Align="Right"  width="145px" ItemStyle-Font-Size="16px" >
                            <TemplateSettings TemplateId="ApproveTemplate"/>
                        </ogrid:Column>
                  <ogrid:Column HeaderText="Select" Width="60px" > <TemplateSettings TemplateId="SelectTemplate"/> </ogrid:Column> 
                  <ogrid:Column DataField="ApprovedQty" HeaderText="Approved QTY" Width="130"></ogrid:Column> 
                 <ogrid:Column HeaderText="Issue QTY" AllowEdit="true" Align="Right"  width="145px" ItemStyle-Font-Size="16px" >
                            <TemplateSettings TemplateId="IssueTemplate"/>
                        </ogrid:Column>
                <ogrid:Column DataField="MIN_No" HeaderText="MIN_No"></ogrid:Column>
                 <ogrid:Column DataField="Sector_Name" HeaderText="Budget Sector"  Visible="true"></ogrid:Column>   
                 <ogrid:Column DataField="Category_Name" HeaderText="Category_Name" Width="130"></ogrid:Column>
                 <ogrid:Column DataField="Recurring" HeaderText="Recurring" Width="100"></ogrid:Column>
                <ogrid:Column DataField="Asset_Code" HeaderText="Asset Code" Width="100"></ogrid:Column>
                <ogrid:Column DataField="Standard" HeaderText="Standard" Align="right" Width="100"></ogrid:Column>
                  <ogrid:Column DataField="Maintenance" HeaderText="Maintenance" Width="120"></ogrid:Column>
                  <ogrid:Column DataField="Service_Date"  HeaderText="Service Date" Width="110" DataFormatString="{0:d}" ></ogrid:Column>
                <ogrid:Column DataField="IsApproved" HeaderText="Maintenance" Visible="false" Width="120"></ogrid:Column> 
                <ogrid:Column HeaderText="Delete" Width="110" AllowDelete="true"></ogrid:Column>            
            </Columns>
             <Templates>
          <ogrid:GridTemplate ID="SelectTemplate" runat="server">
                            <Template>
                                <asp:CheckBox runat="server" ID="chkSelect" />
                                <asp:HiddenField ID="hdn_MIN_Item_ID" runat="server" Value='<%# Container.DataItem["MIN_Item_ID"] %>'/>
                                <asp:HiddenField ID="hdn_Item_Code" runat="server" Value='<%# Container.DataItem["Item_Code"] %>'/>
                            </Template>
                        </ogrid:GridTemplate>
        <ogrid:GridTemplate ID="ListItemTemplate" runat="server">
            <Template>

                <asp:LinkButton ID="lnkMINItemID" runat="server" CssClass="gridCB" Text='<%# Convert.ToInt32(Container.DataItem["MIN_Item_ID"]) %>'>
 </asp:LinkButton>
            </Template>
        </ogrid:GridTemplate>
                  <ogrid:GridTemplate ID="ApproveTemplate" runat="server">
                            <Template>
                                <asp:TextBox runat="server"  Value='<%# Container.DataItem["RequestedQty"] %>' ID="txtApproveQty"  AutoComplete="Off" CssClass="gridCB" Width="100px"></asp:TextBox>
                                <%--<footer>
                                    <asp:Label runat="server" ID="lblTotalApproveAmt" Text="100"></asp:Label>
                                </footer>--%>
                            </Template>
                            
                        </ogrid:GridTemplate>
                  <ogrid:GridTemplate ID="IssueTemplate" runat="server">
                            <Template>
                                <asp:TextBox runat="server"  Value='<%# Container.DataItem["ApprovedQty"] %>' ID="txtIssueQty"  AutoComplete="Off" CssClass="gridCB" Width="100px"></asp:TextBox>
                                <%--<footer>
                                    <asp:Label runat="server" ID="lblTotalApproveAmt" Text="100"></asp:Label>
                                </footer>--%>
                            </Template>
                            
                        </ogrid:GridTemplate>
    </Templates>
                      

        </ogrid:Grid>
                   
      
                </>
            <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        <br />
                 <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" CssClass="btn btn-default"></asp:Button>
                <asp:Button ID="btnIssueQty" runat="server" Text="Issue Qty" OnClick="btnIssueQty_Click" CssClass="btn btn-default"></asp:Button>
                </div>

    </div>

    <ajaxToolkit:ModalPopupExtender ID="ModelMINPopup" runat="server" PopupControlID="PanelIndentItem" TargetControlID="btnAddItem"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelIndentItem" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnClose" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>  <h5 id="myModalLabelcrate">Add Items</h5></center>
                </div>
                <div class="modal-body">
               
                            <div class="row">
                                <div class="col-md-2">
                                    Budget Sector&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlBudgetSector" ValidationGroup="ValAddItem" InitialValue="-Select-" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlBudgetSector" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBudgetSector_SelectedIndexChanged">
                                        <asp:ListItem Text="-Select-" Value="-Select-"></asp:ListItem>
                                       
                                    </asp:DropDownList>

                                </div>

                                <div class="col-md-2">
                                    Category&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="-Select-" ControlToValidate="ddlCategory" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"  OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="-Select-" Value="-Select-" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>


                            </div>

                            <div class="row">
                                <div class="col-md-2">
                                    Item&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" InitialValue="-Select-" ControlToValidate="ddlItem" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlItem" runat="server" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                        <asp:ListItem Text="-Select-" Value="-Select-" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    Recurring
       
                                </div>
                                <div class="col-md-4">
                                    <asp:CheckBox ID="chkRenewable" runat="server" AutoPostBack="true" OnCheckedChanged="chkRenewable_CheckedChanged" />
                                </div>

                            </div>

                           <div class="row" visible="false" runat="server" id="TrAutomobiles">
                                <div >
                                     <div class="col-md-2">
                                        
                            Asset Type&nbsp;
                                          <%--<asp:RequiredFieldValidator ID="RfvTyep" runat="server" InitialValue="-Select-" ControlToValidate="ddlAssetType_r" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlAssetType_r" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddlAssetType_r_SelectedIndexChanged" CssClass="form-control">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            Asset Category&nbsp;
                           <%--  <asp:RequiredFieldValidator ID="RfvCat" runat="server" InitialValue="-Select-" ControlToValidate="ddlAssetCate_r" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlAssetCate_r" runat="server" OnSelectedIndexChanged="ddlAssetCate_r_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                                    </div>
                               </div>
                                <div class="row">
                                    <div  visible="false" runat="server" id="TrAutomobiles1">
                                     <div class="col-md-2">
                                        Asset Name&nbsp;
                  <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" InitialValue="-Select-" ControlToValidate="ddlAssetName" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlAssetName" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAssetName_SelectedIndexChanged">
                                            <asp:ListItem Text="-Select-" Value="-Select-"></asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                        </div>
                                     <div  visible="false" runat="server" id="divrecurring">
                                    <div class="col-md-2">
                                        Maintenance&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" InitialValue="-Select-" ControlToValidate="ddlMaintenance" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlMaintenance" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="-Select-" Value="-Select-" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Schedule Service" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Prevent Maintenance" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Other" Value="3"></asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                         </div>

                                
                                </div>
                     <div class="row" visible="false" runat="server" id="divrecurring1">
                                 <div >
                                     <div class="col-md-2">
                                        Standard (Hrs/kms)&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtStandard" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtStandard" CssClass="form-control input-pos-float" runat="server"></asp:TextBox>
                                    </div>
                                      <div class="col-md-2">
                                        Service Date&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtServiceDate" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtServiceDate" CssClass="form-control" onkeypress="javascript: return false;" onPaste="javascript: return false;" runat="server"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtServiceDate" Format="dd-MM-yyyy" runat="server"></ajaxToolkit:CalendarExtender>
                                    </div>


                                </div>

                                     </div>
                         

                                <div class="row">           
                                
                                    <div class="col-md-2">
                                    Unit&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue="-Select-" ControlToValidate="ddlUnit" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*">
                    </asp:RequiredFieldValidator>

                                </div>
                                <div class="col-md-4">

                            <asp:DropDownList ID="ddlUnit" runat="server" Enabled="false" CssClass="form-control">
                                        <asp:ListItem Text="-Select-" Value="-Select-" ></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                    <div class="col-md-2">
                                       Available Qty&nbsp;
                   

                                </div>
                                <div class="col-md-4">
                                 <asp:TextBox runat="server" ID="txtAvailableQty" Enabled="false" CssClass="form-control input-pos-int"></asp:TextBox>
                          
                                </div>


                                </div>
                        

                            <div class="row">
                               
                                <div class="col-md-2">
                                    Issue Qty  &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQty" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="txtQty" CssClass="form-control input-pos-float"></asp:TextBox>
                                </div>


                            </div>
                            
                        
              
                <div class="modal-footer">
                    <center>
           <asp:Button ID="btnSaveItem" runat="server" Text="Save"   CssClass="btn btn-default" OnClick="Btn_SaveItem" ValidationGroup="ValAddItem"  />
                     
                        <asp:Button ID="btnCancelItem" runat="server" Text="Cancel" OnClick="btnCancelItem_Click"  CssClass="btn btn-default" CausesValidation="false"   />
                 </center>

                </div>
        </div>
        </div>
      

    </asp:Panel>
</asp:Content>


