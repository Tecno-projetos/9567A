using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace _9567A_V00___PI.Teclados
{
    /// <summary>
    /// Lógica interna para keypad.xaml
    /// </summary>
    public partial class keypad : Window,INotifyPropertyChanged
    {

        #region Public Properties

        private string _result;

        private Int16 _maxDispaly;
        public string Result
        {
            get { return _result; }
            private set { _result = value; this.OnPropertyChanged("Result"); }
        }

        #endregion
        /// <summary>
        /// Teclado Numpad para aplicações TouchScreen
        /// </summary>
        /// <param name="owner">Tela criada</param>
        /// <param name="bloqueiaFlutuante">Bloqueia para digitar o ponto flutuante</param>
        /// <param name="maxDisplay">Número máximo no Dysplay do numPad</param>
        public keypad(/*Window owner*/bool bloqueiaFlutuante, Int16 maxDisplay)
        {
            InitializeComponent();
            //this.Owner = owner;
            this.DataContext = this;


            _maxDispaly = maxDisplay;

            if (bloqueiaFlutuante)
            {
                button19.IsEnabled = false;
            }
            else
            {
                button19.IsEnabled = true;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            switch (button.CommandParameter.ToString())
            {
                case "ESC":
                    this.DialogResult = false;
                    break;

                case "RETURN":
                    if (Result == null)
                    {
                        Result = "0";
                    }
                    this.DialogResult = true;
                    break;

                case "BACK":
                    if (Result!= null)
                    {
                        if (Result.Length > 0)
                            Result = Result.Remove(Result.Length - 1);
                    }
                    break;

                default:
                    if (Result != null)
                    {
                        if (Result.Length <= _maxDispaly)
                        {
                            Result += button.Content.ToString();
                        }
                    }
                    else
                    {
                        Result += button.Content.ToString();
                    }

                    break;
            }
        }

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion


    }
}
