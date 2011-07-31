// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Money.cs" company="Silverseed.de">
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
  /// A money type with an amount and a currency.
  /// </summary>
  /// <remarks>
  /// This money type acts as an immutable value type, hence it is defined as struct.
  /// </remarks>
  public struct Money : IComparable, IComparable<Money>, IEquatable<Money>
  {
    /// <summary>
    /// The amount of money in <see cref="currency"/>.
    /// </summary>
    private readonly decimal amount;

    /// <summary>
    /// The currency of <see cref="amount"/>.
    /// </summary>
    private readonly Currency currency;

    /// <summary>
    /// Initializes a new instance of the <see cref="Money"/> struct.
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <param name="currency">The currency.</param>
    public Money(decimal amount, Currency currency)
    {
      this.amount = amount;
      this.currency = currency;
    }

    /// <summary>
    /// Gets amount of money in <see cref="Currency"/>.
    /// </summary>
    public decimal Amount
    {
      get { return this.amount; }
    }

    /// <summary>
    /// Gets the currency of <see cref="Amount"/>.
    /// </summary>
    public Currency Currency
    {
      get { return this.currency; }
    }

    /// <summary>
    /// Implements the binary operator +.
    /// </summary>
    /// <param name="left">The left argument to add.</param>
    /// <param name="right">The right argument to add.</param>
    /// <returns>
    /// A new <see cref="Money"/> instance with the same currency as <paramref name="left"/> and <paramref name="right"/>
    /// and the added amount of both.
    /// </returns>
    /// <remarks>
    /// The <paramref name="left"/> and <paramref name="right"/> argument must use the same currency in order to be added. 
    /// Otherwise, an <see cref="InvalidOperationException"/> is thrown.
    /// </remarks>
    public static Money operator +(Money left, Money right)
    {
      if (HaveSameCurrency(left, right))
      {
        return new Money(left.Amount + right.Amount, left.Currency);
      }

      throw new InvalidOperationException("Adding monetary values of different currencies is not supported.");
    }

    /// <summary>
    /// Implements the unary operator - which negates the amount of this money instance.
    /// </summary>
    /// <param name="money">The money instance whose amount is negated.</param>
    /// <returns>
    /// A new <see cref="Money"/> instance with the same currency as <paramref name="money"/> but with the
    /// negated amount.
    /// </returns>
    public static Money operator -(Money money)
    {
      return new Money(-money.Amount, money.Currency);
    }

    /// <summary>
    /// Implements the binary operator - to substract <paramref name="right"/> from <paramref name="left"/>.
    /// </summary>
    /// <param name="left">The left argument from which to substract.</param>
    /// <param name="right">The right argument with the amount to substract.</param>
    /// <returns>
    /// A new <see cref="Money"/> instance with the same currency as <paramref name="left"/> and <paramref name="right"/>
    /// and the substracted amount.
    /// </returns>
    /// <remarks>
    /// The <paramref name="left"/> and <paramref name="right"/> argument must use the same currency in order to be added. 
    /// Otherwise, an <see cref="InvalidOperationException"/> is thrown.
    /// </remarks>
    public static Money operator -(Money left, Money right)
    {
      if (HaveSameCurrency(left, right))
      {
        return left + (-right); // "left - right" would call this method recursively
      }

      throw new InvalidOperationException("Subtracting monetary values of different currencies is not supported.");
    }

    /// <summary>
    /// Implements the binary operator *.
    /// </summary>
    /// <param name="money">The money whose amount to multiply.</param>
    /// <param name="factor">The factor with which to multiply the amount of <paramref name="money"/>.</param>
    /// <returns>
    /// A new <see cref="Money"/> instance with the same currency as <paramref name="money"/>
    /// and the amount of <paramref name="money"/> multiplied by <paramref name="factor"/>.
    /// </returns>
    public static Money operator *(Money money, decimal factor)
    {
      return new Money(factor * money.Amount, money.Currency);
    }

    /// <summary>
    /// Implements the binary operator *.
    /// </summary>
    /// <param name="factor">The factor with which to multiply the amount of <paramref name="money"/>.</param>
    /// <param name="money">The money whose amount to multiply.</param>
    /// <returns>
    /// A new <see cref="Money"/> instance with the same currency as <paramref name="money"/>
    /// and the amount of <paramref name="money"/> multiplied by <paramref name="factor"/>.
    /// </returns>
    public static Money operator *(decimal factor, Money money)
    {
      return money * factor;
    }

    /// <summary>
    /// Implements the binary operator /.
    /// </summary>
    /// <param name="money">The money whose amount to divide.</param>
    /// <param name="factor">The factor with which to divide the amount of <paramref name="money"/>.</param>
    /// <returns>
    /// A new <see cref="Money"/> instance with the same currency as <paramref name="money"/>
    /// and the amount of <paramref name="money"/> divided by <paramref name="factor"/>.
    /// </returns>
    public static Money operator /(Money money, decimal factor)
    {
      return new Money(money.Amount / factor, money.Currency);
    }

    /// <summary>
    /// Implements the operator == to compare to money instances.
    /// </summary>
    /// <param name="left">The left money to compare.</param>
    /// <param name="right">The right money to compare.</param>
    /// <returns>
    ///   <c>True</c> if both money instances are equal; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator ==(Money left, Money right)
    {
      return left.Equals(right);
    }

    /// <summary>
    /// Implements the operator != to compare to money instances.
    /// </summary>
    /// <param name="left">The left money to compare.</param>
    /// <param name="right">The right money to compare.</param>
    /// <returns>
    ///   <c>True</c> if both money instances are unequal; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator !=(Money left, Money right)
    {
      return !left.Equals(right);
    }

    /// <summary>
    /// Implements the operator &gt; to compare to money instances.
    /// </summary>
    /// <param name="left">The left money to compare.</param>
    /// <param name="right">The right money to compare.</param>
    /// <returns>
    ///   <c>True</c> if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator >(Money left, Money right)
    {
      return left.CompareTo(right) > 0;
    }

    /// <summary>
    /// Implements the operator &lt; to compare to money instances.
    /// </summary>
    /// <param name="left">The left money to compare.</param>
    /// <param name="right">The right money to compare.</param>
    /// <returns>
    ///   <c>True</c> if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator <(Money left, Money right)
    {
      return left.CompareTo(right) < 0;
    }

    /// <summary>
    /// Implements the operator &gt;= to compare to money instances.
    /// </summary>
    /// <param name="left">The left money to compare.</param>
    /// <param name="right">The right money to compare.</param>
    /// <returns>
    ///   <c>True</c> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator >=(Money left, Money right)
    {
      return left.CompareTo(right) >= 0;
    }

    /// <summary>
    /// Implements the operator &lt;= to compare to money instances.
    /// </summary>
    /// <param name="left">The left money to compare.</param>
    /// <param name="right">The right money to compare.</param>
    /// <returns>
    ///   <c>True</c> if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator <=(Money left, Money right)
    {
      return left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Returns a <see cref="System.String"/> that represents this money instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String"/> that represents this instance.
    /// </returns>
    /// <remarks>
    /// Note maybe <see cref="Currency"/> should have a property to determine how the money should be displayed (e.g. decimal separator, grouping, position of the currency symbol)
    /// </remarks>
    public override string ToString()
    {
      return String.Format("{1}{0}", this.amount, this.currency);
    }

    /// <summary>
    /// Compares the current instance with another object of the same type and returns an integer that indicates whether 
    /// the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>
    /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has these meanings:
    /// <list type="unsorted">
    /// <listheader>Value Meaning</listheader>
    ///   <item>Less than zero: This instance is less than <paramref name="obj"/>.</item>
    ///   <item>Zero: This instance is equal to <paramref name="obj"/>.</item>
    ///   <item>Greater than zero: This instance is greater than <paramref name="obj"/>.</item>
    /// </list>
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="obj"/> is not the same type as this instance.
    /// </exception>
    public int CompareTo(object obj)
    {
      if (!(obj is Money))
      {
        throw new ArgumentException();
      }

      return this.CompareTo((Money)obj);
    }
 
    /// <summary>
    /// Compares the current object with another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has these meanings:
    /// <list type="unsorted">
    /// <listheader>Value Meaning</listheader>
    ///   <item>Less than zero: This instance is less than <paramref name="other"/>.</item>
    ///   <item>Zero: This instance is equal to <paramref name="other"/>.</item>
    ///   <item>Greater than zero: This instance is greater than <paramref name="other"/>.</item>
    /// </list>
    /// </returns>
    public int CompareTo(Money other)
    {
      if (this.currency == other.currency)
      {
        return this.amount.CompareTo(other.Amount);
      }

      return this.currency.Symbol.CompareTo(other.Currency.Symbol);
    }

    /// <summary>
    /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
    /// <returns>
    ///   <c>True</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (!(obj is Money))
      {
        throw new NotSupportedException();
      }

      return this.Equals((Money)obj);
    }

    /// <summary>
    /// Determines whether the specified <paramref name="other"/> money instance is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="Money"/> to compare with this instance.</param>
    /// <returns>
    ///   <c>True</c> if the specified <paramref name="other"/> <see cref="Money"/> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    public bool Equals(Money other)
    {
      return (this.currency == other.Currency)
          && (this.amount == other.Amount);
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override int GetHashCode()
    {
      // ToDo refactor hashcode logic
      return this.amount.GetHashCode() ^ this.currency.GetHashCode();
    }

    /// <summary>
    /// Checks whether the specified <paramref name="left"/> and <paramref name="right"/> money instances
    /// use the same currency.
    /// </summary>
    /// <param name="left">The left money instance of the comparison.</param>
    /// <param name="right">The right money instance of the comparison.</param>
    /// <returns><c>True</c> if <paramref name="left"/> and <paramref name="right"/> use the same currency; otherwise <c>false</c>.</returns>
    private static bool HaveSameCurrency(Money left, Money right)
    {
      return object.Equals(left.Currency, right.Currency);
    }
  }
}
