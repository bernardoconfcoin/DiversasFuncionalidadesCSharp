using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PreferenciasDAO_DAL;
using PreferenciasVO_Model;

namespace PreferenciasFachada_Facade
{
    public class FamiliarFD
    {
        FamiliarDAO objFamiliarDAO;

        //public DataTable ConsultarBDFamiliar(FamiliarVO objParFamiliarVO)
        //{
        //    try
        //    {
        //        objFamiliarDAO = new FamiliarDAO();
        //        return objFamiliarDAO.ConsultarBDFamiliar(objParFamiliarVO);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public DataTable ConsultarBD(FamiliarVO objParFamiliarVO)
        {
            try
            {
                objFamiliarDAO = new FamiliarDAO();
                Object objVO_VO = (Object)objParFamiliarVO;
                objFamiliarDAO.ConsultarBD(ref objVO_VO);

                DataTable objTabelaRetorno = new DataTable();
                objTabelaRetorno.Columns.Add("Cod");
                objTabelaRetorno.Columns.Add("Nome");
                objTabelaRetorno.Columns.Add("Sexo");
                objTabelaRetorno.Columns.Add("Idade");
                objTabelaRetorno.Columns.Add("Ganho_Total_Mensal");
                objTabelaRetorno.Columns.Add("Gasto_Total_Mensal");
                objTabelaRetorno.Columns.Add("Observacao");

                foreach (FamiliarVO objFamiliarVO in objParFamiliarVO.FamiliarVOCollection)
                {
                    objTabelaRetorno.Rows.Add(objFamiliarVO.Cod,
                                              objFamiliarVO.Nome,
                                              objFamiliarVO.Sexo,
                                              objFamiliarVO.Idade,
                                              objFamiliarVO.Ganho_Total_Mensal,
                                              objFamiliarVO.Gasto_Total_Mensal,
                                              objFamiliarVO.Observacao);
                }
                return objTabelaRetorno;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InserirBD(FamiliarVO objParFamiliarVO)
        {
            try
            {
                objFamiliarDAO = new FamiliarDAO();
                return objFamiliarDAO.InserirBD(objParFamiliarVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExcluirBD(FamiliarVO objParFamiliarVO)
        {
            try
            {
                objFamiliarDAO = new FamiliarDAO();
                return objFamiliarDAO.ExcluirBD(objParFamiliarVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AlterarBDFamiliar(FamiliarVO objParFamiliarVO)
        {
            try
            {
                objFamiliarDAO = new FamiliarDAO();
                return objFamiliarDAO.AlterarBD(objParFamiliarVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
