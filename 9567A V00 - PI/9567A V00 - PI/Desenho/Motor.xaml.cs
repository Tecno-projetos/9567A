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

namespace _9567A_V00___PI.Desenho
{
    /// <summary>
    /// Interação lógica para Motor.xam
    /// </summary>
    public partial class Motor : UserControl
    {


        #region Variables

        Utilidades.EquipsControl equip;
        bool loadedEquip = false;

        //================================================================================================================================
        LinearGradientBrush VM = new LinearGradientBrush();
        LinearGradientBrush AM = new LinearGradientBrush();
        LinearGradientBrush AZ = new LinearGradientBrush();
        LinearGradientBrush VD = new LinearGradientBrush();
        LinearGradientBrush VD_1 = new LinearGradientBrush();
        LinearGradientBrush CZ = new LinearGradientBrush();
        LinearGradientBrush LJ = new LinearGradientBrush();

        LinearGradientBrush VM_2 = new LinearGradientBrush();
        LinearGradientBrush AM_2 = new LinearGradientBrush();
        LinearGradientBrush AZ_2 = new LinearGradientBrush();
        LinearGradientBrush VD_2 = new LinearGradientBrush();
        LinearGradientBrush VD_1_2 = new LinearGradientBrush();
        LinearGradientBrush CZ_2 = new LinearGradientBrush();
        LinearGradientBrush LJ_2 = new LinearGradientBrush();

        //================================================================================================================================

        bool ticktack = false;

        #endregion

