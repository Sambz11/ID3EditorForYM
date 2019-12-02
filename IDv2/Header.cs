using System;
using System.Collections.Generic;
using System.Text;

namespace IDv2
{

    public class Header
    {
        // Identifier "ID3"
        private readonly byte[] _marker = { 0x49, 0x44, 0x33 };
        private int _version;
        private int _subVersion;
        private bool _unsynchronisationFlag;
        private bool _extendedHeaderFlag;
        private bool _experimentalFlag;
        private uint _size;

        public uint Size 
        {
            get { return _size; }
            set 
            {
                _size = value;
                //Event!
            }
        }

        public bool ExtendedHeaderIncluded
        {
            get { return _extendedHeaderFlag; }
            set 
            { 
                _extendedHeaderFlag = value; 
                //Event!
            }
        }

        public Header()
        {

        }

        public Header(byte[] headerBytes)
        {
            if (!headerBytes[0..3].Equals(_marker))
            {
                //throw new ArgumentException("File doesn`t contain IDv2Tag");
            }

            _version = headerBytes[3];
            _subVersion = headerBytes[4];

            _unsynchronisationFlag = (headerBytes[5] & 0x80) == 1;
            _extendedHeaderFlag = (headerBytes[5] & 0x40) == 1;
            _experimentalFlag = (headerBytes[5] & 0x20) == 1;

            _size = (uint)headerBytes[9];
            _size += (uint)headerBytes[8] << 7;
            _size += (uint)headerBytes[7] << 14;
            _size += (uint)headerBytes[6] << 21;

        }


        public byte[] GetBytes()
        {
            return new byte[0];
        }
    }
}
