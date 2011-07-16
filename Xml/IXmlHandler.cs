// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IXmlHandler.cs" company="Silverseed.de">
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

namespace Silverseed.Core.Xml
{
  using System.Collections.Generic;

  /// <summary>
  /// Defines methods all XML handlers need to support.
  /// </summary>
  public interface IXmlHandler
  {
    /// <summary>
    /// Handles the start element "event" when parsing an XML document.
    /// </summary>
    /// <param name="name">The name of the starting element.</param>
    /// <param name="attributes">All attributes defined with this starting element.</param>
    void ProcessStartElement(string name, Dictionary<string, string> attributes);

    /// <summary>
    /// Handles the end element "event" when parsing an XML document.
    /// </summary>
    /// <param name="name">The name of the ending element.</param>
    /// <remarks>This is called both for explicit end elements (e.g. "&lt;/Document&gt;")
    /// as well as for empty elements with an implicit end element (e.g. "&lt;Document /&gt;").</remarks>
    void ProcessEndElement(string name);

    /// <summary>
    /// Handles text encountered between a start element and an end element tag.
    /// </summary>
    /// <param name="value">The text between start element and end element.</param>
    void ProcessText(string value);
  }
}