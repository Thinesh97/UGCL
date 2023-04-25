<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Procurement/DirectPurchaseOrder.aspx.cs" Inherits="DirectPurchaseOrder" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
            $(".input-pos-float-3decimal").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,3}$/ });
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

        function CalculateItemAmt() {
            var Qty = document.getElementById("<%=txtQty.ClientID%>").value;
            var Price = document.getElementById("<%=txtPrice.ClientID%>").value;
            var Amount = parseFloat(Qty) * parseFloat(Price);
            document.getElementById("<%=txtAmount.ClientID%>").value = Amount.toFixed(2);
        }

        function CalculateItemTaxAmt() {

            var itemAmt = document.getElementById("<%=txtAmount.ClientID%>").value;
            var igstPerc = document.getElementById("<%=txtIgstPerc.ClientID%>").value;
            var cgstPerc = document.getElementById("<%=txtCgstPerc.ClientID%>").value;
            var sgstPerc = document.getElementById("<%=txtSgstPerc.ClientID%>").value;

            var igstAmt = parseFloat(parseFloat(itemAmt) * parseFloat(igstPerc)) / 100;
            var cgstAmt = parseFloat(parseFloat(itemAmt) * parseFloat(cgstPerc)) / 100;
            var sgstAmt = parseFloat(parseFloat(itemAmt) * parseFloat(sgstPerc)) / 100;

            document.getElementById("<%=txtIgstAmt.ClientID%>").value = igstAmt.toFixed(2);
            document.getElementById("<%=txtCgstAmt.ClientID%>").value = cgstAmt.toFixed(2);
            document.getElementById("<%=txtSgstAmt.ClientID%>").value = sgstAmt.toFixed(2);
        }

        function CalculateItemTaxAmtPO(txt1, txt2) {
            debugger;
            /*   var totalAmtPO = 50;*/
            <%-- var qty = document.getElementById("<%=txtQty.ClientID%>").value;
            var price = document.getElementById("<%=txtPrice.ClientID%>").value;
            var totalAmtPO = parseFloat(parseFloat(price) * parseFloat(qty))--%>

            <%--var Qty = document.getElementById("<%=txtQty.ClientID%>").value;
            var Price = document.getElementById("<%=txtPrice.ClientID%>").value;
            var totalAmtPO = parseFloat(Qty) * parseFloat(Price);--%>
           <%--document.getElementById("<%=txtAmount.ClientID%>").value = totalAmtPO.toFixed(2);--%>

            var totalAmtPO = document.getElementById("<%=txtTotal.ClientID%>").value;

            var percPO = document.getElementById('ContentPlaceHolder1_' + txt1).value;
            var amtPO = parseFloat(parseFloat(totalAmtPO) * parseFloat(percPO)) / 100;
            document.getElementById('ContentPlaceHolder1_' + txt2).value = amtPO.toFixed(2);

            <%--<%--var igstPercPO = document.getElementById("<%=txtIgstPercPO.ClientID%>").value;
            var cgstPercPO = document.getElementById("<%=txtCgstPercPO.ClientID%>").value;
            var sgstPercPO = document.getElementById("<%=txtSgstPercPO.ClientID%>").value;

            var igstAmtPO = parseFloat(parseFloat(totalAmtPO) * parseFloat(igstPercPO)) / 100;
            var cgstAmtPO = parseFloat(parseFloat(totalAmtPO) * parseFloat(cgstPercPO)) / 100;
            var sgstAmtPO = parseFloat(parseFloat(totalAmtPO) * parseFloat(sgstPercPO)) / 100;

            document.getElementById("<%=txtIgstAmtPO.ClientID%>").value = igstAmtPO.toFixed(2);
            document.getElementById("<%=txtCgstAmtPO.ClientID%>").value = cgstAmtPO.toFixed(2);
            document.getElementById("<%=txtSgstAmtPO.ClientID%>").value = sgstAmtPO.toFixed(2)--%>;
        }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.chosen-select').chosen();
        });
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

                Direct Purchase Order

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-2">PO No</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtPONo" Enabled="false" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>
                <div class="col-md-2">Financial Year </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlFYear" CssClass="form-control" TabIndex="2">
                        <asp:ListItem Value="0" Text="-Select-" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="15-16" Text="2015-2016"></asp:ListItem>
                        <asp:ListItem Value="16-17" Text="2016-2017"></asp:ListItem>
                        <asp:ListItem Value="17-18" Text="2017-2018"></asp:ListItem>
                        <asp:ListItem Value="18-19" Text="2018-2019"></asp:ListItem>
                        <asp:ListItem Value="19-20" Text="2019-2020"></asp:ListItem>
                        <asp:ListItem Value="20-21" Text="2020-2021"></asp:ListItem>
                        <asp:ListItem Value="21-22" Text="2021-2022"></asp:ListItem>
                        <asp:ListItem Value="22-23" Text="2022-2023"></asp:ListItem>
                        <asp:ListItem Value="23-24" Text="2023-2024"></asp:ListItem>
                        <asp:ListItem Value="24-25" Text="2024-2025"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    Month
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlMonth" CssClass="form-control" TabIndex="3">
                        <asp:ListItem Value="0" Text="-Select-" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="JAN" Text="January"></asp:ListItem>
                        <asp:ListItem Value="FEB" Text="February"></asp:ListItem>
                        <asp:ListItem Value="MAR" Text="March"></asp:ListItem>
                        <asp:ListItem Value="APR" Text="April"></asp:ListItem>
                        <asp:ListItem Value="MAY" Text="May"></asp:ListItem>
                        <asp:ListItem Value="JUN" Text="June"></asp:ListItem>
                        <asp:ListItem Value="JUL" Text="July"></asp:ListItem>
                        <asp:ListItem Value="AUG" Text="August"></asp:ListItem>
                        <asp:ListItem Value="SEP" Text="September"></asp:ListItem>
                        <asp:ListItem Value="OCT" Text="October"></asp:ListItem>
                        <asp:ListItem Value="NOV" Text="November"></asp:ListItem>
                        <asp:ListItem Value="DEC" Text="December"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Date&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPODate" CssClass="Validation_Text" ValidationGroup="ValPO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtPODate" CssClass="form-control" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" runat="server" TabIndex="4"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="cal1" Format="dd-MM-yyyy" TargetControlID="txtPODate"></ajaxToolkit:CalendarExtender>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-2">
                    RFQ No&nbsp;
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlrfqno"  class="chosen-select form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlrfqno_SelectedIndexChanged"  TabIndex="5"></asp:DropDownList>
                </div>
            </div>

            <div class="row">
                 <div class="col-md-2">
                    Project Name&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" InitialValue="-Select-" ControlToValidate="ddlProject" CssClass="Validation_Text" ValidationGroup="ValPO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlProject"  class="chosen-select form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" TabIndex="6"></asp:DropDownList>
                </div>
                <div class="col-md-2">Delivery Schedule</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtDeliverySchedule" CssClass="form-control" autocomplete= "off" MaxLength="50" TabIndex="5"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">
                    Vendor&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="-Select-" ControlToValidate="ddlVendor" CssClass="Validation_Text" ValidationGroup="ValPO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlVendor" class="chosen-select form-control"  AutoPostBack="true" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged" TabIndex="7">
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlDispatchTo" CssClass="Validation_Text" ValidationGroup="ValPO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlDispatchTo" class="chosen-select form-control"  AutoPostBack="true" OnSelectedIndexChanged="ddlDispatchTo_SelectedIndexChanged" TabIndex="7">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
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
                    <asp:TextBox runat="server" ID="txtPayTerms" TextMode="MultiLine" Style="resize: none" CssClass="form-control" TabIndex="15"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    Approved By&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" InitialValue="-Select-" ControlToValidate="ddlApprovedBy" CssClass="Validation_Text" ValidationGroup="ValPO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlApprovedBy" class="chosen-select form-control"  TabIndex="16">
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
                    <asp:TextBox runat="server" ID="txtTINNo" Enabled="false" CssClass="form-control" MaxLength="20"></asp:TextBox>
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
                <div class="col-md-2">Dispatch Mode</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtDispatchMode" autocomplete= "off" CssClass="form-control" TabIndex="19"></asp:TextBox>
                </div>
                <div class="col-md-2">TDS U/S 194 Q</div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlTDSPerc" CssClass="form-control" TabIndex="20">
                        <asp:ListItem Text="0.0" Value="0.00" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="0.1" Value="0.10"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

             <div class="row">
                <div class="col-md-2">Place of Dispatch</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtPlaceofDispatch" autocomplete= "off" CssClass="form-control" TabIndex="22"></asp:TextBox>
                </div>
                <div class="col-md-2">Destination</div>
               <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtDestination" autocomplete= "off" CssClass="form-control" TabIndex="23"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">Remarks</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" TextMode="MultiLine" TabIndex="24"></asp:TextBox>
                </div>
                <div class="col-md-2">Other Terms</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtOthers" CssClass="form-control" TextMode="MultiLine" TabIndex="25"></asp:TextBox>
                </div>
            </div>

            <div class="row" runat="server" id="div_Draft" visible="false">
                <div class="col-md-2">Draft</div>
                <div class="col-md-4">
                    <asp:CheckBox runat="server" ID="chkDraft" Text="Draft Confidential" AutoPostBack="true" OnCheckedChanged="chkDraft_ChckedChanged" TabIndex="26"></asp:CheckBox>
                </div>
                <div runat="server" id="ApproverComments" visible="false">
                  <div class="col-md-2">Approver Comments</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtApproverComments" CssClass="form-control" TextMode="MultiLine" TabIndex="25"></asp:TextBox>
                </div>
                     </div>
            </div>

            <div class="row">
                <div runat="server" id="div_BeforeUpload" visible="false">
                    <div class="col-md-2">Upload File</div>
                    <div class="col-md-4">
                        <asp:FileUpload runat="server" ID="fuDPODoc" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationGroup="FileSave"
                            ErrorMessage="PDF File Only" ForeColor="Red" ControlToValidate="fuDPODoc"
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
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ValPO" OnClick="btnSubmit_Click" CssClass="btn btn-default" TabIndex="20"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default" TabIndex="21"></asp:Button>
                    <asp:Button ID="btnAddItem" Text="Add Item" CssClass="btn btn-default" runat="server" OnClick="btnAddItem_Click" CausesValidation="false" Visible="false" TabIndex="22" />
                    <asp:Button ID="btnAddTax" Text="Add Tax" CssClass="btn btn-default" runat="server" OnClick="btnAddTax_Click" CausesValidation="false" Visible="false" TabIndex="23" />
                    <a runat="server" id="btnPrint" visible="false" class="btn btn-default" tabindex="24">Print </a>
                    <asp:Button ID="btnPrintPDF" runat="server" Text="PDF Print" OnClick="btnPrintPDF_Click" CssClass="btn btn-default" tabindex="25" Visible="false"></asp:Button>
                 <asp:Button ID="btnApprove" Text="Approve" CssClass="btn btn-default" runat="server" OnClick="btnApprove_Click" CausesValidation="false" Visible="false" TabIndex="22" />
                </div>
            </div>
            <br />
            <br />
            
            <center>
                <asp:TextBox ID="txtTotal" Text="0" runat="server" Style="display:none "></asp:TextBox>

                <ogrid:Grid runat="server" ID="Grid_DirectPOItem" ShowColumnsFooter="true" OnDeleteCommand="Grid_DirectPOItem_DeleteCommand" 
                    OnRowDataBound="Grid_DirectPOItem_RowDataBound" OnRowCreated="Grid_DirectPOItem_RowCreated" AutoGenerateColumns="false"   
                    CallbackMode="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="20">
                    <ScrollingSettings ScrollWidth="100%" />
                    <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
                    <Columns>
                          <ogrid:Column DataField="Item_Name" HeaderText="Item Name" Width="180" Wrap="true">
                            <TemplateSettings TemplateId="ItemTemplate" />
                        </ogrid:Column>
                        <ogrid:Column DataField="Category_Name" HeaderText="Category" Width="120" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="UOM" HeaderText="UOM" Width="100px" ></ogrid:Column>
                        <ogrid:Column DataField="Qty_required" HeaderText="Quantity" Align="right" Width="120"></ogrid:Column> 
                        <ogrid:Column DataField="Price" HeaderText="Price" Width="100" Align="right"></ogrid:Column>    
                        <ogrid:Column DataField="Amount" HeaderText="Amount" Width="120" Align="right"></ogrid:Column>
                        <ogrid:Column DataField="Igst_Amt" HeaderText="IGST" Width="100" Align="right"></ogrid:Column>
                        <ogrid:Column DataField="Cgst_Amt" HeaderText="CGST" Width="100" Align="right"></ogrid:Column>
                        <ogrid:Column DataField="Sgst_Amt" HeaderText="SGST" Width="100" Align="right"></ogrid:Column>
                                   
                        <ogrid:Column DataField="Amt_With_Tax" HeaderText="Amt With Tax" Width="120" Align="right"></ogrid:Column>    
                        <ogrid:Column  HeaderText="Delete" AllowDelete="true" Width="100" ></ogrid:Column>                     
                        <ogrid:Column DataField="PO_Item_Id"  HeaderText="PO_Item_Id"  Visible="false" Width="100" ></ogrid:Column>       
                    </Columns>
                
                    <Templates>
                        <ogrid:GridTemplate ID="ItemTemplate" runat="server">
                            <Template>
                               <asp:LinkButton ID="lnkPOItem" Text='<%#Container.DataItem["Item_Name"] %>' CommandArgument='<%#Container.DataItem["Item_Type"] %>' CommandName='<%#Container.DataItem["PO_Item_Id"] %>'  OnClick="lnkWOItem_Click" runat="server" CssClass="gridCB">
                                        </asp:LinkButton>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>

                </ogrid:Grid>
           
                <br />
                <br />

                <ogrid:Grid runat="server" ID="Grid_DirectPOTax" ShowColumnsFooter="true" OnDeleteCommand="Grid_DirectPOTax_DeleteCommand" 
                     AutoGenerateColumns="false"   
                    CallbackMode="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="5">
                    <ScrollingSettings ScrollWidth="80%" />
                    <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
                    <Columns>
                        
                        <ogrid:Column DataField="Type" HeaderText="Type" Width="250px"></ogrid:Column>
                        <ogrid:Column DataField="Description" HeaderText="Desccription" Width="150px"></ogrid:Column>
                        <ogrid:Column DataField="Type_Perc" HeaderText="Percentage" Width="150px" Align="right"></ogrid:Column>
                        <ogrid:Column DataField="Type_Amount" HeaderText="Amount" Width="150px" Align="right"></ogrid:Column>
                        <ogrid:Column  HeaderText="Delete" AllowDelete="true"></ogrid:Column>                     
                        <ogrid:Column DataField="PO_Tax_Id"  HeaderText="PO_Tax_Id"  Visible="false"></ogrid:Column>       
                    </Columns>
                </ogrid:Grid>
             
                </center>
            <br />
            <center>
                <asp:TextBox ID="txtTotalRFQ" Text="0" runat="server" Style="display: none"></asp:TextBox>

                <ogrid:Grid runat="server" ID="Grid_DirectRFQItem" ShowColumnsFooter="true" OnDeleteCommand="Grid_DirectRFQItem_DeleteCommand" 
                    OnRowDataBound="Grid_DirectRFQItem_RowDataBound" OnRowCreated="Grid_DirectRFQItem_RowCreated" AutoGenerateColumns="false"   
                    CallbackMode="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="20">
                    <ScrollingSettings ScrollWidth="100%" />
                    <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
                    <Columns>
                          <ogrid:Column DataField="Item_Name" HeaderText="Item Name" Width="180" Wrap="true">
                            <TemplateSettings TemplateId="ItemTemplateRFQ" />
                        </ogrid:Column>
                        <ogrid:Column DataField="Category_Name" HeaderText="Category" Width="120" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="UOMPrefix" HeaderText="UOM" Width="100px" ></ogrid:Column>
                        <ogrid:Column DataField="Qty_required" HeaderText="Quantity" Align="right" Width="120"></ogrid:Column> 
                        <ogrid:Column DataField="Price" HeaderText="Price" Width="100" Align="right"></ogrid:Column>    
                        <ogrid:Column DataField="Amount" HeaderText="Amount" Width="120" Align="right"></ogrid:Column>
                        <ogrid:Column DataField="Igst_Amt" HeaderText="IGST" Width="100" Align="right"></ogrid:Column>
                        <ogrid:Column DataField="Cgst_Amt" HeaderText="CGST" Width="100" Align="right"></ogrid:Column>
                        <ogrid:Column DataField="Sgst_Amt" HeaderText="SGST" Width="100" Align="right"></ogrid:Column>
                                   
                        <ogrid:Column DataField="Amt_With_Tax" HeaderText="Amt With Tax" Width="120" Align="right"></ogrid:Column>    
                        <ogrid:Column  HeaderText="Delete" AllowDelete="true" Width="100" ></ogrid:Column>                     
                        <ogrid:Column DataField="RFQ_Item_Id"  HeaderText="RFQ_Item_Id"  Visible="false" Width="100" ></ogrid:Column>       
                    </Columns>
                
                    <Templates>
                        <ogrid:GridTemplate ID="ItemTemplateRFQ" runat="server">
                            <Template>
                               <asp:LinkButton ID="lnkRFQItem" Text='<%#Container.DataItem["Item_Name"] %>' CommandArgument='<%#Container.DataItem["Item_Type"] %>' CommandName='<%#Container.DataItem["RFQ_Item_Id"] %>'  OnClick="lnkRFQItem_Click" runat="server" CssClass="gridCB">
                                        </asp:LinkButton>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>

                </ogrid:Grid>
           
                <br />
                <br />

                <ogrid:Grid runat="server" ID="Grid_DirectRFQTax" ShowColumnsFooter="true" OnDeleteCommand="Grid_DirectRFQTax_DeleteCommand" 
                     AutoGenerateColumns="false"   
                    CallbackMode="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="5">
                    <ScrollingSettings ScrollWidth="80%" />
                    <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
                    <Columns>
                        
                        <ogrid:Column DataField="Type" HeaderText="Type" Width="250px"></ogrid:Column>
                        <ogrid:Column DataField="Description" HeaderText="Desccription" Width="150px"></ogrid:Column>
                        <ogrid:Column DataField="Type_Perc" HeaderText="Percentage" Width="150px" Align="right"></ogrid:Column>
                        <ogrid:Column DataField="Type_Amount" HeaderText="Amount" Width="150px" Align="right"></ogrid:Column>
                        <ogrid:Column  HeaderText="Delete" AllowDelete="true"></ogrid:Column>                     
                        <ogrid:Column DataField="RFQ_Tax_Id"  HeaderText="RFQ_Tax_Id"  Visible="false"></ogrid:Column>       
                    </Columns>
                </ogrid:Grid>
             
                </center>
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

    <%--Add Items --%>

    <asp:Button ID="btnDummy" runat="server" Style="display: none" Text="Button" />

    <ajaxToolkit:ModalPopupExtender ID="ModelItemPopup" runat="server" PopupControlID="PanelItem" TargetControlID="btnDummy"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelItem" runat="server" align="center" Style="display: none">

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnClose" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>   <h5 id="myModalLabelcrate"><asp:Label ID="lblAddItems" runat="server" Text="Add Items"></asp:Label></h5></center>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-2">
                            Category &nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCategory" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlCategory" class="chosen-select form-control" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Item&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlItem" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlItem" class="chosen-select form-control" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            UOM&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtUOM" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Qty&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtQty" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtQty" onkeyup="CalculateItemAmt(), CalculateItemTaxAmt()" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float-3decimal"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            Price&nbsp;
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPrice" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtPrice" onkeyup="CalculateItemAmt(), CalculateItemTaxAmt()" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Amount&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtAmount" Enabled="false" onpaste="javascript: return false" autocomplete="off" Text="0.00" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            IGST Perc&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtIgstPerc" runat="server" onkeyup="CalculateItemTaxAmt()" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Amount
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtIgstAmt" Enabled="false" runat="server" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            CGST Perc&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtCgstPerc" runat="server" onkeyup="CalculateItemTaxAmt()" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Amount&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtCgstAmt" Enabled="false" runat="server" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            SGST Perc&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtSgstPerc" runat="server" onkeyup="CalculateItemTaxAmt()" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Amount&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtSgstAmt" Enabled="false" runat="server" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>
                    
                </div>
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnSaveItem" runat="server" Text="Save"  CssClass="btn btn-default" ValidationGroup="ValAddItem" OnClick="btnSaveItem_Click"    />
                        <asp:Button ID="btnCancelItem" runat="server"  Text="Cancel"   CssClass="btn btn-default" CausesValidation="false" OnClick="btnCancelItem_Click" />
                 </center>

                </div>
            </div>

        </div>

    </asp:Panel>

    <asp:Button ID="Button1" runat="server" Style="display: none" Text="Button" />

    <ajaxToolkit:ModalPopupExtender ID="ModelTaxPopup" runat="server" PopupControlID="PanelTax" TargetControlID="Button1"
        CancelControlID="btnClose1" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelTax" runat="server" align="center" Style="display: none">

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnClose1" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>   <h5 >Add Tax</h5></center>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-12">
                            <asp:RadioButtonList runat="server" ID="rbtntype" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbtntype_SelectedIndexChanged">
                                <asp:ListItem Text="IGST Tax" Value="IGST" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="CGST Tax" Value="CGST"></asp:ListItem>
                                <asp:ListItem Text="SGST Tax" Value="SGST"></asp:ListItem>
                                <asp:ListItem Text="Transport" Value="Transport"></asp:ListItem>
                                <asp:ListItem Text="Packing & Forwarding Cost" Value="PackingForwardingCost"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row" runat="server" id="div_Igst">
                        <div class="col-md-2">
                            IGST Perc&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtIgstPercPO" runat="server" onkeyup="CalculateItemTaxAmtPO('txtIgstPercPO','txtIgstAmtPO')" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Amount
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtIgstAmtPO" Enabled="false" runat="server" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" runat="server" id="div_Cgst" visible="false">
                        <div class="col-md-2">
                            CGST Perc&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtCgstPercPO" runat="server" onkeyup="CalculateItemTaxAmtPO('txtCgstPercPO','txtCgstAmtPO')" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Amount&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtCgstAmtPO" Enabled="false" runat="server" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" runat="server" id="div_Sgst" visible="false">
                        <div class="col-md-2">
                            SGST Perc&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtSgstPercPO" runat="server" onkeyup="CalculateItemTaxAmtPO('txtSgstPercPO','txtSgstAmtPO')" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Amount&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtSgstAmtPO" Enabled="false" runat="server" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row" runat="server" id="div_Packing" visible="false">
                        <div class="col-md-6">
                            Packing & Forwarding Cost &nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtPackingCost" runat="server" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" runat="server" id="div_Transport" visible="false">
                        <div class="col-md-6">
                            Transport Cost &nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtTransportCost" runat="server" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnSaveTax" runat="server" Text="Save" CssClass="btn btn-default" OnClick="btnSaveTax_Click" />
                        <asp:Button ID="BtnCanelTax" runat="server" Text="Cancel" CssClass="btn btn-default" CausesValidation="false" OnClick="btnCancelTax_Click" />
                 </center>

                </div>
            </div>

        </div>

    </asp:Panel>
    
    <asp:Button ID="btnDummyrfq" runat="server" Style="display: none" Text="Button" />

    <ajaxToolkit:ModalPopupExtender ID="ModelRFQItemPopup" runat="server" PopupControlID="PanelRFQItem" TargetControlID="btnDummyrfq"
        CancelControlID="btnClose01" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelRFQItem" runat="server" align="center" Style="display: none">

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnClose01" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>   <h5 id="myModalLabelcrate1"><asp:Label ID="Label1" runat="server" Text="Add Items"></asp:Label></h5></center>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-2">
                            Category &nbsp;
                            
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlCategoryrfq" Visible="false"  OnSelectedIndexChanged="ddlCategoryrfq_SelectedIndexChanged" class="chosen-select form-control" Enabled="false" AutoPostBack="true">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Item&nbsp;
                            
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlItemrfq" Visible="false" class="chosen-select form-control" OnSelectedIndexChanged="ddlItemrfq_SelectedIndexChanged" Enabled="false" AutoPostBack="true">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            UOM&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtUOMrfq" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Qty&nbsp;
                           
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtQtyrfq" onkeyup="CalculateItemAmt(), CalculateItemTaxAmt()" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float-3decimal"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            Price&nbsp;
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtPrice" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtPricerfq" onkeyup="CalculateItemAmt(), CalculateItemTaxAmt()" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Amount&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtAmountrfq" ReadOnly="true" onpaste="javascript: return false" autocomplete="off" Text="0.00" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            IGST Perc&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtIgstPercrfq" runat="server" ReadOnly="true" onkeyup="CalculateItemTaxAmt()" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Amount
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtIgstAmtrfq" ReadOnly="true"  runat="server" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            CGST Perc&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtCgstPercrfq" ReadOnly="true" runat="server" onkeyup="CalculateItemTaxAmt()" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Amount&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtCgstAmtrfq" ReadOnly="true" runat="server" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            SGST Perc&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtSgstPercrfq" runat="server" ReadOnly="true" onkeyup="CalculateItemTaxAmt()" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Amount&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtSgstAmtrfq" ReadOnly="true" runat="server" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                        <div class="col-md-0">
                            <asp:TextBox ID="txtmatcatid" ReadOnly="true" runat="server" autocomplete="off" Text="0.00"  CssClass="form-control input-pos-float" Visible="false"></asp:TextBox>
                        </div>
                    </div>
                    
                </div>
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnSaveItemrfq" runat="server" Text="Save" CausesValidation="False"   CssClass="btn btn-default" ValidationGroup="ValAddItem" OnClick="btnSaveItemrfq_Click"    />
                        <asp:Button ID="btnCancelItemrfq" runat="server"  Text="Cancel"   CssClass="btn btn-default" CausesValidation="false" OnClick="btnCancelItemrfq_Click" />
                 </center>

                </div>
            </div>

        </div>

    </asp:Panel>
</asp:Content>
