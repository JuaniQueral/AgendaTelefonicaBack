namespace AgendaTelefonicaBack.Models
{
    public class Contacto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public long Telefono { get; set; }

        public int LlamadasEntrante { get; set; }

        public int LlamadasSaliente { get; set; }

        public DateTime FechaCreacion { get; set; }

        //Clave foranea

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

    }
}
