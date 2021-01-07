using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _9567A_V00___PI.Comunicacao
{
    public class CallCommunicationPLC
    {

        #region Create Variables Users, IP, Clocks, Ping

        private static Ping Ping_PLC = new Ping();

        bool Ping_PLC_Success = false;

        bool pinging = false;

        #endregion

        #region Create Variables, Objects For Driver

        //Driver for Read
        //=====================================================================
        public Comunicacao.Sharp7.S7Client Client;

        //Status da conexão com PLC

        SolidColorBrush BrushConnectionStatus = new SolidColorBrush();
        int ConnectionStatus = -1;
        bool PLCConnected = false;

        #endregion

        #region Create DateTime

        //Create DateTime for mesuare Time

        static DateTime DT_Tempo_Leitura_Buffer0_PLC = new DateTime();
        static DateTime DT_Tempo_Escrita_Buffer0_PLC = new DateTime();

        static DateTime DT_Tempo_Ping_PLC = new DateTime();

        #endregion

        int _BufferInicial = -1;
        int _BufferFinal = -1;

        public CallCommunicationPLC(int BufferInicial, int BufferFinal)
        {
            _BufferInicial = BufferInicial;
            _BufferFinal = BufferFinal;

            //Cria o driver
            Client = new Comunicacao.Sharp7.S7Client();

            Ping_PLC.PingCompleted += new PingCompletedEventHandler(Ping_PLC_PingCompleted);
        }

        public void readBuffersPLC()
        {
            Call_Ping();

            DT_Tempo_Leitura_Buffer0_PLC = DateTime.Now;

            try
            {
                if (Ping_PLC_Success)
                {
                    if (Client.Connected)
                    {
                        for (int i = _BufferInicial; i < _BufferFinal; i++)
                        {
                            if (Utilidades.VariaveisGlobais.Buffer_PLC[i].Enable_Read)
                            {
                                try
                                {
                                    Utilidades.VariaveisGlobais.Buffer_PLC[i].Result = Client.DBRead(Utilidades.VariaveisGlobais.Buffer_PLC[i].DBNumber, Utilidades.VariaveisGlobais.Buffer_PLC[i].Start, Utilidades.VariaveisGlobais.Buffer_PLC[i].Size, Utilidades.VariaveisGlobais.Buffer_PLC[i].Buffer);


                                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = Client.ErrorText(Utilidades.VariaveisGlobais.Buffer_PLC[i].Result);

                                }
                                catch (Exception ex)
                                {
                                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
            }

            Utilidades.VariaveisGlobais.Window_Diagnostic.Tempo_Leitura_PLC_GS = (DateTime.Now - DT_Tempo_Leitura_Buffer0_PLC).ToString();
        }

        public void writeBufferPLC()
        {
            Call_Ping();

            DT_Tempo_Escrita_Buffer0_PLC = DateTime.Now;
            //-----------------------------------------------------------------------------------------------------------------------
            try
            {
                if (Ping_PLC_Success)
                {
                    if (Client.Connected)
                    {
                        for (int i = _BufferInicial; i < _BufferFinal; i++)
                        {
                            if (Utilidades.VariaveisGlobais.Buffer_PLC[i].Enable_Write)
                            {
                                try
                                {
                                    Utilidades.VariaveisGlobais.Buffer_PLC[i].Result = Client.DBWrite(Utilidades.VariaveisGlobais.Buffer_PLC[i].DBNumber, Utilidades.VariaveisGlobais.Buffer_PLC[i].Start, Utilidades.VariaveisGlobais.Buffer_PLC[i].Size, Utilidades.VariaveisGlobais.Buffer_PLC[i].Buffer);

                                    Utilidades.VariaveisGlobais.Buffer_PLC[i].Enable_Write = false;

                                    if (Utilidades.VariaveisGlobais.Buffer_PLC[i].OnlyWrite)
                                    {
                                        Utilidades.VariaveisGlobais.Buffer_PLC[i].Enable_Read = false;
                                    }
                                    else
                                    {
                                        Utilidades.VariaveisGlobais.Buffer_PLC[i].Enable_Read = true;
                                    }

                                }
                                catch (Exception ex)
                                {
                                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
            }

            //-----------------------------------------------------------------------------------------------------------------------
            Utilidades.VariaveisGlobais.Window_Diagnostic.Tempo_Escrita_PLC_GS = (DateTime.Now - DT_Tempo_Escrita_Buffer0_PLC).ToString();
        }


        private void Call_Ping()
        {
#pragma warning disable CS0219 // Variable is assigned but its value is never used
            bool Error_Plc;
            bool Value_Timer;
#pragma warning restore CS0219 // Variable is assigned but its value is never used
            bool count = false;
            int Result_Async;

            #region PING And Status PLC

            //-----------------------------------------------------------------------------------------------------------------------

            DT_Tempo_Ping_PLC = DateTime.Now;


            if (!Client.Connected)
            {
                PLCConnected = false;
            }
            else
            {
                PLCConnected = true;
            }
            
            //PING PLC
            try
            {
                if (!pinging)
                {
                    Ping_PLC.SendAsync(Utilidades.VariaveisGlobais.IP_Plc_GS, 100);
                    pinging = true;
                }


            }
            catch (Exception ex)
            {
                Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
            }

            //Status PLC
            try
            {
                if (!Client.Connected)
                {
                    count = false;
                }
                else
                {
                    count = true;
                }


                if (!Ping_PLC_Success)
                {
                    if (count)
                    {
                        Client.Disconnect();
                        Result_Async = -1;

                        Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = Client.ErrorText(Client.LastError());
                    }

                }
                else
                {
                    if (!count)
                    {
                        Result_Async = Client.ConnectTo(Utilidades.VariaveisGlobais.IP_Plc_GS, Utilidades.VariaveisGlobais.Rack_PLC_GS, Utilidades.VariaveisGlobais.Slot_PLC_GS);

                        Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = Client.ErrorText(Client.LastError());
                    }
                }

                #region Status Plc

                if (Client.Connected)
                {

                    int Status = 0;
                    int Result0 = -1;

                    Result0 = Client.PlcGetStatus(ref Status);

                    if (Result0 == 0)
                    {
                        switch (Status)
                        {

                            case Comunicacao.Sharp7.S7Consts.S7CpuStatusRun:
                                {
                                    BrushConnectionStatus.Dispatcher.Invoke(delegate { BrushConnectionStatus = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0)); });

                                    Error_Plc = false;
                                    Value_Timer = true;
                                    ConnectionStatus = 1;
                                    break;
                                }
                            case Comunicacao.Sharp7.S7Consts.S7CpuStatusStop:
                                {
                                    BrushConnectionStatus.Dispatcher.Invoke(delegate { BrushConnectionStatus = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0)); });
                                    ConnectionStatus = 0;
                                    break;
                                }
                            default:
                                {
                                    BrushConnectionStatus.Dispatcher.Invoke(delegate { BrushConnectionStatus = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)); });
                                    ConnectionStatus = 0;
                                    Error_Plc = true;

                                    break;
                                }
                        }
                    }
                    else
                    {
                        BrushConnectionStatus.Dispatcher.Invoke(delegate { BrushConnectionStatus = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)); });
                        Error_Plc = true;
                    }
                }
                else
                {
                    BrushConnectionStatus.Dispatcher.Invoke(delegate { BrushConnectionStatus = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)); });
                    Error_Plc = true;

                }

                #endregion

            }
            catch (Exception ex)
            {
                Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
            }


            //-----------------------------------------------------------------------------------------------------------------------

            Utilidades.VariaveisGlobais.Window_Diagnostic.Tempo_Ping_PLC_GS = (DateTime.Now - DT_Tempo_Ping_PLC).ToString();

            #endregion
        }


        private void Ping_PLC_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            try
            {
                PingReply reply = e.Reply;

                if (reply.Status == IPStatus.Success)
                {
                    Ping_PLC_Success = true;
                }
                else
                {
                    Ping_PLC_Success = false;
                    Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = "Ping PLC ERROR";
                }
            }
            catch (Exception ex)
            {
                Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
            }
            finally
            {
                pinging = false;
            }

        }

        public bool Communication_Ready (int nBuffer)
        {

            if (Utilidades.VariaveisGlobais.Buffer_PLC[nBuffer].Size != 0)
            {
                return (Comunicacao.Sharp7.S7.GetIntAt(Utilidades.VariaveisGlobais.Buffer_PLC[nBuffer].Buffer, 0) == 1000) ? true : false;
            }
            else
            {
                return false;
            }
            
        }

        public SolidColorBrush BrushConnectionStatus_GS { get => BrushConnectionStatus; set => BrushConnectionStatus = value; }

        public int ConnectionStatus_GS { get => ConnectionStatus; set => ConnectionStatus = value; }

        public bool PLCConnected_GS { get => PLCConnected; set => PLCConnected = value; }
    }
}
