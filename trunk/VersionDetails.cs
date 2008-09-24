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
    public struct VersionDetails
    {
        private DateTime _Modified;
        private string _Username;
        private string _IPAddress;
        private int _Version;

        public DateTime Modified
        {
            get { return _Modified; }
            set { _Modified = value; }
        }

        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }

        public string IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }

        public int Version
        {
            get { return _Version; }
            set { _Version = value; }
        }

        //public string Type
        //{
        //    get
        //    {
        //        throw new System.NotImplementedException();
        //    }
        //    set
        //    {
        //    }
        //}

        public VersionDetails(DateTime when, string username, string ipAddress, int version)
        {
            _Modified = when;
            _Username = username;
            _IPAddress = ipAddress;
            _Version = version;
        }
    }
}
