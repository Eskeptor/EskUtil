// ======================================================================================================
// File Name        : ExtensionUtil.cs
// Project          : CSUtil
// Last Update      : 2024.09.16 - yc.jeon
// ======================================================================================================

using System.Runtime.InteropServices;
using System.Text;

namespace CSUtil
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
        public static string ConvertToString(this byte[] bytes, bool isRemoveEOF = false)
        {
            return isRemoveEOF ? Encoding.ASCII.GetString(bytes).Replace("\0", "") : Encoding.ASCII.GetString(bytes);
        }

        /// <summary>
        /// 문자열 데이터(HEX)를 Decimal 값으로 변환하는 함수 <br/>
        /// (Convert.ToInt32를 사용함으로 그에 따른 Exception 발생 가능)
        /// </summary>
        /// <param name="str">문자열 데이터</param>
        /// <returns>
        /// 16진수를 10진수로 변환한 데이터 <br/>
        /// </returns>
        public static int HexToDec(this string str)
        {
            string hex = $"{str.ToUpperInvariant():X6}";
            return Convert.ToInt32(hex, 16);
        }

        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수 <br/>
        /// (StringBuilder 사용)
        /// </summary>
        /// <typeparam name="T">일반적인 자료형(ToString이 있어야함)</typeparam>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        public static string OneLineString<T>(this T[] values, char token)
        {
            int totalLength = 0;
            string[] stringArray = new string[values.Length];
            for (int i = 0; i < values.Length; ++i)
            {
                stringArray[i] = values[i]?.ToString() ?? string.Empty;
                totalLength += stringArray[i].Length;
            }

            // 전체 문자열 길이 + token 길이
            StringBuilder stringBuilder = new StringBuilder(totalLength + values.Length);
            for (int i = 0; i < values.Length; ++i)
            {
                stringBuilder.Append(stringArray[i]);
                if (i != values.Length - 1)
                {
                    stringBuilder.Append(token);
                }
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
        public static string OneLineString<T>(this ICollection<T> values, char token)
        {
            int totalLength = 0;
            string[] stringArray = new string[values.Count];
            int index = 0;
            foreach (T item in values)
            {
                stringArray[index] = item?.ToString() ?? string.Empty;
                totalLength += stringArray[index].Length;
                ++index;
            }

            // 전체 문자열 길이 + token 길이
            StringBuilder stringBuilder = new StringBuilder(totalLength + values.Count);
            for (int i = 0; i < values.Count; ++i)
            {
                stringBuilder.Append(stringArray[i]);
                if (i != values.Count - 1)
                {
                    stringBuilder.Append(token);
                }
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
        public static string OneLineString(this byte[] values, char token, bool isHex = false)
        {
            int totalLength = 0;
            string[] stringArray = new string[values.Length];
            if (isHex)
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    stringArray[i] = values[i].ToString("X");
                    totalLength += stringArray[i].Length;
                }
            }
            else
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    stringArray[i] = values[i].ToString();
                    totalLength += stringArray[i].Length;
                }
            }

            // 전체 문자열 길이 + token 길이
            StringBuilder stringBuilder = new StringBuilder(totalLength + values.Length);
            for (int i = 0; i < values.Length; ++i)
            {
                stringBuilder.Append(stringArray[i]);
                if (i != values.Length - 1)
                {
                    stringBuilder.Append(token);
                }
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
        public static string OneLineString(this ICollection<byte> values, char token, bool isHex = false)
        {
            int totalLength = 0;
            int index = 0;
            string[] stringArray = new string[values.Count];
            if (isHex)
            {
                foreach (byte item in values)
                {
                    stringArray[index] = item.ToString("X");
                    totalLength += stringArray[index].Length;
                    ++index;
                }
            }
            else
            {
                foreach (byte item in values)
                {
                    stringArray[index] = item.ToString();
                    totalLength += stringArray[index].Length;
                    ++index;
                }
            }

            // 전체 문자열 길이 + token 길이
            StringBuilder stringBuilder = new StringBuilder(totalLength + values.Count);
            for (int i = 0; i < values.Count; ++i)
            {
                stringBuilder.Append(stringArray[i]);
                if (i != values.Count - 1)
                {
                    stringBuilder.Append(token);
                }
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
        public static string OneLineString2<T>(this T[] values, char token)
        {
            string result = string.Join($"{token}", values);
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
        public static string OneLineString2<T>(this ICollection<T> values, char token)
        {
            string result = string.Join($"{token}", values);
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
        public static int FindName(this Type[] types, string name, string priorityString = "")
        {
            List<Tuple<int, string>> finds = new List<Tuple<int, string>>();

            for (int i = 0; i < types.Length; ++i)
            {
                if (types[i].Name.Equals(name, StringComparison.Ordinal))
                {
                    if (string.IsNullOrEmpty(priorityString))
                    {
                        return i;
                    }
                    finds.Add(new Tuple<int, string>(i, types[i].FullName!));
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

            return -1;
        }

        /// <summary>
        /// <paramref name="text"/>이 배열을 나타내는 문자열일때 해당 배열의 크기 또는 인덱스를 반환하는 함수 <br/><br/>
        /// 예1) TestData[55] -> 55 반환 <br/>
        /// 예2) TestData -> -1 반환 <br/>
        /// 예3) TestData[] -> -1 반환
        /// </summary>
        /// <param name="text">문자열</param>
        /// <returns>
        /// -1: 배열을 나타내는 문자열이 아님 <br/>
        /// 0~: 배열의 크기 또는 인덱스
        /// </returns>
        public static int GetArrayNumber(this string text)
        {
            int arrayBracketIdx = text.IndexOf('[');
            int arrayBracketEndIdx = text.IndexOf(']');

            if (arrayBracketIdx == -1 ||
                arrayBracketEndIdx == -1)
            {
                return -1;
            }

            string index = text.Substring(arrayBracketIdx + 1, arrayBracketEndIdx - arrayBracketIdx - 1);
            if (!int.TryParse(index, out int arraySize))
            {
                return -1;
            }

            return arraySize;
        }

        /// <summary>
        /// object를 byte 배열로 변환하는 함수
        /// </summary>
        /// <param name="obj">byte 배열로 변환할 object</param>
        /// <returns>
        /// 변환된 byte 배열
        /// </returns>
        public static byte[] ToByteDatas(this object obj)
        {
            int size = Marshal.SizeOf(obj);
            byte[] arr = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(obj, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);

            return arr;
        }

        /// <summary>
        /// byte 배열 데이터를 struct로 변환하는 함수
        /// </summary>
        /// <typeparam name="T">struct</typeparam>
        /// <param name="bytes">struct로 변환할 byte 배열 데이터</param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"><paramref name="bytes"/>의 배열길이와 <typeparamref name="T"/>의 데이터 크기가 다른 경우</exception>
        public static T ByteToStruct<T>(this byte[] bytes) where T : struct
        {
            int size = Marshal.SizeOf(typeof(T));
            if (size > bytes.Length)
            {
                throw new InvalidCastException();
            }

            T obj = default;
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(bytes, 0, ptr, size);
            if (Marshal.PtrToStructure(ptr, typeof(T)) is T convData)
            {
                obj = convData;
            }
            Marshal.FreeHGlobal(ptr);

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
    }
}
