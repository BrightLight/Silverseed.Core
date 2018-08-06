// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrConditionTests.cs" company="Silverseed.de">
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
  public class OrConditionTests
  {
    [Test]
    public void UpdateWhenAddingNewConditionsTest()
    {
      var orCondition = new OrCondition(false);
      orCondition.Add(ConstCondition.AlwaysTrue);
      Assert.IsTrue(orCondition.State);
    }

    [Test]
    public void UpdateWhenRemovingConditionsTest()
    {
      var orCondition = new OrCondition(true);
      orCondition.Add(ConstCondition.AlwaysFalse);
      Assert.IsFalse(orCondition.State);
      orCondition.Add(ConstCondition.AlwaysTrue);
      Assert.IsTrue(orCondition.State);
      orCondition.Remove(ConstCondition.AlwaysTrue);
      Assert.IsFalse(orCondition.State);
    }

    [Test]
    public void UpdateWhenConditionChangesTest()
    {
      var orCondition = new OrCondition(false);
      var myCondition = new Condition(true);
      orCondition.Add(myCondition);
      Assert.IsTrue(orCondition.State);
      myCondition.State = false;
      Assert.IsFalse(orCondition.State);
      myCondition.State = true;
      Assert.IsTrue(orCondition.State);
    }

    [Test]
    public void ApplyOrLogicCorrectlyTest()
    {
      var orCondition = new OrCondition(false);
      var myCondition1 = new Condition(true);
      var myCondition2 = new Condition(true);
      orCondition.Add(myCondition1);
      orCondition.Add(myCondition2);
      Assert.IsTrue(orCondition.State);
      myCondition1.State = false;
      Assert.IsTrue(orCondition.State);
      myCondition2.State = false;
      Assert.IsFalse(orCondition.State);
      myCondition1.State = true;
      Assert.IsTrue(orCondition.State);
      myCondition2.State = true;
      Assert.IsTrue(orCondition.State);
    }
  }
}
