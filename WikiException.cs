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
    public class WikiException : Exception
    {
        public WikiException()
            : base()
        { }

        public WikiException(string message)
            : base(message)
        { }

        public WikiException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
