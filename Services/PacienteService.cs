using SistemaCitasMedicas.Models;
using SistemaCitasMedicas.Validators;

namespace SistemaCitasMedicas.Services
{
    public class PacienteService
    {
        private readonly List<Paciente> _pacientes = new List<Paciente>();
        private int _nextId = 1;

        public Paciente RegistrarPaciente(string nombre, string apellido, string telefono, string email)
        {
            // Usamos Validator para no repetir esta logica en otros servicios (DRY)
            Validator.ValidarCampoVacio(nombre, "Nombre");
            Validator.ValidarCampoVacio(apellido, "Apellido");
            Validator.ValidarCampoVacio(telefono, "Telefono");
            Validator.ValidarCampoVacio(email, "Email");

            var paciente = new Paciente(_nextId++, nombre, apellido, telefono, email);
            _pacientes.Add(paciente);
            return paciente;
        }

        public Paciente BuscarPaciente(int id)
        {
            var paciente = _pacientes.Find(p => p.Id == id);
            if (paciente == null)
                throw new InvalidOperationException($"No se encontro paciente con ID {id}.");
            return paciente;
        }

        public List<Paciente> ObtenerTodos()
        {
            return _pacientes;
        }
    }
}
