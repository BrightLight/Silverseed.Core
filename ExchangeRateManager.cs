// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExchangeRateManager.cs" company="Silverseed.de">
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
  using System.Collections.Generic;

  /// <summary>
  /// Handles exchanges rates between currencies.
  /// </summary>
  /// <remarks>
  /// Note this is only a first draft and not yet a complete implementation. For instance it doesn't handle dates right now.
  /// </remarks>
  public static class ExchangeRateManager
  {
    private static readonly Dictionary<Currency, Dictionary<Currency, decimal>> exchangeRates = new Dictionary<Currency, Dictionary<Currency, decimal>>();

    public static void AddExchangeRate(Currency fromCurrency, Currency toCurrency, decimal factor)
    {
      Dictionary<Currency, decimal> toCurrencies;
      if (!exchangeRates.TryGetValue(fromCurrency, out toCurrencies))
      {
        toCurrencies = new Dictionary<Currency, decimal>();
        exchangeRates.Add(fromCurrency, toCurrencies);
      }

      toCurrencies.Add(toCurrency, factor);
    }

    public static decimal GetExchangeRate(Currency fromCurrency, Currency toCurrency)
    {
      decimal factor;
      if (TryGetExchangeRate(fromCurrency, toCurrency, out factor))
      {
        return factor;
      }

      throw new NotSupportedException();
    }

    public static bool TryGetExchangeRate(Currency fromCurrency, Currency toCurrency, out decimal factor)
    {
      // before anything else, check whether a conversion is at all required.
      if (fromCurrency.Equals(toCurrency))
      {
        factor = 1;
        return true;
      }

      Dictionary<Currency, decimal> toCurrencies;
      if (exchangeRates.TryGetValue(fromCurrency, out toCurrencies))
      {
        return toCurrencies.TryGetValue(toCurrency, out factor);
      }

      // if not found, try reverse direction
      Dictionary<Currency, decimal> fromCurrencies;
      if (exchangeRates.TryGetValue(toCurrency, out fromCurrencies))
      {
        if (fromCurrencies.TryGetValue(fromCurrency, out factor))
        {
          factor = 1 / factor;
          return true;
        }
      }

      factor = 0;
      return false;
    }

    //public static decimal GetExchangeRate(Currency fromCurrency, Currency toCurrency, DateTime referenceDate)
  }
}