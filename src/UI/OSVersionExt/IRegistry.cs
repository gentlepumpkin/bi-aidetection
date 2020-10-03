using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSVersionExt.Registry
{
    public interface IRegistry
    {
        /// <summary>
        /// Retrieves the value associated with the specified name, in the specified registry key.
        /// If the name is not found in the specified key, returns a default value that you provide, or null if the specified key does not exist.
        /// </summary>
        /// <param name="keyName">The full registry path of the key, beginning with a valid registry root, such as "HKEY_CURRENT_USER".</param>
        /// <param name="valueName">The name of the name/value pair.</param>
        /// <param name="defaultValue">The value to return if valueName does not exist.</param>
        /// <returns></returns>
        object GetValue(string keyName, string valueName, object defaultValue);
    }
}
