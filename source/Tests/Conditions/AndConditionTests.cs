// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AndConditionTests.cs" company="Silverseed.de">
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


namespace Silverseed.Core.Tests.Conditions
{
  using NUnit.Framework;
  using Silverseed.Core.Conditions;

  [TestFixture]
  public class AndConditionTests
  {
    [Test]
    public void UpdateWhenAddingNewConditionsTest()
    {
      var andCondition = new AndCondition(false);
      andCondition.Add(ConstCondition.AlwaysTrue);
      Assert.IsTrue(andCondition.State);
    }

    [Test]
    public void UpdateWhenRemovingConditionsTest()
    {
      var andCondition = new AndCondition(false);
      andCondition.Add(ConstCondition.AlwaysTrue);
      Assert.IsTrue(andCondition.State);
      andCondition.Add(ConstCondition.AlwaysFalse);
      Assert.IsFalse(andCondition.State);
      andCondition.Remove(ConstCondition.AlwaysFalse);
      Assert.IsTrue(andCondition.State);
    }

    [Test]
    public void UpdateWhenConditionChangesTest()
    {
      var andCondition = new AndCondition(false);
      var myCondition = new Condition(true);
      andCondition.Add(myCondition);
      Assert.IsTrue(andCondition.State);
      myCondition.State = false;
      Assert.IsFalse(andCondition.State);
      myCondition.State = true;
      Assert.IsTrue(andCondition.State);
    }

    [Test]
    public void ApplyAndLogicCorrectlyTest()
    {
      var andCondition = new AndCondition(false);
      var myCondition1 = new Condition(true);
      var myCondition2 = new Condition(true);
      andCondition.Add(myCondition1);
      andCondition.Add(myCondition2);
      Assert.IsTrue(andCondition.State);
      myCondition1.State = false;
      Assert.IsFalse(andCondition.State);
      myCondition1.State = true;
      Assert.IsTrue(andCondition.State);
      myCondition2.State = false;
      Assert.IsFalse(andCondition.State);
      myCondition1.State = false;
      Assert.IsFalse(andCondition.State);
      myCondition1.State = true;
      myCondition2.State = true;
      Assert.IsTrue(andCondition.State);
    }
  }
}
