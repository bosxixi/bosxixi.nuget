using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace bosxixi.Extensions
{
    public static class Extension
    {
        public static bool IsPhoneNumber(this string text)
        {
            Regex phone = new Regex("[0-9]{11}");
            if (text.Length > 11)
            {
                return false;
            }
            return phone.IsMatch(text);
        }
        public static bool IsEmail(this string text)
        {
            Regex email = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            return email.IsMatch(text);
        }

        public static bool IsIp(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }


        public static string CutString(string source, int maxLength)
        {
            if (maxLength < 3)
                throw new ArgumentException();

            if (String.IsNullOrEmpty(source))
                return string.Empty;

            if (source.Length > maxLength)
                return source.Substring(0, maxLength - 3) + "...";

            return source;
        }

        public static bool IsNullOrCountEqualsZero<T>(this IEnumerable<T> list)
        {
            if (list == null)
            {
                return true;
            }

            return !list.Any();
        }
        public static string ToString(this IEnumerable<string> stringArray, char spliter)
        {
            if (stringArray.IsNullOrCountEqualsZero())
            {
                return null;
            }
            StringBuilder b = new StringBuilder();
            foreach (var item in stringArray)
            {
                b.Append(spliter.ToString()).Append(item);
            }
            return b.ToString().Remove(0, 1);
        }

        public static List<string> SplitToList(this string fakeList, char spliter)
        {
            if (String.IsNullOrEmpty(fakeList))
            {
                return null;
            }
            List<string> listOfString = new List<string>();

            foreach (var item in fakeList.Split(spliter))
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                listOfString.Add(item);
            }

            if (listOfString.Count() == 0)
            {
                return null;
            }
            return listOfString;
        }

        public static List<int> SplitToListOfInt(this string fakeList, char spliter)
        {
            if (String.IsNullOrEmpty(fakeList))
            {
                return null;
            }
            List<int> listOfInt = new List<int>();

            foreach (var item in fakeList.Split(spliter))
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                listOfInt.Add(int.Parse(item));
            }

            if (listOfInt.Count() == 0)
            {
                return null;
            }
            return listOfInt;
        }


        /// <summary>
        /// 判断地理位置是否在中国国土
        /// </summary>
        /// <param name="locationValue"></param>
        /// <returns></returns>
        public static bool IsLocationBelongToChina(this string locationValue)
        {
            try
            {
                string[] _value = locationValue.Split(',');
                if (decimal.Parse(_value[0].Trim()) < 3 || decimal.Parse(_value[0].Trim()) > 53)
                {
                    return false;
                }
                if (decimal.Parse(_value[1].Trim()) < 73 || decimal.Parse(_value[1].Trim()) > 135)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static string ReplaceMiddle(this string value, char charToPutIn)
        {
            try
            {
                var builder = new StringBuilder(value);
                builder[(value.Length / 2) + 2] = charToPutIn;
                builder[(value.Length / 2) - 1] = charToPutIn;
                builder[(value.Length / 2)] = charToPutIn;
                builder[(value.Length / 2) + 1] = charToPutIn;
                builder[(value.Length / 2) + 2] = charToPutIn;
                return builder.ToString();
            }
            catch (Exception)
            {
                return value;
            }

        }

        public static string ReplaceStarFroName(this string value, char charToPutIn)
        {
            try
            {
                var builder = new StringBuilder(value);

                builder[1] = charToPutIn;

                return builder.ToString();
            }
            catch (Exception)
            {
                return value;
            }

        }
    }
}
