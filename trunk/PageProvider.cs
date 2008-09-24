using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace ClinPhone.Wiki
{
    public abstract class PageProviderBase : System.Configuration.Provider.ProviderBase
    {
        public abstract bool PageExists(string path);

        public abstract bool PageExists(string page, string space);

        public abstract bool NamespaceExists(string space);

        public abstract PageCollection GetNamespacePages(string space);

        public abstract void CreateNamespace(string space);

        public abstract PageBase GetPage(string path);

        public abstract PageBase GetPageVersion(string path, int version);

        //public abstract string GetHistory(string path);

        public abstract PageBase CreatePage(string path);

        public abstract PageBase CreatePage(string page, string space);

        //public abstract void UpdatePage(PageBase page);

        protected string[] SplitNamespace(string path)
        {
            return path.Split(':');
        }

    }
}
