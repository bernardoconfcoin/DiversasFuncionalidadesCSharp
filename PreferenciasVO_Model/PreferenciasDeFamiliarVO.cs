using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreferenciasVO_Model
{
    public class PreferenciasDeFamiliarVO
    {
        private FamiliarVO objFamiliarVO;
        private PreferenciaVO objPreferenciaVO;
        private float intensidade;
        private string observacao;

        public PreferenciasDeFamiliarVO()
        {
        }

        public PreferenciasDeFamiliarVO(
                                        FamiliarVO objParFamiliarVO, 
                                        PreferenciaVO objParPreferenciaVO, 
                                        float fltIntensidade, 
                                        string strObservacao)
        {
            setObjFamiliarVO(objParFamiliarVO);
            ObjPreferenciaVO = objParPreferenciaVO;
            setIntensidade(fltIntensidade);
            Observacao = strObservacao;
        }

        public FamiliarVO getObjFamiliarVO()
        {
            return objFamiliarVO;
        }
        public PreferenciaVO getObjPreferenciaVO()
        {
            return objPreferenciaVO;
        }
        public float getIntensidade()
        {
            return intensidade;
        }
        public string getObservacao()
        {
            return observacao;
        }

        public void setObjFamiliarVO(FamiliarVO objParFamiliarVO)
        {
            objFamiliarVO = objParFamiliarVO; ;
        }
        public void setObjPreferenciaVO(PreferenciaVO objParPreferenciaVO)
        {
            objPreferenciaVO = objParPreferenciaVO; ;
        }
        public void setIntensidade(float fltIntensidade)
        {
            intensidade = fltIntensidade; ;
        }
        public void setObservacao(string strObservacao)
        {
            observacao = strObservacao;
        }

        public FamiliarVO ObjFamiliarVO
        {
            get { return objFamiliarVO; }
            set { this.objFamiliarVO = value; }
        }
        public PreferenciaVO ObjPreferenciaVO
        {
            get { return objPreferenciaVO; }
            set { this.objPreferenciaVO = value; }
        }
        public float Intensidade
        {
            get { return intensidade; }
            set { this.intensidade = value; }
        }
        public string Observacao
        {
            get { return observacao; }
            set { this.observacao = value; }
        }
        public List<PreferenciasDeFamiliarVO> PreferenciaDeFamiliarVOCollection = new List<PreferenciasDeFamiliarVO>();
    }
}
