<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="PaymentIndentList_Verification.aspx.cs" Inherits="PaymentIndentList_Verification" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript">
        function exportgrid1() {
            debugger;
            Grid_PaymentIndent_Verify.exportToExcel();
        }

        function ConfirmDelete() {
            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                return false;
            }
            return true;
        }
        

        function beforedelete() {
            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                return false;
            }
            return true;
        }
    </script>

    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Payment Indent List For Verification

            </h3>
        </div>

        <div class="panel-body">
            <center>
                <ogrid:Grid runat="server" ID="Grid_PaymentIndent_Verify" CallbackMode="false" AutoGenerateColumns="false"  FolderStyle="../Gridstyles/grand_gray"
                    AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10"
                    OnRowDataBound="Grid_PaymentIndent_Verify_RowDataBound" >
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"/>
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  
                        CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
                    <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />
                    <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
            
                    <Columns>
                        <ogrid:Column DataField="PayInd_No" HeaderText="Payment Indent No" runat="server" Width="150px" >
                            <TemplateSettings  TemplateId="PayIndNoTemplate"/>
                        </ogrid:Column>      
                        <ogrid:Column DataField="PayInd_Date" HeaderText="Date" Width="110px" DataFormatString="{0:d}"></ogrid:Column>
                        <ogrid:Column DataField="State_Name" HeaderText="State" Width="150px"></ogrid:Column>
                        <ogrid:Column DataField="Project_Name" HeaderText="Project Name" Width="200px"></ogrid:Column>
                        <ogrid:Column DataField="Ben_Name" HeaderText="Company Name" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Name" HeaderText="Bank Name" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Branch" HeaderText="Branch Name" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Account" HeaderText="Account Number"></ogrid:Column>
                        <ogrid:Column DataField="Bank_IFSC" HeaderText="IFSC Code"></ogrid:Column>
                        <ogrid:Column DataField="Amt_PartPayment" HeaderText="Amount"></ogrid:Column>
                        <ogrid:Column DataField="WorkDoneFor" HeaderText="Work Done For/Item Supplied to"></ogrid:Column>
                        <ogrid:Column DataField="Status" HeaderText="Status"></ogrid:Column>
                        <ogrid:Column DataField="Name" HeaderText="Prepared By"></ogrid:Column>
                        <ogrid:Column  HeaderText="Download Document" Width="200px" Wrap="true" >
                            <TemplateSettings TemplateId="lnkDownloadDocument_Verify_Template" />
                        </ogrid:Column>
                    </Columns>
                
                    <Templates>
                        <ogrid:GridTemplate ID="PayIndNoTemplate" runat="server">
                            <Template>
                                <asp:HyperLink ID="lnkPayIndNo" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["PayInd_No"] %>'></asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate ID="lnkDownloadDocument_Verify_Template" runat="server">
                            <Template>
                            <asp:LinkButton ID="lnkWOItem" Text="View Document" CommandArgument='<%#Container.DataItem["PayInd_No"] %>' CommandName='<%#Container.DataItem["PayInd_No"] %>'  OnClick="lnkDownloadDocumentItem_Click" runat="server" CssClass="gridCB">
                                        </asp:LinkButton>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
                </ogrid:Grid>
                
                <br />
                <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" OnClick="btnExportToPDF_Click" CssClass="btn btn-default"></asp:Button>
                <input onclick="exportgrid1()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />   
                <br />
                <br />

            </center>
        </div>
    </div>
        <asp:Button runat="server" ID="btnAddSubItem" Style="display: none"></asp:Button>
    <ajaxToolkit:ModalPopupExtender ID="ModalWOSubItem" runat="server" PopupControlID="PanelWOSubItem" TargetControlID="btnAddSubItem"
        CancelControlID="BtnClose1" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    
    <asp:Panel ID="PanelWOSubItem" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="BtnClose1" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center><h5 id="myModalamt11"><asp:Label ID="lblPendingGrid_DocDownload" runat="server" ></asp:Label></h5></center>
                </div>
                <div class="modal-body">

                  <center>
            <ogrid:Grid runat="server" ID="Grid_File" CallbackMode="false" PageSize="5"
                OnDeleteCommand="Grid_File_DeleteCommand"
                AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowPaging="true">
                <ScrollingSettings ScrollWidth="70%" />
                <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />

                <Columns>
                    <ogrid:Column DataField="File_Type" HeaderText="File Type" Width="150px"></ogrid:Column>
                    <ogrid:Column DataField="File_Name" HeaderText="File Name" Width="400px">
                        <TemplateSettings TemplateId="FileTemplate" />
                    </ogrid:Column>
                    <ogrid:Column HeaderText="Delete" AllowDelete="true"></ogrid:Column>

                </Columns>

                <Templates>
                    <ogrid:GridTemplate ID="FileTemplate" runat="server">
                        <Template>
                            <asp:LinkButton ID="lnkDownloadFile" runat="server" Text='<%#Container.DataItem["File_Name"].ToString() %>' OnClick="lnkDownloadFile_Click" CssClass="gridCB" ForeColor="#337ab7"></asp:LinkButton>
                        </Template>
                    </ogrid:GridTemplate>
                </Templates>

            </ogrid:Grid>
        </center>

                </div>
               <%-- <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnSaveWOSubItem" runat="server" Text="Save" CssClass="btn btn-default" ValidationGroup="ValSubItem" OnClick="btnSaveWOSubItem_Click" />
                        <asp:Button ID="btnCancelSubItem" runat="server"  Text="Cancel"  CssClass="btn btn-default" CausesValidation="false" OnClick="btnCancelWOSubItem_Click" />        
                 </center>

                </div>--%>
            </div>
        </div>

    </asp:Panel>
</asp:Content>
