﻿//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CommonServiceLocator;
using System;
using System.ComponentModel.Composition;
using System.IO;
using UAOOI.Configuration.Core;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Configuration.DataBindings
{

  /// <summary>
  /// Class UANetworkingConfigurationEditor - 
  /// </summary>
  [Export(typeof(IConfiguration))]
  public sealed class UANetworkingConfigurationEditor : ConfigurationBase<ConfigurationData>
  {

    #region API
    /// <summary>
    /// Initializes a new instance of the <see cref="UANetworkingConfigurationEditor"/> class.
    /// </summary>
    public UANetworkingConfigurationEditor()
    {
      ComposeParts();
      DefaultConfigurationLoader = NewConfigurationData;
      CreateDefaultConfiguration();
    }

    #region ConfigurationBase
    /// <summary>
    /// Creates the default configuration.
    /// </summary>
    public override void CreateDefaultConfiguration()
    {
      CurrentConfiguration = DefaultConfigurationLoader();
    }
    /// <summary>
    /// Gets the instance to be used by a user to configure the selected node.
    /// </summary>
    /// <param name="descriptor">Provides identifying description of the node to be configured.</param>
    /// <returns>Returned object provides access to the instance node configuration edition functionality.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public override IInstanceConfiguration GetInstanceConfiguration(INodeDescriptor descriptor)
    {
      if (descriptor == null)
        throw new ArgumentNullException(nameof(descriptor));
      if (CurrentConfiguration == null)
        return null;
      NodeDescriptorBase _nd = NodeDescriptorBase.Clone(descriptor);
      return InstanceConfigurationFactory.GetIInstanceConfiguration(CurrentConfiguration.GetInstanceConfiguration(_nd), CurrentConfiguration.GetMessageHandlers(), TraceSource.TraceData, () => this.RaiseOnChangeEvent());
    }
    /// <summary>
    /// Gets the configuration editor - user interface to edit the plug-in configuration file.
    /// </summary>
    /// <returns>Represents a window or dialog box that makes up an application's user interface to be used to edit configuration file.</returns>
    /// <exception cref="System.ArgumentNullException">Configuration Editor is unavailable.</exception>
    public override void EditConfiguration()
    {
      if (ConfigurationEditor == null)
        throw new ArgumentNullException(nameof(ConfigurationEditor), "Configuration Editor is unavailable.");
      ConfigurationEditor.EditConfiguration(CurrentConfiguration);
    }
    /// <summary>
    /// Gets the default name of the file created from the name provided the assembly configuration file.
    /// </summary>
    /// <value>The default name of the file - <c>UANetworkingConfiguration.uasconfig</c> if not changed.</value>
    public override string DefaultFileName
    {
      get
      {
        return String.Format("{0}.{1}", Settings.DefaultConfigurationFileName, Settings.DefaultConfigurationFileNametExtension);
      }
    }
    /// <summary>
    /// Creates automatically the instance configurations on the best effort basis.
    /// </summary>
    /// <param name="descriptors">The descriptors of nodes.</param>
    /// <param name="SkipOpeningConfigurationFile">if set to <c>true</c> skip opening configuration file.</param>
    /// <param name="CancelWasPressed">if set to <c>true</c> cancel was pressed.</param>
    /// <exception cref="System.ArgumentNullException">Configuration Editor is unavailable.</exception>
    public override void CreateInstanceConfigurations(INodeDescriptor[] descriptors, bool SkipOpeningConfigurationFile, out bool CancelWasPressed)
    {
      CancelWasPressed = false;
      if (ConfigurationEditor == null)
        throw new ArgumentNullException(nameof(ConfigurationEditor), "Configuration Editor is unavailable.");
      bool _CancelWasPressed = false;
      ConfigurationEditor.CreateInstanceConfigurations(descriptors, SkipOpeningConfigurationFile, x => _CancelWasPressed = x);
      CancelWasPressed = _CancelWasPressed;
    }
    /// <summary>
    /// Saves the configuration.
    /// </summary>
    /// <param name="solutionFilePath">The solution file path.</param>
    /// <param name="configurationFile">The configuration file.</param>
    public override void SaveConfiguration(string solutionFilePath, FileInfo configurationFile)
    {
      base.SaveConfiguration(configurationFile);
    }
    #endregion

    #region MEF injection points
    /// <summary>
    /// Gets or sets the configuration editor - an access point to the external component.
    /// </summary>
    /// <value>The configuration editor.</value>
    public IConfigurationEditor ConfigurationEditor { get; private set; }
    /// <summary>
    /// Gets or sets the instance configuration factory.
    /// </summary>
    /// <value>The instance configuration factory.</value>
    public IInstanceConfigurationFactory InstanceConfigurationFactory { get; set; }
    #endregion

    /// <summary>
    /// Gets or sets the default configuration loader.
    /// </summary>
    /// <value>The default configuration loader <see cref="Func{ConfigurationDataType}"/>.</value>
    public Func<ConfigurationData> DefaultConfigurationLoader { private get; set; }
    #endregion

    #region private
    private void ComposeParts()
    {
      IServiceLocator _locator = ServiceLocator.Current;
      this.ConfigurationEditor = _locator.GetInstance<IConfigurationEditor>();
      this.InstanceConfigurationFactory = _locator.GetInstance<IInstanceConfigurationFactory>();
    }
    private static ConfigurationData NewConfigurationData()
    {
      return new ConfigurationData() { DataSets = new DataSetConfiguration[] { }, MessageHandlers = new MessageHandlerConfiguration[] { } };
    }
    #endregion

  }

}
