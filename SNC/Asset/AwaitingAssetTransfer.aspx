<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage/SNC.Master" CodeBehind="~/Asset/AwaitingAssetTransfer.aspx.cs" Inherits="AwaitingAssetTransfer" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <script type="text/javascript">
          window.setInterval(function () {
              window.location.reload();
          }, 600000);
    </script>

    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                List of Asset Transfer requests

            </h3>

        </div>
        <div class="panel-body">
            <center>
       
                 <ogrid:Grid runat="server" ID="GridAssets" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true"  PageSize="10">
            <ScrollingSettings ScrollWidth="100%" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
            <Columns>
                 <ogrid:Column DataField="Print" HeaderText="Print">
                             <TemplateSettings TemplateId="PrintID" />
                        </ogrid:Column>
                
                 <ogrid:Column DataField="AssetTran_ID" HeaderText="AssetTran_ID" Visible="false"></ogrid:Column> 
                 <ogrid:Column DataField="Asset_Code" HeaderText="AssetCode" ReadOnly="true"></ogrid:Column>                
                <ogrid:Column DataField="Name" HeaderText="AssetName" ReadOnly="true"></ogrid:Column>
                  <ogrid:Column DataField="Asset_Type" HeaderText="Type" ReadOnly="true"></ogrid:Column>                
                <ogrid:Column DataField="Category_Name" HeaderText="Category" ReadOnly="true"></ogrid:Column>
                  <ogrid:Column DataField="Condition" HeaderText="Condition" ReadOnly="true"></ogrid:Column>                
                <ogrid:Column DataField="Status" HeaderText="Status" ReadOnly="true"></ogrid:Column> 
                
                 
                 <ogrid:CheckBoxSelectColumn HeaderText=" Select All" ShowHeaderCheckBox="true" Width="100px"></ogrid:CheckBoxSelectColumn>
                
            </Columns>
                      <Templates>
                        <ogrid:GridTemplate ID="PrintID" runat="server">
                            <Template>
                                 
                                
 <asp:LinkButton ID="lnkbtnPrint" runat="server" CssClass="gridCB" CommandName='<%#Container.DataItem["AssetTran_ID"] %>'  Text='Print' OnClick="lnkbtnPrint_Click"></asp:LinkButton>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
              
      
        </ogrid:Grid>
                <br />
                <br />

                    
                     
                    <asp:Button ID="btnAccepts" runat="server" Text="Accept" CssClass="btn btn-default" OnClick="btnAccepts_Click"   ></asp:Button>&nbsp;
                <asp:Button ID="btnRejects" CssClass="btn btn-default" Text="Reject" runat="server" OnClick="btnRejects_Click" />
                    
                
                
                      </center>
        </div>
    </div>
</asp:Content>