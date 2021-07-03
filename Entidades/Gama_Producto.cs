///<author> Steven Scalzo </author>

using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Gama_Producto
    {

        [StringLength(50, ErrorMessage = "Gama invalida")]
        public string Gama { get; set; }
        public string Descripcion_Texto { get; set; }
        public string Descripcion_Html { get; set; }

        [StringLength(256, ErrorMessage = "Imagen invalida")]
        public string Imagen { get; set; }

        public Gama_Producto()
        {
            this.Gama = "";
            this.Descripcion_Texto = "";
            this.Descripcion_Html = "";
            this.Imagen = "";
        }

        public Gama_Producto(string gama, string descripcionTexto, string descripcionHtml, string imagen)
        {
            this.Gama = gama;
            this.Descripcion_Texto = descripcionTexto;
            this.Descripcion_Html = descripcionHtml;
            this.Imagen = imagen;
        }

        ~Gama_Producto()
        {
            System.Diagnostics.Trace.WriteLine("Destructor de la clase Gama ha sido llamado");

            this.Gama = "";
            this.Descripcion_Texto = "";
            this.Descripcion_Html = "";
            this.Imagen = "";

        }

        public override string ToString()
        {
            return Gama + " # " + Descripcion_Texto + " # " + Descripcion_Html + " # " + Imagen;
        }
    }
}
