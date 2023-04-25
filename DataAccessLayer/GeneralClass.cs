using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.IO;

/// <summary>
/// Summary description for GeneralClass
/// </summary>
public class GeneralClass
{
	public GeneralClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public string ConvertNumbertoWord(int no)
    {


        string[] Ones = { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Ninteen" };

        string[] Tens = { "Ten", "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninty" };


        string strWords = "";

        if (no >= 10000 && no < 100000)
        {
            int i = no / 1000;
            strWords = strWords + Ones[i - 1] + " Lacks ";
            no = no % 1000;
        }

        if (no > 999 && no < 10000)
        {
            int i = no / 1000;
            strWords = strWords + Ones[i - 1] + " Thousand ";
            no = no % 1000;
        }

        if (no > 99 && no < 1000)
        {
            int i = no / 100;
            strWords = strWords + Ones[i - 1] + " Hundred ";
            no = no % 100;
        }

        if (no > 19 && no < 100)
        {
            int i = no / 10;
            strWords = strWords + Tens[i - 1] + " ";
            no = no % 10;
        }

        if (no > 0 && no < 20)
        {
            strWords = strWords + Ones[no - 1];
        }

        return strWords + "  Only";
    }

    #region Convert number to word New
    public String changeNumericToWords(double numb)
    {
        String num = numb.ToString();
        return changeToWords(num, false);
    }
    
    private String changeToWords(String numb, bool isCurrency)
    {
        String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
        String endStr = (isCurrency) ? ("") : ("");
        try
        {
            
            int decimalPlace = numb.IndexOf(".");
            if (decimalPlace > 0)
            {
                wholeNo = numb.Substring(0, decimalPlace);
                points = numb.Substring(decimalPlace + 1);
                if (Convert.ToInt32(points) > 0)
                {
                    andStr = (isCurrency) ? ("and") : ("point");// just to separate whole numbers from points/cents
                    endStr = (isCurrency) ? ("" + endStr) : ("");
                    pointStr = translateCents(points);
                }
            }
            val = String.Format("{0} {1}{2} {3}", translateWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
        }
        catch { ;}
    return val ; 
    }

    public String changeNumericToWordsUSD(double numb)
    {
        Decimal num = Convert.ToDecimal(numb);
        return USDToWords(num);
    }

    public static string USDToWords(decimal value)
    {
        string decimals = "";
        string input = Math.Round(value, 2).ToString();

        if (input.Contains("."))
        {
            decimals = input.Substring(input.IndexOf(".") + 1);
            // remove decimal part from input
            input = input.Remove(input.IndexOf("."));
        }

        // Convert input into words. save it into strWords
        string strWords = GetWords(input) + "  Dollars";


        if (decimals.Length > 0)
        {
            // if there is any decimal part convert it to words and add it to strWords.
            strWords += " and " + GetWords(decimals) + " Cents";
        }

        return strWords;
    }

    public String changeNumericToWordsINR(double numb)
    {
        Decimal num = Convert.ToDecimal(numb);
        return INRToWords(num);
    }

    public static string INRToWords(decimal value)
    {
        string decimals = "";
        string input = Math.Round(value, 2).ToString();

        if (input.Contains("."))
        {
            decimals = input.Substring(input.IndexOf(".") + 1);
            // remove decimal part from input
            input = input.Remove(input.IndexOf("."));
        }

        // Convert input into words. save it into strWords
        string strWords = GetWordsINR(input) + " Rupees";


        if (decimals.Length > 0)
        {
            // if there is any decimal part convert it to words and add it to strWords.
            strWords += " and " + GetWords(decimals) + " Paise";
        }

        return strWords;
    }

    private static string GetWords(string input)
    {
        // these are seperators for each 3 digit in numbers. you can add more if you want convert beigger numbers.
        string[] seperators = { "", " Thousand ", " Million ", " Billion " };

        // Counter is indexer for seperators. each 3 digit converted this will count.
        int i = 0;

        string strWords = "";

        while (input.Length > 0)
        {
            // get the 3 last numbers from input and store it. if there is not 3 numbers just use take it.
            string _3digits = input.Length < 3 ? input : input.Substring(input.Length - 3);
            // remove the 3 last digits from input. if there is not 3 numbers just remove it.
            input = input.Length < 3 ? "" : input.Remove(input.Length - 3);

            int no = int.Parse(_3digits);
            // Convert 3 digit number into words.
            _3digits = GetWord(no);

            // apply the seperator.
            _3digits += seperators[i];
            // since we are getting numbers from right to left then we must append resault to strWords like this.
            strWords = _3digits + strWords;

            // 3 digits converted. count and go for next 3 digits
            i++;
        }
        return strWords;
    }

    private static string GetWordsINR(string input)
    {
        int i = 0;
        string strWords = "";

        if (input.Length > 0)
        {
            // get the 3 last numbers from input and store it. if there is not 3 numbers just use take it.
            string _3digits = input.Length < 3 ? input : input.Substring(input.Length - 3);

            // remove the 3 last digits from input. if there is not 3 numbers just remove it.
            input = input.Length < 3 ? "" : input.Remove(input.Length - 3);

            int no1 = int.Parse(_3digits);
            _3digits = GetWord(no1);
            strWords = _3digits;
        }
        string[] seperators = { " Thousand ", " Lakh ", " Crore " };
        while (input.Length > 0)
        {
            string _2digits = input.Length < 2 ? input : input.Substring(input.Length - 2);
            input = input.Length < 2 ? "" : input.Remove(input.Length - 2);
            int no2 = int.Parse(_2digits);
            _2digits = GetWord(no2);
            if (_2digits != string.Empty)
            {
                _2digits += seperators[i];
            }
            strWords = _2digits + strWords;
            i++;
            if (i == 2 && input.Length == 3)
            {
                string _3digits = input.Length < 3 ? input : input.Substring(input.Length - 3);
                input = input.Length < 3 ? "" : input.Remove(input.Length - 3);
                int no1 = int.Parse(_3digits);
                _3digits = GetWord(no1);
                _3digits += seperators[i];
                strWords = _3digits + strWords;
            }
        }

        //string _2digits2 = input.Length < 2 ? input : input.Substring(input.Length - 2);
        //input = input.Length < 2 ? "" : input.Remove(input.Length - 2);
        //int no3 = int.Parse(_2digits2);
        //_2digits2 = GetWord(no3);
        //_2digits2 += seperators[i];
        //strWords = _2digits2 + strWords;

        return strWords;
    }

    // your method just to convert 3digit number into words.
    private static string GetWord(int no)
    {
        string[] Ones =
		{
			"One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
			"Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Ninteen"
		};

        string[] Tens = { "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninty" };

        string word = "";

        if (no > 99 && no < 1000)
        {
            int i = no / 100;
            word = word + Ones[i - 1] + " Hundred ";
            no = no % 100;
        }

        if (no > 19 && no < 100)
        {
            int i = no / 10;
            word = word + Tens[i - 1] + " ";
            no = no % 10;
        }

        if (no > 0 && no < 20)
        {
            word = word + Ones[no - 1];
        }

        return word;
    }
   
    private String translateWholeNumber(String number)
    {
        string word = "";
        try
        {
            bool beginsZero = false;//tests for 0XX
            bool isDone = false;//test if already translated
            double dblAmt = (Convert.ToDouble(number));
            //if ((dblAmt > 0) && number.StartsWith("0"))
            if (dblAmt > 0)
            {//test for zero or digit zero in a nuemric
                beginsZero = number.StartsWith("0");
                int numDigits = number.Length;
                if (numDigits == 1) beginsZero = true;
                int pos = 0;//store digit grouping
                String place = "";//digit grouping name:hundres,thousand,etc...
                switch (numDigits)
                {
                    case 1://ones' range

                        word = ones(number);
                        isDone = true;
                        break;

                    case 2://tens' range

                        word = tens(number);
                        isDone = true;

                        break;

                    case 3://hundreds' range

                        pos = (numDigits % 3) + 1;
                        if (number.StartsWith("0"))
                        {
                            place = "";
                        }

                        else
                        {
                            place = " Hundred " + "and" + "&nbsp;";
                        
                        }
                        //if (number.Substring(0, pos) == "1")
                        //{
                        //place = " Hundred ";
                        //}
                        //else
                        //{
                        //    place = " Hundreds ";
                        //}

                        break;

                    case 4://thousands' range

                        pos = (numDigits % 4) + 1;

                        if (number.StartsWith("0"))
                        {
                            place = "";
                        }
                        else
                        {
                            place = " Thousand " + "and" + "&nbsp;";

                        }
                        //if (number.Substring(0, pos) == "1")
                        //{
                        //place = " Thousand ";
                        //}
                        //else
                        //{
                        //    place = " Thousands ";
                        //}

                        break;
                    case 5:
                       
                        if (number.StartsWith("0"))
                        {
                            pos = (numDigits % 5) + 1;
                            place = "";
                        }
                        else
                        {
                            pos = (numDigits % 4) + 1;
                            place = " Thousand " + "and" + "&nbsp;";

                        }
                       
                        break;
                    case 6:

                        pos = (numDigits % 6) + 1;
                        if (number.StartsWith("0"))
                        {
                            place = "";
                            
                        }
                        else  if (number.Substring(0, pos) == "1")
                        {
                            place = " Lakh " + "and" + "&nbsp;";
                        }
                        else
                        {
                            place = " Lakhs " + "and" + "&nbsp;";
                        }
                        break;

                    case 7://millions' range

                    case 8:

                    case 9:

                        pos = (numDigits % 7) + 1;
                        place = " Million " + "and" + "&nbsp;";
                        break;

                    case 10://Billions's range

                        pos = (numDigits % 10) + 1;
                        place = " Billion " + "and"+"&nbsp;";
                        break;

                    //add extra case options for anything above Billion...

                    default:
                        isDone = true;
                        break;
                }
                if (!isDone)
                {//if transalation is not done, continue...(Recursion comes in now!!)
                    word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos));
                    //check for trailing zeros
                    if (beginsZero) word =  word.Trim();
                   
                }
                //ignore digit grouping names
                if (word.Trim().Equals(place.Trim())) word = "";
            }
        }
        catch { ;}
        if(word.Trim().EndsWith("&nbsp;"))
        {
            word = word.Trim().Substring(0, word.Trim().Length - 6);            
        }
        if(word.Trim().EndsWith("and"))
        {
            word = word.Trim().Substring(0, word.Trim().Length - 3);
        }
        return word.Trim();
    }
    