        public Motor()
        {
            InitializeComponent();

            #region Create LinearGradienBrush

            //================================================================================================================================

            VM.GradientStops.Add(new GradientStop(Color.FromRgb(255, 0, 0), 0.0));
            VM.GradientStops.Add(new GradientStop(Color.FromRgb(255, 160, 160), 0.5));
            VM.GradientStops.Add(new GradientStop(Color.FromRgb(255, 0, 0), 1.0));

            RotateTransform rot = new RotateTransform();
            rot.Angle = 90;
            rot.CenterX = 0.5;
            rot.CenterY = 0.5;

            TransformGroup tgroup = new TransformGroup();
            tgroup.Children.Add(rot);

            VM.RelativeTransform = tgroup;

            //================================================================================================================================

            AM.GradientStops.Add(new GradientStop(Color.FromRgb(200, 200, 0), 0.0));
            AM.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 150), 0.5));
            AM.GradientStops.Add(new GradientStop(Color.FromRgb(200, 200, 0), 1.0));

            RotateTransform rot1 = new RotateTransform();
            rot1.Angle = 90;
            rot1.CenterX = 0.5;
            rot1.CenterY = 0.5;

            TransformGroup tgroup1 = new TransformGroup();
            tgroup1.Children.Add(rot1);

            AM.RelativeTransform = tgroup1;

            //================================================================================================================================

            AZ.GradientStops.Add(new GradientStop(Color.FromRgb(0, 0, 255), 0.0));
            AZ.GradientStops.Add(new GradientStop(Color.FromRgb(160, 160, 255), 0.5));
            AZ.GradientStops.Add(new GradientStop(Color.FromRgb(0, 0, 255), 1.0));

            RotateTransform rot2 = new RotateTransform();
            rot2.Angle = 90;
            rot2.CenterX = 0.5;
            rot2.CenterY = 0.5;

            TransformGroup tgroup2 = new TransformGroup();
            tgroup2.Children.Add(rot2);

            AZ.RelativeTransform = tgroup2;

            //================================================================================================================================

            VD.GradientStops.Add(new GradientStop(Color.FromRgb(0, 100, 0), 0.0));
            VD.GradientStops.Add(new GradientStop(Color.FromRgb(160, 255, 160), 0.5));
            VD.GradientStops.Add(new GradientStop(Color.FromRgb(0, 100, 0), 1.0));

            RotateTransform rot3 = new RotateTransform();
            rot3.Angle = 90;
            rot3.CenterX = 0.5;
            rot3.CenterY = 0.5;

            TransformGroup tgroup3 = new TransformGroup();
            tgroup3.Children.Add(rot3);

            VD.RelativeTransform = tgroup3;

            //================================================================================================================================

            VD_1.GradientStops.Add(new GradientStop(Color.FromRgb(0, 150, 0), 0.0));
            VD_1.GradientStops.Add(new GradientStop(Color.FromRgb(190, 255, 190), 0.5));
            VD_1.GradientStops.Add(new GradientStop(Color.FromRgb(0, 150, 0), 1.0));

            RotateTransform rot10 = new RotateTransform();
            rot10.Angle = 90;
            rot10.CenterX = 0.5;
            rot10.CenterY = 0.5;

            TransformGroup tgroup10 = new TransformGroup();
            tgroup10.Children.Add(rot10);

            VD_1.RelativeTransform = tgroup10;



            //================================================================================================================================

            CZ.GradientStops.Add(new GradientStop(Color.FromRgb(80, 80, 80), 0.0));
            CZ.GradientStops.Add(new GradientStop(Color.FromRgb(200, 200, 200), 0.5));
            CZ.GradientStops.Add(new GradientStop(Color.FromRgb(80, 80, 80), 1.0));

            RotateTransform rot4 = new RotateTransform();
            rot4.Angle = 90;
            rot4.CenterX = 0.5;
            rot4.CenterY = 0.5;

            TransformGroup tgroup4 = new TransformGroup();
            tgroup4.Children.Add(rot4);

            CZ.RelativeTransform = tgroup4;

            //================================================================================================================================

            LJ.GradientStops.Add(new GradientStop(Color.FromRgb(255, 150, 0), 0.0));
            LJ.GradientStops.Add(new GradientStop(Color.FromRgb(200, 200, 200), 0.5));
            LJ.GradientStops.Add(new GradientStop(Color.FromRgb(255, 150, 0), 1.0));

            RotateTransform rot11 = new RotateTransform();
            rot11.Angle = 90;
            rot11.CenterX = 0.5;
            rot11.CenterY = 0.5;

            TransformGroup tgroup11 = new TransformGroup();
            tgroup11.Children.Add(rot11);

            LJ.RelativeTransform = tgroup11;

            //================================================================================================================================

            //================================================================================================================================

            VM_2.GradientStops.Add(new GradientStop(Color.FromRgb(255, 0, 0), 0.0));
            VM_2.GradientStops.Add(new GradientStop(Color.FromRgb(255, 160, 160), 0.5));
            VM_2.GradientStops.Add(new GradientStop(Color.FromRgb(255, 0, 0), 1.0));

            RotateTransform rot5 = new RotateTransform();
            rot5.Angle = 60;
            rot5.CenterX = 0.5;
            rot5.CenterY = 0.5;

            TransformGroup tgroup5 = new TransformGroup();
            tgroup5.Children.Add(rot5);

            VM_2.RelativeTransform = tgroup5;

            //================================================================================================================================

            AM_2.GradientStops.Add(new GradientStop(Color.FromRgb(200, 200, 0), 0.0));
            AM_2.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 150), 0.5));
            AM_2.GradientStops.Add(new GradientStop(Color.FromRgb(200, 200, 0), 1.0));

            RotateTransform rot6 = new RotateTransform();
            rot6.Angle = 60;
            rot6.CenterX = 0.5;
            rot6.CenterY = 0.5;

            TransformGroup tgroup6 = new TransformGroup();
            tgroup6.Children.Add(rot6);

            AM_2.RelativeTransform = tgroup6;

            //================================================================================================================================

            AZ_2.GradientStops.Add(new GradientStop(Color.FromRgb(0, 0, 255), 0.0));
            AZ_2.GradientStops.Add(new GradientStop(Color.FromRgb(160, 160, 255), 0.5));
            AZ_2.GradientStops.Add(new GradientStop(Color.FromRgb(0, 0, 255), 1.0));

            RotateTransform rot7 = new RotateTransform();
            rot7.Angle = 60;
            rot7.CenterX = 0.5;
            rot7.CenterY = 0.5;

            TransformGroup tgroup7 = new TransformGroup();
            tgroup7.Children.Add(rot7);

            AZ_2.RelativeTransform = tgroup7;

            //================================================================================================================================

            VD_2.GradientStops.Add(new GradientStop(Color.FromRgb(0, 100, 0), 0.0));
            VD_2.GradientStops.Add(new GradientStop(Color.FromRgb(160, 255, 160), 0.5));
            VD_2.GradientStops.Add(new GradientStop(Color.FromRgb(0, 100, 0), 1.0));

            RotateTransform rot8 = new RotateTransform();
            rot8.Angle = 60;
            rot8.CenterX = 0.5;
            rot8.CenterY = 0.5;

            TransformGroup tgroup8 = new TransformGroup();
            tgroup8.Children.Add(rot8);

            VD_2.RelativeTransform = tgroup8;

            //================================================================================================================================

            VD_1_2.GradientStops.Add(new GradientStop(Color.FromRgb(0, 150, 0), 0.0));
            VD_1_2.GradientStops.Add(new GradientStop(Color.FromRgb(190, 255, 190), 0.5));
            VD_1_2.GradientStops.Add(new GradientStop(Color.FromRgb(0, 150, 0), 1.0));

            RotateTransform rot9 = new RotateTransform();
            rot9.Angle = 60;
            rot9.CenterX = 0.5;
            rot9.CenterY = 0.5;

            TransformGroup tgroup9 = new TransformGroup();
            tgroup9.Children.Add(rot9);

            VD_1_2.RelativeTransform = tgroup9;



            //================================================================================================================================

            CZ_2.GradientStops.Add(new GradientStop(Color.FromRgb(80, 80, 80), 0.0));
            CZ_2.GradientStops.Add(new GradientStop(Color.FromRgb(200, 200, 200), 0.5));
            CZ_2.GradientStops.Add(new GradientStop(Color.FromRgb(80, 80, 80), 1.0));

            RotateTransform rot12 = new RotateTransform();
            rot12.Angle = 60;
            rot12.CenterX = 0.5;
            rot12.CenterY = 0.5;

            TransformGroup tgroup12 = new TransformGroup();
            tgroup12.Children.Add(rot12);

            CZ_2.RelativeTransform = tgroup12;

            //================================================================================================================================

            LJ_2.GradientStops.Add(new GradientStop(Color.FromRgb(255, 150, 0), 0.0));
            LJ_2.GradientStops.Add(new GradientStop(Color.FromRgb(200, 200, 200), 0.5));
            LJ_2.GradientStops.Add(new GradientStop(Color.FromRgb(255, 150, 0), 1.0));

            RotateTransform rot13 = new RotateTransform();
            rot13.Angle = 60;
            rot13.CenterX = 0.5;
            rot13.CenterY = 0.5;

            TransformGroup tgroup13 = new TransformGroup();
            tgroup13.Children.Add(rot13);

            LJ_2.RelativeTransform = tgroup13;


            #endregion


            this.PreviewMouseLeftButtonUp += motor_PreviewMouseLeftButtonUp;


        }

        private void motor_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Utilidades.VariaveisGlobais.NumberOfGroup_GS == 0)
            {
                Utilidades.messageBox inputDialog = new messageBox(Utilidades.VariaveisGlobais.faltaUsuarioTitle, Utilidades.VariaveisGlobais.faltaUsuarioMessage, MaterialDesignThemes.Wpf.PackIconKind.Error, "OK", "Fechar");

                inputDialog.ShowDialog();

                return;
            }

            equip.OpenWindow();
        }

        public void loadEquip(typeEquip Equip, typeCommand TCommand, int initialOffSet, int bufferPlc, string nome, string tag, string numeroPartida, string paginaProjeto)
        {
            equip = new EquipsControl(Equip, TCommand, initialOffSet, bufferPlc, nome, tag, numeroPartida, paginaProjeto);
            loadedEquip = true;
        }

        public EquipsControl Equip_GS { get => equip; }

        public void actualize_UI()
        {
            if (loadedEquip)
            {
                ticktack = Utilidades.VariaveisGlobais.TickTack_GS;

                if (!equip.Command_Get.Standard.Emergencia)
                {
                    Rec4.Dispatcher.BeginInvoke((Action)(() => Rec4.Fill = VM_2));
                    Rec5.Dispatcher.BeginInvoke((Action)(() => Rec5.Fill = VM_2));
                    Rec6.Dispatcher.BeginInvoke((Action)(() => Rec6.Fill = VM_2));


                }
                else if (equip.Command_Get.Standard.Falha_Geral)
                {
                    if (ticktack)
                    {
              
                      
     
                        Rec4.Dispatcher.BeginInvoke((Action)(() => Rec4.Fill = VM_2));
                        Rec5.Dispatcher.BeginInvoke((Action)(() => Rec5.Fill = VM_2));
                        Rec6.Dispatcher.BeginInvoke((Action)(() => Rec6.Fill = VM_2));

                    }
                    else
                    {
               
                        Rec4.Dispatcher.BeginInvoke((Action)(() => Rec4.Fill = CZ_2));
                        Rec5.Dispatcher.BeginInvoke((Action)(() => Rec5.Fill = CZ_2));
                        Rec6.Dispatcher.BeginInvoke((Action)(() => Rec6.Fill = CZ_2));
                    }

                }
                else if (equip.Command_Get.Standard.Manutencao)
                {

                    Rec4.Dispatcher.BeginInvoke((Action)(() => Rec4.Fill = AZ_2));
                    Rec5.Dispatcher.BeginInvoke((Action)(() => Rec5.Fill = AZ_2));
                    Rec6.Dispatcher.BeginInvoke((Action)(() => Rec6.Fill = AZ_2));
                }
                else if (!equip.Command_Get.Standard.Liberado)
                {

                    Rec4.Dispatcher.BeginInvoke((Action)(() => Rec4.Fill = AM_2));
                    Rec5.Dispatcher.BeginInvoke((Action)(() => Rec5.Fill = AM_2));
                    Rec6.Dispatcher.BeginInvoke((Action)(() => Rec6.Fill = AM_2));
                }
                else if (equip.Command_Get.Standard.Ligando)
                {
                    if (ticktack)
                    {

                        Rec4.Dispatcher.BeginInvoke((Action)(() => Rec4.Fill = VD_2));
                        Rec5.Dispatcher.BeginInvoke((Action)(() => Rec5.Fill = VD_2));
                        Rec6.Dispatcher.BeginInvoke((Action)(() => Rec6.Fill = VD_2));


                    }
                    else
                    {

                        Rec4.Dispatcher.BeginInvoke((Action)(() => Rec4.Fill = VD_2));
                        Rec5.Dispatcher.BeginInvoke((Action)(() => Rec5.Fill = VD_2));
                        Rec6.Dispatcher.BeginInvoke((Action)(() => Rec6.Fill = VD_2));


                    }
                }
                else if (equip.Command_Get.Standard.Desligando)
                {
                    if (ticktack)
                    {

                        Rec4.Dispatcher.BeginInvoke((Action)(() => Rec4.Fill = VD_2));
                        Rec5.Dispatcher.BeginInvoke((Action)(() => Rec5.Fill = VD_2));
                        Rec6.Dispatcher.BeginInvoke((Action)(() => Rec6.Fill = VD_2));


                    }
                    else
                    {

                        Rec4.Dispatcher.BeginInvoke((Action)(() => Rec4.Fill = CZ_2));
                        Rec5.Dispatcher.BeginInvoke((Action)(() => Rec5.Fill = CZ_2));
                        Rec6.Dispatcher.BeginInvoke((Action)(() => Rec6.Fill = CZ_2));

                    }
                }
                else
                {
                    if (equip.Command_Get.Standard.Ligado)
                    {

                        Rec4.Dispatcher.BeginInvoke((Action)(() => Rec4.Fill = VD_2));
                        Rec5.Dispatcher.BeginInvoke((Action)(() => Rec5.Fill = VD_2));
                        Rec6.Dispatcher.BeginInvoke((Action)(() => Rec6.Fill = VD_2));


                    }
                    else
                    {

                        Rec4.Dispatcher.BeginInvoke((Action)(() => Rec4.Fill = CZ_2));
                        Rec5.Dispatcher.BeginInvoke((Action)(() => Rec5.Fill = CZ_2));
                        Rec6.Dispatcher.BeginInvoke((Action)(() => Rec6.Fill = CZ_2));


                    }
                }
                #region Names

                if (equip.Command_Get.Standard.Automatico)
                {
                    LB_M_A.Dispatcher.BeginInvoke((Action)(() => LB_M_A.Content = "A"));

                }
                else
                {
                    LB_M_A.Dispatcher.BeginInvoke((Action)(() => LB_M_A.Content = "M"));
                }

                #endregion
            }
        }

        public bool actualize_Equip
        {
            set
            {
                if (loadedEquip)
                {
                    //Atualiza Equipamento
                    equip.actualize_Equip = value;

                    //Atualiza visual do equipamento
                    actualize_UI();

                    //Atualiza tela de controle e status do equipamento

                }

            }
            get { return true; }

        }
    }
}
