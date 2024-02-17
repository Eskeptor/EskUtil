// ======================================================================================================
// File Name        : ImageUtil.cs
// Project          : CSUtil
// Last Update      : 2024.02.17 - yc.jeon
// ======================================================================================================

using System.Reflection;

namespace CSUtil
{
    public static class ImageUtil
    {
        /// <summary>
        /// Return the Uri of the resource
        /// </summary>
        /// <param name="resourcePath">Resource Path</param>
        /// <returns>Uri of the resource</returns>
        /// <exception cref="ArgumentException" />
        public static Uri GetUriFromResource(string resourcePath)
        {
            ArgumentException.ThrowIfNullOrEmpty(resourcePath, nameof(resourcePath));

            Assembly assm = Assembly.GetCallingAssembly();
            if (resourcePath[0].Equals('/'))
            {
                resourcePath = resourcePath.Substring(1);
            }

            return new Uri(@$"pack://application:,,,/{assm.GetName().Name};component/{resourcePath}", UriKind.Absolute);
        }
    }
}
