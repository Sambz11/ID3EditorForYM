using System;
using System.Collections.Generic;
using System.Text;

namespace IDv2.Frames
{
    public class TextInformationFrame : IFrame
    {

        private FrameHeader _frameHeader;
        private string _value;

        private Encoding _encoding;
        
        
        public FrameHeader FrameHeader 
        {   
            get { return _frameHeader; }
            set { _frameHeader = value; } 
        }

        public string Value 
        {
            get { return _value; } 
            set
            {
                _value = value;
            } 
        }

        public byte[] Data { get; set; }

        public TextInformationFrame()
        {

        }
        public TextInformationFrame(byte[] frameBytes, FrameHeader frameHeader)
        {
            _frameHeader = frameHeader;

            frameBytes.CopyTo(Data, 0);

            _encoding = (frameBytes[0]) switch
            {
                0 => Encoding.GetEncoding("ISO-8859-1"),
                1 => Encoding.Unicode,
                _ => throw new ArgumentException("Unknown encoding"),
            };
            _value = _encoding.GetString(frameBytes[11..]);

        }

        public byte[] GetBytes()
        {
            return new byte[0];
        }

        public string GetFrameID()
        {
            return _frameHeader.FrameID;
        }

        public static string BytesToString(byte[] frame)
        {
            Encoding encoding = (frame[0]) switch
            {
                0 => Encoding.GetEncoding("ISO-8859-1"),
                1 => Encoding.Unicode,
                _ => throw new ArgumentException("Unknown encoding"),
            };
            return encoding.GetString(frame[1..]);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
