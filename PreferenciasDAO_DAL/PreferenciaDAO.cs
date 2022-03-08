using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using PreferenciasVO_Model;

namespace PreferenciasDAO_DAL
{
    public class PreferenciaDAO : DAO_DAL
    {
        OleDbCommand objComando;
        OleDbDataAdapter objAdaptador;
        OleDbDataReader objLeitorBD;

        DataTable objTabela;

        public List<string> ImpBDCon()
        {
            try
            {
                List<string> resultado = new List<string>();

                AbreConexao();

                objComando = new OleDbCommand();
                objComando.Connection = ObjConexao;
                objComando.CommandText = "SELECT ID, Descricao FROM Preferências_3";

                objLeitorBD = objComando.ExecuteReader();
                while (objLeitorBD.Read())
                {
                    resultado.Add(objLeitorBD["Descricao"].ToString());
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas na importação de dados con ==> " + ex.Message);
            }
            finally
            {
                FechaConexao();
            }
        }

        public List<string> ImpBDDesc()
        {
            try
            {
                List<string> resultado = new List<string>();

                objComando = new OleDbCommand();
                objComando.Connection = ObjConexao;
                objComando.CommandText = "SELECT ID, Descricao FROM Preferências_3";

                objAdaptador = new OleDbDataAdapter();
                objAdaptador.SelectCommand = objComando;

                objTabela = new DataTable();
                objAdaptador.Fill(objTabela);

                foreach(DataRow drItemDaTabela in objTabela.Rows)
                {
                    resultado.Add(drItemDaTabela["Descricao"].ToString());
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas na importação de dados desc ==> " + ex.Message);
            }
        }

        public override DataTable ConsultarBD(Object objParPreferenciaVO_VO)
        {
            try
            {
                //casting - modelagem do tipo object
                PreferenciaVO objParPreferenciaVO = (PreferenciaVO)objParPreferenciaVO_VO;

                objComando = new OleDbCommand();
                objComando.Connection = ObjConexao;

                if (objParPreferenciaVO.ID > 0)
                {
                    objComando.CommandText = "SELECT ID, Descricao FROM Preferências_3 WHERE ID = :parID";
                    objComando.Parameters.AddWithValue("parID", objParPreferenciaVO.getID());
                }
                else if (string.IsNullOrEmpty(objParPreferenciaVO.getDescricao()))
                {
                    objComando.CommandText = "SELECT ID, Descricao FROM Preferências_3";
                }
                else
                {
                    objComando.CommandText = "SELECT ID, Descricao FROM Preferências_3 WHERE Descricao = :parDescricao";
                    objComando.Parameters.AddWithValue("parDescricao", objParPreferenciaVO.getDescricao());
                }

                objAdaptador = new OleDbDataAdapter();
                objAdaptador.SelectCommand = objComando;

                objTabela = new DataTable();
                objAdaptador.Fill(objTabela);

                return objTabela;
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas na consulta de dados ==> " + ex.Message);
            }
        }

        public override void ConsultarBD(ref Object objParPreferenciaVO_VO)
        {
            try
            {
                PreferenciaVO objParPreferenciaVO = (PreferenciaVO) objParPreferenciaVO_VO;

                objComando = new OleDbCommand();
                objComando.Connection = ObjConexao;

                if (objParPreferenciaVO.ID > 0)
                {
                    objComando.CommandText = "SELECT ID, Descricao FROM Preferências_3 WHERE ID = :parID";
                    objComando.Parameters.AddWithValue("parID", objParPreferenciaVO.getID());
                }
                else if (string.IsNullOrEmpty(objParPreferenciaVO.getDescricao()))
                {
                    objComando.CommandText = "SELECT ID, Descricao FROM Preferências_3";
                }
                else
                {
                    objComando.CommandText = "SELECT ID, Descricao FROM Preferências_3 WHERE Descricao = :parDescricao";
                    objComando.Parameters.AddWithValue("parDescricao", objParPreferenciaVO.getDescricao());
                }

                objAdaptador = new OleDbDataAdapter();
                objAdaptador.SelectCommand = objComando;

                objTabela = new DataTable();
                objAdaptador.Fill(objTabela);

                foreach (DataRow drItemObjPreferenciaVODaTabela in objTabela.Rows)
                {
                    PreferenciaVO objPreferenciaVOaSerIncluidaNaTabela = new PreferenciaVO(
                                                                    Convert.ToInt32(drItemObjPreferenciaVODaTabela["ID"].ToString()), 
                                                                    drItemObjPreferenciaVODaTabela["Descricao"].ToString());
                    objParPreferenciaVO.PreferenciaVOCollection.Add(objPreferenciaVOaSerIncluidaNaTabela);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas na consulta de dados ref ==> " + ex.Message);
            }
        }

        public override bool InserirBD(Object objParPreferenciaVO_VO)
        {
            try
            {
                PreferenciaVO objParPreferenciaVO = (PreferenciaVO)objParPreferenciaVO_VO;

                bool resultado = false;

                AbreConexao();

                objComando = new OleDbCommand();
                objComando.Connection = ObjConexao;

                objComando.CommandText = "INSERT INTO Preferências_3 (Descricao) VALUES (:parDescricao)";
                objComando.Parameters.AddWithValue("parDescricao", objParPreferenciaVO.getDescricao());
                if (objComando.ExecuteNonQuery() > 0)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas na inserção de dados ==> " + ex.Message);
            }
            finally
            {
                FechaConexao();
            }
        }

        public override bool ExcluirBD(Object objParPreferenciaVO_VO)
        {
            try
            {
                PreferenciaVO objParPreferenciaVO = (PreferenciaVO)objParPreferenciaVO_VO;

                bool resultado = false;

                AbreConexao();

                objComando = new OleDbCommand();
                objComando.Connection = ObjConexao;

                objComando.CommandText = "DELETE FROM Preferências_3 WHERE ID = :parID";
                objComando.Parameters.AddWithValue("parID", objParPreferenciaVO.getID());
                if (objComando.ExecuteNonQuery() > 0)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas na exclusão de dados ==> " + ex.Message);
            }
            finally
            {
                FechaConexao();
            }
        }

        public override bool AlterarBD(Object objParPreferenciaVO_VO)
        {
            try
            {
                PreferenciaVO objParPreferenciaVO = (PreferenciaVO)objParPreferenciaVO_VO;

                bool resultado = false;

                AbreConexao();

                objComando = new OleDbCommand();
                objComando.Connection = ObjConexao;

                objComando.CommandText = "UPDATE Preferências_3 SET Descricao = :parDescricao WHERE ID = :parID";
                objComando.Parameters.AddWithValue("parDescricao", objParPreferenciaVO.getDescricao());
                objComando.Parameters.AddWithValue("parID", objParPreferenciaVO.getID());

                if (objComando.ExecuteNonQuery() > 0)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas na alteração de dados ==> " + ex.Message);
            }
            finally
            {
                FechaConexao();
            }
        }
    }
}
