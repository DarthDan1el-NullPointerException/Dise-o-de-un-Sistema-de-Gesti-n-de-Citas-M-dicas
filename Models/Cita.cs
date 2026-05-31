namespace SistemaCitasMedicas.Models
{
    public enum EstadoCita
    {
        Pendiente,
        Cancelada,
        Completada
    }

    public class Cita
    {
        public int Id { get; set; }
        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
        public DateTime FechaHora { get; set; }
        public EstadoCita Estado { get; set; }

        public Cita(int id, Paciente paciente, Medico medico, DateTime fechaHora)
        {
            Id = id;
            Paciente = paciente;
            Medico = medico;
            FechaHora = fechaHora;
            Estado = EstadoCita.Pendiente;
        }

        public override string ToString()
        {
            return $"[{Id}] {Paciente.Nombre} {Paciente.Apellido} con Dr. {Medico.Nombre} {Medico.Apellido} " +
                   $"- {FechaHora:dd/MM/yyyy HH:mm} - Estado: {Estado}";
        }
    }
}
