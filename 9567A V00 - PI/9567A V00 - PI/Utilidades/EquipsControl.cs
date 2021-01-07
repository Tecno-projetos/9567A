using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace _9567A_V00___PI.Utilidades
{
    public class EquipsControl
    {

        #region Variables 

        Partidas.Principal.principalPartidaDireta Window_PD;
        Partidas.Principal.principalControleInversor Window_INV;
        Partidas.Principal.principalControleMoinho Window_SS;
        Partidas.Principal.principalControleAtuadorAnalogico Window_AtuadorA;
        Partidas.Principal.principalControleAtuadorLinear Window_AtuadorD;
        Partidas.Principal.principalControleAtuadorLinearBifurcada Window_BF;

        SolidColorBrush Verde = new SolidColorBrush(Color.FromRgb(0, 140, 0));
        SolidColorBrush Laranja = new SolidColorBrush(Color.FromRgb(250, 150, 0));
        SolidColorBrush Cinza = new SolidColorBrush(Color.FromRgb(150, 150, 150));

        //Palavra de comando geral
        Utilidades.VariaveisGlobais.type_All Command = new Utilidades.VariaveisGlobais.type_All("");


        #region Alarmes
        private bool Aux_Tempo_Manutencao = false;
        private bool Aux_Contator_Nao_Ligou = false;
        private bool Aux_Partida_Desligou = false;
        private bool Aux_Partida_Nao_Desligou = false;
        private bool Aux_Falha_Leitura_Corrente_Entrada_Analógica = false;
        private bool Aux_FalhaLado1 = false;
        private bool Aux_FalhaLado2 = false;
        private bool Aux_Falha2PosicoesAtiva = false;
        private bool Aux_FalhaPosicionamento = false;
        private bool Aux_falhaLeituraPosicao = false;

        #endregion

        #endregion

        public EquipsControl(typeEquip Equip, typeCommand TCommand, int initialOffSet, int bufferPlc, string nome, string tag, string numeroPartida, string paginaProjeto)
        {
            Command.initialOffSet = initialOffSet;
            Command.bufferPlc = bufferPlc;
            Command.TypeEquip = Equip;
            Command.TypeCommand = TCommand;
            Command.Nome = nome;
            Command.Tag = tag;
            Command.NumeroPartida = numeroPartida;
            Command.PaginaProjeto = paginaProjeto;

            //==========================================================================================
            //Inicia a tela dependendo o tipo de motor.

            //PD
            if (Equip == typeEquip.PD)
            {
                Window_PD = new Partidas.Principal.principalPartidaDireta();
            }
            //INV
            else if (Equip == typeEquip.INV)
            {
                Window_INV = new Partidas.Principal.principalControleInversor();
            }
            //SS
            else if (Equip == typeEquip.SS)
            {
                Window_SS = new Partidas.Principal.principalControleMoinho();
            }
            //Atuador
            else if (Equip == typeEquip.Atuador)
            {
                if (TCommand == typeCommand.Atuador_Digital)
                {
                    Window_AtuadorD = new Partidas.Principal.principalControleAtuadorLinear();

                }
                else if(TCommand == typeCommand.Atuador_Analogico)
                {
                    Window_AtuadorA = new Partidas.Principal.principalControleAtuadorAnalogico();
                }

            }
            //BF
            else if (Equip == typeEquip.BF)
            {
                Window_BF = new Partidas.Principal.principalControleAtuadorLinearBifurcada();
            }

        }

        public void Generate_Faults_DB(Utilidades.VariaveisGlobais.type_All Command, string UserLogged, string GroupUserLogged)
        {
            //========================================================================================================================================================================================================================================

            if (Command.Standard.Tempo_Manutencao)
            {
                if (!Aux_Tempo_Manutencao)
                {
                    if (!DataBase.SqlFunctionsEquips.GetBoolLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Tempo de Manutenção do Motor Atingido"))
                    {
                        DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Tempo de Manutenção do Motor Atingido", false, true, 1, false, "", UserLogged, GroupUserLogged, DateTime.Now.ToString(), "", "", DateTime.Now, "");
                    }

                    Aux_Tempo_Manutencao = true;
                }
            }
            else
            {
                if (Aux_Tempo_Manutencao)
                {
                    if (DataBase.SqlFunctionsEquips.GetBoolLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Tempo de Manutenção do Motor Atingido"))
                    {
                        DataBase.SqlFunctionsEquips.UpdateActiveFalse_Table_EquipAlarmEvent("_" + Command.Tag, DataBase.SqlFunctionsEquips.GetIntLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Tempo de Manutenção do Motor Atingido"));
                        Aux_Tempo_Manutencao = false;
                    }

                }
            }

            //========================================================================================================================================================================================================================================

            if (Command.Standard.Falha_Partida_Nao_Confirmou)
            {
                if (!Aux_Contator_Nao_Ligou)
                {
                    if (!DataBase.SqlFunctionsEquips.GetBoolLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha Contator Não Ligou"))
                    {
                        DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Falha Contator Não Ligou", false, true, 1, false, "", UserLogged, GroupUserLogged, DateTime.Now.ToString(), "", "", DateTime.Now, "");

                    }

                    Aux_Contator_Nao_Ligou = true;
                }

            }
            else
            {
                if (Aux_Contator_Nao_Ligou)
                {
                    if (DataBase.SqlFunctionsEquips.GetBoolLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha Contator Não Ligou"))
                    {
                        DataBase.SqlFunctionsEquips.UpdateActiveFalse_Table_EquipAlarmEvent("_" + Command.Tag, DataBase.SqlFunctionsEquips.GetIntLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha Contator Não Ligou"));
                        Aux_Contator_Nao_Ligou = false;
                    }


                }
            }

            //========================================================================================================================================================================================================================================
            if (Command.Standard.Falha_Disjuntor_Desligou || Command.Standard.Falha_Contator_Desligou)
            {
                if (!Aux_Partida_Desligou)
                {
                    if (!DataBase.SqlFunctionsEquips.GetBoolLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha Partida Desligou"))
                    {
                        DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Falha Partida Desligou", false, true, 1, false, "", UserLogged, GroupUserLogged, DateTime.Now.ToString(), "", "", DateTime.Now, "");

                    }

                    Aux_Partida_Desligou = true;
                }
            }
            else
            {
                if (Aux_Partida_Desligou)
                {
                    if (DataBase.SqlFunctionsEquips.GetBoolLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha Partida Desligou"))
                    {
                        DataBase.SqlFunctionsEquips.UpdateActiveFalse_Table_EquipAlarmEvent("_" + Command.Tag, DataBase.SqlFunctionsEquips.GetIntLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha Partida Desligou"));

                        Aux_Partida_Desligou = false;
                    }
                }

            }
            //========================================================================================================================================================================================================================================

            if (Command.Standard.Falha_Partida_Nao_Desligou)
            {
                if (!Aux_Partida_Nao_Desligou)
                {
                    if (!DataBase.SqlFunctionsEquips.GetBoolLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha Contator Não Desligou"))
                    {
                        DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Falha Contator Não Desligou", false, true, 1, false, "", UserLogged, GroupUserLogged, DateTime.Now.ToString(), "", "", DateTime.Now, "");

                    }

                    Aux_Partida_Nao_Desligou = true;
                }

            }
            else
            {
                if (Aux_Partida_Nao_Desligou)
                {
                    if (DataBase.SqlFunctionsEquips.GetBoolLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha Contator Não Desligou"))
                    {
                        DataBase.SqlFunctionsEquips.UpdateActiveFalse_Table_EquipAlarmEvent("_" + Command.Tag, DataBase.SqlFunctionsEquips.GetIntLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha Contator Não Desligou"));

                        Aux_Partida_Nao_Desligou = false;
                    }
                }

            }

            //========================================================================================================================================================================================================================================

            if (Command.Standard.FalhaAcionandoLado1 || Command.Standard.FalhaConfirmacaoContatorLado1)
            {
                if (!Aux_FalhaLado1)
                {
                    if (!DataBase.SqlFunctionsEquips.GetBoolLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha acionar Esquerda"))
                    {
                        DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Falha acionar Esquerda", false, true, 1, false, "", UserLogged, GroupUserLogged, DateTime.Now.ToString(), "", "", DateTime.Now, "");

                    }

                    Aux_FalhaLado1 = true;
                }
            }
            else
            {
                if (Aux_FalhaLado1)
                {
                    if (DataBase.SqlFunctionsEquips.GetBoolLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha acionar Esquerda"))
                    {
                        DataBase.SqlFunctionsEquips.UpdateActiveFalse_Table_EquipAlarmEvent("_" + Command.Tag, DataBase.SqlFunctionsEquips.GetIntLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha acionar Esquerda"));
                    }

                    Aux_FalhaLado1 = false;
                }

            }

            //========================================================================================================================================================================================================================================

            if (Command.Standard.FalhaAcionandoLado2 || Command.Standard.FalhaConfirmacaoContatorLado2)
            {
                if (!Aux_FalhaLado2)
                {
                    if (!DataBase.SqlFunctionsEquips.GetBoolLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha acionar Direita"))
                    {
                        DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Falha acionar Direita", false, true, 1, false, "", UserLogged, GroupUserLogged, DateTime.Now.ToString(), "", "", DateTime.Now, "");

                    }

                    Aux_FalhaLado2 = true;
                }
            }
            else
            {
                if (Aux_FalhaLado2)
                {
                    if (DataBase.SqlFunctionsEquips.GetBoolLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha acionar Direita"))
                    {
                        DataBase.SqlFunctionsEquips.UpdateActiveFalse_Table_EquipAlarmEvent("_" + Command.Tag, DataBase.SqlFunctionsEquips.GetIntLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha acionar Direita"));
                    }

                    Aux_FalhaLado2 = false;
                }

            }

            //========================================================================================================================================================================================================================================

            if (Command.Standard.Falha2PosicoesAtiva)
            {
                if (!Aux_Falha2PosicoesAtiva)
                {
                    if (!DataBase.SqlFunctionsEquips.GetBoolLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha sensores Ativos"))
                    {
                        DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Falha sensores Ativos", false, true, 1, false, "", UserLogged, GroupUserLogged, DateTime.Now.ToString(), "", "", DateTime.Now, "");

                    }

                    Aux_Falha2PosicoesAtiva = true;
                }
            }
            else
            {
                if (Aux_Falha2PosicoesAtiva)
                {
                    if (DataBase.SqlFunctionsEquips.GetBoolLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha sensores Ativos"))
                    {
                        DataBase.SqlFunctionsEquips.UpdateActiveFalse_Table_EquipAlarmEvent("_" + Command.Tag, DataBase.SqlFunctionsEquips.GetIntLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha sensores Ativos"));
                    }

                    Aux_Falha2PosicoesAtiva = false;
                }

            }

            //========================================================================================================================================================================================================================================

            if (Command.Standard.falhaPosicionamento)
            {
                if (!Aux_FalhaPosicionamento)
                {
                    if (!DataBase.SqlFunctionsEquips.GetBoolLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha posicionar Atuador"))
                    {
                        DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Falha posicionar Atuador", false, true, 1, false, "", UserLogged, GroupUserLogged, DateTime.Now.ToString(), "", "", DateTime.Now, "");

                    }

                    Aux_FalhaPosicionamento = true;
                }
            }
            else
            {
                if (Aux_FalhaPosicionamento)
                {
                    if (DataBase.SqlFunctionsEquips.GetBoolLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha posicionar Atuador"))
                    {
                        DataBase.SqlFunctionsEquips.UpdateActiveFalse_Table_EquipAlarmEvent("_" + Command.Tag, DataBase.SqlFunctionsEquips.GetIntLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha posicionar Atuador"));
                    }

                    Aux_FalhaPosicionamento = false;
                }

            }

            //========================================================================================================================================================================================================================================


            if (Command.Standard.falhaLeituraPosicao)
            {
                if (!Aux_falhaLeituraPosicao)
                {
                    if (!DataBase.SqlFunctionsEquips.GetBoolLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha leitura posição Atuador"))
                    {
                        DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Falha leitura posição Atuador", false, true, 1, false, "", UserLogged, GroupUserLogged, DateTime.Now.ToString(), "", "", DateTime.Now, "");

                    }

                    Aux_falhaLeituraPosicao = true;
                }
            }
            else
            {
                if (Aux_falhaLeituraPosicao)
                {
                    if (DataBase.SqlFunctionsEquips.GetBoolLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha leitura posição Atuador"))
                    {
                        DataBase.SqlFunctionsEquips.UpdateActiveFalse_Table_EquipAlarmEvent("_" + Command.Tag, DataBase.SqlFunctionsEquips.GetIntLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha leitura posição Atuador"));
                    }

                    Aux_falhaLeituraPosicao = false;
                }

            }

            //========================================================================================================================================================================================================================================
            if (Command.SS.Corrente_Atual == 32767 || Command.INV.Corrente_Atual == 32767)
            {
                if (!Aux_Falha_Leitura_Corrente_Entrada_Analógica)
                {
                    if (!DataBase.SqlFunctionsEquips.GetBoolLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha Leitura Corrente Entrada Analógica"))
                    {
                        DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Falha Leitura Corrente Entrada Analógica", false, true, 1, false, "", UserLogged, GroupUserLogged, DateTime.Now.ToString(), "", "", DateTime.Now, "");
                    }


                    Aux_Falha_Leitura_Corrente_Entrada_Analógica = true;
                }
            }
            else
            {
                if (Aux_Falha_Leitura_Corrente_Entrada_Analógica)
                {
                    if (DataBase.SqlFunctionsEquips.GetBoolLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha Leitura Corrente Entrada Analógica"))
                    {
                        DataBase.SqlFunctionsEquips.UpdateActiveFalse_Table_EquipAlarmEvent("_" + Command.Tag, DataBase.SqlFunctionsEquips.GetIntLastRowActive_Table_EquipAlarmEvent("_" + Command.Tag, "Falha Leitura Corrente Entrada Analógica"));
                    }

                    Aux_Falha_Leitura_Corrente_Entrada_Analógica = false;
                }

            }
            //========================================================================================================================================================================================================================================
        }

        #region Events Window

        private void Window_PD_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Window_PD.Hide();
        }

        private void Window_INV_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

            Window_INV.Hide();
        }

        private void Window_SS_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

            Window_SS.Hide();
        }

        private void Window_AtuadorD_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

            Window_AtuadorD.Hide();
        }

        private void Window_AtuadorA_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;

            Window_AtuadorA.Hide();
        }


        private void Window_AtuadorBF_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;

            Window_BF.Hide();
        }

        #endregion

        #region Events Screens

        //Atuador Bifurcada
        private void AtuadorBF_Bt_Fechar_Click(object sender, EventArgs e)
        {
            Window_BF.DialogResult = true;
        }

        private void AtuadorBF_Bt_Manual_Click(object sender, EventArgs e)
        {
            invertBitManual();
        }

        private void AtuadorBF_Bt_Manutencao_Click(object sender, EventArgs e)
        {
            invertBitManutencao();
        }

        private void AtuadorBF_Bt_Libera_Click(object sender, EventArgs e)
        {
            invertBitLibera();
        }

        private void AtuadorBF_Bt_Reset_Click(object sender, EventArgs e)
        {
            setBitReset();
        }

        private void AtuadorBF_Bt_LigarEsquerda_Click(object sender, EventArgs e)
        {
            if (Command.Standard.AcionaLado1 || (Command.Standard.EmPosicaoLado1 & !Command.Standard.AcionaLado2))
            {
                Command.Standard.AcionaLado1 = false;
                Command.Standard.AcionaLado2 = true;
            }
            else if (Command.Standard.AcionaLado2 || (Command.Standard.EmPosicaoLado2 & !Command.Standard.AcionaLado1))
            {
                Command.Standard.AcionaLado2 = false;
                Command.Standard.AcionaLado1 = true;
            }
            else
            {
                Command.Standard.AcionaLado1 = true;
            }

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeAtuadorD_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeAtuadorD(Command)));

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;

            if (Command.Standard.AcionaLado1)
            {
                DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Comando Abrir Esquerda", true, false, 1, false, "", Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS, DateTime.Now.ToString(), "", "", DateTime.Now, "");
            }
            else
            {
                DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Comando Fechar Esquerda", true, false, 1, false, "", Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS, DateTime.Now.ToString(), "", "", DateTime.Now, "");

            }
        }

        private void AtuadorBF_Bt_LigarDireita_Click(object sender, EventArgs e)
        {

            if (Command.Standard.AcionaLado1 || (Command.Standard.EmPosicaoLado1 & !Command.Standard.AcionaLado2))
            {
                Command.Standard.AcionaLado1 = false;
                Command.Standard.AcionaLado2 = true;
            }
            else if (Command.Standard.AcionaLado2 || (Command.Standard.EmPosicaoLado2 & !Command.Standard.AcionaLado1))
            {
                Command.Standard.AcionaLado2 = false;
                Command.Standard.AcionaLado1 = true;
            }
            else
            {
                Command.Standard.AcionaLado2 = true;
            }



            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeAtuadorD_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeAtuadorD(Command)));

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;

            if (Command.Standard.AcionaLado2)
            {
                DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Comando Abrir Direita", true, false, 1, false, "", Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS, DateTime.Now.ToString(), "", "", DateTime.Now, "");
            }
            else
            {
                DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Comando Fechar Direita", true, false, 1, false, "", Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS, DateTime.Now.ToString(), "", "", DateTime.Now, "");

            }
        }


        //Atuador Analógico 
        private void AtuadorA_Bt_Ligar_Click(object sender, EventArgs e)
        {
            if (Command.Standard.Liga_Manual )
            {
                Command.Standard.Liga_Manual = false;
            }
            else
            {
                Command.Standard.Liga_Manual = true;
            }

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeAtuadorA_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeAtuadorA(Command)));

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;

            if (Command.Standard.Liga_Manual)
            {
                DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Comando Abrir", true, false, 1, false, "", Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS, DateTime.Now.ToString(), "", "", DateTime.Now, "");
            }
            else
            {
                DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Comando Fechar", true, false, 1, false, "", Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS, DateTime.Now.ToString(), "", "", DateTime.Now, "");

            }

        }

        private void AtuadorA_Bt_Manual_Click(object sender, EventArgs e)
        {
            invertBitManual();
        }

        private void AtuadorA_Bt_Manutencao_Click(object sender, EventArgs e)
        {
            invertBitManutencao();
        }

        private void AtuadorA_Bt_Libera_Click(object sender, EventArgs e)
        {
            invertBitLibera();
        }

        private void AtuadorA_Bt_Reset_Click(object sender, EventArgs e)
        {
            setBitReset();
        }

        private void AtuadorA_AtulizaPosicao(object sender, EventArgs e)
        {
            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            Comunicacao.Sharp7.S7.SetRealAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.AtuadorA.offSet_SP_Posicao_Manual, Convert.ToSingle(Window_AtuadorA.controleAtuadorAnalogico.PosicaoSolicitada_GS));

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;
        }

        private void AtuadorA_Bt_Fechar_Click(object sender, EventArgs e)
        {
            Window_AtuadorA.DialogResult = true;
        }

        //PD
        protected void PD_Bt_Ligar_Click(object sender, EventArgs e)
        {
            invertBitLigar();
        }

        protected void PD_Bt_Reset_Click(object sender, EventArgs e)
        {
            setBitReset();
        }

        protected void PD_Bt_Libera_Click(object sender, EventArgs e)
        {
            invertBitLibera();
        }

        protected void PD_Bt_Manutencao_Click(object sender, EventArgs e)
        {
            invertBitManutencao();
        }

        protected void PD_Bt_Manual_Click(object sender, EventArgs e)
        {
            invertBitManual();
        }

        private void PD_resetTotal_Click(object sender, EventArgs e)
        {
            setBitResetHorimetroTotal();
        }

        private void PD_resetParcial_Click(object sender, EventArgs e)
        {
            setBitResetHorimetroParcial();
        }
        private void PD_atualizaSPManutencao_Event(object sender, EventArgs e)
        {
            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            Comunicacao.Sharp7.S7.SetDIntAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.PD.offSet_SP_Manutencao, Convert.ToInt32(Window_PD.configuracoesPD.SpManutencao));

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;
        }

        private void PD_atualizaSPLimepza_Event(object sender, EventArgs e)
        {
            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            Comunicacao.Sharp7.S7.SetDIntAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.PD.offSet_Tempo_Limpeza, Convert.ToInt32(Window_PD.configuracoesPD.SpLimpeza));

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;
        }

        protected void PD_Bt_Fechar_Click(object sender, EventArgs e)
        {
            Window_PD.DialogResult = true;
        }

        //INV
        protected void INV_Bt_Ligar_Click(object sender, EventArgs e)
        {
            invertBitLigar();
        }

        protected void INV_Bt_Reset_Click(object sender, EventArgs e)
        {
            setBitReset();
        }

        protected void INV_Bt_Libera_Click(object sender, EventArgs e)
        {
            invertBitLibera();
        }

        protected void INV_Bt_Manutencao_Click(object sender, EventArgs e)
        {
            invertBitManutencao();
        }

        protected void INV_Bt_Manual_Click(object sender, EventArgs e)
        {
            invertBitManual();
        }

        private void INV_resetTotal_Click(object sender, EventArgs e)
        {
            setBitResetHorimetroTotal();
        }

        private void INV_resetParcial_Click(object sender, EventArgs e)
        {
            setBitResetHorimetroParcial();
        }
        private void INV_atualizaSPManutencao_Event(object sender, EventArgs e)
        {
            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            Comunicacao.Sharp7.S7.SetDIntAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.INV.offSet_SP_Manutencao, Convert.ToInt32(Window_INV.configuracoesINV.SpManutencao));

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;
        }

        private void INV_atualizaSPLimepza_Event(object sender, EventArgs e)
        {
            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            Comunicacao.Sharp7.S7.SetDIntAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.INV.offSet_Tempo_Limpeza, Convert.ToInt32(Window_INV.configuracoesINV.SpLimpeza));

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;
        }

        private void INV_atualizaSPMotorVazio_Event(object sender, EventArgs e)
        {
            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            Comunicacao.Sharp7.S7.SetRealAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.INV.offSet_SP_Corrente_Motor_Vazio, Convert.ToSingle(Window_INV.configuracoesINV.SpMotorVazio));

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;
        }



        protected void INV_Bt_Fechar_Click(object sender, EventArgs e)
        {
            Window_INV.DialogResult = true;
        }

        private void INV_atualiza_Velocidade(object sender, EventArgs e)
        {
            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            Comunicacao.Sharp7.S7.SetRealAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + 4, Convert.ToSingle(Window_INV.controleINV.velocidadeManual_GS));

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;
        }

        //SS
        protected void SS_Bt_Ligar_Click(object sender, EventArgs e)
        {
            invertBitLigar();
        }

        protected void SS_Bt_Reset_Click(object sender, EventArgs e)
        {
            setBitReset();
        }

        protected void SS_Bt_Libera_Click(object sender, EventArgs e)
        {
            invertBitLibera();
        }

        protected void SS_Bt_Manutencao_Click(object sender, EventArgs e)
        {
            invertBitManutencao();
        }

        protected void SS_Bt_Manual_Click(object sender, EventArgs e)
        {
            invertBitManual();
        }

        protected void SS_Bt_InverterSentido_Click(object sender, EventArgs e)
        {
            if (Command.Standard.inverterSentidoGiro)
            {
                Command.Standard.inverterSentidoGiro = false;
            }
            else
            {
                Command.Standard.inverterSentidoGiro = true;
            }

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            if (Command_Get.TypeEquip == typeEquip.SS && Command_Get.TypeCommand == typeCommand.SS)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeSS_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeSS(Command)));
            }

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;

            if (Command.Standard.inverterSentidoGiro)
            {
                DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Comando Sentido Horário", true, false, 1, false, "", Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS, DateTime.Now.ToString(), "", "", DateTime.Now, "");
            }
            else
            {
                DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Comando Sentido Anti-Horário", true, false, 1, false, "", Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS, DateTime.Now.ToString(), "", "", DateTime.Now, "");
            }
        }

        protected void SS_Bt_Fechar_Click(object sender, EventArgs e)
        {
            Window_SS.DialogResult = true;
        }

        protected void SS_Bt_atualizarCorrenteVazio_Click(object sender, EventArgs e)
        {
            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            Comunicacao.Sharp7.S7.SetRealAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.SS.offSet_SP_Corrente_Motor_Vazio, Convert.ToSingle(Window_SS.configuracoesMOINHO.SpMotorVazio));

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;
        }

        protected void SS_Bt_atualizarTempoReversao_Click(object sender, EventArgs e)
        {
            //VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            //Comunicacao.Sharp7.S7.SetDIntAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.SS.offSet_SP_Tempo_Reversao, Convert.ToInt32(Window_SS.configuracoesMOINHO.SpTempoReversao));

            //VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;
        }

        private void SS_Bt_atualizarSPManutencao_Click(object sender, EventArgs e)
        {
            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            Comunicacao.Sharp7.S7.SetDIntAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.SS.offSet_SP_Manutencao, Convert.ToInt32(Window_SS.configuracoesMOINHO.SpManutencao));

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;
        }

        private void SS_Bt_atualizarSPLimpeza_Click(object sender, EventArgs e)
        {
            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            Comunicacao.Sharp7.S7.SetDIntAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.SS.offSet_Tempo_Limpeza, Convert.ToInt32(Window_SS.configuracoesMOINHO.SpLimpeza));

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;
        }

        

        private void SS_Bt_Confirma_Sentido_Alimentador_Moinho_Click(object sender, EventArgs e)
        {
            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            Command.Standard.confirmaSentidoGiroAlimentador = true;

            Command.Standard.SentidoGiroMotorMudou = false;

            Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeSS_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeSS(Command)));

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;
        }

        //Atuador Digital
        protected void AtuadorD_Bt_Ligar_Click(object sender, EventArgs e)
        {
            if (Command.Standard.AcionaLado1 || (Command.Standard.EmPosicaoLado1 & !Command.Standard.AcionaLado2))
            {
                Command.Standard.AcionaLado1 = false;
                Command.Standard.AcionaLado2 = true;
            }
            else if (Command.Standard.AcionaLado2 || (Command.Standard.EmPosicaoLado2 & !Command.Standard.AcionaLado1))
            {
                Command.Standard.AcionaLado2 = false;
                Command.Standard.AcionaLado1 = true;
            }
            else
            {
                Command.Standard.AcionaLado1 = true;
            }

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeAtuadorD_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeAtuadorD(Command)));
            
            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;

            if (Command.Standard.AcionaLado1)
            {
                DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Comando Abrir", true, false, 1, false, "", Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS, DateTime.Now.ToString(), "", "", DateTime.Now, "");
            }

            if (Command.Standard.AcionaLado2)
            {
                DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Comando Fechar", true, false, 1, false, "", Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS, DateTime.Now.ToString(), "", "", DateTime.Now, "");
            }
        }

        protected void AtuadorD_Bt_Reset_Click(object sender, EventArgs e)
        {
            setBitReset();
        }

        protected void AtuadorD_Bt_Libera_Click(object sender, EventArgs e)
        {
            invertBitLibera();
        }

        protected void AtuadorD_Bt_Manutencao_Click(object sender, EventArgs e)
        {
            invertBitManutencao();
        }

        protected void AtuadorD_Bt_Manual_Click(object sender, EventArgs e)
        {
            invertBitManual();
        }

        protected void AtuadorD_Bt_Fechar_Click(object sender, EventArgs e)
        {
            Window_AtuadorD.DialogResult = true;
        }

        #endregion

        #region Functions

        private void invertBitLigar()
        {
            if (Command.Standard.Liga_Manual)
            {
                Command.Standard.Liga_Manual = false;
            }
            else
            {
                Command.Standard.Liga_Manual = true;
            }

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            if (Command_Get.TypeEquip == typeEquip.PD && Command_Get.TypeCommand == typeCommand.PD)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typePD_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typePD(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.INV && Command_Get.TypeCommand == typeCommand.INV)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeINV_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeINV(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.SS && Command_Get.TypeCommand == typeCommand.SS)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeSS_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeSS(Command)));
            }

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;

            if (Command.Standard.Liga_Manual)
            {
                DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Comando Ligar", true, false, 1, false, "", Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS, DateTime.Now.ToString(), "", "", DateTime.Now, "");
            }
            else
            {
                DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Comando Desligar", true, false, 1, false, "", Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS, DateTime.Now.ToString(), "", "", DateTime.Now, "");
            }
        }

        private void setBitReset()
        {
            Command.Standard.Reset = true;

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            if (Command_Get.TypeEquip == typeEquip.PD && Command_Get.TypeCommand == typeCommand.PD)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typePD_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typePD(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.INV && Command_Get.TypeCommand == typeCommand.INV)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeINV_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeINV(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.SS && Command_Get.TypeCommand == typeCommand.SS)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeSS_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeSS(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.Atuador && Command_Get.TypeCommand == typeCommand.Atuador_Digital)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeAtuadorD_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeAtuadorD(Command)));
            }

            else if (Command_Get.TypeEquip == typeEquip.Atuador && Command_Get.TypeCommand == typeCommand.Atuador_Analogico)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeAtuadorA_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeAtuadorA(Command)));
            }



            else if (Command_Get.TypeEquip == typeEquip.BF && Command_Get.TypeCommand == typeCommand.Atuador_Digital)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeAtuadorD_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeAtuadorD(Command)));
            }

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;

            if (Command.Standard.Reset)
            {
                DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Comando Reset Falha", true, false, 1, false, "", Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS, DateTime.Now.ToString(), "", "", DateTime.Now, "");
            }
        }

        private void invertBitLibera()
        {
            if (Command.Standard.Libera_Bloqueio)
            {
                Command.Standard.Libera_Bloqueio = false;
            }
            else
            {
                Command.Standard.Libera_Bloqueio = true;
            }

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            if (Command_Get.TypeEquip == typeEquip.PD && Command_Get.TypeCommand == typeCommand.PD)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typePD_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typePD(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.INV && Command_Get.TypeCommand == typeCommand.INV)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeINV_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeINV(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.SS && Command_Get.TypeCommand == typeCommand.SS)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeSS_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeSS(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.Atuador && Command_Get.TypeCommand == typeCommand.Atuador_Digital)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeAtuadorD_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeAtuadorD(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.Atuador && Command_Get.TypeCommand == typeCommand.Atuador_Analogico)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeAtuadorA_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeAtuadorA(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.BF && Command_Get.TypeCommand == typeCommand.Atuador_Digital)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeAtuadorD_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeAtuadorD(Command)));
            }
            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;

            if (Command.Standard.Libera_Bloqueio)
            {
                DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Comando Libera Bloqueio", true, false, 1, false, "", Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS, DateTime.Now.ToString(), "", "", DateTime.Now, "");
            }
        }

        private void invertBitManutencao()
        {
            //if (Utilidades.VariaveisGlobais.NumberOfGroup_GS < 2)
            //{
            //    TopMost.TopMostMessageBox.Show(Utilidades.VariaveisGlobais.faltaPermissaoMessage, Utilidades.VariaveisGlobais.faltaPermissaoTitle, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //}
            //else
            //{
            //    Solicita_Manutencao = true;
            //}
            if (Command.Standard.Manutencao)
            {
                Command.Standard.Manutencao = false;
            }
            else
            {
                Command.Standard.Manutencao = true;
            }

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            if (Command_Get.TypeEquip == typeEquip.PD && Command_Get.TypeCommand == typeCommand.PD)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typePD_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typePD(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.INV && Command_Get.TypeCommand == typeCommand.INV)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeINV_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeINV(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.SS && Command_Get.TypeCommand == typeCommand.SS)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeSS_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeSS(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.Atuador && Command_Get.TypeCommand == typeCommand.Atuador_Digital)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeAtuadorD_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeAtuadorD(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.Atuador && Command_Get.TypeCommand == typeCommand.Atuador_Analogico)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeAtuadorA_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeAtuadorA(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.BF && Command_Get.TypeCommand == typeCommand.Atuador_Digital)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeAtuadorD_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeAtuadorD(Command)));
            }
            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;

            if (Command.Standard.Manutencao)
            {
                DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Comando Sair Modo Manutenção", true, false, 1, false, "", Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS, DateTime.Now.ToString(), "", "", DateTime.Now, "");
            }
            else
            {
                DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Comando Ir Para Modo Manutenção", true, false, 1, false, "", Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS, DateTime.Now.ToString(), "", "", DateTime.Now, "");
            }
        }

        private void invertBitManual()
        {
            if (Command.Standard.Automatico)
            {
                Command.Standard.Automatico = false;
                Command.Standard.Manual = true;
            }
            else
            {
                Command.Standard.Automatico = true;
                Command.Standard.Manual = false;
            }

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            if (Command_Get.TypeEquip == typeEquip.PD && Command_Get.TypeCommand == typeCommand.PD)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typePD_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typePD(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.INV && Command_Get.TypeCommand == typeCommand.INV)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeINV_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeINV(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.SS && Command_Get.TypeCommand == typeCommand.SS)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeSS_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeSS(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.Atuador && Command_Get.TypeCommand == typeCommand.Atuador_Digital)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeAtuadorD_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeAtuadorD(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.Atuador && Command_Get.TypeCommand == typeCommand.Atuador_Analogico)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeAtuadorA_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeAtuadorA(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.BF && Command_Get.TypeCommand == typeCommand.Atuador_Digital)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeAtuadorD_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeAtuadorD(Command)));
            }

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;

            if (Command.Standard.Automatico)
            {
                DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Comando Ir Para Modo Automático", true, false, 1, false, "", Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS, DateTime.Now.ToString(), "", "", DateTime.Now, "");
            }
            else
            {
                DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Comando Ir Para Modo Manual", true, false, 1, false, "", Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS, DateTime.Now.ToString(), "", "", DateTime.Now, "");
            }
        }

        private void setBitResetHorimetroTotal()
        {
            Command.Standard.Reset_Timer_Total = true;

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            if (Command_Get.TypeEquip == typeEquip.PD && Command_Get.TypeCommand == typeCommand.PD)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typePD_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typePD(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.INV && Command_Get.TypeCommand == typeCommand.INV)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeINV_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeINV(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.SS && Command_Get.TypeCommand == typeCommand.SS)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeSS_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeSS(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.Atuador && Command_Get.TypeCommand == typeCommand.Atuador_Digital)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeAtuadorD_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeAtuadorD(Command)));
            }

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;

            if (Command.Standard.Reset_Timer_Total)
            {
                DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Comando Reset Horímetro Total", true, false, 1, false, "", Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS, DateTime.Now.ToString(), "", "", DateTime.Now, "");
            }
        }

        private void setBitResetHorimetroParcial()
        {
            Command.Standard.Reset_Timer = true;

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read = false;

            if (Command_Get.TypeEquip == typeEquip.PD && Command_Get.TypeCommand == typeCommand.PD)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typePD_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typePD(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.INV && Command_Get.TypeCommand == typeCommand.INV)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeINV_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeINV(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.SS && Command_Get.TypeCommand == typeCommand.SS)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeSS_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeSS(Command)));
            }
            else if (Command_Get.TypeEquip == typeEquip.Atuador && Command_Get.TypeCommand == typeCommand.Atuador_Digital)
            {
                Comunicacao.Sharp7.S7.SetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet, Utilidades.Move_Bits.typeAtuadorD_TO_Dword(Utilidades.Move_Bits.typeStandardGUI_TO_typeAtuadorD(Command)));
            }

            VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Write = true;

            if (Command.Standard.Reset_Timer_Total)
            {
                DataBase.SqlFunctionsEquips.IntoDate_Table_EquipAlarmEvent("_" + Command.Tag, "Comando Reset Horímetro Parcial", true, false, 1, false, "", Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS, DateTime.Now.ToString(), "", "", DateTime.Now, "");
            }
        }

        #endregion

        #region Encapulate Fields

        public bool actualize_Equip
        {
            set
            {
                //Leitura das Variaveis
                if (VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Enable_Read)
                {
                    if (Command.TypeEquip == typeEquip.PD && Command.TypeCommand == typeCommand.PD)
                    {
                        //Lendo variaveis do buffer do CLP
                        Command.DWord = Comunicacao.Sharp7.S7.GetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet);
                        Command.PD.SP_Manutencao = Comunicacao.Sharp7.S7.GetDIntAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.PD.offSet_SP_Manutencao);
                        Command.PD.HorimetroParcial = Comunicacao.Sharp7.S7.GetDIntAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.PD.offSet_HorimetroParcial);
                        Command.PD.HorimetroTotal = Comunicacao.Sharp7.S7.GetDIntAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.PD.offSet_HorimetroTotal);
                        Command.PD.Tempo_Limpeza = Comunicacao.Sharp7.S7.GetDIntAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.PD.offSet_Tempo_Limpeza);

                        //Atualizando as variaveis para o standard GUI

                        //Primeiro converte a Dword para os bits
                        Command = Utilidades.Move_Bits.Dword_TO_typePD(Command.DWord, Command);

                        //Segundo atualiza o standard GUI
                        Command = Utilidades.Move_Bits.typePD_TO_typeStandardGUI(Command);


                        Generate_Faults_DB(Command, Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS);

                    }
                    else if (Command_Get.TypeEquip == typeEquip.INV && Command_Get.TypeCommand == typeCommand.INV)
                    {
                        //Lendo variaveis do buffer do CLP
                        Command.DWord = Comunicacao.Sharp7.S7.GetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet);




                        Command.INV.Velocidade_Manual = (float)Math.Round(Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.INV.offSet_Velocidade_Manual), 2);
                        Command.INV.Velocidade_Automatica_Solicita = (float)Math.Round(Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.INV.offSet_Velocidade_Automatica_Solicita), 2);
                        Command.INV.Velocidade_Atual = (float)Math.Round(Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.INV.offSet_Velocidade_Atual), 0);
                        Command.INV.Velocidade_Nominal = (float)Math.Round(Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.INV.offSet_Velocidade_Nominal), 2);
                        Command.INV.SP_Corrente_Motor_Vazio = (float)Math.Round(Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.INV.offSet_SP_Corrente_Motor_Vazio), 2);
                        Command.INV.Corrente_Atual = (float)Math.Round(Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.INV.offSet_Corrente_Atual), 2);
                        Command.INV.Codigo_Alarme = (float)Math.Round(Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.INV.offSet_Codigo_Alarme), 2);
                        Command.INV.Codigo_Falha = (float)Math.Round(Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.INV.offSet_Codigo_Falha), 2);

                        Command.INV.SP_Manutencao = Comunicacao.Sharp7.S7.GetDIntAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.INV.offSet_SP_Manutencao);
                        Command.INV.HorimetroParcial = Comunicacao.Sharp7.S7.GetDIntAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.INV.offSet_HorimetroParcial);
                        Command.INV.HorimetroTotal = Comunicacao.Sharp7.S7.GetDIntAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.INV.offSet_HorimetroTotal);
                        Command.INV.Tempo_Limpeza = Comunicacao.Sharp7.S7.GetDIntAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.INV.offSet_Tempo_Limpeza);

                        //Atualizando as variaveis para o standard GUI

                        //Primeiro converte a Dword para os bits
                        Command = Utilidades.Move_Bits.Dword_TO_typeINV(Command.DWord, Command);

                        //Segundo atualiza o standard GUI
                        Command = Utilidades.Move_Bits.typeINV_TO_typeStandardGUI(Command);
                        Generate_Faults_DB(Command, Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS);
                    }
                    else if (Command_Get.TypeEquip == typeEquip.SS && Command_Get.TypeCommand == typeCommand.SS)
                    {
                        //Lendo variaveis do buffer do CLP
                        Command.DWord = Comunicacao.Sharp7.S7.GetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet);
                        Command.SS.SP_Manutencao = Comunicacao.Sharp7.S7.GetDIntAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.SS.offSet_SP_Manutencao);
                        Command.SS.HorimetroParcial = Comunicacao.Sharp7.S7.GetDIntAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.SS.offSet_HorimetroParcial);
                        Command.SS.HorimetroTotal = Comunicacao.Sharp7.S7.GetDIntAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.SS.offSet_HorimetroTotal);
                        Command.SS.Tempo_Limpeza = Comunicacao.Sharp7.S7.GetDIntAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.SS.offSet_Tempo_Limpeza);
                        Command.SS.Corrente_Atual = (float)Math.Round(Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.SS.offSet_Corrente_Atual), 2);
                        Command.SS.SP_Corrente_Motor_Vazio = (float)Math.Round(Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.SS.offSet_SP_Corrente_Motor_Vazio), 2);
                        Command.SS.SP_Tempo_Reversao = Comunicacao.Sharp7.S7.GetDIntAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.SS.offSet_SP_Tempo_Reversao);
                        Command.SS.Tempo_Reversao_Atual = Comunicacao.Sharp7.S7.GetDIntAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.SS.offSet_Tempo_Reversao_Atual);


                        //Atualizando as variaveis para o standard GUI

                        //Primeiro converte a Dword para os bits
                        Command = Utilidades.Move_Bits.Dword_TO_typeSS(Command.DWord, Command);

                        //Segundo atualiza o standard GUI
                        Command = Utilidades.Move_Bits.typeSS_TO_typeStandardGUI(Command);
                        Generate_Faults_DB(Command, Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS);
                    }
                    else if (Command_Get.TypeEquip == typeEquip.Atuador && Command_Get.TypeCommand == typeCommand.Atuador_Digital)
                    {
                        //Lendo variaveis do buffer do CLP
                        Command.DWord = Comunicacao.Sharp7.S7.GetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet);

                        //Atualizando as variaveis para o standard GUI

                        //Primeiro converte a Dword para os bits
                        Command = Utilidades.Move_Bits.Dword_TO_typeAtuadorD(Command.DWord, Command);

                        //Segundo atualiza o standard GUI
                        Command = Utilidades.Move_Bits.typeAtuadorD_TO_typeStandardGUI(Command);
                        Generate_Faults_DB(Command, Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS);
                    }
                    else if (Command_Get.TypeEquip == typeEquip.Atuador && Command_Get.TypeCommand == typeCommand.Atuador_Analogico)
                    {
                        //Lendo variaveis do buffer do CLP
                        Command.DWord = Comunicacao.Sharp7.S7.GetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet);

                        Command.AtuadorA.PosicaoSolicitadaAutomatico = (float)Math.Round(Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.AtuadorA.offSet_PosicaoSolicitadaAutomatico), 2);
                        Command.AtuadorA.SP_Posicao_Manual = (float)Math.Round(Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.AtuadorA.offSet_SP_Posicao_Manual), 2);
                        Command.AtuadorA.PosicaoAtual = (float)Math.Round(Comunicacao.Sharp7.S7.GetRealAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet + Command.AtuadorA.offSet_PosicaoAtual), 2);


                        //Atualizando as variaveis para o standard GUI

                        //Primeiro converte a Dword para os bits
                        Command = Utilidades.Move_Bits.Dword_TO_typeAtuadorA(Command.DWord, Command);

                        //Segundo atualiza o standard GUI
                        Command = Utilidades.Move_Bits.typeAtuadorA_TO_typeStandardGUI(Command);
                        Generate_Faults_DB(Command, Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS);
                    }
                    else if (Command_Get.TypeEquip == typeEquip.BF && Command_Get.TypeCommand == typeCommand.Atuador_Digital)
                    {
                        //Lendo variaveis do buffer do CLP
                        Command.DWord = Comunicacao.Sharp7.S7.GetDWordAt(VariaveisGlobais.Buffer_PLC[Command.bufferPlc].Buffer, Command.initialOffSet);

                        //Atualizando as variaveis para o standard GUI

                        //Primeiro converte a Dword para os bits
                        Command = Utilidades.Move_Bits.Dword_TO_typeAtuadorD(Command.DWord, Command);

                        //Segundo atualiza o standard GUI
                        Command = Utilidades.Move_Bits.typeAtuadorD_TO_typeStandardGUI(Command);
                        Generate_Faults_DB(Command, Utilidades.VariaveisGlobais.UserLogged_GS, Utilidades.VariaveisGlobais.GroupUserLogged_GS);
                    }
                }

                //Atualiza Window
                if (Command_Get.TypeEquip == typeEquip.PD && Command_Get.TypeCommand == typeCommand.PD)
                {
                    Window_PD.actualize_UI(ref Command);
                }
                else if (Command_Get.TypeEquip == typeEquip.INV && Command_Get.TypeCommand == typeCommand.INV)
                {
                    Window_INV.actualize_UI(Command);
                }
                else if (Command_Get.TypeEquip == typeEquip.SS && Command_Get.TypeCommand == typeCommand.SS)
                {
                    Window_SS.actualize_UI(Command);
                }
                else if (Command_Get.TypeEquip == typeEquip.Atuador && Command_Get.TypeCommand == typeCommand.Atuador_Digital)
                {
                    Window_AtuadorD.actualize_UI(Command);
                }
                else if (Command_Get.TypeEquip == typeEquip.Atuador && Command_Get.TypeCommand == typeCommand.Atuador_Analogico)
                {
                    Window_AtuadorA.actualize_UI(Command);
                }
                else if (Command_Get.TypeEquip == typeEquip.BF && Command_Get.TypeCommand == typeCommand.Atuador_Digital)
                {
                    Window_BF.actualize_UI(Command);
                }
            }
        }

        public Utilidades.VariaveisGlobais.type_All Command_Get
        {
            get
            {
                return Command;
            }
        }

        public void OpenWindow()
        {
            if (Command_Get.TypeEquip == typeEquip.PD && Command_Get.TypeCommand == typeCommand.PD)
            {
                if (!Window_PD.created_GS)
                {
                    Window_PD = new Partidas.Principal.principalPartidaDireta(Command.Nome, Command.Tag, Command.NumeroPartida, Command.PaginaProjeto);

                    this.Window_PD.Height = 515;
                    this.Window_PD.Width = 255;

                    //Click para controle da Partida 
                    Window_PD.controlePD.Bt_Ligar_Click += new EventHandler(PD_Bt_Ligar_Click);
                    Window_PD.controlePD.Bt_Reset_Click += new EventHandler(PD_Bt_Reset_Click);
                    Window_PD.controlePD.Bt_Libera_Click += new EventHandler(PD_Bt_Libera_Click);
                    Window_PD.controlePD.Bt_Manutencao_Click += new EventHandler(PD_Bt_Manutencao_Click);
                    Window_PD.controlePD.Bt_Manual_Click += new EventHandler(PD_Bt_Manual_Click);

                    Window_PD.configuracoesPD.resetTotal_Click += new EventHandler(PD_resetTotal_Click);
                    Window_PD.configuracoesPD.resetParcial_Click += new EventHandler(PD_resetParcial_Click);
                    Window_PD.configuracoesPD.atualizaSPManutencao_Click += new EventHandler(PD_atualizaSPManutencao_Event);
                    Window_PD.configuracoesPD.atualizaSPLimpeza_Click += new EventHandler(PD_atualizaSPLimepza_Event);

                    Window_PD.Closing += Window_PD_Closing;

                    Window_PD.Bt_Fechar_Click += new EventHandler(PD_Bt_Fechar_Click);
                }

                Window_PD.configuracoesPD.SpManutencao = Command.PD.SP_Manutencao.ToString();

                Window_PD.configuracoesPD.SpLimpeza = Command.PD.Tempo_Limpeza.ToString();

                Window_PD.ShowDialog();
            }
            else if(Command_Get.TypeEquip == typeEquip.INV && Command_Get.TypeCommand == typeCommand.INV)
            {
                if (!Window_INV.created_GS)
                {
                    Window_INV = new Partidas.Principal.principalControleInversor(Command.Nome, Command.Tag, Command.NumeroPartida, Command.PaginaProjeto);

                    this.Window_INV.Height = 660;
                    this.Window_INV.Width = 255;

                    Window_INV.Closing += Window_INV_Closing;

                    //Click para controle da Partida 
                    Window_INV.controleINV.Bt_Ligar_Click += new EventHandler(INV_Bt_Ligar_Click);
                    Window_INV.controleINV.Bt_Reset_Click += new EventHandler(INV_Bt_Reset_Click);
                    Window_INV.controleINV.Bt_Libera_Click += new EventHandler(INV_Bt_Libera_Click);
                    Window_INV.controleINV.Bt_Manutencao_Click += new EventHandler(INV_Bt_Manutencao_Click);
                    Window_INV.controleINV.Bt_Manual_Click += new EventHandler(INV_Bt_Manual_Click);
                    Window_INV.controleINV.atualizarVelocidade += new EventHandler(INV_atualiza_Velocidade);


                    Window_INV.configuracoesINV.resetTotal_Click += new EventHandler(INV_resetTotal_Click);
                    Window_INV.configuracoesINV.resetParcial_Click += new EventHandler(INV_resetParcial_Click);
                    Window_INV.configuracoesINV.atualizaSPManutencao_Click += new EventHandler(INV_atualizaSPManutencao_Event);
                    Window_INV.configuracoesINV.atualizaSPLimpeza_Click += new EventHandler(INV_atualizaSPLimepza_Event);
                    Window_INV.configuracoesINV.atualizaSPMotorVazio_Click += new EventHandler(INV_atualizaSPMotorVazio_Event);

                    Window_INV.Bt_Fechar_Click += new EventHandler(INV_Bt_Fechar_Click);
                }

                Window_INV.controleINV.velocidadeManual_GS = Command.INV.Velocidade_Manual.ToString();

                Window_INV.configuracoesINV.SpManutencao = Command.INV.SP_Manutencao.ToString();

                Window_INV.configuracoesINV.SpLimpeza = Command.INV.Tempo_Limpeza.ToString();

                Window_INV.configuracoesINV.SpMotorVazio = Command.INV.SP_Corrente_Motor_Vazio.ToString();

                Window_INV.ShowDialog();
            }
            else if (Command_Get.TypeEquip == typeEquip.SS && Command_Get.TypeCommand == typeCommand.SS)
            {
                if (!Window_SS.created_GS)
                {
                    Window_SS = new Partidas.Principal.principalControleMoinho(Command.Nome, Command.Tag, Command.NumeroPartida, Command.PaginaProjeto);

                    this.Window_SS.Height = 617;
                    this.Window_SS.Width = 255;

                    Window_SS.Closing += Window_SS_Closing;

                    //Click para controle da Partida 
                    Window_SS.controleMoinho.Bt_Ligar_Click += new EventHandler(SS_Bt_Ligar_Click);
                    Window_SS.controleMoinho.Bt_Reset_Click += new EventHandler(SS_Bt_Reset_Click);
                    Window_SS.controleMoinho.Bt_Libera_Click += new EventHandler(SS_Bt_Libera_Click);
                    Window_SS.controleMoinho.Bt_Manutencao_Click += new EventHandler(SS_Bt_Manutencao_Click);
                    Window_SS.controleMoinho.Bt_Manual_Click += new EventHandler(SS_Bt_Manual_Click);

                    Window_SS.Bt_Fechar_Click += new EventHandler(SS_Bt_Fechar_Click);

                    Window_SS.controleMoinho.Bt_Inverte_Click += new EventHandler(SS_Bt_InverterSentido_Click);

                    Window_SS.configuracoesMOINHO.atualizaSPMotorVazio_Click += new EventHandler(SS_Bt_atualizarCorrenteVazio_Click);
                    Window_SS.configuracoesMOINHO.atualizaSPTempoReversao_Click += new EventHandler(SS_Bt_atualizarTempoReversao_Click);
                    Window_SS.configuracoesMOINHO.atualizaSPLimpeza_Click += new EventHandler(SS_Bt_atualizarSPLimpeza_Click);
                    Window_SS.configuracoesMOINHO.atualizaSPManutencao_Click += new EventHandler(SS_Bt_atualizarSPManutencao_Click);
                }

                Window_SS.configuracoesMOINHO.SpMotorVazio = Command.SS.SP_Corrente_Motor_Vazio.ToString();
                //Window_SS.configuracoesMOINHO.SpTempoReversao = Command.SS.SP_Tempo_Reversao.ToString();
                Window_SS.configuracoesMOINHO.SpLimpeza = Command.SS.Tempo_Limpeza.ToString();
                Window_SS.configuracoesMOINHO.SpManutencao = Command.SS.SP_Manutencao.ToString();


                Window_SS.ShowDialog();
            }
            else if (Command_Get.TypeEquip == typeEquip.Atuador && Command_Get.TypeCommand == typeCommand.Atuador_Digital)
            {
                if (!Window_AtuadorD.created_GS)
                {
                    Window_AtuadorD = new Partidas.Principal.principalControleAtuadorLinear(Command.Nome, Command.Tag, Command.NumeroPartida, Command.PaginaProjeto);

                    this.Window_AtuadorD.Height = 515;
                    this.Window_AtuadorD.Width = 255;


                    Window_AtuadorD.Closing += Window_AtuadorD_Closing;

                    //Click para controle da Partida 
                    Window_AtuadorD.controleAtuadorLinear.Bt_Abrir_Click += new EventHandler(AtuadorD_Bt_Ligar_Click);
                    Window_AtuadorD.controleAtuadorLinear.Bt_Reset_Click += new EventHandler(AtuadorD_Bt_Reset_Click);
                    Window_AtuadorD.controleAtuadorLinear.Bt_Libera_Click += new EventHandler(AtuadorD_Bt_Libera_Click);
                    Window_AtuadorD.controleAtuadorLinear.Bt_Manutencao_Click += new EventHandler(AtuadorD_Bt_Manutencao_Click);
                    Window_AtuadorD.controleAtuadorLinear.Bt_Manual_Click += new EventHandler(AtuadorD_Bt_Manual_Click);
                    Window_AtuadorD.Bt_Fechar_Click += new EventHandler(AtuadorD_Bt_Fechar_Click);
                }

                Window_AtuadorD.ShowDialog();
            }
            else if (Command_Get.TypeEquip == typeEquip.Atuador && Command_Get.TypeCommand == typeCommand.Atuador_Analogico)
            {
                if (!Window_AtuadorA.created_GS)
                {
                    Window_AtuadorA = new Partidas.Principal.principalControleAtuadorAnalogico(Command.Nome, Command.Tag, Command.NumeroPartida, Command.PaginaProjeto);

                    this.Window_AtuadorA.Height = 585;
                    this.Window_AtuadorA.Width = 255;

                    Window_AtuadorA.Closing += Window_AtuadorA_Closing;

                    //Click para controle da Partida 
                    Window_AtuadorA.controleAtuadorAnalogico.Bt_Ligar_Click += new EventHandler(AtuadorA_Bt_Ligar_Click);
                    Window_AtuadorA.controleAtuadorAnalogico.Bt_Reset_Click += new EventHandler(AtuadorA_Bt_Reset_Click);
                    Window_AtuadorA.controleAtuadorAnalogico.Bt_Libera_Click += new EventHandler(AtuadorA_Bt_Libera_Click);
                    Window_AtuadorA.controleAtuadorAnalogico.Bt_Manutencao_Click += new EventHandler(AtuadorA_Bt_Manutencao_Click);
                    Window_AtuadorA.controleAtuadorAnalogico.Bt_Manual_Click += new EventHandler(AtuadorA_Bt_Manual_Click);
                    Window_AtuadorA.controleAtuadorAnalogico.atualizarPosicao += new EventHandler(AtuadorA_AtulizaPosicao);

                    Window_AtuadorA.Bt_Fechar_Click += new EventHandler(AtuadorA_Bt_Fechar_Click);

                }

                Window_AtuadorA.controleAtuadorAnalogico.PosicaoSolicitada_GS = Convert.ToInt32(Command.AtuadorA.SP_Posicao_Manual).ToString();
               
                Window_AtuadorA.ShowDialog();
            }
            else if (Command_Get.TypeEquip == typeEquip.BF && Command_Get.TypeCommand == typeCommand.Atuador_Digital)
            {
                if (!Window_BF.created_GS)
                {
                    Window_BF = new Partidas.Principal.principalControleAtuadorLinearBifurcada(Command.Nome, Command.Tag, Command.NumeroPartida, Command.PaginaProjeto);

                    this.Window_BF.Height = 535;
                    this.Window_BF.Width = 255;


                    Window_BF.Closing += Window_AtuadorBF_Closing;

                    //Click para controle da Partida 
                    Window_BF.controleAtuadorBifurcada.Bt_AbrirDireita_Click += new EventHandler(AtuadorBF_Bt_LigarDireita_Click);
                    Window_BF.controleAtuadorBifurcada.Bt_AbrirEsquerda_Click += new EventHandler(AtuadorBF_Bt_LigarEsquerda_Click);

                    Window_BF.controleAtuadorBifurcada.Bt_Reset_Click += new EventHandler(AtuadorBF_Bt_Reset_Click);
                    Window_BF.controleAtuadorBifurcada.Bt_Libera_Click += new EventHandler(AtuadorBF_Bt_Libera_Click);
                    Window_BF.controleAtuadorBifurcada.Bt_Manutencao_Click += new EventHandler(AtuadorBF_Bt_Manutencao_Click);
                    Window_BF.controleAtuadorBifurcada.Bt_Manual_Click += new EventHandler(AtuadorBF_Bt_Manual_Click);
                    Window_BF.Bt_Fechar_Click += new EventHandler(AtuadorBF_Bt_Fechar_Click);

                }

                Window_BF.ShowDialog();
            }
        }

        #endregion
    }

}
