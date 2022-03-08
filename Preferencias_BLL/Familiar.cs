using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PreferenciasVO_Model;
using PreferenciasFachada_Facade;

namespace Preferencias_BLL
{
    public class Familiar
    {
        FamiliarFD objFamiliarFD;


        //public DataTable ConsultarBDFamiliar(FamiliarVO objParFamiliarVO)
        //{
        //    try
        //    {
        //        objFamiliarFD = new FamiliarFD();
        //        return objFamiliarFD.ConsultarBDFamiliar(objParFamiliarVO);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public DataTable ConsultarBDFamiliar(FamiliarVO objParFamiliarVO)
        {
            try
            {
                objFamiliarFD = new FamiliarFD();
                return objFamiliarFD.ConsultarBD(objParFamiliarVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InserirBDFamiliar(FamiliarVO objParFamiliarVO)
        {
            try
            {
                objFamiliarFD = new FamiliarFD();
                return objFamiliarFD.InserirBD(objParFamiliarVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExcluirBDFamiliar(FamiliarVO objParFamiliarVO)
        {
            try
            {
                objFamiliarFD = new FamiliarFD();
                return objFamiliarFD.ExcluirBD(objParFamiliarVO);
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
                objFamiliarFD = new FamiliarFD();
                return objFamiliarFD.AlterarBDFamiliar(objParFamiliarVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
