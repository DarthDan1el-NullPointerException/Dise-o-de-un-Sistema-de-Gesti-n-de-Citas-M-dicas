using SistemaCitasMedicas.Models;

namespace SistemaCitasMedicas.Interfaces
{
    public interface IReminderSender
    {
        void EnviarRecordatorio(Cita cita);
    }
}
