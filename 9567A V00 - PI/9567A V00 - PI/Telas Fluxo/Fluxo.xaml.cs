﻿using _9567A_V00___PI.Utilidades;
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

        }

        #region Click List

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

        #endregion

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

            Utilidades.VariaveisGlobais.Buffer_PLC[1].Enable_Read = false;

            VariaveisGlobais.controleProducao.Manual_Automatico = !VariaveisGlobais.controleProducao.Manual_Automatico;

            Comunicacao.Sharp7.S7.SetWordAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 18, Utilidades.Move_Bits.ControleProducaoToWord(VariaveisGlobais.controleProducao));
            
            Utilidades.VariaveisGlobais.Buffer_PLC[1].Enable_Write = true;
        }

        private void BT_confirma_Click(object sender, RoutedEventArgs e)
        {
            if (VariaveisGlobais.controleProducao.HabilitadoDosarEmManual)
            {
                Utilidades.VariaveisGlobais.Buffer_PLC[1].Enable_Read = false;

                if (VariaveisGlobais.controleProducao.primeiroProdutoDosar)
                {
                    VariaveisGlobais.controleProducao.Dosando = true;
                }
                else
                {
                    VariaveisGlobais.controleProducao.Troca_Produto = true;
                }

                //Atualiza Valores dos pesos do produto
                VariaveisGlobais.controleProducao.PesoDosar = VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].pesoProdutoDesejado;
                VariaveisGlobais.controleProducao.PesoTolerancia = VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].tolerancia;

                Comunicacao.Sharp7.S7.SetWordAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 18, Utilidades.Move_Bits.ControleProducaoToWord(VariaveisGlobais.controleProducao));
                Comunicacao.Sharp7.S7.SetRealAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 26, VariaveisGlobais.controleProducao.PesoDosar);
                Comunicacao.Sharp7.S7.SetRealAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 30, VariaveisGlobais.controleProducao.PesoTolerancia);

                Utilidades.VariaveisGlobais.Buffer_PLC[1].Enable_Write = true;

                VariaveisGlobais.controleProducao.HabilitadoDosarEmManual = false;
                VariaveisGlobais.controleProducao.primeiroProdutoDosar = false;

            }
        }
    }
}
