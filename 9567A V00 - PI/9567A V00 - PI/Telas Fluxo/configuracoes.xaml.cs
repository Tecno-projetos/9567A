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
                //VariaveisGlobais.Buffer_PLC[4].Enable_Read = false;

                //dummyAuxiliaresProcesso = Utilidades.VariaveisGlobais.auxiliaresProcesso;


                //dummyAuxiliaresProcesso.Set_Automatico_Equipamentos = true;
                //Utilidades.VariaveisGlobais.auxiliaresProcesso = dummyAuxiliaresProcesso;


                //Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[4].Buffer, 56, Move_Bits.AuxiliaresProcessoToDword(Utilidades.VariaveisGlobais.auxiliaresProcesso)); //Atualiza os Bits do command

                //VariaveisGlobais.Buffer_PLC[4].Enable_Write = true;
            }
        }
    }
}
