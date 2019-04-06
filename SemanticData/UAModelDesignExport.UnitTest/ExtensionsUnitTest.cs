﻿//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.UAModelDesignExport.XML;

namespace UAOOI.SemanticData.UAModelDesignExport
{
  [TestClass]
  public class ExtensionsUnitTest
  {
    [TestMethod]
    public void AddLocalizedTextTestMethod()
    {
      LocalizedText _description = null;
      int _counter = 0;
      Extensions.AddLocalizedText("localeField1", "valueField1", ref _description, x => _counter++);
      Assert.AreEqual<int>(0, _counter);
      Assert.IsNotNull(_description);
      Assert.AreEqual<string>("localeField1", _description.Key);
      Assert.AreEqual<string>("valueField1", _description.Value);
      LocalizedText _descriptionCopy = _description;
      Extensions.AddLocalizedText("localeField1", "valueField1", ref _description, x => _counter++);
      Assert.AreEqual<int>(1, _counter);
      Assert.IsNotNull(_description);
      Assert.AreSame(_descriptionCopy, _description);
    }

  }
}
