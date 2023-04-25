<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Budget/MonthlyBudget.aspx.cs" Inherits="MonthlyBudget" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<%@ Register TagPrefix="MultiDropDownLib" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <link href="../Style/Panel_colp/css/fonts/awesomeIconFonts/font-awesome.css" rel="stylesheet" />
    <link href="../Style/Panel_colp/css/jquery.accordion.css" rel="stylesheet" />
    <script>
        function beforedelete() {
            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                return false;
            }
            return true;
        }


        $(document).ready(function () {

            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
        });
    </script>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
    </style>
    <style>
        .accordion-ver > ul > li > .h1 {
            position: relative;
            display: block;
            float: right;
            margin-top: -25px;
            padding: 5px 20px 0px 15px;
            top: 0px;
            _right: 55px;
            font-size: 12px;
            font-family: Helvetica,Arial,sans-serif;
            text-decoration: none;
            text-transform: uppercase;
            color: #000;
            height: 20px;
            background: url(../Images/IndianRupee.png) no-repeat;
            background-position: left;
        }

        .dropdownstyle {
            width: 200px;
            background-color: white;
            border: 1px solid silver;
            height: 220px;
            overflow-y: scroll;
        }

        .dropdowntextstyle {
            width: 250px;
        }

        .dropdownimg {
            _border: 5px solid silver;
            _background-color: silver;
            height: 36px;
            width: 20px;
            _position: relative;
            margin-left: -12px;
            _border-bottom-right-radius: 5px;
            _border-top-right-radius: 5px;
            background: url(../Images/drpdwn.jpg) no-repeat;
        }

        .tbl1 tr:first-child {
            background: #b42525;
            color: white;
        }
    </style>




    <div class="panel">

        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-dashboard"></i>

                Budget

            </h3>

        </div>
        <div class="panel-body">
            <asp:UpdatePanel ID="upd1" runat="server">
                <ContentTemplate>


                    <div class="row">
                        <div class="col-md-2">
                            Project Name
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" InitialValue="-Select-" ControlToValidate="ddlProject" CssClass="Validation_Text" ValidationGroup="SaveBudget" ErrorMessage="*"></asp:RequiredFieldValidator>

                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" AutoPostBack="true" TabIndex="1">
                                <asp:ListItem Text="Select"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Project Code
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtProjectcode" CssClass="Validation_Text" ValidationGroup="SaveBudget" ErrorMessage="*"></asp:RequiredFieldValidator>

                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtProjectcode" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="row">
                <div class="col-md-2">
                    Month
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue="-Select-" ControlToValidate="ddlMonth" CssClass="Validation_Text" ValidationGroup="SaveBudget" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" Enabled="false">
                        <asp:ListItem Text="Select"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Year
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtYear" CssClass="Validation_Text" ValidationGroup="SaveBudget" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtYear" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    Reporting Person                   
                </div>
                <div class="col-md-4" style="background-color: white; z-index: 1;">
                    <%--   <asp:ListBox runat="server" ID="ddlReportingPerson"  Width="100%" Height="100px" SelectionMode="Multiple" > </asp:ListBox>--%>
                    <MultiDropDownLib:ComboBox runat="server" ID="ddlReportingPerson" Width="100%" Height="100px" CssClass="form-control" AllowEdit="false" SelectionMode="Multiple" TabIndex="2" />

                </div>
                <div class="col-md-2">
                    Primary Person
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" InitialValue="-Select-" ControlToValidate="ddlPromaryPerson" CssClass="Validation_Text" ValidationGroup="SaveBudget" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlPromaryPerson" CssClass="form-control" TabIndex="3">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">
                    Create From
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtCreatedDate" CssClass="Validation_Text" ValidationGroup="SaveBudget" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtCreatedDate" Enabled="false" onkeypress="javascript: return false;" onPaste="javascript: return false;" CssClass="form-control" runat="server"></asp:TextBox>

                  <%--  <asp:CompareValidator ControlToCompare="txtBudgetClosedDate" ValidationGroup="SaveBudget"
                        ControlToValidate="txtCreatedDate"
                        Display="Dynamic"
                        Style="color: red"
                        ErrorMessage="Create date should be less then closed date"
                        ID="CompareValidator1"
                        Operator="LessThan"
                        Type="Date"
                        runat="server" />--%>
                </div>
                <div class="col-md-2">
                    Budget Closed On
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtBudgetClosedDate" CssClass="Validation_Text" ValidationGroup="SaveBudget" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtBudgetClosedDate" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                  <%--  <asp:CompareValidator ControlToCompare="txtCreatedDate" ValidationGroup="SaveBudget"
                        ControlToValidate="txtBudgetClosedDate"
                        Display="Dynamic"
                        ErrorMessage="Closed date should be greater then create date"
                        Style="color: red"
                        ID="CompareValidator2"
                        Operator="GreaterThan"
                        Type="Date"
                        runat="server" />--%>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">Budget ID</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtBudgetID" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>

                </div>
                <div class="col-md-2">Total Values</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtTotalValues" CssClass="form-control input-pos-int" Enabled="false" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    Status
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="rd_Status" CssClass="Validation_Text" ValidationGroup="SaveBudget" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:RadioButtonList runat="server" ID="rd_Status" RepeatDirection="Horizontal" TabIndex="4">
                        <asp:ListItem Text="Open&nbsp;" Value="Open" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Hold&nbsp;" Value="Hold"></asp:ListItem>
                        <asp:ListItem Text="Close" Value="Close"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="col-md-2">Description</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtDescriptionBud" CssClass="form-control" runat="server" Style="resize: none" TextMode="MultiLine" TabIndex="5"></asp:TextBox>

                </div>
            </div>
        </div>
        <div class="modal-footer">

            <center>
           <asp:Button ID="btnSaveBudget" runat="server" Text="Save"  CssClass="btn btn-default" ValidationGroup="SaveBudget" OnClick="btnSaveBudget_Click" TabIndex="6"  />
                        <asp:Button ID="btnCancelItem" runat="server" Text="Cancel"  CssClass="btn btn-default" CausesValidation="false" OnClick="btnCancel_Click" TabIndex="7"   />
                         <asp:Button ID="btnSendForApproval" runat="server" Text="Send For Approval" OnClick="btnSendForApproval_Click"  CssClass="btn btn-default" CausesValidation="false"  Visible="false" TabIndex="8"  />
                         <asp:LinkButton ID="BtnPrint"  runat="server" Text="Print" Visible="false"  CssClass="btn btn-default" CausesValidation="false"  OnClick="BtnPrint_Click" TabIndex="9"  />
             </center>

        </div>
    </div>

    <br />

    <div id="accordion1">
        <ul runat="server" id="ItemListContainer" visible="false">

            <li style="background-color: #f6c8c8;">
                <h1>Automobiles </h1>
                <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:<asp:Label runat="server" ID="lblAutoTotal" CssClass="" Text="0"></asp:Label>
                 
                Approved Amount:<asp:Label runat="server" ID="lblAutoApproved" CssClass="" Text="0"></asp:Label>
               Actual PO raised:

                    <asp:LinkButton ID="lbPOAutoMobiles"  CommandArgument="1" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                Balance:<asp:Label runat="server" ID="lblRemAutoMobiles" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">

                    <p class="alnright">


                        <asp:ImageButton ID="img_AutoNew" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;

                        <asp:ImageButton ID="imgref_AutoNew" runat="server" ToolTip="Refer other budget " CssClass="imgicon" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ToolTip="Add New Item" ImageUrl="~/Style/Images/Add_item.jpg" ID="PopupItem_Auto" OnClick="PopupItem_Auto_Click" />

                        &nbsp;&nbsp; 
                        
                       <asp:ImageButton runat="server" ID="AutoMobiles_Print" ToolTip="Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon"  OnClick="Printimage_Click" />

                    </p>

                    <center>          
                       
                    <asp:GridView runat="server" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand"  OnRowDataBound="AutomobilesList_RowDataBound" ShowFooter="true" ID="AutomobilesList" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                             <asp:BoundField DataField="AssetName" HeaderText="Asset Name" />                        
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right"  />
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right" />
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                              <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                              <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Auto" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="likAutoDelete" OnClientClick="javascript: return beforedelete()"></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>
                           
                        </Columns>                        
                    </asp:GridView> 
                        
                              
                    </center>
                    <br />
                    <div class="row" runat="server" id="rowAutoRemarks" visible="false">
                        <div class="col-md-2">
                            <b>Remarks :</b>
                        </div>
                        <div class="col-md-10">
                            <asp:Label runat="server" ID="lblAutoRemark" Text=""></asp:Label>
                        </div>
                    </div>

                </div>
            </li>
            <li>
                <h1>Plant & Machinery</h1>
             <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:  <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalValuesPlant"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblPMpproved" CssClass="" Text="0"></asp:Label>
                 Actual PO raised:
                 <asp:LinkButton ID="lbPOPlantandMach"  CommandArgument="2" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                 
                 
                Balance:<asp:Label runat="server" ID="lblRemPlantandMach" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="Imag_PlantCSS" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgref_PlantCSS" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;                      
                        <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Plant" OnClick="PopupItem_Auto_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="PlantMachinery_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />

                    </p>
                    <br />

                    <div class="row">
                        <div class="col-md-12">
                            <center>
                                  <asp:GridView runat="server" DataKeyNames="Bud_Item_Id" OnRowDataBound="GridPlant_RowDataBound" OnRowCommand="BudgetList_RowCommand"  ShowFooter="true" ID="GridPlant" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                             <asp:BoundField DataField="AssetName" HeaderText="Asset Name" />                          
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right"/>
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                              <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Plant" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                           <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkPlantDelete" OnClientClick="javascript: return beforedelete()"  ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                        </Columns>                        
                    </asp:GridView> 
            
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="rowPlant" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="lblPlantRemarks" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li style="background-color: #f6c8c8;">
                <h1>Shuttering Materials</h1>
              <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount: <asp:Label runat="server" CssClass="" Text="0" ID="lblShutteringTotal"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblSMApproved" CssClass="" Text="0"></asp:Label>
                 Actual PO raised:
                  
                   <asp:LinkButton ID="lbPOShutteringMaterial"  CommandArgument="3" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                
                  
             
                Balance:<asp:Label runat="server" ID="lblRemShutteringMaterial" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="Imag_shuttCSS" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;

                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgref_shuttCSS" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                       
                           <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Shutter" OnClick="PopupItem_Auto_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="Shutter_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />

                    </p>
                    <br />

                    <div class="row">
                        <div class="col-md-12">
                            <center>

 <asp:GridView runat="server" ShowFooter="true" ID="GridShutter" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" OnRowDataBound="GridShutter_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right" />
                              <asp:BoundField DataField="Rate" HeaderText="Rate" />
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                              <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Shutter" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                               <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkShutterDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                        </Columns>                        
                    </asp:GridView> 
              
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trshutter" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="shutterRemarks" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>



                </div>
            </li>
            <li>
                <h1>Consumable Items</h1>
              <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:   <asp:Label runat="server" CssClass="" Text="0" ID="lblConsumeTotal"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblCIapproved" CssClass="" Text="0"></asp:Label>
                Actual PO raised:
                      <asp:LinkButton ID="lbPOConsumable"  CommandArgument="4" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                
                  
                  
                Balance:<asp:Label runat="server" ID="lblRemConsumable" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImageCSS_Consu" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_Consu" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                         <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="Img_Consume" OnClick="PopupItem_Auto_Click" />

                        &nbsp;&nbsp;

                        <asp:ImageButton runat="server" ID="ConsumableItems_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />

                    </p>
                    <br />


                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <center>
                            <asp:GridView runat="server" ShowFooter="true" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" ID="GridConsume" OnRowDataBound="GridConsume_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true">
                                <Columns>

                                    <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                                    <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <b>
                                                <asp:Label runat="server" ID="lblTotalName" Text="Total"> </asp:Label></b>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                                    <asp:TemplateField HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <b>
                                                <asp:Label runat="server" ID="lblTotalPOV" Text=""> </asp:Label></b>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                                <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Consume" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                                     <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkConsumableDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                                </Columns>
                            </asp:GridView>
                            </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trConsumable" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reConsumable" Text=""></asp:Label>
                            </div>
                        </div>



                    </div>
            </li>
            <li style="background-color: #f6c8c8;">
                <h1>Electrical Items</h1>
               <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:  <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalElect"></asp:Label>

                 Approved Amount:<asp:Label runat="server" ID="lblEIapproved" CssClass="" Text="0"></asp:Label>
               Actual PO raised:
                     <asp:LinkButton ID="lbPOElectricItem"  CommandArgument="5" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                
                   
               
                Balance:<asp:Label runat="server" ID="lblRemElectricItem" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImagCSS_Elect" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_Elect" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Elect" OnClick="PopupItem_Auto_Click" />


                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="ElectricalItems_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />

                    </p>
                    <br />


                    <div class="row">
                        <div class="col-md-12">
                            <center>


 <asp:GridView runat="server" ShowFooter="true" ID="Grid_Elect" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" OnRowDataBound="Grid_Elect_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right" />
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                              <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Elect" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkElectricalDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trElectrical" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reElectrical" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li>
                <h1>HSD</h1>
                <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount: <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalHSD"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblHSDapproved" CssClass="" Text="0"></asp:Label>
                 Actual PO raised:
                      <asp:LinkButton ID="lbPOHSD"  CommandArgument="6" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                
                   
                 

                Balance:<asp:Label runat="server" ID="lblRemHSD" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImageCSS_HSD" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_HSD" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                       <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_HSD" OnClick="PopupItem_Auto_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="HSDPetrol_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />

                    </p>
                    <br />



                    <div class="row">
                        <div class="col-md-12">
                            <center>
                 <asp:GridView runat="server" ShowFooter="true" ID="Grid_HSD" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" OnRowDataBound="Grid_HSD_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right" />
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                             <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_HSD" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkHSDDelete"  OnClientClick="javascript: return beforedelete()"></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trHSD" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reHSD" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li style="background-color: #f6c8c8;">
                <h1>Petrol,Oil & Lubricants</h1>
           <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:      <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalOil"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblPOLapproved" CssClass="" Text="0"></asp:Label>
                 Actual PO raised:
                <asp:LinkButton ID="lbPOPetrolOil"  CommandArgument="7" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                
            
                Balance:<asp:Label runat="server" ID="lblRemPetrolOil" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImageCSS_Oil" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_Oil" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                         <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Oil" OnClick="PopupItem_Auto_Click" />


                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="OIL_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />

                    </p>
                    <br />


                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" ID="Grid_Oil" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" OnRowDataBound="Grid_Oil_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right" />
                              <asp:BoundField DataField="Rate" HeaderText="Rate"  ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                            <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Oil" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkOilDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trPetrol" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="rePetrol" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li>
                <h1>Hardware Items</h1>
             <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:    <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalHardware"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblHIapproved" CssClass="" Text="0"></asp:Label>
                Actual PO raised:
                   <asp:LinkButton ID="lbPOHardwareItems"  CommandArgument="8" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                
            
                Balance:<asp:Label runat="server" ID="lblRemHI" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImageCSS_Hard" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_Hard" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Hardware" OnClick="PopupItem_Auto_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="Hardware_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />


                    </p>
                    <br />


                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" ID="Grid_Hardware" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" OnRowDataBound="Grid_Hardware_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right" />
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                              <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Hardware" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkHardwareDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trHard" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reHard" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li style="background-color: #f6c8c8;">
                <h1>Welding Electrodes</h1>
             <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:    <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalWelding"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblWEapproved" CssClass="" Text="0"></asp:Label>
                 Actual PO raised:
                 <asp:LinkButton ID="lbPOWeldingElecrodes"  CommandArgument="9" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                
                Balance:<asp:Label runat="server" ID="lblRemWE" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImagCSS_Wedd" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_Wedd" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                         <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Welding" OnClick="PopupItem_Auto_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="Welding_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />


                    </p>
                    <br />



                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" ID="Grid_Welding" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" OnRowDataBound="Grid_Welding_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right" />
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right" />
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                              <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Welding" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkWeldingDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trWelding" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reWelding" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li>
                <h1>Oxygen & Acetylene Gas</h1>
               <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:  <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalOxygen"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblOAGapproved" CssClass="" Text="0"></asp:Label>
                Actual PO raised:
                   <asp:LinkButton ID="lbPOOxygen"  CommandArgument="10" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                
                   
                Balance:<asp:Label runat="server" ID="lblRemOAG" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImageCSS_Oxygen" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_Oxygen" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                         <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Oxygen" OnClick="PopupItem_Auto_Click" />

                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="Oxygen_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />


                    </p>
                    <br />


                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" ID="Grid_Oxygen" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" OnRowDataBound="Grid_Oxygen_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right"/>
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right" />
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                            <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Oxygen" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkOxygenDelete"  OnClientClick="javascript: return beforedelete()"></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trOxygen" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reOxygen" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li style="background-color: #f6c8c8;">
                <h1>Safety Items</h1>
           <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:      <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalSafety"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblSIapproved" CssClass="" Text="0"></asp:Label>
               Actual PO raised:
               <asp:LinkButton ID="lbPOSafty"  CommandArgument="11" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                
                Balance:<asp:Label runat="server" ID="lblRemSI" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImageBfCSS_Safe" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Images/refer.png" ID="imgrefCSS_Safe" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                         <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Safety" OnClick="PopupItem_Auto_Click" />

                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="Safety_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />


                    </p>
                    <br />


                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" ID="Grid_Safety" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" OnRowDataBound="Grid_Safety_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right"/>
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right" />
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                           <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Safety" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkSafetyDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trSafety" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reSafety" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li>
                <h1>Staff Welfare</h1>
             <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:    <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalStaff"></asp:Label>

                 Approved Amount:<asp:Label runat="server" ID="lblSWapproved" CssClass="" Text="0"></asp:Label>
               Actual PO raised:
                 <asp:LinkButton ID="lbStaffWelfare"  CommandArgument="12" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />

                Balance:<asp:Label runat="server" ID="lblRemSW" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImageCSS_Staff" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Images/refer.png" ID="imgrefCSS_Staff" OnClick="imgref_AutoNew_Click" />

                        &nbsp;&nbsp;
                        
                         <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Staff" OnClick="PopupItem_Auto_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="Staff_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />

                    </p>
                    <br />



                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" ID="Grid_Staff" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" OnRowDataBound="Grid_Staff_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right"/>
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                            <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Staff" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkStaffDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trStaff" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reStaff" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li style="background-color: #f6c8c8;">
                <h1>Mess Expenditure</h1>
               <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:  <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalMess"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblMEapproved" CssClass="" Text="0"></asp:Label>
                Actual PO raised:
                   <asp:LinkButton ID="lbPOMessExpend"  CommandArgument="13" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />

                Balance:<asp:Label runat="server" ID="lblRemME" CssClass="" Text="0"></asp:Label>

                   </span>
               

                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImageBCSS_Mess" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_Mess" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                      
                         <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Mess" OnClick="PopupItem_Auto_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="Mess_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />

                    </p>
                    <br />




                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" ID="Grid_Mess" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" OnRowDataBound="Grid_Mess_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right"/>
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                             <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Mess" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkMessDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trMess" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reMess" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li>
                <h1>Printing & Stationery</h1>
               <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:  <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalPrinting"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblPSapproved" CssClass="" Text="0"></asp:Label>
                Actual PO raised:
                   <asp:LinkButton ID="lbPOPrinting"  CommandArgument="14" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                Balance:<asp:Label runat="server" ID="lblRemPS" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImageCSS_Print" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_Print" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                       <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Printing" OnClick="PopupItem_Auto_Click" />

                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="Printing_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />

                    </p>
                    <br />

                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" ID="Grid_Printing" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" OnRowDataBound="Grid_Printing_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right"/>
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                            <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Printing" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkPrintingDelete" OnClientClick="javascript: return beforedelete()"></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trPrinting" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="rePrinting" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li style="background-color: #f6c8c8;">
                <h1>Repairs & Maintenance</h1>
              <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:   <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalRepairs"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblRMapproved" CssClass="" Text="0"></asp:Label>
                 Actual PO raised:
                  <asp:LinkButton ID="lbPORepair"  CommandArgument="15" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                  
                Balance:<asp:Label runat="server" ID="lblRemRM" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImageCSS_Repair" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_Repair" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                        
                         <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Repairs" OnClick="PopupItem_Auto_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="Repairs_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />
                    </p>
                    <br />



                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" ID="Grid_Repairs" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" OnRowDataBound="Grid_Repairs_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                             <asp:BoundField DataField="AssetName" HeaderText="Asset Name" /> 
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right" />
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                             <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Repairs" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkRepairsDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trRepairs" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reRepairs" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li>
                <h1>BOQ Items</h1>
            <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:     <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalBoQ"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblBIapproved" CssClass="" Text="0"></asp:Label>
               Actual PO raised:
                <asp:LinkButton ID="lbPOBOQ"  CommandArgument="16" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />

                Balance:<asp:Label runat="server" ID="lblRemBI" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImagefCSS_BOQ" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_BOQ" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                         <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_BOQ" OnClick="PopupItem_Auto_Click" />

                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="BOQ_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />
                    </p>
                    <br />


                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" ID="Grid_BOQ" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" OnRowDataBound="Grid_BOQ_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right"/>
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                              <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_BOQ" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkBOQDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trBOQ" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reBOQ" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li style="background-color: #f6c8c8;">
                <h1>Sanitary Materials</h1>
               <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:  <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalSanitary"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblSMaterialapproved" CssClass="" Text="0"></asp:Label>
                 Actual PO raised:
                   <asp:LinkButton ID="lbPOSanitary"  CommandArgument="17" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                 
                Balance:<asp:Label runat="server" ID="lblRemSMaterial" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImageBfCSS_Sanit" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_Sanit" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                       
                         <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Sanitary" OnClick="PopupItem_Auto_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="Sanitary_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />
                    </p>
                    <br />



                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" ID="Grid_Sanitary" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" OnRowDataBound="Grid_Sanitary_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right"/>
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                              <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Sanitary" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkSanitaryDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trSanitary" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reSanitary" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li>
                <h1>Blasting Materials</h1>
               <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:  <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalBlasting"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblBMapproved" CssClass="" Text="0"></asp:Label>
                Actual PO raised:
                   
                   <asp:LinkButton ID="lbPOBlasting"  CommandArgument="18" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                   
                Balance:<asp:Label runat="server" ID="lblRemBM" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImagCSS_Blast" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_Blast" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                       
                         <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Blasting" OnClick="PopupItem_Auto_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="Blasting_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />
                    </p>
                    <br />




                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" ID="Grid_Blasting" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" OnRowDataBound="Grid_Blasting_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right"/>
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                            <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Blasting" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkBlastingDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trBlasting" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reBlasting" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li style="background-color: #f6c8c8;">
                <h1>Furnitures & Fixtures</h1>
             <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:    <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalFurnitures"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblFFapproved" CssClass="" Text="0"></asp:Label>
               Actual PO raised:
                 <asp:LinkButton ID="lbPOFurnitures"  CommandArgument="19" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                   
                Balance:<asp:Label runat="server" ID="lblRemFF" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImageCSS_Furn" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Images/refer.png" ID="imgrefCSS_Furn" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                        
                         <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Furnitures" OnClick="PopupItem_Auto_Click" />
                        &nbsp;&nbsp;
                        
                        <asp:ImageButton runat="server" ID="Furnitures_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />
                    </p>
                    <br />


                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" ID="Grid_Furnitures" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" OnRowDataBound="Grid_Furnitures_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right"/>
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                             <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Furnitures" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkFurnituresDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trFurnitures" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reFurnitures" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li>
                <h1>Fixed Assets</h1>
               <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:  <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalFixedAssets"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblFAapproved" CssClass="" Text="0"></asp:Label>
                Actual PO raised:
                   
                   <asp:LinkButton ID="lbPOFixed"  CommandArgument="20" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                  
                Balance:<asp:Label runat="server" ID="lblRemFA" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImageCSS_Fixed" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_Fixed" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_FixedAssets" OnClick="PopupItem_Auto_Click" />

                        &nbsp;&nbsp;
                        
                        <asp:ImageButton runat="server" ID="Fixed_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />
                    </p>
                    <br />


                    <div class="row">
                        <div class="col-md-12">
                            <center>
                                
                <asp:GridView runat="server"  ShowFooter="true" ID="Grid_Fixed_Assets" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" OnRowDataBound="Grid_Fixed_Assets_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right"/>
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                              <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_FixedAssets" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkFixedAssetsDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                             
                            
                                             
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trFixed" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reFixed" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li style="background-color: #f6c8c8;">
                <h1>Infrastructure Items</h1>
               <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:  <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalInfrastructure"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblIIapproved" CssClass="" Text="0"></asp:Label>
                Actual PO raised:
                    <asp:LinkButton ID="lbPOInfrastructure"  CommandArgument="21" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                  
                Balance:<asp:Label runat="server" ID="lblRemII" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImaCSS_Infr" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_Infr" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                       
                         <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Infrastructure" OnClick="PopupItem_Auto_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="Infrastructure_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />
                    </p>
                    <br />


                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" ID="Grid_Infrastructure" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" OnRowDataBound="Grid_Infrastructure_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right" />
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                              <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Infrastructure" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkInfrastructureDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trInfrastructure" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reInfrastructure" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li>
                <h1>Sand</h1>
                <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount: <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalSand"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblSapproved" CssClass="" Text="0"></asp:Label>
                Actual PO raised:
                     <asp:LinkButton ID="lbPOSand"  CommandArgument="22" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                  
                Balance:<asp:Label runat="server" ID="lblRemS" CssClass="" Text="0"></asp:Label>

                    </span>
               
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImagCSS_Sand" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_Sand" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                         <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Sand" OnClick="PopupItem_Auto_Click" />

                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="Sand_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />
                    </p>
                    <br />




                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" ID="Grid_Sand" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" OnRowDataBound="Grid_Sand_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right"/>
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                              <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Sand" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkSandDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trSand" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reSand" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li style="background-color: #f6c8c8;">
                <h1>Jelly/Metal/Aggregates</h1>
             <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:    <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalJelly"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblJMaapproved" CssClass="" Text="0"></asp:Label>
               Actual PO raised:
                 <asp:LinkButton ID="lbPOJelly"  CommandArgument="23" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                  
                Balance:<asp:Label runat="server" ID="lblRemJMa" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImageCSS_Jelly" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Images/refer.png" ID="imgrefCSS_Jelly" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Jelly" OnClick="PopupItem_Auto_Click" />

                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="Jelly_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />
                    </p>
                    <br />

                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" ID="Grid_Jelly" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" OnRowDataBound="Grid_Jelly_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right"/>
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                            <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Jelly" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkJellyDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trJelly" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reJelly" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li>
                <h1>Red Soil</h1>
               <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:  <asp:Label runat="server" CssClass="" Text="0" ID="lblRedSoil"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblRSapproved" CssClass="" Text="0"></asp:Label>
                Actual PO raised:
                    <asp:LinkButton ID="lbPORedSoil"  CommandArgument="24" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                  
                Balance:<asp:Label runat="server" ID="lblRemRS" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImageCSS_Red" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_Red" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                       
                         <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_RedSoil" OnClick="PopupItem_Auto_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="RedSoil_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />
                    </p>
                    <br />

                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" ID="Grid_RedSoil" OnRowDataBound="Grid_RedSoil_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right"/>
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                              <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_RedSoil" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkRedSoilDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trRed" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reRed" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li style="background-color: #f6c8c8;">
                <h1>Cement</h1>
                <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount: <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalCement"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblcementApproved" CssClass="" Text="0"></asp:Label>
                Actual PO raised:
                      <asp:LinkButton ID="lbPOCement"  CommandArgument="25" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                  
                Balance:<asp:Label runat="server" ID="lblRemCement" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImagCSS_Cement" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Images/refer.png" ID="imgrefCSS_Cement" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                      
                         <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Cement" OnClick="PopupItem_Auto_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="Cement_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />
                    </p>
                    <br />


                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" ID="Grid_Cement" OnRowDataBound="Grid_Cement_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right"/>
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                              <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Cement" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkCementDelete" OnClientClick="javascript: return beforedelete()"></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trCement" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reCement" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li>
                <h1>Chemicals</h1>
              <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:   <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalChemicals"></asp:Label>

                 Approved Amount:<asp:Label runat="server" ID="lblchemicalApproved" CssClass="" Text="0"></asp:Label>
                Actual PO raised:
                     <asp:LinkButton ID="lbPOChemicals"  CommandArgument="26" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                  
                Balance:<asp:Label runat="server" ID="lblRemchemical" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImagfCSS_Chemi" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_Chemi" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Chemicals" OnClick="PopupItem_Auto_Click" />

                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="Chemicals_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />
                    </p>
                    <br />



                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" ID="Grid_Chemicals" OnRowDataBound="Grid_Chemicals_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right"/>
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                              <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Chemicals" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkChemicalsDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trChemicals" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reChemicals" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li style="background-color: #f6c8c8;">
                <h1>Bricks</h1>
               <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:  <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalBricks"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblBricksApproved" CssClass="" Text="0"></asp:Label>
                Actual PO raised:
                   <asp:LinkButton ID="lbPOBricks"  CommandArgument="27" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                  
                Balance:<asp:Label runat="server" ID="lblRemBricks" CssClass="" Text="0"></asp:Label>

               </span>
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImaCSS_Bricks" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_Bricks" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                      
                         <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Bricks" OnClick="PopupItem_Auto_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="Bricks_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />

                    </p>
                    <br />



                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" ID="Grid_Bricks" OnRowDataBound="Grid_Bricks_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right"/>
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                            <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Bricks" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkBricksDelete" OnClientClick="javascript: return beforedelete()"></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trBricks" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reBricks" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li>
                <h1>Steels</h1>
                 <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:<asp:Label runat="server" CssClass="" Text="0" ID="lblTotalSteels"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblSteelsApproved" CssClass="" Text="0"></asp:Label>
                Actual PO raised:
                       <asp:LinkButton ID="lbPOSteels"  CommandArgument="28" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                  
                Balance:<asp:Label runat="server" ID="lblRemSteels" CssClass="" Text="0"></asp:Label>

                     </span>
               
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImagCSS_steel" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_steel" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                         <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Steels" OnClick="PopupItem_Auto_Click" />

                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="Steels_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />

                    </p>
                    <br />

                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" ID="Grid_Steels" OnRowDataBound="Grid_Steels_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right"/>
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                              <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Steels" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkSteelsDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trSteels" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reSteels" Text=""></asp:Label>
                            </div>
                        </div>



                    </div>
            </li>
            <li style="background-color: #f6c8c8;">
                <h1>Other Construction Materials</h1>
              <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount:   <asp:Label runat="server" CssClass="" Text="0" ID="lblTotal_OtherConstruction"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblOCMapproved" CssClass="" Text="0"></asp:Label>
                Actual PO raised:
                     <asp:LinkButton ID="lbPOOther_Construction"  CommandArgument="29" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                  
                Balance:<asp:Label runat="server" ID="lblRemOCM" CssClass="" Text="0"></asp:Label>

                  </span>
               
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImagefCSS_OtherCons" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_OtherCons" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                       
                         <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_OtherConstruction" OnClick="PopupItem_Auto_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="OtherConstruction_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />

                    </p>
                    <br />

                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" ID="Grid_Other_Construction" OnRowDataBound="Grid_Other_Construction_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right"/>
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                             <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                             <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_OtherConstruction" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkOtherConstructionDelete" OnClientClick="javascript: return beforedelete()" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trOtherc" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reOtherc" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
            <li>
                <h1>Others</h1>
                <span style="position: absolute;right: 20px; top: 8px;" class="pull-right">
             Requested Amount: <asp:Label runat="server" CssClass="" Text="0" ID="lblTotalOthers"></asp:Label>
                 Approved Amount:<asp:Label runat="server" ID="lblOthApproved" CssClass="" Text="0"></asp:Label>
                Actual PO raised:
                      <asp:LinkButton ID="lbPOOthers"  CommandArgument="30" OnClick="lbPOAutoMobiles_Click" Text="0" runat="server" />
                  
                Balance:<asp:Label runat="server" ID="lblRemOth" CssClass="" Text="0"></asp:Label>

                    </span>
               
                <div class="panel-body">
                    <p class="alnright">
                        <asp:ImageButton ID="ImageCSS_Others" runat="server" ToolTip="Refer Material" CssClass="imgicon" ImageUrl="~/Images/MaterilaIcon.jpg" OnClick="img_MaterialRefer_Click" />
                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" CssClass="imgicon" ID="imgrefCSS_Others" ImageUrl="~/Images/refer.png" OnClick="imgref_AutoNew_Click" />
                        &nbsp;&nbsp;
                         <asp:ImageButton runat="server" CssClass="imgicon" ImageUrl="~/Style/Images/Add_item.jpg" ID="img_Others" OnClick="PopupItem_Auto_Click" />

                        &nbsp;&nbsp;
                        <asp:ImageButton runat="server" ID="Other_Print" ImageUrl="~/Style/Images/Print.png" CssClass="imgicon" OnClick="Printimage_Click" />


                    </p>
                    <br />

                    <div class="row">
                        <div class="col-md-12">
                            <center>
                <asp:GridView runat="server" ShowFooter="true" DataKeyNames="Bud_Item_Id" OnRowCommand="BudgetList_RowCommand" ID="Grid_Others" OnRowDataBound="Grid_Others_RowDataBound" CssClass="table tbl1" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>  
                       
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" ItemStyle-HorizontalAlign="Right" />
                              <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"/>
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues" Text='<%# Eval("Purc_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                           <asp:TemplateField  HeaderText="HO" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHO" Text='<%# Eval("HO_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalHO" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  HeaderText="Local" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLocal_Value" Text='<%# Eval("Local_Value") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalLocal_Value" Text="" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField  HeaderText="Edit" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="EditBudgetItem_Others" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Edit" ID="likAutoEdit" ></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField>   
                             <asp:TemplateField  HeaderText="Delete" >
                                <ItemTemplate>
                                <asp:LinkButton runat="server" CausesValidation="false"  CommandName="DeleteBudgetItem" CommandArgument='<%# Eval("Bud_Item_Id") %>'
                Text="Delete" ID="linkOthersDelete" OnClientClick="javascript: return beforedelete()"></asp:LinkButton>
                                </ItemTemplate>
                              </asp:TemplateField> 
                        </Columns>                        
                    </asp:GridView>
               
                     </center>

                        </div>
                        <br />
                        <div class="row" runat="server" id="trOthers" visible="false">
                            <div class="col-md-2">
                                <b>Remarks :</b>
                            </div>
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="reOthers" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </li>
        </ul>
    </div>

    <asp:Button ID="dummy2" runat="server" Text="cssdummy" Style="display: none" />

    <ajaxToolkit:ModalPopupExtender ID="ModelPopupCSSItem" runat="server" PopupControlID="PanelCSS" TargetControlID="dummy2"
        CancelControlID="btnCloseCSS" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelCSS" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnCloseCSS" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center> <h5 id="myModalLabelcrate1">Refer Existing Items</h5></center>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-2">
                            Project Name
                              <asp:RequiredFieldValidator ID="RequiredProject" runat="server" ControlToValidate="ddlExistingProjectName" ValidationGroup="crateinfo" InitialValue="-Select-" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlExistingProjectName" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlExistingProjectName_SelectedIndexChanged">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            Monthly Budget
                     
                             <asp:RequiredFieldValidator ID="RequiredFieldMonth" runat="server" ControlToValidate="ddlExistingMonth" ValidationGroup="crateinfo" InitialValue="-Select-" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlExistingMonth" CssClass="form-control" runat="server">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>
                    <div class="row" style="overflow-y: scroll">


                        <ogrid:Grid ID="GridViewHorror" runat="server" FolderStyle="../Gridstyles/grand_gray" AllowRecordSelection="true" AllowFiltering="false" AllowAddingRecords="false" AutoGenerateColumns="false" AllowGrouping="false" AllowPageSizeSelection="false">
                            <ScrollingSettings ScrollWidth="100%" />
                            <Columns>
                                <ogrid:Column DataField="AssetName" HeaderText="Asset Name"></ogrid:Column>
                                <ogrid:Column DataField="Category_Name" HeaderText="Category"></ogrid:Column>
                                <ogrid:Column DataField="Item_Name" HeaderText="Item"></ogrid:Column>                           
                                 <ogrid:Column DataField="UOMName" HeaderText="UOM"></ogrid:Column>
                                <ogrid:Column DataField="Req_Qty" HeaderText="Required Qty"></ogrid:Column>
                                <ogrid:Column DataField="Rate" HeaderText="Rate"></ogrid:Column> 
                                <ogrid:Column DataField="HO_Value" HeaderText="HO"></ogrid:Column>
                                <ogrid:Column DataField="Local_Value" HeaderText="Local"></ogrid:Column>
                                  <ogrid:Column DataField="Purc_Value" HeaderText="Values Of Purchase"></ogrid:Column>
                            </Columns>                          
                        </ogrid:Grid>



                    </div>

                </div>

                <div class="modal-footer">
                    <center>
                     <asp:Button ID="btn_ItemImport" Visible="false" runat="server" Text="Import"  CssClass="btn btn-default"  OnClick="btn_ItemImport_Click"/>

           <asp:Button ID="btnExistingBudgetSearch" runat="server" Text="Search"  CssClass="btn btn-default" ValidationGroup="crateinfo" OnClick="btnExistingBudgetSearch_Click"/>
            <asp:Button ID="btnExistingBudgetCancel" runat="server" Text="Cancel"   CssClass="btn btn-default" CausesValidation="false" OnClick="btnExistingBudgetCancel_Click"   />
                 </center>

                </div>
            </div>
        </div>

        <input type="hidden" id="Pannelcnt" runat="server" value="1" />
    </asp:Panel>







    <!-- ModalPopupExtender -->
    <asp:Button ID="btnDummy" runat="server" Text="btnDummy" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="ModelPopupRecurringItem" runat="server" BehaviorID="cmnid" PopupControlID="PanelBudget" TargetControlID="btnDummy"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelBudget" runat="server" align="center" Style="display: none">

        <div class="modal-dialog">
            <div class="modal-content">
                <button type="button" id="btnClose" class="close" data-dismiss="modal" aria-hidden="true"></button>
                <div class="modal-body">
                    <center> <h5 id="myModalLabelcrate2">Add Items</h5></center>
                </div>
                <div class="modal-body">
                    <div class="row" runat="server" visible="false" id="MaterialType">
                        <div class="col-md-2">
                            Material Type&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlItemType_r" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ItemInfo_r"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:RadioButtonList ID="ddlItemType_r" runat="server">
                                <asp:ListItem Text="Material" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="General Description"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-2">
                            Description
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtDescription_r" CssClass="form-control"></asp:TextBox>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            Category&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlCateGory_r" InitialValue="-Select-" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ItemInfo_r"> </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlCateGory_r" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            Item&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlItem_r" InitialValue="-Select-" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ItemInfo_r"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlItem_r" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>

                    <div class="row" runat="server" id="trRecurring">
                        <div class="col-md-2">
                            Part No &nbsp;
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtPartNo_r" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ItemInfo_r"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtPartNo_r" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div class="col-md-2">
                            Recurring     
                        </div>
                        <div class="col-md-4" runat="server" id="trRecurringCntr">
                            <asp:CheckBox ID="chkRecurring_r" AutoPostBack="true" OnCheckedChanged="chkRenewable_CheckedChanged" runat="server" />
                        </div>


                    </div>
                    <div class="row" runat="server" id="trAssetType">
                        <div class="col-md-2">
                            Asset Type
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlAssetType_r" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAssetType_r_SelectedIndexChanged" CssClass="form-control">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            Asset Category
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlAssetCate_r" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAssetCate_r_SelectedIndexChanged" CssClass="form-control">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>
                    <div class="row" runat="server" id="trAsset">
                        <div class="col-md-2">
                            Asset Name
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlAssetName_r" runat="server" CssClass="form-control">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>

                        </div>


                    </div>
                    <div class="row" visible="false" runat="server" id="divrecurring">
                        <div class="row">
                            <div class="col-md-2">
                                Maintenance
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlMaintainance_r" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Schedule Service" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Prevent Maintenance" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Other" Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                Standard (Hrs/kms)
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtStdard_r" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-md-2">
                                Service Date
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtServiceDate_r" CssClass="form-control" onkeypress="javascript: return false;" onPaste="javascript: return false;" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalServiceDate" TargetControlID="txtServiceDate_r" Format="dd-MM-yyyy" runat="server"></ajaxToolkit:CalendarExtender>
                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            Unit&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlUnit_r" InitialValue="-Select-" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ItemInfo_r"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlUnit_r" runat="server" CssClass="form-control" Enabled="false">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2">
                            Required Qty
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtRequiredQty_r" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ItemInfo_r"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtRequiredQty_r" onkeyup="POCalc()" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            Rate
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="txtRate_r" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ItemInfo_r"></asp:RequiredFieldValidator>

                        </div>
                        <div class="col-md-4">



                            <asp:TextBox runat="server" ID="txtRate_r" onkeyup="POCalc()" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Value Of Purchase
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtValuesOdPurchase_r" Text="" onpaste="return false" onkeypress="return false" CssClass="form-control input-pos-float"></asp:TextBox>

                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            Local
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtLocal_r" onkeyup="POCalc()" Text="" CssClass="form-control input-pos-float"></asp:TextBox>
                            <asp:Button ID="btnImport" Visible="false" runat="server" Text="Import" CssClass="btn btn-default" OnClick="btnRecurringItemSave_Click" />

                        </div>

                        <div class="col-md-2">
                            HO
                        </div>
                        <div class="col-md-4">

                            <asp:TextBox runat="server" ID="txtHo_r" Text="0" onpaste="return false" onkeypress="return false" AutoComplete="off" CssClass="form-control input-pos-float"></asp:TextBox>

                        </div>

                    </div>
                    <div class="modal-footer">
                        <center>
           <asp:Button ID="btnSaveItem" runat="server" Text="Save"   CssClass="btn btn-default" OnClick="btnRecurringItemSave_Click" ValidationGroup="ItemInfo_r"   />
                        <asp:Button ID="btnItempopCancel" runat="server" Text="Cancel" OnClick="btnItempopCancel_Click"  CssClass="btn btn-default" CausesValidation="false"   />
                 </center>

                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>
    <!-- ModalPopupExtender -->




    <asp:Button ID="BtnMaterialRef" runat="server" Text="cssdummy" Style="display: none" />

    <ajaxToolkit:ModalPopupExtender ID="ModelReferMaterial" runat="server" PopupControlID="matrilarefpanel" TargetControlID="BtnMaterialRef"
        CancelControlID="btnCloseCSS2" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="matrilarefpanel" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnCloseCSS2" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center> <h5 id="myModalLabelcrate31">Refer Material Items</h5></center>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-2">
                            Sector Name
                              <asp:RequiredFieldValidator ID="RequiredFieldValidatorforSector" runat="server" ControlToValidate="ddlBudgetSector" ValidationGroup="SearchRef" InitialValue="-Select-" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlBudgetSector" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlBudgetSector_SelectedIndexChanged">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            Category Name                    
                            
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlCategory" CssClass="form-control" runat="server">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>
                    <div class="row" style="overflow-y: scroll">

                        <ogrid:Grid ID="RefMaterialGridItems" runat="server" FolderStyle="../Gridstyles/grand_gray" AllowRecordSelection="true" AllowFiltering="false" AllowAddingRecords="false" AutoGenerateColumns="false" AllowGrouping="false" AllowPageSizeSelection="false">

                            <Columns>
                                <ogrid:CheckBoxSelectColumn ShowHeaderCheckBox="true" HeaderText=" Select" Width="150px"></ogrid:CheckBoxSelectColumn>
                                <ogrid:Column DataField="Item_Name" HeaderText="Item Name"></ogrid:Column>
                                <ogrid:Column DataField="Item_Code" HeaderText="Item Code"></ogrid:Column>
                            </Columns>
                        </ogrid:Grid>
                    </div>
                    <div class="modal-footer">
                        <center>
                         <asp:Button ID="btnImportPOPUP" Visible="false" runat="server" Text="Import" OnClick="btnImportPOPUP_Click"   CssClass="btn btn-default"/>
                   
                          <asp:Button ID="btnSearchPop" runat="server" Text="Search"  CssClass="btn btn-default" ValidationGroup="SearchRef" OnClick="btnSearchPop_Click"/>
                        <asp:Button ID="btnCancelPop" runat="server" Text="Cancel"   CssClass="btn btn-default" CausesValidation="false" OnClick="btnCancelPop_Click" />         
            
                 </center>

                    </div>
                </div>
            </div>

            <input type="hidden" id="Hidden1" runat="server" value="1" />
    </asp:Panel>





    <%--<script src="../Style/Panel_colp/jquery-1.8.2.min.js"></script>--%>
    <script src="../Style/Panel_colp/jquery.accordion.js"></script>
    <script>




        $("#accordion1").awsAccordion({


            type: "vertical",
            cssAttrsVer: {
                //ulWidth: 1000,
                liHeight: 30
            },
            startSlide: 1<%--parseInt('<% Convert.ToInt32(Session["count"]); %>')--%>,
            openCloseHelper: {
                openIcon: "plus-sign",
                closeIcon: "minus-sign"

            },
            openOnebyOne: false,
            classTab: "active",
            slideOn: "click",
            autoPlay: false,
            autoPlaySpeed: 2000
        })



    </script>


    <script type="text/javascript">

        function POCalc() {
            var txtRate_r = document.getElementById("<%=txtRate_r.ClientID%>").value
            var txtRequiredQty_r = document.getElementById("<%=txtRequiredQty_r.ClientID%>").value
            var txtValuesOdPurchase_rV = document.getElementById("<%=txtValuesOdPurchase_r.ClientID%>").value

            if (document.getElementById("<%=txtRate_r.ClientID%>").value != "" && document.getElementById("<%=txtRequiredQty_r.ClientID%>").value != "") {
                document.getElementById("<%=txtValuesOdPurchase_r.ClientID%>").value = (document.getElementById("<%=txtRate_r.ClientID%>").value * document.getElementById("<%=txtRequiredQty_r.ClientID%>").value);
                BudgetItemCalc();
            }
            else {
                document.getElementById("<%=txtValuesOdPurchase_r.ClientID%>").value = 0;
                BudgetItemCalc();
            }
        }



        function BudgetItemCalc() {
            var txtValuesOdPurchase_rV = document.getElementById("<%=txtValuesOdPurchase_r.ClientID%>").value
            var txtLocal_rV = document.getElementById("<%=txtLocal_r.ClientID%>").value


            if (document.getElementById("<%=txtValuesOdPurchase_r.ClientID%>").value != "" && document.getElementById("<%=txtValuesOdPurchase_r.ClientID%>").value != null) {
                if (document.getElementById("<%=txtLocal_r.ClientID%>").value != "" && document.getElementById("<%=txtLocal_r.ClientID%>").value != null) {


                    var localValue = document.getElementById("<%=txtLocal_r.ClientID%>").value;
                    var purchaseValue = document.getElementById("<%=txtValuesOdPurchase_r.ClientID%>").value;

                    if (+parseFloat(localValue).toFixed(2) > +parseFloat(purchaseValue).toFixed(2)) {

                        document.getElementById("<%=txtLocal_r.ClientID%>").value = 0;
                        document.getElementById("<%=txtHo_r.ClientID%>").value = document.getElementById("<%=txtValuesOdPurchase_r.ClientID%>").value;
                    }
                    else {
                        document.getElementById("<%=txtHo_r.ClientID%>").value = (document.getElementById("<%=txtValuesOdPurchase_r.ClientID%>").value - document.getElementById("<%=txtLocal_r.ClientID%>").value);
                    }
                }
                else {
                    document.getElementById("<%=txtHo_r.ClientID%>").value = document.getElementById("<%=txtValuesOdPurchase_r.ClientID%>").value;
                }
            }
            else {
                document.getElementById("<%=txtHo_r.ClientID%>").value = 0;
                document.getElementById("<%=txtValuesOdPurchase_r.ClientID%>").value = 0;
                document.getElementById("<%=txtLocal_r.ClientID%>").value = 0;
            }
        }

    </script>

    <!------------------------------------------------------------------------------------------------------------------------------>




</asp:Content>

