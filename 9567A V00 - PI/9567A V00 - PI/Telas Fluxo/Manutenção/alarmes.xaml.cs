using _9567A_V00___PI.Telas_Fluxo.Relatorios;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace _9567A_V00___PI.Telas_Fluxo.Manutenção
{
    /// <summary>
    /// Interação lógica para alarmes.xam
    /// </summary>
    public partial class alarmes : UserControl
    {
        Utilidades.messageBox messageBox;

        public alarmes()
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

        #region Controle Calendario

        public void CombinedDialogOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            calendar(Visibility.Hidden);

            CombinedCalendar.SelectedDate = ((PickersViewModel)DataContext).Date;
            CombinedClock.Time = ((PickersViewModel)DataContext).Time;
        }

        public void CombinedDialogClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (Equals(eventArgs.Parameter, "1"))
            {

                var combined = CombinedCalendar.SelectedDate.Value.AddSeconds(CombinedClock.Time.TimeOfDay.TotalSeconds);
                ((PickersViewModel)DataContext).Time = combined;
                ((PickersViewModel)DataContext).Date = combined;

                int descontarsegundos;
                descontarsegundos = combined.Second;

                txtDataSelecionada.Content = Convert.ToString(combined.AddSeconds(-descontarsegundos));
            }

            calendar(Visibility.Visible);
        }

        public void CombinedDialogOpenedEventHandler_FIM(object sender, DialogOpenedEventArgs eventArgs)
        {

            calendar(Visibility.Hidden);
            CombinedCalendar_FIM.SelectedDate = ((PickersViewModel)DataContext).Date;
            CombinedClock_FIM.Time = ((PickersViewModel)DataContext).Time;


        }

        public void CombinedDialogClosingEventHandler_FIM(object sender, DialogClosingEventArgs eventArgs)
        {
            if (Equals(eventArgs.Parameter, "1"))
            {
                var combined = CombinedCalendar_FIM.SelectedDate.Value.AddSeconds(CombinedClock_FIM.Time.TimeOfDay.TotalSeconds);
                ((PickersViewModel)DataContext).Time = combined;
                ((PickersViewModel)DataContext).Date = combined;

                int descontarsegundos;
                descontarsegundos = combined.Second;

                txtFIM.Content = Convert.ToString(combined.AddSeconds(-descontarsegundos));
            }

            calendar(Visibility.Visible);
        }

        #endregion


        private void calendar(Visibility visibility)
        {
            DataGrid_Search.Visibility = visibility;
            gridNoclick.Visibility = visibility;
            btLeftList_Produtos.Visibility = visibility;
            btRightList_Produtos.Visibility = visibility;
            btUpList_Produtos.Visibility = visibility;
            btDownList_Produtos.Visibility = visibility;
        }
        private void btLeftList_Produtos_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGrid_Search, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - 20);
        }

        private void btDownList_Produtos_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGrid_Search, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToVerticalOffset(scroll.VerticalOffset + 5);
        }

        private void btRightList_Produtos_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGrid_Search, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset + 20);
        }

        private void btUpList_Produtos_Click(object sender, RoutedEventArgs e)
        {
            var scroll = (VisualTreeHelper.GetChild(DataGrid_Search, 0) as Decorator).Child as ScrollViewer;

            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - 5);
        }

        private void btPesquisar_Click(object sender, RoutedEventArgs e)
        {

            //Convertendo Valor para string
            DateTime DT_IN_General = new DateTime();
            DateTime DT_OUT_General = new DateTime();

            DT_IN_General = Convert.ToDateTime(txtDataSelecionada.Content);
            DT_OUT_General = Convert.ToDateTime(txtFIM.Content);

            if (DT_IN_General < DT_OUT_General)
            {

                DataGrid_ItemSource_Alarms(DataBase.SqlFunctionsEquips.GetReportAlarm_Table_EquipAlarmEvent(DT_IN_General, DT_OUT_General));
            }
            else
            {
                messageBox = new Utilidades.messageBox("Data Inválida", "Selecione uma data Inicial inferior a data Final", PackIconKind.Information, "Ok", "Fechar");
                messageBox.ShowDialog();
            }

        }

        private void DataGrid_Search_Alarme_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                DataRowView item = e.Row.Item as DataRowView;

                if (item != null)
                {
                    bool Active = (bool)item.Row.ItemArray[4];
                    bool Ack = (bool)item.Row.ItemArray[6];
                    bool Event = (bool)item.Row.ItemArray[3];

                    if (Active)
                    {
                        e.Row.Background = new SolidColorBrush(Colors.Red);
                        e.Row.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                        if (Ack)
                        {
                            e.Row.Background = new SolidColorBrush(Colors.Yellow);
                            e.Row.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                        }
                    }
                    else if (Ack)
                    {
                        e.Row.Background = new SolidColorBrush(Colors.ForestGreen);
                    }
                    else
                    {
                        e.Row.Background = new SolidColorBrush(Colors.DodgerBlue);
                        e.Row.Foreground = new SolidColorBrush(Colors.White);
                    }

                    if (Event)
                    {
                        e.Row.Background = new SolidColorBrush(Colors.Silver);
                    }
                }
            }
            catch (Exception ex)
            {
                Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
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

        //Monta o grid conforme a seleção
        #region Data Source



        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid_ItemSource_Alarms(DataBase.SqlFunctionsEquips.GetReportAlarm_Table_EquipAlarmEvent(DateTime.Now.AddDays(-1), DateTime.Now.AddMinutes(1)));
        }

        private void DataGrid_ItemSource_Alarms(System.Data.DataTable Data)
        {

            DataGrid_Search.ItemsSource = Data.DefaultView;
            DataGrid_Search.Columns[0].Header = "Id";
            DataGrid_Search.Columns[1].Header = "Tag";
            DataGrid_Search.Columns[2].Header = "Descrição";
            DataGrid_Search.Columns[7].Header = "Usuário Reconheceu";
            DataGrid_Search.Columns[8].Header = "Usuário Logado";
            DataGrid_Search.Columns[10].Header = "Data Entrada";
            DataGrid_Search.Columns[11].Header = "Data Saída";
            DataGrid_Search.Columns[12].Header = "Data Reconhecido";

            DataGrid_Search.Columns[3].Visibility = Visibility.Hidden;
            DataGrid_Search.Columns[4].Visibility = Visibility.Hidden;
            DataGrid_Search.Columns[5].Visibility = Visibility.Hidden;
            DataGrid_Search.Columns[6].Visibility = Visibility.Hidden;
            DataGrid_Search.Columns[9].Visibility = Visibility.Hidden;
            DataGrid_Search.Columns[11].Visibility = Visibility.Visible;
            DataGrid_Search.Columns[12].Visibility = Visibility.Visible;
            DataGrid_Search.Columns[13].Visibility = Visibility.Hidden;
        }
        #endregion
    }
}
