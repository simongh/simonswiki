using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace ClinPhone.Wiki.SqlProvider
{
    public class SqlPage : PageBase
    {
        public override string Name
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string Body
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public override string Namespace
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string CreatedBy
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override bool IsLatest
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }
    }
}
