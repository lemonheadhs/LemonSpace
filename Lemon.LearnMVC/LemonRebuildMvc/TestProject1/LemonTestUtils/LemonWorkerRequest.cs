using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.IO;
using System.Threading;

namespace TestProject1.LemonTestUtils
{
    public class LemonWorkerRequest : SimpleWorkerRequest
    {
        private bool _hasRuntimeInfo;
        private String _appVirtPath;       // "/foo"
        private String _appPhysPath;       // "c:\foo\"
        private String _page;
        private String _pathInfo;
        private String _queryString;
        private TextWriter _output;
        private String _installDir;

        public LemonWorkerRequest(String page, String query, TextWriter output)
            : base(page, query, output)
        {
            _queryString = query;
            _output = output;
            _page = page;

            ExtractPagePathInfo();

            _appPhysPath = Thread.GetDomain().GetData(".appPath").ToString();
            _appVirtPath = Thread.GetDomain().GetData(".appVPath").ToString();
            //_installDir = HttpRuntime.AspInstallDirectoryInternal;

            _hasRuntimeInfo = true;
        }

        private void ExtractPagePathInfo()
        {
            string[] fileExtensions = { ".aspx", ".html" };
            if (!string.IsNullOrEmpty(_page))
            {
                foreach (var fe in fileExtensions)
                {
                    int pos = _page.IndexOf(fe);
                    if (pos > 0)
                    {
                        if (!_page.EndsWith(fe))
                        {
                            _pathInfo = _page.Substring(pos + fe.Length);
                            _page = _page.Substring(0, pos) + fe;
                        }
                        return;
                    }
                }
            }
        }

        public override string GetFilePath()
        {
            return GetPathInternal(false);
        }

        public override string GetPathInfo()
        {
            return (_pathInfo != null) ? _pathInfo : String.Empty;
        }

        private String GetPathInternal(bool includePathInfo)
        {
            String s = _appVirtPath.Equals("/") ? ("/" + _page) : (_appVirtPath + "/" + _page);

            if (includePathInfo && _pathInfo != null)
                return s + _pathInfo;
            else
                return s;
        }
    }
}
