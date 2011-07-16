// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceLocator.cs" company="Silverseed.de">
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

namespace Silverseed.Core
{
  using System;
  using System.Collections.Generic;

  public class ServiceLocator<T>
    where T : class
  {
    /// <summary>
    /// All registered handlers managed by this <see cref="ServiceLocator{T}"/>.
    /// </summary>
    private readonly Dictionary<string, Type> registeredHandlers = new Dictionary<string, Type>();

    public void RegisterHandler(string identifier, Type handler)
    {
      if (typeof(T).IsAssignableFrom(handler))
      {
        this.registeredHandlers.Add(identifier, handler);
      }
    }

    public bool UnregisterHandler(string identifier)
    {
      if (this.registeredHandlers.ContainsKey(identifier))
      {
        return this.registeredHandlers.Remove(identifier);
      }

      return false;
    }

    public T CreateHandler(string identifier)
    {
      Type handlerType;
      if (this.registeredHandlers.TryGetValue(identifier, out handlerType))
      {
        return CreateHandler(handlerType);
      }

      return null;
    }

    public bool ContainsHandler(string identifier)
    {
      return this.registeredHandlers.ContainsKey(identifier);
    }

    private static T CreateHandler(Type handlerType)
    {
      var handlerInstance = Activator.CreateInstance(handlerType) as T;
      if (handlerInstance == null)
      {
        throw new NotSupportedException(String.Format("Each registered handler needs to support {0}.", typeof(T)));
      }

      return handlerInstance;
    }
  }
}
