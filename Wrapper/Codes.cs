using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wrapper
{
    /// <summary>
    /// Clase base donde se generara los codigos TBK 
    /// </summary>
    public class Codes
    {

        string code;
        /// <summary>
        /// codigo
        /// </summary>
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        
        string descripcion;
        /// <summary>
        /// descripcion del codigo
        /// </summary>
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
    }

    /// <summary>
    /// codigos de estado de una transaccion 
    /// </summary>
    public class TransactionCode
    {
        private List<Codes> lst = new List<Codes>();
        /// <summary>
        /// genera la lista 
        /// </summary>
        public TransactionCode()
        {
            lst.Add(new Codes { Code = "00", Descripcion = "Aprobado" });
            lst.Add(new Codes { Code = "01", Descripcion = "Rechazado" });
            lst.Add(new Codes { Code = "02", Descripcion = "Autorizador no Responde" });
            lst.Add(new Codes { Code = "03", Descripcion = "Conexión Fallo" });
            lst.Add(new Codes { Code = "04", Descripcion = "Transacción ya Fue Anulada" });
            lst.Add(new Codes { Code = "05", Descripcion = "No existe Transacción para Anular" });
            lst.Add(new Codes { Code = "06", Descripcion = "Tarjeta no Soportada" });
            lst.Add(new Codes { Code = "07", Descripcion = "Transacción Cancelada" });
            lst.Add(new Codes { Code = "08", Descripcion = "No puede Anular Transacción Debito" });
            lst.Add(new Codes { Code = "09", Descripcion = "Error Lectura Tarjeta" });
            lst.Add(new Codes { Code = "10", Descripcion = "Monto menor al mínimo permitido" });
            lst.Add(new Codes { Code = "11", Descripcion = "No existe venta" });
            lst.Add(new Codes { Code = "12", Descripcion = "Transacción No Soportada" });
            lst.Add(new Codes { Code = "13", Descripcion = "Debe ejecutar cierre" });
            lst.Add(new Codes { Code = "14", Descripcion = "Error Encriptando PAN (BCYCLE)" });
            lst.Add(new Codes { Code = "15", Descripcion = "Error Operando con Debito (BCYCLE)" });
            lst.Add(new Codes { Code = "80", Descripcion = "Solicitando Conformar Monto" });
            lst.Add(new Codes { Code = "81", Descripcion = "Solicitando Ingreso de Clave" });
            lst.Add(new Codes { Code = "82", Descripcion = "Enviando transacción al Autorizador" });
            lst.Add(new Codes { Code = "83", Descripcion = "Selección menú crédito/redcompra" });
            lst.Add(new Codes { Code = "84", Descripcion = "Opere tarjeta" });
            lst.Add(new Codes { Code = "85", Descripcion = "Selección de cuotas" });
            lst.Add(new Codes { Code = "86", Descripcion = "Ingreso de cuotas" });
            lst.Add(new Codes { Code = "87", Descripcion = "Confirmación de cuotas" });
            lst.Add(new Codes { Code = "88", Descripcion = "Aceptar consulta cuotas" });
            lst.Add(new Codes { Code = "89", Descripcion = "Opción mes de gracia" });
            lst.Add(new Codes { Code = "90", Descripcion = "Inicialización Exitosa" });
            lst.Add(new Codes { Code = "91", Descripcion = "Inicialización Fallida" });
            lst.Add(new Codes { Code = "92", Descripcion = "Lector no Conectado" });
            lst.Add(new Codes { Code = "93", Descripcion = "Consultando cuota al Autorizador" });
        }

        /// <summary>
        /// obtiene la descripcion de un codigo
        /// </summary>
        /// <param name="id">codigo</param>
        /// <returns>descripcion</returns>
        public string GetCodeById(string id)
        {
            Codes code = lst.Where(x => x.Code == id).FirstOrDefault();
            string description = "No existe el código";
            if(code != null){
                description = code.Descripcion;
            }
            return description;
        }
        /// <summary>
        /// obtiene la lista comppleta
        /// </summary>
        /// <returns>lista</returns>
        public List<Codes> GetCodes()
        {
            return lst;
        }
    }

    /// <summary>
    /// codigos de las tarjetas que acepta el POS
    /// </summary>
    public class CardCode
    {
        private List<Codes> lst = new List<Codes>();
        /// <summary>
        /// genera la lista 
        /// </summary>
        public CardCode()
        {
            lst.Add(new Codes { Code = "VI", Descripcion = "VISA" });
            lst.Add(new Codes { Code = "MC", Descripcion = "MASTERCARD" });
            lst.Add(new Codes { Code = "AX", Descripcion = "AMEX" });
            lst.Add(new Codes { Code = "DC", Descripcion = "DINERS" });
            lst.Add(new Codes { Code = "MG", Descripcion = "MAGNA" });
            lst.Add(new Codes { Code = "DB", Descripcion = "DEBITO (REDCOMPRA)" });
        }
        /// <summary>
        /// obtiene la descripcion de un codigo
        /// </summary>
        /// <param name="id">codigo</param>
        /// <returns>descripcion</returns>
        public string GetCodeById(string id)
        {
            Codes code = lst.Where(x => x.Code == id).FirstOrDefault();
            string description = "No existe el código";
            if (code != null)
            {
                description = code.Descripcion;
            }
            return description;
        }
        /// <summary>
        /// obtiene la lista comppleta
        /// </summary>
        /// <returns>lista</returns>
        public List<Codes> GetCodes()
        {
            return lst;
        }
    }

    /// <summary>
    /// Como fue procesado el pago
    /// </summary>
    public class ProcessingCardCode
    {
        private List<Codes> lst = new List<Codes>();
        /// <summary>
        /// genera la lista 
        /// </summary>
        public ProcessingCardCode()
        {
            lst.Add(new Codes { Code = "F", Descripcion = "FALLBACK" });
            lst.Add(new Codes { Code = "B", Descripcion = "BANDA" });
            lst.Add(new Codes { Code = "E", Descripcion = "CHIP" });
            lst.Add(new Codes { Code = "C", Descripcion = "CTLS (SIN CONTACTO) " });
        }
        /// <summary>
        /// obtiene la descripcion de un codigo
        /// </summary>
        /// <param name="id">codigo</param>
        /// <returns>descripcion</returns>
        public string GetCodeById(string id)
        {
            Codes code = lst.Where(x => x.Code == id).FirstOrDefault();
            string description = "No existe el código";
            if (code != null)
            {
                description = code.Descripcion;
            }
            return description;
        }
        /// <summary>
        /// obtiene la lista comppleta
        /// </summary>
        /// <returns>lista</returns>
        public List<Codes> GetCodes()
        {
            return lst;
        }
    }
}
