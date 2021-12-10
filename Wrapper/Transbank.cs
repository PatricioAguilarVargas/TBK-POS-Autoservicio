using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wrapper
{
    /// <summary>
    /// Clase que se comunica con el POS TBK por medio de SerialPort
    /// </summary>
    public static class Transbank
    {
        /// <summary>
        /// puerto COM del POS
        /// </summary>
        public static string _portName = "COM3";
        /// <summary>
        /// velocidad de la comunicacion
        /// </summary>
        public static int _baudRate = 115200;
        /// <summary>
        /// cantidad de bytes de la cmunicacion
        /// </summary>
        public static int _dataBits = 8;
        /// <summary>
        /// numero deintentos base
        /// </summary>
        public static int _tries = 3;
        /// <summary>
        /// ciclos de espera para respuesta del POS
        /// </summary>
        public static int _cicle = 200;

        /// <summary>
        /// Proceso para realizar el polling de coneccion.
        /// 
        /// </summary>
        /// <returns></returns>
        public static Base Polling()
        {
            //mensaje de respusta
            Base br;
            //configuracion del SerialPort
            SerialPort sp = new SerialPort(_portName, _baudRate, Parity.None, _dataBits, StopBits.One);
            sp.Handshake = Handshake.None;
            try
            {
                //seteo de intentos a 0
                int write = 0;
                //si el puerto esta abierto
                if (!(sp.IsOpen))
                {
                    //abrir el puerto COM
                    sp.Open();
                    //obtener el mensaje (array de bytes)
                    byte[] polling = Message.Polling;
                    
                    do
                    {
                        //enviar el mensaje
                        sp.Write(polling, 0, polling.Length);
                        //esperar 0.5 seg
                        Thread.Sleep(500);
                        //se serial port trae respuesta
                        if (sp.BytesToRead > 0)
                        {
                            //se genera un array con el largo de bytes de respuesta
                            byte[] buffer = new byte[sp.BytesToRead];
                            //Se carga el nuevo array
                            sp.Read(buffer, 0, sp.BytesToRead);
                            //se varifica si la respusta es ACK 
                            if (buffer[0] == 6)
                            {
                                //se carga el mensaje base
                                br.responseCode = "OK";
                                br.response = Encoding.Default.GetString(buffer);
                                return br;
                               
                            }
                        }
                        //se realiza otro intento
                        write++;
                    } while (write < _tries);

                }
                else
                {
                    //puerto cerado
                    br.responseCode = "NOK";
                    br.response = "EL puerto " + _portName + " se encuentra abierto";
                    return br;
                }
                //fallaron los intentos
                br.responseCode = "NOK";
                br.response = "Polling fallo";
                return br;
            }
            catch (Exception ex)
            {
                //error en algun momento de la ejecucion 
                br.responseCode = "NOK";
                br.response =  "Error opening/writing to serial port " + _portName + " ::" + ex.Message + " Error!";
                return br;
            }
            finally
            {
                //cierre del puerto
                if (sp.IsOpen)
                {
                    sp.Close();
                    sp.Dispose();
                }
            }

        }
        /// <summary>
        /// Proceso para realizar la carga de llaves
        /// </summary>
        /// <returns></returns>
        public static Base LoadKeys()
        {
            //mensaje de respusta
            Base br;
            //configuracion del SerialPort
            SerialPort sp = new SerialPort(_portName, _baudRate, Parity.None, _dataBits, StopBits.One);
            sp.Handshake = Handshake.None;
            try
            {
                //si el mensaje fue enviado
                bool siEnviado = false;
                //seteo de intentos a 0
                int write = 0;
                //si el puerto esta abierto
                if (!(sp.IsOpen))
                {
                    //abrir el puerto COM
                    sp.Open();
                    //obtener el mensaje (array de bytes)
                    byte[] loadKeys = Message.LoadKeys;
                    
                    do
                    {
                        //enviar el mensaje
                        sp.Write(loadKeys, 0, loadKeys.Length);
                        //esperar 0.5 seg
                        Thread.Sleep(500);
                        //se serial port trae respuesta
                        if (sp.BytesToRead > 0)
                        {
                            //se genera un array con el largo de bytes de respuesta
                            byte[] buffer = new byte[sp.BytesToRead];
                            //Se carga el nuevo array
                            sp.Read(buffer, 0, sp.BytesToRead);
                            //se varifica si la respusta es ACK 
                            if (buffer[0] == 6)
                            {
                                //se informa qeu fue enviado
                                siEnviado = true;
                                break;
                            }
                        }
                        //se realiza otro intento
                        write++;
                    } while (write < _tries);

                    //si el mensaje fue enviado
                    if (siEnviado == true)
                    {
                        //seteo de intentos a 0
                        write = 0;
                        do
                        {
                            //se espera 0.5 seg
                            Thread.Sleep(500);
                            //se serial port trae respuesta
                            if (sp.BytesToRead > 0)
                            {
                                //se genera un array con el largo de bytes 
                                byte[] buffer = new byte[sp.BytesToRead];
                                //Se carga el nuevo array
                                sp.Read(buffer, 0, sp.BytesToRead);
                                //se calcula el LRC
                                byte LRC = Utils.CalculateLRC(buffer, 1);
                                //se trae el LRC del mensaje
                                byte LRCb = buffer[buffer.Length - 1];
                                //se espera 0.5 seg
                                Thread.Sleep(500);
                                //se verifica que sean iguales
                                if (LRC == LRCb)
                                {
                                    //se informa que esta ACK al POS
                                    byte[] ACK = { (byte)HexCodes.ACK };
                                    sp.Write(ACK, 0, ACK.Length);
                                    br.responseCode = "OK";
                                    br.response = Encoding.Default.GetString(buffer);
                                    return br;
                                }
                                else
                                {
                                    //se informa que esta ACK al POS
                                    byte[] ACK = { (byte)HexCodes.ACK };
                                    sp.Write(ACK, 0, ACK.Length);
                                    br.responseCode = "OK";
                                    br.response = Encoding.Default.GetString(buffer);
                                    return br;
                                }
                           
                            }
                            //se realiza otro intento
                            write++;
                        } while (write < _cicle);
                    }
                }
                else
                {
                    //puerto cerado
                    br.responseCode = "NOK";
                    br.response = "EL puerto " + _portName + " se encuentra abierto";
                    return br;
                }
                //fallaron los intentos
                br.responseCode = "NOK";
                br.response = "Carga de llaves fallo";
                return br;
            }
            catch (Exception ex)
            {
                //error en algun momento de la ejecucion 
                br.responseCode = "NOK";
                br.response = "Error opening/writing to serial port " + _portName + " ::" + ex.Message + " Error!";
                return br;
            }
            finally
            {
                //cierre del puerto
                if (sp.IsOpen)
                {
                    sp.Close();
                    sp.Dispose();
                }
            }

        }

        /// <summary>
        /// Proceso que genera una venta en el POS
        /// </summary>
        /// <param name="amount">Monto</param>
        /// <param name="ticket">Numero de boleta</param>
        /// <returns></returns>
        public static Base Sale(decimal amount, string ticket)
        {
            //mensaje de respusta
            Base br;
            //configuracion del SerialPort
            SerialPort sp = new SerialPort(_portName, _baudRate, Parity.None, _dataBits, StopBits.One);
            sp.Handshake = Handshake.None;
            try
            {
                //si el mensaje fue enviado
                bool siEnviado = false;
                //seteo de intentos a 0
                int write = 0;
                //si el puerto esta abierto
                if (!(sp.IsOpen))
                {
                    //se prepara el mensaje
                    byte[] sale = Utils.PrepareSaleMessage(amount, ticket);//Message.LoadKeys;
                    //abrir el puerto COM
                    sp.Open();
                 
                    do
                    {
                        //enviar el mensaje
                        sp.Write(sale, 0, sale.Length);
                        //se espera 0.5 seg
                        Thread.Sleep(500);
                        //se serial port trae respuesta
                        if (sp.BytesToRead > 0)
                        {
                            //se genera un array con el largo de bytes de respuesta
                            byte[] buffer = new byte[sp.BytesToRead];
                            //Se carga el nuevo array
                            sp.Read(buffer, 0, sp.BytesToRead);
                            //se varifica si la respusta es ACK 
                            if (buffer[0] == 6)
                            {
                                //se informa qeu fue enviado
                                siEnviado = true;
                                break;
                            }
                        }
                        //otro intento
                        write++;
                    } while (write < _tries);

                    //si el mensaje fue enviado
                    if (siEnviado == true)
                    {
                        //seteo de intentos a 0
                        write = 0;
                        do
                        {
                            //se espera 0.5 seg
                            Thread.Sleep(500);
                            //se serial port trae respuesta
                            if (sp.BytesToRead > 0)
                            {
                                //se genera un array con el largo de bytes 
                                byte[] buffer = new byte[sp.BytesToRead];
                                //Se carga el nuevo array
                                sp.Read(buffer, 0, sp.BytesToRead);
                                //se calcula el LRC
                                byte LRC = Utils.CalculateLRC(buffer, 1);
                                //se trae el LRC del mensaje
                                byte LRCb = buffer[buffer.Length - 1];
                                //se espera 0.5 seg
                                Thread.Sleep(500);
                                //se verifica que sean iguales
                                if (LRC == LRCb)
                                {
                                    //se informa que esta ACK al POS
                                    byte[] ACK = { (byte)HexCodes.ACK };
                                    sp.Write(ACK, 0, ACK.Length);
                                    br.responseCode = "OK";
                                    br.response = Encoding.Default.GetString(buffer);
                                    return br;
                                }
                                else
                                {
                                    //se informa que esta ACK al POS
                                    byte[] ACK = { (byte)HexCodes.ACK };
                                    sp.Write(ACK, 0, ACK.Length);
                                    br.responseCode = "OK";
                                    br.response = Encoding.Default.GetString(buffer);
                                    return br;
                                }
                     
                            }
                            //otro intento
                            write++;
                        } while (write < _cicle);
                    }
                }
                else
                {
                    //puerto cerado
                    br.responseCode = "NOK";
                    br.response = "EL puerto " + _portName + " se encuentra abierto";
                    return br;
                }
                //fallaron los intentos
                br.responseCode = "NOK";
                br.response = "Venta Fallo";
                return br;
            }
            catch (Exception ex)
            {
                //error en algun momento de la ejecucion 
                br.responseCode = "NOK";
                br.response = "Error opening/writing to serial port " + _portName + " ::" + ex.Message + " Error!";
                return br;
            }
            finally
            {
                //cierre del puerto
                if (sp.IsOpen)
                {
                    sp.Close();
                    sp.Dispose();
                }
            }

        }

        /// <summary>
        /// Obtiene la ultima venta registrada en el POS
        /// </summary>
        /// <returns></returns>
        public static Base LastSale()
        {
            Base br;
            SerialPort sp = new SerialPort(_portName, _baudRate, Parity.None, _dataBits, StopBits.One);
            sp.Handshake = Handshake.None;
            try
            {
                bool siEnviado = false;
                int write = 0;
                if (!(sp.IsOpen))
                {
                    byte[] lastSale = Message.LastSale;
                    sp.Open();

                    do
                    {
                        sp.Write(lastSale, 0, lastSale.Length);
                        Thread.Sleep(500);
                        if (sp.BytesToRead > 0)
                        {
                            byte[] buffer = new byte[sp.BytesToRead];
                            sp.Read(buffer, 0, sp.BytesToRead);
                            if (buffer[0] == 6)
                            {
                                siEnviado = true;
                                break;
                            }
                        }
                        write++;
                    } while (write < _tries);

                    if (siEnviado == true)
                    {
                        write = 0;
                        do
                        {
                            Thread.Sleep(500);
                            if (sp.BytesToRead > 0)
                            {
                                byte[] buffer = new byte[sp.BytesToRead];
                                sp.Read(buffer, 0, sp.BytesToRead);

                                byte LRC = Utils.CalculateLRC(buffer, 1);
                                byte LRCb = buffer[buffer.Length - 1];
                                Thread.Sleep(500);
                                if (LRC == LRCb)
                                {
                                    byte[] ACK = { (byte)HexCodes.ACK };
                                    sp.Write(ACK, 0, ACK.Length);
                                    br.responseCode = "OK";
                                    br.response = Encoding.Default.GetString(buffer);
                                    return br;
                                }
                                else
                                {
                                    byte[] ACK = { (byte)HexCodes.ACK };
                                    sp.Write(ACK, 0, ACK.Length);
                                    br.responseCode = "OK";
                                    br.response = Encoding.Default.GetString(buffer);
                                    return br;
                                }
                              
                            }

                            write++;
                        } while (write < _cicle);
                    }

                }
                else
                {
                    br.responseCode = "NOK";
                    br.response = "EL puerto " + _portName + " se encuentra abierto";
                    return br;
                }
                br.responseCode = "NOK";
                br.response = "Ultima Venta Fallo";
                return br;
            }
            catch (Exception ex)
            {
                br.responseCode = "NOK";
                br.response = "Error opening/writing to serial port " + _portName + " ::" + ex.Message + " Error!";
                return br;
            }
            finally
            {
                if (sp.IsOpen)
                {
                    sp.Close();
                    sp.Dispose();
                }
            }

        }

        /// <summary>
        /// Envia un mensaje de cierre del dia
        /// </summary>
        /// <returns></returns>
        public static Base Close()
        {
            Base br;
            SerialPort sp = new SerialPort(_portName, _baudRate, Parity.None, _dataBits, StopBits.One);
            sp.Handshake = Handshake.None;
            try
            {
                bool siEnviado = false;
                int write = 0;
                if (!(sp.IsOpen))
                {
                    byte[] close = Message.Close;
                    sp.Open();

                    do
                    {
                        sp.Write(close, 0, close.Length);
                        Thread.Sleep(500);
                        if (sp.BytesToRead > 0)
                        {
                            byte[] buffer = new byte[sp.BytesToRead];
                            sp.Read(buffer, 0, sp.BytesToRead);
                            if (buffer[0] == 6)
                            {
                                siEnviado = true;
                                break;
                            }
                        }
                        write++;
                    } while (write < _tries);

                    if (siEnviado == true)
                    {
                        write = 0;
                        do
                        {
                            Thread.Sleep(500);
                            if (sp.BytesToRead > 0)
                            {
                                byte[] buffer = new byte[sp.BytesToRead];
                                sp.Read(buffer, 0, sp.BytesToRead);

                                byte LRC = Utils.CalculateLRC(buffer, 1);
                                byte LRCb = buffer[buffer.Length - 1];
                                Thread.Sleep(500);
                                if (LRC == LRCb)
                                {
                                    byte[] ACK = { (byte)HexCodes.ACK };
                                    sp.Write(ACK, 0, ACK.Length);
                                    br.responseCode = "OK";
                                    br.response = Encoding.Default.GetString(buffer);
                                    return br;
                                }
                                else
                                {
                                    byte[] ACK = { (byte)HexCodes.ACK };
                                    sp.Write(ACK, 0, ACK.Length);
                                    br.responseCode = "OK";
                                    br.response = Encoding.Default.GetString(buffer);
                                    return br;
                                }
                              
                            }

                            write++;
                        } while (write < _cicle);
                    }

                }
                else
                {
                    br.responseCode = "NOK";
                    br.response = "EL puerto " + _portName + " se encuentra abierto";
                    return br;
                }
                br.responseCode = "NOK";
                br.response = "Cierre Fallo";
                return br;
            }
            catch (Exception ex)
            {
                br.responseCode = "NOK";
                br.response = "Error opening/writing to serial port " + _portName + " ::" + ex.Message + " Error!";
                return br;
            }
            finally
            {
                if (sp.IsOpen)
                {
                    sp.Close();
                    sp.Dispose();
                }
            }
        }

        /// <summary>
        /// obtiene la descripcion del tipo de proceso para el pago
        /// </summary>
        /// <param name="id">codigo</param>
        /// <returns></returns>
        public static string GetProcessingCardCode(string id)
        {
            ProcessingCardCode pcc = new ProcessingCardCode();
            return pcc.GetCodeById(id);
        }
        /// <summary>
        /// obtiene la descripcion del tipo de tarjeta
        /// </summary>
        /// <param name="id">codigo</param>
        /// <returns></returns>
        public static string GetCardCode(string id)
        {
            CardCode cc = new CardCode();
            return cc.GetCodeById(id);
        }

        /// <summary>
        /// obtiene la descripcion de la transaccion de respuesta del POS
        /// </summary>
        /// <param name="id">codigo</param>
        /// <returns></returns>
        public static string GetTransactionCode(string id)
        {
            TransactionCode tc = new TransactionCode();
            return tc.GetCodeById(id);
        }
    }
}
