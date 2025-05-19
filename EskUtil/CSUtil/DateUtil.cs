// ======================================================================================================
// File Name        : DateUtil.cs
// Project          : CSUtil
// Last Update      : 2025.05.19 - yc.jeon
// ======================================================================================================

using System;
using System.Globalization;

namespace CSUtil
{
    public static class DateUtil
    {
        /// <summary>
        /// UTC 시간을 로컬 시간으로 변환하는 함수
        /// </summary>
        /// <param name="utcTime">UTC 시간 (타입: Constants.FORMAT_DATETIME_UTC_LONG_1)</param>
        /// <returns>
        /// 변환된 Local Time <br/>
        /// 변환 실패한 경우 string.Empty 반환
        /// </returns>
        public static string UtcToLocalTime1(string utcTime, string utcFormat = "yyyy-MM-ddTHH:mm:ss.fffZ", string localFormat = "yyyy-MM-dd HH:mm:ss.fff")
        {
            if (!DateTime.TryParseExact(utcTime, utcFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime utc))
            {
                return string.Empty;
            }

            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utc, TimeZoneInfo.Local);
            return localTime.ToStringInvariantCulture(localFormat);
        }
    }
}
