using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

namespace ClinPhone.Wiki.XmlProvider
{
    public class XmlPageProvider : PageProviderBase
    {
        private string _Base;

        public override bool PageExists(string path)
        {
            string folder = this.NamespaceToPath(path) + ".xml";

            return File.Exists(folder);
        }

        public override bool PageExists(string page, string space)
        {
            return this.PageExists(space + ":" + page);
        }

        public override bool NamespaceExists(string space)
        {
            return Directory.Exists(NamespaceToPath(space));
        }

        public override PageCollection GetNamespacePages(string space)
        {
            if (!NamespaceExists(space)) return null;

            PageCollection ret = new PageCollection();
            foreach (string item in Directory.GetFiles(NamespaceToPath(space),"*.xml"))
            {
                ret.Add(GetPage(item));
            }

            return ret;
        }

        public override void CreateNamespace(string space)
        {
            string[] folders = space.Split(':');
            string path = _Base;
            foreach (string item in folders)
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path += "\\" + item;
            }
        }

        public override PageBase GetPage(string path)
        {
            string file = NamespaceToPath(path) + ".xml";
            if (!File.Exists(file)) return null;

            XmlPage ret = new XmlPage(file);
            return ret;
        }

        public override PageBase GetPageVersion(string path, int version)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        //public override string GetHistory(string path)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        public override PageBase CreatePage(string path)
        {
            string space = null;
            if (path.IndexOf(":") > -1)
                space = path.Substring(path.LastIndexOf(":"));

            return CreatePage(path.Substring(path.LastIndexOf(":") + 1), space);
        }

        public override PageBase CreatePage(string page, string space)
        {
            if (!NamespaceExists(space))
                CreateNamespace(space);

            if (PageExists(page, space))
                throw new WikiException("A page with that name already exists.");

            return this.GetPage(space + ":" + page);
        }

        //public override void UpdatePage(PageBase page)
        //{
            
        //}

        private string NamespaceToPath(string space)
        {
            return _Base + space.Replace(':', '\\');
        }

    }
}
