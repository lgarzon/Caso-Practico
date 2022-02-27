namespace CasoPractico.Core.DTOs
{
    public class AddClienteDto
    {
        public string identificacion { get; set; }

        public string nombre { get; set; }

        public string genero { get; set; }

        public int edad { get; set; }

        public string direccion { get; set; }

        public string telefono { get; set; }
     
        public string contrasenia { get; set; }

        public bool estado { get; set; }
    }
}
