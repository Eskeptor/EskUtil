// ======================================================================================================
// File Name        : EnumUtil.cs
// Project          : CSUtil
// Last Update      : 2024.01.27 - yc.jeon
// ======================================================================================================

namespace CSUtil
{
    /// <summary>
    /// Enum 관련 유틸리티
    /// </summary>
    public static class EnumUtil
    {
        /// <summary>
        /// Enum과 Int간의 변환을 위한 구조체
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private struct EStruct<T> where T : struct
        {
            public int IntValue;
            public T Enum;
        }

        /// <summary>
        /// Enum 값을 Int 값으로 변환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e">변환할 Enum</param>
        /// <returns>변환된 Int 값</returns>
        public static int EnumToInt<T>(T e) where T : struct
        {
            EStruct<T> st;
            st.Enum = e;

            unsafe
            {
                int* pInt = &st.IntValue;
                pInt += 1;
                return *pInt;
            }
        }

        /// <summary>
        /// Int 값을 Enum 값으로 변환하는 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">변환할 Int</param>
        /// <returns>변환된 Enum 값</returns>
        public static T IntToEnum<T>(int value) where T : struct
        {
            EStruct<T> st = new EStruct<T>();

            unsafe
            {
                int* pInt = &st.IntValue;
                pInt += 1;
                *pInt = value;
            }
            return st.Enum;
        }

        public static T StringToEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
    }
}
