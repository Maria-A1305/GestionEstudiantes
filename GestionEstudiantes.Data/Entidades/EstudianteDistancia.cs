namespace GestionEstudiantes.Datos.Entidades
{
    /// <summary>
    /// Clase que representa a un estudiante a distancia
    /// Los estudiantes a distancia tienen mayor peso en las prácticas (60%) que en los exámenes (40%)
    /// </summary>
    public class EstudianteDistancia : Estudiante
    {
        public EstudianteDistancia(string matricula, string nombre, string apellido)
            : base(matricula, nombre, apellido)
        {
        }

        /// <summary>
        /// Calcula la nota final para estudiantes a distancia
        /// Fórmula: 40% exámenes + 60% prácticas
        /// </summary>
        public override double CalcularNotaFinal()
        {
            double promedioExamenes = CalcularPromedioExamenes();
            double promedioPracticas = CalcularPromedioPracticas();

            return (promedioExamenes * 0.4) + (promedioPracticas * 0.6);
        }

        /// <summary>
        /// Sobrescribe el método para mostrar información específica del estudiante a distancia
        /// </summary>
        public override string MostrarInformacion()
        {
            return base.MostrarInformacion() + " [A Distancia]";
        }
    }
}
