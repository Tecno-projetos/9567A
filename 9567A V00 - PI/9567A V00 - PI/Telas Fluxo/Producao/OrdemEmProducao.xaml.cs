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

namespace _9567A_V00___PI.Telas_Fluxo.Producao
{
    /// <summary>
    /// Interaction logic for OrdemEmProducao.xaml
    /// </summary>
    public partial class OrdemEmProducao : UserControl
    {
        public OrdemEmProducao()
        {
            InitializeComponent();
        }

        private void btLeftList_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGrid_Ordens, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - 20);
        }

        private void btDownList_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGrid_Ordens, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToVerticalOffset(scroll.VerticalOffset + 5);
        }

        private void btRightList_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGrid_Ordens, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset + 20);
        }

        private void btUpList_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGrid_Ordens, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - 5);
        }

        private void btLeftList_Produtos_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGrid_Produtos, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - 20);
        }

        private void btDownList_Produtos_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGrid_Produtos, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToVerticalOffset(scroll.VerticalOffset + 5);
        }

        private void btRightList_Produtos_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGrid_Produtos, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset + 20);
        }

        private void btUpList_Produtos_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGrid_Produtos, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - 5);
        }

        private void DataGrid_Ordens_LoadingRow(object sender, DataGridRowEventArgs e)
        {

        }

        private void DataGrid_Ordens_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DataGrid_Ordens.SelectedIndex != -1)
            {
                var rowList = (DataGrid_Ordens.ItemContainerGenerator.ContainerFromIndex(DataGrid_Ordens.SelectedIndex) as DataGridRow).Item as DataRowView;

                var index = Utilidades.VariaveisGlobais.OrdensProducao.FindIndex(x => x.id == Convert.ToInt32(rowList.Row.ItemArray[0]));

                DataTable dt = new DataTable();

                dt.Columns.Add("Produto");
                dt.Columns.Add("Peso(kg)");
                dt.Columns.Add("Tolerância(%)");

                foreach (var item in Utilidades.VariaveisGlobais.OrdensProducao[index].receita.listProdutos)
                {
                    DataRow dr = dt.NewRow();

                    dr["Produto"] = item.produto.descricao;
                    dr["Peso(kg)"] = item.pesoProdutoReceita;
                    dr["Tolerância(%)"] = item.tolerancia;
                    dt.Rows.Add(dr);
                }

                DataGrid_Produtos.Dispatcher.Invoke(delegate { DataGrid_Produtos.ItemsSource = dt.DefaultView; });
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Utilidades.VariaveisGlobais.OrdensProducao.Count >= 1)
            {

                DataTable dt = new DataTable();

                dt.Columns.Add("Nº");
                dt.Columns.Add("Nome");
                dt.Columns.Add("Peso");

                foreach (var ordem in Utilidades.VariaveisGlobais.OrdensProducao)
                {
                    DataRow dr = dt.NewRow();

                    dr["Nº"] = ordem.id;
                    dr["Nome"] = ordem.receita.nomeReceita;
                    dr["Peso"] = ordem.pesoTotalProducao;
                    dt.Rows.Add(dr);
                }

                DataGrid_Ordens.Dispatcher.Invoke(delegate { DataGrid_Ordens.ItemsSource = dt.DefaultView; });
            }
        }
    }
}
