using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using IPAddressFiltering;
using NLog;

namespace AzureDeployTest.Core
{
    /// <summary>
    /// Helper class to update IPAddressFiltering internal config
    /// </summary>
    public class RolesWithIpConfig
    {
        private static readonly Logger LOGGER = LogManager.GetLogger("RolesWithIpConfig");

        /// <summary>
        /// Not allowed to create this class
        /// TODO make private, public is for moq only
        /// </summary>
        public RolesWithIpConfig() { }

        /// <summary>
        /// Instance of RolesWithIpConfig class
        /// </summary>
        public static readonly RolesWithIpConfig Default = new RolesWithIpConfig();

        /// <summary>
        /// last cache update time
        /// </summary>
        private static DateTime LastRolesWithIpUpdate = DateTime.MinValue;

        /// <summary>
        /// URL for the IPRoles config file
        /// </summary>
        public virtual string RolesWithIPConfigUrl => GetAppSettingsValue("RolesWithIPConfigUrl");

        /// <summary>
        /// loads roles wit IP config from URL
        /// </summary>
        /// <returns></returns>
        public  Dictionary<string, List<string>> GetRolesWithIp()
        {
            var rolesWithIpList = new Dictionary<string, List<string>>();
            string url = RolesWithIPConfigUrl;//"https://s3-us-west-2.amazonaws.com/imsconfig/IPRoles.config";
            if (string.IsNullOrEmpty(url))
            {
                rolesWithIpList.Add(RolesContainer.GLOBAL_ROLE, new List<string> { "0.0.0.0" }); //if no url provided then allow any IP
                return rolesWithIpList;
            }
            try
            {
                using (WebClient client = new WebClient())
                {
                    //load IPROles config
                    string rawConfig = client.DownloadString(url);
                    rolesWithIpList = ConfigHelper.ParseRolesWithIpFromTxt(rawConfig);
                }
            }
            catch (Exception ex)
            {
                //log
                LOGGER.Fatal(ex, $"Error Processing IPRoles. {ex.Message}");
                rolesWithIpList.Clear();
                rolesWithIpList.Add(RolesContainer.GLOBAL_ROLE, new List<string> { "0.0.0.0" }); //if not parsed then allow any IP
            }
            return rolesWithIpList;
        }

        /// <summary>
        /// Updates internal IProles list, 5 min caching
        /// </summary>
        /// <param name="doForce">forces to ignore the cache</param>
        public void UpdateRolesWithIp(bool doForce = false)
        {
            if (doForce)
            {
                LastRolesWithIpUpdate = DateTime.Now;
                RolesContainer.UpdateRolesList(GetRolesWithIp());
            }
            else
            {
                if (LastRolesWithIpUpdate < DateTime.Now.AddMinutes(-5))
                {
                    LastRolesWithIpUpdate = DateTime.Now;
                    RolesContainer.UpdateRolesList(GetRolesWithIp());
                }
            }
        }
        /// <summary>
        /// Returns ConfigurationManager.AppSettings value by Key
        /// </summary>
        /// <param name="key">string name of the web.config param</param>
        /// <returns></returns>
        private static string GetAppSettingsValue(string key)
        {
            string value = ConfigurationManager.AppSettings[key];
            if (value != null)
            {
                return value;
            }
            throw new ApplicationException("Can't read web.config appSettings key:" + key);
        }
    }
}