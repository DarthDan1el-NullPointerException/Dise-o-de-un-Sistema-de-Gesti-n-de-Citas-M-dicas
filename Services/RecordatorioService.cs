using SistemaCitasMedicas.Interfaces;
using SistemaCitasMedicas.Models;
using SistemaCitasMedicas.Validators;

namespace SistemaCitasMedicas.Services
{
    public class RecordatorioService
    {
        private readonly IReminderSender _sender;

        public RecordatorioService(IReminderSender sender)
        {
            _sender = sender;
        }

        public void EnviarRecordatorio(Cita cita)
        {
            if (cita == null)
                throw new ArgumentNullException("La cita no puede ser nula.");

            if (cita.Estado == EstadoCita.Cancelada)
            {
                Console.WriteLine("No se puede enviar recordatorio de una cita cancelada.");
                return;
            }

            _sender.EnviarRecordatorio(cita);
        }
    }
}
