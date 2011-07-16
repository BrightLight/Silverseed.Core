// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotCondition.cs" company="Silverseed.de">
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
  using System.ComponentModel;

  public class NotCondition : Condition
  {
    private ICondition innerCondition;

    public NotCondition(ICondition innerCondition)
      : base(!innerCondition.State)
    {
      this.innerCondition = innerCondition;
      this.innerCondition.PropertyChanged += this.innerCondition_PropertyChanged;
    }

    protected override ICondition GetNotCondition()
    {
      return this.innerCondition;
    }

    /// <summary>
    /// Handles the PropertyChanged event of the <see cref="innerCondition"/>.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
    private void innerCondition_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.State = !this.innerCondition.State;
    }
  }
}
