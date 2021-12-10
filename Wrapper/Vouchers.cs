using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wrapper
{
    /// <summary>
    /// Enumeracion de los tipos de voucher de TBK
    /// </summary>
    public enum tipoVoucher { 
        VentaDebito = 1, 
        VentaCreditoSinCuota = 2, 
        VentaCreditoCuotaNormal = 3,
        DuplicadoUltimaVentaCredito = 4,
        VentaCredito2CSI = 5,
        VentaCredito3CSI = 6,
        VentaPagoDiferido = 7,
        ComprobantePromocion = 8,
        VentaPromocion = 9,
        CierreTerminal = 10
    };

    /// <summary>
    /// Clase base de los tipos de voucher
    /// </summary>
    public class VouchersB
    {
        public int tipo;
        public string descripcion;
        public float ancho;
        public float largo;
    }

    /// <summary>
    /// Clase que entrega los tipos de voucher
    /// </summary>
    public class Vouchers
    {
        private List<VouchersB> lst = new List<VouchersB>();

        /// <summary>
        /// Genera la lista de los tipos de voucher
        /// </summary>
        public Vouchers()
        {
            lst.Add(new VouchersB() { tipo = (int)tipoVoucher.VentaDebito, descripcion = tipoVoucher.VentaDebito.ToString(), ancho = 40, largo = 19 });
            lst.Add(new VouchersB() { tipo = (int)tipoVoucher.VentaCreditoSinCuota, descripcion = tipoVoucher.VentaCreditoSinCuota.ToString(), ancho = 40, largo = 19 });
            lst.Add(new VouchersB() { tipo = (int)tipoVoucher.VentaCreditoCuotaNormal, descripcion = tipoVoucher.VentaCreditoCuotaNormal.ToString(), ancho = 40, largo = 23 });
            lst.Add(new VouchersB() { tipo = (int)tipoVoucher.DuplicadoUltimaVentaCredito, descripcion = tipoVoucher.DuplicadoUltimaVentaCredito.ToString(), ancho = 40, largo = 23 });
            lst.Add(new VouchersB() { tipo = (int)tipoVoucher.VentaCredito2CSI, descripcion = tipoVoucher.VentaCredito2CSI.ToString(), ancho = 40, largo = 24 });
            lst.Add(new VouchersB() { tipo = (int)tipoVoucher.VentaCredito3CSI, descripcion = tipoVoucher.VentaCredito3CSI.ToString(), ancho = 40, largo = 24 });
            lst.Add(new VouchersB() { tipo = (int)tipoVoucher.VentaPagoDiferido, descripcion = tipoVoucher.VentaPagoDiferido.ToString(), ancho = 40, largo = 25 });
            lst.Add(new VouchersB() { tipo = (int)tipoVoucher.ComprobantePromocion, descripcion = tipoVoucher.ComprobantePromocion.ToString(), ancho = 40, largo = 36 });
            lst.Add(new VouchersB() { tipo = (int)tipoVoucher.VentaPromocion, descripcion = tipoVoucher.VentaPromocion.ToString(), ancho = 40, largo = 29 });
            lst.Add(new VouchersB() { tipo = (int)tipoVoucher.CierreTerminal, descripcion = tipoVoucher.CierreTerminal.ToString(), ancho = 40, largo = 18 });
        }

        /// <summary>
        /// entrega los datos del voucher segun su tipo
        /// </summary>
        /// <param name="tipo">identificador segun enum tipoVoucher</param>
        /// <returns>VouchersB</returns>
        public VouchersB GetVoucherByTipo(int tipo)
        {
            return lst.Where(x => x.tipo == tipo).FirstOrDefault();
        }

        /// <summary>
        /// ertorna la lista completa de los tipos de voucher TBK
        /// </summary>
        /// <returns></returns>
        public List<VouchersB> GetAll()
        {
            return lst;
        }
    }
}
