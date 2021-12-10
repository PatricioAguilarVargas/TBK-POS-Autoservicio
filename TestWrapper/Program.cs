using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wrapper;

namespace TestWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            Transbank._portName = "COM4";
            Transbank._cicle = 200;
            Console.WriteLine("*********************************************");
            /*
            Console.WriteLine("LOAD KEYS");
            Console.WriteLine(Transbank.LoadKeys().response);
            Thread.Sleep(5000); 
            */
            /*
            Console.WriteLine("POLLING");
            Console.WriteLine(Transbank.Polling().response);
            Thread.Sleep(5000); 
            */
            
            Console.WriteLine("SALE");
            Base sale =Transbank.Sale(2500,"123456");
            //Console.WriteLine(sale.response);
            if (sale.responseCode == "OK")
            {
                BaseResponse b = Utils.GetBase(sale);
                if (b.codigoRespuesta == "00")
                {
                    SaleResponse saleRes = Utils.GetSale(sale);
                    Utils.PrintStruct(saleRes);
                }
                else
                {
                    Utils.PrintStruct(b);
                }
            }
            else
            {
                string response = sale.response;
                response = response.Substring(1, response.Length - 3);
                string[] lst = response.Split('|');
                foreach (var d in lst)
                {
                    Console.WriteLine(d);
                }
               
            }
           
            Thread.Sleep(5000); 
            
            /*
            Console.WriteLine("LAST SALE");
            Console.WriteLine(Transbank.LastSale().response);
            Thread.Sleep(5000); 
            */
            /*
            Console.WriteLine("CLOSE");
            Console.WriteLine(Transbank.Close().response);
            */
            Console.WriteLine("*********************************************");
            Console.ReadKey();
        }

    }
}
