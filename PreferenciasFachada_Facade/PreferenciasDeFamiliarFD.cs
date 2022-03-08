using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PreferenciasVO_Model;
using PreferenciasDAO_DAL;

namespace PreferenciasFachada_Facade
{
    public class PreferenciasDeFamiliarFD
    {
        PreferenciasDeFamiliarDAO objPreferenciasDeFamiliarDAO;

        //public DataTable ConsultarBD(PreferenciasDeFamiliarVO objParPreferenciasDeFamiliarVO)
        //{
        //    try
        //    {
        //        objPreferenciasDeFamiliarDAO = new PreferenciasDeFamiliarDAO();
        //        return objPreferenciasDeFamiliarDAO.ConsultarBD(objParPreferenciasDeFamiliarVO);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public DataTable ConsultarBD(PreferenciasDeFamiliarVO objParPreferenciasDeFamiliarVO)
        {
            try
            {
                objPreferenciasDeFamiliarDAO = new PreferenciasDeFamiliarDAO();
                Object objVO_VO = (Object)objParPreferenciasDeFamiliarVO;
                objParPreferenciasDeFamiliarVO.PreferenciaDeFamiliarVOCollection.Clear();
                objPreferenciasDeFamiliarDAO.ConsultarBD(ref objVO_VO);

                DataTable objTabelaRetorno = new DataTable();
                objTabelaRetorno.Columns.Add("Cod");
                objTabelaRetorno.Columns.Add("ID");
                objTabelaRetorno.Columns.Add("Intensidade");
                objTabelaRetorno.Columns.Add("Observacao");

                foreach (PreferenciasDeFamiliarVO objPreferenciasDeFamiliarVO in objParPreferenciasDeFamiliarVO.PreferenciaDeFamiliarVOCollection)
                {
                    objTabelaRetorno.Rows.Add(objPreferenciasDeFamiliarVO.ObjFamiliarVO.Cod,
                                              objPreferenciasDeFamiliarVO.ObjPreferenciaVO.ID,
                                              objPreferenciasDeFamiliarVO.Intensidade,
                                              objPreferenciasDeFamiliarVO.Observacao);
                }
                return objTabelaRetorno;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InserirBD(PreferenciasDeFamiliarVO objParPreferenciasDeFamiliarVO)
        {
            try
            {
                objPreferenciasDeFamiliarDAO = new PreferenciasDeFamiliarDAO();
                return objPreferenciasDeFamiliarDAO.InserirBD(objParPreferenciasDeFamiliarVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExcluirBD(PreferenciasDeFamiliarVO objParPreferenciasDeFamiliarVO)
        {
            try
            {
                objPreferenciasDeFamiliarDAO = new PreferenciasDeFamiliarDAO();
                return objPreferenciasDeFamiliarDAO.ExcluirBD(objParPreferenciasDeFamiliarVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AlterarBD(PreferenciasDeFamiliarVO objParPreferenciasDeFamiliarVO)
        {
            try
            {
                objPreferenciasDeFamiliarDAO = new PreferenciasDeFamiliarDAO();
                return objPreferenciasDeFamiliarDAO.AlterarBD(objParPreferenciasDeFamiliarVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
