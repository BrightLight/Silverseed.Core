// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Currency.cs" company="Silverseed.de">
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
  /// <summary>
  /// A simple currency represented by the symbol of this currency.
  /// </summary>
  /// <remarks>
  /// ToDo should implement a property to determine the internal precision for calculations (e.g. Euro should be calculated and stored with 5 digits precision: 7.43217).
  /// ToDo should implement a property to determine the numer of digits to display in the UI (e.g. Euro should usually be displayed with 2 digits precision: 7.43).
  /// </remarks>
  public class Currency
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Currency"/> class.
    /// </summary>
    /// <param name="symbol">The symbol of this currency.</param>
    /// <param name="code">The ISO 4217 code of this currency.</param>
    public Currency(string symbol, string code)
    {
      this.Symbol = symbol;
      this.Code = code;
    }

    /// <summary>
    /// Gets the symbol of this currency.
    /// </summary>
    /// <remarks>
    /// See http://en.wikipedia.org/wiki/Currency_sign
    /// (not to be confused with currency code and currency symbol)
    /// </remarks>
    public string Symbol { get; private set; }

    /// <summary>
    /// Gets the ISO 4217 code of this currency.
    /// </summary>
    /// <remarks>
    /// See http://en.wikipedia.org/wiki/ISO_4217
    /// </remarks>
    public string Code { get; private set; }

    /// <summary>
    /// Returns a <see cref="System.String"/> with the <see cref="Symbol"/> of this currency.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String"/> that represents this instance.
    /// </returns>
    public override string ToString()
    {
      return this.Symbol;
    }
  }
}
