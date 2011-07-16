// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlHub.cs" company="Silverseed.de">
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
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Xml;
  ////using log4net;

  public class XmlHub
  {
    /// <summary>
    /// A logger used by instances of this class.
    /// </summary>
    ////private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    /// <summary>
    /// The service locator used to find suiteable <see cref="IXmlHandler"/> instances
    /// depending on the current xml element.
    /// </summary>
    private readonly ServiceLocator<IXmlHandler> serviceLocator;

    /// <summary>
    /// Initializes a new instance of the <see cref="XmlHub"/> class.
    /// </summary>
    /// <param name="serviceLocator">The service locator that provides the <see cref="IXmlHandler"/>s to use.</param>
    public XmlHub(ServiceLocator<IXmlHandler> serviceLocator)
    {
      if (serviceLocator == null)
      {
        throw new ArgumentNullException("serviceLocator");
      }

      this.serviceLocator = serviceLocator;
    }

    public void Process(string path)
    {
      using (var dataStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
      {
        this.Process(dataStream);
      }
    }

    public void Process(Stream xmlData)
    {
      var xmlHandlers = new Stack<XmlHandlerElementCounter>();
      xmlHandlers.Push(new XmlHandlerElementCounter(new NullXmlHandler()));

      var xmlReaderSettings = new XmlReaderSettings();
      var xmlReader = XmlReader.Create(xmlData, xmlReaderSettings);
      while (xmlReader.Read())
      {
        this.ProcessCurrentNode(xmlHandlers, xmlReader);
      }
    }

    private void ProcessCurrentNode(Stack<XmlHandlerElementCounter> xmlHandlers, XmlReader xmlReader)
    {
      switch (xmlReader.NodeType)
      {
        case XmlNodeType.Element:
          this.ProcessStartElement(xmlHandlers, xmlReader);
          break;
        case XmlNodeType.EndElement:
          this.ProcessEndElement(xmlHandlers, xmlReader.Name);
          break;
        case XmlNodeType.Text:
          xmlHandlers.Peek().XmlHandler.ProcessText(xmlReader.Value);
          break;
      }
    }

    private void ProcessStartElement(Stack<XmlHandlerElementCounter> xmlHandlers, XmlReader xmlReader)
    {
      // store important information about this element because after reading of the attributes this information
      // is no longer available because the XmlReader will have moved to the attribute nodes.
      string elementName = xmlReader.Name;
      bool isEmptyElement = xmlReader.IsEmptyElement;

      if (this.serviceLocator.ContainsHandler(elementName))
      {
        var newXmlHandler = this.serviceLocator.CreateHandler(elementName);
        xmlHandlers.Push(new XmlHandlerElementCounter(newXmlHandler));
      }

      xmlHandlers.Peek().ElementCount++;

      var attributes = GetAttributes(xmlReader);
      xmlHandlers.Peek().XmlHandler.ProcessStartElement(elementName, attributes);

      // Check for empty element, like <MyTag /> as opposite to <MyTag></MyTag>
      if (isEmptyElement)
      {
        // implicit end element
        this.ProcessEndElement(xmlHandlers, elementName);
      }

    }

    private void ProcessEndElement(Stack<XmlHandlerElementCounter> xmlHandlers, string elementName)
    {
      xmlHandlers.Peek().ElementCount--;
      if (xmlHandlers.Peek().ElementCount == 0)
      {
        xmlHandlers.Pop().XmlHandler.ProcessEndElement(elementName);
      }
    }

    private static Dictionary<string, string> GetAttributes(XmlReader xmlReader)
    {
      var attributes = new Dictionary<string, string>();
      if (xmlReader.HasAttributes)
      {
        if (xmlReader.MoveToFirstAttribute())
        {
          attributes.Add(xmlReader.Name, xmlReader.Value);
          while (xmlReader.MoveToNextAttribute())
          {
            attributes.Add(xmlReader.Name, xmlReader.Value);
          }
        }
      }

      return attributes;
    }

    private class XmlHandlerElementCounter
    {
      public XmlHandlerElementCounter(IXmlHandler xmlHandler)
      {
        this.XmlHandler = xmlHandler;
        this.ElementCount = 0;
      }

      public IXmlHandler XmlHandler { get; private set; }

      public int ElementCount { get; set; }
   }
  }
}
