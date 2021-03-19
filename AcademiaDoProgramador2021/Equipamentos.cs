using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDoProgramador2021
{
    class Equipamentos
    {
        public string nome { get; set; }
        public float preco { get; set; }
        public string sn { get; set; }
        public string data { get; set; }
        public string fabricante { get; set; }

        Db db = new Db();
        public SqlCommand cmd = new SqlCommand();
        public String mensagem = "";
    }
}
