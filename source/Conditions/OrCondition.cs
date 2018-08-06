// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrCondition.cs" company="Silverseed.de">
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
  using System.Collections.Generic;

  public class OrCondition : CompositeCondition
  {
    public OrCondition(bool initialState)
      : base(initialState)
    {
    }

    protected override void CalcOverallState(List<ICondition> conditions)
    {
      this.State = conditions.Exists(x => x.State);
    }
  }
}
