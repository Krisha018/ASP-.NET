namespace Form.Areas.LOC_Contry.Controllers
{
    internal class SqlConnection
    {
        private string connectionStr;

        public SqlConnection(string connectionStr)
        {
            this.connectionStr = connectionStr;
        }

        internal SqlCommand CreateCommand()
        {
            throw new NotImplementedException();
        }

        internal void Open()
        {
            throw new NotImplementedException();
        }
    }
}