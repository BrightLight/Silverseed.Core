// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstCondition.cs" company="Silverseed.de">
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

namespace Silverseed.Core.Conditions
{
  public static class ConstCondition
  {
    /// <summary>
    /// Initializes static members of the <see cref="ConstCondition"/> class.
    /// </summary>
    static ConstCondition()
    {
      AlwaysTrue = new StaticCondition(true);
      AlwaysFalse = new StaticCondition(false);
    }

    public static ICondition AlwaysTrue
    {
      get;
      private set;
    }

    public static ICondition AlwaysFalse
    {
      get;
      private set;
    }

    private class StaticCondition : Condition
    {
      /// <summary>
      /// Initializes a new instance of the <see cref="StaticCondition"/> class.
      /// </summary>
      /// <param name="initialState">The initial and only state of this condition.</param>
      public StaticCondition(bool initialState)
        : base(initialState)
      {
      }
    }
  }
}
