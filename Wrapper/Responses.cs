using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wrapper
{
    /// <summary>
    /// formateo base del mensaje de respuesta del POS
    /// </summary>
    public struct Base
    {
        public string responseCode;
        public string response;
    }

    /// <summary>
    /// formateo del mensaje respuesta (Base.response) del POS para la venta
    /// </summary>
    public struct SaleResponse
    {
        public string comando;
        public string codigoRespuesta;
        public string codigoComercio;
        public string idTerminal;
        public string boleta;
        public string codigoAutorizacion;
        public string monto;
        public string digitosTarjeta;
        public string numeroOperacionTerminal;
        public string tipoTarjeta;
        public string fechaContable;
        public string numeroCuenta;
        public string abreviaTarjeta;
        public string fechaTransaccion;
        public string horaTransaccion;
        public string campoImpresion;
        public string tipoCuota;
        public string numeroCuota;
        public string montoCuota;
        public string glosaTipoCuota;
    }

    /// <summary>
    /// formateo del mensaje respuesta (Base.response) del POS para el cierre
    /// </summary>
    public struct CloseResponse
    {
        public string comando;
        public string codigoRespuesta;
        public string codigoComercio;
        public string idTerminal;
        public string campoImpresion;
    }

    /// <summary>
    /// formateo del mensaje respuesta (Base.response) para todos
    /// </summary>
    public struct BaseResponse
    {
        public string comando;
        public string codigoRespuesta;
    }
}
