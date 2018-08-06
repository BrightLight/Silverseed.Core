// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DictionaryExtensions.cs" company="Silverseed.de">
//
//   The contents of this file are subject to the Mozilla Public License
//   Version 1.1 (the "License"); you may not use this file except in
//   compliance with the License. You may obtain a copy of the License at
//   http://www.mozilla.org/MPL/
//
//   Software distributed under the License is distributed on an "AS IS"
//   basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See the
//   License for the specific language governing rights and limitations
//   under the License.
//
//   The Initial Developer of the Original Code is Markus Hastreiter.
//
//   Contributor(s): ______________________________________.
//
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Silverseed.Core
{
  using System.Collections.Generic;

  /// <summary>
  /// Extension methods for the <see cref="Dictionary{TKey,TValue}"/> class.
  /// </summary>
  public static class DictionaryExtensions
  {
    /// <summary>
    /// Gets the value associated with the specified <paramref name="key"/>. 
    /// If the <paramref name="dictionary"/> doesn't containt this key the <paramref name="defaultValue"/> is returned.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionay.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="dictionary">The dictionary from which to get the value.</param>
    /// <param name="key">The key of the value to get.</param>
    /// <param name="defaultValue">The default value used if the specified <paramref name="key"/> is not present in the <paramref name="dictionary"/>.</param>
    /// <returns>The value associated with the specified <paramref name="key"/> if present; otherwise <paramref name="defaultValue"/>.</returns>
    public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
    {
      TValue valueFromDictionary;
      if (dictionary.TryGetValue(key, out valueFromDictionary))
      {
        return valueFromDictionary;
      }

      return defaultValue;
    }
  }
}