    private String tens(String digit)
    {
        int digt = Convert.ToInt32(digit);
        String name = null;
        switch (digt)
        {
            case 10:
                name = "Ten";
                break;

            case 11:
                name = "Eleven";
                break;

            case 12:
                name = "Twelve";
                break;

            case 13:
                name = "Thirteen";
                break;

            case 14:
                name = "Fourteen";
                break;

            case 15:
                name = "Fifteen";
                break;

            case 16:
                name = "Sixteen";
                break;

            case 17:
                name = "Seventeen";
                break;

            case 18:
                name = "Eighteen";
                break;

            case 19:
                name = "Nineteen";
                break;

            case 20:
                name = "Twenty";
                break;

            case 30:
                name = "Thirty";
                break;

            case 40:
                name = "Forty";
                break;

            case 50:
                name = "Fifty";
                break;

            case 60:
                name = "Sixty";
                break;

            case 70:
                name = "Seventy";
                break;

            case 80:
                name = "Eighty";
                break;

            case 90:
                name = "Ninety";
                break;

            default:
                if (digt > 0)
                {
                    name = tens(digit.Substring(0, 1) + "0") + " " + ones(digit.Substring(1));
                }
                break;
        }
        return name;
    }

    private String ones(String digit)
    {

        int digt = Convert.ToInt32(digit);

        String name = "";

        switch (digt)
        {

            case 1:

                name = "One";

                break;

            case 2:

                name = "Two";

                break;

            case 3:

                name = "Three";

                break;

            case 4:

                name = "Four";

                break;

            case 5:

                name = "Five";

                break;

            case 6:

                name = "Six";

                break;

            case 7:

                name = "Seven";

                break;

            case 8:

                name = "Eight";

                break;

            case 9:

                name = "Nine";

                break;

        }

        return name;

    }

