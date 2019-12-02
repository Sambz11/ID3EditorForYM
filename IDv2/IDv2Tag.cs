using IDv2.Frames;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace IDv2
{
    public class IDv2Tag
    {
        private Header _header;
        private ExtendedHeader _extendedHeader;
        private Dictionary<string, IFrame> _frames;


        public IDv2Tag()
        {
            _header = new Header();
            _extendedHeader = new ExtendedHeader();
        }

        public IDv2Tag(string path)
        {
            using (FileStream fileStream = File.Open(path, FileMode.Open))
            {

                byte[] buffer = new byte[10];
                fileStream.Read(buffer, 0, 10);

                _header = new Header(buffer);
                if (_header.ExtendedHeaderIncluded)
                {
                    fileStream.Read(buffer, 0, 10);
                }

                _frames = new Dictionary<string, IFrame>(0);

                FrameHeader frameHeader;
                while (fileStream.Position < _header.Size)
                {

                    fileStream.Read(buffer, 0, 10);

                    if (!(new Regex("[A-Z]").Match(((char)buffer[0]).ToString()).Success))
                    {
                        fileStream.Position -= 9;
                        continue;
                    }

                    frameHeader = new FrameHeader(buffer);

                    byte[] frameData = new byte[frameHeader.FrameSize];

                    fileStream.Read(frameData, 0, frameData.Length);

                    _frames.Add(frameHeader.FrameID, new Frame(frameHeader, frameData));
                }
                
            }
        }

        public string Artist 
        { 
            get
            {
                IFrame frame;
                if (_frames.TryGetValue("TPE1", out frame))
                    return TextInformationFrame.BytesToString(frame.Data);
                else
                    return "";
            }
        }

        public string Title 
        {
            get
            {
                IFrame frame;
                if (_frames.TryGetValue("TIT2", out frame))
                    return TextInformationFrame.BytesToString(frame.Data);
                else
                    return "";
            }
        }

    }
}
