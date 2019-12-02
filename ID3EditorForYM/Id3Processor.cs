using System;
using System.Collections.Generic;
using System.Text;

namespace ID3EditorForYM
{
    class Id3Processor
    {
        private byte[] headers = new byte[10];

        char[] marker = {'I','D','3'};
        byte version = 4;
        byte subVersion = 0;
        byte flags = 0b0100000;
        byte length;

    }
}
