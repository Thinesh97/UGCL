<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Users" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style type="text/css">
        .chkbox {
            width: 40px;
            text-align: center;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .auto-style1 {
            height: 31px;
        }
    </style>
    
    <script type="text/javascript">
        function beforedelete() {
            if (confirm("This record will now be Deleting. Do you want to proceed?") == false) {
                return false;
            }
            return true;
        }

        function chkboxUserAll() {
            if (document.getElementById('<%=chkboxUserAll.ClientID%>').checked) {
                 document.getElementById('<%=chkboxUserCreate.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxUserview.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxUserupdate.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxUserdelete.ClientID%>').checked = true;
             }
             else {
                 document.getElementById('<%=chkboxUserCreate.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxUserview.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxUserupdate.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxUserdelete.ClientID%>').checked = false;
             }
        }

        function chkboxEmpContractAll() {
            if (document.getElementById('<%=chkboxEmpContractAll.ClientID%>').checked) {
                document.getElementById('<%=chkboxEmpContractCreate.ClientID%>').checked = true;
                document.getElementById('<%=chkboxEmpContractView.ClientID%>').checked = true;
                document.getElementById('<%=chkboxEmpContractUpdate.ClientID%>').checked = true;
                document.getElementById('<%=chkboxEmpContractDelete.ClientID%>').checked = true;
            }
            else {
                document.getElementById('<%=chkboxEmpContractCreate.ClientID%>').checked = false;
                document.getElementById('<%=chkboxEmpContractView.ClientID%>').checked = false;
                document.getElementById('<%=chkboxEmpContractUpdate.ClientID%>').checked = false;
                document.getElementById('<%=chkboxEmpContractDelete.ClientID%>').checked = false;
            }
        }

        function chkboxMailconfigAll() {
            if (document.getElementById('<%=chkboxMailconfigAll.ClientID%>').checked) {
                document.getElementById('<%=chkboxMailConfigUpdate.ClientID%>').checked = true;
            }
            else {
                document.getElementById('<%=chkboxMailConfigUpdate.ClientID%>').checked = false;
            }
        }

        function chkboxStockTransferAll() {
             if (document.getElementById('<%=chkboxStockTransferAll.ClientID%>').checked) {
                 document.getElementById('<%=chkboxStockTransferCreate.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxStockTransferView.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxStockTransferUpdate.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxStockTransferDelete.ClientID%>').checked = true;
             }
             else {
                 document.getElementById('<%=chkboxStockTransferCreate.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxStockTransferView.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxStockTransferUpdate.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxStockTransferDelete.ClientID%>').checked = false;
             }
         }

         function chckboxAllComSite() {
             if (document.getElementById('<%=chckboxAllComSite.ClientID%>').checked) {
                 document.getElementById('<%=chckboxCreateComSite.ClientID%>').checked = true;
                 document.getElementById('<%=chckboxViewComSite.ClientID%>').checked = true;
                 document.getElementById('<%=chckboxUpdateComSite.ClientID%>').checked = true;
                 document.getElementById('<%=chckboxDeleteComSite.ClientID%>').checked = true;
             }
             else {
                 document.getElementById('<%=chckboxCreateComSite.ClientID%>').checked = false;
                 document.getElementById('<%=chckboxViewComSite.ClientID%>').checked = false;
                 document.getElementById('<%=chckboxUpdateComSite.ClientID%>').checked = false;
                 document.getElementById('<%=chckboxDeleteComSite.ClientID%>').checked = false;
             }
         }

         function ChkboxAllProject() {
             if (document.getElementById('<%=ChkboxAllProject.ClientID%>').checked) {
                document.getElementById('<%=ChkboxCreateProject.ClientID%>').checked = true;
                document.getElementById('<%=ChkboxViewProject.ClientID%>').checked = true;
                document.getElementById('<%=ChkboxUpdateProject.ClientID%>').checked = true;
                document.getElementById('<%=ChkboxDeleteProject.ClientID%>').checked = true;
            }
            else {
                document.getElementById('<%=ChkboxCreateProject.ClientID%>').checked = false;
                document.getElementById('<%=ChkboxViewProject.ClientID%>').checked = false;
                document.getElementById('<%=ChkboxUpdateProject.ClientID%>').checked = false;
                document.getElementById('<%=ChkboxDeleteProject.ClientID%>').checked = false;
            }
        }

        function ChkBoxAllUom() {
            if (document.getElementById('<%=ChkBoxAllUom.ClientID%>').checked) {
                 document.getElementById('<%=ChkBoxCreateUom.ClientID%>').checked = true;
                 document.getElementById('<%=ChkBoxViewUom.ClientID%>').checked = true;
                 document.getElementById('<%=ChkBoxUpdateUom.ClientID%>').checked = true;
                 document.getElementById('<%=ChkBoxDeleteUom.ClientID%>').checked = true;
             }
             else {
                 document.getElementById('<%=ChkBoxCreateUom.ClientID%>').checked = false;
                 document.getElementById('<%=ChkBoxViewUom.ClientID%>').checked = false;
                 document.getElementById('<%=ChkBoxUpdateUom.ClientID%>').checked = false;
                 document.getElementById('<%=ChkBoxDeleteUom.ClientID%>').checked = false;
             }
        }

        function chkboxMaterialAll() {
            if (document.getElementById('<%=chkboxMaterialAll.ClientID%>').checked) {
                 document.getElementById('<%=chkboxMateriaCreate.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxMateriaView.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxMateriaUpdate.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxMaterialDelete.ClientID%>').checked = true;
             }
             else {
                 document.getElementById('<%=chkboxMateriaCreate.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxMateriaView.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxMateriaUpdate.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxMaterialDelete.ClientID%>').checked = false;
             }
         }

         function ChBoxVendorAll() {
             if (document.getElementById('<%=ChBoxVendorAll.ClientID%>').checked) {
                 document.getElementById('<%=ChBoxVendorCreate.ClientID%>').checked = true;
                 document.getElementById('<%=ChBoxVendorView.ClientID%>').checked = true;
                 document.getElementById('<%=ChBoxVendorUpdate.ClientID%>').checked = true;
                 document.getElementById('<%=ChBoxVendorDelete.ClientID%>').checked = true;
             }
             else {
                 document.getElementById('<%=ChBoxVendorCreate.ClientID%>').checked = false;
                 document.getElementById('<%=ChBoxVendorView.ClientID%>').checked = false;
                 document.getElementById('<%=ChBoxVendorUpdate.ClientID%>').checked = false;
                 document.getElementById('<%=ChBoxVendorDelete.ClientID%>').checked = false;
             }
         }

         function chkboxSubContractorALL() {
             if (document.getElementById('<%=chkboxSubContractorALL.ClientID%>').checked) {
                 document.getElementById('<%=chkboxSubContractorCreate.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxSubContractorView.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxSubContractorUpdate.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxSubContractorDelete.ClientID%>').checked = true;
             }
             else {
                 document.getElementById('<%=chkboxSubContractorCreate.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxSubContractorView.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxSubContractorUpdate.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxSubContractorDelete.ClientID%>').checked = false;
             }
         }

        function chkboxOtherALL() {
            if (document.getElementById('<%=chkboxOtherAll.ClientID%>').checked) {
                document.getElementById('<%=chkboxOtherCreate.ClientID%>').checked = true;
                document.getElementById('<%=chkboxOtherView.ClientID%>').checked = true;
                document.getElementById('<%=chkboxOtherUpdate.ClientID%>').checked = true;
                document.getElementById('<%=chkboxOtherDelete.ClientID%>').checked = true;
            }
            else {
                document.getElementById('<%=chkboxOtherCreate.ClientID%>').checked = false;
                document.getElementById('<%=chkboxOtherView.ClientID%>').checked = false;
                document.getElementById('<%=chkboxOtherUpdate.ClientID%>').checked = false;
                document.getElementById('<%=chkboxOtherDelete.ClientID%>').checked = false;
            }
        }


         function chkBoxBudgetAll() {
             if (document.getElementById('<%=chkBoxBudgetAll.ClientID%>').checked) {
                 document.getElementById('<%=chkBoxBudgetCreate.ClientID%>').checked = true;
                 document.getElementById('<%=chkBoxBudgetView.ClientID%>').checked = true;
                 document.getElementById('<%=chkBoxBudgetUpdate.ClientID%>').checked = true;
                 document.getElementById('<%=chkBoxBudgetDelete.ClientID%>').checked = true;
             }
             else {
                 document.getElementById('<%=chkBoxBudgetCreate.ClientID%>').checked = false;
                 document.getElementById('<%=chkBoxBudgetView.ClientID%>').checked = false;
                 document.getElementById('<%=chkBoxBudgetUpdate.ClientID%>').checked = false;
                 document.getElementById('<%=chkBoxBudgetDelete.ClientID%>').checked = false;
             }
         }
        

        function chkbox_Local_MRN_ALL() {

            if (document.getElementById('<%=chk_Local_MRN_All.ClientID%>').checked) {
                document.getElementById('<%=chk_Local_MRN_Create.ClientID%>').checked = true;
                 document.getElementById('<%=chk_Local_MRN_Update.ClientID%>').checked = true;
                 document.getElementById('<%=chk_LocalMRN_View.ClientID%>').checked = true;
                 document.getElementById('<%=chk_Local_MRN_Delete.ClientID%>').checked = true;
             }

             else {
                 document.getElementById('<%=chk_Local_MRN_Create.ClientID%>').checked = false;
                 document.getElementById('<%=chk_Local_MRN_Update.ClientID%>').checked = false;
                 document.getElementById('<%=chk_LocalMRN_View.ClientID%>').checked = false;
                 document.getElementById('<%=chk_Local_MRN_Delete.ClientID%>').checked = false;
             }
         }

         function chkboxBudgetModREquestALL() {

             if (document.getElementById('<%=chkboxBudgetModREquestALL.ClientID%>').checked) {
                 document.getElementById('<%=chkboxBudgetModREquestCreate.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxBudgetModREquestView.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxBudgetModREquestUpdate.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxBudgetModREquestDelete.ClientID%>').checked = true;
             }

             else {
                 document.getElementById('<%=chkboxBudgetModREquestCreate.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxBudgetModREquestView.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxBudgetModREquestUpdate.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxBudgetModREquestDelete.ClientID%>').checked = false;
             }
         }


         function ChkBoxReportALL() {

             if (document.getElementById('<%=ChkBoxReportALL.ClientID%>').checked) {
                 document.getElementById('<%=ChkBoxReportView.ClientID%>').checked = true;
             }

             else {
                 document.getElementById('<%=ChkBoxReportView.ClientID%>').checked = false;
             }

         }
         function chkboxQuotationtAll() {

             if (document.getElementById('<%=chkboxQuotationtAll.ClientID%>').checked) {
                 document.getElementById('<%=chkboxQuotationCreate.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxQuotationView.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxQuotationUpdate.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxQuotationDelete.ClientID%>').checked = true;
             }

             else {
                 document.getElementById('<%=chkboxQuotationCreate.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxQuotationView.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxQuotationUpdate.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxQuotationDelete.ClientID%>').checked = false;
             }
         }
         function CheckBox1() {

             if (document.getElementById('<%=CheckBox1.ClientID%>').checked) {
                 document.getElementById('<%=chkboxQuotationCompare.ClientID%>').checked = true;
             }

             else {
                 document.getElementById('<%=chkboxQuotationCompare.ClientID%>').checked = false;
             }

         }


         function chkboxIndentAll() {

             if (document.getElementById('<%=chkboxIndentAll.ClientID%>').checked) {
                 document.getElementById('<%=chkboxIndentCreate.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxIndentView.ClientID%>').checked = true;
                 document.getElementById('<%=CheckBoxIndentUpdate.ClientID%>').checked = true;
                 document.getElementById('<%=CheckBoxIndentDelete.ClientID%>').checked = true;
             }

             else {
                 document.getElementById('<%=chkboxIndentCreate.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxIndentView.ClientID%>').checked = false;
                 document.getElementById('<%=CheckBoxIndentUpdate.ClientID%>').checked = false;
                 document.getElementById('<%=CheckBoxIndentDelete.ClientID%>').checked = false;
             }
         }


         function ChkBoxPOAll() {

             if (document.getElementById('<%=ChkBoxPOAll.ClientID%>').checked) {
                 document.getElementById('<%=ChkBoxPOCreate.ClientID%>').checked = true;
                 document.getElementById('<%=ChkBoxView.ClientID%>').checked = true;
                 document.getElementById('<%=ChkBoxPOUpdate.ClientID%>').checked = true;
                 document.getElementById('<%=ChkBoxDelete.ClientID%>').checked = true;
             }

             else {
                 document.getElementById('<%=ChkBoxPOCreate.ClientID%>').checked = false;
                 document.getElementById('<%=ChkBoxView.ClientID%>').checked = false;
                 document.getElementById('<%=ChkBoxPOUpdate.ClientID%>').checked = false;
                 document.getElementById('<%=ChkBoxDelete.ClientID%>').checked = false;
             }
         }

        function ChkBoxApprove_PO_Delete_All() {

            if (document.getElementById('<%=chk_Appr_PO_Delete_All.ClientID%>').checked) {
                document.getElementById('<%=chk_Appr_PO_Delete.ClientID%>').checked = true;
            }

            else {
                document.getElementById('<%=chk_Appr_PO_Delete.ClientID%>').checked = false;
            }
        }


        function ChkBoxPayIndAll() {

            if (document.getElementById('<%=ChkBoxPayIndAll.ClientID%>').checked) {
                document.getElementById('<%=ChkBoxPayIndCreate.ClientID%>').checked = true;
                document.getElementById('<%=ChkBoxPayIndView.ClientID%>').checked = true;
                document.getElementById('<%=ChkBoxPayIndUpdate.ClientID%>').checked = true;
                document.getElementById('<%=ChkBoxPayIndDelete.ClientID%>').checked = true;
            }

            else {
                document.getElementById('<%=ChkBoxPayIndCreate.ClientID%>').checked = false;
                document.getElementById('<%=ChkBoxPayIndView.ClientID%>').checked = false;
                document.getElementById('<%=ChkBoxPayIndUpdate.ClientID%>').checked = false;
                document.getElementById('<%=ChkBoxPayIndDelete.ClientID%>').checked = false;
            }
        }

        function ChkBoxPayIndVerAll() {

            if (document.getElementById('<%=ChkBoxPayIndVerAll.ClientID%>').checked) {
                document.getElementById('<%=ChkBoxPayIndVerCreate.ClientID%>').checked = true;
                document.getElementById('<%=ChkBoxPayIndVerView.ClientID%>').checked = true;
                document.getElementById('<%=ChkBoxPayIndVerUpdate.ClientID%>').checked = true;
                document.getElementById('<%=ChkBoxPayIndVerDelete.ClientID%>').checked = true;
            }

            else {
                document.getElementById('<%=ChkBoxPayIndVerCreate.ClientID%>').checked = false;
                document.getElementById('<%=ChkBoxPayIndVerView.ClientID%>').checked = false;
                document.getElementById('<%=ChkBoxPayIndVerUpdate.ClientID%>').checked = false;
                document.getElementById('<%=ChkBoxPayIndVerDelete.ClientID%>').checked = false;
            }
        }

        function ChkBoxPayIndAppAll() {

            if (document.getElementById('<%=ChkBoxPayIndAppAll.ClientID%>').checked) {
                document.getElementById('<%=ChkBoxPayIndAppCreate.ClientID%>').checked = true;
                document.getElementById('<%=ChkBoxPayIndAppView.ClientID%>').checked = true;
                document.getElementById('<%=ChkBoxPayIndAppUpdate.ClientID%>').checked = true;
                document.getElementById('<%=ChkBoxPayIndAppDelete.ClientID%>').checked = true;
            }

            else {
                document.getElementById('<%=ChkBoxPayIndAppCreate.ClientID%>').checked = false;
                document.getElementById('<%=ChkBoxPayIndAppView.ClientID%>').checked = false;
                document.getElementById('<%=ChkBoxPayIndAppUpdate.ClientID%>').checked = false;
                document.getElementById('<%=ChkBoxPayIndAppDelete.ClientID%>').checked = false;
            }
        }

         function chkboxPOReportAll() {

             if (document.getElementById('<%=chkboxPOReportAll.ClientID%>').checked) {
                 document.getElementById('<%=chkboxPOReportView.ClientID%>').checked = true;
             }

             else {
                 document.getElementById('<%=chkboxPOReportView.ClientID%>').checked = false;
             }

         }

         function chkboxStockAll() {

             if (document.getElementById('<%=chkboxStockAll.ClientID%>').checked) {
                 document.getElementById('<%=chkboxStockCreate.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxStockView.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxStockUpdate.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxStockDelete.ClientID%>').checked = true;
             }

             else {
                 document.getElementById('<%=chkboxStockCreate.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxStockView.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxStockUpdate.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxStockDelete.ClientID%>').checked = false;
             }
         }
         function chkboxMRNAll() {

             if (document.getElementById('<%=chkboxMRNAll.ClientID%>').checked) {
                 document.getElementById('<%=ChkBoxMRNCreate.ClientID%>').checked = true;
                 document.getElementById('<%=ChkBoxMRNView.ClientID%>').checked = true;
                 document.getElementById('<%=ChkBoxMRNUpdate.ClientID%>').checked = true;
                 document.getElementById('<%=ChkBoxMRNDelete.ClientID%>').checked = true;
             }

             else {
                 document.getElementById('<%=ChkBoxMRNCreate.ClientID%>').checked = false;
                 document.getElementById('<%=ChkBoxMRNView.ClientID%>').checked = false;
                 document.getElementById('<%=ChkBoxMRNUpdate.ClientID%>').checked = false;
                 document.getElementById('<%=ChkBoxMRNDelete.ClientID%>').checked = false;
             }
         }
         function ChkBoxMIN_All() {

             if (document.getElementById('<%=ChkBoxMIN_All.ClientID%>').checked) {
                 document.getElementById('<%=ChkBoxMINCreate.ClientID%>').checked = true;
                 document.getElementById('<%=ChkBoxMINView.ClientID%>').checked = true;
                 document.getElementById('<%=ChkBoxMINUpdate.ClientID%>').checked = true;
                 document.getElementById('<%=ChkBoxMINDelete.ClientID%>').checked = true;
             }

             else {
                 document.getElementById('<%=ChkBoxMINCreate.ClientID%>').checked = false;
                 document.getElementById('<%=ChkBoxMINView.ClientID%>').checked = false;
                 document.getElementById('<%=ChkBoxMINUpdate.ClientID%>').checked = false;
                 document.getElementById('<%=ChkBoxMINDelete.ClientID%>').checked = false;
             }
         }

         function ChkBoxAssetRegAll() {

             if (document.getElementById('<%=ChkBoxAssetRegAll.ClientID%>').checked) {
                 document.getElementById('<%=ChkBoxAssetRegCreate.ClientID%>').checked = true;
                 document.getElementById('<%=ChkBoxAssetRegView.ClientID%>').checked = true;
                 document.getElementById('<%=ChkBoxAssetRegUpdate.ClientID%>').checked = true;
                 document.getElementById('<%=ChkBoxAssetRegDelete.ClientID%>').checked = true;
             }

             else {
                 document.getElementById('<%=ChkBoxAssetRegCreate.ClientID%>').checked = false;
                 document.getElementById('<%=ChkBoxAssetRegView.ClientID%>').checked = false;
                 document.getElementById('<%=ChkBoxAssetRegUpdate.ClientID%>').checked = false;
                 document.getElementById('<%=ChkBoxAssetRegDelete.ClientID%>').checked = false;
             }
         }
         function ChkBoxINVReportsAll() {

             if (document.getElementById('<%=ChkBoxINVReportsAll.ClientID%>').checked) {
                 document.getElementById('<%=ChkBoxINVReportsView.ClientID%>').checked = true;
             }

             else {
                 document.getElementById('<%=ChkBoxINVReportsView.ClientID%>').checked = false;
             }

         }


         function ChkBoxAssetRegAll() {

             if (document.getElementById('<%=ChkBoxAssetRegAll.ClientID%>').checked) {
                 document.getElementById('<%=ChkBoxAssetRegCreate.ClientID%>').checked = true;
                 document.getElementById('<%=ChkBoxAssetRegView.ClientID%>').checked = true;
                 document.getElementById('<%=ChkBoxAssetRegUpdate.ClientID%>').checked = true;
                 document.getElementById('<%=ChkBoxAssetRegDelete.ClientID%>').checked = true;
             }

             else {
                 document.getElementById('<%=ChkBoxAssetRegCreate.ClientID%>').checked = false;
                 document.getElementById('<%=ChkBoxAssetRegView.ClientID%>').checked = false;
                 document.getElementById('<%=ChkBoxAssetRegUpdate.ClientID%>').checked = false;
                 document.getElementById('<%=ChkBoxAssetRegDelete.ClientID%>').checked = false;
             }
         }


         function ChkBoxAssetTransferAll() {

             if (document.getElementById('<%=ChkBoxAssetTransferAll.ClientID%>').checked) {
                 document.getElementById('<%=ChkBoxAssetTransferCreate.ClientID%>').checked = true;
                 document.getElementById('<%=ChkBoxAssetTransferView.ClientID%>').checked = true;
                 document.getElementById('<%=ChkBoxAssetTransferUpdate.ClientID%>').checked = true;
                 document.getElementById('<%=ChkBoxAssetTransferDelete.ClientID%>').checked = true;
             }

             else {
                 document.getElementById('<%=ChkBoxAssetTransferCreate.ClientID%>').checked = false;
                 document.getElementById('<%=ChkBoxAssetTransferView.ClientID%>').checked = false;
                 document.getElementById('<%=ChkBoxAssetTransferUpdate.ClientID%>').checked = false;
                 document.getElementById('<%=ChkBoxAssetTransferDelete.ClientID%>').checked = false;
             }
         }


         function chkboxDailyRunHourKmAll() {

             if (document.getElementById('<%=chkboxDailyRunHourKmAll.ClientID%>').checked) {
                 document.getElementById('<%=chkboxDailyRunHourKmCreate.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxDailyRunHourKmView.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxDailyRunHourKmUpdate.ClientID%>').checked = true;
                 document.getElementById('<%=chkboxDailyRunHourKmDelete.ClientID%>').checked = true;
             }

             else {
                 document.getElementById('<%=chkboxDailyRunHourKmCreate.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxDailyRunHourKmView.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxDailyRunHourKmUpdate.ClientID%>').checked = false;
                 document.getElementById('<%=chkboxDailyRunHourKmDelete.ClientID%>').checked = false;
             }
         }





         function ChkBoxAssetReportsAll() {

             if (document.getElementById('<%=ChkBoxAssetReportsAll.ClientID%>').checked) {
                 document.getElementById('<%=ChkBoxAssetReportsView.ClientID%>').checked = true;
             }

             else {
                 document.getElementById('<%=ChkBoxAssetReportsView.ClientID%>').checked = false;
             }

         }

         function ChkboxAllProjectBudget() {

             if (document.getElementById('<%=ChkProBudgetAll.ClientID%>').checked) {
                 document.getElementById('<%=ChkProBudgetCreate.ClientID%>').checked = true;
                 document.getElementById('<%=ChkProBudgetView.ClientID%>').checked = true;
                 document.getElementById('<%=ChkProBudgetUpdate.ClientID%>').checked = true;
                 document.getElementById('<%=ChkProBudgetDelete.ClientID%>').checked = true;
             }

             else {
                 document.getElementById('<%=ChkProBudgetCreate.ClientID%>').checked = false;
                 document.getElementById('<%=ChkProBudgetView.ClientID%>').checked = false;
                 document.getElementById('<%=ChkProBudgetUpdate.ClientID%>').checked = false;
                 document.getElementById('<%=ChkProBudgetDelete.ClientID%>').checked = false;
             }
         }



    </script>

    <script type="text/javascript">
        function SubCheckUser() {
            if (document.getElementById('<%=chkboxUserCreate.ClientID%>').checked == false || document.getElementById('<%=chkboxUserview.ClientID%>').checked == false || document.getElementById('<%=chkboxUserupdate.ClientID%>').checked == false || document.getElementById('<%=chkboxUserdelete.ClientID%>').checked == false) {
                document.getElementById('<%=chkboxUserAll.ClientID%>').checked = false;
            }
            else if (document.getElementById('<%=chkboxUserCreate.ClientID%>').checked == false || document.getElementById('<%=chkboxUserview.ClientID%>').checked == false || document.getElementById('<%=chkboxUserupdate.ClientID%>').checked == false || document.getElementById('<%=chkboxUserdelete.ClientID%>').checked == false) {
                document.getElementById('<%=chkboxUserAll.ClientID%>').checked = false;
            }
            else if (document.getElementById('<%=chkboxUserCreate.ClientID%>').checked == true && document.getElementById('<%=chkboxUserview.ClientID%>').checked == true && document.getElementById('<%=chkboxUserupdate.ClientID%>').checked == true && document.getElementById('<%=chkboxUserdelete.ClientID%>').checked == true) {
                document.getElementById('<%=chkboxUserAll.ClientID%>').checked = true;
            }
        }

        function SubCheckEmpContract() {
            if (document.getElementById('<%=chkboxEmpContractCreate.ClientID%>').checked == false || document.getElementById('<%=chkboxEmpContractView.ClientID%>').checked == false || document.getElementById('<%=chkboxEmpContractUpdate.ClientID%>').checked == false || document.getElementById('<%=chkboxEmpContractDelete.ClientID%>').checked == false) {
                document.getElementById('<%=chkboxEmpContractAll.ClientID%>').checked = false;
            }
            else if (document.getElementById('<%=chkboxEmpContractCreate.ClientID%>').checked == false || document.getElementById('<%=chkboxEmpContractView.ClientID%>').checked == false || document.getElementById('<%=chkboxEmpContractUpdate.ClientID%>').checked == false || document.getElementById('<%=chkboxEmpContractDelete.ClientID%>').checked == false) {
                document.getElementById('<%=chkboxEmpContractAll.ClientID%>').checked = false;
            }
            else if (document.getElementById('<%=chkboxEmpContractCreate.ClientID%>').checked == true && document.getElementById('<%=chkboxEmpContractView.ClientID%>').checked == true && document.getElementById('<%=chkboxEmpContractUpdate.ClientID%>').checked == true && document.getElementById('<%=chkboxEmpContractDelete.ClientID%>').checked == true) {
                document.getElementById('<%=chkboxEmpContractAll.ClientID%>').checked = true;
            }
        }

        function SubCheckMail() {
            if (document.getElementById('<%=chkboxMailConfigUpdate.ClientID%>').checked == false) {
                document.getElementById('<%=chkboxMailconfigAll.ClientID%>').checked = false;
            }
            else if (document.getElementById('<%=chkboxMailConfigUpdate.ClientID%>').checked == true) {
                document.getElementById('<%=chkboxMailconfigAll.ClientID%>').checked = true;
            }
        }

        function SubCheckSite() {
            if (document.getElementById('<%=chckboxCreateComSite.ClientID%>').checked == false
                || document.getElementById('<%=chckboxViewComSite.ClientID%>').checked == false
                || document.getElementById('<%=chckboxUpdateComSite.ClientID%>').checked == false
                || document.getElementById('<%=chckboxDeleteComSite.ClientID%>').checked == false) {
                document.getElementById('<%=chckboxAllComSite.ClientID%>').checked = false;
            }

            else if (document.getElementById('<%=chckboxCreateComSite.ClientID%>').checked == true
                && document.getElementById('<%=chckboxViewComSite.ClientID%>').checked == true &&
                document.getElementById('<%=chckboxUpdateComSite.ClientID%>').checked == true &&
                document.getElementById('<%=chckboxDeleteComSite.ClientID%>').checked == true) {
                document.getElementById('<%=chckboxAllComSite.ClientID%>').checked = true;
            }
        }
        function SubCheckProjects() {
            if (document.getElementById('<%=ChkboxCreateProject.ClientID%>').checked == false
                 || document.getElementById('<%=ChkboxViewProject.ClientID%>').checked == false
                || document.getElementById('<%=ChkboxUpdateProject.ClientID%>').checked == false
                || document.getElementById('<%=ChkboxDeleteProject.ClientID%>').checked == false) {
                document.getElementById('<%=ChkboxAllProject.ClientID%>').checked = false;
            }

            else if (document.getElementById('<%=ChkboxCreateProject.ClientID%>').checked == true
                && document.getElementById('<%=ChkboxViewProject.ClientID%>').checked == true &&
                document.getElementById('<%=ChkboxUpdateProject.ClientID%>').checked == true &&
                document.getElementById('<%=ChkboxDeleteProject.ClientID%>').checked == true) {
                document.getElementById('<%=ChkboxAllProject.ClientID%>').checked = true;
            }
        }

        function SubCheckUOM() {
            if (document.getElementById('<%=ChkBoxCreateUom.ClientID%>').checked == false
                  || document.getElementById('<%=ChkBoxViewUom.ClientID%>').checked == false
                || document.getElementById('<%=ChkBoxUpdateUom.ClientID%>').checked == false
                || document.getElementById('<%=ChkBoxDeleteUom.ClientID%>').checked == false) {
                document.getElementById('<%=ChkBoxAllUom.ClientID%>').checked = false;
            }
            else if (document.getElementById('<%=ChkBoxCreateUom.ClientID%>').checked == true
                && document.getElementById('<%=ChkBoxViewUom.ClientID%>').checked == true &&
                document.getElementById('<%=ChkBoxUpdateUom.ClientID%>').checked == true &&
                document.getElementById('<%=ChkBoxDeleteUom.ClientID%>').checked == true) {
                document.getElementById('<%=ChkBoxAllUom.ClientID%>').checked = true;
            }
        }

        function SubCheckProjectBudget() {
            if (document.getElementById('<%=ChkProBudgetCreate.ClientID%>').checked == false
                          || document.getElementById('<%=ChkProBudgetView.ClientID%>').checked == false
                || document.getElementById('<%=ChkProBudgetUpdate.ClientID%>').checked == false
                || document.getElementById('<%=ChkProBudgetDelete.ClientID%>').checked == false) {
                document.getElementById('<%=ChkProBudgetAll.ClientID%>').checked = false;
            }
            else if (document.getElementById('<%=ChkProBudgetCreate.ClientID%>').checked == true
                && document.getElementById('<%=ChkProBudgetView.ClientID%>').checked == true &&
                document.getElementById('<%=ChkProBudgetUpdate.ClientID%>').checked == true &&
                document.getElementById('<%=ChkProBudgetDelete.ClientID%>').checked == true) {
                document.getElementById('<%=ChkProBudgetAll.ClientID%>').checked = true;
            }
        }

        function SubCheckMaterial() {
            if (document.getElementById('<%=chkboxMateriaCreate.ClientID%>').checked == false
                   || document.getElementById('<%=chkboxMateriaView.ClientID%>').checked == false
                || document.getElementById('<%=chkboxMateriaUpdate.ClientID%>').checked == false
                || document.getElementById('<%=chkboxMaterialDelete.ClientID%>').checked == false) {
                document.getElementById('<%=chkboxMaterialAll.ClientID%>').checked = false;
            }

            else if (document.getElementById('<%=chkboxMateriaCreate.ClientID%>').checked == true
                && document.getElementById('<%=chkboxMateriaView.ClientID%>').checked == true &&
                document.getElementById('<%=chkboxMateriaUpdate.ClientID%>').checked == true &&
                document.getElementById('<%=chkboxMaterialDelete.ClientID%>').checked == true) {
                document.getElementById('<%=chkboxMaterialAll.ClientID%>').checked = true;
            }
        }

        function SubCheckVendor() {
            if (document.getElementById('<%=ChBoxVendorCreate.ClientID%>').checked == false
                     || document.getElementById('<%=ChBoxVendorView.ClientID%>').checked == false
                || document.getElementById('<%=ChBoxVendorUpdate.ClientID%>').checked == false
                || document.getElementById('<%=ChBoxVendorDelete.ClientID%>').checked == false) {
                document.getElementById('<%=ChBoxVendorAll.ClientID%>').checked = false;
            }

            else if (document.getElementById('<%=ChBoxVendorCreate.ClientID%>').checked == true
                && document.getElementById('<%=ChBoxVendorView.ClientID%>').checked == true &&
                document.getElementById('<%=ChBoxVendorUpdate.ClientID%>').checked == true &&
                document.getElementById('<%=ChBoxVendorDelete.ClientID%>').checked == true) {
                document.getElementById('<%=ChBoxVendorAll.ClientID%>').checked = true;
            }
        }

        function SubCheckSubContractor() {
            if (document.getElementById('<%=chkboxSubContractorCreate.ClientID%>').checked == false
                     || document.getElementById('<%=chkboxSubContractorView.ClientID%>').checked == false
                || document.getElementById('<%=chkboxSubContractorUpdate.ClientID%>').checked == false
                || document.getElementById('<%=chkboxSubContractorDelete.ClientID%>').checked == false) {
                document.getElementById('<%=chkboxSubContractorALL.ClientID%>').checked = false;
            }

            else if (document.getElementById('<%=chkboxSubContractorCreate.ClientID%>').checked == true
                && document.getElementById('<%=chkboxSubContractorView.ClientID%>').checked == true &&
                document.getElementById('<%=chkboxSubContractorUpdate.ClientID%>').checked == true &&
                document.getElementById('<%=chkboxSubContractorDelete.ClientID%>').checked == true) {
                document.getElementById('<%=chkboxSubContractorALL.ClientID%>').checked = true;
            }
        }

        function SubCheckOther() {
            if (document.getElementById('<%=chkboxOtherCreate.ClientID%>').checked == false
                     || document.getElementById('<%=chkboxOtherView.ClientID%>').checked == false
                || document.getElementById('<%=chkboxOtherUpdate.ClientID%>').checked == false
                || document.getElementById('<%=chkboxOtherDelete.ClientID%>').checked == false) {
                document.getElementById('<%=chkboxOtherAll.ClientID%>').checked = false;
            }

            else if (document.getElementById('<%=chkboxOtherCreate.ClientID%>').checked == true
                && document.getElementById('<%=chkboxOtherView.ClientID%>').checked == true &&
                document.getElementById('<%=chkboxOtherUpdate.ClientID%>').checked == true &&
                document.getElementById('<%=chkboxOtherDelete.ClientID%>').checked == true) {
                document.getElementById('<%=chkboxOtherAll.ClientID%>').checked = true;
            }
        }

        function SubCheckBudget() {
            if (document.getElementById('<%=chkBoxBudgetCreate.ClientID%>').checked == false
                     || document.getElementById('<%=chkBoxBudgetView.ClientID%>').checked == false
                || document.getElementById('<%=chkBoxBudgetUpdate.ClientID%>').checked == false
                || document.getElementById('<%=chkBoxBudgetDelete.ClientID%>').checked == false) {
                document.getElementById('<%=chkBoxBudgetAll.ClientID%>').checked = false;
            }
            else if (document.getElementById('<%=chkBoxBudgetCreate.ClientID%>').checked == true
                && document.getElementById('<%=chkBoxBudgetView.ClientID%>').checked == true &&
                document.getElementById('<%=chkBoxBudgetUpdate.ClientID%>').checked == true &&
                document.getElementById('<%=chkBoxBudgetDelete.ClientID%>').checked == true) {
                document.getElementById('<%=chkBoxBudgetAll.ClientID%>').checked = true;
            }
        }

        function SubCheckBudgetModifyRequest() {
            if (document.getElementById('<%=chkboxBudgetModREquestCreate.ClientID%>').checked == false
                      || document.getElementById('<%=chkboxBudgetModREquestView.ClientID%>').checked == false
                || document.getElementById('<%=chkboxBudgetModREquestUpdate.ClientID%>').checked == false
                || document.getElementById('<%=chkboxBudgetModREquestDelete.ClientID%>').checked == false) {
                document.getElementById('<%=chkboxBudgetModREquestALL.ClientID%>').checked = false;
            }
            else if (document.getElementById('<%=chkboxBudgetModREquestCreate.ClientID%>').checked == true
                && document.getElementById('<%=chkboxBudgetModREquestView.ClientID%>').checked == true &&
                document.getElementById('<%=chkboxBudgetModREquestUpdate.ClientID%>').checked == true &&
                document.getElementById('<%=chkboxBudgetModREquestDelete.ClientID%>').checked == true) {
                document.getElementById('<%=chkboxBudgetModREquestALL.ClientID%>').checked = true;
            }
        }

        function SubCheckRLocalMRNequest() {
            if (document.getElementById('<%=chk_Local_MRN_Create.ClientID%>').checked == false
                          || document.getElementById('<%=chk_LocalMRN_View.ClientID%>').checked == false
                || document.getElementById('<%=chk_Local_MRN_Update.ClientID%>').checked == false
                || document.getElementById('<%=chk_Local_MRN_Delete.ClientID%>').checked == false) {
                document.getElementById('<%=chk_Local_MRN_All.ClientID%>').checked = false;
            }
            else if (document.getElementById('<%=chk_Local_MRN_Create.ClientID%>').checked == true
                && document.getElementById('<%=chk_LocalMRN_View.ClientID%>').checked == true &&
                document.getElementById('<%=chk_Local_MRN_Update.ClientID%>').checked == true &&
                document.getElementById('<%=chk_Local_MRN_Delete.ClientID%>').checked == true) {
                document.getElementById('<%=chk_Local_MRN_All.ClientID%>').checked = true;
            }
        }

        function SubCheckProcurement() {
            if (document.getElementById('<%=chkboxQuotationCreate.ClientID%>').checked == false
                       || document.getElementById('<%=chkboxQuotationView.ClientID%>').checked == false
                || document.getElementById('<%=chkboxQuotationUpdate.ClientID%>').checked == false
                || document.getElementById('<%=chkboxQuotationDelete.ClientID%>').checked == false) {
                document.getElementById('<%=chkboxQuotationtAll.ClientID%>').checked = false;
            }
            else if (document.getElementById('<%=chkboxQuotationCreate.ClientID%>').checked == true
                && document.getElementById('<%=chkboxQuotationView.ClientID%>').checked == true &&
                document.getElementById('<%=chkboxQuotationUpdate.ClientID%>').checked == true &&
                document.getElementById('<%=chkboxQuotationDelete.ClientID%>').checked == true) {
                document.getElementById('<%=chkboxQuotationtAll.ClientID%>').checked = true;
            }
        }

        function SubCheckReports1() {
            if (document.getElementById('<%=ChkBoxReportView.ClientID%>').checked == false) {
                document.getElementById('<%=ChkBoxReportALL.ClientID%>').checked = false;
            }
            else if (document.getElementById('<%=ChkBoxReportView.ClientID%>').checked == true) {
                document.getElementById('<%=ChkBoxReportALL.ClientID%>').checked = true;
            }
        }

        function SubCheckQuotationCompare() {
            if (document.getElementById('<%=chkboxQuotationCompare.ClientID%>').checked == false) {
                document.getElementById('<%=CheckBox1.ClientID%>').checked = false;
            }
            else if (document.getElementById('<%=chkboxQuotationCompare.ClientID%>').checked == true) {
                document.getElementById('<%=CheckBox1.ClientID%>').checked = true;
            }
        }

        function SubCheckIndent() {
            if (document.getElementById('<%=chkboxIndentCreate.ClientID%>').checked == false
                || document.getElementById('<%=chkboxIndentView.ClientID%>').checked == false
                || document.getElementById('<%=CheckBoxIndentUpdate.ClientID%>').checked == false
                || document.getElementById('<%=CheckBoxIndentDelete.ClientID%>').checked == false) {
                document.getElementById('<%=chkboxIndentAll.ClientID%>').checked = false;
            }
            else if (document.getElementById('<%=chkboxIndentCreate.ClientID%>').checked == true
                && document.getElementById('<%=chkboxIndentView.ClientID%>').checked == true &&
                document.getElementById('<%=CheckBoxIndentUpdate.ClientID%>').checked == true &&
                document.getElementById('<%=CheckBoxIndentDelete.ClientID%>').checked == true) {
                document.getElementById('<%=chkboxIndentAll.ClientID%>').checked = true;
            }
        }


        function SubCheckPO() {
            if (document.getElementById('<%=ChkBoxPOCreate.ClientID%>').checked == false
                         || document.getElementById('<%=ChkBoxView.ClientID%>').checked == false
                || document.getElementById('<%=ChkBoxPOUpdate.ClientID%>').checked == false
                || document.getElementById('<%=ChkBoxDelete.ClientID%>').checked == false) {
                document.getElementById('<%=ChkBoxPOAll.ClientID%>').checked = false;
            }

            else if (document.getElementById('<%=ChkBoxPOCreate.ClientID%>').checked == true
                && document.getElementById('<%=ChkBoxView.ClientID%>').checked == true &&
                document.getElementById('<%=ChkBoxPOUpdate.ClientID%>').checked == true &&
                document.getElementById('<%=ChkBoxDelete.ClientID%>').checked == true) {
                document.getElementById('<%=ChkBoxPOAll.ClientID%>').checked = true;
            }
        }

        function SubCheck_Approve_PO_Delete() {
            if (document.getElementById('<%=chk_Appr_PO_Delete.ClientID%>').checked == false) {
                document.getElementById('<%=chk_Appr_PO_Delete_All.ClientID%>').checked = false;
            }

            else if (document.getElementById('<%=chk_Appr_PO_Delete.ClientID%>').checked == true) {
                document.getElementById('<%=chk_Appr_PO_Delete_All.ClientID%>').checked = true;
            }
        }

        function SubCheckPayInd() {
            if (document.getElementById('<%=ChkBoxPayIndCreate.ClientID%>').checked == false
                || document.getElementById('<%=ChkBoxPayIndView.ClientID%>').checked == false
                || document.getElementById('<%=ChkBoxPayIndUpdate.ClientID%>').checked == false
                || document.getElementById('<%=ChkBoxPayIndDelete.ClientID%>').checked == false) {
                document.getElementById('<%=ChkBoxPayIndAll.ClientID%>').checked = false;
            }

            else if (document.getElementById('<%=ChkBoxPayIndCreate.ClientID%>').checked == true
                && document.getElementById('<%=ChkBoxPayIndView.ClientID%>').checked == true &&
                document.getElementById('<%=ChkBoxPayIndUpdate.ClientID%>').checked == true &&
                document.getElementById('<%=ChkBoxPayIndDelete.ClientID%>').checked == true) {
                document.getElementById('<%=ChkBoxPayIndAll.ClientID%>').checked = true;
            }
        }

        function SubCheckPayVerInd() {
            if (document.getElementById('<%=ChkBoxPayIndVerCreate.ClientID%>').checked == false
                || document.getElementById('<%=ChkBoxPayIndVerView.ClientID%>').checked == false
                || document.getElementById('<%=ChkBoxPayIndVerUpdate.ClientID%>').checked == false
                || document.getElementById('<%=ChkBoxPayIndVerDelete.ClientID%>').checked == false) {
                document.getElementById('<%=ChkBoxPayIndVerAll.ClientID%>').checked = false;
            }

            else if (document.getElementById('<%=ChkBoxPayIndVerCreate.ClientID%>').checked == true
                && document.getElementById('<%=ChkBoxPayIndVerView.ClientID%>').checked == true &&
                document.getElementById('<%=ChkBoxPayIndVerUpdate.ClientID%>').checked == true &&
                document.getElementById('<%=ChkBoxPayIndVerDelete.ClientID%>').checked == true) {
                document.getElementById('<%=ChkBoxPayIndVerAll.ClientID%>').checked = true;
            }
        }

        function SubCheckPayAppInd() {
            if (document.getElementById('<%=ChkBoxPayIndAppCreate.ClientID%>').checked == false
                || document.getElementById('<%=ChkBoxPayIndAppView.ClientID%>').checked == false
                || document.getElementById('<%=ChkBoxPayIndAppUpdate.ClientID%>').checked == false
                || document.getElementById('<%=ChkBoxPayIndAppDelete.ClientID%>').checked == false) {
                document.getElementById('<%=ChkBoxPayIndAppAll.ClientID%>').checked = false;
            }

            else if (document.getElementById('<%=ChkBoxPayIndAppCreate.ClientID%>').checked == true
                && document.getElementById('<%=ChkBoxPayIndAppView.ClientID%>').checked == true &&
                document.getElementById('<%=ChkBoxPayIndAppUpdate.ClientID%>').checked == true &&
                document.getElementById('<%=ChkBoxPayIndAppDelete.ClientID%>').checked == true) {
                document.getElementById('<%=ChkBoxPayIndAppAll.ClientID%>').checked = true;
            }
        }

    function SubCheckReports2() {
        if (document.getElementById('<%=chkboxPOReportView.ClientID%>').checked == false) {
                document.getElementById('<%=chkboxPOReportAll.ClientID%>').checked = false;
            }
            else if (document.getElementById('<%=chkboxPOReportView.ClientID%>').checked == true) {
                document.getElementById('<%=chkboxPOReportAll.ClientID%>').checked = true;
            }
    }


    function SubCheckInventory() {
        if (document.getElementById('<%=chkboxStockCreate.ClientID%>').checked == false
                         || document.getElementById('<%=chkboxStockView.ClientID%>').checked == false
                || document.getElementById('<%=chkboxStockUpdate.ClientID%>').checked == false
                || document.getElementById('<%=chkboxStockDelete.ClientID%>').checked == false) {
                document.getElementById('<%=chkboxStockAll.ClientID%>').checked = false;
            }

            else if (document.getElementById('<%=chkboxStockCreate.ClientID%>').checked == true
                && document.getElementById('<%=chkboxStockView.ClientID%>').checked == true &&
                document.getElementById('<%=chkboxStockUpdate.ClientID%>').checked == true &&
                document.getElementById('<%=chkboxStockDelete.ClientID%>').checked == true) {
                document.getElementById('<%=chkboxStockAll.ClientID%>').checked = true;
            }
    }



    function SubCheckMRN() {
        if (document.getElementById('<%=ChkBoxMRNCreate.ClientID%>').checked == false
                          || document.getElementById('<%=ChkBoxMRNView.ClientID%>').checked == false
                || document.getElementById('<%=ChkBoxMRNUpdate.ClientID%>').checked == false
                || document.getElementById('<%=ChkBoxMRNDelete.ClientID%>').checked == false) {
                document.getElementById('<%=chkboxMRNAll.ClientID%>').checked = false;
            }

            else if (document.getElementById('<%=ChkBoxMRNCreate.ClientID%>').checked == true
                && document.getElementById('<%=ChkBoxMRNView.ClientID%>').checked == true &&
                document.getElementById('<%=ChkBoxMRNUpdate.ClientID%>').checked == true &&
                document.getElementById('<%=ChkBoxMRNDelete.ClientID%>').checked == true) {
                document.getElementById('<%=chkboxMRNAll.ClientID%>').checked = true;
            }
    }
    function SubCheckStockTransfer() {
        if (document.getElementById('<%=chkboxStockTransferCreate.ClientID%>').checked == false
                            || document.getElementById('<%=chkboxStockTransferView.ClientID%>').checked == false
                || document.getElementById('<%=chkboxStockTransferUpdate.ClientID%>').checked == false
                || document.getElementById('<%=chkboxStockTransferDelete.ClientID%>').checked == false) {
                document.getElementById('<%=chkboxStockTransferAll.ClientID%>').checked = false;
            }

            else if (document.getElementById('<%=chkboxStockTransferCreate.ClientID%>').checked == true
                && document.getElementById('<%=chkboxStockTransferView.ClientID%>').checked == true &&
                document.getElementById('<%=chkboxStockTransferUpdate.ClientID%>').checked == true &&
                document.getElementById('<%=chkboxStockTransferDelete.ClientID%>').checked == true) {
                document.getElementById('<%=chkboxStockTransferAll.ClientID%>').checked = true;
            }
    }

    function SubCheckMIN() {
        if (document.getElementById('<%=ChkBoxMINCreate.ClientID%>').checked == false
                           || document.getElementById('<%=ChkBoxMINView.ClientID%>').checked == false
                || document.getElementById('<%=ChkBoxMINUpdate.ClientID%>').checked == false
                || document.getElementById('<%=ChkBoxMINDelete.ClientID%>').checked == false) {
                document.getElementById('<%=ChkBoxMIN_All.ClientID%>').checked = false;
            }

            else if (document.getElementById('<%=ChkBoxMRNCreate.ClientID%>').checked == true
                && document.getElementById('<%=ChkBoxMRNView.ClientID%>').checked == true &&
                document.getElementById('<%=ChkBoxMRNUpdate.ClientID%>').checked == true &&
                document.getElementById('<%=ChkBoxMRNDelete.ClientID%>').checked == true) {
                document.getElementById('<%=ChkBoxMIN_All.ClientID%>').checked = true;
            }
    }

    function SubCheckReports3() {
        if (document.getElementById('<%=ChkBoxINVReportsView.ClientID%>').checked == false) {
                document.getElementById('<%=ChkBoxINVReportsAll.ClientID%>').checked = false;
            }
            else if (document.getElementById('<%=ChkBoxINVReportsView.ClientID%>').checked == true) {
                document.getElementById('<%=ChkBoxINVReportsAll.ClientID%>').checked = true;
            }
    }


    function SubCheckAssetRegistration() {
        if (document.getElementById('<%=ChkBoxAssetRegCreate.ClientID%>').checked == false
                            || document.getElementById('<%=ChkBoxAssetRegView.ClientID%>').checked == false
                || document.getElementById('<%=ChkBoxAssetRegUpdate.ClientID%>').checked == false
                || document.getElementById('<%=ChkBoxAssetRegDelete.ClientID%>').checked == false) {
                document.getElementById('<%=ChkBoxAssetRegAll.ClientID%>').checked = false;
            }

            else if (document.getElementById('<%=ChkBoxAssetRegCreate.ClientID%>').checked == true
                && document.getElementById('<%=ChkBoxAssetRegView.ClientID%>').checked == true &&
                document.getElementById('<%=ChkBoxAssetRegUpdate.ClientID%>').checked == true &&
                document.getElementById('<%=ChkBoxAssetRegDelete.ClientID%>').checked == true) {
                document.getElementById('<%=ChkBoxAssetRegAll.ClientID%>').checked = true;
            }
    }


    function SubCheckAssetTransfer() {
        if (document.getElementById('<%=ChkBoxAssetTransferCreate.ClientID%>').checked == false
                            || document.getElementById('<%=ChkBoxAssetTransferView.ClientID%>').checked == false
                || document.getElementById('<%=ChkBoxAssetTransferUpdate.ClientID%>').checked == false
                || document.getElementById('<%=ChkBoxAssetTransferDelete.ClientID%>').checked == false) {
                document.getElementById('<%=ChkBoxAssetTransferAll.ClientID%>').checked = false;
            }

            else if (document.getElementById('<%=ChkBoxAssetTransferCreate.ClientID%>').checked == true
                && document.getElementById('<%=ChkBoxAssetTransferView.ClientID%>').checked == true &&
                document.getElementById('<%=ChkBoxAssetTransferUpdate.ClientID%>').checked == true &&
                document.getElementById('<%=ChkBoxAssetTransferDelete.ClientID%>').checked == true) {
                document.getElementById('<%=ChkBoxAssetTransferAll.ClientID%>').checked = true;
            }
    }


    function SubCheckDailyRunningHoursKms() {
        if (document.getElementById('<%=chkboxDailyRunHourKmCreate.ClientID%>').checked == false
                             || document.getElementById('<%=chkboxDailyRunHourKmView.ClientID%>').checked == false
                || document.getElementById('<%=chkboxDailyRunHourKmUpdate.ClientID%>').checked == false
                || document.getElementById('<%=chkboxDailyRunHourKmDelete.ClientID%>').checked == false) {
                document.getElementById('<%=chkboxDailyRunHourKmAll.ClientID%>').checked = false;
            }

            else if (document.getElementById('<%=chkboxDailyRunHourKmCreate.ClientID%>').checked == true
                && document.getElementById('<%=chkboxDailyRunHourKmView.ClientID%>').checked == true &&
                document.getElementById('<%=chkboxDailyRunHourKmUpdate.ClientID%>').checked == true &&
                document.getElementById('<%=chkboxDailyRunHourKmDelete.ClientID%>').checked == true) {
                document.getElementById('<%=chkboxDailyRunHourKmAll.ClientID%>').checked = true;
            }
    }



    function SubCheckReports4() {
        if (document.getElementById('<%=ChkBoxAssetReportsView.ClientID%>').checked == false) {
                document.getElementById('<%=ChkBoxAssetReportsAll.ClientID%>').checked = false;
            }
            else if (document.getElementById('<%=ChkBoxAssetReportsView.ClientID%>').checked == true) {
                document.getElementById('<%=ChkBoxAssetReportsAll.ClientID%>').checked = true;
            }
    }



    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.chosen-select').chosen();
        });

    </script>
   
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                User Details
               
            </h3>

        </div>

        <div class="panel-body">

            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-2">
                    Name&nbsp;

                    <asp:RequiredFieldValidator ID="rfName" runat="server" CssClass="Validation_Text" ErrorMessage="*" ControlToValidate="Tb_name" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="Tb_name" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    Employee ID&nbsp;
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="Validation_Text" ErrorMessage="*" ControlToValidate="Tb_empID" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="Tb_empID" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                </div>
            </div>
            <div class="row">

                <div class="col-md-2">
                    Department&nbsp;
                  <asp:RequiredFieldValidator ID="RequiredFieldValidatorDEPT" runat="server" CssClass="Validation_Text" ErrorMessage="*" InitialValue="-Select-" ControlToValidate="ddlDepartment" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    <a href="#myModal1" data-toggle="modal" role="button">
                        <asp:ImageButton ID="imgBtnDept" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                    </a>


                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlDepartment" class="chosen-select form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" TabIndex="3">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Designation&nbsp;&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDESIG" runat="server" CssClass="Validation_Text" ErrorMessage="*" InitialValue="-Select-" ControlToValidate="ddlDesignation" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    <a href="#myModal" data-toggle="modal" role="button">
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                    </a>


                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlDesignation" class="chosen-select form-control"  TabIndex="4">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>

            </div>





            <div class="row" id="emailstatus" runat="server">
                <div class="col-md-2">
                    Role                     
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlRole" CssClass="form-control" TabIndex="5">
                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Application Admin" Value="Application Admin"></asp:ListItem>
                        <asp:ListItem Text="Other" Value="Other"></asp:ListItem>


                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Email ID&nbsp;
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save" Display="Dynamic" runat="server" CssClass="Validation_Text" ErrorMessage="*" ControlToValidate="txtEmailid"></asp:RequiredFieldValidator>
                    <br />

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"
                        ControlToValidate="txtEmailid" CssClass="Validation_Text"
                        ErrorMessage="Enter Valid Email" Display="Dynamic"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.([com in org net])+$">Enter Valid Email</asp:RegularExpressionValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtEmailid" runat="server" CssClass="form-control" TabIndex="6"></asp:TextBox>
                </div>

            </div>
           
            <div class="row" id="up" runat="server">
                <div class="col-md-2">
                    Username
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="Validation_Text" ErrorMessage="*" ControlToValidate="txtUsername" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" TabIndex="8"></asp:TextBox>
                </div>
                <div class="col-md-2" id="passlabel" runat="server">
                    Password &nbsp;
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Save" runat="server" CssClass="Validation_Text" ErrorMessage="*" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                    <asp:ImageButton ID="imgpassword" runat="server" ImageUrl="~/Images/Generate-keys-icon.png" Height="20px" Width="20px" OnClick="imgpassword_Click" />
                </div>
                <div class="col-md-4" id="passtext" runat="server">
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>

                </div>

            </div>
             <div class="row">

                <div class="col-md-2">
                    Assign To Project


                   <asp:RequiredFieldValidator ID="RequiredFieldValidatorer" runat="server" CssClass="Validation_Text" InitialValue="-Select-" ErrorMessage="*" ControlToValidate="lstAssignToProject" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                  <%--  OnSelectedIndexChanged="ddlAssignToProject_SelectedIndexChanged1" AutoPostBack="true"--%>
                    <asp:ListBox runat="server" ID="lstAssignToProject" SelectionMode="Multiple" CssClass="form-control" TabIndex="10" Rows="5"></asp:ListBox>
                   <%-- <asp:DropDownList runat="server"  ID="ddlAssignToProject" SelectionMode="Multiple" class="form-control" TabIndex="10" Rows="5"  >--%>
                  <%--  </asp:DropDownList>--%>
                    <asp:HiddenField ID="hdfProjectSelectedValue" runat="server" />
                </div>
                <div class="col-md-2">
                    Project Code&nbsp;
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" CssClass="Validation_Text" ErrorMessage="*" ControlToValidate="txtProjectcode" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtProjectcode" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    Status&nbsp;
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Save" runat="server" CssClass="Validation_Text" ErrorMessage="*" ControlToValidate="rblStatus"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="rblStatus" runat="server" CssClass="radiostyle" RepeatDirection="Horizontal" TabIndex="9">

                        <asp:ListItem Value="Active" Selected="True" Text="Active&nbsp;"></asp:ListItem>
                        <asp:ListItem Value="InActive" Text="InActive"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="col-md-2">
                    Is HO User &nbsp;
                </div>
                <div class="col-md-4">
                    <asp:CheckBox ID="chkboxIsHoUser" runat="server" TabIndex="11" />


                </div>


            </div>
            <%--<div class="row">
                <div class="col-md-2">
                    Is CFO User &nbsp;
                </div>
                <div class="col-md-4">
                    <asp:CheckBox ID="chkIsCFOUser" runat="server" AutoPostBack="true" TabIndex="12" 
                        OnCheckedChanged="chkIsCFOUser_CheckedChanged"/>


                </div>
            </div>--%>
            <div class="row">
                <div class="col-md-2">
                    Digital sign image upload
                </div>
                <div class="col-md-4">
                    <asp:FileUpload runat="server" ID="SignUploader" />
                </div>
                <div class="col-md-4">
                    <asp:ImageButton ID="imgBtnDigitalSign" Width="150px" Height="80px" value=" " Visible="false" OnClick="imgBtnDigitalSign_Click" runat="server" />

                </div>
            </div>

            <br />
            <center>
                <table   width="60%"  class="table-condensed"  border="1"  >
                    <tr>
                        <td colspan="4" style="font-weight:bold;background-color:#b42525;color:white; text-align:center">Module Access</td>                        
                    </tr>
                    
                    <tr>                        
                        <th rowspan="2">Module</th>
                        <th rowspan="2">Page</th>
                        <th rowspan="2">All</th>
                        <th style="text-align:center" class="auto-style1">Action</th>
                    </tr>
                    
                    <tr>                        
                        <td>
                            <table>
                                <tr>
                                    <th> Create&nbsp;&nbsp;</th>
                                    <th> View&nbsp;&nbsp;</th>
                                    <th> Update&nbsp;&nbsp;</th>
                                    <th> Delete&nbsp;&nbsp;</th>
                                </tr>
                            </table>
                        </td>
                    </tr>
                  
                    <tr>
                        <td rowspan="3"><b>Admin</b></td>
                        <td>Users</td> 
                        <td><asp:CheckBox ID="chkboxUserAll" runat="server" onclick="chkboxUserAll();"></asp:CheckBox></td>
                        <td>
                            <table>                                                                 
                                <tr>
                                    <td class="chkbox"><asp:CheckBox ID="chkboxUserCreate"  runat="server" onclick="SubCheckUser();"></asp:CheckBox></td>
                                    <td class="chkbox"><asp:CheckBox ID="chkboxUserview"  runat="server" onclick="SubCheckUser();" ></asp:CheckBox></td>
                                    <td class="chkbox"><asp:CheckBox ID="chkboxUserupdate"  runat="server" onclick="SubCheckUser();" ></asp:CheckBox></td>
                                    <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkboxUserdelete"  runat="server" onclick="SubCheckUser();"></asp:CheckBox></td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td>Employee Contract</td> 
                        <td><asp:CheckBox ID="chkboxEmpContractAll" runat="server" onclick="chkboxEmpContractAll();"></asp:CheckBox></td>
                        <td>
                            <table>                                                                 
                                <tr>
                                    <td class="chkbox"><asp:CheckBox ID="chkboxEmpContractCreate"  runat="server" onclick="SubCheckEmpContract();"></asp:CheckBox></td>
                                    <td class="chkbox"><asp:CheckBox ID="chkboxEmpContractView"  runat="server" onclick="SubCheckEmpContract();" ></asp:CheckBox></td>
                                    <td class="chkbox"><asp:CheckBox ID="chkboxEmpContractUpdate"  runat="server" onclick="SubCheckEmpContract();" ></asp:CheckBox></td>
                                    <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkboxEmpContractDelete"  runat="server" onclick="SubCheckEmpContract();"></asp:CheckBox></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                           
                    <tr>
                        <td>
                            Mail Config
                        </td>
                        <td><asp:CheckBox ID="chkboxMailconfigAll" runat="server" onclick="chkboxMailconfigAll();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>
                                    <td class="chkbox">&nbsp;&nbsp;</td>
                                    <td class="chkbox">&nbsp;&nbsp;</td>
                                    <td class="chkbox"><asp:CheckBox ID="chkboxMailConfigUpdate" runat="server" onclick="SubCheckMail();"></asp:CheckBox></td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td>&nbsp;&nbsp;</td>
                                </tr>
                            </table>    
                        </td>
                    </tr>
                   
                    <tr>
                        <td rowspan="7"><b>Master</b></td>
                        <td>Company &amp; Site</td>
                        <td><asp:CheckBox ID="chckboxAllComSite" runat="server" onclick="chckboxAllComSite();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>
                                    <td class="chkbox"><asp:CheckBox ID="chckboxCreateComSite" runat="server" onclick="SubCheckSite();"></asp:CheckBox></td>
                                    <td class="chkbox"><asp:CheckBox ID="chckboxViewComSite" runat="server" onclick="SubCheckSite();"></asp:CheckBox></td>
                                    <td class="chkbox"><asp:CheckBox ID="chckboxUpdateComSite" runat="server" onclick="SubCheckSite();"></asp:CheckBox></td>
                                    <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chckboxDeleteComSite" runat="server" onclick="SubCheckSite();"></asp:CheckBox></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            Projects
                        </td>
                        <td><asp:CheckBox ID="ChkboxAllProject" runat="server" onclick="ChkboxAllProject();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>
                                    <td class="chkbox"><asp:CheckBox ID="ChkboxCreateProject" runat="server" onclick="SubCheckProjects();"></asp:CheckBox></td>
                                    <td class="chkbox"><asp:CheckBox ID="ChkboxViewProject" runat="server" onclick="SubCheckProjects();"></asp:CheckBox></td>
                                    <td class="chkbox"><asp:CheckBox ID="ChkboxUpdateProject" runat="server" onclick="SubCheckProjects();"></asp:CheckBox></td>
                                    <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="ChkboxDeleteProject" runat="server" onclick="SubCheckProjects();"></asp:CheckBox></td>
                                </tr>
                            </table>    
                        </td>
                    </tr>
                    
                    <tr>
                        <td>UOM</td>
                        <td><asp:CheckBox ID="ChkBoxAllUom" runat="server" onclick="ChkBoxAllUom();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>
                                    <td class="chkbox"><asp:CheckBox ID="ChkBoxCreateUom" runat="server" onclick="SubCheckUOM();" ></asp:CheckBox></td>
                                    <td class="chkbox"><asp:CheckBox ID="ChkBoxViewUom" runat="server" onclick="SubCheckUOM();"></asp:CheckBox></td>
                                    <td class="chkbox"><asp:CheckBox ID="ChkBoxUpdateUom" runat="server" onclick="SubCheckUOM();"></asp:CheckBox></td>
                                    <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="ChkBoxDeleteUom" runat="server" onclick="SubCheckUOM();"></asp:CheckBox></td>
                                </tr>
                            </table>    
                        </td>
                    </tr>

                    <tr>
                         <td>Material</td>
                        <td><asp:CheckBox ID="chkboxMaterialAll" runat="server" onclick="chkboxMaterialAll();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>

                                    <td class="chkbox"><asp:CheckBox ID="chkboxMateriaCreate" runat="server" onclick="SubCheckMaterial();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="chkboxMateriaView" runat="server" onclick="SubCheckMaterial();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="chkboxMateriaUpdate" runat="server" onclick="SubCheckMaterial();"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkboxMaterialDelete" runat="server" onclick="SubCheckMaterial();"></asp:CheckBox></td>
                                    
                                  
                                </tr>
                            </table>
                        
                        </td>
                    </tr>
                    
                    <tr>
                       <td>
                           Vendor 
                       </td>
                         <td><asp:CheckBox ID="ChBoxVendorAll" runat="server" onclick="ChBoxVendorAll();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>

                                    <td class="chkbox"><asp:CheckBox ID="ChBoxVendorCreate" runat="server"  onclick="SubCheckVendor();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="ChBoxVendorView" runat="server" onclick="SubCheckVendor();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="ChBoxVendorUpdate" runat="server" onclick="SubCheckVendor();"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="ChBoxVendorDelete" runat="server" onclick="SubCheckVendor();"></asp:CheckBox></td>
                                    
                                </tr>
                            </table>    
                        
                        </td>
                    </tr>
                    
                    <tr>
                       <td>Sub-Contractor</td>
                         <td><asp:CheckBox ID="chkboxSubContractorALL" runat="server" onclick="chkboxSubContractorALL();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>
                                    <td class="chkbox"><asp:CheckBox ID="chkboxSubContractorCreate" runat="server" onclick="SubCheckSubContractor();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="chkboxSubContractorView" runat="server" onclick="SubCheckSubContractor();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="chkboxSubContractorUpdate" runat="server" onclick="SubCheckSubContractor();"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkboxSubContractorDelete" runat="server" onclick="SubCheckSubContractor();"></asp:CheckBox></td>
                                </tr>
                            </table>    
                        </td>
                    </tr>

                    <tr>
                       <td>Other</td>
                         <td><asp:CheckBox ID="chkboxOtherAll" runat="server" onclick="chkboxOtherALL();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>
                                    <td class="chkbox"><asp:CheckBox ID="chkboxOtherCreate" runat="server" onclick="SubCheckOther();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="chkboxOtherView" runat="server" onclick="SubCheckOther();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="chkboxOtherUpdate" runat="server" onclick="SubCheckOther();"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkboxOtherDelete" runat="server" onclick="SubCheckOther();"></asp:CheckBox></td>
                                </tr>
                            </table>    
                        </td>
                    </tr>

                       <tr>
                        <td rowspan="4"><b> Budget</b></td>
                        <td>
                          Budget
                       </td>
                         <td><asp:CheckBox ID="chkBoxBudgetAll" runat="server" onclick="chkBoxBudgetAll();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>
                                     <td class="chkbox"><asp:CheckBox ID="chkBoxBudgetCreate" runat="server" onclick="SubCheckBudget();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="chkBoxBudgetView" onclick="SubCheckBudget();" runat="server"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="chkBoxBudgetUpdate" onclick="SubCheckBudget();"  runat="server"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkBoxBudgetDelete" onclick="SubCheckBudget();"  runat="server"></asp:CheckBox></td>
                                    
                                </tr>
                            </table>    
                        
                        </td>
                    </tr>
                   
                     <tr>
                       <td>
                          Project Budget
                       </td>
                         <td><asp:CheckBox ID="ChkProBudgetAll" runat="server" onclick="ChkboxAllProjectBudget();" ></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>

                                   <td class="chkbox"><asp:CheckBox ID="ChkProBudgetCreate" runat="server" onclick="SubCheckProjectBudget();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="ChkProBudgetView" onclick="SubCheckProjectBudget();" runat="server"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="ChkProBudgetUpdate"  onclick="SubCheckProjectBudget();" runat="server"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="ChkProBudgetDelete" onclick="SubCheckProjectBudget();" runat="server"></asp:CheckBox></td>
                                    
                                </tr>
                            </table>    
                        
                        </td>
                    </tr>
                    <tr>
                       <td>
                           Budget Modify Request 
                       </td>
                         <td><asp:CheckBox ID="chkboxBudgetModREquestALL" runat="server" onclick="chkboxBudgetModREquestALL();" ></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>

                                   <td class="chkbox"><asp:CheckBox ID="chkboxBudgetModREquestCreate" runat="server" onclick="SubCheckBudgetModifyRequest();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="chkboxBudgetModREquestView" onclick="SubCheckBudgetModifyRequest();" runat="server"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="chkboxBudgetModREquestUpdate"  onclick="SubCheckBudgetModifyRequest();" runat="server"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkboxBudgetModREquestDelete" onclick="SubCheckBudgetModifyRequest();" runat="server"></asp:CheckBox></td>
                                    
                                </tr>
                            </table>    
                        
                        </td>
                    </tr>
                                        <tr>
                       <td>
                           Local MRN 
                       </td>
                         <td><asp:CheckBox ID="chk_Local_MRN_All" runat="server" onclick="chkbox_Local_MRN_ALL();" ></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>

                                   <td class="chkbox"><asp:CheckBox ID="chk_Local_MRN_Create" runat="server" onclick="SubCheckRLocalMRNequest();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="chk_LocalMRN_View" onclick="SubCheckRLocalMRNequest();" runat="server"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="chk_Local_MRN_Update"  onclick="SubCheckRLocalMRNequest();" runat="server"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chk_Local_MRN_Delete" onclick="SubCheckRLocalMRNequest();" runat="server"></asp:CheckBox></td>
                                    
                                </tr>
                            </table>    
                        
                        </td>
                    </tr>
                     <tr>
                       <td>
                           Reports
                       </td>
                         <td><asp:CheckBox ID="ChkBoxReportALL" runat="server" onclick="ChkBoxReportALL();" ></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>
                                    <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                     <td class="chkbox"><asp:CheckBox ID="ChkBoxReportView" runat="server" onclick="SubCheckReports1();"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                </tr>
                            </table>    
                        
                        </td>
                    </tr>
              
                  
                 <tr>
                        <td rowspan="8"><b>Procurement</b></td>
                        <td>Quotation</td>
                        <td><asp:CheckBox ID="chkboxQuotationtAll" runat="server" onclick="chkboxQuotationtAll();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>

                                     <td class="chkbox"><asp:CheckBox ID="chkboxQuotationCreate" runat="server" onclick="SubCheckProcurement();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="chkboxQuotationView" runat="server" onclick="SubCheckProcurement();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="chkboxQuotationUpdate" runat="server" onclick="SubCheckProcurement();"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkboxQuotationDelete" runat="server" onclick="SubCheckProcurement();"></asp:CheckBox></td>
                                </tr>
                            </table>
                        
                        </td>
                    </tr>
                    <tr>
                       <td>
                           Quotation Compare</td>
                         <td><asp:CheckBox ID="CheckBox1" runat="server" onclick="CheckBox1();" ></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>
                                    <td class="chkbox"></td>
                                     <td class="chkbox"><asp:CheckBox ID="chkboxQuotationCompare" runat="server"  onclick="SubCheckQuotationCompare();" ></asp:CheckBox></td>
                                     <td class="chkbox"></td>
                                     <td class="chkbox"></td>
                                    
                                   
                                </tr>
                            </table>    
                        
                        </td>
                    </tr>
                    
                    <tr>
                       <td>
                           Indent
                       </td>
                         <td><asp:CheckBox ID="chkboxIndentAll" runat="server" onclick="chkboxIndentAll();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>

                                     <td class="chkbox"><asp:CheckBox ID="chkboxIndentCreate" runat="server" onclick="SubCheckIndent();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="chkboxIndentView" runat="server" onclick="SubCheckIndent();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="CheckBoxIndentUpdate" runat="server" onclick="SubCheckIndent();"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="CheckBoxIndentDelete" runat="server" onclick="SubCheckIndent();"></asp:CheckBox></td>
                                </tr>
                            </table>    
                        
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                           PO
                        </td>
                        <td><asp:CheckBox ID="ChkBoxPOAll" runat="server" onclick="ChkBoxPOAll();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>
                                    <td class="chkbox"><asp:CheckBox ID="ChkBoxPOCreate" runat="server" onclick="SubCheckPO();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="ChkBoxView" runat="server" onclick="SubCheckPO();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="ChkBoxPOUpdate" runat="server" onclick="SubCheckPO();"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="ChkBoxDelete" runat="server" onclick="SubCheckPO();"></asp:CheckBox></td>
                                </tr>
                            </table>    
                        
                        </td>
                    </tr>

                    <tr>
                       <td>
                           Approved PO Delete
                       </td>
                         <td><asp:CheckBox ID="chk_Appr_PO_Delete_All" runat="server" onclick="ChkBoxApprove_PO_Delete_All();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>

                                    <td class="chkbox"></td>
                                     <td class="chkbox"></td>
                                     <td class="chkbox"></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chk_Appr_PO_Delete" runat="server" onclick="SubCheck_Approve_PO_Delete();"></asp:CheckBox></td>
                                </tr>
                            </table>    
                        
                        </td>
                    </tr>

                    <tr>
                        <td>
                           Payment Indent
                       </td>
                         <td><asp:CheckBox ID="ChkBoxPayIndAll" runat="server" onclick="ChkBoxPayIndAll();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>
                                    <td class="chkbox"><asp:CheckBox ID="ChkBoxPayIndCreate" runat="server" onclick="SubCheckPayInd();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="ChkBoxPayIndView" runat="server" onclick="SubCheckPayInd();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="ChkBoxPayIndUpdate" runat="server" onclick="SubCheckPayInd();"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="ChkBoxPayIndDelete" runat="server" onclick="SubCheckPayInd();"></asp:CheckBox></td>
                                </tr>
                            </table>    
                        
                        </td>
                    </tr>

                    <tr>
                        <td>
                           Payment Indent Verification
                       </td>
                         <td><asp:CheckBox ID="ChkBoxPayIndVerAll" runat="server" onclick="ChkBoxPayIndVerAll();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>
                                    <td class="chkbox"><asp:CheckBox ID="ChkBoxPayIndVerCreate" runat="server" onclick="SubCheckPayIndVer();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="ChkBoxPayIndVerView" runat="server" onclick="SubCheckPayIndVer();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="ChkBoxPayIndVerUpdate" runat="server" onclick="SubCheckPayIndVer();"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="ChkBoxPayIndVerDelete" runat="server" onclick="SubCheckPayIndVer();"></asp:CheckBox></td>
                                </tr>
                            </table>    
                        
                        </td>
                    </tr>

                    <tr>
                        <td>
                           Payment Indent Approval
                       </td>
                         <td><asp:CheckBox ID="ChkBoxPayIndAppAll" runat="server" onclick="ChkBoxPayIndAppAll();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>
                                    <td class="chkbox"><asp:CheckBox ID="ChkBoxPayIndAppCreate" runat="server" onclick="SubCheckPayIndApp();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="ChkBoxPayIndAppView" runat="server" onclick="SubCheckPayIndApp();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="ChkBoxPayIndAppUpdate" runat="server" onclick="SubCheckPayIndApp();"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="ChkBoxPayIndAppDelete" runat="server" onclick="SubCheckPayIndApp();"></asp:CheckBox></td>
                                </tr>
                            </table>    
                        
                        </td>
                    </tr>

                    <tr>
                       <td>
                           Reports
                       </td>
                         <td><asp:CheckBox ID="chkboxPOReportAll" runat="server" onclick="chkboxPOReportAll();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>

                                    <td class="chkbox">&nbsp;</td>
                                     <td class="chkbox"><asp:CheckBox ID="chkboxPOReportView" runat="server" onclick="SubCheckReports2();"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                     <td class="chkbox">&nbsp;</td>
                                </tr>
                            </table>    
                        
                        </td>
                    </tr>


                    <tr>
                        <td rowspan="5"><b>Inventory</b></td>
                        <td>Stock</td>
                        <td><asp:CheckBox ID="chkboxStockAll" runat="server" onclick="chkboxStockAll();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>

                                     <td class="chkbox"><asp:CheckBox ID="chkboxStockCreate" runat="server" onclick="SubCheckInventory();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="chkboxStockView" runat="server" onclick="SubCheckInventory();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="chkboxStockUpdate" runat="server" onclick="SubCheckInventory();"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkboxStockDelete" runat="server" onclick="SubCheckInventory();"></asp:CheckBox></td>
                                </tr>
                            </table>
                        
                        </td>
                    </tr>
                  
                    <tr>
                        <td>Stock Transfer</td>
                        <td><asp:CheckBox ID="chkboxStockTransferAll" runat="server" onclick="chkboxStockTransferAll();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>

                                     <td class="chkbox"><asp:CheckBox ID="chkboxStockTransferCreate" runat="server" onclick="SubCheckStockTransfer();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="chkboxStockTransferView" runat="server" onclick="SubCheckStockTransfer();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="chkboxStockTransferUpdate" runat="server" onclick="SubCheckStockTransfer();"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkboxStockTransferDelete" runat="server" onclick="SubCheckStockTransfer();"></asp:CheckBox></td>
                                </tr>
                            </table>
                        
                        </td>
                    </tr>
                    <tr>
                       <td>
                           MRN
                       </td>
                         <td><asp:CheckBox ID="chkboxMRNAll" runat="server" onclick="chkboxMRNAll();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>

                                    <td class="chkbox"><asp:CheckBox ID="ChkBoxMRNCreate" runat="server" onclick="SubCheckMRN();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="ChkBoxMRNView" runat="server" onclick="SubCheckMRN();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="ChkBoxMRNUpdate" runat="server" onclick="SubCheckMRN();"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="ChkBoxMRNDelete" runat="server" onclick="SubCheckMRN();"></asp:CheckBox></td>
                                </tr>
                            </table>    
                        
                        </td>
                    </tr>
                    
                    <tr>
                       <td>
                           MIN
                       </td>
                         <td><asp:CheckBox ID="ChkBoxMIN_All" runat="server" onclick="ChkBoxMIN_All();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>

                                    <td class="chkbox"><asp:CheckBox ID="ChkBoxMINCreate" runat="server" onclick="SubCheckMIN();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="ChkBoxMINView" runat="server" onclick="SubCheckMIN();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="ChkBoxMINUpdate" runat="server" onclick="SubCheckMIN();"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="ChkBoxMINDelete" runat="server" onclick="SubCheckMIN();"></asp:CheckBox></td>
                                </tr>
                            </table>    
                        
                        </td>
                    </tr>
                     
                    <tr>
                        <td>
                           Reports
                       </td>
                         <td><asp:CheckBox ID="ChkBoxINVReportsAll" runat="server" onclick="ChkBoxINVReportsAll();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>

                                   <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                     <td class="chkbox"><asp:CheckBox ID="ChkBoxINVReportsView" onclick="SubCheckReports3();" runat="server"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                </tr>
                            </table>    
                        
                        </td>
                    </tr>
                  
                    <tr>
                        <td rowspan="4"><b>Asset</b></td>
                        <td>Asset Registration</td>
                        <td><asp:CheckBox ID="ChkBoxAssetRegAll" runat="server" onclick="ChkBoxAssetRegAll();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>

                                   <td class="chkbox"><asp:CheckBox ID="ChkBoxAssetRegCreate" runat="server" onclick="SubCheckAssetRegistration();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="ChkBoxAssetRegView" onclick="SubCheckAssetRegistration();" runat="server"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="ChkBoxAssetRegUpdate" onclick="SubCheckAssetRegistration();" runat="server"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="ChkBoxAssetRegDelete" onclick="SubCheckAssetRegistration();" runat="server"></asp:CheckBox></td>
                                </tr>
                            </table>
                        
                        </td>
                    </tr>
                    <tr>
                       <td>
                           Asset Transfer
                       </td>
                         <td><asp:CheckBox ID="ChkBoxAssetTransferAll" runat="server" onclick="ChkBoxAssetTransferAll();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>
                                     <td class="chkbox"><asp:CheckBox ID="ChkBoxAssetTransferCreate" runat="server" onclick="SubCheckAssetTransfer();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="ChkBoxAssetTransferView" runat="server" onclick="SubCheckAssetTransfer();"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="ChkBoxAssetTransferUpdate" runat="server" onclick="SubCheckAssetTransfer();"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="ChkBoxAssetTransferDelete" onclick="SubCheckAssetTransfer();" runat="server"></asp:CheckBox></td>
                                </tr>
                            </table>    
                        
                        </td>
                    </tr>
                    <tr>
                       <td>
                           Daily Running Hours/Kms
                       </td>
                         <td><asp:CheckBox ID="chkboxDailyRunHourKmAll" runat="server" onclick="chkboxDailyRunHourKmAll();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>

                                    <td class="chkbox"><asp:CheckBox ID="chkboxDailyRunHourKmCreate" onclick="SubCheckDailyRunningHoursKms();" runat="server"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="chkboxDailyRunHourKmView" onclick="SubCheckDailyRunningHoursKms();" runat="server"></asp:CheckBox></td>
                                     <td class="chkbox"><asp:CheckBox ID="chkboxDailyRunHourKmUpdate" onclick="SubCheckDailyRunningHoursKms();" runat="server"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkboxDailyRunHourKmDelete" onclick="SubCheckDailyRunningHoursKms();" runat="server"></asp:CheckBox></td>
                                </tr>
                            </table>    
                        
                        </td>
                    </tr>
                    <tr>
                        <td>
                           Reports
                       </td>
                         <td><asp:CheckBox ID="ChkBoxAssetReportsAll" runat="server" onclick="ChkBoxAssetReportsAll();"></asp:CheckBox></td>
                        <td>
                            <table>
                                <tr>
                                    <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                     <td class="chkbox"><asp:CheckBox ID="ChkBoxAssetReportsView" onclick="SubCheckReports4();" runat="server"></asp:CheckBox></td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                     <td class="chkbox">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                </tr>
                            </table>    
                        
                        </td>
                    </tr>
                    
                </table>
        
        <br />
        <div class="row">
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Save" CssClass="btn btn-default" OnClick="btnSubmit_Click" TabIndex="12"></asp:Button>
                <asp:Button ID="btnCancel" runat="server" Text="Clear All" OnClick="btnCancel_Click" CssClass="btn btn-default" TabIndex="13"></asp:Button>

            </div>

        </div>
        <br />

        <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        </div>

        <ajaxToolkit:ModalPopupExtender ID="mpeDesig" runat="server" PopupControlID="PanelDesignation" TargetControlID="ImageButton2"
            CancelControlID="btnCloseDesignation" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="PanelDesignation" runat="server" align="center">

            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" id="btnCloseDesignation" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <center>  <h5 id="myModalLabel">Designation</h5></center>
                    </div>
                    <div class="modal-body">


                        <div class="row">
                            <div class="col-md-3">
                                Designation Name&nbsp;
          <asp:RequiredFieldValidator ID="RFV_ct" runat="server" ErrorMessage="*" ControlToValidate="txtDesignation" CssClass="Validation_Text" ValidationGroup="ValDesignation"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <center>
                        <cc1:Grid ID="Grid_Designation"   CallbackMode="true"  NumberOfPagesShownInFooter="2"  OnDeleteCommand="Grid_Designation_DeleteCommand" runat="server" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="5">
    <ScrollingSettings ScrollWidth="100%" />
                           
                            <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                            

    <Columns>
       <cc1:Column HeaderText="Designation" runat="server">
           <TemplateSettings  TemplateId="DesignationTemplate"/></cc1:Column>
      
        <cc1:Column DataField="Designation" HeaderText="Designation" Visible="false"></cc1:Column>
        
       

      
        <cc1:Column AllowDelete="true" HeaderText="Delete" Width="50px"></cc1:Column>
    </Columns>
                             <Templates>
        <cc1:GridTemplate ID="DesignationTemplate" runat="server">
            <Template>

                <asp:LinkButton ID="lnkDesignition" runat="server"   CssClass="gridCB" OnClick="lnkDesignation_Click" Text='<%# Container.DataItem["Designation"]%>'>


                </asp:LinkButton>
            </Template>
            </cc1:GridTemplate>
                                 </Templates>
