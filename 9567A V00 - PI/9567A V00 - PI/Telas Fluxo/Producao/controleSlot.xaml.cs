using _9567A_V00___PI.Utilidades;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _9567A_V00___PI.Telas_Fluxo.Producao
{
    /// <summary>
    /// Interação lógica para controleSlot.xam
    /// </summary>
    public partial class controleSlot : UserControl
    {

        private int slotPertence;


        public controleSlot()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Nome do Header do Slot
        /// </summary>
        public string NameSlot_GS { get => gb_Nome.Header.ToString(); set => gb_Nome.Header = value; }
       
        /// <summary>
        /// Slot que pertence
        /// 1 = Dosagem
        /// 2 = Mistura
        /// 3 = Silo Pulmão  
        /// </summary>
        public int SlotPertence_GS { get => slotPertence; set => slotPertence = value; }

        public void atualizaReceita(VariaveisGlobais.ControleProducao controleProducao) 
        {
            //Controle Slot Dosagem
            #region Dosagem
            if (slotPertence == 0)
            {
                //Verifica se possui produção
                if (controleProducao.Producao0 > 0)
                {
                    var index = Utilidades.VariaveisGlobais.OrdensProducao.FindIndex(x => x.id == controleProducao.Producao0);

                    if (index != -1)
                    {
                        lbOrdem.Content = Convert.ToString(controleProducao.Producao0);
                        lbNomeReceita.Content = Convert.ToString(Utilidades.VariaveisGlobais.OrdensProducao[index].receita.nomeReceita);
                    }


                }
                else
                {
                    lbNomeReceita.Content = "Aguardando Receita";
                    lbOrdem.Content = "-";
                }

                //Controle Status
                //Status
                //0 = Aguardando Inicio de Dosagem
                //1 = Dosando
                //2 = Estabilizando 
                //3 = Estabilizado 
                //4 = Descarregando
                if (controleProducao.StatusDosagem == 0)
                {
                    statusSlot.Content = "Aguardando Ínicio de Dosagem";
                    statusSlot.Background = new SolidColorBrush(Colors.Gray);
                    statusSlot.Foreground = new SolidColorBrush(Colors.White);

                }
                else if (controleProducao.StatusDosagem == 1)
                {
                    statusSlot.Content = "Dosando Produto";
                    statusSlot.Background = new SolidColorBrush(Colors.Green);
                    statusSlot.Foreground = new SolidColorBrush(Colors.White);
                }
                else if (controleProducao.StatusDosagem == 2)
                {
                    statusSlot.Content = "Estabilizando Produto";

                    if (VariaveisGlobais.TickTack_GS)
                    {
                        statusSlot.Background = new SolidColorBrush(Colors.Green);
                        statusSlot.Foreground = new SolidColorBrush(Colors.White);
                    }
                    else
                    {
                        statusSlot.Background = new SolidColorBrush(Colors.Gray);
                        statusSlot.Foreground = new SolidColorBrush(Colors.White);
                    }
                }
                else if (controleProducao.StatusDosagem == 3)
                {
                    statusSlot.Content = "Produto Estabilizado";
                    statusSlot.Background = new SolidColorBrush(Colors.Green);
                    statusSlot.Foreground = new SolidColorBrush(Colors.White);


                }
                else if (controleProducao.StatusDosagem == 4)
                {
                    statusSlot.Content = "Descarregando";
                    statusSlot.Background = new SolidColorBrush(Colors.Green);
                    statusSlot.Foreground = new SolidColorBrush(Colors.White);
                }
                else
                {
                    statusSlot.Content = "Erro";
                    statusSlot.Background = new SolidColorBrush(Colors.Red);
                    statusSlot.Foreground = new SolidColorBrush(Colors.White);
                }
            }

            #endregion

            //Controle Slot Dosagem
            #region Mistura
            if (slotPertence == 1)
            {
                //Verifica se possui produção
                if (controleProducao.Producao1 > 0)
                {
                    var index = Utilidades.VariaveisGlobais.OrdensProducao.FindIndex(x => x.id == controleProducao.Producao1);

                    if (index != -1)
                    {
                        lbOrdem.Content = Convert.ToString(controleProducao.Producao1);
                        lbNomeReceita.Content = Convert.ToString(Utilidades.VariaveisGlobais.OrdensProducao[index].receita.nomeReceita);
                    }


                }
                else
                {
                    lbNomeReceita.Content = "Aguardando Receita";
                    lbOrdem.Content = "-";
                }

                //Controle Status
                //Status
                //0 = Aguardando Inicio de Mistura
                //1 = Misturando
                //2 = Misturado aguardando condições
                //3 = Aguardando Nivel para liberacao
                //4 = Descarregando para Silo Pulmão
                if (controleProducao.StatusMistura == 0)
                {
                    statusSlot.Content = "Aguardando Inicio de Mistura";
                    statusSlot.Background = new SolidColorBrush(Colors.Gray);
                    statusSlot.Foreground = new SolidColorBrush(Colors.White);

                }
                else if (controleProducao.StatusMistura == 1)
                {
                    statusSlot.Content = "Misturando";

                    if (VariaveisGlobais.TickTack_GS)
                    {
                        statusSlot.Background = new SolidColorBrush(Colors.Green);
                        statusSlot.Foreground = new SolidColorBrush(Colors.White);
                    }
                    else
                    {
                        statusSlot.Background = new SolidColorBrush(Colors.Gray);
                        statusSlot.Foreground = new SolidColorBrush(Colors.White);
                    }

                }
                else if (controleProducao.StatusMistura == 2)
                {
                    statusSlot.Content = "Misturado aguardando condições";

                    statusSlot.Background = new SolidColorBrush(Colors.Orange);
                    statusSlot.Foreground = new SolidColorBrush(Colors.Black);

                }
                else if (controleProducao.StatusMistura == 3)
                {
                    statusSlot.Content = "Aguardando Nivel para liberação";
                    statusSlot.Background = new SolidColorBrush(Colors.Orange);
                    statusSlot.Foreground = new SolidColorBrush(Colors.Black);

                }
                else if (controleProducao.StatusMistura == 4)
                {
                    statusSlot.Content = "Descarregando para Silo Pulmão";
                    statusSlot.Background = new SolidColorBrush(Colors.Green);
                    statusSlot.Foreground = new SolidColorBrush(Colors.White);
                }
                else
                {
                    statusSlot.Content = "Erro";
                    statusSlot.Background = new SolidColorBrush(Colors.Red);
                    statusSlot.Foreground = new SolidColorBrush(Colors.White);
                }
            }



            #endregion

            //Controle Slot Expedição
            #region Expedição
            if (slotPertence == 2)
            {
                //Verifica se possui produção
                if (controleProducao.Producao2 > 0)
                {
                    var index = Utilidades.VariaveisGlobais.OrdensProducao.FindIndex(x => x.id == controleProducao.Producao2);

                    if (index != -1)
                    {
                        lbOrdem.Content = Convert.ToString(controleProducao.Producao2);
                        lbNomeReceita.Content = Convert.ToString(Utilidades.VariaveisGlobais.OrdensProducao[index].receita.nomeReceita);

                    }


                }
                else
                {
                    lbNomeReceita.Content = "Aguardando Receita";
                    lbOrdem.Content = "-";
                }

                //Controle Status
                //Status
                //0 = Aguardando Premix
                //1 = Recebendo Produto
                //2 = Aguardando Ensaque

                if (controleProducao.StatusExpedicao == 0)
                {
                    statusSlot.Content = "Aguardando Premix";
                    statusSlot.Background = new SolidColorBrush(Colors.Gray);
                    statusSlot.Foreground = new SolidColorBrush(Colors.White);

                }
                else if (controleProducao.StatusExpedicao == 1)
                {
                    statusSlot.Content = "Recebendo Produto";

                    if (VariaveisGlobais.TickTack_GS)
                    {
                        statusSlot.Background = new SolidColorBrush(Colors.Green);
                        statusSlot.Foreground = new SolidColorBrush(Colors.White);
                    }
                    else
                    {
                        statusSlot.Background = new SolidColorBrush(Colors.Gray);
                        statusSlot.Foreground = new SolidColorBrush(Colors.White);
                    }

                }
                else if (controleProducao.StatusExpedicao == 2)
                {
                    statusSlot.Content = "Aguardando Ensaque";

                    statusSlot.Background = new SolidColorBrush(Colors.Green);
                    statusSlot.Foreground = new SolidColorBrush(Colors.White);

                }
                else
                {
                    statusSlot.Content = "Erro";
                    statusSlot.Background = new SolidColorBrush(Colors.Red);
                    statusSlot.Foreground = new SolidColorBrush(Colors.White);
                }
            }



            #endregion
        }

    }
}
