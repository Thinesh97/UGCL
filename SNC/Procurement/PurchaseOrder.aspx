<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Procurement/PurchaseOrder.aspx.cs" Inherits="PurchaseOrder" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {

            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
        });


        function ConfirmDelete() {
            if (confirm("Are you want to Delete this record?") == false) {
                return false;
            }
            return true;
        }

        function Ajaxfuncation() {
            var ajaxrequest = new XMLHttpRequest();
        }

    </script>



    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>
                Purchase Order
            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-2">PO No</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtPONo" Enabled="false" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    Date&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPODate" CssClass="Validation_Text" ValidationGroup="ValPO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtPODate" CssClass="form-control" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" runat="server" TabIndex="2"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="cal1" Format="dd-MM-yyyy" TargetControlID="txtPODate"></ajaxToolkit:CalendarExtender>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-2">
                    Project Name&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" InitialValue="-Select-" ControlToValidate="ddlProject" CssClass="Validation_Text" ValidationGroup="ValPO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlProject" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" TabIndex="3"></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Material Indent No&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="-Select-" ControlToValidate="ddlIndentNo" CssClass="Validation_Text" ValidationGroup="ValPO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlIndentNo" CssClass="form-control" TabIndex="4">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-2">Quotation No</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtQuotNo" CssClass="form-control" TabIndex="5"></asp:TextBox>
                </div>
                <div class="col-md-2">Delivery Schedule</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtDeliverySchedule" CssClass="form-control" autocomplete= "off" MaxLength="50" TabIndex="6"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">
                    Vendor&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="-Select-" ControlToValidate="ddlVendor" CssClass="Validation_Text" ValidationGroup="ValPO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlVendor" CssClass="form-control chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged" TabIndex="7">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">Vendor Ref</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtVendorRef" autocomplete= "off" CssClass="form-control" TabIndex="8"></asp:TextBox>
                    <%--<ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd-MM-yyyy" TargetControlID="txtVendorDate"></ajaxToolkit:CalendarExtender>--%>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">
                    Dispatch To&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDespatchAdvise" CssClass="Validation_Text" ValidationGroup="ValPO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtDespatchAdvise" CssClass="form-control" Style="resize: none" TextMode="MultiLine" TabIndex="9"></asp:TextBox>
                </div>
                <%--<div class="col-md-2">UGCL Ref.</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtUGCLRef" autocomplete= "off" onpaste="javascript:return false;" onkeypress="javascript:return false;" CssClass="form-control" TabIndex="7"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" Format="dd-MM-yyyy" TargetControlID="txtUGCLRef"></ajaxToolkit:CalendarExtender>
                </div>--%>
                <div class="col-md-2">Due Date</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtDueDate" autocomplete= "off" onpaste="javascript:return false;" onkeypress="javascript:return false;" CssClass="form-control" TabIndex="10"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender3" Format="dd-MM-yyyy" TargetControlID="txtDueDate"></ajaxToolkit:CalendarExtender>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-2">Contact Person</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtContactName" autocomplete= "off" CssClass="form-control" TabIndex="13"></asp:TextBox>
                </div>
                <div class="col-md-2">Contact Number</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtContactNo" MaxLength="10" autocomplete= "off" CssClass="form-control input-pos-int" TabIndex="14"></asp:TextBox>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-2">Payment Terms</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtPayTerms" autocomplete= "off" CssClass="form-control" TabIndex="15"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    Approved By&nbsp;
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" InitialValue="-Select-" ControlToValidate="ddlApprovedBy" CssClass="Validation_Text" ValidationGroup="ValPO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlApprovedBy" CssClass="form-control" TabIndex="16">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">
                    GST No.&nbsp;
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtTINNo" CssClass="Validation_Text" ValidationGroup="ValPO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtTINNo" Enabled="false" CssClass="form-control" MaxLength="17"></asp:TextBox>
                </div>
                <div class="col-md-2">Status</div>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="rblStatus" RepeatDirection="Horizontal" runat="server" TabIndex="18">
                        <asp:ListItem Text="Open" Value="Open" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Hold" Value="Hold"></asp:ListItem>
                        <asp:ListItem Text="Closed" Value="Closed"></asp:ListItem>
                        <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-2">Remarks</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" Style="resize: none" TextMode="MultiLine" TabIndex="19"></asp:TextBox>
                </div>
                <div class="col-md-2">Other Terms</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtOthers" CssClass="form-control" Style="resize: none" TextMode="MultiLine" TabIndex="20"></asp:TextBox>
                </div>
            </div>
            
            <div class="row" runat="server" id="div_Draft" visible="false">
                <div class="col-md-2">Draft</div>
                <div class="col-md-4">
                    <asp:CheckBox runat="server" ID="chkDraft" Text="Draft Confidential" AutoPostBack="true" OnCheckedChanged="chkDraft_ChckedChanged" TabIndex="21"></asp:CheckBox>
                </div>
            </div>

            <div class="row">
                <div runat="server" id="div_BeforeUpload" visible="false">
                    <div class="col-md-2">Upload File</div>
                    <div class="col-md-4">
                        <asp:FileUpload runat="server" ID="fuPODoc" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationGroup="FileSave"
                            ErrorMessage="PDF File Only" ForeColor="Red" ControlToValidate="fuPODoc"
                            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf)$">  
                        </asp:RegularExpressionValidator>
                    </div>
                </div>
                <div runat="server" id="div_AfterUpload" visible="false">
                    <div class="col-md-2">Uploaded File</div>
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="lnkDownloadFile" OnClick="lnkDownloadFile_Click"></asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />
            
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Label ID="lbl_data" runat="server" Visible="false"></asp:Label>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ValPO" OnClick="btnSubmit_Click" CssClass="btn btn-default" TabIndex="15"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default" TabIndex="16"></asp:Button>

                    <asp:Button ID="btnMailToVendor" runat="server" Text="Mail to Vendor" Visible="false" CssClass="btn btn-default" TabIndex="17"></asp:Button>
                    <a runat="server" id="btnPrint" visible="false" class="btn btn-default" tabindex="18">Print </a>
                    <asp:Button ID="btnPrintPDF" runat="server" Text="PDF Print" OnClick="btnPrintPDF_Click" CssClass="btn btn-default" tabindex="19" Visible="false"></asp:Button>
                </div>
            </div>
            <br />
            <br />
            
            <center>
                <asp:TextBox ID="txtTotal" Text="0" runat="server" Style="display: none"></asp:TextBox>

                <ogrid:Grid runat="server" ID="Grid_POItem" ShowColumnsFooter="true" OnDeleteCommand="Grid_POItem_DeleteCommand" 
                    OnRowCreated="Grid_POItem_RowCreated" OnRowDataBound="Grid_POItem_RowDataBound" AutoGenerateColumns="false"   
                    CallbackMode="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="20">
                    <ScrollingSettings ScrollWidth="100%" />
                    <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
                    <Columns>
            
                        <ogrid:Column DataField="Indent_No" HeaderText="Indent No" Width="100"></ogrid:Column>
                        <ogrid:Column DataField="Budget_Sector" HeaderText="Budget Sector" Width="150"></ogrid:Column>
                        <ogrid:Column DataField="Category_Name" HeaderText="Category"  Width="120"></ogrid:Column>
                        <ogrid:Column DataField="Item_Name" HeaderText="Item" Width="120"></ogrid:Column>
                        <ogrid:Column DataField="BOQ" HeaderText="BOQ" Width="80" >
                            <TemplateSettings TemplateId="BOQTemplate" />
                        </ogrid:Column>
                        <ogrid:Column DataField="BOQ_No" HeaderText="BOQ No" Width="100" ></ogrid:Column>
                        <ogrid:Column DataField="UOM" HeaderText="UOM" Width="100px" ></ogrid:Column>
                        <ogrid:Column DataField="Total_Qty_Involved" HeaderText="Qty involved in this"  Width="150" ></ogrid:Column>              
                        <ogrid:Column DataField="Qty_required" HeaderText="Required Qty" Align="right" Width="120"></ogrid:Column> 
                        <ogrid:Column DataField="Price" HeaderText="Price" Width="100" Align="right"></ogrid:Column>    
                        <ogrid:Column DataField="Amount" HeaderText="Amount" Width="120" Align="right"></ogrid:Column>            
                        <ogrid:Column DataField="Amt_With_Tax" HeaderText="Amt With Tax" Width="120" Align="right"></ogrid:Column>    
                        <ogrid:Column DataField="Referred_VendorCount" HeaderText="Referred Vendor" Width="135" Align="center"></ogrid:Column>             
                        <ogrid:Column DataField="Tentative_Date" HeaderText="Tentative Date of Requirement" Align="center" Width="200" DataFormatString="{0:dd-MM-yyy}"  ></ogrid:Column>
                        <ogrid:Column DataField="Whether_Req_Qty" HeaderText="Whether Req Qty"  Align="center" Width="140" >
                            <TemplateSettings TemplateId="WhetherReqQtyTemplate" />
                        </ogrid:Column> 
                        <ogrid:Column HeaderText="Edit"  Align="center" Width="80" >
                            <TemplateSettings TemplateId="QtyTemplate" />
                        </ogrid:Column> 
                        <ogrid:Column  HeaderText="Delete" AllowDelete="true" Width="100" ></ogrid:Column>                     
                        <ogrid:Column DataField="PO_Item_Id"  HeaderText="PO_Item_Id"  Visible="false" Width="100" ></ogrid:Column>       
                        <ogrid:Column  DataField="Budget_Sector_ID" Visible="false" ></ogrid:Column>     
               
                    </Columns>
                
                    <Templates>
                        <ogrid:GridTemplate ID="BOQTemplate" runat="server">
                            <Template>
                                <asp:Label ID="lblBOQ" runat="server" Text='<%#Container.DataItem["BOQ"].ToString() != string.Empty && Container.DataItem["BOQ"].ToString() == "True" ? "Yes" : "No" %>'></asp:Label>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate ID="WhetherReqQtyTemplate" runat="server">
                            <Template>
                                <asp:Label ID="lblWhetherReqQty" runat="server" Text='<%#Container.DataItem["Whether_Req_Qty"].ToString() == "True" ? "Yes" :"No" %>'></asp:Label>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate ID="QtyTemplate" runat="server">
                            <Template>
                            <asp:LinkButton ID="lnkEditQty" Text="Edit" CommandArgument='<%#Container.DataItem["Qty_required"] %>' CommandName='<%#Container.DataItem["PO_Item_Id"] %>'  OnClick="lnkEditQty_Click" runat="server" CssClass="gridCB">
                                        </asp:LinkButton>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>

                </ogrid:Grid>
           
                <br />
                <br />


                <ogrid:Grid ID="GridItemWiseTax" runat="server"  CallbackMode="false"  AutoGenerateColumns="false" AllowPaging="true" PageSize="15" AllowAddingRecords="false">
                    <ScrollingSettings ScrollWidth="100%" />
                    <Columns>
                        <ogrid:Column DataField="Item_Name" HeaderText="Item Name" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="ItemCode" HeaderText="Item Code" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Type" HeaderText="Type"></ogrid:Column>
                        <ogrid:Column DataField="Description" HeaderText="Description" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="TypePerc" HeaderText="Percentage" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="TypeAmt" HeaderText="Amount" ReadOnly="true"></ogrid:Column>                        
                    </Columns>
                </ogrid:Grid>

                <br />
                <br />

                <ogrid:Grid ID="Grid_Tax" runat="server" ShowColumnsFooter="true" 
                    OnRowCreated="Grid_Tax_RowCreated" CallbackMode="false" OnRowDataBound="Grid_Tax_RowDataBound" 
                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10" AllowAddingRecords="false">
                       
                    <ScrollingSettings ScrollWidth="65%" />
                    <Columns>
                        <ogrid:Column DataField="Type" HeaderText="Type"></ogrid:Column>
                        <ogrid:Column DataField="Description" HeaderText="Description" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Type_Perc" HeaderText="Percentage" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Type_Amount" HeaderText="Amount" ReadOnly="true"></ogrid:Column> 

                        <ogrid:Column HeaderText="Edit"  Align="center" Width="80" >
                            <TemplateSettings TemplateId="QuantityTemplate" />
                        </ogrid:Column> 
                        <ogrid:Column DataField="PO_Tax_ID"  HeaderText="PO_Tax_ID"  Visible="false" Width="100" ></ogrid:Column>   
                        </Columns>
                        <Templates>
                            <ogrid:GridTemplate ID="QuantityTemplate" runat="server">
                                <Template>
                                    <asp:LinkButton ID="lnkEditamt" Text="Edit" CommandArgument='<%#Container.DataItem["Type_Amount"] %>' CommandName='<%#Container.DataItem["PO_Tax_ID"] %>'  OnClick="lnkEditamt_Click" runat="server" CssClass="gridCB">
                                    </asp:LinkButton>
                                </Template>
                            </ogrid:GridTemplate> 
                        </Templates>
                    </ogrid:Grid>
             
                </center>
            <br />
            <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        </div>

    </div>

    <div class="panel panel-default" runat="server" id="divPOapproval" visible="false">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Approval

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->
            <div class="row">
                <div class="col-md-2">
                    Status&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ControlToValidate="rdoStatus" CssClass="Validation_Text" ValidationGroup="POApproval" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:RadioButtonList runat="server" ID="rdoStatus">
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                        <asp:ListItem Text="Send Back For Modification" Value="SendBackForModification"></asp:ListItem>
                        <asp:ListItem Text="Reject" Value="Reject"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="col-md-2">
                    Date &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ControlToValidate="txtDate" CssClass="Validation_Text" ValidationGroup="POApproval" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtDate" Enabled="false" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="calDecision" Format="dd-MM-yyyy" TargetControlID="txtDate"></ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">Comments </div>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtComments" Style="resize: none" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>

                </div>
            </div>
            <div class="row">
                <center>
                      <asp:Button ID="btnDecision" runat="server" OnClick="btnDecision_Click" Text="Decision" ValidationGroup="POApproval" CssClass="btn btn-default" TabIndex="16"></asp:Button>
                    <asp:Button ID="btnApprovalCancel" runat="server" Text="Cancel" OnClick="btnApprovalCancel_Click" CssClass="btn btn-default" TabIndex="17"></asp:Button>
                </center>
            </div>
        </div>
    </div>

    <%--    Edit Qty POP UP--%>

    <asp:Button runat="server" ID="BtnTarget" Style="display: none"></asp:Button>
    <ajaxToolkit:ModalPopupExtender ID="ModelPOQty" runat="server" PopupControlID="PanelEditQty" TargetControlID="BtnTarget"
        CancelControlID="BtnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="PanelEditQty" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="Button" id="BtnClose" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>   <h5 id="myModalTax1"><asp:Label ID="Label1" runat="server" Text="Edit Quantity"></asp:Label></h5></center>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-2">
                            Quantity&nbsp;
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPOQty" CssClass="Validation_Text" ValidationGroup="ValSavePOQty" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">

                            <asp:TextBox ID="txtPOQty" runat="server" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <center>
                        
         <asp:Button ID="btnPoQtySave" runat="server" Text="Save"  CssClass="btn btn-default" ValidationGroup="ValSavePOQty" OnClick="btnPoQtySave_Click"  />
             <asp:Button ID="btnCancelPoQty" runat="server"  Text="Cancel"  CssClass="btn btn-default" CausesValidation="false"    />        
                 </center>

                </div>
            </div>
        </div>

    </asp:Panel>

    <asp:Button runat="server" ID="BtnTarget1" Style="display: none"></asp:Button>
    <ajaxToolkit:ModalPopupExtender ID="ModalOverallAmt" runat="server" PopupControlID="PnlEditAmount" TargetControlID="BtnTarget1"
        CancelControlID="BtnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PnlEditAmount" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btncloseModal" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>   <h5 id="myModalamt1"><asp:Label ID="Label2" runat="server" Text="Edit Amount"></asp:Label></h5></center>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-2">
                            Amount&nbsp;
                     <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtOverallTaxAmount" CssClass="Validation_Text" ValidationGroup="ValSavePOAmount" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-md-4">

                            <asp:TextBox ID="txtOverallTaxAmount" runat="server" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <center>
                        
         <asp:Button ID="btnUpdateOverallAmt" runat="server" Text="Save"  CssClass="btn btn-default" OnClick="btnUpdateOverallAmt_Click"  />
             <asp:Button ID="Button2" runat="server"  Text="Cancel"  CssClass="btn btn-default" CausesValidation="false"    />        
                 </center>

                </div>
            </div>
        </div>

    </asp:Panel>
    
    <script type="text/javascript">
        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }
    </script>

    <script type="text/javascript">
        //On Page Load
        $(".chzn-select").chosen({ search_contains: true });
        $(".chzn-select-deselect").chosen({ allow_single_deselect: true });


        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $(".chzn-select").chosen({ search_contains: true });
                    $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
                }
            });
        };

    </script>

</asp:Content>
