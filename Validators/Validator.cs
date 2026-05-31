namespace SistemaCitasMedicas.Validators
{
    // Centralizamos las validaciones aqui para no repetirlas en cada servicio (DRY)
    public static class Validator
    {
        public static void ValidarCampoVacio(string valor, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException($"El campo '{nombreCampo}' no puede estar vacio.");
        }

        public static void ValidarFechaFutura(DateTime fecha)
        {
            if (fecha <= DateTime.Now)
                throw new ArgumentException("La fecha de la cita debe ser en el futuro.");
        }

        public static void ValidarIdPositivo(int id, string nombreCampo)
        {
            if (id <= 0)
                throw new ArgumentException($"El ID de '{nombreCampo}' debe ser mayor a 0.");
        }
    }
}
