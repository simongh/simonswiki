using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.IO;

namespace ClinPhone.Wiki.XmlProvider
{
    public class XmlPage : PageBase
    {
        private XmlDocument _Doc;
        private string _Path;
        private string _Body;
        private XmlNode _Revision;

        public override string Name
        {
            get { return _Doc.SelectSingleNode("//Name").InnerText; }
        }

        public override string Body
        {
            get
            {
                if (_Body == null)
                {
                    XmlNode node = _Doc.SelectSingleNode("//Revisions/Revision[last()]");
                    if (node == null) return "";

                    return node.InnerText;
                }

                return _Body;
            }
            set
            {
                _Body = value;
            }
        }

        public override string Namespace
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string CreatedBy
        {
            get { return _Doc.SelectSingleNode("//Revisions/Revision[1]/@modifiedBy").Value; }
        }

        public override bool IsLatest
        {
            get { return _Doc.SelectSingleNode("//Revisions/Revision[last()]/@version").Value == this.Version.Version.ToString(); }
        }

        public XmlPage(string path) : this(path, 0)
        { }

        public XmlPage(string path, int version)
        {
            _Path = path;
            _Doc = new XmlDocument();

            if (File.Exists(_Path))
                Load(version);
            else
                CreateNew();
        }

        private void Load(int version)
        {
            _Doc.Load(_Path);

            string rev = version == 0 ? "last()" : ("version=" + version.ToString());
            _Revision = _Doc.SelectSingleNode("//Revisions/Revision[" + rev + "]");

            if (_Revision == null) throw new WikiException("The requested version could not be loaded");

            int ver = int.Parse(_Revision.Attributes["version"].Value);
            DateTime when = DateTime.Parse(_Revision.Attributes["modified"].Value);
            this.Version = new VersionDetails(when, _Revision.Attributes["modifiedBy"].Value, _Revision.Attributes["ipAddress"].Value, version);
        }

        private void CreateNew()
        {
            const string schema = "<Page><Name></Name><Revisions><Revision version=\"\" modifiedBy=\"\" modified=\"\" ipAddress=\"\"></Revision></Revisions></Page>";

            _Doc.LoadXml(schema);
            _Doc.SelectSingleNode("//Revision/@version").Value = "1";
            _Doc.SelectSingleNode("//Revision/@modified").Value = DateTime.Now.ToString();

            _Revision = _Doc.SelectSingleNode("//Revisions/Revision[1]");

            this.Version = new VersionDetails(DateTime.Now, GetCurrentUser(), "", 1);
       }


   }
}
