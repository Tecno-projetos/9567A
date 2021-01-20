using _9567A_V00___PI.Utilidades;
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
    /// Interaction logic for ConfiguracaoReceitaProducao.xaml
    /// </summary>
    public partial class ConfiguracaoReceitaProducao : UserControl
    {
        private messageBox inputDialog;
        public event EventHandler IniciouProducao;

        public event EventHandler TelaAnterior;


        private float pesoTodosProdutos;

        private float pesoProdutosReceita;

        private bool Iniciou = false;


        public ConfiguracaoReceitaProducao()
        {
            InitializeComponent();
        }

        #region Funções

        private bool calculaApartirPesoTotalDesejado(Utilidades.Producao producao)
        {
            bool ret = false;
            Double pesoDesejado = 0;

            //atualiza o peso total da produção
            Utilidades.VariaveisGlobais.OrdensProducao[Utilidades.VariaveisGlobais.OrdensProducao.Count - 1].pesoTotalProducao = Convert.ToSingle(txtPesoDesejado.Text);

            if (!String.IsNullOrEmpty(txtPesoDesejado.Text))
            {

                if (Double.TryParse(txtPesoDesejado.Text, out pesoDesejado))
                {
                    //Calcula peso total da receita
                    float pesoTotalReceita = 0.0f;
                    foreach (var produtos in Utilidades.VariaveisGlobais.OrdensProducao[Utilidades.VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos)
                    {
                        pesoTotalReceita = pesoTotalReceita += produtos.pesoProdutoReceita;
                    }

                    float fatorPeso = Utilidades.VariaveisGlobais.OrdensProducao[Utilidades.VariaveisGlobais.OrdensProducao.Count - 1].pesoTotalProducao / pesoTotalReceita;

                    //Calcula Peso de cada produto
                    foreach (var produtos in Utilidades.VariaveisGlobais.OrdensProducao[Utilidades.VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos)
                    {
                        produtos.pesoProdutoDesejado = Convert.ToSingle(Math.Round(fatorPeso * produtos.pesoProdutoReceita, 2));

                    }

                    ret = true;
                }
                else
                {
                    inputDialog = new Utilidades.messageBox("Valor não é inteiro", "Por favor verifique se o valor pertecem aos números inteiros", MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

                    inputDialog.ShowDialog();
                }

            }
            else
            {
                inputDialog = new Utilidades.messageBox("Campo Necessário", "Por favor verifique se o campo Peso Total Desejado esta vazio!", MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

                inputDialog.ShowDialog();
            }

            atualizaGridProdutos();

            return ret;
        }

        private bool CalculaPesoBaseReceita(Utilidades.Producao producao)
        {

            foreach (var produtos in Utilidades.VariaveisGlobais.OrdensProducao[Utilidades.VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos)
            {
                pesoTodosProdutos = pesoTodosProdutos + produtos.pesoProdutoReceita;
            }

            txtPeso.Text = Convert.ToString(Math.Round(pesoTodosProdutos, 2));


            return false;
        }

        private void atualizaGridProdutos()
        {
            //Atualiza o Grid de equipamentos com os equipamentos que pertencem a receita selecionada.

            DataTable dt = new DataTable();

            dt.Columns.Add("Produto");
            dt.Columns.Add("Peso(kg)");
            dt.Columns.Add("Tolerância(%)");

            foreach (var item in Utilidades.VariaveisGlobais.OrdensProducao[Utilidades.VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos)
            {
                DataRow dr = dt.NewRow();

                dr["Produto"] = item.produto.descricao;
                dr["Peso(kg)"] = item.pesoProdutoDesejado;
                dr["Tolerância(%)"] = item.tolerancia;
                dt.Rows.Add(dr);
            }


            DataGrid_Produtos.Dispatcher.Invoke(delegate { DataGrid_Produtos.ItemsSource = dt.DefaultView; });

        }

        #endregion

        #region Clicks e Sender

        private void btContinuar_Click(object sender, RoutedEventArgs e)
        {

            if (Convert.ToDouble(txtPesoDesejado.Text) < 300 && Convert.ToDouble(txtPesoDesejado.Text) > 0)
            {

                if (VariaveisGlobais.controleProducao.Producao0 == -1)
                {
                    inputDialog = new Utilidades.messageBox("Confirmação", "Você tem certeza que deseja iniciar a produção? Após o inicio não poderá ser retirado a produção! ", MaterialDesignThemes.Wpf.PackIconKind.Error, "Sim", "Não");

                    inputDialog.ShowDialog();

                    if (inputDialog.DialogResult == true)
                    {
                        //Preenche data inicial e data final
                        Utilidades.VariaveisGlobais.OrdensProducao[Utilidades.VariaveisGlobais.OrdensProducao.Count - 1].dateTimeInicioProducao = DateTime.Now;

                        Utilidades.VariaveisGlobais.OrdensProducao[Utilidades.VariaveisGlobais.OrdensProducao.Count - 1].dateTimeFimProducao = DateTime.Now;

                        //Preenche que iniciou a produção
                        Utilidades.VariaveisGlobais.OrdensProducao[Utilidades.VariaveisGlobais.OrdensProducao.Count - 1].IniciouProducao = true;

                        calculaApartirPesoTotalDesejado(Utilidades.VariaveisGlobais.OrdensProducao[Utilidades.VariaveisGlobais.OrdensProducao.Count - 1]);

                        DataBase.SQLFunctionsProducao.AddProducaoBD(Utilidades.VariaveisGlobais.OrdensProducao[Utilidades.VariaveisGlobais.OrdensProducao.Count - 1]);

                        Utilidades.VariaveisGlobais.Buffer_PLC[1].Enable_Read = false;

                        VariaveisGlobais.controleProducao.Producao0 = Utilidades.VariaveisGlobais.OrdensProducao[Utilidades.VariaveisGlobais.OrdensProducao.Count - 1].id;

                        Comunicacao.Sharp7.S7.SetDIntAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 2, VariaveisGlobais.controleProducao.Producao0);

                        var index = Utilidades.VariaveisGlobais.OrdensProducao.FindIndex(x => x.id == VariaveisGlobais.controleProducao.Producao0);
                  
                        if (index != -1)
                        {
                            Int32 codigoReceita = Utilidades.VariaveisGlobais.OrdensProducao[index].receita.Codigo;

                            Comunicacao.Sharp7.S7.SetDIntAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 34, Convert.ToInt32(DataBase.SqlFunctionsReceitas.getTempoMistura(codigoReceita)));
                        }

                        Utilidades.VariaveisGlobais.Buffer_PLC[1].Enable_Write = true;

                        //Verifica qual Produção esta em execução e carrega a produção
                        DataBase.SQLFunctionsProducao.AtualizaOrdemProducaoEmExecucao();

                        Iniciou = true;

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
            else
            {

                inputDialog = new Utilidades.messageBox("Liberado", "O peso está acima de 300 kg ou abaixo de 0 kg! Diminua a produção.", MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");

                inputDialog.ShowDialog();


            }
        }

        private void txtPesoDesejado_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBox txtReceber = (TextBox)sender;

            txtReceber.Text = Utilidades.VariaveisGlobais.floatingKeypad(txtReceber.Text, 6).ToString();


            pesoProdutosReceita = pesoProdutosReceita + (pesoTodosProdutos / 2);
            txtQtdReceita.Text = Convert.ToString(pesoProdutosReceita / pesoTodosProdutos);


            calculaApartirPesoTotalDesejado(Utilidades.VariaveisGlobais.OrdensProducao[Utilidades.VariaveisGlobais.OrdensProducao.Count - 1]);
          


        }

        private void txtQtdReceita_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            if (this.TelaAnterior != null)
                this.TelaAnterior(this, e);
        }

        private void btAumenta_Click(object sender, RoutedEventArgs e)
        {
            if (pesoTodosProdutos > 0)
            {
                pesoProdutosReceita = pesoProdutosReceita + (pesoTodosProdutos / 2);

                txtPesoDesejado.Text = Convert.ToString(pesoProdutosReceita);

                txtQtdReceita.Text = Convert.ToString(pesoProdutosReceita / pesoTodosProdutos);

                calculaApartirPesoTotalDesejado(Utilidades.VariaveisGlobais.OrdensProducao[Utilidades.VariaveisGlobais.OrdensProducao.Count - 1]);
            }
            else
            {
                inputDialog = new Utilidades.messageBox("Sem peso receita", "Por favor verifique se a receita possui produtos com peso válidos!", MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");

                inputDialog.ShowDialog();
            }

        }

        private void btDiminui_Click(object sender, RoutedEventArgs e)
        {
            if (pesoTodosProdutos > 0)
            {
                pesoProdutosReceita = pesoProdutosReceita - (pesoTodosProdutos / 2);

                txtPesoDesejado.Text = Convert.ToString(pesoProdutosReceita);

                txtQtdReceita.Text = Convert.ToString(pesoProdutosReceita / pesoTodosProdutos);

                calculaApartirPesoTotalDesejado(Utilidades.VariaveisGlobais.OrdensProducao[Utilidades.VariaveisGlobais.OrdensProducao.Count - 1]);
            }
            else
            {
                inputDialog = new Utilidades.messageBox("Sem peso receita", "Por favor verifique se a receita possui produtos com peso válidos!", MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");

                inputDialog.ShowDialog();
            }
        }

        #endregion

        #region User control Load
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Atualiza Peso máximo e volume máximo suportado

            Iniciou = false;

            txtPesoMaximoPermitido.Text = "300.0";

            txtPesoDesejado.Text = "0";
            txtQtdReceita.Text = "0";

            pesoTodosProdutos = 0;
            pesoProdutosReceita = 0;

            CalculaPesoBaseReceita(Utilidades.VariaveisGlobais.OrdensProducao[Utilidades.VariaveisGlobais.OrdensProducao.Count - 1]);

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (!Iniciou)
            {
                Utilidades.VariaveisGlobais.OrdensProducao.RemoveAt(VariaveisGlobais.dummyIndex_CriandoReceita);
            }


        }
        #endregion


    }
}
