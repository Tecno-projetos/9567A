using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomasulGUI.Aux_Screen
{
    public class Call_Screens
    {

        public static Aux_Screen.ProgramScan Screen_Scan = new Aux_Screen.ProgramScan();

        public static Aux_Screen.Buffer_Diag Window_Buffer_Diagnostic = new Aux_Screen.Buffer_Diag();

        public static UsersControl.UsersControl Window_User_Control = new UsersControl.UsersControl();

        public static Aux_Screen.ProgressExport ProgramExport = new Aux_Screen.ProgressExport();

        public static Email.Conf_Email Email = new Email.Conf_Email();

        public static Pesquisa.Screen_Search_FullHD Pesquisa_FullHD = new Pesquisa.Screen_Search_FullHD();

        public static Aux_Screen.Legend Legenda = new Legend();

        public static Manutencao.Config_Manutencao config_Manutencao = new Manutencao.Config_Manutencao();

        public static Manutencao.popUp_Descricao_Manutencao popUp_Descricao = new Manutencao.popUp_Descricao_Manutencao();

        public static Manutencao.popUp_Historico popUp_Historico = new Manutencao.popUp_Historico();


    }

}
