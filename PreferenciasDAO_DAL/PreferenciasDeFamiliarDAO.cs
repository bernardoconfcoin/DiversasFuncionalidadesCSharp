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
    public class PreferenciasDeFamiliarDAO : DAO_DAL
    {
        OleDbCommand objComando;
        OleDbDataAdapter objAdaptador;

        DataTable objTabela;

        PreferenciasDeFamiliarVO objPreferenciasDeFamiliarVO;

        public override DataTable ConsultarBD(Object objParPreferenciasDeFamiliarVO_VO)
        {
            try
            {
                PreferenciasDeFamiliarVO objParPreferenciasDeFamiliarVO = (PreferenciasDeFamiliarVO)objParPreferenciasDeFamiliarVO_VO;

                objComando = new OleDbCommand();
                objComando.Connection = ObjConexao;
                if (objParPreferenciasDeFamiliarVO.ObjFamiliarVO.Cod > 0 && objParPreferenciasDeFamiliarVO.ObjPreferenciaVO.ID > 0)
                {
                    objComando.CommandText = "SELECT Cod, ID, Intensidade, Observacao FROM Preferencias_De_Familiares WHERE Cod = :parCod AND ID = :parID";
                    objComando.Parameters.AddWithValue("parCod", objParPreferenciasDeFamiliarVO.ObjFamiliarVO.getCod());
                    objComando.Parameters.AddWithValue("parID", objParPreferenciasDeFamiliarVO.ObjPreferenciaVO.getID());
                }

                else if (objParPreferenciasDeFamiliarVO.ObjFamiliarVO.Cod > 0)
                {
                    objComando.CommandText = "SELECT Cod, ID, Intensidade, Observacao FROM Preferencias_De_Familiares WHERE Cod = :parCod";
                    objComando.Parameters.AddWithValue("parCod", objParPreferenciasDeFamiliarVO.ObjFamiliarVO.getCod());
                }
                else if (objParPreferenciasDeFamiliarVO.ObjFamiliarVO.Cod == 0)
                {
                    objComando.CommandText = "SELECT Cod, ID, Intensidade, Observacao FROM Preferencias_De_Familiares";
                }
                else
                {
                    objComando.CommandText = "SELECT Cod, ID, Intensidade, Observacao FROM Preferencias_De_Familiares WHERE Cod = :parCod AND ID = :parID";
                    objComando.Parameters.AddWithValue("parCod", objParPreferenciasDeFamiliarVO.ObjFamiliarVO.getCod());
                    objComando.Parameters.AddWithValue("parID", objParPreferenciasDeFamiliarVO.ObjPreferenciaVO.getID());
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

        public override void ConsultarBD(ref Object objParPreferenciasDeFamiliarVO_VO)
        {
            try
            {
                PreferenciasDeFamiliarVO objParPreferenciasDeFamiliarVO = (PreferenciasDeFamiliarVO)objParPreferenciasDeFamiliarVO_VO;

                objComando = new OleDbCommand();
                objComando.Connection = ObjConexao;

                if (objParPreferenciasDeFamiliarVO.ObjFamiliarVO.Cod > 0 && objParPreferenciasDeFamiliarVO.ObjPreferenciaVO.ID > 0)
                {
                    objComando.CommandText = "SELECT Cod, ID, Intensidade, Observacao FROM Preferencias_De_Familiares WHERE Cod = :parCod AND ID = :parID";
                    objComando.Parameters.AddWithValue("parCod", objParPreferenciasDeFamiliarVO.ObjFamiliarVO.getCod());
                    objComando.Parameters.AddWithValue("parID", objParPreferenciasDeFamiliarVO.ObjPreferenciaVO.getID());
                }
                else if (objParPreferenciasDeFamiliarVO.ObjFamiliarVO.Cod > 0)
                {
                    objComando.CommandText = "SELECT Cod, ID, Intensidade, Observacao FROM Preferencias_De_Familiares WHERE Cod = :parCod";
                    objComando.Parameters.AddWithValue("parCod", objParPreferenciasDeFamiliarVO.ObjFamiliarVO.getCod());
                }
                else if (objParPreferenciasDeFamiliarVO.ObjFamiliarVO.Cod == 0)
                {
                    objComando.CommandText = "SELECT Cod, ID, Intensidade, Observacao FROM Preferencias_De_Familiares";
                }
                else
                {
                    objComando.CommandText = "SELECT Cod, ID, Intensidade, Observacao FROM Preferencias_De_Familiares WHERE Cod = :parCod AND ID = :parID";
                    objComando.Parameters.AddWithValue("parCod", objParPreferenciasDeFamiliarVO.ObjFamiliarVO.getCod());
                    objComando.Parameters.AddWithValue("parID", objParPreferenciasDeFamiliarVO.ObjPreferenciaVO.getID());
                }

                objAdaptador = new OleDbDataAdapter();
                objAdaptador.SelectCommand = objComando;

                objTabela = new DataTable();
                objAdaptador.Fill(objTabela);

                foreach (DataRow drItemObjPreferenciasDeFamiliarVODaTabela in objTabela.Rows)
                {
                    FamiliarVO objFamiliarVO = new FamiliarVO();
                    objFamiliarVO.Cod = Convert.ToInt32(drItemObjPreferenciasDeFamiliarVODaTabela["Cod"].ToString());
                    FamiliarDAO objFamiliarDAO = new FamiliarDAO();
                    Object objFamVO_VO = (Object)objFamiliarVO;
                    objFamiliarDAO.ConsultarBD(ref objFamVO_VO);
                    objFamiliarVO = objFamiliarVO.FamiliarVOCollection.First<FamiliarVO>();

                    PreferenciaVO objPreferenciaVO = new PreferenciaVO();
                    objPreferenciaVO.ID = Convert.ToInt32(drItemObjPreferenciasDeFamiliarVODaTabela["ID"].ToString());
                    PreferenciaDAO objPreferenciaDAO = new PreferenciaDAO();
                    Object objPrefVO_VO = (Object)objPreferenciaVO;
                    objPreferenciaDAO.ConsultarBD(ref objPrefVO_VO);
                    objPreferenciaVO = objPreferenciaVO.PreferenciaVOCollection.First<PreferenciaVO>();

                    PreferenciasDeFamiliarVO objPreferenciasDeFamiliarVOaSerIncluCodaNaTabela = new PreferenciasDeFamiliarVO(
                                                        objFamiliarVO,
                                                        objPreferenciaVO,
                                                        Convert.ToSingle(drItemObjPreferenciasDeFamiliarVODaTabela["Intensidade"].ToString()),
                                                        drItemObjPreferenciasDeFamiliarVODaTabela["Observacao"].ToString());

                    objParPreferenciasDeFamiliarVO.PreferenciaDeFamiliarVOCollection.Add(objPreferenciasDeFamiliarVOaSerIncluCodaNaTabela);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas na consulta de dados ref ==> " + ex.Message);
            }
        }

        public override bool InserirBD(Object objParPreferenciasDeFamiliarVO_VO)
        {
            try
            {
                PreferenciasDeFamiliarVO objParPreferenciasDeFamiliarVO = (PreferenciasDeFamiliarVO)objParPreferenciasDeFamiliarVO_VO;

                bool resultado = false;

                AbreConexao();

                objComando = new OleDbCommand();
                objComando.Connection = ObjConexao;

                objComando.CommandText = @"INSERT INTO Preferencias_De_Familiares (Cod, ID, Intensidade, Observacao) 
                                                            VALUES (:parCod, :parID, :parIntensidade, :parObservacao)";
                objComando.Parameters.AddWithValue("parCod", objParPreferenciasDeFamiliarVO.ObjFamiliarVO.getCod());
                objComando.Parameters.AddWithValue(":parID", objParPreferenciasDeFamiliarVO.ObjPreferenciaVO.getID());
                objComando.Parameters.AddWithValue(":parIntensidade", objParPreferenciasDeFamiliarVO.getIntensidade());
                objComando.Parameters.AddWithValue(":parObservacao", objParPreferenciasDeFamiliarVO.getObservacao());

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

        public override bool ExcluirBD(Object objParPreferenciasDeFamiliarVO_VO)
        {
            try
            {
                PreferenciasDeFamiliarVO objParPreferenciasDeFamiliarVO = (PreferenciasDeFamiliarVO)objParPreferenciasDeFamiliarVO_VO;

                bool resultado = false;

                AbreConexao();

                objComando = new OleDbCommand();
                objComando.Connection = ObjConexao;

                objComando.CommandText = "DELETE FROM Preferencias_De_Familiares WHERE Cod = :parCod AND ID = :parID";
                objComando.Parameters.AddWithValue("parCod", objParPreferenciasDeFamiliarVO.ObjFamiliarVO.getCod());
                objComando.Parameters.AddWithValue(":parID", objParPreferenciasDeFamiliarVO.ObjPreferenciaVO.getID());
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

        public override bool AlterarBD(Object objParPreferenciasDeFamiliarVO_VO)
        {
            try
            {
                PreferenciasDeFamiliarVO objParPreferenciasDeFamiliarVO = (PreferenciasDeFamiliarVO)objParPreferenciasDeFamiliarVO_VO;

                bool resultado = false;

                AbreConexao();

                objComando = new OleDbCommand();
                objComando.Connection = ObjConexao;

                objComando.CommandText = @"UPDATE Preferencias_De_Familiares SET
                                                                   Intensidade = :parIntensidade,
                                                                   Observacao = :parObservacao
                                                                   WHERE Cod = :parCod AND ID = :parID";
                
                objComando.Parameters.AddWithValue(":parIntensidade", objParPreferenciasDeFamiliarVO.getIntensidade());
                objComando.Parameters.AddWithValue(":parObservacao", objParPreferenciasDeFamiliarVO.getObservacao());
                objComando.Parameters.AddWithValue("parCod", objParPreferenciasDeFamiliarVO.ObjFamiliarVO.getCod());
                objComando.Parameters.AddWithValue(":parID", objParPreferenciasDeFamiliarVO.ObjPreferenciaVO.getID());

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
