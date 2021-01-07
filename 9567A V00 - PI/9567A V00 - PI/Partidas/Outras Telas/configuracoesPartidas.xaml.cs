using _9567A_V00___PI.Teclados;
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

namespace _9567A_V00___PI.Partidas.Outras_Telas
{
    /// <summary>
    /// Interação lógica para configuracoesPartidas.xam
    /// </summary>
    public partial class configuracoesPartidas : UserControl
    {
        Int32 n;

        public event EventHandler resetTotal_Click;
        public event EventHandler resetParcial_Click;
        public event EventHandler atualizaSPManutencao_Click;
        public event EventHandler atualizaSPLimpeza_Click;

        public configuracoesPartidas()
        {
            InitializeComponent();
        }

        private void btResetTotal_Click(object sender, RoutedEventArgs e)
        {
            if (this.resetTotal_Click != null)
                this.resetTotal_Click(this, e);
        }

        private void btResetParcial_Click(object sender, RoutedEventArgs e)
        {
            if (this.resetParcial_Click != null)
                this.resetParcial_Click(this, e);
        }
        public void actualize_UI(ref Utilidades.VariaveisGlobais.type_All Command)
        {
            TB_Total_Horas.Text = Convert.ToString(Command.PD.HorimetroTotal);
            TB_Horas.Text = Convert.ToString(Command.PD.HorimetroParcial);

        }

        #region Encapsulate Fields

        public string SpManutencao
        {
            set
            {
                TB_SPMantencao.Dispatcher.Invoke(delegate { TB_SPMantencao.Text = value; });

            }
            get
            {
                return TB_SPMantencao.Text;
            }
        }

        public string SpLimpeza
        {
            set
            {
                TB_SPLimpeza.Dispatcher.Invoke(delegate { TB_SPLimpeza.Text = value; });

            }
            get
            {
                return TB_SPLimpeza.Text;
            }
        }
        #endregion

        private void TB_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBox tb = (TextBox)e.OriginalSource;
                tb.Dispatcher.BeginInvoke(
                    new Action(delegate
                    {
                        tb.SelectAll();
                    }), System.Windows.Threading.DispatcherPriority.Input);
            }
            catch (Exception ex)
            {
                Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();

            }
        }

        private void TB_SPMantencao_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBox txtReceber = (TextBox)sender;

            txtReceber.Text = Utilidades.VariaveisGlobais.IntergerKeypad(txtReceber.Text, 4, 9999).ToString();
            //Retira o foco do textbox.
            Keyboard.ClearFocus();

            //Dispara o evento de atualizar a váriavel no CLP.
            if (this.atualizaSPManutencao_Click != null)
                this.atualizaSPManutencao_Click(this, e);

        }

        private void TB_SPLimpeza_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBox txtReceber = (TextBox)sender;

            txtReceber.Text = Utilidades.VariaveisGlobais.IntergerKeypad(txtReceber.Text, 1, 99).ToString();
            //Retira o foco do textbox.
            Keyboard.ClearFocus();

            //Dispara o evento de atualizar a váriavel no CLP.
            if (this.atualizaSPLimpeza_Click != null)
                this.atualizaSPLimpeza_Click(this, e);
        }

    }
}
