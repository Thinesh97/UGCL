<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="ProffessionalServiceOrder.aspx.cs" Inherits="ProffessionalServiceOrder" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {

            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });

        });

        function ConfirmDelete() {
            if (confirm("Are you sure want to delete this record?") == false) {
                return false;
            }
            return true;
        }

        function ShowHideTax(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    chks[i].checked = false;
                }
            }
            debugger;
            if (chk.value == "Inclusive") {
                $('#div_TaxType').show();
            } else {
                $('#div_TaxType').hide();
            }
            document.getElementById("<%=txtIgstPerc.ClientID%>").value = "0.00";
            document.getElementById("<%=txtIgstAmt.ClientID%>").value = "0.00";
            document.getElementById("<%=txtCgstPerc.ClientID%>").value = "0.00";
            document.getElementById("<%=txtCgstAmt.ClientID%>").value = "0.00";
            document.getElementById("<%=txtSgstPerc.ClientID%>").value = "0.00";
            document.getElementById("<%=txtSgstAmt.ClientID%>").value = "0.00";
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
               Proffessional Work Order
            </h3>

        </div>
        
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-2">PSO No</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtPSONo" Enabled="false" CssClass="form-control" MaxLength="50" TabIndex="1"></asp:TextBox>
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPSODate" CssClass="Validation_Text" ValidationGroup="ValWO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtPSODate" CssClass="form-control" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" runat="server" TabIndex="4"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="cal1" Format="dd-MM-yyyy" TargetControlID="txtPSODate"></ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    Project Name&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" InitialValue="-Select-" ControlToValidate="ddlProject" CssClass="Validation_Text" ValidationGroup="ValWO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlProject" class="chosen-select form-control" TabIndex="5"></asp:DropDownList>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-2">
                    Sub-Contractor&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="-Select-" ControlToValidate="ddlSubContractor" CssClass="Validation_Text" ValidationGroup="ValWO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlSubContractor" class="chosen-select form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSubContractor_SelectedIndexChanged" TabIndex="5">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    PSO Type&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="-Select-" ControlToValidate="ddlPSOType" CssClass="Validation_Text" ValidationGroup="ValWO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlPSOType" CssClass="form-control" TabIndex="6"></asp:DropDownList>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">
                    Work Location&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtWorkLocation" CssClass="Validation_Text" ValidationGroup="ValWO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtWorkLocation" CssClass="form-control" Style="resize: none" TextMode="MultiLine" TabIndex="7"></asp:TextBox>
                </div>
                <div class="col-md-2">Duration Of Work</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtDurationOfWork" autocomplete= "off" CssClass="form-control" TabIndex="8"></asp:TextBox>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-2">Contact Person</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtContactName" autocomplete= "off" CssClass="form-control" TabIndex="9"></asp:TextBox>
                </div>
                <div class="col-md-2">Contact Number</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtContactNo" MaxLength="10" autocomplete= "off" CssClass="form-control input-pos-int" TabIndex="10"></asp:TextBox>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-2">Payment Terms</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtPayTerms" autocomplete= "off" CssClass="form-control" TabIndex="13"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    Approved By&nbsp;
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" InitialValue="-Select-" ControlToValidate="ddlApprovedBy" CssClass="Validation_Text" ValidationGroup="ValWO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlApprovedBy" class="chosen-select form-control" TabIndex="14">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">
                    GST No.&nbsp;
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtGstNo" CssClass="Validation_Text" ValidationGroup="ValWO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtGstNo" Enabled="false" CssClass="form-control" MaxLength="20" TabIndex="15"></asp:TextBox>
                </div>
                <div class="col-md-2">Status</div>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="rblStatus" RepeatDirection="Horizontal" runat="server" TabIndex="16">
                        <asp:ListItem Text="Open" Value="Open" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Hold" Value="Hold"></asp:ListItem>
                        <asp:ListItem Text="Closed" Value="Closed"></asp:ListItem>
                        <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">TDS @ 194</div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlTDSPerc" CssClass="form-control" TabIndex="17">
                        <asp:ListItem Text="0" Value="0.00" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">Order Type</div>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="rblOrderType" RepeatDirection="Horizontal" runat="server" TabIndex="16">
                        <asp:ListItem Text="Fixed" Value="Fixed"></asp:ListItem>
                        <asp:ListItem Text="Recurring" Value="Recurring"></asp:ListItem>
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
                        <asp:FileUpload runat="server" ID="fuWODoc" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationGroup="FileSave"
                            ErrorMessage="PDF File Only" ForeColor="Red" ControlToValidate="fuWODoc"
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
                     <asp:Button ID="btnAddSOW" runat="server" Visible="false" Text="Add Scope Of Work" CssClass="btn btn-default" CausesValidation="false" OnClick="btnAddSOW_Click" TabIndex="15"></asp:Button>
                    <asp:Button ID="btnAddItem" runat="server" Visible="false" Text="Add Item" CssClass="btn btn-default" CausesValidation="false" OnClick="btnAddItem_Click" TabIndex="15"></asp:Button>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ValWO" OnClick="btnSubmit_Click" CssClass="btn btn-default" TabIndex="15"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default" TabIndex="16"></asp:Button>

                    <a runat="server" id="btnPrint" visible="false" class="btn btn-default" tabindex="18">Print </a>
                    <asp:Button ID="btnPrintPDF" runat="server" Text="PDF Print" OnClick="btnPrintPDF_Click" CssClass="btn btn-default" tabindex="19" Visible="false"></asp:Button>
                </div>
            </div>
            <br />
            
            <center>
                <asp:TextBox ID="txtTotal" Text="0" runat="server" Style="display: none"></asp:TextBox>
                <%--OnDeleteCommand="Grid_WOItem_DeleteCommand" OnRowCreated="Grid_WOItem_RowCreated" OnRowDataBound="Grid_WOItem_RowDataBound"--%>
                <ogrid:Grid runat="server" ID="Grid_PSOItem" ShowColumnsFooter="true"  
                     AutoGenerateColumns="false"  OnDeleteCommand="Grid_PSOItem_DeleteCommand"
                    CallbackMode="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="5">
                    <ScrollingSettings ScrollWidth="100%" />
                    <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
                    <Columns>
            
                        <%--<ogrid:Column DataField="Item_Type" HeaderText="Nature Of Work" Width="200px" Wrap="true" >
                            <TemplateSettings TemplateId="ItemTemplate" />
                        </ogrid:Column>--%>
                         <ogrid:Column DataField="Item_Type" HeaderText="Nature Of Work" Width="400px" Wrap="true" ></ogrid:Column>
                        <ogrid:Column DataField="Item_Desc" HeaderText="PSO Description" Width="400px" Wrap="true" ></ogrid:Column>
                        <ogrid:Column DataField="Sub_Item_Desc" HeaderText="Sub Item Description" Width="200px" Wrap="true" ></ogrid:Column>
                        <ogrid:Column DataField="Rate" HeaderText="Rate" Width="100px"></ogrid:Column>
                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" Width="120px" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Item_Perc" HeaderText="Percentage" Width="110px"></ogrid:Column>
                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" Width="100px"></ogrid:Column>
                        <ogrid:Column DataField="Total_Amt" HeaderText="Amount" Width="100px"></ogrid:Column>     
                        <ogrid:Column DataField="Igst_Perc" HeaderText="IGST %" Width="85px"></ogrid:Column>
                        <ogrid:Column DataField="Cgst_Perc" HeaderText="CGST %" Width="85px"></ogrid:Column>
                        <ogrid:Column DataField="Sgst_Perc" HeaderText="SGST %" Width="85px"></ogrid:Column>
                        <ogrid:Column  HeaderText="Delete" AllowDelete="true" Width="100" ></ogrid:Column>
                        <ogrid:Column DataField="PSO_Item_Id" HeaderText="PSO_Item_Id" Visible="false" Width="100" ></ogrid:Column>
                    </Columns>
                
                    <Templates>
                        <%--<ogrid:GridTemplate ID="ItemTemplatePSO" runat="server">
                            <Template>
                            <asp:LinkButton ID="lnkPSOItem" Text='<%#Container.DataItem["Item_Type"] %>' CommandArgument='<%#Container.DataItem["Item_Type"] %>' CommandName='<%#Container.DataItem["PSO_Item_Id"] %>'  OnClick="lnkPSOItem_Click" runat="server" CssClass="gridCB">
                                        </asp:LinkButton>
                            </Template>
                        </ogrid:GridTemplate>--%>
                    </Templates>

                </ogrid:Grid>
           
                <br />
                <br />


                <%--<ogrid:Grid ID="GridItemWiseTax" runat="server"  CallbackMode="false"  AutoGenerateColumns="false" AllowPaging="true" PageSize="15" AllowAddingRecords="false">
                    <ScrollingSettings ScrollWidth="100%" />
                    <Columns>
                        <ogrid:Column DataField="Item_Name" HeaderText="Item Name" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="ItemCode" HeaderText="Item Code" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Type" HeaderText="Type"></ogrid:Column>
                        <ogrid:Column DataField="Description" HeaderText="Description" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="TypePerc" HeaderText="Percentage" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="TypeAmt" HeaderText="Amount" ReadOnly="true"></ogrid:Column>                        
                    </Columns>
                </ogrid:Grid>--%>

                <br />
                <br />

                <%--<ogrid:Grid ID="Grid_Tax" runat="server" ShowColumnsFooter="true" 
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
                        <ogrid:Column DataField="WO_Tax_ID"  HeaderText="WO_Tax_ID"  Visible="false" Width="100" ></ogrid:Column>   
                        </Columns>
                        <Templates>
                            <ogrid:GridTemplate ID="QuantityTemplate" runat="server">
                                <Template>
                                    <asp:LinkButton ID="lnkEditamt" Text="Edit" CommandArgument='<%#Container.DataItem["Type_Amount"] %>' CommandName='<%#Container.DataItem["WO_Tax_ID"] %>'  OnClick="lnkEditamt_Click" runat="server" CssClass="gridCB">
                                    </asp:LinkButton>
                                </Template>
                            </ogrid:GridTemplate> 
                        </Templates>
                    </ogrid:Grid>--%>
             
                </center>
            <br />

               <center>
                <asp:TextBox ID="TextBox2" Text="0" runat="server" Style="display: none"></asp:TextBox>
                <ogrid:Grid runat="server" ID="Grid_SOWItem" ShowColumnsFooter="true"  
                     AutoGenerateColumns="false"  OnDeleteCommand="Grid_SOWItem_DeleteCommand"
                    CallbackMode="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="5">
                    <%--<ScrollingSettings ScrollWidth="100%" />--%>
                    <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
                    <Columns>
            
                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Scope Of Work" Width="400px" Wrap="true" >
                            <TemplateSettings TemplateId="SOWItemTemplate" />
                        </ogrid:Column>
                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description of Work" Width="200px" Wrap="true" ></ogrid:Column>
                        <ogrid:Column DataField="No_of_Staff_to_be_Deputed" HeaderText="No. of Staff to be Deputed" Width="180px" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Tentavie_Time" HeaderText="Tentavie Time Required" Width="180px" Wrap="true" ></ogrid:Column>
                        <ogrid:Column DataField="Amount" HeaderText="Amount" Width="120px"></ogrid:Column>
                        <ogrid:Column  HeaderText="Delete" AllowDelete="true" Width="100" ></ogrid:Column>
                        <ogrid:Column DataField="SOW_Item_Id" HeaderText="SOW_Item_Id" Visible="false"></ogrid:Column>
                    </Columns>
                
                    <Templates>
                        <ogrid:GridTemplate ID="SOWItemTemplatePSO" runat="server">
                            <Template>
                            <asp:LinkButton ID="lnkSOWItem" Text='<%#Container.DataItem["Scope_Of_Work"] %>' CommandArgument='<%#Container.DataItem["Scope_Of_Work"] %>' CommandName='<%#Container.DataItem["SOW_Item_Id"] %>'  OnClick="lnkSOWItem_Click" runat="server">
                                        </asp:LinkButton>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>

                </ogrid:Grid>
           
                <br />
                <br />

                <br />
                <br />
                </center>
            <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        </div>

    </div>

    <div class="panel panel-default" runat="server" id="divWOapproval" visible="false">
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
                    <%--OnClick="btnDecision_Click"  OnClick="btnApprovalCancel_Click"--%>
                    <asp:Button ID="btnDecision" runat="server"  Text="Decision" ValidationGroup="POApproval" CssClass="btn btn-default" TabIndex="16"></asp:Button>
                    <asp:Button ID="btnApprovalCancel" runat="server" Text="Cancel"  CssClass="btn btn-default" TabIndex="17"></asp:Button>
                </center>
            </div>
        </div>
    </div>

    <asp:Button runat="server" ID="btnAddItem1" Style="display: none"></asp:Button>

      <ajaxToolkit:ModalPopupExtender ID="ModalSOWItem" runat="server" PopupControlID="PanelSOWItem" TargetControlID="btnAddSOW"
        CancelControlID="BtnClose2" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    
       <asp:Panel ID="PanelSOWItem" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="BtnClose2" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center><h5 id="myModalamt1SOW"><asp:Label ID="Label2" runat="server" Text="Add  Scope Of Work Item"></asp:Label></h5></center>
                </div>
                <div class="modal-body">   
                     <div class="row">
                        <div class="col-md-2">
                            Description of Work&nbsp;
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtSOW" CssClass="Validation_Text" ValidationGroup="ValItemSOW" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                           <asp:TextBox ID="txtSOW" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                    Tentative Time Required&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="TxtTentavieTime" CssClass="Validation_Text" ValidationGroup="ValItemSOW" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="TxtTentavieTime" CssClass="form-control" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" runat="server" TabIndex="3"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd-MM-yyyy" TargetControlID="TxtTentavieTime"></ajaxToolkit:CalendarExtender>
                </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            No. of Staff to be Deputed&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="TextStaff" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValItemSOW" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <%--<asp:DropDownList runat="server" ID="ddlUOMSOW" CssClass="form-control"></asp:DropDownList>--%>
                            <asp:TextBox ID="TextStaff" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Amount&nbsp;
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="TxtAmountRate" CssClass="Validation_Text" ValidationGroup="ValItemSOW" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                             <asp:TextBox ID="TxtAmountRate" runat="server" autocomplete="off" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                         </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <center>
                          <asp:Button ID="btnSaveSOWItem" runat="server" Text="Save"  CssClass="btn btn-default" ValidationGroup="ValItemSOW" OnClick="btnSaveSOWItem_Click" />
                        <asp:Button ID="btnCancelSOWItem" runat="server"  Text="Cancel"  CssClass="btn btn-default" CausesValidation="false" OnClick="btnCancelSOWItem_Click" />        
                 </center>

                </div>
            </div>
        </div>

    </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="ModalWOItem" runat="server" PopupControlID="PanelWOItem" TargetControlID="btnAddItem"
        CancelControlID="BtnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    
    <asp:Panel ID="PanelWOItem" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="BtnClose" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center><h5 id="myModalamt1"><asp:Label ID="lblTax" runat="server" Text="Add WO Item"></asp:Label></h5></center>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-2">
                            Description/ Nature of Work
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtWorkType" CssClass="Validation_Text" ValidationGroup="ValItem" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox ID="txtWorkType" runat="server" TextMode="MultiLine" autocomplete="off" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            Work Description
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtWorkDesc" CssClass="Validation_Text" ValidationGroup="ValItem" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox ID="txtWorkDesc" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            PSO Perc&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtPSOPerc" runat="server" autocomplete="off" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Rate&nbsp;
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtRate" InitialValue="0.00" CssClass="Validation_Text" ValidationGroup="ValItem" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtRate" runat="server" autocomplete="off" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            UOM&nbsp;
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="ddlUOM" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValItem" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlUOM" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Quantity&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtPSOQty" runat="server" autocomplete="off" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            Total Value Of WO&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtAmount" runat="server" onkeyup="CalculateItemTaxAmt()" autocomplete="off" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>

                        <div class="col-md-2">
                            Tax&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBoxList runat="server" ID="chklTax" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Inclusive" Value="Inclusive" Selected="True" onclick="ShowHideTax(this);"></asp:ListItem>
                                <asp:ListItem Text="Exclusive" Value="Exclusive" onclick="ShowHideTax(this);"></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>

                    <div id="div_TaxType">
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

                </div>
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnAddSubItem" runat="server" Text="Add Sub-Item" visible="false" CssClass="btn btn-default" />
                        <asp:Button ID="btnSavePSOItem" runat="server" Text="Save"  CssClass="btn btn-default" ValidationGroup="ValItem" OnClick="btnSavePSOItem_Click" />
                        <asp:Button ID="btnCancelPSOItem" runat="server"  Text="Cancel"  CssClass="btn btn-default" CausesValidation="false" OnClick="btnCancelPSOItem_Click" />        
                 </center>

                </div>
            </div>
        </div>

    </asp:Panel>


    <ajaxToolkit:ModalPopupExtender ID="ModalWOSubItem" runat="server" PopupControlID="PanelWOSubItem" TargetControlID="btnAddSubItem"
        CancelControlID="BtnClose1" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    
    <asp:Panel ID="PanelWOSubItem" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="BtnClose1" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center><h5 id="myModalamt11"><asp:Label ID="Label1" runat="server" Text="Add WO Sub Item"></asp:Label></h5></center>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-2">
                            Work Description
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtWorkDesc" CssClass="Validation_Text" ValidationGroup="ValSubItem" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox ID="txtWorkDesc_Sub" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            UOM&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="ddlUOM_Sub" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValSubItem" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlUOM_Sub" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Rate&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtRate_Sub" InitialValue="0.00" CssClass="Validation_Text" ValidationGroup="ValSubItem" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtRate_Sub" runat="server" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            Quantity&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtWOQty_Sub" runat="server" autocomplete="off" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnSavePSOSubItem" runat="server" Text="Save" CssClass="btn btn-default" ValidationGroup="ValSubItem" OnClick="btnSavePSOSubItem_Click" />
                        <asp:Button ID="btnCancelSubItem" runat="server"  Text="Cancel"  CssClass="btn btn-default" CausesValidation="false" OnClick="btnCancelWOSubItem_Click" />        
                 </center>

                </div>
            </div>
        </div>

    </asp:Panel>
</asp:Content>
