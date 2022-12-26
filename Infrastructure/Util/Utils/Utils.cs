using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace System
{
    public static class ConstVariable
    {
        public const string TYPE_BOOLEAN = "BOOLEAN";
        public const string TYPE_DATETIME = "DATETIME";
        public const string TYPE_DROPDOWN = "DROPDOWN";
        public const string TYPE_LABLE = "LABLE";
        public const string TYPE_TEXT = "TEXT";
        public const string TYPE_TEXT_MULTILINE = "TEXT (MULTILINE)";
        public const string TYPE_DROPDOWN_MULTISELECT = "DROPDOWN (MULTISELECT)";
        public const string TYPE_FILEUPLOAD = "FILE UPLOAD";
        public const string TYPE_DIVIDER = "DIVIDER";
        public const string TYPE_RADIOLIST = "RADIOLIST";
        public const string TYPE_TIME = "TIME";

        public const string RENDER_TYPE_EDIT = "E";
        public const string RENDER_TYPE_READONLY = "V";
        public const string RENDER_TYPE_HIDE = "H";

        public const string SCREEN_NAME_NEW_CASE = "New Case";
        public const string SCREEN_NAME_UPDATE_CASE = "Update Case";

        public const string ID = "_ID";
        public const string EntityID = "_EntityID";
        public const string RequestTypeID = "_RequestTypeID";
        public const string PriorityID = "_PriorityID";
        public const string StatusID = "_StatusID";

        public const string OwnerShipID = "_OwnerShipID";
        public const string OwnerShipName = "_OwnerShipName";
        public const string CC = "_CC";
        public const string DueDate = "_DueDate";
        public const string CandidateID = "_CandidateID";
        public const string CandidateName = "_CandidateName";
        public const string AssignedTo = "_AssignedTo";
        public const string AssignedToName = "_AssignedToName";

        public const string Notes = "_Notes";

    }

    public static class ConvertExtension
    {
        public static decimal ToByte(this object value)
        {
            try
            {
                Decimal result;
                if (Decimal.TryParse(Convert.ToString(value), out result))
                    result = Convert.ToDecimal(value);
                if (result == 0)
                    return 1;
                return result;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public static decimal ToDecimalDefaultOne(this object value)
        {
            try
            {
                Decimal result;
                if (Decimal.TryParse(Convert.ToString(value), out result))
                    result = Convert.ToDecimal(value);
                if (result == 0)
                    return 1;
                return result;
            }
            catch (Exception)
            {
                return 1;
            }
        }
        public static decimal ToDecimalDefaultZero(this object value)
        {
            try
            {
                Decimal result;
                if (Decimal.TryParse(Convert.ToString(value), out result))
                    result = Convert.ToDecimal(value);
                if (result == 0)
                    return 0;
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static double ToDoubleDefaultZero(this object value)
        {
            try
            {
                double result;
                if (Double.TryParse(Convert.ToString(value), out result))
                    result = Convert.ToDouble(value);
                if (result == 0)
                    return 0;
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static bool ToBoolDefaultFalse(this object value)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool ToBoolDefaultTrue(this object value)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch (Exception)
            {
                return true;
            }
        }
        public static int ToInt(this object value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static string ToActiveOrDeactive(this object value)
        {
            try
            {
                if (Convert.ToBoolean(value))
                    return "Active";
                else
                    return "In Active";
            }
            catch (Exception)
            {
                return "--";
            }
        }
        public static string ToYOrN(this object value)
        {
            try
            {
                if (Convert.ToBoolean(value))
                    return "Y";
                else
                    return "N";
            }
            catch (Exception)
            {
                return "--";
            }
        }
        public static string ToYesOrNo(this object value)
        {
            try
            {
                if (Convert.ToBoolean(value))
                    return "Yes";
                else
                    return "No";
            }
            catch (Exception)
            {
                return "--";
            }
        }

        public static byte[] ToByteArray(this object value)
        {
            try
            {
                byte[] bty = (byte[])value;
                return bty;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
    public static class StringMethod
    {
        public static string ToApproveOrReject(this int item)
        {
            try
            {
                var rtn = "Pending";
                if (item == 1)
                {
                    rtn = "Approved";
                }
                else if (item == 0)
                {
                    rtn = "Rejected";
                }
                else if (item == 2)
                {
                    rtn = "Pending";
                }
                else if (item == 3)
                {
                    rtn = "Visited";
                }
                return rtn;
            }

            catch
            { return string.Empty; }
        }
        public static string StringName(this string input)
        {
            string name = string.Empty;
            try
            {

                char[] chars = input.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    string considerChar = string.Empty;
                    char letter = chars[i];
                    switch (letter)
                    {
                        case '0':
                            {
                                considerChar = "A";
                                break;
                            }
                        case '1':
                            {
                                considerChar = "B";
                                break;
                            }
                        case '2':
                            {
                                considerChar = "C";
                                break;
                            }
                        case '3':
                            {
                                considerChar = "D";
                                break;
                            }
                        case '4':
                            {
                                considerChar = "E";
                                break;
                            }
                        case '5':
                            {
                                considerChar = "F";
                                break;
                            }
                        case '6':
                            {
                                considerChar = "G";
                                break;
                            }
                        case '7':
                            {
                                considerChar = "H";
                                break;
                            }
                        case '8':
                            {
                                considerChar = "I";
                                break;
                            }
                        case '9':
                            {
                                considerChar = "J";
                                break;
                            }
                        default:
                            {
                                considerChar = "K";
                                break;
                            }
                    }

                    name = name + considerChar;
                }
                return name;
            }
            catch
            {
                return string.Empty;
            }
        }
        public static byte[] ToByteArray(this string strValue)
        {
            if (strValue.IsNotStringNullOrEmpty())
            {
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                return encoding.GetBytes(strValue);
            }
            return null;
        }


        /// <summary>
        /// create phone format string
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToPhoneFormatString(this string number)
        {
            if (!string.IsNullOrEmpty(number))
            {
                return Regex.Replace(number, @"(\d{3})(\d{3})(\d{4})", "$1-$2-$3");
            }
            return "--";
        }
        /// <summary>
        /// create phone format string with XXX-XXX-
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToPhoneXXX(this string number)
        {
            if (!string.IsNullOrEmpty(number))
            {
                if (number.Length > 4)
                    return Regex.Replace(string.Concat("XXX-XXX-", number.Substring(number.Length - 4)), @"(\d{3})(\d{3})(\d{4})", "$1-$2-$3");
                return string.Empty;
            }
            return string.Empty;
        }
        /// <summary>
        /// create phone format string with XXX-XXX-
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToPhoneType(this string number, string type, string ext = "")
        {
            if (!string.IsNullOrEmpty(number))
            {
                if (ext.IsNotStringNullOrEmpty())
                {
                    return string.Format("{0}: {1}X{2}", type, number, ext);
                }

                return string.Format("{0}: {1}", type, number);
            }
            return string.Empty;
        }

        //remove replace string 
        public static string ToRemoveString(this string input, string replaceChar)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return input.Replace(replaceChar, "");
            }
            return string.Empty;
        }

        public static string ToActiveOrDeactive(this bool item)
        {
            try
            {
                if (item)
                {
                    return "Active";
                }
                return "Deactive";
            }

            catch
            { return string.Empty; }
        }
        public static string ToLargeOrSmall(this bool item)
        {
            try
            {
                if (item)
                {
                    return "Large";
                }
                return "Small";
            }

            catch
            { return string.Empty; }
        }
        public static string ToYesOrNo(this bool item)
        {
            try
            {
                if (item)
                {
                    return "Yes";
                }
                return "No";
            }

            catch
            { return string.Empty; }
        }
        public static string ToVerify(this bool item)
        {
            try
            {
                if (item)
                {
                    return "Verify";
                }
                return "Un-verify";
            }

            catch
            { return string.Empty; }
        }
        public static string ToDate(this object item)
        {
            try
            {
                return Convert.ToDateTime(item).ToString("dd/MM/yyyy");
            }

            catch (InvalidCastException ecast)
            { throw ecast; }
            catch (IndexOutOfRangeException iore)
            { throw iore; }
            catch (NullReferenceException f)
            { throw f; }
        }
        public static string ToSeoUrl(this string url)
        {
            // make the url lowercase
            string encodedUrl = (url ?? "").ToLower();

            // replace & with and
            encodedUrl = Regex.Replace(encodedUrl, @"\&+", "and");

            // remove characters
            encodedUrl = encodedUrl.Replace("'", "");

            // remove invalid characters
            encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");

            // remove duplicates
            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");

            // trim leading & trailing characters
            encodedUrl = encodedUrl.Trim('-');

            return encodedUrl;
        }
        public static bool IsNotStringNullOrEmpty(this object item)
        {
            try
            {
                if (item != null)
                {
                    if (!string.IsNullOrEmpty(item.ToString()) || !string.IsNullOrWhiteSpace(item.ToString()))
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// remove all white space from string
        /// </summary>
        /// <param name="input">string</param>
        /// <returns>string</returns>
        public static string RemoveSpace(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return Regex.Replace(input, @"\s", "");
            }
            return input;
        }

        /// <summary>
        /// remove all Special Char from string
        /// </summary>
        /// <param name="input">string</param>
        /// <returns></returns>
        public static string ToRemoveSpecialChar(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return Regex.Replace(input, "[^0-9a-zA-Z]+", "").toStringWithDash();
            }
            return input.toStringWithDash();
        }

        /// <summary>
        /// input total seconds 
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns>days:hours:minutes</returns>
        public static string GetDays(this string seconds)
        {
            string dys = string.Empty;
            try
            {
                long sec = Convert.ToInt64(seconds);
                var ts = TimeSpan.FromSeconds(sec);
                int days = ts.Days;
                int hours = ts.Hours;
                int minutes = ts.Minutes;
                dys = string.Format("{0}:{1}:{2}", days <= 9 ? "0" + days.ToString() : days.ToString(),
                    hours <= 9 ? "0" + hours.ToString() : hours.ToString(),
                    minutes <= 9 ? "0" + minutes.ToString() : minutes.ToString());
                return dys;
            }
            catch
            {
                return dys;
            }
        }

        public static string CleanHTMLOnlyString(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return String.Empty;
            string s1 = s.ToString().Trim();
            if (s1.Length == 0) return String.Empty;
            string pattern = @"<[^>]*?>|<[^>]*>";
            Regex rgx = new Regex(pattern);
            return rgx.Replace(s1, String.Empty);
        }

        public static string[] ExtractEmails(this string text)
        {
            string[] EmailList = new string[0];
            Regex r = new Regex(@"[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}", RegexOptions.IgnoreCase);
            Match m;
            for (m = r.Match(text); m.Success; m = m.NextMatch())
            {
                if (m.Value.Length > 0)
                {
                    Array.Resize(ref EmailList, EmailList.Length + 1);
                    EmailList[EmailList.Length - 1] = m.Value;
                }
            }
            return EmailList;
        }
        public static string toStringWithPercentageSymbol(this object strInp)
        {
            try
            {
                if (strInp != null)
                {
                    if (!string.IsNullOrEmpty(strInp.ToString()))
                    {
                        return string.Format("{0}%", strInp);
                    }
                    return "--";
                }
                return "--";
            }
            catch
            {
                return "--";
            }

        }
        public static string toStringWithCurrencySymbol(this object strInp, string cultureName)
        {
            try
            {

                if (cultureName.IsNotStringNullOrEmpty() == false)
                {
                    cultureName = "en-US";
                }
                RegionInfo us = new RegionInfo(cultureName);
                if (strInp != null)
                {
                    if (!string.IsNullOrEmpty(strInp.ToString()))
                    {
                        return string.Format("{0}{1}", us.CurrencySymbol, strInp.ToString());
                    }
                    return "--";
                }
                return "--";
            }
            catch
            {
                return "--";
            }

        }
        public static string toStringWithSupplierType(this object strInp)
        {
            try
            {
                if (strInp != null)
                {
                    if (Convert.ToBoolean(strInp))
                    {
                        return "Primary";
                    }
                    return "Secondary";
                }
                return "Secondary";
            }
            catch
            {
                return "--";
            }
        }
        public static string toStringWithCustomeType(this object strInp)
        {
            try
            {
                if (strInp != null)
                {
                    if (Convert.ToBoolean(strInp))
                    {
                        return "Business";
                    }
                }
                return "Regular";
            }
            catch
            {
                return "Regular";
            }
        }
        public static string toStringWithDash(this object strInp)
        {
            if (strInp != null)
            {
                if (!string.IsNullOrEmpty(strInp.ToString()))
                {
                    return strInp.ToString();
                }
                return "--";
            }
            return "--";

        }
        public static string toStringWithEmpty(this object strInp)
        {
            try
            {
                if (strInp != null)
                {
                    if (!string.IsNullOrEmpty(strInp.ToString()))
                    {
                        return strInp.ToString();
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
        public static string toStringWithYAndN(this object strInp)
        {
            try
            {
                if (strInp != null)
                {
                    if (!string.IsNullOrEmpty(strInp.ToString()))
                    {
                        return strInp.ToString();
                    }
                    else
                    {
                        return "N";
                    }
                }
                return "N";
            }
            catch
            {
                return "N";
            }
        }
        public static string toStringWithZero(this object strInp)
        {
            try
            {
                if (strInp != null)
                {
                    if (!string.IsNullOrEmpty(strInp.ToString()))
                    {
                        return strInp.ToString();
                    }
                    else
                    {
                        return "0.0";
                    }
                }
                return "0.0";
            }
            catch
            {
                return "0.0";
            }
        }
        /// <summary>
        /// gets substring before specified substring.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="a"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        public static string Before(this string value, string a, StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            int posA = value.IndexOf(a, stringComparison);
            if (posA == -1)
            {
                return string.Empty;
            }
            return value.Substring(0, posA).Replace("\t", string.Empty).Replace("\r", string.Empty);
        }

        /// <summary>
        /// gets substring after specified substring.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="a"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        public static string After(this string value, string a, StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            int posA = value.IndexOf(a, stringComparison);
            if (posA == -1)
            {
                return string.Empty;
            }
            return value.Substring(posA).Replace("\t", string.Empty).Replace("\r", string.Empty);
        }

        /// <summary>
        /// Gets string between two specified strings.
        /// </summary>
        /// <param name="textData"></param>
        /// <param name="startString"></param>
        /// <param name="endString"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        public static string Between(this string textData, string startString, string endString = "\r", StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {

            try
            {
                //get position of lastchar of start string
                int pos1 = textData.IndexOf(startString, stringComparison);
                if (pos1 >= 0)
                {
                    pos1 = pos1 + startString.Length;
                    //get position of firstchar endstring
                    int pos2 = textData.IndexOf(endString, pos1, stringComparison);
                    if (pos2 > 0)
                        return textData.Substring(pos1, pos2 - pos1).Trim().Replace("\t", string.Empty).Replace("\r", string.Empty);
                    return string.Empty;
                }

            }
            catch
            {

                return string.Empty;
            }
            return string.Empty;
        }


        public static DateTime ToUniversalTime(this string dt)
        {
            try
            {
                CultureInfo en = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentCulture = en;

                // Creates a DateTime for the local time.
                // Converts the local DateTime to the UTC time.
                DateTime utcdt = Convert.ToDateTime(dt).ToUniversalTime();
                return utcdt;
            }
            catch
            {
                return DateTime.Now.ToUniversalTime();
            }

        }
        /// <summary>
        /// Convert datetime to shortdateString in specific format irrelevant to server date format
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToFormattedShortDateString(this object dt)
        {
            try
            {
                if (dt != null)
                    return Convert.ToDateTime(dt).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                return string.Empty;
            }
            catch (Exception e)
            {
                string s = e.Message;
                return string.Empty;
            }
        }


        public static string ToFormattedShortBirthDateString(this object dt)
        {
            try
            {
                if (dt != null)
                    return Convert.ToDateTime(dt).ToString("MM/dd", CultureInfo.InvariantCulture);
                return string.Empty;
            }
            catch (Exception e)
            {
                string s = e.Message;
                return string.Empty;
            }
        }

        public static string ToStringWithDateTime(this object dt, bool withdash = false)
        {
            try
            {
                if (dt != null)
                    return Convert.ToDateTime(dt).ToString("MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                return withdash ? "--" : string.Empty;
            }
            catch (Exception e)
            {
                string s = e.Message;
                return withdash ? "--" : string.Empty;
            }
        }

        /// <summary>
        /// Checks whether is string is numeric.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string s)
        {
            float output;
            return float.TryParse(s, out output);
        }


        /// <summary>
        /// Checks whether is string is of format ssn or not.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsSSN(this string s)
        {

            bool output = true;
            if (!string.IsNullOrWhiteSpace(s))
            {
                //output = s.IsNumeric();//for numeric string no need to check

                Regex rgx = new Regex(@"^\w{3}-?\w{2}-?\w{4}$");
                output = rgx.IsMatch(s.PadLeft(9, 'X'));
            }
            return output;
        }

        /// <summary>
        /// Get last n char for the given string.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="tail_length"></param>
        /// <returns></returns>
        public static string GetLast(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }


        /// <summary>
        /// Convert  shortdateString MM/dd/yyyy in specific datetime irrelevant to server date format
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static DateTime ToFormattedDateFromString(this string dateString)
        {
            try
            {
                return DateTime.ParseExact(dateString, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            catch (NullReferenceException)
            {
                throw;
            }
            catch (FormatException)
            {
                //to handle additional format
                DateTime formateDate;
                if (DateTime.TryParseExact(dateString,
                       "M/dd/yyyy",
                       CultureInfo.InvariantCulture,
                       DateTimeStyles.None,
                       out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString,
                       "MM/dd/yy",
                       CultureInfo.InvariantCulture,
                       DateTimeStyles.None,
                       out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString,
                   "MMMM dd, yyyy",
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString,
                    "MMMM d, yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString,
                    "dd-MM-yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString,
                    "dd-M-yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString,
                   "MMM-dd-yy",
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString,
                   "dd MMM yyyy",
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString,
                  "MMMM, yyyy",
                  CultureInfo.InvariantCulture,
                  DateTimeStyles.None,
                  out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString,
                    "dd-MMM-yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString,
                    "MM-dd-yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString,
                    "MM/dd/yyyy HH:mm",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString + "/1904",
                  "MM/dd" + "/yyyy",
                  CultureInfo.InvariantCulture,
                  DateTimeStyles.None,
                  out formateDate))
                {
                    //here 1904 is a leap year so using  it instead of 1900.For birthdate when year is not specified.
                    //formateDate = new DateTime(1904, formateDate.Month, formateDate.Day);
                    return formateDate;
                }


                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static NameValueCollection GetQueryStringSeprate(string DeQStr)
        {
            NameValueCollection functionReturnValue = null;
            NameValueCollection sCollection = new NameValueCollection();
            string sQStr = null;
            try
            {

                string[] sAnd = null;
                int cnt = 0;
                sAnd = DeQStr.Split('&');
                if (sAnd.Length > 0)
                {
                    for (cnt = 0; cnt < sAnd.Length; cnt++)
                    {
                        string[] sEqual = null;
                        string str = "";
                        int CntS = 0;
                        str = sAnd[cnt];
                        sEqual = str.Split('=');
                        if (sEqual.Length >= 0)
                        {
                            string sPara = "";
                            string sValue = "";
                            for (CntS = 0; CntS <= sEqual.Length - 1; CntS++)
                            {
                                if (CntS == 0)
                                {
                                    sPara = sEqual[CntS];
                                }
                                if (CntS == 1)
                                {
                                    sValue = sEqual[CntS];
                                }
                            }
                            sCollection.Add(sPara, sValue);
                        }
                    }
                }
                else
                {
                    int intPos;
                    if (((sQStr.IndexOf("=", 0) + 1) > 0))
                    {
                        intPos = (sQStr.IndexOf("=", 0) + 1);
                        if ((intPos > 0))
                        {
                            string strP1;
                            string strV1;
                            strP1 = sQStr.Substring(0, (intPos - 1));
                            strV1 = sQStr.Substring(intPos, sQStr.Length);
                            sCollection.Add(strP1, strV1);
                        }
                    }
                }
                functionReturnValue = sCollection;
            }
            catch
            {
                throw;
            }
            return functionReturnValue;
        }

        public static DateTime? ToFormattedDateNullble(this string dateString)
        {
            try
            {
                if (dateString != null)
                    return DateTime.ParseExact(dateString, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                return null;

            }
            catch (NullReferenceException)
            {
                return null;
            }
            catch (FormatException)
            {
                //to handle additional format
                DateTime formateDate;
                if (DateTime.TryParseExact(dateString,
                       "M/dd/yyyy",
                       CultureInfo.InvariantCulture,
                       DateTimeStyles.None,
                       out formateDate))
                {
                    return formateDate;
                }

                if (DateTime.TryParseExact(dateString,
                       "MM/dd/yy",
                       CultureInfo.InvariantCulture,
                       DateTimeStyles.None,
                       out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString,
                   "MMMM dd, yyyy",
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString,
                    "MMMM d, yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString,
                    "dd-MM-yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString,
                    "dd-M-yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString,
                   "MMM-dd-yy",
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString,
                   "dd MMM yyyy",
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString,
                  "MMMM, yyyy",
                  CultureInfo.InvariantCulture,
                  DateTimeStyles.None,
                  out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString,
                    "dd-MMM-yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString,
                    "MM-dd-yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out formateDate))
                {
                    return formateDate;
                }
                if (DateTime.TryParseExact(dateString + "/1904",
                  "MM/dd" + "/yyyy",
                  CultureInfo.InvariantCulture,
                  DateTimeStyles.None,
                  out formateDate))
                {
                    //here 1904 is a leap year so using  it instead of 1900.For birthdate when year is not specified.
                    //formateDate = new DateTime(1904, formateDate.Month, formateDate.Day);
                    return formateDate;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static int GetBusinessDays(this DateTime startDate, DateTime endDate)
        {
            int count = 0;
            for (DateTime index = startDate; index < endDate; index = index.AddDays(1))
                if (index.DayOfWeek != DayOfWeek.Sunday && index.DayOfWeek != DayOfWeek.Saturday)
                    count++;

            return count;
        }

        #region "Get format"
        public static void ExtractEmails(string inFilePath, string outFilePath)
        {
            string data = inFilePath;// File.ReadAllText(inFilePath); //read File 
                                     //instantiate with this pattern 
            Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
                RegexOptions.IgnoreCase);
            //find items that matches with our pattern
            MatchCollection emailMatches = emailRegex.Matches(data);

            StringBuilder sb = new StringBuilder();

            foreach (Match emailMatch in emailMatches)
            {
                sb.AppendLine(emailMatch.Value);
            }
            //store to file
            //File.WriteAllText(outFilePath, sb.ToString());
            var s = sb.ToString();
        }

        public static int[] makeChange(double val)
        {

            double[] notes = { 5, 2, 1, 0.5 };
            int[] result = new int[6];
            double currentSum = 0;
            int demo = 0;
            while (currentSum < val)
            {
                if (currentSum + notes[demo] <= val)
                {
                    currentSum += notes[demo];
                    result[demo]++;
                }
                else
                {
                    demo++;
                }
            }

            return result;
        }

        public static bool Mod10Check(string Number)
        {
            //// check whether input string is null or empty
            if (string.IsNullOrEmpty(Number))
            {
                return false;
            }


            int sumOfDigits = Number.Where((e) => e >= '0' && e <= '9')
                            .Reverse()
                            .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
                            .Sum((e) => e / 10 + e % 10);
            return sumOfDigits % 10 == 0;
        }

        #endregion

        #region "Get SSN"
        public static string FourDigitSSN(this string ssnnumber)
        {
            if (ssnnumber != null)
            {
                if (ssnnumber.IsNotStringNullOrEmpty())
                {

                    //if (!ssnnumber.IsSSN())
                    //{
                    if (ssnnumber.Length > 3)
                        ssnnumber = ssnnumber.Substring(ssnnumber.Length - 4);

                    //}
                    return Regex.Replace(ssnnumber.PadLeft(9, 'X'), @"^(\w{3})(\w{2})(\w{4})$", "$1-$2-$3");
                }
            }
            return string.Empty;
        }


        #endregion

    }


}
