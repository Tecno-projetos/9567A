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

namespace _9567A_V00___PI.Telas_Fluxo.Configuracoes
{
    /// <summary>
    /// Interaction logic for Especificacoes.xaml
    /// </summary>
    public partial class Especificacoes : UserControl
    {
        private messageBox inputDialog;

        public Especificacoes()
        {
            InitializeComponent();
        }

        private void btSalvarInformacoes_Click(object sender, RoutedEventArgs e)
        {
            EscritaInformacoes(1);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LeituraInformacoes();
        }

        private void txtPesoMaximo_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void TB_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void floatingPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBox txtReceber = (TextBox)sender;

            txtReceber.Text = Utilidades.VariaveisGlobais.floatingKeypad(txtReceber.Text, 6).ToString();

        }

        private void IntergerPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            TextBox txtReceber = (TextBox)sender;

            txtReceber.Text = Utilidades.VariaveisGlobais.IntergerKeypad(txtReceber.Text, 6, 999999).ToString();
        }


        #region Funções

        private void LeituraInformacoes()
        {
            readVariablesBuffer_AuxiliaresProcesso(1);

            txtTempoLimpezaDosagem.Text = Convert.ToString(VariaveisGlobais.controleProducao.TempoLimpezaDosagem);

            txtTempoLimpezaMistura.Text = Convert.ToString(VariaveisGlobais.controleProducao.TempoLimpezaMisturador);

            txtTempoMistura.Text = Convert.ToString(VariaveisGlobais.controleProducao.TempoMistura);

            txtTempoEstabilizacao.Text = Convert.ToString(VariaveisGlobais.controleProducao.TempoEstabilizacao);

            txtTempoPulmaoVazio.Text = Convert.ToString(VariaveisGlobais.controleProducao.TempoPulmaoVazio);
        }

        private void EscritaInformacoes(int bufferPlc_Auxiliares)
        {
            VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Enable_Read = false;

            VariaveisGlobais.controleProducao.TempoLimpezaDosagem = Convert.ToInt32(txtTempoLimpezaDosagem.Text);

            VariaveisGlobais.controleProducao.TempoLimpezaMisturador = Convert.ToInt32(txtTempoLimpezaMistura.Text);

            VariaveisGlobais.controleProducao.TempoMistura = Convert.ToInt32(txtTempoMistura.Text);

            VariaveisGlobais.controleProducao.TempoEstabilizacao = Convert.ToInt32(txtTempoEstabilizacao.Text);

            VariaveisGlobais.controleProducao.TempoPulmaoVazio = Convert.ToInt32(txtTempoPulmaoVazio.Text);


            writeVariables_AuxiliaresProcesso(1);

            inputDialog = new Utilidades.messageBox("Salvar", "Informações salvas com Sucesso!", MaterialDesignThemes.Wpf.PackIconKind.Plus, "OK", "Fechar");

            inputDialog.ShowDialog();
        }


        #region Leitura e Escrita

        public void writeVariables_AuxiliaresProcesso(int bufferPlc_Auxiliares)
        {

            //Controle Máquinas
            Comunicacao.Sharp7.S7.SetDIntAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 54, VariaveisGlobais.controleProducao.TempoLimpezaDosagem);
            Comunicacao.Sharp7.S7.SetDIntAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 58, VariaveisGlobais.controleProducao.TempoLimpezaMisturador);
            Comunicacao.Sharp7.S7.SetDIntAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 34, VariaveisGlobais.controleProducao.TempoMistura);
            Comunicacao.Sharp7.S7.SetDIntAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 38, VariaveisGlobais.controleProducao.TempoEstabilizacao);
            Comunicacao.Sharp7.S7.SetDIntAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 42, VariaveisGlobais.controleProducao.TempoPulmaoVazio);

            VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Enable_Write = true;

        }

        public void readVariablesBuffer_AuxiliaresProcesso(int bufferPlc_Auxiliares)
        {
            //Controle Máquinas
            VariaveisGlobais.controleProducao.TempoLimpezaDosagem = Comunicacao.Sharp7.S7.GetDIntAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 54);
            VariaveisGlobais.controleProducao.TempoLimpezaMisturador = Comunicacao.Sharp7.S7.GetDIntAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 58);
            VariaveisGlobais.controleProducao.TempoMistura = Comunicacao.Sharp7.S7.GetDIntAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 34);
            VariaveisGlobais.controleProducao.TempoEstabilizacao = Comunicacao.Sharp7.S7.GetDIntAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 38);
            VariaveisGlobais.controleProducao.TempoPulmaoVazio = Comunicacao.Sharp7.S7.GetDIntAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 42);
        }



        #endregion

        #endregion
    }
}
