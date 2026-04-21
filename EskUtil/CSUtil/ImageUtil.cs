// ======================================================================================================
// File Name        : ImageUtil.cs
// Project          : CSUtil
// Last Update      : 2026.04.21 - yc.jeon (Eskeptor)
// ======================================================================================================

using System;
using System.Reflection;

namespace Esk.GearForge.CSUtil
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
            if (string.IsNullOrEmpty(resourcePath))
            {
                throw new ArgumentNullException(nameof(resourcePath));
            }

            Assembly assm = Assembly.GetCallingAssembly();
            if (resourcePath[0].Equals('/'))
            {
                resourcePath = resourcePath.Substring(1);
            }

            return new Uri($@"pack://application:,,,/{assm.GetName().Name};component/{resourcePath}", UriKind.Absolute);
        }
    }
}
