using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wrapper
{
    /// <summary>
    /// codigos Hexadecimales qeu se utilizan recurrentemente en los mensajes al POS
    /// </summary>
    public enum HexCodes
    {
        ACK = 0x06,
        NAK = 0x15,
        STX = 0x02,
        ETX = 0x03,
        PIPE = 0x7C
    };
}
