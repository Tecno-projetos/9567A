using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9567A_V00___PI.Utilidades
{
    public class Conversions
    {

        public static void Dword_To_Bit(UInt32 Dword, ref bool[] Bits, bool Swap)
        {
            UInt32 value = 1;

            if (Swap)
            {
                //Swap de bytes na High word e Low word
                Dword = ((Dword >> 8) & 0x00FF00FF) | ((Dword << 8) & 0xFF00FF00);

                //Swap de word na dword
                Dword = ((Dword >> 16) & 0x0000FFFF) | ((Dword << 16) & 0xFFFF0000);
            }

            for (int i = 0; i <= 31; i++)
            {
                if ((Dword & value) == value)
                {
                    Bits[i] = true;
                }
                else
                {
                    Bits[i] = false;
                }

                value = value * 2;
            }
        }


        public static UInt32 Bit_To_Dword(ref bool[] Bits, bool Swap)
        {

            UInt32 value = 1;
            UInt32 Dword = 0;

            for (int i = 0; i <= 31; i++)
            {
                if (Bits[i])
                {
                    Dword += value;
                }

                value = value * 2;
            }

            if (Swap)
            {
                //Swap de bytes na High word e Low word
                Dword = ((Dword >> 8) & 0x00FF00FF) | ((Dword << 8) & 0xFF00FF00);

                //Swap de word na dword
                Dword = ((Dword >> 16) & 0x0000FFFF) | ((Dword << 16) & 0xFFFF0000);
            }

            return Dword;
        }

        public static byte Bit_To_Byte(ref bool[] Bits)
        {

            Byte value = 1;
            Byte _byte = 0;

            for (int i = 0; i <= 7; i++)
            {
                if (Bits[i])
                {
                    _byte += value;
                }

                if (i != 7 )
                {
                    value = Convert.ToByte(value * 2);
                }

            }
            return _byte;
        }

        public static void Byte_To_Bit(Byte _byte, ref bool[] Bits)
        {

            int value = 1;


            for (int i = 0; i <= 7; i++)
            {
                if ((_byte & value) == value)
                {
                    Bits[i] = true;
                }
                else
                {
                    Bits[i] = false;
                }

                value = value * 2;
            }

        }

    

        public static void Word_To_Bit(UInt16 Word, ref bool[] Bits, bool Swap)
        {

            UInt16 value = 1;

            if (Swap)
            {
                //Swap de bytes na High byte e Low byte
                Word = Convert.ToUInt16(Convert.ToUInt16((Word >> 8) & 0x00FF) | Convert.ToUInt16((Word << 8) & 0xFF00));
            }

            for (int i = 0; i <= 15; i++)
            {
                if ((Word & value) == value)
                {
                    Bits[i] = true;
                }
                else
                {
                    Bits[i] = false;
                }

                if (i !=15)
                {
                    value = Convert.ToUInt16(value * 2);
                }

            }

        }

        public static UInt16 Bit_To_Word(ref bool[] Bits, bool Swap)
        {

            UInt16 value = 1;
            UInt16 Word = 0;

            for (int i = 0; i <= 15; i++)
            {
                if (Bits[i])
                {
                    Word += value;
                }

                if (i != 15)
                {
                    value = Convert.ToUInt16(value * 2);
                }

                //value = Convert.ToUInt16(value * 2);
            }

            if (Swap)
            {
                //Swap de bytes na High byte e Low byte
                Word = Convert.ToUInt16(Convert.ToUInt16((Word >> 8) & 0x00FF) | Convert.ToUInt16((Word << 8) & 0xFF00));


            }

            return Word;
        }

    }
}
