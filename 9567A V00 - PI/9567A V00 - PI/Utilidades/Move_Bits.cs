using System;

namespace _9567A_V00___PI.Utilidades
{
    public class Move_Bits
    {

        //Type PD
        //=====================================================================================================================================

        public static Utilidades.VariaveisGlobais.type_All typePD_TO_typeStandardGUI(Utilidades.VariaveisGlobais.type_All Command)
        {
            Command.Standard.Liga_Manual = Command.PD.ligaManual;
            Command.Standard.Manual = Command.PD.manual;
            Command.Standard.Automatico = Command.PD.automatico;
            Command.Standard.Manutencao = Command.PD.manutencao;
            Command.Standard.Libera_Bloqueio = Command.PD.Libera_Bloqueio_Manual;
            Command.Standard.Reset = Command.PD.reset;
            Command.Standard.Ligado = Command.PD.ligado;
            Command.Standard.Ligando = Command.PD.ligando;
            Command.Standard.Desligando = Command.PD.desligando;
            Command.Standard.Liberado = Command.PD.liberado;
            Command.Standard.Falha_Partida_Nao_Confirmou = Command.PD.falhaPartidaNaoConfirmou;
            Command.Standard.Falha_Contator_Desligou = Command.PD.falhaContatorDesligou;
            Command.Standard.Falha_Disjuntor_Desligou = Command.PD.falhaDisjuntoDesligou;
            Command.Standard.Falha_Partida_Nao_Desligou = Command.PD.falhaPartidaNaoDesligou;
            Command.Standard.Reset_Timer_Total = Command.PD.resetHorimetroTotal;
            Command.Standard.Reset_Timer = Command.PD.resetHorimetroParcial;
            Command.Standard.Emergencia = Command.PD.emergencia;
            Command.Standard.Disjuntor_Desligado = Command.PD.disjuntorDesligado;
            Command.Standard.Tempo_Manutencao = Command.PD.tempoManutencao;
            Command.Standard.Falha_Geral = Command.PD.falhaGeral;

            return Command;

        }

        public static Utilidades.VariaveisGlobais.type_All typeStandardGUI_TO_typePD(Utilidades.VariaveisGlobais.type_All Command)
        {
            Command.PD.ligaManual = Command.Standard.Liga_Manual;
            Command.PD.manual = Command.Standard.Manual;
            Command.PD.automatico = Command.Standard.Automatico;
            Command.PD.manutencao = Command.Standard.Manutencao;
            Command.PD.Libera_Bloqueio_Manual = Command.Standard.Libera_Bloqueio;
            Command.PD.reset = Command.Standard.Reset;
            Command.PD.ligado = Command.Standard.Ligado;
            Command.PD.ligando = Command.Standard.Ligando;
            Command.PD.desligando = Command.Standard.Desligando;
            Command.PD.liberado = Command.Standard.Liberado;
            Command.PD.falhaPartidaNaoConfirmou = Command.Standard.Falha_Partida_Nao_Confirmou;
            Command.PD.falhaContatorDesligou = Command.Standard.Falha_Contator_Desligou;
            Command.PD.falhaDisjuntoDesligou = Command.Standard.Falha_Disjuntor_Desligou;
            Command.PD.falhaPartidaNaoDesligou = Command.Standard.Falha_Partida_Nao_Desligou;
            Command.PD.resetHorimetroTotal = Command.Standard.Reset_Timer_Total;
            Command.PD.resetHorimetroParcial = Command.Standard.Reset_Timer;
            Command.PD.emergencia = Command.Standard.Emergencia;
            Command.PD.disjuntorDesligado = Command.Standard.Disjuntor_Desligado;
            Command.PD.tempoManutencao = Command.Standard.Tempo_Manutencao;
            Command.PD.falhaGeral = Command.Standard.Falha_Geral;

            return Command;
        }

        public static Utilidades.VariaveisGlobais.type_All Dword_TO_typePD(UInt32 DWord, Utilidades.VariaveisGlobais.type_All Command)
        {
            bool[] bits = new bool[32];

            Conversions.Dword_To_Bit(DWord, ref bits, true);

            //Atualiza DWord
            Command.DWord = DWord;


            Command.PD.ligaManual = bits[0];
            Command.PD.manual = bits[1];
            Command.PD.automatico = bits[2];
            Command.PD.manutencao = bits[3];
            Command.PD.Libera_Bloqueio_Manual = bits[4];
            Command.PD.reset = bits[5];
            Command.PD.ligado = bits[6];
            Command.PD.ligando = bits[7];
            Command.PD.desligando = bits[8];
            Command.PD.liberado = bits[9];
            Command.PD.falhaPartidaNaoConfirmou = bits[10];
            Command.PD.falhaContatorDesligou = bits[11];
            Command.PD.falhaDisjuntoDesligou = bits[12];
            Command.PD.falhaPartidaNaoDesligou = bits[13];
            Command.PD.resetHorimetroTotal = bits[14];
            Command.PD.resetHorimetroParcial = bits[15];
            Command.PD.emergencia = bits[16];
            Command.PD.disjuntorDesligado = bits[17];
            Command.PD.tempoManutencao = bits[18]; 
            Command.PD.falhaGeral = bits[19];
            Command.PD.bitReserva = bits[20];
            Command.PD.bitReserva_1 = bits[21];
            Command.PD.bitReserva_2 = bits[22];
            Command.PD.bitReserva_3 = bits[23];
            Command.PD.bitReserva_4 = bits[24];
            Command.PD.bitReserva_5 = bits[25];
            Command.PD.bitReserva_6 = bits[26];
            Command.PD.bitReserva_7 = bits[27];
            Command.PD.bitReserva_8 = bits[28];
            Command.PD.bitReserva_9 = bits[29];
            Command.PD.bitReserva_10 = bits[30];
            Command.PD.bitReserva_11 = bits[31];

            return Command;
        }

        public static UInt32 typePD_TO_Dword(Utilidades.VariaveisGlobais.type_All Command)
        {
            bool[] bits = new bool[32];

            bits[0] = Command.PD.ligaManual;
            bits[1] = Command.PD.manual;
            bits[2] = Command.PD.automatico;
            bits[3] = Command.PD.manutencao;
            bits[4] = Command.PD.Libera_Bloqueio_Manual;
            bits[5] = Command.PD.reset;
            bits[6] = Command.PD.ligado;
            bits[7] = Command.PD.ligando;
            bits[8] = Command.PD.desligando;
            bits[9] = Command.PD.liberado;
            bits[10] = Command.PD.falhaPartidaNaoConfirmou;
            bits[11] = Command.PD.falhaContatorDesligou;
            bits[12] = Command.PD.falhaDisjuntoDesligou;
            bits[13] = Command.PD.falhaPartidaNaoDesligou;
            bits[14] = Command.PD.resetHorimetroTotal;
            bits[15] = Command.PD.resetHorimetroParcial;
            bits[16] = Command.PD.emergencia;
            bits[17] = Command.PD.disjuntorDesligado;
            bits[18] = Command.PD.tempoManutencao;
            bits[19] = Command.PD.falhaGeral;
            bits[20] = Command.PD.bitReserva;
            bits[21] = Command.PD.bitReserva_1;
            bits[22] = Command.PD.bitReserva_2;
            bits[23] = Command.PD.bitReserva_3;
            bits[24] = Command.PD.bitReserva_4;
            bits[25] = Command.PD.bitReserva_5;
            bits[26] = Command.PD.bitReserva_6;
            bits[27] = Command.PD.bitReserva_7;
            bits[28] = Command.PD.bitReserva_8;
            bits[29] = Command.PD.bitReserva_9;
            bits[30] = Command.PD.bitReserva_10;
            bits[31] = Command.PD.bitReserva_11;

            Command.DWord = Conversions.Bit_To_Dword(ref bits, true);

            return Command.DWord;
        }

        //Type INV
        //=====================================================================================================================================

        public static Utilidades.VariaveisGlobais.type_All typeINV_TO_typeStandardGUI(Utilidades.VariaveisGlobais.type_All Command)
        {
            Command.Standard.Liga_Manual = Command.INV.ligaManual;
            Command.Standard.Manual = Command.INV.manual;
            Command.Standard.Automatico = Command.INV.automatico;
            Command.Standard.Manutencao = Command.INV.manutencao;
            Command.Standard.Libera_Bloqueio = Command.INV.Libera_Bloqueio_Manual;
            Command.Standard.Reset = Command.INV.reset;
            Command.Standard.Ligado = Command.INV.ligado;
            Command.Standard.Ligando = Command.INV.ligando;
            Command.Standard.Desligando = Command.INV.desligando;
            Command.Standard.Liberado = Command.INV.liberado;
            Command.Standard.Reset_Timer_Total = Command.INV.resetHorimetroTotal;
            Command.Standard.Reset_Timer = Command.INV.resetHorimetroParcial;
            Command.Standard.Emergencia = Command.INV.emergencia;
            Command.Standard.Disjuntor_Desligado = Command.INV.disjuntorDesligado;
            Command.Standard.Tempo_Manutencao = Command.INV.tempoManutencao;
            Command.Standard.Falha_Geral = Command.INV.falha;

            return Command;

        }

