using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcademiaDoProgramador2021
{
    class Equipamentos
    {
        public int id { get; set; }
        public string nome { get; set; }
        public decimal preco { get; set; }
        public string sn { get; set; }
        public DateTime data { get; set; }
        public string fabricante { get; set; }

        Db db = new Db();
        public SqlCommand cmd = new SqlCommand();
        public String mensagem = "";

        public void AddEquipamento(String nome, Decimal preco, String sn, DateTime data, String fabricante)
        {
            cmd.CommandText = "insert into equipamentos (Nome, Preco, Sn, Data, Fabricante) VALUES (@Nome, @Preco, @Sn, @Data, @Fabricante)";

            cmd.Parameters.AddWithValue("@Nome", nome);
            cmd.Parameters.AddWithValue("@Preco", preco);
            cmd.Parameters.AddWithValue("@Sn", sn);
            cmd.Parameters.AddWithValue("@Data", data);
            cmd.Parameters.AddWithValue("@Fabricante", fabricante);

            try
            {
                cmd.Connection = db.conectar(); //Conectando com o banco

                cmd.ExecuteNonQuery(); //Executa o comando

                db.desconectar(); //Desconecta do banco

                MessageBox.Show("Cadastrado com sucesso"); //Mensagem de sucesso
            }
            catch (SqlException e)
            {
                this.mensagem = "Erro ao se conectar com o banco de dados" + e;
                throw;
            }
        }

        public void EditaEquipamento(String nome, Decimal preco, String sn, DateTime data, string fabricante, string snAntigo)
        {
            cmd.CommandText = "update equipamentos SET Nome = @Nome, Preco = @Preco, Sn = @Sn, Data = @Data, Fabricante = @Fabricante Where Sn = @SnAntigo";

            cmd.Parameters.AddWithValue("@Nome", nome);
            cmd.Parameters.AddWithValue("@Preco", preco);
            cmd.Parameters.AddWithValue("@Sn", sn);
            cmd.Parameters.AddWithValue("@Data", data);
            cmd.Parameters.AddWithValue("@Fabricante", fabricante);
            cmd.Parameters.AddWithValue("@SnAntigo", snAntigo);

            try
            {
                cmd.Connection = db.conectar(); //Conectando com o banco

                cmd.ExecuteNonQuery(); //Executa o comando

                db.desconectar(); //Desconecta do banco

                MessageBox.Show("Editado com sucesso!!"); //Mensagem de sucesso
            }
            catch (SqlException e)
            {
                this.mensagem = "Erro ao se conectar com o banco de dados" + e;
                throw;
            }

        }

        public Equipamentos BuscaEquipamento(String sn)
        {
            {
                cmd.CommandText = @"SELECT * FROM equipamentos WHERE sn=@Sn";

                cmd.Parameters.AddWithValue("@Sn", sn);

                try
                {
                    cmd.Connection = db.conectar(); //Conectando com o banco                               

                    SqlDataReader dbResult = cmd.ExecuteReader();   //Executa o comando    

                    return ResultadoProcuraEquipamento(dbResult);
                }
                catch (SqlException e)
                {
                    this.mensagem = "Erro ao se conectar com o banco de dados" + e;
                    throw;
                }
            }
        }


        public Equipamentos ResultadoProcuraEquipamento(SqlDataReader dataReader)
        {
            Equipamentos equipamentos = new Equipamentos();
            int index = 0;

            if (dataReader.Read())
            {
                equipamentos.id = dataReader.GetInt32(index++);
                equipamentos.nome = dataReader.GetString(index++);
                equipamentos.preco = dataReader.GetDecimal(index++);
                equipamentos.sn = dataReader.GetString(index++);
                equipamentos.data = dataReader.GetDateTime(index++);
                equipamentos.fabricante = dataReader.GetString(index++);
            }
            this.nome = equipamentos.nome;
            this.preco = equipamentos.preco;
            this.sn = equipamentos.sn;
            this.data = equipamentos.data;
            this.fabricante = equipamentos.fabricante;

            return equipamentos;
        }

        public void DeletaEquipamento(string sn)
        {

            cmd.CommandText = "delete from equipamentos Where Sn = @Sn";

            cmd.Parameters.AddWithValue("@Sn", sn);

            try
            {
                cmd.Connection = db.conectar(); //Conectando com o banco

                cmd.ExecuteNonQuery(); //Executa o comando

                db.desconectar(); //Desconecta do banco

                this.mensagem = "Deletado com sucesso!!"; //Mensagem de sucesso
            }
            catch (SqlException e)
            {
                this.mensagem = "Erro ao se conectar com o banco de dados" + e;
                throw;
            }
        }
    }
}
