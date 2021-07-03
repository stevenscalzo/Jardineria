﻿using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
// Para contactar con la WEB API
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;

namespace Capa_Datos
{
    public class PagoADO : ADO
    {
        // Leo todos los pagos de la BD
        public List<Pago> LeerPagos()
        {
            List<Pago> listaUsuarios = new List<Pago>();
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/pagos").Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;

                    listaUsuarios = JsonConvert.DeserializeObject<List<Pago>>(aux);
                }
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }

            return listaUsuarios;
        }
        public List<Pago> LeerPago(int id)
        {
            List<Pago> listaUsuarios = new List<Pago>();
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/pagos/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;

                    listaUsuarios = JsonConvert.DeserializeObject<List<Pago>>(aux);
                }
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }

            return listaUsuarios;
        }

        // Creo un nuevo pago en la BD
        public bool InsertarPago(int codigo, string formaPago, string transaccion, DateTime fechaPago, decimal total)
        {
            try
            {
                Pago pay = new Pago(codigo, formaPago, transaccion, fechaPago, total);

                var response = client.PostAsync("api/pagos", new StringContent(JsonConvert.SerializeObject(pay),
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
        public Pago ActualizarPago(Pago pay)
        {
            try
            {

                var response = client.PutAsync("api/pagos/" + pay.Id_Transaccion, new StringContent(JsonConvert.SerializeObject(pay),
                        Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Pago>(response.Content.ReadAsStringAsync().Result);
                else
                    return null;
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }
        }

        public bool BorrarPago(string id)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync("api/pagos/" + id).Result;

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


