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
    /// Interação lógica para historicoUsuarios.xam
    /// </summary>
    public partial class historicoUsuarios : UserControl
    {
        Utilidades.messageBox inputDialog;

        private string Valor = "";

        public historicoUsuarios()
        {
            InitializeComponent();
        }

        private void loadListbox()
        {

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

                        Valor = tablename;

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
            }
        }

        private void btPesquisar_Click(object sender, RoutedEventArgs e)
        {

            if (String.IsNullOrEmpty(Valor))
            {
                inputDialog = new Utilidades.messageBox("Sem Usuário", "Por favor selecione um usuário", MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");

                inputDialog.ShowDialog();
            }
            else
            {
                if (DTPStart.Value < DTPEnd.Value)
                {

                    System.Data.DataTable Data = DataBase.SqlFunctionsUsers.Get_Table(Valor, Convert.ToDateTime(DTPStart.Value), Convert.ToDateTime(DTPEnd.Value));
                    DataGridHistory.ItemsSource = Data.DefaultView;
                    DataGridHistory.Columns[0].Header = "Id";
                    DataGridHistory.Columns[1].Visibility = Visibility.Hidden;
                    DataGridHistory.Columns[2].Header = "Grupo de Usuários";
                    DataGridHistory.Columns[3].Header = "Evento";
                    DataGridHistory.Columns[4].Header = "E-mail";
                    DataGridHistory.Columns[5].Header = "Data/Hora";
                    DataGridHistory.Columns[6].Visibility = Visibility.Hidden;


                }
                else
                {
                    inputDialog = new Utilidades.messageBox("Selecione Data", "A data inicial deve ser menor que a final", MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");

                    inputDialog.ShowDialog();
                }
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            //Reseto o datagrid
            DataGridHistory.Dispatcher.Invoke(delegate { DataGridHistory.ItemsSource = null; });

            loadListbox();

            DTPStart.Value = DateTime.Now.AddMonths(-1);
            DTPEnd.Value = DateTime.Now.AddMinutes(2);

        }

        private void btDownList_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGridHistory, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToVerticalOffset(scroll.VerticalOffset + 5);

        }

        private void btUpList_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGridHistory, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - 5);

        }

        private void btLeftList_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGridHistory, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - 20);


        }

        private void btRightList_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGridHistory, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset + 20);
        }

        private void DataGridHistory_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DataRowView item = e.Row.Item as DataRowView;

            if (item != null)
            {
                e.Row.Background = new SolidColorBrush(Colors.Silver);
                e.Row.Foreground = new SolidColorBrush(Colors.White);
                e.Row.FontSize = 9;
                e.Row.BorderBrush = new SolidColorBrush(Colors.Black);
            }
        }
    }
}
