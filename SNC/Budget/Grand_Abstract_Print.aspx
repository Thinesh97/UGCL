<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC_Print.Master" AutoEventWireup="true" CodeBehind="Grand_Abstract_Print.aspx.cs" Inherits="Grand_Abstract_Print" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <style>
        table td:nth-child(3) {
        text-align:right;
        }
    </style>

    <table border="1" class="table table-bordered" style="width: 825px;">
	
	<tr >
		<td colspan="3" class="text-center text-bold">
        SHANKARANARAYANA CONSTRUCTIONS P LTD. <br />
            PROJECT: <asp:Label ID="lblProjectName" Text="" runat="server" />
            <br />
            MONTHLY EXPENSES BUDGET - <asp:Label ID="lblBudget_ID" Text="" runat="server" /> 2015</td>
		
	</tr>
	<tr >
		<td colspan="3" class="text-center text-bold">A. PURCHASE BUDGET</td>
	</tr>
    <tr>
        <td colspan="3">

            <asp:GridView ID="GridPrint" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="Sl No. " DataField="SINO"/>
                    <asp:BoundField HeaderText="Particulars" DataField="Bud_type"/>
                    <asp:BoundField HeaderText="Amount in Rs." DataField="Amt"/>
                   
                  
                </Columns>
            </asp:GridView>
        </td>
    </tr>

	
	<tr >
		<td></td>
		<td>GRAND TOTAL</td>
		<td><asp:Label ID="lblGrandTotal" Text="" runat="server" /></td>
	</tr>
</table>

</asp:Content>
