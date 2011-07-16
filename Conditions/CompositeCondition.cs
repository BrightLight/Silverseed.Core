// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompositeCondition.cs" company="Silverseed.de">
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

  public abstract class CompositeCondition : Condition
  {
    private readonly List<ICondition> conditions = new List<ICondition>();

    protected CompositeCondition(bool initialState)
      : base(initialState)
    {
    }

    public void Add(ICondition condition)
    {
      condition.PropertyChanged += this.condition_PropertyChanged;
      this.conditions.Add(condition);
      this.CalcOverallState(this.conditions);
    }

    public void Remove(ICondition condition)
    {
      condition.PropertyChanged -= this.condition_PropertyChanged;
      this.conditions.Remove(condition);
      this.CalcOverallState(this.conditions);
    }

    private void condition_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      this.CalcOverallState(this.conditions);
    }

    protected abstract void CalcOverallState(List<ICondition> conditions);
  }
}