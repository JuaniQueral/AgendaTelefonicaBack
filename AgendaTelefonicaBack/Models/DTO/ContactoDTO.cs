namespace AgendaTelefonicaBack.Models.DTO
{
    public class ContactoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public long Telefono { get; set; }
        public int LlamadasEntrante { get; set; }
        public int LlamadasSaliente { get; set; }
    }
}
