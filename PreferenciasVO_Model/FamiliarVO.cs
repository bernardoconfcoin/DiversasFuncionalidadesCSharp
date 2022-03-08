using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreferenciasVO_Model
{
    public class FamiliarVO
    {
        private int cod;
        private string nome;
        private string sexo;
        private int idade;
        private double ganho_Total_Mensal;
        private double gasto_Total_Mensal;
        private string observacao;

        public FamiliarVO()
        {
        }

        public FamiliarVO(
                        int intCod,
                        string strNome,
                        string strSexo = null,
                        int ? intIdade = null,
                        double ? dblGanho_Total_Mensal = null,
                        double ? dblGasto_Total_Mensal = null,
                        string strObservacao = null
                        )
        {
            setCod(intCod);
            setNome(strNome);
            setSexo(strSexo);
            setIdade(Convert.ToInt32(intIdade == null ? 0 : intIdade));
            setGanho_Total_Mensal(Convert.ToDouble(dblGanho_Total_Mensal == null ? 0 : dblGanho_Total_Mensal));
            setGasto_Total_Mensal(Convert.ToDouble(dblGasto_Total_Mensal == null ? 0 : dblGasto_Total_Mensal));
            setObservacao(strObservacao);
        }

        public int getCod()
        {
            return cod;
        }

        public string getNome()
        {
            return nome;
        }

        public string getSexo()
        {
            return sexo;
        }

        public int getIdade()
        {
            return idade;
        }

        public double getGanho_Total_Mensal()
        {
            return ganho_Total_Mensal;
        }

        public double getGasto_Total_Mensal()
        {
            return gasto_Total_Mensal;
        }

        public string getObservacao()
        {
            return observacao;
        }

        public void setCod(int intCod)
        {
            cod = intCod;
        }

        public void setNome(string strNome)
        {
            nome = strNome;
        }

        public void setSexo(string strSexo)
        {
            if (strSexo.ToUpper() == "MASCULINO" || strSexo.ToUpper() == "FEMININO" || strSexo.ToUpper() == "INDETERMINADO")
            {
                sexo = strSexo.ToUpper();
            }
            else
            {
                throw new Exception("Erro na atribuição do sexo. Os valores possíveis são MASCULINO, FEMININO ou  INDETERMINADO");
            }
        }

        public void setIdade(int  intIdade)
        {
            idade = intIdade;
        }

        public void setGanho_Total_Mensal(double dblGanho_Total_Mensal)
        {
            ganho_Total_Mensal = dblGanho_Total_Mensal;
        }

        public void setGasto_Total_Mensal(double dblGasto_Total_Mensal)
        {
            gasto_Total_Mensal = dblGasto_Total_Mensal;
        }

        public void setObservacao(string strObservacao)
        {
            observacao = strObservacao;
        }

        public int Cod
        {
            get{return cod; }
            set{this.cod = value;}
        }

        public string Nome
        {
            get{return nome; }
            set{this.nome = value;}
        }

        public string Sexo
        {
            get{return sexo; }
            set
            {
                if (value.ToUpper() == "MASCULINO" || value.ToUpper() == "FEMININO" || value.ToUpper() == "INDETERMINADO")
                {
                    sexo = value.ToUpper();
                }
                else
                {
                    throw new Exception("Erro na atribuição do sexo. Os valores possíveis são MASCULINO, FEMININO ou  INDETERMINADO");
                }
            }
        }

        public int Idade
        {
            get{return idade; }
            set{this.idade = value;}
        }

        public double Ganho_Total_Mensal
        {
            get{return ganho_Total_Mensal; }
            set{this.ganho_Total_Mensal = value;}
        }

        public double Gasto_Total_Mensal
        {
            get{return gasto_Total_Mensal; }
            set{this.gasto_Total_Mensal = value;}
        }

        public string Observacao
        {
            get { return observacao; }
            set { this.observacao = value; }
        }

        public List<FamiliarVO> FamiliarVOCollection = new List<FamiliarVO>();
    }
}
