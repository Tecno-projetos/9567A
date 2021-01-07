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

namespace _9567A_V00___PI.Partidas.Controle
{
    /// <summary>
    /// Interação lógica para controleAtuadorAnalogico.xam
    /// </summary>
    public partial class controleAtuadorAnalogico : UserControl
    {

        public event EventHandler atualizarPosicao;
        public event EventHandler Bt_Ligar_Click;
        public event EventHandler Bt_Reset_Click;
        public event EventHandler Bt_Libera_Click;
        public event EventHandler Bt_Manutencao_Click;
        public event EventHandler Bt_Manual_Click;
        public event EventHandler Bt_Fechar_Click;

        public controleAtuadorAnalogico()
        {
            InitializeComponent();
        }

        public void actualize_UI(Utilidades.VariaveisGlobais.type_All Command)
        {
            //Habilita ou desabilita botões
            if (!Command.Standard.Emergencia || 
                Command.Standard.FalhaConfirmacaoContatorLado1 ||
                Command.Standard.FalhaConfirmacaoContatorLado2 ||
                Command.Standard.falhaPosicionamento ||
                Command.Standard.falhaLeituraPosicao
                )
            {
                btManual.Dispatcher.Invoke(delegate { btManual.IsEnabled = false; });
                btLibera.Dispatcher.Invoke(delegate { btLibera.IsEnabled = false; });
                btLigar.Dispatcher.Invoke(delegate { btLigar.IsEnabled = false; });

            }
            else if (Command.Standard.Reposicionando)
            {
                btLigar.Dispatcher.Invoke(delegate { btLigar.IsEnabled = false; });
                btManutencao.Dispatcher.Invoke(delegate { btManutencao.IsEnabled = true; });
                btLibera.Dispatcher.Invoke(delegate { btLibera.IsEnabled = false; });
                btManual.Dispatcher.Invoke(delegate { btManual.IsEnabled = false; });
            }
            else
            {
                btManual.Dispatcher.Invoke(delegate { btManual.IsEnabled = true; });
                btManutencao.Dispatcher.Invoke(delegate { btManutencao.IsEnabled = true; });
                btLibera.Dispatcher.Invoke(delegate { btLibera.IsEnabled = true; });
                btLigar.Dispatcher.Invoke(delegate { btLigar.IsEnabled = true; });
            }


            if (!Command.Standard.Emergencia)
            {
                btManutencao.Dispatcher.Invoke(delegate { btManutencao.IsEnabled = true; });
            }

            btReset.Dispatcher.Invoke(delegate { btReset.IsEnabled = true; });

            //Atualiza status dos botões
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
            else if (Command.Standard.FalhaConfirmacaoContatorLado1)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Falha Confirmação Contator Abrir"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Red); });
            }
            else if (Command.Standard.FalhaConfirmacaoContatorLado2)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Falha Confirmação Contator Fechar"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Red); });
            }
            else if (Command.Standard.falhaPosicionamento)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Falha posição"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Red); });
            }
            else if (Command.Standard.falhaLeituraPosicao)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Falha leitura posição"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Red); });
            }
            else if (Command.Standard.Manutencao)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Em Manutenção"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Blue); });
            }
            else if (Command.Standard.Reposicionando)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Reposicionando"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.Black); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Yellow); });
            }
            else if (Command.Standard.Liga_Manual && Command.Standard.AcionaLado1)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Abrindo"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.ForestGreen); });
            }
            else if (Command.Standard.Liga_Manual && Command.Standard.AcionaLado2)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Fechando"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.ForestGreen); });
            }
            else if (Command.Standard.Liga_Manual)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Ligado em Manual"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.ForestGreen); });
            }
            else if (Command.Standard.AcionandoAutomatico && Command.Standard.AcionaLado1)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Abrindo"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.ForestGreen); });
            }
            else if (Command.Standard.AcionandoAutomatico && Command.Standard.AcionaLado2)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Fechando"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.ForestGreen); });
            }
            else if (Command.Standard.AcionandoAutomatico)
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Ligado em Automático"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.Black); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Green); });
            }
            else
            {
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Content = "Desligado"; });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Foreground = new SolidColorBrush(Colors.White); });
                lbStatusMotor.Dispatcher.Invoke(delegate { lbStatusMotor.Background = new SolidColorBrush(Colors.Gray); });
            }

            lbPosicaoAtual.Dispatcher.Invoke(delegate { lbPosicaoAtual.Foreground = new SolidColorBrush(Colors.White); });
            lbPosicaoAtual.Dispatcher.Invoke(delegate { lbPosicaoAtual.Background = new SolidColorBrush(Colors.Gray); });

            lbPosicaoAtual.Dispatcher.Invoke(delegate { lbPosicaoAtual.Content = Command.AtuadorA.PosicaoAtual + " %"; });

        }


        #region Keypad + Atualizar posição solicitada.

        private void tbPosicaoSolicitada_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {



            keypad mainWindow = new keypad(true, 2);

            if (mainWindow.ShowDialog() == true)
            {
                //Recebe Valor antigo digitado no Textbox
                int oldValue = Convert.ToInt16(tbPosicaoSolicitada.Text);
                //Recebe o novo valor digitado no Keypad
                int newValue = Convert.ToInt16(mainWindow.Result);


                //Verifica se o novo valor é diferente do valor anterior para que atualize a váriavel no CLP
                if (oldValue != newValue)
                {
                    //Verifica se o novo valor é menor que 100
                    if (newValue <= 100)
                    {
                        tbPosicaoSolicitada.Text = Convert.ToString(newValue);
                    }
                    else
                    {
                        //Envia o oldValue pois o valor máximo ultrapassou o limite.
                        tbPosicaoSolicitada.Text = Convert.ToString(oldValue);
                    }


                    //Retira o foco do textbox.
                    Keyboard.ClearFocus();

                    //Dispara o evento de atualizar a váriavel no CLP.
                    if (this.atualizarPosicao != null)
                        this.atualizarPosicao(this, e);

                }

            }
        }

        private void btAumenta_Click(object sender, RoutedEventArgs e)
        {
            //Recebe o valor.
            int newValue = Convert.ToInt16(tbPosicaoSolicitada.Text);

            //Verifica se o valor está menor que o permitido
            if (newValue <= 100)
            {
                //Acresenta o valor solicitado com +5
                newValue = newValue + 5;

                //Verifica se o novo valor é maior que o permitido.
                if (newValue > 100)
                {
                    //Envia o valor máximo.
                    newValue = 100;
                }
            }
            else
            {

                newValue = 100;
            }

            tbPosicaoSolicitada.Text = Convert.ToString(newValue);

            //Dispara o evento de atualizar a váriavel no CLP.
            if (this.atualizarPosicao != null)
                this.atualizarPosicao(this, e);

        }

        private void btDiminui_Click(object sender, RoutedEventArgs e)
        {
            //Recebe o valor.
            int newValue = Convert.ToInt16(tbPosicaoSolicitada.Text);

            //Verifica se o valor esta permitido
            if (newValue >= 0)
            {
                //Diminui o valor solicitado com -5
                newValue = newValue - 5;

                //Verifica se o novo valor é maior que o permitido.
                if (newValue < 0)
                {
                    //Envia o valor minimo.
                    newValue = 0;
                }
            }
            else
            {

                newValue = 0;
            }

            tbPosicaoSolicitada.Text = Convert.ToString(newValue);

            //Dispara o evento de atualizar a váriavel no CLP.
            if (this.atualizarPosicao != null)
                this.atualizarPosicao(this, e);
        }

        #endregion


        #region Encapsulate Fields

        public string PosicaoSolicitada_GS
        {
            get => tbPosicaoSolicitada.Text;
            set
            {
                tbPosicaoSolicitada.Dispatcher.Invoke(delegate { tbPosicaoSolicitada.Text = value; });
            }
        }

        #endregion


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

        private void btFechar_Click(object sender, RoutedEventArgs e)
        {
            if (this.Bt_Fechar_Click != null)
                this.Bt_Fechar_Click(this, e);
        }


    }
}
