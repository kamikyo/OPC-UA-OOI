﻿//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Diagnostics;
using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;
using TraceMessage = UAOOI.SemanticData.UANodeSetValidation.TraceMessage;

namespace UAOOI.SemanticData.UAModelDesignExport
{

  internal class DataTypeFieldFactoryBase : IDataTypeFieldFactory
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="DataTypeFieldFactoryBase"/> class.
    /// </summary>
    /// <param name="traceEvent">The trace event.</param>
    internal DataTypeFieldFactoryBase(Action<TraceMessage> traceEvent)
    {
      Debug.Assert(traceEvent != null);
      this.TraceEvent = traceEvent;
    }

    #region IDataTypeFieldFactory
    public XmlQualifiedName DataType
    {
      set;
      private get;
    }
    public int? Identifier
    {
      set { }
    }
    public string Name
    {
      set;
      private get;
    }
    public int? ValueRank
    {
      set;
      private get;
    }
    public IDataTypeDefinitionFactory NewDefinition()
    {
      return new DataTypeDefinitionFactoryBase(this.TraceEvent); //TODO add trace - not implemented.
    }
    public int Value
    {
      set;
      private get;
    }
    public string SymbolicName
    {
      set { }
    }
    public void AddDescription(string localeField, string valueField)
    {
      Extensions.AddLocalizedText(localeField, valueField, ref m_Description, TraceEvent);
    }
    #endregion

    #region internal API
    internal XML.Parameter Export()
    {
      bool _ValueRankSpecified;
      XML.Parameter _newParameter = new XML.Parameter()
      {
        DataType = DataType,
        Description = this.m_Description,
        Identifier = this.Value,
        IdentifierSpecified = true,
        Name = this.Name,
        ValueRank = this.ValueRank.GetValueRank(x => _ValueRankSpecified = x, this.TraceEvent)

        //_item.Definition 
        //The field is a structure with a layout specified by the definition.
        //This field is optional.
        //This field allows designers to create nested structures without defining a new DataType Node for each structure.
        //This field is not specified for subtypes of Enumeration.

        //_item.SymbolicName 
        //A symbolic name for the field that can be used in autogenerated code. It should only be specified if the Name cannot be used for this purpose. 
        //Only letters, digits or the underscore (‘_’) are permitted.

        //_item.Value
        //The value associated with the field.
        //This field is only specified for subtypes of Enumeration.

      };
      return _newParameter;
    }
    #endregion

    #region private
    private Action<TraceMessage> TraceEvent { get; set; }
    private XML.LocalizedText m_Description = null;
    #endregion

  }

}