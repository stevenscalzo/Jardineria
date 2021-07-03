using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
// Para contactar con la WEB API
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;

namespace Capa_Datos
{
    public class Detalle_PedidoADO : ADO
    {
        // Leo todos los detalles de pedidos de la BD
        public List<Detalle_Pedido> LeerDetallesPedidos()
        {
            List<Detalle_Pedido> listaDetallesPedidos = new List<Detalle_Pedido>();
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/detalle_pedido").Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;

                    listaDetallesPedidos = JsonConvert.DeserializeObject<List<Detalle_Pedido>>(aux);
                }
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }

            return listaDetallesPedidos;
        }
        public List<Detalle_Pedido> LeerDetallesPedido(int codigoPedido)
        {
            List<Detalle_Pedido> listaDetallesPedido = new List<Detalle_Pedido>();
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/detalle_pedido/" + codigoPedido).Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;

                    listaDetallesPedido = JsonConvert.DeserializeObject<List<Detalle_Pedido>>(aux);
                }
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }

            return listaDetallesPedido;
        }

        // Creo un nuevo detalle de pedido en la BD
        public bool InsertarDetallePedido(int codigoPedido, string codigoProducto, int cantidad, decimal precioUnidad, short numeroLinea)
        {
            try
            {
                Detalle_Pedido detallePedido = new Detalle_Pedido(codigoPedido, codigoProducto, cantidad, precioUnidad, numeroLinea);

                var response = client.PostAsync("api/detalle_pedido", new StringContent(JsonConvert.SerializeObject(detallePedido),
                        Encoding.UTF8, "application/json")).Result;

                // El objeto retornado lo podemos obtener con JsonConvert.DeserializeObject<Pago>(response.Content.ReadAsStringAsync().Result

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
                return false;
            }
        }
        public Detalle_Pedido ActualizaDetallePedido(Detalle_Pedido detallePedido)
        {
            try
            {
                var response = client.PutAsync("api/detalle_pedido/" + detallePedido.codigo_pedido, new StringContent(JsonConvert.SerializeObject(detallePedido),
                        Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Detalle_Pedido>(response.Content.ReadAsStringAsync().Result);
                else
                    return null;
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }
        }

        public bool BorrarDetallePedido(int codigoPedido, int numLinea)
        {
            try
            {
                string api = "api/detalle_pedido/" + codigoPedido + "/" + numLinea;
                HttpResponseMessage response = client.DeleteAsync(api).Result;

                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }
        }
    }
}
