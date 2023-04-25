<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC_Print.Master" AutoEventWireup="true" CodeBehind="Material_Indent_Print.aspx.cs" Inherits="Material_Indent_Print" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .midP {
            width: 1024px;
            margin: 0 auto;
            height: auto;
        }

        .tb {
            width: 825px;
            border: 1px solid #000000;
        }

        .tdC {
            text-align: center;
            border-bottom: 1px solid #000000;
            padding: 5px;
        }

        p {
            margin: 0;
            padding: 0;
        }

        .ftL {
            float: left;
            width: 50%;
        }

            .ftL p, .ftR p {
                border: 1px solid #000;
                padding: 5px;
            }

        .ftR {
            float: right;
            width: 50%;
        }

        .tbl2 td, .tbl3 td, .tbl3 {
            border: 0px solid white;
        }

            .tbl3 td {
                text-align: center;
            }

        .tbl_3 td:nth-child(7), .tbl_3 td:nth-child(8) {
            text-align: right;
        }
        .tbl4 td{
        font-weight:bold;
        border:0px solid white;
        }
    </style>


    <div class="midP">

        <table class="tb">
            <tr>
                <td class="tdC"><b>UNITED GLOBAL CORPORATION LIMITED</b></td>
            </tr>

            <tr>
                <td class="tdC"><b>MATERIAL INDENT</b></td>
            </tr>

            <tr>
                <td class="tdC" style="font-weight: bold">
                    <div style="width: 100%; padding: 0 5px 0 5px;">
                        <p style="float: left;">
                            Indent No :
                            <asp:Label runat="server" Text="" ID="lblindent_no"></asp:Label>
                        </p>
                        <p style="float: right;">
                            Date :
                            <asp:Label runat="server" Text="" ID="lbldate"></asp:Label>
                        </p>
                        <div style="clear: both;"></div>
                    </div>
                </td>
            </tr>

            <tr>
                <td>
                    <div style="width: 100%;">
                        <div class="ftL">
                            <p>
                                <b>From: 
                                <asp:Label runat="server" Text="" ID="lblfrom"></asp:Label></b>
                            </p>
                            <p>
                                Name of the site:
                                <asp:Label runat="server" Text="" ID="lblnameOfSite"></asp:Label>
                            </p>
                            <p>
                                <asp:Label runat="server" Text="" ID="lblLocationname"></asp:Label>
                            </p>
                        </div>

                        <div class="ftR">
                            <p>
                                <b>TO:
                                <asp:Label runat="server" Text="" ID="lblto"></asp:Label></b>
                            </p>
                            <p>Purchase Department</p>
                            <p>SNC, HO, Bangalore</p>
                        </div>
                        <div style="clear: both;"></div>
                    </div>
                </td>
            </tr>

            <tr>
                <td>

                    <asp:GridView ID="GridMaterialIndentPrint" runat="server" AutoGenerateColumns="false" CssClass="table tbl1 tbl_3"  FolderStyle="../Gridstyles/grand_gray">

                        <Columns>

                            <asp:TemplateField ItemStyle-Width="50px" HeaderText="Category Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblCatName" runat="server" Text='<%#Eval("Category_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="50px" HeaderText="Item Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemNAme" runat="server" Text='<%#Eval("Item_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="50px" HeaderText="BOQ">
                                <ItemTemplate>
                                    <asp:Label ID="lblBoq" runat="server" Text='<%# Eval("BOQ").ToString() != string.Empty  && Eval("BOQ").ToString() == "True" ? "Yes" : "No" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="50px" HeaderText="BOQ No">
                                <ItemTemplate>
                                    <asp:Label ID="lblboqNo" runat="server" Text='<%#Eval("BOQ_No")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="50px" HeaderText="UOM">
                                <ItemTemplate>
                                    <asp:Label ID="lblUOM" runat="server" Text='<%#Eval("UOM")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="50px" HeaderText="Total Qty involved in this">
                                <ItemTemplate>
                                    <asp:Label ID="lbltotqtyinvolved" runat="server" Text='<%#Eval("Total_Qty_Involved")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ItemStyle-Width="50px" HeaderText="Total Qty Received">
                                <ItemTemplate>
                                    <asp:Label ID="lbltotqtyReceived" runat="server" Text='<%#Eval("Total_Qty_Recevied")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="50px" HeaderText="Qty Available">
                                <ItemTemplate>
                                    <asp:Label ID="lblAvlQty" runat="server" Text='<%#Eval("Qty_Available")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="50px" HeaderText="Qty Required">
                                <ItemTemplate>
                                    <asp:Label ID="lblreqQty" runat="server" Text='<%#Eval("Qty_required")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="50px" HeaderText="Tentative Date of Requirement">
                                <ItemTemplate>
                                    <asp:Label ID="lbltentativedate" runat="server" Text='<%#Eval("Tentative_Date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-Width="50px" HeaderText="Whether Req Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblwheatherReq" runat="server" Text='<%# Eval("Whether_Req_Qty").ToString() == "True" ? "Yes" : "No" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                             <asp:TemplateField ItemStyle-Width="50px" HeaderText="Asset Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssetCode" runat="server" Text='<%# Eval("AssetCode")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ItemStyle-Width="50px" HeaderText="Registration No">
                                <ItemTemplate>
                                    <asp:Label ID="lblReg_No" runat="server" Text='<%# Eval("Reg_No")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ItemStyle-Width="50px" HeaderText="Total Running Hours">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalRunningHrs" runat="server" Text='<%# Eval("TotalRunningHrs")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField ItemStyle-Width="50px" HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>
                </td>
            </tr>

        </table>

        <br />

        <table class="tbl2" style="width: 825px; border: 0px solid white">
            <tr>
                <td>NOTE <asp:Label ID="lblnote" runat="server" style="padding-left:10px"></asp:Label></td>
            </tr>
        </table>

        <br />
        <br />
         
        <table class="tbl3" style="width: 825px; border: 0px solid white">
            <tr>
                <td class="text-center">Prepared By
                    <br />
                    <br />


                    <asp:Label ID="lbllPrepBy" runat="server"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />

                    <b>P&M Incharge</b>
                </td>
                <td>Stock Checked By
                    
                    <br />
                    <br />

                    <asp:Label ID="lblstockcheckby" runat="server"></asp:Label>

                    <br />
                    <br />
                    <br />
                    <br />
                    <b>Sr. Executive-stores</b>
                </td>
                <td>
		<!--Stock Checked By
                    <br />
                    <br />
                    <asp:Label ID="lblstchkby" runat="server"></asp:Label>-->
                    <br />
                    <br />
                    <br />
                    <br /> 
		    <br /> 
		    <br />
                    <b>Admin</b>
                </td>
                <td>Qty & Specification
                  
                  
                    <br />
                    <br />
                    <asp:Label ID="lblQtySpcfics" runat="server"></asp:Label>

                    <br />
                    <br />
                    <br />
                    <br />
                    <b>Deputy PM / Manager</b>
                </td>
                <td>
<!--Approved by
                   
                  
                    <br />
                    <br />
                    <asp:Label ID="lblApprovedBy" runat="server"></asp:Label> -->

                    <br />
                    <br />
                    <br />
                    <br />
<br />
<br />
                    <b>Project Incharge</b>
                </td>
            </tr>
        </table>
        <br />
        <br /> 
        <table class="tbl4" style="width: 825px; border: 0px solid white">
            <tr>
                <td style="text-align:left;font-weight:normal" colspan="5" >HO Approvals : <br /> <br /> </td>
               
            </tr>
              <tr>
                <td style="text-align:left" colspan="5" ></td>
               
            </tr>
            <tr>
                 <td style="width:200px;"></td>
		<td>PE</td>
                <td>PC</td>
                <!--<td>DP</td>-->
                <td>MD/CH</td>
                <td></td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
    </div>
</asp:Content>
