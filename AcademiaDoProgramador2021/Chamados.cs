using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcademiaDoProgramador2021
{
    class Chamados
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public string equipamento { get; set; }
        public DateTime data { get; set; }

        Db db = new Db();
        public SqlCommand cmd = new SqlCommand();
        public String mensagem = "";

        public void AddChamado(String titulo, String descricao, String equipamento, DateTime data)
        {
            cmd.CommandText = "insert into chamados (Titulo, Descricao, Equipamento, Data) VALUES (@Titulo, @Descricao, @Equipamento, @Data); SELECT SCOPE_IDENTITY()";

            cmd.Parameters.AddWithValue("@Titulo", titulo);
            cmd.Parameters.AddWithValue("@Descricao", descricao);
            cmd.Parameters.AddWithValue("@Equipamento", equipamento);
            cmd.Parameters.AddWithValue("@Data", data);

            try
            {
                cmd.Connection = db.conectar(); //Conectando com o banco

                int ultimoId = Convert.ToInt32(cmd.ExecuteScalar()); //Executa o comando

                db.desconectar(); //Desconecta do banco

                MessageBox.Show("Cadastrado com sucesso, ID:" + ultimoId); //Mensagem de sucesso
            }
            catch (SqlException e)
            {
                this.mensagem = "Erro ao se conectar com o banco de dados" + e;
                throw;
            }
        }       
        

        public void EditaChamado(String titulo, String descricao, String equipamento, DateTime data, int id)
        {
            cmd.CommandText = "update Chamados SET Titulo = @Titulo, Descricao = @Descricao, Equipamento = @Equipamento, Data = @Data Where Id = @Id";

            cmd.Parameters.AddWithValue("@Titulo", titulo);
            cmd.Parameters.AddWithValue("@Descricao", descricao);
            cmd.Parameters.AddWithValue("@Equipamento", equipamento);
            cmd.Parameters.AddWithValue("@Data", data);
            cmd.Parameters.AddWithValue("@Id", id);

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

        public Chamados BuscaChamado(int id)
        {
            {
                cmd.CommandText = @"SELECT * FROM Chamados WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", id);

                try
                {
                    cmd.Connection = db.conectar(); //Conectando com o banco                               

                    SqlDataReader dbResult = cmd.ExecuteReader();   //Executa o comando    

                    return ResultadoProcuraChamado(dbResult);
                }
                catch (SqlException e)
                {
                    this.mensagem = "Erro ao se conectar com o banco de dados" + e;
                    throw;
                }
            }
        }


        public Chamados ResultadoProcuraChamado(SqlDataReader dataReader)
        {
            Chamados chamado = new Chamados();
            int index = 0;

            if (dataReader.Read())
            {
                chamado.id = dataReader.GetInt32(index++);
                chamado.titulo = dataReader.GetString(index++);
                chamado.descricao = dataReader.GetString(index++);
                chamado.equipamento = dataReader.GetString(index++);
                chamado.data = dataReader.GetDateTime(index++);
            }
            this.titulo = chamado.titulo;
            this.descricao = chamado.descricao;
            this.equipamento = chamado.equipamento;
            this.data = chamado.data;

            return chamado;
        }

        public void DeletaChamado(int id)
        {

            cmd.CommandText = "delete from Chamados Where Id = @Id";

            cmd.Parameters.AddWithValue("@Id", id);

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

    public class Nomes
    {
        public string nome { get; set; }

        Db db = new Db();
        public SqlCommand cmd = new SqlCommand();
        public String mensagem = "";


        public List<Nomes> ListarNomes()
        {

            // select que vai ao banco e retorna a consulta já ordenada
            string qry = "select nome from equipamentos";
            //mandar instrucoes ao sql  (Command)
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db.conectar();
            cmd.CommandText = qry;
            SqlDataReader dr = cmd.ExecuteReader();
            List<Nomes> nomesCBox = new List<Nomes>();
            // quando acabar as linhas que retornou da consulta, sai do While
            while (dr.Read())
            {
                Nomes nom = new Nomes();
                nom.nome = dr.GetString(dr.GetOrdinal("nome"));
                nomesCBox.Add(nom);
            }

            cmd.Connection.Close();
            cmd.Dispose();

            return nomesCBox;

        }

    }
    
}
