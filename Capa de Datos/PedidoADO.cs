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
    public class PedidoADO : ADO
    {
        // Leo todos los pedidos de la BD
        public List<Pedido> LeerPedidos()
        {
            List<Pedido> listaPedidos = new List<Pedido>();
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/pedidos").Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;

                    listaPedidos = JsonConvert.DeserializeObject<List<Pedido>>(aux);
                }
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }

            return listaPedidos;
        }
        public List<Pedido> LeerPedido(int codigoPedido)
        {
            List<Pedido> listaPedido = new List<Pedido>();
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/pedidos/" + codigoPedido).Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;

                    listaPedido = JsonConvert.DeserializeObject<List<Pedido>>(aux);
                }
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }

            return listaPedido;
        }

        // Creo un nuevo pedido en la BD
        public bool InsertarPedido(int? codigoPedido, DateTime? fechaPedido, DateTime? fechaEsperada, DateTime? fechaEntrega,
            string estado, string comentarios, int? codigoCliente)
        {
            try
            {
                Pedido pedido = new Pedido(codigoPedido, fechaPedido, fechaEsperada, fechaEntrega,estado, comentarios, codigoCliente);

                var response = client.PostAsync("api/pedidos", new StringContent(JsonConvert.SerializeObject(pedido),
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
        public Pedido ActualizarPedido(Pedido pedido)
        {
            try
            {

                var response = client.PutAsync("api/pedidos/" + pedido.Codigo_Pedido, new StringContent(JsonConvert.SerializeObject(pedido),
                        Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Pedido>(response.Content.ReadAsStringAsync().Result);
                else
                    return null;
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }
        }

        public bool BorrarPedido(int codigoPedido)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync("api/pedidos/" + codigoPedido).Result;

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