        public static Utilidades.VariaveisGlobais.type_All typeStandardGUI_TO_typeINV(Utilidades.VariaveisGlobais.type_All Command)
        {
            Command.INV.ligaManual = Command.Standard.Liga_Manual;
            Command.INV.manual = Command.Standard.Manual;
            Command.INV.automatico = Command.Standard.Automatico;
            Command.INV.manutencao = Command.Standard.Manutencao;
            Command.INV.Libera_Bloqueio_Manual = Command.Standard.Libera_Bloqueio;
            Command.INV.reset = Command.Standard.Reset;
            Command.INV.ligado = Command.Standard.Ligado;
            Command.INV.ligando = Command.Standard.Ligando;
            Command.INV.desligando = Command.Standard.Desligando;
            Command.INV.liberado = Command.Standard.Liberado;
            Command.INV.resetHorimetroTotal = Command.Standard.Reset_Timer_Total;
            Command.INV.resetHorimetroParcial = Command.Standard.Reset_Timer;
            Command.INV.emergencia = Command.Standard.Emergencia;
            Command.INV.disjuntorDesligado = Command.Standard.Disjuntor_Desligado;
            Command.INV.tempoManutencao = Command.Standard.Tempo_Manutencao;
            Command.INV.falha = Command.Standard.Falha_Geral;

            return Command;
        }

        public static Utilidades.VariaveisGlobais.type_All Dword_TO_typeINV(UInt32 DWord, Utilidades.VariaveisGlobais.type_All Command)
        {
            bool[] bits = new bool[32];

            Conversions.Dword_To_Bit(DWord, ref bits, true);

            //Atualiza DWord
            Command.DWord = DWord;


            Command.INV.ligaManual = bits[0];
            Command.INV.manual = bits[1];
            Command.INV.automatico = bits[2];
            Command.INV.manutencao = bits[3];
            Command.INV.Libera_Bloqueio_Manual = bits[4];
            Command.INV.reset = bits[5];
            Command.INV.ligado = bits[6];
            Command.INV.ligando = bits[7];
            Command.INV.desligando = bits[8];
            Command.INV.liberado = bits[9];
            Command.INV.resetHorimetroTotal = bits[10];
            Command.INV.resetHorimetroParcial = bits[11];
            Command.INV.emergencia = bits[12];
            Command.INV.disjuntorDesligado = bits[13];
            Command.INV.tempoManutencao = bits[14];
            Command.INV.falha = bits[15];
            Command.INV.bitReserva = bits[16];
            Command.INV.bitReserva_1 = bits[17];
            Command.INV.bitReserva_2 = bits[18];
            Command.INV.bitReserva_3 = bits[19];
            Command.INV.bitReserva_4 = bits[20];
            Command.INV.bitReserva_5 = bits[21];
            Command.INV.bitReserva_6 = bits[22];
            Command.INV.bitReserva_7 = bits[23];
            Command.INV.bitReserva_8 = bits[24];
            Command.INV.bitReserva_9 = bits[25];
            Command.INV.bitReserva_10 = bits[26];
            Command.INV.bitReserva_11 = bits[27];
            Command.INV.bitReserva_12 = bits[28];
            Command.INV.bitReserva_13 = bits[29];
            Command.INV.bitReserva_14 = bits[30];
            Command.INV.bitReserva_15 = bits[31];

            return Command;
        }

        public static UInt32 typeINV_TO_Dword(Utilidades.VariaveisGlobais.type_All Command)
        {
            bool[] bits = new bool[32];

            bits[0] = Command.INV.ligaManual;
            bits[1] = Command.INV.manual;
            bits[2] = Command.INV.automatico;
            bits[3] = Command.INV.manutencao;
            bits[4] = Command.INV.Libera_Bloqueio_Manual;
            bits[5] = Command.INV.reset;
            bits[6] = Command.INV.ligado;
            bits[7] = Command.INV.ligando;
            bits[8] = Command.INV.desligando;
            bits[9] = Command.INV.liberado;
            bits[10] = Command.INV.resetHorimetroTotal;
            bits[11] = Command.INV.resetHorimetroParcial;
            bits[12] = Command.INV.emergencia;
            bits[13] = Command.INV.disjuntorDesligado;
            bits[14] = Command.INV.tempoManutencao;
            bits[15] = Command.INV.falha;
            bits[16] = Command.INV.bitReserva;
            bits[17] = Command.INV.bitReserva_1;
            bits[18] = Command.INV.bitReserva_2;
            bits[19] = Command.INV.bitReserva_3;
            bits[20] = Command.INV.bitReserva_4;
            bits[21] = Command.INV.bitReserva_5;
            bits[22] = Command.INV.bitReserva_6;
            bits[23] = Command.INV.bitReserva_7;
            bits[24] = Command.INV.bitReserva_8;
            bits[25] = Command.INV.bitReserva_9;
            bits[26] = Command.INV.bitReserva_10;
            bits[27] = Command.INV.bitReserva_11;
            bits[28] = Command.INV.bitReserva_12;
            bits[29] = Command.INV.bitReserva_13;
            bits[30] = Command.INV.bitReserva_14;
            bits[31] = Command.INV.bitReserva_15;
            Command.DWord = Conversions.Bit_To_Dword(ref bits, true);

            return Command.DWord;
        }

        //Type SS
        //=====================================================================================================================================

        public static Utilidades.VariaveisGlobais.type_All typeSS_TO_typeStandardGUI(Utilidades.VariaveisGlobais.type_All Command)
        {
            Command.Standard.Liga_Manual = Command.SS.ligaManual;
            Command.Standard.Manual = Command.SS.manual;
            Command.Standard.Automatico = Command.SS.automatico;
            Command.Standard.Manutencao = Command.SS.manutencao;
            Command.Standard.Libera_Bloqueio = Command.SS.Libera_Bloqueio_Manual;
            Command.Standard.Reset = Command.SS.reset;
            Command.Standard.Ligado = Command.SS.ligado;
            Command.Standard.Ligando = Command.SS.ligando;
            Command.Standard.Desligando = Command.SS.desligando;
            Command.Standard.Liberado = Command.SS.liberado;
            Command.Standard.Falha_Partida_Nao_Confirmou = Command.SS.falhaPartidaNaoConfirmou;
            Command.Standard.Falha_Contator_Desligou = Command.SS.falhaContatorDesligou;
            Command.Standard.Falha_Disjuntor_Desligou = Command.SS.falhaDisjuntoDesligou;
            Command.Standard.Falha_Partida_Nao_Desligou = Command.SS.falhaPartidaNaoDesligou;
            Command.Standard.Reset_Timer = Command.SS.resetHorimetroParcial;
            Command.Standard.Reset_Timer_Total = Command.SS.resetHorimetroTotal;
            Command.Standard.Emergencia = Command.SS.emergencia;
            Command.Standard.Disjuntor_Desligado = Command.SS.disjuntorDesligado;
            Command.Standard.Tempo_Manutencao = Command.SS.tempoManutencao;
            Command.Standard.Falha_Geral = Command.SS.falhaGeral;
            Command.Standard.inverterSentidoGiro = Command.SS.inverterSentidoGiro;
            Command.Standard.SentidoGiro = Command.SS.sentidoGiro;
            Command.Standard.confirmaSentidoGiroAlimentador = Command.SS.confirmaSentidoGiroAlimentador;
            Command.Standard.SentidoGiroMotorMudou = Command.SS.SentidoGiroMotorMudou;
            Command.Standard.Sensor_Alimentador_Sentido_Horario = Command.SS.Sensor_Alimentador_Sentido_Horario;
            Command.Standard.Sensor_Alimentador_Sentido_Anti_Horario = Command.SS.Sensor_Alimentador_Sentido_Anti_Horario;
            return Command;

        }

        public static Utilidades.VariaveisGlobais.type_All typeStandardGUI_TO_typeSS(Utilidades.VariaveisGlobais.type_All Command)
        {
            Command.SS.ligaManual = Command.Standard.Liga_Manual;
            Command.SS.manual = Command.Standard.Manual;
            Command.SS.automatico = Command.Standard.Automatico;
            Command.SS.manutencao = Command.Standard.Manutencao;
            Command.SS.Libera_Bloqueio_Manual = Command.Standard.Libera_Bloqueio;
            Command.SS.reset = Command.Standard.Reset;
            Command.SS.ligado = Command.Standard.Ligado;
            Command.SS.ligando = Command.Standard.Ligando;
            Command.SS.desligando = Command.Standard.Desligando;
            Command.SS.liberado = Command.Standard.Liberado;
            Command.SS.falhaPartidaNaoConfirmou = Command.Standard.Falha_Partida_Nao_Confirmou;
            Command.SS.falhaContatorDesligou = Command.Standard.Falha_Contator_Desligou;
            Command.SS.falhaDisjuntoDesligou = Command.Standard.Falha_Disjuntor_Desligou;
            Command.SS.falhaPartidaNaoDesligou = Command.Standard.Falha_Partida_Nao_Desligou;
            Command.SS.resetHorimetroTotal = Command.Standard.Reset_Timer;
            Command.SS.resetHorimetroParcial = Command.Standard.Reset_Timer_Total;
            Command.SS.emergencia = Command.Standard.Emergencia;
            Command.SS.disjuntorDesligado = Command.Standard.Disjuntor_Desligado;
            Command.SS.tempoManutencao = Command.Standard.Tempo_Manutencao;
            Command.SS.falhaGeral = Command.Standard.Falha_Geral;
            Command.SS.inverterSentidoGiro = Command.Standard.inverterSentidoGiro;
            Command.SS.sentidoGiro = Command.Standard.SentidoGiro;
            Command.SS.confirmaSentidoGiroAlimentador = Command.Standard.confirmaSentidoGiroAlimentador;
            Command.SS.SentidoGiroMotorMudou = Command.Standard.SentidoGiroMotorMudou;
            Command.SS.Sensor_Alimentador_Sentido_Horario = Command.Standard.Sensor_Alimentador_Sentido_Horario;
            Command.SS.Sensor_Alimentador_Sentido_Anti_Horario = Command.Standard.Sensor_Alimentador_Sentido_Anti_Horario;

            return Command;
        }

