using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interação lógica para editarUsuario.xam
    /// </summary>
    public partial class editarUsuario : UserControl
    {
        private string Valor = "";

        public editarUsuario()
        {
            InitializeComponent();
        }

        private void loadListbox()
        {

            lbUser.Content = "";
            Valor = "";

            if (listbox.Items.Count > 0)
            {
                listbox.Items.Clear();
            }

            System.Data.DataTable datatable = DataBase.SqlFunctionsUsers.GetAllTablesDBCA();

            foreach (DataRow row in datatable.Rows)
            {
                string tablename = row[2].ToString();

                ListBoxItem boxItem = new ListBoxItem();


                if (Utilidades.VariaveisGlobais.NumberOfGroup_GS >= 3)
                {
                    boxItem.Name = tablename;
                    boxItem.Width = 143;
                    boxItem.Background = new SolidColorBrush(Color.FromRgb(60, 60, 60));
                    boxItem.Content = tablename;
                    boxItem.FontSize = 16;
                    boxItem.Foreground = new SolidColorBrush(Colors.White);
                    boxItem.VerticalAlignment = VerticalAlignment.Center;
                    boxItem.HorizontalAlignment = HorizontalAlignment.Center;
                    Thickness margin = boxItem.Margin;
                    margin.Top = 5;
                    boxItem.Margin = margin;
                    boxItem.IsSelected = false;
                    boxItem.IsEnabled = true;
                }
                else
                {
                    if (Utilidades.VariaveisGlobais.UserLogged_GS == tablename)
                    {
                        boxItem.Name = tablename;
                        boxItem.Width = 143;
                        boxItem.Background = new SolidColorBrush(Color.FromRgb(60, 60, 60));
                        boxItem.Content = tablename;
                        boxItem.FontSize = 16;
                        boxItem.Foreground = new SolidColorBrush(Colors.White);
                        boxItem.VerticalAlignment = VerticalAlignment.Center;
                        boxItem.HorizontalAlignment = HorizontalAlignment.Center;
                        Thickness margin = boxItem.Margin;
                        margin.Top = 5;
                        boxItem.Margin = margin;
                        boxItem.IsSelected = true;
                        boxItem.IsEnabled = true;


                    }
                    else
                    {
                        boxItem.Name = tablename;
                        boxItem.Width = 143;
                        boxItem.Background = new SolidColorBrush(Color.FromRgb(60, 60, 60));
                        boxItem.Content = tablename;
                        boxItem.FontSize = 16;
                        boxItem.Foreground = new SolidColorBrush(Colors.White);
                        boxItem.VerticalAlignment = VerticalAlignment.Center;
                        boxItem.HorizontalAlignment = HorizontalAlignment.Center;
                        Thickness margin = boxItem.Margin;
                        margin.Top = 5;
                        boxItem.Margin = margin;
                        boxItem.IsSelected = false;
                        boxItem.IsEnabled = false;

                    }

                }

                if (tablename.Contains("Automasul"))
                {
                    boxItem.IsSelected = false;
                    boxItem.IsEnabled = false;
                }

                listbox.Items.Add(boxItem);
            }

        }

        private void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = listbox.SelectedIndex;

            if (i != -1)
            {
                Valor = listbox.Items[i].ToString();
                int contador = 1;
                for (int j = 0; j <= Valor.Length - 1; j++)
                {
                    if (Valor[j] == ':')
                    {
                        Valor = Valor.Remove(0, contador);

                        Valor = Valor.Replace(" ", "");

                    }
                    contador++;
                }

                txtEmail.Text = "";
                txtSenha.Password = "";
                txtSenha1.Password = "";

                lbUser.Content = Valor;

                System.Data.DataTable datatable = DataBase.SqlFunctionsUsers.GetTableDBCA((string)lbUser.Content);

                string tablename = datatable.Rows[datatable.Rows.Count - 1]["GroupUser"].ToString();


                if (tablename.Equals("Administrador"))
                {
                    pckAdm.Visibility = Visibility.Visible;
                    pckMan.Visibility = Visibility.Hidden;
                    pckOperador.Visibility = Visibility.Hidden;

                    lbAdm.IsSelected = true;
                    lbOperador.IsSelected = false;
                    lbManutencao.IsSelected = false;
                }
                else if (tablename.Equals("Operador"))
                {
                    pckAdm.Visibility = Visibility.Hidden;
                    pckMan.Visibility = Visibility.Hidden;
                    pckOperador.Visibility = Visibility.Visible;

                    lbAdm.IsSelected = false;
                    lbOperador.IsSelected = true;
                    lbManutencao.IsSelected = false;
                }
                else
                {
                    pckAdm.Visibility = Visibility.Hidden;
                    pckMan.Visibility = Visibility.Visible;
                    pckOperador.Visibility = Visibility.Hidden;

                    lbAdm.IsSelected = false;
                    lbOperador.IsSelected = false;
                    lbManutencao.IsSelected = true;
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadListbox();

            if (Utilidades.VariaveisGlobais.NumberOfGroup_GS >= 3)
            {
                lbUser.Content = "";
                listbox.IsEnabled = true;
                listboxGroupUser.IsEnabled = true;

                pckAdm.Visibility = Visibility.Hidden;
                pckMan.Visibility = Visibility.Hidden;
                pckOperador.Visibility = Visibility.Hidden;

            }
            else
            {
                listbox.IsEnabled = false;
                listboxGroupUser.IsEnabled = false;

                lbUser.Content = Utilidades.VariaveisGlobais.UserLogged_GS;


                if (Utilidades.VariaveisGlobais.NumberOfGroup_GS == 1)
                {
                    pckAdm.Visibility = Visibility.Hidden;
                    pckMan.Visibility = Visibility.Hidden;
                    pckOperador.Visibility = Visibility.Visible;

                    lbAdm.IsSelected = false;
                    lbOperador.IsSelected = true;
                    lbManutencao.IsSelected = false;
                }
                else
                {

                    pckAdm.Visibility = Visibility.Hidden;
                    pckMan.Visibility = Visibility.Visible;
                    pckOperador.Visibility = Visibility.Hidden;

                    lbAdm.IsSelected = false;
                    lbOperador.IsSelected = false;
                    lbManutencao.IsSelected = true;

                }

            }
        }

        private void btEditUsuario_Click(object sender, RoutedEventArgs e)
        {
            Utilidades.messageBox inputDialog;
            if (String.IsNullOrEmpty((string)lbUser.Content))
            {
                inputDialog = new Utilidades.messageBox("Sem Usuário", "Por favor selecione um usuário", MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");

                inputDialog.ShowDialog();
            }
            else
            {
                if (String.IsNullOrEmpty(txtSenha1.Password))
                {
                    inputDialog = new Utilidades.messageBox("Campos Vazios", "Por favor verifique se todos os campos estão preenchidos", MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

                    inputDialog.ShowDialog();
                }
                else
                {
                    if (txtSenha.Password.Equals(txtSenha1.Password))
                    {

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

                        DataBase.SqlFunctionsUsers.IntoDateDBCA((string)lbUser.Content, DataBase.SqlFunctionsUsers.MD5Cryptography(txtSenha.Password), groupUser, email, "Modificado");

                        //mensagem que criou corretamente
                        inputDialog = new Utilidades.messageBox("Edição Usuário", "Usuário " + (string)lbUser.Content + " editado com sucesso!", MaterialDesignThemes.Wpf.PackIconKind.UserEdit, "OK", "Fechar");

                        txtEmail.Text = "";
                        txtSenha.Password = "";
                        txtSenha1.Password = "";

                        pckAdm.Visibility = Visibility.Hidden;
                        pckMan.Visibility = Visibility.Hidden;
                        pckOperador.Visibility = Visibility.Hidden;

                        loadListbox();

                        inputDialog.ShowDialog();

                    }
                    else
                    {
                        //mensagem que criou corretamente
                        inputDialog = new Utilidades.messageBox("Senhas não coincidem", "Por favor as senhas não coincidem, digitar novamente o campo senha", MaterialDesignThemes.Wpf.PackIconKind.UserAdd, "OK", "Fechar");

                        inputDialog.ShowDialog();
                    }
                }
            }

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

        private void openKeyboard(object sender, MouseButtonEventArgs e)
        {
            Teclados.keyboard.openKeyboard();
        }
    }
}
