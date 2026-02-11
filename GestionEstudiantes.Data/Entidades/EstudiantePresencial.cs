namespace GestionEstudiantes.Datos.Entidades
{
    /// <summary>
    /// Clase que representa a un estudiante presencial
    /// Los estudiantes presenciales tienen mayor peso en los exámenes (60%) que en las prácticas (40%)
    /// </summary>
    public class EstudiantePresencial : Estudiante
    {
        public EstudiantePresencial(string matricula, string nombre, string apellido)
            : base(matricula, nombre, apellido)
        {
        }

        /// <summary>
        /// Calcula la nota final para estudiantes presenciales
        /// Fórmula: 60% exámenes + 40% prácticas
        /// </summary>
        public override double CalcularNotaFinal()
        {
            double promedioExamenes = CalcularPromedioExamenes();
            double promedioPracticas = CalcularPromedioPracticas();

            return (promedioExamenes * 0.6) + (promedioPracticas * 0.4);
        }

        /// <summary>
        /// Sobrescribe el método para mostrar información específica del estudiante presencial
        /// </summary>
        public override string MostrarInformacion()
        {
            return base.MostrarInformacion() + " [Presencial]";
        }
    }
}
