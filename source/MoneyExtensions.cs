// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MoneyExtensions.cs" company="Silverseed.de">
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
    using System;

  /// <summary>
  /// Extension methods for the <see cref="Money"/> type.
  /// </summary>
    public static class MoneyExtensions
    {
      /// <summary>
      /// Converts the specified <paramref name="money"/> into an amount of <paramref name="toCurrency"/>.
      /// </summary>
      /// <param name="money">The money that is converted into another currency.</param>
      /// <param name="toCurrency">The currency the <paramref name="money"/> is converted into.</param>
      /// <returns>A new <see cref="Money"/> instance of <paramref name="toCurrency"/> and the amount recalculated into the new currency using the exchange rate of today.</returns>
      public static Money ConvertTo(this Money money, Currency toCurrency)
        {
          return money.ConvertTo(toCurrency, DateTime.Now);
        }

        /// <summary>
        /// Converts the specified <paramref name="money"/> into an amount of <paramref name="toCurrency"/>
        /// using an exchange rate that valid for the specified <paramref name="referenceDate"/>.
        /// </summary>
        /// <param name="money">The money that is converted into another currency.</param>
        /// <param name="toCurrency">The currency the <paramref name="money"/> is converted into.</param>
        /// <param name="referenceDate">The reference date that is used to determine the exchange rate.</param>
        /// <returns>A new <see cref="Money"/> instance of <paramref name="toCurrency"/> and the amount recalculated into the new currency using an exchange rate.</returns>
        public static Money ConvertTo(this Money money, Currency toCurrency, DateTime referenceDate)
        {
            // Check whether conversion is at all required.
            if (money.Currency.Equals(toCurrency))
            {
                return new Money(money.Amount, money.Currency); // ToDo maybe we do not need to create a new instance. Is the money parameter already a new instance because value types are copied?
            }

            var factor = ExchangeRateManager.GetExchangeRate(money.Currency, toCurrency);
            return new Money(money.Amount * factor, toCurrency);
        }
    }
}
