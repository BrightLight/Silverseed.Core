// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MoneyTests.cs" company="Silverseed.de">
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

namespace Silverseed.Core.Tests
{
  using System;
  using NUnit.Framework;

  /// <summary>
  /// Unit tests for the <see cref="Money"/> class.
  /// </summary>
  [TestFixture]
  public class MoneyTests
  {
    [Test]
    public void ConstructorInitializesInstanceCorrectly()
    {
      const decimal Amount = 87.4m;
      var dollar = new Currency("$", "USD");
      var dollarMoney = new Money(Amount, dollar);
      Assert.That(dollarMoney.Amount, Is.EqualTo(Amount));
      Assert.That(dollarMoney.Currency, Is.EqualTo(dollar));
    }

    [Test]
    public void TestAddOperator()
    {
      var dollar = new Currency("$", "USD");
      var dollarMoney1 = new Money(87.4m, dollar);
      var dollarMoney2 = new Money(13.6m, dollar);
      var dollarSum = dollarMoney1 + dollarMoney2;
      Assert.That(dollarSum.Amount, Is.EqualTo(dollarMoney1.Amount + dollarMoney2.Amount));
      Assert.That(dollarSum.Currency, Is.EqualTo(dollar));
    }

    [Test]
    public void TestAssociativeLawForAdd()
    {
      var dollar = new Currency("$", "USD");
      var dollarMoney1 = new Money(87.4m, dollar);
      var dollarMoney2 = new Money(13.6m, dollar);
      var dollarSum1 = dollarMoney1 + dollarMoney2;
      var dollarSum2 = dollarMoney2 + dollarMoney1;
      Assert.That(dollarSum1.Amount, Is.EqualTo(dollarSum2.Amount));
      Assert.That(dollarSum1.Currency, Is.EqualTo(dollarSum2.Currency));
    }

    [Test]
    public void TestSubstractOperator()
    {
      var euro = new Currency("€", "EUR");
      var euroMoney1 = new Money(55, euro);
      var euroMoney2 = new Money(22.3m, euro);
      var euroDiff = euroMoney1 - euroMoney2;
      Assert.That(euroDiff.Amount, Is.EqualTo(euroMoney1.Amount - euroMoney2.Amount));
      Assert.That(euroDiff.Currency, Is.EqualTo(euro));
    }

    [Test]
    public void TestMultiplyOperator()
    {
      var dollar = new Currency("$", "USD");
      var dollarMoney1 = new Money(200, dollar);
      var dollarProduct = 2.5m * dollarMoney1;
      Assert.That(dollarProduct.Amount, Is.EqualTo(500));
      Assert.That(dollarProduct.Currency, Is.EqualTo(dollar));
      ////var dollarProduct2 = dollarMoney1 * 2;
    }

    [Test]
    public void TestDivideOperator()
    {
    }
  }
}
