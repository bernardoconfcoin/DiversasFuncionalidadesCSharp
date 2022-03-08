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
    public class PreferenciasDeFamiliar
    {
        PreferenciasDeFamiliarFD objPreferenciasDeFamiliarFD;

        public DataTable ConsultarBD(PreferenciasDeFamiliarVO objParPreferenciasDeFamiliarVO)
        {
            try
            {
                objPreferenciasDeFamiliarFD = new PreferenciasDeFamiliarFD();
                return objPreferenciasDeFamiliarFD.ConsultarBD(objParPreferenciasDeFamiliarVO);
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
                objPreferenciasDeFamiliarFD = new PreferenciasDeFamiliarFD();
                return objPreferenciasDeFamiliarFD.InserirBD(objParPreferenciasDeFamiliarVO);
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
                objPreferenciasDeFamiliarFD = new PreferenciasDeFamiliarFD();
                return objPreferenciasDeFamiliarFD.ExcluirBD(objParPreferenciasDeFamiliarVO);
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
                objPreferenciasDeFamiliarFD = new PreferenciasDeFamiliarFD();
                return objPreferenciasDeFamiliarFD.AlterarBD(objParPreferenciasDeFamiliarVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
