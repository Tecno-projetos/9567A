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
        public Especificacoes()
        {
            InitializeComponent();
        }

        private void btSalvarInformacoes_Click(object sender, RoutedEventArgs e)
        {
            EscritaInformacoes(-1);
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
            //readVariablesBuffer_AuxiliaresProcesso(4);

            //txtVolumeSilo1_2.Text = Convert.ToString(Utilidades.VariaveisGlobais.auxiliaresProcesso.Volume_Maximo_Silo1_2);

            //txtVolumeBalanca.Text = Convert.ToString(Utilidades.VariaveisGlobais.auxiliaresProcesso.Volume_Maximo_Balanca);

            //txtVolumePreMisturador.Text = Convert.ToString(Utilidades.VariaveisGlobais.auxiliaresProcesso.Volume_Maximo_Pre_Misturador);

            //txtVolumePosMisturador.Text = Convert.ToString(Utilidades.VariaveisGlobais.auxiliaresProcesso.Volume_Maximo_Pos_Misturador);

            //txtPesoBalanca.Text = Convert.ToString(Utilidades.VariaveisGlobais.auxiliaresProcesso.Peso_Maximo_Balanca);

            //txtPesoPreMisturador.Text = Convert.ToString(Utilidades.VariaveisGlobais.auxiliaresProcesso.Peso_Maximo_Pre_Misturador);

            //txtPesoPosMisturador.Text = Convert.ToString(Utilidades.VariaveisGlobais.auxiliaresProcesso.Peso_Maximo_Pos_Misturador);

            //txtTempoPreMistura.Text = Convert.ToString(Utilidades.VariaveisGlobais.auxiliaresProcesso.TempoPreMisturador);

            //txtTempoPosMistura.Text = Convert.ToString(Utilidades.VariaveisGlobais.auxiliaresProcesso.TempoPosMisturador);

            //txtTolerancia.Text = Convert.ToString(Utilidades.VariaveisGlobais.auxiliaresProcesso.ToleranciaMinimaDosagemBalança);

            //if (Utilidades.VariaveisGlobais.ProducaoReceita.IniciouProducao)
            //{
            //    txtTempoPosMistura.IsEnabled = false;
            //    txtTempoPreMistura.IsEnabled = false;
            //}
            //else
            //{
            //    txtTempoPosMistura.IsEnabled = true;
            //    txtTempoPreMistura.IsEnabled = true;
            //}
        }

        private void EscritaInformacoes(int bufferPlc_Auxiliares)
        {
            //VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Enable_Read = false;

            //Utilidades.VariaveisGlobais.auxiliaresProcesso.Volume_Maximo_Silo1_2 = Convert.ToSingle(txtVolumeSilo1_2.Text);

            //Utilidades.VariaveisGlobais.auxiliaresProcesso.Volume_Maximo_Balanca = Convert.ToSingle(txtVolumeBalanca.Text);

            //Utilidades.VariaveisGlobais.auxiliaresProcesso.Volume_Maximo_Pre_Misturador = Convert.ToSingle(txtVolumePreMisturador.Text);

            //Utilidades.VariaveisGlobais.auxiliaresProcesso.Volume_Maximo_Pos_Misturador = Convert.ToSingle(txtVolumePosMisturador.Text);

            //Utilidades.VariaveisGlobais.auxiliaresProcesso.Peso_Maximo_Balanca = Convert.ToSingle(txtPesoBalanca.Text);

            //Utilidades.VariaveisGlobais.auxiliaresProcesso.Peso_Maximo_Pre_Misturador = Convert.ToSingle(txtPesoPreMisturador.Text);

            //Utilidades.VariaveisGlobais.auxiliaresProcesso.Peso_Maximo_Pos_Misturador = Convert.ToSingle(txtPesoPosMisturador.Text);

            //Utilidades.VariaveisGlobais.auxiliaresProcesso.TempoPreMisturador = Convert.ToInt32(txtTempoPreMistura.Text);

            //Utilidades.VariaveisGlobais.auxiliaresProcesso.TempoPosMisturador = Convert.ToInt32(txtTempoPosMistura.Text);

            //Utilidades.VariaveisGlobais.auxiliaresProcesso.ToleranciaMinimaDosagemBalança = Convert.ToInt32(txtTolerancia.Text);

            //writeVariables_AuxiliaresProcesso(4);

            //inputDialog = new Utilidades.messageBox("Salvar", "Informações salvas com Sucesso!", MaterialDesignThemes.Wpf.PackIconKind.Plus, "OK", "Fechar");

            //inputDialog.ShowDialog();
        }


        #region Leitura e Escrita

        public void writeVariables_AuxiliaresProcesso(int bufferPlc_Auxiliares)
        {

            //Controle Máquinas
            //Comunicacao.Sharp7.S7.SetRealAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 0, Utilidades.VariaveisGlobais.auxiliaresProcesso.Volume_Maximo_Silo1_2);
            //Comunicacao.Sharp7.S7.SetRealAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 4, Utilidades.VariaveisGlobais.auxiliaresProcesso.Volume_Maximo_Balanca);
            //Comunicacao.Sharp7.S7.SetRealAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 8, Utilidades.VariaveisGlobais.auxiliaresProcesso.Volume_Maximo_Pre_Misturador);
            //Comunicacao.Sharp7.S7.SetRealAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 12, Utilidades.VariaveisGlobais.auxiliaresProcesso.Volume_Maximo_Pos_Misturador);
            //Comunicacao.Sharp7.S7.SetRealAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 16, Utilidades.VariaveisGlobais.auxiliaresProcesso.Peso_Maximo_Silo1_2);
            //Comunicacao.Sharp7.S7.SetRealAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 20, Utilidades.VariaveisGlobais.auxiliaresProcesso.Peso_Maximo_Balanca);
            //Comunicacao.Sharp7.S7.SetRealAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 24, Utilidades.VariaveisGlobais.auxiliaresProcesso.Peso_Maximo_Pre_Misturador);
            //Comunicacao.Sharp7.S7.SetRealAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 28, Utilidades.VariaveisGlobais.auxiliaresProcesso.Peso_Maximo_Pos_Misturador);
            //Comunicacao.Sharp7.S7.SetDIntAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 68, Utilidades.VariaveisGlobais.auxiliaresProcesso.TempoPreMisturador);
            //Comunicacao.Sharp7.S7.SetDIntAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 72, Utilidades.VariaveisGlobais.auxiliaresProcesso.TempoPosMisturador);

            //Comunicacao.Sharp7.S7.SetRealAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 60, Utilidades.VariaveisGlobais.auxiliaresProcesso.ToleranciaMinimaDosagemBalança);

            //VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Enable_Write = true;

        }

        public void readVariablesBuffer_AuxiliaresProcesso(int bufferPlc_Auxiliares)
        {
            //Controle Máquinas
            //VariaveisGlobais.ValoresEspecificacoesEquipamentos.VolumeMaximoPermitidoSilo1_2 = Utilidades.VariaveisGlobais.auxiliaresProcesso.Volume_Maximo_Silo1_2 = Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 0);
            //VariaveisGlobais.ValoresEspecificacoesEquipamentos.VolumeMaximoPermitidoBalanca = Utilidades.VariaveisGlobais.auxiliaresProcesso.Volume_Maximo_Balanca = Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 4);
            //VariaveisGlobais.ValoresEspecificacoesEquipamentos.VolumeMaximoPermitidoPreMisturador = Utilidades.VariaveisGlobais.auxiliaresProcesso.Volume_Maximo_Pre_Misturador = Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 8);
            //VariaveisGlobais.ValoresEspecificacoesEquipamentos.VolumeMaximoPermitidoPosMisturador = Utilidades.VariaveisGlobais.auxiliaresProcesso.Volume_Maximo_Pos_Misturador = Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 12);
            //VariaveisGlobais.ValoresEspecificacoesEquipamentos.PesoMaximoPermitidoSilo1_2 = Utilidades.VariaveisGlobais.auxiliaresProcesso.Peso_Maximo_Silo1_2 = Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 16);
            //VariaveisGlobais.ValoresEspecificacoesEquipamentos.PesoMaximoPermitidoBalanca = Utilidades.VariaveisGlobais.auxiliaresProcesso.Peso_Maximo_Balanca = Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 20);
            //VariaveisGlobais.ValoresEspecificacoesEquipamentos.PesoMaximoPermitidoPreMisturador = Utilidades.VariaveisGlobais.auxiliaresProcesso.Peso_Maximo_Pre_Misturador = Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 24);
            //VariaveisGlobais.ValoresEspecificacoesEquipamentos.PesoMaximoPermitidoPosMisturador = Utilidades.VariaveisGlobais.auxiliaresProcesso.Peso_Maximo_Pos_Misturador = Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 28);
            //VariaveisGlobais.ValoresEspecificacoesEquipamentos.TempoPreMistura = Utilidades.VariaveisGlobais.auxiliaresProcesso.TempoPreMisturador = Comunicacao.Sharp7.S7.GetDIntAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 68);
            //VariaveisGlobais.ValoresEspecificacoesEquipamentos.TempoPosMistura = Utilidades.VariaveisGlobais.auxiliaresProcesso.TempoPosMisturador = Comunicacao.Sharp7.S7.GetDIntAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 72);

            //Utilidades.VariaveisGlobais.auxiliaresProcesso.ToleranciaMinimaDosagemBalança = Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[bufferPlc_Auxiliares].Buffer, 60);
        }



        #endregion

        #endregion
    }
}
