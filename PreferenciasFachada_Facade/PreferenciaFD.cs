using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreferenciasDAO_DAL;
using PreferenciasVO_Model;
using System.Data;

namespace PreferenciasFachada_Facade
{
    public class PreferenciaFD
    {
        PreferenciaDAO objPreferenciaDAO;

        public List<string> ImpBDCon()
        {
            try
            {
                objPreferenciaDAO = new PreferenciaDAO();
                return objPreferenciaDAO.ImpBDCon();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> ImpBDDesc()
        {
            try
            {
                objPreferenciaDAO = new PreferenciaDAO();
                return objPreferenciaDAO.ImpBDDesc();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public DataTable ConsultarBD(PreferenciaVO objParPreferenciaVO)
        //{
        //    try
        //    {
        //        objPreferenciaDAO = new PreferenciaDAO();
        //        return objPreferenciaDAO.ConsultarBD(objParPreferenciaVO);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public DataTable ConsultarBD(PreferenciaVO objParPreferenciaVO)
        {
            try
            {
                objPreferenciaDAO = new PreferenciaDAO();
                Object objVO_VO = (Object)objParPreferenciaVO;
                objParPreferenciaVO.PreferenciaVOCollection.Clear();
                objPreferenciaDAO.ConsultarBD(ref objVO_VO);

                DataTable objTabelaRetorno = new DataTable();
                objTabelaRetorno.Columns.Add("ID");
                objTabelaRetorno.Columns.Add("Descricao");

                foreach (PreferenciaVO objPreferenciaVO in objParPreferenciaVO.PreferenciaVOCollection)
                {
                    objTabelaRetorno.Rows.Add(objPreferenciaVO.ID, objPreferenciaVO.Descricao);
                }
                return objTabelaRetorno;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InserirBD(PreferenciaVO objParPreferenciaVO)
        {
            try
            {
                objPreferenciaDAO = new PreferenciaDAO();
                return objPreferenciaDAO.InserirBD(objParPreferenciaVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExcluirBD(PreferenciaVO objParPreferenciaVO)
        {
            try
            {
                objPreferenciaDAO = new PreferenciaDAO();
                return objPreferenciaDAO.ExcluirBD(objParPreferenciaVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AlterarBD(PreferenciaVO objParPreferenciaVO)
        {
            try
            {
                objPreferenciaDAO = new PreferenciaDAO();
                return objPreferenciaDAO.AlterarBD(objParPreferenciaVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
