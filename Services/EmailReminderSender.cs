using SistemaCitasMedicas.Interfaces;
using SistemaCitasMedicas.Models;

namespace SistemaCitasMedicas.Services
{
    public class EmailReminderSender : IReminderSender
    {
        public void EnviarRecordatorio(Cita cita)
        {
            // En una app real aqui iria la logica de envio de email
            Console.WriteLine($"[EMAIL] Recordatorio enviado a {cita.Paciente.Email}:");
            Console.WriteLine($"  Su cita con Dr. {cita.Medico.Nombre} {cita.Medico.Apellido} es el {cita.FechaHora:dd/MM/yyyy} a las {cita.FechaHora:HH:mm}");
        }
    }
}
