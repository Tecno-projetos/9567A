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

namespace _9567A_V00___PI.Telas_Fluxo
{
    /// <summary>
    /// Interaction logic for producao.xaml
    /// </summary>
    public partial class producao : UserControl
    {

        Utilidades.messageBox inputDialog;
        public Telas_Fluxo.Producao.ProducaoTelaInicial TelaInicialProducao = new Producao.ProducaoTelaInicial();
        public Telas_Fluxo.Producao.ConfiguracaoReceitaProducao TelaConfiguracaoReceitaProducao = new Producao.ConfiguracaoReceitaProducao();
        public Telas_Fluxo.Producao.OrdemEmProducao TelaOrdemEmProducao = new Producao.OrdemEmProducao();

        public producao()
        {
            InitializeComponent();

            TelaInicialProducao.EventoReceitaSelecionada += new EventHandler(EventoReceitaSelecionada);
            TelaConfiguracaoReceitaProducao.TelaAnterior += new EventHandler(EventoTelaAnterior);
        }

        private void btTelaInicialRacao_Click(object sender, RoutedEventArgs e)
        {

            if (Utilidades.VariaveisGlobais.ValoresPreenchidos())
            {
                if (spControleProducao != null)
                {
                    spControleProducao.Children.Clear();
                }
                spControleProducao.Children.Add(TelaInicialProducao);

            }
            else
            {
                //falta preencher algum valor
                inputDialog = new Utilidades.messageBox("Falta informções", "Verifique se os valores na tela de configuração das especificações estão preenchidos!", MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");
                inputDialog.ShowDialog();
            
            }

        }

        private void btEmProducao_Click(object sender, RoutedEventArgs e)
        {
            if (spControleProducao != null)
            {
                spControleProducao.Children.Clear();
            }
            spControleProducao.Children.Add(TelaOrdemEmProducao);
        }

        private void btRelatorio_Click(object sender, RoutedEventArgs e)
        {

        }

        protected void EventoReceitaSelecionada(object sender, EventArgs e)
        {
            if (spControleProducao != null)
            {
                spControleProducao.Children.Clear();
            }
            spControleProducao.Children.Add(TelaConfiguracaoReceitaProducao);
        }
        protected void EventoTelaAnterior(object sender, EventArgs e)
        {
            if (spControleProducao != null)
            {
                spControleProducao.Children.Clear();
            }
            spControleProducao.Children.Add(TelaInicialProducao);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btRetirarProducao_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
