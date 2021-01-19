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
    /// Interação lógica para removerUsuario.xam
    /// </summary>
    public partial class removerUsuario : UserControl
    {
        private string Valor = "";
        public removerUsuario()
        {
            InitializeComponent();

            lbUser.Content = "";

        }
        private void btDeletarUsuario_Click(object sender, RoutedEventArgs e)
        {
            Utilidades.messageBox inputDialog;

            if (!String.IsNullOrEmpty(Valor))
            {
                inputDialog = new Utilidades.messageBox("Confirmação de exclusão do usuário", "O usuário " + Valor + " será excluido, e não poderá ser restaurado. Tem certeza que deseja prosseguir?", MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");

                if (inputDialog.ShowDialog() == true)
                {
                    if (DataBase.SqlFunctionsUsers.DropTableDBCA(Valor))
                    {
                        inputDialog = new Utilidades.messageBox("Exclusão do usuário", "O usuário " + Valor + " foi excluido com sucesso!", MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");

                        inputDialog.ShowDialog();


                        loadListbox();
                    }
                    else
                    {

                        inputDialog = new Utilidades.messageBox("Exclusão do usuário", "Não foi possível excluir o usuário " + Valor + ", entre em contato com o administrador", MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");

                        inputDialog.ShowDialog();
                    }
                }

            }
            else
            {
                inputDialog = new Utilidades.messageBox("Selecione o usuário", "Selecione algum usuário para prosseguir", MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");


                inputDialog.ShowDialog();

            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            loadListbox();

            lbUser.Content = "";
            Valor = "";

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
                    boxItem.IsSelected = false;
                    boxItem.IsEnabled = false;


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
                    boxItem.IsEnabled = true;

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

                lbUser.Content = Valor;
            }
        }
    }
}
