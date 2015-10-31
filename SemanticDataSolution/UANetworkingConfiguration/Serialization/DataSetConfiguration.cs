﻿
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration.Serialization
{

  /// <summary>
  /// Class <see cref="DataSetConfiguration"/> represent a data set as a collection of members to be used as a description to populate (encode) messages sent over the wire 
  /// or analyze (decode) the message read from the wire to retrieve data items. For the senders it also provides criteria on when to forward the new message using the message 
  /// writers associated with this set of data. All communication parties must use the same <see cref="DataSetConfiguration"/> description to decode the message content.
  /// Current value can be loaded from the local configuration or obtained using meta-data exchange centric communication mechanism.
  /// For example it could be read from a file or discovered using OPC UA session from OPC UA server.
  /// </summary>
  public partial class DataSetConfiguration { }
}
