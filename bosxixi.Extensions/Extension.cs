﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace bosxixi.Extensions
{
    public static class Extension
    {
        public static string GetValidFileName(string origin)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                origin = origin.Replace(c, '_');
            }
            return origin;
        }
        public static string HowLongFromNow(this DateTime start)
        {
            var now = DateTime.UtcNow.AddHours(8);
            var dif = now - start;
            if (dif.TotalMinutes < 60)
            {
                return $"{(int)dif.TotalMinutes}m";
            }
            if (dif.TotalHours < 24)
            {
                return $"{(int)dif.TotalHours}h";
            }

            var date = String.Format(new System.Globalization.CultureInfo("en-us"), "{0:m}", start).Split();
            var month = date[0].Substring(0, 3);
            var day = date[1];
            return $"{month} {day}";
        }

        public static string GetName<T>(this T value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return Enum.GetName(typeof(T), value);
        }

        public static string HowLongFromNow(this DateTimeOffset start)
        {
            return HowLongFromNow(start.DateTime);
        }

        public static bool IsNumeric(this string s)
        {
            float output;
            return float.TryParse(s, out output);
        }

        public static bool IsValidFileName(this string fileName, string sourceFolder = null)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                return false;
            }
            if (sourceFolder != null && File.Exists(Path.Combine(sourceFolder, fileName)))
            {
                return false;
            }
            return true;
        }
        public static string GetValidFileName(this string fileName, string sourceFolder = null)
        {
            if (!fileName.IsValidFileName())
            {
                fileName = fileName.CleanFileName();
            }

            if (sourceFolder != null && Exists(fileName, sourceFolder))
            {
                int startNumber = 1;
                fileName = GetUnicFileName(fileName, ref startNumber, sourceFolder);
            }

            return fileName;
        }
        private static string CleanFileName(this string fileName) => new string(fileName.Where(m => !Path.GetInvalidFileNameChars().Contains(m)).ToArray<char>());
        private static bool Exists(this string currentFileName, string sourceFolder) => File.Exists(Path.Combine(sourceFolder, currentFileName));

        private static string GetUnicFileName(this string fileName, ref int currentNumber, string sourceFolder)
        {
            if (!Exists($"fileName_{currentNumber}", sourceFolder))
            {
                return $"fileName_{currentNumber}";
            }
            currentNumber++;
            return GetUnicFileName(fileName, ref currentNumber, sourceFolder);
        }
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

        public static void Foreach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        public static bool PublicInstancePropertiesEqual<T>(this T self, T to, params string[] ignore) where T : class
        {
            if (self != null && to != null)
            {
                var type = typeof(T);
                var ignoreList = new List<string>(ignore);
                var unequalProperties =
                    from pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    where !ignoreList.Contains(pi.Name)
                    let selfValue = type.GetProperty(pi.Name).GetValue(self, null)
                    let toValue = type.GetProperty(pi.Name).GetValue(to, null)
                    where selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue))
                    select selfValue;
                return !unequalProperties.Any();
            }
            return self == to;
        }
    }
}
