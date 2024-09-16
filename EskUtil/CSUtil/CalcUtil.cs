// ======================================================================================================
// File Name        : CalcUtil.cs
// Project          : CSUtil
// Last Update      : 2024.09.16 - yc.jeon
// ======================================================================================================

namespace CSUtil
{
    /// <summary>
    /// 계산 관련 유틸리티
    /// </summary>
    public static class CalcUtil
    {
        /// <summary>
        /// Double 값 Equal 비교 (Epsilon 사용)
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool EpsEquals(this double d1, double d2)
        {
            double epsilon = Math.Max(Math.Abs(d1), Math.Abs(d2)) * 1E-15;
            return Math.Abs(d1 - d2) <= epsilon;
        }

        /// <summary>
        /// <paramref name="data"/>가 <paramref name="target"/>에 대해서 <paramref name="offset"/>범위 내에 들어왔는지 확인하는 함수 <br/>
        /// (isPercent = false: <paramref name="target"/> - <paramref name="offset"/> &lt;= <paramref name="data"/> &lt;= <paramref name="target"/> + <paramref name="offset"/>) <br/>
        /// (isPercent = true: <paramref name="target"/> - (<paramref name="offset"/> * <paramref name="target"/>) &lt;= <paramref name="data"/> &lt;= <paramref name="target"/> + (<paramref name="offset"/> * <paramref name="target"/>))
        /// </summary>
        /// <param name="data">현재 데이터</param>
        /// <param name="target">타겟 데이터</param>
        /// <param name="offset">오차 범위</param>
        /// <param name="isPercent">오차값이 퍼센트인지 유무 (기본값: false)</param>
        /// <returns>
        /// true: 범위안에 들어옴 <br/>
        /// false: 범위를 벗어남
        /// </returns>
        public static bool IsOffsetIn(double data, double target, double offset, bool isPercent = false)
        {
            double diff = isPercent ? target * offset : offset;
            double negRange = target - diff;
            double posRange = target + diff;

            return data >= negRange && data <= posRange;
        }

        /// <summary>
        /// <paramref name="value"/>가 짝수인지 확인하는 함수
        /// </summary>
        /// <param name="value">짝수인지 확인할 값</param>
        /// <returns>
        /// true: 짝수 <br/>
        /// false: 홀수
        /// </returns>
        public static bool IsEven(byte value)
        {
            return (value & 1) == 0;
        }
        /// <summary>
        /// <paramref name="value"/>가 짝수인지 확인하는 함수
        /// </summary>
        /// <param name="value">짝수인지 확인할 값</param>
        /// <returns>
        /// true: 짝수 <br/>
        /// false: 홀수
        /// </returns>
        public static bool IsEven(sbyte value)
        {
            return (value & 1) == 0;
        }
        /// <summary>
        /// <paramref name="value"/>가 짝수인지 확인하는 함수
        /// </summary>
        /// <param name="value">짝수인지 확인할 값</param>
        /// <returns>
        /// true: 짝수 <br/>
        /// false: 홀수
        /// </returns>
        public static bool IsEven(short value)
        {
            return (value & 1) == 0;
        }
        /// <summary>
        /// <paramref name="value"/>가 짝수인지 확인하는 함수
        /// </summary>
        /// <param name="value">짝수인지 확인할 값</param>
        /// <returns>
        /// true: 짝수 <br/>
        /// false: 홀수
        /// </returns>
        public static bool IsEven(ushort value)
        {
            return (value & 1) == 0;
        }
        /// <summary>
        /// <paramref name="value"/>가 짝수인지 확인하는 함수
        /// </summary>
        /// <param name="value">짝수인지 확인할 값</param>
        /// <returns>
        /// true: 짝수 <br/>
        /// false: 홀수
        /// </returns>
        public static bool IsEven(int value)
        {
            return (value & 1) == 0;
        }
        /// <summary>
        /// <paramref name="value"/>가 짝수인지 확인하는 함수
        /// </summary>
        /// <param name="value">짝수인지 확인할 값</param>
        /// <returns>
        /// true: 짝수 <br/>
        /// false: 홀수
        /// </returns>
        public static bool IsEven(uint value)
        {
            return (value & 1) == 0;
        }
        /// <summary>
        /// <paramref name="value"/>가 짝수인지 확인하는 함수
        /// </summary>
        /// <param name="value">짝수인지 확인할 값</param>
        /// <returns>
        /// true: 짝수 <br/>
        /// false: 홀수
        /// </returns>
        public static bool IsEven(long value)
        {
            return (value & 1) == 0;
        }
        /// <summary>
        /// <paramref name="value"/>가 짝수인지 확인하는 함수
        /// </summary>
        /// <param name="value">짝수인지 확인할 값</param>
        /// <returns>
        /// true: 짝수 <br/>
        /// false: 홀수
        /// </returns>
        public static bool IsEven(ulong value)
        {
            return (value & 1) == 0;
        }