        public static Utilidades.VariaveisGlobais.type_All Dword_TO_typeSS(UInt32 DWord, Utilidades.VariaveisGlobais.type_All Command)
        {
            bool[] bits = new bool[32];

            Conversions.Dword_To_Bit(DWord, ref bits, true);

            //Atualiza DWord
            Command.DWord = DWord;


            Command.SS.ligaManual = bits[0];
            Command.SS.manual = bits[1];
            Command.SS.automatico = bits[2];
            Command.SS.manutencao = bits[3];
            Command.SS.Libera_Bloqueio_Manual = bits[4];
            Command.SS.reset = bits[5];
            Command.SS.ligado = bits[6];
            Command.SS.ligando = bits[7];
            Command.SS.desligando = bits[8];
            Command.SS.liberado = bits[9];
            Command.SS.falhaPartidaNaoConfirmou = bits[10];
            Command.SS.falhaContatorDesligou = bits[11];
            Command.SS.falhaDisjuntoDesligou = bits[12];
            Command.SS.falhaPartidaNaoDesligou = bits[13];
            Command.SS.resetHorimetroTotal = bits[14];
            Command.SS.resetHorimetroParcial = bits[15];
            Command.SS.emergencia = bits[16];
            Command.SS.disjuntorDesligado = bits[17];
            Command.SS.tempoManutencao = bits[18];
            Command.SS.falhaGeral = bits[19];
            Command.SS.inverterSentidoGiro = bits[20];
            Command.SS.sentidoGiro = bits[21];
            Command.SS.confirmaSentidoGiroAlimentador = bits[22];
            Command.SS.SentidoGiroMotorMudou = bits[23];
            Command.SS.Sensor_Alimentador_Sentido_Horario = bits[24];
            Command.SS.Sensor_Alimentador_Sentido_Anti_Horario = bits[25];
            Command.SS.bitReserva_4 = bits[26];
            Command.SS.bitReserva_5 = bits[27];
            Command.SS.bitReserva_6 = bits[28];
            Command.SS.bitReserva_7 = bits[29];
            Command.SS.bitReserva_8 = bits[30];
            Command.SS.bitReserva_9 = bits[31];

            return Command;
        }

        public static UInt32 typeSS_TO_Dword(Utilidades.VariaveisGlobais.type_All Command)
        {
            bool[] bits = new bool[32];

            bits[0] = Command.SS.ligaManual;
            bits[1] = Command.SS.manual;
            bits[2] = Command.SS.automatico;
            bits[3] = Command.SS.manutencao;
            bits[4] = Command.SS.Libera_Bloqueio_Manual;
            bits[5] = Command.SS.reset;
            bits[6] = Command.SS.ligado;
            bits[7] = Command.SS.ligando;
            bits[8] = Command.SS.desligando;
            bits[9] = Command.SS.liberado;
            bits[10] = Command.SS.falhaPartidaNaoConfirmou;
            bits[11] = Command.SS.falhaContatorDesligou;
            bits[12] = Command.SS.falhaDisjuntoDesligou;
            bits[13] = Command.SS.falhaPartidaNaoDesligou;
            bits[14] = Command.SS.resetHorimetroTotal;
            bits[15] = Command.SS.resetHorimetroParcial;
            bits[16] = Command.SS.emergencia;
            bits[17] = Command.SS.disjuntorDesligado;
            bits[18] = Command.SS.tempoManutencao;
            bits[19] = Command.SS.falhaGeral;
            bits[20] = Command.SS.inverterSentidoGiro;
            bits[21] = Command.SS.sentidoGiro;
            bits[22] = Command.SS.confirmaSentidoGiroAlimentador;
            bits[23] = Command.SS.SentidoGiroMotorMudou;
            bits[24] = Command.SS.Sensor_Alimentador_Sentido_Horario;
            bits[25] = Command.SS.Sensor_Alimentador_Sentido_Anti_Horario;
            bits[26] = Command.SS.bitReserva_4;
            bits[27] = Command.SS.bitReserva_5;
            bits[28] = Command.SS.bitReserva_6;
            bits[29] = Command.SS.bitReserva_7;
            bits[30] = Command.SS.bitReserva_8;
            bits[31] = Command.SS.bitReserva_9;

            Command.DWord = Conversions.Bit_To_Dword(ref bits, true);

            return Command.DWord;
        }

        //Type Atuador Digital
        //=====================================================================================================================================

        public static Utilidades.VariaveisGlobais.type_All typeAtuadorD_TO_typeStandardGUI(Utilidades.VariaveisGlobais.type_All Command)
        {
            Command.Standard.Manual = Command.AtuadorD.manual;
            Command.Standard.Automatico = Command.AtuadorD.automatico;
            Command.Standard.Manutencao = Command.AtuadorD.manutencao;
            Command.Standard.Libera_Bloqueio = Command.AtuadorD.Libera_Bloqueio_Manual;
            Command.Standard.Emergencia = Command.AtuadorD.emergencia;
            Command.Standard.Liberado = Command.AtuadorD.liberado;
            Command.Standard.AcionaLado1 = Command.AtuadorD.acionaLado1;
            Command.Standard.AcionaLado2 = Command.AtuadorD.acionaLado2;
            Command.Standard.AcionandoLado1 = Command.AtuadorD.acionandoLado1;
            Command.Standard.AcionandoLado2 = Command.AtuadorD.acionandoLado2;
            Command.Standard.AcionandoAutomatico = Command.AtuadorD.acionandoAutomatico;
            Command.Standard.EmPosicaoLado1 = Command.AtuadorD.emPosicaoLado1;
            Command.Standard.EmPosicaoLado2 = Command.AtuadorD.emPosicaoLado2;
            Command.Standard.FalhaAcionandoLado1 = Command.AtuadorD.falhaAcionandoLado1;
            Command.Standard.FalhaAcionandoLado2 = Command.AtuadorD.falhaAcionandoLado2;
            Command.Standard.Falha2PosicoesAtiva = Command.AtuadorD.falha2PosicoesAtiva;
            Command.Standard.FalhaConfirmacaoContatorLado1 = Command.AtuadorD.falhaConfirmacaoContatorLado1;
            Command.Standard.FalhaConfirmacaoContatorLado2 = Command.AtuadorD.falhaConfirmacaoContatorLado2;
            Command.Standard.Reset = Command.AtuadorD.reset;
            return Command;

        }

        public static Utilidades.VariaveisGlobais.type_All typeStandardGUI_TO_typeAtuadorD(Utilidades.VariaveisGlobais.type_All Command)
        {
            Command.AtuadorD.manual = Command.Standard.Manual;
            Command.AtuadorD.automatico = Command.Standard.Automatico;
            Command.AtuadorD.manutencao = Command.Standard.Manutencao;
            Command.AtuadorD.Libera_Bloqueio_Manual = Command.Standard.Libera_Bloqueio;
            Command.AtuadorD.emergencia = Command.Standard.Emergencia;
            Command.AtuadorD.liberado = Command.Standard.Liberado;
            Command.AtuadorD.acionaLado1 = Command.Standard.AcionaLado1;
            Command.AtuadorD.acionaLado2 = Command.Standard.AcionaLado2;
            Command.AtuadorD.acionandoLado1 = Command.Standard.AcionandoLado1;
            Command.AtuadorD.acionandoLado2 = Command.Standard.AcionandoLado2;
            Command.AtuadorD.acionandoAutomatico = Command.Standard.AcionandoAutomatico;
            Command.AtuadorD.emPosicaoLado1 = Command.Standard.EmPosicaoLado1;
            Command.AtuadorD.emPosicaoLado2 = Command.Standard.EmPosicaoLado2;
            Command.AtuadorD.falhaAcionandoLado1 = Command.Standard.FalhaAcionandoLado1;
            Command.AtuadorD.falhaAcionandoLado2 = Command.Standard.FalhaAcionandoLado2;
            Command.AtuadorD.falha2PosicoesAtiva = Command.Standard.Falha2PosicoesAtiva;
            Command.AtuadorD.falhaConfirmacaoContatorLado1 = Command.Standard.FalhaConfirmacaoContatorLado1;
            Command.AtuadorD.falhaConfirmacaoContatorLado2 = Command.Standard.FalhaConfirmacaoContatorLado2;
            Command.AtuadorD.reset = Command.Standard.Reset;
            return Command;
        }

