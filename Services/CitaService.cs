using SistemaCitasMedicas.Models;
using SistemaCitasMedicas.Validators;

namespace SistemaCitasMedicas.Services
{
    public class CitaService
    {
        private readonly List<Cita> _citas = new List<Cita>();
        private int _nextId = 1;

        public Cita AgendarCita(Paciente paciente, Medico medico, DateTime fechaHora)
        {
            // Usamos el validador central (DRY)
            Validator.ValidarFechaFutura(fechaHora);

            // Verificar si el medico ya tiene cita en ese horario
            bool disponible = !_citas.Exists(c =>
                c.Medico.Id == medico.Id &&
                c.FechaHora == fechaHora &&
                c.Estado == EstadoCita.Pendiente);

            if (!disponible)
                throw new InvalidOperationException("El medico ya tiene una cita en ese horario.");

            var cita = new Cita(_nextId++, paciente, medico, fechaHora);
            _citas.Add(cita);
            return cita;
        }

        public List<Cita> ConsultarPorPaciente(int pacienteId)
        {
            Validator.ValidarIdPositivo(pacienteId, "Paciente");
            return _citas.FindAll(c => c.Paciente.Id == pacienteId);
        }

        public List<Cita> ConsultarPorMedico(int medicoId)
        {
            Validator.ValidarIdPositivo(medicoId, "Medico");
            return _citas.FindAll(c => c.Medico.Id == medicoId);
        }

        public void CancelarCita(int citaId)
        {
            var cita = BuscarCita(citaId);
            if (cita.Estado == EstadoCita.Cancelada)
                throw new InvalidOperationException("La cita ya esta cancelada.");

            cita.Estado = EstadoCita.Cancelada;
        }

        public void ReprogramarCita(int citaId, DateTime nuevaFecha)
        {
            Validator.ValidarFechaFutura(nuevaFecha);
            var cita = BuscarCita(citaId);

            if (cita.Estado == EstadoCita.Cancelada)
                throw new InvalidOperationException("No se puede reprogramar una cita cancelada.");

            cita.FechaHora = nuevaFecha;
        }

        public Cita BuscarCita(int citaId)
        {
            var cita = _citas.Find(c => c.Id == citaId);
            if (cita == null)
                throw new InvalidOperationException($"No se encontro cita con ID {citaId}.");
            return cita;
        }

        public List<Cita> ObtenerTodas()
        {
            return _citas;
        }
    }
}
