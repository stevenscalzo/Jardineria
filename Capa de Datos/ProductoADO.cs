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
    public class ProductoADO : ADO
    {
        // Leo todos los productos de la BD
        public List<Producto> LeerProductos()
        {
            List<Producto> listaProductos = new List<Producto>();
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/productos").Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;

                    listaProductos = JsonConvert.DeserializeObject<List<Producto>>(aux);
                }
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }

            return listaProductos;
        }
        public List<Producto> LeerProducto(string codigo)
        {
            List<Producto> listaProducto = new List<Producto>();
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/productos/" + codigo).Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;

                    listaProducto = JsonConvert.DeserializeObject<List<Producto>>(aux);
                }
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }

            return listaProducto;
        }

        // Creo un nuevo producto en la BD
        public bool InsertarProducto(string codigoProducto, string nombre, string gama, string dimensiones,
            string proveedor, string descripcion, short stock, decimal precioVenta,
            decimal precioProveedor)
        {
            try
            {
                Producto producto = new Producto(codigoProducto, nombre, gama, dimensiones, proveedor, descripcion, 
                    stock, precioVenta, precioProveedor);

                var response = client.PostAsync("api/productos", new StringContent(JsonConvert.SerializeObject(producto),
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
        public Producto ActualizarProducto(Producto producto)
        {
            try
            {
                var response = client.PutAsync("api/productos/" + producto.Codigo_Producto, new StringContent(JsonConvert.SerializeObject(producto),
                        Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Producto>(response.Content.ReadAsStringAsync().Result);
                else
                    return null;
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }
        }

        public bool BorrarProducto(string codigo)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync("api/productos/" + codigo).Result;

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
