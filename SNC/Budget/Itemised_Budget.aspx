<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC_Print.Master" AutoEventWireup="true" CodeBehind="Itemised_Budget.aspx.cs" Inherits="Itemised_Budget" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        
        table th {
            font-size:12px;
        }
        table tr td:nth-child(4),td:nth-child(5),td:nth-child(6),td:nth-child(7),td:nth-child(8) {
        text-align:right;
        }
    </style>
    <table  width="825px" border="1" class="table table-bordered" style="width:825px;">
	
	<tr height="31" class="">
		<th colspan="8" class="text-center">
            <div class="text-right">Month: <asp:Label runat="server" ID="lblMonth" Text=""></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;Year:<asp:Label runat="server" ID="lblYear" Text=""></asp:Label></div>
		Shankaranarayana Constructions Pvt Ltd. </th>
		
	</tr>
	
	<tr >
		<th colspan="8" class="text-center">
		 PROJECT : &nbsp;<asp:Label ID="lblprojectName" runat="server" Text=""></asp:Label>
		</th>
		
	</tr>
	<tr >
		<th colspan="8" class="text-center"> 
            ITEMISED BUDGET:&nbsp;&nbsp;<asp:Label runat="server" Text="" ID="lblItemisedBudget"></asp:Label><br />
		</th>
		
	</tr>
       <tr>
           <asp:Label runat="server" CssClass="h1" Text="0"  ID="lblTotal" Style="display:none" ></asp:Label>
           <td>
              <center>
                               

 <asp:GridView runat="server" ShowFooter="true" ID="BudgetGridPrint"   CssClass="table tbl1" OnRowDataBound="BudgetGridPrint_RowDataBound" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" >
                        <Columns>
                            
                              <asp:BoundField DataField="Category_Name" HeaderText="Category" ItemStyle-Width="50px" />
                              <asp:BoundField DataField="Item_Name" HeaderText="Item" />
                             <asp:TemplateField  HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnitValues" Text='<%# Eval("UOMName") %>'  ></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                   <b> <asp:Label runat="server" ID="lblTotalName" Text="Total" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                              
                              <asp:BoundField DataField="Req_Qty" HeaderText="Required Qty" />
                              <asp:BoundField DataField="Rate" HeaderText="Rate" />
                       
                            <asp:TemplateField  HeaderText="Values Of Purchase" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPurchaseValues"  Text='<%# Eval("Purc_Value") %>'   ></asp:Label>
                                    
                                </ItemTemplate>
                                <FooterTemplate>
                                     <b> <asp:Label runat="server" ID="lblTotalPOV"  Text="0" > </asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>                                          
                              <asp:BoundField DataField="HO_Value" HeaderText="HO"  />
                             <asp:BoundField DataField="Local_Value" HeaderText="Local" />
                            
                        </Columns>                        
                    </asp:GridView> 
                 
                     </center>
           </td>
           </tr>
      



     

</table>
    
 
                         
                  


</asp:Content>
