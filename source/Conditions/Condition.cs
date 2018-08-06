// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Condition.cs" company="Silverseed.de">
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
  using System;
  using System.ComponentModel;

  public class Condition : ICondition
  {
    private bool state;

    private ICondition notCondition;

    public Condition(bool initialState)
    {
      this.State = initialState;
    }

    #region INotifyPropertyChanged Members

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region ICondition Members

    /// <summary>
    /// Gets or sets a text describing the condition.
    /// </summary>
    /// <value>The a description of this condition.</value>
    public string Description
    {
      get;
      protected set;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="ICondition"/> is currently fullfilled.
    /// </summary>
    /// <value>
    ///   <c>True</c> if the condition is currently fullfilled; otherwise, <c>false</c>.
    /// </value>
    public bool State
    {
      get
      {
        return this.state;
      }

      set
      {
        if (this.state != value)
        {
          this.state = value;
          this.OnPropertyChanged("State");
        }
      }
    }

    /// <summary>
    /// Gets a condition that represents the negation of this condition.
    /// </summary>
    public ICondition Not
    {
      get { return this.GetNotCondition(); }
    }

    #endregion

    protected virtual ICondition GetNotCondition()
    {
      if (this.notCondition == null)
      {
        this.notCondition = new NotCondition(this);
      }

      return this.notCondition;
    }

    /// <summary>
    /// Called when a property of this instance has changed.
    /// </summary>
    /// <param name="propertyName">The name of the property that has changed.</param>
    protected void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged != null)
      {
        this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }
  }
}