        public static Utilidades.VariaveisGlobais.type_All Dword_TO_typeAtuadorD(UInt32 DWord, Utilidades.VariaveisGlobais.type_All Command)
        {
            bool[] bits = new bool[32];

            Conversions.Dword_To_Bit(DWord, ref bits, true);

            //Atualiza DWord
            Command.DWord = DWord;

            Command.AtuadorD.manual = bits[0];
            Command.AtuadorD.automatico = bits[1];
            Command.AtuadorD.manutencao = bits[2];
            Command.AtuadorD.Libera_Bloqueio_Manual = bits[3];
            Command.AtuadorD.emergencia = bits[4];
            Command.AtuadorD.liberado = bits[5];
            Command.AtuadorD.acionaLado1 = bits[6];
            Command.AtuadorD.acionaLado2 = bits[7];
            Command.AtuadorD.acionandoLado1 = bits[8];
            Command.AtuadorD.acionandoLado2 = bits[9];
            Command.AtuadorD.acionandoAutomatico = bits[10];
            Command.AtuadorD.emPosicaoLado1 = bits[11];
            Command.AtuadorD.emPosicaoLado2 = bits[12];
            Command.AtuadorD.falhaAcionandoLado1 = bits[13];
            Command.AtuadorD.falhaAcionandoLado2 = bits[14];
            Command.AtuadorD.falha2PosicoesAtiva = bits[15];
            Command.AtuadorD.falhaConfirmacaoContatorLado1 = bits[16];
            Command.AtuadorD.falhaConfirmacaoContatorLado2 = bits[17];
            Command.AtuadorD.reset = bits[18];
            Command.AtuadorD.bitReserva = bits[19];
            Command.AtuadorD.bitReserva_1 = bits[20];
            Command.AtuadorD.bitReserva_2 = bits[21];
            Command.AtuadorD.bitReserva_3 = bits[22];
            Command.AtuadorD.bitReserva_4 = bits[23];
            Command.AtuadorD.bitReserva_5 = bits[24];
            Command.AtuadorD.bitReserva_6 = bits[25];
            Command.AtuadorD.bitReserva_7 = bits[26];
            Command.AtuadorD.bitReserva_8 = bits[27];
            Command.AtuadorD.bitReserva_9 = bits[28];
            Command.AtuadorD.bitReserva_10 = bits[29];
            Command.AtuadorD.bitReserva_11 = bits[30];
            Command.AtuadorD.bitReserva_12 = bits[31];


            return Command;
        }

        public static UInt32 typeAtuadorD_TO_Dword(Utilidades.VariaveisGlobais.type_All Command)
        {
            bool[] bits = new bool[32];

            bits[0] = Command.AtuadorD.manual;
            bits[1] = Command.AtuadorD.automatico;
            bits[2] = Command.AtuadorD.manutencao;
            bits[3] = Command.AtuadorD.Libera_Bloqueio_Manual;
            bits[4] = Command.AtuadorD.emergencia;
            bits[5] = Command.AtuadorD.liberado;
            bits[6] = Command.AtuadorD.acionaLado1;
            bits[7] = Command.AtuadorD.acionaLado2;
            bits[8] = Command.AtuadorD.acionandoLado1;
            bits[9] = Command.AtuadorD.acionandoLado2;
            bits[10] = Command.AtuadorD.acionandoAutomatico;
            bits[11] = Command.AtuadorD.emPosicaoLado1;
            bits[12] = Command.AtuadorD.emPosicaoLado2;
            bits[13] = Command.AtuadorD.falhaAcionandoLado1;
            bits[14] = Command.AtuadorD.falhaAcionandoLado2;
            bits[15] = Command.AtuadorD.falha2PosicoesAtiva;
            bits[16] = Command.AtuadorD.falhaConfirmacaoContatorLado1;
            bits[17] = Command.AtuadorD.falhaConfirmacaoContatorLado1;
            bits[18] = Command.AtuadorD.reset;
            bits[19] = Command.AtuadorD.bitReserva;
            bits[20] = Command.AtuadorD.bitReserva_1;
            bits[21] = Command.AtuadorD.bitReserva_2;
            bits[22] = Command.AtuadorD.bitReserva_3;
            bits[23] = Command.AtuadorD.bitReserva_4;
            bits[24] = Command.AtuadorD.bitReserva_5;
            bits[25] = Command.AtuadorD.bitReserva_6;
            bits[26] = Command.AtuadorD.bitReserva_7;
            bits[27] = Command.AtuadorD.bitReserva_8;
            bits[28] = Command.AtuadorD.bitReserva_9;
            bits[29] = Command.AtuadorD.bitReserva_10;
            bits[30] = Command.AtuadorD.bitReserva_11;
            bits[31] = Command.AtuadorD.bitReserva_12;

            Command.DWord = Conversions.Bit_To_Dword(ref bits, true);

            return Command.DWord;
        }

        //Type Registro
        //=====================================================================================================================================

        public static Utilidades.VariaveisGlobais.type_All typeAtuadorR_TO_typeStandardGUI(Utilidades.VariaveisGlobais.type_All Command)
        {


            Command.Standard.Automatico = Command.AtuadorR.automatico;
            Command.Standard.Manual =    Command.AtuadorR.manual;
            Command.Standard.Reset = Command.AtuadorR.Reset;
            Command.Standard.Manutencao = Command.AtuadorR.Manutencao;
            Command.Standard.Liberado = !Command.AtuadorR.Bloqueado;
            Command.Standard.Libera_Bloqueio = Command.AtuadorR.Sem_Bloqueio;
            Command.Standard.Emergencia = Command.AtuadorR.Emergencia;
            Command.Standard.Habilitado_Ligar_Rota = Command.AtuadorR.Habilitado_Ligar_Rota;
            Command.Standard.Liga_Manual = Command.AtuadorR.ligaManual;
            Command.Standard.Abrindo =  Command.AtuadorR.Abrindo; 
            Command.Standard.Fechando = Command.AtuadorR.Fechando;
            Command.Standard.Aberto = Command.AtuadorR.Aberto;
            Command.Standard.Fechado = Command.AtuadorR.Fechado;
            Command.Standard.Falha_Abrir = Command.AtuadorR.Falha_Abrir;
            Command.Standard.Falha_Fechar = Command.AtuadorR.Falha_Fechar;
            Command.Standard.Falha_Sem_Posicao = Command.AtuadorR.Falha_Sem_Posicao;
            Command.Standard.SensorAberto  = Command.AtuadorR.SensorAberto;
            Command.Standard.SensorFechado = Command.AtuadorR.SensorFechado;
            Command.Standard.ErroGeral = Command.AtuadorR.ErroGeral;
            Command.Standard.Seleciona_Rota =  Command.AtuadorR.Seleciona_Rota;

            return Command;






        }

        public static Utilidades.VariaveisGlobais.type_All typeStandardGUI_TO_typeAtuadorR(Utilidades.VariaveisGlobais.type_All Command)
        {
            Command.AtuadorR.automatico = Command.Standard.Automatico;
            Command.AtuadorR.manual = Command.Standard.Manual;
            Command.AtuadorR.Reset = Command.Standard.Reset;
            Command.AtuadorR.Manutencao = Command.Standard.Manutencao;
            Command.AtuadorR.Bloqueado = !Command.Standard.Liberado;
            Command.AtuadorR.Sem_Bloqueio = Command.Standard.Libera_Bloqueio;
            Command.AtuadorR.Emergencia = Command.Standard.Emergencia;
            Command.AtuadorR.Habilitado_Ligar_Rota = Command.Standard.Habilitado_Ligar_Rota;
            Command.AtuadorR.ligaManual = Command.Standard.Liga_Manual;
            Command.AtuadorR.Abrindo = Command.Standard.Abrindo;
            Command.AtuadorR.Fechando = Command.Standard.Fechando;
            Command.AtuadorR.Aberto = Command.Standard.Aberto;
            Command.AtuadorR.Fechado = Command.Standard.Fechado;
            Command.AtuadorR.Falha_Abrir = Command.Standard.Falha_Abrir;
            Command.AtuadorR.Falha_Fechar = Command.Standard.Falha_Fechar;
            Command.AtuadorR.Falha_Sem_Posicao = Command.Standard.Falha_Sem_Posicao;
            Command.AtuadorR.SensorAberto = Command.Standard.SensorAberto;
            Command.AtuadorR.SensorFechado = Command.Standard.SensorFechado;
            Command.AtuadorR.ErroGeral = Command.Standard.ErroGeral;
            Command.AtuadorR.Seleciona_Rota = Command.Standard.Seleciona_Rota;

            return Command;

        }

        public static Utilidades.VariaveisGlobais.type_All Dword_TO_typeAtuadorR(UInt32 DWord, Utilidades.VariaveisGlobais.type_All Command)
        {
            bool[] bits = new bool[32];

            Conversions.Dword_To_Bit(DWord, ref bits, true);

            //Atualiza DWord
            Command.DWord = DWord;

            Command.AtuadorR.automatico = bits[0];
            Command.AtuadorR.manual = bits[1];
            Command.AtuadorR.Reset = bits[2];
            Command.AtuadorR.Manutencao = bits[3];
            Command.AtuadorR.Bloqueado = bits[4];
            Command.AtuadorR.Sem_Bloqueio = bits[5];
            Command.AtuadorR.Emergencia = bits[6];
            Command.AtuadorR.bit07 = bits[7];
            Command.AtuadorR.Habilitado_Ligar_Rota = bits[8];
            Command.AtuadorR.ligaManual = bits[9];
            Command.AtuadorR.Abrindo = bits[10];
            Command.AtuadorR.Fechando = bits[11];
            Command.AtuadorR.Aberto = bits[12];
            Command.AtuadorR.Fechado = bits[13];
            Command.AtuadorR.Falha_Abrir = bits[14];
            Command.AtuadorR.Falha_Fechar = bits[15];
            Command.AtuadorR.Falha_Sem_Posicao = bits[16];
            Command.AtuadorR.SensorAberto = bits[17];
            Command.AtuadorR.SensorFechado = bits[18];
            Command.AtuadorR.ErroGeral = bits[19];
            Command.AtuadorR.Seleciona_Rota = bits[20];
            Command.AtuadorD.bitReserva_1 = bits[21];
            Command.AtuadorD.bitReserva_2 = bits[22];
            Command.AtuadorD.bitReserva_3 = bits[23];
            Command.AtuadorD.bitReserva_4 = bits[24];
            Command.AtuadorD.bitReserva_5 = bits[25];
            Command.AtuadorD.bitReserva_6 = bits[26];
            Command.AtuadorD.bitReserva_7 = bits[27];
            Command.AtuadorD.bitReserva_8 = bits[28];
            Command.AtuadorD.bitReserva_9 = bits[29];
            Command.AtuadorD.bitReserva_10 = bits[30];
            Command.AtuadorD.bitReserva_11 = bits[31];


            return Command;
        }