        /// <summary>
        /// <paramref name="value"/>가 홀수인지 확인하는 함수
        /// </summary>
        /// <param name="value">홀수인지 확인할 값</param>
        /// <returns>
        /// true: 홀수 <br/>
        /// false: 짝수
        /// </returns>
        public static bool IsOdd(byte value)
        {
            return (value & 1) == 1;
        }
        /// <summary>
        /// <paramref name="value"/>가 홀수인지 확인하는 함수
        /// </summary>
        /// <param name="value">홀수인지 확인할 값</param>
        /// <returns>
        /// true: 홀수 <br/>
        /// false: 짝수
        /// </returns>
        public static bool IsOdd(sbyte value)
        {
            return (value & 1) == 1;
        }
        /// <summary>
        /// <paramref name="value"/>가 홀수인지 확인하는 함수
        /// </summary>
        /// <param name="value">홀수인지 확인할 값</param>
        /// <returns>
        /// true: 홀수 <br/>
        /// false: 짝수
        /// </returns>
        public static bool IsOdd(short value)
        {
            return (value & 1) == 1;
        }
        /// <summary>
        /// <paramref name="value"/>가 홀수인지 확인하는 함수
        /// </summary>
        /// <param name="value">홀수인지 확인할 값</param>
        /// <returns>
        /// true: 홀수 <br/>
        /// false: 짝수
        /// </returns>
        public static bool IsOdd(ushort value)
        {
            return (value & 1) == 1;
        }
        /// <summary>
        /// <paramref name="value"/>가 홀수인지 확인하는 함수
        /// </summary>
        /// <param name="value">홀수인지 확인할 값</param>
        /// <returns>
        /// true: 홀수 <br/>
        /// false: 짝수
        /// </returns>
        public static bool IsOdd(int value)
        {
            return (value & 1) == 1;
        }
        /// <summary>
        /// <paramref name="value"/>가 홀수인지 확인하는 함수
        /// </summary>
        /// <param name="value">홀수인지 확인할 값</param>
        /// <returns>
        /// true: 홀수 <br/>
        /// false: 짝수
        /// </returns>
        public static bool IsOdd(uint value)
        {
            return (value & 1) == 1;
        }
        /// <summary>
        /// <paramref name="value"/>가 홀수인지 확인하는 함수
        /// </summary>
        /// <param name="value">홀수인지 확인할 값</param>
        /// <returns>
        /// true: 홀수 <br/>
        /// false: 짝수
        /// </returns>
        public static bool IsOdd(long value)
        {
            return (value & 1) == 1;
        }
        /// <summary>
        /// <paramref name="value"/>가 홀수인지 확인하는 함수
        /// </summary>
        /// <param name="value">홀수인지 확인할 값</param>
        /// <returns>
        /// true: 홀수 <br/>
        /// false: 짝수
        /// </returns>
        public static bool IsOdd(ulong value)
        {
            return (value & 1) == 1;
        }
    }
}
