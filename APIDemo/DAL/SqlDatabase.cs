using System.Data;
using System.Data.Common;

namespace APIDemo.DAL
{
    internal class SqlDatabase
    {
        private string connString;

        public SqlDatabase(string connString)
        {
            this.connString = connString;
        }

        internal IDataReader ExecuteReader(DbCommand dbCommand)
        {
            throw new NotImplementedException();
        }

        internal DbCommand GetStoredProcCommand(string v)
        {
            throw new NotImplementedException();
        }
    }
}