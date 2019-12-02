using System;
using System.Collections.Generic;
using System.Text;

namespace IDv2.Frames
{
    public class Frame : IFrame
    {
        private FrameHeader _frameHeader;
        private byte[] _data;

        public Frame(FrameHeader header, byte[] frameBytes)
        {

            _frameHeader = header;
            //frameBytes.CopyTo(_data, 0);
            _data = frameBytes;
        }

        public FrameHeader FrameHeader { get => _frameHeader; set => _frameHeader = value; }
        public byte[] Data { get => _data; set => _data = value; }
    }
}
