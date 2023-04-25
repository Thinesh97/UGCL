<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="Material.aspx.cs" Inherits="Material" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {

            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
        });


    </script>
    <script type="text/javascript">
        function exportgrid() {
            Grid_Material.exportToExcel();
        }
        function beforedelete() {
            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                return false;
            }
            return true;
        }


    </script>

    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>

    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Material 

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->



            <div class="row">

                <div class="col-md-2">
                    Budget Sector &nbsp;
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValMaterial" InitialValue="-Select-" ControlToValidate="ddlBudgetSector"></asp:RequiredFieldValidator>

                    <asp:ImageButton ID="ImgBtnSector" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />

                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlBudgetSector" runat="server" OnSelectedIndexChanged="ddlBudgetSector_SelectedIndexChanged" AutoPostBack="true"  CssClass="form-control" TabIndex="1">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Category Name &nbsp;
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValMaterial" InitialValue="-Select-" ControlToValidate="ddlCategoryName"></asp:RequiredFieldValidator>
                  
                        <asp:ImageButton ID="ImgBtnCategory" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                  
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlCategoryName" runat="server" CssClass="form-control" TabIndex="2">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
              


            </div>
            <div class="row">
                 <div class="col-md-2">
                    Item Name&nbsp;
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValMaterial" ControlToValidate="txtItemName"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtItemName" CssClass="form-control"  runat="server" TextMode="MultiLine" style="resize:none" TabIndex="3"></asp:TextBox>

                </div>
               
                <div class="col-md-2">
                    UOM&nbsp;
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValMaterial" InitialValue="-Select-" ControlToValidate="ddlUOM"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlUOM" runat="server" CssClass="form-control" TabIndex="4">
                    </asp:DropDownList>

                </div>              
               
            </div>
            <div class="row">
             <asp:PlaceHolder ID="phItemCode" runat="server" Visible="false">
                  <div class="col-md-2">
                    Item Code&nbsp;
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtItemCode" MaxLength="100" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                    </asp:PlaceHolder>

                </div>
            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="ValMaterial" OnClick="btnSave_Click" CssClass="btn btn-default" TabIndex="6"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Clear" OnClick="btnCancel_Click" CssClass="btn btn-default" TabIndex="7"></asp:Button>

                </div>

            </div>
            <br />

            <center>
        <ogrid:Grid runat="server" ID="Grid_Material" CallbackMode="false" AutoGenerateColumns="false" OnRowDataBound="Grid_Material_RowDataBound" OnDeleteCommand="Grid_Material_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowPaging="true" PageSize="10"  AllowAddingRecords="false" >
          <ScrollingSettings ScrollWidth="84%"  />
         <ClientSideEvents OnBeforeClientDelete="beforedelete" />
            <ExportingSettings ExportAllPages="true" ExportTemplates="true" ColumnsToExport="Item_Code,Sector_Name,Category_Name,Item_Name,UOMPrefix" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <Columns> 
                 <ogrid:Column DataField="Item_Code"  HeaderText="Item Code" Width="130px"  >
                                           <TemplateSettings TemplateId="MaterailTemplate"/>
                                       </ogrid:Column>            
                <ogrid:Column DataField="Sector_Name" HeaderText="Budget Sector" ></ogrid:Column>              
                <ogrid:Column DataField="Category_Name" HeaderText="Category Name" ></ogrid:Column>
                 <ogrid:Column DataField="Item_Name" HeaderText="Item Name" Wrap="true" ></ogrid:Column>
                 <ogrid:Column DataField="UOMPrefix" HeaderText="UOM"  Width="80px" ></ogrid:Column>
           
                       <ogrid:Column AllowDelete="true" HeaderText="Delete" Width="80px"></ogrid:Column>                         
            </Columns>
             <Templates>
                    <ogrid:GridTemplate runat="server" ID="MaterailTemplate">
                        <Template>
                           
                            <asp:LinkButton ID="lnkBtnItemcode" OnClick="lnkBtnItemcode_Click" Text='<%# Container.DataItem["Item_Code"] %>'
                              CssClass="gridCB" runat="server"></asp:LinkButton>
         
                        </Template>
                    </ogrid:GridTemplate>
                    </Templates>
        </ogrid:Grid>
                   <br />     
                    <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click" TabIndex="8"></asp:Button>    
                      <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" tabindex="9" />    
                      </center>
        </div>
    </div>


    <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->


    <ajaxToolkit:ModalPopupExtender ID="ModelCategoryPopup" runat="server" PopupControlID="PanelCategory" TargetControlID="ImgBtnCategory"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="PanelCategory" runat="server" align="center" Style="display: none" DefaultButton="btnSaveCategory">

        <div class="modal-dialog">

            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnClose" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>  <h5 id="myModalLabelcrate">Category</h5></center>
                </div>
               <asp:UpdatePanel ID="uppi2" runat="server">
                   <ContentTemplate>

                 
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-3">
                            Budget Sector&nbsp;
          <asp:RequiredFieldValidator ID="rfvbs" runat="server" ErrorMessage="*" InitialValue="-Select-" ControlToValidate="ddlCategoryBudgetSector" CssClass="Validation_Text" ValidationGroup="ValCategory"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-5">
                          <asp:DropDownList ID="ddlCategoryBudgetSector"  runat="server" CssClass="form-control">
                                  <asp:ListItem Text="-Select-" Value="-Select-"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-3">
                            Category Name&nbsp;
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtCategoryName" CssClass="Validation_Text" ValidationGroup="ValCategory"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txtCategoryName" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-3">
                            Prefix&nbsp;
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtCatPrefix" CssClass="Validation_Text" ValidationGroup="ValCategory"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txtCatPrefix" runat="server" MaxLength="5" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">

                        <center>
                               <ogrid:Grid ID="Grid_Category"  runat="server"  CallbackMode="false" AllowPageSizeSelection="false" OnDeleteCommand="Grid_Category_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="5">
                                <ScrollingSettings ScrollWidth="95%" />
                                    <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                <Columns>
                                         <ogrid:Column HeaderText="Budget Sector" DataField="Sector_Name" Wrap="true"></ogrid:Column> 
                                     <ogrid:Column HeaderText="Category Name" DataField="Category_Name" Wrap="true"></ogrid:Column> 
                                     <ogrid:Column DataField="Cat_prefix" HeaderText="Prefix"></ogrid:Column>
                                     <ogrid:Column DataField="Mat_cat_ID"  HeaderText="Edit" Width="110px">
                                           <TemplateSettings TemplateId="CategoryTemplate" />
                                       </ogrid:Column>
                                    
                                     <ogrid:Column AllowDelete="true" HeaderText="Delete"></ogrid:Column>
                                </Columns>
                                   <Templates>
                                        <ogrid:GridTemplate runat="server" ID="CategoryTemplate">
                                            <Template>
                                                <asp:LinkButton ID="lnkBtnCategoryID" CausesValidation="false" CommandName='<%# Container.DataItem["Mat_cat_ID"] %>' OnClick="lnkBtnCategoryID_Click" Text="Edit" CssClass="gridCB" runat="server"></asp:LinkButton>
                                            </Template>
                                        </ogrid:GridTemplate>
                                   </Templates>
                            </ogrid:Grid>
                              </center>


                    </div>
                </div>
                      
                <div class="modal-footer">
                    <center>
           <asp:Button ID="btnSaveCategory" runat="server" Text="Save"   CssClass="btn btn-default" ValidationGroup="ValCategory" OnClick="btnSaveCategory_Click"   />
                        <asp:Button ID="btnCancelCategory" runat="server" OnClick="btnCancelCategory_Click" Text="Cancel"  CssClass="btn btn-default" ValidationGroup="ValCategory" CausesValidation="false"  />
                 </center>
                       </ContentTemplate>
                  <Triggers>
                       <asp:AsyncPostBackTrigger ControlID="btnCancelCategory"  />
                   <%--    <asp:AsyncPostBackTrigger ControlID="btnSaveCategory" EventName="Click" />--%>
                       <asp:PostBackTrigger ControlID="btnSaveCategory" />
                   </Triggers>
               </asp:UpdatePanel>

                </div>
                                

            </div>

        </div>
    </asp:Panel>


       <ajaxToolkit:ModalPopupExtender ID="mpeBudgetSector" runat="server" PopupControlID="PanelBudgetSector" TargetControlID="ImgBtnSector"
        CancelControlID="btnCloseSector" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>

