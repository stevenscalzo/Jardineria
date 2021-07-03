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
    public class OficinaADO : ADO
    {
        // Leo todos las oficinas de la BD
        public List<Oficina> LeerOficinas()
        {
            List<Oficina> listaOficinas = new List<Oficina>();
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/oficinas").Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;

                    listaOficinas = JsonConvert.DeserializeObject<List<Oficina>>(aux);
                }
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }

            return listaOficinas;
        }
        public List<Oficina> LeerOficina(string codigo)
        {
            List<Oficina> listaOficinas = new List<Oficina>();
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/oficinas/" + codigo).Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;

                    listaOficinas = JsonConvert.DeserializeObject<List<Oficina>>(aux);
                }
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }

            return listaOficinas;
        }

        // Creo un nuevo pago en la BD
        public bool InsertarOficina(string codigoOficina, string ciudad, string pais, string region, 
            string codigoPostal, string telefono, string lineaDireccion1, string lineaDireciion2)
        {
            try
            {
                Oficina oficina = new Oficina(codigoOficina, ciudad, pais, region, codigoPostal, telefono,
                    lineaDireccion1, lineaDireciion2);

                var response = client.PostAsync("api/oficinas", new StringContent(JsonConvert.SerializeObject(oficina),
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
        public Oficina ActualizarOficina(Oficina oficina)
        {
            try
            {

                var response = client.PutAsync("api/oficinas/" + oficina.Codigo_Oficina, new StringContent(JsonConvert.SerializeObject(oficina),
                        Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Oficina>(response.Content.ReadAsStringAsync().Result);
                else
                    return null;
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }
        }

        public bool BorrarPago(string codigo)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync("api/oficinas/" + codigo).Result;

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
