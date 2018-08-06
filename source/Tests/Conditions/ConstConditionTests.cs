// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstConditionTests.cs" company="Silverseed.de">
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
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using NUnit.Framework;
  using Silverseed.Core.Conditions;

  [TestFixture]
  public class ConstConditionTests
  {
    [Test]
    public void AlwaysTrueTest()
    {
      Assert.IsTrue(ConstCondition.AlwaysTrue.State);
      Assert.IsFalse(ConstCondition.AlwaysTrue.Not.State);
    }

    [Test]
    public void AlwaysFalseTest()
    {
      Assert.IsFalse(ConstCondition.AlwaysFalse.State);
      Assert.IsTrue(ConstCondition.AlwaysFalse.Not.State);
    }
  }
}
