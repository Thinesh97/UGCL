<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC_Authantication.Master" AutoEventWireup="true" EnableEventValidation = "false" CodeBehind="UGCL_MRNReportViewer.aspx.cs" Inherits="SNC.Reports.UGCL_MRNReportViewer" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <
    
  <%--  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <asp:UpdatePanel runat="server" ID="Upd">
            <ContentTemplate>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" BackColor="#f9e79f" Height="650px" HyperlinkTarget="_blank"
                    PageCountMode="Actual">
                    <LocalReport EnableHyperlinks="True">
                    </LocalReport>
                </rsweb:ReportViewer>
                
                
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>--%>
    <div>
                     <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                          <tbody>
                            <tr>
					            <td colspan="10">
				                    <table border="0" cellpadding="0" cellspacing="0" style="width:80%">
					                    <tbody>
                                            <tr>
					                            <td colspan="" style="width: 100%;" height="20%">
                                                  <%--  <img src="../Style/Images/HeaderImg.png"/>--%>

					                                <img src="../Images/print-header.jpg"   style="width:1300px" />
					                            </td>
                                            </tr>
                                            <tr>
                                                <td colspan="8" style="font-weight:700;vertical-align: middle; text-align:center; ">
                                                    <div style="font-size: 20px !important;margin-left: 12%;"> <asp:Label runat="server" Text="Report"></asp:Label></div>
                                                </td>
                                            </tr>
					                    </tbody>
				                    </table>
					            </td>
					        </tr>
                              <tr>

                              </tr>
                              </tbody>
                         </table>
                </div>
    <div runat="server">

                 
                      

       <asp:GridView ID="GridItemsMRNReport"  EmptyDataText="no records found" ShowHeaderWhenEmpty="true" AllowAddingRecords="false"  ShowFooter="true"  FolderStyle="../Style/GridStyles/style_7"
                                        runat="server" AutoGenerateColumns="False" Width="100%">
                <%--<ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"/>--%>
                         <%--ColumnsToExport="EmployeeID,Full_Name,Actual_Date_of_Joining,Designation,Location,Status" />--%>
                    <%--<CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/> --%>
                                        <Columns>
                                            <asp:BoundField DataField="MRN_No"  HeaderText="MRN Number" ItemStyle-Width="20px"></asp:BoundField>
                                            <asp:BoundField DataField="MRNDATE"  HeaderText="MRN Date" DataFormatString="{0:dd/MM/yyyy}" ></asp:BoundField>
                                            <asp:BoundField DataField="Vendor_name"  HeaderText="Vendor Name"></asp:BoundField>
                                            <asp:BoundField DataField="PONo"  HeaderText="PO No"></asp:BoundField>
                                            <asp:BoundField DataField="PODate"  HeaderText="PO Date" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                                            <%--<asp:BoundField DataField="Creation_dttm" DataFormatString="{0:dd/MM/yyyy}" HeaderText="BG Requisition Date"></asp:BoundField>--%>
                                            <asp:BoundField DataField="Invoice_No"  HeaderText="Invoice No"></asp:BoundField>
                                            <%--<asp:BoundField DataField="App_dec_Dt" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Requisition Approved Date"></asp:BoundField>--%>
                                            <asp:BoundField DataField="LedgerHead"  HeaderText="Ledger Head"></asp:BoundField>
                                            <asp:BoundField DataField="material_Name"  HeaderText="Material Name"></asp:BoundField>
                                            <asp:BoundField DataField="PO_Qty"  HeaderText="PO Qty."></asp:BoundField>
                                            <asp:BoundField DataField="MRNReceived_Qty"  HeaderText="Received Qty"></asp:BoundField>
                                            <asp:BoundField DataField="MRNAccepted_Qty" HeaderText="Accepted Qty"></asp:BoundField>
                                            <asp:BoundField DataField="MRNPending_Qty" HeaderText="Pending Qty"></asp:BoundField>
                                            <asp:BoundField DataField="Rate_per_Unit" HeaderText="Rate per Unit"></asp:BoundField>
                                            <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                            <asp:BoundField DataField="TotalTax" HeaderText="TAX %"></asp:BoundField>
                                            <asp:BoundField DataField="MRNTaxAmt" HeaderText="TAX Amount"></asp:BoundField>
                                            <asp:BoundField DataField="MRNDiscountAmt" HeaderText="Discount Amount"></asp:BoundField>
                                            <asp:BoundField DataField="MRNItem_TransportCost" HeaderText="Transportation Amount"></asp:BoundField>
                                            <asp:BoundField DataField="MRNInvoiceAmt" HeaderText="Material Total Amount"></asp:BoundField>
                                            <%--<asp:BoundField DataField="File_Invoice" HeaderText="Invoice"></asp:BoundField>--%>
                                            <%--<asp:BoundField DataField="" HeaderText="PO Status"></asp:BoundField>--%>


                                    <asp:TemplateField>                                                 
                                    <ItemTemplate>
                <asp:LinkButton ID="lnkDownload" Text='<%# Eval("File_Invoice") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                    Invoice
                                    </HeaderTemplate>
                                    </asp:TemplateField>


                                          
                                           </Columns>
                                        <FooterStyle Font-Bold="true" Font-Size="Large" HorizontalAlign="Right" />
                                        <HeaderStyle Height="100%" BackColor="#FFC107" />
                                     </asp:GridView>
        <br /> <br />
                                    <centre>
                                        <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="ExportToExcel" CssClass="btn btn-default" />
                                        <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" OnClick="btnExportToPDF_Click" CssClass="btn btn-default"></asp:Button>
            <%--<input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />--%> 
                                                  </centre>

     
    </div>
</asp:Content>