    private String translateCents(String cents)
    {

        String cts = "", digit = "", engOne = "";

        for (int i = 0; i < cents.Length; i++)
        {

            digit = cents[i].ToString();

            if (digit.Equals("0"))
            {

                engOne = "Zero";

            }

            else
            {

                engOne = ones(digit);

            }

            cts += " " + engOne;

        }

        return cts;

    }
    # endregion

    # region Send Mail
    public int SendMail(ArrayList To, string CC, string BCC, string Subject, string Message, ArrayList attachment)
    {
        string MailServer = ConfigurationManager.AppSettings["SMTPServer"].ToString();
        string MailServerUser = ConfigurationManager.AppSettings["MailServerUser"].ToString();
        string MailServerPWD = ConfigurationManager.AppSettings["MailServerPWD"].ToString();
        string SmtpPort = ConfigurationManager.AppSettings["smtpPort"].ToString();
        string EnableSsl = ConfigurationManager.AppSettings["EnableSsl"].ToString();

        System.Net.Mail.MailMessage Msg = new System.Net.Mail.MailMessage();
        Msg.IsBodyHtml = true;
        Msg.From = new MailAddress(MailServerUser);
        try
        {
            for (int i = 0; i < To.Count; i++)
            {
                Msg.To.Add(To[i].ToString());
            }
        }
        catch (Exception ex)
        {
            return 0;
            //return false;
        }
        if (CC != null && CC.Trim() != "")
            Msg.CC.Add(CC);
        if (BCC != null && BCC.Trim() != "")
            Msg.Bcc.Add(BCC);
        Msg.Subject = Subject;
        Msg.Body = Message;

        try
        {
            for (int i = 0; i < attachment.Count; i++)
            {
                Msg.Attachments.Add(new Attachment(attachment[i].ToString()));
            }
           
            System.Net.Mail.SmtpClient SMC = new System.Net.Mail.SmtpClient();           
            System.Net.ICredentialsByHost Cred = new System.Net.NetworkCredential(MailServerUser, MailServerPWD);
            SMC.Credentials = Cred;
            SMC.Port =Convert.ToInt32(SmtpPort);
            SMC.Host = MailServer;
            SMC.EnableSsl =Convert.ToBoolean(EnableSsl);
            SMC.Send(Msg);
           

        }
        catch (Exception ex)
        {
           
            return 0;
        }
        

        return 1;

    }
    public int SendMail(string To, string Subject, string Message)
    {
        string MailServer = ConfigurationManager.AppSettings["SMTPServer"].ToString();
        string MailServerUser = ConfigurationManager.AppSettings["MailServerUser"].ToString();
        string MailServerPWD = ConfigurationManager.AppSettings["MailServerPWD"].ToString();
        string SmtpPort = ConfigurationManager.AppSettings["smtpPort"].ToString();
        string EnableSsl = ConfigurationManager.AppSettings["EnableSsl"].ToString();

        System.Net.Mail.MailMessage Msg = new System.Net.Mail.MailMessage();
        Msg.IsBodyHtml = true;
        Msg.From = new MailAddress(MailServerUser);
        Msg.To.Add(To);


        Msg.Subject = Subject;
        Msg.Body = Message;

        try
        {

            System.Net.Mail.SmtpClient SMC = new System.Net.Mail.SmtpClient();
            System.Net.ICredentialsByHost Cred = new System.Net.NetworkCredential(MailServerUser, MailServerPWD);
            SMC.Credentials = Cred;
            SMC.Port = Convert.ToInt32(SmtpPort);
            SMC.Host = MailServer;
            SMC.EnableSsl = Convert.ToBoolean(EnableSsl);
            SMC.Send(Msg);


        }
        catch (Exception ex)
        {

            return 0;
        }


        return 1;

    }
    # endregion



    



}