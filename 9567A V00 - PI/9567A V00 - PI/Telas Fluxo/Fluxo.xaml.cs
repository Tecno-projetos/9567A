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
            nivelDigital_Designer.Nivel_Baixo_GS = VariaveisGlobais.auxiliaresBooleanos.NivelSilo;

            AtualizaEmergencia(Utilidades.VariaveisGlobais.auxiliaresBooleanos.Emergencia);


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
    }
}
