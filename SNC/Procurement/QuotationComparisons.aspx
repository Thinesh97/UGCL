<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="QuotationComparisons.aspx.cs" Inherits="QuotationComparsions" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <style type="text/css">

       .tbl1 tr:first-child {
           background: #b42525;
           color: white;
       }

       .hideGridColumn
       {
           display:none;
       }
        .tbl1 tr:last-child{ background: white;color: black;}
         .gvwCasesPager a {
            margin-left: 5px;
            margin-right: 5px;
           
        }
   </style>

    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Quotation Comparisons 

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->
             <div class="row">
                <div class="col-md-2">Search By</div>
                <div class="col-md-4">
                    <asp:RadioButtonList runat="server" ID="rd_SearchBy" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rd_SearchBy_SelectedIndexChanged" TabIndex="1" >
                        <asp:ListItem Text="Item based" Value="ItemBased" Selected="True"></asp:ListItem>
                           <asp:ListItem Text="Indent based" Value="IndentBased"></asp:ListItem>
                    </asp:RadioButtonList>

                </div>
                 </div>
            <div class="row" runat="server" id="CateBaseddiv" >

                <div class="col-md-2">
                    Budget Sector &nbsp;
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValItemWise" InitialValue="-Select-" ControlToValidate="ddlBudgetSector"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlBudgetSector" runat="server" OnSelectedIndexChanged="ddlBudgetSector_SelectedIndexChanged" AutoPostBack="true"  CssClass="form-control" TabIndex="2">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>


                <div class="col-md-2">Category&nbsp;
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValItemWise" InitialValue="-Select-" ControlToValidate="ddlCategory"></asp:RequiredFieldValidator>
                   
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlCategory" CssClass="form-control" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" TabIndex="3">
                        <asp:ListItem Text="-Select-" Value="-Select-"></asp:ListItem>
                    </asp:DropDownList>

                </div>
      
            </div>
            <div class="row" runat="server" id="itemDiv">
                  <div class="col-md-2">Item&nbsp;
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValItemWise" InitialValue="-Select-" ControlToValidate="ddlItem"></asp:RequiredFieldValidator></div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlItem" CssClass="form-control" runat="server" TabIndex="4">
                          <asp:ListItem Text="-Select-" Value="-Select-"></asp:ListItem>
                    </asp:DropDownList>
                   
                </div>
            </div>
             <div class="row" runat="server" id="Indentbaseddiv" visible="false">
                <div class="col-md-2">Indent No</div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlIndentNo" CssClass="form-control" runat="server" TabIndex="5">
                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                    </asp:DropDownList>

                </div>
                <div class="col-md-2">Specified Quotation</div>
                <div class="col-md-4">
                     <asp:CheckBox ID="chkspecificquotation"  runat="server"  />

                </div>
             
            </div>
         
            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="ValItemWise" OnClick="btnSearch_Click" CssClass="btn btn-default" TabIndex="5"></asp:Button>                   
                   
                    <asp:Button ID="btn_ProceedToPO" runat="server" Text="Proceed To PO" CssClass="btn btn-default" OnClick="btn_ProceedToPO_Click" TabIndex="6" ></asp:Button>
                    <%-- <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="false" OnClick="btnPrint_Click" CssClass="btn btn-default"></asp:Button>--%>
                     <a runat="server" id="btnPrint" visible="false"  class="btn btn-default">Print </a>
                </div>
                </div>
               <br />
                  <div class="row" runat="server" align="right"   visible="false"  id="Div10">
                      <div class="form-inline">
                          <div class="form-group">
                              <label>Quatation Comparision Date:<asp:label runat="server" id="lblDate2"></asp:label></label>
                          </div>
                      </div>
              </div>
       
            <center>
                <div style="width:100%; overflow-x:scroll;" >

                                  <asp:PlaceHolder ID = "PlaceHolder1" runat="server" />
                 

                  <asp:GridView ID="Grid_QuotationComparsion" runat="server" AllowPaging="true" PageSize="25" OnPageIndexChanging="Grid_QuotationComparsion_PageIndexChanging"  CssClass="table tbl1" 
                      AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                       OnPreRender="Grid_QuotationComparsion_PreRender" EmptyDataText="No Records Found"  ShowHeader="true" >
                         <PagerStyle CssClass="gvwCasesPager" />  
                      <Columns>
                             <asp:BoundField DataField="Category_Name" HeaderText="Category Name" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item Name" /> 
                         
                            <asp:BoundField DataField="Vendor_name" HeaderText="Vendor Name" />  
                            <asp:TemplateField HeaderText="Vendor ID">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblVendorID" Text='<%# Eval("Vendor_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <%--  <asp:BoundField DataField="Vendor_ID" HeaderText="Vendor ID" />--%>
                             <asp:TemplateField HeaderText="Q.No">
                                <ItemTemplate>
                              <asp:HyperLink ID="lnQuotationNo" runat="server" NavigateUrl= '<%# "~/Procurement/Quotation.aspx?RefID=" + Eval("QuotationNo") %>'    CssClass="gridCB"  Text='<%# Eval("QuotationNo") %>' ></asp:HyperLink>  


                                   <%-- <asp:LinkButton runat="server" ID="lklQuotationNo" PostBackUrl="~/Procurement/Quotation.aspx" Text='<%# Eval("QuotationNo") %>'></asp:LinkButton>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <%--   <asp:BoundField DataField="QuotationNo" HeaderText="Q.No" />--%>
                                 <asp:BoundField DataField="QuotationDate" HeaderText="Quotation Date" HeaderStyle-Width="150px" ItemStyle-Width="150px"  DataFormatString="{0:dd-MM-yyy}" />
                              <asp:BoundField DataField="UOMName" HeaderText="UOM" />
                              <asp:BoundField DataField="Qty_required" HeaderText="Quantity" />
                              <asp:BoundField DataField="Rate" HeaderText="Rate" />
                             <asp:BoundField DataField="Amount" HeaderText="Amount" />
                              <asp:BoundField DataField="Tax" HeaderText="Tax" />  
                              <asp:BoundField DataField="TaxAmt" HeaderText="Tax Amt" />  
                                <asp:BoundField DataField="Discount" HeaderText="Discount" />  
                              <asp:BoundField DataField="DiscountAmt" HeaderText="Discount Amt" />  
                             <asp:BoundField DataField="WithOverAllAmt" HeaderText="Sub Total" /> 
                            <asp:BoundField DataField="TotalAmt"  HeaderText="Total Amount" />
                      
                             <asp:TemplateField  HeaderText="Select">
                                <ItemTemplate>

                                    <asp:RadioButton ID="ChkSelect" AutoPostBack="true" runat="server" GroupName="UselessGroup" OnCheckedChanged="ChkSelect_CheckedChanged"  />

                                 <%-- <asp:CheckBox ID="ChkSelect" runat="server"></asp:CheckBox>--%>
                                </ItemTemplate>
                                 </asp:TemplateField>                                        
                             
                           
                            
                        </Columns>                        
                    </asp:GridView> 
                </div>
           
                </center>
            <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        </div>

    </div>
    
</asp:Content>
