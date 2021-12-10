using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;  

namespace Wrapper
{
    /// <summary>
    /// Clase Utilitaria para el manejo de POS
    /// </summary>
    public static class Utils
    {
       
        /// <summary>
        /// Prepara el mensaje de venta para ser enviado al POS
        /// </summary>
        /// <param name="amount">monto</param>
        /// <param name="ticket">Numero de boleta</param>
        /// <returns>array de bytes</returns>
        public static byte[] PrepareSaleMessage(decimal amount, string ticket)
        {

            byte[] amountBytes = Encoding.ASCII.GetBytes(amount.ToString());
            byte[] ticketBytes = Encoding.ASCII.GetBytes(ticket.ToString());

            byte[] current = new byte[0];
            //current[0] = (byte)HexCodes.STX;
            current = AddByteToArray(current, (byte)HexCodes.STX);
            current = AddByteToArray(current, 0x30);
            current = AddByteToArray(current, 0x32);
            current = AddByteToArray(current, 0x30);
            current = AddByteToArray(current, 0x30);
            current = AddByteToArray(current, (byte) HexCodes.PIPE);
            foreach (byte c in amountBytes)
            {
                current = AddByteToArray(current, c);
            }
            current = AddByteToArray(current, (byte)HexCodes.PIPE);
            foreach (byte c in ticketBytes)
            {
                current = AddByteToArray(current, c);
            }
            current = AddByteToArray(current, (byte)HexCodes.PIPE);
            current = AddByteToArray(current, 0x31);
            current = AddByteToArray(current, (byte)HexCodes.PIPE);
            current = AddByteToArray(current, 0x30);
            current = AddByteToArray(current, (byte)HexCodes.ETX);
            current = AddByteToArray(current, 0x40);
            current[current.Length - 1] = CalculateLRC(current);
            return current;
        }

        /// <summary>
        /// calcula el LRC de un mensaje 
        /// </summary>
        /// <param name="bytes">array de bytes</param>
        /// <param name="op">si empieza a leer desde la pocision 0 o 1</param>
        /// <returns>byte</returns>
        public static byte CalculateLRC(byte[] bytes, int op = 0)
        {
            byte LRC = 0;
            if (op == 0)
            {
                for (int i = 1; i < bytes.Length - 1; i++)
                {
                    LRC ^= bytes[i];
                }
            }

            if (op == 1)
            {
                for (int i = 0; i < bytes.Length - 1; i++)
                {
                    LRC ^= bytes[i];
                }
            }

            return LRC;
        }

        /// <summary>
        /// agrega un byte, en ultima pocision, a un array de bytes
        /// </summary>
        /// <param name="bArray">array de bytes</param>
        /// <param name="newByte">nuevo bytes</param>
        /// <returns>array de byts</returns>
        public static byte[] AddByteToArray(byte[] bArray, byte newByte)
        {
            byte[] newArray = new byte[bArray.Length + 1];
            bArray.CopyTo(newArray, 0);
            newArray[bArray.Length] = newByte;
            return newArray;
        }

        /// <summary>
        /// formatea el mensaje base en el formato de una venta
        /// </summary>
        /// <param name="sale">Mensaje de respusta del POS</param>
        /// <returns>SaleResponse</returns>
        public static SaleResponse GetSale(Base sale)
        {
            SaleResponse sr;
            string response = sale.response;
            response = response.Substring(1, response.Length - 3);
            string[] lst = response.Split('|');
            
            sr.comando = lst[0];
            sr.codigoRespuesta = lst[1];
            sr.codigoComercio = lst[2];
            sr.idTerminal = lst[3];
            sr.boleta = lst[4];
            sr.codigoAutorizacion = lst[5];
            sr.monto = lst[6];
            sr.digitosTarjeta = lst[7];
            sr.numeroOperacionTerminal = lst[8];
            sr.tipoTarjeta = lst[9];
            sr.fechaContable = lst[10];
            sr.numeroCuenta = lst[11];
            sr.abreviaTarjeta = lst[12];
            sr.fechaTransaccion = lst[13];
            sr.horaTransaccion = lst[14];
            sr.campoImpresion = lst[15];
            if (sr.tipoTarjeta != "DB")
            {
                sr.tipoCuota = lst[16];
                sr.numeroCuota = lst[17];
                sr.montoCuota = lst[18];
                sr.glosaTipoCuota = lst[19];
            }
            else
            {
                sr.tipoCuota = "";
                sr.numeroCuota = "";
                sr.montoCuota = "";
                sr.glosaTipoCuota = "";
            }
           
            return sr;
        }

        /// <summary>
        /// formatea el mensaje base en el formato de un cierre
        /// </summary>
        /// <param name="close">Mensaje de respusta del POS</param>
        /// <returns>CloseResponse</returns>
        public static CloseResponse GetClose(Base close)
        {
            CloseResponse cr;
            string response = close.response;
            response = response.Substring(1, response.Length - 3);
            string[] lst = response.Split('|');
            
            cr.comando = lst[0];
            cr.codigoRespuesta = lst[1];
            cr.codigoComercio = lst[2];
            cr.idTerminal = lst[3];
            cr.campoImpresion = lst[4];
            return cr;
        }

        /// <summary>
        /// formatea el mensaje base en el formato de BaseResponse
        /// </summary>
        /// <param name="msg">Mensaje de respusta del POS</param>
        /// <returns>BaseResponse</returns>
        public static BaseResponse GetBase(Base msg)
        {
            BaseResponse lkr;
            string response = msg.response;
            response = response.Substring(1, response.Length - 3);
            string[] lst = response.Split('|');
            
            lkr.comando = lst[0];
            lkr.codigoRespuesta = lst[1];
            return lkr;
        }

        /// <summary>
        /// imprime por consola un mensaje
        /// </summary>
        /// <param name="sale"></param>
        public static void PrintStruct(Object sale)
        {
             foreach (var prop in sale.GetType().GetFields())
            {
                Console.WriteLine(prop.Name + ": " + prop.GetValue(sale));
            }
        }
    }
}
