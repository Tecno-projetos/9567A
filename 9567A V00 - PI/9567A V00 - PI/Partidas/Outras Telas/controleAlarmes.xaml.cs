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

namespace _9567A_V00___PI.Partidas.Outras_Telas
{
    /// <summary>
    /// Interação lógica para controleAlarmes.xam
    /// </summary>
    public partial class controleAlarmes : UserControl
    {
        public controleAlarmes()
        {
            InitializeComponent();
        }

        //Coloca cores no Grid dos alarme se está ativo, se foi reconhecido
        #region Cores linha Grid
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

        private void DataGrid_Search_Eventos_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Background = new SolidColorBrush(Colors.Silver);
        }

        #endregion

        //Monta o grid conforme a seleção
        #region Data Source
        /// <summary>
        /// Função para alterar o cabeçalho do datagrid 
        /// </summary>
        /// <param name="Data">\DataTable que será enviado</param>
        /// <param name="dt">DataGrid de referencia para ser enviado </param>
        /// <param name="alarme">Se em true irá definir o DataGrid em um formato, e em false em formato de Evento</param>
        public void DataGrid_ItemSource_Alarms_And_Events(System.Data.DataTable Data, DataGrid dt, bool alarme)
        {
            try
            {
                dt.ItemsSource = null;

                if (alarme)
                {
                    dt.ItemsSource = Data.DefaultView;

                    dt.Columns[1].Header = "Tag";
                    dt.Columns[2].Header = "Descrição";
                    dt.Columns[7].Header = "Usuário Reconheceu";
                    dt.Columns[8].Header = "Usuário Logado";
                    dt.Columns[10].Header = "Data Entrada";
                    dt.Columns[11].Header = "Data Saída";
                    dt.Columns[12].Header = "Data Reconhecido";

                    dt.Columns[0].Visibility = Visibility.Hidden;
                    dt.Columns[3].Visibility = Visibility.Hidden;
                    dt.Columns[4].Visibility = Visibility.Hidden;
                    dt.Columns[5].Visibility = Visibility.Hidden;
                    dt.Columns[6].Visibility = Visibility.Hidden;
                    dt.Columns[9].Visibility = Visibility.Hidden;
                    dt.Columns[11].Visibility = Visibility.Visible;
                    dt.Columns[12].Visibility = Visibility.Visible;
                    dt.Columns[13].Visibility = Visibility.Hidden;
                }
                else
                {
                    dt.ItemsSource = Data.DefaultView;
                    dt.Columns[1].Header = "Tag";
                    dt.Columns[2].Header = "Descrição";
                    dt.Columns[8].Header = "Usuário Logado";
                    dt.Columns[10].Header = "Data Entrada";

                    dt.Columns[0].Visibility = Visibility.Hidden;
                    dt.Columns[3].Visibility = Visibility.Hidden;
                    dt.Columns[4].Visibility = Visibility.Hidden;
                    dt.Columns[5].Visibility = Visibility.Hidden;
                    dt.Columns[6].Visibility = Visibility.Hidden;
                    dt.Columns[7].Visibility = Visibility.Hidden;
                    dt.Columns[8].Visibility = Visibility.Visible;
                    dt.Columns[9].Visibility = Visibility.Hidden;
                    dt.Columns[11].Visibility = Visibility.Hidden;
                    dt.Columns[12].Visibility = Visibility.Hidden;
                    dt.Columns[13].Visibility = Visibility.Hidden;
                }


                dt.Items.Refresh();
            }
            catch (Exception ex)
            {
                Utilidades.VariaveisGlobais.Window_Buffer_Diagnostic.List_Error = ex.ToString();
            }
        }

        #endregion
    }
}
