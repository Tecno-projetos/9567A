using System;
using System.Collections.Generic;
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

namespace _9567A_V00___PI.Partidas.Principal
{
    /// <summary>
    /// Lógica interna para principalPartidaDireta.xaml
    /// </summary>
    public partial class principalPartidaDireta : Window
    {
        private string tagEquip = "";
        private string NomePartida = "";
        private bool created = false;
        public bool created_GS { get => created; }

        public event EventHandler Bt_Fechar_Click;

        public principalPartidaDireta()
        {

        }

        public principalPartidaDireta(string nome, string tag, string numeroPartida, string paginaProjeto)
        {
            InitializeComponent();          
            this.Title = nome + " " + tag;

            pckInicial.Foreground = Utilidades.VariaveisGlobais.Verde;
            pckAlarmes.Foreground = Utilidades.VariaveisGlobais.Branco;
            pckConfiguracoes.Foreground = Utilidades.VariaveisGlobais.Branco;

            tagEquip = tag;
            NomePartida = nome + " " + tag;

            controlePD.lbName.Content = NomePartida;
            configuracoesPD.lbName.Content = NomePartida;
            alarmes.lbNameEquip.Content = NomePartida;
            created = true;
        }

        public void actualize_UI(ref Utilidades.VariaveisGlobais.type_All Command)
        {
            if (created)
            {
                controlePD.actualize_UI(ref Command);
                configuracoesPD.actualize_UI(ref Command);
            }
        }

        private void Home_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            pckInicial.Foreground = Utilidades.VariaveisGlobais.Verde;
            pckAlarmes.Foreground = Utilidades.VariaveisGlobais.Branco;
            pckConfiguracoes.Foreground = Utilidades.VariaveisGlobais.Branco;

            this.Height = 515;
            this.Width = 255;

        }

        private void config_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            pckInicial.Foreground = Utilidades.VariaveisGlobais.Branco;
            pckAlarmes.Foreground = Utilidades.VariaveisGlobais.Branco;
            pckConfiguracoes.Foreground = Utilidades.VariaveisGlobais.Verde;


            this.Height = 515;
            this.Width = 255;

        }

        private void alarmes_Tela_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            pckInicial.Foreground = Utilidades.VariaveisGlobais.Branco;
            pckAlarmes.Foreground = Utilidades.VariaveisGlobais.Verde;
            pckConfiguracoes.Foreground = Utilidades.VariaveisGlobais.Branco;


            this.Height = 515;
            this.Width = 515;

            DateTime dtin;
            DateTime dtout;

            dtin = DateTime.Now.AddMonths(-1);
            dtout = DateTime.Now.AddMinutes(2);

            /// Referencia de código para a tela de alarmes
            // Click para atualizar os alarmes tela de alarmes
            alarmes.DataGrid_ItemSource_Alarms_And_Events(DataBase.SqlFunctionsEquips.GetReportAlarm_Table_EquipAlarmEvent(dtin, dtout, "_" + tagEquip), alarmes.DataGrid_Search_Alarme, true);

            //Click para atualizar os Eventos na tela de alarmes
            alarmes.DataGrid_ItemSource_Alarms_And_Events(DataBase.SqlFunctionsEquips.GetReportEvent_Table_EquipAlarmEvent(dtin, dtout, "_" + tagEquip), alarmes.DataGrid_Search_Eventos, false);

        }

        private void btFechar_Click(object sender, RoutedEventArgs e)
        {
            if (this.Bt_Fechar_Click != null)
                this.Bt_Fechar_Click(this, e);
        }

        private void btDownList_Click(object sender, RoutedEventArgs e)
        {
            var scrollAlarme = (VisualTreeHelper.GetChild(alarmes.DataGrid_Search_Alarme, 0) as Decorator).Child as ScrollViewer;

            scrollAlarme.ScrollToVerticalOffset(scrollAlarme.VerticalOffset + 5);

            var scrollEvento = (VisualTreeHelper.GetChild(alarmes.DataGrid_Search_Eventos, 0) as Decorator).Child as ScrollViewer;

            scrollEvento.ScrollToVerticalOffset(scrollEvento.VerticalOffset + 5);

        }

        private void btUpList_Click(object sender, RoutedEventArgs e)
        {

            var scrollAlarme = (VisualTreeHelper.GetChild(alarmes.DataGrid_Search_Alarme, 0) as Decorator).Child as ScrollViewer;

            scrollAlarme.ScrollToVerticalOffset(scrollAlarme.VerticalOffset -5);

            var scrollEvento = (VisualTreeHelper.GetChild(alarmes.DataGrid_Search_Eventos, 0) as Decorator).Child as ScrollViewer;

            scrollEvento.ScrollToVerticalOffset(scrollEvento.VerticalOffset -5 );

        }

        private void btLeftList_Click(object sender, RoutedEventArgs e)
        {


            var scrollAlarme = (VisualTreeHelper.GetChild(alarmes.DataGrid_Search_Alarme, 0) as Decorator).Child as ScrollViewer;

            scrollAlarme.ScrollToHorizontalOffset(scrollAlarme.HorizontalOffset - 20);

            var scrollEvento = (VisualTreeHelper.GetChild(alarmes.DataGrid_Search_Eventos, 0) as Decorator).Child as ScrollViewer;

            scrollEvento.ScrollToHorizontalOffset(scrollEvento.HorizontalOffset - 20);


        }

        private void btRightList_Click(object sender, RoutedEventArgs e)
        {
            var scrollAlarme = (VisualTreeHelper.GetChild(alarmes.DataGrid_Search_Alarme, 0) as Decorator).Child as ScrollViewer;

            scrollAlarme.ScrollToHorizontalOffset(scrollAlarme.HorizontalOffset + 20);

            var scrollEvento = (VisualTreeHelper.GetChild(alarmes.DataGrid_Search_Eventos, 0) as Decorator).Child as ScrollViewer;

            scrollEvento.ScrollToHorizontalOffset(scrollEvento.HorizontalOffset + 20);
        }
    }
}
