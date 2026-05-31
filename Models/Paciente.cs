namespace SistemaCitasMedicas.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public Paciente(int id, string nombre, string apellido, string telefono, string email)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
            Email = email;
        }

        public override string ToString()
        {
            return $"[{Id}] {Nombre} {Apellido} - Tel: {Telefono} - Email: {Email}";
        }
    }
}
