using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace _9567A_V00___PI.Telas_Fluxo.Relatorios
{
    /// <summary>
    /// Lógica interna para discoExportar.xaml
    /// </summary>
    public partial class discoExportar : Window
    {

        private int idexOLD = -1;
        private int idexNew = 0;
        private string nomeExportação = "";
        DriveInfo[] allDrives;

        public discoExportar()
        {
            InitializeComponent();

            atualizaDrivers();
        }

        public void atualizaDrivers()
        {
            try
            {
                allDrives = DriveInfo.GetDrives();

                if (listbox.Items.Count > 0)
                {
                    listbox.Items.Clear();
                }


                foreach (DriveInfo drive in allDrives)
                {
                    ListBoxItem boxItem = new ListBoxItem();

                    if (drive.Name.Contains("C:"))
                    {
                        boxItem.IsSelected = true;

                    }
                    else
                    {
                        boxItem.IsSelected = false;
                    }
                    boxItem.Width = 130;
                    boxItem.Height = 60;
                    boxItem.Content = drive.Name + " " + drive.VolumeLabel;
                    boxItem.FontSize = 12;
                    boxItem.Foreground = new SolidColorBrush(Colors.White);
                    boxItem.VerticalAlignment = VerticalAlignment.Center;
                    boxItem.HorizontalAlignment = HorizontalAlignment.Center;
                    boxItem.Background = new SolidColorBrush(Color.FromRgb(60, 60, 60));
                    Thickness margin = boxItem.Margin;
                    margin.Top = 2;
                    boxItem.Margin = margin;
                    boxItem.IsEnabled = true;

                    listbox.Items.Add(boxItem);
                }
            }
            catch (Exception ex)
            {

                Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString() + " Erro pesquisa de drivers no PC";
            }

        }

        private void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                idexNew = listbox.SelectedIndex;

                if (idexNew != -1)
                {
                    var row_list = (ListBoxItem)listbox.Items[idexNew];

                    if (idexNew != idexOLD)
                    {
                        row_list.Background = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                        row_list.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));


                        if (idexOLD != -1)
                        {
                            var row_listOld = (ListBoxItem)listbox.Items[idexOLD];
                            row_listOld.Background = new SolidColorBrush(Color.FromRgb(60, 60, 60));
                            row_listOld.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                        }

                        idexOLD = idexNew;

                        nomeExportação = allDrives[idexNew].Name;

                    }
                }
            }
            catch (Exception ex)
            {

                Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString() + " Erro selecionar drivers no PC";
            }

        }

        public string discoExportacao
        {
            get { return nomeExportação; }
        }

        private void btExportar_Click(object sender, RoutedEventArgs e)
        {

            nomeExportação = nomeExportação + "Relatorio_Producoes";

            if (!Directory.Exists(nomeExportação))
            {
                //Criamos um com o nome folder
                Directory.CreateDirectory(nomeExportação);
            }

            this.DialogResult = true;
        }
    }
}
