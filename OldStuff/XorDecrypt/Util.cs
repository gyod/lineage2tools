using System;
using System.Collections.Generic;
using System.Text;

namespace L2PacketDecrypt
{
    public class Util
    {
        public static string printData(byte[] data)
        {
            return printData(data, data.Length);
        }

        public static string printData(ByteBuffer buf)
        {
            return printData(buf.Get_ByteArray());
        }

        public static string printData(byte[] data, int len)
        {
            StringBuilder result = new StringBuilder();
            int counter = 0;
            for (int i = 0; i < len; i++)
            {
                if (counter % 16 == 0)
                {
                    result.Append(fillHex(i, 4) + ": ");
                }

                result.Append(fillHex(data[i] & 0xff, 2) + " ");
                counter++;
                if (counter == 16)
                {
                    result.Append("   ");

                    int charpoint = i - 15;
                    for (int a = 0; a < 16; a++)
                    {
                        int t1 = data[charpoint++];
                        if (t1 > 0x1f && t1 < 0x80)
                        {
                            result.Append((char)t1);
                        }
                        else
                        {
                            result.Append('.');
                        }
                    }
                    result.Append("\r\n");
                    counter = 0;
                }
            }
            int rest = data.Length % 16;
            if (rest > 0)
            {
                for (int i = 0; i < 17 - rest; i++)
                {
                    result.Append("   ");
                }

                int charpoint = data.Length - rest;
                for (int a = 0; a < rest; a++)
                {
                    int t1 = data[charpoint++];
                    if (t1 > 0x1f && t1 < 0x80)
                    {
                        result.Append((char)t1);
                    }
                    else
                    {
                        result.Append('.');
                    }
                }

                result.Append("\n");
            }
            return result.ToString();
        }

        public static string fillHex(int data, int digits)
        {
            string number = string.Format("{0:X2} ", data);

            for (int i = number.Length; i < digits; i++)
            {
                number = "0" + number;
            }

            return number;
        }

        public static byte[] convertStringToByteArray(string str)
        {
            string[] data = str.Split(' ');
            byte[] buffer = new byte[data.Length];
            int i = 0;
            foreach (string s in data)
            {
                buffer[i] = byte.Parse(s, System.Globalization.NumberStyles.HexNumber);
                i++;
            }
            return buffer;
        }

    }
}
