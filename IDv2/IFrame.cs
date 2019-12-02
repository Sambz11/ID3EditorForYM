using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IDv2
{
    interface IFrame
    {
        FrameHeader FrameHeader { get; set; }

        byte[] Data { get; set; }

    }
}
