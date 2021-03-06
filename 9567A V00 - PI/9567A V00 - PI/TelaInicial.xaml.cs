﻿using _9567A_V00___PI.Utilidades;
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
            VariaveisGlobais.Fluxo.RP2_Designer.loadEquip(Utilidades.typeEquip.Atuador, Utilidades.typeCommand.Registro, 118, 0, "Atuador 2-3", "RP-2-3", "-", "16/17");
            VariaveisGlobais.Fluxo.RP3_Designer.loadEquip(Utilidades.typeEquip.Atuador, Utilidades.typeCommand.Registro, 122, 0, "Atuador 4", "RP-4", "-", "16/17");


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

            Utilidades.VariaveisGlobais.Buffer_PLC[2].Name = "DB Indicador Peso Balança";
            Utilidades.VariaveisGlobais.Buffer_PLC[2].DBNumber = 17;
            Utilidades.VariaveisGlobais.Buffer_PLC[2].Start = 0;
            Utilidades.VariaveisGlobais.Buffer_PLC[2].Size = 6;
            Utilidades.VariaveisGlobais.Buffer_PLC[2].Enable_Read = true;
            Utilidades.VariaveisGlobais.Buffer_PLC[2].Enable_Write = false;



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


            VariaveisGlobais.producao.TelaConfiguracaoReceitaProducao.IniciouProducao += IniciouProducao;

            VariaveisGlobais.windowFirstLoading.Close();

            Utilidades.VariaveisGlobais.Window_Diagnostic.Closing += Window_Diagnostic_Closing;

            spInical.Children.Add(Utilidades.VariaveisGlobais.Fluxo);

            //Atualiza Menu no CLick
            AtualizaButton(pckHome);

            //Verifica se possui um alarme ativo.
            AlarmInSup.Visibility = Visibility.Hidden;

            DataBase.SQLFunctionsProducao.AtualizaOrdemProducaoEmExecucao();
        }

        private void IniciouProducao(object sender, EventArgs e)
        {
            if (spInical != null)
            {
                spInical.Children.Clear();

                spInical.Children.Add(Utilidades.VariaveisGlobais.Fluxo);
                AtualizaButton(pckHome);
            }
        }

        #region Events de telas
        private void Window_Diagnostic_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Utilidades.VariaveisGlobais.Window_Diagnostic.Hide();
        }

        #endregion

        #region Timers Tickers

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

            try
            {
                //Chama a atulização da Manutenção
                VariaveisGlobais.manutencao.atualizaManutencao();

                #region Conexão

                LB_No_Connection.Dispatcher.Invoke(delegate { LB_No_Connection.Visibility = Visibility.Hidden; });
                REC_No_Connection.Dispatcher.Invoke(delegate { REC_No_Connection.Visibility = Visibility.Hidden; });

                if (Utilidades.VariaveisGlobais.CommunicationPLC.ConnectionStatus_GS == 1 || VariaveisGlobais.manutencao.TelaManutencaoAtiva_Get)
                {
                    LB_PLC_STOP.Dispatcher.Invoke(delegate { LB_PLC_STOP.Visibility = Visibility.Hidden; });
                    REC_PLC_STOP.Dispatcher.Invoke(delegate { REC_PLC_STOP.Visibility = Visibility.Hidden; });
                }
                else if (Utilidades.VariaveisGlobais.CommunicationPLC.ConnectionStatus_GS == 0)
                {
                    LB_PLC_STOP.Dispatcher.Invoke(delegate { LB_PLC_STOP.Visibility = Visibility.Visible; });
                    REC_PLC_STOP.Dispatcher.Invoke(delegate { REC_PLC_STOP.Visibility = Visibility.Visible; });
                }


                if (!Utilidades.VariaveisGlobais.CommunicationPLC.PLCConnected_GS && !VariaveisGlobais.manutencao.TelaManutencaoAtiva_Get)
                {
                    LB_No_Connection.Dispatcher.Invoke(delegate { LB_No_Connection.Visibility = Visibility.Visible; });
                    REC_No_Connection.Dispatcher.Invoke(delegate { REC_No_Connection.Visibility = Visibility.Visible; });

                    LB_PLC_STOP.Dispatcher.Invoke(delegate { LB_PLC_STOP.Visibility = Visibility.Hidden; });
                    REC_PLC_STOP.Dispatcher.Invoke(delegate { REC_PLC_STOP.Visibility = Visibility.Hidden; });
                }
                else
                {
                    LB_No_Connection.Dispatcher.Invoke(delegate { LB_No_Connection.Visibility = Visibility.Hidden; });
                    REC_No_Connection.Dispatcher.Invoke(delegate { REC_No_Connection.Visibility = Visibility.Hidden; });

                }
                #endregion

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
                //Verifica se está bloqueado ou não a balança.
                if (!VariaveisGlobais.balancaPrincipal.BloqueiaLeitura_GS)
                {
                    //Atualiza Balança
                    VariaveisGlobais.balancaPrincipal.LeituraModbus();

                    //Escreve o peso lido da balança
                    //Lembrando que -1 simboliza erro na leitura.
                    VariaveisGlobais.balancaPrincipal.EscritaCLP(2, 2);
                }

                ////Atualiza Dword Geral de auxiliares Processo.
                Utilidades.VariaveisGlobais.auxiliaresBooleanos = Move_Bits.DwordTocontroleAuxiliaresBooleanas(Comunicacao.Sharp7.S7.GetDWordAt(Utilidades.VariaveisGlobais.Buffer_PLC[0].Buffer, 126), Utilidades.VariaveisGlobais.auxiliaresBooleanos);

                //Atualzia SIlo
                VariaveisGlobais.Fluxo.AtualizaFluxo();

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

                controleDosagemProdutos();

                controleFinalizaProducao();
            }

            VariaveisGlobais.CommunicationPLC.writeBufferPLC();//Chama a escrita no PLC
        }
        #endregion

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

                if (spInical != null)
                {
                    spInical.Children.Clear();

                    spInical.Children.Add(Utilidades.VariaveisGlobais.Fluxo);
                    AtualizaButton(pckHome);
                }
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
                AtualizaButton(pckHome);
            }
        }

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

        private void btConfiguracoesUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (VariaveisGlobais.NumberOfGroup_GS == 0)
            {
                Utilidades.messageBox inputDialog = new Utilidades.messageBox(VariaveisGlobais.faltaUsuarioTitle, VariaveisGlobais.faltaUsuarioMessage, MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");
                inputDialog.ShowDialog();
            }
            else
            {
                spInical.Children.Clear();

                spInical.Children.Add(Utilidades.VariaveisGlobais.controleUsuario);

                Utilidades.VariaveisGlobais.controleUsuario.spControleUsuario.Children.Clear();

                AtualizaButton(pckUser);

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


        public void InicioDosagem()
        {
            Utilidades.VariaveisGlobais.Buffer_PLC[1].Enable_Read = false;

            

            Comunicacao.Sharp7.S7.SetWordAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 18, Utilidades.Move_Bits.ControleProducaoToWord(VariaveisGlobais.controleProducao));
            Comunicacao.Sharp7.S7.SetRealAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 26, VariaveisGlobais.controleProducao.PesoDosar);
            Comunicacao.Sharp7.S7.SetRealAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 30, VariaveisGlobais.controleProducao.PesoTolerancia);

            Utilidades.VariaveisGlobais.Buffer_PLC[1].Enable_Write = true;
        }

        /// <summary>
        /// Verifica se o produto que esta dosando foi dosado pelo PLC
        /// Verifica qual produto esta dosando e Controla a troca para o próximo produto.
        /// </summary>
        public void controleDosagemProdutos()
        {
            #region Verifica qual produto esta dosando e Controla a troca para o próximo produto

            //Verifica se tem OP na dosagem
            if (VariaveisGlobais.controleProducao.Producao0 > 0)
            {
                //Verifica o index que esta a OP
                VariaveisGlobais.controleProducao.indexProducao = Utilidades.VariaveisGlobais.OrdensProducao.FindIndex(x => x.id == VariaveisGlobais.controleProducao.Producao0);

                //Verifica se existe a OP no BD
                if (VariaveisGlobais.controleProducao.indexProducao != -1)
                {
                    if (VariaveisGlobais.controleProducao.IniciadoDosagemProduto_PLC && VariaveisGlobais.controleProducao.Troca_Produto)
                    {
                        VariaveisGlobais.controleProducao.Troca_Produto = false;
                        InicioDosagem();
                    }

                    //Verifica o produto que esta sendo produzido
                    int i = 0;
                    foreach (var produtos in Utilidades.VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos)
                    {
                        //Verifica o produto que não foi dosado
                        if (!produtos.finalizouDosagem)
                        {
                            //Atualiza index do produto 
                            VariaveisGlobais.controleProducao.indexProduto = i;

                            
                            if (VariaveisGlobais.controleProducao.Manual_Automatico)
                            {
                                if (produtos.iniciouDosagem)
                                {
                                    //Verifica caso o peso a dosar não esteja igual ao da receita, isso envia para o PLC novamente os pesos e solicita dosagem
                                    if (VariaveisGlobais.controleProducao.PesoDosar != VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].pesoProdutoDesejado ||
                                        VariaveisGlobais.controleProducao.PesoTolerancia != VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].tolerancia
                                        )
                                    {
                                        if (DataBase.SQLFunctionsProducao.Update_IniciouDosagemProduto(VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].id, VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].produto.id) > 0)
                                        {
                                            VariaveisGlobais.controleProducao.PesoDosar = VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].pesoProdutoDesejado;
                                            VariaveisGlobais.controleProducao.PesoTolerancia = VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].tolerancia;
                                            VariaveisGlobais.controleProducao.Dosando = true;
                                            VariaveisGlobais.controleProducao.Troca_Produto = true;
                                            VariaveisGlobais.controleProducao.IniciadoDosagemProduto_PLC = false;
                                            VariaveisGlobais.controleProducao.Estabilizado = false;
                                            InicioDosagem();
                                        }
                                    }

                                    //Finaliza Dosagem
                                    if (VariaveisGlobais.controleProducao.Estabilizado && VariaveisGlobais.controleProducao.IniciadoDosagemProduto_PLC)
                                    {
                                        if (DataBase.SQLFunctionsProducao.Update_FinalizouDosagemProduto(VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].id, VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].produto.id) > 0)
                                        {
                                            VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].finalizouDosagem = true;

                                            if (DataBase.SQLFunctionsProducao.Update_PesoDosado_Produto(VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].id, VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].produto.id, VariaveisGlobais.controleProducao.Peso_Parcial_Produzindo) > 0)
                                            {
                                                //Verifica se é o ultimo produto
                                                if (i + 1 == VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos.Count)
                                                {
                                                    DataBase.SQLFunctionsProducao.Update_PesoDosadoTotal(Utilidades.VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].id, VariaveisGlobais.controleProducao.Peso_Total_Produzindo);
                                                    Utilidades.VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].pesoTotalProduzido = VariaveisGlobais.controleProducao.Peso_Total_Produzindo;

                                                    VariaveisGlobais.controleProducao.PesoDosar = 0;
                                                    VariaveisGlobais.controleProducao.PesoTolerancia = 0;
                                                    VariaveisGlobais.controleProducao.Troca_Produto = false;
                                                    VariaveisGlobais.controleProducao.Dosando = false;
                                                    VariaveisGlobais.controleProducao.IniciadoDosagemProduto_PLC = false;
                                                    InicioDosagem();
                                                }

                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    //Verifica se não é o primeiro produto
                                    if (i -1 >=0)
                                    {
                                        if (DataBase.SQLFunctionsProducao.Update_IniciouDosagemProduto(VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].id, VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].produto.id) > 0)
                                        {
                                            VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].iniciouDosagem = true;

                                            VariaveisGlobais.controleProducao.Troca_Produto = true;
                                            VariaveisGlobais.controleProducao.IniciadoDosagemProduto_PLC = false;
                                            VariaveisGlobais.controleProducao.Estabilizado = false;
                                            VariaveisGlobais.controleProducao.PesoDosar = VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].pesoProdutoDesejado;
                                            VariaveisGlobais.controleProducao.PesoTolerancia = VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].tolerancia;

                                            InicioDosagem();
                                        }
                                    }
                                    else //Se for o primeiro produto
                                    {
                                        if (DataBase.SQLFunctionsProducao.Update_IniciouDosagemProduto(VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].id, VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].produto.id) > 0)
                                        {
                                            VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].iniciouDosagem = true;
                                            VariaveisGlobais.controleProducao.Dosando = true;
                                            VariaveisGlobais.controleProducao.Troca_Produto = false;
                                            VariaveisGlobais.controleProducao.IniciadoDosagemProduto_PLC = true;
                                            VariaveisGlobais.controleProducao.PesoDosar = VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].pesoProdutoDesejado;
                                            VariaveisGlobais.controleProducao.PesoTolerancia = VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].tolerancia;

                                            InicioDosagem();
                                        }
                                    }
                                }

                                VariaveisGlobais.controleProducao.EncerrarDosagem = false;
                                VariaveisGlobais.controleProducao.HabilitadoDosarEmManual = false;
                                VariaveisGlobais.controleProducao.primeiroProdutoDosar = false;
                            }
                            else
                            {

                                //Troca produto
                                if (VariaveisGlobais.controleProducao.Estabilizado)
                                {
                                    VariaveisGlobais.controleProducao.HabilitadoDosarEmManual = true;}
                                else
                                {
                                    VariaveisGlobais.controleProducao.HabilitadoDosarEmManual = false;
                                }

                                //Primeiro produto a Dosar
                                if (i == 0 && !VariaveisGlobais.controleProducao.Dosando)
                                {
                                    VariaveisGlobais.controleProducao.primeiroProdutoDosar = true;
                                    VariaveisGlobais.controleProducao.HabilitadoDosarEmManual = true;
                                }

                                //Finalizada dosagem da produção
                                if ((i + 1) == Utilidades.VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos.Count &&
                                    VariaveisGlobais.controleProducao.Estabilizado)
                                {
                                    VariaveisGlobais.controleProducao.EncerrarDosagem = true;
                                    VariaveisGlobais.controleProducao.HabilitadoDosarEmManual = true;
                                }

                            }

                            VariaveisGlobais.OrdensProducao[VariaveisGlobais.controleProducao.indexProducao].receita.listProdutos[VariaveisGlobais.controleProducao.indexProduto].pesoProdutoDosado = VariaveisGlobais.controleProducao.Peso_Parcial_Produzindo;

                            break;
                        }

                        i++;
                    }
                }
            }
            else
            {
                VariaveisGlobais.controleProducao.EncerrarDosagem = false;
                VariaveisGlobais.controleProducao.HabilitadoDosarEmManual = false;
                VariaveisGlobais.controleProducao.primeiroProdutoDosar = false;
                VariaveisGlobais.controleProducao.indexProduto = -1;
                VariaveisGlobais.controleProducao.indexProdutoOld = -1;
            }

            #endregion
        }

        public void controleFinalizaProducao()
        {
            if (VariaveisGlobais.controleProducao.Producao3 > 0)
            {
                //Verifica o index que esta a OP
                int indexProducaoFinalizar = Utilidades.VariaveisGlobais.OrdensProducao.FindIndex(x => x.id == VariaveisGlobais.controleProducao.Producao3);

                //Verifica se existe a OP no BD
                if (indexProducaoFinalizar != -1)
                {
                    //Atualiza no BD que finalizou a produção
                    DataBase.SQLFunctionsProducao.Update_FinalizaProducao(Utilidades.VariaveisGlobais.OrdensProducao[indexProducaoFinalizar].id);

                    //Retira da lista de ordens de produção a produção
                    Utilidades.VariaveisGlobais.OrdensProducao.RemoveAt(indexProducaoFinalizar);

                    //Solicita atualizar as ordens de produção de acordo com o BD
                    DataBase.SQLFunctionsProducao.AtualizaOrdemProducaoEmExecucao();

                    Utilidades.VariaveisGlobais.Buffer_PLC[1].Enable_Read = false;

                    Comunicacao.Sharp7.S7.SetDIntAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 14, -1);

                    Utilidades.VariaveisGlobais.Buffer_PLC[1].Enable_Write = true;
                }
                else
                {
                    Utilidades.VariaveisGlobais.Buffer_PLC[1].Enable_Read = false;

                    Comunicacao.Sharp7.S7.SetDIntAt(Utilidades.VariaveisGlobais.Buffer_PLC[1].Buffer, 14, -1);

                    Utilidades.VariaveisGlobais.Buffer_PLC[1].Enable_Write = true;
                }

            }
        }

        #endregion


    }
}
