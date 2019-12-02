using System;
using System.Collections.Generic;
using System.Text;

namespace IDv2
{
    public class FrameHeader
    {
        private string _frameID;
        private uint _frameSize;

        public string FrameID 
        {
            get { return _frameID; }
        }

        public uint FrameSize
        {
            get { return _frameSize; }
        }

        public uint FrameSizeTotal
        {
            get { return _frameSize + 10; }
        }

        /*
         * Frame Flags
         */

        public FrameHeader() { }

        public FrameHeader(byte[] frameHeaderBytes)
        {
            _frameID = Encoding.ASCII.GetString(frameHeaderBytes[0..4]);

            _frameSize = (uint)frameHeaderBytes[7];
            _frameSize += (uint)frameHeaderBytes[6] << 8;
            _frameSize += (uint)frameHeaderBytes[5] << 16;
            _frameSize += (uint)frameHeaderBytes[4] << 24;

        }



    }
}
