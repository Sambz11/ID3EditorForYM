using System;
using System.IO;
using System.Text;
using IDv2;

namespace ConsoleInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename;
            string path = @"E:\CSharp\Projects\ID3EditorForYM\ConsoleInterface";

            Console.Write("File name: ");
            filename = Console.ReadLine();
            path += $"/{filename}.mp3";

            /*
            ID3v2Test trackInfo;

            FileStream fileStream;
            
            using (fileStream = new FileStream(path, FileMode.Open))
            {
                byte[] header = new byte[10];
                fileStream.Read(header, 0, header.Length);
                trackInfo = new ID3v2Test(header);
                byte[] data = new byte[trackInfo.header.length];
                fileStream.Read(data, 0, data.Length);
                trackInfo.GetID(data);

            }
            */

            IDv2Tag iDv2Tag = new IDv2Tag(path);

            Console.OutputEncoding = Encoding.Unicode;

            Console.WriteLine("Artist   {0}", iDv2Tag.Artist);
            Console.WriteLine("Title    {0}", iDv2Tag.Title);

            Console.ReadLine();
        }
    }
}
