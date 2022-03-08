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
    public class FamiliarDAO : DAO_DAL
    {
        OleDbCommand objComando;
        OleDbDataAdapter objAdaptador;

        DataTable objTabela;

        public override DataTable ConsultarBD(Object objParFamiliarVO_VO)
        {
            try
            {
                FamiliarVO objParFamiliarVO = (FamiliarVO) objParFamiliarVO_VO;

                objComando = new OleDbCommand();
                objComando.Connection = ObjConexao;

                if (objParFamiliarVO.Cod > 0)
                {
                    objComando.CommandText = "SELECT Cod, Nome, Sexo, Idade, Ganho_Total_Mensal, Gasto_Total_Mensal, Observacao FROM familiares_1 WHERE Cod = :parCod";
                    objComando.Parameters.AddWithValue("parCod", objParFamiliarVO.getCod());
                }
                else if (string.IsNullOrEmpty(objParFamiliarVO.getNome()))
                {
                    objComando.CommandText = "SELECT Cod, Nome, Sexo, Idade, Ganho_Total_Mensal, Gasto_Total_Mensal, Observacao FROM familiares_1";
                }
                else
                {
                    objComando.CommandText = "SELECT Cod, Nome, Sexo, Idade, Ganho_Total_Mensal, Gasto_Total_Mensal, Observacao FROM familiares_1 WHERE Nome = :parNome";
                    objComando.Parameters.AddWithValue("parNome", objParFamiliarVO.getNome());
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

        public override void ConsultarBD(ref Object objParFamiliarVO_VO)
        {
            try
            {
                FamiliarVO objParFamiliarVO = (FamiliarVO)objParFamiliarVO_VO;

                objComando = new OleDbCommand();
                objComando.Connection = ObjConexao;

                if (objParFamiliarVO.Cod > 0)
                {
                    objComando.CommandText = "SELECT Cod, Nome, Sexo, Idade, Ganho_Total_Mensal, Gasto_Total_Mensal, Observacao FROM familiares_1 WHERE Cod = :parCod";
                    objComando.Parameters.AddWithValue("parCod", objParFamiliarVO.getCod());
                }
                else if (string.IsNullOrEmpty(objParFamiliarVO.getNome()))
                {
                    objComando.CommandText = "SELECT Cod, Nome, Sexo, Idade, Ganho_Total_Mensal, Gasto_Total_Mensal, Observacao FROM familiares_1";
                }
                else
                {
                    objComando.CommandText = "SELECT Cod, Nome, Sexo, Idade, Ganho_Total_Mensal, Gasto_Total_Mensal, Observacao FROM familiares_1 WHERE Nome = :parNome";
                    objComando.Parameters.AddWithValue("parNome", objParFamiliarVO.getNome());
                }

                objAdaptador = new OleDbDataAdapter();
                objAdaptador.SelectCommand = objComando;

                objTabela = new DataTable();
                objAdaptador.Fill(objTabela);

                foreach (DataRow drItemObjFamiliarVODaTabela in objTabela.Rows)
                {
                    FamiliarVO objFamiliarVOaSerIncluCodaNaTabela = new FamiliarVO(
                                                                    Convert.ToInt32(drItemObjFamiliarVODaTabela["Cod"].ToString()),
                                                                    drItemObjFamiliarVODaTabela["Nome"].ToString(),
                                                                    drItemObjFamiliarVODaTabela["Sexo"].ToString(),
                                                                    Convert.ToInt32(drItemObjFamiliarVODaTabela["Idade"].ToString()),
                                                                    Convert.ToDouble(drItemObjFamiliarVODaTabela["Ganho_Total_Mensal"].ToString()),
                                                                    Convert.ToDouble(drItemObjFamiliarVODaTabela["Gasto_Total_Mensal"].ToString()),
                                                                    drItemObjFamiliarVODaTabela["Idade"].ToString());

                    objParFamiliarVO.FamiliarVOCollection.Add(objFamiliarVOaSerIncluCodaNaTabela);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas na consulta de dados ref ==> " + ex.Message);
            }
        }

        public override bool InserirBD(Object objParFamiliarVO_VO)
        {
            try
            {
                FamiliarVO objParFamiliarVO = (FamiliarVO)objParFamiliarVO_VO;

                bool resultado = false;

                AbreConexao();

                objComando = new OleDbCommand();
                objComando.Connection = ObjConexao;

                objComando.CommandText = @"INSERT INTO familiares_1 (Nome, Sexo, Idade, Ganho_Total_Mensal, Gasto_Total_Mensal, Observacao) 
                                                            VALUES (:parNome, :parSexo, :parIdade, :parGanho_Total_Mensal, :parGasto_Total_Mensal, :parObservacao)";
                objComando.Parameters.AddWithValue("parNome", objParFamiliarVO.getNome());
                objComando.Parameters.AddWithValue(":parSexo", objParFamiliarVO.getSexo());
                objComando.Parameters.AddWithValue(":parIdade", objParFamiliarVO.getIdade());
                objComando.Parameters.AddWithValue(":parGanho_Total_Mensal", objParFamiliarVO.getGanho_Total_Mensal());
                objComando.Parameters.AddWithValue(":parGasto_Total_Mensal", objParFamiliarVO.getGasto_Total_Mensal());
                objComando.Parameters.AddWithValue(":parObservacao", objParFamiliarVO.getObservacao());

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

        public override bool ExcluirBD(Object objParFamiliarVO_VO)
        {
            try
            {
                FamiliarVO objParFamiliarVO = (FamiliarVO)objParFamiliarVO_VO;

                bool resultado = false;

                AbreConexao();

                objComando = new OleDbCommand();
                objComando.Connection = ObjConexao;

                objComando.CommandText = "DELETE FROM familiares_1 WHERE Cod = :parCod";
                objComando.Parameters.AddWithValue("parCod", objParFamiliarVO.getCod());
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

        public override bool AlterarBD(Object objParFamiliarVO_VO)
        {
            try
            {
                FamiliarVO objParFamiliarVO = (FamiliarVO)objParFamiliarVO_VO;

                bool resultado = false;

                AbreConexao();

                objComando = new OleDbCommand();
                objComando.Connection = ObjConexao;

                objComando.CommandText = @"UPDATE familiares_1 SET Nome = :parNome,
                                                                   Sexo = :parSexo,
                                                                   Idade = :parIdade,
                                                                   Ganho_Total_Mensal = :parGanho_Total_Mensal,
                                                                   Gasto_Total_Mensal = :parGasto_Total_Mensal,
                                                                   Observacao = :parObservacao
                                                                   WHERE Cod = :parCod";
                objComando.Parameters.AddWithValue("parNome", objParFamiliarVO.getNome());
                objComando.Parameters.AddWithValue(":parSexo", objParFamiliarVO.getSexo());
                objComando.Parameters.AddWithValue(":parIdade", objParFamiliarVO.getIdade());
                objComando.Parameters.AddWithValue(":parGanho_Total_Mensal", objParFamiliarVO.getGanho_Total_Mensal());
                objComando.Parameters.AddWithValue(":parGasto_Total_Mensal", objParFamiliarVO.getGasto_Total_Mensal());
                objComando.Parameters.AddWithValue(":parObservacao", objParFamiliarVO.getObservacao());
                objComando.Parameters.AddWithValue("parCod", objParFamiliarVO.getCod());


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
