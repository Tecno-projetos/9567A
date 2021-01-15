using Modbus.Device;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modbus;


namespace _9567A_V00___PI.RTU
{
    public class IndicadorPesagem_3102C_S
    {


        /// <summary>
        /// Região de Váriaveis para contrele Modbus.
        /// </summary>
        #region Variaveis

        private byte slaveID;
        private ushort BaundRate;
        private ushort DataBits;
        private Parity Portparity;
        private StopBits PortStopBits;
        private string Port;

        private bool ErrorModbus;
        private float PesoAtualBalanca;

        private string AuXErro = "";

        private bool BloqueiaLeitura;
        private int auxBloqueia;


        #endregion

        /// <summary>
        /// Funçaão de Criar a leitura modbus do peso Atual do Indicador de Pesagem 
        /// </summary>
        /// <param name="BaundRate">9600 - 19200</param>
        /// <param name="DataBits">7 ou 8 </param>
        /// <param name="Portparity">Par - Impar - Nenhum </param>
        /// <param name="PortStopBits"> 1 -2 </param>
        /// <param name="Port">COM X Porta de comunicação Habilitada no USB ou PC</param>
        /// <param name="slaveID"> Endereço Modbus</param>
        public IndicadorPesagem_3102C_S(ushort BaundRate, ushort DataBits, Parity Portparity, StopBits PortStopBits, string Port, byte slaveID)
        {
            try
            {
                this.slaveID = slaveID;
                this.BaundRate = BaundRate;
                this.DataBits = DataBits;
                this.Portparity = Portparity;
                this.PortStopBits = PortStopBits;
                this.Port = Port;

            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Peso Atual da balança
        /// </summary>
        public float PesoAtualBalanca_GS { get => PesoAtualBalanca; }

        /// <summary>
        /// Erro de Leitura Modbus.
        /// </summary>
        public bool ErrorModbus_GS { get => ErrorModbus; }


        /// <summary>
        /// Bloqueia a leitura do Modbus até o Reset
        /// </summary>
        public bool BloqueiaLeitura_GS { get => BloqueiaLeitura; set => BloqueiaLeitura = value; }

        /// <summary>
        /// Atualiza a leitura Modbus.
        /// </summary>
        public bool LeituraModbus()
        {
            ErrorModbus = false;

            //Função Modbus 0x03(Read Holding Registers);
            //Número do registrador 0x00 0x51(decimal 81);
            //Quantidade de registradores 0x00 0x06(decimal 6).

            //Recebe o valor de leitura de peso e Statusd da balança.
            //Lembrando que para ler o Indicador 3102C.S pelo  C# deve-se descontar -1 do endereço 
            //Por Exemplo Endereço = 81 (Da tabela do equipmaneto)
            //81 - 1 = 80 enviar a função o valor 80 pois em C# a base é 1 de controle.
            ushort[] valorLeitura = ReadInt16(80, 6);


            //Inicia o Valor do peso da balança = 0.
            PesoAtualBalanca = 0;
            //Verifica se a conexão de Holding register leu o valor solicitado pela Modbus.
            if (valorLeitura.Length == 6)
            {
                //Recebe p valor do peso atual da balança no indice 3
                PesoAtualBalanca = valorLeitura[3];

                //Força o erro de leitura Modbus sempre para false.
                ErrorModbus = false;

                //Se possui leitura reseta o cantador de erros
                auxBloqueia = 0;
                BloqueiaLeitura = false;


                return true;
            }
            else
            {
                //Erro não foi possível fazer a leitura desse Indicador.
                //Irá mandar -1 para o valor do peso Atual simbolizando erro.
                PesoAtualBalanca = -1;

                //Força o erro de leitura Modbus sempre para true.
                ErrorModbus = true;


                string ErroAtual = ModbusMaster.error;

                if (ErroAtual != null)
                {
                    //Verifica o erro que está dando se for sempre o mesmo erro é escito apenas uma vez o nome do erro.
                    if (!ErroAtual.Equals(AuXErro))
                    {
        
                        AuXErro = ErroAtual;
                    }
                }

                //Verifica quantos erros deu de leitura
                auxBloqueia++;

                //Se possuir mais de 3 erros quando solicitado bloqueia a leitura até o reset da balança ser utilizado.
                if (auxBloqueia > 3)
                {
                    BloqueiaLeitura = true;
                    auxBloqueia = 0;
                }

                return false;

            }
        }


        public void EscritaCLP(int bufferPlcEnsaque, int PosicaoBuffer)
        {
            if (Utilidades.VariaveisGlobais.CommunicationPLC.PLCConnected_GS)
            {
                Utilidades.VariaveisGlobais.Buffer_PLC[bufferPlcEnsaque].Enable_Read = false;

                Comunicacao.Sharp7.S7.SetRealAt(Utilidades.VariaveisGlobais.Buffer_PLC[bufferPlcEnsaque].Buffer, PosicaoBuffer, PesoAtualBalanca);

                Utilidades.VariaveisGlobais.Buffer_PLC[bufferPlcEnsaque].Enable_Write = true;
            }
        }

        #region Leitura Modbus

        /// <summary>
        /// ReadHoldingRegisters
        /// </summary>
        /// <param name="startAddress">Inicio do endereço </param>
        /// <param name="Tamanho">Tamanho de dados apartir daquele endereço</param>
        /// <returns></returns>
        private ushort[] ReadInt16(ushort startAddress, ushort Tamanho)
        {
            ushort[] RecebeInt16 = { 0 };
            try
            {
                using (SerialPort port = new SerialPort(Port))
                {
                    // configure serial port
                    port.BaudRate = BaundRate;
                    port.DataBits = DataBits;
                    port.Parity = Portparity;
                    port.StopBits = PortStopBits;

                    port.Open();


                    // create modbus master
                    IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(port);

                    var aux = master.ReadHoldingRegisters(slaveID, startAddress, Tamanho);

                    return aux;
                }
            }
            catch (Exception ex)
            {

                return RecebeInt16;
            }
        }

        #endregion

    }
}
