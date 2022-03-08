using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PreferenciasDAO_DAL
{
    public abstract class DAO_DAL : DB_DAO
    {
        public abstract DataTable ConsultarBD(Object objVO_VO);

        public abstract void ConsultarBD(ref Object objVO_VO);

        public abstract bool InserirBD(Object objVo_VO);

        public abstract bool ExcluirBD(Object objVO_VO);

        public abstract bool AlterarBD(Object objVO_VO);

    }
}
