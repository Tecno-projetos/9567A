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
    /// Interação lógica para controleAtuadorLinearBifurcada.xam
    /// </summary>
    public partial class controleAtuadorLinearBifurcada : UserControl
    {
        public event EventHandler Bt_AbrirEsquerda_Click;
        public event EventHandler Bt_AbrirDireita_Click;
        public event EventHandler Bt_Reset_Click;
        public event EventHandler Bt_Libera_Click;
        public event EventHandler Bt_Manutencao_Click;
        public event EventHandler Bt_Manual_Click;


        public controleAtuadorLinearBifurcada()
        {
            InitializeComponent();
        }

        public void actualize_UI(Utilidades.VariaveisGlobais.type_All Command)
        {
            //Habilita ou desabilita botões
            if (!Command.Standard.Emergencia ||
                Command.Standard.FalhaAcionandoLado1 ||
                Command.Standard.FalhaAcionandoLado2 ||
                Command.Standard.Falha2PosicoesAtiva ||
                Command.Standard.FalhaConfirmacaoContatorLado1 ||
                Command.Standard.FalhaConfirmacaoContatorLado2
                )
            {
                btManual.Dispatcher.Invoke(delegate { btManual.IsEnabled = false; });
                btLibera.Dispatcher.Invoke(delegate { btLibera.IsEnabled = false; });

                btAbre_Direita.Dispatcher.Invoke(delegate { btAbre_Direita.IsEnabled = false; });
                btAbre_Esquerda.Dispatcher.Invoke(delegate { btAbre_Esquerda.IsEnabled = false; });
            }
            else
            {
                btManual.Dispatcher.Invoke(delegate { btManual.IsEnabled = true; });

                btLibera.Dispatcher.Invoke(delegate { btLibera.IsEnabled = true; });

                btAbre_Direita.Dispatcher.Invoke(delegate { btAbre_Direita.IsEnabled = true; });
                btAbre_Esquerda.Dispatcher.Invoke(delegate { btAbre_Esquerda.IsEnabled = true; });
            }
            if (!Command.Standard.Emergencia)
            {
                btManutencao.Dispatcher.Invoke(delegate { btManutencao.IsEnabled = true; });
            }

            btReset.Dispatcher.Invoke(delegate { btReset.IsEnabled = true; });

            //Atualiza status dos botões
            if (Command.Standard.AcionaLado1 || (Command.Standard.EmPosicaoLado1 && !Command.Standard.AcionaLado2))
            {
                btAbre_Esquerda.Dispatcher.Invoke(delegate { btAbre_Esquerda.IsEnabled = true; });

                btAbre_Esquerda.Dispatcher.Invoke(delegate { btAbre_Esquerda.IsChecked = true; });
                btAbre_Direita.Dispatcher.Invoke(delegate { btAbre_Direita.IsChecked = false; });

            }
            else if (Command.Standard.AcionaLado2 || (Command.Standard.EmPosicaoLado2 && !Command.Standard.AcionaLado1))
            {

                btAbre_Direita.Dispatcher.Invoke(delegate { btAbre_Direita.IsEnabled = true; });

                btAbre_Direita.Dispatcher.Invoke(delegate { btAbre_Direita.IsChecked = true; });
                btAbre_Esquerda.Dispatcher.Invoke(delegate { btAbre_Esquerda.IsChecked = false; });
            }
            else if (Command.Standard.EmPosicaoLado1)
            {
                btAbre_Esquerda.Dispatcher.Invoke(delegate { btAbre_Esquerda.IsChecked = true; });

            } else if (Command.Standard.EmPosicaoLado2) 
            {
                btAbre_Direita.Dispatcher.Invoke(delegate { btAbre_Direita.IsChecked = true; });
            }
            else
            {
                //btAbre_Esquerda.Dispatcher.Invoke(delegate { btAbre_Esquerda.IsEnabled = false; });
                //btAbre_Direita.Dispatcher.Invoke(delegate { btAbre_Direita.IsEnabled = false; });


                btAbre_Esquerda.Dispatcher.Invoke(delegate { btAbre_Esquerda.IsChecked = false; });
                btAbre_Direita.Dispatcher.Invoke(delegate { btAbre_Direita.IsChecked = false; });
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
            else if (Command.Standard.FalhaAcionandoLado1)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Falha ao Abrir Esquerda"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Red); });
            }
            else if (Command.Standard.FalhaAcionandoLado2)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Falha ao Abrir Direita"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Red); });
            }
            else if (Command.Standard.Falha2PosicoesAtiva)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Falha 2 Posições Ativa"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Red); });
            }
            else if (Command.Standard.FalhaConfirmacaoContatorLado1)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Falha Confirmação Contator Esquerda"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Red); });
            }
            else if (Command.Standard.FalhaConfirmacaoContatorLado2)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Falha Confirmação Contator Direita"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Red); });
            }
            else if (Command.Standard.Manutencao)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Em Manutenção"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Blue); });
            }
            else if (Command.Standard.AcionandoLado1)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Abrindo Esquerda"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.Black); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.ForestGreen); });
            }
            else if (Command.Standard.AcionandoLado2)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Abrindo Direita"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.Black); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.ForestGreen); });
            }
            else if (Command.Standard.AcionandoAutomatico)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Acionando Automático"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.Black); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Green); });
            }
            else if (Command.Standard.EmPosicaoLado1)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Aberto Esquerda"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.Black); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Green); });
            }
            else if (Command.Standard.EmPosicaoLado2)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Aberto Direita"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.Black); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Green); });
            }
            else
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Sem Poisção"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Gray); });
            }
        }

        private void btAbre_Esquerda_Click(object sender, RoutedEventArgs e)
        {
            if (this.Bt_AbrirEsquerda_Click != null)
                this.Bt_AbrirEsquerda_Click(this, e);

        }

        private void btAbre_Direita_Click(object sender, RoutedEventArgs e)
        {
            if (this.Bt_AbrirDireita_Click != null)
                this.Bt_AbrirDireita_Click(this, e);
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