<asp:Panel ID="PanelBudgetSector" runat="server" align="center" Style="display: none" DefaultButton="btnBudgetSector">

        <div class="modal-dialog">

            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnCloseSector" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>  <h5 id="myModalLabelSector">Budget Sector</h5></center>
                </div>
                <%--    <asp:UpdatePanel ID="uppi1" runat="server">
                            <ContentTemplate>--%>
                <div class="modal-body">



                    <div class="row">



                        <div class="col-md-2"></div>
                        <div class="col-md-3">
                            Budget Sector&nbsp;
          <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" InitialValue="-Select-" ControlToValidate="ddlSector" CssClass="Validation_Text" ValidationGroup="ValSector"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList ID="ddlSector"  CssClass="form-control" runat="server">
                                <asp:ListItem Text="-Select-" Value="-Select-"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-3">
                            Prefix&nbsp;
          <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ControlToValidate="txtBudgetPrefix" CssClass="Validation_Text" ValidationGroup="ValSector"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txtBudgetPrefix" runat="server" MaxLength="5" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">

                        <center>
                               <ogrid:Grid ID="Grid_Sector"  runat="server"  CallbackMode="true" AllowPageSizeSelection="false"  FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="5">
                                <ScrollingSettings />
                                    <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                <Columns>
                                       
                                     <ogrid:Column HeaderText="Budget Sector" DataField="Sector_Name" Width="180" Wrap="true">
                                         <TemplateSettings TemplateId="BudgetSectorTemplate" />
                                     </ogrid:Column> 
                                     <ogrid:Column DataField="Sector_Prefix" Width="100" HeaderText="Prefix">
                                         <TemplateSettings  TemplateId="BudgetSectorPrefix" />
                                     </ogrid:Column>
                                   <ogrid:Column DataField="Budget_Sector_ID" HeaderText="Sector ID" Visible="false"></ogrid:Column>
                                    
                                  
                                </Columns>
                                <Templates>
                                    <ogrid:GridTemplate ID="BudgetSectorTemplate" runat="server">
                                        <Template>
                                          <asp:LinkButton ID="lnkBudgetSector" runat="server" OnClick="lnkBudgetSector_Click" CommandName='<%#Container.DataItem["Sector_Prefix"] %>' CommandArgument='<%# Container.DataItem["Budget_Sector_ID"] %>' Text='<%#Container.DataItem["Sector_Name"] %>'></asp:LinkButton>
                                        </Template>
                                    </ogrid:GridTemplate>
                                    <ogrid:GridTemplate ID="BudgetSectorPrefix" runat="server">
                                        <Template>
                                            <asp:Label ID="lblSectorPrefix" runat="server" Text='<%#Container.DataItem["Sector_Prefix"] %>'></asp:Label>
                                        </Template>
                                    </ogrid:GridTemplate>
                                </Templates>
                            </ogrid:Grid>
                              </center>


                    </div>
                </div>

                <div class="modal-footer">
                    <center>
           <asp:Button ID="btnBudgetSector" runat="server" Text="Save"  OnClick="btnBudgetSector_Click" CssClass="btn btn-default" ValidationGroup="ValSector"  />
                        <asp:Button ID="btnCancelBudgetSector" runat="server"  OnClick="btnCancelBudgetSector_Click" Text="Cancel"  CssClass="btn btn-default" ValidationGroup="ValSector" CausesValidation="false"  />
                 </center>

                </div>
                            

            </div>

        </div>
    </asp:Panel>

</asp:Content>
