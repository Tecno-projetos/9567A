using _9567A_V00___PI.DataBase;
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
    public enum typeCommand { PD, INV, SS, Atuador_Digital, Atuador_Analogico, Registro }

    public class VariaveisGlobais
    {
        public static TelasAuxiliares.FirstLoading windowFirstLoading = new TelasAuxiliares.FirstLoading();

        public static RTU.IndicadorPesagem_3102C_S balancaPrincipal = new RTU.IndicadorPesagem_3102C_S(9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One, "COM1", 2);

        #region Structs

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


        public struct AuxiliaresBooleanas
        {

            public bool Emergencia;
            public bool Automatico_Equips;
            public bool NivelSilo;
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

        public struct ControleProducao
        {
            public ControleProducao(string value) : this()
            {
                indexProduto = -1;
                indexProdutoOld = -1;
            }

            public int Producao0;  //DOSAGEM
            public int Producao1;  //MISTURA
            public int Producao2;  //EXPEDIÇÃO
            public int Producao3;  //FINALIZOU


            //Palavra de commando
            public bool Dosando;                    
            public bool Estabilizado;
            public bool Manual_Automatico;
            public bool IniciadoDosagemProduto_PLC;
            public bool Solicita_Descarga;
            public bool Troca_Produto;
            public bool Reserva_1;
            public bool Reserva_2;
            public bool Reserva_3;
            public bool Reserva_4;
            public bool Reserva_5;
            public bool Reserva_6;
            public bool Reserva_7;
            public bool Reserva_8;
            public bool Reserva_9;
            public bool Reserva_10;

            //Variaveis lidas e escritas PLC
            public short StatusDosagem;
            public short StatusMistura;
            public short StatusExpedicao;
            public float PesoDosar;
            public float PesoTolerancia;
            public int TempoMistura;
            public int TempoEstabilizacao;
            public int TempoPulmaoVazio;
            public float Peso_Total_Produzindo;
            public float Peso_Parcial_Produzindo;
            public int TempoLimpezaDosagem;
            public int TempoLimpezaMisturador;

            //Variaveis auxiliares supervisao
            public bool HabilitadoDosarEmManual;
            public int indexProduto;
            public int indexProdutoOld;
            public int indexProducao;
            public bool primeiroProdutoDosar;
            public bool EncerrarDosagem;
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

        public struct type_Registro
        {

            public bool automatico;
            public bool manual;
            public bool Reset;
            public bool Manutencao;
            public bool Bloqueado;
            public bool Sem_Bloqueio;
            public bool Emergencia;
            public bool bit07;
            public bool Habilitado_Ligar_Rota;
            public bool ligaManual;
            public bool Abrindo;
            public bool Fechando;
            public bool Aberto;
            public bool Fechado;
            public bool Falha_Abrir;
            public bool Falha_Fechar;
            public bool Falha_Sem_Posicao;
            public bool SensorAberto;
            public bool SensorFechado;
            public bool ErroGeral;
            public bool Seleciona_Rota;
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
            public type_Registro AtuadorR;
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

            public bool bit07;
            public bool Habilitado_Ligar_Rota;
            public bool Abrindo;
            public bool Fechando;
            public bool Aberto;
            public bool Fechado;
            public bool Falha_Abrir;
            public bool Falha_Fechar;
            public bool Falha_Sem_Posicao;
            public bool SensorAberto;
            public bool SensorFechado;
            public bool ErroGeral;
            public bool Seleciona_Rota;



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

        public static Usuarios.controleUsuario controleUsuario = new Usuarios.controleUsuario();

        public static Telas_Fluxo.manutencao manutencao = new Telas_Fluxo.manutencao();

        public static Telas_Fluxo.configuracoes configuracoes = new Telas_Fluxo.configuracoes();

        public static Telas_Fluxo.producao producao = new Telas_Fluxo.producao();

        //public static Telas_Fluxo.receitas receitas = new Telas_Fluxo.receitas();

        public static Telas_Fluxo.relatorios relatorios = new Telas_Fluxo.relatorios();

        //public static Telas_Fluxo.Controle_Produção.controleBalanca telabalanca = new Telas_Fluxo.Controle_Produção.controleBalanca(0, 384);

        #endregion

        #region Connection PLC

        //Cria comunicação com CLP
        public static Comunicacao.CallCommunicationPLC CommunicationPLC = new Comunicacao.CallCommunicationPLC(0, 10);

        private static string IP_Plc = "192.168.1.100";

        private static int Rack_PLC = 0;
        private static int Slot_PLC = 2;

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
        //Configurar banco de dados para acesso remoto
        //http://www.systematiza.com.br/site/?page_id=837

        static bool DB_Connected;
        private static bool SQLCe;
        private static string Connection_DB_Create = @"Server=172.16.1.66\SQLEXPRESS,1433;Integrated Security=false;User ID=sa;Password=33162600";
        private static string Connection_DB_Users = @"Server=172.16.1.66\SQLEXPRESS,1433;Database=DB_Users_IHM;Integrated Security=false;User ID=sa;Password=33162600";
        private static string Connection_DB_Equip = @"Server=172.16.1.66\SQLEXPRESS,1433;Database=DB_Equips_IHM;Integrated Security=false;User ID=sa;Password=33162600";
        private static string Connection_DB_Producao = @"Server=172.16.1.66\SQLEXPRESS,1433;Database=DB_Producao_IHM;Integrated Security=false;User ID=sa;Password=33162600";

        private static string Connection_DB_Receitas = @"Server=172.16.1.66\SQLEXPRESS,1433;Database=DB_ReceitasPreMix;Integrated Security=false;User ID=sa;Password=33162600";
        public static string Connection_DB_Users_GS { get => Connection_DB_Users; set => Connection_DB_Users = value; }
        public static string Connection_DB_Equip_GS { get => Connection_DB_Equip; set => Connection_DB_Equip = value; }
        public static string Connection_DB_Producao_GS { get => Connection_DB_Producao; set => Connection_DB_Producao = value; }
        public static string Connection_DB_Receitas_GS { get => Connection_DB_Receitas; set => Connection_DB_Receitas = value; }
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

            //Inicializa Tabelas
            DataBase.SqlFunctionsUsers.Initialize_ProgramDBCA();
            DataBase.SqlFunctionsEquips.ExistTable();
            DataBase.SQLFunctionsProducao.Create_Table_Producao();
            DataBase.SQLFunctionsProducao.Create_Table_ProducaoProdutos();
        }

        public static AuxiliaresBooleanas auxiliaresBooleanos = new AuxiliaresBooleanas();

        public static List<Produto> listProdutos = new List<Produto>();

        public static List<Receita> listReceitas = new List<Receita>();

        public static Receita ReceitaCadastro = new Receita();

        public static List<Producao> OrdensProducao = new List<Producao>();

        public static ControleProducao controleProducao = new ControleProducao();

        public static IndicadorPesagem indicadorPesagem = new IndicadorPesagem();

        public static int dummyIndex_CriandoReceita; 

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

        private static bool TickTack;
        public static bool TickTack_GS { get => TickTack; set => TickTack = value; }

        public static bool ValoresPreenchidos()
        {
            if (controleProducao.TempoLimpezaDosagem != 0)
            {
                if (controleProducao.TempoLimpezaMisturador != 0)
                {
                    if (controleProducao.TempoEstabilizacao != 0)
                    {
                        if (controleProducao.TempoPulmaoVazio != 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

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

        /// <summary>
        /// Atualiza a lista de produtos de acordo com o banco de dados
        /// </summary>
        public static void atualizalistProdutos()
        {
            VariaveisGlobais.listProdutos.Clear();

            DataTable dt = DataBase.SqlFunctionsProdutos.getTableProdutos();

            Produto dummyProduto;

            foreach (DataRow item in dt.Rows)
            {
                dummyProduto = new Produto();

                dummyProduto.id = Convert.ToInt32(item.ItemArray[0]);
                dummyProduto.descricao = Convert.ToString(item.ItemArray[1]);
                dummyProduto.codigo = Convert.ToInt32(item.ItemArray[2]);
                dummyProduto.observacao = Convert.ToString(item.ItemArray[3]);

                VariaveisGlobais.listProdutos.Add(dummyProduto);
            }
        }

        /// <summary>
        /// Atualiza a lista de receitas de acordo com o banco de dados
        /// </summary>
        public static void atualizalistReceitas()
        {
            VariaveisGlobais.listReceitas.Clear();

            DataTable dtReceitas = DataBase.SqlFunctionsReceitas.getReceitas();

            Receita dummyReceita;

            atualizalistProdutos();

            foreach (DataRow item in dtReceitas.Rows)
            {
                dummyReceita = new Receita();

                dummyReceita.id = Convert.ToInt32(item.ItemArray[0]);
                dummyReceita.nomeReceita = Convert.ToString(item.ItemArray[1]);
                dummyReceita.Codigo = Convert.ToInt32(item.ItemArray[2]);
                dummyReceita.observacao = Convert.ToString(item.ItemArray[3]);
                dummyReceita.tempoMistura = Convert.ToInt32(item.ItemArray[4]);

                DataTable dtProdutosReceita = DataBase.SqlFunctionsReceitas.getProdutosReceita(dummyReceita.Codigo);

                foreach (DataRow item1 in dtProdutosReceita.Rows)
                {
                    ProdutoReceita dummyProdutoReceita = new ProdutoReceita();
                    Produto dummyProduto = new Produto();
                    dummyProduto = VariaveisGlobais.listProdutos[VariaveisGlobais.listProdutos.FindIndex(x => x.codigo == Convert.ToInt32(item1.ItemArray[1]))];

                    dummyProdutoReceita.pesoProdutoReceita = Convert.ToSingle(Math.Round((float)item1.ItemArray[2], 2));
                    dummyProdutoReceita.tolerancia = Convert.ToSingle(Math.Round((float)item1.ItemArray[3], 2));

                    ActualizeValuesProduto(ref dummyProduto); //pega os valores do produto e atualiza esse produto


                    dummyProdutoReceita.produto = dummyProduto;
                    dummyReceita.listProdutos.Add(dummyProdutoReceita);
                }

                VariaveisGlobais.listReceitas.Add(dummyReceita);
            }

        }

        private static void ActualizeValuesProduto(ref Produto prod)
        {
            //Atualiza a lista de produtos a partir do banco de dados

            int codigo = prod.codigo;

            var index = VariaveisGlobais.listProdutos.FindIndex(x => x.codigo == codigo);

            prod.id = VariaveisGlobais.listProdutos[index].id;
            prod.codigo = VariaveisGlobais.listProdutos[index].codigo;
            prod.descricao = VariaveisGlobais.listProdutos[index].descricao;
            prod.observacao = VariaveisGlobais.listProdutos[index].observacao;

        }

        public static void DataRow_To_Producao(DataRow dr)
        {
            //Atualiza primeiro os valores que existem informações no DataRow
            VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count-1].id = Convert.ToInt32(dr.ItemArray[0]);
            VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].pesoTotalProducao = Convert.ToInt32(dr.ItemArray[1]);
            VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].pesoTotalProduzido = Convert.ToInt32(dr.ItemArray[2]);
            VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].dateTimeInicioProducao = Convert.ToDateTime(dr.ItemArray[3]);
            VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].dateTimeFimProducao = Convert.ToDateTime(dr.ItemArray[4]);
            VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].IniciouProducao = Convert.ToBoolean(dr.ItemArray[5]);
            VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].FinalizouProducao = Convert.ToBoolean(dr.ItemArray[6]);
            VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.id = Convert.ToInt32(dr.ItemArray[7]);
            VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.nomeReceita = Convert.ToString(dr.ItemArray[8]);
            VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.Codigo = Convert.ToInt32(dr.ItemArray[9]);
            VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.observacao = Convert.ToString(dr.ItemArray[10]);
            VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.tempoMistura = Convert.ToInt32(dr.ItemArray[11]);

            //Após atualizar as info que existem, a partir delas devemos atualizar o seguinte item
            //Passo 1 = Produtos que contêm na receita.

            foreach (DataRow row in SQLFunctionsProducao.getProducaoProdutosFromIdProducao(VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].id).Rows)
            {
                VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos.Add(new ProdutoReceita());

                VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos[
                                                                                                VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos.Count -1
                ].produto.id = Convert.ToInt32(row.ItemArray[1]);

                VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos[
                                                                                                VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos.Count - 1
                ].produto.descricao = Convert.ToString(row.ItemArray[2]);

                VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos[
                                                                                                VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos.Count - 1
                ].produto.codigo = Convert.ToInt32(row.ItemArray[3]);

                VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos[
                                                                                                VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos.Count - 1
                ].produto.observacao = Convert.ToString(row.ItemArray[4]);

                VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos[
                                                                                                VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos.Count - 1
                ].pesoProdutoReceita = Convert.ToSingle(row.ItemArray[5]);

                VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos[
                                                                                                VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos.Count - 1
                ].pesoProdutoDesejado = Convert.ToSingle(row.ItemArray[6]);

                VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos[
                                                                                                VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos.Count - 1
                ].pesoProdutoDosado = Convert.ToSingle(row.ItemArray[7]);

                VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos[
                                                                                                VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos.Count - 1
                ].tolerancia = Convert.ToSingle(row.ItemArray[8]);

                VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos[
                                                                                                VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos.Count - 1
                ].iniciouDosagem = Convert.ToBoolean(row.ItemArray[9]);

                VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos[
                                                                                                VariaveisGlobais.OrdensProducao[VariaveisGlobais.OrdensProducao.Count - 1].receita.listProdutos.Count - 1
                ].finalizouDosagem = Convert.ToBoolean(row.ItemArray[10]);  
            }
        }

    }
    public class Produto
    {
        public int id { get; set; }

        public string descricao { get; set; }
        public int codigo { get; set; }

        public string observacao { get; set; }
    }

    public class ProdutoReceita
    {
        public Produto produto = new Produto();

        public float pesoProdutoReceita = 0.0f; //Peso na receita base

        public float pesoProdutoDesejado = 0.0f; //Peso desejado na produção

        public float pesoProdutoDosado = 0.0f; //Peso dosado na produção

        public float tolerancia = 0.0f; //Tolerância

        public bool iniciouDosagem = false;

        public bool finalizouDosagem = false;
    }

    public class Receita
    {
        public int id = -1;

        public string nomeReceita = "";

        public int Codigo = -1;

        public string observacao = "";

        public int tempoMistura = -1;

        public List<ProdutoReceita> listProdutos = new List<ProdutoReceita>();

    }

    public class Producao
    {
        public int id {get; set;} // Id da produção

        public float pesoTotalProducao { get; set; } //Peso total da produção

        public float pesoTotalProduzido { get; set; } //Peso total produzido

        public DateTime dateTimeInicioProducao = new DateTime(); //Data Inicio produção

        public DateTime dateTimeFimProducao = new DateTime(); //Data fim da produção

        public bool IniciouProducao { get; set; } //Iniciou Produção

        public bool FinalizouProducao { get; set; } //Finalizou Produção

        public Receita receita = new Receita(); //Receita Base

    }

}
