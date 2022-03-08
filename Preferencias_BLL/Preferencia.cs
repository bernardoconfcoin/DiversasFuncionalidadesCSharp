using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using PreferenciasFachada_Facade;
using PreferenciasVO_Model;

namespace Preferencias_BLL
{
    public class Preferencia
    {
        StreamReader objLeitor;
        string strLinhaLida;

        PreferenciaFD objPreferenciaFD;

        public List<string> ImpTxtWhile()
        {
            try
            {
                List<string> resultado = new List<string>();

                objLeitor = new StreamReader(@"D:\Curso Programa\Preferencias.txt");
                strLinhaLida = objLeitor.ReadLine();

                while (strLinhaLida != null)
                {
                    resultado.Add(strLinhaLida);
                    strLinhaLida = objLeitor.ReadLine();
                }
                return resultado;

            }
            catch (Exception ex)
            {

                throw new Exception("Problemas na importação do texto ==> " + ex.Message);
            }
            finally
            {
                objLeitor.Close();
            }
        }

        public List<string> ImpBDCon()
        {
            try
            {
                objPreferenciaFD = new PreferenciaFD();
                return objPreferenciaFD.ImpBDCon();
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
                objPreferenciaFD = new PreferenciaFD();
                return objPreferenciaFD.ImpBDDesc();
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
        //        objPreferenciaFD = new PreferenciaFD();
        //        return objPreferenciaFD.ConsultarBD(objParPreferenciaVO);
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
                objPreferenciaFD = new PreferenciaFD();
                return objPreferenciaFD.ConsultarBD(objParPreferenciaVO);
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
                objPreferenciaFD = new PreferenciaFD();
                return objPreferenciaFD.InserirBD(objParPreferenciaVO);
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
                objPreferenciaFD = new PreferenciaFD();
                return objPreferenciaFD.ExcluirBD(objParPreferenciaVO);
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
                objPreferenciaFD = new PreferenciaFD();
                return objPreferenciaFD.AlterarBD(objParPreferenciaVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