        public static UInt32 typeAtuadorR_TO_Dword(Utilidades.VariaveisGlobais.type_All Command)
        {
            bool[] bits = new bool[32];


            bits[0] = Command.AtuadorR.automatico;
            bits[1] = Command.AtuadorR.manual;
            bits[2] = Command.AtuadorR.Reset;
            bits[3] = Command.AtuadorR.Manutencao;
            bits[4] = Command.AtuadorR.Bloqueado;
            bits[5] = Command.AtuadorR.Sem_Bloqueio;
            bits[6] = Command.AtuadorR.Emergencia;
            bits[7] = Command.AtuadorR.bit07;
            bits[8] = Command.AtuadorR.Habilitado_Ligar_Rota;
            bits[9] = Command.AtuadorR.ligaManual;
            bits[10] = Command.AtuadorR.Abrindo;
            bits[11] = Command.AtuadorR.Fechando;
            bits[12] = Command.AtuadorR.Aberto;
            bits[13] = Command.AtuadorR.Fechado;
            bits[14] = Command.AtuadorR.Falha_Abrir;
            bits[15] = Command.AtuadorR.Falha_Fechar;
            bits[16] = Command.AtuadorR.Falha_Sem_Posicao;
            bits[17] = Command.AtuadorR.SensorAberto;
            bits[18] = Command.AtuadorR.SensorFechado;
            bits[19] = Command.AtuadorR.ErroGeral;
            bits[20] = Command.AtuadorR.Seleciona_Rota;
            bits[21] = Command.AtuadorD.bitReserva_1;
            bits[22] = Command.AtuadorD.bitReserva_2;
            bits[23] = Command.AtuadorD.bitReserva_3;
            bits[24] = Command.AtuadorD.bitReserva_4;
            bits[25] = Command.AtuadorD.bitReserva_5;
            bits[26] = Command.AtuadorD.bitReserva_6;
            bits[27] = Command.AtuadorD.bitReserva_7;
            bits[28] = Command.AtuadorD.bitReserva_8;
            bits[29] = Command.AtuadorD.bitReserva_9;
            bits[30] = Command.AtuadorD.bitReserva_10;
            bits[31] = Command.AtuadorD.bitReserva_11;

            Command.DWord = Conversions.Bit_To_Dword(ref bits, true);

            return Command.DWord;
        }




        //Type Atuador Analogico
        //=====================================================================================================================================

        public static Utilidades.VariaveisGlobais.type_All typeAtuadorA_TO_typeStandardGUI(Utilidades.VariaveisGlobais.type_All Command)
        {
            Command.Standard.Liga_Manual = Command.AtuadorA.ligaManual;
            Command.Standard.Manual = Command.AtuadorA.manual;
            Command.Standard.Automatico = Command.AtuadorA.automatico;
            Command.Standard.Manutencao = Command.AtuadorA.manutencao;
            Command.Standard.Libera_Bloqueio = Command.AtuadorA.Libera_Bloqueio_Manual;
            Command.Standard.Emergencia = Command.AtuadorA.emergencia;
            Command.Standard.Liberado = Command.AtuadorA.liberado;
            Command.Standard.AcionandoAutomatico = Command.AtuadorA.acionandoAutomatico;
            Command.Standard.FalhaConfirmacaoContatorLado1 = Command.AtuadorA.falhaConfirmacaoContatorLado1;
            Command.Standard.FalhaConfirmacaoContatorLado2 = Command.AtuadorA.falhaConfirmacaoContatorLado2;
            Command.Standard.falhaPosicionamento = Command.AtuadorA.falhaPosicionamento;
            Command.Standard.falhaLeituraPosicao = Command.AtuadorA.falhaLeituraPosicao;
            Command.Standard.Reset = Command.AtuadorA.reset;
            Command.Standard.AcionaLado1 = Command.AtuadorA.acionaLado1;
            Command.Standard.AcionaLado2 = Command.AtuadorA.acionaLado2;
            Command.Standard.Reposicionando = Command.AtuadorA.Reposicionando;
            return Command;

        }

        public static Utilidades.VariaveisGlobais.type_All typeStandardGUI_TO_typeAtuadorA(Utilidades.VariaveisGlobais.type_All Command)
        {
            Command.AtuadorA.ligaManual = Command.Standard.Liga_Manual;
            Command.AtuadorA.manual = Command.Standard.Manual;
            Command.AtuadorA.automatico = Command.Standard.Automatico;
            Command.AtuadorA.manutencao = Command.Standard.Manutencao;
            Command.AtuadorA.Libera_Bloqueio_Manual = Command.Standard.Libera_Bloqueio;
            Command.AtuadorA.emergencia = Command.Standard.Emergencia;
            Command.AtuadorA.liberado = Command.Standard.Liberado;
            Command.AtuadorA.acionandoAutomatico = Command.Standard.AcionandoAutomatico;
            Command.AtuadorA.falhaConfirmacaoContatorLado1 = Command.Standard.FalhaConfirmacaoContatorLado1;
            Command.AtuadorA.falhaConfirmacaoContatorLado2 = Command.Standard.FalhaConfirmacaoContatorLado2;
            Command.AtuadorA.falhaPosicionamento = Command.Standard.falhaPosicionamento;
            Command.AtuadorA.falhaLeituraPosicao = Command.Standard.falhaLeituraPosicao;
            Command.AtuadorA.reset = Command.Standard.Reset;
            Command.AtuadorA.acionaLado1 = Command.Standard.AcionaLado1;
            Command.AtuadorA.acionaLado2 = Command.Standard.AcionaLado2;
            Command.AtuadorA.Reposicionando = Command.Standard.Reposicionando;
            return Command;
        }

        public static Utilidades.VariaveisGlobais.type_All Dword_TO_typeAtuadorA(UInt32 DWord, Utilidades.VariaveisGlobais.type_All Command)
        {
            bool[] bits = new bool[32];

            Conversions.Dword_To_Bit(DWord, ref bits, true);

            //Atualiza DWord
            Command.DWord = DWord;

            Command.AtuadorA.ligaManual = bits[0];
            Command.AtuadorA.manual = bits[1];
            Command.AtuadorA.automatico = bits[2];
            Command.AtuadorA.manutencao = bits[3];
            Command.AtuadorA.Libera_Bloqueio_Manual = bits[4];
            Command.AtuadorA.emergencia = bits[5];
            Command.AtuadorA.liberado = bits[6];
            Command.AtuadorA.acionandoAutomatico = bits[7];
            Command.AtuadorA.falhaConfirmacaoContatorLado1 = bits[8];
            Command.AtuadorA.falhaConfirmacaoContatorLado2 = bits[9];
            Command.AtuadorA.falhaPosicionamento = bits[10];
            Command.AtuadorA.falhaLeituraPosicao = bits[11];
            Command.AtuadorA.reset = bits[12];
            Command.AtuadorA.acionaLado1 = bits[13];
            Command.AtuadorA.acionaLado2 = bits[14];
            Command.AtuadorA.Reposicionando = bits[15];
            Command.AtuadorA.bitReserva_3 = bits[16];
            Command.AtuadorA.bitReserva_4 = bits[17];
            Command.AtuadorA.bitReserva_5 = bits[18];
            Command.AtuadorA.bitReserva_6 = bits[19];
            Command.AtuadorA.bitReserva_7 = bits[20];
            Command.AtuadorA.bitReserva_8 = bits[21];
            Command.AtuadorA.bitReserva_9 = bits[22];
            Command.AtuadorA.bitReserva_10 = bits[23];
            Command.AtuadorA.bitReserva_11 = bits[24];
            Command.AtuadorA.bitReserva_12 = bits[25];
            Command.AtuadorA.bitReserva_13 = bits[26];
            Command.AtuadorA.bitReserva_14 = bits[27];
            Command.AtuadorA.bitReserva_15 = bits[28];
            Command.AtuadorA.bitReserva_16 = bits[29];
            Command.AtuadorA.bitReserva_17 = bits[30];
            Command.AtuadorA.bitReserva_18 = bits[31];

            return Command;
        }

