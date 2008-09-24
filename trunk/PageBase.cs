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
    public abstract class PageBase
    {
        private VersionDetails _Version;

        public abstract string Name
        {
            get;
        }

        public abstract string Body
        {
            get;
            set;
        }

        public VersionDetails Version
        {
            get { return _Version; }
            protected set { _Version = value; }
        }

        public abstract string Namespace
        {
            get;
        }

        //public bool IsRevision
        //{
        //    get
        //    {
        //        throw new System.NotImplementedException();
        //    }
        //    set
        //    {
        //    }
        //}

        public abstract string CreatedBy
        {
            get;
        }

        public abstract bool IsLatest
        {
            get;
        }

        protected PageBase()
        { }

        public virtual string GetCurrentUser()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public virtual string GetUserIpAddress()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }
    }
}
