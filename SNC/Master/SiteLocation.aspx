<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true"  CodeBehind="SiteLocation.aspx.cs" Inherits="SiteLocation" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {

            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
        });
        function ShowDiv() {
            document.getElementById('btn_AddBank').style.display = 'block';

        }
    </script>
    <script type="text/javascript">
        function exportgrid() {
            Grid_Location.exportToExcel();
        }

        function beforedelete() {
            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                return false;
            }
            return true;
        }
    </script>

    <script>

        $(function () {

            $('#<%=txtPANNo.ClientID%>').keyup(function () {
               var yourInput = $(this).val();
               re = /[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi;
               var isSplChar = re.test(yourInput);
               if (isSplChar) {
                   var no_spl_char = yourInput.replace(/[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '');
                   $(this).val(no_spl_char);
               }
           });

       });
       $(function () {

           $('#<%=txtCIN.ClientID%>').keyup(function () {
               var yourInput = $(this).val();
               re = /[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi;
               var isSplChar = re.test(yourInput);
               if (isSplChar) {
                   var no_spl_char = yourInput.replace(/[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '');
                   $(this).val(no_spl_char);
               }
           });

       });
       $(function () {

           $('#<%=txtCSTRegNo.ClientID%>').keyup(function () {
               var yourInput = $(this).val();
               re = /[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi;
               var isSplChar = re.test(yourInput);
               if (isSplChar) {
                   var no_spl_char = yourInput.replace(/[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '');
                   $(this).val(no_spl_char);
               }
           });

       });

    </script>
  
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
    </style>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>

    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Company & Location

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-1">
                    Company Name&nbsp;
           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValCompanyInfo" ControlToValidate="txtCompanyName"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtCompanyName" CssClass="form-control" runat="server" MaxLength="100" TabIndex="1"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    CIN&nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValCompanyInfo" ControlToValidate="txtCIN"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtCIN" CssClass="form-control" runat="server" MaxLength="50" TabIndex="2"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    PAN No&nbsp;
             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValCompanyInfo" ControlToValidate="txtPANNo"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtPANNo" CssClass="form-control" runat="server" MaxLength="20" TabIndex="3"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-1">
                    Registration Date&nbsp;
          
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtRegDate" runat="server" class="form-control" onkeypress="javascript:return false" onPaste="javascript:return false" TabIndex="4"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txtRegDate" runat="server" Format="dd-MM-yyyy"></ajaxToolkit:CalendarExtender>

                </div>
                <div class="col-md-1">
                    TIN No.&nbsp;
          
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtCSTRegNo" CssClass="form-control" runat="server" MaxLength="50" TabIndex="5"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" ValidationGroup="ValCompanyInfo" OnClick="btnSave_Click" runat="server" Text="Save" CssClass="btn btn-default" TabIndex="6"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default" TabIndex="7"></asp:Button>


                    <asp:Button ID="btnAddLocation" Text="Add Location" CssClass="btn btn-default" runat="server" CausesValidation="false" TabIndex="8" />
                     <asp:Button ID="btnAddInfoBanks" Text="Add Bank" CssClass="btn btn-default" runat="server" CausesValidation="false" TabIndex="8" />
                </div>

            </div>
            <br />
            <center>
        <ogrid:Grid runat="server" ID="Grid_Location"  CallbackMode="false" AutoGenerateColumns="false" OnRowDataBound="Grid_Location_RowDataBound" OnDeleteCommand="Grid_Location_DeleteCommand"  FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowPaging="true" PageSize="10"  AllowAddingRecords="false" >
          <ScrollingSettings ScrollWidth="100%"  />
    <ExportingSettings ExportAllPages="true"  ExportTemplates="true" ExportedFilesTargetWindow="New" ColumnsToExport="Location_Name,Loc_Type,Short_Name,TIN,GST,Contact_Name,Contact_No,Address_Line1,Landmark,City,State,Country,PIN"/>
            
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
               <ClientSideEvents OnBeforeClientDelete="beforedelete" />
            <Columns>           
                 
              
                <ogrid:Column DataField="Location_Name" HeaderText="Location Name" Width="125px" ></ogrid:Column>
                 <ogrid:Column DataField="Loc_Type" HeaderText="Location Type" Width="125px" ></ogrid:Column>
                 <ogrid:Column DataField="Short_Name" HeaderText="Short Name" Width="110px" ></ogrid:Column>
                 <ogrid:Column DataField="TIN" HeaderText="TIN"  Width="110px"></ogrid:Column>
                  <ogrid:Column DataField="GST" HeaderText="GST"  Width="110px" ></ogrid:Column>
                 <ogrid:Column DataField="Contact_Name" HeaderText="Contact Name"  Width="130px"></ogrid:Column>
                  <ogrid:Column DataField="Contact_No" HeaderText="Contact No"  Width="110px" ></ogrid:Column>
                <ogrid:Column DataField="Address_Line1" HeaderText="Address Line" Width="150px" ></ogrid:Column>
                 <ogrid:Column DataField="Landmark" HeaderText="Landmark" ></ogrid:Column>
                 <ogrid:Column DataField="City" HeaderText="City"  Width="120px"></ogrid:Column>
                <ogrid:Column DataField="State" HeaderText="State" Width="120px" ></ogrid:Column>
                 <ogrid:Column DataField="Country" HeaderText="Country" Width="110px" ></ogrid:Column>
                 <ogrid:Column DataField="PIN" HeaderText="PIN" Width="90px" ></ogrid:Column>
                  <ogrid:Column DataField="Location_ID" HeaderText="Edit" runat="server"  Width="90" >
            <TemplateSettings  TemplateId="LocationIDTemplate"/>
        </ogrid:Column>     
                  <ogrid:Column AllowDelete="true" HeaderText="Delete" Width="80px"></ogrid:Column> 
                 
            </Columns>
           <Templates>
        <ogrid:GridTemplate ID="LocationIDTemplate" runat="server" >
            <Template>
             <asp:LinkButton ID="LinkLocation" CommandName='<%#Container.DataItem["Location_ID"] %>' Text="Edit" OnClick="lnkbtnLocationID_Click"   runat="server" CausesValidation="false" />             
                  
            </Template>
        </ogrid:GridTemplate>
             
    </Templates>
        </ogrid:Grid>
                   <br />  
                        <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click" TabIndex="9"></asp:Button>    
                      <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" tabindex="10" />        
                      </center>

            <br />
       <div class="row">
                     
                        <center>
                               <ogrid:Grid ID="GridFrontBank"  runat="server"  CallbackMode="false" AllowPageSizeSelection="false" OnDeleteCommand="Grid_Bank_Details_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="5">
                                <ScrollingSettings ScrollWidth="95%" />
                                    <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                <Columns>
                                    <ogrid:Column  DataField="ID" HeaderText="ID" Wrap="true" Visible="false"></ogrid:Column>
                                     <ogrid:Column  DataField="Bank_Name" HeaderText="Bank Name" Wrap="true"></ogrid:Column> 
                                    <ogrid:Column  DataField="Branch" HeaderText="Branch" Wrap="true"></ogrid:Column>
                                    <ogrid:Column  DataField="Account_No" HeaderText="Account_No" Wrap="true"></ogrid:Column>
                                    <ogrid:Column  DataField="IFSC" HeaderText="IFSC" Wrap="true"></ogrid:Column>
                                    <ogrid:Column  DataField="MICR" HeaderText="MICR" Wrap="true"></ogrid:Column>
                                    <ogrid:Column  DataField="RTGS" HeaderText="RTGS" Wrap="true"></ogrid:Column>
                                    <ogrid:Column  DataField="SWIFT" HeaderText="SWIFT" Wrap="true"></ogrid:Column>
                                     <ogrid:Column AllowDelete="true" HeaderText="Delete"></ogrid:Column>
                                </Columns>
                                 
                            </ogrid:Grid>
                              </center>
                           
                      
                    </div>
            <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        </div>

    </div>
   
     <!-- ModalPopupExtender -->
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupBank" runat="server" PopupControlID="PanelInfoBanks" TargetControlID="btnAddInfoBanks"
        CancelControlID="btnCloseAddInfoBanks" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelInfoBanks" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnCloseAddInfoBanks" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <center>  <h5 id="myModalAddInfoBanks"><asp:Label ID="Label1" runat="server" Text="Add Bank"></asp:Label></h5></center>
                </div>
                <div class="modal-body">

                    <div class="row">
                      <div class="col-md-2">
                            Bank&nbsp;
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*" CssClass="Validation_Text" InitialValue="-Select-" ValidationGroup="ValLocationInfo" ControlToValidate="ddlCountry"></asp:RequiredFieldValidator>
                             
                                          <asp:ImageButton ID="ImgBtnImageBank" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                                    
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlBank" CssClass="form-control" runat="server">
                                <asp:ListItem Text="-Select-"></asp:ListItem>
                            </asp:DropDownList>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1">Branch</div>
                        <div class="col-md-10">
                            <asp:TextBox ID="Tb_bankbranch" runat="server" CssClass="form-control" onkeypress="LimtCharacters(this,150,'Label_comm2');"></asp:TextBox>
                            <label id="Label_comm2" style="background-color: #E2EEF1; color: Blue; font-weight: normal; font-size: xx-small; float: left">Character left:150</label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-1">Account No   &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*" CssClass="Validation_Text" ControlToValidate="TB_accno" ValidationGroup="addBankdetails"></asp:RequiredFieldValidator></div>
                        <div class="col-md-10">
                            <asp:TextBox ID="TB_accno" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1">IFSC</div>
                        <div class="col-md-3">
                            <asp:TextBox ID="TB_IFSC" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1">MICR</div>
                        <div class="col-md-3">
                            <asp:TextBox ID="TB_MICR" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3">Make Default Bank</div>
                        <div class="col-md-1">
                              <asp:CheckBox ID="chkbankDefault" runat="server" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-1">RTGS</div>
                        <div class="col-md-3">
                            <asp:TextBox ID="TB_RTGS" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1">SWIFT</div>
                        <div class="col-md-3">
                            <asp:TextBox ID="TB_swift" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <br />
                    <center>
                <asp:Button ID="btn_AddCompanyBankDetails" runat="server" Text="Save" ValidationGroup="addBankdetails" CssClass="btn btn-github" OnClick="btn_AddCompanyBankDetails_Click" />
                        <asp:Button ID="Button4" runat="server" Text="Cancel" CssClass="btn btn-github" />
                    </center>
                </div>
            </div>
        </div>
    </asp:Panel>

    <!-- ModalPopupExtender -->
    <ajaxToolkit:ModalPopupExtender ID="mpeLocation" runat="server" PopupControlID="PanelLocation" TargetControlID="btnAddLocation"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelLocation" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnClose" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <center>  <h5 id="myModalLabelcrate"><asp:Label ID="lblHeading" runat="server" Text="Add Location"></asp:Label></h5></center>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-2">
                            Location Type&nbsp;
           <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" CssClass="Validation_Text" InitialValue="-Select-" ValidationGroup="ValLocationInfo" ControlToValidate="ddlLocationType"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlLocationType" CssClass="form-control" runat="server">
                                <asp:ListItem Text="-Select-"></asp:ListItem>
                                <asp:ListItem Text="Regional Office"></asp:ListItem>
                                <asp:ListItem Text="Head Office"></asp:ListItem>
                                <asp:ListItem Text="Site Office"></asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            Location Name&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValLocationInfo" ControlToValidate="txtLocationName"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtLocationName" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>

                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            Short Name&nbsp;  
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtShortName" MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="col-md-2">
                            TIN No&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValLocationInfo" ControlToValidate="txtTINNo"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtTINNo" CssClass="form-control" MaxLength="20" runat="server"></asp:TextBox>

                        </div>
                    </div>

                       <div class="row">
                        <div class="col-md-2">
                            Contact Name&nbsp;  
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtContactName" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="col-md-2">
                            Contact No.
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtContactNo" CssClass="form-control" MaxLength="20" runat="server"></asp:TextBox>

                        </div>
                    </div>

                    <div class="row">

                        <div class="col-md-2">
                            GST No&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" MaxLength="20" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValLocationInfo" ControlToValidate="txtGSTNo"></asp:RequiredFieldValidator>

                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtGSTNo" runat="server" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">Address Line 1</div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtAddress" runat="server" MaxLength="1000" style="resize:none" TextMode="MultiLine" class="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-md-2">Landmark</div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtLandmark" runat="server" MaxLength="100" class="form-control"></asp:TextBox>

                        </div>
                        <div class="col-md-2">City</div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtCity" runat="server" MaxLength="50" class="form-control"></asp:TextBox>

                        </div>
                    </div>
                    <div class="row">
                       
                        <div class="col-md-2">
                            Country&nbsp;
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" CssClass="Validation_Text" InitialValue="-Select-" ValidationGroup="ValLocationInfo" ControlToValidate="ddlCountry"></asp:RequiredFieldValidator>
                             
                                          <asp:ImageButton ID="ImgBtnCountry" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                                    
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlCountry" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" runat="server">
                                <asp:ListItem Text="-Select-"></asp:ListItem>
                            </asp:DropDownList>

                        </div>
                         <div class="col-md-2">
                            State&nbsp;
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" CssClass="Validation_Text" InitialValue="-Select-" ValidationGroup="ValLocationInfo" ControlToValidate="ddlState"></asp:RequiredFieldValidator>
                         <asp:ImageButton ID="ImgBtnState" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                         </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlState" CssClass="form-control" runat="server">
                                <asp:ListItem Text="-Select-"></asp:ListItem>
                            </asp:DropDownList>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">Pin No</div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtPinNo" runat="server" MaxLength="10" class="form-control input-pos-int"></asp:TextBox>

                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <center>
           <asp:Button ID="btnSaveLocation" runat="server" Text="Save"  CssClass="btn btn-default" ValidationGroup="ValLocationInfo" OnClick="btnSaveLocation_Click"  />
                        <asp:Button ID="btnCancelLocation" runat="server" Text="Cancel"   CssClass="btn btn-default"  CausesValidation="false" OnClick="btnCancelLocation_Click"   />
                 </center>

                </div>
            </div>
        </div>
    </asp:Panel>

       <ajaxToolkit:ModalPopupExtender ID="ModelCountryPopup" runat="server" PopupControlID="PanelCountry" TargetControlID="ImgBtnCountry"
        CancelControlID="btnCloseCountry" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="PanelCountry" runat="server" align="center" style="display:none">
       
        <div class="modal-dialog" style="width:40%">
             
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnCloseCountry" class="close"  data-dismiss="modal" aria-hidden="true">×</button>
                 <center>  <h5 id="myModalLabelCountry">Add Country</h5></center> 
                </div>
                    <asp:UpdatePanel ID="upCountry" runat="server">
                            <ContentTemplate>
                <div class="modal-body">
                  

                     
                    <div class="row">
                        
                         
                           
                        <div class="col-md-2"></div>
                        <div class="col-md-3">
                            Country Name&nbsp;
          <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ControlToValidate="txtCountryName" CssClass="Validation_Text" ValidationGroup="ValCountry"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txtCountryName" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                   
                    <div class="row">
                     
                        <center>
                               <ogrid:Grid ID="Grid_Country"  runat="server"  CallbackMode="false" AllowPageSizeSelection="false" OnDeleteCommand="Grid_Country_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="5">
                                <ScrollingSettings ScrollWidth="95%" />
                                    <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                <Columns>
                                       
                                     <ogrid:Column  DataField="Country" HeaderText="Country Name" Wrap="true"></ogrid:Column> 
                                    
                                     <ogrid:Column AllowDelete="true" HeaderText="Delete"></ogrid:Column>
                                </Columns>
                                 
                            </ogrid:Grid>
                              </center>
                           
                      
                    </div>
                </div>
                     
                <div class="modal-footer">
                     <center>
           <asp:Button ID="btnSaveCountry" runat="server" Text="Save"  OnClick="btnSaveCountry_Click"    CssClass="btn btn-default" ValidationGroup="ValCountry"  />
                        <asp:Button ID="btnCancelCountry" runat="server"  OnClick="btnCancelCountry_Click" Text="Cancel"  CssClass="btn btn-default" ValidationGroup="ValCountry" CausesValidation="false"  />
                 </center>
  
                </div>
               </ContentTemplate>
                            <Triggers>
                              <%--  <asp:AsyncPostBackTrigger ControlID="btnSaveCountry" EventName="Click" />--%>
                                <asp:PostBackTrigger ControlID="btnSaveCountry" />
                              <%--  <asp:AsyncPostBackTrigger ControlID="ddlCategoryName" EventName="" />--%>
                               <%-- <asp:PostBackTrigger ControlID="Grid_Category" />--%>
                            <%--  <asp:AsyncPostBackTrigger ControlID="Grid_Category" EventName="RowDeleted" />--%>
                            </Triggers>
                        </asp:UpdatePanel>               

            </div>
             
        </div>
         </asp:Panel>

     <ajaxToolkit:ModalPopupExtender ID="ModalPopupBankPane" runat="server" PopupControlID="PanelBtnImageBank" TargetControlID="ImgBtnImageBank"
        CancelControlID="btnClosePanelBtnImageBank" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="PanelBtnImageBank" runat="server" align="center" style="display:none">
       
        <div class="modal-dialog" style="width:40%">
             
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnClosePanelBtnImageBank" class="close"  data-dismiss="modal" aria-hidden="true">×</button>
                 <center>  <h5 id="myModalLabelBtnImageBank">Add Bank</h5></center> 
                </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                <div class="modal-body">
                  

                     
                    <div class="row">
                        
                         
                           
                        <div class="col-md-2"></div>
                        <div class="col-md-3">
                            Bank Name&nbsp;
          <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="*" ControlToValidate="txtBank" CssClass="Validation_Text" ValidationGroup="ValBank"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txtBank" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                   
                    <div class="row">
                     
                        <center>
                               <ogrid:Grid ID="GridBank"  runat="server"  CallbackMode="false" AllowPageSizeSelection="false" OnDeleteCommand="Grid_Bank_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="5">
                                <ScrollingSettings ScrollWidth="95%" />
                                    <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                <Columns>
                                       
                                     <ogrid:Column  DataField="Bank_Name" HeaderText="Bank Name" Wrap="true"></ogrid:Column> 
                                    
                                     <ogrid:Column AllowDelete="true" HeaderText="Delete"></ogrid:Column>
                                </Columns>
                                 
                            </ogrid:Grid>
                              </center>
                           
                      
                    </div>
                </div>
                     
                <div class="modal-footer">
                     <center>
           <asp:Button ID="Button1" runat="server" Text="Save"  OnClick="btnSavebank_Click"    CssClass="btn btn-default" ValidationGroup="ValBank"  />
                        <asp:Button ID="Button2" runat="server"  OnClick="btnCancelCountry_Click" Text="Cancel"  CssClass="btn btn-default" ValidationGroup="ValBank" CausesValidation="false"  />
                 </center>
  
                </div>
               </ContentTemplate>
                            <Triggers>
                              <%--  <asp:AsyncPostBackTrigger ControlID="btnSaveCountry" EventName="Click" />--%>
                                <asp:PostBackTrigger ControlID="btnSaveCountry" />
                              <%--  <asp:AsyncPostBackTrigger ControlID="ddlCategoryName" EventName="" />--%>
                               <%-- <asp:PostBackTrigger ControlID="Grid_Category" />--%>
                            <%--  <asp:AsyncPostBackTrigger ControlID="Grid_Category" EventName="RowDeleted" />--%>
                            </Triggers>
                        </asp:UpdatePanel>               

            </div>
             
        </div>
         </asp:Panel>
   
   <ajaxToolkit:ModalPopupExtender ID="mpeState" runat="server" PopupControlID="PanelState" TargetControlID="ImgBtnState"
        CancelControlID="btnCloseState" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>


    <asp:Panel ID="PanelState" runat="server" align="center" style="display:none">
       
        <div class="modal-dialog" style="width:40%;">
             
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnCloseState" class="close"  data-dismiss="modal" aria-hidden="true">×</button>
                 <center>  <h5 id="myModalLabelState">Add State</h5></center> 
                </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                <div class="modal-body">
                  

                     
                    <div class="row">
                        
                         
                           
                        <div class="col-md-2"></div>
                        <div class="col-md-3">
                            Country Name&nbsp;
          <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" ControlToValidate="ddlCountryName" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValState"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList ID="ddlCountryName" CssClass="form-control" runat="server">
                                <asp:ListItem Text="-Select-" Value="-Select-"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                   <div class="row">
                       <div class="col-md-2"></div>
                        <div class="col-md-3">
                            State Name&nbsp;
          <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" ControlToValidate="txtStateName" CssClass="Validation_Text" ValidationGroup="ValState"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txtStateName" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                        </div>
                   </div>
                    <div class="row">
                     
                        <center>
                               <ogrid:Grid ID="Grid_State"  runat="server"  CallbackMode="false" AllowPageSizeSelection="false" OnDeleteCommand="Grid_State_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="5">
                                <ScrollingSettings ScrollWidth="95%" />
                                    <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                <Columns>
                                       
                                     <ogrid:Column  DataField="Country" HeaderText="Country Name" Wrap="true"></ogrid:Column> 
                                    <ogrid:Column  DataField="State" HeaderText="State Name" Wrap="true"></ogrid:Column> 
                                     <ogrid:Column AllowDelete="true" HeaderText="Delete"></ogrid:Column>
                                </Columns>
                                 
                            </ogrid:Grid>
                              </center>
                           
                      
                    </div>
                </div>
                     
                <div class="modal-footer">
                      <center>
           <asp:Button ID="btnSaveState" runat="server" Text="Save" OnClick="btnSaveState_Click"  CssClass="btn btn-default" ValidationGroup="ValState"  />
                        <asp:Button ID="btnStateCancel" runat="server" Text="Cancel" OnClick="btnStateCancel_Click"  CssClass="btn btn-default" ValidationGroup="ValState" CausesValidation="false"  />
                 </center>
  
                </div>
               </ContentTemplate>
                            <Triggers>
                              <%--  <asp:AsyncPostBackTrigger ControlID="btnSaveCountry" EventName="Click" />--%>
                                <asp:PostBackTrigger ControlID="btnSaveState" />
                              <%--  <asp:AsyncPostBackTrigger ControlID="ddlCategoryName" EventName="" />--%>
                               <%-- <asp:PostBackTrigger ControlID="Grid_Category" />--%>
                            <%--  <asp:AsyncPostBackTrigger ControlID="Grid_Category" EventName="RowDeleted" />--%>
                            </Triggers>
                        </asp:UpdatePanel>               

            </div>
             
        </div>
         </asp:Panel>
</asp:Content>
