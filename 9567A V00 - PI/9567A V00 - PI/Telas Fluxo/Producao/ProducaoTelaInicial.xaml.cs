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
    /// Interaction logic for ProducaoTelaInicial.xaml
    /// </summary>
    public partial class ProducaoTelaInicial : UserControl
    {
        public ProducaoTelaInicial()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Utilidades.functions.atualizalistReceitas();

            DataTable dt = new DataTable();

            dt = DataBase.SqlFunctionsReceitas.getReceitas();

            DataGrid_Receita.Dispatcher.Invoke(delegate { DataGrid_Receita.ItemsSource = dt.DefaultView; });

            DataGrid_Produtos.Dispatcher.Invoke(delegate { DataGrid_Produtos.ItemsSource = null; });
        }


        private void btLeftList_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGrid_Receita, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - 20);
        }

        private void btDownList_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGrid_Receita, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToVerticalOffset(scroll.VerticalOffset + 5);
        }

        private void btRightList_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGrid_Receita, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset + 20);
        }

        private void btUpList_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGrid_Receita, 0) as Decorator).Child as ScrollViewer;

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

        private void btSelecionaRota_Click(object sender, RoutedEventArgs e)
        {
            if (!Utilidades.VariaveisGlobais.ProducaoReceita.IniciouProducao)
            {
                if (DataGrid_Receita.SelectedIndex != -1)
                {

                    Utilidades.VariaveisGlobais.ProducaoReceita = new Utilidades.Producao();

                    var rowList = (DataGrid_Receita.ItemContainerGenerator.ContainerFromIndex(DataGrid_Receita.SelectedIndex) as DataGridRow).Item as DataRowView;

                    Utilidades.functions.atualizalistReceitas();

                    var index = Utilidades.VariaveisGlobais.listReceitas.FindIndex(x => x.id == Convert.ToInt32(rowList.Row.ItemArray[0]));

                    //Passa id da receita desejada para a produção receita
                    Utilidades.VariaveisGlobais.ProducaoReceita.IdReceitaBase = Utilidades.VariaveisGlobais.listReceitas[index].id;
                    //Passa a Receita desejada para a produção Receita
                    Utilidades.VariaveisGlobais.ProducaoReceita.receita = Utilidades.VariaveisGlobais.listReceitas[index];

                    //Passa TempoPreMistura da receita desejada para a produção receita
                    Utilidades.VariaveisGlobais.ProducaoReceita.tempoPreMistura = Utilidades.VariaveisGlobais.ValoresEspecificacoesEquipamentos.TempoPreMistura;

                    //Passa TempoPosMistura da receita desejada para a produção receita
                    Utilidades.VariaveisGlobais.ProducaoReceita.tempoPosMistura = Utilidades.VariaveisGlobais.ValoresEspecificacoesEquipamentos.TempoPosMistura;

                    //Dispara evento para editar produtos.
                    if (this.EventoReceitaSelecionada != null)
                        this.EventoReceitaSelecionada(this, e);
                }
            }
            else
            {
                inputDialog = new Utilidades.messageBox("Em produção", "Existe uma produção em andamento, aguarde a finalização da produção!", MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

                inputDialog.ShowDialog();
            }
        }
    }
}
