using System;
using System.Collections.Generic;
using System.Text;
using L2Proxy;

namespace L2PacketEditor
{
    class ByteParser
    {
        public static byte[] ParseBytes(string str)
        {
            ByteBuffer buffer = new ByteBuffer();
            // writeD(7);writeS(hallo);writeD(7);
            string[] splitted = str.Split(';');

            int len = 7;
            foreach (string s in splitted)
            {
                if (s.StartsWith("writeC"))
                {
                    string tmp = s.Substring(len, s.Length - (len + 1));
                    if (tmp.StartsWith("0x"))
                    {
                        buffer.WriteByte(byte.Parse(tmp, System.Globalization.NumberStyles.HexNumber));
                    }
                    else
                    {
                        buffer.WriteByte(byte.Parse(tmp));
                    }
                }
                else if (s.StartsWith("writeF"))
                {
                    string tmp = s.Substring(len, s.Length - (len + 1));
                    if (tmp.StartsWith("0x"))
                    {
                        buffer.WriteDouble(double.Parse(tmp, System.Globalization.NumberStyles.HexNumber));
                    }
                    else
                    {
                        buffer.WriteDouble(double.Parse(tmp));
                    }
                }
                else if (s.StartsWith("writeH"))
                {
                    string tmp = s.Substring(len, s.Length - (len + 1));
                    if (tmp.StartsWith("0x"))
                    {
                        buffer.WriteInt16(short.Parse(tmp, System.Globalization.NumberStyles.HexNumber));
                    }
                    else
                    {
                        buffer.WriteInt16(short.Parse(tmp));
                    }
                }
                else if (s.StartsWith("writeD"))
                {
                    string tmp = s.Substring(len, s.Length - (len + 1));
                    if (tmp.StartsWith("0x"))
                    {
                        buffer.WriteInt32(int.Parse(tmp, System.Globalization.NumberStyles.HexNumber));
                    }
                    else
                    {
                        buffer.WriteInt32(int.Parse(tmp));
                    }
                }
                else if (s.StartsWith("writeQ"))
                {
                    string tmp = s.Substring(len, s.Length - (len + 1));
                    if (tmp.StartsWith("0x"))
                    {
                        buffer.WriteInt64(long.Parse(tmp, System.Globalization.NumberStyles.HexNumber));
                    }
                    else
                    {
                        buffer.WriteInt64(long.Parse(tmp));
                    }
                }
                else if (s.StartsWith("writeB"))
                {
                    string tmp = s.Substring(len, s.Length - (len + 1));
                    if (tmp.StartsWith("0x"))
                    {
                        buffer.WriteByte(byte.Parse(tmp, System.Globalization.NumberStyles.HexNumber));
                    }
                    else
                    {
                        buffer.WriteByte(byte.Parse(tmp));
                    }
                }
                else if (s.StartsWith("writeS"))
                {
                    string tmp = s.Substring(len, s.Length - (len + 1));
                    buffer.WriteString(tmp);
                }
                else
                {
                    if (s != "")
                    {
                        throw new ArgumentException("Syntax error: " + s);
                    }
                }
            }
            return buffer.Get_ByteArray();
        }
    }
}
