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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _9567A_V00___PI.Usuarios
{
    /// <summary>
    /// Interação lógica para controleUsuario.xam
    /// </summary>
    public partial class controleUsuario : UserControl
    {
        Utilidades.messageBox inputDialog;

        adicionarUsuario addUser = new adicionarUsuario();
        removerUsuario removeUser = new removerUsuario();
        editarUsuario editUser = new editarUsuario();
        historicoUsuarios historicoUser = new historicoUsuarios();

        public controleUsuario()
        {
            InitializeComponent();
        }

        private void btCriarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (Utilidades.VariaveisGlobais.NumberOfGroup_GS == 3)
            {
                if (spControleUsuario != null)
                {
                    spControleUsuario.Children.Clear();

                    spControleUsuario.Children.Add(addUser);
                }
                else
                {
                    spControleUsuario.Children.Add(addUser);
                }
            }
            else
            {
                inputDialog = new Utilidades.messageBox(Utilidades.VariaveisGlobais.faltaPermissaoTitle, Utilidades.VariaveisGlobais.faltaPermissaoMessage, MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");

                inputDialog.ShowDialog();
            }
        }

        private void btApagarUsuario_Click(object sender, RoutedEventArgs e)
        {

            if (Utilidades.VariaveisGlobais.NumberOfGroup_GS == 3)
            {
                if (spControleUsuario != null)
                {
                    spControleUsuario.Children.Clear();

                    spControleUsuario.Children.Add(removeUser);
                }
                else
                {
                    spControleUsuario.Children.Add(removeUser);
                }
            }
            else
            {
                inputDialog = new Utilidades.messageBox(Utilidades.VariaveisGlobais.faltaPermissaoTitle, Utilidades.VariaveisGlobais.faltaPermissaoMessage, MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");

                inputDialog.ShowDialog();
            }

        }

        private void brEditarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (spControleUsuario != null)
            {
                spControleUsuario.Children.Clear();

                spControleUsuario.Children.Add(editUser);
            }
            else
            {
                spControleUsuario.Children.Add(editUser);
            }
        }

        private void btHistoricoUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (spControleUsuario != null)
            {
                spControleUsuario.Children.Clear();

                spControleUsuario.Children.Add(historicoUser);
            }
            else
            {
                spControleUsuario.Children.Add(historicoUser);
            }
        }


    }
}