        public static UInt32 typeAtuadorA_TO_Dword(Utilidades.VariaveisGlobais.type_All Command)
        {
            bool[] bits = new bool[32];

            bits[0] = Command.AtuadorA.ligaManual;
            bits[1] = Command.AtuadorA.manual;
            bits[2] = Command.AtuadorA.automatico;
            bits[3] = Command.AtuadorA.manutencao;
            bits[4] = Command.AtuadorA.Libera_Bloqueio_Manual;
            bits[5] = Command.AtuadorA.emergencia;
            bits[6] = Command.AtuadorA.liberado;
            bits[7] = Command.AtuadorA.acionandoAutomatico;
            bits[8] = Command.AtuadorA.falhaConfirmacaoContatorLado1;
            bits[9] = Command.AtuadorA.falhaConfirmacaoContatorLado1;
            bits[10] = Command.AtuadorA.falhaPosicionamento;
            bits[11] = Command.AtuadorA.falhaLeituraPosicao;
            bits[12] = Command.AtuadorA.reset;
            bits[13] = Command.AtuadorA.acionaLado1;
            bits[14] = Command.AtuadorA.acionaLado2;
            bits[15] = Command.AtuadorA.Reposicionando;
            bits[16] = Command.AtuadorA.bitReserva_3;
            bits[17] = Command.AtuadorA.bitReserva_4;
            bits[18] = Command.AtuadorA.bitReserva_5;
            bits[19] = Command.AtuadorA.bitReserva_6;
            bits[20] = Command.AtuadorA.bitReserva_7;
            bits[21] = Command.AtuadorA.bitReserva_8;
            bits[22] = Command.AtuadorA.bitReserva_9;
            bits[23] = Command.AtuadorA.bitReserva_10;
            bits[24] = Command.AtuadorA.bitReserva_11;
            bits[25] = Command.AtuadorA.bitReserva_12;
            bits[26] = Command.AtuadorA.bitReserva_13;
            bits[27] = Command.AtuadorA.bitReserva_14;
            bits[28] = Command.AtuadorA.bitReserva_15;
            bits[29] = Command.AtuadorA.bitReserva_16;
            bits[30] = Command.AtuadorA.bitReserva_17;
            bits[31] = Command.AtuadorA.bitReserva_18;

            Command.DWord = Conversions.Bit_To_Dword(ref bits, true);

            return Command.DWord;
        }

        //Command Complemento
        //=====================================================================================================================================
        public static VariaveisGlobais.Complemento ByteToComplementoBatelada(byte _byte, VariaveisGlobais.Complemento complemento)
        {
            bool[] bits = new bool[8];

            Conversions.Byte_To_Bit(_byte, ref bits);

            complemento.Habilitado_Inicio_Dosagem = bits[0];
            complemento.Botao_Inicio_Fim_Dosagem_IHM = bits[1];
            complemento.Item_Atual_Iniciado_Dosagem = bits[2];
            complemento.Item_Atual_Finalizado_Dosagem = bits[3];
            complemento.Finalizado_Dosagem_Complementos = bits[4];
            complemento.Supervisao_Salvou_Dados_Dosado_Item_Atual = bits[5];
            complemento.Reserva_1 = bits[6];
            complemento.Reserva_2 = bits[7];

            return complemento;
        }


        public static byte ComplementoToByteBatelada(VariaveisGlobais.Complemento complemento)
        {
            bool[] bits = new bool[8];

            bits[0] = complemento.Habilitado_Inicio_Dosagem;
            bits[1] = complemento.Botao_Inicio_Fim_Dosagem_IHM;
            bits[2] = complemento.Item_Atual_Iniciado_Dosagem;
            bits[3] = complemento.Item_Atual_Finalizado_Dosagem;
            bits[4] = complemento.Finalizado_Dosagem_Complementos;
            bits[5] = complemento.Supervisao_Salvou_Dados_Dosado_Item_Atual;
            bits[6] = complemento.Reserva_1;
            bits[7] = complemento.Reserva_2;

            return Conversions.Bit_To_Byte(ref bits);
        }


        //Command Slot Produção
        //=====================================================================================================================================
        public static VariaveisGlobais.SlotBatelada DwordToSlotBatelada(UInt32 DWord, VariaveisGlobais.SlotBatelada slot)
        {
            bool[] bits = new bool[32];

            Conversions.Dword_To_Bit(DWord, ref bits, true);

            slot.Solicita_Nova_Batelada = bits[0];
            slot.Iniciou_Producao_No_Processo = bits[1];
            slot.Finalizou_Producao_No_Processo = bits[2];
            slot.Producao_Carregou_Dados_Iniciais_Batelada_No_Processo = bits[3];
            slot.Liberado_Nova_Batelada = bits[4];
            slot.Supervisao_Carregou_Dados_Batelada = bits[5];
            slot.Finalizou_Dosagem_Automatica_Silo_1 = bits[6];
            slot.Finalizou_Dosagem_Automatica_Silo_2 = bits[7];
            slot.Supervisorio_Salvou_Dados_Silo_1 = bits[8];
            slot.Supervisorio_Salvou_Dados_Silo_2 = bits[9];
            slot.Dosar_Primeiro_Silo_2 = bits[10];
            slot.Reserva_8 = bits[11];
            slot.Reserva_9 = bits[12];
            slot.Reserva_10 = bits[13];
            slot.Reserva_11 = bits[14];
            slot.Reserva_12 = bits[15];
            slot.Reserva_13 = bits[16];
            slot.Reserva_14 = bits[17];
            slot.Reserva_15 = bits[18];
            slot.Reserva_16 = bits[19];
            slot.Reserva_17 = bits[20];
            slot.Reserva_18 = bits[21];
            slot.Reserva_19 = bits[22];
            slot.Reserva_20 = bits[23];
            slot.Reserva_21 = bits[24];
            slot.Reserva_22 = bits[25];
            slot.Reserva_23 = bits[26];
            slot.Reserva_24 = bits[27];
            slot.Reserva_25 = bits[28];
            slot.Reserva_26 = bits[29];
            slot.Reserva_27 = bits[30];
            slot.Reserva_28 = bits[31];

            return slot;
        }

        public static UInt32 SlotToDwordBatelada(VariaveisGlobais.SlotBatelada slot)
        {
            bool[] bits = new bool[32];

            bits[0] = slot.Solicita_Nova_Batelada;
            bits[1] = slot.Iniciou_Producao_No_Processo;
            bits[2] = slot.Finalizou_Producao_No_Processo;
            bits[3] = slot.Producao_Carregou_Dados_Iniciais_Batelada_No_Processo;
            bits[4] = slot.Liberado_Nova_Batelada;
            bits[5] = slot.Supervisao_Carregou_Dados_Batelada;
            bits[6] = slot.Finalizou_Dosagem_Automatica_Silo_1;
            bits[7] = slot.Finalizou_Dosagem_Automatica_Silo_2;
            bits[8] = slot.Supervisorio_Salvou_Dados_Silo_1;
            bits[9] = slot.Supervisorio_Salvou_Dados_Silo_2;
            bits[10] = slot.Dosar_Primeiro_Silo_2;
            bits[11] = slot.Reserva_8;
            bits[12] = slot.Reserva_9;
            bits[13] = slot.Reserva_10;
            bits[14] = slot.Reserva_11;
            bits[15] = slot.Reserva_12;
            bits[16] = slot.Reserva_13;
            bits[17] = slot.Reserva_14;
            bits[18] = slot.Reserva_15;
            bits[19] = slot.Reserva_16;
            bits[20] = slot.Reserva_17;
            bits[21] = slot.Reserva_18;
            bits[22] = slot.Reserva_19;
            bits[23] = slot.Reserva_20;
            bits[24] = slot.Reserva_21;
            bits[25] = slot.Reserva_22;
            bits[26] = slot.Reserva_23;
            bits[27] = slot.Reserva_24;
            bits[28] = slot.Reserva_25;
            bits[29] = slot.Reserva_26;
            bits[30] = slot.Reserva_27;
            bits[31] = slot.Reserva_28;

            return Conversions.Bit_To_Dword(ref bits, true);
        }


        //Commando Producao
        //=====================================================================================================================================
        public static VariaveisGlobais.ControleExecucaoProducao DwordToControleExecucaoProducao(UInt32 DWord, VariaveisGlobais.ControleExecucaoProducao execucaoProducao)
        {
            bool[] bits = new bool[32];

            Conversions.Dword_To_Bit(DWord, ref bits, true);

            execucaoProducao.Iniciar_Producao = bits[0];
            execucaoProducao.Habilitado_Iniciar_Nova_Producao = bits[1];
            execucaoProducao.RetiraProducao = bits[2];
            execucaoProducao.Reserva_2 = bits[3];
            execucaoProducao.Reserva_3 = bits[4];
            execucaoProducao.Reserva_4 = bits[5];
            execucaoProducao.Reserva_5 = bits[6];
            execucaoProducao.Reserva_6 = bits[7];
            execucaoProducao.Reserva_7 = bits[8];
            execucaoProducao.Reserva_8 = bits[9];
            execucaoProducao.Reserva_9 = bits[10];
            execucaoProducao.Reserva_10 = bits[11];
            execucaoProducao.Reserva_11 = bits[12];
            execucaoProducao.Reserva_12 = bits[13];
            execucaoProducao.Reserva_13 = bits[14];
            execucaoProducao.Reserva_14 = bits[15];
            execucaoProducao.Reserva_15 = bits[16];
            execucaoProducao.Reserva_16 = bits[17];
            execucaoProducao.Reserva_17 = bits[18];
            execucaoProducao.Reserva_18 = bits[19];
            execucaoProducao.Reserva_19 = bits[20];
            execucaoProducao.Reserva_20 = bits[21];
            execucaoProducao.Reserva_21 = bits[22];
            execucaoProducao.Reserva_22 = bits[23];
            execucaoProducao.Reserva_23 = bits[24];
            execucaoProducao.Reserva_24 = bits[25];
            execucaoProducao.Reserva_25 = bits[26];
            execucaoProducao.Reserva_26 = bits[27];
            execucaoProducao.Reserva_27 = bits[28];
            execucaoProducao.Reserva_28 = bits[29];
            execucaoProducao.Reserva_29 = bits[30];
            execucaoProducao.Reserva_30 = bits[31];

            return execucaoProducao;
        }

