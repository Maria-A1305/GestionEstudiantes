using GestionEstudiantes.Datos.Comun;

namespace GestionEstudiantes.Datos.Entidades
{
    /// <summary>
    /// Clase base abstracta que representa a un estudiante
    /// </summary>
    public abstract class Estudiante
    {
        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        // Array para almacenar calificaciones de exámenes
        private double[] calificacionesExamenes;
        // Array para almacenar calificaciones de prácticas
        private double[] calificacionesPracticas;

        private int contadorExamenes;
        private int contadorPracticas;

        protected Estudiante(string matricula, string nombre, string apellido)
        {
            Matricula = matricula;
            Nombre = nombre;
            Apellido = apellido;
            calificacionesExamenes = new double[10];
            calificacionesPracticas = new double[10];
            contadorExamenes = 0;
            contadorPracticas = 0;
        }

        /// <summary>
        /// Agrega una calificación de examen
        /// </summary>
        public OperationResult AgregarCalificacionExamen(double calificacion)
        {
            try
            {
                if (calificacion < 0 || calificacion > 100)
                {
                    return new OperationResult(false, "La calificación debe estar entre 0 y 100.");
                }

                if (contadorExamenes >= calificacionesExamenes.Length)
                {
                    return new OperationResult(false, "Se ha alcanzado el máximo de exámenes permitidos.");
                }

                calificacionesExamenes[contadorExamenes] = calificacion;
                contadorExamenes++;
                return new OperationResult(true, "Calificación de examen agregada exitosamente.");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, $"Error al agregar calificación de examen: {ex.Message}");
            }
        }

        /// <summary>
        /// Agrega una calificación de práctica
        /// </summary>
        public OperationResult AgregarCalificacionPractica(double calificacion)
        {
            try
            {
                if (calificacion < 0 || calificacion > 100)
                {
                    return new OperationResult(false, "La calificación debe estar entre 0 y 100.");
                }

                if (contadorPracticas >= calificacionesPracticas.Length)
                {
                    return new OperationResult(false, "Se ha alcanzado el máximo de prácticas permitidas.");
                }

                calificacionesPracticas[contadorPracticas] = calificacion;
                contadorPracticas++;
                return new OperationResult(true, "Calificación de práctica agregada exitosamente.");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, $"Error al agregar calificación de práctica: {ex.Message}");
            }
        }

        /// <summary>
        /// Calcula el promedio de exámenes
        /// </summary>
        public double CalcularPromedioExamenes()
        {
            if (contadorExamenes == 0) return 0;

            double suma = 0;
            for (int i = 0; i < contadorExamenes; i++)
            {
                suma += calificacionesExamenes[i];
            }
            return suma / contadorExamenes;
        }

        /// <summary>
        /// Calcula el promedio de prácticas
        /// </summary>
        public double CalcularPromedioPracticas()
        {
            if (contadorPracticas == 0) return 0;

            double suma = 0;
            for (int i = 0; i < contadorPracticas; i++)
            {
                suma += calificacionesPracticas[i];
            }
            return suma / contadorPracticas;
        }

        /// <summary>
        /// Método abstracto para calcular la nota final (polimorfismo)
        /// Cada tipo de estudiante puede tener su propia forma de calcular la nota final
        /// </summary>
        public abstract double CalcularNotaFinal();

        /// <summary>
        /// Método virtual para mostrar información del estudiante
        /// </summary>
        public virtual string MostrarInformacion()
        {
            return $"Matrícula: {Matricula}, Nombre: {Nombre} {Apellido}";
        }

        /// <summary>
        /// Obtiene el número de exámenes registrados
        /// </summary>
        public int ObtenerNumeroExamenes()
        {
            return contadorExamenes;
        }

        /// <summary>
        /// Obtiene el número de prácticas registradas
        /// </summary>
        public int ObtenerNumeroPracticas()
        {
            return contadorPracticas;
        }

        /// <summary>
        /// Verifica si el estudiante está aprobado (nota >= 70)
        /// </summary>
        public bool EstaAprobado()
        {
            return CalcularNotaFinal() >= 70;
        }
    }
}
