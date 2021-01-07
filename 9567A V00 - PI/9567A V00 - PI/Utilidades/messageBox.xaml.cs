using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _9567A_V00___PI.Utilidades
{
    /// <summary>
    /// Lógica interna para messageBox.xaml
    /// </summary>
    public partial class messageBox : Window
    {

        public event EventHandler Esquerda_Click;
        public event EventHandler Direita_Click;

        public messageBox(string Titulo, string Mensagem, MaterialDesignThemes.Wpf.PackIconKind packIcon, string contentButtonEsquerda, string contentButtonDireita)
        {
            InitializeComponent();

            txtTitle.Text = Titulo;
            txtMessage.Text = Mensagem;
            pckIcon.Kind = packIcon;
            genericButton_Esquerda.Content = contentButtonEsquerda;
            genericButton_Direita.Content = contentButtonDireita;


        }


        private void genericButton_Esquerda_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

            this.Hide();

        }

        private void genericButton_Direita_Click(object sender, RoutedEventArgs e)
        {
            //bubble the event up to the parent
            if (this.Direita_Click != null)
                this.Direita_Click(this, e);
        }
    }
}
