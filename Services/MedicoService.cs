using SistemaCitasMedicas.Models;
using SistemaCitasMedicas.Validators;

namespace SistemaCitasMedicas.Services
{
    public class MedicoService
    {
        private readonly List<Medico> _medicos = new List<Medico>();
        private readonly List<Especialidad> _especialidades = new List<Especialidad>();
        private int _nextMedicoId = 1;
        private int _nextEspecialidadId = 1;

        public Especialidad RegistrarEspecialidad(string nombre)
        {
            Validator.ValidarCampoVacio(nombre, "Nombre de especialidad");
            var especialidad = new Especialidad(_nextEspecialidadId++, nombre);
            _especialidades.Add(especialidad);
            return especialidad;
        }

        public Medico RegistrarMedico(string nombre, string apellido, int especialidadId)
        {
            Validator.ValidarCampoVacio(nombre, "Nombre");
            Validator.ValidarCampoVacio(apellido, "Apellido");

            var especialidad = BuscarEspecialidad(especialidadId);
            var medico = new Medico(_nextMedicoId++, nombre, apellido, especialidad);
            _medicos.Add(medico);
            return medico;
        }

        public Medico BuscarMedico(int id)
        {
            var medico = _medicos.Find(m => m.Id == id);
            if (medico == null)
                throw new InvalidOperationException($"No se encontro medico con ID {id}.");
            return medico;
        }

        public Especialidad BuscarEspecialidad(int id)
        {
            var esp = _especialidades.Find(e => e.Id == id);
            if (esp == null)
                throw new InvalidOperationException($"No se encontro especialidad con ID {id}.");
            return esp;
        }

        public List<Medico> ObtenerTodos()
        {
            return _medicos;
        }

        public List<Especialidad> ObtenerEspecialidades()
        {
            return _especialidades;
        }
    }
}
