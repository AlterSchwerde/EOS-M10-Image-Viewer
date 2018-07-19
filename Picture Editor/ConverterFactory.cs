using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;

namespace Picture_Editor
{
    class ConverterFactory
    {


        // 16.909.060 als Hex = 01020304
        // Big-Endian --> 01 02 03 04
        // Little-Endian --> 04 03 02 01

        static int s_beginningDataBytes = 992;


        #region GetValues
        public static Dictionary<int, string> GetValues(byte[] allBytes, byte[] makernoteBytes, bool bigEndian, int type, bool isHex, int length, int offset)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            //byte[] bytes = new byte[];



            if (offset == 0 || length == 1)
            {
                if (type == 2)
                {
                    int z = 0;
                }
                else if (type == 3)
                {
                    byte[] valueBytes = { makernoteBytes[8], makernoteBytes[9], makernoteBytes[10], makernoteBytes[11] };
                    Int64 value16 = ByteToInt16(valueBytes, true);
                    dic.Add(0, value16.ToString());
                }
                else if (type == 4)
                {
                    byte[] valueBytes = { makernoteBytes[8], makernoteBytes[9], makernoteBytes[10], makernoteBytes[11] };
                    Int64 value64 = ByteToInt64(valueBytes, true);
                    dic.Add(0, value64.ToString());
                }
            }
            else
            {
                // ascii = bit byte
                if (type == 1)
                {
                    int count = length;
                    byte[] valueBytes = new byte[count];
                    int index = offset - s_beginningDataBytes;
                    for (int i = 0; i < count; i++)
                    {
                        valueBytes[i] = allBytes[index + i];
                    }

                    dic = MakernoteFactory.GetValuesInt8(valueBytes);
                }
                // ascii = bit byte
                else if (type == 2)
                {
                    int count = length;
                    byte[] valueBytes = new byte[count];
                    int index = offset - s_beginningDataBytes;
                    for (int i = 0; i < count; i++)
                    {
                        valueBytes[i] = allBytes[index + i];
                    }

                    dic = MakernoteFactory.GetValuesString(valueBytes, false);
                }
                // short = 2 byte
                else if (type == 3)
                {
                    int count = length * 2;
                    byte[] valueBytes = new byte[count];
                    int index = offset - s_beginningDataBytes;
                    for (int i = 0; i < count; i = i + 2)
                    {
                        valueBytes[i] = allBytes[index + i];
                        valueBytes[i + 1] = allBytes[index + (i + 1)];
                    }

                    if (isHex)
                        dic = MakernoteFactory.GetValuesHex(valueBytes, false);
                    else
                        dic = MakernoteFactory.GetValuesInt16(valueBytes, true);


                }

                // long = 4 byte
                else if (type == 4)
                {
                    int count = length * 4;
                    byte[] valueBytes = new byte[count];
                    int index = offset - s_beginningDataBytes;
                    for (int i = 0; i < count; i = i + 4)
                    {
                        valueBytes[i] = allBytes[index + i];
                        valueBytes[i + 1] = allBytes[index + (i + 1)];
                        valueBytes[i + 2] = allBytes[index + (i + 2)];
                        valueBytes[i + 3] = allBytes[index + (i + 3)];
                    }

                    dic = MakernoteFactory.GetValuesInt32(valueBytes, bigEndian);
                }
            }

            return dic;
        }
        #endregion


        #region ByteToString
        public static string ByteToString(byte[] bytes, bool bigEndian)
        {
            string valueString = null;

            if (!bigEndian)
            {
                byte[] bytesBigEndian = MakeBigEndian(bytes);
                bytes = bytesBigEndian;
            }

            valueString = Encoding.ASCII.GetString(bytes);

            return valueString;
        }
        #endregion


        #region ByteToHex
        public static string ByteToHex(byte[] bytes, bool bigEndian)
        {
            string valueHex = null;

            if (!bigEndian)
            {
                byte[] bytesBigEndian = MakeBigEndian(bytes);
                bytes = bytesBigEndian;
            }

            valueHex = BitConverter.ToString(bytes).Replace("-", string.Empty);

            return valueHex;
        }
        #endregion


        #region ByteToInt8
        public static int ByteToInt8(byte[] bytes)
        {
            int valueInt;

            valueInt = Convert.ToInt16(bytes[0]);

            return valueInt;
        }
        #endregion


        #region ByteToInt16
        public static Int16 ByteToInt16(byte[] bytes, bool bigEndian)
        {
            Int16 valueInt16;

            if (!bigEndian)
            {
                byte[] bytesBigEndian = MakeBigEndian(bytes);
                bytes = bytesBigEndian;
            }

            valueInt16 = BitConverter.ToInt16(bytes, 0);

            return valueInt16;
        }
        #endregion


        #region ByteToInt64
        public static Int64 ByteToInt64(byte[] bytes, bool bigEndian)
        {
            Int64 valueInt64 = 0;

            if (!bigEndian)
            {
                byte[] bytesBigEndian = MakeBigEndian(bytes);
                bytes = bytesBigEndian;
            }

            if (bytes.Length == 8)
            {
                UInt64 valueUInt64 = BitConverter.ToUInt64(bytes, 0);
                valueInt64 = Convert.ToInt64(valueUInt64);

            }
            if (bytes.Length == 4)
            {
                Int32 valueInt32 = BitConverter.ToInt32(bytes, 0);
                valueInt64 = Convert.ToInt64(valueInt32);
            }
            if (bytes.Length == 2)
            {
                Int16 valueInt16 = BitConverter.ToInt16(bytes, 0);
                valueInt64 = Convert.ToInt64(valueInt16);
            }

            return valueInt64;
        }
        #endregion


        #region MakeBigEndian
        public static byte[] MakeBigEndian(byte[] bytes)
        {
            byte[] orderdBytes = new byte[bytes.Length];
            int lengthBytes = bytes.Length;

            for (int i = 0; i < lengthBytes; i++)
                orderdBytes[i] = bytes[(lengthBytes - 1) - i];


            return orderdBytes;
        }
        #endregion

    }
}
