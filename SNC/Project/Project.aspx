<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" EnableViewState="true" AutoEventWireup="true" CodeBehind="Project.aspx.cs" Inherits="Project" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>--%>
<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript">
        $(document).ready(function () {

            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
        });

        function beforedelete() {

            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                return false;
            }
            return true;



        }
        function hidebefore() {
            debugger;
            var selectedvalue = $('#<%= rblLetterSelect.ClientID %> input:checked').val()
            if (selectedvalue == "FreshLetter_Div") {
                $('#FreshLetter_Div').show();
            }
            else if (selectedvalue == "ReplaytoLetter_Div") {
                $('#ReplaytoLetter_Div').show();
            }
        }
    </script>
    <script>
        function Hidepopup() {
            $('#myModal112').modal("hide");
        }

        function hipop() {
            $('#FreshLetter_Div').modal('hide');
        }
        function hipopUp() {
            $('#FreshLetter_Div').modal('hide');
        }


    </script>
    <script>
        $(document).ready(function () {
            debugger;
            $('#FreshLetter_Div').hide();
            $('#ReplaytoLetter_Div').hide();

            $('[id*="rblLetterSelect"] input').on('click', function () {
                debugger;
                var _selectedValue = $(this).val();
                if (_selectedValue == "FreshLetter") {
                    $('#FreshLetter_Div').show();
                    $('#ReplaytoLetter_Div').hide();
                }
                else if (_selectedValue == "ReplayToletter") {
                    $('#ReplaytoLetter_Div').show();
                    $('#FreshLetter_Div').hide();
                }
            });


        });


        function pageLoad() {
            debugger;
            var selectedvalue = $('#<%= rblLetterSelect.ClientID %> input:checked').val()
            if (selectedvalue == "FreshLetter_Div") {
                $('#FreshLetter_Div').show();
            }
            else if (selectedvalue == "ReplaytoLetter_Div") {
                $('#ReplaytoLetter_Div').show();
            }

        }
    </script>

    <script>
        $(document).ready(function () {
            debugger;
            $('#FreshLetter_Div_letRecFrom_Dept').hide();
            $('#ReplaytoLetter_Div_letRecFrom_Dept').hide();

            $('[id*="rbLetRecFrom_Dept"] input').on('click', function () {
                debugger;
                var _selectedValue = $(this).val();
                if (_selectedValue == "FreshLetter") {
                    $('#FreshLetter_Div_letRecFrom_Dept').show();
                    $('#ReplaytoLetter_Div_letRecFrom_Dept').hide();
                }
                else if (_selectedValue == "ReplayToletter") {
                    $('#ReplaytoLetter_Div_letRecFrom_Dept').show();
                    $('#FreshLetter_Div_letRecFrom_Dept').hide();
                }
            });


        });

        function pageLoad() {
            debugger;
            var selectedvalue = $('#<%= rblLetterSelect.ClientID %> input:checked').val()
            if (selectedvalue == "FreshLetter_Div") {
                $('#FreshLetter_Div').show();
            }
            else if (selectedvalue == "ReplaytoLetter_Div") {
                $('#ReplaytoLetter_Div').show();
            }

        }
    </script>
    
    <style>
        div#mybtnalig a input[type="submit"] {
   
    height: 45px;
    width: 200px;
    white-space: normal;
}
        hr {
  border: 2px solid green;
  border-radius: 1px;
}
        .contrctwidth {
            width: 100% !important;
            padding: 0px !important;
            border-radius: 15px;

        }


        
    </style>

   
    

    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>
                Project Details
            </h3>
        </div>
        
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-1">
                    Project ID
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtProjectID" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    Project Name&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtProjectName" CssClass="Validation_Text" ValidationGroup="ValProject" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtProjectName" CssClass="form-control" MaxLength="100" autocomplete= "off" runat="server" TabIndex="1"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    Project Type
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlProjectType" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValProject" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <a href="#myModal" data-toggle="modal" role="button">
                        <asp:ImageButton ID="ImgProjectType" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                    </a>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlProjectType" CssClass="form-control" runat="server" TabIndex="2"></asp:DropDownList>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-1">
                    State
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="ddlState" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValProject" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="ddlState" CssClass="form-control" TabIndex="3" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-1">
                    Location
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="ddlLocation" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValProject" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="ddlLocation" CssClass="form-control" TabIndex="4"></asp:DropDownList>
                    <%--<asp:TextBox runat="server" ID="txtLocation" CssClass="form-control" autocomplete= "off" TabIndex="4"></asp:TextBox>--%>
                </div>
                <div class="col-md-1">Alias Project</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtAliasProject" CssClass="form-control" runat="server" MaxLength="100" autocomplete= "off" TabIndex="5"></asp:TextBox>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-1">
                    Start Date
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtStartDate" CssClass="Validation_Text" ValidationGroup="ValProject" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtStartDate" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtStartDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>

                </div>
                <div class="col-md-1">Completion Date</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtCompletionDate" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtCompletionDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                    <asp:CompareValidator ErrorMessage="Completion Date should be greater than or Equal to Start Date" Font-Size="10px" CssClass="Validation_Text" ControlToValidate="txtCompletionDate" ControlToCompare="txtStartDate" Display="Dynamic" Type="Date" Operator="GreaterThanEqual" ValidationGroup="ValProject" runat="server" />
                </div>
                <div class="col-md-1">Project Cost &nbsp;(INR)</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtProjectCost" CssClass="form-control input-pos-float" runat="server" MaxLength="13" autocomplete= "off" AutoPostBack="true" TabIndex="8" OnTextChanged="txtProjectCost_TextChanged"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">Project Cost Amount In Words (INR):</div>
                <div class="col-md-10"><asp:TextBox ID="txtamtinwords" TextMode="MultiLine" CssClass="form-control" runat="server" MaxLength="100" autocomplete= "off" TabIndex="5"></asp:TextBox></div>
            </div>

            <div class="row">
                <div class="col-md-1">
                    Awarded By
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="ddlAwardedBy" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValProject" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <a href="#myModal" data-toggle="modal" role="button">
                        <asp:ImageButton ID="ImgCompany" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                    </a>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="ddlAwardedBy" CssClass="form-control" TabIndex="9"></asp:DropDownList>
                    <%--<asp:TextBox ID="txtAwardedBy" CssClass="form-control" MaxLength="50" runat="server" TabIndex="9"></asp:TextBox>--%>
                </div>
                <div class="col-md-1">Principal Contractor</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtPrincipalContractor" CssClass="form-control" runat="server" MaxLength="100" autocomplete= "off" TabIndex="5"></asp:TextBox>
                </div>
                <div class="col-md-1">Principal Agreement Date</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtPrincipalAgreementDate" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender4" TargetControlID="txtPrincipalAgreementDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                </div>
            </div>
            <div class="row">
            <div class="col-md-1"> Employer Agreement Date</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtEmployerAgreementDate" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender3" TargetControlID="txtEmployerAgreementDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                </div>            
                <div class="col-md-1">Description</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtDescription" CssClass="form-control" TextMode="MultiLine" Rows="3" runat="server" Style="resize: None" MaxLength="500" TabIndex="10"></asp:TextBox>
                </div>
                <div class="col-md-1">Status</div>
                <div class="col-md-3">
                    <asp:RadioButtonList runat="server" ID="rd_Status" RepeatDirection="Horizontal" TabIndex="11">
                        <asp:ListItem Text="Running" Value="Open" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Hold" Value="Hold"></asp:ListItem>
                        <asp:ListItem Text="Completed" Value="Close"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
             <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ValProject" CssClass="btn btn-default" CausesValidation="false" OnClick="btnSubmit_Click" TabIndex="10"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btnCancel_Click" TabIndex="11"></asp:Button>
                    <a href="#myModal1" data-toggle="modal" role="button">
                        <asp:Button ID="btn_addContact" runat="server" Text="Add Contact" CssClass="btn btn-default" TabIndex="12"></asp:Button>
                    </a>
                     <a href="#myModal112" data-toggle="modal" role="button">
                        <asp:Button ID="Button3" runat="server" Text="Add Documents" CssClass="btn btn-default" TabIndex="12"></asp:Button>
                    </a>
                    <a href="#Modal_Sitelocation" data-toggle="modal" role="button">
                        <asp:Button ID="btn_Sitelocation" runat="server" Text="Add Site location" CssClass="btn btn-default" TabIndex="12"></asp:Button>
                    </a>
                </div>

            </div>
            <br />
            
             <a href="#page-ContactDetails" class="panel-heading contrctwidth" data-toggle="collapse" data-target="#page-ContactDetails">
           <h4 style="text-align:center">Contact List</h4>
        </a>
             <div id="page-ContactDetails" class="panel-collapse panel-body collapse out">
                  <div class="row">
                <center>
                    <cc1:Grid ID="GridContact" CallbackMode="false" runat="server" OnDeleteCommand="GridContact_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                        <ScrollingSettings ScrollWidth="95%" />
                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                        <Columns>
                            <cc1:Column DataField="Proj_Con_ID" Visible="false" HeaderText="Contact ID"></cc1:Column>
                            <cc1:Column DataField="Name" HeaderText="Contact Person" Width="130" Wrap="true"></cc1:Column>
                            <cc1:Column DataField="Cont_Type" HeaderText="Contact Type"></cc1:Column>
                            <cc1:Column DataField="Department" HeaderText="Department" Width="130px" Wrap="true"></cc1:Column>
                            <cc1:Column DataField="Designation" HeaderText="Designation" Width="130px" Wrap="true"></cc1:Column>
                            <cc1:Column DataField="Cont_No" HeaderText="Contact No" Width="130px"></cc1:Column>
                            <cc1:Column DataField="Email_ID" HeaderText="Email ID" Wrap="true" Width="150px"></cc1:Column>
                            <cc1:Column DataField="Dispatch_Address" HeaderText="Dispatch Address" Wrap="true" Width="250px"></cc1:Column> 
                            <cc1:Column AllowDelete="true" HeaderText="Delete" Width="80px"></cc1:Column>
                        </Columns>
                    </cc1:Grid>
                </center>
            </div>
           </div>

            <br />
            <br />
              <a href="#page-Sitelocation" class="panel-heading contrctwidth" data-toggle="collapse" data-target="#page-Sitelocation">
           <h4 style="text-align:center">Site location List</h4>
        </a>
             <div id="page-Sitelocation" class="panel-collapse panel-body collapse out">
                  <div class="row">
                <center>
                    <cc1:Grid ID="Grid_Sitelocation" CallbackMode="false" runat="server" OnDeleteCommand="GridSitelocation_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                        <ScrollingSettings ScrollWidth="80%" />
                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                        <Columns>
                            <cc1:Column DataField="Site_ID" Visible="false" HeaderText="ID"></cc1:Column>
                            <cc1:Column DataField="SiteContactPerson" HeaderText="Contact Person" Width="130px" Wrap="true"></cc1:Column>
                            <cc1:Column DataField="SiteMobileNumber" HeaderText="Mobile Number" Width="150px"></cc1:Column>
                            <cc1:Column DataField="SiteAddress" HeaderText="Site Address" Width="250px" Wrap="true"></cc1:Column>
                            <cc1:Column AllowDelete="true" HeaderText="Delete" Width="80px"></cc1:Column>
                        </Columns>
                    </cc1:Grid>
                </center>
            </div>
           </div>

            <br />
            <br />
          <h4 style="text-align:center; padding-top:10px;padding-bottom:10px; font-size:20px">Documents </h4>
             <a href="#page-Correspondence" class="panel-heading contrctwidth" data-toggle="collapse">
           <h4 style="text-align:center">Letter Correspondence to Department</h4>
        </a>
                 <div id="page-Correspondence"   class="panel-collapse panel-body collapse out" > 
              <div class="row">
                  <h4 style="text-align:center">Fresh Letter List</h4>
                <center>
                    <cc1:Grid ID="GridFreshLetter" CallbackMode="false" runat="server" OnDeleteCommand="GridFreshLetter_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                        <ScrollingSettings ScrollWidth="95%" />
                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                        <Columns>
                             <cc1:Column DataField="ID" Visible="false" HeaderText="ID"></cc1:Column>
                            <cc1:Column DataField="Letter_ID" HeaderText="Letter ID">
                                <TemplateSettings TemplateId="ItemTemplateFreshLetterID" />
                            </cc1:Column>
                            <cc1:Column DataField="LetterRefNumber" HeaderText="Contact ID"></cc1:Column>
                            <cc1:Column DataField="LetterRecdFrom" HeaderText="Letter Recived From"></cc1:Column>
                            <cc1:Column DataField="LetterRecdFrom" HeaderText="Letter Recived From" Width="190" Wrap="true" Visible="false">

                           
                            </cc1:Column>
                            <cc1:Column DataField="FreshLetterSubject" HeaderText="Subject">
                           
                            </cc1:Column>
                             <cc1:Column DataField="LetterReceivedDate" HeaderText="Letter Received Date">
                            
                             </cc1:Column>
                            <cc1:Column DataField="DateofReceipt" HeaderText="Date of Receipt" Width="130px" Wrap="true">
                            
                            </cc1:Column>
                            <cc1:Column DataField="ReplyByDate" HeaderText="Reply By Date" Width="130px" Wrap="true">
                                
                            </cc1:Column>
                            <cc1:Column DataField="fCopyof_Fresh_Letter_Path" HeaderText="View or Download Document" Width="130px">
                                 <TemplateSettings  TemplateId="FreshLetterTemplate"/>
                            </cc1:Column>
                           
                            <cc1:Column AllowDelete="true" HeaderText="Delete" Width="80px"></cc1:Column>
                            
                        </Columns>
                        <Templates>
                            <cc1:GridTemplate ID="FreshLetterTemplate" runat="server">
                                <Template>
                                <asp:LinkButton ID="lnkDownloadFile" Text='<%#Container.DataItem["Copyof_Fresh_Letter_Path"] %>' 
                                    OnClick="lnkDownloadFile_Click" runat="server" CssClass="gridCB">
                                </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>

                            <%--<cc1:GridTemplate runat="server" ID="template_LetterRecivedFrom" ControlID="txtLetterRecivedFrom" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtLetterRecivedFrom" Width="150px" runat="server"></asp:TextBox>
                            </Template>
                        </cc1:GridTemplate>
                            <cc1:GridTemplate runat="server" ID="template_Subject" ControlID="txtSubject" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtSubject" Width="150px" runat="server"></asp:TextBox>
                            </Template>
                        </cc1:GridTemplate>
                            <cc1:GridTemplate runat="server" ID="template_LetterRecivedDate" ControlID="txtLetterRecivedDate" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtLetterRecivedDate" Width="120px" runat="server" TextMode="Date" Text='<%# Container.DataItem["LetterReceivedDate"] %>'></asp:TextBox>
                            </Template>
                        </cc1:GridTemplate>
                            <cc1:GridTemplate runat="server" ID="template_DateofReceipt" ControlID="txtDateofReceipt" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtDateofReceipt" Width="120px" runat="server" TextMode="Date" Text='<%# Container.DataItem["DateofReceipt"] %>'></asp:TextBox>
                            </Template>
                        </cc1:GridTemplate>
                            <cc1:GridTemplate runat="server" ID="template_ReplyByDate" ControlID="txtReplyByDate" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtReplyByDate" Width="120px" runat="server" TextMode="Date" Text='<%# Container.DataItem["ReplyByDate"] %>'></asp:TextBox>
                            </Template>
                        </cc1:GridTemplate>--%>

                        </Templates>
                        <Templates>
                        <cc1:GridTemplate ID="ItemTemplateFreshLetterID" runat="server">
                            <Template>
                               
                            <asp:LinkButton ID="lnkLetterIdFreshLetter" Text='<%#Container.DataItem["Letter_ID"] %>' CommandArgument='<%#Container.DataItem["Letter_ID"] %>' CommandName='<%#Container.DataItem["Letter_ID"] %>' OnClick="lnkLetterIdFreshLetter_Click" runat="server" CssClass="gridCB">
                                        </asp:LinkButton>
                                   
                            </Template>
                        </cc1:GridTemplate>
                    </Templates>
                    </cc1:Grid>
                </center>
            </div>
                      <div class="row">
                  <h4 style="text-align:center">Reply to Letter List</h4>
                <center>
                    <cc1:Grid ID="GridReplayToLetter" CallbackMode="false" Width="100%" runat="server" OnDeleteCommand="GridReplayLetter_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                        <ScrollingSettings ScrollWidth="95%" />
                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                        <Columns>
                             <cc1:Column DataField="ID" Visible="false" HeaderText="ID"  Wrap="true"></cc1:Column>
                            <cc1:Column DataField="ReplayToLetter_ID_RTL" HeaderText="Letter ID"  Wrap="true">
                                <TemplateSettings TemplateId="ItemTemplateReplyToLetterID" />
                            </cc1:Column>  
                            <cc1:Column DataField="LetterSentTo_RTL" HeaderText="Letter Sent To"  Wrap="true"></cc1:Column>
                            <cc1:Column DataField="Date_RTL" HeaderText="Date" Width="130" Wrap="true" ></cc1:Column>
                             <cc1:Column DataField="Subject" HeaderText="Subject"  Wrap="true"></cc1:Column>
                            <cc1:Column DataField="LetterRefNumber_RTL" HeaderText="Letter Ref Number"  Wrap="true"></cc1:Column>
                            <cc1:Column DataField="ModeofDispatch_RTL" HeaderText="Mode Of Dispatch"  Wrap="true"></cc1:Column>
                            <cc1:Column DataField="LetterCopy_RTL" HeaderText="Copy Of Letter" Width="130px" Wrap="true">
                               <TemplateSettings  TemplateId="ReplayLetterLetterCopy_RTL"/>
                                </cc1:Column>
                            <cc1:Column DataField="AcknowledgementCopy_RTL" HeaderText="Acknowledgement Copy" Width="190px" Wrap="true">
                               <TemplateSettings  TemplateId="ReplayLetterAcknowledgementCopy_RTL"/>
                                </cc1:Column>
                            
                            <cc1:Column DataField="CC1" HeaderText="CC1" Width="60px" Wrap="true"></cc1:Column>
                            <cc1:Column DataField="CC2" HeaderText="CC2" Width="60px"  Wrap="true"></cc1:Column>
                            <cc1:Column DataField="CC3" HeaderText="CC3" Width="60px"></cc1:Column>
                            <cc1:Column DataField="CC4" HeaderText="CC4" Width="60px"></cc1:Column>
                            <cc1:Column DataField="CC5" HeaderText="CC5" Width="60px"></cc1:Column>
                            <cc1:Column AllowDelete="true" HeaderText="Delete" Width="80px"></cc1:Column>
                        </Columns>
                        <Templates>
                            <cc1:GridTemplate ID="ReplayLetterLetterCopy_RTL" runat="server">
                                <Template>
                                <asp:LinkButton ID="lnkDownloadFile" Text='<%#Container.DataItem["LetterCopy_RTL"] %>' 
                                    OnClick="lnkDownloadFile_Click" runat="server" CssClass="gridCB">
                                </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>
                    </Templates>
                        <Templates>
                            <cc1:GridTemplate ID="ReplayLetterAcknowledgementCopy_RTL" runat="server">
                                <Template>
                                <asp:LinkButton ID="lnkDownloadFile"  Text='<%#Container.DataItem["AcknowledgementCopy_RTL"] %>' 
                                    OnClick="lnkDownloadFile_Click" runat="server" CssClass="gridCB">
                                </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>
                    </Templates>
                        <Templates>
                        <cc1:GridTemplate ID="ItemTemplateReplyToLetterID" runat="server">
                            <Template>
                               
                            <asp:LinkButton ID="lnkreplyToLetter" Text='<%#Container.DataItem["ReplayToLetter_ID_RTL"] %>' CommandArgument='<%#Container.DataItem["ReplayToLetter_ID_RTL"] %>' CommandName='<%#Container.DataItem["ReplayToLetter_ID_RTL"] %>' OnClick="lnkreplyToLetter_Click" runat="server" CssClass="gridCB">
                                        </asp:LinkButton>
                                   
                            </Template>
                        </cc1:GridTemplate>
                    </Templates>
                    </cc1:Grid>
                </center>
            </div>
                     </div>

             <a href="#page-Received" class="panel-heading contrctwidth" data-toggle="collapse">
           <h4 style="text-align:center">Letter Received from Department</h4>
        </a>
                 <div id="page-Received"  class="panel-collapse panel-body collapse out">
                  <div class="row">
                  <h4 style="text-align:center">Fresh Letter List Letter Received from Department</h4>
                <center>
                    <cc1:Grid ID="GridLetter_letRecFrom_Dept" CallbackMode="false" runat="server" OnDeleteCommand="GridFreshLetterletRecFrom_Dept_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                        <ScrollingSettings ScrollWidth="95%" />
                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                        <Columns>
                             <cc1:Column DataField="ID" Visible="false" HeaderText="ID"></cc1:Column>
                            <cc1:Column DataField="Letter_ID" HeaderText="Letter ID">
                                 <TemplateSettings TemplateId="ItemTemplate" />
                            </cc1:Column>
                            <cc1:Column DataField="LetterRefNumber" HeaderText="Letter Ref No"></cc1:Column>
                            <cc1:Column DataField="LetterRecdFrom" HeaderText="Letter Recived From" Width="130" Wrap="true"></cc1:Column>
                            <cc1:Column DataField="LetterReceivedDate" HeaderText="Letter Date"></cc1:Column>
                            <cc1:Column DataField="FreshLetterSubject" HeaderText="Subject" Wrap="true"></cc1:Column>
                            <cc1:Column DataField="DateofReceipt" HeaderText="Date of Receipt" Width="130px" Wrap="true"></cc1:Column>
                            <cc1:Column DataField="ReplyByDate" HeaderText="Reply By Date" Width="130px" Wrap="true"></cc1:Column>
                            <cc1:Column DataField="Copyof_Fresh_Letter_Path" HeaderText="View or Download Document" Width="130px">
                                <TemplateSettings  TemplateId="FreshLetter_letRecFrom_Dept"/>
                            </cc1:Column>
                           
                            <cc1:Column AllowDelete="true" HeaderText="Delete" Width="80px"></cc1:Column>
                        </Columns>

                            <Templates>
                        <cc1:GridTemplate ID="ItemTemplate" runat="server">
                            <Template>
                               
                            <asp:LinkButton ID="lnkLetterId" Text='<%#Container.DataItem["Letter_ID"] %>' CommandArgument='<%#Container.DataItem["Letter_ID"] %>' CommandName='<%#Container.DataItem["Letter_ID"] %>'  OnClick="lnkLetterId_Click" runat="server" CssClass="gridCB">
                                        </asp:LinkButton>
                                   
                            </Template>
                        </cc1:GridTemplate>
                    </Templates>

                        <Templates>
                            <cc1:GridTemplate ID="FreshLetter_letRecFrom_Dept" runat="server">
                                <Template>
                                <asp:LinkButton ID="lnkDownloadFile" Text='<%#Container.DataItem["Copyof_Fresh_Letter_Path"] %>' 
                                    OnClick="lnkDownloadFile_Click" runat="server" CssClass="gridCB">
                                </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>
                    </Templates>
                    </cc1:Grid>
                </center>
            </div>

                 <div class="row">
                  <h4 style="text-align:center">Reply to Letter List</h4>
                <center>
                    <cc1:Grid ID="GridLetterRecFrDep_ReplayLeter" CallbackMode="false" runat="server" OnDeleteCommand="GridReplayLetter_letRecFrom_Dept_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                        <ScrollingSettings ScrollWidth="95%" />
                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                        <Columns>
                             <cc1:Column DataField="ID" Visible="false" HeaderText="ID"></cc1:Column>
                            <cc1:Column DataField="ReplayToLetter_ID_RTL" HeaderText="Letter ID"></cc1:Column>  
                            <cc1:Column DataField="LetterSentTo_RTL" HeaderText="Letter Sent To"></cc1:Column>
                            <cc1:Column DataField="Date_RTL" HeaderText="Date" Width="130" Wrap="true"></cc1:Column>
                            <cc1:Column DataField="FreshLetterSubject" HeaderText="Subject"></cc1:Column>
                            <cc1:Column DataField="LetterRefNumber_RTL" HeaderText="Letter Ref Number"></cc1:Column>
                            <cc1:Column DataField="ModeofDispatch_RTL" HeaderText="Mode Of Dispatch"></cc1:Column>
                            <cc1:Column DataField="LetterCopy_RTL" HeaderText="Copy Of Letter" Width="130px" Wrap="true">
                               <TemplateSettings  TemplateId="ReplayLetterLetterCopy_RTL_recived"/>
                                </cc1:Column>
                            <cc1:Column DataField="AcknowledgementCopy_RTL" HeaderText="Acknowledgement Copy" Width="130px" Wrap="true">
                               <TemplateSettings  TemplateId="AcknowledgementCopy_RTL"/>
                                </cc1:Column>
                            <cc1:Column AllowDelete="true" HeaderText="Delete" Width="80px"></cc1:Column>
                        </Columns>
                        <Templates>
                            <cc1:GridTemplate ID="ReplayLetterLetterCopy_RTL_recived" runat="server">
                                <Template>
                                <asp:LinkButton ID="lnkDownloadFile" Text='<%#Container.DataItem["LetterCopy_RTL"] %>' 
                                    OnClick="lnkDownloadFile_Click" runat="server" CssClass="gridCB">
                                </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>
                    </Templates>
                        <Templates>
                            <cc1:GridTemplate ID="AcknowledgementCopy_RTL" runat="server">
                                <Template>
                                <asp:LinkButton ID="lnkDownloadFile" Text='<%#Container.DataItem["AcknowledgementCopy_RTL"] %>' 
                                    OnClick="lnkDownloadFile_Click" runat="server" CssClass="gridCB">
                                </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>
                    </Templates>
                    </cc1:Grid>
                </center>
            </div>
                     </div>

             <a href="#page-Other" class="panel-heading contrctwidth" data-toggle="collapse">
           <h4 style="text-align:center">Other Documents</h4>
        </a>
                 <div id="page-Other"  class="panel-collapse panel-body collapse in">
              
                 <div class="row">
                  <h4 style="text-align:center"> Variations/ Price Adjustments</h4>
                <center>
                    <cc1:Grid ID="GridVariations" CallbackMode="false" runat="server" OnDeleteCommand="GridVariations_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                        <%--<ScrollingSettings ScrollWidth="95%" />--%>
                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                        <Columns>
                             <cc1:Column DataField="ID" Visible="false" HeaderText="ID"></cc1:Column>
                            <cc1:Column DataField="Filename_Variations" HeaderText="File Name"></cc1:Column> 
                            <cc1:Column DataField="Variations_FilePath" HeaderText="File" Width="200px" Wrap="true">
                               <TemplateSettings  TemplateId="Variations_Template"/>   </cc1:Column>
                             <cc1:Column DataField="ApprovalDocumentVariations_FilePath" HeaderText="Approval Document File" Width="200px" Wrap="true">
                               <TemplateSettings  TemplateId="ApprovalDocumentVariations_FilePath_Template"/>   </cc1:Column>
                            <cc1:Column AllowDelete="true" HeaderText="Delete" Width="80px"></cc1:Column>
                        </Columns>
                        <Templates>
                            <cc1:GridTemplate ID="Variations_Template" runat="server">
                                <Template>
                                <asp:LinkButton ID="lnkDownloadFile" Text='<%#Container.DataItem["Variations_FilePath"] %>' 
                                    OnClick="lnkDownloadFile_Click" runat="server" CssClass="gridCB">
                                </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>
                    </Templates>
                        <Templates>
                            <cc1:GridTemplate ID="ApprovalDocumentVariations_FilePath_Template" runat="server">
                                <Template>
                                <asp:LinkButton ID="lnkDownloadFile" Text='<%#Container.DataItem["ApprovalDocumentVariations_FilePath"] %>' 
                                    OnClick="lnkDownloadFile_Click" runat="server" CssClass="gridCB">
                                </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>
                    </Templates>
                    </cc1:Grid>
                </center>
            </div>
                     <div class="row">
                  <h4 style="text-align:center">Drawings</h4>
                <center>
                    <cc1:Grid ID="GridDrawings" CallbackMode="false" runat="server" OnDeleteCommand="GridDrawings_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                        <%--<ScrollingSettings ScrollWidth="95%" />--%>
                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                        <Columns>
                             <cc1:Column DataField="ID" Visible="false" HeaderText="ID"></cc1:Column>
                            <cc1:Column DataField="Filename_Drawings" HeaderText="File Name"></cc1:Column> 
                            <cc1:Column DataField="Drawings_FilePath" HeaderText="File" Width="200px" Wrap="true">
                               <TemplateSettings  TemplateId="Drawings_FilePathTemplate"/>  </cc1:Column>
                            <cc1:Column DataField="ApprovalDocumentsDrawings_FilePath" HeaderText="Approval Documents File" Width="200px" Wrap="true">
                               <TemplateSettings  TemplateId="ApprovalDocumentsDrawings_FilePathTemplate"/>  </cc1:Column>
                            <cc1:Column AllowDelete="true" HeaderText="Delete" Width="80px"></cc1:Column>
                        </Columns>
                        <Templates>
                            <cc1:GridTemplate ID="Drawings_FilePathTemplate" runat="server">
                                <Template>
                                <asp:LinkButton ID="lnkDownloadFile" Text='<%#Container.DataItem["Drawings_FilePath"] %>' 
                                    OnClick="lnkDownloadFile_Click" runat="server" CssClass="gridCB">
                                </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>
                            <cc1:GridTemplate ID="ApprovalDocumentsDrawings_FilePathTemplate" runat="server">
                                <Template>
                                <asp:LinkButton ID="lnkDownloadFile" Text='<%#Container.DataItem["ApprovalDocumentsDrawings_FilePath"] %>' 
                                    OnClick="lnkDownloadFile_Click" runat="server" CssClass="gridCB">
                                </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>
                    </Templates>
                       
                    </cc1:Grid>
                </center>
            </div>
                     <div class="row">
                  <h4 style="text-align:center">Vendor Credentials / Approvals</h4>
                <center>
                    <cc1:Grid ID="GridVendorCredentialsApprovals" CallbackMode="false" Width="90%" runat="server" OnDeleteCommand="GridDVendor_Credentials_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                        <%--<ScrollingSettings ScrollWidth="95%" />--%>
                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                        <Columns>
                             <cc1:Column DataField="ID" Visible="false" HeaderText="ID"></cc1:Column>
                            <cc1:Column DataField="Filename_Vendor_Credentials" HeaderText="File Name"></cc1:Column> 
                            <cc1:Column DataField="Vendor_Credentials_FilePath" HeaderText="File" Width="200px" Wrap="true">
                               <TemplateSettings  TemplateId="Template_Vendor_Credentials"/> </cc1:Column>
                            <cc1:Column DataField="ApprovalDocumentsCredentialsApproval_FilePath" HeaderText="Approval Documents File" Width="200px" Wrap="true">
                               <TemplateSettings  TemplateId="Template_ApprovalDocumentsCredentials"/> </cc1:Column>
                            <cc1:Column AllowDelete="true" HeaderText="Delete" Width="80px"></cc1:Column>
                        </Columns>
                        <Templates>
                            <cc1:GridTemplate ID="Template_Vendor_Credentials" runat="server">
                                <Template>
                                <asp:LinkButton ID="lnkDownloadFile" Text='<%#Container.DataItem["Vendor_Credentials_FilePath"] %>' 
                                    OnClick="lnkDownloadFile_Click" runat="server" CssClass="gridCB">
                                </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>
                              <cc1:GridTemplate ID="Template_ApprovalDocumentsCredentials" runat="server">
                                <Template>
                                <asp:LinkButton ID="lnkDownloadFile" Text='<%#Container.DataItem["ApprovalDocumentsCredentialsApproval_FilePath"] %>' 
                                    OnClick="lnkDownloadFile_Click" runat="server" CssClass="gridCB">
                                </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>
                    </Templates>
                       
                    </cc1:Grid>
                </center>
            </div>
                 <div class="row">
                  <h4 style="text-align:center">Bill of Quantity</h4>
                <center>
                    <cc1:Grid ID="GridQuantity" CallbackMode="false" runat="server" OnDeleteCommand="GridQuantity_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                        <%--<ScrollingSettings ScrollWidth="95%" />--%>
                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                        <Columns>
                             <cc1:Column DataField="ID" Visible="false" HeaderText="ID"></cc1:Column>
                            <cc1:Column DataField="Filename_BillofQuantity" HeaderText="File Name"></cc1:Column> 
                            <cc1:Column DataField="BillofQuantity_FilePath" HeaderText="File" Width="130px" Wrap="true">
                               <TemplateSettings  TemplateId="BillofQuantity_SA"/>
                                </cc1:Column>
                            <cc1:Column AllowDelete="true" HeaderText="Delete" Width="80px"></cc1:Column>
                        </Columns>
                        <Templates>
                            <cc1:GridTemplate ID="BillofQuantity_SA" runat="server">
                                <Template>
                                <asp:LinkButton ID="lnkDownloadFile" Text='<%#Container.DataItem["BillofQuantity_FilePath"] %>' 
                                    OnClick="lnkDownloadFile_Click" runat="server" CssClass="gridCB">
                                </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>
                    </Templates>
                       
                    </cc1:Grid>
                </center>
            </div>
                   <div class="row">
                  <h4 style="text-align:center"> Licenses</h4>
                <center>
                    <cc1:Grid ID="gridlicense" CallbackMode="false" runat="server" OnDeleteCommand="gridlicense_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                        <%--<ScrollingSettings ScrollWidth="95%" />--%>
                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                        <Columns>
                             <cc1:Column DataField="ID" Visible="false" HeaderText="ID"></cc1:Column>
                            <cc1:Column  HeaderText="Add Or View Extension" Width="100px" Wrap="true" >
                            <TemplateSettings TemplateId="ItemlicenseExtension" />
                        </cc1:Column>
                            <cc1:Column DataField="License_Type" HeaderText="License Type" Wrap="true" Width="100px"></cc1:Column> 
                            <cc1:Column DataField="Issuer_Name" HeaderText="Issuer Name" Wrap="true" Width="100px"></cc1:Column> 
                            <cc1:Column DataField="License_Number" HeaderText="License_Number" Wrap="true" Width="100px"></cc1:Column> 
                            <cc1:Column DataField="Amount" HeaderText="Amount" Wrap="true" Width="100px"></cc1:Column>
                             <cc1:Column DataField="License_Date" HeaderText="License Date" Wrap="true" Width="100px"></cc1:Column> 
                            <cc1:Column DataField="Original_Expiry_Date" HeaderText="Original Expiry Date" Wrap="true" Width="100px"></cc1:Column> 
                            <cc1:Column DataField="License_Copy_FilePath" HeaderText="File" Width="130px" Wrap="true">
                               <TemplateSettings  TemplateId="License_SA"/>
                                </cc1:Column>
                            <cc1:Column DataField="License_Extension" HeaderText="Extension" Visible="false" Wrap="true" Width="100px"></cc1:Column>
                            <cc1:Column DataField="License_Valid_Upto" HeaderText="Valid_upto" Visible="false" Wrap="true" Width="100px"></cc1:Column>
                            <cc1:Column DataField="License_Extension_Copy" HeaderText="Ext File" Visible="false" Width="100px" Wrap="true">
                               <TemplateSettings  TemplateId="License_SA_Upload"/>
                                </cc1:Column>

                            <cc1:Column AllowDelete="true" HeaderText="Delete" Width="60px"></cc1:Column>
                        </Columns>
                        <Templates>
                            <cc1:GridTemplate ID="License_SA" runat="server">
                                <Template>
                                <asp:LinkButton ID="lnkDownloadFile" Text='<%#Container.DataItem["License_Copy_FilePath"] %>' 
                                    OnClick="lnkDownloadFile_Click" runat="server" CssClass="gridCB">
                                </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>
                             <cc1:GridTemplate ID="ItemlicenseExtension" runat="server">
                            <Template>
                            <asp:LinkButton ID="lnklicenseExtensionAdd" Text="Add or View Extension" CommandArgument='<%#Container.DataItem["ID"] %>' CommandName='<%#Container.DataItem["ID"] %>'  OnClick="lnklicenseExtensionExtension_Click" runat="server" CssClass="gridCB">
                                        </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>
                    </Templates>
                       
                    </cc1:Grid>
                </center>
            </div>

                      <div class="row">
                  <h4 style="text-align:center"> Extension Licenses</h4>
                     <center>
                    <cc1:Grid ID="gridextlicense" CallbackMode="false"  runat="server"  FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="5">
                        <%--<ScrollingSettings ScrollWidth="95%" />--%>
                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                        <Columns>
                             <cc1:Column DataField="ID" Visible="false" HeaderText="ID"></cc1:Column>
                             <cc1:Column DataField="License_Type" HeaderText="License Type" Wrap="true" Width="110px"></cc1:Column> 
                            <cc1:Column DataField="Issuer_Name" HeaderText="Issuer Name" Wrap="true" Width="110px"></cc1:Column> 
                            <cc1:Column DataField="License_Number" HeaderText="License_Number" Wrap="true" Width="100px"></cc1:Column> 
                            <cc1:Column DataField="Amount" HeaderText="Amount" Wrap="true" Width="100px"></cc1:Column>
                             <cc1:Column DataField="License_Date" HeaderText="License Date" Wrap="true" Width="100px"></cc1:Column> 
                            <cc1:Column DataField="Original_Expiry_Date" HeaderText="Original Expiry Date" Wrap="true" Width="100px"></cc1:Column> 
                            <cc1:Column DataField="License_Copy_FilePath" HeaderText="File" Width="130px" Visible="false" Wrap="true">
                               <TemplateSettings  TemplateId="License_SA"/>
                                </cc1:Column>
                            <cc1:Column DataField="License_Extension" HeaderText="Extension" Wrap="true" Width="70px"></cc1:Column>
                            <cc1:Column DataField="License_Valid_Upto" HeaderText="Valid_upto" Wrap="true" Width="100px"></cc1:Column>
                            <cc1:Column DataField="License_Extension_Copy" HeaderText="Ext File" Width="70px" Wrap="true">
                               <TemplateSettings  TemplateId="License_SA_Upload"/>
                                </cc1:Column>

                            <cc1:Column AllowDelete="true" HeaderText="Delete" Width="60px"></cc1:Column>
                        </Columns>
                        
                            
                       
                    </cc1:Grid>
                </center>

                    </div>


                <div class="row">
                  <h4 style="text-align:center"> Bank Guarantee Documents</h4>
                <center>
                    <cc1:Grid ID="GridBGDoc"  Width="80%" CallbackMode="false" runat="server" OnDeleteCommand="GridBGDoc_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                        <%--<ScrollingSettings ScrollWidth="95%" />--%>
                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                        <Columns>
                             <cc1:Column DataField="ID" Visible="false" HeaderText="ID"></cc1:Column>
                            <cc1:Column  HeaderText="Add Or View Extension" Width="100px" Wrap="true" >
                            <TemplateSettings TemplateId="ItemBGExtension" />
                        </cc1:Column>
                            <cc1:Column DataField="BG_Type" HeaderText="BG Type" Wrap="true" Width="100px"></cc1:Column> 
                            <cc1:Column DataField="BG_Beificiary" HeaderText="Beificiary" Wrap="true" Width="80px"></cc1:Column> 
                            <cc1:Column DataField="BG_Number" HeaderText="BG Number" Wrap="true" Width="100px"></cc1:Column> 
                            <cc1:Column DataField="BG_Amount" HeaderText="BG Amount" Wrap="true" Width="100px"></cc1:Column>
                             <cc1:Column DataField="BG_Date" HeaderText="BG Date" Wrap="true" Width="100px"></cc1:Column> 
                             <cc1:Column DataField="Claim_Date" HeaderText="Claim Date" Wrap="true" Width="100px"></cc1:Column>
                            <cc1:Column DataField="Original_Expiry_Date" HeaderText="Original Expiry Date" Wrap="true" Width="100px"></cc1:Column> 
                            <cc1:Column DataField="BG_Copy_FilePath" HeaderText="File" Width="130px" Wrap="true">
                               <TemplateSettings  TemplateId="BG_Copy_FilePath"/>
                                </cc1:Column>
                             <cc1:Column AllowDelete="true" HeaderText="Delete" Width="60px"></cc1:Column>
                        </Columns>
                        <Templates>
                            <cc1:GridTemplate ID="BG_Copy_FilePath" runat="server">
                                <Template>
                                <asp:LinkButton ID="lnkDownloadFile" Text='<%#Container.DataItem["BG_Copy_FilePath"] %>' 
                                    OnClick="lnkDownloadFile_Click" runat="server" CssClass="gridCB">
                                </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>
                           <cc1:GridTemplate ID="ItemBGExtension" runat="server">
                            <Template>
                            <asp:LinkButton ID="lnkBGExtensionAdd" Text="Add Or View Extension" CommandArgument='<%#Container.DataItem["ID"] %>' CommandName='<%#Container.DataItem["ID"] %>'  OnClick="lnkBGAddExtension_Click" runat="server" CssClass="gridCB">
                                        </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>

                    </Templates>
                       
                    </cc1:Grid>
                </center>
            </div>
                       <div class="row">
                  <h4 style="text-align:center"> Insurance Documents</h4>
                <center>
                    <cc1:Grid ID="GridInsuranceDoc" CallbackMode="false" runat="server" OnDeleteCommand="GridInsuranceDoc_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                        <%--<ScrollingSettings ScrollWidth="95%" />--%>
                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                        <Columns>
                             <cc1:Column DataField="ID" Visible="false" HeaderText="ID"></cc1:Column>
                             <cc1:Column  HeaderText="Add Or View Insurance" Width="100px" Wrap="true" >
                            <TemplateSettings TemplateId="ItemBInsurance" />
                        </cc1:Column>
                            <cc1:Column DataField="Insurance_Type" HeaderText="Insurance Type" Wrap="true" Width="80px"></cc1:Column> 
                            <cc1:Column DataField="Insurer_Name" HeaderText="Insurer Name" Wrap="true" Width="100px"></cc1:Column> 
                            <cc1:Column DataField="Policy_Number" HeaderText="Policy Number" Wrap="true" Width="100px"></cc1:Column> 
                            <cc1:Column DataField="Policy_Amount" HeaderText="Policy Amount" Wrap="true" Width="100px"></cc1:Column>
                             <cc1:Column DataField="Policy_Date" HeaderText="Policy Date" Wrap="true" Width="100px"></cc1:Column> 
                             <cc1:Column DataField="Claim_Date" HeaderText="Claim Date" Wrap="true" Width="100px"></cc1:Column>
                            <cc1:Column DataField="Insurance_Original_Expiry_Date" HeaderText="Original Expiry Date" Wrap="true" Width="100px"></cc1:Column> 
                            <cc1:Column DataField="Insurance_Copy_FilePath" HeaderText="File" Width="130px" Wrap="true">
                               <TemplateSettings  TemplateId="Insurance_Copy_FilePath"/>
                                </cc1:Column>
                             <cc1:Column AllowDelete="true" HeaderText="Delete" Width="60px"></cc1:Column>
                        </Columns>
                        <Templates>
                            <cc1:GridTemplate ID="Insurance_Copy_FilePath" runat="server">
                                <Template>
                                <asp:LinkButton ID="lnkDownloadFile" Text='<%#Container.DataItem["Insurance_Copy_FilePath"] %>' 
                                    OnClick="lnkDownloadFile_Click" runat="server" CssClass="gridCB">
                                </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>
                            <cc1:GridTemplate ID="ItemBInsurance" runat="server">
                            <Template>
                            <asp:LinkButton ID="lnkInsuranceRenewalAdd" Text="Add Or View Insurance" CommandArgument='<%#Container.DataItem["ID"] %>' CommandName='<%#Container.DataItem["ID"] %>'  OnClick="lnkInsuranceRenewal_Click" runat="server" CssClass="gridCB">
                                        </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>
                    </Templates>
                       
                    </cc1:Grid>
                </center>
            </div>
                 <div class="row">
                       <h4 style="text-align:center">Contract Agreement</h4>
                <center>
                    <cc1:Grid ID="GridContractAgreement" CallbackMode="false" runat="server" OnDeleteCommand="GridContractAgreement_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                        <%--<ScrollingSettings ScrollWidth="95%" />--%>
                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                        <Columns>
                             <cc1:Column DataField="ID" Visible="false" HeaderText="ID"></cc1:Column>
                            <cc1:Column DataField="Filename_Contract_Agreement" HeaderText="File Name"></cc1:Column> 
                            <cc1:Column DataField="Contract_Agreement_FilePath" HeaderText="File" Width="130px" Wrap="true">
                               <TemplateSettings  TemplateId="Template_Contract_Agreement"/>
                                </cc1:Column>
                            <cc1:Column AllowDelete="true" HeaderText="Delete" Width="80px"></cc1:Column>
                        </Columns>
                        <Templates>
                            <cc1:GridTemplate ID="Template_Contract_Agreement" runat="server">
                                <Template>
                                <asp:LinkButton ID="lnkDownloadFile" Text='<%#Container.DataItem["Contract_Agreement_FilePath"] %>' 
                                    OnClick="lnkDownloadFile_Click" runat="server" CssClass="gridCB">
                                </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>
                    </Templates>
                       
                    </cc1:Grid>
                </center>
            </div>
                     
                     </div>
           
            <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        </div>

    </div>

    <%--<ajaxToolkit:ModalPopupExtender ID="ModelLetterDept_Name" runat="server" PopupControlID="PanelLetter_Received_from" TargetControlID="Img_Letter_Received_from"
        CancelControlID="btnClose5" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>--%>
    <asp:Panel ID="PanelLetter_Received_from" Visible="false" runat="server" align="center" Style="z-index:1000;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnClose5" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                         <div class="col-md-2"> </div>
                         <div class="col-md-3">
                          Add Department Name
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" ControlToValidate="txtLetDepartmentName"  CssClass="Validation_Text" ValidationGroup="ValProjectType"></asp:RequiredFieldValidator>--%>
                        </div>
                          <div class="col-md-4" style="z-index:0">
                               <asp:TextBox ID="txtLetDepartmentName"  Enabled="true" CausesValidation="false" MaxLength="500"  runat="server"></asp:TextBox>

                            </div>
                    </div>
                    
                
                    <div class="row">
                        <center>
                            <cc1:Grid ID="GrdDept" CallbackMode="true"   AllowPageSizeSelection="false" runat="server" OnDeleteCommand="Grid_Document_name_DeleteCommand"  FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="false" AllowPaging="true" PageSize="5">
                                <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                <Columns>
                                    <cc1:Column DataField="ID" HeaderText="ID" Width="200px" Visible="false"></cc1:Column>
                                    <cc1:Column DataField="Letter_Recived_Dept" HeaderText="Department Name" Width="200px"></cc1:Column>
                                    <cc1:Column AllowDelete="true" HeaderText="Delete"></cc1:Column>
                                </Columns>
                                <Templates>
                                    <cc1:GridTemplate runat="server" ID="GridTemplate2">
                                        <Template>
                                            <asp:LinkButton ID="lnkBtnProjectID" CausesValidation="false" CommandName='<%# Container.DataItem["ID"] %>' OnClick="lnkBtnProjectID_Click" Text="Edit" CssClass="gridCB" runat="server"></asp:LinkButton>
                                        </Template>
                                    </cc1:GridTemplate>
                                </Templates>
                            </cc1:Grid>
                        </center>
                    </div>
                </div>
                
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnSaveLetter_Recived_Dept" CausesValidation="false" runat="server" Text="Save"  CssClass="btn btn-default" ValidationGroup="valDeptName" OnClick="btnSaveLetterRecivedDepartment" />
                        <asp:Button ID="Button2" runat="server" Text="Cancel"  CssClass="btn btn-default" ValidationGroup="valDeptName" CausesValidation="false" OnClick="btnCancelProjectType_Click"  />
                    </center>
                </div>
            </div>
        </div>
    </asp:Panel>

   
    <ajaxToolkit:ModalPopupExtender ID="ModelProjectTypePopup" runat="server" PopupControlID="PanelPrjectType" TargetControlID="ImgProjectType"
        CancelControlID="btnClose1" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelPrjectType" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnClose1" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-2">
                            Project Type Name
                            <asp:RequiredFieldValidator ID="RFV_ct" runat="server" ErrorMessage="*" ControlToValidate="txtProjectTypeName" CssClass="Validation_Text" ValidationGroup="ValProjectType"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txtProjectTypeName" runat="server" MaxLength="50" autocomplete= "off" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-2">
                            Project Type Code
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtProjectTypeCode" CssClass="Validation_Text" ValidationGroup="ValProjectType"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txtProjectTypeCode" runat="server" MaxLength="5" autocomplete= "off" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <center>
                            <cc1:Grid ID="Grid_ProjectType" CallbackMode="true"   AllowPageSizeSelection="false" runat="server" OnDeleteCommand="Grid_ProjectType_DeleteCommand"  FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="false" AllowPaging="true" PageSize="5">
                                <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                <Columns>
                                    <cc1:Column DataField="Proj_Type" HeaderText="Project Type" Width="200px"></cc1:Column>
                                    <cc1:Column DataField="Proj_Type_Code" HeaderText="Type Code" Width="100px"></cc1:Column>  
                                    <cc1:Column DataField="Proj_Type_ID" HeaderText="Edit" Width="100px">
                                        <TemplateSettings TemplateId="EditTemplate1" />
                                    </cc1:Column>
                                    <cc1:Column AllowDelete="true" HeaderText="Delete"></cc1:Column>
                                </Columns>
                                <Templates>
                                    <cc1:GridTemplate runat="server" ID="EditTemplate1">
                                        <Template>
                                            <asp:LinkButton ID="lnkBtnProjectID" CausesValidation="false" CommandName='<%# Container.DataItem["Proj_Type_ID"] %>' OnClick="lnkBtnProjectID_Click" Text="Edit" CssClass="gridCB" runat="server"></asp:LinkButton>
                                        </Template>
                                    </cc1:GridTemplate>
                                </Templates>
                            </cc1:Grid>
                        </center>
                    </div>
                </div>
                
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnSaveProjectType" runat="server" Text="Save"   CssClass="btn btn-default" ValidationGroup="ValProjectType" OnClick="btnSaveProjectType_Click" />
                        <asp:Button ID="btnCancelProjectType" runat="server" Text="Cancel"  CssClass="btn btn-default" ValidationGroup="ValProjectType" CausesValidation="false" OnClick="btnCancelProjectType_Click"  />
                    </center>
                </div>
            </div>
        </div>
    </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="ModalCompanyPopup" runat="server" PopupControlID="PanelCompany" TargetControlID="ImgCompany"
        CancelControlID="btnClose2" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelCompany" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnClose2" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-2">
                            Company Name
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ControlToValidate="txtCompanyName" CssClass="Validation_Text" ValidationGroup="ValCompany"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txtCompanyName" runat="server" MaxLength="100" autocomplete= "off" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-2">
                            Company Code
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" ControlToValidate="txtCompanyCode" CssClass="Validation_Text" ValidationGroup="ValCompany"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txtCompanyCode" runat="server" MaxLength="5" autocomplete= "off" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <center>
                            <cc1:Grid ID="Grid_Company" CallbackMode="true"   AllowPageSizeSelection="false" runat="server" OnDeleteCommand="Grid_Company_DeleteCommand"  FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="false" AllowPaging="true" PageSize="5">
                                <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                <Columns>
                                    <cc1:Column DataField="Company_Name" HeaderText="Company Name" Width="200px"></cc1:Column>
                                    <cc1:Column DataField="Company_Code" HeaderText="Code" Width="100px"></cc1:Column>                                      
                                    <cc1:Column HeaderText="Edit" Width="100px">
                                        <TemplateSettings TemplateId="EditTemplate2" />
                                    </cc1:Column>
                                    <cc1:Column AllowDelete="true" HeaderText="Delete"></cc1:Column>
                                </Columns>
                                <Templates>
                                    <cc1:GridTemplate runat="server" ID="EditTemplate2">
                                        <Template>
                                            <asp:LinkButton ID="lnkBtnCompany" CausesValidation="false" CommandName='<%# Container.DataItem["Company_Code"] %>' OnClick="lnkBtnCompany_Click" Text="Edit" CssClass="gridCB" runat="server"></asp:LinkButton>
                                        </Template>
                                    </cc1:GridTemplate>
                                </Templates>
                            </cc1:Grid>
                        </center>
                    </div>
                </div>
                
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnSaveCompany" runat="server" Text="Save"   CssClass="btn btn-default" ValidationGroup="ValCompany" OnClick="btnSaveCompany_Click" />
                        <asp:Button ID="btnCancelCompany" runat="server" Text="Cancel"  CssClass="btn btn-default" ValidationGroup="ValCompany" CausesValidation="false" OnClick="btnCancelCompany_Click"  />
                    </center>
                </div>
            </div>
        </div>
    </asp:Panel>




    <div class="modal small fade" id="myModal1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 650px">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>  <h5 id="myModalLabel1">Contact Details</h5></center>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-2">
                            Contact Type&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlContactType" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValContact" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlContactType" CssClass="form-control">
                                <asp:ListItem Text="-Select-"></asp:ListItem>
                                <asp:ListItem Text="Primary"></asp:ListItem>
                                <asp:ListItem Text="Alternate"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Name&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtContactName" CssClass="Validation_Text" ValidationGroup="ValContact" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtContactName" CssClass="form-control" MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            Department&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlDept" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValContact" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlDept" CssClass="form-control">
                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Designation&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="ddlDesignation" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValContact" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlDesignation" CssClass="form-control">
                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            Contact No
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtContactNo" CssClass="form-control input-pos-int" MaxLength="15" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">Email ID</div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtContactEmailID" CssClass="form-control" MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>

                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ValidationGroup="ValContact" runat="server"
                                ControlToValidate="txtContactEmailID" CssClass="Validation_Text"
                                ErrorMessage="Enter Valid Email" Display="Dynamic"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.([com in org net])+$">Enter Valid Email</asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            Dispatch Address
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox ID="txtDispatchAdd" CssClass="form-control" TextMode="MultiLine" Rows="2" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnAddContact" runat="server" Text="Save" OnClick="btnAddContact_Click"  CssClass="btn btn-default" ValidationGroup="ValContact"  />
                        <asp:Button ID="btnCancelContact" runat="server" Text="Cancel" OnClick="btnCancelContact_Click"  CssClass="btn btn-default"  CausesValidation="false"  />
                    </center>
                </div>
            </div>
        </div>
    </div>
     <div class="modal small fade" id="Modal_Sitelocation" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 650px">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>  <h5 id="Modal_SitelocationLabel1">Site location Details</h5></center>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-2">
                           Site Contact Person&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ControlToValidate="txtSiteContactPerson" CssClass="Validation_Text" ValidationGroup="ValSitelocation" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtSiteContactPerson" CssClass="form-control" MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                         <div class="col-md-2">
                           Mobile Number&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ControlToValidate="txtSiteMobileNumber" CssClass="Validation_Text" ValidationGroup="ValSitelocation" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtSiteMobileNumber" CssClass="form-control" MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            Address&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ControlToValidate="txtSiteAddress" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValSitelocation" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtSiteAddress" TextMode="MultiLine" CssClass="form-control" MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="Button1" runat="server" Text="Save" OnClick="btnSitelocation_Click"  CssClass="btn btn-default" ValidationGroup="ValSitelocation"  />
                        <asp:Button ID="Button7" runat="server" Text="Cancel" OnClick="btnCancelSitelocation_Click"  CssClass="btn btn-default"  CausesValidation="false"  />
                    </center>
                </div>
            </div>
        </div>
    </div>
    <div class="modal small fade" id="ModelLetterCorDepartment"  tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog " style="width: 750px" > 
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    
                </div>
                <div class="modal-body">
                     <div class="row">
                          <div class="col-md-2" style="text-align:right">
                               
                            </div>
                          <div class="col-md-8">
                             <span style="font-weight:300; font-size:20px; padding-left:10px">Letter Correspondence to Department </span>
                              </div>
                      
                         </div>
                     <div class="row">
                          <div class="col-md-5" style="text-align:right">
                               
                            </div>
                          <div class="col-md-4">
                              <asp:RadioButtonList ID="rblLetterSelect" runat="server" OnClientClick="hidebefore()" >
                                  <asp:ListItem Text="Fresh Letter " Value="FreshLetter" />
                                          <asp:ListItem Text="Reply To Letter" Value="ReplayToletter" />
                                    </asp:RadioButtonList>
                              </div>
                      
                         </div>
                    <hr />
                    <div id="FreshLetter_Div">
                    <div class="row">
                            <div class="col-md-2">
                                Letter ID&nbsp;
                            </div>
                            <div class="col-md-4"  runat="server">
                            <asp:TextBox ID="txtLetter_ID" Enabled="false"  autocomplete= "off" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                </div>
                            <div class="col-md-2">
                                Letter Reference Number&nbsp;
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="txtLetterRefNumber" CssClass="Validation_Text" ValidationGroup="Validation_Fresh_Letter" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtLetterRefNumber" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">

                             <div class="col-md-2">
                    Letter Received from 
                    <a href="#myModalFreshLetter" data-toggle="modal" role="button">
                        <asp:ImageButton ID="Img_Letter_Received_from" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" OnClientClick="hipop()" />
                    </a>
                </div>
                            <div class="col-md-4">
                                  <asp:DropDownList runat="server" ID="ddlLetterRecdFrom" CssClass="form-control">
                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                              
                            </div>
                            <div class="col-md-2">
                                Letter Received Date&nbsp;
                     <asp:RequiredFieldValidator ID="asd" ControlToValidate="txtLetterReceivedDate" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="Validation_Fresh_Letter" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtLetterReceivedDate" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender6" TargetControlID="txtLetterReceivedDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                               Date of Receipt 

                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtDateofReceipt" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender5" TargetControlID="txtDateofReceipt" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                            </div>
                            <div class="col-md-2">Reply by(date)

                            </div>
                            <div class="col-md-4">
                                 <asp:TextBox ID="txtReplyByDate" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:CalendarExtender ID="calReplyDate" TargetControlID="txtReplyByDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                            </div>
                        </div>
                    <div class="row">
                         <div class="col-md-2">
                               Mode of Recepit
                            </div>
                            
                            <div class="col-md-4">
                                 <asp:TextBox ID="txtModeofRecepit" CssClass="form-control" autocomplete= "off"  runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                               Copy of Letter
                            </div>
                            <div class="col-md-4">
                                <div ">
                    <asp:FileUpload runat="server" ID="fupCopyof_Fresh_Letter" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="fupCopyof_Fresh_Letter"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>
                            </div>
                        </div>
                         <div  class="row" >
                             <div class="col-md-2">
                               Subject
                            </div>
                            <div class="col-md-6">
                                 <asp:TextBox ID="txtFreshLetterSubject" CssClass="form-control"   TextMode="MultiLine" autocomplete= "off"  runat="server"></asp:TextBox>
                            </div>
                </div>
                </div>

                     <div id="ReplaytoLetter_Div">
                          <div class="row">
                            <div class="col-md-2">
                                Letter ID&nbsp;
                            </div>
                            <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="ddlReplayToLetter_ID_RTL" CssClass="form-control">
                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                </div>

                               <div class="col-md-2">
                               R Letter ID&nbsp;
                            </div>
                            <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="ddlFreshNewToletterId" CssClass="form-control"  >
                                    
                                </asp:DropDownList>
                                </div>
                           </div>
                               
                           <div class="row"> 
                          <div class="col-md-3">
                             Cor To Dep Letter ID&nbsp;
                            </div>
                            <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="ddlCorToDepID" CssClass="form-control">
                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                </div>
                       </div>
                        <div class="row">
                              <div class="col-md-2">
                               Letter sent to&nbsp;
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtLetterSentTo" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                               Letter Reference Number UGCL&nbsp;
                       </div>
                            <div class="col-md-4">
                               <asp:TextBox ID="txtLetterRefNumber_RTL" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                            </div>
                           
                        </div>
                        <div class="row">
                             <div class="col-md-2">
                                Date&nbsp;
                    
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="Date_RTL" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender8" TargetControlID="Date_RTL" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                            </div>
                            <div class="col-md-2">
                              Mode of Dispatch

                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtModeofDispatch_RTL" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                            </div>
                           
                        </div>
                        <div  class="row" >
                             <div class="col-md-2">
                               Subject
                            </div>
                            <div class="col-md-6">
                                 <asp:TextBox ID="txtSubjuectCorrespondence" CssClass="form-control"   TextMode="MultiLine" autocomplete= "off"  runat="server"></asp:TextBox>
                            </div>
                               </div>
                        <div class="row">
                             <div class="col-md-2">
                                Cc1&nbsp;
                            </div>
                            <div class="col-md-2">
                              <asp:TextBox ID="Txtcc1" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>  
                            </div>
                            <div class="col-md-2">
                              Cc2
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="Txtcc2" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                            </div>
                       <div class="col-md-2">
                              Cc3
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="Txtcc3" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                            </div>
                           
                        </div>
                        <div class="row">
                             <div class="col-md-2">
                                Cc4&nbsp;
                            </div>
                            <div class="col-md-2">
                              <asp:TextBox ID="Txtcc4" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>  
                            </div>
                            <div class="col-md-2">
                              Cc5
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="Txtcc5" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                            </div>
                      
                           
                        </div>



                    <div class="row">
                         <div class="col-md-2">
                                Letter Copy (Upload)
                            </div>
                            <div class="col-md-4">
                                
                    <asp:FileUpload runat="server" ID="fupLetterCopy_RTL" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="fupLetterCopy_RTL"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>  
                         <div class="col-md-2">
                             Acknowledgement Copy (Upload)
                            </div>
                          <div class="col-md-4">
                                
                    <asp:FileUpload runat="server" ID="fupAcknowledgementCopy_RTL" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="fupAcknowledgementCopy_RTL"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>  
                        </div>
                         </div>
                    <div class="modal-footer">
                    <center>
                      
           <asp:Button ID="btnSaveReplytoLetter" runat="server" Text="Save" OnClick="btnSave_FreshLetter_Click"  CssClass="btn btn-default" ValidationGroup="valLetterToDept"  />
                        <asp:Button ID="btnCancelSaveReplytoLetter" runat="server" Text="Cancel" OnClick="btnCancelSaveReplytoLetter_Click"  CssClass="btn btn-default"  CausesValidation="false"  />
                 </center>

                </div>
                </div>
                 
               </div>
            </div>
        </div>
     
    <div class="modal small fade" id="ModelLetterRecdDept" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 650px">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    
                </div>
                <div class="modal-body">
                     <div class="row">
                          <div class="col-md-2" style="text-align:right">
                               
                            </div>
                          <div class="col-md-8">
                             <span style="font-weight:300; font-size:20px; padding-left:10px">Letter Received from Department</span>
                              </div>
                      
                         </div>
                     <div class="row">
                          <div class="col-md-5" style="text-align:right">
                               
                            </div>
                          <div class="col-md-4">
                              <asp:RadioButtonList ID="rbLetRecFrom_Dept" runat="server" OnClientClick="hidebefore()" >
                                  <asp:ListItem Text="Fresh Letter " Value="FreshLetter" />
                                         <%-- <asp:ListItem Text="Reply To Letter" Value="ReplayToletter" />--%>
                                    </asp:RadioButtonList>
                              </div>
                      
                         </div>
                    <hr />
                    <div id="FreshLetter_Div_letRecFrom_Dept">
                    <div class="row">
                            <div class="col-md-2">
                                Letter ID&nbsp;
                            </div>
                            <div class="col-md-4"  runat="server">
                            <asp:TextBox ID="txtLetterID_letRecFrom_Dept" Enabled="false"  autocomplete= "off" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                </div>
                            <div class="col-md-2">
                                Letter Reference Number&nbsp;
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtLetterRefNo_letRecFrom_Dept" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                Letter Received from&nbsp;
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtLetterRecFrom_letRecFrom_Dept" CssClass="form-control" MaxLength="500" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                Letter Received Date&nbsp;
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtLetterRecDate_letRecFrom_Dept" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender9" TargetControlID="txtLetterRecDate_letRecFrom_Dept" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                               Date of Receipt 

                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="Date_of_Receipt_letRecFrom_Dept" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender10" TargetControlID="Date_of_Receipt_letRecFrom_Dept" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                            </div>
                            <div class="col-md-2">Reply by(date)

                            </div>
                            <div class="col-md-4">
                                 <asp:TextBox ID="txtReplyByDate_letRecFrom_Dept" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender11" TargetControlID="txtReplyByDate_letRecFrom_Dept" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                            </div>
                        </div>
                    <div class="row">
                         <div class="col-md-2">
                               Mode of Recepit
                            </div>
                            
                            <div class="col-md-4">
                                 <asp:TextBox ID="txtModeofRecepit_ModeofRecepit" CssClass="form-control" autocomplete= "off"  runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                               Copy of Letter
                            </div>
                            <div class="col-md-4">
                                <div ">
                    <asp:FileUpload runat="server" ID="fupCopyofLetter_letRecFrom_Dept" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="fupCopyof_Fresh_Letter"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>
                            </div>
                        </div>
                         <div  class="row" >
                             <div class="col-md-2">
                               Subject
                            </div>
                            <div class="col-md-6">
                                 <asp:TextBox ID="txtSubjext_letRecFrom_Dept" CssClass="form-control"   TextMode="MultiLine" autocomplete= "off"  runat="server"></asp:TextBox>
                            </div>
                </div>
                </div>
                    <%-- <div id="ReplaytoLetter_Div_letRecFrom_Dept">
                <div class="modal-body">
                    <div class="row">
                            <div class="col-md-2">
                                Letter ID&nbsp;
                            </div>
                            <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlLetter_ID_letRecFrom_Dept_RepLt" CssClass="form-control">
                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                </div>
                           
                            <div class="col-md-2">
                               Letter sent to&nbsp;
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ControlToValidate="txtContactName" CssClass="Validation_Text" ValidationGroup="ValContact" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtLetterSentTo_letRecFrom_Dept_RepLt" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                               Letter Reference Number UGCL&nbsp;
                            </div>
                            <div class="col-md-4">
                               <asp:TextBox ID="txtLetterRefNumber_letRecFrom_Dept_RepLt" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                Date&nbsp;
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="Date_RTL_letRecFrom_Dept" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender12" TargetControlID="Date_RTL_letRecFrom_Dept" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                              Mode of Dispatch

                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtModeofDispatch_letRecFrom_Dept_RepLt" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                Letter Copy (Upload)
                            </div>
                            <div class="col-md-4">
                                
                    <asp:FileUpload runat="server" ID="fupLetterCopy_letRecFrom_Dept_RepLt" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="fupLetterCopy_RTL"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>  
                        </div>
                    <div class="row">
                         <div class="col-md-2">
                             Acknowledgement Copy (Upload)
                            </div>
                          <div class="col-md-4">
                    <asp:FileUpload runat="server" ID="fupAcknowledgementCopy_letRecFrom_Dept_RepLt" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="fupAcknowledgementCopy_RTL"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>  
                        </div>
                        
                </div>
                </div>--%>
                 </div>
                
            
                    <div class="modal-footer">
                    <center>
                      
           <asp:Button ID="Button22" runat="server" Text="Save" OnClick="btnSave__letRecFrom_Dept_RepLt_Click" CausesValidation="false"  CssClass="btn btn-default" ValidationGroup="testval"  />
                        <asp:Button ID="Button23" runat="server" Text="Cancel" OnClick="btnCancel_FreshLetter_Click"  CssClass="btn btn-default"  CausesValidation="false"  />
                 </center>

                </div>
                </div>
               </div>
            </div>
        
    <div class="modal small fade" id="ModelVariationsPriceAdjustments" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 650px">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>  <h5 id="ModelVariationsPriceAdj">Variations/Price Adjustments</h5></center>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="row">
                            <div class="col-md-2">
                               File Name
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtFilename_Variations" CssClass="form-control"  runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                 <asp:FileUpload runat="server" ID="fup_Variations" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="fup_Variations"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>  
                            </div>
                         <div class="row">
                            <div class="col-md-2">
                               Approval Documents Name
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtApprovalDocumentVariations" CssClass="form-control"  runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                 <asp:FileUpload runat="server" ID="FupApprovalDocumentVariations" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="fup_Variations"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>  
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                    <center>
           <asp:Button ID="btn_Variations" runat="server" Text="Save" OnClick="btn_Variations_Click"  CssClass="btn btn-default" ValidationGroup="Val_Variations"  />
                        <asp:Button ID="Button16" runat="server" Text="Cancel" OnClick="btnCancelVariations_Click"  CssClass="btn btn-default"  CausesValidation="false"  />
                 </center>

                </div>
                </div>
            
            </div>
        </div>
    
    <div class="modal small fade" id="ModelVendorCredentialsApprovals" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 650px">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>  <h5 id="ModelVendorCredentials_Approvals">Vendor Credentials Approvals</h5></center>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="row">
                            <div class="col-md-2">
                               File Name
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtFilename_VendorCredentials" CssClass="form-control"  runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                 <asp:FileUpload runat="server" ID="fupFilename_VendorCredentials" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="fup_Variations"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>  
                            </div>
                        </div>
                      <div class="row">
                            <div class="col-md-2">
                               Approval Documents Name
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtApprovalDocumentsCredentialsApproval" CssClass="form-control"  runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                 <asp:FileUpload runat="server" ID="fupApprovalDocumentsCredentialsApproval" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="fup_Variations"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>  
                            </div>
                    </div>
                    <div class="modal-footer">
                    <center>
           <asp:Button ID="btn_VendorCredentials" runat="server" Text="Save" OnClick="btn_VendorCredentials_Click"  CssClass="btn btn-default" ValidationGroup="Val_VendorCredentials"  />
                        <asp:Button ID="Button18" runat="server" Text="Cancel" OnClick="btnCancelVendorCredentials_Click"  CssClass="btn btn-default"  CausesValidation="false"  />
                 </center>

                </div>
                </div>
            
            </div>
        </div>
     
    <div class="modal small fade" id="ModelContractAgreement" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 650px">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>  <h5 id="Model_ContractAgreement">Contract Agreement</h5></center>
                </div>
                 <div class="modal-body">
                    <div class="row">
                        <div class="row">
                            <div class="col-md-2">
                               File Name
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txt_Filename_ContractAgreement" CssClass="form-control"  runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                 <asp:FileUpload runat="server" ID="fupContractAgreement" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="fupContractAgreement"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>  
                            </div>
                        </div>

                    </div>
                <div class="modal-footer">
                    <center>
                      
           <asp:Button ID="btnSaveContractAgreement" runat="server" Text="Save" OnClick="btnSave_ContractAgreement_Click"  CssClass="btn btn-default" ValidationGroup="ValContractAgreement"  />
                        <asp:Button ID="btnCancelContractAgreement" runat="server" Text="Cancel" OnClick="btnCancelContractAgreement_Click"  CssClass="btn btn-default"  CausesValidation="false"  />
                 </center>

                </div>
            </div>
        </div>
    </div>

     <div class="modal small fade" id="ModelBillofQuantity" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 650px">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>  <h5 id="Model_BillofQuantity">Bill of Quantity</h5></center>
                </div>
               <div class="modal-body">
                    <div class="row">
                        <div class="row">
                            <div class="col-md-2">
                               File Name
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtBillofQuantity" CssClass="form-control"  runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                 <asp:FileUpload runat="server" ID="fupBillofQuantity" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="fupBillofQuantity"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>  
                            </div>
                        </div>

                    </div>
                <div class="modal-footer">
                    <center>
                      
           <asp:Button ID="btnSaveBillofQuantity" runat="server" Text="Save" OnClick="btnSave_BillofQuantity_Click"  CssClass="btn btn-default"  />
                        <asp:Button ID="Button13" runat="server" Text="Cancel" OnClick="btnCancel_BillofQuantity_Click"  CssClass="btn btn-default"  CausesValidation="false"  />
                 </center>

                </div>
            </div>
        </div>
    </div>

     <div class="modal small fade" id="ModelDrawings" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 650px">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>  <h5 id="Model_Drawings">Drawings</h5></center>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="row">
                            <div class="col-md-2">
                               File Name
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtDrawings" CssClass="form-control"  runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                 <asp:FileUpload runat="server" ID="fupDrawings" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="fupBillofQuantity"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>  
                            </div>
                        </div>
                     <div class="row">
                            <div class="col-md-2">
                               Approval Documents Name
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtApprovalDocumentsDrawings" CssClass="form-control"  runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                 <asp:FileUpload runat="server" ID="fupApprovalDocumentsDrawings" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="fup_Variations"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>  
                            </div>
                    </div>
                <div class="modal-footer">
                    <center>
                      
           <asp:Button ID="btn_Save_Drawings" runat="server" Text="Save" OnClick="btnSave_Drawings_Click"  CssClass="btn btn-default"  />
                        <asp:Button ID="BtnCancel_Drawings" runat="server" Text="Cancel" OnClick="btnCancel_Drawings_Click"  CssClass="btn btn-default"  CausesValidation="false"  />
                 </center>

                </div>
            </div>
        </div>
    </div>
    <div class="modal small fade" id="ModelBGDocuments" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 650px">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>  <h5 id="Model_BGDocuments">Bank Guarantee</h5></center>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="row">
                             <div class="col-md-2">
                    BG Type 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ControlToValidate="ddlLicensetype" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValProject" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <a href="#myModalBG" data-toggle="modal" role="button">
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                    </a>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlBGType" CssClass="form-control" runat="server" TabIndex="2"></asp:DropDownList>
                </div>
                             <div class="col-md-2">
                               BG Beificiary 
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtBGBeificiary" CssClass="form-control"  runat="server"></asp:TextBox>
                            </div>
                            </div>
                         <div class="row">
                            <div class="col-md-2">
                               BG Number 
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtBGNumber" CssClass="form-control"  runat="server"></asp:TextBox>
                            </div>
                             <div class="col-md-2">
                               BG Amount 
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtBGAmount" CssClass="form-control"  runat="server"></asp:TextBox>
                            </div>
                            </div>
                         <div class="row">
                            <div class="col-md-2">
                               BG Date 
                            </div>
                           <div class="col-md-4">
                    <asp:TextBox ID="txtBGDate" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender7" TargetControlID="txtBGDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender> 
                             </div>
                             <div class="col-md-2">
                              Original Expiry Date
                            </div>
                            <div class="col-md-4">
                    <asp:TextBox ID="txtBGOriginalExpiryDate" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender12" TargetControlID="txtBGOriginalExpiryDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender> 
                             </div>
                            </div>
                          <div class="row">
                            <div class="col-md-2">
                               Claim Date
                            </div>
                             <div class="col-md-4">
                    <asp:TextBox ID="txtBGClaimDate" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender13" TargetControlID="txtBGClaimDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender> 
                             </div>
                             <div class="col-md-2">
                              BG Copy
                            </div>
                            <div class="col-md-4">
                                 <asp:FileUpload runat="server" ID="fupBGCopy" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="fup_Variations"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>  
                            </div>
                        </div>
                    </div>
                <div class="modal-footer">
                    <center>
                      
           <asp:Button ID="btnSaveBG" runat="server" Text="Save" OnClick="btnSaveBG_Click"  CssClass="btn btn-default"  />
                        <asp:Button ID="btnCancelBG" runat="server" Text="Cancel" OnClick="btnCancelBG_Click"  CssClass="btn btn-default"  CausesValidation="false"  />
                 </center>

                </div>
            </div>
        </div>
    </div>
      <div class="modal small fade" id="ModelIsurance" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 650px">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>  <h5 id="ModelIsurance_Heading">Insurance</h5></center>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="row">
                             <div class="col-md-2">
                    Insurance Type 
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ControlToValidate="ddlInsuranceType" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValProject" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <a href="#myModalInsuranceType" data-toggle="modal" role="button">
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                    </a>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlInsuranceType" CssClass="form-control" runat="server" TabIndex="2"></asp:DropDownList>
                </div>
                             <div class="col-md-2">
                               Insurer Name 
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtInsurerName" CssClass="form-control"  runat="server"></asp:TextBox>
                            </div>
                            </div>
                         <div class="row">
                            <div class="col-md-2">
                               Policy Number 
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtPolicyNumber" CssClass="form-control"  runat="server"></asp:TextBox>
                            </div>
                             <div class="col-md-2">
                               Policy Amount
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtPolicyAmount" CssClass="form-control"  runat="server"></asp:TextBox>
                            </div>
                            </div>
                         <div class="row">
                            <div class="col-md-2">
                               Policy Date  
                            </div>
                           <div class="col-md-4">
                    <asp:TextBox ID="txtPolicyDate" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender14" TargetControlID="txtPolicyDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender> 
                             </div>
                             <div class="col-md-2">
                             Original Expiry Date
                            </div>
                            <div class="col-md-4">
                    <asp:TextBox ID="txtInsurerOriginalExpiryDate" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender15" TargetControlID="txtInsurerOriginalExpiryDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender> 
                             </div>
                            </div>
                          <div class="row">
                             <div class="col-md-2">
                              Insurance Copy
                            </div>
                            <div class="col-md-4">
                                 <asp:FileUpload runat="server" ID="fupInsuranceCopy" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="fup_Variations"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>  
                            </div>
                        </div>
                   
                    </div>
                <div class="modal-footer">
                    <center>
                      
           <asp:Button ID="btnSaveInsurance" runat="server" Text="Save" OnClick="btnSaveInsurance_Click"  CssClass="btn btn-default"  />
                        <asp:Button ID="btnCancelInsurance" runat="server" Text="Cancel" OnClick="btnCancelInsurance_Click"  CssClass="btn btn-default"  CausesValidation="false"  />
                 </center>

                </div>
            </div>
        </div>
    </div>   
  
       <div class="modal small fade" id="ModelLicenses" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 650px">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>  <h5 id="Model_Licenses">Add Licenses</h5></center>
                </div>
               <div class="modal-body">
                    <div class="row">
                        <div class="row">
                               <div class="col-md-2">
                    License Type
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ControlToValidate="ddlLicensetype" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValProject" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <a href="#myModalLicence" data-toggle="modal" role="button">
                        <asp:ImageButton ID="ImageBtnLicenseType" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                    </a>
                </div>
                               <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlLicensetype" CssClass="form-control" TabIndex="9"></asp:DropDownList>
                    </div>
                            <div class="col-md-2">Issuer Name
                </div> 
                            <div class="col-md-4">
                                <asp:TextBox ID="txt_issuername" CssClass="form-control"  runat="server"></asp:TextBox>
                            </div>
                            </div>
                         <div class="row">
                            <div class="col-md-2">
                               License Number
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txt_LicenseNumber" CssClass="form-control"  runat="server"></asp:TextBox>
                                
                            </div>
                            <div class="col-md-2">Amount
                                 </div> 
                            <div class="col-md-4">
                                <asp:TextBox ID="txtamt" CssClass="form-control"  runat="server"></asp:TextBox>
                            </div>
                            </div>
                        <div class="row">
                            <div class="col-md-2">
                               License Date 
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtlicensedate" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender16" TargetControlID="txtlicensedate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                            </div>
                            <div class="col-md-2">Original Expiry Date
                                 </div> 
                            <div class="col-md-4">
                                <asp:TextBox ID="txtexpdate" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                                 <asp:CalendarExtender ID="CalendarExtender17" TargetControlID="txtexpdate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                                </div>
                            </div>
                        <div class="row">
                             <div class="col-md-2">
                               Licence Copy
                            </div>
                             <div class="col-md-4">
                                <asp:FileUpload runat="server" ID="FileuploadLicencecopy" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="FileuploadLicencecopy"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                            </div>
                        </div>
                        </div>

                    </div>
                <div class="modal-footer">
                    <center>
                      
           <asp:Button ID="btnLicense" runat="server" Text="Save" OnClick="btnLicense_Click" CssClass="btn btn-default"  />
                        <asp:Button ID="Button20" runat="server" Text="Cancel" OnClick="btnCancel_BillofQuantity_Click"  CssClass="btn btn-default"  CausesValidation="false"  />
                 </center>

                </div>
            </div>
        </div>
    </div>
    <div class="modal small fade" id="myModal112" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 650px">
            <div class="modal-content">
                <div class="modal-header">
                  
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                     <center>  <h5 id="myModalLabel112">Document Masters</h5></center>
                    </div>
                <div class="modal-body"  id="mybtnalig">
                    <div class="row">
                        <div class="col-sm-4">
                               <a href="#ModelLetterRecdDept" data-toggle="modal" role="button">
                        <asp:Button ID="Button8" runat="server" Text="Letter Received from Department" OnClientClick="Hidepopup()"  CssClass="btn btn-default" TabIndex="12"></asp:Button>
                    </a>
                        </div>
                        <div class="col-sm-4">
                             <a href="#ModelLetterCorDepartment" data-toggle="modal" role="button" style="width:200px" >
                        <asp:Button ID="btnLetterCorDpet"  runat="server" Text="Letter Correspondence to Department" CssClass="btn btn-default" OnClientClick="Hidepopup()" TabIndex="12"></asp:Button>
                    </a>

                        </div>
                         
                         <div class="col-sm-4">
                              <a href="#ModelVariationsPriceAdjustments" data-toggle="modal" role="button">
                        <asp:Button ID="Button10" runat="server" Text="Variations/ Price Adjustments" OnClientClick="Hidepopup()"  CssClass="btn btn-default" TabIndex="12"></asp:Button>
                    </a>

                        </div>
                    </div>
                    <div class="row">
                         <div class="col-sm-4">
                     <a href="#ModelVendorCredentialsApprovals" data-toggle="modal" role="button">
                        <asp:Button ID="Button9" runat="server" Text="Vendor Credentials / Approvals" OnClientClick="Hidepopup()"  CssClass="btn btn-default" TabIndex="12"></asp:Button>
                    </a>
                        </div>
                         <div class="col-sm-4">
                                <a href="#ModelContractAgreement" data-toggle="modal" role="button">
                        <asp:Button ID="Button4" runat="server" Text="Contract Agreement" OnClientClick="Hidepopup()"  CssClass="btn btn-default" TabIndex="12"></asp:Button>
                    </a>

                        </div>
                          <div class="col-sm-4">
                              <a href="#ModelBillofQuantity" data-toggle="modal" role="button">
                        <asp:Button ID="Button5" runat="server" Text="Bill of Quantity" CssClass="btn btn-default" OnClientClick="Hidepopup()"  TabIndex="12"></asp:Button>
                    </a>

                        </div>
                    </div>
                     <div class="row">
                         <div class="col-sm-4">
                              <a href="#ModelBGDocuments" data-toggle="modal" role="button">
                        <asp:Button ID="Button11" runat="server" Text="Bank Guarantee" CssClass="btn btn-default" OnClientClick="Hidepopup()"  TabIndex="12"></asp:Button>
                    </a>
                        </div>
                          <div class="col-sm-4">
                              <a href="#ModelIsurance" data-toggle="modal" role="button">
                        <asp:Button ID="Button12" runat="server" Text="Isurance" CssClass="btn btn-default" OnClientClick="Hidepopup()"  TabIndex="12"></asp:Button>
                    </a>
                        </div>
                          <div class="col-sm-4">
                              <a href="#ModelLicenses" data-toggle="modal" role="button">
                        <asp:Button ID="Button14" runat="server" Text="Licenses" CssClass="btn btn-default" OnClientClick="Hidepopup()"  TabIndex="12"></asp:Button>
                    </a>
                        </div>
                    </div>
                     <div class="row">
                         <div class="col-sm-4">
                              <a href="#ModelDrawings" data-toggle="modal" role="button">
                        <asp:Button ID="Button6" runat="server" Text="Drawings" CssClass="btn btn-default" OnClientClick="Hidepopup()"  TabIndex="12"></asp:Button>
                    </a>
                        </div>
                    </div>
                </div>
        </div>
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
                    <center><h5>Add Bank Guarantee Extension</h5></center>
                </div>
                <div class="modal-body">

                 <div class="row">
                            <div class="col-md-2">
                               Extension
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtBGExtension" CssClass="form-control"  runat="server"></asp:TextBox>
                                <asp:HiddenField ID="hdnBGExtensionID" runat="server" />
                            </div>
                            <div class="col-md-2">Valid upto
                                 </div> 
                      <div class="col-md-4">
                                <asp:TextBox ID="txtBGValidupto" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                                 <asp:CalendarExtender ID="CalendarExtender19" TargetControlID="txtBGValidupto" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                                </div>
                            </div>
                     <div class="row">
                             <div class="col-md-2">
                               Extension Copy
                            </div>
                             <div class="col-md-4">
                                <asp:FileUpload runat="server" ID="fupBGExtensionCopy" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="FileuploadLicencecopy"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                            </div>
                        </div>
                    
                     <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnSaveBGExtension" runat="server" Text="Save" CssClass="btn btn-default" ValidationGroup="ValSubItem" OnClick="btnSaveBGExtension_Click" />
                        <asp:Button ID="btnCancelBGExtension" runat="server"  Text="Cancel"  CssClass="btn btn-default" CausesValidation="false" OnClick="btnCancelBGExtension_Click" />        
                 </center>
                         <br />
                    <hr />
                </div>
                     <div class="row">
                  <h4 style="text-align:center"> BG Extension Documents</h4>
                <center>
                    <cc1:Grid ID="GridBG_Extension" CallbackMode="false" runat="server" OnDeleteCommand="GridBGExtension_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                        <%--<ScrollingSettings ScrollWidth="95%" />--%>
                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                        <Columns>
                             <cc1:Column DataField="BGExtension" HeaderText="Extension" Wrap="true" Width="100px"></cc1:Column>
                            <cc1:Column DataField="Valid_Upto" HeaderText="Valid Upto" Wrap="true" Width="100px"></cc1:Column> 
                            <cc1:Column DataField="Extension_Copy" HeaderText="File" Width="130px" Wrap="true">
                               <TemplateSettings  TemplateId="Extension_Copy"/>
                                </cc1:Column>
                             <cc1:Column AllowDelete="true" HeaderText="Delete" Width="60px"></cc1:Column>
                        </Columns>
                        <Templates>
                            <cc1:GridTemplate ID="Extension_Copy" runat="server">
                                <Template>
                                <asp:LinkButton ID="lnkDownloadFile" Text='<%#Container.DataItem["Extension_Copy"] %>' 
                                    OnClick="lnkDownloadFile_Click" runat="server" CssClass="gridCB">
                                </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>
                    </Templates>
                       
                    </cc1:Grid>
                </center>
            </div>
                </div>
               
            </div>
        </div>

    </asp:Panel>

      <ajaxToolkit:ModalPopupExtender ID="ModalINSRItem" runat="server" PopupControlID="PanelINSRItem" TargetControlID="btnAddSubItem"
        CancelControlID="BtnCloseInsurance" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    
    <asp:Panel ID="PanelINSRItem" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="BtnCloseInsurance" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center><h5>Add Insurance Renewal </h5></center>
                </div>
                <div class="modal-body">

                 <div class="row">
                            <div class="col-md-2">
                               Insurance Name
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtInsuranceName" CssClass="form-control"  runat="server"></asp:TextBox>
                                <asp:HiddenField ID="hdnInsuranceR" runat="server" />
                            </div>
                            <div class="col-md-2">Valid upto
                                 </div> 
                      <div class="col-md-4">
                                <asp:TextBox ID="txtInsuranceValidUpto" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                                 <asp:CalendarExtender ID="CalendarExtender20" TargetControlID="txtInsuranceValidUpto" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                                </div>
                            </div>
                     <div class="row">
                             <div class="col-md-2">
                               Insurance Renewal Copy
                            </div>
                             <div class="col-md-4">
                                <asp:FileUpload runat="server" ID="FupInsuranceRenewalCopy" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="FileuploadLicencecopy"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                            </div>
                        </div>
                    
                     <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnSaveInsuranceRenewal" runat="server" Text="Save" CssClass="btn btn-default" ValidationGroup="ValSubItem" OnClick="btnSaveInsuranceRenewal_Click" />
                        <asp:Button ID="btnCancelInsuranceRenewal" runat="server"  Text="Cancel"  CssClass="btn btn-default" CausesValidation="false" OnClick="btnCancelInsuranceRenewal_Click" />        
                 </center>
                         <br />
                    <hr />
                </div>
                     <div class="row">
                  <h4 style="text-align:center">Insurance Renewal  Documents</h4>
                <center>
                    <cc1:Grid ID="Grid_Insurance_Renewal" CallbackMode="false" runat="server" OnDeleteCommand="GridBGExtension_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                        <%--<ScrollingSettings ScrollWidth="95%" />--%>
                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                        <Columns>
                             <cc1:Column DataField="Insurance_Name" HeaderText="Insurance Name" Wrap="true" Width="100px"></cc1:Column>
                            <cc1:Column DataField="Insurance_Renewal_Valid_Upto" HeaderText="Valid Upto" Wrap="true" Width="100px"></cc1:Column> 
                            <cc1:Column DataField="Insurance_Renewal_Copy" HeaderText="File" Width="130px" Wrap="true">
                               <TemplateSettings  TemplateId="Extension_Copy"/>
                                </cc1:Column>
                             <cc1:Column AllowDelete="true" HeaderText="Delete" Width="60px"></cc1:Column>
                        </Columns>
                        <Templates>
                            <cc1:GridTemplate ID="GridTemplate5" runat="server">
                                <Template>
                                <asp:LinkButton ID="lnkDownloadFile" Text='<%#Container.DataItem["Insurance_Renewal_Copy"] %>' 
                                    OnClick="lnkDownloadFile_Click" runat="server" CssClass="gridCB">
                                </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>
                    </Templates>
                       
                    </cc1:Grid>
                </center>
            </div>
                </div>
               
            </div>
        </div>

    </asp:Panel>

     <ajaxToolkit:ModalPopupExtender ID="ModalLicensesItem" runat="server" PopupControlID="PanelLicensesItem" TargetControlID="btnAddSubItem"
        CancelControlID="BtnCloseLicenses" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    
    <asp:Panel ID="PanelLicensesItem" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="BtnCloseLicenses" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center><h5>Add Licenses Extension </h5></center>
                </div>
                <div class="modal-body">

                 <div class="row">
                            <div class="col-md-2">
                               Extension Name
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtExtensionLicenses" CssClass="form-control"  runat="server"></asp:TextBox>
                                <asp:HiddenField ID="hdnLicenses" runat="server" />
                            </div>
                            <div class="col-md-2">Valid upto
                                 </div> 
                      <div class="col-md-4">
                                <asp:TextBox ID="txtValidUptoLicensesExtension" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                                 <asp:CalendarExtender ID="CalendarExtender21" TargetControlID="txtValidUptoLicensesExtension" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                                </div>
                            </div>
                     <div class="row">
                             <div class="col-md-2">
                               License Extension Copy
                            </div>
                             <div class="col-md-4">
                                <asp:FileUpload runat="server" ID="fupLicenseExtensionCopy" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="FileuploadLicencecopy"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                            </div>
                        </div>
                    
                     <div class="modal-footer">
                    <center>
                        
                        <asp:Button ID="btnLicenseSave" runat="server" Text="Save" CssClass="btn btn-default" ValidationGroup="ValSubItem" OnClick="btnLicenseSave_Click" />
                        <asp:Button ID="btnLicenseCancel" runat="server"  Text="Cancel"  CssClass="btn btn-default" CausesValidation="false" OnClick="btnLicenseCancel_Click" />        
                 </center>
                         <br />
                    <hr />
                </div>
                     <div class="row">
                  <h4 style="text-align:center">License Extension  Documents</h4>
                <center>
                    <cc1:Grid ID="Grid_ExtensionLicense" CallbackMode="false" runat="server" OnDeleteCommand="GridBGExtension_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                        <%--<ScrollingSettings ScrollWidth="95%" />--%>
                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                        <Columns>
                             <cc1:Column DataField="License_Extension" HeaderText="License Name" Wrap="true" Width="100px"></cc1:Column>
                            <cc1:Column DataField="License_Valid_Upto" HeaderText="Valid Upto" Wrap="true" Width="100px"></cc1:Column> 
                            <cc1:Column DataField="License_Extension_Copy" HeaderText="File" Width="130px" Wrap="true">
                               <TemplateSettings  TemplateId="License_Copy"/>
                                </cc1:Column>
                             <cc1:Column AllowDelete="true" HeaderText="Delete" Width="60px"></cc1:Column>
                        </Columns>
                        <Templates>
                            <cc1:GridTemplate ID="License_Copy" runat="server">
                                <Template>
                                <asp:LinkButton ID="lnkDownloadFile" Text='<%#Container.DataItem["License_Extension_Copy"] %>' 
                                    OnClick="lnkDownloadFile_Click" runat="server" CssClass="gridCB">
                                </asp:LinkButton>
                            </Template>
                        </cc1:GridTemplate>
                    </Templates>
                       
                    </cc1:Grid>
                </center>
            </div>
                </div>
               
            </div>
        </div>

    </asp:Panel>
    <div class="modal small fade" id="myModalFreshLetter" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 650px">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnCloseFreshLetter" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                         <div class="col-md-2"> </div>
                         <div class="col-md-3">
                          Add Department Name
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ErrorMessage="*" ControlToValidate="txtLetDepartmentNameUp"  CssClass="Validation_Text" ValidationGroup="valDeptName"></asp:RequiredFieldValidator>
                        </div>
                          <div class="col-md-4">
                               <asp:TextBox ID="txtLetDepartmentNameUp" CssClass="form-control" MaxLength="500"  runat="server"></asp:TextBox>
                            </div>
                    </div>
                    
                
                    <div class="row">
                        <center>
                            <cc1:Grid ID="GridLetter_Recived_Dept" CallbackMode="true"   AllowPageSizeSelection="false" runat="server" OnDeleteCommand="Grid_Document_name_DeleteCommand"  FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="false" AllowPaging="true" PageSize="5">
                                <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                <Columns>
                                    <cc1:Column DataField="ID" HeaderText="ID" Width="200px" Visible="false"></cc1:Column>
                                    <cc1:Column DataField="Letter_Recived_Dept" HeaderText="Department Name" Width="200px"></cc1:Column>
                                    <cc1:Column AllowDelete="true" HeaderText="Delete"></cc1:Column>
                                </Columns>
                                <Templates>
                                    <cc1:GridTemplate runat="server" ID="GridTemplate3">
                                        <Template>
                                            <asp:LinkButton ID="lnkBtnProjectID" CausesValidation="false" CommandName='<%# Container.DataItem["ID"] %>' OnClick="lnkBtnProjectID_Click" Text="Edit" CssClass="gridCB" runat="server"></asp:LinkButton>
                                        </Template>
                                    </cc1:GridTemplate>
                                </Templates>
                            </cc1:Grid>
                        </center>
                    </div>
                </div>
                
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnSaveLetterRecDepartment" runat="server" CausesValidation="false" Text="Save" CssClass="btn btn-default" ValidationGroup="ValLCT" OnClick="btnSaveLetterRecivedDepartment" />
                        <asp:Button ID="Button17" runat="server" Text="Cancel"  CssClass="btn btn-default" ValidationGroup="ValLCT" CausesValidation="false" OnClick="btnCancelProjectType_Click"  />
                    </center>
                </div>
            </div>
            </div>
        </div>

     <div class="modal small fade" id="myModalLicence" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 650px">
            <div class="modal-content">
                <div class="modal-header">
                  
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                     <center>  <h5 id="myModalLicenceDocument">Document Masters</h5></center>
                    </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-2">
                            License Name
                            <asp:RequiredFieldValidator  ID="RFVLCTLN" runat="server" ErrorMessage="*" ControlToValidate="txtLicenseName" CssClass="Validation_Text" ValidationGroup="ValLCT" ></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txtLicenseName"  runat="server"  MaxLength="100" autocomplete= "off" CssClass="form-control"></asp:TextBox>
                          
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-2">
                            License Code
                            <asp:RequiredFieldValidator ID="RFVLCTLC" runat="server" ErrorMessage="*" ControlToValidate="txtLicenseTypeCode" CssClass="Validation_Text" ValidationGroup="ValLCT"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txtLicenseTypeCode" runat="server" MaxLength="5" autocomplete= "off" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <center>
                            <cc1:Grid ID="GridLisenceType" CallbackMode="true"   AllowPageSizeSelection="false" runat="server" OnDeleteCommand="GridLisenceType_DeleteCommand"  FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="false" AllowPaging="true" PageSize="5">
                                <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                <Columns>
                                    <cc1:Column DataField="License_Name" HeaderText="License Name" Width="200px"></cc1:Column>
                                    <cc1:Column DataField="License_Code" HeaderText="Code" Width="100px"></cc1:Column>                                      
                                    <cc1:Column HeaderText="Edit" Width="100px">
                                        <TemplateSettings TemplateId="EditLCT" />
                                    </cc1:Column>
                                    <cc1:Column AllowDelete="true" HeaderText="Delete"></cc1:Column>
                                </Columns>
                                <Templates>
                                    <cc1:GridTemplate runat="server" ID="EditLCT">
                                        <Template>
                                            <asp:LinkButton ID="lnkBtnLicenseType" CausesValidation="false" CommandName='<%# Container.DataItem["License_Code"] %>' OnClick="lnkBtnLicenseType_Click" Text="Edit" CssClass="gridCB" runat="server"></asp:LinkButton>
                                        </Template>
                                    </cc1:GridTemplate>
                                </Templates>
                            </cc1:Grid>
                        </center>
                    </div>
                     <div class="row">
                           <center>
                         <asp:Button ID="BtnLCTSave" runat="server" Text="Save" CssClass="btn btn-default" ValidationGroup="ValLCT" onclick="BtnLCTSave_Click"  />
                        <asp:Button ID="BtnLCTCancel" runat="server" Text="Cancel"  CssClass="btn btn-default" ValidationGroup="ValLCT" CausesValidation="false" onclick="BtnLCTCancel_Click"   />
                               </center>
                    </div>
                </div>
        </div>
    </div>
    </div>
    <div class="modal small fade" id="PanelFreshLetter" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
         <div class="modal-dialog" style="width:650px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="BtnCloseFreshLetter" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center><h5 id="myModalamt2"><asp:Label ID="lblmodalfreshletter" runat="server" Text="Letter Correspondence to Department"></asp:Label></h5></center>
                </div>
                <div class="modal-body">
                     <div class="row">
                          <div class="col-md-2" style="text-align:right">
                               
                            </div>
                          <div class="col-md-8">
                             <span style="font-weight:300; font-size:20px; padding-left:10px">Letter Received from Department</span>
                              </div>
                      
                         </div>
                     <div class="row">
                          <div class="col-md-5" style="text-align:right">
                               
                            </div>
                          <div class="col-md-4">
                              <asp:RadioButtonList ID="rblFreshReply" runat="server">
                                  <asp:ListItem Text="Fresh Letter " Value="FreshLetter"  />
                                          <%--<asp:ListItem Text="Reply To Letter" Value="ReplayToletter" />--%>
                                    </asp:RadioButtonList>
                              </div>
                      
                         </div>
                    <hr />
              <div id="">

              </div>
                    <div class="row">
                            <div class="col-md-2">
                                Letter ID&nbsp;
                            </div>
                            <div class="col-md-4"  runat="server">
                            <asp:TextBox ID="txtFreshLetterID" Enabled="false"  autocomplete= "off" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                </div>
                            <div class="col-md-2">
                                Letter Reference Number&nbsp;
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtFreshLetterRefNo" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                Letter Received from&nbsp;
                                <a href="#myModalFreshLetter" data-toggle="modal" role="button">
                                       <%-- <asp:ImageButton ID="Img_Letter_Received_fromUpdate" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" OnClientClick="hipopUP()" />--%>

                                      <asp:ImageButton ID="abc" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" OnClientClick="hipopUP()" />
                                    </a>
                    
                    
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList runat="server" ID="ddlfreshletterRecFrom" CssClass="form-control">
                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                Letter Received Date&nbsp;
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtFreshletterRecDate" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender24" TargetControlID="txtFreshletterRecDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                               Date of Receipt 

                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtFreashLetterdateofrecipt" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender25" TargetControlID="txtFreashLetterdateofrecipt" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                            </div>
                            <div class="col-md-2">Reply by(date)

                            </div>
                            <div class="col-md-4">
                                 <asp:TextBox ID="TxtfreshLetterreplydate" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender26" TargetControlID="TxtfreshLetterreplydate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                            </div>
                        </div>
                    <div class="row">
                         <div class="col-md-2">
                               Mode of Recepit
                            </div>
                            
                            <div class="col-md-4">
                                 <asp:TextBox ID="txtfreshLetterModeOfRecipt" CssClass="form-control" autocomplete= "off"  runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                               Copy of Letter
                            </div>
                            <div class="col-md-4">
                                <div ">
                    <asp:FileUpload runat="server" ID="FUPFreshLetter" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="fupCopyof_Fresh_Letter"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>
                            </div>
                        </div>
                         <div  class="row" >
                             <div class="col-md-2">
                               Subject
                            </div>
                            <div class="col-md-6">
                                 <asp:TextBox ID="txtGridFreshLetterSubject" CssClass="form-control"   TextMode="MultiLine" autocomplete= "off"  runat="server"></asp:TextBox>
                            </div>
                </div>
               

                </div>
                <asp:Button ID="btnFreshletterUpdate" runat="server" Text="Update" CausesValidation="false" CssClass="btn btn-default" OnClick="btnFreshletterUpdate_Click" ValidationGroup="ValLCT"/>
                <asp:Button ID="btnfreshlettercancel" runat="server" Text="Cancel" CssClass="btn btn-default" ValidationGroup="ValLCT" OnClick="btnfreshlettercancel_Click" CausesValidation="false"/>
                <div class="modal-footer">
                    
                </div>
            </div>
        </div>
         </div>
     <div class="modal small fade" id="myModalBG" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 650px">
            <div class="modal-content">
                <div class="modal-header">
                  
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                     <center>  <h5 id="myModalBGDocument">Add New Banck Guarantee Type</h5></center>
                    </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-2">
                            BG Type
                            <asp:RequiredFieldValidator  ID="RequiredFieldValidator22" runat="server" ErrorMessage="*" ControlToValidate="txtAddBGtype" CssClass="Validation_Text" ValidationGroup="ValBGType" ></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txtAddBGtype"  runat="server"  MaxLength="100" autocomplete= "off" CssClass="form-control"></asp:TextBox>
                          
                        </div>
                    </div>
                   
                    <div class="row">
                        <center>
                            <cc1:Grid ID="Grid_BGType" CallbackMode="true"   AllowPageSizeSelection="false" runat="server" OnDeleteCommand="GridBGType_DeleteCommand"  FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="false" AllowPaging="true" PageSize="5">
                                <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                <Columns>
                                    <cc1:Column DataField="BG_Type" HeaderText="BG Type" Width="200px"></cc1:Column>
                                    <cc1:Column DataField="ID" HeaderText="BG Type"  Visible="false" Width="200px"></cc1:Column>
                                    <cc1:Column HeaderText="Edit"  Visible="false" Width="100px">
                                        <TemplateSettings TemplateId="EditBGType" />
                                    </cc1:Column>
                                    <cc1:Column AllowDelete="true" HeaderText="Delete"></cc1:Column>
                                </Columns>
                                <Templates>
                                    <cc1:GridTemplate runat="server" ID="EditBGType">
                                        <Template>
                                            <asp:LinkButton ID="lnkBtnEditBGType" CausesValidation="false" CommandName='<%# Container.DataItem["ID"] %>' OnClick="lnkBtnEditBGType_Click" Text="Edit" CssClass="gridCB" runat="server"></asp:LinkButton>
                                        </Template>
                                    </cc1:GridTemplate>
                                </Templates>
                            </cc1:Grid>
                        </center>
                    </div>
                     <div class="row">
                           <center>
                         <asp:Button ID="btnSaveBGType" runat="server" Text="Save" CssClass="btn btn-default" ValidationGroup="ValBGType" onclick="btnSaveBGType_Click"  />
                        <asp:Button ID="btnCancelBGType" runat="server" Text="Cancel"  CssClass="btn btn-default" ValidationGroup="ValBGType" CausesValidation="false" onclick="btnCancelBGType_Click"   />
                               </center>
                    </div>
                </div>
        </div>
    </div>
    </div>

       <div class="modal small fade" id="myModalInsuranceType" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 650px">
            <div class="modal-content">
                <div class="modal-header">
                  
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                     <center>  <h5 id="myModalInsuranceTypeDoc">Add New Insurance Type</h5></center>
                    </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-2">
                            Insurance Type
                            <asp:RequiredFieldValidator  ID="RequiredFieldValidator20" runat="server" ErrorMessage="*" ControlToValidate="txtAddInsuranceType" CssClass="Validation_Text" ValidationGroup="ValtxtInsuranceType" ></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txtAddInsuranceType"  runat="server"  MaxLength="100" autocomplete= "off" CssClass="form-control"></asp:TextBox>
                          
                        </div>
                    </div>
                   
                    <div class="row">
                        <center>
                            <cc1:Grid ID="Grid_InsuranceType" CallbackMode="true"   AllowPageSizeSelection="false" runat="server" OnDeleteCommand="GridInsuranceType_DeleteCommand"  FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="false" AllowPaging="true" PageSize="5">
                                <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                <Columns>
                                    <cc1:Column DataField="Insurance_Type" HeaderText="Insurance Type" Width="200px"></cc1:Column>
                                    <cc1:Column DataField="ID" HeaderText="ID"  Visible="false" Width="200px"></cc1:Column>
                                    <cc1:Column HeaderText="Edit" Visible="false" Width="100px">
                                        <TemplateSettings TemplateId="EditBGType" />
                                    </cc1:Column>
                                    <cc1:Column AllowDelete="true" HeaderText="Delete"></cc1:Column>
                                </Columns>
                                <Templates>
                                    <cc1:GridTemplate runat="server" ID="GridTemplate1">
                                        <Template>
                                            <asp:LinkButton ID="lnkBtnEditInsuranceType" CausesValidation="false" CommandName='<%# Container.DataItem["ID"] %>' OnClick="lnkBtnEditInsuranceType_Click" Text="Edit" CssClass="gridCB" runat="server"></asp:LinkButton>
                                        </Template>
                                    </cc1:GridTemplate>
                                </Templates>
                            </cc1:Grid>
                        </center>
                    </div>
                     <div class="row">
                           <center>
                         <asp:Button ID="btnSaveInsuranceType" runat="server" Text="Save" CssClass="btn btn-default" ValidationGroup="ValtxtInsuranceType" onclick="btnSaveInsuranceType_Click"  />
                        <asp:Button ID="btnCancelInsuranceType" runat="server" Text="Cancel"  CssClass="btn btn-default" ValidationGroup="ValtxtInsuranceType" CausesValidation="false" onclick="btnCancelInsuranceType_Click"   />
                               </center>
                    </div>
                </div>
        </div>
    </div>
    </div>

     <asp:Button runat="server" ID="btnAddItem1" Style="display: none"></asp:Button>
    <ajaxToolkit:ModalPopupExtender ID="ModalLetterItem" runat="server" PopupControlID="PanelWOItem" TargetControlID="btnAddItem1"
        CancelControlID="BtnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    
    <asp:Panel ID="PanelWOItem" runat="server" align="center" Style="display: none">
        <div class="modal-dialog" style="width:650px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="BtnClose" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center><h5 id="myModalamt1"><asp:Label ID="lblTax" runat="server" Text="Letter Received from Department"></asp:Label></h5></center>
                </div>
                <div class="modal-body">
                     <div class="row">
                          <div class="col-md-2" style="text-align:right">
                               
                            </div>
                          <div class="col-md-8">
                             <span style="font-weight:300; font-size:20px; padding-left:10px">Letter Received from Department</span>
                              </div>
                      
                         </div>
                     <div class="row">
                          <div class="col-md-5" style="text-align:right">
                               
                            </div>
                          <div class="col-md-4">
                              <asp:RadioButtonList ID="rbLetRecFrom_DeptUpdate" runat="server" >
                                  <asp:ListItem Text="Fresh Letter " Value="FreshLetter" />
                                         <%-- <asp:ListItem Text="Reply To Letter" Value="ReplayToletter" />--%>
                                    </asp:RadioButtonList>
                              </div>
                      
                         </div>
                    <hr />
              
                    <div class="row">
                            <div class="col-md-2">
                                Letter ID&nbsp;
                            </div>
                            <div class="col-md-4"  runat="server">
                            <asp:TextBox ID="txtLetterID" Enabled="false"  autocomplete= "off" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                </div>
                            <div class="col-md-2">
                                Letter Reference Number&nbsp;
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtLetterRefNo" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                Letter Received from&nbsp;
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtLetterRecFrom" CssClass="form-control" MaxLength="500" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                Letter Received Date&nbsp;
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtLetterRecDate" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender18" TargetControlID="txtLetterRecDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                               Date of Receipt 

                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtDateofRecipt" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender22" TargetControlID="txtDateofRecipt" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                            </div>
                            <div class="col-md-2">Reply by(date)

                            </div>
                            <div class="col-md-4">
                                 <asp:TextBox ID="txtReplyBydateModal" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender23" TargetControlID="txtReplyBydateModal" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                            </div>
                        </div>
                    <div class="row">
                         <div class="col-md-2">
                               Mode of Recepit
                            </div>
                            
                            <div class="col-md-4">
                                 <asp:TextBox ID="txtModeOfRecipt" CssClass="form-control" autocomplete= "off"  runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                               Copy of Letter
                            </div>
                            <div class="col-md-4">
                                <div ">
                    <asp:FileUpload runat="server" ID="FupCopyOfletter" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="fupCopyof_Fresh_Letter"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>
                            </div>
                        </div>
                         <div  class="row" >
                             <div class="col-md-2">
                               Subject
                            </div>
                            <div class="col-md-6">
                                 <asp:TextBox ID="txtSubject" CssClass="form-control"   TextMode="MultiLine" autocomplete= "off"  runat="server"></asp:TextBox>
                            </div>
                </div>
               

                </div>
                <asp:Button ID="BtnUpdateLetter" runat="server" Text="Update" CausesValidation="false" CssClass="btn btn-default" ValidationGroup="ValLCT" OnClick="BtnUpdateLetter_Click"/>
                <asp:Button ID="BtnCancelFreshLetter" runat="server" Text="Cancel" CssClass="btn btn-default" ValidationGroup="ValLCT" OnClick="BtnCancelFreshLetter_Click" CausesValidation="false"/>
                <div class="modal-footer">
                    
                </div>
            </div>
        </div>

    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="ModalFreshLetter" runat="server" PopupControlID="PanelFreshLetter" TargetControlID="btnAddItem1"
        CancelControlID="BtnCloseFreshLetter" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    
   <%-- <asp:Panel ID="" runat="server" align="center" Style="display: none;z-index:1000;">
        

    </asp:Panel>--%>

     
     <ajaxToolkit:ModalPopupExtender ID="ModalReplyToLetter" runat="server" PopupControlID="PanelreplyToLetter" TargetControlID="btnAddItem1"
        CancelControlID="BtnCloseReplyToletter" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    
    <asp:Panel ID="PanelreplyToLetter" runat="server" align="center" Style="display: none;z-index:1000;">
        <div class="modal-dialog" style="width:650px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="BtnCloseReplyToletter" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center><h5 id="myModalreplyToLetter"><asp:Label ID="lblReplyToLetter" runat="server" Text="Letter Correspondence to Department"></asp:Label></h5></center>
                </div>
                <div class="modal-body">
                     <div class="row">
                          <div class="col-md-2" style="text-align:right">
                               
                            </div>
                          <div class="col-md-8">
                             <span style="font-weight:300; font-size:20px; padding-left:10px">Letter Correspondence to Department</span>
                              </div>
                      
                         </div>
                     <div class="row">
                          <div class="col-md-5" style="text-align:right">
                               
                            </div>
                          <div class="col-md-4">
                              <asp:RadioButtonList ID="rblreplyToLetter" runat="server" >
                                  <%--<asp:ListItem Text="Fresh Letter " Value="FreshLetter" Enabled="true" />--%>
                                          <asp:ListItem Text="Reply To Letter" Value="ReplayToletter" />
                                    </asp:RadioButtonList>
                              </div>
                      
                         </div>
                    <hr />
            

              <%--<div id="ReplaytoLetterModal_Div">--%>
                          <div class="row">
                            <div class="col-md-2">
                                Letter ID&nbsp;
                            </div>
                            <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlReplyToletterId" CssClass="form-control" Enabled="false" >
                                    
                                </asp:DropDownList>
                                </div>
                           
                          <div class="col-md-2">
                             Cor To Dep Letter ID&nbsp;
                            </div>
                            <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlReplyToletterCorToDept" CssClass="form-control">
                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                </div>
                        </div>
                        <div class="row">
                              <div class="col-md-2">
                               Letter sent to&nbsp;
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtReplyToletterLettersentto" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                               Letter Reference Number UGCL&nbsp;
                       </div>
                            <div class="col-md-4">
                               <asp:TextBox ID="txtReplyToletterReferenceNo" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                            </div>
                           
                        </div>
                        <div class="row">
                             <div class="col-md-2">
                                Date&nbsp;
                    
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtReplyToletterDate" autocomplete= "off" onkeypress="javascript:return false;" onpaste="javascript:return false;" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender27" TargetControlID="txtReplyToletterDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                            </div>
                            <div class="col-md-2">
                              Mode of Dispatch

                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtReplyToletterMOD" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                            </div>
                           
                        </div>
                        <div  class="row" >
                             <div class="col-md-2">
                               Subject
                            </div>
                            <div class="col-md-6">
                                 <asp:TextBox ID="txtReplyToletterSubject" CssClass="form-control"   TextMode="MultiLine" autocomplete= "off"  runat="server"></asp:TextBox>
                            </div>
                               </div>
                        <div class="row">
                             <div class="col-md-2">
                                Cc1&nbsp;
                            </div>
                            <div class="col-md-2">
                              <asp:TextBox ID="txtReplyToletterCC1" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>  
                            </div>
                            <div class="col-md-2">
                              Cc2
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtReplyToletterCC2" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                            </div>
                       <div class="col-md-2">
                              Cc3
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtReplyToletterCC3" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                            </div>                          
                        </div>
                        <div class="row">
                             <div class="col-md-2">
                                Cc4&nbsp;
                            </div>
                            <div class="col-md-2">
                              <asp:TextBox ID="txtReplyToletterCC4" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>  
                            </div>
                            <div class="col-md-2">
                              Cc5
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtReplyToletterCC5" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                            </div>
                           </div>
                    <div class="row">
                         <div class="col-md-2">
                                Letter Copy (Upload)
                            </div>
                            <div class="col-md-4">
                                
                    <asp:FileUpload runat="server" ID="FUPReplyToletterCopy" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator22" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="fupLetterCopy_RTL"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>  
                         <div class="col-md-2">
                             Acknowledgement Copy (Upload)
                            </div>
                          <div class="col-md-4">
                                
                    <asp:FileUpload runat="server" ID="FUPReplyToletterAcknowledgementCopy" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator23" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="fupAcknowledgementCopy_RTL"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>  
                        </div>
                         
                <asp:Button ID="btnReplyToletterUpdate" runat="server" Text="Update" CausesValidation="false" CssClass="btn btn-default" OnClick="btnReplyToletterUpdate_Click"  ValidationGroup="ValLCT"/>
                <asp:Button ID="btnReplyToletterCancel" runat="server" Text="Cancel" CssClass="btn btn-default" ValidationGroup="ValLCT" OnClick="btnReplyToletterCancel_Click" CausesValidation="false" />
                <div class="modal-footer">
                    
                </div>
            
        </div>
                </div>
            </div>

    </asp:Panel>

    
   <%-- <ajaxToolkit:ModalPopupExtender ID="ModelLetterDept_Nameupdate" runat="server" PopupControlID="PanelLetter_Received_fromUpdate" TargetControlID="Img_Letter_Received_fromUpdate"
        CancelControlID="btnCloseFreshLetter" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelLetter_Received_fromUpdate" Visible="true" runat="server" align="center" Style="z-index:1000;">
        <div class="modal-dialog">
           
        </div>
    </asp:Panel>--%>
</asp:Content>
