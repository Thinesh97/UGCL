<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC_Print.Master" AutoEventWireup="true" CodeBehind="EmployeeContractPrint.aspx.cs" Inherits="EmployeeContractPrint" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        table#instruct tr td {
            font-size: 14px;
            padding: 10px;
        }
        table#instruct {
            border: none;
        }

        table#instruct1 tr td {
            font-size: 11px;
            padding: 8px;
        }
        table#instruct1 {
            border: none;
        }

        table#ContentPlaceHolder1_GridPrint {
        /*border: 1px 1px solid #fff !important;*/
        border-top: 1px solid #fff;
        border-left: 1px solid #fff;
   
        }
        
        table#ContentPlaceHolder1_GridPrint tr th:nth-child(7) {
   
    border-right: 1px solid #fff;
}
        table#ContentPlaceHolder1_GridPrint tr td:nth-child(7) {
    border-right: 1px solid #fff;
   
}
        table#ContentPlaceHolder1_GridPrint tr td {
    border-bottom: 1px solid #fff;
}
        table#ContentPlaceHolder1_PO_GridTax {
    border: 1px solid #fff;
}
        table#ContentPlaceHolder1_GridPrint {
    margin-top: -2px;
}
        table#mytbl1 tr th, #mytbl2a tr th, #mytbl2b tr th, #mytbl3a tr th, #mytbl3b tr th {
            font-size: 14px;
            padding: 0 10px;
        }
        table#mytbl1 tr td, #mytbl2a tr td, #mytbl2b tr td, #mytbl3a tr td, #mytbl3b tr td {
            font-size: 14px;
            padding: 0 10px;
        }
        table#mytbl1 tr th {
            font-weight: bold;
        }
        table#mytbl2a tbody td, table#mytbl2b tbody td {
            border:  1px solid;
        }
        table#mytbl2a, table#mytbl2b {
            border:hidden;
        }

        invoiceFooter p {
            font-size: 14px;
            font-weight: 600;
        }
        table#mytbl2a tbody td, table#mytbl2b tbody td {
            border:  1px solid;
        }
        
        td.mysgst p {
            margin: 0;
            padding: 0;
            font-weight: 600;
        }

        @page {
            _size: 8.5in 11in;
            margin-top: 0px;
        }

        @media print {
            table#instruct3 {      
    margin: 1em 0 !important;
}
            table#instruct tr td {
                font-size: 14px;
            }
            table#instruct1 tr td {
                font-size: 12px;
            }
            table#ContentPlaceHolder1_GridPrint {
                border: 1px 1px solid #fff !important;
                border-top: 1px solid #fff !important;
                border-left: 1px solid #fff !important;
   
            }
            
            table#ContentPlaceHolder1_GridPrint tr th:nth-child(7) {
                border-right: 1px solid #fff !important;
            }
            table#ContentPlaceHolder1_PO_GridTax tr td {
                border: 1px solid #fff !important;
            }

            table#ContentPlaceHolder1_GridPrint tr td:nth-child(7) {
                border-right: 1px solid #fff !important;
            }
            table#ContentPlaceHolder1_GridPrint tr td {
                border-bottom: 1px solid #fff !important;
            }
            table#ContentPlaceHolder1_PO_GridTax tr td span {
                border: 1px solid #fff !important;
            }
            table#ContentPlaceHolder1_PO_GridTax {
                border: 1px solid #fff !important;
            }
            table#ContentPlaceHolder1_PO_GridTax {
                border: 1px solid #fff !important;
            }
            table#ContentPlaceHolder1_GridPrint {
                margin-top: -2px !important;
            }
            table#mytbl3a tbody td {
                /*border-bottom: 1px solid #fff !important;*/
                /*padding:1px !important;*/
            }
            table#mytbl1 tr th {
                font-weight: normal;
            }
            /*tr {
                page-break-before: always;
            }*/

            .no-print, .no-print * {
                display: none !important;
            }
        }

        @media all {
            .page-break { display: none; }
        }

        @media print {
            /*table#ContentPlaceHolder1_PO_GridTax {
            border: 1px solid #fff !important;
        }*/
             table#instruct3 {      
    margin: 1em 0 !important;
}
         
            table#instruct {
                border: none !important;
            }
             table#ContentPlaceHolder1_PO_GridTax {
                border: 1px solid #fff !important;
            }
            table#ContentPlaceHolder1_GridPrint {
                margin-top: -2px !important;
            }
            table#mytbl3 tbody td {
                border-bottom: 1px solid #fff !important;
                padding:1px !important;
            }
                    table#mytbl1 tr th {
                font-weight: normal;
            }
            .page-break { 
                display: block; 
                page-break-before: always;
            }
        }
    </style>


    <style>
        @media print {
            tr {
                /*page-break-before: always;*/
            }

            .no-print, .no-print * {
                display: none !important;
            }
        }


        td {
            text-align: justify;
            font-family: Arial;
            font-size: 15px;
            border: none;
            padding-bottom: 5px;
        }

        .firsttd {
            font-weight: bold;
            font-size: 18px;
            vertical-align: top;
            width: 30px;
        }
         .firsttdfromstart {
              font-weight: bold;
            font-size: 18px;
            vertical-align: top;
            width: 1px;
        }
         .firsttd2 {
            font-weight: bold;
            font-size: 16px;
            vertical-align: top;
            width: 100px;
        }
        .mypageadjust{
            height:960px;
        }
        .mypageadjusttwo{
            min-height:880px;
        }
    </style>
    <style>
        .heading{
             text-align: justify;
            font-family: Book Antiqua;
            font-size: 40px;
            border: none;
            padding-bottom: 5px;
        }
        .subheading{
             text-align: justify;
            font-family: Book Antiqua;
            font-size: 25px;
            border: none;
            padding-bottom: 5px;
        }
        .paragraphfont{
             font-size: 18px;
        }
        .Heading2{
            text-align: justify;
            font-family: Bahnschrift;
            font-size: 30px;
            border: none;
            padding-bottom: 5px;
        }
        .Heading3{
            text-align: justify;
            font-family: Bahnschrift;
            font-size: 20px;
            border: none;
            padding-bottom: 5px;
        }
         .Amrasent{
            
            font-size: 50px;
           
        }
         .mytable{
             border:none;
             width: 830px; 
             text-align: justify;
             
         }
         .alignCenter{
             text-align: center;
         }
         .alignleft{
             text-align: left;
         }
         .alignright{
             text-align: right;
         }
         .paddingtop{
              padding-top: 150px;
         }
         .signature {
    border: 0;
    border-bottom: 1px solid #000;
    
}
         /*.paytableStyle{
             border-bottom: 1px solid #000;

         }*/
         table.paytableStyle {
    border-width: 1px;
    border-spacing: 2px;
    border-style:inset;
    border-color: gray;
   background-color: white;
   width:40%;
   
}

