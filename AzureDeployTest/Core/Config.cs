using System;
using System.Configuration;
using System.Globalization;
using NLog;

namespace AzureDeployTest.Core
{
    /// <summary>
    /// IConfig interface must be used instead of Config class. SO the config can be mocked in Unit tests.
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// site root without last / eg: http://www.example.com
        /// </summary>
        string SiteRoot { get; }
        string SiteName { get; }
        string SitePassword { get; }

        string Commit { get; }

        string PublishedDate { get; }

        string Branch { get; }

        
        string FromEmail { get; }

        string FromName { get; }
        
        string MapPath(string path);
    }

    /// <summary>
    /// Website config. Reads values from web.config
    /// </summary>
    public class Config : IConfig
    {
        private static readonly Logger LOGGER = LogManager.GetLogger("Config");


        public static readonly CultureInfo ENUSCulture = new CultureInfo("en-US");
        public static IConfig DefaultConfig = new Config();
        public PublishInfo PublishInfo { get; } = new PublishInfo();

        public string SiteRoot
        {
            get
            {
                var siteRoot = GetAppSettingsValue("SiteRoot");
                if (siteRoot.EndsWith("/") || siteRoot.EndsWith("\\"))
                {
                    siteRoot = siteRoot.Substring(0, siteRoot.Length - 1);
                }
                return siteRoot;
            }
        }
        public virtual string SiteName => GetAppSettingsValue("SiteName");

        /*
        public string SqlConnection
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            }
        }
        */


        private string GetAppSettingsValue(string key)
        {
            string value = ConfigurationManager.AppSettings[key];
            if (value != null)
            {
                return value;
            }
            else
            {
                throw new ApplicationException("Can't read web.config appSettings key:" + key);
            }
        }

        public string SitePassword
        {
            get { return GetAppSettingsValue("SitePassword"); }
        }

        public virtual string FromEmail => GetAppSettingsValue("FromEmail");

        public virtual string FromName => GetAppSettingsValue("FromName");

        public virtual string MapPath(string path)
        {
            return System.Web.Hosting.HostingEnvironment.MapPath(path);
        }
        
        public string Commit => PublishInfo.Commit;

        public string PublishedDate => PublishInfo.PublishedDate;

        public string Branch => PublishInfo.Branch;
    }
}