using System;
using System.Web.Hosting;

namespace AzureDeployTest.Core
{
    public class PublishInfo
    {
        public string _commit;
        public string _publishedDate;
        public string _branch;

        private void RefreshInfo()
        {
            _commit = "";
            _publishedDate = "";
            _branch = "";
            string infoPath = HostingEnvironment.MapPath("~/info.txt");
            if (infoPath == null)
            {
                return;
            }
            if (!System.IO.File.Exists(infoPath))
            {
                _commit = "Unknown";
                _publishedDate = "N/A";
                _branch = "Unknown";
                return;
            }
            string[] lines = System.IO.File.ReadAllLines(infoPath);
            foreach (string line in lines)
            {
                if (line.Contains(":"))
                {
                    string left = line.Split(':')[0];
                    string right = line.Substring(line.IndexOf(':') + 1);
                    if (string.Compare(left, "commit", StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        _commit = right.Trim();
                    }
                    if (string.Compare(left, "publishDate", StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        _publishedDate = right.Trim();
                    }
                    if (string.Compare(left, "branch", StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        _branch = right.Trim();
                    }
                }
            }
        }

        public string Commit
        {
            get
            {
                if (_commit==null)
                {
                    RefreshInfo();
                }
                return _commit;
            }
        }

        public string PublishedDate
        {
            get
            {
                if (_publishedDate == null)
                {
                    RefreshInfo();
                }
                return _publishedDate;
            }
        }
        public string Branch
        {
            get
            {
                if (_branch == null)
                {
                    RefreshInfo();
                }
                return _branch;
            }
        }
    }
}