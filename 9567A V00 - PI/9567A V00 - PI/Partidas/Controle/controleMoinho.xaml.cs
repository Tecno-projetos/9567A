using MaterialDesignThemes.Wpf;
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

namespace _9567A_V00___PI.Partidas.Controle
{
    /// <summary>
    /// Interação lógica para controleMoinho.xam
    /// </summary>
    public partial class controleMoinho : UserControl
    {

        public event EventHandler Bt_Ligar_Click;
        public event EventHandler Bt_Reset_Click;
        public event EventHandler Bt_Libera_Click;
        public event EventHandler Bt_Inverte_Click;
        public event EventHandler Bt_Manutencao_Click;
        public event EventHandler Bt_Manual_Click;

        public controleMoinho()
        {
            InitializeComponent();

        }

        public void actualize_UI(Utilidades.VariaveisGlobais.type_All Command)
        {

            //Habilita ou desabilita botões
            if (!Command.Standard.Emergencia || Command.Standard.Falha_Geral)
            {
               
                btManual.Dispatcher.Invoke(delegate { btManual.IsEnabled = false; });

                btLibera.Dispatcher.Invoke(delegate { btLibera.IsEnabled = false; });

                btLigar.Dispatcher.Invoke(delegate { btLigar.IsEnabled = false; });

             
            }
            else
            {
     
                btManual.Dispatcher.Invoke(delegate { btManual.IsEnabled = true; });

                btLibera.Dispatcher.Invoke(delegate { btLibera.IsEnabled = true; });

                btLigar.Dispatcher.Invoke(delegate { btLigar.IsEnabled = true; });

                
            }
            if (!Command.Standard.Emergencia)
            {
                btManutencao.Dispatcher.Invoke(delegate { btManutencao.IsEnabled = true; });
            }

            btReset.Dispatcher.Invoke(delegate { btReset.IsEnabled = true; });
            //Atualiza status dos botões2

            if (Command.Standard.Liga_Manual)
            {
                btLigar.Dispatcher.Invoke(delegate { btLigar.IsChecked = true; });
            }
            else
            {
                btLigar.Dispatcher.Invoke(delegate { btLigar.IsChecked = false; });
            }

            if (Command.Standard.Manual)
            {
                btManual.Dispatcher.Invoke(delegate { btManual.Content = "M"; });
                lbManual.Dispatcher.Invoke(delegate { lbManual.Text = "Em Modo Manual"; });


            }

            if (Command.Standard.Automatico)
            {
                btManual.Dispatcher.Invoke(delegate { btManual.Content = "A"; });
                lbManual.Dispatcher.Invoke(delegate { lbManual.Text = "Em Modo Automático"; });
            }

            if (Command.Standard.Manutencao)
            {
                btManutencao.Dispatcher.Invoke(delegate { btManutencao.IsChecked = true; });
            }
            else
            {
                btManutencao.Dispatcher.Invoke(delegate { btManutencao.IsChecked = false; });
            }

            if (Command.Standard.Libera_Bloqueio)
            {
                iconLibera.Dispatcher.Invoke(delegate { iconLibera.Kind = MaterialDesignThemes.Wpf.PackIconKind.LockOpen; });
                textLibera.Dispatcher.Invoke(delegate { textLibera.Text = "Bloquear Liberada"; });
                btLibera.Dispatcher.Invoke(delegate { btLibera.Background = new SolidColorBrush(Colors.Green); });
            }
            else
            {
                iconLibera.Dispatcher.Invoke(delegate { iconLibera.Kind = MaterialDesignThemes.Wpf.PackIconKind.Lock; });
                textLibera.Dispatcher.Invoke(delegate { textLibera.Text = "Liberar Cascata"; });
                btLibera.Dispatcher.Invoke(delegate { btLibera.Background = new SolidColorBrush(Colors.Yellow); });
            }

            //Status Motor
            if (!Command.Standard.Emergencia)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Em Emergência"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Red); });

            }
            else if (Command.Standard.Falha_Geral)
            {
                if (Command.Standard.Falha_Partida_Nao_Confirmou)
                {
                    lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Falha Confirmação"; });
                    lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                    lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Red); });
                }
                else if (Command.Standard.Falha_Contator_Desligou)
                {
                    lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Falha Partida Não Desligou"; });
                    lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                    lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Red); });
                }
                else if (Command.Standard.Falha_Disjuntor_Desligou)
                {
                    lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Falha Partida Desligou"; });
                    lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                    lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Red); });
                }
                else if (Command.Standard.Falha_Partida_Nao_Desligou)
                {
                    lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Falha Partida Não Desligou"; });
                    lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                    lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Red); });
                }
            }
            else if (Command.Standard.SentidoGiroMotorMudou && !Command.Standard.Ligado)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Verificar sentido Alimentador"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.Black); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Yellow); });
            }
            else if (Command.Standard.Manutencao)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Em Manutenção"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Blue); });
            }
            else if (Command.Standard.Disjuntor_Desligado)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Disjuntor Desligado"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.Black); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Yellow); });
            }
            else if (Command.Standard.Ligando)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Ligando"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.Black); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.ForestGreen); });
            }
            else if (Command.Standard.Desligando)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Desligando"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.ForestGreen); });
            }
            else if (Command.Standard.Ligado)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Ligado"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.Black); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Green); });
            }
            else
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Desligado"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Gray); });
            }

            lbCorrenteAtual.Dispatcher.Invoke(delegate { lbCorrenteAtual.Content = Command.SS.Corrente_Atual + " A"; });

            lbCorrenteAtual.Dispatcher.Invoke(delegate { lbCorrenteAtual.Foreground = new SolidColorBrush(Colors.White); });
            lbCorrenteAtual.Dispatcher.Invoke(delegate { lbCorrenteAtual.Background = new SolidColorBrush(Colors.Gray); });

            lbTempoReversao.Dispatcher.Invoke(delegate { lbTempoReversao.Content = "Falta: " + Command.SS.Tempo_Reversao_Atual + " min."; });

            lbTempoReversao.Dispatcher.Invoke(delegate { lbTempoReversao.Foreground = new SolidColorBrush(Colors.White); });
            lbTempoReversao.Dispatcher.Invoke(delegate { lbTempoReversao.Background = new SolidColorBrush(Colors.Gray); });

            if (Command.Standard.SentidoGiro)
            {
                lbSentidoGiro.Dispatcher.Invoke(delegate { lbSentidoGiro.Content = "Sentido Horário"; });
            }
            else
            {
                lbSentidoGiro.Dispatcher.Invoke(delegate { lbSentidoGiro.Content = "Sentido Anti-Horário"; });
            }


            lbSentidoGiro.Dispatcher.Invoke(delegate { lbSentidoGiro.Foreground = new SolidColorBrush(Colors.White); });
            lbSentidoGiro.Dispatcher.Invoke(delegate { lbSentidoGiro.Background = new SolidColorBrush(Colors.Gray); });


        }

        private void btLigar_Click(object sender, RoutedEventArgs e)
        {
            if (this.Bt_Ligar_Click != null)
                this.Bt_Ligar_Click(this, e);
        }

        private void btReset_Click(object sender, RoutedEventArgs e)
        {
            if (this.Bt_Reset_Click != null)
                this.Bt_Reset_Click(this, e);
        }

        private void btLibera_Click(object sender, RoutedEventArgs e)
        {
            if (this.Bt_Libera_Click != null)
                this.Bt_Libera_Click(this, e);
        }

        private void btInverte_Click(object sender, RoutedEventArgs e)
        {
            if (this.Bt_Inverte_Click != null)
                this.Bt_Inverte_Click(this, e);
        }

        private void btManutencao_Click(object sender, RoutedEventArgs e)
        {
            if (this.Bt_Manutencao_Click != null)
                this.Bt_Manutencao_Click(this, e);
        }

        private void btManual_Click(object sender, RoutedEventArgs e)
        {
            if (this.Bt_Manual_Click != null)
                this.Bt_Manual_Click(this, e);
        }

    }
}
