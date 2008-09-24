using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;

namespace ClinPhone.Wiki.SqlProvider
{
    public class SqlPageProvider : PageProviderBase
    {
        private DbProviderFactory _DbFactory;
        private string _conn;

        public override bool PageExists(string path)
        {
            DbCommand cmd = GetCommand("SELECT count(*) FROM wiki_pages WHERE fullname=@fullname");
            cmd.Parameters.Add(GetParameter("@Fullname", path));

            try
            {
                cmd.Connection.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
            catch (Exception ex)
            {
                throw new WikiException("Unable to verify page exists.", ex);
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open) cmd.Connection.Close();
            }
        }

        public override bool PageExists(string page, string space)
        {
            return PageExists(space + ":" + page);
        }

        public override bool NamespaceExists(string space)
        {
            DbCommand cmd = GetCommand("SELECT count(*) FROM wiki_namespaces WHERE fullname=@fullname");
            cmd.Parameters.Add(GetParameter("@Fullname", space));

            try
            {
                cmd.Connection.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
            catch (Exception ex)
            {
                throw new WikiException("Unable to verify namespace exists.", ex);
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open) cmd.Connection.Close();
            }
        }

        public override PageCollection GetNamespacePages(string space)
        {
            DbCommand cmd = GetCommand("dbo.Wiki_NamespacePages_sp");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(GetParameter("@Fullname", space));

            DbDataAdapter da = _DbFactory.CreateDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = null;
            try
            {
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                throw new WikiException("Unable to retrive pages in namespace.", ex);
            }

            PageCollection ret = new PageCollection();
            foreach (DataRow row in dt.Rows)
            {
                ret.Add(GetPage(row));
            }

            return ret;
        }

        public override void CreateNamespace(string space)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override PageBase GetPage(string path)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override PageBase GetPageVersion(string path, int version)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private PageBase GetPage(DataRow row)
        {
            return new SqlPage();
        }

        public override PageBase CreatePage(string path)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override PageBase CreatePage(string path, string space)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private DbCommand GetCommand(string sql)
        {
            DbCommand cmd = _DbFactory.CreateCommand();
            cmd.Connection = _DbFactory.CreateConnection();
            cmd.Connection.ConnectionString = ConfigurationManager.ConnectionStrings[_conn].ConnectionString;
            cmd.CommandText = sql;

            return cmd;
        }

        private DbParameter GetParameter(string name, object value)
        {
            DbParameter ret = _DbFactory.CreateParameter();
            ret.ParameterName = name;
            ret.Value = value;

            return ret;
        }
    }
}
