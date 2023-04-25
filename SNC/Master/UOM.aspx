<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Master/UOM.aspx.cs" Inherits="UOMPage" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="updatepanelUOM" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                function beforedelete() {
                    if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                        return false;
                    }
                    return true;
                }
            </script>
       
    <div class="panel panel-default">
       <div class="panel-heading" >
       
    <h3 class="panel-title" >
        <i class="glyphicon glyphicon-envelope" ></i>

      UOM

    </h3>

</div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

             <div class="row">
                                <div class="col-md-3"></div>
                                <div class="col-md-2">UOM Name&nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" CssClass="Validation_Text" ControlToValidate="txtUOMName" ValidationGroup="ValUOM"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtUOMName" MaxLength="30" CssClass="form-control" runat="server" TabIndex="1"></asp:TextBox><br/>

                                </div>
                            </div>
             <div class="row">
                                <div class="col-md-3"></div>
                                <div class="col-md-2">UOM Prefix&nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="Validation_Text" ControlToValidate="txtUOMPrefix" ValidationGroup="ValUOM"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtUOMPrefix" MaxLength="5" CssClass="form-control" runat="server" TabIndex="2"></asp:TextBox><br/>

                                </div>
                 

                            </div>
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="ValUOM" OnClick="btnSave_Click" CssClass="btn btn-default" TabIndex="3"></asp:Button>
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btnCancel_Click" TabIndex="4"></asp:Button>

                                </div>

                            </div>
                            <br />
                            <center>
        <ogrid:Grid runat="server" ID="Grid_UOM"  AutoGenerateColumns="false"  CallbackMode="false" GenerateRecordIds="true" OnDeleteCommand="Grid_UOM_DeleteCommand"  FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true"  AllowPaging="true" PageSize="10"  AllowAddingRecords="false">
            <ScrollingSettings />
            <ClientSideEvents OnBeforeClientDelete="beforedelete" />
            <Columns> 
                  <ogrid:Column HeaderText="UOM ID" DataField="UOM_ID" Wrap="true" Width="150px" Visible="false">
                            <TemplateSettings TemplateId="UOMTemplate" />
                        </ogrid:Column>            
              
                <ogrid:Column DataField="UOM" HeaderText="UOM Name"  Width="250px"></ogrid:Column>
                <ogrid:Column DataField="UOMPrefix" HeaderText="UOM Prefix" Width="180px"  ></ogrid:Column>
              
                <ogrid:Column AllowDelete="true" HeaderText="Delete" Width="100px"></ogrid:Column>
                                 
            </Columns>
             <Templates>
                        <ogrid:GridTemplate runat="server" ID="UOMTemplate">
                            <Template>
                                <asp:LinkButton ID="lnkBtnUOMID"  OnClick="lnkBtnUOMID_Click"   Text='<%# Container.DataItem["UOM_ID"] %>'
                                    CssClass="gridCB" runat="server"></asp:LinkButton>
                            </Template>
                        </ogrid:GridTemplate>

                    </Templates>
        </ogrid:Grid>
                     
                      </center>
                        </div>


             <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        </div>

             </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
