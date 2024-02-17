// ======================================================================================================
// File Name        : ExtensionUtil.cs
// Project          : CSUtil
// Last Update      : 2024.01.27 - yc.jeon
// ======================================================================================================

using System.Text;

namespace CSUtil
{
    /// <summary>
    /// Extension Class
    /// </summary>
    public static class ExtensionUtil
    {
        /// <summary>
        /// 배열 데이터를 한 줄의 문자열로 중간 구분자를 사용하여 만드는 함수
        /// </summary>
        /// <typeparam name="T">일반적인 자료형(ToString이 있어야함)</typeparam>
        /// <param name="values">배열 데이터</param>
        /// <param name="token">중간 구분자</param>
        /// <returns>한 줄로 만들어진 문자열</returns>
        public static string OneLineStringWidthChar<T>(this T[] values, char token)
        {
            StringBuilder stringBuilder = new StringBuilder(128);

            for (int i = 0; i < values.Length; ++i)
            {
                stringBuilder.Append(values[i]!.ToString());
                if (i != values.Length - 1)
                {
                    stringBuilder.Append(token);
                }
            }

            return stringBuilder.ToString();
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
                if (types[i].Name.Equals(name))
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
    }
}
