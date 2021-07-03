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
    public class EmpleadoADO : ADO
    {
        // Leo todos los empleados de la BD
        public List<Empleado> LeerEmpleados()
        {
            List<Empleado> listaEmpleados = new List<Empleado>();
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/empleados").Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;

                    listaEmpleados = JsonConvert.DeserializeObject<List<Empleado>>(aux);
                }
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }

            return listaEmpleados;
        }
        public List<Empleado> LeerEmpleado(int codigo)
        {
            List<Empleado> listaEmpleados = new List<Empleado>();
            string aux;

            try
            {
                HttpResponseMessage response = client.GetAsync("api/empleados/" + codigo).Result;
                if (response.IsSuccessStatusCode)
                {
                    aux = response.Content.ReadAsStringAsync().Result;

                    listaEmpleados = JsonConvert.DeserializeObject<List<Empleado>>(aux);
                }
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }

            return listaEmpleados;
        }

        // Creo un nuevo empleado en la BD
        public bool InsertarEmpleado(int? codigoEmpleado, string nombre, string apellido1, string apellido2, 
            string extension, string email, string codigoOficina, int? codigoJefe, string puesto, string pass)
        {
            try
            {
                Empleado empleado = new Empleado(codigoEmpleado, nombre, apellido1, apellido2, extension,email, 
                    codigoOficina, codigoJefe, puesto, pass);

                var response = client.PostAsync("api/empleados", new StringContent(JsonConvert.SerializeObject(empleado),
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
        public Empleado ActualizarEmpleado(Empleado empleado)
        {
            try
            {

                var response = client.PutAsync("api/empleados/" + empleado.Codigo_Empleado, new StringContent(JsonConvert.SerializeObject(empleado),
                        Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Empleado>(response.Content.ReadAsStringAsync().Result);
                else
                    return null;
            }
            catch (Exception e)
            {
                throw new ExternalException("Error:" + e);
            }
        }

        public bool BorrarEmpleado(int codigo)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync("api/empleados/" + codigo).Result;

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
