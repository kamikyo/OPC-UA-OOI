﻿//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{

  public abstract partial class UAInstance
  {

    /// <summary>
    /// Indicates whether the the inherited parent object is also equal to another object.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><c>true</c> if the current object is equal to the <paramref name="other">other</paramref>; otherwise,, <c>false</c> otherwise.</returns>
    protected override bool ParentEquals(UANode other)
    {
      UAInstance _other = other as UAInstance;
      if ( Object.ReferenceEquals( _other, null))
        return false;
      return true;
    }
    /// <summary>
    /// Clones current object to a new one./>.
    /// </summary>
    /// <param name="ret">The ret.</param>
    protected void CloneUAInstance(UAInstance ret)
    {
      ret.ParentNodeId = this.ParentNodeId;
      base.CloneUANode(this);
    }
    internal override void RecalculateNodeIds(IUAModelContext modelContext)
    {
      base.RecalculateNodeIds(modelContext);
      this.ParentNodeId = modelContext.ImportNodeId(this.ParentNodeId);
    }
  }

}
