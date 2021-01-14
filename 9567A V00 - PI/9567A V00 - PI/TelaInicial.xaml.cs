using _9567A_V00___PI.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace _9567A_V00___PI
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class TelaInicial : Window
    {

        #region Dispacher Timers

        DispatcherTimer timer50ms = new DispatcherTimer(); //Roda o CLP

        DispatcherTimer timer1s = new DispatcherTimer(); //Roda ciclos de 1 segundo

        DispatcherTimer timer4h = new DispatcherTimer(); //Roda ciclos de 1 segundo

        DispatcherTimer Clock_TickTack = new DispatcherTimer(); //Roda ciclos de 1 segundo

        #endregion

        Utilidades.messageBox inputDialog;

        RTU.IndicadorPesagem_3102C_S inidicadorPesagem;


        public TelaInicial()
        {
            InitializeComponent();


            #region Verifica se existe alguma instãnca do arquivo aberta se existir fecha todos

            string nomeProcesso = Process.GetCurrentProcess().ProcessName;

            // Obtém todos os processos com o nome do atual
            Process[] processes = Process.GetProcessesByName(nomeProcesso);

            // Maior do que 1, porque a instância atual também conta
            if (processes.Length > 1)
            {

                VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = "Supervisório Aberto! Fechando todas as instâncias.";

                Process[] proc1 = Process.GetProcessesByName(nomeProcesso);
                proc1[0].Kill();

                Process proc = Process.GetCurrentProcess();
                proc.Kill();

                return;
            }


            VariaveisGlobais.Load_Connection();

            #endregion

            VariaveisGlobais.Fluxo.BMP1_Designer.loadEquip(Utilidades.typeEquip.PD, Utilidades.typeCommand.PD, 2, 0, "Misturador Motor 1", "BMP-1", "1", "12");
            VariaveisGlobais.Fluxo.BMP2_Designer.loadEquip(Utilidades.typeEquip.PD, Utilidades.typeCommand.PD, 22, 0, "Misturador Motor 2", "BMP-2", "2", "13");
            VariaveisGlobais.Fluxo.TD1_Designer.loadEquip(Utilidades.typeEquip.INV, Utilidades.typeCommand.INV, 42, 0, "Rosca Ensaque", "TD-1", "3", "14");
            VariaveisGlobais.Fluxo.FM1_Designer.loadEquip(Utilidades.typeEquip.PD, Utilidades.typeCommand.PD, 94, 0, "Captação de Pó", "FM-1", "4", "15");
           
            VariaveisGlobais.Fluxo.RP1_Designer.loadEquip(Utilidades.typeEquip.Atuador, Utilidades.typeCommand.Registro, 114, 0, "Atuador 1", "RP-1", "-", "16/17");
            VariaveisGlobais.Fluxo.RP2_Designer.loadEquip(Utilidades.typeEquip.Atuador, Utilidades.typeCommand.Registro, 118, 0, "Atuador 2", "RP-2", "-", "16/17");
            VariaveisGlobais.Fluxo.RP3_Designer.loadEquip(Utilidades.typeEquip.Atuador, Utilidades.typeCommand.Registro, 122, 0, "Atuador 3", "RP-3", "-", "16/17");


            #region Configuração Buffers PLC

            Utilidades.VariaveisGlobais.Buffer_PLC[0].Name = "DB Controle Todos Equipamentos";
            Utilidades.VariaveisGlobais.Buffer_PLC[0].DBNumber = 2;
            Utilidades.VariaveisGlobais.Buffer_PLC[0].Start = 0;
            Utilidades.VariaveisGlobais.Buffer_PLC[0].Size = 130;
            Utilidades.VariaveisGlobais.Buffer_PLC[0].Enable_Read = true;
            Utilidades.VariaveisGlobais.Buffer_PLC[0].Enable_Write = false;

            Utilidades.VariaveisGlobais.Buffer_PLC[1].Name = "DB Produção Automática";
            Utilidades.VariaveisGlobais.Buffer_PLC[1].DBNumber = 18;
            Utilidades.VariaveisGlobais.Buffer_PLC[1].Start = 0;
            Utilidades.VariaveisGlobais.Buffer_PLC[1].Size = 62;
            Utilidades.VariaveisGlobais.Buffer_PLC[1].Enable_Read = true;
            Utilidades.VariaveisGlobais.Buffer_PLC[1].Enable_Write = false;

            //Utilidades.VariaveisGlobais.Buffer_PLC[2].Name = "DB Produção Ensaque";
            //Utilidades.VariaveisGlobais.Buffer_PLC[2].DBNumber = 25;
            //Utilidades.VariaveisGlobais.Buffer_PLC[2].Start = 0;
            //Utilidades.VariaveisGlobais.Buffer_PLC[2].Size = 16;
            //Utilidades.VariaveisGlobais.Buffer_PLC[2].Enable_Read = false;
            //Utilidades.VariaveisGlobais.Buffer_PLC[2].Enable_Write = false;

            //Utilidades.VariaveisGlobais.Buffer_PLC[3].Name = "DB Auxiliares";
            //Utilidades.VariaveisGlobais.Buffer_PLC[3].DBNumber = 22;
            //Utilidades.VariaveisGlobais.Buffer_PLC[3].Start = 0;
            //Utilidades.VariaveisGlobais.Buffer_PLC[3].Size = 32;
            //Utilidades.VariaveisGlobais.Buffer_PLC[3].Enable_Read = true;
            //Utilidades.VariaveisGlobais.Buffer_PLC[3].Enable_Write = false;

            //Utilidades.VariaveisGlobais.Buffer_PLC[4].Name = "DB Configuracoes Auxiliares Processo";
            //Utilidades.VariaveisGlobais.Buffer_PLC[4].DBNumber = 23;
            //Utilidades.VariaveisGlobais.Buffer_PLC[4].Start = 0;
            //Utilidades.VariaveisGlobais.Buffer_PLC[4].Size = 102;
            //Utilidades.VariaveisGlobais.Buffer_PLC[4].Enable_Read = true;
            //Utilidades.VariaveisGlobais.Buffer_PLC[4].Enable_Write = false;


            for (int i = 0; i < Utilidades.VariaveisGlobais.Buffer_PLC.Length; i++)
            {
                Utilidades.VariaveisGlobais.Buffer_PLC[i].Buffer = new byte[Utilidades.VariaveisGlobais.Buffer_PLC[i].Size];
                Utilidades.VariaveisGlobais.Buffer_PLC[i].Result = 99999;
            }

            #endregion

            #region Configuração Dispatcher

            timer50ms.Interval = TimeSpan.FromMilliseconds(50);
            timer50ms.Tick += timer_Tick;
            timer50ms.Start();
            ////====================================================
            timer1s.Interval = TimeSpan.FromSeconds(1);
            timer1s.Tick += timer1s_Tick;
            timer1s.Start();
            ////====================================================
            //timer4h.Interval = TimeSpan.FromHours(4);
            //timer4h.Tick += timer4h_Tick;
            //timer4h.Start();
            ////====================================================
            Clock_TickTack.Interval = TimeSpan.FromSeconds(1);
            Clock_TickTack.Tick += timerTickTack;
            Clock_TickTack.Start();

            #endregion

            VariaveisGlobais.windowFirstLoading.Close();
            Utilidades.VariaveisGlobais.Window_Diagnostic.Closing += Window_Diagnostic_Closing;


            spInical.Children.Add(Utilidades.VariaveisGlobais.Fluxo);


            //Verifica se possui um alarme ativo.
            AlarmInSup.Visibility = Visibility.Hidden;


        }

        private void Window_Diagnostic_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Utilidades.VariaveisGlobais.Window_Diagnostic.Hide();
        }

        private void timerTickTack(object sender, EventArgs e)
        {
            if (Utilidades.VariaveisGlobais.TickTack_GS)
            {
                Utilidades.VariaveisGlobais.TickTack_GS = false;
            }
            else
            {
                Utilidades.VariaveisGlobais.TickTack_GS = true;
            }
        }


        private void timer1s_Tick(object sender, EventArgs e)
        {
            //Chama a atulização da Manutenção
            VariaveisGlobais.manutencao.atualizaManutencao();

            try
            {
                DataTable dt = new DataTable();

                dt = DataBase.SqlFunctionsEquips.GetGridAlarm_Table_EquipAlarmEvent();

                if (dt.Rows != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        AlarmInSup.Visibility = Visibility.Visible;
                        VariaveisGlobais.manutencao.AlarmInsup_GS = Visibility.Visible;
                    }
                    else
                    {
                        AlarmInSup.Visibility = Visibility.Hidden;
                        VariaveisGlobais.manutencao.AlarmInsup_GS = Visibility.Hidden;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }




        private void timer_Tick(object sender, EventArgs e)
        {

            lbAno.Content = DateTime.Now.Year;
            lbDiaMes.Content = DateTime.Now.Day + "/" + DateTime.Now.Month;
            lbHorario.Content = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;


            VariaveisGlobais.CommunicationPLC.readBuffersPLC(); //Chama a leitura no PLC

            //Verifica se esta lendo valor válido do CLP

            if (Comunicacao.Sharp7.S7.GetIntAt(Utilidades.VariaveisGlobais.Buffer_PLC[0].Buffer, 0) == 1000)
            {

                //Atualiza Balança
                //VariaveisGlobais.balancaPrincipal.LeituraModbus();

                ////Escreve o peso lido da balança
                ////Lembrando que -1 simboliza erro na leitura.
                //VariaveisGlobais.balancaPrincipal.EscritaCLP(0, 2);

                ////Atualiza Niveis Silos
                //Utilidades.VariaveisGlobais.niveis = Move_Bits.Dword_TO_NIveis(Comunicacao.Sharp7.S7.GetDWordAt(Utilidades.VariaveisGlobais.Buffer_PLC[3].Buffer, 0), Utilidades.VariaveisGlobais.niveis);

                ////Atualiza Dword Geral de auxiliares Processo.
                //Utilidades.VariaveisGlobais.auxiliaresProcesso = Move_Bits.DwordTocontroleAuxiliaresProcesso(Comunicacao.Sharp7.S7.GetDWordAt(Utilidades.VariaveisGlobais.Buffer_PLC[4].Buffer, 56), Utilidades.VariaveisGlobais.auxiliaresProcesso);


                //Atualização Equip

                VariaveisGlobais.Fluxo.BMP1_Designer.actualize_Equip = true;
                VariaveisGlobais.Fluxo.BMP2_Designer.actualize_Equip = true;
                VariaveisGlobais.Fluxo.TD1_Designer.actualize_Equip = true;
                VariaveisGlobais.Fluxo.FM1_Designer.actualize_Equip = true;


                VariaveisGlobais.Fluxo.RP1_Designer.actualize_Equip = true;
                VariaveisGlobais.Fluxo.RP2_Designer.actualize_Equip = true;
                VariaveisGlobais.Fluxo.RP3_Designer.actualize_Equip = true;
            }

            if (Comunicacao.Sharp7.S7.GetIntAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 0) == 1000)
            {
                if (Utilidades.VariaveisGlobais.Buffer_PLC[1].Enable_Read)
                {
                    VariaveisGlobais.controleProducao.Producao0 = Comunicacao.Sharp7.S7.GetDIntAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 2);
                    VariaveisGlobais.controleProducao.Producao1 = Comunicacao.Sharp7.S7.GetDIntAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 6);
                    VariaveisGlobais.controleProducao.Producao2 = Comunicacao.Sharp7.S7.GetDIntAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 10);
                    VariaveisGlobais.controleProducao.Producao3 = Comunicacao.Sharp7.S7.GetDIntAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 14);


                    VariaveisGlobais.controleProducao = Move_Bits.WordToControleProducao(Comunicacao.Sharp7.S7.GetWordAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 18), VariaveisGlobais.controleProducao);

                    VariaveisGlobais.controleProducao.StatusDosagem = Comunicacao.Sharp7.S7.GetIntAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 20);
                    VariaveisGlobais.controleProducao.StatusMistura = Comunicacao.Sharp7.S7.GetIntAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 22);
                    VariaveisGlobais.controleProducao.StatusExpedicao = Comunicacao.Sharp7.S7.GetIntAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 24);

                    VariaveisGlobais.controleProducao.PesoDosar = Comunicacao.Sharp7.S7.GetRealAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 26);
                    VariaveisGlobais.controleProducao.PesoTolerancia = Comunicacao.Sharp7.S7.GetRealAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 30);

                    VariaveisGlobais.controleProducao.TempoMistura = Comunicacao.Sharp7.S7.GetDIntAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 34);
                    VariaveisGlobais.controleProducao.TempoEstabilizacao = Comunicacao.Sharp7.S7.GetDIntAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 38);
                    VariaveisGlobais.controleProducao.TempoPulmaoVazio = Comunicacao.Sharp7.S7.GetDIntAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 42);

                    VariaveisGlobais.controleProducao.Peso_Total_Produzindo = Comunicacao.Sharp7.S7.GetRealAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 46);
                    VariaveisGlobais.controleProducao.Peso_Parcial_Produzindo = Comunicacao.Sharp7.S7.GetRealAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 50);

                    VariaveisGlobais.controleProducao.TempoLimpezaDosagem = Comunicacao.Sharp7.S7.GetDIntAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 54);
                    VariaveisGlobais.controleProducao.TempoLimpezaMisturador = Comunicacao.Sharp7.S7.GetDIntAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 58);
                }
            }

            VariaveisGlobais.CommunicationPLC.writeBufferPLC();//Chama a escrita no PLC
        }

        #region Clicks

        #region Login e Logout

        private bool login()
        {
            if (DataBase.SqlFunctionsUsers.ExistTableDBCA(txtUser.Text))
            {
                if (DataBase.SqlFunctionsUsers.CheckPasswordDBCA(txtUser.Text, txtSenha.Password))
                {
                    DataBase.SqlFunctionsUsers.IntoDateDBCA(txtUser.Text, DataBase.SqlFunctionsUsers.MD5Cryptography(txtSenha.Password), DataBase.SqlFunctionsUsers.GetLastValueTableDBCA(txtUser.Text, "GroupUser"), DataBase.SqlFunctionsUsers.GetLastValueTableDBCA(txtUser.Text, "Email"), "Entrou");

                    VariaveisGlobais.UserLogged_GS = txtUser.Text;
                    VariaveisGlobais.GroupUserLogged_GS = DataBase.SqlFunctionsUsers.GetLastValueTableDBCA(txtUser.Text, "GroupUser");
                    VariaveisGlobais.PasswordLogged_GS = txtSenha.Password;



                    if (VariaveisGlobais.GroupUserLogged_GS.Equals("Operador"))
                    {
                        VariaveisGlobais.NumberOfGroup_GS = 1;
                    }
                    else if (VariaveisGlobais.GroupUserLogged_GS.Equals("Manutenção"))
                    {
                        VariaveisGlobais.NumberOfGroup_GS = 2;
                    }
                    else if (VariaveisGlobais.GroupUserLogged_GS.Equals("Administrador"))
                    {
                        VariaveisGlobais.NumberOfGroup_GS = 3;
                    }


                    txtUser.IsEnabled = false;
                    txtSenha.Password = "";
                    txtSenha.IsEnabled = false;


                    iconLogin.Kind = MaterialDesignThemes.Wpf.PackIconKind.Logout;
                    iconLogin.Foreground = new SolidColorBrush(Colors.Red);

                    return true;

                }
                else
                {
                    Utilidades.messageBox inputDialog = new messageBox("Senha Incorreta", "Senha incorreta, por favor verifique e tente novamente", MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

                    inputDialog.ShowDialog();

                    return false;

                }
            }
            else
            {

                Utilidades.messageBox inputDialog = new messageBox("Usuário não Cadastrado", "Usuário não cadastrado, por favor verifique e tente novamente", MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");

                inputDialog.ShowDialog();

                return false;


            }
        }

        private void logout()
        {
            DataBase.SqlFunctionsUsers.IntoDateDBCA(VariaveisGlobais.UserLogged_GS, DataBase.SqlFunctionsUsers.GetLastValueTableDBCA(VariaveisGlobais.UserLogged_GS, "Password"), DataBase.SqlFunctionsUsers.GetLastValueTableDBCA(VariaveisGlobais.UserLogged_GS, "GroupUser"), DataBase.SqlFunctionsUsers.GetLastValueTableDBCA(VariaveisGlobais.UserLogged_GS, "Email"), "Saiu");

            txtUser.IsEnabled = true;
            txtSenha.IsEnabled = true;

            txtUser.Text = "";
            VariaveisGlobais.UserLogged_GS = "";
            VariaveisGlobais.GroupUserLogged_GS = "";
            VariaveisGlobais.PasswordLogged_GS = "";
            VariaveisGlobais.NumberOfGroup_GS = 0;

            iconLogin.Kind = MaterialDesignThemes.Wpf.PackIconKind.Login;
            iconLogin.Foreground = new SolidColorBrush(Colors.White);

        }

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            if (iconLogin.Kind == MaterialDesignThemes.Wpf.PackIconKind.Login)
            {
                login();
            }
            else
            {
                logout();

                //spInical.Children.Clear();



            }
        }

        private void openKeyboard(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Utilidades.VariaveisGlobais.NumberOfGroup_GS == 0)
                {
                    Teclados.keyboard.openKeyboard();
                }
            }
            catch (Exception ex)
            {
                Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
            }
        }

        private void txtSenha_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            KeyConverter key = new KeyConverter();
            if (e.Key == Key.Enter || e.Key == Key.DeadCharProcessed)
            {
                login();
                btLogin.Focus();
            }
        }
        #endregion

        #region Clicks Menu

        private void btHome_Click(object sender, RoutedEventArgs e)
        {
            if (spInical != null)
            {
                spInical.Children.Clear();

                spInical.Children.Add(Utilidades.VariaveisGlobais.Fluxo);

            }
        }

        private void btProducao_Click(object sender, RoutedEventArgs e)
        {
            if (Utilidades.VariaveisGlobais.NumberOfGroup_GS == 0)
            {
                Utilidades.messageBox inputDialog = new messageBox(Utilidades.VariaveisGlobais.faltaUsuarioTitle, Utilidades.VariaveisGlobais.faltaUsuarioMessage, MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

                inputDialog.ShowDialog();

                return;
            }

            if (spInical != null)
            {
                spInical.Children.Clear();

                spInical.Children.Add(Utilidades.VariaveisGlobais.producao);
                AtualizaButton(pckProducao);

            }

            if (Utilidades.VariaveisGlobais.producao.spControleProducao.Children != null)
            {
                Utilidades.VariaveisGlobais.producao.spControleProducao.Children.Clear();
            }

        }

        private void btConfiguracoes_Click(object sender, RoutedEventArgs e)
        {
            if (Utilidades.VariaveisGlobais.NumberOfGroup_GS == 0)
            {
                Utilidades.messageBox inputDialog = new messageBox(Utilidades.VariaveisGlobais.faltaUsuarioTitle, Utilidades.VariaveisGlobais.faltaUsuarioMessage, MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

                inputDialog.ShowDialog();

                return;
            }


            if (spInical != null)
            {
                spInical.Children.Clear();

                spInical.Children.Add(Utilidades.VariaveisGlobais.configuracoes);

                AtualizaButton(pckConfiguracoes);

            }
        }

        #endregion

        #endregion

        #region Funções

        public void AtualizaButton(MaterialDesignThemes.Wpf.PackIcon packIcon)
        {
            if (packIcon.Name == pckManutencao.Name)
            {
                pckManutencao.Foreground = VariaveisGlobais.Verde;
                pckConfiguracoes.Foreground = VariaveisGlobais.Branco;
                pckHome.Foreground = VariaveisGlobais.Branco;
                pckProducao.Foreground = VariaveisGlobais.Branco;
              
                pckRelatorio.Foreground = VariaveisGlobais.Branco;
                pckUser.Foreground = VariaveisGlobais.Branco;
            }
            else if (packIcon.Name == pckConfiguracoes.Name)
            {
                pckManutencao.Foreground = VariaveisGlobais.Branco;
                pckConfiguracoes.Foreground = VariaveisGlobais.Verde;
                pckHome.Foreground = VariaveisGlobais.Branco;
                pckProducao.Foreground = VariaveisGlobais.Branco;
               
                pckRelatorio.Foreground = VariaveisGlobais.Branco;
                pckUser.Foreground = VariaveisGlobais.Branco;
            }
            else if (packIcon.Name == pckHome.Name)
            {
                pckManutencao.Foreground = VariaveisGlobais.Branco;
                pckConfiguracoes.Foreground = VariaveisGlobais.Branco;
                pckHome.Foreground = VariaveisGlobais.Verde;
                pckProducao.Foreground = VariaveisGlobais.Branco;
               
                pckRelatorio.Foreground = VariaveisGlobais.Branco;
                pckUser.Foreground = VariaveisGlobais.Branco;
            }
            else if (packIcon.Name == pckProducao.Name)
            {
                pckManutencao.Foreground = VariaveisGlobais.Branco;
                pckConfiguracoes.Foreground = VariaveisGlobais.Branco;
                pckHome.Foreground = VariaveisGlobais.Branco;
                pckProducao.Foreground = VariaveisGlobais.Verde;
               
                pckRelatorio.Foreground = VariaveisGlobais.Branco;
                pckUser.Foreground = VariaveisGlobais.Branco;
            }
            else if (packIcon.Name == pckRelatorio.Name)
            {
                pckManutencao.Foreground = VariaveisGlobais.Branco;
                pckConfiguracoes.Foreground = VariaveisGlobais.Branco;
                pckHome.Foreground = VariaveisGlobais.Branco;
                pckProducao.Foreground = VariaveisGlobais.Branco;
                
                pckRelatorio.Foreground = VariaveisGlobais.Verde;
                pckUser.Foreground = VariaveisGlobais.Branco;
            }
            else if (packIcon.Name == pckUser.Name)
            {
                pckManutencao.Foreground = VariaveisGlobais.Branco;
                pckConfiguracoes.Foreground = VariaveisGlobais.Branco;
                pckHome.Foreground = VariaveisGlobais.Branco;
                pckProducao.Foreground = VariaveisGlobais.Branco;
                
                pckRelatorio.Foreground = VariaveisGlobais.Branco;
                pckUser.Foreground = VariaveisGlobais.Verde;
            }
        }


        #endregion

        private void btRelatorio_Click(object sender, RoutedEventArgs e)
        {
            if (Utilidades.VariaveisGlobais.NumberOfGroup_GS == 0)
            {
                Utilidades.messageBox inputDialog = new messageBox(Utilidades.VariaveisGlobais.faltaUsuarioTitle, Utilidades.VariaveisGlobais.faltaUsuarioMessage, MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

                inputDialog.ShowDialog();

                return;
            }

            if (spInical != null)
            {
                spInical.Children.Clear();

                spInical.Children.Add(Utilidades.VariaveisGlobais.relatorios);

                AtualizaButton(pckRelatorio);

            }
        }

        private void btManutencao_Click(object sender, RoutedEventArgs e)
        {
            if (Utilidades.VariaveisGlobais.NumberOfGroup_GS == 0)
            {
                Utilidades.messageBox inputDialog = new messageBox(Utilidades.VariaveisGlobais.faltaUsuarioTitle, Utilidades.VariaveisGlobais.faltaUsuarioMessage, MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

                inputDialog.ShowDialog();

                return;
            }

            if (spInical != null)
            {
                spInical.Children.Clear();

                spInical.Children.Add(Utilidades.VariaveisGlobais.manutencao);

                AtualizaButton(pckManutencao);

            }
        }
    }
}
