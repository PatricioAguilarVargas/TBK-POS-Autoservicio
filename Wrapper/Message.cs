using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wrapper
{
    /// <summary>
    /// case qeu genera losmensajes bases para ser enviados al POS
    /// </summary>
    public static class Message
    {
        private static byte[] polling;
        /// <summary>
        /// retorna el mensaje para el polling
        /// </summary>
        public static byte[] Polling
        {
            get {
                byte[] current = new byte[7];
                current[0] = (byte)HexCodes.STX;    //<STX>
                current[1] = 0x30;                  // 0
                current[2] = 0x31;                  // 1
                current[3] = 0x30;                  // 0
                current[4] = 0x30;                  // 0
                current[5] = (byte)HexCodes.ETX;    //<ETX>
                current[6] = 0x02;                  //<LRC>
                Message.polling = current;
                return Message.polling; 
            }

        }
      

        private static byte[] loadKeys;
        /// <summary>
        /// retorna el mensaje para la carga de llaves
        /// </summary>
        public static byte[] LoadKeys
        {
            get {
                byte[] current = new byte[7];
                current[0] = (byte)HexCodes.STX;    //<STX>
                current[1] = 0x30;                  // 0
                current[2] = 0x38;                  // 8
                current[3] = 0x30;                  // 0
                current[4] = 0x30;                  // 0
                current[5] = (byte)HexCodes.ETX;    //<ETX>
                current[6] = 0x0B;                  //<LRC>
                Message.loadKeys = current;
                return Message.loadKeys; 
            }

        }


        private static byte[] lastSale;
        /// <summary>
        /// mensaje para obtener la ultima venta
        /// </summary>
        public static byte[] LastSale
        {
            get {
                byte[] current = new byte[9];
                current[0] = (byte)HexCodes.STX;    //<STX>
                current[1] = 0x30;                  // 0
                current[2] = 0x32;                  // 2
                current[3] = 0x35;                  // 5
                current[4] = 0x30;                  // 0
                current[5] = (byte)HexCodes.PIPE;   // |
                current[6] = 0x31;                  // 1
                current[7] = (byte)HexCodes.ETX;    //<ETX>
                current[8] = 0x49;                  //<LRC>
                Message.lastSale = current;
                return Message.lastSale; 
            }
        }


        private static byte[] close;
        /// <summary>
        /// mensaje de cierre 
        /// </summary>
        public static byte[] Close
        {
            get {
                byte[] current = new byte[9];
                current[0] = (byte)HexCodes.STX;    //<STX>
                current[1] = 0x30;                  // 0
                current[2] = 0x35;                  // 5
                current[3] = 0x30;                  // 0
                current[4] = 0x30;                  // 0
                current[5] = (byte)HexCodes.PIPE;   // |
                current[6] = 0x31;                  // 1
                current[7] = (byte)HexCodes.ETX;    //<ETX>
                current[8] = Utils.CalculateLRC(current, 0); // 0x4A;                  //<LRC>
                Message.close = current;
                return Message.close; 
            }

        }

    }
}
