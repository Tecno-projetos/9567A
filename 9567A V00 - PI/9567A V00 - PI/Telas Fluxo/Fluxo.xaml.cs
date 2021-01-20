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

namespace _9567A_V00___PI.Telas_Fluxo
{
    /// <summary>
    /// Interação lógica para Fluxo.xam
    /// </summary>
    public partial class Fluxo : UserControl
    {
        Utilidades.messageBox inputDialog;

        public Fluxo()
        {
            InitializeComponent();
        }

        public void AtualizaFluxo() 
        {
            //Sinaliza o sensor de nível do silo pulmão.
            nivelDigital_Designer.Nivel_Baixo_GS = VariaveisGlobais.auxiliaresBooleanos.NivelSilo;

            //Atualiza o botão de emergência
            AtualizaEmergencia(Utilidades.VariaveisGlobais.auxiliaresBooleanos.Emergencia);

            //Envia os valores para o label
            lbCorrenteTD1.Content = TD1_Designer.Equip_GS.Command_Get.INV.Corrente_Atual + "  (A)";
            lbVelocidadeTD1.Content = TD1_Designer.Equip_GS.Command_Get.INV.Velocidade_Atual + " RPM";

            //Chama atualização da balança.
            pesoBalanca.Balanca(VariaveisGlobais.balancaPrincipal);
           
            slot1.atualizaReceita(VariaveisGlobais.controleProducao);
            slot2.atualizaReceita(VariaveisGlobais.controleProducao);
            slot3.atualizaReceita(VariaveisGlobais.controleProducao);

            BT_confirma.IsEnabled = VariaveisGlobais.controleProducao.HabilitadoDosarEmManual;

            //Atualiza texto do botão
            if (VariaveisGlobais.controleProducao.primeiroProdutoDosar)
            {
                txtVirtual2.Text = "Iniciar Dosagem";
            }
            else if (VariaveisGlobais.controleProducao.EncerrarDosagem)
            {
                txtVirtual2.Text = "Encerrar Dosagem";
            }
            else
            {
                txtVirtual2.Text = "Próximo Produto";
            }

            if (VariaveisGlobais.controleProducao.Manual_Automatico)
            {
                txtVirtual.Text = "Em Automático";
                txtVirtual.Foreground = new SolidColorBrush(Colors.White);
                pckIconManual.Foreground = new SolidColorBrush(Colors.White);

                btManualAuto.Background = new SolidColorBrush(Colors.Green);
                btManualAuto.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                txtVirtual.Text = "Em Manual";

                txtVirtual.Foreground = new SolidColorBrush(Colors.Black);
                pckIconManual.Foreground = new SolidColorBrush(Colors.Black);
                btManualAuto.Background = new SolidColorBrush(Colors.Yellow);
                btManualAuto.Foreground = new SolidColorBrush(Colors.Black);
            }


            AtualizaProdutosProducao();
        }

        #region Click List

