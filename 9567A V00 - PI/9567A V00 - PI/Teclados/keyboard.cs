using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _9567A_V00___PI.Teclados
{

    /// <summary>
    /// No aplicativo Configurações , vá para a categoria Dispositivos e selecione Digitação . Se você rolar até o fim, poderá encontrar a configuração "Mostrar o teclado virtual ou o painel de escrita quando não estiver no modo tablet e não houver teclado conectado", que você pode ativar.
    /// </summary>

    public class keyboard
    {
        private static bool possuiTeclado = false;
       /// <summary>
       /// Abre teclado virtual para digitação
       /// </summary>
        public static void openKeyboard() 
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select Name from Win32_Keyboard");

            string touchKeyboardPath = @"C:\Program Files\Common Files\Microsoft Shared\Ink\TabTip.exe";
         
            foreach (ManagementObject keyboard in searcher.Get())
            {
                if (!keyboard.GetPropertyValue("Name").Equals(""))
                {
                    possuiTeclado = true;
                }

            }
            if (possuiTeclado && Utilidades.VariaveisGlobais.AtivaDesativaTecladoVirtual)
            {
                closeKeyboard();
            }
            else
            {
                closeKeyboard();
                Process.Start(touchKeyboardPath);
            }
        }

        /// <summary>
        /// Fecha Teclado Virtual
        /// </summary>
        public static void closeKeyboard() 
        {
   
                Process[] tabtip = Process.GetProcessesByName("TabTip");

                if (null != tabtip)
                {
                    tabtip.ToList().ForEach(a => { if (null != a) { a.Kill(); } });

                }
           
        }

    }
}