        public static UInt32 ControleExecucaoProducaoToDword(VariaveisGlobais.ControleExecucaoProducao execucaoProducao)
        {
            bool[] bits = new bool[32];

            bits[0] = execucaoProducao.Iniciar_Producao;
            bits[1] = execucaoProducao.Habilitado_Iniciar_Nova_Producao;
            bits[2] = execucaoProducao.RetiraProducao;
            bits[3] = execucaoProducao.Reserva_2;
            bits[4] = execucaoProducao.Reserva_3;
            bits[5] = execucaoProducao.Reserva_4;
            bits[6] = execucaoProducao.Reserva_5;
            bits[7] = execucaoProducao.Reserva_6;
            bits[8] = execucaoProducao.Reserva_7;
            bits[9] = execucaoProducao.Reserva_8;
            bits[10] = execucaoProducao.Reserva_9;
            bits[11] = execucaoProducao.Reserva_10;
            bits[12] = execucaoProducao.Reserva_11;
            bits[13] = execucaoProducao.Reserva_12;
            bits[14] = execucaoProducao.Reserva_13;
            bits[15] = execucaoProducao.Reserva_14;
            bits[16] = execucaoProducao.Reserva_15;
            bits[17] = execucaoProducao.Reserva_16;
            bits[18] = execucaoProducao.Reserva_17;
            bits[19] = execucaoProducao.Reserva_18;
            bits[20] = execucaoProducao.Reserva_19;
            bits[21] = execucaoProducao.Reserva_20;
            bits[22] = execucaoProducao.Reserva_21;
            bits[23] = execucaoProducao.Reserva_22;
            bits[24] = execucaoProducao.Reserva_23;
            bits[25] = execucaoProducao.Reserva_24;
            bits[26] = execucaoProducao.Reserva_25;
            bits[27] = execucaoProducao.Reserva_26;
            bits[28] = execucaoProducao.Reserva_27;
            bits[29] = execucaoProducao.Reserva_28;
            bits[30] = execucaoProducao.Reserva_29;
            bits[31] = execucaoProducao.Reserva_30;

            return Conversions.Bit_To_Dword(ref bits, true);
        }

        //Command Ensaque
        //=====================================================================================================================================
        public static VariaveisGlobais.controleEnsaque DwordTocontroleEnsaque(UInt32 DWord, VariaveisGlobais.controleEnsaque controleEnsaque)
        {
            bool[] bits = new bool[32];

            Conversions.Dword_To_Bit(DWord, ref bits, true);

            controleEnsaque.IniciaEnsaque = bits[0];
            controleEnsaque.TerminaEnsaque = bits[1];
            controleEnsaque.Saco_Atual_Finalizado = bits[2];
            controleEnsaque.Saco_Atual_Dosando = bits[3];
            controleEnsaque.Supervisorio_Salvou_Saco_Atual = bits[4];
            controleEnsaque.HabilitaFinalizarEnsaque = bits[5];
            controleEnsaque.Habilita_Iniciar_Ensaque = bits[6];
            controleEnsaque.Reserva_4 = bits[7];
            controleEnsaque.Reserva_5 = bits[8];
            controleEnsaque.Reserva_6 = bits[9];
            controleEnsaque.Reserva_7 = bits[10];
            controleEnsaque.Reserva_8 = bits[11];
            controleEnsaque.Reserva_9 = bits[12];
            controleEnsaque.Reserva_10 = bits[13];
            controleEnsaque.Reserva_11 = bits[14];
            controleEnsaque.Reserva_12 = bits[15];
            controleEnsaque.Reserva_13 = bits[16];
            controleEnsaque.Reserva_14 = bits[17];
            controleEnsaque.Reserva_15 = bits[18];
            controleEnsaque.Reserva_16 = bits[19];
            controleEnsaque.Reserva_17 = bits[20];
            controleEnsaque.Reserva_18 = bits[21];
            controleEnsaque.Reserva_19 = bits[22];
            controleEnsaque.Reserva_20 = bits[23];
            controleEnsaque.Reserva_21 = bits[24];
            controleEnsaque.Reserva_22 = bits[25];
            controleEnsaque.Reserva_23 = bits[26];
            controleEnsaque.Reserva_24 = bits[27];
            controleEnsaque.Reserva_25 = bits[28];
            controleEnsaque.Reserva_26 = bits[29];
            controleEnsaque.Reserva_27 = bits[30];
            controleEnsaque.Reserva_28 = bits[31];

            return controleEnsaque;
        }

        public static UInt32 EnsaqueToDwordControleEnsaque(VariaveisGlobais.controleEnsaque controleEnsaque)
        {
            bool[] bits = new bool[32];

            bits[0] = controleEnsaque.IniciaEnsaque;
            bits[1] = controleEnsaque.TerminaEnsaque;
            bits[2] = controleEnsaque.Saco_Atual_Finalizado;
            bits[3] = controleEnsaque.Saco_Atual_Dosando;
            bits[4] = controleEnsaque.Supervisorio_Salvou_Saco_Atual;
            bits[5] = controleEnsaque.HabilitaFinalizarEnsaque;
            bits[6] = controleEnsaque.Habilita_Iniciar_Ensaque;
            bits[7] = controleEnsaque.Reserva_4;
            bits[8] = controleEnsaque.Reserva_5;
            bits[9] = controleEnsaque.Reserva_6;
            bits[10] = controleEnsaque.Reserva_7;
            bits[11] = controleEnsaque.Reserva_8;
            bits[12] = controleEnsaque.Reserva_9;
            bits[13] = controleEnsaque.Reserva_10;
            bits[14] = controleEnsaque.Reserva_11;
            bits[15] = controleEnsaque.Reserva_12;
            bits[16] = controleEnsaque.Reserva_13;
            bits[17] = controleEnsaque.Reserva_14;
            bits[18] = controleEnsaque.Reserva_15;
            bits[19] = controleEnsaque.Reserva_16;
            bits[20] = controleEnsaque.Reserva_17;
            bits[21] = controleEnsaque.Reserva_18;
            bits[22] = controleEnsaque.Reserva_19;
            bits[23] = controleEnsaque.Reserva_20;
            bits[24] = controleEnsaque.Reserva_21;
            bits[25] = controleEnsaque.Reserva_22;
            bits[26] = controleEnsaque.Reserva_23;
            bits[27] = controleEnsaque.Reserva_24;
            bits[28] = controleEnsaque.Reserva_25;
            bits[29] = controleEnsaque.Reserva_26;
            bits[30] = controleEnsaque.Reserva_27;
            bits[31] = controleEnsaque.Reserva_28;

            return Conversions.Bit_To_Dword(ref bits, true);
        }

        //Command Indicador de Pesagem
        //=====================================================================================================================================

        public static VariaveisGlobais.IndicadorPesagem ByteToIndicadorPesagem(byte _byte, VariaveisGlobais.IndicadorPesagem indicador)
        {
            bool[] bits = new bool[8];

            Conversions.Byte_To_Bit(_byte, ref bits);

            indicador.Comando_Zero = bits[0];
            indicador.Comando_Tara = bits[1];
            indicador.Erro_Leitura = bits[2];
            indicador.Balanca_Vazia_Manual = bits[3];
            indicador.Habilita_Setar_Balanca_Vazia = bits[4];
            indicador.Seta_Balanca_Vazia = bits[5];
            indicador.Reserva_4 = bits[6];
            indicador.Reserva_5 = bits[7];

            return indicador;
        }

        public static byte IndicadorPesagemToByte(VariaveisGlobais.IndicadorPesagem indicador)
        {
            bool[] bits = new bool[8];

            bits[0] = indicador.Comando_Zero;
            bits[1] = indicador.Comando_Tara;
            bits[2] = indicador.Erro_Leitura;
            bits[3] = indicador.Balanca_Vazia_Manual;
            bits[4] = indicador.Habilita_Setar_Balanca_Vazia;
            bits[5] = indicador.Seta_Balanca_Vazia;
            bits[6] = indicador.Reserva_4;
            bits[7] = indicador.Reserva_5;

            return Conversions.Bit_To_Byte(ref bits);
        }
             
        //Command Profinet
        //=====================================================================================================================================
        public static Utilidades.VariaveisGlobais.diagnosticoProfinet WordToDiagnosticoProfinet(UInt16 Word,  VariaveisGlobais.diagnosticoProfinet diagnosticoProfinet)
        {
            bool[] bits = new bool[16];

            Conversions.Word_To_Bit(Word, ref bits, false);


            diagnosticoProfinet.Good = bits[0];
            diagnosticoProfinet.Disabled = bits[1];
            diagnosticoProfinet.Maintenancerequired = bits[2];
            diagnosticoProfinet.Maintenancedemanded = bits[3];
            diagnosticoProfinet.Error = bits[4];
            diagnosticoProfinet.Hardwarecomponentnotreachable = bits[5];
            diagnosticoProfinet.Qualified = bits[6];
            diagnosticoProfinet.IODatanotavailable = bits[7];
            diagnosticoProfinet.Reserved = bits[8];
            diagnosticoProfinet.Reserved1 = bits[9];
            diagnosticoProfinet.Reserved2 = bits[10];
            diagnosticoProfinet.Reserved3 = bits[11];
            diagnosticoProfinet.Reserved4 = bits[12];
            diagnosticoProfinet.Reserved5 = bits[13];
            diagnosticoProfinet.Reserved6 = bits[14];
            


            return diagnosticoProfinet;
        }

