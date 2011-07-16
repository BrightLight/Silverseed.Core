// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICondition.cs" company="Silverseed.de">
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

  /// <summary>
  /// A single condition that must be fulfilled.
  /// </summary>
  public interface ICondition : INotifyPropertyChanged
  {
    /// <summary>
    /// Gets a text describing the condition.
    /// </summary>
    /// <value>The a description of this condition.</value>
    string Description { get; }

    /// <summary>
    /// Gets a value indicating whether this <see cref="ICondition"/> is currently fullfilled.
    /// </summary>
    /// <value><c>True</c> if the condition is currently fullfilled; otherwise, <c>false</c>.</value>
    bool State { get; }

    /// <summary>
    /// Gets a condition that represents the negation of this condition.
    /// </summary>
    ICondition Not { get; }
  }
}