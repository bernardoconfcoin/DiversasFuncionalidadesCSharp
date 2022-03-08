using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Configuration;

namespace PreferenciasDAO_DAL
{
    public class DB_DAO
    {
        private static OleDbConnection objConexao;

        public static OleDbConnection getConexao()
        {
            if (objConexao == null)
            {
                setConexao(ConfigurationSettings.AppSettings["StringDeConexao"].ToString());
            }
            return objConexao;
        }

        public static void setConexao(string strConnectionString)
        {
            objConexao = new OleDbConnection(strConnectionString);
        }

        public static OleDbConnection ObjConexao
        {
            get
            {
                if (objConexao == null)
                {
                    setConexao(ConfigurationSettings.AppSettings["StringDeConexao"].ToString());
                }
                return objConexao;
            }
        }

        public static void AbreConexao()
        {
            if (objConexao.State == System.Data.ConnectionState.Closed)
            {
                objConexao.Open();
            }
        }

        public static void FechaConexao()
        {
            if (objConexao.State == System.Data.ConnectionState.Open)
            {
                objConexao.Close();
                objConexao.Dispose();
                objConexao = null;
            }
        }
    }
}
