// ======================================================================================================
// File Name        : CalcUtil.cs
// Project          : CSUtil
// Last Update      : 2025.05.19 - yc.jeon
// ======================================================================================================

using System;

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
        /// Float 값 Equal 비교 (Epsilon 사용)
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool EpsEquals(this float f1, float f2)
        {
            double epsilon = Math.Max(Math.Abs(f1), Math.Abs(f2)) * 1E-7;
            return Math.Abs(f1 - f2) <= epsilon;
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
            double diff = isPercent ? target * offset * 0.01 : offset;
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

        /// <summary>
        /// Degree를 Radian으로 변환하는 함수
        /// </summary>
        /// <param name="degree">변환할 각도</param>
        /// <returns></returns>
        public static double DegreeToRadian(double degree)
        {
            return degree * Math.PI / 180.0;
        }

        /// <summary>
        /// 현재 각도(<paramref name="degree"/>)를 0 ~ 360 사이의 각도로 변경하는 함수
        /// </summary>
        /// <param name="degree">현재 각도</param>
        /// <returns>
        /// 변경된 각도
        /// </returns>
        /// <exception cref="ArithmeticException"></exception>
        public static double ConvertDegree0To360(double degree)
        {
            if (degree >= 0.0 &&
                degree <= 360.0)
            {
                return degree;
            }
            if (degree >= int.MaxValue / 2)
            {
                throw new ArithmeticException("The angle is too big.");
            }

            double convDegree = degree;
            while (convDegree < 0.0 ||
                convDegree > 360.0)
            {
                if (convDegree > 360.0)
                {
                    convDegree -= 360.0;
                }
                else if (convDegree < 0)
                {
                    convDegree += 360.0;
                }
            }

            return convDegree;
        }

        /// <summary>
        /// RGB값을 HSV로 변환하는 함수
        /// </summary>
        /// <param name="red">Red (0~255)</param>
        /// <param name="green">Green (0~255)</param>
        /// <param name="blue">Blue (0~255)</param>
        /// <param name="hue">색상 (0~360)</param>
        /// <param name="saturation">채도 (0~1)</param>
        /// <param name="value">명도 (0~1)</param>
        public static void RGBtoHSV(int red, int green, int blue, out double hue, out double saturation, out double value)
        {
            double redNorm = red / 255.0;
            double greenNorm = green / 255.0;
            double blueNorm = blue / 255.0;

            double max = Math.Max(redNorm, Math.Max(greenNorm, blueNorm));
            double min = Math.Min(redNorm, Math.Min(greenNorm, blueNorm));
            double delta = max - min;

            if (delta == 0)
            {
                hue = 0;
            }
            else if (max == redNorm)
            {
                hue = 60.0 * (((greenNorm - blueNorm) / delta) % 6);
            }
            else if (max == greenNorm)
            {
                hue = 60.0 * (((blueNorm - redNorm) / delta) + 2);
            }
            else
            {
                hue = 60.0 * (((redNorm - greenNorm) / delta) + 4);
            }

            if (hue < 0)
            {
                hue += 360.0;
            }

            saturation = (max == 0) ? 0 : (delta / max);

            value = max;
        }

        /// <summary>
        /// HSV값을 RGB로 변환하는 함수
        /// </summary>
        /// <param name="hue">색상 (0~360)</param>
        /// <param name="saturation">채도 (0~1)</param>
        /// <param name="value">명도 (0~1)</param>
        /// <param name="red">Red (0~255)</param>
        /// <param name="green">Green (0~255)</param>
        /// <param name="blue">Blue (0~255)</param>
        public static void HSVtoRGB(double hue, double saturation, double value, out int red, out int green, out int blue)
        {
            double chroma = value * saturation;
            double secondComponent = chroma * (1.0 - Math.Abs((hue / 60.0) % 2.0 - 1.0));
            double matchValue = value - chroma;

            double redNorm = 0.0;
            double greenNorm = 0.0;
            double blueNorm = 0.0;

            if (hue >= 0.0 &&
                hue < 60.0)
            {
                redNorm = chroma;
                greenNorm = secondComponent;
                blueNorm = 0.0;
            }
            else if (hue >= 60.0 &&
                hue < 120.0)
            {
                redNorm = secondComponent;
                greenNorm = chroma;
                blueNorm = 0.0;
            }
            else if (hue >= 120.0 &&
                hue < 180.0)
            {
                redNorm = 0.0;
                greenNorm = chroma;
                blueNorm = secondComponent;
            }
            else if (hue >= 180 &&
                hue < 240)
            {
                redNorm = 0.0;
                greenNorm = secondComponent;
                blueNorm = chroma;
            }
            else if (hue >= 240 &&
                hue < 300)
            {
                redNorm = secondComponent;
                greenNorm = 0.0;
                blueNorm = chroma;
            }
            else
            {
                redNorm = chroma;
                greenNorm = 0.0;
                blueNorm = secondComponent;
            }

            red = (int)Math.Round((redNorm + matchValue) * 255);
            green = (int)Math.Round((greenNorm + matchValue) * 255);
            blue = (int)Math.Round((blueNorm + matchValue) * 255);
        }

        /// <summary>
        /// 입력 값(inputValue)을 주어진 입력 범위(inputStart, inputEnd)에서 출력 범위(outputStart, outputEnd)로 선형적으로 변환하는 함수입니다. <br/>
        /// 예를 들어, 입력 값이 0~100 범위에 있고 이를 0~1 범위로 변환하려는 경우 사용할 수 있습니다.
        /// </summary>
        /// <param name="inputValue">변환할 입력 값</param>
        /// <param name="inputStart">입력 값의 시작 범위</param>
        /// <param name="inputEnd">입력 값의 끝 범위</param>
        /// <param name="outputStart">출력 값의 시작 범위</param>
        /// <param name="outputEnd">출력 값의 끝 범위</param>
        /// <returns>변환된 출력 값</returns>
        public static short GetLinear(short inputValue, double inputStart, double inputEnd, double outputStart, double outputEnd)
        {
            short result;
            try
            {
                result = Convert.ToInt16(GetLinear((double)inputValue, inputStart, inputEnd, outputStart, outputEnd));
            }
            catch
            {
                result = inputValue;
            }
            return result;
        }
        /// <summary>
        /// 입력 값(inputValue)을 주어진 입력 범위(inputStart, inputEnd)에서 출력 범위(outputStart, outputEnd)로 선형적으로 변환하는 함수입니다. <br/>
        /// 예를 들어, 입력 값이 0~100 범위에 있고 이를 0~1 범위로 변환하려는 경우 사용할 수 있습니다.
        /// </summary>
        /// <param name="inputValue">변환할 입력 값</param>
        /// <param name="inputStart">입력 값의 시작 범위</param>
        /// <param name="inputEnd">입력 값의 끝 범위</param>
        /// <param name="outputStart">출력 값의 시작 범위</param>
        /// <param name="outputEnd">출력 값의 끝 범위</param>
        /// <returns>변환된 출력 값</returns>
        public static ushort GetLinear(ushort inputValue, double inputStart, double inputEnd, double outputStart, double outputEnd)
        {
            ushort result;
            try
            {
                result = Convert.ToUInt16(GetLinear((double)inputValue, inputStart, inputEnd, outputStart, outputEnd));
            }
            catch
            {
                result = inputValue;
            }
            return result;
        }
        /// <summary>
        /// 입력 값(inputValue)을 주어진 입력 범위(inputStart, inputEnd)에서 출력 범위(outputStart, outputEnd)로 선형적으로 변환하는 함수입니다. <br/>
        /// 예를 들어, 입력 값이 0~100 범위에 있고 이를 0~1 범위로 변환하려는 경우 사용할 수 있습니다.
        /// </summary>
        /// <param name="inputValue">변환할 입력 값</param>
        /// <param name="inputStart">입력 값의 시작 범위</param>
        /// <param name="inputEnd">입력 값의 끝 범위</param>
        /// <param name="outputStart">출력 값의 시작 범위</param>
        /// <param name="outputEnd">출력 값의 끝 범위</param>
        /// <returns>변환된 출력 값</returns>
        public static int GetLinear(int inputValue, double inputStart, double inputEnd, double outputStart, double outputEnd)
        {
            int result;
            try
            {
                result = Convert.ToInt32(GetLinear((double)inputValue, inputStart, inputEnd, outputStart, outputEnd));
            }
            catch
            {
                result = inputValue;
            }
            return result;
        }
        /// <summary>
        /// 입력 값(inputValue)을 주어진 입력 범위(inputStart, inputEnd)에서 출력 범위(outputStart, outputEnd)로 선형적으로 변환하는 함수입니다. <br/>
        /// 예를 들어, 입력 값이 0~100 범위에 있고 이를 0~1 범위로 변환하려는 경우 사용할 수 있습니다.
        /// </summary>
        /// <param name="inputValue">변환할 입력 값</param>
        /// <param name="inputStart">입력 값의 시작 범위</param>
        /// <param name="inputEnd">입력 값의 끝 범위</param>
        /// <param name="outputStart">출력 값의 시작 범위</param>
        /// <param name="outputEnd">출력 값의 끝 범위</param>
        /// <returns>변환된 출력 값</returns>
        public static uint GetLinear(uint inputValue, double inputStart, double inputEnd, double outputStart, double outputEnd)
        {
            uint result;
            try
            {
                result = Convert.ToUInt32(GetLinear((double)inputValue, inputStart, inputEnd, outputStart, outputEnd));
            }
            catch
            {
                result = inputValue;
            }
            return result;
        }
        /// <summary>
        /// 입력 값(inputValue)을 주어진 입력 범위(inputStart, inputEnd)에서 출력 범위(outputStart, outputEnd)로 선형적으로 변환하는 함수입니다. <br/>
        /// 예를 들어, 입력 값이 0~100 범위에 있고 이를 0~1 범위로 변환하려는 경우 사용할 수 있습니다.
        /// </summary>
        /// <param name="inputValue">변환할 입력 값</param>
        /// <param name="inputStart">입력 값의 시작 범위</param>
        /// <param name="inputEnd">입력 값의 끝 범위</param>
        /// <param name="outputStart">출력 값의 시작 범위</param>
        /// <param name="outputEnd">출력 값의 끝 범위</param>
        /// <returns>변환된 출력 값</returns>
        public static long GetLinear(long inputValue, double inputStart, double inputEnd, double outputStart, double outputEnd)
        {
            long result;
            try
            {
                result = Convert.ToInt64(GetLinear((double)inputValue, inputStart, inputEnd, outputStart, outputEnd));
            }
            catch
            {
                result = inputValue;
            }
            return result;
        }
        /// <summary>
        /// 입력 값(inputValue)을 주어진 입력 범위(inputStart, inputEnd)에서 출력 범위(outputStart, outputEnd)로 선형적으로 변환하는 함수입니다. <br/>
        /// 예를 들어, 입력 값이 0~100 범위에 있고 이를 0~1 범위로 변환하려는 경우 사용할 수 있습니다.
        /// </summary>
        /// <param name="inputValue">변환할 입력 값</param>
        /// <param name="inputStart">입력 값의 시작 범위</param>
        /// <param name="inputEnd">입력 값의 끝 범위</param>
        /// <param name="outputStart">출력 값의 시작 범위</param>
        /// <param name="outputEnd">출력 값의 끝 범위</param>
        /// <returns>변환된 출력 값</returns>
        public static ulong GetLinear(ulong inputValue, double inputStart, double inputEnd, double outputStart, double outputEnd)
        {
            ulong result;
            try
            {
                result = Convert.ToUInt64(GetLinear((double)inputValue, inputStart, inputEnd, outputStart, outputEnd));
            }
            catch
            {
                result = inputValue;
            }
            return result;
        }
        /// <summary>
        /// 입력 값(inputValue)을 주어진 입력 범위(inputStart, inputEnd)에서 출력 범위(outputStart, outputEnd)로 선형적으로 변환하는 함수입니다. <br/>
        /// 예를 들어, 입력 값이 0~100 범위에 있고 이를 0~1 범위로 변환하려는 경우 사용할 수 있습니다.
        /// </summary>
        /// <param name="inputValue">변환할 입력 값</param>
        /// <param name="inputStart">입력 값의 시작 범위</param>
        /// <param name="inputEnd">입력 값의 끝 범위</param>
        /// <param name="outputStart">출력 값의 시작 범위</param>
        /// <param name="outputEnd">출력 값의 끝 범위</param>
        /// <returns>변환된 출력 값</returns>
        public static float GetLinear(float inputValue, double inputStart, double inputEnd, double outputStart, double outputEnd)
        {
            float result;
            try
            {
                result = Convert.ToSingle(GetLinear((double)inputValue, inputStart, inputEnd, outputStart, outputEnd));
            }
            catch
            {
                result = inputValue;
            }
            return result;
        }

        /// <summary>
        /// 입력 값(inputValue)을 주어진 입력 범위(inputStart, inputEnd)에서 출력 범위(outputStart, outputEnd)로 선형적으로 변환하는 함수입니다. <br/>
        /// 예를 들어, 입력 값이 0~100 범위에 있고 이를 0~1 범위로 변환하려는 경우 사용할 수 있습니다.
        /// </summary>
        /// <param name="inputValue">변환할 입력 값</param>
        /// <param name="inputStart">입력 값의 시작 범위</param>
        /// <param name="inputEnd">입력 값의 끝 범위</param>
        /// <param name="outputStart">출력 값의 시작 범위</param>
        /// <param name="outputEnd">출력 값의 끝 범위</param>
        /// <returns>변환된 출력 값</returns>
        public static double GetLinear(double inputValue, double inputStart, double inputEnd, double outputStart, double outputEnd)
        {
            if (inputValue > inputEnd)
            {
                inputValue = inputEnd;
            }
            if (inputValue < inputStart)
            {
                inputValue = inputStart;
            }
            double inputRange = inputEnd - inputStart;
            double outputRange = outputEnd - outputStart;
            double result = inputRange == 0.0 ? 0.0 : outputStart + (inputValue - inputStart) * (outputRange / inputRange);
            return result;
        }

        /// <summary>
        /// 입력 값(inputValue)을 주어진 입력 범위(inputStart, inputEnd)에서 출력 범위(outputStart, outputEnd)로 선형적으로 변환하는 함수입니다. <br/>
        /// 예를 들어, 입력 값이 0~100 범위에 있고 이를 0~1 범위로 변환하려는 경우 사용할 수 있습니다.
        /// </summary>
        /// <param name="inputValue">변환할 입력 값</param>
        /// <param name="inputStart">입력 값의 시작 범위</param>
        /// <param name="inputEnd">입력 값의 끝 범위</param>
        /// <param name="outputStart">출력 값의 시작 범위</param>
        /// <param name="outputEnd">출력 값의 끝 범위</param>
        /// <returns>변환된 출력 값</returns>
        public static object GetLinear(object inputValue, double inputStart, double inputEnd, double outputStart, double outputEnd)
        {
            switch (inputValue)
            {
                case float f:
                    return GetLinear(f, inputStart, inputEnd, outputStart, outputEnd);
                case double d:
                    return GetLinear(d, inputStart, inputEnd, outputStart, outputEnd);
                case short s:
                    return GetLinear(s, inputStart, inputEnd, outputStart, outputEnd);
                case ushort us:
                    return GetLinear(us, inputStart, inputEnd, outputStart, outputEnd);
                case int i:
                    return GetLinear(i, inputStart, inputEnd, outputStart, outputEnd);
                case uint ui:
                    return GetLinear(ui, inputStart, inputEnd, outputStart, outputEnd);
                case long l:
                    return GetLinear(l, inputStart, inputEnd, outputStart, outputEnd);
                case ulong ul:
                    return GetLinear(ul, inputStart, inputEnd, outputStart, outputEnd);
                default:
                    return null;
            }
        }
    }
}
