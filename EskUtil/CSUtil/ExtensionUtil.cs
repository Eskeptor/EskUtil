// ======================================================================================================
// File Name        : ExtensionUtil.cs
// Project          : CSUtil
// Last Update      : 2026.07.27 - yc.jeon (Eskeptor)
// ======================================================================================================

using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Esk.GearForge.CSUtil
{
    /// <summary>
    /// Extension Class
    /// </summary>
    public static class ExtensionUtil
    {
        /// <summary>
        /// byte 배열의 값을 string(ASCII)으로 변환하는 함수 <br/>
        /// (Encoding.ASCII.GetString을 사용함으로 그에 따른 Exception 발생 가능)
        /// </summary>
        /// <param name="bytes">변환할 byte 배열 값</param>
        /// <param name="isRemoveEOF">문장의 끝 문자(\0)를 제거할지 유무</param>
        /// <returns>
        /// byte 배열의 값을 string으로 변환한 문자열 값
        /// </returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DecoderFallbackException"></exception>
        public static string ConvertToString(this byte[] bytes, bool isRemoveEOF = false)
        {
            string result = Encoding.ASCII.GetString(bytes);
            return isRemoveEOF ? result.Replace("\0", "") : result;
        }

        /// <summary>
        /// 문자열 데이터(HEX)를 Decimal 값으로 변환하는 함수 <br/>
        /// (Convert.ToInt32를 사용함으로 그에 따른 Exception 발생 가능)
        /// </summary>
        /// <param name="str">문자열 데이터</param>
        /// <returns>
        /// 16진수를 10진수로 변환한 데이터 <br/>
        /// </returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static int HexToDec(this string str)
        {
            return Convert.ToInt32(str, 16);
        }

        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (StringBuilder 사용)
        /// </summary>
        /// <typeparam name="T">일반적인 자료형(ToString이 있어야함)</typeparam>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string OneLineString<T>(this T[] values, char token)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(values[0] != null ? values[0].ToString() : string.Empty);
            for (int i = 1; i < values.Length; ++i)
            {
                stringBuilder.Append(token);
                stringBuilder.Append(values[i] != null ? values[i].ToString() : string.Empty);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (StringBuilder 사용)
        /// </summary>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string OneLineString(this string[] values, char token)
        {
            string result = OneLineString(values, token, values.Length);
            return result;
        }

        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (StringBuilder 사용)
        /// </summary>
        /// <typeparam name="T">일반적인 자료형(ToString이 있어야함)</typeparam>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string OneLineString<T>(this T[] values, string token)
        {
            string result = OneLineString(values, token, values.Length);
            return result;
        }

        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (StringBuilder 사용)
        /// </summary>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string OneLineString(this string[] values, string token)
        {
            string result = OneLineString(values, token, values.Length);
            return result;
        }

        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (StringBuilder 사용)
        /// </summary>
        /// <typeparam name="T">일반적인 자료형(ToString이 있어야함)</typeparam>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <param name="count">배열의 인자 개수 (이 개수만큼만 배열의 데이터를 문자열로 만듬)</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string OneLineString<T>(this T[] values, char token, int count)
        {
            int length = count > values.Length ? values.Length : count;
            if (values.Length == 0)
            {
                return string.Empty;
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(values[0] != null ? values[0].ToString() : string.Empty);
            for (int i = 1; i < length; ++i)
            {
                stringBuilder.Append(token);
                stringBuilder.Append(values[i] != null ? values[i].ToString() : string.Empty);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (StringBuilder 사용)
        /// </summary>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <param name="count">배열의 인자 개수 (이 개수만큼만 배열의 데이터를 문자열로 만듬)</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string OneLineString(this string[] values, char token, int count)
        {
            int length = count > values.Length ? values.Length : count;
            if (values.Length == 0)
            {
                return string.Empty;
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(values[0] ?? string.Empty);
            for (int i = 1; i < length; ++i)
            {
                stringBuilder.Append(token);
                stringBuilder.Append(values[i] ?? string.Empty);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (StringBuilder 사용)
        /// </summary>
        /// <typeparam name="T">일반적인 자료형(ToString이 있어야함)</typeparam>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <param name="count">배열의 인자 개수 (이 개수만큼만 배열의 데이터를 문자열로 만듬)</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string OneLineString<T>(this T[] values, string token, int count)
        {
            int length = count > values.Length ? values.Length : count;
            if (values.Length == 0)
            {
                return string.Empty;
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(values[0] != null ? values[0].ToString() : string.Empty);
            for (int i = 1; i < length; ++i)
            {
                stringBuilder.Append(token);
                stringBuilder.Append(values[i] != null ? values[i].ToString() : string.Empty);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (StringBuilder 사용)
        /// </summary>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <param name="count">배열의 인자 개수 (이 개수만큼만 배열의 데이터를 문자열로 만듬)</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string OneLineString(this string[] values, string token, int count)
        {
            int length = count > values.Length ? values.Length : count;
            if (values.Length == 0)
            {
                return string.Empty;
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(values[0] ?? string.Empty);
            for (int i = 1; i < length; ++i)
            {
                stringBuilder.Append(token);
                stringBuilder.Append(values[i] ?? string.Empty);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (StringBuilder 사용)
        /// </summary>
        /// <typeparam name="T">일반적인 자료형(ToString이 있어야함)</typeparam>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string OneLineString<T>(this ICollection<T> values, char token)
        {
            string result = OneLineString(values, token, values.Count);
            return result;
        }

        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (StringBuilder 사용)
        /// </summary>
        /// <typeparam name="T">일반적인 자료형(ToString이 있어야함)</typeparam>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string OneLineString(this ICollection<string> values, char token)
        {
            string result = OneLineString(values, token, values.Count);
            return result;
        }

        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (StringBuilder 사용)
        /// </summary>
        /// <typeparam name="T">일반적인 자료형(ToString이 있어야함)</typeparam>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string OneLineString<T>(this ICollection<T> values, string token)
        {
            string result = OneLineString(values, token, values.Count);
            return result;
        }

        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (StringBuilder 사용)
        /// </summary>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string OneLineString(this ICollection<string> values, string token)
        {
            string result = OneLineString(values, token, values.Count);
            return result;
        }

        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (StringBuilder 사용)
        /// </summary>
        /// <typeparam name="T">일반적인 자료형(ToString이 있어야함)</typeparam>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <param name="count">배열의 인자 개수 (이 개수만큼만 배열의 데이터를 문자열로 만듬)</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string OneLineString<T>(this ICollection<T> values, char token, int count)
        {
            int length = count > values.Count ? values.Count : count;
            if (length == 0)
            {
                return string.Empty;
            }
            StringBuilder stringBuilder = new StringBuilder();
            int index = 0;
            using (IEnumerator<T> enumerator = values.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    if (index >= length)
                    {
                        break;
                    }
                    stringBuilder.Append(enumerator.Current != null ? enumerator.Current.ToString() : string.Empty);
                    stringBuilder.Append(token);
                    ++index;
                }
            }
            if (stringBuilder.Length > 0)
            {
                --stringBuilder.Length;
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (StringBuilder 사용)
        /// </summary>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <param name="count">배열의 인자 개수 (이 개수만큼만 배열의 데이터를 문자열로 만듬)</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string OneLineString(this ICollection<string> values, char token, int count)
        {
            int length = count > values.Count ? values.Count : count;
            if (length == 0)
            {
                return string.Empty;
            }
            StringBuilder stringBuilder = new StringBuilder();
            using (IEnumerator<string> enumerator = values.GetEnumerator())
            {
                int index = 0;
                while (enumerator.MoveNext())
                {
                    if (index >= length)
                    {
                        break;
                    }
                    stringBuilder.Append(enumerator.Current ?? string.Empty);
                    stringBuilder.Append(token);
                    ++index;
                }
            }
            if (stringBuilder.Length > 0)
            {
                --stringBuilder.Length;
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (StringBuilder 사용)
        /// </summary>
        /// <typeparam name="T">일반적인 자료형(ToString이 있어야함)</typeparam>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <param name="count">배열의 인자 개수 (이 개수만큼만 배열의 데이터를 문자열로 만듬)</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string OneLineString<T>(this ICollection<T> values, string token, int count)
        {
            int length = count > values.Count ? values.Count : count;
            if (length == 0)
            {
                return string.Empty;
            }
            StringBuilder stringBuilder = new StringBuilder();
            using (IEnumerator<T> enumerator = values.GetEnumerator())
            {
                int index = 0;
                while (enumerator.MoveNext())
                {
                    if (index >= length)
                    {
                        break;
                    }
                    stringBuilder.Append(enumerator.Current != null ? enumerator.Current.ToString() : string.Empty);
                    stringBuilder.Append(token);
                    ++index;
                }
            }
            if (stringBuilder.Length > token.Length)
            {
                stringBuilder.Length -= token.Length;
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (StringBuilder 사용)
        /// </summary>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <param name="count">배열의 인자 개수 (이 개수만큼만 배열의 데이터를 문자열로 만듬)</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string OneLineString(this ICollection<string> values, string token, int count)
        {
            int length = count > values.Count ? values.Count : count;
            if (length == 0)
            {
                return string.Empty;
            }
            StringBuilder stringBuilder = new StringBuilder();
            using (IEnumerator<string> enumerator = values.GetEnumerator())
            {
                int index = 0;
                while (enumerator.MoveNext())
                {
                    if (index >= length)
                    {
                        break;
                    }
                    stringBuilder.Append(enumerator.Current ?? string.Empty);
                    stringBuilder.Append(token);
                    ++index;
                }
            }
            if (stringBuilder.Length > token.Length)
            {
                stringBuilder.Length -= token.Length;
            }
            return stringBuilder.ToString();
        }


        /// <summary>
        /// 바이트 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (StringBuilder 사용)
        /// </summary>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <param name="isHex">16진수로 출력할지 유무</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string OneLineString(this byte[] values, char token, bool isHex = false)
        {
            string result = OneLineString(values, token, values.Length, isHex);
            return result;
        }

        /// <summary>
        /// 바이트 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (StringBuilder 사용)
        /// </summary>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <param name="count">배열의 인자 개수 (이 개수만큼만 배열의 데이터를 문자열로 만듬)</param>
        /// <param name="isHex">16진수로 출력할지 유무</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string OneLineString(this byte[] values, char token, int count, bool isHex = false)
        {
            int length = count > values.Length ? values.Length : count;
            if (length == 0)
            {
                return string.Empty;
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(isHex ? values[0].ToStringInvariantCulture("X") : values[0].ToStringInvariantCulture());
            for (int i = 1; i < length; ++i)
            {
                stringBuilder.Append(token);
                stringBuilder.Append(isHex ? values[i].ToStringInvariantCulture("X") : values[i].ToStringInvariantCulture());
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 바이트 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (StringBuilder 사용)
        /// </summary>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <param name="isHex">16진수로 출력할지 유무</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string OneLineString(this ICollection<byte> values, char token, bool isHex = false)
        {
            string result = OneLineString(values, token, values.Count, isHex);
            return result;
        }

        /// <summary>
        /// 바이트 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (StringBuilder 사용)
        /// </summary>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <param name="count">배열의 인자 개수 (이 개수만큼만 배열의 데이터를 문자열로 만듬)</param>
        /// <param name="isHex">16진수로 출력할지 유무</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string OneLineString(this ICollection<byte> values, char token, int count, bool isHex = false)
        {
            int length = count > values.Count ? values.Count : count;
            if (length == 0)
            {
                return string.Empty;
            }
            StringBuilder stringBuilder = new StringBuilder();
            using (IEnumerator<byte> enumerator = values.GetEnumerator())
            {
                int index = 0;
                while (enumerator.MoveNext())
                {
                    if (index >= length)
                    {
                        break;
                    }
                    stringBuilder.Append(isHex ? enumerator.Current.ToStringInvariantCulture("X") : enumerator.Current.ToStringInvariantCulture());
                    stringBuilder.Append(token);
                    ++index;
                }
            }
            if (stringBuilder.Length > 0)
            {
                --stringBuilder.Length;
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (string.Join 사용)
        /// </summary>
        /// <typeparam name="T">일반적인 자료형(ToString이 있어야함)</typeparam>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string OneLineString2<T>(this T[] values, char token)
        { 
            return string.Join(token.ToString(), values);
        }

        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (string.Join 사용)
        /// </summary>
        /// <typeparam name="T">일반적인 자료형(ToString이 있어야함)</typeparam>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string OneLineString2<T>(this T[] values, string token)
        {
            return string.Join(token, values);
        }

        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (string.Join 사용)
        /// </summary>
        /// <typeparam name="T">일반적인 자료형(ToString이 있어야함)</typeparam>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string OneLineString2<T>(this ICollection<T> values, char token)
        {
            string result = string.Join(token.ToString(), values);
            return result;
        }

        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (string.Join 사용)
        /// </summary>
        /// <typeparam name="T">일반적인 자료형(ToString이 있어야함)</typeparam>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string OneLineString2<T>(this ICollection<T> values, string token)
        {
            string result = string.Join(token, values);
            return result;
        }

        /// <summary>
        /// 배열의 데이터를 <paramref name="value"/>로 초기화 하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="value"></param>
        public static void Populate<T>(this T[] array, T value)
        {
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = value;
            }
        }

        /// <summary>
        /// Type 배열 안에서 Type.Name을 찾는 함수
        /// </summary>
        /// <param name="types">Type 배열</param>
        /// <param name="name">찾을 Type.Name</param>
        /// <param name="priorityString">
        /// 찾은 데이터가 여러 개 일때 우선순위로 Contains를 할 문자열<br/>
        /// (값이 비어있는 경우 우선순위를 찾지 않음)
        /// </param>
        /// <returns>찾은 위치의 인덱스값</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static int FindName(this Type[] types, string name, string priorityString = "")
        {
            bool usePriority = !string.IsNullOrWhiteSpace(priorityString);
            if (usePriority)
            {
                List<Tuple<int, string>> finds = new List<Tuple<int, string>>(types.Length);
                for (int i = 0; i < types.Length; ++i)
                {
                    if (types[i].Name.EqualsOrdinal(name))
                    {
                        finds.Add(new Tuple<int, string>(i, types[i].FullName));
                    }
                }

                if (finds.Count > 0)
                {
                    for (int i = 0; i < finds.Count; ++i)
                    {
                        if (finds[i].Item2.Contains(priorityString))
                        {
                            return finds[i].Item1;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < types.Length; ++i)
                {
                    if (types[i].Name.EqualsOrdinal(name))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// <paramref name="text"/>이 배열을 나타내는 문자열일때 해당 배열의 인덱스를 반환하는 함수 <br/>
        /// 예1) TestData[55] -> 55 반환 <br/>
        /// 예2) TestData -> -1 반환 <br/>
        /// 예3) TestData[] -> -1 반환
        /// </summary>
        /// <param name="text">문자열</param>
        /// <returns>
        /// -1: 배열을 나타내는 문자열이 아님 <br/>
        /// 0~: 배열의 인덱스
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int GetArrayIndex(this string text)
        {
            int arrayBracketIdx = text.IndexOf('[');
            int arrayBracketEndIdx = text.IndexOf(']');

            if (arrayBracketIdx == -1 ||
                arrayBracketEndIdx == -1)
            {
                return -1;
            }

            string index = text.Substring(arrayBracketIdx + 1, arrayBracketEndIdx - arrayBracketIdx - 1);
            if (!int.TryParse(index, out int arrayIndex))
            {
                return -1;
            }

            return arrayIndex;
        }

        /// <summary>
        /// object를 byte 배열로 변환하는 함수
        /// </summary>
        /// <param name="obj">byte 배열로 변환할 object</param>
        /// <returns>
        /// 변환된 byte 배열 (비정상 종료시 null 반환)
        /// </returns>
        /// <exception cref="OutOfMemoryException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MissingMethodException"></exception>
        public static byte[] ToByteDatas<T>(this T obj) where T : struct
        {
            int size = Marshal.SizeOf(obj);
            byte[] arr = new byte[size];
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(obj, ptr, true);
                Marshal.Copy(ptr, arr, 0, size);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                arr = null;
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }

            return arr;
        }

        /// <summary>
        /// byte 배열 데이터를 struct로 변환하는 함수
        /// </summary>
        /// <typeparam name="T">struct</typeparam>
        /// <param name="bytes">struct로 변환할 byte 배열 데이터</param>
        /// <returns></returns>
        /// <exception cref="OutOfMemoryException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MissingMethodException"></exception>
        /// <exception cref="InvalidCastException"><paramref name="bytes"/>의 배열길이와 <typeparamref name="T"/>의 데이터 크기가 다른 경우</exception>
        public static T ByteToStruct<T>(this byte[] bytes) where T : struct
        {
            int size = Marshal.SizeOf<T>();
            if (size > bytes.Length)
            {
                throw new InvalidCastException();
            }

            IntPtr ptr = IntPtr.Zero;
            T obj = default;
            try
            {
                ptr = Marshal.AllocHGlobal(size);
                Marshal.Copy(bytes, 0, ptr, size);
                obj = Marshal.PtrToStructure<T>(ptr);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }

            return obj;
        }

        /// <summary>
        /// 반복되는 문자열을 만드는 함수 <br/>
        /// ex: str=AB, repeatCnt=3  ==> ABABAB 반환
        /// </summary>
        /// <param name="str">반복할 문자열</param>
        /// <param name="repeatCnt">반복 횟수</param>
        /// <returns></returns>
        public static string Repeat(this string str, int repeatCnt)
        {
            if (repeatCnt <= 1)
            {
                return str;
            }

            StringBuilder stringBuilder = new StringBuilder(str.Length * repeatCnt);
            for (int i = 0; i < repeatCnt; ++i)
            {
                stringBuilder.Append(str);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// String 비교 (바이트 비교)
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool EqualsOrdinal(this string str1, string str2)
        {
            return str1.Equals(str2, StringComparison.Ordinal);
        }

        /// <summary>
        /// String 비교 (대소문자 무시 비교)
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool EqualsIgnoreCase(this string str1, string str2)
        {
            return str1.Equals(str2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// String StartWith (바이트 비교)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static bool StartsWithOrdinal(this string str, string value)
        {
            return str.StartsWith(value, StringComparison.Ordinal);
        }

        /// <summary>
        /// String StartWith (대소문자 무시 비교)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static bool StartsWithIgnoreCase(this string str, string value)
        {
            return str.StartsWith(value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// String EndsWith (바이트 비교)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static bool EndsWithOrdinal(this string str, string value)
        {
            return str.EndsWith(value, StringComparison.Ordinal);
        }
        /// <summary>
        /// String EndsWith (대소문자 무시 비교)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static bool EndsWithIgnoreCase(this string str, string value)
        {
            return str.EndsWith(value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// String IndexOf (바이트 비교)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int IndexOfOrdinal(this string str, string value)
        {
            return str.IndexOf(value, StringComparison.Ordinal);
        }
        /// <summary>
        /// String IndexOf (바이트 비교)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int IndexOfOrdinal(this string str, string value, int startIndex)
        {
            return str.IndexOf(value, startIndex, StringComparison.Ordinal);
        }
        /// <summary>
        /// String IndexOf (바이트 비교)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int IndexOfOrdinal(this string str, string value, int startIndex, int count)
        {
            return str.IndexOf(value, startIndex, count, StringComparison.Ordinal);
        }

        /// <summary>
        /// String IndexOf (대소문자 무시 비교)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int IndexOfIgnoreCase(this string str, string value)
        {
            return str.IndexOf(value, StringComparison.OrdinalIgnoreCase);
        }
        /// <summary>
        /// String IndexOf (대소문자 무시 비교)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int IndexOfIgnoreCase(this string str, string value, int startIndex)
        {
            return str.IndexOf(value, startIndex, StringComparison.OrdinalIgnoreCase);
        }
        /// <summary>
        /// String IndexOf (대소문자 무시 비교)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int IndexOfIgnoreCase(this string str, string value, int startIndex, int count)
        {
            return str.IndexOf(value, startIndex, count, StringComparison.OrdinalIgnoreCase);
        }
        /// <summary>
        /// String LastIndexOf (바이트 비교)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int LastIndexOfOrdinal(this string str, string value)
        {
            return str.LastIndexOf(value, StringComparison.Ordinal);
        }
        /// <summary>
        /// String LastIndexOf (바이트 비교)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int LastIndexOfOrdinal(this string str, string value, int startIndex)
        {
            return str.LastIndexOf(value, startIndex, StringComparison.Ordinal);
        }
        /// <summary>
        /// String LastIndexOf (바이트 비교)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int LastIndexOfOrdinal(this string str, string value, int startIndex, int count)
        {
            return str.LastIndexOf(value, startIndex, count, StringComparison.Ordinal);
        }
        /// <summary>
        /// String LastIndexOf (대소문자 무시 비교)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int LastIndexOfIgnoreCase(this string str, string value)
        {
            return str.LastIndexOf(value, StringComparison.OrdinalIgnoreCase);
        }
        /// <summary>
        /// String LastIndexOf (대소문자 무시 비교)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int LastIndexOfIgnoreCase(this string str, string value, int startIndex)
        {
            return str.LastIndexOf(value, startIndex, StringComparison.OrdinalIgnoreCase);
        }
        /// <summary>
        /// String LastIndexOf (대소문자 무시 비교)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int LastIndexOfIgnoreCase(this string str, string value, int startIndex, int count)
        {
            return str.LastIndexOf(value, startIndex, count, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// String Split (공백 제거)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separators"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string[] SplitRemoveEmpty(this string str, params char[] separators)
        {
            return str.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// String Split (공백 제거)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separators"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string[] SplitRemoveEmpty(this string str, string[] separators)
        {
            return str.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// String Split (공백 포함)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separators"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string[] SplitIncludeEmpty(this string str, params char[] separators)
        {
            return str.Split(separators, StringSplitOptions.None);
        }

        /// <summary>
        /// String Split (공백 포함)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separators"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string[] SplitIncludeEmpty(this string str, string[] separators)
        {
            return str.Split(separators, StringSplitOptions.None);
        }

        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringCurrentCulture(this bool source)
        {
            return source.ToString(CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringCurrentCulture(this byte source)
        {
            return source.ToString(CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static string ToStringCurrentCulture(this byte source, string format)
        {
            return source.ToString(format, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringCurrentCulture(this sbyte source)
        {
            return source.ToString(CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static string ToStringCurrentCulture(this sbyte source, string format)
        {
            return source.ToString(format, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringCurrentCulture(this short source)
        {
            return source.ToString(CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToStringCurrentCulture(this short source, string format)
        {
            return source.ToString(format, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringCurrentCulture(this ushort source)
        {
            return source.ToString(CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static string ToStringCurrentCulture(this ushort source, string format)
        {
            return source.ToString(format, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringCurrentCulture(this int source)
        {
            return source.ToString(CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static string ToStringCurrentCulture(this int source, string format)
        {
            return source.ToString(format, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringCurrentCulture(this uint source)
        {
            return source.ToString(CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static string ToStringCurrentCulture(this uint source, string format)
        {
            return source.ToString(format, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringCurrentCulture(this long source)
        {
            return source.ToString(CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static string ToStringCurrentCulture(this long source, string format)
        {
            return source.ToString(format, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringCurrentCulture(this ulong source)
        {
            return source.ToString(CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static string ToStringCurrentCulture(this ulong source, string format)
        {
            return source.ToString(format, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringCurrentCulture(this float source)
        {
            return source.ToString(CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToStringCurrentCulture(this float source, string format)
        {
            return source.ToString(format, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringCurrentCulture(this double source)
        {
            return source.ToString(CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToStringCurrentCulture(this double source, string format)
        {
            return source.ToString(format, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringCurrentCulture(this decimal source)
        {
            return source.ToString(CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Current Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static string ToStringCurrentCulture(this decimal source, string format)
        {
            return source.ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringInvariantCulture(this bool source)
        {
            return source.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringInvariantCulture(this byte source)
        {
            return source.ToString(CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static string ToStringInvariantCulture(this byte source, string format)
        {
            return source.ToString(format, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringInvariantCulture(this sbyte source)
        {
            return source.ToString(CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static string ToStringInvariantCulture(this sbyte source, string format)
        {
            return source.ToString(format, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringInvariantCulture(this short source)
        {
            return source.ToString(CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToStringInvariantCulture(this short source, string format)
        {
            return source.ToString(format, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringInvariantCulture(this ushort source)
        {
            return source.ToString(CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static string ToStringInvariantCulture(this ushort source, string format)
        {
            return source.ToString(format, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringInvariantCulture(this int source)
        {
            return source.ToString(CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static string ToStringInvariantCulture(this int source, string format)
        {
            return source.ToString(format, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringInvariantCulture(this uint source)
        {
            return source.ToString(CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static string ToStringInvariantCulture(this uint source, string format)
        {
            return source.ToString(format, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringInvariantCulture(this long source)
        {
            return source.ToString(CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static string ToStringInvariantCulture(this long source, string format)
        {
            return source.ToString(format, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringInvariantCulture(this ulong source)
        {
            return source.ToString(CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static string ToStringInvariantCulture(this ulong source, string format)
        {
            return source.ToString(format, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringInvariantCulture(this float source)
        {
            return source.ToString(CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToStringInvariantCulture(this float source, string format)
        {
            return source.ToString(format, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringInvariantCulture(this double source)
        {
            return source.ToString(CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToStringInvariantCulture(this double source, string format)
        {
            return source.ToString(format, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToStringInvariantCulture(this decimal source)
        {
            return source.ToString(CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Primitive Type ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static string ToStringInvariantCulture(this decimal source, string format)
        {
            return source.ToString(format, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// DateTime ToString (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="date"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string ToStringInvariantCulture(this DateTime date, string format)
        {
            return date.ToString(format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// String 값을 sbyte(int8)로 변환하는 함수 (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static sbyte ParseSByte(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }
            return sbyte.Parse(value, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// String 값을 byte(uint8)로 변환하는 함수 (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static byte ParseByte(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }
            return byte.Parse(value, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// String 값을 short(int16)로 변환하는 함수 (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static short ParseShort(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }
            return short.Parse(value, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// String 값을 ushort(uint16)로 변환하는 함수 (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static ushort ParseUShort(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }
            return ushort.Parse(value, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// String 값을 int(int32)로 변환하는 함수 (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static int ParseInt(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }
            return int.Parse(value, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// String 값을 uint(uint32)로 변환하는 함수 (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static uint ParseUInt(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }
            return uint.Parse(value, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// String 값을 long(int64)로 변환하는 함수 (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static long ParseLong(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }
            return long.Parse(value, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// String 값을 ulong(uint64)로 변환하는 함수 (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static ulong ParseULong(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }
            return ulong.Parse(value, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// String 값을 decimal로 변환하는 함수 (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static decimal ParseDecimal(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }
            return decimal.Parse(value, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// String 값을 float으로 변환하는 함수 (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static float ParseFloat(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0.0f;
            }
            return float.Parse(value, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// String 값을 double로 변환하는 함수 (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static double ParseDouble(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0.0;
            }
            return double.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// String 값을 DateTime으로 변환하는 함수 (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        public static DateTime ParseDateTime(this string value)
        {
            return DateTime.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// String 값을 DateTime으로 변환하는 함수 (Culture Type: Invariant Culture)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        public static DateTime ParseExactDateTime(this string value, string format)
        {
            return DateTime.ParseExact(value, format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// double 데이터를 ToString 한 값을 E지수표기법으로 변환하는 함수 <br/>
        /// (3E-09 -&gt; 3.00E-9)
        /// </summary>
        /// <param name="value">변환할 double 데이터를 ToString한 문자열 데이터</param>
        /// <returns></returns>
        public static string ToExponential(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }
            if (!double.TryParse(value, out double conv))
            {
                return value;
            }

            string exValue = conv.ToStringInvariantCulture("E2");
            int exponent = exValue.IndexOfOrdinal("E") + 2;
            int last = 0;
            for (int i = exponent; i < exValue.Length; ++i)
            {
                string number = exValue.Substring(i, 1);
                if (number.ParseInt() != 0)
                {
                    last = i;
                    break;
                }
                else
                {
                    last = exValue.Length - 1;
                }
            }

            string result = $"{exValue.Substring(0, exponent)}{exValue.Substring(last)}";
            return result;
        }

        /// <summary>
        /// double 데이터를 E지수표기법으로 변환하는 함수 <br/>
        /// (3E-09 -&gt; 3.00E-9)
        /// </summary>
        /// <param name="value">변환할 double 데이터</param>
        /// <returns></returns>
        public static string ToExponential(this double value)
        {
            return ToExponential(value.ToStringInvariantCulture());
        }

        /// <summary>
        /// 실수값의 소수점을 지정한 자리수로 자르는 함수 <br/>
        /// </summary>
        /// <param name="value">원본값</param>
        /// <param name="decimalPlace">지정 자리수</param>
        /// <returns></returns>
        public static double DecimalPlace(this double value, int decimalPlace)
        {
            if (value == 0)
            {
                return 0;
            }
            double multiplier = Math.Pow(10, decimalPlace);
            double result = (double)(long)(value * multiplier) / multiplier;
            return result;
        }

        /// <summary>
        /// 실수값의 소수점을 지정한 자리수로 자르는 함수 <br/>
        /// </summary>
        /// <param name="value">원본값</param>
        /// <param name="multiplier">소수점 자리수에 따른 곱셈 값 (예: 소수점 2자리이면 100, 3자리이면 1000)</param>
        /// <returns></returns>
        public static double DecimalPlace(this double value, double multiplier)
        {
            if (value == 0)
            {
                return 0;
            }
            double result = (double)(long)(value * multiplier) / multiplier;
            return result;
        }

        /// <summary>
        /// 실수값의 소수점을 지정한 자리수로 자르는 함수 <br/>
        /// </summary>
        /// <param name="value">원본값</param>
        /// <param name="decimalPlace">지정 자리수</param>
        /// <returns></returns>
        public static float DecimalPlace(this float value, int decimalPlace)
        {
            if (value == 0)
            {
                return 0;
            }
            float multiplier = (float)Math.Pow(10, decimalPlace);
            float result = (float)(long)(value * multiplier) / multiplier;
            return result;
        }

        /// <summary>
        /// 실수값의 소수점을 지정한 자리수로 자르는 함수 <br/>
        /// </summary>
        /// <param name="value">원본값</param>
        /// <param name="multiplier">소수점 자리수에 따른 곱셈 값 (예: 소수점 2자리이면 100, 3자리이면 1000)</param>
        /// <returns></returns>
        public static float DecimalPlace(this float value, float multiplier)
        {
            if (value == 0)
            {
                return 0;
            }
            float result = (float)(long)(value * multiplier) / multiplier;
            return result;
        }
    }
}
