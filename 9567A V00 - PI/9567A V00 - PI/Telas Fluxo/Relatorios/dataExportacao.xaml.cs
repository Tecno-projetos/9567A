using MaterialDesignThemes.Wpf;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace _9567A_V00___PI.Telas_Fluxo.Relatorios
{
    /// <summary>
    /// Interação lógica para dataExportacao.xam
    /// </summary>
    public partial class dataExportacao : UserControl
    {
        public event EventHandler bt_Pesquisar;

        public event EventHandler bt_Exportar;

        private string exportacao = "";

        public dataExportacao()
        {
            InitializeComponent();
            DataContext = new PickersViewModel();

            var lang = System.Windows.Markup.XmlLanguage.GetLanguage("pt-BR");
            CombinedCalendar.Language = lang;
            CombinedClock.Language = lang;

            CombinedCalendar_FIM.Language = lang;
            CombinedClock_FIM.Language = lang;

            int descontarsegundos;
            descontarsegundos = DateTime.Now.Second;

            txtDataSelecionada.Content = DateTime.Now.AddSeconds(-descontarsegundos).AddDays(-1).ToString();

            txtFIM.Content = DateTime.Now.AddSeconds(-descontarsegundos).AddMinutes(1).ToString();
        }


        #region Encapsulate Fields

        public DateTime dataInicial_GS
        {
            set
            {
                txtDataSelecionada.Dispatcher.Invoke(delegate { txtDataSelecionada.Content = value; });

            }
            get
            {
                return Convert.ToDateTime((string)txtDataSelecionada.Content);
            }
        }

        public DateTime dataFinal_GS
        {
            set
            {
                txtFIM.Dispatcher.Invoke(delegate { txtFIM.Content = value; });

            }
            get
            {
                return Convert.ToDateTime((string)txtFIM.Content);
            }
        }

        public string discoOrigem_GS { get => exportacao; set => exportacao = value; }


        #endregion

        #region Controle Calendario

        public void CombinedDialogOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            webBrowse.Visibility = Visibility.Hidden;


            CombinedCalendar.SelectedDate = ((PickersViewModel)DataContext).Date;
            CombinedClock.Time = ((PickersViewModel)DataContext).Time;
        }

        public void CombinedDialogClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (Equals(eventArgs.Parameter, "1"))
            {
                DateTime d = (DateTime)CombinedCalendar.SelectedDate;

                d = d.AddMinutes(-d.Minute);
                d = d.AddHours(-d.Hour);
                d = d.AddSeconds(-d.Second);
                CombinedCalendar.SelectedDate = d;

                var combined = CombinedCalendar.SelectedDate.Value.AddSeconds(CombinedClock.Time.TimeOfDay.TotalSeconds);
                ((PickersViewModel)DataContext).Time = combined;
                ((PickersViewModel)DataContext).Date = combined;

                int descontarsegundos;
                descontarsegundos = combined.Second;

                txtDataSelecionada.Content = Convert.ToString(combined.AddSeconds(-descontarsegundos));
            }

            webBrowse.Visibility = Visibility.Visible;
        }

        public void CombinedDialogOpenedEventHandler_FIM(object sender, DialogOpenedEventArgs eventArgs)
        {

            webBrowse.Visibility = Visibility.Hidden;

            CombinedCalendar_FIM.SelectedDate = ((PickersViewModel)DataContext).Date;
            CombinedClock_FIM.Time = ((PickersViewModel)DataContext).Time;


        }

        public void CombinedDialogClosingEventHandler_FIM(object sender, DialogClosingEventArgs eventArgs)
        {
            if (Equals(eventArgs.Parameter, "1"))
            {
                DateTime d = (DateTime)CombinedCalendar_FIM.SelectedDate;


                d = d.AddMinutes(-d.Minute);
                d = d.AddHours(-d.Hour);
                d = d.AddSeconds(-d.Second);


                CombinedCalendar_FIM.SelectedDate = d;

                var combined = CombinedCalendar_FIM.SelectedDate.Value.AddSeconds(CombinedClock_FIM.Time.TimeOfDay.TotalSeconds);
                ((PickersViewModel)DataContext).Time = combined;
                ((PickersViewModel)DataContext).Date = combined;

                int descontarsegundos;
                descontarsegundos = combined.Second;

                txtFIM.Content = Convert.ToString(combined.AddSeconds(-descontarsegundos));
            }

            webBrowse.Visibility = Visibility.Visible;
        }

        #endregion



        public void atualizaProjeto(String sFilename)
        {
            if (this.webBrowse != null)
            {
                this.webBrowse.Navigate(sFilename);
            }

        }

        private void btPesquisar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.bt_Pesquisar != null)
                this.bt_Pesquisar(this, e);
        }

        private void btExport_Click(object sender, RoutedEventArgs e)
        {
            discoExportar inputDialog = new discoExportar();

            if (inputDialog.ShowDialog() == true)
            {
                exportacao = inputDialog.discoExportacao;

                if (this.bt_Exportar != null)
                    this.bt_Exportar(this, e);




            }
        }

        private void ButtonOpenDialog_Click(object sender, RoutedEventArgs e)
        {
            PopupAddCustom1.IsOpen = false;
        }

        private void ButtonOpenDialogFim_Click(object sender, RoutedEventArgs e)
        {
            PopupAddCustom.IsOpen = false;
        }
    }
}
