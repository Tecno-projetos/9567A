using _9567A_V00___PI.Telas_Fluxo.Relatorios;
using MaterialDesignThemes.Wpf;
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
    /// Interação lógica para relatorios.xam
    /// </summary>
    public partial class relatorios : UserControl
    {
        Relatorios.relatorioProducao producao = new relatorioProducao();


        public relatorios()
        {
            InitializeComponent();
        }

        private void btProducao_Click(object sender, RoutedEventArgs e)
        {
            if (spRelatorio.Children != null)
            {
                spRelatorio.Children.Clear();
            }

            spRelatorio.Children.Add(producao);

            pckProducao.Foreground = new SolidColorBrush(Colors.Red);

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (spRelatorio.Children != null)
            {
                spRelatorio.Children.Clear();
            }

            pckProducao.Foreground = new SolidColorBrush(Colors.White);

        }
    }
}
