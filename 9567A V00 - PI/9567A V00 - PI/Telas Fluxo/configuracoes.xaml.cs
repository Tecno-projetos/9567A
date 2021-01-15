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
    /// Interaction logic for Configuracoes.xaml
    /// </summary>
    public partial class configuracoes : UserControl
    {
        Utilidades.messageBox inputDialog;

        Telas_Fluxo.Configuracoes.Especificacoes especificaoes = new Configuracoes.Especificacoes();

        Utilidades.VariaveisGlobais.AuxiliaresBooleanas dummyAuxiliaresProcesso = new Utilidades.VariaveisGlobais.AuxiliaresBooleanas();

        public configuracoes()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (spConfiguracao != null)
            {
                spConfiguracao.Children.Clear();
            }
        }

        private void btInformacoesEspecificacoes_Click(object sender, RoutedEventArgs e)
        {
            if (Utilidades.VariaveisGlobais.NumberOfGroup_GS < 2)
            {
                Utilidades.messageBox inputDialog = new messageBox(Utilidades.VariaveisGlobais.faltaPermissaoTitle, Utilidades.VariaveisGlobais.faltaPermissaoMessage, MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

                inputDialog.ShowDialog();

                return;
            }

            if (spConfiguracao != null)
            {
                spConfiguracao.Children.Clear();
            }

            spConfiguracao.Children.Add(especificaoes);
        }




        private void BT_AutomaticAll_Click(object sender, RoutedEventArgs e)
        {
            inputDialog = new Utilidades.messageBox("Automático", "Deseja Passar os equipamentos para automático!", MaterialDesignThemes.Wpf.PackIconKind.Information, "Sim", "Não");

            if (inputDialog.ShowDialog() == true)
            {
                VariaveisGlobais.Buffer_PLC[0].Enable_Read = false;

                dummyAuxiliaresProcesso = Utilidades.VariaveisGlobais.auxiliaresBooleanos;


                dummyAuxiliaresProcesso.Automatico_Equips = true;
                Utilidades.VariaveisGlobais.auxiliaresBooleanos = dummyAuxiliaresProcesso;


                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[0].Buffer, 126, Move_Bits.AuxiliaresBooleanasToDword(Utilidades.VariaveisGlobais.auxiliaresBooleanos)); //Atualiza os Bits do command

                VariaveisGlobais.Buffer_PLC[0].Enable_Write = true;
            }
        }
    }
}
