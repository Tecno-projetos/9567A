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
    /// Interação lógica para nivelBalanca.xam
    /// </summary>
    public partial class nivelDigital : UserControl
    {
        bool Nivel;

        public nivelDigital()
        {
            InitializeComponent();
        }

        public bool Nivel_Baixo_GS
        {
            set
            {
                Nivel = value;

                if (Nivel)
                {
                    nivel.Fill = Brushes.Red;
                }
                else
                {
                    nivel.Fill = new SolidColorBrush(Color.FromRgb(0, 200, 0));
                }
            }
            get
            {
                return Nivel;
            }
        }

    }
}
