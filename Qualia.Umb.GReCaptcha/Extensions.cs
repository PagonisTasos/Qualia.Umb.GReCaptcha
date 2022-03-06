using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualia.Umb.GReCaptcha
{
    internal static partial class Extensions
    {
        /// <summary>
        ///     Returns null if the string is empty.
        /// </summary>
        /// <param name="this">The string to act upon.</param>
        /// <returns>Null if the string is empty, otherwise returns the string.</returns>
        public static string? NullIfEmpty(this string? @this)
        {
            return String.IsNullOrEmpty(@this) ? null : @this;
        }
    }
}
