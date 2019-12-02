using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleInterface
{
    class ID3v2Test
    {
        public IDHeader header;
        public Dictionary<string, TagFrame> Tags;

        public ID3v2Test(byte[] headerBytes)
        {
            header.marker = Encoding.ASCII.GetString(headerBytes[0..3]);
            header.version = headerBytes[3];
            header.subVersion = headerBytes[4];
            header.flags = headerBytes[5];

            header.length = 0;

            for (int i = 6; i < 10; ++i)
            {
                header.length += (int)headerBytes[i] * (1 << (7 * (9 - i)));
            }

            Tags = new Dictionary<string, TagFrame>(0);
        }

        public void GetID(byte[] data)
        {
            int pointer = 0;
            

            while (pointer < data.Length)
            {
                
                if (!(new Regex("[A-Z]").Match(((char)data[pointer]).ToString()).Success))
                {
                    pointer++;
                    continue;
                }
                

                TagFrame tmpTag = new TagFrame
                {
                    Id = Encoding.ASCII.GetString(data, pointer, 4),
                    flags = data[(pointer + 8)..(pointer + 10)]
                };

                tmpTag.length = data[pointer + 7];
                tmpTag.length += data[pointer + 6] << 8;
                tmpTag.length += data[pointer + 5] << 16;
                tmpTag.length += data[pointer + 4] << 24;

                

                byte[] resultBytes = data[(pointer + 11)..(pointer + 10 + tmpTag.length)];
                if (data[pointer + 10] == 1)
                {
                    tmpTag.data = Encoding.Unicode.GetString(resultBytes);
                }
                else
                {
                    tmpTag.data = Encoding.GetEncoding("ISO-8859-1").GetString(resultBytes);
                }
                pointer += (tmpTag.length + 10);

                Tags.Add(tmpTag.Id, tmpTag);
            }
        }

        public static int ToInt32LE(byte[] value, int startIndex)
        {
            int result = 0;
            int endIndex = startIndex + 4;

            byte[] reverseLengthBytes = value[startIndex..endIndex];
            Array.Reverse(reverseLengthBytes);

            result = BitConverter.ToInt32(reverseLengthBytes, 0);

            return result;
        }
    }

    struct TagFrame
    {
        public string Id;
        public int length;
        public byte[] flags;

        public string data;
    }

    struct IDHeader
    {
        public string marker;
        public byte version;
        public byte subVersion;
        public byte flags;
        public int length;
    }



} 
