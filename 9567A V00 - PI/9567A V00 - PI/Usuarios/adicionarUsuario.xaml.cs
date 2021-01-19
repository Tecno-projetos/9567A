using _9567A_V00___PI.Utilidades;
using MaterialDesignThemes.Wpf;
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
    /// Interação lógica para adicionarUsuario.xam
    /// </summary>
    public partial class adicionarUsuario : UserControl
    {
        public adicionarUsuario()
        {
            InitializeComponent();

            lbOperador.IsSelected = true;
            pckAdm.Visibility = Visibility.Hidden;
            pckMan.Visibility = Visibility.Hidden;
            pckOperador.Visibility = Visibility.Visible;

        }

        private void btCriarUsuario_Click(object sender, RoutedEventArgs e)
        {
            Utilidades.messageBox inputDialog;

            if (VariaveisGlobais.DB_Connected_GS)
            {

                if (String.IsNullOrEmpty(txtUser.Text) || String.IsNullOrEmpty(txtSenha.Password) || String.IsNullOrEmpty(txtSenha1.Password))
                {
                    inputDialog = new Utilidades.messageBox("Campos Vazios", "Por favor verifique se todos os campos estão preenchidos", MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

                    inputDialog.ShowDialog();

                }
                else
                {
                    if ((bool)lbAdm.IsSelected || (bool)lbManutencao.IsSelected || (bool)lbOperador.IsSelected)
                    {
                        if (txtSenha.Password.Equals(txtSenha1.Password))
                        {
                            char t = Convert.ToChar(txtUser.Text.Substring(0, 1));

                            if (!char.IsLetter(t))
                            {
                                inputDialog = new Utilidades.messageBox("Letra ao iniciar", "Por favor inicie o nome do usuário com um caracter do alfabeto", MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

                                inputDialog.ShowDialog();

                            }
                            else
                            {
                                if ((DataBase.SqlFunctionsUsers.ExistTableDBCA(txtUser.Text)) == true)
                                {

                                    inputDialog = new Utilidades.messageBox("Conflito de Usuários", "Esse nome de usuário já existe, por favor informe outro nome", MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

                                    inputDialog.ShowDialog();

                                }
                                else
                                {
                                    DataBase.SqlFunctionsUsers.CreateTableDBCA(txtUser.Text);

                                    string groupUser = "";
                                    string email = "";

                                    if ((bool)lbManutencao.IsSelected)
                                    {
                                        groupUser = "Manutenção";
                                    }
                                    else if ((bool)lbOperador.IsSelected)
                                    {
                                        groupUser = "Operador";
                                    }
                                    else if ((bool)lbAdm.IsSelected)
                                    {
                                        groupUser = "Administrador";
                                    }

                                    if (String.IsNullOrEmpty(txtEmail.Text))
                                    {
                                        email = "";
                                    }
                                    else
                                    {
                                        email = txtEmail.Text;
                                    }

                                    DataBase.SqlFunctionsUsers.IntoDateDBCA(txtUser.Text, DataBase.SqlFunctionsUsers.MD5Cryptography(txtSenha.Password), groupUser, email, "Criou");

                                    //mensagem que criou corretamente
                                    inputDialog = new Utilidades.messageBox("Usuário Criado", "Usuário " + txtUser.Text + " cadastrado com sucesso!", MaterialDesignThemes.Wpf.PackIconKind.UserAdd, "OK", "Fechar");

                                    inputDialog.ShowDialog();

                                    limpaCampos();

                                }
                            }
                        }
                        else
                        {
                            //mensagem que criou corretamente
                            inputDialog = new Utilidades.messageBox("Senhas não coincidem", "Por favor as senhas não coincidem, digitar novamente o campo senha", MaterialDesignThemes.Wpf.PackIconKind.UserAdd, "OK", "Fechar");

                            inputDialog.ShowDialog();

                        }
                    }
                    else
                    {

                        inputDialog = new Utilidades.messageBox("Grupo de Usuário", "Por favor selecioane o grupo de usuário", MaterialDesignThemes.Wpf.PackIconKind.UserAdd, "OK", "Fechar");

                        inputDialog.ShowDialog();
                    }
                }
            }
            else
            {
                inputDialog = new Utilidades.messageBox("Sem conexão com Banco de Dados", "Por favor verifique a conexão com o Banco de Dados", MaterialDesignThemes.Wpf.PackIconKind.DatabaseRefresh, "OK", "Fechar");

                inputDialog.ShowDialog();
            }
        }
        private void openKeyboard(object sender, MouseButtonEventArgs e)
        {
            Teclados.keyboard.openKeyboard();
        }

        private void lbAdm_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (lbAdm.IsSelected)
            {
                pckAdm.Visibility = Visibility.Visible;
                pckMan.Visibility = Visibility.Hidden;
                pckOperador.Visibility = Visibility.Hidden;

            }
        }

        private void lbManutencao_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (lbManutencao.IsSelected)
            {
                pckAdm.Visibility = Visibility.Hidden;
                pckMan.Visibility = Visibility.Visible;
                pckOperador.Visibility = Visibility.Hidden;
            }
        }

        private void lbOperador_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (lbOperador.IsSelected)
            {
                pckAdm.Visibility = Visibility.Hidden;
                pckMan.Visibility = Visibility.Hidden;
                pckOperador.Visibility = Visibility.Visible;
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            limpaCampos();

        }

        private void limpaCampos()
        {

            //limpa campos
            txtUser.Text = "";
            txtSenha.Password = "";
            txtSenha1.Password = "";
            txtEmail.Text = "";

            lbOperador.IsSelected = true;
            pckAdm.Visibility = Visibility.Hidden;
            pckMan.Visibility = Visibility.Hidden;
            pckOperador.Visibility = Visibility.Visible;
        }

    }
}
