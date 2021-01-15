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

namespace _9567A_V00___PI.Desenho
{
    /// <summary>
    /// Interação lógica para balancaAutomatica.xam
    /// </summary>
    public partial class balancaAutomatica : UserControl
    {
        public balancaAutomatica()
        {
            InitializeComponent();
        }

        public void Balanca(RTU.IndicadorPesagem_3102C_S indicadorPesagem_3102C_S)
        {

            if (indicadorPesagem_3102C_S.ErrorModbus_GS)
            {
                if (VariaveisGlobais.TickTack_GS)
                {
                    lbStatusBalanca.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                }
                else
                {
                    lbStatusBalanca.Background = new SolidColorBrush(Color.FromRgb(89, 76, 76));
                }
            }
            else
            {
                lbStatusBalanca.Background = new SolidColorBrush(Color.FromRgb(89, 76, 76));
            }

            LbPeso.Content = indicadorPesagem_3102C_S.PesoAtualBalanca_GS;


            if (indicadorPesagem_3102C_S.BloqueiaLeitura_GS)
            {
                btReset.IsEnabled = true;
            }
            else
            {
                btReset.IsEnabled = false;
            }  
        }

        private void btEmergencia_Click(object sender, RoutedEventArgs e)
        {
            //Reseta o erro da balança
            VariaveisGlobais.balancaPrincipal.BloqueiaLeitura_GS = false;
        }
    }
}
