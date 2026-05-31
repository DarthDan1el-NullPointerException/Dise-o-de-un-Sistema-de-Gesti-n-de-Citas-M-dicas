namespace SistemaCitasMedicas.Models
{
    public class Medico
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Especialidad Especialidad { get; set; }

        public Medico(int id, string nombre, string apellido, Especialidad especialidad)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Especialidad = especialidad;
        }

        public override string ToString()
        {
            return $"[{Id}] Dr. {Nombre} {Apellido} - Especialidad: {Especialidad.Nombre}";
        }
    }
}
