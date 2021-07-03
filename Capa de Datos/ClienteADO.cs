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
    /// <summary>
    /// Clase que contiene las conexiones para el tipo cliente
    /// </summary>
    public class ClienteADO : ADO
    {
        /// <summary>
        /// Leer todos los clientes de la DB
        /// </summary>
        /// <returns>Lista de tipo cliente</returns>
        public List <Cliente> LeerClientes()
        {
            List<Cliente> listaClientes = new List<Cliente>();
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/clientes").Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;

                    listaClientes = JsonConvert.DeserializeObject<List<Cliente>>(aux);
                }
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }

            return listaClientes;
        }


        /// <summary>
        /// Lee cliente por id
        /// </summary>
        /// <param name="codigoCliente">Codigo/Id del cliente</param>
        /// <returns>Lista de tipo cliente</returns>
        public List<Cliente> LeerCliente(int codigoCliente)
        {
            List<Cliente> listaClientes = new List<Cliente>();
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/clientes/" + codigoCliente).Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;

                    listaClientes = JsonConvert.DeserializeObject<List<Cliente>>(aux);
                }
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }

            return listaClientes;
        }

        /// <summary>
        /// Creo un nuevo cliente en la BD
        /// </summary>
        /// <param name="codigoCliente">Codigo del cliente</param>
        /// <param name="nombreCliente">Nombre del cliente</param>
        /// <param name="nombreContacto">Nombre del contacto</param>
        /// <param name="apellidoContacto">Apellido del contacto</param>
        /// <param name="telefono">Telefono</param>
        /// <param name="fax">Fax</param>
        /// <param name="lineaDireccion1">Direccion linea 1</param>
        /// <param name="lineaDireccion2">Direccion linea 2</param>
        /// <param name="ciudad">Ciudad</param>
        /// <param name="region">Region</param>
        /// <param name="pais">País</param>
        /// <param name="codigoPostal">Código postal</param>
        /// <param name="codigoEmpleadosRepVentas">Código del representante de ventas</param>
        /// <param name="limiteCredito">Límite de crédito</param>
        /// <returns>Bool indicando si se insertó el cliente o no</returns>
        public bool InsertarCliente(int? codigoCliente, string nombreCliente, string nombreContacto, string apellidoContacto,
            string telefono, string fax, string lineaDireccion1, string lineaDireccion2, string ciudad,
            string region, string pais, string codigoPostal, int codigoEmpleadosRepVentas, decimal limiteCredito)
        {
            try
            {
                Cliente cliente = new Cliente (codigoCliente, nombreCliente, nombreContacto, apellidoContacto, telefono, fax, 
                    lineaDireccion1, lineaDireccion2, ciudad, region, pais, codigoPostal, codigoEmpleadosRepVentas, limiteCredito);

                var response = client.PostAsync("api/clientes", new StringContent(JsonConvert.SerializeObject(cliente),
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

        /// <summary>
        /// Actualiza la informacion de un cliente
        /// </summary>
        /// <param name="cliente">Cliente a actualziar</param>
        /// <returns>El cliente actualizado</returns>
        public Cliente ActualizarCliente(Cliente cliente)
        {
            try
            {

                var response = client.PutAsync("api/clientes/" + cliente.codigo_cliente, new StringContent(JsonConvert.SerializeObject(cliente),
                        Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Cliente>(response.Content.ReadAsStringAsync().Result);
                else
                    return null;
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }
        }

        /// <summary>
        /// Borra al cliente de la DB
        /// </summary>
        /// <param name="codigoCliente">Codigo del cliente</param>
        /// <returns>Bool indicando si se eliminó el cliente o no</returns>
        public bool BorrarCliente (int codigoCliente)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync("api/clientes/" + codigoCliente).Result;

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
