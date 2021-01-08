//using _9567A_V00___PI.DataBase;
using _9567A_V00___PI.Teclados;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace _9567A_V00___PI.Utilidades
{
    public enum typeEquip { PD, INV, SS, Atuador, BF, TRF }
    public enum typeCommand { PD, INV, SS, Atuador_Digital, Atuador_Analogico }

    public class VariaveisGlobais
    {
        public static TelasAuxiliares.FirstLoading windowFirstLoading = new TelasAuxiliares.FirstLoading();

        #region Structs

        public struct Niveis
        {
            public bool Superior_Silo_1;
            public bool Superior_Silo_2;
            public bool Superior_Silo_Exp;
            public bool Inferior_Silo_1;
            public bool Inferior_Silo_2;
            public bool Inferior_Silo_Exp;
            public bool Inferior_Pre_Misturador;
            public bool Inferior_Pos_Misturador;
            public bool Reserva_2;
            public bool Reserva_3;
            public bool Reserva_4;
            public bool Reserva_5;
            public bool Reserva_6;
            public bool Reserva_7;
            public bool Reserva_8;
            public bool Reserva_9;
            public bool Reserva_10;
            public bool Reserva_11;
            public bool Reserva_12;
            public bool Reserva_13;
            public bool Reserva_14;
            public bool Reserva_15;
            public bool Reserva_16;
            public bool Reserva_17;
            public bool Reserva_18;
            public bool Reserva_19;
            public bool Reserva_20;
            public bool Reserva_21;
            public bool Reserva_22;
            public bool Reserva_23;
            public bool Reserva_24;
            public bool Reserva_25;
        }

        public struct PID 
        {
            public float SetPoint;
            public float Ki;
            public float kp;
            public float kd;

            public float limiteMaximo;
            public float limiteMinimo;

            //Palavra de Comando
            public bool Habilita_PID;
            public bool Reserva;
            public bool Reserva_1;
            public bool Reserva_2;
            public bool Reserva_3;
            public bool Reserva_4;
            public bool Reserva_5;
            public bool Reserva_6;
 
        }

        public struct AuxiliaresProcesso
        {
            //Region estrutura de máquinas
            public float Volume_Maximo_Silo1_2;                       //Maximo silo produto acabado
            public float Volume_Maximo_Balanca;                       //Maximo silo 1 materia prima.
            public float Volume_Maximo_Pre_Misturador;                //Maximo silo 2 materia prima.
            public float Volume_Maximo_Pos_Misturador;                //Maximo silo 2 materia prima.
            public float Peso_Maximo_Silo1_2;                         //Maximo pré misturador.
            public float Peso_Maximo_Balanca;                         //Maximo pré misturador.
            public float Peso_Maximo_Pre_Misturador;                  //Maximo pré misturador.
            public float Peso_Maximo_Pos_Misturador;                  //Maximo pré misturador.

            public int TempoPosMisturador;                  //Maximo pré misturador.
            public int TempoPreMisturador;                  //Maximo pré misturador.

            //Region estrutura dos registros 1 e  2 
            //Registro 1
            public float Abertura_Máxima_Silo1;                      //Maximo abertura sulo 1                                                             
            public float Inicio_Reducao_Silo1;                      //Inicio redução registro silo 1
            public float Fechamento_Antecipado_Silo1;               //Fechamento antecipado silo 1

            //Registro 2
            public float Abertura_Máxima_Silo2;                      //Maximo abertura sulo 2                                                             
            public float Inicio_Reducao_Silo2;                      //Inicio redução registro silo 2
            public float Fechamento_Antecipado_Silo2;               //Fechamento antecipado silo 2


            //Region Geral
            //Palavra de controle
                 
            public bool Set_Automatico_Equipamentos;                      
            public bool Habilita_Finalizar_Dosagem_E_Iniciar_Transporte;
            public bool Seta_Finalizar_Dosagem_E_Inicia_Transporte;
            public bool Reserva_3;
            public bool Reserva_4;
            public bool Reserva_5;
            public bool Reserva_6;
            public bool Reserva_7;
            public bool Reserva_8;
            public bool Reserva_9;
            public bool Reserva_10;
            public bool Reserva_11;
            public bool Reserva_12;
            public bool Reserva_13;
            public bool Reserva_14;
            public bool Reserva_15;
            public bool Reserva_16;
            public bool Reserva_17;
            public bool Reserva_18;
            public bool Reserva_19;
            public bool Reserva_20;
            public bool Reserva_21;
            public bool Reserva_22;
            public bool Reserva_23;
            public bool Reserva_24;
            public bool Reserva_25;
            public bool Reserva_26;
            public bool Reserva_27;
            public bool Reserva_28;
            public bool Reserva_29;
            public bool Reserva_30;
            public bool Reserva_31;

            public float ToleranciaMinimaDosagemBalança;      //Peso em percentual de 0 a 10 %
            public float reserva_Real_2;                      //Maximo abertura sulo 2    



        }

        public struct diagnosticoProfinet
        {
            public bool Good;
            public bool Disabled;
            public bool Maintenancerequired;
            public bool Maintenancedemanded;
            public bool Error;
            public bool Hardwarecomponentnotreachable;
            public bool Qualified;
            public bool IODatanotavailable;
            public bool Reserved;
            public bool Reserved1;
            public bool Reserved2;
            public bool Reserved3;
            public bool Reserved4;
            public bool Reserved5;
            public bool Reserved6;
            public bool Reserved7;

        }

        public struct controleEnsaque
        {
            public bool IniciaEnsaque;                       //Inicia o ensaque
            public bool TerminaEnsaque;                      //Termina o ensaque
            public bool Saco_Atual_Finalizado;               //Ensaque Concluido
            public bool Saco_Atual_Dosando;                  //Ensaque Concluido
            public bool Supervisorio_Salvou_Saco_Atual;      //Supervisório salvou os dados do saco no banco de dados
            public bool HabilitaFinalizarEnsaque;            //Habilita finalizar ensaque
            public bool Habilita_Iniciar_Ensaque;
            public bool Reserva_4;
            public bool Reserva_5;
            public bool Reserva_6;
            public bool Reserva_7;
            public bool Reserva_8;
            public bool Reserva_9;
            public bool Reserva_10;
            public bool Reserva_11;
            public bool Reserva_12;
            public bool Reserva_13;
            public bool Reserva_14;
            public bool Reserva_15;
            public bool Reserva_16;
            public bool Reserva_17;
            public bool Reserva_18;
            public bool Reserva_19;
            public bool Reserva_20;
            public bool Reserva_21;
            public bool Reserva_22;
            public bool Reserva_23;
            public bool Reserva_24;
            public bool Reserva_25;
            public bool Reserva_26;
            public bool Reserva_27;
            public bool Reserva_28;

            public float pesoDesejado;                   //Peso desejado por Saco
            public float pesoSacoAtual;                  //Peso ensacado no saco 


        }

        public struct Ensaque 
        {
            public controleEnsaque controleEnsaque;
            public int IdProducao;
        
        }

        public struct IndicadorPesagem
        {

            public float Valor_Atual_Indicador;

            public float Percentual_Habilita_Balanca_Vazia_Automatica;

            public float Percentual_Habilita_Balanca_Vazia_Manual;

            public bool Comando_Zero;
            public bool Comando_Tara;
            public bool Erro_Leitura;
            public bool Balanca_Vazia_Manual;
            public bool Habilita_Setar_Balanca_Vazia;
            public bool Seta_Balanca_Vazia;
            public bool Reserva_4;
            public bool Reserva_5;


        }

        public struct Complemento
        {
            public short Quantidade_Itens;                          //Qtd itens dosagem Manual
            public short Item_Atual;                                //Item atual
            public float Quantidade_Dosada_Item_Atual;              //Qtd dosada item Atual
            public float Quantidade_Total_Desejada;                 //Qtd total desejada
            public bool Habilitado_Inicio_Dosagem;                  //Habilita Iniciar a dosagem
            public bool Botao_Inicio_Fim_Dosagem_IHM;               //BT Inicia ou finaliza Dosagem
            public bool Item_Atual_Iniciado_Dosagem;                //Iniciado Dosagem item atual
            public bool Item_Atual_Finalizado_Dosagem;              //Finalizado Dosagem item atual
            public bool Finalizado_Dosagem_Complementos;            //Finalizado a dosagem de todos complementos
            public bool Supervisao_Salvou_Dados_Dosado_Item_Atual;  //Supervisao salvou os dados de dosagem do item atual
            public bool Reserva_1;                                  //Reserva
            public bool Reserva_2;                                  //Reserva
        }

        public struct SlotBatelada
        {
            public int Tempo_Pre_Mistura;                    //Tempo de pré Mistura
            public int Tempo_Pos_Mistura;                    //Tempo de pós Mistura
            public float Peso_Total_Batelada_Desejado;       //Peso total desejado da batelada
            public float Dosagem_Materia_Prima_Silo_1;       //Dosagem Materia Prima no Silo 1
            public float Dosagem_Materia_Prima_Silo_2;       //Dosagem Materia Prima no Silo 2
            public float Dosado_Materia_Prima_Silo_1;        //Dosado Materia Prima no Silo 1
            public float Dosado_Materia_Prima_Silo_2;        //Dosado Materia Prima no Silo 2

            public Complemento Complemento_Pre;              //Complemento ou Matéria Prima dosada manualmente na Balança
            public Complemento Complemento_Pos;              //Complemento ou Matéria Prima dosada manualmente na Pós Mistura

            public int TempoAtualDesdeIniciado;              //Tempo total desde iniciado a produção da batelada
            public int TempoAtualPasso;                      //Tempo atual de cada passo, como tempo em dosagem manual, tempo em pré mistura...

            public int TempoRestantePreMistura;              //Tempo restante da pré mistura
            public int TempoRestantePosMistura;              //Tempo restante da pos mistura
            public short Quantidade_Total_Produtos;          //Quantidade Total de produtos na batelada
            public short Produto_Atual_Em_Producao;          //Qual produto da lista de produtos esta em produção 
            public short Status_Produto_Atual_Em_Producao;   //Status do produto atual que esta em produção

            public short Status_Batelada;                      //Status Batelada 
            public short NumeroBatelada;                       //O numero da batelada em execução no Slot Solicitado

            // 0 - Sem batelada
            // 1 - Dosagem Matéria Prima Manual
            // 2 - Dosagem Matéria Prima Automática
            // 3 - Transporte Para Pré Mistura
            // 4 - Pré Mistura
            // 5 - Moagem e Transporte Pós Mistura
            // 6 - Pós Mistura
            // 7 - Transporte Para Produto Acabado

            //Palavra de commando
            public bool Solicita_Nova_Batelada;                                   //Slot Disponível para iniciar nova Batelada
            public bool Iniciou_Producao_No_Processo;                             //Iniciou batelada
            public bool Finalizou_Producao_No_Processo;                           //Finalizou batelada
            public bool Producao_Carregou_Dados_Iniciais_Batelada_No_Processo;    //Carregou os dados iniciais no processo para iniciar a dosagem manual e a dosagem automática.
            public bool Liberado_Nova_Batelada;                                   //Slot Disponível para iniciar nova Batelada
            public bool Supervisao_Carregou_Dados_Batelada;                       //Quando o Supervisorio carregou os dados da nova batelada
            public bool Finalizou_Dosagem_Automatica_Silo_1;                      //Quando o CLP finalizou de dosar automaticamente o silo 1
            public bool Finalizou_Dosagem_Automatica_Silo_2;                      //Quando o CLP finalizou de dosar automaticamente o silo 2
            public bool Supervisorio_Salvou_Dados_Silo_1;                         //Quando Supervisorio salvou os dados do silo 1
            public bool Supervisorio_Salvou_Dados_Silo_2;                         //Quando Supervisorio salvou os dados do silo 2
            public bool Dosar_Primeiro_Silo_2;
            public bool Reserva_8;
            public bool Reserva_9;
            public bool Reserva_10;
            public bool Reserva_11;
            public bool Reserva_12;
            public bool Reserva_13;
            public bool Reserva_14;
            public bool Reserva_15;
            public bool Reserva_16;
            public bool Reserva_17;
            public bool Reserva_18;
            public bool Reserva_19;
            public bool Reserva_20;
            public bool Reserva_21;
            public bool Reserva_22;
            public bool Reserva_23;
            public bool Reserva_24;
            public bool Reserva_25;
            public bool Reserva_26;
            public bool Reserva_27;
            public bool Reserva_28;


        }

        public struct ControleExecucaoProducao
        {
            public SlotBatelada Slot_1;
            public SlotBatelada Slot_2;
            public SlotBatelada Slot_3;

            public short Bateladas_Iniciadas;
            public short Bateladas_Finalizadas;

            //Palavra de commando
            public bool Iniciar_Producao;                    //Inica Produção
            public bool Habilitado_Iniciar_Nova_Producao;
            public bool RetiraProducao;
            public bool Reserva_2;
            public bool Reserva_3;
            public bool Reserva_4;
            public bool Reserva_5;
            public bool Reserva_6;
            public bool Reserva_7;
            public bool Reserva_8;
            public bool Reserva_9;
            public bool Reserva_10;
            public bool Reserva_11;
            public bool Reserva_12;
            public bool Reserva_13;
            public bool Reserva_14;
            public bool Reserva_15;
            public bool Reserva_16;
            public bool Reserva_17;
            public bool Reserva_18;
            public bool Reserva_19;
            public bool Reserva_20;
            public bool Reserva_21;
            public bool Reserva_22;
            public bool Reserva_23;
            public bool Reserva_24;
            public bool Reserva_25;
            public bool Reserva_26;
            public bool Reserva_27;
            public bool Reserva_28;
            public bool Reserva_29;
            public bool Reserva_30;

            
        }

        public struct type_SS
        {
            public type_SS(string value) : this()
            {
                offSet_SP_Manutencao = 4;
                offSet_HorimetroParcial = 8;
                offSet_HorimetroTotal = 12;
                offSet_Tempo_Limpeza = 16;
                offSet_Corrente_Atual = 20;
                offSet_SP_Corrente_Motor_Vazio = 24;
                offSet_SP_Tempo_Reversao = 28;
                offSet_Tempo_Reversao_Atual = 32;

            }

            public bool ligaManual;
            public bool manual;
            public bool automatico;
            public bool manutencao;
            public bool Libera_Bloqueio_Manual;
            public bool reset;
            public bool ligado;
            public bool ligando;
            public bool desligando;
            public bool liberado;
            public bool falhaPartidaNaoConfirmou;
            public bool falhaContatorDesligou;
            public bool falhaDisjuntoDesligou;
            public bool falhaPartidaNaoDesligou;
            public bool resetHorimetroTotal;
            public bool resetHorimetroParcial;
            public bool emergencia;
            public bool disjuntorDesligado;
            public bool tempoManutencao;
            public bool falhaGeral;
            public bool inverterSentidoGiro;
            public bool sentidoGiro;
            public bool confirmaSentidoGiroAlimentador;
            public bool SentidoGiroMotorMudou;
            public bool Sensor_Alimentador_Sentido_Horario;
            public bool Sensor_Alimentador_Sentido_Anti_Horario;
            public bool bitReserva_4;
            public bool bitReserva_5;
            public bool bitReserva_6;
            public bool bitReserva_7;
            public bool bitReserva_8;
            public bool bitReserva_9;
            public Int32 SP_Manutencao;
            public Int32 HorimetroParcial;
            public Int32 HorimetroTotal;
            public Int32 Tempo_Limpeza;
            public float Corrente_Atual;
            public float SP_Corrente_Motor_Vazio;
            public Int32 SP_Tempo_Reversao;
            public Int32 Tempo_Reversao_Atual;

            public int offSet_SP_Manutencao;
            public int offSet_HorimetroParcial;
            public int offSet_HorimetroTotal;
            public int offSet_Tempo_Limpeza;
            public int offSet_Corrente_Atual;
            public int offSet_SP_Corrente_Motor_Vazio;
            public int offSet_SP_Tempo_Reversao;
            public int offSet_Tempo_Reversao_Atual;

        }

        public struct type_PD
        {

            public type_PD(string value) : this()
            {
                offSet_SP_Manutencao = 4;
                offSet_HorimetroParcial = 8;
                offSet_HorimetroTotal = 12;
                offSet_Tempo_Limpeza = 16;
            }

            public bool ligaManual;
            public bool manual;
            public bool automatico;
            public bool manutencao;
            public bool Libera_Bloqueio_Manual;
            public bool reset;
            public bool ligado;
            public bool ligando;
            public bool desligando;
            public bool liberado;
            public bool falhaPartidaNaoConfirmou;
            public bool falhaContatorDesligou;
            public bool falhaDisjuntoDesligou;
            public bool falhaPartidaNaoDesligou;
            public bool resetHorimetroTotal;
            public bool resetHorimetroParcial;
            public bool emergencia;
            public bool disjuntorDesligado;
            public bool tempoManutencao;
            public bool falhaGeral;
            public bool bitReserva;
            public bool bitReserva_1;
            public bool bitReserva_2;
            public bool bitReserva_3;
            public bool bitReserva_4;
            public bool bitReserva_5;
            public bool bitReserva_6;
            public bool bitReserva_7;
            public bool bitReserva_8;
            public bool bitReserva_9;
            public bool bitReserva_10;
            public bool bitReserva_11;

            public Int32 SP_Manutencao;
            public Int32 HorimetroParcial;
            public Int32 HorimetroTotal;
            public Int32 Tempo_Limpeza;

            public int offSet_SP_Manutencao;
            public int offSet_HorimetroParcial;
            public int offSet_HorimetroTotal;
            public int offSet_Tempo_Limpeza;

        }

        public struct type_INV
        {
            public type_INV(string value) : this()
            {
                offSet_Velocidade_Manual = 4;
                offSet_Velocidade_Automatica_Solicita = 8;
                offSet_Velocidade_Atual = 12;
                offSet_Velocidade_Nominal = 16;
                offSet_SP_Corrente_Motor_Vazio = 20;
                offSet_Corrente_Atual = 24;
                offSet_Codigo_Alarme = 28;
                offSet_Codigo_Falha = 32;
                offSet_SP_Manutencao = 36;
                offSet_HorimetroParcial = 40;
                offSet_HorimetroTotal = 44;
                offSet_Tempo_Limpeza = 48;
            }

            public bool ligaManual;
            public bool manual;
            public bool automatico;
            public bool manutencao;
            public bool Libera_Bloqueio_Manual;
            public bool reset;
            public bool ligado;
            public bool ligando;
            public bool desligando;
            public bool liberado;
            public bool resetHorimetroTotal;
            public bool resetHorimetroParcial;
            public bool emergencia;
            public bool disjuntorDesligado;
            public bool tempoManutencao;
            public bool falha;
            public bool bitReserva;
            public bool bitReserva_1;
            public bool bitReserva_2;
            public bool bitReserva_3;
            public bool bitReserva_4;
            public bool bitReserva_5;
            public bool bitReserva_6;
            public bool bitReserva_7;
            public bool bitReserva_8;
            public bool bitReserva_9;
            public bool bitReserva_10;
            public bool bitReserva_11;
            public bool bitReserva_12;
            public bool bitReserva_13;
            public bool bitReserva_14;
            public bool bitReserva_15;
            public float Velocidade_Manual;
            public float Velocidade_Automatica_Solicita;
            public float Velocidade_Atual;
            public float Velocidade_Nominal;
            public float SP_Corrente_Motor_Vazio;
            public float Corrente_Atual;
            public float Codigo_Alarme;
            public float Codigo_Falha;
            public Int32 SP_Manutencao;
            public Int32 HorimetroParcial;
            public Int32 HorimetroTotal;
            public Int32 Tempo_Limpeza;

            public int offSet_Velocidade_Manual;
            public int offSet_Velocidade_Automatica_Solicita;
            public int offSet_Velocidade_Atual;
            public int offSet_Velocidade_Nominal;
            public int offSet_SP_Corrente_Motor_Vazio;
            public int offSet_Corrente_Atual;
            public int offSet_Codigo_Alarme;
            public int offSet_Codigo_Falha;
            public int offSet_SP_Manutencao;
            public int offSet_HorimetroParcial;
            public int offSet_HorimetroTotal;
            public int offSet_Tempo_Limpeza;
        }

        public struct type_AtuadorDigital
        {
            public bool manual;
            public bool automatico;
            public bool manutencao;
            public bool Libera_Bloqueio_Manual;
            public bool emergencia;
            public bool liberado;
            public bool acionaLado1;
            public bool acionaLado2;
            public bool acionandoLado1;
            public bool acionandoLado2;
            public bool acionandoAutomatico;
            public bool emPosicaoLado1;
            public bool emPosicaoLado2;
            public bool falhaAcionandoLado1;
            public bool falhaAcionandoLado2;
            public bool falha2PosicoesAtiva;
            public bool falhaConfirmacaoContatorLado1;
            public bool falhaConfirmacaoContatorLado2;
            public bool reset;
            public bool bitReserva;
            public bool bitReserva_1;
            public bool bitReserva_2;
            public bool bitReserva_3;
            public bool bitReserva_4;
            public bool bitReserva_5;
            public bool bitReserva_6;
            public bool bitReserva_7;
            public bool bitReserva_8;
            public bool bitReserva_9;
            public bool bitReserva_10;
            public bool bitReserva_11;
            public bool bitReserva_12;

        }

        public struct type_AtuadorAnalogico
        {

            public type_AtuadorAnalogico(string value) : this()
            {
                offSet_PosicaoSolicitadaAutomatico = 4;
                offSet_SP_Posicao_Manual = 8;
                offSet_PosicaoAtual = 12;
            }

            public bool ligaManual;
            public bool manual;
            public bool automatico;
            public bool manutencao;
            public bool Libera_Bloqueio_Manual;
            public bool emergencia;
            public bool liberado;
            public bool acionandoAutomatico;
            public bool falhaConfirmacaoContatorLado1;
            public bool falhaConfirmacaoContatorLado2;
            public bool falhaPosicionamento;
            public bool falhaLeituraPosicao;
            public bool reset;
            public bool acionaLado1;
            public bool acionaLado2;
            public bool Reposicionando;
            public bool bitReserva_3;
            public bool bitReserva_4;
            public bool bitReserva_5;
            public bool bitReserva_6;
            public bool bitReserva_7;
            public bool bitReserva_8;
            public bool bitReserva_9;
            public bool bitReserva_10;
            public bool bitReserva_11;
            public bool bitReserva_12;
            public bool bitReserva_13;
            public bool bitReserva_14;
            public bool bitReserva_15;
            public bool bitReserva_16;
            public bool bitReserva_17;
            public bool bitReserva_18;

            public float PosicaoSolicitadaAutomatico;
            public float SP_Posicao_Manual;
            public float PosicaoAtual;

            public int offSet_PosicaoSolicitadaAutomatico;
            public int offSet_SP_Posicao_Manual;
            public int offSet_PosicaoAtual;
        }

        //Padrão de bits, com todos os bits de todas as UDTs que existem no clp relacionada a equipamentos.
        public struct type_All
        {
            public type_All(string value) : this()
            {
                PD = new type_PD("");
                INV = new type_INV("");
                SS = new type_SS("");
                AtuadorA = new type_AtuadorAnalogico("");
            }

            public UInt32 DWord;                  //Dword bits palavra de comando e status
            public type_Standard_GUI Standard;   //Tipo de estrutura padrão para GUI
                                                 //
            public type_PD PD;                   //Tipo de estrutura PD_PCM
            
            public type_INV INV;
            public type_SS SS;
            public type_AtuadorDigital AtuadorD;
            public type_AtuadorAnalogico AtuadorA;

            public int initialOffSet;
            public int bufferPlc;
            public string Nome;                  //Nome
            public string Tag;                   //Tag
            public string NumeroPartida;         //Numero da Partida
            public string PaginaProjeto;         //Pagina do Projeto

            public typeEquip TypeEquip;          //tipo do equipamento
            public typeCommand TypeCommand;      //tipo de palavra que o equipamento ira utilizar

         
        }

        public struct type_Standard_GUI
        {
            public bool Emergencia;

            //Comandos Partidas
            public bool Liga_Manual;
            public bool Manual;
            public bool Automatico;
            public bool Manutencao;
            public bool Libera_Bloqueio;
            public bool Reset;
            public bool inverterSentidoGiro;
            public bool confirmaSentidoGiroAlimentador;
            public bool SentidoGiroMotorMudou;
            public bool Sensor_Alimentador_Sentido_Horario;
            public bool Sensor_Alimentador_Sentido_Anti_Horario;

            //Comandos Atuadores
            public bool AcionaLado1;
            public bool AcionaLado2;
            public bool Reposicionando;

            //Status Partidas
            public bool Ligado;
            public bool Ligando;
            public bool Desligando;
            public bool Liberado;
            public bool SentidoGiro;
            public bool AcionandoLado1;
            public bool AcionandoLado2;
            public bool AcionandoAutomatico;
            public bool EmPosicaoLado1;
            public bool EmPosicaoLado2;

            //Falhas Partidas
            public bool Falha_Geral;
            public bool Falha_Partida_Nao_Confirmou;
            public bool Falha_Contator_Desligou;
            public bool Falha_Disjuntor_Desligou;
            public bool Falha_Partida_Nao_Desligou;
            public bool Disjuntor_Desligado;

            //Falhas Atuadores
            public bool FalhaAcionandoLado1;
            public bool FalhaAcionandoLado2;
            public bool Falha2PosicoesAtiva;
            public bool FalhaConfirmacaoContatorLado1;
            public bool FalhaConfirmacaoContatorLado2;

            public bool falhaPosicionamento;
            public bool falhaLeituraPosicao;

            //Resets
            public bool Reset_Timer;
            public bool Reset_Timer_Total;

            //Utilidades
            public bool Tempo_Manutencao;
        }

        public struct type_BufferPLC
        {
            public string Name;         //Nome do DB que esta no clp e alguma descrição
            public byte[] Buffer;       //Array de buffer
            public int DBNumber;        //Número do DB
            public int Start;           //Inicio de leitura e escrita do DB
            public int Size;            //Tamanho do Buffer
            public int Result;          //List error de retorno
            public bool Enable_Read;    //Habilita leitura
            public bool Enable_Write;   //Habilita Escrita
            public bool OnlyWrite;      //Buffer somente de escrita

        }

        public struct typeUsers
        {
            public string userLogged;
            public string groupUserLogged;
            public string passwordLogged;
            public int numberOfGroup;

            public typeUsers(string v1, string v2, string v3, int v4) : this()
            {
                userLogged = v1;
                groupUserLogged = v2;
                passwordLogged = v3;
                numberOfGroup = v4;
            }
        }

        #endregion

        #region Mensagens Padrões

        public static string faltaPermissaoMessage = "Permissão insuficiente para realizar esse tipo de operação!";
        public static string faltaUsuarioMessage = "Falta realizar o login!";

        public static string faltaPermissaoTitle = "Permissão insuficiente";
        public static string faltaUsuarioTitle = "Falta realizar o login";

        #endregion

        #region Telas Supervisão 

        public static TelasAuxiliares.Buffer_Diag Window_Buffer_Diagnostic = new TelasAuxiliares.Buffer_Diag();

        public static TelasAuxiliares.Diagnosticos Window_Diagnostic = new TelasAuxiliares.Diagnosticos();

        public static Telas_Fluxo.Fluxo Fluxo = new Telas_Fluxo.Fluxo();

        //public static Usuarios.controleUsuario controleUsuario = new Usuarios.controleUsuario();

        //public static Telas_Fluxo.manutencao manutencao = new Telas_Fluxo.manutencao();

        //public static Telas_Fluxo.configuracoes configuracoes = new Telas_Fluxo.configuracoes();

        //public static Telas_Fluxo.producao producao = new Telas_Fluxo.producao();

        //public static Telas_Fluxo.receitas receitas = new Telas_Fluxo.receitas();

        //public static Telas_Fluxo.relatorios relatorios = new Telas_Fluxo.relatorios();

        //public static Telas_Fluxo.Controle_Produção.controleBalanca telabalanca = new Telas_Fluxo.Controle_Produção.controleBalanca(0, 384);

        #endregion

        #region Connection PLC

        //Cria comunicação com CLP
        public static Comunicacao.CallCommunicationPLC CommunicationPLC = new Comunicacao.CallCommunicationPLC(0, 10);

        private static string IP_Plc = "192.168.0.104";

        private static int Rack_PLC = 0;
        private static int Slot_PLC = 1;

        public static string IP_Plc_GS { get => IP_Plc; set => IP_Plc = value; }
        public static int Rack_PLC_GS { get => Rack_PLC; set => Rack_PLC = value; }
        public static int Slot_PLC_GS { get => Slot_PLC; set => Slot_PLC = value; }

        public static type_BufferPLC[] Buffer_PLC = new type_BufferPLC[50];

        #endregion

        #region Pastas

        private static string folderSql = @"C:\SQLCe"; //nome do diretorio a ser criado
        private static string folderLogs = @"C:\Logs";


        #endregion

        #region Connection DB and Type DB (SQLExpress or SqlCe)

        static bool DB_Connected;
        private static bool SQLCe;
        private static string Connection_DB_Create = @"Server=172.16.1.88\AVELL_AUTOMASUL\automasul,1433;Integrated Security=false;User ID=sa;Password=33162600";
        private static string Connection_DB_Users = @"Server=172.16.1.88\AVELL_AUTOMASUL\automasul,1433;Database=DB_Users_IHM;Integrated Security=false;User ID=sa;Password=33162600";
        private static string Connection_DB_Equip = @"Server=172.16.1.88\AVELL_AUTOMASUL\automasul,1433;Database=DB_Equips_IHM;Integrated Security=false;User ID=sa;Password=33162600";
        private static string Connection_DB_Producao = @"Server=172.16.1.88\AVELL_AUTOMASUL\automasul,1433;Database=DB_Producao_IHM;Integrated Security=false;User ID=sa;Password=33162600";

        public static string Connection_DB_Users_GS { get => Connection_DB_Users; set => Connection_DB_Users = value; }
        public static string Connection_DB_Equip_GS { get => Connection_DB_Equip; set => Connection_DB_Equip = value; }
        public static string Connection_DB_Producao_GS { get => Connection_DB_Producao; set => Connection_DB_Producao = value; }

        public static string Connection_DB_Create_GS { get => Connection_DB_Create; set => Connection_DB_Create = value; }

        public static bool SQLCe_GS { get => SQLCe; set => SQLCe = value; }

        public static bool DB_Connected_GS
        {
            get { return DB_Connected; }
            set
            {
                DB_Connected = value;
            }
        }

        #endregion

        #region Controle de Usuário

        static typeUsers Users = new typeUsers(UserLogged_GS = "", GroupUserLogged_GS = "", PasswordLogged_GS = "", NumberOfGroup_GS = 0);

        public static string UserLogged_GS { get => Users.userLogged; set => Users.userLogged = value; }
        public static string GroupUserLogged_GS { get => Users.groupUserLogged; set => Users.groupUserLogged = value; }
        public static string PasswordLogged_GS { get => Users.passwordLogged; set => Users.passwordLogged = value; }
        public static int NumberOfGroup_GS { get => Users.numberOfGroup; set => Users.numberOfGroup = value; }


        #endregion

        #region Cor de Fundo Icones

        public static SolidColorBrush Verde = new SolidColorBrush(Colors.Green);

        public static SolidColorBrush Branco = new SolidColorBrush(Colors.White);

        #endregion

        public static void createFolder(string folder) 
        {
          
            //Se o diretório não existir...

            if (!Directory.Exists(folder))
            {
                //Criamos um com o nome folder
                Directory.CreateDirectory(folder);
            }

        }

        public static void Load_Connection()
        {
            SQLCe_GS = false;

            DataBase.SqlGlobalFuctions.Create_DB("DB_Users_IHM", Connection_DB_Create);
            DataBase.SqlGlobalFuctions.Create_DB("DB_Equips_IHM", Connection_DB_Create);
            DataBase.SqlGlobalFuctions.Create_DB("DB_Producao_IHM", Connection_DB_Create);

            DB_Connected_GS = true;


        }

        public static List<Produto> listProdutos = new List<Produto>();

        public static List<Receita> listReceitas = new List<Receita>();

        public static Receita ReceitaCadastro = new Receita();

        public static Producao ProducaoReceita = new Producao();

        public static List<Producao> PesquisaProducao = new List<Producao>();

        public static AuxiliaresProcesso auxiliaresProcesso = new AuxiliaresProcesso();

        public static IndicadorPesagem indicadorPesagem = new IndicadorPesagem();

        public static PID controlePID = new PID();

        public static Niveis niveis = new Niveis();

        #region Contole teclado

        private static bool ativaTecladoVirtual = false;

        public static bool AtivaDesativaTecladoVirtual { get => ativaTecladoVirtual; set => ativaTecladoVirtual = value; }

        public static float floatingKeypad(string OldValue, short numeroMaximoCasas)
        {
            keypad mainWindow = new keypad(false, numeroMaximoCasas);
            float floatPoint;
            float oldValue = 0;
            float newValue = 0;
            bool isNumeric;
            isNumeric = float.TryParse(OldValue, out floatPoint);

            if (isNumeric)
            {
                //Recebe Valor antigo digitado no Textbox
                oldValue = Convert.ToSingle(OldValue);
                //Recebe o novo valor digitado no Keypad
            }

            if (mainWindow.ShowDialog() == true)
            {
                isNumeric = float.TryParse(mainWindow.Result, out floatPoint);

                if (isNumeric)
                {
                    newValue = floatPoint;

                    if (oldValue != newValue)
                    {
                        //Retira o foco do textbox.
                        Keyboard.ClearFocus();
                        return newValue;

                    }
                }
                else
                {
                    //Envia o oldValue pois o valor máximo ultrapassou o limite.
                    return oldValue;
                }

            }

            //Envia o oldValue pois o valor máximo ultrapassou o limite.
            return oldValue;
        }

        public static float floatingKeypad(string OldValue, short numeroMaximoCasas, int limite)
        {
            keypad mainWindow = new keypad(false, numeroMaximoCasas);
            float floatPoint;
            float oldValue = 0;
            float newValue = 0;
            bool isNumeric;
            isNumeric = float.TryParse(OldValue, out floatPoint);

            if (isNumeric)
            {
                //Recebe Valor antigo digitado no Textbox
                oldValue = Convert.ToSingle(OldValue);
                //Recebe o novo valor digitado no Keypad
            }

            if (mainWindow.ShowDialog() == true)
            {
                isNumeric = float.TryParse(mainWindow.Result, out floatPoint);

                if (isNumeric)
                {
                    if (floatPoint <= limite)
                    {
                        newValue = floatPoint;

                        if (oldValue != newValue)
                        {
                            //Retira o foco do textbox.
                            Keyboard.ClearFocus();
                            return newValue;

                        }
                    }
                    else
                    {
                        //Envia o oldValue pois o valor máximo ultrapassou o limite.
                        return oldValue;
                    }
                }
                else
                {
                    //Envia o oldValue pois o valor máximo ultrapassou o limite.
                    return oldValue;
                }

            }

            //Envia o oldValue pois o valor máximo ultrapassou o limite.
            return oldValue;
        }

        public static Int32 IntergerKeypad(string OldValue, short numeroMaximoCasas, Int32 limite)
        {
            keypad mainWindow = new keypad(true, numeroMaximoCasas);

            Int32 IntergerPoint;
            Int32 oldValue = 0;
            Int32 newValue = 0;

            bool isNumeric;
            isNumeric = Int32.TryParse(OldValue, out IntergerPoint);

            if (isNumeric)
            {
                //Recebe Valor antigo digitado no Textbox
                oldValue = Convert.ToInt32(OldValue);
                //Recebe o novo valor digitado no Keypad
            }

            if (mainWindow.ShowDialog() == true)
            {
                isNumeric = Int32.TryParse(mainWindow.Result, out IntergerPoint);

                if (isNumeric)
                {
                    if (IntergerPoint <= limite)
                    {
                        newValue = IntergerPoint;

                        if (oldValue != newValue)
                        {
                            //Retira o foco do textbox.
                            Keyboard.ClearFocus();
                            return newValue;
                        }
                    }
                    else
                    {
                        return oldValue;
                    }

                }
                else
                {
                    //Envia o oldValue pois o valor máximo ultrapassou o limite.
                    return oldValue;
                }
            }
            //Envia o oldValue pois o valor máximo ultrapassou o limite.
            return oldValue;
        }
        #endregion

        public static int Id_Producao_No_Silo_Expedicao;

        public static string NomeReceita_No_Silo_Expedicao = "";

        private static bool TickTack;
        public static bool TickTack_GS { get => TickTack; set => TickTack = value; }

        private static bool IniciouCadastro;
        public static bool IniciouCadastro_GS { get => IniciouCadastro; set => IniciouCadastro = value; }

        public static bool Libera_Escrita_Slot = false;

    }

    public class convertToTable
    {
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];

                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;

        }

    }

    public class functions
    {
        public static float percentualProduto(float pesoDesejado, float pesoBase)
        {
            float perct = 0;

            perct = ((100 * pesoDesejado) / pesoBase);

            return (float)Math.Round(perct, 2);
        }



        /// <summary>
        /// Função para controla o status da produção
        /// </summary>
        /// <param name="status">Status da produção numero inteiro.</param>
        /// <param name="label">Label que deseja alterar.</param>
        /// <returns>Retorna um label com o content + foreground e Backgorund</returns>
        public static Label controleStatus(int status, Label label)
        {

            if (status == 0)
            {
                label.Content = "Sem batelada";
                label.Background = new SolidColorBrush(Colors.Gray);
                label.Foreground = new SolidColorBrush(Colors.White);
            }
            else if (status == 1)
            {
                label.Content = "Dosagem Matéria Prima Manual";
                label.Background = new SolidColorBrush(Colors.Orange);
                label.Foreground = new SolidColorBrush(Colors.Black);
            }
            else if (status == 2)
            {
                label.Content = "Dosagem Matéria Prima Automática";
                label.Background = new SolidColorBrush(Colors.Green);
                label.Foreground = new SolidColorBrush(Colors.White);
            }
            else if (status == 3)
            {
                label.Content = "Transporte Para Pré Mistura";
                label.Background = new SolidColorBrush(Colors.Green);
                label.Foreground = new SolidColorBrush(Colors.White);
            }
            else if (status == 4)
            {
                label.Content = "Pré Mistura";
                label.Background = new SolidColorBrush(Colors.Green);
                label.Foreground = new SolidColorBrush(Colors.White);
            }
            else if (status == 5)
            {
                label.Content = "Moagem e Transporte Pós Mistura";
                label.Background = new SolidColorBrush(Colors.Green);
                label.Foreground = new SolidColorBrush(Colors.White);
            }
            else if (status == 6)
            {
                label.Content = "Pós Mistura";
                label.Background = new SolidColorBrush(Colors.Green);
                label.Foreground = new SolidColorBrush(Colors.White);
            }
            else if (status == 7)
            {
                label.Content = "Transporte Para Produto Acabado";
                label.Background = new SolidColorBrush(Colors.Green);
                label.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                label.Content = "Sem Status";
                label.Background = new SolidColorBrush(Colors.Red);
                label.Foreground = new SolidColorBrush(Colors.White);
            }


            return label;

        }

        /// <summary>
        /// Função para controla o botão da produção
        /// </summary>
        /// <param name="status">Status da produção numero inteiro.</param>
        /// <param name="Button">Botão que deseja alterar</param>
        /// <param name="executaProducao">Classe executa produção para controle de status</param>
        /// <param name="slotProducao">Qual slot pertence o botão 1 - 2 ou 3</param>
        /// <param name="numeroBatelada">Numero da batelada que o slot está</param>
        /// <returns></returns>
        public class EspecificacoesEquipamentos
        {

            public float PesoMaximoPermitidoBalanca { get; set; } //Peso máximo permitido na balança

            public Int32 TempoMistura { get; set; } //Tempo mistura

        }
  
    }

    public class Produto
    {
        public int id { get; set; }
        public string codigo { get; set; } 

        public string descricao { get; set; }

        public float densidade { get; set; }

        public string tipoProduto { get; set; }

        public string observacao { get; set; }
    }

    public class ProdutoReceita
    {
        public Produto produto { get; set; }

        public float pesoPorProduto = 0.0f;

        public string tipoDosagemMateriaPrima = ""; //"Automático" ou "Manual" - Na matéria prima pode ser dosado o produto manualmente ou automaticamente
    }

    public class Receita
    {
        public int id = -1;

        public string nomeReceita = "";

        public float pesoBase { get; set; }

        public List<ProdutoReceita> listProdutos = new List<ProdutoReceita>();

        public string observacao = "";
    }

    public class ProdutoBatelada : Produto
    {
        /// <summary>
        /// Atualizado na tela VerificacaoBateladas
        /// </summary>
        public int idProducao { get; set; }

        /// <summary>
        /// Atualizado na tela VerificacaoBateladas
        /// </summary>
        public float pesoDesejado { get; set; }

        public float pesoDosado { get; set; }

        public string statusItem { get; set; }

    }

    public class Batelada
    {

        public List<ProdutoBatelada> produtos = new List<ProdutoBatelada>();

        public int numeroBatelada { get; set; }

        public float pesoDesejado { get; set; }

        public float pesoDosado { get; set; }

        public float volumeDesejado { get; set; }
    }

    public class Producao
    {
        public int id {get; set;} // Id da produção

        /// <summary>
        /// Atualizado na tela ProduçãoTelaInicial
        /// </summary>
        public int IdReceitaBase { get; set; } // Id da receita base

        /// <summary>
        /// Atualizado na tela ProduçãoTelaInicial
        /// </summary>
        public Receita receita { get; set; } //Receita Base

        /// <summary>
        /// Atualizado na tela ConfiguracaoReceitaProducao
        /// </summary>
        public int quantidadeBateladas { get; set; } //Quantidade de bateladas

        /// <summary>
        /// Atualizado na tela ConfiguracaoReceitaProducao
        /// </summary>
        public List<Batelada> batelada = new List<Batelada>(); //Lista das bateladas

        /// <summary>
        /// Atualizado na tela ProduçãoTelaInicial
        /// </summary>
        public Int32 tempoPreMistura { get; set; } //Tempo de pré mistura

        /// <summary>
        /// Atualizado na tela ProduçãoTelaInicial
        /// </summary>
        public Int32 tempoPosMistura { get; set; } //Tempo de pos mistura

        /// <summary>
        /// Atualizado na tela ConfiguracaoReceitaProducao
        /// </summary>
        public float pesoTotalProducao { get; set; } //Peso total da produção - 

        /// <summary>
        /// 
        /// </summary>
        public float volumeTotalProducao { get; set; }//Volume total da produção

        /// <summary>
        /// Atualizado na tela ConfiguracaoReceitaProducao
        /// </summary>
        public string CodigoProdutoDosagemAutomaticaSilo1 = ""; //Codigo do produto que sera dosado automaticamente no silo 1.

        /// <summary>
        /// Atualizado na tela ConfiguracaoReceitaProducao
        /// </summary>
        public string CodigoProdutoDosagemAutomaticaSilo2 = ""; //Codigo do produto que sera dosado automaticamente no silo 2.

        /// <summary>
        /// Atualizado na tela VerificacaoBateladas
        /// </summary>
        public DateTime dateTimeInicioProducao = new DateTime(); //Data Inicio produção

        /// <summary>
        /// Atualizado na tela VerificacaoBateladas
        /// </summary>
        public DateTime dateTimeFimProducao = new DateTime(); //Data fim da produção

        /// <summary>
        /// Atualizado na tela VerificacaoBateladas
        /// </summary>
        public bool IniciouProducao { get; set; } //Iniciou Produção

        /// <summary>
        /// 
        /// </summary>
        public bool FinalizouProducao { get; set; } //Finalizou Produção

        /// <summary>
        /// 
        /// </summary>
        public float pesoTotalProduzido { get; set; } //Peso total produzido

        /// <summary>
        /// 
        /// </summary>
        public float volumeTotalProduzido { get; set; }//Volume total produzido

    }

}