</cc1:Grid></center>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>
           <asp:Button ID="btnSaveDesignation" runat="server" Text="Save"   CssClass="btn btn-default" ValidationGroup="ValDesignation"  OnClick="btnSaveDesignation_Click" TabIndex="18" />
                        <asp:Button ID="btnCancelDesignation" runat="server" Text="Cancel"  CssClass="btn btn-default" ValidationGroup="ValDesignation" CausesValidation="false" OnClick="btnCancelDesignation_Click" TabIndex="19"   />
                 </center>

                    </div>
                </div>
            </div>

        </asp:Panel>


        <ajaxToolkit:ModalPopupExtender ID="mpeDept" runat="server" PopupControlID="PanelDept" TargetControlID="imgBtnDept"
            CancelControlID="btnClose" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="PanelDept" runat="server" align="center">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" id="btnClose" data-dismiss="modal" aria-hidden="true">×</button>
                        <center><h5 id="myModalLabel1">Department</h5></center>
                    </div>
                    <div class="modal-body">
                        <div class="row">

                            <div class="col-md-3">
                                Department Name&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtDeptName" CssClass="Validation_Text" ValidationGroup="ValDept"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="txtDeptName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <center>
                               <cc1:Grid ID="Grid_Department" CallbackMode="true" NumberOfPagesShownInFooter="2" runat="server" FolderStyle="../Gridstyles/grand_gray" OnDeleteCommand="Grid_Department_DeleteCommand" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="5">
                                <ScrollingSettings ScrollWidth="100%" />
                                   <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                <Columns>
                                  
                                     <cc1:Column HeaderText="Department" runat="server" >
                                        <TemplateSettings  TemplateId="DepartmentTemplate"/>
        
                                               </cc1:Column>
        <cc1:Column DataField="Department" HeaderText="Department" Visible="false"></cc1:Column>
                                 
                                    <cc1:Column AllowDelete="true" HeaderText="Delete" Width="50px"></cc1:Column>
                                </Columns>
                                    <Templates>
        <cc1:GridTemplate ID="DepartmentTemplate" runat="server">
            <Template>

                <asp:LinkButton ID="lnkProjectID" runat="server"   CssClass="gridCB"  OnClick="lnkDepartment_Click"  Text='<%#Container.DataItem["Department"] %>'>


                </asp:LinkButton>
               
            </Template>
        </cc1:GridTemplate>

    </Templates>
                            </cc1:Grid>


                                


                        </center>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>
           <asp:Button ID="btnSaveDepartment" runat="server" Text="Save"   CssClass="btn btn-default" ValidationGroup="ValDept" OnClick="btnSaveDepartment_Click"/>
                        <asp:Button ID="btnCancelDepartment" runat="server" Text="Cancel"  CssClass="btn btn-default" ValidationGroup="ValDept" CausesValidation="false" OnClick="btnCancelDepartment_Click"  />
                 </center>

                    </div>
                </div>
            </div>


        </asp:Panel>
    </div>
</asp:Content>