        private void btLeftList_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGrid_Produtos, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - 20);
        }

        private void btDownList_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGrid_Produtos, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToVerticalOffset(scroll.VerticalOffset + 5);
        }

        private void btRightList_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGrid_Produtos, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset + 20);
        }

        private void btUpList_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGrid_Produtos, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - 5);
        }

        #endregion

        private void AtualizaProdutosProducao()
        {
            //Verifica se tem OP na dosagem
            if (VariaveisGlobais.controleProducao.Producao0 > 0)
            {
                VariaveisGlobais.controleProducao.indexProducao = Utilidades.VariaveisGlobais.OrdensProducao.FindIndex(x => x.id == VariaveisGlobais.controleProducao.Producao0);

                if (VariaveisGlobais.controleProducao.indexProducao != -1)
                {

                    DataTable dt = new DataTable();

                    dt.Columns.Add("Produto");
                    dt.Columns.Add("Peso");
                    dt.Columns.Add("Peso Dosado");

                    int i = 0;
                    //Verifica o produto a dosar
                    foreach (var produtos in Utilidades.VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos)
                    {
                        DataRow dr = dt.NewRow();

                        dr["Produto"] = produtos.produto.descricao;
                        dr["Peso"] = produtos.pesoProdutoDesejado;
                        dr["Peso Dosado"] = produtos.pesoProdutoDosado;

                        dt.Rows.Add(dr);

                        i++;
                    }

                    DataGrid_Produtos.Dispatcher.Invoke(delegate { DataGrid_Produtos.ItemsSource = dt.DefaultView; });
                }
            }
            else
            {
                DataTable dt = new DataTable();

                DataGrid_Produtos.Dispatcher.Invoke(delegate { DataGrid_Produtos.ItemsSource = dt.DefaultView; });
            }
        }

        private void btEmergencia_Click(object sender, RoutedEventArgs e)
        {
            VariaveisGlobais.AuxiliaresBooleanas dummyAuxiliaresProcesso = Utilidades.VariaveisGlobais.auxiliaresBooleanos;

            if (dummyAuxiliaresProcesso.Emergencia)
            {
                inputDialog = new Utilidades.messageBox("Emergência", "Deseja retirar de emergência os equipamentos!" , MaterialDesignThemes.Wpf.PackIconKind.Information, "Sim", "Não");

                if (inputDialog.ShowDialog() == true)
                {
                    VariaveisGlobais.Buffer_PLC[0].Enable_Read = false;

                    dummyAuxiliaresProcesso.Emergencia = false;
                    Utilidades.VariaveisGlobais.auxiliaresBooleanos = dummyAuxiliaresProcesso;

                    Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[0].Buffer, 126, Move_Bits.AuxiliaresBooleanasToDword(Utilidades.VariaveisGlobais.auxiliaresBooleanos)); //Atualiza os Bits do command

                    VariaveisGlobais.Buffer_PLC[0].Enable_Write = true;
                }
            }
            else
            {
                inputDialog = new Utilidades.messageBox("Emergência", "Deseja colocar em emergência os equipamentos!", MaterialDesignThemes.Wpf.PackIconKind.Information, "Sim", "Não");

                if (inputDialog.ShowDialog() == true)
                {
                    VariaveisGlobais.Buffer_PLC[0].Enable_Read = false;

                    dummyAuxiliaresProcesso.Emergencia = true;
                    Utilidades.VariaveisGlobais.auxiliaresBooleanos = dummyAuxiliaresProcesso;

                    Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[0].Buffer, 126, Move_Bits.AuxiliaresBooleanasToDword(Utilidades.VariaveisGlobais.auxiliaresBooleanos)); //Atualiza os Bits do command

                    VariaveisGlobais.Buffer_PLC[0].Enable_Write = true;

                }
            }
        }

        private void AtualizaEmergencia(bool emergencia) 
        {

            if (emergencia)
            {
                btEmergencia.Background = new SolidColorBrush(Colors.Red);
                btEmergencia.Foreground = new SolidColorBrush(Colors.White);

            }
            else
            {
                btEmergencia.Background = new SolidColorBrush(Colors.Green);
                btEmergencia.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void btManualAuto_Click(object sender, RoutedEventArgs e)
        {

            if (Utilidades.VariaveisGlobais.NumberOfGroup_GS == 0)
            {
                Utilidades.messageBox inputDialog = new messageBox(Utilidades.VariaveisGlobais.faltaUsuarioTitle, Utilidades.VariaveisGlobais.faltaUsuarioMessage, MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

                inputDialog.ShowDialog();

                return;
            }


            Utilidades.VariaveisGlobais.Buffer_PLC[1].Enable_Read = false;

            VariaveisGlobais.controleProducao.Manual_Automatico = !VariaveisGlobais.controleProducao.Manual_Automatico;

            Comunicacao.Sharp7.S7.SetWordAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 18, Utilidades.Move_Bits.ControleProducaoToWord(VariaveisGlobais.controleProducao));
            
            Utilidades.VariaveisGlobais.Buffer_PLC[1].Enable_Write = true;
        }

        private void BT_confirma_Click(object sender, RoutedEventArgs e)
        {
            if (Utilidades.VariaveisGlobais.NumberOfGroup_GS == 0)
            {
                Utilidades.messageBox inputDialog = new messageBox(Utilidades.VariaveisGlobais.faltaUsuarioTitle, Utilidades.VariaveisGlobais.faltaUsuarioMessage, MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

                inputDialog.ShowDialog();

                return;
            }


            if (VariaveisGlobais.controleProducao.primeiroProdutoDosar)
            {
                VariaveisGlobais.controleProducao.primeiroProdutoDosar = false;

                if (DataBase.SQLFunctionsProducao.Update_IniciouDosagemProduto(VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].id, VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].produto.id) > 0)
                {
                    Utilidades.VariaveisGlobais.Buffer_PLC[1].Enable_Read = false;

                    VariaveisGlobais.controleProducao.Dosando = true;
                    VariaveisGlobais.controleProducao.IniciadoDosagemProduto_PLC = false;
                    VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].iniciouDosagem = true;

                    //Atualiza Valores dos pesos do produto
                    VariaveisGlobais.controleProducao.PesoDosar = VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].pesoProdutoDesejado;
                    VariaveisGlobais.controleProducao.PesoTolerancia = VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].tolerancia;

                    Comunicacao.Sharp7.S7.SetWordAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 18, Utilidades.Move_Bits.ControleProducaoToWord(VariaveisGlobais.controleProducao));
                    Comunicacao.Sharp7.S7.SetRealAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 26, VariaveisGlobais.controleProducao.PesoDosar);
                    Comunicacao.Sharp7.S7.SetRealAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 30, VariaveisGlobais.controleProducao.PesoTolerancia);
                }
                else
                {
                    Utilidades.messageBox inputDialog = new messageBox("Erro Update DB", "Erro ao Atualizar Produto no db, Tente novamente", MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

                    inputDialog.ShowDialog();
                    return;
                }

            }
            else
            {
                if (DataBase.SQLFunctionsProducao.Update_FinalizouDosagemProduto(VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].id, VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto ].produto.id) > 0 &&
                    VariaveisGlobais.controleProducao.IniciadoDosagemProduto_PLC)
                {
                    if (DataBase.SQLFunctionsProducao.Update_PesoDosado_Produto(VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].id, VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].produto.id, VariaveisGlobais.controleProducao.Peso_Parcial_Produzindo) > 0)
                    {
                        VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].pesoProdutoDosado = VariaveisGlobais.controleProducao.Peso_Parcial_Produzindo;

                        VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].finalizouDosagem = true;

                        //Verifica se não é o ultimo produto
                        if (!VariaveisGlobais.controleProducao.EncerrarDosagem)
                        {
                            if (DataBase.SQLFunctionsProducao.Update_IniciouDosagemProduto(VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].id, VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto + 1].produto.id) > 0)
                            {
                                Utilidades.VariaveisGlobais.Buffer_PLC[1].Enable_Read = false;

                                VariaveisGlobais.controleProducao.Troca_Produto = true;
                                VariaveisGlobais.controleProducao.Estabilizado = false;
                                VariaveisGlobais.controleProducao.IniciadoDosagemProduto_PLC = false;
                                VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto + 1].iniciouDosagem = true;

                                //Atualiza Valores dos pesos do produto
                                VariaveisGlobais.controleProducao.PesoDosar = VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto + 1].pesoProdutoDesejado;
                                VariaveisGlobais.controleProducao.PesoTolerancia = VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto + 1].tolerancia;

                                Comunicacao.Sharp7.S7.SetWordAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 18, Utilidades.Move_Bits.ControleProducaoToWord(VariaveisGlobais.controleProducao));
                                Comunicacao.Sharp7.S7.SetRealAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 26, VariaveisGlobais.controleProducao.PesoDosar);
                                Comunicacao.Sharp7.S7.SetRealAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 30, VariaveisGlobais.controleProducao.PesoTolerancia);
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            Utilidades.VariaveisGlobais.Buffer_PLC[1].Enable_Read = false;
                            DataBase.SQLFunctionsProducao.Update_PesoDosadoTotal(Utilidades.VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].id, VariaveisGlobais.controleProducao.Peso_Total_Produzindo);
                            Utilidades.VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].pesoTotalProduzido = VariaveisGlobais.controleProducao.Peso_Total_Produzindo;

                            //Atualiza Valores dos pesos do produto
                            VariaveisGlobais.controleProducao.PesoDosar = 0;
                            VariaveisGlobais.controleProducao.PesoTolerancia = 0;
                            VariaveisGlobais.controleProducao.Troca_Produto = false;
                            VariaveisGlobais.controleProducao.Dosando = false;
                            VariaveisGlobais.controleProducao.IniciadoDosagemProduto_PLC = false;
                            Comunicacao.Sharp7.S7.SetWordAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 18, Utilidades.Move_Bits.ControleProducaoToWord(VariaveisGlobais.controleProducao));
                            Comunicacao.Sharp7.S7.SetRealAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 26, 0);
                            Comunicacao.Sharp7.S7.SetRealAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 30, 0);

                            VariaveisGlobais.controleProducao.EncerrarDosagem = false;
                        }
                    }
                }
                else
                {
                    return;
                }
            }

            Utilidades.VariaveisGlobais.Buffer_PLC[1].Enable_Write = true;

            VariaveisGlobais.controleProducao.HabilitadoDosarEmManual = false;


            
        }

        private void DataGrid_Produtos_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                int IndexAtual = e.Row.GetIndex();

                if (IndexAtual == VariaveisGlobais.controleProducao.indexProduto)
                {
                    e.Row.Background = new SolidColorBrush(Colors.ForestGreen);
                    e.Row.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                }

            }
            catch (Exception)
            {
            }

        }
    }
}
