<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="RateCard.aspx.cs"  Inherits="SNC.SubContractorBills.RateCard" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <script type="text/javascript">
          $(document).ready(function () {
              $('.chosen-select').chosen();
          });
      </script>
    <script>
        function exportgrid() {
            Grid_DPR_Entry.exportToExcel();
        }
    </script>
        <script type="text/javascript">
           
             function beforedelete() {
                if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                    //alert('Check');
                    document.getElementById('<%=HF_Confirm.ClientID%>').value = "false";
                //alert(document.getElementById('<%=HF_Confirm.ClientID%>').value);
                return false;
            }
            else {
                document.getElementById('<%=HF_Confirm.ClientID%>').value = "true";
                    return true;
                }
            }
            

            function CalculateCumulativeAmount() {
                var CumulativeValue = document.getElementById("<%=txtRCQuantity_Item.ClientID%>").value;
                var PresentValue = document.getElementById("<%=txtRateRC_Item.ClientID%>").value;
                var added = parseInt(CumulativeValue) * parseInt(PresentValue);
                document.getElementById("<%=txtAmountRC_Item .ClientID%>").value = Math.round(added)
            }
        </script>

     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
  

    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

              Generate Rate Card

            </h3>
        </div>
        <div class="panel-body">
            <!------------------------------------------------------------Body Content-------------------------------------------------------------------->
       <div class="row">
             <div class="col-md-2">
                   RC Number&nbsp;
                </div>
           
                <div class="col-md-4">
                     <asp:TextBox ID="txtRCID"  Enabled="false" runat="server"  autocomplete="off" CssClass="form-control"></asp:TextBox>
                </div>
           
       </div>
             <div class="row">
            <div class="col-md-2">Financial Year 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="-Select-" ControlToValidate="ddlFYear" CssClass="Validation_Text" ValidationGroup="ValRC" ErrorMessage="*"></asp:RequiredFieldValidator>

            </div>
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
                        <asp:ListItem Value="22-23" Text="2022-2023" ></asp:ListItem>
                        <asp:ListItem Value="23-24" Text="2023-2024"></asp:ListItem>
                        <asp:ListItem Value="24-25" Text="2024-2025"></asp:ListItem>
                    </asp:DropDownList>
                </div>
           
                 <div class="col-md-2">Rate Card Date
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  ControlToValidate="txtRateCardDate" CssClass="Validation_Text" ValidationGroup="ValRC" ErrorMessage="*"></asp:RequiredFieldValidator>

                 </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtRateCardDate" CssClass="form-control"  autocomplete="off" runat="server" TabIndex="1"></asp:TextBox>
                <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd-MM-yyyy" TargetControlID="txtRateCardDate"></ajaxToolkit:CalendarExtender>
            </div>
            </div>
            <div class="row">
                   <div runat="server">
                    <div class="col-md-2">Sub Contractor Name
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="-Select-" ControlToValidate="ddlSubContractor" CssClass="Validation_Text" ValidationGroup="ValRC" ErrorMessage="*"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="ddlSubContractor" class="chosen-select form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSubContractor_SelectedIndexChanged"  TabIndex="4"></asp:DropDownList>
                    </div>
                </div>
                 <div class="col-md-2">
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="-Select-" ControlToValidate="ddlWO" CssClass="Validation_Text" ValidationGroup="ValRC" ErrorMessage="*"></asp:RequiredFieldValidator>

                   Select WO&nbsp;
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlWO" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlWO_SelectedIndexChanged" CssClass="form-control" TabIndex="3"></asp:DropDownList>
                </div>
               
            
            </div>
                <div class="row">
                  <div class="col-md-2">Work Order Type </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlWoTypeRC" CssClass="form-control" TabIndex="2">
                        <asp:ListItem Value="0" Text="-Select-" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="Labour Contract" Text="Labour Contract"></asp:ListItem>
                        <asp:ListItem Value="Work Contract" Text="Work Contract"></asp:ListItem>
                        <asp:ListItem Value="Proffessional Contract" Text="Proffessional Contract"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">  </div>
                 <div class="col-md-4"> <asp:CheckBox ID="chkFixed" runat="server"   style="font-weight: 700" Text="Fixed"  />
                       <asp:CheckBox ID="chkRecurring" runat="server"  style="font-weight: 700" Text="Recurring"   /> </div>

            </div>
            <div class="row">
                  <div class="col-md-2">
                    Project Name&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" InitialValue="-Select-" ControlToValidate="ddlProject" CssClass="Validation_Text" ValidationGroup="ValRC" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" Enabled="false" ID="ddlProject" class="chosen-select form-control" TabIndex="4"></asp:DropDownList>
                </div>
                  <div class="col-md-2">
                   Work Description&nbsp;
                </div>
                <div class="col-md-4">
                     <asp:TextBox ID="txtWorkDescription"   Enabled="false" runat="server" TextMode="MultiLine" autocomplete="off" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

        
            <br />
            <br />
            <div class="col-md-12 text-center">
                 <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="ValRC" CssClass="btn btn-default"  TabIndex="9"></asp:Button>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" TabIndex="10"></asp:Button>
               <asp:Button ID="btnAddItem" runat="server" Visible="false" Text="Add Rate Card Item" CssClass="btn btn-default" CausesValidation="false" OnClick="btnAddItem_Click" TabIndex="15"></asp:Button>
            </div>
            
            
            <br />
            <br />
           
            </div>

           <h4 style="text-align:center"> Rate Card list for WO Number - <span style="text-decoration-color:blue" runat="server" id="ID_WOno"></span></h4>
        <div id="Div_GridDPR" runat="server"> 
          <center>
                <ogrid:Grid runat="server" ID="Grid_RC_Entry" CallbackMode="false" AutoGenerateColumns="false" 
                    OnRowDataBound="Grid_DPR_RowDataBound"    OnDeleteCommand="Grid_RC_Item_DeleteCommand"
                    FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="10" >
                    <ScrollingSettings ScrollWidth="100%"  ScrollHeight="400"/>
                      <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"
                         ColumnsToExport="WONo,WODate,Subcon_name,Type_Name,DurationOfWork,Status,Name,Prepared_By,Other_Terms,Remarks" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

                    <Columns>
                        <%-- <ogrid:Column DataField="ID" HeaderText="DPR No /Click to Edit" Width="180" Wrap="true">
                            <TemplateSettings TemplateId="ItemTemplate" />
                        </ogrid:Column>--%>
                        <ogrid:Column DataField="ID" HeaderText="DPR No /Click to Edit" Width="150px" >
                             <TemplateSettings TemplateId="ItemTemplate" />
                        </ogrid:Column>
                        <ogrid:Column DataField="RateCard_ID" HeaderText="Rate Card ID" Width="100px" ></ogrid:Column>
                        <ogrid:Column DataField="Discription_Of_Work" HeaderText="Discription Of Work" Wrap="false"></ogrid:Column>
                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" Width="180px" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="UOMPrefix" HeaderText="UOM" Wrap="true"   Width="200px"></ogrid:Column>
                        <ogrid:Column DataField="Rate" HeaderText="Rate" Wrap="true"   Width="200px"></ogrid:Column>
                        <ogrid:Column DataField="Amount" HeaderText="Amount" Align="center" Width="150px"></ogrid:Column> 
                         <ogrid:Column  HeaderText="Delete" AllowDelete="true" Width="100" ></ogrid:Column>                
                    </Columns>
                     <Templates>
                        <ogrid:GridTemplate ID="ItemTemplate" runat="server">
                            <Template>
                               <asp:LinkButton ID="lnkPOItem" Text='<%#Container.DataItem["ID"] %>' CommandArgument='<%#Container.DataItem["ID"] %>'   OnClick="lnkDPRItem_Click" runat="server" CssClass="gridCB">
                                        </asp:LinkButton>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
                </ogrid:Grid>
                      
                
                
                <br />
                <center>
                   <%-- <a href="DailyProgressReport.aspx" runat="server" id="lnkbtnAdd" class="btn btn-default">Add New DPR</a> --%>
                    <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />  
                </center>
                      </center>
        <asp:HiddenField runat="server" Value="" Id="HF_Confirm"/>
    </div>
        </div>
    <ajaxToolkit:ModalPopupExtender ID="ModalDPRItem" runat="server" PopupControlID="PanelDPRItem" TargetControlID="btnAddItem"
        CancelControlID="BtnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelDPRItem" runat="server" align="center" Style="display: none" ScrollBars="Auto" Height="100%">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="BtnClose" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center><h5 id="myModalamt1"><asp:Label ID="lblTax" runat="server" Text="Add Rate Card"></asp:Label></h5></center>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4">
                            Discription of Work&nbsp;
                        </div>
                         <div class="col-md-8">
                            <asp:TextBox ID="txtDiscriptionOfWorkRC_Item" runat="server" TextMode="MultiLine" autocomplete="off"  CssClass="form-control"></asp:TextBox>
                        </div>
                        </div>
                        <div class="row">
                        <div class="col-md-4">
                            Quantity&nbsp;
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtRCQuantity_Item" runat="server" onkeyup="CalculateCumulativeAmount()" autocomplete="off"  CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                         </div>
                       <div class="row">
                        <div class="col-md-4">
                            UOM&nbsp;
                        </div>
                        <div class="col-md-8">
                            <asp:DropDownList runat="server" ID="ddlUMORC_Item" class="chosen-select form-control"   TabIndex="4"></asp:DropDownList>
                        </div>
                         </div>
                     <div class="row">
                        <div class="col-md-4">
                            Rate&nbsp;
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtRateRC_Item" runat="server" onkeyup="CalculateCumulativeAmount()" autocomplete="off"  CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                         </div>
                     <div class="row">
                        <div class="col-md-4">
                            Amount&nbsp;
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtAmountRC_Item" runat="server" onkeyup="CalculateCumulativeAmount()" autocomplete="off"  CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                         </div>
                </div>
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnSaveRC_Entry" runat="server" Text="Save"  CssClass="btn btn-default" ValidationGroup="ValItem" OnClick="btnSaveRC_Entry_Click" />
                        <asp:Button ID="btnCancelRC_Entry" runat="server"  Text="Cancel"  CssClass="btn btn-default" CausesValidation="false" OnClick="btnCancelRC_Entry_Click" />        
                 </center>

                </div>
            </div>
        </div>
        

     
    </asp:Panel>
    
    </asp:Content>