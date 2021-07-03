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
    public class Gama_ProductoADO : ADO
    {
        // Leo todos las gama de productos de la BD
        public List<Gama_Producto> LeerGamaProductos()
        {
            List<Gama_Producto> listaGamaProductos = new List<Gama_Producto>();
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/gama_producto").Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;

                    listaGamaProductos = JsonConvert.DeserializeObject<List<Gama_Producto>>(aux);
                }
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }

            return listaGamaProductos;
        }
        public List<Gama_Producto> LeerGamaProducto(string gama)
        {
            List<Gama_Producto> listaGamaProductos = new List<Gama_Producto>();
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/gama_producto/" + gama).Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;

                    listaGamaProductos = JsonConvert.DeserializeObject<List<Gama_Producto>>(aux);
                }
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }

            return listaGamaProductos;
        }

        // Creo una nueva gama de producto en la BD
        public bool InsertarGamaProducto (string gama, string descripcionTexto, string descripcionHtml, string imagen)
        {
            try
            {
                Gama_Producto gamaProducto = new Gama_Producto(gama, descripcionTexto, descripcionHtml, imagen);

                var response = client.PostAsync("api/gama_producto", new StringContent(JsonConvert.SerializeObject(gamaProducto),
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
        public Gama_Producto ActualizarGamaProducto (Gama_Producto gamaProducto)
        {
            try
            {

                var response = client.PutAsync("api/gama_producto/" + gamaProducto.Gama, new StringContent(JsonConvert.SerializeObject(gamaProducto),
                        Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Gama_Producto>(response.Content.ReadAsStringAsync().Result);
                else
                    return null;
            }
            catch (Exception e)
            {
                throw new ExternalException ("Error:" + e);
            }
        }

        public bool BorrarGamaProducto (string gama)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync("api/gama_producto/" + gama).Result;

                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw new ExternalException ("Error:" + e);
            }
        }
    }
}
