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
        public TelaPrincipal()
        {
            InitializeComponent();
        }

        private void lkLblSobre_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Academia Do Programador 2021\nDesenvolvido por: Gustavo Cesar Mariano", "Sobre"); //Link label sobre - "mensagem","titulo da janela"
        }
    }
}
