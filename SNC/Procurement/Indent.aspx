<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="Indent.aspx.cs" Inherits="Indent" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {

            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
        });

        function ConfirmDelete() {
            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                return false;
            }
            return true;
        }

        function Calc()
        {
            debugger;
            var Budgetvalue = document.getElementById('<%=txtBudgetQty.ClientID%>').value;
            var QtyRequiredvalue = document.getElementById('<%=txtReqQty.ClientID%>').value;
            var ExistingQty = document.getElementById('<%=txtExistingQty.ClientID%>').value;

            var ddlSector = document.getElementById("<%=ddlBudgetSector.ClientID %>");
            var selectedText = ddlSector.options[ddlSector.selectedIndex].innerHTML;
            var selectedValue = ddlSector.value;
            //alert("Selected Text: " + selectedText + " Value: " + selectedValue);
            

            //Total Project Budget Qty

            var projectBudgetQty = document.getElementById('<%=txtProjBudQty.ClientID%>').value;
            var totalExistingIndentQty = document.getElementById('<%=txtTotalIndentQty.ClientID%>').value;
           
            if (parseFloat(QtyRequiredvalue) > parseFloat(Budgetvalue)) {
                alert("Indent item can not generate more then budgeted qty.");
                document.getElementById('<%=txtReqQty.ClientID%>').value = 0;
                return false;
             }

            if ((parseFloat(ExistingQty) + parseFloat(QtyRequiredvalue)) > (parseFloat(Budgetvalue)))
            {
                alert("Indent generated for this item upto budgeted qty. Already Indented Qty is: " + ExistingQty);
                document.getElementById('<%=txtReqQty.ClientID%>').value = 0;
                return false;
            }
          
           
            if (document.getElementById('<%=txtProjBudQty.ClientID%>').value != '') {
                if ((parseFloat(totalExistingIndentQty) + parseFloat(QtyRequiredvalue)) > (parseFloat(projectBudgetQty))) {
                    if (selectedText == "BOQ Items") {
                        alert("Project Budgeted Qty for this Category is: " + projectBudgetQty + "   Already Indented Qty for this Category is : " + totalExistingIndentQty);
                    }
                    else {
                        alert("Project Budgeted Qty for this sector is: " + projectBudgetQty + "   Already Indented Qty for this sector is : " + totalExistingIndentQty);
                    }

                    document.getElementById('<%=txtReqQty.ClientID%>').value = 0;

                    return false;
                }
            }
        }

    </script>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
    </style>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>

    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Indent Details 

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->



            <div class="row">
                <div class="col-md-2">
                    Project Name&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" InitialValue="-Select-" ControlToValidate="ddlProjectName" CssClass="Validation_Text" ValidationGroup="ValIndent" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" TabIndex="1">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>

                </div>
                <div class="col-md-2">
                    Budget&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue="-Select-" ControlToValidate="ddlBudget" CssClass="Validation_Text" ValidationGroup="ValIndent" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlBudget" AutoPostBack="true" OnSelectedIndexChanged="ddlBudget_SelectedIndexChanged" CssClass="form-control" TabIndex="2">
                        <asp:ListItem Text="-Select-" Value="-Select-"></asp:ListItem>
                    </asp:DropDownList>
                </div>

            </div>


            <div class="row">
                <div class="col-md-2">
                    Indent No&nbsp;                
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtIndentNo" Enabled="false" CssClass="form-control" MaxLength="10"></asp:TextBox>

                </div>
                <div class="col-md-2">
                    Date&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIndDate" CssClass="Validation_Text" ValidationGroup="ValIndent" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtIndDate" Enabled="false" CssClass="form-control" onpaste="javascript:return false;" onkeypress="javascript:return false;" runat="server" TabIndex="3"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="cal1" Format="dd-MM-yyyy" TargetControlID="txtIndDate"></ajaxToolkit:CalendarExtender>
                </div>

            </div>
            <div class="row">

                <div class="col-md-2">
                    Prepared By&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="-Select-" ControlToValidate="ddlPreparedBy" CssClass="Validation_Text" ValidationGroup="ValIndent" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlPreparedBy" CssClass="form-control">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">Stock checked By</div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlStckChk" runat="server" CssClass="form-control" TabIndex="4">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>

                </div>
            </div>

            <div class="row">
               
                 <div class="col-md-2">
                    Location&nbsp;
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" InitialValue="-Select-" ControlToValidate="ddlNameofSite" CssClass="Validation_Text" ValidationGroup="ValIndent" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" Enabled="false" ID="ddlNameofSite" CssClass="form-control">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">HO Approver</div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlHOApproved" CssClass="form-control" TabIndex="6">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>


            <div class="row">
                <div class="col-md-2">
                    Process From&nbsp;
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="rblProcessFrom" CssClass="Validation_Text" ValidationGroup="ValIndent" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:RadioButtonList runat="server" ID="rblProcessFrom" AutoPostBack="true" OnSelectedIndexChanged="rblProcessFrom_SelectedIndexChanged" RepeatDirection="Horizontal" TabIndex="7">
                        <asp:ListItem Text="HO&nbsp;" Value="HO"></asp:ListItem>
                        <asp:ListItem Text="Site" Value="Site" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>

                </div>
                <div class="col-md-2">To be processed by</div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlProcessBy" Enabled="false" CssClass="form-control">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>


            </div>
            <div class="row">
               
                <div class="col-md-2">
                    Status&nbsp;
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="rd_Status" CssClass="Validation_Text" ValidationGroup="ValIndent" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:RadioButtonList runat="server" ID="rd_Status" CssClass="radiostyle" AutoPostBack="true" OnSelectedIndexChanged="rd_Status_SelectedIndexChanged" TabIndex="8">
                        <asp:ListItem Text="Open" Value="Open" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Processed" Value="Processed"></asp:ListItem>
                        <asp:ListItem Text="Close" Value="Close"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                 <div class="col-md-2">Remarks</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" Style="resize: none" TextMode="MultiLine" MaxLength="250" TabIndex="9"></asp:TextBox>

                </div>
                 <div class="col-md-2" style="margin-top:10px">Note</div>
                <div class="col-md-4" style="margin-top:10px">
                    <asp:TextBox runat="server" ID="txtnote" Enabled="true" CssClass="form-control" ></asp:TextBox>

                </div>
            </div>
            <div class="row">

               
            </div>


            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ValIndent" OnClick="btnSubmit_Click" CssClass="btn btn-default" TabIndex="10"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default" TabIndex="11"></asp:Button>
                    <asp:Button ID="btnAddItem" Text="Add Item" CssClass="btn btn-default" Visible="false" runat="server" CausesValidation="false" TabIndex="12"/>
                    <asp:Button ID="btnProceed" runat="server" Text="Proceed To PO" Visible="false" CssClass="btn btn-default" OnClick="btnProceed_Click" TabIndex="13"></asp:Button>

                    <a id="btnPrint" runat="server" visible="false" class="btn btn-default" tabindex="14">Print</a>
                    <asp:Button ID="btnReferBudgetItems" runat="server" Visible="false" Text="Refer Budget Items" OnClick="btnReferBudgetItems_Click" CssClass="btn btn-default" />

                </div>

            </div>
            <br />
            <center>
          
                
        <ogrid:Grid runat="server" ID="Grid_IndentItem"   ShowColumnsFooter="true" AutoGenerateColumns="false" OnRowCreated="Grid_IndentItem_RowCreated" OnRowDataBound="Grid_IndentItem_RowDataBound" OnDeleteCommand="Grid_IndentItem_DeleteCommand" CallbackMode="false"  FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="15">
            <ScrollingSettings ScrollWidth="100%" />
              <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
           <Columns>
               
            <ogrid:CheckBoxSelectColumn ShowHeaderCheckBox="true"/>
   
                 <ogrid:Column DataField="Indent_Item_Id" HeaderText="Indent Item ID" runat="server" Width="120" >
            <TemplateSettings  TemplateId="IndentItemIDTemplate"/>
        </ogrid:Column>    
                      
              <ogrid:Column DataField="Indent_No" HeaderText="Indent No" Width="120"></ogrid:Column>
                  <ogrid:Column DataField="Budget_Sector" HeaderText="Budget Sector" Width="150"></ogrid:Column>
                <ogrid:Column DataField="Category_Name" HeaderText="Category" Width="110" ></ogrid:Column>
                 <ogrid:Column DataField="Item_Name" HeaderText="Item" Width="120"></ogrid:Column>
                 <ogrid:Column DataField="BOQ" HeaderText="BOQ" Width="80" >
                     <TemplateSettings TemplateId="BOQTemplate" />
                 </ogrid:Column>
                 <ogrid:Column DataField="BOQ_No" HeaderText="BOQ No" Width="110" ></ogrid:Column>
                <ogrid:Column DataField="UOM" HeaderText="UOM" Width="100"></ogrid:Column>
                <ogrid:Column DataField="Total_Qty_Involved" HeaderText="Total Qty involved in this"  Width="150" ></ogrid:Column>
                <ogrid:Column DataField="Total_Qty_Recevied" HeaderText="Total Qty Received" Align="right" Width="140"></ogrid:Column>
                <ogrid:Column DataField="Qty_Available" HeaderText="Available Qty" Align="right" Width="120"></ogrid:Column>
                  <ogrid:Column DataField="Qty_required" HeaderText="Required Qty" Align="right"  Width="120"></ogrid:Column> 
                <ogrid:Column DataField="Tentative_Date" HeaderText="Tentative Date of Requirement" Align="center" Width="150" ></ogrid:Column>
                  <ogrid:Column DataField="Recurring" HeaderText="Recurring" Align="center" Width="130" ></ogrid:Column>
                  <ogrid:Column DataField="Reg_No" HeaderText="Registration Number" Align="center" Width="130" ></ogrid:Column>
                  <ogrid:Column DataField="AssetCode" HeaderText="Asset Code" Align="center" Width="130" ></ogrid:Column>
                  <ogrid:Column DataField="TotalRunningHrs" HeaderText="Total Running Hours" Align="center" Width="130" ></ogrid:Column>

                <ogrid:Column DataField="Whether_Req_Qty" HeaderText="Whether Req Qty" Align="center" Width="130" >
                      <TemplateSettings  TemplateId="WhetherReqQty"/>
                  </ogrid:Column>                  
               <ogrid:Column  HeaderText="Delete" AllowDelete="true" Width="70"></ogrid:Column>                     
                       
            </Columns>
       <Templates>
        <ogrid:GridTemplate ID="IndentItemIDTemplate" runat="server">
            <Template>                  
                       <asp:LinkButton ID="lnkbtnIndentItemID" Text='<%#Container.DataItem["Indent_Item_Id"] %>'  OnClick="lnkbtnIndentItemID_Click"   runat="server" CausesValidation="false" />              
                     
            </Template>
        </ogrid:GridTemplate>
           <ogrid:GridTemplate ID="BOQTemplate" runat="server">
               <Template>
                   <asp:Label ID="lblBOQ" runat="server" Text='<%#Container.DataItem["BOQ"].ToString() !=string.Empty && Container.DataItem["BOQ"].ToString() == "True" ? "Yes" : "No" %>'></asp:Label>
               </Template>
           </ogrid:GridTemplate>
           <ogrid:GridTemplate ID="WhetherReqQty" runat="server">
               <Template>
                   <asp:Label ID="lblWhetherReqQty" runat="server" Text='<%#Container.DataItem["Whether_Req_Qty"].ToString() == "True" ? "Yes" : "No" %>'></asp:Label>
               </Template>
           </ogrid:GridTemplate>
    </Templates>
        </ogrid:Grid>
               <br />
                 <asp:Button ID="btnMultipitemDelete" runat="server" Text="Multi Delete" OnClick="btnMultipitemDelete_Click" CssClass="btn btn-default" />
                </center>


           
            <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        </div>

    </div>

      <ajaxToolkit:ModalPopupExtender ID="ModelReferBudgetItems" runat="server" PopupControlID="matrilarefpanel" TargetControlID="btnReferBudgetItems"
        CancelControlID="btnCloseCSS2" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="matrilarefpanel" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnCloseCSS2" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center> <h5 id="myModalLabelcrate31">Refer Budget Items</h5></center>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4">
                          Sector Name
                              <asp:RequiredFieldValidator ID="RFVBudget" runat="server" ControlToValidate="ddlSectorName" ValidationGroup="valReferBudItem" InitialValue="-Select-" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlSectorName" CssClass="form-control"  runat="server" >
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                     
                    </div>
                      <div class="row" style="overflow-y: scroll">
                          <ogrid:Grid ID="GridReferBudgetItems" runat="server" AutoGenerateColumns="false" AllowAddingRecords="false">
                               
                              <Columns>
                                  <ogrid:CheckBoxSelectColumn ShowHeaderCheckBox="true"></ogrid:CheckBoxSelectColumn>
                                  <ogrid:Column DataField="Item_Code" HeaderText="ItemCode" />
                                  <ogrid:Column DataField="Item_Name" HeaderText="ItemName" />
                                  <ogrid:Column DataField="Req_Qty" Width="120" HeaderText="Quantity" Align="right" />
                                   <ogrid:Column DataField="Bud_Item_Id" HeaderText="Budget Item ID" Visible="false" />
                              </Columns>
                          </ogrid:Grid>
                          </div>
                   
                 <%--   <center>--%>
                     
             <asp:Button ID="btnImportReferBudgetItem" runat="server" Text="Import" OnClick="btnImportReferBudgetItem_Click" Visible="false" CssClass="btn btn-default" ValidationGroup="valReferBudItem" />
           <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"   CssClass="btn btn-default" ValidationGroup="valReferBudItem" />
            <asp:Button ID="btnCancelPopUp" runat="server" Text="Cancel"   CssClass="btn btn-default" CausesValidation="false" />
                 </center>

                </div>
            </div>
        </div>

        <input type="hidden" id="Hidden1" runat="server" value="1" />
    </asp:Panel>
    <!-- ModalPopupExtender -->

    <ajaxToolkit:ModalPopupExtender ID="mpeIndentItem" runat="server" PopupControlID="PanelIndentItem" TargetControlID="btnAddItem"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelIndentItem" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnClose" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center> <h5 id="myModalLabelcrate">Add Items</h5></center>
                </div>
                <div class="modal-body">
                   <div  class="row"> <div class="col-md-2">
                                    Recurring
       
                                </div>
                                <div class="col-md-4">
                                    <asp:CheckBox ID="chkRenewable" runat="server" AutoPostBack="true" OnCheckedChanged="chkRenewable_CheckedChanged"  />
                                </div>
                    </div>
                     <div visible="false" runat="server" id="TrAutomobiles">
                                <div  class="row">
                                     <div class="col-md-2">
                                        
                            Asset Type&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlAssetType_r" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddlAssetType_r_SelectedIndexChanged" CssClass="form-control">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            Asset Category&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlAssetCate_r" runat="server" OnSelectedIndexChanged="ddlAssetCate_r_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                                    </div>
                                <div class="row">
                                     <div class="col-md-2">
                                        Asset Name&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" InitialValue="-Select-" ControlToValidate="ddlAssetName" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlAssetName" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="-Select-" Value="-Select-"></asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                  
                                    </div>
                                    </div>
                                        


                      <div class="row">
                        <div class="col-md-2">
                            Budget Sector&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" InitialValue="-Select-" ControlToValidate="ddlBudgetSector" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>

                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlBudgetSector"  OnSelectedIndexChanged="ddlBudgetSector_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Category&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="-Select-" ControlToValidate="ddlCategory" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>

                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlCategory" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>


                    </div>


                    <div class="row">
                       
                        <div class="col-md-2">
                            Item&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="-Select-" ControlToValidate="ddlItem" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlItem" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                          <div runat="server" id="Totalrunninghrs" visible="false">
                                        <div class="col-md-2">
                                        Total Running Hours&nbsp;
                                    </div>
                                    <div class="col-md-4">
                                     <asp:TextBox runat="server" ID="txtTotalrunninghrs" Enabled="false" CssClass="form-control" ReadOnly="true"></asp:TextBox>       </div>
                                    </div>
                        </div>

                    <div class="row">
                          <div class="col-md-2">
                            BOQ
       
                        </div>
                        <div class="col-md-4">
                            <asp:RadioButtonList ID="rblBOQ" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblBOQ_SelectedIndexChanged" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Selected="True" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                    
                      
                        <div class="col-md-2">
                            BOQ No
       
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" Enabled="false" ID="txtBOQ" CssClass="form-control"></asp:TextBox>
                        </div></div>

                    <div class="row">
                           <div class="col-md-2">
                            UOM&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtUOM" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>


                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtUOM" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                        </div>
                    
                     
                        <div class="col-md-2">
                            Total Qty involved
                        </div>
                        <div class="col-md-4">

                            <asp:TextBox runat="server" ID="txtTotQty" CssClass="form-control"></asp:TextBox>
                        </div></div>


                    <div class="row">
                           <div class="col-md-2">
                           Total Qty Received &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAvailQty" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">

                            <asp:TextBox runat="server" ID="txtQtyAlready" Text="0" onPaste="javascript:return false" CssClass="form-control input-pos-float" Enabled="false"></asp:TextBox>
                        </div>

                    

                       
                        <div class="col-md-2">
                            Qty Available &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtAvailQty" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">

                            <asp:TextBox runat="server" ID="txtAvailQty" Text="0" onPaste="javascript:return false" CssClass="form-control input-pos-float" Enabled="false"></asp:TextBox>
                        </div></div>

                    <div class="row">
                         <div class="col-md-2">
                            Budgeted Qty               
                        </div>
                        <div class="col-md-4">

                            <asp:TextBox runat="server" ID="txtBudgetQty" Text="0" onPaste="javascript:return false" CssClass="form-control input-pos-float" Enabled="false"></asp:TextBox>
                        </div>

                    
                       
                        <div class="col-md-2">
                            Qty Required&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtReqQty" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" onkeyup="Calc();" ID="txtReqQty" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                          <asp:TextBox ID="txtExistingQty" runat="server" Text="0" style="display:none" ></asp:TextBox>
                         <asp:TextBox ID="txtProjBudQty" runat="server" Text="" style="display:none" ></asp:TextBox>
                         <asp:TextBox ID="txtTotalIndentQty" runat="server" Text="0" style="display:none" ></asp:TextBox>
                 </div>
                    <div class="row">      <div class="col-md-2">
                            Tentative date of requirement     
                        </div>
                        <div class="col-md-4">

                            <asp:TextBox runat="server" ID="txtTenativeDate" onpaste="javascript:return false;" onkeypress="javascript:return false;" CssClass="form-control"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender runat="server" ID="CalendarTentative" Format="dd-MM-yyyy" TargetControlID="txtTenativeDate"></ajaxToolkit:CalendarExtender>
                        </div>

                    

                       
                        <div class="col-md-2">
                            Qty Consumed < 60 days
                        </div>
                        <div class="col-md-4">
                            <asp:RadioButtonList ID="RdWhetherRQ" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Yes" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div> </div> 
                        <div class="row">  <div class="col-md-2">
                            Remarks   
                        </div>
                        <div class="col-md-8">

                            <asp:TextBox runat="server" ID="txtRemark" CssClass="form-control" Style="resize: none" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        </div>
                    
                </div>
                <div class="row">
                <div class="modal-footer">
                    <center>
           <asp:Button ID="btnSaveItem" runat="server" Text="Save" OnClick="btnSaveItem_Click"  CssClass="btn btn-default" ValidationGroup="ValAddItem"   />
                        <asp:Button ID="btnCancelItem" runat="server" Text="Cancel" OnClick="btnCancelItem_Click"   CssClass="btn btn-default" CausesValidation="false"   />
                 </center>

                </div>
                </div>
            </div>
        </div>

    </asp:Panel>
    <!-- ModalPopupExtender -->

</asp:Content>
