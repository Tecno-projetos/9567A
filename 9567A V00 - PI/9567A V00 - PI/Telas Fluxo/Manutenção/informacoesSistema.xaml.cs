using _9567A_V00___PI.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
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

namespace _9567A_V00___PI.Telas_Fluxo.Manutenção
{
    /// <summary>
    /// Interação lógica para informacoesSistema.xam
    /// </summary>

     public partial class informacoesSistema : UserControl
    {

        private PerformanceCounter ramCounter;
        private PerformanceCounter cpuCounter;

        public string getCurrentCpuUsage;

        public informacoesSistema()
        {
            InitializeComponent();

            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            cpuCounter = new PerformanceCounter();

        }

        public void atualizaSistema()
        {


            usoMemoria();

            GetCpuUsage();

            getDiskInformation();



        }

        private void getDiskInformation()
        {


            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in allDrives)
            {
                //Console.WriteLine("Drive {0}", d.Name);
                //Console.WriteLine("  Drive type: {0}", d.DriveType);
                if (drive.Name.Contains("C:"))
                {
                    if (drive.IsReady == true)
                    {

                        const double BytesInGB = 1073741824;

                        double totalDisco = Math.Round(drive.TotalSize / BytesInGB, 1);
                        double AvailableFreeSpace = Math.Round((drive.TotalFreeSpace) / BytesInGB, 1);
                        double percentual = Math.Round((drive.AvailableFreeSpace / (double)drive.TotalSize) * 100, 1);



                        lbTotalDisco.Content = "Tamanho disco:  " + totalDisco + " GB";

                        lbFreedisco.Content = "Espaço disponível Disco: " + AvailableFreeSpace + " GB" + "  (" + percentual + " %)";

                    }
                }
            }
        }


        private void GetCpuUsage()
        {


            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";

            getCurrentCpuUsage = cpuCounter.NextValue() + "%".ToString();

            lbUseCPU.Content = "Uso do CPU: " + getCurrentCpuUsage.ToString();
        }


        private void usoMemoria()
        {
            try
            {
                ramCounter = new PerformanceCounter("Memory", "Available MBytes", true);

                lbFree.Content = "Memória Ram dispinível: " + Convert.ToInt32(ramCounter.NextValue()).ToString() + "Mb";

            }
            catch (Exception)
            {

            }
        }

        private void btAtivaDesativa_Click(object sender, RoutedEventArgs e)
        {
            if (VariaveisGlobais.NumberOfGroup_GS < 3)
            {
                Utilidades.messageBox inputDialog = new Utilidades.messageBox(Utilidades.VariaveisGlobais.faltaPermissaoTitle, Utilidades.VariaveisGlobais.faltaPermissaoMessage, MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");
                inputDialog.ShowDialog();
            }
            else
            {
                if (Utilidades.VariaveisGlobais.AtivaDesativaTecladoVirtual)
                {

                    Utilidades.VariaveisGlobais.AtivaDesativaTecladoVirtual = false;

                    btAtivaDesativa.Background = new SolidColorBrush(Colors.Green);
                    txtVirtual.Text = "Desabilitar teclado virtual";
                    txtVirtual.Foreground = new SolidColorBrush(Colors.Black);
                    pckIcon.Foreground = new SolidColorBrush(Colors.Black);


                }
                else
                {
                    Utilidades.VariaveisGlobais.AtivaDesativaTecladoVirtual = true;

                    btAtivaDesativa.Background = new SolidColorBrush(Colors.Red);
                    txtVirtual.Text = "Habilitar teclado virtual";
                    txtVirtual.Foreground = new SolidColorBrush(Colors.White);
                    pckIcon.Foreground = new SolidColorBrush(Colors.White);

                }


            }




        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Utilidades.VariaveisGlobais.AtivaDesativaTecladoVirtual)
            {
                btAtivaDesativa.Background = new SolidColorBrush(Colors.Red);
                txtVirtual.Text = "Habilitar teclado virtual";
                txtVirtual.Foreground = new SolidColorBrush(Colors.White);
                pckIcon.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                btAtivaDesativa.Background = new SolidColorBrush(Colors.Green);
                txtVirtual.Text = "Desabilitar teclado virtual";
                txtVirtual.Foreground = new SolidColorBrush(Colors.Black);
                pckIcon.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {

            if (VariaveisGlobais.NumberOfGroup_GS < 3)
            {
                Utilidades.messageBox inputDialog = new Utilidades.messageBox(Utilidades.VariaveisGlobais.faltaPermissaoTitle, Utilidades.VariaveisGlobais.faltaPermissaoMessage, MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");
                inputDialog.ShowDialog();
            }
            else
            {
                //Apago do banco para 6 meses
                DataBase.SqlGlobalFuctions.AutoDelete(6);


            }
        }

        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            if (Utilidades.VariaveisGlobais.NumberOfGroup_GS < 2)
            {
                Utilidades.messageBox inputDialog = new Utilidades.messageBox(Utilidades.VariaveisGlobais.faltaPermissaoTitle, Utilidades.VariaveisGlobais.faltaPermissaoMessage, MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

                inputDialog.ShowDialog();

                return;
            }


            string nomeProcesso = Process.GetCurrentProcess().ProcessName;
            // Obtém todos os processos com o nome do atual
            Process[] processes = Process.GetProcessesByName(nomeProcesso);

            // Maior do que 1, porque a instância atual também conta
            if (processes.Length >= 1)
            {

                Process[] proc1 = Process.GetProcessesByName(nomeProcesso);
                proc1[0].Kill();

                Process proc = Process.GetCurrentProcess();
                proc.Kill();

                return;
            }
        }
    }

}
