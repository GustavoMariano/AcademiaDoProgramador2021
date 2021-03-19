using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcademiaDoProgramador2021
{
    public partial class TelaPrincipal : Form
    {
        public int idEditar;
        public string snEditar;

        public TelaPrincipal()
        {
            InitializeComponent();
            
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
            //Cria e carrega chamados
            Chamados chamados = new Chamados();
            chamados.titulo = txtChamadosAddTitulo.Text;
            chamados.descricao = txtChamadoAddDesc.Text;
            chamados.equipamento = cBoxChamadosAddEquip.Text;
            chamados.data = Convert.ToDateTime(maskTxtChamadosAddData.Text);

            //Executa o metodo AddChamados utilizando chamados
            chamados.AddChamado(chamados.titulo, chamados.descricao, chamados.equipamento, chamados.data);

            //Limpa campos
            txtChamadosAddTitulo.Text = "";
            txtChamadoAddDesc.Text = "";
            cBoxChamadosAddEquip.Text = "";
            maskTxtChamadosAddData.Text = "";
        }

        private void btnChamadosBuscar_Click(object sender, EventArgs e) //Botão para buscar um chamado 
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

        private void btnChamadosEditarBuscar_Click(object sender, EventArgs e)//Botao para buscar um chamado pelo ID
        {
            Chamados chamados = new Chamados();
            chamados.id = Convert.ToInt32(txtChamadosEditarBuscarId.Text);
            idEditar = chamados.id;//Armazena o ID buscado, para realizar o "Where" na query de update

            chamados.BuscaChamado(chamados.id);

            //Carregar text box e masked text box do group box Editar
            txtChamadoEditarTitulo.Text = chamados.titulo;
            txtChamadoEditarDesc.Text = chamados.descricao;
            maskTxtChamadoEditarData.Text = chamados.data.ToString("dd/MM/yyy");
            cBoxChamadoEditarEquip.Text = chamados.equipamento;
            
        }

        private void btnChamadoDeletar_Click(object sender, EventArgs e)//Botao para deletar um chamado
        {
            Chamados chamados = new Chamados();
            chamados.id = Convert.ToInt32(txtChamadoDeletarId.Text);

            //MessageBox de confirmação
            if (DialogResult.Yes == MessageBox.Show("Tem certeza que deseja APAGAR o chamado ID: "+chamados.id + " ?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                //Método deletar
                chamados.DeletaChamado(chamados.id);

                //Confirmando exclusão
                MessageBox.Show("Registro apagado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnEditarAtualizar_Click(object sender, EventArgs e)//Realiza o Update
        {
            Chamados chamados = new Chamados();
            chamados.id = idEditar;//Carrega o ID passado no metodo para ser utilizado na query (WHERE)
            chamados.titulo = txtChamadoEditarTitulo.Text;
            chamados.descricao = txtChamadoEditarDesc.Text;
            chamados.equipamento = cBoxChamadoEditarEquip.Text;
            chamados.data = Convert.ToDateTime(maskTxtChamadoEditarData.Text);

            chamados.EditaChamado(chamados.titulo, chamados.descricao, chamados.equipamento, chamados.data, idEditar);
        }
        #endregion

        //EQUIPAMENTOS
        

        private void btnEquipAdd_Click(object sender, EventArgs e)//Botao de adicionar equipamentos
        {
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

        private void btnBuscarSn_Click(object sender, EventArgs e)
        {
            //Cria e carrega chamados
            Equipamentos equipamentos = new Equipamentos();
            equipamentos.sn = txtEquipBuscarSn.Text;

            //Executa o metodo BuscaChamado utilizando chamados
            equipamentos.BuscaEquipamento(equipamentos.sn);

            //Carregar campos
            lblEquipMostraNome.Text = "Nome: " + equipamentos.nome;
            lblEquipMostraFabricante.Text = "Fabricante: " + equipamentos.fabricante;
            lblEquipMostraSn.Text = "Número de série: " + equipamentos.sn;
        }

        private void btnEquipEditarBuscar_Click(object sender, EventArgs e)
        {
            //Cria e carrega chamados
            Equipamentos equipamentos = new Equipamentos();
            equipamentos.sn = txtEquipEditarBuscarSn.Text;
            snEditar = txtEquipEditarBuscarSn.Text;

            //Executa o metodo BuscaChamado utilizando chamados
            equipamentos.BuscaEquipamento(equipamentos.sn);

            txtEquipEditarNome.Text = equipamentos.nome;
            txtEquipEditarFabricante.Text = equipamentos.fabricante;
            txtEquipEditarPreco.Text = Convert.ToString(equipamentos.preco);
            txtEquipEditarSn.Text = equipamentos.sn;
            maskTxtEquipEditarData.Text = equipamentos.data.ToString("dd/MM/yyy");

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Equipamentos equipamentos = new Equipamentos();
            equipamentos.nome = txtEquipEditarNome.Text;
            equipamentos.preco = Convert.ToDecimal(txtEquipEditarPreco.Text);
            equipamentos.sn = txtEquipEditarSn.Text;
            equipamentos.data = Convert.ToDateTime(maskTxtEquipEditarData.Text);
            equipamentos.fabricante = txtEquipEditarFabricante.Text;

            equipamentos.EditaEquipamento(equipamentos.nome, equipamentos.preco, equipamentos.sn, equipamentos.data, equipamentos.fabricante, snEditar);
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
        }
    }
}
