using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Common
{
    public class CoreValidator
    {
        public static void ThrowIfNull(object obj, string name)
        {
            throw new ArgumentNullException(name);
        }

        public static void ThrowIfNullOrEmpty(string text, string name)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException(message: $"{name} cannot be null or empty." ,paramName: name);
            }
        }
    }
}
