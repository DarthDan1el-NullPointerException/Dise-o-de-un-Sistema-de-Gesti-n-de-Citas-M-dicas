using SistemaCitasMedicas.Services;

class Program
{
    static CitaService citaService = new CitaService();
    static PacienteService pacienteService = new PacienteService();
    static MedicoService medicoService = new MedicoService();
    static RecordatorioService recordatorioService = new RecordatorioService(new EmailReminderSender());

    static void Main(string[] args)
    {
        Console.WriteLine("=== Sistema de Gestion de Citas Medicas ===\n");

        bool salir = false;
        while (!salir)
        {
            MostrarMenu();
            string opcion = Console.ReadLine()?.Trim() ?? "";

            try
            {
                switch (opcion)
                {
                    case "1": RegistrarPaciente(); break;
                    case "2": RegistrarEspecialidad(); break;
                    case "3": RegistrarMedico(); break;
                    case "4": AgendarCita(); break;
                    case "5": ConsultarCitasPorPaciente(); break;
                    case "6": ConsultarCitasPorMedico(); break;
                    case "7": CancelarCita(); break;
                    case "8": ReprogramarCita(); break;
                    case "9": EnviarRecordatorio(); break;
                    case "0": salir = true; break;
                    default: Console.WriteLine("Opcion no valida."); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            if (!salir)
            {
                Console.WriteLine("\nPresione Enter para continuar...");
                Console.ReadLine();
            }
        }

        Console.WriteLine("Saliendo del sistema...");
    }

    static void MostrarMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Sistema de Citas Medicas ===");
        Console.WriteLine("1. Registrar paciente");
        Console.WriteLine("2. Registrar especialidad");
        Console.WriteLine("3. Registrar medico");
        Console.WriteLine("4. Agendar cita");
        Console.WriteLine("5. Consultar citas por paciente");
        Console.WriteLine("6. Consultar citas por medico");
        Console.WriteLine("7. Cancelar cita");
        Console.WriteLine("8. Reprogramar cita");
        Console.WriteLine("9. Enviar recordatorio de cita");
        Console.WriteLine("0. Salir");
        Console.Write("\nSeleccione una opcion: ");
    }

    static void RegistrarPaciente()
    {
        Console.WriteLine("\n-- Registrar Paciente --");
        Console.Write("Nombre: "); string nombre = Console.ReadLine() ?? "";
        Console.Write("Apellido: "); string apellido = Console.ReadLine() ?? "";
        Console.Write("Telefono: "); string telefono = Console.ReadLine() ?? "";
        Console.Write("Email: "); string email = Console.ReadLine() ?? "";

        var paciente = pacienteService.RegistrarPaciente(nombre, apellido, telefono, email);
        Console.WriteLine($"\nPaciente registrado: {paciente}");
    }

    static void RegistrarEspecialidad()
    {
        Console.WriteLine("\n-- Registrar Especialidad --");
        Console.Write("Nombre de la especialidad: "); string nombre = Console.ReadLine() ?? "";

        var esp = medicoService.RegistrarEspecialidad(nombre);
        Console.WriteLine($"Especialidad registrada: {esp}");
    }

    static void RegistrarMedico()
    {
        Console.WriteLine("\n-- Registrar Medico --");

        var especialidades = medicoService.ObtenerEspecialidades();
        if (especialidades.Count == 0)
        {
            Console.WriteLine("No hay especialidades registradas. Registre una primero.");
            return;
        }

        Console.WriteLine("Especialidades disponibles:");
        especialidades.ForEach(e => Console.WriteLine($"  {e}"));

        Console.Write("Nombre: "); string nombre = Console.ReadLine() ?? "";
        Console.Write("Apellido: "); string apellido = Console.ReadLine() ?? "";
        Console.Write("ID de especialidad: "); int espId = int.Parse(Console.ReadLine() ?? "0");

        var medico = medicoService.RegistrarMedico(nombre, apellido, espId);
        Console.WriteLine($"\nMedico registrado: {medico}");
    }

    static void AgendarCita()
    {
        Console.WriteLine("\n-- Agendar Cita --");

        MostrarPacientes();
        MostrarMedicos();

        Console.Write("ID del paciente: "); int pacienteId = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("ID del medico: "); int medicoId = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Fecha y hora (dd/MM/yyyy HH:mm): "); string fechaStr = Console.ReadLine() ?? "";

        var paciente = pacienteService.BuscarPaciente(pacienteId);
        var medico = medicoService.BuscarMedico(medicoId);
        var fecha = DateTime.ParseExact(fechaStr, "dd/MM/yyyy HH:mm", null);

        var cita = citaService.AgendarCita(paciente, medico, fecha);
        Console.WriteLine($"\nCita agendada: {cita}");
    }

    static void ConsultarCitasPorPaciente()
    {
        Console.WriteLine("\n-- Consultar Citas por Paciente --");
        MostrarPacientes();
        Console.Write("ID del paciente: "); int id = int.Parse(Console.ReadLine() ?? "0");

        var citas = citaService.ConsultarPorPaciente(id);
        if (citas.Count == 0) { Console.WriteLine("No hay citas para este paciente."); return; }
        citas.ForEach(c => Console.WriteLine($"  {c}"));
    }

    static void ConsultarCitasPorMedico()
    {
        Console.WriteLine("\n-- Consultar Citas por Medico --");
        MostrarMedicos();
        Console.Write("ID del medico: "); int id = int.Parse(Console.ReadLine() ?? "0");

        var citas = citaService.ConsultarPorMedico(id);
        if (citas.Count == 0) { Console.WriteLine("No hay citas para este medico."); return; }
        citas.ForEach(c => Console.WriteLine($"  {c}"));
    }

    static void CancelarCita()
    {
        Console.WriteLine("\n-- Cancelar Cita --");
        MostrarCitas();
        Console.Write("ID de la cita a cancelar: "); int id = int.Parse(Console.ReadLine() ?? "0");
        citaService.CancelarCita(id);
        Console.WriteLine("Cita cancelada exitosamente.");
    }

    static void ReprogramarCita()
    {
        Console.WriteLine("\n-- Reprogramar Cita --");
        MostrarCitas();
        Console.Write("ID de la cita a reprogramar: "); int id = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Nueva fecha y hora (dd/MM/yyyy HH:mm): "); string fechaStr = Console.ReadLine() ?? "";

        var nuevaFecha = DateTime.ParseExact(fechaStr, "dd/MM/yyyy HH:mm", null);
        citaService.ReprogramarCita(id, nuevaFecha);
        Console.WriteLine("Cita reprogramada exitosamente.");
    }

    static void EnviarRecordatorio()
    {
        Console.WriteLine("\n-- Enviar Recordatorio --");
        MostrarCitas();
        Console.Write("ID de la cita: "); int id = int.Parse(Console.ReadLine() ?? "0");

        var cita = citaService.BuscarCita(id);
        recordatorioService.EnviarRecordatorio(cita);
    }

    static void MostrarPacientes()
    {
        var pacientes = pacienteService.ObtenerTodos();
        if (pacientes.Count == 0) { Console.WriteLine("No hay pacientes registrados."); return; }
        Console.WriteLine("Pacientes:");
        pacientes.ForEach(p => Console.WriteLine($"  {p}"));
    }

    static void MostrarMedicos()
    {
        var medicos = medicoService.ObtenerTodos();
        if (medicos.Count == 0) { Console.WriteLine("No hay medicos registrados."); return; }
        Console.WriteLine("Medicos:");
        medicos.ForEach(m => Console.WriteLine($"  {m}"));
    }

    static void MostrarCitas()
    {
        var citas = citaService.ObtenerTodas();
        if (citas.Count == 0) { Console.WriteLine("No hay citas registradas."); return; }
        Console.WriteLine("Citas:");
        citas.ForEach(c => Console.WriteLine($"  {c}"));
    }
}
