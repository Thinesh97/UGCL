<%@ Page Language="C#" MasterPageFile="~/MasterPage/SNC_Print.Master" AutoEventWireup="true" CodeBehind="QuotationPrint.aspx.cs" Inherits="QuotationPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .tbl1 tr:first-child {
            background: #b42525;
            color: white;
        }

        .hideGridColumn {
            display: none;
        }

        td, th {
            font-size: 12px;
        }
    </style>
    <style>
        .tbl_1 td:nth-child(4), .tbl_1 td:nth-child(5), .tbl_1 td:nth-child(6), .tbl_1 td:nth-child(7), .tbl_2 td:nth-child(3), .tbl_2 td:nth-child(4), .tbl_3 td:nth-child(2), .tbl_3 td:nth-child(3) {
            text-align: right;
        }

        @page {
            _size: 8.5in 11in;
            margin-top: 140px;
        }

        @media print {
            tr {
                page-break-before: always;
            }

            .no-print, .no-print * {
                display: none !important;
            }
        }
    </style>

    <style>
        table {
            width: 100%;
            border: 1px solid black;
        }


        .tbl th {
            /*padding:5px;*/
        }

        .tbl tr {
            height: 5px;
        }

        .lst p {
            list-style: decimal;
            display: inline;
            float: left;
            width: 250px;
            margin-left: 10px;
        }

        #mid {
            width: 100%;
            padding: 0;
        }

        .NoMargin {
            margin: 0px;
        }

        .tblw td {
            padding: 6px;
        }

        .GridCenter th, .GridCenter th {
            text-align: center;
        }

        .GridCenter2 {
            text-align: right;
        }

        .GridCenter3 {
            text-align: center;
        }

        .auto-style1 {
            text-align: center;
            height: 10px;
        }

        .auto-style2 {
            height: 68px;
        }
    </style>

    <br />
    <br />


    <div id="mid">
        <div id="divToPrint">
            <div id="divTableDataHolder" style="width: 100%;">

                <div style="padding: 10px;">
                    <b><u>Quotation Comparisons</u></b>
                    <br />
                    <br />
                    <div class="row" runat="server" id="trIndentNo1"  style="font-weight: bold">
                        <div class="col-xs-2">Indent No :</div>
                        <div class="col-xs-1">  <asp:Label ID="lblIndentNo1" runat="server" Text="" /></div>
                         <div class="col-xs-2">Indent Date:</div>
                        <div class="col-xs-1">  <asp:Label ID="lblIndentdate" runat="server" Text="" /></div>
                         <div class="col-xs-4">Quatation Comparision Date :</div>
                        <div class="col-xs-1">  <asp:Label ID="lblquatcomparedate" runat="server" Text="" /></div>
                    </div>
                    <div class="row" runat="server" id="trItemName1" style="font-weight: bold">
                        <div class="col-md-1">Item Name :</div>
                        <div class="col-md-2">  <asp:Label ID="lblItem1" runat="server" Text="" /></div>
                    </div>
                    <br />
                    <div style="overflow-x: scroll;">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
                        <br />
                    </div>


                    <br />
                    <br />
                    <table class="table table-bordered" style="display:none">


                        <tr runat="server" id="trIndentNo" style="font-weight: bold">
                            <td style="width: 100px; border-color: none; border-bottom-color: white; border-right-color: white">Indent No:
                            </td>

                            <td style="border-color: none; border-bottom-color: white">
                                <asp:Label ID="lblIndentNo" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr runat="server" id="trItemName" style="font-weight: bold">
                            <td style="width: 100px; border-color: none; border-bottom-color: white; border-right-color: white">Item Name:
                            </td>

                            <td style="border-color: none; border-bottom-color: white;">
                                <asp:Label ID="lblItem" runat="server" Text="" />
                            </td>
                        </tr>





                        <tr>

                            <td colspan="2" >

                                <asp:GridView ID="Grid_QuotationComparsion" CellPadding="2" CellSpacing="5" runat="server" Width="100%" CssClass="GridCenter"
                                    AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Records Found" ShowHeader="true">
                                    <Columns>
                                        <asp:BoundField DataField="Category_Name" HeaderText="Category Name" />
                                        <asp:BoundField DataField="Item_Name" HeaderText="Item Name" />

                                        <asp:BoundField DataField="Vendor_name" HeaderText="Vendor Name" />
                                        <asp:TemplateField HeaderText="Vendor ID">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblVendorID" Text='<%# Eval("Vendor_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="QuotationNo" HeaderText="Q.No" />
                                        <asp:BoundField DataField="QuotationDate" HeaderText="Quotation Date" HeaderStyle-Width="80px" ItemStyle-Width="80px" DataFormatString="{0:dd-MM-yyy}" />
                                        <asp:BoundField DataField="UOMName" HeaderText="UOM" />
                                        <asp:BoundField DataField="Qty_required" HeaderText="Quantity" />
                                        <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                        <asp:BoundField DataField="Tax" HeaderText="Tax" />
                                        <asp:BoundField DataField="TaxAmt" HeaderText="Tax Amt" />
                                        <asp:BoundField DataField="Discount" HeaderText="Discount" />
                                        <asp:BoundField DataField="DiscountAmt" HeaderText="Discount Amt" />
                                        <asp:BoundField DataField="WithOverAllAmt" HeaderText="Tax With Amt" />
                                        <asp:BoundField DataField="TotalAmt" HeaderText="Total Amount" />
                                        <asp:BoundField DataField="TransportCost" HeaderText="Transport Cost" />
                                        <asp:BoundField DataField="PaymentTerms" HeaderText="Payment Terms" />
                                        <asp:BoundField DataField="Delivery" HeaderText="Delivery Schedule" />

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>

                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
