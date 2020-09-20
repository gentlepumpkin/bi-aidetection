using OSVersionExt.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSVersionExt.MajorVersion10
{
    /// <summary>
    /// Get the release id and UBR (Update Build Revision) on Windows system having major version 10.
    /// </summary>
    public class MajorVersion10Properties
    {
        private const string registryCurrentVersionKeyName = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion";
        private const string releaseIdKeyName = "ReleaseId";
        private const string releaseIdDefault = null;
        private const string UBRkeyName = "UBR";
        private const string UBRdefault = null;

        private IRegistry _registryProvider;


        private string _releaseId = releaseIdDefault;
        private string _UBR = UBRdefault;

        /// <summary>
        /// Returns the Windows release ID.
        /// </summary>
        /// <remarks>returns the release id or null, if detection has failed.</remarks>
        public string ReleaseId { get => _releaseId; }

        /// <summary>
        /// Gets the Update Build Revision of a Windows 10 system
        /// </summary>
        /// <remarks>returns null, if detection has failed.</remarks>
        public string UBR { get => _UBR; }

        /// <summary>
        /// Create instance with custom registry provider.
        /// </summary>
        /// <param name="registryProvider"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public MajorVersion10Properties(IRegistry registryProvider)
        {
            if (registryProvider != null)
            {
                this._registryProvider = registryProvider;
                GetAllProperties();
            }
            else
                throw new ArgumentNullException();
        }

        public MajorVersion10Properties()
        {
            this._registryProvider = new RegistryProviderDefault();
            GetAllProperties();
        }

        private void GetAllProperties()
        {
            this._releaseId = this.GetReleaseId();
            this._UBR = this.GetUBR();
        }

        /// <summary>        
        /// The version number representing feature updates, is referred as the release id, such as 1903, 1909.
        /// </summary>
        /// <returns>Returns the release id or null, if value is not available.</returns>
        /// <remarks>Feature updates for Windows 10 are released twice a year, around March and September, via the Semi-Annual Channel.</remarks>
        private string GetReleaseId()
        {
            return this._registryProvider.GetValue(registryCurrentVersionKeyName, releaseIdKeyName, releaseIdDefault)?.ToString();
        }

        /// <summary>
        /// Gets the  UBR (Update Build Revision).
        /// </summary>
        /// <returns></returns>
        /// <remarks>E.g, it returns 778 for Microsoft Windows [Version 10.0.18363.778] </remarks>
        private string  GetUBR()
        {
            return this._registryProvider.GetValue(registryCurrentVersionKeyName, UBRkeyName, UBRdefault)?.ToString();
        }
    }
}