        public static UInt16 DiagToWordProfinet(VariaveisGlobais.diagnosticoProfinet diagnosticoProfinet)
        {
            bool[] bits = new bool[16];

            bits[0] = diagnosticoProfinet.Good;
            bits[1] = diagnosticoProfinet.Disabled;
            bits[2] = diagnosticoProfinet.Maintenancerequired;
            bits[3] = diagnosticoProfinet.Maintenancedemanded;
            bits[4] = diagnosticoProfinet.Error;
            bits[5] = diagnosticoProfinet.Hardwarecomponentnotreachable;
            bits[6] = diagnosticoProfinet.Qualified;
            bits[7] = diagnosticoProfinet.IODatanotavailable;
            bits[8] = diagnosticoProfinet.Reserved;
            bits[9] = diagnosticoProfinet.Reserved1;
            bits[10] = diagnosticoProfinet.Reserved2;
            bits[11] = diagnosticoProfinet.Reserved3;
            bits[12] = diagnosticoProfinet.Reserved4;
            bits[13] = diagnosticoProfinet.Reserved5;
            bits[14] = diagnosticoProfinet.Reserved6;
       

            return Conversions.Bit_To_Word(ref bits, true);
        }


        //Command Auxiliares Processo
        //=====================================================================================================================================
        public static VariaveisGlobais.AuxiliaresProcesso DwordTocontroleAuxiliaresProcesso(UInt32 DWord, VariaveisGlobais.AuxiliaresProcesso auxiliaresProcesso )
        {
            bool[] bits = new bool[32];

            Conversions.Dword_To_Bit(DWord, ref bits, true);

            auxiliaresProcesso.Set_Automatico_Equipamentos = bits[0];
            auxiliaresProcesso.Habilita_Finalizar_Dosagem_E_Iniciar_Transporte = bits[1];
            auxiliaresProcesso.Seta_Finalizar_Dosagem_E_Inicia_Transporte = bits[2];
            auxiliaresProcesso.Reserva_3 = bits[3];
            auxiliaresProcesso.Reserva_4 = bits[4];
            auxiliaresProcesso.Reserva_5 = bits[5];
            auxiliaresProcesso.Reserva_6 = bits[6];
            auxiliaresProcesso.Reserva_7 = bits[7];
            auxiliaresProcesso.Reserva_8 = bits[8];
            auxiliaresProcesso.Reserva_9 = bits[9];
            auxiliaresProcesso.Reserva_10 = bits[10];
            auxiliaresProcesso.Reserva_11 = bits[11];
            auxiliaresProcesso.Reserva_12 = bits[12];
            auxiliaresProcesso.Reserva_13 = bits[13];
            auxiliaresProcesso.Reserva_14 = bits[14];
            auxiliaresProcesso.Reserva_15 = bits[15];
            auxiliaresProcesso.Reserva_16 = bits[16];
            auxiliaresProcesso.Reserva_17 = bits[17];
            auxiliaresProcesso.Reserva_18 = bits[18];
            auxiliaresProcesso.Reserva_19 = bits[19];
            auxiliaresProcesso.Reserva_20 = bits[20];
            auxiliaresProcesso.Reserva_21 = bits[21];
            auxiliaresProcesso.Reserva_22 = bits[22];
            auxiliaresProcesso.Reserva_23 = bits[23];
            auxiliaresProcesso.Reserva_24 = bits[24];
            auxiliaresProcesso.Reserva_25 = bits[25];
            auxiliaresProcesso.Reserva_26 = bits[26];
            auxiliaresProcesso.Reserva_27 = bits[27];
            auxiliaresProcesso.Reserva_28 = bits[28];
            auxiliaresProcesso.Reserva_29 = bits[29];
            auxiliaresProcesso.Reserva_30 = bits[30];
            auxiliaresProcesso.Reserva_31 = bits[31];

            return auxiliaresProcesso;
        }

        public static UInt32 AuxiliaresProcessoToDword(VariaveisGlobais.AuxiliaresProcesso auxiliaresProcesso)
        {
            bool[] bits = new bool[32];

            bits[0] = auxiliaresProcesso.Set_Automatico_Equipamentos;
            bits[1] = auxiliaresProcesso.Habilita_Finalizar_Dosagem_E_Iniciar_Transporte;
            bits[2] = auxiliaresProcesso.Seta_Finalizar_Dosagem_E_Inicia_Transporte;
            bits[3] = auxiliaresProcesso.Reserva_3;
            bits[4] = auxiliaresProcesso.Reserva_4;
            bits[5] = auxiliaresProcesso.Reserva_5;
            bits[6] = auxiliaresProcesso.Reserva_6;
            bits[7] = auxiliaresProcesso.Reserva_7;
            bits[8] = auxiliaresProcesso.Reserva_8;
            bits[9] = auxiliaresProcesso.Reserva_9;
            bits[10] = auxiliaresProcesso.Reserva_10;
            bits[11] = auxiliaresProcesso.Reserva_11;
            bits[12] = auxiliaresProcesso.Reserva_12;
            bits[13] = auxiliaresProcesso.Reserva_13;
            bits[14] = auxiliaresProcesso.Reserva_14;
            bits[15] = auxiliaresProcesso.Reserva_15;
            bits[16] = auxiliaresProcesso.Reserva_16;
            bits[17] = auxiliaresProcesso.Reserva_17;
            bits[18] = auxiliaresProcesso.Reserva_18;
            bits[19] = auxiliaresProcesso.Reserva_19;
            bits[20] = auxiliaresProcesso.Reserva_20;
            bits[21] = auxiliaresProcesso.Reserva_21;
            bits[22] = auxiliaresProcesso.Reserva_22;
            bits[23] = auxiliaresProcesso.Reserva_23;
            bits[24] = auxiliaresProcesso.Reserva_24;
            bits[25] = auxiliaresProcesso.Reserva_25;
            bits[26] = auxiliaresProcesso.Reserva_26;
            bits[27] = auxiliaresProcesso.Reserva_27;
            bits[28] = auxiliaresProcesso.Reserva_28;
            bits[29] = auxiliaresProcesso.Reserva_29;
            bits[30] = auxiliaresProcesso.Reserva_30;
            bits[31] = auxiliaresProcesso.Reserva_31;

            return Conversions.Bit_To_Dword(ref bits, true);
        }


        //Command PID
        //=====================================================================================================================================

        public static VariaveisGlobais.PID ByteToPID(byte _byte, VariaveisGlobais.PID PID)
        {
            bool[] bits = new bool[8];

            Conversions.Byte_To_Bit(_byte, ref bits);

            PID.Habilita_PID = bits[0];
            PID.Reserva = bits[1];
            PID.Reserva_1 = bits[2];
            PID.Reserva_2 = bits[3];
            PID.Reserva_3 = bits[4];
            PID.Reserva_4 = bits[5];
            PID.Reserva_5 = bits[6];
            PID.Reserva_6 = bits[7];

            return PID;
        }

        public static byte PIDToByte(VariaveisGlobais.PID PID)
        {
            bool[] bits = new bool[8];

            bits[0] = PID.Habilita_PID;
            bits[1] = PID.Reserva;
            bits[2] = PID.Reserva_1;
            bits[3] = PID.Reserva_2;
            bits[4] = PID.Reserva_3;
            bits[5] = PID.Reserva_4;
            bits[6] = PID.Reserva_5;
            bits[7] = PID.Reserva_6;

            return Conversions.Bit_To_Byte(ref bits);
        }


        //Command Niveis
        //=====================================================================================================================================
        public static Utilidades.VariaveisGlobais.Niveis Dword_TO_NIveis(UInt32 DWord, Utilidades.VariaveisGlobais.Niveis Command)
        {
            bool[] bits = new bool[32];

            Conversions.Dword_To_Bit(DWord, ref bits, true);

            Command.Superior_Silo_1 = bits[0];
            Command.Superior_Silo_2 = bits[1];
            Command.Superior_Silo_Exp = bits[2];
            Command.Inferior_Silo_1 = bits[3];
            Command.Inferior_Silo_2 = bits[4];
            Command.Inferior_Silo_Exp = bits[5];
            Command.Inferior_Pre_Misturador = bits[6];
            Command.Inferior_Pos_Misturador = bits[7];
            Command.Reserva_2 = bits[8];
            Command.Reserva_3 = bits[9];
            Command.Reserva_4 = bits[10];
            Command.Reserva_5 = bits[11];
            Command.Reserva_6 = bits[12];
            Command.Reserva_7 = bits[13];
            Command.Reserva_8 = bits[14];
            Command.Reserva_9 = bits[15];
            Command.Reserva_10 = bits[16];
            Command.Reserva_11 = bits[17];
            Command.Reserva_12 = bits[18];
            Command.Reserva_13 = bits[19];
            Command.Reserva_14 = bits[20];
            Command.Reserva_15 = bits[21];
            Command.Reserva_16= bits[22];
            Command.Reserva_17 = bits[23];
            Command.Reserva_18 = bits[24];
            Command.Reserva_19= bits[25];
            Command.Reserva_20 = bits[26];
            Command.Reserva_21 = bits[27];
            Command.Reserva_22 = bits[28];
            Command.Reserva_23 = bits[29];
            Command.Reserva_24 = bits[30];
            Command.Reserva_25 = bits[31];

            return Command;
        }



    }
}
