<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="DailyProgressReport.aspx.cs"  Inherits="SNC.SubContractorBills.DailyProgressReport" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <script type="text/javascript">
          $(document).ready(function () {
              $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
              $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
          });
          $(document).ready(function () {
              $('.chosen-select').chosen();
          });
         
          $(document).ready(function () {
              var CumulativeValue = document.getElementById("<%=hdntxtCumulativeProgress.ClientID%>").value;
              var PresentValue = document.getElementById("<%=txtPresent_Progress.ClientID%>").value;
              var added = parseInt(CumulativeValue) + parseInt(PresentValue);
              document.getElementById("<%=txtCumulativeProgress .ClientID%>").value = Math.round(added)
          });
          function calcOutput(obj) {
              debugger;
              var txtStart = document.getElementById("<%=txtStartKm.ClientID%>").value; 
              var txtEnd = document.getElementById("<%=txtEndKM.ClientID%>").value; 

              if (txtStart != "" && txtEnd != "") {
                  document.getElementById("<%=txtOutput .ClientID%>").value = Math.round(txtEnd - txtStart);
              }
              else {
                
                  document.getElementById("<%=txtOutput .ClientID%>").val("0.00");
              }
          }

         

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
                debugger;
                var CumulativeValue = document.getElementById("<%=hdntxtCumulativeProgress.ClientID%>").value;
                var PresentValue = document.getElementById("<%=txtPresent_Progress.ClientID%>").value;
                var added = parseInt(CumulativeValue) + parseInt(PresentValue);
                document.getElementById("<%=txtCumulativeProgress .ClientID%>").value = Math.round(added)
            }
        </script>

     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
  

    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

               Daily Progress Report

            </h3>
        </div>
        <div class="panel-body">
            <!------------------------------------------------------------Body Content-------------------------------------------------------------------->
       <div class="row">
             <div class="col-md-2">
                   DPR Number&nbsp;
                </div>
                <div class="col-md-4">
                     <asp:TextBox ID="txtDPRNo"  Enabled="false" runat="server"  autocomplete="off" CssClass="form-control"></asp:TextBox>
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
                 <div class="col-md-2">
                    Project Name&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" InitialValue="-Select-" ControlToValidate="ddlProject" CssClass="Validation_Text" ValidationGroup="ValWO" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlProject" class="chosen-select form-control" TabIndex="4"></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Location &nbsp;
                  <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="-Select-" ControlToValidate="ddlLocation" CssClass="Validation_Text" ValidationGroup="ValWO" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                </div>

                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlLocation" class="chosen-select form-control" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true" TabIndex="3">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>

             
            </div>
           
                <div class="row">
                <div class="col-md-2">
                      Work &nbsp;
                 <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="Validation_Text" ErrorMessage="*" InitialValue="-Select-" ControlToValidate="ddlWorkName" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                     </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlWorkName" class="chosen-select form-control" OnSelectedIndexChanged="ddlWorkName_SelectedIndexChanged" AutoPostBack="true" TabIndex="3">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
                   
                <div class="col-md-2">
                    Sub Work &nbsp;
                  <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="Validation_Text" ErrorMessage="*" InitialValue="-Select-" ControlToValidate="ddlSubWorkName" ValidationGroup="Save"></asp:RequiredFieldValidator>
                  --%> 
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlSubWorkName" class="chosen-select form-control" OnSelectedIndexChanged="ddlSubWorkName_SelectedIndexChanged" AutoPostBack="true" TabIndex="3">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
               
            </div>
            <div class="row">
                  <div runat="server">
                    <div class="col-md-2">Sub Contractor Name</div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="ddlSubContractor" class="chosen-select form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSubContractor_SelectedIndexChanged"  TabIndex="4"></asp:DropDownList>
                    </div>
                </div>
            </div>
            
            <div class="row">
                 <div class="col-md-2">
                   Select WO&nbsp;
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlWO" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlWO_SelectedIndexChanged" CssClass="form-control" TabIndex="3"></asp:DropDownList>
                </div>
                  <div class="col-md-2">
                   Work Description&nbsp;
                </div>
                <div class="col-md-4">
                     <asp:TextBox ID="txtWorkDescription" runat="server" TextMode="MultiLine" autocomplete="off" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <br />
            <br />
            <div class="col-md-12 text-center">
                 <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="ValMRN" CssClass="btn btn-default"  TabIndex="9"></asp:Button>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" TabIndex="10"></asp:Button>
               <asp:Button ID="btnAddItem" runat="server" Visible="false" Text="Add Item" CssClass="btn btn-default" CausesValidation="false" OnClick="btnAddItem_Click" TabIndex="15"></asp:Button>
              <asp:Button ID="btnConsumedMaterial" runat="server" Text="Add Consumed Material" CssClass="btn btn-default" TabIndex="12"></asp:Button>
              <asp:Button ID="btnAddSOW" runat="server" Visible="true" Text="Add Utilized Machinary" CssClass="btn btn-default" CausesValidation="false" OnClick="btnAddSOW_Click" TabIndex="15"></asp:Button>
              <asp:Button ID="btnUtilizedLabours" runat="server" Text="Add Utilized Labours" CssClass="btn btn-default" TabIndex="15"></asp:Button>
             <asp:Button ID="btnAssignSC" runat="server"  Visible="false" Text="Assign Work To Sub Contractor" CssClass="btn btn-default" CausesValidation="false" OnClick="btnAssignSC_Click" TabIndex="15"></asp:Button>
              
            </div>
            
            <br />
            <br />
           
            </div>

           <h4 style="text-align:center"> Daily Entry list</h4>
        <div id="Div_GridDPR" runat="server"> 
          <center>
                <ogrid:Grid runat="server" ID="Grid_DPR_Entry" CallbackMode="false" AutoGenerateColumns="false" 
                    OnRowDataBound="Grid_DPR_RowDataBound"  
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
                        <ogrid:Column DataField="ID" HeaderText="DPR No /Click to Edit" Width="100px" >
                             <TemplateSettings TemplateId="ItemTemplate" />
                        </ogrid:Column>
                        <ogrid:Column DataField="Date" HeaderText="Date" Width="100px" ></ogrid:Column>
                        <ogrid:Column DataField="Location_Chainage" HeaderText="Location / Chainage" Wrap="false"></ogrid:Column>
                        <ogrid:Column DataField="Work_Done_Activity" HeaderText="Work Done Activity" Width="180px" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Discription_Of_Work" HeaderText="Work Description " Wrap="true"   Width="200px"></ogrid:Column>
                        <ogrid:Column DataField="Total_Employes" HeaderText="No of Labors Utilized" Wrap="true"   Width="200px"></ogrid:Column>
                        <ogrid:Column DataField="Present_Progress" HeaderText="Present Progress" Align="center" Width="150px"></ogrid:Column>                 
                        <ogrid:Column DataField="UOM" HeaderText="UOM" Width="100px" ></ogrid:Column>
                        <ogrid:Column DataField="DPRFile_Path" HeaderText="DPRFile Path" Width="100px" ></ogrid:Column>
                        <ogrid:Column DataField="Remarks" HeaderText="Remarks" Width="100px" ></ogrid:Column>
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
        
     <div>
              <h4 style="text-align:center">Consumed Material List</h4>
                <center>
                <ogrid:Grid runat="server" ID="Grid_RequiredMaterial" CallbackMode="false" AutoGenerateColumns="false"   
                    FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="10" >
                    <ScrollingSettings ScrollWidth="100%"  ScrollHeight="400"/>
                      <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"
                         ColumnsToExport="WONo,WODate,Subcon_name,Type_Name,DurationOfWork,Status,Name,Prepared_By,Other_Terms,Remarks" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

                    <Columns>
                        <ogrid:Column DataField="ID" HeaderText="ID" Width="190px" >     </ogrid:Column>
                        <ogrid:Column DataField="Material_Name" HeaderText="Material Name" Width="250px" ></ogrid:Column>
                         <ogrid:Column DataField="Quantity" HeaderText="Quantity" Width="200px" ></ogrid:Column>
                         <ogrid:Column DataField="UOM_ID" HeaderText="UOM" Width="200px" ></ogrid:Column>
                         <ogrid:Column DataField="ConsumedMaterialDate" HeaderText="Date" Width="100px" ></ogrid:Column>
                    </Columns>
                    <Templates>
                      
                    </Templates>
                </ogrid:Grid>
                <br />
             
                      </center>
          </div>
                       <div>
              <h4 style="text-align:center">Utilized Machinery List</h4>
                <center>
                 <ogrid:Grid runat="server" ID="GridUtilizedMachinery" CallbackMode="false" AutoGenerateColumns="false"  
                    FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="10" >
                    <ScrollingSettings ScrollWidth="100%"  ScrollHeight="400"/>
                      <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"
                         ColumnsToExport="WONo,WODate,Subcon_name,Type_Name,DurationOfWork,Status,Name,Prepared_By,Other_Terms,Remarks" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

                    <Columns>
                        <ogrid:Column DataField="ID" HeaderText="ID" Width="190px" >
                        </ogrid:Column> 
                        <ogrid:Column DataField="Machinary_Name" HeaderText="Machinary Name" Width="100px" ></ogrid:Column>
                        <ogrid:Column DataField="Reg_Number" HeaderText="Reg Number" Width="100px" ></ogrid:Column>
                         <ogrid:Column DataField="Unit" HeaderText="Unit" Width="200px" ></ogrid:Column>
                         <ogrid:Column DataField="StartKM" HeaderText="Start KM" Width="250px" ></ogrid:Column> 
                         <ogrid:Column DataField="EndKM" HeaderText="End KM" Width="100px" ></ogrid:Column> 
                        <ogrid:Column DataField="UOM" HeaderText="UOM" Width="150px" ></ogrid:Column> 
                          <ogrid:Column DataField="OutPut" HeaderText="Out Put" Width="150px" ></ogrid:Column> 
                          <ogrid:Column DataField="Issued_Diesel" HeaderText="Issued Diesel" Width="150px" ></ogrid:Column> 
                          <ogrid:Column DataField="UtilizedMachinary_File" HeaderText="Date" Width="100px" ></ogrid:Column>
                          <ogrid:Column DataField="SiteImage" HeaderText="View Image" Width="150px" ></ogrid:Column> 
                          
                    </Columns>
                    <Templates>
                        <ogrid:GridTemplate ID="GridTemplate1" runat="server">
                            <Template>
                               <asp:HyperLink ID="lnk_ID" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["ID"] %>'>
                        </asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
                </ogrid:Grid>
                <br />
             
                      </center>
          </div>
                      <div>
              <h4 style="text-align:center">Utilized Labours List</h4>
                <center>
                 <ogrid:Grid runat="server" ID="GridRequiredLabours" CallbackMode="false" AutoGenerateColumns="false"  
                    FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="10" >
                    <ScrollingSettings ScrollWidth="100%"  ScrollHeight="400"/>
                      <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"
                         ColumnsToExport="WONo,WODate,Subcon_name,Type_Name,DurationOfWork,Status,Name,Prepared_By,Other_Terms,Remarks" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

                    <Columns>
                        <ogrid:Column DataField="ID" HeaderText="ID" Width="190px" >
                        </ogrid:Column>
                        <ogrid:Column DataField="Labour_Type" HeaderText="Labour Type" Width="200px" ></ogrid:Column>
                         <ogrid:Column DataField="Quantity" HeaderText="Quantity" Width="100px" ></ogrid:Column>
                         <ogrid:Column DataField="UOM" HeaderText="UOM" Width="100px" ></ogrid:Column>
                          <ogrid:Column DataField="UtilizedLabourDate" HeaderText="Date" Width="200px" ></ogrid:Column>
                    </Columns>
                    <Templates>
                     
                    </Templates>
                </ogrid:Grid>
                <br />
             
                      </center>
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
                    <center><h5 id="myModalamt1"><asp:Label ID="lblTax" runat="server" Text="Add Daily  Report"></asp:Label></h5></center>
                </div>
                <div class="modal-body">
                    <div class="row">
                         <div class="col-md-4">
                           Date&nbsp;
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtDPRDate" runat="server" onkeyup="CalculateItemTaxAmt()" autocomplete="off" CssClass="form-control"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender runat="server" ID="cal1" Format="dd-MM-yyyy" TargetControlID="txtDPRDate"></ajaxToolkit:CalendarExtender>
                            </div>
                    </div>
                            <div class="row">
                        <div class="col-md-4">
                           Location / Chainage&nbsp;
                        </div>
                        <div class="col-md-8">
                            <asp:HiddenField ID="hdnDPR_ID"  runat="server"/>
                            <asp:TextBox ID="txtLocation_Chainage" runat="server" autocomplete="off"  CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                           Work Done Activity
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtWork_Done_Activity" CssClass="Validation_Text" ValidationGroup="ValItem" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtWork_Done_Activity" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            Discription of Work&nbsp;
                             <asp:ImageButton  Visible="false" ID="ImgBtnDiscription_Of_Work" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                        </div>
                        <div class="col-md-8">
                            <asp:DropDownList runat="server" ID="ddlDiscriptionOf_Work" AutoPostBack="true" OnSelectedIndexChanged="ddlDiscriptionOf_Work_Changed" CssClass="form-control"></asp:DropDownList>
                        </div>
                        </div>
                        <div class="row">
                        <div class="col-md-4">
                            Present Progress&nbsp;
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtPresent_Progress" runat="server" onkeyup="CalculateCumulativeAmount()" autocomplete="off"  CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                         </div>
                        <div class="row">
                        <div class="col-md-4">
                            Cumulative Progress&nbsp;
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtCumulativeProgress" runat="server" autocomplete="off"  CssClass="form-control input-pos-float"></asp:TextBox>
                        <asp:HiddenField runat="server"  ID="hdntxtCumulativeProgress"/>
                        </div>
                         </div>
                    <div class="row">
                        <div class="col-md-4">
                           UOM&nbsp;
                        </div>
                        <div class="col-md-8">
                              <asp:DropDownList runat="server" Enabled="false" ID="ddlUOM"  CssClass="form-control"></asp:DropDownList>
                        </div>
                        
                   </div>
                    <div class="row">
                        <div class="col-md-4">
                            NMR ID&nbsp;
                            </div>
                        <div class="col-md-8">
                            <asp:DropDownList runat="server" ID="ddlNMR" AutoPostBack="true" OnSelectedIndexChanged="ddlNMR_SelectedIndexChanged"  CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                     <div class="row" id="Worker_Div" runat="server">
                         <div class="col-md-4">
                       <span><label for="time">Men -</label> <b> <asp:Label runat="server" id="No_Men_NMR"> </asp:Label> </b>  <asp:TextBox ID="txtNoMen" runat="server" autocomplete="off"  CssClass="form-control input-pos-float"></asp:TextBox></span>
    <span>:</span>
                             </div>
                         <div class="col-md-4">
    <span><label for="time">Women -</label><b>   <asp:Label runat="server" id="No_Women_NMR"> </asp:Label></b><asp:TextBox ID="txtNoWomen" runat="server" autocomplete="off"  CssClass="form-control input-pos-float"></asp:TextBox></span>
    <span>:</span>
                             </div>
                          <div class="col-md-4">
    <span><label for="time">Helper -</label><b>  <asp:Label runat="server" id="No_Helper_NMR"> </asp:Label> </b><asp:TextBox ID="txtNoHelper" runat="server" autocomplete="off"  CssClass="form-control input-pos-float"></asp:TextBox></span>
                         </div>
                         </div>
                     
                     <div class="row">
                        <div class="col-md-4">
                           Remarks
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtRemarks" CssClass="Validation_Text" ValidationGroup="ValItem" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                       <div class="row">
                          
                <div runat="server" id="div_BeforeUpload" >
                    <div class="col-md-4">Upload File</div>
                    <div class="col-md-4">
                        <asp:FileUpload runat="server" ID="fuDPRDoc" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationGroup="FileSave"
                            ErrorMessage="PDF File Only" ForeColor="Red" ControlToValidate="fuDPRDoc"
                            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf)$">  
                        </asp:RegularExpressionValidator>
                    </div>
                </div>
                <div runat="server" id="div_AfterUpload" visible="false">
                    <div class="col-md-2">Uploaded File</div>
                    <div class="col-md-4">
                        <%--<asp:LinkButton runat="server" ID="lnkDownloadFile" OnClick="lnkDownloadFile_Click"></asp:LinkButton>--%>
                    </div>
                </div>
            </div>
                </div>
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnSaveDPR_Entry" runat="server" Text="Save"  CssClass="btn btn-default" ValidationGroup="ValItem" OnClick="btnSaveDPR_Entry_Click" />
                        <asp:Button ID="btnCancelDPR_Entry" runat="server"  Text="Cancel"  CssClass="btn btn-default" CausesValidation="false" OnClick="btnCancelDPR_Entry_Click" />        
                 </center>

                </div>
            </div>
        </div>
        

     
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="ModelLedgerPopup" runat="server" PopupControlID="PanelPanelDiscription_Of_Work" TargetControlID="ImgBtnDiscription_Of_Work"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelPanelDiscription_Of_Work" runat="server" align="center" Style="display: none" DefaultButton="btnSaveLedger">

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnClose" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>
                        <h5 id="myModalLabelcrate">Discription of Work </h5>
                    </center>
                </div>
                <asp:UpdatePanel ID="uppi2" runat="server">
                    <ContentTemplate>


                        <div class="modal-body">

                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-3">
                                    Discription Of Work&nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtDiscription_Of_Work" CssClass="Validation_Text" ValidationGroup="ValLedger"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtDiscription_Of_Work" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">

                                <center>
                                    <ogrid:Grid ID="Grid_Discription_Of_Work" runat="server" CallbackMode="false" AllowPageSizeSelection="false" OnDeleteCommand="Grid_Discription_Of_Work_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowSorting="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="5">
                                        <%--<ScrollingSettings ScrollWidth="95%" />--%>
                                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                        <Columns>
                                            <ogrid:Column DataField="Discription_Of_Work" HeaderText="Discription Of Work" Wrap="true"></ogrid:Column>
                                            <ogrid:Column DataField="ID" Visible="false"></ogrid:Column>
                                            <ogrid:Column HeaderText="Delete" AllowDelete="true"></ogrid:Column>
                                        </Columns>
                                    </ogrid:Grid>
                                </center>

                            </div>
                        </div>

                        <div class="modal-footer">
                            <center>
                                <asp:Button ID="btnSaveLedger" runat="server" Text="Save" OnClick="btnSaveDiscription_Of_Work_Click" CssClass="btn btn-default" ValidationGroup="ValLedger" />
                                <asp:Button ID="btnCancelLedger" runat="server" Text="Cancel" OnClick="btnCancelDiscription_Of_Work_Click" CssClass="btn btn-default" ValidationGroup="ValLedger" CausesValidation="false" />
                            </center>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnCancelLedger" />
                        <%--<asp:AsyncPostBackTrigger ControlID="btnSaveLedger" EventName="Click" />--%>
                        <asp:PostBackTrigger ControlID="btnSaveLedger" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
        </div>
    </asp:Panel>

         

     <ajaxToolkit:ModalPopupExtender ID="ModalPopupConsumedMaterial" runat="server" PopupControlID="PanelConsumedMaterial" TargetControlID="btnConsumedMaterial"
        CancelControlID="btnConsumedMaterialClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    
       <asp:Panel ID="PanelConsumedMaterial" runat="server" align="center" Style="display: none" >
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnConsumedMaterialClose" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center><h5 id="myModalamtConsumedMaterialClose"><asp:Label ID="Label3" runat="server" Text="Add Consumed Material Details"></asp:Label></h5></center>
                </div>
               <div class="modal-body">
                    <div class="row">
                         <div class="col-md-4" style="text-align:right">
                    Date&nbsp;
                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtConsumedMaterialDate" CssClass="Validation_Text" ValidationGroup="ValRequired_Material" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtConsumedMaterialDate" autocomplete="off" onpaste="javascript:return false;" onkeypress="javascript:return false;" CssClass="form-control" TabIndex="2"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender3" Format="dd-MM-yyyy" TargetControlID="txtConsumedMaterialDate"></ajaxToolkit:CalendarExtender>
                </div>
                        </div>
                    <div class="row">
                        <div class="col-md-4" style="text-align:right">
                           Consumed Material Name&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="Validation_Text" ErrorMessage="*" InitialValue="-Select-" ControlToValidate="ddlConsumedMaterial" ValidationGroup="ValRequired_Material"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="ddlConsumedMaterial" class="form-control" OnSelectedIndexChanged="ddlConsumedMaterial_SelectedIndexChanged" AutoPostBack="true" TabIndex="3">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                        </div>
                    </div>
                      <div class="row">
                        <div class="col-md-4" style="text-align:right">
                           Quantity&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtRequired_MaterialQuantity"  CssClass="Validation_Text" ValidationGroup="ValRequired_Material" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtRequired_MaterialQuantity"   CssClass="form-control input-pos-float"  MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>
                      <div class="row">
                       <div class="col-md-4" style="text-align:right">
                            UOM
                        </div>
                        <div class="col-md-6">
                             <asp:DropDownList ID="ddlRequiredMaterialUOM" CssClass="form-control" runat="server"  TabIndex="8">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnSaveRequired_Material" runat="server" Text="Save" OnClick="btnSaveRequired_Material_Click"  CssClass="btn btn-default" ValidationGroup="ValRequired_Material"  />
                       <%-- <asp:Button ID="btnCancelRequired_Material" runat="server" Text="Cancel" OnClick="btnCancelRequired_Material_Click"  CssClass="btn btn-default"  CausesValidation="false"  />--%>
                    </center>
                </div>
            </div>
        </div>

    </asp:Panel>
         <ajaxToolkit:ModalPopupExtender ID="ModalSOWItem" runat="server" PopupControlID="PanelSOWItem" TargetControlID="btnAddSOW"
        CancelControlID="BtnClose2" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    
       <asp:Panel ID="PanelSOWItem" runat="server" align="center" Style="display: none" >
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="BtnClose2" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center><h5 id="myModalamt1SOW"><asp:Label ID="Label2" runat="server" Text="Add Utilized Machinary"></asp:Label></h5></center>
                </div>
                <div class="modal-body">   
                    <div class="row">
                        
                        </div>
                      <div class="row">
                       <div class="col-md-4" style="text-align:right">
                          Select Machinary Name
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValUtilizedMachinary" ControlToValidate="ddlMachinary_Name"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlMachinary_Name" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlMachinary_Name_SelectedIndexChanged" AutoPostBack="true"  TabIndex="8">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                     <div class="row">
                        
                        <div class="col-md-2" style="text-align:left">
                            Reg Number&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtRegNumber"    CssClass="form-control"   MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                          <div class="col-md-2" style="text-align:left">
                    Date&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUtilizedMachinaryDate" CssClass="Validation_Text" ValidationGroup="ValUtilizedMachinary" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtUtilizedMachinaryDate" autocomplete="off" onpaste="javascript:return false;" onkeypress="javascript:return false;" CssClass="form-control" TabIndex="2"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd-MM-yyyy" TargetControlID="txtUtilizedMachinaryDate"></ajaxToolkit:CalendarExtender>
                </div>
                    </div>
                     <div class="row">
                        
                        <div class="col-md-2" style="text-align:left">
                            Start KM&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtStartKm" Text="0.00" OnKeyUp="calcOutput(this);"   CssClass="form-control input-pos-float"  MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                         <div class="col-md-2" style="text-align:left">
                           End KM&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtEndKM"   Text="0.00" OnKeyUp="calcOutput(this)" CssClass="form-control input-pos-float"  MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>
                      <div class="row">
                        
                        <div class="col-md-2" style="text-align:left">
                            UOM&nbsp;
                        </div>
                        <div class="col-md-4">
                          <asp:DropDownList ID="ddlUOMUsedMat" CssClass="form-control" runat="server"  TabIndex="8">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                   
                        </div>
                         <div class="col-md-2" style="text-align:left">
                           OutPut&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtOutput"   CssClass="form-control input-pos-float"  MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>
                     <div class="row">
                        
                        <div class="col-md-2" style="text-align:left">
                            Issued Diesel&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtissuedDiesel"   CssClass="form-control input-pos-float"  MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                         <div class="col-md-2" style="text-align:left">
                           Remarks&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtRemarksUsedMat" TextMode="MultiLine"   CssClass="form-control"  MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        
                        <div class="col-md-2" style="text-align:left">
                            Upload File&nbsp;
                        </div>
                      <div class="col-md-4">
                                <asp:FileUpload runat="server" ID="fup_UtilizedMachinary_File" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." 
                        ForeColor="Red" ControlToValidate="fup_UtilizedMachinary_File"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                            </div>
                         <div class="col-md-2" style="text-align:left">
                          Unit&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlUnit" CssClass="form-control" TabIndex="2">
                        <asp:ListItem Value="0" Text="-Select-" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="Hours" Text="Hours"></asp:ListItem>
                        <asp:ListItem Value="Kms" Text="Kms"></asp:ListItem>
                                  </asp:DropDownList>
                        </div>
                    </div>
                </div>
                    <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnSaveUtilizedMachinary" runat="server" Text="Save" OnClick="btnSaveUtilizedMachinary_Click"  CssClass="btn btn-default" ValidationGroup="ValUtilizedMachinary"  />
                       <%-- <asp:Button ID="Button5" runat="server" Text="Cancel" OnClick="btnCancelRequired_Machinery_Click"  CssClass="btn btn-default"  CausesValidation="false"  />--%>
                    </center>
                </div>
            </div>
        </div>

    </asp:Panel>
       <ajaxToolkit:ModalPopupExtender ID="ModalPopupUtilizedLabours" runat="server" PopupControlID="PanelUtilizedLabours" TargetControlID="btnUtilizedLabours"
        CancelControlID="BtnClosePanelUtilizedLabours" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    
       <asp:Panel ID="PanelUtilizedLabours" runat="server" align="center" Style="display: none" >
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="BtnClosePanelUtilizedLabours" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center><h5 id="myModelUtilizedLabours"><asp:Label ID="Label4" runat="server" Text="Add Utilized Labour"></asp:Label></h5></center>
                </div>
          <div class="modal-body">
                     <div class="row">
                         <div class="col-md-4" style="text-align:right">
                    Date&nbsp;
                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtUtilizedLabourDate" CssClass="Validation_Text" ValidationGroup="ValUtilizedMachinary" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                </div>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtUtilizedLabourDate" autocomplete="off" onpaste="javascript:return false;" onkeypress="javascript:return false;" CssClass="form-control" TabIndex="2"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" Format="dd-MM-yyyy" TargetControlID="txtUtilizedLabourDate"></ajaxToolkit:CalendarExtender>
                </div>
                        </div>
                     <div class="row">
                      <div class="col-md-4" style="text-align:right"> 
                   Labour Type&nbsp;
                    </div>
                                 <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="ddlLabour" class="form-control" OnSelectedIndexChanged="ddlLabour_Name_SelectedIndexChanged" AutoPostBack="true"  TabIndex="8"></asp:DropDownList>
                </div>
                          </div>
                      <div class="row">
                        <div class="col-md-4" style="text-align:right">
                           Quantity&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="txtLaboursQuantity"  CssClass="Validation_Text" ValidationGroup="ValRequired_Labours" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6" style="text-align:right">
                            <asp:TextBox ID="txtLaboursQuantity"   CssClass="form-control input-pos-float"  MaxLength="50" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>
                      <div class="row">
                         <div class="col-md-4" style="text-align:right">
                            UOM
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValRequired_Labours" ControlToValidate="ddlLaboursUOM"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6" style="text-align:right">
                            <asp:DropDownList ID="ddlLaboursUOM"  CssClass="form-control" runat="server"  TabIndex="8">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="Button6" runat="server" Text="Save" OnClick="btnSaveUtilized_Labours_Click"  CssClass="btn btn-default" ValidationGroup="ValRequired_Labours"  />
                        <asp:Button ID="Button8" runat="server" Text="Cancel" OnClick="btnCancelUtilizedLabours_Click"  CssClass="btn btn-default"  CausesValidation="false"  />
                    </center>
                </div>
            </div>
        </div>

    </asp:Panel>
         <ajaxToolkit:ModalPopupExtender ID="ModalPopupAssignSC" runat="server" PopupControlID="PanelAssignSCtem" TargetControlID="btnAssignSC"
        CancelControlID="AssignSC_Close" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
       <asp:Panel ID="PanelAssignSCtem" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="AssignSC_Close" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center><h5 id="myModalamt1AssignSC"><asp:Label ID="Label1" runat="server" Text="Add Assign Work To Sub Contractor"></asp:Label></h5></center>
                </div>
                <div class="modal-body">   
                      <div class="row">
                      <div  class="col-md-4" style="text-align:right">Sub Contractor Name
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17"  runat="server" InitialValue="-Select-" ControlToValidate="ddlSubContractor" CssClass="Validation_Text" ValidationGroup="ValRequired_SC" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </div>
                    <div  class="col-md-6" style="text-align:right">
                        <asp:DropDownList runat="server" ID="DropDownList2"  class="chosen-select form-control" TabIndex="4"></asp:DropDownList>
                    </div>
                    </div>
                     <div class="row">
                        
                        <div class="col-md-2" style="text-align:right">
                             Sub Work List&nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="*" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValRequired_SC" ControlToValidate="ddlSubworkAssingment"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlSubworkAssingment" CssClass="form-control" runat="server"   TabIndex="8">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>


                    </div>
                   
                </div>
                    <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnbtnSaveContractor" runat="server" Text="Save" OnClick="btnSaveContractor_Click"  CssClass="btn btn-default" ValidationGroup="ValRequired_SC"  />
                       <%-- <asp:Button ID="Button5" runat="server" Text="Cancel" OnClick="btnCancelRequired_Machinery_Click"  CssClass="btn btn-default"  CausesValidation="false"  />--%>
                    </center>
                </div>
            </div>
        </div>

    </asp:Panel>
    </asp:Content>