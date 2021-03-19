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

        public TelaPrincipal()
        {
            InitializeComponent();
            
        }

        private void lkLblSobre_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Academia Do Programador 2021\nDesenvolvido por: Gustavo Cesar Mariano", "Sobre"); //Link label sobre - "mensagem","titulo da janela"
        }

        private void btnChamadosAdd_Click(object sender, EventArgs e)
        {
            Chamados chamados = new Chamados();
            chamados.titulo = txtChamadosAddTitulo.Text;
            chamados.descricao = txtChamadoAddDesc.Text;
            chamados.equipamento = cBoxChamadosAddEquip.Text;
            chamados.data = Convert.ToDateTime(maskTxtChamadosAddData.Text);


            chamados.AddChamado(chamados.titulo, chamados.descricao, chamados.equipamento, chamados.data);

            txtChamadosAddTitulo.Text = "";
            txtChamadoAddDesc.Text = "";
            cBoxChamadosAddEquip.Text = "";
            maskTxtChamadosAddData.Text = "";
        }

        private void btnChamadosBuscar_Click(object sender, EventArgs e)
        {
            Chamados chamados = new Chamados();
            chamados.id = Convert.ToInt32(txtChamadosBuscarId.Text);

            chamados.BuscaChamado(chamados.id);

            lblChamadoMostraTitulo.Text = "Titulo: " + chamados.titulo;
            lblChamadoMostraEquip.Text = "Equipamento: " + chamados.equipamento;
            lblChamadoMostraDataAbertura.Text = "Data de abertura: " + chamados.data.ToString("dd/MM/yyyy");
            lblChamadoMostraDias.Text = "Dias aberto: " + (DateTime.Now - chamados.data).ToString("dd");
        }

        private void btnChamadosEditarBuscar_Click(object sender, EventArgs e)
        {
            Chamados chamados = new Chamados();
            chamados.id = Convert.ToInt32(txtChamadosEditarBuscarId.Text);
            idEditar = chamados.id;

            chamados.BuscaChamado(chamados.id);

            txtChamadoEditarTitulo.Text = chamados.titulo;
            txtChamadoEditarDesc.Text = chamados.descricao;
            maskTxtChamadoEditarData.Text = chamados.data.ToString("dd/MM/yyy");
            cBoxChamadoEditarEquip.Text = chamados.equipamento;
            
        }

        private void btnChamadoDeletar_Click(object sender, EventArgs e)
        {
            Chamados chamados = new Chamados();
            chamados.id = Convert.ToInt32(txtChamadoDeletarId.Text);

            if (DialogResult.Yes == MessageBox.Show("Tem certeza que deseja APAGAR o chamado ID: "+chamados.id + " ?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                //Excluir
                chamados.DeletaChamado(chamados.id);

                //Confirmando exclusão para o usuário
                MessageBox.Show("Registro apagado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnEditarAtualizar_Click(object sender, EventArgs e)
        {
            Chamados chamados = new Chamados();
            chamados.id = idEditar;
            chamados.titulo = txtChamadoEditarTitulo.Text;
            chamados.descricao = txtChamadoEditarDesc.Text;
            chamados.equipamento = cBoxChamadoEditarEquip.Text;
            chamados.data = Convert.ToDateTime(maskTxtChamadoEditarData.Text);

            chamados.EditaChamado(chamados.titulo, chamados.descricao, chamados.equipamento, chamados.data, idEditar);
        }
    }
}
