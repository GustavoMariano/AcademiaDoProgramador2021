using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDoProgramador2021
{
    public class Db
    {
        SqlConnection con = new SqlConnection();
        public SqlDataReader reader;

        public Db()
        {
            con.ConnectionString = @"Data Source=DESKTOP-G99C339;Initial Catalog=DB.Crud;Integrated Security=True";
        }

        public SqlConnection conectar()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }

            return con;
        }

        public void desconectar()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
