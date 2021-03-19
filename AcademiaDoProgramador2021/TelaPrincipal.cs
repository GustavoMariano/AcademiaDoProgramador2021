using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcademiaDoProgramador2021
{

    public partial class TelaPrincipal : Form
    {
        Db db = new Db();
        public SqlCommand cmd = new SqlCommand();
        public String mensagem = "";
        public int idEditar;
        public string snEditar;

        public TelaPrincipal()
        {
            InitializeComponent();

            Nomes nomes = new Nomes();
            List<Nomes> nome = nomes.ListarNomes();

            this.cBoxChamadosAddEquip.DataSource = nome;
            this.cBoxChamadosAddEquip.DisplayMember = "nome";

            this.cBoxChamadosEditarEquip.DataSource = nome;
            this.cBoxChamadosEditarEquip.DisplayMember = "nome";
        }

        //Link
        private void lkLblSobre_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Academia Do Programador 2021\nDesenvolvido por: Gustavo Cesar Mariano", "Sobre"); //Link label sobre - "mensagem","titulo da janela"
        }

        //CHAMADOS
        #region Chamados                    

        private void btnChamadosAdd_Click(object sender, EventArgs e)  //Botão para adicionar um chamado
        {
            if (txtChamadosAddTitulo.Text == "")
            {
                MessageBox.Show("O chamado deve possuir um titulo!!", "ERRO");
            }
            else if (txtChamadoAddDesc.Text == "")
            {
                MessageBox.Show("O equipamento deve uma descrição!!", "ERRO");
            }
            else if (!maskTxtChamadosAddData.MaskFull)
            {
                MessageBox.Show("Deve possuir data de abertura!!", "ERRO");
            }
            else if(cBoxChamadosAddEquip.Text == "")
            {
                MessageBox.Show("Deve possuir um equipamento!!", "ERRO");
            }
            else
            {
                //Cria e carrega chamados
                Chamados chamados = new Chamados();
                chamados.titulo = txtChamadosAddTitulo.Text;
                chamados.descricao = txtChamadoAddDesc.Text;
                chamados.equipamento = cBoxChamadosAddEquip.Text;
                chamados.data = Convert.ToDateTime(maskTxtChamadosAddData.Text);

                //Executa o metodo AddChamados utilizando chamados
                chamados.AddChamado(chamados.titulo, chamados.descricao, chamados.equipamento, chamados.data);

                MessageBox.Show("ID: " + chamados.id);

                //Limpa campos
                txtChamadosAddTitulo.Text = "";
                txtChamadoAddDesc.Text = "";
                cBoxChamadosAddEquip.Text = "";
                maskTxtChamadosAddData.Text = "";
            }
        }

        private void btnChamadosBuscar_Click(object sender, EventArgs e) //Botão para buscar um chamado 
        {
            if (txtChamadosBuscarId.Text == "")
            {
                MessageBox.Show("Insira o ID que deseja buscar!!", "ERRO");
            }
            else
            {
                //Cria e carrega chamados
                Chamados chamados = new Chamados();
                chamados.id = Convert.ToInt32(txtChamadosBuscarId.Text);

                //Executa o metodo BuscaChamado utilizando chamados
                chamados.BuscaChamado(chamados.id);

                //Carregar campos
                lblChamadoMostraTitulo.Text = "Titulo: " + chamados.titulo;
                lblChamadoMostraEquip.Text = "Equipamento: " + chamados.equipamento;
                lblChamadoMostraDataAbertura.Text = "Data de abertura: " + chamados.data.ToString("dd/MM/yyyy");
                lblChamadoMostraDias.Text = "Dias aberto: " + (DateTime.Now - chamados.data).ToString("dd");
            }
        }

        private void btnChamadosEditarBuscar_Click(object sender, EventArgs e)//Botao para buscar um chamado pelo ID
        {
            if (txtChamadosEditarBuscarId.Text == "")
            {
                MessageBox.Show("Insira o ID que deseja editar!!", "ERRO");
            }
            else
            {
                Chamados chamados = new Chamados();
                chamados.id = Convert.ToInt32(txtChamadosEditarBuscarId.Text);
                idEditar = chamados.id;//Armazena o ID buscado, para realizar o "Where" na query de update

                chamados.BuscaChamado(chamados.id);

                //Carregar text box e masked text box do group box Editar
                txtChamadoEditarTitulo.Text = chamados.titulo;
                txtChamadoEditarDesc.Text = chamados.descricao;
                maskTxtChamadoEditarData.Text = chamados.data.ToString("dd/MM/yyy");
                cBoxChamadosEditarEquip.Text = chamados.equipamento;
            }
            
        }

        private void btnChamadoDeletar_Click(object sender, EventArgs e)//Botao para deletar um chamado
        {
            if(txtChamadoDeletarId.Text == "")
            {
                MessageBox.Show("Insira o ID que deseja deletar!!", "ERRO");
            }
            else
            {
                Chamados chamados = new Chamados();
                chamados.id = Convert.ToInt32(txtChamadoDeletarId.Text);

                //MessageBox de confirmação
                if (DialogResult.Yes == MessageBox.Show("Tem certeza que deseja APAGAR o chamado ID: " + chamados.id + " ?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    //Método deletar
                    chamados.DeletaChamado(chamados.id);

                    //Confirmando exclusão
                    MessageBox.Show("Registro apagado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void btnEditarAtualizar_Click(object sender, EventArgs e)//Realiza o Update
        {
            if (txtChamadoEditarTitulo.Text == "")
            {
                MessageBox.Show("O chamado deve possuir um titulo!!", "ERRO");
            }
            else if (txtChamadoEditarDesc.Text == "")
            {
                MessageBox.Show("O equipamento deve uma descrição!!", "ERRO");
            }
            else if (!maskTxtChamadoEditarData.MaskFull)
            {
                MessageBox.Show("Deve possuir data de abertura!!", "ERRO");
            }
            else if (cBoxChamadosEditarEquip.Text == "")
            {
                MessageBox.Show("Deve possuir um equipamento!!", "ERRO");
            }
            else
            {
                Chamados chamados = new Chamados();
                chamados.id = idEditar;//Carrega o ID passado no metodo para ser utilizado na query (WHERE)
                chamados.titulo = txtChamadoEditarTitulo.Text;
                chamados.descricao = txtChamadoEditarDesc.Text;
                chamados.equipamento = cBoxChamadosEditarEquip.Text;
                chamados.data = Convert.ToDateTime(maskTxtChamadoEditarData.Text);

                chamados.EditaChamado(chamados.titulo, chamados.descricao, chamados.equipamento, chamados.data, idEditar);
            }
        }

        #endregion

        //EQUIPAMENTOS
        #region Equipamentos


        private void btnEquipAdd_Click(object sender, EventArgs e)//Botao de adicionar equipamentos
        {
            //Validações
            if (txtEquipAddNome.TextLength < 6)
            {
                MessageBox.Show("O nome deve ter no mínimo 6 caracteres!!","ERRO");
            }
            else if (txtEquipAddSn.Text == "")
            {
                MessageBox.Show("O equipamento deve possuir um número de série!!", "ERRO");
            }
            else if(!maskTxtEquipAddData.MaskFull)
            {
                MessageBox.Show("Deve possuir data de aquisição!!", "ERRO");
            }
            else if (txtEquipAddFabricante.Text == "")
            {
                MessageBox.Show("O equipamento deve possuir fabricante!!", "ERRO");
            }
            else { 
            //Cria e carrega equipamentos
            Equipamentos equipamentos = new Equipamentos();
            equipamentos.nome = txtEquipAddNome.Text;
            equipamentos.preco = Convert.ToDecimal(maskTxtEquipAddPreco.Text);
            equipamentos.sn = txtEquipAddSn.Text;
            equipamentos.data = Convert.ToDateTime(maskTxtEquipAddData.Text);
            equipamentos.fabricante = txtEquipAddFabricante.Text;

            //Executa método AddEquipamento
            equipamentos.AddEquipamento(equipamentos.nome, equipamentos.preco, equipamentos.sn, equipamentos.data, equipamentos.fabricante);

            //Limpa campos
            txtEquipAddNome.Text = "";
            maskTxtEquipAddPreco.Text = "";
            txtEquipAddSn.Text = "";
            maskTxtEquipAddData.Text = "";
            txtEquipAddFabricante.Text = "";
            }
            cBoxChamadosAddEquip.Update();


            //Atualiza combo box
            Nomes nomes = new Nomes();
            List<Nomes> nome = nomes.ListarNomes();

            this.cBoxChamadosAddEquip.DataSource = nome;
            this.cBoxChamadosAddEquip.DisplayMember = "nome";

            this.cBoxChamadosEditarEquip.DataSource = nome;
            this.cBoxChamadosEditarEquip.DisplayMember = "nome";
        }

        private void btnBuscarSn_Click(object sender, EventArgs e)
        {
            if (txtEquipBuscarSn.Text == "")
            {
                MessageBox.Show("Insira o número de série para procurar!!", "ERRO");
            }
            else
            {
                //Cria e carrega equipamentos
                Equipamentos equipamentos = new Equipamentos();
            equipamentos.sn = txtEquipBuscarSn.Text;

            //Executa o metodo BuscaEquipamento utilizando chamados
            equipamentos.BuscaEquipamento(equipamentos.sn);

            //Carregar campos
            lblEquipMostraNome.Text = "Nome: " + equipamentos.nome;
            lblEquipMostraFabricante.Text = "Fabricante: " + equipamentos.fabricante;
            lblEquipMostraSn.Text = "Número de série: " + equipamentos.sn;
                }
        }

        private void btnEquipEditarBuscar_Click(object sender, EventArgs e)
        {
            if(txtEquipEditarBuscarSn.Text == "")
            {
                MessageBox.Show("Insira o número de série para procurar!!", "ERRO");
            }            
            else
            {
                //Cria e carrega equipamentos
                Equipamentos equipamentos = new Equipamentos();
                equipamentos.sn = txtEquipEditarBuscarSn.Text;
                snEditar = txtEquipEditarBuscarSn.Text;

                //Executa o metodo BuscaEquipamento utilizando equipamentos
                equipamentos.BuscaEquipamento(equipamentos.sn);

                txtEquipEditarNome.Text = equipamentos.nome;
                txtEquipEditarFabricante.Text = equipamentos.fabricante;
                txtEquipEditarPreco.Text = Convert.ToString(equipamentos.preco);
                txtEquipEditarSn.Text = equipamentos.sn;
                maskTxtEquipEditarData.Text = equipamentos.data.ToString("dd/MM/yyy");
            }

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtEquipEditarNome.TextLength < 6)
            {
                MessageBox.Show("O nome deve ter no mínimo 6 caracteres!!", "ERRO");
            }
            else if (txtEquipEditarSn.Text == "")
            {
                MessageBox.Show("O equipamento deve possuir um número de série!!", "ERRO");
            }
            else if (!maskTxtEquipEditarData.MaskFull)
            {
                MessageBox.Show("Deve possuir data de aquisição!!", "ERRO");
            }
            else if (txtEquipEditarFabricante.Text == "")
            {
                MessageBox.Show("O equipamento deve possuir fabricante!!", "ERRO");
            }
            else
            {
                Equipamentos equipamentos = new Equipamentos();
                equipamentos.nome = txtEquipEditarNome.Text;
                equipamentos.preco = Convert.ToDecimal(txtEquipEditarPreco.Text);
                equipamentos.sn = txtEquipEditarSn.Text;
                equipamentos.data = Convert.ToDateTime(maskTxtEquipEditarData.Text);
                equipamentos.fabricante = txtEquipEditarFabricante.Text;

                equipamentos.EditaEquipamento(equipamentos.nome, equipamentos.preco, equipamentos.sn, equipamentos.data, equipamentos.fabricante, snEditar);
            }
            //Atualiza combo box
            Nomes nomes = new Nomes();
            List<Nomes> nome = nomes.ListarNomes();

            this.cBoxChamadosAddEquip.DataSource = nome;
            this.cBoxChamadosAddEquip.DisplayMember = "nome";

            this.cBoxChamadosEditarEquip.DataSource = nome;
            this.cBoxChamadosEditarEquip.DisplayMember = "nome";
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            Equipamentos equipamentos = new Equipamentos();
            equipamentos.sn = txtEquipDeletarSn.Text;

            //MessageBox de confirmação
            if (DialogResult.Yes == MessageBox.Show("Tem certeza que deseja APAGAR o chamado ID: " + equipamentos.sn + " ?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                //Método deletar
                equipamentos.DeletaEquipamento(equipamentos.sn);

                //Confirmando exclusão
                MessageBox.Show("Registro apagado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            //Atualiza combo box
            Nomes nomes = new Nomes();
            List<Nomes> nome = nomes.ListarNomes();

            this.cBoxChamadosAddEquip.DataSource = nome;
            this.cBoxChamadosAddEquip.DisplayMember = "nome";

            this.cBoxChamadosEditarEquip.DataSource = nome;
            this.cBoxChamadosEditarEquip.DisplayMember = "nome";
        }

        #endregion
    }
}