table.paytableStyle td {
    border-width: 1px;
    padding: 1px;
    border-style: inset;
    border-color: gray;
    background-color: white;
    -moz-border-radius: ;
}
         
    </style>

    <script>

        $(document).ready(function () {
            $("#ContentPlaceHolder1_lblBasic_PayY").text(parseFloat($("#ContentPlaceHolder1_lblBasic_Pay").text()) * 12)
            $("#ContentPlaceHolder1_lblHouse_Rental_AllowanceY").text(parseFloat($("#ContentPlaceHolder1_lblHouse_Rental_Allowance").text()) * 12)
            $("#ContentPlaceHolder1_lblConveyance_AllowanceY").text(parseFloat($("#ContentPlaceHolder1_lblConveyance_Allowance").text()) * 12)
            $("#ContentPlaceHolder1_lblSpecial_AllowanceY").text(parseFloat($("#ContentPlaceHolder1_lblSpecial_Allowance").text()) * 12)
            $("#ContentPlaceHolder1_lblPF_ERY").text(parseFloat($("#ContentPlaceHolder1_lblPF_ER").text()) * 12)
            $("#ContentPlaceHolder1_lblPFY").text(parseFloat($("#ContentPlaceHolder1_lblPF").text()) * 12)
            $("#ContentPlaceHolder1_lblPTY").text(parseFloat($("#ContentPlaceHolder1_lblPT").text()) * 12)
            $("#ContentPlaceHolder1_lblPFY").text(parseFloat($("#ContentPlaceHolder1_lblPF").text()) * 12)
            $("#ContentPlaceHolder1_lblSub_Total_AY").text(parseFloat($("#ContentPlaceHolder1_lblSub_Total_A").text()) * 12)
            $("#ContentPlaceHolder1_lblTotal_Cost_to_CompanyY").text(parseFloat($("#ContentPlaceHolder1_lblTotal_Cost_to_Company").text()) * 12)
            $("#ContentPlaceHolder1_lblTotal_DeductionsY").text(parseFloat($("#ContentPlaceHolder1_lblTotal_Deductions").text()) * 12)
            $("#ContentPlaceHolder1_lblNET_PAYMENTY").text(parseFloat($("#ContentPlaceHolder1_lblNET_PAYMENT").text()) * 12)
            $("#lblTotal_Cost_to_CompanyTotalBind").text(parseFloat($("#lblTotal_Cost_to_CompanyTotalBind").text()) * 12)
        });
    </script>
   
    <div id="divToPrint1" class="paragraphfont" style="padding-top:20px; padding-bottom:20px">
        <div id="divTableDataHolder1" style="width: 880px">
        
            <b class="heading"><u>Appointment Letter</u> </b>
            <br />
            <br />
            <br />
           
            <b class="subheading">Private and Confidential</b>
            <br />
            <br />
            By and Between
            <br />
            <br />
           <b class="Heading2">M/S. UNITED GLOBAL CORPORATION LIMITED</b> <br />
           <b class="Heading3">(FORMERLY UNITED INFRA CORPORATION BANGALORE LIMITED)</b>  
            <br />
             <br />
            <%--<asp:Label runat="server" ID="lblCompanyAddress"></asp:Label>--%>
            <p>E-07, 7th Floor, Solus Jain Heights,No. 2, 1st Cross,J.C.Road, Opp. Jain University, Bengaluru - 560027, KA</p>
            <br />
            <br />
            <br />
           
           <b class="Amrasent"> &</b>
             <br />
              <br />
              <br />
           <b class="Heading3"><asp:Label runat="server" ID="lblFull_Name"></asp:Label>  </b>
            <br />
             <br />
            <asp:Label runat="server" ID="lblPermanent_Address"></asp:Label>
            <br />
             <br />
             <br />
         <b class="heading"><asp:Label runat="server" ID="lblActualDOfJoining"></asp:Label> </b> 
        </div>
    </div>

    <div class="page-break"></div>
    <br />
    <br />   
    <table class="mytable" style="padding-top:40px; padding-bottom:40px">
       <img src="../Style/Images/Pic1.png" style="width:10px" />
                <tr>
                    <td class="firsttd">
                    </td>
                    
                    <td style="width:50%; font-weight:bold; padding-left:30px;  text-align:left">To, <br />
                        <asp:Label runat="server" ID="lblFull_Name1"></asp:Label> <br /> 
                         <asp:Label runat="server" ID="lblPermanent_Address1"></asp:Label> <br />
                        <div style="Height:70px; Width:180px;"></div>
                    </td> 
                     <td style="width:50%; font-weight:bold; padding-left:30px; text-align:right"">Date:<asp:Label runat="server" ID="lblActualDOfJoining1"></asp:Label>
                        <div style="Height:70px; Width:180px;"></div>
                    </td>
                </tr>
           </table>

            <table  class="mytable" id="instruct2">
                <tr>
                    <td class="firsttd">
                    </td>
                    <td>
                        <b class="Heading3">Dear  <asp:Label runat="server" ID="lblFull_Name2"></asp:Label>,</b><br />
                        <b class="Heading3">Sub: Appointment Letter</b><br /><br />
                        We wish to thank you for your interest in our Organization. We also wish to congratulate you for your time and effort spent towards our structured recruitment and selection process.
                        <br />
                        <br />
                        In pursuance to your efforts during the selection process we are pleased to offer you the position of <b>  <asp:Label runat="server"  ID="lblDesignation"></asp:Label> </b>in our organization. We hope that you would do justice to your role and carry the tasks allotted to you by us. Though you have been delivered a copy of the most important terms and conditions with some basic remuneration details etc, you are required to sign and accept this detailed employment contract which shall underline all the terms and conditions, deliverables, emoluments, separation procedures and allied additional documents that would be required for smoother functioning of this engagement with you.
                         <br />
                        <br />
                         <br />
                        You are requested to accept, sign and retain one copy of this Employment Contract as the same shall be referred and referable for all future communications with the organization.
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="firsttd">
                    </td>
                    <td>
                       Yours Truly, <br />
                 <b> For United Global Corporation Limited</b> 
                        <br />
                        <br />
                        <br />
                        <b>Director</b>

                    </td>
                </tr>
                </table>
         <div class="page-break"></div>
       <table  class="mytable" id="instruct3">
                    <tr>
                    <td class="firsttd">
                    </td>
                    <td>
                          <img src="../Style/Images/Pic1.png" style="width:10px" /> 
                        <img src="../Style/Images/Pic1.png" style="width:10px" /> 
                   <h4 class="h3" style="text-align:center">Contents</h4>
                        <br />
                       
                        <div style="font-size:18px">
                    Chapter 1: <b>Parties to the Agreement </b>.........................................3<br />
                    Chapter 2: <b> Duration of the Agreement</b>.......................................3<br />
                    Chapter 3: <b> Consideration</b>............................................................3<br />
                    Chapter 4: <b>Employment Terms and Conditions</b>........................3<br />
                    Chapter 5: <b>Separation and Termination</b>.....................................5<br />
                    Chapter 6: <b>Dispute Jurisdiction and Resolution</b>.......................6<br />
                    Chapter 7: <b>Employee Indemnity</b>..................................................6<br />
                    Chapter 8: <b>Confidentiality of Relationship and information</b>.....7<br />
                    Chapter 9: <b>Severability</b>.................................................................7<br />
                    Chapter 10: <b>Non Solicitation</b>........................................................8<br />
                    Annexure A: <b>Salary Break UP</b>.......................................................9<br />
                    Annexure B: <b>Non-Compete Undertaking</b>......................................10<br />
                            <br />
                            <br />
                        </div>
                    </td>
                </tr>
                </table>

    <div class="page-break"></div>
       <table  class="mytable" id="instruct4">
                <tr>
                    <td>
                    </td>
                    <td>
                        <img src="../Style/Images/Pic1.png" style="width:10px" /> <br /> <br />
                        <h4 class="Heading3" style="text-align:center">This Indenture/ Agreement shall commence on <asp:Label runat="server" ID="lblActualDOfJoining2"></asp:Label></h4>
                        <h4 class="Heading3" style="text-align:center"> <b>Chapter 1: Parties to the Agreement </b></h4> <br/>
                        1.<b>M/s. United Global Corporation Limited</b>, having its office at E-07, 7th Floor, Solus Jain Heights,No. 2, 1st Cross,J.C.Road, Opp. Jain University, Bengaluru - 560027, KA, which term unless repugnant to the context shall mean and include all representatives, successors in title and permitted assigns. Herein after referred as<b>"EMPLOYER"</b>  
                        <br />
                        <br />
                        
                        2.<b><asp:Label runat="server" ID="lblFull_Name3"></asp:Label></b>, S/o. <asp:Label runat="server" ID="lblFatherName"></asp:Label> having Permanent Residence at <asp:Label runat="server" ID="lblPermanent_Address2"></asp:Label>. term unless repugnant shall mean all his legal heirs, representatives, and assigns. Herein after referred as<b>"EMPLOYEE"</b> 
                        <br />
                        <br />
                        <h4 class="Heading3" style="text-align:center"> <b>Chapter 2: Duration of the Agreement</b></h4>
                        <br />
                       
                        The term of this Agreement shall begin from the date of this offer as stated on the cover page of this indenture and shall exist for a period that may be termed as superannuation unless terminated before. Upon becoming permanent employee and unless otherwise terminated, the term may be considered till superannuation of the employee.
                        <br />
                        
                        <h4 class="Heading3" style="text-align:center"> <b>Chapter 3: Consideration</b></h4>
                         <br />
                        
                        A total sum of ₹ <b><asp:Label runat="server" ID="lblTotal_Cost_to_Company_y"></asp:Label> </b>   <b> ( <asp:Label runat="server" ID="lblTotal_Cost_to_Company_Yearly_InWords"></asp:Label> )</b> shall be earned by the EMPLOYEE every year. This shall be subject to revision in accordance with the performance appraisal policies of the Organization, which shall be the absolute discretion of EMPLOYER.
                        <br />
                         <br />
                        <h4 class="Heading3" style="text-align:center"> <b>Chapter 4: Employment Terms and Conditions</b></h4>
                        <br />
                       
                        In accordance with the offer made to you and you having accepted the offer of employment, now as an Employee you undertake and agree that during the course of your employment with EMPLOYER. 
                        <br />
                        <br />
                    </td>
                </tr>
                    <tr>
                        <td class="firsttd"></td><td><b>A. General Terms and Conditions</b> </td>
                    </tr>
                <tr>
                    <td class="firsttd">
                        <b>a.</b>
                    </td>
                    <td>You shall not undertake any form of business/employment whether part time, full time or contractual or otherwise with any other employer, partner, related or unrelated to the business of EMPLOYER for the duration of this agreement. In case, EMPLOYER gets notice of the same, you undertake and agree that it may result in instant termination of this engagement without limiting to the rights of EMPLOYER for any appropriate action, under law.</td>
                </tr>
           <tr>
               <td class="firsttd"> <b>b.</b></td>
               <td>Your engagement as an employee is on your accepting the condition that upon termination of this contract, you will not in any manner compete with EMPLOYER and/ or aid the competition for a period of at least 12 months from the date of Termination of this Contract.A non-competition undertaking will be signed by you as given in Annexure B.</td>
           </tr>
           <tr>
               <td class="firsttd">c.</td>
               <td>In connection with the performance of your professional assignment, you may have to interact with personnel of EMPLOYER and/or its affiliates, stake holders, Clients etc. Your conduct should be business-like and we expect you to refrain from any acts of commission or omission, which subverts the discipline of "EMPLOYER ".</td>
           </tr>
            <tr>
               <td class="firsttd">d.</td>
               <td>You confirm and accept that the work performed by you is for the benefit of "EMPLOYER" and therefore, all the rights accruing/ accrued, arising out of performance of the assignment/s will be vesting in "EMPLOYER" and that the salary earned by you is full and final consideration for the entire assignment/s of such proprietary material/s.</td>
           </tr>
            <tr>
               <td class="firsttd">e.</td>
               <td>You will indemnify "EMPLOYER" against any false claim that you make and/ or loss that you may cause to "EMPLOYER" because of your inept and unprofessional like conduct or actions taken by any third party is, due to your violation of any of the terms of the Agreement.</td>
           </tr>
            <tr>
               <td class="firsttd">f.</td>
               <td>You accept that the amount of consideration paid to you is composite payments for all rights that you have under this contract and you will not claim any other payment from "EMPLOYER" over and above the consideration fixed herein above.
                  
               </td>
           </tr>
            <tr>
                
               <td class="firsttd">g.</td>
               <td>No notice of resignation will be effective if given during a period of leave of absence from the Company and you will also not be eligible to proceed on such leave during the notice period.
               </td>
           </tr>
            <tr>
                <td></td>
                <td><img src="../Style/Images/Pic1.png"  style="width:10px"/> <br />
               <img src="../Style/Images/Pic1.png"  style="width:10px"/> <br />
                    <img src="../Style/Images/Pic1.png"  style="width:10px"/>
              
            </tr>
            <tr">
               <td class="firsttd">h.</td>
               <td >You will be entitled to Privilege leave in accordance with the Rules of the Company being in force at the time. You will carry out all instructions of your superior(s) in the Company as regards your work, attendance, conduct, behaviour, etc. and carry out diligently and honestly all duties that may be assigned to you by the Company from time to time notwithstanding the designation given above. Your days of work and working hours will be as per the working hours of the office in which you are for the time being posted and can be changed at the discretion of the management of the Company.</td>
           </tr>
            <tr>
               <td class="firsttd">i.</td>
               <td>You will retire in the normal course from the services of the Company on attaining the age of superannuation, that is on the day following your 58th birthday.</td>
           </tr>
            <tr>
               <td class="firsttd">j.</td>
               <td>Company however will have a right to retire you earlier from the services of the Company any time after attaining the age of fifty five, if it is of the opinion that you are not physically or mentally fit enough to perform your assigned duties efficiently and effectively. Such early retirement may be given to you by giving a notice of two months. No extra compensation is payable to you in such an event.</td>
           </tr>
            <tr>
               <td class="firsttd">k.</td>
               <td>Your individual remuneration is purely a matter between yourself and the company and has been arrived on the basis of your job, skills specific background and professional merit. We expect you to maintain this information and any changes made therein from time to time as personal and confidential.</td>
           </tr>
            <tr>
               <td class="firsttd">l.</td>
               <td>Information pertaining to the Company’s operations shall remain secret and safeguarded by you. On joining the Company, a formal agreement to effect non-disclosure of confidential information and intellectual property etc, shall be executed by you at a later date.  You will also keep us duly informed if you are bound by any confidentiality agreement with your previous employers, in which case you shall keep us indemnified against any breach thereof by you.</td>
           </tr>
            <tr>
               <td class="firsttd">m.</td>
               <td>The terms set out in this letter, together with the non-disclosure agreement, will form your Contract of Employment with the Company. You Should abide by the Rules and Regulations of the Company which are in force from time to time and the Company shall have the right to vary or modify any or all of the above terms and conditions in service which shall be binding on you.</td>
           </tr>
            <tr>
               <td class="firsttd">n.</td>
               <td>Any act of dishonesty, disobedience, insubordination, incivility, intemperance, irregularity in attendance or other misconduct or neglect of duty, or incompetence in the discharge of duty on your part or the breach of any of the terms, conditions and stipulations contained herein will render you liable to termination of your employment without notice or compensation thereof.</td>
           </tr>
           <tr>
               <td class="firsttd">o.</td>
               <td>You being adjudged an insolvent or applying to be adjudged an insolvent or making a compensation or arrangement with your creditors or being found guilty by a competent court of any offence involving moral turpitude will render you liable to termination of your employment without notice or compensation thereof.</td>
           </tr>
           <tr>
               <td class="firsttd">p.</td>
               <td>You shall inform the company of any change in your personal data within 3 working days. Any notice required to be given to you shall be deemed to have been duly and properly given if delivered to you personally or sent to you by registered post to you at your address in India, as recorded with the Company.</td>
           </tr>
           <tr>
               <td class="firsttd">q.</td>
               <td>By signing below, you confirm that you are not bound by any agreement with any previous employer or any party, which restricts in any way your prospective employment by Company (for example, any non-compete or noncompetition agreement, non-disclosure or confidentiality agreement, non-solicitation agreement, etc.).  Such agreements may be contained in offer letters from previous employers, stock option grants, employment agreements, independent contract agreements, agreements for the sale of a business etc.  You represent that your employment with Company and the performance of your proposed duties for Company will not violate any obligations you have to such previous employer or other party.  In your work for Company, you will not disclose or make use of any information or trade secrets in violation of any agreements with or rights of any such previous employer or other party, and you will not bring to Company premises any copies or other tangible embodiments of non-public information belonging to or obtained from any such previous employment or other party.</td>
           </tr>
           <tr>
               <td class="firsttd">r.</td>
               <td>In case any information furnished by you either in your application for employment or during the selection process is found to be incorrect/false, and /or if it is found that you have suppressed any material information in respect of your qualifications and past experience, the Company reserves the right to terminate your services any time without notice or compensation in lieu of notice.</td>
           </tr><tr>
               <td class="firsttd">s.</td>
               <td>Breach of any of the above terms and conditions will render you liable to termination of your employment without notice or compensation thereof. The Benefits provided by the Company as outlined herein and in the Company policies are subject to change at the discretion of the Company.</td>
           </tr>
           <tr>
               <img src="../Style/Images/Pic1.png"  style="width:10px"/> <br />
               <img src="../Style/Images/Pic1.png"  style="width:10px"/> <br />
               <td class="firsttd">t.</td>
               <td>The following original documents (or officially certified copies) must be mandatorily provided either before the commencement of employment, or no later than the morning of your first working day:
                   <br />
                   <ul>
                       <li>Highest level examination certificate(s) and professional qualification certificate(s)</li>
                        <li>Your birth certificate or school leaving certificate</li>
                        <li>Your passport/ ID Proof/ Address Proof</li>
                   </ul>
               </td>
           </tr> 
       </table>
                
    <table class="mytable" id="instruct5">
       
       <tr><td class="firsttd"></td><td><img src="../Style/Images/Pic1.png"  style="width:10px"/> <br /><img src="../Style/Images/Pic1.png"  style="width:10px"/> <br /><b>  B. Special Terms under Probation </b>  </td></tr>
         <tr>

            <td class="firsttd">a.</td>
            <td>Your initial appointment with EMPLOYER is upon probation for a period of at least 3 months from the date of Joining and the same shall be your status quo until you are made permanent employee, at the discretion of the organization. The decision taken by EMPLOYER shall be deemed final in this regard.</td>
        </tr>
        <tr>
            <td class="firsttd">b.</td>
            <td>Voluntary Resignation: In the event you resign from the Company without assigning any valid reasons and without providing the 60 days’ notice as stated under clause c below, to the Company, then the Company shall be entitled to recover costs as stipulated under clause e below. </td>
        </tr>
        <tr>
            <td class="firsttd">c.</td>
            <td>Termination: In the event of wilful neglect of your duties, breach of trust, gross indiscipline, performance issues or any other serious dereliction of duties that may be prejudicial to the interests of the company, the company has the discretion to terminate your services forthwith or with such notice as it deems fit and without any notice pay whatsoever. You expressly undertake to diligently help the company during any transition of your designated assignments in case of any voluntary or involuntary separation of you from the company. The relieving letter may be returned after completion of agreed separation procedures with the Company.</td>
        </tr>
        <tr>
            <td class="firsttd">d.</td>
            <td>In the event of wilful neglect of your duties, breach of trust, gross indiscipline, performance issues or any other serious dereliction of duties that may be prejudicial to the interests of the company, the company has the discretion to terminate your services forthwith; with or without any notice or pay in lieu of notice.</td>
        </tr>
        <tr>
            <td class="firsttd">e.</td>
            <td>Post Termination/ Resignation: The Company shall be entitled to recover from you costs incurred by it in terms of training cost, transition cost and such other expenses that may have been incurred by it, in addition to the 8.33% of your CTC (Hiring Cost), as per law.</td>
        </tr>
        <tr>
            <td class="firsttd">f.</td>
            <td>Upon such separation from EMPLOYER, you shall undertake to deliver back any hardware given to you including but not limited to your handheld  phones,  SIM cards, laptops etc. and also enlist all the confidential  information  that you  were subject to acquire on a need to know basis during such period. You shall be bound by the Annexure B of this contract even after separation.</td>
        </tr>
        <tr>
            <td class="firsttd"></td>
                <td class="alignCenter"> <b class="Heading3"> Chapter 5: Separation and Termination</b></td>
        </tr>
        <tr>
            <td class="firsttd"></td>
            <td> <b>A.	Terms of Separation </b></td>
        </tr>
         <tr>
            <td class="firsttd">a.</td>
            <td>In case you have found better avenues and for some reasons you are not able to continue with the organization, you shall be free to separate out of the organization by serving atleast a 60 Days' notice or as may be decided by EMPLOYER as a part of their transition process or Salary in lieu of the notice period including all benefits at the sole discretion of Employer.</td>
        </tr>
        <tr>
            <td class="firsttd">b.</td>
            <td>To facilitate the separation process, you may submit your resignation to your reporting officer and making a copy to the HR in the company. Once accepted by the company, your 60 Days’ notice period shall begin from the date of acceptance of resignation by your reporting Manager.</td>
        </tr>
        <tr>
            <td class="firsttd">c.</td>
            <td>Upon serving of such notice period to the satisfaction of the company you shall be entitled for your full and final settlement, relieving letter, experience letter and any other document that you may require to evidence your employment with the organization.</td>
        </tr>
        <tr>
            <td class="firsttd"></td>
             <td></td>
            
        </tr>
        <tr>
            <td class="firsttd">d.</td>
             <td>Any defaults in the process of separation as underlined above or any other internal policy document to this effect shall be constituted as a breach of contract and shall be liable for termination.</td>
            
        </tr>
        <tr>
            <td class="firsttd"></td>
             <td> <b>B. Termination </b></td>
            
        </tr>
        <tr>
            <td class="firsttd">a.</td>
             <td>In case of any default from your end in not serving the notice period or non-payment of the salary in lieu of the notice period shall be treated as a breach of covenant of this indenture and the company may reserve its rights to proceed against you for non-performance of your obligations under this indenture</td>
            
        </tr>
        <tr>
            <td class="firsttd">b.</td>
             <td>Once terminated, you will be responsible to ensure that you return all the deliverables, stop interacting with the clients or affiliates of Employer, decipher or use any information that you were subjected to during your employment with Employer, and any other deliverables as evidenced in Annexure B to this indenture.</td>
            
        </tr>
        <tr>
            <td class="firsttd">c.</td>
             <td>You will be subjected to immediate termination for any illegal activities including but not limited to sexual harassment, racial abuse at work place, cheating, forgery, aiding to competition for wrongful gains etc. in case of termination of these sorts, Company shall reserve the right to approach the police to lodge appropriate complaints over and above termination procedures.</td>
            
        </tr>
        <tr>
            <td class="firsttd">d.</td>
             <td>Termination shall not waive any rights whatsoever of the company towards any claims, recovery or any other punitive or other legal action.</td>
            
        </tr>
        <tr>
           
            <td class="firsttd"></td>
            
             <td class="alignCenter"> 
                
                <img src="../Style/Images/Pic1.png" style="width:10px" /> <br /><img src="../Style/Images/Pic1.png" style="width:10px" /> <br />
            <b class="Heading3">Chapter 6: Dispute Jurisdiction and Resolution</b></td>
        </tr>
        <tr>
             <img src="../Style/Images/Pic1.png" style="width:10px" /> <br /><img src="../Style/Images/Pic1.png" style="width:10px" /> <br /><img src="../Style/Images/Pic1.png" style="width:10px" /> <br />
            <td class="firsttd">1.</td>
            <td>In case of performance or absenteeism related issues, alone, a show cause letter will be issued against the employee, before issuance of termination letter. For all other cases listed in Chapter 5 Part B, the employee may be terminated with immediate effect.</td>
        </tr>
        <tr>
            
            <td class="firsttd">2.</td>
            <td>In case of any disputes arising between the employee and EMPLOYER, and amicable discussions with the management of EMPLOYER fail, this Agreement shall be strictly subjected to the jurisdiction of the Courts of Bangalore, Karnataka India. This indenture shall be governed by the Laws of Republic of India.</td>
        </tr>
        <tr>
            <td class="firsttd"></td>
           <td  class="alignCenter"><b class="Heading3"><br />Chapter 7: Employee Indemnity</b> </td>     
        </tr>
        <tr>
            <td class="firsttd01"></td>
            <td>EMPLOYEE agrees and undertakes that in no event shall EMPLOYER be liable for any incidental, punitive, indirect or consequential damages whatsoever (including but not omitted titled to damages for loss of profits or confidential or other information, for any kind of interruption, for personal injury, for loss of privacy, for failure to meet any duty including of good faith or of reasonable care, negligence, and any other pecuniary or other loss whatever) arising out of or in any way related to the use of or inability to use and other inputs provided under this Agreement , the provision of or failure to provide support or other services, information and related content, or otherwise arising out of the use or otherwise under even in the event of fault, tort (including negligence), misrepresentation, strict or product liability , breach of contract or breach of warranty of EMPLOYER or any person acting on behalf of EMPLOYER and even if EMPLOYER or Person acting on behalf of EMPLOYER has been advised of the possibility of such damages.</td>
        </tr>
        <tr>
            <td class="firsttd"></td>
            <td class="alignCenter"><b class="Heading3">Chapter 8: Confidentiality of Relationship and information</b>
            </td>
        </tr>
        <tr>
            <td class="firsttd">1.</td>
            <td>Parties shall not use or divulge or disclose in any manner any Proprietary Information or any part thereof to any person (other than those whose province it is to know the same or as permitted or contemplated by this Agreement).</td>
        </tr>
        <tr>
            <td class="firsttd">2.</td>
            <td>Parties shall strictly adhere to the non-disclosure provisions contained herein, and shall comply with the confidentiality provisions contained herein.</td>
        </tr>
        <tr>
            <td class="firsttd">3.</td>
            <td>Receiving Party (Employee) hereby undertakes that it shall:</td>
        </tr>
        <tr>
            <td class="firsttd"></td>
             
            <td><b>a.</b> Use the greatest degree of care to avoid unauthorized dissemination or publication of "Proprietary Information" disclosed by the Disclosing Party (EMPLOYER).</td>
        </tr>
        <tr>
            <td class="firsttd"></td>
            <td></td>
        </tr>
        <tr>
            <td class="firsttd"></td>
            <td> <b>b.</b>Use the "Proprietary Information" only for the purposes specifically permitted by the Disclosing Party (EMPLOYER)</td>
        </tr>
        <tr>
            <td class="firsttd"></td>
            <td><b>c.</b>	Not make copies of "Proprietary Information" other than for the purposes specifically permitted by the Disclosing Party (EMPLOYER).</td>
        </tr>
        <tr>
            <td class="firsttd">d.</td>
            <td>Ensure that "Proprietary Information" is kept secured on its premises.</td>
        </tr>
        <tr>
            <td class="firsttd">e.</td>
            <td>In the event of becoming aware of any unauthorized copying, disclosure or use of the confidential information, promptly notify Disclosing Party (EMPLOYER) thereof and if requested take such steps as shall be necessary to prevent such further unauthorized copying, disclosure or use.</td>
        </tr>
        <tr>
            <td class="firsttd">4.</td>
            <td>The restrictions contained hereinabove with regard to the confidentiality, shall not apply in the following cases:</td>
        </tr>
        <tr>
            <td class="firsttd"></td>
            <td><b>a.</b>Any information which may come into the public domain otherwise than through unauthorized disclosure by the receiving party (Employee).</td>
        </tr>
        <tr>
            <td class="firsttd"></td>
            <td><b>b.</b>Any disclosure is required to be made in pursuance of any law or regulation or by a duly authorized written order of court/ relevant government authority, provided such disclosure shall be restricted to the extent the same is required and the disclosing party (EMPLOYER) shall be promptly informed by receiving party (Employee) regarding such disclosure</td>
        </tr>
         <tr>
            <td class="firsttd" >5.</td>
            <td>To reach a better degree of trust, the receiving party (Employee) shall sign a Proprietary Information Agreement as a part of this contract in Annexure B</td>
        </tr>
         <tr>
            <td class="firsttd"></td>
            <td class="alignCenter"><b class="Heading3">Chapter 9: Severability </b></td>
        </tr>
         <tr>
            <td style="padding-left:0px" ></td>
            <td>Any provision of this Agreement, which is held to be invalid or unenforceable for any reason, shall be ineffective to the extent of such invalidity or unenforceability only, and without affecting in any way the remaining provisions hereof.</td>
        </tr>
         <tr>
            <td class="firsttd"></td>
            <td class="alignCenter"><b class="Heading3"><img src="../Style/Images/Pic1.png" style="width:10px" /> <br />
                
                 
                Chapter 10: Non-Solicitation</b></td>
        </tr>
        <tr>
            <td class="firsttd"> </td>
                <td>During the term of your service with the Company or anytime thereafter for a period of 12 months after your last working date, you shall not induce or solicit any other employees of the Company to join any other organisation or your own company/ firm/ sole proprietorship and you shall not solicit the Clients/customers of the Company for any work for such other organisation or your own company/ firm/sole proprietorship</td>
           
        </tr>
         <tr>
            <td class="firsttd"> </td>
                <td><img src="../Style/Images/Pic1.png" style="width:10px" /> <br /><img src="../Style/Images/Pic1.png" style="width:10px" /> <br /><img src="../Style/Images/Pic1.png" style="width:10px" /> <br /><img src="../Style/Images/Pic1.png" style="width:10px" /> <br />IN WITNESS WHEREOF the parties hereto have hereunto caused this Agreement to be executed in duplicate by their duly authorised representatives on the day and year first hereinabove mentioned.</td>
           
        </tr>
         <tr>
            <td class="firsttd"> </td>
                <td></td>
           
        </tr>
        <tr>
            <td class="firsttd"></td>
                <td></td>
            
        </tr>
         <tr>
            <td class="firsttd"> </td>
                <td>THIS Agreement, is executed by the persons signing below who warrant that they have the authority to execute the contract.</td>
        </tr>
        <tr>
            <td class="firsttd"></td>
                <td style="padding-top:100px; padding-bottom:100px" >For United Global Corporation Limited</td>
            
        </tr>
    </table>
     <table class="mytable">
              <tr>
                  <td style="border-top:groove">Director <p class="alignright"><asp:Label runat="server" ID="lblFull_Name4"></asp:Label></p> 
                  
                </td>
              </tr>
                <tr>
                   <td></td>
                </tr>
           </table>
    <div class="page-break"></div>
      <table class="mytable">
             <tr>
                 <td class="firsttd"></td>
                 <td class="alignCenter"> <b class="Heading3">
                     <img src="../Style/Images/Pic1.png" style="width:10px" />  <br />
                     <img src="../Style/Images/Pic1.png" style="width:10px" />  <br />
                     <img src="../Style/Images/Pic1.png" style="width:10px" />  <br />
                     <img src="../Style/Images/Pic1.png" style="width:10px" />  <br />
                     Annexure A: Salary Break up </b></td>
             </tr>
          <tr>
              <td class="firsttd"></td>
              <td>Upon successful completion of the tasks given to you as a part of your employment with EMPLOYER, your remuneration shall be as stated below:</td>
          </tr>
          <tr>
              <td class="firsttd"></td>
              <td>Cost to the Company: </td>
          </tr>
           </table>
   
      <table class="paytableStyle">
               <thead>
                   <tr>
                       <td> <b>Salary Component</b></td>
                       <td><b>Monthly</b></td>
                       <td><b>Yearly</b></td>
                   </tr>
               </thead>
                <tbody>
                    <tr>
                        <td>Basic Pay</td>
                     <td> <asp:Label runat="server" ID="lblBasic_Pay"></asp:Label></td>   
                      <td><asp:Label runat="server" ID="lblBasic_PayY"></asp:Label> </td>  
                    </tr>
                     <tr>
                        <td>House Rental Allowance</td>
                      <td>  <asp:Label runat="server" ID="lblHouse_Rental_Allowance"></asp:Label> </td>
                       <td> <asp:Label runat="server" ID="lblHouse_Rental_AllowanceY"></asp:Label></td> 
                    </tr>
                     <tr>
                        <td>Conveyance Allowance</td>
                          <td><asp:Label runat="server" ID="lblConveyance_Allowance"></asp:Label></td>
                         <td> <asp:Label runat="server" ID="lblConveyance_AllowanceY"></asp:Label></td>
                       
                    </tr>
                    <tr>
                        <td>Special Allowance</td>
                         <td><asp:Label runat="server" ID="lblSpecial_Allowance"></asp:Label></td>
                        <td><asp:Label runat="server" ID="lblSpecial_AllowanceY"></asp:Label></td>
                        
                    </tr>
                    <tr>
                        <td><b>Sub Total (A)</b></td>
                        <td><b><asp:Label runat="server" ID="lblSub_Total_A"></asp:Label></b></td>
                        <td><b><asp:Label runat="server" ID="lblSub_Total_AY"></asp:Label></b></td>
                    </tr>
                    <tr>
                        <td>Provident Fund Employer</td>
                         <td><asp:Label runat="server" ID="lblPF_ER"></asp:Label></td>
                        <td><asp:Label runat="server" ID="lblPF_ERY"></asp:Label></td>
                    </tr>
                    <tr>
                         <td><br /></td>
                         <td><br /></td>
                        <td><br /></td>
                    </tr>
                    <tr>
                        <td><b>Total Cost to the Company</b></td>
                         <td><b><asp:Label runat="server" ID="lblTotal_Cost_to_Company"></asp:Label></b></td>
                        <td><b><asp:Label runat="server" ID="lblTotal_Cost_to_CompanyY"></asp:Label></b></td>
                    </tr>
                    <tr>
                        <td><b>Deductions</b></td>
                         <td><asp:Label runat="server" ID="lblDeductions"></asp:Label></td>
                        <td><asp:Label runat="server" ID="lblDeductionsY"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Provident Fund Employee</td>
                         <td><asp:Label runat="server" ID="lblPF"></asp:Label></td>
                        <td><asp:Label runat="server" ID="lblPFY"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Professional Tax</td>
                         <td><asp:Label runat="server" ID="lblPT"></asp:Label></td>
                        <td><asp:Label runat="server" ID="lblPTY"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>Total Deductions (B)</b></td>
                         <td><b><asp:Label runat="server" ID="lblTotal_Deductions"></asp:Label></b></td>
                        <td><b><asp:Label runat="server" ID="lblTotal_DeductionsY"></asp:Label></b></td>
                    </tr>
                    <tr>
                        <td><br /></td>
                         <td><br /></td>
                        <td><br /></td>
                    </tr>
                    <tr>
                        <td><b>Take Home (C = A- B)</b></td>
                         <td><b><asp:Label runat="server" ID="lblNET_PAYMENT"></asp:Label></b></td>
                        <td><b><asp:Label runat="server" ID="lblNET_PAYMENTY"></asp:Label> </b></td>
                    </tr>
                     
                </tbody>
           </table>
    <div class="page-break"></div>
    <table class="mytable" id="instruct6">
        <tr>
            <td class="firsttd"></td>
              <td class="alignCenter"> <b class="Heading3"><img src="../Style/Images/Pic1.png" style="width:10px" /> <br /><img src="../Style/Images/Pic1.png" style="width:10px" /> <br />Annexure B: Non-Compete Undertaking </b></td>
        </tr>
         <tr>
            <td class="firsttd"></td>
              <td>Non- Compete Agreement is executed on this <asp:Label runat="server" ID="lblActualDOfJoining3"></asp:Label>, <asp:Label runat="server" ID="lblFull_Name5"></asp:Label>residing at <asp:Label runat="server" ID="lblPermanent_Address3"></asp:Label>, (Receiving Party) which expression unless repugnant to the context shall mean and include their representatives, employees, successors who may have access to the information received from "EMPLOYER".</td>
        </tr>
         <tr>
            <td class="firsttd"></td>
              <td> <b> <asp:Label runat="server" ID="lblFull_Name8"></asp:Label></b>, receiving Party accept that "United Global Corporation Limited", (hereinafter referred to as "EMPLOYER") having their office at E-07, 7th Floor, Solus Jain Heights,No. 2, 1st Cross,J.C.Road, Opp. Jain University, Bengaluru - 560027, KA is entitled to seek this Non-Compete Agreement considering the value of the information "EMPLOYER" may share or may come to our knowledge during various interactions verbally, in writing and / or through any mode of communication.</td>
        </tr>
         <tr>
            <td class="firsttd"></td>
              <td>Receiving Party therefore voluntarily and willingly executes this undertaking to protect the interest of "EMPLOYER" as herein appearing.</td>
        </tr>
         <tr>
            <td class="firsttd"></td>
              <td>This agreement is valid for a period of 12 months from the Last working date of the employee.</td>
        </tr>
         <tr>
            <td class="firsttd">1.</td>
              <td>Receiving Party (Employee) specifically agrees that he/ she is not violating any agreement signed with any competitors by agreeing to render services to EMPLOYER. Further, after the execution of this Agreement, unless agreed in writing, Receiving Party (Employee) shall not be dealing with competitors or related products in any manner to infringe the confidentiality agreement with "EMPLOYER" and to disclose even accidentally any information pertaining to "EMPLOYER" business, trade secrets being used to benefits the competitors or related products. If Receiving Party (Employee) seeks permission of "EMPLOYER" and the permission by "EMPLOYER" is not granted and Receiving Party (Employee) insists for taking up the business activity of competitors or related products, "EMPLOYER" shall have the option to terminate all agreements with the Receiving Party (Employee) without notice.</td>
        </tr>
         <tr>
            <td class="firsttd">2.</td>
              <td>Upon breach of this undertaking, Receiving Party (Employee) will not be entitled for any further disclosures, and shall not earn or be entitled to receive amount earned/and or due from the affected party and can further take appropriate action against the Receiving Party (Employee) as provided under the laws of India including be suit for damages</td>
        </tr>
         <tr>
            <td class="firsttd">3.</td>
              <td>No failure or delay by "EMPLOYER" in exercising or enforcing any right, remedy or power hereunder shall operate as a waiver thereof, nor shall any single or partial exercise or enforcement of any right, remedy or power preclude any further exercise or enforcement thereof or the exercise or enforcements of any other right, remedy or power.</td>
        </tr>
         <tr>
            <td class="firsttd"></td>
              <td>Any dispute between the parties pertaining to this section shall be settled amicably by the parties, failure of which will be referred to an Arbitrator appointed by "EMPLOYER" and the decision of the Arbitrator shall be final and binding on the parties. The Arbitration shall be in English, the place of Arbitration shall be in Bangalore, and the Arbitration shall be as per the Provisions of Indian Arbitration & Conciliation Act, 1996, and any amendments or replacement thereof. Subject to Arbitration, the courts in City of Bangalore shall alone have jurisdiction</td>
        </tr>
        <tr">
            <td class="firsttdfromstart" style="padding-top:60px;padding-bottom:60px;"></td>
            <td>Receiving Party <br /> <b><asp:Label runat="server" ID="lblFull_Name6"></asp:Label> </b> </td>
            
        </tr>
          <tr>
            <td class="firsttdfromstart"></td>
            <td></td>
        </tr>
    </table>

   
    <script src="../Style/js/jquery-1.11.1.min.js"></script>
    <script src="../Style/js/FileSaver.js"></script>
    <script src="../Style/js/jquery.wordexport.js"></script>

    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $("a.word-export").click(function (event) {
                $("#divToPrint").wordExport("Purchase Order");
            });
        });
    </script>
    <script type="text/javascript">
        var tableToExcel = (function () {

            var uri = 'data:application/vnd.ms-excel;base64,'
                , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
                , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
                , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
            return function (table, name) {
                if (!table.nodeType) table = document.getElementById(table)
                var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
                window.location.href = uri + base64(format(template, ctx))
            }
        })()
    </script>
    <script type="text/javascript">
        function test() {
            debugger;
            window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('div[id$=divToPrint]').html()));
            e.preventDefault();
        }
    </script>
    <script type="text/javascript">
        function PrintDiv() {
            var divToPrint = document.getElementById('divToPrint');
            var popupWin = window.open('', '_blank', 'width=1000,height=1000');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }
    </script>

</asp:Content>