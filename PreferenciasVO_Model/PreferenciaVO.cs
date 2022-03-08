using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreferenciasVO_Model
{
    public class PreferenciaVO
    {
        private int iD;
        private string descricao;

        public PreferenciaVO()
        {
        }

        public PreferenciaVO(int intID, string strDescricao)
        {
            //setter microsoft
            ID = intID;
            
            //setter classico
            setDescricao(strDescricao);
        }

        public int getID()
        {
            return this.iD;
        }

        public string getDescricao()
        {
            return descricao;
        }

        public void setID(int intID)
        {
            iD = intID;
        }

        public void setDescricao(string strDescricao)
        {
            descricao = strDescricao;
        }

        public int ID
        {
            get { return this.iD; }
            set { this.iD = value; }
        }

        public string Descricao
        {
            get { return descricao; }
            set { this.descricao = value; }
        }

        public List<PreferenciaVO> PreferenciaVOCollection = new List<PreferenciaVO>();
    }
}
