namespace clinica_api.Entities
{
    public class Cita
    {
        public int Id { get; set; }
        public DateTime fecha { get; set; }
        public int IdMedico { get; set; }
        public string? NombreMedico { get; set; }
        public string? Especialidad { get; set; }
    }
}
