using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDoProgramador2021
{
    class Chamados
    {
        public string titulo { get; set; }
        public string descricao { get; set; }
        public string equipamento { get; set; }
        public string data { get; set; }

        Db db = new Db();
        public SqlCommand cmd = new SqlCommand();
        public String mensagem = "";
    }
}
