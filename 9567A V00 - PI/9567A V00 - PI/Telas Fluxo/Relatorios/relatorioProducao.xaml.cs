using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using iTextSharp.text.pdf;          //*iTextSharp
using iTextSharp.text.pdf.parser;   //*iTextSharp Text-Reader

namespace _9567A_V00___PI.Telas_Fluxo.Relatorios
{
    /// <summary>
    /// Interação lógica para relatorioProducao.xam
    /// </summary>
    public partial class relatorioProducao : UserControl
    {

        private string fileName = "";
        private string nameArquivo = "";
        private string folder = @"C:\Temp";
        private bool NecessitaApagar = false;
        private bool pesquisou = false;
        private string OldfileName = "";

        Utilidades.messageBox inputDialog;

        public relatorioProducao()
        {
            InitializeComponent();


            producao.bt_Exportar += Producao_bt_Exportar;

            producao.bt_Pesquisar += Producao_bt_Pesquisar;

            Utilidades.VariaveisGlobais.createFolder(folder);


            clearFolder(folder);

        }

        //Apaga tudo dentro do diretório
        private void clearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                clearFolder(di.FullName);
                di.Delete();
            }
        }

        private void Producao_bt_Pesquisar(object sender, EventArgs e)
        {
            Utilidades.VariaveisGlobais.createFolder(folder);

            //Verifica se já possui um arquivo criado
            if (!String.IsNullOrEmpty(fileName))
            {
                KillRunningProcess();
                OldfileName = fileName;
                NecessitaApagar = true;
            }

            //recebe novo nome de arquivo
            nameArquivo = "Producao" + "_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + ".pdf";

            fileName = folder + "\\" + nameArquivo;

            //Verifica se o file já foi criado
            if (!File.Exists(fileName))
            {

                inputDialog = new Utilidades.messageBox("Pesquisando", "Isso pode levar alguns minutos, por favor aguarde.", MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");

                inputDialog.ShowDialog();


                //Original
                if (!Relatorios.ExportacaoRelatorios.exportProducao(fileName, "Produção Total", producao.dataInicial_GS, producao.dataFinal_GS))
                {
                    inputDialog = new Utilidades.messageBox("Erro", "Erro ao gerar relatório. Tente Novamente!", MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");

                    inputDialog.ShowDialog();
                }
                else
                {
                    producao.atualizaProjeto(fileName);

                    if (NecessitaApagar)
                    {
                        File.Delete(OldfileName);
                        NecessitaApagar = false;
                    }
                }
            }
            else
            {
                producao.atualizaProjeto(fileName);

                inputDialog = new Utilidades.messageBox("Arquivo já exportado", "O arquivo já foi exportado.", MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");

                inputDialog.ShowDialog();

            }

            pesquisou = true;
        }

        private void Producao_bt_Exportar(object sender, EventArgs e)
        {
            if (pesquisou)
            {
                string destinationFile = producao.discoOrigem_GS + "\\" + nameArquivo;

                if (!File.Exists(destinationFile))
                {
                    inputDialog = new Utilidades.messageBox("Exportando", "Isso pode levar alguns minutos, por favor aguarde.", MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");

                    inputDialog.ShowDialog();

                    //Original
                    if (Relatorios.ExportacaoRelatorios.exportProducao(destinationFile, "Produção Total", producao.dataInicial_GS, producao.dataFinal_GS))
                    {
                       inputDialog = new Utilidades.messageBox("Arquivo exportado", "O arquivo foi exportado com sucesso", MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");

                         inputDialog.ShowDialog();
                    }
                    else
                    {
                        inputDialog = new Utilidades.messageBox("Erro", "Erro ao exportar relatório. Tente Novamente!", MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");

                        inputDialog.ShowDialog();

                    }
                }
                else
                {
                    inputDialog = new Utilidades.messageBox("Arquivo já exportado", "O arquivo já foi exportado.", MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");

                    inputDialog.ShowDialog();

                }
            }
            else
            {
                inputDialog = new Utilidades.messageBox("Realizar Pesquisa", "Para exportar algum arquivo é necessário realizar a pesquisa.", MaterialDesignThemes.Wpf.PackIconKind.Information, "OK", "Fechar");

                inputDialog.ShowDialog();
            }
        }

        private void KillRunningProcess()
        {

            Process[] tabtip = Process.GetProcessesByName("Acrord32");

            if (null != tabtip)
            {
                tabtip.ToList().ForEach(a => { if (null != a) { a.Kill(); } });

            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            KillRunningProcess();

            pesquisou = false;
        }
    }
}
