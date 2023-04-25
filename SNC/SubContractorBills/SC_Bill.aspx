<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="SC_Bill.aspx.cs"  Inherits="SNC.SubContractorBills.SC_Bill"%>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <script>
        function ConfirmDelete() {
            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                return false;
            }
            return true;
        }
    </script>
     <script type="text/javascript">
         $(document).ready(function () {
             $('.chosen-select').chosen();
         });
     </script>
  
   
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>
                Generate Sub Contractor Bill
            </h3>
        </div>
        <div class="panel-body">
            <!------------------------------------------------------------Body Content-------------------------------------------------------------------->
          <div class="row" runat="server">
              <div class="col-md-2">
                   RA Bill No
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtRA_BillNo"  CssClass="form-control" runat="server"></asp:TextBox>
                </div>  
              <div class="col-md-2">
                  Bill Date&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSCBillDate" CssClass="Validation_Text" ValidationGroup="ValMRN" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtSCBillDate" CssClass="form-control" onkeypress="javascript:return false;" onpaste="javascript:return false;" runat="server" TabIndex="2" AutoComplete="Off"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd-MM-yyyy" TargetControlID="txtSCBillDate"></ajaxToolkit:CalendarExtender>
                </div>
           </div>

            <div class="row">
                 <div class="col-md-2">
                    Project Name&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" InitialValue="-Select-" ControlToValidate="ddlProject" CssClass="Validation_Text" ValidationGroup="ValWO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlProject" class="chosen-select form-control" TabIndex="4"></asp:DropDownList>
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
                <div runat="server" id="div_SubCon">
                    <div class="col-md-2">Sub Contractor Name</div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="ddlSubContractor" class="chosen-select form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSubContractor_SelectedIndexChanged"  TabIndex="4"></asp:DropDownList>
                    </div>
                </div>
                      <div class="col-md-2">
                   Select WO&nbsp;
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="-Select-" runat="server" ControlToValidate="ddlWO" CssClass="Validation_Text" ValidationGroup="ValSC" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlWO" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWO_SelectedIndexChanged"  CssClass="form-control" TabIndex="3"></asp:DropDownList>
                </div>        

              </div>
            <div class="row" runat="server" id="div_WO">
                <div class="col-md-2">
                  Billing Period From&nbsp;
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBillingPeriodFrom" CssClass="Validation_Text" ValidationGroup="ValSC" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                     <asp:TextBox ID="txtBillingPeriodFrom" CssClass="form-control"  runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender3" Format="dd-MM-yyyy" TargetControlID="txtBillingPeriodFrom"></ajaxToolkit:CalendarExtender>
                </div>
                    <div class="col-md-2">
                    Billing Period To&nbsp;
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBillingPeriodTo" CssClass="Validation_Text" ValidationGroup="ValSC" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtBillingPeriodTo" CssClass="form-control"  runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" Format="dd-MM-yyyy" TargetControlID="txtBillingPeriodTo"></ajaxToolkit:CalendarExtender>
                </div>
            </div>
             <div class="row" runat="server" id="div1">
               
                <div class="col-md-2">
                  Wor Description
                </div>
                <div class="col-md-6">
                    <asp:HiddenField runat="server" ID="hdnSC_Bill_ID" />
                    <asp:TextBox ID="txtWorkDescription" CssClass="form-control" TextMode="MultiLine"  runat="server"></asp:TextBox>
                     </div>
                 <div class="col-md-4">
                  <asp:CheckBox ID="CheckBoxDPR" runat="server" Text="DPR" />
                 <asp:CheckBox ID="CheckBoxNMR" runat="server" Text="NMR" />  
                      
                </div>
                
            </div>
            <br />
            <br />
            <div class="col-md-12 text-center">
                 <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ValMRN" OnClick="btnSCBill_Click"  CssClass="btn btn-default"  TabIndex="9"></asp:Button>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" TabIndex="10"></asp:Button>
                 <asp:Button ID="btnAddTax" Text="Add Tax" CssClass="btn btn-default" runat="server" OnClick="btnAddTax_Click" CausesValidation="false" Visible="false" TabIndex="23" />
                 <asp:Button ID="btnAddRetention" Text="Add Retention" CssClass="btn btn-default" runat="server" OnClick="btnAddRetention_Click" CausesValidation="false" Visible="false" TabIndex="23" />
                <asp:Button ID="btnAddDPR" runat="server" Text="ADD DPR" CssClass="btn btn-default" OnClick="btnAddDPR_Click" ValidationGroup="ValSC" Visible="false" TabIndex="10"></asp:Button>
                <asp:Button ID="btnAddPayment" runat="server" Text="ADD Previous Payments" CssClass="btn btn-default" OnClick="btnAddPaymen_Click" Visible="false"  ValidationGroup="ValSC" TabIndex="10"></asp:Button>
                <asp:Button ID="btnIssuedMaterial" runat="server" Text="ADD Issued Material" CssClass="btn btn-default" OnClick="btnAddMIN_Click" Visible="false"  ValidationGroup="ValSC" TabIndex="10"></asp:Button>
                  <asp:LinkButton ID="lbtnPrint" runat="server" Text="Print" Visible="false" CssClass="btn btn-default" CausesValidation="false" OnClick="btnPrint_Click" OnClientClick="document.forms[0].target = '_blank';" TabIndex="19" />
         
            <br />
            <br />
           </div>
            </div>
                <br />
        
                 <div class="panel panel-default" runat="server" id="Div_DPRList">
                     <h4 style="text-align:center">Daily Progress Report list </h4>
             <div class="row" runat="server" id="dpradd">
                  <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="Grid_DPR"   CallbackMode="false" AutoGenerateColumns="false"  OnDeleteCommand="Grid_DPR_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" OnRowDataBound="Grid_DPR_RowDataBound" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true"   PageSize="10">
           
              <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
            <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />

            <Columns>
                   <ogrid:Column DataField="DPR_No" HeaderText="DPR_No" runat="server" Width="150" >
        </ogrid:Column>      
                
                <ogrid:Column DataField="Location_Chainage" HeaderText="Location Chainage" Width="110px" ReadOnly="true" ></ogrid:Column>
                <ogrid:Column DataField="Work_Done_Activity" HeaderText="Work Done Activity" Width="120px" Wrap="true" ReadOnly="true"></ogrid:Column>
                 <ogrid:Column DataField="WO_Des" HeaderText="Work Description" Width="250px" ReadOnly="true"></ogrid:Column>
                <ogrid:Column DataField="Present_Progress" HeaderText="Present Progress" Width="100" ReadOnly="true" ></ogrid:Column>
                 <ogrid:Column DataField="Cumulative_Progress" HeaderText="Cumulative Progress" Width="100" ReadOnly="true" ></ogrid:Column>
                <ogrid:Column DataField="ApprovedBy" HeaderText="Approved By" ReadOnly="true" ></ogrid:Column>
                <ogrid:Column DataField="UOM" HeaderText="UOM" ReadOnly="true" Width="150" ></ogrid:Column>
                 <ogrid:Column DataField="Date" HeaderText="Date" Wrap="true" ></ogrid:Column>
                <ogrid:Column DataField="Remarks" HeaderText="Remarks"  Width="120px" ReadOnly="true" ></ogrid:Column>
                <ogrid:Column DataField="DPRFile_Path" HeaderText="DPRFile_Path"  Width="140px" ReadOnly="true" ></ogrid:Column>
                   <ogrid:Column  HeaderText="Delete" AllowDelete="true" Width="70"  ></ogrid:Column>
              
            </Columns>
            
             <Templates>
    </Templates>
        </ogrid:Grid>
                  </center>      
                 <br />
               
                    
        </div>
                 </div>

 </div>
        <div class="panel panel-default" runat="server" id="Div_NMRList">
                     <h4 style="text-align:center">Nominal Master Roll list </h4>
             <div class="row" runat="server" id="Div6">
                  <div class="panel-body">
            <center>
         <ogrid:Grid runat="server" ID="Grid_LabourList"   CallbackMode="false" AutoGenerateColumns="false"   FolderStyle="../Gridstyles/grand_gray"  AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true"   PageSize="10">
           
              <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
            <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />

            <Columns>
                   <ogrid:Column DataField="ID" HeaderText="ID" Width="100px"  runat="server" >
            <TemplateSettings  TemplateId="NMR_NoTemplate"/>
        </ogrid:Column>      
              
              
                <ogrid:Column DataField="Labour_Date" HeaderText="Date" Width="100px" ReadOnly="true" ></ogrid:Column>
                  <ogrid:Column DataField="Labour_Type" HeaderText="Labour Type" Width="210px" ReadOnly="true" ></ogrid:Column>
                 <ogrid:Column DataField="NoOf_Labour" HeaderText="No Of Labour" ReadOnly="true" Width="210px" ></ogrid:Column>
                <ogrid:Column DataField="Labour_Rate" HeaderText="Rate" ReadOnly="true"  Width="200px" ></ogrid:Column>
                 <ogrid:Column DataField="LabourCost_Total" HeaderText="Total Cost"  Width="110px" Wrap="true" ></ogrid:Column>
                
            </Columns>
            
               <Templates>
                        <ogrid:GridTemplate ID="NMR_NoTemplate" runat="server">
                            <Template>
                               <asp:HyperLink ID="lnk_NMR_No" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["ID"] %>'>
                        </asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
    </Templates>
        </ogrid:Grid>
                  </center>      
                 <br />
               
                    
        </div>
                 </div>

 </div>
           <div class="panel panel-default">
                     <h4 style="text-align:center"> Previous Payments Detail</h4>
             <div class="row" runat="server" id="Div_PaymentDetail">
                  <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="Grid_Payment_Indent"   CallbackMode="false" AutoGenerateColumns="false"   FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true"   PageSize="10">
           
              <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
            <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />

            <Columns>
                <ogrid:Column DataField="PayInd_No" HeaderText="PayInd No" Width="110px" ReadOnly="true" ></ogrid:Column>
                <ogrid:Column DataField="PayInd_Date" HeaderText="Payment Date" Width="120px" Wrap="true" ReadOnly="true"></ogrid:Column>
                <ogrid:Column DataField="Amt_Transferable" HeaderText="Amount Transfered" Width="120px" ReadOnly="true"></ogrid:Column>
                <ogrid:Column DataField="Bank_Name" HeaderText="Bank" ReadOnly="true" ></ogrid:Column>
                <ogrid:Column DataField="Bank_Branch" HeaderText="Branch" ReadOnly="true" ></ogrid:Column>
                 <ogrid:Column DataField="Bank_Account" HeaderText="Account No" Wrap="true" ></ogrid:Column>
                <ogrid:Column DataField="Bank_IFSC" HeaderText="IFSC Code"  Width="120px" ReadOnly="true" ></ogrid:Column>
                <ogrid:Column DataField="POWO_ID" HeaderText="Work Oder No" ReadOnly="true" ></ogrid:Column>
                <ogrid:Column DataField="Payment_Mode" HeaderText="Payment Mode"  Width="140px" ReadOnly="true" ></ogrid:Column>
                 <ogrid:Column DataField="Narration" HeaderText="Narration"  Width="140px" ReadOnly="true" ></ogrid:Column>
            </Columns>
            
             <Templates>
    </Templates>
        </ogrid:Grid>
                     
                      </center>
        </div>
                 </div>

 </div>
         <div class="panel panel-default">
                     <h4 style="text-align:center"> Issued Material Detail</h4>
             <div class="row" runat="server" id="Div_btnIssuedMaterial">
                  <div class="panel-body">
            <center>
                   <ogrid:Grid runat="server" ID="Grid_MIN" CallbackMode="false" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" 
                    AllowAddingRecords="false" AllowFiltering="true" AllowRecordSelection="true" AllowGrouping="true" AllowPaging="true" PageSize="10" OnUpdateCommand="Grid_MIN_UpdateCommand"  >
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  
                        CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
                    <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />
       
            <Columns>
                <ogrid:Column DataField="MIN_Item_ID" HeaderText="MIN_Item_ID" Visible="false" Width="180px" Wrap="true" ReadOnly="true"></ogrid:Column>
                <ogrid:Column DataField="MIN_No" HeaderText="MIN No" Width="180px" Wrap="true" ReadOnly="true"></ogrid:Column>
                 <ogrid:Column DataField="DATE" HeaderText="MIN Date" Width="180px" ReadOnly="true"></ogrid:Column>
                 <ogrid:Column DataField="Item_Name" HeaderText="Item Issued" Width="180px" ReadOnly="true"></ogrid:Column>
                <ogrid:Column DataField="ApprovedQty" HeaderText="Approved Quantity"  Width="180px" ReadOnly="true"  ></ogrid:Column>
                <ogrid:Column DataField="UOMPrefix" HeaderText="UOM" ReadOnly="true" Width="120px" ></ogrid:Column>
                <ogrid:Column DataField="Rate" HeaderText="Rate"  Width="120px">
                           <TemplateSettings EditTemplateId="EditRate_MIN" />
                        </ogrid:Column>
                 <ogrid:Column AllowEdit="true" Width="90px"></ogrid:Column>
            </Columns>
            
             <Templates>
                         <ogrid:GridTemplate runat="server" ID="EditRate_MIN" ControlID="txtRate_MIN" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtRate_MIN" Width="150px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
    </Templates>
        </ogrid:Grid>
                     
                      </center>
        </div>
                 </div>

 </div>

        <div class="panel panel-default">
              <h4 style="text-align:center">Tax Details</h4>
             <div class="row" runat="server" id="Div4">
                  <div class="panel-body">
            <center>
             <ogrid:Grid runat="server" ID="Grid_Tax"  Width="80%"  CallbackMode="false" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray"  AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true"   PageSize="10">
           
              <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
            <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />

            <Columns>
                <ogrid:Column DataField="IGST" HeaderText="IGST" Width="120px" ReadOnly="true" ></ogrid:Column>
                <ogrid:Column DataField="CGST" HeaderText="CGST" Width="120px" Wrap="true" ReadOnly="true"></ogrid:Column>
                 <ogrid:Column DataField="SGST" HeaderText="SGST" Width="120px" ReadOnly="true"></ogrid:Column>
                <ogrid:Column DataField="TDS" HeaderText="TDS" Width="120px" ReadOnly="true" ></ogrid:Column>
                 <ogrid:Column DataField="RetentionPerc" HeaderText="Retention %" Width="120px" ReadOnly="true" ></ogrid:Column>
                <ogrid:Column DataField="RetentionAmount" HeaderText="RetentionAmount" ReadOnly="true" Width="120px" ></ogrid:Column>
                
            </Columns>
            
             <Templates>
    </Templates>
        </ogrid:Grid>
                 </center>      
                 <br />
               
                    
        </div>
                 </div>
        </div>
            </div>
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
                                <asp:ListItem Text="TDS" Value="TDS"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row" runat="server" id="div_Igst">
                        <div class="col-md-2">
                            IGST Perc&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtIgstPercPO" runat="server" onkeyup="CalculateItemTaxAmtPO('txtIgstPercPO','txtIgstAmtPO')" autocomplete="off" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                        <%--<div class="col-md-2">
                            Amount
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtIgstAmtPO" Enabled="false" runat="server" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>--%>
                    </div>
                    <div class="row" runat="server" id="div_Cgst" visible="false">
                        <div class="col-md-2">
                            CGST Perc&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtCgstPercPO" runat="server" onkeyup="CalculateItemTaxAmtPO('txtCgstPercPO','txtCgstAmtPO')" autocomplete="off"  onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                        <%--<div class="col-md-2">
                            Amount&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtCgstAmtPO" Enabled="false" runat="server" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>--%>
                    </div>
                    <div class="row" runat="server" id="div_Sgst" visible="false">
                        <div class="col-md-2">
                            SGST Perc&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtSgstPercPO" runat="server" onkeyup="CalculateItemTaxAmtPO('txtSgstPercPO','txtSgstAmtPO')" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                       <%-- <div class="col-md-2">
                            Amount&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtSgstAmtPO" Enabled="false" runat="server" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>--%>
                    </div>
                     <div class="row" runat="server" id="div_TDS" visible="false">
                        <div class="col-md-2">
                            TDS Perc&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtTDSperc" runat="server" onkeyup="CalculateItemTaxAmtPO('txtSgstPercPO','txtSgstAmtPO')" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                       <%-- <div class="col-md-2">
                            Amount&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtSgstAmtPO" Enabled="false" runat="server" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>--%>
                    </div>
                </div>
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnSaveTax" runat="server" Text="UpdateTax" CssClass="btn btn-default" OnClick="btnSaveTax_Click" />
                        <asp:Button ID="BtnCanelTax" runat="server" Text="Cancel" CssClass="btn btn-default" CausesValidation="false" OnClick="btnCancelTax_Click" />
                 </center>

                </div>
            </div>

        </div>

    </asp:Panel>
       
     <asp:Button ID="Button2" runat="server" Style="display: none" Text="Button" />

    <ajaxToolkit:ModalPopupExtender ID="ModelRetentionPopup" runat="server" PopupControlID="PanelRetention" TargetControlID="Button2"
        CancelControlID="btnClose2" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelRetention" runat="server" align="center" Style="display: none">

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnClose2" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>   <h5 >Add Retention</h5></center>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-12">
                            <asp:RadioButtonList runat="server" ID="rblRetention" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbtntype_SelectedIndexChanged">
                                <asp:ListItem Text="Percentage Value" Value="Percentage" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Amount Value" Value="Amount"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row" runat="server" id="div2">
                        <div class="col-md-2">
                           Retention In Percentage &nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtRetentionPrec" runat="server" onkeyup="CalculateItemTaxAmtPO('txtIgstPercPO','txtIgstAmtPO')" autocomplete="off" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" runat="server" id="div3" visible="false">
                        <div class="col-md-2">
                            Retention In Amount&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtRetentioninAmont" runat="server" onkeyup="CalculateItemTaxAmtPO('txtCgstPercPO','txtCgstAmtPO')" autocomplete="off"  onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                      
                    </div>
                   
                </div>
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnSaveRetention" runat="server" Text="UpdateTax" CssClass="btn btn-default" OnClick="btnSaveRetention_Click" />
                        <asp:Button ID="btnCancelRetention" runat="server" Text="Cancel" CssClass="btn btn-default" CausesValidation="false" OnClick="btnCancelRetention_Click" />
                 </center>

                </div>
            </div>

        </div>

    </asp:Panel>
        </asp:Content>

    

