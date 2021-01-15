using _9567A_V00___PI.Utilidades;
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

namespace _9567A_V00___PI.Telas_Fluxo.Producao
{
    /// <summary>
    /// Interaction logic for ConfiguracaoReceitaProducao.xaml
    /// </summary>
    public partial class ConfiguracaoReceitaProducao : UserControl
    {
        private messageBox inputDialog;
        public event EventHandler IniciouProducao;

        public event EventHandler TelaAnterior;
        public ConfiguracaoReceitaProducao()
        {
            InitializeComponent();
        }

        private void btContinuar_Click(object sender, RoutedEventArgs e)
        {
            if (VariaveisGlobais.controleProducao.Producao0 == -1)
            {
                inputDialog = new Utilidades.messageBox("Confirmação", "Você tem certeza que deseja iniciar a produção?", MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

                inputDialog.ShowDialog();

                if (inputDialog.DialogResult == true)
                {
                    //Preenche data inicial e data final
                    Utilidades.VariaveisGlobais.OrdensProducao[Utilidades.VariaveisGlobais.OrdensProducao.Count - 1].dateTimeInicioProducao = DateTime.Now;
                    Utilidades.VariaveisGlobais.OrdensProducao[Utilidades.VariaveisGlobais.OrdensProducao.Count - 1].dateTimeFimProducao = DateTime.Now;
                    Utilidades.VariaveisGlobais.OrdensProducao[Utilidades.VariaveisGlobais.OrdensProducao.Count - 1].pesoTotalProducao = Convert.ToSingle(txtPesoDesejado.Text);

                    //Preenche que iniciou a produção
                    Utilidades.VariaveisGlobais.OrdensProducao[Utilidades.VariaveisGlobais.OrdensProducao.Count - 1].IniciouProducao = true;

                    DataBase.SQLFunctionsProducao.AddProducaoBD(Utilidades.VariaveisGlobais.OrdensProducao[Utilidades.VariaveisGlobais.OrdensProducao.Count - 1]);

                    //Verifica qual Produção esta em execução e carrega a produção
                    DataBase.SQLFunctionsProducao.AtualizaOrdemProducaoEmExecucao();

                    if (this.IniciouProducao != null)
                        this.IniciouProducao(this, e);
                }
            }
            else
            {
                inputDialog = new Utilidades.messageBox("Liberado", "Verifique se tem alguma produção na dosagem", MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");

                inputDialog.ShowDialog();
            }

        }

        private void txtPesoDesejado_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBox txtReceber = (TextBox)sender;

            txtReceber.Text = Utilidades.VariaveisGlobais.floatingKeypad(txtReceber.Text, 6).ToString();

            calculaApartirPesoTotalDesejado(false);
        }

        private void txtQtdReceita_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Atualiza Peso máximo e volume máximo suportado
            txtPesoMaximoPermitido.Text = "300.0";
        }

        private bool calculaApartirPesoTotalDesejado(bool AddBateladas)
        {
            bool ret = false;
            //int pesoDesejado = 0;
            //int pesoResto = 0;
            //int countBatelada = 0;

            //if (!String.IsNullOrEmpty(txtPesoDesejado.Text))
            //{

            //    if (Int32.TryParse(txtPesoDesejado.Text, out pesoDesejado))
            //    {
            //        //atualiza o peso total da produção
            //        Utilidades.VariaveisGlobais.ProducaoReceita.pesoTotalProducao = pesoDesejado;

            //        //Calcula quantas bateladas inteiras terão utilizando o máximo permitido de peso
            //        bateladas = pesoDesejado / Convert.ToInt32(txtPesoMaximoPermitido.Text);

            //        //Calcula o peso da ultima batelada
            //        pesoResto = pesoDesejado % Convert.ToInt32(txtPesoMaximoPermitido.Text);

            //        if (AddBateladas)
            //        {
            //            Utilidades.VariaveisGlobais.ProducaoReceita.batelada.Clear();

            //            //cria um dummy da batelada para poder inserir cada peso desejado para cada batelada
            //            Utilidades.Batelada DummyBatelada = new Batelada();

            //            //passa por cada batelada e adiciona o peso das bateladas inteiras
            //            for (int i = 0; i < bateladas; i++)
            //            {
            //                countBatelada += 1;

            //                DummyBatelada = new Batelada();

            //                DummyBatelada.pesoDesejado = Convert.ToInt32(txtPesoMaximoPermitido.Text);
            //                DummyBatelada.numeroBatelada = countBatelada;
            //                Utilidades.VariaveisGlobais.ProducaoReceita.batelada.Add(DummyBatelada);
            //            }

            //            //Verifica se tem peso resto para adicionar na ultima batelada o restante (isso ira acontecer quando ocorrer valores que não forem multiplo do valor máximo permitido)
            //            if (pesoResto > 0)
            //            {
            //                bateladas += 1;
            //                countBatelada += 1;
            //                DummyBatelada = new Batelada();

            //                DummyBatelada.pesoDesejado = pesoResto;
            //                DummyBatelada.numeroBatelada = countBatelada;
            //                Utilidades.VariaveisGlobais.ProducaoReceita.batelada.Add(DummyBatelada);
            //            }
            //        }
            //        else
            //        {
            //            //Verifica se tem peso resto para adicionar na ultima batelada o restante (isso ira acontecer quando ocorrer valores que não forem multiplo do valor máximo permitido)
            //            if (pesoResto > 0)
            //            {
            //                bateladas += 1;
            //            }
            //        }


            //        //passa para a produção receita a quantidade de bateladas.
            //        Utilidades.VariaveisGlobais.ProducaoReceita.quantidadeBateladas = bateladas;

            //        ret = true;
            //    }
            //    else
            //    {
            //        if (AddBateladas)
            //        {
            //            inputDialog = new Utilidades.messageBox("Valor não é inteiro", "Por favor verifique se o valor pertecem aos números inteiros", MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

            //            inputDialog.ShowDialog();
            //        }

            //    }

            //}
            //else
            //{
            //    if (AddBateladas)
            //    {
            //        inputDialog = new Utilidades.messageBox("Campo Necessário", "Por favor verifique se o campo Peso Total Desejado esta vazio!", MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

            //        inputDialog.ShowDialog();
            //    }

            //}

            //atualizaValoresPesoVolumeQtdBatelada();

            return ret;
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            if (this.TelaAnterior != null)
                this.TelaAnterior(this, e);
        }
    }
}
